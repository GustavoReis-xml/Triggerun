using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using UnityEngine;

// WEAPON-002. Bundle-owned data, immutable shared meshes and actor-local state.
// Native equip/material/network/combat logic remains authoritative.
public static class TriggerunWeaponCosmetics
{
    const BindingFlags Private = BindingFlags.NonPublic | BindingFlags.Instance;
    static readonly FieldInfo Hero = typeof(CHeroWeapon).GetField("nHeroId", Private);
    static readonly FieldInfo Cid = typeof(CHeroWeapon).GetField("nCid", Private);
    static readonly FieldInfo Type = typeof(CHeroWeapon).GetField("nModelType", Private);
    static readonly FieldInfo Weapon = typeof(CHeroWeapon).GetField("nWeaponId", Private);
    static readonly FieldInfo Mode = typeof(CHeroWeapon).GetField("nMode", Private);

    internal static WeaponUnlock Current(CHeroWeapon owner)
    {
        int hero = (int)Hero.GetValue(owner), id = (int)Weapon.GetValue(owner);
        if (id == -1)
            return (int)Mode.GetValue(owner) == 1 ? HeroDataManager.GetInstance().GetUseRobotWeaponData(hero, 1) :
                HeroDataManager.GetInstance().GetUseWeaponData(hero, (int)Cid.GetValue(owner));
        HeroUnlockData data;
        WeaponUnlock weapon;
        if (!fGameManager.mloadConfig.AllHeroUnlock.TryGetValue(hero, out data) ||
            !data.szWeaponUnlock.TryGetValue(id, out weapon)) return null;
        return weapon;
    }

    sealed class Request
    {
        public string path;
        public int revision;
    }

    public static void Prepare(CHeroWeapon owner, int mode)
    {
        if (owner == null) return;
        TriggerunWeaponCosmeticState state = owner.GetComponent<TriggerunWeaponCosmeticState>();
        if (state != null) state.Restore(false);
        if (state == null) state = owner.gameObject.AddComponent<TriggerunWeaponCosmeticState>();
        state.Capture(owner);
        state.BeginRequest(mode == 2);
    }

    public static void EndPrepare(CHeroWeapon owner)
    {
        if (owner == null) return;
        TriggerunWeaponCosmeticState state = owner.GetComponent<TriggerunWeaponCosmeticState>();
        if (state != null) state.EndRequestSetup();
    }

    public static object Wrap(string path, CHeroWeapon owner)
    {
        Request request = new Request(); request.path = path;
        TriggerunWeaponCosmeticState state = owner.GetComponent<TriggerunWeaponCosmeticState>();
        request.revision = state != null ? state.Revision : -1;
        return request;
    }

    public static bool Accept(CHeroWeapon owner, WWW www, object custom)
    {
        if (owner == null || www == null || www.error != null || www.assetBundle == null) return false;
        Request request = custom as Request;
        TriggerunWeaponCosmeticState state = owner.GetComponent<TriggerunWeaponCosmeticState>();
        if (request == null || state == null || !state.IsCurrent(request.revision)) return false;
        WeaponUnlock weapon = Current(owner);
        if (weapon == null) return false;
        int type = (int)Type.GetValue(owner), mode = (int)Mode.GetValue(owner);
        string name = mode == 2 ? weapon.highMat_1st : type != 1 ? weapon.comMat_3rd :
            GameRoot.inst.Setting.Video_Class.Effect_Level != 0 ? weapon.highMat_1st : weapon.lowMat_1st;
        bool accepted = !String.IsNullOrEmpty(name) && String.Equals(request.path,
            "HeroAnimator/" + name + ".mo", StringComparison.OrdinalIgnoreCase);
        return accepted;
    }

    public static void Apply(CHeroWeapon owner, WWW www, object custom)
    {
        if (!Accept(owner, www, custom)) return;
        TriggerunWeaponCosmeticState state = owner.GetComponent<TriggerunWeaponCosmeticState>();
        if (state == null) return;
        string metadata = null;
        foreach (string asset in www.assetBundle.GetAllAssetNames())
            if (asset.EndsWith("/tr_weapon_cosmetic.xml", StringComparison.OrdinalIgnoreCase)) { metadata = asset; break; }
        if (metadata == null) return; // Original material-only cosmetics.
        try
        {
            state.Restore(false); // Duplicate callbacks cannot stack effects.
            TextAsset text = www.assetBundle.LoadAsset<TextAsset>(metadata);
            if (text == null) throw new InvalidOperationException("Missing weapon metadata");
            XmlDocument document = new XmlDocument(); document.XmlResolver = null;
            document.LoadXml(text.text);
            XmlElement root = document.DocumentElement;
            WeaponUnlock weapon = Current(owner);
            if (root == null || root.Name != "weaponCosmetic" || root.GetAttribute("version") != "1" ||
                Int32.Parse(root.GetAttribute("hero"), CultureInfo.InvariantCulture) != (int)Hero.GetValue(owner) ||
                Int32.Parse(root.GetAttribute("id"), CultureInfo.InvariantCulture) != weapon.nId)
                throw new InvalidOperationException("Weapon metadata identity/version mismatch");
            state.Apply(owner, www.assetBundle, root);
        }
        catch (Exception error)
        {
            state.Restore(true);
            Debug.LogWarning("[WeaponCosmetics] Restored original weapon: " + error.Message);
        }
    }
}

public sealed class TriggerunWeaponCosmeticState : MonoBehaviour
{
    sealed class Binding
    {
        public Renderer renderer;
        public MeshFilter filter;
        public SkinnedMeshRenderer skin;
        public Mesh mesh;
        public Material[] materials;
        public Bounds bounds;
    }
    sealed class ParticleState
    {
        public ParticleSystem particle;
        public ParticleSystem.MinMaxGradient start, lifetime, speed;
    }
    sealed class MaterialState
    {
        public Renderer renderer;
        public Material[] original, copies;
    }
    sealed class TrailState
    {
        public TrailRenderer trail;
        public Gradient gradient;
    }
    readonly List<Binding> bindings = new List<Binding>();
    readonly List<Binding> companions = new List<Binding>();
    readonly List<ParticleState> particles = new List<ParticleState>();
    readonly List<MaterialState> materials = new List<MaterialState>();
    readonly List<TrailState> trails = new List<TrailState>();
    int revision;
    bool alive, ignoreNextDisable;
    public int Revision { get { return revision; } }
    public bool IsCurrent(int value) { return alive && value == revision; }
    public void BeginRequest(bool shop)
    {
        unchecked { revision++; }
        alive = true;
        // The original shop loader hides the model synchronously while its
        // material downloads. This one disable must not invalidate the lease.
        ignoreNextDisable = shop && gameObject.activeSelf;
    }
    public void EndRequestSetup() { ignoreNextDisable = false; }

    public void Capture(CHeroWeapon owner)
    {
        if (owner.WeaponTransform == null) return;
        HashSet<Renderer> current = new HashSet<Renderer>();
        foreach (Transform slot in owner.WeaponTransform)
            if (slot != null && slot.GetComponent<Renderer>() != null) current.Add(slot.GetComponent<Renderer>());
        bindings.RemoveAll(delegate(Binding b) { return b.renderer == null || !current.Contains(b.renderer); });
        foreach (Transform transform in owner.WeaponTransform)
        {
            if (transform == null) continue;
            Renderer renderer = transform.GetComponent<Renderer>();
            if (renderer == null || bindings.Exists(delegate(Binding b) { return b.renderer == renderer; })) continue;
            MeshFilter filter = transform.GetComponent<MeshFilter>();
            SkinnedMeshRenderer skin = renderer as SkinnedMeshRenderer;
            if (filter == null && skin == null) continue;
            Binding item = new Binding(); item.renderer = renderer; item.filter = filter; item.skin = skin;
            item.mesh = filter != null ? filter.sharedMesh : skin.sharedMesh;
            item.materials = renderer.sharedMaterials;
            if (skin != null) item.bounds = skin.localBounds;
            bindings.Add(item);
        }
    }

    public void Apply(CHeroWeapon owner, AssetBundle bundle, XmlElement root)
    {
        Dictionary<string, string> map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (XmlElement entry in root.SelectNodes("meshes/mesh"))
        {
            string source = entry.GetAttribute("source"), asset = entry.GetAttribute("asset");
            if (source.Length == 0 || asset.Length == 0 || map.ContainsKey(source))
                throw new InvalidOperationException("Invalid or duplicate mesh mapping");
            map.Add(source, asset);
        }
        // Some native prefabs have a separate hologram/visibility shell that
        // is not listed in WeaponTransform. Explicit paths keep its geometry
        // aligned while preserving its original material and visibility logic.
        HashSet<Renderer> seen = new HashSet<Renderer>();
        foreach (Binding item in bindings) if (item.renderer != null) seen.Add(item.renderer);
        foreach (XmlElement entry in root.SelectNodes("meshes/companion"))
        {
            string path = entry.GetAttribute("path"), source = entry.GetAttribute("source");
            if (path.Length == 0 || source.Length == 0 || !map.ContainsKey(source))
                throw new InvalidOperationException("Invalid companion mapping");
            Transform transform = owner.transform.Find(path);
            if (transform == null) continue; // Metadata can describe several prefab profiles.
            Renderer renderer = transform.GetComponent<Renderer>();
            MeshFilter filter = transform.GetComponent<MeshFilter>();
            SkinnedMeshRenderer skin = renderer as SkinnedMeshRenderer;
            if (renderer == null || (filter == null && skin == null))
                throw new InvalidOperationException("Companion is not a mesh renderer: " + path);
            Mesh mesh = filter != null ? filter.sharedMesh : skin.sharedMesh;
            string actual = mesh != null ? mesh.name.Replace(" (Clone)", "").Replace("(Clone)", "") : "";
            if (!String.Equals(actual, source, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Companion mesh mismatch: " + path);
            if (!seen.Add(renderer)) continue;
            Binding item = new Binding(); item.renderer = renderer; item.filter = filter; item.skin = skin;
            item.mesh = mesh; item.materials = renderer.sharedMaterials;
            if (skin != null) item.bounds = skin.localBounds;
            companions.Add(item);
        }
        List<Binding> all = new List<Binding>(bindings); all.AddRange(companions);
        List<Mesh> targets = new List<Mesh>();
        // Validate the ENTIRE set before modifying any renderer/LOD.
        foreach (Binding item in all)
        {
            if (item.renderer == null) { targets.Add(null); continue; }
            string asset;
            string name = item.mesh != null ? item.mesh.name.Replace(" (Clone)", "").Replace("(Clone)", "") : "";
            if (!map.TryGetValue(name, out asset)) throw new InvalidOperationException("No mesh mapping for " + name);
            Mesh mesh = bundle.LoadAsset<Mesh>(asset);
            if (mesh == null || mesh.vertexCount == 0 || mesh.subMeshCount != 1)
                throw new InvalidOperationException("Missing or invalid target mesh " + asset);
            if (item.skin != null && mesh.bindposes.Length != item.mesh.bindposes.Length)
                throw new InvalidOperationException("Skinned weapon bind palette mismatch");
            targets.Add(mesh);
        }
        if (targets.Count == 0) throw new InvalidOperationException("No weapon renderers");
        for (int i = 0; i < all.Count; i++)
        {
            Binding item = all[i]; Mesh mesh = targets[i];
            if (item.renderer == null || mesh == null) continue;
            if (item.filter != null) item.filter.sharedMesh = mesh;
            else { item.skin.sharedMesh = mesh; item.skin.localBounds = mesh.bounds; }
        }
        HashSet<ParticleSystem> touchedParticles = new HashSet<ParticleSystem>();
        HashSet<Renderer> touchedRenderers = new HashSet<Renderer>();
        HashSet<TrailRenderer> touchedTrails = new HashSet<TrailRenderer>();
        foreach (XmlElement entry in root.SelectNodes("effects/effect"))
        {
            Transform anchor = owner.transform.Find(entry.GetAttribute("path"));
            if (anchor == null) continue;
            string[] rgb = entry.GetAttribute("color").Split(',');
            if (rgb.Length != 3) throw new InvalidOperationException("Invalid effect tint");
            Color color = new Color(Single.Parse(rgb[0], CultureInfo.InvariantCulture),
                Single.Parse(rgb[1], CultureInfo.InvariantCulture), Single.Parse(rgb[2], CultureInfo.InvariantCulture), 1f);
            foreach (float value in new float[] { color.r, color.g, color.b })
                if (Single.IsNaN(value) || Single.IsInfinity(value) || value < 0f || value > 8f)
                    throw new InvalidOperationException("Invalid effect color range");
            foreach (ParticleSystem particle in anchor.GetComponentsInChildren<ParticleSystem>(true))
            {
                if (!touchedParticles.Add(particle)) continue;
                ParticleState item = new ParticleState(); item.particle = particle;
                ParticleSystem.MainModule main = particle.main;
                ParticleSystem.ColorOverLifetimeModule lifetime = particle.colorOverLifetime;
                ParticleSystem.ColorBySpeedModule speed = particle.colorBySpeed;
                item.start = main.startColor; item.lifetime = lifetime.color; item.speed = speed.color;
                particles.Add(item);
                main.startColor = Tint(item.start, color); lifetime.color = Tint(item.lifetime, color); speed.color = Tint(item.speed, color);
                particle.Clear(false);
            }
            foreach (Renderer renderer in anchor.GetComponentsInChildren<Renderer>(true))
            {
                if (!touchedRenderers.Add(renderer)) continue;
                MaterialState item = new MaterialState(); item.renderer = renderer; item.original = renderer.sharedMaterials;
                item.copies = new Material[item.original.Length]; materials.Add(item);
                for (int i = 0; i < item.original.Length; i++)
                {
                    if (item.original[i] == null) continue;
                    Material material = new Material(item.original[i]); item.copies[i] = material;
                    foreach (string property in new string[] { "_TintColor", "_Color", "_EmissionColor" })
                        if (material.HasProperty(property)) material.SetColor(property, Tint(material.GetColor(property), color));
                }
                renderer.sharedMaterials = item.copies;
            }
            foreach (TrailRenderer trail in anchor.GetComponentsInChildren<TrailRenderer>(true))
            {
                if (!touchedTrails.Add(trail)) continue;
                TrailState item = new TrailState(); item.trail = trail; item.gradient = trail.colorGradient; trails.Add(item);
                trail.colorGradient = Tint(item.gradient, color); trail.Clear();
            }
        }
    }

    static Color Tint(Color old, Color color)
    {
        float intensity = Mathf.Max(old.r, Mathf.Max(old.g, old.b));
        return new Color(color.r * intensity, color.g * intensity, color.b * intensity, old.a);
    }
    static Gradient Tint(Gradient old, Color color)
    {
        if (old == null) return null;
        Gradient result = new Gradient(); GradientColorKey[] keys = old.colorKeys;
        for (int i = 0; i < keys.Length; i++) keys[i].color = Tint(keys[i].color, color);
        result.SetKeys(keys, old.alphaKeys); result.mode = old.mode; return result;
    }
    static ParticleSystem.MinMaxGradient Tint(ParticleSystem.MinMaxGradient old, Color color)
    {
        ParticleSystem.MinMaxGradient result = old;
        result.colorMin = Tint(old.colorMin, color); result.colorMax = Tint(old.colorMax, color);
        result.gradientMin = Tint(old.gradientMin, color); result.gradientMax = Tint(old.gradientMax, color);
        return result;
    }

    public void Restore(bool fallbackMaterials)
    {
        List<Binding> all = new List<Binding>(bindings); all.AddRange(companions);
        foreach (Binding item in all)
        {
            if (item.renderer == null) continue;
            if (item.filter != null) item.filter.sharedMesh = item.mesh;
            else if (item.skin != null) { item.skin.sharedMesh = item.mesh; item.skin.localBounds = item.bounds; }
            if (fallbackMaterials) item.renderer.sharedMaterials = item.materials;
        }
        foreach (ParticleState item in particles)
        {
            if (item.particle == null) continue;
            ParticleSystem.MainModule main = item.particle.main;
            ParticleSystem.ColorOverLifetimeModule lifetime = item.particle.colorOverLifetime;
            ParticleSystem.ColorBySpeedModule speed = item.particle.colorBySpeed;
            main.startColor = item.start; lifetime.color = item.lifetime; speed.color = item.speed;
            item.particle.Clear(false);
        }
        foreach (TrailState item in trails)
            if (item.trail != null) { item.trail.colorGradient = item.gradient; item.trail.Clear(); }
        foreach (MaterialState item in materials)
        {
            if (item.renderer != null) item.renderer.sharedMaterials = item.original;
            foreach (Material copy in item.copies) if (copy != null) UnityEngine.Object.Destroy(copy);
        }
        particles.Clear(); trails.Clear(); materials.Clear(); companions.Clear();
    }
    void OnDisable()
    {
        Restore(false);
        if (ignoreNextDisable) ignoreNextDisable = false;
        else { alive = false; unchecked { revision++; } }
    }
    void OnDestroy() { alive = false; Restore(false); bindings.Clear(); }
}

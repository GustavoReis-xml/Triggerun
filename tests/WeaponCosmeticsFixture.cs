using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace UnityEngine
{
    public class Object
    {
        public string name = ""; public bool destroyed;
        public static int deleted;
        public static void Destroy(Object value) { value.destroyed = true; deleted++; }
    }
    public class Component : Object
    {
        public GameObject gameObject; public Transform transform { get { return gameObject.transform; } }
        public T GetComponent<T>() where T : class { return gameObject.GetComponent<T>(); }
        public T[] GetComponentsInChildren<T>(bool inactive) where T : class { return gameObject.GetComponentsInChildren<T>(inactive); }
    }
    public class MonoBehaviour : Component { }
    public class GameObject : Object
    {
        readonly List<Component> components = new List<Component>();
        public Transform transform; public bool activeSelf = true;
        public GameObject(string n) { name = n; transform = new Transform(); transform.gameObject = this; components.Add(transform); }
        public T AddComponent<T>() where T : Component, new() { T c = new T(); c.gameObject = this; components.Add(c); return c; }
        public T GetComponent<T>() where T : class { return components.OfType<T>().FirstOrDefault(); }
        public T[] GetComponentsInChildren<T>(bool inactive) where T : class
        {
            List<T> result = new List<T>(); if (inactive || activeSelf) result.AddRange(components.OfType<T>());
            foreach (Transform t in transform.children) result.AddRange(t.gameObject.GetComponentsInChildren<T>(inactive)); return result.ToArray();
        }
        public void SetActive(bool value)
        {
            bool disable = activeSelf && !value; activeSelf = value;
            if (disable) foreach (Component c in components.ToArray())
            {
                MethodInfo m = c.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic);
                if (m != null) m.Invoke(c, null);
            }
        }
    }
    public class Transform : Component
    {
        public List<Transform> children = new List<Transform>();
        public Transform Find(string path)
        {
            Transform t = this; foreach (string n in path.Split('/')) { t = t.children.FirstOrDefault(x => x.gameObject.name == n); if (t == null) break; } return t;
        }
    }
    public struct Bounds { public int value; }
    public struct Matrix4x4 { }
    public class Mesh : Object { public int vertexCount = 10, subMeshCount = 1; public Bounds bounds; public Matrix4x4[] bindposes = new Matrix4x4[0]; }
    public class MeshFilter : Component { public Mesh sharedMesh; }
    public class Renderer : Component
    {
        public bool enabled = true;
        Material[] m = new Material[0];
        public Material[] sharedMaterials { get { return (Material[])m.Clone(); } set { m = (Material[])value.Clone(); } }
    }
    public class MeshRenderer : Renderer { }
    public class SkinnedMeshRenderer : Renderer { public Mesh sharedMesh; public Bounds localBounds; }
    public class Material : Object
    {
        readonly Dictionary<string, Color> colors = new Dictionary<string, Color>();
        public Material() { colors["_TintColor"] = new Color(.2f, .8f, .4f, .7f); }
        public Material(Material other) { name = other.name; foreach (var p in other.colors) colors[p.Key] = p.Value; }
        public bool HasProperty(string p) { return colors.ContainsKey(p); }
        public Color GetColor(string p) { return colors[p]; }
        public void SetColor(string p, Color color) { colors[p] = color; }
    }
    public struct Color
    {
        public float r, g, b, a;
        public Color(float r, float g, float b, float a) { this.r = r; this.g = g; this.b = b; this.a = a; }
    }
    public enum GradientMode { Blend, Fixed }
    public struct GradientColorKey { public Color color; public float time; public GradientColorKey(Color c, float t) { color = c; time = t; } }
    public struct GradientAlphaKey { public float alpha, time; public GradientAlphaKey(float a, float t) { alpha = a; time = t; } }
    public class Gradient
    {
        GradientColorKey[] c = new GradientColorKey[0]; GradientAlphaKey[] a = new GradientAlphaKey[0];
        public GradientMode mode;
        public GradientColorKey[] colorKeys { get { return (GradientColorKey[])c.Clone(); } }
        public GradientAlphaKey[] alphaKeys { get { return (GradientAlphaKey[])a.Clone(); } }
        public void SetKeys(GradientColorKey[] colors, GradientAlphaKey[] alpha) { c = (GradientColorKey[])colors.Clone(); a = (GradientAlphaKey[])alpha.Clone(); }
    }
    public class ParticleSystem : Component
    {
        public struct MinMaxGradient { public Color colorMin, colorMax; public Gradient gradientMin, gradientMax; public int mode; }
        MinMaxGradient start, life, speed;
        public bool lifetimeEnabled = true, speedEnabled = false, isPlaying = true; public int clears;
        public struct MainModule { public ParticleSystem p; public MinMaxGradient startColor { get { return p.start; } set { p.start = value; } } }
        public struct ColorOverLifetimeModule { public ParticleSystem p; public MinMaxGradient color { get { return p.life; } set { p.life = value; } } }
        public struct ColorBySpeedModule { public ParticleSystem p; public MinMaxGradient color { get { return p.speed; } set { p.speed = value; } } }
        public MainModule main { get { return new MainModule { p = this }; } }
        public ColorOverLifetimeModule colorOverLifetime { get { return new ColorOverLifetimeModule { p = this }; } }
        public ColorBySpeedModule colorBySpeed { get { return new ColorBySpeedModule { p = this }; } }
        public void Clear(bool children) { clears++; }
    }
    public class TrailRenderer : Renderer { public Gradient colorGradient; public int clears; public void Clear() { clears++; } }
    public class TextAsset : Object { public string text; }
    public class AssetBundle : Object
    {
        public Dictionary<string, Object> assets = new Dictionary<string, Object>();
        public string[] GetAllAssetNames() { return assets.Keys.OrderBy(x => x).ToArray(); }
        public T LoadAsset<T>(string n) where T : Object { Object value; return assets.TryGetValue(n, out value) ? value as T : null; }
    }
    public class WWW { public string error; public AssetBundle assetBundle; }
    public static class Mathf { public static float Max(float a, float b) { return Math.Max(a, b); } }
    public static class Debug { public static int warnings; public static void LogWarning(object value) { warnings++; } }
}
public class WeaponUnlock { public int nId; public string lowMat_1st, highMat_1st, comMat_3rd; }
public class HeroUnlockData { public Dictionary<int, WeaponUnlock> szWeaponUnlock = new Dictionary<int, WeaponUnlock>(); }
public class Config { public Dictionary<int, HeroUnlockData> AllHeroUnlock = new Dictionary<int, HeroUnlockData>(); }
public static class fGameManager { public static Config mloadConfig = new Config(); }
public class HeroDataManager
{
    static HeroDataManager instance = new HeroDataManager(); public static Dictionary<int, int> selected = new Dictionary<int, int>();
    public static HeroDataManager GetInstance() { return instance; }
    public WeaponUnlock GetUseWeaponData(int h, int c) { return fGameManager.mloadConfig.AllHeroUnlock[h].szWeaponUnlock[selected[h]]; }
    public WeaponUnlock GetUseRobotWeaponData(int h, int c) { return fGameManager.mloadConfig.AllHeroUnlock[h].szWeaponUnlock[1]; }
}
public class GameRoot
{
    public class Video { public int Effect_Level = 1; } public class Settings { public Video Video_Class = new Video(); }
    public Settings Setting = new Settings(); public static GameRoot inst = new GameRoot();
}
public class CHeroWeapon : MonoBehaviour
{
    int nHeroId, nCid, nModelType, nWeaponId, nMode;
    public Transform[] WeaponTransform;
    public int hero, equipped, mode, partsCalls;
    public void Set(int h, int type, int id, int m) { nHeroId = hero = h; nCid = 1; nModelType = type; nWeaponId = equipped = id; nMode = mode = m; }
}

class WeaponCosmeticsFixture
{
    static int assertions;
    static void Check(bool value, string reason) { assertions++; if (!value) throw new Exception(reason); }
    static bool Near(float a, float b) { return Math.Abs(a - b) < .00001f; }
    static Gradient OriginalGradient()
    {
        Gradient g = new Gradient(); g.mode = GradientMode.Fixed;
        g.SetKeys(new[] { new GradientColorKey(new Color(.2f, .8f, .4f, .6f), 0f), new GradientColorKey(new Color(.1f, .3f, .7f, .4f), .8f) },
            new[] { new GradientAlphaKey(.8f, 0f), new GradientAlphaKey(0f, 1f) }); return g;
    }
    static Transform Path(GameObject root, string path)
    {
        Transform t = root.transform;
        foreach (string name in path.Split('/'))
        {
            Transform child = t.children.FirstOrDefault(x => x.gameObject.name == name);
            if (child == null) { child = new GameObject(name).transform; t.children.Add(child); } t = child;
        }
        return t;
    }
    class Actor
    {
        public GameObject root; public CHeroWeapon owner; public MeshFilter[] filters; public Mesh[] originals;
        public ParticleSystem particle, unrelated; public Renderer fx; public TrailRenderer trail; public Material sharedFx; public Gradient sharedGradient;
        public Actor(int hero, string[] names, Material shared, Gradient gradient)
        {
            root = new GameObject("Actor"); owner = root.AddComponent<CHeroWeapon>(); owner.Set(hero, 2, 1, 0);
            filters = new MeshFilter[names.Length]; originals = new Mesh[names.Length]; owner.WeaponTransform = new Transform[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                Transform t = Path(root, "Weapon" + i); filters[i] = t.gameObject.AddComponent<MeshFilter>(); originals[i] = new Mesh { name = names[i], bounds = new Bounds { value = 10 + i } }; filters[i].sharedMesh = originals[i];
                t.gameObject.AddComponent<MeshRenderer>().sharedMaterials = new[] { new Material { name = "native" } }; owner.WeaponTransform[i] = t;
            }
            sharedFx = shared; sharedGradient = gradient;
            GameObject effect = Path(root, "fx").gameObject; particle = effect.AddComponent<ParticleSystem>(); fx = effect.AddComponent<MeshRenderer>(); fx.sharedMaterials = new Material[] { shared, null };
            var main = particle.main; main.startColor = new ParticleSystem.MinMaxGradient { colorMin = new Color(.2f, .8f, .4f, .7f), colorMax = new Color(.1f, .3f, .7f, .8f), gradientMin = gradient, gradientMax = gradient, mode = 3 };
            var life = particle.colorOverLifetime; life.color = main.startColor;
            var speed = particle.colorBySpeed; speed.color = main.startColor;
            trail = Path(root, "fx/sub").gameObject.AddComponent<TrailRenderer>(); trail.sharedMaterials = new[] { shared }; trail.colorGradient = gradient;
            unrelated = Path(root, "bodySkill").gameObject.AddComponent<ParticleSystem>(); var um = unrelated.main; um.startColor = main.startColor;
        }
        public void Original() { for (int i = 0; i < filters.Length; i++) Check(ReferenceEquals(filters[i].sharedMesh, originals[i]), "Original mesh not restored"); }
        public void NativeEffects()
        {
            Check(ReferenceEquals(fx.sharedMaterials[0], sharedFx), "Shared FX material not restored");
            Check(ReferenceEquals(particle.main.startColor.gradientMin, sharedGradient), "Particle gradient not restored");
            Check(ReferenceEquals(trail.colorGradient, sharedGradient), "Trail not restored");
        }
    }
    static string Xml(int hero, int weapon, string[] names, string color)
    {
        return "<weaponCosmetic version='1' hero='" + hero + "' id='" + weapon + "'><meshes>" + String.Join("", names.Select((x, i) => "<mesh source='" + x + "' asset='mesh" + i + "'/>")) +
            "</meshes><effects><effect path='fx' color='" + color + "'/><effect path='fx/sub' color='" + color + "'/></effects></weaponCosmetic>";
    }
    static AssetBundle Bundle(string xml, int meshes)
    {
        AssetBundle b = new AssetBundle(); b.assets.Add("assets/00.mat", new Material { name = "selected" });
        if (xml != null) b.assets.Add("assets/tr_weapon_cosmetic.xml", new TextAsset { text = xml });
        for (int i = 0; i < meshes; i++) b.assets.Add("mesh" + i, new Mesh { name = "New" + i, vertexCount = 50 + i }); return b;
    }
    static void Register(int hero)
    {
        HeroUnlockData data = new HeroUnlockData();
        foreach (int id in new[] { 1, 5, 6 }) data.szWeaponUnlock.Add(id, new WeaponUnlock { nId = id, highMat_1st = "mat" + id, lowMat_1st = "mat" + id, comMat_3rd = "mat" + id });
        fGameManager.mloadConfig.AllHeroUnlock[hero] = data; HeroDataManager.selected[hero] = 5;
    }
    class Request { public Actor a; public object token; public WWW www; }
    static Request Begin(Actor a, int id, AssetBundle bundle, int mode = 0, int type = 2)
    {
        TriggerunWeaponCosmetics.Prepare(a.owner, mode); a.owner.Set(a.owner.hero, type, id, mode);
        int selected = id == -1 ? mode == 1 ? 1 : HeroDataManager.selected[a.owner.hero] : id;
        Request r = new Request { a = a, token = TriggerunWeaponCosmetics.Wrap("HeroAnimator/mat" + selected + ".mo", a.owner), www = new WWW { assetBundle = bundle } };
        if (mode == 2) a.root.SetActive(false);
        TriggerunWeaponCosmetics.EndPrepare(a.owner); return r;
    }
    static bool Finish(Request r)
    {
        if (!TriggerunWeaponCosmetics.Accept(r.a.owner, r.www, r.token)) return false;
        foreach (Transform t in r.a.owner.WeaponTransform) t.GetComponent<Renderer>().sharedMaterials = new[] { r.www.assetBundle.LoadAsset<Material>("assets/00.mat") };
        if (r.a.owner.mode == 2) r.a.root.SetActive(true);
        r.a.owner.partsCalls++; TriggerunWeaponCosmetics.Apply(r.a.owner, r.www, r.token); return true;
    }
    static void Main()
    {
        Register(1016); Register(1021); string[] names = { "waerjili_tps_wq", "waerjili_wuqi_500", "waerjili_wuqi_2500" };
        Material shared = new Material(); Gradient gradient = OriginalGradient();
        Actor a = new Actor(1016, names, shared, gradient), b = new Actor(1016, names, shared, gradient);
        AssetBundle custom = Bundle(Xml(1016, 5, names, "1,.035,.085"), 3), native = Bundle(null, 0);
        Check(Finish(Begin(a, 1, native)), "Native callback failed"); a.Original();
        Request first = Begin(a, 5, custom); Check(Finish(first), "Custom callback failed");
        for (int i = 0; i < names.Length; i++) Check(ReferenceEquals(a.filters[i].sharedMesh, custom.LoadAsset<Mesh>("mesh" + i)), "LOD mapping failed");
        Check(!ReferenceEquals(a.fx.sharedMaterials[0], shared), "FX did not get private material");
        Check(Near(shared.GetColor("_TintColor").g, .8f), "Shared material mutated");
        Check(Near(gradient.colorKeys[0].color.g, .8f), "Shared gradient mutated");
        Check(Near(a.particle.main.startColor.colorMin.r, .8f) && Near(a.particle.main.startColor.colorMin.g, .028f), "Crimson tint missing");
        Check(Near(a.particle.main.startColor.colorMin.a, .7f) && a.particle.main.startColor.mode == 3, "Alpha/random mode modified");
        Check(a.particle.lifetimeEnabled && !a.particle.speedEnabled && a.particle.isPlaying, "Native effect activation changed");
        Check(Near(a.particle.main.startColor.gradientMin.alphaKeys[0].alpha, .8f) && a.trail.colorGradient.mode == GradientMode.Fixed, "Gradient timing/mode changed");
        Check(ReferenceEquals(a.unrelated.main.startColor.gradientMin, gradient), "Body skill recolored"); b.Original(); b.NativeEffects();
        Material copy = a.fx.sharedMaterials[0]; Check(Finish(first), "Duplicate callback rejected unexpectedly"); Check(copy.destroyed, "Duplicate callback leaked material");
        Check(Finish(Begin(b, 5, custom)), "Second actor failed");
        Check(!ReferenceEquals(a.fx.sharedMaterials[0], b.fx.sharedMaterials[0]), "Actors share mutable FX");
        Check(Finish(Begin(a, 1, native)), "Native restore callback failed"); a.Original(); a.NativeEffects();
        Check(ReferenceEquals(b.filters[0].sharedMesh, custom.LoadAsset<Mesh>("mesh0")), "Restoring A affected B");
        Request stale = Begin(a, 5, custom); Begin(a, 1, native); Request newest = Begin(a, 5, custom);
        Check(!Finish(stale), "Old A callback accepted after A-B-A"); Check(Finish(newest), "Latest A rejected");
        Request pooled = Begin(a, 5, custom); a.root.SetActive(false); Check(!Finish(pooled) && !a.root.activeSelf, "Pooled actor resurrected"); a.Original(); a.NativeEffects(); a.root.SetActive(true);
        Request shop = Begin(a, 5, custom, 2); Check(!a.root.activeSelf, "Shop was not hidden"); Check(Finish(shop) && a.root.activeSelf, "Legitimate shop callback invalidated");
        Actor future = new Actor(1021, new[] { "OtherGun" }, shared, gradient);
        Check(Finish(Begin(future, 6, Bundle(Xml(1021, 6, new[] { "OtherGun" }, ".1,.2,1"), 1))), "Data-driven future hero/weapon failed");
        Check(Near(future.particle.main.startColor.colorMin.b, .8f), "Future theme hardcoded red");
        Check(Finish(Begin(a, -1, custom)), "Profile-selected weapon path failed");
        Check(Finish(Begin(a, -1, native, 1)), "Robot default path failed"); a.Original();
        foreach (string xml in new[] { "<broken", Xml(1016, 6, names, "1,0,0"), Xml(1021, 5, names, "1,0,0"), Xml(1016, 5, names, "NaN,0,0"), Xml(1016, 5, names, "1,0,0").Replace("version='1'", "version='2'"), Xml(1016, 5, names, "1,0,0").Replace("mesh1", "missing") })
        {
            Check(Finish(Begin(a, 5, Bundle(xml, 3))), "Callback should reach graceful metadata fallback"); a.Original(); a.NativeEffects();
        }
        Request error = Begin(a, 5, custom); error.www.error = "failed download"; Check(!Finish(error), "Failed WWW accepted");
        for (int i = 0; i < 100; i++) { Check(Finish(Begin(a, 5, custom)), "Repeated equip failed"); Check(Finish(Begin(a, 1, native)), "Repeated restore failed"); a.Original(); a.NativeEffects(); }
        Check(!shared.destroyed && Near(shared.GetColor("_TintColor").g, .8f), "Asset destroyed/mutated after many swaps");
        Check(Debug.warnings >= 6, "Invalid config fallback was not exercised");
        Actor shellActor = new Actor(1016, names, shared, gradient);
        Transform shell = Path(shellActor.root, "hologram"); MeshFilter shellFilter = shell.gameObject.AddComponent<MeshFilter>();
        Mesh shellOriginal = new Mesh { name = names[0] }; shellFilter.sharedMesh = shellOriginal;
        Renderer shellRenderer = shell.gameObject.AddComponent<MeshRenderer>(); shellRenderer.sharedMaterials = new[] { shared }; shellRenderer.enabled = false;
        Transform body = Path(shellActor.root, "unlistedBody"); MeshFilter bodyFilter = body.gameObject.AddComponent<MeshFilter>(); Mesh bodyOriginal = new Mesh { name = names[0] }; bodyFilter.sharedMesh = bodyOriginal; body.gameObject.AddComponent<MeshRenderer>();
        string shellXml = Xml(1016, 5, names, "1,.035,.085").Replace("</meshes>", "<companion path='hologram' source='" + names[0] + "'/><companion path='hologram' source='" + names[0] + "'/><companion path='anotherPrefab/shell' source='" + names[0] + "'/></meshes>");
        AssetBundle shellBundle = Bundle(shellXml, 3); Request shellRequest = Begin(shellActor, 5, shellBundle);
        Check(Finish(shellRequest), "Companion callback failed");
        Check(ReferenceEquals(shellFilter.sharedMesh, shellBundle.LoadAsset<Mesh>("mesh0")), "Hologram retained original geometry");
        Check(!shellRenderer.enabled && ReferenceEquals(shellRenderer.sharedMaterials[0], shared), "Visibility shell behavior/material changed");
        Check(ReferenceEquals(bodyFilter.sharedMesh, bodyOriginal), "Unlisted body/mesh was changed by broad search");
        Check(Finish(shellRequest), "Duplicate companion callback failed");
        Check(ReferenceEquals(shellFilter.sharedMesh, shellBundle.LoadAsset<Mesh>("mesh0")), "Duplicate companion mapping failed");
        Check(Finish(Begin(shellActor, 1, native)), "Companion restore failed");
        Check(ReferenceEquals(shellFilter.sharedMesh, shellOriginal), "Companion original geometry not restored");
        for (int i = 0; i < 20; i++)
        {
            Check(Finish(Begin(shellActor, 5, shellBundle)), "Repeated companion equip failed");
            Check(Finish(Begin(shellActor, 1, native)), "Repeated companion restore failed");
            Check(ReferenceEquals(shellFilter.sharedMesh, shellOriginal), "Repeated companion restore retained custom geometry");
        }
        Check(Finish(Begin(shellActor, 5, shellBundle)), "Companion equip before disable failed");shellActor.root.SetActive(false);
        Check(ReferenceEquals(shellFilter.sharedMesh, shellOriginal), "Disabled companion retained custom geometry");shellActor.root.SetActive(true);
        foreach (string xml in new[] { shellXml.Replace("source='" + names[0] + "'", "source='WrongMesh'"), shellXml.Replace("path='hologram'", "path='bodySkill'"), shellXml.Replace("</meshes>", "<companion path='hologram' source='" + names[1] + "'/></meshes>"), shellXml.Replace("asset='mesh2'", "asset='missing'") })
        {
            Check(Finish(Begin(shellActor, 5, Bundle(xml, 3))), "Companion invalid metadata callback failed");shellActor.Original();
            Check(ReferenceEquals(shellFilter.sharedMesh, shellOriginal), "Companion failure left partial geometry");
        }
        File.WriteAllText("scratch/weapon064/helper-validation.json", "{\"assertions\":" + assertions + ",\"status\":\"passed\",\"UnityTested\":false,\"UnityAPISimulated\":true}");
        Console.WriteLine("PASS weapon cosmetics " + assertions + " assertions");
    }
}

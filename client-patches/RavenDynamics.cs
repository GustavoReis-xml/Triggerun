using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// SKIN-040/042/050/055/058/067: opt-in cosmetic chains only; native DynamicBone solver remains
// authoritative. No Unity collider, Rigidbody, networking or global actor cache.
public static class TriggerunRavenDynamics
{
    public static bool BeforeTick(DynamicBone bone, float deltaTime)
    {
        if (bone == null || bone.m_Root == null) return true;
        string rootName = bone.m_Root.name;
        if (!rootName.StartsWith("TRRaven040_", StringComparison.Ordinal) &&
            !rootName.StartsWith("TRSkin042_", StringComparison.Ordinal) &&
            !rootName.StartsWith("TRSkin050_", StringComparison.Ordinal) &&
            !rootName.StartsWith("TRSkin055_", StringComparison.Ordinal) &&
            !rootName.StartsWith("TRSkin058_", StringComparison.Ordinal) &&
            !rootName.StartsWith("TRSkin067_", StringComparison.Ordinal)) return true;
        TriggerunRavenMotionState state = bone.GetComponent<TriggerunRavenMotionState>();
        if (state == null) state = bone.gameObject.AddComponent<TriggerunRavenMotionState>();
        return state.Tick(bone, deltaTime);
    }
}

public sealed class TriggerunRavenMotionState : MonoBehaviour
{
    sealed class Snapshot
    {
        public Transform root;
        public Vector3 position;
        public Quaternion rotation;
        public float time;
    }
    readonly Dictionary<DynamicBone, Snapshot> snapshots = new Dictionary<DynamicBone, Snapshot>();
    readonly List<DynamicBoneCollider> volumes = new List<DynamicBoneCollider>();
    bool volumesReady;
    const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
    static readonly MethodInfo Init = typeof(DynamicBone).GetMethod("InitTransforms", PrivateInstance);
    static readonly MethodInfo Reset = typeof(DynamicBone).GetMethod("ResetParticlesPosition", PrivateInstance);
    static readonly MethodInfo Setup = typeof(DynamicBone).GetMethod("SetupParticles", PrivateInstance);
    static readonly FieldInfo Previous = typeof(DynamicBone).GetField("m_ObjectPrevPosition", PrivateInstance);
    static readonly FieldInfo Move = typeof(DynamicBone).GetField("m_ObjectMove", PrivateInstance);
    static readonly FieldInfo Accumulator = typeof(DynamicBone).GetField("m_Time", PrivateInstance);

    void OnDisable() { snapshots.Clear(); }

    public bool Tick(DynamicBone bone, float dt)
    {
        bool wolf = bone.m_Root.name.StartsWith("TRSkin042_", StringComparison.Ordinal);
        bool lily = bone.m_Root.name.StartsWith("TRSkin050_", StringComparison.Ordinal);
        bool khan = bone.m_Root.name.StartsWith("TRSkin055_", StringComparison.Ordinal);
        bool valk = bone.m_Root.name.StartsWith("TRSkin058_", StringComparison.Ordinal);
        bool carmen = bone.m_Root.name.StartsWith("TRSkin067_", StringComparison.Ordinal);
        if (!volumesReady) BuildVolumes(wolf, lily, khan, valk, carmen);
        Snapshot previous;
        bool known = snapshots.TryGetValue(bone, out previous);
        bool rootChanged = known && previous.root != bone.m_Root;
        bool reset = !known || rootChanged || dt <= 0f || dt > .12f;
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        float now = Time.time;
        if (known)
            reset |= (position - previous.position).sqrMagnitude > 6.25f ||
                Quaternion.Angle(rotation, previous.rotation) > 110f ||
                now - previous.time > .20f || now < previous.time;
        if (!known)
        {
            previous = new Snapshot();
            snapshots.Add(bone, previous);
            // Each chain receives THIS actor's private list, not a scene query.
            bone.m_Colliders = new List<DynamicBoneCollider>(volumes);
        }
        previous.root = bone.m_Root;
        previous.position = position;
        previous.rotation = rotation;
        previous.time = now;
        // Tiny idle sway; native solver uses force per fixed step, not per dt.
        bool wing = bone.m_Root.name.IndexOf("Wing", StringComparison.Ordinal) >= 0;
        bool hair = carmen && bone.m_Root.name.IndexOf("Hair", StringComparison.Ordinal) >= 0;
        float phase = bone.m_Root.name.IndexOf("L_", StringComparison.Ordinal) >= 0 ? 0f : .7f;
        bone.m_Force = transform.right * ((carmen ? (hair ? .00001f : .000025f) : wolf ? .00003f : lily ? .000025f : khan ? .00002f : valk ? .000025f : wing ? .00004f : .00006f) * Mathf.Sin(now * 1.5f + phase));
        if (!reset) return true;
        // Reset only after a discontinuity, avoiding reflection/allocation in
        // the normal solver loop. First tick also clears pre-spawn positions.
        Init.Invoke(bone, null);
        if (rootChanged) Setup.Invoke(bone, null);
        Reset.Invoke(bone, null);
        Previous.SetValue(bone, position);
        Move.SetValue(bone, Vector3.zero);
        Accumulator.SetValue(bone, 0f);
        return false;
    }

    void BuildVolumes(bool wolf, bool lily, bool khan, bool valk, bool carmen)
    {
        volumesReady = true;
        Dictionary<string, Transform> bones = new Dictionary<string, Transform>();
        foreach (Transform t in GetComponentsInChildren<Transform>())
            if (!bones.ContainsKey(t.name)) bones.Add(t.name, t);
        Capsule(bones, "Bip001 Spine1", "Bip001 Neck", wolf ? .14f : (lily || valk || carmen) ? .080f : khan ? .14f : .105f, "Torso");
        Capsule(bones, "Bip001 Pelvis", null, wolf ? .17f : (lily || carmen) ? .100f : valk ? .105f : .13f, "Hips");
        Capsule(bones, "Bip001 L Thigh", "Bip001 L Calf", wolf ? .085f : (lily || valk || carmen) ? .050f : khan ? .070f : .065f, "ThighL");
        Capsule(bones, "Bip001 R Thigh", "Bip001 R Calf", wolf ? .085f : (lily || valk || carmen) ? .050f : khan ? .070f : .065f, "ThighR");
        if (khan) Capsule(bones, "Bip001 L UpperArm", "Bip001 L Forearm", .075f, "TowelShoulder");
        if (carmen)
        {
            Capsule(bones, "Bip001 Neck", "Bip001 Head", .045f, "HairNeck");
            Capsule(bones, "Bip001 Head", null, .060f, "HairHead");
        }
    }

    void Capsule(Dictionary<string, Transform> bones, string startName, string endName, float radius, string label)
    {
        Transform start, end;
        if (!bones.TryGetValue(startName, out start)) return;
        end = start;
        if (endName != null && !bones.TryGetValue(endName, out end)) return;
        GameObject volume = new GameObject("TRRaven040_Volume_" + label);
        volume.transform.parent = start;
        Vector3 a = start.position;
        Vector3 b = end.position;
        float scale = Mathf.Abs(transform.lossyScale.x);
        if (scale < .0001f) scale = 1f;
        volume.transform.position = (a + b) * .5f;
        volume.transform.rotation = (b - a).sqrMagnitude > .000001f ?
            Quaternion.FromToRotation(Vector3.up, b - a) : start.rotation;
        volume.transform.localScale = Vector3.one;
        DynamicBoneCollider collider = volume.AddComponent<DynamicBoneCollider>();
        collider.m_Center = Vector3.zero;
        collider.m_Direction = DynamicBoneCollider.Direction.Y;
        collider.m_Bound = DynamicBoneCollider.Bound.Outside;
        collider.m_Radius = radius;
        collider.m_Height = (b - a).magnitude / scale + radius * 2f;
        volumes.Add(collider);
    }
}

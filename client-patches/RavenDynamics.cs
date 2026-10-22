using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// SKIN-040: opt-in cosmetic chains only; the native DynamicBone solver remains
// authoritative. No Unity collider, Rigidbody, networking or global actor cache.
public static class TriggerunRavenDynamics
{
    public static bool BeforeTick(DynamicBone bone, float deltaTime)
    {
        if (bone == null || bone.m_Root == null ||
            !bone.m_Root.name.StartsWith("TRRaven040_", StringComparison.Ordinal)) return true;
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
        if (!volumesReady) BuildVolumes();
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
        float phase = bone.m_Root.name.IndexOf("L_", StringComparison.Ordinal) >= 0 ? 0f : .7f;
        bone.m_Force = transform.right * ((wing ? .00004f : .00006f) * Mathf.Sin(now * 1.5f + phase));
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

    void BuildVolumes()
    {
        volumesReady = true;
        Dictionary<string, Transform> bones = new Dictionary<string, Transform>();
        foreach (Transform t in GetComponentsInChildren<Transform>())
            if (!bones.ContainsKey(t.name)) bones.Add(t.name, t);
        Capsule(bones, "Bip001 Spine1", "Bip001 Neck", .105f, "Torso");
        Capsule(bones, "Bip001 Pelvis", null, .13f, "Hips");
        Capsule(bones, "Bip001 L Thigh", "Bip001 L Calf", .065f, "ThighL");
        Capsule(bones, "Bip001 R Thigh", "Bip001 R Calf", .065f, "ThighR");
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

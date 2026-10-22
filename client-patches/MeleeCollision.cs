using System.Collections.Generic;
using UnityEngine;

// Filters query-only movement capsules. Does not resize or move any collider.
public static class TriggerunMeleeCollision
{
    public static RaycastHit[] MeleeBlockers(Vector3 origin, Vector3 direction, float distance, int mask)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, mask);
        List<RaycastHit> eligible = new List<RaycastHit>(hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider collider = hits[i].collider;
            if (collider == null || collider.name.Contains("_IgnoreDamge")) continue;
            eligible.Add(hits[i]);
        }
        return eligible.ToArray();
    }

    // A blade belongs to its owner's root, but touching a hazard with it is not body contact.
    public static bool MayEnterSlay(Slay zone, Collider contact)
    {
        if (contact == null) return false;
        bool weapon = contact.GetComponentInParent<CMuzzlePhysicalTrigger>() != null;
        PublicInfo owner = contact.transform.root.GetComponent<PublicInfo>();
        if (owner != null && owner.NetObject != null)
            Debug.Log("[TRIG Slay] zone=" + zone.name + " contact=" + contact.name +
                " layer=" + contact.gameObject.layer + " position=" + contact.transform.position +
                " weapon=" + weapon);
        return !weapon;
    }
}
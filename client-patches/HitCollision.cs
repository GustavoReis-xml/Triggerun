using UnityEngine;

// Query filtering only. Never changes collider size, physical layers or rigidbodies.
public static class TriggerunHitCollision
{
    private static RaycastHit[] buffer = new RaycastHit[32];

    public static bool Raycast(Ray ray, out RaycastHit hit, float distance, int mask)
    {
        return Closest(ray, out hit, distance, mask, false);
    }

    // CameraControl already excludes shields from its aim selection.
    public static int AimRaycast(Ray ray, RaycastHit[] results, float distance, int mask)
    {
        RaycastHit hit;
        if (results == null || results.Length == 0 || !Closest(ray, out hit, distance, mask, true))
            return 0;
        results[0] = hit;
        return 1;
    }

    private static bool Closest(Ray ray, out RaycastHit hit, float distance, int mask, bool aiming)
    {
        hit = new RaycastHit();
        if (distance <= 0f) return false;
        int count;
        RaycastHit[] hits = buffer;
        // A full NonAlloc buffer may have omitted the closest eligible collider.
        while (true)
        {
            count = Physics.RaycastNonAlloc(ray, hits, distance, mask);
            if (count < hits.Length) break;
            if (hits.Length >= 1024)
            {
                hits = Physics.RaycastAll(ray, distance, mask);
                count = hits.Length;
                break;
            }
            buffer = new RaycastHit[hits.Length * 2];
            hits = buffer;
        }
        float nearest = float.PositiveInfinity;
        bool found = false;
        for (int i = 0; i < count; i++)
        {
            Collider collider = hits[i].collider;
            if (collider == null || collider.name.Contains("_IgnoreDamge")) continue;
            if (aiming && collider.tag.Contains("can_damage_dun")) continue;
            if (hits[i].distance < nearest)
            {
                nearest = hits[i].distance;
                hit = hits[i];
                found = true;
            }
        }
        return found;
    }
}

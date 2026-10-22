using System.Collections.Generic;
using UnityEngine;

public static class TriggerunLamsterArea
{
    public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float distance, int mask, CBulletMe bullet)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, mask);
        if (bullet == null || bullet.muzzle == null || bullet.muzzle.launcher == null) return hits;
        CLauncher launcher = bullet.muzzle.launcher;
        GamePlayer actor = launcher.NetObject as GamePlayer;
        PlayerCommon player = actor == null ? null : actor.playerCommon;
        if (player == null || player.m_nModelIndex != 2000 || launcher.launcherName != 2) return hits;
        List<RaycastHit> blockers = new List<RaycastHit>();
        foreach (RaycastHit hit in hits)
            if (hit.collider != null && !hit.collider.name.Contains("_IgnoreDamge")) blockers.Add(hit);
        return blockers.ToArray();
    }
}


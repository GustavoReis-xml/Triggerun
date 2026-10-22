using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TriggerunCombatHud
{
    private class Hit { public int place, attacker; public float time; }
    private static readonly Dictionary<NetLiving,Hit> hits = new Dictionary<NetLiving,Hit>();
    private static readonly Dictionary<NetLiving,float> notices = new Dictionary<NetLiving,float>();
    public static void ObserveDamage(NetLiving target, ArrayList damage, ArrayList hp)
    {
        if (target == null || damage == null || damage.Count < 6 || hp == null || hp.Count == 0) return;
        if (hits.Count > 512) hits.Clear();
        hits[target] = new Hit { place=Convert.ToInt32(damage[5]), attacker=Convert.ToInt32(damage[3]), time=Time.unscaledTime };        // Training/Adventure bots receive HP zero and a death animation instead of OnDeath.
        if (target is RobotAI && Convert.ToInt32(hp[0]) <= 0) {
            float previous;
            bool repeated=notices.TryGetValue(target,out previous) && Time.unscaledTime-previous < 1f;
            int attacker=Convert.ToInt32(damage[3]);
            ConfirmDeath(target,attacker);
            HeroPlayer player=fGameManager.Heroplayer;
            if (!repeated && notices.ContainsKey(target) && player!=null && player.wc!=null && damage.Count>14)
                player.wc.DeadSound(Convert.ToInt32(damage[13]),Convert.ToInt32(damage[14]));
        }
    }
    public static void ConfirmDeath(NetLiving victim, int attacker)
    {
        try {
            HeroPlayer player=fGameManager.Heroplayer;
            if (player == null || victim == null || victim.isCopy || victim == player || attacker != player.getNetid() || victim.nForce == player.nForce) return;
            float last;
            if (notices.TryGetValue(victim,out last) && Time.unscaledTime-last < 1f) return;
            if (notices.Count > 512) notices.Clear();
            notices[victim]=Time.unscaledTime;
            Hit hit; int place=0;
            if (hits.TryGetValue(victim,out hit) && hit.attacker==attacker && Time.unscaledTime-hit.time < 1f) place=hit.place;
            hits.Remove(victim);
            if (player.playerZhunxingMe != null) player.playerZhunxingMe.lastKill(place);
            CPlayerSoundMe sound=player.playerSound as CPlayerSoundMe;
            if (sound != null) sound.PlayJiZhongSound();
        } catch (Exception ex) { Debug.LogWarning("[CombatHud] " + ex.Message); }
    }
    public static void InitCrosshair(Cross_Hair_Handle cross, int width, int length)
    {
        TriggerunCrosshair component=cross.GetComponent<TriggerunCrosshair>();
        if (component==null) component=cross.gameObject.AddComponent<TriggerunCrosshair>();
        component.Initialize(cross,width,length);
        TriggerunHudSettings.Ensure();
    }
}


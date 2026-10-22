using System;
using UnityEngine;

// Negative NVM IDs name robot.xml definitions; positive IDs retain legacy hero skins.
public static class TriggerunModelSwap
{
    public static string[] Apply(string[] original, PlayerCommon player)
    {
        if (player == null || player.m_nVisualModelIndex >= 0 || original == null || original.Length < 4)
            return original;
        try
        {
            int id = checked(-player.m_nVisualModelIndex);
            if (!fGameManager.mloadConfig.AllRobotCommonData.ContainsKey(id)) return original;
            RobotSkinUnlock skin = HeroDataManager.GetInstance().GetNpcSkinData(id);
            if (skin == null || String.IsNullOrEmpty(skin.tpsModel) || String.IsNullOrEmpty(skin.tpsControl)) return original;
            string[] result = (string[])original.Clone();
            result[0] = skin.tpsModel;
            result[1] = skin.tpsControl;
            // Keep the original first-person arms and animator, hence weapon input/aim.
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogWarning("[ModelSwap] Original model retained: " + ex.Message);
            return original;
        }
    }

    public static Type AnimationType(string original, GamePlayer player)
    {
        if (player != null && player.playerCommon != null && player.playerCommon.m_nVisualModelIndex < 0)
        {
            try
            {
                int id = checked(-player.playerCommon.m_nVisualModelIndex);
                int hero = fGameManager.mloadConfig.AllRobotCommonData[id].heroId;
                string name = "TBCharAnimation" + fGameManager.mloadConfig.AllHeroList[hero].heroclass + "Tps";
                Type type = typeof(GamePlayer).Assembly.GetType(name);
                if (type != null && typeof(TBCharAnimationThirdIK).IsAssignableFrom(type)) return type;
            }
            catch (Exception ex) { Debug.LogWarning("[ModelSwap] Animation fallback: " + ex.Message); }
        }
        return typeof(GamePlayer).Assembly.GetType(original);
    }
}

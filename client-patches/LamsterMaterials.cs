using System.Collections.Generic;

// Unarmed Lamster has native materials embedded in its models.
public static class TriggerunMaterialFilter
{
    public static List<string> ForPlayer(List<string> materials, PlayerCommon player)
    {
        if (materials == null || player == null || player.m_nModelIndex != 2000) return materials;
        for (int i=materials.Count-1;i>=0;i--)
            if (materials[i] == null || materials[i].Trim().Length == 0 || materials[i].Trim() == "0") materials.RemoveAt(i);
        return materials;
    }
}

// Lamster's native branch hardcodes male_tps; allow its equipped cosmetic only.
public static class TriggerunLamsterSkinModel
{
    public static string[] ForPlayer(string[] original, PlayerCommon player)
    {
        if(player==null || player.m_nModelIndex!=2000 || (player.m_nVisualModelIndex>0 && player.m_nVisualModelIndex!=2000))return original;
        try{
            SkinUnlock skin=HeroDataManager.GetInstance().GetUseSkinData(2000,player.m_nCid);
            if(skin==null || string.IsNullOrEmpty(skin.tpsModel) || string.IsNullOrEmpty(skin.tpsControl) || string.IsNullOrEmpty(skin.fpsModel) || string.IsNullOrEmpty(skin.fpsControl))return original;
            return new string[]{skin.tpsModel,skin.tpsControl,skin.fpsModel,skin.fpsControl};
        }catch(System.Exception){return original;}
    }
}

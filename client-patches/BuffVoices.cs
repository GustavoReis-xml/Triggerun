using System;
using UnityEngine;

public static class TriggerunBuffVoices
{
    public static bool TryLoad(CPlayerSound sound, string name, int buffId)
    {
        if(buffId!=1007)return false;
        if(sound==null || name==null)return true;
        name=name.ToLowerInvariant();
        switch(name){
            case "buff_1007_jipusai":case "buff_1007_jvjishou":case "buff_1007_leizhenzi":case "buff_1007_terminator":case "buff_1007_xingjilieren": name+="_bx";break;
            case "buff_1007_valkyries":break;
            default:return true; // This hero has no original recording for this buff.
        }
        string url="SoundRes/"+name+".sds";
        WWW cached=GameRoot.GetResWWW(url);
        if(cached!=null)sound.LoadBuffSoundPrefeb(cached,buffId);
        else GameRoot.LoadAsset(url,new fResourceManager.DownFinishDelegate(sound.LoadBuffSoundPrefeb),buffId);
        return true;
    }
}

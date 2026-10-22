using System;
namespace UnityEngine { public class WWW {} }
public class CPlayerSound { public int id,calls; public void LoadBuffSoundPrefeb(UnityEngine.WWW www,object style){id=(int)style;calls++;} }
public class fResourceManager { public delegate void DownFinishDelegate(UnityEngine.WWW www,object style); }
public static class GameRoot {
 public static bool cached;public static int loads;public static string url;
 public static UnityEngine.WWW GetResWWW(string name){url=name;return cached?new UnityEngine.WWW():null;}
 public static void LoadAsset(string name,fResourceManager.DownFinishDelegate cb,object style){url=name;loads++;cb(new UnityEngine.WWW(),style);}
}
class Test {
 static void Check(bool v){if(!v)throw new Exception("buff voice failure");}
 static void Main(){
  var s=new CPlayerSound();Check(TriggerunBuffVoices.TryLoad(s,"buff_1007_Jvjishou",1007));Check(GameRoot.url=="SoundRes/buff_1007_jvjishou_bx.sds"&&s.id==1007&&s.calls==1);
  GameRoot.cached=true;TriggerunBuffVoices.TryLoad(s,"buff_1007_Terminator",1007);Check(s.id==1007&&s.calls==2&&GameRoot.loads==1);
  TriggerunBuffVoices.TryLoad(s,"buff_1007_LuoMo",1007);Check(s.calls==2&&GameRoot.loads==1);
  Check(!TriggerunBuffVoices.TryLoad(s,"other",1008));
  TriggerunBuffVoices.TryLoad(s,"buff_1007_Valkyries",1007);Check(GameRoot.url=="SoundRes/buff_1007_valkyries.sds");
  Console.WriteLine("6 buff voice checks passed (simulated client)");
 }
}

using System;
using System.Collections.Generic;
namespace UnityEngine { public static class Debug { public static void LogWarning(object value) {} } }
public class PlayerCommon { public int m_nVisualModelIndex; }
public class GamePlayer { public PlayerCommon playerCommon = new PlayerCommon(); }
public class TBCharAnimationThirdIK {}
public class TBCharAnimationTestTps : TBCharAnimationThirdIK {}
public class RobotSkinUnlock { public string tpsModel,tpsControl; }
public class Robot { public int heroId; }
public class Hero { public string heroclass; }
public class Config { public Dictionary<int,Robot> AllRobotCommonData=new Dictionary<int,Robot>(); public Dictionary<int,Hero> AllHeroList=new Dictionary<int,Hero>(); }
public static class fGameManager { public static Config mloadConfig=new Config(); }
public class HeroDataManager {
 public static RobotSkinUnlock skin;
 public static HeroDataManager GetInstance(){return new HeroDataManager();}
 public RobotSkinUnlock GetNpcSkinData(int id){return skin;}
}
class Test {
 static void Check(bool ok){if(!ok)throw new Exception("Failed model-swap assertion");}
 static void Main(){
 var p=new GamePlayer();var original=new[]{"base_tps","base_anim","base_fps","base_fps_anim"};
 Check(Object.ReferenceEquals(original,TriggerunModelSwap.Apply(original,p.playerCommon)));
 p.playerCommon.m_nVisualModelIndex=-1109;
 Check(Object.ReferenceEquals(original,TriggerunModelSwap.Apply(original,p.playerCommon)));
 fGameManager.mloadConfig.AllRobotCommonData[1109]=new Robot{heroId=2003};
 fGameManager.mloadConfig.AllHeroList[2003]=new Hero{heroclass="Test"};
 HeroDataManager.skin=new RobotSkinUnlock{tpsModel="monster",tpsControl="monster_anim"};
 var swapped=TriggerunModelSwap.Apply(original,p.playerCommon);
 Check(swapped[0]=="monster" && swapped[1]=="monster_anim" && swapped[2]==original[2] && swapped[3]==original[3] && original[0]=="base_tps");
 Check(TriggerunModelSwap.AnimationType("TBCharAnimationThirdIK",p)==typeof(TBCharAnimationTestTps));
 p.playerCommon.m_nVisualModelIndex=0;
 Check(Object.ReferenceEquals(original,TriggerunModelSwap.Apply(original,p.playerCommon)));
 Check(TriggerunModelSwap.AnimationType("TBCharAnimationThirdIK",p)==typeof(TBCharAnimationThirdIK));
 p.playerCommon.m_nVisualModelIndex=Int32.MinValue;
 Check(Object.ReferenceEquals(original,TriggerunModelSwap.Apply(original,p.playerCommon)));
 Console.WriteLine("7 model swap checks passed (simulated client)");
 }
}

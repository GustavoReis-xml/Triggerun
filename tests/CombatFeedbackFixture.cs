using System;
using System.Collections;
namespace UnityEngine {
 public static class Time {public static float unscaledTime;}
 public static class Debug {public static void LogWarning(object message){throw new Exception(message.ToString());}}
 public class GameObject {public T AddComponent<T>() where T:new(){return new T();}}
}
public class Cross_Hair_Handle {public UnityEngine.GameObject gameObject=new UnityEngine.GameObject();public T GetComponent<T>() where T:class{return null;}}
public class TriggerunCrosshair {public void Initialize(Cross_Hair_Handle c,int w,int l){}}
public static class TriggerunHudSettings {public static void Ensure(){}}
public class zhunxingBase {public int count,place;public void lastKill(int p){count++;place=p;}}
public class CPlayerSoundMe {public int count;public void PlayJiZhongSound(){count++;}}
public class Weapon {public int count,weapon,launcher;public void DeadSound(int w,int l){count++;weapon=w;launcher=l;}}
public class NetLiving {public bool isCopy;public int id,nForce;public int getNetid(){return id;}}
public class RobotAI:NetLiving {}
public class HeroPlayer:NetLiving {public zhunxingBase playerZhunxingMe=new zhunxingBase();public CPlayerSoundMe playerSound=new CPlayerSoundMe();public Weapon wc=new Weapon();}
public static class fGameManager {public static HeroPlayer Heroplayer;}
class Test {
 static void Check(bool value){if(!value)throw new Exception("Feedback assertion failed");}
 static ArrayList Damage(int id,int place){var d=new ArrayList();for(int i=0;i<16;i++)d.Add(0);d[3]=id;d[5]=place;d[13]=1;d[14]=1;return d;}
 static void Main(){
  var p=new HeroPlayer{id=10,nForce=1};fGameManager.Heroplayer=p;var v=new NetLiving{id=11,nForce=2};
  TriggerunCombatHud.ObserveDamage(v,Damage(10,1),new ArrayList{0});Check(p.playerZhunxingMe.count==0);
  TriggerunCombatHud.ConfirmDeath(v,10);Check(p.playerZhunxingMe.count==1&&p.playerZhunxingMe.place==1&&p.playerSound.count==1);
  TriggerunCombatHud.ConfirmDeath(v,10);Check(p.playerZhunxingMe.count==1);
  var ally=new NetLiving{id=12,nForce=1};TriggerunCombatHud.ConfirmDeath(ally,10);TriggerunCombatHud.ConfirmDeath(v,99);Check(p.playerZhunxingMe.count==1);
  UnityEngine.Time.unscaledTime=5;TriggerunCombatHud.ConfirmDeath(v,10);Check(p.playerZhunxingMe.count==2&&p.playerZhunxingMe.place==0);
  var bot=new RobotAI{id=13,nForce=2};TriggerunCombatHud.ObserveDamage(bot,Damage(10,1),new ArrayList{0});Check(p.playerZhunxingMe.count==3&&p.wc.count==1&&p.wc.weapon==1&&p.wc.launcher==1);
  TriggerunCombatHud.ObserveDamage(bot,Damage(10,1),new ArrayList{0});Check(p.playerZhunxingMe.count==3&&p.wc.count==1);
  Console.WriteLine("7 feedback checks passed (simulated client)");
 }
}

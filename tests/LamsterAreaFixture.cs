namespace UnityEngine {
 public struct Vector3 { public float x; }
 public class Collider { public string name; }
 public struct RaycastHit { public Collider collider; }
 public static class Physics { public static RaycastHit[] Hits; public static int Mask; public static RaycastHit[] RaycastAll(Vector3 a,Vector3 b,float d,int mask){Mask=mask;return Hits;} }
}
public class NetLiving {}
public class PlayerCommon { public int m_nModelIndex; }
public class GamePlayer : NetLiving { public PlayerCommon playerCommon; }
public class CLauncher { public NetLiving NetObject; public int launcherName; }
public class CMuzzle { public CLauncher launcher; }
public class CBulletMe { public CMuzzle muzzle; }
public static class LamsterAreaTests {
 static UnityEngine.RaycastHit Hit(string name){return new UnityEngine.RaycastHit{collider=new UnityEngine.Collider{name=name}};}
 public static void Run(){
  var input=new[]{Hit("control_IgnoreDamge"),Hit("defaultMesh_IgnoreDamge"),Hit("wall"),Hit("can_damage_dun"),Hit("Bip001 Head")};
  UnityEngine.Physics.Hits=input;
  var actor=new GamePlayer{playerCommon=new PlayerCommon{m_nModelIndex=2000}};
  var launcher=new CLauncher{NetObject=actor,launcherName=2};var bullet=new CBulletMe{muzzle=new CMuzzle{launcher=launcher}};
  var result=TriggerunLamsterArea.RaycastAll(new UnityEngine.Vector3(),new UnityEngine.Vector3(),8,123,bullet);
  Check(result.Length==3 && result[0].collider.name=="wall" && result[1].collider.name=="can_damage_dun" && result[2].collider.name=="Bip001 Head","auxiliary capsules removed; walls shields and hitboxes retained");
  Check(UnityEngine.Physics.Mask==123,"native collision mask preserved");
  launcher.launcherName=1;Check(object.ReferenceEquals(input,TriggerunLamsterArea.RaycastAll(new UnityEngine.Vector3(),new UnityEngine.Vector3(),8,123,bullet)),"primary unchanged");
  launcher.launcherName=2;actor.playerCommon.m_nModelIndex=1017;Check(object.ReferenceEquals(input,TriggerunLamsterArea.RaycastAll(new UnityEngine.Vector3(),new UnityEngine.Vector3(),8,123,bullet)),"other heroes unchanged");
  Check(object.ReferenceEquals(input,TriggerunLamsterArea.RaycastAll(new UnityEngine.Vector3(),new UnityEngine.Vector3(),8,123,null)),"missing owner preserves native query");
 }
 static void Check(bool ok,string msg){if(!ok)throw new System.Exception(msg);System.Console.WriteLine("PASS "+msg);}
}

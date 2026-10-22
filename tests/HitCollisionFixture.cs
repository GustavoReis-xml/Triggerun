namespace UnityEngine {
 public struct Ray { public float origin; }
 public class Collider { public string name; public string tag="can_damage_human"; }
 public struct RaycastHit { public Collider collider; public float distance; }
 public static class Physics {
  public static RaycastHit[] Scene=new RaycastHit[0];
  public static float LastOrigin; public static int LastMask; public static int AllCalls;
  public static int RaycastNonAlloc(Ray ray,RaycastHit[] buffer,float distance,int mask){
   LastOrigin=ray.origin;LastMask=mask;int n=0;
   for(int i=0;i<Scene.Length && n<buffer.Length;i++)if(Scene[i].distance<=distance)buffer[n++]=Scene[i];
   return n;
  }
  public static RaycastHit[] RaycastAll(Ray ray,float distance,int mask){AllCalls++;return Scene;}
 }
}
public static class HitCollisionTests {
 static UnityEngine.RaycastHit H(string n,float d,string tag="can_damage_human") {return new UnityEngine.RaycastHit{collider=new UnityEngine.Collider{name=n,tag=tag},distance=d};}
 static void Check(bool condition,string name){if(!condition)throw new System.Exception(name);System.Console.WriteLine("PASS "+name);}
 public static void Run(){
  var ray=new UnityEngine.Ray{origin=13};UnityEngine.RaycastHit hit;
  UnityEngine.Physics.Scene=new[]{H("control_IgnoreDamge",1f),H("Bip001 Head",1.04f),H("wall",4f)};
  Check(TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider.name=="Bip001 Head","head 4cm behind ignored capsule remains eligible");
  Check(UnityEngine.Physics.LastOrigin==13&&UnityEngine.Physics.LastMask==7,"query keeps origin and layer mask");
  UnityEngine.Physics.Scene=new[]{H("Bip001 Head",2),H("wall",1),H("control_IgnoreDamge",.5f)};
  Check(TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider.name=="wall","near wall blocks head despite unsorted hits");
  UnityEngine.Physics.Scene=new[]{H("shield",1,"can_damage_dun"),H("Bip001 Head",2)};
  Check(TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider.name=="shield","bullets preserve shields");
  var aim=new UnityEngine.RaycastHit[5];
  Check(TriggerunHitCollision.AimRaycast(ray,aim,5,7)==1&&aim[0].collider.name=="Bip001 Head","camera preserves shield exclusion");
  UnityEngine.Physics.Scene=new[]{H("control_IgnoreDamge",1)};
  Check(!TriggerunHitCollision.Raycast(ray,out hit,5,7),"ignored-only query does not invent damage");
  UnityEngine.Physics.Scene=new UnityEngine.RaycastHit[81];
  for(int i=0;i<80;i++)UnityEngine.Physics.Scene[i]=H("control_IgnoreDamge",1);
  UnityEngine.Physics.Scene[80]=H("Bip001 Head",1.04f);
  Check(TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider.name=="Bip001 Head","full buffer grows without dropping hit");
  UnityEngine.Physics.Scene=new UnityEngine.RaycastHit[1025];
  for(int i=0;i<1024;i++)UnityEngine.Physics.Scene[i]=H("control_IgnoreDamge",1);
  UnityEngine.Physics.Scene[1024]=H("Bip001 Head",1.04f);
  Check(TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider.name=="Bip001 Head"&&UnityEngine.Physics.AllCalls==1,"saturated maximum buffer uses full query");
  UnityEngine.Physics.Scene=new UnityEngine.RaycastHit[0];
  Check(!TriggerunHitCollision.Raycast(ray,out hit,5,7)&&hit.collider==null,"empty query clears previous hit");
 }
}

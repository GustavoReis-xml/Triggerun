namespace UnityEngine {
 public struct Vector3 {public float x; public override string ToString(){return x.ToString();}}
 public class GameObject {public int layer;}
 public class Transform {public Transform root;public Vector3 position;public PublicInfo owner;public T GetComponent<T>()where T:class{return owner as T;}}
 public class Collider {public string name;public string tag;public GameObject gameObject=new GameObject();public Transform transform=new Transform();public CMuzzlePhysicalTrigger blade;public T GetComponentInParent<T>()where T:class{return blade as T;}}
 public struct RaycastHit {public Collider collider;}
 public static class Physics {public static RaycastHit[] Scene;public static int LastMask;public static float LastDistance;public static RaycastHit[] RaycastAll(Vector3 o,Vector3 d,float dist,int mask){LastMask=mask;LastDistance=dist;return Scene;}}
 public static class Debug {public static void Log(string s){}}
}
public class CMuzzlePhysicalTrigger {}
public class PublicInfo {public object NetObject;}
public class Slay {public string name="hazard";}
public static class MeleeCollisionTests {
 static UnityEngine.Collider C(string name,bool weapon=false,bool owner=true){var c=new UnityEngine.Collider{name=name,blade=weapon?new CMuzzlePhysicalTrigger():null};c.transform.root=c.transform;c.transform.owner=owner?new PublicInfo{NetObject=new object()}:null;return c;}
 static void Check(bool ok,string name){if(!ok)throw new System.Exception(name);System.Console.WriteLine("PASS "+name);}
 public static void Run(){
  var body=C("control_IgnoreDamge");var head=C("head");var wall=C("concrete");var shield=C("shield");var auxiliary=C("defaultMesh_IgnoreDamge");UnityEngine.Physics.Scene=new[]{new UnityEngine.RaycastHit{collider=body},new UnityEngine.RaycastHit{collider=head},new UnityEngine.RaycastHit{collider=wall},new UnityEngine.RaycastHit{collider=shield},new UnityEngine.RaycastHit{collider=auxiliary},new UnityEngine.RaycastHit()};
  var hits=TriggerunMeleeCollision.MeleeBlockers(new UnityEngine.Vector3(),new UnityEngine.Vector3(),2.6f,37);
  Check(hits.Length==3,"auxiliary capsules and null hits removed");Check(hits[0].collider==head,"real target preserved");Check(hits[1].collider==wall,"wall still blocks melee");Check(hits[2].collider==shield,"shield preserved");Check(UnityEngine.Physics.LastMask==37&&UnityEngine.Physics.LastDistance==2.6f,"query range and mask unchanged");
  var zone=new Slay();Check(!TriggerunMeleeCollision.MayEnterSlay(zone,C("sword",true)),"weapon contact cannot kill owner");Check(TriggerunMeleeCollision.MayEnterSlay(zone,body),"movement capsule still triggers legitimate death");Check(TriggerunMeleeCollision.MayEnterSlay(zone,head),"body damage collider still triggers hazard");Check(TriggerunMeleeCollision.MayEnterSlay(zone,C("npc",false,false)),"NPC handling preserved");Check(!TriggerunMeleeCollision.MayEnterSlay(zone,null),"null ignored safely");
  UnityEngine.Physics.Scene=new UnityEngine.RaycastHit[0];Check(TriggerunMeleeCollision.MeleeBlockers(new UnityEngine.Vector3(),new UnityEngine.Vector3(),1,1).Length==0,"empty query stays empty");
 }
}
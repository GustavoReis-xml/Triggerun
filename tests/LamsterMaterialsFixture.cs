using System;
using System.Collections.Generic;
public class PlayerCommon { public int m_nModelIndex; }
class MaterialFixture {
 static void Check(bool v){if(!v)throw new Exception("material check failed");}
 static void Main(){
  var lamster=new PlayerCommon{m_nModelIndex=2000};
  var fps=new List<string>{"",""};Check(Object.ReferenceEquals(fps,TriggerunMaterialFilter.ForPlayer(fps,lamster))&&fps.Count==0);
  var tps=new List<string>{""};Check(TriggerunMaterialFilter.ForPlayer(tps,lamster).Count==0);
  var mixed=new List<string>{null," ","0","existing_material"};Check(TriggerunMaterialFilter.ForPlayer(mixed,lamster).Count==1&&mixed[0]=="existing_material");
  var other=new List<string>{"","original"};Check(TriggerunMaterialFilter.ForPlayer(other,new PlayerCommon{m_nModelIndex=1015}).Count==2);
  Check(TriggerunMaterialFilter.ForPlayer(null,lamster)==null);
  Check(TriggerunMaterialFilter.ForPlayer(other,null)==other);
  Console.WriteLine("6 material checks passed (simulated client)");
 }
}

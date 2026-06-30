using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class CTestNormal : MonoBehaviour
{
	// Token: 0x060007BD RID: 1981 RVA: 0x000438BC File Offset: 0x00041ABC
	private void Update()
	{
		Vector3 position = base.transform.position;
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(position, base.transform.forward, out raycastHit);
		if (flag)
		{
			float d = Vector3.Distance(raycastHit.point, base.transform.position);
			Debug.DrawRay(position, base.transform.forward * d, Color.blue);
			Debug.DrawRay(this.flower.position + this.flower.forward * 0.05f, this.flower.forward, Color.blue);
			Debug.DrawRay(this.flower.position + this.flower.forward * 0.05f, this.flower.right, Color.red);
			Debug.DrawRay(this.flower.position + this.flower.forward * 0.05f, this.flower.up, Color.green);
			Vector3 forward = base.transform.forward;
			Vector3 vector = Vector3.Cross(forward, this.phyDir.forward);
			this.phyDirChuizhi.transform.forward = vector;
			Vector3 forward2 = Vector3.Cross(raycastHit.normal, vector);
			this.touying.transform.position = raycastHit.point;
			this.touying.transform.forward = forward2;
			Quaternion lhs = Quaternion.LookRotation(forward2, raycastHit.normal);
			Quaternion rhs = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
			Quaternion rhs2 = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
			this.flower.transform.rotation = lhs * rhs * rhs2;
			this.flower.transform.position = raycastHit.point;
			this.pic2.eulerAngles = lhs.eulerAngles;
			this.pic2.transform.position = raycastHit.point;
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00043AE0 File Offset: 0x00041CE0
	private void OnGUI()
	{
		GUI.color = Color.black;
		Vector3 vector = Camera.main.WorldToScreenPoint(this.flower.transform.position);
		GUI.Label(new Rect(vector.x, vector.y, 200f, 50f), "武器砍出来的贴花(最终效果)");
		vector = Camera.main.WorldToScreenPoint(this.phyDir.transform.position);
		GUI.Label(new Rect(vector.x, vector.y, 200f, 50f), "武器砍出来物理方向");
		vector = Camera.main.WorldToScreenPoint(this.phyDirChuizhi.transform.position);
		GUI.Label(new Rect(vector.x, vector.y, 200f, 50f), "垂直于物理方向的方向,用来计算投影向量");
		vector = Camera.main.WorldToScreenPoint(this.touying.transform.position + new Vector3(0f, 0.2f, 0f));
		GUI.Label(new Rect(vector.x, vector.y, 200f, 50f), "灰色长棍子是投影于平面的向量");
	}

	// Token: 0x040005BF RID: 1471
	public Transform flower;

	// Token: 0x040005C0 RID: 1472
	public Transform pic2;

	// Token: 0x040005C1 RID: 1473
	public Transform phyDir;

	// Token: 0x040005C2 RID: 1474
	public Transform phyDirChuizhi;

	// Token: 0x040005C3 RID: 1475
	public Transform touying;
}

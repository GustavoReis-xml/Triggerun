using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class CTestTietu : MonoBehaviour
{
	// Token: 0x060007D6 RID: 2006 RVA: 0x00044453 File Offset: 0x00042653
	private void Start()
	{
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00044458 File Offset: 0x00042658
	private void Update()
	{
		Vector3 forward = Camera.main.transform.forward;
		Ray ray = new Ray(Camera.main.transform.position, forward);
		RaycastHit raycastHit;
		Physics.Raycast(ray, out raycastHit, 100f, 1);
		bool flag = !(raycastHit.collider == null);
		if (flag)
		{
			this.tietu.transform.position = raycastHit.point;
			Vector3 forward2 = Camera.main.transform.forward;
			Vector3 forward3 = Vector3.Cross(raycastHit.normal, -Camera.main.transform.right);
			Quaternion rotation = Quaternion.LookRotation(forward3, raycastHit.normal);
			this.v1.transform.forward = forward3;
			this.tietu.transform.rotation = rotation;
		}
	}

	// Token: 0x040005CC RID: 1484
	public Transform qiangmian;

	// Token: 0x040005CD RID: 1485
	public Transform tietu;

	// Token: 0x040005CE RID: 1486
	public Transform v1;
}

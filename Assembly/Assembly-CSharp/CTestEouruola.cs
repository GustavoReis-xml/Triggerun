using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class CTestEouruola : MonoBehaviour
{
	// Token: 0x060007B4 RID: 1972 RVA: 0x00043701 File Offset: 0x00041901
	private void Start()
	{
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00043704 File Offset: 0x00041904
	private void Update()
	{
		Vector3 normalized = (this.ob.transform.position - base.transform.position).normalized;
		Debug.DrawRay(base.transform.position, normalized * 1001f, Color.red);
		Quaternion rotation = Quaternion.Euler(0f, 30f, 0f);
		Quaternion rotation2 = Quaternion.Euler(0f, -30f, 0f);
		Vector3 end = base.transform.position + rotation * normalized * 1001f;
		Vector3 end2 = base.transform.position + rotation2 * normalized * 1001f;
		Debug.DrawLine(base.transform.position, end);
		Debug.DrawLine(base.transform.position, end2);
	}

	// Token: 0x040005BD RID: 1469
	public GameObject ob;
}

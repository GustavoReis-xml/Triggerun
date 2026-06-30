using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class CTestDot : MonoBehaviour
{
	// Token: 0x060007AD RID: 1965 RVA: 0x00043580 File Offset: 0x00041780
	private void Start()
	{
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00043584 File Offset: 0x00041784
	private void Update()
	{
		Vector3 forward = this.me.transform.forward;
		Vector3 normalized = (this.target.transform.position - this.me.transform.position).normalized;
		Debug.DrawRay(this.me.transform.position, normalized * 100f);
		Vector3 vector = Vector3.Cross(forward, normalized);
		Debug.Log(vector);
	}

	// Token: 0x040005B8 RID: 1464
	public GameObject me;

	// Token: 0x040005B9 RID: 1465
	public GameObject target;
}

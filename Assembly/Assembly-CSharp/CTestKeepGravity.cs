using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class CTestKeepGravity : MonoBehaviour
{
	// Token: 0x060007BA RID: 1978 RVA: 0x00043867 File Offset: 0x00041A67
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00043878 File Offset: 0x00041A78
	private void FixedUpdate()
	{
		bool flag = !(this.rig == null);
		if (flag)
		{
			this.rig.AddForce(-Physics.gravity);
		}
	}

	// Token: 0x040005BE RID: 1470
	private Rigidbody rig;
}

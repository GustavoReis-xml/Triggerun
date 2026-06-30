using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class CTestUpAngle : MonoBehaviour
{
	// Token: 0x060007DD RID: 2013 RVA: 0x000445D2 File Offset: 0x000427D2
	private void Start()
	{
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x000445D8 File Offset: 0x000427D8
	private void Update()
	{
		Vector3 forward = this.T1.forward;
		Vector3 up = Vector3.up;
		Vector3 forward2 = Vector3.Lerp(forward, up, 0.1f);
		this.T2.forward = forward2;
	}

	// Token: 0x040005D2 RID: 1490
	public Transform T1;

	// Token: 0x040005D3 RID: 1491
	public Transform T2;
}

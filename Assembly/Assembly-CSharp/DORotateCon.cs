using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class DORotateCon : MonoBehaviour
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x0004C36C File Offset: 0x0004A56C
	private void Update()
	{
		bool flag = this.bStart;
		if (flag)
		{
			this.nZ = base.transform.localEulerAngles.z;
			this.nZ -= Time.deltaTime * 500f;
			bool flag2 = this.nZ <= -360f;
			if (flag2)
			{
				this.nZ = 0f;
			}
			base.transform.localRotation = Quaternion.Euler(0f, 0f, this.nZ);
		}
	}

	// Token: 0x04000687 RID: 1671
	public bool bStart;

	// Token: 0x04000688 RID: 1672
	private float nZ;
}

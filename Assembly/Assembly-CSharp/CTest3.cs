using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class CTest3 : MonoBehaviour
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x0004340A File Offset: 0x0004160A
	private void Start()
	{
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00043410 File Offset: 0x00041610
	private void Update()
	{
		bool flag = !(this.target == null);
		if (flag)
		{
			Vector3 normalized = (this.target.position - base.transform.position).normalized;
			Debug.DrawRay(base.transform.position, normalized * 50f);
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normalized);
			base.transform.rotation = rotation;
		}
	}

	// Token: 0x040005B3 RID: 1459
	public Transform target;
}

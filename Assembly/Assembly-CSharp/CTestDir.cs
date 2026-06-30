using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class CTestDir : MonoBehaviour
{
	// Token: 0x060007AA RID: 1962 RVA: 0x00043495 File Offset: 0x00041695
	private void Start()
	{
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00043498 File Offset: 0x00041698
	private void Update()
	{
		this.t1.transform.forward = this.dir1;
		this.t2.transform.forward = this.dir2;
		float num = Vector3.Dot(this.dir1, this.dir2);
		float num2 = 0f - (num - 1f);
		Debug.Log(string.Concat(new string[]
		{
			num.ToString("F1"),
			",",
			num2.ToString("F1"),
			"   ,    ",
			this.dir1.ToString("F1"),
			",",
			this.dir2.ToString("F1")
		}));
	}

	// Token: 0x040005B4 RID: 1460
	public Transform t1;

	// Token: 0x040005B5 RID: 1461
	public Transform t2;

	// Token: 0x040005B6 RID: 1462
	public Vector3 dir1 = Vector3.forward;

	// Token: 0x040005B7 RID: 1463
	public Vector3 dir2 = Vector3.forward;
}

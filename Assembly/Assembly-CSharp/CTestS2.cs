using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class CTestS2 : MonoBehaviour
{
	// Token: 0x060007CA RID: 1994 RVA: 0x00043EC6 File Offset: 0x000420C6
	private void Start()
	{
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00043ECC File Offset: 0x000420CC
	private void Update()
	{
		Debug.Log(string.Concat(new string[]
		{
			"localScale:",
			base.transform.localScale.ToString("F4"),
			",lossyScale:",
			base.transform.lossyScale.ToString("F4"),
			",(",
			(base.transform.localScale.x / base.transform.lossyScale.x).ToString("F4"),
			",",
			(base.transform.localScale.y / base.transform.lossyScale.y).ToString("F4"),
			",",
			(base.transform.localScale.z / base.transform.lossyScale.z).ToString("F4"),
			"),(",
			(base.transform.lossyScale.x / base.transform.localScale.x).ToString("F4"),
			",",
			(base.transform.lossyScale.y / base.transform.localScale.y).ToString("F4"),
			",",
			(base.transform.lossyScale.z / base.transform.localScale.z).ToString("F4"),
			"),1/lossyScale:(",
			(1f / base.transform.lossyScale.x).ToString("F4"),
			",",
			(1f / base.transform.lossyScale.y).ToString("F4"),
			",",
			(1f / base.transform.lossyScale.z).ToString("F4"),
			"),1/localScale:(",
			(1f / base.transform.localScale.x).ToString("F4"),
			",",
			(1f / base.transform.localScale.y).ToString("F4"),
			",",
			(1f / base.transform.localScale.z).ToString("F4"),
			")"
		}));
		Debug.Log(Vector3.ClampMagnitude(this.v3, 1f).ToString("F5"));
	}

	// Token: 0x040005C9 RID: 1481
	public Vector3 v3;
}

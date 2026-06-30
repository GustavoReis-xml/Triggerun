using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class CTestUp : MonoBehaviour
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x0004453F File Offset: 0x0004273F
	private void Start()
	{
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00044542 File Offset: 0x00042742
	private void Update()
	{
		this.q = Quaternion.LookRotation(this.T1.forward, Vector3.right);
		this.T2.transform.rotation = this.q;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00044578 File Offset: 0x00042778
	private void OnGUI()
	{
		string text = string.Format("q:{0}", this.q.eulerAngles.ToString("F1"));
		GUI.Label(new Rect(10f, 10f, 500f, 50f), text);
	}

	// Token: 0x040005CF RID: 1487
	public Transform T1;

	// Token: 0x040005D0 RID: 1488
	public Transform T2;

	// Token: 0x040005D1 RID: 1489
	private Quaternion q;
}

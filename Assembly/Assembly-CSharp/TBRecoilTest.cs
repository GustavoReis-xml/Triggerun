using System;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class TBRecoilTest : MonoBehaviour
{
	// Token: 0x06002C0C RID: 11276 RVA: 0x0018211C File Offset: 0x0018031C
	private void Start()
	{
		this.recoil = base.GetComponent<TBRecoil>();
	}

	// Token: 0x06002C0D RID: 11277 RVA: 0x0018212C File Offset: 0x0018032C
	private void Update()
	{
		bool flag = Time.time >= this.t;
		if (flag)
		{
			this.t = Time.time + this.firestep;
			this.recoil.Fire(this.magnitude);
		}
	}

	// Token: 0x06002C0E RID: 11278 RVA: 0x00182174 File Offset: 0x00180374
	private void OnGUI()
	{
		GUILayout.Label("Press R for procedural recoil.", new GUILayoutOption[0]);
	}

	// Token: 0x04002584 RID: 9604
	public float magnitude = 1f;

	// Token: 0x04002585 RID: 9605
	private TBRecoil recoil;

	// Token: 0x04002586 RID: 9606
	private float t;

	// Token: 0x04002587 RID: 9607
	public float firestep = 0.1f;
}

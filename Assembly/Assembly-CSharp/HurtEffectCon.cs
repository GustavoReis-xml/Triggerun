using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E9 RID: 489
public class HurtEffectCon : MonoBehaviour
{
	// Token: 0x060015EB RID: 5611 RVA: 0x000CD29C File Offset: 0x000CB49C
	private void Awake()
	{
		this.oImg = base.transform.GetComponent<Image>();
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000CD2B0 File Offset: 0x000CB4B0
	private void Start()
	{
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000CD2B3 File Offset: 0x000CB4B3
	private void Update()
	{
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x000CD2B8 File Offset: 0x000CB4B8
	public void OnStart(Vector3 ve)
	{
		bool flag = this.vOld != ve;
		if (flag)
		{
			base.transform.localRotation = Quaternion.Euler(ve);
			this.vOld = ve;
		}
		bool flag2 = base.IsInvoking("DelayDelete");
		if (flag2)
		{
			base.CancelInvoke("DelayDelete");
		}
		base.Invoke("DelayDelete", 1f);
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x000CD320 File Offset: 0x000CB520
	private void DelayDelete()
	{
		Color endValue = new Color(1f, 1f, 1f, 0f);
		Tween t = this.oImg.DOColor(endValue, 0.5f);
		t.OnComplete(delegate
		{
			this.OverShowUI();
		});
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x000CD36E File Offset: 0x000CB56E
	private void OverShowUI()
	{
		base.CancelInvoke("DelayDelete");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400110E RID: 4366
	private Image oImg;

	// Token: 0x0400110F RID: 4367
	private Vector3 vOld;
}

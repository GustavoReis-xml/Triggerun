using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AC RID: 428
public class HeroPartsAnim : MonoBehaviour
{
	// Token: 0x060013AD RID: 5037 RVA: 0x000BA61F File Offset: 0x000B881F
	private void Awake()
	{
		this.moveText = base.GetComponent<Text>();
		this.moveText.text = string.Empty;
		this.startPos = this.moveText.rectTransform.anchoredPosition3D;
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000BA655 File Offset: 0x000B8855
	private void Start()
	{
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x000BA658 File Offset: 0x000B8858
	private void Update()
	{
		bool flag = this.startDelay > 0f && Time.time - this.startDelay >= 1f;
		if (flag)
		{
			this.startDelay = 0f;
			this.textAnim = this.moveText.rectTransform.DOAnchorPos3D(this.startPos + Vector3.up * 50f, 1f, false);
			this.textAnim.OnComplete(delegate
			{
				this.moveText.text = string.Empty;
				this.StartAnim();
			});
		}
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000BA6EB File Offset: 0x000B88EB
	public void AddShowText(string sText)
	{
		this.szList.Add(sText);
		this.StartAnim();
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x000BA704 File Offset: 0x000B8904
	private void StartAnim()
	{
		bool flag = (this.textAnim != null && this.textAnim.IsPlaying()) || this.szList.Count == 0;
		if (!flag)
		{
			this.moveText.rectTransform.anchoredPosition3D = this.startPos;
			this.moveText.text = this.szList[0];
			this.szList.RemoveAt(0);
			this.textAnim = this.moveText.rectTransform.DOScale(1.5f, 0.5f);
			this.textAnim.OnComplete(delegate
			{
				this.textAnim = this.moveText.rectTransform.DOScale(1f, 0.1f);
				this.textAnim.OnComplete(delegate
				{
					this.startDelay = Time.time;
				});
			});
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000BA7B4 File Offset: 0x000B89B4
	public void StopAnim()
	{
		bool flag = this.textAnim != null;
		if (flag)
		{
			this.startDelay = 0f;
			this.moveText.text = string.Empty;
			this.szList.Clear();
			this.textAnim.Kill(true);
		}
	}

	// Token: 0x04000F33 RID: 3891
	private float startDelay;

	// Token: 0x04000F34 RID: 3892
	private Text moveText;

	// Token: 0x04000F35 RID: 3893
	private List<string> szList = new List<string>();

	// Token: 0x04000F36 RID: 3894
	private Vector3 startPos;

	// Token: 0x04000F37 RID: 3895
	private Tweener textAnim;
}

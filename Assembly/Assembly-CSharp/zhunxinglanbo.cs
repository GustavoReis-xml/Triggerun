using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C0 RID: 1216
public class zhunxinglanbo : zhunxingBase
{
	// Token: 0x06002EE7 RID: 12007 RVA: 0x001A328C File Offset: 0x001A148C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.superskillzhunxing = gotherui.FindChild("superskillzhunxing").gameObject;
		this.superskillzhunxing.SetActive(false);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stime = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x001A3329 File Offset: 0x001A1529
	protected override void Update()
	{
		base.Update();
		this.CheckDjs();
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x001A333C File Offset: 0x001A153C
	private void CheckDjs()
	{
		bool flag = this.oDjs == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
			if (flag2)
			{
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool flag4 = !this.oDjs.activeSelf;
					if (flag4)
					{
						this.oDjs.SetActive(true);
					}
					float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
					this.img.fillAmount = num / fContinuesTime;
					this.stime.text = Mathf.CeilToInt(num).ToString() + "s";
					bool flag5 = !this.superskillzhunxing.activeSelf;
					if (flag5)
					{
						this.superskillzhunxing.SetActive(true);
					}
					base.ChecklanboAutoAir_Df(true);
					return;
				}
			}
			bool activeSelf = this.oDjs.activeSelf;
			if (activeSelf)
			{
				this.oDjs.SetActive(false);
			}
			bool activeSelf2 = this.superskillzhunxing.activeSelf;
			if (activeSelf2)
			{
				this.superskillzhunxing.SetActive(false);
			}
			base.ChecklanboAutoAir_Df(false);
		}
	}

	// Token: 0x04002897 RID: 10391
	private GameObject oDjs;

	// Token: 0x04002898 RID: 10392
	private Image img;

	// Token: 0x04002899 RID: 10393
	private Text stime;

	// Token: 0x0400289A RID: 10394
	private DateTime m_dDateTime;

	// Token: 0x0400289B RID: 10395
	private GameObject superskillzhunxing;
}

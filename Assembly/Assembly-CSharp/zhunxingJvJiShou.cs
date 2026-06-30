using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004BF RID: 1215
public class zhunxingJvJiShou : zhunxingBase
{
	// Token: 0x06002ED9 RID: 11993 RVA: 0x001A2A7C File Offset: 0x001A0C7C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gXuli = gotherui.Find("xuli").gameObject;
		this.imagex1 = this.gXuli.transform.Find("back2").GetComponent<Image>();
		this.imagex2 = this.gXuli.transform.Find("back3").GetComponent<Image>();
		this.imagex3 = this.gXuli.transform.Find("back4").GetComponent<Image>();
		this.textProshow = this.gXuli.transform.Find("proshow").GetComponent<Text>();
		this.initXuli();
		this.GHongWai = gotherui.Find("hongwai").gameObject;
		this.imageHongWai = this.GHongWai.transform.Find("Image2").GetComponent<Image>();
		this.GHongWai.transform.Find("desc").GetComponent<Text>().text = Language.GetLang(3012, new object[0]);
		this.GHongWai.SetActive(false);
		this.bUiLoaded = true;
		this.gRead = base.tOtherUi.Find("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.ldesc.text = Language.GetLang(3013, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.gRead.SetActive(false);
		this.gYinShen = gotherui.Find("yinshen").gameObject;
		this.gYsfill = this.gYinShen.transform.Find("ysfill").GetComponent<Image>();
		this.gYinShen.transform.Find("ystext").GetComponent<Text>().text = Language.GetLang(3023, new object[0]);
		this.gYinShen.SetActive(false);
		this.gKaiJin = gotherui.Find("ui_jjs_kaijin").gameObject;
		this.gKaiJin.SetActive(false);
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x001A2D00 File Offset: 0x001A0F00
	protected override void Update()
	{
		base.Update();
		bool flag = !this.bUiLoaded;
		if (!flag)
		{
			this.drawHongWai();
			this.CheckYinShen();
			bool flag2 = this.gKaiJin != null && this.hero != null;
			if (flag2)
			{
				bool flag3 = ((IHeroDataJvjishou)this.hero.heroBase.IHD).playerState == NetMsg.JvJiShouState.KaiJing && !this.gKaiJin.activeSelf && !((IHeroDataJvjishou)this.hero.heroBase.IHD).GuanJing;
				if (flag3)
				{
					this.gKaiJin.SetActive(true);
				}
				bool flag4 = this.gKaiJin.activeSelf && ((IHeroDataJvjishou)this.hero.heroBase.IHD).GuanJing;
				if (flag4)
				{
					this.gKaiJin.SetActive(false);
				}
				bool flag5 = this.hero.isDead && this.gKaiJin.activeSelf;
				if (flag5)
				{
					this.gKaiJin.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x001A2E24 File Offset: 0x001A1024
	private void initXuli()
	{
		this.imagex1.fillAmount = 0f;
		this.imagex2.fillAmount = 0f;
		this.imagex3.fillAmount = 0f;
		this.textProshow.text = string.Empty;
		this.gXuli.SetActive(false);
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x001A2E84 File Offset: 0x001A1084
	private void drawXuli(float fPercent)
	{
		bool flag = fPercent <= 0.33f;
		if (flag)
		{
			this.imagex1.fillAmount = fPercent * 3f;
		}
		else
		{
			bool flag2 = fPercent <= 0.66f;
			if (flag2)
			{
				this.imagex2.fillAmount = (fPercent - 0.33f) * 3f;
			}
			else
			{
				this.imagex3.fillAmount = (fPercent - 0.66f) * 3f;
			}
		}
		float num = fPercent * 100f;
		bool flag3 = num > 100f;
		if (flag3)
		{
			num = 100f;
		}
		this.textProshow.text = string.Format("{0}%", num.ToString("f0"));
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x001A2F40 File Offset: 0x001A1140
	private void drawHongWai()
	{
		bool flag = !(this.hero.playerDazhao != null);
		if (!flag)
		{
			bool flag2 = !this.hero.playerDazhao.doing;
			if (flag2)
			{
				bool flag3 = this.bHongWaiIng;
				if (flag3)
				{
					this.GHongWai.SetActive(false);
					this.bHongWaiIng = false;
				}
			}
			else
			{
				bool flag4 = !this.bHongWaiIng;
				if (flag4)
				{
					this.GHongWai.SetActive(true);
					this.bHongWaiIng = true;
				}
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag5 = this.imageHongWai != null;
				if (flag5)
				{
					this.imageHongWai.fillAmount = num / this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
				}
			}
		}
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x001A3020 File Offset: 0x001A1220
	public override void readyShow()
	{
		bool flag = !this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(true);
			base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
		}
		base.zhunxingshowOrHide(false);
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x001A307A File Offset: 0x001A127A
	public override void readyDisable()
	{
		this.gRead.SetActive(false);
		base.zhunxingshowOrHide(true);
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x001A3094 File Offset: 0x001A1294
	public override void OnDeath()
	{
		base.OnDeath();
		this.hero.playerDazhao.doing = false;
		bool flag = this.gRead != null && this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(false);
		}
	}

	// Token: 0x06002EE1 RID: 12001 RVA: 0x001A30E9 File Offset: 0x001A12E9
	public override void Revive()
	{
		base.Revive();
	}

	// Token: 0x06002EE2 RID: 12002 RVA: 0x001A30F4 File Offset: 0x001A12F4
	public override void ShowSkillDo(object opam)
	{
		int num = (int)opam;
		bool flag = num == 1009;
		if (flag)
		{
			CPSkillStruct skillData = this.hero.heroSkill.GetSkillData(1009);
			bool flag2 = skillData != null;
			if (flag2)
			{
				this.nDjlLong = skillData.m_nSKLongTime;
				this.sYsEndTime = Fun.Now.AddSeconds((double)(this.nDjlLong / 1000)).ToString();
			}
		}
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x001A3170 File Offset: 0x001A1370
	public override void ShowSkillFinish(object opam)
	{
		int num = (int)opam;
		bool flag = num == 1009;
		if (flag)
		{
			this.OnYinShenEnd();
		}
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x001A319C File Offset: 0x001A139C
	private void CheckYinShen()
	{
		bool flag = this.hero == null || !(this.hero.wc != null) || !(this.sYsEndTime != string.Empty);
		if (!flag)
		{
			int endCDMilliseconds = Fun.GetEndCDMilliseconds(this.sYsEndTime, 0);
			bool flag2 = endCDMilliseconds >= 0;
			if (flag2)
			{
				this.gYsfill.fillAmount = (float)endCDMilliseconds / (float)this.nDjlLong;
				bool flag3 = !this.gYinShen.activeSelf;
				if (flag3)
				{
					this.gYinShen.SetActive(true);
				}
			}
			else
			{
				this.OnYinShenEnd();
			}
		}
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x001A3240 File Offset: 0x001A1440
	private void OnYinShenEnd()
	{
		this.sYsEndTime = string.Empty;
		bool activeSelf = this.gYinShen.activeSelf;
		if (activeSelf)
		{
			this.gYinShen.SetActive(false);
		}
	}

	// Token: 0x04002885 RID: 10373
	private bool bUiLoaded;

	// Token: 0x04002886 RID: 10374
	private GameObject gXuli;

	// Token: 0x04002887 RID: 10375
	private Image imagex1;

	// Token: 0x04002888 RID: 10376
	private Image imagex2;

	// Token: 0x04002889 RID: 10377
	private Image imagex3;

	// Token: 0x0400288A RID: 10378
	private Text textProshow;

	// Token: 0x0400288B RID: 10379
	private GameObject GHongWai;

	// Token: 0x0400288C RID: 10380
	private Image imageHongWai;

	// Token: 0x0400288D RID: 10381
	private GameObject gYinShen;

	// Token: 0x0400288E RID: 10382
	private Image gYsfill;

	// Token: 0x0400288F RID: 10383
	private GameObject gKaiJin;

	// Token: 0x04002890 RID: 10384
	private string sYsEndTime = string.Empty;

	// Token: 0x04002891 RID: 10385
	private int nDjlLong;

	// Token: 0x04002892 RID: 10386
	private bool bHongWaiIng;

	// Token: 0x04002893 RID: 10387
	private GameObject gRead;

	// Token: 0x04002894 RID: 10388
	private Text ldesc;

	// Token: 0x04002895 RID: 10389
	private Text rdesc;

	// Token: 0x04002896 RID: 10390
	private Image rImg;
}

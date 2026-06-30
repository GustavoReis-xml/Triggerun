using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D2 RID: 1234
public class zhunxingZhuRong : zhunxingBase
{
	// Token: 0x06002F46 RID: 12102 RVA: 0x001A600C File Offset: 0x001A420C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		Transform x = gotherui.FindChild("read");
		bool flag = x == null;
		if (flag)
		{
			Debug.Log("祝融准星zhunxingZhuRong初始化时gotherui中没有找到read对象");
		}
		this.gRead = gotherui.FindChild("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stiem = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
		this.gRead.SetActive(false);
		Transform x2 = gotherui.FindChild("itemnum");
		bool flag2 = x2 == null;
		if (flag2)
		{
			Debug.Log("祝融准星zhunxingZhuRong初始化时gotherui中没有找到itemnum对象");
		}
		this.oItemNum = gotherui.FindChild("itemnum").gameObject;
		this.leftnum = this.oItemNum.transform.Find("leftnum").GetComponent<Text>();
		this.rightnum = this.oItemNum.transform.Find("rightnum").GetComponent<Text>();
		this.imgIco = this.oItemNum.transform.Find("img").GetComponent<RawImage>();
		this.imgIco.enabled = false;
		this.oItemNum.SetActive(false);
		this.stru = this.hero.heroSkill.GetSkillData(1067);
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x001A61FC File Offset: 0x001A43FC
	protected override void Update()
	{
		base.Update();
		bool flag = this.gRead == null || this.oDjs == null;
		if (!flag)
		{
			bool flag2 = this.fDjsTime > 0f;
			if (flag2)
			{
				float num = this.fDjsTime - Time.time;
				bool flag3 = num >= 0f;
				if (flag3)
				{
					this.img.fillAmount = num / this.fDjlLong;
					this.stiem.text = Mathf.CeilToInt(num).ToString() + "s";
				}
			}
			this.OnUpdateNpcNum();
		}
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x001A62A8 File Offset: 0x001A44A8
	public override void readyShow()
	{
		bool flag = !this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(true);
			this.ldesc.text = Language.GetLang(3017, new object[0]);
			base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
		}
		bool activeSelf = this.oDjs.activeSelf;
		if (activeSelf)
		{
			this.oDjs.SetActive(false);
		}
		bool flag2 = this.hero.GetAnimatorInteger("AttackState") != 0;
		if (flag2)
		{
			this.hero.SetAnimator("AttackState", 2, 0, 2);
		}
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x001A6366 File Offset: 0x001A4566
	public override void readyDisable()
	{
		this.OnEndVisableUI();
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x001A6370 File Offset: 0x001A4570
	public override void ShowSkillDo(object opam)
	{
		bool flag = !this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(true);
			this.ldesc.text = Language.GetLang(3014, new object[0]);
			base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
		}
		bool flag2 = !this.oDjs.activeSelf;
		if (flag2)
		{
			int idx = (int)opam;
			CPSkillStruct skillData = this.hero.heroSkill.GetSkillData(idx);
			bool flag3 = skillData != null;
			if (flag3)
			{
				this.fDjlLong = (float)skillData.m_nSKLongTime / 1000f;
				bool flag4 = this.fDjlLong > 0f;
				if (flag4)
				{
					bool flag5 = !this.oDjs.activeSelf;
					if (flag5)
					{
						this.oDjs.SetActive(true);
					}
					this.fDjsTime = Time.time + this.fDjlLong;
					return;
				}
			}
		}
		bool activeSelf = this.oDjs.activeSelf;
		if (activeSelf)
		{
			this.oDjs.SetActive(false);
		}
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x001A6498 File Offset: 0x001A4698
	public override void ShowSkillFinish(object opam)
	{
		this.OnEndVisableUI();
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x001A64A2 File Offset: 0x001A46A2
	public override void OnDeath()
	{
		base.OnDeath();
		this.OnEndVisableUI();
	}

	// Token: 0x06002F4D RID: 12109 RVA: 0x001A64B4 File Offset: 0x001A46B4
	private void OnEndVisableUI()
	{
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
		bool activeSelf2 = this.oDjs.activeSelf;
		if (activeSelf2)
		{
			this.oDjs.SetActive(false);
		}
	}

	// Token: 0x06002F4E RID: 12110 RVA: 0x001A6500 File Offset: 0x001A4700
	private void OnUpdateNpcNum()
	{
		bool flag = this.oItemNum == null || this.hero.heroSkill == null;
		if (!flag)
		{
			this.stru = this.hero.heroSkill.GetSkillData(1067);
			bool flag2 = this.stru == null;
			if (!flag2)
			{
				bool flag3 = !this.imgIco.enabled;
				if (flag3)
				{
					sysload.loadImage(this.imgIco, "skillicon/" + this.stru._skilldata.icon + ".txt", null);
				}
				bool flag4 = this.stru.m_nNpcNum > 0;
				if (flag4)
				{
					bool flag5 = !this.oItemNum.activeSelf;
					if (flag5)
					{
						this.oItemNum.SetActive(true);
					}
					this.leftnum.text = this.stru.m_nNpcNum.ToString();
					this.rightnum.text = this.stru._skilldata.maxItemNumber.ToString();
				}
				else
				{
					bool activeSelf = this.oItemNum.activeSelf;
					if (activeSelf)
					{
						this.oItemNum.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x040028F4 RID: 10484
	private GameObject gRead;

	// Token: 0x040028F5 RID: 10485
	private GameObject oDjs;

	// Token: 0x040028F6 RID: 10486
	private GameObject oItemNum;

	// Token: 0x040028F7 RID: 10487
	private Image img;

	// Token: 0x040028F8 RID: 10488
	private Text stiem;

	// Token: 0x040028F9 RID: 10489
	private Text leftnum;

	// Token: 0x040028FA RID: 10490
	private Text rightnum;

	// Token: 0x040028FB RID: 10491
	private RawImage imgIco;

	// Token: 0x040028FC RID: 10492
	private float fDjsTime;

	// Token: 0x040028FD RID: 10493
	private float fDjlLong;

	// Token: 0x040028FE RID: 10494
	private CPSkillStruct stru;

	// Token: 0x040028FF RID: 10495
	private Text ldesc;

	// Token: 0x04002900 RID: 10496
	private Text rdesc;

	// Token: 0x04002901 RID: 10497
	private Image rImg;
}

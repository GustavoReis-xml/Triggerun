using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CE RID: 1230
public class zhunxingVampire : zhunxingBase
{
	// Token: 0x06002F33 RID: 12083 RVA: 0x001A55F4 File Offset: 0x001A37F4
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gRead = gotherui.FindChild("read").gameObject;
		this.gRead.transform.Find("Image2").GetComponent<Image>().enabled = false;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.ldesc.text = Language.GetLang(3014, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.oDjs.GetComponent<Image>().enabled = false;
		this.stiem = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.superdjs = gotherui.FindChild("superdjs").gameObject;
		this.superimg = this.superdjs.transform.Find("img").GetComponent<Image>();
		this.supertiem = this.superdjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
		this.gRead.SetActive(false);
		this.superdjs.SetActive(false);
	}

	// Token: 0x06002F34 RID: 12084 RVA: 0x001A57C0 File Offset: 0x001A39C0
	protected override void Update()
	{
		base.Update();
		bool flag = !this.CheckDjs();
		if (flag)
		{
			this.CheckSuperSkillUi();
		}
	}

	// Token: 0x06002F35 RID: 12085 RVA: 0x001A57EC File Offset: 0x001A39EC
	private void CheckSuperSkillUi()
	{
		bool flag = this.superdjs == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
			if (flag2)
			{
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool flag4 = !this.superdjs.activeSelf;
					if (flag4)
					{
						this.superdjs.SetActive(true);
					}
					float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
					this.superimg.fillAmount = num / fContinuesTime;
					this.supertiem.text = Mathf.CeilToInt(num).ToString() + "s";
					return;
				}
			}
			bool activeSelf = this.superdjs.activeSelf;
			if (activeSelf)
			{
				this.superdjs.SetActive(false);
			}
		}
	}

	// Token: 0x06002F36 RID: 12086 RVA: 0x001A5918 File Offset: 0x001A3B18
	private bool CheckDjs()
	{
		bool flag = this.gRead == null || this.oDjs == null || (this.hero.playerDazhao != null && this.hero.playerDazhao.doing);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (int)(this.m_dDateTime - Fun.Now).TotalMilliseconds;
			bool flag2 = this.bBool && num > 0;
			if (flag2)
			{
				bool flag3 = !this.gRead.activeSelf;
				if (flag3)
				{
					this.gRead.SetActive(true);
					base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
				}
				bool flag4 = !this.oDjs.activeSelf;
				if (flag4)
				{
					this.oDjs.SetActive(true);
				}
				this.stiem.text = Mathf.CeilToInt((float)num / 1000f).ToString() + "S";
				this.img.fillAmount = (float)num / (float)this.nDjlLong;
			}
			else
			{
				this.OnEndVisableUI();
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06002F37 RID: 12087 RVA: 0x001A5A68 File Offset: 0x001A3C68
	public override void ShowSkillDo(object opam)
	{
		int num = (int)opam;
		bool flag = num != 1100;
		if (!flag)
		{
			CPSkillStruct skillData = this.hero.heroSkill.GetSkillData(num);
			bool flag2 = skillData != null;
			if (flag2)
			{
				this.nDjlLong = skillData.m_nSKLongTime;
				bool flag3 = this.nDjlLong > 0;
				if (flag3)
				{
					this.m_dDateTime = Fun.Now.AddMilliseconds((double)this.nDjlLong);
					this.bBool = true;
				}
			}
		}
	}

	// Token: 0x06002F38 RID: 12088 RVA: 0x001A5AEC File Offset: 0x001A3CEC
	public override void ShowSkillFinish(object opam)
	{
		int num = (int)opam;
		bool flag = num == 1100;
		if (flag)
		{
			this.OnEndVisableUI();
		}
	}

	// Token: 0x06002F39 RID: 12089 RVA: 0x001A5B16 File Offset: 0x001A3D16
	public override void OnDeath()
	{
		base.OnDeath();
		this.OnEndVisableUI();
	}

	// Token: 0x06002F3A RID: 12090 RVA: 0x001A5B28 File Offset: 0x001A3D28
	private void OnEndVisableUI()
	{
		bool flag = !(this.gRead == null) && !(this.oDjs == null) && !(this.superdjs == null);
		if (flag)
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
			bool activeSelf3 = this.superdjs.activeSelf;
			if (activeSelf3)
			{
				this.superdjs.SetActive(false);
			}
			this.bBool = false;
		}
	}

	// Token: 0x040028DD RID: 10461
	private GameObject gRead;

	// Token: 0x040028DE RID: 10462
	private GameObject oDjs;

	// Token: 0x040028DF RID: 10463
	private Image img;

	// Token: 0x040028E0 RID: 10464
	private Text stiem;

	// Token: 0x040028E1 RID: 10465
	private GameObject superdjs;

	// Token: 0x040028E2 RID: 10466
	private Image superimg;

	// Token: 0x040028E3 RID: 10467
	private Text supertiem;

	// Token: 0x040028E4 RID: 10468
	private int nDjlLong;

	// Token: 0x040028E5 RID: 10469
	private DateTime m_dDateTime;

	// Token: 0x040028E6 RID: 10470
	private bool bBool;

	// Token: 0x040028E7 RID: 10471
	private Text ldesc;

	// Token: 0x040028E8 RID: 10472
	private Text rdesc;

	// Token: 0x040028E9 RID: 10473
	private Image rImg;
}

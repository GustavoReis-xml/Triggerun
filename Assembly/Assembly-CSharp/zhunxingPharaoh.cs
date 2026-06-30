using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C9 RID: 1225
public class zhunxingPharaoh : zhunxingBase
{
	// Token: 0x06002F05 RID: 12037 RVA: 0x001A3EC0 File Offset: 0x001A20C0
	public override void init(NetLiving hero)
	{
		base.init(hero);
		this.phero = hero;
		this.heropharao = (IHeroDataLeiZhenZi)hero.heroBase.IHD;
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x001A3EE8 File Offset: 0x001A20E8
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.GFlyParent = gotherui.FindChild("fly").gameObject;
		this.imageFlyFill = this.GFlyParent.transform.FindChild("fill").GetComponent<Image>();
		this.showOrHideFlyUi(false);
		this.bUiLoad = true;
		this.gRead = gotherui.FindChild("read").gameObject;
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stiem = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.ldesc.text = Language.GetLang(3029, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.gRead.SetActive(false);
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x001A4064 File Offset: 0x001A2264
	protected override void Update()
	{
		base.Update();
		this.CheckSuperSkill();
		bool flag = !this.bUiLoad;
		if (!flag)
		{
			bool isDead = this.phero.isDead;
			if (isDead)
			{
				this.showOrHideFlyUi(false);
			}
			else
			{
				bool flag2 = !this.heropharao.Energy_Deduct_State;
				if (flag2)
				{
					bool flag3 = this.flying && this.heropharao.Current_Energy >= this.heropharao.Max_Energy;
					if (flag3)
					{
						this.showOrHideFlyUi(false);
					}
				}
				else
				{
					bool flag4 = !this.flying;
					if (flag4)
					{
						this.showOrHideFlyUi(true);
					}
				}
				bool flag5 = this.flying;
				if (flag5)
				{
					this.imageFlyFill.fillAmount = this.heropharao.Current_Energy * 1f / this.heropharao.Max_Energy;
				}
			}
		}
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x001A414C File Offset: 0x001A234C
	private void showOrHideFlyUi(bool boo)
	{
		bool flag = !(this.GFlyParent == null);
		if (flag)
		{
			this.flying = boo;
			bool flag2 = this.GFlyParent.activeSelf != boo;
			if (flag2)
			{
				this.GFlyParent.SetActive(boo);
			}
		}
	}

	// Token: 0x06002F09 RID: 12041 RVA: 0x001A419C File Offset: 0x001A239C
	private void CheckSuperSkill()
	{
		bool flag = this.gRead == null || this.oDjs == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
			if (flag2)
			{
				this.showOrHideFlyUi(false);
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool flag4 = !this.gRead.activeSelf;
					if (flag4)
					{
						this.gRead.SetActive(true);
						base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
					}
					bool flag5 = !this.oDjs.activeSelf;
					if (flag5)
					{
						this.oDjs.SetActive(true);
					}
					float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
					this.img.fillAmount = num / fContinuesTime;
					this.stiem.text = Mathf.CeilToInt(num).ToString() + "s";
					return;
				}
			}
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
			bool flag6 = this.heropharao.Current_Energy < this.heropharao.Max_Energy;
			if (flag6)
			{
				this.showOrHideFlyUi(true);
			}
		}
	}

	// Token: 0x040028B3 RID: 10419
	private bool bUiLoad;

	// Token: 0x040028B4 RID: 10420
	private GameObject GFlyParent;

	// Token: 0x040028B5 RID: 10421
	private Image imageFlyFill;

	// Token: 0x040028B6 RID: 10422
	private bool flying;

	// Token: 0x040028B7 RID: 10423
	private IHeroDataLeiZhenZi heropharao;

	// Token: 0x040028B8 RID: 10424
	private NetLiving phero;

	// Token: 0x040028B9 RID: 10425
	private GameObject gRead;

	// Token: 0x040028BA RID: 10426
	private GameObject oDjs;

	// Token: 0x040028BB RID: 10427
	private Image img;

	// Token: 0x040028BC RID: 10428
	private Text stiem;

	// Token: 0x040028BD RID: 10429
	private Text leftnum;

	// Token: 0x040028BE RID: 10430
	private Text rightnum;

	// Token: 0x040028BF RID: 10431
	private Text ldesc;

	// Token: 0x040028C0 RID: 10432
	private Text rdesc;

	// Token: 0x040028C1 RID: 10433
	private Image rImg;
}

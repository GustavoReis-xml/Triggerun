using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C3 RID: 1219
public class zhunxingLingLong : zhunxingBase
{
	// Token: 0x06002EEF RID: 12015 RVA: 0x001A34F0 File Offset: 0x001A16F0
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		Transform x = gotherui.FindChild("read");
		bool flag = x == null;
		if (flag)
		{
			Debug.Log("玲珑准星zhunxingLingLong初始化时gotherui中没有找到read对象");
		}
		this.gRead = gotherui.FindChild("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.ldesc.text = Language.GetLang(3017, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.gRead.SetActive(false);
		this.oItemNum = gotherui.FindChild("itemnum").gameObject;
		this.leftnum = this.oItemNum.transform.Find("leftnum").GetComponent<Text>();
		this.rightnum = this.oItemNum.transform.Find("rightnum").GetComponent<Text>();
		this.imgIco = this.oItemNum.transform.Find("img").GetComponent<RawImage>();
		this.imgIco.enabled = false;
		this.oItemNum.SetActive(false);
		this.oDzItem = gotherui.FindChild("csmnum").gameObject;
		this.oDzItemNum = this.oDzItem.transform.Find("num").GetComponent<Text>();
		this.oDzItem.SetActive(false);
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x001A36BC File Offset: 0x001A18BC
	protected override void Update()
	{
		base.Update();
		this.OnUpdateNpcNum();
		bool flag = !(this.oDzItem != null);
		if (!flag)
		{
			int transmission_Durability = ((IHeroDataLingLong)this.hero.heroBase.IHD).Transmission_Durability;
			bool flag2 = transmission_Durability > 0 && !this.hero.isDead;
			if (flag2)
			{
				bool flag3 = !this.oDzItem.activeSelf;
				if (flag3)
				{
					this.oDzItem.SetActive(true);
				}
				this.oDzItemNum.text = transmission_Durability.ToString();
			}
			else
			{
				bool activeSelf = this.oDzItem.activeSelf;
				if (activeSelf)
				{
					this.oDzItem.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x001A3780 File Offset: 0x001A1980
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

	// Token: 0x06002EF2 RID: 12018 RVA: 0x001A37DC File Offset: 0x001A19DC
	public override void readyDisable()
	{
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
		base.zhunxingshowOrHide(true);
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x001A3810 File Offset: 0x001A1A10
	public override void OnDeath()
	{
		base.OnDeath();
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x001A3844 File Offset: 0x001A1A44
	private void OnUpdateNpcNum()
	{
		bool flag = this.oItemNum == null || this.hero.heroSkill == null;
		if (!flag)
		{
			this.stru = this.hero.heroSkill.GetSkillData(1076);
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

	// Token: 0x0400289C RID: 10396
	private GameObject gRead;

	// Token: 0x0400289D RID: 10397
	private GameObject oItemNum;

	// Token: 0x0400289E RID: 10398
	private Text leftnum;

	// Token: 0x0400289F RID: 10399
	private Text rightnum;

	// Token: 0x040028A0 RID: 10400
	private RawImage imgIco;

	// Token: 0x040028A1 RID: 10401
	private GameObject oDzItem;

	// Token: 0x040028A2 RID: 10402
	private Text oDzItemNum;

	// Token: 0x040028A3 RID: 10403
	private CPSkillStruct stru;

	// Token: 0x040028A4 RID: 10404
	private Text ldesc;

	// Token: 0x040028A5 RID: 10405
	private Text rdesc;

	// Token: 0x040028A6 RID: 10406
	private Image rImg;
}

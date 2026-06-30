using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C4 RID: 1220
public class zhunxingLuoJi : zhunxingBase
{
	// Token: 0x06002EF6 RID: 12022 RVA: 0x001A398C File Offset: 0x001A1B8C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gRead = gotherui.FindChild("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.ldesc.text = Language.GetLang(3017, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.gRead.SetActive(false);
	}

	// Token: 0x06002EF7 RID: 12023 RVA: 0x001A3A60 File Offset: 0x001A1C60
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

	// Token: 0x06002EF8 RID: 12024 RVA: 0x001A3ABC File Offset: 0x001A1CBC
	public override void readyDisable()
	{
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
		base.zhunxingshowOrHide(true);
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x001A3AF0 File Offset: 0x001A1CF0
	public override void OnDeath()
	{
		base.OnDeath();
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
	}

	// Token: 0x040028A7 RID: 10407
	private GameObject gRead;

	// Token: 0x040028A8 RID: 10408
	private Text ldesc;

	// Token: 0x040028A9 RID: 10409
	private Text rdesc;

	// Token: 0x040028AA RID: 10410
	private Image rImg;
}

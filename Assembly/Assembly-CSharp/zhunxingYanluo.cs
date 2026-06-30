using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CF RID: 1231
public class zhunxingYanluo : zhunxingBase
{
	// Token: 0x06002F3C RID: 12092 RVA: 0x001A5BD4 File Offset: 0x001A3DD4
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gRead = gotherui.FindChild("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.ldesc.text = Language.GetLang(3013, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.gRead.SetActive(false);
	}

	// Token: 0x06002F3D RID: 12093 RVA: 0x001A5CA8 File Offset: 0x001A3EA8
	public override void readyShow()
	{
		bool flag = !this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(true);
			base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
		}
	}

	// Token: 0x06002F3E RID: 12094 RVA: 0x001A5CFA File Offset: 0x001A3EFA
	public override void readyDisable()
	{
		this.gRead.SetActive(false);
	}

	// Token: 0x040028EA RID: 10474
	private GameObject gRead;

	// Token: 0x040028EB RID: 10475
	private Text ldesc;

	// Token: 0x040028EC RID: 10476
	private Text rdesc;

	// Token: 0x040028ED RID: 10477
	private Image rImg;
}

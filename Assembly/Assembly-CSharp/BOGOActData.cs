using System;
using System.Xml;

// Token: 0x02000031 RID: 49
public class BOGOActData
{
	// Token: 0x060001DC RID: 476 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.nBuyItem = int.Parse(xml.GetAttribute("buyitem"));
		this.nGiftItem = int.Parse(xml.GetAttribute("giftitem"));
		this.szRate = xml.GetAttribute("rate").Split(new char[]
		{
			':'
		});
		this.nMaxNum = int.Parse(xml.GetAttribute("maxnum"));
		this.sTitle1 = xml.GetAttribute("stitle1");
		this.sTitle2 = xml.GetAttribute("stitle2");
		this.sDesc1 = xml.GetAttribute("desc1");
		this.sDesc2 = xml.GetAttribute("desc2");
		this.sBottom1 = xml.GetAttribute("sbottom1");
		this.sBottom2 = xml.GetAttribute("sbottom2");
	}

	// Token: 0x04000173 RID: 371
	public int nId;

	// Token: 0x04000174 RID: 372
	public string sActName;

	// Token: 0x04000175 RID: 373
	public string sIconName;

	// Token: 0x04000176 RID: 374
	public int nBuyItem;

	// Token: 0x04000177 RID: 375
	public int nGiftItem;

	// Token: 0x04000178 RID: 376
	public string[] szRate;

	// Token: 0x04000179 RID: 377
	public int nMaxNum;

	// Token: 0x0400017A RID: 378
	public string sDesc1;

	// Token: 0x0400017B RID: 379
	public string sDesc2;

	// Token: 0x0400017C RID: 380
	public string sTitle1;

	// Token: 0x0400017D RID: 381
	public string sTitle2;

	// Token: 0x0400017E RID: 382
	public string sBottom1;

	// Token: 0x0400017F RID: 383
	public string sBottom2;
}

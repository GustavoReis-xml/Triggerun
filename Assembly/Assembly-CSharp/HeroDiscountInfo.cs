using System;
using System.Xml;

// Token: 0x02000179 RID: 377
public class HeroDiscountInfo
{
	// Token: 0x06001177 RID: 4471 RVA: 0x000A3C7C File Offset: 0x000A1E7C
	public void Init(XmlElement xml)
	{
		this.nHeroId = int.Parse(xml.GetAttribute("heroid"));
		this.nType = int.Parse(xml.GetAttribute("type"));
		this.nIndex = int.Parse(xml.GetAttribute("index"));
		this.sKey = string.Format("{0}_{1}_{2}", this.nHeroId, this.nType, this.nIndex);
		this.nDiscount = int.Parse(xml.GetAttribute("discount"));
		this.nGiftuse = int.Parse(xml.GetAttribute("giftuse"));
		this.szUnlock = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
	}

	// Token: 0x04000DD5 RID: 3541
	public string sKey;

	// Token: 0x04000DD6 RID: 3542
	public int nHeroId;

	// Token: 0x04000DD7 RID: 3543
	public int nType;

	// Token: 0x04000DD8 RID: 3544
	public int nIndex;

	// Token: 0x04000DD9 RID: 3545
	public int nDiscount;

	// Token: 0x04000DDA RID: 3546
	public int nGiftuse;

	// Token: 0x04000DDB RID: 3547
	public string[] szUnlock;

	// Token: 0x04000DDC RID: 3548
	public string sDesc;
}

using System;
using System.Xml;

// Token: 0x0200025C RID: 604
public class NewSoldierActData
{
	// Token: 0x06001B92 RID: 7058 RVA: 0x00104A18 File Offset: 0x00102C18
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sActDesc = xml.GetAttribute("actdesc").Replace("\\n", "\n");
		this.sActTime = xml.GetAttribute("acttime");
		this.sRegTitle = xml.GetAttribute("regtitle");
		this.szRegReward = xml.GetAttribute("regreward").Split(new char[]
		{
			','
		});
		this.sLvlTitle = xml.GetAttribute("leveltitle");
		this.nLvlNum = int.Parse(xml.GetAttribute("level"));
		this.szLvlReward = xml.GetAttribute("levelreward").Split(new char[]
		{
			','
		});
		bool flag = this.sLvlTitle.Contains("{0}");
		if (flag)
		{
			this.sLvlTitle = string.Format(this.sLvlTitle, this.nLvlNum);
		}
	}

	// Token: 0x040018E7 RID: 6375
	public int nId;

	// Token: 0x040018E8 RID: 6376
	public string sActName;

	// Token: 0x040018E9 RID: 6377
	public string sIconName;

	// Token: 0x040018EA RID: 6378
	public string sActDesc;

	// Token: 0x040018EB RID: 6379
	public string sActTime;

	// Token: 0x040018EC RID: 6380
	public string sRegTitle;

	// Token: 0x040018ED RID: 6381
	public string[] szRegReward;

	// Token: 0x040018EE RID: 6382
	public string sLvlTitle;

	// Token: 0x040018EF RID: 6383
	public int nLvlNum;

	// Token: 0x040018F0 RID: 6384
	public string[] szLvlReward;
}

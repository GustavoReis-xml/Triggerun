using System;
using System.Xml;

// Token: 0x0200023E RID: 574
public class MatchNaRewardData
{
	// Token: 0x06001A44 RID: 6724 RVA: 0x000F9248 File Offset: 0x000F7448
	public MatchNaRewardData(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nMatchNum = int.Parse(xml.GetAttribute("matchnum"));
		this.sTitle = xml.GetAttribute("title");
		this.sName = xml.GetAttribute("msg");
		this.sIcon = xml.GetAttribute("img");
		this.szReward = xml.GetAttribute("reward").Split(new char[]
		{
			','
		});
		bool flag = this.sTitle.Contains("{0}");
		if (flag)
		{
			this.sTitle = string.Format(this.sTitle, this.nMatchNum);
		}
	}

	// Token: 0x040014CF RID: 5327
	public int nId;

	// Token: 0x040014D0 RID: 5328
	public int nMatchNum;

	// Token: 0x040014D1 RID: 5329
	public string sTitle;

	// Token: 0x040014D2 RID: 5330
	public string sName;

	// Token: 0x040014D3 RID: 5331
	public string sIcon;

	// Token: 0x040014D4 RID: 5332
	public string[] szReward;
}

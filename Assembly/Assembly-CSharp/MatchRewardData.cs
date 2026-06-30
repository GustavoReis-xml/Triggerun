using System;
using System.Xml;

// Token: 0x02000240 RID: 576
public class MatchRewardData
{
	// Token: 0x06001A47 RID: 6727 RVA: 0x000F9428 File Offset: 0x000F7628
	public MatchRewardData(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nMatchNum = int.Parse(xml.GetAttribute("matchnum"));
		this.sTitle = xml.GetAttribute("title");
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

	// Token: 0x040014DB RID: 5339
	public int nId;

	// Token: 0x040014DC RID: 5340
	public int nMatchNum;

	// Token: 0x040014DD RID: 5341
	public string sTitle;

	// Token: 0x040014DE RID: 5342
	public string[] szReward;
}

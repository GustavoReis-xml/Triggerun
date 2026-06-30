using System;
using System.Xml;

// Token: 0x0200015D RID: 349
public class GuildFuliDay
{
	// Token: 0x06000FCA RID: 4042 RVA: 0x00091878 File Offset: 0x0008FA78
	public void Init(XmlElement xml)
	{
		this.nDay = int.Parse(xml.GetAttribute("day"));
		this.sTitle = xml.GetAttribute("name");
		this.szReward = xml.GetAttribute("rewarditem").Split(new char[]
		{
			','
		});
		this.nNeednum = int.Parse(xml.GetAttribute("neednum"));
	}

	// Token: 0x04000C8B RID: 3211
	public int nDay;

	// Token: 0x04000C8C RID: 3212
	public string sTitle;

	// Token: 0x04000C8D RID: 3213
	public string[] szReward;

	// Token: 0x04000C8E RID: 3214
	public int nNeednum;
}

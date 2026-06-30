using System;
using System.Xml;

// Token: 0x0200015E RID: 350
public class GuildFuliWeek
{
	// Token: 0x06000FCC RID: 4044 RVA: 0x000918F0 File Offset: 0x0008FAF0
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.szReward = xml.GetAttribute("rewarditem").Split(new char[]
		{
			','
		});
		this.nNeedLvl = int.Parse(xml.GetAttribute("needlvl"));
		this.nNeedTotal = int.Parse(xml.GetAttribute("needtotal"));
	}

	// Token: 0x04000C8F RID: 3215
	public int nId;

	// Token: 0x04000C90 RID: 3216
	public string[] szReward;

	// Token: 0x04000C91 RID: 3217
	public int nNeedLvl;

	// Token: 0x04000C92 RID: 3218
	public int nNeedTotal;
}

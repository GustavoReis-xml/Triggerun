using System;
using System.Xml;

// Token: 0x020002B4 RID: 692
public class PvpWeekReward
{
	// Token: 0x06001FCB RID: 8139 RVA: 0x00124390 File Offset: 0x00122590
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.nMatchNum = int.Parse(xml.GetAttribute("matchnum"));
		this.szReward = xml.GetAttribute("rewarditem").Split(new char[]
		{
			','
		});
		this.sTitle = xml.GetAttribute("stitle");
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
	}

	// Token: 0x04001D00 RID: 7424
	public int nId;

	// Token: 0x04001D01 RID: 7425
	public string sActName;

	// Token: 0x04001D02 RID: 7426
	public string sIconName;

	// Token: 0x04001D03 RID: 7427
	public int nMatchNum;

	// Token: 0x04001D04 RID: 7428
	public string[] szReward;

	// Token: 0x04001D05 RID: 7429
	public string sTitle;

	// Token: 0x04001D06 RID: 7430
	public string sDesc;
}

using System;
using System.Xml;

// Token: 0x02000204 RID: 516
public class JieRiOnline
{
	// Token: 0x06001680 RID: 5760 RVA: 0x000D072C File Offset: 0x000CE92C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sStartTime = xml.GetAttribute("starttime");
		this.sEndTime = xml.GetAttribute("endtime");
		this.szReward = xml.GetAttribute("rewarditem").Split(new char[]
		{
			','
		});
		this.sTitle = xml.GetAttribute("stitle");
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
	}

	// Token: 0x04001186 RID: 4486
	public int nId;

	// Token: 0x04001187 RID: 4487
	public string sActName;

	// Token: 0x04001188 RID: 4488
	public string sIconName;

	// Token: 0x04001189 RID: 4489
	public string sStartTime;

	// Token: 0x0400118A RID: 4490
	public string sEndTime;

	// Token: 0x0400118B RID: 4491
	public string[] szReward;

	// Token: 0x0400118C RID: 4492
	public string sTitle;

	// Token: 0x0400118D RID: 4493
	public string sDesc;
}

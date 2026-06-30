using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200049B RID: 1179
public class WeekChargeData
{
	// Token: 0x06002DB5 RID: 11701 RVA: 0x00193470 File Offset: 0x00191670
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sDescTxt = xml.GetAttribute("desc");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			WeekChargeReward weekChargeReward = new WeekChargeReward(xml2);
			this.allChargeReward.Add(weekChargeReward.nWeek, weekChargeReward);
		}
	}

	// Token: 0x040026F4 RID: 9972
	public int nId;

	// Token: 0x040026F5 RID: 9973
	public string sActName;

	// Token: 0x040026F6 RID: 9974
	public string sDescTxt;

	// Token: 0x040026F7 RID: 9975
	public Dictionary<int, WeekChargeReward> allChargeReward = new Dictionary<int, WeekChargeReward>();
}

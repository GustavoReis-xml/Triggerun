using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200023D RID: 573
public class MatchNaRewardActData
{
	// Token: 0x06001A42 RID: 6722 RVA: 0x000F9130 File Offset: 0x000F7330
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sActDesc1 = xml.GetAttribute("desc1").Replace("\\n", "\n");
		this.sActDesc2 = xml.GetAttribute("desc2").Replace("\\n", "\n");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "m";
			if (flag)
			{
				MatchNaRewardData matchNaRewardData = new MatchNaRewardData(xmlElement);
				this.szReward.Add(matchNaRewardData.nId, matchNaRewardData);
			}
		}
	}

	// Token: 0x040014C9 RID: 5321
	public int nId;

	// Token: 0x040014CA RID: 5322
	public string sActName;

	// Token: 0x040014CB RID: 5323
	public string sIconName;

	// Token: 0x040014CC RID: 5324
	public string sActDesc1;

	// Token: 0x040014CD RID: 5325
	public string sActDesc2;

	// Token: 0x040014CE RID: 5326
	public Dictionary<int, MatchNaRewardData> szReward = new Dictionary<int, MatchNaRewardData>();
}

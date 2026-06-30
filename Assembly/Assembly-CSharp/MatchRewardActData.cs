using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200023F RID: 575
public class MatchRewardActData
{
	// Token: 0x06001A45 RID: 6725 RVA: 0x000F9310 File Offset: 0x000F7510
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
				MatchRewardData matchRewardData = new MatchRewardData(xmlElement);
				this.szReward.Add(matchRewardData.nId, matchRewardData);
			}
		}
	}

	// Token: 0x040014D5 RID: 5333
	public int nId;

	// Token: 0x040014D6 RID: 5334
	public string sActName;

	// Token: 0x040014D7 RID: 5335
	public string sIconName;

	// Token: 0x040014D8 RID: 5336
	public string sActDesc1;

	// Token: 0x040014D9 RID: 5337
	public string sActDesc2;

	// Token: 0x040014DA RID: 5338
	public Dictionary<int, MatchRewardData> szReward = new Dictionary<int, MatchRewardData>();
}

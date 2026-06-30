using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200049E RID: 1182
public class WeekendOnlineData
{
	// Token: 0x06002DBF RID: 11711 RVA: 0x00193BA4 File Offset: 0x00191DA4
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sDescText = xml.GetAttribute("desc").Replace("\\n", "\n");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "t";
			if (flag)
			{
				WeekendOnlineReward weekendOnlineReward = new WeekendOnlineReward(xmlElement);
				this.szReward.Add(weekendOnlineReward.nIndex, weekendOnlineReward);
			}
		}
	}

	// Token: 0x04002701 RID: 9985
	public int nId;

	// Token: 0x04002702 RID: 9986
	public string sActName;

	// Token: 0x04002703 RID: 9987
	public string sIconName;

	// Token: 0x04002704 RID: 9988
	public string sDescText;

	// Token: 0x04002705 RID: 9989
	public Dictionary<int, WeekendOnlineReward> szReward = new Dictionary<int, WeekendOnlineReward>();
}

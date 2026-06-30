using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000315 RID: 789
[Serializable]
public class SignXMLData
{
	// Token: 0x06002645 RID: 9797 RVA: 0x0015D1D8 File Offset: 0x0015B3D8
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			OneMonthSignData oneMonthSignData = new OneMonthSignData();
			oneMonthSignData.Init(xml2);
			this.AllReward.Add(oneMonthSignData.id, oneMonthSignData);
		}
	}

	// Token: 0x04002212 RID: 8722
	public int id;

	// Token: 0x04002213 RID: 8723
	public string name;

	// Token: 0x04002214 RID: 8724
	public Dictionary<int, OneMonthSignData> AllReward = new Dictionary<int, OneMonthSignData>();
}

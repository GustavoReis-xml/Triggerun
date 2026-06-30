using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020004A9 RID: 1193
public class WinterActData
{
	// Token: 0x06002DFE RID: 11774 RVA: 0x00196654 File Offset: 0x00194854
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.nActItemId = int.Parse(xml.GetAttribute("itemid"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "e";
			if (flag)
			{
				WinterExData winterExData = new WinterExData(xmlElement);
				this.szExchange.Add(winterExData.nIndex, winterExData);
			}
			bool flag2 = xmlElement.Name == "t";
			if (flag2)
			{
				WinterTarData winterTarData = new WinterTarData(xmlElement);
				this.szTarget.Add(winterTarData.nIndex, winterTarData);
			}
		}
	}

	// Token: 0x04002753 RID: 10067
	public int nId;

	// Token: 0x04002754 RID: 10068
	public string sActName;

	// Token: 0x04002755 RID: 10069
	public string sIconName;

	// Token: 0x04002756 RID: 10070
	public int nActItemId;

	// Token: 0x04002757 RID: 10071
	public Dictionary<int, WinterExData> szExchange = new Dictionary<int, WinterExData>();

	// Token: 0x04002758 RID: 10072
	public Dictionary<int, WinterTarData> szTarget = new Dictionary<int, WinterTarData>();
}

using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000F7 RID: 247
public class DiscountItemData
{
	// Token: 0x060008CA RID: 2250 RVA: 0x0004A5D8 File Offset: 0x000487D8
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "s";
			if (flag)
			{
				DiscountInfo discountInfo = new DiscountInfo(xmlElement);
				this.szAllInfo.Add(discountInfo.nId, discountInfo);
			}
		}
	}

	// Token: 0x0400065B RID: 1627
	public int nId;

	// Token: 0x0400065C RID: 1628
	public string sActName;

	// Token: 0x0400065D RID: 1629
	public string sIconName;

	// Token: 0x0400065E RID: 1630
	public Dictionary<int, DiscountInfo> szAllInfo = new Dictionary<int, DiscountInfo>();
}

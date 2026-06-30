using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020004AD RID: 1197
public class WishTreeData
{
	// Token: 0x06002E0A RID: 11786 RVA: 0x001977C0 File Offset: 0x001959C0
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.nZuanshi = int.Parse(xml.GetAttribute("zuanshi"));
		this.nWishgold = int.Parse(xml.GetAttribute("wishgold"));
		this.extraReward = xml.GetAttribute("extrareward").Split(new char[]
		{
			','
		});
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "w";
			if (flag)
			{
				WishTreeReward wishTreeReward = new WishTreeReward(xmlElement);
				this.allRewardData.Add(wishTreeReward.nId, wishTreeReward);
			}
		}
	}

	// Token: 0x04002771 RID: 10097
	public int nId;

	// Token: 0x04002772 RID: 10098
	public string sActName;

	// Token: 0x04002773 RID: 10099
	public int nZuanshi;

	// Token: 0x04002774 RID: 10100
	public int nWishgold;

	// Token: 0x04002775 RID: 10101
	public string[] extraReward;

	// Token: 0x04002776 RID: 10102
	public Dictionary<int, WishTreeReward> allRewardData = new Dictionary<int, WishTreeReward>();
}

using System;
using System.Xml;

// Token: 0x020004AE RID: 1198
public class WishTreeReward
{
	// Token: 0x06002E0C RID: 11788 RVA: 0x001978D4 File Offset: 0x00195AD4
	public WishTreeReward(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sNameTxt = xml.GetAttribute("name");
		this.szBuyItem = xml.GetAttribute("itemdata").Split(new char[]
		{
			','
		});
		this.nCost = int.Parse(xml.GetAttribute("costwish"));
		this.szReward = xml.GetAttribute("rewarditem").Split(new char[]
		{
			','
		});
	}

	// Token: 0x04002777 RID: 10103
	public int nId;

	// Token: 0x04002778 RID: 10104
	public string sNameTxt;

	// Token: 0x04002779 RID: 10105
	public string[] szBuyItem;

	// Token: 0x0400277A RID: 10106
	public int nCost;

	// Token: 0x0400277B RID: 10107
	public string[] szReward;
}

using System;
using System.Xml;

// Token: 0x0200011E RID: 286
public class ExchangeInfo
{
	// Token: 0x06000A5A RID: 2650 RVA: 0x0005D0DC File Offset: 0x0005B2DC
	public void Init(XmlElement xml)
	{
		this.nIdx = int.Parse(xml.GetAttribute("id"));
		this.sTitle = xml.GetAttribute("name");
		this.nItemId = int.Parse(xml.GetAttribute("itemid"));
		this.itemData = fGameManager.mloadConfig.AllItemData[this.nItemId];
		this.nPerNum = int.Parse(xml.GetAttribute("pernum"));
		this.nExItemId = int.Parse(xml.GetAttribute("exchangeitem"));
		this.exItemData = fGameManager.mloadConfig.AllItemData[this.nExItemId];
		this.nExNum = int.Parse(xml.GetAttribute("neednum"));
		this.sIcon = xml.GetAttribute("icon");
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
	}

	// Token: 0x04000860 RID: 2144
	public int nIdx;

	// Token: 0x04000861 RID: 2145
	public string sTitle;

	// Token: 0x04000862 RID: 2146
	public int nItemId;

	// Token: 0x04000863 RID: 2147
	public int nPerNum;

	// Token: 0x04000864 RID: 2148
	public int nExItemId;

	// Token: 0x04000865 RID: 2149
	public int nExNum;

	// Token: 0x04000866 RID: 2150
	public string sIcon;

	// Token: 0x04000867 RID: 2151
	public string sDesc;

	// Token: 0x04000868 RID: 2152
	public ItemData itemData;

	// Token: 0x04000869 RID: 2153
	public ItemData exItemData;
}

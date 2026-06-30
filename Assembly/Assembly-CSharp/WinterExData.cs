using System;
using System.Xml;

// Token: 0x020004AB RID: 1195
public class WinterExData
{
	// Token: 0x06002E08 RID: 11784 RVA: 0x00197630 File Offset: 0x00195830
	public WinterExData(XmlElement xml)
	{
		this.nIndex = int.Parse(xml.GetAttribute("id"));
		this.sIcon = xml.GetAttribute("icon");
		this.nNeedNum = int.Parse(xml.GetAttribute("neednum"));
		this.szExchange = xml.GetAttribute("exchangeitem").Split(new char[]
		{
			','
		});
		this.nMaxNum = int.Parse(xml.GetAttribute("maxnum"));
	}

	// Token: 0x04002767 RID: 10087
	public int nIndex;

	// Token: 0x04002768 RID: 10088
	public string sIcon;

	// Token: 0x04002769 RID: 10089
	public int nNeedNum;

	// Token: 0x0400276A RID: 10090
	public int nMaxNum;

	// Token: 0x0400276B RID: 10091
	public string[] szExchange;
}

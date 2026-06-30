using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000226 RID: 550
public class LuckyDrawData
{
	// Token: 0x060018DA RID: 6362 RVA: 0x000E8300 File Offset: 0x000E6500
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.costNum = xml.GetAttribute("costnum").Split(new char[]
		{
			','
		});
		this.totalRate = int.Parse(xml.GetAttribute("totalrate"));
		this.luckRate = int.Parse(xml.GetAttribute("luckyrate"));
		this.sIconName = xml.GetAttribute("bgimg");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "l";
			if (flag)
			{
				LuckyDrawReward luckyDrawReward = new LuckyDrawReward();
				luckyDrawReward.Init(xmlElement);
				this.szLuckyDrawReward.Add(luckyDrawReward.nIndex, luckyDrawReward);
			}
		}
	}

	// Token: 0x0400137F RID: 4991
	public int nId;

	// Token: 0x04001380 RID: 4992
	public int totalRate;

	// Token: 0x04001381 RID: 4993
	public int luckRate;

	// Token: 0x04001382 RID: 4994
	public string sIconName;

	// Token: 0x04001383 RID: 4995
	public string[] costNum;

	// Token: 0x04001384 RID: 4996
	public Dictionary<int, LuckyDrawReward> szLuckyDrawReward = new Dictionary<int, LuckyDrawReward>();
}

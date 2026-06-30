using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000199 RID: 409
public class HeroLimitBuyData
{
	// Token: 0x0600133D RID: 4925 RVA: 0x000B7EBC File Offset: 0x000B60BC
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sIconName = xml.GetAttribute("bgimg");
		this.sNoteDesc = xml.GetAttribute("notedesc").Replace("\\n", "\n");
		this.sBtnTxt = xml.GetAttribute("name");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "h";
			if (flag)
			{
				HeroLimitBuyInfo heroLimitBuyInfo = new HeroLimitBuyInfo();
				heroLimitBuyInfo.Init(xmlElement);
				this.allLimitBuyInfo.Add(heroLimitBuyInfo.sKey, heroLimitBuyInfo);
			}
		}
	}

	// Token: 0x04000EF8 RID: 3832
	public int nId;

	// Token: 0x04000EF9 RID: 3833
	public string sIconName;

	// Token: 0x04000EFA RID: 3834
	public string sNoteDesc;

	// Token: 0x04000EFB RID: 3835
	public string sBtnTxt;

	// Token: 0x04000EFC RID: 3836
	public Dictionary<string, HeroLimitBuyInfo> allLimitBuyInfo = new Dictionary<string, HeroLimitBuyInfo>();
}

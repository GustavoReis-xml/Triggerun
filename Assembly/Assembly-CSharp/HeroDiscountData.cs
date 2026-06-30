using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000178 RID: 376
public class HeroDiscountData
{
	// Token: 0x06001175 RID: 4469 RVA: 0x000A3B4C File Offset: 0x000A1D4C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sIconName = xml.GetAttribute("bgimg");
		this.sActName = xml.GetAttribute("name");
		this.sNoteDesc = xml.GetAttribute("notedesc").Replace("\\n", "\n");
		this.sBtnTxt = xml.GetAttribute("btntxt");
		this.szBtnUrl = xml.GetAttribute("btnurl").Split(new char[]
		{
			','
		});
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "h";
			if (flag)
			{
				HeroDiscountInfo heroDiscountInfo = new HeroDiscountInfo();
				heroDiscountInfo.Init(xmlElement);
				this.allDiscountInfo.Add(heroDiscountInfo.sKey, heroDiscountInfo);
			}
		}
	}

	// Token: 0x04000DCE RID: 3534
	public int nId;

	// Token: 0x04000DCF RID: 3535
	public string sIconName;

	// Token: 0x04000DD0 RID: 3536
	public string sNoteDesc;

	// Token: 0x04000DD1 RID: 3537
	public string sBtnTxt;

	// Token: 0x04000DD2 RID: 3538
	public string sActName;

	// Token: 0x04000DD3 RID: 3539
	public string[] szBtnUrl;

	// Token: 0x04000DD4 RID: 3540
	public Dictionary<string, HeroDiscountInfo> allDiscountInfo = new Dictionary<string, HeroDiscountInfo>();
}

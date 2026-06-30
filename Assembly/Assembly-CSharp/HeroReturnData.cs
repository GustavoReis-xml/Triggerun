using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020001B1 RID: 433
public class HeroReturnData
{
	// Token: 0x06001405 RID: 5125 RVA: 0x000BCCBC File Offset: 0x000BAEBC
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sNoteDesc = xml.GetAttribute("notedesc").Replace("\\n", "\n");
		this.sBtnTxt = xml.GetAttribute("btntxt");
		this.szActDesc = xml.GetAttribute("actdesc").Split(new char[]
		{
			'|'
		});
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "h";
			if (flag)
			{
				string key = string.Format("{0}_{1}_{2}", xmlElement.GetAttribute("heroid"), xmlElement.GetAttribute("type"), xmlElement.GetAttribute("index"));
				string[] value = xmlElement.GetAttribute("returnitem").Split(new char[]
				{
					','
				});
				this.allHeroReturnInfo.Add(key, value);
			}
		}
	}

	// Token: 0x04000F59 RID: 3929
	public int nId;

	// Token: 0x04000F5A RID: 3930
	public string sActName;

	// Token: 0x04000F5B RID: 3931
	public string sIconName;

	// Token: 0x04000F5C RID: 3932
	public string sNoteDesc;

	// Token: 0x04000F5D RID: 3933
	public string sBtnTxt;

	// Token: 0x04000F5E RID: 3934
	public string[] szActDesc;

	// Token: 0x04000F5F RID: 3935
	public Dictionary<string, string[]> allHeroReturnInfo = new Dictionary<string, string[]>();
}

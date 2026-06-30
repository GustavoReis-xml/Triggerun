using System;
using System.Xml;

// Token: 0x0200018B RID: 395
public class HeroInfoBg
{
	// Token: 0x0600122F RID: 4655 RVA: 0x000AA60F File Offset: 0x000A880F
	public void Init(XmlElement xml)
	{
		this.nID = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.sUIName = xml.GetAttribute("uiname");
	}

	// Token: 0x04000E5E RID: 3678
	public int nID;

	// Token: 0x04000E5F RID: 3679
	public string sName;

	// Token: 0x04000E60 RID: 3680
	public string sUIName;
}

using System;
using System.Xml;

// Token: 0x02000279 RID: 633
public class OneHeroSkillData
{
	// Token: 0x06001D43 RID: 7491 RVA: 0x0010E6FC File Offset: 0x0010C8FC
	public void Init(XmlElement xml)
	{
		this.nSkillType = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.sSkillIco = xml.GetAttribute("icon");
		this.nSkillIndex = int.Parse(xml.GetAttribute("skillindex"));
		this.nIsOpen = int.Parse(xml.GetAttribute("isopen"));
	}

	// Token: 0x040019D8 RID: 6616
	public int nSkillType;

	// Token: 0x040019D9 RID: 6617
	public string sName;

	// Token: 0x040019DA RID: 6618
	public string sSkillIco;

	// Token: 0x040019DB RID: 6619
	public int nSkillIndex;

	// Token: 0x040019DC RID: 6620
	public int nIsOpen;
}

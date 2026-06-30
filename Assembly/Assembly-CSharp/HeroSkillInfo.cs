using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020001B8 RID: 440
public class HeroSkillInfo
{
	// Token: 0x1700029B RID: 667
	// (get) Token: 0x0600142F RID: 5167 RVA: 0x000BEB79 File Offset: 0x000BCD79
	public int key
	{
		get
		{
			return this.nHeroid;
		}
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x000BEB84 File Offset: 0x000BCD84
	public void Init(XmlElement xml)
	{
		this.nHeroid = int.Parse(xml.GetAttribute("heroid"));
		this.nStar = int.Parse(xml.GetAttribute("star"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "s";
			if (flag)
			{
				SkillInfoData skillInfoData = new SkillInfoData();
				skillInfoData.Init(xmlElement);
				this.szInfoData.Add(skillInfoData);
			}
		}
	}

	// Token: 0x04000F94 RID: 3988
	public int nHeroid;

	// Token: 0x04000F95 RID: 3989
	public int nStar;

	// Token: 0x04000F96 RID: 3990
	public List<SkillInfoData> szInfoData = new List<SkillInfoData>();
}

using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000167 RID: 359
[Serializable]
public class HeroAllSkillData
{
	// Token: 0x0600105C RID: 4188 RVA: 0x00098CE0 File Offset: 0x00096EE0
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("heroid"));
		this.sName = xml.GetAttribute("name");
		this.sIco = xml.GetAttribute("ico");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "s";
			if (flag)
			{
				OneHeroSkillData oneHeroSkillData = new OneHeroSkillData();
				oneHeroSkillData.Init(xmlElement);
				this.mHeroSkillData.Add(oneHeroSkillData.nSkillType, oneHeroSkillData);
				bool flag2 = !this.mSkillTypeData.ContainsKey(oneHeroSkillData.nSkillIndex);
				if (flag2)
				{
					this.mSkillTypeData.Add(oneHeroSkillData.nSkillIndex, oneHeroSkillData.nSkillType);
				}
			}
		}
	}

	// Token: 0x04000D14 RID: 3348
	public int nId;

	// Token: 0x04000D15 RID: 3349
	public string sName;

	// Token: 0x04000D16 RID: 3350
	public string sIco;

	// Token: 0x04000D17 RID: 3351
	public Dictionary<int, OneHeroSkillData> mHeroSkillData = new Dictionary<int, OneHeroSkillData>();

	// Token: 0x04000D18 RID: 3352
	public Dictionary<int, int> mSkillTypeData = new Dictionary<int, int>();
}

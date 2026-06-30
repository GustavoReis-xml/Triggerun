using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020001E5 RID: 485
public class HuDongHeroData
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x060015CF RID: 5583 RVA: 0x000CC0A4 File Offset: 0x000CA2A4
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000CC0AC File Offset: 0x000CA2AC
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("heroid"));
		this.m_sName = xml.GetAttribute("name");
		this.m_nZlType = int.Parse(xml.GetAttribute("zltype"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "r";
			if (flag)
			{
				this.AllHuDongSound.Add(int.Parse(xmlElement.GetAttribute("id")), xmlElement.GetAttribute("yinxiao").Split(new char[]
				{
					'|'
				}));
			}
			else
			{
				hudongData hudongData = new hudongData();
				hudongData.Init(xmlElement);
				bool flag2 = this.AllHuDongData.ContainsKey(hudongData.m_nType);
				if (flag2)
				{
					List<hudongData> list = this.AllHuDongData[hudongData.m_nType];
					list.Add(hudongData);
					this.AllHuDongData[hudongData.m_nType] = list;
				}
				else
				{
					List<hudongData> list2 = new List<hudongData>();
					list2.Add(hudongData);
					this.AllHuDongData.Add(hudongData.m_nType, list2);
				}
			}
		}
	}

	// Token: 0x040010E6 RID: 4326
	public int id;

	// Token: 0x040010E7 RID: 4327
	public string m_sName;

	// Token: 0x040010E8 RID: 4328
	public int m_nZlType;

	// Token: 0x040010E9 RID: 4329
	public Dictionary<int, List<hudongData>> AllHuDongData = new Dictionary<int, List<hudongData>>();

	// Token: 0x040010EA RID: 4330
	public Dictionary<int, string[]> AllHuDongSound = new Dictionary<int, string[]>();
}

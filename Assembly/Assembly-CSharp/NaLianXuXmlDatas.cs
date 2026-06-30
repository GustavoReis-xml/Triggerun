using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000250 RID: 592
[Serializable]
public class NaLianXuXmlDatas
{
	// Token: 0x06001A97 RID: 6807 RVA: 0x000FD79C File Offset: 0x000FB99C
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bg");
		this.sTitle = xml.GetAttribute("stitle");
		this.sBottom = xml.GetAttribute("sbottom");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			NaLianXuXmlData naLianXuXmlData = new NaLianXuXmlData();
			naLianXuXmlData.id = int.Parse(xmlElement.GetAttribute("id"));
			naLianXuXmlData.denglunum = int.Parse(xmlElement.GetAttribute("denglunum"));
			naLianXuXmlData.name = xmlElement.GetAttribute("name");
			naLianXuXmlData.img = xmlElement.GetAttribute("img");
			naLianXuXmlData.msg = xmlElement.GetAttribute("msg");
			string attribute = xmlElement.GetAttribute("itemlist");
			bool flag = attribute != string.Empty;
			if (flag)
			{
				string[] array = attribute.Split(new char[]
				{
					'|'
				});
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					string text = array[i];
					bool flag2 = !(text == string.Empty);
					if (flag2)
					{
						string[] array2 = text.Split(new char[]
						{
							','
						});
						naLianXuXmlData.itemlist.Add(new ArrayList
						{
							int.Parse(array2[0]),
							int.Parse(array2[1]),
							int.Parse(array2[2])
						});
					}
				}
			}
			this.naLianXus.Add(naLianXuXmlData.denglunum, naLianXuXmlData);
		}
	}

	// Token: 0x04001563 RID: 5475
	public int id;

	// Token: 0x04001564 RID: 5476
	public string name;

	// Token: 0x04001565 RID: 5477
	public string sIconName;

	// Token: 0x04001566 RID: 5478
	public string sTitle;

	// Token: 0x04001567 RID: 5479
	public string sBottom;

	// Token: 0x04001568 RID: 5480
	public Dictionary<int, NaLianXuXmlData> naLianXus = new Dictionary<int, NaLianXuXmlData>();
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000102 RID: 258
public class DtsMapDoorData
{
	// Token: 0x06000930 RID: 2352 RVA: 0x0004E214 File Offset: 0x0004C414
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.loadmodel = xml.GetAttribute("loadmodel");
		string[] array = xml.GetAttribute("createpos").Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.szCreatePos.Add(int.Parse(array[i]));
		}
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "item";
			if (flag)
			{
				DtsMapItemData dtsMapItemData = new DtsMapItemData();
				dtsMapItemData.Init(xmlElement);
				this.mDtsMapItemData.Add(dtsMapItemData.nId, dtsMapItemData);
			}
		}
	}

	// Token: 0x040006A9 RID: 1705
	public int nId;

	// Token: 0x040006AA RID: 1706
	public string sName;

	// Token: 0x040006AB RID: 1707
	public string loadmodel;

	// Token: 0x040006AC RID: 1708
	public ArrayList szCreatePos = new ArrayList();

	// Token: 0x040006AD RID: 1709
	public Dictionary<int, DtsMapItemData> mDtsMapItemData = new Dictionary<int, DtsMapItemData>();
}

using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000107 RID: 263
[Serializable]
public class DtsMapXmlData
{
	// Token: 0x0600094E RID: 2382 RVA: 0x000505F8 File Offset: 0x0004E7F8
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("index"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "i";
			if (flag)
			{
				DtsMapDoorData dtsMapDoorData = new DtsMapDoorData();
				dtsMapDoorData.Init(xmlElement);
				this.mDtsMapDoorData.Add(dtsMapDoorData.nId, dtsMapDoorData);
			}
		}
	}

	// Token: 0x040006EA RID: 1770
	public int nId;

	// Token: 0x040006EB RID: 1771
	public Dictionary<int, DtsMapDoorData> mDtsMapDoorData = new Dictionary<int, DtsMapDoorData>();
}

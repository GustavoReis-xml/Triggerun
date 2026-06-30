using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000105 RID: 261
public class DtsMapItemData
{
	// Token: 0x06000941 RID: 2369 RVA: 0x0004F098 File Offset: 0x0004D298
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.loadmodel = xml.GetAttribute("loadmodel");
		this.nItemType = int.Parse(xml.GetAttribute("itemtype"));
		this.nIndex = int.Parse(xml.GetAttribute("index"));
		this.nCreateNum = int.Parse(xml.GetAttribute("createnum"));
		string[] array = xml.GetAttribute("randpos").Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < array2.Length; j++)
			{
				arrayList.Add(int.Parse(array2[j].ToString()));
			}
			this.szRandPos.Add(arrayList);
		}
	}

	// Token: 0x040006C3 RID: 1731
	public int nId;

	// Token: 0x040006C4 RID: 1732
	public string sName;

	// Token: 0x040006C5 RID: 1733
	public string loadmodel;

	// Token: 0x040006C6 RID: 1734
	public int nItemType;

	// Token: 0x040006C7 RID: 1735
	public int nIndex;

	// Token: 0x040006C8 RID: 1736
	public int nCreateNum;

	// Token: 0x040006C9 RID: 1737
	public List<ArrayList> szRandPos = new List<ArrayList>();
}

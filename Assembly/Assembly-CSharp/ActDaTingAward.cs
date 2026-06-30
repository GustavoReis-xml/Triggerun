using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000004 RID: 4
[Serializable]
public class ActDaTingAward
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
	public void Init(XmlElement xml)
	{
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
	public void InitData(XmlElement xml, bool isnew)
	{
		try
		{
			this.id = int.Parse(xml.GetAttribute("id"));
			this.name = xml.GetAttribute("name");
			this.acttype = int.Parse(xml.GetAttribute("acttype"));
			this.bgimg = xml.GetAttribute("bgimg");
			foreach (object obj in xml.ChildNodes)
			{
				XmlElement xml2 = (XmlElement)obj;
				AItemData aitemData = new AItemData();
				aitemData.Init(xml2, this.acttype);
				this.szActList.Add(aitemData.index, aitemData);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
	}

	// Token: 0x04000002 RID: 2
	public int id;

	// Token: 0x04000003 RID: 3
	public string name;

	// Token: 0x04000004 RID: 4
	public int acttype;

	// Token: 0x04000005 RID: 5
	public int awardid;

	// Token: 0x04000006 RID: 6
	public string bgimg = string.Empty;

	// Token: 0x04000007 RID: 7
	public Dictionary<int, AItemData> szActList = new Dictionary<int, AItemData>();
}

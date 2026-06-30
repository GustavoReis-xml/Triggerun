using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000125 RID: 293
[Serializable]
public class FbOpenSetData
{
	// Token: 0x06000AB4 RID: 2740 RVA: 0x00062564 File Offset: 0x00060764
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		string[] array = xml.GetAttribute("fbtype").Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				this.szFbType.Add(int.Parse(array[i]));
			}
		}
	}

	// Token: 0x040008DE RID: 2270
	public int nId;

	// Token: 0x040008DF RID: 2271
	public string sName;

	// Token: 0x040008E0 RID: 2272
	public List<int> szFbType = new List<int>();
}

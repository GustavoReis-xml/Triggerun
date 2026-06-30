using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000285 RID: 645
public class OpenHeroActData
{
	// Token: 0x06001DA0 RID: 7584 RVA: 0x00113B80 File Offset: 0x00111D80
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		string[] array = xml.GetAttribute("opendata").Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				int item = int.Parse(array[i]);
				this.szOpenData.Add(item);
			}
		}
	}

	// Token: 0x04001A8C RID: 6796
	public int nId;

	// Token: 0x04001A8D RID: 6797
	public string sActName;

	// Token: 0x04001A8E RID: 6798
	public string sIconName;

	// Token: 0x04001A8F RID: 6799
	public List<int> szOpenData = new List<int>();
}

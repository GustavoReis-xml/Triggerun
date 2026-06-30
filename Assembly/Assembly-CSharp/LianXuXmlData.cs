using System;
using System.Collections;
using System.Xml;

// Token: 0x02000218 RID: 536
[Serializable]
public class LianXuXmlData
{
	// Token: 0x06001735 RID: 5941 RVA: 0x000D90B0 File Offset: 0x000D72B0
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.denglunum = int.Parse(xml.GetAttribute("denglunum"));
		this.name = xml.GetAttribute("name");
		this.img = xml.GetAttribute("img");
		this.msg = xml.GetAttribute("msg");
		string attribute = xml.GetAttribute("itemlist");
		bool flag = !(attribute != string.Empty);
		if (!flag)
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
					this.itemlist.Add(new ArrayList
					{
						int.Parse(array2[0]),
						int.Parse(array2[1]),
						int.Parse(array2[2])
					});
				}
			}
		}
	}

	// Token: 0x0400126E RID: 4718
	public int id;

	// Token: 0x0400126F RID: 4719
	public string name;

	// Token: 0x04001270 RID: 4720
	public ArrayList itemlist = new ArrayList();

	// Token: 0x04001271 RID: 4721
	public int denglunum;

	// Token: 0x04001272 RID: 4722
	public string img;

	// Token: 0x04001273 RID: 4723
	public string msg;
}

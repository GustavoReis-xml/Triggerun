using System;
using System.Collections;
using System.Xml;

// Token: 0x0200022A RID: 554
public class LvRewardXmlData
{
	// Token: 0x060018F6 RID: 6390 RVA: 0x000E9ECC File Offset: 0x000E80CC
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.lv = int.Parse(xml.GetAttribute("get_lv"));
		this.name = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
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

	// Token: 0x040013AE RID: 5038
	public int id;

	// Token: 0x040013AF RID: 5039
	public string name;

	// Token: 0x040013B0 RID: 5040
	public string sIconName;

	// Token: 0x040013B1 RID: 5041
	public ArrayList itemlist = new ArrayList();

	// Token: 0x040013B2 RID: 5042
	public int lv;

	// Token: 0x040013B3 RID: 5043
	public ArrayList checkid = new ArrayList();
}

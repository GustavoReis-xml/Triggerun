using System;
using System.Collections;
using System.Xml;

// Token: 0x0200027D RID: 637
public class OneMonthSignData
{
	// Token: 0x06001D64 RID: 7524 RVA: 0x0010FE60 File Offset: 0x0010E060
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("index"));
		this.name = xml.GetAttribute("name");
		this.img = xml.GetAttribute("img");
		this.msg = xml.GetAttribute("msg");
		this.signnum = int.Parse(xml.GetAttribute("signnum"));
		string attribute = xml.GetAttribute("awarditem");
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

	// Token: 0x04001A10 RID: 6672
	public int id;

	// Token: 0x04001A11 RID: 6673
	public string name;

	// Token: 0x04001A12 RID: 6674
	public string msg;

	// Token: 0x04001A13 RID: 6675
	public string img;

	// Token: 0x04001A14 RID: 6676
	public int signnum;

	// Token: 0x04001A15 RID: 6677
	public ArrayList itemlist = new ArrayList();
}

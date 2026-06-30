using System;
using System.Collections;
using System.Xml;

// Token: 0x02000312 RID: 786
[Serializable]
public class ShouChongXmlData
{
	// Token: 0x06002629 RID: 9769 RVA: 0x0015BB3C File Offset: 0x00159D3C
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.moneynum = int.Parse(xml.GetAttribute("money"));
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
						int.Parse(array2[0].ToString()),
						int.Parse(array2[1].ToString()),
						int.Parse(array2[2].ToString())
					});
				}
			}
		}
	}

	// Token: 0x040021EC RID: 8684
	public int id;

	// Token: 0x040021ED RID: 8685
	public string name;

	// Token: 0x040021EE RID: 8686
	public string sIconName;

	// Token: 0x040021EF RID: 8687
	public int moneynum;

	// Token: 0x040021F0 RID: 8688
	public ArrayList itemlist = new ArrayList();
}

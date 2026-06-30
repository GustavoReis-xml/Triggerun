using System;
using System.Collections;
using System.Xml;

// Token: 0x020004A1 RID: 1185
public class WeekLoginData
{
	// Token: 0x06002DC9 RID: 11721 RVA: 0x001943DC File Offset: 0x001925DC
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "week";
			if (flag)
			{
				int num = int.Parse(xmlElement.GetAttribute("id"));
				string[] array = xmlElement.GetAttribute("reward").Split(new char[]
				{
					','
				});
				ArrayList arrayList = new ArrayList();
				int num2 = 0;
				while (num2 < array.Length && !string.IsNullOrEmpty(array[num2]))
				{
					arrayList.Add(int.Parse(array[num2]));
					num2++;
				}
				this.szWeekReward.Add(num);
				this.szWeekReward.Add((ArrayList)arrayList.Clone());
			}
		}
	}

	// Token: 0x04002712 RID: 10002
	public int nId;

	// Token: 0x04002713 RID: 10003
	public string sActName;

	// Token: 0x04002714 RID: 10004
	public string sIconName;

	// Token: 0x04002715 RID: 10005
	public ArrayList szWeekReward = new ArrayList();
}

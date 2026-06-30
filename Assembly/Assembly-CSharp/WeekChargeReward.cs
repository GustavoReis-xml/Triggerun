using System;
using System.Collections;
using System.Xml;

// Token: 0x0200049C RID: 1180
public class WeekChargeReward
{
	// Token: 0x06002DB7 RID: 11703 RVA: 0x00193530 File Offset: 0x00191730
	public WeekChargeReward(XmlElement xml)
	{
		this.nWeek = int.Parse(xml.GetAttribute("id"));
		this.sTitle = xml.GetAttribute("title");
		string[] array = xml.GetAttribute("reward").Split(new char[]
		{
			'#'
		});
		foreach (string text in array)
		{
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				ArrayList value = new ArrayList
				{
					int.Parse(array3[0]),
					array3[1],
					array3[2],
					array3[3].Split(new char[]
					{
						'|'
					})
				};
				this.szReward.Add(value);
			}
		}
	}

	// Token: 0x040026F8 RID: 9976
	public int nWeek;

	// Token: 0x040026F9 RID: 9977
	public string sTitle;

	// Token: 0x040026FA RID: 9978
	public ArrayList szReward = new ArrayList();
}

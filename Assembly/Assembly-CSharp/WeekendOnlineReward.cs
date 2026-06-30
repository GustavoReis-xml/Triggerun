using System;
using System.Xml;

// Token: 0x0200049F RID: 1183
public class WeekendOnlineReward
{
	// Token: 0x06002DC1 RID: 11713 RVA: 0x00193C9C File Offset: 0x00191E9C
	public WeekendOnlineReward(XmlElement xml)
	{
		this.nIndex = int.Parse(xml.GetAttribute("id"));
		this.sStartTime = xml.GetAttribute("start");
		this.sEndTime = xml.GetAttribute("end");
		this.nOnlineDay = int.Parse(xml.GetAttribute("weekday"));
		this.sOnlineTitle = xml.GetAttribute("title");
		this.szReward = xml.GetAttribute("reward").Split(new char[]
		{
			','
		});
		bool flag = this.sOnlineTitle.Contains("{1}");
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(this.sStartTime);
			this.sOnlineTitle = string.Format(this.sOnlineTitle, Fun.GetHZWeekDay(this.nOnlineDay), dateTime.ToString("HH:mm"));
		}
	}

	// Token: 0x04002706 RID: 9990
	public int nIndex;

	// Token: 0x04002707 RID: 9991
	public string sStartTime;

	// Token: 0x04002708 RID: 9992
	public string sEndTime;

	// Token: 0x04002709 RID: 9993
	public int nOnlineDay;

	// Token: 0x0400270A RID: 9994
	public string sOnlineTitle;

	// Token: 0x0400270B RID: 9995
	public string[] szReward;
}

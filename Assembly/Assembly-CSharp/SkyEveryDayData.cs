using System;
using System.Xml;

// Token: 0x020003D7 RID: 983
public class SkyEveryDayData
{
	// Token: 0x06002A0D RID: 10765 RVA: 0x00171E04 File Offset: 0x00170004
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.nMatchNum = int.Parse(xml.GetAttribute("matchnum"));
		this.nGoldNum = int.Parse(xml.GetAttribute("gold"));
		this.sMatchTitle = xml.GetAttribute("matchtitle");
		bool flag = this.sMatchTitle.Contains("{0}");
		if (flag)
		{
			this.sMatchTitle = string.Format(this.sMatchTitle, this.nMatchNum);
		}
		this.sMatchDesc = xml.GetAttribute("matchdesc").Replace("\\n", "\n");
		bool flag2 = this.sMatchDesc.Contains("{0}");
		if (flag2)
		{
			this.sMatchDesc = string.Format(this.sMatchDesc, this.nMatchNum);
		}
		this.sMatchNumDesc = xml.GetAttribute("matchnumdesc").Replace("\\n", "\n");
		this.sStartTime = xml.GetAttribute("expstart");
		this.sEndTime = xml.GetAttribute("expend");
		this.sExpTitle = xml.GetAttribute("exptitle");
		this.sExpDesc = xml.GetAttribute("expdesc").Replace("\\n", "\n");
		bool flag3 = this.sExpDesc.Contains("{1}");
		if (flag3)
		{
			this.sExpDesc = string.Format(this.sExpDesc, DateTime.Parse(this.sStartTime).ToString("HH:mm"), DateTime.Parse(this.sEndTime).ToString("HH:mm"));
		}
	}

	// Token: 0x0400237C RID: 9084
	public int nId;

	// Token: 0x0400237D RID: 9085
	public string sActName;

	// Token: 0x0400237E RID: 9086
	public string sIconName;

	// Token: 0x0400237F RID: 9087
	public int nMatchNum;

	// Token: 0x04002380 RID: 9088
	public string sMatchNumDesc;

	// Token: 0x04002381 RID: 9089
	public int nGoldNum;

	// Token: 0x04002382 RID: 9090
	public string sMatchTitle;

	// Token: 0x04002383 RID: 9091
	public string sMatchDesc;

	// Token: 0x04002384 RID: 9092
	public string sStartTime;

	// Token: 0x04002385 RID: 9093
	public string sEndTime;

	// Token: 0x04002386 RID: 9094
	public string sExpTitle;

	// Token: 0x04002387 RID: 9095
	public string sExpDesc;
}

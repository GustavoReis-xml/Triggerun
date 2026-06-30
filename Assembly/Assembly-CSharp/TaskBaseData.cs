using System;
using System.Collections;
using System.Xml;

// Token: 0x020003F9 RID: 1017
public class TaskBaseData
{
	// Token: 0x06002AB9 RID: 10937 RVA: 0x00178708 File Offset: 0x00176908
	public void Init(XmlElement xml)
	{
		this.m_nTaskId = int.Parse(xml.GetAttribute("id"));
		this.m_sTaskName = xml.GetAttribute("taskname");
		this.m_nTaskType = int.Parse(xml.GetAttribute("maintype"));
		this.m_nLevel = int.Parse(xml.GetAttribute("level"));
		this.m_nPretaskid = int.Parse(xml.GetAttribute("pretaskid"));
		this.m_sIco = xml.GetAttribute("ico");
		this.m_nZuanShi = int.Parse(xml.GetAttribute("gains_zuanshi"));
		this.m_nMoney = int.Parse(xml.GetAttribute("gain_money"));
		this.m_szReward = ItemData.GetArrlist(xml.GetAttribute("gains_nor"), '|', false, '#');
		this.m_nPinZhi = int.Parse(xml.GetAttribute("lvlhard"));
		this.m_nIsHide = (int.Parse(xml.GetAttribute("ishide")) == 1);
		this.sbaseXml = xml;
	}

	// Token: 0x0400243F RID: 9279
	public int m_nTaskId;

	// Token: 0x04002440 RID: 9280
	public int m_nTaskType;

	// Token: 0x04002441 RID: 9281
	public string m_sTaskName;

	// Token: 0x04002442 RID: 9282
	public int m_nLevel;

	// Token: 0x04002443 RID: 9283
	public int m_nlasttime;

	// Token: 0x04002444 RID: 9284
	public int m_nEndNpcId;

	// Token: 0x04002445 RID: 9285
	public string m_sIco = string.Empty;

	// Token: 0x04002446 RID: 9286
	public XmlElement sbaseXml;

	// Token: 0x04002447 RID: 9287
	public int m_nMoney;

	// Token: 0x04002448 RID: 9288
	public int m_nZuanShi;

	// Token: 0x04002449 RID: 9289
	public ArrayList m_szReward;

	// Token: 0x0400244A RID: 9290
	public int m_nPinZhi;

	// Token: 0x0400244B RID: 9291
	public int m_nPretaskid;

	// Token: 0x0400244C RID: 9292
	public bool m_nIsHide;
}

using System;
using System.Xml;

// Token: 0x02000305 RID: 773
public class SevenTargetData
{
	// Token: 0x0600253F RID: 9535 RVA: 0x0014FAB4 File Offset: 0x0014DCB4
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nDay = int.Parse(xml.GetAttribute("day"));
		this.nType = int.Parse(xml.GetAttribute("type"));
		this.nTargetNum = int.Parse(xml.GetAttribute("nparam"));
		this.nModeType = int.Parse(xml.GetAttribute("mparam"));
		this.szReward = xml.GetAttribute("reward").Split(new char[]
		{
			','
		});
		this.sTitleDesc = xml.GetAttribute("title").Replace("\\n", "\n");
		this.sTargetDesc = xml.GetAttribute("targetdesc").Replace("\\n", "\n");
		this.nUnlock = int.Parse(xml.GetAttribute("jiesuo"));
		bool flag = this.sTargetDesc.Contains("{1}");
		if (flag)
		{
			this.sTargetDesc = string.Format(this.sTargetDesc, this.nTargetNum, this.GetModeStr());
		}
		bool flag2 = this.sTargetDesc.Contains("{0}");
		if (flag2)
		{
			this.sTargetDesc = string.Format(this.sTargetDesc, this.nTargetNum);
		}
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x0014FC14 File Offset: 0x0014DE14
	private string GetModeStr()
	{
		string result = string.Empty;
		switch (this.nModeType)
		{
		case 1:
			result = Language.GetLang(8001, new object[0]);
			break;
		case 2:
			result = Language.GetLang(8002, new object[0]);
			break;
		case 3:
			result = Language.GetLang(8003, new object[0]);
			break;
		case 4:
			result = Language.GetLang(8004, new object[0]);
			break;
		case 5:
			result = Language.GetLang(8005, new object[0]);
			break;
		case 7:
			result = Language.GetLang(8024, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x040020CD RID: 8397
	public const int SEVENDAY_TARGET1 = 1;

	// Token: 0x040020CE RID: 8398
	public const int SEVENDAY_TARGET2 = 2;

	// Token: 0x040020CF RID: 8399
	public const int SEVENDAY_TARGET3 = 3;

	// Token: 0x040020D0 RID: 8400
	public const int SEVENDAY_TARGET4 = 4;

	// Token: 0x040020D1 RID: 8401
	public const int SEVENDAY_TARGET5 = 5;

	// Token: 0x040020D2 RID: 8402
	public const int SEVENDAY_TARGET6 = 6;

	// Token: 0x040020D3 RID: 8403
	public const int SEVENDAY_TARGET7 = 7;

	// Token: 0x040020D4 RID: 8404
	public const int SEVENDAY_TARGET8 = 8;

	// Token: 0x040020D5 RID: 8405
	public const int SEVENDAY_TARGET9 = 9;

	// Token: 0x040020D6 RID: 8406
	public const int SEVENDAY_TARGET10 = 10;

	// Token: 0x040020D7 RID: 8407
	public const int SEVENDAY_TARGET11 = 11;

	// Token: 0x040020D8 RID: 8408
	public const int SEVENDAY_TARGET12 = 12;

	// Token: 0x040020D9 RID: 8409
	public const int SEVENDAY_TARGET13 = 13;

	// Token: 0x040020DA RID: 8410
	public const int SEVENDAY_TARGET14 = 14;

	// Token: 0x040020DB RID: 8411
	public int nId;

	// Token: 0x040020DC RID: 8412
	public int nDay;

	// Token: 0x040020DD RID: 8413
	public int nType;

	// Token: 0x040020DE RID: 8414
	public int nTargetNum;

	// Token: 0x040020DF RID: 8415
	public int nModeType;

	// Token: 0x040020E0 RID: 8416
	public string[] szReward;

	// Token: 0x040020E1 RID: 8417
	public string sTitleDesc;

	// Token: 0x040020E2 RID: 8418
	public string sTargetDesc;

	// Token: 0x040020E3 RID: 8419
	public int nUnlock;
}

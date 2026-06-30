using System;

// Token: 0x02000162 RID: 354
public static class GuildUserState
{
	// Token: 0x06001030 RID: 4144 RVA: 0x000967B8 File Offset: 0x000949B8
	public static string GetOnLineStr(int nState, string sLogOffTime)
	{
		string result = string.Empty;
		if (nState != 0)
		{
			if (nState == 1)
			{
				result = "<color='#00ff00'>" + Language.GetLang(38042, new object[0]) + "</color>";
			}
		}
		else
		{
			int sec = -Fun.GetEndCDTime(sLogOffTime);
			result = "<color='#625D5D'>" + Language.GetLang(38067, new object[]
			{
				Fun.SecToFullTimeHStr(sec)
			}) + "</color>";
		}
		return result;
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00096838 File Offset: 0x00094A38
	public static int GetNeedHuoYue(int nLvl)
	{
		return nLvl * nLvl * nLvl * 5000 + nLvl * nLvl * 10000 + nLvl * 10000 + 5000;
	}

	// Token: 0x04000CE4 RID: 3300
	public const int Guild_Create_Money = 200;

	// Token: 0x04000CE5 RID: 3301
	public const int Guild_Create_ZunShiMoney = 100;

	// Token: 0x04000CE6 RID: 3302
	public const int Guild_Create_Lv = 10;

	// Token: 0x04000CE7 RID: 3303
	public const int Guild_Join_Lv = 10;
}

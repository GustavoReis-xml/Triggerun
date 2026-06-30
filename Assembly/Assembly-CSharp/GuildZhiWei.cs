using System;

// Token: 0x02000163 RID: 355
public static class GuildZhiWei
{
	// Token: 0x06001032 RID: 4146 RVA: 0x00096870 File Offset: 0x00094A70
	public static string GetZhiWeiStr(int nZhiWei)
	{
		string result = string.Empty;
		switch (nZhiWei)
		{
		case 0:
			result = Language.GetLang(38039, new object[0]);
			break;
		case 1:
			result = Language.GetLang(38040, new object[0]);
			break;
		case 2:
			result = Language.GetLang(38041, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x04000CE8 RID: 3304
	public const int GUILD_RANK_MEMBER = 0;

	// Token: 0x04000CE9 RID: 3305
	public const int GUILD_RANK_MATE = 1;

	// Token: 0x04000CEA RID: 3306
	public const int GUILD_RANK_CHAIRMAN = 2;
}

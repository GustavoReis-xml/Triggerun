using System;

// Token: 0x02000400 RID: 1024
public static class TaskState
{
	// Token: 0x06002AE6 RID: 10982 RVA: 0x00179ED4 File Offset: 0x001780D4
	public static string getTaskStateStr(int nstate)
	{
		if (!true)
		{
		}
		string result;
		switch (nstate)
		{
		case 1:
			result = "[" + Language.GetLang(109023, new object[0]) + "]";
			break;
		case 2:
			result = "[" + Language.GetLang(277001, new object[0]) + "]";
			break;
		case 3:
			result = "[fffc00][" + Language.GetLang(277002, new object[0]) + "][-]";
			break;
		default:
			result = string.Empty;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x0400247F RID: 9343
	public const int m_none = -1;

	// Token: 0x04002480 RID: 9344
	public const int m_nFinish = 0;

	// Token: 0x04002481 RID: 9345
	public const int m_ncanAccept = 1;

	// Token: 0x04002482 RID: 9346
	public const int m_nExecute = 2;

	// Token: 0x04002483 RID: 9347
	public const int m_nNoAccept = 3;

	// Token: 0x04002484 RID: 9348
	public const int m_nComplete = 4;
}

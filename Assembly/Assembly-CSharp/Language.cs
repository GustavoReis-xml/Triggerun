using System;
using System.Text.RegularExpressions;

// Token: 0x02000214 RID: 532
public class Language
{
	// Token: 0x06001717 RID: 5911 RVA: 0x000D6AD0 File Offset: 0x000D4CD0
	public static string GetServerLang(string sMsg)
	{
		Match match = Regex.Match(sMsg, Language.pattern);
		string text = sMsg;
		while (match.Success)
		{
			string value = match.Value;
			string s = value.Replace("{=", string.Empty).Replace("}", string.Empty);
			text = text.Replace(value, Language.GetLang(int.Parse(s), new object[0]));
			match = match.NextMatch();
		}
		return text;
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x000D6B4C File Offset: 0x000D4D4C
	public static string GetLang(int nMsgID, params object[] values)
	{
		bool flag = fGameManager.mloadConfig != null && fGameManager.mloadConfig.LangObj != null && fGameManager.mloadConfig.LangObj.ContainsKey(nMsgID);
		string result;
		if (flag)
		{
			result = string.Format(fGameManager.mloadConfig.LangObj[nMsgID], values);
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x000D6BB0 File Offset: 0x000D4DB0
	public static string GetUTFCodeStr(string sstr)
	{
		int num = sstr.IndexOf("[id]");
		int num2 = sstr.IndexOf("[/id]");
		bool flag = num != -1 && num2 != -1;
		if (flag)
		{
			string text = sstr.Substring(num, num2 + 5 - num);
			string text2 = text.Substring(4);
			text2 = text2.Substring(0, text2.Length - 5);
			string[] array = text2.Split(new char[]
			{
				'|'
			});
			int num3 = int.Parse(array[0]);
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				string[] array2 = new string[array.Length - 1];
				Array.Copy(array, 1, array2, 0, array2.Length);
				int nMsgID = num3;
				object[] values = array2;
				text2 = Language.GetLang(nMsgID, values);
			}
			else
			{
				text2 = Language.GetLang(num3, new object[0]);
			}
			sstr = sstr.Replace(text, text2);
			num = sstr.IndexOf("[id]");
			num2 = sstr.IndexOf("[/id]");
			bool flag3 = num != -1 && num2 != -1;
			if (flag3)
			{
				return Language.GetUTFCodeStr(sstr);
			}
		}
		num = sstr.IndexOf("[mi]");
		num2 = sstr.IndexOf("[/mi]");
		bool flag4 = num != -1 && num2 != -1;
		if (flag4)
		{
			string text3 = sstr.Substring(num, num2 + 5 - num);
			string text4 = text3.Substring(4);
			text4 = text4.Substring(0, text4.Length - 5);
			sstr = sstr.Replace(text3, Fun.DecodingUTF8(text4));
			num = sstr.IndexOf("[mi]");
			num2 = sstr.IndexOf("[/mi]");
			bool flag5 = num != -1 && num2 != -1;
			if (flag5)
			{
				return Language.GetUTFCodeStr(sstr);
			}
		}
		return sstr;
	}

	// Token: 0x04001248 RID: 4680
	private static string pattern = "{=[\\d]*?}";
}

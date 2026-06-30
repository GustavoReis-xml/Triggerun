using System;
using System.Collections;
using System.Text.RegularExpressions;

// Token: 0x020002CF RID: 719
public static class RegexTxt
{
	// Token: 0x06002115 RID: 8469 RVA: 0x00131028 File Offset: 0x0012F228
	public static string taskRegTxt(string sStr, string pattern, bool isLink = false)
	{
		Regex regex = new Regex(pattern);
		string text = string.Empty;
		foreach (object obj in regex.Matches(sStr))
		{
			Match match = (Match)obj;
			text = match.Value.ToString();
			text = text.Substring(1, text.Length - 2);
			string[] array = text.Split(new char[]
			{
				'|'
			});
			text = RegexTxt.getStrTask(text);
			string text2 = array[0];
			bool flag = text != "nopath" && isLink;
			if (flag)
			{
				text2 = string.Concat(new string[]
				{
					"[u][url=",
					text,
					"]",
					text2,
					"[/url][/u]"
				});
			}
			sStr = regex.Replace(sStr, text2, 1);
		}
		sStr = sStr.Replace("\\n", "\n");
		return sStr;
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x0013113C File Offset: 0x0012F33C
	public static string taskRegTxtFroManyTartet(string sStr, string pattern, ArrayList targetList, taskinfo tinfo)
	{
		Regex regex = new Regex(pattern);
		string text = string.Empty;
		string replacement = string.Empty;
		MatchCollection matchCollection = regex.Matches(sStr);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			text = matchCollection[i].Value.ToString();
			text = text.Substring(1, text.Length - 2);
			string[] array = text.Split(new char[]
			{
				'|'
			});
			text = RegexTxt.getStrTask(text);
			bool flag = matchCollection.Count != targetList.Count;
			if (flag)
			{
				replacement = string.Concat(new string[]
				{
					array[0].ToString(),
					"<color=#00ff00>(",
					tinfo.m_nTargetFinishCount.ToString(),
					"/",
					targetList.Count.ToString(),
					")</color>"
				});
			}
			else
			{
				bool flag2 = targetList.Count > i;
				if (flag2)
				{
					ArrayList arrayList = (ArrayList)targetList[i];
					replacement = string.Concat(new string[]
					{
						array[0].ToString(),
						"<color=#00ff00>(",
						arrayList[3].ToString(),
						"/",
						arrayList[2].ToString(),
						")</color>"
					});
				}
				else
				{
					replacement = array[0].ToString();
				}
			}
			sStr = regex.Replace(sStr, replacement, 1);
		}
		sStr = sStr.Replace("\\n", "\n");
		return sStr;
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x001312D8 File Offset: 0x0012F4D8
	public static string GetTaskLinkTxt(string sStr)
	{
		string text = string.Empty;
		Regex regex = new Regex(RegexTxt.taskspattern);
		Match match = regex.Match(sStr);
		bool flag = !match.Success;
		string result;
		if (flag)
		{
			result = string.Empty;
		}
		else
		{
			text = match.Value;
			result = text.Substring(1, text.Length - 2);
		}
		return result;
	}

	// Token: 0x06002118 RID: 8472 RVA: 0x00131334 File Offset: 0x0012F534
	public static string GetTaskLinkManyTargetTxt(string sStr, ArrayList targetList)
	{
		Regex regex = new Regex(RegexTxt.taskspattern);
		string text = string.Empty;
		int num = 0;
		MatchCollection matchCollection = regex.Matches(sStr);
		foreach (object obj in matchCollection)
		{
			Match match = (Match)obj;
			text = match.Value.ToString();
			text = text.Substring(1, text.Length - 2);
			bool flag = targetList.Count > num;
			if (flag)
			{
				ArrayList arrayList = (ArrayList)targetList[num];
				bool flag2 = (int)arrayList[3] < (int)arrayList[2];
				if (flag2)
				{
					return text;
				}
			}
			num++;
		}
		return text;
	}

	// Token: 0x06002119 RID: 8473 RVA: 0x0013141C File Offset: 0x0012F61C
	public static string GetTasksMsghManyTargetTxt(string sStr, ArrayList oldtargetList, ArrayList targetList)
	{
		Regex regex = new Regex(RegexTxt.taskspattern);
		string text = string.Empty;
		string text2 = string.Empty;
		int count = targetList.Count;
		MatchCollection matchCollection = regex.Matches(sStr);
		bool flag = count > matchCollection.Count;
		string result;
		if (flag)
		{
			result = string.Empty;
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)targetList[i];
				ArrayList arrayList2 = (ArrayList)oldtargetList[i];
				bool flag2 = (int)arrayList[3] != (int)arrayList2[3];
				if (flag2)
				{
					text = matchCollection[i].Value.ToString();
					text = text.Substring(1, text.Length - 2);
					string[] array = text.Split(new char[]
					{
						'|'
					});
					text2 = string.Concat(new string[]
					{
						array[0].ToString(),
						"<color=#00ff00>",
						arrayList[3].ToString(),
						"/",
						arrayList[2].ToString(),
						")</color>"
					});
					break;
				}
			}
			result = text2;
		}
		return result;
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x00131568 File Offset: 0x0012F768
	public static int GetTasksTargetPlot(string sStr, ArrayList oldtargetList, ArrayList targetList)
	{
		Regex regex = new Regex(RegexTxt.taskspattern);
		string text = string.Empty;
		int result = 0;
		int count = targetList.Count;
		MatchCollection matchCollection = regex.Matches(sStr);
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList = (ArrayList)targetList[i];
			ArrayList arrayList2 = (ArrayList)oldtargetList[i];
			bool flag = (int)arrayList[3] == (int)arrayList2[3];
			if (!flag)
			{
				bool flag2 = matchCollection.Count > i;
				if (flag2)
				{
					text = matchCollection[i].Value.ToString();
					text = text.Substring(1, text.Length - 2);
					string[] array = text.Split(new char[]
					{
						'|'
					});
					bool flag3 = array.Length >= 8;
					if (flag3)
					{
						result = int.Parse(array[7]);
					}
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x0013166C File Offset: 0x0012F86C
	public static string getStrTask(string sStr)
	{
		int num = sStr.IndexOf("|");
		bool flag = num == -1;
		string result;
		if (flag)
		{
			result = "nopath";
		}
		else
		{
			result = sStr.Substring(num, sStr.Length - num);
		}
		return result;
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x001316AC File Offset: 0x0012F8AC
	public static string GetUrlAtIndex(int index, string mText)
	{
		bool flag = index != -1 && index < mText.Length;
		if (flag)
		{
			int num = mText.IndexOf("[url=", index);
			bool flag2 = num != -1;
			if (flag2)
			{
				num += 5;
				int num2 = mText.IndexOf("]", num);
				bool flag3 = num2 != -1;
				if (flag3)
				{
					int num3 = mText.IndexOf("[/url]", num2);
					bool flag4 = num3 == -1 || num3 >= index;
					if (flag4)
					{
						return mText.Substring(num, num2 - num);
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x00131744 File Offset: 0x0012F944
	public static string RoomSetRegTxt(string sStr, string pattern, bool isLink, string sFrom)
	{
		Regex regex = new Regex(pattern);
		string text = string.Empty;
		foreach (object obj in regex.Matches(sStr))
		{
			Match match = (Match)obj;
			text = match.Value.ToString();
			text = text.Substring(1, text.Length - 2);
			string[] array = text.Split(new char[]
			{
				'|'
			});
			text = RegexTxt.getStrTask(text);
			string text2 = array[0];
			bool flag = text != "nopath" && isLink;
			if (flag)
			{
				text = text + "|" + sFrom;
				text2 = string.Concat(new string[]
				{
					"[u][url=",
					text,
					"]",
					text2,
					"[/url][/u]"
				});
				bool flag2 = int.Parse(array[array.Length - 1]) == 1;
				if (flag2)
				{
					string text3 = text2;
					text2 = string.Concat(new string[]
					{
						text3,
						"[url=chuan",
						text,
						"]  [u][ffff00][",
						Language.GetLang(252003, new object[0]),
						"][-][/u][/url] "
					});
				}
			}
			sStr = regex.Replace(sStr, text2, 1);
		}
		sStr = sStr.Replace("\\n", "\n");
		return sStr;
	}

	// Token: 0x04001E13 RID: 7699
	public static string taskspattern = "\\{.*?\\}";

	// Token: 0x04001E14 RID: 7700
	public static string mailspattern = "\\$.*?\\$";
}

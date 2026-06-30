using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class RandomName : MonoBehaviour
{
	// Token: 0x06002071 RID: 8305 RVA: 0x00129820 File Offset: 0x00127A20
	public static string GetRandName()
	{
		string text = string.Empty;
		string[] randomNameArr_first = fGameManager.mloadConfig.GetRandomNameArr_first();
		string[] randomNameArr_second = fGameManager.mloadConfig.GetRandomNameArr_second();
		string[] randomNameArr_special = fGameManager.mloadConfig.GetRandomNameArr_special();
		string str = string.Empty;
		str = randomNameArr_special[UnityEngine.Random.Range(0, randomNameArr_special.Length)].ToString();
		string text2 = string.Empty;
		string text3 = string.Empty;
		bool flag = randomNameArr_first != null;
		if (flag)
		{
			text3 = RandomName.GetRandom(randomNameArr_first);
		}
		while ((text3.Length == text2.Length && text2.Length >= 3) || text2.Length == 0)
		{
			bool flag2 = randomNameArr_second != null;
			if (flag2)
			{
				text2 = RandomName.GetRandom(randomNameArr_second);
			}
		}
		text = text3 + str + text2;
		bool flag3 = RandomName.szUsedName.Contains(text);
		string result;
		if (flag3)
		{
			result = RandomName.GetRandName();
		}
		else
		{
			result = text;
		}
		return result;
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x00129904 File Offset: 0x00127B04
	private static string GetRandom(string[] strarr)
	{
		string empty = string.Empty;
		int max = strarr.Length;
		int num = RandomName.myRandom(0, max);
		return strarr[num].ToString();
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x00129934 File Offset: 0x00127B34
	public static void AddUsedName(string sname)
	{
		bool flag = !RandomName.szUsedName.Contains(sname);
		if (flag)
		{
			RandomName.szUsedName.Add(sname);
		}
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x00129964 File Offset: 0x00127B64
	public static int myRandom(int min, int max)
	{
		return RandomName.randomn.Next(min, max);
	}

	// Token: 0x04001D86 RID: 7558
	private static List<string> szUsedName = new List<string>();

	// Token: 0x04001D87 RID: 7559
	private static long tick = DateTime.Now.Ticks;

	// Token: 0x04001D88 RID: 7560
	private static System.Random randomn = new System.Random((int)RandomName.tick | (int)(RandomName.tick >> 32));
}

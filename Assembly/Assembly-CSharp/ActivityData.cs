using System;
using System.Collections;

// Token: 0x02000005 RID: 5
public class ActivityData
{
	// Token: 0x06000006 RID: 6 RVA: 0x0000217B File Offset: 0x0000037B
	public ActivityData(int actid)
	{
		this.nActId = actid;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021A4 File Offset: 0x000003A4
	public void SetData(ArrayList szInfo)
	{
		this.nActId = (int)szInfo[0];
		this.nIsLoop = (int)szInfo[1];
		this.nStartDay = (int)szInfo[2];
		this.nLastDay = (int)szInfo[3];
		this.nFreeDay = (int)szInfo[4];
		this.nActType = (int)szInfo[5];
		this.szActParam = (ArrayList)szInfo[6];
		this.szWeek = (ArrayList)szInfo[7];
		this.nStartType = (int)szInfo[8];
		this.sStartTime = szInfo[9].ToString();
		this.sEndTime = szInfo[10].ToString();
		this.nEndDay = (int)szInfo[11];
		this.nUseHeFu = (int)szInfo[12];
		this.sActName = szInfo[13].ToString();
		this.nXmlId = (int)szInfo[14];
		this.nIsActOpen = (int)szInfo[15];
		bool flag = this.nIsActOpen == 0 && this.nIsLoop == 1 && this.nStartType != 3;
		if (flag)
		{
			this.sNextStart = this.GetNextStart();
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000230C File Offset: 0x0000050C
	private string GetNextStart()
	{
		string result = string.Empty;
		DateTime t = DateTime.MaxValue;
		string s = (this.nUseHeFu != 1) ? ActivityManager.GetInstance().sServerStart : ActivityManager.GetInstance().sServerMerge;
		DateTime dateTime = DateTime.Parse(s).AddDays((double)this.nStartDay);
		bool flag = string.IsNullOrEmpty(this.sEndTime);
		if (flag)
		{
			result = dateTime.ToString("yyyy-MM-dd") + " 00:00:00";
		}
		else
		{
			DateTime t2 = DateTime.Parse(DateTime.Parse(this.sEndTime).AddDays((double)this.nFreeDay).ToString("yyyy-MM-dd") + " 00:00:00");
			bool flag2 = this.nEndDay > 0;
			if (flag2)
			{
				t = dateTime.AddDays((double)this.nEndDay);
			}
			bool flag3 = t2 < t;
			if (flag3)
			{
				result = t2.ToString("yyyy-MM-dd HH:mm:ss");
			}
		}
		return result;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000240C File Offset: 0x0000060C
	public int GetLoopNum()
	{
		int result = 0;
		bool flag = this.nIsLoop == 1;
		if (flag)
		{
			string s = (this.nUseHeFu != 1) ? ActivityManager.GetInstance().sServerStart : ActivityManager.GetInstance().sServerMerge;
			DateTime d = DateTime.Parse(DateTime.Parse(s).AddDays((double)this.nStartDay).ToString("yyyy-MM-dd") + " 00:00:00");
			DateTime d2 = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd") + " 00:00:00");
			int num = (int)(d2 - d).TotalDays;
			result = num / (this.nLastDay + this.nFreeDay) + 1;
		}
		return result;
	}

	// Token: 0x04000008 RID: 8
	public int nActId;

	// Token: 0x04000009 RID: 9
	public int nIsLoop;

	// Token: 0x0400000A RID: 10
	public int nStartDay;

	// Token: 0x0400000B RID: 11
	public int nLastDay;

	// Token: 0x0400000C RID: 12
	public int nFreeDay;

	// Token: 0x0400000D RID: 13
	public int nActType;

	// Token: 0x0400000E RID: 14
	public ArrayList szActParam = new ArrayList();

	// Token: 0x0400000F RID: 15
	public ArrayList szWeek = new ArrayList();

	// Token: 0x04000010 RID: 16
	public int nStartType;

	// Token: 0x04000011 RID: 17
	public string sStartTime;

	// Token: 0x04000012 RID: 18
	public string sEndTime;

	// Token: 0x04000013 RID: 19
	public int nEndDay;

	// Token: 0x04000014 RID: 20
	public int nUseHeFu;

	// Token: 0x04000015 RID: 21
	public string sActName;

	// Token: 0x04000016 RID: 22
	public int nXmlId;

	// Token: 0x04000017 RID: 23
	public int nIsActOpen;

	// Token: 0x04000018 RID: 24
	public string sNextStart;
}

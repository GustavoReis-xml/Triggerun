using System;
using System.Collections;
using System.Xml;

// Token: 0x02000008 RID: 8
[Serializable]
public class AItemData
{
	// Token: 0x06000022 RID: 34 RVA: 0x00003548 File Offset: 0x00001748
	public void Init(XmlElement xml, int id)
	{
		this.index = int.Parse(xml.GetAttribute("index"));
		switch (id)
		{
		case 10:
		{
			this.sName = xml.GetAttribute("name");
			this.needLv = int.Parse(xml.GetAttribute("lv"));
			this.msg = xml.GetAttribute("msg");
			this.img = xml.GetAttribute("img");
			string[] array = xml.GetAttribute("awarditem").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = array[i].Length > 0;
				if (flag)
				{
					this.aryAward.Add(array[i].Split(new char[]
					{
						','
					}));
				}
			}
			break;
		}
		case 11:
		{
			this.awardid = int.Parse(xml.GetAttribute("awardid"));
			string[] array2 = xml.GetAttribute("day_awarditem").Split(new char[]
			{
				'|'
			});
			for (int j = 0; j < array2.Length; j++)
			{
				bool flag2 = array2[j].Length > 0;
				if (flag2)
				{
					this.aryAward.Add(array2[j].Split(new char[]
					{
						','
					}));
				}
			}
			break;
		}
		case 12:
			this.UserBuff = int.Parse(xml.GetAttribute("buff"));
			this.HeroBuff = int.Parse(xml.GetAttribute("herobuff"));
			this.sName1 = xml.GetAttribute("sname1");
			this.sName2 = xml.GetAttribute("sname2");
			break;
		case 17:
		{
			this.OnLineTime = int.Parse(xml.GetAttribute("time"));
			string[] array3 = xml.GetAttribute("rewardlist").Split(new char[]
			{
				'|'
			});
			for (int k = 0; k < array3.Length; k++)
			{
				bool flag3 = array3[k].Length > 0;
				if (flag3)
				{
					this.aryAward.Add(array3[k].Split(new char[]
					{
						','
					}));
				}
			}
			break;
		}
		case 18:
		{
			this.sOnTimeStart = xml.GetAttribute("starttime");
			this.sOnTimeEnd = xml.GetAttribute("endtime");
			this.sOnTimeDes = xml.GetAttribute("des");
			string[] array4 = xml.GetAttribute("rewardlist").Split(new char[]
			{
				'|'
			});
			for (int l = 0; l < array4.Length; l++)
			{
				bool flag4 = array4[l].Length > 0;
				if (flag4)
				{
					this.aryAward.Add(array4[l].Split(new char[]
					{
						','
					}));
				}
			}
			break;
		}
		case 19:
		{
			this.skyacttargetnum = int.Parse(xml.GetAttribute("targetnum"));
			this.skyactDes = xml.GetAttribute("des");
			string[] array5 = xml.GetAttribute("rewardlist").Split(new char[]
			{
				'|'
			});
			for (int m = 0; m < array5.Length; m++)
			{
				bool flag5 = array5[m].Length > 0;
				if (flag5)
				{
					this.aryAward.Add(array5[m].Split(new char[]
					{
						','
					}));
				}
			}
			break;
		}
		}
	}

	// Token: 0x0400004E RID: 78
	public int index;

	// Token: 0x0400004F RID: 79
	public int num;

	// Token: 0x04000050 RID: 80
	public string[] szshowAward;

	// Token: 0x04000051 RID: 81
	public string[] szAward;

	// Token: 0x04000052 RID: 82
	public int awardnum;

	// Token: 0x04000053 RID: 83
	public int people;

	// Token: 0x04000054 RID: 84
	public int needcharge;

	// Token: 0x04000055 RID: 85
	public int needvip;

	// Token: 0x04000056 RID: 86
	public string[] szAwardvip;

	// Token: 0x04000057 RID: 87
	public int day;

	// Token: 0x04000058 RID: 88
	public ArrayList aryAward = new ArrayList();

	// Token: 0x04000059 RID: 89
	public ArrayList aryDay = new ArrayList();

	// Token: 0x0400005A RID: 90
	public string vipnum = string.Empty;

	// Token: 0x0400005B RID: 91
	public string sName = string.Empty;

	// Token: 0x0400005C RID: 92
	public string hname = string.Empty;

	// Token: 0x0400005D RID: 93
	public ArrayList vipitem1 = new ArrayList();

	// Token: 0x0400005E RID: 94
	public ArrayList vipitem2 = new ArrayList();

	// Token: 0x0400005F RID: 95
	public string itempic = string.Empty;

	// Token: 0x04000060 RID: 96
	public string itemnum = string.Empty;

	// Token: 0x04000061 RID: 97
	public string msg = string.Empty;

	// Token: 0x04000062 RID: 98
	public string itempinzhi = string.Empty;

	// Token: 0x04000063 RID: 99
	public string sStr = string.Empty;

	// Token: 0x04000064 RID: 100
	public string[] szShowItem;

	// Token: 0x04000065 RID: 101
	public int freenum;

	// Token: 0x04000066 RID: 102
	public int allnum;

	// Token: 0x04000067 RID: 103
	public int teshunum;

	// Token: 0x04000068 RID: 104
	public int awardnum1;

	// Token: 0x04000069 RID: 105
	public int needLv;

	// Token: 0x0400006A RID: 106
	public string img = string.Empty;

	// Token: 0x0400006B RID: 107
	public int awardid;

	// Token: 0x0400006C RID: 108
	public int UserBuff;

	// Token: 0x0400006D RID: 109
	public int HeroBuff;

	// Token: 0x0400006E RID: 110
	public string sName1 = string.Empty;

	// Token: 0x0400006F RID: 111
	public string sName2 = string.Empty;

	// Token: 0x04000070 RID: 112
	public int OnLineTime;

	// Token: 0x04000071 RID: 113
	public string sOnTimeStart = string.Empty;

	// Token: 0x04000072 RID: 114
	public string sOnTimeEnd = string.Empty;

	// Token: 0x04000073 RID: 115
	public string sOnTimeDes = string.Empty;

	// Token: 0x04000074 RID: 116
	public int skyacttargetnum;

	// Token: 0x04000075 RID: 117
	public string skyactDes = string.Empty;
}

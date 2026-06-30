using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000216 RID: 534
public class LearnStepData
{
	// Token: 0x0600172D RID: 5933 RVA: 0x000D8398 File Offset: 0x000D6598
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		string attribute = xml.GetAttribute("showmsg");
		bool flag = attribute != string.Empty && attribute != "0";
		if (flag)
		{
			string[] array = attribute.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag2 = !(array[i] == string.Empty);
				if (flag2)
				{
					string[] array2 = array[i].Split(new char[]
					{
						','
					});
					this.szShowMsg.Add(new ArrayList
					{
						array2[0],
						array2[1],
						float.Parse(array2[2])
					});
				}
			}
		}
		attribute = xml.GetAttribute("npc");
		bool flag3 = attribute != string.Empty && attribute != "0";
		if (flag3)
		{
			string[] array = attribute.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			for (int j = 0; j < num; j++)
			{
				bool flag4 = !(array[j] == string.Empty);
				if (flag4)
				{
					string[] array3 = array[j].Split(new char[]
					{
						','
					});
					this.szNpc.Add(new ArrayList
					{
						int.Parse(array3[0]),
						float.Parse(array3[1]),
						float.Parse(array3[2]),
						float.Parse(array3[3]),
						float.Parse(array3[4])
					});
				}
			}
		}
		attribute = xml.GetAttribute("mapeff");
		bool flag5 = attribute != string.Empty && attribute != "0";
		if (flag5)
		{
			string[] array = attribute.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			for (int k = 0; k < num; k++)
			{
				bool flag6 = !(array[k] == string.Empty);
				if (flag6)
				{
					string[] array4 = array[k].Split(new char[]
					{
						','
					});
					this.szMapEff.Add(new ArrayList
					{
						array4[0],
						float.Parse(array4[1]),
						float.Parse(array4[2]),
						float.Parse(array4[3]),
						int.Parse(array4[3])
					});
				}
			}
		}
		attribute = xml.GetAttribute("uieff");
		bool flag7 = attribute != string.Empty && attribute != "0";
		if (flag7)
		{
			string[] array = attribute.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			for (int l = 0; l < num; l++)
			{
				bool flag8 = !(array[l] == string.Empty);
				if (flag8)
				{
					string[] array5 = array[l].Split(new char[]
					{
						','
					});
					this.szUiEff.Add(new ArrayList
					{
						int.Parse(array5[0]),
						array5[1],
						int.Parse(array5[2])
					});
				}
			}
		}
		attribute = xml.GetAttribute("banactid");
		bool flag9 = attribute != string.Empty && attribute != "0";
		if (flag9)
		{
			string[] array = attribute.Split(new char[]
			{
				','
			});
			int num = array.Length;
			for (int m = 0; m < num; m++)
			{
				bool flag10 = !(array[m] == string.Empty);
				if (flag10)
				{
					this.szBanActId.Add(int.Parse(array[m]));
				}
			}
		}
		this.sethp = int.Parse(xml.GetAttribute("sethp"));
		this.isfillpower = !(xml.GetAttribute("fillpower") == "0");
		this.comtype = int.Parse(xml.GetAttribute("comtype"));
		attribute = xml.GetAttribute("comparam");
		bool flag11 = attribute != string.Empty && attribute != "0";
		if (flag11)
		{
			string[] array = attribute.Split(new char[]
			{
				','
			});
			int num = array.Length;
			for (int n = 0; n < num; n++)
			{
				bool flag12 = !(array[n] == string.Empty);
				if (flag12)
				{
					this.szComParam.Add(float.Parse(array[n]));
				}
			}
		}
		attribute = xml.GetAttribute("tip");
		bool flag13 = attribute != string.Empty && attribute != "0";
		if (flag13)
		{
			string[] array = attribute.Split(new char[]
			{
				','
			});
			this.szTip = new ArrayList
			{
				array[0],
				array[1],
				float.Parse(array[2]),
				float.Parse(array[3])
			};
		}
		attribute = xml.GetAttribute("openui");
		bool flag14 = !(attribute != string.Empty) || !(attribute != "0");
		if (!flag14)
		{
			string[] array = attribute.Split(new char[]
			{
				','
			});
			int num = array.Length;
			for (int num2 = 0; num2 < num; num2++)
			{
				bool flag15 = !(array[num2] == string.Empty);
				if (flag15)
				{
					this.szOpenUi.Add(int.Parse(array[num2]));
				}
			}
		}
	}

	// Token: 0x0400125B RID: 4699
	public int id;

	// Token: 0x0400125C RID: 4700
	public string name = string.Empty;

	// Token: 0x0400125D RID: 4701
	public ArrayList szShowMsg = new ArrayList();

	// Token: 0x0400125E RID: 4702
	public ArrayList szNpc = new ArrayList();

	// Token: 0x0400125F RID: 4703
	public ArrayList szMapEff = new ArrayList();

	// Token: 0x04001260 RID: 4704
	public ArrayList szUiEff = new ArrayList();

	// Token: 0x04001261 RID: 4705
	public List<int> szBanActId = new List<int>();

	// Token: 0x04001262 RID: 4706
	public int sethp;

	// Token: 0x04001263 RID: 4707
	public bool isfillpower;

	// Token: 0x04001264 RID: 4708
	public int comtype;

	// Token: 0x04001265 RID: 4709
	public ArrayList szComParam = new ArrayList();

	// Token: 0x04001266 RID: 4710
	public ArrayList szTip = new ArrayList();

	// Token: 0x04001267 RID: 4711
	public List<int> szOpenUi = new List<int>();
}

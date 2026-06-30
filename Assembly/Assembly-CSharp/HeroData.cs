using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000173 RID: 371
[Serializable]
public class HeroData
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06001104 RID: 4356 RVA: 0x0009D8FD File Offset: 0x0009BAFD
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0009D908 File Offset: 0x0009BB08
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.hname = xml.GetAttribute("name");
		this.hcareer = int.Parse(xml.GetAttribute("career"));
		this.hhead = xml.GetAttribute("head");
		this.hfromtype = int.Parse(xml.GetAttribute("fromtype"));
		this.hbulletpos = int.Parse(xml.GetAttribute("bulletpos"));
		this.hzxicon = xml.GetAttribute("zxicon");
		this.sFPSModel = xml.GetAttribute("fps_model");
		this.sTPSModel = xml.GetAttribute("tps_model");
		this.hzhunxingClass = xml.GetAttribute("zhunxingclass");
		this.hratesize = int.Parse(xml.GetAttribute("ratesize"));
		this.Min_Rate_Size = int.Parse(xml.GetAttribute("minratesize"));
		this.Cross_Hair_Width = int.Parse(xml.GetAttribute("crosshairwidth"));
		this.Cross_Hair_Height = int.Parse(xml.GetAttribute("crosshairheight"));
		string attribute = xml.GetAttribute("fps_camera_pos");
		string[] array = attribute.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			Vector3 item = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
			this.FPSCameraPos.Add(item);
		}
		attribute = xml.GetAttribute("fps_model_pos");
		array = attribute.Split(new char[]
		{
			'|'
		});
		for (int j = 0; j < array.Length; j++)
		{
			string[] array3 = array[j].Split(new char[]
			{
				','
			});
			Vector3 item2 = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
			this.FPSModelPos.Add(item2);
		}
		attribute = xml.GetAttribute("tps_camera_pos");
		array = attribute.Split(new char[]
		{
			'|'
		});
		for (int k = 0; k < array.Length; k++)
		{
			string[] array4 = array[k].Split(new char[]
			{
				','
			});
			Vector3 item3 = new Vector3(float.Parse(array4[0]), float.Parse(array4[1]), float.Parse(array4[2]));
			this.TPSCameraPos.Add(item3);
		}
		attribute = xml.GetAttribute("head_Name_pos");
		array = attribute.Split(new char[]
		{
			','
		});
		bool flag = array.Length != 0;
		if (flag)
		{
			this.vHeadNamePos = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		this.fTPSCameraDis = float.Parse(xml.GetAttribute("tps_camera_dis"));
		attribute = xml.GetAttribute("fps_ani_effect");
		array = attribute.Split(new char[]
		{
			'#'
		});
		for (int l = 0; l < array.Length; l++)
		{
			string[] array5 = array[l].Split(new char[]
			{
				','
			});
			List<string> list = new List<string>();
			for (int m = 1; m < array5.Length; m++)
			{
				this.listFPSAllAniEffect.Add(array5[m]);
				list.Add(array5[m]);
			}
			this.dicFPSAniEffect.Add(array5[0], list);
		}
		attribute = xml.GetAttribute("tps_ani_effect");
		array = attribute.Split(new char[]
		{
			'#'
		});
		for (int n = 0; n < array.Length; n++)
		{
			string[] array6 = array[n].Split(new char[]
			{
				','
			});
			List<string> list2 = new List<string>();
			for (int num = 1; num < array6.Length; num++)
			{
				this.listTPSAllAniEffect.Add(array6[num]);
				list2.Add(array6[num]);
			}
			this.dicTPSAniEffect.Add(array6[0], list2);
		}
		this.skillshow = new HeroData.skillShow(xml.GetAttribute("showskill"));
		this.heroclass = xml.GetAttribute("heroclass");
		attribute = xml.GetAttribute("anihuifu");
		bool flag2 = attribute != "0";
		if (flag2)
		{
			array = attribute.Split(new char[]
			{
				','
			});
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				this.listAniHuifu.Add(array[num2]);
			}
		}
		attribute = xml.GetAttribute("speed");
		array = attribute.Split(new char[]
		{
			'|'
		});
		for (int num3 = 0; num3 < array.Length; num3++)
		{
			List<float> list3 = new List<float>();
			string[] array7 = array[num3].Split(new char[]
			{
				','
			});
			for (int num4 = 0; num4 < array7.Length; num4++)
			{
				list3.Add((float)int.Parse(array7[num4]) / 10f);
			}
			this.PlayerSpeed.Add(list3);
		}
		attribute = xml.GetAttribute("footsound");
		this.footSound = new ArrayList();
		bool flag3 = attribute != "0";
		if (flag3)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num5 = 0; num5 < array.Length; num5++)
			{
				this.footSound.Add(array[num5]);
			}
		}
		attribute = xml.GetAttribute("pinsisound");
		this.pinsiSound = new ArrayList();
		bool flag4 = attribute != "0";
		if (flag4)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num6 = 0; num6 < array.Length; num6++)
			{
				this.pinsiSound.Add(array[num6]);
			}
		}
		attribute = xml.GetAttribute("hurtsound");
		this.hurtSound = new ArrayList();
		bool flag5 = attribute != "0";
		if (flag5)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num7 = 0; num7 < array.Length; num7++)
			{
				this.hurtSound.Add(array[num7]);
			}
		}
		attribute = xml.GetAttribute("deadsound");
		this.deadSound = new ArrayList();
		bool flag6 = attribute != "0";
		if (flag6)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num8 = 0; num8 < array.Length; num8++)
			{
				this.deadSound.Add(array[num8]);
			}
		}
		attribute = xml.GetAttribute("RecoverySound");
		this.recoverySound = new ArrayList();
		bool flag7 = attribute != "0";
		if (flag7)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num9 = 0; num9 < array.Length; num9++)
			{
				this.recoverySound.Add(array[num9]);
			}
		}
		attribute = xml.GetAttribute("EnterSceneSound");
		this.EnterSceneSound = new ArrayList();
		bool flag8 = attribute != "0";
		if (flag8)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num10 = 0; num10 < array.Length; num10++)
			{
				this.EnterSceneSound.Add(array[num10]);
			}
		}
		attribute = xml.GetAttribute("ReviveInBaseSound");
		this.ReviveInBaseSound = new ArrayList();
		bool flag9 = attribute != "0";
		if (flag9)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num11 = 0; num11 < array.Length; num11++)
			{
				this.ReviveInBaseSound.Add(array[num11]);
			}
		}
		attribute = xml.GetAttribute("UnbreakbleKillSound");
		this.UnbreakbleKillSound = new ArrayList();
		bool flag10 = attribute != "0";
		if (flag10)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num12 = 0; num12 < array.Length; num12++)
			{
				this.UnbreakbleKillSound.Add(array[num12]);
			}
		}
		this.secSkill = xml.GetAttribute("secskill");
		this.showshadow = int.Parse(xml.GetAttribute("showshadow"));
		attribute = xml.GetAttribute("electricSound");
		this.ts_hurtSound = new ArrayList();
		bool flag11 = attribute != "0";
		if (flag11)
		{
			array = attribute.Split(new char[]
			{
				'|'
			});
			for (int num13 = 0; num13 < array.Length; num13++)
			{
				this.ts_hurtSound.Add(array[num13]);
			}
		}
		this.JumpOverSound = xml.GetAttribute("jumpovesound");
		this.shengyacolor = xml.GetAttribute("color");
		this.heroType = int.Parse(xml.GetAttribute("herotype"));
	}

	// Token: 0x04000D77 RID: 3447
	public int id;

	// Token: 0x04000D78 RID: 3448
	public string hname;

	// Token: 0x04000D79 RID: 3449
	public string hhead;

	// Token: 0x04000D7A RID: 3450
	public int hcareer;

	// Token: 0x04000D7B RID: 3451
	public int hfromtype;

	// Token: 0x04000D7C RID: 3452
	public int hbulletpos;

	// Token: 0x04000D7D RID: 3453
	public string hzxicon;

	// Token: 0x04000D7E RID: 3454
	public string heroclass;

	// Token: 0x04000D7F RID: 3455
	public HeroData.skillShow skillshow;

	// Token: 0x04000D80 RID: 3456
	public string hzhunxingClass = string.Empty;

	// Token: 0x04000D81 RID: 3457
	public int hratesize;

	// Token: 0x04000D82 RID: 3458
	public int Min_Rate_Size;

	// Token: 0x04000D83 RID: 3459
	public int Cross_Hair_Width;

	// Token: 0x04000D84 RID: 3460
	public int Cross_Hair_Height;

	// Token: 0x04000D85 RID: 3461
	public string sFPSModel;

	// Token: 0x04000D86 RID: 3462
	public string sTPSModel;

	// Token: 0x04000D87 RID: 3463
	public List<Vector3> FPSCameraPos = new List<Vector3>();

	// Token: 0x04000D88 RID: 3464
	public List<Vector3> FPSModelPos = new List<Vector3>();

	// Token: 0x04000D89 RID: 3465
	public List<Vector3> TPSCameraPos = new List<Vector3>();

	// Token: 0x04000D8A RID: 3466
	public Vector3 vHeadNamePos = Vector3.zero;

	// Token: 0x04000D8B RID: 3467
	public float fTPSCameraDis;

	// Token: 0x04000D8C RID: 3468
	public List<string> listFPSAllAniEffect = new List<string>();

	// Token: 0x04000D8D RID: 3469
	public Dictionary<string, List<string>> dicFPSAniEffect = new Dictionary<string, List<string>>();

	// Token: 0x04000D8E RID: 3470
	public List<string> listTPSAllAniEffect = new List<string>();

	// Token: 0x04000D8F RID: 3471
	public Dictionary<string, List<string>> dicTPSAniEffect = new Dictionary<string, List<string>>();

	// Token: 0x04000D90 RID: 3472
	public List<string> listAniHuifu = new List<string>();

	// Token: 0x04000D91 RID: 3473
	public List<List<float>> PlayerSpeed = new List<List<float>>();

	// Token: 0x04000D92 RID: 3474
	public ArrayList footSound;

	// Token: 0x04000D93 RID: 3475
	public ArrayList pinsiSound;

	// Token: 0x04000D94 RID: 3476
	public ArrayList hurtSound;

	// Token: 0x04000D95 RID: 3477
	public ArrayList ts_hurtSound;

	// Token: 0x04000D96 RID: 3478
	public ArrayList deadSound;

	// Token: 0x04000D97 RID: 3479
	public ArrayList recoverySound;

	// Token: 0x04000D98 RID: 3480
	public ArrayList EnterSceneSound;

	// Token: 0x04000D99 RID: 3481
	public ArrayList ReviveInBaseSound;

	// Token: 0x04000D9A RID: 3482
	public ArrayList UnbreakbleKillSound;

	// Token: 0x04000D9B RID: 3483
	public string JumpOverSound;

	// Token: 0x04000D9C RID: 3484
	public string secSkill;

	// Token: 0x04000D9D RID: 3485
	public int showshadow;

	// Token: 0x04000D9E RID: 3486
	public string shengyacolor;

	// Token: 0x04000D9F RID: 3487
	public int heroType;

	// Token: 0x02000593 RID: 1427
	public class skillShow
	{
		// Token: 0x060034A9 RID: 13481 RVA: 0x001BE618 File Offset: 0x001BC818
		public skillShow(string smsg)
		{
			this.heroSectionSkills = new Dictionary<int, ArrayList>();
			string[] array = smsg.Split(new char[]
			{
				'|'
			});
			string text = string.Empty;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				text = array[i];
				bool flag = !(text != string.Empty);
				if (!flag)
				{
					int key = i;
					bool flag2 = !this.heroSectionSkills.ContainsKey(key);
					if (flag2)
					{
						this.heroSectionSkills.Add(key, new ArrayList());
					}
					string[] array2 = text.Split(new char[]
					{
						','
					});
					int num2 = array2.Length;
					for (int j = 0; j < num2; j++)
					{
						string text2 = array2[j];
						bool flag3 = text2 != string.Empty;
						if (flag3)
						{
							this.heroSectionSkills[key].Add(int.Parse(text2));
							this.heroAllSkills.Add(int.Parse(text2));
						}
					}
				}
			}
		}

		// Token: 0x04002D8E RID: 11662
		public List<int> heroAllSkills = new List<int>();

		// Token: 0x04002D8F RID: 11663
		public Dictionary<int, ArrayList> heroSectionSkills;
	}
}

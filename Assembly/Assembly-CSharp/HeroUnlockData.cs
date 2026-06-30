using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class HeroUnlockData
{
	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06001466 RID: 5222 RVA: 0x000C0104 File Offset: 0x000BE304
	public int key
	{
		get
		{
			return this.nHeroid;
		}
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x000C010C File Offset: 0x000BE30C
	public void Init(XmlElement xml, int nId)
	{
		this.nHeroid = int.Parse(xml.GetAttribute("heroid"));
		this.nSortId = nId;
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sHeroWeapon = xml.GetAttribute("weaponmodel");
		this.sHeroModel = xml.GetAttribute("usermodel");
		this.modelScale = float.Parse(xml.GetAttribute("scale"));
		this.weaponScale = float.Parse(xml.GetAttribute("weaponscale"));
		this.szUnLock = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < this.szUnLock.Length; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(this.szUnLock[i]) && this.szUnLock[i] == "4";
			if (flag)
			{
				this.needChip = int.Parse(this.szUnLock[i + 1]);
				break;
			}
		}
		this.sBtnData = xml.GetAttribute("btndata");
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
		this.sUnlockDesc = xml.GetAttribute("desc");
		this.nIsShow = int.Parse(xml.GetAttribute("isshow"));
		string[] array = xml.GetAttribute("pos").Split(new char[]
		{
			','
		});
		bool flag2 = array.Length == 3;
		if (flag2)
		{
			this.modelPos = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		this.weaponYPos = float.Parse(xml.GetAttribute("weaponypos"));
		this.weaponAngle = int.Parse(xml.GetAttribute("weaponrot"));
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag3 = xmlElement.Name == "skin";
			if (flag3)
			{
				SkinUnlock skinUnlock = new SkinUnlock();
				skinUnlock.Init(xmlElement);
				this.szSkinUnlock.Add(skinUnlock.nId, skinUnlock);
				bool flag4 = skinUnlock.nDefault != 1;
				if (flag4)
				{
					this.totalSkinNum++;
				}
				this.allTpsModel.Add(skinUnlock.tpsModel);
				this.allFpsModel.Add(skinUnlock.fpsModel);
			}
			bool flag5 = xmlElement.Name == "weapon";
			if (flag5)
			{
				WeaponUnlock weaponUnlock = new WeaponUnlock();
				weaponUnlock.Init(xmlElement);
				this.szWeaponUnlock.Add(weaponUnlock.nId, weaponUnlock);
				bool flag6 = weaponUnlock.nDefault != 1;
				if (flag6)
				{
					this.totalWeaponNum++;
				}
				bool flag7 = !string.IsNullOrEmpty(weaponUnlock.lowMat_1st);
				if (flag7)
				{
					this.allWeaponMat.Add(weaponUnlock.lowMat_1st);
				}
				bool flag8 = !string.IsNullOrEmpty(weaponUnlock.highMat_1st);
				if (flag8)
				{
					this.allWeaponMat.Add(weaponUnlock.highMat_1st);
				}
				bool flag9 = !string.IsNullOrEmpty(weaponUnlock.comMat_3rd);
				if (flag9)
				{
					this.allWeaponMat.Add(weaponUnlock.comMat_3rd);
				}
			}
			bool flag10 = xmlElement.Name == "emotion";
			if (flag10)
			{
				EmotionUnlock emotionUnlock = new EmotionUnlock();
				emotionUnlock.Init(xmlElement);
				this.szEmotionUnlock.Add(emotionUnlock.nId, emotionUnlock);
				bool flag11 = emotionUnlock.nDefault != 1;
				if (flag11)
				{
					this.totalEmotionNum++;
				}
			}
			bool flag12 = xmlElement.Name == "pose";
			if (flag12)
			{
				PoseUnlock poseUnlock = new PoseUnlock();
				poseUnlock.Init(xmlElement);
				this.szPoseUnlock.Add(poseUnlock.nId, poseUnlock);
				bool flag13 = poseUnlock.nDefault != 1;
				if (flag13)
				{
					this.totalPoseNum++;
				}
			}
		}
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000C0608 File Offset: 0x000BE808
	public List<string> BtnUnlockData()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.szBtnData.Length; i += 2)
		{
			bool flag = i + 1 < this.szBtnData.Length;
			if (flag)
			{
				string text = this.szBtnData[i + 1];
				list.Add(this.szBtnData[i]);
				list.Add(text);
				list.Add(this.GetUnlockValue(text));
			}
		}
		return list;
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000C0684 File Offset: 0x000BE884
	public string GetUnlockValue(string stype)
	{
		string result = string.Empty;
		for (int i = 0; i < this.szUnLock.Length; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(this.szUnLock[i]) && this.szUnLock[i] == stype;
			if (flag)
			{
				result = this.szUnLock[i + 1];
				break;
			}
		}
		return result;
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000C06EC File Offset: 0x000BE8EC
	public void SetSkinData(ArrayList szData)
	{
		int num = 0;
		this.skinNewType = 0;
		for (int i = 0; i < szData.Count; i += 2)
		{
			int key = (int)szData[i];
			bool flag = !this.szSkinUnlock.ContainsKey(key);
			if (!flag)
			{
				SkinUnlock skinUnlock = this.szSkinUnlock[key];
				skinUnlock.SetData((ArrayList)szData[i + 1]);
				bool flag2 = skinUnlock.nDefault != 1 && skinUnlock.nLockState > 0;
				if (flag2)
				{
					num++;
				}
				bool flag3 = skinUnlock.nLockState == 1;
				if (flag3)
				{
					bool flag4 = this.heroNewType < 1;
					if (flag4)
					{
						this.heroNewType = 1;
					}
					bool flag5 = this.skinNewType < 1;
					if (flag5)
					{
						this.skinNewType = 1;
					}
				}
				bool flag6 = skinUnlock.nNewState == 1;
				if (flag6)
				{
					bool flag7 = this.heroNewType < 2;
					if (flag7)
					{
						this.heroNewType = 2;
					}
					bool flag8 = this.skinNewType < 2;
					if (flag8)
					{
						this.skinNewType = 2;
					}
				}
			}
		}
		this.openSkinNum = num;
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x000C0818 File Offset: 0x000BEA18
	public void SetWeaponData(ArrayList szData)
	{
		int num = 0;
		this.weaponNewType = 0;
		for (int i = 0; i < szData.Count; i += 2)
		{
			int key = (int)szData[i];
			bool flag = !this.szWeaponUnlock.ContainsKey(key);
			if (!flag)
			{
				WeaponUnlock weaponUnlock = this.szWeaponUnlock[key];
				weaponUnlock.SetData((ArrayList)szData[i + 1]);
				bool flag2 = weaponUnlock.nDefault != 1 && weaponUnlock.nLockState > 0;
				if (flag2)
				{
					num++;
				}
				bool flag3 = weaponUnlock.nLockState == 1;
				if (flag3)
				{
					bool flag4 = this.heroNewType < 1;
					if (flag4)
					{
						this.heroNewType = 1;
					}
					bool flag5 = this.weaponNewType < 1;
					if (flag5)
					{
						this.weaponNewType = 1;
					}
				}
				bool flag6 = weaponUnlock.nNewState == 1;
				if (flag6)
				{
					bool flag7 = this.heroNewType < 2;
					if (flag7)
					{
						this.heroNewType = 2;
					}
					bool flag8 = this.weaponNewType < 2;
					if (flag8)
					{
						this.weaponNewType = 2;
					}
				}
			}
		}
		this.openWeaponNum = num;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000C0944 File Offset: 0x000BEB44
	public void SetEmotionData(ArrayList szData)
	{
		int num = 0;
		this.emotionNewType = 0;
		for (int i = 0; i < szData.Count; i += 2)
		{
			int key = (int)szData[i];
			bool flag = !this.szEmotionUnlock.ContainsKey(key);
			if (!flag)
			{
				EmotionUnlock emotionUnlock = this.szEmotionUnlock[key];
				emotionUnlock.SetData((ArrayList)szData[i + 1]);
				bool flag2 = emotionUnlock.nDefault != 1 && emotionUnlock.nLockState > 0;
				if (flag2)
				{
					num++;
				}
				bool flag3 = emotionUnlock.nLockState == 1;
				if (flag3)
				{
					bool flag4 = this.heroNewType < 1;
					if (flag4)
					{
						this.heroNewType = 1;
					}
					bool flag5 = this.emotionNewType < 1;
					if (flag5)
					{
						this.emotionNewType = 1;
					}
				}
				bool flag6 = emotionUnlock.nNewState == 1;
				if (flag6)
				{
					bool flag7 = this.heroNewType < 2;
					if (flag7)
					{
						this.heroNewType = 2;
					}
					bool flag8 = this.emotionNewType < 2;
					if (flag8)
					{
						this.emotionNewType = 2;
					}
				}
			}
		}
		this.openEmotionNum = num;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000C0A70 File Offset: 0x000BEC70
	public void SetPoseData(ArrayList szData)
	{
		int num = 0;
		this.poseNewType = 0;
		for (int i = 0; i < szData.Count; i += 2)
		{
			int key = (int)szData[i];
			bool flag = !this.szPoseUnlock.ContainsKey(key);
			if (!flag)
			{
				PoseUnlock poseUnlock = this.szPoseUnlock[key];
				poseUnlock.SetData((ArrayList)szData[i + 1]);
				bool flag2 = poseUnlock.nDefault != 1 && poseUnlock.nLockState > 0;
				if (flag2)
				{
					num++;
				}
				bool flag3 = poseUnlock.nLockState == 1;
				if (flag3)
				{
					bool flag4 = this.heroNewType < 1;
					if (flag4)
					{
						this.heroNewType = 1;
					}
					bool flag5 = this.poseNewType < 1;
					if (flag5)
					{
						this.poseNewType = 1;
					}
				}
				bool flag6 = poseUnlock.nNewState == 1;
				if (flag6)
				{
					bool flag7 = this.heroNewType < 2;
					if (flag7)
					{
						this.heroNewType = 2;
					}
					bool flag8 = this.poseNewType < 2;
					if (flag8)
					{
						this.poseNewType = 2;
					}
				}
			}
		}
		this.openPoseNum = num;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000C0B9C File Offset: 0x000BED9C
	public int GetAllOpenNum()
	{
		return this.openSkinNum + this.openWeaponNum + this.openEmotionNum + this.openPoseNum;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x000C0BCC File Offset: 0x000BEDCC
	public int GetTotalNum()
	{
		return this.totalSkinNum + this.totalWeaponNum + this.totalEmotionNum + this.totalPoseNum;
	}

	// Token: 0x04000FAF RID: 4015
	public int nHeroid;

	// Token: 0x04000FB0 RID: 4016
	public int nDefault;

	// Token: 0x04000FB1 RID: 4017
	public int nIsShow;

	// Token: 0x04000FB2 RID: 4018
	public string sHeroWeapon;

	// Token: 0x04000FB3 RID: 4019
	public string sHeroModel;

	// Token: 0x04000FB4 RID: 4020
	public string[] szUnLock;

	// Token: 0x04000FB5 RID: 4021
	public string sBtnData;

	// Token: 0x04000FB6 RID: 4022
	public string[] szBtnData;

	// Token: 0x04000FB7 RID: 4023
	public int needChip;

	// Token: 0x04000FB8 RID: 4024
	public string sUnlockDesc;

	// Token: 0x04000FB9 RID: 4025
	public float modelScale;

	// Token: 0x04000FBA RID: 4026
	public float weaponScale;

	// Token: 0x04000FBB RID: 4027
	public Vector3 modelPos = Vector3.zero;

	// Token: 0x04000FBC RID: 4028
	public float weaponYPos;

	// Token: 0x04000FBD RID: 4029
	public int weaponAngle;

	// Token: 0x04000FBE RID: 4030
	public int nChipId;

	// Token: 0x04000FBF RID: 4031
	public int nSortId;

	// Token: 0x04000FC0 RID: 4032
	public string[] szGiftData;

	// Token: 0x04000FC1 RID: 4033
	public string sGiftDesc;

	// Token: 0x04000FC2 RID: 4034
	public int nGiftNum;

	// Token: 0x04000FC3 RID: 4035
	public int nIsZhekou;

	// Token: 0x04000FC4 RID: 4036
	public int openSkinNum;

	// Token: 0x04000FC5 RID: 4037
	public int totalSkinNum;

	// Token: 0x04000FC6 RID: 4038
	public int openWeaponNum;

	// Token: 0x04000FC7 RID: 4039
	public int totalWeaponNum;

	// Token: 0x04000FC8 RID: 4040
	public int openEmotionNum;

	// Token: 0x04000FC9 RID: 4041
	public int totalEmotionNum;

	// Token: 0x04000FCA RID: 4042
	public int openPoseNum;

	// Token: 0x04000FCB RID: 4043
	public int totalPoseNum;

	// Token: 0x04000FCC RID: 4044
	public List<string> allTpsModel = new List<string>();

	// Token: 0x04000FCD RID: 4045
	public List<string> allFpsModel = new List<string>();

	// Token: 0x04000FCE RID: 4046
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04000FCF RID: 4047
	public List<string> allWeaponMat = new List<string>();

	// Token: 0x04000FD0 RID: 4048
	public Dictionary<int, SkinUnlock> szSkinUnlock = new Dictionary<int, SkinUnlock>();

	// Token: 0x04000FD1 RID: 4049
	public Dictionary<int, WeaponUnlock> szWeaponUnlock = new Dictionary<int, WeaponUnlock>();

	// Token: 0x04000FD2 RID: 4050
	public Dictionary<int, EmotionUnlock> szEmotionUnlock = new Dictionary<int, EmotionUnlock>();

	// Token: 0x04000FD3 RID: 4051
	public Dictionary<int, PoseUnlock> szPoseUnlock = new Dictionary<int, PoseUnlock>();

	// Token: 0x04000FD4 RID: 4052
	public int heroLockType;

	// Token: 0x04000FD5 RID: 4053
	public int heroNewType;

	// Token: 0x04000FD6 RID: 4054
	public int skinNewType;

	// Token: 0x04000FD7 RID: 4055
	public int weaponNewType;

	// Token: 0x04000FD8 RID: 4056
	public int emotionNewType;

	// Token: 0x04000FD9 RID: 4057
	public int poseNewType;
}

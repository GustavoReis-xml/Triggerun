using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class RobotUnlockData
{
	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06002197 RID: 8599 RVA: 0x00138532 File Offset: 0x00136732
	public int key
	{
		get
		{
			return this.nHeroid;
		}
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x0013853C File Offset: 0x0013673C
	public void Init(XmlElement xml, int nId)
	{
		this.nHeroid = int.Parse(xml.GetAttribute("heroid"));
		this.nSortId = nId;
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sHeroModel = xml.GetAttribute("usermodel");
		this.modelScale = float.Parse(xml.GetAttribute("scale"));
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
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
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
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag3 = xmlElement.Name == "skin";
			if (flag3)
			{
				RobotSkinUnlock robotSkinUnlock = new RobotSkinUnlock();
				robotSkinUnlock.Init(xmlElement);
				this.szSkinUnlock.Add(robotSkinUnlock.nId, robotSkinUnlock);
				bool flag4 = robotSkinUnlock.nDefault != 1;
				if (flag4)
				{
					this.totalSkinNum++;
				}
				this.allTpsModel.Add(robotSkinUnlock.tpsModel);
				this.allFpsModel.Add(robotSkinUnlock.fpsModel);
			}
			bool flag5 = xmlElement.Name == "weapon";
			if (flag5)
			{
				RobotWeaponUnlock robotWeaponUnlock = new RobotWeaponUnlock();
				robotWeaponUnlock.Init(xmlElement);
				this.szWeaponUnlock.Add(robotWeaponUnlock.nId, robotWeaponUnlock);
				bool flag6 = robotWeaponUnlock.nDefault != 1;
				if (flag6)
				{
					this.totalWeaponNum++;
				}
			}
			bool flag7 = xmlElement.Name == "emotion";
			if (flag7)
			{
				RobotEmotionUnlock robotEmotionUnlock = new RobotEmotionUnlock();
				robotEmotionUnlock.Init(xmlElement);
				this.szEmotionUnlock.Add(robotEmotionUnlock.nId, robotEmotionUnlock);
				bool flag8 = robotEmotionUnlock.nDefault != 1;
				if (flag8)
				{
					this.totalEmotionNum++;
				}
			}
			bool flag9 = xmlElement.Name == "pose";
			if (flag9)
			{
				RobotPoseUnlock robotPoseUnlock = new RobotPoseUnlock();
				robotPoseUnlock.Init(xmlElement);
				this.szPoseUnlock.Add(robotPoseUnlock.nId, robotPoseUnlock);
				bool flag10 = robotPoseUnlock.nDefault != 1;
				if (flag10)
				{
					this.totalPoseNum++;
				}
			}
		}
	}

	// Token: 0x04001ECF RID: 7887
	public int nHeroid;

	// Token: 0x04001ED0 RID: 7888
	public int nDefault;

	// Token: 0x04001ED1 RID: 7889
	public int nIsShow;

	// Token: 0x04001ED2 RID: 7890
	public string sHeroModel;

	// Token: 0x04001ED3 RID: 7891
	public string[] szUnLock;

	// Token: 0x04001ED4 RID: 7892
	public string[] szBtnData;

	// Token: 0x04001ED5 RID: 7893
	public int needChip;

	// Token: 0x04001ED6 RID: 7894
	public string sUnlockDesc;

	// Token: 0x04001ED7 RID: 7895
	public float modelScale;

	// Token: 0x04001ED8 RID: 7896
	public Vector3 modelPos = Vector3.zero;

	// Token: 0x04001ED9 RID: 7897
	public int nChipId;

	// Token: 0x04001EDA RID: 7898
	public int nSortId;

	// Token: 0x04001EDB RID: 7899
	public int openSkinNum;

	// Token: 0x04001EDC RID: 7900
	public int totalSkinNum;

	// Token: 0x04001EDD RID: 7901
	public int openWeaponNum;

	// Token: 0x04001EDE RID: 7902
	public int totalWeaponNum;

	// Token: 0x04001EDF RID: 7903
	public int openEmotionNum;

	// Token: 0x04001EE0 RID: 7904
	public int totalEmotionNum;

	// Token: 0x04001EE1 RID: 7905
	public int openPoseNum;

	// Token: 0x04001EE2 RID: 7906
	public int totalPoseNum;

	// Token: 0x04001EE3 RID: 7907
	public List<string> allTpsModel = new List<string>();

	// Token: 0x04001EE4 RID: 7908
	public List<string> allFpsModel = new List<string>();

	// Token: 0x04001EE5 RID: 7909
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001EE6 RID: 7910
	public Dictionary<int, RobotSkinUnlock> szSkinUnlock = new Dictionary<int, RobotSkinUnlock>();

	// Token: 0x04001EE7 RID: 7911
	public Dictionary<int, RobotWeaponUnlock> szWeaponUnlock = new Dictionary<int, RobotWeaponUnlock>();

	// Token: 0x04001EE8 RID: 7912
	public Dictionary<int, RobotEmotionUnlock> szEmotionUnlock = new Dictionary<int, RobotEmotionUnlock>();

	// Token: 0x04001EE9 RID: 7913
	public Dictionary<int, RobotPoseUnlock> szPoseUnlock = new Dictionary<int, RobotPoseUnlock>();

	// Token: 0x04001EEA RID: 7914
	public int heroLockType;

	// Token: 0x04001EEB RID: 7915
	public int heroNewType;

	// Token: 0x04001EEC RID: 7916
	public int skinNewType;

	// Token: 0x04001EED RID: 7917
	public int weaponNewType;

	// Token: 0x04001EEE RID: 7918
	public int emotionNewType;

	// Token: 0x04001EEF RID: 7919
	public int poseNewType;
}

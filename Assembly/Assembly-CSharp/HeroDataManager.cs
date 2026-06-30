using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class HeroDataManager
{
	// Token: 0x06001107 RID: 4359 RVA: 0x0009E2E0 File Offset: 0x0009C4E0
	public static HeroDataManager GetInstance()
	{
		bool flag = HeroDataManager.instance == null;
		if (flag)
		{
			HeroDataManager.instance = new HeroDataManager();
		}
		return HeroDataManager.instance;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0009E310 File Offset: 0x0009C510
	public void OnHeroDataMsg(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		int num2 = num;
		switch (num2)
		{
		case 11001:
			this.InitPlayerFace((ArrayList)szNetMsg[1]);
			break;
		case 11002:
			this.OpenPlayerFace((ArrayList)szNetMsg[1]);
			break;
		case 11003:
			this.SetPlayerFace((ArrayList)szNetMsg[1]);
			break;
		case 11004:
		case 11005:
		case 11006:
		case 11007:
		case 11010:
			break;
		case 11008:
			this.InitPlayerPaint((ArrayList)szNetMsg[1]);
			break;
		case 11009:
			this.OpenPlayerPaint((ArrayList)szNetMsg[1]);
			break;
		case 11011:
			this.OnGiftOpenPaint((ArrayList)szNetMsg[1]);
			break;
		case 11012:
			this.OnGiftOpenPaintBack((ArrayList)szNetMsg[1]);
			break;
		case 11013:
			this.OnGiftOpenFace((ArrayList)szNetMsg[1]);
			break;
		case 11014:
			this.OnGiftOpenFaceBack((ArrayList)szNetMsg[1]);
			break;
		default:
			switch (num2)
			{
			case 12001:
				this.SetOpenHeroData((ArrayList)szNetMsg[1]);
				break;
			case 12002:
				this.InitOpenHeroData((ArrayList)szNetMsg[1]);
				break;
			case 12005:
				this.OneHeroLvlUp((ArrayList)szNetMsg[1]);
				break;
			case 12006:
				this.UpdateOneHero((ArrayList)szNetMsg[1]);
				break;
			case 12008:
				this.HeroUnLockOpen((ArrayList)szNetMsg[1]);
				break;
			case 12009:
				this.OnChangeUseOk((ArrayList)szNetMsg[1]);
				break;
			case 12010:
				this.HeroLvlUpAwardTip((ArrayList)szNetMsg[1]);
				break;
			case 12011:
				this.UpdateEquipChip((ArrayList)szNetMsg[1]);
				break;
			case 12016:
				this.OnGiftOpen((ArrayList)szNetMsg[1]);
				break;
			case 12017:
				this.OnGiftOpenBack((ArrayList)szNetMsg[1]);
				break;
			case 12018:
				this.OnSelectHeroUILockEnd((ArrayList)szNetMsg[1]);
				break;
			case 12020:
				this.InitHeroParts((ArrayList)szNetMsg[1]);
				break;
			case 12021:
				this.HeroPartsLvlUp((ArrayList)szNetMsg[1]);
				break;
			case 12023:
				this.HeroPartsRemove((ArrayList)szNetMsg[1]);
				break;
			case 12024:
				this.HeroPartsOpen((ArrayList)szNetMsg[1]);
				break;
			}
			break;
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0009E610 File Offset: 0x0009C810
	private void HeroLvlUpAwardTip(ArrayList szMsg)
	{
		int key = (int)szMsg[0];
		int num = (int)szMsg[1];
		int ntype = (int)szMsg[2];
		int nvalue = (int)szMsg[3];
		int num2 = (int)szMsg[4];
		bool flag = num2 > 0;
		if (flag)
		{
			HeroAwardData heroAwardData = fGameManager.mloadConfig.AllHeroAward[key];
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39535, new object[]
			{
				heroAwardData.GetDesc(ntype, nvalue),
				num2
			}), ChatColor.system, "system");
		}
		else
		{
			TipManager.GetInstance().ShowRewardTip(1, szMsg, null, null);
		}
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0009E6D0 File Offset: 0x0009C8D0
	private void OnChangeUseOk(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int nHeroId = (int)szMsg[1];
		int num2 = (int)szMsg[2];
		bool flag = num == 1 && num2 == 1 && HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.OnChangeSkinOk(nHeroId);
		}
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0009E73C File Offset: 0x0009C93C
	private void UpdateEquipChip(ArrayList arr)
	{
		PlayerDataManager.getInstance().playerdet.heroSkinChip = (int)arr[3];
		PlayerDataManager.getInstance().playerdet.heroEmotChip = (int)arr[4];
		bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.SetUnlockInfo();
		}
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0009E7AC File Offset: 0x0009C9AC
	public void SendHeroDataMsgToServer(int nNetID, ArrayList arr)
	{
		fGameManager.SendMsgToHallServer(1306, new ArrayList
		{
			nNetID,
			arr
		});
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
	public void SetOpenHeroData(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		this.UpdateOneHero(szMsg);
		bool flag = szMsg[2].ToString() != "gift";
		if (flag)
		{
			TipManager.GetInstance().ShowRewardTip(3, new ArrayList
			{
				num
			}, null, null);
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0009E844 File Offset: 0x0009CA44
	public void OnSelectHeroUILockEnd(ArrayList szMsg)
	{
		int nSeletHero = (int)szMsg[0];
		bool flag = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
		if (flag)
		{
			SelectHeroUICon.inst.OnHeroLockOver(nSeletHero, 1);
			SelectHeroUICon.inst.CloseLuckHeroWin();
		}
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x0009E898 File Offset: 0x0009CA98
	public void InitOpenHeroData(ArrayList szData)
	{
		for (int i = 0; i < szData.Count; i += 2)
		{
			int num = (int)szData[i];
			bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(num);
			if (flag)
			{
				bool flag2 = this.allOpenHero.ContainsKey(num);
				OpenHeroData openHeroData;
				if (flag2)
				{
					openHeroData = this.allOpenHero[num];
				}
				else
				{
					openHeroData = new OpenHeroData(num);
					this.allOpenHero.Add(openHeroData.nHeroId, openHeroData);
				}
				openHeroData.SetData((ArrayList)szData[i + 1]);
			}
		}
		bool flag3 = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag3)
		{
			HeroInfoUICon.inst.InitHeroData();
			HeroInfoUICon.inst.RefreshSlcHeroInfo();
		}
		bool flag4 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag4)
		{
			MainUICon.inst.SetHeroNewState();
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0009E9A0 File Offset: 0x0009CBA0
	public void UpdateOneHero(ArrayList szData)
	{
		int num = (int)szData[0];
		ArrayList data = (ArrayList)szData[1];
		bool flag = this.allOpenHero.ContainsKey(num);
		if (flag)
		{
			this.allOpenHero[num].SetData(data);
		}
		bool flag2 = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag2)
		{
			HeroInfoUICon.inst.SetOneHeroGrid(num);
			HeroInfoUICon.inst.SetUnlockTgNewState(num);
			HeroInfoUICon.inst.OnClickUnlock(1);
		}
		bool flag3 = MainUICon.inst != null;
		if (flag3)
		{
			MainUICon.inst.SetHeroNewState();
		}
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0009EA50 File Offset: 0x0009CC50
	public bool IsAwarded(int heroid, int nlvl)
	{
		bool result = false;
		OpenHeroData openHeroData = this.GetOpenHeroData(heroid);
		bool flag = openHeroData != null && openHeroData.szAward.IndexOf(nlvl) != -1;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0009EA94 File Offset: 0x0009CC94
	public OpenHeroData GetOneHeroData(int nHeroId)
	{
		OpenHeroData result = null;
		bool flag = this.allOpenHero.ContainsKey(nHeroId);
		if (flag)
		{
			result = this.allOpenHero[nHeroId];
		}
		return result;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
	public OpenHeroData GetOpenHeroData(int nHeroId)
	{
		OpenHeroData result = null;
		bool flag = this.allOpenHero.ContainsKey(nHeroId) && this.allOpenHero[nHeroId].nOpenState > 0;
		if (flag)
		{
			result = this.allOpenHero[nHeroId];
		}
		return result;
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0009EB18 File Offset: 0x0009CD18
	public int GetHeroEquipOpen(int heroid, int ntype, int nid)
	{
		int result = 0;
		OpenHeroData oneHeroData = this.GetOneHeroData(heroid);
		bool flag = oneHeroData != null;
		if (flag)
		{
			switch (ntype)
			{
			case 1:
			{
				bool flag2 = oneHeroData.unlockData.szSkinUnlock.ContainsKey(nid);
				if (flag2)
				{
					result = oneHeroData.unlockData.szSkinUnlock[nid].nLockState;
				}
				break;
			}
			case 2:
			{
				bool flag3 = oneHeroData.unlockData.szWeaponUnlock.ContainsKey(nid);
				if (flag3)
				{
					result = oneHeroData.unlockData.szWeaponUnlock[nid].nLockState;
				}
				break;
			}
			case 3:
			{
				bool flag4 = oneHeroData.unlockData.szEmotionUnlock.ContainsKey(nid);
				if (flag4)
				{
					result = oneHeroData.unlockData.szEmotionUnlock[nid].nLockState;
				}
				break;
			}
			case 4:
			{
				bool flag5 = oneHeroData.unlockData.szPoseUnlock.ContainsKey(nid);
				if (flag5)
				{
					result = oneHeroData.unlockData.szPoseUnlock[nid].nLockState;
				}
				break;
			}
			case 5:
			{
				bool flag6 = fGameManager.mloadConfig.AllPaintData.ContainsKey(nid);
				if (flag6)
				{
					result = fGameManager.mloadConfig.AllPaintData[nid].nLockState;
				}
				break;
			}
			}
		}
		return result;
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0009EC69 File Offset: 0x0009CE69
	private void OneHeroLvlUp(ArrayList szMsg)
	{
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0009EC6C File Offset: 0x0009CE6C
	public ArrayList GetHeroMatchData(int nheroid)
	{
		ArrayList result = new ArrayList();
		ArrayList arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szHeroCareerData.Clone();
		for (int i = 1; i < arrayList.Count; i += 2)
		{
			bool flag = (int)(arrayList[i] as ArrayList)[0] == nheroid;
			if (flag)
			{
				result = (ArrayList)arrayList[i];
				break;
			}
		}
		return result;
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
	public int GetHeroNewState()
	{
		int num = 0;
		foreach (OpenHeroData openHeroData in this.allOpenHero.Values)
		{
			bool flag = openHeroData.nHeroId != 0;
			if (flag)
			{
				bool flag2 = openHeroData.nHeroNewType == 1 && num < 1;
				if (flag2)
				{
					num = 1;
				}
				bool flag3 = (openHeroData.nHeroNewType == 2 || PlayerDataManager.getInstance().playerCommon.Gem_Data_Class.GetGemNewState(openHeroData.nHeroId) == 1) && num < 2;
				if (flag3)
				{
					num = 2;
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0009EDA8 File Offset: 0x0009CFA8
	public SkinUnlock GetUseSkinData(int nheroid, int ncid)
	{
		SkinUnlock result = null;
		int key = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			key = this.GetOneHeroData(nheroid).nUseSkin;
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag2 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag2)
			{
				ArrayList oneHeroInfo = playerByCid.playerCommon.GetOneHeroInfo(nheroid);
				bool flag3 = oneHeroInfo.Count > 15;
				if (flag3)
				{
					key = (int)oneHeroInfo[11];
				}
			}
		}
		bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nheroid) && fGameManager.mloadConfig.AllHeroUnlock[nheroid].szSkinUnlock.ContainsKey(key);
		if (flag4)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nheroid].szSkinUnlock[key];
		}
		return result;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x0009EE8C File Offset: 0x0009D08C
	public RobotSkinUnlock GetNpcSkinData(int nheroid)
	{
		RobotSkinUnlock result = null;
		int heroId = fGameManager.mloadConfig.AllRobotCommonData[nheroid].heroId;
		int skinID = fGameManager.mloadConfig.AllRobotCommonData[nheroid].skinID;
		bool flag = fGameManager.mloadConfig.AllRobotUnlock.ContainsKey(heroId);
		if (flag)
		{
			result = fGameManager.mloadConfig.AllRobotUnlock[heroId].szSkinUnlock[skinID];
		}
		return result;
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0009EF04 File Offset: 0x0009D104
	public EmotionUnlock GetUseEmotionData(int nheroid, int ncid)
	{
		EmotionUnlock result = null;
		int key = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			key = this.GetOneHeroData(nheroid).nUseEmotion;
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag2 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag2)
			{
				ArrayList oneHeroInfo = playerByCid.playerCommon.GetOneHeroInfo(nheroid);
				bool flag3 = oneHeroInfo.Count > 15;
				if (flag3)
				{
					key = (int)oneHeroInfo[13];
				}
			}
		}
		bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nheroid) && fGameManager.mloadConfig.AllHeroUnlock[nheroid].szEmotionUnlock.ContainsKey(key);
		if (flag4)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nheroid].szEmotionUnlock[key];
		}
		return result;
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0009EFE8 File Offset: 0x0009D1E8
	public PoseUnlock GetUsePoseData(int nheroid, int ncid)
	{
		PoseUnlock result = null;
		int key = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			key = this.GetOneHeroData(nheroid).nUsePose;
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag2 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag2)
			{
				ArrayList oneHeroInfo = playerByCid.playerCommon.GetOneHeroInfo(nheroid);
				bool flag3 = oneHeroInfo.Count > 15;
				if (flag3)
				{
					key = (int)oneHeroInfo[14];
				}
			}
		}
		bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nheroid) && fGameManager.mloadConfig.AllHeroUnlock[nheroid].szPoseUnlock.ContainsKey(key);
		if (flag4)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nheroid].szPoseUnlock[key];
		}
		return result;
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0009F0CC File Offset: 0x0009D2CC
	public PlayerPaintData GetUsePaintData(int ncid)
	{
		PlayerPaintData result = null;
		int key = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			key = PlayerDataManager.getInstance().HUserData.m_nUsePaint;
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag2 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag2)
			{
				key = playerByCid.playerCommon.m_nUsePaint;
			}
		}
		bool flag3 = fGameManager.mloadConfig.AllPaintData.ContainsKey(key);
		if (flag3)
		{
			result = fGameManager.mloadConfig.AllPaintData[key];
		}
		return result;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0009F164 File Offset: 0x0009D364
	public WeaponUnlock GetUseWeaponData(int nheroid, int ncid)
	{
		WeaponUnlock result = null;
		int key = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			OpenHeroData oneHeroData = this.GetOneHeroData(nheroid);
			bool flag2 = oneHeroData != null;
			if (flag2)
			{
				key = this.GetOneHeroData(nheroid).nUseWeapon;
			}
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag3 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag3)
			{
				ArrayList oneHeroInfo = playerByCid.playerCommon.GetOneHeroInfo(nheroid);
				bool flag4 = oneHeroInfo.Count > 15;
				if (flag4)
				{
					key = (int)oneHeroInfo[12];
				}
			}
		}
		bool flag5 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nheroid) && fGameManager.mloadConfig.AllHeroUnlock[nheroid].szWeaponUnlock.ContainsKey(key);
		if (flag5)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nheroid].szWeaponUnlock[key];
		}
		return result;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0009F260 File Offset: 0x0009D460
	public ArrayList GetWeaponPartsData(int nheroid, int ncid)
	{
		ArrayList result = new ArrayList();
		int num = 0;
		bool flag = ncid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			OpenHeroData oneHeroData = this.GetOneHeroData(nheroid);
			bool flag2 = oneHeroData != null;
			if (flag2)
			{
				num = this.GetOneHeroData(nheroid).nUseWeapon;
			}
			result = (ArrayList)oneHeroData.unlockData.szWeaponUnlock[num].szPartsGrid.Clone();
		}
		else
		{
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(ncid);
			bool flag3 = playerByCid != null && playerByCid.playerCommon != null;
			if (flag3)
			{
				ArrayList oneHeroInfo = playerByCid.playerCommon.GetOneHeroInfo(nheroid);
				bool flag4 = oneHeroInfo.Count > 15;
				if (flag4)
				{
					num = (int)oneHeroInfo[12];
				}
				ArrayList arrayList = (ArrayList)oneHeroInfo[7];
				int num2 = arrayList.IndexOf(num);
				bool flag5 = num2 != -1;
				if (flag5)
				{
					arrayList = (ArrayList)arrayList[num2 + 1];
					result = (ArrayList)(arrayList[6] as ArrayList).Clone();
				}
			}
		}
		return result;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0009F38C File Offset: 0x0009D58C
	public WeaponUnlock GetUseRobotWeaponData(int nheroid, int ncid)
	{
		WeaponUnlock result = null;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nheroid) && fGameManager.mloadConfig.AllHeroUnlock[nheroid].szWeaponUnlock.ContainsKey(1);
		if (flag)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nheroid].szWeaponUnlock[1];
		}
		return result;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0009F3F4 File Offset: 0x0009D5F4
	private void HeroUnLockOpen(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int num2 = (int)szMsg[1];
		int num3 = (int)szMsg[2];
		string item = szMsg[3].ToString();
		bool flag = num2 != 5;
		if (flag)
		{
			List<string> list = new List<string>
			{
				"money",
				"chip",
				"gold",
				"matchmoney",
				"task",
				"shouchong",
				"lianxu",
				"luckydraw",
				"exchangeitem"
			};
			bool flag2 = list.IndexOf(item) != -1;
			if (flag2)
			{
				TipManager.GetInstance().ShowRewardTip(2, new ArrayList
				{
					num,
					num2,
					num3
				}, null, null);
			}
		}
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0009F518 File Offset: 0x0009D718
	public List<string> GetAllHeroModel(int nType)
	{
		bool flag = this.allHeroTpsModel.Count == 0 || this.allHeroFpsModel.Count == 0 || this.allHeroWeapon.Count == 0;
		if (flag)
		{
			foreach (HeroUnlockData heroUnlockData in fGameManager.mloadConfig.AllHeroUnlock.Values)
			{
				for (int i = 0; i < heroUnlockData.allTpsModel.Count; i++)
				{
					string text = heroUnlockData.allTpsModel[i];
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						this.allHeroTpsModel.Add(text);
					}
				}
				for (int j = 0; j < heroUnlockData.allFpsModel.Count; j++)
				{
					string text2 = heroUnlockData.allFpsModel[j];
					bool flag3 = !string.IsNullOrEmpty(text2);
					if (flag3)
					{
						this.allHeroFpsModel.Add(text2);
					}
				}
				for (int k = 0; k < heroUnlockData.allWeaponMat.Count; k++)
				{
					string text3 = heroUnlockData.allWeaponMat[k];
					bool flag4 = !string.IsNullOrEmpty(text3);
					if (flag4)
					{
						this.allHeroWeapon.Add(text3);
					}
				}
			}
		}
		if (!true)
		{
		}
		List<string> result;
		if (nType != 0)
		{
			if (nType != 1)
			{
				result = this.allHeroWeapon;
			}
			else
			{
				result = this.allHeroFpsModel;
			}
		}
		else
		{
			result = this.allHeroTpsModel;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0009F6E0 File Offset: 0x0009D8E0
	private void InitPlayerFace(ArrayList szData)
	{
		foreach (PlayerFaceData playerFaceData in fGameManager.mloadConfig.AllFaceData.Values)
		{
			int num = szData.IndexOf(playerFaceData.key);
			bool flag = num != -1;
			if (flag)
			{
				ArrayList arrayList = (ArrayList)szData[num + 1];
				playerFaceData.nIsOpen = 1;
				playerFaceData.nIsNew = (int)arrayList[0];
				playerFaceData.nGiftTime = (int)arrayList[3];
			}
			else
			{
				playerFaceData.nIsOpen = 0;
				playerFaceData.nIsNew = 0;
				playerFaceData.nGiftTime = 0;
			}
		}
		bool flag2 = FaceUICon.inst != null && FaceUICon.inst.visible;
		if (flag2)
		{
			FaceUICon.inst.InitFaceData();
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0009F7E0 File Offset: 0x0009D9E0
	private void OpenPlayerFace(ArrayList szData)
	{
		ArrayList szData2 = (ArrayList)szData[0];
		this.InitPlayerFace(szData2);
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0009F804 File Offset: 0x0009DA04
	private void SetPlayerFace(ArrayList szData)
	{
		PlayerDataManager.getInstance().HUserData.m_nFace = (int)szData[0];
		ArrayList szData2 = (ArrayList)szData[1];
		this.InitPlayerFace(szData2);
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetPlayerInfo();
		}
		bool flag2 = FaceUICon.inst != null && FaceUICon.inst.visible;
		if (flag2)
		{
			FaceUICon.inst.SetPlayerInfo();
		}
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0009F898 File Offset: 0x0009DA98
	private void OpenPlayerPaint(ArrayList szData)
	{
		ArrayList data = (ArrayList)szData[0];
		int key = (int)szData[1];
		bool flag = fGameManager.mloadConfig.AllPaintData.ContainsKey(key);
		if (flag)
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[key];
			playerPaintData.SetData(data);
		}
		bool flag2 = FaceUICon.inst != null && FaceUICon.inst.visible;
		if (flag2)
		{
			FaceUICon.inst.ResetPaintGrid();
		}
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0009F91C File Offset: 0x0009DB1C
	private void InitPlayerPaint(ArrayList szData)
	{
		ArrayList arrayList = (ArrayList)szData[0];
		PlayerDataManager.getInstance().HUserData.m_nUsePaint = (int)szData[1];
		foreach (PlayerPaintData playerPaintData in fGameManager.mloadConfig.AllPaintData.Values)
		{
			int num = arrayList.IndexOf(playerPaintData.nPaintId);
			bool flag = num != -1;
			if (flag)
			{
				ArrayList data = (ArrayList)arrayList[num + 1];
				playerPaintData.SetData(data);
			}
		}
		bool flag2 = FaceUICon.inst != null && FaceUICon.inst.visible;
		if (flag2)
		{
			FaceUICon.inst.ResetPaintGrid();
		}
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0009FA08 File Offset: 0x0009DC08
	public void OnFcmDataMsg(ArrayList szNetMsg)
	{
		switch ((int)szNetMsg[0])
		{
		case 13001:
			this.SetFcmState((ArrayList)szNetMsg[1]);
			break;
		case 13004:
			this.ShowFcmTip((ArrayList)szNetMsg[1]);
			break;
		case 13005:
			this.SetFcmData((ArrayList)szNetMsg[1]);
			break;
		}
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0009FA90 File Offset: 0x0009DC90
	public void SendFcmDataMsgToServer(int nNetID, ArrayList arr)
	{
		fGameManager.SendMsgToHallServer(1307, new ArrayList
		{
			nNetID,
			arr
		});
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0009FAC8 File Offset: 0x0009DCC8
	public int CountOnLineTime()
	{
		int num = -1;
		bool flag = this.nFcmOpen == 0 || this.nIsFcmCheck == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime serverDate = Fun.GetServerDate();
			int num2 = (int)(serverDate - this.dOnlTime).TotalSeconds;
			result = this.nOnline + this.nFcmNum * num2;
		}
		return result;
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0009FB28 File Offset: 0x0009DD28
	public void CheckTipTime()
	{
		int num = 0;
		int num2 = 0;
		bool flag = fGameManager.m_nUserType == 1;
		if (!flag)
		{
			int num3 = this.CountOnLineTime();
			bool flag2 = num3 <= 0 || num3 == this.onlineCount;
			if (!flag2)
			{
				bool flag3 = this.onlineCount == 0;
				if (flag3)
				{
					num = 1;
				}
				this.onlineCount = num3;
				bool flag4 = num3 >= 3600 && num2 == 0;
				if (flag4)
				{
					num2 = 1;
				}
				bool flag5 = num3 >= 7200 && num2 == 1;
				if (flag5)
				{
					num2 = 2;
				}
				bool flag6 = num3 >= 10500 && num2 == 2;
				if (flag6)
				{
					num2 = 3;
				}
				bool flag7 = num3 >= 10800 && num2 == 3;
				if (flag7)
				{
					num2 = 4;
				}
				bool flag8 = num2 != this.nFcmTip;
				if (flag8)
				{
					this.nFcmTip = num2;
					bool flag9 = num != 1 || this.nFcmTip == 4;
					if (flag9)
					{
						HeroDataManager.GetInstance().SendFcmDataMsgToServer(13004, new ArrayList());
					}
				}
			}
		}
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0009FC38 File Offset: 0x0009DE38
	private void ShowFcmTip(ArrayList szMsg)
	{
		switch ((int)szMsg[0])
		{
		case 1:
		{
			this.nFcmTip = 1;
			bool flag = string.IsNullOrEmpty(this.sIDCard);
			if (flag)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15007, new object[]
				{
					1
				}), Language.GetLang(15004, new object[0]), Language.GetLang(15005, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnFcmCertificate), null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15007, new object[]
				{
					1
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			break;
		}
		case 2:
		{
			this.nFcmTip = 2;
			bool flag2 = string.IsNullOrEmpty(this.sIDCard);
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15007, new object[]
				{
					2
				}), Language.GetLang(15004, new object[0]), Language.GetLang(15005, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnFcmCertificate), null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15007, new object[]
				{
					2
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			break;
		}
		case 3:
		{
			this.nFcmTip = 3;
			bool flag3 = string.IsNullOrEmpty(this.sIDCard);
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15008, new object[0]), Language.GetLang(15004, new object[0]), Language.GetLang(15005, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnFcmCertificate), null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			break;
		}
		case 4:
		{
			this.nFcmTip = 4;
			bool flag4 = string.IsNullOrEmpty(this.sIDCard) && (int)szMsg[1] == 0;
			if (flag4)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15009, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(15011, new object[0]), new TipManager.TipOKDelegate(this.OnExitGame), null, new TipManager.TipCancelDelegate(this.OnFcmCertificate), null, string.Empty, false, null, 8, string.Empty, string.Empty, string.Empty);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15012, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnExitGame), null, null, null, string.Empty, false, null, 8, string.Empty, string.Empty, string.Empty);
			}
			break;
		}
		case 5:
		{
			ArrayList arrayList = (ArrayList)szMsg[1];
			string value = arrayList[0].ToString();
			DateTime serverDate = Fun.GetServerDate();
			DateTime d = DateTime.Parse(arrayList[2].ToString());
			int num = (int)(serverDate - d).TotalSeconds;
			num = 18000 - num;
			bool flag5 = string.IsNullOrEmpty(value);
			if (flag5)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15006, new object[]
				{
					Fun.SecToFullTimeString(num)
				}), Language.GetLang(8012, new object[0]), Language.GetLang(15011, new object[0]), new TipManager.TipOKDelegate(this.OnExitGame), null, new TipManager.TipCancelDelegate(this.OnFcmCertificate), arrayList, string.Empty, false, null, 8, string.Empty, string.Empty, string.Empty);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15006, new object[]
				{
					Fun.SecToFullTimeString(num)
				}), Language.GetLang(8012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnExitGame), null, null, null, string.Empty, false, null, 8, string.Empty, string.Empty, string.Empty);
			}
			break;
		}
		case 6:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(15003, new object[0]), Language.GetLang(15002, new object[0]), Language.GetLang(15004, new object[0]), Language.GetLang(15005, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnFcmCertificate), null, null, null);
			break;
		}
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x000A01DF File Offset: 0x0009E3DF
	private void OnFcmWebCertificate(object szParam)
	{
		Application.Quit();
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000A01E8 File Offset: 0x0009E3E8
	private void OnRelogin(object szParam)
	{
		UserSocket socket = fGameManager.GetInstance().GetSocket();
		socket.SetIpAndPort();
		socket.unFsorket.InitClientScoketInfo("LoginServer", "LoginUser", GameRoot.Login_Control.Login_Name, GameRoot.Login_Control.Login_Password, string.Concat(new string[]
		{
			GameRoot.Login_Control.Server,
			"#HS#",
			SysConfig.g_client,
			"#",
			Fun.GetUdId(),
			"#",
			Fun.GetMAC0()
		}));
		socket.connectServer();
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000A0281 File Offset: 0x0009E481
	private void OnExitGame(object szParam)
	{
		Application.Quit();
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000A028A File Offset: 0x0009E48A
	private void OnFcmCertificate(object szParam)
	{
		fUIManager.GetInstance().ShowWin("FCMUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.LoadFcmUIOk), szParam, true);
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000A02AC File Offset: 0x0009E4AC
	private void LoadFcmUIOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = FCMUICon.inst != null;
		if (flag)
		{
			FCMUICon.inst.SetFcmData(szParam);
		}
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000A02D8 File Offset: 0x0009E4D8
	private void SetFcmState(ArrayList szMsg)
	{
		this.nFcmOpen = (int)szMsg[0];
		bool flag = this.nFcmOpen == 1 && this.nIsFcmCheck == 1;
		if (flag)
		{
			this.CheckTipTime();
		}
		bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag2)
		{
			MainUICon.inst.SetFcmShow();
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000A0348 File Offset: 0x0009E548
	private void SetFcmData(ArrayList szMsg)
	{
		this.sIDCard = szMsg[0].ToString();
		this.nOnline = (int)szMsg[1];
		this.dOnlTime = DateTime.Parse(szMsg[2].ToString());
		this.nIsFcmCheck = (int)szMsg[3];
		this.nFcmNum = (int)szMsg[4];
		bool flag = this.nFcmOpen == 1 && this.nIsFcmCheck == 1;
		if (flag)
		{
			this.CheckTipTime();
		}
		bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag2)
		{
			MainUICon.inst.SetFcmShow();
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000A0404 File Offset: 0x0009E604
	public void SetIsGongwang()
	{
		bool flag = Fun.m_nIsTopUp() != 1;
		if (!flag)
		{
			List<HeroUnlockData> list = new List<HeroUnlockData>(fGameManager.mloadConfig.AllHeroUnlock.Values);
			for (int i = 0; i < list.Count; i++)
			{
				bool flag2 = list[i].nIsShow == 0;
				if (flag2)
				{
					fGameManager.mloadConfig.AllHeroUnlock.Remove(list[i].key);
				}
			}
		}
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000A0484 File Offset: 0x0009E684
	public void GetPlayerOldReward()
	{
		string value = (!WebParam.isExeLogin) ? PlayerPrefs.GetString(GameRoot.inst.serverSaveStr() + "uName") : WebParam.client_account;
		this.SendFcmDataMsgToServer(11006, new ArrayList
		{
			PlayerDataManager.getInstance().HUserData.m_nCid,
			value
		});
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000A04EE File Offset: 0x0009E6EE
	public void GetPlayerOldRewardBack(ArrayList szMsg)
	{
		this.szOldReward = (ArrayList)szMsg.Clone();
		this.ShowOldRewardTip(null);
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000A050C File Offset: 0x0009E70C
	private void ShowOldRewardTip(object szParam)
	{
		bool flag = this.szOldReward.Count > 0;
		if (flag)
		{
			int num = (int)this.szOldReward[0];
			ArrayList arrayList = (ArrayList)this.szOldReward[1];
			int num2 = (int)arrayList[0];
			TipManager.GetInstance().ShowRewardTip(4, new ArrayList
			{
				num,
				num2
			}, new TipManager.TipOKDelegate(this.ShowOldRewardTip), null);
			this.szOldReward.RemoveRange(0, 2);
		}
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000A05A8 File Offset: 0x0009E7A8
	public void OpenUnlockTip(int heroid, int ntype, int nid, string suser = "", int nreason = 0)
	{
		string[] array = null;
		string[] array2 = null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		bool flag = !(suser == string.Empty);
		OpenHeroData oneHeroData = this.GetOneHeroData(heroid);
		HeroLimitBuyInfo heroLimitInfo = this.GetHeroLimitInfo(heroid, ntype, nid);
		HeroDiscountInfo heroDiscountInfo = this.GetHeroDiscountInfo(heroid, ntype, nid, flag);
		string lang = Language.GetLang(8009, new object[0]);
		string value = string.Empty;
		string value2 = string.Empty;
		string value3 = string.Empty;
		string text = string.Empty;
		string lang2 = Language.GetLang(19006, new object[0]);
		string value4 = string.Empty;
		bool flag2 = heroDiscountInfo == null;
		int num6;
		string value5;
		if (flag2)
		{
			num6 = 0;
			value5 = string.Empty;
		}
		else
		{
			num6 = heroDiscountInfo.nDiscount;
			value5 = heroDiscountInfo.sDesc;
		}
		switch (ntype)
		{
		case 0:
			value = ((!flag) ? oneHeroData.unlockData.sUnlockDesc : oneHeroData.unlockData.sGiftDesc);
			array2 = oneHeroData.unlockData.szUnLock;
			array = oneHeroData.unlockData.szGiftData;
			num3 = oneHeroData.nGiftTime;
			num4 = oneHeroData.unlockData.nGiftNum;
			num5 = oneHeroData.unlockData.nIsZhekou;
			break;
		case 1:
			value = ((!flag) ? oneHeroData.unlockData.szSkinUnlock[nid].sActDesc : oneHeroData.unlockData.szSkinUnlock[nid].sGiftDesc);
			array2 = oneHeroData.unlockData.szSkinUnlock[nid].unlockData;
			array = oneHeroData.unlockData.szSkinUnlock[nid].szGiftData;
			num3 = oneHeroData.unlockData.szSkinUnlock[nid].nPieceNum;
			num4 = oneHeroData.unlockData.szSkinUnlock[nid].nGiftNum;
			num5 = oneHeroData.unlockData.szSkinUnlock[nid].nIsZhekou;
			break;
		case 3:
			value = ((!flag) ? oneHeroData.unlockData.szEmotionUnlock[nid].sActDesc : oneHeroData.unlockData.szEmotionUnlock[nid].sGiftDesc);
			array2 = oneHeroData.unlockData.szEmotionUnlock[nid].unlockData;
			array = oneHeroData.unlockData.szEmotionUnlock[nid].szGiftData;
			num3 = oneHeroData.unlockData.szEmotionUnlock[nid].nPieceNum;
			num4 = oneHeroData.unlockData.szEmotionUnlock[nid].nGiftNum;
			num5 = oneHeroData.unlockData.szEmotionUnlock[nid].nIsZhekou;
			break;
		}
		bool flag3 = heroLimitInfo != null;
		if (flag3)
		{
			value = ((!flag) ? heroLimitInfo.sUnlockDesc : heroLimitInfo.sGiftDesc);
			array2 = heroLimitInfo.szUnLock;
			array = heroLimitInfo.szGiftData;
			num4 = heroLimitInfo.nGiftNum;
			num5 = 0;
		}
		bool flag4 = heroDiscountInfo != null;
		if (flag4)
		{
			array2 = heroDiscountInfo.szUnlock;
			num5 = 0;
		}
		bool flag5 = array == null || array2 == null || int.Parse(array[0]) == 0 || array2.Length < 2;
		if (!flag5)
		{
			for (int i = 0; i < array2.Length; i += 2)
			{
				bool flag6 = int.Parse(array2[i]) == 2;
				if (flag6)
				{
					num = int.Parse(array2[i + 1]);
				}
				bool flag7 = int.Parse(array2[i]) == 5;
				if (flag7)
				{
					num2 = int.Parse(array2[i + 1]);
				}
			}
			bool flag8 = num == 0 && num2 == 0;
			if (!flag8)
			{
				bool flag9 = !flag;
				if (flag9)
				{
					bool flag10 = num > 0;
					if (flag10)
					{
						value3 = Language.GetLang(7042, new object[0]);
					}
					bool flag11 = num2 > 0;
					if (flag11)
					{
						text = Language.GetLang(7043, new object[0]);
					}
				}
				else
				{
					for (int j = 1; j < array.Length; j++)
					{
						bool flag12 = int.Parse(array[j]) == 2;
						if (flag12)
						{
							value3 = Language.GetLang(7045, new object[0]);
						}
						bool flag13 = int.Parse(array[j]) == 5;
						if (flag13)
						{
							text = Language.GetLang(7046, new object[0]);
						}
					}
				}
				int num7 = (PlayerDataManager.getInstance().playerdet.gameCurrency >= num2 && (!flag || num3 < num4)) ? 1 : 0;
				bool flag14 = text != string.Empty && PlayerDataManager.getInstance().playerdet.gameCurrency < num2;
				if (flag14)
				{
					value2 = Language.GetLang(7027, new object[0]);
				}
				bool flag15 = text != string.Empty && flag;
				if (flag15)
				{
					value4 = Language.GetLang(7051, new object[]
					{
						num4 - num3
					});
				}
				ArrayList value6 = new ArrayList
				{
					lang,
					value,
					value2,
					value3,
					text,
					lang2,
					value4,
					value5,
					num6
				};
				ArrayList value7 = new ArrayList
				{
					heroid,
					ntype,
					nid,
					suser,
					1,
					2,
					num,
					nreason
				};
				ArrayList value8 = new ArrayList
				{
					heroid,
					ntype,
					nid,
					suser,
					num7,
					5,
					num2,
					nreason
				};
				bool flag16 = easy_tipCon.inst != null;
				if (flag16)
				{
					this.TipLoadOk("easy_tip", easy_tipCon.inst.gameObject, new ArrayList
					{
						value6,
						value7,
						value8,
						num5
					});
				}
				else
				{
					fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.TipLoadOk), new ArrayList
					{
						value6,
						value7,
						value8,
						num5
					}, true);
				}
			}
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x000A0C94 File Offset: 0x0009EE94
	private void TipLoadOk(string p, GameObject gameObject, object szParam)
	{
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)(szParam as ArrayList)[0];
			ArrayList aparam = (ArrayList)(szParam as ArrayList)[1];
			ArrayList bparam = (ArrayList)(szParam as ArrayList)[2];
			int iszhekou = (int)(szParam as ArrayList)[3];
			easy_tipCon.inst.ShowUnlockTip(arrayList[0].ToString(), arrayList[1].ToString(), arrayList[2].ToString(), arrayList[3].ToString(), arrayList[4].ToString(), arrayList[5].ToString(), arrayList[6].ToString(), new easy_tipCon.UnlockADelegate(this.UnlockHeroData), aparam, new easy_tipCon.UnlockBDelegate(this.UnlockHeroData), bparam, arrayList[7].ToString(), (int)arrayList[8], iszhekou);
		}
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000A0D94 File Offset: 0x0009EF94
	private void UnlockHeroData(object szParam, bool ischk)
	{
		ArrayList arrayList = (ArrayList)szParam;
		int num = (int)arrayList[0];
		int num2 = (int)arrayList[1];
		int num3 = (int)arrayList[2];
		string value = arrayList[3].ToString();
		int num4 = (int)arrayList[5];
		int num5 = (int)arrayList[6];
		int num6 = (int)arrayList[7];
		int num7 = ischk ? 1 : 0;
		bool flag = num4 == 2;
		if (flag)
		{
			if (ischk)
			{
				bool flag2 = ShopManager.GetInstance().GetHaveItemNum(1027) < 1;
				if (flag2)
				{
					return;
				}
				ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
				num5 = Mathf.CeilToInt((float)(num5 * itemData.nZhekou) / 100f);
			}
			bool flag3 = PlayerDataManager.getInstance().playerdet.chargeCurrency < num5;
			if (flag3)
			{
				fUIManager.GetInstance().OpenChongZhiUI("chongzhi");
				return;
			}
		}
		bool flag4 = string.IsNullOrEmpty(value);
		if (flag4)
		{
			bool flag5 = num2 == 0;
			if (flag5)
			{
				this.SendHeroDataMsgToServer(12001, new ArrayList
				{
					num,
					num4,
					num6,
					num7
				});
			}
			else
			{
				this.SendHeroDataMsgToServer(12007, new ArrayList
				{
					num,
					num2,
					num3,
					num4,
					num7
				});
			}
		}
		else
		{
			this.SendHeroDataMsgToServer(12016, new ArrayList
			{
				num,
				num2,
				num3,
				num4,
				value,
				num7
			});
			bool flag6 = InviteUICon.inst != null;
			if (flag6)
			{
				InviteUICon.inst.ShowUserList();
			}
		}
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
	private void OnGiftOpen(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int num2 = (int)szMsg[1];
		int num3 = (int)szMsg[2];
		string value = Fun.DecodingUTF8(szMsg[3].ToString());
		TipManager.GetInstance().ShowRewardTip(6, new ArrayList
		{
			num,
			num2,
			num3,
			value
		}, null, null);
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x000A1070 File Offset: 0x0009F270
	private void OnGiftOpenBack(ArrayList szMsg)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = (int)szMsg[0];
		int num2 = num;
		if (num2 != -1)
		{
			if (num2 != 1)
			{
				int num3 = (int)szMsg[1];
				int num4 = (int)szMsg[2];
				int key = (int)szMsg[3];
				text = Fun.DecodingUTF8(szMsg[4].ToString());
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[num3];
				OpenHeroData oneHeroData = this.GetOneHeroData(num3);
				bool flag = num4 == 0;
				if (flag)
				{
					text2 = heroData.hname;
				}
				bool flag2 = num4 == 1;
				if (flag2)
				{
					text2 = oneHeroData.unlockData.szSkinUnlock[key].sName;
				}
				bool flag3 = num4 == 3;
				if (flag3)
				{
					text2 = oneHeroData.unlockData.szEmotionUnlock[key].sName;
				}
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15014, new object[]
				{
					text,
					text2
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15013, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000A1250 File Offset: 0x0009F450
	public HeroDiscountInfo GetHeroDiscountInfo(int nheroid, int ntype, int nindex, bool isgift = false)
	{
		HeroDiscountInfo heroDiscountInfo = null;
		ActivityData activityData = null;
		bool flag = ntype == 0;
		if (flag)
		{
			activityData = ActivityManager.GetInstance().GetActDataByType(30);
		}
		bool flag2 = ntype == 1;
		if (flag2)
		{
			activityData = ActivityManager.GetInstance().GetActDataByType(47);
		}
		bool flag3 = activityData == null;
		HeroDiscountInfo result;
		if (flag3)
		{
			result = heroDiscountInfo;
		}
		else
		{
			bool flag4 = !fGameManager.mloadConfig.AllHeroDiscountData.ContainsKey(activityData.nXmlId);
			if (flag4)
			{
				result = heroDiscountInfo;
			}
			else
			{
				HeroDiscountData heroDiscountData = fGameManager.mloadConfig.AllHeroDiscountData[activityData.nXmlId];
				string key = string.Format("{0}_{1}_{2}", nheroid, ntype, nindex);
				bool flag5 = !heroDiscountData.allDiscountInfo.ContainsKey(key);
				if (flag5)
				{
					result = heroDiscountInfo;
				}
				else
				{
					heroDiscountInfo = heroDiscountData.allDiscountInfo[key];
					bool flag6 = !isgift || (isgift && heroDiscountInfo.nGiftuse == 1);
					if (flag6)
					{
						result = heroDiscountInfo;
					}
					else
					{
						result = null;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000A1358 File Offset: 0x0009F558
	public HeroLimitBuyInfo GetHeroLimitInfo(int nheroid, int ntype, int nindex)
	{
		HeroLimitBuyInfo heroLimitBuyInfo = null;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(36);
		bool flag = actDataByType == null;
		HeroLimitBuyInfo result;
		if (flag)
		{
			result = heroLimitBuyInfo;
		}
		else
		{
			bool flag2 = !fGameManager.mloadConfig.AllHeroLimitBuyData.ContainsKey(actDataByType.nXmlId);
			if (flag2)
			{
				result = heroLimitBuyInfo;
			}
			else
			{
				HeroLimitBuyData heroLimitBuyData = fGameManager.mloadConfig.AllHeroLimitBuyData[actDataByType.nXmlId];
				string key = string.Format("{0}_{1}_{2}", nheroid, ntype, nindex);
				bool flag3 = !heroLimitBuyData.allLimitBuyInfo.ContainsKey(key);
				if (flag3)
				{
					result = heroLimitBuyInfo;
				}
				else
				{
					result = heroLimitBuyData.allLimitBuyInfo[key];
				}
			}
		}
		return result;
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000A140C File Offset: 0x0009F60C
	public int GetEquipZhekou(int nSlcHeroId, int nSlcType, int nSlcId)
	{
		int result = 0;
		HeroUnlockData heroUnlockData = null;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nSlcHeroId);
		if (flag)
		{
			heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[nSlcHeroId];
		}
		switch (nSlcType)
		{
		case 0:
			result = heroUnlockData.nIsZhekou;
			break;
		case 1:
			result = heroUnlockData.szSkinUnlock[nSlcId].nIsZhekou;
			break;
		case 2:
			result = heroUnlockData.szWeaponUnlock[nSlcId].nIsZhekou;
			break;
		case 3:
			result = heroUnlockData.szEmotionUnlock[nSlcId].nIsZhekou;
			break;
		case 4:
			result = heroUnlockData.szPoseUnlock[nSlcId].nIsZhekou;
			break;
		case 5:
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nSlcId];
			result = playerFaceData.nIsZhekou;
			break;
		}
		case 6:
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[nSlcId];
			result = playerPaintData.nIsZhekou;
			break;
		}
		}
		return result;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x000A1510 File Offset: 0x0009F710
	public void OpenPaintTip(int paintid, string suser)
	{
		int num = 0;
		int num2 = 0;
		bool flag = !(suser == string.Empty);
		PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[paintid];
		string lang = Language.GetLang(8009, new object[0]);
		string value = string.Empty;
		string value2 = string.Empty;
		string text = string.Empty;
		string lang2 = Language.GetLang(19006, new object[0]);
		string value3 = string.Empty;
		string value4 = (!flag) ? playerPaintData.sActDesc : playerPaintData.sGiftDesc;
		string[] unlockData = playerPaintData.unlockData;
		string[] szGiftData = playerPaintData.szGiftData;
		int nGiftTime = playerPaintData.nGiftTime;
		int nGiftNum = playerPaintData.nGiftNum;
		int nIsZhekou = playerPaintData.nIsZhekou;
		bool flag2 = szGiftData.Length < 2 || unlockData.Length < 2 || int.Parse(szGiftData[0]) == 0;
		if (!flag2)
		{
			for (int i = 0; i < unlockData.Length; i += 2)
			{
				bool flag3 = int.Parse(unlockData[i]) == 2;
				if (flag3)
				{
					num = int.Parse(unlockData[i + 1]);
				}
				bool flag4 = int.Parse(unlockData[i]) == 5;
				if (flag4)
				{
					num2 = int.Parse(unlockData[i + 1]);
				}
			}
			bool flag5 = num == 0 && num2 == 0;
			if (!flag5)
			{
				bool flag6 = !flag;
				if (flag6)
				{
					bool flag7 = num > 0;
					if (flag7)
					{
						value2 = Language.GetLang(7042, new object[0]);
					}
					bool flag8 = num2 > 0;
					if (flag8)
					{
						text = Language.GetLang(7043, new object[0]);
					}
				}
				else
				{
					for (int j = 1; j < szGiftData.Length; j++)
					{
						bool flag9 = int.Parse(szGiftData[j]) == 2;
						if (flag9)
						{
							value2 = Language.GetLang(7045, new object[0]);
						}
						bool flag10 = int.Parse(szGiftData[j]) == 5;
						if (flag10)
						{
							text = Language.GetLang(7046, new object[0]);
						}
					}
				}
				int num3 = (PlayerDataManager.getInstance().playerdet.gameCurrency >= num2 && (!flag || nGiftTime < nGiftNum)) ? 1 : 0;
				bool flag11 = text != string.Empty && PlayerDataManager.getInstance().playerdet.gameCurrency < num2;
				if (flag11)
				{
					value = Language.GetLang(7027, new object[0]);
				}
				bool flag12 = text != string.Empty && flag;
				if (flag12)
				{
					value3 = Language.GetLang(7051, new object[]
					{
						nGiftNum - nGiftTime
					});
				}
				ArrayList value5 = new ArrayList
				{
					lang,
					value4,
					value,
					value2,
					text,
					lang2,
					value3,
					string.Empty,
					0,
					nIsZhekou
				};
				ArrayList value6 = new ArrayList
				{
					0,
					6,
					paintid,
					suser,
					1,
					2,
					num,
					0
				};
				ArrayList value7 = new ArrayList
				{
					0,
					6,
					paintid,
					suser,
					num3,
					5,
					num2,
					0
				};
				bool flag13 = easy_tipCon.inst != null;
				if (flag13)
				{
					this.PaintTipLoadOk("easy_tip", easy_tipCon.inst.gameObject, new ArrayList
					{
						value5,
						value6,
						value7
					});
				}
				else
				{
					fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.PaintTipLoadOk), new ArrayList
					{
						value5,
						value6,
						value7
					}, true);
				}
			}
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x000A19A4 File Offset: 0x0009FBA4
	private void PaintTipLoadOk(string p, GameObject gameObject, object szParam)
	{
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)(szParam as ArrayList)[0];
			ArrayList aparam = (ArrayList)(szParam as ArrayList)[1];
			ArrayList bparam = (ArrayList)(szParam as ArrayList)[2];
			easy_tipCon.inst.ShowUnlockTip(arrayList[0].ToString(), arrayList[1].ToString(), arrayList[2].ToString(), arrayList[3].ToString(), arrayList[4].ToString(), arrayList[5].ToString(), arrayList[6].ToString(), new easy_tipCon.UnlockADelegate(this.UnlockPaintData), aparam, new easy_tipCon.UnlockBDelegate(this.UnlockPaintData), bparam, arrayList[7].ToString(), (int)arrayList[8], (int)arrayList[9]);
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000A1A9C File Offset: 0x0009FC9C
	private void UnlockPaintData(object szParam, bool ischk)
	{
		ArrayList arrayList = (ArrayList)szParam;
		int num = (int)arrayList[2];
		string value = arrayList[3].ToString();
		int num2 = (int)arrayList[5];
		int num3 = (int)arrayList[6];
		int num4 = ischk ? 1 : 0;
		bool flag = num2 == 2;
		if (flag)
		{
			if (ischk)
			{
				bool flag2 = ShopManager.GetInstance().GetHaveItemNum(1027) < 1;
				if (flag2)
				{
					return;
				}
				ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
				num3 = Mathf.CeilToInt((float)(num3 * itemData.nZhekou) / 100f);
			}
			bool flag3 = PlayerDataManager.getInstance().playerdet.chargeCurrency < num3;
			if (flag3)
			{
				fUIManager.GetInstance().OpenChongZhiUI("chongzhi");
				return;
			}
		}
		bool flag4 = string.IsNullOrEmpty(value);
		if (flag4)
		{
			this.SendHeroDataMsgToServer(11009, new ArrayList
			{
				num,
				num2,
				num4
			});
		}
		else
		{
			this.SendHeroDataMsgToServer(11011, new ArrayList
			{
				num,
				num2,
				value,
				num4
			});
			bool flag5 = InviteUICon.inst != null;
			if (flag5)
			{
				InviteUICon.inst.ShowUserList();
			}
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x000A1C2C File Offset: 0x0009FE2C
	private void OnGiftOpenPaint(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		string value = Fun.DecodingUTF8(szMsg[1].ToString());
		TipManager.GetInstance().ShowRewardTip(6, new ArrayList
		{
			0,
			6,
			num,
			value
		}, null, null);
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x000A1C9C File Offset: 0x0009FE9C
	private void OnGiftOpenPaintBack(ArrayList szMsg)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = (int)szMsg[0];
		int num2 = num;
		if (num2 != -1)
		{
			if (num2 != 1)
			{
				int key = (int)szMsg[1];
				text = Fun.DecodingUTF8(szMsg[2].ToString());
				PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[key];
				text2 = playerPaintData.sName;
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15014, new object[]
				{
					text,
					text2
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15013, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x000A1E00 File Offset: 0x000A0000
	public void OpenFaceTip(int faceid, string suser)
	{
		int num = 0;
		int num2 = 0;
		bool flag = !(suser == string.Empty);
		PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[faceid];
		string lang = Language.GetLang(8009, new object[0]);
		string value = string.Empty;
		string value2 = string.Empty;
		string text = string.Empty;
		string lang2 = Language.GetLang(19006, new object[0]);
		string value3 = string.Empty;
		string value4 = (!flag) ? playerFaceData.sOpenDesc : playerFaceData.sGiftDesc;
		string[] szUnLock = playerFaceData.szUnLock;
		string[] szGiftData = playerFaceData.szGiftData;
		int nGiftTime = playerFaceData.nGiftTime;
		int nGiftNum = playerFaceData.nGiftNum;
		int nIsZhekou = playerFaceData.nIsZhekou;
		bool flag2 = szGiftData.Length < 2 || szUnLock.Length < 2 || int.Parse(szGiftData[0]) == 0;
		if (!flag2)
		{
			for (int i = 0; i < szUnLock.Length; i += 2)
			{
				bool flag3 = int.Parse(szUnLock[i]) == 2;
				if (flag3)
				{
					num = int.Parse(szUnLock[i + 1]);
				}
				bool flag4 = int.Parse(szUnLock[i]) == 5;
				if (flag4)
				{
					num2 = int.Parse(szUnLock[i + 1]);
				}
			}
			bool flag5 = num == 0 && num2 == 0;
			if (!flag5)
			{
				bool flag6 = !flag;
				if (flag6)
				{
					bool flag7 = num > 0;
					if (flag7)
					{
						value2 = Language.GetLang(7042, new object[0]);
					}
					bool flag8 = num2 > 0;
					if (flag8)
					{
						text = Language.GetLang(7043, new object[0]);
					}
				}
				else
				{
					for (int j = 1; j < szGiftData.Length; j++)
					{
						bool flag9 = int.Parse(szGiftData[j]) == 2;
						if (flag9)
						{
							value2 = Language.GetLang(7045, new object[0]);
						}
						bool flag10 = int.Parse(szGiftData[j]) == 5;
						if (flag10)
						{
							text = Language.GetLang(7046, new object[0]);
						}
					}
				}
				int num3 = (PlayerDataManager.getInstance().playerdet.gameCurrency >= num2 && (!flag || nGiftTime < nGiftNum)) ? 1 : 0;
				bool flag11 = text != string.Empty && PlayerDataManager.getInstance().playerdet.gameCurrency < num2;
				if (flag11)
				{
					value = Language.GetLang(7027, new object[0]);
				}
				bool flag12 = text != string.Empty && flag;
				if (flag12)
				{
					value3 = Language.GetLang(7051, new object[]
					{
						nGiftNum - nGiftTime
					});
				}
				ArrayList value5 = new ArrayList
				{
					lang,
					value4,
					value,
					value2,
					text,
					lang2,
					value3,
					string.Empty,
					0,
					nIsZhekou
				};
				ArrayList value6 = new ArrayList
				{
					0,
					5,
					faceid,
					suser,
					1,
					2,
					num,
					0
				};
				ArrayList value7 = new ArrayList
				{
					0,
					5,
					faceid,
					suser,
					num3,
					5,
					num2,
					0
				};
				bool flag13 = easy_tipCon.inst != null;
				if (flag13)
				{
					this.FaceTipLoadOk("easy_tip", easy_tipCon.inst.gameObject, new ArrayList
					{
						value5,
						value6,
						value7
					});
				}
				else
				{
					fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.FaceTipLoadOk), new ArrayList
					{
						value5,
						value6,
						value7
					}, true);
				}
			}
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x000A2294 File Offset: 0x000A0494
	private void FaceTipLoadOk(string p, GameObject gameObject, object szParam)
	{
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)(szParam as ArrayList)[0];
			ArrayList aparam = (ArrayList)(szParam as ArrayList)[1];
			ArrayList bparam = (ArrayList)(szParam as ArrayList)[2];
			easy_tipCon.inst.ShowUnlockTip(arrayList[0].ToString(), arrayList[1].ToString(), arrayList[2].ToString(), arrayList[3].ToString(), arrayList[4].ToString(), arrayList[5].ToString(), arrayList[6].ToString(), new easy_tipCon.UnlockADelegate(this.UnlockFaceData), aparam, new easy_tipCon.UnlockBDelegate(this.UnlockFaceData), bparam, arrayList[7].ToString(), (int)arrayList[8], (int)arrayList[9]);
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000A238C File Offset: 0x000A058C
	private void UnlockFaceData(object szParam, bool ischk)
	{
		ArrayList arrayList = (ArrayList)szParam;
		int num = (int)arrayList[2];
		string value = arrayList[3].ToString();
		int num2 = (int)arrayList[5];
		int num3 = (int)arrayList[6];
		int num4 = ischk ? 1 : 0;
		bool flag = num2 == 2;
		if (flag)
		{
			if (ischk)
			{
				bool flag2 = ShopManager.GetInstance().GetHaveItemNum(1027) < 1;
				if (flag2)
				{
					return;
				}
				ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
				num3 = Mathf.CeilToInt((float)(num3 * itemData.nZhekou) / 100f);
			}
			bool flag3 = PlayerDataManager.getInstance().playerdet.chargeCurrency < num3;
			if (flag3)
			{
				fUIManager.GetInstance().OpenChongZhiUI("chongzhi");
				return;
			}
		}
		bool flag4 = string.IsNullOrEmpty(value);
		if (flag4)
		{
			this.SendHeroDataMsgToServer(11002, new ArrayList
			{
				num,
				num2,
				num4
			});
		}
		else
		{
			this.SendHeroDataMsgToServer(11013, new ArrayList
			{
				num,
				num2,
				value,
				num4
			});
			bool flag5 = InviteUICon.inst != null;
			if (flag5)
			{
				InviteUICon.inst.ShowUserList();
			}
		}
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x000A251C File Offset: 0x000A071C
	private void OnGiftOpenFace(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		string value = Fun.DecodingUTF8(szMsg[1].ToString());
		TipManager.GetInstance().ShowRewardTip(6, new ArrayList
		{
			0,
			5,
			num,
			value
		}, null, null);
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x000A258C File Offset: 0x000A078C
	private void OnGiftOpenFaceBack(ArrayList szMsg)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = (int)szMsg[0];
		int num2 = num;
		if (num2 != -1)
		{
			if (num2 != 1)
			{
				int key = (int)szMsg[1];
				text = Fun.DecodingUTF8(szMsg[2].ToString());
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
				text2 = playerFaceData.sFaceName;
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15014, new object[]
				{
					text,
					text2
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15013, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(15015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x000A26F0 File Offset: 0x000A08F0
	private void InitHeroParts(ArrayList szData)
	{
		for (int i = 0; i < szData.Count; i += 2)
		{
			int key = (int)szData[i];
			HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[key];
			heroPartsData.SetData((ArrayList)szData[i + 1]);
		}
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x000A2748 File Offset: 0x000A0948
	private void HeroPartsLvlUp(ArrayList szData)
	{
		int num = (int)szData[0];
		int addvalue = (int)szData[2];
		int num2 = (int)szData[3];
		HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[num];
		heroPartsData.SetData((ArrayList)szData[1]);
		bool flag = HeroWeaponCon.inst != null;
		if (flag)
		{
			bool flag2 = num2 > 0;
			if (flag2)
			{
				HeroWeaponCon.inst.ResetLvlUpTab(2);
			}
			else
			{
				HeroWeaponCon.inst.ResetLvlUpTab(1);
			}
			HeroWeaponCon.inst.PlayLvlUpEff(num, addvalue, num2);
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000A27EC File Offset: 0x000A09EC
	private void HeroPartsOpen(ArrayList szData)
	{
		int nindex = (int)szData[0];
		int key = (int)szData[1];
		HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[key];
		heroPartsData.SetData((ArrayList)szData[2]);
		bool flag = HeroWeaponCon.inst != null;
		if (flag)
		{
			HeroWeaponCon.inst.ShowPartsLevelUp(nindex);
		}
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000A2858 File Offset: 0x000A0A58
	private void HeroPartsRemove(ArrayList szData)
	{
		int num = (int)szData[0];
		bool flag = num == 1;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(7094, new object[0]), "ffdd00", "system");
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7094, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x04000DA0 RID: 3488
	private static HeroDataManager instance;

	// Token: 0x04000DA1 RID: 3489
	public Dictionary<int, OpenHeroData> allOpenHero = new Dictionary<int, OpenHeroData>();

	// Token: 0x04000DA2 RID: 3490
	private List<string> allHeroTpsModel = new List<string>();

	// Token: 0x04000DA3 RID: 3491
	private List<string> allHeroFpsModel = new List<string>();

	// Token: 0x04000DA4 RID: 3492
	private List<string> allHeroWeapon = new List<string>();

	// Token: 0x04000DA5 RID: 3493
	public const int FCM_KICKTIME = 10800;

	// Token: 0x04000DA6 RID: 3494
	public const int FCM_FIRSTTIP = 3600;

	// Token: 0x04000DA7 RID: 3495
	public const int FCM_SECONDTIP = 7200;

	// Token: 0x04000DA8 RID: 3496
	public const int FCM_THRIDTIP = 10500;

	// Token: 0x04000DA9 RID: 3497
	public const int FCM_RESETTIME = 18000;

	// Token: 0x04000DAA RID: 3498
	public int nFcmOpen;

	// Token: 0x04000DAB RID: 3499
	public int nIsFcmCheck;

	// Token: 0x04000DAC RID: 3500
	public string sIDCard;

	// Token: 0x04000DAD RID: 3501
	public int nOnline;

	// Token: 0x04000DAE RID: 3502
	public DateTime dOnlTime;

	// Token: 0x04000DAF RID: 3503
	public int nFcmNum;

	// Token: 0x04000DB0 RID: 3504
	public int nFcmTip;

	// Token: 0x04000DB1 RID: 3505
	private int onlineCount;

	// Token: 0x04000DB2 RID: 3506
	private ArrayList szOldReward = new ArrayList();
}

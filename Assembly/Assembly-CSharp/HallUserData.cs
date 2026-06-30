using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class HallUserData
{
	// Token: 0x06001045 RID: 4165 RVA: 0x00098068 File Offset: 0x00096268
	public static HallUserData getInstance()
	{
		bool flag = HallUserData.instance == null;
		if (flag)
		{
			HallUserData.instance = new HallUserData();
		}
		return HallUserData.instance;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x00098098 File Offset: 0x00096298
	public void InitHUserBase(ArrayList szBase)
	{
		this.m_nCid = (int)szBase[0];
		this.m_sName = Fun.DecodingUTF8(szBase[1].ToString());
		this.m_nSex = (int)szBase[2];
		bool flag = (int)szBase[3] != 0;
		if (flag)
		{
			this.m_nFace = (int)szBase[3];
		}
		this.m_nNational = (int)szBase[4];
		this.m_nProfession = (int)szBase[5];
		this.m_nLevel = (int)szBase[6];
		this.m_nExp = (int)szBase[7];
		this.m_nGM = (int)szBase[8];
		this.m_nPunNum = (int)szBase[23];
		this.m_nPunNumBak = this.m_nPunNum;
		this.m_szReadUpMsgID = (ArrayList)szBase[24];
		this.m_nChangeName = (int)szBase[25];
		this.m_dRegDate = DateTime.Parse(szBase[14].ToString());
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x000981C0 File Offset: 0x000963C0
	public void InitFriendData(ArrayList szFriend, ArrayList szLately, ArrayList szFriendmsg)
	{
		for (int i = 0; i < szFriend.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(szFriend[i].ToString());
			if (flag)
			{
				szFriend[i] = Fun.DecodingUTF8(szFriend[i].ToString());
				ArrayList arrayList = (ArrayList)szFriend[i + 1];
				arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
			}
		}
		this.m_szFriendData = szFriend;
		for (int j = 0; j < szLately.Count; j += 2)
		{
			bool flag2 = !string.IsNullOrEmpty(szLately[j].ToString());
			if (flag2)
			{
				szLately[j] = Fun.DecodingUTF8(szLately[j].ToString());
				ArrayList arrayList2 = (ArrayList)szLately[j + 1];
				arrayList2[1] = Fun.DecodingUTF8(arrayList2[1].ToString());
			}
		}
		this.m_szFriendLately = szLately;
		for (int k = 0; k < szFriendmsg.Count; k += 2)
		{
			bool flag3 = !string.IsNullOrEmpty(szFriendmsg[k].ToString());
			if (flag3)
			{
				ArrayList arrayList3 = (ArrayList)szFriendmsg[k + 1];
				arrayList3[1] = Fun.DecodingUTF8(arrayList3[1].ToString());
			}
		}
		this.m_szFriendUnMsg = szFriendmsg;
		bool flag4 = MainUICon.inst != null;
		if (flag4)
		{
			MainUICon.inst.UpdateUnMsgNum();
		}
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0009835C File Offset: 0x0009655C
	public bool OnIsFriend(string sName)
	{
		int num = this.m_szFriendData.IndexOf(sName);
		return num != -1;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x00098390 File Offset: 0x00096590
	public void UserUpExp(int nExp, int nLevel, int nPunnum)
	{
		bool flag = this.m_nLevel < 5 && nLevel >= 5;
		if (flag)
		{
			GameRoot.mPreLoadManager.StartPreLoad();
		}
		bool flag2 = this.m_nLevel < PveManager.GetInstance().m_nOpenLv && nLevel >= PveManager.GetInstance().m_nOpenLv;
		if (flag2)
		{
			PveManager.GetInstance().OnSetMaoXianBtnState();
		}
		this.m_nLevel = nLevel;
		this.m_nExp = nExp;
		this.m_nPunNumBak = this.m_nPunNum;
		this.m_nPunNum = nPunnum;
		bool flag3 = MainUICon.inst != null;
		if (flag3)
		{
			MainUICon.inst.SetPunUI();
			MainUICon.inst.SetLzAwardState();
			MainUICon.inst.SetQQAwardState();
			MainUICon.inst.UpdateLv();
		}
		bool flag4 = MatchModelCon.inst != null;
		if (flag4)
		{
			MatchModelCon.inst.SetLvShow();
		}
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x00098474 File Offset: 0x00096674
	public void InitShopData(ArrayList arr)
	{
		this.UpdateShopItem((ArrayList)arr[0]);
		this.UpdateItemBuff((ArrayList)arr[1]);
		this.UpdateItem((ArrayList)arr[2]);
		ShopManager.GetInstance().InitLiBaoNum();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.CreateBuffIco();
			MainUICon.inst.ShowLiBaoNum(ShopManager.GetInstance().nLiBaoNum);
		}
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000984F7 File Offset: 0x000966F7
	public void UpdateShopItem(ArrayList arr)
	{
		this.m_szShopItemData = arr;
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x00098501 File Offset: 0x00096701
	public void UpdateItem(ArrayList arr)
	{
		this.m_szItemData = arr;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0009850B File Offset: 0x0009670B
	public void UpdateItemBuff(ArrayList arr)
	{
		this.m_szShopItemBuff = arr;
		ShopManager.GetInstance().BuffItemData();
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00098520 File Offset: 0x00096720
	public void InitTaskData(ArrayList arr)
	{
		TaskManager.GetInstance().taskdata.InitTaskData((ArrayList)arr[0], arr[1].ToString());
		this.m_nEnergy = (int)arr[3];
		PveManager.GetInstance().SetPveDataByFbType(9, (ArrayList)arr[4]);
		PveManager.GetInstance().StrengthenData.InitData((ArrayList)arr[5]);
		PveManager.GetInstance().SetPveDataByFbType(12, (ArrayList)arr[6]);
		TaskManager.GetInstance().taskdata.InitChapterData((ArrayList)arr[7]);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x000985D2 File Offset: 0x000967D2
	public void SetRoomID(int roomuid)
	{
		this.m_nRoomUID = roomuid;
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x000985DC File Offset: 0x000967DC
	public void SetCareerData(ArrayList arr)
	{
		this.m_szUserCareerData = (ArrayList)arr[0];
		this.m_szHeroCareerData = (ArrayList)arr[1];
		this.m_szHeroCareerHighData = (ArrayList)arr[2];
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x00098618 File Offset: 0x00096818
	public void HallToWebMsg(int netid, ArrayList szMsg)
	{
		bool flag = netid == 4009;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szMsg[0];
			TipManager.GetInstance().ShowComTipMsg(arrayList[1].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), arrayList[3].ToString().Replace("&lt;", "<").Replace("&gt;", ">").Replace("\\\\n", "\n"), Language.GetLang(25007, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.ClickGetWebReward), (int)arrayList[0], null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x00098700 File Offset: 0x00096900
	private void ClickGetWebReward(object szParam)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(1308, new ArrayList
			{
				4009,
				new ArrayList
				{
					(int)szParam,
					0
				}
			});
		}
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0009876D File Offset: 0x0009696D
	private void InitGameOverData(ArrayList szGameOverData)
	{
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00098770 File Offset: 0x00096970
	public void OnGmActionMsg(int netId, ArrayList szMsg)
	{
		switch (netId)
		{
		case 8201:
		{
			bool flag = GMUICon.inst != null && GMUICon.inst.visible;
			if (flag)
			{
				GMUICon.inst.setPlayerPos(szMsg);
			}
			break;
		}
		case 8203:
		{
			bool flag2 = GMUICon.inst != null && GMUICon.inst.visible;
			if (flag2)
			{
				bool flag3 = (int)szMsg[0] == 2;
				if (flag3)
				{
					GMUICon.inst.SetPlayerNumTxt((int)szMsg[0], szMsg, -1);
				}
				else
				{
					GMUICon.inst.SetPlayerNumTxt((int)szMsg[0], szMsg, (int)szMsg[1]);
				}
			}
			break;
		}
		case 8204:
		{
			bool flag4 = GMUICon.inst != null && GMUICon.inst.visible;
			if (flag4)
			{
				GMUICon.inst.SetSkyMatchNum((ArrayList)szMsg[0], (ArrayList)szMsg[1]);
			}
			break;
		}
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00098898 File Offset: 0x00096A98
	public void InitUpMsg(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		this.m_szNewUpMsg = (ArrayList)szMsg[1];
		bool flag = this.m_szNewUpMsg.Count == 0 || num != 1;
		if (!flag)
		{
			int num2 = (int)this.m_szNewUpMsg[0];
			bool flag2 = this.m_szReadUpMsgID.IndexOf(num2) >= 0;
			if (!flag2)
			{
				string text = this.m_szNewUpMsg[3].ToString();
				bool flag3 = !(text == string.Empty);
				if (flag3)
				{
					text += " 00:00:01";
					int num3 = Mathf.Abs(Fun.GetEndCDTime(text));
					bool flag4 = num3 / 3600 <= 168;
					if (flag4)
					{
					}
				}
			}
		}
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x00098974 File Offset: 0x00096B74
	public void OnChangeNickBack(ArrayList szMsg)
	{
		bool flag = (int)szMsg[0] == 1;
		if (flag)
		{
			this.m_sName = Fun.DecodingUTF8(szMsg[1].ToString());
			this.m_nChangeName = (int)szMsg[2];
			bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
			if (flag2)
			{
				MainUICon.inst.SetPlayerInfo();
			}
			fUIManager.GetInstance().SetWinActive("CreateRoleUI", false);
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(46073, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnClickExit), null, null, null);
		}
		else
		{
			bool flag3 = CreateRoleUICon.inst != null && CreateRoleUICon.inst.visible;
			if (flag3)
			{
				CreateRoleUICon.inst.ShowTipMsg(new ArrayList
				{
					1016,
					new ArrayList
					{
						0,
						0,
						0,
						szMsg[1].ToString()
					}
				});
			}
		}
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x00098AE0 File Offset: 0x00096CE0
	public void OnResetChangeNick(ArrayList szMsg)
	{
		this.m_nChangeName = (int)szMsg[0];
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetPlayerInfo();
		}
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x00098B2C File Offset: 0x00096D2C
	private void OnClickExit(object szParam)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		bool flag2 = fGameManager.Heroplayer != null;
		if (flag2)
		{
			fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		Application.Quit();
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x00098BBC File Offset: 0x00096DBC
	public void InitDtsData(ArrayList arr)
	{
		bool flag = arr.Count >= 1;
		if (flag)
		{
			this.m_sDtsDayTime = arr[0].ToString();
			this.m_nDtsFristGold = (int)arr[4];
		}
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00098C00 File Offset: 0x00096E00
	public void InitCommonData(ArrayList arr)
	{
		PveManager.GetInstance().StrengthenData.SetGeneData((ArrayList)arr[14]);
	}

	// Token: 0x04000CF1 RID: 3313
	private static HallUserData instance;

	// Token: 0x04000CF2 RID: 3314
	public int autoid;

	// Token: 0x04000CF3 RID: 3315
	public int m_nCid;

	// Token: 0x04000CF4 RID: 3316
	public string m_sName;

	// Token: 0x04000CF5 RID: 3317
	public int m_nSex;

	// Token: 0x04000CF6 RID: 3318
	public int m_nFace;

	// Token: 0x04000CF7 RID: 3319
	public int m_nUsePaint;

	// Token: 0x04000CF8 RID: 3320
	public int m_nNational;

	// Token: 0x04000CF9 RID: 3321
	public int m_nProfession;

	// Token: 0x04000CFA RID: 3322
	public int m_nLevel;

	// Token: 0x04000CFB RID: 3323
	public int m_nExp;

	// Token: 0x04000CFC RID: 3324
	public int m_nGM;

	// Token: 0x04000CFD RID: 3325
	public int m_nChangeName;

	// Token: 0x04000CFE RID: 3326
	public int m_nRoomUID;

	// Token: 0x04000CFF RID: 3327
	public int m_nTeamID;

	// Token: 0x04000D00 RID: 3328
	public int m_nPunNum = -1;

	// Token: 0x04000D01 RID: 3329
	public int m_nPunNumBak;

	// Token: 0x04000D02 RID: 3330
	public ArrayList m_szFriendData = new ArrayList();

	// Token: 0x04000D03 RID: 3331
	public ArrayList m_szFriendLately = new ArrayList();

	// Token: 0x04000D04 RID: 3332
	public ArrayList m_szFriendUnMsg = new ArrayList();

	// Token: 0x04000D05 RID: 3333
	public ArrayList m_szTeamUser = new ArrayList();

	// Token: 0x04000D06 RID: 3334
	public ArrayList m_szShopItemData = new ArrayList();

	// Token: 0x04000D07 RID: 3335
	public ArrayList m_szItemData = new ArrayList();

	// Token: 0x04000D08 RID: 3336
	public ArrayList m_szShopItemBuff = new ArrayList();

	// Token: 0x04000D09 RID: 3337
	public ArrayList m_szUserCareerData = new ArrayList();

	// Token: 0x04000D0A RID: 3338
	public ArrayList m_szHeroCareerData = new ArrayList();

	// Token: 0x04000D0B RID: 3339
	public ArrayList m_szHeroCareerHighData = new ArrayList();

	// Token: 0x04000D0C RID: 3340
	public ArrayList m_szWebReward = new ArrayList();

	// Token: 0x04000D0D RID: 3341
	public string m_sOnLoginDate = string.Empty;

	// Token: 0x04000D0E RID: 3342
	public ArrayList m_szReadUpMsgID = new ArrayList();

	// Token: 0x04000D0F RID: 3343
	public ArrayList m_szNewUpMsg = new ArrayList();

	// Token: 0x04000D10 RID: 3344
	public int m_nEnergy;

	// Token: 0x04000D11 RID: 3345
	public DateTime m_dRegDate;

	// Token: 0x04000D12 RID: 3346
	public string m_sDtsDayTime = string.Empty;

	// Token: 0x04000D13 RID: 3347
	public int m_nDtsFristGold;
}

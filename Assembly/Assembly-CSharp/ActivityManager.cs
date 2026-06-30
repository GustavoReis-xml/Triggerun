using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000006 RID: 6
public class ActivityManager
{
	// Token: 0x0600000A RID: 10 RVA: 0x000024D4 File Offset: 0x000006D4
	public static ActivityManager GetInstance()
	{
		bool flag = ActivityManager.instance == null;
		if (flag)
		{
			ActivityManager.instance = new ActivityManager();
		}
		return ActivityManager.instance;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002504 File Offset: 0x00000704
	public void OnActivityMsg(ArrayList szNetMsg)
	{
		switch ((int)szNetMsg[0])
		{
		case 16100:
			this.AddActData((ArrayList)szNetMsg[1]);
			break;
		case 16101:
			this.UpdateActData((ArrayList)szNetMsg[1]);
			break;
		case 16103:
			this.JoinTestDataInit((ArrayList)szNetMsg[1]);
			break;
		case 16104:
			this.JoinTestResult((ArrayList)szNetMsg[1]);
			break;
		case 16105:
			this.OpenUIShowData((ArrayList)szNetMsg[1]);
			break;
		case 16106:
			this.ResetSevenTargetData((ArrayList)szNetMsg[1]);
			break;
		case 16108:
			this.GetLuckyDrawData((ArrayList)szNetMsg[1]);
			break;
		case 16109:
			this.SetLuckyDrawData((ArrayList)szNetMsg[1]);
			break;
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000260C File Offset: 0x0000080C
	private void AddActData(ArrayList szMsg)
	{
		ArrayList arrayList = (ArrayList)szMsg[0];
		this.sServerStart = szMsg[1].ToString();
		this.sServerMerge = szMsg[2].ToString();
		for (int i = 0; i < arrayList.Count; i++)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i];
			bool flag = arrayList2.Count > 0;
			if (flag)
			{
				int num = (int)arrayList2[0];
				bool flag2 = this.allActData.ContainsKey(num);
				ActivityData activityData;
				if (flag2)
				{
					activityData = this.allActData[num];
				}
				else
				{
					activityData = new ActivityData(num);
					this.allActData.Add(num, activityData);
				}
				activityData.SetData(arrayList2);
			}
		}
		bool flag3 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag3)
		{
			MainUICon.inst.CreateOtherBuffIco();
		}
		bool flag4 = LiBaoManager.GetInstance() != null;
		if (flag4)
		{
			LiBaoManager.GetInstance().CheckLiBaoCanGet();
			LiBaoManager.GetInstance().CheckNationalActReward();
			LiBaoManager.GetInstance().CheckJieRiActReward();
			LiBaoManager.GetInstance().CheckFuliCanGet();
		}
		bool flag5 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag5)
		{
			MainUICon.inst.Set_Carnival_State();
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002774 File Offset: 0x00000974
	public void UpdateActData(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int key = (int)szMsg[1];
		int num2 = (int)szMsg[2];
		ArrayList data = (ArrayList)szMsg[3];
		bool flag = this.allActData.ContainsKey(key);
		if (flag)
		{
			this.allActData[key].SetData(data);
		}
		int num3 = num2;
		int num4 = num3;
		if (num4 <= 12)
		{
			if (num4 != 2)
			{
				if (num4 != 3)
				{
					if (num4 == 12)
					{
						bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
						if (flag2)
						{
							MainUICon.inst.CreateOtherBuffIco();
						}
					}
				}
				else
				{
					bool flag3 = MainUICon.inst != null && MainUICon.inst.visible;
					if (flag3)
					{
						MainUICon.inst.SetTeacherActBtn();
					}
				}
			}
			else
			{
				bool flag4 = this.allActData[key].nIsActOpen == 0 && MainUICon.inst != null && MainUICon.inst.visible;
				if (flag4)
				{
					MainUICon.inst.SetTiantiBtn();
				}
			}
		}
		else if (num4 <= 28)
		{
			if (num4 != 16)
			{
				if (num4 == 28)
				{
					bool flag5 = MainUICon.inst != null && MainUICon.inst.visible;
					if (flag5)
					{
						MainUICon.inst.SetLuckyDrawBtn();
					}
				}
			}
			else
			{
				bool flag6 = MainUICon.inst != null && MainUICon.inst.visible;
				if (flag6)
				{
					MainUICon.inst.Set_Carnival_State();
				}
			}
		}
		else if (num4 != 30)
		{
			if (num4 - 41 <= 1)
			{
				this.ReSetFbOpenState();
			}
		}
		else
		{
			bool flag7 = MainUICon.inst != null && MainUICon.inst.visible;
			if (flag7)
			{
				MainUICon.inst.SetHeroDiscountBtn();
			}
		}
		bool flag8 = LiBaoManager.GetInstance() != null;
		if (flag8)
		{
			LiBaoManager.GetInstance().CheckLiBaoCanGet();
			LiBaoManager.GetInstance().CheckNationalActReward();
			LiBaoManager.GetInstance().CheckJieRiActReward();
			LiBaoManager.GetInstance().CheckFuliCanGet();
		}
		bool flag9 = NationalDayUICon.inst != null && NationalDayUICon.inst.visible;
		if (flag9)
		{
			NationalDayUICon.inst.Reset();
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000029EC File Offset: 0x00000BEC
	private void ReSetFbOpenState()
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (!flag)
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch == 1 && this.IsOpenLimit(MatchManager.GetInstance().m_nMatchFbType) == 0 && (TeamManager.GetInstance().m_nTeamID == 0 || (TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1));
			if (flag2)
			{
				MatchManager.GetInstance().OnCancelMatch();
			}
			bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag3)
			{
				RoomData proomData = RoomManage.GetInstance().PRoomData;
				bool flag4 = proomData != null && this.IsOpenLimit((int)proomData.GetRoomInfo(5)) == 0 && (TeamManager.GetInstance().m_nTeamID == 0 || (TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1));
				if (flag4)
				{
					RoomManage.GetInstance().OnLeave();
				}
			}
			bool flag5 = MatchModelCon.inst != null && MatchModelCon.inst.visible;
			if (flag5)
			{
				MatchModelCon.inst.SetLvShow();
			}
			bool flag6 = FindRoomCon.inst != null && FindRoomCon.inst.visible;
			if (flag6)
			{
				FindRoomCon.inst.SetRoomInfo(0);
			}
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002B40 File Offset: 0x00000D40
	public List<int> GetActIdByType(int acttype)
	{
		List<int> list = new List<int>();
		foreach (ActivityData activityData in this.allActData.Values)
		{
			bool flag = acttype == activityData.nActType;
			if (flag)
			{
				list.Add(activityData.nActId);
			}
		}
		return list;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002BC0 File Offset: 0x00000DC0
	public int GetOpenActIdByType(int acttype)
	{
		int result = 0;
		foreach (ActivityData activityData in this.allActData.Values)
		{
			bool flag = acttype == activityData.nActType && activityData.nIsActOpen == 1;
			if (flag)
			{
				result = activityData.nActId;
			}
		}
		return result;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002C44 File Offset: 0x00000E44
	public ActivityData GetActDataById(int actid)
	{
		ActivityData result = null;
		bool flag = this.allActData.ContainsKey(actid);
		if (flag)
		{
			result = this.allActData[actid];
		}
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002C78 File Offset: 0x00000E78
	public ActivityData GetActDataByType(int acttype)
	{
		ActivityData result = null;
		foreach (ActivityData activityData in this.allActData.Values)
		{
			bool flag = acttype == activityData.nActType && activityData.nIsActOpen == 1;
			if (flag)
			{
				result = activityData;
			}
		}
		return result;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002CF4 File Offset: 0x00000EF4
	public void SendActMsgToServer(int nNetID, ArrayList arr)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(nNetID);
			arrayList.Add(arr);
			fGameManager.LobbyPlayer.SendMsgToHallServer(16100, arrayList);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002D3C File Offset: 0x00000F3C
	public int GetActDataXML_ID(int acttype)
	{
		int openActIdByType = this.GetOpenActIdByType(acttype);
		bool flag = openActIdByType != 0;
		if (flag)
		{
			ActivityData actDataById = this.GetActDataById(openActIdByType);
			bool flag2 = actDataById != null;
			if (flag2)
			{
				return actDataById.nXmlId;
			}
		}
		return 0;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002D80 File Offset: 0x00000F80
	public void JoinTestDataInit(ArrayList szMsg)
	{
		ArrayList arrayList = (ArrayList)szMsg[0];
		this.JoinTestButState = false;
		bool flag = arrayList.Count > 0;
		if (flag)
		{
			this.JoinTestActData = arrayList;
			bool flag2 = (int)arrayList[4] == 1;
			if (flag2)
			{
				bool flag3 = WebParam.client_jointeststate == 0;
				if (flag3)
				{
					this.JoinTestButState = true;
				}
				else
				{
					this.JoinTestButState = false;
				}
			}
			else
			{
				this.JoinTestButState = false;
			}
		}
		bool flag4 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag4)
		{
			bool joinTestButState = this.JoinTestButState;
			if (joinTestButState)
			{
				MainUICon.inst.ShoworHiddenJoinTestBUt(true);
			}
			else
			{
				MainUICon.inst.ShoworHiddenJoinTestBUt(false);
			}
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002E44 File Offset: 0x00001044
	private void JoinTestResult(ArrayList szMsg)
	{
		int num = (int)szMsg[1];
		bool flag = jointestserverCon.inst != null && jointestserverCon.inst.visible;
		if (flag)
		{
			jointestserverCon.inst.Close();
		}
		bool flag2 = num == 1;
		if (flag2)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(94001, new object[0]), "ffdd00", "system");
			bool flag3 = MainUICon.inst != null && MainUICon.inst.visible;
			if (flag3)
			{
				WebParam.client_jointeststate = 1;
				MainUICon.inst.ShoworHiddenJoinTestBUt(false);
			}
		}
		else
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(94002, new object[0]), "ffdd00", "system");
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002F14 File Offset: 0x00001114
	private void OpenUIShowData(ArrayList szMsg)
	{
		ArrayList joinTestActData = (ArrayList)szMsg[0];
		this.JoinTestActData = joinTestActData;
		fUIManager.GetInstance().ShowWin("jointestserver", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002F4C File Offset: 0x0000114C
	private void ResetSevenTargetData(ArrayList szMsg)
	{
		this.m_szSevenTargetData = szMsg;
		bool flag = SevenTargetUICon.inst != null && SevenTargetUICon.inst.visible;
		if (flag)
		{
			SevenTargetUICon.inst.ResetSevenTargetData();
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002F8C File Offset: 0x0000118C
	public ArrayList GetSeventTargetDay()
	{
		bool flag = this.szSevenTargetByDay.Count > 0;
		ArrayList result;
		if (flag)
		{
			result = this.szSevenTargetByDay;
		}
		else
		{
			foreach (SevenTargetData sevenTargetData in fGameManager.mloadConfig.AllSevenTarget.Values)
			{
				int num = this.szSevenTargetByDay.IndexOf(sevenTargetData.nDay);
				bool flag2 = num != -1;
				if (flag2)
				{
					ArrayList arrayList = (ArrayList)this.szSevenTargetByDay[num + 1];
					arrayList.Add(sevenTargetData);
				}
				else
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(sevenTargetData);
					this.szSevenTargetByDay.Add(sevenTargetData.nDay);
					this.szSevenTargetByDay.Add(arrayList);
				}
			}
			result = this.szSevenTargetByDay;
		}
		return result;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00003094 File Offset: 0x00001294
	public ArrayList GetSevenTargetInfo(int nid)
	{
		ArrayList result = new ArrayList();
		int num = this.m_szSevenTargetData.IndexOf(nid);
		bool flag = num != -1;
		if (flag)
		{
			result = (ArrayList)this.m_szSevenTargetData[num + 1];
		}
		return result;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000030E0 File Offset: 0x000012E0
	private void GetLuckyDrawData(ArrayList szData)
	{
		this.m_szLuckyDrawRank = szData;
		bool flag = this.m_szLuckyDrawRank.Count > 0 && LuckyDrawUICon.inst != null;
		if (flag)
		{
			LuckyDrawUICon.inst.AddRankMsg(0);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003124 File Offset: 0x00001324
	private void SetLuckyDrawData(ArrayList szData)
	{
		this.m_szLuckyDrawRank.Add(szData);
		bool flag = this.m_szLuckyDrawRank.Count > 50;
		if (flag)
		{
			this.m_szLuckyDrawRank.RemoveAt(0);
		}
		bool flag2 = LuckyDrawUICon.inst != null;
		if (flag2)
		{
			LuckyDrawUICon.inst.AddRankMsg(1);
		}
		bool flag3 = (int)szData[3] != 1;
		if (!flag3)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[(int)szData[1]];
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(28011, new object[]
			{
				szData[0].ToString(),
				Fun.GetQualColor(itemData.m_nQuality),
				itemData.m_sName,
				szData[2].ToString()
			}), "f2ede7", "choujiang");
			bool flag4 = szData[0].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag4)
			{
				bool flag5 = (int)szData[4] == 1;
				if (flag5)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39535, new object[]
					{
						itemData.m_sName,
						itemData.nHuiGou
					}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				fUISoundManager.instance.PlayUISound("ui_huodong_zhongjian", 0, null, 20);
			}
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000032C8 File Offset: 0x000014C8
	public int IsOpenLimit(int fbtype)
	{
		FbOpenSetData fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[0];
		bool flag = fbOpenSetData.szFbType.IndexOf(fbtype) == -1;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			ActivityData actDataByType = this.GetActDataByType(41);
			bool flag2 = actDataByType != null;
			if (flag2)
			{
				fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[actDataByType.nXmlId];
				bool flag3 = fbOpenSetData.szFbType.IndexOf(fbtype) != -1;
				if (flag3)
				{
					return 1;
				}
			}
			actDataByType = this.GetActDataByType(42);
			bool flag4 = actDataByType != null;
			if (flag4)
			{
				fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[actDataByType.nXmlId];
				bool flag5 = fbOpenSetData.szFbType.IndexOf(fbtype) != -1;
				if (flag5)
				{
					return 1;
				}
			}
			result = 0;
		}
		return result;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003398 File Offset: 0x00001598
	public string GetFbOpenTime(int fbtype)
	{
		List<int> list = new List<int>();
		FbOpenSetData fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[0];
		bool flag = fbOpenSetData.szFbType.IndexOf(fbtype) == -1;
		string empty;
		if (flag)
		{
			empty = string.Empty;
		}
		else
		{
			list = this.GetActIdByType(41);
			bool flag2 = list.Count > 0;
			if (flag2)
			{
				ActivityData activityData = this.allActData[list[0]];
				fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[activityData.nXmlId];
				bool flag3 = fbOpenSetData.szFbType.IndexOf(fbtype) != -1;
				if (flag3)
				{
					return activityData.sStartTime;
				}
			}
			list = this.GetActIdByType(42);
			bool flag4 = list.Count > 0;
			if (flag4)
			{
				ActivityData activityData2 = this.allActData[list[0]];
				fbOpenSetData = fGameManager.mloadConfig.AllFbOpenSetData[activityData2.nXmlId];
				bool flag5 = fbOpenSetData.szFbType.IndexOf(fbtype) != -1;
				if (flag5)
				{
					return activityData2.sStartTime;
				}
			}
			empty = string.Empty;
		}
		return empty;
	}

	// Token: 0x04000019 RID: 25
	public const int ACT_TYPE_OPENHERO = 1;

	// Token: 0x0400001A RID: 26
	public const int ACT_TYPE_SKYLADDER = 2;

	// Token: 0x0400001B RID: 27
	public const int ACT_TYPE_TEACHER = 3;

	// Token: 0x0400001C RID: 28
	public const int ACT_TYPE_USERLV = 10;

	// Token: 0x0400001D RID: 29
	public const int ACT_TYPE_SIGN = 11;

	// Token: 0x0400001E RID: 30
	public const int ACT_TYPE_EXPBUFF = 12;

	// Token: 0x0400001F RID: 31
	public const int ACT_TYPE_CDK = 13;

	// Token: 0x04000020 RID: 32
	public const int ACT_TYPE_CARNIVAL = 16;

	// Token: 0x04000021 RID: 33
	public const int ACT_TYPE_ONLINE = 17;

	// Token: 0x04000022 RID: 34
	public const int ACT_TYPE_ONTIME = 18;

	// Token: 0x04000023 RID: 35
	public const int ACT_TYPE_SKYACT = 19;

	// Token: 0x04000024 RID: 36
	public const int ACT_TYPE_WEEKLOGIN = 20;

	// Token: 0x04000025 RID: 37
	public const int ACT_TYPE_SKYEVERYDAY = 21;

	// Token: 0x04000026 RID: 38
	public const int ACT_TYPE_NEWSOLDIER = 22;

	// Token: 0x04000027 RID: 39
	public const int ACT_TYPE_WEEKENDONLINE = 23;

	// Token: 0x04000028 RID: 40
	public const int ACT_TYPE_KAIXUEGIFT = 24;

	// Token: 0x04000029 RID: 41
	public const int ACT_TYPE_MATCHREWARD = 25;

	// Token: 0x0400002A RID: 42
	public const int ACT_TYPE_GODDESSGIFT = 26;

	// Token: 0x0400002B RID: 43
	public const int ACT_TYPE_WEEKCHARGE = 27;

	// Token: 0x0400002C RID: 44
	public const int ACT_TYPE_LUCKYDRAW = 28;

	// Token: 0x0400002D RID: 45
	public const int ACT_TYPE_BOGOACT = 29;

	// Token: 0x0400002E RID: 46
	public const int ACT_TYPE_HERODISCOUNT = 30;

	// Token: 0x0400002F RID: 47
	public const int ACT_TYPE_RECHARGEZUAN = 31;

	// Token: 0x04000030 RID: 48
	public const int ACT_TYPE_NATIONALDAYLIANXU = 32;

	// Token: 0x04000031 RID: 49
	public const int ACT_TYPE_NAMATCHREWARD = 33;

	// Token: 0x04000032 RID: 50
	public const int ACT_TYPE_RECHARGEITEM = 34;

	// Token: 0x04000033 RID: 51
	public const int ACT_TYPE_DISCOUNTITEM = 35;

	// Token: 0x04000034 RID: 52
	public const int ACT_TYPE_HEROLIMITBUY = 36;

	// Token: 0x04000035 RID: 53
	public const int ACT_TYPE_HUILIUACT = 37;

	// Token: 0x04000036 RID: 54
	public const int ACT_TYPE_HERORETURN = 38;

	// Token: 0x04000037 RID: 55
	public const int ACT_TYPE_DISCOUNTSHOP = 39;

	// Token: 0x04000038 RID: 56
	public const int ACT_TYPE_TUIJIAN = 40;

	// Token: 0x04000039 RID: 57
	public const int ACT_TYPE_FBOPENODD = 41;

	// Token: 0x0400003A RID: 58
	public const int ACT_TYPE_FBOPENEVEN = 42;

	// Token: 0x0400003B RID: 59
	public const int ACT_TYPE_WISHTREE = 43;

	// Token: 0x0400003C RID: 60
	public const int ACT_TYPE_PVPWEEKREWARD = 44;

	// Token: 0x0400003D RID: 61
	public const int ACT_TYPE_MULTIBOGO = 45;

	// Token: 0x0400003E RID: 62
	public const int ACT_TYPE_WINTERACT = 46;

	// Token: 0x0400003F RID: 63
	public const int ACT_TYPE_SKINDISCOUNT = 47;

	// Token: 0x04000040 RID: 64
	public const int ACT_TYPE_JIERILOGIN = 48;

	// Token: 0x04000041 RID: 65
	private static ActivityManager instance;

	// Token: 0x04000042 RID: 66
	private Dictionary<int, ActivityData> allActData = new Dictionary<int, ActivityData>();

	// Token: 0x04000043 RID: 67
	public string sServerStart;

	// Token: 0x04000044 RID: 68
	public string sServerMerge;

	// Token: 0x04000045 RID: 69
	public bool JoinTestButState;

	// Token: 0x04000046 RID: 70
	public ArrayList JoinTestActData = new ArrayList();

	// Token: 0x04000047 RID: 71
	public ArrayList szSevenTargetByDay = new ArrayList();

	// Token: 0x04000048 RID: 72
	public ArrayList m_szLuckyDrawRank = new ArrayList();

	// Token: 0x04000049 RID: 73
	private ArrayList m_szSevenTargetData = new ArrayList();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class TeamManager
{
	// Token: 0x06002C33 RID: 11315 RVA: 0x001840C4 File Offset: 0x001822C4
	public static TeamManager GetInstance()
	{
		bool flag = TeamManager.instance == null;
		if (flag)
		{
			TeamManager.instance = new TeamManager();
		}
		return TeamManager.instance;
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x001840F4 File Offset: 0x001822F4
	public void InitTeam()
	{
		int nTeamID = this.m_nTeamID;
		this.m_nTeamID = 0;
		this.m_nCaptain = 0;
		this.Dyaoqing.Clear();
		this.MyTeamData.Clear();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.DeTeamObj();
		}
		bool flag2 = nTeamID != 0;
		if (flag2)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(60010);
			arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
			arrayList.Add(PlayerDataManager.getInstance().HUserData.m_nCid);
			arrayList.Add(nTeamID);
			fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
		}
		else
		{
			this.LoadTeamOK = true;
		}
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x001841D0 File Offset: 0x001823D0
	public void OnTeamMsg(ArrayList szNetMsg)
	{
		switch ((int)szNetMsg[0])
		{
		case 60001:
			this.OnCreateOK(szNetMsg);
			break;
		case 60002:
			this.OnJoinTeam((ArrayList)szNetMsg[1]);
			break;
		case 60003:
			this.OnLeaveTeam((ArrayList)szNetMsg[1]);
			break;
		case 60004:
			this.OnChangeCap((string)szNetMsg[1], (int)szNetMsg[2], (ArrayList)szNetMsg[3], (int)szNetMsg[4]);
			break;
		case 60005:
			this.OnInviteTeam((ArrayList)szNetMsg[1]);
			break;
		case 60006:
			TeamManager.GetInstance().DelImgCD(Fun.DecodingUTF8(szNetMsg[2].ToString()));
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40003, new object[]
			{
				Fun.DecodingUTF8(szNetMsg[2].ToString())
			}), "ffdd00", "system");
			break;
		case 60007:
		{
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			this.DelImgCD(Fun.DecodingUTF8(arrayList[1].ToString()));
			bool flag = (int)arrayList[0] == -1;
			if (flag)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40012, new object[0]), "ffdd00", "system");
			}
			break;
		}
		case 60008:
			TeamManager.GetInstance().RetuenMsg(szNetMsg);
			break;
		case 60009:
			this.OnKickUser((ArrayList)szNetMsg[1]);
			break;
		case 60010:
			this.OnGetTeamInfo((ArrayList)szNetMsg[1]);
			break;
		case 60012:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40029, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			break;
		case 60016:
		{
			ArrayList arrayList2 = (ArrayList)(szNetMsg[1] as ArrayList).Clone();
			for (int i = 0; i < arrayList2.Count; i += 2)
			{
				bool flag2 = !string.IsNullOrEmpty(arrayList2[i].ToString());
				if (flag2)
				{
					arrayList2[i] = Fun.DecodingUTF8(arrayList2[i].ToString());
					ArrayList arrayList3 = (ArrayList)arrayList2[i + 1];
					arrayList3[0] = Fun.DecodingUTF8(arrayList3[0].ToString());
				}
			}
			this.MyTeamData = arrayList2;
			break;
		}
		case 60018:
		{
			bool flag3 = LookingTeamCon.inst != null;
			if (flag3)
			{
				LookingTeamCon.inst.SetLookingTeamData((ArrayList)szNetMsg[1], (int)szNetMsg[2]);
			}
			break;
		}
		case 60019:
			this.CreateLookingTeam((int)szNetMsg[1]);
			break;
		case 60020:
			this.LeaveLookingTeamOK((ArrayList)szNetMsg[1]);
			break;
		case 60021:
			this.ApplyJoinTeam((ArrayList)szNetMsg[1]);
			break;
		case 60022:
			this.SetOneLookTeam(szNetMsg);
			break;
		case 60024:
			this.SetErrorMsg((ArrayList)szNetMsg[1]);
			break;
		case 60026:
		{
			ArrayList arrayList4 = (ArrayList)(szNetMsg[1] as ArrayList).Clone();
			ArrayList arrayList5;
			for (int j = 0; j < arrayList4.Count; j += 2)
			{
				bool flag4 = !string.IsNullOrEmpty(arrayList4[j].ToString());
				if (flag4)
				{
					arrayList4[j] = Fun.DecodingUTF8(arrayList4[j].ToString());
					arrayList5 = (ArrayList)arrayList4[j + 1];
					arrayList5[0] = Fun.DecodingUTF8(arrayList5[0].ToString());
				}
			}
			this.MyTeamData = arrayList4;
			arrayList5 = (ArrayList)szNetMsg[2];
			bool flag5 = MainUICon.inst != null;
			if (flag5)
			{
				MainUICon.inst.ChangeTeamMemberNick(Fun.DecodingUTF8(arrayList5[0].ToString()), Fun.DecodingUTF8(arrayList5[1].ToString()));
			}
			break;
		}
		}
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x0018468C File Offset: 0x0018288C
	private void SetErrorMsg(ArrayList arr)
	{
		int num = (int)arr[1];
		bool flag = num == -1;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40054, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		bool flag2 = num == -2;
		if (flag2)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40055, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06002C37 RID: 11319 RVA: 0x00184750 File Offset: 0x00182950
	public void LeaveLookingTeam(int nType, int nModel, int nFbtype, int nFbLevel)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(60020);
		arrayList.Add(nType);
		arrayList.Add(nModel);
		arrayList.Add(nFbtype);
		arrayList.Add(nFbLevel);
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
	}

	// Token: 0x06002C38 RID: 11320 RVA: 0x001847BB File Offset: 0x001829BB
	private void CreateLookingTeam(int nId)
	{
		this.lookingTeamId = nId;
	}

	// Token: 0x06002C39 RID: 11321 RVA: 0x001847C8 File Offset: 0x001829C8
	private void LeaveLookingTeamOK(ArrayList arr)
	{
		int num = (int)arr[1];
		this.lookingTeamId = 0;
		switch (num)
		{
		case 1:
			MatchManager.GetInstance().OnEnterTraining(null);
			break;
		case 2:
			MatchManager.GetInstance().OnEnterLearnFb(null);
			break;
		case 3:
		{
			bool flag = MatchModelCon.inst != null;
			if (flag)
			{
				MatchModelCon.inst.StartMatch_Go((int)arr[2]);
			}
			break;
		}
		case 4:
		{
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.SkyStartMatch();
			}
			break;
		}
		case 5:
		{
			bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID == 0;
			if (flag3)
			{
				RoomManage.GetInstance().OnCreateRoom(string.Empty, 4);
			}
			else
			{
				fUIManager.GetInstance().ShowWin("FindRoom", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(MainUICon.inst.OnLoadContentUIOk), null, true);
			}
			break;
		}
		case 6:
			PveManager.GetInstance().OnPveOk((int)arr[2], (int)arr[3], (int)arr[4], 1);
			break;
		}
	}

	// Token: 0x06002C3A RID: 11322 RVA: 0x0018491C File Offset: 0x00182B1C
	public void SetOneLookTeam(ArrayList arr)
	{
		bool flag = this.m_nTeamID != 0;
		if (flag)
		{
			this.lookingTeamId = (int)arr[1];
			bool flag2 = LookingTeamCon.inst != null;
			if (flag2)
			{
				LookingTeamCon.inst.SetOneLookTeam(arr);
			}
		}
	}

	// Token: 0x06002C3B RID: 11323 RVA: 0x00184968 File Offset: 0x00182B68
	public void ApplyJoinTeam(ArrayList szNetMsg)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(5);
		szNetMsg[1] = Fun.DecodingUTF8(szNetMsg[1].ToString());
		arrayList.Add(Language.GetLang(40048, new object[]
		{
			szNetMsg[1].ToString()
		}));
		arrayList.Add(szNetMsg);
		this.szTeamMsg = szNetMsg;
		fUIManager.GetInstance().OnPushMsg(arrayList, 20);
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x001849E8 File Offset: 0x00182BE8
	private void OnGetTeamInfo(ArrayList arr)
	{
		this.LoadTeamOK = true;
		bool flag = (int)arr[3] == 0;
		if (flag)
		{
			this.MyTeamData.Clear();
			this.m_nTeamID = 0;
			this.m_nCaptain = 0;
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.DeTeamObj();
			}
		}
		else
		{
			ArrayList arrayList = (ArrayList)(arr[1] as ArrayList).Clone();
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				bool flag3 = !string.IsNullOrEmpty(arrayList[i].ToString());
				if (flag3)
				{
					arrayList[i] = Fun.DecodingUTF8(arrayList[i].ToString());
					ArrayList arrayList2 = (ArrayList)arrayList[i + 1];
					arrayList2[0] = Fun.DecodingUTF8(arrayList2[0].ToString());
				}
			}
			this.MyTeamData = arrayList;
			bool flag4 = !this.MyTeamData.Contains(PlayerDataManager.getInstance().HUserData.m_sName);
			if (flag4)
			{
				this.MyTeamData.Clear();
				this.m_nTeamID = 0;
				this.m_nCaptain = 0;
				bool flag5 = MainUICon.inst != null;
				if (flag5)
				{
					MainUICon.inst.DeTeamObj();
				}
			}
			else
			{
				this.m_nTeamID = (int)arr[3];
				bool flag6 = Fun.DecodingUTF8(arr[4].ToString()) == PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag6)
				{
					this.m_nCaptain = 1;
				}
				bool flag7 = MainUICon.inst != null;
				if (flag7)
				{
					MainUICon.inst.InitTeamUI(this.MyTeamData, (int)arr[5], Fun.DecodingUTF8(arr[0].ToString()));
				}
			}
		}
	}

	// Token: 0x06002C3D RID: 11325 RVA: 0x00184BCC File Offset: 0x00182DCC
	private void OnCreateOK(ArrayList szNetMsg)
	{
		this.m_nTeamID = (int)szNetMsg[1];
		this.m_nCaptain = (int)szNetMsg[2];
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetTeamMaster(true);
		}
	}

	// Token: 0x06002C3E RID: 11326 RVA: 0x00184C1C File Offset: 0x00182E1C
	public void OnInviteTeam(ArrayList szNetMsg)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(2);
		szNetMsg[0] = Fun.DecodingUTF8(szNetMsg[0].ToString());
		bool flag = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag)
		{
			arrayList.Add(Language.GetLang(40011, new object[]
			{
				szNetMsg[0].ToString()
			}));
		}
		else
		{
			arrayList.Add(Language.GetLang(40010, new object[]
			{
				szNetMsg[0].ToString()
			}));
		}
		arrayList.Add(szNetMsg);
		this.szTeamMsg = szNetMsg;
		fUIManager.GetInstance().OnPushMsg(arrayList, 20);
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x00184CDC File Offset: 0x00182EDC
	private void OnJoinTeam(ArrayList szNetMsg)
	{
		ArrayList arrayList = (ArrayList)(szNetMsg[1] as ArrayList).Clone();
		for (int i = 0; i < arrayList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(arrayList[i].ToString());
			if (flag)
			{
				arrayList[i] = Fun.DecodingUTF8(arrayList[i].ToString());
				ArrayList arrayList2 = (ArrayList)arrayList[i + 1];
				arrayList2[0] = Fun.DecodingUTF8(arrayList2[0].ToString());
			}
		}
		this.MyTeamData = arrayList;
		this.m_nTeamID = (int)szNetMsg[2];
		string text = Fun.DecodingUTF8(szNetMsg[0].ToString());
		bool flag2 = MainUICon.inst != null;
		if (flag2)
		{
			MainUICon.inst.InitTeamUI(this.MyTeamData, (int)szNetMsg[3], text);
		}
		bool flag3 = text == PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag3)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40002, new object[]
			{
				Language.GetLang(47017, new object[0])
			}), "ffdd00", "system");
		}
		else
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40002, new object[]
			{
				text
			}), "ffdd00", "system");
		}
		this.DelUserYaoQing(text);
		bool flag4 = this.MyTeamData.Count >= 10;
		if (flag4)
		{
			TeamManager.GetInstance().Dyaoqing.Clear();
		}
		bool flag5 = VoiceGameCon.Inst != null;
		if (flag5)
		{
			VoiceGameCon.Inst.JoinVoice(PlayerDataManager.getInstance().HUserData.m_nCid, this.m_nTeamID, 1);
		}
		bool flag6 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag6)
		{
			VoiceUICon.inst.InitUI();
		}
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x00184EEC File Offset: 0x001830EC
	private void OnChangeCap(string sNick, int nCaptain, ArrayList szallcid, int nCid)
	{
		this.m_nCaptain = nCaptain;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnChangeTeamCap(nCid, szallcid);
		}
		ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40004, new object[]
		{
			Fun.DecodingUTF8(sNick)
		}), "ffdd00", "system");
	}

	// Token: 0x06002C41 RID: 11329 RVA: 0x00184F50 File Offset: 0x00183150
	private void OnLeaveTeam(ArrayList szNetMsg)
	{
		string text = Fun.DecodingUTF8(szNetMsg[0].ToString());
		ArrayList arrayList = (ArrayList)(szNetMsg[1] as ArrayList).Clone();
		for (int i = 0; i < arrayList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(arrayList[i].ToString());
			if (flag)
			{
				arrayList[i] = Fun.DecodingUTF8(arrayList[i].ToString());
				ArrayList arrayList2 = (ArrayList)(arrayList[i + 1] as ArrayList).Clone();
				arrayList2[0] = Fun.DecodingUTF8(arrayList2[0].ToString());
				arrayList[i + 1] = arrayList2;
			}
		}
		this.MyTeamData = arrayList;
		this.m_nTeamID = (int)szNetMsg[2];
		bool flag2 = MainUICon.inst != null;
		if (flag2)
		{
			MainUICon.inst.DelTeamUI(arrayList, (int)szNetMsg[3]);
		}
		bool flag3 = text == PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag3)
		{
			bool flag4 = this.m_nCaptain == 1;
			if (flag4)
			{
				bool flag5 = this.MyTeamData.Count > 0;
				if (flag5)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40008, new object[]
					{
						Language.GetLang(47017, new object[0])
					}), "ffdd00", "system");
				}
				bool flag6 = LookingTeamCon.inst != null && this.m_nTeamID == this.lookingTeamId;
				if (flag6)
				{
					LookingTeamCon.inst.RemoveTeamRender(this.lookingTeamId);
				}
			}
			else
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40008, new object[]
				{
					Language.GetLang(47017, new object[0])
				}), "ffdd00", "system");
			}
			this.m_nTeamID = 0;
			this.m_nCaptain = 0;
			this.lookingTeamId = 0;
			this.MyTeamData.Clear();
			bool flag7 = MainUICon.inst != null;
			if (flag7)
			{
				MainUICon.inst.DeTeamObj();
			}
			VoiceGameCon.Inst.LeaveVoice();
			bool flag8 = VoiceTalkCon.inst != null;
			if (flag8)
			{
				VoiceTalkCon.inst.DelAllUser();
			}
		}
		else
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40008, new object[]
			{
				text
			}), "ffdd00", "system");
		}
		bool flag9 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag9)
		{
			VoiceUICon.inst.InitUI();
		}
	}

	// Token: 0x06002C42 RID: 11330 RVA: 0x00185214 File Offset: 0x00183414
	private void OnKickUser(ArrayList szNetMsg)
	{
		szNetMsg[0] = Fun.DecodingUTF8(szNetMsg[0].ToString());
		ArrayList arrayList = (ArrayList)(szNetMsg[1] as ArrayList).Clone();
		for (int i = 0; i < arrayList.Count; i++)
		{
			bool flag = !string.IsNullOrEmpty(arrayList[i].ToString());
			if (flag)
			{
				arrayList[i] = Fun.DecodingUTF8(arrayList[i].ToString());
				ArrayList arrayList2 = (ArrayList)(arrayList[i + 1] as ArrayList).Clone();
				arrayList2[0] = Fun.DecodingUTF8(arrayList2[0].ToString());
				arrayList[i + 1] = arrayList2;
			}
		}
		this.MyTeamData = arrayList;
		this.m_nTeamID = (int)szNetMsg[2];
		bool flag2 = MainUICon.inst != null;
		if (flag2)
		{
			MainUICon.inst.DelTeamUI(arrayList, (int)szNetMsg[3]);
		}
		bool flag3 = szNetMsg[0].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag3)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40008, new object[]
			{
				Language.GetLang(47017, new object[0])
			}), "ffdd00", "system");
			this.m_nTeamID = 0;
			this.m_nCaptain = 0;
			this.lookingTeamId = 0;
			this.MyTeamData.Clear();
			bool flag4 = MainUICon.inst != null;
			if (flag4)
			{
				MainUICon.inst.DeTeamObj();
			}
			bool flag5 = VoiceTalkCon.inst != null;
			if (flag5)
			{
				VoiceTalkCon.inst.DelAllUser();
			}
			bool flag6 = VoiceGameCon.Inst != null;
			if (flag6)
			{
				VoiceGameCon.Inst.LeaveVoice();
			}
		}
		else
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40021, new object[]
			{
				szNetMsg[0].ToString()
			}), "ffdd00", "system");
		}
	}

	// Token: 0x06002C43 RID: 11331 RVA: 0x00185438 File Offset: 0x00183638
	public void LoadUIOk(string sUI, GameObject ob, object szParam)
	{
		ArrayList arrayList = (ArrayList)szParam;
		SystemMsgTipCon.inst.CreateMsgTip("队伍消息", Language.GetLang(40010, new object[]
		{
			arrayList[1].ToString()
		}), new ArrayList
		{
			"确定",
			"取消"
		}, 10, false, this.szTeamMsg, new SystemMsgTipCon.onOkClick(this.onMsgClick), 14, 14);
	}

	// Token: 0x06002C44 RID: 11332 RVA: 0x001854B4 File Offset: 0x001836B4
	public void onMsgClick(bool bl, int nState, ArrayList zPram)
	{
		ArrayList arrayList = (ArrayList)zPram.Clone();
		arrayList[0] = Fun.EncodingUTF8(arrayList[0].ToString());
		bool flag = nState == 1;
		if (flag)
		{
			bool flag2 = !MatchManager.GetInstance().isEnterFb;
			if (flag2)
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag3)
				{
					RoomManage.GetInstance().OnLeave();
				}
				bool flag4 = MatchManager.GetInstance().m_nIsMatch > 0;
				if (flag4)
				{
					MatchManager.GetInstance().OnCancelMatch();
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40027, new object[0]), "ffdd00", "system");
				}
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(60002);
				arrayList2.Add(arrayList);
				arrayList2.Add(0);
				fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList2);
				bool flag5 = MainUICon.inst != null;
				if (flag5)
				{
					MainUICon.inst.OnCheckTipMsg(2);
				}
			}
		}
		else
		{
			bool flag6 = fGameManager.Heroplayer != null;
			if (flag6)
			{
				HeroPlayer player = fGameManager.GetInstance().getPlayer();
				bool flag7 = player != null;
				if (flag7)
				{
					player.SendMessageToServer(60006, new ArrayList
					{
						60006,
						arrayList[0].ToString()
					});
				}
			}
			else
			{
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(60006);
				arrayList3.Add(arrayList[0].ToString());
				fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList3);
			}
		}
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x00185680 File Offset: 0x00183880
	public int IsShowMsg(OnePushMsgInfo pData)
	{
		int team_Invited_Type = GameRoot.inst.Setting.Social_Class.Team_Invited_Type;
		bool flag = team_Invited_Type == 1 && !FriendManager.GetInstance().IsFriendRlt(pData.szData[0].ToString());
		int result;
		if (flag)
		{
			TeamManager.GetInstance().onMsgClick(true, 0, pData.szData);
			result = 0;
		}
		else
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x001856EC File Offset: 0x001838EC
	public void OnLeaveTeam()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(60003);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
	}

	// Token: 0x06002C47 RID: 11335 RVA: 0x00185740 File Offset: 0x00183940
	public void OnYaoQingLeaveTeam()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(60025);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
	}

	// Token: 0x06002C48 RID: 11336 RVA: 0x00185791 File Offset: 0x00183991
	public void OnLoginLeave()
	{
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x00185794 File Offset: 0x00183994
	public void TeamInviteFriend(ArrayList szFriendlist)
	{
		bool flag = TeamManager.GetInstance().MyTeamData.Count / 2 + TeamManager.GetInstance().Dyaoqing.Count >= 5;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40005, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
			if (!flag2)
			{
				bool flag3 = TeamManager.GetInstance().MyTeamData.Count >= 10;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40005, new object[0]), "ffdd00", "system");
				}
				else
				{
					int count = TeamManager.GetInstance().MyTeamData.Count;
					int num = (count <= 0) ? 4 : (5 - count / 2);
					for (int i = 0; i < szFriendlist.Count; i++)
					{
						bool flag4 = num == 0;
						if (flag4)
						{
							break;
						}
						bool flag5 = TeamManager.GetInstance().Dyaoqing.Count >= 4;
						if (flag5)
						{
							break;
						}
						bool flag6 = TeamManager.GetInstance().Dyaoqing.ContainsKey(szFriendlist[i].ToString());
						if (flag6)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40032, new object[0]), "ffdd00", "system");
						}
						else
						{
							bool flag7 = MainUICon.inst != null;
							if (flag7)
							{
								MainUICon.inst.SetTeamCD(szFriendlist[i].ToString());
							}
							ArrayList arrayList = new ArrayList();
							arrayList.Add(60005);
							arrayList.Add(Fun.EncodingUTF8(szFriendlist[i].ToString()));
							fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
							num--;
						}
					}
					bool flag8 = szFriendlist.Count > 0;
					if (flag8)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40014, new object[0]), "ffdd00", "system");
					}
				}
			}
		}
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x001859D4 File Offset: 0x00183BD4
	public void RetuenMsg(ArrayList szMsg)
	{
		int num = (int)szMsg[3];
		bool flag = (int)szMsg[4] != 0;
		switch (num)
		{
		case -6:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40053, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			break;
		case -3:
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40005, new object[0]), "ffdd00", "system");
			bool flag2 = flag;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			break;
		}
		case -2:
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40019, new object[]
			{
				Fun.DecodingUTF8(szMsg[2].ToString())
			}), "ffdd00", "system");
			break;
		case -1:
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40018, new object[0]), "ffdd00", "system");
			bool flag3 = flag;
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40018, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			break;
		}
		}
	}

	// Token: 0x06002C4B RID: 11339 RVA: 0x00185BAC File Offset: 0x00183DAC
	public void DelImgCD(string sName)
	{
		this.DelUserYaoQing(sName);
		MainUICon.inst.GetTeamObj(sName);
	}

	// Token: 0x06002C4C RID: 11340 RVA: 0x00185BC4 File Offset: 0x00183DC4
	public void DelUserYaoQing(string sName)
	{
		int num = this.MyTeamData.Count / 2;
		bool flag = this.Dyaoqing.ContainsKey(sName);
		if (flag)
		{
			this.Dyaoqing.Remove(sName);
			bool flag2 = this.Dyaoqing.Count == 0 && num <= 1;
			if (flag2)
			{
				this.OnYaoQingLeaveTeam();
			}
		}
		MainUICon.inst.SortTeamObj();
	}

	// Token: 0x040025AB RID: 9643
	private static TeamManager instance;

	// Token: 0x040025AC RID: 9644
	public int m_nTeamID;

	// Token: 0x040025AD RID: 9645
	public int m_nCaptain;

	// Token: 0x040025AE RID: 9646
	public ArrayList szAddTeamInfo = new ArrayList();

	// Token: 0x040025AF RID: 9647
	public int m_nExitTeam;

	// Token: 0x040025B0 RID: 9648
	public ArrayList MyTeamData = new ArrayList();

	// Token: 0x040025B1 RID: 9649
	private ArrayList szTeamMsg = new ArrayList();

	// Token: 0x040025B2 RID: 9650
	public Dictionary<string, GameObject> Dyaoqing = new Dictionary<string, GameObject>();

	// Token: 0x040025B3 RID: 9651
	public int IsInitTeam;

	// Token: 0x040025B4 RID: 9652
	public bool LoadTeamOK;

	// Token: 0x040025B5 RID: 9653
	public int lookingTeamId;
}

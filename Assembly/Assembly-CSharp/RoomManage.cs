using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class RoomManage
{
	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0013A0F8 File Offset: 0x001382F8
	// (set) Token: 0x060021CA RID: 8650 RVA: 0x0013A110 File Offset: 0x00138310
	public RoomData PRoomData
	{
		get
		{
			return this.m_pRoomData;
		}
		set
		{
			this.m_pRoomData = value;
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x0013A11C File Offset: 0x0013831C
	public static RoomManage GetInstance()
	{
		bool flag = RoomManage.instance == null;
		if (flag)
		{
			RoomManage.instance = new RoomManage();
		}
		return RoomManage.instance;
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x0013A14C File Offset: 0x0013834C
	public void OnRoomMsg(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		int num2 = num;
		switch (num2)
		{
		case 3301:
			this.JoinRoom();
			break;
		case 3302:
			this.OnGameOver(szNetMsg);
			break;
		case 3303:
			this.OnJoinRoomBack((ArrayList)szNetMsg[1]);
			break;
		case 3304:
			this.OnLeaveRoomBack((ArrayList)szNetMsg[1]);
			break;
		case 3305:
			break;
		case 3306:
			this.OnGetRoomPageInfo((ArrayList)szNetMsg[1]);
			break;
		case 3307:
			this.OnRoomInfoBack((ArrayList)szNetMsg[1]);
			break;
		case 3308:
			this.OnPlayerChangeBack((ArrayList)szNetMsg[1]);
			break;
		default:
			switch (num2)
			{
			case 3318:
				this.OnChangeTeam((ArrayList)szNetMsg[1]);
				break;
			case 3320:
				roleuiManager.GetInstance().OpenSelectHeroUI(0);
				break;
			case 3324:
				this.OnModifyRoomInfo((ArrayList)szNetMsg[1]);
				break;
			case 3325:
				this.OnGetSomeRoomInfo((ArrayList)szNetMsg[1]);
				break;
			case 3326:
				this.OnQuickJoinBack((ArrayList)szNetMsg[1]);
				break;
			case 3329:
				this.OnChangeUserData((ArrayList)szNetMsg[1]);
				break;
			case 3331:
			{
				bool flag = this.m_pRoomData != null;
				if (flag)
				{
					this.m_pRoomData.OnUserLeave((string)szNetMsg[1]);
				}
				break;
			}
			case 3333:
				this.OnGameStartErr((ArrayList)szNetMsg[1]);
				break;
			case 3335:
				this.OnSetPlayerInfo((ArrayList)szNetMsg[1]);
				break;
			case 3336:
				this.OnJoinMatch((ArrayList)szNetMsg[1]);
				break;
			case 3337:
				this.OnGetGroupData((ArrayList)szNetMsg[1]);
				break;
			case 3338:
				this.OnTeamJoinRoomBack((ArrayList)szNetMsg[1]);
				break;
			}
			break;
		}
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x0013A3A8 File Offset: 0x001385A8
	private void OnGameStartErr(ArrayList arrayList)
	{
		int num = (int)arrayList[0];
		ArrayList arrayList2 = (ArrayList)arrayList[1];
		bool flag = this.m_pRoomData == null || fGameManager.Heroplayer != null;
		if (!flag)
		{
			bool flag2 = num == 1;
			if (flag2)
			{
				this.m_pRoomData.InitRoom(new ArrayList
				{
					(ArrayList)arrayList2[0],
					(ArrayList)arrayList2[1]
				});
				UserData userData = this.m_pRoomData.GetUserData(PlayerDataManager.getInstance().HUserData.m_sName);
				bool flag3 = userData != null && userData.m_nIsReady == 1;
				if (flag3)
				{
					fUISoundManager.instance.PlayUISound("ui_startgame", 0, null, 20);
				}
			}
			bool flag4 = num != -3 || this.m_pRoomData.m_nRoomUID != (int)arrayList2[1];
			if (!flag4)
			{
				string sName = PlayerDataManager.getInstance().HUserData.m_sName;
				bool flag5 = this.m_pRoomData.m_sCreator == sName;
				if (flag5)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8010, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					bool flag6 = FindRoomCon.inst != null && FindRoomCon.inst.visible;
					if (flag6)
					{
						FindRoomCon.inst.SetStartBtn();
					}
				}
				else
				{
					bool flag7 = this.m_pRoomData.GetUserData(sName).m_nIsReady == 0;
					if (flag7)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8011, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
				}
			}
		}
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x0013A5B4 File Offset: 0x001387B4
	private void OnGameOver(ArrayList szMsg)
	{
		bool flag = FindRoomCon.inst != null;
		if (flag)
		{
			this.m_pRoomData = null;
			PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
			FindRoomCon.inst.CloseCreateRoom();
		}
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x0013A5F8 File Offset: 0x001387F8
	public List<RoomData> GetRoomList(int nType, int nRule)
	{
		List<RoomData> list = new List<RoomData>(this.m_szAllRoom.Values);
		RoomDataSort roomDataSort = new RoomDataSort();
		roomDataSort.SetCompare(nType);
		list.Sort(roomDataSort);
		bool flag = nRule == 0;
		if (flag)
		{
			list.Reverse();
		}
		return list;
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x0013A644 File Offset: 0x00138844
	public RoomData GetOneRoomData(int nRoomUID)
	{
		RoomData result = null;
		bool flag = this.m_szAllRoom.ContainsKey(nRoomUID);
		if (flag)
		{
			result = this.m_szAllRoom[nRoomUID];
		}
		return result;
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x0013A678 File Offset: 0x00138878
	public UserData GetUserData(string sName)
	{
		bool flag = this.m_pRoomData != null;
		UserData result;
		if (flag)
		{
			result = this.m_pRoomData.GetUserData(sName);
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x0013A6A8 File Offset: 0x001388A8
	public void OnClickHallHero(int nHeroid, int nIsOk)
	{
		this.SendRoomMsgToServer(3321, new ArrayList
		{
			PlayerDataManager.getInstance().HUserData.m_nRoomUID,
			nHeroid,
			nIsOk
		});
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x0013A700 File Offset: 0x00138900
	public void OnGameChangeHero(int nHeroid)
	{
		fGameManager.SendMsgToServer(5010, new ArrayList
		{
			nHeroid,
			1
		});
		roleuiManager.GetInstance().OnCloseSelectHeroUI();
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x0013A748 File Offset: 0x00138948
	private void OnGetRoomPageInfo(ArrayList szMsg)
	{
		bool flag = FindRoomCon.inst != null && FindRoomCon.inst.joinRoomUI.activeSelf;
		if (flag)
		{
			FindRoomCon.inst.ResetRoomNum((int)szMsg[0], (int)szMsg[1]);
			FindRoomCon.inst.ShowRoomList((ArrayList)szMsg[2]);
		}
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x0013A7B8 File Offset: 0x001389B8
	private RoomData GetOneRoomByID(int roomid)
	{
		RoomData result = null;
		foreach (RoomData roomData in this.m_szAllRoom.Values)
		{
			bool flag = roomData.m_nRoomID == roomid;
			if (flag)
			{
				result = roomData;
				break;
			}
		}
		return result;
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x0013A82C File Offset: 0x00138A2C
	private void OnGetSomeRoomInfo(ArrayList szMsg)
	{
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x0013A830 File Offset: 0x00138A30
	private void JoinRoom()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag)
		{
			this.SendRoomMsgToServer(3307, new ArrayList
			{
				PlayerDataManager.getInstance().HUserData.m_nRoomUID,
				0
			});
		}
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x0013A890 File Offset: 0x00138A90
	private void OnRoomInfoBack(ArrayList arr)
	{
		bool flag = (int)arr[0] >= 0;
		if (flag)
		{
			bool flag2 = this.m_pRoomData == null;
			if (flag2)
			{
				this.m_pRoomData = new RoomData();
			}
			this.m_pRoomData.InitRoom((ArrayList)arr[1]);
			bool flag3 = MainUICon.inst != null;
			if (flag3)
			{
				MainUICon.inst.ChangeTitleToIdx(0, null);
			}
		}
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x0013A908 File Offset: 0x00138B08
	private void OnJoinRoomBack(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		bool flag = num < 0;
		if (flag)
		{
			int num2 = (int)szNetMsg[1];
			int num3 = num2;
			if (num3 == 0)
			{
				bool flag2 = num == -2;
				if (flag2)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag3 = num == -1;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8021, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				return;
			}
			if (num3 == 1)
			{
				bool flag4 = num == -1;
				if (flag4)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag5 = num == -2;
				if (flag5)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag6 = num == -3;
				if (flag6)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8016, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag7 = num == -4;
				if (flag7)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8017, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag8 = num == -5;
				if (flag8)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8022, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag9 = num == -6;
				if (flag9)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8025, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				return;
			}
		}
		bool flag10 = num != 1;
		if (!flag10)
		{
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			ArrayList szRoomData = new ArrayList();
			szRoomData = (ArrayList)arrayList[1];
			string a = Fun.DecodingUTF8(arrayList[2].ToString());
			bool flag11 = this.m_pRoomData == null;
			if (flag11)
			{
				this.m_pRoomData = new RoomData();
			}
			this.m_pRoomData.InitRoom(szRoomData);
			bool flag12 = a == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag12)
			{
				bool flag13 = MainUICon.inst != null;
				if (flag13)
				{
					MainUICon.inst.ChangeTitleToIdx(0, null);
				}
			}
			else
			{
				bool flag14 = FindRoomCon.inst != null;
				if (flag14)
				{
					FindRoomCon.inst.SetCreateRoom(1);
				}
			}
		}
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x0013ACDC File Offset: 0x00138EDC
	private void OnTeamJoinRoomBack(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		bool flag = num < 0;
		if (flag)
		{
			int num2 = (int)szNetMsg[1];
			int num3 = num2;
			if (num3 == 0)
			{
				bool flag2 = num == -2;
				if (flag2)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag3 = num == -1;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8021, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				return;
			}
			if (num3 == 1)
			{
				bool flag4 = num == -1;
				if (flag4)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag5 = num == -2;
				if (flag5)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag6 = num == -3;
				if (flag6)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8016, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag7 = num == -4;
				if (flag7)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8017, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag8 = num == -5;
				if (flag8)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8022, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				bool flag9 = num == -6;
				if (flag9)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8025, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				return;
			}
		}
		bool flag10 = num == 1;
		if (flag10)
		{
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			ArrayList szRoomData = new ArrayList();
			szRoomData = (ArrayList)arrayList[1];
			bool flag11 = this.m_pRoomData == null;
			if (flag11)
			{
				this.m_pRoomData = new RoomData();
			}
			this.m_pRoomData.InitRoom(szRoomData);
			bool flag12 = MainUICon.inst != null;
			if (flag12)
			{
				MainUICon.inst.ChangeTitleToIdx(0, null);
			}
		}
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x0013B054 File Offset: 0x00139254
	private void OnQuickJoinBack(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		bool flag = num == 1;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			ArrayList szRoomData = new ArrayList();
			szRoomData = (ArrayList)arrayList[1];
			bool flag2 = this.m_pRoomData == null;
			if (flag2)
			{
				this.m_pRoomData = new RoomData();
			}
			this.m_pRoomData.InitRoom(szRoomData);
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8018, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x0013B10F File Offset: 0x0013930F
	public void GetGroupData()
	{
		this.SendRoomMsgToServer(3337, new ArrayList());
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x0013B124 File Offset: 0x00139324
	private void OnGetGroupData(ArrayList szMsg)
	{
		this.nGroupLvl1 = (int)szMsg[0];
		this.nGroupLvl2 = (int)szMsg[1];
		bool flag = FindRoomCon.inst != null && FindRoomCon.inst.visible;
		if (flag)
		{
			FindRoomCon.inst.SetGroupData();
		}
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x0013B184 File Offset: 0x00139384
	public void OnCreateRoom(string sCode = "", int nGroup = 4)
	{
		ArrayList arrayList = new ArrayList
		{
			sCode,
			nGroup
		};
		bool flag = fGameManager.LobbyPlayer == null || !TeamManager.GetInstance().LoadTeamOK;
		if (!flag)
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().Dyaoqing.Count > 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40001, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmCreateRoom), arrayList, null, null);
					}
					else
					{
						bool flag5 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
						if (flag5)
						{
							TeamManager.GetInstance().OnLeaveTeam();
						}
						this.OnConfirmCreateRoom(arrayList);
					}
				}
			}
		}
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x0013B358 File Offset: 0x00139558
	private void OnConfirmCreateRoom(object szParam)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
		if (flag)
		{
			TeamManager.GetInstance().OnLeaveTeam();
		}
		bool flag2 = TeamManager.GetInstance().lookingTeamId != 0 && TeamManager.GetInstance().m_nCaptain == 1;
		if (flag2)
		{
			TeamManager.GetInstance().LeaveLookingTeam(7, 0, 0, 0);
		}
		List<int> allOpenMapType = this.GetAllOpenMapType();
		ArrayList arrayList = (ArrayList)szParam;
		int index = UnityEngine.Random.Range(0, allOpenMapType.Count);
		List<FbXmlData> roomMapData = this.GetRoomMapData(allOpenMapType[index]);
		index = ((this.nCreateFbId != 0) ? UnityEngine.Random.Range(0, roomMapData.Count) : UnityEngine.Random.Range(0, Mathf.Min(roomMapData.Count, 5)));
		FbXmlData fbXmlData = roomMapData[index];
		this.nCreateFbId = fbXmlData.nId;
		this.SendRoomMsgToServer(3301, new ArrayList
		{
			Fun.EncodingUTF8(Language.GetLang(6021, new object[]
			{
				PlayerDataManager.getInstance().HUserData.m_sName
			})),
			arrayList[0].ToString(),
			this.nCreateFbId,
			fbXmlData.nType,
			(int)arrayList[1],
			10,
			0,
			10,
			1
		});
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x0013B504 File Offset: 0x00139704
	public void OnLeaveRoom()
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8026, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickOkBtn), "OnLeaveRoom", null, null);
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8027, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickOkBtn), "OnLeaveRoom", null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8019, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickOkBtn), "OnLeaveRoom", null, null);
			}
		}
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x0013B688 File Offset: 0x00139888
	private void OnClickOkBtn(object szParam)
	{
		string a = szParam.ToString();
		bool flag = a == "OnLeaveRoom";
		if (flag)
		{
			this.OnLeave();
		}
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x0013B6B8 File Offset: 0x001398B8
	public void OnLeave()
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
			if (flag2)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			this.SendRoomMsgToServer(3304, new ArrayList());
			this.m_pRoomData = null;
			bool flag3 = FindRoomCon.inst != null;
			if (flag3)
			{
				FindRoomCon.inst.CloseCreateRoom();
			}
			PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		}
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x0013B748 File Offset: 0x00139948
	public void OnTeamLeave()
	{
		this.SendRoomMsgToServer(3304, new ArrayList());
		this.m_pRoomData = null;
		bool flag = FindRoomCon.inst != null;
		if (flag)
		{
			FindRoomCon.inst.CloseCreateRoom();
		}
		PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x0013B79C File Offset: 0x0013999C
	private void OnLeaveRoomBack(ArrayList szNetMsg)
	{
		int num = 0;
		int num2 = (int)szNetMsg[0];
		bool flag = num2 != 1;
		if (!flag)
		{
			int key = (int)szNetMsg[1];
			ArrayList arrayList = (ArrayList)(szNetMsg[2] as ArrayList).Clone();
			string a = Fun.DecodingUTF8(szNetMsg[3].ToString());
			int num3 = (int)szNetMsg[5];
			ArrayList arrayList2 = (ArrayList)(arrayList[1] as ArrayList).Clone();
			bool flag2 = num3 == 1 && a == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8013, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			bool flag3 = num3 == 3;
			if (flag3)
			{
				this.m_szAllRoom.Remove(key);
				this.m_pRoomData = null;
				PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
				bool flag4 = FindRoomCon.inst != null;
				if (flag4)
				{
					FindRoomCon.inst.CloseCreateRoom();
				}
			}
			else
			{
				for (int i = 0; i < arrayList2.Count; i += 2)
				{
					bool flag5 = (string)arrayList2[i] != string.Empty;
					if (flag5)
					{
						string b = Fun.DecodingUTF8(arrayList2[i].ToString());
						bool flag6 = PlayerDataManager.getInstance().HUserData.m_sName == b;
						if (flag6)
						{
							num = 1;
						}
					}
				}
				bool flag7 = num != 1;
				if (flag7)
				{
					PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
					this.m_pRoomData = null;
					bool flag8 = FindRoomCon.inst != null;
					if (flag8)
					{
						FindRoomCon.inst.CloseCreateRoom();
					}
				}
				else
				{
					bool flag9 = this.m_pRoomData != null;
					if (flag9)
					{
						this.m_pRoomData.InitRoom(arrayList);
					}
					bool flag10 = FindRoomCon.inst != null;
					if (flag10)
					{
						FindRoomCon.inst.SetCreateRoom(0);
					}
				}
				bool flag11 = this.m_pRoomData != null;
				if (flag11)
				{
					this.m_pRoomData.SetRoomUserList(arrayList2);
				}
			}
		}
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x0013BA04 File Offset: 0x00139C04
	private void OnChangeUserData(ArrayList szNetMsg)
	{
		bool flag = szNetMsg.Count >= 1;
		if (flag)
		{
			string text = Fun.DecodingUTF8(szNetMsg[0].ToString());
			ArrayList szUser = (ArrayList)szNetMsg[1];
			bool flag2 = this.m_pRoomData != null;
			if (flag2)
			{
				this.m_pRoomData.SetRoomUserList(text, szUser);
			}
			bool flag3 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
			if (flag3)
			{
				roleuiManager.GetInstance().OnOpenHeroUI(text);
			}
		}
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x0013BA90 File Offset: 0x00139C90
	public void OnChangeTeam(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		bool flag = num < 0;
		if (flag)
		{
			bool flag2 = num == -3 || num == -7;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6026, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			bool flag3 = num == -5;
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6025, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			bool flag4 = num == -6;
			if (flag4)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			bool flag5 = num == -8;
			if (flag5)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(6034, new object[0]), "ffdd00", "system");
			}
		}
		else
		{
			ArrayList roomUserList = (ArrayList)szNetMsg[1];
			bool flag6 = this.m_pRoomData != null;
			if (flag6)
			{
				this.m_pRoomData.SetRoomUserList(roomUserList);
			}
			bool flag7 = FindRoomCon.inst != null;
			if (flag7)
			{
				FindRoomCon.inst.RefreshPlayerList();
			}
			bool flag8 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
			if (flag8)
			{
				roleuiManager.GetInstance().OnOpenHeroUI(string.Empty);
			}
		}
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x0013BC74 File Offset: 0x00139E74
	private void OnPlayerChangeBack(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		bool flag = num == 1;
		if (flag)
		{
			bool flag2 = this.m_pRoomData == null;
			if (flag2)
			{
				this.m_pRoomData = new RoomData();
			}
			this.m_pRoomData.InitRoom((ArrayList)szNetMsg[1]);
			string sNick = Fun.DecodingUTF8(szNetMsg[2].ToString());
			bool flag3 = FindRoomCon.inst != null;
			if (flag3)
			{
				FindRoomCon.inst.UpdateOneUser(sNick);
			}
		}
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x0013BCFC File Offset: 0x00139EFC
	private void OnModifyRoomInfo(ArrayList szMsg)
	{
		bool flag = szMsg.Count == 0 || this.m_pRoomData == null;
		if (!flag)
		{
			int nRoomState = this.m_pRoomData.m_nRoomState;
			for (int i = 0; i < szMsg.Count; i += 2)
			{
				this.m_pRoomData.ChangeGameInfo((int)szMsg[i], szMsg[i + 1]);
			}
			bool flag2 = this.m_pRoomData.m_nRoomState == 4 && this.m_szAllRoom.ContainsKey(this.m_pRoomData.m_nRoomUID);
			if (flag2)
			{
				this.m_szAllRoom.Remove(this.m_pRoomData.m_nRoomUID);
			}
			bool flag3 = FindRoomCon.inst != null;
			if (flag3)
			{
				FindRoomCon.inst.SetRoomInfo(0);
				bool flag4 = nRoomState == 0 && this.m_pRoomData.m_nRoomState > 0;
				if (flag4)
				{
					FindRoomCon.inst.RefreshPlayerList();
				}
			}
		}
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x0013BDF8 File Offset: 0x00139FF8
	private void OnSetPlayerInfo(ArrayList szMsg)
	{
		string uname = Fun.DecodingUTF8(szMsg[0].ToString());
		ArrayList szInfo = (ArrayList)szMsg[1];
		bool flag = this.m_pRoomData != null;
		if (flag)
		{
			this.m_pRoomData.SetRoomUserListByIdx(uname, szInfo);
		}
		bool flag2 = FindRoomCon.inst != null;
		if (flag2)
		{
			FindRoomCon.inst.RefreshPlayerList();
		}
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x0013BE60 File Offset: 0x0013A060
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetCurOpenUI(sUI);
		}
		bool flag2 = FindRoomCon.inst != null;
		if (flag2)
		{
			FindRoomCon.inst.SetCreateRoom(0);
		}
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x0013BEA8 File Offset: 0x0013A0A8
	public void SendRoomMsgToServer(int nNetID, ArrayList arr)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(nNetID);
			arrayList.Add(arr);
			fGameManager.LobbyPlayer.SendMsgToHallServer(20013, arrayList);
		}
	}

	// Token: 0x060021EC RID: 8684 RVA: 0x0013BEF0 File Offset: 0x0013A0F0
	public void AddInvitedFriend(ArrayList szData)
	{
		DateTime serverDate = Fun.GetServerDate();
		for (int i = 0; i < szData.Count; i++)
		{
			int num = this.szInvited.IndexOf(szData[i]);
			bool flag = num == -1;
			if (flag)
			{
				this.szInvited.Add(szData[i].ToString());
				this.szInvited.Add(serverDate);
			}
			else
			{
				this.szInvited[num + 1] = serverDate;
			}
		}
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x0013BF80 File Offset: 0x0013A180
	private void RemoveInviteUser(string sName)
	{
		int num = this.szInvited.IndexOf(sName);
		bool flag = num != -1;
		if (flag)
		{
			this.szInvited.RemoveRange(num, 2);
			bool flag2 = InviteUICon.inst != null && InviteUICon.inst.visible;
			if (flag2)
			{
				InviteUICon.inst.ShowPageData();
			}
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x0013BFE4 File Offset: 0x0013A1E4
	public void CheckInvitedTime()
	{
		bool flag = this.szInvited.Count == 0;
		if (!flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			int i = 0;
			int num = 0;
			while (i < this.szInvited.Count)
			{
				DateTime d = (DateTime)this.szInvited[i + 1];
				bool flag2 = (serverDate - d).TotalSeconds >= 20.0;
				if (flag2)
				{
					this.szInvited.RemoveRange(i, 2);
					num = 1;
				}
				else
				{
					i += 2;
				}
			}
			bool flag3 = num == 1 && InviteUICon.inst != null && InviteUICon.inst.visible;
			if (flag3)
			{
				InviteUICon.inst.ShowPageData();
			}
		}
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x0013C0B4 File Offset: 0x0013A2B4
	public void OnInviteRoom(ArrayList szMsg)
	{
		szMsg[0] = Fun.DecodingUTF8(szMsg[0].ToString());
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			this.OnInviteBack(2, new ArrayList
			{
				szMsg[0].ToString(),
				0
			});
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag2)
			{
				this.OnInviteBack(3, new ArrayList
				{
					szMsg[0].ToString(),
					0
				});
			}
			else
			{
				fUIManager.GetInstance().OnPushMsg(new ArrayList
				{
					4,
					Language.GetLang(46074, new object[]
					{
						szMsg[0].ToString()
					}),
					szMsg
				}, 20);
			}
		}
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x0013C1B0 File Offset: 0x0013A3B0
	public void OnInviteRoomBack(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		string text = Fun.DecodingUTF8(szMsg[2].ToString());
		this.RemoveInviteUser(text);
		bool flag = num == 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(21012, new object[]
			{
				text
			}), "ffdd00", "system");
		}
		bool flag2 = num == 1;
		if (flag2)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(21014, new object[]
			{
				text
			}), "ffdd00", "system");
		}
		bool flag3 = num == 2 && text != PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag3)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(21013, new object[]
			{
				text
			}), "ffdd00", "system");
		}
		bool flag4 = num == 3 && text != PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag4)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(21018, new object[]
			{
				text
			}), "ffdd00", "system");
		}
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x0013C2E8 File Offset: 0x0013A4E8
	public void OnInviteBack(int nRet, ArrayList szData)
	{
		bool flag = nRet == 1;
		if (flag)
		{
			bool flag2 = fGameManager.Heroplayer != null;
			if (flag2)
			{
				return;
			}
			bool flag3 = MatchManager.GetInstance().m_nMatchType == 2;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40056, new object[0]), "ffdd00", "system");
				return;
			}
			bool flag4 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag4)
			{
				RoomManage.GetInstance().OnLeave();
			}
			bool flag5 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag5)
			{
				MatchManager.GetInstance().OnCancelMatch();
			}
			bool flag6 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag6)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			this.SendRoomMsgToServer(3303, new ArrayList
			{
				szData[3].ToString(),
				(int)szData[1],
				0,
				0,
				(int)szData[2]
			});
		}
		bool flag7 = fGameManager.Heroplayer != null;
		if (flag7)
		{
			fGameManager.Heroplayer.SendMessageToServer(1312, new ArrayList
			{
				nRet,
				Fun.EncodingUTF8(szData[0].ToString())
			});
		}
		else
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(1312, new ArrayList
			{
				nRet,
				Fun.EncodingUTF8(szData[0].ToString())
			});
		}
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x0013C4AC File Offset: 0x0013A6AC
	public List<int> GetAllRoomMapType()
	{
		List<int> result = new List<int>();
		bool flag = this.m_szAllMap.Count == 0;
		if (flag)
		{
			this.SortAllRoomMap();
		}
		bool flag2 = this.m_szAllMap.Count > 0;
		if (flag2)
		{
			result = new List<int>(this.m_szAllMap.Keys);
		}
		return result;
	}

	// Token: 0x060021F3 RID: 8691 RVA: 0x0013C508 File Offset: 0x0013A708
	public List<int> GetAllOpenMapType()
	{
		List<int> list = new List<int>();
		bool flag = this.m_szAllMap.Count == 0;
		if (flag)
		{
			this.SortAllRoomMap();
		}
		bool flag2 = this.m_szAllMap.Count > 0;
		if (flag2)
		{
			foreach (int num in this.m_szAllMap.Keys)
			{
				bool flag3 = ActivityManager.GetInstance().IsOpenLimit(num) == 1;
				if (flag3)
				{
					list.Add(num);
				}
			}
		}
		return list;
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x0013C5BC File Offset: 0x0013A7BC
	public List<FbXmlData> GetRoomMapData(int ntype)
	{
		List<FbXmlData> result = new List<FbXmlData>();
		bool flag = this.m_szAllMap.Count == 0;
		if (flag)
		{
			this.SortAllRoomMap();
		}
		bool flag2 = this.m_szAllMap.ContainsKey(ntype);
		if (flag2)
		{
			result = this.m_szAllMap[ntype];
		}
		return result;
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x0013C610 File Offset: 0x0013A810
	private void SortAllRoomMap()
	{
		int num = Fun.m_nIsTopUp();
		foreach (FbXmlData fbXmlData in fGameManager.mloadConfig.FbData.Values)
		{
			bool flag = fbXmlData != null && fbXmlData.nispublic >= num && fbXmlData.nType != 6 && fbXmlData.nType != 8 && fbXmlData.nType != 9 && fbXmlData.nType != 10 && fbXmlData.nType != 12;
			if (flag)
			{
				bool flag2 = this.m_szAllMap.ContainsKey(fbXmlData.nType);
				List<FbXmlData> list;
				if (flag2)
				{
					list = this.m_szAllMap[fbXmlData.nType];
				}
				else
				{
					list = new List<FbXmlData>();
					this.m_szAllMap.Add(fbXmlData.nType, list);
				}
				list.Add(fbXmlData);
			}
		}
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x0013C714 File Offset: 0x0013A914
	private void OnJoinMatch(ArrayList szMsg)
	{
		bool flag = FindRoomCon.inst == null;
		if (flag)
		{
			this.isShowRoomUI = false;
		}
		bool flag2 = FindRoomCon.inst != null && FindRoomCon.inst.visible;
		if (flag2)
		{
			FindRoomCon.inst.SetRoomBtn(false);
			FindRoomCon.inst.Close();
		}
	}

	// Token: 0x04001F5A RID: 8026
	private static RoomManage instance;

	// Token: 0x04001F5B RID: 8027
	public const int ROOM_PLAYERMAXNUM = 10;

	// Token: 0x04001F5C RID: 8028
	private Dictionary<int, RoomData> m_szAllRoom = new Dictionary<int, RoomData>();

	// Token: 0x04001F5D RID: 8029
	private RoomData m_pRoomData;

	// Token: 0x04001F5E RID: 8030
	private Dictionary<int, List<FbXmlData>> m_szAllMap = new Dictionary<int, List<FbXmlData>>();

	// Token: 0x04001F5F RID: 8031
	public int nGroupLvl1 = 8;

	// Token: 0x04001F60 RID: 8032
	public int nGroupLvl2 = 14;

	// Token: 0x04001F61 RID: 8033
	public bool isShowRoomUI = true;

	// Token: 0x04001F62 RID: 8034
	private int nCreateFbId;

	// Token: 0x04001F63 RID: 8035
	public ArrayList szInvited = new ArrayList();
}

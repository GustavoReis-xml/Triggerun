using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000129 RID: 297
public class fGameManager
{
	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000634C8 File Offset: 0x000616C8
	public static bool Npc_Action_State
	{
		get
		{
			return fUIManager.GetInstance().GetNowGameState() > 0;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000634F4 File Offset: 0x000616F4
	// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0006350B File Offset: 0x0006170B
	public static HeroPlayer Heroplayer
	{
		get
		{
			return fGameManager.m_Heroplayer;
		}
		set
		{
			fGameManager.m_Heroplayer = value;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00063514 File Offset: 0x00061714
	// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0006352B File Offset: 0x0006172B
	public static HallUser LobbyPlayer
	{
		get
		{
			return fGameManager.m_LobbyPlayer;
		}
		set
		{
			fGameManager.m_LobbyPlayer = value;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00063534 File Offset: 0x00061734
	// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0006354B File Offset: 0x0006174B
	public static ChatUser ChatPlayer
	{
		get
		{
			return fGameManager.m_ChatPlayer;
		}
		set
		{
			fGameManager.m_ChatPlayer = value;
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00063554 File Offset: 0x00061754
	public static void SetIsGongwang(ArrayList arr)
	{
		fGameManager.m_nIsTopUp = (int)arr[1];
		ArrayList arrayList = (ArrayList)arr[3];
		fGameManager.m_nIsCharge = (int)arrayList[0];
		HeroDataManager.GetInstance().SetIsGongwang();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnShowOrHideChargeBtn();
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000635B8 File Offset: 0x000617B8
	public static void SendMsgToServer(int nNetID, ArrayList szNetMsg)
	{
		bool flag = fGameManager.m_Heroplayer != null;
		if (flag)
		{
			fGameManager.m_Heroplayer.SendMessageToServer(nNetID, szNetMsg);
		}
		else
		{
			Debug.LogError(string.Format("玩家对象不存在！发送消息失败 netid:{0}", nNetID));
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x000635FC File Offset: 0x000617FC
	public static void SendMsgToHallServer(int nNetID, ArrayList szNetMsg)
	{
		bool flag = fGameManager.m_LobbyPlayer != null;
		if (flag)
		{
			fGameManager.m_LobbyPlayer.SendMsgToHallServer(nNetID, szNetMsg);
		}
		else
		{
			Debug.LogError(string.Format("玩家对象不存在！发送消息失败 netid:{0}", nNetID));
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00063640 File Offset: 0x00061840
	public static void SendMsgToChatServer(int nNetID, ArrayList szNetMsg)
	{
		bool flag = fGameManager.m_ChatPlayer != null;
		if (flag)
		{
			fGameManager.m_ChatPlayer.SendMsgToChatServer(nNetID, szNetMsg);
		}
		else
		{
			Debug.LogError(string.Format("玩家对象不存在！发送消息失败 netid:{0}", nNetID));
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00063684 File Offset: 0x00061884
	public static fGameManager GetInstance()
	{
		bool flag = fGameManager.mInst == null;
		if (flag)
		{
			fGameManager.mInst = new fGameManager();
		}
		return fGameManager.mInst;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000636B4 File Offset: 0x000618B4
	public fResourceManager GetResManager()
	{
		return fGameManager.mresManager;
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x000636CB File Offset: 0x000618CB
	public void SetGameRoot(GameObject root)
	{
		fGameManager.mGame = root;
		this.mGameRoot = root.GetComponent<GameRoot>();
		fGameManager.getWebParam();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x000636E8 File Offset: 0x000618E8
	public UserSocket GetSocket()
	{
		return GameRoot.mSocket;
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x000636FF File Offset: 0x000618FF
	public void SetLoginParam(int nCid, string sGroup, string sPlayerName, int nAutoID, string sIp, int nPort)
	{
		this.m_nCid = nCid;
		this.m_sGroup = sGroup;
		this.m_sPlayerName = Fun.DecodingUTF8(sPlayerName);
		this.m_nAutoID = nAutoID;
		this.m_sIp = sIp;
		this.m_nPort = nPort;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00063734 File Offset: 0x00061934
	public void SetHallParam(int nCid, string sGroup, string sPlayerName, int nAutoID, string sIp, int nPort)
	{
		this.m_nHall_Cid = nCid;
		this.m_sHall_Group = sGroup;
		this.m_sHall_PlayerName = sPlayerName;
		this.m_nHall_AutoID = nAutoID;
		this.m_sHall_Ip = sIp;
		this.m_nHall_Port = nPort;
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00063764 File Offset: 0x00061964
	public void ConnectToHall()
	{
		GameRoot.HallSocket.ConnectServer(this.m_sHall_Ip, this.m_nHall_Port, this.m_sHall_Group, "HallUser", this.m_nHall_Cid.ToString(), this.m_nHall_AutoID.ToString(), this.m_sHall_PlayerName);
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x000637B0 File Offset: 0x000619B0
	public void OnReturnHallOK(int nCid, string sNick, int nAutoID)
	{
		fGameManager.bGoToHall = false;
		GameRoot.inst.audioEar.transform.SetParent(GameRoot.inst.transform, false);
		fGameManager.GetInstance().CloseLink(2);
		SceneManager.LoadScene(3);
		HeroPlayer.isMouseShow = true;
		User_Data_Handle.Watch_Fight_State = false;
		PlayerDataManager.getInstance().playercom.m_nIsFristFight = 0;
		bool flag = Setting_TipsCon.Instance != null && Setting_TipsCon.Instance.visible;
		if (flag)
		{
			Setting_TipsCon.Instance.Close();
		}
		MatchManager.GetInstance().ClearMatchUserData();
		PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		fGameManager.GetInstance().InitReturnHall();
		bool flag2 = nCid != 0 && !string.IsNullOrEmpty(sNick);
		if (flag2)
		{
			this.mGameRoot.OnDelayCheckTip();
		}
		fUIManager.GetInstance().CloseGameUI();
		fUIManager.GetInstance().InitRoomData();
		fGameManager.GetInstance().nNowScnID = 0;
		CReadModel.Instance.isLoaded = false;
		fUIManager.SetProgreessShow(false);
		SkyLadderManager.GetInstance().ClearSkyMatchData();
		DtsBatteManager.GetInstance().ClearDtsMatchData();
		WarPersonManager.Instance.Closed();
		QuanManager.GetInstance().ClearDtsQuanData();
		bool flag3 = fGameManager.LobbyPlayer != null;
		if (flag3)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(1310, new ArrayList
			{
				0,
				new ArrayList()
			});
		}
		PveManager.GetInstance().OnHallOk();
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0006392C File Offset: 0x00061B2C
	public void InitReturnHall()
	{
		bool flag = FindRoomCon.inst != null;
		if (flag)
		{
			FindRoomCon.inst.CloseCreateRoom();
		}
		ScnMsgManager.GetInstance().OnClearPushMsg();
		fUIManager.GetInstance().ChangeHallResetUI();
		FBManager.GetInstance().m_szFbResultData.Clear();
		fGameManager.m_nPreLoadOff = false;
		MatchManager.GetInstance().m_nIsMatch = 0;
		MatchManager.GetInstance().m_nMatchFbType = 0;
		MatchManager.GetInstance().IsMatchState = 100;
		TeamManager.GetInstance().InitTeam();
		bool flag2 = fGameManager.bFirstLogin;
		if (flag2)
		{
			this.SoundStop();
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x000639C4 File Offset: 0x00061BC4
	public void SoundStop()
	{
		bool flag = (!(SelectHeroUICon.inst != null) || !SelectHeroUICon.inst.visible) && !fGameManager.m_nPreLoadOff;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound("music_commen", 1, new fUISoundManager.UISoundStopFun(this.DaTingSound), 10);
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00063A1C File Offset: 0x00061C1C
	public void DaTingSound()
	{
		bool nPreLoadOff = fGameManager.m_nPreLoadOff;
		if (nPreLoadOff)
		{
			fUISoundManager.instance.StopSound("music_commen", 0);
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00063A48 File Offset: 0x00061C48
	public void ConnectToScn()
	{
		GameRoot.SceneSocket.ConnectServer(this.m_sIp, this.m_nPort, this.m_sGroup, "PlayerUser", this.m_nCid.ToString(), this.m_nAutoID.ToString(), Fun.EncodingUTF8(this.m_sPlayerName));
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00063A99 File Offset: 0x00061C99
	public void SetChatInfo(string sIp, int nPort)
	{
		this.m_sChatIp = sIp;
		this.m_sChatPort = nPort;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00063AAC File Offset: 0x00061CAC
	public void ConnectToChat()
	{
		bool flag = fGameManager.ChatPlayer == null;
		if (flag)
		{
			GameRoot.ChatSocket.ConnectServer(this.m_sChatIp, this.m_sChatPort, "ChatServer", "ChatUser", this.m_nHall_Cid.ToString(), string.Empty, string.Empty);
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00063B00 File Offset: 0x00061D00
	public void CloseLink(int nType)
	{
		bool flag = nType != 1 && GameRoot.LoginSocket != null;
		if (flag)
		{
			GameRoot.LoginSocket.breakIt();
		}
		bool flag2 = nType == 2 || GameRoot.HallSocket != null;
		if (flag2)
		{
		}
		bool flag3 = nType != 3 && GameRoot.SceneSocket != null;
		if (flag3)
		{
			GameRoot.SceneSocket.breakIt();
		}
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00063B6C File Offset: 0x00061D6C
	public void LoginUser()
	{
		UserSocket loginSocket = GameRoot.LoginSocket;
		loginSocket.m_sIP = WebParam.client_ips;
		loginSocket.m_nPort = WebParam.client_port;
		fGameManager.m_nLostReason = 20001;
		loginSocket.SetIpAndPort();
		GameRoot.LoginSocket.ConnectServer(loginSocket.m_sIP, loginSocket.m_nPort, "LoginServer", "LoginUser", WebParam.client_account, WebParam.client_password, string.Concat(new string[]
		{
			WebParam.client_serverid,
			"#HS#",
			SysConfig.g_client,
			"#",
			Fun.GetUdId(),
			"#",
			Fun.GetMAC0()
		}));
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00063C18 File Offset: 0x00061E18
	public HeroPlayer getPlayer()
	{
		return this.mPlayerObj;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00063C30 File Offset: 0x00061E30
	public void setPlayer(HeroPlayer obj)
	{
		this.mPlayerObj = obj;
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00063C3A File Offset: 0x00061E3A
	public void OnLoginOK()
	{
		fGameManager.mloadConfig.LoadConfigXml();
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00063C48 File Offset: 0x00061E48
	public static void ReLoadCfg()
	{
		bool flag = fGameManager.mloadConfig == null;
		if (flag)
		{
			UnityEngine.Object.Destroy(fGameManager.mloadConfig);
			fGameManager.mloadConfig = null;
		}
		GameObject gameObject = fGameManager.mGame.transform.FindChild("_fConfigManager").gameObject;
		bool flag2 = gameObject != null;
		if (flag2)
		{
			fGameManager.mloadConfig = gameObject.AddComponent<loadConfig>();
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00063CAA File Offset: 0x00061EAA
	public void XMLLoadOK()
	{
		this.ConnectToHall();
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00063CB4 File Offset: 0x00061EB4
	public void loadScene(string loadname, int ver, int nscnid)
	{
		Debug.Log(string.Format("MATCH_DEBUG: loadScene called: {0}, ver: {1}, id: {2}", loadname, ver, nscnid));
		this.mloadName = loadname;
		this.nNowScnID = nscnid;
		this.mver = ver;
		bool flag = this.mloadName == string.Empty;
		if (flag)
		{
		}
		SceneManager.LoadScene(1);
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x00063D14 File Offset: 0x00061F14
	public void OnUserLost(string type)
	{
		string lang = Language.GetLang(2027, new object[0]);
		bool flag = fGameManager.isFtUser;
		if (flag)
		{
			lang = fGameManager.sFtUserTip;
		}
		this.OnLostTip(true, lang);
		fGameManager.isClose = true;
		FunManager.Instance.reSet();
		GameRoot.inst.SendHttpRequest(1600, new ArrayList());
		Debug.Log("zlg  用户断线 ~~~~~~~~~~~~~~~~~~~~~~~~~" + type + "~~~~~~~~~~~~~~~~" + DateTime.Now.ToString());
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00063D96 File Offset: 0x00061F96
	public void OnTimerOver(string sIp, int nPort)
	{
		this.OnLostTip(Language.GetLang(1509, new object[0]));
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x00063DB0 File Offset: 0x00061FB0
	public void OnLostTip(string str)
	{
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), str, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00063E20 File Offset: 0x00062020
	public void OnLostTip(bool act, string sStr)
	{
		bool flag = fGameManager.isGameClose;
		if (!flag)
		{
			if (act)
			{
				bool flag2 = easy_tipCon.inst != null;
				if (flag2)
				{
					easy_tipCon.inst.ClearPopTip(true);
				}
				GameRoot.LoginSocket.cancelRelink();
				bool flag3 = fGameManager.isFtUser;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), fGameManager.sFtUserTip, Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
				}
				else
				{
					TipManager.GetInstance().ShowRelinkTip(Language.GetLang(8009, new object[0]), sStr, 19012, 30, Language.GetLang(19013, new object[0]), new TipManager.TipOKDelegate(this.OnClickYesBtn), string.Empty, new TipManager.TipCancelDelegate(this.OnCLickNoBtn), string.Empty);
				}
			}
			else
			{
				TipManager.GetInstance().HideTextTip();
			}
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00063F36 File Offset: 0x00062136
	private void OnClickYesBtn(object szParam)
	{
		GameRoot.LoginSocket.relink();
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00063F44 File Offset: 0x00062144
	private void OnCLickNoBtn(object szParam)
	{
		Application.Quit();
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00063F50 File Offset: 0x00062150
	public static void getWebParam()
	{
		bool flag = fGameManager.mWebLogin == null;
		if (flag)
		{
			GameObject gameObject = new GameObject("_fWebLogin");
			gameObject.transform.parent = fGameManager.mGame.transform;
			gameObject.AddComponent<HackLogMemo>();
			fGameManager.mWebLogin = gameObject.AddComponent<WebParam>();
			fGameManager.mWebLogin.getWebParam();
		}
		bool flag2 = fGameManager.mloadConfig == null;
		if (flag2)
		{
			fGameManager.mloadConfig = new GameObject("_fConfigManager")
			{
				transform = 
				{
					parent = fGameManager.mGame.transform
				}
			}.AddComponent<loadConfig>();
		}
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00063FEA File Offset: 0x000621EA
	private static void DontDestroyOnLoad(GameObject so)
	{
		UnityEngine.Object.DontDestroyOnLoad(so);
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00063FF4 File Offset: 0x000621F4
	public void SetUserLoadStep_log(string str)
	{
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00063FF8 File Offset: 0x000621F8
	public void StarMatch()
	{
		fGameManager.SendMsgToHallServer(80001, new ArrayList
		{
			80002,
			1,
			Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName)
		});
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00064054 File Offset: 0x00062254
	public void EscMatch()
	{
		fGameManager.SendMsgToHallServer(80001, new ArrayList
		{
			80003,
			MatchManager.GetInstance().m_nIsMatch,
			Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName),
			1
		});
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x000640C4 File Offset: 0x000622C4
	public void Start_Npc_All_Action(int _Net_ID)
	{
		NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(_Net_ID);
		bool flag = netObj.netobject_type == 2;
		if (flag)
		{
			Npc_Entity entity = (Npc_Entity)netObj;
			this.Start_Npc_All_Action(entity);
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00064108 File Offset: 0x00062308
	public void Start_Npc_All_Action(Npc_Entity _Entity)
	{
		Npc_Controller npc_Controller = (Npc_Controller)_Entity.Controller_Component;
		npc_Controller.Start_All_Action();
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0006412C File Offset: 0x0006232C
	public void Stop_Npc_All_Action(int _Net_ID)
	{
		NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(_Net_ID);
		bool flag = netObj.netobject_type == 2;
		if (flag)
		{
			Npc_Entity entity = (Npc_Entity)netObj;
			this.Stop_Npc_All_Action(entity);
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00064170 File Offset: 0x00062370
	public void Stop_Npc_All_Action(Npc_Entity _Entity)
	{
		Npc_Controller npc_Controller = (Npc_Controller)_Entity.Controller_Component;
		npc_Controller.Stop_All_Action();
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00064194 File Offset: 0x00062394
	public void Message_Handle(int _Net_ID, ArrayList szMsg)
	{
		bool flag = _Net_ID != 1;
		if (flag)
		{
			fGameManager.isClose = true;
		}
		switch (_Net_ID)
		{
		case 1000:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1510, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			break;
		case 1001:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1511, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			break;
		case 1002:
		{
			ArrayList arrayList = (ArrayList)szMsg[7];
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22000, new object[]
			{
				arrayList[1].ToString(),
				Fun.DecodingUTF8(arrayList[2].ToString())
			}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
			break;
		}
		case 1003:
		{
			fGameManager.isFtUser = true;
			ArrayList arrayList2 = (ArrayList)szMsg[7];
			string text = Language.GetLang(46075, new object[0]);
			bool flag2 = (int)arrayList2[3] < 59999940;
			if (flag2)
			{
				text = DateTime.Parse(arrayList2[2].ToString()).AddSeconds((double)((int)arrayList2[3])).ToString("yyyy-MM-dd hh:mm:ss");
			}
			fGameManager.sFtUserTip = Language.GetLang(22001, new object[]
			{
				text,
				Fun.DecodingUTF8(arrayList2[1].ToString())
			});
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22001, new object[]
			{
				text,
				Fun.DecodingUTF8(arrayList2[1].ToString())
			}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
			break;
		}
		case 1004:
		case 1005:
		{
			ArrayList arrayList3 = (ArrayList)szMsg[7];
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22003, new object[]
			{
				Fun.DecodingUTF8(arrayList3[2].ToString())
			}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
			break;
		}
		default:
			if (_Net_ID != 1014)
			{
				if (_Net_ID == 1015)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1513, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
				}
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1512, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
			}
			break;
		}
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0006458C File Offset: 0x0006278C
	public static void OpenQQUrl(string operate, string param, string sysname)
	{
		bool flag = Application.platform == RuntimePlatform.WindowsEditor || WebParam.client_company == "qq";
		if (flag)
		{
			bool flag2 = fGameManager.mTencentPlatform == null;
			if (flag2)
			{
				GameObject gameObject = new GameObject("_TencentPlatform");
				gameObject.transform.parent = fGameManager.mGame.transform;
				fGameManager.DontDestroyOnLoad(gameObject);
				fGameManager.mTencentPlatform = gameObject.AddComponent<TencentPlatform>();
			}
			fGameManager.mTencentPlatform.OpenQQUrl(operate, param, sysname);
		}
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0006460C File Offset: 0x0006280C
	public void SendPlayerReportMsg(ArrayList arr)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.Heroplayer.SendMessageToServer(6001, arr);
		}
		else
		{
			fGameManager.SendMsgToHallServer(6001, arr);
		}
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00064649 File Offset: 0x00062849
	public void OnFengHaoToServer()
	{
	}

	// Token: 0x04000908 RID: 2312
	public static GameObject allBaozhaFather;

	// Token: 0x04000909 RID: 2313
	public static GameObject allBulletFather;

	// Token: 0x0400090A RID: 2314
	public static GameObject allSoundFather;

	// Token: 0x0400090B RID: 2315
	public static GameObject allPaintFather;

	// Token: 0x0400090C RID: 2316
	public static fResourceManager mresManager;

	// Token: 0x0400090D RID: 2317
	public static fGameManager mInst;

	// Token: 0x0400090E RID: 2318
	public GameRoot mGameRoot;

	// Token: 0x0400090F RID: 2319
	public static GameObject mGame;

	// Token: 0x04000910 RID: 2320
	public HeroPlayer mPlayerObj;

	// Token: 0x04000911 RID: 2321
	public static int m_nIsLoad;

	// Token: 0x04000912 RID: 2322
	public static string sAssName = string.Empty;

	// Token: 0x04000913 RID: 2323
	public static string m_nJoinGame = "未进入游戏";

	// Token: 0x04000914 RID: 2324
	public static bool m_nPreLoadOff = true;

	// Token: 0x04000915 RID: 2325
	public static int m_nIsTopUp = 1;

	// Token: 0x04000916 RID: 2326
	public static int m_nIsCharge;

	// Token: 0x04000917 RID: 2327
	public static bool bFirstLogin = false;

	// Token: 0x04000918 RID: 2328
	public static bool IsJoinGame = false;

	// Token: 0x04000919 RID: 2329
	public static int m_nUserType = 0;

	// Token: 0x0400091A RID: 2330
	public static int m_nLostReason = 0;

	// Token: 0x0400091B RID: 2331
	public static bool IsSingle = false;

	// Token: 0x0400091C RID: 2332
	public static bool IsHadLearn = false;

	// Token: 0x0400091D RID: 2333
	public static bool IsNeedShowPiPeiYinDao = false;

	// Token: 0x0400091E RID: 2334
	public static bool IsPreDownOK = false;

	// Token: 0x0400091F RID: 2335
	public static bool IsModelDownOK = false;

	// Token: 0x04000920 RID: 2336
	public static bool bGoToHall = false;

	// Token: 0x04000921 RID: 2337
	public static bool isGameClose = false;

	// Token: 0x04000922 RID: 2338
	public static int m_nJoinState = 1;

	// Token: 0x04000923 RID: 2339
	public static bool isClose = false;

	// Token: 0x04000924 RID: 2340
	public static int m_nCloseState = 0;

	// Token: 0x04000925 RID: 2341
	public static float nJoinFbTime = Time.realtimeSinceStartup;

	// Token: 0x04000926 RID: 2342
	public static float m_nLoadScnTime = 0f;

	// Token: 0x04000927 RID: 2343
	public static float m_nLoadCarTime = 0f;

	// Token: 0x04000928 RID: 2344
	public static float m_nCreateModelTime = 0f;

	// Token: 0x04000929 RID: 2345
	public static bool isFtUser = false;

	// Token: 0x0400092A RID: 2346
	public static string sFtUserTip = string.Empty;

	// Token: 0x0400092B RID: 2347
	private static TencentPlatform mTencentPlatform;

	// Token: 0x0400092C RID: 2348
	public static GameObject GLostTip;

	// Token: 0x0400092D RID: 2349
	public string mloadName;

	// Token: 0x0400092E RID: 2350
	public int nNowScnID;

	// Token: 0x0400092F RID: 2351
	public int mver;

	// Token: 0x04000930 RID: 2352
	public string mPreLoadMap;

	// Token: 0x04000931 RID: 2353
	private static HeroPlayer m_Heroplayer;

	// Token: 0x04000932 RID: 2354
	private static HallUser m_LobbyPlayer;

	// Token: 0x04000933 RID: 2355
	private static ChatUser m_ChatPlayer;

	// Token: 0x04000934 RID: 2356
	public static loadConfig mloadConfig;

	// Token: 0x04000935 RID: 2357
	public static WebParam mWebLogin;

	// Token: 0x04000936 RID: 2358
	private string m_sHall_Group;

	// Token: 0x04000937 RID: 2359
	private string m_sHall_PlayerName;

	// Token: 0x04000938 RID: 2360
	private int m_nHall_AutoID;

	// Token: 0x04000939 RID: 2361
	private string m_sHall_Ip;

	// Token: 0x0400093A RID: 2362
	private int m_nHall_Port;

	// Token: 0x0400093B RID: 2363
	private int m_nHall_Cid;

	// Token: 0x0400093C RID: 2364
	private string m_sGroup;

	// Token: 0x0400093D RID: 2365
	private string m_sPlayerName;

	// Token: 0x0400093E RID: 2366
	private int m_nAutoID;

	// Token: 0x0400093F RID: 2367
	private string m_sIp;

	// Token: 0x04000940 RID: 2368
	private int m_nPort;

	// Token: 0x04000941 RID: 2369
	private int m_nCid;

	// Token: 0x04000942 RID: 2370
	private string m_sChatIp;

	// Token: 0x04000943 RID: 2371
	private int m_sChatPort;
}

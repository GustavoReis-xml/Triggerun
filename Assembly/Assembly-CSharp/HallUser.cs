using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class HallUser : NetObj
{
	// Token: 0x0600103A RID: 4154 RVA: 0x00096B40 File Offset: 0x00094D40
	public HallUser()
	{
		fGameManager.GetInstance().CloseLink(2);
		PlayerDataManager.getInstance();
		fGameManager.LobbyPlayer = this;
		fGameManager.m_nUserType = 0;
		fGameManager.IsSingle = false;
		fUIManager.IsMapOK = false;
		fUIManager.GetInstance().ShowMainUIBackGround();
		this.ShowMainUI();
		bool flag = ChatUICon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("ChatUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loadchatuiok), null, true);
		}
		this.openBroadCastUI();
		GameRoot.inst.SendHttpRequest(1604, new ArrayList());
		bool flag2 = fGameManager.Heroplayer != null;
		if (flag2)
		{
			fGameManager.Heroplayer.OnNetDestroy();
		}
		fGameManager.m_nJoinState = 17;
		GameRoot.inst.SendHttpRequest(1607, new ArrayList());
		VoiceGameCon.Inst.UnInit();
		VoiceGameCon.Inst.InitVoice();
		GameRoot.inst.OnDelayCheckTip();
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x00096C34 File Offset: 0x00094E34
	public override void OnNetDestroy()
	{
		fGameManager.LobbyPlayer = null;
		base.OnNetDestroy();
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00096C45 File Offset: 0x00094E45
	private void loadchatuiok(string sUI, GameObject ob, object szParam)
	{
		ChatManager.GetInstance().OnInitUserChat();
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x00096C54 File Offset: 0x00094E54
	private void openBroadCastUI()
	{
		bool flag = BroadCastMSGCon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("BroadCastMSG", NetMsg.UITRANSFROM.PublicUI, null, null, true);
		}
		else
		{
			BroadCastMSGCon.inst.Open();
		}
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00096C98 File Offset: 0x00094E98
	public void ShowMainUI()
	{
		bool flag = MainUICon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("MainUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), null, true);
		}
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x00096CD8 File Offset: 0x00094ED8
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
		fUIManager.SetProgreessShow(false);
		fGameManager.m_nPreLoadOff = false;
		fGameManager.IsJoinGame = false;
		GameRoot.mPreLoadManager.InitPreLoad();
		fUIManager.GetInstance().ProgressChange(false, "LoadingGamePlayer", 1f, 1f);
		bool flag = TeamManager.GetInstance().szAddTeamInfo.Count > 0;
		if (flag)
		{
			fUIManager.GetInstance().CallJoinTeam();
		}
		bool flag2 = GameRoot.inst.Setting.Video_Class.Graphics_Equipment_Describe == string.Empty;
		if (flag2)
		{
			GameRoot.inst.Setting.Video_Class.Graphics_Equipment_Describe = SystemInfo.graphicsDeviceName;
			GameRoot.inst.Setting.Video_Class.Use_Auto_Data();
		}
		else
		{
			GameRoot.inst.Setting.Video_Class.Use_Data();
		}
		GameRoot.inst.Setting.Use_Video_Setting();
		GameRoot.inst.Setting_FPS.Current_Display_Efficiency_FPS = GameRoot.inst.Setting.Video_Class.Display_Efficiency_FPS;
		GameRoot.inst.Setting_FPS.Current_Display_Efficiency_Ping = GameRoot.inst.Setting.Video_Class.Display_Efficiency_Ping;
		GameRoot.inst.Setting_FPS.Load_State = true;
		Application.targetFrameRate = GameRoot.inst.Setting.Video_Class.Frame_Limit;
		bool flag3 = PlayerDataManager.getInstance().HUserData.m_nPunNum != -1;
		if (flag3)
		{
			MainUICon.inst.SetPunUI();
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x00096E58 File Offset: 0x00095058
	public void SendMsgToHallServer(int nNetID, ArrayList szMsg)
	{
		bool load_Data_State = this.Load_Data_State;
		if (load_Data_State)
		{
			this.TosFun("SendMsgToHallServer", new object[]
			{
				nNetID,
				szMsg
			});
		}
		else
		{
			Debug.LogError(string.Format("玩家数据未加载完，发送消息失败 netid:{0}", nNetID));
		}
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x00096EAC File Offset: 0x000950AC
	public void SendMsgToHallClient(int nNetID, ArrayList szMsg)
	{
		int num = nNetID;
		int num2 = num;
		if (num2 <= 16200)
		{
			if (num2 <= 3450)
			{
				if (num2 <= 1601)
				{
					if (num2 <= 1205)
					{
						if (num2 == 1006)
						{
							bool flag = (int)szMsg[0] != 7 || fGameManager.m_nUserType != 1;
							if (flag)
							{
								ChatManager.GetInstance().Chat_Server_To_ClientNetMsg((int)szMsg[0], szMsg);
							}
							return;
						}
						if (num2 == 1205)
						{
							bool flag2 = szMsg.Count == 7;
							if (flag2)
							{
								ArrayList arrayList = (ArrayList)szMsg[3];
								int nIsFinder = (int)szMsg[6];
								int nLevel = (int)szMsg[4];
								fUIManager.GetInstance().OnFindOtherUserNo((int)arrayList[0], (ArrayList)arrayList[1], nIsFinder, Fun.DecodingUTF8(szMsg[2].ToString()), nLevel);
							}
							else
							{
								ArrayList arrayList2 = (ArrayList)szMsg[3];
								fUIManager.GetInstance().OnFindOtherUserOk((int)arrayList2[0], (ArrayList)arrayList2[1]);
							}
							return;
						}
					}
					else
					{
						switch (num2)
						{
						case 1301:
						{
							fGameManager.SetIsGongwang((ArrayList)szMsg[7]);
							PlayerDataManager.getInstance().HUserData.InitHUserBase((ArrayList)szMsg[0]);
							PlayerDataManager.getInstance().HUserData.InitCommonData((ArrayList)szMsg[15]);
							PlayerDataManager.getInstance().playerdet.SetMoneyData((ArrayList)szMsg[1]);
							PlayerDataManager.getInstance().HUserData.InitShopData((ArrayList)szMsg[6]);
							PlayerDataManager.getInstance().HUserData.InitTaskData((ArrayList)szMsg[8]);
							Prerogative_Data.Instance = PlayerDataManager.getInstance().playerCommon.Prerogative_Class;
							Prerogative_Data.Instance.Initialize_Data((ArrayList)szMsg[9]);
							SkyLadderManager.GetInstance().InitSkyLadderMatch((ArrayList)szMsg[16]);
							bool flag3 = MainUICon.inst != null && MainUICon.inst.visible;
							if (flag3)
							{
								MainUICon.inst.SetPlayerInfo();
							}
							TeamManager.GetInstance().InitTeam();
							bool flag4 = PlayerDataManager.getInstance().playerCommon.Gem_Data_Class != null;
							if (flag4)
							{
								Gem_Data.Instance.Initialize_Data((ArrayList)szMsg[13]);
								bool flag5 = MainUICon.inst != null;
								if (flag5)
								{
									MainUICon.inst.SetPunUI();
								}
								Fun.Log("读取到了宝石数据", null);
								this.Load_Data_State = true;
								HeroDataManager.GetInstance().GetPlayerOldReward();
							}
							return;
						}
						case 1302:
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1302, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							return;
						case 1303:
						case 1305:
						case 1311:
						case 1312:
						case 1316:
						case 1317:
							break;
						case 1304:
							fGameManager.GetInstance().SetLoginParam((int)szMsg[0], szMsg[4].ToString(), szMsg[1].ToString(), (int)szMsg[5], szMsg[2].ToString(), (int)szMsg[3]);
							fGameManager.GetInstance().ConnectToScn();
							MatchManager.GetInstance().entertrainorlearntime = Time.time;
							return;
						case 1306:
							HeroDataManager.GetInstance().OnHeroDataMsg(szMsg);
							return;
						case 1307:
							HeroDataManager.GetInstance().OnFcmDataMsg(szMsg);
							return;
						case 1308:
							PlayerDataManager.getInstance().HUserData.HallToWebMsg((int)szMsg[0], (ArrayList)szMsg[1]);
							return;
						case 1309:
							PlayerDataManager.getInstance().HUserData.m_sOnLoginDate = (string)szMsg[1];
							return;
						case 1310:
							PlayerDataManager.getInstance().HUserData.OnGmActionMsg((int)szMsg[0], (ArrayList)szMsg[1]);
							return;
						case 1313:
							this.OnShowLearnMsg((int)szMsg[0]);
							return;
						case 1314:
							WenJuanManager.GetInstance().WenJuanServerMsgToClient((int)szMsg[0], (ArrayList)szMsg[1]);
							return;
						case 1315:
							LiBaoManager.GetInstance().LiBaoServerMsgToClient((int)szMsg[0], (ArrayList)szMsg[1]);
							return;
						case 1318:
							fGameManager.isGameClose = true;
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(2030, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickQuit), null, null, null);
							return;
						case 1319:
							PlayerDataManager.getInstance().HUserData.InitDtsData(szMsg);
							return;
						default:
							if (num2 == 1601)
							{
								GameRoot.inst.SendHttpRequest(1601, szMsg);
								return;
							}
							break;
						}
					}
				}
				else if (num2 <= 3310)
				{
					switch (num2)
					{
					case 3001:
						ChatManager.GetInstance().OnRadio(szMsg);
						return;
					case 3002:
						PlayerDataManager.getInstance().HUserData.InitUpMsg(szMsg);
						return;
					case 3003:
						break;
					case 3004:
						ChatManager.GetInstance().OnCloseServerRadio(szMsg);
						return;
					default:
						if (num2 == 3310)
						{
							PlayerDataManager.getInstance().HUserData.SetRoomID((int)szMsg[0]);
							ChatManager.GetInstance().OnInitUserChat();
							return;
						}
						break;
					}
				}
				else
				{
					if (num2 == 3400)
					{
						MailManager.GetInstance().MailServerMsgToClient((int)szMsg[0], (ArrayList)szMsg[1]);
						return;
					}
					if (num2 == 3450)
					{
						PlayerDataManager.getInstance().playerCommon.InItQQGiftData(szMsg);
						this.Initialize_Reward_Data(szMsg);
						bool flag6 = MainUICon.inst != null && MainUICon.inst.visible;
						if (flag6)
						{
							MainUICon.inst.SetLzAwardState();
							MainUICon.inst.SetQQAwardState();
						}
						return;
					}
				}
			}
			else if (num2 <= 10025)
			{
				if (num2 <= 4002)
				{
					if (num2 == 3451)
					{
						PlayerDataManager.getInstance().playerCommon.InItQQGiftData(szMsg);
						this.Initialize_Reward_Data(szMsg);
						bool flag7 = MainUICon.inst != null && MainUICon.inst.visible;
						if (flag7)
						{
							MainUICon.inst.SetLzAwardState();
							MainUICon.inst.SetQQAwardState();
						}
						return;
					}
					if (num2 == 4002)
					{
						fGameManager.isFtUser = true;
						string text = Language.GetLang(46075, new object[0]);
						bool flag8 = (int)szMsg[3] < 59999940;
						if (flag8)
						{
							text = DateTime.Parse(szMsg[2].ToString()).AddSeconds((double)((int)szMsg[3])).ToString("yyyy-MM-dd hh:mm:ss");
						}
						fGameManager.sFtUserTip = Language.GetLang(22001, new object[]
						{
							text,
							Fun.DecodingUTF8(szMsg[4].ToString())
						});
						fGameManager.GetInstance().OnUserLost("SVR_WEB_FTUSER");
						return;
					}
				}
				else
				{
					if (num2 == 4013)
					{
						fGameManager.isFtUser = true;
						fGameManager.sFtUserTip = Language.GetLang(22003, new object[]
						{
							Fun.DecodingUTF8(szMsg[1].ToString())
						});
						fGameManager.GetInstance().OnUserLost("SVR_WEB_UDIDMAC");
						return;
					}
					if (num2 == 10025)
					{
						PlayerDataManager.getInstance().playerdet.SetMoneyData((ArrayList)szMsg[1]);
						return;
					}
				}
			}
			else if (num2 <= 10097)
			{
				if (num2 == 10096)
				{
					PlayerDataManager.getInstance().HUserData.OnChangeNickBack(szMsg);
					return;
				}
				if (num2 == 10097)
				{
					PlayerDataManager.getInstance().HUserData.OnResetChangeNick(szMsg);
					return;
				}
			}
			else
			{
				if (num2 == 11006)
				{
					HeroDataManager.GetInstance().GetPlayerOldRewardBack(szMsg);
					return;
				}
				if (num2 == 16100)
				{
					ActivityManager.GetInstance().OnActivityMsg(szMsg);
					return;
				}
				if (num2 == 16200)
				{
					SkyLadderManager.GetInstance().OnSkyLadderMsg((int)szMsg[0], (ArrayList)szMsg[1]);
					return;
				}
			}
		}
		else if (num2 <= 60100)
		{
			if (num2 <= 20058)
			{
				if (num2 <= 19500)
				{
					if (num2 == 17000)
					{
						RankManager.GetInstance().OnRankMsg((int)szMsg[0], (ArrayList)szMsg[1]);
						return;
					}
					if (num2 == 19500)
					{
						PveManager.GetInstance().OnPve_NetMsg((int)szMsg[0], (ArrayList)szMsg[1]);
						return;
					}
				}
				else
				{
					if (num2 == 20013)
					{
						RoomManage.GetInstance().OnRoomMsg(szMsg);
						return;
					}
					if (num2 == 20058)
					{
						FunManager.Instance.SetServerTime((string)szMsg[0], (int)szMsg[1]);
						return;
					}
				}
			}
			else if (num2 <= 41111)
			{
				if (num2 == 21002)
				{
					PlayerDataManager.getInstance().HUserData.UserUpExp((int)szMsg[2], (int)szMsg[3], (int)szMsg[4]);
					return;
				}
				if (num2 == 41111)
				{
					Debug.Log("Hall Client:" + (string)szMsg[0] + "  Server:" + (string)szMsg[1]);
					return;
				}
			}
			else
			{
				if (num2 == 60000)
				{
					TeamManager.GetInstance().OnTeamMsg(szMsg);
					return;
				}
				if (num2 == 60100)
				{
					FriendManager.GetInstance().OnFriendMsg(szMsg);
					return;
				}
			}
		}
		else if (num2 <= 80001)
		{
			if (num2 <= 70000)
			{
				if (num2 == 60118)
				{
					PlayerDataManager.getInstance().HUserData.InitFriendData((ArrayList)szMsg[0], (ArrayList)szMsg[1], (ArrayList)szMsg[2]);
					return;
				}
				if (num2 == 70000)
				{
					ShopManager.GetInstance().ShopServerMsgToClient((int)szMsg[0], (ArrayList)szMsg[1]);
					return;
				}
			}
			else
			{
				if (num2 == 71000)
				{
					TaskManager.GetInstance().OnServerMsg((int)szMsg[0], (ArrayList)szMsg[1]);
					return;
				}
				if (num2 == 80001)
				{
					MatchManager.GetInstance().OnMatchMsg(szMsg);
					return;
				}
			}
		}
		else if (num2 <= 100001)
		{
			if (num2 == 91000)
			{
				GuildManager.GetInstance().GuildServerMsgToClient((int)szMsg[0], (ArrayList)szMsg[1]);
				return;
			}
			if (num2 == 100001)
			{
				PlayerDataManager.getInstance().HUserData.SetCareerData(szMsg);
				bool flag9 = MainUICon.inst != null;
				if (flag9)
				{
					MainUICon.inst.OnOpenCareerUI();
				}
				return;
			}
		}
		else
		{
			if (num2 == 3010001)
			{
				GameRoot.inst.Setting.Set_Data(string.Concat(new string[]
				{
					szMsg[0].ToString(),
					"&",
					szMsg[1].ToString(),
					"&",
					szMsg[2].ToString(),
					"&",
					szMsg[3].ToString(),
					"&",
					szMsg[4].ToString()
				}));
				Fun.Log("读取到了系统设置", null);
				bool flag10 = !fGameManager.bFirstLogin;
				if (flag10)
				{
					fUISoundManager.instance.PlayUISound("zhujiemian_beijingyinyue", 0, new fUISoundManager.UISoundStopFun(fGameManager.GetInstance().SoundStop), 10);
					fGameManager.bFirstLogin = true;
				}
				else
				{
					fGameManager.GetInstance().SoundStop();
				}
				bool flag11 = MainUICon.inst != null && MainUICon.inst.visible;
				if (flag11)
				{
					MainUICon.inst.SetSoundBtn();
				}
				return;
			}
			if (num2 == 3010014)
			{
				Fun.Log("保存成功了", null);
				return;
			}
			switch (num2)
			{
			case 3020102:
			{
				Gem_Data.Instance.Update_Package_Data(szMsg);
				bool flag12 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag12)
				{
					Gem_UICon.Instance.View_Component.Display_Package();
				}
				return;
			}
			case 3020104:
			{
				Gem_Data.Instance.Update_Package_Data(szMsg);
				bool flag13 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag13)
				{
					Gem_UICon.Instance.View_Component.Display_Package();
				}
				return;
			}
			case 3020106:
			{
				bool flag14 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag14)
				{
					Gem_UICon.Instance.View_Component.Sell_Result(int.Parse(szMsg[0].ToString()), (ArrayList)szMsg[1], int.Parse(szMsg[2].ToString()));
				}
				return;
			}
			case 3020108:
			{
				Gem_Data.Instance.Update_Equip_Data(szMsg[1].ToString());
				bool flag15 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag15)
				{
					Gem_UICon.Instance.View_Component.Equip_Result(int.Parse(szMsg[0].ToString()), int.Parse(szMsg[2].ToString()), int.Parse(szMsg[3].ToString()));
				}
				return;
			}
			case 3020110:
			{
				bool flag16 = Fun.Convert_Int(szMsg[0].ToString()) > 0;
				if (flag16)
				{
					Gem_Data.Instance.Update_Equip_Data(szMsg[1].ToString());
				}
				bool flag17 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag17)
				{
					Gem_UICon.Instance.View_Component.Equip_Result(int.Parse(szMsg[0].ToString()), int.Parse(szMsg[2].ToString()), int.Parse(szMsg[3].ToString()));
				}
				return;
			}
			case 3020112:
			{
				bool flag18 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag18)
				{
					Gem_UICon.Instance.View_Component.Synthetic_Result(int.Parse(szMsg[0].ToString()), int.Parse(szMsg[1].ToString()));
				}
				return;
			}
			case 3020114:
			{
				bool flag19 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag19)
				{
					Gem_UICon.Instance.View_Component.Refresh_Synthetic(szMsg);
				}
				return;
			}
			case 3020116:
			{
				bool flag20 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag20)
				{
					Gem_UICon.Instance.View_Component.Batch_Synthetic_Result(int.Parse(szMsg[0].ToString()), int.Parse(szMsg[1].ToString()), int.Parse(szMsg[2].ToString()), int.Parse(szMsg[3].ToString()));
				}
				return;
			}
			case 3020117:
				Gem_Data.Instance.Update_Have_New_Data(szMsg[0].ToString());
				return;
			case 3020119:
			{
				bool flag21 = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
				if (flag21)
				{
					Gem_UICon.Instance.View_Component.Refresh_Synthetic_Success_Rate(szMsg);
				}
				return;
			}
			}
		}
		Debug.LogError("SendMsgToHallClient 消息ID没有找到." + nNetID.ToString());
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0009800C File Offset: 0x0009620C
	private void OnShowLearnMsg(int nHadLearn)
	{
		bool flag = nHadLearn == 1;
		if (flag)
		{
			fGameManager.IsHadLearn = true;
		}
		else
		{
			fUIManager.GetInstance().ShowJiaoXueWin(1);
		}
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x0009803A File Offset: 0x0009623A
	private void Initialize_Reward_Data(ArrayList szNetMsg)
	{
		Prerogative_Data.Instance = PlayerDataManager.getInstance().playerCommon.Prerogative_Class;
		Prerogative_Data.Instance.Initialize_Reward_Data(szNetMsg);
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0009805D File Offset: 0x0009625D
	private void OnCLickQuit(object szParam)
	{
		Application.Quit();
	}

	// Token: 0x04000CF0 RID: 3312
	public bool Load_Data_State;
}

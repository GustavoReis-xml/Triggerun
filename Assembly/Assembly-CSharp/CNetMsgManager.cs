using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class CNetMsgManager : MonoBehaviour
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000660 RID: 1632 RVA: 0x000356A4 File Offset: 0x000338A4
	// (set) Token: 0x06000661 RID: 1633 RVA: 0x000356BB File Offset: 0x000338BB
	public static CNetMsgManager Instance
	{
		get
		{
			return CNetMsgManager._instance;
		}
		set
		{
			CNetMsgManager._instance = value;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x000356C4 File Offset: 0x000338C4
	// (set) Token: 0x06000663 RID: 1635 RVA: 0x000356F6 File Offset: 0x000338F6
	public Hero_Message_Handle Hero_Message_Handle
	{
		get
		{
			bool flag = this._Hero_Message_Handle == null;
			if (flag)
			{
				this._Hero_Message_Handle = new Hero_Message_Handle();
			}
			return this._Hero_Message_Handle;
		}
		set
		{
			this._Hero_Message_Handle = value;
		}
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00035700 File Offset: 0x00033900
	private void Start()
	{
		CNetMsgManager.Instance = this;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0003570C File Offset: 0x0003390C
	public void MSGClient(HeroPlayer player, int NetID, ArrayList szNetMsg)
	{
		if (NetID <= 20014)
		{
			if (NetID <= 9003)
			{
				if (NetID <= 2000)
				{
					if (NetID <= 1205)
					{
						if (NetID != 1006)
						{
							if (NetID == 1205)
							{
								bool flag = szNetMsg.Count == 7;
								if (flag)
								{
									ArrayList arrayList = (ArrayList)szNetMsg[3];
									int nIsFinder = (int)szNetMsg[6];
									int nLevel = (int)szNetMsg[4];
									fUIManager.GetInstance().OnFindOtherUserNo((int)arrayList[0], (ArrayList)arrayList[1], nIsFinder, Fun.DecodingUTF8(szNetMsg[2].ToString()), nLevel);
								}
								else
								{
									ArrayList arrayList2 = (ArrayList)szNetMsg[3];
									fUIManager.GetInstance().OnFindOtherUserOk((int)arrayList2[0], (ArrayList)arrayList2[1]);
								}
							}
						}
						else
						{
							ChatManager.GetInstance().Chat_Server_To_ClientNetMsg((int)szNetMsg[0], szNetMsg);
						}
					}
					else if (NetID != 1306)
					{
						if (NetID != 1307)
						{
							if (NetID == 2000)
							{
								ChatManager.GetInstance().OnHuDongNetMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
							}
						}
						else
						{
							HeroDataManager.GetInstance().OnFcmDataMsg(szNetMsg);
						}
					}
					else
					{
						HeroDataManager.GetInstance().OnHeroDataMsg(szNetMsg);
					}
				}
				else if (NetID <= 5011)
				{
					if (NetID != 5000)
					{
						if (NetID != 5009)
						{
							if (NetID == 5011)
							{
								player.PlayerSelect(szNetMsg);
							}
						}
						else
						{
							roleuiManager.GetInstance().OpenSelectHeroUI(1);
						}
					}
					else
					{
						FBManager.GetInstance().OnFbMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
					}
				}
				else if (NetID != 5031)
				{
					if (NetID != 5033)
					{
						if (NetID == 9003)
						{
							MatchManager.GetInstance().UserJoinFbLog(8, "服务端获取客户端状态:" + MatchManager.GetInstance().m_nJoinState.ToString());
						}
					}
					else
					{
						PlayerLearnManager.GetInstance().OnHpFull();
					}
				}
				else
				{
					TeamManager.GetInstance().m_nExitTeam = 1;
				}
			}
			else if (NetID <= 19000)
			{
				if (NetID <= 10095)
				{
					if (NetID != 10001)
					{
						if (NetID != 10094)
						{
							if (NetID == 10095)
							{
								bool flag2 = CPaintManager.Instance != null;
								if (flag2)
								{
									CPaintManager.Instance.PaintInfoSync(szNetMsg);
								}
							}
						}
						else
						{
							player.MatchUserInfo(szNetMsg);
						}
					}
					else
					{
						player.PlayerCreate(szNetMsg);
					}
				}
				else if (NetID != 16001)
				{
					if (NetID != 16204)
					{
						if (NetID == 19000)
						{
							KingTrainManager.GetInstance().OnKingTrain_Scn_NetMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
						}
					}
					else
					{
						SkyLadderManager.GetInstance().OnSkyLadderExit(szNetMsg);
					}
				}
				else
				{
					bool flag3 = gameuiCon.inst != null;
					if (flag3)
					{
						gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(3030, new object[]
						{
							szNetMsg[0],
							szNetMsg[1]
						}));
					}
				}
			}
			else if (NetID <= 19700)
			{
				if (NetID != 19100)
				{
					if (NetID != 19600)
					{
						if (NetID == 19700)
						{
							WarPersonManager.Instance.ClientMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1], player);
						}
					}
					else
					{
						DtsBatteManager.GetInstance().OnDts_Scn_NetMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1], player);
					}
				}
				else
				{
					KingTrainManager.GetInstance().OnHeiAnTrain_Scn_NetMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
				}
			}
			else if (NetID != 20001)
			{
				if (NetID != 20013)
				{
					if (NetID == 20014)
					{
						MatchManager.GetInstance().OnMatchMsg(szNetMsg);
					}
				}
				else
				{
					RoomManage.GetInstance().OnRoomMsg(szNetMsg);
				}
			}
			else
			{
				fGameManager.mInst.OnReturnHallOK((int)szNetMsg[0], (string)szNetMsg[1], (int)szNetMsg[2]);
			}
		}
		else if (NetID <= 71000)
		{
			if (NetID <= 41111)
			{
				if (NetID <= 21002)
				{
					if (NetID != 20058)
					{
						if (NetID == 21002)
						{
							player.playerCommon.UserUpExp((int)szNetMsg[2], (int)szNetMsg[3]);
						}
					}
					else
					{
						FunManager.Instance.SetServerTime((string)szNetMsg[0], (int)szNetMsg[1]);
					}
				}
				else if (NetID != 40001)
				{
					if (NetID != 40004)
					{
						if (NetID == 41111)
						{
							Debug.Log("Client:" + (string)szNetMsg[0] + "  Server:" + (string)szNetMsg[1]);
						}
					}
					else
					{
						CMapEffectManager.Instance.EndMapEffectAIForUser(player, (int)szNetMsg[0]);
					}
				}
				else
				{
					CMapEffectManager.Instance.BeginEffectForUser(player, (int)szNetMsg[0]);
				}
			}
			else if (NetID <= 60003)
			{
				if (NetID != 52002)
				{
					if (NetID != 60000)
					{
						if (NetID == 60003)
						{
							TeamManager.GetInstance().OnLeaveTeam();
						}
					}
					else
					{
						TeamManager.GetInstance().OnTeamMsg(szNetMsg);
					}
				}
				else
				{
					int nNetID = (int)szNetMsg[0];
					NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
					bool flag4 = netLiving != null;
					if (flag4)
					{
						netLiving.playerCommon.m_nInJidi = (int)szNetMsg[1];
					}
				}
			}
			else if (NetID != 60100)
			{
				if (NetID != 60118)
				{
					if (NetID == 71000)
					{
						TaskManager.GetInstance().OnServerMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
					}
				}
				else
				{
					PlayerDataManager.getInstance().HUserData.InitFriendData((ArrayList)szNetMsg[0], (ArrayList)szNetMsg[1], (ArrayList)szNetMsg[2]);
				}
			}
			else
			{
				FriendManager.GetInstance().OnFriendMsg(szNetMsg);
			}
		}
		else if (NetID <= 302000)
		{
			if (NetID <= 110100)
			{
				if (NetID != 91000)
				{
					if (NetID != 110000)
					{
						if (NetID == 110100)
						{
							Scramble_Message_Handle.Instantiation.Message_Handle((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
						}
					}
					else
					{
						Arena_Message_Handle.Instantiation.Message_Handle((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
					}
				}
				else
				{
					GuildManager.GetInstance().GuildServerMsgToClient((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
				}
			}
			else if (NetID != 110200)
			{
				if (NetID != 110300)
				{
					if (NetID == 302000)
					{
						player.playerCommon.Prerogative_Class.Receive_Message(int.Parse(szNetMsg[0].ToString()), (ArrayList)szNetMsg[1]);
					}
				}
				else
				{
					Arena_Score_Message_Handle.Instantiation.Message_Handle((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
				}
			}
			else
			{
				Training_Message_Handle.Instantiation.Message_Handle((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
			}
		}
		else if (NetID <= 1030000)
		{
			if (NetID != 1010000)
			{
				if (NetID != 1020000)
				{
					if (NetID == 1030000)
					{
						Door_Control.Get_Instantiate.Special_Door_Handle((int)szNetMsg[0], szNetMsg[1].ToString());
						Door_Control.Get_Instantiate.General_Door_Handle((int)szNetMsg[0], szNetMsg[1].ToString(), true);
					}
				}
				else
				{
					bool flag5 = player.UserControl != null;
					if (flag5)
					{
						player.Npc_Handle_Component.Receive_Network_Message((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
					}
				}
			}
			else
			{
				this.Hero_Message_Handle.Handle(player, szNetMsg);
			}
		}
		else if (NetID != 2010000)
		{
			if (NetID != 2050001)
			{
				if (NetID == 3010000)
				{
					GameRoot.inst.Setting.Receive_Message(int.Parse(szNetMsg[0].ToString()), (ArrayList)szNetMsg[1]);
				}
			}
			else
			{
				player.LuoJiHurtLeiJi(szNetMsg);
			}
		}
		else
		{
			this.Hero_Message_Handle.Handle(player, szNetMsg);
		}
	}

	// Token: 0x04000495 RID: 1173
	private static CNetMsgManager _instance;

	// Token: 0x04000496 RID: 1174
	private Hero_Message_Handle _Hero_Message_Handle;
}

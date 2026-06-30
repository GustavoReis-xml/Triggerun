using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class ChatManager
{
	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x00022980 File Offset: 0x00020B80
	// (set) Token: 0x0600046D RID: 1133 RVA: 0x00022998 File Offset: 0x00020B98
	public bool isChatPopTip
	{
		get
		{
			return this._isChatPopTip;
		}
		set
		{
			this._isChatPopTip = value;
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x000229A4 File Offset: 0x00020BA4
	public static ChatManager GetInstance()
	{
		bool flag = ChatManager.mInst == null;
		if (flag)
		{
			ChatManager.mInst = new ChatManager();
		}
		return ChatManager.mInst;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x000229D4 File Offset: 0x00020BD4
	public void Chat_Server_To_ClientNetMsg(int nPinDao, ArrayList szNetMsg)
	{
		string text = string.Empty;
		string sPname = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string empty = string.Empty;
		int nSiLiaoType = 1;
		empty = string.Empty;
		bool flag = (nPinDao == 1 && fGameManager.m_nUserType == 1) || ChatUICon.inst == null;
		if (!flag)
		{
			bool flag2 = szNetMsg.Count > 3;
			if (flag2)
			{
				szNetMsg[1] = Language.GetUTFCodeStr(szNetMsg[1].ToString());
				szNetMsg[2] = Language.GetUTFCodeStr(szNetMsg[2].ToString());
				szNetMsg[3] = Language.GetUTFCodeStr(szNetMsg[3].ToString());
				text3 = szNetMsg[2].ToString();
				text3 = text3.Replace("\\", string.Empty);
			}
			bool flag3 = nPinDao == 999;
			if (flag3)
			{
				switch (int.Parse(szNetMsg[1].ToString()))
				{
				case 1:
					this.OnSendSystemMsg(Language.GetLang(39000, new object[0]), "ffdd00", "system");
					break;
				case 2:
					this.OnSendSystemMsg(Language.GetLang(39001, new object[0]), "ffdd00", "system");
					break;
				case 3:
					this.OnSendSystemMsg(Language.GetLang(39002, new object[]
					{
						Fun.SecToMaxTime(int.Parse(szNetMsg[2].ToString()), "d", "s")
					}), "ffdd00", "system");
					break;
				}
			}
			else
			{
				bool flag4 = this.isPingBiUser(szNetMsg[1].ToString());
				if (!flag4)
				{
					switch (nPinDao)
					{
					case 1:
					{
						bool flag5 = szNetMsg[1].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
						if (flag5)
						{
							string oldValue = "[00ff00]" + Language.GetLang(39008, new object[0]) + "[-]";
							text3 = text3.ToString().Replace(oldValue, string.Empty);
						}
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.shijie,
							">",
							szNetMsg[1].ToString(),
							":",
							text3,
							"</color>"
						});
						text += empty;
						sPname = "shijie";
						text2 = szNetMsg[1].ToString();
						break;
					}
					case 2:
					{
						bool flag6 = szNetMsg.Count == 2 && int.Parse(szNetMsg[1].ToString()) == -2;
						if (flag6)
						{
							this.OnSendSystemMsg(Language.GetLang(39003, new object[0]), "ffdd00", "system");
							return;
						}
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.xiaodui,
							">",
							szNetMsg[1].ToString(),
							":",
							text3,
							"</color>"
						});
						text += empty;
						sPname = "duiwu";
						text2 = szNetMsg[1].ToString();
						break;
					}
					case 3:
					{
						bool flag7 = szNetMsg.Count == 2 && int.Parse(szNetMsg[1].ToString()) == -2;
						if (flag7)
						{
							this.OnSendSystemMsg(Language.GetLang(39004, new object[0]), "ffdd00", "system");
							return;
						}
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.duiwu,
							">",
							szNetMsg[1].ToString(),
							":",
							text3,
							"</color>"
						});
						text += empty;
						sPname = "zhenying";
						text2 = szNetMsg[1].ToString();
						break;
					}
					case 4:
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.bisai,
							">",
							szNetMsg[1].ToString(),
							":",
							text3,
							"</color>"
						});
						text += empty;
						sPname = "bisai";
						text2 = szNetMsg[1].ToString();
						break;
					case 5:
					{
						string text4 = string.Empty;
						bool flag8 = szNetMsg.Count == 4 && int.Parse(szNetMsg[1].ToString()) == -2;
						if (flag8)
						{
							ChatUICon.inst.OnDeleteSiLiaoList(szNetMsg[3].ToString());
							this.OnSendSystemMsg(Language.GetLang(39005, new object[0]), "ffdd00", "system");
							return;
						}
						bool flag9 = szNetMsg[1].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
						if (flag9)
						{
							text = ((!(WebParam.client_language == "baxi")) ? string.Concat(new string[]
							{
								"<color=#",
								ChatColor.siliao,
								">",
								Language.GetLang(39009, new object[]
								{
									szNetMsg[3].ToString()
								}),
								szNetMsg[2].ToString(),
								"</color>"
							}) : string.Concat(new object[]
							{
								"<color=#",
								ChatColor.siliao,
								">",
								szNetMsg[1],
								":",
								szNetMsg[2].ToString(),
								"</color>"
							}));
							text4 = szNetMsg[3].ToString();
							nSiLiaoType = 2;
						}
						else
						{
							int num = -1;
							bool flag10 = num != -1;
							if (flag10)
							{
								return;
							}
							nSiLiaoType = 1;
							text = ((!(WebParam.client_language == "baxi")) ? string.Concat(new string[]
							{
								"<color=#",
								ChatColor.siliao,
								">",
								Language.GetLang(39010, new object[]
								{
									szNetMsg[1].ToString()
								}),
								szNetMsg[2].ToString(),
								"</color>"
							}) : string.Concat(new object[]
							{
								"<color=#",
								ChatColor.siliao,
								">",
								szNetMsg[1],
								":",
								szNetMsg[2].ToString(),
								"</color>"
							}));
							text4 = szNetMsg[1].ToString();
						}
						sPname = "siliao";
						text2 = text4;
						break;
					}
					case 6:
					{
						bool flag11 = szNetMsg.Count == 2 && int.Parse(szNetMsg[1].ToString()) == -2;
						if (flag11)
						{
							this.OnSendSystemMsg(Language.GetLang(39006, new object[0]), "ffdd00", "system");
							return;
						}
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.bisai,
							">",
							szNetMsg[1].ToString(),
							":",
							text3,
							"</color>"
						});
						text += empty;
						sPname = "room";
						text2 = szNetMsg[1].ToString();
						break;
					}
					case 7:
					{
						bool flag12 = szNetMsg.Count == 2 && int.Parse(szNetMsg[1].ToString()) == -2;
						if (flag12)
						{
							this.OnSendSystemMsg(Language.GetLang(39007, new object[0]), "ffdd00", "system");
							return;
						}
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.guild,
							">",
							szNetMsg[1].ToString(),
							":",
							szNetMsg[2].ToString(),
							"</color>"
						});
						sPname = "guild";
						text2 = szNetMsg[1].ToString();
						break;
					}
					case 8:
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.system,
							">",
							szNetMsg[2].ToString(),
							"</color>"
						});
						sPname = szNetMsg[1].ToString();
						text2 = string.Empty;
						break;
					case 10:
						text = string.Concat(new string[]
						{
							"<color=#",
							ChatColor.laba,
							">",
							szNetMsg[1].ToString(),
							":",
							szNetMsg[2].ToString(),
							"</color>"
						});
						sPname = "laba";
						text2 = szNetMsg[1].ToString();
						ChatUICon.inst.OnLaBaChat(text2, szNetMsg[2].ToString(), szNetMsg);
						break;
					}
					ChatUICon.inst.SendPinDaoMsg(nPinDao, 0, sPname, text, text2, nSiLiaoType, (ArrayList)szNetMsg[8], (int)szNetMsg[6]);
				}
			}
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002334C File Offset: 0x0002154C
	public void OnSendSystemMsg(string sMsg, string sColor = "ffdd00", string sSystem = "system")
	{
		string sText = string.Empty;
		sText = string.Concat(new string[]
		{
			"<color=#",
			sColor,
			">",
			sMsg,
			"</color>"
		});
		bool flag = ChatUICon.inst != null;
		if (flag)
		{
			ChatUICon.inst.SendPinDaoMsg(8, 0, sSystem, sText, string.Empty, 2, null, 0);
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x000233B4 File Offset: 0x000215B4
	public void OnHuDongNetMsg(int nNetId, ArrayList szNetMsg)
	{
		if (nNetId != 2001)
		{
			if (nNetId == 2002)
			{
				HuDongUICon.SetStartCdTime((float)((int)szNetMsg[1]));
				bool flag = HuDongUICon.inst != null;
				if (flag)
				{
					HuDongUICon.inst.OnUICd();
				}
			}
		}
		else
		{
			this.OnHuDong(szNetMsg);
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00023418 File Offset: 0x00021618
	private void OnHuDong(ArrayList szData)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		bool flag = szData.Count == 2;
		if (flag)
		{
			bool flag2 = !fGameManager.Heroplayer.isCopy;
			if (flag2)
			{
				this.OnHuDongChatMsg((int)szData[0], szData);
			}
		}
		else
		{
			int net_ID = (int)szData[6];
			NetLiving netLiving = Fun.Get_NetLiving(net_ID);
			bool flag3 = netLiving == null || !netLiving.playerHaveCreate || netLiving.haveDestroy;
			if (!flag3)
			{
				ArrayList arrayList = (ArrayList)szData[0];
				int num = (int)szData[5];
				bool flag4 = fGameManager.GetInstance().getPlayer().playerCommon.m_nShiliServer == num && Fun.DecodingUTF8(arrayList[2].ToString()) != string.Empty && !netLiving.isCopy;
				if (flag4)
				{
					this.OnHuDongChatMsg((int)arrayList[0], arrayList);
				}
				text = szData[1].ToString();
				bool flag5 = text != string.Empty && !netLiving.isDead;
				if (flag5)
				{
					netLiving.playerSound.PlayZhaoHuSound(text);
				}
				text2 = szData[2].ToString();
				int num2 = (int)szData[4];
				int num3 = (int)szData[8];
				int num4 = (int)szData[9];
				int num5 = (int)szData[10];
				int num6 = (int)szData[11];
				int num7 = CFilmManager.Instance.filmNetId(netLiving);
				bool flag6 = num2 == 1 && netLiving.getNetid() == num7 && !netLiving.bePlayActive;
				if (!flag6)
				{
					bool flag7 = text2 != string.Empty;
					if (flag7)
					{
						bool flag8 = int.Parse(text2) < 50;
						if (flag8)
						{
							num4 = 1;
						}
						bool flag9 = num3 != 1 || num4 != 1 || num6 != 0 || num5 != 1;
						if (!flag9)
						{
							bool flag10 = int.Parse(text2) >= 50 && netLiving.getNetid() == num7;
							if (flag10)
							{
								netLiving.playerZhunxingMe.zhunxingshowOrHide(false);
								ChatManager.IsShowZhunXing = false;
							}
							bool flag11 = num7 == netLiving.getNetid();
							if (flag11)
							{
								bool flag12 = int.Parse(text2) >= 50 && netLiving.FpsOrTps != 2;
								if (flag12)
								{
									bool flag13 = !netLiving.isCopy;
									if (flag13)
									{
										netLiving.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.OnChangeCameraOk), text2);
									}
									else
									{
										netLiving.changeCameraState(CameraState.ThirdState, null, string.Empty);
									}
								}
								else
								{
									bool flag14 = !netLiving.isCopy;
									if (flag14)
									{
										netLiving.SetAnimator("HuDongState", 2, int.Parse(text2), 2);
									}
								}
							}
							text3 = szData[7].ToString();
							bool flag15 = text3 != string.Empty && num7 != netLiving.getNetid() && !(netLiving.newplayercon == null);
							if (flag15)
							{
								netLiving.newplayercon.AddFaceIco(text3, num2);
							}
						}
					}
					else
					{
						text3 = szData[7].ToString();
						bool flag16 = text3 != string.Empty && num7 != netLiving.getNetid() && !(netLiving.newplayercon == null);
						if (flag16)
						{
							netLiving.newplayercon.AddFaceIco(text3, num2);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x000237D0 File Offset: 0x000219D0
	private void OnChangeCameraOk(object obj)
	{
		string s = obj.ToString();
		fGameManager.Heroplayer.SetAnimator("HuDongState", 2, int.Parse(s), 2);
		fGameManager.Heroplayer.nHuDongState = int.Parse(s);
		fGameManager.Heroplayer.HuDongChangeCameraIng = true;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00023820 File Offset: 0x00021A20
	private void OnHuDongChatMsg(int nPinDao, ArrayList szNetMsg)
	{
		string sText = string.Empty;
		string str = string.Empty;
		string text = string.Empty;
		string text2 = string.Empty;
		string str2 = string.Empty;
		bool flag = szNetMsg.Count > 2;
		if (flag)
		{
			str = Fun.DecodingUTF8(szNetMsg[2].ToString());
		}
		bool flag2 = (int)szNetMsg[0] == -3;
		if (flag2)
		{
			this.OnSendSystemMsg(Language.GetLang(39013, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag3 = (int)szNetMsg[0] == -2;
			if (flag3)
			{
				this.OnSendSystemMsg(Language.GetLang(39014, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag4 = (int)szNetMsg[0] == -1;
				if (flag4)
				{
					this.OnSendSystemMsg(Language.GetLang(39015, new object[0]), "ffdd00", "system");
				}
				else
				{
					text = Fun.DecodingUTF8(szNetMsg[1].ToString());
					text2 = Fun.DecodingUTF8(szNetMsg[3].ToString());
					bool flag5 = text2 != string.Empty;
					if (flag5)
					{
						bool flag6 = PlayerDataManager.getInstance().playercom.m_sName == text2;
						if (flag6)
						{
							text2 = Language.GetLang(39011, new object[0]);
						}
						str2 = Language.GetLang(39012, new object[]
						{
							text,
							text2
						});
					}
					else
					{
						str2 = text + ":";
					}
					sText = "<color=#00C3FF>" + str2 + str + "</color>";
					ChatUICon.inst.SendPinDaoMsg(nPinDao, 0, "zhenying", sText, text, 1, (ArrayList)szNetMsg[8], (int)szNetMsg[6]);
				}
			}
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000239F8 File Offset: 0x00021BF8
	public void Chat_Client_To_ServerNetMsg(int nPinDao, ArrayList szNetMsg)
	{
		bool flag = nPinDao == 10;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1006, szNetMsg);
		}
		else
		{
			bool flag2 = fGameManager.m_nUserType == 1;
			if (flag2)
			{
				fGameManager.SendMsgToServer(1006, szNetMsg);
			}
			else
			{
				fGameManager.SendMsgToHallServer(1006, szNetMsg);
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00023A4C File Offset: 0x00021C4C
	public void Chat_Client_To_ScnNetMsg(int nPinDao, ArrayList szNetMsg)
	{
		bool flag = (nPinDao == 3 || nPinDao == 4) && fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.SendMsgToServer(1012, szNetMsg);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00023A80 File Offset: 0x00021C80
	public void OnSendMsg(int nType)
	{
		bool flag = (GameResultUICon.Inst != null && GameResultUICon.Inst.visible) || (gameuiCon.inst != null && gameuiCon.inst.mainRoundUI.mboolstat) || fUIManager.GetInstance().GetResultModelScn();
		if (!flag)
		{
			string text = string.Empty;
			string sFormatMsg = string.Empty;
			bool flag2 = nType == 0;
			if (!flag2)
			{
				HeroPlayer heroplayer = fGameManager.Heroplayer;
				bool flag3 = heroplayer == null || !heroplayer.playerHaveCreate || heroplayer.playerCommon.m_nHunMi == 1 || heroplayer.playerCommon.m_nJiangzhi == 1 || heroplayer.playerCommon.m_nDongjie == 1 || heroplayer.playerCommon.m_nPlayerState == 0;
				if (!flag3)
				{
					if (nType != 1)
					{
						if (nType == 9)
						{
							bool flag4 = CPaintManager.Instance != null;
							if (flag4)
							{
								CPaintManager.Instance.OnSetPaint();
							}
							return;
						}
					}
					else
					{
						bool flag5 = !heroplayer.bePlayActive || heroplayer.beSuperSkill;
						if (flag5)
						{
							return;
						}
					}
					int nModelIndex = heroplayer.playerCommon.m_nModelIndex;
					bool flag6 = !fGameManager.mloadConfig.AllHuDongUserData.ContainsKey(nModelIndex);
					if (!flag6)
					{
						HuDongHeroData huDongHeroData = fGameManager.mloadConfig.AllHuDongUserData[nModelIndex];
						bool flag7 = !huDongHeroData.AllHuDongData.ContainsKey(nType);
						if (!flag7)
						{
							GamePlayer mubiaoObj = this.GetMubiaoObj();
							bool flag8 = mubiaoObj != null;
							if (flag8)
							{
								text = mubiaoObj.playerCommon.m_sName;
							}
							List<hudongData> list = huDongHeroData.AllHuDongData[nType];
							hudongData data;
							if (nType != 4)
							{
								if (nType != 5)
								{
									data = list[0];
								}
								else
								{
									bool flag9 = text == string.Empty;
									if (flag9)
									{
										data = list[0];
									}
									else
									{
										nModelIndex = mubiaoObj.playerCommon.m_nModelIndex;
										bool flag10 = !fGameManager.mloadConfig.AllHuDongUserData.ContainsKey(nModelIndex);
										if (flag10)
										{
											return;
										}
										HuDongHeroData huDongHeroData2 = fGameManager.mloadConfig.AllHuDongUserData[nModelIndex];
										int nZlType = huDongHeroData2.m_nZlType;
										if (!true)
										{
										}
										hudongData hudongData;
										if (nZlType != 2)
										{
											if (nZlType != 3)
											{
												hudongData = list[0];
											}
											else
											{
												hudongData = list[2];
											}
										}
										else
										{
											hudongData = list[1];
										}
										if (!true)
										{
										}
										data = hudongData;
									}
								}
							}
							else
							{
								data = (heroplayer.playerDazhao.canDo ? list[1] : list[0]);
								bool flag11 = heroplayer.heroBase.GetType() == typeof(HeroLingLongMe);
								if (flag11)
								{
									int transmission_Durability = ((HeroLingLongMe)heroplayer.heroBase).Transmission_Durability;
									bool flag12 = transmission_Durability > 0;
									if (flag12)
									{
										data = list[2];
										sFormatMsg = transmission_Durability.ToString();
									}
								}
							}
							int num = heroplayer.wc.CanFirePublicCD(1f) ? 1 : 0;
							int num2 = heroplayer.tChar.onGround ? 1 : 0;
							int num3 = heroplayer.bePlayActive ? 1 : 0;
							int nHuDongState = heroplayer.nHuDongState;
							ArrayList msg = this.GetMsg(data, heroplayer.playerCommon.m_nModelIndex, sFormatMsg);
							bool flag13 = msg != null;
							if (flag13)
							{
								ArrayList value = new ArrayList
								{
									num,
									num2,
									num3,
									nHuDongState
								};
								this.Chat_Client_To_ScnNetMsg(3, new ArrayList
								{
									3,
									msg[0].ToString(),
									Fun.EncodingUTF8(text),
									new ArrayList(),
									msg,
									value,
									this.GetUserChatParam()
								});
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00023E90 File Offset: 0x00022090
	public ArrayList GetUserChatParam()
	{
		return new ArrayList
		{
			Fun.GetLzImgName(),
			Fun.GetLzNianImgName(),
			Fun.GetQQ_Zuan_Type(null)
		};
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00023ED0 File Offset: 0x000220D0
	private GamePlayer GetMubiaoObj()
	{
		GameObject mubiaoObj = fGameManager.Heroplayer.getMubiaoObj(2, false);
		bool flag = mubiaoObj == null;
		GamePlayer result;
		if (flag)
		{
			result = null;
		}
		else
		{
			PublicInfo component = mubiaoObj.transform.root.gameObject.GetComponent<PublicInfo>();
			bool flag2 = component == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = (component.NetObject as GamePlayer);
			}
		}
		return result;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00023F34 File Offset: 0x00022134
	private ArrayList GetMsg(hudongData data, int nHeroIndex, string sFormatMsg = "")
	{
		string text = string.Empty;
		string value = string.Empty;
		string value2 = string.Empty;
		int nIsDo = data.m_nIsDo;
		bool flag = nIsDo == 0;
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			int num = (int)UnityEngine.Random.Range(0f, (float)nIsDo);
			bool flag2 = num < data.m_szSound.Count;
			if (flag2)
			{
				value = data.m_szSound[num].ToString();
			}
			bool flag3 = num < data.m_szMsg.Count;
			if (flag3)
			{
				text = data.m_szMsg[num].ToString();
				bool flag4 = sFormatMsg != string.Empty;
				if (flag4)
				{
					text = string.Format(text, sFormatMsg);
				}
			}
			bool flag5 = data.m_nType == 1;
			if (flag5)
			{
				EmotionUnlock useEmotionData = HeroDataManager.GetInstance().GetUseEmotionData(nHeroIndex, PlayerDataManager.getInstance().HUserData.m_nCid);
				bool flag6 = useEmotionData == null;
				if (flag6)
				{
					Debug.LogError("------------------herodata.useEmotion==null");
					return null;
				}
				value2 = useEmotionData.animParm;
			}
			else
			{
				bool flag7 = num < data.m_szDongZuo.Count;
				if (flag7)
				{
					value2 = data.m_szDongZuo[num].ToString();
				}
			}
			result = new ArrayList
			{
				Fun.EncodingUTF8(text),
				value,
				value2,
				data.m_nType,
				data.m_sIco
			};
		}
		return result;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000240BC File Offset: 0x000222BC
	public void ChangePinDao()
	{
		bool flag = !(ChatUICon.inst == null) && ChatUICon.inst.isOpenChat;
		if (flag)
		{
			ChatUICon.inst.ChangePinDao(false);
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x000240F8 File Offset: 0x000222F8
	public void OnPenChatUI()
	{
		bool isChangeGong = GuildManager.isChangeGong;
		if (!isChangeGong)
		{
			bool isChatPopTip = this.isChatPopTip;
			if (isChatPopTip)
			{
				this.isChatPopTip = false;
			}
			else
			{
				bool flag = (!(ChongZhiUICon.inst != null) || !ChongZhiUICon.inst.visible) && (!(playerreportCon.inst != null) || !playerreportCon.inst.OpenState) && (!(WenJuanUICon.inst != null) || !WenJuanUICon.inst.visible) && !(ChatUICon.inst == null);
				if (flag)
				{
					bool isOpenChat = ChatUICon.inst.isOpenChat;
					if (isOpenChat)
					{
						ChatUICon.inst.onGetFocus();
						ChatUICon.inst.Chat_SendMsgToServer();
					}
					else
					{
						ChatUICon.inst.OnOpenChat();
					}
				}
			}
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000241C8 File Offset: 0x000223C8
	public void OnShowOrHideChatWin(bool isShow)
	{
		bool flag = ChatUICon.inst != null;
		if (flag)
		{
			ChatUICon.inst.gameObject.SetActive(isShow);
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x000241F8 File Offset: 0x000223F8
	public void OnPenChatMiYu()
	{
		bool flag = !(ChatUICon.inst == null);
		if (flag)
		{
			ChatUICon.inst.OnMiYuOpen();
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00024228 File Offset: 0x00022428
	public void OnCloseChatUI(bool isss)
	{
		bool flag = !(ChatUICon.inst == null) && !isss;
		if (flag)
		{
			ChatUICon.inst.OnCloseChatUI();
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0002425C File Offset: 0x0002245C
	public void OnInitUserChat()
	{
		bool flag = !(ChatUICon.inst == null);
		if (flag)
		{
			ChatUICon.inst.OnChange();
		}
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002428C File Offset: 0x0002248C
	public void OnOpenHuDongWin()
	{
		bool flag = !ChatManager.ChatInput && fGameManager.Heroplayer != null && !(fGameManager.Heroplayer.wInput == null) && fGameManager.Heroplayer.playerCommon.m_nPlayerState != 0 && fGameManager.Heroplayer.wInput.focus() && (!(GameResultUICon.Inst != null) || !GameResultUICon.Inst.visible) && (!(gameuiCon.inst != null) || !gameuiCon.inst.mainRoundUI.mboolstat) && !fUIManager.GetInstance().GetResultModelScn() && !this.HuDongUIOpen;
		if (flag)
		{
			this.HuDongUIOpen = true;
			HeroPlayer.isMouseShow = true;
			fUIManager.GetInstance().ShowWin("HuDongUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), null, true);
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00024364 File Offset: 0x00022564
	public void OnCloseHuDongWin()
	{
		bool huDongUIOpen = this.HuDongUIOpen;
		if (huDongUIOpen)
		{
			this.HuDongUIOpen = false;
			this.OnChangeMouseShow();
			bool flag = HuDongUICon.inst != null;
			if (flag)
			{
				HuDongUICon.inst.OnCloseUI();
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000243A8 File Offset: 0x000225A8
	public void OnChangeMouseShow()
	{
		bool flag = (Setting_OptionCon.Instance == null || !Setting_OptionCon.Instance.visible) && fGameManager.Heroplayer != null;
		if (flag)
		{
			HeroPlayer.isMouseShow = false;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x000243E6 File Offset: 0x000225E6
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x000243EC File Offset: 0x000225EC
	private int GetType(string sName)
	{
		int result = 0;
		uint num = PrivateImplementationDetails.ComputeStringHash(sName);
		if (num <= 1172580663U)
		{
			if (num <= 132990436U)
			{
				if (num != 101563593U)
				{
					if (num == 132990436U)
					{
						if (sName == "nihaobtn")
						{
							result = 3;
						}
					}
				}
				else if (sName == "jihebtn")
				{
					result = 8;
				}
			}
			else if (num != 321360759U)
			{
				if (num == 1172580663U)
				{
					if (sName == "zhiliaobtn")
					{
						result = 5;
					}
				}
			}
			else if (sName == "biaoqingbtn")
			{
				result = 1;
			}
		}
		else if (num <= 2412441114U)
		{
			if (num != 2368724890U)
			{
				if (num == 2412441114U)
				{
					if (sName == "tanksbtn")
					{
						result = 6;
					}
				}
			}
			else if (sName == "penqibtn")
			{
				result = 9;
			}
		}
		else if (num != 2661567490U)
		{
			if (num != 3020973019U)
			{
				if (num == 3935698118U)
				{
					if (sName == "shoudaobtn")
					{
						result = 7;
					}
				}
			}
			else if (sName == "yuyinbtn")
			{
				result = 2;
			}
		}
		else if (sName == "skillbtn")
		{
			result = 4;
		}
		return result;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00024550 File Offset: 0x00022750
	public void OnHuDongOnKey()
	{
		int num = 0;
		bool flag = fGameManager.Heroplayer != null && !(fGameManager.Heroplayer.wInput == null) && fGameManager.Heroplayer.wInput.focus() && (!(GameResultUICon.Inst != null) || !GameResultUICon.Inst.visible) && (!(gameuiCon.inst != null) || !gameuiCon.inst.mainRoundUI.mboolstat) && !fUIManager.GetInstance().GetResultModelScn() && fGameManager.Heroplayer.playerCommon.m_nPlayerState != 0 && !ChatManager.ChatInput;
		if (flag)
		{
			bool flag2 = Input_Manager.Get_Button_Up("Interact_Thank");
			if (flag2)
			{
				num = 6;
			}
			else
			{
				bool flag3 = Input_Manager.Get_Button_Up("Interact_Expression");
				if (flag3)
				{
					num = 1;
				}
				else
				{
					bool flag4 = Input_Manager.Get_Button_Up("Interact_Roger");
					if (flag4)
					{
						num = 7;
					}
					else
					{
						bool flag5 = Input_Manager.Get_Button_Up("Interact_Hello");
						if (flag5)
						{
							num = 3;
						}
						else
						{
							bool flag6 = Input_Manager.Get_Button_Up("Interact_Need_Help");
							if (flag6)
							{
								num = 5;
							}
							else
							{
								bool flag7 = Input_Manager.Get_Button_Up("Interact_Ultimate_Skill_State");
								if (flag7)
								{
									num = 4;
								}
								else
								{
									bool flag8 = Input_Manager.Get_Button_Up("Interact_Collection");
									if (flag8)
									{
										num = 8;
									}
								}
							}
						}
					}
				}
			}
			bool flag9 = num != 0 && !ChatManager.ChatInput;
			if (flag9)
			{
				this.OnSendMsg(num);
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x000246AC File Offset: 0x000228AC
	public void OnMsgTip(string sname, int nType = 1, ArrayList szBtnState = null, int nCid = 0)
	{
		bool flag = nType == 1 || nType == 3;
		if (flag)
		{
			bool flag2 = szBtnState == null;
			if (flag2)
			{
				szBtnState = new ArrayList();
			}
			bool flag3 = GuildManager.mInst.m_nZhiWei == 0 || nType == 3;
			if (flag3)
			{
				szBtnState.Add(1);
			}
			bool flag4 = PlayerDataManager.getInstance().HUserData.OnIsFriend(sname);
			if (flag4)
			{
				szBtnState.Add(3);
			}
			else
			{
				szBtnState.Add(4);
			}
			bool flag5 = sname == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag5)
			{
				szBtnState.Add(0);
				szBtnState.Add(1);
				szBtnState.Add(3);
				szBtnState.Add(4);
				szBtnState.Add(5);
			}
			bool flag6 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
			if (flag6)
			{
				bool flag7 = nType == 1;
				if (flag7)
				{
					szBtnState.Add(5);
				}
				bool flag8 = nType == 3;
				if (flag8)
				{
					szBtnState.Add(8);
				}
			}
			else
			{
				bool flag9 = fGameManager.m_nUserType == 1;
				if (flag9)
				{
					szBtnState.Add(5);
				}
			}
		}
		bool flag10 = easy_tipCon.inst != null;
		if (flag10)
		{
			easy_tipCon.inst.Open();
			easy_tipCon.inst.InitName(sname, nType, szBtnState, nCid);
			fUIManager.GetInstance().sChatName = sname;
		}
		else
		{
			fUIManager.GetInstance().sChatName = sname;
			fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Tiploaduiok), new ArrayList
			{
				nType,
				szBtnState,
				nCid
			}, true);
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000248A0 File Offset: 0x00022AA0
	private void Tiploaduiok(string sUI, GameObject obj, object szParam)
	{
		ArrayList arrayList = (ArrayList)szParam;
		int nType = (int)arrayList[0];
		ArrayList szBtnState = (ArrayList)arrayList[1];
		int nCid = (int)arrayList[2];
		easy_tipCon.inst.InitName(fUIManager.GetInstance().sChatName, nType, szBtnState, nCid);
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x000248F8 File Offset: 0x00022AF8
	public void OnSetPingBiUser(string sUserName)
	{
		string text = string.Empty;
		bool flag = !this.isPingBiUser(sUserName);
		if (flag)
		{
			string sName = PlayerDataManager.getInstance().HUserData.m_sName;
			bool flag2 = PlayerPrefs.HasKey(sName);
			if (flag2)
			{
				text = PlayerPrefs.GetString(sName);
			}
			bool flag3 = text != string.Empty;
			if (flag3)
			{
				text += "&";
			}
			text += sUserName;
			PlayerPrefs.SetString(sName, text);
		}
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00024970 File Offset: 0x00022B70
	public void OnQuXiaoPingBi(string sUserName)
	{
		string text = string.Empty;
		bool flag = this.isPingBiUser(sUserName);
		if (flag)
		{
			string sName = PlayerDataManager.getInstance().HUserData.m_sName;
			text = PlayerPrefs.GetString(sName);
			string[] array = text.Split(new char[]
			{
				'&'
			});
			text = ((text.IndexOf(sUserName) != 0) ? text.Replace("&" + sUserName, string.Empty) : ((array.Length <= 1) ? string.Empty : text.Replace(sUserName + "&", string.Empty)));
			PlayerPrefs.SetString(sName, text);
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00024A08 File Offset: 0x00022C08
	public bool isPingBiUser(string sUserName)
	{
		string text = string.Empty;
		string sName = PlayerDataManager.getInstance().HUserData.m_sName;
		bool flag = PlayerPrefs.HasKey(sName);
		if (flag)
		{
			text = PlayerPrefs.GetString(sName);
			bool flag2 = text.IndexOf(sUserName) != -1;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00024A60 File Offset: 0x00022C60
	public void OnRadio(ArrayList szMsg)
	{
		string text = string.Empty;
		text = Fun.DecodingUTF8(szMsg[0].ToString());
		int num = (int)szMsg[1];
		bool flag = num == 0 || num == 1;
		if (flag)
		{
			BroadCastMSGCon.inst.AddMSG(text, num);
		}
		else
		{
			this.OnSendSystemMsg(text, "ffdd00", "system");
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00024AC8 File Offset: 0x00022CC8
	public void OnCloseServerRadio(ArrayList szData)
	{
		int sec = (int)szData[0];
		string lang = Language.GetLang(39035, new object[]
		{
			ChatManager.SecToFullTimeString(sec)
		});
		this.OnSendSystemMsg(lang, "ffdd00", "system");
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00024B10 File Offset: 0x00022D10
	public static string SecToFullTimeString(int sec)
	{
		string text = string.Empty;
		bool flag = sec <= 0;
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			int num = sec % 3600 / 60;
			int num2 = sec % 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				text = Language.GetLang(9006, new object[]
				{
					num.ToString()
				});
				bool flag3 = num2 > 0;
				if (flag3)
				{
					text += Language.GetLang(9007, new object[]
					{
						num2.ToString()
					});
				}
			}
			else
			{
				text = Language.GetLang(9007, new object[]
				{
					num2.ToString()
				});
			}
			result = text;
		}
		return result;
	}

	// Token: 0x0400036B RID: 875
	public static ChatManager mInst;

	// Token: 0x0400036C RID: 876
	public static bool IsShowZhunXing = true;

	// Token: 0x0400036D RID: 877
	public static bool IsHuDongHover = false;

	// Token: 0x0400036E RID: 878
	public static int m_nWorldMoney = 10;

	// Token: 0x0400036F RID: 879
	public static int m_nLaBaMoney = 10;

	// Token: 0x04000370 RID: 880
	public static string m_sLaBaMoney = "1m";

	// Token: 0x04000371 RID: 881
	public static List<GameObject> ShowLaBaList = new List<GameObject>();

	// Token: 0x04000372 RID: 882
	public static List<ArrayList> AllLaBaList = new List<ArrayList>();

	// Token: 0x04000373 RID: 883
	public static bool ChatInput = false;

	// Token: 0x04000374 RID: 884
	private bool _isChatPopTip;

	// Token: 0x04000375 RID: 885
	public bool HuDongUIOpen;
}

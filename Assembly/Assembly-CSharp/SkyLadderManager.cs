using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class SkyLadderManager
{
	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00172BB0 File Offset: 0x00170DB0
	// (set) Token: 0x06002A1C RID: 10780 RVA: 0x00172BC8 File Offset: 0x00170DC8
	public int isSkyMatching
	{
		get
		{
			return this.m_nSkyMatchOk;
		}
		set
		{
			this.m_nSkyMatchOk = value;
			bool flag = this.m_nSkyMatchOk == 1;
			if (flag)
			{
				this.matchingTime = Time.time;
			}
			else
			{
				this.matchingTime = 0f;
			}
		}
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x00172C08 File Offset: 0x00170E08
	public static SkyLadderManager GetInstance()
	{
		bool flag = SkyLadderManager.instance == null;
		if (flag)
		{
			SkyLadderManager.instance = new SkyLadderManager();
		}
		return SkyLadderManager.instance;
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x00172C38 File Offset: 0x00170E38
	public void OnSkyLadderMsg(int netid, ArrayList szNetMsg)
	{
		switch (netid)
		{
		case 16200:
			this.InitSkyLadderMatch(szNetMsg);
			break;
		case 16202:
			this.InitSkyLadderReward(szNetMsg);
			break;
		case 16203:
			this.GetSkyLadderReward(szNetMsg);
			break;
		case 16205:
			this.GetSkyLadderSeason(szNetMsg);
			break;
		case 16208:
			this.SetSkyRelink(szNetMsg);
			break;
		case 16210:
			this.ShowSkyScoreTip(szNetMsg);
			break;
		}
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x00172CC4 File Offset: 0x00170EC4
	private void ShowOpenTip()
	{
		bool flag = this.m_nSkyOpenTip <= 0 && this.m_nSkyLadderOpen == 2 && fGameManager.m_nUserType != 1 && fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(this.m_nSkyLadderActId);
		if (flag)
		{
			SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[this.m_nSkyLadderActId];
			DateTime serverDate = Fun.GetServerDate();
			string[] skyLadderOpenTime = this.GetSkyLadderOpenTime(serverDate);
			bool flag2 = !string.IsNullOrEmpty(skyLadderOpenTime[0]);
			if (flag2)
			{
				DateTime dateTime = DateTime.Parse(skyLadderOpenTime[0]);
				DateTime dateTime2 = DateTime.Parse(skyLadderOpenTime[1]);
				string content = (dateTime.DayOfWeek != DayOfWeek.Saturday) ? Language.GetLang(23038, new object[]
				{
					dateTime.ToString("HH:mm"),
					dateTime2.ToString("HH:mm")
				}) : Language.GetLang(23043, new object[0]);
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(23039, new object[0]), content, Language.GetLang(23040, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickOpen), null, null, null);
				fGameManager.LobbyPlayer.SendMsgToHallServer(16200, new ArrayList
				{
					16209,
					new ArrayList
					{
						1,
						0
					}
				});
				this.m_nSkyOpenTip = 1;
			}
		}
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x00172E4B File Offset: 0x0017104B
	private void OnClickOpen(object szParam)
	{
		fUIManager.GetInstance().ShowWin("SkyLadderUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadUIOk), null, true);
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x00172E70 File Offset: 0x00171070
	private void OnLoadUIOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetCurOpenUI(sUI);
		}
		bool flag2 = SkyLadderUICon.inst != null;
		if (flag2)
		{
			SkyLadderUICon.inst.ResetSkyLadderInfo(this.m_nSkyLadderActId);
		}
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x00172EC0 File Offset: 0x001710C0
	public void InitSkyLadderMatch(ArrayList szData)
	{
		bool flag = szData.Count > 0;
		if (flag)
		{
			int nSkyScore = this.m_nSkyScore;
			this.m_nSkyScore = (int)szData[0];
			this.m_nSkyLevel = (int)szData[1];
			this.m_nSkyCurMaxScore = (int)szData[2];
			this.m_nSkyCurMaxlvl = (int)szData[3];
			this.m_nSkyMatchnum = (int)szData[4];
			this.m_sSkyMatchtime = szData[5].ToString();
			this.m_nSkyMatchmap = (int)szData[6];
			this.m_szSkyMatchHero = (ArrayList)szData[7];
			this.m_nSkyPunishScore = (int)szData[8];
			this.m_sSkyPunishTime = szData[9].ToString();
			this.m_szSkyMatchResult = (ArrayList)szData[10];
			this.m_nSkySeasonId = (int)szData[11];
			this.m_nAddSkyScore = this.m_nSkyScore - nSkyScore;
			this.m_nSkyOpenTip = (int)szData[13];
		}
		bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag2)
		{
			MainUICon.inst.SetTiantiBtn();
		}
		this.m_nSkyLadderState = 0;
		this.szSkyLadderState.Clear();
		this.CheckRefreshTime();
		this.CheckTipTime();
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x00173030 File Offset: 0x00171230
	private void InitSkyLadderReward(ArrayList szData)
	{
		this.m_szSkyRewardInfo = (ArrayList)szData[0];
		this.m_szSkyLvlReward = (ArrayList)szData[1];
		this.m_nSkyEndReward = (int)szData[2];
		this.m_nSkyMaxScore = (int)szData[3];
		this.m_nSkyMaxlvl = (int)szData[4];
		int num = (int)szData[5];
		bool flag = this.m_szSkyRewardInfo.Count == 0;
		if (!flag)
		{
			int key = (int)this.m_szSkyRewardInfo[0];
			int num2 = (int)this.m_szSkyRewardInfo[1];
			int num3 = (int)this.m_szSkyRewardInfo[2];
			int key2 = (int)this.m_szSkyRewardInfo[3];
			bool flag2 = (num != -1 && num2 == num) || num2 == this.rewardSeasonId || !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(key);
			if (!flag2)
			{
				SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[key];
				bool flag3 = !skyLadderData.szSkyLadderReward.ContainsKey(key2);
				if (!flag3)
				{
					SkyLadderReward skyLadderReward = skyLadderData.szSkyLadderReward[key2];
					ArrayList arrayList = new ArrayList();
					bool flag4 = this.m_nSkyEndReward != 0 || this.isRewardTip;
					if (!flag4)
					{
						this.isRewardTip = true;
						for (int i = 0; i < skyLadderReward.szEndReward.Length; i++)
						{
							bool flag5 = !string.IsNullOrEmpty(skyLadderReward.szEndReward[i]);
							if (flag5)
							{
								string[] array = skyLadderReward.szEndReward[i].Split(new char[]
								{
									','
								});
								arrayList.Add(int.Parse(array[0]));
								arrayList.Add(int.Parse(array[1]));
							}
						}
						this.rewardSeasonId = num2;
						TipManager.GetInstance().ShowItemTip(Language.GetLang(23016, new object[0]), Language.GetLang(23017, new object[]
						{
							num2,
							skyLadderReward.sLvlname,
							num3
						}), (ArrayList)arrayList.Clone(), Language.GetLang(23007, new object[0]), new TipManager.TipOKDelegate(this.OnGetSkyReward), new ArrayList
						{
							0,
							-1
						});
					}
				}
			}
		}
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x001732BC File Offset: 0x001714BC
	public void OnGetSkyReward(object szParam)
	{
		ArrayList arrayList = (ArrayList)szParam;
		bool flag = (int)arrayList[0] != 1 || this.m_szSkyLvlReward.IndexOf((int)arrayList[1]) == -1;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(16200, new ArrayList
			{
				16203,
				arrayList
			});
			this.isRewardTip = false;
		}
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x0017333D File Offset: 0x0017153D
	public void GetSkyLadderReward(ArrayList szData)
	{
		this.m_szSkyRewardInfo = (ArrayList)szData[0];
		this.m_szSkyLvlReward = (ArrayList)szData[1];
		this.m_nSkyEndReward = (int)szData[2];
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x00173378 File Offset: 0x00171578
	public void GetSkyLadderSeason(ArrayList szData)
	{
		this.m_szSeasonList = (ArrayList)szData[1];
		bool flag = RankUICon.inst != null && RankUICon.inst.visible;
		if (flag)
		{
			RankUICon.inst.UpdateSeasonList();
		}
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x001733C4 File Offset: 0x001715C4
	public void OnSkyLadderMatch(int nXmlId, SkyLadderManager.MatchBtnDelegate func, object szParam)
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch > 0 || !TeamManager.GetInstance().LoadTeamOK;
		if (!flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().Dyaoqing.Count > 0;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag4 = TeamManager.GetInstance().m_nTeamID == 0 || (TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1);
					if (flag4)
					{
						int num = 0;
						int num2 = 0;
						int num3 = 0;
						SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[nXmlId];
						ArrayList arrayList = (ArrayList)TeamManager.GetInstance().MyTeamData.Clone();
						string sName = PlayerDataManager.getInstance().HUserData.m_sName;
						int num4 = PlayerDataManager.getInstance().HUserData.m_nLevel;
						string text = SkyLadderManager.GetInstance().m_sSkyPunishTime;
						bool flag5 = arrayList.Count > 0;
						if (flag5)
						{
							num2 = int.MinValue;
							num3 = int.MaxValue;
							for (int i = 0; i < arrayList.Count; i += 2)
							{
								ArrayList arrayList2 = (ArrayList)arrayList[i + 1];
								bool flag6 = (int)arrayList2[1] < num4;
								if (flag6)
								{
									num4 = (int)arrayList2[1];
								}
								ArrayList arrayList3 = (ArrayList)arrayList2[19];
								int num5 = (int)arrayList3[0];
								bool flag7 = num5 == 0;
								if (flag7)
								{
									num = 1;
									num5 = (int)arrayList2[16];
								}
								bool flag8 = num5 > num2;
								if (flag8)
								{
									num2 = num5;
								}
								bool flag9 = num5 < num3;
								if (flag9)
								{
									num3 = num5;
								}
								bool flag10 = string.IsNullOrEmpty(arrayList3[5].ToString());
								if (!flag10)
								{
									bool flag11 = string.IsNullOrEmpty(text);
									if (flag11)
									{
										text = arrayList3[5].ToString();
									}
									else
									{
										DateTime t = DateTime.Parse(text);
										DateTime t2 = DateTime.Parse(arrayList3[5].ToString());
										bool flag12 = t2 > t;
										if (flag12)
										{
											text = arrayList3[5].ToString();
										}
									}
								}
							}
						}
						bool flag13 = num == 0 && num2 - num3 > this.nSkyTeamDif;
						if (flag13)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(24027, new object[]
							{
								this.nSkyTeamDif
							}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							return;
						}
						bool flag14 = num4 < skyLadderData.nMinOpenLvl;
						if (flag14)
						{
							bool flag15 = TeamManager.GetInstance().m_nTeamID != 0;
							if (flag15)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(24013, new object[]
								{
									skyLadderData.nMinOpenLvl
								}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(24012, new object[]
								{
									skyLadderData.nMinOpenLvl
								}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							return;
						}
						bool flag16 = !string.IsNullOrEmpty(text);
						if (flag16)
						{
							DateTime serverDate = Fun.GetServerDate();
							DateTime dateTime = DateTime.Parse(text);
							bool flag17 = serverDate < dateTime;
							if (flag17)
							{
								bool flag18 = TeamManager.GetInstance().m_nTeamID != 0;
								if (flag18)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(23030, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
									return;
								}
								ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(2);
								DateTime t3 = DateTime.Parse(actDataByType.sEndTime);
								bool flag19 = dateTime >= t3;
								if (flag19)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(23032, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
									return;
								}
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(23031, new object[]
								{
									Fun.sencondsToHMS_CXP((int)(dateTime - serverDate).TotalSeconds)
								}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
								return;
							}
						}
					}
					this.matchBtnFunc = func;
					this.matchBtnParam = szParam;
					bool flag20 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
					if (flag20)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40024, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.SkyLadderMatch), null, null, null);
					}
					else
					{
						this.SkyLadderMatch(null);
					}
				}
			}
		}
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x00173A04 File Offset: 0x00171C04
	private void SkyLadderMatch(object szParam)
	{
		bool flag = this.matchBtnFunc != null;
		if (flag)
		{
			this.matchBtnFunc(this.matchBtnParam);
		}
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x00173A34 File Offset: 0x00171C34
	public void OnSkyLadderGameOver()
	{
		bool flag = this.isSkyLadderMatch;
		if (flag)
		{
			this.isSkyLadderOver = true;
			this.m_nSkyLadderState = 0;
			this.ShowSkyLadderTip();
		}
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x00173A64 File Offset: 0x00171C64
	public void OnSkyLadderExit(ArrayList szNetMsg)
	{
		this.m_nSkyLadderState = (int)szNetMsg[0];
		this.szSkyLadderState = (ArrayList)szNetMsg.Clone();
		this.ShowSkyLadderTip();
		bool flag = this.m_nSkyLadderState == 1;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(24019, new object[]
			{
				(int)this.szSkyLadderState[2]
			}), "ffdd00", "system");
			this.CheckForceTime();
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(24026, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
		}
	}

	// Token: 0x06002A2B RID: 10795 RVA: 0x00173B50 File Offset: 0x00171D50
	public void CheckForceTime()
	{
		bool flag = this.m_nSkyLadderState == 1 && this.szSkyLadderState.Count == 3 && Time.time - this.cutTime >= 1f;
		if (flag)
		{
			this.cutTime = Time.time;
			int num = (int)this.szSkyLadderState[2] - 1;
			bool flag2 = num < 0;
			if (flag2)
			{
				num = 0;
			}
			this.szSkyLadderState[2] = num;
		}
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x00173BD4 File Offset: 0x00171DD4
	public void ShowSkyLadderTip()
	{
		bool flag = !(gameuiCon.inst == null);
		if (flag)
		{
			switch (this.m_nSkyLadderState)
			{
			case 0:
				gameuiCon.inst.mainMsgUI.OnCloseExitMsg();
				break;
			case 1:
				gameuiCon.inst.mainMsgUI.OnCloseExitMsg();
				gameuiCon.inst.mainMsgUI.OnExitGameMsg(Language.GetLang(24023, new object[0]), 24024, (int)this.szSkyLadderState[2]);
				break;
			case 3:
				gameuiCon.inst.mainMsgUI.OnCloseExitMsg();
				gameuiCon.inst.mainMsgUI.OnExitGameMsg(Language.GetLang(24018, new object[0]), 24005, (int)this.szSkyLadderState[2]);
				break;
			case 4:
				gameuiCon.inst.mainMsgUI.OnCloseExitMsg();
				gameuiCon.inst.mainMsgUI.OnShowExitMsg(Language.GetLang(24021, new object[0]), Language.GetLang(24022, new object[0]));
				break;
			}
		}
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x00173D10 File Offset: 0x00171F10
	public void CheckRefreshTime()
	{
		bool flag = this.m_nSkyLadderOpen == 0 || fGameManager.LobbyPlayer == null;
		if (!flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			int num = 0;
			float num2 = 0f;
			bool flag2 = false;
			for (int i = 0; i < this.szSkyRankRefresh.Count; i++)
			{
				DateTime d = DateTime.Parse(serverDate.ToString("yyyy-MM-dd") + " " + this.szSkyRankRefresh[i]);
				num2 = (float)(serverDate - d).TotalSeconds;
				num = Mathf.CeilToInt(Mathf.Abs(num2));
				bool flag3 = num <= 10;
				if (flag3)
				{
					flag2 = true;
				}
				bool flag4 = num2 < -10f;
				if (flag4)
				{
					break;
				}
			}
			bool flag5 = flag2;
			if (flag5)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
				{
					17003,
					PlayerDataManager.getInstance().playerCommon.m_nCid,
					this.m_nSkySeasonId,
					1,
					100
				});
			}
			bool flag6 = num2 > 0f;
			if (flag6)
			{
				DateTime d2 = DateTime.Parse(serverDate.AddDays(1.0).ToString("yyyy-MM-dd") + " " + this.szSkyRankRefresh[0]);
				num = Mathf.CeilToInt((float)(d2 - serverDate).TotalSeconds);
			}
			bool flag7 = num > 10 && GameRoot.inst != null;
			if (flag7)
			{
				GameRoot.inst.StartSkyLadderRefresh(num);
			}
		}
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x00173EE0 File Offset: 0x001720E0
	public void CheckTipTime()
	{
		bool flag = this.m_nSkyLadderOpen == 0;
		if (!flag)
		{
			this.ShowOpenTip();
			bool flag2 = false;
			int num = 0;
			int num2 = 1;
			bool flag3 = !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(this.m_nSkyLadderActId);
			if (!flag3)
			{
				SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[this.m_nSkyLadderActId];
				DateTime serverDate = Fun.GetServerDate();
				string[] skyLadderOpenTime = this.GetSkyLadderOpenTime(serverDate);
				bool flag4 = string.IsNullOrEmpty(skyLadderOpenTime[0]) && string.IsNullOrEmpty(skyLadderOpenTime[4]);
				if (!flag4)
				{
					bool flag5 = !string.IsNullOrEmpty(skyLadderOpenTime[0]) || this.m_nSkyRelink == 1;
					if (flag5)
					{
						num2 = 2;
					}
					bool flag6 = num2 != this.m_nSkyLadderOpen;
					if (flag6)
					{
						bool flag7 = MainUICon.inst != null;
						if (flag7)
						{
							MainUICon.inst.SetTiantiBtn();
						}
					}
					else
					{
						bool flag8 = string.IsNullOrEmpty(skyLadderOpenTime[0]);
						DateTime dateTime;
						DateTime dateTime2;
						if (flag8)
						{
							dateTime = DateTime.Parse(skyLadderOpenTime[4]);
							dateTime2 = DateTime.Parse(skyLadderOpenTime[5]);
						}
						else
						{
							dateTime = DateTime.Parse(skyLadderOpenTime[0]);
							dateTime2 = DateTime.Parse(skyLadderOpenTime[1]);
						}
						int num3 = Mathf.CeilToInt((float)(serverDate - dateTime2).TotalSeconds);
						bool flag9 = serverDate < dateTime;
						if (flag9)
						{
							num = Mathf.CeilToInt((float)(dateTime - serverDate).TotalSeconds);
						}
						else
						{
							bool flag10 = serverDate >= dateTime && serverDate < dateTime2;
							if (flag10)
							{
								int num4 = Mathf.CeilToInt((float)(dateTime2 - dateTime).TotalSeconds);
								num4 = num4 / 1800 + 1;
								for (int i = 0; i < num4; i++)
								{
									DateTime d = dateTime.AddSeconds((double)(i * 30 * 60));
									float num5 = (float)(serverDate - d).TotalSeconds;
									bool flag11 = num5 <= 1f;
									if (flag11)
									{
										num = Mathf.CeilToInt(Mathf.Abs(num5));
										bool flag12 = num > 1;
										if (flag12)
										{
											break;
										}
										flag2 = true;
									}
								}
							}
							else
							{
								num = Mathf.CeilToInt((float)(dateTime - serverDate).TotalSeconds);
							}
						}
						bool flag13 = flag2 && num3 < 0;
						if (flag13)
						{
							string lang = Language.GetLang(23038, new object[]
							{
								dateTime.ToString("HH:mm"),
								dateTime2.ToString("HH:mm")
							});
							bool flag14 = dateTime.DayOfWeek == DayOfWeek.Saturday;
							if (flag14)
							{
								lang = Language.GetLang(23043, new object[0]);
							}
							bool flag15 = BroadCastMSGCon.inst != null;
							if (flag15)
							{
								BroadCastMSGCon.inst.AddMSG(lang, 1);
							}
						}
						bool flag16 = GameRoot.inst != null;
						if (flag16)
						{
							GameRoot.inst.CheckSkyLadderOpenTip(num);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002A2F RID: 10799 RVA: 0x001741DF File Offset: 0x001723DF
	public void ClearSkyMatchData()
	{
		this.isSkyLadderMatch = false;
		this.isSkyLadderOver = false;
		this.m_nSkyLadderState = 0;
		this.m_nSkyMatchOk = 0;
		this.szSkyLadderState.Clear();
	}

	// Token: 0x06002A30 RID: 10800 RVA: 0x0017420C File Offset: 0x0017240C
	public void SetSkyRelink(ArrayList szMsg)
	{
		this.m_nSkyRelink = (int)szMsg[0];
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetTiantiBtn();
		}
	}

	// Token: 0x06002A31 RID: 10801 RVA: 0x00174248 File Offset: 0x00172448
	public string[] GetSkyLadderOpenTime(DateTime dtime)
	{
		string[] array = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(2);
		bool flag = actDataByType == null;
		string[] result;
		if (flag)
		{
			result = array;
		}
		else
		{
			bool flag2 = !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(actDataByType.nXmlId);
			if (flag2)
			{
				result = array;
			}
			else
			{
				SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[actDataByType.nXmlId];
				int num = skyLadderData.szOpenTime.Length;
				int num2 = (int)dtime.DayOfWeek;
				bool flag3 = num2 == 0;
				if (flag3)
				{
					num2 = 7;
				}
				bool flag4 = num2 == 7 || num2 == 6;
				if (flag4)
				{
					DateTime t = DateTime.Parse(dtime.AddDays((double)(6 - num2)).ToString("yyyy-MM-dd"));
					DateTime t2 = DateTime.Parse(dtime.AddDays((double)(8 - num2)).ToString("yyyy-MM-dd"));
					array[0] = t.ToString("yyyy-MM-dd HH:mm:ss");
					array[1] = t2.ToString("yyyy-MM-dd HH:mm:ss");
					array[2] = t.AddDays(-1.0).ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num - 2];
					array[3] = t.AddDays(-1.0).ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num - 1];
					array[4] = t2.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[0];
					array[5] = t2.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[1];
					result = array;
				}
				else
				{
					int num3 = 0;
					DateTime t;
					DateTime t2;
					while (num3 < num && !string.IsNullOrEmpty(skyLadderData.szOpenTime[num3]))
					{
						t = DateTime.Parse(dtime.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num3]);
						t2 = DateTime.Parse(dtime.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num3 + 1]);
						bool flag5 = dtime >= t && dtime < t2;
						if (flag5)
						{
							array[0] = t.ToString("yyyy-MM-dd HH:mm:ss");
							array[1] = t2.ToString("yyyy-MM-dd HH:mm:ss");
						}
						bool flag6 = dtime < t;
						if (flag6)
						{
							array[4] = t.ToString("yyyy-MM-dd HH:mm:ss");
							array[5] = t2.ToString("yyyy-MM-dd HH:mm:ss");
							break;
						}
						array[2] = t.ToString("yyyy-MM-dd HH:mm:ss");
						array[3] = t2.ToString("yyyy-MM-dd HH:mm:ss");
						num3 += 2;
					}
					t = dtime.AddDays(-1.0);
					t2 = dtime.AddDays(1.0);
					bool flag7 = string.IsNullOrEmpty(array[2]);
					if (flag7)
					{
						bool flag8 = t.DayOfWeek == DayOfWeek.Sunday;
						if (flag8)
						{
							array[2] = t.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
						}
						else
						{
							array[2] = t.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num - 2];
						}
					}
					bool flag9 = string.IsNullOrEmpty(array[3]);
					if (flag9)
					{
						bool flag10 = t.DayOfWeek == DayOfWeek.Sunday;
						if (flag10)
						{
							array[3] = dtime.ToString("yyyy-MM-dd") + " 00:00:00";
						}
						else
						{
							array[3] = t.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[num - 1];
						}
					}
					bool flag11 = string.IsNullOrEmpty(array[4]);
					if (flag11)
					{
						num2 = (int)t2.DayOfWeek;
						bool flag12 = num2 == 6;
						if (flag12)
						{
							array[4] = t2.ToString("yyyy-MM-dd") + " 00:00:00";
						}
						else
						{
							array[4] = t2.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[0];
						}
					}
					bool flag13 = string.IsNullOrEmpty(array[5]);
					if (flag13)
					{
						num2 = (int)t2.DayOfWeek;
						bool flag14 = num2 == 6;
						if (flag14)
						{
							array[5] = t2.AddDays(2.0).ToString("yyyy-MM-dd") + " 00:00:00";
						}
						else
						{
							array[5] = t2.ToString("yyyy-MM-dd") + " " + skyLadderData.szOpenTime[1];
						}
					}
					result = array;
				}
			}
		}
		return result;
	}

	// Token: 0x06002A32 RID: 10802 RVA: 0x0017471C File Offset: 0x0017291C
	private void ShowSkyScoreTip(ArrayList szMsg)
	{
		string text = string.Empty;
		int num = (int)szMsg[0];
		string text2 = (string)szMsg[1];
		bool flag = num > 0;
		if (flag)
		{
			text = Language.GetLang(24028, new object[]
			{
				Mathf.Abs(num)
			});
		}
		bool flag2 = num < 0;
		if (flag2)
		{
			text = Language.GetLang(24029, new object[]
			{
				Mathf.Abs(num)
			});
		}
		bool flag3 = !string.IsNullOrEmpty(text);
		if (flag3)
		{
			ChatManager.GetInstance().OnSendSystemMsg(text, "ffdd00", "system");
		}
	}

	// Token: 0x040023A3 RID: 9123
	private static SkyLadderManager instance;

	// Token: 0x040023A4 RID: 9124
	public int m_nSkyScore;

	// Token: 0x040023A5 RID: 9125
	public int m_nSkyLevel;

	// Token: 0x040023A6 RID: 9126
	public int m_nSkyCurMaxScore;

	// Token: 0x040023A7 RID: 9127
	public int m_nSkyCurMaxlvl;

	// Token: 0x040023A8 RID: 9128
	public int m_nSkyWinLose;

	// Token: 0x040023A9 RID: 9129
	public int m_nSkyMatchnum;

	// Token: 0x040023AA RID: 9130
	public string m_sSkyMatchtime;

	// Token: 0x040023AB RID: 9131
	public int m_nSkyMatchmap;

	// Token: 0x040023AC RID: 9132
	public ArrayList m_szSkyMatchHero;

	// Token: 0x040023AD RID: 9133
	public int m_nSkyPunishScore;

	// Token: 0x040023AE RID: 9134
	public string m_sSkyPunishTime;

	// Token: 0x040023AF RID: 9135
	public ArrayList m_szSkyMatchResult = new ArrayList();

	// Token: 0x040023B0 RID: 9136
	public int m_nSkySeasonId;

	// Token: 0x040023B1 RID: 9137
	public int m_nSkyLadderActId;

	// Token: 0x040023B2 RID: 9138
	public int m_nSkyLadderOpen;

	// Token: 0x040023B3 RID: 9139
	public int m_nAddSkyScore;

	// Token: 0x040023B4 RID: 9140
	public int m_nSkyRelink;

	// Token: 0x040023B5 RID: 9141
	public int m_nSkyRewardId;

	// Token: 0x040023B6 RID: 9142
	public ArrayList m_szSkyLvlReward;

	// Token: 0x040023B7 RID: 9143
	public int m_nSkyEndReward;

	// Token: 0x040023B8 RID: 9144
	public ArrayList m_szSkyRewardInfo;

	// Token: 0x040023B9 RID: 9145
	private bool isRewardTip;

	// Token: 0x040023BA RID: 9146
	public int m_nSkyMaxScore;

	// Token: 0x040023BB RID: 9147
	public int m_nSkyMaxlvl;

	// Token: 0x040023BC RID: 9148
	public ArrayList m_szSeasonList = new ArrayList();

	// Token: 0x040023BD RID: 9149
	public bool isSkyLadderMatch;

	// Token: 0x040023BE RID: 9150
	public bool isSkyLadderOver;

	// Token: 0x040023BF RID: 9151
	public List<string> szSkyRankRefresh = new List<string>
	{
		"00:00:00",
		"04:00:00",
		"08:00:00",
		"12:00:00",
		"16:00:00",
		"20:00:00"
	};

	// Token: 0x040023C0 RID: 9152
	public int m_nSkyLadderState;

	// Token: 0x040023C1 RID: 9153
	private ArrayList szSkyLadderState = new ArrayList();

	// Token: 0x040023C2 RID: 9154
	public int m_nSkyOpenTip;

	// Token: 0x040023C3 RID: 9155
	private int rewardSeasonId;

	// Token: 0x040023C4 RID: 9156
	private float cutTime;

	// Token: 0x040023C5 RID: 9157
	private int m_nSkyMatchOk;

	// Token: 0x040023C6 RID: 9158
	private float matchingTime;

	// Token: 0x040023C7 RID: 9159
	private int nSkyTeamDif = 1000;

	// Token: 0x040023C8 RID: 9160
	private SkyLadderManager.MatchBtnDelegate matchBtnFunc;

	// Token: 0x040023C9 RID: 9161
	private object matchBtnParam;

	// Token: 0x02000674 RID: 1652
	// (Invoke) Token: 0x06003928 RID: 14632
	public delegate void MatchBtnDelegate(object szParam);
}

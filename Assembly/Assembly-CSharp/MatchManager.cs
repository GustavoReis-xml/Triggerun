using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class MatchManager
{
	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000F5B40 File Offset: 0x000F3D40
	// (set) Token: 0x060019F6 RID: 6646 RVA: 0x000F5BB3 File Offset: 0x000F3DB3
	public FbXmlData m_pFbData
	{
		get
		{
			bool flag = this._pFbData == null;
			if (flag)
			{
				this._pFbData = new FbXmlData();
				this._pFbData.sName = "bachang_baxi";
				this._pFbData.sTeamshowbg = "0";
				this._pFbData.uipos = new string[0];
				this._pFbData.m_szPveLevel = new ArrayList();
			}
			return this._pFbData;
		}
		set
		{
			this._pFbData = value;
		}
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x000F5BC0 File Offset: 0x000F3DC0
	public static MatchManager GetInstance()
	{
		bool flag = MatchManager.instance == null;
		if (flag)
		{
			MatchManager.instance = new MatchManager();
		}
		return MatchManager.instance;
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000F5BF0 File Offset: 0x000F3DF0
	public Dictionary<string, UserData> getAllUserData()
	{
		return this.m_szAllUserData;
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000F5C08 File Offset: 0x000F3E08
	public void UserJoinFbLog(int nState, string sLog)
	{
		this.OnSendMsgToServer(9002, new ArrayList
		{
			nState,
			sLog
		});
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000F5C40 File Offset: 0x000F3E40
	public void OnSelectHero(int nHeroID, int nIsOK)
	{
		this.OnSendMsgToServer(20014, new ArrayList
		{
			90002,
			nHeroID,
			nIsOK
		});
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x000F5C8C File Offset: 0x000F3E8C
	public void OnSendMsgToServer(int nNetID, ArrayList arr)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.Heroplayer.SendMessageToServer(nNetID, arr);
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000F5CB5 File Offset: 0x000F3EB5
	private void SendMsg()
	{
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x000F5CB8 File Offset: 0x000F3EB8
	private void OnCloseGame()
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.mInst.OnReturnHallOK(0, string.Empty, 0);
		}
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x000F5CE8 File Offset: 0x000F3EE8
	public void OnMatchMsg(ArrayList szNetMsg)
	{
		string str = "MATCH_DEBUG: OnMatchMsg received ID: ";
		object obj = szNetMsg[0];
		Debug.Log(str + ((obj != null) ? obj.ToString() : null));
		int num = (int)szNetMsg[0];
		int num2 = num;
		switch (num2)
		{
		case 80006:
			this.OnCloseGame();
			this.OnSetTime((ArrayList)szNetMsg[1]);
			fUISoundManager.instance.PlayUISound("match_ganme_searching", 0, null, 20);
			fUISoundManager.instance.PlayUISound("match_game_findin", 0, null, 20);
			fUIManager.GetInstance().ShowWin("MatchUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), null, true);
			MainUICon.inst.SetMatchState();
			break;
		case 80007:
		{
			int nIsMatch = this.m_nIsMatch;
			this.m_nIsMatch = 0;
			this.m_nMatchFbType = 0;
			MatchManager.GetInstance().IsMatchState = 200;
			fUISoundManager.instance.StopSound("match_ganme_searching", 0);
			bool flag = MatchUICon.inst != null;
			if (flag)
			{
				MatchUICon.inst.Close();
			}
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				int num3 = 0;
				bool flag3 = nIsMatch == 2 || nIsMatch == 4;
				if (flag3)
				{
					ArrayList arrayList = (ArrayList)szNetMsg[1];
					arrayList = (ArrayList)arrayList[1];
					num3 = (int)arrayList[1];
				}
				bool flag4 = num3 == 0;
				if (flag4)
				{
					MainUICon.inst.SetMatchState();
				}
				bool flag5 = nIsMatch == 2;
				if (flag5)
				{
					SkyLadderManager.GetInstance().isSkyMatching = num3;
				}
				bool flag6 = nIsMatch == 4;
				if (flag6)
				{
					DtsBatteManager.GetInstance().isDtsMatching = num3;
				}
			}
			break;
		}
		case 80008:
		case 80009:
		case 80010:
		case 80014:
		case 80015:
			break;
		case 80011:
		{
			bool flag7 = MatchUICon.inst != null;
			if (flag7)
			{
				MatchUICon.inst.OnUpDataMatchTeamNum((ArrayList)szNetMsg[1]);
			}
			break;
		}
		case 80012:
			MatchManager.GetInstance().OnSetMatchModelNum((ArrayList)szNetMsg[1]);
			break;
		case 80013:
			this.OnMatchOkMsg((ArrayList)szNetMsg[1]);
			break;
		case 80016:
		{
			ArrayList arrayList2 = (ArrayList)szNetMsg[1];
			this.isEnterFb = ((int)arrayList2[0] >= 0);
			break;
		}
		default:
			if (num2 != 80021)
			{
				switch (num2)
				{
				case 90001:
				{
					this.m_nJoinState = 2;
					this.SetIsSingleState((int)szNetMsg[2]);
					fUISoundManager.instance.StopSound("match_ganme_searching", 0);
					fUISoundManager.instance.PlayUISound("match_game_findout", 0, null, 20);
					this.SetUserList((ArrayList)szNetMsg[1]);
					this.SetFbInfo((int)szNetMsg[2], (int)szNetMsg[3], (string)szNetMsg[4], (int)szNetMsg[5], (int)szNetMsg[6], (string)szNetMsg[7], (int)szNetMsg[8], (int)szNetMsg[9]);
					MatchManager.GetInstance().LoadScene(fGameManager.GetInstance().mloadName);
					bool flag8 = this.m_nMatchType == 4;
					if (flag8)
					{
						DtsBatteManager.GetInstance().OnEnterScn();
					}
					else
					{
						roleuiManager.GetInstance().OpenSelectHeroUI(1);
					}
					SkyLadderManager.GetInstance().isSkyMatching = 0;
					this.m_nJoinState = 3;
					break;
				}
				case 90002:
					this.OnChangeUserData(szNetMsg);
					break;
				case 90004:
					this.OnShowLoad();
					break;
				case 90005:
					this.UpDataGameData((ArrayList)szNetMsg[1]);
					break;
				case 90006:
					this.SetUserList((ArrayList)szNetMsg[1]);
					break;
				case 90007:
					this.OnUserLeave((string)szNetMsg[1]);
					break;
				case 90008:
				{
					bool flag9 = (int)szNetMsg[2] == 1;
					if (flag9)
					{
						MatchManager.GetInstance().OnEnterGame();
						SelectHeroUICon.inst.Close();
					}
					break;
				}
				case 90009:
					this.OnShowLoad((ArrayList)szNetMsg[1]);
					break;
				}
			}
			else
			{
				this.OnMatchCheckTip((ArrayList)szNetMsg[1]);
			}
			break;
		}
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000F6168 File Offset: 0x000F4368
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
		bool flag = MatchUICon.inst != null;
		if (flag)
		{
			MatchUICon.inst.Init();
		}
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x000F6194 File Offset: 0x000F4394
	private void OnSetTime(ArrayList arr)
	{
		ArrayList arrayList = new ArrayList();
		this.m_nIsMatch = (int)arr[0];
		arrayList = (ArrayList)arr[1];
		bool flag = (int)arrayList[1] == 0 || (int)arrayList[2] == 0;
		if (flag)
		{
			this.m_nYjTime = -1;
		}
		else
		{
			this.m_nYjTime = (int)arrayList[1] / (int)arrayList[2];
			this.m_nMatchNum = (int)arrayList[3];
		}
		this.nNowNum = (int)arrayList[4];
		this.nMaxNum = (int)arrayList[5];
		this.m_szAllModelNum = (ArrayList)arrayList[6];
		this.m_nMatchFbType = (int)arrayList[8];
		bool flag2 = MatchModelCon.inst != null;
		if (flag2)
		{
			MatchModelCon.inst.SetMatchModelUI();
		}
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x000F6290 File Offset: 0x000F4490
	public int GetYjTime()
	{
		bool flag = this.m_nYjTime == -1;
		int result;
		if (flag)
		{
			result = this.m_nYjTime;
		}
		else
		{
			bool flag2 = this.m_nYjTime / 60 < 5 && this.m_nMatchNum < 10;
			if (flag2)
			{
				result = -1;
			}
			else
			{
				result = this.m_nYjTime;
			}
		}
		return result;
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x000F62E4 File Offset: 0x000F44E4
	private void SetFbInfo(int fbid, int fbtype, string fbname, int nTime, int nIsChangeHero, string starttime, int nMatchtype, int nTeamVoice)
	{
		this.m_nScnID = fbid;
		this.m_nMapType = fbtype;
		this.m_sMapName = fbname;
		this.m_nSelectTime = nTime;
		this.m_nIsChangeHero = nIsChangeHero;
		this.m_sGameStartTime = starttime;
		this.m_dGameStartTime = DateTime.Parse(MatchManager.GetInstance().m_sGameStartTime);
		fGameManager.GetInstance().nNowScnID = fbid;
		fGameManager.GetInstance().mloadName = this.m_sMapName;
		this.m_nMatchType = nMatchtype;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(fbid);
		if (flag)
		{
			this.m_pFbData = fGameManager.mloadConfig.FbData[fbid];
		}
		fGameManager.IsJoinGame = false;
		bool flag2 = this.m_nMatchType == 2;
		if (flag2)
		{
			SkyLadderManager.GetInstance().isSkyLadderMatch = true;
		}
		else
		{
			SkyLadderManager.GetInstance().isSkyLadderMatch = false;
		}
		SkyLadderManager.GetInstance().isSkyLadderOver = false;
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x000F63BE File Offset: 0x000F45BE
	private void MatchMsgToServer(ArrayList arr)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(60100, arr);
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x000F63D2 File Offset: 0x000F45D2
	private void UpDataGameData(ArrayList arr)
	{
		this.m_nIsChangeHero = (int)arr[0];
		this.m_sGameStartTime = (string)arr[1];
		this.m_dGameStartTime = DateTime.Parse(MatchManager.GetInstance().m_sGameStartTime);
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x000F6410 File Offset: 0x000F4610
	public void SetUserList(ArrayList szList)
	{
		for (int i = 0; i < szList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(szList[i].ToString());
			if (flag)
			{
				szList[i] = Fun.DecodingUTF8(szList[i].ToString());
				ArrayList arrayList = (ArrayList)szList[i + 1];
				arrayList[0] = Fun.DecodingUTF8(arrayList[0].ToString());
			}
		}
		this.m_szUserList = (ArrayList)szList.Clone();
		this.CreateUserData();
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x000F64A8 File Offset: 0x000F46A8
	private void CreateUserData()
	{
		int i = 0;
		while (i < this.m_szUserList.Count)
		{
			bool flag = (string)this.m_szUserList[i] != string.Empty;
			if (flag)
			{
				bool flag2 = this.m_szAllUserData.ContainsKey((string)this.m_szUserList[i]);
				if (flag2)
				{
					this.OnUpUserDate((string)this.m_szUserList[i], (ArrayList)this.m_szUserList[i + 1]);
				}
				else
				{
					UserData userData = new UserData();
					userData.InitUserData((ArrayList)this.m_szUserList[i + 1]);
					this.m_szAllUserData.Add((string)this.m_szUserList[i], userData);
				}
			}
			IL_BA:
			i += 2;
			continue;
			goto IL_BA;
		}
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x000F658C File Offset: 0x000F478C
	public void OnUserLeave(string sNick)
	{
		sNick = Fun.DecodingUTF8(sNick);
		int num = this.m_szUserList.IndexOf(sNick);
		bool flag = num != -1;
		if (flag)
		{
			this.m_szUserList[num] = string.Empty;
			this.m_szUserList[num + 1] = string.Empty;
		}
		bool flag2 = this.m_szAllUserData.ContainsKey(sNick);
		if (flag2)
		{
			this.m_szAllUserData.Remove(sNick);
		}
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000F6604 File Offset: 0x000F4804
	private void OnChangeUserData(ArrayList szNetMsg)
	{
		bool flag = szNetMsg.Count >= 1;
		if (flag)
		{
			string text = Fun.DecodingUTF8(szNetMsg[1].ToString());
			ArrayList arrayList = (ArrayList)szNetMsg[2];
			arrayList[0] = Fun.DecodingUTF8(arrayList[0].ToString());
			this.SetUserList(text, arrayList);
			bool flag2 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
			if (flag2)
			{
				roleuiManager.GetInstance().OnOpenHeroUI(text);
			}
			bool isSingle = fGameManager.IsSingle;
			if (isSingle)
			{
				this.OnEnterGame();
			}
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x000F66A8 File Offset: 0x000F48A8
	public void SetUserList(string uname, ArrayList szUser)
	{
		int num = this.m_szUserList.IndexOf(uname);
		bool flag = num != -1;
		if (flag)
		{
			this.m_szUserList[num + 1] = szUser;
			this.OnUpUserDate(uname, szUser);
		}
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x000F66EC File Offset: 0x000F48EC
	private void OnUpUserDate(string sNick, ArrayList arr)
	{
		bool flag = this.m_szAllUserData.ContainsKey(sNick);
		if (flag)
		{
			UserData userData = this.m_szAllUserData[sNick];
			userData.InitUserData(arr);
		}
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x000F6724 File Offset: 0x000F4924
	public ArrayList GetUserList()
	{
		return this.m_szUserList;
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000F673C File Offset: 0x000F493C
	public ArrayList GetAllUserData()
	{
		return new ArrayList(this.m_szAllUserData.Keys);
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x000F6760 File Offset: 0x000F4960
	public UserData GetUserData(string sName)
	{
		bool flag = sName == null;
		UserData result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.m_szAllUserData.ContainsKey(sName);
			if (flag2)
			{
				result = this.m_szAllUserData[sName];
			}
			else
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x000F67A0 File Offset: 0x000F49A0
	public void ClearMatchUserData()
	{
		this.m_szUserList.Clear();
		this.m_szAllUserData.Clear();
		bool flag = MatchUICon.inst != null;
		if (flag)
		{
			this.m_nIsMatch = 0;
			this.m_nMatchFbType = 0;
			MatchManager.GetInstance().IsMatchState = 300;
			MatchUICon.inst.Close();
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.SetMatchState();
			}
		}
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000F681C File Offset: 0x000F4A1C
	public void OnShowLoad()
	{
		Debug.Log("MATCH_DEBUG: OnShowLoad triggered");
		fUIManager.mUILoading.SetRawImg(this.m_nScnID.ToString(), this.m_sMapName);
		fUIManager.SetProgreessShow(true);
		fGameManager.m_nJoinGame = "确认选择英雄";
		this.GetMeTeamMode();
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x000F686C File Offset: 0x000F4A6C
	private void OnShowLoad(ArrayList szData)
	{
		this.m_nScnID = (int)szData[0];
		this.m_sMapName = (string)szData[1];
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(this.m_nScnID);
		if (flag)
		{
			this.m_pFbData = fGameManager.mloadConfig.FbData[this.m_nScnID];
		}
		this.m_nMapType = this.m_pFbData.nType;
		fGameManager.GetInstance().nNowScnID = this.m_nScnID;
		fGameManager.GetInstance().mloadName = this.m_sMapName;
		fGameManager.IsJoinGame = false;
		fUIManager.mUILoading.SetRawImg(this.m_nScnID.ToString(), this.m_sMapName);
		fUIManager.SetProgreessShow(true);
		fGameManager.m_nJoinGame = "确认选择英雄";
		this.GetMeTeamMode();
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x000F6940 File Offset: 0x000F4B40
	public void GetMeTeamMode()
	{
		Debug.Log("MATCH_DEBUG: GetMeTeamMode called");
		int num = 1;
		UserData userData = this.GetUserData(PlayerDataManager.getInstance().playercom.m_sName);
		bool flag = userData != null;
		if (flag)
		{
			num = userData.m_nShili;
		}
		bool flag2 = this.m_pFbData != null;
		if (flag2)
		{
			fUISoundManager.instance.PlayUISound(this.m_pFbData.loadsound, 0, new fUISoundManager.UISoundStopFun(this.SoundStop), 10);
		}
		foreach (UserData userData2 in this.m_szAllUserData.Values)
		{
			bool flag3 = userData2 == null || userData2.m_nShili != num;
			if (!flag3)
			{
				int nHeroID = userData2.m_nHeroID;
				bool flag4 = !WebParam.client_resurl_force_local && this.m_nMatchType != 4;
				if (!flag4)
				{
					this.m_nJoinState = 5;
					bool nPreLoadOff = fGameManager.m_nPreLoadOff;
					if (nPreLoadOff)
					{
						fGameManager.GetInstance().loadScene(this.m_sMapName, 11, this.m_nScnID);
					}
					return;
				}
				bool flag5 = nHeroID != 0 && userData2.m_sNick == PlayerDataManager.getInstance().playercom.m_sName;
				if (flag5)
				{
					int useEquipId = userData2.GetUseEquipId(1, 1);
					string fpsModel = fGameManager.mloadConfig.AllHeroUnlock[nHeroID].szSkinUnlock[useEquipId].fpsModel;
					string tpsModel = fGameManager.mloadConfig.AllHeroUnlock[nHeroID].szSkinUnlock[useEquipId].tpsModel;
					string fpsControl = fGameManager.mloadConfig.AllHeroUnlock[nHeroID].szSkinUnlock[useEquipId].fpsControl;
					string tpsControl = fGameManager.mloadConfig.AllHeroUnlock[nHeroID].szSkinUnlock[useEquipId].tpsControl;
					this.ModelArr.Add(GameRoot.mPreLoadManager.GetResPath(3) + fpsModel + ".mo");
					this.ModelArr.Add(GameRoot.mPreLoadManager.GetResPath(3) + tpsModel + ".mo");
					this.ModelArr.Add(GameRoot.mPreLoadManager.GetResPath(3) + fpsControl + ".mo");
					this.ModelArr.Add(GameRoot.mPreLoadManager.GetResPath(3) + tpsControl + ".mo");
					this.FPSModelArr.Add(fpsModel + ".mo");
					this.FPSModelArr.Add(tpsModel + ".mo");
					this.FPSModelArr.Add(fpsControl + ".mo");
					this.FPSModelArr.Add(tpsControl + ".mo");
					this.StarUI("gameui.ab");
				}
			}
		}
		this.StartLoadModel();
	}

	// Token: 0x06001A12 RID: 6674 RVA: 0x000F6C50 File Offset: 0x000F4E50
	private void StarUI(string sUIname)
	{
		string url = "UI/" + sUIname;
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.DownLoadOK), sUIname);
		}
		else
		{
			this.DownLoadOK(resWWW, sUIname);
		}
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x000F6C9C File Offset: 0x000F4E9C
	public void SoundStop()
	{
		bool flag = !fUIManager.IsMapOK;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound("music_commen", 1, null, 20);
		}
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000F6CCC File Offset: 0x000F4ECC
	private void DownLoadOK(WWW www, object obj)
	{
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x000F6CD0 File Offset: 0x000F4ED0
	private void StartLoadModel()
	{
		Debug.Log("MATCH_DEBUG: StartLoadModel called, count: " + this.FPSModelArr.Count.ToString());
		bool flag = this.FPSModelArr.Count > 0;
		if (flag)
		{
			string url = "HeroAnimator/" + (string)this.FPSModelArr[0];
			fUIManager.mUILoading.SetModelArr("HeroAnimator/" + (string)this.FPSModelArr[0]);
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.DownLoadModel), (string)this.FPSModelArr[0]);
			}
			else
			{
				this.DownLoadModel(resWWW, (string)this.FPSModelArr[0]);
			}
		}
		else
		{
			this.m_nJoinState = 5;
			fUISoundManager.instance.StopSound("match_ganme_searching", 0);
			bool nPreLoadOff = fGameManager.m_nPreLoadOff;
			if (nPreLoadOff)
			{
				fUIManager.mUILoading.LoadScn();
				fGameManager.GetInstance().loadScene(this.m_sMapName, 11, this.m_nScnID);
			}
		}
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000F6DFC File Offset: 0x000F4FFC
	public void DownLoadModel(WWW www, object obj)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			bool flag2 = this.FPSModelArr.Count > 0;
			if (flag2)
			{
				this.FPSModelArr.RemoveAt(0);
			}
			fUIManager.mUILoading.SetModelArr(string.Empty);
			bool flag3 = this.FPSModelArr.Count > 0;
			if (flag3)
			{
				this.StartLoadModel();
			}
			else
			{
				this.m_nJoinState = 5;
				bool nPreLoadOff = fGameManager.m_nPreLoadOff;
				if (nPreLoadOff)
				{
					fUIManager.mUILoading.LoadScn();
					fGameManager.GetInstance().loadScene(this.m_sMapName, 11, this.m_nScnID);
				}
			}
		}
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000F6EA8 File Offset: 0x000F50A8
	public void LoadScene(string sMapName)
	{
		string text = "scene/" + sMapName + ".map";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.DownLoadMapOK), text);
		}
		else
		{
			this.DownLoadMapOK(resWWW, text);
		}
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x000F6EFC File Offset: 0x000F50FC
	public void DownLoadMapOK(WWW www, object obj)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			bool flag2 = www != null;
			if (flag2)
			{
				AssetBundle assetBundle = www.assetBundle;
			}
			fGameManager.GetInstance().mPreLoadMap = "OK";
			this.m_nJoinState = 4;
		}
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x000F6F46 File Offset: 0x000F5146
	public void OnCheckModel(string sModelName)
	{
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x000F6F4C File Offset: 0x000F514C
	public void OnEnterGame()
	{
		Debug.Log("MATCH_DEBUG: OnEnterGame called, IsJoinGame: " + fGameManager.IsJoinGame.ToString());
		bool flag = !fGameManager.IsJoinGame;
		if (flag)
		{
			fGameManager.IsJoinGame = true;
			HeroPlayer.isPlayerControl = true;
			HeroPlayer.isMouseShow = (SelectHeroUICon.inst != null);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(90004);
			arrayList.Add(0);
			MatchManager.GetInstance().OnSendMsgToServer(20014, arrayList);
		}
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x000F6FD4 File Offset: 0x000F51D4
	public void OnCancelMatch()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(80003);
		arrayList.Add(this.m_nIsMatch);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		arrayList.Add(1);
		arrayList.Add(TeamManager.GetInstance().m_nCaptain);
		MatchManager.GetInstance().IsMatchState = 400;
		fGameManager.SendMsgToHallServer(80001, arrayList);
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x000F7064 File Offset: 0x000F5264
	private void SetIsSingleState(int nFbID)
	{
		fGameManager.IsSingle = (nFbID == 5205);
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			this.OnSelectHero(1015, 1);
			PlayerLearnManager.GetInstance();
			fUIManager.GetInstance().ShowWin("XinShouJiaoXue", NetMsg.UITRANSFROM.GameUI, null, null, true);
			bool flag = !fGameManager.IsHadLearn;
			if (flag)
			{
				fGameManager.IsNeedShowPiPeiYinDao = true;
			}
		}
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x000F70C8 File Offset: 0x000F52C8
	public void OnGetMatchModelNum()
	{
		fGameManager.SendMsgToHallServer(80001, new ArrayList
		{
			80012,
			1
		});
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x000F7108 File Offset: 0x000F5308
	public void OnSetMatchModelNum(ArrayList arr)
	{
		this.m_szAllModelNum = arr;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OpenMatchModelUI();
		}
		bool flag2 = MatchModelCon.inst != null;
		if (flag2)
		{
			MatchModelCon.inst.SetMatchModelUI();
		}
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x000F7158 File Offset: 0x000F5358
	public void OnEnterTrain()
	{
		bool flag = Time.time - this.enterTrain >= 2f;
		if (flag)
		{
			bool flag2 = TeamManager.GetInstance().Dyaoqing.Count > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5020, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterTraining), null, null, null);
				}
				else
				{
					bool flag4 = this.m_nIsMatch > 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5021, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterTraining), null, null, null);
					}
					else
					{
						bool flag5 = TeamManager.GetInstance().m_nTeamID != 0;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5022, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterTraining), null, null, null);
						}
						else
						{
							bool flag6 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
							if (flag6)
							{
								TeamManager.GetInstance().LeaveLookingTeam(1, 0, 0, 0);
							}
							else
							{
								this.OnEnterTraining(null);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x000F7390 File Offset: 0x000F5590
	public void OnEnterTraining(object szParam)
	{
		bool flag = fGameManager.LobbyPlayer != null && TeamManager.GetInstance().LoadTeamOK && !this.CheckEnterFbIsLock();
		if (flag)
		{
			this.AddEnterFbLock();
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				RoomManage.GetInstance().OnLeave();
			}
			bool flag3 = this.m_nIsMatch > 0;
			if (flag3)
			{
				MatchManager.GetInstance().OnCancelMatch();
			}
			bool flag4 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag4)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			this.enterTrain = Time.time;
			fUIManager.GetInstance().SetWinActive("EnterJiaoXue", false);
			fGameManager.LobbyPlayer.SendMsgToHallServer(5030, new ArrayList());
		}
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x000F745C File Offset: 0x000F565C
	public void OnEnterLearnFb()
	{
		bool flag = Time.time - this.enterlearnfb >= 2f;
		if (flag)
		{
			bool flag2 = TeamManager.GetInstance().Dyaoqing.Count > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5033, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterLearnFb), null, null, null);
				}
				else
				{
					bool flag4 = this.m_nIsMatch > 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5034, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterLearnFb), null, null, null);
					}
					else
					{
						bool flag5 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
						if (flag5)
						{
							TeamManager.GetInstance().LeaveLookingTeam(2, 0, 0, 0);
						}
						else
						{
							bool flag6 = TeamManager.GetInstance().m_nTeamID != 0;
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(5035, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEnterLearnFb), null, null, null);
							}
							else
							{
								this.OnEnterLearnFb(null);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x000F7698 File Offset: 0x000F5898
	public void OnEnterLearnFb(object szParam)
	{
		bool flag = fGameManager.LobbyPlayer != null && TeamManager.GetInstance().LoadTeamOK && !this.CheckEnterFbIsLock();
		if (flag)
		{
			this.AddEnterFbLock();
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				RoomManage.GetInstance().OnLeave();
			}
			bool flag3 = this.m_nIsMatch > 0;
			if (flag3)
			{
				MatchManager.GetInstance().OnCancelMatch();
			}
			bool flag4 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag4)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			this.enterlearnfb = Time.time;
			fUIManager.GetInstance().SetWinActive("EnterJiaoXue", false);
			fGameManager.LobbyPlayer.SendMsgToHallServer(5032, new ArrayList());
		}
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x000F7764 File Offset: 0x000F5964
	private bool CheckEnterFbIsLock()
	{
		bool flag = !this.entertrainorlearnlock;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = Time.time - this.entertrainorlearntime > 2f;
			if (flag2)
			{
				this.entertrainorlearnlock = false;
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000F77AC File Offset: 0x000F59AC
	private void AddEnterFbLock()
	{
		this.entertrainorlearnlock = true;
		this.entertrainorlearntime = Time.time;
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x000F77C4 File Offset: 0x000F59C4
	private void OnMatchOkMsg(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		bool flag = num == 2;
		if (flag)
		{
		}
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000F77EC File Offset: 0x000F59EC
	private void OnSkyLadderLoad(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = SkyLadderLoadUICon.inst != null;
		if (flag)
		{
			SkyLadderLoadUICon.inst.ShowUserInfo((ArrayList)szParam);
		}
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000F781C File Offset: 0x000F5A1C
	private void OnMatchCheckTip(ArrayList szMsg)
	{
		this.m_nIsMatch = 0;
		this.m_nMatchFbType = 0;
		this.IsMatchState = 200;
		fUISoundManager.instance.StopSound("match_ganme_searching", 0);
		bool flag = MatchUICon.inst != null;
		if (flag)
		{
			MatchUICon.inst.Close();
		}
		bool flag2 = MainUICon.inst != null;
		if (flag2)
		{
			MainUICon.inst.SetMatchState();
		}
		int num = (int)szMsg[1];
		int num2 = num;
		if (num2 != 1)
		{
			if (num2 == 2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17020, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17019, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x0400148B RID: 5259
	private static MatchManager instance;

	// Token: 0x0400148C RID: 5260
	public const int MATCH_TYPE_TRAIN = 0;

	// Token: 0x0400148D RID: 5261
	public const int MATCH_TYPE_FAST = 1;

	// Token: 0x0400148E RID: 5262
	public const int MATCH_TYPE_SKYLADDER = 2;

	// Token: 0x0400148F RID: 5263
	public const int MATCH_TYPE_ROOM = 5;

	// Token: 0x04001490 RID: 5264
	public ArrayList MyTeamData = new ArrayList();

	// Token: 0x04001491 RID: 5265
	public int m_nIsMatch;

	// Token: 0x04001492 RID: 5266
	public int m_nMatchFbType;

	// Token: 0x04001493 RID: 5267
	public int IsMatchState;

	// Token: 0x04001494 RID: 5268
	private ArrayList m_szUserList = new ArrayList();

	// Token: 0x04001495 RID: 5269
	private Dictionary<string, UserData> m_szAllUserData = new Dictionary<string, UserData>();

	// Token: 0x04001496 RID: 5270
	public int m_nScnID;

	// Token: 0x04001497 RID: 5271
	public int m_nMapType;

	// Token: 0x04001498 RID: 5272
	public string m_sMapName;

	// Token: 0x04001499 RID: 5273
	public int m_nMatchType = -1;

	// Token: 0x0400149A RID: 5274
	public int m_nGameVoice;

	// Token: 0x0400149B RID: 5275
	private FbXmlData _pFbData;

	// Token: 0x0400149C RID: 5276
	public int m_nSelectTime;

	// Token: 0x0400149D RID: 5277
	public int m_nIsChangeHero;

	// Token: 0x0400149E RID: 5278
	public string m_sGameStartTime;

	// Token: 0x0400149F RID: 5279
	public DateTime m_dGameStartTime;

	// Token: 0x040014A0 RID: 5280
	public int m_nIsJoinGame;

	// Token: 0x040014A1 RID: 5281
	private ArrayList ModelArr = new ArrayList();

	// Token: 0x040014A2 RID: 5282
	private ArrayList FPSModelArr = new ArrayList();

	// Token: 0x040014A3 RID: 5283
	public ArrayList m_szAllModelNum = new ArrayList();

	// Token: 0x040014A4 RID: 5284
	private int m_nYjTime;

	// Token: 0x040014A5 RID: 5285
	private int m_nMatchNum;

	// Token: 0x040014A6 RID: 5286
	public int nNowNum;

	// Token: 0x040014A7 RID: 5287
	public int nMaxNum;

	// Token: 0x040014A8 RID: 5288
	public int m_nJoinState;

	// Token: 0x040014A9 RID: 5289
	public bool isEnterFb;

	// Token: 0x040014AA RID: 5290
	public bool entertrainorlearnlock;

	// Token: 0x040014AB RID: 5291
	public float entertrainorlearntime;

	// Token: 0x040014AC RID: 5292
	private float enterTrain;

	// Token: 0x040014AD RID: 5293
	private float enterlearnfb;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class DtsBatteManager
{
	// Token: 0x17000198 RID: 408
	// (get) Token: 0x060008FC RID: 2300 RVA: 0x0004C554 File Offset: 0x0004A754
	// (set) Token: 0x060008FD RID: 2301 RVA: 0x0004C56C File Offset: 0x0004A76C
	public int isDtsMatching
	{
		get
		{
			return this.m_nDtsMatchOk;
		}
		set
		{
			this.m_nDtsMatchOk = value;
			bool flag = this.m_nDtsMatchOk == 1;
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

	// Token: 0x060008FE RID: 2302 RVA: 0x0004C5AC File Offset: 0x0004A7AC
	public static DtsBatteManager GetInstance()
	{
		bool flag = DtsBatteManager.instance == null;
		if (flag)
		{
			DtsBatteManager.instance = new DtsBatteManager();
		}
		return DtsBatteManager.instance;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0004C5DC File Offset: 0x0004A7DC
	public void CheckForceTime()
	{
		bool flag = this.m_nDtsMatchOk == 1;
		if (flag)
		{
			bool flag2 = Time.time - this.matchingTime >= 10f;
			if (flag2)
			{
				this.m_nDtsMatchOk = 0;
			}
		}
		else
		{
			this.m_nDtsMatchOk = 0;
		}
		this.matchingTime = 0f;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0004C634 File Offset: 0x0004A834
	public void SetParent(Transform t)
	{
		bool flag = DtsBatteManager.mDtsMapItemObj == null;
		if (flag)
		{
			DtsBatteManager.mDtsMapItemObj = new GameObject("_DtsMapItem");
		}
		t.SetParent(DtsBatteManager.mDtsMapItemObj.transform);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0004C674 File Offset: 0x0004A874
	public GameObject GetDtsMapItemObj()
	{
		return DtsBatteManager.mDtsMapItemObj;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0004C68C File Offset: 0x0004A88C
	public void OnEnterScn()
	{
		fUIManager.mUILoading.SetRawImg(MatchManager.GetInstance().m_pFbData.nScnid.ToString(), MatchManager.GetInstance().m_pFbData.sMapAss);
		fUISoundManager.instance.StopSound("zhujiemian_beijingyinyue", 0);
		fUISoundManager.instance.StopSound("music_commen", 0);
		MatchManager.GetInstance().OnEnterGame();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0004C6F8 File Offset: 0x0004A8F8
	public void OnCreateDtsDoor()
	{
		bool flag = DtsBatteManager.mDtsMapItemObj == null;
		if (flag)
		{
			DtsBatteManager.mDtsMapItemObj = new GameObject("_DtsMapItem");
		}
		bool flag2 = this.m_nGameState > 2 || !fGameManager.mloadConfig.DtsMapXml.ContainsKey(MatchManager.GetInstance().m_pFbData.nScnid);
		if (!flag2)
		{
			DtsMapXmlData dtsMapXmlData = fGameManager.mloadConfig.DtsMapXml[MatchManager.GetInstance().m_pFbData.nScnid];
			foreach (int key in dtsMapXmlData.mDtsMapDoorData.Keys)
			{
				DtsMapDoorData dtsMapDoorData = dtsMapXmlData.mDtsMapDoorData[key];
				bool flag3 = dtsMapDoorData != null;
				if (flag3)
				{
					this.OnLoadDtsDoor(dtsMapDoorData);
				}
			}
		}
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0004C7E8 File Offset: 0x0004A9E8
	private void OnLoadDtsDoor(DtsMapDoorData dtsDoorData)
	{
		string loadmodel = dtsDoorData.loadmodel;
		bool flag = !this.DtsDoorObjList.ContainsKey(dtsDoorData.nId);
		if (flag)
		{
			WWW resWWW = GameRoot.GetResWWW("HeroAnimator/" + loadmodel + ".mo");
			bool flag2 = resWWW == null || resWWW.error != null;
			if (flag2)
			{
				GameRoot.LoadAsset("HeroAnimator/" + loadmodel + ".mo", new fResourceManager.DownFinishDelegate(this.OnLoadDtsDoorEnd), dtsDoorData);
			}
			else
			{
				this.OnLoadDtsDoorEnd(resWWW, dtsDoorData);
			}
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0004C874 File Offset: 0x0004AA74
	public void OnLoadDtsDoorEnd(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(DtsBatteManager.mDtsMapItemObj == null);
		if (flag)
		{
			DtsMapDoorData dtsMapDoorData = (DtsMapDoorData)sStyle;
			ArrayList szCreatePos = dtsMapDoorData.szCreatePos;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
			gameObject.transform.localPosition = new Vector3((float)((int)szCreatePos[0]) / 100f, (float)((int)szCreatePos[1]) / 100f, (float)((int)szCreatePos[2]) / 100f);
			gameObject.transform.localEulerAngles = new Vector3((float)((int)szCreatePos[3]), 0f, 0f);
			gameObject.transform.SetParent(DtsBatteManager.mDtsMapItemObj.transform);
			gameObject.name = "dtsdoor_" + dtsMapDoorData.nId.ToString();
			GameObject gameObject2 = gameObject.transform.Find("body").gameObject;
			DtsDoorCon dtsDoorCon = gameObject2.GetComponent<DtsDoorCon>();
			bool flag2 = dtsDoorCon == null;
			if (flag2)
			{
				dtsDoorCon = gameObject2.AddComponent<DtsDoorCon>();
				dtsDoorCon.m_nID = dtsMapDoorData.nId;
			}
			this.DtsDoorObjList.Add(dtsMapDoorData.nId, gameObject);
			this.LoadDoorHeadUi(dtsMapDoorData.nId, dtsMapDoorData.sName);
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0004C9F4 File Offset: 0x0004ABF4
	private void LoadDoorHeadUi(int nDoorId, string sName)
	{
		string url = string.Format("EffectRes/{0}.ef", "newplayerui");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), new ArrayList
			{
				nDoorId,
				sName
			});
		}
		else
		{
			this.loadReayPrefab(resWWW, new ArrayList
			{
				nDoorId,
				sName
			});
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0004CA78 File Offset: 0x0004AC78
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)sStyle;
			int key = (int)arrayList[0];
			bool flag2 = this.DtsDoorObjList.ContainsKey(key);
			if (flag2)
			{
				GameObject gameObject = this.DtsDoorObjList[key];
				bool flag3 = !(gameObject == null);
				if (flag3)
				{
					GameObject original = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject2.transform.SetParent(gameObject.transform);
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localRotation = Quaternion.identity;
					gameObject2.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
					GameObject3DName gameObject3DName = gameObject2.AddComponent<GameObject3DName>();
					string sName = "<color='#00aedd'>" + arrayList[1] + "</color>";
					gameObject3DName.setUI(sName, new Vector3(0f, 3.5f, 0f));
				}
			}
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0004CBAD File Offset: 0x0004ADAD
	public void OnSendMsgToServer(int nNetID, ArrayList arr)
	{
		fGameManager.Heroplayer.SendMessageToServer(nNetID, arr);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0004CBC0 File Offset: 0x0004ADC0
	public void OnDts_Scn_NetMsg(int nNetID, ArrayList szMsg, NetLiving _net)
	{
		switch (nNetID)
		{
		case 19003:
			_net.setPlayerNewPos(this.Split_Position(szMsg));
			break;
		case 19004:
			this.OnSetGameData((ArrayList)szMsg[0]);
			this.OnCreateUser((ArrayList)szMsg[1]);
			CSoundManager.Instance.maxDisParm = 1.8f;
			break;
		case 19005:
			this.OnSetGameData(szMsg);
			break;
		case 19007:
			this.OnUpdateOneUserData(_net, szMsg);
			break;
		case 19008:
			this.OnUseHpBox(szMsg);
			break;
		case 19009:
			this.OnStartGameResult(_net, szMsg);
			break;
		case 19010:
			this.OnSetUserQuanStat(_net, (int)szMsg[0]);
			break;
		case 19011:
			this.OnPickItemMsg(_net, szMsg);
			break;
		case 19012:
			this.OnMatchCheckTip(szMsg);
			break;
		}
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0004CCAC File Offset: 0x0004AEAC
	private void OnMatchCheckTip(ArrayList szMsg)
	{
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
		int num = (int)szMsg[0];
		int num2 = num;
		if (num2 != 1)
		{
			if (num2 == 2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(29056, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(29055, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.OnCLickNoBtn), null, null, null);
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
	private void OnCLickNoBtn(object szParam)
	{
		fUIManager.GetInstance().GotoHall(0);
		bool flag = PlayerDataManager.getInstance().HUserData != null;
		if (flag)
		{
			PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		}
		ChatManager.mInst.OnShowOrHideChatWin(true);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0004CE1C File Offset: 0x0004B01C
	private void OnPickItemMsg(NetLiving _net, ArrayList szMsg)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = gameuiCon.inst == null;
		if (!flag)
		{
			int count = szMsg.Count;
			for (int i = 0; i < count; i++)
			{
				arrayList = (ArrayList)szMsg[i];
				bool flag2 = arrayList.Count >= 1;
				if (flag2)
				{
					int nType = (int)arrayList[0];
					int nIndex = (int)arrayList[1];
					int nNum = (int)arrayList[2];
					string pickString = this.GetPickString(nType, nIndex, nNum);
					bool flag3 = !(pickString == string.Empty);
					if (flag3)
					{
						gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29039, new object[]
						{
							pickString
						}));
					}
				}
			}
			fUISoundManager.instance.PlayUISound("ui_dts_pickup", 0, null, 20);
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0004CF10 File Offset: 0x0004B110
	private string GetPickString(int nType, int nIndex, int nNum)
	{
		string result = string.Empty;
		string text = string.Empty;
		switch (nType)
		{
		case 1:
		case 7:
		{
			bool flag = fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(nIndex);
			if (flag)
			{
				HeroAllSkillData heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[nIndex];
				text = heroAllSkillData.sName;
			}
			bool flag2 = text != string.Empty;
			if (flag2)
			{
				result = Language.GetLang(29040, new object[]
				{
					text
				});
			}
			break;
		}
		case 2:
			result = this.GetSkillNameText(nIndex) + "*" + nNum.ToString();
			break;
		case 3:
			result = Language.GetLang(29015, new object[]
			{
				"*" + nNum.ToString()
			});
			break;
		case 4:
			result = Language.GetLang(29016, new object[]
			{
				"*" + nNum.ToString()
			});
			break;
		case 5:
			result = Language.GetLang(29017, new object[]
			{
				"*" + nNum.ToString()
			});
			break;
		case 8:
			result = Language.GetLang(29064, new object[]
			{
				"*" + nNum.ToString()
			});
			break;
		}
		bool flag3 = nType > 2 && DtsUICon.Inst != null && DtsUICon.Inst.visible;
		if (flag3)
		{
			DtsUICon.Inst.OnStartItemEff(nType);
		}
		return result;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0004D0B0 File Offset: 0x0004B2B0
	public string GetSkillNameText(int nType)
	{
		string result = string.Empty;
		switch (nType)
		{
		case 1:
			result = Language.GetLang(29010, new object[0]);
			break;
		case 2:
			result = Language.GetLang(29011, new object[0]);
			break;
		case 3:
			result = Language.GetLang(29012, new object[0]);
			break;
		case 4:
			result = Language.GetLang(29013, new object[0]);
			break;
		case 5:
			result = Language.GetLang(29014, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0004D14C File Offset: 0x0004B34C
	private void OnSetUserQuanStat(NetLiving _net, int nStat)
	{
		DtsUser userObj = this.GetUserObj(_net.playerCommon.m_nCid);
		if (userObj != null)
		{
			userObj.SetIsQuanIngStat(nStat);
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0004D17C File Offset: 0x0004B37C
	private void OnStartGameResult(NetLiving _net, ArrayList szData)
	{
		bool flag = DtsUICon.Inst != null && DtsUICon.Inst.visible;
		if (flag)
		{
			DtsUICon.Inst.OnStartGame(szData);
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0004D1B8 File Offset: 0x0004B3B8
	private void OnUseHpBox(ArrayList szMsg)
	{
		bool flag = DtsUICon.Inst != null;
		if (flag)
		{
			DtsUICon.Inst.OnSetHpTime(szMsg);
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0004D1E4 File Offset: 0x0004B3E4
	public void OpenDtsUI(DtsUser _net)
	{
		bool flag = DtsUICon.Inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("DtsUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadUiOk), _net, true);
		}
		else
		{
			DtsUICon.Inst.Open();
			this.StartOpenDtsUI(_net);
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0004D238 File Offset: 0x0004B438
	private void LoadUiOk(string sUI, GameObject ob, object szParam)
	{
		DtsUser net = (DtsUser)szParam;
		this.StartOpenDtsUI(net);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0004D258 File Offset: 0x0004B458
	private void StartOpenDtsUI(DtsUser _net)
	{
		fUIManager.GetInstance().ProgressChange(false, "LoadingGamePlayer", 1f, 1f);
		bool flag = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag)
		{
			VoiceUICon.inst.Close();
		}
		bool flag2 = DtsUICon.Inst != null;
		if (flag2)
		{
			DtsUICon.Inst.showDtsUi(_net);
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0004D2CC File Offset: 0x0004B4CC
	private Vector3 Split_Position(ArrayList _Value)
	{
		return new Vector3((float)int.Parse(_Value[0].ToString()) / 100f, (float)int.Parse(_Value[2].ToString()) / 100f, (float)int.Parse(_Value[1].ToString()) / 100f);
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0004D32C File Offset: 0x0004B52C
	public void OnSetGameData(ArrayList szData)
	{
		this.m_nGameState = (int)szData[0];
		this.m_nLivePlayerNum = (int)szData[1];
		this.m_nDelayStartTime = (int)szData[2];
		this.m_nDelayEnterScnTime = (int)szData[3];
		this.m_nHpBosUseTime = (int)szData[4];
		this.m_nDzBosUseTime = (int)szData[5];
		bool flag = this.m_nGameState == 2 && this.DtsDoorObjList.Count < 1;
		if (flag)
		{
			DtsBatteManager.GetInstance().OnCreateDtsDoor();
		}
		bool flag2 = this.m_nGameState == 3;
		if (flag2)
		{
			this.OnDelDtsDoor();
		}
		bool flag3 = DtsUICon.Inst != null && DtsUICon.Inst.visible;
		if (flag3)
		{
			DtsUser userObj = this.GetUserObj(fGameManager.GetInstance().getPlayer().playerCommon.m_nCid);
			bool flag4 = userObj != null;
			if (flag4)
			{
				DtsUICon.Inst.showDtsUi(userObj);
			}
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0004D43C File Offset: 0x0004B63C
	public void OnCreateUser(ArrayList szData)
	{
		ArrayList szData2 = new ArrayList();
		int count = szData.Count;
		for (int i = 0; i < count; i += 2)
		{
			int nCid = (int)szData[i];
			szData2 = (ArrayList)szData[i + 1];
			this.AddDtsUser(nCid, szData2);
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0004D490 File Offset: 0x0004B690
	private void AddDtsUser(int nCid, ArrayList szData)
	{
		bool flag = !this.AllDtsUserList.ContainsKey(nCid);
		if (flag)
		{
			DtsUser dtsUser = new DtsUser(szData);
			this.AllDtsUserList.Add(nCid, dtsUser);
		}
		else
		{
			DtsUser dtsUser = this.AllDtsUserList[nCid];
			dtsUser.setUser(szData);
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
	public DtsUser GetUserObj(int nCid)
	{
		DtsUser dtsUser = null;
		bool flag = !this.AllDtsUserList.ContainsKey(nCid);
		DtsUser result;
		if (flag)
		{
			result = dtsUser;
		}
		else
		{
			result = this.AllDtsUserList[nCid];
		}
		return result;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0004D51C File Offset: 0x0004B71C
	private void OnDelDtsDoor()
	{
		foreach (int key in this.DtsDoorObjList.Keys)
		{
			GameObject obj = this.DtsDoorObjList[key];
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.DtsDoorObjList.Clear();
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0004D594 File Offset: 0x0004B794
	public void ClearDtsMatchData()
	{
		this.m_nGameState = 0;
		foreach (int key in this.DtsMapItemList.Keys)
		{
			GameObject gameObject = this.DtsMapItemList[key];
			bool flag = !(gameObject == null);
			if (flag)
			{
				DtsMapItemCon component = gameObject.GetComponent<DtsMapItemCon>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.thisItem.OnNetDestroy();
				}
			}
		}
		bool flag3 = fGameManager.Heroplayer != null;
		if (flag3)
		{
			fGameManager.Heroplayer.playerDtsItemObj = null;
		}
		this.AllDtsUserList.Clear();
		this.OnDelDtsDoor();
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0004D664 File Offset: 0x0004B864
	private void OnUpdateOneUserData(NetLiving _net, ArrayList szData)
	{
		DtsUser userObj = this.GetUserObj(_net.playerCommon.m_nCid);
		if (userObj != null)
		{
			userObj.setUser(szData);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0004D694 File Offset: 0x0004B894
	public void AddDtsMapItemData(int nNetid, GameObject oObj)
	{
		bool flag = !this.DtsMapItemList.ContainsKey(nNetid);
		if (flag)
		{
			this.DtsMapItemList.Add(nNetid, oObj);
		}
		else
		{
			this.DtsMapItemList[nNetid] = oObj;
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0004D6D8 File Offset: 0x0004B8D8
	public void DelDtsMapItemData(int nNetid)
	{
		bool flag = this.DtsMapItemList.ContainsKey(nNetid);
		if (flag)
		{
			this.DtsMapItemList.Remove(nNetid);
		}
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0004D708 File Offset: 0x0004B908
	public void SetMapItemStat(DtsUser dUser)
	{
		foreach (int key in this.DtsMapItemList.Keys)
		{
			GameObject gameObject = this.DtsMapItemList[key];
			bool flag = !(gameObject == null);
			if (flag)
			{
				DtsMapItemCon component = gameObject.GetComponent<DtsMapItemCon>();
				bool flag2 = !(component == null) && component.thisItem != null;
				if (flag2)
				{
					bool mapItemStat = dUser.IsItemCan(component.thisItem.m_nItmeType, component.thisItem.m_nItmeIndex, 0, component.thisItem.m_szItemData);
					component.SetMapItemStat(mapItemStat);
				}
			}
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0004D7DC File Offset: 0x0004B9DC
	public OneHeroSkillData GetOneHeroSkillData(int nHeroId, int nSkillType)
	{
		OneHeroSkillData oneHeroSkillData = null;
		bool flag = !fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(nHeroId);
		OneHeroSkillData result;
		if (flag)
		{
			result = oneHeroSkillData;
		}
		else
		{
			HeroAllSkillData heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[nHeroId];
			bool flag2 = !heroAllSkillData.mHeroSkillData.ContainsKey(nSkillType);
			if (flag2)
			{
				result = oneHeroSkillData;
			}
			else
			{
				result = heroAllSkillData.mHeroSkillData[nSkillType];
			}
		}
		return result;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0004D844 File Offset: 0x0004BA44
	public int GetHeroSkillType(int nHeroId, int nSkillId)
	{
		int num = 0;
		bool flag = !fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(nHeroId);
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			HeroAllSkillData heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[nHeroId];
			bool flag2 = heroAllSkillData == null;
			if (flag2)
			{
				result = num;
			}
			else
			{
				Dictionary<int, int> mSkillTypeData = heroAllSkillData.mSkillTypeData;
				bool flag3 = !mSkillTypeData.ContainsKey(nSkillId);
				if (flag3)
				{
					result = num;
				}
				else
				{
					result = mSkillTypeData[nSkillId];
				}
			}
		}
		return result;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0004D8C0 File Offset: 0x0004BAC0
	public int CanDtsScnSkill(NetLiving _net, int nSkillId)
	{
		int num = 0;
		bool flag = MatchManager.GetInstance().m_nMatchType != 4;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = _net == null;
			if (flag2)
			{
				result = num;
			}
			else
			{
				DtsUser userObj = this.GetUserObj(_net.playerCommon.m_nCid);
				bool flag3 = userObj == null;
				if (flag3)
				{
					result = num;
				}
				else
				{
					int nNowHeroID = userObj.m_nNowHeroID;
					int heroSkillType = this.GetHeroSkillType(nNowHeroID, nSkillId);
					OneHeroSkillData oneHeroSkillData = this.GetOneHeroSkillData(nNowHeroID, heroSkillType);
					bool flag4 = oneHeroSkillData == null;
					if (flag4)
					{
						result = num;
					}
					else
					{
						bool flag5 = oneHeroSkillData.nIsOpen == 1;
						if (flag5)
						{
							result = -1;
						}
						else
						{
							ArrayList szSkillList = userObj.m_szSkillList;
							int num2 = szSkillList.IndexOf(heroSkillType.ToString());
							bool flag6 = num2 == -1;
							if (flag6)
							{
								result = num;
							}
							else
							{
								result = (int)szSkillList[num2 + 1];
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
	public void OnCheckItem(int nCheckType, int nItemNetId)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(nItemNetId);
			arrayList.Add(0);
			arrayList.Add(nCheckType);
			fGameManager.Heroplayer.TosFun("SendMsgToServer", new object[]
			{
				19006,
				arrayList
			});
		}
	}

	// Token: 0x0400068C RID: 1676
	private static DtsBatteManager instance;

	// Token: 0x0400068D RID: 1677
	public const int DTSMATCHTYPE = 4;

	// Token: 0x0400068E RID: 1678
	public const int DTSMAXPICKNUM = 10;

	// Token: 0x0400068F RID: 1679
	public int m_nGameState;

	// Token: 0x04000690 RID: 1680
	public int m_nLivePlayerNum;

	// Token: 0x04000691 RID: 1681
	public int m_nDelayStartTime;

	// Token: 0x04000692 RID: 1682
	public int m_nDelayEnterScnTime;

	// Token: 0x04000693 RID: 1683
	public int m_nHpBosUseTime;

	// Token: 0x04000694 RID: 1684
	public int m_nDzBosUseTime;

	// Token: 0x04000695 RID: 1685
	private Dictionary<int, DtsUser> AllDtsUserList = new Dictionary<int, DtsUser>();

	// Token: 0x04000696 RID: 1686
	private Dictionary<int, GameObject> DtsDoorObjList = new Dictionary<int, GameObject>();

	// Token: 0x04000697 RID: 1687
	private Dictionary<int, GameObject> DtsMapItemList = new Dictionary<int, GameObject>();

	// Token: 0x04000698 RID: 1688
	private static GameObject mDtsMapItemObj;

	// Token: 0x04000699 RID: 1689
	private int m_nDtsMatchOk;

	// Token: 0x0400069A RID: 1690
	private float matchingTime;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class KingTrainManager
{
	// Token: 0x060016ED RID: 5869 RVA: 0x000D52C0 File Offset: 0x000D34C0
	public static KingTrainManager GetInstance()
	{
		bool flag = KingTrainManager.mInst == null;
		if (flag)
		{
			KingTrainManager.mInst = new KingTrainManager();
		}
		return KingTrainManager.mInst;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000D52F0 File Offset: 0x000D34F0
	public void OnUpdateFuHuoBi()
	{
		bool flag = KingCon.inst != null;
		if (flag)
		{
			KingCon.inst.UpdateRevive();
		}
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000D531C File Offset: 0x000D351C
	public void OnKingTrain_Scn_NetMsg(int nNetID, ArrayList szMsg)
	{
		switch (nNetID)
		{
		case 19001:
		{
			this.OnClear();
			this.m_pKingGame = new KingGameInfo((ArrayList)szMsg[0], 1);
			this.m_szAllUser = (ArrayList)szMsg[1];
			this.OnSetKingUserData(this.m_szAllUser);
			bool flag = KingCon.inst == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("King", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadUiOk), null, true);
			}
			else
			{
				KingCon.inst.Open();
				KingCon.inst.onGameData(this.m_pKingGame);
			}
			break;
		}
		case 19002:
		{
			bool flag2 = this.m_pKingGame != null;
			if (flag2)
			{
				this.m_pKingGame.OnGameOver();
			}
			bool flag3 = KingCon.inst != null;
			if (flag3)
			{
				KingCon.inst.OnGameOver(szMsg);
			}
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
			this.OnClear();
			break;
		}
		case 19003:
		{
			bool flag4 = this.m_pKingGame != null;
			if (flag4)
			{
				this.m_pKingGame.OnSetData(szMsg);
			}
			bool flag5 = KingCon.inst != null;
			if (flag5)
			{
				KingCon.inst.OnFightStart(this.m_pKingGame);
			}
			break;
		}
		case 19004:
		{
			this.m_pKingGame.OnRoundEnd(szMsg);
			bool flag6 = KingCon.inst != null;
			if (flag6)
			{
				KingCon.inst.OnFightEnd(this.m_pKingGame);
			}
			break;
		}
		case 19005:
		{
			bool flag7 = this.m_pKingGame != null;
			if (flag7)
			{
				this.m_pKingGame.OnSetData(szMsg);
			}
			bool flag8 = KingCon.inst != null;
			if (flag8)
			{
				KingCon.inst.OnFightReady(this.m_pKingGame);
			}
			break;
		}
		case 19007:
		{
			bool flag9 = szMsg.Count > 1;
			if (flag9)
			{
				szMsg[1] = Fun.DecodingUTF8(szMsg[1].ToString());
			}
			this.OnSetKingUserData((int)szMsg[0], szMsg);
			break;
		}
		case 19008:
		{
			bool flag10 = this.m_pKingGame != null;
			if (flag10)
			{
				this.m_pKingGame.m_nMonsterNum = (int)szMsg[0];
			}
			bool flag11 = KingCon.inst != null;
			if (flag11)
			{
				KingCon.inst.OnUpdateMonsterNum(this.m_pKingGame.m_nMonsterNum);
			}
			break;
		}
		case 19011:
			this.OnUserLeave((int)szMsg[0], szMsg);
			break;
		case 19013:
		{
			bool flag12 = szMsg.Count > 1;
			if (flag12)
			{
				szMsg[1] = Fun.DecodingUTF8(szMsg[1].ToString());
			}
			this.OnSetKingUserData((int)szMsg[0], szMsg);
			bool flag13 = KingCon.inst != null;
			if (flag13)
			{
				KingCon.inst.OnAddUser(szMsg);
			}
			break;
		}
		case 19014:
		{
			bool flag14 = this.m_pKingGame != null;
			if (flag14)
			{
				this.m_pKingGame.OnUpdateAllUserDeadState(szMsg);
				bool flag15 = KingCon.inst != null;
				if (flag15)
				{
					KingCon.inst.OnAllUserDead(this.m_pKingGame.m_nAllUserDeadState);
				}
			}
			break;
		}
		}
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000D5678 File Offset: 0x000D3878
	public void OnClear()
	{
		this.m_szAllUser.Clear();
		this.AllKingUserData.Clear();
		bool flag = KingCon.inst != null;
		if (flag)
		{
			KingCon.inst.m_szAllUserObj.Clear();
		}
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000D56C0 File Offset: 0x000D38C0
	public void OnHeiAnTrain_Scn_NetMsg(int nNetID, ArrayList szMsg)
	{
		switch (nNetID)
		{
		case 19001:
		{
			this.OnClear();
			this.m_pKingGame = new KingGameInfo((ArrayList)szMsg[0], 2);
			this.m_szAllUser = (ArrayList)szMsg[1];
			this.OnSetKingUserData(this.m_szAllUser);
			bool flag = KingCon.inst == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("King", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadUiOk), null, true);
			}
			else
			{
				KingCon.inst.Open();
				KingCon.inst.onGameData(this.m_pKingGame);
			}
			break;
		}
		case 19002:
		{
			bool flag2 = this.m_pKingGame != null;
			if (flag2)
			{
				this.m_pKingGame.OnGameOver();
			}
			bool flag3 = KingCon.inst != null;
			if (flag3)
			{
				KingCon.inst.OnGameOver(szMsg);
			}
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
			this.OnClear();
			break;
		}
		case 19003:
		{
			bool flag4 = this.m_pKingGame != null;
			if (flag4)
			{
				this.m_pKingGame.OnSetData(szMsg);
			}
			bool flag5 = KingCon.inst != null;
			if (flag5)
			{
				KingCon.inst.OnFightStart(this.m_pKingGame);
			}
			break;
		}
		case 19004:
		{
			this.m_pKingGame.OnRoundEnd(szMsg);
			bool flag6 = KingCon.inst != null;
			if (flag6)
			{
				KingCon.inst.OnOpenDoor((ArrayList)szMsg[5], 1);
			}
			break;
		}
		case 19005:
		{
			bool flag7 = this.m_pKingGame != null;
			if (flag7)
			{
				this.m_pKingGame.OnSetData(szMsg);
			}
			break;
		}
		case 19007:
		{
			bool flag8 = szMsg.Count > 1;
			if (flag8)
			{
				szMsg[1] = Fun.DecodingUTF8(szMsg[1].ToString());
			}
			this.OnSetKingUserData((int)szMsg[0], szMsg);
			break;
		}
		case 19008:
		{
			bool flag9 = this.m_pKingGame != null;
			if (flag9)
			{
				this.m_pKingGame.m_nMonsterNum = (int)szMsg[0];
			}
			bool flag10 = KingCon.inst != null;
			if (flag10)
			{
				KingCon.inst.OnUpdateMonsterNum(this.m_pKingGame.m_nMonsterNum);
			}
			break;
		}
		case 19011:
			this.OnUserLeave((int)szMsg[0], szMsg);
			break;
		case 19013:
		{
			bool flag11 = szMsg.Count > 1;
			if (flag11)
			{
				szMsg[1] = Fun.DecodingUTF8(szMsg[1].ToString());
			}
			this.OnSetKingUserData((int)szMsg[0], szMsg);
			bool flag12 = KingCon.inst != null;
			if (flag12)
			{
				KingCon.inst.OnAddUser(szMsg);
			}
			break;
		}
		case 19014:
		{
			bool flag13 = this.m_pKingGame != null;
			if (flag13)
			{
				this.m_pKingGame.OnUpdateAllUserDeadState(szMsg);
				bool flag14 = KingCon.inst != null;
				if (flag14)
				{
					KingCon.inst.OnAllUserDead(this.m_pKingGame.m_nAllUserDeadState);
				}
			}
			break;
		}
		}
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x000D59FF File Offset: 0x000D3BFF
	private void LoadUiOk(string sUI, GameObject ob, object szParam)
	{
		KingCon.inst.OnShowAllUser(this.AllKingUserData);
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x000D5A14 File Offset: 0x000D3C14
	public void OnSetKingUserData(ArrayList szMsg)
	{
		int count = szMsg.Count;
		for (int i = 0; i < count; i += 2)
		{
			ArrayList arrayList = (ArrayList)szMsg[i + 1];
			bool flag = arrayList.Count > 1;
			if (flag)
			{
				arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
			}
			this.OnSetKingUserData((int)szMsg[i], (ArrayList)arrayList.Clone());
		}
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x000D5A94 File Offset: 0x000D3C94
	public void OnSetKingUserData(int nCid, ArrayList szUserInfo)
	{
		bool flag = this.AllKingUserData.ContainsKey(nCid);
		if (flag)
		{
			this.AllKingUserData[nCid] = szUserInfo;
		}
		else
		{
			this.AllKingUserData.Add(nCid, szUserInfo);
		}
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000D5AD4 File Offset: 0x000D3CD4
	private void OnUserLeave(int nCid, ArrayList szUserInfo)
	{
		bool flag = this.AllKingUserData.ContainsKey(nCid);
		if (flag)
		{
			this.AllKingUserData.Remove(nCid);
		}
		bool flag2 = KingCon.inst != null;
		if (flag2)
		{
			KingCon.inst.OnDeleteUser(nCid);
		}
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000D5B20 File Offset: 0x000D3D20
	public void OnLianSha(int nCount)
	{
		bool flag = KingCon.inst != null;
		if (flag)
		{
			KingCon.inst.OnLianShaEffect(nCount);
		}
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000D5B4C File Offset: 0x000D3D4C
	public ArrayList DictonarySort()
	{
		int num = 0;
		ArrayList arrayList = new ArrayList();
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		IOrderedEnumerable<KeyValuePair<int, ArrayList>> orderedEnumerable = from objDic in this.AllKingUserData
		orderby (int)objDic.Value[7] descending
		select objDic;
		foreach (KeyValuePair<int, ArrayList> keyValuePair in orderedEnumerable)
		{
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			num++;
			arrayList.Add(keyValuePair.Key);
		}
		return arrayList;
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x000D5C04 File Offset: 0x000D3E04
	public void OnLoadMapOk()
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 12 && this.m_pKingGame != null && !(KingCon.inst == null);
		if (flag)
		{
			KingCon.inst.OnOpenDoor(this.m_pKingGame.m_szOpenDoorData, 2);
		}
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x000D5C5C File Offset: 0x000D3E5C
	public int GetGameState()
	{
		bool flag = this.m_pKingGame == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_pKingGame.m_nGameState;
		}
		return result;
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000D5C8C File Offset: 0x000D3E8C
	public int GetFuhuoState()
	{
		bool flag = this.m_pKingGame == null;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = this.m_pKingGame.m_nGameState != 1;
			if (flag2)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000D5CCC File Offset: 0x000D3ECC
	public int GetGameIsReady()
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType != 9;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = this.m_pKingGame == null;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = this.m_pKingGame.m_nGameState == -1;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					bool flag4 = this.m_pKingGame.m_nFightState > 0;
					if (flag4)
					{
						result = 0;
					}
					else
					{
						result = 1;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000D5D40 File Offset: 0x000D3F40
	public static string GetNanDuMsg(int nNanDu)
	{
		string result = string.Empty;
		switch (nNanDu)
		{
		case -1:
			result = Language.GetLang(26060, new object[0]);
			break;
		case 0:
			result = Language.GetLang(26034, new object[0]);
			break;
		case 1:
			result = Language.GetLang(26035, new object[0]);
			break;
		case 2:
			result = Language.GetLang(26036, new object[0]);
			break;
		case 3:
			result = Language.GetLang(26037, new object[0]);
			break;
		case 4:
			result = Language.GetLang(26038, new object[0]);
			break;
		case 5:
			result = Language.GetLang(26039, new object[0]);
			break;
		case 6:
			result = Language.GetLang(26040, new object[0]);
			break;
		case 7:
			result = Language.GetLang(26041, new object[0]);
			break;
		case 8:
			result = Language.GetLang(26042, new object[0]);
			break;
		case 9:
			result = Language.GetLang(26043, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000D5E74 File Offset: 0x000D4074
	public int GetPveUserDeahState()
	{
		bool flag = this.m_pKingGame == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_pKingGame.m_nAllUserDeadState;
		}
		return result;
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000D5EA4 File Offset: 0x000D40A4
	public int GetXing()
	{
		bool flag = this.m_pKingGame == null || string.IsNullOrEmpty(this.m_pKingGame.m_sGameStart);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			DateTime serverDate = Fun.GetServerDate();
			DateTime d = DateTime.Parse(this.m_pKingGame.m_sGameStart);
			int nLongTime = (int)(serverDate - d).TotalSeconds;
			result = this.GetXing(nLongTime);
		}
		return result;
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x000D5F10 File Offset: 0x000D4110
	public int GetXing(int nLongTime)
	{
		bool flag = nLongTime < 720;
		int result;
		if (flag)
		{
			result = 3;
		}
		else
		{
			bool flag2 = nLongTime < 1260;
			if (flag2)
			{
				result = 2;
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x04001233 RID: 4659
	public static KingTrainManager mInst;

	// Token: 0x04001234 RID: 4660
	public Dictionary<int, ArrayList> AllKingUserData = new Dictionary<int, ArrayList>();

	// Token: 0x04001235 RID: 4661
	public ArrayList m_szAllUser = new ArrayList();

	// Token: 0x04001236 RID: 4662
	public KingGameInfo m_pKingGame;
}

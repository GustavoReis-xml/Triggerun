using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002C7 RID: 711
public class RankManager
{
	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x0600207B RID: 8315 RVA: 0x00129BDC File Offset: 0x00127DDC
	// (set) Token: 0x0600207C RID: 8316 RVA: 0x00129BF4 File Offset: 0x00127DF4
	public int RankUiTabIndex
	{
		get
		{
			return this._RankUiTabIndex;
		}
		set
		{
			this._RankUiTabIndex = value;
		}
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x00129C00 File Offset: 0x00127E00
	public static RankManager GetInstance()
	{
		bool flag = RankManager.inst == null;
		if (flag)
		{
			RankManager.inst = new RankManager();
		}
		return RankManager.inst;
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x00129C30 File Offset: 0x00127E30
	public void OnRankMsg(int nNetID, ArrayList szMsg)
	{
		switch (nNetID)
		{
		case 17001:
			this.OnUserRankBack(szMsg);
			break;
		case 17002:
			this.OnHeroRankBack(szMsg);
			break;
		case 17003:
			this.UpdateSkyladderRank(szMsg);
			break;
		case 17005:
			this.OnPveRankBack(szMsg);
			break;
		case 17006:
			this.OnDtsRankBack(szMsg);
			break;
		case 17007:
			this.OnFlowerRankBack(szMsg);
			break;
		}
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x00129CAC File Offset: 0x00127EAC
	public bool CheckNextDay(string sDate, string sDate2)
	{
		bool flag = sDate == string.Empty;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = sDate2 == string.Empty;
			if (flag2)
			{
				result = false;
			}
			else
			{
				DateTime dateTime = Convert.ToDateTime(sDate);
				DateTime dateTime2 = Convert.ToDateTime(sDate2);
				DateTime serverDate = Fun.GetServerDate();
				DateTime d = Convert.ToDateTime(string.Format("{0} 00:00:00", serverDate.ToString("yyyy-MM-dd")));
				bool flag3 = dateTime.Day != dateTime2.Day && (serverDate - d).TotalSeconds >= 3600.0;
				if (flag3)
				{
					this.CleanAllData();
					result = true;
				}
				else
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x00129D68 File Offset: 0x00127F68
	public void CleanAllData()
	{
		this.m_szSortUserRank.Clear();
		this.m_nUserRankMaxSize = 0;
		this.m_nMyUserRank = 0;
		this.m_szAllSortHeroRank.Clear();
		this.m_szHeroRankMaxSize.Clear();
		this.m_szMyHeroRank.Clear();
		this.m_szPveRankMaxSize.Clear();
		this.m_szSortPveRank.Clear();
		this.m_szMyPveRank.Clear();
		this.m_szSortDtsRank.Clear();
		this.m_nDtsRankMaxSize = 0;
		this.m_nMyDtsRank = 0;
		this.m_szSortFlowerRank.Clear();
		this.m_nFlowerRankMaxSize = 0;
		this.m_nMyFlowerRank = 0;
		this.sRankDate = string.Empty;
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x00129E18 File Offset: 0x00128018
	private void OnHeroRankBack(ArrayList szMsg)
	{
		string sDate = szMsg[1].ToString();
		bool flag = this.CheckNextDay(sDate, RankManager.GetInstance().sRankDate);
		if (!flag)
		{
			this.sRankDate = szMsg[1].ToString();
			int nHeroID = (int)szMsg[2];
			ArrayList arrayList = (ArrayList)szMsg[3];
			int value = (int)szMsg[4];
			int num = (int)szMsg[5];
			int value2 = (int)szMsg[6];
			int num2 = (int)szMsg[7];
			int num3 = (int)szMsg[8];
			int nSeason = (int)szMsg[9];
			bool flag2 = num3 == -1;
			if (flag2)
			{
				bool flag3 = this.RankUiTabIndex == 1 && RankUICon.inst != null;
				if (flag3)
				{
					RankUICon.inst.ShowHeroRankNotRankOK();
				}
			}
			else
			{
				int heroRankKey = this.GetHeroRankKey(nSeason, nHeroID);
				bool flag4 = this.m_szMyHeroRank.ContainsKey(heroRankKey);
				if (flag4)
				{
					this.m_szMyHeroRank[heroRankKey] = value;
				}
				else
				{
					this.m_szMyHeroRank.Add(heroRankKey, value);
				}
				bool flag5 = this.m_szAllSortHeroRank.ContainsKey(heroRankKey);
				Dictionary<int, ArrayList> dictionary;
				if (flag5)
				{
					dictionary = this.m_szAllSortHeroRank[heroRankKey];
				}
				else
				{
					dictionary = new Dictionary<int, ArrayList>();
					this.m_szAllSortHeroRank.Add(heroRankKey, dictionary);
				}
				bool flag6 = this.m_szHeroRankMaxSize.ContainsKey(heroRankKey);
				if (flag6)
				{
					this.m_szHeroRankMaxSize[heroRankKey] = value2;
				}
				else
				{
					this.m_szHeroRankMaxSize.Add(heroRankKey, value2);
				}
				int count = arrayList.Count;
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					bool flag7 = i % 10 == 0;
					if (flag7)
					{
						bool flag8 = i > 0;
						if (flag8)
						{
							arrayList2 = new ArrayList();
						}
						int key = i / 10 + 1 + (num - 1) / 10 * 10;
						bool flag9 = dictionary.ContainsKey(key);
						if (flag9)
						{
							dictionary[key] = arrayList2;
						}
						else
						{
							dictionary.Add(key, arrayList2);
						}
					}
					ArrayList arrayList3 = (ArrayList)arrayList[i];
					bool flag10 = arrayList3.Count > 0;
					if (flag10)
					{
						arrayList2.Add(arrayList3);
					}
				}
				bool flag11 = this.RankUiTabIndex == 1 && RankUICon.inst != null && num2 != 1;
				if (flag11)
				{
					RankUICon.inst.ShowHeroRankList(num);
				}
			}
		}
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x0012A0BC File Offset: 0x001282BC
	private void OnUserRankBack(ArrayList szMsg)
	{
		string sDate = szMsg[1].ToString();
		bool flag = this.CheckNextDay(sDate, RankManager.GetInstance().sRankDate);
		if (!flag)
		{
			this.sRankDate = szMsg[1].ToString();
			ArrayList arrayList = (ArrayList)szMsg[2];
			int nMyUserRank = (int)szMsg[3];
			int num = (int)szMsg[4];
			this.m_nUserRankMaxSize = (int)szMsg[5];
			int num2 = (int)szMsg[6];
			int num3 = (int)szMsg[7];
			bool flag2 = num3 == -1;
			if (flag2)
			{
				bool flag3 = this.RankUiTabIndex == 0 && RankUICon.inst != null;
				if (flag3)
				{
					RankUICon.inst.ShowUserRankNotRankOK();
				}
			}
			else
			{
				this.m_nMyUserRank = nMyUserRank;
				int count = arrayList.Count;
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					bool flag4 = i % 10 == 0;
					if (flag4)
					{
						bool flag5 = i > 0;
						if (flag5)
						{
							arrayList2 = new ArrayList();
						}
						int key = i / 10 + 1 + (num - 1) / 10 * 10;
						bool flag6 = this.m_szSortUserRank.ContainsKey(key);
						if (flag6)
						{
							this.m_szSortUserRank[key] = arrayList2;
						}
						else
						{
							this.m_szSortUserRank.Add(key, arrayList2);
						}
					}
					ArrayList arrayList3 = (ArrayList)arrayList[i];
					bool flag7 = arrayList3.Count > 0;
					if (flag7)
					{
						arrayList2.Add(arrayList3);
					}
				}
				bool flag8 = this.RankUiTabIndex == 0 && RankUICon.inst != null && num2 != 1;
				if (flag8)
				{
					RankUICon.inst.ShowUserRankList(num);
				}
			}
		}
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x0012A29C File Offset: 0x0012849C
	public int GetMaxUserSize()
	{
		return this.m_nUserRankMaxSize;
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x0012A2B4 File Offset: 0x001284B4
	public int GetHeroRankKey(int nSeason, int nHeroID)
	{
		return nSeason * 10000 + nHeroID;
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x0012A2D0 File Offset: 0x001284D0
	public int GetMaxHeroSize(int nHeroID, int nSeason)
	{
		int heroRankKey = this.GetHeroRankKey(nSeason, nHeroID);
		bool flag = this.m_szHeroRankMaxSize.ContainsKey(heroRankKey);
		int result;
		if (flag)
		{
			result = this.m_szHeroRankMaxSize[heroRankKey];
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x0012A30C File Offset: 0x0012850C
	public bool IsHadHeroRankData(int nSeason, int nHeroID)
	{
		int heroRankKey = this.GetHeroRankKey(nSeason, nHeroID);
		return this.m_szAllSortHeroRank.ContainsKey(heroRankKey);
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x0012A334 File Offset: 0x00128534
	public ArrayList GetHeroListByPage(int nHerID, int nPage, int nSeason)
	{
		ArrayList arrayList = new ArrayList();
		int heroRankKey = this.GetHeroRankKey(nSeason, nHerID);
		bool flag = !this.m_szAllSortHeroRank.ContainsKey(heroRankKey);
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			Dictionary<int, ArrayList> dictionary = this.m_szAllSortHeroRank[heroRankKey];
			bool flag2 = dictionary.ContainsKey(nPage);
			if (flag2)
			{
				result = dictionary[nPage];
			}
			else
			{
				result = arrayList;
			}
		}
		return result;
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x0012A398 File Offset: 0x00128598
	public ArrayList GetUserListByPage(int nPage)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.m_szSortUserRank.ContainsKey(nPage);
		ArrayList result;
		if (flag)
		{
			result = this.m_szSortUserRank[nPage];
		}
		else
		{
			result = arrayList;
		}
		return result;
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x0012A3D4 File Offset: 0x001285D4
	public bool HadHeroPage(int nHeroID, int nPage, int nSeason)
	{
		int heroRankKey = this.GetHeroRankKey(nSeason, nHeroID);
		bool flag = !this.m_szAllSortHeroRank.ContainsKey(heroRankKey);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			Dictionary<int, ArrayList> dictionary = this.m_szAllSortHeroRank[heroRankKey];
			result = dictionary.ContainsKey(nPage);
		}
		return result;
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x0012A41C File Offset: 0x0012861C
	public bool HadUserPage(int nPage)
	{
		return this.m_szSortUserRank.ContainsKey(nPage);
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x0012A43C File Offset: 0x0012863C
	public int GetMyLevelRank()
	{
		bool flag = this.m_nMyUserRank < 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_nMyUserRank + 1;
		}
		return result;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x0012A468 File Offset: 0x00128668
	public int GetMyHeroRank(int nHeroID, int nSeason)
	{
		int heroRankKey = this.GetHeroRankKey(nSeason, nHeroID);
		bool flag = this.m_szMyHeroRank.ContainsKey(heroRankKey);
		int result;
		if (flag)
		{
			int num = this.m_szMyHeroRank[heroRankKey];
			bool flag2 = num < 0;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				result = num + 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x0012A4BC File Offset: 0x001286BC
	private void UpdateSkyladderRank(ArrayList szData)
	{
		int num = (int)szData[1];
		int nPage = (int)szData[2];
		ArrayList arrayList = (ArrayList)szData[3];
		int value = (int)arrayList[0];
		int value2 = (int)arrayList[1];
		arrayList = (ArrayList)arrayList[2];
		bool flag = this.m_szMySkyRank.ContainsKey(num);
		if (flag)
		{
			this.m_szMySkyRank[num] = value;
		}
		else
		{
			this.m_szMySkyRank.Add(num, value);
		}
		bool flag2 = this.m_szSkyladderMaxSize.ContainsKey(num);
		if (flag2)
		{
			this.m_szSkyladderMaxSize[num] = value2;
		}
		else
		{
			this.m_szSkyladderMaxSize.Add(num, value2);
		}
		bool flag3 = this.m_szSkyladderRank.ContainsKey(num);
		Dictionary<int, ArrayList> dictionary;
		if (flag3)
		{
			dictionary = this.m_szSkyladderRank[num];
		}
		else
		{
			dictionary = new Dictionary<int, ArrayList>();
			this.m_szSkyladderRank.Add(num, dictionary);
		}
		for (int i = 0; i < arrayList.Count; i++)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i];
			bool flag4 = arrayList2.Count != 0;
			if (flag4)
			{
				int key = (int)arrayList2[8];
				bool flag5 = dictionary.ContainsKey(key);
				if (flag5)
				{
					dictionary[key] = arrayList2;
				}
				else
				{
					dictionary.Add(key, arrayList2);
				}
			}
		}
		bool flag6 = this.RankUiTabIndex == 2 && RankUICon.inst != null && RankUICon.inst.visible;
		if (flag6)
		{
			RankUICon.inst.ShowSkyladderRankList(num, nPage);
		}
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x0012A674 File Offset: 0x00128874
	public int GetMySkyladderRank(int nSeason)
	{
		int result = 0;
		bool flag = this.m_szMySkyRank.ContainsKey(nSeason);
		if (flag)
		{
			result = this.m_szMySkyRank[nSeason];
		}
		return result;
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x0012A6A8 File Offset: 0x001288A8
	public int GetSeasonLadderMaxNum(int nSeason)
	{
		int result = 0;
		bool flag = this.m_szSkyladderMaxSize.ContainsKey(nSeason);
		if (flag)
		{
			result = this.m_szSkyladderMaxSize[nSeason];
		}
		return result;
	}

	// Token: 0x06002090 RID: 8336 RVA: 0x0012A6DC File Offset: 0x001288DC
	public ArrayList GetSkyladderPageData(int nSeason, int nPage)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.m_szSkyladderRank.ContainsKey(nSeason);
		if (flag)
		{
			Dictionary<int, ArrayList> dictionary = this.m_szSkyladderRank[nSeason];
			for (int i = 0; i < 10; i++)
			{
				int key = (nPage - 1) * 10 + (i + 1);
				bool flag2 = dictionary.ContainsKey(key);
				if (flag2)
				{
					arrayList.Add((ArrayList)dictionary[key].Clone());
				}
			}
		}
		return arrayList;
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x0012A760 File Offset: 0x00128960
	public int IsShowSuperIcon(int nSeason, int nCid, int nType)
	{
		int num = 0;
		bool flag = !this.m_szSkyladderRank.ContainsKey(nSeason);
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			Dictionary<int, ArrayList> dictionary = this.m_szSkyladderRank[nSeason];
			bool flag2 = dictionary.Count == 0;
			if (flag2)
			{
				result = num;
			}
			else
			{
				for (int i = 1; i <= 100; i++)
				{
					bool flag3 = !dictionary.ContainsKey(i);
					if (!flag3)
					{
						ArrayList arrayList = dictionary[i];
						bool flag4 = !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey((int)arrayList[9]);
						if (!flag4)
						{
							SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[(int)arrayList[9]];
							bool flag5 = skyLadderData.szSkyLadderReward.ContainsKey(8) && (int)arrayList[0] == nCid && (int)arrayList[4] >= skyLadderData.szSkyLadderReward[8].nMinScore;
							if (flag5)
							{
								bool flag6 = nType == 0 && (int)arrayList[3] == 8;
								if (flag6)
								{
									num = (int)arrayList[8];
								}
								bool flag7 = nType == 1 && (int)arrayList[5] == 8;
								if (flag7)
								{
									num = (int)arrayList[8];
								}
							}
						}
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x0012A8EC File Offset: 0x00128AEC
	private void OnPveRankBack(ArrayList szMsg)
	{
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		string sDate = szMsg[1].ToString();
		bool flag = this.CheckNextDay(sDate, RankManager.GetInstance().sRankDate);
		if (!flag)
		{
			this.sRankDate = szMsg[1].ToString();
			ArrayList arrayList = (ArrayList)szMsg[2];
			int value = (int)szMsg[3];
			int num = (int)szMsg[4];
			int value2 = (int)szMsg[5];
			int num2 = (int)szMsg[6];
			int num3 = (int)szMsg[7];
			int key = (int)szMsg[8];
			bool flag2 = num3 == -1;
			if (flag2)
			{
				bool flag3 = this.RankUiTabIndex == 3 && RankUICon.inst != null;
				if (flag3)
				{
					RankUICon.inst.ShowPveRankNotRankOK();
				}
			}
			else
			{
				bool flag4 = this.m_szSortPveRank.ContainsKey(key);
				if (flag4)
				{
					dictionary = this.m_szSortPveRank[key];
					this.m_szPveRankMaxSize[key] = value2;
					this.m_szMyPveRank[key] = value;
				}
				else
				{
					this.m_szSortPveRank.Add(key, dictionary);
					this.m_szPveRankMaxSize.Add(key, value2);
					this.m_szMyPveRank.Add(key, value);
				}
				int count = arrayList.Count;
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					bool flag5 = i % 10 == 0;
					if (flag5)
					{
						bool flag6 = i > 0;
						if (flag6)
						{
							arrayList2 = new ArrayList();
						}
						int key2 = i / 10 + 1 + (num - 1) / 10 * 10;
						bool flag7 = dictionary.ContainsKey(key2);
						if (flag7)
						{
							dictionary[key2] = arrayList2;
						}
						else
						{
							dictionary.Add(key2, arrayList2);
						}
					}
					ArrayList arrayList3 = (ArrayList)arrayList[i];
					bool flag8 = arrayList3.Count > 0;
					if (flag8)
					{
						arrayList2.Add(arrayList3);
					}
				}
				this.m_szSortPveRank[key] = dictionary;
				bool flag9 = this.RankUiTabIndex == 3 && RankUICon.inst != null && num2 != 1;
				if (flag9)
				{
					RankUICon.inst.ShowPveRankList(num);
				}
			}
		}
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x0012AB4C File Offset: 0x00128D4C
	public int GetMaxPveSize(int nHeroType)
	{
		bool flag = !this.m_szPveRankMaxSize.ContainsKey(nHeroType);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_szPveRankMaxSize[nHeroType];
		}
		return result;
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x0012AB84 File Offset: 0x00128D84
	public ArrayList GetPveListByPage(int nPage, int nType)
	{
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		ArrayList arrayList = new ArrayList();
		bool flag = !this.m_szSortPveRank.ContainsKey(nType);
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			dictionary = this.m_szSortPveRank[nType];
			bool flag2 = dictionary.ContainsKey(nPage);
			if (flag2)
			{
				result = dictionary[nPage];
			}
			else
			{
				result = arrayList;
			}
		}
		return result;
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x0012ABE0 File Offset: 0x00128DE0
	public int GetMyPveRank(int nType)
	{
		bool flag = !this.m_szMyPveRank.ContainsKey(nType);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = ((this.m_szMyPveRank[nType] >= 0) ? (this.m_szMyPveRank[nType] + 1) : 0);
		}
		return result;
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x0012AC2C File Offset: 0x00128E2C
	public bool HadPvePage(int nPage, int nType)
	{
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		bool flag = !this.m_szSortPveRank.ContainsKey(nType);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			dictionary = this.m_szSortPveRank[nType];
			result = dictionary.ContainsKey(nPage);
		}
		return result;
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x0012AC70 File Offset: 0x00128E70
	public ArrayList GetDtsListByPage(int nPage)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.m_szSortDtsRank.ContainsKey(nPage);
		ArrayList result;
		if (flag)
		{
			result = this.m_szSortDtsRank[nPage];
		}
		else
		{
			result = arrayList;
		}
		return result;
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x0012ACAC File Offset: 0x00128EAC
	public bool HadDtsPage(int nPage)
	{
		return this.m_szSortDtsRank.ContainsKey(nPage);
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x0012ACCC File Offset: 0x00128ECC
	public int GetMaxDtsSize()
	{
		return this.m_nDtsRankMaxSize;
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x0012ACE4 File Offset: 0x00128EE4
	public int GetMyDtsRank()
	{
		bool flag = this.m_nMyDtsRank < 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_nMyDtsRank + 1;
		}
		return result;
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x0012AD10 File Offset: 0x00128F10
	private void OnDtsRankBack(ArrayList szMsg)
	{
		string sDate = szMsg[1].ToString();
		bool flag = this.CheckNextDay(sDate, RankManager.GetInstance().sRankDate);
		if (!flag)
		{
			this.sRankDate = szMsg[1].ToString();
			ArrayList arrayList = (ArrayList)szMsg[2];
			int nMyDtsRank = (int)szMsg[3];
			int num = (int)szMsg[4];
			this.m_nDtsRankMaxSize = (int)szMsg[5];
			int num2 = (int)szMsg[6];
			int num3 = (int)szMsg[7];
			bool flag2 = num3 == -1;
			if (flag2)
			{
				bool flag3 = this.RankUiTabIndex == 4 && RankUICon.inst != null;
				if (flag3)
				{
					RankUICon.inst.ShowDtsRankNotRankOK();
				}
			}
			else
			{
				this.m_nMyDtsRank = nMyDtsRank;
				int count = arrayList.Count;
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					bool flag4 = i % 10 == 0;
					if (flag4)
					{
						bool flag5 = i > 0;
						if (flag5)
						{
							arrayList2 = new ArrayList();
						}
						int key = i / 10 + 1 + (num - 1) / 10 * 10;
						bool flag6 = this.m_szSortDtsRank.ContainsKey(key);
						if (flag6)
						{
							this.m_szSortDtsRank[key] = arrayList2;
						}
						else
						{
							this.m_szSortDtsRank.Add(key, arrayList2);
						}
					}
					ArrayList arrayList3 = (ArrayList)arrayList[i];
					bool flag7 = arrayList3.Count > 0;
					if (flag7)
					{
						arrayList2.Add(arrayList3);
					}
				}
				bool flag8 = this.RankUiTabIndex == 4 && RankUICon.inst != null && num2 != 1;
				if (flag8)
				{
					RankUICon.inst.ShowDtsRankList(num);
				}
			}
		}
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x0012AEF0 File Offset: 0x001290F0
	public ArrayList GetFlowerListByPage(int nPage)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.m_szSortFlowerRank.ContainsKey(nPage);
		ArrayList result;
		if (flag)
		{
			result = this.m_szSortFlowerRank[nPage];
		}
		else
		{
			result = arrayList;
		}
		return result;
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x0012AF2C File Offset: 0x0012912C
	public bool HadFlowerPage(int nPage)
	{
		return this.m_szSortFlowerRank.ContainsKey(nPage);
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x0012AF4C File Offset: 0x0012914C
	public int GetMaxFlowerSize()
	{
		return this.m_nFlowerRankMaxSize;
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x0012AF64 File Offset: 0x00129164
	public int GetMyFlowerRank()
	{
		bool flag = this.m_nMyFlowerRank < 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.m_nMyFlowerRank + 1;
		}
		return result;
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x0012AF90 File Offset: 0x00129190
	private void OnFlowerRankBack(ArrayList szMsg)
	{
		string sDate = szMsg[1].ToString();
		bool flag = this.CheckNextDay(sDate, RankManager.GetInstance().sRankDate);
		if (!flag)
		{
			this.sRankDate = szMsg[1].ToString();
			ArrayList arrayList = (ArrayList)szMsg[2];
			int nMyFlowerRank = (int)szMsg[3];
			int num = (int)szMsg[4];
			this.m_nFlowerRankMaxSize = (int)szMsg[5];
			int num2 = (int)szMsg[6];
			int num3 = (int)szMsg[7];
			bool flag2 = num3 == -1;
			if (flag2)
			{
				bool flag3 = this.RankUiTabIndex == 5 && RankUICon.inst != null;
				if (flag3)
				{
					RankUICon.inst.ShowFlowerRankNotRankOK();
				}
			}
			else
			{
				this.m_nMyFlowerRank = nMyFlowerRank;
				int count = arrayList.Count;
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					bool flag4 = i % 10 == 0;
					if (flag4)
					{
						bool flag5 = i > 0;
						if (flag5)
						{
							arrayList2 = new ArrayList();
						}
						int key = i / 10 + 1 + (num - 1) / 10 * 10;
						bool flag6 = this.m_szSortFlowerRank.ContainsKey(key);
						if (flag6)
						{
							this.m_szSortFlowerRank[key] = arrayList2;
						}
						else
						{
							this.m_szSortFlowerRank.Add(key, arrayList2);
						}
					}
					ArrayList arrayList3 = (ArrayList)arrayList[i];
					bool flag7 = arrayList3.Count > 0;
					if (flag7)
					{
						arrayList2.Add(arrayList3);
					}
				}
				bool flag8 = this.RankUiTabIndex == 5 && RankUICon.inst != null && num2 != 1;
				if (flag8)
				{
					RankUICon.inst.ShowFlowerRankList(num);
				}
			}
		}
	}

	// Token: 0x04001D92 RID: 7570
	private static RankManager inst;

	// Token: 0x04001D93 RID: 7571
	public const int RANK_PAGE_NUM = 10;

	// Token: 0x04001D94 RID: 7572
	private int _RankUiTabIndex = -1;

	// Token: 0x04001D95 RID: 7573
	public string sRankDate = string.Empty;

	// Token: 0x04001D96 RID: 7574
	private Dictionary<int, ArrayList> m_szSortUserRank = new Dictionary<int, ArrayList>();

	// Token: 0x04001D97 RID: 7575
	private int m_nUserRankMaxSize;

	// Token: 0x04001D98 RID: 7576
	private int m_nMyUserRank;

	// Token: 0x04001D99 RID: 7577
	public Dictionary<int, Dictionary<int, ArrayList>> m_szAllSortHeroRank = new Dictionary<int, Dictionary<int, ArrayList>>();

	// Token: 0x04001D9A RID: 7578
	private Dictionary<int, int> m_szHeroRankMaxSize = new Dictionary<int, int>();

	// Token: 0x04001D9B RID: 7579
	private Dictionary<int, int> m_szMyHeroRank = new Dictionary<int, int>();

	// Token: 0x04001D9C RID: 7580
	public Dictionary<int, Dictionary<int, ArrayList>> m_szSkyladderRank = new Dictionary<int, Dictionary<int, ArrayList>>();

	// Token: 0x04001D9D RID: 7581
	private Dictionary<int, int> m_szSkyladderMaxSize = new Dictionary<int, int>();

	// Token: 0x04001D9E RID: 7582
	private Dictionary<int, int> m_szMySkyRank = new Dictionary<int, int>();

	// Token: 0x04001D9F RID: 7583
	private Dictionary<int, Dictionary<int, ArrayList>> m_szSortPveRank = new Dictionary<int, Dictionary<int, ArrayList>>();

	// Token: 0x04001DA0 RID: 7584
	private Dictionary<int, int> m_szPveRankMaxSize = new Dictionary<int, int>();

	// Token: 0x04001DA1 RID: 7585
	private Dictionary<int, int> m_szMyPveRank = new Dictionary<int, int>();

	// Token: 0x04001DA2 RID: 7586
	private Dictionary<int, ArrayList> m_szSortDtsRank = new Dictionary<int, ArrayList>();

	// Token: 0x04001DA3 RID: 7587
	private int m_nDtsRankMaxSize;

	// Token: 0x04001DA4 RID: 7588
	private int m_nMyDtsRank;

	// Token: 0x04001DA5 RID: 7589
	private Dictionary<int, ArrayList> m_szSortFlowerRank = new Dictionary<int, ArrayList>();

	// Token: 0x04001DA6 RID: 7590
	private int m_nFlowerRankMaxSize;

	// Token: 0x04001DA7 RID: 7591
	private int m_nMyFlowerRank;
}

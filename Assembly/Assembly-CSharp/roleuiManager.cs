using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class roleuiManager
{
	// Token: 0x0600219D RID: 8605 RVA: 0x00138B3C File Offset: 0x00136D3C
	public static roleuiManager GetInstance()
	{
		bool flag = roleuiManager.instance == null;
		if (flag)
		{
			roleuiManager.instance = new roleuiManager();
		}
		return roleuiManager.instance;
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x00138B6C File Offset: 0x00136D6C
	public static HeroData getHeroData(int nHid)
	{
		bool flag = fGameManager.mloadConfig.AllHeroList.ContainsKey(nHid);
		HeroData result;
		if (flag)
		{
			result = fGameManager.mloadConfig.AllHeroList[nHid];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x00138BA8 File Offset: 0x00136DA8
	public void OpenSelectHeroUI(int nOpenType)
	{
		bool isSingle = fGameManager.IsSingle;
		if (!isSingle)
		{
			bool flag = SelectHeroUICon.inst == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("SelectHeroUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.loadselecthero), null, true);
			}
			else
			{
				fUISoundManager.instance.StopSound("zhujiemian_beijingyinyue", 0);
				fUISoundManager.instance.StopSound("music_commen", 0);
				SelectHeroUICon.inst.Open();
				this.OnOpenHeroUI(string.Empty);
			}
			bool flag2 = MatchManager.GetInstance().m_nIsChangeHero != 1;
			if (!flag2)
			{
				FbXmlData pFbData = MatchManager.GetInstance().m_pFbData;
				bool flag3 = pFbData != null && !fUISoundManager.instance.SoundIsPlay(pFbData.selectsound);
				if (flag3)
				{
					fUISoundManager.instance.PlayUISound(pFbData.selectsound, 1, null, 10);
				}
				bool flag4 = pFbData.nType < 3;
				if (flag4)
				{
					int playerTeam = this.GetPlayerTeam();
					bool flag5 = playerTeam == 1;
					if (flag5)
					{
						PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1369];
						fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
					}
					bool flag6 = playerTeam == 2;
					if (flag6)
					{
						PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1368];
						fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
					}
				}
				else
				{
					PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[1369];
					fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
				}
			}
		}
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x00138D48 File Offset: 0x00136F48
	public int GetPlayerTeam()
	{
		string meName = this.GetMeName();
		UserData userData = this.GetUserData(meName);
		return (userData != null) ? userData.m_nShili : 0;
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x00138D78 File Offset: 0x00136F78
	private void loadselecthero(string sUI, GameObject ob, object szParam)
	{
		fGameManager.m_nJoinGame = "选择英雄中";
		fUISoundManager.instance.StopSound("zhujiemian_beijingyinyue", 0);
		fUISoundManager.instance.StopSound("music_commen", 0);
		fUIManager.GetInstance().ProgressChange(false, "LoadingGamePlayer", 1f, 1f);
		SelectHeroUICon.inst.InitUI();
		this.OnOpenHeroUI(string.Empty);
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x00138DE4 File Offset: 0x00136FE4
	public void OnOpenHeroUI(string sName)
	{
		fUIManager.SetProgreessShow(false);
		HeroPlayer.isPlayerControl = false;
		HeroPlayer.isMouseShow = true;
		SelectHeroUICon.inst.show(sName);
		bool flag = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag)
		{
			VoiceUICon.inst.Close();
		}
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x00138E3C File Offset: 0x0013703C
	public void OnCloseSelectHeroUI()
	{
		bool flag = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible && fGameManager.IsJoinGame;
		if (flag)
		{
			SelectHeroUICon.inst.Close();
		}
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x00138E7C File Offset: 0x0013707C
	public ArrayList GetHeroList(int nHeroType)
	{
		Dictionary<int, ArrayList> heroCareerList = fGameManager.mloadConfig.HeroCareerList;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		bool flag = nHeroType == 0;
		if (flag)
		{
			foreach (int num in fGameManager.mloadConfig.AllHeroList.Keys)
			{
				arrayList.Add(num);
			}
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_nMatchType == 3;
			if (flag2)
			{
				arrayList = PveManager.GetInstance().GetHeroList(nHeroType);
			}
			else
			{
				bool flag3 = heroCareerList.ContainsKey(nHeroType);
				if (flag3)
				{
					arrayList = heroCareerList[nHeroType];
				}
			}
		}
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			int num2 = (int)arrayList[i];
			bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(num2);
			if (flag4)
			{
				arrayList2.Add(num2);
			}
		}
		return arrayList2;
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x00138FA4 File Offset: 0x001371A4
	public string GetMeName()
	{
		string text = string.Empty;
		text = PlayerDataManager.getInstance().playerCommon.m_sName;
		bool flag = text == null;
		if (flag)
		{
			text = PlayerDataManager.getInstance().HUserData.m_sName;
		}
		return text;
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x00138FE8 File Offset: 0x001371E8
	public ArrayList GetGameUserList(int nTeamType, int nType = 0)
	{
		string text = string.Empty;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int playerTeam = this.GetPlayerTeam();
		bool flag = nTeamType == 0;
		if (flag)
		{
			text = this.GetMeName();
			bool flag2 = !arrayList2.Contains(text);
			if (flag2)
			{
				arrayList2.Add(text);
			}
		}
		arrayList = this.GetAllUserData();
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			string text2 = arrayList[i].ToString();
			UserData userData = this.GetUserData(text2);
			bool flag3 = userData != null && (nType != 1 || userData.m_nIsChsok == 1) && (nTeamType != 0 || (!(text == text2) && userData.m_nShili == playerTeam)) && (nTeamType != 1 || userData.m_nShili != playerTeam) && !arrayList2.Contains(text2);
			if (flag3)
			{
				arrayList2.Add(text2);
			}
		}
		return this.GetTeamSort(arrayList2);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x001390E0 File Offset: 0x001372E0
	private ArrayList GetTeamSort(ArrayList szData)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList c = new ArrayList();
		int count = szData.Count;
		bool flag = count < 1;
		ArrayList result;
		if (flag)
		{
			result = szData;
		}
		else
		{
			arrayList2 = this.GetTeamIDList(szData);
			for (int i = 0; i < count; i++)
			{
				string text = szData[i].ToString();
				bool flag2 = !arrayList.Contains(text);
				if (flag2)
				{
					int teamID = this.GetTeamID(text);
					int num = arrayList2.IndexOf(teamID);
					bool flag3 = num != -1;
					if (flag3)
					{
						c = (ArrayList)arrayList2[num + 1];
						arrayList.AddRange(c);
					}
				}
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x001391A4 File Offset: 0x001373A4
	public int GetTeamID(string sUserName)
	{
		int num = 0;
		UserData userData = roleuiManager.GetInstance().GetUserData(sUserName);
		return (userData != null) ? userData.m_nTeamID : num;
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x001391D4 File Offset: 0x001373D4
	public int GetIsChsok(string sUserName)
	{
		int num = 0;
		UserData userData = roleuiManager.GetInstance().GetUserData(sUserName);
		return (userData != null) ? userData.m_nIsChsok : num;
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x00139204 File Offset: 0x00137404
	public ArrayList GetTeamIDList(ArrayList szData)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int count = szData.Count;
		for (int i = 0; i < count; i++)
		{
			string text = szData[i].ToString();
			UserData userData = this.GetUserData(text);
			bool flag = userData == null;
			if (!flag)
			{
				int nTeamID = userData.m_nTeamID;
				int num = arrayList.IndexOf(nTeamID);
				bool flag2 = num == -1;
				if (flag2)
				{
					arrayList.Add(nTeamID);
					arrayList.Add(new ArrayList
					{
						text
					});
				}
				else
				{
					arrayList2 = (ArrayList)arrayList[num + 1];
					bool flag3 = !arrayList2.Contains(text);
					if (flag3)
					{
						arrayList2.Add(text);
						arrayList[num + 1] = arrayList2;
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x001392F8 File Offset: 0x001374F8
	public UserData GetNowUserData()
	{
		string meName = this.GetMeName();
		return this.GetUserData(meName);
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x0013931C File Offset: 0x0013751C
	public ArrayList GetTeamSelectOkHero()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList gameUserList = this.GetGameUserList(0, 0);
		int count = gameUserList.Count;
		string meName = this.GetMeName();
		for (int i = 0; i < count; i++)
		{
			string text = gameUserList[i].ToString();
			bool flag = text == string.Empty || meName == text;
			if (!flag)
			{
				UserData userData = this.GetUserData(text);
				bool flag2 = userData != null;
				if (flag2)
				{
					int nIsChsok = userData.m_nIsChsok;
					bool flag3 = nIsChsok == 1;
					if (flag3)
					{
						int nHeroID = userData.m_nHeroID;
						arrayList.Add(nHeroID);
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x001393E0 File Offset: 0x001375E0
	public UserData GetUserData(string sName)
	{
		return MatchManager.GetInstance().GetUserData(sName);
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x00139400 File Offset: 0x00137600
	public ArrayList GetAllUserData()
	{
		ArrayList arrayList = new ArrayList();
		return MatchManager.GetInstance().GetAllUserData();
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x00139424 File Offset: 0x00137624
	public int GetSelectModeType(int type)
	{
		return type;
	}

	// Token: 0x04001F05 RID: 7941
	public static roleuiManager instance;

	// Token: 0x04001F06 RID: 7942
	public static int nUiOpenFrom;

	// Token: 0x04001F07 RID: 7943
	private ArrayList arrHeroPam;

	// Token: 0x04001F08 RID: 7944
	public bool bOtherUiVisable;
}

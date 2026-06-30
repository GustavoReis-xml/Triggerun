using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000140 RID: 320
public class GameTabUICon : Window
{
	// Token: 0x06000D6A RID: 3434 RVA: 0x00080D54 File Offset: 0x0007EF54
	protected override void Awake()
	{
		base.Awake();
		GameTabUICon.Inst = this;
		this.nametext = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.gametime = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.userdata = this.UIGameObj.Get_GameObj(2);
		this.herodata = this.UIGameObj.Get_GameObj(3);
		this.cloneuser = this.UIGameObj.Get_GameObj(5);
		this.cloneuser.SetActive(false);
		this.youfanglist = this.UIGameObj.Get_GameObj(6);
		this.difanglist = this.UIGameObj.Get_GameObj(7);
		this.youfangEffect = this.UIGameObj.Get_GameObj(8);
		this.youfangEffect.SetActive(false);
		this.difangEffect = this.UIGameObj.Get_GameObj(9);
		this.difangEffect.SetActive(false);
		this.oXiaoTong = this.UIGameObj.Get_GameObj(10);
		this.oXiaoTong.SetActive(false);
		this.oXiaoYin = this.UIGameObj.Get_GameObj(11);
		this.oXiaoYin.SetActive(false);
		this.oXiaoJin = this.UIGameObj.Get_GameObj(12);
		this.oXiaoJin.SetActive(false);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00080EA4 File Offset: 0x0007F0A4
	public void OnShowWin()
	{
		this.nametext.text = MatchManager.GetInstance().m_pFbData.sName;
		int nowGameTimeLong = this.getNowGameTimeLong();
		bool flag = nowGameTimeLong > 0;
		if (flag)
		{
			this.gametime.text = Fun.sencondsToHMS_CXP(nowGameTimeLong);
			base.CancelInvoke("OnGameDjsTime");
			base.InvokeRepeating("OnGameDjsTime", 0f, 1f);
		}
		this.OnShowUserData();
		this.OnShowHeroData();
		this.OnShowTeamData();
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00080F28 File Offset: 0x0007F128
	private void OnGameDjsTime()
	{
		int nowGameTimeLong = this.getNowGameTimeLong();
		this.gametime.text = Fun.sencondsToHMS_CXP(nowGameTimeLong);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00080F50 File Offset: 0x0007F150
	private int getNowGameTimeLong()
	{
		DateTime dGameStartTime = MatchManager.GetInstance().m_dGameStartTime;
		return (int)(Fun.GetServerDate() - dGameStartTime).TotalSeconds;
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00080F84 File Offset: 0x0007F184
	private ArrayList GetTabInfo(int nType)
	{
		ArrayList result = new ArrayList();
		ArrayList szTabWinInfo = FBManager.GetInstance().szTabWinInfo;
		bool flag = nType < szTabWinInfo.Count;
		if (flag)
		{
			result = (ArrayList)szTabWinInfo[nType];
		}
		return result;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00080FC4 File Offset: 0x0007F1C4
	private void OnShowUserData()
	{
		ArrayList tabInfo = this.GetTabInfo(0);
		ArrayList tabInfo2 = this.GetTabInfo(2);
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		this.userdata.transform.Find("name").GetComponent<Text>().text = player.playerCommon.m_sName;
		bool flag = tabInfo.Count > 0;
		if (flag)
		{
			GameObject gameObject = this.userdata.transform.Find("xm/hzbg").gameObject;
			this.getNowRankHz((int)tabInfo2[0], gameObject);
			this.userdata.transform.Find("xm/value").GetComponent<Text>().text = tabInfo[0].ToString();
			this.userdata.transform.Find("xm/Text").GetComponent<Text>().text = Language.GetLang(4002, new object[0]);
			gameObject = this.userdata.transform.Find("mbgfxm/hzbg").gameObject;
			this.getNowRankHz((int)tabInfo2[1], gameObject);
			this.userdata.transform.Find("mbgfxm/value").GetComponent<Text>().text = tabInfo[1].ToString();
			this.userdata.transform.Find("mbgfxm/Text").GetComponent<Text>().text = Language.GetLang(4003, new object[0]);
			gameObject = this.userdata.transform.Find("gfsj/hzbg").gameObject;
			this.getNowRankHz((int)tabInfo2[2], gameObject);
			this.userdata.transform.Find("gfsj/value").GetComponent<Text>().text = Fun.sencondsToHMS_CXP(int.Parse(tabInfo[2].ToString()));
			this.userdata.transform.Find("gfsj/Text").GetComponent<Text>().text = Language.GetLang(4004, new object[0]);
			gameObject = this.userdata.transform.Find("shl/hzbg").gameObject;
			this.getNowRankHz((int)tabInfo2[3], gameObject);
			this.userdata.transform.Find("shl/value").GetComponent<Text>().text = tabInfo[3].ToString();
			this.userdata.transform.Find("shl/Text").GetComponent<Text>().text = Language.GetLang(4005, new object[0]);
			gameObject = this.userdata.transform.Find("zll/hzbg").gameObject;
			this.getNowRankHz((int)tabInfo2[4], gameObject);
			this.userdata.transform.Find("zll/value").GetComponent<Text>().text = tabInfo[4].ToString();
			this.userdata.transform.Find("zll/Text").GetComponent<Text>().text = Language.GetLang(4006, new object[0]);
			this.userdata.transform.Find("zw/value").GetComponent<Text>().text = tabInfo[5].ToString();
			this.userdata.transform.Find("zw/Text").GetComponent<Text>().text = Language.GetLang(4007, new object[0]);
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00081354 File Offset: 0x0007F554
	private void getNowRankHz(int nRank, GameObject oHz)
	{
		GameObject gameObject = null;
		switch (nRank)
		{
		case 0:
			gameObject = this.oXiaoJin;
			break;
		case 1:
			gameObject = this.oXiaoYin;
			break;
		case 2:
			gameObject = this.oXiaoTong;
			break;
		}
		bool flag = oHz.transform.childCount > 0;
		if (flag)
		{
			Fun.ClearChildObejct(oHz);
		}
		bool flag2 = !(gameObject == null);
		if (flag2)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(oHz.transform);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, -2f);
			gameObject2.transform.localScale = new Vector3(57f, 57f, 57f);
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.SetActive(true);
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00081440 File Offset: 0x0007F640
	private void OnShowHeroData()
	{
		string text = string.Empty;
		ArrayList tabInfo = this.GetTabInfo(1);
		bool flag = tabInfo.Count > 0;
		if (flag)
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[(int)tabInfo[0]];
			bool flag2 = heroData != null;
			if (flag2)
			{
				text = heroData.hname;
			}
			this.herodata.transform.Find("name").GetComponent<Text>().text = text;
			this.herodata.transform.Find("xm/value").GetComponent<Text>().text = tabInfo[1].ToString();
			this.herodata.transform.Find("xm/Text").GetComponent<Text>().text = Language.GetLang(4002, new object[0]);
			this.herodata.transform.Find("mbgfxm/value").GetComponent<Text>().text = tabInfo[2].ToString();
			this.herodata.transform.Find("mbgfxm/Text").GetComponent<Text>().text = Language.GetLang(4003, new object[0]);
			this.herodata.transform.Find("gfsj/value").GetComponent<Text>().text = Fun.sencondsToHMS_CXP(int.Parse(tabInfo[3].ToString()));
			this.herodata.transform.Find("gfsj/Text").GetComponent<Text>().text = Language.GetLang(4004, new object[0]);
			this.herodata.transform.Find("shl/value").GetComponent<Text>().text = tabInfo[4].ToString();
			this.herodata.transform.Find("shl/Text").GetComponent<Text>().text = Language.GetLang(4005, new object[0]);
			this.herodata.transform.Find("zll/value").GetComponent<Text>().text = tabInfo[5].ToString();
			this.herodata.transform.Find("zll/Text").GetComponent<Text>().text = Language.GetLang(4006, new object[0]);
			this.herodata.transform.Find("zw/value").GetComponent<Text>().text = tabInfo[6].ToString();
			this.herodata.transform.Find("zw/Text").GetComponent<Text>().text = Language.GetLang(4007, new object[0]);
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x000816F0 File Offset: 0x0007F8F0
	private void OnShowTeamData()
	{
		int num = 5;
		ArrayList gameUserList = roleuiManager.GetInstance().GetGameUserList(0, 0);
		int count = gameUserList.Count;
		for (int i = 0; i < num; i++)
		{
			Transform transform = this.youfanglist.transform.Find(i.ToString());
			bool flag = transform == null;
			GameObject gameObject;
			if (flag)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cloneuser);
				gameObject.transform.SetParent(this.youfanglist.transform);
				gameObject.transform.localPosition = new Vector3((float)i * 250f - 500f, 0f, 0f);
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = i.ToString();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			string text = string.Empty;
			int num2 = 0;
			bool mSuo = false;
			bool flag2 = i < count;
			if (flag2)
			{
				text = gameUserList[i].ToString();
				num2 = roleuiManager.GetInstance().GetTeamID(text);
			}
			bool flag3 = num2 != 0 && i + 1 < count;
			if (flag3)
			{
				string sUserName = gameUserList[i + 1].ToString();
				int teamID = roleuiManager.GetInstance().GetTeamID(sUserName);
				bool flag4 = num2 == teamID;
				if (flag4)
				{
					mSuo = true;
				}
			}
			OneTabUIData oneTabUIData = gameObject.GetComponent<OneTabUIData>();
			bool flag5 = oneTabUIData == null;
			if (flag5)
			{
				oneTabUIData = gameObject.AddComponent<OneTabUIData>();
			}
			oneTabUIData.OnSetUI(text, 0, this.youfangEffect, mSuo);
		}
		gameUserList = roleuiManager.GetInstance().GetGameUserList(1, 0);
		count = gameUserList.Count;
		for (int j = 0; j < num; j++)
		{
			Transform transform = this.difanglist.transform.Find(j.ToString());
			bool flag6 = transform == null;
			GameObject gameObject;
			if (flag6)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cloneuser);
				gameObject.transform.SetParent(this.difanglist.transform);
				gameObject.transform.localPosition = new Vector3((float)j * 250f - 500f, 0f, 0f);
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = j.ToString();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			string text2 = string.Empty;
			int num3 = 0;
			bool mSuo = false;
			bool flag7 = j < count;
			if (flag7)
			{
				text2 = gameUserList[j].ToString();
				num3 = roleuiManager.GetInstance().GetTeamID(text2);
			}
			bool flag8 = num3 != 0 && j + 1 < count;
			if (flag8)
			{
				string sUserName2 = gameUserList[j + 1].ToString();
				int teamID2 = roleuiManager.GetInstance().GetTeamID(sUserName2);
				bool flag9 = num3 == teamID2;
				if (flag9)
				{
					mSuo = true;
				}
			}
			OneTabUIData oneTabUIData = gameObject.GetComponent<OneTabUIData>();
			bool flag10 = oneTabUIData == null;
			if (flag10)
			{
				oneTabUIData = gameObject.AddComponent<OneTabUIData>();
			}
			oneTabUIData.OnSetUI(text2, 1, this.difangEffect, mSuo);
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00081A4D File Offset: 0x0007FC4D
	public override void Close()
	{
		base.Close();
		base.CancelInvoke("OnGameDjsTime");
	}

	// Token: 0x04000B0D RID: 2829
	public static GameTabUICon Inst;

	// Token: 0x04000B0E RID: 2830
	private Text nametext;

	// Token: 0x04000B0F RID: 2831
	private Text gametime;

	// Token: 0x04000B10 RID: 2832
	private GameObject userdata;

	// Token: 0x04000B11 RID: 2833
	private GameObject herodata;

	// Token: 0x04000B12 RID: 2834
	private GameObject cloneuser;

	// Token: 0x04000B13 RID: 2835
	private GameObject youfanglist;

	// Token: 0x04000B14 RID: 2836
	private GameObject difanglist;

	// Token: 0x04000B15 RID: 2837
	private GameObject youfangEffect;

	// Token: 0x04000B16 RID: 2838
	private GameObject difangEffect;

	// Token: 0x04000B17 RID: 2839
	private GameObject oXiaoTong;

	// Token: 0x04000B18 RID: 2840
	private GameObject oXiaoYin;

	// Token: 0x04000B19 RID: 2841
	private GameObject oXiaoJin;
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class gameuiCon : Window
{
	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00081A6C File Offset: 0x0007FC6C
	public NetLiving Net
	{
		get
		{
			return this._NetLiving;
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00081A74 File Offset: 0x0007FC74
	protected override void Awake()
	{
		base.Awake();
		this.oCenterZhunXing = this.UIGameObj.Get_GameObj(0);
		this.mainHeroZhunXing = this.oCenterZhunXing.AddComponent<HeroZhunXingUiCon>();
		this.oRightHpUI = this.UIGameObj.Get_GameObj(1);
		this.NewMainHp = this.oRightHpUI.AddComponent<NewHeroHpUi>();
		this.oRightSkillUI = this.UIGameObj.Get_GameObj(2);
		this.NewMainHeroSkill = this.oRightSkillUI.AddComponent<NewHeroSkillUi>();
		this.oCenterDaZhao = this.UIGameObj.Get_GameObj(3);
		this.NewMainDaZhao = this.oCenterDaZhao.AddComponent<NewHeroDaZhaoUi>();
		this.oDeadUI = this.UIGameObj.Get_GameObj(4);
		this.mainDead = this.oDeadUI.AddComponent<HeroDeadUiCon>();
		this.oMsgUI = this.UIGameObj.Get_GameObj(5);
		this.mainMsgUI = this.oMsgUI.AddComponent<HeroGameMsgCon>();
		this.oRoundEnd = this.UIGameObj.Get_GameObj(6);
		this.mainRoundUI = this.oRoundEnd.AddComponent<HeroRoundEndCon>();
		this.oBody = this.UIGameObj.Get_GameObj(7);
		this.oBody.SetActive(false);
		gameuiCon.inst = this;
		SkyLadderManager.GetInstance().ShowSkyLadderTip();
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00081BB0 File Offset: 0x0007FDB0
	private void Update()
	{
		this.CheckOpenDeadUI();
		bool flag = this._NetLiving != null && !(this.oBody == null);
		if (flag)
		{
			bool flag2 = !this.oBody.activeSelf;
			if (flag2)
			{
				this.oBody.SetActive(true);
			}
			this.CheckTabUI();
			this.CheckChangeHero();
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00081C14 File Offset: 0x0007FE14
	private void CheckTabUI()
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType != 10 && MatchManager.GetInstance().m_pFbData.nType != 11;
		if (flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("Score_Window");
			if (flag2)
			{
				fUIManager.GetInstance().GetTabData();
			}
			bool flag3 = Input_Manager.Get_Button_Up("Score_Window");
			if (flag3)
			{
				fUIManager.GetInstance().CloseTabWin();
			}
		}
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00081C8C File Offset: 0x0007FE8C
	private void CheckChangeHero()
	{
		bool flag = this._NetLiving == null;
		if (!flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("Replace_Hero");
			if (flag2)
			{
				ChatManager.GetInstance().OnCloseHuDongWin();
				bool flag3 = ChatManager.ChatInput || (Setting_OptionCon.Instance != null && Setting_OptionCon.Instance.visible);
				if (flag3)
				{
					return;
				}
				bool flag4 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
				if (flag4)
				{
					SelectHeroUICon.inst.Close();
					return;
				}
				bool flag5 = MatchManager.GetInstance().m_pFbData.nType == 10 || this.mainRoundUI.mboolstat;
				if (flag5)
				{
					return;
				}
				bool flag6 = KingTrainManager.GetInstance().GetGameIsReady() == 1 || MatchManager.GetInstance().m_pFbData.nType == 6 || fGameManager.Heroplayer.playerCommon.m_nInJidi == 1 || fUIManager.GetInstance().Arena_Score_Death_Change_Hero() || (MatchManager.GetInstance().m_pFbData.nType == 11 && fGameManager.Heroplayer.isDead);
				if (flag6)
				{
					roleuiManager.GetInstance().OpenSelectHeroUI(1);
				}
			}
			bool keyDown = Input.GetKeyDown(KeyCode.Escape);
			if (keyDown)
			{
				roleuiManager.GetInstance().OnCloseSelectHeroUI();
				bool flag7 = GameResultUICon.Inst != null && GameResultUICon.Inst.visible;
				if (flag7)
				{
					GameResultUICon.Inst.OnCloseUi(null);
				}
			}
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00081E11 File Offset: 0x00080011
	public void clear(int nType)
	{
		this.mainMsgUI.clear(nType);
		this.mainDead.Close();
		this.NewMainHeroSkill.GunInit(nType);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x00081E3C File Offset: 0x0008003C
	public void showHeroUi(NetLiving _netObj, int nOpenType)
	{
		this._NetLiving = _netObj;
		bool flag = _netObj == null || _netObj.netobject_type == 2;
		if (!flag)
		{
			int nModelIndex = this._NetLiving.playerCommon.m_nModelIndex;
			this.heroSelectData = roleuiManager.getHeroData(nModelIndex);
			bool flag2 = this.heroSelectData == null;
			if (flag2)
			{
				Debug.LogError("还没有选择英雄就进入游戏或选择的英雄配表中没有" + nModelIndex.ToString());
			}
			else
			{
				this.mainHeroZhunXing.Refresh(this._NetLiving);
				this.NewMainHp.Refresh(this._NetLiving);
				this.NewMainDaZhao.Refresh(this._NetLiving);
				this.NewMainHeroSkill.OnChangeWeaponGroup(this._NetLiving);
				this.NewMainHeroSkill.showSkillUi(this._NetLiving);
				this.mainMsgUI.OnUpdateNetLiving(this._NetLiving);
				bool flag3 = nOpenType == 1;
				if (flag3)
				{
					this.mainMsgUI.OnLookHeroTextEffect();
					bool flag4 = KingCon.inst != null && KingCon.inst.visible;
					if (flag4)
					{
						KingCon.inst.ShowHighEffTip();
					}
				}
				this.mainDead.OnUpdateNetLiving(this._NetLiving);
			}
		}
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00081F72 File Offset: 0x00080172
	private void OnDisable()
	{
		this.clear(0);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00081F80 File Offset: 0x00080180
	public void OnCheckHeadHpData(NetLiving oPlayer, NetLiving netobj)
	{
		bool flag = true;
		bool flag2 = oPlayer != null;
		if (flag2)
		{
			string sHead = string.Empty;
			bool flag3 = oPlayer.heroBase.Release_Musth_State && oPlayer.heroBase.heroClassName.Contains("YukiOnna") && netobj != null;
			if (flag3)
			{
				sHead = "npc_1013";
				flag = false;
				this.mYukiOnna = true;
			}
			else
			{
				netobj = fGameManager.GetInstance().getPlayer();
				sHead = "male_1";
				bool flagShowMale = MatchManager.GetInstance().m_nMatchType == 4 && netobj.playerCommon.m_nModelIndex == 2000;
				if (!flagShowMale)
				{
					SkinUnlock useSkinData = HeroDataManager.GetInstance().GetUseSkinData(netobj.playerCommon.m_nModelIndex, netobj.playerCommon.m_nCid);
					if (useSkinData != null)
					{
						sHead = useSkinData.sIconName;
					}
					else if (fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(netobj.playerCommon.m_nModelIndex))
					{
						sHead = fGameManager.mloadConfig.AllHeroUnlock[netobj.playerCommon.m_nModelIndex].szSkinUnlock[1].sIconName;
					}
				}
				this.mYukiOnna = false;
			}
			bool flag4 = this.oCenterZhunXing.transform.Find("show/common").gameObject.activeSelf != flag;
			if (flag4)
			{
				this.oCenterZhunXing.transform.Find("show/common").gameObject.SetActive(flag);
			}
			this.NewMainHp.UpdateHpObj(netobj, sHead);
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000820A8 File Offset: 0x000802A8
	private void CheckOpenDeadUI()
	{
		bool flag = this.mainDead == null || fGameManager.Heroplayer == null || MatchManager.GetInstance().m_pFbData.nType == 10;
		if (!flag)
		{
			bool flag2 = fGameManager.Heroplayer.isDead && !this.mYukiOnna;
			if (flag2)
			{
				bool flag3 = !this.mainDead._DeadWin.activeSelf && !this.mainDead.mOpenBool;
				if (flag3)
				{
					this.mainDead.OpenDeadWin();
					bool flag4 = MatchManager.GetInstance().m_nMatchType == 3 && KingCon.inst != null;
					if (flag4)
					{
						KingCon.inst.OnKingDead(true);
					}
				}
			}
			else
			{
				bool mOpenBool = this.mainDead.mOpenBool;
				if (mOpenBool)
				{
					this.mainDead.mOpenBool = false;
				}
				bool activeSelf = this.mainDead._DeadWin.activeSelf;
				if (activeSelf)
				{
					this.mainDead.Close();
				}
			}
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000821B4 File Offset: 0x000803B4
	public void OpenRoundEndUI(ArrayList szData)
	{
		bool flag = !(this.mainRoundUI == null) || this._NetLiving != null;
		if (flag)
		{
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
			fUIManager.GetInstance().CloseTabWin();
			this.mainRoundUI.OnSetUI(szData);
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00082208 File Offset: 0x00080408
	public void CloseRoundEndUI()
	{
		bool flag = this.mainDead != null && this.mainDead._DeadWin.activeSelf;
		if (flag)
		{
			this.mainDead.Close();
		}
		bool flag2 = (!(this.mainRoundUI == null) || this._NetLiving != null) && this.mainRoundUI.mboolstat;
		if (flag2)
		{
			this.mainRoundUI.Close();
		}
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00082280 File Offset: 0x00080480
	public void CloseResultSence()
	{
		bool flag = GameShouUICon.Inst != null;
		if (flag)
		{
			GameShouUICon.Inst.Close();
		}
		bool flag2 = !fUIManager.mGameUI.activeSelf;
		if (flag2)
		{
			fUIManager.mGameUI.SetActive(true);
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x000822CC File Offset: 0x000804CC
	public void OnLoadResultSence()
	{
		string text = string.Empty;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		arrayList2 = this.GetMeFbResultData();
		bool flag = arrayList2.Count < 1;
		if (flag)
		{
			fUIManager.GetInstance().CloseTabWin();
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
			this.CloseRoundEndUI();
			fUIManager.GetInstance().GotoHall(0);
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8023, new object[0]), string.Empty, string.Empty, string.Empty, null, null, null, null);
			Debug.Log("打开结算展示界面时没有自己的结算数据--名称:" + roleuiManager.instance.GetMeName() + "--时间:" + Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"));
		}
		else
		{
			bool flag2 = !fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
			if (flag2)
			{
				fUIManager.GetInstance().ShowGameResultUI();
				Debug.Log(string.Concat(new string[]
				{
					"打开结算展示界面时没有场景数据--名称:",
					roleuiManager.instance.GetMeName(),
					"--时间:",
					Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"),
					"--nNowScnID:",
					fGameManager.GetInstance().nNowScnID.ToString()
				}));
			}
			else
			{
				FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
				text = fbXmlData.sTeamshowbg.ToLower();
				bool flag3 = text == string.Empty || text == "0";
				if (flag3)
				{
					fUIManager.GetInstance().ShowGameResultUI();
					Debug.Log(string.Concat(new string[]
					{
						"打开结算展示界面时没有场景数据--名称:",
						roleuiManager.instance.GetMeName(),
						"--时间:",
						Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"),
						"--nNowScnID:",
						fGameManager.GetInstance().nNowScnID.ToString(),
						"--sUIName:",
						text
					}));
				}
				else
				{
					bool flag4 = fbXmlData.nType == 9 || fbXmlData.nType == 12;
					if (flag4)
					{
						fUIManager.GetInstance().ShowGameResultUI();
					}
					else
					{
						bool flag5 = arrayList2.Count > 0 && (int)arrayList2[5] == 0 && (int)arrayList2[6] == 0 && (int)arrayList2[7] == 0;
						if (flag5)
						{
							fUIManager.GetInstance().ShowGameResultUI();
							Debug.Log(string.Concat(new object[]
							{
								"结算没有结果时不打开结算界面--名称:",
								roleuiManager.instance.GetMeName(),
								"--时间:",
								Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"),
								"--胜利:",
								arrayList2[5],
								"--失败:",
								arrayList2[6],
								"--平局:",
								arrayList2[7]
							}));
						}
						else
						{
							arrayList = this.GetResultMvpData();
							bool flag6 = arrayList.Count < 1;
							if (flag6)
							{
								fUIManager.GetInstance().ShowGameResultUI();
								Debug.Log("结算没有找到mvp玩家--名称:" + roleuiManager.instance.GetMeName() + "--时间:" + Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"));
							}
							else
							{
								fUIManager.GetInstance().openMvpShowUi(arrayList, text);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0008265C File Offset: 0x0008085C
	private ArrayList GetResultMvpData()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList();
		ArrayList arrayList5 = new ArrayList();
		ArrayList result = new ArrayList();
		int nTeamType = 1;
		arrayList = this.GetMeFbResultData();
		bool flag = arrayList.Count > 0;
		if (flag)
		{
			bool flag2 = (int)arrayList[5] == 1;
			if (flag2)
			{
				nTeamType = 0;
			}
			else
			{
				bool flag3 = (int)arrayList[7] == 1;
				if (flag3)
				{
					nTeamType = -1;
				}
			}
			arrayList2 = roleuiManager.GetInstance().GetGameUserList(nTeamType, 0);
			arrayList3 = FBManager.GetInstance().m_szFbResultData;
			bool flag4 = arrayList3.Count < 1;
			if (flag4)
			{
				return result;
			}
			arrayList4 = (ArrayList)arrayList3[0];
			int count = arrayList2.Count;
			for (int i = 0; i < count; i++)
			{
				string s = arrayList2[i].ToString();
				int num = arrayList4.IndexOf(Fun.EncodingUTF8(s));
				bool flag5 = num != -1;
				if (flag5)
				{
					arrayList5 = (ArrayList)arrayList4[num + 1];
					bool flag6 = (int)arrayList5[9] == 1;
					if (flag6)
					{
						result = arrayList5;
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000827AC File Offset: 0x000809AC
	private ArrayList GetMeFbResultData()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		arrayList2 = FBManager.GetInstance().m_szFbResultData;
		bool flag = arrayList2.Count < 1;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			ArrayList arrayList3 = (ArrayList)arrayList2[0];
			string meName = roleuiManager.instance.GetMeName();
			int num = arrayList3.IndexOf(Fun.EncodingUTF8(meName));
			bool flag2 = num == -1;
			if (flag2)
			{
				result = arrayList;
			}
			else
			{
				result = (ArrayList)arrayList3[num + 1];
			}
		}
		return result;
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x00082832 File Offset: 0x00080A32
	public override void Close()
	{
		this.NewMainDaZhao.Close();
		this._NetLiving = null;
		this.heroSelectData = null;
		base.Close();
	}

	// Token: 0x04000B1A RID: 2842
	public static gameuiCon inst;

	// Token: 0x04000B1B RID: 2843
	private GameObject oCenterZhunXing;

	// Token: 0x04000B1C RID: 2844
	public HeroZhunXingUiCon mainHeroZhunXing;

	// Token: 0x04000B1D RID: 2845
	private GameObject oRightHpUI;

	// Token: 0x04000B1E RID: 2846
	public NewHeroHpUi NewMainHp;

	// Token: 0x04000B1F RID: 2847
	private GameObject oRightSkillUI;

	// Token: 0x04000B20 RID: 2848
	public NewHeroSkillUi NewMainHeroSkill;

	// Token: 0x04000B21 RID: 2849
	private GameObject oCenterDaZhao;

	// Token: 0x04000B22 RID: 2850
	public NewHeroDaZhaoUi NewMainDaZhao;

	// Token: 0x04000B23 RID: 2851
	private GameObject oDeadUI;

	// Token: 0x04000B24 RID: 2852
	public HeroDeadUiCon mainDead;

	// Token: 0x04000B25 RID: 2853
	private GameObject oMsgUI;

	// Token: 0x04000B26 RID: 2854
	public HeroGameMsgCon mainMsgUI;

	// Token: 0x04000B27 RID: 2855
	private GameObject oRoundEnd;

	// Token: 0x04000B28 RID: 2856
	public HeroRoundEndCon mainRoundUI;

	// Token: 0x04000B29 RID: 2857
	private GameObject oBody;

	// Token: 0x04000B2A RID: 2858
	private NetLiving _NetLiving;

	// Token: 0x04000B2B RID: 2859
	public HeroData heroSelectData;

	// Token: 0x04000B2C RID: 2860
	public DtsUI DtsUiObj;

	// Token: 0x04000B2D RID: 2861
	public bool mYukiOnna;
}

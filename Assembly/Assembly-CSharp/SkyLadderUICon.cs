using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DD RID: 989
public class SkyLadderUICon : Window
{
	// Token: 0x06002A36 RID: 10806 RVA: 0x0017494C File Offset: 0x00172B4C
	protected override void Awake()
	{
		base.Awake();
		this.seasonTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.seasonTxt.font = fUIManager.mNewComic;
		}
		this.skyScoreTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.skyScoreImg = this.UIGameObj.Get_GameObj(14).GetComponent<Image>();
		this.skyLvlIcon = this.UIGameObj.Get_GameObj(2).GetComponent<Image>();
		this.skyLvlImg = this.UIGameObj.Get_GameObj(3).GetComponent<Image>();
		this.skyLvlTxt = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.endRewardGrid = this.UIGameObj.Get_GameObj(4);
		this.endRewardGrid.SetActive(false);
		GameObject go = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnStartMatch);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lvlDescTg = this.UIGameObj.Get_GameObj(6).GetComponent<Toggle>();
		EventTriggerListener.Get(this.lvlDescTg.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.ruleDescTg = this.UIGameObj.Get_GameObj(7).GetComponent<Toggle>();
		EventTriggerListener.Get(this.ruleDescTg.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lvlRewardGrid = this.UIGameObj.Get_GameObj(9);
		this.lvlRewardGrid.SetActive(false);
		this.lvlDescTxt = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.matchDescTxt = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.seasonDescTxt = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.limitTimeDesc = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.matchMoneyTxt = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		go = this.UIGameObj.Get_GameObj(19);
		EventTriggerListener.Get(go).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverMatchMoney);
		EventTriggerListener.Get(go).onExit = new EventTriggerListener.VoidDelegate(this.OnExitMatchMoney);
		this.nextLvlTxt = this.UIGameObj.Get_GameObj(20).GetComponent<Text>();
		SkyLadderUICon.inst = this;
	}

	// Token: 0x06002A37 RID: 10807 RVA: 0x00174BCC File Offset: 0x00172DCC
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.ShowLimitTime();
		this.matchMoneyTxt.text = PlayerDataManager.getInstance().playerdet.matchMoney.ToString();
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x00174C20 File Offset: 0x00172E20
	private void Update()
	{
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x00174C24 File Offset: 0x00172E24
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.lvlDescTg.isOn = true;
		this.ShowLimitTime();
		this.matchMoneyTxt.text = PlayerDataManager.getInstance().playerdet.matchMoney.ToString();
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x00174C8C File Offset: 0x00172E8C
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x00174C98 File Offset: 0x00172E98
	private void ShowLimitTime()
	{
		bool flag = SkyLadderManager.GetInstance().m_nSkyLadderOpen == 1;
		if (flag)
		{
			SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[SkyLadderManager.GetInstance().m_nSkyLadderActId];
			string[] skyLadderOpenTime = SkyLadderManager.GetInstance().GetSkyLadderOpenTime(Fun.GetServerDate());
			string lang = Language.GetLang(23035, new object[0]);
			bool flag2 = DateTime.Parse(skyLadderOpenTime[4]).DayOfWeek == DayOfWeek.Saturday;
			if (flag2)
			{
				lang = Language.GetLang(23042, new object[0]);
			}
			this.limitTimeDesc.text = lang;
			this.limitTimeDesc.transform.parent.GetComponent<Image>().color = Color.white;
		}
		else
		{
			this.limitTimeDesc.text = string.Empty;
			this.limitTimeDesc.transform.parent.GetComponent<Image>().color = Color.clear;
		}
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x00174D88 File Offset: 0x00172F88
	public void ResetSkyLadderInfo(int nId)
	{
		this.nXmlId = nId;
		this.SetSkyLadderInfo();
		this.SetSkyLvlReward();
		SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[this.nXmlId];
		this.lvlDescTxt.text = skyLadderData.sLvlDesc;
		this.matchDescTxt.text = skyLadderData.sMatchDesc;
		this.seasonDescTxt.text = skyLadderData.sSeasonDesc;
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x00174DF8 File Offset: 0x00172FF8
	public void SetSkyLadderInfo()
	{
		SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[this.nXmlId];
		int num = SkyLadderManager.GetInstance().m_nSkyLevel;
		int num2 = SkyLadderManager.GetInstance().m_nSkyScore;
		int nSkyMatchnum = SkyLadderManager.GetInstance().m_nSkyMatchnum;
		this.seasonTxt.text = Language.GetLang(23009, new object[]
		{
			SkyLadderManager.GetInstance().m_nSkySeasonId
		});
		bool flag = nSkyMatchnum < skyLadderData.nMinMatchNum;
		SkyLadderReward skyLadderReward;
		if (flag)
		{
			this.skyScoreImg.enabled = false;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ArrayList szSkyMatchResult = SkyLadderManager.GetInstance().m_szSkyMatchResult;
			for (int i = skyLadderData.nMinMatchNum - 1; i >= 0; i--)
			{
				bool flag2 = i < szSkyMatchResult.Count;
				if (flag2)
				{
					num2 = (int)szSkyMatchResult[i];
					bool flag3 = num2 == 0;
					if (flag3)
					{
						num4++;
					}
					bool flag4 = num2 == 1;
					if (flag4)
					{
						num3++;
					}
					bool flag5 = num2 == 2;
					if (flag5)
					{
						num5++;
					}
				}
			}
			this.skyScoreTxt.text = Language.GetLang(23033, new object[]
			{
				nSkyMatchnum,
				skyLadderData.nMinMatchNum,
				num3,
				num4,
				num5
			});
			this.skyLvlIcon.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", num));
			this.skyLvlImg.enabled = false;
			this.skyLvlTxt.text = string.Empty;
			this.nextLvlTxt.text = string.Empty;
			num = 1;
		}
		else
		{
			this.skyScoreImg.enabled = false;
			this.skyScoreTxt.text = Language.GetLang(5045, new object[]
			{
				num2
			});
			this.skyLvlIcon.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", num));
			this.skyLvlImg.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_name{0}", num));
			bool flag6 = WebParam.client_language == "zh";
			if (flag6)
			{
				this.skyLvlImg.enabled = true;
				this.skyLvlTxt.text = string.Empty;
			}
			else
			{
				this.skyLvlImg.enabled = false;
				this.skyLvlTxt.text = skyLadderData.szSkyLadderReward[num].sLvlname;
			}
			bool flag7 = num < skyLadderData.nMaxLvl;
			if (flag7)
			{
				skyLadderReward = skyLadderData.szSkyLadderReward[num + 1];
				this.nextLvlTxt.text = Language.GetLang(23037, new object[]
				{
					skyLadderReward.nMinScore - num2,
					skyLadderReward.sLvlname
				});
			}
			else
			{
				this.nextLvlTxt.text = string.Empty;
			}
		}
		fUIManager.OnClearObj(this.szEndReward);
		skyLadderReward = skyLadderData.szSkyLadderReward[num];
		for (int j = 0; j < skyLadderReward.szEndReward.Length; j++)
		{
			bool flag8 = !string.IsNullOrEmpty(skyLadderReward.szEndReward[j]);
			if (flag8)
			{
				string[] array = skyLadderReward.szEndReward[j].Split(new char[]
				{
					','
				});
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.endRewardGrid);
				gameObject.transform.SetParent(this.endRewardGrid.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.SetActive(true);
				gameObject.name = "EndReward" + array[0];
				RawImage component = gameObject.transform.Find("RawImage").GetComponent<RawImage>();
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(array[0])];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverEndRewardGrid);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitEndRewardGrid);
				this.szEndReward.Add(gameObject);
			}
		}
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x001752AC File Offset: 0x001734AC
	private void ExitEndRewardGrid(GameObject go)
	{
		Text component = go.transform.Find("Text").GetComponent<Text>();
		component.text = string.Empty;
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x001752DC File Offset: 0x001734DC
	private void OverEndRewardGrid(GameObject go)
	{
		Text component = go.transform.Find("Text").GetComponent<Text>();
		int key = int.Parse(go.name.Replace("EndReward", string.Empty));
		bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(key);
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			component.text = itemData.m_sName;
		}
		else
		{
			component.text = string.Empty;
		}
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x00175360 File Offset: 0x00173560
	public void SetSkyLvlReward()
	{
		SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[this.nXmlId];
		int nSkyLevel = SkyLadderManager.GetInstance().m_nSkyLevel;
		int num = 0;
		foreach (SkyLadderReward skyLadderReward in skyLadderData.szSkyLadderReward.Values)
		{
			bool flag = num < this.szLvlRewardGrid.Count;
			GameObject gameObject;
			if (flag)
			{
				gameObject = this.szLvlRewardGrid[num];
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.lvlRewardGrid);
				gameObject.transform.SetParent(this.lvlRewardGrid.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.SetActive(true);
				this.szLvlRewardGrid.Add(gameObject);
			}
			gameObject.name = string.Format("LvlReward{0}", skyLadderReward.nLvl);
			Image component = gameObject.GetComponent<Image>();
			bool flag2 = skyLadderReward.nLvl >= 3 && skyLadderReward.nLvl <= 8;
			if (flag2)
			{
				component.enabled = true;
				component.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_t{0}", skyLadderReward.nLvl));
			}
			else
			{
				component.enabled = false;
			}
			component = gameObject.transform.Find("Background").GetComponent<Image>();
			component.enabled = (nSkyLevel == skyLadderReward.nLvl);
			component = gameObject.transform.Find("Icon").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", skyLadderReward.nLvl));
			Text component2 = gameObject.transform.Find("Name").GetComponent<Text>();
			component2.text = skyLadderReward.sLvlname;
			component2 = gameObject.transform.Find("Score").GetComponent<Text>();
			bool flag3 = skyLadderReward.nMaxScore == 10000;
			if (flag3)
			{
				component2.text = Language.GetLang(23029, new object[]
				{
					skyLadderReward.nMinScore
				});
			}
			else
			{
				component2.text = string.Format("{0}-{1}", skyLadderReward.nMinScore, skyLadderReward.nMaxScore);
			}
			component2 = gameObject.transform.Find("JingjiTxt").GetComponent<Text>();
			for (int i = 0; i < skyLadderReward.szEndReward.Length; i++)
			{
				bool flag4 = !string.IsNullOrEmpty(skyLadderReward.szEndReward[i]);
				if (flag4)
				{
					string[] array = skyLadderReward.szEndReward[i].Split(new char[]
					{
						','
					});
					bool flag5 = int.Parse(array[0]) == 1019;
					if (flag5)
					{
						component2.text = array[1];
						break;
					}
				}
			}
			component = gameObject.transform.Find("SaveLvl").GetComponent<Image>();
			component.enabled = (skyLadderReward.nIsSaveLvl == 0);
			component = gameObject.transform.Find("CutScore").GetComponent<Image>();
			component.enabled = (skyLadderReward.nCutScore != 0);
			num++;
		}
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x00175720 File Offset: 0x00173920
	private void OnStartMatch(GameObject go)
	{
		bool flag = SkyLadderManager.GetInstance().isSkyMatching == 1;
		if (!flag)
		{
			bool flag2 = SkyLadderManager.GetInstance().m_nSkyLadderOpen == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(23014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = SkyLadderManager.GetInstance().m_nSkyLadderOpen == 1;
				if (flag3)
				{
					SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[SkyLadderManager.GetInstance().m_nSkyLadderActId];
					string[] skyLadderOpenTime = SkyLadderManager.GetInstance().GetSkyLadderOpenTime(Fun.GetServerDate());
					string lang = Language.GetLang(23034, new object[0]);
					bool flag4 = DateTime.Parse(skyLadderOpenTime[4]).DayOfWeek == DayOfWeek.Saturday;
					if (flag4)
					{
						lang = Language.GetLang(23041, new object[0]);
					}
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), lang, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					SkyLadderManager.GetInstance().OnSkyLadderMatch(this.nXmlId, new SkyLadderManager.MatchBtnDelegate(this.SkyLadderMatch), null);
				}
			}
		}
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x00175888 File Offset: 0x00173A88
	private void SkyLadderMatch(object szParam)
	{
		base.CancelInvoke("SkyLadderMatch");
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
		if (flag)
		{
			TeamManager.GetInstance().OnLeaveTeam();
			base.Invoke("SkyLadderMatch", 0.5f);
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
			if (flag2)
			{
				TeamManager.GetInstance().LeaveLookingTeam(4, 0, 0, 0);
			}
			else
			{
				bool flag3 = MainUICon.inst != null;
				if (flag3)
				{
					MainUICon.inst.SkyStartMatch();
				}
			}
		}
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x00175934 File Offset: 0x00173B34
	private void OnExitMatchMoney(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x00175944 File Offset: 0x00173B44
	private void OnOverMatchMoney(GameObject go)
	{
		string sText = string.Empty;
		sText = Language.GetLang(23036, new object[]
		{
			PlayerDataManager.getInstance().playerdet.matchMoney
		});
		TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x040023D3 RID: 9171
	public static SkyLadderUICon inst;

	// Token: 0x040023D4 RID: 9172
	private Text seasonTxt;

	// Token: 0x040023D5 RID: 9173
	private Text skyScoreTxt;

	// Token: 0x040023D6 RID: 9174
	private Image skyScoreImg;

	// Token: 0x040023D7 RID: 9175
	private Image skyLvlIcon;

	// Token: 0x040023D8 RID: 9176
	private Image skyLvlImg;

	// Token: 0x040023D9 RID: 9177
	private Text skyLvlTxt;

	// Token: 0x040023DA RID: 9178
	private Text nextLvlTxt;

	// Token: 0x040023DB RID: 9179
	private GameObject endRewardGrid;

	// Token: 0x040023DC RID: 9180
	private List<GameObject> szEndReward = new List<GameObject>();

	// Token: 0x040023DD RID: 9181
	private GameObject lvlRewardGrid;

	// Token: 0x040023DE RID: 9182
	private List<GameObject> szLvlRewardGrid = new List<GameObject>();

	// Token: 0x040023DF RID: 9183
	private Toggle lvlDescTg;

	// Token: 0x040023E0 RID: 9184
	private Toggle ruleDescTg;

	// Token: 0x040023E1 RID: 9185
	private Text lvlDescTxt;

	// Token: 0x040023E2 RID: 9186
	private Text matchDescTxt;

	// Token: 0x040023E3 RID: 9187
	private Text seasonDescTxt;

	// Token: 0x040023E4 RID: 9188
	private Text limitTimeDesc;

	// Token: 0x040023E5 RID: 9189
	private Text matchMoneyTxt;

	// Token: 0x040023E6 RID: 9190
	private int nXmlId;
}

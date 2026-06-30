using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C8 RID: 712
public class RankUICon : Window
{
	// Token: 0x060020A2 RID: 8354 RVA: 0x0012B21C File Offset: 0x0012941C
	protected override void Awake()
	{
		base.Awake();
		Text component = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			component.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(5)).onClick = new EventTriggerListener.VoidDelegate(this.CloseUI);
		this.Tab1 = this.UIGameObj.Get_GameObj(2).gameObject;
		this.Tab2 = this.UIGameObj.Get_GameObj(3).gameObject;
		this.Tab3 = this.UIGameObj.Get_GameObj(4).gameObject;
		this.Tab4 = this.UIGameObj.Get_GameObj(35).gameObject;
		this.Tab4.SetActive(Fun.IsFunctionOpen("PveRankTabBtn", null));
		this.Tab5 = this.UIGameObj.Get_GameObj(41).gameObject;
		this.Tab5.SetActive(Fun.IsFunctionOpen("DtsRankTabBtn", null));
		this.Tab6 = this.UIGameObj.Get_GameObj(47);
		this.Tab6.SetActive(Fun.IsFunctionOpen("FlowerRankTabBtn", null));
		EventTriggerListener.Get(this.Tab1).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		EventTriggerListener.Get(this.Tab2).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		EventTriggerListener.Get(this.Tab3).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		EventTriggerListener.Get(this.Tab4).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		EventTriggerListener.Get(this.Tab5).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		EventTriggerListener.Get(this.Tab6).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		this.LevelRankPanel = this.UIGameObj.Get_GameObj(7).gameObject;
		this.HeroRankPanel = this.UIGameObj.Get_GameObj(8).gameObject;
		this.LadderRankPanel = this.UIGameObj.Get_GameObj(18).gameObject;
		this.PveRankPanel = this.UIGameObj.Get_GameObj(37).gameObject;
		this.DtsRankPanel = this.UIGameObj.Get_GameObj(42).gameObject;
		this.flowerRankPanel = this.UIGameObj.Get_GameObj(27);
		this.RankItem = this.UIGameObj.Get_GameObj(10).gameObject;
		this.PveItem = this.UIGameObj.Get_GameObj(39).gameObject;
		this.DtsItem = this.UIGameObj.Get_GameObj(44).gameObject;
		this.DtsItem.SetActive(false);
		this.flowerItem = this.UIGameObj.Get_GameObj(46);
		this.LeftEndBtn = this.UIGameObj.Get_GameObj(12).GetComponent<Button>();
		this.LeftBtn = this.UIGameObj.Get_GameObj(13).GetComponent<Button>();
		this.RightEndBtn = this.UIGameObj.Get_GameObj(15).GetComponent<Button>();
		this.RightBtn = this.UIGameObj.Get_GameObj(16).GetComponent<Button>();
		EventTriggerListener.Get(this.LeftEndBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnLeftEnd);
		EventTriggerListener.Get(this.LeftBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnLeft);
		EventTriggerListener.Get(this.RightEndBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRightEnd);
		EventTriggerListener.Get(this.RightBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRight);
		this.PageText = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.PageText.text = string.Empty;
		this.HeroDropDown = this.UIGameObj.Get_GameObj(17).GetComponent<Dropdown>();
		this.LevelRankList = this.UIGameObj.Get_GameObj(19).gameObject;
		this.HeroRankList = this.UIGameObj.Get_GameObj(20).gameObject;
		this.PveRankList = this.UIGameObj.Get_GameObj(36).gameObject;
		this.DtsRankList = this.UIGameObj.Get_GameObj(45).gameObject;
		this.flowerRankList = this.UIGameObj.Get_GameObj(29);
		this.HeroDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnHeroDropDownChange));
		this.SeasonDropDown = this.UIGameObj.Get_GameObj(34).GetComponent<Dropdown>();
		this.SeasonDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnSeasonDropDownChange));
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(5)).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.skyladderDropDown = this.UIGameObj.Get_GameObj(22).GetComponent<Dropdown>();
		this.skyladderDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnSkyladderDropDown));
		this.skyladderRender = this.UIGameObj.Get_GameObj(24);
		this.skyladderRender.SetActive(false);
		this.skyladderList = this.UIGameObj.Get_GameObj(25);
		this.skyladderList.SetActive(false);
		this.skyladderTip = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.skyladderTip.enabled = true;
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(30)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickMyRank);
		this.myRankTxt = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.LevelRankTip = this.UIGameObj.Get_GameObj(32);
		this.HeroRankTip = this.UIGameObj.Get_GameObj(33);
		this.PveRankTip = this.UIGameObj.Get_GameObj(40);
		this.DtsRankTip = this.UIGameObj.Get_GameObj(43);
		this.flowerRankTip = this.UIGameObj.Get_GameObj(28);
		this.LevelRankTip.GetComponent<Text>().text = Language.GetLang(23028, new object[0]);
		this.HeroRankTip.GetComponent<Text>().text = Language.GetLang(23028, new object[0]);
		this.PveRankTip.GetComponent<Text>().text = Language.GetLang(23028, new object[0]);
		this.DtsRankTip.GetComponent<Text>().text = Language.GetLang(23028, new object[0]);
		this.LevelRankTip.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
		this.HeroRankTip.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
		this.PveRankTip.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
		this.DtsRankTip.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
		this.PveHeroTypeDropDown = this.UIGameObj.Get_GameObj(38).GetComponent<Dropdown>();
		this.PveHeroTypeDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnPveHeroTypeDropDown));
		RankUICon.inst = this;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x0012B934 File Offset: 0x00129B34
	private void Start()
	{
		this.m_szSeasonID.Clear();
		for (int i = 0; i < SkyLadderManager.GetInstance().m_szSeasonList.Count; i++)
		{
			ArrayList arrayList = (ArrayList)SkyLadderManager.GetInstance().m_szSeasonList[i];
			int num = (int)arrayList[0];
			this.m_szSeasonID.Add(num.ToString());
		}
		this.m_szSeasonID.Add(SkyLadderManager.GetInstance().m_nSkySeasonId.ToString());
		List<int> list = new List<int>();
		list.AddRange(fGameManager.mloadConfig.AllHeroList.Keys);
		this.m_SelectnHeroID = list[0];
		this.m_szPveHeroTypeID.AddRange(fGameManager.mloadConfig.AllQiangHuaData.Values);
		this.OpenTabByIndex(RankManager.GetInstance().RankUiTabIndex);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x0012BA18 File Offset: 0x00129C18
	public override void Reset()
	{
		base.Reset();
		this.m_szSeasonID.Clear();
		for (int i = 0; i < SkyLadderManager.GetInstance().m_szSeasonList.Count; i++)
		{
			ArrayList arrayList = (ArrayList)SkyLadderManager.GetInstance().m_szSeasonList[i];
			int num = (int)arrayList[0];
			this.m_szSeasonID.Add(num.ToString());
		}
		this.m_szSeasonID.Add(SkyLadderManager.GetInstance().m_nSkySeasonId.ToString());
		RankManager.GetInstance().CheckNextDay(RankManager.GetInstance().sRankDate, Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss"));
		this.OpenTabByIndex(RankManager.GetInstance().RankUiTabIndex);
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x0012BAE4 File Offset: 0x00129CE4
	public void OnChangeEvent(GameObject obj)
	{
		bool flag = this.oNowSelectTab != null;
		GameObject tabPanel;
		if (flag)
		{
			this.oNowSelectTab.GetComponent<Image>().sprite = Window.spriteName("ab/rankuiatlas", "shanganniu_normal");
			tabPanel = this.GetTabPanel(this.oNowSelectTab.name);
			tabPanel.SetActive(false);
		}
		obj.GetComponent<Image>().sprite = Window.spriteName("ab/rankuiatlas", "shanganniu_selecte");
		tabPanel = this.GetTabPanel(obj.name);
		tabPanel.SetActive(true);
		this.oNowSelectTab = obj;
		int nTab = int.Parse(this.oNowSelectTab.name.Substring(6)) - 1;
		this.ShowTabByIndex(nTab);
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x0012BB98 File Offset: 0x00129D98
	private GameObject GetTabPanel(string sTabName)
	{
		if (!true)
		{
		}
		GameObject result;
		if (!(sTabName == "Button1"))
		{
			if (!(sTabName == "Button2"))
			{
				if (!(sTabName == "Button3"))
				{
					if (!(sTabName == "Button5"))
					{
						if (!(sTabName == "Button6"))
						{
							result = this.PveRankPanel;
						}
						else
						{
							result = this.flowerRankPanel;
						}
					}
					else
					{
						result = this.DtsRankPanel;
					}
				}
				else
				{
					result = this.LadderRankPanel;
				}
			}
			else
			{
				result = this.HeroRankPanel;
			}
		}
		else
		{
			result = this.LevelRankPanel;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x0012BC2C File Offset: 0x00129E2C
	private GameObject GetTabBtn(int nTab)
	{
		if (!true)
		{
		}
		GameObject result;
		switch (nTab)
		{
		case 0:
			result = this.Tab1;
			goto IL_5B;
		case 1:
			result = this.Tab2;
			goto IL_5B;
		case 2:
			result = this.Tab3;
			goto IL_5B;
		case 4:
			result = this.Tab5;
			goto IL_5B;
		case 5:
			result = this.Tab6;
			goto IL_5B;
		}
		result = this.Tab4;
		IL_5B:
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x0012BC9D File Offset: 0x00129E9D
	public void OpenTabByIndex(int nTab)
	{
		this.OnChangeEvent(this.GetTabBtn(nTab));
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x0012BCB0 File Offset: 0x00129EB0
	private void ShowTabByIndex(int nTab)
	{
		switch (nTab)
		{
		case 0:
		{
			bool flag = this.nNowTabIndex != 0 || this.nNowPage != 1;
			if (flag)
			{
				this.nNowTabIndex = 0;
				RankManager.GetInstance().RankUiTabIndex = 0;
				bool flag2 = RankManager.GetInstance().sRankDate == string.Empty || RankManager.GetInstance().GetMaxUserSize() == 0;
				if (flag2)
				{
					this.GetUserRankToServer(1, 0);
				}
				else
				{
					this.ShowUserRankList(1);
				}
			}
			break;
		}
		case 1:
		{
			bool flag3 = this.nNowTabIndex != 1 || this.nNowPage != 1;
			if (flag3)
			{
				this.nNowTabIndex = 1;
				RankManager.GetInstance().RankUiTabIndex = 1;
				bool flag4 = this.m_szHeroID.Count == 0;
				if (flag4)
				{
					this.InitHeroDropDown();
				}
				this.InitUserDropDown();
				bool flag5 = RankManager.GetInstance().sRankDate == string.Empty || !RankManager.GetInstance().IsHadHeroRankData(this.m_nNowMatchType, this.m_SelectnHeroID);
				if (flag5)
				{
					this.GetHeroRankToServer(this.m_SelectnHeroID, 1, 0);
				}
				else
				{
					this.ShowHeroRankList(1);
				}
			}
			break;
		}
		case 2:
		{
			bool flag6 = this.nNowTabIndex != 2;
			if (flag6)
			{
				this.nNowTabIndex = 2;
				RankManager.GetInstance().RankUiTabIndex = 2;
				this.UpdateSeasonList();
			}
			break;
		}
		case 3:
		{
			bool flag7 = this.nNowTabIndex != 3 || this.nNowPage != 1;
			if (flag7)
			{
				this.nNowTabIndex = 3;
				RankManager.GetInstance().RankUiTabIndex = 3;
				this.InitPveDropDown();
				bool flag8 = RankManager.GetInstance().sRankDate == string.Empty || RankManager.GetInstance().GetMaxPveSize(this.m_nPveHeroType) == 0;
				if (flag8)
				{
					this.GetPveRankToServer(1, this.m_nPveHeroType, 0);
				}
				else
				{
					this.ShowPveRankList(1);
				}
			}
			break;
		}
		case 4:
		{
			bool flag9 = this.nNowTabIndex != 4 || this.nNowPage != 1;
			if (flag9)
			{
				this.nNowTabIndex = 4;
				RankManager.GetInstance().RankUiTabIndex = 4;
				bool flag10 = RankManager.GetInstance().sRankDate == string.Empty || RankManager.GetInstance().GetMaxDtsSize() == 0;
				if (flag10)
				{
					this.GetDtsRankToServer(1, 0);
				}
				else
				{
					this.ShowDtsRankList(1);
				}
			}
			break;
		}
		case 5:
		{
			bool flag11 = this.nNowTabIndex != 5 || this.nNowPage != 1;
			if (flag11)
			{
				this.nNowTabIndex = 5;
				RankManager.GetInstance().RankUiTabIndex = 5;
				bool flag12 = RankManager.GetInstance().sRankDate == string.Empty || RankManager.GetInstance().GetMaxFlowerSize() == 0;
				if (flag12)
				{
					this.GetFlowerRankToServer(1, 0);
				}
				else
				{
					this.ShowFlowerRankList(1);
				}
			}
			break;
		}
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x0012BFC0 File Offset: 0x0012A1C0
	private void InitPveDropDown()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		int count = this.m_szPveHeroTypeID.Count;
		this.PveHeroTypeDropDown.options.Clear();
		for (int i = 0; i < count; i++)
		{
			list.Add(new Dropdown.OptionData(this.m_szPveHeroTypeID[i].sName));
		}
		this.PveHeroTypeDropDown.AddOptions(list);
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x0012C030 File Offset: 0x0012A230
	private void OnPveHeroTypeDropDown(int nSelect)
	{
		bool flag = nSelect < this.m_szPveHeroTypeID.Count && this.m_nPveHeroType != nSelect;
		if (flag)
		{
			this.m_nPveHeroType = nSelect;
			bool flag2 = RankManager.GetInstance().sRankDate == string.Empty || RankManager.GetInstance().GetMaxPveSize(this.m_nPveHeroType) == 0;
			if (flag2)
			{
				this.GetPveRankToServer(1, this.m_nPveHeroType, 0);
			}
			else
			{
				this.ShowPveRankList(1);
			}
		}
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x0012C0B8 File Offset: 0x0012A2B8
	public void ShowUserRankList(int nPage)
	{
		int maxUserSize = RankManager.GetInstance().GetMaxUserSize();
		this.nMaxPage = (maxUserSize - 1) / 10 + 1;
		bool flag = maxUserSize <= 0;
		if (flag)
		{
			this.LevelRankTip.SetActive(true);
		}
		else
		{
			this.LevelRankTip.SetActive(false);
		}
		int myLevelRank = RankManager.GetInstance().GetMyLevelRank();
		this.SetMyRank(myLevelRank);
		this.ShowUserRankOnPage(nPage);
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x0012C128 File Offset: 0x0012A328
	private void SetMyRank(int nMyRank)
	{
		bool flag = nMyRank <= 0;
		if (flag)
		{
			this.myRankTxt.text = Language.GetLang(46093, new object[0]);
		}
		else
		{
			this.myRankTxt.text = Language.GetLang(46094, new object[]
			{
				nMyRank.ToString()
			});
			bool flag2 = this.nNowTabIndex == 0;
			if (flag2)
			{
				int nPage = (nMyRank - 1) / 10 + 1;
				bool flag3 = !RankManager.GetInstance().HadUserPage(nPage);
				if (flag3)
				{
					bool flag4 = base.IsInvoking("DelayGetMyRankData");
					if (flag4)
					{
						base.CancelInvoke("DelayGetMyRankData");
					}
					base.Invoke("DelayGetMyRankData", 0.2f);
				}
			}
			bool flag5 = this.nNowTabIndex == 1;
			if (flag5)
			{
				int nPage = (nMyRank - 1) / 10 + 1;
				bool flag6 = !RankManager.GetInstance().HadHeroPage(this.m_SelectnHeroID, nPage, this.m_nNowMatchType);
				if (flag6)
				{
					bool flag7 = base.IsInvoking("DelayGetMyHeroData");
					if (flag7)
					{
						base.CancelInvoke("DelayGetMyHeroData");
					}
					base.Invoke("DelayGetMyHeroData", 0.2f);
				}
			}
			bool flag8 = this.nNowTabIndex == 4;
			if (flag8)
			{
				int nPage = (nMyRank - 1) / 10 + 1;
				bool flag9 = !RankManager.GetInstance().HadDtsPage(nPage);
				if (flag9)
				{
					bool flag10 = base.IsInvoking("DelayGetMyDtsRankData");
					if (flag10)
					{
						base.CancelInvoke("DelayGetMyDtsRankData");
					}
					base.Invoke("DelayGetMyDtsRankData", 0.2f);
				}
			}
			bool flag11 = this.nNowTabIndex != 5;
			if (!flag11)
			{
				int nPage = (nMyRank - 1) / 10 + 1;
				bool flag12 = !RankManager.GetInstance().HadFlowerPage(nPage);
				if (flag12)
				{
					bool flag13 = base.IsInvoking("DelayGetMyFlowerRankData");
					if (flag13)
					{
						base.CancelInvoke("DelayGetMyFlowerRankData");
					}
					base.Invoke("DelayGetMyFlowerRankData", 0.2f);
				}
			}
		}
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x0012C318 File Offset: 0x0012A518
	private void DelayGetMyFlowerRankData()
	{
		bool flag = this.nNowTabIndex == 0;
		if (flag)
		{
			int myFlowerRank = RankManager.GetInstance().GetMyFlowerRank();
			int nPage = (myFlowerRank - 1) / 10 + 1;
			bool flag2 = !RankManager.GetInstance().HadFlowerPage(nPage);
			if (flag2)
			{
				this.GetFlowerRankToServer(nPage, 1);
			}
		}
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x0012C368 File Offset: 0x0012A568
	private void DelayGetMyDtsRankData()
	{
		bool flag = this.nNowTabIndex == 0;
		if (flag)
		{
			int myDtsRank = RankManager.GetInstance().GetMyDtsRank();
			int nPage = (myDtsRank - 1) / 10 + 1;
			bool flag2 = !RankManager.GetInstance().HadDtsPage(nPage);
			if (flag2)
			{
				this.GetDtsRankToServer(nPage, 1);
			}
		}
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x0012C3B8 File Offset: 0x0012A5B8
	private void DelayGetMyRankData()
	{
		bool flag = this.nNowTabIndex == 0;
		if (flag)
		{
			int myLevelRank = RankManager.GetInstance().GetMyLevelRank();
			int nPage = (myLevelRank - 1) / 10 + 1;
			bool flag2 = !RankManager.GetInstance().HadUserPage(nPage);
			if (flag2)
			{
				this.GetUserRankToServer(nPage, 1);
			}
		}
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x0012C408 File Offset: 0x0012A608
	private void DelayGetMyHeroData()
	{
		bool flag = this.nNowTabIndex == 1;
		if (flag)
		{
			int myHeroRank = RankManager.GetInstance().GetMyHeroRank(this.m_SelectnHeroID, this.m_nNowMatchType);
			int nPage = (myHeroRank - 1) / 10 + 1;
			bool flag2 = !RankManager.GetInstance().HadHeroPage(this.m_SelectnHeroID, nPage, this.m_nNowMatchType);
			if (flag2)
			{
				this.GetHeroRankToServer(this.m_SelectnHeroID, nPage, 1);
			}
		}
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x0012C474 File Offset: 0x0012A674
	private void ShowUserRankOnPage(int nPage)
	{
		bool flag = nPage % 10 == 5;
		if (flag)
		{
			int num = nPage + 6;
			bool flag2 = num <= this.nMaxPage && !RankManager.GetInstance().HadUserPage(num);
			if (flag2)
			{
				this.GetUserRankToServer(num, 1);
			}
			num -= 20;
			bool flag3 = num > 0 && !RankManager.GetInstance().HadUserPage(num);
			if (flag3)
			{
				this.GetUserRankToServer(num, 1);
			}
		}
		bool flag4 = nPage == 1 && !RankManager.GetInstance().HadUserPage(this.nMaxPage);
		if (flag4)
		{
			bool flag5 = base.IsInvoking("DalayGetUserData");
			if (flag5)
			{
				base.CancelInvoke("DalayGetUserData");
			}
			base.Invoke("DalayGetUserData", 0.1f);
		}
		ArrayList userListByPage = RankManager.GetInstance().GetUserListByPage(nPage);
		int count = userListByPage.Count;
		bool flag6 = count <= 0;
		if (flag6)
		{
			this.GetUserRankToServer(nPage, 0);
		}
		else
		{
			fUIManager.OnDelRawImage();
			fUIManager.OnClearObj(this.szUserRank);
			this.nNowPage = nPage;
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)userListByPage[i];
				bool flag7 = arrayList.Count > 0;
				if (flag7)
				{
					GameObject freeRankItem = this.GetFreeRankItem();
					this.szUserRank.Add(freeRankItem);
					freeRankItem.transform.SetParent(this.LevelRankList.transform, false);
					freeRankItem.transform.localScale = Vector3.one;
					this.UpdateUserRankItem(freeRankItem, arrayList);
				}
			}
			this.UpdatePageBtn();
		}
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x0012C610 File Offset: 0x0012A810
	private void DalayGetUserData()
	{
		bool flag = this.nNowTabIndex == 0;
		if (flag)
		{
			this.GetUserRankToServer(this.nMaxPage, 1);
		}
		bool flag2 = this.nNowTabIndex == 3;
		if (flag2)
		{
			this.GetPveRankToServer(this.nMaxPage, this.m_nPveHeroType, 1);
		}
		bool flag3 = this.nNowTabIndex == 4;
		if (flag3)
		{
			this.GetDtsRankToServer(this.nMaxPage, 1);
		}
		bool flag4 = this.nNowTabIndex == 5;
		if (flag4)
		{
			this.GetFlowerRankToServer(this.nMaxPage, 1);
		}
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x0012C698 File Offset: 0x0012A898
	private void DalayGetHeroData()
	{
		bool flag = this.nNowTabIndex == 1;
		if (flag)
		{
			this.GetHeroRankToServer(this.m_SelectnHeroID, this.nMaxPage, 1);
		}
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x0012C6CC File Offset: 0x0012A8CC
	private void UpdateUserRankItem(GameObject obj, ArrayList szUser)
	{
		int num = (int)szUser[7];
		int key = (int)szUser[4];
		int num2 = (int)szUser[5];
		int num3 = (int)szUser[2];
		string text = Fun.DecodingUTF8(szUser[1].ToString());
		obj.SetActive(true);
		bool flag = num < 4;
		if (flag)
		{
			Image component = obj.transform.Find("rankimg").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num.ToString());
			component.gameObject.SetActive(true);
		}
		else
		{
			obj.transform.Find("rank").GetComponent<Text>().text = num.ToString();
		}
		obj.transform.Find("level/num").GetComponent<Text>().text = num3.ToString();
		obj.transform.Find("unick").GetComponent<Text>().text = text.ToString();
		obj.transform.Find("matchnum").GetComponent<Text>().text = (num2.ToString() ?? "");
		bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
		if (flag2)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			fUIManager.OnAddRawImage(obj.transform.Find("head").GetComponent<RawImage>());
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", obj.transform.Find("head").GetComponent<RawImage>());
		}
		bool flag3 = num == RankManager.GetInstance().GetMyLevelRank();
		if (flag3)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
		}
		else
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
		}
		string text2 = szUser[6].ToString();
		string[] array = text2.Split(new char[]
		{
			','
		});
		int num4 = array.Length / 2;
		bool flag4 = num4 > 3;
		if (flag4)
		{
			num4 = 3;
		}
		for (int i = 0; i < num4; i++)
		{
			string name = "herolistitem/herohead" + (i + 1).ToString();
			string name2 = "herolistitem/num" + (i + 1).ToString();
			int key2 = int.Parse(array[i * 2]);
			int num5 = int.Parse(array[i * 2 + 1]);
			bool flag5 = fGameManager.mloadConfig.AllHeroList.ContainsKey(key2);
			if (flag5)
			{
				GameObject gameObject = obj.transform.Find(name).gameObject;
				obj.transform.Find(name2).GetComponent<Text>().text = (i + 1).ToString();
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[key2];
				fUIManager.OnAddRawImage(gameObject.GetComponent<RawImage>());
				fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", gameObject.GetComponent<RawImage>());
				obj.transform.Find(name).gameObject.SetActive(true);
				gameObject.name = heroData.hname + ":" + num5.ToString();
			}
		}
		obj.transform.Find("herolistitem").gameObject.SetActive(true);
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x0012CA8B File Offset: 0x0012AC8B
	private void ExitFaceIcon(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x0012CA99 File Offset: 0x0012AC99
	private void OverFaceIcon(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(go.name, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x0012CABD File Offset: 0x0012ACBD
	public void ShowUserRankNotRankOK()
	{
		fUIManager.OnClearObj(this.szUserRank);
		this.nMaxPage = 1;
		this.nNowPage = 1;
		this.UpdatePageBtn();
		this.SetMyRank(-1);
		this.LevelRankTip.SetActive(true);
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x0012CAF6 File Offset: 0x0012ACF6
	public void ShowHeroRankNotRankOK()
	{
		fUIManager.OnClearObj(this.szHeroRank);
		this.nMaxPage = 1;
		this.nNowPage = 1;
		this.UpdatePageBtn();
		this.SetMyRank(-1);
		this.HeroRankTip.SetActive(true);
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x0012CB30 File Offset: 0x0012AD30
	public void ShowHeroRankList(int nPage)
	{
		int myHeroRank = RankManager.GetInstance().GetMyHeroRank(this.m_SelectnHeroID, this.m_nNowMatchType);
		this.SetMyRank(myHeroRank);
		fUIManager.OnClearObj(this.szHeroRank);
		bool flag = RankManager.GetInstance().IsHadHeroRankData(this.m_nNowMatchType, this.m_SelectnHeroID);
		if (flag)
		{
			int maxHeroSize = RankManager.GetInstance().GetMaxHeroSize(this.m_SelectnHeroID, this.m_nNowMatchType);
			bool flag2 = maxHeroSize == 0;
			if (flag2)
			{
				this.nMaxPage = 1;
				this.nNowPage = 1;
				this.UpdatePageBtn();
				this.HeroRankTip.SetActive(true);
			}
			else
			{
				this.HeroRankTip.SetActive(false);
				this.nMaxPage = (maxHeroSize - 1) / 10 + 1;
				this.ShowHeroRankOnPage(1);
			}
		}
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x0012CBF0 File Offset: 0x0012ADF0
	private void ShowHeroRankOnPage(int nPage)
	{
		bool flag = !RankManager.GetInstance().IsHadHeroRankData(this.m_nNowMatchType, this.m_SelectnHeroID);
		if (!flag)
		{
			bool flag2 = nPage % 10 == 5;
			if (flag2)
			{
				int num = nPage + 6;
				bool flag3 = num <= this.nMaxPage && !RankManager.GetInstance().HadHeroPage(this.m_SelectnHeroID, num, this.m_nNowMatchType);
				if (flag3)
				{
					this.GetHeroRankToServer(this.m_SelectnHeroID, num, 1);
				}
				num -= 20;
				bool flag4 = num > 0 && !RankManager.GetInstance().HadHeroPage(this.m_SelectnHeroID, num, this.m_nNowMatchType);
				if (flag4)
				{
					this.GetHeroRankToServer(this.m_SelectnHeroID, num, 1);
				}
			}
			bool flag5 = nPage == 1 && !RankManager.GetInstance().HadHeroPage(this.m_SelectnHeroID, this.nMaxPage, this.m_nNowMatchType);
			if (flag5)
			{
				bool flag6 = base.IsInvoking("DalayGetHeroData");
				if (flag6)
				{
					base.CancelInvoke("DalayGetHeroData");
				}
				base.Invoke("DalayGetHeroData", 0.2f);
			}
			ArrayList heroListByPage = RankManager.GetInstance().GetHeroListByPage(this.m_SelectnHeroID, nPage, this.m_nNowMatchType);
			bool flag7 = heroListByPage.Count <= 0;
			if (flag7)
			{
				this.GetHeroRankToServer(this.m_SelectnHeroID, nPage, 0);
			}
			else
			{
				fUIManager.OnDelRawImage();
				fUIManager.OnClearObj(this.szHeroRank);
				this.HeroRankTip.SetActive(false);
				int maxHeroSize = RankManager.GetInstance().GetMaxHeroSize(this.m_SelectnHeroID, this.m_nNowMatchType);
				this.nMaxPage = (maxHeroSize - 1) / 10 + 1;
				this.nNowPage = nPage;
				for (int i = 0; i < heroListByPage.Count; i++)
				{
					ArrayList arrayList = (ArrayList)heroListByPage[i];
					bool flag8 = arrayList.Count > 0;
					if (flag8)
					{
						GameObject freeRankItem = this.GetFreeRankItem();
						this.szHeroRank.Add(freeRankItem);
						freeRankItem.transform.SetParent(this.HeroRankList.transform, false);
						freeRankItem.transform.localScale = Vector3.one;
						this.UpdateHeroRankItem(freeRankItem, arrayList);
					}
				}
				this.UpdatePageBtn();
			}
		}
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x0012CE34 File Offset: 0x0012B034
	private void UpdateHeroRankItem(GameObject obj, ArrayList szUser)
	{
		string text = Fun.DecodingUTF8(szUser[1].ToString());
		int key = (int)szUser[2];
		int nHeroID = (int)szUser[3];
		int num = (int)szUser[4];
		int num2 = (int)szUser[5];
		int num3 = (int)szUser[6];
		int num4 = (int)szUser[7];
		int num5 = (int)szUser[8];
		obj.SetActive(true);
		bool flag = num3 < 4;
		if (flag)
		{
			Image component = obj.transform.Find("rankimg").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num3.ToString());
			component.gameObject.SetActive(true);
		}
		else
		{
			obj.transform.Find("rank").GetComponent<Text>().text = num3.ToString();
		}
		obj.transform.Find("rank").GetComponent<Text>().text = num3.ToString();
		bool flag2 = num5 == 0;
		if (flag2)
		{
			obj.transform.Find("level/num").GetComponent<Text>().text = "---";
		}
		else
		{
			obj.transform.Find("level/num").GetComponent<Text>().text = num5.ToString();
		}
		obj.transform.Find("unick").GetComponent<Text>().text = text.ToString();
		obj.transform.Find("matchnum").GetComponent<Text>().text = (num2.ToString() ?? "");
		obj.transform.Find("alltime").GetComponent<Text>().text = ((num4 / 60).ToString() ?? "");
		bool flag3 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
		if (flag3)
		{
			GameObject gameObject = obj.transform.Find("head").gameObject;
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			fUIManager.OnAddRawImage(gameObject.GetComponent<RawImage>());
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", gameObject.GetComponent<RawImage>());
		}
		bool flag4 = num3 == RankManager.GetInstance().GetMyHeroRank(nHeroID, this.m_nNowMatchType);
		if (flag4)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
		}
		else
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
		}
		obj.transform.Find("shenglv").GetComponent<Text>().text = (num.ToString() ?? "");
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x0012D138 File Offset: 0x0012B338
	private void GetUserRankToServer(int nPage, int nIsPro)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
		{
			17001,
			PlayerDataManager.getInstance().playerCommon.m_nCid,
			nPage,
			nIsPro
		});
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x0012D1A4 File Offset: 0x0012B3A4
	private void GetHeroRankToServer(int nHeroID, int nPage, int nIsPro)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
		{
			17002,
			PlayerDataManager.getInstance().playerCommon.m_nCid,
			nHeroID,
			nPage,
			nIsPro,
			this.m_nNowMatchType
		});
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x0012D22C File Offset: 0x0012B42C
	private void GetSkyladderRankToServer(int nPage)
	{
		bool flag = RankManager.GetInstance().GetSkyladderPageData(this.nSeason, nPage).Count > 0;
		if (flag)
		{
			this.ShowSkyladderRankList(this.nSeason, nPage);
		}
		else
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
			{
				17003,
				PlayerDataManager.getInstance().playerCommon.m_nCid,
				this.nSeason,
				nPage,
				10
			});
		}
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x0012D2D8 File Offset: 0x0012B4D8
	private void GetPveRankToServer(int nPage, int nHeroType, int nIsPro)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
		{
			17005,
			PlayerDataManager.getInstance().playerCommon.m_nCid,
			nPage,
			nIsPro,
			nHeroType
		});
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x0012D350 File Offset: 0x0012B550
	private void GetDtsRankToServer(int nPage, int nIsPro)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
		{
			17006,
			PlayerDataManager.getInstance().playerCommon.m_nCid,
			nPage,
			nIsPro
		});
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x0012D3BC File Offset: 0x0012B5BC
	private void GetFlowerRankToServer(int nPage, int nIsPro)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(17000, new ArrayList
		{
			17007,
			PlayerDataManager.getInstance().playerCommon.m_nCid,
			nPage,
			nIsPro
		});
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x0012D428 File Offset: 0x0012B628
	private void InitHeroDropDown()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		this.m_szHeroID.AddRange(fGameManager.mloadConfig.AllHeroList.Keys);
		int count = this.m_szHeroID.Count;
		for (int i = 0; i < count; i++)
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.m_szHeroID[i]];
			bool flag = heroData.heroType == 1;
			if (flag)
			{
				Dropdown.OptionData item = new Dropdown.OptionData(heroData.hname, Window.spriteName("Setting_Atlas", heroData.hhead));
				list.Add(item);
			}
		}
		this.HeroDropDown.AddOptions(list);
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x0012D4D8 File Offset: 0x0012B6D8
	private void InitUserDropDown()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		int count = this.m_szSeasonID.Count;
		this.SeasonDropDown.options.Clear();
		list.Add(new Dropdown.OptionData(Language.GetLang(46095, new object[0])));
		for (int i = 0; i < count; i++)
		{
			list.Add(new Dropdown.OptionData(Language.GetLang(46096, new object[]
			{
				this.m_szSeasonID[i]
			})));
		}
		this.SeasonDropDown.AddOptions(list);
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x0012D570 File Offset: 0x0012B770
	private void OnHeroDropDownChange(int nSelect)
	{
		bool flag = nSelect < this.m_szHeroID.Count;
		if (flag)
		{
			this.m_SelectnHeroID = this.m_szHeroID[nSelect];
			this.nNowPage = 1;
			bool flag2 = !RankManager.GetInstance().IsHadHeroRankData(this.m_nNowMatchType, this.m_SelectnHeroID);
			if (flag2)
			{
				this.GetHeroRankToServer(this.m_SelectnHeroID, 1, 0);
			}
			else
			{
				int myHeroRank = RankManager.GetInstance().GetMyHeroRank(this.m_SelectnHeroID, this.m_nNowMatchType);
				this.SetMyRank(myHeroRank);
				this.ShowHeroRankOnPage(1);
			}
		}
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x0012D604 File Offset: 0x0012B804
	private void OnSeasonDropDownChange(int nSelect)
	{
		bool flag = this.m_nNowMatchType != nSelect;
		if (flag)
		{
			this.m_nNowMatchType = nSelect;
			bool flag2 = !RankManager.GetInstance().IsHadHeroRankData(this.m_nNowMatchType, this.m_SelectnHeroID);
			if (flag2)
			{
				this.GetHeroRankToServer(this.m_SelectnHeroID, 1, 0);
			}
			else
			{
				this.ShowHeroRankList(1);
			}
		}
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x0012D665 File Offset: 0x0012B865
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x0012D66F File Offset: 0x0012B86F
	private void CloseUI(GameObject obj)
	{
		fUIManager.GetInstance().SetWinActive("MainUI", true);
		this.Close();
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x0012D68C File Offset: 0x0012B88C
	private void OnLeftEnd(GameObject obj)
	{
		bool flag = this.nNowPage > 1;
		if (flag)
		{
			switch (this.nNowTabIndex)
			{
			case 0:
				this.ShowUserRankOnPage(1);
				break;
			case 1:
				this.ShowHeroRankOnPage(1);
				break;
			case 2:
				this.GetSkyladderRankToServer(1);
				break;
			case 3:
				this.ShowPveRankOnPage(1);
				break;
			case 4:
				this.ShowDtsRankOnPage(1);
				break;
			case 5:
				this.ShowFlowerRankOnPage(1);
				break;
			}
		}
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x0012D710 File Offset: 0x0012B910
	private void OnLeft(GameObject obj)
	{
		bool flag = this.nNowPage > 1;
		if (flag)
		{
			switch (this.nNowTabIndex)
			{
			case 0:
				this.ShowUserRankOnPage(this.nNowPage - 1);
				break;
			case 1:
				this.ShowHeroRankOnPage(this.nNowPage - 1);
				break;
			case 2:
				this.GetSkyladderRankToServer(this.nNowPage - 1);
				break;
			case 3:
				this.ShowPveRankOnPage(this.nNowPage - 1);
				break;
			case 4:
				this.ShowDtsRankOnPage(this.nNowPage - 1);
				break;
			case 5:
				this.ShowFlowerRankOnPage(this.nNowPage - 1);
				break;
			}
		}
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x0012D7C0 File Offset: 0x0012B9C0
	private void OnRightEnd(GameObject obj)
	{
		bool flag = this.nNowPage < this.nMaxPage;
		if (flag)
		{
			switch (this.nNowTabIndex)
			{
			case 0:
				this.ShowUserRankOnPage(this.nMaxPage);
				break;
			case 1:
				this.ShowHeroRankOnPage(this.nMaxPage);
				break;
			case 2:
				this.GetSkyladderRankToServer(this.nMaxPage);
				break;
			case 3:
				this.ShowPveRankOnPage(this.nMaxPage);
				break;
			case 4:
				this.ShowDtsRankOnPage(this.nMaxPage);
				break;
			case 5:
				this.ShowFlowerRankOnPage(this.nMaxPage);
				break;
			}
		}
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x0012D868 File Offset: 0x0012BA68
	private void OnRight(GameObject obj)
	{
		bool flag = this.nNowPage < this.nMaxPage;
		if (flag)
		{
			switch (this.nNowTabIndex)
			{
			case 0:
				this.ShowUserRankOnPage(this.nNowPage + 1);
				break;
			case 1:
				this.ShowHeroRankOnPage(this.nNowPage + 1);
				break;
			case 2:
				this.GetSkyladderRankToServer(this.nNowPage + 1);
				break;
			case 3:
				this.ShowPveRankOnPage(this.nNowPage + 1);
				break;
			case 4:
				this.ShowDtsRankOnPage(this.nNowPage + 1);
				break;
			case 5:
				this.ShowFlowerRankOnPage(this.nNowPage + 1);
				break;
			}
		}
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x0012D91C File Offset: 0x0012BB1C
	private void UpdatePageBtn()
	{
		this.PageText.text = this.nNowPage.ToString() + " / " + this.nMaxPage.ToString();
		bool flag = this.nMaxPage <= 1;
		if (flag)
		{
			this.RightEndBtn.interactable = false;
			this.RightBtn.interactable = false;
			this.LeftEndBtn.interactable = false;
			this.LeftBtn.interactable = false;
		}
		else
		{
			bool flag2 = this.nNowPage == this.nMaxPage;
			if (flag2)
			{
				this.RightEndBtn.interactable = false;
				this.RightBtn.interactable = false;
				this.LeftEndBtn.interactable = true;
				this.LeftBtn.interactable = true;
			}
			else
			{
				bool flag3 = this.nNowPage > 1 && this.nNowPage < this.nMaxPage;
				if (flag3)
				{
					this.RightEndBtn.interactable = true;
					this.RightBtn.interactable = true;
					this.LeftEndBtn.interactable = true;
					this.LeftBtn.interactable = true;
				}
				else
				{
					this.RightEndBtn.interactable = true;
					this.RightBtn.interactable = true;
					this.LeftEndBtn.interactable = false;
					this.LeftBtn.interactable = false;
				}
			}
		}
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x0012DA7C File Offset: 0x0012BC7C
	public void ShowSkyladderRankList(int nSeason, int nPage)
	{
		this.szSkyLadder = RankManager.GetInstance().GetSkyladderPageData(nSeason, nPage);
		int seasonLadderMaxNum = RankManager.GetInstance().GetSeasonLadderMaxNum(nSeason);
		this.nMaxPage = (seasonLadderMaxNum - 1) / 10 + 1;
		int mySkyladderRank = RankManager.GetInstance().GetMySkyladderRank(nSeason);
		this.SetMyRank(mySkyladderRank);
		this.ShowSkyladderOnPage(nPage);
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x0012DAD4 File Offset: 0x0012BCD4
	private void ShowSkyladderOnPage(int npage)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.szSkyLadder.Count == 0;
		if (flag)
		{
			this.skyladderList.SetActive(false);
			this.skyladderTip.enabled = true;
		}
		else
		{
			this.skyladderTip.enabled = false;
			this.skyladderList.SetActive(true);
		}
		this.nNowPage = npage;
		for (int i = 0; i < 10; i++)
		{
			bool flag2 = i < this.szSkyladderRend.Count;
			GameObject gameObject;
			if (flag2)
			{
				gameObject = this.szSkyladderRend[i];
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.skyladderRender);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.skyladderRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.szSkyladderRend.Add(gameObject);
			}
			arrayList.Clear();
			bool flag3 = i < this.szSkyLadder.Count;
			if (flag3)
			{
				arrayList = (ArrayList)(this.szSkyLadder[i] as ArrayList).Clone();
			}
			this.UpdateSkyladderRender(gameObject, arrayList);
		}
		this.UpdatePageBtn();
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x0012DC3C File Offset: 0x0012BE3C
	private void UpdateSkyladderRender(GameObject obj, ArrayList szData)
	{
		bool flag = szData.Count == 0;
		if (flag)
		{
			Image component = obj.transform.Find("Background").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
			Text component2 = obj.transform.Find("RankIdx").GetComponent<Text>();
			component2.text = string.Empty;
			component = obj.transform.Find("RankIdx/Rankimg").GetComponent<Image>();
			component.enabled = false;
			GameObject gameObject = obj.transform.Find("SkyScore").gameObject;
			gameObject.SetActive(false);
			component2 = obj.transform.Find("NickTxt").GetComponent<Text>();
			component2.text = string.Empty;
			component2 = obj.transform.Find("MatchNum").GetComponent<Text>();
			component2.text = string.Empty;
			RawImage component3 = obj.transform.Find("FaceIcon").GetComponent<RawImage>();
			component3.enabled = false;
			gameObject = obj.transform.Find("HeroList").gameObject;
			gameObject.SetActive(false);
		}
		else
		{
			obj.name = "SkyRender" + szData[0].ToString();
			int num = (int)szData[8];
			int key = (int)szData[2];
			int num2 = (int)szData[6];
			int num3 = (int)szData[4];
			string text = Fun.DecodingUTF8(szData[1].ToString());
			Image component = obj.transform.Find("Background").GetComponent<Image>();
			bool flag2 = (int)szData[0] == PlayerDataManager.getInstance().HUserData.m_nCid;
			if (flag2)
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
			}
			else
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
			}
			Text component2 = obj.transform.Find("RankIdx").GetComponent<Text>();
			component = obj.transform.Find("RankIdx/Rankimg").GetComponent<Image>();
			bool flag3 = num < 4;
			if (flag3)
			{
				component2.text = string.Empty;
				component.enabled = true;
				component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num.ToString());
			}
			else
			{
				component2.text = num.ToString();
				component.enabled = false;
			}
			GameObject gameObject = obj.transform.Find("SkyScore").gameObject;
			gameObject.SetActive(true);
			component = obj.transform.Find("SkyScore/LvlIcon").GetComponent<Image>();
			component2 = obj.transform.Find("SkyScore/LvlIcon/Text").GetComponent<Text>();
			int num4 = RankManager.GetInstance().IsShowSuperIcon(this.nSeason, (int)szData[0], 0);
			int num5 = (num4 != 0) ? 9 : ((int)szData[3]);
			bool flag4 = num5 == 9;
			if (flag4)
			{
				component2.text = string.Format("<color=#00e4ff>{0}</color>", num4);
			}
			else
			{
				component2.text = string.Empty;
			}
			component.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num5.ToString());
			component2 = obj.transform.Find("SkyScore/Score").GetComponent<Text>();
			component2.text = this.GetTiTle(num5);
			component2 = obj.transform.Find("NickTxt").GetComponent<Text>();
			component2.text = text;
			component2 = obj.transform.Find("MatchNum").GetComponent<Text>();
			component2.text = Language.GetLang(5045, new object[]
			{
				num3
			});
			bool flag5 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
			if (flag5)
			{
				RawImage component4 = obj.transform.Find("FaceIcon").GetComponent<RawImage>();
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component4);
			}
			ArrayList arrayList = (ArrayList)szData[7];
			gameObject = obj.transform.Find("HeroList").gameObject;
			gameObject.SetActive(true);
			for (int i = 0; i < 3; i++)
			{
				RawImage component5 = obj.transform.Find("HeroList/HeroIcon" + (i + 1).ToString()).GetComponent<RawImage>();
				component2 = obj.transform.Find("HeroList/Num" + (i + 1).ToString()).GetComponent<Text>();
				bool flag6 = i * 2 < arrayList.Count;
				if (flag6)
				{
					int key2 = (int)arrayList[i * 2];
					int num6 = (int)arrayList[i * 2 + 1];
					bool flag7 = fGameManager.mloadConfig.AllHeroList.ContainsKey(key2);
					if (flag7)
					{
						HeroData heroData = fGameManager.mloadConfig.AllHeroList[key2];
						component2.text = (i + 1).ToString();
						fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", component5);
						EventTriggerListener.Get(component5.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverFaceIcon);
						EventTriggerListener.Get(component5.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitFaceIcon);
					}
				}
				else
				{
					component2.text = string.Empty;
					component5.enabled = false;
				}
			}
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x0012E208 File Offset: 0x0012C408
	private string GetTiTle(int nRank)
	{
		if (!true)
		{
		}
		string result;
		switch (nRank)
		{
		case 1:
			result = Language.GetLang(46097, new object[0]);
			break;
		case 2:
			result = Language.GetLang(46098, new object[0]);
			break;
		case 3:
			result = Language.GetLang(46099, new object[0]);
			break;
		case 4:
			result = Language.GetLang(46100, new object[0]);
			break;
		case 5:
			result = Language.GetLang(46101, new object[0]);
			break;
		case 6:
			result = Language.GetLang(46102, new object[0]);
			break;
		case 7:
			result = Language.GetLang(46103, new object[0]);
			break;
		case 8:
			result = Language.GetLang(46104, new object[0]);
			break;
		case 9:
			result = Language.GetLang(46105, new object[0]);
			break;
		default:
			result = string.Empty;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x0012E310 File Offset: 0x0012C510
	public void UpdateSeasonList()
	{
		this.nDefSeason = int.Parse(this.m_szSeasonID[this.m_szSeasonID.Count - 1]);
		this.skyladderDropDown.ClearOptions();
		this.skyladderDropDown.AddOptions(this.m_szSeasonID);
		this.nSeason = this.nDefSeason;
		this.skyladderDropDown.value = this.m_szSeasonID.Count - 1;
		this.GetSkyladderRankToServer(1);
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x0012E38C File Offset: 0x0012C58C
	private void OnSkyladderDropDown(int nSelect)
	{
		bool flag = nSelect < this.m_szSeasonID.Count;
		if (flag)
		{
			this.nSeason = int.Parse(this.m_szSeasonID[nSelect]);
			this.GetSkyladderRankToServer(1);
		}
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x0012E3D0 File Offset: 0x0012C5D0
	private GameObject GetFreeRankItem()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.RankItem);
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x0012E3F0 File Offset: 0x0012C5F0
	private void OnClickMyRank(GameObject obj)
	{
		switch (this.nNowTabIndex)
		{
		case 0:
			this.OnGoToMyUserRank();
			break;
		case 1:
			this.OnGoToMyHeroRank();
			break;
		case 2:
			this.OnGoToMyLadderRank();
			break;
		case 3:
			this.OnGoToMyPveRank();
			break;
		case 4:
			this.OnGoToMyDtsRank();
			break;
		case 5:
			this.OnGoToMyFlowerRank();
			break;
		}
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x0012E460 File Offset: 0x0012C660
	private void OnGoToMyUserRank()
	{
		bool flag = this.nNowTabIndex == 0;
		if (flag)
		{
			int myLevelRank = RankManager.GetInstance().GetMyLevelRank();
			bool flag2 = myLevelRank > 0;
			if (flag2)
			{
				int nPage = (myLevelRank - 1) / 10 + 1;
				this.ShowUserRankOnPage(nPage);
			}
		}
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x0012E4A4 File Offset: 0x0012C6A4
	private void OnGoToMyHeroRank()
	{
		bool flag = this.nNowTabIndex == 1;
		if (flag)
		{
			int myHeroRank = RankManager.GetInstance().GetMyHeroRank(this.m_SelectnHeroID, this.m_nNowMatchType);
			bool flag2 = myHeroRank > 0;
			if (flag2)
			{
				int nPage = (myHeroRank - 1) / 10 + 1;
				this.ShowHeroRankOnPage(nPage);
			}
		}
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x0012E4F4 File Offset: 0x0012C6F4
	private void OnGoToMyLadderRank()
	{
		bool flag = this.nNowTabIndex == 2;
		if (flag)
		{
			int mySkyladderRank = RankManager.GetInstance().GetMySkyladderRank(this.nSeason);
			bool flag2 = mySkyladderRank > 0;
			if (flag2)
			{
				int nPage = (mySkyladderRank - 1) / 10 + 1;
				this.GetSkyladderRankToServer(nPage);
			}
		}
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x0012E540 File Offset: 0x0012C740
	private void OnGoToMyPveRank()
	{
		bool flag = this.nNowTabIndex == 3;
		if (flag)
		{
			int myPveRank = RankManager.GetInstance().GetMyPveRank(this.m_nPveHeroType);
			bool flag2 = myPveRank > 0;
			if (flag2)
			{
				int nPage = (myPveRank - 1) / 10 + 1;
				this.ShowPveRankOnPage(nPage);
			}
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x0012E58C File Offset: 0x0012C78C
	private void ShowPveRankOnPage(int nPage)
	{
		bool flag = nPage % 10 == 5;
		if (flag)
		{
			int num = nPage + 6;
			bool flag2 = num <= this.nMaxPage && !RankManager.GetInstance().HadPvePage(num, this.m_nPveHeroType);
			if (flag2)
			{
				this.GetPveRankToServer(num, this.m_nPveHeroType, 1);
			}
			num -= 20;
			bool flag3 = num > 0 && !RankManager.GetInstance().HadPvePage(num, this.m_nPveHeroType);
			if (flag3)
			{
				this.GetPveRankToServer(num, this.m_nPveHeroType, 1);
			}
		}
		bool flag4 = nPage == 1 && !RankManager.GetInstance().HadPvePage(this.nMaxPage, this.m_nPveHeroType);
		if (flag4)
		{
			bool flag5 = base.IsInvoking("DalayGetUserData");
			if (flag5)
			{
				base.CancelInvoke("DalayGetUserData");
			}
			base.Invoke("DalayGetUserData", 0.1f);
		}
		ArrayList pveListByPage = RankManager.GetInstance().GetPveListByPage(nPage, this.m_nPveHeroType);
		int count = pveListByPage.Count;
		bool flag6 = count <= 0;
		if (flag6)
		{
			this.GetPveRankToServer(nPage, this.m_nPveHeroType, 1);
		}
		else
		{
			fUIManager.OnDelRawImage();
			fUIManager.OnClearObj(this.szPveRank);
			this.nNowPage = nPage;
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)pveListByPage[i];
				bool flag7 = arrayList.Count > 0;
				if (flag7)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PveItem);
					this.szPveRank.Add(gameObject);
					gameObject.transform.SetParent(this.PveRankList.transform, false);
					gameObject.transform.localScale = Vector3.one;
					this.UpdatePveRankItem(gameObject, arrayList);
				}
			}
			this.UpdatePageBtn();
		}
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x0012E759 File Offset: 0x0012C959
	public void ShowPveRankNotRankOK()
	{
		fUIManager.OnClearObj(this.szPveRank);
		this.nMaxPage = 1;
		this.nNowPage = 1;
		this.UpdatePageBtn();
		this.SetMyRank(-1);
		this.PveRankTip.SetActive(true);
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x0012E794 File Offset: 0x0012C994
	public void ShowPveRankList(int nPage)
	{
		int maxPveSize = RankManager.GetInstance().GetMaxPveSize(this.m_nPveHeroType);
		this.nMaxPage = (maxPveSize - 1) / 10 + 1;
		bool flag = maxPveSize <= 0;
		if (flag)
		{
			this.PveRankTip.SetActive(true);
		}
		else
		{
			this.PveRankTip.SetActive(false);
		}
		int myPveRank = RankManager.GetInstance().GetMyPveRank(this.m_nPveHeroType);
		this.SetMyRank(myPveRank);
		this.ShowPveRankOnPage(nPage);
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x0012E810 File Offset: 0x0012CA10
	private void UpdatePveRankItem(GameObject obj, ArrayList szUser)
	{
		int num = (int)szUser[7];
		int key = (int)szUser[4];
		int num2 = (int)szUser[5];
		int num3 = (int)szUser[2];
		string text = Fun.DecodingUTF8(szUser[1].ToString());
		int num4 = (int)szUser[8];
		int num5 = (int)szUser[9];
		obj.SetActive(true);
		bool flag = num < 4;
		if (flag)
		{
			Image component = obj.transform.Find("rankimg").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num.ToString());
			component.gameObject.SetActive(true);
		}
		else
		{
			obj.transform.Find("rank").GetComponent<Text>().text = num.ToString();
		}
		Image component2 = obj.transform.Find("zdlimg").GetComponent<Image>();
		Image image = component2;
		string atlas_PVEFB = AtlasType.ATLAS_PVEFB;
		if (!true)
		{
		}
		string imageName;
		if (num5 != 0)
		{
			if (num5 != 1)
			{
				imageName = "fz";
			}
			else
			{
				imageName = "f";
			}
		}
		else
		{
			imageName = "g";
		}
		if (!true)
		{
		}
		image.sprite = Window.spriteName(atlas_PVEFB, imageName);
		obj.transform.Find("level/num").GetComponent<Text>().text = num3.ToString();
		obj.transform.Find("unick").GetComponent<Text>().text = text.ToString();
		obj.transform.Find("matchnum").GetComponent<Text>().text = (num4.ToString() ?? "");
		bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
		if (flag2)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			fUIManager.OnAddRawImage(obj.transform.Find("head").GetComponent<RawImage>());
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", obj.transform.Find("head").GetComponent<RawImage>());
		}
		bool flag3 = num == RankManager.GetInstance().GetMyPveRank(this.m_nPveHeroType);
		if (flag3)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
		}
		else
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
		}
		string text2 = szUser[6].ToString();
		string[] array = text2.Split(new char[]
		{
			','
		});
		int num6 = array.Length / 2;
		bool flag4 = num6 > 3;
		if (flag4)
		{
			num6 = 3;
		}
		for (int i = 0; i < num6; i++)
		{
			string name = "herolistitem/herohead" + (i + 1).ToString();
			string name2 = "herolistitem/num" + (i + 1).ToString();
			int key2 = int.Parse(array[i * 2]);
			int num7 = int.Parse(array[i * 2 + 1]);
			bool flag5 = fGameManager.mloadConfig.AllHeroList.ContainsKey(key2);
			if (flag5)
			{
				GameObject gameObject = obj.transform.Find(name).gameObject;
				obj.transform.Find(name2).GetComponent<Text>().text = (i + 1).ToString();
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[key2];
				fUIManager.OnAddRawImage(gameObject.GetComponent<RawImage>());
				fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", gameObject.GetComponent<RawImage>());
				obj.transform.Find(name).gameObject.SetActive(true);
				gameObject.name = heroData.hname + ":" + num7.ToString();
			}
		}
		obj.transform.Find("herolistitem").gameObject.SetActive(true);
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x0012EC58 File Offset: 0x0012CE58
	private void OnGoToMyDtsRank()
	{
		bool flag = this.nNowTabIndex == 4;
		if (flag)
		{
			int myDtsRank = RankManager.GetInstance().GetMyDtsRank();
			bool flag2 = myDtsRank > 0;
			if (flag2)
			{
				int nPage = (myDtsRank - 1) / 10 + 1;
				this.ShowDtsRankOnPage(nPage);
			}
		}
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x0012EC9B File Offset: 0x0012CE9B
	public void ShowDtsRankNotRankOK()
	{
		fUIManager.OnClearObj(this.szDtsRank);
		this.nMaxPage = 1;
		this.nNowPage = 1;
		this.UpdatePageBtn();
		this.SetMyRank(-1);
		this.DtsRankTip.SetActive(true);
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x0012ECD4 File Offset: 0x0012CED4
	private void ShowDtsRankOnPage(int nPage)
	{
		bool flag = nPage % 10 == 5;
		if (flag)
		{
			int num = nPage + 6;
			bool flag2 = num <= this.nMaxPage && !RankManager.GetInstance().HadDtsPage(num);
			if (flag2)
			{
				this.GetDtsRankToServer(num, 1);
			}
			num -= 20;
			bool flag3 = num > 0 && !RankManager.GetInstance().HadDtsPage(num);
			if (flag3)
			{
				this.GetDtsRankToServer(num, 1);
			}
		}
		bool flag4 = nPage == 1 && !RankManager.GetInstance().HadDtsPage(this.nMaxPage);
		if (flag4)
		{
			bool flag5 = base.IsInvoking("DalayGetUserData");
			if (flag5)
			{
				base.CancelInvoke("DalayGetUserData");
			}
			base.Invoke("DalayGetUserData", 0.1f);
		}
		ArrayList dtsListByPage = RankManager.GetInstance().GetDtsListByPage(nPage);
		int count = dtsListByPage.Count;
		bool flag6 = count <= 0;
		if (flag6)
		{
			this.GetDtsRankToServer(nPage, 0);
		}
		else
		{
			fUIManager.OnDelRawImage();
			fUIManager.OnClearObj(this.szDtsRank);
			this.nNowPage = nPage;
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)dtsListByPage[i];
				bool flag7 = arrayList.Count > 0;
				if (flag7)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DtsItem);
					this.szDtsRank.Add(gameObject);
					gameObject.transform.SetParent(this.DtsRankList.transform, false);
					gameObject.transform.localScale = Vector3.one;
					this.UpdateDtsRankItem(gameObject, arrayList);
				}
			}
			this.UpdatePageBtn();
		}
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x0012EE78 File Offset: 0x0012D078
	public void ShowDtsRankList(int nPage)
	{
		int maxDtsSize = RankManager.GetInstance().GetMaxDtsSize();
		this.nMaxPage = (maxDtsSize - 1) / 10 + 1;
		bool flag = maxDtsSize <= 0;
		if (flag)
		{
			this.DtsRankTip.SetActive(true);
		}
		else
		{
			this.DtsRankTip.SetActive(false);
		}
		int myDtsRank = RankManager.GetInstance().GetMyDtsRank();
		this.SetMyRank(myDtsRank);
		this.ShowDtsRankOnPage(nPage);
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x0012EEE8 File Offset: 0x0012D0E8
	private void UpdateDtsRankItem(GameObject obj, ArrayList szUser)
	{
		string text = Fun.DecodingUTF8(szUser[1].ToString());
		int num = (int)szUser[2];
		int key = (int)szUser[4];
		int num2 = (int)szUser[5];
		int num3 = (int)szUser[6];
		int num4 = (int)szUser[7];
		int num5 = (int)szUser[8];
		obj.SetActive(true);
		bool flag = num4 < 4;
		if (flag)
		{
			Image component = obj.transform.Find("rankimg").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num4.ToString());
			component.gameObject.SetActive(true);
		}
		else
		{
			obj.transform.Find("rank").GetComponent<Text>().text = num4.ToString();
		}
		obj.transform.Find("level/num").GetComponent<Text>().text = num.ToString();
		obj.transform.Find("unick").GetComponent<Text>().text = text.ToString();
		obj.transform.Find("killnum").GetComponent<Text>().text = (num2.ToString() ?? "");
		obj.transform.Find("winnum").GetComponent<Text>().text = (num3.ToString() ?? "");
		obj.transform.Find("maxkillnum").GetComponent<Text>().text = (num5.ToString() ?? "");
		bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
		if (flag2)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			fUIManager.OnAddRawImage(obj.transform.Find("head").GetComponent<RawImage>());
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", obj.transform.Find("head").GetComponent<RawImage>());
		}
		bool flag3 = num4 == RankManager.GetInstance().GetMyDtsRank();
		if (flag3)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
		}
		else
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
		}
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x0012F180 File Offset: 0x0012D380
	private void OnGoToMyFlowerRank()
	{
		bool flag = this.nNowTabIndex == 5;
		if (flag)
		{
			int myFlowerRank = RankManager.GetInstance().GetMyFlowerRank();
			bool flag2 = myFlowerRank > 0;
			if (flag2)
			{
				int nPage = (myFlowerRank - 1) / 10 + 1;
				this.ShowFlowerRankOnPage(nPage);
			}
		}
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x0012F1C3 File Offset: 0x0012D3C3
	public void ShowFlowerRankNotRankOK()
	{
		fUIManager.OnClearObj(this.szFlowerRank);
		this.nMaxPage = 1;
		this.nNowPage = 1;
		this.UpdatePageBtn();
		this.SetMyRank(-1);
		this.flowerRankTip.SetActive(true);
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x0012F1FC File Offset: 0x0012D3FC
	private void ShowFlowerRankOnPage(int nPage)
	{
		bool flag = nPage % 10 == 5;
		if (flag)
		{
			int num = nPage + 6;
			bool flag2 = num <= this.nMaxPage && !RankManager.GetInstance().HadFlowerPage(num);
			if (flag2)
			{
				this.GetFlowerRankToServer(num, 1);
			}
			num -= 20;
			bool flag3 = num > 0 && !RankManager.GetInstance().HadFlowerPage(num);
			if (flag3)
			{
				this.GetFlowerRankToServer(num, 1);
			}
		}
		bool flag4 = nPage == 1 && !RankManager.GetInstance().HadFlowerPage(this.nMaxPage);
		if (flag4)
		{
			bool flag5 = base.IsInvoking("DalayGetUserData");
			if (flag5)
			{
				base.CancelInvoke("DalayGetUserData");
			}
			base.Invoke("DalayGetUserData", 0.1f);
		}
		ArrayList flowerListByPage = RankManager.GetInstance().GetFlowerListByPage(nPage);
		int count = flowerListByPage.Count;
		bool flag6 = count <= 0;
		if (flag6)
		{
			this.GetFlowerRankToServer(nPage, 0);
		}
		else
		{
			fUIManager.OnDelRawImage();
			fUIManager.OnClearObj(this.szFlowerRank);
			this.nNowPage = nPage;
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)flowerListByPage[i];
				bool flag7 = arrayList.Count > 0;
				if (flag7)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.flowerItem);
					this.szFlowerRank.Add(gameObject);
					gameObject.transform.SetParent(this.flowerRankList.transform, false);
					gameObject.transform.localScale = Vector3.one;
					this.UpdateFlowerRankItem(gameObject, arrayList);
				}
			}
			this.UpdatePageBtn();
		}
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x0012F3A0 File Offset: 0x0012D5A0
	public void ShowFlowerRankList(int nPage)
	{
		int maxFlowerSize = RankManager.GetInstance().GetMaxFlowerSize();
		this.nMaxPage = (maxFlowerSize - 1) / 10 + 1;
		bool flag = maxFlowerSize <= 0;
		if (flag)
		{
			this.flowerRankTip.SetActive(true);
		}
		else
		{
			this.flowerRankTip.SetActive(false);
		}
		int myFlowerRank = RankManager.GetInstance().GetMyFlowerRank();
		this.SetMyRank(myFlowerRank);
		this.ShowFlowerRankOnPage(nPage);
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x0012F410 File Offset: 0x0012D610
	private void UpdateFlowerRankItem(GameObject obj, ArrayList szUser)
	{
		string text = Fun.DecodingUTF8(szUser[1].ToString());
		int num = (int)szUser[2];
		int key = (int)szUser[3];
		int num2 = (int)szUser[4];
		int num3 = (int)szUser[5];
		obj.SetActive(true);
		Image component = obj.transform.Find("rankimg").GetComponent<Image>();
		bool flag = num3 < 4;
		if (flag)
		{
			component.sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, num3.ToString());
			component.enabled = true;
		}
		else
		{
			component.enabled = false;
			obj.transform.Find("rank").GetComponent<Text>().text = num3.ToString();
		}
		obj.transform.Find("level/num").GetComponent<Text>().text = num.ToString();
		obj.transform.Find("unick").GetComponent<Text>().text = text.ToString();
		obj.transform.Find("numtxt").GetComponent<Text>().text = num2.ToString();
		bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
		if (flag2)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			fUIManager.OnAddRawImage(obj.transform.Find("head").GetComponent<RawImage>());
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", obj.transform.Find("head").GetComponent<RawImage>());
		}
		bool flag3 = num3 == RankManager.GetInstance().GetMyFlowerRank();
		if (flag3)
		{
			obj.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "huangtiao");
		}
		else
		{
			obj.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_RANKUI, "baitiao");
		}
	}

	// Token: 0x04001DA8 RID: 7592
	public static RankUICon inst;

	// Token: 0x04001DA9 RID: 7593
	private GameObject Tab1;

	// Token: 0x04001DAA RID: 7594
	private GameObject Tab2;

	// Token: 0x04001DAB RID: 7595
	private GameObject Tab3;

	// Token: 0x04001DAC RID: 7596
	private GameObject Tab4;

	// Token: 0x04001DAD RID: 7597
	private GameObject Tab5;

	// Token: 0x04001DAE RID: 7598
	private GameObject Tab6;

	// Token: 0x04001DAF RID: 7599
	private GameObject LevelRankPanel;

	// Token: 0x04001DB0 RID: 7600
	private GameObject HeroRankPanel;

	// Token: 0x04001DB1 RID: 7601
	private GameObject LadderRankPanel;

	// Token: 0x04001DB2 RID: 7602
	private GameObject PveRankPanel;

	// Token: 0x04001DB3 RID: 7603
	private GameObject DtsRankPanel;

	// Token: 0x04001DB4 RID: 7604
	private GameObject flowerRankPanel;

	// Token: 0x04001DB5 RID: 7605
	private GameObject RankItem;

	// Token: 0x04001DB6 RID: 7606
	private GameObject PveItem;

	// Token: 0x04001DB7 RID: 7607
	private GameObject DtsItem;

	// Token: 0x04001DB8 RID: 7608
	private GameObject flowerItem;

	// Token: 0x04001DB9 RID: 7609
	private GameObject LevelRankList;

	// Token: 0x04001DBA RID: 7610
	private GameObject HeroRankList;

	// Token: 0x04001DBB RID: 7611
	private GameObject PveRankList;

	// Token: 0x04001DBC RID: 7612
	private GameObject DtsRankList;

	// Token: 0x04001DBD RID: 7613
	private GameObject flowerRankList;

	// Token: 0x04001DBE RID: 7614
	private Button LeftEndBtn;

	// Token: 0x04001DBF RID: 7615
	private Button LeftBtn;

	// Token: 0x04001DC0 RID: 7616
	private Button RightEndBtn;

	// Token: 0x04001DC1 RID: 7617
	private Button RightBtn;

	// Token: 0x04001DC2 RID: 7618
	private Text PageText;

	// Token: 0x04001DC3 RID: 7619
	private Dropdown HeroDropDown;

	// Token: 0x04001DC4 RID: 7620
	private Dropdown SeasonDropDown;

	// Token: 0x04001DC5 RID: 7621
	private Dropdown skyladderDropDown;

	// Token: 0x04001DC6 RID: 7622
	private Dropdown PveHeroTypeDropDown;

	// Token: 0x04001DC7 RID: 7623
	private GameObject skyladderRender;

	// Token: 0x04001DC8 RID: 7624
	private GameObject skyladderList;

	// Token: 0x04001DC9 RID: 7625
	private Text skyladderTip;

	// Token: 0x04001DCA RID: 7626
	private List<GameObject> szSkyladderRend = new List<GameObject>();

	// Token: 0x04001DCB RID: 7627
	private int nSeason;

	// Token: 0x04001DCC RID: 7628
	private int nDefSeason;

	// Token: 0x04001DCD RID: 7629
	private List<string> m_szSeasonID = new List<string>();

	// Token: 0x04001DCE RID: 7630
	private ArrayList szSkyLadder = new ArrayList();

	// Token: 0x04001DCF RID: 7631
	private int nNowTabIndex = -1;

	// Token: 0x04001DD0 RID: 7632
	private int nNowPage = 1;

	// Token: 0x04001DD1 RID: 7633
	private int nMaxPage = 1;

	// Token: 0x04001DD2 RID: 7634
	private List<int> m_szHeroID = new List<int>();

	// Token: 0x04001DD3 RID: 7635
	private int m_SelectnHeroID = 1003;

	// Token: 0x04001DD4 RID: 7636
	private int m_nNowMatchType;

	// Token: 0x04001DD5 RID: 7637
	private List<GameObject> szUserRank = new List<GameObject>();

	// Token: 0x04001DD6 RID: 7638
	private List<GameObject> szHeroRank = new List<GameObject>();

	// Token: 0x04001DD7 RID: 7639
	private List<GameObject> szPveRank = new List<GameObject>();

	// Token: 0x04001DD8 RID: 7640
	private List<GameObject> szDtsRank = new List<GameObject>();

	// Token: 0x04001DD9 RID: 7641
	private List<GameObject> szFlowerRank = new List<GameObject>();

	// Token: 0x04001DDA RID: 7642
	private GameObject oNowSelectTab;

	// Token: 0x04001DDB RID: 7643
	private Text myRankTxt;

	// Token: 0x04001DDC RID: 7644
	private GameObject LevelRankTip;

	// Token: 0x04001DDD RID: 7645
	private GameObject HeroRankTip;

	// Token: 0x04001DDE RID: 7646
	private GameObject PveRankTip;

	// Token: 0x04001DDF RID: 7647
	private GameObject DtsRankTip;

	// Token: 0x04001DE0 RID: 7648
	private GameObject flowerRankTip;

	// Token: 0x04001DE1 RID: 7649
	private List<QiangHuaHeroTypeXmlData> m_szPveHeroTypeID = new List<QiangHuaHeroTypeXmlData>();

	// Token: 0x04001DE2 RID: 7650
	private int m_nPveHeroType;
}

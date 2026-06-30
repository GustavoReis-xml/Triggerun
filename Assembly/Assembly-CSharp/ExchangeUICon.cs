using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200011F RID: 287
public class ExchangeUICon : Window
{
	// Token: 0x06000A5C RID: 2652 RVA: 0x0005D1DC File Offset: 0x0005B3DC
	protected override void Awake()
	{
		base.Awake();
		this.tabToggle = this.UIGameObj.Get_GameObj(0).GetComponent<Toggle>();
		this.tabToggle.gameObject.SetActive(false);
		GameObject item = this.UIGameObj.Get_GameObj(1);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(2);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(3);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(4);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(5);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(6);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(7);
		this.szGridList.Add(item);
		item = this.UIGameObj.Get_GameObj(8);
		this.szGridList.Add(item);
		this.preBtn = this.UIGameObj.Get_GameObj(9).GetComponent<Button>();
		EventTriggerListener.Get(this.preBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangePage);
		EventTriggerListener.Get(this.preBtn.gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.nextBtn = this.UIGameObj.Get_GameObj(10).GetComponent<Button>();
		EventTriggerListener.Get(this.nextBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangePage);
		EventTriggerListener.Get(this.nextBtn.gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.pageNumTxt = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.heroDropdown = this.UIGameObj.Get_GameObj(12).GetComponent<Dropdown>();
		this.heroDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDropDown));
		this.chipPanel = this.UIGameObj.Get_GameObj(13);
		EventTriggerListener.Get(this.chipPanel).onEnter = new EventTriggerListener.VoidDelegate(this.OverChipPanel);
		EventTriggerListener.Get(this.chipPanel).onExit = new EventTriggerListener.VoidDelegate(this.ExitChipPanel);
		this.chipIcon = this.UIGameObj.Get_GameObj(14).GetComponent<RawImage>();
		this.chipNum = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		ExchangeUICon.inst = this;
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0005D46C File Offset: 0x0005B66C
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.SetExchangeData();
		this.szTabBtn[0].isOn = true;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0005D4B3 File Offset: 0x0005B6B3
	private void Update()
	{
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0005D4B8 File Offset: 0x0005B6B8
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.SetGridList();
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0005D4F4 File Offset: 0x0005B6F4
	public void SetExchangeData()
	{
		foreach (ExchangeData exchangeData in fGameManager.mloadConfig.AllExchangeData.Values)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tabToggle.gameObject);
			gameObject.transform.SetParent(this.tabToggle.transform.parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localScale = Vector3.one;
			int nIdx = exchangeData.nId;
			gameObject.name = "Toggle" + nIdx.ToString();
			gameObject.SetActive(true);
			Toggle component = gameObject.GetComponent<Toggle>();
			component.onValueChanged.AddListener(delegate(bool v)
			{
				this.OnTabChange(nIdx, v);
			});
			Text component2 = gameObject.transform.Find("Label").GetComponent<Text>();
			component2.text = exchangeData.sTabName;
			this.szTabBtn.Add(component);
		}
		List<string> list = new List<string>();
		list.Add(Language.GetLang(35001, new object[0]));
		this.szHeroList.Add(-1);
		list.Add(Language.GetLang(35002, new object[0]));
		this.szHeroList.Add(0);
		foreach (HeroData heroData in fGameManager.mloadConfig.AllHeroList.Values)
		{
			bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(heroData.key);
			if (flag)
			{
				list.Add(heroData.hname);
				this.szHeroList.Add(heroData.key);
			}
		}
		this.heroDropdown.AddOptions(list);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0005D750 File Offset: 0x0005B950
	private void OnTabChange(int nIdx, bool v)
	{
		if (v)
		{
			this.ChangeToIdx(nIdx);
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0005D770 File Offset: 0x0005B970
	public void ChangeToIdx(int idx)
	{
		bool flag = idx == this.nSlcTab;
		if (!flag)
		{
			this.nSlcTab = idx;
			this.nCurPage = 0;
			bool flag2 = this.nSlcTab == 3;
			if (flag2)
			{
				this.heroDropdown.gameObject.SetActive(true);
				bool flag3 = this.heroDropdown.value != 0;
				if (flag3)
				{
					this.heroDropdown.value = 0;
				}
				else
				{
					this.OnDropDown(0);
				}
			}
			else
			{
				this.heroDropdown.gameObject.SetActive(false);
				this.SetGridList();
			}
		}
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0005D808 File Offset: 0x0005BA08
	private void OnDropDown(int nidx)
	{
		bool flag = nidx >= 0 && nidx < this.szHeroList.Count;
		if (flag)
		{
			this.nCommon = this.szHeroList[nidx];
			this.nCurPage = 0;
			this.SetGridList();
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0005D854 File Offset: 0x0005BA54
	private void OnChangePage(GameObject go)
	{
		bool flag = go.name == "PreBtn";
		if (flag)
		{
			this.nCurPage--;
		}
		bool flag2 = go.name == "NextBtn";
		if (flag2)
		{
			this.nCurPage++;
		}
		bool flag3 = this.nCurPage < 0;
		if (flag3)
		{
			this.nCurPage = 0;
		}
		bool flag4 = this.nCurPage >= this.nMaxPage;
		if (flag4)
		{
			this.nCurPage = this.nMaxPage - 1;
		}
		this.SetGridList();
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0005D8EC File Offset: 0x0005BAEC
	public void SetGridList()
	{
		bool flag = !fGameManager.mloadConfig.AllExchangeData.ContainsKey(this.nSlcTab);
		if (!flag)
		{
			ExchangeData exchangeData = fGameManager.mloadConfig.AllExchangeData[this.nSlcTab];
			List<ExchangeInfo> exchangeInfo = exchangeData.GetExchangeInfo(this.nCommon);
			this.nMaxPage = Mathf.CeilToInt((float)exchangeInfo.Count * 1f / (float)this.szGridList.Count * 1f);
			bool flag2 = this.nMaxPage > 0;
			if (flag2)
			{
				this.pageNumTxt.text = string.Format("{0}/{1}", this.nCurPage + 1, this.nMaxPage);
			}
			else
			{
				this.nCurPage = 0;
				this.pageNumTxt.text = "0/0";
			}
			bool flag3 = this.nCurPage == 0;
			if (flag3)
			{
				this.preBtn.interactable = false;
			}
			else
			{
				this.preBtn.interactable = true;
			}
			bool flag4 = this.nCurPage == this.nMaxPage - 1 || this.nMaxPage == 0;
			if (flag4)
			{
				this.nextBtn.interactable = false;
			}
			else
			{
				this.nextBtn.interactable = true;
			}
			int i = 0;
			while (i < this.szGridList.Count)
			{
				GameObject gameObject = this.szGridList[i];
				bool flag5 = this.nCurPage * this.szGridList.Count + i < exchangeInfo.Count;
				if (flag5)
				{
					ExchangeInfo exchangeInfo2 = exchangeInfo[this.nCurPage * this.szGridList.Count + i];
					gameObject.SetActive(true);
					gameObject.name = string.Format("Render{0}", exchangeInfo2.nIdx);
					Text component = gameObject.transform.Find("TitleTxt").GetComponent<Text>();
					component.text = exchangeInfo2.sTitle;
					GameObject gameObject2 = gameObject.transform.Find("Image").gameObject;
					EventTriggerListener.Get(gameObject2).onEnter = new EventTriggerListener.VoidDelegate(this.OverExchangeGrid);
					EventTriggerListener.Get(gameObject2).onExit = new EventTriggerListener.VoidDelegate(this.ExitExchangeGrid);
					RawImage component2 = gameObject.transform.Find("Image/RawImage").GetComponent<RawImage>();
					component2.rectTransform.sizeDelta = exchangeData.iconSize;
					fUIManager.GetInstance().GetIconAsset(string.Format(exchangeData.sIconUrl, exchangeInfo2.sIcon), component2);
					component = gameObject.transform.Find("NameTxt").GetComponent<Text>();
					bool flag6 = exchangeInfo2.nPerNum > 1;
					if (flag6)
					{
						component.text = string.Concat(new string[]
						{
							"<color=",
							Fun.GetQualColor(exchangeInfo2.itemData.m_nQuality),
							">",
							string.Format(exchangeInfo2.itemData.m_sLiBaoDesc, exchangeInfo2.nPerNum),
							"</color>"
						});
					}
					else
					{
						component.text = string.Concat(new string[]
						{
							"<color=",
							Fun.GetQualColor(exchangeInfo2.itemData.m_nQuality),
							">",
							exchangeInfo2.itemData.m_sName,
							"</color>"
						});
					}
					component = gameObject.transform.Find("NeedDesc").GetComponent<Text>();
					component.text = Language.GetLang(35003, new object[]
					{
						exchangeInfo2.exItemData.m_sName
					});
					component2 = gameObject.transform.Find("ExchangeIcon").GetComponent<RawImage>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(exchangeInfo2.exItemData.m_nType), exchangeInfo2.exItemData.m_sIco), component2);
					component = gameObject.transform.Find("ExchangeIcon/NumTxt").GetComponent<Text>();
					Button component3 = gameObject.transform.Find("Button").GetComponent<Button>();
					int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(exchangeInfo2.nExItemId);
					bool flag7 = haveItemNum < exchangeInfo2.nExNum;
					if (flag7)
					{
						component.text = string.Format("<color=#ff0000>{0}/{1}</color>", haveItemNum, exchangeInfo2.nExNum);
						component3.interactable = false;
					}
					else
					{
						component.text = string.Format("<color=#00ff00>{0}/{1}</color>", haveItemNum, exchangeInfo2.nExNum);
						component3.interactable = true;
						EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnExchangeClick);
						EventTriggerListener.Get(component3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
					}
				}
				else
				{
					gameObject.SetActive(false);
				}
				IL_4D3:
				i++;
				continue;
				goto IL_4D3;
			}
			this.SetChipPanel();
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	private void OnExchangeClick(GameObject go)
	{
		int nIdx = int.Parse(go.transform.parent.name.Replace("Render", string.Empty));
		bool flag = fGameManager.mloadConfig.AllExchangeData.ContainsKey(this.nSlcTab);
		if (flag)
		{
			ExchangeData exchangeData = fGameManager.mloadConfig.AllExchangeData[this.nSlcTab];
			ExchangeInfo oneInfo = exchangeData.GetOneInfo(nIdx);
			bool flag2 = oneInfo != null;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(35004, new object[]
				{
					oneInfo.exItemData.m_sName,
					oneInfo.nExNum,
					oneInfo.itemData.m_sName
				}), Language.GetLang(35000, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmExchange), oneInfo, null, null);
			}
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0005DEFC File Offset: 0x0005C0FC
	private void OnConfirmExchange(object szParam)
	{
		ExchangeInfo exchangeInfo = (ExchangeInfo)szParam;
		int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(exchangeInfo.nExItemId);
		bool flag = haveItemNum >= exchangeInfo.nExNum;
		if (flag)
		{
			ShopManager.ShopClientToServer(70019, new ArrayList
			{
				this.nSlcTab,
				exchangeInfo.nIdx
			});
		}
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0005DF68 File Offset: 0x0005C168
	private void ExitExchangeGrid(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0005DF78 File Offset: 0x0005C178
	private void OverExchangeGrid(GameObject go)
	{
		int nIdx = int.Parse(go.transform.parent.name.Replace("Render", string.Empty));
		bool flag = fGameManager.mloadConfig.AllExchangeData.ContainsKey(this.nSlcTab);
		if (flag)
		{
			ExchangeData exchangeData = fGameManager.mloadConfig.AllExchangeData[this.nSlcTab];
			ExchangeInfo oneInfo = exchangeData.GetOneInfo(nIdx);
			bool flag2 = oneInfo != null;
			if (flag2)
			{
				TipManager.GetInstance().ShowTextTip(oneInfo.sDesc, TextAnchor.MiddleLeft, 1f, 20f, -20f);
			}
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0005E014 File Offset: 0x0005C214
	private void SetChipPanel()
	{
		bool flag = fGameManager.mloadConfig.AllExchangeData.ContainsKey(this.nSlcTab);
		if (flag)
		{
			ExchangeData exchangeData = fGameManager.mloadConfig.AllExchangeData[this.nSlcTab];
			bool flag2 = fGameManager.mloadConfig.AllItemData.ContainsKey(exchangeData.chipId);
			if (flag2)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[exchangeData.chipId];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), this.chipIcon);
				this.chipNum.text = ShopManager.GetInstance().GetHaveItemNum(exchangeData.chipId).ToString();
				this.chipPanel.SetActive(true);
				return;
			}
		}
		this.chipPanel.SetActive(false);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0005E0EE File Offset: 0x0005C2EE
	private void ExitChipPanel(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0005E0FC File Offset: 0x0005C2FC
	private void OverChipPanel(GameObject go)
	{
		bool flag = fGameManager.mloadConfig.AllExchangeData.ContainsKey(this.nSlcTab);
		if (flag)
		{
			ExchangeData exchangeData = fGameManager.mloadConfig.AllExchangeData[this.nSlcTab];
			bool flag2 = exchangeData.chipId != 0;
			if (flag2)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[exchangeData.chipId];
				TipManager.GetInstance().ShowTextTip(itemData.m_sTipDesc, TextAnchor.MiddleLeft, 1f, 20f, -20f);
			}
		}
	}

	// Token: 0x0400086A RID: 2154
	public static ExchangeUICon inst;

	// Token: 0x0400086B RID: 2155
	private Toggle tabToggle;

	// Token: 0x0400086C RID: 2156
	private List<Toggle> szTabBtn = new List<Toggle>();

	// Token: 0x0400086D RID: 2157
	private List<GameObject> szGridList = new List<GameObject>();

	// Token: 0x0400086E RID: 2158
	private Button preBtn;

	// Token: 0x0400086F RID: 2159
	private Button nextBtn;

	// Token: 0x04000870 RID: 2160
	private Text pageNumTxt;

	// Token: 0x04000871 RID: 2161
	private Dropdown heroDropdown;

	// Token: 0x04000872 RID: 2162
	private GameObject chipPanel;

	// Token: 0x04000873 RID: 2163
	private RawImage chipIcon;

	// Token: 0x04000874 RID: 2164
	private Text chipNum;

	// Token: 0x04000875 RID: 2165
	private List<int> szHeroList = new List<int>();

	// Token: 0x04000876 RID: 2166
	private int nSlcTab = -1;

	// Token: 0x04000877 RID: 2167
	private int nCurPage;

	// Token: 0x04000878 RID: 2168
	private int nMaxPage;

	// Token: 0x04000879 RID: 2169
	private int nCommon = -1;
}

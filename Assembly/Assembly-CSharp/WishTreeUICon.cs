using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004AF RID: 1199
public class WishTreeUICon : Window
{
	// Token: 0x06002E0D RID: 11789 RVA: 0x00197968 File Offset: 0x00195B68
	protected override void Awake()
	{
		base.Awake();
		this.timeTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.gridRender = this.UIGameObj.Get_GameObj(1);
		this.gridRender.SetActive(false);
		this.wishGoldTxt = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.descTxt = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		WishTreeUICon.inst = this;
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x001979E8 File Offset: 0x00195BE8
	private void Start()
	{
		bool flag = BackgroundUICon.inst != null;
		if (flag)
		{
			bool flag2 = WebParam.client_language == "zh";
			if (flag2)
			{
				BackgroundUICon.inst.ShowIconImg(Window.spriteName(AtlasType.ATLAS_WISHTREE, "wishtree_di"));
			}
			bool flag3 = WebParam.client_language == "baxi";
			if (flag3)
			{
				BackgroundUICon.inst.ShowIconImg(Window.spriteName(AtlasType.ATLAS_WISHTREE, "wishtree_di_baxi"));
			}
		}
		bool flag4 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag4)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.ShowUIData();
		this.SetWishGoldNum();
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x00197AA4 File Offset: 0x00195CA4
	public override void Reset()
	{
		base.Reset();
		bool flag = BackgroundUICon.inst != null;
		if (flag)
		{
			bool flag2 = WebParam.client_language == "zh";
			if (flag2)
			{
				BackgroundUICon.inst.ShowIconImg(Window.spriteName(AtlasType.ATLAS_WISHTREE, "wishtree_di"));
			}
			bool flag3 = WebParam.client_language == "baxi";
			if (flag3)
			{
				BackgroundUICon.inst.ShowIconImg(Window.spriteName(AtlasType.ATLAS_WISHTREE, "wishtree_di_baxi"));
			}
		}
		bool flag4 = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag4)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.SetWishGoldNum();
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x00197B60 File Offset: 0x00195D60
	public override void Close()
	{
		base.Close();
		bool flag = BackgroundUICon.inst != null;
		if (flag)
		{
			BackgroundUICon.inst.ShowIconImg(null);
		}
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x00197B92 File Offset: 0x00195D92
	public void SetWishGoldNum()
	{
		this.wishGoldTxt.text = PlayerDataManager.getInstance().playerdet.m_nWishGold.ToString();
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x00197BB8 File Offset: 0x00195DB8
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(43);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			WishTreeData wishTreeData = fGameManager.mloadConfig.AllWishTreeData[actDataByType.nXmlId];
			this.timeTxt.text = Language.GetLang(28013, new object[]
			{
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				dateTime2.Year,
				dateTime2.Month,
				dateTime2.Day
			});
			this.descTxt.text = Language.GetLang(34004, new object[]
			{
				wishTreeData.nZuanshi,
				wishTreeData.nWishgold
			});
			foreach (WishTreeReward wishTreeReward in wishTreeData.allRewardData.Values)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gridRender);
				gameObject.transform.SetParent(this.gridRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = "WishGrid" + wishTreeReward.nId.ToString();
				gameObject.SetActive(true);
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(wishTreeReward.szBuyItem[0])];
				Text component = gameObject.transform.Find("NameTxt").GetComponent<Text>();
				bool flag2 = int.Parse(wishTreeReward.szBuyItem[1]) > 1;
				if (flag2)
				{
					component.text = string.Format(itemData.m_sLiBaoDesc, wishTreeReward.szBuyItem[1]);
				}
				else
				{
					component.text = itemData.m_sName;
				}
				Image component2 = gameObject.transform.Find("QualImg").GetComponent<Image>();
				EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWishGrid);
				EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWishGrid);
				component2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, Fun.GetQualBgStr(itemData.m_nQuality));
				RawImage component3 = gameObject.transform.Find("QualImg/RawImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component3);
				component = gameObject.transform.Find("CostTxt").GetComponent<Text>();
				component.text = Language.GetLang(34001, new object[]
				{
					wishTreeReward.nCost
				});
				GameObject gameObject2 = gameObject.transform.Find("Button").gameObject;
				EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnWishBuy);
				EventTriggerListener.Get(gameObject2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			}
		}
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x00197F6C File Offset: 0x0019616C
	private void OnWishBuy(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(43);
		bool flag = actDataByType == null;
		if (!flag)
		{
			WishTreeData wishTreeData = fGameManager.mloadConfig.AllWishTreeData[actDataByType.nXmlId];
			int num = int.Parse(go.transform.parent.name.Replace("WishGrid", string.Empty));
			bool flag2 = wishTreeData.allRewardData.ContainsKey(num);
			if (flag2)
			{
				WishTreeReward wishTreeReward = wishTreeData.allRewardData[num];
				bool flag3 = wishTreeReward.nCost > PlayerDataManager.getInstance().playerdet.GetMoneyByType(8);
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(34006, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					ShopManager.GetInstance().OpenBuyItemWin("wishtree", int.Parse(wishTreeReward.szBuyItem[0]), 8, 0, wishTreeReward.nCost, int.Parse(wishTreeReward.szBuyItem[1]), 100, string.Empty, new ShopManager.BuyItemDelegate(this.OnWishBuyYes), num);
				}
			}
		}
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x001980B4 File Offset: 0x001962B4
	private void OnWishBuyYes(object szParam, int type, int num)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(43);
		bool flag = actDataByType == null;
		if (!flag)
		{
			WishTreeData wishTreeData = fGameManager.mloadConfig.AllWishTreeData[actDataByType.nXmlId];
			int num2 = (int)szParam;
			bool flag2 = wishTreeData.allRewardData.ContainsKey(num2);
			if (flag2)
			{
				WishTreeReward wishTreeReward = wishTreeData.allRewardData[num2];
				int num3 = wishTreeReward.nCost * num;
				bool flag3 = num3 > PlayerDataManager.getInstance().playerdet.GetMoneyByType(8);
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(34006, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					fGameManager.SendMsgToHallServer(1315, new ArrayList
					{
						15039,
						new ArrayList
						{
							num2,
							num
						}
					});
				}
			}
		}
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x001981D7 File Offset: 0x001963D7
	private void ExitWishGrid(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x001981E8 File Offset: 0x001963E8
	private void OverWishGrid(GameObject go)
	{
		string text = string.Empty;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(43);
		bool flag = actDataByType != null;
		if (flag)
		{
			WishTreeData wishTreeData = fGameManager.mloadConfig.AllWishTreeData[actDataByType.nXmlId];
			int key = int.Parse(go.transform.parent.name.Replace("WishGrid", string.Empty));
			bool flag2 = wishTreeData.allRewardData.ContainsKey(key);
			if (flag2)
			{
				WishTreeReward wishTreeReward = wishTreeData.allRewardData[key];
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(wishTreeReward.szBuyItem[0])];
				text = ((int.Parse(wishTreeReward.szBuyItem[1]) <= 1) ? string.Concat(new string[]
				{
					"<size=20><color=",
					Fun.GetQualColor(itemData.m_nQuality),
					">",
					itemData.m_sName,
					"</color></size>"
				}) : string.Concat(new string[]
				{
					"<size=20><color=",
					Fun.GetQualColor(itemData.m_nQuality),
					">",
					string.Format(itemData.m_sLiBaoDesc, wishTreeReward.szBuyItem[1]),
					"</color></size>"
				}));
				text = text + "\n" + itemData.m_sdesc;
				text = text + "\n" + Language.GetLang(34001, new object[]
				{
					wishTreeReward.nCost
				});
				TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
			}
		}
	}

	// Token: 0x0400277C RID: 10108
	public static WishTreeUICon inst;

	// Token: 0x0400277D RID: 10109
	private Text timeTxt;

	// Token: 0x0400277E RID: 10110
	private GameObject gridRender;

	// Token: 0x0400277F RID: 10111
	private List<GameObject> szGrid = new List<GameObject>();

	// Token: 0x04002780 RID: 10112
	private Text wishGoldTxt;

	// Token: 0x04002781 RID: 10113
	private Text descTxt;
}

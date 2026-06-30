using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030C RID: 780
public class ShopItemCon : Window
{
	// Token: 0x060025C9 RID: 9673 RVA: 0x00156E80 File Offset: 0x00155080
	protected override void Awake()
	{
		base.Awake();
		this.ItemNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.pBuyBtn_1 = this.UIGameObj.Get_GameObj(1);
		this.pBuyBtn_2 = this.UIGameObj.Get_GameObj(2);
		this.TimeTitle = this.UIGameObj.Get_GameObj(3);
		this.TimeTxt = this.TimeTitle.transform.FindChild("Text").GetComponent<Text>();
		this.pIsHot = this.UIGameObj.Get_GameObj(4);
		this.pIsNew = this.UIGameObj.Get_GameObj(5);
		this.ItemIco = this.UIGameObj.Get_GameObj(6).GetComponent<RawImage>();
		this.pLiBaodetailed = this.UIGameObj.Get_GameObj(7);
		this.pDaojudetailed = this.UIGameObj.Get_GameObj(8);
		this.pYuanJiaPanel = this.UIGameObj.Get_GameObj(9);
		this.pYuanJiaMoney_l = this.UIGameObj.Get_GameObj(10);
		this.pYuanJiaMoney_2 = this.UIGameObj.Get_GameObj(11);
		this.pXianJiaMoney_l = this.UIGameObj.Get_GameObj(13);
		this.pXianJiaMoney_2 = this.UIGameObj.Get_GameObj(14);
		this.JiLvHuoDeiList = this.UIGameObj.Get_GameObj(15);
		this.HuoDeIco = this.UIGameObj.Get_GameObj(16);
		this.ItemDaoJuDesc = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.HuoDeIco.SetActive(false);
		this.pNumList = this.UIGameObj.Get_GameObj(18);
		this.pNumImg = this.UIGameObj.Get_GameObj(19);
		this.pNumImg.SetActive(false);
		EventTriggerListener.Get(this.pBuyBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuy);
		EventTriggerListener.Get(this.pBuyBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuy);
		EventTriggerListener.Get(this.pBuyBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pBuyBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.LiaoItemIco = this.UIGameObj.Get_GameObj(20).GetComponent<RawImage>();
		this.LiaoItemIco.gameObject.SetActive(false);
		this.ItemIco.gameObject.SetActive(false);
		EventTriggerListener.Get(this.LiaoItemIco.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.LiaoItemIco.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x00157134 File Offset: 0x00155334
	private void Start()
	{
		this.ItemNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x00157150 File Offset: 0x00155350
	public void InitData(ShopData data)
	{
		this.pShopData = data;
		bool flag = !fGameManager.mloadConfig.AllItemData.ContainsKey(this.pShopData.m_nItemId);
		if (!flag)
		{
			this.pItemData = fGameManager.mloadConfig.AllItemData[this.pShopData.m_nItemId];
			this.ItemNameText.text = string.Concat(new string[]
			{
				"<color=",
				Fun.GetQualColor(this.pItemData.m_nQuality),
				">",
				this.pItemData.m_sName,
				"</color>"
			});
			string icoPath = ShopManager.GetIcoPath(this.pItemData.m_nType);
			bool flag2 = this.pShopData.m_nTitle == 5 || this.pShopData.m_nTitle == 6;
			if (flag2)
			{
				this.LiaoItemIco.gameObject.SetActive(true);
				this.ItemIco.gameObject.SetActive(false);
				fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, this.pItemData.m_sIco), this.LiaoItemIco);
				ShopUICon.inst.OnAddRawImage(this.LiaoItemIco);
			}
			else
			{
				this.LiaoItemIco.gameObject.SetActive(false);
				this.ItemIco.gameObject.SetActive(true);
				fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, this.pItemData.m_sIco), this.ItemIco);
				ShopUICon.inst.OnAddRawImage(this.ItemIco);
			}
			this.pLiBaodetailed.SetActive(false);
			this.pDaojudetailed.SetActive(true);
			this.ItemDaoJuDesc.text = this.pItemData.m_sdesc;
			this.pIsHot.SetActive(this.pShopData.m_nIsHot == 1);
			this.pIsNew.SetActive(this.pShopData.m_nIsNew == 1);
			bool flag3 = this.pShopData.m_nIsLimit == 1;
			if (flag3)
			{
				this.TimeTitle.SetActive(true);
				int num = ShopItemPanelCon.isItemLimit(this.pShopData);
				bool flag4 = num <= 0;
				if (flag4)
				{
					this.TimeTxt.text = Language.GetLang(39508, new object[0]);
				}
				else
				{
					this.TimeTxt.text = Language.GetLang(39509, new object[]
					{
						Fun.SecToFullTimeShop(num)
					});
				}
			}
			else
			{
				bool flag5 = this.pShopData.m_nZheKou == 0;
				if (flag5)
				{
					this.TimeTitle.SetActive(false);
				}
				else
				{
					this.TimeTitle.SetActive(true);
					this.TimeTxt.text = ((float)this.pShopData.m_nZheKou / 10f).ToString() + Language.GetLang(39510, new object[0]);
				}
			}
			this.OnShowPrice();
		}
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x0015744C File Offset: 0x0015564C
	private void OnShowNewPrice()
	{
		string text = this.pItemData.m_nGlodPrice.ToString();
		foreach (char c in text)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pNumImg);
			gameObject.transform.SetParent(this.pNumList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, c.ToString());
		}
		text = this.pItemData.m_nDiamondPrice.ToString();
		foreach (char c2 in text)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.pNumImg);
			gameObject2.transform.SetParent(this.pNumList.transform);
			gameObject2.SetActive(true);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject2.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, c2.ToString());
		}
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x001575D4 File Offset: 0x001557D4
	private void OnShowPrice()
	{
		bool flag = this.pShopData.m_nIsZengSong == 1;
		if (flag)
		{
			this.pBuyBtn_2.SetActive(true);
		}
		else
		{
			this.pBuyBtn_2.SetActive(false);
		}
		int nZheKou = this.pShopData.m_nZheKou;
		this.nGlodPrice = this.pItemData.m_nGlodPrice;
		this.nZuanShiPrice = this.pItemData.m_nDiamondPrice;
		bool flag2 = nZheKou == 0;
		if (flag2)
		{
			this.pYuanJiaPanel.SetActive(false);
			bool flag3 = this.nGlodPrice != 0 && this.nZuanShiPrice != 0;
			if (flag3)
			{
				this.pXianJiaMoney_2.SetActive(true);
				this.onSetMoneyObj(this.pXianJiaMoney_l, "zuanshi", this.nZuanShiPrice);
				this.onSetMoneyObj(this.pXianJiaMoney_2, "jinbi", this.nGlodPrice);
			}
			else
			{
				this.pXianJiaMoney_2.SetActive(false);
				bool flag4 = this.nGlodPrice != 0;
				if (flag4)
				{
					this.onSetMoneyObj(this.pXianJiaMoney_l, "jinbi", this.nGlodPrice);
				}
				else
				{
					bool flag5 = this.nZuanShiPrice != 0;
					if (flag5)
					{
						this.onSetMoneyObj(this.pXianJiaMoney_l, "zuanshi", this.nZuanShiPrice);
					}
				}
			}
		}
		else
		{
			int nPrice = Mathf.CeilToInt((float)(this.nGlodPrice * nZheKou) / 100f);
			int nPrice2 = Mathf.CeilToInt((float)(this.nZuanShiPrice * nZheKou) / 100f);
			this.pYuanJiaPanel.SetActive(true);
			bool flag6 = this.nGlodPrice != 0 && this.nZuanShiPrice != 0;
			if (flag6)
			{
				this.pXianJiaMoney_2.SetActive(true);
				this.pYuanJiaMoney_2.SetActive(true);
				this.onSetMoneyObj(this.pYuanJiaMoney_l, "jinbi", this.nGlodPrice);
				this.onSetMoneyObj(this.pYuanJiaMoney_2, "zuanshi", this.nZuanShiPrice);
				this.onSetMoneyObj(this.pXianJiaMoney_l, "jinbi", nPrice);
				this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", nPrice2);
			}
			else
			{
				this.pXianJiaMoney_2.SetActive(false);
				this.pYuanJiaMoney_2.SetActive(false);
				bool flag7 = this.nGlodPrice != 0;
				if (flag7)
				{
					this.onSetMoneyObj(this.pYuanJiaMoney_l, "jinbi", this.nGlodPrice);
					this.onSetMoneyObj(this.pXianJiaMoney_l, "jinbi", nPrice);
				}
				else
				{
					bool flag8 = this.nZuanShiPrice != 0;
					if (flag8)
					{
						this.onSetMoneyObj(this.pYuanJiaMoney_l, "zuanshi", this.nZuanShiPrice);
						this.onSetMoneyObj(this.pXianJiaMoney_l, "zuanshi", nPrice2);
					}
				}
			}
		}
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x00157884 File Offset: 0x00155A84
	private void onSetMoneyObj(GameObject obj, string sType, int nPrice)
	{
		obj.transform.FindChild("NumText").GetComponent<Text>().text = nPrice.ToString();
		Image component = obj.transform.FindChild("Image").GetComponent<Image>();
		component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_" + sType);
		bool flag = sType == "jinbi";
		if (flag)
		{
			component.rectTransform.sizeDelta = new Vector2(30f, 30f);
		}
		else
		{
			component.rectTransform.sizeDelta = new Vector2(30f, 24f);
		}
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x00157934 File Offset: 0x00155B34
	public void OnClickBuy(GameObject obj)
	{
		string text = obj.name;
		int num = text.LastIndexOf("_");
		text = text.Substring(num + 1, 1);
		num = int.Parse(text);
		bool flag = num == 1 && ShopManager.onGetPrice(this.pItemData, 3) <= 0 && this.pItemData.m_nType == 20;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39531, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			ShopManager.GetInstance().OpenBuyItemWin(num, this.pShopData);
		}
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x001579F4 File Offset: 0x00155BF4
	public void OnCreatHuoDeListBtn(ArrayList szList)
	{
		fUIManager.OnClearObj(this.ObjHuoDeiBtnList);
		int count = szList.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.HuoDeIco);
			gameObject.transform.SetParent(this.JiLvHuoDeiList.transform);
			gameObject.name = szList[i].ToString();
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ObjHuoDeiBtnList.Add(gameObject);
			EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnHuoDeiEnterObj);
			EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnHuoDeiExitObj);
			ItemData itemData = fGameManager.mloadConfig.AllItemData[(int)szList[i]];
			string icoPath = ShopManager.GetIcoPath(itemData.m_nType);
			fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, itemData.m_sIco), gameObject.transform.FindChild("huodeIco").GetComponent<RawImage>());
		}
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x00157B34 File Offset: 0x00155D34
	private void OnHuoDeiEnterObj(GameObject obj)
	{
		int key = int.Parse(obj.name);
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		TipManager.GetInstance().ShowTextTip(itemData.m_sName, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x00157B80 File Offset: 0x00155D80
	private void OnHuoDeiExitObj(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x00157B90 File Offset: 0x00155D90
	private void OnEnterObj(GameObject obj)
	{
		bool flag = ShopUICon.inst != null;
		if (flag)
		{
			ShopUICon.inst.OnShowItemTip(this.pItemData);
		}
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x00157BC0 File Offset: 0x00155DC0
	private void OnExitObj(GameObject obj)
	{
		bool flag = ShopUICon.inst != null;
		if (flag)
		{
			ShopUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x04002183 RID: 8579
	private ShopData pShopData;

	// Token: 0x04002184 RID: 8580
	private ItemData pItemData;

	// Token: 0x04002185 RID: 8581
	private Text ItemNameText;

	// Token: 0x04002186 RID: 8582
	private RawImage ItemIco;

	// Token: 0x04002187 RID: 8583
	private RawImage LiaoItemIco;

	// Token: 0x04002188 RID: 8584
	private GameObject pBuyBtn_1;

	// Token: 0x04002189 RID: 8585
	private GameObject pBuyBtn_2;

	// Token: 0x0400218A RID: 8586
	private GameObject TimeTitle;

	// Token: 0x0400218B RID: 8587
	private Text TimeTxt;

	// Token: 0x0400218C RID: 8588
	private GameObject pIsHot;

	// Token: 0x0400218D RID: 8589
	private GameObject pIsNew;

	// Token: 0x0400218E RID: 8590
	private GameObject pLiBaodetailed;

	// Token: 0x0400218F RID: 8591
	private GameObject pDaojudetailed;

	// Token: 0x04002190 RID: 8592
	private GameObject pYuanJiaPanel;

	// Token: 0x04002191 RID: 8593
	private GameObject pYuanJiaMoney_l;

	// Token: 0x04002192 RID: 8594
	private GameObject pYuanJiaMoney_2;

	// Token: 0x04002193 RID: 8595
	private GameObject pXianJiaMoney_l;

	// Token: 0x04002194 RID: 8596
	private GameObject pXianJiaMoney_2;

	// Token: 0x04002195 RID: 8597
	private GameObject JiLvHuoDeiList;

	// Token: 0x04002196 RID: 8598
	private GameObject HuoDeIco;

	// Token: 0x04002197 RID: 8599
	private Text ItemDaoJuDesc;

	// Token: 0x04002198 RID: 8600
	private GameObject pNumList;

	// Token: 0x04002199 RID: 8601
	private GameObject pNumImg;

	// Token: 0x0400219A RID: 8602
	private List<GameObject> ObjHuoDeiBtnList = new List<GameObject>();

	// Token: 0x0400219B RID: 8603
	private int nGlodPrice;

	// Token: 0x0400219C RID: 8604
	private int nZuanShiPrice;
}

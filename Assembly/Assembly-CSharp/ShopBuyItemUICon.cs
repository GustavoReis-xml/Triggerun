using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000309 RID: 777
public class ShopBuyItemUICon : Window
{
	// Token: 0x060025A6 RID: 9638 RVA: 0x00154428 File Offset: 0x00152628
	protected override void Awake()
	{
		base.Awake();
		ShopBuyItemUICon.inst = this;
		this.ItemNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.pBuyBtn_0 = this.UIGameObj.Get_GameObj(1);
		this.pBuyBtn_1 = this.UIGameObj.Get_GameObj(20);
		this.pBuyBtn_2 = this.UIGameObj.Get_GameObj(2);
		this.pYuanJiaMoney_l = this.UIGameObj.Get_GameObj(3);
		this.pXianJiaMoney_l = this.UIGameObj.Get_GameObj(4);
		this.pXianJiaMoney_2 = this.UIGameObj.Get_GameObj(19);
		this.ItemIco = this.UIGameObj.Get_GameObj(5).GetComponent<RawImage>();
		this.pInputField = this.UIGameObj.Get_GameObj(6).GetComponent<InputField>();
		this.pInputField.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChang));
		this.pMaxBtn = this.UIGameObj.Get_GameObj(7);
		this.pZuoBtn = this.UIGameObj.Get_GameObj(8);
		this.pYouBtn = this.UIGameObj.Get_GameObj(9);
		this.pBuyBtn_Text_1 = this.UIGameObj.Get_GameObj(22).GetComponent<Text>();
		this.pBuyBtn_Text_2 = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		EventTriggerListener.Get(this.pMaxBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickMax);
		EventTriggerListener.Get(this.pMaxBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.ChangeCountClick);
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.ChangeCountClick);
		EventTriggerListener.Get(this.pBuyBtn_0.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseClick);
		EventTriggerListener.Get(this.pBuyBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyClick);
		EventTriggerListener.Get(this.pBuyBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyClick);
		EventTriggerListener.Get(this.pBuyBtn_0).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pBuyBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pBuyBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.comtipBg = this.UIGameObj.Get_GameObj(10).GetComponent<RawImage>();
		this.pBuyPanel_1 = this.UIGameObj.Get_GameObj(11);
		this.pBuyPanel_2 = this.UIGameObj.Get_GameObj(12);
		this.OneHero = this.UIGameObj.Get_GameObj(13);
		this.OneHero.SetActive(false);
		this.pPricePanel = this.UIGameObj.Get_GameObj(18);
		this.discountSp = this.UIGameObj.Get_GameObj(23).GetComponent<Image>();
		this.zhekouItemObj = this.UIGameObj.Get_GameObj(24);
		this.zhekouRimg = this.zhekouItemObj.GetComponent<RawImage>();
		EventTriggerListener.Get(this.zhekouItemObj).onEnter = new EventTriggerListener.VoidDelegate(this.OverZhekouObj);
		EventTriggerListener.Get(this.zhekouItemObj).onExit = new EventTriggerListener.VoidDelegate(this.ExitZhekouObj);
		this.zhekouItemTxt = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.zhekouItemObj.SetActive(false);
		this.zhekouTg = this.UIGameObj.Get_GameObj(26).GetComponent<Toggle>();
		this.zhekouTg.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleCheck));
		this.zhekouTgTxt = this.zhekouTg.transform.Find("Label").GetComponent<Text>();
		this.zhekouTg.gameObject.SetActive(false);
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x00154838 File Offset: 0x00152A38
	private void Start()
	{
		GameRoot.inst.Setting.Load_Setting_Data_State = false;
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x0015484C File Offset: 0x00152A4C
	public override void Reset()
	{
		base.Reset();
		this.Start();
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x00154860 File Offset: 0x00152A60
	private void OnValueChang(string stext)
	{
		bool flag = this.pItemData == null;
		if (!flag)
		{
			bool flag2 = this.pInputField.text == string.Empty;
			if (flag2)
			{
				this.pInputField.text = "1";
			}
			bool flag3 = this.pInputField.text == "-";
			if (flag3)
			{
				this.pInputField.text = "1";
			}
			this.CurCount = int.Parse(this.pInputField.text);
			bool isOn = this.zhekouTg.isOn;
			if (isOn)
			{
				bool flag4 = this.CurCount > this.nZhekouMaxNum;
				if (flag4)
				{
					this.pInputField.text = this.nZhekouMaxNum.ToString();
				}
			}
			else
			{
				bool flag5 = this.CurCount > this.m_nMaxNum;
				if (flag5)
				{
					this.pInputField.text = this.m_nMaxNum.ToString();
				}
			}
			bool flag6 = this.CurCount < 1;
			if (flag6)
			{
				this.pInputField.text = "1";
			}
			this.CurCount = int.Parse(this.pInputField.text);
			bool flag7 = this.isZheKou == 0;
			if (flag7)
			{
				this.nAllPrice = this.nPrice * this.CurCount;
				this.nXianAllPrice = this.nNewPrice * this.CurCount;
				this.nXianZuanShiAllPrice = this.nZuanShiNewPrice * this.CurCount;
			}
			bool flag8 = this.pShopData != null && LiBaoManager.GetInstance().CheckDiscountShop(this.pShopData.key);
			if (flag8)
			{
				this.nXianZuanShiAllPrice = LiBaoManager.GetInstance().GetDiscountShopPrice(this.pShopData, this.CurCount, this.nXianZuanShiAllPrice);
			}
			else
			{
				bool flag9 = this.pShopData != null && this.pShopData.m_nTitle != 0;
				if (flag9)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
					bool flag10 = !this.zhekouItemObj.activeSelf;
					if (flag10)
					{
						this.zhekouItemObj.SetActive(true);
						fUIManager.GetInstance().GetIconAsset("ItemIco/" + itemData.m_sIco + ".txt", this.zhekouRimg);
					}
					this.zhekouTg.gameObject.SetActive(true);
					int itemNumById = ShopManager.GetInstance().GetItemNumById(1027);
					bool flag11 = itemNumById < 1;
					if (flag11)
					{
						this.zhekouTg.interactable = false;
					}
					else
					{
						this.zhekouTg.interactable = true;
					}
					bool isOn2 = this.zhekouTg.isOn;
					if (isOn2)
					{
						int num = Mathf.Min(itemNumById, this.CurCount);
						this.nXianZuanShiAllPrice = Mathf.CeilToInt((float)(this.nZuanShiNewPrice * itemData.nZhekou) / 100f) * num + this.nZuanShiNewPrice * (this.CurCount - num);
						this.zhekouItemTxt.text = (itemNumById - num).ToString();
						this.zhekouTgTxt.text = Language.GetLang(39543, new object[]
						{
							itemNumById - num
						});
					}
					else
					{
						this.zhekouItemTxt.text = itemNumById.ToString();
						this.zhekouTgTxt.text = Language.GetLang(39543, new object[]
						{
							itemNumById
						});
					}
				}
				else
				{
					this.zhekouTg.gameObject.SetActive(false);
					this.zhekouItemObj.SetActive(false);
				}
			}
			this.pXianJiaMoney_l.transform.FindChild("NumText").GetComponent<Text>().text = this.nXianAllPrice.ToString();
			this.pXianJiaMoney_2.transform.FindChild("NumText").GetComponent<Text>().text = this.nXianZuanShiAllPrice.ToString();
			bool flag12 = this.nXianAllPrice > PlayerDataManager.getInstance().playerdet.gameCurrency;
			if (flag12)
			{
				this.pBuyBtn_1.GetComponent<Button>().interactable = false;
			}
			else
			{
				this.pBuyBtn_1.GetComponent<Button>().interactable = true;
			}
			bool flag13 = this.groupNum > 1;
			if (flag13)
			{
				this.ItemNameText.text = string.Concat(new string[]
				{
					"<color=",
					Fun.GetQualColor(this.pItemData.m_nQuality),
					">",
					string.Format(this.pItemData.m_sLiBaoDesc, this.groupNum * this.CurCount),
					"</color>"
				});
			}
			else
			{
				this.ItemNameText.text = string.Concat(new string[]
				{
					"<color=",
					Fun.GetQualColor(this.pItemData.m_nQuality),
					">",
					this.pItemData.m_sName,
					"</color>"
				});
			}
		}
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x00154D64 File Offset: 0x00152F64
	public void SetData(int nType, ShopData Data)
	{
		this.comTipState = false;
		fUIManager.tipBlur.rawImage = this.comtipBg;
		fUIManager.tipBlur.enabled = true;
		this.pShopData = Data;
		this.m_nCmdType = nType;
		bool flag = !fGameManager.mloadConfig.AllItemData.ContainsKey(this.pShopData.m_nItemId);
		if (!flag)
		{
			this.CurCount = 1;
			this.pInputField.text = this.CurCount.ToString();
			this.zhekouTg.isOn = false;
			this.pItemData = fGameManager.mloadConfig.AllItemData[this.pShopData.m_nItemId];
			string format = string.Empty;
			format = "ItemIco/{0}.txt";
			bool flag2 = this.pItemData.m_nType != 20;
			if (flag2)
			{
				this.pBuyPanel_1.SetActive(true);
				this.pBuyPanel_2.SetActive(false);
				this.pPricePanel.transform.localPosition = new Vector3(0f, 58f, 0f);
				fUIManager.GetInstance().GetIconAsset(string.Format(format, this.pItemData.m_sIco), this.ItemIco);
				bool flag3 = this.pShopData.m_nTitle == 3 || this.pShopData.m_nTitle == 4;
				if (flag3)
				{
					this.ItemIco.rectTransform.sizeDelta = new Vector2(244f, 171f);
				}
				else
				{
					this.ItemIco.rectTransform.sizeDelta = new Vector2(164f, 164f);
				}
			}
			else
			{
				this.pPricePanel.transform.localPosition = new Vector3(0f, 58f, 0f);
				fUIManager.OnClearObj(this.ObjItemList);
				this.pBuyPanel_1.SetActive(false);
				this.pBuyPanel_2.SetActive(true);
				this.OnShowHero(this.pItemData);
			}
			bool flag4 = this.m_nCmdType == 1;
			if (flag4)
			{
				this.pBuyBtn_Text_1.text = Language.GetLang(46060, new object[0]);
				this.pBuyBtn_Text_2.text = Language.GetLang(46059, new object[0]);
			}
			else
			{
				this.pBuyBtn_Text_1.text = Language.GetLang(7046, new object[0]);
				this.pBuyBtn_Text_2.text = Language.GetLang(7045, new object[0]);
			}
			this.ItemNameText.text = string.Concat(new string[]
			{
				"<color=",
				Fun.GetQualColor(this.pItemData.m_nQuality),
				">",
				this.pItemData.m_sName,
				"</color>"
			});
			this.OnShowPrice();
		}
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x00155040 File Offset: 0x00153240
	public void SetData(ArrayList szParam)
	{
		this.sTipReason = szParam[0].ToString();
		int key = (int)szParam[1];
		this.nMoneyType = (int)szParam[2];
		int oldnum = (int)szParam[3];
		int newnum = (int)szParam[4];
		this.groupNum = (int)szParam[5];
		int maxnum = (int)szParam[6];
		string text = szParam[7].ToString();
		this.buyItemFunc = (ShopManager.BuyItemDelegate)szParam[8];
		this.buyItemParam = szParam[9];
		this.comTipState = false;
		fUIManager.tipBlur.rawImage = this.comtipBg;
		fUIManager.tipBlur.enabled = true;
		this.m_nCmdType = 1;
		this.CurCount = 1;
		this.pInputField.text = this.CurCount.ToString();
		this.pShopData = null;
		this.zhekouTg.gameObject.SetActive(false);
		this.zhekouItemObj.SetActive(false);
		this.pItemData = fGameManager.mloadConfig.AllItemData[key];
		string format = string.Empty;
		format = "ItemIco/{0}.txt";
		this.pBuyPanel_1.SetActive(true);
		this.pBuyPanel_2.SetActive(false);
		this.pPricePanel.transform.localPosition = new Vector3(0f, 58f, 0f);
		bool flag = string.IsNullOrEmpty(text) || text == "0";
		if (flag)
		{
			fUIManager.GetInstance().GetIconAsset(string.Format(format, this.pItemData.m_sIco), this.ItemIco);
		}
		else
		{
			fUIManager.GetInstance().GetIconAsset(string.Format(format, text), this.ItemIco);
		}
		this.ItemIco.rectTransform.sizeDelta = new Vector2(164f, 164f);
		bool flag2 = this.m_nCmdType == 1;
		if (flag2)
		{
			this.pBuyBtn_Text_1.text = Language.GetLang(46060, new object[0]);
			bool flag3 = this.nMoneyType == 2;
			if (flag3)
			{
				this.pBuyBtn_Text_2.text = Language.GetLang(46059, new object[0]);
			}
			bool flag4 = this.nMoneyType == 8;
			if (flag4)
			{
				this.pBuyBtn_Text_2.text = Language.GetLang(34002, new object[0]);
			}
		}
		else
		{
			this.pBuyBtn_Text_1.text = Language.GetLang(7046, new object[0]);
			bool flag5 = this.nMoneyType == 2;
			if (flag5)
			{
				this.pBuyBtn_Text_2.text = Language.GetLang(7045, new object[0]);
			}
			bool flag6 = this.nMoneyType == 8;
			if (flag6)
			{
				this.pBuyBtn_Text_2.text = Language.GetLang(34002, new object[0]);
			}
		}
		bool flag7 = this.groupNum > 1;
		if (flag7)
		{
			this.ItemNameText.text = string.Concat(new string[]
			{
				"<color=",
				Fun.GetQualColor(this.pItemData.m_nQuality),
				">",
				string.Format(this.pItemData.m_sLiBaoDesc, this.groupNum),
				"</color>"
			});
		}
		else
		{
			this.ItemNameText.text = string.Concat(new string[]
			{
				"<color=",
				Fun.GetQualColor(this.pItemData.m_nQuality),
				">",
				this.pItemData.m_sName,
				"</color>"
			});
		}
		this.OnShowPrice(oldnum, newnum, maxnum);
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x00155408 File Offset: 0x00153608
	private void OnShowHero(ItemData pItemData)
	{
		string text = string.Empty;
		int count = pItemData.m_szChildItem.Count;
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList = (ArrayList)pItemData.m_szChildItem[i];
			int key = (int)arrayList[0];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			int nHeroId = itemData.m_nHeroId;
			text = string.Empty;
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[nHeroId];
			bool flag = ShopManager.OnGetHeroHaveEquipOpen(itemData) == 1 && this.m_nCmdType == 1;
			if (flag)
			{
				text = "(已拥有)";
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneHero);
			gameObject.transform.SetParent(this.pBuyPanel_2.transform);
			gameObject.SetActive(true);
			int num2 = num * 150 - (count - 1) * 75;
			gameObject.transform.localPosition = new Vector3((float)num2, 162f, 0f);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.FindChild("havestate").GetComponent<Text>().text = text;
			gameObject.transform.FindChild("hName").GetComponent<Text>().text = itemData.m_sName;
			RawImage component = gameObject.transform.FindChild("Ico").GetComponent<RawImage>();
			fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco, 1), component);
			this.ObjItemList.Add(gameObject);
			num++;
		}
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x001555D4 File Offset: 0x001537D4
	private void OnShowPrice(int oldnum, int newnum, int maxnum)
	{
		this.isZheKou = 0;
		bool flag = this.nMoneyType == 2 || this.nMoneyType == 8;
		if (flag)
		{
			this.nZuanShiPrice = oldnum;
			this.nZuanShiNewPrice = newnum;
			this.pBuyBtn_2.SetActive(true);
			this.pBuyBtn_1.SetActive(false);
			this.pXianJiaMoney_l.SetActive(false);
		}
		else
		{
			this.nPrice = oldnum;
			this.nNewPrice = newnum;
			this.pBuyBtn_2.SetActive(false);
			this.pBuyBtn_1.SetActive(true);
			this.pXianJiaMoney_l.SetActive(true);
		}
		bool flag2 = oldnum > 0;
		if (flag2)
		{
			this.pYuanJiaMoney_l.SetActive(true);
			bool flag3 = this.nMoneyType == 2;
			if (flag3)
			{
				this.onSetMoneyObj(this.pYuanJiaMoney_l, "zuanshi", this.nZuanShiPrice);
			}
			else
			{
				bool flag4 = this.nMoneyType == 8;
				if (flag4)
				{
					this.onSetMoneyObj(this.pYuanJiaMoney_l, "wishgold", this.nZuanShiPrice);
				}
				else
				{
					this.onSetMoneyObj(this.pYuanJiaMoney_l, "jinbi", this.nPrice);
				}
			}
		}
		else
		{
			this.pYuanJiaMoney_l.SetActive(false);
		}
		this.pXianJiaMoney_2.SetActive(true);
		bool flag5 = this.nMoneyType == 2;
		if (flag5)
		{
			this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", this.nZuanShiNewPrice);
		}
		else
		{
			bool flag6 = this.nMoneyType == 8;
			if (flag6)
			{
				this.onSetMoneyObj(this.pXianJiaMoney_2, "wishgold", this.nZuanShiNewPrice);
			}
			else
			{
				this.onSetMoneyObj(this.pXianJiaMoney_2, "jinbi", this.nNewPrice);
			}
		}
		this.nAllPrice = this.nPrice;
		this.nXianAllPrice = this.nNewPrice;
		this.nXianZuanShiAllPrice = this.nZuanShiNewPrice;
		int num = PlayerDataManager.getInstance().playerdet.GetMoneyByType(this.nMoneyType);
		num /= newnum;
		this.m_nMaxNum = Mathf.Min(maxnum, num);
		bool flag7 = this.m_nMaxNum < 1;
		if (flag7)
		{
			this.m_nMaxNum = 1;
		}
		bool flag8 = this.nXianAllPrice > PlayerDataManager.getInstance().playerdet.gameCurrency;
		if (flag8)
		{
			this.pBuyBtn_1.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.pBuyBtn_1.GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x00155830 File Offset: 0x00153A30
	private void OnShowPrice()
	{
		int num = 0;
		this.isZheKou = this.pShopData.m_nZheKou;
		bool flag = this.m_nCmdType == 1;
		if (flag)
		{
			this.nPrice = ShopManager.onGetPrice(this.pItemData, 1);
			this.nZuanShiPrice = ShopManager.onGetPrice(this.pItemData, 2);
		}
		else
		{
			this.nPrice = this.pItemData.m_nGlodPrice;
			this.nZuanShiPrice = this.pItemData.m_nDiamondPrice;
		}
		bool flag2 = this.isZheKou == 0;
		if (flag2)
		{
			this.pYuanJiaMoney_l.SetActive(false);
			this.nNewPrice = this.nPrice;
			this.nZuanShiNewPrice = this.nZuanShiPrice;
			bool flag3 = this.m_nCmdType == 1;
			if (flag3)
			{
				bool flag4 = this.nNewPrice > 0 && this.nZuanShiNewPrice > 0;
				if (flag4)
				{
					this.pXianJiaMoney_l.SetActive(true);
					this.pXianJiaMoney_2.SetActive(true);
					this.onSetMoneyObj(this.pXianJiaMoney_l, "jinbi", this.nPrice);
					this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", this.nZuanShiPrice);
					int num2 = PlayerDataManager.getInstance().playerdet.gameCurrency / this.nNewPrice;
					int num3 = PlayerDataManager.getInstance().playerdet.chargeCurrency / this.nZuanShiNewPrice;
					bool flag5 = num2 > num3;
					if (flag5)
					{
						this.m_nMaxNum = num2;
					}
					else
					{
						this.m_nMaxNum = num3;
					}
				}
				else
				{
					this.pXianJiaMoney_l.SetActive(false);
					this.pXianJiaMoney_2.SetActive(true);
					bool flag6 = this.nNewPrice > 0;
					if (flag6)
					{
						this.onSetMoneyObj(this.pXianJiaMoney_2, "jinbi", this.nPrice);
						int nMaxNum = PlayerDataManager.getInstance().playerdet.gameCurrency / this.nNewPrice;
						this.m_nMaxNum = nMaxNum;
						num = 1;
					}
					else
					{
						this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", this.nZuanShiPrice);
						int nMaxNum2 = PlayerDataManager.getInstance().playerdet.chargeCurrency / this.nZuanShiNewPrice;
						this.m_nMaxNum = nMaxNum2;
						num = 2;
					}
				}
			}
			else
			{
				bool flag7 = this.pShopData.m_nZsType == 3;
				if (flag7)
				{
					this.pXianJiaMoney_l.SetActive(true);
					this.pXianJiaMoney_2.SetActive(true);
					this.onSetMoneyObj(this.pXianJiaMoney_l, "jinbi", this.nPrice);
					this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", this.nZuanShiPrice);
					int num4 = PlayerDataManager.getInstance().playerdet.gameCurrency / this.nNewPrice;
					int num5 = PlayerDataManager.getInstance().playerdet.chargeCurrency / this.nZuanShiNewPrice;
					bool flag8 = num4 > num5;
					if (flag8)
					{
						this.m_nMaxNum = num4;
					}
					else
					{
						this.m_nMaxNum = num5;
					}
				}
				else
				{
					this.pXianJiaMoney_l.SetActive(false);
					this.pXianJiaMoney_2.SetActive(true);
					bool flag9 = this.pShopData.m_nZsType == 1;
					if (flag9)
					{
						this.onSetMoneyObj(this.pXianJiaMoney_2, "jinbi", this.nPrice);
						int nMaxNum3 = PlayerDataManager.getInstance().playerdet.gameCurrency / this.nNewPrice;
						this.m_nMaxNum = nMaxNum3;
						num = 1;
					}
					else
					{
						this.onSetMoneyObj(this.pXianJiaMoney_2, "zuanshi", this.nZuanShiPrice);
						int nMaxNum4 = PlayerDataManager.getInstance().playerdet.chargeCurrency / this.nZuanShiNewPrice;
						this.m_nMaxNum = nMaxNum4;
						num = 2;
					}
				}
			}
		}
		int num6 = num;
		int num7 = num6;
		if (num7 != 1)
		{
			if (num7 != 2)
			{
				this.pBuyBtn_2.SetActive(true);
				this.pBuyBtn_1.SetActive(true);
			}
			else
			{
				this.pBuyBtn_2.SetActive(true);
				this.pBuyBtn_1.SetActive(false);
			}
		}
		else
		{
			this.pBuyBtn_2.SetActive(false);
			this.pBuyBtn_1.SetActive(true);
		}
		this.nAllPrice = this.nPrice;
		this.nXianAllPrice = this.nNewPrice;
		this.nXianZuanShiAllPrice = this.nZuanShiNewPrice;
		bool flag10 = this.m_nCmdType == 1;
		if (flag10)
		{
			bool flag11 = this.m_nMaxNum > this.pShopData.m_nMaxnum;
			if (flag11)
			{
				this.m_nMaxNum = this.pShopData.m_nMaxnum;
			}
		}
		else
		{
			bool flag12 = this.m_nMaxNum > this.pShopData.m_nZsMaxnum;
			if (flag12)
			{
				this.m_nMaxNum = this.pShopData.m_nZsMaxnum;
			}
		}
		bool flag13 = this.m_nMaxNum < 1;
		if (flag13)
		{
			this.m_nMaxNum = 1;
		}
		bool flag14 = LiBaoManager.GetInstance().CheckDiscountShop(this.pShopData.key);
		if (flag14)
		{
			this.m_nMaxNum = this.GetMaxNum();
		}
		this.OnValueChang(this.CurCount.ToString());
		bool flag15 = this.nXianAllPrice > PlayerDataManager.getInstance().playerdet.gameCurrency;
		if (flag15)
		{
			this.pBuyBtn_1.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.pBuyBtn_1.GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x00155D54 File Offset: 0x00153F54
	private int GetZhekouMaxNum(int nCost)
	{
		int chargeCurrency = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		int result = chargeCurrency / nCost;
		bool flag = this.pShopData != null && this.pShopData.m_nTitle != 0;
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			int itemNumById = ShopManager.GetInstance().GetItemNumById(1027);
			int num = Mathf.CeilToInt((float)(nCost * itemData.nZhekou) / 100f);
			result = ((chargeCurrency <= num * itemNumById) ? (chargeCurrency / num) : (itemNumById + (chargeCurrency - num * itemNumById) / nCost));
		}
		return result;
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x00155DF4 File Offset: 0x00153FF4
	private int GetMaxNum()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(39);
		DiscountShopData discountShopData = fGameManager.mloadConfig.AllDiscountShopData[actDataByType.nXmlId];
		ItemData itemData = fGameManager.mloadConfig.AllItemData[this.pShopData.m_nItemId];
		int num = 0;
		int nDiamondPrice = itemData.m_nDiamondPrice;
		int chargeCurrency = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		int num5;
		for (int i = 0; i < discountShopData.szDiscountInfo.Count; i += 4)
		{
			int num2 = 0;
			int num3 = discountShopData.szDiscountInfo[i];
			int num4 = discountShopData.szDiscountInfo[i + 2];
			bool flag = nDiamondPrice * num3 * num4 % 100 != 0;
			if (flag)
			{
				num2 = 1;
			}
			num5 = nDiamondPrice * num3 * num4 / 100 + num2;
			bool flag2 = num5 <= chargeCurrency;
			if (!flag2)
			{
				break;
			}
			num2 = 0;
			bool flag3 = nDiamondPrice * num4 % 100 != 0;
			if (flag3)
			{
				num2 = 1;
			}
			num = nDiamondPrice * num4 / 100 + num2;
		}
		bool flag4 = num == 0;
		if (flag4)
		{
			num = nDiamondPrice;
		}
		num5 = chargeCurrency / num;
		int num6 = Mathf.Min(this.pShopData.m_nZsMaxnum, num5);
		bool flag5 = num6 < 1;
		if (flag5)
		{
			num6 = 1;
		}
		return num6;
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x00155F4C File Offset: 0x0015414C
	private void onSetMoneyObj(GameObject obj, string sType, int nPrice)
	{
		obj.transform.FindChild("NumText").GetComponent<Text>().text = nPrice.ToString();
		obj.transform.FindChild("Image").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_" + sType);
		bool flag = this.pShopData != null;
		if (flag)
		{
			this.discountSp.gameObject.SetActive(LiBaoManager.GetInstance().CheckDiscountShop(this.pShopData.key));
		}
		else
		{
			this.discountSp.gameObject.SetActive(false);
		}
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x00155FF7 File Offset: 0x001541F7
	public void OnClickMax(GameObject obj)
	{
		this.CurCount = this.m_nMaxNum;
		this.pInputField.text = this.CurCount.ToString();
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x00156020 File Offset: 0x00154220
	public void ChangeCountClick(GameObject obj)
	{
		string name = obj.name;
		this.CurCount = int.Parse(this.pInputField.text);
		bool flag = name == "zuobtn";
		if (flag)
		{
			bool flag2 = this.CurCount == 1;
			if (flag2)
			{
				return;
			}
			this.CurCount--;
		}
		bool flag3 = name == "youbtn";
		if (flag3)
		{
			bool flag4 = this.CurCount >= this.m_nMaxNum;
			if (flag4)
			{
				return;
			}
			this.CurCount++;
		}
		this.pInputField.text = this.CurCount.ToString();
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x001560CC File Offset: 0x001542CC
	public void OnCloseClick(GameObject obj)
	{
		base.Close();
		GameRoot.inst.Setting.Load_Setting_Data_State = true;
		bool flag = KingTrainManager.GetInstance().GetGameState() == 1;
		if (flag)
		{
			ChatManager.GetInstance().OnChangeMouseShow();
		}
		this.sTipReason = string.Empty;
		this.buyItemFunc = null;
		this.buyItemParam = null;
		this.nMoneyType = 2;
		this.groupNum = 1;
		this.nZhekouMaxNum = 0;
		this.zhekouTg.isOn = false;
		this.zhekouTg.gameObject.SetActive(false);
		this.zhekouItemObj.SetActive(false);
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x0015616C File Offset: 0x0015436C
	private void OnBuyClick(GameObject obj)
	{
		this.m_nBuyType = int.Parse(obj.name.Replace("buy_btn_", string.Empty));
		bool flag = this.m_nBuyType == 1;
		if (flag)
		{
			bool flag2 = this.nXianAllPrice > PlayerDataManager.getInstance().playerdet.gameCurrency;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39506, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			}
		}
		else
		{
			bool flag3 = this.nMoneyType == 2 && this.nXianZuanShiAllPrice > PlayerDataManager.getInstance().playerdet.chargeCurrency;
			if (flag3)
			{
				fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
				return;
			}
			bool flag4 = this.nMoneyType == 8 && this.nXianZuanShiAllPrice > PlayerDataManager.getInstance().playerdet.m_nWishGold;
			if (flag4)
			{
				return;
			}
		}
		bool flag5 = string.IsNullOrEmpty(this.sTipReason);
		if (flag5)
		{
			int num = this.zhekouTg.isOn ? 1 : 0;
			bool flag6 = this.m_nCmdType == 1;
			if (flag6)
			{
				ShopManager.ShopClientToServer(70001, new ArrayList
				{
					this.pShopData.id,
					this.CurCount,
					this.m_nBuyType,
					num
				});
			}
			else
			{
				bool flag7 = ShopManager.GetInstance().m_nGiveSelectCid != 0;
				if (flag7)
				{
					ShopManager.ShopClientToServer(70011, new ArrayList
					{
						this.pShopData.id,
						this.CurCount,
						this.m_nBuyType,
						ShopManager.GetInstance().m_nGiveSelectCid,
						Fun.EncodingUTF8(ShopManager.GetInstance().m_sGiceSelectUserName),
						num
					});
					ShopManager.GetInstance().m_nGiveSelectCid = 0;
					ShopManager.GetInstance().m_sGiceSelectUserName = string.Empty;
					this.OnCloseClick(null);
					bool flag8 = InviteUICon.inst != null;
					if (flag8)
					{
						InviteUICon.inst.ShowUserList();
					}
				}
			}
		}
		else
		{
			bool flag9 = this.buyItemFunc != null;
			if (flag9)
			{
				this.buyItemFunc(this.buyItemParam, this.nMoneyType, this.CurCount);
			}
			this.OnCloseClick(null);
		}
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x00156434 File Offset: 0x00154634
	private void Update()
	{
		bool flag = !this.comTipState && this.comtipBg.texture != null;
		if (flag)
		{
			this.comtipBg.enabled = true;
			this.comTipState = true;
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x00156478 File Offset: 0x00154678
	private void ExitZhekouObj(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x00156488 File Offset: 0x00154688
	private void OverZhekouObj(GameObject go)
	{
		int num = ShopManager.GetInstance().GetItemNumById(1027);
		bool isOn = this.zhekouTg.isOn;
		if (isOn)
		{
			num -= this.CurCount;
		}
		ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
		string text = string.Concat(new string[]
		{
			"<size=20><color=",
			Fun.GetQualColor(itemData.m_nQuality),
			">",
			itemData.m_sName,
			"</color></size>"
		});
		text = text + "\n" + itemData.m_sdesc;
		text = text + "\n" + Language.GetLang(39542, new object[]
		{
			num
		});
		TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x00156564 File Offset: 0x00154764
	private void OnToggleCheck(bool ischk)
	{
		int itemNumById = ShopManager.GetInstance().GetItemNumById(1027);
		bool flag = itemNumById < 1 && ischk;
		if (flag)
		{
			this.zhekouTg.isOn = false;
		}
		else
		{
			if (ischk)
			{
				this.nZhekouMaxNum = Mathf.Min(this.m_nMaxNum, itemNumById);
			}
			this.OnValueChang(this.CurCount.ToString());
		}
	}

	// Token: 0x04002132 RID: 8498
	public static ShopBuyItemUICon inst;

	// Token: 0x04002133 RID: 8499
	private ShopData pShopData;

	// Token: 0x04002134 RID: 8500
	private ItemData pItemData;

	// Token: 0x04002135 RID: 8501
	private int m_nBuyType;

	// Token: 0x04002136 RID: 8502
	private int m_nCmdType;

	// Token: 0x04002137 RID: 8503
	private Text ItemNameText;

	// Token: 0x04002138 RID: 8504
	private RawImage ItemIco;

	// Token: 0x04002139 RID: 8505
	private GameObject pBuyBtn_1;

	// Token: 0x0400213A RID: 8506
	private GameObject pBuyBtn_2;

	// Token: 0x0400213B RID: 8507
	private GameObject pBuyBtn_0;

	// Token: 0x0400213C RID: 8508
	private GameObject pYuanJiaMoney_l;

	// Token: 0x0400213D RID: 8509
	private GameObject pXianJiaMoney_l;

	// Token: 0x0400213E RID: 8510
	private GameObject pXianJiaMoney_2;

	// Token: 0x0400213F RID: 8511
	private InputField pInputField;

	// Token: 0x04002140 RID: 8512
	private GameObject pMaxBtn;

	// Token: 0x04002141 RID: 8513
	private GameObject pZuoBtn;

	// Token: 0x04002142 RID: 8514
	private GameObject pYouBtn;

	// Token: 0x04002143 RID: 8515
	private int CurCount = 1;

	// Token: 0x04002144 RID: 8516
	private RawImage comtipBg;

	// Token: 0x04002145 RID: 8517
	private bool comTipState;

	// Token: 0x04002146 RID: 8518
	private int m_nMaxNum;

	// Token: 0x04002147 RID: 8519
	private GameObject pBuyPanel_1;

	// Token: 0x04002148 RID: 8520
	private GameObject pBuyPanel_2;

	// Token: 0x04002149 RID: 8521
	private GameObject OneHero;

	// Token: 0x0400214A RID: 8522
	private GameObject pPricePanel;

	// Token: 0x0400214B RID: 8523
	private Text pBuyBtn_Text_1;

	// Token: 0x0400214C RID: 8524
	private Text pBuyBtn_Text_2;

	// Token: 0x0400214D RID: 8525
	private Image discountSp;

	// Token: 0x0400214E RID: 8526
	private int groupNum = 1;

	// Token: 0x0400214F RID: 8527
	private int nMoneyType = 2;

	// Token: 0x04002150 RID: 8528
	public const int nZhekouItemId = 1027;

	// Token: 0x04002151 RID: 8529
	private GameObject zhekouItemObj;

	// Token: 0x04002152 RID: 8530
	private RawImage zhekouRimg;

	// Token: 0x04002153 RID: 8531
	private Text zhekouItemTxt;

	// Token: 0x04002154 RID: 8532
	private Toggle zhekouTg;

	// Token: 0x04002155 RID: 8533
	private Text zhekouTgTxt;

	// Token: 0x04002156 RID: 8534
	private int nZhekouMaxNum;

	// Token: 0x04002157 RID: 8535
	private int nAllPrice;

	// Token: 0x04002158 RID: 8536
	private int nXianAllPrice;

	// Token: 0x04002159 RID: 8537
	private int nXianZuanShiAllPrice;

	// Token: 0x0400215A RID: 8538
	private int isZheKou;

	// Token: 0x0400215B RID: 8539
	private ShopManager.BuyItemDelegate buyItemFunc;

	// Token: 0x0400215C RID: 8540
	private object buyItemParam;

	// Token: 0x0400215D RID: 8541
	private string sTipReason = string.Empty;

	// Token: 0x0400215E RID: 8542
	private List<GameObject> ObjItemList = new List<GameObject>();

	// Token: 0x0400215F RID: 8543
	private int nPrice;

	// Token: 0x04002160 RID: 8544
	private int nNewPrice;

	// Token: 0x04002161 RID: 8545
	private int nZuanShiPrice;

	// Token: 0x04002162 RID: 8546
	private int nZuanShiNewPrice;
}

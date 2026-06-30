using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030B RID: 779
public class ShopFuWenItem : Window
{
	// Token: 0x060025BF RID: 9663 RVA: 0x0015683C File Offset: 0x00154A3C
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
		this.TGlodText = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.TZuanShiText = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		EventTriggerListener.Get(this.pBuyBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuy);
		EventTriggerListener.Get(this.pBuyBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuy);
		EventTriggerListener.Get(this.pBuyBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pBuyBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ItemIco.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.ItemIco.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
		this.discountImg = this.UIGameObj.Get_GameObj(9).GetComponent<Image>();
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x001569F9 File Offset: 0x00154BF9
	private void Start()
	{
		this.ItemNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x00156A14 File Offset: 0x00154C14
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
			fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, this.pItemData.m_sIco), this.ItemIco);
			ShopUICon.inst.OnAddRawImage(this.ItemIco);
			this.pIsHot.SetActive(this.pShopData.m_nIsHot == 1);
			this.pIsNew.SetActive(this.pShopData.m_nIsNew == 1);
			this.discountImg.gameObject.SetActive(LiBaoManager.GetInstance().CheckDiscountShop(data.key));
			bool flag2 = this.pShopData.m_nIsLimit == 1;
			if (flag2)
			{
				this.TimeTitle.SetActive(true);
				int num = ShopItemPanelCon.isItemLimit(this.pShopData);
				bool flag3 = num <= 0;
				if (flag3)
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
				bool flag4 = this.pShopData.m_nZheKou == 0;
				if (flag4)
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

	// Token: 0x060025C2 RID: 9666 RVA: 0x00156C5C File Offset: 0x00154E5C
	private void OnShowPrice()
	{
		int nZheKou = this.pShopData.m_nZheKou;
		this.nGlodPrice = this.pItemData.m_nGlodPrice;
		this.nZuanShiPrice = this.pItemData.m_nDiamondPrice;
		bool flag = this.pShopData.m_nIsZengSong == 1;
		if (flag)
		{
			this.pBuyBtn_2.SetActive(true);
		}
		else
		{
			this.pBuyBtn_2.SetActive(false);
		}
		this.TGlodText.text = this.nGlodPrice.ToString();
		this.TZuanShiText.text = this.nZuanShiPrice.ToString();
		bool flag2 = nZheKou == 0 && (this.nGlodPrice == 0 || this.nZuanShiPrice == 0) && this.nGlodPrice == 0 && this.nZuanShiPrice == 0;
		if (flag2)
		{
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x00156D28 File Offset: 0x00154F28
	public void OnClickBuy(GameObject obj)
	{
		string text = obj.name;
		int num = text.LastIndexOf("_");
		text = text.Substring(num + 1, 1);
		num = int.Parse(text);
		bool flag = ShopManager.onGetPrice(this.pItemData, num) <= 0 && this.pItemData.m_nType == 20;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39531, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			ShopManager.GetInstance().OpenBuyItemWin(num, this.pShopData);
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x00156DE4 File Offset: 0x00154FE4
	private void OnHuoDeiEnterObj(GameObject obj)
	{
		int key = int.Parse(obj.name);
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		TipManager.GetInstance().ShowTextTip(itemData.m_sName, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x00156E30 File Offset: 0x00155030
	private void OnHuoDeiExitObj(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x00156E3E File Offset: 0x0015503E
	private void OnEnterObj(GameObject obj)
	{
		TipManager.GetInstance().ShowTextTip(this.pItemData.m_sTipDesc, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x00156E67 File Offset: 0x00155067
	private void OnExitObj(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x04002172 RID: 8562
	private ShopData pShopData;

	// Token: 0x04002173 RID: 8563
	private ItemData pItemData;

	// Token: 0x04002174 RID: 8564
	private Text ItemNameText;

	// Token: 0x04002175 RID: 8565
	private RawImage ItemIco;

	// Token: 0x04002176 RID: 8566
	private GameObject pBuyBtn_1;

	// Token: 0x04002177 RID: 8567
	private GameObject pBuyBtn_2;

	// Token: 0x04002178 RID: 8568
	private GameObject TimeTitle;

	// Token: 0x04002179 RID: 8569
	private Text TimeTxt;

	// Token: 0x0400217A RID: 8570
	private GameObject pIsHot;

	// Token: 0x0400217B RID: 8571
	private GameObject pIsNew;

	// Token: 0x0400217C RID: 8572
	private Image discountImg;

	// Token: 0x0400217D RID: 8573
	private GameObject pNumList;

	// Token: 0x0400217E RID: 8574
	private GameObject pNumImg;

	// Token: 0x0400217F RID: 8575
	private Text TGlodText;

	// Token: 0x04002180 RID: 8576
	private Text TZuanShiText;

	// Token: 0x04002181 RID: 8577
	private int nGlodPrice;

	// Token: 0x04002182 RID: 8578
	private int nZuanShiPrice;
}

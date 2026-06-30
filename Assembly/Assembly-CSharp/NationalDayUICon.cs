using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000252 RID: 594
public class NationalDayUICon : Window
{
	// Token: 0x06001A9F RID: 6815 RVA: 0x000FDE5C File Offset: 0x000FC05C
	protected override void Awake()
	{
		base.Awake();
		this.rechargeItemTg = this.UIGameObj.Get_GameObj(6).GetComponent<Toggle>();
		this.rechargeItemTip = this.rechargeItemTg.transform.Find("Image").GetComponent<Image>();
		this.rechargeItemTxt = this.rechargeItemTg.transform.Find("Label").GetComponent<Text>();
		this.rechargeItemUI = this.UIGameObj.Get_GameObj(7);
		this.rechargeItemUI.AddComponent<RechargeItemUICon>();
		this.rechargeItemTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 4, this.rechargeItemUI);
		});
		this.rechargeItemTg.gameObject.SetActive(false);
		this.rechargeItemUI.SetActive(false);
		this.discountItemTg = this.UIGameObj.Get_GameObj(8).GetComponent<Toggle>();
		this.discountItemTip = this.discountItemTg.transform.Find("Image").GetComponent<Image>();
		this.discountItemTxt = this.discountItemTg.transform.Find("Label").GetComponent<Text>();
		this.discountItemUI = this.UIGameObj.Get_GameObj(9);
		this.discountItemUI.AddComponent<DiscountItemUICon>();
		this.discountItemTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 5, this.discountItemUI);
		});
		this.discountItemTg.gameObject.SetActive(false);
		this.discountItemUI.SetActive(false);
		this.heroLimitBuyTg = this.UIGameObj.Get_GameObj(10).GetComponent<Toggle>();
		this.heroLimitBuyTip = this.heroLimitBuyTg.transform.Find("Image").GetComponent<Image>();
		this.heroLimitBuyTxt = this.heroLimitBuyTg.transform.Find("Label").GetComponent<Text>();
		this.heroLimitBuyUI = this.UIGameObj.Get_GameObj(11);
		this.heroLimitBuyUI.AddComponent<HeroLimitBuyUICon>();
		this.heroLimitBuyTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 6, this.heroLimitBuyUI);
		});
		this.heroLimitBuyTg.gameObject.SetActive(false);
		this.heroLimitBuyUI.SetActive(false);
		this.discountShopToggle = this.UIGameObj.Get_GameObj(12).GetComponent<Toggle>();
		this.discountShopTip = this.discountShopToggle.transform.Find("Image").GetComponent<Image>();
		this.discountShopTxt = this.discountShopToggle.transform.Find("Label").GetComponent<Text>();
		this.discountShopUI = this.UIGameObj.Get_GameObj(13);
		this.discountShopUI.AddComponent<DiscountShopUICon>();
		this.discountShopToggle.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 7, this.discountShopUI);
		});
		this.discountShopToggle.gameObject.SetActive(false);
		this.discountShopUI.SetActive(false);
		this.heroReturnToggle = this.UIGameObj.Get_GameObj(14).GetComponent<Toggle>();
		this.heroReturnTip = this.heroReturnToggle.transform.Find("Image").GetComponent<Image>();
		this.heroReturnTxt = this.heroReturnToggle.transform.Find("Label").GetComponent<Text>();
		this.heroReturnUI = this.UIGameObj.Get_GameObj(15);
		this.heroReturnUI.AddComponent<HeroReturnUICon>();
		this.heroReturnToggle.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 8, this.heroReturnUI);
		});
		this.heroReturnToggle.gameObject.SetActive(false);
		this.heroReturnUI.SetActive(false);
		NationalDayUICon.inst = this;
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x000FE1F3 File Offset: 0x000FC3F3
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000FE200 File Offset: 0x000FC400
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.IsShowRechargeItemTg();
		this.IsShowDiscountItemTg();
		this.IsShowHeroLimitBuyTg();
		this.IsShowDiscountShopTg();
		this.IsShowHeroReturnTg();
		bool activeSelf = this.rechargeItemTg.gameObject.activeSelf;
		if (activeSelf)
		{
			this.rechargeItemTg.isOn = true;
		}
		else
		{
			bool activeSelf2 = this.discountItemTg.gameObject.activeSelf;
			if (activeSelf2)
			{
				this.discountItemTg.isOn = true;
			}
			else
			{
				bool activeSelf3 = this.heroLimitBuyTg.gameObject.activeSelf;
				if (activeSelf3)
				{
					this.heroLimitBuyTg.isOn = true;
				}
				else
				{
					bool activeSelf4 = this.discountShopToggle.gameObject.activeSelf;
					if (activeSelf4)
					{
						this.discountShopToggle.isOn = true;
					}
					else
					{
						bool activeSelf5 = this.heroReturnToggle.gameObject.activeSelf;
						if (activeSelf5)
						{
							this.heroReturnToggle.isOn = true;
						}
					}
				}
			}
		}
		this.ShowRechargeItemImg();
		this.ShowDiscountItemImg();
		this.ShowHeroLimitBuyImg();
		this.ShowDiscountShopImg();
		this.ShowHeroReturnImg();
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000FE338 File Offset: 0x000FC538
	private void BtnClick(bool v, int nType, GameObject go)
	{
		bool flag = this.OldSelectObj != null;
		if (flag)
		{
			this.OldSelectObj.SetActive(false);
		}
		go.SetActive(v);
		this.OldSelectObj = go;
		this.ShowUIData();
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000FE37C File Offset: 0x000FC57C
	public void ShowUIData()
	{
		bool activeSelf = this.discountItemUI.activeSelf;
		if (activeSelf)
		{
			LiBaoManager.GetInstance().SetDiscountItemClick(true);
		}
		bool activeSelf2 = this.heroLimitBuyUI.activeSelf;
		if (activeSelf2)
		{
			LiBaoManager.GetInstance().SetHeroLimitBuyClick(true);
		}
		bool activeSelf3 = this.discountShopUI.activeSelf;
		if (activeSelf3)
		{
			LiBaoManager.GetInstance().SetDiscountShopClick(true);
		}
		bool activeSelf4 = this.heroReturnUI.activeSelf;
		if (activeSelf4)
		{
			LiBaoManager.GetInstance().SetHeroReturnClick(true);
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000FE400 File Offset: 0x000FC600
	private bool IsShowRechargeItemTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(34);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			RechargeItemData rechargeItemData = fGameManager.mloadConfig.AllRechargeItem[actDataByType.nXmlId];
			this.rechargeItemTxt.text = rechargeItemData.sBtnTxt;
		}
		this.rechargeItemTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x000FE470 File Offset: 0x000FC670
	private bool IsShowDiscountItemTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(35);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			DiscountItemData discountItemData = fGameManager.mloadConfig.AllDiscountItem[actDataByType.nXmlId];
			this.discountItemTxt.text = discountItemData.sActName;
		}
		this.discountItemTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x000FE4E0 File Offset: 0x000FC6E0
	private bool IsShowHeroLimitBuyTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(36);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			HeroLimitBuyData heroLimitBuyData = fGameManager.mloadConfig.AllHeroLimitBuyData[actDataByType.nXmlId];
			this.heroLimitBuyTxt.text = heroLimitBuyData.sBtnTxt;
		}
		this.heroLimitBuyTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000FE54E File Offset: 0x000FC74E
	public void ShowRechargeItemImg()
	{
		this.rechargeItemTip.enabled = LiBaoManager.GetInstance().GetRechargeItemState();
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000FE567 File Offset: 0x000FC767
	public void ShowDiscountItemImg()
	{
		this.discountItemTip.enabled = LiBaoManager.GetInstance().GetDiscountItemState();
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x000FE580 File Offset: 0x000FC780
	public void ShowHeroLimitBuyImg()
	{
		this.heroLimitBuyTip.enabled = LiBaoManager.GetInstance().GetHeroLimitBuyState();
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x000FE59C File Offset: 0x000FC79C
	private bool IsShowDiscountShopTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(39);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			DiscountShopData discountShopData = fGameManager.mloadConfig.AllDiscountShopData[actDataByType.nXmlId];
			this.discountShopTxt.text = discountShopData.sActName;
		}
		this.discountShopToggle.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x000FE606 File Offset: 0x000FC806
	public void ShowDiscountShopImg()
	{
		this.discountShopTip.enabled = LiBaoManager.GetInstance().GetDiscountShopState();
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x000FE620 File Offset: 0x000FC820
	private bool IsShowHeroReturnTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(38);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			HeroReturnData heroReturnData = fGameManager.mloadConfig.AllHeroReturnData[actDataByType.nXmlId];
			this.heroReturnTxt.text = heroReturnData.sActName;
		}
		this.heroReturnToggle.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000FE68C File Offset: 0x000FC88C
	public void ShowHeroReturnImg()
	{
		this.heroReturnTip.enabled = LiBaoManager.GetInstance().GetHeroReturnState();
	}

	// Token: 0x04001570 RID: 5488
	public static NationalDayUICon inst;

	// Token: 0x04001571 RID: 5489
	private GameObject OldSelectObj;

	// Token: 0x04001572 RID: 5490
	private GameObject rechargeItemUI;

	// Token: 0x04001573 RID: 5491
	private Toggle rechargeItemTg;

	// Token: 0x04001574 RID: 5492
	private Image rechargeItemTip;

	// Token: 0x04001575 RID: 5493
	private Text rechargeItemTxt;

	// Token: 0x04001576 RID: 5494
	private GameObject discountItemUI;

	// Token: 0x04001577 RID: 5495
	private Toggle discountItemTg;

	// Token: 0x04001578 RID: 5496
	private Image discountItemTip;

	// Token: 0x04001579 RID: 5497
	private Text discountItemTxt;

	// Token: 0x0400157A RID: 5498
	private GameObject heroLimitBuyUI;

	// Token: 0x0400157B RID: 5499
	private Toggle heroLimitBuyTg;

	// Token: 0x0400157C RID: 5500
	private Image heroLimitBuyTip;

	// Token: 0x0400157D RID: 5501
	private Text heroLimitBuyTxt;

	// Token: 0x0400157E RID: 5502
	private GameObject heroReturnUI;

	// Token: 0x0400157F RID: 5503
	private Toggle heroReturnToggle;

	// Token: 0x04001580 RID: 5504
	private Image heroReturnTip;

	// Token: 0x04001581 RID: 5505
	private Text heroReturnTxt;

	// Token: 0x04001582 RID: 5506
	private GameObject discountShopUI;

	// Token: 0x04001583 RID: 5507
	private Toggle discountShopToggle;

	// Token: 0x04001584 RID: 5508
	private Image discountShopTip;

	// Token: 0x04001585 RID: 5509
	private Text discountShopTxt;
}

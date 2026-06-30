using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F8 RID: 248
public class DiscountItemUICon : Window
{
	// Token: 0x060008CC RID: 2252 RVA: 0x0004A6B0 File Offset: 0x000488B0
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.timeText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.tabToggle = this.UIGameObj.Get_GameObj(2);
		this.tabToggle.SetActive(false);
		this.itemRender = this.UIGameObj.Get_GameObj(3);
		this.itemRender.SetActive(false);
		this.preBtn = this.UIGameObj.Get_GameObj(4).GetComponent<Button>();
		EventTriggerListener.Get(this.preBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPageBtn);
		EventTriggerListener.Get(this.preBtn.gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.nextBtn = this.UIGameObj.Get_GameObj(5).GetComponent<Button>();
		EventTriggerListener.Get(this.nextBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPageBtn);
		EventTriggerListener.Get(this.nextBtn.gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.pageNumTxt = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		DiscountItemUICon.inst = this;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0004A7FE File Offset: 0x000489FE
	private void Start()
	{
		this.InitUI();
		this.ShowUIData(1);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0004A810 File Offset: 0x00048A10
	public void Update()
	{
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0004A814 File Offset: 0x00048A14
	private void InitUI()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(35);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			DiscountItemData discountItemData = fGameManager.mloadConfig.AllDiscountItem[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + discountItemData.sIconName + ".txt", this.bgRimg);
			foreach (DiscountInfo discountInfo in discountItemData.szAllInfo.Values)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tabToggle);
				gameObject.transform.SetParent(this.tabToggle.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = "TabToggle" + discountInfo.nId.ToString();
				gameObject.SetActive(true);
				this.szTabToggle.Add(gameObject);
				Text component = gameObject.transform.Find("Label").GetComponent<Text>();
				component.text = discountInfo.sTabName;
				EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTab);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			}
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0004AA34 File Offset: 0x00048C34
	public void ShowUIData(int ntype = 0)
	{
		bool flag = ntype == 1;
		if (flag)
		{
			this.nTabIdx = 1;
			this.nPageIdx = 0;
		}
		bool flag2 = this.nTabIdx - 1 < this.szTabToggle.Count;
		if (flag2)
		{
			this.OnClickTab(this.szTabToggle[this.nTabIdx - 1]);
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0004AA90 File Offset: 0x00048C90
	private void OnClickTab(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(35);
		bool flag = actDataByType != null;
		if (flag)
		{
			DiscountItemData discountItemData = fGameManager.mloadConfig.AllDiscountItem[actDataByType.nXmlId];
			int num = int.Parse(go.name.Replace("TabToggle", string.Empty));
			bool flag2 = this.nTabIdx != num;
			if (flag2)
			{
				this.nPageIdx = 0;
			}
			this.nTabIdx = num;
			DiscountInfo discountInfo = discountItemData.szAllInfo[this.nTabIdx];
			this.nPageMax = Mathf.CeilToInt((float)discountInfo.nItemNum * 1f / 8f);
			go.GetComponent<Toggle>().isOn = true;
			this.ShowItemByPage();
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0004AB54 File Offset: 0x00048D54
	private void OnClickPageBtn(GameObject go)
	{
		bool flag = go.name == "PreBtn";
		if (flag)
		{
			this.nPageIdx--;
		}
		bool flag2 = go.name == "NextBtn";
		if (flag2)
		{
			this.nPageIdx++;
		}
		bool flag3 = this.nPageIdx < 0;
		if (flag3)
		{
			this.nPageIdx = 0;
		}
		bool flag4 = this.nPageIdx > this.nPageMax - 1;
		if (flag4)
		{
			this.nPageIdx = this.nPageMax - 1;
		}
		this.ShowItemByPage();
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0004ABEC File Offset: 0x00048DEC
	private void ShowItemByPage()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(35);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DiscountItemData discountItemData = fGameManager.mloadConfig.AllDiscountItem[actDataByType.nXmlId];
			DiscountInfo discountInfo = discountItemData.szAllInfo[this.nTabIdx];
			fUIManager.OnClearObj(this.szItemRender);
			for (int i = this.nPageIdx * 8; i < discountInfo.szItemInfo.Count; i++)
			{
				ArrayList arrayList = (ArrayList)discountInfo.szItemInfo[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemRender);
				gameObject.transform.SetParent(this.itemRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = "ItemRender" + i.ToString();
				gameObject.SetActive(true);
				this.szItemRender.Add(gameObject);
				int num = (int)arrayList[0];
				ItemData itemData = fGameManager.mloadConfig.AllItemData[num];
				Text component = gameObject.transform.Find("ItemName").GetComponent<Text>();
				component.text = itemData.m_sName;
				num = (int)arrayList[7];
				component = gameObject.transform.Find("Image/DiscountNum").GetComponent<Text>();
				component.text = Language.GetLang(32010, new object[]
				{
					num
				});
				RawImage component2 = gameObject.transform.Find("ItemIcon/RawImage").GetComponent<RawImage>();
				bool flag2 = i < discountInfo.szItemIcon.Length && discountInfo.szItemIcon[i] != "0" && !string.IsNullOrEmpty(discountInfo.szItemIcon[i]);
				if (flag2)
				{
					fUIManager.GetInstance().GetIconAsset("ItemIco/" + discountInfo.szItemIcon[i] + ".txt", component2);
				}
				else
				{
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
				}
				float num2 = 0f;
				component = gameObject.transform.Find("Yuanjia/Text").GetComponent<Text>();
				num2 += component.preferredWidth;
				num = (int)arrayList[2];
				Image component3 = gameObject.transform.Find("Yuanjia/MoneySp").GetComponent<Image>();
				component3.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, this.GetMoneySpStr(num));
				component3.SetNativeSize();
				num2 += component3.preferredWidth;
				num = (int)arrayList[3];
				component = gameObject.transform.Find("Yuanjia/MoneyNum").GetComponent<Text>();
				component.text = num.ToString();
				num2 += component.preferredWidth;
				component3 = gameObject.transform.Find("XianSp").GetComponent<Image>();
				component3.rectTransform.sizeDelta = new Vector2(num2 + 4f, 3f);
				num = (int)arrayList[4];
				component3 = gameObject.transform.Find("Xianjia/MoneySp").GetComponent<Image>();
				component3.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, this.GetMoneySpStr(num));
				component3.SetNativeSize();
				num = (int)arrayList[5];
				component = gameObject.transform.Find("Xianjia/MoneyNum").GetComponent<Text>();
				component.text = num.ToString();
				num = (int)arrayList[6];
				component = gameObject.transform.Find("BuyNum").GetComponent<Text>();
				num -= LiBaoManager.GetInstance().GetDiscountItemState(this.nTabIdx, i, itemData.m_nItemId);
				component.text = Language.GetLang(32008, new object[]
				{
					num
				});
				Button component4 = gameObject.transform.Find("Button").GetComponent<Button>();
				component4.name = string.Format("Button{0}", i);
				bool flag3 = num <= 0;
				if (flag3)
				{
					component4.interactable = false;
				}
				else
				{
					component4.interactable = true;
					EventTriggerListener.Get(component4.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuy);
					EventTriggerListener.Get(component4.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
				bool flag4 = this.szItemRender.Count == 8;
				if (flag4)
				{
					break;
				}
			}
			bool flag5 = this.nPageIdx == 0;
			if (flag5)
			{
				this.preBtn.interactable = false;
			}
			else
			{
				this.preBtn.interactable = true;
			}
			bool flag6 = this.nPageIdx == this.nPageMax - 1;
			if (flag6)
			{
				this.nextBtn.interactable = false;
			}
			else
			{
				this.nextBtn.interactable = true;
			}
			this.pageNumTxt.text = string.Format("{0}/{1}", this.nPageIdx + 1, this.nPageMax);
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0004B170 File Offset: 0x00049370
	private void OnClickBuy(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(35);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DiscountItemData discountItemData = fGameManager.mloadConfig.AllDiscountItem[actDataByType.nXmlId];
			DiscountInfo discountInfo = discountItemData.szAllInfo[this.nTabIdx];
			int num = int.Parse(go.name.Replace("Button", string.Empty));
			ArrayList arrayList = (ArrayList)discountInfo.szItemInfo[num];
			int num2 = (int)arrayList[0];
			int num3 = (int)arrayList[4];
			int num4 = (int)arrayList[5];
			int maxnum = (int)arrayList[6] - LiBaoManager.GetInstance().GetDiscountItemState(this.nTabIdx, num, num2);
			bool flag2 = PlayerDataManager.getInstance().playerdet.GetMoneyByType(num3) < num4;
			if (flag2)
			{
				bool flag3 = num3 == 2;
				if (flag3)
				{
					fUIManager.GetInstance().OpenChongZhiUI("DiscountItemUICon");
				}
			}
			else
			{
				ShopManager.GetInstance().OpenBuyItemWin("discountitem", num2, num3, (int)arrayList[3], num4, 1, maxnum, discountInfo.szItemIcon[num], new ShopManager.BuyItemDelegate(this.OnClickBuy), new ArrayList
				{
					this.nTabIdx,
					num,
					num2
				});
			}
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0004B2EC File Offset: 0x000494EC
	private void OnClickBuy(object szParam, int type, int num)
	{
		ArrayList arrayList = (ArrayList)szParam;
		arrayList.Add(num);
		fGameManager.SendMsgToHallServer(1315, new ArrayList
		{
			15035,
			arrayList
		});
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0004B338 File Offset: 0x00049538
	private string GetMoneySpStr(int ntemp)
	{
		string result = "mainui_zuanshi";
		if (ntemp != 0)
		{
			if (ntemp == 2)
			{
				result = "mainui_zuanshi";
			}
		}
		else
		{
			result = "mainui_jinbi";
		}
		return result;
	}

	// Token: 0x0400065F RID: 1631
	public static DiscountItemUICon inst;

	// Token: 0x04000660 RID: 1632
	private RawImage bgRimg;

	// Token: 0x04000661 RID: 1633
	private Text timeText;

	// Token: 0x04000662 RID: 1634
	private GameObject tabToggle;

	// Token: 0x04000663 RID: 1635
	private GameObject itemRender;

	// Token: 0x04000664 RID: 1636
	private Button preBtn;

	// Token: 0x04000665 RID: 1637
	private Button nextBtn;

	// Token: 0x04000666 RID: 1638
	private Text pageNumTxt;

	// Token: 0x04000667 RID: 1639
	private List<GameObject> szTabToggle = new List<GameObject>();

	// Token: 0x04000668 RID: 1640
	private List<GameObject> szItemRender = new List<GameObject>();

	// Token: 0x04000669 RID: 1641
	private int nTabIdx = 1;

	// Token: 0x0400066A RID: 1642
	private int nPageIdx;

	// Token: 0x0400066B RID: 1643
	private int nPageMax;
}

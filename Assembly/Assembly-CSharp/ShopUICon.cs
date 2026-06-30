using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000310 RID: 784
public class ShopUICon : Window
{
	// Token: 0x0600260B RID: 9739 RVA: 0x0015A24F File Offset: 0x0015844F
	public void OnAddRawImage(RawImage rimg)
	{
		this.m_szAllRawImgList.Add(rimg);
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x0015A260 File Offset: 0x00158460
	public void OnDelRawImage(RawImage rawimg)
	{
		bool flag = !(rawimg == null) && !(rawimg.texture == null);
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(rawimg.texture);
		}
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x0015A29C File Offset: 0x0015849C
	public void OnDelAllRawImage()
	{
		for (int i = 0; i < this.m_szAllRawImgList.Count; i++)
		{
			bool flag = !(this.m_szAllRawImgList[i] == null) && !(this.m_szAllRawImgList[i].texture == null);
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.m_szAllRawImgList[i].texture);
			}
		}
		this.m_szAllRawImgList.Clear();
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x0015A320 File Offset: 0x00158520
	protected override void Awake()
	{
		base.Awake();
		ShopUICon.inst = this;
		this.pLiBaoNumTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.pLiBaoNumObj = this.UIGameObj.Get_GameObj(1);
		this.pOneItem = this.UIGameObj.Get_GameObj(12);
		this.pOneItem.SetActive(false);
		this.pFuWenOneItem = this.UIGameObj.Get_GameObj(21);
		this.pFuWenOneItem.SetActive(false);
		this.pToggleBtn_1 = this.UIGameObj.Get_GameObj(4);
		this.pToggleBtn_2 = this.UIGameObj.Get_GameObj(5);
		this.pToggleBtn_3 = this.UIGameObj.Get_GameObj(6);
		this.pToggleBtn_4 = this.UIGameObj.Get_GameObj(17);
		this.pToggleBtn_5 = this.UIGameObj.Get_GameObj(18);
		this.pToggleBtn_6 = this.UIGameObj.Get_GameObj(22);
		this.pToggleBtn_7 = this.UIGameObj.Get_GameObj(23);
		this.pToggleBtn_20 = this.UIGameObj.Get_GameObj(7);
		this.szToggBtnList.Add(1, this.pToggleBtn_1);
		this.szToggBtnList.Add(2, this.pToggleBtn_2);
		this.szToggBtnList.Add(3, this.pToggleBtn_3);
		this.szToggBtnList.Add(4, this.pToggleBtn_4);
		this.szToggBtnList.Add(5, this.pToggleBtn_5);
		this.szToggBtnList.Add(6, this.pToggleBtn_6);
		this.szToggBtnList.Add(7, this.pToggleBtn_7);
		foreach (GameObject gameObject in this.szToggBtnList.Values)
		{
			bool flag = !(gameObject == null);
			if (flag)
			{
				EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			}
		}
		EventTriggerListener.Get(this.pToggleBtn_20.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		this.OnInitShopData();
		this.pTuiJianPanel = this.UIGameObj.Get_GameObj(8);
		this.pLiBaoPanel = this.UIGameObj.Get_GameObj(9);
		this.pDaoJuPanel = this.UIGameObj.Get_GameObj(10);
		this.pFuWenPanel = this.UIGameObj.Get_GameObj(19);
		this.pFuWenPane2 = this.UIGameObj.Get_GameObj(20);
		this.pHeroPanel = this.UIGameObj.Get_GameObj(24);
		this.pSkinPanel = this.UIGameObj.Get_GameObj(25);
		this.szPanelList.Add(1, this.pTuiJianPanel);
		this.szPanelList.Add(2, this.pLiBaoPanel);
		this.szPanelList.Add(3, this.pDaoJuPanel);
		this.szPanelList.Add(4, this.pFuWenPanel);
		this.szPanelList.Add(5, this.pFuWenPane2);
		this.szPanelList.Add(6, this.pHeroPanel);
		this.szPanelList.Add(7, this.pSkinPanel);
		foreach (int num in this.szPanelList.Keys)
		{
			ShopItemPanelCon shopItemPanelCon = this.szPanelList[num].AddComponent<ShopItemPanelCon>();
			bool flag2 = num == 4 || num == 5;
			if (flag2)
			{
				shopItemPanelCon.InitData(num, this.pFuWenOneItem);
			}
			else
			{
				shopItemPanelCon.InitData(num, this.pOneItem);
			}
			this.AllShopPanelCon.Add(num, shopItemPanelCon);
		}
		this.pBuJiPanel = this.UIGameObj.Get_GameObj(11);
		this.BuJiCon = this.pBuJiPanel.AddComponent<ShopBuJiPanelCon>();
		this.BuJiCon.InitData();
		this.pLeftPanel = this.UIGameObj.Get_GameObj(13);
		this.pToggleBtn_2.SetActive(false);
		this.OpenQQLanZuanBtn = this.UIGameObj.Get_GameObj(14);
		this.pTopRight = this.UIGameObj.Get_GameObj(15);
		bool flag3 = WebParam.client_language == "baxi";
		if (flag3)
		{
			this.pTopRight.SetActive(false);
		}
		else
		{
			this.pTopRight.SetActive(true);
			this.T_QQZuan = this.pTopRight.transform.FindChild("Text").GetComponent<Text>();
			EventTriggerListener.Get(this.OpenQQLanZuanBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnOpenQQUrl);
			EventTriggerListener.Get(this.OpenQQLanZuanBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		}
		this.pItemTip = this.UIGameObj.Get_GameObj(16);
		this.pItemTip.SetActive(false);
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x0015A844 File Offset: 0x00158A44
	private void OnOpenQQUrl(GameObject obj)
	{
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			fGameManager.OpenQQUrl("hzvip", string.Empty, "Prerogative");
		}
		else
		{
			fGameManager.OpenQQUrl("bluevip", string.Empty, "Prerogative");
		}
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x0015A898 File Offset: 0x00158A98
	private void Start()
	{
		base.gameObject.transform.SetSiblingIndex(1);
		this.InitLibaoNum();
		bool flag = !this.OnToggleToBuJi();
		if (flag)
		{
			this.OnClickToggleFun(this.pDefaulToggleBtn, false);
		}
		this.Show_QQ_UrlBtnImg();
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x0015A8E3 File Offset: 0x00158AE3
	public void OnOpenType(int nType)
	{
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x0015A8E8 File Offset: 0x00158AE8
	private void OnInitShopData()
	{
		this.AllShopItemData.Clear();
		this.AllShopItemData = ShopManager.GetInstance().GetAllShop();
		foreach (GameObject gameObject in this.szToggBtnList.Values)
		{
			bool flag = !(gameObject == null);
			if (flag)
			{
				int key = int.Parse(gameObject.name.Replace("Toggle", string.Empty));
				bool flag2 = !this.AllShopItemData.ContainsKey(key);
				if (flag2)
				{
					gameObject.SetActive(false);
				}
				else
				{
					gameObject.SetActive(true);
				}
			}
		}
		foreach (int key2 in this.szTogbtnSort)
		{
			bool activeSelf = this.szToggBtnList[key2].activeSelf;
			if (activeSelf)
			{
				this.pDefaulToggleBtn = this.szToggBtnList[key2];
				break;
			}
		}
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x0015AA24 File Offset: 0x00158C24
	private void OnUpdaetShopData(int nShopItemId, int UpdateType)
	{
		this.OnInitShopData();
		this.AllShopPanelCon[this.m_nPanelType].OnUpdateData(this.AllShopItemData[this.m_nPanelType]);
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x0015AA58 File Offset: 0x00158C58
	private void OnClickToggleFun(GameObject obj, bool isClick)
	{
		int num = int.Parse(obj.name.Replace("Toggle", string.Empty));
		bool flag = num == 1;
		if (flag)
		{
			obj = this.pDefaulToggleBtn;
			num = 1;
		}
		SC_shakeCamera.shakestop();
		bool flag2 = this.m_nPanelType == num;
		if (!flag2)
		{
			this.m_nPanelType = num;
			bool flag3 = this.OnSelectPanel != null;
			if (flag3)
			{
				this.OnSelectPanel.SetActive(false);
			}
			bool flag4 = this.m_nPanelType < 20;
			if (flag4)
			{
				this.AllShopPanelCon[this.m_nPanelType].gameObject.SetActive(true);
				bool flag5 = this.AllShopItemData.ContainsKey(this.m_nPanelType);
				if (flag5)
				{
					this.AllShopPanelCon[this.m_nPanelType].onSetData(this.AllShopItemData[this.m_nPanelType]);
				}
				this.OnSelectPanel = this.AllShopPanelCon[this.m_nPanelType].gameObject;
				this.pLeftPanel.SetActive(true);
				bool flag6 = WebParam.client_language == "baxi";
				if (flag6)
				{
					this.pTopRight.SetActive(false);
				}
				else
				{
					this.pTopRight.SetActive(true);
				}
			}
			else
			{
				this.pBuJiPanel.SetActive(true);
				this.OnSelectPanel = this.pBuJiPanel;
				this.BuJiCon.InitShowLiBao();
				this.pLeftPanel.SetActive(false);
				this.pTopRight.SetActive(false);
			}
			bool flag7 = this.OnSelectToggleBtn != null;
			if (flag7)
			{
				this.OnSelectToggleBtn.GetComponent<Toggle>().enabled = true;
				this.OnSelectToggleBtn.GetComponent<Toggle>().interactable = true;
			}
			if (isClick)
			{
				obj.GetComponent<Toggle>().enabled = false;
			}
			obj.GetComponent<Toggle>().interactable = false;
			this.OnSelectToggleBtn = obj;
		}
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x0015AC4A File Offset: 0x00158E4A
	private void OnClickToggle(GameObject obj)
	{
		this.OnClickToggleFun(obj, true);
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x0015AC58 File Offset: 0x00158E58
	public bool ReBack()
	{
		this.OnHideItemTip();
		bool flag = this.m_nPanelType == 20;
		bool result;
		if (flag)
		{
			this.pLeftPanel.SetActive(true);
			this.OnClickToggleFun(this.pDefaulToggleBtn, false);
			this.BuJiCon.OnClearScn();
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x0015ACAC File Offset: 0x00158EAC
	public override void Reset()
	{
		base.Reset();
		base.gameObject.transform.SetSiblingIndex(1);
		this.OnToggleToBuJi();
		this.OnHideItemTip();
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x0015ACD8 File Offset: 0x00158ED8
	private bool OnToggleToBuJi()
	{
		bool flag = this.m_nLiBaoNum > 10000000;
		bool result;
		if (flag)
		{
			this.OnClickToggleFun(this.pToggleBtn_20, false);
			result = true;
		}
		else
		{
			bool flag2 = this.m_nPanelType == 20 || this.m_nPanelType == 0;
			if (flag2)
			{
				this.OnClickToggleFun(this.pDefaulToggleBtn, false);
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x0015AD39 File Offset: 0x00158F39
	public void InitLibaoNum()
	{
		this.m_nLiBaoNum = ShopManager.GetInstance().nLiBaoNum;
		this.UpdateShowNumTxt();
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x0015AD53 File Offset: 0x00158F53
	public void UpdateLibaoNum()
	{
		this.m_nLiBaoNum = ShopManager.GetInstance().nLiBaoNum;
		this.UpdateShowNumTxt();
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x0015AD70 File Offset: 0x00158F70
	private void UpdateShowNumTxt()
	{
		bool flag = this.m_nLiBaoNum > 0;
		if (flag)
		{
			this.pLiBaoNumObj.SetActive(true);
			this.pLiBaoNumTxt.text = this.m_nLiBaoNum.ToString();
			this.pToggleBtn_20.SetActive(true);
		}
		else
		{
			this.pLiBaoNumObj.SetActive(false);
			this.pToggleBtn_20.SetActive(false);
		}
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x0015ADDC File Offset: 0x00158FDC
	public void showTest(ArrayList arr)
	{
		string text = string.Empty;
		for (int i = 0; i < arr.Count; i++)
		{
			ArrayList arrayList = (ArrayList)arr[i];
			int key = (int)arrayList[0];
			int num = (int)arrayList[1];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			bool flag = text != string.Empty;
			if (flag)
			{
				text += "|";
			}
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"[",
				itemData.m_sName,
				"] ",
				Language.GetLang(39526, new object[0]),
				num.ToString()
			});
		}
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x0015AEB4 File Offset: 0x001590B4
	public override void Close()
	{
		base.Close();
		bool flag = this.BuJiCon != null;
		if (flag)
		{
			this.BuJiCon.OnClearScn();
		}
		SC_shakeCamera.shakestop();
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x0015AEF0 File Offset: 0x001590F0
	public void OnShowItemTip(ItemData pItemData)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		this.pItemTip.SetActive(true);
		this.pItemTip.transform.FindChild("ItemName").GetComponent<Text>().text = pItemData.m_sName;
		this.pItemTip.transform.FindChild("libaomiaoshu").GetComponent<Text>().text = pItemData.m_sTipDesc;
		int count = pItemData.m_szChildItem.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList = (ArrayList)pItemData.m_szChildItem[i];
			int key = (int)arrayList[0];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			text2 = string.Empty;
			text3 = "ffffff";
			int nHeroId = itemData.m_nHeroId;
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[nHeroId];
			bool flag = ShopManager.OnGetHeroHaveEquipOpen(itemData) == 1;
			if (flag)
			{
				text2 = "<color=#36ff00>(已拥有)</color>";
				text3 = "A3A1A1FF";
			}
			bool flag2 = text != string.Empty;
			if (flag2)
			{
				text += "\n";
			}
			string text4 = text;
			text = string.Concat(new string[]
			{
				text4,
				"<color=#",
				text3,
				">",
				itemData.m_sName,
				"</color>",
				text2
			});
		}
		this.pItemTip.transform.FindChild("HeroList").GetComponent<Text>().text = text;
		this.mousepos(this.pItemTip, 20f, -20f);
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x0015B0A4 File Offset: 0x001592A4
	public void OnHideItemTip()
	{
		this.pItemTip.SetActive(false);
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x0015B0B4 File Offset: 0x001592B4
	private void mousepos(GameObject obj, float xpos = 20f, float ypos = -20f)
	{
		Transform transform = obj.transform;
		Vector3 mousePosition = Input.mousePosition;
		RectTransform component = obj.GetComponent<RectTransform>();
		int num = Mathf.CeilToInt(component.sizeDelta.x);
		int num2 = Mathf.CeilToInt(component.sizeDelta.y);
		float screenWidth = SysConfig.ScreenWidth;
		float screenHeight = SysConfig.ScreenHeight;
		mousePosition.x = Mathf.Lerp(-0.5f * screenWidth, 0.5f * screenWidth, mousePosition.x / (float)Screen.width) + xpos;
		mousePosition.y = Mathf.Lerp(-0.5f * screenHeight, 0.5f * screenHeight, mousePosition.y / (float)Screen.height) + ypos;
		mousePosition.z = 0f;
		bool flag = mousePosition.x + (float)num > screenWidth / 2f;
		if (flag)
		{
			mousePosition.x = screenWidth / 2f - (float)num;
		}
		bool flag2 = mousePosition.y - (float)num2 < screenHeight / -2f;
		if (flag2)
		{
			mousePosition.y = (float)num2 - screenHeight / 2f;
		}
		mousePosition.y -= 90f;
		mousePosition.x += 10f;
		transform.localPosition = mousePosition;
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x0015B1F0 File Offset: 0x001593F0
	private void Show_QQ_UrlBtnImg()
	{
		bool flag = !(WebParam.client_language == "baxi");
		if (flag)
		{
			SpriteState spriteState = default(SpriteState);
			bool flag2 = Fun.GetQQ_Zuan_Type(null) == "hz";
			if (flag2)
			{
				spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_SHOP, "huangzuan_an21");
				this.OpenQQLanZuanBtn.GetComponent<Button>().targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SHOP, "huangzuan_an2");
				this.OpenQQLanZuanBtn.GetComponent<Button>().spriteState = spriteState;
				this.OpenQQLanZuanBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(152f, 46f);
				this.T_QQZuan.text = Language.GetLang(92032, new object[0]);
			}
			else
			{
				spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_SHOP, "kaitong_up");
				this.OpenQQLanZuanBtn.GetComponent<Button>().targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SHOP, "kaitong_normal");
				this.OpenQQLanZuanBtn.GetComponent<Button>().spriteState = spriteState;
				this.T_QQZuan.text = Language.GetLang(92031, new object[0]);
				this.OpenQQLanZuanBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(155f, 33f);
			}
		}
	}

	// Token: 0x040021C1 RID: 8641
	public static ShopUICon inst;

	// Token: 0x040021C2 RID: 8642
	private GameObject pOneItem;

	// Token: 0x040021C3 RID: 8643
	private GameObject pFuWenOneItem;

	// Token: 0x040021C4 RID: 8644
	private Text pLiBaoNumTxt;

	// Token: 0x040021C5 RID: 8645
	private GameObject pLiBaoNumObj;

	// Token: 0x040021C6 RID: 8646
	private GameObject pToggleBtn_1;

	// Token: 0x040021C7 RID: 8647
	private GameObject pToggleBtn_2;

	// Token: 0x040021C8 RID: 8648
	private GameObject pToggleBtn_3;

	// Token: 0x040021C9 RID: 8649
	private GameObject pToggleBtn_4;

	// Token: 0x040021CA RID: 8650
	private GameObject pToggleBtn_5;

	// Token: 0x040021CB RID: 8651
	private GameObject pToggleBtn_6;

	// Token: 0x040021CC RID: 8652
	private GameObject pToggleBtn_7;

	// Token: 0x040021CD RID: 8653
	private GameObject pToggleBtn_20;

	// Token: 0x040021CE RID: 8654
	private GameObject pTuiJianPanel;

	// Token: 0x040021CF RID: 8655
	private GameObject pLiBaoPanel;

	// Token: 0x040021D0 RID: 8656
	private GameObject pDaoJuPanel;

	// Token: 0x040021D1 RID: 8657
	private GameObject pBuJiPanel;

	// Token: 0x040021D2 RID: 8658
	private GameObject pLeftPanel;

	// Token: 0x040021D3 RID: 8659
	private GameObject pFuWenPanel;

	// Token: 0x040021D4 RID: 8660
	private GameObject pFuWenPane2;

	// Token: 0x040021D5 RID: 8661
	private GameObject pHeroPanel;

	// Token: 0x040021D6 RID: 8662
	private GameObject pSkinPanel;

	// Token: 0x040021D7 RID: 8663
	private Dictionary<int, GameObject> szToggBtnList = new Dictionary<int, GameObject>();

	// Token: 0x040021D8 RID: 8664
	private Dictionary<int, GameObject> szPanelList = new Dictionary<int, GameObject>();

	// Token: 0x040021D9 RID: 8665
	public int m_nPanelType;

	// Token: 0x040021DA RID: 8666
	public GameObject OnSelectPanel;

	// Token: 0x040021DB RID: 8667
	private GameObject OnSelectToggleBtn;

	// Token: 0x040021DC RID: 8668
	private Dictionary<int, ShopItemPanelCon> AllShopPanelCon = new Dictionary<int, ShopItemPanelCon>();

	// Token: 0x040021DD RID: 8669
	private Dictionary<int, List<ShopData>> AllShopItemData = new Dictionary<int, List<ShopData>>();

	// Token: 0x040021DE RID: 8670
	public ShopBuJiPanelCon BuJiCon;

	// Token: 0x040021DF RID: 8671
	private int m_nLiBaoNum;

	// Token: 0x040021E0 RID: 8672
	private GameObject OpenQQLanZuanBtn;

	// Token: 0x040021E1 RID: 8673
	private GameObject pTopRight;

	// Token: 0x040021E2 RID: 8674
	private GameObject pItemTip;

	// Token: 0x040021E3 RID: 8675
	private Text T_QQZuan;

	// Token: 0x040021E4 RID: 8676
	private GameObject pDefaulToggleBtn;

	// Token: 0x040021E5 RID: 8677
	private List<int> szTogbtnSort = new List<int>
	{
		1,
		6,
		7,
		2,
		3,
		4,
		5
	};

	// Token: 0x040021E6 RID: 8678
	public List<RawImage> m_szAllRawImgList = new List<RawImage>();
}

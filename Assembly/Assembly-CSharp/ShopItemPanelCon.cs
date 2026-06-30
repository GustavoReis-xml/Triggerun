using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030E RID: 782
public class ShopItemPanelCon : Window
{
	// Token: 0x060025D8 RID: 9688 RVA: 0x00157C68 File Offset: 0x00155E68
	protected override void Awake()
	{
		base.Awake();
		this.pPageList = this.UIGameObj.Get_GameObj(0);
		this.pPageBtn = this.UIGameObj.Get_GameObj(1);
		this.pYouBtn = this.UIGameObj.Get_GameObj(2);
		this.pZuoBtn = this.UIGameObj.Get_GameObj(3);
		this.pItemList = this.UIGameObj.Get_GameObj(4);
		this.pDropDown_1 = this.UIGameObj.Get_GameObj(5).GetComponent<Dropdown>();
		this.pDropDown_2 = this.UIGameObj.Get_GameObj(6).GetComponent<Dropdown>();
		this.pInputField = this.UIGameObj.Get_GameObj(7).GetComponent<InputField>();
		this.pFindBtn = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(this.pFindBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFind);
		EventTriggerListener.Get(this.pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYe);
		EventTriggerListener.Get(this.pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYe);
		EventTriggerListener.Get(this.pYouBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pPageBtn.SetActive(false);
		this.pDropDown_1.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Selected_Dropdown_Value(1, _Value);
		});
		this.pDropDown_2.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Selected_Dropdown_Value(2, _Value);
		});
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x00157E10 File Offset: 0x00156010
	public void InitData(int nType, GameObject pItem)
	{
		this.pOneItem = pItem;
		this.m_nPanelType = nType;
		bool flag = this.m_nPanelType == 4 || this.m_nPanelType == 5;
		if (flag)
		{
			this.m_nPageMaxCount = 8;
		}
		else
		{
			this.m_nPageMaxCount = 4;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x00157E68 File Offset: 0x00156068
	public void onSetData(List<ShopData> szItem)
	{
		this.AllItem = szItem;
		this.pDropDown_1.value = 0;
		this.pDropDown_2.value = 0;
		this.nMoneySelectValue = 1;
		this.nXianzhiSelectValue = 1;
		this.AllFilterItemList.Clear();
		foreach (ShopData shopData in this.AllItem)
		{
			bool flag = shopData.m_nIsLimit != 1 || ShopItemPanelCon.isItemLimit(shopData) != 0;
			if (flag)
			{
				this.AllFilterItemList.Add(shopData);
			}
		}
		this.OnShowFilterItem();
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x00157F24 File Offset: 0x00156124
	public void OnUpdateData(List<ShopData> szItem)
	{
		this.AllItem = szItem;
		this.AllFilterItemList = szItem;
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x00157F38 File Offset: 0x00156138
	public static int isItemLimit(ShopData data)
	{
		DateTime d = Convert.ToDateTime(data.m_sStartTime);
		DateTime dateTime = Convert.ToDateTime(data.m_sEndTime);
		DateTime serverDate = Fun.GetServerDate();
		bool flag = (serverDate - d).TotalSeconds < 0.0 || (serverDate - dateTime).TotalSeconds > 0.0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = (int)(dateTime - serverDate).TotalSeconds;
		}
		return result;
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x00157FC0 File Offset: 0x001561C0
	private void Selected_Dropdown_Value(int nType, int nValue)
	{
		bool flag = nType == 1;
		if (flag)
		{
			this.nMoneySelectValue = nValue + 1;
		}
		else
		{
			this.nXianzhiSelectValue = nValue + 1;
		}
		this.OnFilterItem(this.nMoneySelectValue, this.nXianzhiSelectValue, string.Empty);
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x00158008 File Offset: 0x00156208
	public void OnFilterItem(int nType, int nType1, string sFindName = "")
	{
		List<ShopData> list = new List<ShopData>();
		foreach (ShopData shopData in this.AllItem)
		{
			bool flag = shopData.m_nIsLimit == 1 && ShopItemPanelCon.isItemLimit(shopData) == 0;
			if (!flag)
			{
				bool flag2 = sFindName != string.Empty;
				if (flag2)
				{
					bool flag3 = this.GetItemData(shopData.m_nItemId).m_sName == sFindName;
					if (flag3)
					{
						list.Add(shopData);
					}
				}
				else
				{
					bool flag4 = this.isGetMoneyFilter(nType, this.GetItemData(shopData.m_nItemId)) > 0 && this.isGetFilter(nType1, shopData) > 0;
					if (flag4)
					{
						list.Add(shopData);
					}
				}
			}
		}
		this.AllFilterItemList = list;
		this.OnShowFilterItem();
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x00158100 File Offset: 0x00156300
	private void OnShowFilterItem()
	{
		bool flag = this.AllFilterItemList.Count % this.m_nPageMaxCount == 0;
		if (flag)
		{
			this.m_nMaxPage = this.AllFilterItemList.Count / this.m_nPageMaxCount;
		}
		else
		{
			this.m_nMaxPage = this.AllFilterItemList.Count / this.m_nPageMaxCount + 1;
		}
		this.m_nCurPage = 1;
		this.OnCreatPageBtn(this.m_nMaxPage);
		this.OnShowItem(this.AllFilterItemList, 1);
		this.UpdatePageBtnState();
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x00158188 File Offset: 0x00156388
	private int isGetFilter(int nType, ShopData data)
	{
		if (!true)
		{
		}
		int result;
		switch (nType)
		{
		case 1:
			result = 1;
			break;
		case 2:
			result = data.m_nIsLimit;
			break;
		case 3:
			result = data.m_nZheKou;
			break;
		case 4:
			result = data.m_nIsNew;
			break;
		case 5:
			result = data.m_nIsHot;
			break;
		default:
			result = 0;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x001581F0 File Offset: 0x001563F0
	private int isGetMoneyFilter(int nType, ItemData data)
	{
		if (!true)
		{
		}
		int result;
		switch (nType)
		{
		case 1:
			result = 1;
			break;
		case 2:
			result = data.m_nGlodPrice;
			break;
		case 3:
			result = data.m_nDiamondPrice;
			break;
		default:
			result = 0;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x0015823C File Offset: 0x0015643C
	public void OnShowItem(List<ShopData> ItemList, int nCurPage)
	{
		bool flag = ItemList.Count == 0;
		if (flag)
		{
			Debug.Log("没有找到相关物品");
			fUIManager.OnClearObj(this.ObjItemList);
		}
		else
		{
			bool flag2 = nCurPage > this.m_nMaxPage;
			if (!flag2)
			{
				int num = (nCurPage != this.m_nMaxPage) ? (this.m_nPageMaxCount * nCurPage) : ItemList.Count;
				ShopUICon.inst.OnDelAllRawImage();
				fUIManager.OnClearObj(this.ObjItemList);
				for (int i = (nCurPage - 1) * this.m_nPageMaxCount; i < num; i++)
				{
					ShopData shopData = ItemList[i];
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneItem);
					gameObject.transform.SetParent(this.pItemList.transform);
					gameObject.name = shopData.id.ToString();
					gameObject.SetActive(true);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					bool flag3 = this.m_nPanelType == 4 || this.m_nPanelType == 5;
					if (flag3)
					{
						ShopFuWenItem shopFuWenItem = gameObject.AddComponent<ShopFuWenItem>();
						shopFuWenItem.InitData(shopData);
					}
					else
					{
						ShopItemCon shopItemCon = gameObject.AddComponent<ShopItemCon>();
						shopItemCon.InitData(shopData);
					}
					this.ObjItemList.Add(gameObject);
				}
				this.m_nCurPage = nCurPage;
			}
		}
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x001583BC File Offset: 0x001565BC
	public void OnPageBtn(GameObject obj)
	{
		int nCurPage = int.Parse(obj.name);
		this.m_nCurPage = nCurPage;
		bool flag = this.selectPageBtn != null;
		if (flag)
		{
			this.selectPageBtn.GetComponent<Button>().enabled = true;
			this.selectPageBtn.GetComponent<Button>().interactable = true;
		}
		obj.GetComponent<Button>().enabled = false;
		obj.GetComponent<Button>().interactable = false;
		this.selectPageBtn = obj;
		this.OnShowItem(this.AllFilterItemList, this.m_nCurPage);
		this.UpdatePageBtnState();
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x00158450 File Offset: 0x00156650
	private void OnFanYe(GameObject obj)
	{
		string a = string.Empty;
		a = obj.name;
		bool flag = a == "you_btn";
		if (flag)
		{
			bool flag2 = this.m_nCurPage >= this.m_nMaxPage;
			if (flag2)
			{
				return;
			}
			this.m_nCurPage++;
		}
		else
		{
			bool flag3 = this.m_nCurPage <= 1;
			if (flag3)
			{
				return;
			}
			this.m_nCurPage--;
		}
		bool flag4 = this.selectPageBtn != null;
		if (flag4)
		{
			this.selectPageBtn.GetComponent<Button>().enabled = true;
			this.selectPageBtn.GetComponent<Button>().interactable = true;
		}
		this.ObjPageBtnList[this.m_nCurPage - 1].GetComponent<Button>().interactable = false;
		this.selectPageBtn = this.ObjPageBtnList[this.m_nCurPage - 1];
		this.OnShowItem(this.AllFilterItemList, this.m_nCurPage);
		this.UpdatePageBtnState();
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x00158558 File Offset: 0x00156758
	private void OnClickFind(GameObject obj)
	{
		string text = string.Empty;
		text = this.pInputField.text;
		bool flag = !(text == string.Empty);
		if (flag)
		{
			this.OnFilterItem(1, 1, text);
		}
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x00158598 File Offset: 0x00156798
	public void OnCreatPageBtn(int nMaxpage)
	{
		this.selectPageBtn = null;
		fUIManager.OnClearObj(this.ObjPageBtnList);
		for (int i = 0; i < nMaxpage; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pPageBtn);
			gameObject.transform.SetParent(this.pPageList.transform);
			gameObject.name = (i + 1).ToString();
			gameObject.transform.FindChild("Text").GetComponent<Text>().text = (i + 1).ToString();
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ObjPageBtnList.Add(gameObject);
			EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnPageBtn);
			EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		}
		this.ObjPageBtnList[0].GetComponent<Button>().interactable = false;
		this.selectPageBtn = this.ObjPageBtnList[0];
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x001586D0 File Offset: 0x001568D0
	public void UpdatePageBtnState()
	{
		bool flag = this.m_nMaxPage <= 1;
		if (flag)
		{
			this.pZuoBtn.GetComponent<Button>().interactable = false;
			this.pYouBtn.GetComponent<Button>().interactable = false;
		}
		else
		{
			bool flag2 = this.m_nCurPage == 1;
			if (flag2)
			{
				this.pZuoBtn.GetComponent<Button>().interactable = false;
				this.pYouBtn.GetComponent<Button>().interactable = true;
			}
			else
			{
				bool flag3 = this.m_nCurPage == this.m_nMaxPage;
				if (flag3)
				{
					this.pZuoBtn.GetComponent<Button>().interactable = true;
					this.pYouBtn.GetComponent<Button>().interactable = false;
				}
				else
				{
					this.pZuoBtn.GetComponent<Button>().interactable = true;
					this.pYouBtn.GetComponent<Button>().interactable = true;
				}
			}
		}
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x001587B0 File Offset: 0x001569B0
	private ItemData GetItemData(int shopId)
	{
		return fGameManager.mloadConfig.AllItemData[shopId];
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x001587D4 File Offset: 0x001569D4
	private void Start()
	{
	}

	// Token: 0x040021A2 RID: 8610
	public int m_nPanelType;

	// Token: 0x040021A3 RID: 8611
	public int m_nMaxItem;

	// Token: 0x040021A4 RID: 8612
	public GameObject pOneItem;

	// Token: 0x040021A5 RID: 8613
	private GameObject pPageList;

	// Token: 0x040021A6 RID: 8614
	private GameObject pPageBtn;

	// Token: 0x040021A7 RID: 8615
	private GameObject pZuoBtn;

	// Token: 0x040021A8 RID: 8616
	private GameObject pYouBtn;

	// Token: 0x040021A9 RID: 8617
	private GameObject pItemList;

	// Token: 0x040021AA RID: 8618
	private Dropdown pDropDown_1;

	// Token: 0x040021AB RID: 8619
	private Dropdown pDropDown_2;

	// Token: 0x040021AC RID: 8620
	private InputField pInputField;

	// Token: 0x040021AD RID: 8621
	private GameObject pFindBtn;

	// Token: 0x040021AE RID: 8622
	private int m_nPageMaxCount = 4;

	// Token: 0x040021AF RID: 8623
	private List<ShopData> AllItem = new List<ShopData>();

	// Token: 0x040021B0 RID: 8624
	private List<ShopData> AllFilterItemList = new List<ShopData>();

	// Token: 0x040021B1 RID: 8625
	public int m_nMaxPage = 1;

	// Token: 0x040021B2 RID: 8626
	public int m_nCurPage = 1;

	// Token: 0x040021B3 RID: 8627
	private List<GameObject> ObjPageBtnList = new List<GameObject>();

	// Token: 0x040021B4 RID: 8628
	private List<GameObject> ObjItemList = new List<GameObject>();

	// Token: 0x040021B5 RID: 8629
	public int nMoneySelectValue = 1;

	// Token: 0x040021B6 RID: 8630
	public int nXianzhiSelectValue = 1;

	// Token: 0x040021B7 RID: 8631
	public GameObject selectPageBtn;
}

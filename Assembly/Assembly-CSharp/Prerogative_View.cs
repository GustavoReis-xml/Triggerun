using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A5 RID: 677
public class Prerogative_View : MonoBehaviour
{
	// Token: 0x170003BA RID: 954
	// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0011D2A4 File Offset: 0x0011B4A4
	// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0011D302 File Offset: 0x0011B502
	public Prerogative_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Prerogative_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Prerogative_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0011D30C File Offset: 0x0011B50C
	// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0011D36A File Offset: 0x0011B56A
	public Prerogative_Controller Controller_Component
	{
		get
		{
			bool flag = this._Controller_Component == null;
			if (flag)
			{
				this._Controller_Component = base.gameObject.GetComponent<Prerogative_Controller>();
				bool flag2 = this._Controller_Component == null;
				if (flag2)
				{
					this._Controller_Component = base.gameObject.AddComponent<Prerogative_Controller>();
				}
			}
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0011D374 File Offset: 0x0011B574
	// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0011D38C File Offset: 0x0011B58C
	public bool Load_State
	{
		get
		{
			return this._Load_State;
		}
		set
		{
			this._Load_State = value;
		}
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x0011D398 File Offset: 0x0011B598
	public void Initialize()
	{
		EventTriggerListener.Get(this.UI_Component.Prerogative_Page_Text.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Prerogative_Page);
		EventTriggerListener.Get(this.UI_Component.Top_Left_Get_Novice_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Novice_Reward);
		EventTriggerListener.Get(this.UI_Component.Lower_Left_Get_Growth_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Growth_Reward);
		EventTriggerListener.Get(this.UI_Component.Lower_Left_Get_Day_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Day_Reward);
		EventTriggerListener.Get(this.UI_Component.Top_Right_Prerogative_Handle_1_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Prerogative_Handle);
		EventTriggerListener.Get(this.UI_Component.Top_Right_Prerogative_Handle_2_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Prerogative_Handle);
		this.Display_Data();
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x0011D494 File Offset: 0x0011B694
	private void Prerogative_Page(GameObject _GameObject)
	{
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			Debuger.OpenURL("https://vip.qzone.qq.com/");
		}
		else
		{
			Debuger.OpenURL("http://gamevip.qq.com/?ADTAG=VIP.WEB.csjj");
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x0011D4D2 File Offset: 0x0011B6D2
	private void Get_Novice_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			3,
			0
		});
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x0011D4FF File Offset: 0x0011B6FF
	private void Get_Growth_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			2,
			0
		});
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x0011D52C File Offset: 0x0011B72C
	private void Get_Day_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			0,
			0
		});
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			1,
			0
		});
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			4,
			0
		});
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x0011D5B8 File Offset: 0x0011B7B8
	private void Prerogative_Handle(GameObject _GameObject)
	{
		string operate = "bluevip";
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			operate = "hzvip";
		}
		bool flag2 = _GameObject.name == "btn_Prerogative_Handle_1";
		if (flag2)
		{
			fGameManager.OpenQQUrl(operate, string.Empty, "Prerogative");
		}
		else
		{
			fGameManager.OpenQQUrl(operate, string.Empty, "Prerogative");
		}
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x0011D624 File Offset: 0x0011B824
	public void Display_Button()
	{
		bool interactable = false;
		string text = string.Empty;
		bool flag = !Prerogative_Data.Instance.Prerogative_State;
		if (flag)
		{
			this.UI_Component.Top_Left_Get_Novice_Reward_Button.interactable = false;
			this.UI_Component.Top_Left_Get_Novice_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "不可领取";
			this.UI_Component.Lower_Left_Get_Growth_Reward_Button.interactable = false;
			this.UI_Component.Lower_Left_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "不可领取";
			this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = false;
			this.UI_Component.Lower_Left_Get_Day_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "不可领取";
			this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.text = "未达成";
			this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.color = Color.grey;
			this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.text = "未达成";
			this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.color = Color.grey;
			this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.text = "未达成";
			this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.color = Color.grey;
		}
		else
		{
			this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = false;
			bool flag2 = PlayerDataManager.getInstance().playerCommon.m_nisCanGetDay == 1;
			if (flag2)
			{
				this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.text = "领取";
				this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.color = Color.green;
				this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = true;
			}
			else
			{
				bool prerogative_State = Prerogative_Data.Instance.Prerogative_State;
				if (prerogative_State)
				{
					this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.text = "已领取";
					this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.color = Color.white;
				}
				else
				{
					this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.text = "未达成";
					this.UI_Component.Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text.color = Color.grey;
				}
			}
			bool flag3 = PlayerDataManager.getInstance().playerCommon.m_nisCanGetSuper == 1;
			if (flag3)
			{
				this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.text = "领取";
				this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.color = Color.green;
				this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = true;
			}
			else
			{
				bool prerogative_Super = Prerogative_Data.Instance.Prerogative_Super;
				if (prerogative_Super)
				{
					this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.text = "已领取";
					this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.color = Color.white;
				}
				else
				{
					this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.text = "未达成";
					this.UI_Component.Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text.color = Color.grey;
				}
			}
			bool flag4 = PlayerDataManager.getInstance().playerCommon.m_nisCanGetYear == 1;
			if (flag4)
			{
				this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.text = "领取";
				this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.color = Color.green;
				this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = true;
			}
			else
			{
				bool prerogative_Year = Prerogative_Data.Instance.Prerogative_Year;
				if (prerogative_Year)
				{
					this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.text = "已领取";
					this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.color = Color.white;
				}
				else
				{
					this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.text = "未达成";
					this.UI_Component.Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text.color = Color.grey;
				}
			}
			bool flag5 = !this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable;
			if (flag5)
			{
				this.UI_Component.Lower_Left_Get_Day_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "已领取";
			}
			else
			{
				this.UI_Component.Lower_Left_Get_Day_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "领取";
			}
			switch (PlayerDataManager.getInstance().playerCommon.m_nisCanGetNew)
			{
			case 0:
				interactable = false;
				text = "不可领取";
				break;
			case 1:
				interactable = true;
				text = "领取";
				break;
			case 2:
				interactable = false;
				text = "已领取";
				break;
			}
			this.UI_Component.Top_Left_Get_Novice_Reward_Button.interactable = interactable;
			this.UI_Component.Top_Left_Get_Novice_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = text;
			ArrayList growth_Data = Prerogative_Data.Instance.Growth_Data;
			this.UI_Component.Lower_Left_Get_Growth_Reward_Button.interactable = false;
			this.UI_Component.Lower_Left_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "不可领取";
			for (int i = 0; i < growth_Data.Count; i++)
			{
				bool flag6 = int.Parse(growth_Data[i].ToString()) == 0;
				if (flag6)
				{
					this.UI_Component.Lower_Left_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "已领取";
				}
				bool flag7 = int.Parse(growth_Data[i].ToString()) == 1;
				if (flag7)
				{
					this.UI_Component.Lower_Left_Get_Growth_Reward_Button.interactable = true;
					this.UI_Component.Lower_Left_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "领取";
					break;
				}
			}
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x0011DBF4 File Offset: 0x0011BDF4
	public void Display_Data()
	{
		this.Display_Button();
		bool load_State = this.Load_State;
		if (load_State)
		{
			this.Display_Growth_Level_Reward();
		}
		else
		{
			this.Load_State = true;
			bool prerogative_State = Prerogative_Data.Instance.Prerogative_State;
			if (prerogative_State)
			{
				this.UI_Component.Describe_Text.gameObject.SetActive(false);
			}
			else
			{
				this.UI_Component.Describe_Text.gameObject.SetActive(true);
			}
			this.Display_Novice_Reward();
			this.Display_Growth_Level_Reward();
			this.Display_Day_Reward();
			this.Display_Prerogative_Level_Reward();
		}
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0011DC83 File Offset: 0x0011BE83
	private void Display_Novice_Reward()
	{
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(0), this.UI_Component.Top_Left_Item, this.UI_Component.Top_Left_Grid);
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0011DCB0 File Offset: 0x0011BEB0
	private void Display_Growth_Level_Reward()
	{
		Vector2 vector = Vector2.zero;
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(4), this.UI_Component.Lower_Left_1_Item, this.UI_Component.Lower_Left_1_Image_Item, this.UI_Component.Lower_Left_1_Grid);
		vector = this.UI_Component.Lower_Left_1_Content_RectTransform.sizeDelta;
		this.UI_Component.Lower_Left_1_Content_RectTransform.sizeDelta = new Vector2(vector.x, (float)(fGameManager.mloadConfig.GetItemData(4).Keys.Count * 58 + 5));
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x0011DD40 File Offset: 0x0011BF40
	private void Display_Day_Reward()
	{
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(5), this.UI_Component.Lower_Left_2_Image_Item, this.UI_Component.Lower_Left_2_Item_1.transform.Find("Grid").gameObject, string.Empty, string.Empty);
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(2), this.UI_Component.Lower_Left_2_Image_Item, this.UI_Component.Lower_Left_2_Item_2.transform.Find("Grid").gameObject, string.Empty);
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(3), this.UI_Component.Lower_Left_2_Image_Item, this.UI_Component.Lower_Left_2_Item_3.transform.Find("Grid").gameObject, string.Empty);
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x0011DE18 File Offset: 0x0011C018
	private void Display_Prerogative_Level_Reward()
	{
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(5), this.UI_Component.Lower_Right_1_Item, this.UI_Component.Lower_Right_1_Image_Item, this.UI_Component.Lower_Right_1_Grid);
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			this.UI_Component.Lower_Right_1_Grid.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 419f);
			this.UI_Component.Lower_Right_1_Grid.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(-110f, -6f);
			this.UI_Component.Lower_Right_1_Grid.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 440f);
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x0011DEFC File Offset: 0x0011C0FC
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Item_Object, GameObject _Parent_Object)
	{
		List<int> list = new List<int>(_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			bool flag = _Parent_Object.transform.FindChild((i + 1).ToString()) != null;
			GameObject gameObject;
			if (flag)
			{
				gameObject = _Parent_Object.transform.FindChild((i + 1).ToString()).gameObject;
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(_Item_Object);
				gameObject.name = (i + 1).ToString();
				gameObject.transform.SetParent(_Parent_Object.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
			}
			gameObject.SetActive(true);
			RawImage component = gameObject.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
			Text component2 = gameObject.transform.Find("lbl_Name").gameObject.GetComponent<Text>();
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
			Text text = component2;
			string sName = itemData.m_sName;
			string str = "* ";
			object obj = _Data[key];
			text.text = sName + str + ((obj != null) ? obj.ToString() : null);
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0011E0A4 File Offset: 0x0011C2A4
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Item_Object, GameObject _Parent_Object, string a, string b)
	{
		Dictionary<int, int> dictionary = (Dictionary<int, int>)_Data[Prerogative_Data.Instance.Prerogative_Level];
		List<int> list = new List<int>(dictionary.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			bool flag = _Parent_Object.transform.FindChild((i + 1).ToString()) != null;
			GameObject gameObject;
			if (flag)
			{
				gameObject = _Parent_Object.transform.FindChild((i + 1).ToString()).gameObject;
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(_Item_Object);
				gameObject.name = (i + 1).ToString();
				gameObject.transform.SetParent(_Parent_Object.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
			}
			gameObject.SetActive(true);
			RawImage component = gameObject.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
			Text component2 = gameObject.transform.Find("lbl_Amount").gameObject.GetComponent<Text>();
			component2.text = "* " + dictionary[key].ToString();
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
		}
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0011E258 File Offset: 0x0011C458
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Item_Object, GameObject _Image_Item_Object, GameObject _Parent_Object)
	{
		GameObject gameObject = null;
		List<int> list = new List<int>(_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int num = list[i];
			Dictionary<int, int> dictionary = (Dictionary<int, int>)_Data[num];
			bool flag = _Parent_Object.transform.FindChild((i + 1).ToString()) != null;
			GameObject gameObject2;
			if (flag)
			{
				gameObject2 = _Parent_Object.transform.FindChild((i + 1).ToString()).gameObject;
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(_Item_Object);
				gameObject2.name = (i + 1).ToString();
				gameObject2.transform.SetParent(_Parent_Object.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localEulerAngles = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
			}
			gameObject2.SetActive(true);
			bool flag2 = gameObject2.transform.Find("Grid/lbl_Describe") == null;
			if (flag2)
			{
				Image component = gameObject2.transform.Find("img_Level_Icon").GetComponent<Image>();
				component.sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, Fun.GetQQ_Zuan_Type(null) + "_0" + num.ToString());
			}
			else
			{
				Text component2 = gameObject2.transform.Find("Grid/lbl_Describe").GetComponent<Text>();
				component2.text = num.ToString() + "0级";
			}
			List<int> list2 = new List<int>(dictionary.Keys);
			for (int j = 0; j < list2.Count; j++)
			{
				int key = list2[j];
				bool flag3 = gameObject2.transform.Find("Grid").transform.FindChild((j + 1).ToString()) != null;
				if (flag3)
				{
					gameObject = gameObject2.transform.Find("Grid").transform.FindChild((j + 1).ToString()).gameObject;
				}
				else
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(_Image_Item_Object);
					gameObject.name = (j + 1).ToString();
					gameObject.transform.SetParent(gameObject2.transform.Find("Grid"));
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localEulerAngles = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
				}
				gameObject.SetActive(true);
				RawImage component3 = gameObject.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
				Text component2 = gameObject.transform.Find("lbl_Amount").gameObject.GetComponent<Text>();
				component2.text = "* " + dictionary[key].ToString();
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component3);
			}
			bool flag4 = !(gameObject.transform.Find("lbl_State") != null);
			if (!flag4)
			{
				Text component2 = gameObject.transform.Find("lbl_State").gameObject.GetComponent<Text>();
				bool flag5 = num - 1 >= Prerogative_Data.Instance.Growth_Data.Count;
				if (flag5)
				{
					component2.color = Color.grey;
					component2.text = string.Empty;
				}
				else
				{
					bool flag6 = !Prerogative_Data.Instance.Prerogative_State;
					if (flag6)
					{
						component2.color = Color.grey;
						component2.text = string.Empty;
					}
					else
					{
						switch (int.Parse(Prerogative_Data.Instance.Growth_Data[num - 1].ToString()))
						{
						case 0:
							component2.color = Color.white;
							component2.text = "已领取";
							break;
						case 1:
							component2.color = Color.green;
							component2.text = "可领取";
							break;
						case 2:
							component2.color = Color.grey;
							component2.text = string.Empty;
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x0011E6F0 File Offset: 0x0011C8F0
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Image_Item_Object, GameObject _Parent_Object, string _Sign)
	{
		List<int> list = new List<int>(_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(_Image_Item_Object);
			gameObject.transform.SetParent(_Parent_Object.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			RawImage component = gameObject.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
			Text component2 = gameObject.transform.Find("lbl_Amount").gameObject.GetComponent<Text>();
			component2.text = "* " + _Data[key].ToString();
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
			gameObject.SetActive(true);
		}
	}

	// Token: 0x04001C72 RID: 7282
	private Prerogative_UI _UI_Component;

	// Token: 0x04001C73 RID: 7283
	private Prerogative_Controller _Controller_Component;

	// Token: 0x04001C74 RID: 7284
	private bool _Load_State;
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014E RID: 334
public class Gem_View : MonoBehaviour
{
	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00086C6C File Offset: 0x00084E6C
	// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00086CCA File Offset: 0x00084ECA
	public Gem_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Gem_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Gem_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00086CD4 File Offset: 0x00084ED4
	// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00086D32 File Offset: 0x00084F32
	public Gem_Controller Controller_Component
	{
		get
		{
			bool flag = this._Controller_Component == null;
			if (flag)
			{
				this._Controller_Component = base.gameObject.GetComponent<Gem_Controller>();
				bool flag2 = this._Controller_Component == null;
				if (flag2)
				{
					this._Controller_Component = base.gameObject.AddComponent<Gem_Controller>();
				}
			}
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00086D3C File Offset: 0x00084F3C
	public int Hero_ID
	{
		get
		{
			return Gem_UICon.Instance.Hero_ID;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00086D48 File Offset: 0x00084F48
	// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00086D60 File Offset: 0x00084F60
	public bool Display_Sell_State
	{
		get
		{
			return this._Display_Sell_State;
		}
		set
		{
			this._Display_Sell_State = value;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00086D6C File Offset: 0x00084F6C
	// (set) Token: 0x06000EFC RID: 3836 RVA: 0x00086D84 File Offset: 0x00084F84
	public int Display_Type
	{
		get
		{
			return this._Display_Type;
		}
		set
		{
			this._Display_Type = value;
			switch (value)
			{
			case 1:
				this._Display_Type = 4;
				break;
			case 2:
				this._Display_Type = 1;
				break;
			case 3:
				this._Display_Type = 2;
				break;
			case 4:
				this._Display_Type = 3;
				break;
			}
			this.Refresh_Package();
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00086DE4 File Offset: 0x00084FE4
	// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00086DFC File Offset: 0x00084FFC
	public int Display_Quality
	{
		get
		{
			return this._Display_Quality;
		}
		set
		{
			this._Display_Quality = value;
			this.Refresh_Package();
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00086E10 File Offset: 0x00085010
	// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00086E28 File Offset: 0x00085028
	public int Current_Page
	{
		get
		{
			return this._Current_Page;
		}
		set
		{
			bool flag = value <= 1;
			if (flag)
			{
				value = 1;
			}
			bool flag2 = value >= this.Max_Page;
			if (flag2)
			{
				value = this.Max_Page;
			}
			this._Current_Page = value;
			bool flag3 = this._Current_Page <= 1;
			if (flag3)
			{
				this.UI_Component.Right_Prev_Button.interactable = false;
			}
			else
			{
				this.UI_Component.Right_Prev_Button.interactable = true;
			}
			this.Refresh_Package();
			this.UI_Component.Right_Page_Text.text = this._Current_Page.ToString() + "/" + this.Max_Page.ToString();
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00086EDC File Offset: 0x000850DC
	// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00086EF4 File Offset: 0x000850F4
	public int Max_Page
	{
		get
		{
			return this._Max_Page;
		}
		set
		{
			bool flag = this.Current_Page > value;
			if (flag)
			{
				this.Current_Page = value;
			}
			this._Max_Page = value;
			bool flag2 = this._Current_Page >= this._Max_Page;
			if (flag2)
			{
				this.UI_Component.Right_Next_Button.interactable = false;
			}
			else
			{
				this.UI_Component.Right_Next_Button.interactable = true;
			}
			this.UI_Component.Right_Page_Text.text = this.Current_Page.ToString() + "/" + this._Max_Page.ToString();
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00086F94 File Offset: 0x00085194
	// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00086FE6 File Offset: 0x000851E6
	public List<int> Synthetic_Data
	{
		get
		{
			bool flag = this._Synthetic_Data == null;
			if (flag)
			{
				this._Synthetic_Data = new List<int>
				{
					0,
					0,
					0,
					0
				};
			}
			return this._Synthetic_Data;
		}
		set
		{
			this._Synthetic_Data = value;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00086FF0 File Offset: 0x000851F0
	// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00087008 File Offset: 0x00085208
	public int Synthetic_Auto_Location
	{
		get
		{
			return this._Synthetic_Auto_Location;
		}
		set
		{
			this._Synthetic_Auto_Location = value;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00087014 File Offset: 0x00085214
	// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00087046 File Offset: 0x00085246
	public ArrayList Package_Item_Select_Object_List
	{
		get
		{
			bool flag = this._Package_Item_Select_Object_List == null;
			if (flag)
			{
				this._Package_Item_Select_Object_List = new ArrayList();
			}
			return this._Package_Item_Select_Object_List;
		}
		set
		{
			this._Package_Item_Select_Object_List = value;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00087050 File Offset: 0x00085250
	// (set) Token: 0x06000F0A RID: 3850 RVA: 0x00087082 File Offset: 0x00085282
	public Dictionary<int, Vector3> Sell_Item_Data
	{
		get
		{
			bool flag = this._Sell_Item_Data == null;
			if (flag)
			{
				this._Sell_Item_Data = new Dictionary<int, Vector3>();
			}
			return this._Sell_Item_Data;
		}
		set
		{
			this._Sell_Item_Data = value;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0008708C File Offset: 0x0008528C
	// (set) Token: 0x06000F0C RID: 3852 RVA: 0x000870C6 File Offset: 0x000852C6
	public GameObject Gem_Sell_Effect_Parent_Object
	{
		get
		{
			bool flag = this._Gem_Sell_Effect_Parent_Object == null;
			if (flag)
			{
				this._Gem_Sell_Effect_Parent_Object = new GameObject("Gem_Sell_Effect_Parent_Object");
			}
			return this._Gem_Sell_Effect_Parent_Object;
		}
		set
		{
			this._Gem_Sell_Effect_Parent_Object = value;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000870D0 File Offset: 0x000852D0
	// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00087102 File Offset: 0x00085302
	public Dictionary<int, List<GameObject>> Packgae_Item_Data
	{
		get
		{
			bool flag = this._Packgae_Item_Data == null;
			if (flag)
			{
				this._Packgae_Item_Data = new Dictionary<int, List<GameObject>>();
			}
			return this._Packgae_Item_Data;
		}
		set
		{
			this._Packgae_Item_Data = value;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0008710C File Offset: 0x0008530C
	// (set) Token: 0x06000F10 RID: 3856 RVA: 0x00087124 File Offset: 0x00085324
	public bool Synthetic_State
	{
		get
		{
			return this._Synthetic_State;
		}
		set
		{
			this._Synthetic_State = value;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00087130 File Offset: 0x00085330
	// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00087148 File Offset: 0x00085348
	public bool Batch_Synthetic_State
	{
		get
		{
			return this._Batch_Synthetic_State;
		}
		set
		{
			this._Batch_Synthetic_State = value;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00087154 File Offset: 0x00085354
	// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0008716C File Offset: 0x0008536C
	public bool Batch_Synthetic_Auto_State
	{
		get
		{
			return this._Batch_Synthetic_Auto_State;
		}
		set
		{
			this._Batch_Synthetic_Auto_State = value;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00087178 File Offset: 0x00085378
	// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00087190 File Offset: 0x00085390
	public float Batch_Synthetic_Time
	{
		get
		{
			return this._Batch_Synthetic_Time;
		}
		set
		{
			this._Batch_Synthetic_Time = value;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0008719C File Offset: 0x0008539C
	// (set) Token: 0x06000F18 RID: 3864 RVA: 0x000871B4 File Offset: 0x000853B4
	public float Batch_Synthetic_Delay_Time
	{
		get
		{
			return this._Batch_Synthetic_Delay_Time;
		}
		set
		{
			this._Batch_Synthetic_Delay_Time = value;
		}
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x000871BE File Offset: 0x000853BE
	private void OnDisable()
	{
		this.Hidden_Synthetic_Effect();
		this.Hidden_Sell_Effect();
		this.Synthetic_Describe_Tips_Exit(null);
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000871D8 File Offset: 0x000853D8
	public void Initialize()
	{
		EventTriggerListener.Get(this.UI_Component.Left_Buy_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Buy_Gem);
		this.UI_Component.Right_Package_Drag_Area_Object.SetActive(false);
		this.UI_Component.Right_Display_Equip_Toggle.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Change_Tab(this.UI_Component.Right_Display_Equip_Toggle.gameObject, _Value, "Top");
		});
		this.UI_Component.Right_Display_Synthetic_Toggle.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Change_Tab(this.UI_Component.Right_Display_Synthetic_Toggle.gameObject, _Value, "Top");
		});
		this.UI_Component.Right_Display_Batch_Synthetic_Toggle.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Change_Tab(this.UI_Component.Right_Display_Batch_Synthetic_Toggle.gameObject, _Value, "Top");
		});
		this.UI_Component.Right_Display_Package_Toggle.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Change_Tab(this.UI_Component.Right_Display_Package_Toggle.gameObject, _Value, "Down");
		});
		this.UI_Component.Right_Display_Sell_Toggle.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Change_Tab(this.UI_Component.Right_Display_Sell_Toggle.gameObject, _Value, "Down");
		});
		this.UI_Component.Right_Type_Dropdown.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Dropdown_Selected(this.UI_Component.Right_Type_Dropdown.gameObject, _Value);
		});
		this.UI_Component.Right_Quality_Dropdown.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Dropdown_Selected(this.UI_Component.Right_Quality_Dropdown.gameObject, _Value);
		});
		EventTriggerListener.Get(this.UI_Component.Right_Prev_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Prev_Page);
		EventTriggerListener.Get(this.UI_Component.Right_Next_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Next_Page);
		EventTriggerListener.Get(this.UI_Component.Right_Item_Select_All_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Item_Select_All);
		EventTriggerListener.Get(this.UI_Component.Right_Sell_Select_All_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Sell_Select_All);
		EventTriggerListener.Get(this.UI_Component.Right_Cancel_Select_All_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Cancel_Select_All);
		EventTriggerListener.Get(this.UI_Component.Right_Attack_Location_Icon_Image_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Attack_Location_Icon_Image_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Attack_Location_Icon_Image_3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Defend_Location_Icon_Image_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Defend_Location_Icon_Image_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Skill_Location_Icon_Image_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Skill_Location_Icon_Image_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Stunt_Location_Icon_Image_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Unequip);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic_Cencel);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic_Cencel);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic_Cencel);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_4.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic_Cencel);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Batch_Synthetic_Place_Cancel);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Batch_Synthetic_Place_Cancel);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Batch_Synthetic);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Describe_Object.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Synthetic_Describe_Tips_Enter);
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Describe_Object.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Synthetic_Describe_Tips_Exit);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Tips_Button_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.aaa);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Tips_Button_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.bbb);
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Tips_Button_3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.ccc);
		this._Current_Page = 1;
		this.UI_Component.Right_Prev_Button.interactable = false;
		this.Max_Page = (Gem_Data.Instance.Gem_Package_Data.Count - 1) / 15 + 1;
		this._Display_Type = 0;
		this.Display_Quality = 0;
		this.Synthetic_Auto_Location = 1;
		this.Display_Equip();
		this.UI_Component.Right_Synthetic_Success_Rate_Describe_Text.fontSize = 14;
		this.UI_Component.Right_Batch_Synthetic_Success_Rate_Describe_Text.fontSize = 14;
		this.UI_Component.Right_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
		{
			this.Get_Synthetic_Success_Rate()
		});
		this.UI_Component.Right_Batch_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
		{
			this.Get_Batch_Synthetic_Success_Rate()
		});
		foreach (Dropdown.OptionData optionData in this.UI_Component.Right_Type_Dropdown.options)
		{
			optionData.text = Language.GetLang(int.Parse(optionData.text), new object[0]);
		}
		foreach (Dropdown.OptionData optionData2 in this.UI_Component.Right_Quality_Dropdown.options)
		{
			optionData2.text = Language.GetLang(int.Parse(optionData2.text), new object[0]);
		}
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x00087890 File Offset: 0x00085A90
	private void Buy_Gem(GameObject _GameObject)
	{
		ShopManager.GetInstance().OnPenShopWin(1);
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x000878A0 File Offset: 0x00085AA0
	private void Change_Tab(GameObject _GameObject, bool _State, string _Type)
	{
		bool flag = _Type == "Top";
		if (flag)
		{
			bool flag2 = _GameObject.name == "btn_Display_Equip";
			if (flag2)
			{
				this._Tab_State = 1;
				this.Clear_Synthetic_Data();
				this.Batch_Synthetic_Place_Cancel(null);
				this.UI_Component.Right_Equip_View_Object.SetActive(_State);
				this.Hidden_Synthetic_Effect();
			}
			bool flag3 = _GameObject.name == "btn_Display_Synthetic";
			if (flag3)
			{
				this._Tab_State = 2;
				this.UI_Component.Right_Synthetic_View_Object.SetActive(_State);
				this.Batch_Synthetic_Place_Cancel(null);
				this.Synthetic_Describe_Tips_Exit(null);
			}
			bool flag4 = _GameObject.name == "btn_Display_Batch_Synthetic";
			if (flag4)
			{
				this._Tab_State = 3;
				this.Clear_Synthetic_Data();
				this.UI_Component.Right_Batch_Synthetic_View_Object.SetActive(_State);
				this.Hidden_Synthetic_Effect();
			}
		}
		bool flag5 = _Type == "Down";
		if (flag5)
		{
			bool flag6 = _GameObject.name == "btn_Display_Package";
			if (flag6)
			{
				if (_State)
				{
					this.Display_Sell_State = false;
					this.Select_Or_Cancel_All(false);
					this.Display_Or_Hidden_Item_Select_Button(false);
					this.UI_Component.Right_Item_Select_All_Button.gameObject.SetActive(false);
					this.UI_Component.Right_Sell_Select_All_Button.gameObject.SetActive(false);
					this.UI_Component.Right_Cancel_Select_All_Button.gameObject.SetActive(false);
				}
			}
			else if (_State)
			{
				this.Display_Sell_State = true;
				this.Display_Or_Hidden_Item_Select_Button(true);
				this.UI_Component.Right_Item_Select_All_Button.gameObject.SetActive(true);
				this.UI_Component.Right_Sell_Select_All_Button.gameObject.SetActive(true);
				this.UI_Component.Right_Cancel_Select_All_Button.gameObject.SetActive(true);
			}
		}
		bool flag7 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
		if (flag7)
		{
			Gem_Tips_UICon.Instance.Close();
		}
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00087AA4 File Offset: 0x00085CA4
	private void Dropdown_Selected(GameObject _GameObject, int _Parameter)
	{
		bool flag = _GameObject.name == "ddl_Type";
		if (flag)
		{
			this.Display_Type = _Parameter;
		}
		bool flag2 = _GameObject.name == "ddl_Quality";
		if (flag2)
		{
			this.Display_Quality = _Parameter;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00087AEE File Offset: 0x00085CEE
	public void Display_Equip()
	{
		this.Refresh_Equip();
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00087AF8 File Offset: 0x00085CF8
	private void Refresh_Equip()
	{
		ArrayList arrayList = null;
		Image icon_Component = null;
		Image quality_Component = null;
		Text index_Component = null;
		Text id_Component = null;
		Dictionary<int, ArrayList> gem_Equip_Data = Gem_Data.Instance.Gem_Equip_Data;
		bool flag = gem_Equip_Data.ContainsKey(this.Hero_ID);
		if (flag)
		{
			arrayList = gem_Equip_Data[this.Hero_ID];
		}
		bool flag2 = arrayList != null;
		ArrayList arrayList2;
		if (flag2)
		{
			arrayList2 = (ArrayList)arrayList[0];
		}
		else
		{
			arrayList2 = new ArrayList
			{
				"0",
				"0",
				"0"
			};
		}
		for (int i = 0; i < arrayList2.Count; i++)
		{
			switch (i)
			{
			case 0:
				icon_Component = this.UI_Component.Right_Attack_Location_Icon_Image_1;
				quality_Component = this.UI_Component.Right_Attack_Location_Quality_Image_1;
				index_Component = this.UI_Component.Right_Attack_Location_Index_Text_1;
				id_Component = this.UI_Component.Right_Attack_Location_ID_Text_1;
				break;
			case 1:
				icon_Component = this.UI_Component.Right_Attack_Location_Icon_Image_2;
				quality_Component = this.UI_Component.Right_Attack_Location_Quality_Image_2;
				index_Component = this.UI_Component.Right_Attack_Location_Index_Text_2;
				id_Component = this.UI_Component.Right_Attack_Location_ID_Text_2;
				break;
			case 2:
				icon_Component = this.UI_Component.Right_Attack_Location_Icon_Image_3;
				quality_Component = this.UI_Component.Right_Attack_Location_Quality_Image_3;
				index_Component = this.UI_Component.Right_Attack_Location_Index_Text_3;
				id_Component = this.UI_Component.Right_Attack_Location_ID_Text_3;
				break;
			}
			bool flag3 = Fun.Convert_Int(arrayList2[i].ToString()) != 0;
			if (flag3)
			{
				int gem_Index = Fun.Convert_Int(arrayList2[i].ToString());
				Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
				bool flag4 = gem_Entity != null;
				if (flag4)
				{
					this.Display_Equip_Item(gem_Entity.Icon, gem_Entity.Quality.ToString(), gem_Index.ToString(), arrayList2[i].ToString(), icon_Component, quality_Component, index_Component, id_Component);
				}
				else
				{
					this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
				}
			}
			else
			{
				this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
			}
		}
		bool flag5 = arrayList != null;
		if (flag5)
		{
			arrayList2 = (ArrayList)arrayList[1];
		}
		else
		{
			arrayList2 = new ArrayList
			{
				"0",
				"0"
			};
		}
		for (int i = 0; i < arrayList2.Count; i++)
		{
			int num = i;
			int num2 = num;
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					icon_Component = this.UI_Component.Right_Skill_Location_Icon_Image_2;
					quality_Component = this.UI_Component.Right_Skill_Location_Quality_Image_2;
					index_Component = this.UI_Component.Right_Skill_Location_Index_Text_2;
					id_Component = this.UI_Component.Right_Skill_Location_ID_Text_2;
				}
			}
			else
			{
				icon_Component = this.UI_Component.Right_Skill_Location_Icon_Image_1;
				quality_Component = this.UI_Component.Right_Skill_Location_Quality_Image_1;
				index_Component = this.UI_Component.Right_Skill_Location_Index_Text_1;
				id_Component = this.UI_Component.Right_Skill_Location_ID_Text_1;
			}
			bool flag6 = Fun.Convert_Int(arrayList2[i].ToString()) != 0;
			if (flag6)
			{
				int gem_Index = Fun.Convert_Int(arrayList2[i].ToString());
				Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
				bool flag7 = gem_Entity != null;
				if (flag7)
				{
					this.Display_Equip_Item(gem_Entity.Icon, gem_Entity.Quality.ToString(), gem_Index.ToString(), arrayList2[i].ToString(), icon_Component, quality_Component, index_Component, id_Component);
				}
				else
				{
					this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
				}
			}
			else
			{
				this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
			}
		}
		bool flag8 = arrayList != null;
		if (flag8)
		{
			arrayList2 = (ArrayList)arrayList[2];
		}
		else
		{
			arrayList2 = new ArrayList
			{
				"0",
				"0"
			};
		}
		for (int i = 0; i < arrayList2.Count - 1; i++)
		{
			bool flag9 = i == 0;
			if (flag9)
			{
				icon_Component = this.UI_Component.Right_Stunt_Location_Icon_Image_1;
				quality_Component = this.UI_Component.Right_Stunt_Location_Quality_Image_1;
				index_Component = this.UI_Component.Right_Stunt_Location_Index_Text_1;
				id_Component = this.UI_Component.Right_Stunt_Location_ID_Text_1;
			}
			bool flag10 = Fun.Convert_Int(arrayList2[i].ToString()) != 0;
			if (flag10)
			{
				int gem_Index = Fun.Convert_Int(arrayList2[i].ToString());
				Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
				bool flag11 = gem_Entity != null;
				if (flag11)
				{
					this.Display_Equip_Item(gem_Entity.Icon, gem_Entity.Quality.ToString(), gem_Index.ToString(), arrayList2[i].ToString(), icon_Component, quality_Component, index_Component, id_Component);
				}
				else
				{
					this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
				}
			}
			else
			{
				this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
			}
		}
		bool flag12 = arrayList != null;
		if (flag12)
		{
			arrayList2 = (ArrayList)arrayList[3];
		}
		else
		{
			arrayList2 = new ArrayList
			{
				"0",
				"0"
			};
		}
		for (int i = 0; i < arrayList2.Count; i++)
		{
			int num3 = i;
			int num4 = num3;
			if (num4 != 0)
			{
				if (num4 == 1)
				{
					icon_Component = this.UI_Component.Right_Defend_Location_Icon_Image_2;
					quality_Component = this.UI_Component.Right_Defend_Location_Quality_Image_2;
					index_Component = this.UI_Component.Right_Defend_Location_Index_Text_2;
					id_Component = this.UI_Component.Right_Defend_Location_ID_Text_2;
				}
			}
			else
			{
				icon_Component = this.UI_Component.Right_Defend_Location_Icon_Image_1;
				quality_Component = this.UI_Component.Right_Defend_Location_Quality_Image_1;
				index_Component = this.UI_Component.Right_Defend_Location_Index_Text_1;
				id_Component = this.UI_Component.Right_Defend_Location_ID_Text_1;
			}
			bool flag13 = Fun.Convert_Int(arrayList2[i].ToString()) != 0;
			if (flag13)
			{
				int gem_Index = Fun.Convert_Int(arrayList2[i].ToString());
				Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
				bool flag14 = gem_Entity != null;
				if (flag14)
				{
					this.Display_Equip_Item(gem_Entity.Icon, gem_Entity.Quality.ToString(), gem_Index.ToString(), arrayList2[i].ToString(), icon_Component, quality_Component, index_Component, id_Component);
				}
				else
				{
					this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
				}
			}
			else
			{
				this.Display_Equip_Item("0", "0", "0", "0", icon_Component, quality_Component, index_Component, id_Component);
			}
		}
		this.Display_Effect_Describe();
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x00088224 File Offset: 0x00086424
	private void Display_Equip_Item(string _Icon, string _Quality, string _Gem_Index, string _Gem_ID, Image _Icon_Component, Image _Quality_Component, Text _Index_Component, Text _ID_Component)
	{
		bool flag = _Icon == "0" && _Quality == "0" && _Gem_Index == "0" && _Gem_ID == "0";
		if (flag)
		{
			_Icon_Component.gameObject.SetActive(false);
			_Quality_Component.gameObject.SetActive(false);
			_Index_Component.text = string.Empty;
			_ID_Component.text = "0";
			EventTriggerListener.Get(_Icon_Component.gameObject).onEnter = null;
			EventTriggerListener.Get(_Icon_Component.gameObject).onExit = null;
		}
		else
		{
			bool flag2 = _Icon_Component.sprite != null && _Icon_Component.sprite.texture != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(_Icon_Component.sprite.texture, true);
			}
			Fun.Load_Image("Icon/fuwen_icon", _Icon_Component, _Icon);
			_Quality_Component.sprite = Window.spriteName(AtlasType.ATLAS_GEM, _Quality);
			_Index_Component.text = _Gem_Index.ToString();
			_ID_Component.text = _Gem_ID.ToString();
			_Icon_Component.gameObject.SetActive(true);
			_Quality_Component.gameObject.SetActive(true);
			EventTriggerListener.Get(_Icon_Component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
			EventTriggerListener.Get(_Icon_Component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0008839D File Offset: 0x0008659D
	public void Display_Package()
	{
		this.Refresh_Package();
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000883A8 File Offset: 0x000865A8
	private void Refresh_Package()
	{
		int num = 1;
		int num2 = 1;
		List<int> list = this.Package_Filter();
		this.Max_Page = (list.Count - 1) / 15 + 1;
		this.Package_Item_Select_Object_List.Clear();
		int i = (this.Current_Page - 1) * 15;
		while (i < this.Current_Page * 15)
		{
			bool flag = num > 5;
			if (flag)
			{
				num = 1;
				num2++;
			}
			GameObject gameObject;
			switch (num2)
			{
			case 1:
				gameObject = this.UI_Component.Right_Package_Cell_Object_1;
				goto IL_D7;
			case 2:
				gameObject = this.UI_Component.Right_Package_Cell_Object_2;
				goto IL_D7;
			case 3:
				gameObject = this.UI_Component.Right_Package_Cell_Object_3;
				goto IL_D7;
			default:
				Debug.LogError(num.ToString() + " --- " + num2.ToString());
				break;
			}
			IL_41C:
			i++;
			continue;
			IL_D7:
			bool flag2 = !this.Packgae_Item_Data.ContainsKey(num2);
			if (flag2)
			{
				this.Packgae_Item_Data.Add(num2, new List<GameObject>
				{
					null,
					null,
					null,
					null,
					null
				});
			}
			bool flag3 = this.Packgae_Item_Data[num2][num - 1] == null;
			GameObject gameObject2;
			if (flag3)
			{
				gameObject2 = gameObject.transform.FindChild("Item_" + num.ToString()).gameObject;
				this.Packgae_Item_Data[num2][num - 1] = gameObject2;
			}
			else
			{
				gameObject2 = this.Packgae_Item_Data[num2][num - 1];
			}
			num++;
			bool flag4 = i >= list.Count;
			if (flag4)
			{
				gameObject2.SetActive(false);
				goto IL_41C;
			}
			int gem_ID = list[i];
			List<int> list2 = this.Get_Package_Gem_Data(gem_ID);
			int key = list2[0];
			int num3 = list2[2];
			bool flag5 = fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(key);
			if (flag5)
			{
				Gem_Entity gem_Entity = fGameManager.mloadConfig.Gem_Xml_Data[key];
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.FindChild("lbl_Index").GetComponent<Text>().text = key.ToString();
				gameObject2.transform.FindChild("lbl_ID").GetComponent<Text>().text = gem_ID.ToString();
				gameObject2.transform.FindChild("lbl_Amount").GetComponent<Text>().text = num3.ToString();
				Image component = gameObject2.transform.FindChild("img_Icon").GetComponent<Image>();
				bool flag6 = component.sprite != null && component.sprite.texture != null;
				if (flag6)
				{
					UnityEngine.Object.DestroyImmediate(component.sprite.texture, true);
				}
				Fun.Load_Image("Icon/fuwen_icon", component, gem_Entity.Icon);
				gameObject2.transform.FindChild("img_Quality").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				gameObject2.transform.FindChild("btn_Select").gameObject.SetActive(this.Display_Sell_State);
				EventTriggerListener.Get(gameObject2.transform.FindChild("img_Icon").gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
				EventTriggerListener.Get(gameObject2.transform.FindChild("img_Icon").gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
				EventTriggerListener.Get(gameObject2.transform.FindChild("img_Icon").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Item_Click);
				gameObject2.SetActive(true);
				this.Package_Item_Select_Object_List.Add(gameObject2.transform.FindChild("btn_Select").gameObject);
			}
			goto IL_41C;
		}
		this.Select_Or_Cancel_All(false);
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x000887F4 File Offset: 0x000869F4
	private List<int> Package_Filter()
	{
		Dictionary<int, List<int>> gem_Package_Data = Gem_Data.Instance.Gem_Package_Data;
		List<int> list = new List<int>(gem_Package_Data.Keys);
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < list.Count; i++)
		{
			List<int> list2 = gem_Package_Data[list[i]];
			int num = Fun.Convert_Int(list2[1].ToString());
			int key = this.Get_Package_Gem_Index(num);
			bool flag = !fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(key);
			if (!flag)
			{
				Gem_Entity gem_Entity = fGameManager.mloadConfig.Gem_Xml_Data[key];
				bool flag2 = gem_Entity.Limit_Hero_Index != 0 && gem_Entity.Limit_Hero_Index != this.Hero_ID;
				if (!flag2)
				{
					bool flag3 = gem_Entity.Type == 5;
					if (flag3)
					{
						dictionary.Add(key, num);
					}
					else
					{
						bool flag4 = this.Display_Type == 0 && this.Display_Quality == 0;
						if (flag4)
						{
							bool flag5 = !dictionary.ContainsKey(key);
							if (flag5)
							{
								dictionary.Add(key, num);
							}
						}
						else
						{
							bool flag6 = this.Display_Type == 0 && this.Display_Quality != 0;
							if (flag6)
							{
								bool flag7 = gem_Entity.Quality == this.Display_Quality && !dictionary.ContainsKey(key);
								if (flag7)
								{
									dictionary.Add(key, num);
								}
							}
							else
							{
								bool flag8 = this.Display_Type != 0 && this.Display_Quality == 0;
								if (flag8)
								{
									bool flag9 = this.Display_Type == gem_Entity.Type && !dictionary.ContainsKey(key);
									if (flag9)
									{
										dictionary.Add(key, num);
									}
								}
								else
								{
									bool flag10 = this.Display_Type != 0 && this.Display_Quality != 0 && this.Display_Type == gem_Entity.Type && this.Display_Quality == gem_Entity.Quality && !dictionary.ContainsKey(key);
									if (flag10)
									{
										dictionary.Add(key, num);
									}
								}
							}
						}
					}
				}
			}
		}
		list = new List<int>(dictionary.Keys);
		List<int> list3 = new List<int>(dictionary.Values);
		for (int i = 0; i < list.Count; i++)
		{
			for (int j = 0; j < list.Count - i - 1; j++)
			{
				bool flag11 = list[j] > list[j + 1];
				if (flag11)
				{
					int value = list[j];
					list[j] = list[j + 1];
					list[j + 1] = value;
					value = list3[j];
					list3[j] = list3[j + 1];
					list3[j + 1] = value;
				}
			}
		}
		return list3;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00088B14 File Offset: 0x00086D14
	private void Display_Or_Hidden_Item_Select_Button(bool _State)
	{
		for (int i = 0; i < this.Package_Item_Select_Object_List.Count; i++)
		{
			GameObject gameObject = (GameObject)this.Package_Item_Select_Object_List[i];
			gameObject.SetActive(_State);
		}
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x00088B5C File Offset: 0x00086D5C
	private void Prev_Page(GameObject _GameObject)
	{
		int current_Page = this.Current_Page;
		this.Current_Page = current_Page - 1;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00088B7C File Offset: 0x00086D7C
	private void Next_Page(GameObject _GameObject)
	{
		int current_Page = this.Current_Page;
		this.Current_Page = current_Page + 1;
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x00088B9B File Offset: 0x00086D9B
	private void Item_Select_All(GameObject _GameObject)
	{
		this.Select_Or_Cancel_All(true);
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00088BA8 File Offset: 0x00086DA8
	private void Sell_Select_All(GameObject _GameObject)
	{
		List<int> sell_Gem_ID_List = this.Get_Sell_Gem_ID_List();
		bool flag = sell_Gem_ID_List.Count >= 1;
		if (flag)
		{
			bool flag2 = this._Tab_State == 3;
			if (flag2)
			{
				this.Batch_Synthetic_Place_Cancel(null);
			}
			fUIManager.GetInstance().ShowWin("Gem_Sell_UI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.Load_Gem_Sell_UI_Complete), sell_Gem_ID_List, true);
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00088C08 File Offset: 0x00086E08
	private void Load_Gem_Sell_UI_Complete(string _UI_Name, GameObject _UI_Object, object _Parameters)
	{
		Gem_Sell_View gem_Sell_View = _UI_Object.GetComponent<Gem_Sell_View>();
		bool flag = gem_Sell_View == null;
		if (flag)
		{
			gem_Sell_View = _UI_Object.AddComponent<Gem_Sell_View>();
			gem_Sell_View.Parent_View_Component = this;
		}
		List<int> sell_Data = (List<int>)_Parameters;
		gem_Sell_View.Initialize_Data(sell_Data);
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00088C4C File Offset: 0x00086E4C
	public void Sell_Result(int _State, ArrayList _Result, int _Total_Sell_Money)
	{
		bool flag = _State < 1;
		if (!flag)
		{
			Dictionary<int, Vector3> dictionary = new Dictionary<int, Vector3>();
			for (int i = 0; i < _Result.Count; i++)
			{
				int key = Fun.Convert_Int(_Result[i].ToString());
				bool flag2 = this.Sell_Item_Data.ContainsKey(key);
				if (flag2)
				{
					dictionary.Add(key, this.Sell_Item_Data[key]);
				}
			}
			this.Sell_Item_Data.Clear();
			bool flag3 = _Result.Count == 1;
			if (flag3)
			{
				fUISoundManager.instance.PlayUISound("ui_fuwen_sell_small", 0, null, 20);
			}
			else
			{
				fUISoundManager.instance.PlayUISound("ui_fuwen_sell_big", 0, null, 20);
			}
			base.StopCoroutine(this.Display_Sell_Effect(dictionary, _Total_Sell_Money));
			base.StartCoroutine(this.Display_Sell_Effect(dictionary, _Total_Sell_Money));
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00088D2E File Offset: 0x00086F2E
	private IEnumerator Display_Sell_Effect(Dictionary<int, Vector3> _Sell_Gem_Data, int _Total_Sell_Money)
	{
		GameObject _Clone_Into_Package_Effect_Object = null;
		List<GameObject> _Game_Object_List = new List<GameObject>();
		int i = 0;
		Vector3 _Position = Vector3.zero;
		List<int> _Keys = null;
		_Keys = new List<int>(_Sell_Gem_Data.Keys);
		int num;
		for (i = 0; i < _Keys.Count; i = num + 1)
		{
			_Position = _Sell_Gem_Data[_Keys[i]];
			_Clone_Into_Package_Effect_Object = UnityEngine.Object.Instantiate<GameObject>(this.UI_Component.Right_Synthetic_Into_Package_Effect_Object);
			_Clone_Into_Package_Effect_Object.transform.SetParent(this.Gem_Sell_Effect_Parent_Object.transform);
			_Clone_Into_Package_Effect_Object.transform.localPosition = _Position;
			_Clone_Into_Package_Effect_Object.transform.localEulerAngles = Vector3.zero;
			_Clone_Into_Package_Effect_Object.transform.localScale = Vector3.one;
			_Clone_Into_Package_Effect_Object.SetActive(true);
			_Clone_Into_Package_Effect_Object.transform.DOMove(this.UI_Component.Right_Package_Money_Position_Object.transform.position, 1f, false);
			_Game_Object_List.Add(_Clone_Into_Package_Effect_Object);
			num = i;
		}
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(this.Gem_Sell_Effect_Parent_Object);
		this.Gem_Sell_Effect_Parent_Object = null;
		this.UI_Component.Right_Sell_Add_Money_Text_Object.SetActive(true);
		Text _Text = null;
		_Text = this.UI_Component.Right_Sell_Add_Money_Text_Object.GetComponent<Text>();
		_Text.gameObject.SetActive(true);
		_Text.text = " + " + _Total_Sell_Money.ToString();
		yield return new WaitForSeconds(1f);
		this.UI_Component.Right_Sell_Add_Money_Text_Object.SetActive(false);
		yield break;
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x00088D4C File Offset: 0x00086F4C
	private void Hidden_Sell_Effect()
	{
		bool flag = !(this.Gem_Sell_Effect_Parent_Object == null);
		if (flag)
		{
			UnityEngine.Object.Destroy(this._Gem_Sell_Effect_Parent_Object);
			this.Gem_Sell_Effect_Parent_Object = null;
		}
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00088D84 File Offset: 0x00086F84
	private List<int> Get_Sell_Gem_ID_List()
	{
		List<int> list = new List<int>();
		this.Sell_Item_Data.Clear();
		for (int i = 0; i < this.Package_Item_Select_Object_List.Count; i++)
		{
			Toggle component = ((GameObject)this.Package_Item_Select_Object_List[i]).GetComponent<Toggle>();
			bool isOn = component.isOn;
			if (isOn)
			{
				GameObject gameObject = ((GameObject)this.Package_Item_Select_Object_List[i]).transform.parent.gameObject;
				int num = Fun.Convert_Int(gameObject.transform.FindChild("lbl_ID").GetComponent<Text>().text);
				this.Sell_Item_Data.Add(num, gameObject.transform.position);
				list.Add(num);
			}
		}
		return list;
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00088E62 File Offset: 0x00087062
	private void Cancel_Select_All(GameObject _GameObject)
	{
		this.Select_Or_Cancel_All(false);
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00088E70 File Offset: 0x00087070
	private void Select_Or_Cancel_All(bool _State)
	{
		for (int i = 0; i < this.Package_Item_Select_Object_List.Count; i++)
		{
			GameObject gameObject = (GameObject)this.Package_Item_Select_Object_List[i];
			Toggle component = gameObject.GetComponent<Toggle>();
			component.isOn = _State;
		}
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00088EC4 File Offset: 0x000870C4
	private void Item_Tips_Enter(GameObject _GameObject)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(_GameObject.transform.parent.FindChild("lbl_Index").GetComponent<Text>().text);
		arrayList.Add(_GameObject);
		fUIManager.GetInstance().ShowWin("Gem_Tips_UI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.Load_Gem_Tips_UI_Complete), arrayList, true);
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00088F28 File Offset: 0x00087128
	private void Load_Gem_Tips_UI_Complete(string _UI_Name, GameObject _UI_Object, object _Parameters)
	{
		Vector2 zero = Vector2.zero;
		Gem_Tips_View gem_Tips_View = _UI_Object.GetComponent<Gem_Tips_View>();
		bool flag = gem_Tips_View == null;
		if (flag)
		{
			gem_Tips_View = _UI_Object.AddComponent<Gem_Tips_View>();
			gem_Tips_View.Parent_View_Component = this;
		}
		ArrayList arrayList = (ArrayList)_Parameters;
		int gem_Index = Fun.Convert_Int(arrayList[0].ToString());
		gem_Tips_View.Display_Data(gem_Index);
		RectTransform component = _UI_Object.GetComponent<RectTransform>();
		component.sizeDelta = gem_Tips_View.UI_Component.Background_RectTransform.sizeDelta;
		bool flag2 = RectTransformUtility.ScreenPointToLocalPointInRectangle(fUIManager.mCanObj.transform as RectTransform, Input.mousePosition, fUIManager.uiCamera, out zero);
		if (flag2)
		{
			component.anchoredPosition = zero;
		}
		fUIManager.GetInstance().SetWinActive("Gem_Tips_UI", true);
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00088FF4 File Offset: 0x000871F4
	private void Item_Tips_Exit(GameObject _GameObject)
	{
		bool flag = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
		if (flag)
		{
			Gem_Tips_UICon.Instance.Close();
		}
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00089030 File Offset: 0x00087230
	private void Item_Click(GameObject _GameObject)
	{
		bool flag = !this.Display_Sell_State;
		if (flag)
		{
			bool flag2 = this._Tab_State == 1;
			if (flag2)
			{
				this.Equip(_GameObject);
			}
			else
			{
				bool flag3 = this._Tab_State == 2;
				if (flag3)
				{
					this.Synthetic_Place(_GameObject, -1);
				}
				else
				{
					bool flag4 = this._Tab_State == 3;
					if (flag4)
					{
						this.Batch_Synthetic_Place(_GameObject);
					}
				}
			}
			bool flag5 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
			if (flag5)
			{
				Gem_Tips_UICon.Instance.Close();
			}
		}
		else
		{
			Toggle component = _GameObject.transform.parent.FindChild("btn_Select").GetComponent<Toggle>();
			component.isOn = !component.isOn;
		}
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x000890FC File Offset: 0x000872FC
	public void Equip(int _Hero_ID, int _Gem_ID, int _Prve_Equip_Location, int _Target_Equip_Location)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(_Hero_ID);
		arrayList.Add(_Gem_ID);
		arrayList.Add(_Prve_Equip_Location);
		arrayList.Add(_Target_Equip_Location);
		arrayList.Add("宝石装备");
		fGameManager.SendMsgToHallServer(3020000, new ArrayList
		{
			3020107,
			arrayList
		});
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x00089180 File Offset: 0x00087380
	public void Equip(GameObject _GameObject)
	{
		int hero_ID = this.Hero_ID;
		int gem_Index = Fun.Convert_Int(_GameObject.transform.parent.FindChild("lbl_Index").GetComponent<Text>().text);
		int gem_ID = Fun.Convert_Int(_GameObject.transform.parent.FindChild("lbl_ID").GetComponent<Text>().text);
		int prve_Equip_Location = this.Get_Equip_Location(_GameObject.transform.parent.name);
		int num = this.Get_Auto_Equip_Location(hero_ID, gem_Index);
		bool flag = num >= 0;
		if (flag)
		{
			this.Equip(hero_ID, gem_ID, prve_Equip_Location, num);
		}
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00089228 File Offset: 0x00087428
	public void Equip_Result(int _State, int _Gem_Type, int _Location)
	{
		string text = string.Empty;
		bool flag = _State < 1;
		if (flag)
		{
			bool flag2 = _State == -1;
			if (flag2)
			{
				text = Language.GetLang(101010, new object[0]);
			}
			bool flag3 = !string.IsNullOrEmpty(text);
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), text, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			fUISoundManager.instance.PlayUISound("jiemian_unclick", 0, null, 20);
		}
		else
		{
			this.Refresh_Equip();
			if (_State != 1)
			{
				if (_State == 2)
				{
					fUISoundManager.instance.PlayUISound("ui_fuwen_takeoff", 0, null, 20);
				}
			}
			else
			{
				fUISoundManager.instance.PlayUISound("ui_fuwen_setup", 0, null, 20);
			}
			base.StopCoroutine(this.Display_Equip_Effect(_Gem_Type, _Location));
			base.StartCoroutine(this.Display_Equip_Effect(_Gem_Type, _Location));
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00089325 File Offset: 0x00087525
	private IEnumerator Display_Equip_Effect(int _Gem_Type, int _Location)
	{
		GameObject _GameObject = null;
		switch (_Gem_Type)
		{
		case 1:
			switch (_Location)
			{
			case 0:
				_GameObject = this.UI_Component.Right_Attack_Location_Icon_Image_1.gameObject;
				break;
			case 1:
				_GameObject = this.UI_Component.Right_Attack_Location_Icon_Image_2.gameObject;
				break;
			case 2:
				_GameObject = this.UI_Component.Right_Attack_Location_Icon_Image_3.gameObject;
				break;
			}
			break;
		case 2:
			if (_Location != 0)
			{
				if (_Location == 1)
				{
					_GameObject = this.UI_Component.Right_Skill_Location_Icon_Image_2.gameObject;
				}
			}
			else
			{
				_GameObject = this.UI_Component.Right_Skill_Location_Icon_Image_1.gameObject;
			}
			break;
		case 3:
		{
			bool flag = _Location == 0;
			if (flag)
			{
				_GameObject = this.UI_Component.Right_Stunt_Location_Icon_Image_1.gameObject;
			}
			break;
		}
		case 4:
			if (_Location != 0)
			{
				if (_Location == 1)
				{
					_GameObject = this.UI_Component.Right_Defend_Location_Icon_Image_2.gameObject;
				}
			}
			else
			{
				_GameObject = this.UI_Component.Right_Defend_Location_Icon_Image_1.gameObject;
			}
			break;
		}
		this.UI_Component.Right_Equip_Effect_Object.transform.position = _GameObject.transform.position;
		this.UI_Component.Right_Equip_Effect_Object.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		this.UI_Component.Right_Equip_Effect_Object.SetActive(false);
		yield break;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00089344 File Offset: 0x00087544
	private void Synthetic(GameObject _GameObject)
	{
		bool synthetic_State = this.Synthetic_State;
		if (!synthetic_State)
		{
			for (int i = 0; i < this.Synthetic_Data.Count; i++)
			{
				bool flag = this.Synthetic_Data[i] == 0;
				if (flag)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				}
			}
			this.Synthetic_State = true;
			ArrayList value = new ArrayList
			{
				new ArrayList
				{
					this.Synthetic_Data[0],
					this.Synthetic_Data[1],
					this.Synthetic_Data[2],
					this.Synthetic_Data[3]
				},
				"宝石合成"
			};
			fGameManager.SendMsgToHallServer(3020000, new ArrayList
			{
				3020111,
				value
			});
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000894A0 File Offset: 0x000876A0
	public void Synthetic_Result(int _State, int _Gem_Index)
	{
		string content = null;
		this.Synthetic_State = false;
		bool flag = _State > 1;
		if (flag)
		{
			switch (_State)
			{
			case 2:
				content = Language.GetLang(101007, new object[0]);
				break;
			case 3:
				content = Language.GetLang(101008, new object[0]);
				break;
			case 4:
				content = Language.GetLang(101008, new object[0]);
				break;
			case 5:
				content = Language.GetLang(101009, new object[0]);
				break;
			}
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), content, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.Clear_Synthetic_Data();
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(_Gem_Index);
			bool flag2 = gem_Entity == null;
			if (flag2)
			{
				Debug.Log("无法找到索引为:" + _Gem_Index.ToString() + "的宝石");
			}
			else
			{
				Fun.Load_Image("Icon/fuwen_icon", this.UI_Component.Right_Synthetic_Location_Icon_Image_New, gem_Entity.Icon);
				this.UI_Component.Right_Synthetic_Location_Quality_Image_New.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				this.UI_Component.Right_Synthetic_Location_Index_Text_New.text = _Gem_Index.ToString();
				this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject.SetActive(true);
				this.UI_Component.Right_Synthetic_Location_Quality_Image_New.gameObject.SetActive(true);
				EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
				EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
				this.Display_Synthetic_Effect(_State);
				base.StopCoroutine(this.Display_Synthetic_Effect(_State));
				base.StartCoroutine(this.Display_Synthetic_Effect(_State));
				bool flag3 = _State == 1;
				if (flag3)
				{
					fUISoundManager.instance.PlayUISound("ui_fuwen_hecheng_success", 0, null, 20);
				}
				else
				{
					fUISoundManager.instance.PlayUISound("ui_fuwen_hecheng_fail", 0, null, 20);
				}
			}
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000896E0 File Offset: 0x000878E0
	private IEnumerator Display_Synthetic_Effect(int _State)
	{
		bool flag = this._Tab_State != 2;
		if (flag)
		{
			yield break;
		}
		Gem_Entity _Gem_Xml_Data = null;
		int _Gem_Index = 0;
		_Gem_Index = Fun.Convert_Int(this.UI_Component.Right_Synthetic_Location_Index_Text_New.text);
		bool flag2 = fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(_Gem_Index);
		if (flag2)
		{
			_Gem_Xml_Data = fGameManager.mloadConfig.Gem_Xml_Data[_Gem_Index];
			bool flag3 = _Gem_Xml_Data.Type != 5;
			if (flag3)
			{
				this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject.SetActive(false);
				this.UI_Component.Right_Synthetic_Location_Quality_Image_New.gameObject.SetActive(false);
			}
		}
		this.UI_Component.Right_Synthetic_Location_ID_Text_New.text = string.Empty;
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onEnter = null;
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onExit = null;
		GameObject _Clone_Into_Package_Effect_Object = null;
		_Clone_Into_Package_Effect_Object = UnityEngine.Object.Instantiate<GameObject>(this.UI_Component.Right_Synthetic_Into_Package_Effect_Object);
		_Clone_Into_Package_Effect_Object.transform.SetParent(this.UI_Component.Right_Synthetic_Into_Package_Effect_Object.transform.parent);
		_Clone_Into_Package_Effect_Object.transform.localPosition = Vector3.zero;
		_Clone_Into_Package_Effect_Object.transform.localEulerAngles = Vector3.zero;
		_Clone_Into_Package_Effect_Object.transform.localScale = Vector3.one;
		_Clone_Into_Package_Effect_Object.SetActive(true);
		_Clone_Into_Package_Effect_Object.transform.DOMove(this.UI_Component.Right_Display_Package_Toggle.transform.position, 1f, false);
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(_Clone_Into_Package_Effect_Object);
		yield break;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000896F8 File Offset: 0x000878F8
	private void Hidden_Synthetic_Effect()
	{
		base.StopCoroutine(this.Display_Synthetic_Effect(0));
		this.UI_Component.Right_Synthetic_Effect_Object.transform.FindChild("0").gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Effect_Object.transform.FindChild("1").gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Quality_Image_New.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_ID_Text_New.text = string.Empty;
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onEnter = null;
		EventTriggerListener.Get(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject).onExit = null;
		this.UI_Component.Right_Batch_Synthetic_Effect_Object.transform.FindChild("0").gameObject.SetActive(false);
		this.UI_Component.Right_Batch_Synthetic_Effect_Object.transform.FindChild("1").gameObject.SetActive(false);
		this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject.SetActive(false);
		this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image_New.gameObject.SetActive(false);
		this.UI_Component.Right_Batch_Synthetic_Location_ID_Text_New.text = string.Empty;
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onEnter = null;
		EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onExit = null;
		foreach (object obj in this.UI_Component.Right_Synthetic_Into_Package_Effect_Object.transform.parent)
		{
			Transform transform = (Transform)obj;
			bool flag = transform.gameObject.name.Contains("Into_Package_Effect(");
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		foreach (object obj2 in this.UI_Component.Right_Batch_Synthetic_Into_Package_Effect_Object.transform.parent)
		{
			Transform transform2 = (Transform)obj2;
			bool flag2 = transform2.gameObject.name.Contains("Into_Package_Effect(");
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000899A4 File Offset: 0x00087BA4
	private void Batch_Synthetic(GameObject _GameObject)
	{
		bool flag = !this.UI_Component.Right_Batch_Synthetic_Location_Amount_Object.activeSelf;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			base.StopCoroutine(this.Wait_Batch_Synthetic());
			bool batch_Synthetic_Auto_State = this.Batch_Synthetic_Auto_State;
			if (batch_Synthetic_Auto_State)
			{
				this.Batch_Synthetic_Auto_State = false;
				this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.raycastTarget = true;
				this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.raycastTarget = true;
				this.UI_Component.Right_Batch_Synthetic_Button.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(101012, new object[0]);
			}
			else
			{
				int key = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Location_Index_Text.text);
				bool flag2 = !fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(key);
				if (flag2)
				{
					return;
				}
				Gem_Entity gem_Entity = fGameManager.mloadConfig.Gem_Xml_Data[key];
				bool flag3 = gem_Entity.Quality > 1;
				if (flag3)
				{
					bool flag4 = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Success_Rate_ID_Text.text) == 0;
					if (flag4)
					{
						fUIManager.tipBlur.enabled = true;
						fUIManager.tipBlur.rawImage = this.UI_Component.Right_Batch_Synthetic_Tips_RawImage;
						this.UI_Component.Right_Batch_Synthetic_Tips_Object.SetActive(true);
						this.UI_Component.Right_Batch_Synthetic_Tips_Object.transform.localScale = Vector3.one;
						base.StartCoroutine(this.ddd());
					}
					else
					{
						int num = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text.text);
						int num2 = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text.text);
						bool flag5 = num2 < num / 4;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101015, new object[0]), Language.GetLang(101016, new object[0]), Language.GetLang(101017, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.aaa), null, new TipManager.TipCancelDelegate(this.ccc), null);
						}
						else
						{
							this.aaa(null);
						}
					}
				}
				else
				{
					this.aaa(null);
				}
			}
			bool flag6 = !this.Batch_Synthetic_State;
			if (flag6)
			{
			}
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00089C56 File Offset: 0x00087E56
	private IEnumerator ddd()
	{
		yield return new WaitForSeconds(0.01f);
		this.UI_Component.Right_Batch_Synthetic_Tips_RawImage.enabled = true;
		yield break;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00089C68 File Offset: 0x00087E68
	private void aaa(object _Object)
	{
		bool flag = Time.time >= this.Batch_Synthetic_Time;
		if (flag)
		{
			this.UI_Component.Right_Batch_Synthetic_Tips_Object.SetActive(false);
			this.Batch_Synthetic_Auto_State = true;
			this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.raycastTarget = false;
			this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.raycastTarget = false;
			this.UI_Component.Right_Batch_Synthetic_Button.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(101013, new object[0]);
			base.StartCoroutine(this.Wait_Batch_Synthetic());
		}
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00089D0F File Offset: 0x00087F0F
	private void bbb(object _Object)
	{
		this.UI_Component.Right_Batch_Synthetic_Tips_Object.SetActive(false);
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00089D24 File Offset: 0x00087F24
	private void ccc(object _Object)
	{
		this.UI_Component.Right_Batch_Synthetic_Tips_Object.SetActive(false);
		ShopManager.GetInstance().OnPenShopWin(1);
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00089D45 File Offset: 0x00087F45
	private IEnumerator Wait_Batch_Synthetic()
	{
		ArrayList _Parameters_ = null;
		int _Gem_ID = 0;
		int _Success_Rate_Gem_ID = 0;
		int _Gem_Amount = 0;
		bool flag = !this.Batch_Synthetic_Auto_State;
		if (flag)
		{
			yield break;
		}
		_Gem_ID = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Location_ID_Text.text);
		bool flag2 = this.Get_Package_Gem_Index(_Gem_ID) < 1;
		if (flag2)
		{
			yield break;
		}
		_Gem_Amount = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text.text);
		bool flag3 = _Gem_Amount < 4;
		if (flag3)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			this.Batch_Synthetic_Place_Cancel(null);
			yield break;
		}
		_Success_Rate_Gem_ID = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Success_Rate_ID_Text.text);
		_Gem_Amount = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text.text);
		bool flag4 = _Success_Rate_Gem_ID != 0 && _Gem_Amount < 1;
		if (flag4)
		{
			this.Batch_Synthetic_Place_Cancel(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.gameObject);
			yield break;
		}
		_Parameters_ = new ArrayList
		{
			new ArrayList
			{
				_Gem_ID,
				_Success_Rate_Gem_ID
			},
			"宝石批量合成"
		};
		fGameManager.SendMsgToHallServer(3020000, new ArrayList
		{
			3020115,
			_Parameters_
		});
		this.Batch_Synthetic_State = true;
		yield return new WaitForSeconds(3f);
		this.Batch_Synthetic_State = false;
		base.StartCoroutine(this.Wait_Batch_Synthetic());
		yield break;
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00089D54 File Offset: 0x00087F54
	public void Batch_Synthetic_Result(int _State, int _Gem_Index, int _Gem_Amount, int _Success_Rate_Gem_Amount)
	{
		string content = null;
		bool flag = _State > 1;
		if (flag)
		{
			switch (_State)
			{
			case 2:
				content = Language.GetLang(101007, new object[0]);
				break;
			case 3:
				content = Language.GetLang(101008, new object[0]);
				break;
			case 4:
				content = Language.GetLang(101008, new object[0]);
				break;
			case 5:
				content = Language.GetLang(101009, new object[0]);
				break;
			}
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), content, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(_Gem_Index);
			bool flag2 = gem_Entity == null;
			if (flag2)
			{
				Debug.Log("无法找到索引为:" + _Gem_Index.ToString() + "的宝石");
			}
			else
			{
				this.Batch_Synthetic_Time = Time.time + this.Batch_Synthetic_Delay_Time;
				bool flag3 = _Gem_Amount - 4 < 0;
				if (flag3)
				{
					this.Batch_Synthetic_Place_Cancel(null);
				}
				this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text.text = _Gem_Amount.ToString();
				bool flag4 = Fun.Convert_Int(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Index_Text.text) != 0 && _Success_Rate_Gem_Amount < 1;
				if (flag4)
				{
					this.Batch_Synthetic_Place_Cancel(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.gameObject);
				}
				else
				{
					this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text.text = _Success_Rate_Gem_Amount.ToString();
				}
				Fun.Load_Image("Icon/fuwen_icon", this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New, gem_Entity.Icon);
				this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image_New.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				this.UI_Component.Right_Batch_Synthetic_Location_Index_Text_New.text = _Gem_Index.ToString();
				this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject.SetActive(true);
				this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image_New.gameObject.SetActive(true);
				EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
				EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
				bool flag5 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
				if (flag5)
				{
					Gem_Tips_UICon.Instance.Close();
				}
				base.StopCoroutine(this.Display_Batch_Synthetic_Effect(_State));
				base.StartCoroutine(this.Display_Batch_Synthetic_Effect(_State));
				bool flag6 = _State == 1;
				if (flag6)
				{
					fUISoundManager.instance.PlayUISound("ui_fuwen_hecheng_success", 0, null, 20);
				}
				else
				{
					fUISoundManager.instance.PlayUISound("ui_fuwen_hecheng_fail", 0, null, 20);
				}
			}
		}
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0008A045 File Offset: 0x00088245
	private IEnumerator Display_Batch_Synthetic_Effect(int _State)
	{
		GameObject _Effect_Object = null;
		bool flag = this._Tab_State == 3;
		if (flag)
		{
			_Effect_Object = this.UI_Component.Right_Batch_Synthetic_Effect_Object.transform.FindChild(_State.ToString()).gameObject;
			_Effect_Object.SetActive(true);
			yield return new WaitForSeconds(2f);
			_Effect_Object.SetActive(false);
			this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject.SetActive(false);
			this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image_New.gameObject.SetActive(false);
			this.UI_Component.Right_Batch_Synthetic_Location_Index_Text_New.text = string.Empty;
			this.UI_Component.Right_Batch_Synthetic_Location_ID_Text_New.text = string.Empty;
			EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onEnter = null;
			EventTriggerListener.Get(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image_New.gameObject).onExit = null;
			GameObject _Clone_Into_Package_Effect_Object = null;
			_Clone_Into_Package_Effect_Object = UnityEngine.Object.Instantiate<GameObject>(this.UI_Component.Right_Batch_Synthetic_Into_Package_Effect_Object);
			_Clone_Into_Package_Effect_Object.transform.SetParent(this.UI_Component.Right_Batch_Synthetic_Into_Package_Effect_Object.transform.parent);
			_Clone_Into_Package_Effect_Object.transform.localPosition = Vector3.zero;
			_Clone_Into_Package_Effect_Object.transform.localEulerAngles = Vector3.zero;
			_Clone_Into_Package_Effect_Object.transform.localScale = Vector3.one;
			_Clone_Into_Package_Effect_Object.SetActive(true);
			_Clone_Into_Package_Effect_Object.transform.DOMove(this.UI_Component.Right_Display_Package_Toggle.transform.position, 1f, false);
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.Destroy(_Clone_Into_Package_Effect_Object);
			_Clone_Into_Package_Effect_Object = null;
		}
		yield break;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0008A05C File Offset: 0x0008825C
	public int Get_Package_Gem_Index(int _Gem_ID)
	{
		bool flag = !Gem_Data.Instance.Gem_Package_Data.ContainsKey(_Gem_ID);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			List<int> list = this.Get_Package_Gem_Data(_Gem_ID);
			result = list[0];
		}
		return result;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0008A09C File Offset: 0x0008829C
	public List<int> Get_Package_Gem_Data(int _Gem_ID)
	{
		List<int> list = new List<int>
		{
			0,
			0,
			0
		};
		bool flag = !Gem_Data.Instance.Gem_Package_Data.ContainsKey(_Gem_ID);
		List<int> result;
		if (flag)
		{
			result = list;
		}
		else
		{
			result = Gem_Data.Instance.Gem_Package_Data[_Gem_ID];
		}
		return result;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0008A0FC File Offset: 0x000882FC
	public Gem_Entity Get_Gem_Xml_Data(int _Gem_Index)
	{
		bool flag = !fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(_Gem_Index);
		Gem_Entity result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = fGameManager.mloadConfig.Gem_Xml_Data[_Gem_Index];
		}
		return result;
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0008A13C File Offset: 0x0008833C
	private int Get_Auto_Equip_Location(int _Hero_ID, int _Gem_Index)
	{
		int num = -1;
		ArrayList arrayList = null;
		Dictionary<int, ArrayList> gem_Equip_Data = Gem_Data.Instance.Gem_Equip_Data;
		Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(_Gem_Index);
		bool flag = gem_Entity == null;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = gem_Entity.Type == 5;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				result = -1;
			}
			else
			{
				bool flag3 = !gem_Equip_Data.ContainsKey(_Hero_ID);
				if (flag3)
				{
					result = 0;
				}
				else
				{
					ArrayList arrayList2 = gem_Equip_Data[_Hero_ID];
					switch (gem_Entity.Type)
					{
					case 1:
						arrayList = (ArrayList)arrayList2[0];
						break;
					case 2:
						arrayList = (ArrayList)arrayList2[1];
						break;
					case 3:
						arrayList = (ArrayList)arrayList2[2];
						break;
					case 4:
						arrayList = (ArrayList)arrayList2[3];
						break;
					}
					for (int i = 0; i < arrayList.Count; i++)
					{
						bool flag4 = gem_Entity.Type == 3 && i > 0;
						if (flag4)
						{
							return num;
						}
						bool flag5 = Fun.Convert_Int(arrayList[i].ToString()) == 0;
						if (flag5)
						{
							return i;
						}
					}
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0008A2CC File Offset: 0x000884CC
	public void Unequip(GameObject _GameObject)
	{
		int hero_ID = this.Hero_ID;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(hero_ID);
		int key = Fun.Convert_Int(_GameObject.transform.parent.FindChild("lbl_Index").GetComponent<Text>().text);
		bool flag = fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(key);
		if (flag)
		{
			int type = fGameManager.mloadConfig.Gem_Xml_Data[key].Type;
			int num = this.Get_Equip_Location(_GameObject.transform.parent.name);
			arrayList.Add(type);
			arrayList.Add(num);
			arrayList.Add("宝石卸下");
			fGameManager.SendMsgToHallServer(3020000, new ArrayList
			{
				3020109,
				arrayList
			});
			bool flag2 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
			if (flag2)
			{
				Gem_Tips_UICon.Instance.Close();
			}
		}
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0008A3F4 File Offset: 0x000885F4
	public int Get_Equip_Location(string _Control_Name)
	{
		int num = -1;
		if (!true)
		{
		}
		uint num2 = PrivateImplementationDetails.ComputeStringHash(_Control_Name);
		int result;
		if (num2 <= 3170599143U)
		{
			if (num2 <= 696874018U)
			{
				if (num2 != 680096399U)
				{
					if (num2 == 696874018U)
					{
						if (_Control_Name == "img_Equip_Defend_Location_1")
						{
							result = 0;
							goto IL_12A;
						}
					}
				}
				else if (_Control_Name == "img_Equip_Defend_Location_2")
				{
					result = 1;
					goto IL_12A;
				}
			}
			else if (num2 != 3153821524U)
			{
				if (num2 == 3170599143U)
				{
					if (_Control_Name == "img_Equip_Attack_Location_2")
					{
						result = 1;
						goto IL_12A;
					}
				}
			}
			else if (_Control_Name == "img_Equip_Attack_Location_3")
			{
				result = 2;
				goto IL_12A;
			}
		}
		else if (num2 <= 3324306818U)
		{
			if (num2 != 3187376762U)
			{
				if (num2 == 3324306818U)
				{
					if (_Control_Name == "img_Equip_Stunt_Location_1")
					{
						result = 0;
						goto IL_12A;
					}
				}
			}
			else if (_Control_Name == "img_Equip_Attack_Location_1")
			{
				result = 0;
				goto IL_12A;
			}
		}
		else if (num2 != 3395182820U)
		{
			if (num2 == 3445515677U)
			{
				if (_Control_Name == "img_Equip_Skill_Location_1")
				{
					result = 0;
					goto IL_12A;
				}
			}
		}
		else if (_Control_Name == "img_Equip_Skill_Location_2")
		{
			result = 1;
			goto IL_12A;
		}
		result = num;
		IL_12A:
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0008A534 File Offset: 0x00088734
	public void Synthetic_Place(GameObject _GameObject, int _Location = -1)
	{
		int num = Fun.Convert_Int(_GameObject.transform.parent.FindChild("lbl_ID").GetComponent<Text>().text);
		bool flag = !Gem_Data.Instance.Gem_Package_Data.ContainsKey(num);
		if (!flag)
		{
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(Gem_Data.Instance.Gem_Package_Data[num][0]);
			bool flag2 = gem_Entity == null;
			if (!flag2)
			{
				bool flag3 = gem_Entity.Type == 5;
				if (flag3)
				{
					_Location = 0;
					ArrayList arrayList = new ArrayList();
					arrayList.Add(1);
					arrayList.Add(num);
					arrayList.Add("宝石附加成功率放置");
					fGameManager.SendMsgToHallServer(3020000, new ArrayList
					{
						3020118,
						arrayList
					});
				}
				else
				{
					int quality = gem_Entity.Quality;
					bool flag4 = quality > 5;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101011, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						bool flag5 = _Location < 0;
						if (flag5)
						{
							for (int i = 0; i < this.Synthetic_Data.Count; i++)
							{
								bool flag6 = this.Synthetic_Data[i] == 0;
								if (flag6)
								{
									_Location = i;
									break;
								}
							}
						}
						bool flag7 = _Location < 0;
						if (!flag7)
						{
							for (int i = 0; i < this.Synthetic_Data.Count; i++)
							{
								gem_Entity = this.Get_Gem_Xml_Data(this.Synthetic_Data[i]);
								bool flag8 = gem_Entity != null && quality != gem_Entity.Quality;
								if (flag8)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
									return;
								}
							}
							ArrayList arrayList = new ArrayList();
							arrayList.Add(1);
							arrayList.Add(num);
							arrayList.Add(_Location);
							arrayList.Add("宝石合成放置");
							fGameManager.SendMsgToHallServer(3020000, new ArrayList
							{
								3020113,
								arrayList
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x0008A7F8 File Offset: 0x000889F8
	public void Synthetic_Cencel(GameObject _GameObject)
	{
		string name = _GameObject.transform.parent.name;
		string a = name;
		ArrayList arrayList;
		int num;
		if (!(a == "img_Synthetic_Location_1"))
		{
			if (!(a == "img_Synthetic_Location_2"))
			{
				if (!(a == "img_Synthetic_Location_3"))
				{
					if (!(a == "img_Synthetic_Location_4"))
					{
						if (!(a == "img_Synthetic_Location_New"))
						{
							return;
						}
						arrayList = new ArrayList();
						arrayList.Add(0);
						arrayList.Add(0);
						arrayList.Add("宝石附加成功率放置取消");
						fGameManager.SendMsgToHallServer(3020000, new ArrayList
						{
							3020118,
							arrayList
						});
						return;
					}
					else
					{
						num = 3;
					}
				}
				else
				{
					num = 2;
				}
			}
			else
			{
				num = 1;
			}
		}
		else
		{
			num = 0;
		}
		arrayList = new ArrayList();
		arrayList.Add(0);
		arrayList.Add(0);
		arrayList.Add(num);
		arrayList.Add("宝石合成放置取消");
		fGameManager.SendMsgToHallServer(3020000, new ArrayList
		{
			3020113,
			arrayList
		});
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0008A938 File Offset: 0x00088B38
	public void Batch_Synthetic_Place(GameObject _GameObject)
	{
		int num = Fun.Convert_Int(_GameObject.transform.parent.FindChild("lbl_ID").GetComponent<Text>().text);
		bool flag = !Gem_Data.Instance.Gem_Package_Data.ContainsKey(num);
		if (!flag)
		{
			List<int> list = this.Get_Package_Gem_Data(num);
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(list[0]);
			bool flag2 = gem_Entity == null;
			if (!flag2)
			{
				int num2 = list[0];
				int num3 = list[2];
				bool flag3 = gem_Entity.Type == 5;
				Image image;
				Image image2;
				Text text;
				Text text2;
				GameObject gameObject;
				Text text3;
				if (flag3)
				{
					image = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image;
					image2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Quality_Image;
					text = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Index_Text;
					text2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_ID_Text;
					gameObject = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Object;
					text3 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text;
					bool flag4 = text2.text != num.ToString() && this.Batch_Synthetic_Auto_State;
					if (flag4)
					{
						this.Batch_Synthetic_Place_Cancel(this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.gameObject);
					}
				}
				else
				{
					bool flag5 = num3 < 4;
					if (flag5)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						return;
					}
					bool flag6 = gem_Entity.Quality > 5;
					if (flag6)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(101006, new object[0]), Language.GetLang(101011, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						return;
					}
					image = this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image;
					image2 = this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image;
					text = this.UI_Component.Right_Batch_Synthetic_Location_Index_Text;
					text2 = this.UI_Component.Right_Batch_Synthetic_Location_ID_Text;
					gameObject = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Object;
					text3 = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text;
					bool flag7 = text2.text != num.ToString() && this.Batch_Synthetic_Auto_State;
					if (flag7)
					{
						this.Batch_Synthetic_Place_Cancel(this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.gameObject);
					}
				}
				text.text = num2.ToString();
				text2.text = num.ToString();
				gameObject.SetActive(true);
				text3.text = num3.ToString();
				Fun.Load_Image("Icon/fuwen_icon", image, gem_Entity.Icon);
				image2.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				text.text = gem_Entity.Index.ToString();
				image.gameObject.SetActive(true);
				image2.gameObject.SetActive(true);
				EventTriggerListener.Get(image.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
				EventTriggerListener.Get(image.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
				this.UI_Component.Right_Batch_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
				{
					this.Get_Batch_Synthetic_Success_Rate()
				});
			}
		}
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x0008ACD4 File Offset: 0x00088ED4
	public void Batch_Synthetic_Place_Cancel(GameObject _GameObject)
	{
		this.Batch_Synthetic_Auto_State = false;
		this.UI_Component.Right_Batch_Synthetic_Button.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(101012, new object[0]);
		bool flag = _GameObject != null;
		if (flag)
		{
			bool flag2 = _GameObject.transform.parent.name == "img_Synthetic_Location";
			Image image;
			Image image2;
			Text text;
			Text text2;
			GameObject gameObject;
			if (flag2)
			{
				image = this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image;
				image2 = this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image;
				text = this.UI_Component.Right_Batch_Synthetic_Location_Index_Text;
				text2 = this.UI_Component.Right_Batch_Synthetic_Location_ID_Text;
				gameObject = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Object;
				Text text3 = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text;
				this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.raycastTarget = true;
			}
			else
			{
				image = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image;
				image2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Quality_Image;
				text = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Index_Text;
				text2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_ID_Text;
				gameObject = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Object;
				Text text3 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text;
				this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.raycastTarget = true;
			}
			text.text = string.Empty;
			text2.text = "0";
			gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			image2.gameObject.SetActive(false);
			EventTriggerListener.Get(image.gameObject).onEnter = null;
			EventTriggerListener.Get(image.gameObject).onExit = null;
			bool flag3 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
			if (flag3)
			{
				Gem_Tips_UICon.Instance.Close();
			}
		}
		else
		{
			Image image = this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image;
			Image image2 = this.UI_Component.Right_Batch_Synthetic_Location_Quality_Image;
			Text text = this.UI_Component.Right_Batch_Synthetic_Location_Index_Text;
			Text text2 = this.UI_Component.Right_Batch_Synthetic_Location_ID_Text;
			GameObject gameObject = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Object;
			Text text3 = this.UI_Component.Right_Batch_Synthetic_Location_Amount_Text;
			this.UI_Component.Right_Batch_Synthetic_Location_Icon_Image.raycastTarget = true;
			text.text = string.Empty;
			text2.text = "0";
			gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			image2.gameObject.SetActive(false);
			EventTriggerListener.Get(image.gameObject).onEnter = null;
			EventTriggerListener.Get(image.gameObject).onExit = null;
			bool flag4 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
			if (flag4)
			{
				Gem_Tips_UICon.Instance.Close();
			}
			image = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image;
			image2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Quality_Image;
			text = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Index_Text;
			text2 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_ID_Text;
			gameObject = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Object;
			text3 = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Amount_Text;
			this.UI_Component.Right_Batch_Synthetic_Success_Rate_Icon_Image.raycastTarget = true;
			text.text = string.Empty;
			text2.text = "0";
			gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			image2.gameObject.SetActive(false);
			EventTriggerListener.Get(image.gameObject).onEnter = null;
			EventTriggerListener.Get(image.gameObject).onExit = null;
			bool flag5 = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
			if (flag5)
			{
				Gem_Tips_UICon.Instance.Close();
			}
		}
		this.UI_Component.Right_Batch_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
		{
			this.Get_Batch_Synthetic_Success_Rate()
		});
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x0008B0AC File Offset: 0x000892AC
	public void Refresh_Synthetic(ArrayList _Synthetic_Place_Data)
	{
		Image image = null;
		Image image2 = null;
		Text text = null;
		this.Synthetic_Data = new List<int>
		{
			Fun.Convert_Int(_Synthetic_Place_Data[0].ToString()),
			Fun.Convert_Int(_Synthetic_Place_Data[1].ToString()),
			Fun.Convert_Int(_Synthetic_Place_Data[2].ToString()),
			Fun.Convert_Int(_Synthetic_Place_Data[3].ToString())
		};
		bool flag = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
		if (flag)
		{
			Gem_Tips_UICon.Instance.Close();
		}
		for (int i = 0; i < _Synthetic_Place_Data.Count; i++)
		{
			int num = Fun.Convert_Int(_Synthetic_Place_Data[i].ToString());
			switch (i)
			{
			case 0:
			{
				image = this.UI_Component.Right_Synthetic_Location_Icon_Image_1;
				image2 = this.UI_Component.Right_Synthetic_Location_Quality_Image_1;
				text = this.UI_Component.Right_Synthetic_Location_Index_Text_1;
				Text text2 = this.UI_Component.Right_Synthetic_Location_ID_Text_1;
				break;
			}
			case 1:
			{
				image = this.UI_Component.Right_Synthetic_Location_Icon_Image_2;
				image2 = this.UI_Component.Right_Synthetic_Location_Quality_Image_2;
				text = this.UI_Component.Right_Synthetic_Location_Index_Text_2;
				Text text2 = this.UI_Component.Right_Synthetic_Location_ID_Text_2;
				break;
			}
			case 2:
			{
				image = this.UI_Component.Right_Synthetic_Location_Icon_Image_3;
				image2 = this.UI_Component.Right_Synthetic_Location_Quality_Image_3;
				text = this.UI_Component.Right_Synthetic_Location_Index_Text_3;
				Text text2 = this.UI_Component.Right_Synthetic_Location_ID_Text_3;
				break;
			}
			case 3:
			{
				image = this.UI_Component.Right_Synthetic_Location_Icon_Image_4;
				image2 = this.UI_Component.Right_Synthetic_Location_Quality_Image_4;
				text = this.UI_Component.Right_Synthetic_Location_Index_Text_4;
				Text text2 = this.UI_Component.Right_Synthetic_Location_ID_Text_4;
				break;
			}
			}
			bool flag2 = num != 0;
			if (flag2)
			{
				Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(num);
				Fun.Load_Image("Icon/fuwen_icon", image, gem_Entity.Icon);
				image2.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				text.text = num.ToString();
				image.gameObject.SetActive(true);
				image2.gameObject.SetActive(true);
				EventTriggerListener.Get(image.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
				EventTriggerListener.Get(image.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
			}
			else
			{
				image.gameObject.SetActive(false);
				image2.gameObject.SetActive(false);
				EventTriggerListener.Get(image.gameObject).onEnter = null;
				EventTriggerListener.Get(image.gameObject).onExit = null;
			}
		}
		this.UI_Component.Right_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
		{
			this.Get_Synthetic_Success_Rate()
		});
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0008B3B4 File Offset: 0x000895B4
	public void Refresh_Synthetic_Success_Rate(ArrayList _Synthetic_Place_Success_Rate_Data)
	{
		int num = Fun.Convert_Int(_Synthetic_Place_Success_Rate_Data[0].ToString());
		bool flag = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
		if (flag)
		{
			Gem_Tips_UICon.Instance.Close();
		}
		Image right_Synthetic_Location_Icon_Image_New = this.UI_Component.Right_Synthetic_Location_Icon_Image_New;
		Image right_Synthetic_Location_Quality_Image_New = this.UI_Component.Right_Synthetic_Location_Quality_Image_New;
		Text right_Synthetic_Location_Index_Text_New = this.UI_Component.Right_Synthetic_Location_Index_Text_New;
		Text right_Synthetic_Location_ID_Text_New = this.UI_Component.Right_Synthetic_Location_ID_Text_New;
		bool flag2 = num != 0;
		if (flag2)
		{
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(num);
			Fun.Load_Image("Icon/fuwen_icon", right_Synthetic_Location_Icon_Image_New, gem_Entity.Icon);
			right_Synthetic_Location_Quality_Image_New.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
			right_Synthetic_Location_Index_Text_New.text = num.ToString();
			right_Synthetic_Location_Icon_Image_New.gameObject.SetActive(true);
			right_Synthetic_Location_Quality_Image_New.gameObject.SetActive(true);
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.Item_Tips_Enter);
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.Item_Tips_Exit);
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Synthetic_Cencel);
		}
		else
		{
			right_Synthetic_Location_Index_Text_New.text = "0";
			right_Synthetic_Location_Icon_Image_New.gameObject.SetActive(false);
			right_Synthetic_Location_Quality_Image_New.gameObject.SetActive(false);
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onEnter = null;
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onExit = null;
			EventTriggerListener.Get(right_Synthetic_Location_Icon_Image_New.gameObject).onClick = null;
		}
		this.UI_Component.Right_Synthetic_Success_Rate_Describe_Text.text = Language.GetLang(106038, new object[]
		{
			this.Get_Synthetic_Success_Rate()
		});
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0008B594 File Offset: 0x00089794
	private int Get_Synthetic_Success_Rate()
	{
		int num = 0;
		Text right_Synthetic_Location_Index_Text_New = this.UI_Component.Right_Synthetic_Location_Index_Text_New;
		int gem_Index = Fun.Convert_Int(right_Synthetic_Location_Index_Text_New.text);
		Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
		bool flag = gem_Entity != null && gem_Entity.Type == 5;
		if (flag)
		{
			int sub_Type = gem_Entity.Sub_Type;
			int num2 = sub_Type;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					num += 50;
				}
			}
			else
			{
				num += 25;
			}
		}
		for (int i = 0; i < this.Synthetic_Data.Count; i++)
		{
			gem_Index = this.Synthetic_Data[i];
			gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
			bool flag2 = gem_Entity == null;
			if (flag2)
			{
				return num;
			}
		}
		int quality = gem_Entity.Quality;
		if (!true)
		{
		}
		int result;
		switch (quality)
		{
		case 1:
			result = num + 100;
			break;
		case 2:
			result = num + 75;
			break;
		case 3:
			result = num + 50;
			break;
		case 4:
			result = num + 25;
			break;
		case 5:
			result = num + 25;
			break;
		default:
			result = num;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x0008B6B8 File Offset: 0x000898B8
	private int Get_Batch_Synthetic_Success_Rate()
	{
		int num = 0;
		Text text = this.UI_Component.Right_Batch_Synthetic_Success_Rate_Index_Text;
		int gem_Index = Fun.Convert_Int(text.text);
		Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
		bool flag = gem_Entity != null && gem_Entity.Type == 5;
		if (flag)
		{
			int sub_Type = gem_Entity.Sub_Type;
			int num2 = sub_Type;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					num += 50;
				}
			}
			else
			{
				num += 25;
			}
		}
		text = this.UI_Component.Right_Batch_Synthetic_Location_Index_Text;
		gem_Index = Fun.Convert_Int(text.text);
		gem_Entity = this.Get_Gem_Xml_Data(gem_Index);
		bool flag2 = gem_Entity != null;
		if (flag2)
		{
			switch (gem_Entity.Quality)
			{
			case 1:
				num += 100;
				break;
			case 2:
				num += 75;
				break;
			case 3:
				num += 50;
				break;
			case 4:
				num += 25;
				break;
			case 5:
				num += 25;
				break;
			default:
				return num;
			}
		}
		return num;
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0008B7B4 File Offset: 0x000899B4
	public void Clear_Synthetic_Data()
	{
		this.UI_Component.Right_Synthetic_Location_Icon_Image_1.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Quality_Image_1.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_ID_Text_1.text = "0";
		this.UI_Component.Right_Synthetic_Location_Icon_Image_2.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Quality_Image_2.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_ID_Text_2.text = "0";
		this.UI_Component.Right_Synthetic_Location_Icon_Image_3.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Quality_Image_3.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_ID_Text_3.text = "0";
		this.UI_Component.Right_Synthetic_Location_Icon_Image_4.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_Quality_Image_4.gameObject.SetActive(false);
		this.UI_Component.Right_Synthetic_Location_ID_Text_4.text = "0";
		this.Synthetic_Data = new List<int>
		{
			0,
			0,
			0,
			0
		};
		this.Synthetic_Cencel(this.UI_Component.Right_Synthetic_Location_Icon_Image_1.gameObject);
		this.Synthetic_Cencel(this.UI_Component.Right_Synthetic_Location_Icon_Image_2.gameObject);
		this.Synthetic_Cencel(this.UI_Component.Right_Synthetic_Location_Icon_Image_3.gameObject);
		this.Synthetic_Cencel(this.UI_Component.Right_Synthetic_Location_Icon_Image_4.gameObject);
		this.Synthetic_Cencel(this.UI_Component.Right_Synthetic_Location_Icon_Image_New.gameObject);
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0008B974 File Offset: 0x00089B74
	private void Display_Effect_Describe()
	{
		ArrayList arrayList = null;
		foreach (object obj in this.UI_Component.Left_Grid.transform)
		{
			Transform transform = (Transform)obj;
			UnityEngine.Object.Destroy(transform.gameObject);
		}
		Dictionary<int, ArrayList> gem_Equip_Data = Gem_Data.Instance.Gem_Equip_Data;
		bool flag = gem_Equip_Data.ContainsKey(this.Hero_ID);
		if (flag)
		{
			arrayList = gem_Equip_Data[this.Hero_ID];
		}
		bool flag2 = arrayList != null;
		if (flag2)
		{
			this.Dipslay_Attribute_Describe((ArrayList)arrayList[3]);
			this.Dipslay_Attribute_Describe((ArrayList)arrayList[0]);
			this.Dipslay_Attribute_Describe((ArrayList)arrayList[1]);
			this.Dipslay_Attribute_Describe((ArrayList)arrayList[2]);
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0008BA6C File Offset: 0x00089C6C
	private void Dipslay_Attribute_Describe(ArrayList _Equip_Data)
	{
		List<Dictionary<int, List<int>>> list = new List<Dictionary<int, List<int>>>();
		List<Dictionary<int, Dictionary<int, List<int>>>> list2 = new List<Dictionary<int, Dictionary<int, List<int>>>>();
		List<Dictionary<int, Dictionary<int, List<int>>>> list3 = new List<Dictionary<int, Dictionary<int, List<int>>>>();
		List<Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>> list4 = new List<Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>>();
		List<Dictionary<int, Dictionary<int, List<int>>>> list5 = new List<Dictionary<int, Dictionary<int, List<int>>>>();
		List<Dictionary<int, Dictionary<int, List<int>>>> list6 = new List<Dictionary<int, Dictionary<int, List<int>>>>();
		List<Dictionary<int, List<int>>> list7 = new List<Dictionary<int, List<int>>>();
		List<Dictionary<int, Dictionary<int, List<int>>>> list8 = new List<Dictionary<int, Dictionary<int, List<int>>>>();
		List<Gem_Entity> list9 = new List<Gem_Entity>();
		for (int i = 0; i < _Equip_Data.Count; i++)
		{
			Gem_Entity gem_Entity = this.Get_Gem_Xml_Data(Fun.Convert_Int(_Equip_Data[i].ToString()));
			bool flag = gem_Entity != null;
			if (flag)
			{
				list9.Add(gem_Entity);
			}
		}
		for (int i = 0; i < list9.Count; i++)
		{
			Gem_Entity gem_Entity = list9[i];
			list.Add(gem_Entity.General_Attribute);
			list2.Add(gem_Entity.Skill_Attribute);
			list3.Add(gem_Entity.Stunt_Attribute);
			list4.Add(gem_Entity.Weapon_Attribute);
			list5.Add(gem_Entity.Buff_Attribute);
			list6.Add(gem_Entity.Npc_Attribute);
			list7.Add(gem_Entity.Special_Attribute);
			list8.Add(gem_Entity.Addition_Buff_Attribute);
		}
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list, "0");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list2, "1");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list4, "3");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list5, "4");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list6, "5");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list7, "6");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list8, "7");
		this.Get_Attribute_Describe(this.UI_Component.Left_Grid.transform, this.UI_Component.Left_Item_Object, list3, "2");
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0008BCDC File Offset: 0x00089EDC
	private Dictionary<int, List<int>> Get_Attribute_Total(List<Dictionary<int, List<int>>> _Data)
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		for (int i = 0; i < _Data.Count; i++)
		{
			Dictionary<int, List<int>> dictionary2 = _Data[i];
			List<int> list = new List<int>(dictionary2.Keys);
			for (int j = 0; j < list.Count; j++)
			{
				bool flag = !dictionary.ContainsKey(list[j]);
				if (flag)
				{
					dictionary.Add(list[j], new List<int>
					{
						0,
						dictionary2[list[j]][1]
					});
				}
				List<int> list2 = dictionary[list[j]];
				list2[0] = list2[0] + dictionary2[list[j]][0];
			}
		}
		return dictionary;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x0008BDDC File Offset: 0x00089FDC
	private Dictionary<int, Dictionary<int, List<int>>> Get_Attribute_Total(List<Dictionary<int, Dictionary<int, List<int>>>> _Data)
	{
		Dictionary<int, Dictionary<int, List<int>>> dictionary = new Dictionary<int, Dictionary<int, List<int>>>();
		for (int i = 0; i < _Data.Count; i++)
		{
			Dictionary<int, Dictionary<int, List<int>>> dictionary2 = _Data[i];
			List<int> list = new List<int>(dictionary2.Keys);
			for (int j = 0; j < list.Count; j++)
			{
				Dictionary<int, List<int>> dictionary3 = dictionary2[list[j]];
				bool flag = !dictionary.ContainsKey(list[j]);
				if (flag)
				{
					dictionary.Add(list[j], new Dictionary<int, List<int>>());
				}
				List<int> list2 = new List<int>(dictionary3.Keys);
				for (int k = 0; k < list2.Count; k++)
				{
					bool flag2 = !dictionary[list[j]].ContainsKey(list2[k]);
					if (flag2)
					{
						dictionary[list[j]].Add(list2[k], new List<int>
						{
							0,
							dictionary2[list[j]][list2[k]][1]
						});
					}
					List<int> list3 = dictionary[list[j]][list2[k]];
					list3[0] = list3[0] + dictionary2[list[j]][list2[k]][0];
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0008BF94 File Offset: 0x0008A194
	private Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> Get_Attribute_Total(List<Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>> _Data)
	{
		Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> dictionary = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>();
		for (int i = 0; i < _Data.Count; i++)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> dictionary2 = _Data[i];
			List<int> list = new List<int>(dictionary2.Keys);
			for (int j = 0; j < list.Count; j++)
			{
				bool flag = !dictionary.ContainsKey(list[j]);
				if (flag)
				{
					dictionary.Add(list[j], new Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>());
				}
				List<int> list2 = new List<int>(dictionary2[list[j]].Keys);
				for (int k = 0; k < list2.Count; k++)
				{
					bool flag2 = !dictionary[list[j]].ContainsKey(list2[k]);
					if (flag2)
					{
						dictionary[list[j]].Add(list2[k], new Dictionary<int, Dictionary<int, List<int>>>());
					}
					List<int> list3 = new List<int>(dictionary2[list[j]][list2[k]].Keys);
					for (int l = 0; l < list3.Count; l++)
					{
						bool flag3 = !dictionary[list[j]][list2[k]].ContainsKey(list3[l]);
						if (flag3)
						{
							dictionary[list[j]][list2[k]].Add(list3[l], new Dictionary<int, List<int>>());
						}
						Dictionary<int, List<int>> dictionary3 = dictionary2[list[j]][list2[k]][list3[l]];
						List<int> list4 = new List<int>(dictionary2[list[j]][list2[k]][list3[l]].Keys);
						for (int m = 0; m < list4.Count; m++)
						{
							bool flag4 = !dictionary[list[j]][list2[k]][list3[l]].ContainsKey(list4[m]);
							if (flag4)
							{
								dictionary[list[j]][list2[k]][list3[l]].Add(list4[m], new List<int>
								{
									0,
									dictionary2[list[j]][list2[k]][list3[l]][list4[m]][1]
								});
							}
							List<int> list5 = dictionary[list[j]][list2[k]][list3[l]][list4[m]];
							list5[0] = list5[0] + dictionary2[list[j]][list2[k]][list3[l]][list4[m]][0];
						}
					}
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x0008C35C File Offset: 0x0008A55C
	private void Get_Attribute_Describe(Transform _Parent_Transform, GameObject _Instantiate_Object, List<Dictionary<int, List<int>>> _Data, string _Subjoin)
	{
		string text = string.Empty;
		Dictionary<int, List<int>> dictionary = this.Get_Attribute_Total(_Data);
		List<int> list = new List<int>(dictionary.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			bool flag = dictionary[list[i]][1] != 0;
			if (flag)
			{
				text = Language.GetLang(dictionary[list[i]][1], new object[]
				{
					((float)dictionary[list[i]][0] * 1f / 100f).ToString()
				});
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(_Instantiate_Object);
				gameObject.transform.SetParent(_Parent_Transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.FindChild("lbl_Value").gameObject.SetActive(false);
				gameObject.transform.FindChild("lbl_Describe").GetComponent<Text>().text = text.Replace("\\n", "\n");
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0008C4B0 File Offset: 0x0008A6B0
	private void Get_Attribute_Describe(Transform _Parent_Transform, GameObject _Instantiate_Object, List<Dictionary<int, Dictionary<int, List<int>>>> _Data, string _Subjoin)
	{
		string text = string.Empty;
		Dictionary<int, Dictionary<int, List<int>>> dictionary = this.Get_Attribute_Total(_Data);
		List<int> list = new List<int>(dictionary.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			List<int> list2 = new List<int>(dictionary[list[i]].Keys);
			for (int j = 0; j < list2.Count; j++)
			{
				bool flag = dictionary[list[i]][list2[j]][1] != 0;
				if (flag)
				{
					text = Language.GetLang(dictionary[list[i]][list2[j]][1], new object[]
					{
						((float)dictionary[list[i]][list2[j]][0] * 1f / 100f).ToString()
					});
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(_Instantiate_Object);
					gameObject.transform.SetParent(_Parent_Transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localEulerAngles = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.FindChild("lbl_Value").gameObject.SetActive(false);
					gameObject.transform.FindChild("lbl_Describe").GetComponent<Text>().text = text.Replace("\\n", "\n");
					gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x0008C674 File Offset: 0x0008A874
	private void Get_Attribute_Describe(Transform _Parent_Transform, GameObject _Instantiate_Object, List<Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>> _Data, string _Subjoin)
	{
		string text = string.Empty;
		Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> dictionary = this.Get_Attribute_Total(_Data);
		List<int> list = new List<int>(dictionary.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			List<int> list2 = new List<int>(dictionary[list[i]].Keys);
			for (int j = 0; j < list2.Count; j++)
			{
				List<int> list3 = new List<int>(dictionary[list[i]][list2[j]].Keys);
				for (int k = 0; k < list3.Count; k++)
				{
					List<int> list4 = new List<int>(dictionary[list[i]][list2[j]][list3[k]].Keys);
					for (int l = 0; l < list4.Count; l++)
					{
						bool flag = dictionary[list[i]][list2[j]][list3[k]][list4[l]][1] != 0;
						if (flag)
						{
							text = Language.GetLang(dictionary[list[i]][list2[j]][list3[k]][list4[l]][1], new object[]
							{
								((float)dictionary[list[i]][list2[j]][list3[k]][list4[l]][0] * 1f / 100f).ToString()
							});
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(_Instantiate_Object);
							gameObject.transform.SetParent(_Parent_Transform);
							gameObject.transform.localPosition = Vector3.zero;
							gameObject.transform.localEulerAngles = Vector3.zero;
							gameObject.transform.localScale = Vector3.one;
							gameObject.transform.FindChild("lbl_Value").gameObject.SetActive(false);
							gameObject.transform.FindChild("lbl_Describe").GetComponent<Text>().text = text.Replace("\\n", "\n");
							gameObject.SetActive(true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x0008C93E File Offset: 0x0008AB3E
	private void Synthetic_Describe_Tips_Enter(GameObject _GameObject)
	{
		this.UI_Component.Right_Synthetic_Describe_Tips_Object.SetActive(true);
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x0008C953 File Offset: 0x0008AB53
	private void Synthetic_Describe_Tips_Exit(GameObject _GameObject)
	{
		this.UI_Component.Right_Synthetic_Describe_Tips_Object.SetActive(false);
	}

	// Token: 0x04000BD7 RID: 3031
	private Gem_UI _UI_Component;

	// Token: 0x04000BD8 RID: 3032
	private Gem_Controller _Controller_Component;

	// Token: 0x04000BD9 RID: 3033
	private int _Tab_State = 1;

	// Token: 0x04000BDA RID: 3034
	private bool _Display_Sell_State;

	// Token: 0x04000BDB RID: 3035
	private int _Display_Type;

	// Token: 0x04000BDC RID: 3036
	private int _Display_Quality;

	// Token: 0x04000BDD RID: 3037
	private int _Current_Page = 1;

	// Token: 0x04000BDE RID: 3038
	private int _Max_Page = 1;

	// Token: 0x04000BDF RID: 3039
	private int _Synthetic_Auto_Location = 1;

	// Token: 0x04000BE0 RID: 3040
	private List<int> _Synthetic_Data;

	// Token: 0x04000BE1 RID: 3041
	private ArrayList _Package_Item_Select_Object_List;

	// Token: 0x04000BE2 RID: 3042
	private Dictionary<int, Vector3> _Sell_Item_Data;

	// Token: 0x04000BE3 RID: 3043
	private GameObject _Gem_Sell_Effect_Parent_Object;

	// Token: 0x04000BE4 RID: 3044
	private Dictionary<int, List<GameObject>> _Packgae_Item_Data;

	// Token: 0x04000BE5 RID: 3045
	private bool _Synthetic_State;

	// Token: 0x04000BE6 RID: 3046
	private bool _Batch_Synthetic_State;

	// Token: 0x04000BE7 RID: 3047
	private bool _Batch_Synthetic_Auto_State;

	// Token: 0x04000BE8 RID: 3048
	private float _Batch_Synthetic_Time;

	// Token: 0x04000BE9 RID: 3049
	private float _Batch_Synthetic_Delay_Time = 5f;
}

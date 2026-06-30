using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000148 RID: 328
public class Gem_Sell_View : MonoBehaviour
{
	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00083630 File Offset: 0x00081830
	// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0008368E File Offset: 0x0008188E
	public Gem_Sell_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Gem_Sell_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Gem_Sell_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00083698 File Offset: 0x00081898
	// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x000836B0 File Offset: 0x000818B0
	public Gem_View Parent_View_Component
	{
		get
		{
			return this._Parent_View_Component;
		}
		set
		{
			this._Parent_View_Component = value;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000836BC File Offset: 0x000818BC
	// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00083769 File Offset: 0x00081969
	public List<GameObject> Item_List
	{
		get
		{
			bool flag = this._Item_List == null;
			if (flag)
			{
				this._Item_List = new List<GameObject>
				{
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null
				};
			}
			return this._Item_List;
		}
		set
		{
			this._Item_List = value;
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x00083774 File Offset: 0x00081974
	public void Initialize_Data(List<int> _Sell_Data)
	{
		fUIManager.tipBlur.rawImage = Gem_Sell_UICon.Instance._Background_Raw_Image;
		fUIManager.tipBlur.enabled = true;
		EventTriggerListener.Get(this.UI_Component.Confirm_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Confirm_Sell);
		EventTriggerListener.Get(this.UI_Component.Cancel_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Cancel_Sell);
		this.Display_Data(_Sell_Data);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000837F8 File Offset: 0x000819F8
	public void Display_Data(List<int> _Sell_Data)
	{
		int num = 0;
		Dictionary<int, List<int>> gem_Package_Data = Gem_Data.Instance.Gem_Package_Data;
		for (int i = 0; i < 15; i++)
		{
			InputField _Amount_Component = null;
			int _Gem_Amount = 0;
			bool flag = this.Item_List[i] == null;
			Transform transform;
			if (flag)
			{
				transform = this.UI_Component.Grid_Object.transform.FindChild("Item_" + (i + 1).ToString());
				this.Item_List[i] = transform.gameObject;
			}
			else
			{
				transform = this.Item_List[i].transform;
			}
			bool flag2 = i >= _Sell_Data.Count;
			if (flag2)
			{
				transform.gameObject.SetActive(false);
			}
			else
			{
				transform.gameObject.SetActive(true);
				Image component = transform.FindChild("img_Icon").GetComponent<Image>();
				Image component2 = transform.FindChild("img_Quality").GetComponent<Image>();
				Text component3 = transform.FindChild("lbl_Index").GetComponent<Text>();
				Text component4 = transform.FindChild("lbl_ID").GetComponent<Text>();
				Text component5 = transform.FindChild("lbl_Gem_Name").GetComponent<Text>();
				Text component6 = transform.FindChild("lbl_Amount").GetComponent<Text>();
				_Amount_Component = transform.FindChild("txt_Amount").GetComponent<InputField>();
				_Amount_Component.onValueChanged.RemoveAllListeners();
				Button component7 = transform.FindChild("btn_Reduce").GetComponent<Button>();
				component7.onClick.RemoveAllListeners();
				component7.onClick.AddListener(delegate()
				{
					this.Reduce_Sell_Amount(_Amount_Component);
				});
				Button component8 = transform.FindChild("btn_Add").GetComponent<Button>();
				component8.onClick.RemoveAllListeners();
				component8.onClick.AddListener(delegate()
				{
					this.Add_Sell_Amount(_Amount_Component);
				});
				int key = _Sell_Data[i];
				List<int> list = gem_Package_Data[key];
				int gem_Index = list[0];
				_Gem_Amount = list[2];
				_Amount_Component.text = _Gem_Amount.ToString();
				_Amount_Component.onValueChanged.AddListener(delegate(string _Value)
				{
					this.Edit_Text_Validate(_Amount_Component, _Value, 0, _Gem_Amount);
				});
				component3.text = gem_Index.ToString();
				component4.text = key.ToString();
				component6.text = _Gem_Amount.ToString();
				Gem_Entity gem_Entity = this.Parent_View_Component.Get_Gem_Xml_Data(gem_Index);
				num += _Gem_Amount * gem_Entity.Sell_Price;
				Fun.Load_Image("Icon/fuwen_icon", component, gem_Entity.Icon);
				component2.sprite = Window.spriteName(AtlasType.ATLAS_GEM, gem_Entity.Quality.ToString());
				component5.text = this.Get_Gem_Quality_Color_Value(gem_Entity.Quality, gem_Entity.Name);
			}
		}
		this.UI_Component.Sell_Price_Text.text = num.ToString();
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00083B4C File Offset: 0x00081D4C
	private void Edit_Text_Validate(InputField _InputField_Component, string _Value, int _Min, int _Max)
	{
		int value = 0;
		this.Validate_Number(_Value, out value);
		_InputField_Component.text = this.Detection_Modify_Value(value, _Min, _Max).ToString();
		this.Calculate_Sell_Price();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00083B88 File Offset: 0x00081D88
	private void Calculate_Sell_Price()
	{
		int num = 0;
		for (int i = 0; i < this.Item_List.Count; i++)
		{
			Transform transform = this.Item_List[i].transform;
			bool activeSelf = transform.gameObject.activeSelf;
			if (activeSelf)
			{
				Text component = transform.FindChild("lbl_Index").GetComponent<Text>();
				Text component2 = transform.FindChild("lbl_ID").GetComponent<Text>();
				InputField component3 = transform.FindChild("txt_Amount").GetComponent<InputField>();
				int gem_Index = Fun.Convert_Int(component.text);
				int num2 = Fun.Convert_Int(component2.text);
				int num3 = Fun.Convert_Int(component3.text);
				Gem_Entity gem_Entity = this.Parent_View_Component.Get_Gem_Xml_Data(gem_Index);
				bool flag = gem_Entity != null;
				if (flag)
				{
					num += num3 * gem_Entity.Sell_Price;
				}
			}
		}
		this.UI_Component.Sell_Price_Text.text = num.ToString();
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00083C9C File Offset: 0x00081E9C
	private bool Validate_Number(string _Value, out int _Result)
	{
		Regex regex = new Regex("^\\d+$");
		_Result = 0;
		bool flag = regex.IsMatch(_Value);
		bool result;
		if (flag)
		{
			_Result = int.Parse(_Value);
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00083CD8 File Offset: 0x00081ED8
	public int Detection_Modify_Value(int _Value, int _Min, int _Max)
	{
		bool flag = _Value < _Min;
		int result;
		if (flag)
		{
			result = _Min;
		}
		else
		{
			bool flag2 = _Value > _Max;
			if (flag2)
			{
				result = _Max;
			}
			else
			{
				result = _Value;
			}
		}
		return result;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00083D08 File Offset: 0x00081F08
	private void Reduce_Sell_Amount(InputField _InputField_Component)
	{
		int num = Fun.Convert_Int(_InputField_Component.text);
		_InputField_Component.text = (num - 1).ToString();
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00083D38 File Offset: 0x00081F38
	private void Add_Sell_Amount(InputField _InputField_Component)
	{
		int num = Fun.Convert_Int(_InputField_Component.text);
		_InputField_Component.text = (num + 1).ToString();
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00083D68 File Offset: 0x00081F68
	private void Confirm_Sell(GameObject _GameObject)
	{
		ArrayList value = new ArrayList
		{
			this.Get_Sell_Data(),
			"宝石合成"
		};
		fGameManager.SendMsgToHallServer(3020000, new ArrayList
		{
			3020105,
			value
		});
		Gem_Sell_UICon.Instance.Close();
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00083DD1 File Offset: 0x00081FD1
	private void Cancel_Sell(GameObject _GameObject)
	{
		Gem_Sell_UICon.Instance.Close();
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00083DE0 File Offset: 0x00081FE0
	private ArrayList Get_Sell_Data()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < 15; i++)
		{
			Transform transform = this.UI_Component.Grid_Object.transform.FindChild("Item_" + (i + 1).ToString());
			bool activeSelf = transform.gameObject.activeSelf;
			if (activeSelf)
			{
				Text component = transform.FindChild("lbl_Index").GetComponent<Text>();
				Text component2 = transform.FindChild("lbl_ID").GetComponent<Text>();
				InputField component3 = transform.FindChild("txt_Amount").GetComponent<InputField>();
				int num = Fun.Convert_Int(component.text);
				int num2 = Fun.Convert_Int(component2.text);
				int num3 = Fun.Convert_Int(component3.text);
				ArrayList value = new ArrayList
				{
					num,
					num2,
					num3
				};
				arrayList.Add(value);
			}
		}
		return arrayList;
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00083F14 File Offset: 0x00082114
	private string Get_Gem_Quality_Color_Value(int _Quality, string _Value)
	{
		if (!true)
		{
		}
		string result;
		switch (_Quality)
		{
		case 1:
			result = "<color=#BBBBBB>" + _Value + "</color>";
			break;
		case 2:
			result = "<color=#00FF00>" + _Value + "</color>";
			break;
		case 3:
			result = "<color=#00B4FF>" + _Value + "</color>";
			break;
		case 4:
			result = "<color=#FF00FF>" + _Value + "</color>";
			break;
		case 5:
			result = "<color=#F0C800>" + _Value + "</color>";
			break;
		case 6:
			result = "<color=#FF3333>" + _Value + "</color>";
			break;
		default:
			result = "<color=#BBBBBB>" + _Value + "</color>";
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x04000B56 RID: 2902
	private Gem_Sell_UI _UI_Component;

	// Token: 0x04000B57 RID: 2903
	private Gem_View _Parent_View_Component;

	// Token: 0x04000B58 RID: 2904
	private List<GameObject> _Item_List;
}

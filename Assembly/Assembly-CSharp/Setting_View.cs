using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000303 RID: 771
public class Setting_View : MonoBehaviour
{
	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x060024EE RID: 9454 RVA: 0x0014B9A8 File Offset: 0x00149BA8
	// (set) Token: 0x060024EF RID: 9455 RVA: 0x0014BA06 File Offset: 0x00149C06
	public Setting_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Setting_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Setting_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0014BA10 File Offset: 0x00149C10
	// (set) Token: 0x060024F1 RID: 9457 RVA: 0x0014BA6E File Offset: 0x00149C6E
	public Setting_Controller Controller_Component
	{
		get
		{
			bool flag = this._Controller_Component == null;
			if (flag)
			{
				this._Controller_Component = base.gameObject.GetComponent<Setting_Controller>();
				bool flag2 = this._Controller_Component == null;
				if (flag2)
				{
					this._Controller_Component = base.gameObject.AddComponent<Setting_Controller>();
				}
			}
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0014BA78 File Offset: 0x00149C78
	// (set) Token: 0x060024F3 RID: 9459 RVA: 0x0014BA90 File Offset: 0x00149C90
	public string Selected_Tab_Name
	{
		get
		{
			return this._Selected_Tab_Name;
		}
		set
		{
			this._Selected_Tab_Name = value;
		}
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0014BA9C File Offset: 0x00149C9C
	// (set) Token: 0x060024F5 RID: 9461 RVA: 0x0014BAB4 File Offset: 0x00149CB4
	public GameObject Selected_Tab_Object
	{
		get
		{
			return this._Selected_Tab_Object;
		}
		set
		{
			this._Selected_Tab_Object = value;
		}
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x060024F6 RID: 9462 RVA: 0x0014BAC0 File Offset: 0x00149CC0
	// (set) Token: 0x060024F7 RID: 9463 RVA: 0x0014BAD8 File Offset: 0x00149CD8
	public bool Video_Advanced_State
	{
		get
		{
			return this._Video_Advanced_State;
		}
		set
		{
			this._Video_Advanced_State = value;
		}
	}

	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x060024F8 RID: 9464 RVA: 0x0014BAE4 File Offset: 0x00149CE4
	// (set) Token: 0x060024F9 RID: 9465 RVA: 0x0014BAFC File Offset: 0x00149CFC
	public bool Voice_On_Off_Wait_State
	{
		get
		{
			return this._Voice_On_Off_Wait_State;
		}
		set
		{
			this._Voice_On_Off_Wait_State = value;
		}
	}

	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x060024FA RID: 9466 RVA: 0x0014BB06 File Offset: 0x00149D06
	public Event Key_Event
	{
		get
		{
			return Event.current;
		}
	}

	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x060024FB RID: 9467 RVA: 0x0014BB10 File Offset: 0x00149D10
	// (set) Token: 0x060024FC RID: 9468 RVA: 0x0014BB28 File Offset: 0x00149D28
	public int Selected_Hero_Control_Index
	{
		get
		{
			return this._Selected_Hero_Control_Index;
		}
		set
		{
			this._Selected_Hero_Control_Index = value;
			this.Controller_Component.Selected_Hero_Control_Index = value;
			this.Controller_Component.Model_Component.Selected_Hero_Control_Index = value;
		}
	}

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x060024FD RID: 9469 RVA: 0x0014BB54 File Offset: 0x00149D54
	// (set) Token: 0x060024FE RID: 9470 RVA: 0x0014BB6C File Offset: 0x00149D6C
	public string Key_Property_Name
	{
		get
		{
			return this._Key_Property_Name;
		}
		set
		{
			this._Key_Property_Name = value;
		}
	}

	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x060024FF RID: 9471 RVA: 0x0014BB78 File Offset: 0x00149D78
	// (set) Token: 0x06002500 RID: 9472 RVA: 0x0014BB90 File Offset: 0x00149D90
	public float Auto_Hidden_Modify_Content_Time
	{
		get
		{
			return this._Auto_Hidden_Modify_Content_Time;
		}
		set
		{
			this._Auto_Hidden_Modify_Content_Time = value;
		}
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x0014BB9C File Offset: 0x00149D9C
	private void Start()
	{
		this.Selected_Tab_Object = this.UI_Component.Tab_Video_Toggle.gameObject;
		this.Selected_Tab_Name = "tab_Video";
		EventTriggerListener.Get(this.UI_Component.Close_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Close);
		EventTriggerListener.Get(this.UI_Component.Confirm_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Close);
		this.UI_Component.Tab_Video_Toggle.onValueChanged.AddListener(delegate(bool p0)
		{
			this.Change_Tab(this.UI_Component.Tab_Video_Toggle.gameObject);
		});
		this.UI_Component.Tab_Voice_Toggle.onValueChanged.AddListener(delegate(bool p0)
		{
			this.Change_Tab(this.UI_Component.Tab_Voice_Toggle.gameObject);
		});
		this.UI_Component.Tab_Control_Toggle.onValueChanged.AddListener(delegate(bool p0)
		{
			this.Change_Tab(this.UI_Component.Tab_Control_Toggle.gameObject);
		});
		this.UI_Component.Tab_Social_Toggle.onValueChanged.AddListener(delegate(bool p0)
		{
			this.Change_Tab(this.UI_Component.Tab_Social_Toggle.gameObject);
		});
		EventTriggerListener.Get(this.UI_Component.Video_Restore_Setting_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Restore_Video_Setting);
		EventTriggerListener.Get(this.UI_Component.Video_Use_Setting_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Use_Video_Setting);
		EventTriggerListener.Get(this.UI_Component.Voice_Restore_Setting_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Restore_Voice_Setting);
		EventTriggerListener.Get(this.UI_Component.Control_Restore_Setting_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Restore_Control_Setting);
		EventTriggerListener.Get(this.UI_Component.Social_Restore_Setting_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Restore_Social_Setting);
		this.Initialize_Video_Setting_Component();
		this.Initialize_Voice_Setting_Component();
		this.Initialize_Game_Setting_Component();
		this.Initialize_Control_Setting_Component();
		this.Initialize_Social_Setting_Component();
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x0014BD84 File Offset: 0x00149F84
	private void OnGUI()
	{
		bool flag = Time.time >= this.Auto_Hidden_Modify_Content_Time;
		if (flag)
		{
			this.UI_Component.Control_Content_Result_Image.gameObject.SetActive(false);
			this.UI_Component.Control_Content_Result_Text.gameObject.SetActive(false);
		}
		this.Detection_Modify_Key();
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x0014BDE0 File Offset: 0x00149FE0
	public void Close(GameObject _GameObject)
	{
		bool flag = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().playerZhunxingMe != null;
		if (flag)
		{
			fGameManager.GetInstance().getPlayer().playerZhunxingMe.zhunxingshowOrHide(true);
		}
		SettingCon.Instance.Close();
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x0014BE3C File Offset: 0x0014A03C
	private void Change_Tab(GameObject _GameObject)
	{
		bool flag = this.Selected_Tab_Object != null;
		if (flag)
		{
			this.Display_Or_Hidden_Tab_Content(this.Selected_Tab_Object.name, false);
		}
		this.Selected_Tab_Object = _GameObject;
		this.Selected_Tab_Name = this.Selected_Tab_Object.name;
		this.Display_Or_Hidden_Tab_Content(this.Selected_Tab_Object.name, true);
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x0014BE9C File Offset: 0x0014A09C
	private void Display_Or_Hidden_Tab_Content(string _Type, bool _State)
	{
		if (!(_Type == "tab_Video"))
		{
			if (!(_Type == "tab_Voice"))
			{
				if (!(_Type == "tab_Game"))
				{
					if (!(_Type == "tab_Control"))
					{
						if (_Type == "tab_Social")
						{
							this.UI_Component.Social_Content_Object.SetActive(_State);
						}
					}
					else
					{
						this.UI_Component.Control_Content_Object.SetActive(_State);
					}
				}
				else
				{
					this.UI_Component.Game_Content_Object.SetActive(_State);
				}
			}
			else
			{
				this.UI_Component.Voice_Content_Object.SetActive(_State);
			}
		}
		else
		{
			this.UI_Component.Video_Content_Object.SetActive(_State);
		}
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x0014BF58 File Offset: 0x0014A158
	public void Install_Language(Dropdown _Dropdown)
	{
		foreach (Dropdown.OptionData optionData in _Dropdown.options)
		{
			bool flag = Regex.IsMatch(optionData.text, "^[0-9]*$");
			if (flag)
			{
				optionData.text = Language.GetLang(int.Parse(optionData.text.Trim()), new object[0]);
			}
		}
		Text[] componentsInChildren = _Dropdown.GetComponentsInChildren<Text>();
		foreach (Text text in componentsInChildren)
		{
			bool flag2 = Regex.IsMatch(text.text, "^[0-9]*$");
			if (flag2)
			{
				text.text = Language.GetLang(int.Parse(text.text.Trim()), new object[0]);
			}
		}
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x0014C048 File Offset: 0x0014A248
	private void Initialize_Video_Setting_Component()
	{
		this.Install_Language(this.UI_Component.Video_Content_Structure["Display_Type"].GetComponentInChildren<Dropdown>());
		Dropdown effectDdl = this.UI_Component.Video_Content_Structure["Effect_Level"].GetComponentInChildren<Dropdown>();
		this.Install_Language(effectDdl);
		if (effectDdl.options.Count <= 3)
		{
			effectDdl.options.Add(new Dropdown.OptionData("Ultra"));
		}
		this.Install_Language(this.UI_Component.Video_Content_Structure["Texture_Quality"].GetComponentInChildren<Dropdown>());
		this.Install_Language(this.UI_Component.Video_Content_Structure["Anisotropic_Textures"].GetComponentInChildren<Dropdown>());
		this.Install_Language(this.UI_Component.Video_Content_Structure["Anti_Aliasing"].GetComponentInChildren<Dropdown>());
		this.Install_Language(this.UI_Component.Video_Content_Structure["Shadow_Resolution"].GetComponentInChildren<Dropdown>());
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Display_Type"]);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Resolution_Ratio"]);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Video_Content_Structure["Smooth"], 0, 10);
		this.Button_Prev_And_Next_Event(this.UI_Component.Video_Content_Structure["VSync"], 2);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Video_Content_Structure["Frame_Limit"], 30, 300);
		this.Button_Prev_And_Next_Event(this.UI_Component.Video_Content_Structure["Display_Efficiency_Ping"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Video_Content_Structure["Display_Efficiency_FPS"], 2);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Effect_Level"]);
		this.Button_Click_Video_Advanced_Event(this.UI_Component.Video_Content_Structure["Advanced"]);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Texture_Quality"]);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Anisotropic_Textures"]);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Anti_Aliasing"]);
		this.Dropdown_Selected_Video_Event(this.UI_Component.Video_Content_Structure["Shadow_Resolution"]);
		this.Display_Video_Data();
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x0014C29C File Offset: 0x0014A49C
	private void Initialize_Voice_Setting_Component()
	{
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Main_Volume"], 0, 100);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Music_Volume"], 0, 100);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Sound_Volume"], 0, 100);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Custom_Voice_Volume"], 0, 100);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Chat_Voice_Volume"], 0, 100);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Voice_Content_Structure["Microphone_Volume"], 0, 100);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Main_Volume"]);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Music_Volume"]);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Sound_Volume"]);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Custom_Voice_Volume"]);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Chat_Voice_Volume"]);
		this.Button_Click_Voice_On_Off_Event(this.UI_Component.Voice_Content_Structure["Microphone_Volume"]);
		this.Button_Prev_And_Next_Event(this.UI_Component.Voice_Content_Structure["Team_Voice_Chat_Type"], 3);
		this.Button_Prev_And_Next_Event(this.UI_Component.Voice_Content_Structure["Country_Voice_Chat_Type"], 3);
		this.Button_Prev_And_Next_Event(this.UI_Component.Voice_Content_Structure["Voice_Chat_Type"], 2);
		this.Display_Voice_Data();
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x0014C46A File Offset: 0x0014A66A
	private void Initialize_Game_Setting_Component()
	{
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x0014C470 File Offset: 0x0014A670
	private void Initialize_Control_Setting_Component()
	{
		this.Refresh_Hero_Control_List();
		Button control_Content_Restore_Single_Hero_Button = this.UI_Component.Control_Content_Restore_Single_Hero_Button;
		control_Content_Restore_Single_Hero_Button.onClick.AddListener(new UnityAction(this.Restore_Hero_Control_Data));
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Control_Content_Structure["Mouse_Sensitivity"], 0, 100);
		this.Button_Prev_And_Next_Event(this.UI_Component.Control_Content_Structure["Mouse_Inversion"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Control_Content_Structure["Teammate_Health_Bars"], 2);
		this.InputField_And_Slider_Linkage_Event(this.UI_Component.Control_Content_Structure["Aim_Mouse_Sensitivity"], 0, 100);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Forward"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Backward"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Left"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Right"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Jump"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Skill_1"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Skill_2"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Skill_3"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Use_Weapon_1"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Use_Weapon_2"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Reload"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Call_Weapon_1"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Call_Weapon_2"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Melee_Attack"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Call_Prev_Weapon"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Call_Next_Weapon"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Replace_Hero"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Score_Window"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Menu"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Thank"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Expression"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Roger"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Hello"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Custom_Voice_Sound"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Need_Help"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Ultimate_Skill_State"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Interact_Collection"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Spray_Paint"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Reply_Whisper"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Display_Chat_Window"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Voice_Chat"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Microphone_Mute_On_Off"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Open_Chat_Window"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Team_Chat_On_Off"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Game_Chat_On_Off"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Hidden_Chat_Window"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Improve_Quality"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Reduce_Quality"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Check_Hero_Information"]);
		this.Toggle_Click_Key_Event(this.UI_Component.Control_Content_Structure["Key_Open_Menu"]);
		this.Display_Control_Data();
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x0014C98C File Offset: 0x0014AB8C
	private void Refresh_Hero_Control_List()
	{
		string str = string.Empty;
		GameRoot.inst.Setting.Hero_Control_Class.Check_Data_Conformity_Degree();
		Dropdown control_Content_Selected_Hero_Dropdown = this.UI_Component.Control_Content_Selected_Hero_Dropdown;
		control_Content_Selected_Hero_Dropdown.onValueChanged.AddListener(new UnityAction<int>(this.Tab_Control_Selected_Hero));
		control_Content_Selected_Hero_Dropdown.options.Clear();
		Dropdown.OptionData item = new Dropdown.OptionData(Language.GetLang(102390, new object[0]));
		control_Content_Selected_Hero_Dropdown.options.Add(item);
		List<int> list = new List<int>(HeroDataManager.GetInstance().allOpenHero.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			OpenHeroData openHeroData = HeroDataManager.GetInstance().allOpenHero[list[i]];
			bool flag = fGameManager.mloadConfig.AllHeroList.ContainsKey(openHeroData.nHeroId);
			if (flag)
			{
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[openHeroData.nHeroId];
				str = ((!GameRoot.inst.Setting.Hero_Control_Class.Hero_Control_Data.ContainsKey(openHeroData.nHeroId)) ? string.Empty : "*");
				bool flag2 = openHeroData.nHeroId == this.Selected_Hero_Control_Index;
				if (flag2)
				{
					control_Content_Selected_Hero_Dropdown.captionText.text = heroData.hname + "   " + str;
				}
				item = new Dropdown.OptionData(heroData.hname + "   " + str, Window.spriteName("Setting_Atlas", heroData.hhead));
				control_Content_Selected_Hero_Dropdown.options.Add(item);
			}
		}
		this.Tab_Control_Selected_Hero(this.Selected_Hero_Control_Index);
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x0014CB48 File Offset: 0x0014AD48
	private void Initialize_Social_Setting_Component()
	{
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Team_Invited_Type"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Team_Apply_Type"], 3);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Team_Player_Invited_Permission"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Display_Offline_Friends"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Auto_Accept_Add_Friend_Apply"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Friends_Online_Notice"], 2);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Loading_Background_Image"], fGameManager.mloadConfig.AllHeroInBg.Keys.Count);
		this.Button_Prev_And_Next_Event(this.UI_Component.Social_Content_Structure["Death_Playback"], 2);
		this.Display_Social_Data();
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x0014CC58 File Offset: 0x0014AE58
	public void Display_Video_Data()
	{
		this.Set_Text(this.UI_Component.Video_Content_Structure["Graphics_Equipment_Describe"], "lbl_Value", GameRoot.inst.Setting.Video_Class.Graphics_Equipment_Describe.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Display_Type"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Display_Type.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Resolution_Ratio"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Resolution_Ratio.ToString());
		int num = GameRoot.inst.Setting.Video_Class.Smooth;
		this.Set_InputField(this.UI_Component.Video_Content_Structure["Smooth"], "txt_Value", num.ToString());
		this.Set_Slider(this.UI_Component.Video_Content_Structure["Smooth"], "Slider", (float)num);
		this.Get_Button_State_Describe_2(this.UI_Component.Video_Content_Structure["VSync"], "Grid/lbl_State", GameRoot.inst.Setting.Video_Class.VSync.ToString());
		num = GameRoot.inst.Setting.Video_Class.Frame_Limit;
		this.Set_InputField(this.UI_Component.Video_Content_Structure["Frame_Limit"], "txt_Value", num.ToString());
		this.Set_Slider(this.UI_Component.Video_Content_Structure["Frame_Limit"], "Slider", (float)num);
		this.Get_Button_State_Describe_2(this.UI_Component.Video_Content_Structure["Display_Efficiency_Ping"], "Grid/lbl_State", GameRoot.inst.Setting.Video_Class.Display_Efficiency_Ping.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Video_Content_Structure["Display_Efficiency_FPS"], "Grid/lbl_State", GameRoot.inst.Setting.Video_Class.Display_Efficiency_FPS.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Effect_Level"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Effect_Level.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Texture_Quality"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Texture_Quality.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Anisotropic_Textures"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Anisotropic_Textures.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Anti_Aliasing"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Anti_Aliasing.ToString());
		this.Set_Dropdown(this.UI_Component.Video_Content_Structure["Shadow_Resolution"], "ddl_Select", GameRoot.inst.Setting.Video_Class.Shadow_Resolution.ToString());
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x0014CFC4 File Offset: 0x0014B1C4
	public void Display_Voice_Data()
	{
		float parameter = GameRoot.inst.Setting.Voice_Class.Main_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Main_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Main_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Main_Volume"], parameter);
		parameter = GameRoot.inst.Setting.Voice_Class.Music_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Music_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Music_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Music_Volume"], parameter);
		parameter = GameRoot.inst.Setting.Voice_Class.Sound_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Sound_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Sound_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Sound_Volume"], parameter);
		parameter = GameRoot.inst.Setting.Voice_Class.Custom_Voice_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Custom_Voice_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Custom_Voice_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Custom_Voice_Volume"], parameter);
		parameter = GameRoot.inst.Setting.Voice_Class.Chat_Voice_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Chat_Voice_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Chat_Voice_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Chat_Voice_Volume"], parameter);
		parameter = GameRoot.inst.Setting.Voice_Class.Microphone_Volume;
		this.Set_InputField(this.UI_Component.Voice_Content_Structure["Microphone_Volume"], "txt_Value", parameter.ToString());
		this.Set_Slider(this.UI_Component.Voice_Content_Structure["Microphone_Volume"], "Slider", parameter);
		this.Detection_Voice_On_Off(this.UI_Component.Voice_Content_Structure["Microphone_Volume"], parameter);
		this.Get_Button_State_Describe_3(this.UI_Component.Voice_Content_Structure["Team_Voice_Chat_Type"], "Grid/lbl_State", GameRoot.inst.Setting.Voice_Class.Team_Voice_Chat_Type.ToString());
		this.Get_Button_State_Describe_3(this.UI_Component.Voice_Content_Structure["Country_Voice_Chat_Type"], "Grid/lbl_State", GameRoot.inst.Setting.Voice_Class.Country_Voice_Chat_Type.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Voice_Content_Structure["Voice_Chat_Type"], "Grid/lbl_State", GameRoot.inst.Setting.Voice_Class.Voice_Chat_Type.ToString());
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x0014D378 File Offset: 0x0014B578
	private void Display_Control_Data()
	{
		Setting_Entity_Control setting_Entity_Control = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.Selected_Hero_Control_Index);
		this.Set_InputField(this.UI_Component.Control_Content_Structure["Mouse_Sensitivity"], "txt_Value", setting_Entity_Control.Mouse_Sensitivity.ToString());
		this.Set_Slider(this.UI_Component.Control_Content_Structure["Mouse_Sensitivity"], "Slider", setting_Entity_Control.Mouse_Sensitivity);
		this.Get_Button_State_Describe_2(this.UI_Component.Control_Content_Structure["Mouse_Inversion"], "Grid/lbl_State", setting_Entity_Control.Mouse_Inversion.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Control_Content_Structure["Teammate_Health_Bars"], "Grid/lbl_State", setting_Entity_Control.Teammate_Health_Bars.ToString());
		this.Set_InputField(this.UI_Component.Control_Content_Structure["Aim_Mouse_Sensitivity"], "txt_Value", setting_Entity_Control.Aim_Mouse_Sensitivity.ToString());
		this.Set_Slider(this.UI_Component.Control_Content_Structure["Aim_Mouse_Sensitivity"], "Slider", setting_Entity_Control.Aim_Mouse_Sensitivity);
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Forward"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Forward.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Backward"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Backward.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Left"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Left.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Right"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Right.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Jump"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Jump.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Skill_1"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Skill_1.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Skill_2"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Skill_2.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Skill_3"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Skill_3.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Use_Weapon_1"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Use_Weapon_1.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Use_Weapon_2"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Use_Weapon_2.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Reload"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Reload.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Call_Weapon_1"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Call_Weapon_1.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Call_Weapon_2"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Call_Weapon_2.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Melee_Attack"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Melee_Attack.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Call_Prev_Weapon"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Call_Prev_Weapon.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Call_Next_Weapon"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Call_Next_Weapon.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Replace_Hero"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Replace_Hero.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Score_Window"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Score_Window.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Menu"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Menu.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Thank"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Thank.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Expression"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Expression.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Roger"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Roger.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Hello"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Hello.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Custom_Voice_Sound"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Custom_Voice_Sound.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Need_Help"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Need_Help.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Ultimate_Skill_State"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Ultimate_Skill_State.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Interact_Collection"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Interact_Collection.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Spray_Paint"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Spray_Paint.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Reply_Whisper"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Reply_Whisper.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Display_Chat_Window"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Display_Chat_Window.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Voice_Chat"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Voice_Chat.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Microphone_Mute_On_Off"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Microphone_Mute_On_Off.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Open_Chat_Window"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Open_Chat_Window.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Team_Chat_On_Off"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Team_Chat_On_Off.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Game_Chat_On_Off"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Game_Chat_On_Off.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Hidden_Chat_Window"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Hidden_Chat_Window.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Improve_Quality"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Improve_Quality.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Reduce_Quality"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Reduce_Quality.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Check_Hero_Information"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Check_Hero_Information.ToString());
		this.Set_Text(this.UI_Component.Control_Content_Structure["Key_Open_Menu"], "tog_Key/lbl_Key", setting_Entity_Control.Key_Open_Menu.ToString());
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x0014DB90 File Offset: 0x0014BD90
	private void Display_Social_Data()
	{
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Team_Invited_Type"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Team_Invited_Type.ToString());
		this.Get_Button_State_Describe_3(this.UI_Component.Social_Content_Structure["Team_Apply_Type"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Team_Apply_Type.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Team_Player_Invited_Permission"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Team_Player_Invited_Permission.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Display_Offline_Friends"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Display_Offline_Friends.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Auto_Accept_Add_Friend_Apply"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Auto_Accept_Add_Friend_Apply.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Friends_Online_Notice"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Friends_Online_Notice.ToString());
		this.Get_Button_State_Describe_More(this.UI_Component.Social_Content_Structure["Loading_Background_Image"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Loading_Background_Image.ToString());
		this.Get_Button_State_Describe_2(this.UI_Component.Social_Content_Structure["Death_Playback"], "Grid/lbl_State", GameRoot.inst.Setting.Social_Class.Death_Playback.ToString());
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x0014DD88 File Offset: 0x0014BF88
	private void Button_Click_Video_Advanced_Event(GameObject _Parent_Object)
	{
		Button _Button_Component = null;
		_Button_Component = _Parent_Object.transform.Find("btn_Display_Advanced").GetComponent<Button>();
		_Button_Component.onClick.AddListener(delegate()
		{
			this.Click_Display_Or_Hidden_Video_Advanced(_Parent_Object, _Button_Component.gameObject);
		});
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x0014DDEE File Offset: 0x0014BFEE
	private void Click_Display_Or_Hidden_Video_Advanced(GameObject _Parent_Object, GameObject _Button_Object)
	{
		this.Display_Or_Hidden_Video_Advanced();
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x0014DDF8 File Offset: 0x0014BFF8
	private void Display_Or_Hidden_Video_Advanced()
	{
		this.Video_Advanced_State = !this.Video_Advanced_State;
		bool video_Advanced_State = this.Video_Advanced_State;
		if (video_Advanced_State)
		{
			this.UI_Component.Video_Mask_Content_RectTransform.sizeDelta = new Vector2(0f, 817f);
		}
		else
		{
			this.UI_Component.Video_Mask_Content_RectTransform.sizeDelta = new Vector2(0f, 487f);
		}
		this.UI_Component.Video_Content_Structure["Texture_Quality"].SetActive(this.Video_Advanced_State);
		this.UI_Component.Video_Content_Structure["Anisotropic_Textures"].SetActive(this.Video_Advanced_State);
		this.UI_Component.Video_Content_Structure["Anti_Aliasing"].SetActive(this.Video_Advanced_State);
		this.UI_Component.Video_Content_Structure["Shadow_Resolution"].SetActive(this.Video_Advanced_State);
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x0014DEEC File Offset: 0x0014C0EC
	private void InputField_And_Slider_Linkage_Event(GameObject _Parent_Object, int _Min, int _Max)
	{
		InputField component = _Parent_Object.transform.Find("txt_Value").GetComponent<InputField>();
		component.characterLimit = 3;
		component.onEndEdit.AddListener(delegate(string _Value)
		{
			this.Edit_Text_Value(_Parent_Object, _Value, _Min, _Max);
		});
		Slider component2 = _Parent_Object.transform.Find("Slider").GetComponent<Slider>();
		component2.minValue = (float)_Min;
		component2.maxValue = (float)_Max;
		component2.wholeNumbers = true;
		component2.onValueChanged.AddListener(delegate(float _Value)
		{
			this.Slider_Text_Value(_Parent_Object, _Value);
		});
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x0014DFB4 File Offset: 0x0014C1B4
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

	// Token: 0x06002516 RID: 9494 RVA: 0x0014DFF0 File Offset: 0x0014C1F0
	private void Edit_Text_Value(GameObject _Parent_Object, string _Parameter, int _Min, int _Max)
	{
		int num = 0;
		InputField component = _Parent_Object.transform.Find("txt_Value").GetComponent<InputField>();
		this.Validate_Number(_Parameter, out num);
		num = this.Controller_Component.Detection_Modify_Value(num, 0, _Max);
		component.text = num.ToString();
		this.Set_Slider(_Parent_Object, "Slider", (float)num);
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x0014E050 File Offset: 0x0014C250
	private void Slider_Text_Value(GameObject _Parent_Object, float _Parameter)
	{
		this.Set_InputField(_Parent_Object, "txt_Value", Mathf.RoundToInt(_Parameter).ToString());
		this.Set_Property(_Parent_Object.name, Mathf.RoundToInt(_Parameter).ToString());
		this.Detection_Voice_On_Off(_Parent_Object, _Parameter);
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x0014E0A0 File Offset: 0x0014C2A0
	private void Detection_Voice_On_Off(GameObject _Parent_Object, float _Parameter)
	{
		bool flag = this.UI_Component.Voice_Content_Structure.ContainsKey(_Parent_Object.name);
		if (flag)
		{
			Transform transform = this.UI_Component.Voice_Content_Structure[_Parent_Object.name].transform.Find("btn_On_Off");
			Toggle component = transform.gameObject.GetComponent<Toggle>();
			bool flag2 = _Parameter <= 0f;
			if (flag2)
			{
				component.isOn = true;
			}
			else
			{
				this.Voice_On_Off_Wait_State = true;
				component.isOn = false;
				this.Voice_On_Off_Wait_State = false;
			}
		}
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x0014E134 File Offset: 0x0014C334
	private void Button_Click_Voice_On_Off_Event(GameObject _Parent_Object)
	{
		Toggle component = _Parent_Object.transform.Find("btn_On_Off").GetComponent<Toggle>();
		component.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Voice_On_Off(_Parent_Object, _Value);
		});
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x0014E18C File Offset: 0x0014C38C
	private void Voice_On_Off(GameObject _Parent_Object, bool _State)
	{
		if (_State)
		{
			this.Set_Slider(_Parent_Object, "Slider", 0f);
		}
		else
		{
			bool flag = !this.Voice_On_Off_Wait_State;
			if (flag)
			{
				this.Set_Slider(_Parent_Object, "Slider", 100f);
			}
		}
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x0014E1D8 File Offset: 0x0014C3D8
	private void Set_InputField(GameObject _Parent_Object, string _Component_Object_Name, string _Parameter)
	{
		InputField component = _Parent_Object.transform.Find(_Component_Object_Name).GetComponent<InputField>();
		component.text = _Parameter;
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x0014E204 File Offset: 0x0014C404
	private void Set_Slider(GameObject _Parent_Object, string _Component_Object_Name, float _Parameter)
	{
		Slider component = _Parent_Object.transform.Find(_Component_Object_Name).GetComponent<Slider>();
		component.value = _Parameter;
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x0014E230 File Offset: 0x0014C430
	private void Button_Prev_And_Next_Event(GameObject _Parent_Object, int _Type)
	{
		Button _Prev_Button_Component = null;
		Button _Next_Button_Component = null;
		_Prev_Button_Component = _Parent_Object.transform.Find("Grid/btn_Prev_State").GetComponent<Button>();
		_Prev_Button_Component.onClick.AddListener(delegate()
		{
			this.Button_Change_State(_Parent_Object, _Prev_Button_Component.gameObject, _Type);
		});
		_Next_Button_Component = _Parent_Object.transform.Find("Grid/btn_Next_State").GetComponent<Button>();
		_Next_Button_Component.onClick.AddListener(delegate()
		{
			this.Button_Change_State(_Parent_Object, _Next_Button_Component.gameObject, _Type);
		});
		Button component = _Parent_Object.transform.Find("Grid/lbl_State").GetComponent<Button>();
		component.onClick.AddListener(delegate()
		{
			this.Button_Change_State(_Parent_Object, _Next_Button_Component.gameObject, _Type);
		});
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x0014E318 File Offset: 0x0014C518
	private void Button_Change_State(GameObject _Parent_Object, GameObject _Button_Object, int _Type)
	{
		int num = (int)this.Controller_Component.Get_Property_Value(this.Selected_Tab_Name, _Parent_Object.name);
		bool flag = _Button_Object.name == "btn_Prev_State";
		if (flag)
		{
			num--;
			bool flag2 = num < 0;
			if (flag2)
			{
				num = _Type - 1;
			}
		}
		else
		{
			bool flag3 = _Button_Object.name == "btn_Next_State";
			if (flag3)
			{
				num++;
				bool flag4 = num >= _Type;
				if (flag4)
				{
					num = 0;
				}
			}
		}
		this.Set_Property(_Parent_Object.name, num);
		if (_Type != 2)
		{
			if (_Type != 3)
			{
				bool flag5 = _Type == fGameManager.mloadConfig.AllHeroInBg.Keys.Count;
				if (flag5)
				{
					this.Get_Button_State_Describe_More(_Parent_Object, "Grid/lbl_State", num.ToString());
				}
			}
			else
			{
				this.Get_Button_State_Describe_3(_Parent_Object, "Grid/lbl_State", num.ToString());
			}
		}
		else
		{
			this.Get_Button_State_Describe_2(_Parent_Object, "Grid/lbl_State", num.ToString());
		}
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x0014E424 File Offset: 0x0014C624
	private void Get_Button_State_Describe_2(GameObject _Parent_Object, string _Component_Object_Name, string _Property_Value)
	{
		string text = null;
		string text2 = null;
		string name = _Parent_Object.name;
		string text3 = name;
		uint num = PrivateImplementationDetails.ComputeStringHash(text3);
		if (num <= 2038993348U)
		{
			if (num <= 747116006U)
			{
				if (num != 62380939U)
				{
					if (num != 645730799U)
					{
						if (num == 747116006U)
						{
							if (text3 == "VSync")
							{
								text = Language.GetLang(102201, new object[0]);
								text2 = Language.GetLang(102202, new object[0]);
							}
						}
					}
					else if (text3 == "Auto_Accept_Add_Friend_Apply")
					{
						text = Language.GetLang(102201, new object[0]);
						text2 = Language.GetLang(102202, new object[0]);
					}
				}
				else if (text3 == "Teammate_Health_Bars")
				{
					text = Language.GetLang(102205, new object[0]);
					text2 = Language.GetLang(102202, new object[0]);
				}
			}
			else if (num != 1311191409U)
			{
				if (num != 1675139967U)
				{
					if (num == 2038993348U)
					{
						if (text3 == "Display_Efficiency_Ping")
						{
							text = Language.GetLang(102201, new object[0]);
							text2 = Language.GetLang(102202, new object[0]);
						}
					}
				}
				else if (text3 == "Display_Offline_Friends")
				{
					text = Language.GetLang(102205, new object[0]);
					text2 = Language.GetLang(102208, new object[0]);
				}
			}
			else if (text3 == "Friends_Online_Notice")
			{
				text = Language.GetLang(102201, new object[0]);
				text2 = Language.GetLang(102202, new object[0]);
			}
		}
		else if (num <= 3457256891U)
		{
			if (num != 2099945281U)
			{
				if (num != 3390562414U)
				{
					if (num == 3457256891U)
					{
						if (text3 == "Display_Efficiency_FPS")
						{
							text = Language.GetLang(102201, new object[0]);
							text2 = Language.GetLang(102202, new object[0]);
						}
					}
				}
				else if (text3 == "Mouse_Inversion")
				{
					text = Language.GetLang(102201, new object[0]);
					text2 = Language.GetLang(102202, new object[0]);
				}
			}
			else if (text3 == "Voice_Chat_Type")
			{
				text = Language.GetLang(102203, new object[0]);
				text2 = Language.GetLang(102204, new object[0]);
			}
		}
		else if (num != 3491007749U)
		{
			if (num != 4050871219U)
			{
				if (num == 4073573778U)
				{
					if (text3 == "Team_Player_Invited_Permission")
					{
						text = Language.GetLang(102201, new object[0]);
						text2 = Language.GetLang(102202, new object[0]);
					}
				}
			}
			else if (text3 == "Death_Playback")
			{
				text = Language.GetLang(102201, new object[0]);
				text2 = Language.GetLang(102202, new object[0]);
			}
		}
		else if (text3 == "Team_Invited_Type")
		{
			text = Language.GetLang(102206, new object[0]);
			text2 = Language.GetLang(102207, new object[0]);
		}
		string parameter = (int.Parse(_Property_Value) != 1) ? text2 : text;
		this.Set_Text(_Parent_Object, "Grid/lbl_State", parameter);
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x0014E7F4 File Offset: 0x0014C9F4
	private void Get_Button_State_Describe_3(GameObject _Parent_Object, string _Component_Object_Name, string _Property_Value)
	{
		string text = null;
		string text2 = null;
		string text3 = null;
		string parameter = null;
		string name = _Parent_Object.name;
		string a = name;
		if (!(a == "Team_Voice_Chat_Type"))
		{
			if (!(a == "Country_Voice_Chat_Type"))
			{
				if (a == "Team_Apply_Type")
				{
					text = Language.GetLang(102207, new object[0]);
					text2 = Language.GetLang(102206, new object[0]);
					text3 = Language.GetLang(102202, new object[0]);
				}
			}
			else
			{
				text = Language.GetLang(102209, new object[0]);
				text2 = Language.GetLang(102201, new object[0]);
				text3 = Language.GetLang(102202, new object[0]);
			}
		}
		else
		{
			text = Language.GetLang(102209, new object[0]);
			text2 = Language.GetLang(102201, new object[0]);
			text3 = Language.GetLang(102202, new object[0]);
		}
		switch (int.Parse(_Property_Value))
		{
		case 0:
			parameter = text;
			break;
		case 1:
			parameter = text2;
			break;
		case 2:
			parameter = text3;
			break;
		}
		this.Set_Text(_Parent_Object, "Grid/lbl_State", parameter);
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x0014E920 File Offset: 0x0014CB20
	private void Get_Button_State_Describe_More(GameObject _Parent_Object, string _Component_Object_Name, string _Property_Value)
	{
		Dictionary<int, HeroInfoBg> dictionary = new Dictionary<int, HeroInfoBg>();
		dictionary = fGameManager.mloadConfig.AllHeroInBg;
		string parameter = dictionary.ContainsKey(int.Parse(_Property_Value)) ? dictionary[int.Parse(_Property_Value)].sName : Language.GetLang(102310, new object[0]);
		this.Set_Text(_Parent_Object, "Grid/lbl_State", parameter);
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x0014E984 File Offset: 0x0014CB84
	private void Set_Text(GameObject _Parent_Object, string _Component_Object_Name, string _Parameter)
	{
		string text = GameRoot.inst.Setting.Get_Key_Describe(_Parameter);
		Text component = _Parent_Object.transform.Find(_Component_Object_Name).GetComponent<Text>();
		component.text = text;
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x0014E9C4 File Offset: 0x0014CBC4
	private void Dropdown_Selected_Video_Event(GameObject _Parent_Object)
	{
		Dropdown component = _Parent_Object.transform.Find("ddl_Select").GetComponent<Dropdown>();
		component.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Dropdown_Selected_Video_Value(_Parent_Object, _Value);
		});
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x0014EA1C File Offset: 0x0014CC1C
	private void Dropdown_Selected_Video_Value(GameObject _Parent_Object, int _Parameter)
	{
		bool flag = _Parent_Object.name == "Anti_Aliasing";
		if (flag)
		{
			switch (_Parameter)
			{
			case 0:
				_Parameter = 0;
				break;
			case 1:
				_Parameter = 2;
				break;
			case 2:
				_Parameter = 4;
				break;
			case 3:
				_Parameter = 8;
				break;
			}
		}
		bool flag2 = _Parent_Object.name == "Display_Type";
		if (flag2)
		{
			bool flag3 = _Parameter != GameRoot.inst.Setting.Setting_Display_Type;
			if (flag3)
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(true);
			}
			else
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(false);
			}
		}
		bool flag4 = _Parent_Object.name == "Resolution_Ratio";
		if (flag4)
		{
			bool flag5 = _Parameter != GameRoot.inst.Setting.Setting_Resolution_Ratio;
			if (flag5)
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(true);
			}
			else
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(false);
			}
		}
		this.Set_Property(_Parent_Object.name, _Parameter);
		bool flag6 = _Parent_Object.name == "Effect_Level" || _Parent_Object.name == "Texture_Quality" || _Parent_Object.name == "Anisotropic_Textures" || _Parent_Object.name == "Anti_Aliasing" || _Parent_Object.name == "Shadow_Resolution";
		if (flag6)
		{
			this.Display_Video_Data();
		}
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x0014EBBC File Offset: 0x0014CDBC
	private void Set_Dropdown(GameObject _Parent_Object, string _Component_Object_Name, string _Parameter)
	{
		Dropdown component = _Parent_Object.transform.Find(_Component_Object_Name).GetComponent<Dropdown>();
		if (_Parent_Object.name == "Effect_Level")
		{
			if (component.options.Count == 3)
			{
				component.options.Add(new Dropdown.OptionData("Ultra"));
			}
			int parsedVal = 2;
			int.TryParse(_Parameter, out parsedVal);
			component.value = Mathf.Clamp(parsedVal, 0, component.options.Count - 1);
			component.RefreshShownValue();
			return;
		}
		bool flag = _Parent_Object.name == "Anti_Aliasing";
		if (flag)
		{
			int num = int.Parse(_Parameter);
			int num2 = num;
			switch (num2)
			{
			case 0:
				_Parameter = "0";
				break;
			case 1:
			case 3:
				break;
			case 2:
				_Parameter = "1";
				break;
			case 4:
				_Parameter = "2";
				break;
			default:
				if (num2 == 8)
				{
					_Parameter = "3";
				}
				break;
			}
		}
		bool flag2 = _Parent_Object.name == "Display_Type";
		if (flag2)
		{
			bool flag3 = component.value != GameRoot.inst.Setting.Setting_Display_Type;
			if (flag3)
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(true);
			}
			else
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(false);
			}
		}
		bool flag4 = _Parent_Object.name == "Resolution_Ratio";
		if (flag4)
		{
			bool flag5 = component.value != GameRoot.inst.Setting.Setting_Resolution_Ratio;
			if (flag5)
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(true);
			}
			else
			{
				this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(false);
			}
		}
		component.value = int.Parse(_Parameter);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x0014ED28 File Offset: 0x0014CF28
	private void Toggle_Click_Key_Event(GameObject _Parent_Object)
	{
		Toggle _Toggle_Component = null;
		_Toggle_Component = _Parent_Object.transform.Find("tog_Key").GetComponent<Toggle>();
		_Toggle_Component.onValueChanged.AddListener(delegate(bool _Value)
		{
			this.Modify_Key(_Parent_Object, _Toggle_Component, _Value);
		});
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x0014ED90 File Offset: 0x0014CF90
	private void Modify_Key(GameObject _Parent_Object, Toggle _Component, bool _Value)
	{
		if (_Value)
		{
			bool flag = this._Key == "Mouse0";
			if (flag)
			{
				this.UI_Component.Control_Content_Toggle_Group.SetAllTogglesOff();
				this._Key = null;
			}
			else
			{
				Debug.Log("修改按键开始");
				SettingCon.Instance.Modify_Key_State = true;
				this.Key_Property_Name = _Parent_Object.name;
			}
		}
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x0014EDFC File Offset: 0x0014CFFC
	private void Detection_Modify_Key()
	{
		try
		{
			bool flag = !SettingCon.Instance.Modify_Key_State;
			if (!flag)
			{
				this.UI_Component.Control_Content_ScrollRect.enabled = false;
				bool flag2 = this.Key_Event.keyCode != KeyCode.Escape && this.Key_Event.keyCode != KeyCode.Tab && this.Key_Event.keyCode != KeyCode.KeypadPlus && this.Key_Event.keyCode != KeyCode.KeypadMinus && this.Key_Event.keyCode != KeyCode.Return;
				if (flag2)
				{
					bool key = Input.GetKey(KeyCode.LeftShift);
					if (key)
					{
						this._Key = KeyCode.LeftShift.ToString();
					}
					else
					{
						bool key2 = Input.GetKey(KeyCode.RightShift);
						if (key2)
						{
							this._Key = KeyCode.RightShift.ToString();
						}
						else
						{
							bool isKey = this.Key_Event.isKey;
							if (isKey)
							{
								bool flag3 = this.Key_Event.keyCode == KeyCode.None;
								if (flag3)
								{
									return;
								}
								this._Key = this.Key_Event.keyCode.ToString();
								Debug.Log("组合键按下的键为:" + this.Key_Event.keyCode.ToString());
							}
							else
							{
								this._Key = this.Get_Mouse_Key();
								bool flag4 = this._Key == null && this._Key == null;
								if (flag4)
								{
									return;
								}
								Debug.Log("按下的鼠标键为:" + this._Key);
							}
						}
					}
					Debug.Log(string.Concat(new string[]
					{
						"按下的键为:",
						this._Key,
						" 修改的项为:",
						this.Key_Property_Name,
						string.Empty,
						this.Selected_Hero_Control_Index.ToString()
					}));
					ArrayList arrayList = new ArrayList();
					bool flag5 = this.Set_Property(this.Key_Property_Name, this._Key);
					if (flag5)
					{
						Debug.Log("修改按键成功");
					}
					this.Auto_Hidden_Modify_Content_Time = Time.time + 10f;
					Setting_Entity_Control setting_Entity_Control = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.Selected_Hero_Control_Index);
					arrayList = setting_Entity_Control.Update_Value(this.Key_Property_Name, this._Key, this.UI_Component.Control_Content_Structure);
					this.UI_Component.Control_Content_Result_Image.gameObject.SetActive(true);
					this.UI_Component.Control_Content_Result_Text.gameObject.SetActive(true);
					bool flag6 = (bool)arrayList[0];
					if (flag6)
					{
						this.UI_Component.Control_Content_Result_Image.sprite = Window.spriteName("setting_atlas", "tishi");
						this.Set_Text(this.UI_Component.Control_Content_Structure[arrayList[2].ToString()], "tog_Key/lbl_Key", this.Controller_Component.Get_Property_Value("tab_Control", arrayList[2].ToString()).ToString());
					}
					else
					{
						this.UI_Component.Control_Content_Result_Image.sprite = Window.spriteName("setting_atlas", "tishi1");
					}
					this.UI_Component.Control_Content_Result_Text.text = arrayList[1].ToString();
					this.Set_Text(this.UI_Component.Control_Content_Structure[this.Key_Property_Name], "tog_Key/lbl_Key", this._Key);
				}
				else
				{
					this.Auto_Hidden_Modify_Content_Time = Time.time + 10f;
					this.UI_Component.Control_Content_Result_Image.gameObject.SetActive(true);
					this.UI_Component.Control_Content_Result_Image.sprite = Window.spriteName("setting_atlas", "tishi2");
					this.UI_Component.Control_Content_Result_Text.gameObject.SetActive(true);
					this.UI_Component.Control_Content_Result_Text.text = Language.GetLang(102215, new object[0]);
				}
				fUIManager.GetInstance().Setting_Modify_Key_Complete(this.Key_Property_Name, this._Key);
				this.Key_Property_Name = null;
				SettingCon.Instance.Modify_Key_State = false;
				this.UI_Component.Control_Content_Toggle_Group.SetAllTogglesOff();
				this.UI_Component.Control_Content_ScrollRect.enabled = true;
			}
		}
		catch (Exception ex)
		{
			Fun.LogError(string.Concat(new object[]
			{
				ex.Message,
				" - ",
				ex.Source,
				" - ",
				ex.StackTrace,
				" - ",
				ex.HelpLink,
				" - ",
				ex.InnerException,
				"\n"
			}), null);
		}
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x0014F2F8 File Offset: 0x0014D4F8
	private string Get_Mouse_Key()
	{
		string result = null;
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButton)
		{
			result = KeyCode.Mouse0.ToString();
		}
		else
		{
			bool mouseButton2 = Input.GetMouseButton(1);
			if (mouseButton2)
			{
				result = KeyCode.Mouse1.ToString();
			}
			else
			{
				bool mouseButton3 = Input.GetMouseButton(2);
				if (mouseButton3)
				{
					result = KeyCode.Mouse2.ToString();
				}
				else
				{
					bool mouseButton4 = Input.GetMouseButton(3);
					if (mouseButton4)
					{
						result = KeyCode.Mouse3.ToString();
					}
					else
					{
						bool mouseButton5 = Input.GetMouseButton(4);
						if (mouseButton5)
						{
							result = KeyCode.Mouse4.ToString();
						}
						else
						{
							bool mouseButton6 = Input.GetMouseButton(5);
							if (mouseButton6)
							{
								result = KeyCode.Mouse5.ToString();
							}
							else
							{
								bool mouseButton7 = Input.GetMouseButton(6);
								if (mouseButton7)
								{
									result = KeyCode.Mouse6.ToString();
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x0014F410 File Offset: 0x0014D610
	private void Tab_Control_Selected_Hero(int _Parameter)
	{
		switch (_Parameter)
		{
		case 1:
			_Parameter = 1003;
			break;
		case 2:
			_Parameter = 1005;
			break;
		case 3:
			_Parameter = 1006;
			break;
		case 4:
			_Parameter = 1015;
			break;
		case 5:
			_Parameter = 1016;
			break;
		case 6:
			_Parameter = 1017;
			break;
		case 7:
			_Parameter = 1021;
			break;
		case 8:
			_Parameter = 1022;
			break;
		case 9:
			_Parameter = 1023;
			break;
		case 10:
			_Parameter = 1024;
			break;
		case 11:
			_Parameter = 1025;
			break;
		case 12:
			_Parameter = 1026;
			break;
		case 13:
			_Parameter = 1027;
			break;
		case 14:
			_Parameter = 1028;
			break;
		case 15:
			_Parameter = 1029;
			break;
		case 16:
			_Parameter = 1030;
			break;
		case 17:
			_Parameter = 1031;
			break;
		case 18:
			_Parameter = 1032;
			break;
		case 19:
			_Parameter = 1033;
			break;
		case 20:
			_Parameter = 1034;
			break;
		case 21:
			_Parameter = 1035;
			break;
		case 22:
			_Parameter = 1036;
			break;
		case 23:
			_Parameter = 1037;
			break;
		case 24:
			_Parameter = 1038;
			break;
		case 25:
			_Parameter = 1039;
			break;
		}
		this.Selected_Hero_Control_Index = _Parameter;
		bool flag = this.Selected_Hero_Control_Index == 0;
		if (flag)
		{
			this.UI_Component.Control_Content_Restore_Single_Hero_Button.gameObject.SetActive(false);
			this.UI_Component.Control_Content_Grid_Aim_Mouse_Sensitivity_Object.SetActive(false);
		}
		else
		{
			this.UI_Component.Control_Content_Restore_Single_Hero_Button.gameObject.SetActive(true);
			bool flag2 = this.Selected_Hero_Control_Index != 1003 && this.Selected_Hero_Control_Index != 1031;
			if (flag2)
			{
				this.UI_Component.Control_Content_Grid_Aim_Mouse_Sensitivity_Object.SetActive(false);
			}
			else
			{
				this.UI_Component.Control_Content_Grid_Aim_Mouse_Sensitivity_Object.SetActive(true);
			}
		}
		this.Display_Control_Data();
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x0014F642 File Offset: 0x0014D842
	private void Restore_Hero_Control_Data()
	{
		this.Controller_Component.Restore_Hero_Control_Data();
		this.Refresh_Hero_Control_List();
		this.Display_Control_Data();
		GameRoot.inst.Setting.Use_Setting(this.Selected_Hero_Control_Index);
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x0014F678 File Offset: 0x0014D878
	private void Restore_Video_Setting(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(102216, new object[0]), Language.GetLang(102219, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Restore_Video_Setting), null, null, null);
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x0014F6E5 File Offset: 0x0014D8E5
	private void Confirm_Restore_Video_Setting(object _Object)
	{
		this.Controller_Component.Restore_Video_Setting();
		this.Display_Video_Data();
		GameRoot.inst.Setting.Use_Video_Setting();
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x0014F71B File Offset: 0x0014D91B
	public void Use_Video_Setting(GameObject _Object)
	{
		GameRoot.inst.Setting.Use_Video_Setting();
		this.UI_Component.Video_Use_Setting_Button.gameObject.SetActive(false);
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x0014F748 File Offset: 0x0014D948
	private void Restore_Voice_Setting(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(102216, new object[0]), Language.GetLang(102220, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Restore_Voice_Setting), null, null, null);
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x0014F7B5 File Offset: 0x0014D9B5
	private void Confirm_Restore_Voice_Setting(object _Object)
	{
		this.Controller_Component.Restore_Voice_Setting();
		this.Display_Voice_Data();
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x0014F7DC File Offset: 0x0014D9DC
	private void Restore_Game_Setting(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(102216, new object[0]), Language.GetLang(102221, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Restore_Game_Setting), null, null, null);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x0014F849 File Offset: 0x0014DA49
	private void Confirm_Restore_Game_Setting(object _Object)
	{
		this.Controller_Component.Restore_Game_Setting();
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0014F868 File Offset: 0x0014DA68
	private void Restore_Control_Setting(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(102216, new object[0]), Language.GetLang(102222, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Restore_Control_Setting), null, null, null);
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0014F8D8 File Offset: 0x0014DAD8
	private void Confirm_Restore_Control_Setting(object _Object)
	{
		this.Controller_Component.Restore_Control_Setting();
		this.Refresh_Hero_Control_List();
		this.Display_Control_Data();
		fUIManager.GetInstance().Setting_Modify_Key_Complete("All", string.Empty);
		GameRoot.inst.Setting.Use_Setting(0);
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x0014F938 File Offset: 0x0014DB38
	private void Restore_Social_Setting(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(102216, new object[0]), Language.GetLang(102221, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Restore_Social_Setting), null, null, null);
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x0014F9A5 File Offset: 0x0014DBA5
	private void Confirm_Restore_Social_Setting(object _Object)
	{
		this.Controller_Component.Restore_Social_Setting();
		this.Display_Social_Data();
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0014F9CC File Offset: 0x0014DBCC
	public bool Set_Property(string Property_Name, object _Property_Value)
	{
		bool flag = this.Controller_Component.Set_Property_Value(this.Selected_Tab_Name, Property_Name, _Property_Value);
		bool flag2 = this.Selected_Tab_Name == "tab_Control";
		if (flag2)
		{
			this.Refresh_Hero_Control_List();
		}
		bool flag3 = flag;
		if (flag3)
		{
			GameRoot.inst.Setting.Save_Data();
		}
		return flag;
	}

	// Token: 0x040020C3 RID: 8387
	private Setting_UI _UI_Component;

	// Token: 0x040020C4 RID: 8388
	private Setting_Controller _Controller_Component;

	// Token: 0x040020C5 RID: 8389
	private string _Selected_Tab_Name = string.Empty;

	// Token: 0x040020C6 RID: 8390
	private GameObject _Selected_Tab_Object;

	// Token: 0x040020C7 RID: 8391
	private bool _Video_Advanced_State;

	// Token: 0x040020C8 RID: 8392
	private bool _Voice_On_Off_Wait_State;

	// Token: 0x040020C9 RID: 8393
	private int _Selected_Hero_Control_Index;

	// Token: 0x040020CA RID: 8394
	private string _Key_Property_Name;

	// Token: 0x040020CB RID: 8395
	private string _Key;

	// Token: 0x040020CC RID: 8396
	private float _Auto_Hidden_Modify_Content_Time;
}

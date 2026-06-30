using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TeamUtility.IO;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class Setting_Entity : MonoBehaviour
{
	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x0600231E RID: 8990 RVA: 0x001435A8 File Offset: 0x001417A8
	// (set) Token: 0x0600231F RID: 8991 RVA: 0x001435E0 File Offset: 0x001417E0
	public Setting_Entity_Video Video_Class
	{
		get
		{
			bool flag = this._Video_Class == null;
			if (flag)
			{
				this._Video_Class = this.CreateInstance<Setting_Entity_Video>("Setting_Entity_Video");
			}
			return this._Video_Class;
		}
		set
		{
			this._Video_Class = value;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x06002320 RID: 8992 RVA: 0x001435EC File Offset: 0x001417EC
	// (set) Token: 0x06002321 RID: 8993 RVA: 0x00143624 File Offset: 0x00141824
	public Setting_Entity_Voice Voice_Class
	{
		get
		{
			bool flag = this._Voice_Class == null;
			if (flag)
			{
				this._Voice_Class = this.CreateInstance<Setting_Entity_Voice>("Setting_Entity_Voice");
			}
			return this._Voice_Class;
		}
		set
		{
			this._Voice_Class = value;
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x06002322 RID: 8994 RVA: 0x00143630 File Offset: 0x00141830
	// (set) Token: 0x06002323 RID: 8995 RVA: 0x00143668 File Offset: 0x00141868
	public Setting_Entity_Game Game_Class
	{
		get
		{
			bool flag = this._Game_Class == null;
			if (flag)
			{
				this._Game_Class = this.CreateInstance<Setting_Entity_Game>("Setting_Entity_Game");
			}
			return this._Game_Class;
		}
		set
		{
			this._Game_Class = value;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06002324 RID: 8996 RVA: 0x00143674 File Offset: 0x00141874
	// (set) Token: 0x06002325 RID: 8997 RVA: 0x001436AC File Offset: 0x001418AC
	public Setting_Entity_Hero_Control Hero_Control_Class
	{
		get
		{
			bool flag = this._Hero_Control_Class == null;
			if (flag)
			{
				this._Hero_Control_Class = this.CreateInstance<Setting_Entity_Hero_Control>("Setting_Entity_Hero_Control");
			}
			return this._Hero_Control_Class;
		}
		set
		{
			this._Hero_Control_Class = value;
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x06002326 RID: 8998 RVA: 0x001436B8 File Offset: 0x001418B8
	// (set) Token: 0x06002327 RID: 8999 RVA: 0x001436F0 File Offset: 0x001418F0
	public Setting_Entity_Social Social_Class
	{
		get
		{
			bool flag = this._Social_Class == null;
			if (flag)
			{
				this._Social_Class = this.CreateInstance<Setting_Entity_Social>("Setting_Entity_Social");
			}
			return this._Social_Class;
		}
		set
		{
			this._Social_Class = value;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x06002328 RID: 9000 RVA: 0x001436FC File Offset: 0x001418FC
	// (set) Token: 0x06002329 RID: 9001 RVA: 0x0014372F File Offset: 0x0014192F
	public Setting_Entity_Video Temp_Video_Class
	{
		get
		{
			bool flag = this._Temp_Video_Class == null;
			if (flag)
			{
				this._Temp_Video_Class = this.Video_Class;
			}
			return this._Temp_Video_Class;
		}
		set
		{
			this._Temp_Video_Class = value;
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x0600232A RID: 9002 RVA: 0x0014373C File Offset: 0x0014193C
	// (set) Token: 0x0600232B RID: 9003 RVA: 0x0014376F File Offset: 0x0014196F
	public Setting_Entity_Voice Temp_Voice_Class
	{
		get
		{
			bool flag = this._Temp_Voice_Class == null;
			if (flag)
			{
				this._Temp_Voice_Class = this.Voice_Class;
			}
			return this._Temp_Voice_Class;
		}
		set
		{
			this._Temp_Voice_Class = value;
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x0600232C RID: 9004 RVA: 0x0014377C File Offset: 0x0014197C
	// (set) Token: 0x0600232D RID: 9005 RVA: 0x001437AF File Offset: 0x001419AF
	public Setting_Entity_Game Temp_Game_Class
	{
		get
		{
			bool flag = this._Temp_Game_Class == null;
			if (flag)
			{
				this._Temp_Game_Class = this.Game_Class;
			}
			return this._Temp_Game_Class;
		}
		set
		{
			this._Temp_Game_Class = value;
		}
	}

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x0600232E RID: 9006 RVA: 0x001437BC File Offset: 0x001419BC
	// (set) Token: 0x0600232F RID: 9007 RVA: 0x001437EF File Offset: 0x001419EF
	public Setting_Entity_Hero_Control Temp_Hero_Control_Class
	{
		get
		{
			bool flag = this._Temp_Hero_Control_Class == null;
			if (flag)
			{
				this._Temp_Hero_Control_Class = this.Hero_Control_Class;
			}
			return this._Temp_Hero_Control_Class;
		}
		set
		{
			this._Temp_Hero_Control_Class = value;
		}
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06002330 RID: 9008 RVA: 0x001437FC File Offset: 0x001419FC
	// (set) Token: 0x06002331 RID: 9009 RVA: 0x0014382F File Offset: 0x00141A2F
	public Setting_Entity_Social Temp_Social_Class
	{
		get
		{
			bool flag = this._Temp_Social_Class == null;
			if (flag)
			{
				this._Temp_Social_Class = this.Social_Class;
			}
			return this._Temp_Social_Class;
		}
		set
		{
			this._Temp_Social_Class = value;
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06002332 RID: 9010 RVA: 0x0014383C File Offset: 0x00141A3C
	// (set) Token: 0x06002333 RID: 9011 RVA: 0x0014386E File Offset: 0x00141A6E
	public Dictionary<string, int> KeyCode_Data
	{
		get
		{
			bool flag = this._KeyCode_Data == null;
			if (flag)
			{
				this._KeyCode_Data = new Dictionary<string, int>();
			}
			return this._KeyCode_Data;
		}
		set
		{
			this._KeyCode_Data = value;
		}
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06002334 RID: 9012 RVA: 0x00143878 File Offset: 0x00141A78
	// (set) Token: 0x06002335 RID: 9013 RVA: 0x001438A6 File Offset: 0x00141AA6
	protected ArrayList Video_Field_Name_Array
	{
		get
		{
			bool flag = this._Video_Field_Name_Array == null;
			if (flag)
			{
				this.Initialize_Video_Setting();
			}
			return this._Video_Field_Name_Array;
		}
		set
		{
			this._Video_Field_Name_Array = value;
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06002336 RID: 9014 RVA: 0x001438B0 File Offset: 0x00141AB0
	// (set) Token: 0x06002337 RID: 9015 RVA: 0x001438DE File Offset: 0x00141ADE
	protected ArrayList Voice_Field_Name_Array
	{
		get
		{
			bool flag = this._Voice_Field_Name_Array == null;
			if (flag)
			{
				this.Initialize_Voice_Setting();
			}
			return this._Voice_Field_Name_Array;
		}
		set
		{
			this._Voice_Field_Name_Array = value;
		}
	}

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06002338 RID: 9016 RVA: 0x001438E8 File Offset: 0x00141AE8
	// (set) Token: 0x06002339 RID: 9017 RVA: 0x00143916 File Offset: 0x00141B16
	protected ArrayList Game_Field_Name_Array
	{
		get
		{
			bool flag = this._Game_Field_Name_Array == null;
			if (flag)
			{
				this.Initialize_Game_Setting();
			}
			return this._Game_Field_Name_Array;
		}
		set
		{
			this._Game_Field_Name_Array = value;
		}
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x0600233A RID: 9018 RVA: 0x00143920 File Offset: 0x00141B20
	// (set) Token: 0x0600233B RID: 9019 RVA: 0x0014394E File Offset: 0x00141B4E
	public ArrayList Control_Field_Name_Array
	{
		get
		{
			bool flag = this._Control_Field_Name_Array == null;
			if (flag)
			{
				this.Initialize_Control_Setting();
			}
			return this._Control_Field_Name_Array;
		}
		set
		{
			this._Control_Field_Name_Array = value;
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x0600233C RID: 9020 RVA: 0x00143958 File Offset: 0x00141B58
	// (set) Token: 0x0600233D RID: 9021 RVA: 0x0014398A File Offset: 0x00141B8A
	public ArrayList Control_Key_Field_Name_Array
	{
		get
		{
			bool flag = this._Control_Key_Field_Name_Array == null;
			if (flag)
			{
				this._Control_Key_Field_Name_Array = new ArrayList();
			}
			return this._Control_Key_Field_Name_Array;
		}
		set
		{
			this._Control_Key_Field_Name_Array = value;
		}
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x0600233E RID: 9022 RVA: 0x00143994 File Offset: 0x00141B94
	// (set) Token: 0x0600233F RID: 9023 RVA: 0x001439C2 File Offset: 0x00141BC2
	protected ArrayList Social_Field_Name_Array
	{
		get
		{
			bool flag = this._Social_Field_Name_Array == null;
			if (flag)
			{
				this.Initialize_Social_Setting();
			}
			return this._Social_Field_Name_Array;
		}
		set
		{
			this._Social_Field_Name_Array = value;
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06002340 RID: 9024 RVA: 0x001439CC File Offset: 0x00141BCC
	// (set) Token: 0x06002341 RID: 9025 RVA: 0x00143A0D File Offset: 0x00141C0D
	public int Setting_Display_Type
	{
		get
		{
			bool flag = this._Setting_Display_Type == -1;
			if (flag)
			{
				this._Setting_Display_Type = GameRoot.inst.Setting.Video_Class.Display_Type;
			}
			return this._Setting_Display_Type;
		}
		set
		{
			this._Setting_Display_Type = value;
		}
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06002342 RID: 9026 RVA: 0x00143A18 File Offset: 0x00141C18
	// (set) Token: 0x06002343 RID: 9027 RVA: 0x00143A59 File Offset: 0x00141C59
	public int Setting_Resolution_Ratio
	{
		get
		{
			bool flag = this._Setting_Resolution_Ratio == -1;
			if (flag)
			{
				this._Setting_Resolution_Ratio = GameRoot.inst.Setting.Video_Class.Resolution_Ratio;
			}
			return this._Setting_Resolution_Ratio;
		}
		set
		{
			this._Setting_Resolution_Ratio = value;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06002344 RID: 9028 RVA: 0x00143A64 File Offset: 0x00141C64
	// (set) Token: 0x06002345 RID: 9029 RVA: 0x00143AB8 File Offset: 0x00141CB8
	public int Default_Resolution_Width
	{
		get
		{
			bool flag = this._Default_Resolution_Width < 1280;
			if (flag)
			{
				this._Default_Resolution_Width = 1280;
			}
			bool flag2 = this._Default_Resolution_Width > 1920;
			if (flag2)
			{
				this._Default_Resolution_Width = 1920;
			}
			return this._Default_Resolution_Width;
		}
		set
		{
			this._Default_Resolution_Width = value;
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x06002346 RID: 9030 RVA: 0x00143AC4 File Offset: 0x00141CC4
	// (set) Token: 0x06002347 RID: 9031 RVA: 0x00143B18 File Offset: 0x00141D18
	public int Default_Resolution_Height
	{
		get
		{
			bool flag = this._Default_Resolution_Height < 720;
			if (flag)
			{
				this._Default_Resolution_Height = 720;
			}
			bool flag2 = this._Default_Resolution_Height > 1080;
			if (flag2)
			{
				this._Default_Resolution_Height = 1080;
			}
			return this._Default_Resolution_Height;
		}
		set
		{
			this._Default_Resolution_Height = value;
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06002348 RID: 9032 RVA: 0x00143B24 File Offset: 0x00141D24
	// (set) Token: 0x06002349 RID: 9033 RVA: 0x00143B3C File Offset: 0x00141D3C
	public Setting_View View_Component
	{
		get
		{
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x0600234A RID: 9034 RVA: 0x00143B48 File Offset: 0x00141D48
	// (set) Token: 0x0600234B RID: 9035 RVA: 0x00143B60 File Offset: 0x00141D60
	public bool Load_Setting_Data_State
	{
		get
		{
			return this._Load_Setting_Data_State;
		}
		set
		{
			this._Load_Setting_Data_State = value;
		}
	}

	// Token: 0x0600234C RID: 9036 RVA: 0x00143B6A File Offset: 0x00141D6A
	public void Start()
	{
		this.Initialize_Setting();
	}

	// Token: 0x0600234D RID: 9037 RVA: 0x00143B74 File Offset: 0x00141D74
	protected void Initialize_Setting()
	{
		this.Initialize_Video_Setting();
		this.Initialize_Voice_Setting();
		this.Initialize_Game_Setting();
		this.Initialize_Control_Setting();
		this.Initialize_Social_Setting();
		this.Create_KeyCode_Data();
	}

	// Token: 0x0600234E RID: 9038 RVA: 0x00143BA4 File Offset: 0x00141DA4
	public void Create_KeyCode_Data()
	{
		this.KeyCode_Data = new Dictionary<string, int>();
		FieldInfo[] fields = typeof(KeyCode).GetFields(BindingFlags.Static | BindingFlags.Public);
		FieldInfo[] array = fields;
		foreach (FieldInfo fieldInfo in array)
		{
			this.KeyCode_Data.Add(fieldInfo.Name, (int)fieldInfo.GetValue(null));
		}
	}

	// Token: 0x0600234F RID: 9039 RVA: 0x00143C0C File Offset: 0x00141E0C
	public void Open_Setting_Option(GameObject _Object)
	{
		bool flag = (Setting_OptionCon.Instance != null && Setting_OptionCon.Instance.visible) || (Setting_Option_HallCon.Instance != null && Setting_Option_HallCon.Instance.visible);
		if (flag)
		{
			this.Close_Setting_Option(null);
		}
		else
		{
			bool flag2 = fGameManager.GetInstance().nNowScnID < 1;
			if (flag2)
			{
				fUIManager.GetInstance().ShowWin("Setting_Option_Hall", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Load_Setting_Option_Complete), null, true);
			}
			else
			{
				fUIManager.GetInstance().ShowWin("Setting_Option", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Load_Setting_Option_Complete), null, true);
			}
		}
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x00143CB8 File Offset: 0x00141EB8
	private void Load_Setting_Option_Complete(string _Url, GameObject _UI_Object, object _Parameters)
	{
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Close").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Close_Setting_Option);
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Close").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Setting").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Open_Setting);
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Setting").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_BBS").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Open_BBS);
		EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_BBS").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		bool flag = _UI_Object.name == "Setting_Option";
		if (flag)
		{
			EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Return").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Return_Hall);
			EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Return").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			bool isSkyLadderMatch = SkyLadderManager.GetInstance().isSkyLadderMatch;
			if (isSkyLadderMatch)
			{
				RectTransform component = _UI_Object.transform.Find("Center").GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(268f, 120f);
				component = _UI_Object.transform.Find("Center/Background").GetComponent<RectTransform>();
				component.localPosition = new Vector3(0f, -17.8f, 0f);
				component.sizeDelta = new Vector2(248f, 60f);
				_UI_Object.transform.Find("Center/btn_Setting").transform.localPosition = new Vector3(0f, -18.6f, 0f);
				_UI_Object.transform.Find("Center/btn_Return").gameObject.SetActive(false);
			}
			else
			{
				RectTransform component = _UI_Object.transform.Find("Center").GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(268f, 204f);
				component = _UI_Object.transform.Find("Center/Background").GetComponent<RectTransform>();
				component.localPosition = new Vector3(0f, -22.29999f, 0f);
				component.sizeDelta = new Vector2(248f, 117f);
				_UI_Object.transform.Find("Center/btn_Setting").transform.localPosition = new Vector3(0f, -2f, 0f);
				_UI_Object.transform.Find("Center/btn_Return").gameObject.SetActive(true);
			}
		}
		else
		{
			bool flag2 = _UI_Object.name == "Setting_Option_Hall";
			if (flag2)
			{
				EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Quit").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Quit_Game);
				EventTriggerListener.Get(_UI_Object.transform.Find("Center/btn_Quit").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			}
		}
		bool flag3 = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().playerZhunxingMe != null;
		if (flag3)
		{
			fGameManager.GetInstance().getPlayer().playerZhunxingMe.zhunxingshowOrHide(false);
		}
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x0014407C File Offset: 0x0014227C
	public void Close_Setting_Option(GameObject _Object)
	{
		bool flag = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().playerZhunxingMe != null;
		if (flag)
		{
			fGameManager.GetInstance().getPlayer().playerZhunxingMe.zhunxingshowOrHide(true);
		}
		bool flag2 = Setting_OptionCon.Instance != null && Setting_OptionCon.Instance.visible;
		if (flag2)
		{
			Setting_OptionCon.Instance.Close();
			bool flag3 = fGameManager.m_nUserType == 1 && CameraControl.Instance != null;
			if (flag3)
			{
				HeroPlayer.isMouseShow = false;
				HeroPlayer.isPlayerControl = true;
			}
		}
		bool flag4 = Setting_Option_HallCon.Instance != null && Setting_Option_HallCon.Instance.visible;
		if (flag4)
		{
			Setting_Option_HallCon.Instance.Close();
			bool flag5 = fGameManager.m_nUserType == 1 && CameraControl.Instance != null;
			if (flag5)
			{
				HeroPlayer.isMouseShow = false;
				HeroPlayer.isPlayerControl = true;
			}
		}
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x00144174 File Offset: 0x00142374
	public void Open_Setting(GameObject _Object)
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(51012, new object[0]), Language.GetLang(51011, new object[0]), Language.GetLang(51013, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.Close_Setting_Option(null);
			bool flag = fGameManager.m_nUserType == 1;
			if (flag)
			{
				HeroPlayer.isMouseShow = true;
				HeroPlayer.isPlayerControl = false;
			}
			fUIManager.GetInstance().ShowWin("Setting", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Load_Setting_UI_Complete), null, true);
		}
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x00144218 File Offset: 0x00142418
	private void Load_Setting_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		UIDepth component = _UI_Object.GetComponent<UIDepth>();
		component.order = 97;
		component.isUI = true;
		component.isUpdata = true;
		this.View_Component = _UI_Object.GetComponent<Setting_View>();
		bool flag = this.View_Component == null;
		if (flag)
		{
			this.View_Component = _UI_Object.AddComponent<Setting_View>();
		}
		this.View_Component.Display_Video_Data();
		this.View_Component.Display_Voice_Data();
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x00144287 File Offset: 0x00142487
	private void Open_BBS(GameObject _Object)
	{
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x0014428C File Offset: 0x0014248C
	private void Return_Hall(GameObject _Object)
	{
		string stip = string.Empty;
		this.Close_Setting_Option(null);
		bool flag = fGameManager.GetInstance().nNowScnID != 5205 && fGameManager.GetInstance().nNowScnID != 5202 && PlayerDataManager.getInstance().HUserData.m_nRoomUID == 0 && !SkyLadderManager.GetInstance().isSkyLadderMatch && MatchManager.GetInstance().m_nMatchType != 4;
		if (flag)
		{
			stip = ((MatchManager.GetInstance().m_nMatchType == 3) ? Language.GetLang(1506, new object[0]) : ((PlayerDataManager.getInstance().HUserData.m_nPunNum >= 45) ? Language.GetLang(1503, new object[0]) : Language.GetLang(1504, new object[0])));
		}
		bool flag2 = SkyLadderManager.GetInstance().isSkyLadderMatch && SkyLadderManager.GetInstance().m_nSkyLadderState != 4 && !SkyLadderManager.GetInstance().isSkyLadderOver;
		if (flag2)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(24015, new object[0]), Language.GetLang(24016, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Return_Hall), null, null, null);
		}
		else
		{
			bool flag3 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag3)
			{
				bool flag4 = TeamManager.GetInstance().m_nCaptain == 1;
				if (flag4)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(140001, new object[0]), Language.GetLang(40022, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), stip, new TipManager.TipOKDelegate(this.Confirm_Return_Hall), null, null, null);
				}
				else
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(140001, new object[0]), Language.GetLang(40023, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), stip, new TipManager.TipOKDelegate(this.Confirm_Return_Hall), null, null, null);
				}
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(140001, new object[0]), Language.GetLang(140002, new object[0]), Language.GetLang(102217, new object[0]), Language.GetLang(102218, new object[0]), stip, new TipManager.TipOKDelegate(this.Confirm_Return_Hall), null, null, null);
			}
		}
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x0014452C File Offset: 0x0014272C
	private void Confirm_Return_Hall(object _Object)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
			{
				1,
				0
			});
		}
		bool flag2 = fGameManager.Heroplayer != null;
		if (flag2)
		{
			fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
			{
				1,
				0
			});
		}
		this.Close_Setting_Option(null);
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			PlayerLearnManager.GetInstance().CloseLearn();
		}
		bool flag3 = TeamManager.GetInstance().m_nCaptain != 1;
		if (flag3)
		{
			TeamManager.GetInstance().IsInitTeam = 1;
		}
		fUIManager.GetInstance().GotoHall(0);
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x00144600 File Offset: 0x00142800
	private void Quit_Game(GameObject _Object)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(46135, new object[0]), Language.GetLang(46137, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.Confirm_Quit_Game), null, null, null);
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x00144670 File Offset: 0x00142870
	private void Confirm_Quit_Game(object _Object)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		bool flag2 = fGameManager.Heroplayer != null;
		if (flag2)
		{
			fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		QualitySettings.SetQualityLevel(2);
		Application.Quit();
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x00144708 File Offset: 0x00142908
	public void Save_Data()
	{
		bool flag = fGameManager.GetInstance() != null && !(GameRoot.inst.Setting == null);
		if (flag)
		{
			ArrayList data = GameRoot.inst.Setting.Get_Data();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(3010013);
			arrayList.Add(data);
			fGameManager.LobbyPlayer.SendMsgToHallServer(3010000, arrayList);
		}
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x0014477D File Offset: 0x0014297D
	private void Initialize_Video_Setting()
	{
		this.Fill_Field(this.Video_Class, ref this._Video_Field_Name_Array);
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x00144793 File Offset: 0x00142993
	private void Initialize_Voice_Setting()
	{
		this.Fill_Field(this.Voice_Class, ref this._Voice_Field_Name_Array);
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x001447A9 File Offset: 0x001429A9
	private void Initialize_Game_Setting()
	{
		this.Fill_Field(this.Game_Class, ref this._Game_Field_Name_Array);
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x001447BF File Offset: 0x001429BF
	private void Initialize_Control_Setting()
	{
		this.Fill_Field(this.Hero_Control_Class.Hero_Control_Data[0], ref this._Control_Field_Name_Array);
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x001447E0 File Offset: 0x001429E0
	private void Initialize_Social_Setting()
	{
		this.Fill_Field(this.Social_Class, ref this._Social_Field_Name_Array);
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x001447F8 File Offset: 0x001429F8
	protected T CreateInstance<T>(string _Class_Name)
	{
		Type type = Type.GetType(_Class_Name);
		object obj = Activator.CreateInstance(type, true);
		return (T)((object)obj);
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x00144820 File Offset: 0x00142A20
	protected void Fill_Field(object _Class, ref ArrayList _Parameters)
	{
		_Parameters = new ArrayList();
		foreach (PropertyInfo propertyInfo in _Class.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
		{
			_Parameters.Add(propertyInfo.Name);
			bool flag = _Class.GetType() == typeof(Setting_Entity_Control);
			if (flag)
			{
				string[] array = propertyInfo.Name.Split(new char[]
				{
					'_'
				});
				bool flag2 = !(array[0].ToString() != "Key");
				if (flag2)
				{
					this.Control_Key_Field_Name_Array.Add(propertyInfo);
				}
			}
		}
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x001448D0 File Offset: 0x00142AD0
	public string Get_Property_Describe(object _Class, string _Property_Name)
	{
		Type type = _Class.GetType();
		object[] customAttributes = type.GetProperty(_Property_Name).GetCustomAttributes(typeof(DescriptionAttribute), true);
		bool flag = customAttributes.Length != 0;
		string result;
		if (flag)
		{
			result = ((DescriptionAttribute)customAttributes[0]).Description;
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x00144924 File Offset: 0x00142B24
	public object Get_Property_Value(object _Class, string _Property_Name)
	{
		Type type = _Class.GetType();
		PropertyInfo property = type.GetProperty(_Property_Name);
		bool flag = property == null;
		object result;
		if (flag)
		{
			result = null;
		}
		else
		{
			object value = property.GetValue(_Class, null);
			bool flag2 = property.PropertyType == typeof(string);
			if (flag2)
			{
				result = value.ToString();
			}
			else
			{
				bool flag3 = property.PropertyType == typeof(int);
				if (flag3)
				{
					result = int.Parse(value.ToString());
				}
				else
				{
					bool flag4 = property.PropertyType == typeof(float);
					if (flag4)
					{
						result = float.Parse(value.ToString());
					}
					else
					{
						bool flag5 = property.PropertyType == typeof(bool);
						if (flag5)
						{
							result = (((bool)value) ? 1 : 0);
						}
						else
						{
							result = value;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x00144A18 File Offset: 0x00142C18
	public bool Set_Property_Value(object _Class, string _Property_Name, object _Property_Value)
	{
		Type type = _Class.GetType();
		PropertyInfo property = type.GetProperty(_Property_Name);
		bool flag = property == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = property.PropertyType == typeof(string);
			if (flag2)
			{
				property.SetValue(_Class, _Property_Value.ToString(), null);
			}
			else
			{
				bool flag3 = property.PropertyType == typeof(int);
				if (flag3)
				{
					property.SetValue(_Class, int.Parse(_Property_Value.ToString()), null);
				}
				else
				{
					bool flag4 = property.PropertyType == typeof(float);
					if (flag4)
					{
						property.SetValue(_Class, float.Parse(_Property_Value.ToString()), null);
					}
					else
					{
						bool flag5 = property.PropertyType == typeof(bool);
						if (flag5)
						{
							property.SetValue(_Class, int.Parse(_Property_Value.ToString()) == 1, null);
						}
						else
						{
							property.SetValue(_Class, _Property_Value, null);
						}
					}
				}
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x00144B2C File Offset: 0x00142D2C
	public void Set_Data(string _Parameter)
	{
		string[] array = _Parameter.Split(new char[]
		{
			'&'
		});
		bool flag = array[1].ToString() != string.Empty;
		if (flag)
		{
			this.Set_Data(this.Voice_Class, this.Voice_Field_Name_Array, array[1].ToString());
		}
		else
		{
			GameRoot.inst.Setting.Voice_Class.Initialization();
		}
		bool flag2 = array[3].ToString() != string.Empty;
		if (flag2)
		{
			this.Hero_Control_Class.Set_Data(this.Control_Field_Name_Array, array[3].ToString());
		}
		bool flag3 = array[4].ToString() != string.Empty;
		if (flag3)
		{
			this.Set_Data(this.Social_Class, this.Social_Field_Name_Array, array[4].ToString());
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x00144C00 File Offset: 0x00142E00
	public void Set_Data(object _Class, ArrayList _Field_Array, string _Parameter)
	{
		string[] array = _Parameter.Split(new char[]
		{
			','
		});
		for (int i = 0; i < _Field_Array.Count; i++)
		{
			bool flag = i < array.Length;
			if (flag)
			{
				this.Set_Property_Value(_Class, _Field_Array[i].ToString(), array[i].ToString());
			}
		}
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x00144C64 File Offset: 0x00142E64
	public ArrayList Get_Data()
	{
		return new ArrayList
		{
			this.Get_Data(this.Video_Class, this.Video_Field_Name_Array),
			this.Get_Data(this.Voice_Class, this.Voice_Field_Name_Array),
			this.Get_Data(this.Game_Class, this.Game_Field_Name_Array),
			this.Hero_Control_Class.Get_Data(this.Control_Field_Name_Array),
			this.Get_Data(this.Social_Class, this.Social_Field_Name_Array)
		};
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x00144CFC File Offset: 0x00142EFC
	public string Get_Data(object _Class, ArrayList _Field_Array)
	{
		string text = string.Empty;
		for (int i = 0; i < _Field_Array.Count; i++)
		{
			text = text + this.Get_Property_Value(_Class, _Field_Array[i].ToString()) + ",";
		}
		bool flag = text != string.Empty;
		if (flag)
		{
			text = text.Substring(0, text.Length - 1);
		}
		return text;
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x00144D74 File Offset: 0x00142F74
	public void Use_Video_Setting()
	{
		this.Setting_Display_Type = GameRoot.inst.Setting.Video_Class.Display_Type;
		this.Setting_Resolution_Ratio = GameRoot.inst.Setting.Video_Class.Resolution_Ratio;
		int num;
		int num2;
		switch (this.Setting_Resolution_Ratio)
		{
		case 0:
			num = 1280;
			num2 = 720;
			break;
		case 1:
			num = 1366;
			num2 = 768;
			break;
		case 2:
			num = 1600;
			num2 = 900;
			break;
		case 3:
			num = 1920;
			num2 = 1080;
			break;
		default:
			num = GameRoot.inst.Setting.Default_Resolution_Width;
			num2 = GameRoot.inst.Setting.Default_Resolution_Height;
			break;
		}
		bool flag = num > GameRoot.inst.Setting.Default_Resolution_Width || num2 > GameRoot.inst.Setting.Default_Resolution_Height;
		if (flag)
		{
			this.Setting_Resolution_Ratio = GameRoot.inst.Setting.Video_Class.Get_Default_Resolution_Ratio();
			switch (this.Setting_Resolution_Ratio)
			{
			case 0:
				num = 1280;
				num2 = 720;
				break;
			case 1:
				num = 1366;
				num2 = 768;
				break;
			case 2:
				num = 1600;
				num2 = 900;
				break;
			case 3:
				num = 1920;
				num2 = 1080;
				break;
			default:
				num = 1280;
				num2 = 720;
				break;
			}
		}
		Screen.SetResolution(num, num2, this.Setting_Display_Type == 0);
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x00144F00 File Offset: 0x00143100
	public void Use_Setting(int _Hero_Index)
	{
		bool flag = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().userModelFps != null;
		if (flag)
		{
			Setting_Quality_FPS_Model component = fGameManager.GetInstance().getPlayer().userModelFps.GetComponent<Setting_Quality_FPS_Model>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
			CHeroWeapon component2 = fGameManager.GetInstance().getPlayer().userModelFps.GetComponent<CHeroWeapon>();
			bool flag3 = component2 != null;
			if (flag3)
			{
				component2.SetHeroWeapon(1, -1, 0);
			}
		}
		bool flag4 = fGameManager.m_nUserType == 1 && CameraControl.Instance != null && CameraControl.Instance.controlCamera != null;
		if (flag4)
		{
			Setting_Quality_Camera component3 = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Camera>();
			bool flag5 = component3 != null;
			if (flag5)
			{
				component3.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
			Setting_Quality_Particle component4 = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Particle>();
			bool flag6 = component4 != null;
			if (flag6)
			{
				component4.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
		}
		Setting_Entity_Control setting_Entity_Control = this.Hero_Control_Class.Get_Hero_Control_Data(_Hero_Index);
		this.Use_Key_Setting(setting_Entity_Control);
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x00145070 File Offset: 0x00143270
	public void Use_Key_Setting(Setting_Entity_Control _Setting_Entity_Control)
	{
		AxisConfiguration axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Mouse X");
		axisConfiguration.sensitivity = 0.1f;
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Mouse Y");
		axisConfiguration.sensitivity = 0.1f;
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Aim Mouse X");
		axisConfiguration.sensitivity = 0.1f;
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Aim Mouse Y");
		axisConfiguration.sensitivity = 0.1f;
		bool flag = fGameManager.m_nUserType == 1 && CameraControl.Instance != null;
		if (flag)
		{
			CameraControl.Instance.ChangeMouseSensitivity();
			CameraControl.Instance.ChangeMouseAimSensitivity();
		}
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Mouse X");
		axisConfiguration.invert = (_Setting_Entity_Control.Mouse_Inversion == 1);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Mouse Y");
		axisConfiguration.invert = (_Setting_Entity_Control.Mouse_Inversion == 1);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Aim Mouse X");
		axisConfiguration.invert = (_Setting_Entity_Control.Mouse_Inversion == 1);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Aim Mouse Y");
		axisConfiguration.invert = (_Setting_Entity_Control.Mouse_Inversion == 1);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Vertical");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Forward, axisConfiguration.name);
		axisConfiguration.negative = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Backward, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Horizontal");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Right, axisConfiguration.name);
		axisConfiguration.negative = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Left, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Jump");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Jump, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Skill1");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Skill_1, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Skill2");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Skill_2, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "superskill");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Skill_3, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "WeaponFirst");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Use_Weapon_1, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "WeaponSec");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Use_Weapon_2, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Fight");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Melee_Attack, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Paint");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Spray_Paint, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Improve_Quality");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Improve_Quality, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Reduce_Quality");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Reduce_Quality, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Reload");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Reload, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Replace_Hero");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Replace_Hero, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Score_Window");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Score_Window, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Menu");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Menu, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Thank");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Thank, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Expression");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Expression, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Roger");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Roger, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Hello");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Hello, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Need_Help");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Need_Help, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Ultimate_Skill_State");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Ultimate_Skill_State, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Interact_Collection");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Interact_Collection, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Reply_Whisper");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Reply_Whisper, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Check_Hero_Information");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Check_Hero_Information, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Key_Voice_Chat");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Voice_Chat, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Key_Microphone_Mute_On_Off");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Microphone_Mute_On_Off, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Key_Open_Chat_Window");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Open_Chat_Window, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Key_Team_Chat_On_Off");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Team_Chat_On_Off, axisConfiguration.name);
		axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, "Key_Game_Chat_On_Off");
		axisConfiguration.positive = (KeyCode)this.Get_KeyCode(_Setting_Entity_Control.Key_Game_Chat_On_Off, axisConfiguration.name);
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x001455E4 File Offset: 0x001437E4
	public int Get_KeyCode(string _Key, string _Axis_Name)
	{
		bool flag = _Key == string.Empty;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = !this.KeyCode_Data.ContainsKey(_Key);
			if (flag2)
			{
				ArrayList arrayList = this.Combination_Key(_Key);
				_Key = arrayList[1].ToString();
				bool flag3 = _Key == string.Empty;
				if (flag3)
				{
					return 0;
				}
				bool flag4 = bool.Parse(arrayList[0].ToString());
				if (flag4)
				{
					AxisConfiguration axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, _Axis_Name);
					bool flag5 = axisConfiguration != null;
					if (flag5)
					{
						axisConfiguration.description = "Alt";
					}
				}
			}
			else
			{
				AxisConfiguration axisConfiguration = InputManager.GetAxisConfiguration(PlayerID.One, _Axis_Name);
				bool flag6 = axisConfiguration != null;
				if (flag6)
				{
					axisConfiguration.description = string.Empty;
				}
			}
			result = this.KeyCode_Data[_Key];
		}
		return result;
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x001456C4 File Offset: 0x001438C4
	public ArrayList Combination_Key(string _Key)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = false;
		_Key = _Key.Trim();
		string[] array = _Key.Split(new char[]
		{
			'+'
		});
		bool flag2 = array.Length > 1;
		if (flag2)
		{
			flag = true;
		}
		bool flag3 = array.Length < 2;
		if (flag3)
		{
			arrayList.Add(false);
			arrayList.Add(string.Empty);
		}
		else
		{
			arrayList.Add(flag);
			arrayList.Add(array[1].ToString().Trim());
		}
		return arrayList;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x00145758 File Offset: 0x00143958
	public string Get_Key_Describe(string _Value)
	{
		string[] array = _Value.Split(new char[]
		{
			'+'
		});
		string text = string.Empty;
		int i = 0;
		while (i < array.Length)
		{
			string text2 = array[i].Trim();
			if (!true)
			{
			}
			uint num = PrivateImplementationDetails.ComputeStringHash(text2);
			string str;
			if (num <= 1617156697U)
			{
				if (num <= 823167237U)
				{
					if (num <= 739279142U)
					{
						if (num <= 257323198U)
						{
							if (num != 75071339U)
							{
								if (num != 211247592U)
								{
									if (num != 257323198U)
									{
										goto IL_F8F;
									}
									if (!(text2 == "Semicolon"))
									{
										goto IL_F8F;
									}
									str = text + ";";
								}
								else
								{
									if (!(text2 == "KeypadPeriod"))
									{
										goto IL_F8F;
									}
									str = text + Language.GetLang(102114, new object[0]) + " Del";
								}
							}
							else
							{
								if (!(text2 == "LeftAlt"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102112, new object[0]);
							}
						}
						else if (num <= 705723904U)
						{
							if (num != 651038163U)
							{
								if (num != 705723904U)
								{
									goto IL_F8F;
								}
								if (!(text2 == "Keypad1"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102114, new object[0]) + " 1";
							}
							else
							{
								if (!(text2 == "RightBracket"))
								{
									goto IL_F8F;
								}
								str = text + "]";
							}
						}
						else if (num != 722501523U)
						{
							if (num != 739279142U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Keypad3"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " 3";
						}
						else
						{
							if (!(text2 == "Keypad0"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " 0";
						}
					}
					else if (num <= 789611999U)
					{
						if (num != 756056761U)
						{
							if (num != 772834380U)
							{
								if (num != 789611999U)
								{
									goto IL_F8F;
								}
								if (!(text2 == "Keypad4"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102114, new object[0]) + " 4";
							}
							else
							{
								if (!(text2 == "Keypad5"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102114, new object[0]) + " 5";
							}
						}
						else
						{
							if (!(text2 == "Keypad2"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " 2";
						}
					}
					else if (num <= 806389618U)
					{
						if (num != 798719728U)
						{
							if (num != 806389618U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Keypad7"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " 7";
						}
						else
						{
							if (!(text2 == "Mouse4"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102105, new object[0]);
						}
					}
					else if (num != 815497347U)
					{
						if (num != 823167237U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Keypad6"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102114, new object[0]) + " 6";
					}
					else
					{
						if (!(text2 == "Mouse5"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102106, new object[0]);
					}
				}
				else if (num <= 899385442U)
				{
					if (num <= 850645478U)
					{
						if (num != 832274966U)
						{
							if (num != 839944856U)
							{
								if (num != 850645478U)
								{
									goto IL_F8F;
								}
								if (!(text2 == "RightArrow"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102118, new object[0]);
							}
							else
							{
								if (!(text2 == "Keypad9"))
								{
									goto IL_F8F;
								}
								str = text + Language.GetLang(102114, new object[0]) + " 9";
							}
						}
						else
						{
							if (!(text2 == "Mouse6"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102107, new object[0]);
						}
					}
					else if (num <= 865830204U)
					{
						if (num != 856722475U)
						{
							if (num != 865830204U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Mouse0"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102101, new object[0]);
						}
						else
						{
							if (!(text2 == "Keypad8"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " 8";
						}
					}
					else if (num != 882607823U)
					{
						if (num != 899385442U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Mouse2"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102103, new object[0]);
					}
					else
					{
						if (!(text2 == "Mouse1"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102102, new object[0]);
					}
				}
				else if (num <= 1258159639U)
				{
					if (num <= 1050238388U)
					{
						if (num != 916163061U)
						{
							if (num != 1050238388U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Equals"))
							{
								goto IL_F8F;
							}
							str = text + "=";
						}
						else
						{
							if (!(text2 == "Mouse3"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102104, new object[0]);
						}
					}
					else if (num != 1231278590U)
					{
						if (num != 1258159639U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Backslash"))
						{
							goto IL_F8F;
						}
						str = text + "\\";
					}
					else
					{
						if (!(text2 == "RightControl"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102111, new object[0]);
					}
				}
				else if (num <= 1522423415U)
				{
					if (num != 1428210068U)
					{
						if (num != 1522423415U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Quote"))
						{
							goto IL_F8F;
						}
						str = text + "''";
					}
					else
					{
						if (!(text2 == "LeftShift"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102108, new object[0]);
					}
				}
				else if (num != 1565561622U)
				{
					if (num != 1617156697U)
					{
						goto IL_F8F;
					}
					if (!(text2 == "LeftCommand"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102122, new object[0]);
				}
				else
				{
					if (!(text2 == "DownArrow"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102116, new object[0]);
				}
			}
			else if (num <= 2785818529U)
			{
				if (num <= 2434225852U)
				{
					if (num <= 1732852044U)
					{
						if (num != 1629542348U)
						{
							if (num != 1706424088U)
							{
								if (num != 1732852044U)
								{
									goto IL_F8F;
								}
								if (!(text2 == "LeftBracket"))
								{
									goto IL_F8F;
								}
								str = text + "[";
							}
							else
							{
								if (!(text2 == "Comma"))
								{
									goto IL_F8F;
								}
								str = text + "<";
							}
						}
						else
						{
							if (!(text2 == "Backspace"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102119, new object[0]);
						}
					}
					else if (num <= 2267317284U)
					{
						if (num != 1898928778U)
						{
							if (num != 2267317284U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Period"))
							{
								goto IL_F8F;
							}
							str = text + ">";
						}
						else
						{
							if (!(text2 == "Slash"))
							{
								goto IL_F8F;
							}
							str = text + "?";
						}
					}
					else if (num != 2267893868U)
					{
						if (num != 2434225852U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "RightAlt"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102113, new object[0]);
					}
					else
					{
						if (!(text2 == "KeypadDivide"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102114, new object[0]) + " /";
					}
				}
				else if (num <= 2651597577U)
				{
					if (num <= 2634819958U)
					{
						if (num != 2600408491U)
						{
							if (num != 2634819958U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Alpha9"))
							{
								goto IL_F8F;
							}
							str = text + "9";
						}
						else
						{
							if (!(text2 == "KeypadMinus"))
							{
								goto IL_F8F;
							}
							str = text + Language.GetLang(102114, new object[0]) + " -";
						}
					}
					else if (num != 2638687865U)
					{
						if (num != 2651597577U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Alpha8"))
						{
							goto IL_F8F;
						}
						str = text + "8";
					}
					else
					{
						if (!(text2 == "KeypadMultiply"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102114, new object[0]) + " *";
					}
				}
				else if (num <= 2752263291U)
				{
					if (num != 2735485672U)
					{
						if (num != 2752263291U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Alpha2"))
						{
							goto IL_F8F;
						}
						str = text + "2";
					}
					else
					{
						if (!(text2 == "Alpha3"))
						{
							goto IL_F8F;
						}
						str = text + "3";
					}
				}
				else if (num != 2769040910U)
				{
					if (num != 2785818529U)
					{
						goto IL_F8F;
					}
					if (!(text2 == "Alpha0"))
					{
						goto IL_F8F;
					}
					str = text + "0";
				}
				else
				{
					if (!(text2 == "Alpha1"))
					{
						goto IL_F8F;
					}
					str = text + "1";
				}
			}
			else if (num <= 3082514982U)
			{
				if (num <= 2836151386U)
				{
					if (num != 2802596148U)
					{
						if (num != 2819373767U)
						{
							if (num != 2836151386U)
							{
								goto IL_F8F;
							}
							if (!(text2 == "Alpha5"))
							{
								goto IL_F8F;
							}
							str = text + "5";
						}
						else
						{
							if (!(text2 == "Alpha6"))
							{
								goto IL_F8F;
							}
							str = text + "6";
						}
					}
					else
					{
						if (!(text2 == "Alpha7"))
						{
							goto IL_F8F;
						}
						str = text + "7";
					}
				}
				else if (num <= 2852929005U)
				{
					if (num != 2848837449U)
					{
						if (num != 2852929005U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Alpha4"))
						{
							goto IL_F8F;
						}
						str = text + "4";
					}
					else
					{
						if (!(text2 == "LeftArrow"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102117, new object[0]);
					}
				}
				else if (num != 3036628469U)
				{
					if (num != 3082514982U)
					{
						goto IL_F8F;
					}
					if (!(text2 == "Escape"))
					{
						goto IL_F8F;
					}
					str = text + "Esc";
				}
				else
				{
					if (!(text2 == "LeftControl"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102110, new object[0]);
				}
			}
			else if (num <= 3422663135U)
			{
				if (num <= 3250860581U)
				{
					if (num != 3247849247U)
					{
						if (num != 3250860581U)
						{
							goto IL_F8F;
						}
						if (!(text2 == "Space"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102121, new object[0]);
					}
					else
					{
						if (!(text2 == "RightApple"))
						{
							goto IL_F8F;
						}
						str = text + Language.GetLang(102123, new object[0]);
					}
				}
				else if (num != 3388260431U)
				{
					if (num != 3422663135U)
					{
						goto IL_F8F;
					}
					if (!(text2 == "Return"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102120, new object[0]);
				}
				else
				{
					if (!(text2 == "Minus"))
					{
						goto IL_F8F;
					}
					str = text + "-";
				}
			}
			else if (num <= 3583141561U)
			{
				if (num != 3579990273U)
				{
					if (num != 3583141561U)
					{
						goto IL_F8F;
					}
					if (!(text2 == "UpArrow"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102115, new object[0]);
				}
				else
				{
					if (!(text2 == "KeypadPlus"))
					{
						goto IL_F8F;
					}
					str = text + Language.GetLang(102114, new object[0]) + " +";
				}
			}
			else if (num != 3592460967U)
			{
				if (num != 3818333214U)
				{
					goto IL_F8F;
				}
				if (!(text2 == "BackQuote"))
				{
					goto IL_F8F;
				}
				str = text + "~";
			}
			else
			{
				if (!(text2 == "RightShift"))
				{
					goto IL_F8F;
				}
				str = text + Language.GetLang(102109, new object[0]);
			}
			IL_FA0:
			if (!true)
			{
			}
			text = str + " + ";
			i++;
			continue;
			IL_F8F:
			str = text + array[i].Trim();
			goto IL_FA0;
		}
		return text.Substring(0, text.Length - 3);
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x00146740 File Offset: 0x00144940
	public void Ignore(GameObject _GameObject)
	{
		PlayerPrefs.SetString("Ignore_Setting_Up_Tips", DateTime.Now.AddDays(15.0).ToString());
		bool flag = Setting_TipsCon.Instance != null;
		if (flag)
		{
			Setting_TipsCon.Instance.Close();
		}
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x00146794 File Offset: 0x00144994
	public void Receive_Message(int _Message_ID, ArrayList _Parameters)
	{
		if (_Message_ID != 3010019)
		{
			if (_Message_ID == 3010020)
			{
				GameRoot.inst.Setting_FPS.Detection_Improve_Quality();
			}
		}
		else
		{
			GameRoot.inst.Setting_FPS.Statistic_FPS();
		}
	}

	// Token: 0x04001FFF RID: 8191
	private Setting_Entity_Video _Video_Class;

	// Token: 0x04002000 RID: 8192
	private Setting_Entity_Voice _Voice_Class;

	// Token: 0x04002001 RID: 8193
	private Setting_Entity_Game _Game_Class;

	// Token: 0x04002002 RID: 8194
	private Setting_Entity_Hero_Control _Hero_Control_Class;

	// Token: 0x04002003 RID: 8195
	private Setting_Entity_Social _Social_Class;

	// Token: 0x04002004 RID: 8196
	private Setting_Entity_Video _Temp_Video_Class;

	// Token: 0x04002005 RID: 8197
	private Setting_Entity_Voice _Temp_Voice_Class;

	// Token: 0x04002006 RID: 8198
	private Setting_Entity_Game _Temp_Game_Class;

	// Token: 0x04002007 RID: 8199
	private Setting_Entity_Hero_Control _Temp_Hero_Control_Class;

	// Token: 0x04002008 RID: 8200
	private Setting_Entity_Social _Temp_Social_Class;

	// Token: 0x04002009 RID: 8201
	private Dictionary<string, int> _KeyCode_Data;

	// Token: 0x0400200A RID: 8202
	private ArrayList _Video_Field_Name_Array;

	// Token: 0x0400200B RID: 8203
	private ArrayList _Voice_Field_Name_Array;

	// Token: 0x0400200C RID: 8204
	private ArrayList _Game_Field_Name_Array;

	// Token: 0x0400200D RID: 8205
	private ArrayList _Control_Field_Name_Array;

	// Token: 0x0400200E RID: 8206
	private ArrayList _Control_Key_Field_Name_Array;

	// Token: 0x0400200F RID: 8207
	private ArrayList _Social_Field_Name_Array;

	// Token: 0x04002010 RID: 8208
	private int _Setting_Display_Type = -1;

	// Token: 0x04002011 RID: 8209
	private int _Setting_Resolution_Ratio = -1;

	// Token: 0x04002012 RID: 8210
	private int _Default_Resolution_Width;

	// Token: 0x04002013 RID: 8211
	private int _Default_Resolution_Height;

	// Token: 0x04002014 RID: 8212
	private Setting_View _View_Component;

	// Token: 0x04002015 RID: 8213
	private bool _Load_Setting_Data_State = true;
}

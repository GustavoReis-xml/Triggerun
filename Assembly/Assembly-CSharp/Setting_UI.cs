using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000302 RID: 770
public class Setting_UI : MonoBehaviour
{
	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x0600249C RID: 9372 RVA: 0x0014AB84 File Offset: 0x00148D84
	// (set) Token: 0x0600249D RID: 9373 RVA: 0x0014ABCE File Offset: 0x00148DCE
	public GameObject Top_Layout_Object
	{
		get
		{
			bool flag = this._Top_Layout_Object == null;
			if (flag)
			{
				this._Top_Layout_Object = base.gameObject.transform.Find("Top").gameObject;
			}
			return this._Top_Layout_Object;
		}
		set
		{
			this._Top_Layout_Object = value;
		}
	}

	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x0600249E RID: 9374 RVA: 0x0014ABD8 File Offset: 0x00148DD8
	// (set) Token: 0x0600249F RID: 9375 RVA: 0x0014AC27 File Offset: 0x00148E27
	public Button Close_Button
	{
		get
		{
			bool flag = this._Close_Button == null;
			if (flag)
			{
				this._Close_Button = this.Top_Layout_Object.transform.Find("btn_Close").gameObject.GetComponent<Button>();
			}
			return this._Close_Button;
		}
		set
		{
			this._Close_Button = value;
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x060024A0 RID: 9376 RVA: 0x0014AC34 File Offset: 0x00148E34
	// (set) Token: 0x060024A1 RID: 9377 RVA: 0x0014AC83 File Offset: 0x00148E83
	public Button Confirm_Button
	{
		get
		{
			bool flag = this._Confirm_Button == null;
			if (flag)
			{
				this._Confirm_Button = this.Top_Layout_Object.transform.Find("btn_Confirm").gameObject.GetComponent<Button>();
			}
			return this._Confirm_Button;
		}
		set
		{
			this._Confirm_Button = value;
		}
	}

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x060024A2 RID: 9378 RVA: 0x0014AC90 File Offset: 0x00148E90
	// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0014ACDA File Offset: 0x00148EDA
	public GameObject Tab_Grid_Object
	{
		get
		{
			bool flag = this._Tab_Grid_Object == null;
			if (flag)
			{
				this._Tab_Grid_Object = this.Top_Layout_Object.transform.Find("Grid").gameObject;
			}
			return this._Tab_Grid_Object;
		}
		set
		{
			this._Tab_Grid_Object = value;
		}
	}

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0014ACE4 File Offset: 0x00148EE4
	// (set) Token: 0x060024A5 RID: 9381 RVA: 0x0014AD33 File Offset: 0x00148F33
	public Toggle Tab_Video_Toggle
	{
		get
		{
			bool flag = this._Tab_Video_Toggle == null;
			if (flag)
			{
				this._Tab_Video_Toggle = this.Tab_Grid_Object.transform.Find("tab_Video").gameObject.GetComponent<Toggle>();
			}
			return this._Tab_Video_Toggle;
		}
		set
		{
			this._Tab_Video_Toggle = value;
		}
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x060024A6 RID: 9382 RVA: 0x0014AD40 File Offset: 0x00148F40
	// (set) Token: 0x060024A7 RID: 9383 RVA: 0x0014AD8F File Offset: 0x00148F8F
	public Toggle Tab_Voice_Toggle
	{
		get
		{
			bool flag = this._Tab_Voice_Toggle == null;
			if (flag)
			{
				this._Tab_Voice_Toggle = this.Tab_Grid_Object.transform.Find("tab_Voice").gameObject.GetComponent<Toggle>();
			}
			return this._Tab_Voice_Toggle;
		}
		set
		{
			this._Tab_Voice_Toggle = value;
		}
	}

	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x060024A8 RID: 9384 RVA: 0x0014AD9C File Offset: 0x00148F9C
	// (set) Token: 0x060024A9 RID: 9385 RVA: 0x0014ADEB File Offset: 0x00148FEB
	public Toggle Tab_Game_Toggle
	{
		get
		{
			bool flag = this._Tab_Game_Toggle == null;
			if (flag)
			{
				this._Tab_Game_Toggle = this.Tab_Grid_Object.transform.Find("tab_Game").gameObject.GetComponent<Toggle>();
			}
			return this._Tab_Game_Toggle;
		}
		set
		{
			this._Tab_Game_Toggle = value;
		}
	}

	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x060024AA RID: 9386 RVA: 0x0014ADF8 File Offset: 0x00148FF8
	// (set) Token: 0x060024AB RID: 9387 RVA: 0x0014AE47 File Offset: 0x00149047
	public Toggle Tab_Control_Toggle
	{
		get
		{
			bool flag = this._Tab_Control_Toggle == null;
			if (flag)
			{
				this._Tab_Control_Toggle = this.Tab_Grid_Object.transform.Find("tab_Control").gameObject.GetComponent<Toggle>();
			}
			return this._Tab_Control_Toggle;
		}
		set
		{
			this._Tab_Control_Toggle = value;
		}
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x060024AC RID: 9388 RVA: 0x0014AE54 File Offset: 0x00149054
	// (set) Token: 0x060024AD RID: 9389 RVA: 0x0014AEA3 File Offset: 0x001490A3
	public Toggle Tab_Social_Toggle
	{
		get
		{
			bool flag = this._Tab_Social_Toggle == null;
			if (flag)
			{
				this._Tab_Social_Toggle = this.Tab_Grid_Object.transform.Find("tab_Social").gameObject.GetComponent<Toggle>();
			}
			return this._Tab_Social_Toggle;
		}
		set
		{
			this._Tab_Social_Toggle = value;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x060024AE RID: 9390 RVA: 0x0014AEB0 File Offset: 0x001490B0
	// (set) Token: 0x060024AF RID: 9391 RVA: 0x0014AEFA File Offset: 0x001490FA
	public GameObject Center_Layout_Object
	{
		get
		{
			bool flag = this._Center_Layout_Object == null;
			if (flag)
			{
				this._Center_Layout_Object = base.gameObject.transform.Find("Center").gameObject;
			}
			return this._Center_Layout_Object;
		}
		set
		{
			this._Center_Layout_Object = value;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0014AF04 File Offset: 0x00149104
	// (set) Token: 0x060024B1 RID: 9393 RVA: 0x0014AF4E File Offset: 0x0014914E
	public GameObject Video_Content_Object
	{
		get
		{
			bool flag = this._Video_Content_Object == null;
			if (flag)
			{
				this._Video_Content_Object = this.Center_Layout_Object.transform.Find("Video_Content").gameObject;
			}
			return this._Video_Content_Object;
		}
		set
		{
			this._Video_Content_Object = value;
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0014AF58 File Offset: 0x00149158
	// (set) Token: 0x060024B3 RID: 9395 RVA: 0x0014AFA7 File Offset: 0x001491A7
	public RectTransform Video_Mask_Content_RectTransform
	{
		get
		{
			bool flag = this._Video_Mask_Content_RectTransform == null;
			if (flag)
			{
				this._Video_Mask_Content_RectTransform = this.Video_Content_Object.transform.Find("Scroll View/Viewport/Content").gameObject.GetComponent<RectTransform>();
			}
			return this._Video_Mask_Content_RectTransform;
		}
		set
		{
			this._Video_Mask_Content_RectTransform = value;
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0014AFB4 File Offset: 0x001491B4
	// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0014AFFE File Offset: 0x001491FE
	public GameObject Video_Content_Grid_Object
	{
		get
		{
			bool flag = this._Video_Content_Grid_Object == null;
			if (flag)
			{
				this._Video_Content_Grid_Object = this.Video_Content_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Video_Content_Grid_Object;
		}
		set
		{
			this._Video_Content_Grid_Object = value;
		}
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x060024B6 RID: 9398 RVA: 0x0014B008 File Offset: 0x00149208
	// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0014B057 File Offset: 0x00149257
	public Button Video_Restore_Setting_Button
	{
		get
		{
			bool flag = this._Video_Restore_Setting_Button == null;
			if (flag)
			{
				this._Video_Restore_Setting_Button = this.Video_Content_Object.transform.Find("btn_Restore").gameObject.GetComponent<Button>();
			}
			return this._Video_Restore_Setting_Button;
		}
		set
		{
			this._Video_Restore_Setting_Button = value;
		}
	}

	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0014B064 File Offset: 0x00149264
	// (set) Token: 0x060024B9 RID: 9401 RVA: 0x0014B0B3 File Offset: 0x001492B3
	public Button Video_Use_Setting_Button
	{
		get
		{
			bool flag = this._Video_Use_Setting_Button == null;
			if (flag)
			{
				this._Video_Use_Setting_Button = this.Video_Content_Object.transform.Find("btn_Use").gameObject.GetComponent<Button>();
			}
			return this._Video_Use_Setting_Button;
		}
		set
		{
			this._Video_Use_Setting_Button = value;
		}
	}

	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x060024BA RID: 9402 RVA: 0x0014B0C0 File Offset: 0x001492C0
	// (set) Token: 0x060024BB RID: 9403 RVA: 0x0014B10A File Offset: 0x0014930A
	public GameObject Voice_Content_Object
	{
		get
		{
			bool flag = this._Voice_Content_Object == null;
			if (flag)
			{
				this._Voice_Content_Object = this.Center_Layout_Object.transform.Find("Voice_Content").gameObject;
			}
			return this._Voice_Content_Object;
		}
		set
		{
			this._Voice_Content_Object = value;
		}
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x060024BC RID: 9404 RVA: 0x0014B114 File Offset: 0x00149314
	// (set) Token: 0x060024BD RID: 9405 RVA: 0x0014B15E File Offset: 0x0014935E
	public GameObject Voice_Content_Grid_Object
	{
		get
		{
			bool flag = this._Voice_Content_Grid_Object == null;
			if (flag)
			{
				this._Voice_Content_Grid_Object = this.Voice_Content_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Voice_Content_Grid_Object;
		}
		set
		{
			this._Voice_Content_Grid_Object = value;
		}
	}

	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x060024BE RID: 9406 RVA: 0x0014B168 File Offset: 0x00149368
	// (set) Token: 0x060024BF RID: 9407 RVA: 0x0014B1B7 File Offset: 0x001493B7
	public Button Voice_Restore_Setting_Button
	{
		get
		{
			bool flag = this._Voice_Restore_Setting_Button == null;
			if (flag)
			{
				this._Voice_Restore_Setting_Button = this.Voice_Content_Object.transform.Find("btn_Restore").gameObject.GetComponent<Button>();
			}
			return this._Voice_Restore_Setting_Button;
		}
		set
		{
			this._Voice_Restore_Setting_Button = value;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x060024C0 RID: 9408 RVA: 0x0014B1C4 File Offset: 0x001493C4
	// (set) Token: 0x060024C1 RID: 9409 RVA: 0x0014B20E File Offset: 0x0014940E
	public GameObject Game_Content_Object
	{
		get
		{
			bool flag = this._Game_Content_Object == null;
			if (flag)
			{
				this._Game_Content_Object = this.Center_Layout_Object.transform.Find("Game_Content").gameObject;
			}
			return this._Game_Content_Object;
		}
		set
		{
			this._Game_Content_Object = value;
		}
	}

	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0014B218 File Offset: 0x00149418
	// (set) Token: 0x060024C3 RID: 9411 RVA: 0x0014B262 File Offset: 0x00149462
	public GameObject Game_Content_Grid_Object
	{
		get
		{
			bool flag = this._Game_Content_Grid_Object == null;
			if (flag)
			{
				this._Game_Content_Grid_Object = this.Game_Content_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Game_Content_Grid_Object;
		}
		set
		{
			this._Game_Content_Grid_Object = value;
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x060024C4 RID: 9412 RVA: 0x0014B26C File Offset: 0x0014946C
	// (set) Token: 0x060024C5 RID: 9413 RVA: 0x0014B2BB File Offset: 0x001494BB
	public Button Game_Restore_Setting_Button
	{
		get
		{
			bool flag = this._Game_Restore_Setting_Button == null;
			if (flag)
			{
				this._Game_Restore_Setting_Button = this.Game_Content_Object.transform.Find("btn_Restore").gameObject.GetComponent<Button>();
			}
			return this._Game_Restore_Setting_Button;
		}
		set
		{
			this._Game_Restore_Setting_Button = value;
		}
	}

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0014B2C8 File Offset: 0x001494C8
	// (set) Token: 0x060024C7 RID: 9415 RVA: 0x0014B312 File Offset: 0x00149512
	public GameObject Control_Content_Object
	{
		get
		{
			bool flag = this._Control_Content_Object == null;
			if (flag)
			{
				this._Control_Content_Object = this.Center_Layout_Object.transform.Find("Control_Content").gameObject;
			}
			return this._Control_Content_Object;
		}
		set
		{
			this._Control_Content_Object = value;
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x060024C8 RID: 9416 RVA: 0x0014B31C File Offset: 0x0014951C
	// (set) Token: 0x060024C9 RID: 9417 RVA: 0x0014B36B File Offset: 0x0014956B
	public ScrollRect Control_Content_ScrollRect
	{
		get
		{
			bool flag = this._Control_Content_ScrollRect == null;
			if (flag)
			{
				this._Control_Content_ScrollRect = this.Control_Content_Object.transform.Find("Scroll View").gameObject.GetComponent<ScrollRect>();
			}
			return this._Control_Content_ScrollRect;
		}
		set
		{
			this._Control_Content_ScrollRect = value;
		}
	}

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x060024CA RID: 9418 RVA: 0x0014B378 File Offset: 0x00149578
	// (set) Token: 0x060024CB RID: 9419 RVA: 0x0014B3C2 File Offset: 0x001495C2
	public GameObject Control_Content_Grid_Object
	{
		get
		{
			bool flag = this._Control_Content_Grid_Object == null;
			if (flag)
			{
				this._Control_Content_Grid_Object = this.Control_Content_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Control_Content_Grid_Object;
		}
		set
		{
			this._Control_Content_Grid_Object = value;
		}
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x060024CC RID: 9420 RVA: 0x0014B3CC File Offset: 0x001495CC
	// (set) Token: 0x060024CD RID: 9421 RVA: 0x0014B416 File Offset: 0x00149616
	public GameObject Control_Content_Grid_Aim_Mouse_Sensitivity_Object
	{
		get
		{
			bool flag = this._Control_Content_Grid_Aim_Mouse_Sensitivity_Object == null;
			if (flag)
			{
				this._Control_Content_Grid_Aim_Mouse_Sensitivity_Object = this.Control_Content_Grid_Object.transform.Find("Aim_Mouse_Sensitivity").gameObject;
			}
			return this._Control_Content_Grid_Aim_Mouse_Sensitivity_Object;
		}
		set
		{
			this._Control_Content_Grid_Aim_Mouse_Sensitivity_Object = value;
		}
	}

	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x060024CE RID: 9422 RVA: 0x0014B420 File Offset: 0x00149620
	// (set) Token: 0x060024CF RID: 9423 RVA: 0x0014B46F File Offset: 0x0014966F
	public Button Control_Content_Restore_Single_Hero_Button
	{
		get
		{
			bool flag = this._Control_Content_Restore_Single_Hero_Button == null;
			if (flag)
			{
				this._Control_Content_Restore_Single_Hero_Button = this.Control_Content_Object.transform.Find("btn_Restore_Single").gameObject.GetComponent<Button>();
			}
			return this._Control_Content_Restore_Single_Hero_Button;
		}
		set
		{
			this._Control_Content_Restore_Single_Hero_Button = value;
		}
	}

	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0014B47C File Offset: 0x0014967C
	// (set) Token: 0x060024D1 RID: 9425 RVA: 0x0014B4CB File Offset: 0x001496CB
	public Dropdown Control_Content_Selected_Hero_Dropdown
	{
		get
		{
			bool flag = this._Control_Content_Selected_Hero_Dropdown == null;
			if (flag)
			{
				this._Control_Content_Selected_Hero_Dropdown = this.Control_Content_Object.transform.Find("ddl_Select").gameObject.GetComponent<Dropdown>();
			}
			return this._Control_Content_Selected_Hero_Dropdown;
		}
		set
		{
			this._Control_Content_Selected_Hero_Dropdown = value;
		}
	}

	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0014B4D8 File Offset: 0x001496D8
	// (set) Token: 0x060024D3 RID: 9427 RVA: 0x0014B527 File Offset: 0x00149727
	public Image Control_Content_Result_Image
	{
		get
		{
			bool flag = this._Control_Content_Result_Image == null;
			if (flag)
			{
				this._Control_Content_Result_Image = this.Control_Content_Object.transform.Find("img_Result").gameObject.GetComponent<Image>();
			}
			return this._Control_Content_Result_Image;
		}
		set
		{
			this._Control_Content_Result_Image = value;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0014B534 File Offset: 0x00149734
	// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0014B588 File Offset: 0x00149788
	public Text Control_Content_Result_Text
	{
		get
		{
			bool flag = this._Control_Content_Result_Text == null;
			if (flag)
			{
				this._Control_Content_Result_Text = this.Control_Content_Result_Image.gameObject.transform.Find("lbl_Result").gameObject.GetComponent<Text>();
			}
			return this._Control_Content_Result_Text;
		}
		set
		{
			this._Control_Content_Result_Text = value;
		}
	}

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x060024D6 RID: 9430 RVA: 0x0014B594 File Offset: 0x00149794
	// (set) Token: 0x060024D7 RID: 9431 RVA: 0x0014B5E3 File Offset: 0x001497E3
	public ToggleGroup Control_Content_Toggle_Group
	{
		get
		{
			bool flag = this._Control_Content_Toggle_Group == null;
			if (flag)
			{
				this._Control_Content_Toggle_Group = this.Control_Content_Object.transform.Find("Toggle_Group").gameObject.GetComponent<ToggleGroup>();
			}
			return this._Control_Content_Toggle_Group;
		}
		set
		{
			this._Control_Content_Toggle_Group = value;
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x060024D8 RID: 9432 RVA: 0x0014B5F0 File Offset: 0x001497F0
	// (set) Token: 0x060024D9 RID: 9433 RVA: 0x0014B63F File Offset: 0x0014983F
	public Button Control_Restore_Setting_Button
	{
		get
		{
			bool flag = this._Control_Restore_Setting_Button == null;
			if (flag)
			{
				this._Control_Restore_Setting_Button = this.Control_Content_Object.transform.Find("btn_Restore").gameObject.GetComponent<Button>();
			}
			return this._Control_Restore_Setting_Button;
		}
		set
		{
			this._Control_Restore_Setting_Button = value;
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x060024DA RID: 9434 RVA: 0x0014B64C File Offset: 0x0014984C
	// (set) Token: 0x060024DB RID: 9435 RVA: 0x0014B696 File Offset: 0x00149896
	public GameObject Social_Content_Object
	{
		get
		{
			bool flag = this._Social_Content_Object == null;
			if (flag)
			{
				this._Social_Content_Object = this.Center_Layout_Object.transform.Find("Social_Content").gameObject;
			}
			return this._Social_Content_Object;
		}
		set
		{
			this._Social_Content_Object = value;
		}
	}

	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x060024DC RID: 9436 RVA: 0x0014B6A0 File Offset: 0x001498A0
	// (set) Token: 0x060024DD RID: 9437 RVA: 0x0014B6EA File Offset: 0x001498EA
	public GameObject Social_Content_Grid_Object
	{
		get
		{
			bool flag = this._Social_Content_Grid_Object == null;
			if (flag)
			{
				this._Social_Content_Grid_Object = this.Social_Content_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Social_Content_Grid_Object;
		}
		set
		{
			this._Social_Content_Grid_Object = value;
		}
	}

	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x060024DE RID: 9438 RVA: 0x0014B6F4 File Offset: 0x001498F4
	// (set) Token: 0x060024DF RID: 9439 RVA: 0x0014B743 File Offset: 0x00149943
	public Button Social_Restore_Setting_Button
	{
		get
		{
			bool flag = this._Social_Restore_Setting_Button == null;
			if (flag)
			{
				this._Social_Restore_Setting_Button = this.Social_Content_Object.transform.Find("btn_Restore").gameObject.GetComponent<Button>();
			}
			return this._Social_Restore_Setting_Button;
		}
		set
		{
			this._Social_Restore_Setting_Button = value;
		}
	}

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x060024E0 RID: 9440 RVA: 0x0014B750 File Offset: 0x00149950
	// (set) Token: 0x060024E1 RID: 9441 RVA: 0x0014B79A File Offset: 0x0014999A
	public GameObject Bottom_Layout_Object
	{
		get
		{
			bool flag = this._Bottom_Layout_Object == null;
			if (flag)
			{
				this._Bottom_Layout_Object = base.gameObject.transform.Find("Bottom").gameObject;
			}
			return this._Bottom_Layout_Object;
		}
		set
		{
			this._Bottom_Layout_Object = value;
		}
	}

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0014B7A4 File Offset: 0x001499A4
	// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0014B7DE File Offset: 0x001499DE
	public Dictionary<string, GameObject> Video_Content_Structure
	{
		get
		{
			bool flag = this._Video_Content_Structure == null;
			if (flag)
			{
				this.Traversal_Structure(this.Video_Content_Grid_Object, ref this._Video_Content_Structure);
			}
			return this._Video_Content_Structure;
		}
		set
		{
			this._Video_Content_Structure = value;
		}
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x060024E4 RID: 9444 RVA: 0x0014B7E8 File Offset: 0x001499E8
	// (set) Token: 0x060024E5 RID: 9445 RVA: 0x0014B822 File Offset: 0x00149A22
	public Dictionary<string, GameObject> Voice_Content_Structure
	{
		get
		{
			bool flag = this._Voice_Content_Structure == null;
			if (flag)
			{
				this.Traversal_Structure(this.Voice_Content_Grid_Object, ref this._Voice_Content_Structure);
			}
			return this._Voice_Content_Structure;
		}
		set
		{
			this._Voice_Content_Structure = value;
		}
	}

	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x060024E6 RID: 9446 RVA: 0x0014B82C File Offset: 0x00149A2C
	// (set) Token: 0x060024E7 RID: 9447 RVA: 0x0014B866 File Offset: 0x00149A66
	public Dictionary<string, GameObject> Game_Content_Structure
	{
		get
		{
			bool flag = this._Game_Content_Structure == null;
			if (flag)
			{
				this.Traversal_Structure(this.Game_Content_Grid_Object, ref this._Game_Content_Structure);
			}
			return this._Game_Content_Structure;
		}
		set
		{
			this._Game_Content_Structure = value;
		}
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x060024E8 RID: 9448 RVA: 0x0014B870 File Offset: 0x00149A70
	// (set) Token: 0x060024E9 RID: 9449 RVA: 0x0014B8AA File Offset: 0x00149AAA
	public Dictionary<string, GameObject> Control_Content_Structure
	{
		get
		{
			bool flag = this._Control_Content_Structure == null;
			if (flag)
			{
				this.Traversal_Structure(this.Control_Content_Grid_Object, ref this._Control_Content_Structure);
			}
			return this._Control_Content_Structure;
		}
		set
		{
			this._Control_Content_Structure = value;
		}
	}

	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x060024EA RID: 9450 RVA: 0x0014B8B4 File Offset: 0x00149AB4
	// (set) Token: 0x060024EB RID: 9451 RVA: 0x0014B8EE File Offset: 0x00149AEE
	public Dictionary<string, GameObject> Social_Content_Structure
	{
		get
		{
			bool flag = this._Social_Content_Structure == null;
			if (flag)
			{
				this.Traversal_Structure(this.Social_Content_Grid_Object, ref this._Social_Content_Structure);
			}
			return this._Social_Content_Structure;
		}
		set
		{
			this._Social_Content_Structure = value;
		}
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x0014B8F8 File Offset: 0x00149AF8
	private void Traversal_Structure(GameObject _Parent_Object, ref Dictionary<string, GameObject> _Structure)
	{
		bool flag = _Structure == null;
		if (flag)
		{
			_Structure = new Dictionary<string, GameObject>();
		}
		foreach (object obj in _Parent_Object.transform)
		{
			Transform transform = (Transform)obj;
			bool flag2 = !(transform.gameObject.name == "Text");
			if (flag2)
			{
				_Structure.Add(transform.gameObject.name, transform.gameObject);
			}
		}
	}

	// Token: 0x0400209B RID: 8347
	private GameObject _Top_Layout_Object;

	// Token: 0x0400209C RID: 8348
	private Button _Close_Button;

	// Token: 0x0400209D RID: 8349
	private Button _Confirm_Button;

	// Token: 0x0400209E RID: 8350
	private GameObject _Tab_Grid_Object;

	// Token: 0x0400209F RID: 8351
	private Toggle _Tab_Video_Toggle;

	// Token: 0x040020A0 RID: 8352
	private Toggle _Tab_Voice_Toggle;

	// Token: 0x040020A1 RID: 8353
	private Toggle _Tab_Game_Toggle;

	// Token: 0x040020A2 RID: 8354
	private Toggle _Tab_Control_Toggle;

	// Token: 0x040020A3 RID: 8355
	private Toggle _Tab_Social_Toggle;

	// Token: 0x040020A4 RID: 8356
	private GameObject _Center_Layout_Object;

	// Token: 0x040020A5 RID: 8357
	private GameObject _Video_Content_Object;

	// Token: 0x040020A6 RID: 8358
	private RectTransform _Video_Mask_Content_RectTransform;

	// Token: 0x040020A7 RID: 8359
	private GameObject _Video_Content_Grid_Object;

	// Token: 0x040020A8 RID: 8360
	private Button _Video_Restore_Setting_Button;

	// Token: 0x040020A9 RID: 8361
	private Button _Video_Use_Setting_Button;

	// Token: 0x040020AA RID: 8362
	private GameObject _Voice_Content_Object;

	// Token: 0x040020AB RID: 8363
	private GameObject _Voice_Content_Grid_Object;

	// Token: 0x040020AC RID: 8364
	private Button _Voice_Restore_Setting_Button;

	// Token: 0x040020AD RID: 8365
	private GameObject _Game_Content_Object;

	// Token: 0x040020AE RID: 8366
	private GameObject _Game_Content_Grid_Object;

	// Token: 0x040020AF RID: 8367
	private Button _Game_Restore_Setting_Button;

	// Token: 0x040020B0 RID: 8368
	private GameObject _Control_Content_Object;

	// Token: 0x040020B1 RID: 8369
	private ScrollRect _Control_Content_ScrollRect;

	// Token: 0x040020B2 RID: 8370
	private GameObject _Control_Content_Grid_Object;

	// Token: 0x040020B3 RID: 8371
	private GameObject _Control_Content_Grid_Aim_Mouse_Sensitivity_Object;

	// Token: 0x040020B4 RID: 8372
	private Button _Control_Content_Restore_Single_Hero_Button;

	// Token: 0x040020B5 RID: 8373
	private Dropdown _Control_Content_Selected_Hero_Dropdown;

	// Token: 0x040020B6 RID: 8374
	private Image _Control_Content_Result_Image;

	// Token: 0x040020B7 RID: 8375
	private Text _Control_Content_Result_Text;

	// Token: 0x040020B8 RID: 8376
	private ToggleGroup _Control_Content_Toggle_Group;

	// Token: 0x040020B9 RID: 8377
	private Button _Control_Restore_Setting_Button;

	// Token: 0x040020BA RID: 8378
	private GameObject _Social_Content_Object;

	// Token: 0x040020BB RID: 8379
	private GameObject _Social_Content_Grid_Object;

	// Token: 0x040020BC RID: 8380
	private Button _Social_Restore_Setting_Button;

	// Token: 0x040020BD RID: 8381
	private GameObject _Bottom_Layout_Object;

	// Token: 0x040020BE RID: 8382
	private Dictionary<string, GameObject> _Video_Content_Structure;

	// Token: 0x040020BF RID: 8383
	private Dictionary<string, GameObject> _Voice_Content_Structure;

	// Token: 0x040020C0 RID: 8384
	private Dictionary<string, GameObject> _Game_Content_Structure;

	// Token: 0x040020C1 RID: 8385
	private Dictionary<string, GameObject> _Control_Content_Structure;

	// Token: 0x040020C2 RID: 8386
	private Dictionary<string, GameObject> _Social_Content_Structure;
}

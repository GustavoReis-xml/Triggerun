using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E6 RID: 742
public class Scramble_UI : MonoBehaviour
{
	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06002263 RID: 8803 RVA: 0x0013E568 File Offset: 0x0013C768
	// (set) Token: 0x06002264 RID: 8804 RVA: 0x0013E5B2 File Offset: 0x0013C7B2
	public GameObject Top_Container
	{
		get
		{
			bool flag = this._Top_Container == null;
			if (flag)
			{
				this._Top_Container = base.gameObject.transform.Find("Top").gameObject;
			}
			return this._Top_Container;
		}
		set
		{
			this._Top_Container = value;
		}
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06002265 RID: 8805 RVA: 0x0013E5BC File Offset: 0x0013C7BC
	// (set) Token: 0x06002266 RID: 8806 RVA: 0x0013E606 File Offset: 0x0013C806
	public GameObject Center_Container
	{
		get
		{
			bool flag = this._Center_Container == null;
			if (flag)
			{
				this._Center_Container = base.gameObject.transform.Find("Center").gameObject;
			}
			return this._Center_Container;
		}
		set
		{
			this._Center_Container = value;
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x06002267 RID: 8807 RVA: 0x0013E610 File Offset: 0x0013C810
	// (set) Token: 0x06002268 RID: 8808 RVA: 0x0013E6A4 File Offset: 0x0013C8A4
	public GameObject Victory_Effect
	{
		get
		{
			bool flag = this._Victory_Effect == null;
			if (flag)
			{
				bool flag2 = WebParam.client_language == "zh";
				if (flag2)
				{
					this._Victory_Effect = base.gameObject.transform.Find("Victory_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Victory_Effect = base.gameObject.transform.Find("Victory_Effect_bx").gameObject;
				}
			}
			return this._Victory_Effect;
		}
		set
		{
			this._Victory_Effect = value;
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06002269 RID: 8809 RVA: 0x0013E6B0 File Offset: 0x0013C8B0
	// (set) Token: 0x0600226A RID: 8810 RVA: 0x0013E744 File Offset: 0x0013C944
	public GameObject Draw_Effect
	{
		get
		{
			bool flag = this._Draw_Effect == null;
			if (flag)
			{
				bool flag2 = WebParam.client_language == "zh";
				if (flag2)
				{
					this._Draw_Effect = base.gameObject.transform.Find("Draw_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Draw_Effect = base.gameObject.transform.Find("Draw_Effect_bx").gameObject;
				}
			}
			return this._Draw_Effect;
		}
		set
		{
			this._Draw_Effect = value;
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x0600226B RID: 8811 RVA: 0x0013E750 File Offset: 0x0013C950
	// (set) Token: 0x0600226C RID: 8812 RVA: 0x0013E7E4 File Offset: 0x0013C9E4
	public GameObject Failure_Effect
	{
		get
		{
			bool flag = this._Failure_Effect == null;
			if (flag)
			{
				bool flag2 = WebParam.client_language == "zh";
				if (flag2)
				{
					this._Failure_Effect = base.gameObject.transform.Find("Failure_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Failure_Effect = base.gameObject.transform.Find("Failure_Effect_bx").gameObject;
				}
			}
			return this._Failure_Effect;
		}
		set
		{
			this._Failure_Effect = value;
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x0600226D RID: 8813 RVA: 0x0013E7F0 File Offset: 0x0013C9F0
	// (set) Token: 0x0600226E RID: 8814 RVA: 0x0013E83A File Offset: 0x0013CA3A
	public Slider A_Progress_Slider
	{
		get
		{
			bool flag = this._A_Progress_Slider == null;
			if (flag)
			{
				this._A_Progress_Slider = this.Top_Container.transform.Find("A_Progress").GetComponent<Slider>();
			}
			return this._A_Progress_Slider;
		}
		set
		{
			this._A_Progress_Slider = value;
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x0600226F RID: 8815 RVA: 0x0013E844 File Offset: 0x0013CA44
	// (set) Token: 0x06002270 RID: 8816 RVA: 0x0013E88E File Offset: 0x0013CA8E
	public Image A_Progress_Slider_Background
	{
		get
		{
			bool flag = this._A_Progress_Slider_Background == null;
			if (flag)
			{
				this._A_Progress_Slider_Background = this.A_Progress_Slider.transform.Find("Background").GetComponent<Image>();
			}
			return this._A_Progress_Slider_Background;
		}
		set
		{
			this._A_Progress_Slider_Background = value;
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06002271 RID: 8817 RVA: 0x0013E898 File Offset: 0x0013CA98
	// (set) Token: 0x06002272 RID: 8818 RVA: 0x0013E8E2 File Offset: 0x0013CAE2
	public Image A_Progress_Slider_Fill
	{
		get
		{
			bool flag = this._A_Progress_Slider_Fill == null;
			if (flag)
			{
				this._A_Progress_Slider_Fill = this.A_Progress_Slider.transform.Find("Fill Area/Fill").GetComponent<Image>();
			}
			return this._A_Progress_Slider_Fill;
		}
		set
		{
			this._A_Progress_Slider_Fill = value;
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06002273 RID: 8819 RVA: 0x0013E8EC File Offset: 0x0013CAEC
	// (set) Token: 0x06002274 RID: 8820 RVA: 0x0013E93B File Offset: 0x0013CB3B
	public Text A_Score_Text
	{
		get
		{
			bool flag = this._A_Score_Text == null;
			if (flag)
			{
				this._A_Score_Text = this.A_Progress_Slider.gameObject.transform.Find("Score").GetComponent<Text>();
			}
			return this._A_Score_Text;
		}
		set
		{
			this._A_Score_Text = value;
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06002275 RID: 8821 RVA: 0x0013E948 File Offset: 0x0013CB48
	// (set) Token: 0x06002276 RID: 8822 RVA: 0x0013E992 File Offset: 0x0013CB92
	public Slider B_Progress_Slider
	{
		get
		{
			bool flag = this._B_Progress_Slider == null;
			if (flag)
			{
				this._B_Progress_Slider = this.Top_Container.transform.Find("B_Progress").GetComponent<Slider>();
			}
			return this._B_Progress_Slider;
		}
		set
		{
			this._B_Progress_Slider = value;
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06002277 RID: 8823 RVA: 0x0013E99C File Offset: 0x0013CB9C
	// (set) Token: 0x06002278 RID: 8824 RVA: 0x0013E9E6 File Offset: 0x0013CBE6
	public Image B_Progress_Slider_Background
	{
		get
		{
			bool flag = this._B_Progress_Slider_Background == null;
			if (flag)
			{
				this._B_Progress_Slider_Background = this.B_Progress_Slider.transform.Find("Background").GetComponent<Image>();
			}
			return this._B_Progress_Slider_Background;
		}
		set
		{
			this._B_Progress_Slider_Background = value;
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06002279 RID: 8825 RVA: 0x0013E9F0 File Offset: 0x0013CBF0
	// (set) Token: 0x0600227A RID: 8826 RVA: 0x0013EA3A File Offset: 0x0013CC3A
	public Image B_Progress_Slider_Fill
	{
		get
		{
			bool flag = this._B_Progress_Slider_Fill == null;
			if (flag)
			{
				this._B_Progress_Slider_Fill = this.B_Progress_Slider.transform.Find("Fill Area/Fill").GetComponent<Image>();
			}
			return this._B_Progress_Slider_Fill;
		}
		set
		{
			this._B_Progress_Slider_Fill = value;
		}
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x0600227B RID: 8827 RVA: 0x0013EA44 File Offset: 0x0013CC44
	// (set) Token: 0x0600227C RID: 8828 RVA: 0x0013EA93 File Offset: 0x0013CC93
	public Text B_Score_Text
	{
		get
		{
			bool flag = this._B_Score_Text == null;
			if (flag)
			{
				this._B_Score_Text = this.B_Progress_Slider.gameObject.transform.Find("Score").GetComponent<Text>();
			}
			return this._B_Score_Text;
		}
		set
		{
			this._B_Score_Text = value;
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x0600227D RID: 8829 RVA: 0x0013EAA0 File Offset: 0x0013CCA0
	// (set) Token: 0x0600227E RID: 8830 RVA: 0x0013EAEA File Offset: 0x0013CCEA
	public Text Remaining_Time_Text
	{
		get
		{
			bool flag = this._Remaining_Time_Text == null;
			if (flag)
			{
				this._Remaining_Time_Text = this.Top_Container.transform.Find("Remaining_Time").GetComponent<Text>();
			}
			return this._Remaining_Time_Text;
		}
		set
		{
			this._Remaining_Time_Text = value;
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x0600227F RID: 8831 RVA: 0x0013EAF4 File Offset: 0x0013CCF4
	// (set) Token: 0x06002280 RID: 8832 RVA: 0x0013EB3E File Offset: 0x0013CD3E
	public Text Prepare_Describe_Text
	{
		get
		{
			bool flag = this._Prepare_Describe_Text == null;
			if (flag)
			{
				this._Prepare_Describe_Text = this.Top_Container.transform.Find("Prepare_Describe").GetComponent<Text>();
			}
			return this._Prepare_Describe_Text;
		}
		set
		{
			this._Prepare_Describe_Text = value;
		}
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06002281 RID: 8833 RVA: 0x0013EB48 File Offset: 0x0013CD48
	// (set) Token: 0x06002282 RID: 8834 RVA: 0x0013EB92 File Offset: 0x0013CD92
	public GameObject A_Group_Object
	{
		get
		{
			bool flag = this._A_Group_Object == null;
			if (flag)
			{
				this._A_Group_Object = this.Top_Container.transform.Find("A").gameObject;
			}
			return this._A_Group_Object;
		}
		set
		{
			this._A_Group_Object = value;
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06002283 RID: 8835 RVA: 0x0013EB9C File Offset: 0x0013CD9C
	// (set) Token: 0x06002284 RID: 8836 RVA: 0x0013EC22 File Offset: 0x0013CE22
	public Progress_Effect_Handle_Image A_Image_Progress_Effect_Component
	{
		get
		{
			bool flag = this._A_Image_Progress_Effect_Component == null;
			if (flag)
			{
				this._A_Image_Progress_Effect_Component = this.A_Group_Object.transform.Find("Image_Progress").gameObject.GetComponent<Progress_Effect_Handle_Image>();
				bool flag2 = this._A_Image_Progress_Effect_Component == null;
				if (flag2)
				{
					this._A_Image_Progress_Effect_Component = this.A_Group_Object.transform.Find("Image_Progress").gameObject.AddComponent<Progress_Effect_Handle_Image>();
				}
			}
			return this._A_Image_Progress_Effect_Component;
		}
		set
		{
			this._A_Image_Progress_Effect_Component = value;
		}
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x06002285 RID: 8837 RVA: 0x0013EC2C File Offset: 0x0013CE2C
	// (set) Token: 0x06002286 RID: 8838 RVA: 0x0013EC76 File Offset: 0x0013CE76
	public GameObject B_Group_Object
	{
		get
		{
			bool flag = this._B_Group_Object == null;
			if (flag)
			{
				this._B_Group_Object = this.Top_Container.transform.Find("B").gameObject;
			}
			return this._B_Group_Object;
		}
		set
		{
			this._B_Group_Object = value;
		}
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x06002287 RID: 8839 RVA: 0x0013EC80 File Offset: 0x0013CE80
	// (set) Token: 0x06002288 RID: 8840 RVA: 0x0013ED06 File Offset: 0x0013CF06
	public Progress_Effect_Handle_Image B_Image_Progress_Effect_Component
	{
		get
		{
			bool flag = this._B_Image_Progress_Effect_Component == null;
			if (flag)
			{
				this._B_Image_Progress_Effect_Component = this.B_Group_Object.transform.Find("Image_Progress").gameObject.GetComponent<Progress_Effect_Handle_Image>();
				bool flag2 = this._B_Image_Progress_Effect_Component == null;
				if (flag2)
				{
					this._B_Image_Progress_Effect_Component = this.B_Group_Object.transform.Find("Image_Progress").gameObject.AddComponent<Progress_Effect_Handle_Image>();
				}
			}
			return this._B_Image_Progress_Effect_Component;
		}
		set
		{
			this._B_Image_Progress_Effect_Component = value;
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x06002289 RID: 8841 RVA: 0x0013ED10 File Offset: 0x0013CF10
	// (set) Token: 0x0600228A RID: 8842 RVA: 0x0013ED5A File Offset: 0x0013CF5A
	public GameObject C_Group_Object
	{
		get
		{
			bool flag = this._C_Group_Object == null;
			if (flag)
			{
				this._C_Group_Object = this.Top_Container.transform.Find("C").gameObject;
			}
			return this._C_Group_Object;
		}
		set
		{
			this._C_Group_Object = value;
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x0600228B RID: 8843 RVA: 0x0013ED64 File Offset: 0x0013CF64
	// (set) Token: 0x0600228C RID: 8844 RVA: 0x0013EDEA File Offset: 0x0013CFEA
	public Progress_Effect_Handle_Image C_Image_Progress_Effect_Component
	{
		get
		{
			bool flag = this._C_Image_Progress_Effect_Component == null;
			if (flag)
			{
				this._C_Image_Progress_Effect_Component = this.C_Group_Object.transform.Find("Image_Progress").gameObject.GetComponent<Progress_Effect_Handle_Image>();
				bool flag2 = this._C_Image_Progress_Effect_Component == null;
				if (flag2)
				{
					this._C_Image_Progress_Effect_Component = this.C_Group_Object.transform.Find("Image_Progress").gameObject.AddComponent<Progress_Effect_Handle_Image>();
				}
			}
			return this._C_Image_Progress_Effect_Component;
		}
		set
		{
			this._C_Image_Progress_Effect_Component = value;
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x0600228D RID: 8845 RVA: 0x0013EDF4 File Offset: 0x0013CFF4
	// (set) Token: 0x0600228E RID: 8846 RVA: 0x0013EE3E File Offset: 0x0013D03E
	public Slider Hold_Progress_Slider
	{
		get
		{
			bool flag = this._Hold_Progress_Slider == null;
			if (flag)
			{
				this._Hold_Progress_Slider = this.Center_Container.transform.Find("Progress").GetComponent<Slider>();
			}
			return this._Hold_Progress_Slider;
		}
		set
		{
			this._Hold_Progress_Slider = value;
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x0600228F RID: 8847 RVA: 0x0013EE48 File Offset: 0x0013D048
	// (set) Token: 0x06002290 RID: 8848 RVA: 0x0013EEC2 File Offset: 0x0013D0C2
	public Progress_Effect_Handle_Slider Hold_Progress_Effect_Component
	{
		get
		{
			bool flag = this._Hold_Progress_Effect_Component == null;
			if (flag)
			{
				this._Hold_Progress_Effect_Component = this.Center_Container.gameObject.GetComponent<Progress_Effect_Handle_Slider>();
				bool flag2 = this._Hold_Progress_Effect_Component == null;
				if (flag2)
				{
					this._Hold_Progress_Effect_Component = this.Center_Container.gameObject.AddComponent<Progress_Effect_Handle_Slider>();
				}
				this._Hold_Progress_Effect_Component.Slider_Component = this.Hold_Progress_Slider;
			}
			return this._Hold_Progress_Effect_Component;
		}
		set
		{
			this._Hold_Progress_Effect_Component = value;
		}
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x06002291 RID: 8849 RVA: 0x0013EECC File Offset: 0x0013D0CC
	// (set) Token: 0x06002292 RID: 8850 RVA: 0x0013EF16 File Offset: 0x0013D116
	public Text Describe_Text
	{
		get
		{
			bool flag = this._Describe_Text == null;
			if (flag)
			{
				this._Describe_Text = this.Center_Container.transform.Find("Describe").GetComponent<Text>();
			}
			return this._Describe_Text;
		}
		set
		{
			this._Describe_Text = value;
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x06002293 RID: 8851 RVA: 0x0013EF20 File Offset: 0x0013D120
	// (set) Token: 0x06002294 RID: 8852 RVA: 0x0013EFA3 File Offset: 0x0013D1A3
	public GameObject Countdown_Effect
	{
		get
		{
			bool flag = this._Countdown_Effect == null;
			if (flag)
			{
				bool flag2 = WebParam.client_language == "baxi";
				if (flag2)
				{
					this._Countdown_Effect = this.Center_Container.transform.Find("Countdown_bx/Particle System").gameObject;
				}
				else
				{
					this._Countdown_Effect = this.Center_Container.transform.Find("Countdown/Particle System").gameObject;
				}
			}
			return this._Countdown_Effect;
		}
		set
		{
			this._Countdown_Effect = value;
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x06002295 RID: 8853 RVA: 0x0013EFB0 File Offset: 0x0013D1B0
	// (set) Token: 0x06002296 RID: 8854 RVA: 0x0013EFFA File Offset: 0x0013D1FA
	public GameObject UI_Area_Position_1
	{
		get
		{
			bool flag = this._UI_Area_Position_1 == null;
			if (flag)
			{
				this._UI_Area_Position_1 = this.Center_Container.transform.Find("Area_Position_1").gameObject;
			}
			return this._UI_Area_Position_1;
		}
		set
		{
			this._UI_Area_Position_1 = value;
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06002297 RID: 8855 RVA: 0x0013F004 File Offset: 0x0013D204
	// (set) Token: 0x06002298 RID: 8856 RVA: 0x0013F0C0 File Offset: 0x0013D2C0
	public UI_Position_Rotation Area_UI_Position_Rotation_1
	{
		get
		{
			bool flag = this._Area_UI_Position_Rotation_1 == null;
			if (flag)
			{
				bool flag2 = fUIManager.MapEffectObj1 == null;
				if (flag2)
				{
					return null;
				}
				this._Area_UI_Position_Rotation_1 = fUIManager.MapEffectObj1.transform.GetChild(0).gameObject.GetComponent<UI_Position_Rotation>();
				bool flag3 = this._Area_UI_Position_Rotation_1 == null;
				if (flag3)
				{
					this._Area_UI_Position_Rotation_1 = fUIManager.MapEffectObj1.transform.GetChild(0).gameObject.AddComponent<UI_Position_Rotation>();
				}
				this._Area_UI_Position_Rotation_1.UI_Object = this.A_Group_Object.transform.Find("Image_Arrows").gameObject;
			}
			return this._Area_UI_Position_Rotation_1;
		}
		set
		{
			this._Area_UI_Position_Rotation_1 = value;
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06002299 RID: 8857 RVA: 0x0013F0CC File Offset: 0x0013D2CC
	// (set) Token: 0x0600229A RID: 8858 RVA: 0x0013F116 File Offset: 0x0013D316
	public GameObject UI_Area_Position_2
	{
		get
		{
			bool flag = this._UI_Area_Position_2 == null;
			if (flag)
			{
				this._UI_Area_Position_2 = this.Center_Container.transform.Find("Area_Position_2").gameObject;
			}
			return this._UI_Area_Position_2;
		}
		set
		{
			this._UI_Area_Position_2 = value;
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x0600229B RID: 8859 RVA: 0x0013F120 File Offset: 0x0013D320
	// (set) Token: 0x0600229C RID: 8860 RVA: 0x0013F1DC File Offset: 0x0013D3DC
	public UI_Position_Rotation Area_UI_Position_Rotation_2
	{
		get
		{
			bool flag = this._Area_UI_Position_Rotation_2 == null;
			if (flag)
			{
				bool flag2 = fUIManager.MapEffectObj2 == null;
				if (flag2)
				{
					return null;
				}
				this._Area_UI_Position_Rotation_2 = fUIManager.MapEffectObj2.transform.GetChild(0).gameObject.GetComponent<UI_Position_Rotation>();
				bool flag3 = this._Area_UI_Position_Rotation_2 == null;
				if (flag3)
				{
					this._Area_UI_Position_Rotation_2 = fUIManager.MapEffectObj2.transform.GetChild(0).gameObject.AddComponent<UI_Position_Rotation>();
				}
				this._Area_UI_Position_Rotation_2.UI_Object = this.B_Group_Object.transform.Find("Image_Arrows").gameObject;
			}
			return this._Area_UI_Position_Rotation_2;
		}
		set
		{
			this._Area_UI_Position_Rotation_2 = value;
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x0600229D RID: 8861 RVA: 0x0013F1E8 File Offset: 0x0013D3E8
	// (set) Token: 0x0600229E RID: 8862 RVA: 0x0013F284 File Offset: 0x0013D484
	public GameObject UI_Area_Position_3
	{
		get
		{
			bool flag = this._UI_Area_Position_3 == null;
			if (flag)
			{
				this._UI_Area_Position_3 = this.Center_Container.transform.Find("Area_Position_3").gameObject;
				UI_Position_Rotation ui_Position_Rotation = fUIManager.MapEffectObj3.transform.GetChild(0).gameObject.GetComponent<UI_Position_Rotation>();
				bool flag2 = ui_Position_Rotation == null;
				if (flag2)
				{
					ui_Position_Rotation = fUIManager.MapEffectObj3.transform.GetChild(0).gameObject.AddComponent<UI_Position_Rotation>();
				}
				ui_Position_Rotation.UI_Object = this._UI_Area_Position_3;
			}
			return this._UI_Area_Position_3;
		}
		set
		{
			this._UI_Area_Position_3 = value;
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x0600229F RID: 8863 RVA: 0x0013F290 File Offset: 0x0013D490
	// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0013F34C File Offset: 0x0013D54C
	public UI_Position_Rotation Area_UI_Position_Rotation_3
	{
		get
		{
			bool flag = this._Area_UI_Position_Rotation_3 == null;
			if (flag)
			{
				bool flag2 = fUIManager.MapEffectObj3 == null;
				if (flag2)
				{
					return null;
				}
				this._Area_UI_Position_Rotation_3 = fUIManager.MapEffectObj3.transform.GetChild(0).gameObject.GetComponent<UI_Position_Rotation>();
				bool flag3 = this._Area_UI_Position_Rotation_3 == null;
				if (flag3)
				{
					this._Area_UI_Position_Rotation_3 = fUIManager.MapEffectObj3.transform.GetChild(0).gameObject.AddComponent<UI_Position_Rotation>();
				}
				this._Area_UI_Position_Rotation_3.UI_Object = this.C_Group_Object.transform.Find("Image_Arrows").gameObject;
			}
			return this._Area_UI_Position_Rotation_3;
		}
		set
		{
			this._Area_UI_Position_Rotation_3 = value;
		}
	}

	// Token: 0x04001F95 RID: 8085
	private GameObject _Top_Container;

	// Token: 0x04001F96 RID: 8086
	private GameObject _Center_Container;

	// Token: 0x04001F97 RID: 8087
	private GameObject _Victory_Effect;

	// Token: 0x04001F98 RID: 8088
	private GameObject _Draw_Effect;

	// Token: 0x04001F99 RID: 8089
	private GameObject _Failure_Effect;

	// Token: 0x04001F9A RID: 8090
	private Slider _A_Progress_Slider;

	// Token: 0x04001F9B RID: 8091
	private Image _A_Progress_Slider_Background;

	// Token: 0x04001F9C RID: 8092
	private Image _A_Progress_Slider_Fill;

	// Token: 0x04001F9D RID: 8093
	private Text _A_Score_Text;

	// Token: 0x04001F9E RID: 8094
	private Slider _B_Progress_Slider;

	// Token: 0x04001F9F RID: 8095
	private Image _B_Progress_Slider_Background;

	// Token: 0x04001FA0 RID: 8096
	private Image _B_Progress_Slider_Fill;

	// Token: 0x04001FA1 RID: 8097
	private Text _B_Score_Text;

	// Token: 0x04001FA2 RID: 8098
	private Text _Remaining_Time_Text;

	// Token: 0x04001FA3 RID: 8099
	private Text _Prepare_Describe_Text;

	// Token: 0x04001FA4 RID: 8100
	private GameObject _A_Group_Object;

	// Token: 0x04001FA5 RID: 8101
	private Progress_Effect_Handle_Image _A_Image_Progress_Effect_Component;

	// Token: 0x04001FA6 RID: 8102
	private GameObject _B_Group_Object;

	// Token: 0x04001FA7 RID: 8103
	private Progress_Effect_Handle_Image _B_Image_Progress_Effect_Component;

	// Token: 0x04001FA8 RID: 8104
	private GameObject _C_Group_Object;

	// Token: 0x04001FA9 RID: 8105
	private Progress_Effect_Handle_Image _C_Image_Progress_Effect_Component;

	// Token: 0x04001FAA RID: 8106
	private Slider _Hold_Progress_Slider;

	// Token: 0x04001FAB RID: 8107
	private Progress_Effect_Handle_Slider _Hold_Progress_Effect_Component;

	// Token: 0x04001FAC RID: 8108
	private Text _Describe_Text;

	// Token: 0x04001FAD RID: 8109
	private GameObject _Countdown_Effect;

	// Token: 0x04001FAE RID: 8110
	private GameObject _UI_Area_Position_1;

	// Token: 0x04001FAF RID: 8111
	private UI_Position_Rotation _Area_UI_Position_Rotation_1;

	// Token: 0x04001FB0 RID: 8112
	private GameObject _UI_Area_Position_2;

	// Token: 0x04001FB1 RID: 8113
	private UI_Position_Rotation _Area_UI_Position_Rotation_2;

	// Token: 0x04001FB2 RID: 8114
	private GameObject _UI_Area_Position_3;

	// Token: 0x04001FB3 RID: 8115
	private UI_Position_Rotation _Area_UI_Position_Rotation_3;
}

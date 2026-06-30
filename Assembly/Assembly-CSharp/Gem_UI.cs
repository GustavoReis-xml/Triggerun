using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014C RID: 332
public class Gem_UI : MonoBehaviour
{
	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0008463C File Offset: 0x0008283C
	// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00084686 File Offset: 0x00082886
	public GameObject Left_Container
	{
		get
		{
			bool flag = this._Left_Container == null;
			if (flag)
			{
				this._Left_Container = base.gameObject.transform.FindChild("Left").gameObject;
			}
			return this._Left_Container;
		}
		set
		{
			this._Left_Container = value;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00084690 File Offset: 0x00082890
	// (set) Token: 0x06000E0B RID: 3595 RVA: 0x000846DF File Offset: 0x000828DF
	public GridLayoutGroup Left_Grid
	{
		get
		{
			bool flag = this._Left_Grid == null;
			if (flag)
			{
				this._Left_Grid = this.Left_Container.transform.FindChild("Grid").gameObject.GetComponent<GridLayoutGroup>();
			}
			return this._Left_Grid;
		}
		set
		{
			this._Left_Grid = value;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000846EC File Offset: 0x000828EC
	// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00084736 File Offset: 0x00082936
	public GameObject Left_Item_Object
	{
		get
		{
			bool flag = this._Left_Item_Object == null;
			if (flag)
			{
				this._Left_Item_Object = this.Left_Container.transform.FindChild("Item").gameObject;
			}
			return this._Left_Item_Object;
		}
		set
		{
			this._Left_Item_Object = value;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00084740 File Offset: 0x00082940
	// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0008478A File Offset: 0x0008298A
	public Button Left_Buy_Button
	{
		get
		{
			bool flag = this._Left_Buy_Button == null;
			if (flag)
			{
				this._Left_Buy_Button = this.Left_Container.transform.FindChild("btn_Buy").GetComponent<Button>();
			}
			return this._Left_Buy_Button;
		}
		set
		{
			this._Left_Buy_Button = value;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00084794 File Offset: 0x00082994
	// (set) Token: 0x06000E11 RID: 3601 RVA: 0x000847DE File Offset: 0x000829DE
	public GameObject Right_Container
	{
		get
		{
			bool flag = this._Right_Container == null;
			if (flag)
			{
				this._Right_Container = base.gameObject.transform.FindChild("Right").gameObject;
			}
			return this._Right_Container;
		}
		set
		{
			this._Right_Container = value;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000847E8 File Offset: 0x000829E8
	// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00084832 File Offset: 0x00082A32
	public Toggle Right_Display_Equip_Toggle
	{
		get
		{
			bool flag = this._Right_Display_Equip_Toggle == null;
			if (flag)
			{
				this._Right_Display_Equip_Toggle = this.Right_Container.transform.FindChild("btn_Display_Equip").GetComponent<Toggle>();
			}
			return this._Right_Display_Equip_Toggle;
		}
		set
		{
			this._Right_Display_Equip_Toggle = value;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0008483C File Offset: 0x00082A3C
	// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00084886 File Offset: 0x00082A86
	public GameObject Right_Equip_View_Object
	{
		get
		{
			bool flag = this._Right_Equip_View_Object == null;
			if (flag)
			{
				this._Right_Equip_View_Object = this.Right_Container.transform.FindChild("Equip").gameObject;
			}
			return this._Right_Equip_View_Object;
		}
		set
		{
			this._Right_Equip_View_Object = value;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00084890 File Offset: 0x00082A90
	// (set) Token: 0x06000E17 RID: 3607 RVA: 0x000848DA File Offset: 0x00082ADA
	public Image Right_Attack_Location_Icon_Image_1
	{
		get
		{
			bool flag = this._Right_Attack_Location_Icon_Image_1 == null;
			if (flag)
			{
				this._Right_Attack_Location_Icon_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_1/img_Icon").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Icon_Image_1;
		}
		set
		{
			this._Right_Attack_Location_Icon_Image_1 = value;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000848E4 File Offset: 0x00082AE4
	// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0008492E File Offset: 0x00082B2E
	public Image Right_Attack_Location_Quality_Image_1
	{
		get
		{
			bool flag = this._Right_Attack_Location_Quality_Image_1 == null;
			if (flag)
			{
				this._Right_Attack_Location_Quality_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_1/img_Quality").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Quality_Image_1;
		}
		set
		{
			this._Right_Attack_Location_Quality_Image_1 = value;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00084938 File Offset: 0x00082B38
	// (set) Token: 0x06000E1B RID: 3611 RVA: 0x00084982 File Offset: 0x00082B82
	public Text Right_Attack_Location_Index_Text_1
	{
		get
		{
			bool flag = this._Right_Attack_Location_Index_Text_1 == null;
			if (flag)
			{
				this._Right_Attack_Location_Index_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_1/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Attack_Location_Index_Text_1;
		}
		set
		{
			this._Right_Attack_Location_Index_Text_1 = value;
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0008498C File Offset: 0x00082B8C
	// (set) Token: 0x06000E1D RID: 3613 RVA: 0x000849D6 File Offset: 0x00082BD6
	public Text Right_Attack_Location_ID_Text_1
	{
		get
		{
			bool flag = this._Right_Attack_Location_ID_Text_1 == null;
			if (flag)
			{
				this._Right_Attack_Location_ID_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_1/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Attack_Location_ID_Text_1;
		}
		set
		{
			this._Right_Attack_Location_ID_Text_1 = value;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000849E0 File Offset: 0x00082BE0
	// (set) Token: 0x06000E1F RID: 3615 RVA: 0x00084A2A File Offset: 0x00082C2A
	public Image Right_Attack_Location_Icon_Image_2
	{
		get
		{
			bool flag = this._Right_Attack_Location_Icon_Image_2 == null;
			if (flag)
			{
				this._Right_Attack_Location_Icon_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_2/img_Icon").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Icon_Image_2;
		}
		set
		{
			this._Right_Attack_Location_Icon_Image_2 = value;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00084A34 File Offset: 0x00082C34
	// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00084A7E File Offset: 0x00082C7E
	public Image Right_Attack_Location_Quality_Image_2
	{
		get
		{
			bool flag = this._Right_Attack_Location_Quality_Image_2 == null;
			if (flag)
			{
				this._Right_Attack_Location_Quality_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_2/img_Quality").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Quality_Image_2;
		}
		set
		{
			this._Right_Attack_Location_Quality_Image_2 = value;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00084A88 File Offset: 0x00082C88
	// (set) Token: 0x06000E23 RID: 3619 RVA: 0x00084AD2 File Offset: 0x00082CD2
	public Text Right_Attack_Location_Index_Text_2
	{
		get
		{
			bool flag = this._Right_Attack_Location_Index_Text_2 == null;
			if (flag)
			{
				this._Right_Attack_Location_Index_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_2/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Attack_Location_Index_Text_2;
		}
		set
		{
			this._Right_Attack_Location_Index_Text_2 = value;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00084ADC File Offset: 0x00082CDC
	// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00084B26 File Offset: 0x00082D26
	public Text Right_Attack_Location_ID_Text_2
	{
		get
		{
			bool flag = this._Right_Attack_Location_ID_Text_2 == null;
			if (flag)
			{
				this._Right_Attack_Location_ID_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_2/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Attack_Location_ID_Text_2;
		}
		set
		{
			this._Right_Attack_Location_ID_Text_2 = value;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00084B30 File Offset: 0x00082D30
	// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00084B7A File Offset: 0x00082D7A
	public Image Right_Attack_Location_Icon_Image_3
	{
		get
		{
			bool flag = this._Right_Attack_Location_Icon_Image_3 == null;
			if (flag)
			{
				this._Right_Attack_Location_Icon_Image_3 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_3/img_Icon").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Icon_Image_3;
		}
		set
		{
			this._Right_Attack_Location_Icon_Image_3 = value;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00084B84 File Offset: 0x00082D84
	// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00084BCE File Offset: 0x00082DCE
	public Image Right_Attack_Location_Quality_Image_3
	{
		get
		{
			bool flag = this._Right_Attack_Location_Quality_Image_3 == null;
			if (flag)
			{
				this._Right_Attack_Location_Quality_Image_3 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_3/img_Quality").GetComponent<Image>();
			}
			return this._Right_Attack_Location_Quality_Image_3;
		}
		set
		{
			this._Right_Attack_Location_Quality_Image_3 = value;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00084BD8 File Offset: 0x00082DD8
	// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00084C22 File Offset: 0x00082E22
	public Text Right_Attack_Location_Index_Text_3
	{
		get
		{
			bool flag = this._Right_Attack_Location_Index_Text_3 == null;
			if (flag)
			{
				this._Right_Attack_Location_Index_Text_3 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_3/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Attack_Location_Index_Text_3;
		}
		set
		{
			this._Right_Attack_Location_Index_Text_3 = value;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00084C2C File Offset: 0x00082E2C
	// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00084C76 File Offset: 0x00082E76
	public Text Right_Attack_Location_ID_Text_3
	{
		get
		{
			bool flag = this._Right_Attack_Location_ID_Text_3 == null;
			if (flag)
			{
				this._Right_Attack_Location_ID_Text_3 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Attack_Location_3/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Attack_Location_ID_Text_3;
		}
		set
		{
			this._Right_Attack_Location_ID_Text_3 = value;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00084C80 File Offset: 0x00082E80
	// (set) Token: 0x06000E2F RID: 3631 RVA: 0x00084CCA File Offset: 0x00082ECA
	public Image Right_Defend_Location_Icon_Image_1
	{
		get
		{
			bool flag = this._Right_Defend_Location_Icon_Image_1 == null;
			if (flag)
			{
				this._Right_Defend_Location_Icon_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_1/img_Icon").GetComponent<Image>();
			}
			return this._Right_Defend_Location_Icon_Image_1;
		}
		set
		{
			this._Right_Defend_Location_Icon_Image_1 = value;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00084CD4 File Offset: 0x00082ED4
	// (set) Token: 0x06000E31 RID: 3633 RVA: 0x00084D1E File Offset: 0x00082F1E
	public Image Right_Defend_Location_Quality_Image_1
	{
		get
		{
			bool flag = this._Right_Defend_Location_Quality_Image_1 == null;
			if (flag)
			{
				this._Right_Defend_Location_Quality_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_1/img_Quality").GetComponent<Image>();
			}
			return this._Right_Defend_Location_Quality_Image_1;
		}
		set
		{
			this._Right_Defend_Location_Quality_Image_1 = value;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00084D28 File Offset: 0x00082F28
	// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00084D72 File Offset: 0x00082F72
	public Text Right_Defend_Location_Index_Text_1
	{
		get
		{
			bool flag = this._Right_Defend_Location_Index_Text_1 == null;
			if (flag)
			{
				this._Right_Defend_Location_Index_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_1/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Defend_Location_Index_Text_1;
		}
		set
		{
			this._Right_Defend_Location_Index_Text_1 = value;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00084D7C File Offset: 0x00082F7C
	// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00084DC6 File Offset: 0x00082FC6
	public Text Right_Defend_Location_ID_Text_1
	{
		get
		{
			bool flag = this._Right_Defend_Location_ID_Text_1 == null;
			if (flag)
			{
				this._Right_Defend_Location_ID_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_1/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Defend_Location_ID_Text_1;
		}
		set
		{
			this._Right_Defend_Location_ID_Text_1 = value;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00084DD0 File Offset: 0x00082FD0
	// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00084E1A File Offset: 0x0008301A
	public Image Right_Defend_Location_Icon_Image_2
	{
		get
		{
			bool flag = this._Right_Defend_Location_Icon_Image_2 == null;
			if (flag)
			{
				this._Right_Defend_Location_Icon_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_2/img_Icon").GetComponent<Image>();
			}
			return this._Right_Defend_Location_Icon_Image_2;
		}
		set
		{
			this._Right_Defend_Location_Icon_Image_2 = value;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00084E24 File Offset: 0x00083024
	// (set) Token: 0x06000E39 RID: 3641 RVA: 0x00084E6E File Offset: 0x0008306E
	public Image Right_Defend_Location_Quality_Image_2
	{
		get
		{
			bool flag = this._Right_Defend_Location_Quality_Image_2 == null;
			if (flag)
			{
				this._Right_Defend_Location_Quality_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_2/img_Quality").GetComponent<Image>();
			}
			return this._Right_Defend_Location_Quality_Image_2;
		}
		set
		{
			this._Right_Defend_Location_Quality_Image_2 = value;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00084E78 File Offset: 0x00083078
	// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00084EC2 File Offset: 0x000830C2
	public Text Right_Defend_Location_Index_Text_2
	{
		get
		{
			bool flag = this._Right_Defend_Location_Index_Text_2 == null;
			if (flag)
			{
				this._Right_Defend_Location_Index_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_2/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Defend_Location_Index_Text_2;
		}
		set
		{
			this._Right_Defend_Location_Index_Text_2 = value;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00084ECC File Offset: 0x000830CC
	// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00084F16 File Offset: 0x00083116
	public Text Right_Defend_Location_ID_Text_2
	{
		get
		{
			bool flag = this._Right_Defend_Location_ID_Text_2 == null;
			if (flag)
			{
				this._Right_Defend_Location_ID_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Defend_Location_2/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Defend_Location_ID_Text_2;
		}
		set
		{
			this._Right_Defend_Location_ID_Text_2 = value;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00084F20 File Offset: 0x00083120
	// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00084F6A File Offset: 0x0008316A
	public Image Right_Skill_Location_Icon_Image_1
	{
		get
		{
			bool flag = this._Right_Skill_Location_Icon_Image_1 == null;
			if (flag)
			{
				this._Right_Skill_Location_Icon_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_1/img_Icon").GetComponent<Image>();
			}
			return this._Right_Skill_Location_Icon_Image_1;
		}
		set
		{
			this._Right_Skill_Location_Icon_Image_1 = value;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00084F74 File Offset: 0x00083174
	// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00084FBE File Offset: 0x000831BE
	public Image Right_Skill_Location_Quality_Image_1
	{
		get
		{
			bool flag = this._Right_Skill_Location_Quality_Image_1 == null;
			if (flag)
			{
				this._Right_Skill_Location_Quality_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_1/img_Quality").GetComponent<Image>();
			}
			return this._Right_Skill_Location_Quality_Image_1;
		}
		set
		{
			this._Right_Skill_Location_Quality_Image_1 = value;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00084FC8 File Offset: 0x000831C8
	// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00085012 File Offset: 0x00083212
	public Text Right_Skill_Location_Index_Text_1
	{
		get
		{
			bool flag = this._Right_Skill_Location_Index_Text_1 == null;
			if (flag)
			{
				this._Right_Skill_Location_Index_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_1/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Skill_Location_Index_Text_1;
		}
		set
		{
			this._Right_Skill_Location_Index_Text_1 = value;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0008501C File Offset: 0x0008321C
	// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00085066 File Offset: 0x00083266
	public Text Right_Skill_Location_ID_Text_1
	{
		get
		{
			bool flag = this._Right_Skill_Location_ID_Text_1 == null;
			if (flag)
			{
				this._Right_Skill_Location_ID_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_1/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Skill_Location_ID_Text_1;
		}
		set
		{
			this._Right_Skill_Location_ID_Text_1 = value;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00085070 File Offset: 0x00083270
	// (set) Token: 0x06000E47 RID: 3655 RVA: 0x000850BA File Offset: 0x000832BA
	public Image Right_Skill_Location_Icon_Image_2
	{
		get
		{
			bool flag = this._Right_Skill_Location_Icon_Image_2 == null;
			if (flag)
			{
				this._Right_Skill_Location_Icon_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_2/img_Icon").GetComponent<Image>();
			}
			return this._Right_Skill_Location_Icon_Image_2;
		}
		set
		{
			this._Right_Skill_Location_Icon_Image_2 = value;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000E48 RID: 3656 RVA: 0x000850C4 File Offset: 0x000832C4
	// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0008510E File Offset: 0x0008330E
	public Image Right_Skill_Location_Quality_Image_2
	{
		get
		{
			bool flag = this._Right_Skill_Location_Quality_Image_2 == null;
			if (flag)
			{
				this._Right_Skill_Location_Quality_Image_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_2/img_Quality").GetComponent<Image>();
			}
			return this._Right_Skill_Location_Quality_Image_2;
		}
		set
		{
			this._Right_Skill_Location_Quality_Image_2 = value;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00085118 File Offset: 0x00083318
	// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00085162 File Offset: 0x00083362
	public Text Right_Skill_Location_Index_Text_2
	{
		get
		{
			bool flag = this._Right_Skill_Location_Index_Text_2 == null;
			if (flag)
			{
				this._Right_Skill_Location_Index_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_2/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Skill_Location_Index_Text_2;
		}
		set
		{
			this._Right_Skill_Location_Index_Text_2 = value;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0008516C File Offset: 0x0008336C
	// (set) Token: 0x06000E4D RID: 3661 RVA: 0x000851B6 File Offset: 0x000833B6
	public Text Right_Skill_Location_ID_Text_2
	{
		get
		{
			bool flag = this._Right_Skill_Location_ID_Text_2 == null;
			if (flag)
			{
				this._Right_Skill_Location_ID_Text_2 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Skill_Location_2/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Skill_Location_ID_Text_2;
		}
		set
		{
			this._Right_Skill_Location_ID_Text_2 = value;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000851C0 File Offset: 0x000833C0
	// (set) Token: 0x06000E4F RID: 3663 RVA: 0x0008520A File Offset: 0x0008340A
	public Image Right_Stunt_Location_Icon_Image_1
	{
		get
		{
			bool flag = this._Right_Stunt_Location_Icon_Image_1 == null;
			if (flag)
			{
				this._Right_Stunt_Location_Icon_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Stunt_Location_1/img_Icon").GetComponent<Image>();
			}
			return this._Right_Stunt_Location_Icon_Image_1;
		}
		set
		{
			this._Right_Stunt_Location_Icon_Image_1 = value;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00085214 File Offset: 0x00083414
	// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0008525E File Offset: 0x0008345E
	public Image Right_Stunt_Location_Quality_Image_1
	{
		get
		{
			bool flag = this._Right_Stunt_Location_Quality_Image_1 == null;
			if (flag)
			{
				this._Right_Stunt_Location_Quality_Image_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Stunt_Location_1/img_Quality").GetComponent<Image>();
			}
			return this._Right_Stunt_Location_Quality_Image_1;
		}
		set
		{
			this._Right_Stunt_Location_Quality_Image_1 = value;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00085268 File Offset: 0x00083468
	// (set) Token: 0x06000E53 RID: 3667 RVA: 0x000852B2 File Offset: 0x000834B2
	public Text Right_Stunt_Location_Index_Text_1
	{
		get
		{
			bool flag = this._Right_Stunt_Location_Index_Text_1 == null;
			if (flag)
			{
				this._Right_Stunt_Location_Index_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Stunt_Location_1/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Stunt_Location_Index_Text_1;
		}
		set
		{
			this._Right_Stunt_Location_Index_Text_1 = value;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000852BC File Offset: 0x000834BC
	// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00085306 File Offset: 0x00083506
	public Text Right_Stunt_Location_ID_Text_1
	{
		get
		{
			bool flag = this._Right_Stunt_Location_ID_Text_1 == null;
			if (flag)
			{
				this._Right_Stunt_Location_ID_Text_1 = this.Right_Equip_View_Object.transform.FindChild("img_Equip_Stunt_Location_1/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Stunt_Location_ID_Text_1;
		}
		set
		{
			this._Right_Stunt_Location_ID_Text_1 = value;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00085310 File Offset: 0x00083510
	// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0008535A File Offset: 0x0008355A
	public GameObject Right_Equip_Effect_Object
	{
		get
		{
			bool flag = this._Right_Equip_Effect_Object == null;
			if (flag)
			{
				this._Right_Equip_Effect_Object = this.Right_Equip_View_Object.transform.FindChild("Equip_Effect").gameObject;
			}
			return this._Right_Equip_Effect_Object;
		}
		set
		{
			this._Right_Equip_Effect_Object = value;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00085364 File Offset: 0x00083564
	// (set) Token: 0x06000E59 RID: 3673 RVA: 0x000853AE File Offset: 0x000835AE
	public Toggle Right_Display_Synthetic_Toggle
	{
		get
		{
			bool flag = this._Right_Display_Synthetic_Toggle == null;
			if (flag)
			{
				this._Right_Display_Synthetic_Toggle = this.Right_Container.transform.FindChild("btn_Display_Synthetic").GetComponent<Toggle>();
			}
			return this._Right_Display_Synthetic_Toggle;
		}
		set
		{
			this._Right_Display_Synthetic_Toggle = value;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000853B8 File Offset: 0x000835B8
	// (set) Token: 0x06000E5B RID: 3675 RVA: 0x00085402 File Offset: 0x00083602
	public GameObject Right_Synthetic_View_Object
	{
		get
		{
			bool flag = this._Right_Synthetic_View_Object == null;
			if (flag)
			{
				this._Right_Synthetic_View_Object = this.Right_Container.transform.FindChild("Synthetic").gameObject;
			}
			return this._Right_Synthetic_View_Object;
		}
		set
		{
			this._Right_Synthetic_View_Object = value;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0008540C File Offset: 0x0008360C
	// (set) Token: 0x06000E5D RID: 3677 RVA: 0x00085456 File Offset: 0x00083656
	public Image Right_Synthetic_Location_Icon_Image_1
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Icon_Image_1 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Icon_Image_1 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_1/img_Icon").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Icon_Image_1;
		}
		set
		{
			this._Right_Synthetic_Location_Icon_Image_1 = value;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00085460 File Offset: 0x00083660
	// (set) Token: 0x06000E5F RID: 3679 RVA: 0x000854AA File Offset: 0x000836AA
	public Image Right_Synthetic_Location_Quality_Image_1
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Quality_Image_1 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Quality_Image_1 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_1/img_Quality").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Quality_Image_1;
		}
		set
		{
			this._Right_Synthetic_Location_Quality_Image_1 = value;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000E60 RID: 3680 RVA: 0x000854B4 File Offset: 0x000836B4
	// (set) Token: 0x06000E61 RID: 3681 RVA: 0x000854FE File Offset: 0x000836FE
	public Text Right_Synthetic_Location_Index_Text_1
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Index_Text_1 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Index_Text_1 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_1/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_Index_Text_1;
		}
		set
		{
			this._Right_Synthetic_Location_Index_Text_1 = value;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00085508 File Offset: 0x00083708
	// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00085552 File Offset: 0x00083752
	public Text Right_Synthetic_Location_ID_Text_1
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_ID_Text_1 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_ID_Text_1 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_1/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_ID_Text_1;
		}
		set
		{
			this._Right_Synthetic_Location_ID_Text_1 = value;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0008555C File Offset: 0x0008375C
	// (set) Token: 0x06000E65 RID: 3685 RVA: 0x000855A6 File Offset: 0x000837A6
	public Image Right_Synthetic_Location_Icon_Image_2
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Icon_Image_2 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Icon_Image_2 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_2/img_Icon").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Icon_Image_2;
		}
		set
		{
			this._Right_Synthetic_Location_Icon_Image_2 = value;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000855B0 File Offset: 0x000837B0
	// (set) Token: 0x06000E67 RID: 3687 RVA: 0x000855FA File Offset: 0x000837FA
	public Image Right_Synthetic_Location_Quality_Image_2
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Quality_Image_2 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Quality_Image_2 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_2/img_Quality").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Quality_Image_2;
		}
		set
		{
			this._Right_Synthetic_Location_Quality_Image_2 = value;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00085604 File Offset: 0x00083804
	// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0008564E File Offset: 0x0008384E
	public Text Right_Synthetic_Location_Index_Text_2
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Index_Text_2 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Index_Text_2 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_2/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_Index_Text_2;
		}
		set
		{
			this._Right_Synthetic_Location_Index_Text_2 = value;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00085658 File Offset: 0x00083858
	// (set) Token: 0x06000E6B RID: 3691 RVA: 0x000856A2 File Offset: 0x000838A2
	public Text Right_Synthetic_Location_ID_Text_2
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_ID_Text_2 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_ID_Text_2 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_2/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_ID_Text_2;
		}
		set
		{
			this._Right_Synthetic_Location_ID_Text_2 = value;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000E6C RID: 3692 RVA: 0x000856AC File Offset: 0x000838AC
	// (set) Token: 0x06000E6D RID: 3693 RVA: 0x000856F6 File Offset: 0x000838F6
	public Image Right_Synthetic_Location_Icon_Image_3
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Icon_Image_3 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Icon_Image_3 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_3/img_Icon").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Icon_Image_3;
		}
		set
		{
			this._Right_Synthetic_Location_Icon_Image_3 = value;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00085700 File Offset: 0x00083900
	// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0008574A File Offset: 0x0008394A
	public Image Right_Synthetic_Location_Quality_Image_3
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Quality_Image_3 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Quality_Image_3 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_3/img_Quality").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Quality_Image_3;
		}
		set
		{
			this._Right_Synthetic_Location_Quality_Image_3 = value;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00085754 File Offset: 0x00083954
	// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0008579E File Offset: 0x0008399E
	public Text Right_Synthetic_Location_Index_Text_3
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Index_Text_3 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Index_Text_3 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_3/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_Index_Text_3;
		}
		set
		{
			this._Right_Synthetic_Location_Index_Text_3 = value;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000857A8 File Offset: 0x000839A8
	// (set) Token: 0x06000E73 RID: 3699 RVA: 0x000857F2 File Offset: 0x000839F2
	public Text Right_Synthetic_Location_ID_Text_3
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_ID_Text_3 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_ID_Text_3 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_3/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_ID_Text_3;
		}
		set
		{
			this._Right_Synthetic_Location_ID_Text_3 = value;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000857FC File Offset: 0x000839FC
	// (set) Token: 0x06000E75 RID: 3701 RVA: 0x00085846 File Offset: 0x00083A46
	public Image Right_Synthetic_Location_Icon_Image_4
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Icon_Image_4 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Icon_Image_4 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_4/img_Icon").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Icon_Image_4;
		}
		set
		{
			this._Right_Synthetic_Location_Icon_Image_4 = value;
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00085850 File Offset: 0x00083A50
	// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0008589A File Offset: 0x00083A9A
	public Image Right_Synthetic_Location_Quality_Image_4
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Quality_Image_4 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Quality_Image_4 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_4/img_Quality").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Quality_Image_4;
		}
		set
		{
			this._Right_Synthetic_Location_Quality_Image_4 = value;
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000858A4 File Offset: 0x00083AA4
	// (set) Token: 0x06000E79 RID: 3705 RVA: 0x000858EE File Offset: 0x00083AEE
	public Text Right_Synthetic_Location_Index_Text_4
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Index_Text_4 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Index_Text_4 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_4/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_Index_Text_4;
		}
		set
		{
			this._Right_Synthetic_Location_Index_Text_4 = value;
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000858F8 File Offset: 0x00083AF8
	// (set) Token: 0x06000E7B RID: 3707 RVA: 0x00085942 File Offset: 0x00083B42
	public Text Right_Synthetic_Location_ID_Text_4
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_ID_Text_4 == null;
			if (flag)
			{
				this._Right_Synthetic_Location_ID_Text_4 = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_4/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_ID_Text_4;
		}
		set
		{
			this._Right_Synthetic_Location_ID_Text_4 = value;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0008594C File Offset: 0x00083B4C
	// (set) Token: 0x06000E7D RID: 3709 RVA: 0x00085996 File Offset: 0x00083B96
	public Image Right_Synthetic_Location_Icon_Image_New
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Icon_Image_New == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Icon_Image_New = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/img_Icon").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Icon_Image_New;
		}
		set
		{
			this._Right_Synthetic_Location_Icon_Image_New = value;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000859A0 File Offset: 0x00083BA0
	// (set) Token: 0x06000E7F RID: 3711 RVA: 0x000859EA File Offset: 0x00083BEA
	public Image Right_Synthetic_Location_Quality_Image_New
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Quality_Image_New == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Quality_Image_New = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/img_Quality").GetComponent<Image>();
			}
			return this._Right_Synthetic_Location_Quality_Image_New;
		}
		set
		{
			this._Right_Synthetic_Location_Quality_Image_New = value;
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000E80 RID: 3712 RVA: 0x000859F4 File Offset: 0x00083BF4
	// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00085A3E File Offset: 0x00083C3E
	public Text Right_Synthetic_Location_Index_Text_New
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_Index_Text_New == null;
			if (flag)
			{
				this._Right_Synthetic_Location_Index_Text_New = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_Index_Text_New;
		}
		set
		{
			this._Right_Synthetic_Location_Index_Text_New = value;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00085A48 File Offset: 0x00083C48
	// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00085A92 File Offset: 0x00083C92
	public Text Right_Synthetic_Location_ID_Text_New
	{
		get
		{
			bool flag = this._Right_Synthetic_Location_ID_Text_New == null;
			if (flag)
			{
				this._Right_Synthetic_Location_ID_Text_New = this.Right_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Synthetic_Location_ID_Text_New;
		}
		set
		{
			this._Right_Synthetic_Location_ID_Text_New = value;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00085A9C File Offset: 0x00083C9C
	// (set) Token: 0x06000E85 RID: 3717 RVA: 0x00085AE6 File Offset: 0x00083CE6
	public Text Right_Synthetic_Success_Rate_Describe_Text
	{
		get
		{
			bool flag = this._Right_Synthetic_Success_Rate_Describe_Text == null;
			if (flag)
			{
				this._Right_Synthetic_Success_Rate_Describe_Text = this.Right_Synthetic_View_Object.transform.FindChild("lbl_Success_Rate").GetComponent<Text>();
			}
			return this._Right_Synthetic_Success_Rate_Describe_Text;
		}
		set
		{
			this._Right_Synthetic_Success_Rate_Describe_Text = value;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00085AF0 File Offset: 0x00083CF0
	// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00085B3A File Offset: 0x00083D3A
	public GameObject Right_Synthetic_Describe_Object
	{
		get
		{
			bool flag = this._Right_Synthetic_Describe_Object == null;
			if (flag)
			{
				this._Right_Synthetic_Describe_Object = this.Right_Synthetic_View_Object.transform.FindChild("lbl_Describe").gameObject;
			}
			return this._Right_Synthetic_Describe_Object;
		}
		set
		{
			this._Right_Synthetic_Describe_Object = value;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00085B44 File Offset: 0x00083D44
	// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00085B8E File Offset: 0x00083D8E
	public GameObject Right_Synthetic_Describe_Tips_Object
	{
		get
		{
			bool flag = this._Right_Synthetic_Describe_Tips_Object == null;
			if (flag)
			{
				this._Right_Synthetic_Describe_Tips_Object = this.Right_Synthetic_View_Object.transform.FindChild("Describe_Tips").gameObject;
			}
			return this._Right_Synthetic_Describe_Tips_Object;
		}
		set
		{
			this._Right_Synthetic_Describe_Tips_Object = value;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00085B98 File Offset: 0x00083D98
	// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00085BE2 File Offset: 0x00083DE2
	public Button Right_Synthetic_Button
	{
		get
		{
			bool flag = this._Right_Synthetic_Button == null;
			if (flag)
			{
				this._Right_Synthetic_Button = this.Right_Synthetic_View_Object.transform.FindChild("btn_Synthetic").GetComponent<Button>();
			}
			return this._Right_Synthetic_Button;
		}
		set
		{
			this._Right_Synthetic_Button = value;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00085BEC File Offset: 0x00083DEC
	// (set) Token: 0x06000E8D RID: 3725 RVA: 0x00085C36 File Offset: 0x00083E36
	public GameObject Right_Synthetic_Effect_Object
	{
		get
		{
			bool flag = this._Right_Synthetic_Effect_Object == null;
			if (flag)
			{
				this._Right_Synthetic_Effect_Object = this.Right_Synthetic_View_Object.transform.FindChild("Synthetic_Effect").gameObject;
			}
			return this._Right_Synthetic_Effect_Object;
		}
		set
		{
			this._Right_Synthetic_Effect_Object = value;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00085C40 File Offset: 0x00083E40
	// (set) Token: 0x06000E8F RID: 3727 RVA: 0x00085C8A File Offset: 0x00083E8A
	public GameObject Right_Synthetic_Into_Package_Effect_Object
	{
		get
		{
			bool flag = this._Right_Synthetic_Into_Package_Effect_Object == null;
			if (flag)
			{
				this._Right_Synthetic_Into_Package_Effect_Object = this.Right_Synthetic_View_Object.transform.FindChild("Into_Package_Effect").gameObject;
			}
			return this._Right_Synthetic_Into_Package_Effect_Object;
		}
		set
		{
			this._Right_Synthetic_Into_Package_Effect_Object = value;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00085C94 File Offset: 0x00083E94
	// (set) Token: 0x06000E91 RID: 3729 RVA: 0x00085CDE File Offset: 0x00083EDE
	public Toggle Right_Display_Package_Toggle
	{
		get
		{
			bool flag = this._Right_Display_Package_Toggle == null;
			if (flag)
			{
				this._Right_Display_Package_Toggle = this.Right_Container.transform.FindChild("btn_Display_Package").GetComponent<Toggle>();
			}
			return this._Right_Display_Package_Toggle;
		}
		set
		{
			this._Right_Display_Package_Toggle = value;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00085CE8 File Offset: 0x00083EE8
	// (set) Token: 0x06000E93 RID: 3731 RVA: 0x00085D32 File Offset: 0x00083F32
	public Toggle Right_Display_Sell_Toggle
	{
		get
		{
			bool flag = this._Right_Display_Sell_Toggle == null;
			if (flag)
			{
				this._Right_Display_Sell_Toggle = this.Right_Container.transform.FindChild("btn_Display_Sell").GetComponent<Toggle>();
			}
			return this._Right_Display_Sell_Toggle;
		}
		set
		{
			this._Right_Display_Sell_Toggle = value;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00085D3C File Offset: 0x00083F3C
	// (set) Token: 0x06000E95 RID: 3733 RVA: 0x00085D86 File Offset: 0x00083F86
	public Dropdown Right_Type_Dropdown
	{
		get
		{
			bool flag = this._Right_Type_Dropdown == null;
			if (flag)
			{
				this._Right_Type_Dropdown = this.Right_Container.transform.FindChild("ddl_Type").GetComponent<Dropdown>();
			}
			return this._Right_Type_Dropdown;
		}
		set
		{
			this._Right_Type_Dropdown = value;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00085D90 File Offset: 0x00083F90
	// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00085DDA File Offset: 0x00083FDA
	public Dropdown Right_Quality_Dropdown
	{
		get
		{
			bool flag = this._Right_Quality_Dropdown == null;
			if (flag)
			{
				this._Right_Quality_Dropdown = this.Right_Container.transform.FindChild("ddl_Quality").GetComponent<Dropdown>();
			}
			return this._Right_Quality_Dropdown;
		}
		set
		{
			this._Right_Quality_Dropdown = value;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00085DE4 File Offset: 0x00083FE4
	// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00085E2E File Offset: 0x0008402E
	public GameObject Right_Package_View_Object
	{
		get
		{
			bool flag = this._Right_Package_View_Object == null;
			if (flag)
			{
				this._Right_Package_View_Object = this.Right_Container.transform.FindChild("Package").gameObject;
			}
			return this._Right_Package_View_Object;
		}
		set
		{
			this._Right_Package_View_Object = value;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00085E38 File Offset: 0x00084038
	// (set) Token: 0x06000E9B RID: 3739 RVA: 0x00085E82 File Offset: 0x00084082
	public GameObject Right_Package_Row_Object
	{
		get
		{
			bool flag = this._Right_Package_Row_Object == null;
			if (flag)
			{
				this._Right_Package_Row_Object = this.Right_Package_View_Object.transform.FindChild("Row").gameObject;
			}
			return this._Right_Package_Row_Object;
		}
		set
		{
			this._Right_Package_Row_Object = value;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00085E8C File Offset: 0x0008408C
	// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00085EE3 File Offset: 0x000840E3
	public GameObject Right_Package_Drag_Area_Object
	{
		get
		{
			bool flag = this._Right_Package_Drag_Area_Object == null;
			if (flag)
			{
				this._Right_Package_Drag_Area_Object = this.Right_Package_View_Object.transform.FindChild("Drag_Area").gameObject;
				this._Right_Package_Drag_Area_Object.SetActive(false);
			}
			return this._Right_Package_Drag_Area_Object;
		}
		set
		{
			this._Right_Package_Drag_Area_Object = value;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00085EF0 File Offset: 0x000840F0
	// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00085F3A File Offset: 0x0008413A
	public GameObject Right_Package_Money_Position_Object
	{
		get
		{
			bool flag = this._Right_Package_Money_Position_Object == null;
			if (flag)
			{
				this._Right_Package_Money_Position_Object = this.Right_Package_View_Object.transform.FindChild("Money_Position").gameObject;
			}
			return this._Right_Package_Money_Position_Object;
		}
		set
		{
			this._Right_Package_Money_Position_Object = value;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00085F44 File Offset: 0x00084144
	// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00085F8E File Offset: 0x0008418E
	public GameObject Right_Package_Cell_Object_1
	{
		get
		{
			bool flag = this._Right_Package_Cell_Object_1 == null;
			if (flag)
			{
				this._Right_Package_Cell_Object_1 = this.Right_Package_Row_Object.transform.FindChild("Cell_1").gameObject;
			}
			return this._Right_Package_Cell_Object_1;
		}
		set
		{
			this._Right_Package_Cell_Object_1 = value;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00085F98 File Offset: 0x00084198
	// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x00085FE2 File Offset: 0x000841E2
	public GameObject Right_Package_Cell_Object_2
	{
		get
		{
			bool flag = this._Right_Package_Cell_Object_2 == null;
			if (flag)
			{
				this._Right_Package_Cell_Object_2 = this.Right_Package_Row_Object.transform.FindChild("Cell_2").gameObject;
			}
			return this._Right_Package_Cell_Object_2;
		}
		set
		{
			this._Right_Package_Cell_Object_2 = value;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00085FEC File Offset: 0x000841EC
	// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00086036 File Offset: 0x00084236
	public GameObject Right_Package_Cell_Object_3
	{
		get
		{
			bool flag = this._Right_Package_Cell_Object_3 == null;
			if (flag)
			{
				this._Right_Package_Cell_Object_3 = this.Right_Package_Row_Object.transform.FindChild("Cell_3").gameObject;
			}
			return this._Right_Package_Cell_Object_3;
		}
		set
		{
			this._Right_Package_Cell_Object_3 = value;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00086040 File Offset: 0x00084240
	// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0008608A File Offset: 0x0008428A
	public GameObject Right_Page_Object
	{
		get
		{
			bool flag = this._Right_Page_Object == null;
			if (flag)
			{
				this._Right_Page_Object = this.Right_Container.transform.FindChild("Page").gameObject;
			}
			return this._Right_Page_Object;
		}
		set
		{
			this._Right_Page_Object = value;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00086094 File Offset: 0x00084294
	// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x000860DE File Offset: 0x000842DE
	public Button Right_Prev_Button
	{
		get
		{
			bool flag = this._Right_Prev_Button == null;
			if (flag)
			{
				this._Right_Prev_Button = this.Right_Page_Object.transform.FindChild("btn_Prev").GetComponent<Button>();
			}
			return this._Right_Prev_Button;
		}
		set
		{
			this._Right_Prev_Button = value;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000EAA RID: 3754 RVA: 0x000860E8 File Offset: 0x000842E8
	// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00086132 File Offset: 0x00084332
	public Text Right_Page_Text
	{
		get
		{
			bool flag = this._Right_Page_Text == null;
			if (flag)
			{
				this._Right_Page_Text = this.Right_Page_Object.transform.FindChild("lbl_Page").GetComponent<Text>();
			}
			return this._Right_Page_Text;
		}
		set
		{
			this._Right_Page_Text = value;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0008613C File Offset: 0x0008433C
	// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00086186 File Offset: 0x00084386
	public Button Right_Next_Button
	{
		get
		{
			bool flag = this._Right_Next_Button == null;
			if (flag)
			{
				this._Right_Next_Button = this.Right_Page_Object.transform.FindChild("btn_Next").GetComponent<Button>();
			}
			return this._Right_Next_Button;
		}
		set
		{
			this._Right_Next_Button = value;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00086190 File Offset: 0x00084390
	// (set) Token: 0x06000EAF RID: 3759 RVA: 0x000861DA File Offset: 0x000843DA
	public Button Right_Item_Select_All_Button
	{
		get
		{
			bool flag = this._Right_Item_Select_All_Button == null;
			if (flag)
			{
				this._Right_Item_Select_All_Button = this.Right_Container.transform.FindChild("btn_Item_Select_All").GetComponent<Button>();
			}
			return this._Right_Item_Select_All_Button;
		}
		set
		{
			this._Right_Item_Select_All_Button = value;
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000861E4 File Offset: 0x000843E4
	// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0008622E File Offset: 0x0008442E
	public Button Right_Sell_Select_All_Button
	{
		get
		{
			bool flag = this._Right_Sell_Select_All_Button == null;
			if (flag)
			{
				this._Right_Sell_Select_All_Button = this.Right_Container.transform.FindChild("btn_Sell_Select_All").GetComponent<Button>();
			}
			return this._Right_Sell_Select_All_Button;
		}
		set
		{
			this._Right_Sell_Select_All_Button = value;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00086238 File Offset: 0x00084438
	// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00086282 File Offset: 0x00084482
	public Button Right_Cancel_Select_All_Button
	{
		get
		{
			bool flag = this._Right_Cancel_Select_All_Button == null;
			if (flag)
			{
				this._Right_Cancel_Select_All_Button = this.Right_Container.transform.FindChild("btn_Cancel_Select_All").GetComponent<Button>();
			}
			return this._Right_Cancel_Select_All_Button;
		}
		set
		{
			this._Right_Cancel_Select_All_Button = value;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0008628C File Offset: 0x0008448C
	// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x000862D6 File Offset: 0x000844D6
	public GameObject Right_Sell_Add_Money_Text_Object
	{
		get
		{
			bool flag = this._Right_Sell_Add_Money_Text_Object == null;
			if (flag)
			{
				this._Right_Sell_Add_Money_Text_Object = this.Right_Package_View_Object.transform.FindChild("Add_Money_Text").gameObject;
			}
			return this._Right_Sell_Add_Money_Text_Object;
		}
		set
		{
			this._Right_Sell_Add_Money_Text_Object = value;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000862E0 File Offset: 0x000844E0
	// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x0008632A File Offset: 0x0008452A
	public Toggle Right_Display_Batch_Synthetic_Toggle
	{
		get
		{
			bool flag = this._Right_Display_Batch_Synthetic_Toggle == null;
			if (flag)
			{
				this._Right_Display_Batch_Synthetic_Toggle = this.Right_Container.transform.FindChild("btn_Display_Batch_Synthetic").GetComponent<Toggle>();
			}
			return this._Right_Display_Batch_Synthetic_Toggle;
		}
		set
		{
			this._Right_Display_Batch_Synthetic_Toggle = value;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00086334 File Offset: 0x00084534
	// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x0008637E File Offset: 0x0008457E
	public GameObject Right_Batch_Synthetic_View_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_View_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_View_Object = this.Right_Container.transform.FindChild("Batch_Synthetic").gameObject;
			}
			return this._Right_Batch_Synthetic_View_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_View_Object = value;
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00086388 File Offset: 0x00084588
	// (set) Token: 0x06000EBB RID: 3771 RVA: 0x000863D2 File Offset: 0x000845D2
	public GameObject Right_Batch_Synthetic_Tips_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Tips_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Tips_Object = base.gameObject.transform.FindChild("Tips").gameObject;
			}
			return this._Right_Batch_Synthetic_Tips_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_Tips_Object = value;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000863DC File Offset: 0x000845DC
	// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00086417 File Offset: 0x00084617
	public RawImage Right_Batch_Synthetic_Tips_RawImage
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Tips_RawImage == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Tips_RawImage = this.Right_Batch_Synthetic_Tips_Object.GetComponent<RawImage>();
			}
			return this._Right_Batch_Synthetic_Tips_RawImage;
		}
		set
		{
			this._Right_Batch_Synthetic_Tips_RawImage = value;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00086424 File Offset: 0x00084624
	// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0008646E File Offset: 0x0008466E
	public Button Right_Batch_Synthetic_Tips_Button_1
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Tips_Button_1 == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Tips_Button_1 = this.Right_Batch_Synthetic_Tips_Object.transform.FindChild("Tips/btn_Button_1").GetComponent<Button>();
			}
			return this._Right_Batch_Synthetic_Tips_Button_1;
		}
		set
		{
			this._Right_Batch_Synthetic_Tips_Button_1 = value;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00086478 File Offset: 0x00084678
	// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x000864C2 File Offset: 0x000846C2
	public Button Right_Batch_Synthetic_Tips_Button_2
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Tips_Button_2 == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Tips_Button_2 = this.Right_Batch_Synthetic_Tips_Object.transform.FindChild("Tips/btn_Button_2").GetComponent<Button>();
			}
			return this._Right_Batch_Synthetic_Tips_Button_2;
		}
		set
		{
			this._Right_Batch_Synthetic_Tips_Button_2 = value;
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000864CC File Offset: 0x000846CC
	// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00086516 File Offset: 0x00084716
	public Button Right_Batch_Synthetic_Tips_Button_3
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Tips_Button_3 == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Tips_Button_3 = this.Right_Batch_Synthetic_Tips_Object.transform.FindChild("Tips/btn_Button_3").GetComponent<Button>();
			}
			return this._Right_Batch_Synthetic_Tips_Button_3;
		}
		set
		{
			this._Right_Batch_Synthetic_Tips_Button_3 = value;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00086520 File Offset: 0x00084720
	// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0008656A File Offset: 0x0008476A
	public Button Right_Batch_Synthetic_Button
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Button == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Button = this.Right_Batch_Synthetic_View_Object.transform.FindChild("btn_Batch_Synthetic").GetComponent<Button>();
			}
			return this._Right_Batch_Synthetic_Button;
		}
		set
		{
			this._Right_Batch_Synthetic_Button = value;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00086574 File Offset: 0x00084774
	// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x000865BE File Offset: 0x000847BE
	public GameObject Right_Batch_Synthetic_Effect_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Effect_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Effect_Object = this.Right_Batch_Synthetic_View_Object.transform.FindChild("Synthetic_Effect").gameObject;
			}
			return this._Right_Batch_Synthetic_Effect_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_Effect_Object = value;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000865C8 File Offset: 0x000847C8
	// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00086612 File Offset: 0x00084812
	public GameObject Right_Batch_Synthetic_Into_Package_Effect_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Into_Package_Effect_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Into_Package_Effect_Object = this.Right_Batch_Synthetic_View_Object.transform.FindChild("Into_Package_Effect").gameObject;
			}
			return this._Right_Batch_Synthetic_Into_Package_Effect_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_Into_Package_Effect_Object = value;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0008661C File Offset: 0x0008481C
	// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00086666 File Offset: 0x00084866
	public Image Right_Batch_Synthetic_Location_Icon_Image
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Icon_Image == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Icon_Image = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location/img_Icon").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Location_Icon_Image;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Icon_Image = value;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00086670 File Offset: 0x00084870
	// (set) Token: 0x06000ECD RID: 3789 RVA: 0x000866BA File Offset: 0x000848BA
	public GameObject Right_Batch_Synthetic_Location_Amount_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Amount_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Amount_Object = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location/Amount").gameObject;
			}
			return this._Right_Batch_Synthetic_Location_Amount_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Amount_Object = value;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000866C4 File Offset: 0x000848C4
	// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0008670E File Offset: 0x0008490E
	public Text Right_Batch_Synthetic_Location_Amount_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Amount_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Amount_Text = this.Right_Batch_Synthetic_Location_Amount_Object.transform.FindChild("lbl_Amount").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Location_Amount_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Amount_Text = value;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00086718 File Offset: 0x00084918
	// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00086762 File Offset: 0x00084962
	public Image Right_Batch_Synthetic_Location_Quality_Image
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Quality_Image == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Quality_Image = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location/img_Quality").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Location_Quality_Image;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Quality_Image = value;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0008676C File Offset: 0x0008496C
	// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000867B6 File Offset: 0x000849B6
	public Text Right_Batch_Synthetic_Location_Index_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Index_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Index_Text = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Location_Index_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Index_Text = value;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000867C0 File Offset: 0x000849C0
	// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x0008680A File Offset: 0x00084A0A
	public Text Right_Batch_Synthetic_Location_ID_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_ID_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_ID_Text = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Location_ID_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_ID_Text = value;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00086814 File Offset: 0x00084A14
	// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x0008685E File Offset: 0x00084A5E
	public Image Right_Batch_Synthetic_Success_Rate_Icon_Image
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Icon_Image == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Icon_Image = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Success_Rate/img_Icon").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_Icon_Image;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Icon_Image = value;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00086868 File Offset: 0x00084A68
	// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x000868B2 File Offset: 0x00084AB2
	public GameObject Right_Batch_Synthetic_Success_Rate_Amount_Object
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Amount_Object == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Amount_Object = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Success_Rate/Amount").gameObject;
			}
			return this._Right_Batch_Synthetic_Success_Rate_Amount_Object;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Amount_Object = value;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000868BC File Offset: 0x00084ABC
	// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00086906 File Offset: 0x00084B06
	public Text Right_Batch_Synthetic_Success_Rate_Amount_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Amount_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Amount_Text = this.Right_Batch_Synthetic_Success_Rate_Amount_Object.transform.FindChild("lbl_Amount").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_Amount_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Amount_Text = value;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00086910 File Offset: 0x00084B10
	// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0008695A File Offset: 0x00084B5A
	public Image Right_Batch_Synthetic_Success_Rate_Quality_Image
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Quality_Image == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Quality_Image = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Success_Rate/img_Quality").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_Quality_Image;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Quality_Image = value;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00086964 File Offset: 0x00084B64
	// (set) Token: 0x06000EDF RID: 3807 RVA: 0x000869AE File Offset: 0x00084BAE
	public Text Right_Batch_Synthetic_Success_Rate_Index_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Index_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Index_Text = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Success_Rate/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_Index_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Index_Text = value;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000869B8 File Offset: 0x00084BB8
	// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00086A02 File Offset: 0x00084C02
	public Text Right_Batch_Synthetic_Success_Rate_ID_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_ID_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_ID_Text = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Success_Rate/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_ID_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_ID_Text = value;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00086A0C File Offset: 0x00084C0C
	// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00086A56 File Offset: 0x00084C56
	public Text Right_Batch_Synthetic_Success_Rate_Describe_Text
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Success_Rate_Describe_Text == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Success_Rate_Describe_Text = this.Right_Batch_Synthetic_View_Object.transform.FindChild("lbl_Success_Rate").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Success_Rate_Describe_Text;
		}
		set
		{
			this._Right_Batch_Synthetic_Success_Rate_Describe_Text = value;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00086A60 File Offset: 0x00084C60
	// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00086AAA File Offset: 0x00084CAA
	public Image Right_Batch_Synthetic_Location_Icon_Image_New
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Icon_Image_New == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Icon_Image_New = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/img_Icon").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Location_Icon_Image_New;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Icon_Image_New = value;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00086AB4 File Offset: 0x00084CB4
	// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x00086AFE File Offset: 0x00084CFE
	public Image Right_Batch_Synthetic_Location_Quality_Image_New
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Quality_Image_New == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Quality_Image_New = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/img_Quality").GetComponent<Image>();
			}
			return this._Right_Batch_Synthetic_Location_Quality_Image_New;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Quality_Image_New = value;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00086B08 File Offset: 0x00084D08
	// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00086B52 File Offset: 0x00084D52
	public Text Right_Batch_Synthetic_Location_Index_Text_New
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_Index_Text_New == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_Index_Text_New = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/lbl_Index").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Location_Index_Text_New;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_Index_Text_New = value;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00086B5C File Offset: 0x00084D5C
	// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00086BA6 File Offset: 0x00084DA6
	public Text Right_Batch_Synthetic_Location_ID_Text_New
	{
		get
		{
			bool flag = this._Right_Batch_Synthetic_Location_ID_Text_New == null;
			if (flag)
			{
				this._Right_Batch_Synthetic_Location_ID_Text_New = this.Right_Batch_Synthetic_View_Object.transform.FindChild("img_Synthetic_Location_New/lbl_ID").GetComponent<Text>();
			}
			return this._Right_Batch_Synthetic_Location_ID_Text_New;
		}
		set
		{
			this._Right_Batch_Synthetic_Location_ID_Text_New = value;
		}
	}

	// Token: 0x04000B62 RID: 2914
	private GameObject _Left_Container;

	// Token: 0x04000B63 RID: 2915
	private GridLayoutGroup _Left_Grid;

	// Token: 0x04000B64 RID: 2916
	private GameObject _Left_Item_Object;

	// Token: 0x04000B65 RID: 2917
	private Button _Left_Buy_Button;

	// Token: 0x04000B66 RID: 2918
	private GameObject _Right_Container;

	// Token: 0x04000B67 RID: 2919
	private Toggle _Right_Display_Equip_Toggle;

	// Token: 0x04000B68 RID: 2920
	private GameObject _Right_Equip_View_Object;

	// Token: 0x04000B69 RID: 2921
	private Image _Right_Attack_Location_Icon_Image_1;

	// Token: 0x04000B6A RID: 2922
	private Image _Right_Attack_Location_Quality_Image_1;

	// Token: 0x04000B6B RID: 2923
	private Text _Right_Attack_Location_Index_Text_1;

	// Token: 0x04000B6C RID: 2924
	private Text _Right_Attack_Location_ID_Text_1;

	// Token: 0x04000B6D RID: 2925
	private Image _Right_Attack_Location_Icon_Image_2;

	// Token: 0x04000B6E RID: 2926
	private Image _Right_Attack_Location_Quality_Image_2;

	// Token: 0x04000B6F RID: 2927
	private Text _Right_Attack_Location_Index_Text_2;

	// Token: 0x04000B70 RID: 2928
	private Text _Right_Attack_Location_ID_Text_2;

	// Token: 0x04000B71 RID: 2929
	private Image _Right_Attack_Location_Icon_Image_3;

	// Token: 0x04000B72 RID: 2930
	private Image _Right_Attack_Location_Quality_Image_3;

	// Token: 0x04000B73 RID: 2931
	private Text _Right_Attack_Location_Index_Text_3;

	// Token: 0x04000B74 RID: 2932
	private Text _Right_Attack_Location_ID_Text_3;

	// Token: 0x04000B75 RID: 2933
	private Image _Right_Defend_Location_Icon_Image_1;

	// Token: 0x04000B76 RID: 2934
	private Image _Right_Defend_Location_Quality_Image_1;

	// Token: 0x04000B77 RID: 2935
	private Text _Right_Defend_Location_Index_Text_1;

	// Token: 0x04000B78 RID: 2936
	private Text _Right_Defend_Location_ID_Text_1;

	// Token: 0x04000B79 RID: 2937
	private Image _Right_Defend_Location_Icon_Image_2;

	// Token: 0x04000B7A RID: 2938
	private Image _Right_Defend_Location_Quality_Image_2;

	// Token: 0x04000B7B RID: 2939
	private Text _Right_Defend_Location_Index_Text_2;

	// Token: 0x04000B7C RID: 2940
	private Text _Right_Defend_Location_ID_Text_2;

	// Token: 0x04000B7D RID: 2941
	private Image _Right_Skill_Location_Icon_Image_1;

	// Token: 0x04000B7E RID: 2942
	private Image _Right_Skill_Location_Quality_Image_1;

	// Token: 0x04000B7F RID: 2943
	private Text _Right_Skill_Location_Index_Text_1;

	// Token: 0x04000B80 RID: 2944
	private Text _Right_Skill_Location_ID_Text_1;

	// Token: 0x04000B81 RID: 2945
	private Image _Right_Skill_Location_Icon_Image_2;

	// Token: 0x04000B82 RID: 2946
	private Image _Right_Skill_Location_Quality_Image_2;

	// Token: 0x04000B83 RID: 2947
	private Text _Right_Skill_Location_Index_Text_2;

	// Token: 0x04000B84 RID: 2948
	private Text _Right_Skill_Location_ID_Text_2;

	// Token: 0x04000B85 RID: 2949
	private Image _Right_Stunt_Location_Icon_Image_1;

	// Token: 0x04000B86 RID: 2950
	private Image _Right_Stunt_Location_Quality_Image_1;

	// Token: 0x04000B87 RID: 2951
	private Text _Right_Stunt_Location_Index_Text_1;

	// Token: 0x04000B88 RID: 2952
	private Text _Right_Stunt_Location_ID_Text_1;

	// Token: 0x04000B89 RID: 2953
	private GameObject _Right_Equip_Effect_Object;

	// Token: 0x04000B8A RID: 2954
	private Toggle _Right_Display_Synthetic_Toggle;

	// Token: 0x04000B8B RID: 2955
	private GameObject _Right_Synthetic_View_Object;

	// Token: 0x04000B8C RID: 2956
	private Image _Right_Synthetic_Location_Icon_Image_1;

	// Token: 0x04000B8D RID: 2957
	private Image _Right_Synthetic_Location_Quality_Image_1;

	// Token: 0x04000B8E RID: 2958
	private Text _Right_Synthetic_Location_Index_Text_1;

	// Token: 0x04000B8F RID: 2959
	private Text _Right_Synthetic_Location_ID_Text_1;

	// Token: 0x04000B90 RID: 2960
	private Image _Right_Synthetic_Location_Icon_Image_2;

	// Token: 0x04000B91 RID: 2961
	private Image _Right_Synthetic_Location_Quality_Image_2;

	// Token: 0x04000B92 RID: 2962
	private Text _Right_Synthetic_Location_Index_Text_2;

	// Token: 0x04000B93 RID: 2963
	private Text _Right_Synthetic_Location_ID_Text_2;

	// Token: 0x04000B94 RID: 2964
	private Image _Right_Synthetic_Location_Icon_Image_3;

	// Token: 0x04000B95 RID: 2965
	private Image _Right_Synthetic_Location_Quality_Image_3;

	// Token: 0x04000B96 RID: 2966
	private Text _Right_Synthetic_Location_Index_Text_3;

	// Token: 0x04000B97 RID: 2967
	private Text _Right_Synthetic_Location_ID_Text_3;

	// Token: 0x04000B98 RID: 2968
	private Image _Right_Synthetic_Location_Icon_Image_4;

	// Token: 0x04000B99 RID: 2969
	private Image _Right_Synthetic_Location_Quality_Image_4;

	// Token: 0x04000B9A RID: 2970
	private Text _Right_Synthetic_Location_Index_Text_4;

	// Token: 0x04000B9B RID: 2971
	private Text _Right_Synthetic_Location_ID_Text_4;

	// Token: 0x04000B9C RID: 2972
	private Image _Right_Synthetic_Location_Icon_Image_New;

	// Token: 0x04000B9D RID: 2973
	private Image _Right_Synthetic_Location_Quality_Image_New;

	// Token: 0x04000B9E RID: 2974
	private Text _Right_Synthetic_Location_Index_Text_New;

	// Token: 0x04000B9F RID: 2975
	private Text _Right_Synthetic_Location_ID_Text_New;

	// Token: 0x04000BA0 RID: 2976
	private Text _Right_Synthetic_Success_Rate_Describe_Text;

	// Token: 0x04000BA1 RID: 2977
	private GameObject _Right_Synthetic_Describe_Object;

	// Token: 0x04000BA2 RID: 2978
	private GameObject _Right_Synthetic_Describe_Tips_Object;

	// Token: 0x04000BA3 RID: 2979
	private Button _Right_Synthetic_Button;

	// Token: 0x04000BA4 RID: 2980
	private GameObject _Right_Synthetic_Effect_Object;

	// Token: 0x04000BA5 RID: 2981
	private GameObject _Right_Synthetic_Into_Package_Effect_Object;

	// Token: 0x04000BA6 RID: 2982
	private Toggle _Right_Display_Package_Toggle;

	// Token: 0x04000BA7 RID: 2983
	private Toggle _Right_Display_Sell_Toggle;

	// Token: 0x04000BA8 RID: 2984
	private Dropdown _Right_Type_Dropdown;

	// Token: 0x04000BA9 RID: 2985
	private Dropdown _Right_Quality_Dropdown;

	// Token: 0x04000BAA RID: 2986
	private GameObject _Right_Package_View_Object;

	// Token: 0x04000BAB RID: 2987
	private GameObject _Right_Package_Row_Object;

	// Token: 0x04000BAC RID: 2988
	private GameObject _Right_Package_Drag_Area_Object;

	// Token: 0x04000BAD RID: 2989
	private GameObject _Right_Package_Money_Position_Object;

	// Token: 0x04000BAE RID: 2990
	private GameObject _Right_Package_Cell_Object_1;

	// Token: 0x04000BAF RID: 2991
	private GameObject _Right_Package_Cell_Object_2;

	// Token: 0x04000BB0 RID: 2992
	private GameObject _Right_Package_Cell_Object_3;

	// Token: 0x04000BB1 RID: 2993
	private GameObject _Right_Page_Object;

	// Token: 0x04000BB2 RID: 2994
	private Button _Right_Prev_Button;

	// Token: 0x04000BB3 RID: 2995
	private Text _Right_Page_Text;

	// Token: 0x04000BB4 RID: 2996
	private Button _Right_Next_Button;

	// Token: 0x04000BB5 RID: 2997
	private Button _Right_Item_Select_All_Button;

	// Token: 0x04000BB6 RID: 2998
	private Button _Right_Sell_Select_All_Button;

	// Token: 0x04000BB7 RID: 2999
	private Button _Right_Cancel_Select_All_Button;

	// Token: 0x04000BB8 RID: 3000
	private GameObject _Right_Sell_Add_Money_Text_Object;

	// Token: 0x04000BB9 RID: 3001
	private Toggle _Right_Display_Batch_Synthetic_Toggle;

	// Token: 0x04000BBA RID: 3002
	private GameObject _Right_Batch_Synthetic_View_Object;

	// Token: 0x04000BBB RID: 3003
	private GameObject _Right_Batch_Synthetic_Tips_Object;

	// Token: 0x04000BBC RID: 3004
	private RawImage _Right_Batch_Synthetic_Tips_RawImage;

	// Token: 0x04000BBD RID: 3005
	private Button _Right_Batch_Synthetic_Tips_Button_1;

	// Token: 0x04000BBE RID: 3006
	private Button _Right_Batch_Synthetic_Tips_Button_2;

	// Token: 0x04000BBF RID: 3007
	private Button _Right_Batch_Synthetic_Tips_Button_3;

	// Token: 0x04000BC0 RID: 3008
	private Button _Right_Batch_Synthetic_Button;

	// Token: 0x04000BC1 RID: 3009
	private GameObject _Right_Batch_Synthetic_Effect_Object;

	// Token: 0x04000BC2 RID: 3010
	private GameObject _Right_Batch_Synthetic_Into_Package_Effect_Object;

	// Token: 0x04000BC3 RID: 3011
	private Image _Right_Batch_Synthetic_Location_Icon_Image;

	// Token: 0x04000BC4 RID: 3012
	private GameObject _Right_Batch_Synthetic_Location_Amount_Object;

	// Token: 0x04000BC5 RID: 3013
	private Text _Right_Batch_Synthetic_Location_Amount_Text;

	// Token: 0x04000BC6 RID: 3014
	private Image _Right_Batch_Synthetic_Location_Quality_Image;

	// Token: 0x04000BC7 RID: 3015
	private Text _Right_Batch_Synthetic_Location_Index_Text;

	// Token: 0x04000BC8 RID: 3016
	private Text _Right_Batch_Synthetic_Location_ID_Text;

	// Token: 0x04000BC9 RID: 3017
	private Image _Right_Batch_Synthetic_Success_Rate_Icon_Image;

	// Token: 0x04000BCA RID: 3018
	private GameObject _Right_Batch_Synthetic_Success_Rate_Amount_Object;

	// Token: 0x04000BCB RID: 3019
	private Text _Right_Batch_Synthetic_Success_Rate_Amount_Text;

	// Token: 0x04000BCC RID: 3020
	private Image _Right_Batch_Synthetic_Success_Rate_Quality_Image;

	// Token: 0x04000BCD RID: 3021
	private Text _Right_Batch_Synthetic_Success_Rate_Index_Text;

	// Token: 0x04000BCE RID: 3022
	private Text _Right_Batch_Synthetic_Success_Rate_ID_Text;

	// Token: 0x04000BCF RID: 3023
	private Text _Right_Batch_Synthetic_Success_Rate_Describe_Text;

	// Token: 0x04000BD0 RID: 3024
	private Image _Right_Batch_Synthetic_Location_Icon_Image_New;

	// Token: 0x04000BD1 RID: 3025
	private Image _Right_Batch_Synthetic_Location_Quality_Image_New;

	// Token: 0x04000BD2 RID: 3026
	private Text _Right_Batch_Synthetic_Location_Index_Text_New;

	// Token: 0x04000BD3 RID: 3027
	private Text _Right_Batch_Synthetic_Location_ID_Text_New;
}

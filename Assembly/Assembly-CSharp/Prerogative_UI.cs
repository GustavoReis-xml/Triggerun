using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A4 RID: 676
public class Prerogative_UI : MonoBehaviour
{
	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0011C870 File Offset: 0x0011AA70
	// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x0011C8BF File Offset: 0x0011AABF
	public Button Prerogative_Page_Text
	{
		get
		{
			bool flag = this._Prerogative_Page_Text == null;
			if (flag)
			{
				this._Prerogative_Page_Text = base.gameObject.transform.Find("Prerogative_Page").gameObject.GetComponent<Button>();
			}
			return this._Prerogative_Page_Text;
		}
		set
		{
			this._Prerogative_Page_Text = value;
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0011C8CC File Offset: 0x0011AACC
	// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0011C91B File Offset: 0x0011AB1B
	public Text Describe_Text
	{
		get
		{
			bool flag = this._Describe_Text == null;
			if (flag)
			{
				this._Describe_Text = base.gameObject.transform.Find("lbl_Describe").gameObject.GetComponent<Text>();
			}
			return this._Describe_Text;
		}
		set
		{
			this._Describe_Text = value;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0011C928 File Offset: 0x0011AB28
	// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0011C972 File Offset: 0x0011AB72
	public GameObject Top_Left_Layout_Object
	{
		get
		{
			bool flag = this._Top_Left_Layout_Object == null;
			if (flag)
			{
				this._Top_Left_Layout_Object = base.gameObject.transform.Find("Top_Left").gameObject;
			}
			return this._Top_Left_Layout_Object;
		}
		set
		{
			this._Top_Left_Layout_Object = value;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0011C97C File Offset: 0x0011AB7C
	// (set) Token: 0x06001EBA RID: 7866 RVA: 0x0011C9C6 File Offset: 0x0011ABC6
	public GameObject Top_Left_Grid
	{
		get
		{
			bool flag = this._Top_Left_Grid == null;
			if (flag)
			{
				this._Top_Left_Grid = this.Top_Left_Layout_Object.transform.Find("Grid").gameObject;
			}
			return this._Top_Left_Grid;
		}
		set
		{
			this._Top_Left_Grid = value;
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0011C9D0 File Offset: 0x0011ABD0
	// (set) Token: 0x06001EBC RID: 7868 RVA: 0x0011CA1A File Offset: 0x0011AC1A
	public GameObject Top_Left_Item
	{
		get
		{
			bool flag = this._Top_Left_Item == null;
			if (flag)
			{
				this._Top_Left_Item = this.Top_Left_Layout_Object.transform.Find("Item").gameObject;
			}
			return this._Top_Left_Item;
		}
		set
		{
			this._Top_Left_Item = value;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0011CA24 File Offset: 0x0011AC24
	// (set) Token: 0x06001EBE RID: 7870 RVA: 0x0011CA73 File Offset: 0x0011AC73
	public Button Top_Left_Get_Novice_Reward_Button
	{
		get
		{
			bool flag = this._Top_Left_Get_Novice_Reward_Button == null;
			if (flag)
			{
				this._Top_Left_Get_Novice_Reward_Button = this.Top_Left_Layout_Object.transform.Find("btn_Get_Novice_Reward").gameObject.GetComponent<Button>();
			}
			return this._Top_Left_Get_Novice_Reward_Button;
		}
		set
		{
			this._Top_Left_Get_Novice_Reward_Button = value;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0011CA80 File Offset: 0x0011AC80
	// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x0011CACA File Offset: 0x0011ACCA
	public GameObject Lower_Left_1_Layout_Object
	{
		get
		{
			bool flag = this._Lower_Left_1_Layout_Object == null;
			if (flag)
			{
				this._Lower_Left_1_Layout_Object = base.gameObject.transform.Find("Lower_Left_1").gameObject;
			}
			return this._Lower_Left_1_Layout_Object;
		}
		set
		{
			this._Lower_Left_1_Layout_Object = value;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0011CAD4 File Offset: 0x0011ACD4
	// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x0011CB1E File Offset: 0x0011AD1E
	public GameObject Lower_Left_1_Grid
	{
		get
		{
			bool flag = this._Lower_Left_1_Grid == null;
			if (flag)
			{
				this._Lower_Left_1_Grid = this.Lower_Left_1_Layout_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Lower_Left_1_Grid;
		}
		set
		{
			this._Lower_Left_1_Grid = value;
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0011CB28 File Offset: 0x0011AD28
	// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0011CB72 File Offset: 0x0011AD72
	public RectTransform Lower_Left_1_Content_RectTransform
	{
		get
		{
			bool flag = this._Lower_Left_1_Content_RectTransform == null;
			if (flag)
			{
				this._Lower_Left_1_Content_RectTransform = this.Lower_Left_1_Grid.transform.parent.gameObject.GetComponent<RectTransform>();
			}
			return this._Lower_Left_1_Content_RectTransform;
		}
		set
		{
			this._Lower_Left_1_Content_RectTransform = value;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0011CB7C File Offset: 0x0011AD7C
	// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0011CBC6 File Offset: 0x0011ADC6
	public GameObject Lower_Left_1_Item
	{
		get
		{
			bool flag = this._Lower_Left_1_Item == null;
			if (flag)
			{
				this._Lower_Left_1_Item = this.Lower_Left_1_Layout_Object.transform.Find("Item").gameObject;
			}
			return this._Lower_Left_1_Item;
		}
		set
		{
			this._Lower_Left_1_Item = value;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0011CBD0 File Offset: 0x0011ADD0
	// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x0011CC1A File Offset: 0x0011AE1A
	public GameObject Lower_Left_1_Image_Item
	{
		get
		{
			bool flag = this._Lower_Left_1_Image_Item == null;
			if (flag)
			{
				this._Lower_Left_1_Image_Item = this.Lower_Left_1_Layout_Object.transform.Find("img_Item").gameObject;
			}
			return this._Lower_Left_1_Image_Item;
		}
		set
		{
			this._Lower_Left_1_Image_Item = value;
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0011CC24 File Offset: 0x0011AE24
	// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0011CC73 File Offset: 0x0011AE73
	public Button Lower_Left_Get_Growth_Reward_Button
	{
		get
		{
			bool flag = this._Lower_Left_Get_Growth_Reward_Button == null;
			if (flag)
			{
				this._Lower_Left_Get_Growth_Reward_Button = this.Lower_Left_1_Layout_Object.transform.Find("btn_Get_Growth_Reward").gameObject.GetComponent<Button>();
			}
			return this._Lower_Left_Get_Growth_Reward_Button;
		}
		set
		{
			this._Lower_Left_Get_Growth_Reward_Button = value;
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0011CC80 File Offset: 0x0011AE80
	// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0011CCCA File Offset: 0x0011AECA
	public GameObject Lower_Left_2_Layout_Object
	{
		get
		{
			bool flag = this._Lower_Left_2_Layout_Object == null;
			if (flag)
			{
				this._Lower_Left_2_Layout_Object = base.gameObject.transform.Find("Lower_Left_2").gameObject;
			}
			return this._Lower_Left_2_Layout_Object;
		}
		set
		{
			this._Lower_Left_2_Layout_Object = value;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0011CCD4 File Offset: 0x0011AED4
	// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0011CD1E File Offset: 0x0011AF1E
	public GameObject Lower_Left_2_Grid
	{
		get
		{
			bool flag = this._Lower_Left_2_Grid == null;
			if (flag)
			{
				this._Lower_Left_2_Grid = this.Lower_Left_2_Layout_Object.transform.Find("Grid").gameObject;
			}
			return this._Lower_Left_2_Grid;
		}
		set
		{
			this._Lower_Left_2_Grid = value;
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0011CD28 File Offset: 0x0011AF28
	// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0011CD72 File Offset: 0x0011AF72
	public GameObject Lower_Left_2_Image_Item
	{
		get
		{
			bool flag = this._Lower_Left_2_Image_Item == null;
			if (flag)
			{
				this._Lower_Left_2_Image_Item = this.Lower_Left_2_Layout_Object.transform.Find("img_Item").gameObject;
			}
			return this._Lower_Left_2_Image_Item;
		}
		set
		{
			this._Lower_Left_2_Image_Item = value;
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0011CD7C File Offset: 0x0011AF7C
	// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0011CDC6 File Offset: 0x0011AFC6
	public GameObject Lower_Left_2_Item_1
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_1 == null;
			if (flag)
			{
				this._Lower_Left_2_Item_1 = this.Lower_Left_2_Grid.transform.Find("Item_1").gameObject;
			}
			return this._Lower_Left_2_Item_1;
		}
		set
		{
			this._Lower_Left_2_Item_1 = value;
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0011CDD0 File Offset: 0x0011AFD0
	// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0011CE1F File Offset: 0x0011B01F
	public Text Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text == null;
			if (flag)
			{
				this._Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text = this.Lower_Left_2_Item_1.transform.Find("lbl_Get_Day_Reward_Describe").gameObject.GetComponent<Text>();
			}
			return this._Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text;
		}
		set
		{
			this._Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text = value;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0011CE2C File Offset: 0x0011B02C
	// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0011CE76 File Offset: 0x0011B076
	public GameObject Lower_Left_2_Item_2
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_2 == null;
			if (flag)
			{
				this._Lower_Left_2_Item_2 = this.Lower_Left_2_Grid.transform.Find("Item_2").gameObject;
			}
			return this._Lower_Left_2_Item_2;
		}
		set
		{
			this._Lower_Left_2_Item_2 = value;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0011CE80 File Offset: 0x0011B080
	// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0011CECF File Offset: 0x0011B0CF
	public Text Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text == null;
			if (flag)
			{
				this._Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text = this.Lower_Left_2_Item_2.transform.Find("lbl_Get_Day_Reward_Describe").gameObject.GetComponent<Text>();
			}
			return this._Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text;
		}
		set
		{
			this._Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text = value;
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0011CEDC File Offset: 0x0011B0DC
	// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0011CF26 File Offset: 0x0011B126
	public GameObject Lower_Left_2_Item_3
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_3 == null;
			if (flag)
			{
				this._Lower_Left_2_Item_3 = this.Lower_Left_2_Grid.transform.Find("Item_3").gameObject;
			}
			return this._Lower_Left_2_Item_3;
		}
		set
		{
			this._Lower_Left_2_Item_3 = value;
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0011CF30 File Offset: 0x0011B130
	// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0011CF7F File Offset: 0x0011B17F
	public Text Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text
	{
		get
		{
			bool flag = this._Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text == null;
			if (flag)
			{
				this._Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text = this.Lower_Left_2_Item_3.transform.Find("lbl_Get_Day_Reward_Describe").gameObject.GetComponent<Text>();
			}
			return this._Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text;
		}
		set
		{
			this._Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text = value;
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0011CF8C File Offset: 0x0011B18C
	// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0011CFDB File Offset: 0x0011B1DB
	public Button Lower_Left_Get_Day_Reward_Button
	{
		get
		{
			bool flag = this._Lower_Left_Get_Day_Reward_Button == null;
			if (flag)
			{
				this._Lower_Left_Get_Day_Reward_Button = this.Lower_Left_2_Layout_Object.transform.Find("btn_Get_Day_Reward").gameObject.GetComponent<Button>();
			}
			return this._Lower_Left_Get_Day_Reward_Button;
		}
		set
		{
			this._Lower_Left_Get_Day_Reward_Button = value;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0011CFE8 File Offset: 0x0011B1E8
	// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0011D032 File Offset: 0x0011B232
	public GameObject Top_Right_Layout_Object
	{
		get
		{
			bool flag = this._Top_Right_Layout_Object == null;
			if (flag)
			{
				this._Top_Right_Layout_Object = base.gameObject.transform.Find("Top_Right").gameObject;
			}
			return this._Top_Right_Layout_Object;
		}
		set
		{
			this._Top_Right_Layout_Object = value;
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0011D03C File Offset: 0x0011B23C
	// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x0011D08B File Offset: 0x0011B28B
	public Button Top_Right_Prerogative_Handle_1_Button
	{
		get
		{
			bool flag = this._Top_Right_Prerogative_Handle_1_Button == null;
			if (flag)
			{
				this._Top_Right_Prerogative_Handle_1_Button = this.Top_Right_Layout_Object.transform.Find("btn_Prerogative_Handle_1").gameObject.GetComponent<Button>();
			}
			return this._Top_Right_Prerogative_Handle_1_Button;
		}
		set
		{
			this._Top_Right_Prerogative_Handle_1_Button = value;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0011D098 File Offset: 0x0011B298
	// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0011D0E7 File Offset: 0x0011B2E7
	public Button Top_Right_Prerogative_Handle_2_Button
	{
		get
		{
			bool flag = this._Top_Right_Prerogative_Handle_2_Button == null;
			if (flag)
			{
				this._Top_Right_Prerogative_Handle_2_Button = this.Top_Right_Layout_Object.transform.Find("btn_Prerogative_Handle_2").gameObject.GetComponent<Button>();
			}
			return this._Top_Right_Prerogative_Handle_2_Button;
		}
		set
		{
			this._Top_Right_Prerogative_Handle_2_Button = value;
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0011D0F4 File Offset: 0x0011B2F4
	// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x0011D13E File Offset: 0x0011B33E
	public GameObject Lower_Right_1_Layout_Object
	{
		get
		{
			bool flag = this._Lower_Right_1_Layout_Object == null;
			if (flag)
			{
				this._Lower_Right_1_Layout_Object = base.gameObject.transform.Find("Lower_Right_1").gameObject;
			}
			return this._Lower_Right_1_Layout_Object;
		}
		set
		{
			this._Lower_Right_1_Layout_Object = value;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0011D148 File Offset: 0x0011B348
	// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x0011D192 File Offset: 0x0011B392
	public GameObject Lower_Right_1_Grid
	{
		get
		{
			bool flag = this._Lower_Right_1_Grid == null;
			if (flag)
			{
				this._Lower_Right_1_Grid = this.Lower_Right_1_Layout_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Lower_Right_1_Grid;
		}
		set
		{
			this._Lower_Right_1_Grid = value;
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0011D19C File Offset: 0x0011B39C
	// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0011D1E6 File Offset: 0x0011B3E6
	public GameObject Lower_Right_1_Item
	{
		get
		{
			bool flag = this._Lower_Right_1_Item == null;
			if (flag)
			{
				this._Lower_Right_1_Item = this.Lower_Right_1_Layout_Object.transform.Find("Item").gameObject;
			}
			return this._Lower_Right_1_Item;
		}
		set
		{
			this._Lower_Right_1_Item = value;
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0011D1F0 File Offset: 0x0011B3F0
	// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0011D23A File Offset: 0x0011B43A
	public GameObject Lower_Right_1_Image_Item
	{
		get
		{
			bool flag = this._Lower_Right_1_Image_Item == null;
			if (flag)
			{
				this._Lower_Right_1_Image_Item = this.Lower_Right_1_Layout_Object.transform.Find("img_Item").gameObject;
			}
			return this._Lower_Right_1_Image_Item;
		}
		set
		{
			this._Lower_Right_1_Image_Item = value;
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06001EED RID: 7917 RVA: 0x0011D244 File Offset: 0x0011B444
	// (set) Token: 0x06001EEE RID: 7918 RVA: 0x0011D28E File Offset: 0x0011B48E
	public GameObject Lower_Right_2_Layout_Object
	{
		get
		{
			bool flag = this._Lower_Right_2_Layout_Object == null;
			if (flag)
			{
				this._Lower_Right_2_Layout_Object = base.gameObject.transform.Find("Lower_Right_2").gameObject;
			}
			return this._Lower_Right_2_Layout_Object;
		}
		set
		{
			this._Lower_Right_2_Layout_Object = value;
		}
	}

	// Token: 0x04001C54 RID: 7252
	private Button _Prerogative_Page_Text;

	// Token: 0x04001C55 RID: 7253
	private Text _Describe_Text;

	// Token: 0x04001C56 RID: 7254
	private GameObject _Top_Left_Layout_Object;

	// Token: 0x04001C57 RID: 7255
	private GameObject _Top_Left_Grid;

	// Token: 0x04001C58 RID: 7256
	private GameObject _Top_Left_Item;

	// Token: 0x04001C59 RID: 7257
	private Button _Top_Left_Get_Novice_Reward_Button;

	// Token: 0x04001C5A RID: 7258
	private GameObject _Lower_Left_1_Layout_Object;

	// Token: 0x04001C5B RID: 7259
	private GameObject _Lower_Left_1_Grid;

	// Token: 0x04001C5C RID: 7260
	private RectTransform _Lower_Left_1_Content_RectTransform;

	// Token: 0x04001C5D RID: 7261
	private GameObject _Lower_Left_1_Item;

	// Token: 0x04001C5E RID: 7262
	private GameObject _Lower_Left_1_Image_Item;

	// Token: 0x04001C5F RID: 7263
	private Button _Lower_Left_Get_Growth_Reward_Button;

	// Token: 0x04001C60 RID: 7264
	private GameObject _Lower_Left_2_Layout_Object;

	// Token: 0x04001C61 RID: 7265
	private GameObject _Lower_Left_2_Grid;

	// Token: 0x04001C62 RID: 7266
	private GameObject _Lower_Left_2_Image_Item;

	// Token: 0x04001C63 RID: 7267
	private GameObject _Lower_Left_2_Item_1;

	// Token: 0x04001C64 RID: 7268
	private Text _Lower_Left_2_Item_1_Get_Day_Reward_Describe_Text;

	// Token: 0x04001C65 RID: 7269
	private GameObject _Lower_Left_2_Item_2;

	// Token: 0x04001C66 RID: 7270
	private Text _Lower_Left_2_Item_2_Get_Day_Reward_Describe_Text;

	// Token: 0x04001C67 RID: 7271
	private GameObject _Lower_Left_2_Item_3;

	// Token: 0x04001C68 RID: 7272
	private Text _Lower_Left_2_Item_3_Get_Day_Reward_Describe_Text;

	// Token: 0x04001C69 RID: 7273
	private Button _Lower_Left_Get_Day_Reward_Button;

	// Token: 0x04001C6A RID: 7274
	private GameObject _Top_Right_Layout_Object;

	// Token: 0x04001C6B RID: 7275
	private Button _Top_Right_Prerogative_Handle_1_Button;

	// Token: 0x04001C6C RID: 7276
	private Button _Top_Right_Prerogative_Handle_2_Button;

	// Token: 0x04001C6D RID: 7277
	private GameObject _Lower_Right_1_Layout_Object;

	// Token: 0x04001C6E RID: 7278
	private GameObject _Lower_Right_1_Grid;

	// Token: 0x04001C6F RID: 7279
	private GameObject _Lower_Right_1_Item;

	// Token: 0x04001C70 RID: 7280
	private GameObject _Lower_Right_1_Image_Item;

	// Token: 0x04001C71 RID: 7281
	private GameObject _Lower_Right_2_Layout_Object;
}

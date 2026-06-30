using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BC RID: 700
public class QQ_Games_Hall_Prerogative_UI : MonoBehaviour
{
	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06002038 RID: 8248 RVA: 0x00128584 File Offset: 0x00126784
	// (set) Token: 0x06002039 RID: 8249 RVA: 0x001285CE File Offset: 0x001267CE
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

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x0600203A RID: 8250 RVA: 0x001285D8 File Offset: 0x001267D8
	// (set) Token: 0x0600203B RID: 8251 RVA: 0x00128622 File Offset: 0x00126822
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

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x0600203C RID: 8252 RVA: 0x0012862C File Offset: 0x0012682C
	// (set) Token: 0x0600203D RID: 8253 RVA: 0x00128676 File Offset: 0x00126876
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

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x0600203E RID: 8254 RVA: 0x00128680 File Offset: 0x00126880
	// (set) Token: 0x0600203F RID: 8255 RVA: 0x001286CF File Offset: 0x001268CF
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

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x06002040 RID: 8256 RVA: 0x001286DC File Offset: 0x001268DC
	// (set) Token: 0x06002041 RID: 8257 RVA: 0x00128726 File Offset: 0x00126926
	public GameObject Lower_Left_Layout_Object
	{
		get
		{
			bool flag = this._Lower_Left_Layout_Object == null;
			if (flag)
			{
				this._Lower_Left_Layout_Object = base.gameObject.transform.Find("Lower_Left").gameObject;
			}
			return this._Lower_Left_Layout_Object;
		}
		set
		{
			this._Lower_Left_Layout_Object = value;
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06002042 RID: 8258 RVA: 0x00128730 File Offset: 0x00126930
	// (set) Token: 0x06002043 RID: 8259 RVA: 0x0012877A File Offset: 0x0012697A
	public GameObject Lower_Left_Grid
	{
		get
		{
			bool flag = this._Lower_Left_Grid == null;
			if (flag)
			{
				this._Lower_Left_Grid = this.Lower_Left_Layout_Object.transform.Find("Grid").gameObject;
			}
			return this._Lower_Left_Grid;
		}
		set
		{
			this._Lower_Left_Grid = value;
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06002044 RID: 8260 RVA: 0x00128784 File Offset: 0x00126984
	// (set) Token: 0x06002045 RID: 8261 RVA: 0x001287CE File Offset: 0x001269CE
	public GameObject Lower_Left_Item
	{
		get
		{
			bool flag = this._Lower_Left_Item == null;
			if (flag)
			{
				this._Lower_Left_Item = this.Lower_Left_Layout_Object.transform.Find("Item").gameObject;
			}
			return this._Lower_Left_Item;
		}
		set
		{
			this._Lower_Left_Item = value;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06002046 RID: 8262 RVA: 0x001287D8 File Offset: 0x001269D8
	// (set) Token: 0x06002047 RID: 8263 RVA: 0x00128827 File Offset: 0x00126A27
	public Button Lower_Left_Get_Day_Reward_Button
	{
		get
		{
			bool flag = this._Lower_Left_Get_Day_Reward_Button == null;
			if (flag)
			{
				this._Lower_Left_Get_Day_Reward_Button = this.Lower_Left_Layout_Object.transform.Find("btn_Get_Day_Reward").gameObject.GetComponent<Button>();
			}
			return this._Lower_Left_Get_Day_Reward_Button;
		}
		set
		{
			this._Lower_Left_Get_Day_Reward_Button = value;
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06002048 RID: 8264 RVA: 0x00128834 File Offset: 0x00126A34
	// (set) Token: 0x06002049 RID: 8265 RVA: 0x0012887E File Offset: 0x00126A7E
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

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x0600204A RID: 8266 RVA: 0x00128888 File Offset: 0x00126A88
	// (set) Token: 0x0600204B RID: 8267 RVA: 0x001288D2 File Offset: 0x00126AD2
	public GameObject Top_Right_Grid
	{
		get
		{
			bool flag = this._Top_Right_Grid == null;
			if (flag)
			{
				this._Top_Right_Grid = this.Top_Right_Layout_Object.transform.Find("Scroll View/Viewport/Content/Grid").gameObject;
			}
			return this._Top_Right_Grid;
		}
		set
		{
			this._Top_Right_Grid = value;
		}
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x0600204C RID: 8268 RVA: 0x001288DC File Offset: 0x00126ADC
	// (set) Token: 0x0600204D RID: 8269 RVA: 0x00128926 File Offset: 0x00126B26
	public RectTransform Top_Right_Content_RectTransform
	{
		get
		{
			bool flag = this._Top_Right_Content_RectTransform == null;
			if (flag)
			{
				this._Top_Right_Content_RectTransform = this.Top_Right_Grid.transform.parent.gameObject.GetComponent<RectTransform>();
			}
			return this._Top_Right_Content_RectTransform;
		}
		set
		{
			this._Top_Right_Content_RectTransform = value;
		}
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x0600204E RID: 8270 RVA: 0x00128930 File Offset: 0x00126B30
	// (set) Token: 0x0600204F RID: 8271 RVA: 0x0012897A File Offset: 0x00126B7A
	public GameObject Top_Right_Item
	{
		get
		{
			bool flag = this._Top_Right_Item == null;
			if (flag)
			{
				this._Top_Right_Item = this.Top_Right_Layout_Object.transform.Find("Item").gameObject;
			}
			return this._Top_Right_Item;
		}
		set
		{
			this._Top_Right_Item = value;
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06002050 RID: 8272 RVA: 0x00128984 File Offset: 0x00126B84
	// (set) Token: 0x06002051 RID: 8273 RVA: 0x001289CE File Offset: 0x00126BCE
	public GameObject Top_Right_Image_Item
	{
		get
		{
			bool flag = this._Top_Right_Image_Item == null;
			if (flag)
			{
				this._Top_Right_Image_Item = this.Top_Right_Layout_Object.transform.Find("img_Item").gameObject;
			}
			return this._Top_Right_Image_Item;
		}
		set
		{
			this._Top_Right_Image_Item = value;
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06002052 RID: 8274 RVA: 0x001289D8 File Offset: 0x00126BD8
	// (set) Token: 0x06002053 RID: 8275 RVA: 0x00128A27 File Offset: 0x00126C27
	public Button Top_Right_Get_Growth_Reward_Button
	{
		get
		{
			bool flag = this._Top_Right_Get_Growth_Reward_Button == null;
			if (flag)
			{
				this._Top_Right_Get_Growth_Reward_Button = this.Top_Right_Layout_Object.transform.Find("btn_Get_Growth_Reward").gameObject.GetComponent<Button>();
			}
			return this._Top_Right_Get_Growth_Reward_Button;
		}
		set
		{
			this._Top_Right_Get_Growth_Reward_Button = value;
		}
	}

	// Token: 0x04001D74 RID: 7540
	private GameObject _Top_Left_Layout_Object;

	// Token: 0x04001D75 RID: 7541
	private GameObject _Top_Left_Grid;

	// Token: 0x04001D76 RID: 7542
	private GameObject _Top_Left_Item;

	// Token: 0x04001D77 RID: 7543
	private Button _Top_Left_Get_Novice_Reward_Button;

	// Token: 0x04001D78 RID: 7544
	private GameObject _Lower_Left_Layout_Object;

	// Token: 0x04001D79 RID: 7545
	private GameObject _Lower_Left_Grid;

	// Token: 0x04001D7A RID: 7546
	private GameObject _Lower_Left_Item;

	// Token: 0x04001D7B RID: 7547
	private Button _Lower_Left_Get_Day_Reward_Button;

	// Token: 0x04001D7C RID: 7548
	private GameObject _Top_Right_Layout_Object;

	// Token: 0x04001D7D RID: 7549
	private GameObject _Top_Right_Grid;

	// Token: 0x04001D7E RID: 7550
	private RectTransform _Top_Right_Content_RectTransform;

	// Token: 0x04001D7F RID: 7551
	private GameObject _Top_Right_Item;

	// Token: 0x04001D80 RID: 7552
	private GameObject _Top_Right_Image_Item;

	// Token: 0x04001D81 RID: 7553
	private Button _Top_Right_Get_Growth_Reward_Button;
}

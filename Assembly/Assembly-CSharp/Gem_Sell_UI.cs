using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000146 RID: 326
public class Gem_Sell_UI : MonoBehaviour
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00083454 File Offset: 0x00081654
	// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0008349E File Offset: 0x0008169E
	public GameObject Grid_Object
	{
		get
		{
			bool flag = this._Grid_Object == null;
			if (flag)
			{
				this._Grid_Object = base.gameObject.transform.FindChild("Grid").gameObject;
			}
			return this._Grid_Object;
		}
		set
		{
			this._Grid_Object = value;
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000834A8 File Offset: 0x000816A8
	// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x000834F2 File Offset: 0x000816F2
	public Button Confirm_Button
	{
		get
		{
			bool flag = this._Confirm_Button == null;
			if (flag)
			{
				this._Confirm_Button = base.gameObject.transform.FindChild("btn_Confirm").GetComponent<Button>();
			}
			return this._Confirm_Button;
		}
		set
		{
			this._Confirm_Button = value;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000834FC File Offset: 0x000816FC
	// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00083546 File Offset: 0x00081746
	public Button Cancel_Button
	{
		get
		{
			bool flag = this._Cancel_Button == null;
			if (flag)
			{
				this._Cancel_Button = base.gameObject.transform.FindChild("btn_Cancel").GetComponent<Button>();
			}
			return this._Cancel_Button;
		}
		set
		{
			this._Cancel_Button = value;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00083550 File Offset: 0x00081750
	// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0008359A File Offset: 0x0008179A
	public Text Sell_Price_Text
	{
		get
		{
			bool flag = this._Sell_Price_Text == null;
			if (flag)
			{
				this._Sell_Price_Text = base.gameObject.transform.FindChild("lbl_Sell_Price").GetComponent<Text>();
			}
			return this._Sell_Price_Text;
		}
		set
		{
			this._Sell_Price_Text = value;
		}
	}

	// Token: 0x04000B4F RID: 2895
	private GameObject _Grid_Object;

	// Token: 0x04000B50 RID: 2896
	private Button _Confirm_Button;

	// Token: 0x04000B51 RID: 2897
	private Button _Cancel_Button;

	// Token: 0x04000B52 RID: 2898
	private Text _Sell_Price_Text;
}

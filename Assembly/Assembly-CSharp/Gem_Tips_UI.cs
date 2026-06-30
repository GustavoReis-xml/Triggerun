using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000149 RID: 329
public class Gem_Tips_UI : MonoBehaviour
{
	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00083FE4 File Offset: 0x000821E4
	// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0008402E File Offset: 0x0008222E
	public GameObject Container
	{
		get
		{
			bool flag = this._Container == null;
			if (flag)
			{
				this._Container = base.gameObject.transform.FindChild("Background").gameObject;
			}
			return this._Container;
		}
		set
		{
			this._Container = value;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00084038 File Offset: 0x00082238
	// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00084073 File Offset: 0x00082273
	public RectTransform Background_RectTransform
	{
		get
		{
			bool flag = this._Background_RectTransform == null;
			if (flag)
			{
				this._Background_RectTransform = this.Container.GetComponent<RectTransform>();
			}
			return this._Background_RectTransform;
		}
		set
		{
			this._Background_RectTransform = value;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00084080 File Offset: 0x00082280
	// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x000840CA File Offset: 0x000822CA
	public Text Name_Text
	{
		get
		{
			bool flag = this._Name_Text == null;
			if (flag)
			{
				this._Name_Text = this.Container.transform.FindChild("lbl_Gem_Name").GetComponent<Text>();
			}
			return this._Name_Text;
		}
		set
		{
			this._Name_Text = value;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x000840D4 File Offset: 0x000822D4
	// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x0008411E File Offset: 0x0008231E
	public Text Limit_Hero_Text
	{
		get
		{
			bool flag = this._Limit_Hero_Text == null;
			if (flag)
			{
				this._Limit_Hero_Text = this.Container.transform.FindChild("lbl_Gem_Limit_Hero").GetComponent<Text>();
			}
			return this._Limit_Hero_Text;
		}
		set
		{
			this._Limit_Hero_Text = value;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00084128 File Offset: 0x00082328
	// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00084172 File Offset: 0x00082372
	public Text Type_Text
	{
		get
		{
			bool flag = this._Type_Text == null;
			if (flag)
			{
				this._Type_Text = this.Container.transform.FindChild("lbl_Gem_Type").GetComponent<Text>();
			}
			return this._Type_Text;
		}
		set
		{
			this._Type_Text = value;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0008417C File Offset: 0x0008237C
	// (set) Token: 0x06000DFC RID: 3580 RVA: 0x000841C6 File Offset: 0x000823C6
	public Text Describe_Text
	{
		get
		{
			bool flag = this._Describe_Text == null;
			if (flag)
			{
				this._Describe_Text = this.Container.transform.FindChild("lbl_Gem_Describe").GetComponent<Text>();
			}
			return this._Describe_Text;
		}
		set
		{
			this._Describe_Text = value;
		}
	}

	// Token: 0x04000B59 RID: 2905
	private GameObject _Container;

	// Token: 0x04000B5A RID: 2906
	private RectTransform _Background_RectTransform;

	// Token: 0x04000B5B RID: 2907
	private Text _Name_Text;

	// Token: 0x04000B5C RID: 2908
	private Text _Limit_Hero_Text;

	// Token: 0x04000B5D RID: 2909
	private Text _Type_Text;

	// Token: 0x04000B5E RID: 2910
	private Text _Describe_Text;
}

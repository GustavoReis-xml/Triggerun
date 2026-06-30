using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001D RID: 29
public class Arena_UI : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000106 RID: 262 RVA: 0x00006918 File Offset: 0x00004B18
	// (set) Token: 0x06000107 RID: 263 RVA: 0x00006962 File Offset: 0x00004B62
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

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000108 RID: 264 RVA: 0x0000696C File Offset: 0x00004B6C
	public Image A_Score_Image
	{
		get
		{
			bool flag = this._A_Score_Image == null;
			if (flag)
			{
				this._A_Score_Image = this.Top_Container.transform.FindChild("Score/A_Score").GetComponent<Image>();
			}
			return this._A_Score_Image;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000109 RID: 265 RVA: 0x000069B8 File Offset: 0x00004BB8
	public Image B_Score_Image
	{
		get
		{
			bool flag = this._B_Score_Image == null;
			if (flag)
			{
				this._B_Score_Image = this.Top_Container.transform.FindChild("Score/B_Score").GetComponent<Image>();
			}
			return this._B_Score_Image;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600010A RID: 266 RVA: 0x00006A04 File Offset: 0x00004C04
	public GameObject A_User_List
	{
		get
		{
			bool flag = this._A_User_List == null;
			if (flag)
			{
				this._A_User_List = this.Top_Container.transform.FindChild("A_User_List").gameObject;
			}
			return this._A_User_List;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600010B RID: 267 RVA: 0x00006A50 File Offset: 0x00004C50
	public GameObject B_User_List
	{
		get
		{
			bool flag = this._B_User_List == null;
			if (flag)
			{
				this._B_User_List = this.Top_Container.transform.FindChild("B_User_List").gameObject;
			}
			return this._B_User_List;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600010C RID: 268 RVA: 0x00006A9C File Offset: 0x00004C9C
	public GameObject A_Victory_List
	{
		get
		{
			bool flag = this._A_Victory_List == null;
			if (flag)
			{
				this._A_Victory_List = this.Top_Container.transform.FindChild("A_Victory_List").gameObject;
			}
			return this._A_Victory_List;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600010D RID: 269 RVA: 0x00006AE8 File Offset: 0x00004CE8
	public GameObject B_Victory_List
	{
		get
		{
			bool flag = this._B_Victory_List == null;
			if (flag)
			{
				this._B_Victory_List = this.Top_Container.transform.FindChild("B_Victory_List").gameObject;
			}
			return this._B_Victory_List;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600010E RID: 270 RVA: 0x00006B34 File Offset: 0x00004D34
	public Text Remaining_Time_Text
	{
		get
		{
			bool flag = this._Remaining_Time_Text == null;
			if (flag)
			{
				this._Remaining_Time_Text = this.Top_Container.transform.FindChild("Remaining_Time").GetComponent<Text>();
			}
			return this._Remaining_Time_Text;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x0600010F RID: 271 RVA: 0x00006B80 File Offset: 0x00004D80
	// (set) Token: 0x06000110 RID: 272 RVA: 0x00006BCA File Offset: 0x00004DCA
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

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000111 RID: 273 RVA: 0x00006BD4 File Offset: 0x00004DD4
	// (set) Token: 0x06000112 RID: 274 RVA: 0x00006C1E File Offset: 0x00004E1E
	public Text Fight_Describe_Text
	{
		get
		{
			bool flag = this._Fight_Describe_Text == null;
			if (flag)
			{
				this._Fight_Describe_Text = this.Top_Container.transform.Find("Fight_Describe").GetComponent<Text>();
			}
			return this._Fight_Describe_Text;
		}
		set
		{
			this._Fight_Describe_Text = value;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00006C28 File Offset: 0x00004E28
	// (set) Token: 0x06000114 RID: 276 RVA: 0x00006C72 File Offset: 0x00004E72
	public GameObject Effect_Object
	{
		get
		{
			bool flag = this._Effect_Object == null;
			if (flag)
			{
				this._Effect_Object = this.Top_Container.transform.Find("Describe_Effect").gameObject;
			}
			return this._Effect_Object;
		}
		set
		{
			this._Effect_Object = value;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000115 RID: 277 RVA: 0x00006C7C File Offset: 0x00004E7C
	// (set) Token: 0x06000116 RID: 278 RVA: 0x00006D38 File Offset: 0x00004F38
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
					this._Victory_Effect = this.Top_Container.transform.Find("Victory_Effect").gameObject;
					this._Victory_Effect.transform.localPosition = new Vector3(0f, -300f, 0f);
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Victory_Effect = this.Top_Container.transform.Find("Victory_Effect_bx").gameObject;
				}
			}
			return this._Victory_Effect;
		}
		set
		{
			this._Victory_Effect = value;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000117 RID: 279 RVA: 0x00006D44 File Offset: 0x00004F44
	// (set) Token: 0x06000118 RID: 280 RVA: 0x00006E00 File Offset: 0x00005000
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
					this._Failure_Effect = this.Top_Container.transform.Find("Failure_Effect").gameObject;
					this._Failure_Effect.transform.localPosition = new Vector3(0f, -300f, 0f);
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Failure_Effect = this.Top_Container.transform.Find("Failure_Effect_bx").gameObject;
				}
			}
			return this._Failure_Effect;
		}
		set
		{
			this._Failure_Effect = value;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000119 RID: 281 RVA: 0x00006E0C File Offset: 0x0000500C
	// (set) Token: 0x0600011A RID: 282 RVA: 0x00006E56 File Offset: 0x00005056
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

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600011B RID: 283 RVA: 0x00006E60 File Offset: 0x00005060
	// (set) Token: 0x0600011C RID: 284 RVA: 0x00006EAA File Offset: 0x000050AA
	public GameObject Center_Background
	{
		get
		{
			bool flag = this._Center_Background == null;
			if (flag)
			{
				this._Center_Background = this.Center_Container.transform.Find("Background").gameObject;
			}
			return this._Center_Background;
		}
		set
		{
			this._Center_Background = value;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600011D RID: 285 RVA: 0x00006EB4 File Offset: 0x000050B4
	// (set) Token: 0x0600011E RID: 286 RVA: 0x00006EFE File Offset: 0x000050FE
	public Text Center_Background_Text
	{
		get
		{
			bool flag = this._Center_Background_Text == null;
			if (flag)
			{
				this._Center_Background_Text = this.Center_Background.transform.Find("Describe").GetComponent<Text>();
			}
			return this._Center_Background_Text;
		}
		set
		{
			this._Center_Background_Text = value;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600011F RID: 287 RVA: 0x00006F08 File Offset: 0x00005108
	// (set) Token: 0x06000120 RID: 288 RVA: 0x00006F52 File Offset: 0x00005152
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

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000121 RID: 289 RVA: 0x00006F5C File Offset: 0x0000515C
	// (set) Token: 0x06000122 RID: 290 RVA: 0x00006FDF File Offset: 0x000051DF
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

	// Token: 0x040000C6 RID: 198
	private GameObject _Top_Container;

	// Token: 0x040000C7 RID: 199
	private Image _A_Score_Image;

	// Token: 0x040000C8 RID: 200
	private Image _B_Score_Image;

	// Token: 0x040000C9 RID: 201
	private GameObject _A_User_List;

	// Token: 0x040000CA RID: 202
	private GameObject _B_User_List;

	// Token: 0x040000CB RID: 203
	private GameObject _A_Victory_List;

	// Token: 0x040000CC RID: 204
	private GameObject _B_Victory_List;

	// Token: 0x040000CD RID: 205
	private Text _Remaining_Time_Text;

	// Token: 0x040000CE RID: 206
	private Text _Prepare_Describe_Text;

	// Token: 0x040000CF RID: 207
	private Text _Fight_Describe_Text;

	// Token: 0x040000D0 RID: 208
	private GameObject _Effect_Object;

	// Token: 0x040000D1 RID: 209
	private GameObject _Victory_Effect;

	// Token: 0x040000D2 RID: 210
	private GameObject _Failure_Effect;

	// Token: 0x040000D3 RID: 211
	private GameObject _Center_Container;

	// Token: 0x040000D4 RID: 212
	private GameObject _Center_Background;

	// Token: 0x040000D5 RID: 213
	private Text _Center_Background_Text;

	// Token: 0x040000D6 RID: 214
	private Text _Describe_Text;

	// Token: 0x040000D7 RID: 215
	private GameObject _Countdown_Effect;
}

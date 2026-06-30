using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class Arena_Score_UI : MonoBehaviour
{
	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005C84 File Offset: 0x00003E84
	// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005CCE File Offset: 0x00003ECE
	public GameObject Top_Container
	{
		get
		{
			bool flag = this._Top_Container == null;
			if (flag)
			{
				this._Top_Container = base.gameObject.transform.FindChild("Top").gameObject;
			}
			return this._Top_Container;
		}
		set
		{
			this._Top_Container = value;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005CD8 File Offset: 0x00003ED8
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

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000CA RID: 202 RVA: 0x00005D24 File Offset: 0x00003F24
	// (set) Token: 0x060000CB RID: 203 RVA: 0x00005D6E File Offset: 0x00003F6E
	public Text Max_Score_Text
	{
		get
		{
			bool flag = this._Max_Score_Text == null;
			if (flag)
			{
				this._Max_Score_Text = this.Top_Container.transform.FindChild("lbl_Max_Score").GetComponent<Text>();
			}
			return this._Max_Score_Text;
		}
		set
		{
			this._Max_Score_Text = value;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000CC RID: 204 RVA: 0x00005D78 File Offset: 0x00003F78
	// (set) Token: 0x060000CD RID: 205 RVA: 0x00005DC2 File Offset: 0x00003FC2
	public Text A_Score_Text
	{
		get
		{
			bool flag = this._A_Score_Text == null;
			if (flag)
			{
				this._A_Score_Text = this.Top_Container.transform.FindChild("lbl_A_Score").GetComponent<Text>();
			}
			return this._A_Score_Text;
		}
		set
		{
			this._A_Score_Text = value;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000CE RID: 206 RVA: 0x00005DCC File Offset: 0x00003FCC
	// (set) Token: 0x060000CF RID: 207 RVA: 0x00005E16 File Offset: 0x00004016
	public Text B_Score_Text
	{
		get
		{
			bool flag = this._B_Score_Text == null;
			if (flag)
			{
				this._B_Score_Text = this.Top_Container.transform.FindChild("lbl_B_Score").GetComponent<Text>();
			}
			return this._B_Score_Text;
		}
		set
		{
			this._B_Score_Text = value;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005E20 File Offset: 0x00004020
	// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005E6A File Offset: 0x0000406A
	public Text Prepare_Describe_Text
	{
		get
		{
			bool flag = this._Prepare_Describe_Text == null;
			if (flag)
			{
				this._Prepare_Describe_Text = this.Top_Container.transform.FindChild("Prepare_Describe").GetComponent<Text>();
			}
			return this._Prepare_Describe_Text;
		}
		set
		{
			this._Prepare_Describe_Text = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005E74 File Offset: 0x00004074
	// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005EBE File Offset: 0x000040BE
	public Text Fight_Describe_Text
	{
		get
		{
			bool flag = this._Fight_Describe_Text == null;
			if (flag)
			{
				this._Fight_Describe_Text = this.Top_Container.transform.FindChild("Fight_Describe").GetComponent<Text>();
			}
			return this._Fight_Describe_Text;
		}
		set
		{
			this._Fight_Describe_Text = value;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005EC8 File Offset: 0x000040C8
	// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005F12 File Offset: 0x00004112
	public GameObject Effect_Object
	{
		get
		{
			bool flag = this._Effect_Object == null;
			if (flag)
			{
				this._Effect_Object = this.Top_Container.transform.FindChild("Describe_Effect").gameObject;
			}
			return this._Effect_Object;
		}
		set
		{
			this._Effect_Object = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005F1C File Offset: 0x0000411C
	// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005F66 File Offset: 0x00004166
	public GameObject Center_Container
	{
		get
		{
			bool flag = this._Center_Container == null;
			if (flag)
			{
				this._Center_Container = base.gameObject.transform.FindChild("Center").gameObject;
			}
			return this._Center_Container;
		}
		set
		{
			this._Center_Container = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005F70 File Offset: 0x00004170
	// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005FBA File Offset: 0x000041BA
	public Text Describe_Text
	{
		get
		{
			bool flag = this._Describe_Text == null;
			if (flag)
			{
				this._Describe_Text = this.Center_Container.transform.FindChild("Describe").GetComponent<Text>();
			}
			return this._Describe_Text;
		}
		set
		{
			this._Describe_Text = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060000DA RID: 218 RVA: 0x00005FC4 File Offset: 0x000041C4
	// (set) Token: 0x060000DB RID: 219 RVA: 0x00006047 File Offset: 0x00004247
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
					this._Countdown_Effect = this.Center_Container.transform.FindChild("Countdown_bx/Particle System").gameObject;
				}
				else
				{
					this._Countdown_Effect = this.Center_Container.transform.FindChild("Countdown/Particle System").gameObject;
				}
			}
			return this._Countdown_Effect;
		}
		set
		{
			this._Countdown_Effect = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060000DC RID: 220 RVA: 0x00006054 File Offset: 0x00004254
	// (set) Token: 0x060000DD RID: 221 RVA: 0x000060E8 File Offset: 0x000042E8
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
					this._Victory_Effect = this.Center_Container.transform.FindChild("Victory_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Victory_Effect = this.Center_Container.transform.FindChild("Victory_Effect_bx").gameObject;
				}
			}
			return this._Victory_Effect;
		}
		set
		{
			this._Victory_Effect = value;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060000DE RID: 222 RVA: 0x000060F4 File Offset: 0x000042F4
	// (set) Token: 0x060000DF RID: 223 RVA: 0x00006188 File Offset: 0x00004388
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
					this._Draw_Effect = this.Center_Container.transform.FindChild("Draw_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Draw_Effect = this.Center_Container.transform.FindChild("Draw_Effect_bx").gameObject;
				}
			}
			return this._Draw_Effect;
		}
		set
		{
			this._Draw_Effect = value;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006194 File Offset: 0x00004394
	// (set) Token: 0x060000E1 RID: 225 RVA: 0x00006228 File Offset: 0x00004428
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
					this._Failure_Effect = this.Center_Container.transform.FindChild("Failure_Effect").gameObject;
				}
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					this._Failure_Effect = this.Center_Container.transform.FindChild("Failure_Effect_bx").gameObject;
				}
			}
			return this._Failure_Effect;
		}
		set
		{
			this._Failure_Effect = value;
		}
	}

	// Token: 0x040000AE RID: 174
	private GameObject _Top_Container;

	// Token: 0x040000AF RID: 175
	private Text _Remaining_Time_Text;

	// Token: 0x040000B0 RID: 176
	private Text _Max_Score_Text;

	// Token: 0x040000B1 RID: 177
	private Text _A_Score_Text;

	// Token: 0x040000B2 RID: 178
	private Text _B_Score_Text;

	// Token: 0x040000B3 RID: 179
	private Text _Prepare_Describe_Text;

	// Token: 0x040000B4 RID: 180
	private Text _Fight_Describe_Text;

	// Token: 0x040000B5 RID: 181
	private GameObject _Effect_Object;

	// Token: 0x040000B6 RID: 182
	private GameObject _Center_Container;

	// Token: 0x040000B7 RID: 183
	private Text _Describe_Text;

	// Token: 0x040000B8 RID: 184
	private GameObject _Countdown_Effect;

	// Token: 0x040000B9 RID: 185
	private GameObject _Victory_Effect;

	// Token: 0x040000BA RID: 186
	private GameObject _Draw_Effect;

	// Token: 0x040000BB RID: 187
	private GameObject _Failure_Effect;
}

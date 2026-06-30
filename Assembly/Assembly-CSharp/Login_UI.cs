using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000224 RID: 548
public class Login_UI : MonoBehaviour
{
	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x060018AE RID: 6318 RVA: 0x000E6DDC File Offset: 0x000E4FDC
	// (set) Token: 0x060018AF RID: 6319 RVA: 0x000E6E26 File Offset: 0x000E5026
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

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x060018B0 RID: 6320 RVA: 0x000E6E30 File Offset: 0x000E5030
	// (set) Token: 0x060018B1 RID: 6321 RVA: 0x000E6E7A File Offset: 0x000E507A
	public GameObject GM_Input_Container
	{
		get
		{
			bool flag = this._GM_Input_Container == null;
			if (flag)
			{
				this._GM_Input_Container = this.Center_Container.transform.Find("GM_Input").gameObject;
			}
			return this._GM_Input_Container;
		}
		set
		{
			this._GM_Input_Container = value;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000E6E84 File Offset: 0x000E5084
	// (set) Token: 0x060018B3 RID: 6323 RVA: 0x000E6ECE File Offset: 0x000E50CE
	public GameObject User_Input_Container
	{
		get
		{
			bool flag = this._User_Input_Container == null;
			if (flag)
			{
				this._User_Input_Container = this.Center_Container.transform.Find("User_Input").gameObject;
			}
			return this._User_Input_Container;
		}
		set
		{
			this._User_Input_Container = value;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000E6ED8 File Offset: 0x000E50D8
	// (set) Token: 0x060018B5 RID: 6325 RVA: 0x000E6F22 File Offset: 0x000E5122
	public InputField Login_IP_Text
	{
		get
		{
			bool flag = this._Login_IP_Text == null;
			if (flag)
			{
				this._Login_IP_Text = this.GM_Input_Container.transform.Find("lbl_Login_IP/txt_Login_IP").GetComponent<InputField>();
			}
			return this._Login_IP_Text;
		}
		set
		{
			this._Login_IP_Text = value;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000E6F2C File Offset: 0x000E512C
	// (set) Token: 0x060018B7 RID: 6327 RVA: 0x000E6F76 File Offset: 0x000E5176
	public Transform Login_IP_Text_Caret
	{
		get
		{
			bool flag = this._Login_IP_Text_Caret == null;
			if (flag)
			{
				this._Login_IP_Text_Caret = this.Login_IP_Text.gameObject.transform.FindChild("txt_Login_IP Input Caret");
			}
			return this._Login_IP_Text_Caret;
		}
		set
		{
			this._Login_IP_Text_Caret = value;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000E6F80 File Offset: 0x000E5180
	// (set) Token: 0x060018B9 RID: 6329 RVA: 0x000E6FCA File Offset: 0x000E51CA
	public InputField Login_Name_Text
	{
		get
		{
			bool flag = this._Login_Name_Text == null;
			if (flag)
			{
				this._Login_Name_Text = this.User_Input_Container.transform.Find("lbl_Login_Name/txt_Login_Name").GetComponent<InputField>();
			}
			return this._Login_Name_Text;
		}
		set
		{
			this._Login_Name_Text = value;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x060018BA RID: 6330 RVA: 0x000E6FD4 File Offset: 0x000E51D4
	// (set) Token: 0x060018BB RID: 6331 RVA: 0x000E701E File Offset: 0x000E521E
	public Transform Login_Name_Text_Caret
	{
		get
		{
			bool flag = this._Login_Name_Text_Caret == null;
			if (flag)
			{
				this._Login_Name_Text_Caret = this.Login_Name_Text.gameObject.transform.FindChild("txt_Login_Name Input Caret");
			}
			return this._Login_Name_Text_Caret;
		}
		set
		{
			this._Login_Name_Text_Caret = value;
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x060018BC RID: 6332 RVA: 0x000E7028 File Offset: 0x000E5228
	// (set) Token: 0x060018BD RID: 6333 RVA: 0x000E7072 File Offset: 0x000E5272
	public InputField Login_Password_Text
	{
		get
		{
			bool flag = this._Login_Password_Text == null;
			if (flag)
			{
				this._Login_Password_Text = this.User_Input_Container.transform.Find("lbl_Login_Password/txt_Login_Password").GetComponent<InputField>();
			}
			return this._Login_Password_Text;
		}
		set
		{
			this._Login_Password_Text = value;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x060018BE RID: 6334 RVA: 0x000E707C File Offset: 0x000E527C
	// (set) Token: 0x060018BF RID: 6335 RVA: 0x000E70C6 File Offset: 0x000E52C6
	public Transform Login_Password_Text_Caret
	{
		get
		{
			bool flag = this._Login_Password_Text_Caret == null;
			if (flag)
			{
				this._Login_Password_Text_Caret = this.Login_Password_Text.gameObject.transform.FindChild("txt_Login_Password Input Caret");
			}
			return this._Login_Password_Text_Caret;
		}
		set
		{
			this._Login_Password_Text_Caret = value;
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000E70D0 File Offset: 0x000E52D0
	// (set) Token: 0x060018C1 RID: 6337 RVA: 0x000E711A File Offset: 0x000E531A
	public Text Login_Message_Text
	{
		get
		{
			bool flag = this._Login_Message_Text == null;
			if (flag)
			{
				this._Login_Message_Text = this.User_Input_Container.transform.Find("lbl_Message").GetComponent<Text>();
			}
			return this._Login_Message_Text;
		}
		set
		{
			this._Login_Message_Text = value;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000E7124 File Offset: 0x000E5324
	// (set) Token: 0x060018C3 RID: 6339 RVA: 0x000E716E File Offset: 0x000E536E
	public Button Register_Button
	{
		get
		{
			bool flag = this._Register_Button == null;
			if (flag)
			{
				this._Register_Button = this.User_Input_Container.transform.Find("btn_Register").GetComponent<Button>();
			}
			return this._Register_Button;
		}
		set
		{
			this._Register_Button = value;
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x060018C4 RID: 6340 RVA: 0x000E7178 File Offset: 0x000E5378
	// (set) Token: 0x060018C5 RID: 6341 RVA: 0x000E71C2 File Offset: 0x000E53C2
	public Button Login_Button
	{
		get
		{
			bool flag = this._Login_Button == null;
			if (flag)
			{
				this._Login_Button = this.User_Input_Container.transform.Find("btn_Login").GetComponent<Button>();
			}
			return this._Login_Button;
		}
		set
		{
			this._Login_Button = value;
		}
	}

	// Token: 0x0400135F RID: 4959
	private GameObject _Center_Container;

	// Token: 0x04001360 RID: 4960
	private GameObject _GM_Input_Container;

	// Token: 0x04001361 RID: 4961
	private GameObject _User_Input_Container;

	// Token: 0x04001362 RID: 4962
	private InputField _Login_IP_Text;

	// Token: 0x04001363 RID: 4963
	private Transform _Login_IP_Text_Caret;

	// Token: 0x04001364 RID: 4964
	private InputField _Login_Name_Text;

	// Token: 0x04001365 RID: 4965
	private Transform _Login_Name_Text_Caret;

	// Token: 0x04001366 RID: 4966
	private InputField _Login_Password_Text;

	// Token: 0x04001367 RID: 4967
	private Transform _Login_Password_Text_Caret;

	// Token: 0x04001368 RID: 4968
	private Text _Login_Message_Text;

	// Token: 0x04001369 RID: 4969
	private Button _Register_Button;

	// Token: 0x0400136A RID: 4970
	private Button _Login_Button;
}

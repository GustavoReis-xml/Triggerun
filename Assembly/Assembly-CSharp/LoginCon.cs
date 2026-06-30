using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000221 RID: 545
public class LoginCon : Window
{
	// Token: 0x170002CE RID: 718
	// (get) Token: 0x0600188B RID: 6283 RVA: 0x000E5D1C File Offset: 0x000E3F1C
	// (set) Token: 0x0600188C RID: 6284 RVA: 0x000E5D7A File Offset: 0x000E3F7A
	public Login_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Login_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Login_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x0600188D RID: 6285 RVA: 0x000E5D84 File Offset: 0x000E3F84
	// (set) Token: 0x0600188E RID: 6286 RVA: 0x000E5DA6 File Offset: 0x000E3FA6
	public string Login_IP
	{
		get
		{
			return this.UI_Component.Login_IP_Text.text;
		}
		set
		{
			this.UI_Component.Login_IP_Text.text = value;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x0600188F RID: 6287 RVA: 0x000E5DBC File Offset: 0x000E3FBC
	// (set) Token: 0x06001890 RID: 6288 RVA: 0x000E5DDE File Offset: 0x000E3FDE
	public string Login_Name
	{
		get
		{
			return this.UI_Component.Login_Name_Text.text;
		}
		set
		{
			this.UI_Component.Login_Name_Text.text = value;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06001891 RID: 6289 RVA: 0x000E5DF4 File Offset: 0x000E3FF4
	// (set) Token: 0x06001892 RID: 6290 RVA: 0x000E5E16 File Offset: 0x000E4016
	public string Login_Password
	{
		get
		{
			return this.UI_Component.Login_Password_Text.text;
		}
		set
		{
			this.UI_Component.Login_Password_Text.text = value;
		}
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x000E5E2C File Offset: 0x000E402C
	private void Start()
	{
		this.Save_Data = GameRoot.inst.serverSaveStr();
		this.Login_IP = PlayerPrefs.GetString(this.Save_Data + "ip");
		this.Login_Name = PlayerPrefs.GetString(this.Save_Data + "uName");
		this.Login_Password = PlayerPrefs.GetString(this.Save_Data + "uPwd");
		this.UI_Component.Login_Button.onClick.AddListener(delegate()
		{
			this.Login();
		});
		this.UI_Component.Register_Button.onClick.AddListener(delegate()
		{
			this.Register();
		});
		base.gameObject.AddComponent<Login_Input_Navigator>();
		bool flag = GlobalDefine.Login_Type > 0;
		if (flag)
		{
			this.UI_Component.GM_Input_Container.SetActive(false);
			this.Login_IP = GlobalDefine.Login_IP;
		}
		fUIManager.SetProgreessShow(false);
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000E5F24 File Offset: 0x000E4124
	private void Handle_Field_Caret()
	{
		bool flag = !this._Login_IP_Text_Caret_State && this.UI_Component.Login_IP_Text_Caret != null;
		if (flag)
		{
			this._RectTransform = this.UI_Component.Login_IP_Text_Caret.GetComponent<RectTransform>();
			this._RectTransform.localPosition = new Vector3(0f, 5f, 0f);
			this._RectTransform.localScale = new Vector3(1f, 0.7f, 1f);
		}
		bool flag2 = !this._Login_Name_Text_Caret_State && this.UI_Component.Login_Name_Text_Caret != null;
		if (flag2)
		{
			this._RectTransform = this.UI_Component.Login_Name_Text_Caret.GetComponent<RectTransform>();
			this._RectTransform.localPosition = new Vector3(0f, 5f, 0f);
			this._RectTransform.localScale = new Vector3(1f, 0.7f, 1f);
		}
		bool flag3 = !this._Login_Password_Text_Caret_State && this.UI_Component.Login_Password_Text_Caret != null;
		if (flag3)
		{
			this._RectTransform = this.UI_Component.Login_Password_Text_Caret.GetComponent<RectTransform>();
			this._RectTransform.localPosition = new Vector3(0f, 5f, 0f);
			this._RectTransform.localScale = new Vector3(1f, 0.7f, 1f);
		}
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x000E609C File Offset: 0x000E429C
	private void Login()
	{
		bool flag = this.m_nLogin != 1;
		if (flag)
		{
			this.m_nLogin = 1;
			this._UserSocket = GameRoot.LoginSocket;
			bool flag2 = this.Proxy.Length > 0;
			if (flag2)
			{
				this.SetProxyInfo(this.Login_Name.GetHashCode(), this.Proxy);
			}
			bool flag3 = !string.IsNullOrEmpty(this.Client_Proxy_IP) && !string.IsNullOrEmpty(this.Client_Proxy_Port);
			if (flag3)
			{
				this._UserSocket.SetProxy(this.Client_Proxy_IP, this.Client_Proxy_Port);
			}
			this._UserSocket.m_sIP = this.Login_IP;
			this._UserSocket.m_nPort = 11041;
			PlayerPrefs.SetString(this.Save_Data + "ip", this.UI_Component.Login_IP_Text.text);
			PlayerPrefs.SetString(this.Save_Data + "uName", this.UI_Component.Login_Name_Text.text);
			PlayerPrefs.SetString(this.Save_Data + "uPwd", this.UI_Component.Login_Password_Text.text);
			SysConfig.gUserName = this.Login_Name;
			WebParam.client_account = this.Login_Name;
			WebParam.client_password = this.Login_Password;
			fGameManager.m_nLostReason = 20001;
			this._UserSocket.SetIpAndPort();
			this._UserSocket.ConnectServer(this._UserSocket.m_sIP, this._UserSocket.m_nPort, "LoginServer", "LoginUser", this.Login_Name, this.Login_Password, string.Concat(new string[]
			{
				this.Server,
				"#HS#",
				SysConfig.g_client,
				"#",
				Fun.GetUdId(),
				"#",
				Fun.GetMAC0()
			}));
		}
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x000E627F File Offset: 0x000E447F
	private void Register()
	{
		fUIManager.GetInstance().ShowWin("Reg", NetMsg.UITRANSFROM.LoginUI, new fUIManager.UILoadFinishDelegate(this.OnRegUI), null, true);
		Fun.Log("Url", null);
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000E62AD File Offset: 0x000E44AD
	public void OnRegUI(string UI_Name, GameObject UIObject, object szParam)
	{
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x000E62B0 File Offset: 0x000E44B0
	private void SetProxyInfo(int _ID, string _Proxy_Info)
	{
		bool flag = !string.IsNullOrEmpty(_Proxy_Info);
		if (flag)
		{
			string[] array = _Proxy_Info.Split(new char[]
			{
				','
			});
			string[] array2 = array[_ID % array.Length].Split(new char[]
			{
				':'
			});
			this.Client_Proxy_IP = array2[0];
			this.Client_Proxy_Port = array2[1];
		}
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x000E630C File Offset: 0x000E450C
	public void Message_Handle(int _Net_ID, ArrayList szMsg)
	{
		bool flag = _Net_ID != 1;
		if (flag)
		{
			fGameManager.isClose = true;
		}
		if (_Net_ID <= 1005)
		{
			if (_Net_ID != 1)
			{
				switch (_Net_ID)
				{
				case 1000:
					this.UI_Component.Login_Message_Text.text = Language.GetLang(1510, new object[0]);
					this.UI_Component.Login_Password_Text.text = string.Empty;
					PlayerPrefs.SetString(this.Save_Data + "Password", string.Empty);
					break;
				case 1001:
					this.UI_Component.Login_Message_Text.text = Language.GetLang(1511, new object[0]);
					this.UI_Component.Login_Password_Text.text = string.Empty;
					PlayerPrefs.SetString(this.Save_Data + "Password", string.Empty);
					break;
				case 1002:
				{
					this.UI_Component.Login_Message_Text.text = Language.GetLang(1514, new object[0]);
					ArrayList arrayList = (ArrayList)szMsg[7];
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22000, new object[]
					{
						arrayList[1].ToString(),
						Fun.DecodingUTF8(arrayList[2].ToString())
					}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), null, null, null, null);
					break;
				}
				case 1003:
				{
					fGameManager.isFtUser = true;
					this.UI_Component.Login_Message_Text.text = Language.GetLang(1515, new object[0]);
					ArrayList arrayList2 = (ArrayList)szMsg[7];
					string text = Language.GetLang(46075, new object[0]);
					bool flag2 = (int)arrayList2[3] < 59999940;
					if (flag2)
					{
						text = DateTime.Parse(arrayList2[2].ToString()).AddSeconds((double)((int)arrayList2[3])).ToString("yyyy-MM-dd hh:mm:ss");
					}
					fGameManager.sFtUserTip = Language.GetLang(22001, new object[]
					{
						text,
						Fun.DecodingUTF8(arrayList2[1].ToString())
					});
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22001, new object[]
					{
						text,
						Fun.DecodingUTF8(arrayList2[1].ToString())
					}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), null, null, null, null);
					break;
				}
				case 1004:
				case 1005:
				{
					ArrayList arrayList3 = (ArrayList)szMsg[7];
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(22003, new object[]
					{
						Fun.DecodingUTF8(arrayList3[2].ToString())
					}), Language.GetLang(8012, new object[0]), string.Empty, Language.GetLang(22002, new object[0]), null, null, null, null);
					break;
				}
				}
			}
			else
			{
				base.gameObject.SetActive(false);
				fUIManager.SetProgreessShow(true);
			}
		}
		else if (_Net_ID != 1014)
		{
			if (_Net_ID == 1015)
			{
				this.UI_Component.Login_Message_Text.text = Language.GetLang(1513, new object[0]);
			}
		}
		else
		{
			this.UI_Component.Login_Message_Text.text = Language.GetLang(1512, new object[0]);
		}
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000E66F8 File Offset: 0x000E48F8
	private void aaa()
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = this.UI_Component.Center_Container.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		Text text = gameObject.AddComponent<Text>();
		text.text = "v1.0";
		text.alignment = TextAnchor.MiddleCenter;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.font = fUIManager.mFont;
		text.fontSize = 30;
		text.fontStyle = FontStyle.Bold;
		text.color = Color.black;
		gameObject.transform.localPosition = new Vector3(909.2f, 769.4f, 0f);
	}

	// Token: 0x0400134F RID: 4943
	private Login_UI _UI_Component;

	// Token: 0x04001350 RID: 4944
	public BaseSocket _BaseSocket;

	// Token: 0x04001351 RID: 4945
	public UserSocket _UserSocket;

	// Token: 0x04001352 RID: 4946
	private string Proxy = string.Empty;

	// Token: 0x04001353 RID: 4947
	public string Server = "s1";

	// Token: 0x04001354 RID: 4948
	private string Save_Data = string.Empty;

	// Token: 0x04001355 RID: 4949
	private string Client_Proxy_IP = string.Empty;

	// Token: 0x04001356 RID: 4950
	private string Client_Proxy_Port = string.Empty;

	// Token: 0x04001357 RID: 4951
	private bool _Login_IP_Text_Caret_State;

	// Token: 0x04001358 RID: 4952
	private bool _Login_Name_Text_Caret_State;

	// Token: 0x04001359 RID: 4953
	private bool _Login_Password_Text_Caret_State;

	// Token: 0x0400135A RID: 4954
	private RectTransform _RectTransform;

	// Token: 0x0400135B RID: 4955
	public int m_nLogin;
}

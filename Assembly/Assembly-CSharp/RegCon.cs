using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002CE RID: 718
public class RegCon : Window
{
	// Token: 0x06002102 RID: 8450 RVA: 0x001306B8 File Offset: 0x0012E8B8
	protected override void Awake()
	{
		base.Awake();
		RegCon.inst = this;
		this.RegBtn = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(this.RegBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		this.AccountInput = this.UIGameObj.Get_GameObj(1).GetComponent<InputField>();
		this.AccountInput.onEndEdit.AddListener(new UnityAction<string>(this.OnAccountEdits));
		this.MiMaInput = this.UIGameObj.Get_GameObj(2).GetComponent<InputField>();
		this.MiMaInput.onEndEdit.AddListener(new UnityAction<string>(this.OnMimaEdits));
		this.EndMiMaInput = this.UIGameObj.Get_GameObj(3).GetComponent<InputField>();
		this.EndMiMaInput.onEndEdit.AddListener(new UnityAction<string>(this.OnEndMimaEdits));
		this.NameInput = this.UIGameObj.Get_GameObj(4).GetComponent<InputField>();
		this.NameInput.onEndEdit.AddListener(new UnityAction<string>(this.OnNameEdits));
		EventTriggerListener.Get(this.NameInput.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.NameInput.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		this.ShenFenZhengInput = this.UIGameObj.Get_GameObj(5).GetComponent<InputField>();
		this.ShenFenZhengInput.onEndEdit.AddListener(new UnityAction<string>(this.OnSFXEdits));
		this.MsgText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.MsgText.text = string.Empty;
		this.MsgMMText = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.MsgMMText.text = string.Empty;
		this.MsgSFZText = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.MsgSFZText.text = string.Empty;
		this.MsgBtnText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.MsgBtnText.text = string.Empty;
		this.MsgnameText = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		this.MsgnameText.text = string.Empty;
		this.CloseBtn = this.UIGameObj.Get_GameObj(11);
		EventTriggerListener.Get(this.CloseBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseUI);
		this.MsgMIMAText = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.MsgMIMAText.text = string.Empty;
		ChatManager.ChatInput = true;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x0013096F File Offset: 0x0012EB6F
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x00130978 File Offset: 0x0012EB78
	private void OnFocusExit(GameObject obj)
	{
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x0013097B File Offset: 0x0012EB7B
	private void Start()
	{
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x0013097E File Offset: 0x0012EB7E
	private void Update()
	{
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x00130981 File Offset: 0x0012EB81
	private void OnCloseUI(GameObject obj)
	{
		RegCon.inst.Close();
		ChatManager.ChatInput = false;
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x00130998 File Offset: 0x0012EB98
	private void OnBtnClick(GameObject obj)
	{
		this.nRet = this.OnIsReg();
		bool flag = this.nRet < 0;
		if (flag)
		{
			this.MsgBtnText.text = Language.GetLang(18004, new object[0]);
		}
		else
		{
			base.StartCoroutine(Fun.shenfenzheng(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/interface/checkname.aspx?n=",
				this.NameInput.text,
				"&c=",
				this.ShenFenZhengInput.text,
				string.Empty
			}))));
		}
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x00130A30 File Offset: 0x0012EC30
	public void SendReg()
	{
		ChatManager.ChatInput = false;
		this.MsgBtnText.text = string.Empty;
		base.StartCoroutine(Fun.SendHttpRequest_Post(SysConfig.getWebURL(string.Concat(new string[]
		{
			"/interface/clientregapi.aspx?action=check&account=",
			this.AccountInput.text,
			"&password=",
			this.MiMaInput.text,
			"&idcard=",
			this.ShenFenZhengInput.text,
			"&online=0&ischeck=",
			this.nIsCheck.ToString(),
			string.Empty
		}))));
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x00130AD4 File Offset: 0x0012ECD4
	private void OnAccountEdits(string stext)
	{
		bool flag = !(stext == string.Empty);
		if (flag)
		{
			bool flag2 = stext.Length < 6 || stext.Length > 16;
			if (flag2)
			{
				this.MsgText.text = Language.GetLang(18001, new object[0]);
				this.nZhHao = 1;
			}
			else
			{
				this.MsgText.text = string.Empty;
				this.nZhHao = 0;
			}
		}
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x00130B54 File Offset: 0x0012ED54
	private void OnMimaEdits(string stext)
	{
		bool flag = stext == string.Empty;
		if (flag)
		{
			this.MsgMIMAText.text = string.Empty;
		}
		else
		{
			bool flag2 = stext.Length < 6;
			if (flag2)
			{
				this.MsgMIMAText.text = Language.GetLang(18006, new object[0]);
			}
			else
			{
				bool flag3 = this.IsChinaString(stext);
				if (flag3)
				{
					this.MsgMIMAText.text = Language.GetLang(18006, new object[0]);
					this.nIsZw = 1;
				}
				else
				{
					bool flag4 = this.button18_Click(stext) == 1;
					if (flag4)
					{
						this.MsgMIMAText.text = Language.GetLang(18006, new object[0]);
						this.nIsZw = 1;
					}
					else
					{
						this.MsgMIMAText.text = string.Empty;
						bool flag5 = this.EndMiMaInput.text != string.Empty;
						if (flag5)
						{
							bool flag6 = this.MiMaInput.text != this.EndMiMaInput.text;
							if (flag6)
							{
								this.MsgMMText.text = Language.GetLang(18002, new object[0]);
								this.nIsZw = 1;
							}
							else
							{
								this.MsgMMText.text = string.Empty;
								this.nIsZw = 0;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x00130CB4 File Offset: 0x0012EEB4
	private void OnEndMimaEdits(string stext)
	{
		bool flag = this.MiMaInput.text != this.EndMiMaInput.text;
		if (flag)
		{
			this.MsgMMText.text = Language.GetLang(18002, new object[0]);
			this.nIsZw = 1;
		}
		else
		{
			this.nIsZw = 0;
			this.MsgMMText.text = string.Empty;
		}
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x00130D24 File Offset: 0x0012EF24
	private void OnNameEdits(string stext)
	{
		bool flag = !(stext == string.Empty);
		if (flag)
		{
			bool flag2 = Regex.IsMatch(stext, "[\\u4e00-\\u9fa5a-zA-Z]+$");
			bool flag3 = string.IsNullOrEmpty(stext) || !flag2;
			if (flag3)
			{
				this.MsgnameText.text = Language.GetLang(18005, new object[0]);
				this.nName = 1;
			}
			else
			{
				this.MsgnameText.text = string.Empty;
				this.nName = 0;
			}
		}
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x00130DA8 File Offset: 0x0012EFA8
	private void OnSFXEdits(string stext)
	{
		bool flag = !(stext == string.Empty);
		if (flag)
		{
			bool flag2 = Fun.IDCardValidate(stext);
			if (flag2)
			{
				this.IsYoung(stext);
				this.MsgSFZText.text = string.Empty;
				this.nSfz = 0;
			}
			else
			{
				this.MsgSFZText.text = Language.GetLang(18003, new object[0]);
				this.nSfz = 1;
			}
		}
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x00130E20 File Offset: 0x0012F020
	private void IsYoung(string sCard)
	{
		bool flag = sCard.Length == 15;
		if (flag)
		{
			int num = int.Parse(sCard.Substring(6, 2));
			bool flag2 = Fun.Now.Year - (num + 1900) < 18;
			if (flag2)
			{
				this.nIsCheck = 1;
			}
			else
			{
				this.nIsCheck = 0;
			}
		}
		bool flag3 = sCard.Length == 18;
		if (flag3)
		{
			int num2 = int.Parse(sCard.Substring(6, 4));
			bool flag4 = Fun.Now.Year - num2 < 18;
			if (flag4)
			{
				this.nIsCheck = 1;
			}
			else
			{
				this.nIsCheck = 0;
			}
		}
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x00130ECC File Offset: 0x0012F0CC
	private int OnIsReg()
	{
		bool flag = this.AccountInput.text == string.Empty || this.MiMaInput.text == string.Empty || this.EndMiMaInput.text == string.Empty || this.NameInput.text == string.Empty || this.ShenFenZhengInput.text == string.Empty;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = this.nIsZw == 1 || this.nZhHao == 1 || this.nName == 1 || this.nSfz == 1;
			if (flag2)
			{
				result = -1;
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x00130F8C File Offset: 0x0012F18C
	public bool IsChinaString(string CString)
	{
		return Regex.IsMatch(CString, "[\\u4e00-\\u9fa5]");
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x00130FAC File Offset: 0x0012F1AC
	private int button18_Click(string stext)
	{
		bool flag = false;
		foreach (char c in stext)
		{
			bool flag2 = !char.IsLetter(c) && !char.IsNumber(c);
			if (flag2)
			{
				flag = true;
			}
		}
		bool flag3 = flag;
		int result;
		if (flag3)
		{
			result = 1;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x0013100C File Offset: 0x0012F20C
	public override void Reset()
	{
		base.Reset();
		ChatManager.ChatInput = true;
	}

	// Token: 0x04001DFF RID: 7679
	public static RegCon inst;

	// Token: 0x04001E00 RID: 7680
	private InputField AccountInput;

	// Token: 0x04001E01 RID: 7681
	private InputField MiMaInput;

	// Token: 0x04001E02 RID: 7682
	private InputField EndMiMaInput;

	// Token: 0x04001E03 RID: 7683
	private InputField NameInput;

	// Token: 0x04001E04 RID: 7684
	private InputField ShenFenZhengInput;

	// Token: 0x04001E05 RID: 7685
	private GameObject RegBtn;

	// Token: 0x04001E06 RID: 7686
	private GameObject CloseBtn;

	// Token: 0x04001E07 RID: 7687
	private Text MsgText;

	// Token: 0x04001E08 RID: 7688
	private Text MsgMIMAText;

	// Token: 0x04001E09 RID: 7689
	private Text MsgMMText;

	// Token: 0x04001E0A RID: 7690
	private Text MsgSFZText;

	// Token: 0x04001E0B RID: 7691
	private Text MsgBtnText;

	// Token: 0x04001E0C RID: 7692
	private Text MsgnameText;

	// Token: 0x04001E0D RID: 7693
	private int nRet;

	// Token: 0x04001E0E RID: 7694
	private int nIsCheck;

	// Token: 0x04001E0F RID: 7695
	private int nZhHao;

	// Token: 0x04001E10 RID: 7696
	private int nIsZw;

	// Token: 0x04001E11 RID: 7697
	private int nName;

	// Token: 0x04001E12 RID: 7698
	private int nSfz;
}

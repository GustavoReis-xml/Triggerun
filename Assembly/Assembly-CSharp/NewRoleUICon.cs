using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025B RID: 603
public class NewRoleUICon : Window
{
	// Token: 0x06001B85 RID: 7045 RVA: 0x0010462C File Offset: 0x0010282C
	protected override void Awake()
	{
		base.Awake();
		this.randNameInput = this.UIGameObj.Get_GameObj(1).GetComponent<InputField>();
		EventTriggerListener.Get(this.randNameInput.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		GameObject go = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRandName);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		go = this.UIGameObj.Get_GameObj(3);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCreate);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.tipText = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		NewRoleUICon.inst = this;
		GameRoot.inst.SendHttpRequest(1606, new ArrayList());
		fGameManager.m_nJoinState = 14;
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x00104726 File Offset: 0x00102926
	private void Start()
	{
		NewRoleUICon.iSex = UnityEngine.Random.Range(1, 3);
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00104735 File Offset: 0x00102935
	private void Update()
	{
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x00104738 File Offset: 0x00102938
	public override void Reset()
	{
		base.Reset();
		NewRoleUICon.iSex = UnityEngine.Random.Range(1, 3);
		this.GetRandName();
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x00104755 File Offset: 0x00102955
	public override void Close()
	{
		base.Close();
		this.tipText.text = string.Empty;
		ChatManager.ChatInput = false;
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x00104776 File Offset: 0x00102976
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00104780 File Offset: 0x00102980
	public void ShowTipMsg(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		int num2 = num;
		if (num2 != 1016)
		{
			if (num2 == 1018)
			{
				this.Close();
			}
		}
		else
		{
			this.tipText.text = Language.GetLang(10006, new object[0]);
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			RandomName.AddUsedName(Language.GetUTFCodeStr(arrayList[3].ToString()));
		}
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x00104800 File Offset: 0x00102A00
	private void OnClickCreate(GameObject go)
	{
		fGameManager.m_nJoinState = 15;
		GameRoot.inst.SendHttpRequest(1605, new ArrayList());
		string text = this.randNameInput.text.Trim();
		bool flag = string.IsNullOrEmpty(text) || Fun.Text_Length(text) < 4 || Fun.Text_Length(text) > 12;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10004, new object[0]);
		}
		else
		{
			bool flag2 = !Fun.CheckSpecialStr(text);
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10005, new object[0]);
			}
			else
			{
				bool flag3 = !this.IsLegalUserName(text);
				if (flag3)
				{
					this.tipText.text = Language.GetLang(10005, new object[0]);
				}
				else
				{
					fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 0, 0);
				}
			}
		}
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x001048F4 File Offset: 0x00102AF4
	private void OnCheckBack(int strstate, string sName)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10005, new object[0]);
		}
		else
		{
			this.m_sVideoCard = SystemInfo.graphicsDeviceName;
			GameRoot.LoginSocket.unFsorket.ToServer("OnCreatePlayer", new object[]
			{
				sName,
				0,
				NewRoleUICon.iSex,
				1,
				1,
				this.m_sVideoCard
			});
			GameRoot.inst.SendHttpRequest(1602, new ArrayList());
			fGameManager.m_nJoinState = 16;
		}
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x001049A3 File Offset: 0x00102BA3
	private void OnClickRandName(GameObject go)
	{
		this.GetRandName();
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x001049B0 File Offset: 0x00102BB0
	private void GetRandName()
	{
		string text = string.Empty;
		text = RandomName.GetRandName();
		this.randNameInput.text = text;
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x001049D8 File Offset: 0x00102BD8
	public bool IsLegalUserName(string sNick)
	{
		string pattern = "[一-龥a-zA-Z0-9_¤-㈩！-￥]+";
		return Regex.IsMatch(sNick, pattern);
	}

	// Token: 0x040018E0 RID: 6368
	public static NewRoleUICon inst;

	// Token: 0x040018E1 RID: 6369
	private InputField randNameInput;

	// Token: 0x040018E2 RID: 6370
	private Text tipText;

	// Token: 0x040018E3 RID: 6371
	public static int iSex;

	// Token: 0x040018E4 RID: 6372
	public string m_sVideoCard = string.Empty;

	// Token: 0x040018E5 RID: 6373
	public string m_sCPU = string.Empty;

	// Token: 0x040018E6 RID: 6374
	public int m_nMemory;
}

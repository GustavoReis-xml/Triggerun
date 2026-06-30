using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000127 RID: 295
public class FCMUICon : Window
{
	// Token: 0x06000ABA RID: 2746 RVA: 0x000628BC File Offset: 0x00060ABC
	protected override void Awake()
	{
		base.Awake();
		GameObject go = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.idCardInput = this.UIGameObj.Get_GameObj(1).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCertificate);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.nameInput = this.UIGameObj.Get_GameObj(3).GetComponent<InputField>();
		EventTriggerListener.Get(this.nameInput.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.nameInput.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		FCMUICon.inst = this;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x000629BB File Offset: 0x00060BBB
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x000629C4 File Offset: 0x00060BC4
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000629CD File Offset: 0x00060BCD
	private void Start()
	{
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x000629D0 File Offset: 0x00060BD0
	private void Update()
	{
		bool flag = this.visible && !HeroPlayer.isMouseShow;
		if (flag)
		{
			HeroPlayer.isMouseShow = true;
		}
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00062A00 File Offset: 0x00060C00
	public void SetFcmData(object szParam)
	{
		bool flag = szParam != null;
		if (flag)
		{
			this.szFcm = (ArrayList)szParam;
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x00062A24 File Offset: 0x00060C24
	public override void Reset()
	{
		base.Reset();
		this.nameInput.text = string.Empty;
		this.idCardInput.text = string.Empty;
		this.sIDCard = string.Empty;
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x00062A5C File Offset: 0x00060C5C
	private void OnClickCertificate(GameObject go)
	{
		string text = this.nameInput.text;
		bool flag = Regex.IsMatch(text, "[\\u4e00-\\u9fa5a-zA-Z]+$");
		bool flag2 = string.IsNullOrEmpty(text) || !flag || !Fun.CheckSpecialStr(text);
		if (flag2)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.sIDCard = this.idCardInput.text;
			bool flag3 = !Fun.CheckSpecialStr(this.sIDCard);
			if (flag3)
			{
				Debug.Log("OnClickCertificate///" + this.sIDCard);
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				base.StartCoroutine(Fun.TecentFCMCheck(SysConfig.getWebURL(string.Concat(new string[]
				{
					"/interface/qq_real_name_register.aspx?uid=",
					WebParam.client_account,
					"&idcards=",
					this.sIDCard,
					"&sname=",
					text,
					string.Empty
				}))));
			}
		}
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00062BC2 File Offset: 0x00060DC2
	public void SendFcmInfo(int ncheck)
	{
		HeroDataManager.GetInstance().SendFcmDataMsgToServer(13006, new ArrayList
		{
			this.sIDCard,
			ncheck
		});
		this.Close();
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00062BFC File Offset: 0x00060DFC
	private void OnClickClose(GameObject go)
	{
		this.Close();
		int num = HeroDataManager.GetInstance().CountOnLineTime();
		bool flag = num >= 10800 || (this.szFcm.Count >= 2 && (int)this.szFcm[1] >= 10800);
		if (flag)
		{
			this.szFcm.Clear();
			bool flag2 = fGameManager.LobbyPlayer != null;
			if (flag2)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
				{
					2,
					0
				});
			}
			bool flag3 = fGameManager.Heroplayer != null;
			if (flag3)
			{
				fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
				{
					2,
					0
				});
			}
			Application.Quit();
		}
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00062CE8 File Offset: 0x00060EE8
	public override void Close()
	{
		base.Close();
		HeroPlayer.isMouseShow = false;
	}

	// Token: 0x040008F8 RID: 2296
	public static FCMUICon inst;

	// Token: 0x040008F9 RID: 2297
	private InputField idCardInput;

	// Token: 0x040008FA RID: 2298
	private InputField nameInput;

	// Token: 0x040008FB RID: 2299
	private ArrayList szFcm = new ArrayList();

	// Token: 0x040008FC RID: 2300
	private string sIDCard;
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000299 RID: 665
public class playerreportCon : Window
{
	// Token: 0x06001E2A RID: 7722 RVA: 0x00119F0C File Offset: 0x0011810C
	protected override void Awake()
	{
		base.Awake();
		playerreportCon.inst = this;
		this.Bg = this.UIGameObj.Get_GameObj(4).GetComponent<RawImage>();
		this.Bg.enabled = false;
		this.rePlayer_nick = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.reContent = this.UIGameObj.Get_GameObj(13).GetComponent<InputField>();
		this.CancelBut = this.UIGameObj.Get_GameObj(14).GetComponent<Button>();
		this.SubmitBut = this.UIGameObj.Get_GameObj(15).GetComponent<Button>();
		this.submitInputObj = this.UIGameObj.Get_GameObj(16);
		this.submitokShowObj = this.UIGameObj.Get_GameObj(17);
		this.submitokButClo = this.UIGameObj.Get_GameObj(15).GetComponent<Button>();
		this.submitokButClo = this.UIGameObj.Get_GameObj(19).GetComponent<Button>();
		EventTriggerListener.Get(this.submitokButClo.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.submitokButClo_Click);
		this.SubmitBut.interactable = false;
		EventTriggerListener.Get(this.CancelBut.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.CancelBut_Click);
		EventTriggerListener.Get(this.SubmitBut.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.SubmitBut_Click);
		EventTriggerListener.Get(this.reContent.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.reContent.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		fUIManager.tipBlur.rawImage = this.Bg;
		fUIManager.tipBlur.enabled = true;
		this.UIGameObj.Get_GameObj(21).GetComponent<Text>().text = Language.GetLang(49001, new object[0]);
		this.UIGameObj.Get_GameObj(22).GetComponent<Text>().text = Language.GetLang(49002, new object[0]);
		this.UIGameObj.Get_GameObj(23).GetComponent<Text>().text = Language.GetLang(49003, new object[0]);
		this.UIGameObj.Get_GameObj(24).GetComponent<Text>().text = Language.GetLang(49004, new object[0]);
		this.UIGameObj.Get_GameObj(25).GetComponent<Text>().text = Language.GetLang(49005, new object[0]);
		this.UIGameObj.Get_GameObj(26).GetComponent<Text>().text = Language.GetLang(49006, new object[0]);
		this.UIGameObj.Get_GameObj(27).GetComponent<Text>().text = Language.GetLang(49007, new object[0]);
		this.UIGameObj.Get_GameObj(28).GetComponent<Text>().text = Language.GetLang(49008, new object[0]);
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x0011A20F File Offset: 0x0011840F
	private void Start()
	{
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x0011A214 File Offset: 0x00118414
	private void Update()
	{
		bool flag = this.Bg.texture != null;
		if (flag)
		{
			this.Bg.enabled = true;
		}
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x0011A248 File Offset: 0x00118448
	private void FixedUpdate()
	{
		this.countnt_txt = this.reContent.text;
		bool flag = this.countnt_txt.Length < 10 || this.countnt_txt.Length > 200;
		if (flag)
		{
			this.SubmitBut.interactable = false;
		}
		else
		{
			this.SubmitBut.interactable = true;
		}
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0011A2B0 File Offset: 0x001184B0
	public void SetData(ArrayList arr)
	{
		GameRoot.inst.Setting.Close_Setting_Option(null);
		bool flag = fGameManager.GetInstance().nNowScnID > 0;
		if (flag)
		{
			HeroPlayer.isMouseShow = true;
			HeroPlayer.isPlayerControl = false;
		}
		bool flag2 = arr.Count == 2;
		if (flag2)
		{
			this.rePlayer_cid = arr[0].ToString();
			this.rePlayer_nick.text = arr[1].ToString();
			this.reContent.text = string.Empty;
			this.my_Player_cid = PlayerDataManager.getInstance().HUserData.m_nCid.ToString();
			this.my_Player_unick = PlayerDataManager.getInstance().HUserData.m_sName;
			this.SetShow();
		}
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x0011A36E File Offset: 0x0011856E
	private void SetShow()
	{
		this.OpenState = true;
		this.submitInputObj.SetActive(true);
		this.submitokShowObj.SetActive(false);
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0011A392 File Offset: 0x00118592
	public void CancelBut_Click(GameObject obj)
	{
		this.Close();
		this.OpenState = false;
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0011A3A4 File Offset: 0x001185A4
	public void SubmitBut_Click(GameObject obj)
	{
		this.countnt_txt = this.reContent.text;
		this.countnt_txt = this.countnt_txt.Replace("\n", string.Empty);
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.my_Player_cid);
		arrayList.Add(Fun.EncodingUTF8(this.my_Player_unick));
		arrayList.Add(this.rePlayer_cid);
		arrayList.Add(Fun.EncodingUTF8(this.rePlayer_nick.text));
		arrayList.Add(this.countnt_txt);
		fGameManager.GetInstance().SendPlayerReportMsg(arrayList);
		this.submitInputObj.SetActive(false);
		this.submitokShowObj.SetActive(true);
		this.reContent.text = string.Empty;
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x0011A46B File Offset: 0x0011866B
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x0011A474 File Offset: 0x00118674
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x0011A47D File Offset: 0x0011867D
	public void submitokButClo_Click(GameObject obj)
	{
		this.Close();
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x0011A487 File Offset: 0x00118687
	public override void Reset()
	{
		base.Reset();
		fUIManager.tipBlur.rawImage = this.Bg;
		fUIManager.tipBlur.enabled = true;
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x0011A4B0 File Offset: 0x001186B0
	public override void Close()
	{
		base.Close();
		UnityEngine.Object.DestroyImmediate(this.Bg.texture);
		this.Bg.texture = null;
		this.Bg.enabled = false;
		this.OpenState = false;
		bool flag = fGameManager.GetInstance().nNowScnID > 0;
		if (flag)
		{
			HeroPlayer.isMouseShow = false;
			HeroPlayer.isPlayerControl = true;
		}
	}

	// Token: 0x04001BCB RID: 7115
	public static playerreportCon inst;

	// Token: 0x04001BCC RID: 7116
	public bool OpenState;

	// Token: 0x04001BCD RID: 7117
	private Text rePlayer_nick;

	// Token: 0x04001BCE RID: 7118
	private string rePlayer_cid;

	// Token: 0x04001BCF RID: 7119
	private InputField reContent;

	// Token: 0x04001BD0 RID: 7120
	private Button CancelBut;

	// Token: 0x04001BD1 RID: 7121
	private Button SubmitBut;

	// Token: 0x04001BD2 RID: 7122
	private string my_Player_cid;

	// Token: 0x04001BD3 RID: 7123
	private string my_Player_unick;

	// Token: 0x04001BD4 RID: 7124
	private string countnt_txt;

	// Token: 0x04001BD5 RID: 7125
	private GameObject submitInputObj;

	// Token: 0x04001BD6 RID: 7126
	private GameObject submitokShowObj;

	// Token: 0x04001BD7 RID: 7127
	private Button submitokButClo;

	// Token: 0x04001BD8 RID: 7128
	private RawImage Bg;
}

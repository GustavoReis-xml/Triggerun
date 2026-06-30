using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000206 RID: 518
public class jointestserverCon : Window
{
	// Token: 0x06001688 RID: 5768 RVA: 0x000D0C20 File Offset: 0x000CEE20
	protected override void Awake()
	{
		base.Awake();
		jointestserverCon.inst = this;
		this.Bg = this.UIGameObj.Get_GameObj(4).GetComponent<RawImage>();
		this.Bg.enabled = false;
		this.CancelBut = this.UIGameObj.Get_GameObj(2).GetComponent<Button>();
		this.SubmitBut = this.UIGameObj.Get_GameObj(1).GetComponent<Button>();
		EventTriggerListener.Get(this.CancelBut.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.CancelBut_Click);
		EventTriggerListener.Get(this.SubmitBut.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.SubmitBut_Click);
		this.ShowNumstext = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		fUIManager.tipBlur.rawImage = this.Bg;
		fUIManager.tipBlur.enabled = true;
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x000D0D02 File Offset: 0x000CEF02
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x000D0D0C File Offset: 0x000CEF0C
	private void Update()
	{
		bool flag = this.Bg.texture != null;
		if (flag)
		{
			this.Bg.enabled = true;
		}
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x000D0D3E File Offset: 0x000CEF3E
	private void FixedUpdate()
	{
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x000D0D44 File Offset: 0x000CEF44
	public void SetData(ArrayList arr)
	{
		GameRoot.inst.Setting.Close_Setting_Option(null);
		bool flag = fGameManager.GetInstance().nNowScnID > 0;
		if (flag)
		{
			HeroPlayer.isMouseShow = true;
			HeroPlayer.isPlayerControl = false;
		}
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x000D0D82 File Offset: 0x000CEF82
	private void SetShow()
	{
		this.OpenState = true;
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000D0D8C File Offset: 0x000CEF8C
	public void CancelBut_Click(GameObject obj)
	{
		this.Close();
		this.OpenState = false;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000D0DA0 File Offset: 0x000CEFA0
	public void SubmitBut_Click(GameObject obj)
	{
		fGameManager.SendMsgToHallServer(16104, new ArrayList
		{
			PlayerDataManager.getInstance().HUserData.m_nCid.ToString(),
			WebParam.client_account
		});
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x000D0DE7 File Offset: 0x000CEFE7
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x000D0DF0 File Offset: 0x000CEFF0
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x000D0DF9 File Offset: 0x000CEFF9
	public void submitokButClo_Click(GameObject obj)
	{
		this.Close();
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x000D0E04 File Offset: 0x000CF004
	public override void Reset()
	{
		base.Reset();
		fUIManager.tipBlur.rawImage = this.Bg;
		fUIManager.tipBlur.enabled = true;
		bool flag = ActivityManager.GetInstance().JoinTestActData.Count == 6;
		if (flag)
		{
			int num = (int)ActivityManager.GetInstance().JoinTestActData[2];
			int num2 = (int)ActivityManager.GetInstance().JoinTestActData[5];
			bool flag2 = num == num2;
			if (flag2)
			{
				this.ShowNumstext.text = Language.GetLang(48001, new object[0]);
				this.SubmitBut.interactable = false;
			}
			bool flag3 = num - num2 > 0;
			if (flag3)
			{
				this.ShowNumstext.text = Language.GetLang(48002, new object[0]) + (num - num2).ToString();
				this.SubmitBut.interactable = true;
			}
		}
		else
		{
			this.ShowNumstext.text = Language.GetLang(48001, new object[0]);
			this.SubmitBut.interactable = false;
		}
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x000D0F24 File Offset: 0x000CF124
	public override void Close()
	{
		base.Close();
		UnityEngine.Object.DestroyImmediate(this.Bg.texture);
		this.Bg.texture = null;
		this.Bg.enabled = false;
		this.OpenState = false;
	}

	// Token: 0x04001196 RID: 4502
	public static jointestserverCon inst;

	// Token: 0x04001197 RID: 4503
	public bool OpenState;

	// Token: 0x04001198 RID: 4504
	private Button CancelBut;

	// Token: 0x04001199 RID: 4505
	private Button SubmitBut;

	// Token: 0x0400119A RID: 4506
	private RawImage Bg;

	// Token: 0x0400119B RID: 4507
	private Text ShowNumstext;
}

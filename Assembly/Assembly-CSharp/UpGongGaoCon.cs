using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200048C RID: 1164
public class UpGongGaoCon : Window
{
	// Token: 0x06002CF8 RID: 11512 RVA: 0x0018B988 File Offset: 0x00189B88
	protected override void Awake()
	{
		base.Awake();
		this.sTitle = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.sContent = this.UIGameObj.Get_GameObj(1).GetComponent<LinkImageText>();
		this.Bg = this.UIGameObj.Get_GameObj(3).GetComponent<RawImage>();
		this.Bg.enabled = false;
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(2).gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnYes);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(4).gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLunTan);
		UpGongGaoCon.inst = this;
		this.Reset();
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x0018BA4C File Offset: 0x00189C4C
	private void Update()
	{
		bool flag = this.Bg.texture != null;
		if (flag)
		{
			this.Bg.enabled = true;
		}
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x0018BA80 File Offset: 0x00189C80
	public override void Reset()
	{
		base.Reset();
		ArrayList szNewUpMsg = PlayerDataManager.getInstance().HUserData.m_szNewUpMsg;
		bool flag = szNewUpMsg.Count == 0;
		if (flag)
		{
			this.Close();
		}
		else
		{
			this.nMsgID = (int)szNewUpMsg[0];
			this.sTitle.text = szNewUpMsg[1].ToString();
			string text = szNewUpMsg[2].ToString();
			text = text.Replace("\\n", "\n");
			text = text.Replace("\\", string.Empty);
			this.sContent.m_PreText = text;
			LinkImageText linkImageText = this.sContent;
			bool flag2 = !(linkImageText == null);
			if (flag2)
			{
				linkImageText.onHrefClick.AddListener(new UnityAction<string>(this.OnHrefClick));
				fUIManager.tipBlur.rawImage = this.Bg;
				fUIManager.tipBlur.enabled = true;
			}
		}
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x0018BB71 File Offset: 0x00189D71
	private void OnHrefClick(string hrefName)
	{
		Application.OpenURL(hrefName);
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x0018BB7B File Offset: 0x00189D7B
	public override void Close()
	{
		base.Close();
		UnityEngine.Object.DestroyImmediate(this.Bg.texture);
		this.Bg.texture = null;
		this.Bg.enabled = false;
	}

	// Token: 0x06002CFD RID: 11517 RVA: 0x0018BBB0 File Offset: 0x00189DB0
	private void OnYes(GameObject obj)
	{
		this.Close();
		bool flag = this.nMsgID != 0;
		if (flag)
		{
			PlayerDataManager.getInstance().HUserData.m_szReadUpMsgID.Add(this.nMsgID);
			fGameManager.SendMsgToHallServer(3003, new ArrayList
			{
				this.nMsgID,
				0
			});
		}
	}

	// Token: 0x06002CFE RID: 11518 RVA: 0x0018BC24 File Offset: 0x00189E24
	private void OnClickLunTan(GameObject obj)
	{
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Application.OpenURL("http://www.triggerun.com/");
		}
		else
		{
			Application.OpenURL("http://qqgame.qq.com/gameinfo/10677.html");
		}
	}

	// Token: 0x04002620 RID: 9760
	public static UpGongGaoCon inst;

	// Token: 0x04002621 RID: 9761
	private Text sTitle;

	// Token: 0x04002622 RID: 9762
	private LinkImageText sContent;

	// Token: 0x04002623 RID: 9763
	private RawImage Bg;

	// Token: 0x04002624 RID: 9764
	private int nMsgID;
}

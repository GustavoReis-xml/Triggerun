using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027B RID: 635
public class OneMailCon : Window
{
	// Token: 0x06001D4F RID: 7503 RVA: 0x0010ED8C File Offset: 0x0010CF8C
	protected override void Awake()
	{
		base.Awake();
		this.mMailState = this.UIGameObj.Get_GameObj(0).GetComponent<Image>();
		this.tMailTitle = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.tMailText = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.pItem = this.UIGameObj.Get_GameObj(3);
		this.tMailTime = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.pDelBtn = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(this.pDelBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnDelClick);
		EventTriggerListener.Get(this.pDelBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(base.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClick);
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x0010EE7C File Offset: 0x0010D07C
	public void OnInit(int nId, int nNum)
	{
		this.m_nMailId = nId;
		this.m_nNum = nNum;
		bool flag = this.isLoad;
		if (flag)
		{
			this.OnShowInfo();
		}
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0010EEAC File Offset: 0x0010D0AC
	private void Start()
	{
		this.isLoad = true;
		bool flag = this.m_nMailId != 0;
		if (flag)
		{
			this.OnShowInfo();
		}
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x0010EED8 File Offset: 0x0010D0D8
	public void OnShowInfo()
	{
		bool flag = this.m_nNum == 0 && MailUICon.inst != null;
		if (flag)
		{
			MailUICon.inst.OnShowDetailedMail(this.m_nMailId, base.gameObject);
		}
		bool flag2 = MailManager.GetInstance().AllMailData.ContainsKey(this.m_nMailId);
		if (flag2)
		{
			MailInfo mailInfo = MailManager.GetInstance().AllMailData[this.m_nMailId];
			MailStateInfo mailStateInfo = MailManager.GetInstance().AllMailStateList[this.m_nMailId];
			this.tMailTitle.text = mailInfo.m_sMailtitle;
			string text = mailInfo.m_sMailtext;
			text = text.Replace("\\\\r\\\\n", string.Empty);
			text = text.Replace("\\n", string.Empty);
			text = text.Replace("\\", string.Empty);
			bool flag3 = text.Length > 15;
			if (flag3)
			{
				text = text.Substring(0, 15) + "......";
			}
			this.tMailText.text = text;
			this.tMailTime.text = mailInfo.m_sCreateTime;
			this.mMailState.sprite = Window.spriteName(AtlasType.ATLAS_MAIL, "youjian" + (mailStateInfo.m_nIsRead + 1).ToString());
			bool flag4 = mailInfo.m_szItem.Count < 1;
			if (flag4)
			{
				this.pItem.SetActive(false);
			}
			else
			{
				bool flag5 = mailStateInfo.m_nIsGet == 1;
				if (flag5)
				{
					this.pItem.SetActive(false);
				}
				else
				{
					this.pItem.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x0010F084 File Offset: 0x0010D284
	public void OnUpdateMailInfo(int nType)
	{
		this.mMailState.sprite = Window.spriteName(AtlasType.ATLAS_MAIL, "youjian2");
		bool flag = nType != 1;
		if (flag)
		{
			this.pItem.SetActive(false);
		}
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x0010F0C8 File Offset: 0x0010D2C8
	private void OnClick(GameObject obj)
	{
		bool flag = MailUICon.inst != null;
		if (flag)
		{
			MailUICon.inst.OnShowDetailedMail(this.m_nMailId, base.gameObject);
		}
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x0010F100 File Offset: 0x0010D300
	public void OnDelClick(GameObject obj)
	{
		bool flag = MailManager.GetInstance().AllMailStateList.ContainsKey(this.m_nMailId);
		if (flag)
		{
			MailStateInfo mailStateInfo = MailManager.GetInstance().AllMailStateList[this.m_nMailId];
			MailInfo mailInfo = MailManager.GetInstance().AllMailData[this.m_nMailId];
			bool flag2 = mailStateInfo.m_nIsGet == 0 && mailInfo.m_szItem.Count > 0;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38503, new object[0]), "ffdd00", "system");
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38512, new object[]
				{
					mailInfo.m_sMailtitle
				}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnDel), this.m_nMailId, null, null);
			}
		}
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x0010F20F File Offset: 0x0010D40F
	private void OnDel(object obj)
	{
		MailManager.OnSendMsgToMailServer(3405, new ArrayList
		{
			this.m_nMailId,
			this.m_nMailId
		});
	}

	// Token: 0x040019EA RID: 6634
	private bool isLoad;

	// Token: 0x040019EB RID: 6635
	private int m_nMailId;

	// Token: 0x040019EC RID: 6636
	private int m_nNum;

	// Token: 0x040019ED RID: 6637
	private Image mMailState;

	// Token: 0x040019EE RID: 6638
	private Text tMailTitle;

	// Token: 0x040019EF RID: 6639
	private Text tMailText;

	// Token: 0x040019F0 RID: 6640
	private GameObject pItem;

	// Token: 0x040019F1 RID: 6641
	private Text tMailTime;

	// Token: 0x040019F2 RID: 6642
	private GameObject pDelBtn;
}

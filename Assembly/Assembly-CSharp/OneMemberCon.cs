using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027C RID: 636
public class OneMemberCon : Window
{
	// Token: 0x06001D58 RID: 7512 RVA: 0x0010F250 File Offset: 0x0010D450
	protected override void Awake()
	{
		base.Awake();
		OneMemberCon.inst = this;
		this.UserLv = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.UserName = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.UserHuoYue = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.UserSkyLv = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.UserZhiWei = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.UserOnLine = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.pTiChuBtn = this.UIGameObj.Get_GameObj(6);
		this.pBg = this.UIGameObj.Get_GameObj(7);
		this.pBgOver = this.UIGameObj.Get_GameObj(8);
		this.pMemberInfoPanel = this.UIGameObj.Get_GameObj(9);
		this.pResquestInfoPanel = this.UIGameObj.Get_GameObj(10);
		this.ReqUserLv = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.ReqUserName = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.ReqUserSkyScore = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.ReqUserSkyLv = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.ReqUserTime = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.pTongYiBtn = this.UIGameObj.Get_GameObj(16);
		this.pJuJueBtn = this.UIGameObj.Get_GameObj(17);
		this.UserSkyIco = this.UIGameObj.Get_GameObj(18).GetComponent<Image>();
		this.ReqUserSkyIco = this.UIGameObj.Get_GameObj(19).GetComponent<Image>();
		EventTriggerListener.Get(this.pTiChuBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTiChuGuildClick);
		EventTriggerListener.Get(this.pTiChuBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pTongYiBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTongYiRequsetGuildClick);
		EventTriggerListener.Get(this.pTongYiBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pJuJueBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnJuJueRequsetGuildClick);
		EventTriggerListener.Get(this.pJuJueBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(base.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnGuildEnter);
		EventTriggerListener.Get(base.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnGuildExit);
		EventTriggerListener.Get(base.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGuildClick);
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0010F528 File Offset: 0x0010D728
	public void OnGuildClick(GameObject obj)
	{
		bool flag = this.m_nType == 2;
		if (!flag)
		{
			bool flag2 = this.m_sUserName == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(47016, new object[0]), "ffdd00", "system");
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				bool flag3 = GuildManager.GetInstance().m_nZhiWei != 2;
				if (flag3)
				{
					arrayList.Add(5);
					arrayList.Add(6);
					arrayList.Add(7);
				}
				else
				{
					bool flag4 = this.m_nZhiWei == 1;
					if (flag4)
					{
						arrayList.Add(6);
					}
					bool flag5 = this.m_nZhiWei == 0;
					if (flag5)
					{
						arrayList.Add(7);
					}
				}
				ChatManager.GetInstance().OnMsgTip(this.m_sUserName, 3, arrayList, this.m_nCid);
			}
		}
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x0010F62E File Offset: 0x0010D82E
	public void OnSetData(ArrayList arr, int nNum, int nType)
	{
		this.m_szData = (ArrayList)arr.Clone();
		this.m_nBianHao = nNum;
		this.m_nType = nType;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x0010F650 File Offset: 0x0010D850
	private void Start()
	{
		bool flag = this.m_nBianHao % 2 == 0;
		if (flag)
		{
			this.pBg.SetActive(true);
		}
		else
		{
			this.pBg.SetActive(false);
		}
		bool flag2 = this.m_nType == 1;
		if (flag2)
		{
			string text = string.Empty;
			this.m_sUserName = Fun.DecodingUTF8(this.m_szData[1].ToString());
			int num = (int)this.m_szData[5];
			this.m_nCid = (int)this.m_szData[8];
			text = ((num != 0) ? "#ffffff" : "#625D5D");
			bool flag3 = this.m_sUserName == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag3)
			{
				text = "#00ff00";
			}
			this.UserLv.text = string.Concat(new string[]
			{
				"<color='",
				text,
				"'>",
				this.m_szData[0].ToString(),
				"</color>"
			});
			this.UserName.text = string.Concat(new string[]
			{
				"<color='",
				text,
				"'>",
				this.m_sUserName,
				"</color>"
			});
			this.UserHuoYue.text = string.Concat(new string[]
			{
				"<color='",
				text,
				"'>",
				this.m_szData[2].ToString(),
				"</color>"
			});
			this.UserSkyLv.text = string.Empty;
			this.UserSkyIco.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_" + this.m_szData[3].ToString());
			this.m_nZhiWei = (int)this.m_szData[4];
			this.UserZhiWei.text = string.Concat(new string[]
			{
				"<color='",
				text,
				"'>",
				GuildZhiWei.GetZhiWeiStr((int)this.m_szData[4]),
				"</color>"
			});
			this.UserOnLine.text = GuildUserState.GetOnLineStr((int)this.m_szData[5], this.m_szData[6].ToString());
			this.pMemberInfoPanel.SetActive(true);
			this.pResquestInfoPanel.SetActive(false);
			bool flag4 = GuildManager.GetInstance().m_nZhiWei > 0;
			if (flag4)
			{
				bool flag5 = this.m_sUserName == PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag5)
				{
					this.pTiChuBtn.SetActive(false);
				}
				else
				{
					bool flag6 = this.m_nZhiWei >= GuildManager.GetInstance().m_nZhiWei;
					if (flag6)
					{
						this.pTiChuBtn.SetActive(false);
					}
					else
					{
						this.pTiChuBtn.SetActive(true);
					}
				}
			}
			else
			{
				this.pTiChuBtn.SetActive(false);
			}
		}
		else
		{
			this.pMemberInfoPanel.SetActive(false);
			this.pResquestInfoPanel.SetActive(true);
			this.m_nDataId = (int)this.m_szData[0];
			this.m_sRequstUserName = Fun.DecodingUTF8(this.m_szData[3].ToString());
			this.ReqUserLv.text = this.m_szData[4].ToString();
			this.ReqUserName.text = this.m_sRequstUserName;
			this.ReqUserSkyScore.text = this.m_szData[5].ToString();
			this.ReqUserTime.text = this.m_szData[7].ToString();
			this.ReqUserSkyLv.text = string.Empty;
			this.ReqUserSkyIco.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_" + this.m_szData[6].ToString());
			base.name = this.m_nDataId.ToString();
		}
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x0010FA81 File Offset: 0x0010DC81
	private void OnGuildEnter(GameObject obj)
	{
		this.pBgOver.SetActive(true);
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x0010FA91 File Offset: 0x0010DC91
	private void OnGuildExit(GameObject obj)
	{
		this.pBgOver.SetActive(false);
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x0010FAA4 File Offset: 0x0010DCA4
	private void OnTiChuGuildClick(GameObject obj)
	{
		bool flag = GuildManager.GetInstance().m_nZhiWei == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = this.m_sUserName == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38055, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = this.m_sUserName == GuildManager.GetInstance().m_sTanheUser;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(46144, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38056, new object[]
					{
						this.m_sUserName
					}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnTichu), null, null, null);
				}
			}
		}
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x0010FC4C File Offset: 0x0010DE4C
	private void OnTichu(object obj)
	{
		GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
		{
			91005,
			new ArrayList
			{
				Fun.EncodingUTF8(this.m_sUserName),
				this.m_nCid
			}
		});
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x0010FCB0 File Offset: 0x0010DEB0
	private void OnTongYiRequsetGuildClick(GameObject obj)
	{
		bool flag = GuildManager.GetInstance().m_nZhiWei == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91015,
				new ArrayList
				{
					Fun.EncodingUTF8(this.m_sRequstUserName),
					this.m_nDataId
				}
			});
		}
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x0010FD74 File Offset: 0x0010DF74
	private void OnJuJueRequsetGuildClick(GameObject obj)
	{
		bool flag = GuildManager.GetInstance().m_nZhiWei == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91016,
				new ArrayList
				{
					Fun.EncodingUTF8(this.m_sRequstUserName),
					this.m_nDataId
				}
			});
		}
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x0010FE35 File Offset: 0x0010E035
	private void Update()
	{
	}

	// Token: 0x040019F3 RID: 6643
	public static OneMemberCon inst;

	// Token: 0x040019F4 RID: 6644
	private ArrayList m_szData;

	// Token: 0x040019F5 RID: 6645
	private int m_nBianHao;

	// Token: 0x040019F6 RID: 6646
	private GameObject pBg;

	// Token: 0x040019F7 RID: 6647
	private GameObject pBgOver;

	// Token: 0x040019F8 RID: 6648
	private Text UserLv;

	// Token: 0x040019F9 RID: 6649
	private Text UserName;

	// Token: 0x040019FA RID: 6650
	private Text UserHuoYue;

	// Token: 0x040019FB RID: 6651
	private Text UserSkyLv;

	// Token: 0x040019FC RID: 6652
	private Text UserZhiWei;

	// Token: 0x040019FD RID: 6653
	private Text UserOnLine;

	// Token: 0x040019FE RID: 6654
	private GameObject pTiChuBtn;

	// Token: 0x040019FF RID: 6655
	private Image UserSkyIco;

	// Token: 0x04001A00 RID: 6656
	private int m_nType;

	// Token: 0x04001A01 RID: 6657
	private GameObject pMemberInfoPanel;

	// Token: 0x04001A02 RID: 6658
	private GameObject pResquestInfoPanel;

	// Token: 0x04001A03 RID: 6659
	private Text ReqUserLv;

	// Token: 0x04001A04 RID: 6660
	private Text ReqUserName;

	// Token: 0x04001A05 RID: 6661
	private Text ReqUserSkyScore;

	// Token: 0x04001A06 RID: 6662
	private Text ReqUserSkyLv;

	// Token: 0x04001A07 RID: 6663
	private Text ReqUserTime;

	// Token: 0x04001A08 RID: 6664
	private Image ReqUserSkyIco;

	// Token: 0x04001A09 RID: 6665
	private GameObject pTongYiBtn;

	// Token: 0x04001A0A RID: 6666
	private GameObject pJuJueBtn;

	// Token: 0x04001A0B RID: 6667
	private string m_sRequstUserName = string.Empty;

	// Token: 0x04001A0C RID: 6668
	private int m_nDataId = -1;

	// Token: 0x04001A0D RID: 6669
	private string m_sUserName = string.Empty;

	// Token: 0x04001A0E RID: 6670
	private int m_nZhiWei;

	// Token: 0x04001A0F RID: 6671
	private int m_nCid;
}

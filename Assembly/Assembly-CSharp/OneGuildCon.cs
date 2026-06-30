using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000277 RID: 631
public class OneGuildCon : Window
{
	// Token: 0x06001D35 RID: 7477 RVA: 0x0010DDB4 File Offset: 0x0010BFB4
	protected override void Awake()
	{
		base.Awake();
		OneGuildCon.inst = this;
		this.pBg = this.UIGameObj.Get_GameObj(0);
		this.pBgOver = this.UIGameObj.Get_GameObj(1);
		this.GuildRank = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.GuildName = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.GuildLv = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.GuildDuiZhang = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.GuildRenShu = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.GuildCreateTime = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.pShenQingBtn = this.UIGameObj.Get_GameObj(8);
		this.pRanlBgImg = this.UIGameObj.Get_GameObj(9).GetComponent<Image>();
		this.TShengQingBtnTxt = this.pShenQingBtn.transform.FindChild("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.pShenQingBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnAddGuildClick);
		EventTriggerListener.Get(this.pShenQingBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(base.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnGuildEnter);
		EventTriggerListener.Get(base.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnGuildExit);
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x0010DF3B File Offset: 0x0010C13B
	public void OnSetData(ArrayList arr, int nNum, ArrayList szRequest, string sFindGuildName = "")
	{
		this.m_szGuildInfo = arr;
		this.m_nBianHao = nNum;
		this.m_nGuildId = (int)this.m_szGuildInfo[0];
		this.m_szRequest = szRequest;
		this.m_sFindGuildName = sFindGuildName;
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x0010DF74 File Offset: 0x0010C174
	private void OnAddGuildClick(GameObject obj)
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel < 10;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38054, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91003,
				new ArrayList
				{
					this.m_nGuildId,
					Fun.EncodingUTF8(this.m_sDuiZhang)
				}
			});
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x0010E03B File Offset: 0x0010C23B
	private void OnGuildEnter(GameObject obj)
	{
		this.pBgOver.SetActive(true);
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0010E04B File Offset: 0x0010C24B
	private void OnGuildExit(GameObject obj)
	{
		this.pBgOver.SetActive(false);
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x0010E05C File Offset: 0x0010C25C
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
		this.GuildName.text = Fun.DecodingUTF8(this.m_szGuildInfo[2].ToString());
		this.GuildLv.text = this.m_szGuildInfo[3].ToString();
		this.m_sDuiZhang = Fun.DecodingUTF8(this.m_szGuildInfo[4].ToString());
		this.GuildDuiZhang.text = this.m_sDuiZhang;
		this.GuildRenShu.text = this.m_szGuildInfo[5].ToString() + "/" + this.m_szGuildInfo[7].ToString();
		this.GuildCreateTime.text = this.m_szGuildInfo[6].ToString();
		this.UpdateShengQingBtnState(this.m_szRequest.IndexOf(this.m_nGuildId) == -1, (GuildManager.GetInstance().m_nGuildId != this.m_nGuildId) ? string.Empty : "1");
		this.m_nRank = (int)this.m_szGuildInfo[1];
		bool flag2 = this.m_nRank <= 3;
		if (flag2)
		{
			this.GuildRank.text = string.Empty;
			this.pRanlBgImg.sprite = Window.spriteName(AtlasType.ATLAS_GUILD, "guild_" + this.m_nRank.ToString());
			this.pRanlBgImg.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(24f, 37f);
		}
		else
		{
			this.GuildRank.text = this.m_nRank.ToString();
			this.pRanlBgImg.sprite = Window.spriteName(AtlasType.ATLAS_GUILD, "guild_4");
			this.pRanlBgImg.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(45f, 25f);
		}
		bool flag3 = this.m_sFindGuildName == Fun.DecodingUTF8(this.m_szGuildInfo[2].ToString());
		if (flag3)
		{
			this.pBgOver.SetActive(true);
		}
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x0010E2B4 File Offset: 0x0010C4B4
	public void UpdateShengQingBtnState(bool isClick, string sShowText = "")
	{
		bool flag = !isClick;
		if (flag)
		{
			this.pShenQingBtn.GetComponent<Button>().interactable = false;
			bool flag2 = sShowText == string.Empty;
			if (flag2)
			{
				this.TShengQingBtnTxt.text = Language.GetLang(38050, new object[0]);
			}
			else
			{
				this.TShengQingBtnTxt.text = Language.GetLang(38051, new object[0]);
			}
		}
		else
		{
			bool flag3 = sShowText == string.Empty;
			if (flag3)
			{
				this.pShenQingBtn.GetComponent<Button>().interactable = true;
				this.TShengQingBtnTxt.text = Language.GetLang(38052, new object[0]);
			}
			else
			{
				this.pShenQingBtn.GetComponent<Button>().interactable = false;
				this.TShengQingBtnTxt.text = Language.GetLang(38051, new object[0]);
			}
			bool flag4 = (int)this.m_szGuildInfo[5] >= (int)this.m_szGuildInfo[7];
			if (flag4)
			{
				this.TShengQingBtnTxt.text = Language.GetLang(38053, new object[0]);
				this.pShenQingBtn.GetComponent<Button>().interactable = false;
			}
		}
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x0010E3FB File Offset: 0x0010C5FB
	private void Update()
	{
	}

	// Token: 0x040019BE RID: 6590
	public static OneGuildCon inst;

	// Token: 0x040019BF RID: 6591
	private ArrayList m_szGuildInfo;

	// Token: 0x040019C0 RID: 6592
	private ArrayList m_szRequest;

	// Token: 0x040019C1 RID: 6593
	private int m_nBianHao;

	// Token: 0x040019C2 RID: 6594
	private GameObject pBg;

	// Token: 0x040019C3 RID: 6595
	private GameObject pBgOver;

	// Token: 0x040019C4 RID: 6596
	private Text GuildRank;

	// Token: 0x040019C5 RID: 6597
	private Text GuildName;

	// Token: 0x040019C6 RID: 6598
	private Text GuildLv;

	// Token: 0x040019C7 RID: 6599
	private Text GuildDuiZhang;

	// Token: 0x040019C8 RID: 6600
	private Text GuildRenShu;

	// Token: 0x040019C9 RID: 6601
	private Text GuildCreateTime;

	// Token: 0x040019CA RID: 6602
	private GameObject pShenQingBtn;

	// Token: 0x040019CB RID: 6603
	private Text TShengQingBtnTxt;

	// Token: 0x040019CC RID: 6604
	public int m_nGuildId;

	// Token: 0x040019CD RID: 6605
	private string m_sDuiZhang;

	// Token: 0x040019CE RID: 6606
	private Image pRanlBgImg;

	// Token: 0x040019CF RID: 6607
	private int m_nRank;

	// Token: 0x040019D0 RID: 6608
	private string m_sFindGuildName = string.Empty;
}

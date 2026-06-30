using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
public class HeroDeadUiCon : Window
{
	// Token: 0x0600116D RID: 4461 RVA: 0x000A338C File Offset: 0x000A158C
	protected override void Awake()
	{
		base.Awake();
		this._DeadWin = base.transform.Find("deadwin").gameObject;
		this._DeadWin.transform.Find("xmzlab").GetComponent<Text>().text = Language.GetLang(3008, new object[0]);
		this.xmzname = this._DeadWin.transform.Find("xmzname").GetComponent<Text>();
		this.sxyxlab = this._DeadWin.transform.Find("sxyxlab").GetComponent<Text>();
		this.daojishi = this._DeadWin.transform.Find("daojishi").gameObject;
		this.daojishi.transform.Find("cssjlab").GetComponent<Text>().text = Language.GetLang(3009, new object[0]);
		this.Textbssjlab = this.daojishi.transform.Find("bssjlab").GetComponent<Text>();
		this.Textbssjlab.text = string.Empty;
		this.imgjdt = this.daojishi.transform.Find("jdt").GetComponent<Image>();
		this.Texdjs = this.daojishi.transform.Find("djs").GetComponent<Text>();
		this._DownPic = this._DeadWin.transform.Find("downpic").gameObject;
		this.TextQhlab = this._DownPic.transform.Find("qhlab").GetComponent<Text>();
		this.zwjtTxt = this._DownPic.transform.Find("zwpic").GetComponent<Text>();
		this.oGrsdUI = this._DownPic.transform.Find("grsdmsg").gameObject;
		this.oGrsdUIBg = this.oGrsdUI.transform.Find("bg").GetComponent<Image>();
		this.oGrsdUImsg = this.oGrsdUI.transform.Find("msg").GetComponent<Text>();
		this.oGrsdUI.SetActive(false);
		this.TextQhlab.text = string.Empty;
		this.TextQhlab.enabled = false;
		this._DeadWin.SetActive(false);
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000A35E5 File Offset: 0x000A17E5
	public void OnUpdateNetLiving(NetLiving _net)
	{
		this._NetLiving = fGameManager.Heroplayer;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000A35F4 File Offset: 0x000A17F4
	private void Update()
	{
		bool flag = !(this._DeadWin == null) && this.fDjsMaxTime > 0f;
		if (flag)
		{
			this.OnDeadDjsEffect();
		}
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x000A3630 File Offset: 0x000A1830
	public override void Close()
	{
		this.fDjsMaxTime = 0f;
		this.fDjsNowTime = 0f;
		gameuiCon.inst.mainMsgUI.OnDeadname(string.Empty);
		this._DeadWin.SetActive(false);
		bool flag = ZhanDianCon.inst != null;
		if (flag)
		{
			ZhanDianCon.inst.Open();
		}
		fUISoundManager.instance.StopSound("music_dead", 0);
		base.Close();
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000A36AC File Offset: 0x000A18AC
	public void OpenDeadWin()
	{
		string text = string.Empty;
		bool flag = this._NetLiving == null || this._DeadWin.activeSelf;
		if (!flag)
		{
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
			DateTime d = this._NetLiving.playerCommon.m_dPlayerDeathDate.AddSeconds((double)this._NetLiving.playerCommon.m_nDeathTimeLong);
			int num = (int)(d - Fun.Now).TotalSeconds;
			this._DownPic.SetActive(true);
			this._DeadWin.SetActive(true);
			this.mOpenBool = true;
			this.xmzname.text = string.Empty;
			this.sxyxlab.text = string.Empty;
			bool flag2 = this._NetLiving.GameAttackPlayer != null;
			if (flag2)
			{
				this.xmzname.text = this._NetLiving.GameAttackPlayer.playerCommon.m_sName.ToString();
				bool flag3 = fGameManager.mloadConfig.AllHeroList.ContainsKey(this._NetLiving.GameAttackPlayer.playerCommon.m_nModelIndex);
				if (flag3)
				{
					HeroData heroData = fGameManager.mloadConfig.AllHeroList[this._NetLiving.GameAttackPlayer.playerCommon.m_nModelIndex];
					bool flag4 = heroData != null;
					if (flag4)
					{
						text = heroData.hname;
					}
				}
				this.sxyxlab.text = Language.GetLang(3003, new object[]
				{
					text
				});
			}
			this.daojishi.SetActive(false);
			bool flag5 = MatchManager.GetInstance().m_pFbData.nType != 4;
			if (flag5)
			{
				this.fDjsMaxTime = (float)num;
				this.fDjsNowTime = 0f;
				bool flag6 = WebParam.client_language == "baxi";
				if (flag6)
				{
					this.zwjtTxt.text = string.Empty;
				}
				else
				{
					this.zwjtTxt.text = Language.GetLang(46132, new object[0]);
				}
			}
			else
			{
				this.zwjtTxt.text = Language.GetLang(46132, new object[0]);
			}
			bool flag7 = ZhanDianCon.inst != null;
			if (flag7)
			{
			}
			fUISoundManager.instance.PlayUISound("music_dead", 0, null, 20);
		}
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000A3900 File Offset: 0x000A1B00
	private void OnDeadDjsEffect()
	{
		bool flag = MatchManager.GetInstance().m_nMatchType == 3;
		if (flag)
		{
			int pveUserDeahState = KingTrainManager.GetInstance().GetPveUserDeahState();
			bool flag2 = pveUserDeahState == 1;
			if (flag2)
			{
				bool activeSelf = this.daojishi.activeSelf;
				if (activeSelf)
				{
					this.daojishi.SetActive(false);
				}
				return;
			}
			bool flag3 = !this.daojishi.activeSelf;
			if (flag3)
			{
				DateTime d = this._NetLiving.playerCommon.m_dPlayerDeathDate.AddSeconds((double)this._NetLiving.playerCommon.m_nDeathTimeLong);
				this.fDjsMaxTime = (float)((int)(d - Fun.Now).TotalSeconds);
				this.fDjsNowTime = 0f;
			}
		}
		bool flag4 = !this.daojishi.activeSelf;
		if (flag4)
		{
			this.daojishi.SetActive(true);
		}
		this.fDjsNowTime += Time.deltaTime;
		float num = this.fDjsMaxTime - this.fDjsNowTime;
		bool flag5 = num < 0f;
		if (flag5)
		{
			this.Close();
		}
		else
		{
			this.imgjdt.fillAmount = this.fDjsNowTime / this.fDjsMaxTime;
			int num2 = Mathf.CeilToInt(num);
			this.Texdjs.text = num2.ToString();
			int num3 = (int)this.fDjsMaxTime - num2;
			bool flag6 = MatchManager.GetInstance().m_pFbData.nType == 11 && num3 > 2;
			if (flag6)
			{
				bool flag7 = !this.oGrsdUI.activeSelf;
				if (flag7)
				{
					this.oGrsdUI.SetActive(true);
				}
				this.oGrsdUIBg.sprite = Window.spriteName(AtlasType.ATLAS_GAME, "fuhuo");
				this.oGrsdUImsg.text = Language.GetLang(3033, new object[0]);
			}
			else
			{
				bool activeSelf2 = this.oGrsdUI.activeSelf;
				if (activeSelf2)
				{
					this.oGrsdUI.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000A3B04 File Offset: 0x000A1D04
	public void OnShowBsTime(string stime)
	{
		bool activeSelf = this._DeadWin.activeSelf;
		if (activeSelf)
		{
			this.Textbssjlab.text = Language.GetLang(3004, new object[]
			{
				stime
			});
		}
	}

	// Token: 0x04000DBD RID: 3517
	public GameObject _DeadWin;

	// Token: 0x04000DBE RID: 3518
	private GameObject _DownPic;

	// Token: 0x04000DBF RID: 3519
	private Text xmzname;

	// Token: 0x04000DC0 RID: 3520
	private Text sxyxlab;

	// Token: 0x04000DC1 RID: 3521
	private GameObject daojishi;

	// Token: 0x04000DC2 RID: 3522
	private Image imgjdt;

	// Token: 0x04000DC3 RID: 3523
	private Text Texdjs;

	// Token: 0x04000DC4 RID: 3524
	private Text Textbssjlab;

	// Token: 0x04000DC5 RID: 3525
	private Text TextQhlab;

	// Token: 0x04000DC6 RID: 3526
	private GameObject oGrsdUI;

	// Token: 0x04000DC7 RID: 3527
	private Image oGrsdUIBg;

	// Token: 0x04000DC8 RID: 3528
	private Text oGrsdUImsg;

	// Token: 0x04000DC9 RID: 3529
	private Text zwjtTxt;

	// Token: 0x04000DCA RID: 3530
	private float fDjsMaxTime;

	// Token: 0x04000DCB RID: 3531
	private float fDjsNowTime;

	// Token: 0x04000DCC RID: 3532
	public bool mOpenBool;

	// Token: 0x04000DCD RID: 3533
	private NetLiving _NetLiving;
}

using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000203 RID: 515
public class JieRiActUICon : Window
{
	// Token: 0x06001668 RID: 5736 RVA: 0x000CFCF0 File Offset: 0x000CDEF0
	protected override void Awake()
	{
		base.Awake();
		this.LianXuUI_Toggle = this.UIGameObj.Get_GameObj(0);
		this.LianXuUI = this.UIGameObj.Get_GameObj(1);
		this.LianXuUI_Toggle.SetActive(false);
		this.matchRewardToggle = this.UIGameObj.Get_GameObj(2);
		this.matchRewardUI = this.UIGameObj.Get_GameObj(3);
		this.matchRewardToggle.SetActive(false);
		this.LianXuUI.AddComponent<NaLianXuUICon>();
		this.matchRewardUI.AddComponent<NaMatchRewardUICon>();
		this.LianXuUI_Toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 1, this.LianXuUI);
		});
		this.matchRewardToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 2, this.matchRewardUI);
		});
		this.LianXuUI.SetActive(false);
		this.matchRewardUI.SetActive(false);
		this.doubleExpTg = this.UIGameObj.Get_GameObj(4).GetComponent<Toggle>();
		this.doubleExpTip = this.doubleExpTg.transform.Find("Image").GetComponent<Image>();
		this.doubleExpTxt = this.doubleExpTg.transform.Find("Label").GetComponent<Text>();
		this.doubleExpUI = this.UIGameObj.Get_GameObj(5);
		this.doubleExpUI.AddComponent<DoubleExpUICon>();
		this.doubleExpTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 3, this.doubleExpUI);
		});
		this.doubleExpTg.gameObject.SetActive(false);
		this.doubleExpUI.SetActive(false);
		this.rechargeZuanTg = this.UIGameObj.Get_GameObj(6).GetComponent<Toggle>();
		this.rechargeZuanTip = this.rechargeZuanTg.transform.Find("Image").GetComponent<Image>();
		this.rechargeZuanTxt = this.rechargeZuanTg.transform.Find("Label").GetComponent<Text>();
		this.rechargeZuanUI = this.UIGameObj.Get_GameObj(7);
		this.rechargeZuanUI.AddComponent<RechargeZuanUICon>();
		this.rechargeZuanTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 4, this.rechargeZuanUI);
		});
		this.rechargeZuanTg.gameObject.SetActive(false);
		this.rechargeZuanUI.SetActive(false);
		this.skinDiscountTg = this.UIGameObj.Get_GameObj(8).GetComponent<Toggle>();
		this.skinDiscountTip = this.skinDiscountTg.transform.Find("Image").GetComponent<Image>();
		this.skinDiscountTxt = this.skinDiscountTg.transform.Find("Label").GetComponent<Text>();
		this.skinDiscountUI = this.UIGameObj.Get_GameObj(9);
		this.skinDiscountUI.AddComponent<SkinDiscountUICon>();
		this.skinDiscountTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 5, this.skinDiscountUI);
		});
		this.skinDiscountTg.gameObject.SetActive(false);
		this.skinDiscountUI.SetActive(false);
		this.jieriOnlineTg = this.UIGameObj.Get_GameObj(10).GetComponent<Toggle>();
		this.jieriOnlineTip = this.jieriOnlineTg.transform.Find("Image").GetComponent<Image>();
		this.jieriOnlineTxt = this.jieriOnlineTg.transform.Find("Label").GetComponent<Text>();
		this.jieriOnlineUI = this.UIGameObj.Get_GameObj(11);
		this.jieriOnlineUI.AddComponent<JieRiOnlineUICon>();
		this.jieriOnlineTg.onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 6, this.jieriOnlineUI);
		});
		this.jieriOnlineTg.gameObject.SetActive(false);
		this.jieriOnlineUI.SetActive(false);
		JieRiActUICon.inst = this;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000D00AA File Offset: 0x000CE2AA
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000D00B4 File Offset: 0x000CE2B4
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.IsShowRechargeZuanTg();
		this.IsShowNaLianXueTg();
		this.IsShowNaMatchTg();
		this.IsShowDoubleExpTg();
		this.IsShowSkinDiscountTg();
		this.IsShowJieRiOnlineTg();
		bool activeSelf = this.jieriOnlineTg.gameObject.activeSelf;
		if (activeSelf)
		{
			this.jieriOnlineTg.isOn = true;
		}
		else
		{
			bool activeSelf2 = this.rechargeZuanTg.gameObject.activeSelf;
			if (activeSelf2)
			{
				this.rechargeZuanTg.isOn = true;
			}
			else
			{
				bool activeSelf3 = this.LianXuUI_Toggle.activeSelf;
				if (activeSelf3)
				{
					this.LianXuUI_Toggle.GetComponent<Toggle>().isOn = true;
				}
				else
				{
					bool activeSelf4 = this.matchRewardToggle.activeSelf;
					if (activeSelf4)
					{
						this.matchRewardToggle.GetComponent<Toggle>().isOn = true;
					}
					else
					{
						bool activeSelf5 = this.doubleExpTg.gameObject.activeSelf;
						if (activeSelf5)
						{
							this.doubleExpTg.isOn = true;
						}
						else
						{
							bool activeSelf6 = this.skinDiscountTg.gameObject.activeSelf;
							if (activeSelf6)
							{
								this.skinDiscountTg.isOn = true;
							}
						}
					}
				}
			}
		}
		this.ShowRechargeZuanImg();
		this.ShowNaLianXuWarnImg();
		this.ShowNaMatchRewardToggleImg();
		this.ShowDoubleExpToggleImg();
		this.ShowSkinDiscountImg();
		this.ShowJieRiOnlineImg();
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000D0224 File Offset: 0x000CE424
	private void BtnClick(bool v, int nType, GameObject go)
	{
		bool flag = this.OldSelectObj != null;
		if (flag)
		{
			this.OldSelectObj.SetActive(false);
		}
		go.SetActive(v);
		this.OldSelectObj = go;
		this.ShowUIData();
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000D0268 File Offset: 0x000CE468
	public void ShowUIData()
	{
		bool flag = this.LianXuUI.activeSelf && NaLianXuUICon.inst != null;
		if (flag)
		{
			NaLianXuUICon.inst.ShowUIData();
		}
		bool flag2 = this.matchRewardUI.activeSelf && NaMatchRewardUICon.inst != null;
		if (flag2)
		{
			NaMatchRewardUICon.inst.ShowUIData();
		}
		bool activeSelf = this.doubleExpUI.activeSelf;
		if (activeSelf)
		{
			LiBaoManager.GetInstance().SetDoubleExpClick(true);
		}
		bool activeSelf2 = this.skinDiscountUI.activeSelf;
		if (activeSelf2)
		{
			LiBaoManager.GetInstance().SetSkinDiscountClick(true);
		}
		bool activeSelf3 = this.jieriOnlineUI.activeSelf;
		if (activeSelf3)
		{
			LiBaoManager.GetInstance().SetJieRiOnlineClick(true);
		}
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000D0327 File Offset: 0x000CE527
	public void ShowRechargeZuanImg()
	{
		this.rechargeZuanTip.enabled = LiBaoManager.GetInstance().GetRechargeZuanState();
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000D0340 File Offset: 0x000CE540
	public void ShowNaMatchRewardToggleImg()
	{
		int naMatchReward = LiBaoManager.GetInstance().GetNaMatchReward();
		this.matchRewardToggle.transform.FindChild("Image").GetComponent<Image>().enabled = (naMatchReward == 1);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000D037D File Offset: 0x000CE57D
	public void ShowNaLianXuWarnImg()
	{
		this.LianXuUI_Toggle.transform.FindChild("Image").GetComponent<Image>().enabled = LiBaoManager.GetInstance().GetJieriTipState();
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000D03AA File Offset: 0x000CE5AA
	public void ShowDoubleExpToggleImg()
	{
		this.doubleExpTip.enabled = LiBaoManager.GetInstance().GetDoubleExpState();
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000D03C3 File Offset: 0x000CE5C3
	public void ShowSkinDiscountImg()
	{
		this.skinDiscountTip.enabled = LiBaoManager.GetInstance().GetSkinDiscountState();
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x000D03DC File Offset: 0x000CE5DC
	public void ShowJieRiOnlineImg()
	{
		this.jieriOnlineTip.enabled = LiBaoManager.GetInstance().IsShowJieRiOnlineEff();
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000D03F8 File Offset: 0x000CE5F8
	private bool IsShowRechargeZuanTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(31);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			RechargeZuanData rechargeZuanData = fGameManager.mloadConfig.AllRechargeZuan[actDataByType.nXmlId];
			this.rechargeZuanTxt.text = rechargeZuanData.sBtnTxt;
		}
		this.rechargeZuanTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000D0468 File Offset: 0x000CE668
	private bool IsShowNaLianXueTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(32);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			NaLianXuXmlDatas naLianXuXmlDatas = fGameManager.mloadConfig.AllNaLianXuData[actDataByType.nXmlId];
			this.LianXuUI_Toggle.transform.Find("Label").GetComponent<Text>().text = naLianXuXmlDatas.name;
		}
		this.LianXuUI_Toggle.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x000D04EC File Offset: 0x000CE6EC
	public bool IsShowNaMatchTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(33);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			MatchNaRewardActData matchNaRewardActData = fGameManager.mloadConfig.AllNaMatchRewardAct[actDataByType.nXmlId];
			this.matchRewardToggle.transform.Find("Label").GetComponent<Text>().text = matchNaRewardActData.sActName;
		}
		this.matchRewardToggle.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x000D056C File Offset: 0x000CE76C
	private bool IsShowDoubleExpTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(12);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataByType.nXmlId];
			this.doubleExpTxt.text = actDaTingAward.name;
		}
		this.doubleExpTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x000D05DC File Offset: 0x000CE7DC
	private bool IsShowSkinDiscountTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(47);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			HeroDiscountData heroDiscountData = fGameManager.mloadConfig.AllHeroDiscountData[actDataByType.nXmlId];
			this.skinDiscountTxt.text = heroDiscountData.sActName;
		}
		this.skinDiscountTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000D0648 File Offset: 0x000CE848
	private bool IsShowJieRiOnlineTg()
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(48);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			flag = true;
			JieRiOnline jieRiOnline = fGameManager.mloadConfig.AllJieRiOnline[actDataByType.nXmlId];
			this.jieriOnlineTxt.text = jieRiOnline.sActName;
		}
		this.jieriOnlineTg.gameObject.SetActive(flag);
		return flag;
	}

	// Token: 0x04001170 RID: 4464
	public static JieRiActUICon inst;

	// Token: 0x04001171 RID: 4465
	private GameObject LianXuUI;

	// Token: 0x04001172 RID: 4466
	private GameObject matchRewardUI;

	// Token: 0x04001173 RID: 4467
	private GameObject LianXuUI_Toggle;

	// Token: 0x04001174 RID: 4468
	private GameObject matchRewardToggle;

	// Token: 0x04001175 RID: 4469
	private GameObject OldSelectObj;

	// Token: 0x04001176 RID: 4470
	private GameObject doubleExpUI;

	// Token: 0x04001177 RID: 4471
	private Toggle doubleExpTg;

	// Token: 0x04001178 RID: 4472
	private Image doubleExpTip;

	// Token: 0x04001179 RID: 4473
	private Text doubleExpTxt;

	// Token: 0x0400117A RID: 4474
	private GameObject rechargeZuanUI;

	// Token: 0x0400117B RID: 4475
	private Toggle rechargeZuanTg;

	// Token: 0x0400117C RID: 4476
	private Image rechargeZuanTip;

	// Token: 0x0400117D RID: 4477
	private Text rechargeZuanTxt;

	// Token: 0x0400117E RID: 4478
	private GameObject skinDiscountUI;

	// Token: 0x0400117F RID: 4479
	private Toggle skinDiscountTg;

	// Token: 0x04001180 RID: 4480
	private Image skinDiscountTip;

	// Token: 0x04001181 RID: 4481
	private Text skinDiscountTxt;

	// Token: 0x04001182 RID: 4482
	private GameObject jieriOnlineUI;

	// Token: 0x04001183 RID: 4483
	private Toggle jieriOnlineTg;

	// Token: 0x04001184 RID: 4484
	private Image jieriOnlineTip;

	// Token: 0x04001185 RID: 4485
	private Text jieriOnlineTxt;
}

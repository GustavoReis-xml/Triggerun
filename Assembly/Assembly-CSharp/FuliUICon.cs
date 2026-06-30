using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class FuliUICon : Window
{
	// Token: 0x06000C16 RID: 3094 RVA: 0x000725D8 File Offset: 0x000707D8
	protected override void Awake()
	{
		base.Awake();
		FuliUICon.inst = this;
		this.SignToggle = this.UIGameObj.Get_GameObj(8);
		this.SignUI = this.UIGameObj.Get_GameObj(9);
		this.SignUI.AddComponent<SignRewardUICon>();
		this.DayOnLineToggle = this.UIGameObj.Get_GameObj(12);
		this.DayOnLineUI = this.UIGameObj.Get_GameObj(13);
		this.DayOnLineUI.AddComponent<DayOnlineRewardUICon>();
		this.DayOnTimeToggle = this.UIGameObj.Get_GameObj(14);
		this.DayOnTimeUI = this.UIGameObj.Get_GameObj(15);
		this.DayOnTimeUI.AddComponent<DayOnTimeRewardUICon>();
		this.SkyActToggle = this.UIGameObj.Get_GameObj(16);
		this.SkyActUI = this.UIGameObj.Get_GameObj(17);
		this.SkyActUI.AddComponent<SkyActRewardUICon>();
		this.weekLoginToggle = this.UIGameObj.Get_GameObj(18);
		this.weekLoginUI = this.UIGameObj.Get_GameObj(19);
		this.weekLoginUI.AddComponent<WeekLoginUICon>();
		this.skyEveryDayToggle = this.UIGameObj.Get_GameObj(20);
		this.skyEveryDayUI = this.UIGameObj.Get_GameObj(21);
		this.skyEveryDayUI.AddComponent<SkyEveryDayUICon>();
		this.pvpEveryWeekToggle = this.UIGameObj.Get_GameObj(40);
		this.pvpEveryWeekUI = this.UIGameObj.Get_GameObj(41);
		this.pvpEveryWeekUI.AddComponent<PvpEveryWeekUICon>();
		this.SignToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 4, this.SignUI);
		});
		this.DayOnLineToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 6, this.DayOnLineUI);
		});
		this.DayOnTimeToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 7, this.DayOnTimeUI);
		});
		this.SkyActToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 8, this.SkyActUI);
		});
		this.weekLoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 9, this.weekLoginUI);
		});
		this.skyEveryDayToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 10, this.skyEveryDayUI);
		});
		this.pvpEveryWeekToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 20, this.pvpEveryWeekUI);
		});
		this.SignToggle.transform.FindChild("Label").GetComponent<Text>().text = Language.GetLang(42014, new object[0]);
		this.OldSelectObj = this.SignUI;
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0007287A File Offset: 0x00070A7A
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00072884 File Offset: 0x00070A84
	public override void Reset()
	{
		base.Reset();
		this.SignToggle.SetActive(LiBaoManager.GetInstance().CanShowSignUI());
		this.DayOnLineToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(17));
		this.DayOnTimeToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(18));
		this.SkyActToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(19));
		this.weekLoginToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(20));
		this.skyEveryDayToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(21));
		this.pvpEveryWeekToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(44));
		bool activeSelf = this.SignToggle.activeSelf;
		if (activeSelf)
		{
			this.SignToggle.GetComponent<Toggle>().isOn = true;
		}
		else
		{
			bool activeSelf2 = this.DayOnLineToggle.activeSelf;
			if (activeSelf2)
			{
				this.DayOnLineToggle.GetComponent<Toggle>().isOn = true;
			}
			else
			{
				bool activeSelf3 = this.DayOnTimeToggle.activeSelf;
				if (activeSelf3)
				{
					this.DayOnTimeToggle.GetComponent<Toggle>().isOn = true;
				}
				else
				{
					bool activeSelf4 = this.SkyActToggle.activeSelf;
					if (activeSelf4)
					{
						this.SkyActToggle.GetComponent<Toggle>().isOn = true;
					}
					else
					{
						bool activeSelf5 = this.weekLoginToggle.activeSelf;
						if (activeSelf5)
						{
							this.weekLoginToggle.GetComponent<Toggle>().isOn = true;
						}
						else
						{
							bool activeSelf6 = this.skyEveryDayToggle.activeSelf;
							if (activeSelf6)
							{
								this.skyEveryDayToggle.GetComponent<Toggle>().isOn = true;
							}
							else
							{
								bool activeSelf7 = this.pvpEveryWeekToggle.activeSelf;
								if (activeSelf7)
								{
									this.pvpEveryWeekToggle.GetComponent<Toggle>().isOn = true;
								}
							}
						}
					}
				}
			}
		}
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.ShowSignImg();
		this.ShowOnLine_ToggleImg();
		this.ShowOnTime_ToggleImg();
		this.ShowSkyAct_ToggleImg();
		this.ShowWeekLoginToggleImg();
		this.ShowSkyEveryDayToggleImg();
		this.ShowPvpEveryWeekToggleImg();
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00072AA4 File Offset: 0x00070CA4
	private void BtnClick(bool v, int nType, GameObject go)
	{
		bool flag = this.OldSelectObj != go && v;
		if (flag)
		{
			this.OldSelectObj.SetActive(false);
			go.SetActive(true);
			this.OldSelectObj = go;
		}
		this.ShowUIData();
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00072AEC File Offset: 0x00070CEC
	public void ShowUIData()
	{
		bool flag = this.SignUI.activeSelf && SignRewardUICon.inst != null;
		if (flag)
		{
			SignRewardUICon.inst.ShowUIData();
		}
		bool flag2 = this.DayOnLineUI.activeSelf && DayOnlineRewardUICon.inst != null;
		if (flag2)
		{
			DayOnlineRewardUICon.inst.ShowUIData();
		}
		bool flag3 = this.DayOnTimeUI.activeSelf && DayOnTimeRewardUICon.inst != null;
		if (flag3)
		{
			DayOnTimeRewardUICon.inst.ShowUIData();
		}
		bool flag4 = this.SkyActUI.activeSelf && SkyActRewardUICon.inst != null;
		if (flag4)
		{
			SkyActRewardUICon.inst.ShowUIData();
		}
		bool activeSelf = this.pvpEveryWeekUI.activeSelf;
		if (activeSelf)
		{
			LiBaoManager.GetInstance().SetPvpEveryWeekClick(true);
		}
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00072BC9 File Offset: 0x00070DC9
	public void ShowSignImg()
	{
		this.SignToggle.transform.FindChild("Image").gameObject.SetActive(LiBaoManager.GetInstance().GetSignState());
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00072BF8 File Offset: 0x00070DF8
	public void ShowOnLine_ToggleImg()
	{
		int num = LiBaoManager.GetInstance().OnLine_ToggleState();
		bool active = false;
		bool flag = num > 0;
		if (flag)
		{
			active = true;
		}
		this.DayOnLineToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00072C40 File Offset: 0x00070E40
	public void ShowOnTime_ToggleImg()
	{
		int num = LiBaoManager.GetInstance().OnTime_ToggleState();
		bool active = false;
		bool flag = num > 0;
		if (flag)
		{
			active = true;
		}
		this.DayOnTimeToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00072C88 File Offset: 0x00070E88
	public void ShowSkyAct_ToggleImg()
	{
		int num = LiBaoManager.GetInstance().SkyAct_ToggleState();
		bool active = false;
		bool flag = num > 0;
		if (flag)
		{
			active = true;
		}
		this.SkyActToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00072CD0 File Offset: 0x00070ED0
	public void ShowWeekLoginToggleImg()
	{
		int weekLoginReward = LiBaoManager.GetInstance().GetWeekLoginReward();
		this.weekLoginToggle.transform.FindChild("Image").gameObject.SetActive(weekLoginReward == 0);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00072D10 File Offset: 0x00070F10
	public void ShowSkyEveryDayToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowSkyEveryDayEff();
		this.skyEveryDayToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00072D4C File Offset: 0x00070F4C
	public void ShowPvpEveryWeekToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowPvpEveryWeekEff();
		this.pvpEveryWeekToggle.transform.FindChild("Image").gameObject.SetActive(active);
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(44);
		bool flag = actDataByType != null;
		if (flag)
		{
			PvpWeekReward pvpWeekReward = fGameManager.mloadConfig.AllPvpWeekReward[actDataByType.nXmlId];
			this.pvpEveryWeekToggle.transform.Find("Label").GetComponent<Text>().text = pvpWeekReward.sActName;
		}
	}

	// Token: 0x04000A40 RID: 2624
	public static FuliUICon inst;

	// Token: 0x04000A41 RID: 2625
	private GameObject OldSelectObj;

	// Token: 0x04000A42 RID: 2626
	private GameObject SignUI;

	// Token: 0x04000A43 RID: 2627
	private GameObject DayOnLineUI;

	// Token: 0x04000A44 RID: 2628
	private GameObject DayOnTimeUI;

	// Token: 0x04000A45 RID: 2629
	private GameObject SkyActUI;

	// Token: 0x04000A46 RID: 2630
	private GameObject weekLoginUI;

	// Token: 0x04000A47 RID: 2631
	private GameObject skyEveryDayUI;

	// Token: 0x04000A48 RID: 2632
	private GameObject pvpEveryWeekUI;

	// Token: 0x04000A49 RID: 2633
	private GameObject SignToggle;

	// Token: 0x04000A4A RID: 2634
	private GameObject DayOnLineToggle;

	// Token: 0x04000A4B RID: 2635
	private GameObject DayOnTimeToggle;

	// Token: 0x04000A4C RID: 2636
	private GameObject SkyActToggle;

	// Token: 0x04000A4D RID: 2637
	private GameObject weekLoginToggle;

	// Token: 0x04000A4E RID: 2638
	private GameObject skyEveryDayToggle;

	// Token: 0x04000A4F RID: 2639
	private GameObject pvpEveryWeekToggle;
}

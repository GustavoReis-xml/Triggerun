using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021A RID: 538
public class LiBaoUICon : Window
{
	// Token: 0x060017D5 RID: 6101 RVA: 0x000DD680 File Offset: 0x000DB880
	protected override void Awake()
	{
		base.Awake();
		LiBaoUICon.inst = this;
		this.ShouChongUI_Toggle = this.UIGameObj.Get_GameObj(0);
		this.LianXuUI_Toggle = this.UIGameObj.Get_GameObj(1);
		this.Lv30RewardToggle = this.UIGameObj.Get_GameObj(4);
		this.LvRewardToggle = this.UIGameObj.Get_GameObj(6);
		this.CDKToggle = this.UIGameObj.Get_GameObj(10);
		this.ShouChongUI = this.UIGameObj.Get_GameObj(2);
		this.ShouChongUI.AddComponent<ShouChongUICon>();
		this.LianXuUI = this.UIGameObj.Get_GameObj(3);
		this.LianXuUI.AddComponent<LianXuUICon>();
		this.Lv30RewardUI = this.UIGameObj.Get_GameObj(5);
		this.Lv30RewardUI.AddComponent<LvRewardUICon>();
		this.LvRewardUI = this.UIGameObj.Get_GameObj(7);
		this.LvRewardUI.AddComponent<UserLvRewardUICon>();
		this.CDKUI = this.UIGameObj.Get_GameObj(11);
		this.CDKUI.AddComponent<CDKRewardUICon>();
		this.newSoldierToggle = this.UIGameObj.Get_GameObj(22);
		this.newSoldierUI = this.UIGameObj.Get_GameObj(23);
		this.newSoldierUI.AddComponent<NewSoldierUICon>();
		this.weekendOnlineToggle = this.UIGameObj.Get_GameObj(24);
		this.weekendOnlineUI = this.UIGameObj.Get_GameObj(25);
		this.weekendOnlineUI.AddComponent<WeekendOnlineUICon>();
		this.kaixueGiftToggle = this.UIGameObj.Get_GameObj(26);
		this.kaixueGiftUI = this.UIGameObj.Get_GameObj(27);
		this.kaixueGiftUI.AddComponent<KaiXueUICon>();
		this.matchRewardToggle = this.UIGameObj.Get_GameObj(28);
		this.matchRewardUI = this.UIGameObj.Get_GameObj(29);
		this.matchRewardUI.AddComponent<MatchRewardUICon>();
		this.goddessGiftToggle = this.UIGameObj.Get_GameObj(30);
		this.goddessGiftUI = this.UIGameObj.Get_GameObj(31);
		this.goddessGiftUI.AddComponent<GoddessGiftUICon>();
		this.weekChargeToggle = this.UIGameObj.Get_GameObj(32);
		this.weekChargeUI = this.UIGameObj.Get_GameObj(33);
		this.weekChargeUI.AddComponent<WeekChargeUICon>();
		this.BOGOActToggle = this.UIGameObj.Get_GameObj(34);
		this.BOGOActUI = this.UIGameObj.Get_GameObj(35);
		this.BOGOActUI.AddComponent<BOGOActUICon>();
		this.tuijianToggle = this.UIGameObj.Get_GameObj(36);
		this.tuijianUI = this.UIGameObj.Get_GameObj(37);
		this.tuijianUI.AddComponent<TuijianUICon>();
		this.waiguaToggle = this.UIGameObj.Get_GameObj(38);
		this.waiguaUI = this.UIGameObj.Get_GameObj(39);
		this.waiguaUI.AddComponent<WaiGuaUICon>();
		this.multiBOGOToggle = this.UIGameObj.Get_GameObj(42);
		this.multiBOGOUI = this.UIGameObj.Get_GameObj(43);
		this.multiBOGOUI.AddComponent<MultiBOGOUICon>();
		this.ShouChongUI_Toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 0, this.ShouChongUI);
		});
		this.LianXuUI_Toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 1, this.LianXuUI);
		});
		this.Lv30RewardToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 2, this.Lv30RewardUI);
		});
		this.LvRewardToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 3, this.LvRewardUI);
		});
		this.CDKToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 5, this.CDKUI);
		});
		this.newSoldierToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 11, this.newSoldierUI);
		});
		this.weekendOnlineToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 12, this.weekendOnlineUI);
		});
		this.kaixueGiftToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 13, this.kaixueGiftUI);
		});
		this.matchRewardToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 14, this.matchRewardUI);
		});
		this.goddessGiftToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 15, this.goddessGiftUI);
		});
		this.weekChargeToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 16, this.weekChargeUI);
		});
		this.BOGOActToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 17, this.BOGOActUI);
		});
		this.tuijianToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 18, this.tuijianUI);
		});
		this.waiguaToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 19, this.waiguaUI);
		});
		this.multiBOGOToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool v)
		{
			this.BtnClick(v, 21, this.multiBOGOUI);
		});
		this.LianXuUI_Toggle.transform.FindChild("Label").GetComponent<Text>().text = Language.GetLang(42000, new object[0]);
		this.ShouChongUI_Toggle.transform.FindChild("Label").GetComponent<Text>().text = Language.GetLang(42001, new object[0]);
		this.Lv30RewardToggle.transform.FindChild("Label").GetComponent<Text>().text = Language.GetLang(42010, new object[0]);
		this.LvRewardToggle.transform.FindChild("Label").GetComponent<Text>().text = Language.GetLang(42012, new object[0]);
		this.OldSelectObj = this.ShouChongUI;
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000DDC4B File Offset: 0x000DBE4B
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000DDC58 File Offset: 0x000DBE58
	public override void Reset()
	{
		base.Reset();
		int userRewardState = LiBaoManager.GetInstance().GetUserRewardState();
		this.LianXuUI_Toggle.SetActive(LiBaoManager.GetInstance().CanShowLianXuUI());
		this.ShouChongUI_Toggle.SetActive(LiBaoManager.GetInstance().CanShowShouChongUI());
		this.Lv30RewardToggle.SetActive(LiBaoManager.GetInstance().CanShowLvRewardUI());
		this.CDKToggle.SetActive(LiBaoManager.GetInstance().GetCDK_Act());
		this.newSoldierToggle.SetActive(LiBaoManager.GetInstance().GetNewSoldierAct());
		this.weekendOnlineToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(23));
		this.kaixueGiftToggle.SetActive(LiBaoManager.GetInstance().isShowKaiXueGift());
		this.matchRewardToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(25));
		this.goddessGiftToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(26));
		this.weekChargeToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(27));
		this.BOGOActToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(29));
		this.tuijianToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(40));
		this.waiguaToggle.SetActive(LiBaoManager.isWaiGuaOpen);
		this.multiBOGOToggle.SetActive(LiBaoManager.GetInstance().CanShowActUI(45));
		this.LvRewardToggle.SetActive(userRewardState > 0);
		this.LvRewardToggle.transform.FindChild("Image").gameObject.SetActive(LiBaoManager.GetInstance().Get_UserLvRewardNum() == 1);
		bool activeSelf = this.ShouChongUI_Toggle.activeSelf;
		if (activeSelf)
		{
			this.ShouChongUI_Toggle.GetComponent<Toggle>().isOn = true;
		}
		else
		{
			bool activeSelf2 = this.tuijianToggle.activeSelf;
			if (activeSelf2)
			{
				this.tuijianToggle.GetComponent<Toggle>().isOn = true;
				this.BtnClick(true, 18, this.tuijianUI);
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
					bool activeSelf4 = this.Lv30RewardToggle.activeSelf;
					if (activeSelf4)
					{
						this.Lv30RewardToggle.GetComponent<Toggle>().isOn = true;
					}
					else
					{
						bool activeSelf5 = this.LvRewardToggle.activeSelf;
						if (activeSelf5)
						{
							this.LvRewardToggle.GetComponent<Toggle>().isOn = true;
						}
						else
						{
							bool activeSelf6 = this.CDKToggle.activeSelf;
							if (activeSelf6)
							{
								this.CDKToggle.GetComponent<Toggle>().isOn = true;
							}
							else
							{
								bool activeSelf7 = this.newSoldierToggle.activeSelf;
								if (activeSelf7)
								{
									this.newSoldierToggle.GetComponent<Toggle>().isOn = true;
								}
								else
								{
									bool activeSelf8 = this.weekendOnlineToggle.activeSelf;
									if (activeSelf8)
									{
										this.weekendOnlineToggle.GetComponent<Toggle>().isOn = true;
									}
									else
									{
										bool activeSelf9 = this.kaixueGiftToggle.activeSelf;
										if (activeSelf9)
										{
											this.kaixueGiftToggle.GetComponent<Toggle>().isOn = true;
										}
										else
										{
											bool activeSelf10 = this.matchRewardToggle.activeSelf;
											if (activeSelf10)
											{
												this.matchRewardToggle.GetComponent<Toggle>().isOn = true;
											}
											else
											{
												bool activeSelf11 = this.goddessGiftToggle.activeSelf;
												if (activeSelf11)
												{
													this.goddessGiftToggle.GetComponent<Toggle>().isOn = true;
												}
												else
												{
													bool activeSelf12 = this.weekChargeToggle.activeSelf;
													if (activeSelf12)
													{
														this.weekChargeToggle.GetComponent<Toggle>().isOn = true;
													}
													else
													{
														bool activeSelf13 = this.BOGOActToggle.activeSelf;
														if (activeSelf13)
														{
															this.BOGOActToggle.GetComponent<Toggle>().isOn = true;
														}
														else
														{
															bool activeSelf14 = this.waiguaToggle.activeSelf;
															if (activeSelf14)
															{
																this.waiguaToggle.GetComponent<Toggle>().isOn = true;
															}
															else
															{
																bool activeSelf15 = this.multiBOGOToggle.activeSelf;
																if (activeSelf15)
																{
																	this.multiBOGOToggle.GetComponent<Toggle>().isOn = true;
																}
															}
														}
													}
												}
											}
										}
									}
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
		this.ShowUserLvImg();
		this.ShowShouChongImg();
		this.ShowLv30RewardToggleImg();
		this.ShowLianXuUI_ToggleImg();
		this.ShowNewSoldierToggleImg();
		this.ShowWeekendOnlineImg();
		this.ShowKaiXueGiftToggleImg();
		this.ShowMatchRewardToggleImg();
		this.ShowGoddessGiftToggleImg();
		this.ShowWeekChargeToggleImg();
		this.ShowBOGOActToggleImg();
		this.ShowTuijianToggleImg();
		this.ShowWaiGuaToggleImg();
		this.ShowMultiBOGOToggleImg();
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000DE0E8 File Offset: 0x000DC2E8
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

	// Token: 0x060017D9 RID: 6105 RVA: 0x000DE130 File Offset: 0x000DC330
	public void ShowUIData()
	{
		bool flag = this.LianXuUI.activeSelf && LianXuUICon.inst != null;
		if (flag)
		{
			LianXuUICon.inst.ShowUIData();
		}
		bool flag2 = this.ShouChongUI.activeSelf && ShouChongUICon.inst != null;
		if (flag2)
		{
			ShouChongUICon.inst.ShowUIData();
		}
		bool flag3 = this.Lv30RewardUI.activeSelf && LvRewardUICon.inst != null;
		if (flag3)
		{
			LvRewardUICon.inst.ShowUIData();
		}
		bool flag4 = this.LvRewardUI.activeSelf && UserLvRewardUICon.inst != null;
		if (flag4)
		{
			UserLvRewardUICon.inst.ShowUIData();
		}
		bool flag5 = this.CDKUI.activeSelf && CDKRewardUICon.inst != null;
		if (flag5)
		{
			CDKRewardUICon.inst.ShowUIData();
		}
		bool activeSelf = this.goddessGiftUI.activeSelf;
		if (activeSelf)
		{
			LiBaoManager.GetInstance().SetGoddessGiftClick(true);
		}
		bool activeSelf2 = this.weekChargeUI.activeSelf;
		if (activeSelf2)
		{
			LiBaoManager.GetInstance().SetWeekChargeClick(true);
		}
		bool activeSelf3 = this.tuijianUI.activeSelf;
		if (activeSelf3)
		{
			LiBaoManager.GetInstance().SetTuijianClick(true);
		}
		bool activeSelf4 = this.waiguaUI.activeSelf;
		if (activeSelf4)
		{
			LiBaoManager.GetInstance().SetWaiGuaClick(true);
		}
		bool activeSelf5 = this.multiBOGOUI.activeSelf;
		if (activeSelf5)
		{
			LiBaoManager.GetInstance().SetMultiBOGOClick(true);
		}
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x000DE2B8 File Offset: 0x000DC4B8
	public void ShowUserLvImg()
	{
		bool flag = UserLvRewardUICon.inst != null;
		if (flag)
		{
			this.LvRewardToggle.transform.FindChild("Image").gameObject.SetActive(UserLvRewardUICon.inst.nGetNum > 0);
		}
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x000DE304 File Offset: 0x000DC504
	public void ShowShouChongImg()
	{
		this.ShouChongUI_Toggle.transform.FindChild("Image").gameObject.SetActive(LiBaoManager.GetInstance().GetShouChongState());
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000DE331 File Offset: 0x000DC531
	public void ShowLv30RewardToggleImg()
	{
		this.Lv30RewardToggle.transform.FindChild("Image").gameObject.SetActive(LiBaoManager.GetInstance().Lv30RewardState());
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x000DE35E File Offset: 0x000DC55E
	public void ShowLianXuUI_ToggleImg()
	{
		this.LianXuUI_Toggle.transform.FindChild("Image").gameObject.SetActive(LiBaoManager.GetInstance().LianXuUI_ToggleState());
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x000DE38C File Offset: 0x000DC58C
	public void ShowNewSoldierToggleImg()
	{
		int newSoldierReward = LiBaoManager.GetInstance().GetNewSoldierReward();
		this.newSoldierToggle.transform.FindChild("Image").gameObject.SetActive(newSoldierReward == 1);
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x000DE3CC File Offset: 0x000DC5CC
	public void ShowWeekendOnlineImg()
	{
		int weekendOnlineReward = LiBaoManager.GetInstance().GetWeekendOnlineReward();
		this.weekendOnlineToggle.transform.FindChild("Image").gameObject.SetActive(weekendOnlineReward == 1);
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000DE40C File Offset: 0x000DC60C
	public void ShowKaiXueGiftToggleImg()
	{
		int kaiXueGiftReward = LiBaoManager.GetInstance().GetKaiXueGiftReward();
		this.kaixueGiftToggle.transform.FindChild("Image").gameObject.SetActive(kaiXueGiftReward == 1);
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000DE44C File Offset: 0x000DC64C
	public void ShowMatchRewardToggleImg()
	{
		int matchReward = LiBaoManager.GetInstance().GetMatchReward();
		this.matchRewardToggle.transform.FindChild("Image").gameObject.SetActive(matchReward == 1);
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000DE48C File Offset: 0x000DC68C
	public void ShowGoddessGiftToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowGoddessGiftEff();
		this.goddessGiftToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x000DE4C8 File Offset: 0x000DC6C8
	public void ShowWeekChargeToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowWeekChargeEff();
		this.weekChargeToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000DE504 File Offset: 0x000DC704
	public void ShowBOGOActToggleImg()
	{
		int bogoactReward = LiBaoManager.GetInstance().GetBOGOActReward();
		this.BOGOActToggle.transform.FindChild("Image").gameObject.SetActive(bogoactReward == 1);
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x000DE544 File Offset: 0x000DC744
	public void ShowTuijianToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowTuijianEff();
		this.tuijianToggle.transform.FindChild("Image").gameObject.SetActive(active);
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(40);
		bool flag = actDataByType != null;
		if (flag)
		{
			TuijianData tuijianData = fGameManager.mloadConfig.AllTuijianData[actDataByType.nXmlId];
			this.tuijianToggle.transform.Find("Label").GetComponent<Text>().text = tuijianData.sActName;
		}
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x000DE5D4 File Offset: 0x000DC7D4
	public void ShowWaiGuaToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowWaiGuaEff();
		this.waiguaToggle.transform.FindChild("Image").gameObject.SetActive(active);
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000DE610 File Offset: 0x000DC810
	public void ShowMultiBOGOToggleImg()
	{
		bool active = LiBaoManager.GetInstance().IsShowMultiBOGOEff();
		this.multiBOGOToggle.transform.FindChild("Image").gameObject.SetActive(active);
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(45);
		bool flag = actDataByType != null;
		if (flag)
		{
			MultiBOGOData multiBOGOData = fGameManager.mloadConfig.AllMultiBOGOData[actDataByType.nXmlId];
			this.multiBOGOToggle.transform.Find("Label").GetComponent<Text>().text = multiBOGOData.sActName;
		}
	}

	// Token: 0x040012AF RID: 4783
	public static LiBaoUICon inst;

	// Token: 0x040012B0 RID: 4784
	private GameObject OldSelectObj;

	// Token: 0x040012B1 RID: 4785
	private GameObject ShouChongUI;

	// Token: 0x040012B2 RID: 4786
	private GameObject LianXuUI;

	// Token: 0x040012B3 RID: 4787
	private GameObject Lv30RewardUI;

	// Token: 0x040012B4 RID: 4788
	private GameObject LvRewardUI;

	// Token: 0x040012B5 RID: 4789
	private GameObject CDKUI;

	// Token: 0x040012B6 RID: 4790
	private GameObject newSoldierUI;

	// Token: 0x040012B7 RID: 4791
	private GameObject weekendOnlineUI;

	// Token: 0x040012B8 RID: 4792
	private GameObject kaixueGiftUI;

	// Token: 0x040012B9 RID: 4793
	private GameObject matchRewardUI;

	// Token: 0x040012BA RID: 4794
	private GameObject goddessGiftUI;

	// Token: 0x040012BB RID: 4795
	private GameObject weekChargeUI;

	// Token: 0x040012BC RID: 4796
	private GameObject BOGOActUI;

	// Token: 0x040012BD RID: 4797
	private GameObject tuijianUI;

	// Token: 0x040012BE RID: 4798
	private GameObject waiguaUI;

	// Token: 0x040012BF RID: 4799
	private GameObject multiBOGOUI;

	// Token: 0x040012C0 RID: 4800
	private GameObject ShouChongUI_Toggle;

	// Token: 0x040012C1 RID: 4801
	private GameObject LianXuUI_Toggle;

	// Token: 0x040012C2 RID: 4802
	private GameObject Lv30RewardToggle;

	// Token: 0x040012C3 RID: 4803
	private GameObject LvRewardToggle;

	// Token: 0x040012C4 RID: 4804
	private GameObject CDKToggle;

	// Token: 0x040012C5 RID: 4805
	private GameObject newSoldierToggle;

	// Token: 0x040012C6 RID: 4806
	private GameObject weekendOnlineToggle;

	// Token: 0x040012C7 RID: 4807
	private GameObject kaixueGiftToggle;

	// Token: 0x040012C8 RID: 4808
	private GameObject matchRewardToggle;

	// Token: 0x040012C9 RID: 4809
	private GameObject goddessGiftToggle;

	// Token: 0x040012CA RID: 4810
	private GameObject weekChargeToggle;

	// Token: 0x040012CB RID: 4811
	private GameObject BOGOActToggle;

	// Token: 0x040012CC RID: 4812
	private GameObject tuijianToggle;

	// Token: 0x040012CD RID: 4813
	private GameObject waiguaToggle;

	// Token: 0x040012CE RID: 4814
	private GameObject multiBOGOToggle;
}

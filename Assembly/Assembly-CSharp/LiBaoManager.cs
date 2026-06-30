using System;
using System.Collections;

// Token: 0x02000219 RID: 537
public class LiBaoManager
{
	// Token: 0x06001737 RID: 5943 RVA: 0x000D920C File Offset: 0x000D740C
	public static LiBaoManager GetInstance()
	{
		bool flag = LiBaoManager.instance == null;
		if (flag)
		{
			LiBaoManager.instance = new LiBaoManager();
		}
		LiBaoManager.isWaiGuaOpen = Fun.IsFunctionOpen("WaiGuaOpen", null);
		return LiBaoManager.instance;
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x000D924C File Offset: 0x000D744C
	public void LiBaoServerMsgToClient(int nNetId, ArrayList szMsg)
	{
		switch (nNetId)
		{
		case 15001:
			this.SetLiBaoData(szMsg);
			break;
		case 15002:
			this.UpDataShouChongData(szMsg);
			break;
		case 15003:
		case 15005:
		case 15006:
		case 15008:
		case 15010:
		case 15011:
		case 15014:
		case 15016:
		case 15018:
		case 15029:
		case 15037:
		case 15038:
			break;
		case 15004:
			this.UpDataLianXuData(szMsg);
			break;
		case 15007:
			this.UpDataLvRewardData(szMsg);
			break;
		case 15009:
			this.UpDataUserLvRewardData(szMsg);
			break;
		case 15012:
			this.UpDataSignData(szMsg);
			break;
		case 15013:
			this.UpDataAllSignData(szMsg);
			break;
		case 15015:
			this.UpDataOnLineRewardData(szMsg);
			break;
		case 15017:
			this.UpDataOnTimeRewardData(szMsg);
			break;
		case 15019:
			this.UpDataSkyActRewardData(szMsg);
			break;
		case 15020:
			this.SetWeekLoginReward(szMsg);
			break;
		case 15021:
			this.SetSkyEveryDayData(szMsg);
			break;
		case 15022:
			this.SetNewSoldierReward(szMsg);
			break;
		case 15023:
			this.SetWeekendOnlineReward(szMsg);
			break;
		case 15024:
			this.SetKaiXueGiftReward(szMsg);
			break;
		case 15025:
			this.SetMatchRewardActData(szMsg);
			break;
		case 15026:
			this.SetGoddessGiftReward(szMsg);
			break;
		case 15027:
			this.SetWeekChargeReward(szMsg);
			break;
		case 15028:
			this.SetLuckyDrawData(szMsg);
			break;
		case 15030:
			this.SetBOGOActData(szMsg);
			break;
		case 15031:
			this.SetRechargeZuanData(szMsg);
			break;
		case 15032:
			this.SetJieriLoginData(szMsg);
			break;
		case 15033:
			this.SetRechargeItemData(szMsg);
			break;
		case 15034:
			this.SetNaMatchRewardActData(szMsg);
			break;
		case 15035:
			this.SetDiscountItemData(szMsg);
			break;
		case 15036:
			this.SetHuiliuActData(szMsg);
			break;
		case 15039:
			this.ShowWishTreeTip(szMsg);
			break;
		case 15040:
			this.SetPvpEveryWeekRewardData(szMsg);
			break;
		case 15041:
			this.SetMultiBOGOData(szMsg);
			break;
		case 15042:
			this.SetWinterActData(szMsg);
			break;
		case 15043:
			this.SetJieRiOnlineData(szMsg);
			break;
		default:
			if (nNetId == 18002)
			{
				this.ShowCDK_Msg(szMsg);
			}
			break;
		}
		this.CheckLiBaoCanGet();
		this.CheckNationalActReward();
		this.CheckJieRiActReward();
		this.CheckFuliCanGet();
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x000D94BC File Offset: 0x000D76BC
	private void SetLiBaoData(ArrayList szMsg)
	{
		this.m_nChongZhiNum = (int)szMsg[0];
		this.m_szShouChong = (ArrayList)szMsg[1];
		this.m_nLianXuNum = (int)szMsg[2];
		this.m_szLianXu = (ArrayList)szMsg[3];
		this.m_szLvReward = (ArrayList)szMsg[5];
		this.m_szUserLvReward = (ArrayList)szMsg[6];
		this.m_szSignData = (ArrayList)szMsg[7];
		this.m_szSignReward = (ArrayList)szMsg[8];
		this.m_nSignAllNum = (int)szMsg[10];
		this.m_nCDK_Num = (int)szMsg[13];
		bool flag = szMsg[15].ToString() != string.Empty;
		if (flag)
		{
			this.m_dDayOnLoginDate = DateTime.Parse(szMsg[15].ToString());
		}
		this.m_nDayOnlineTime = (int)szMsg[16];
		this.m_szOnlineReward = (ArrayList)szMsg[17];
		this.m_szOnTimeReward = (ArrayList)szMsg[18];
		bool flag2 = szMsg[19].ToString() != string.Empty;
		if (flag2)
		{
			this.m_dSkyActDate = DateTime.Parse(szMsg[19].ToString());
		}
		this.m_nSkyActNum = (int)szMsg[20];
		this.m_szSkyActReward = (ArrayList)szMsg[21];
		this.m_szRepairSignData = (ArrayList)szMsg[42];
		this.m_szPvpEveryWeek = (ArrayList)szMsg[44];
		bool flag3 = LiBaoUICon.inst != null;
		if (flag3)
		{
			LiBaoUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000D968C File Offset: 0x000D788C
	public int Getm_nLianXuNum()
	{
		return this.m_nLianXuNum;
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000D96A4 File Offset: 0x000D78A4
	public int Get_LianXuState(int index)
	{
		bool flag = this.m_szLianXu == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = this.m_szLianXu.IndexOf(index);
		}
		return result;
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x000D96DC File Offset: 0x000D78DC
	public int Get_UserLvState(int index)
	{
		bool flag = this.m_szUserLvReward == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = this.m_szUserLvReward.IndexOf(index);
		}
		return result;
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000D9714 File Offset: 0x000D7914
	public ArrayList GetRepairSignData()
	{
		return this.m_szRepairSignData;
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000D972C File Offset: 0x000D792C
	private void UpDataLianXuData(ArrayList szMsg)
	{
		this.m_nLianXuNum = (int)szMsg[0];
		this.m_szLianXu = (ArrayList)szMsg[1];
		bool flag = LianXuUICon.inst != null;
		if (flag)
		{
			LianXuUICon.inst.UpDataAllReward();
			LiBaoUICon.inst.ShowLianXuUI_ToggleImg();
		}
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x000D9788 File Offset: 0x000D7988
	public int GetShouChongSatte(int nIndex)
	{
		ShouChongXmlData shouChongXmlData = fGameManager.mloadConfig.AllShouChongData[nIndex];
		bool flag = shouChongXmlData.moneynum > this.m_nChongZhiNum;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = this.m_szShouChong.IndexOf(nIndex) != -1;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000D97E4 File Offset: 0x000D79E4
	private void UpDataShouChongData(ArrayList szMsg)
	{
		this.m_nChongZhiNum = (int)szMsg[0];
		this.m_szShouChong = (ArrayList)szMsg[1];
		bool flag = ShouChongUICon.inst != null;
		if (flag)
		{
			ShouChongUICon.inst.ShowUIData();
		}
		bool flag2 = LiBaoUICon.inst != null;
		if (flag2)
		{
			LiBaoUICon.inst.ShowShouChongImg();
		}
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x000D9850 File Offset: 0x000D7A50
	private void UpDataLvRewardData(ArrayList szMsg)
	{
		this.m_szLvReward = (ArrayList)szMsg[1];
		bool flag = LvRewardUICon.inst != null;
		if (flag)
		{
			LvRewardUICon.inst.ShowUIData();
			LiBaoUICon.inst.ShowLv30RewardToggleImg();
		}
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x000D9898 File Offset: 0x000D7A98
	private void UpDataUserLvRewardData(ArrayList szMsg)
	{
		this.m_szUserLvReward = szMsg;
		bool flag = UserLvRewardUICon.inst != null;
		if (flag)
		{
			UserLvRewardUICon.inst.ShowUIData();
			LiBaoUICon.inst.ShowUserLvImg();
		}
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x000D98D4 File Offset: 0x000D7AD4
	public void CheckLiBaoCanGet()
	{
		bool huoDongBtnGuangXiao = false;
		bool shouChongState = this.GetShouChongState();
		bool flag = shouChongState;
		if (flag)
		{
			huoDongBtnGuangXiao = shouChongState;
		}
		else
		{
			bool flag2 = !this.ClickState;
			if (flag2)
			{
				huoDongBtnGuangXiao = true;
			}
		}
		bool flag3 = this.Lv30RewardState();
		if (flag3)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag4 = this.LianXuUI_ToggleState();
		if (flag4)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag5 = this.Get_UserLvRewardNum() == 1;
		if (flag5)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag6 = this.GetNewSoldierReward() == 1;
		if (flag6)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag7 = this.GetWeekendOnlineReward() == 1;
		if (flag7)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag8 = this.GetKaiXueGiftReward() == 1;
		if (flag8)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag9 = this.GetMatchReward() == 1;
		if (flag9)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag10 = this.IsShowGoddessGiftEff();
		if (flag10)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag11 = this.IsShowWeekChargeEff();
		if (flag11)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag12 = this.GetBOGOActReward() == 1;
		if (flag12)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag13 = this.IsShowTuijianEff();
		if (flag13)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag14 = this.IsShowWaiGuaEff();
		if (flag14)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag15 = this.IsShowMultiBOGOEff();
		if (flag15)
		{
			huoDongBtnGuangXiao = true;
		}
		bool flag16 = MainUICon.inst != null;
		if (flag16)
		{
			MainUICon.inst.SetHuoDongBtnGuangXiao(huoDongBtnGuangXiao);
			MainUICon.inst.SetHuoDongBtnActive(this.GetMainBntState());
		}
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x000D9A19 File Offset: 0x000D7C19
	public void ClickMainUIBnt()
	{
		this.ClickState = true;
		this.CheckLiBaoCanGet();
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000D9A2C File Offset: 0x000D7C2C
	private bool GetMainBntState()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.CanShowShouChongUI();
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.CanShowLianXuUI();
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = this.CanShowLvRewardUI();
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.CanShowUserLvRewardUI();
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = this.GetCDK_Act();
		}
		bool flag7 = !flag;
		if (flag7)
		{
			flag = this.GetNewSoldierAct();
		}
		bool flag8 = !flag;
		if (flag8)
		{
			flag = this.CanShowActUI(23);
		}
		bool flag9 = !flag;
		if (flag9)
		{
			flag = this.isShowKaiXueGift();
		}
		bool flag10 = !flag;
		if (flag10)
		{
			flag = this.CanShowActUI(25);
		}
		bool flag11 = !flag;
		if (flag11)
		{
			flag = this.CanShowActUI(26);
		}
		bool flag12 = !flag;
		if (flag12)
		{
			flag = this.CanShowActUI(27);
		}
		bool flag13 = !flag;
		if (flag13)
		{
			flag = this.CanShowActUI(29);
		}
		bool flag14 = !flag;
		if (flag14)
		{
			flag = this.CanShowActUI(40);
		}
		bool flag15 = !flag;
		if (flag15)
		{
			flag = LiBaoManager.isWaiGuaOpen;
		}
		bool flag16 = !flag;
		if (flag16)
		{
			flag = this.CanShowActUI(45);
		}
		return flag;
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000D9B68 File Offset: 0x000D7D68
	public bool CanShowShouChongUI()
	{
		bool flag = this.m_szShouChong != null && this.m_szShouChong.Count >= fGameManager.mloadConfig.AllShouChongData.Count;
		return !flag;
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000D9BB0 File Offset: 0x000D7DB0
	public bool CanShowLianXuUI()
	{
		bool flag = this.m_szLianXu != null && this.m_szLianXu.Count >= fGameManager.mloadConfig.AllLianXuData.Count;
		return !flag;
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000D9BF8 File Offset: 0x000D7DF8
	public bool CanShowLvRewardUI()
	{
		bool flag = this.m_szLvReward != null && this.m_szLvReward.Count >= fGameManager.mloadConfig.AllLvReward.Count;
		return !flag;
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000D9C40 File Offset: 0x000D7E40
	public bool CanShowUserLvRewardUI()
	{
		bool flag = this.GetUserRewardState() < 1;
		return !flag;
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x000D9C68 File Offset: 0x000D7E68
	public int GetLvRewardSatte(int nIndex)
	{
		LvRewardXmlData lvRewardXmlData = fGameManager.mloadConfig.AllLvReward[nIndex];
		bool flag = lvRewardXmlData.lv > PlayerDataManager.getInstance().HUserData.m_nLevel;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = this.m_szLvReward.IndexOf(nIndex) != -1;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000D9CCC File Offset: 0x000D7ECC
	public int GetUserRewardState()
	{
		bool flag = this.m_szUserLvReward == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(10);
			bool flag2 = openActIdByType == 0;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
				bool flag3 = actDataById == null;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataById.nXmlId];
					bool flag4 = this.m_szUserLvReward.Count < actDaTingAward.szActList.Count;
					if (flag4)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000D9D68 File Offset: 0x000D7F68
	public int Get_UserLvRewardNum()
	{
		int num = 0;
		bool flag = this.m_szUserLvReward == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(10);
			bool flag2 = openActIdByType == 0;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
				bool flag3 = actDataById == null;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataById.nXmlId];
					foreach (int num2 in actDaTingAward.szActList.Keys)
					{
						bool flag4 = PlayerDataManager.getInstance().HUserData.m_nLevel >= actDaTingAward.szActList[num2].needLv && LiBaoManager.GetInstance().Get_UserLvState(num2) == -1;
						if (flag4)
						{
							num = 1;
							break;
						}
					}
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000D9E78 File Offset: 0x000D8078
	public bool CanShowActUI(int nActType)
	{
		bool flag = ActivityManager.GetInstance().GetActDataXML_ID(nActType) == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = nActType == 19 && SkyLadderManager.GetInstance().m_nSkyLadderOpen < 1;
			result = !flag2;
		}
		return result;
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000D9EC0 File Offset: 0x000D80C0
	public AItemData GetActExpBuff()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(12);
		bool flag = actDataXML_ID == 0;
		AItemData result;
		if (flag)
		{
			result = null;
		}
		else
		{
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			result = actDaTingAward.szActList[0];
		}
		return result;
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000D9F0C File Offset: 0x000D810C
	public bool GetShouChongState()
	{
		bool flag = this.m_szShouChong != null && this.m_szShouChong.Count < fGameManager.mloadConfig.AllShouChongData.Count;
		if (flag)
		{
			foreach (int num in fGameManager.mloadConfig.AllShouChongData.Keys)
			{
				bool flag2 = this.m_nChongZhiNum >= fGameManager.mloadConfig.AllShouChongData[num].moneynum && this.m_szShouChong.IndexOf(num) == -1;
				if (flag2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000D9FD8 File Offset: 0x000D81D8
	public bool Lv30RewardState()
	{
		bool flag = this.m_szLvReward != null;
		if (flag)
		{
			foreach (int num in fGameManager.mloadConfig.AllLvReward.Keys)
			{
				bool flag2 = fGameManager.mloadConfig.AllLvReward[num].lv <= PlayerDataManager.getInstance().HUserData.m_nLevel && this.m_szLvReward.IndexOf(num) == -1;
				if (flag2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000DA094 File Offset: 0x000D8294
	public bool LianXuUI_ToggleState()
	{
		bool flag = this.m_szLianXu != null;
		if (flag)
		{
			foreach (int num in fGameManager.mloadConfig.AllLianXuData.Keys)
			{
				bool flag2 = fGameManager.mloadConfig.AllLianXuData[num].denglunum <= this.m_nLianXuNum && this.m_szLianXu.IndexOf(num) == -1;
				if (flag2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000DA144 File Offset: 0x000D8344
	private void ShowCDK_Msg(ArrayList szMsg)
	{
		bool flag = (int)szMsg[0] == -10;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42036, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = (int)szMsg[0] == -6;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42031, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = (int)szMsg[0] == -7;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42037, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = (int)szMsg[0] < 1;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42032, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
					}
					else
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(42033, new object[0]), "ffdd00", "system");
						this.m_nCDK_Num = (int)szMsg[1];
						bool flag5 = CDKRewardUICon.inst != null;
						if (flag5)
						{
							CDKRewardUICon.inst.UpDataBtnState();
							CDKRewardUICon.inst.ClearSend();
						}
						string text = (string)szMsg[3];
						string[] array = text.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i += 2)
						{
							TipManager.GetInstance().ShowRewardTip(5, new ArrayList
							{
								0,
								int.Parse(array[i]),
								int.Parse(array[i + 1])
							}, null, null);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000DA3F4 File Offset: 0x000D85F4
	public bool GetCDK_Act()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(13) == 0;
		return !flag;
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000DA420 File Offset: 0x000D8620
	private void SetNewSoldierReward(ArrayList szData)
	{
		this.m_szNewSoldierReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowNewSoldierToggleImg();
		}
		bool flag2 = NewSoldierUICon.inst != null;
		if (flag2)
		{
			NewSoldierUICon.inst.ShowUIData();
		}
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000DA470 File Offset: 0x000D8670
	public bool GetNewSoldierAct()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(22);
		bool flag = actDataByType == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			DateTime dRegDate = PlayerDataManager.getInstance().HUserData.m_dRegDate;
			DateTime t = DateTime.Parse(actDataByType.sStartTime);
			bool flag2 = dRegDate >= t && this.m_szNewSoldierReward.Count > 0 && ((int)this.m_szNewSoldierReward[2] < 2 || (int)this.m_szNewSoldierReward[3] < 2);
			result = flag2;
		}
		return result;
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000DA510 File Offset: 0x000D8710
	public int GetNewSoldierReward()
	{
		int result = 0;
		bool flag = this.CanShowActUI(22) && this.m_szNewSoldierReward.Count > 0 && ((int)this.m_szNewSoldierReward[2] == 1 || (int)this.m_szNewSoldierReward[3] == 1);
		if (flag)
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000DA574 File Offset: 0x000D8774
	public int GetNewSoldierReward(int ntype)
	{
		int result = 0;
		bool flag = this.m_szNewSoldierReward.Count > 0;
		if (flag)
		{
			result = (int)this.m_szNewSoldierReward[ntype];
		}
		return result;
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000DA5B0 File Offset: 0x000D87B0
	public void SetWeekendOnlineReward(ArrayList szData)
	{
		this.m_szWeekendOnlineReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowWeekendOnlineImg();
		}
		bool flag2 = WeekendOnlineUICon.inst != null;
		if (flag2)
		{
			WeekendOnlineUICon.inst.ShowUIData();
		}
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000DA600 File Offset: 0x000D8800
	public int GetWeekendOnlineReward()
	{
		int result = 0;
		bool flag = this.CanShowActUI(23) && this.m_szWeekendOnlineReward.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szWeekendOnlineReward[2];
			bool flag2 = arrayList.Contains(-2);
			if (flag2)
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000DA660 File Offset: 0x000D8860
	public int GetWeekendOnlineReward(int nday)
	{
		int result = 0;
		bool flag = this.m_szWeekendOnlineReward.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szWeekendOnlineReward[2];
			int num = arrayList.IndexOf(nday);
			bool flag2 = num != -1;
			if (flag2)
			{
				num = (int)arrayList[num + 1];
				bool flag3 = num == -2;
				if (flag3)
				{
					result = 1;
				}
				bool flag4 = num == -3;
				if (flag4)
				{
					result = 2;
				}
			}
		}
		return result;
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000DA6E8 File Offset: 0x000D88E8
	private void SetKaiXueGiftReward(ArrayList szData)
	{
		this.m_szKaiXueGiftReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowKaiXueGiftToggleImg();
		}
		bool flag2 = KaiXueUICon.inst != null;
		if (flag2)
		{
			KaiXueUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000DA738 File Offset: 0x000D8938
	public bool isShowKaiXueGift()
	{
		return this.CanShowActUI(24) && this.m_szKaiXueGiftReward.Count > 0 && (int)this.m_szKaiXueGiftReward[2] < 2;
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000DA788 File Offset: 0x000D8988
	public int GetKaiXueGiftReward()
	{
		int result = 0;
		bool flag = this.CanShowActUI(24) && this.m_szKaiXueGiftReward.Count > 0;
		if (flag)
		{
			result = (int)this.m_szKaiXueGiftReward[2];
		}
		return result;
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x000DA7D0 File Offset: 0x000D89D0
	private void SetMatchRewardActData(ArrayList szData)
	{
		this.m_szMatchReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowMatchRewardToggleImg();
		}
		bool flag2 = MatchRewardUICon.inst != null;
		if (flag2)
		{
			MatchRewardUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x000DA820 File Offset: 0x000D8A20
	public int GetMatchReward()
	{
		int num = 0;
		int num2 = 0;
		bool flag = !this.CanShowActUI(25);
		int result;
		if (flag)
		{
			result = num2;
		}
		else
		{
			bool flag2 = this.m_szMatchReward.Count == 0;
			if (flag2)
			{
				result = num2;
			}
			else
			{
				ArrayList arrayList = (ArrayList)this.m_szMatchReward[2];
				for (int i = 0; i < arrayList.Count; i += 3)
				{
					int num3 = (int)arrayList[i + 2];
					int num4 = num3;
					if (num4 == 1)
					{
						num2 = 1;
						break;
					}
					if (num4 == 2)
					{
						num++;
					}
				}
				bool flag3 = arrayList.Count > 0 && num == arrayList.Count / 3;
				if (flag3)
				{
					num2 = 2;
				}
				result = num2;
			}
		}
		return result;
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000DA8F0 File Offset: 0x000D8AF0
	public int GetMatchReward(int nIdx)
	{
		int num = 0;
		bool flag = this.m_szMatchReward.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.m_szMatchReward[2];
			for (int i = 0; i < arrayList.Count; i += 3)
			{
				int num2 = (int)arrayList[i];
				bool flag2 = nIdx == num2;
				if (flag2)
				{
					num = (int)arrayList[i + 2];
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000DA978 File Offset: 0x000D8B78
	public int GetMatchRewardNum(int nIdx)
	{
		int num = 0;
		bool flag = this.m_szMatchReward.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.m_szMatchReward[2];
			for (int i = 0; i < arrayList.Count; i += 3)
			{
				int num2 = (int)arrayList[i];
				bool flag2 = nIdx == num2;
				if (flag2)
				{
					num = (int)arrayList[i + 1];
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000DAA00 File Offset: 0x000D8C00
	private void SetGoddessGiftReward(ArrayList szData)
	{
		this.m_szGoddessGiftReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowGoddessGiftToggleImg();
		}
		bool flag2 = GoddessGiftUICon.inst != null;
		if (flag2)
		{
			GoddessGiftUICon.inst.ShowUIData();
		}
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000DAA50 File Offset: 0x000D8C50
	public void SetGoddessGiftClick(bool state)
	{
		this.isGoddessGiftClick = state;
		this.CheckLiBaoCanGet();
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowGoddessGiftToggleImg();
		}
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000DAA88 File Offset: 0x000D8C88
	public bool IsShowGoddessGiftEff()
	{
		bool flag = this.CanShowActUI(26);
		if (flag)
		{
			int goddessGiftReward = this.GetGoddessGiftReward();
			bool flag2 = goddessGiftReward == 0 && !this.isGoddessGiftClick;
			if (flag2)
			{
				return true;
			}
			bool flag3 = goddessGiftReward == 1;
			if (flag3)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000DAAD8 File Offset: 0x000D8CD8
	public int GetGoddessGiftReward()
	{
		int result = 0;
		bool flag = this.CanShowActUI(26) && this.m_szGoddessGiftReward.Count > 0;
		if (flag)
		{
			result = (int)this.m_szGoddessGiftReward[2];
		}
		return result;
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000DAB20 File Offset: 0x000D8D20
	private void SetWeekChargeReward(ArrayList szData)
	{
		this.m_szWeekChargeReward = szData;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowWeekChargeToggleImg();
		}
		bool flag2 = WeekChargeUICon.inst != null;
		if (flag2)
		{
			WeekChargeUICon.inst.ShowUIData();
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000DAB70 File Offset: 0x000D8D70
	public void SetWeekChargeClick(bool state)
	{
		this.isWeekChargeClick = state;
		this.CheckLiBaoCanGet();
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowWeekChargeToggleImg();
		}
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000DABA8 File Offset: 0x000D8DA8
	public bool IsShowWeekChargeEff()
	{
		bool flag = this.CanShowActUI(27);
		if (flag)
		{
			int weekChargeReward = this.GetWeekChargeReward();
			bool flag2 = weekChargeReward == 0 && !this.isWeekChargeClick;
			if (flag2)
			{
				return true;
			}
			bool flag3 = weekChargeReward == 1;
			if (flag3)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000DABF8 File Offset: 0x000D8DF8
	public int GetWeekChargeReward()
	{
		bool flag = !this.CanShowActUI(27);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = this.m_szWeekChargeReward.Count > 0;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)this.m_szWeekChargeReward[3];
				bool flag3 = arrayList.Contains(1);
				if (flag3)
				{
					return 1;
				}
				WeekChargeData weekChargeData = fGameManager.mloadConfig.AllWeekCharge[(int)this.m_szWeekChargeReward[0]];
				WeekChargeReward weekChargeReward = weekChargeData.allChargeReward[1];
				int num = 0;
				for (int i = 0; i < weekChargeReward.szReward.Count; i++)
				{
					bool flag4 = i < arrayList.Count && (int)arrayList[i] == 2;
					if (flag4)
					{
						num++;
					}
				}
				bool flag5 = num == weekChargeReward.szReward.Count;
				if (flag5)
				{
					return 2;
				}
			}
			result = 0;
		}
		return result;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000DAD04 File Offset: 0x000D8F04
	public int GetWeekChargeNum()
	{
		int result = 0;
		bool flag = this.m_szWeekChargeReward.Count > 0;
		if (flag)
		{
			result = (int)this.m_szWeekChargeReward[4];
		}
		return result;
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000DAD40 File Offset: 0x000D8F40
	public int GetWeekChargeReward(int nweek, int nidx)
	{
		int result = 0;
		bool flag = this.m_szWeekChargeReward.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szWeekChargeReward[3];
			result = (int)arrayList[nidx];
		}
		return result;
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000DAD88 File Offset: 0x000D8F88
	private void SetLuckyDrawData(ArrayList szData)
	{
		this.m_szLuckyDrawData = szData;
		bool flag = this.m_szLuckyDrawData.Count == 0;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szLuckyDrawData[4];
			bool flag2 = arrayList.Count > 0;
			if (flag2)
			{
				bool flag3 = LuckyDrawUICon.inst != null && LuckyDrawUICon.inst.visible;
				if (flag3)
				{
					LuckyDrawUICon.inst.ShowRewardAnim();
				}
				else
				{
					fGameManager.SendMsgToHallServer(1315, new ArrayList
					{
						15029,
						new ArrayList()
					});
				}
			}
			else
			{
				bool flag4 = LuckyDrawUICon.inst != null && LuckyDrawUICon.inst.visible;
				if (flag4)
				{
					LuckyDrawUICon.inst.ResetUIData(1);
				}
			}
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000DAE60 File Offset: 0x000D9060
	private void SetBOGOActData(ArrayList szMsg)
	{
		this.m_szBOGOActData = szMsg;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowBOGOActToggleImg();
		}
		bool flag2 = BOGOActUICon.inst != null;
		if (flag2)
		{
			BOGOActUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000DAEB0 File Offset: 0x000D90B0
	public int GetBOGOActReward()
	{
		int result = 0;
		bool flag = this.m_szBOGOActData.Count > 0;
		if (flag)
		{
			result = (int)this.m_szBOGOActData[3];
		}
		return result;
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000DAEEC File Offset: 0x000D90EC
	public void CheckNationalActReward()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetNationalDayBtn(this.IsShowNationalDayBtn());
			MainUICon.inst.SetNationalDayImg(this.IsNationalDayReward());
			MainUICon.inst.SetHuiliuBtn(this.GetHuiliuRewardState());
			MainUICon.inst.SetWishTreeBtn(this.CanShowActUI(43));
			MainUICon.inst.SetWinterActBtn(this.CanShowActUI(46));
			MainUICon.inst.SetWinterActBtnTip(this.IsWinterActTip());
		}
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x000DAF78 File Offset: 0x000D9178
	private bool IsNationalDayReward()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.GetRechargeItemState();
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.GetDiscountItemState();
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = this.GetHeroLimitBuyState();
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.GetDiscountShopState();
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = this.GetHeroReturnState();
		}
		return flag;
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x000DAFE8 File Offset: 0x000D91E8
	public bool IsShowNationalDayBtn()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.CanShowActUI(34);
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.CanShowActUI(35);
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = this.CanShowActUI(36);
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.CanShowActUI(39);
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = this.CanShowActUI(38);
		}
		return flag;
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x000DB064 File Offset: 0x000D9264
	private void SetRechargeItemData(ArrayList szMsg)
	{
		this.m_szRechargeItemData = szMsg;
		bool flag = NationalDayUICon.inst != null;
		if (flag)
		{
			NationalDayUICon.inst.ShowRechargeItemImg();
		}
		bool flag2 = RechargeItemUICon.inst != null;
		if (flag2)
		{
			RechargeItemUICon.inst.ShowUIData(0);
		}
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x000DB0B4 File Offset: 0x000D92B4
	public bool GetRechargeItemState()
	{
		int num = 0;
		bool flag = this.m_szRechargeItemData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szRechargeItemData[3];
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				bool flag2 = (int)arrayList[i + 1] == 1;
				if (flag2)
				{
					num = 1;
					break;
				}
			}
		}
		return num == 1;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000DB12C File Offset: 0x000D932C
	public int GetRechargeItemTotal()
	{
		int result = 0;
		bool flag = this.m_szRechargeItemData.Count > 0;
		if (flag)
		{
			result = (int)this.m_szRechargeItemData[2];
		}
		return result;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000DB168 File Offset: 0x000D9368
	public int GetRechargeItemState(int nidx)
	{
		int result = 0;
		bool flag = this.m_szRechargeItemData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szRechargeItemData[3];
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				bool flag2 = (int)arrayList[i] == nidx;
				if (flag2)
				{
					result = (int)arrayList[i + 1];
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000DB1E8 File Offset: 0x000D93E8
	private void SetDiscountItemData(ArrayList szMsg)
	{
		this.m_szDiscountItemData = szMsg;
		bool flag = DiscountItemUICon.inst != null;
		if (flag)
		{
			DiscountItemUICon.inst.ShowUIData(0);
		}
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x000DB21C File Offset: 0x000D941C
	public void SetDiscountItemClick(bool state)
	{
		this.isDiscountItemClick = state;
		bool flag = NationalDayUICon.inst != null;
		if (flag)
		{
			NationalDayUICon.inst.ShowDiscountItemImg();
		}
		this.CheckNationalActReward();
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000DB254 File Offset: 0x000D9454
	public bool GetDiscountItemState()
	{
		bool flag = this.CanShowActUI(35);
		return flag && !this.isDiscountItemClick;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000DB284 File Offset: 0x000D9484
	public int GetDiscountItemState(int ntabIdx, int nindex, int itemid)
	{
		int result = 0;
		bool flag = this.m_szDiscountItemData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szDiscountItemData[2];
			int num = arrayList.IndexOf(ntabIdx);
			bool flag2 = num != -1;
			if (flag2)
			{
				arrayList = (ArrayList)arrayList[num + 1];
				for (int i = 0; i < arrayList.Count; i += 3)
				{
					bool flag3 = (int)arrayList[i] == nindex && (int)arrayList[i + 1] == itemid;
					if (flag3)
					{
						result = (int)arrayList[i + 2];
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x000DB34C File Offset: 0x000D954C
	public void SetHeroLimitBuyClick(bool state)
	{
		this.isHeroLimitBuyClick = state;
		bool flag = NationalDayUICon.inst != null;
		if (flag)
		{
			NationalDayUICon.inst.ShowHeroLimitBuyImg();
		}
		this.CheckNationalActReward();
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000DB384 File Offset: 0x000D9584
	public bool GetHeroLimitBuyState()
	{
		bool flag = this.CanShowActUI(36);
		return flag && !this.isHeroLimitBuyClick;
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000DB3B4 File Offset: 0x000D95B4
	private void SetHuiliuActData(ArrayList szMsg)
	{
		this.m_szHuiliuActData = szMsg;
		bool flag = this.m_szHuiliuActData.Count != 0;
		if (flag)
		{
			bool flag2 = ActivityManager.GetInstance().GetActDataByType(37) == null && (int)this.m_szHuiliuActData[3] == 1;
			if (flag2)
			{
				fUIManager.GetInstance().ShowWin("HuiliuUI", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
			}
		}
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000DB420 File Offset: 0x000D9620
	public bool GetHuiliuRewardState()
	{
		bool flag = this.m_szHuiliuActData.Count == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (int)this.m_szHuiliuActData[3];
			result = (num == 1);
		}
		return result;
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x000DB460 File Offset: 0x000D9660
	public ArrayList GetHuiliuActData()
	{
		return (ArrayList)this.m_szHuiliuActData.Clone();
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000DB484 File Offset: 0x000D9684
	public bool CheckHeroReturn(int nSlcHeroId, int nSlcType, int nSlcId)
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(38);
		bool flag2 = actDataByType == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = !fGameManager.mloadConfig.AllHeroReturnData.ContainsKey(actDataByType.nXmlId);
			if (flag3)
			{
				result = flag;
			}
			else
			{
				HeroReturnData heroReturnData = fGameManager.mloadConfig.AllHeroReturnData[actDataByType.nXmlId];
				string key = string.Format("{0}_{1}_{2}", nSlcHeroId, nSlcType, nSlcId);
				result = heroReturnData.allHeroReturnInfo.ContainsKey(key);
			}
		}
		return result;
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000DB51C File Offset: 0x000D971C
	public void SetHeroReturnClick(bool state)
	{
		this.isHeroReturnClick = state;
		bool flag = NationalDayUICon.inst != null;
		if (flag)
		{
			NationalDayUICon.inst.ShowHeroReturnImg();
		}
		this.CheckNationalActReward();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000DB554 File Offset: 0x000D9754
	public bool GetHeroReturnState()
	{
		bool flag = this.CanShowActUI(38);
		return flag && !this.isHeroReturnClick;
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000DB584 File Offset: 0x000D9784
	public bool CheckDiscountShop(int nShopId)
	{
		bool flag = false;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(39);
		bool flag2 = actDataByType == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = !fGameManager.mloadConfig.AllDiscountShopData.ContainsKey(actDataByType.nXmlId);
			if (flag3)
			{
				result = flag;
			}
			else
			{
				DiscountShopData discountShopData = fGameManager.mloadConfig.AllDiscountShopData[actDataByType.nXmlId];
				result = discountShopData.szShopId.Contains(nShopId);
			}
		}
		return result;
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x000DB5FC File Offset: 0x000D97FC
	public int GetDiscountShopPrice(ShopData shopData, int nNum, int nPrice)
	{
		int num = 0;
		int num2 = 0;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(39);
		bool flag = actDataByType == null;
		int result;
		if (flag)
		{
			result = nPrice;
		}
		else
		{
			bool flag2 = !fGameManager.mloadConfig.AllDiscountShopData.ContainsKey(actDataByType.nXmlId);
			if (flag2)
			{
				result = nPrice;
			}
			else
			{
				DiscountShopData discountShopData = fGameManager.mloadConfig.AllDiscountShopData[actDataByType.nXmlId];
				bool flag3 = !discountShopData.szShopId.Contains(shopData.key);
				if (flag3)
				{
					result = nPrice;
				}
				else
				{
					bool flag4 = !fGameManager.mloadConfig.AllItemData.ContainsKey(shopData.m_nItemId);
					if (flag4)
					{
						result = nPrice;
					}
					else
					{
						ItemData itemData = fGameManager.mloadConfig.AllItemData[shopData.m_nItemId];
						int num3 = 0;
						while (num3 < discountShopData.szDiscountInfo.Count && nNum >= discountShopData.szDiscountInfo[num3])
						{
							num2 = discountShopData.szDiscountInfo[num3 + 2];
							num3 += 4;
						}
						bool flag5 = itemData.m_nDiamondPrice * num2 * nNum % 100 != 0;
						if (flag5)
						{
							num = 1;
						}
						nPrice = itemData.m_nDiamondPrice * num2 * nNum / 100 + num;
						result = nPrice;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000DB748 File Offset: 0x000D9948
	public void SetDiscountShopClick(bool state)
	{
		this.isDiscountShopClick = state;
		bool flag = NationalDayUICon.inst != null;
		if (flag)
		{
			NationalDayUICon.inst.ShowDiscountShopImg();
		}
		this.CheckNationalActReward();
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x000DB780 File Offset: 0x000D9980
	public bool GetDiscountShopState()
	{
		bool flag = this.CanShowActUI(39);
		return flag && !this.isDiscountShopClick;
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x000DB7B0 File Offset: 0x000D99B0
	public void SetTuijianClick(bool state)
	{
		this.isTuijianClick = state;
		this.CheckLiBaoCanGet();
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowTuijianToggleImg();
		}
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000DB7E8 File Offset: 0x000D99E8
	public bool IsShowTuijianEff()
	{
		bool flag = this.CanShowActUI(40);
		return flag && !this.isTuijianClick;
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000DB818 File Offset: 0x000D9A18
	private void ShowWishTreeTip(ArrayList szData)
	{
		bool flag = szData.Count == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(34007, new object[0]), Language.GetLang(34008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			TipManager.GetInstance().ShowItemTip(Language.GetLang(34009, new object[0]), string.Empty, (ArrayList)szData.Clone(), Language.GetLang(8012, new object[0]), null, null);
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000DB8C0 File Offset: 0x000D9AC0
	public void SetWaiGuaClick(bool state)
	{
		this.isWaiGuaClick = state;
		this.CheckLiBaoCanGet();
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowWaiGuaToggleImg();
		}
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000DB8F8 File Offset: 0x000D9AF8
	public bool IsShowWaiGuaEff()
	{
		bool flag = LiBaoManager.isWaiGuaOpen;
		return flag && !this.isWaiGuaClick;
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000DB924 File Offset: 0x000D9B24
	private void SetMultiBOGOData(ArrayList szMsg)
	{
		this.m_szMultiBOGOData = szMsg;
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowMultiBOGOToggleImg();
		}
		bool flag2 = MultiBOGOUICon.inst != null;
		if (flag2)
		{
			MultiBOGOUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000DB974 File Offset: 0x000D9B74
	public bool IsShowMultiBOGOEff()
	{
		bool flag = this.CanShowActUI(45);
		bool flag2 = flag;
		bool result;
		if (flag2)
		{
			int num = 0;
			bool flag3 = this.m_szMultiBOGOData.Count > 0;
			if (flag3)
			{
				num = (int)this.m_szMultiBOGOData[3];
			}
			bool flag4 = num == 1;
			result = (flag4 || !this.isMultiBOGOClick);
		}
		else
		{
			result = flag;
		}
		return result;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
	public void SetMultiBOGOClick(bool state)
	{
		this.isMultiBOGOClick = state;
		this.CheckLiBaoCanGet();
		bool flag = LiBaoUICon.inst != null;
		if (flag)
		{
			LiBaoUICon.inst.ShowMultiBOGOToggleImg();
		}
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000DBA18 File Offset: 0x000D9C18
	public void CheckJieRiActReward()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetJieRiActBtn(this.IsShowJieRiActBtn());
			MainUICon.inst.SetJieRiActImg(this.IsJieRiActReward());
		}
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000DBA5C File Offset: 0x000D9C5C
	private bool IsJieRiActReward()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.GetRechargeZuanState();
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.GetJieriTipState();
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = (this.GetNaMatchReward() == 1);
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.GetDoubleExpState();
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = (this.GetJieRiOnlineState() == 1);
		}
		return flag;
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000DBAD4 File Offset: 0x000D9CD4
	public bool IsShowJieRiActBtn()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.CanShowActUI(31);
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.CanShowActUI(32);
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = this.CanShowActUI(33);
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.CanShowActUI(12);
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = this.CanShowActUI(48);
		}
		return flag;
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000DBB50 File Offset: 0x000D9D50
	private void SetRechargeZuanData(ArrayList szMsg)
	{
		this.m_szRechargeZuanData = szMsg;
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowRechargeZuanImg();
		}
		bool flag2 = RechargeZuanUICon.inst != null;
		if (flag2)
		{
			RechargeZuanUICon.inst.ShowUIData();
		}
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000DBBA0 File Offset: 0x000D9DA0
	public bool GetRechargeZuanState()
	{
		int num = 0;
		bool flag = this.m_szRechargeZuanData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szRechargeZuanData[3];
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				bool flag2 = (int)arrayList[i + 1] == 1;
				if (flag2)
				{
					num = 1;
					break;
				}
			}
		}
		return num == 1;
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000DBC18 File Offset: 0x000D9E18
	public int GetRechargeZuanTotal()
	{
		int result = 0;
		bool flag = this.m_szRechargeZuanData.Count > 0;
		if (flag)
		{
			result = (int)this.m_szRechargeZuanData[2];
		}
		return result;
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000DBC54 File Offset: 0x000D9E54
	public int GetRechargeZuanState(int nidx)
	{
		int result = 0;
		bool flag = this.m_szRechargeZuanData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szRechargeZuanData[3];
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				bool flag2 = (int)arrayList[i] == nidx;
				if (flag2)
				{
					result = (int)arrayList[i + 1];
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x000DBCD4 File Offset: 0x000D9ED4
	public bool GetJieriTipState()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(32);
		bool flag = actDataByType == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int jieriLoginDay = this.GetJieriLoginDay();
			for (int i = 1; i <= jieriLoginDay; i++)
			{
				bool flag2 = this.GetJieriLoginReward(i) == 1;
				if (flag2)
				{
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x000DBD3C File Offset: 0x000D9F3C
	public int GetJieriLoginDay()
	{
		int result = 0;
		bool flag = this.m_szJieriLoginData.Count > 0;
		if (flag)
		{
			result = (int)this.m_szJieriLoginData[2];
		}
		return result;
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000DBD78 File Offset: 0x000D9F78
	public int GetJieriLoginReward(int nday)
	{
		int result = (nday <= this.GetJieriLoginDay()) ? 1 : 0;
		bool flag = this.m_szJieriLoginData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szJieriLoginData[3];
			int num = arrayList.IndexOf(nday);
			bool flag2 = num != -1;
			if (flag2)
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x000DBDE4 File Offset: 0x000D9FE4
	private void SetJieriLoginData(ArrayList szMsg)
	{
		this.m_szJieriLoginData = szMsg;
		bool flag = NaLianXuUICon.inst != null;
		if (flag)
		{
			NaLianXuUICon.inst.UpDataAllReward();
			bool flag2 = JieRiActUICon.inst != null;
			if (flag2)
			{
				JieRiActUICon.inst.ShowNaLianXuWarnImg();
				JieRiActUICon.inst.IsShowNaMatchTg();
			}
		}
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x000DBE3C File Offset: 0x000DA03C
	private void SetNaMatchRewardActData(ArrayList szData)
	{
		this.m_szNaMatchReward = szData;
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowNaMatchRewardToggleImg();
		}
		bool flag2 = NaMatchRewardUICon.inst != null;
		if (flag2)
		{
			NaMatchRewardUICon.inst.ShowUIData();
		}
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000DBE8C File Offset: 0x000DA08C
	public int GetNaMatchReward()
	{
		int num = 0;
		int num2 = 0;
		bool flag = !this.CanShowActUI(33);
		int result;
		if (flag)
		{
			result = num2;
		}
		else
		{
			bool flag2 = this.m_szNaMatchReward.Count == 0;
			if (flag2)
			{
				result = num2;
			}
			else
			{
				ArrayList arrayList = (ArrayList)this.m_szNaMatchReward[2];
				for (int i = 0; i < arrayList.Count; i += 3)
				{
					int num3 = (int)arrayList[i + 2];
					int num4 = num3;
					if (num4 == 1)
					{
						num2 = 1;
						break;
					}
					if (num4 == 2)
					{
						num++;
					}
				}
				bool flag3 = arrayList.Count > 0 && num == arrayList.Count / 3;
				if (flag3)
				{
					num2 = 2;
				}
				result = num2;
			}
		}
		return result;
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x000DBF5C File Offset: 0x000DA15C
	public int GetNaMatchReward(int nIdx)
	{
		int num = 0;
		bool flag = this.m_szNaMatchReward.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.m_szNaMatchReward[2];
			for (int i = 0; i < arrayList.Count; i += 3)
			{
				int num2 = (int)arrayList[i];
				bool flag2 = nIdx == num2;
				if (flag2)
				{
					num = (int)arrayList[i + 2];
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x000DBFE4 File Offset: 0x000DA1E4
	public int GetMatchNaRewardNum(int nIdx)
	{
		int num = 0;
		bool flag = this.m_szNaMatchReward.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.m_szNaMatchReward[2];
			for (int i = 0; i < arrayList.Count; i += 3)
			{
				int num2 = (int)arrayList[i];
				bool flag2 = nIdx == num2;
				if (flag2)
				{
					num = (int)arrayList[i + 1];
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000DC06C File Offset: 0x000DA26C
	public void SetDoubleExpClick(bool state)
	{
		this.isDoubleExpClick = state;
		this.CheckJieRiActReward();
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowDoubleExpToggleImg();
		}
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000DC0A4 File Offset: 0x000DA2A4
	public bool GetDoubleExpState()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(12);
		bool flag = actDataByType == null;
		return !flag && !this.isDoubleExpClick;
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000DC0DC File Offset: 0x000DA2DC
	public void SetSkinDiscountClick(bool state)
	{
		this.isSkinDiscountClick = state;
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowSkinDiscountImg();
		}
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000DC110 File Offset: 0x000DA310
	public bool GetSkinDiscountState()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(47);
		bool flag = actDataByType == null;
		return !flag && !this.isSkinDiscountClick;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000DC148 File Offset: 0x000DA348
	private void SetJieRiOnlineData(ArrayList szData)
	{
		this.m_szJieRiOnline = szData;
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowJieRiOnlineImg();
		}
		bool flag2 = JieRiOnlineUICon.inst != null;
		if (flag2)
		{
			JieRiOnlineUICon.inst.ShowUIData();
		}
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x000DC198 File Offset: 0x000DA398
	public void SetJieRiOnlineClick(bool state)
	{
		this.isJieRiOnlineClick = state;
		this.CheckJieRiActReward();
		bool flag = JieRiActUICon.inst != null;
		if (flag)
		{
			JieRiActUICon.inst.ShowJieRiOnlineImg();
		}
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000DC1D0 File Offset: 0x000DA3D0
	public bool IsShowJieRiOnlineEff()
	{
		bool flag = this.CanShowActUI(48);
		bool flag2 = flag;
		if (flag2)
		{
			switch (this.GetJieRiOnlineState())
			{
			case 0:
				return !this.isJieRiOnlineClick;
			case 1:
				return true;
			case 2:
				return false;
			}
		}
		return flag;
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000DC22C File Offset: 0x000DA42C
	public int GetJieRiOnlineState()
	{
		int num = 0;
		bool flag = this.m_szJieRiOnline.Count > 0;
		if (flag)
		{
			num = (int)this.m_szJieRiOnline[2];
			bool flag2 = num != 2;
			if (flag2)
			{
				JieRiOnline jieRiOnline = fGameManager.mloadConfig.AllJieRiOnline[(int)this.m_szJieRiOnline[0]];
				DateTime serverDate = Fun.GetServerDate();
				DateTime t = DateTime.Parse(jieRiOnline.sStartTime);
				DateTime t2 = DateTime.Parse(jieRiOnline.sEndTime);
				num = ((!(serverDate < t) && !(serverDate > t2)) ? 1 : 0);
			}
		}
		return num;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000DC2D4 File Offset: 0x000DA4D4
	public void CheckFuliCanGet()
	{
		bool fuliBtnTip = false;
		bool signState = this.GetSignState();
		if (signState)
		{
			fuliBtnTip = true;
		}
		bool flag = this.OnLine_ToggleState() > 0;
		if (flag)
		{
			fuliBtnTip = true;
		}
		bool flag2 = this.OnTime_ToggleState() > 0;
		if (flag2)
		{
			fuliBtnTip = true;
		}
		bool flag3 = this.SkyAct_ToggleState() > 0;
		if (flag3)
		{
			fuliBtnTip = true;
		}
		bool flag4 = this.GetWeekLoginReward() == 0;
		if (flag4)
		{
			fuliBtnTip = true;
		}
		bool flag5 = this.IsShowSkyEveryDayEff();
		if (flag5)
		{
			fuliBtnTip = true;
		}
		bool flag6 = this.IsShowPvpEveryWeekEff();
		if (flag6)
		{
			fuliBtnTip = true;
		}
		bool flag7 = MainUICon.inst != null;
		if (flag7)
		{
			MainUICon.inst.SetFuliBtnTip(fuliBtnTip);
			MainUICon.inst.SetFuliBtnActive(this.GetFuliBtnState());
		}
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x000DC38C File Offset: 0x000DA58C
	private bool GetFuliBtnState()
	{
		bool flag = false;
		bool flag2 = !flag;
		if (flag2)
		{
			flag = this.Get_SingOpen();
		}
		bool flag3 = !flag;
		if (flag3)
		{
			flag = this.Get_OnLineOpen();
		}
		bool flag4 = !flag;
		if (flag4)
		{
			flag = this.Get_OnTimeOpen();
		}
		bool flag5 = !flag;
		if (flag5)
		{
			flag = this.Get_SkyActOpen();
		}
		bool flag6 = !flag;
		if (flag6)
		{
			flag = this.CanShowActUI(20);
		}
		bool flag7 = !flag;
		if (flag7)
		{
			flag = this.CanShowActUI(21);
		}
		bool flag8 = !flag;
		if (flag8)
		{
			flag = this.CanShowActUI(44);
		}
		return flag;
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000DC428 File Offset: 0x000DA628
	public int GetSignData(int nDay)
	{
		return (int)this.m_szSignData[nDay];
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000DC44C File Offset: 0x000DA64C
	public bool Get_SignAllRewardState(int index)
	{
		bool flag = this.m_szSignReward.IndexOf(index) != -1;
		return !flag;
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x000DC480 File Offset: 0x000DA680
	public bool Get_OneSignRewardState(int index)
	{
		SignXMLData signXML_Data = this.GetSignXML_Data();
		bool flag = signXML_Data == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.m_nSignAllNum >= signXML_Data.AllReward[index].signnum;
			result = flag2;
		}
		return result;
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x000DC4CC File Offset: 0x000DA6CC
	public bool Get_OneSignRewardState()
	{
		SignXMLData signXML_Data = this.GetSignXML_Data();
		bool flag = signXML_Data == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			foreach (int num in signXML_Data.AllReward.Keys)
			{
				bool flag2 = this.m_nSignAllNum >= signXML_Data.AllReward[num].signnum && this.m_szSignReward.IndexOf(num) == -1;
				if (flag2)
				{
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x000DC57C File Offset: 0x000DA77C
	public SignXMLData GetSignXML_Data()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(11);
		bool flag = actDataXML_ID == 0;
		SignXMLData result;
		if (flag)
		{
			result = null;
		}
		else
		{
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			AItemData aitemData = actDaTingAward.szActList[Fun.GetServerDate().Month];
			int awardid = aitemData.awardid;
			result = fGameManager.mloadConfig.AllSignAward[awardid];
		}
		return result;
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000DC5F0 File Offset: 0x000DA7F0
	public bool CanShowSignUI()
	{
		bool flag = ActivityManager.GetInstance().GetActDataXML_ID(11) == 0;
		return !flag;
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x000DC61C File Offset: 0x000DA81C
	private void UpDataSignData(ArrayList szMsg)
	{
		this.m_szSignData = (ArrayList)szMsg[0];
		this.m_nSignAllNum = (int)szMsg[1];
		this.m_szRepairSignData = (ArrayList)szMsg[2];
		bool flag = SignRewardUICon.inst != null;
		if (flag)
		{
			SignRewardUICon.inst.ShowUIData();
			FuliUICon.inst.ShowSignImg();
		}
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x000DC688 File Offset: 0x000DA888
	private void UpDataAllSignData(ArrayList szMsg)
	{
		this.m_szSignReward = szMsg;
		bool flag = SignRewardUICon.inst != null;
		if (flag)
		{
			SignRewardUICon.inst.UpDataAllReward();
			FuliUICon.inst.ShowSignImg();
		}
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x000DC6C4 File Offset: 0x000DA8C4
	public int GetSignAllNum()
	{
		return this.m_nSignAllNum;
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000DC6DC File Offset: 0x000DA8DC
	public bool GetSignState()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(11) == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.m_szSignData == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.GetSignData(Fun.GetServerDate().Day) == 0;
				if (flag3)
				{
					result = true;
				}
				else
				{
					bool oneSignRewardState = this.Get_OneSignRewardState();
					result = oneSignRewardState;
				}
			}
		}
		return result;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000DC74C File Offset: 0x000DA94C
	public bool Get_SingOpen()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(11) == 0;
		return !flag;
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000DC778 File Offset: 0x000DA978
	private void UpDataOnLineRewardData(ArrayList szMsg)
	{
		this.m_dDayOnLoginDate = DateTime.Parse(szMsg[0].ToString());
		this.m_nDayOnlineTime = (int)szMsg[1];
		this.m_szOnlineReward = (ArrayList)szMsg[2];
		bool flag = DayOnlineRewardUICon.inst != null;
		if (flag)
		{
			DayOnlineRewardUICon.inst.ShowUIData();
			FuliUICon.inst.ShowOnLine_ToggleImg();
		}
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000DC7E8 File Offset: 0x000DA9E8
	public DateTime GetDayOnLoginDate()
	{
		return this.m_dDayOnLoginDate;
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000DC800 File Offset: 0x000DAA00
	public ArrayList GetOnLineReward()
	{
		return this.m_szOnlineReward;
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x000DC818 File Offset: 0x000DAA18
	public int GetOnLineLong()
	{
		return (int)(Fun.GetServerDate() - this.m_dDayOnLoginDate).TotalSeconds + this.m_nDayOnlineTime;
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x000DC84C File Offset: 0x000DAA4C
	public int OnLine_ToggleState()
	{
		int num = 0;
		int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(17);
		bool flag = openActIdByType == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
			bool flag2 = actDataById == null;
			if (flag2)
			{
				result = num;
			}
			else
			{
				ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataById.nXmlId];
				ArrayList arrayList = new ArrayList(actDaTingAward.szActList.Keys);
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					int num2 = (int)arrayList[i];
					bool flag3 = !actDaTingAward.szActList.ContainsKey(num2);
					if (!flag3)
					{
						AItemData aitemData = actDaTingAward.szActList[num2];
						bool flag4 = aitemData != null && this.m_szOnlineReward.IndexOf(num2) == -1;
						if (flag4)
						{
							int onLineLong = this.GetOnLineLong();
							bool flag5 = onLineLong >= aitemData.OnLineTime * 60;
							if (flag5)
							{
								num++;
							}
						}
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x000DC97C File Offset: 0x000DAB7C
	public bool Get_OnLineOpen()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(17) == 0;
		return !flag;
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x000DC9A8 File Offset: 0x000DABA8
	private void UpDataOnTimeRewardData(ArrayList szMsg)
	{
		this.m_szOnTimeReward = szMsg;
		bool flag = DayOnTimeRewardUICon.inst != null;
		if (flag)
		{
			DayOnTimeRewardUICon.inst.ShowUIData();
			FuliUICon.inst.ShowOnTime_ToggleImg();
		}
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x000DC9E4 File Offset: 0x000DABE4
	public ArrayList GetOnTimeReward()
	{
		return this.m_szOnTimeReward;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000DC9FC File Offset: 0x000DABFC
	public int OnTime_ToggleState()
	{
		int num = 0;
		int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(18);
		bool flag = openActIdByType == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
			bool flag2 = actDataById == null;
			if (flag2)
			{
				result = num;
			}
			else
			{
				DateTime dayOnLoginDate = LiBaoManager.GetInstance().GetDayOnLoginDate();
				ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataById.nXmlId];
				ArrayList arrayList = new ArrayList(actDaTingAward.szActList.Keys);
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					int num2 = (int)arrayList[i];
					bool flag3 = !actDaTingAward.szActList.ContainsKey(num2);
					if (!flag3)
					{
						AItemData aitemData = actDaTingAward.szActList[num2];
						bool flag4 = aitemData == null;
						if (!flag4)
						{
							int num3 = this.m_szOnTimeReward.IndexOf(num2);
							bool flag5 = num3 != -1;
							if (flag5)
							{
								bool flag6 = (int)this.m_szOnTimeReward[num3 + 1] == 0;
								if (flag6)
								{
									num++;
								}
							}
							else
							{
								DateTime d = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd") + " " + aitemData.sOnTimeStart);
								DateTime d2 = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd") + " " + aitemData.sOnTimeEnd);
								TimeSpan timeSpan = dayOnLoginDate - d2;
								TimeSpan timeSpan2 = d - Fun.GetServerDate();
								bool flag7 = (int)timeSpan.TotalMilliseconds <= 0 && (int)timeSpan2.TotalMilliseconds <= 0;
								if (flag7)
								{
									num++;
								}
							}
						}
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000DCBE0 File Offset: 0x000DADE0
	public bool Get_OnTimeOpen()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(18) == 0;
		return !flag;
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x000DCC0C File Offset: 0x000DAE0C
	private void UpDataSkyActRewardData(ArrayList szMsg)
	{
		this.m_dSkyActDate = DateTime.Parse(szMsg[0].ToString());
		this.m_nSkyActNum = (int)szMsg[1];
		this.m_szSkyActReward = (ArrayList)szMsg[2];
		bool flag = SkyActRewardUICon.inst != null;
		if (flag)
		{
			SkyActRewardUICon.inst.ShowUIData();
			FuliUICon.inst.ShowSkyAct_ToggleImg();
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000DCC7C File Offset: 0x000DAE7C
	public int GetSkyActNum()
	{
		return this.m_nSkyActNum;
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000DCC94 File Offset: 0x000DAE94
	public ArrayList GetSkyActReward()
	{
		return this.m_szSkyActReward;
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x000DCCAC File Offset: 0x000DAEAC
	public int SkyAct_ToggleState()
	{
		int num = 0;
		int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(19);
		bool flag = openActIdByType == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
			bool flag2 = actDataById == null;
			if (flag2)
			{
				result = num;
			}
			else
			{
				ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataById.nXmlId];
				ArrayList arrayList = new ArrayList(actDaTingAward.szActList.Keys);
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					int num2 = (int)arrayList[i];
					bool flag3 = actDaTingAward.szActList.ContainsKey(num2);
					if (flag3)
					{
						AItemData aitemData = actDaTingAward.szActList[num2];
						bool flag4 = aitemData != null && this.m_szSkyActReward.IndexOf(num2) == -1 && this.m_nSkyActNum >= aitemData.skyacttargetnum;
						if (flag4)
						{
							num++;
						}
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000DCDC0 File Offset: 0x000DAFC0
	public bool Get_SkyActOpen()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(19) == 0;
		return !flag;
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x000DCDEC File Offset: 0x000DAFEC
	private void SetWeekLoginReward(ArrayList szData)
	{
		this.m_szWeekLoginReward = szData;
		bool flag = FuliUICon.inst != null;
		if (flag)
		{
			FuliUICon.inst.ShowWeekLoginToggleImg();
		}
		bool flag2 = WeekLoginUICon.inst != null;
		if (flag2)
		{
			WeekLoginUICon.inst.ShowWeekReward();
		}
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x000DCE3C File Offset: 0x000DB03C
	public int GetWeekLoginReward()
	{
		int num = -1;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(20);
		bool flag = actDataByType == null;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime dDate = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
			DateTime dDate2 = DateTime.Parse(DateTime.Parse(actDataByType.sStartTime).ToString("yyyy-MM-dd"));
			int num2 = Fun.GetWeek2(dDate) - Fun.GetWeek2(dDate2) + 1;
			for (int i = 0; i < this.m_szWeekLoginReward.Count; i += 2)
			{
				int num3 = (int)this.m_szWeekLoginReward[i];
				bool flag2 = num3 == num2;
				if (flag2)
				{
					num = (int)this.m_szWeekLoginReward[i + 1];
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000DCF18 File Offset: 0x000DB118
	private void SetSkyEveryDayData(ArrayList szData)
	{
		bool flag = this.m_szSkyEveryDay.Count == 0 && szData.Count > 0;
		if (flag)
		{
			this.nMatchNum = (int)szData[1];
		}
		this.m_szSkyEveryDay = szData;
		bool flag2 = this.m_szSkyEveryDay.Count > 0;
		if (flag2)
		{
			SkyEveryDayData skyEveryDayData = fGameManager.mloadConfig.AllSkyEveryDay[(int)this.m_szSkyEveryDay[0]];
			this.nAddGoldNum = 0;
			bool flag3 = (int)this.m_szSkyEveryDay[1] > this.nMatchNum;
			if (flag3)
			{
				this.nAddGoldNum = skyEveryDayData.nGoldNum;
			}
			this.nMatchNum = (int)this.m_szSkyEveryDay[1];
			bool flag4 = FuliUICon.inst != null;
			if (flag4)
			{
				FuliUICon.inst.ShowSkyEveryDayToggleImg();
			}
			bool flag5 = SkyEveryDayUICon.inst != null;
			if (flag5)
			{
				SkyEveryDayUICon.inst.ShowUIData();
			}
		}
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000DD01C File Offset: 0x000DB21C
	public void RefreshSkyEveryDayEff()
	{
		this.CheckFuliCanGet();
		bool flag = FuliUICon.inst != null;
		if (flag)
		{
			FuliUICon.inst.ShowSkyEveryDayToggleImg();
		}
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000DD050 File Offset: 0x000DB250
	public bool IsShowSkyEveryDayEff()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(2);
		bool flag = actDataByType == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[actDataByType.nXmlId];
			DateTime serverDate = Fun.GetServerDate();
			bool flag2 = DateTime.Equals(serverDate, DateTime.MinValue);
			if (flag2)
			{
				result = false;
			}
			else
			{
				string[] skyLadderOpenTime = SkyLadderManager.GetInstance().GetSkyLadderOpenTime(serverDate);
				bool flag3 = string.IsNullOrEmpty(skyLadderOpenTime[0]);
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = this.CanShowActUI(21) && this.m_szSkyEveryDay.Count > 0;
					if (flag4)
					{
						SkyEveryDayData skyEveryDayData = fGameManager.mloadConfig.AllSkyEveryDay[(int)this.m_szSkyEveryDay[0]];
						DateTime t = DateTime.Parse(serverDate.ToString("yyyy-MM-dd") + " " + skyEveryDayData.sStartTime);
						DateTime t2 = DateTime.Parse(serverDate.ToString("yyyy-MM-dd") + " " + skyEveryDayData.sEndTime);
						bool flag5 = this.nMatchNum < skyEveryDayData.nMatchNum || (serverDate >= t && serverDate <= t2);
						if (flag5)
						{
							return true;
						}
					}
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000DD1A4 File Offset: 0x000DB3A4
	public void ShowSkyEveryDayGold()
	{
		bool flag = this.nAddGoldNum > 0;
		if (flag)
		{
			GameResultUICon.Inst.ShowAddGold(this.nAddGoldNum);
			this.nAddGoldNum = 0;
		}
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000DD1DC File Offset: 0x000DB3DC
	public int GetLastMatchNum()
	{
		int result = 0;
		bool flag = this.m_szSkyEveryDay.Count > 0;
		if (flag)
		{
			SkyEveryDayData skyEveryDayData = fGameManager.mloadConfig.AllSkyEveryDay[(int)this.m_szSkyEveryDay[0]];
			result = skyEveryDayData.nMatchNum - (int)this.m_szSkyEveryDay[1];
		}
		return result;
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x000DD240 File Offset: 0x000DB440
	private void SetPvpEveryWeekRewardData(ArrayList szData)
	{
		this.m_szPvpEveryWeek = szData;
		bool flag = FuliUICon.inst != null && FuliUICon.inst.visible;
		if (flag)
		{
			FuliUICon.inst.ShowPvpEveryWeekToggleImg();
		}
		bool flag2 = PvpEveryWeekUICon.inst != null;
		if (flag2)
		{
			PvpEveryWeekUICon.inst.ShowUIData();
		}
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000DD29C File Offset: 0x000DB49C
	public void SetPvpEveryWeekClick(bool state)
	{
		this.isPvpWeekClick = state;
		this.CheckFuliCanGet();
		bool flag = FuliUICon.inst != null;
		if (flag)
		{
			FuliUICon.inst.ShowPvpEveryWeekToggleImg();
		}
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x000DD2D4 File Offset: 0x000DB4D4
	public bool IsShowPvpEveryWeekEff()
	{
		bool flag = this.CanShowActUI(44);
		bool flag2 = flag;
		if (flag2)
		{
			switch (this.GetPvpRewardState())
			{
			case 0:
				return !this.isPvpWeekClick;
			case 1:
				return true;
			case 2:
				return false;
			}
		}
		return flag;
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000DD330 File Offset: 0x000DB530
	public int GetPvpMatchNum()
	{
		int result = 0;
		bool flag = this.m_szPvpEveryWeek.Count > 0;
		if (flag)
		{
			result = (int)this.m_szPvpEveryWeek[3];
		}
		return result;
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x000DD36C File Offset: 0x000DB56C
	public int GetPvpRewardState()
	{
		int result = 0;
		bool flag = this.m_szPvpEveryWeek.Count > 0;
		if (flag)
		{
			result = (int)this.m_szPvpEveryWeek[4];
		}
		return result;
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000DD3A8 File Offset: 0x000DB5A8
	private bool IsWinterActTip()
	{
		bool result = false;
		bool flag = this.CanShowActUI(46);
		if (flag)
		{
			result = !this.isWinterActClick;
		}
		return result;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000DD3D8 File Offset: 0x000DB5D8
	public void SetWinterActClick(bool state)
	{
		this.isWinterActClick = state;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetWinterActBtnTip(this.IsWinterActTip());
		}
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x000DD410 File Offset: 0x000DB610
	private void SetWinterActData(ArrayList szMsg)
	{
		this.m_szWinterActData = szMsg;
		bool flag = WinterActUICon.inst != null && WinterActUICon.inst.visible;
		if (flag)
		{
			WinterActUICon.inst.InitUI();
		}
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000DD450 File Offset: 0x000DB650
	public int GetWinterActTar(int ntype)
	{
		int result = 0;
		bool flag = this.m_szWinterActData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szWinterActData[3];
			int num = arrayList.IndexOf(ntype.ToString());
			bool flag2 = num != -1;
			if (flag2)
			{
				result = (int)arrayList[num + 1];
			}
		}
		return result;
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000DD4BC File Offset: 0x000DB6BC
	public int GetWinterActEx(int nidx)
	{
		int result = 0;
		bool flag = this.m_szWinterActData.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szWinterActData[4];
			result = (int)arrayList[nidx];
		}
		return result;
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x000DD504 File Offset: 0x000DB704
	public void OnLink_OpenLiBiaoUI()
	{
		bool flag = !this.isOpenLiBiaoUI;
		if (flag)
		{
			this.isOpenLiBiaoUI = true;
			MainUICon.inst.OnClickLiBaoBtn(null);
		}
	}

	// Token: 0x04001274 RID: 4724
	private static LiBaoManager instance;

	// Token: 0x04001275 RID: 4725
	private ArrayList m_szShouChong;

	// Token: 0x04001276 RID: 4726
	private int m_nChongZhiNum;

	// Token: 0x04001277 RID: 4727
	private int m_nLianXuNum;

	// Token: 0x04001278 RID: 4728
	private ArrayList m_szLianXu;

	// Token: 0x04001279 RID: 4729
	public bool ClickState;

	// Token: 0x0400127A RID: 4730
	private ArrayList m_szLvReward = new ArrayList();

	// Token: 0x0400127B RID: 4731
	private ArrayList m_szUserLvReward;

	// Token: 0x0400127C RID: 4732
	private ArrayList m_szSignData;

	// Token: 0x0400127D RID: 4733
	private ArrayList m_szSignReward;

	// Token: 0x0400127E RID: 4734
	private int m_nSignAllNum;

	// Token: 0x0400127F RID: 4735
	public int m_nCDK_Num;

	// Token: 0x04001280 RID: 4736
	private ArrayList m_szRepairSignData;

	// Token: 0x04001281 RID: 4737
	private DateTime m_dDayOnLoginDate = DateTime.MinValue;

	// Token: 0x04001282 RID: 4738
	private int m_nDayOnlineTime;

	// Token: 0x04001283 RID: 4739
	private ArrayList m_szOnlineReward = new ArrayList();

	// Token: 0x04001284 RID: 4740
	private ArrayList m_szOnTimeReward = new ArrayList();

	// Token: 0x04001285 RID: 4741
	private DateTime m_dSkyActDate = DateTime.MinValue;

	// Token: 0x04001286 RID: 4742
	private int m_nSkyActNum;

	// Token: 0x04001287 RID: 4743
	private ArrayList m_szSkyActReward = new ArrayList();

	// Token: 0x04001288 RID: 4744
	private ArrayList m_szWeekLoginReward = new ArrayList();

	// Token: 0x04001289 RID: 4745
	private ArrayList m_szSkyEveryDay = new ArrayList();

	// Token: 0x0400128A RID: 4746
	private ArrayList m_szNewSoldierReward = new ArrayList();

	// Token: 0x0400128B RID: 4747
	private ArrayList m_szWeekendOnlineReward = new ArrayList();

	// Token: 0x0400128C RID: 4748
	private ArrayList m_szKaiXueGiftReward = new ArrayList();

	// Token: 0x0400128D RID: 4749
	private ArrayList m_szMatchReward = new ArrayList();

	// Token: 0x0400128E RID: 4750
	public bool isGoddessGiftClick;

	// Token: 0x0400128F RID: 4751
	private ArrayList m_szGoddessGiftReward = new ArrayList();

	// Token: 0x04001290 RID: 4752
	public bool isWeekChargeClick;

	// Token: 0x04001291 RID: 4753
	private ArrayList m_szWeekChargeReward = new ArrayList();

	// Token: 0x04001292 RID: 4754
	public ArrayList m_szLuckyDrawData = new ArrayList();

	// Token: 0x04001293 RID: 4755
	public ArrayList m_szBOGOActData = new ArrayList();

	// Token: 0x04001294 RID: 4756
	private ArrayList m_szRechargeZuanData = new ArrayList();

	// Token: 0x04001295 RID: 4757
	private ArrayList m_szRechargeItemData = new ArrayList();

	// Token: 0x04001296 RID: 4758
	private ArrayList m_szNaMatchReward = new ArrayList();

	// Token: 0x04001297 RID: 4759
	public bool isDiscountItemClick;

	// Token: 0x04001298 RID: 4760
	private ArrayList m_szDiscountItemData = new ArrayList();

	// Token: 0x04001299 RID: 4761
	public bool isHeroLimitBuyClick;

	// Token: 0x0400129A RID: 4762
	public bool isHeroReturnClick;

	// Token: 0x0400129B RID: 4763
	public bool isDiscountShopClick;

	// Token: 0x0400129C RID: 4764
	private ArrayList m_szHuiliuActData = new ArrayList();

	// Token: 0x0400129D RID: 4765
	public bool isTuijianClick;

	// Token: 0x0400129E RID: 4766
	public static bool isWaiGuaOpen = true;

	// Token: 0x0400129F RID: 4767
	public string sWaiGuaBg = "libao_waigua_ditu";

	// Token: 0x040012A0 RID: 4768
	public bool isWaiGuaClick;

	// Token: 0x040012A1 RID: 4769
	private ArrayList m_szPvpEveryWeek = new ArrayList();

	// Token: 0x040012A2 RID: 4770
	public bool isPvpWeekClick;

	// Token: 0x040012A3 RID: 4771
	public bool isDoubleExpClick;

	// Token: 0x040012A4 RID: 4772
	public ArrayList m_szMultiBOGOData = new ArrayList();

	// Token: 0x040012A5 RID: 4773
	public bool isMultiBOGOClick;

	// Token: 0x040012A6 RID: 4774
	public ArrayList m_szJieriLoginData = new ArrayList();

	// Token: 0x040012A7 RID: 4775
	public ArrayList m_szWinterActData = new ArrayList();

	// Token: 0x040012A8 RID: 4776
	private bool isWinterActClick;

	// Token: 0x040012A9 RID: 4777
	public bool isSkinDiscountClick;

	// Token: 0x040012AA RID: 4778
	private ArrayList m_szJieRiOnline = new ArrayList();

	// Token: 0x040012AB RID: 4779
	public bool isJieRiOnlineClick;

	// Token: 0x040012AC RID: 4780
	public bool isOpenLiBiaoUI = true;

	// Token: 0x040012AD RID: 4781
	private int nAddGoldNum;

	// Token: 0x040012AE RID: 4782
	private int nMatchNum;
}

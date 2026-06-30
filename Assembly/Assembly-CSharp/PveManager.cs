using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AD RID: 685
public class PveManager
{
	// Token: 0x06001F60 RID: 8032 RVA: 0x001200EC File Offset: 0x0011E2EC
	public PveManager()
	{
		this.StrengthenData = new StrengthenDataMgr();
		this.mPveDataMgr = new PveDataMgr();
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x00120178 File Offset: 0x0011E378
	public static PveManager GetInstance()
	{
		bool flag = PveManager.mInst == null;
		if (flag)
		{
			PveManager.mInst = new PveManager();
		}
		return PveManager.mInst;
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x001201A8 File Offset: 0x0011E3A8
	public ArrayList GetPveDataByFbType(int nType)
	{
		return this.mPveDataMgr.GetData(nType);
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x001201C6 File Offset: 0x0011E3C6
	public void SetPveDataByFbType(int nType, ArrayList arr)
	{
		this.mPveDataMgr.SetData(nType, arr);
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x001201D8 File Offset: 0x0011E3D8
	public void OnPve_NetMsg(int nNetID, ArrayList szMsg)
	{
		switch (nNetID)
		{
		case 19501:
			this.m_nCurFbType = (int)szMsg[5];
			this.SetPveDataByFbType(this.m_nCurFbType, (ArrayList)szMsg[0]);
			this.m_szDropItemList = (ArrayList)szMsg[2];
			this.m_nWin = (int)szMsg[3];
			this.m_nRound = (((int)szMsg[4] <= 0) ? 1 : ((int)szMsg[4]));
			this.m_szFanPaiData = (ArrayList)szMsg[6];
			this.m_szFirstReward = (ArrayList)szMsg[7];
			break;
		case 19502:
			this.OnQinagHua(szMsg);
			break;
		case 19503:
		{
			bool flag = (int)szMsg[0] == 1 && fGameManager.m_nUserType == 1;
			if (flag)
			{
				fGameManager.SendMsgToServer(19000, new ArrayList
				{
					19010,
					new ArrayList
					{
						0,
						0
					}
				});
			}
			break;
		}
		case 19504:
			this.OnJinJie(szMsg);
			break;
		case 19507:
			this.OnFanPai(szMsg);
			break;
		case 19511:
		{
			PlayerDataManager.getInstance().HUserData.m_nEnergy = (int)szMsg[0];
			bool flag2 = PveUICon.inst != null;
			if (flag2)
			{
				PveUICon.inst.UpdateTiLi();
			}
			break;
		}
		case 19512:
			switch ((int)szMsg[0])
			{
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26028, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -4:
			{
				FbXmlData fbdata = fGameManager.mloadConfig.FbData[(int)szMsg[1]];
				PveUICon.inst.OnCheckZdl(fbdata, 26064);
				break;
			}
			case -3:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26026, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -2:
			case -1:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26025, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			default:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			}
			MatchManager.GetInstance().m_nIsMatch = 0;
			MatchManager.GetInstance().m_nMatchFbType = 0;
			break;
		case 19513:
		{
			PveManager.GetInstance().SetPveDataByFbType(9, szMsg);
			bool flag3 = PveUICon.inst != null;
			if (flag3)
			{
				PveUICon.inst.UpdateByTiliNum();
			}
			break;
		}
		case 19514:
			this.OnSaoDangData(szMsg);
			break;
		case 19515:
			this.m_nOpenLv = (int)szMsg[0];
			this.OnSetMaoXianBtnState();
			break;
		case 19516:
			this.SetPveDataByFbType((int)szMsg[1], (ArrayList)szMsg[0]);
			break;
		case 19517:
			this.StrengthenData.GeneUpdateLose(szMsg);
			break;
		case 19518:
			this.StrengthenData.SetGeneData((ArrayList)szMsg[0]);
			this.mPveDataMgr.SetData(9, (ArrayList)szMsg[1]);
			break;
		case 19520:
		{
			this.m_szPveFirstReward = szMsg;
			bool flag4 = MainUICon.inst != null;
			if (flag4)
			{
				MainUICon.inst.OnSetMaoXianBtnState();
			}
			break;
		}
		case 19521:
			this.UpdatePveFlower(szMsg);
			break;
		}
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x00120670 File Offset: 0x0011E870
	public void OnCheckCanLevelUp()
	{
		int num = 8;
		this.IsShowTanHao = false;
		for (int i = 1; i < 4; i++)
		{
			int num2 = i;
			QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[num2];
			for (int j = 1; j < num; j++)
			{
				int num3 = j;
				QiangHuaAttributeData qiangHuaAttributeData = qiangHuaHeroTypeXmlData.AllAttributeData[num3];
				HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(num2, num3);
				bool flag = attDataByType.m_nJieLv >= qiangHuaHeroTypeXmlData.nMaxJie;
				if (!flag)
				{
					bool flag2 = attDataByType.m_nQhLv >= qiangHuaAttributeData.nMaxLv;
					int nItemId;
					int num4;
					if (flag2)
					{
						ArrayList szJinJieRand = qiangHuaHeroTypeXmlData.szJinJieRand;
						ArrayList arrayList = (ArrayList)szJinJieRand[attDataByType.m_nJieLv];
						bool flag3 = attDataByType.m_nJieLv >= qiangHuaHeroTypeXmlData.nMaxJie;
						if (flag3)
						{
							goto IL_146;
						}
						nItemId = (int)arrayList[2];
						num4 = this.GetUpJieItemNum(attDataByType, qiangHuaAttributeData, qiangHuaHeroTypeXmlData);
					}
					else
					{
						nItemId = qiangHuaAttributeData.m_nItem;
						num4 = this.GetUpQinaghuaItemNum(attDataByType, qiangHuaAttributeData, qiangHuaHeroTypeXmlData);
					}
					int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(nItemId);
					bool flag4 = haveItemNum < num4;
					if (!flag4)
					{
						this.IsShowTanHao = true;
						this.m_szTanHaoState.Add(num2);
						break;
					}
				}
				IL_146:;
			}
		}
		bool flag5 = !this.IsShowTanHao;
		if (flag5)
		{
			this.IsShowTanHao = this.StrengthenData.ShowGeneTanhao();
		}
		this.OnShowQinagHuaTanHao(this.IsShowTanHao);
		bool flag6 = QiangHuaUICon.inst != null;
		if (flag6)
		{
			QiangHuaUICon.inst.OnShowTanHao();
		}
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x00120834 File Offset: 0x0011EA34
	public void OnShowQinagHuaTanHao(bool isShow)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnShowQinagHuaTanHao(isShow);
		}
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x00120860 File Offset: 0x0011EA60
	public void OnSaoDangData(ArrayList szMsg)
	{
		this.m_nCurFbType = (int)szMsg[5];
		PveManager.GetInstance().SetPveDataByFbType(this.m_nCurFbType, (ArrayList)szMsg[0]);
		this.m_szDropItemList.Clear();
		ArrayList arrayList = (ArrayList)szMsg[2];
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i];
			int num = this.m_szDropItemList.IndexOf((int)arrayList2[0]);
			bool flag = num == -1;
			if (flag)
			{
				this.m_szDropItemList.Add((int)arrayList2[0]);
				this.m_szDropItemList.Add((int)arrayList2[1]);
			}
			else
			{
				int num2 = (int)this.m_szDropItemList[num + 1];
				num2 += (int)arrayList2[1];
				this.m_szDropItemList[num + 1] = num2;
			}
		}
		this.m_nWin = (int)szMsg[3];
		this.m_nRound = (((int)szMsg[4] <= 0) ? 1 : ((int)szMsg[4]));
		this.m_szFanPaiData = (ArrayList)szMsg[6];
		this.OnGameResult(new ArrayList
		{
			this.m_nWin,
			2
		});
		bool flag2 = PveUICon.inst != null;
		if (flag2)
		{
			PveUICon.inst.OnUpdateSaoDang(this.m_nCurFbType);
		}
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x00120A22 File Offset: 0x0011EC22
	public void OnSendMsgToServer(int nNetId, ArrayList szMsg)
	{
		fGameManager.SendMsgToHallServer(19500, new ArrayList
		{
			nNetId,
			szMsg
		});
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x00120A4C File Offset: 0x0011EC4C
	public void OnHallOk()
	{
		bool flag = this.m_szFanPaiData.Count > 5 && (int)this.m_szFanPaiData[2] != 0 && (int)this.m_szFanPaiData[1] != 0;
		if (flag)
		{
			this.OnSendMsgToServer(19508, new ArrayList
			{
				0,
				0
			});
			this.OnGameResult(new ArrayList
			{
				this.m_nWin,
				1
			});
			this.m_szFanPaiData[2] = 0;
			this.m_szFanPaiData[1] = 0;
		}
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x00120B14 File Offset: 0x0011ED14
	public void OnQinagHua(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[1];
		ArrayList arrayList = (ArrayList)szNetMsg[0];
		bool flag = szNetMsg.Count >= 3;
		if (flag)
		{
			this.mPveDataMgr.SetData(9, (ArrayList)szNetMsg[2]);
		}
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i];
			int nHeroType = (int)arrayList2[0];
			int nAttType = (int)arrayList2[1];
			ArrayList szData = (ArrayList)arrayList2[2];
			int num2 = (int)arrayList2[3];
			bool flag2 = num2 > 0;
			if (flag2)
			{
				HeroOneAttData userOneAttData = this.StrengthenData.UpdateHeroAttData(nHeroType, nAttType, szData);
				bool flag3 = QiangHuaUICon.inst != null;
				if (flag3)
				{
					QiangHuaUICon.inst.OnUpdateValue(userOneAttData);
				}
			}
			else
			{
				bool flag4 = QiangHuaUICon.inst != null;
				if (flag4)
				{
					QiangHuaUICon.inst.UpdateQhResult(nHeroType, nAttType, num2);
				}
			}
		}
		bool flag5 = num > 0;
		if (flag5)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(27016, new object[]
			{
				num
			}), "ffdd00", "system");
		}
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x00120C78 File Offset: 0x0011EE78
	public void OnJinJie(ArrayList szData)
	{
		this.OnQinagHua(szData);
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x00120C83 File Offset: 0x0011EE83
	public void OnGameResult(ArrayList szMsg)
	{
		fUIManager.GetInstance().ShowWin("KingJieSuanUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), szMsg, true);
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x00120CA8 File Offset: 0x0011EEA8
	private void OnLoadContentUIOk(string sUI, GameObject ob, object szParam)
	{
		ArrayList szData = (ArrayList)szParam;
		bool flag = KingJieSuanUICon.inst != null;
		if (flag)
		{
			KingJieSuanUICon.inst.OnSetData(szData);
		}
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x00120CDC File Offset: 0x0011EEDC
	public void OnFanPai(ArrayList szMsg)
	{
		this.m_szFanPaiData = (ArrayList)szMsg[1];
		bool flag = KingJieSuanUICon.inst != null;
		if (flag)
		{
			KingJieSuanUICon.inst.OnFanPai((ArrayList)szMsg[0], (int)szMsg[2]);
		}
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x00120D30 File Offset: 0x0011EF30
	public ArrayList GetAutoAllXiaoHao(int nHeroType, bool isCheck_1, bool isCheck_2)
	{
		int num = 0;
		int num2 = 8;
		int num3 = 0;
		int num4 = 0;
		this.m_ngameCurrency = PlayerDataManager.getInstance().playerdet.gameCurrency;
		this.m_nchargeCurrency = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[nHeroType];
		for (int i = 1; i < num2; i++)
		{
			int num5 = i;
			QiangHuaAttributeData qiangHuaAttributeData = qiangHuaHeroTypeXmlData.AllAttributeData[num5];
			HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(nHeroType, num5);
			bool flag = attDataByType.m_nJieLv < qiangHuaHeroTypeXmlData.nMaxJie && attDataByType.m_nQhLv < qiangHuaAttributeData.nMaxLv;
			if (flag)
			{
				int nItem = qiangHuaAttributeData.m_nItem;
				int upQinaghuaItemNum = this.GetUpQinaghuaItemNum(attDataByType, qiangHuaAttributeData, qiangHuaHeroTypeXmlData);
				int nLvType = 1;
				num4++;
				ArrayList arrayList = this.OnXiaoHaoCaiLiao(nItem, upQinaghuaItemNum, isCheck_1, isCheck_2, nLvType, false, 0);
				int num6 = (int)arrayList[0];
				int num7 = (int)arrayList[1];
				bool flag2 = num6 > 0;
				if (flag2)
				{
					num += num7;
				}
				else
				{
					num3++;
				}
			}
		}
		return new ArrayList
		{
			num3,
			num,
			num4
		};
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x00120EA8 File Offset: 0x0011F0A8
	public ArrayList OnXiaoHaoCaiLiao(int nItemId, int nItemNum, bool nIsAuto, bool nIsAutoRandom, int nLvType, bool isOne = true, int cglNum = 0)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (isOne)
		{
			this.m_ngameCurrency = PlayerDataManager.getInstance().playerdet.gameCurrency;
			this.m_nchargeCurrency = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		}
		bool flag = !fGameManager.mloadConfig.AllItemData.ContainsKey(nItemId);
		ArrayList result;
		if (flag)
		{
			result = new ArrayList
			{
				-10,
				0,
				0
			};
		}
		else
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[nItemId];
			int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(nItemId);
			bool flag2 = haveItemNum >= nItemNum;
			int num5;
			if (flag2)
			{
				num5 = nItemNum;
			}
			else
			{
				bool flag3 = !nIsAuto;
				if (flag3)
				{
					return new ArrayList
					{
						-3,
						0,
						0
					};
				}
				num5 = haveItemNum;
				num = itemData.m_nDiamondPrice * (nItemNum - haveItemNum);
				num2 = itemData.m_nGlodPrice * (nItemNum - haveItemNum);
			}
			bool flag4 = nLvType == 2 && nIsAutoRandom;
			if (flag4)
			{
				num3 = ShopManager.GetInstance().GetHaveItemNum(this.m_nDaoJuItemID);
				bool flag5 = cglNum > 0;
				if (flag5)
				{
					bool flag6 = num3 >= cglNum;
					if (flag6)
					{
						num4 = cglNum;
					}
					else
					{
						num4 = num3;
						bool flag7 = !nIsAuto;
						if (flag7)
						{
							return new ArrayList
							{
								-7,
								0,
								0
							};
						}
						ItemData itemData2 = fGameManager.mloadConfig.AllItemData[this.m_nDaoJuItemID];
						num += itemData.m_nDiamondPrice;
						num2 += itemData.m_nGlodPrice;
					}
				}
			}
			bool flag8 = this.SubMoney(1, num2) != 1;
			if (flag8)
			{
				bool flag9 = this.SubMoney(2, num) != 1;
				if (flag9)
				{
					return new ArrayList
					{
						-4,
						num
					};
				}
			}
			else
			{
				num = 0;
			}
			bool flag10 = num3 < num4;
			if (flag10)
			{
				result = new ArrayList
				{
					-6,
					0
				};
			}
			else
			{
				bool flag11 = haveItemNum < num5;
				if (flag11)
				{
					result = new ArrayList
					{
						-5,
						0
					};
				}
				else
				{
					result = new ArrayList
					{
						1,
						num
					};
				}
			}
		}
		return result;
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x00121194 File Offset: 0x0011F394
	private int SubMoney(int nType, int nMoney)
	{
		bool flag = nMoney < 0;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			if (nType != 1)
			{
				if (nType == 2)
				{
					bool flag2 = this.m_nchargeCurrency >= nMoney;
					if (!flag2)
					{
						return -2;
					}
					this.m_nchargeCurrency -= nMoney;
				}
			}
			else
			{
				bool flag3 = this.m_ngameCurrency >= nMoney;
				if (!flag3)
				{
					return -2;
				}
				this.m_ngameCurrency -= nMoney;
			}
			result = 1;
		}
		return result;
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x00121214 File Offset: 0x0011F414
	public int GetUpJieItemNum(HeroOneAttData UserOneAttData, QiangHuaAttributeData attdata, QiangHuaHeroTypeXmlData HeroTypeXmlData)
	{
		ArrayList szJinJieRand = HeroTypeXmlData.szJinJieRand;
		ArrayList arrayList = (ArrayList)szJinJieRand[UserOneAttData.m_nJieLv];
		return (int)arrayList[3];
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x0012124C File Offset: 0x0011F44C
	public int GetUpQinaghuaItemNum(HeroOneAttData UserOneAttData, QiangHuaAttributeData attdata, QiangHuaHeroTypeXmlData HeroTypeXmlData)
	{
		return UserOneAttData.m_nJieLv + 1 + (int)((float)(UserOneAttData.m_nJieLv * UserOneAttData.m_nJieLv) / 12f);
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x0012127C File Offset: 0x0011F47C
	public int GetZhanDuLi(int nHeroType)
	{
		int num = 0;
		QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[nHeroType];
		foreach (QiangHuaAttributeData qiangHuaAttributeData in qiangHuaHeroTypeXmlData.AllAttributeData.Values)
		{
			bool flag = qiangHuaAttributeData != null;
			if (flag)
			{
				HeroOneAttData attDataByType = this.StrengthenData.GetAttDataByType(nHeroType, qiangHuaAttributeData.nType);
				int num2 = (int)this.GetZdlValue(attDataByType.m_nQhLv, attDataByType.m_nJieLv);
				num += num2;
			}
		}
		return num;
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x00121328 File Offset: 0x0011F528
	public float GetZdlValue(int m_nQhLv, int m_nJieLv)
	{
		return (float)((m_nQhLv + m_nJieLv * 11) * 100);
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x00121344 File Offset: 0x0011F544
	public ArrayList GetOtherZhanDuLi(int nHeroId, ArrayList szQhData)
	{
		int num = 1;
		int num2 = 0;
		foreach (QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData in fGameManager.mloadConfig.AllQiangHuaData.Values)
		{
			bool flag = qiangHuaHeroTypeXmlData == null || qiangHuaHeroTypeXmlData.szHeroId.IndexOf(nHeroId) == -1;
			if (!flag)
			{
				num = qiangHuaHeroTypeXmlData.nType;
				break;
			}
		}
		ArrayList arrayList = (ArrayList)szQhData[num];
		int count = arrayList.Count;
		for (int i = 0; i < count; i += 2)
		{
			HeroOneAttData heroOneAttData = new HeroOneAttData(num, (int)arrayList[i], (ArrayList)arrayList[i + 1]);
			int num3 = (int)this.GetZdlValue(heroOneAttData.m_nQhLv, heroOneAttData.m_nJieLv);
			num2 += num3;
		}
		return new ArrayList
		{
			num,
			num2
		};
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x0012146C File Offset: 0x0011F66C
	public float GetAttValue(int m_nQhLv, int m_nJieLv, QiangHuaAttributeData attdata)
	{
		return (float)(attdata.nBaseplus * (m_nQhLv + m_nJieLv * 10) + m_nJieLv * attdata.nJJBaseplus);
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x00121498 File Offset: 0x0011F698
	public ArrayList GetQiangHuaXiaoHaoItem(QiangHuaHeroTypeXmlData HeroTypeXmlData, HeroOneAttData UserOneAttData, QiangHuaAttributeData attdata)
	{
		bool flag = UserOneAttData.m_nQhLv == attdata.nMaxLv;
		int num;
		int num2;
		if (flag)
		{
			ArrayList szJinJieRand = HeroTypeXmlData.szJinJieRand;
			ArrayList arrayList = (ArrayList)szJinJieRand[UserOneAttData.m_nJieLv];
			num = (int)arrayList[2];
			bool flag2 = UserOneAttData.m_nJieLv >= HeroTypeXmlData.nMaxJie;
			if (flag2)
			{
				return new ArrayList
				{
					num,
					0
				};
			}
			num2 = this.GetUpJieItemNum(UserOneAttData, attdata, HeroTypeXmlData);
		}
		else
		{
			num = attdata.m_nItem;
			num2 = this.GetUpQinaghuaItemNum(UserOneAttData, attdata, HeroTypeXmlData);
		}
		return new ArrayList
		{
			num,
			num2
		};
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x0012156C File Offset: 0x0011F76C
	public void mousepos(GameObject obj, float xpos = 20f, float ypos = -20f)
	{
		Transform transform = obj.transform;
		Vector3 mousePosition = Input.mousePosition;
		RectTransform component = obj.GetComponent<RectTransform>();
		int num = Mathf.CeilToInt(component.sizeDelta.x);
		int num2 = Mathf.CeilToInt(component.sizeDelta.y);
		float screenWidth = SysConfig.ScreenWidth;
		float screenHeight = SysConfig.ScreenHeight;
		mousePosition.x = Mathf.Lerp(-0.5f * screenWidth, 0.5f * screenWidth, mousePosition.x / (float)Screen.width) + xpos;
		mousePosition.y = Mathf.Lerp(-0.5f * screenHeight, 0.5f * screenHeight, mousePosition.y / (float)Screen.height) + ypos;
		mousePosition.z = 0f;
		bool flag = mousePosition.x + (float)num > screenWidth / 2f;
		if (flag)
		{
			mousePosition.x = screenWidth / 2f - (float)num;
		}
		bool flag2 = mousePosition.y - (float)num2 < screenHeight / -2f;
		if (flag2)
		{
			mousePosition.y = (float)num2 - screenHeight / 2f;
		}
		mousePosition.x -= 130f;
		transform.localPosition = mousePosition;
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x00121698 File Offset: 0x0011F898
	public int GetTili(int nFblv)
	{
		return 10;
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x001216AC File Offset: 0x0011F8AC
	public ArrayList GetHeroList(int nHeroType)
	{
		QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[nHeroType];
		return qiangHuaHeroTypeXmlData.szHeroId;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x001216D8 File Offset: 0x0011F8D8
	public int GetQingHuaItem(int nType, QiangHuaHeroTypeXmlData HeroTypeXmlData)
	{
		QiangHuaAttributeData qiangHuaAttributeData = HeroTypeXmlData.AllAttributeData[nType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(HeroTypeXmlData.nType, nType);
		bool flag = attDataByType.m_nJieLv >= HeroTypeXmlData.nMaxJie;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = attDataByType.m_nQhLv == qiangHuaAttributeData.nMaxLv;
			if (flag2)
			{
				ArrayList szJinJieRand = HeroTypeXmlData.szJinJieRand;
				ArrayList arrayList = (ArrayList)szJinJieRand[attDataByType.m_nJieLv];
				bool flag3 = attDataByType.m_nJieLv >= HeroTypeXmlData.nMaxJie;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					int num = (int)arrayList[2];
					result = this.GetUpJieItemNum(attDataByType, qiangHuaAttributeData, HeroTypeXmlData);
				}
			}
			else
			{
				int num = qiangHuaAttributeData.m_nItem;
				result = this.GetUpQinaghuaItemNum(attDataByType, qiangHuaAttributeData, HeroTypeXmlData);
			}
		}
		return result;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x001217A4 File Offset: 0x0011F9A4
	public string OnShowValue(int nType, float addValue)
	{
		string empty = string.Empty;
		if (nType != 3)
		{
			if (nType == 4)
			{
				return (addValue / 5f).ToString("0.0");
			}
			if (nType != 11)
			{
				return addValue.ToString("0");
			}
		}
		return (addValue / 100f).ToString("0.0") + "%";
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x0012181C File Offset: 0x0011FA1C
	public void OnSetMaoXianBtnState()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnSetMaoXianBtnState();
		}
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x00121848 File Offset: 0x0011FA48
	public void OnDelPveRefresh()
	{
		bool flag = GameRoot.inst != null;
		if (flag)
		{
			GameRoot.inst.OnDelPveRefresh();
		}
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00121874 File Offset: 0x0011FA74
	public PveXmlData GetPveData()
	{
		bool flag = !fGameManager.mloadConfig.AllPveFbData.ContainsKey(1);
		PveXmlData result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = fGameManager.mloadConfig.AllPveFbData[1];
		}
		return result;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x001218B4 File Offset: 0x0011FAB4
	public int GetSaoDangNum(int nFbType)
	{
		ArrayList pveDataByFbType = PveManager.GetInstance().GetPveDataByFbType(nFbType);
		int result = (int)pveDataByFbType[12];
		DateTime dateTime = DateTime.Parse(pveDataByFbType[11].ToString());
		bool flag = Fun.GetServerDate().Date != dateTime.Date;
		if (flag)
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00121918 File Offset: 0x0011FB18
	public bool IsPveOpen()
	{
		bool flag = !Fun.IsFunctionOpen("IsPveOpen", null);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			PveXmlData pveData = PveManager.GetInstance().GetPveData();
			bool flag2 = pveData == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nLevel < this.m_nOpenLv;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27046, new object[]
					{
						this.m_nOpenLv
					}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					result = false;
				}
				else
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x001219D8 File Offset: 0x0011FBD8
	public bool IsOpen()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel < this.m_nOpenLv;
		bool result;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27046, new object[]
			{
				this.m_nOpenLv
			}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x00121A64 File Offset: 0x0011FC64
	public void OnPveOk(int nType, int nFbLv, int nFbId, int nCheck)
	{
		bool flag = !fGameManager.mloadConfig.FbData.ContainsKey(nFbId);
		if (!flag)
		{
			FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[nFbId];
			bool flag2 = !fGameManager.mloadConfig.FbModeData.ContainsKey(fbXmlData.nstartplotid);
			if (!flag2)
			{
				FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
				bool flag3 = TeamManager.GetInstance().m_nTeamID == 0;
				if (flag3)
				{
					int num = (int)PveManager.GetInstance().GetPveDataByFbType(9)[6];
					bool flag4 = num > fbModelXmlData.m_nZdl * 2;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26068, new object[0]), Language.GetLang(26069, new object[0]), Language.GetLang(26070, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.ConfirmPveMatch), new ArrayList
						{
							nType,
							nFbLv,
							nFbId,
							nCheck
						}, null, null);
						return;
					}
				}
				this.ConfirmPveMatch(new ArrayList
				{
					nType,
					nFbLv,
					nFbId,
					nCheck
				});
			}
		}
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x00121BFC File Offset: 0x0011FDFC
	private void ConfirmPveMatch(object szParam)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = (ArrayList)szParam;
		arrayList.Add(80002);
		arrayList.Add(3);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		arrayList.Add((int)arrayList2[0]);
		arrayList.Add((int)arrayList2[1]);
		arrayList.Add((int)arrayList2[2]);
		arrayList.Add((int)arrayList2[3]);
		fGameManager.SendMsgToHallServer(80001, arrayList);
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x00121CBC File Offset: 0x0011FEBC
	private void UpdatePveFlower(ArrayList szMsg)
	{
		int num = PlayerDataManager.getInstance().playerdet.m_nPveFlower;
		PlayerDataManager.getInstance().playerdet.m_nPveFlower = (int)szMsg[1];
		PlayerDataManager.getInstance().playerdet.m_nPveFlowerTime = (int)szMsg[2];
		num = PlayerDataManager.getInstance().playerdet.m_nPveFlower - num;
		bool flag = (int)szMsg[0] == -1;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(26072, new object[0]), "ffdd00", "system");
		}
	}

	// Token: 0x04001C9B RID: 7323
	public static PveManager mInst;

	// Token: 0x04001C9C RID: 7324
	public PveDataMgr mPveDataMgr;

	// Token: 0x04001C9D RID: 7325
	public StrengthenDataMgr StrengthenData;

	// Token: 0x04001C9E RID: 7326
	public int m_nOpenLv = 1;

	// Token: 0x04001C9F RID: 7327
	public float m_nMaxEnergy = 60f;

	// Token: 0x04001CA0 RID: 7328
	public ArrayList m_szDropItemList = new ArrayList();

	// Token: 0x04001CA1 RID: 7329
	public int m_nWin;

	// Token: 0x04001CA2 RID: 7330
	private int m_ngameCurrency;

	// Token: 0x04001CA3 RID: 7331
	private int m_nchargeCurrency;

	// Token: 0x04001CA4 RID: 7332
	public int m_nRound;

	// Token: 0x04001CA5 RID: 7333
	public int m_nFuHuoBiId = 6023;

	// Token: 0x04001CA6 RID: 7334
	public float nCdTime;

	// Token: 0x04001CA7 RID: 7335
	public bool IsShowTanHao;

	// Token: 0x04001CA8 RID: 7336
	public ArrayList m_szTanHaoState = new ArrayList();

	// Token: 0x04001CA9 RID: 7337
	public const int PVEMATCHTYPE = 3;

	// Token: 0x04001CAA RID: 7338
	public int m_nCurFbType;

	// Token: 0x04001CAB RID: 7339
	public ArrayList m_szFanPaiData = new ArrayList();

	// Token: 0x04001CAC RID: 7340
	public ArrayList m_szFirstReward = new ArrayList();

	// Token: 0x04001CAD RID: 7341
	public ArrayList m_szPveFirstReward = new ArrayList();

	// Token: 0x04001CAE RID: 7342
	private int m_nDaoJuItemID = 6022;
}

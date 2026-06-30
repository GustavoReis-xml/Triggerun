using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class ShopManager
{
	// Token: 0x060025ED RID: 9709 RVA: 0x00158854 File Offset: 0x00156A54
	public static ShopManager GetInstance()
	{
		bool flag = ShopManager.mInst == null;
		if (flag)
		{
			ShopManager.mInst = new ShopManager();
		}
		return ShopManager.mInst;
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x00158884 File Offset: 0x00156A84
	public void ShopServerMsgToClient(int nNetId, ArrayList szMsg)
	{
		switch (nNetId)
		{
		case 70001:
			return;
		case 70002:
			return;
		case 70004:
			return;
		case 70005:
		{
			PlayerDataManager.getInstance().HUserData.UpdateShopItem((ArrayList)szMsg[0]);
			this.UpdateLiBaoNum((int)szMsg[3]);
			bool flag = ShopUICon.inst != null;
			if (flag)
			{
				ShopUICon.inst.BuJiCon.UpdateShowLiBaoItem((int)szMsg[1], (int)szMsg[2]);
			}
			return;
		}
		case 70006:
		{
			bool flag2 = ShopUICon.inst != null;
			if (flag2)
			{
				ShopUICon.inst.BuJiCon.OnLiBaoOpen((ArrayList)szMsg[1], (int)szMsg[2]);
			}
			return;
		}
		case 70007:
		{
			PlayerDataManager.getInstance().HUserData.UpdateItemBuff((ArrayList)szMsg[0]);
			bool flag3 = MainUICon.inst != null;
			if (flag3)
			{
				MainUICon.inst.UpdateItemBuff((int)szMsg[1]);
			}
			return;
		}
		case 70008:
			this.OnHuiGouData(szMsg);
			return;
		case 70009:
			this.OnBuyItemOk(szMsg);
			return;
		case 70010:
		{
			bool flag4 = ShopUICon.inst != null;
			if (flag4)
			{
				ShopUICon.inst.BuJiCon.OnLiBaoOpenAll((ArrayList)szMsg[1]);
			}
			return;
		}
		case 70012:
			this.OnGiveUserItemData(szMsg);
			return;
		case 70018:
			this.UpdateUserItemData(szMsg);
			return;
		}
		Debug.Log("ShopServerMsgToClient= " + nNetId.ToString());
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x00158A68 File Offset: 0x00156C68
	public void UpdateUserItemData(ArrayList szItem)
	{
		int num = (int)szItem[0];
		int num2 = (int)szItem[2];
		int num3 = PlayerDataManager.getInstance().HUserData.m_szItemData.IndexOf(num.ToString());
		bool flag = num3 == -1;
		if (flag)
		{
			PlayerDataManager.getInstance().HUserData.m_szItemData.Add(num.ToString());
			PlayerDataManager.getInstance().HUserData.m_szItemData.Add((int)szItem[1]);
		}
		else
		{
			PlayerDataManager.getInstance().HUserData.m_szItemData[num3 + 1] = (int)szItem[1];
		}
		bool flag2 = QiangHuaUICon.inst != null;
		if (flag2)
		{
			QiangHuaUICon.inst.UpdateDaoJuNum(num);
		}
		bool flag3 = num2 == 1;
		if (flag3)
		{
			PveManager.GetInstance().OnCheckCanLevelUp();
		}
		bool flag4 = PveUICon.inst != null;
		if (flag4)
		{
			PveUICon.inst.ShowCaiLiaoNum(num);
		}
		bool flag5 = LuckyDrawUICon.inst != null && LuckyDrawUICon.inst.visible;
		if (flag5)
		{
			LuckyDrawUICon.inst.ResetUIData(0);
		}
		bool flag6 = ExchangeUICon.inst != null && ExchangeUICon.inst.visible;
		if (flag6)
		{
			ExchangeUICon.inst.SetGridList();
		}
		bool flag7 = WinterActUICon.inst != null && WinterActUICon.inst.visible;
		if (flag7)
		{
			WinterActUICon.inst.InitUI();
		}
		bool flag8 = HeroWeaponCon.inst != null && num2 == 1;
		if (flag8)
		{
			HeroWeaponCon.inst.ResetLvlUpTab(0);
			HeroWeaponCon.inst.ResetRemoveTab();
		}
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x00158C38 File Offset: 0x00156E38
	public int GetHaveItemNum(int nItemId)
	{
		int num = PlayerDataManager.getInstance().HUserData.m_szItemData.IndexOf(nItemId.ToString());
		bool flag = num == -1;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = (int)PlayerDataManager.getInstance().HUserData.m_szItemData[num + 1];
		}
		return result;
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x00158C90 File Offset: 0x00156E90
	public int GetItemNumById(int nItemId)
	{
		int num = PlayerDataManager.getInstance().HUserData.m_szItemData.IndexOf(nItemId.ToString());
		bool flag = num == -1;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = (int)PlayerDataManager.getInstance().HUserData.m_szItemData[num + 1];
		}
		return result;
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x00158CE8 File Offset: 0x00156EE8
	public void OnGiveUserItemData(ArrayList szMsg)
	{
		ArrayList arrayList = new ArrayList();
		int num = (int)szMsg[0];
		bool flag = num == -1;
		if (!flag)
		{
			int num2 = (int)szMsg[1];
			int num3 = (int)szMsg[2];
			ArrayList arrayList2 = (ArrayList)szMsg[4];
			string text = Fun.DecodingUTF8(szMsg[5].ToString());
			ItemData itemData = fGameManager.mloadConfig.AllItemData[num2];
			int count = arrayList2.Count;
			int num4 = num;
			int num5 = num4;
			if (num5 != 1)
			{
				if (num5 == 2)
				{
					bool flag2 = itemData.m_nType != 20;
					if (flag2)
					{
						bool flag3 = itemData.m_nType >= 3 && itemData.m_nType <= 8;
						if (flag3)
						{
							bool flag4 = arrayList2.Count == 0;
							if (flag4)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39538, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39539, new object[]
								{
									text,
									itemData.m_sName
								}), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
							}
						}
						else
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39538, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
						}
					}
					else
					{
						bool flag5 = count == 0;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39538, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
						}
						else
						{
							bool flag6 = count / 2 == itemData.m_szChildItem.Count;
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39539, new object[]
								{
									text,
									itemData.m_sName
								}), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
							}
							else
							{
								count = itemData.m_szChildItem.Count;
								for (int i = 0; i < count; i++)
								{
									ArrayList arrayList3 = (ArrayList)itemData.m_szChildItem[i];
									int key = (int)arrayList3[0];
									bool flag7 = fGameManager.mloadConfig.AllItemData.ContainsKey(key);
									if (flag7)
									{
										ItemData itemData2 = fGameManager.mloadConfig.AllItemData[key];
										bool flag8 = arrayList2.IndexOf(key.ToString()) == -1;
										if (flag8)
										{
											TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39541, new object[]
											{
												itemData2.m_sName
											}), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
										}
										else
										{
											TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39539, new object[]
											{
												text,
												itemData2.m_sName
											}), Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag9 = itemData.m_nType != 20 || count == 0;
				if (flag9)
				{
					bool flag10 = itemData.m_nType >= 3 && itemData.m_nType <= 8;
					if (flag10)
					{
						TipManager.GetInstance().ShowRewardTip(6, new ArrayList
						{
							itemData.m_nHeroId,
							ShopManager.GetHeroEquipType(itemData.m_nType),
							itemData.m_nTypeId,
							Fun.DecodingUTF8(szMsg[3].ToString())
						}, null, null);
					}
					else
					{
						TipManager.GetInstance().ShowItemTip(Language.GetLang(8009, new object[0]), Language.GetLang(39540, new object[]
						{
							Fun.DecodingUTF8(szMsg[3].ToString()),
							itemData.m_sName + "*" + num3.ToString()
						}), new ArrayList
						{
							num2,
							num3
						}, Language.GetLang(8012, new object[0]), null, null);
					}
				}
				else
				{
					count = itemData.m_szChildItem.Count;
					for (int j = 0; j < count; j++)
					{
						ArrayList arrayList4 = (ArrayList)itemData.m_szChildItem[j];
						int num6 = (int)arrayList4[0];
						bool flag11 = fGameManager.mloadConfig.AllItemData.ContainsKey(num6) && arrayList2.IndexOf(num6.ToString()) == -1;
						if (flag11)
						{
							ItemData itemData3 = fGameManager.mloadConfig.AllItemData[num6];
							bool flag12 = itemData3.m_nType >= 3 && itemData3.m_nType <= 8;
							if (flag12)
							{
								TipManager.GetInstance().ShowRewardTip(6, new ArrayList
								{
									itemData3.m_nHeroId,
									ShopManager.GetHeroEquipType(itemData3.m_nType),
									itemData3.m_nTypeId,
									Fun.DecodingUTF8(szMsg[3].ToString())
								}, null, null);
							}
							else
							{
								arrayList.Add(num6);
								arrayList.Add((int)arrayList4[3]);
							}
						}
					}
					bool flag13 = arrayList.Count > 0;
					if (flag13)
					{
						TipManager.GetInstance().ShowItemTip(Language.GetLang(8009, new object[0]), Language.GetLang(39540, new object[]
						{
							Fun.DecodingUTF8(szMsg[3].ToString()),
							itemData.m_sName + "*" + num3.ToString()
						}), arrayList, Language.GetLang(8012, new object[0]), null, null);
					}
				}
			}
		}
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x00159478 File Offset: 0x00157678
	private void OnBuyItemOk(ArrayList arr)
	{
		int num = (int)arr[0];
		int num2 = (int)arr[1];
		int num3 = (int)arr[2];
		int num4 = (int)arr[3];
		bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(num);
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[num];
			bool flag2 = num3 == 0;
			int num5;
			if (flag2)
			{
				num5 = num2;
			}
			else
			{
				num5 = num2 - num3;
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39535, new object[]
				{
					itemData.m_sName,
					num4
				}), ChatColor.system, "system");
			}
			bool flag3 = num5 > 0;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(string.Concat(new string[]
				{
					Language.GetLang(46069, new object[0]),
					"[",
					itemData.m_sName,
					"]*",
					num2.ToString()
				}), ChatColor.system, "system");
				bool flag4 = num == PveManager.GetInstance().m_nFuHuoBiId;
				if (flag4)
				{
					KingTrainManager.GetInstance().OnUpdateFuHuoBi();
				}
			}
		}
		bool flag5 = ShopBuyItemUICon.inst != null;
		if (flag5)
		{
			ShopBuyItemUICon.inst.OnCloseClick(null);
		}
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x001595DC File Offset: 0x001577DC
	public int InitLiBaoNum()
	{
		this.nLiBaoNum = 0;
		int count = PlayerDataManager.getInstance().HUserData.m_szShopItemData.Count;
		for (int i = 0; i < count; i += 2)
		{
			this.nLiBaoNum += (int)PlayerDataManager.getInstance().HUserData.m_szShopItemData[i + 1];
		}
		return this.nLiBaoNum;
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x00159650 File Offset: 0x00157850
	public void UpdateLiBaoNum(int nNum)
	{
		this.nLiBaoNum += nNum;
		bool flag = ShopUICon.inst != null;
		if (flag)
		{
			ShopUICon.inst.UpdateLibaoNum();
		}
		bool flag2 = !(MainUICon.inst == null);
		if (flag2)
		{
			MainUICon.inst.ShowLiBaoNum(this.nLiBaoNum);
		}
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x001596B0 File Offset: 0x001578B0
	private void OnHuiGouData(ArrayList szData)
	{
		for (int i = 0; i < this.szEffOpenShunXu.Length; i++)
		{
			bool flag = !this.m_szHuiGouData.ContainsKey(this.szEffOpenShunXu[i]);
			if (flag)
			{
				this.m_szHuiGouData.Add(this.szEffOpenShunXu[i], szData);
				break;
			}
		}
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x0015970C File Offset: 0x0015790C
	public ArrayList GetHuiGouInfo(int nId)
	{
		bool flag = !this.m_szHuiGouData.ContainsKey(nId);
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.m_szHuiGouData[nId];
		}
		return result;
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x00159744 File Offset: 0x00157944
	public static void ShopClientToServer(int nNetId, ArrayList szMsg)
	{
		fGameManager.SendMsgToHallServer(70000, new ArrayList
		{
			nNetId,
			szMsg
		});
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x0015976C File Offset: 0x0015796C
	public void OnPenShopWin(int nType = 1)
	{
		MainUICon.inst.ChangeTitleToIdx(2, null);
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x0015977C File Offset: 0x0015797C
	private void LoadUiOk(string sUI, GameObject ob, object szParam)
	{
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x00159780 File Offset: 0x00157980
	public void OnBuyItem(int nItemId)
	{
		bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(nItemId);
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[nItemId];
			int nShopId = itemData.m_nShopId;
			bool flag2 = !fGameManager.mloadConfig.AllShopData.ContainsKey(nShopId);
			if (flag2)
			{
				Debug.LogError("商城没没有物品，找策划配");
			}
			else
			{
				ShopData pShopData = fGameManager.mloadConfig.AllShopData[nShopId];
				ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
			}
		}
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x00159804 File Offset: 0x00157A04
	public void OnBuyShopItem(int nShopId)
	{
		bool flag = !fGameManager.mloadConfig.AllShopData.ContainsKey(nShopId);
		if (flag)
		{
			Debug.LogError("商城没没有物品，找策划配");
		}
		else
		{
			ShopData pShopData = fGameManager.mloadConfig.AllShopData[nShopId];
			ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
		}
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x00159858 File Offset: 0x00157A58
	public void OpenBuyItemWin(int nType, ShopData pShopData)
	{
		bool flag = pShopData.m_nIsLimit == 1;
		if (flag)
		{
			int num = ShopItemPanelCon.isItemLimit(pShopData);
			bool flag2 = num <= 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39511, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			}
		}
		bool flag3 = nType == 2;
		if (flag3)
		{
			fUIManager.GetInstance().ShowInviteUI(3, pShopData.id, null);
		}
		else
		{
			fUIManager.GetInstance().ShowWin("ShopBuyItemUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadBuyUiOk), new ArrayList
			{
				nType,
				pShopData
			}, true);
		}
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x0015992C File Offset: 0x00157B2C
	public void OpenBuyItemWin(string sreason, int itemid, int moneytype, int oldnum, int newnum, int groupnum, int maxnum, string sicon = "", ShopManager.BuyItemDelegate func = null, object param = null)
	{
		ArrayList szParam = new ArrayList
		{
			sreason,
			itemid,
			moneytype,
			oldnum,
			newnum,
			groupnum,
			maxnum,
			sicon,
			func,
			param
		};
		fUIManager.GetInstance().ShowWin("ShopBuyItemUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadBuyItemUIOk), szParam, true);
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x001599D8 File Offset: 0x00157BD8
	private void LoadBuyItemUIOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = ShopBuyItemUICon.inst != null;
		if (flag)
		{
			ShopBuyItemUICon.inst.SetData((ArrayList)szParam);
		}
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x00159A08 File Offset: 0x00157C08
	private void LoadBuyUiOk(string sUI, GameObject ob, object szParam)
	{
		ArrayList arrayList = (ArrayList)(szParam as ArrayList).Clone();
		int nType = (int)arrayList[0];
		ShopData data = (ShopData)arrayList[1];
		ShopBuyItemUICon.inst.SetData(nType, data);
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x00159A50 File Offset: 0x00157C50
	public void BuffItemData()
	{
		this.BuffDataList.Clear();
		int count = PlayerDataManager.getInstance().HUserData.m_szShopItemBuff.Count;
		for (int i = 0; i < count; i += 2)
		{
			int num = (int)PlayerDataManager.getInstance().HUserData.m_szShopItemBuff[i];
			ArrayList arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szShopItemBuff[i + 1];
			int count2 = arrayList.Count;
			List<ItemBuffData> list = (!this.BuffDataList.ContainsKey(num)) ? new List<ItemBuffData>() : this.BuffDataList[num];
			for (int j = 0; j < count2; j += 3)
			{
				string sEndTime = arrayList[j + 1].ToString();
				bool flag = Fun.GetEndCDTime(sEndTime) > 0;
				if (flag)
				{
					ItemBuffData item = new ItemBuffData
					{
						sId = num.ToString() + "|" + arrayList[j].ToString(),
						nType = num,
						nBeiShu = int.Parse(arrayList[j].ToString()),
						sEndTime = sEndTime,
						pItem = fGameManager.mloadConfig.AllItemData[(int)arrayList[j + 2]]
					};
					list.Add(item);
				}
			}
			bool flag2 = !this.BuffDataList.ContainsKey(num);
			if (flag2)
			{
				bool flag3 = list.Count > 0;
				if (flag3)
				{
					this.BuffDataList.Add(num, list);
				}
			}
			else
			{
				this.BuffDataList[num] = list;
			}
		}
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x00159C18 File Offset: 0x00157E18
	public static int onGetPrice(ItemData pItemData, int nType)
	{
		int num = 0;
		int num2 = 0;
		bool flag = pItemData.m_nType != 20;
		int result;
		if (flag)
		{
			bool flag2 = nType == 1;
			if (flag2)
			{
				result = pItemData.m_nGlodPrice;
			}
			else
			{
				result = pItemData.m_nDiamondPrice;
			}
		}
		else
		{
			int count = pItemData.m_szChildItem.Count;
			for (int i = 0; i < count; i++)
			{
				ArrayList arrayList = (ArrayList)pItemData.m_szChildItem[i];
				int key = (int)arrayList[0];
				ItemData pData = fGameManager.mloadConfig.AllItemData[key];
				bool flag3 = ShopManager.OnGetHeroHaveEquipOpen(pData) == 1;
				if (flag3)
				{
					num += (int)arrayList[1];
					num2 += (int)arrayList[2];
				}
			}
			if (nType != 1)
			{
				if (nType != 3)
				{
					result = pItemData.m_nDiamondPrice - num;
				}
				else
				{
					int num3 = pItemData.m_nGlodPrice - num2;
					bool flag4 = num3 <= 0;
					if (flag4)
					{
						num3 = pItemData.m_nDiamondPrice - num;
					}
					result = num3;
				}
			}
			else
			{
				result = pItemData.m_nGlodPrice - num2;
			}
		}
		return result;
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x00159D4C File Offset: 0x00157F4C
	public static int OnGetHeroHaveEquipOpen(ItemData pData)
	{
		int nHeroId = pData.m_nHeroId;
		int num = pData.m_nType;
		int num2 = num;
		int num3 = num2;
		int result;
		if (num3 != 3)
		{
			if (num3 - 4 > 3)
			{
				result = 0;
			}
			else
			{
				int nTypeId = pData.m_nTypeId;
				num = ShopManager.GetHeroEquipType(num);
				bool flag = HeroDataManager.GetInstance().GetHeroEquipOpen(nHeroId, num, nTypeId) > 1;
				if (flag)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
		}
		else
		{
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nHeroId);
			bool flag2 = oneHeroData != null && oneHeroData.nOpenState > 1;
			if (flag2)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x00159DE8 File Offset: 0x00157FE8
	public static int GetHeroEquipType(int nItemType)
	{
		int result = 0;
		switch (nItemType)
		{
		case 4:
			result = 1;
			break;
		case 5:
			result = 3;
			break;
		case 6:
			result = 2;
			break;
		case 7:
			result = 4;
			break;
		}
		return result;
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x00159E2C File Offset: 0x0015802C
	public static string GiveType(int nType)
	{
		string empty = string.Empty;
		if (!true)
		{
		}
		string result;
		switch (nType)
		{
		case 1:
			result = Language.GetLang(39512, new object[0]);
			break;
		case 2:
			result = Language.GetLang(39513, new object[0]);
			break;
		case 3:
			result = Language.GetLang(39514, new object[0]);
			break;
		case 4:
			result = Language.GetLang(39515, new object[0]);
			break;
		case 5:
			result = Language.GetLang(39516, new object[0]);
			break;
		case 6:
			result = Language.GetLang(39517, new object[0]);
			break;
		case 7:
			result = Language.GetLang(39518, new object[0]);
			break;
		case 8:
			result = Language.GetLang(39519, new object[0]);
			break;
		case 9:
			result = Language.GetLang(39520, new object[0]);
			break;
		case 10:
			result = Language.GetLang(39521, new object[0]);
			break;
		case 11:
			result = Language.GetLang(39522, new object[0]);
			break;
		case 12:
			result = Language.GetLang(39523, new object[0]);
			break;
		case 13:
			result = Language.GetLang(39524, new object[0]);
			break;
		case 14:
			result = Language.GetLang(39525, new object[0]);
			break;
		default:
			result = string.Empty;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x00159FBC File Offset: 0x001581BC
	public static string GetIcoPath(int nType)
	{
		string empty = string.Empty;
		switch (nType)
		{
		case 3:
		case 4:
			return "herounlock/skin/{0}.txt";
		case 5:
			return "herounlock/emotion/{0}.txt";
		case 6:
			return "herounlock/weapon/{0}.txt";
		case 7:
			return "herounlock/pose/{0}.txt";
		case 8:
			return "painticon/{0}.txt";
		case 15:
			return "userface/{0}.txt";
		}
		return "ItemIco/{0}.txt";
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x0015A04C File Offset: 0x0015824C
	public void OnInitShopData(ShopData data)
	{
		bool flag = data.m_nIsTuiJian == 1;
		if (flag)
		{
			bool flag2 = !this.AllShopItemData.ContainsKey(1);
			if (flag2)
			{
				this.AllShopItemData[1] = new List<ShopData>();
			}
			this.AllShopItemData[1].Add(data);
		}
		bool flag3 = data.m_nTitle != 0;
		if (flag3)
		{
			bool flag4 = !this.AllShopItemData.ContainsKey(data.m_nTitle + 1);
			if (flag4)
			{
				this.AllShopItemData[data.m_nTitle + 1] = new List<ShopData>();
			}
			this.AllShopItemData[data.m_nTitle + 1].Add(data);
		}
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x0015A104 File Offset: 0x00158304
	public Dictionary<int, List<ShopData>> GetAllShop()
	{
		return this.AllShopItemData;
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x0015A11C File Offset: 0x0015831C
	public void OnShopGiveUserItem(ArrayList szUser, int nShopId)
	{
		this.m_sGiceSelectUserName = szUser[0].ToString();
		int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(this.m_sGiceSelectUserName);
		bool flag = num != -1;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num + 1];
			this.m_nGiveSelectCid = (int)arrayList[0];
			bool flag2 = fGameManager.mloadConfig.AllShopData.ContainsKey(nShopId);
			if (flag2)
			{
				ShopData value = fGameManager.mloadConfig.AllShopData[nShopId];
				fUIManager.GetInstance().ShowWin("ShopBuyItemUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadBuyUiOk), new ArrayList
				{
					2,
					value
				}, true);
			}
		}
	}

	// Token: 0x040021B8 RID: 8632
	public static ShopManager mInst;

	// Token: 0x040021B9 RID: 8633
	public ArrayList szLimitItem;

	// Token: 0x040021BA RID: 8634
	public Dictionary<int, ArrayList> m_szHuiGouData = new Dictionary<int, ArrayList>();

	// Token: 0x040021BB RID: 8635
	public int nLiBaoNum;

	// Token: 0x040021BC RID: 8636
	public int[] szEffOpenShunXu = new int[]
	{
		3,
		4,
		1,
		2
	};

	// Token: 0x040021BD RID: 8637
	public Dictionary<int, List<ItemBuffData>> BuffDataList = new Dictionary<int, List<ItemBuffData>>();

	// Token: 0x040021BE RID: 8638
	private Dictionary<int, List<ShopData>> AllShopItemData = new Dictionary<int, List<ShopData>>();

	// Token: 0x040021BF RID: 8639
	public string m_sGiceSelectUserName = string.Empty;

	// Token: 0x040021C0 RID: 8640
	public int m_nGiveSelectCid;

	// Token: 0x0200060F RID: 1551
	// (Invoke) Token: 0x060036F0 RID: 14064
	public delegate void BuyItemDelegate(object szParam, int type, int num);
}

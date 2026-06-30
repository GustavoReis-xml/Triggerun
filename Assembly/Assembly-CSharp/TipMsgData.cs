using System;
using System.Collections;

// Token: 0x0200047C RID: 1148
public class TipMsgData : IComparable<TipMsgData>
{
	// Token: 0x06002C92 RID: 11410 RVA: 0x00188295 File Offset: 0x00186495
	public TipMsgData(int tiptype, int type, ArrayList info)
	{
		this.tipType = tiptype;
		this.nType = type;
		this.szInfo = info;
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x001882B4 File Offset: 0x001864B4
	public void SetBackFunc(TipManager.TipOKDelegate okfunc = null, object okparam = null, TipManager.TipCancelDelegate cancelfunc = null, object cancelparam = null, TipManager.TipToggleDelegate togglefunc = null, TipManager.TipInputDelegate inputfun = null, object inputparam = null)
	{
		this.okFunc = okfunc;
		this.okParam = okparam;
		this.cancelFunc = cancelfunc;
		this.cancelParam = cancelparam;
		this.toggleFunc = togglefunc;
		this.inputFunc = inputfun;
		this.inputParam = inputparam;
	}

	// Token: 0x06002C94 RID: 11412 RVA: 0x001882EC File Offset: 0x001864EC
	public string GetHeadIcon()
	{
		string result = string.Empty;
		int num = this.tipType;
		bool flag = num != 1 && num == 2;
		if (flag)
		{
			result = this.GetRewardHeadIcon();
		}
		return result;
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x00188324 File Offset: 0x00186524
	private string GetRewardHeadIcon()
	{
		string result = string.Empty;
		int num = this.nType;
		int num2 = num;
		if (num2 - 1 > 2)
		{
			if (num2 - 4 <= 1)
			{
				int num3 = (int)this.szInfo[0];
				int key = (int)this.szInfo[1];
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
				bool flag = itemData.m_nType == 4;
				if (flag)
				{
					SkinUnlock skinUnlock = fGameManager.mloadConfig.AllHeroUnlock[itemData.m_nHeroId].szSkinUnlock[itemData.m_nTypeId];
					result = "herounlock/skin/" + skinUnlock.sIconName + ".txt";
				}
				bool flag2 = itemData.m_nType == 5;
				if (flag2)
				{
					SkinUnlock useSkinData = HeroDataManager.GetInstance().GetUseSkinData(itemData.m_nHeroId, PlayerDataManager.getInstance().HUserData.m_nCid);
					result = "herounlock/skin/" + useSkinData.sIconName + ".txt";
				}
			}
		}
		else
		{
			SkinUnlock useSkinData2 = HeroDataManager.GetInstance().GetUseSkinData((int)this.szInfo[0], PlayerDataManager.getInstance().HUserData.m_nCid);
			result = "herounlock/skin/" + useSkinData2.sIconName + ".txt";
		}
		return result;
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x0018847C File Offset: 0x0018667C
	public string GetTitle()
	{
		string result = string.Empty;
		int num = this.tipType;
		bool flag = num != 1 && num == 2;
		if (flag)
		{
			result = this.GetRewardTitle();
		}
		return result;
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x001884B4 File Offset: 0x001866B4
	private string GetRewardTitle()
	{
		string result = string.Empty;
		switch (this.nType)
		{
		case 1:
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
			result = Language.GetLang(19001, new object[]
			{
				heroData.hname,
				this.szInfo[1].ToString()
			});
			break;
		}
		case 2:
		{
			HeroData heroData2 = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
			result = Language.GetLang(19004, new object[]
			{
				heroData2.hname,
				this.GetHeroEquipName((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2])
			});
			break;
		}
		case 3:
		{
			HeroData heroData3 = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
			result = Language.GetLang(19005, new object[]
			{
				heroData3.hname
			});
			break;
		}
		case 4:
			result = Language.GetLang(19008, new object[0]);
			break;
		case 5:
			result = Language.GetLang(19007, new object[0]);
			break;
		case 6:
		{
			string text = string.Empty;
			bool flag = (int)this.szInfo[1] == 0;
			if (flag)
			{
				HeroData heroData4 = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
				text = heroData4.hname;
			}
			else
			{
				text = this.GetHeroEquipName((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			}
			result = Language.GetLang(19009, new object[]
			{
				this.szInfo[3].ToString(),
				text
			});
			break;
		}
		}
		return result;
	}

	// Token: 0x06002C98 RID: 11416 RVA: 0x001886FC File Offset: 0x001868FC
	public string GetIcon()
	{
		string result = string.Empty;
		int num = this.tipType;
		bool flag = num != 1 && num == 2;
		if (flag)
		{
			result = this.GetRewardIcon();
		}
		return result;
	}

	// Token: 0x06002C99 RID: 11417 RVA: 0x00188734 File Offset: 0x00186934
	private string GetRewardIcon()
	{
		string result = string.Empty;
		switch (this.nType)
		{
		case 1:
			result = this.GetHeroGrowIcon((int)this.szInfo[0], (int)this.szInfo[2], (int)this.szInfo[3]);
			break;
		case 2:
			result = this.GetHeroGrowIcon((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			break;
		case 3:
		{
			SkinUnlock useSkinData = HeroDataManager.GetInstance().GetUseSkinData((int)this.szInfo[0], PlayerDataManager.getInstance().HUserData.m_nCid);
			result = "herounlock/skin/" + useSkinData.sIconName + ".txt";
			break;
		}
		case 4:
		case 5:
		{
			int key = (int)this.szInfo[1];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			result = string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco);
			break;
		}
		case 6:
		{
			bool flag = (int)this.szInfo[1] == 0;
			if (flag)
			{
				SkinUnlock useSkinData2 = HeroDataManager.GetInstance().GetUseSkinData((int)this.szInfo[0], PlayerDataManager.getInstance().HUserData.m_nCid);
				result = "herounlock/skin/" + useSkinData2.sIconName + ".txt";
			}
			else
			{
				result = this.GetHeroGrowIcon((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x00188928 File Offset: 0x00186B28
	private string GetHeroGrowIcon(int heroid, int ntype, int nid)
	{
		string result = string.Empty;
		switch (ntype)
		{
		case 0:
			result = "ItemIco/jb.txt";
			break;
		case 1:
		case 2:
		case 3:
		case 4:
			result = this.GetHeroEquipIcon(heroid, ntype, nid);
			break;
		case 5:
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nid];
			result = "userface/" + playerFaceData.sFaceIcon + ".txt";
			break;
		}
		case 6:
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[nid];
			result = "painticon/" + playerPaintData.sIconName + ".txt";
			break;
		}
		}
		return result;
	}

	// Token: 0x06002C9B RID: 11419 RVA: 0x001889D8 File Offset: 0x00186BD8
	public string GetQual()
	{
		string result = string.Empty;
		int num = this.tipType;
		bool flag = num != 1 && num == 2;
		if (flag)
		{
			result = this.GetRewardQual();
		}
		return result;
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x00188A10 File Offset: 0x00186C10
	private string GetRewardQual()
	{
		string result = string.Empty;
		switch (this.nType)
		{
		case 1:
			result = this.GetHeroGrowQual((int)this.szInfo[0], (int)this.szInfo[2], (int)this.szInfo[3]);
			break;
		case 2:
			result = this.GetHeroGrowQual((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			break;
		case 3:
			result = string.Empty;
			break;
		case 4:
		case 5:
		{
			int key = (int)this.szInfo[1];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			result = this.GetImageName(itemData.m_nQuality);
			break;
		}
		case 6:
			result = (((int)this.szInfo[1] != 0) ? this.GetHeroGrowQual((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]) : string.Empty);
			break;
		}
		return result;
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x00188B70 File Offset: 0x00186D70
	private string GetHeroGrowQual(int heroid, int ntype, int nid)
	{
		string result = string.Empty;
		if (ntype - 1 > 3)
		{
			if (ntype == 6)
			{
				PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[nid];
				int nQual = playerPaintData.nQual;
				result = this.GetImageName(nQual);
			}
		}
		else
		{
			int heroEquipQual = this.GetHeroEquipQual(heroid, ntype, nid);
			result = this.GetImageName(heroEquipQual);
		}
		return result;
	}

	// Token: 0x06002C9E RID: 11422 RVA: 0x00188BDC File Offset: 0x00186DDC
	private string GetImageName(int nqual)
	{
		string result = string.Empty;
		switch (nqual)
		{
		case 1:
			result = "heroui_bai";
			break;
		case 2:
			result = "heroui_lv";
			break;
		case 3:
			result = "heroui_lan";
			break;
		case 4:
			result = "heroui_zi";
			break;
		case 5:
			result = "heroui_jin";
			break;
		}
		return result;
	}

	// Token: 0x06002C9F RID: 11423 RVA: 0x00188C40 File Offset: 0x00186E40
	public string GetDesc()
	{
		string result = string.Empty;
		int num = this.tipType;
		bool flag = num != 1 && num == 2;
		if (flag)
		{
			result = this.GetRewardDesc();
		}
		return result;
	}

	// Token: 0x06002CA0 RID: 11424 RVA: 0x00188C78 File Offset: 0x00186E78
	private string GetRewardDesc()
	{
		string text = string.Empty;
		switch (this.nType)
		{
		case 1:
			text = this.GetHeroGrowDesc();
			break;
		case 2:
			text = this.GetHeroEquipName((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			break;
		case 3:
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
			text = heroData.hname;
			break;
		}
		case 4:
		case 5:
		{
			int key = (int)this.szInfo[1];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			text = ((itemData.m_nType == 4) ? this.GetHeroEquipName(itemData.m_nHeroId, 1, itemData.m_nTypeId) : ((itemData.m_nType != 5) ? itemData.m_sName : this.GetHeroEquipName(itemData.m_nHeroId, 3, itemData.m_nTypeId)));
			bool flag = this.nType == 5;
			if (flag)
			{
				text = text + "*" + ((int)this.szInfo[2]).ToString();
			}
			break;
		}
		case 6:
		{
			bool flag2 = (int)this.szInfo[1] == 0;
			if (flag2)
			{
				HeroData heroData2 = fGameManager.mloadConfig.AllHeroList[(int)this.szInfo[0]];
				text = heroData2.hname;
			}
			else
			{
				text = this.GetHeroEquipName((int)this.szInfo[0], (int)this.szInfo[1], (int)this.szInfo[2]);
			}
			break;
		}
		}
		return text;
	}

	// Token: 0x06002CA1 RID: 11425 RVA: 0x00188E70 File Offset: 0x00187070
	private string GetHeroGrowDesc()
	{
		string result = string.Empty;
		switch ((int)this.szInfo[2])
		{
		case 0:
			result = Language.GetLang(19002, new object[]
			{
				this.szInfo[3].ToString()
			});
			break;
		case 1:
		case 2:
		case 3:
		case 4:
			result = Language.GetLang(19003, new object[]
			{
				this.GetHeroEquipName((int)this.szInfo[0], (int)this.szInfo[2], (int)this.szInfo[3])
			});
			break;
		case 5:
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[(int)this.szInfo[3]];
			result = Language.GetLang(19003, new object[]
			{
				playerFaceData.sFaceName
			});
			break;
		}
		case 6:
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[(int)this.szInfo[3]];
			result = Language.GetLang(19003, new object[]
			{
				playerPaintData.sName
			});
			break;
		}
		}
		return result;
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x00188FC0 File Offset: 0x001871C0
	private string GetHeroEquipName(int heroid, int ntype, int nid)
	{
		string result = string.Empty;
		HeroUnlockData heroUnlockData = null;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(heroid);
		if (flag)
		{
			heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[heroid];
		}
		switch (ntype)
		{
		case 1:
			result = heroUnlockData.szSkinUnlock[nid].sName;
			break;
		case 2:
			result = heroUnlockData.szWeaponUnlock[nid].sName;
			break;
		case 3:
			result = heroUnlockData.szEmotionUnlock[nid].sName;
			break;
		case 4:
			result = heroUnlockData.szPoseUnlock[nid].sName;
			break;
		case 5:
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nid];
			result = playerFaceData.sFaceName;
			break;
		}
		case 6:
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[nid];
			result = playerPaintData.sName;
			break;
		}
		}
		return result;
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x001890BC File Offset: 0x001872BC
	private string GetHeroEquipIcon(int heroid, int ntype, int nid)
	{
		string result = string.Empty;
		HeroUnlockData heroUnlockData = null;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(heroid);
		if (flag)
		{
			heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[heroid];
		}
		switch (ntype)
		{
		case 1:
			result = "herounlock/skin/" + heroUnlockData.szSkinUnlock[nid].sIconName + ".txt";
			break;
		case 2:
			result = "herounlock/weapon/" + heroUnlockData.szWeaponUnlock[nid].sIconName + ".txt";
			break;
		case 3:
			result = "herounlock/emotion/" + heroUnlockData.szEmotionUnlock[nid].sIconName + ".txt";
			break;
		case 4:
			result = "herounlock/pose/" + heroUnlockData.szPoseUnlock[nid].sIconName + ".txt";
			break;
		}
		return result;
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x001891B0 File Offset: 0x001873B0
	private int GetHeroEquipQual(int heroid, int ntype, int nid)
	{
		int result = 0;
		HeroUnlockData heroUnlockData = null;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(heroid);
		if (flag)
		{
			heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[heroid];
		}
		switch (ntype)
		{
		case 1:
			result = heroUnlockData.szSkinUnlock[nid].nQual;
			break;
		case 2:
			result = heroUnlockData.szWeaponUnlock[nid].nQual;
			break;
		case 3:
			result = heroUnlockData.szEmotionUnlock[nid].nQual;
			break;
		case 4:
			result = heroUnlockData.szPoseUnlock[nid].nQual;
			break;
		}
		return result;
	}

	// Token: 0x06002CA5 RID: 11429 RVA: 0x00189260 File Offset: 0x00187460
	public int CompareTo(TipMsgData other)
	{
		return this.tipType.CompareTo(other.tipType);
	}

	// Token: 0x06002CA6 RID: 11430 RVA: 0x00189288 File Offset: 0x00187488
	public override bool Equals(object obj)
	{
		TipMsgData tipMsgData = (TipMsgData)obj;
		bool flag = this.tipType == tipMsgData.tipType && (this.tipType == 1 || this.tipType == 7);
		bool result;
		if (flag)
		{
			bool flag2 = this.szInfo[0].ToString() != tipMsgData.szInfo[0].ToString();
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.szInfo[1].ToString() != tipMsgData.szInfo[1].ToString();
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = this.szInfo[2].ToString() != tipMsgData.szInfo[2].ToString();
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this.szInfo[3].ToString() != tipMsgData.szInfo[3].ToString();
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = this.szInfo[4].ToString() != tipMsgData.szInfo[4].ToString();
							if (flag6)
							{
								result = false;
							}
							else
							{
								bool flag7 = !this.szInfo[5].Equals(tipMsgData.szInfo[5]);
								result = !flag7;
							}
						}
					}
				}
			}
		}
		else
		{
			result = base.Equals(obj);
		}
		return result;
	}

	// Token: 0x06002CA7 RID: 11431 RVA: 0x00189408 File Offset: 0x00187608
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x040025D8 RID: 9688
	public int tipType;

	// Token: 0x040025D9 RID: 9689
	public int nType;

	// Token: 0x040025DA RID: 9690
	public ArrayList szInfo;

	// Token: 0x040025DB RID: 9691
	public TipManager.TipOKDelegate okFunc;

	// Token: 0x040025DC RID: 9692
	public object okParam;

	// Token: 0x040025DD RID: 9693
	public TipManager.TipCancelDelegate cancelFunc;

	// Token: 0x040025DE RID: 9694
	public object cancelParam;

	// Token: 0x040025DF RID: 9695
	public TipManager.TipToggleDelegate toggleFunc;

	// Token: 0x040025E0 RID: 9696
	public object inputParam;

	// Token: 0x040025E1 RID: 9697
	public TipManager.TipInputDelegate inputFunc;
}

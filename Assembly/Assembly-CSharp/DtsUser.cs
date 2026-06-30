using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200010A RID: 266
public class DtsUser
{
	// Token: 0x0600098B RID: 2443 RVA: 0x00055124 File Offset: 0x00053324
	public DtsUser(ArrayList szData)
	{
		this.setUser(szData);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0005514C File Offset: 0x0005334C
	public void setUser(ArrayList szData)
	{
		this.m_nNetID = (int)szData[0];
		this.m_nCid = (int)szData[1];
		this.m_sNick = szData[2].ToString();
		this.m_nSelectScnID = (int)szData[3];
		this.m_nNowHeroID = (int)szData[4];
		this.m_szHeroList = (ArrayList)szData[5];
		this.m_nHpBox = (int)szData[6];
		this.m_nAttackLvl = (int)szData[7];
		this.m_nDefendLvl = (int)szData[8];
		this.m_nKill = (int)szData[9];
		this.m_szSkillList = (ArrayList)szData[10];
		this.m_nDaZhaoBox = (int)szData[11];
		bool flag = this.m_nNetID == fGameManager.GetInstance().getPlayer().getNetid();
		if (flag)
		{
			DtsBatteManager.GetInstance().SetMapItemStat(this);
			DtsBatteManager.GetInstance().OpenDtsUI(this);
		}
		bool flag2 = DtsUICon.Inst != null && DtsUICon.Inst.visible;
		if (flag2)
		{
			DtsUICon.Inst.OnUpdateKillNum(this);
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00055298 File Offset: 0x00053498
	public void SetIsQuanIngStat(int nStat)
	{
		this.m_nIsQuanStat = nStat;
		bool flag = DtsUICon.Inst != null && DtsUICon.Inst.visible;
		if (flag)
		{
			DtsUICon.Inst.OnSetQuanStat(this.m_nIsQuanStat);
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x000552E0 File Offset: 0x000534E0
	public bool IsItemCan(int nItemType, int nItmeIndex, int nMsgType, ArrayList thisItem)
	{
		bool result = true;
		ArrayList arrayList = new ArrayList();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		string text = string.Empty;
		switch (nItemType)
		{
		case 1:
		{
			bool flag = nItmeIndex == this.m_nNowHeroID;
			if (flag)
			{
				result = false;
				bool flag2 = fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(nItmeIndex) && nMsgType == 1;
				if (flag2)
				{
					HeroAllSkillData heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[nItmeIndex];
					text = Language.GetLang(29047, new object[]
					{
						heroAllSkillData.sName
					});
				}
			}
			else
			{
				bool flag3 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nItmeIndex);
				if (flag3)
				{
					OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nItmeIndex);
					bool flag4 = oneHeroData.nOpenState < 1;
					if (flag4)
					{
						result = false;
						text = Language.GetLang(29046, new object[0]);
					}
				}
			}
			break;
		}
		case 4:
		{
			bool flag5 = this.m_nAttackLvl >= 10;
			if (flag5)
			{
				result = false;
				text = Language.GetLang(29049, new object[0]);
			}
			break;
		}
		case 5:
		{
			bool flag6 = this.m_nDefendLvl >= 10;
			if (flag6)
			{
				result = false;
				text = Language.GetLang(29050, new object[0]);
			}
			break;
		}
		case 7:
		{
			bool flag7 = nItmeIndex != 0 && thisItem.Count > 0 && (int)thisItem[2] != fGameManager.GetInstance().getPlayer().getNetid();
			if (flag7)
			{
				result = false;
			}
			break;
		}
		}
		bool flag8 = nMsgType == 1 && text != string.Empty && gameuiCon.inst != null;
		if (flag8)
		{
			gameuiCon.inst.mainMsgUI.AddGameMsg(text);
		}
		return result;
	}

	// Token: 0x04000752 RID: 1874
	public int m_nNetID;

	// Token: 0x04000753 RID: 1875
	public int m_nCid;

	// Token: 0x04000754 RID: 1876
	public string m_sNick;

	// Token: 0x04000755 RID: 1877
	public int m_nSelectScnID;

	// Token: 0x04000756 RID: 1878
	public int m_nNowHeroID;

	// Token: 0x04000757 RID: 1879
	public ArrayList m_szHeroList = new ArrayList();

	// Token: 0x04000758 RID: 1880
	public int m_nHpBox;

	// Token: 0x04000759 RID: 1881
	public int m_nAttackLvl;

	// Token: 0x0400075A RID: 1882
	public int m_nDefendLvl;

	// Token: 0x0400075B RID: 1883
	public int m_nKill;

	// Token: 0x0400075C RID: 1884
	public int m_nIsQuanStat;

	// Token: 0x0400075D RID: 1885
	public ArrayList m_szSkillList = new ArrayList();

	// Token: 0x0400075E RID: 1886
	public int m_nDaZhaoBox;
}

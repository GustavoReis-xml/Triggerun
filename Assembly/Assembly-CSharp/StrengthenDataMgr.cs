using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E9 RID: 1001
public class StrengthenDataMgr
{
	// Token: 0x06002A77 RID: 10871 RVA: 0x00176984 File Offset: 0x00174B84
	public void InitData(ArrayList szData)
	{
		this.UserStrenthenData.Clear();
		int count = szData.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList szData2 = (ArrayList)szData[i];
			HeroTypeStrengThenData value = new HeroTypeStrengThenData(i, szData2);
			this.UserStrenthenData.Add(i, value);
		}
		PveManager.GetInstance().OnCheckCanLevelUp();
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x001769E8 File Offset: 0x00174BE8
	public HeroOneAttData GetAttDataByType(int nHeroType, int nAttType)
	{
		bool flag = !this.UserStrenthenData.ContainsKey(nHeroType);
		HeroOneAttData result;
		if (flag)
		{
			result = new HeroOneAttData(nHeroType, nAttType, new ArrayList
			{
				0,
				0,
				0,
				0,
				0
			});
		}
		else
		{
			bool flag2 = !this.UserStrenthenData[nHeroType].UserOneAttData.ContainsKey(nAttType);
			if (flag2)
			{
				result = new HeroOneAttData(nHeroType, nAttType, new ArrayList
				{
					0,
					0,
					0,
					0,
					0
				});
			}
			else
			{
				result = this.UserStrenthenData[nHeroType].UserOneAttData[nAttType];
			}
		}
		return result;
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x00176AE8 File Offset: 0x00174CE8
	public HeroOneAttData GetAttData(int nHeroType, int nAttType)
	{
		bool flag = !this.UserStrenthenData.ContainsKey(nHeroType);
		HeroOneAttData result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = !this.UserStrenthenData[nHeroType].UserOneAttData.ContainsKey(nAttType);
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = this.UserStrenthenData[nHeroType].UserOneAttData[nAttType];
			}
		}
		return result;
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x00176B4C File Offset: 0x00174D4C
	public HeroOneAttData UpdateHeroAttData(int nHeroType, int nAttType, ArrayList szData)
	{
		ArrayList arrayList = new ArrayList();
		HeroOneAttData attData = this.GetAttData(nHeroType, nAttType);
		bool flag = attData == null;
		HeroOneAttData result;
		if (flag)
		{
			bool flag2 = !this.UserStrenthenData.ContainsKey(nHeroType);
			if (flag2)
			{
				arrayList.Add(nAttType);
				arrayList.Add(szData);
				this.UserStrenthenData.Add(nHeroType, new HeroTypeStrengThenData(nHeroType, arrayList));
			}
			else
			{
				this.UserStrenthenData[nHeroType].UserOneAttData.Add(nAttType, new HeroOneAttData(nHeroType, nAttType, szData));
			}
			result = this.UserStrenthenData[nHeroType].UserOneAttData[nAttType];
		}
		else
		{
			attData.onUpdate(szData);
			result = attData;
		}
		return result;
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x00176C00 File Offset: 0x00174E00
	public void SetGeneData(ArrayList szMsg)
	{
		this.m_szGeneData = szMsg;
		bool flag = this.m_szGeneData.Count > 0;
		if (flag)
		{
			this.m_nNowStep = (int)this.m_szGeneData[0];
			this.m_nNowStar = (int)this.m_szGeneData[1];
			int num = this.m_nNowStep;
			int num2 = this.m_nNowStar;
			bool flag2 = fGameManager.mloadConfig.AllGeneData.ContainsKey(this.m_nNowStep);
			if (flag2)
			{
				GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.m_nNowStep];
				bool flag3 = this.m_nNowStar >= geneData.nMaxNum;
				if (flag3)
				{
					num++;
					num2 = 1;
				}
				else
				{
					num2++;
				}
			}
			bool flag4 = fGameManager.mloadConfig.AllGeneData.ContainsKey(num);
			if (flag4)
			{
				this.m_nNextStep = num;
				this.m_nNextStar = num2;
			}
			else
			{
				this.m_nNextStep = this.m_nNowStep;
				this.m_nNextStar = this.m_nNowStar;
			}
		}
		else
		{
			this.m_nNowStep = 0;
			this.m_nNowStar = 0;
			this.m_nNextStep = 0;
			this.m_nNextStar = 1;
		}
		bool flag5 = GeneUICon.inst != null;
		if (flag5)
		{
			GeneUICon.inst.InitUIData(1);
		}
		bool flag6 = QiangHuaUICon.inst != null && QiangHuaUICon.inst.visible;
		if (flag6)
		{
			QiangHuaUICon.inst.UpdateZdl();
		}
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x00176D70 File Offset: 0x00174F70
	public void GeneUpdateLose(ArrayList szMsg)
	{
		int nret = (int)szMsg[0];
		int nstep = (int)szMsg[1];
		int nstar = (int)szMsg[2];
		int ncost = (int)szMsg[3];
		bool flag = GeneUICon.inst != null;
		if (flag)
		{
			GeneUICon.inst.ShowUpdateEff(nstep, nstar, nret, ncost);
		}
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x00176DD4 File Offset: 0x00174FD4
	public bool ShowGeneTanhao()
	{
		bool flag = fGameManager.mloadConfig.AllGeneData.ContainsKey(this.m_nNextStep);
		if (flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.m_nNextStep];
			bool flag2 = geneData.allStarData.ContainsKey(this.m_nNextStar);
			if (flag2)
			{
				GeneStarData geneStarData = geneData.allStarData[this.m_nNextStar];
				bool flag3 = PlayerDataManager.getInstance().playerdet.GetMoneyByType(int.Parse(geneStarData.szCost[0])) >= int.Parse(geneStarData.szCost[1]);
				if (flag3)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x00176E80 File Offset: 0x00175080
	public int[] GetGeneAddAttr(int nstep, int nstar)
	{
		int[] array = new int[20];
		bool flag = this.m_szGeneData.Count == 0;
		int[] result;
		if (flag)
		{
			result = array;
		}
		else
		{
			bool flag2 = fGameManager.mloadConfig.AllGeneData.ContainsKey(nstep);
			if (flag2)
			{
				GeneData geneData = fGameManager.mloadConfig.AllGeneData[nstep];
				bool flag3 = !geneData.allStarData.ContainsKey(nstar);
				if (flag3)
				{
					nstep = this.m_nNowStep;
					nstar = this.m_nNowStar;
				}
			}
			else
			{
				nstep = this.m_nNowStep;
				nstar = this.m_nNowStar;
			}
			foreach (GeneData geneData2 in fGameManager.mloadConfig.AllGeneData.Values)
			{
				bool flag4 = geneData2.nStep > nstep;
				if (flag4)
				{
					break;
				}
				foreach (GeneStarData geneStarData in geneData2.allStarData.Values)
				{
					bool flag5 = geneData2.nStep >= nstep && geneStarData.nStar > nstar;
					if (flag5)
					{
						break;
					}
					for (int i = 0; i < geneStarData.szAttr.Length; i += 2)
					{
						array[int.Parse(geneStarData.szAttr[i])] += int.Parse(geneStarData.szAttr[i + 1]);
					}
				}
			}
			result = array;
		}
		return result;
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x00177034 File Offset: 0x00175234
	public int GetGeneAddEffect(int nstep, int nstar)
	{
		int num = 0;
		bool flag = this.m_szGeneData.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = fGameManager.mloadConfig.AllGeneData.ContainsKey(nstep);
			if (flag2)
			{
				GeneData geneData = fGameManager.mloadConfig.AllGeneData[nstep];
				bool flag3 = !geneData.allStarData.ContainsKey(nstar);
				if (flag3)
				{
					nstep = this.m_nNowStep;
					nstar = this.m_nNowStar;
				}
			}
			else
			{
				nstep = this.m_nNowStep;
				nstar = this.m_nNowStar;
			}
			foreach (GeneData geneData2 in fGameManager.mloadConfig.AllGeneData.Values)
			{
				bool flag4 = geneData2.nStep > nstep;
				if (flag4)
				{
					break;
				}
				foreach (GeneStarData geneStarData in geneData2.allStarData.Values)
				{
					bool flag5 = geneData2.nStep >= nstep && geneStarData.nStar > nstar;
					if (flag5)
					{
						break;
					}
					num += geneStarData.nEffect;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06002A80 RID: 10880 RVA: 0x001771A0 File Offset: 0x001753A0
	public int GetGeneEffect(int nstep, int nstar)
	{
		int num = 0;
		bool flag = fGameManager.mloadConfig.AllGeneData.ContainsKey(nstep);
		int result;
		if (flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[nstep];
			bool flag2 = !geneData.allStarData.ContainsKey(nstar);
			if (flag2)
			{
				result = 0;
			}
			else
			{
				foreach (GeneData geneData2 in fGameManager.mloadConfig.AllGeneData.Values)
				{
					bool flag3 = geneData2.nStep > nstep;
					if (flag3)
					{
						break;
					}
					foreach (GeneStarData geneStarData in geneData2.allStarData.Values)
					{
						bool flag4 = geneData2.nStep >= nstep && geneStarData.nStar > nstar;
						if (flag4)
						{
							break;
						}
						num += geneStarData.nEffect;
					}
				}
				result = num;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x04002417 RID: 9239
	public Dictionary<int, HeroTypeStrengThenData> UserStrenthenData = new Dictionary<int, HeroTypeStrengThenData>();

	// Token: 0x04002418 RID: 9240
	public ArrayList m_szGeneData = new ArrayList();

	// Token: 0x04002419 RID: 9241
	public int m_nNowStep;

	// Token: 0x0400241A RID: 9242
	public int m_nNowStar;

	// Token: 0x0400241B RID: 9243
	public int m_nNextStep;

	// Token: 0x0400241C RID: 9244
	public int m_nNextStar;
}

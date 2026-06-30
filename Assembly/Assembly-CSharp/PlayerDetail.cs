using System;
using System.Collections;

// Token: 0x02000293 RID: 659
public class PlayerDetail
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x00117A28 File Offset: 0x00115C28
	public int GetMoneyByType(int ntype)
	{
		int result = 0;
		switch (ntype)
		{
		case 0:
			result = this.gameCurrency;
			break;
		case 1:
			result = this.gameCurrency_Bind;
			break;
		case 2:
			result = this.chargeCurrency;
			break;
		case 3:
			result = this.chargeCurrency_Bind;
			break;
		case 5:
			result = this.matchMoney;
			break;
		case 6:
			result = this.m_nReviveMoney;
			break;
		case 7:
			result = this.m_nGeneChip;
			break;
		case 8:
			result = this.m_nWishGold;
			break;
		}
		return result;
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x00117AB8 File Offset: 0x00115CB8
	public void SetMoneyData(ArrayList szMoney)
	{
		this.gameCurrency = (int)szMoney[0];
		this.gameCurrency_Bind = (int)szMoney[1];
		this.chargeCurrency = (int)szMoney[2];
		this.chargeCurrency_Bind = (int)szMoney[3];
		this.vip = (int)szMoney[4];
		this.vipgold = (int)szMoney[5];
		this.heroSkinChip = (int)szMoney[6];
		this.heroEmotChip = (int)szMoney[7];
		int nGeneChip = this.matchMoney;
		this.matchMoney = (int)szMoney[8];
		bool flag = nGeneChip != this.matchMoney;
		if (flag)
		{
			this.addMatchMoney = this.matchMoney - nGeneChip;
		}
		this.m_nReviveMoney = (int)szMoney[9];
		nGeneChip = this.m_nGeneChip;
		this.m_nGeneChip = (int)szMoney[10];
		bool flag2 = nGeneChip != this.m_nGeneChip;
		if (flag2)
		{
			bool flag3 = QiangHuaUICon.inst != null;
			if (flag3)
			{
				QiangHuaUICon.inst.OnShowTanHao();
			}
			PveManager.GetInstance().OnCheckCanLevelUp();
		}
		this.m_nWishGold = (int)szMoney[11];
		this.m_nPveFlower = (int)szMoney[12];
		this.m_nPveFlowerTime = (int)szMoney[13];
		bool flag4 = MainUICon.inst != null;
		if (flag4)
		{
			MainUICon.inst.OnUpdateMoeny();
		}
		bool flag5 = FaceUICon.inst != null && FaceUICon.inst.visible;
		if (flag5)
		{
			FaceUICon.inst.SetPlayerInfo();
		}
		bool flag6 = LuckyDrawUICon.inst != null && LuckyDrawUICon.inst.visible;
		if (flag6)
		{
			LuckyDrawUICon.inst.ResetUIData(0);
		}
		bool flag7 = WishTreeUICon.inst != null && WishTreeUICon.inst.visible;
		if (flag7)
		{
			WishTreeUICon.inst.SetWishGoldNum();
		}
	}

	// Token: 0x04001B6D RID: 7021
	public int gameCurrency;

	// Token: 0x04001B6E RID: 7022
	public int gameCurrency_Bind;

	// Token: 0x04001B6F RID: 7023
	public int chargeCurrency;

	// Token: 0x04001B70 RID: 7024
	public int chargeCurrency_Bind;

	// Token: 0x04001B71 RID: 7025
	public int vip;

	// Token: 0x04001B72 RID: 7026
	public int vipgold;

	// Token: 0x04001B73 RID: 7027
	public int heroSkinChip;

	// Token: 0x04001B74 RID: 7028
	public int heroEmotChip;

	// Token: 0x04001B75 RID: 7029
	public int matchMoney;

	// Token: 0x04001B76 RID: 7030
	public int addMatchMoney;

	// Token: 0x04001B77 RID: 7031
	public int m_nReviveMoney;

	// Token: 0x04001B78 RID: 7032
	public int m_nGeneChip;

	// Token: 0x04001B79 RID: 7033
	public int m_nWishGold;

	// Token: 0x04001B7A RID: 7034
	public int m_nPveFlower;

	// Token: 0x04001B7B RID: 7035
	public int m_nPveFlowerTime;
}

using System;
using System.Collections;

// Token: 0x02000170 RID: 368
public class HeroCareerData
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0009D2E8 File Offset: 0x0009B4E8
	public void InitUserData(ArrayList arr)
	{
		this.m_sNick = (string)arr[0];
		this.m_nLevel = (int)arr[1];
		this.m_nAvator = (int)arr[2];
		this.m_nShili = (int)arr[3];
		this.m_nIsReady = (int)arr[4];
		this.m_nSex = (int)arr[5];
		this.m_nHeroID = (int)arr[6];
		this.m_nCid = (int)arr[7];
		this.m_nAutoID = (int)arr[8];
		this.m_nIsChsok = (int)arr[9];
		this.m_nFace = (int)arr[10];
		this.m_nVip = (int)arr[11];
		this.m_nTeamID = (int)arr[12];
		this.m_szHeroInfo = (ArrayList)arr[13];
		this.m_nNetid = (int)arr[14];
		this.m_nIsMiJoin = (int)arr[15];
	}

	// Token: 0x04000D65 RID: 3429
	public string m_sNick;

	// Token: 0x04000D66 RID: 3430
	public int m_nLevel;

	// Token: 0x04000D67 RID: 3431
	public int m_nAvator;

	// Token: 0x04000D68 RID: 3432
	public int m_nShili;

	// Token: 0x04000D69 RID: 3433
	public int m_nIsReady;

	// Token: 0x04000D6A RID: 3434
	public int m_nSex;

	// Token: 0x04000D6B RID: 3435
	public int m_nHeroID;

	// Token: 0x04000D6C RID: 3436
	public int m_nCid;

	// Token: 0x04000D6D RID: 3437
	public int m_nAutoID;

	// Token: 0x04000D6E RID: 3438
	public int m_nIsChsok;

	// Token: 0x04000D6F RID: 3439
	public int m_nFace;

	// Token: 0x04000D70 RID: 3440
	public int m_nVip;

	// Token: 0x04000D71 RID: 3441
	public int m_nTeamID;

	// Token: 0x04000D72 RID: 3442
	public int m_nNetid;

	// Token: 0x04000D73 RID: 3443
	public int m_nIsMiJoin;

	// Token: 0x04000D74 RID: 3444
	public ArrayList m_szHeroInfo;
}

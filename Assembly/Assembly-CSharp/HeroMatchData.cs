using System;
using System.Collections;

// Token: 0x020001A4 RID: 420
public class HeroMatchData
{
	// Token: 0x06001377 RID: 4983 RVA: 0x000B96C8 File Offset: 0x000B78C8
	public HeroMatchData(int heroid)
	{
		this.nHeroId = heroid;
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x000B96D9 File Offset: 0x000B78D9
	public void SetData(ArrayList szData)
	{
		this.nVicNum = (int)szData[0];
		this.nAllTime = (int)szData[1];
	}

	// Token: 0x04000F1F RID: 3871
	public int nHeroId;

	// Token: 0x04000F20 RID: 3872
	public int nVicNum;

	// Token: 0x04000F21 RID: 3873
	public int nAllTime;
}

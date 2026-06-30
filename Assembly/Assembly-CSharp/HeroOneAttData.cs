using System;
using System.Collections;

// Token: 0x020001AB RID: 427
public class HeroOneAttData
{
	// Token: 0x060013AB RID: 5035 RVA: 0x000BA5D8 File Offset: 0x000B87D8
	public HeroOneAttData(int nHeroType, int nAttType, ArrayList szData)
	{
		this.m_nHeroType = nHeroType;
		this.m_nType = nAttType;
		this.onUpdate(szData);
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x000BA5F8 File Offset: 0x000B87F8
	public void onUpdate(ArrayList szData)
	{
		this.m_nJieLv = (int)szData[0];
		this.m_nQhLv = (int)szData[1];
	}

	// Token: 0x04000F2F RID: 3887
	public int m_nHeroType;

	// Token: 0x04000F30 RID: 3888
	public int m_nType;

	// Token: 0x04000F31 RID: 3889
	public int m_nJieLv;

	// Token: 0x04000F32 RID: 3890
	public int m_nQhLv;
}

using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020001C1 RID: 449
public class HeroTypeStrengThenData
{
	// Token: 0x06001465 RID: 5221 RVA: 0x000C008C File Offset: 0x000BE28C
	public HeroTypeStrengThenData(int nHeroType, ArrayList szData)
	{
		this.m_nHeroType = nHeroType;
		int count = szData.Count;
		for (int i = 0; i < count; i += 2)
		{
			HeroOneAttData heroOneAttData = new HeroOneAttData(nHeroType, (int)szData[i], (ArrayList)szData[i + 1]);
			this.UserOneAttData.Add(heroOneAttData.m_nType, heroOneAttData);
		}
	}

	// Token: 0x04000FAD RID: 4013
	private int m_nHeroType;

	// Token: 0x04000FAE RID: 4014
	public Dictionary<int, HeroOneAttData> UserOneAttData = new Dictionary<int, HeroOneAttData>();
}

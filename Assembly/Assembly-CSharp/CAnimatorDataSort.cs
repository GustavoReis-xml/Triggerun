using System;
using System.Collections.Generic;

// Token: 0x02000049 RID: 73
public class CAnimatorDataSort : IComparer<CAnimatorData>
{
	// Token: 0x0600027E RID: 638 RVA: 0x00012D98 File Offset: 0x00010F98
	public int Compare(CAnimatorData x, CAnimatorData y)
	{
		return x.t.CompareTo(y.t);
	}
}

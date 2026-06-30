using System;
using System.Collections.Generic;

// Token: 0x02000041 RID: 65
public class CADataSort : IComparer<CAData>
{
	// Token: 0x0600023C RID: 572 RVA: 0x0000F808 File Offset: 0x0000DA08
	public int Compare(CAData x, CAData y)
	{
		return x.t.CompareTo(y.t);
	}
}

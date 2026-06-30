using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class CBezierBase
{
	// Token: 0x0600029B RID: 667 RVA: 0x00014854 File Offset: 0x00012A54
	public CBezierBase(List<Vector3> _source)
	{
		this.source = _source;
	}

	// Token: 0x04000265 RID: 613
	public List<Vector3> source;

	// Token: 0x04000266 RID: 614
	public List<Vector3> point;
}

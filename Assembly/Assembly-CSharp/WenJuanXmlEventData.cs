using System;
using System.Collections.Generic;

// Token: 0x020004A6 RID: 1190
public class WenJuanXmlEventData
{
	// Token: 0x04002748 RID: 10056
	public int eId;

	// Token: 0x04002749 RID: 10057
	public int eType;

	// Token: 0x0400274A RID: 10058
	public string eText;

	// Token: 0x0400274B RID: 10059
	public Dictionary<int, WenJuanXmlKeyData> eWenJuanKeyData = new Dictionary<int, WenJuanXmlKeyData>();
}

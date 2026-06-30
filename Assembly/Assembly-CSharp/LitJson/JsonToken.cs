using System;

namespace LitJson
{
	// Token: 0x02000509 RID: 1289
	public enum JsonToken
	{
		// Token: 0x04002AD3 RID: 10963
		None,
		// Token: 0x04002AD4 RID: 10964
		ObjectStart,
		// Token: 0x04002AD5 RID: 10965
		PropertyName,
		// Token: 0x04002AD6 RID: 10966
		ObjectEnd,
		// Token: 0x04002AD7 RID: 10967
		ArrayStart,
		// Token: 0x04002AD8 RID: 10968
		ArrayEnd,
		// Token: 0x04002AD9 RID: 10969
		Int,
		// Token: 0x04002ADA RID: 10970
		Long,
		// Token: 0x04002ADB RID: 10971
		Double,
		// Token: 0x04002ADC RID: 10972
		String,
		// Token: 0x04002ADD RID: 10973
		Boolean,
		// Token: 0x04002ADE RID: 10974
		Null
	}
}

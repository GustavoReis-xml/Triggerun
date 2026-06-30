using System;

namespace YouMe
{
	// Token: 0x020004ED RID: 1261
	public enum JsonToken
	{
		// Token: 0x040029C9 RID: 10697
		None,
		// Token: 0x040029CA RID: 10698
		ObjectStart,
		// Token: 0x040029CB RID: 10699
		PropertyName,
		// Token: 0x040029CC RID: 10700
		ObjectEnd,
		// Token: 0x040029CD RID: 10701
		ArrayStart,
		// Token: 0x040029CE RID: 10702
		ArrayEnd,
		// Token: 0x040029CF RID: 10703
		Int,
		// Token: 0x040029D0 RID: 10704
		Long,
		// Token: 0x040029D1 RID: 10705
		Double,
		// Token: 0x040029D2 RID: 10706
		String,
		// Token: 0x040029D3 RID: 10707
		Boolean,
		// Token: 0x040029D4 RID: 10708
		Null
	}
}

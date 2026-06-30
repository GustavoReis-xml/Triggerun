using System;

namespace YouMe
{
	// Token: 0x020004F3 RID: 1267
	internal enum ParserToken
	{
		// Token: 0x040029FC RID: 10748
		None = 65536,
		// Token: 0x040029FD RID: 10749
		Number,
		// Token: 0x040029FE RID: 10750
		True,
		// Token: 0x040029FF RID: 10751
		False,
		// Token: 0x04002A00 RID: 10752
		Null,
		// Token: 0x04002A01 RID: 10753
		CharSeq,
		// Token: 0x04002A02 RID: 10754
		Char,
		// Token: 0x04002A03 RID: 10755
		Text,
		// Token: 0x04002A04 RID: 10756
		Object,
		// Token: 0x04002A05 RID: 10757
		ObjectPrime,
		// Token: 0x04002A06 RID: 10758
		Pair,
		// Token: 0x04002A07 RID: 10759
		PairRest,
		// Token: 0x04002A08 RID: 10760
		Array,
		// Token: 0x04002A09 RID: 10761
		ArrayPrime,
		// Token: 0x04002A0A RID: 10762
		Value,
		// Token: 0x04002A0B RID: 10763
		ValueRest,
		// Token: 0x04002A0C RID: 10764
		String,
		// Token: 0x04002A0D RID: 10765
		End,
		// Token: 0x04002A0E RID: 10766
		Epsilon
	}
}

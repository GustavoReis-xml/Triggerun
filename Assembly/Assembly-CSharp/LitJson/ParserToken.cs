using System;

namespace LitJson
{
	// Token: 0x0200050F RID: 1295
	internal enum ParserToken
	{
		// Token: 0x04002B05 RID: 11013
		None = 65536,
		// Token: 0x04002B06 RID: 11014
		Number,
		// Token: 0x04002B07 RID: 11015
		True,
		// Token: 0x04002B08 RID: 11016
		False,
		// Token: 0x04002B09 RID: 11017
		Null,
		// Token: 0x04002B0A RID: 11018
		CharSeq,
		// Token: 0x04002B0B RID: 11019
		Char,
		// Token: 0x04002B0C RID: 11020
		Text,
		// Token: 0x04002B0D RID: 11021
		Object,
		// Token: 0x04002B0E RID: 11022
		ObjectPrime,
		// Token: 0x04002B0F RID: 11023
		Pair,
		// Token: 0x04002B10 RID: 11024
		PairRest,
		// Token: 0x04002B11 RID: 11025
		Array,
		// Token: 0x04002B12 RID: 11026
		ArrayPrime,
		// Token: 0x04002B13 RID: 11027
		Value,
		// Token: 0x04002B14 RID: 11028
		ValueRest,
		// Token: 0x04002B15 RID: 11029
		String,
		// Token: 0x04002B16 RID: 11030
		End,
		// Token: 0x04002B17 RID: 11031
		Epsilon
	}
}

using System;

namespace YouMe
{
	// Token: 0x020004E9 RID: 1257
	public class JsonException : ApplicationException
	{
		// Token: 0x0600302E RID: 12334 RVA: 0x001AC411 File Offset: 0x001AA611
		public JsonException()
		{
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x001AC41B File Offset: 0x001AA61B
		internal JsonException(ParserToken token) : base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x001AC435 File Offset: 0x001AA635
		internal JsonException(ParserToken token, Exception inner_exception) : base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x001AC450 File Offset: 0x001AA650
		internal JsonException(int c) : base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x001AC46B File Offset: 0x001AA66B
		internal JsonException(int c, Exception inner_exception) : base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x001AC487 File Offset: 0x001AA687
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x001AC492 File Offset: 0x001AA692
		public JsonException(string message, Exception inner_exception) : base(message, inner_exception)
		{
		}
	}
}

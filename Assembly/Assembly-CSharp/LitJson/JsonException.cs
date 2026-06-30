using System;

namespace LitJson
{
	// Token: 0x02000506 RID: 1286
	public class JsonException : ApplicationException
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x001B27FD File Offset: 0x001B09FD
		public JsonException()
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x001B2807 File Offset: 0x001B0A07
		internal JsonException(ParserToken token) : base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x001B2821 File Offset: 0x001B0A21
		internal JsonException(ParserToken token, Exception inner_exception) : base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x001B283C File Offset: 0x001B0A3C
		internal JsonException(int c) : base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x001B2857 File Offset: 0x001B0A57
		internal JsonException(int c, Exception inner_exception) : base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x001B2873 File Offset: 0x001B0A73
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x001B287E File Offset: 0x001B0A7E
		public JsonException(string message, Exception inner_exception) : base(message, inner_exception)
		{
		}
	}
}

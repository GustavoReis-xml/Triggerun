using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace YouMe
{
	// Token: 0x020004EF RID: 1263
	public class JsonWriter
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x001AE9B0 File Offset: 0x001ACBB0
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x001AE9C8 File Offset: 0x001ACBC8
		public int IndentValue
		{
			get
			{
				return this.indent_value;
			}
			set
			{
				this.indentation = this.indentation / this.indent_value * value;
				this.indent_value = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x001AE9E8 File Offset: 0x001ACBE8
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x001AEA00 File Offset: 0x001ACC00
		public bool PrettyPrint
		{
			get
			{
				return this.pretty_print;
			}
			set
			{
				this.pretty_print = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x001AEA0A File Offset: 0x001ACC0A
		public TextWriter TextWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x001AEA14 File Offset: 0x001ACC14
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x001AEA2C File Offset: 0x001ACC2C
		public bool Validate
		{
			get
			{
				return this.validate;
			}
			set
			{
				this.validate = value;
			}
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x001AEA43 File Offset: 0x001ACC43
		public JsonWriter()
		{
			this.inst_string_builder = new StringBuilder();
			this.writer = new StringWriter(this.inst_string_builder);
			this.Init();
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x001AEA70 File Offset: 0x001ACC70
		public JsonWriter(StringBuilder sb) : this(new StringWriter(sb))
		{
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x001AEA80 File Offset: 0x001ACC80
		public JsonWriter(TextWriter writer)
		{
			bool flag = writer == null;
			if (flag)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.Init();
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x001AEAB8 File Offset: 0x001ACCB8
		private void DoValidation(Condition cond)
		{
			bool flag = !this.context.ExpectingValue;
			if (flag)
			{
				this.context.Count++;
			}
			bool flag2 = !this.validate;
			if (!flag2)
			{
				bool flag3 = this.has_reached_end;
				if (flag3)
				{
					throw new JsonException("A complete JSON symbol has already been written");
				}
				switch (cond)
				{
				case Condition.InArray:
				{
					bool flag4 = !this.context.InArray;
					if (flag4)
					{
						throw new JsonException("Can't close an array here");
					}
					break;
				}
				case Condition.InObject:
				{
					bool flag5 = !this.context.InObject || this.context.ExpectingValue;
					if (flag5)
					{
						throw new JsonException("Can't close an object here");
					}
					break;
				}
				case Condition.NotAProperty:
				{
					bool flag6 = this.context.InObject && !this.context.ExpectingValue;
					if (flag6)
					{
						throw new JsonException("Expected a property");
					}
					break;
				}
				case Condition.Property:
				{
					bool flag7 = !this.context.InObject || this.context.ExpectingValue;
					if (flag7)
					{
						throw new JsonException("Can't add a property here");
					}
					break;
				}
				case Condition.Value:
				{
					bool flag8 = !this.context.InArray && (!this.context.InObject || !this.context.ExpectingValue);
					if (flag8)
					{
						throw new JsonException("Can't add a value here");
					}
					break;
				}
				}
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x001AEC38 File Offset: 0x001ACE38
		private void Init()
		{
			this.has_reached_end = false;
			this.hex_seq = new char[4];
			this.indentation = 0;
			this.indent_value = 4;
			this.pretty_print = false;
			this.validate = true;
			this.ctx_stack = new Stack<WriterContext>();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x001AECA0 File Offset: 0x001ACEA0
		private static void IntToHex(int n, char[] hex)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = n % 16;
				bool flag = num < 10;
				if (flag)
				{
					hex[3 - i] = (char)(48 + num);
				}
				else
				{
					hex[3 - i] = (char)(65 + (num - 10));
				}
				n >>= 4;
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x001AECF0 File Offset: 0x001ACEF0
		private void Indent()
		{
			bool flag = this.pretty_print;
			if (flag)
			{
				this.indentation += this.indent_value;
			}
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x001AED20 File Offset: 0x001ACF20
		private void Put(string str)
		{
			bool flag = this.pretty_print && !this.context.ExpectingValue;
			if (flag)
			{
				for (int i = 0; i < this.indentation; i++)
				{
					this.writer.Write(' ');
				}
			}
			this.writer.Write(str);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x001AED7F File Offset: 0x001ACF7F
		private void PutNewline()
		{
			this.PutNewline(true);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x001AED8C File Offset: 0x001ACF8C
		private void PutNewline(bool add_comma)
		{
			bool flag = add_comma && !this.context.ExpectingValue && this.context.Count > 1;
			if (flag)
			{
				this.writer.Write(',');
			}
			bool flag2 = this.pretty_print && !this.context.ExpectingValue;
			if (flag2)
			{
				this.writer.Write('\n');
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x001AEDFC File Offset: 0x001ACFFC
		private void PutString(string str)
		{
			this.Put(string.Empty);
			this.writer.Write('"');
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				char c2 = c;
				switch (c2)
				{
				case '\b':
					this.writer.Write("\\b");
					break;
				case '\t':
					this.writer.Write("\\t");
					break;
				case '\n':
					this.writer.Write("\\n");
					break;
				case '\v':
					goto IL_F8;
				case '\f':
					this.writer.Write("\\f");
					break;
				case '\r':
					this.writer.Write("\\r");
					break;
				default:
					if (c2 != '"' && c2 != '\\')
					{
						goto IL_F8;
					}
					this.writer.Write('\\');
					this.writer.Write(str[i]);
					break;
				}
				IL_167:
				i++;
				continue;
				IL_F8:
				bool flag = str[i] >= ' ' && str[i] <= '~';
				if (flag)
				{
					this.writer.Write(str[i]);
					goto IL_167;
				}
				JsonWriter.IntToHex((int)str[i], this.hex_seq);
				this.writer.Write("\\u");
				this.writer.Write(this.hex_seq);
				goto IL_167;
			}
			this.writer.Write('"');
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x001AEF90 File Offset: 0x001AD190
		private void Unindent()
		{
			bool flag = this.pretty_print;
			if (flag)
			{
				this.indentation -= this.indent_value;
			}
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x001AEFC0 File Offset: 0x001AD1C0
		public override string ToString()
		{
			bool flag = this.inst_string_builder == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = this.inst_string_builder.ToString();
			}
			return result;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x001AEFF4 File Offset: 0x001AD1F4
		public void Reset()
		{
			this.has_reached_end = false;
			this.ctx_stack.Clear();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
			bool flag = this.inst_string_builder != null;
			if (flag)
			{
				this.inst_string_builder.Remove(0, this.inst_string_builder.Length);
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x001AF059 File Offset: 0x001AD259
		public void Write(bool boolean)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put((!boolean) ? "false" : "true");
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x001AF08D File Offset: 0x001AD28D
		public void Write(decimal number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x001AF0C0 File Offset: 0x001AD2C0
		public void Write(double number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			string text = Convert.ToString(number, JsonWriter.number_format);
			this.Put(text);
			bool flag = text.IndexOf('.') == -1 && text.IndexOf('E') == -1;
			if (flag)
			{
				this.writer.Write(".0");
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x001AF12D File Offset: 0x001AD32D
		public void Write(int number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x001AF15D File Offset: 0x001AD35D
		public void Write(long number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x001AF190 File Offset: 0x001AD390
		public void Write(string str)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			bool flag = str == null;
			if (flag)
			{
				this.Put("null");
			}
			else
			{
				this.PutString(str);
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x001AF1DB File Offset: 0x001AD3DB
		[CLSCompliant(false)]
		public void Write(ulong number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x001AF20C File Offset: 0x001AD40C
		public void WriteArrayEnd()
		{
			this.DoValidation(Condition.InArray);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			bool flag = this.ctx_stack.Count == 1;
			if (flag)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("]");
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x001AF288 File Offset: 0x001AD488
		public void WriteArrayStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("[");
			this.context = new WriterContext();
			this.context.InArray = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x001AF2E4 File Offset: 0x001AD4E4
		public void WriteObjectEnd()
		{
			this.DoValidation(Condition.InObject);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			bool flag = this.ctx_stack.Count == 1;
			if (flag)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("}");
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x001AF360 File Offset: 0x001AD560
		public void WriteObjectStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("{");
			this.context = new WriterContext();
			this.context.InObject = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x001AF3BC File Offset: 0x001AD5BC
		public void WritePropertyName(string property_name)
		{
			this.DoValidation(Condition.Property);
			this.PutNewline();
			this.PutString(property_name);
			bool flag = this.pretty_print;
			if (flag)
			{
				bool flag2 = property_name.Length > this.context.Padding;
				if (flag2)
				{
					this.context.Padding = property_name.Length;
				}
				for (int i = this.context.Padding - property_name.Length; i >= 0; i--)
				{
					this.writer.Write(' ');
				}
				this.writer.Write(": ");
			}
			else
			{
				this.writer.Write(':');
			}
			this.context.ExpectingValue = true;
		}

		// Token: 0x040029DE RID: 10718
		private static NumberFormatInfo number_format = NumberFormatInfo.InvariantInfo;

		// Token: 0x040029DF RID: 10719
		private WriterContext context;

		// Token: 0x040029E0 RID: 10720
		private Stack<WriterContext> ctx_stack;

		// Token: 0x040029E1 RID: 10721
		private bool has_reached_end;

		// Token: 0x040029E2 RID: 10722
		private char[] hex_seq;

		// Token: 0x040029E3 RID: 10723
		private int indentation;

		// Token: 0x040029E4 RID: 10724
		private int indent_value;

		// Token: 0x040029E5 RID: 10725
		private StringBuilder inst_string_builder;

		// Token: 0x040029E6 RID: 10726
		private bool pretty_print;

		// Token: 0x040029E7 RID: 10727
		private bool validate;

		// Token: 0x040029E8 RID: 10728
		private TextWriter writer;
	}
}

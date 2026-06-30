using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LitJson
{
	// Token: 0x0200050B RID: 1291
	public class JsonWriter
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x001B4B18 File Offset: 0x001B2D18
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x001B4B30 File Offset: 0x001B2D30
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

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x001B4B50 File Offset: 0x001B2D50
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x001B4B68 File Offset: 0x001B2D68
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

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x001B4B72 File Offset: 0x001B2D72
		public TextWriter TextWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x001B4B7C File Offset: 0x001B2D7C
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x001B4B94 File Offset: 0x001B2D94
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

		// Token: 0x0600322C RID: 12844 RVA: 0x001B4BAB File Offset: 0x001B2DAB
		public JsonWriter()
		{
			this.inst_string_builder = new StringBuilder();
			this.writer = new StringWriter(this.inst_string_builder);
			this.Init();
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x001B4BD8 File Offset: 0x001B2DD8
		public JsonWriter(StringBuilder sb) : this(new StringWriter(sb))
		{
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x001B4BE8 File Offset: 0x001B2DE8
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

		// Token: 0x0600322F RID: 12847 RVA: 0x001B4C20 File Offset: 0x001B2E20
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

		// Token: 0x06003230 RID: 12848 RVA: 0x001B4DA0 File Offset: 0x001B2FA0
		private void Init()
		{
			this.has_reached_end = false;
			this.indentation = 0;
			this.indent_value = 4;
			this.pretty_print = false;
			this.validate = true;
			this.ctx_stack = new Stack<WriterContext>();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x001B4DFC File Offset: 0x001B2FFC
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

		// Token: 0x06003232 RID: 12850 RVA: 0x001B4E4C File Offset: 0x001B304C
		private void Indent()
		{
			bool flag = this.pretty_print;
			if (flag)
			{
				this.indentation += this.indent_value;
			}
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x001B4E7C File Offset: 0x001B307C
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

		// Token: 0x06003234 RID: 12852 RVA: 0x001B4EDB File Offset: 0x001B30DB
		private void PutNewline()
		{
			this.PutNewline(true);
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x001B4EE8 File Offset: 0x001B30E8
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

		// Token: 0x06003236 RID: 12854 RVA: 0x001B4F58 File Offset: 0x001B3158
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
					goto IL_EC;
				case '\f':
					this.writer.Write("\\f");
					break;
				case '\r':
					this.writer.Write("\\r");
					break;
				default:
					if (c2 != '"' && c2 != '\\')
					{
						goto IL_EC;
					}
					this.writer.Write('\\');
					this.writer.Write(str[i]);
					break;
				}
				IL_101:
				i++;
				continue;
				IL_EC:
				this.writer.Write(str[i]);
				goto IL_101;
			}
			this.writer.Write('"');
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x001B5088 File Offset: 0x001B3288
		private void Unindent()
		{
			bool flag = this.pretty_print;
			if (flag)
			{
				this.indentation -= this.indent_value;
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x001B50B8 File Offset: 0x001B32B8
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

		// Token: 0x06003239 RID: 12857 RVA: 0x001B50EC File Offset: 0x001B32EC
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

		// Token: 0x0600323A RID: 12858 RVA: 0x001B5151 File Offset: 0x001B3351
		public void Write(bool boolean)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put((!boolean) ? "false" : "true");
			this.context.ExpectingValue = false;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x001B5185 File Offset: 0x001B3385
		public void Write(decimal number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x001B51B8 File Offset: 0x001B33B8
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

		// Token: 0x0600323D RID: 12861 RVA: 0x001B5225 File Offset: 0x001B3425
		public void Write(int number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x001B5255 File Offset: 0x001B3455
		public void Write(long number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x001B5288 File Offset: 0x001B3488
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

		// Token: 0x06003240 RID: 12864 RVA: 0x001B52D3 File Offset: 0x001B34D3
		public void Write(ulong number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x001B5304 File Offset: 0x001B3504
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

		// Token: 0x06003242 RID: 12866 RVA: 0x001B5380 File Offset: 0x001B3580
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

		// Token: 0x06003243 RID: 12867 RVA: 0x001B53DC File Offset: 0x001B35DC
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

		// Token: 0x06003244 RID: 12868 RVA: 0x001B5458 File Offset: 0x001B3658
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

		// Token: 0x06003245 RID: 12869 RVA: 0x001B54B4 File Offset: 0x001B36B4
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

		// Token: 0x04002AE8 RID: 10984
		private static NumberFormatInfo number_format = NumberFormatInfo.InvariantInfo;

		// Token: 0x04002AE9 RID: 10985
		private WriterContext context;

		// Token: 0x04002AEA RID: 10986
		private Stack<WriterContext> ctx_stack;

		// Token: 0x04002AEB RID: 10987
		private bool has_reached_end;

		// Token: 0x04002AEC RID: 10988
		private int indentation;

		// Token: 0x04002AED RID: 10989
		private int indent_value;

		// Token: 0x04002AEE RID: 10990
		private StringBuilder inst_string_builder;

		// Token: 0x04002AEF RID: 10991
		private bool pretty_print;

		// Token: 0x04002AF0 RID: 10992
		private bool validate;

		// Token: 0x04002AF1 RID: 10993
		private TextWriter writer;
	}
}

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace LitJson
{
	// Token: 0x0200050C RID: 1292
	internal class Lexer
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x001B5574 File Offset: 0x001B3774
		// (set) Token: 0x06003247 RID: 12871 RVA: 0x001B558C File Offset: 0x001B378C
		public bool AllowComments
		{
			get
			{
				return this.allow_comments;
			}
			set
			{
				this.allow_comments = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x001B5598 File Offset: 0x001B3798
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x001B55B0 File Offset: 0x001B37B0
		public bool AllowSingleQuotedStrings
		{
			get
			{
				return this.allow_single_quoted_strings;
			}
			set
			{
				this.allow_single_quoted_strings = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x001B55BA File Offset: 0x001B37BA
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x001B55C2 File Offset: 0x001B37C2
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x001B55CA File Offset: 0x001B37CA
		public string StringValue
		{
			get
			{
				return this.string_value;
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x001B55D2 File Offset: 0x001B37D2
		static Lexer()
		{
			Lexer.PopulateFsmTables();
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x001B55DC File Offset: 0x001B37DC
		public Lexer(TextReader reader)
		{
			this.allow_comments = true;
			this.allow_single_quoted_strings = true;
			this.input_buffer = 0;
			this.string_buffer = new StringBuilder(128);
			this.state = 1;
			this.end_of_input = false;
			this.reader = reader;
			this.fsm_context = new FsmContext();
			this.fsm_context.L = this;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x001B5644 File Offset: 0x001B3844
		private static int HexValue(int digit)
		{
			switch (digit)
			{
			case 65:
				break;
			case 66:
				goto IL_50;
			case 67:
				goto IL_55;
			case 68:
				goto IL_5A;
			case 69:
				goto IL_5F;
			case 70:
				goto IL_64;
			default:
				switch (digit)
				{
				case 97:
					break;
				case 98:
					goto IL_50;
				case 99:
					goto IL_55;
				case 100:
					goto IL_5A;
				case 101:
					goto IL_5F;
				case 102:
					goto IL_64;
				default:
					return digit - 48;
				}
				break;
			}
			return 10;
			IL_50:
			return 11;
			IL_55:
			return 12;
			IL_5A:
			return 13;
			IL_5F:
			return 14;
			IL_64:
			return 15;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x001B56C4 File Offset: 0x001B38C4
		private static void PopulateFsmTables()
		{
			Lexer.StateHandler[] array = new Lexer.StateHandler[28];
			int num = 0;
			Lexer.StateHandler stateHandler;
			if ((stateHandler = Lexer.O_cache._State_0_State1) == null)
			{
				stateHandler = (Lexer.O_cache._State_0_State1 = new Lexer.StateHandler(Lexer.State1));
			}
			array[num] = stateHandler;
			int num2 = 1;
			Lexer.StateHandler stateHandler2;
			if ((stateHandler2 = Lexer.O_cache._State_1_State2) == null)
			{
				stateHandler2 = (Lexer.O_cache._State_1_State2 = new Lexer.StateHandler(Lexer.State2));
			}
			array[num2] = stateHandler2;
			int num3 = 2;
			Lexer.StateHandler stateHandler3;
			if ((stateHandler3 = Lexer.O_cache._State_2_State3) == null)
			{
				stateHandler3 = (Lexer.O_cache._State_2_State3 = new Lexer.StateHandler(Lexer.State3));
			}
			array[num3] = stateHandler3;
			int num4 = 3;
			Lexer.StateHandler stateHandler4;
			if ((stateHandler4 = Lexer.O_cache._State_3_State4) == null)
			{
				stateHandler4 = (Lexer.O_cache._State_3_State4 = new Lexer.StateHandler(Lexer.State4));
			}
			array[num4] = stateHandler4;
			int num5 = 4;
			Lexer.StateHandler stateHandler5;
			if ((stateHandler5 = Lexer.O_cache._State_4_State5) == null)
			{
				stateHandler5 = (Lexer.O_cache._State_4_State5 = new Lexer.StateHandler(Lexer.State5));
			}
			array[num5] = stateHandler5;
			int num6 = 5;
			Lexer.StateHandler stateHandler6;
			if ((stateHandler6 = Lexer.O_cache._State_5_State6) == null)
			{
				stateHandler6 = (Lexer.O_cache._State_5_State6 = new Lexer.StateHandler(Lexer.State6));
			}
			array[num6] = stateHandler6;
			int num7 = 6;
			Lexer.StateHandler stateHandler7;
			if ((stateHandler7 = Lexer.O_cache._State_6_State7) == null)
			{
				stateHandler7 = (Lexer.O_cache._State_6_State7 = new Lexer.StateHandler(Lexer.State7));
			}
			array[num7] = stateHandler7;
			int num8 = 7;
			Lexer.StateHandler stateHandler8;
			if ((stateHandler8 = Lexer.O_cache._State_7_State8) == null)
			{
				stateHandler8 = (Lexer.O_cache._State_7_State8 = new Lexer.StateHandler(Lexer.State8));
			}
			array[num8] = stateHandler8;
			int num9 = 8;
			Lexer.StateHandler stateHandler9;
			if ((stateHandler9 = Lexer.O_cache._State_8_State9) == null)
			{
				stateHandler9 = (Lexer.O_cache._State_8_State9 = new Lexer.StateHandler(Lexer.State9));
			}
			array[num9] = stateHandler9;
			int num10 = 9;
			Lexer.StateHandler stateHandler10;
			if ((stateHandler10 = Lexer.O_cache._State_9_State10) == null)
			{
				stateHandler10 = (Lexer.O_cache._State_9_State10 = new Lexer.StateHandler(Lexer.State10));
			}
			array[num10] = stateHandler10;
			int num11 = 10;
			Lexer.StateHandler stateHandler11;
			if ((stateHandler11 = Lexer.O_cache._State_10_State11) == null)
			{
				stateHandler11 = (Lexer.O_cache._State_10_State11 = new Lexer.StateHandler(Lexer.State11));
			}
			array[num11] = stateHandler11;
			int num12 = 11;
			Lexer.StateHandler stateHandler12;
			if ((stateHandler12 = Lexer.O_cache._State_11_State12) == null)
			{
				stateHandler12 = (Lexer.O_cache._State_11_State12 = new Lexer.StateHandler(Lexer.State12));
			}
			array[num12] = stateHandler12;
			int num13 = 12;
			Lexer.StateHandler stateHandler13;
			if ((stateHandler13 = Lexer.O_cache._State_12_State13) == null)
			{
				stateHandler13 = (Lexer.O_cache._State_12_State13 = new Lexer.StateHandler(Lexer.State13));
			}
			array[num13] = stateHandler13;
			int num14 = 13;
			Lexer.StateHandler stateHandler14;
			if ((stateHandler14 = Lexer.O_cache._State_13_State14) == null)
			{
				stateHandler14 = (Lexer.O_cache._State_13_State14 = new Lexer.StateHandler(Lexer.State14));
			}
			array[num14] = stateHandler14;
			int num15 = 14;
			Lexer.StateHandler stateHandler15;
			if ((stateHandler15 = Lexer.O_cache._State_14_State15) == null)
			{
				stateHandler15 = (Lexer.O_cache._State_14_State15 = new Lexer.StateHandler(Lexer.State15));
			}
			array[num15] = stateHandler15;
			int num16 = 15;
			Lexer.StateHandler stateHandler16;
			if ((stateHandler16 = Lexer.O_cache._State_15_State16) == null)
			{
				stateHandler16 = (Lexer.O_cache._State_15_State16 = new Lexer.StateHandler(Lexer.State16));
			}
			array[num16] = stateHandler16;
			int num17 = 16;
			Lexer.StateHandler stateHandler17;
			if ((stateHandler17 = Lexer.O_cache._State_16_State17) == null)
			{
				stateHandler17 = (Lexer.O_cache._State_16_State17 = new Lexer.StateHandler(Lexer.State17));
			}
			array[num17] = stateHandler17;
			int num18 = 17;
			Lexer.StateHandler stateHandler18;
			if ((stateHandler18 = Lexer.O_cache._State_17_State18) == null)
			{
				stateHandler18 = (Lexer.O_cache._State_17_State18 = new Lexer.StateHandler(Lexer.State18));
			}
			array[num18] = stateHandler18;
			int num19 = 18;
			Lexer.StateHandler stateHandler19;
			if ((stateHandler19 = Lexer.O_cache._State_18_State19) == null)
			{
				stateHandler19 = (Lexer.O_cache._State_18_State19 = new Lexer.StateHandler(Lexer.State19));
			}
			array[num19] = stateHandler19;
			int num20 = 19;
			Lexer.StateHandler stateHandler20;
			if ((stateHandler20 = Lexer.O_cache._State_19_State20) == null)
			{
				stateHandler20 = (Lexer.O_cache._State_19_State20 = new Lexer.StateHandler(Lexer.State20));
			}
			array[num20] = stateHandler20;
			int num21 = 20;
			Lexer.StateHandler stateHandler21;
			if ((stateHandler21 = Lexer.O_cache._State_20_State21) == null)
			{
				stateHandler21 = (Lexer.O_cache._State_20_State21 = new Lexer.StateHandler(Lexer.State21));
			}
			array[num21] = stateHandler21;
			int num22 = 21;
			Lexer.StateHandler stateHandler22;
			if ((stateHandler22 = Lexer.O_cache._State_21_State22) == null)
			{
				stateHandler22 = (Lexer.O_cache._State_21_State22 = new Lexer.StateHandler(Lexer.State22));
			}
			array[num22] = stateHandler22;
			int num23 = 22;
			Lexer.StateHandler stateHandler23;
			if ((stateHandler23 = Lexer.O_cache._State_22_State23) == null)
			{
				stateHandler23 = (Lexer.O_cache._State_22_State23 = new Lexer.StateHandler(Lexer.State23));
			}
			array[num23] = stateHandler23;
			int num24 = 23;
			Lexer.StateHandler stateHandler24;
			if ((stateHandler24 = Lexer.O_cache._State_23_State24) == null)
			{
				stateHandler24 = (Lexer.O_cache._State_23_State24 = new Lexer.StateHandler(Lexer.State24));
			}
			array[num24] = stateHandler24;
			int num25 = 24;
			Lexer.StateHandler stateHandler25;
			if ((stateHandler25 = Lexer.O_cache._State_24_State25) == null)
			{
				stateHandler25 = (Lexer.O_cache._State_24_State25 = new Lexer.StateHandler(Lexer.State25));
			}
			array[num25] = stateHandler25;
			int num26 = 25;
			Lexer.StateHandler stateHandler26;
			if ((stateHandler26 = Lexer.O_cache._State_25_State26) == null)
			{
				stateHandler26 = (Lexer.O_cache._State_25_State26 = new Lexer.StateHandler(Lexer.State26));
			}
			array[num26] = stateHandler26;
			int num27 = 26;
			Lexer.StateHandler stateHandler27;
			if ((stateHandler27 = Lexer.O_cache._State_26_State27) == null)
			{
				stateHandler27 = (Lexer.O_cache._State_26_State27 = new Lexer.StateHandler(Lexer.State27));
			}
			array[num27] = stateHandler27;
			int num28 = 27;
			Lexer.StateHandler stateHandler28;
			if ((stateHandler28 = Lexer.O_cache._State_27_State28) == null)
			{
				stateHandler28 = (Lexer.O_cache._State_27_State28 = new Lexer.StateHandler(Lexer.State28));
			}
			array[num28] = stateHandler28;
			Lexer.fsm_handler_table = array;
			Lexer.fsm_return_table = new int[]
			{
				65542,
				0,
				65537,
				65537,
				0,
				65537,
				0,
				65537,
				0,
				0,
				65538,
				0,
				0,
				0,
				65539,
				0,
				0,
				65540,
				65541,
				65542,
				0,
				0,
				65541,
				65542,
				0,
				0,
				0,
				0
			};
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x001B5A50 File Offset: 0x001B3C50
		private static char ProcessEscChar(int esc_char)
		{
			if (esc_char <= 92)
			{
				if (esc_char <= 39)
				{
					if (esc_char != 34 && esc_char != 39)
					{
						goto IL_74;
					}
				}
				else if (esc_char != 47 && esc_char != 92)
				{
					goto IL_74;
				}
				return Convert.ToChar(esc_char);
			}
			if (esc_char <= 102)
			{
				if (esc_char == 98)
				{
					return '\b';
				}
				if (esc_char == 102)
				{
					return '\f';
				}
			}
			else
			{
				if (esc_char == 110)
				{
					return '\n';
				}
				if (esc_char == 114)
				{
					return '\r';
				}
				if (esc_char == 116)
				{
					return '\t';
				}
			}
			IL_74:
			return '?';
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x001B5AD8 File Offset: 0x001B3CD8
		private static bool State1(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13);
				if (!flag)
				{
					bool flag2 = ctx.L.input_char >= 49 && ctx.L.input_char <= 57;
					bool result;
					if (flag2)
					{
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						ctx.NextState = 3;
						result = true;
					}
					else
					{
						int num = ctx.L.input_char;
						int num2 = num;
						if (num2 <= 91)
						{
							if (num2 <= 39)
							{
								if (num2 == 34)
								{
									ctx.NextState = 19;
									ctx.Return = true;
									return true;
								}
								if (num2 != 39)
								{
									goto IL_240;
								}
								bool flag3 = !ctx.L.allow_single_quoted_strings;
								if (flag3)
								{
									return false;
								}
								ctx.L.input_char = 34;
								ctx.NextState = 23;
								ctx.Return = true;
								return true;
							}
							else
							{
								switch (num2)
								{
								case 44:
									break;
								case 45:
									ctx.L.string_buffer.Append((char)ctx.L.input_char);
									ctx.NextState = 2;
									return true;
								case 46:
									goto IL_240;
								case 47:
								{
									bool flag4 = !ctx.L.allow_comments;
									if (flag4)
									{
										return false;
									}
									ctx.NextState = 25;
									return true;
								}
								case 48:
									ctx.L.string_buffer.Append((char)ctx.L.input_char);
									ctx.NextState = 4;
									return true;
								default:
									if (num2 != 58 && num2 != 91)
									{
										goto IL_240;
									}
									break;
								}
							}
						}
						else if (num2 <= 110)
						{
							if (num2 != 93)
							{
								if (num2 == 102)
								{
									ctx.NextState = 12;
									return true;
								}
								if (num2 != 110)
								{
									goto IL_240;
								}
								ctx.NextState = 16;
								return true;
							}
						}
						else
						{
							if (num2 == 116)
							{
								ctx.NextState = 9;
								return true;
							}
							if (num2 != 123 && num2 != 125)
							{
								goto IL_240;
							}
						}
						ctx.NextState = 1;
						ctx.Return = true;
						return true;
						IL_240:
						result = false;
					}
					return result;
				}
			}
			return true;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x001B5D44 File Offset: 0x001B3F44
		private static bool State2(FsmContext ctx)
		{
			ctx.L.GetChar();
			bool flag = ctx.L.input_char >= 49 && ctx.L.input_char <= 57;
			bool result;
			if (flag)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 3;
				result = true;
			}
			else
			{
				int num = ctx.L.input_char;
				bool flag2 = num == 48;
				if (flag2)
				{
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
					ctx.NextState = 4;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x001B5DF4 File Offset: 0x001B3FF4
		private static bool State3(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char >= 48 && ctx.L.input_char <= 57;
				if (!flag)
				{
					bool flag2 = ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13);
					bool result;
					if (flag2)
					{
						ctx.Return = true;
						ctx.NextState = 1;
						result = true;
					}
					else
					{
						int num = ctx.L.input_char;
						int num2 = num;
						if (num2 <= 69)
						{
							if (num2 != 44)
							{
								if (num2 == 46)
								{
									ctx.L.string_buffer.Append((char)ctx.L.input_char);
									ctx.NextState = 5;
									return true;
								}
								if (num2 != 69)
								{
									goto IL_152;
								}
								goto IL_12A;
							}
						}
						else if (num2 != 93)
						{
							if (num2 == 101)
							{
								goto IL_12A;
							}
							if (num2 != 125)
							{
								goto IL_152;
							}
						}
						ctx.L.UngetChar();
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
						IL_12A:
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						ctx.NextState = 7;
						return true;
						IL_152:
						result = false;
					}
					return result;
				}
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
			}
			return true;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x001B5F70 File Offset: 0x001B4170
		private static bool State4(FsmContext ctx)
		{
			ctx.L.GetChar();
			bool flag = ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13);
			bool result;
			if (flag)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				int num = ctx.L.input_char;
				int num2 = num;
				if (num2 <= 69)
				{
					if (num2 != 44)
					{
						if (num2 == 46)
						{
							ctx.L.string_buffer.Append((char)ctx.L.input_char);
							ctx.NextState = 5;
							return true;
						}
						if (num2 != 69)
						{
							goto IL_10B;
						}
						goto IL_E3;
					}
				}
				else if (num2 != 93)
				{
					if (num2 == 101)
					{
						goto IL_E3;
					}
					if (num2 != 125)
					{
						goto IL_10B;
					}
				}
				ctx.L.UngetChar();
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
				IL_E3:
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 7;
				return true;
				IL_10B:
				result = false;
			}
			return result;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001B6090 File Offset: 0x001B4290
		private static bool State5(FsmContext ctx)
		{
			ctx.L.GetChar();
			bool flag = ctx.L.input_char >= 48 && ctx.L.input_char <= 57;
			bool result;
			if (flag)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 6;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x001B6100 File Offset: 0x001B4300
		private static bool State6(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char >= 48 && ctx.L.input_char <= 57;
				if (!flag)
				{
					bool flag2 = ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13);
					bool result;
					if (flag2)
					{
						ctx.Return = true;
						ctx.NextState = 1;
						result = true;
					}
					else
					{
						int num = ctx.L.input_char;
						int num2 = num;
						if (num2 <= 69)
						{
							if (num2 != 44)
							{
								if (num2 != 69)
								{
									goto IL_120;
								}
								goto IL_F8;
							}
						}
						else if (num2 != 93)
						{
							if (num2 == 101)
							{
								goto IL_F8;
							}
							if (num2 != 125)
							{
								goto IL_120;
							}
						}
						ctx.L.UngetChar();
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
						IL_F8:
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						ctx.NextState = 7;
						return true;
						IL_120:
						result = false;
					}
					return result;
				}
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
			}
			return true;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x001B624C File Offset: 0x001B444C
		private static bool State7(FsmContext ctx)
		{
			ctx.L.GetChar();
			bool flag = ctx.L.input_char >= 48 && ctx.L.input_char <= 57;
			bool result;
			if (flag)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 8;
				result = true;
			}
			else
			{
				int num = ctx.L.input_char;
				bool flag2 = num == 43 || num == 45;
				if (flag2)
				{
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
					ctx.NextState = 8;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x001B6304 File Offset: 0x001B4504
		private static bool State8(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char >= 48 && ctx.L.input_char <= 57;
				if (!flag)
				{
					bool flag2 = ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13);
					bool result;
					if (flag2)
					{
						ctx.Return = true;
						ctx.NextState = 1;
						result = true;
					}
					else
					{
						int num = ctx.L.input_char;
						bool flag3 = num == 44 || num == 93 || num == 125;
						if (flag3)
						{
							ctx.L.UngetChar();
							ctx.Return = true;
							ctx.NextState = 1;
							result = true;
						}
						else
						{
							result = false;
						}
					}
					return result;
				}
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
			}
			return true;
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x001B6410 File Offset: 0x001B4610
		private static bool State9(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 114;
			bool result;
			if (flag)
			{
				ctx.NextState = 10;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x001B6454 File Offset: 0x001B4654
		private static bool State10(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 117;
			bool result;
			if (flag)
			{
				ctx.NextState = 11;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x001B6498 File Offset: 0x001B4698
		private static bool State11(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 101;
			bool result;
			if (flag)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x001B64E0 File Offset: 0x001B46E0
		private static bool State12(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 97;
			bool result;
			if (flag)
			{
				ctx.NextState = 13;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x001B6524 File Offset: 0x001B4724
		private static bool State13(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 108;
			bool result;
			if (flag)
			{
				ctx.NextState = 14;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x001B6568 File Offset: 0x001B4768
		private static bool State14(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 115;
			bool result;
			if (flag)
			{
				ctx.NextState = 15;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x001B65AC File Offset: 0x001B47AC
		private static bool State15(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 101;
			bool result;
			if (flag)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x001B65F4 File Offset: 0x001B47F4
		private static bool State16(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 117;
			bool result;
			if (flag)
			{
				ctx.NextState = 17;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x001B6638 File Offset: 0x001B4838
		private static bool State17(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 108;
			bool result;
			if (flag)
			{
				ctx.NextState = 18;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x001B667C File Offset: 0x001B487C
		private static bool State18(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 108;
			bool result;
			if (flag)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x001B66C4 File Offset: 0x001B48C4
		private static bool State19(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				int num = ctx.L.input_char;
				int num2 = num;
				bool result;
				if (num2 != 34)
				{
					if (num2 != 92)
					{
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						continue;
					}
					ctx.StateStack = 19;
					ctx.NextState = 21;
					result = true;
				}
				else
				{
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 20;
					result = true;
				}
				return result;
			}
			return true;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x001B6758 File Offset: 0x001B4958
		private static bool State20(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 34;
			bool result;
			if (flag)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x001B67A0 File Offset: 0x001B49A0
		private static bool State21(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			int num2 = num;
			if (num2 <= 92)
			{
				if (num2 <= 39)
				{
					if (num2 != 34 && num2 != 39)
					{
						goto IL_B6;
					}
				}
				else if (num2 != 47 && num2 != 92)
				{
					goto IL_B6;
				}
			}
			else if (num2 <= 102)
			{
				if (num2 != 98 && num2 != 102)
				{
					goto IL_B6;
				}
			}
			else if (num2 != 110)
			{
				switch (num2)
				{
				case 114:
				case 116:
					break;
				case 115:
					goto IL_B6;
				case 117:
					ctx.NextState = 22;
					return true;
				default:
					goto IL_B6;
				}
			}
			ctx.L.string_buffer.Append(Lexer.ProcessEscChar(ctx.L.input_char));
			ctx.NextState = ctx.StateStack;
			return true;
			IL_B6:
			return false;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x001B6868 File Offset: 0x001B4A68
		private static bool State22(FsmContext ctx)
		{
			int num = 0;
			int num2 = 4096;
			ctx.L.unichar = 0;
			while (ctx.L.GetChar())
			{
				bool flag = (ctx.L.input_char >= 48 && ctx.L.input_char <= 57) || (ctx.L.input_char >= 65 && ctx.L.input_char <= 70) || (ctx.L.input_char >= 97 && ctx.L.input_char <= 102);
				bool result;
				if (flag)
				{
					ctx.L.unichar += Lexer.HexValue(ctx.L.input_char) * num2;
					num++;
					num2 /= 16;
					bool flag2 = num == 4;
					if (!flag2)
					{
						continue;
					}
					ctx.L.string_buffer.Append(Convert.ToChar(ctx.L.unichar));
					ctx.NextState = ctx.StateStack;
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}
			return true;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x001B6984 File Offset: 0x001B4B84
		private static bool State23(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				int num = ctx.L.input_char;
				int num2 = num;
				bool result;
				if (num2 != 39)
				{
					if (num2 != 92)
					{
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						continue;
					}
					ctx.StateStack = 23;
					ctx.NextState = 21;
					result = true;
				}
				else
				{
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 24;
					result = true;
				}
				return result;
			}
			return true;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x001B6A18 File Offset: 0x001B4C18
		private static bool State24(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			bool flag = num == 39;
			bool result;
			if (flag)
			{
				ctx.L.input_char = 34;
				ctx.Return = true;
				ctx.NextState = 1;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x001B6A6C File Offset: 0x001B4C6C
		private static bool State25(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			int num2 = num;
			bool result;
			if (num2 != 42)
			{
				if (num2 != 47)
				{
					result = false;
				}
				else
				{
					ctx.NextState = 26;
					result = true;
				}
			}
			else
			{
				ctx.NextState = 27;
				result = true;
			}
			return result;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x001B6AC0 File Offset: 0x001B4CC0
		private static bool State26(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char == 10;
				if (flag)
				{
					ctx.NextState = 1;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x001B6B08 File Offset: 0x001B4D08
		private static bool State27(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char == 42;
				if (flag)
				{
					ctx.NextState = 28;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x001B6B50 File Offset: 0x001B4D50
		private static bool State28(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				bool flag = ctx.L.input_char == 42;
				if (!flag)
				{
					bool flag2 = ctx.L.input_char == 47;
					bool result;
					if (flag2)
					{
						ctx.NextState = 1;
						result = true;
					}
					else
					{
						ctx.NextState = 27;
						result = true;
					}
					return result;
				}
			}
			return true;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x001B6BB8 File Offset: 0x001B4DB8
		private bool GetChar()
		{
			bool flag = (this.input_char = this.NextChar()) != -1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				this.end_of_input = true;
				result = false;
			}
			return result;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x001B6BF0 File Offset: 0x001B4DF0
		private int NextChar()
		{
			bool flag = this.input_buffer != 0;
			int result;
			if (flag)
			{
				int num = this.input_buffer;
				this.input_buffer = 0;
				result = num;
			}
			else
			{
				result = this.reader.Read();
			}
			return result;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x001B6C30 File Offset: 0x001B4E30
		public bool NextToken()
		{
			this.fsm_context.Return = false;
			for (;;)
			{
				Lexer.StateHandler stateHandler = Lexer.fsm_handler_table[this.state - 1];
				bool flag = !stateHandler(this.fsm_context);
				if (flag)
				{
					break;
				}
				bool flag2 = this.end_of_input;
				if (flag2)
				{
					goto Block_2;
				}
				bool @return = this.fsm_context.Return;
				if (@return)
				{
					goto Block_3;
				}
				this.state = this.fsm_context.NextState;
			}
			throw new JsonException(this.input_char);
			Block_2:
			return false;
			Block_3:
			this.string_value = this.string_buffer.ToString();
			this.string_buffer.Remove(0, this.string_buffer.Length);
			this.token = Lexer.fsm_return_table[this.state - 1];
			bool flag3 = this.token == 65542;
			if (flag3)
			{
				this.token = this.input_char;
			}
			this.state = this.fsm_context.NextState;
			return true;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x001B6D2D File Offset: 0x001B4F2D
		private void UngetChar()
		{
			this.input_buffer = this.input_char;
		}

		// Token: 0x04002AF2 RID: 10994
		private static int[] fsm_return_table;

		// Token: 0x04002AF3 RID: 10995
		private static Lexer.StateHandler[] fsm_handler_table;

		// Token: 0x04002AF4 RID: 10996
		private bool allow_comments;

		// Token: 0x04002AF5 RID: 10997
		private bool allow_single_quoted_strings;

		// Token: 0x04002AF6 RID: 10998
		private bool end_of_input;

		// Token: 0x04002AF7 RID: 10999
		private FsmContext fsm_context;

		// Token: 0x04002AF8 RID: 11000
		private int input_buffer;

		// Token: 0x04002AF9 RID: 11001
		private int input_char;

		// Token: 0x04002AFA RID: 11002
		private TextReader reader;

		// Token: 0x04002AFB RID: 11003
		private int state;

		// Token: 0x04002AFC RID: 11004
		private StringBuilder string_buffer;

		// Token: 0x04002AFD RID: 11005
		private string string_value;

		// Token: 0x04002AFE RID: 11006
		private int token;

		// Token: 0x04002AFF RID: 11007
		private int unichar;

		// Token: 0x020006A1 RID: 1697
		// (Invoke) Token: 0x060039C1 RID: 14785
		private delegate bool StateHandler(FsmContext ctx);

		// Token: 0x020006A2 RID: 1698
		[CompilerGenerated]
		private static class O_cache
		{
			// Token: 0x04003174 RID: 12660
			public static Lexer.StateHandler _State_0_State1;

			// Token: 0x04003175 RID: 12661
			public static Lexer.StateHandler _State_1_State2;

			// Token: 0x04003176 RID: 12662
			public static Lexer.StateHandler _State_2_State3;

			// Token: 0x04003177 RID: 12663
			public static Lexer.StateHandler _State_3_State4;

			// Token: 0x04003178 RID: 12664
			public static Lexer.StateHandler _State_4_State5;

			// Token: 0x04003179 RID: 12665
			public static Lexer.StateHandler _State_5_State6;

			// Token: 0x0400317A RID: 12666
			public static Lexer.StateHandler _State_6_State7;

			// Token: 0x0400317B RID: 12667
			public static Lexer.StateHandler _State_7_State8;

			// Token: 0x0400317C RID: 12668
			public static Lexer.StateHandler _State_8_State9;

			// Token: 0x0400317D RID: 12669
			public static Lexer.StateHandler _State_9_State10;

			// Token: 0x0400317E RID: 12670
			public static Lexer.StateHandler _State_10_State11;

			// Token: 0x0400317F RID: 12671
			public static Lexer.StateHandler _State_11_State12;

			// Token: 0x04003180 RID: 12672
			public static Lexer.StateHandler _State_12_State13;

			// Token: 0x04003181 RID: 12673
			public static Lexer.StateHandler _State_13_State14;

			// Token: 0x04003182 RID: 12674
			public static Lexer.StateHandler _State_14_State15;

			// Token: 0x04003183 RID: 12675
			public static Lexer.StateHandler _State_15_State16;

			// Token: 0x04003184 RID: 12676
			public static Lexer.StateHandler _State_16_State17;

			// Token: 0x04003185 RID: 12677
			public static Lexer.StateHandler _State_17_State18;

			// Token: 0x04003186 RID: 12678
			public static Lexer.StateHandler _State_18_State19;

			// Token: 0x04003187 RID: 12679
			public static Lexer.StateHandler _State_19_State20;

			// Token: 0x04003188 RID: 12680
			public static Lexer.StateHandler _State_20_State21;

			// Token: 0x04003189 RID: 12681
			public static Lexer.StateHandler _State_21_State22;

			// Token: 0x0400318A RID: 12682
			public static Lexer.StateHandler _State_22_State23;

			// Token: 0x0400318B RID: 12683
			public static Lexer.StateHandler _State_23_State24;

			// Token: 0x0400318C RID: 12684
			public static Lexer.StateHandler _State_24_State25;

			// Token: 0x0400318D RID: 12685
			public static Lexer.StateHandler _State_25_State26;

			// Token: 0x0400318E RID: 12686
			public static Lexer.StateHandler _State_26_State27;

			// Token: 0x0400318F RID: 12687
			public static Lexer.StateHandler _State_27_State28;
		}
	}
}

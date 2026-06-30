using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace YouMe
{
	// Token: 0x020004F0 RID: 1264
	internal class Lexer
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x001AF47C File Offset: 0x001AD67C
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x001AF494 File Offset: 0x001AD694
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

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x001AF4A0 File Offset: 0x001AD6A0
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x001AF4B8 File Offset: 0x001AD6B8
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x001AF4C2 File Offset: 0x001AD6C2
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x001AF4CA File Offset: 0x001AD6CA
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x001AF4D2 File Offset: 0x001AD6D2
		public string StringValue
		{
			get
			{
				return this.string_value;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x001AF4DA File Offset: 0x001AD6DA
		static Lexer()
		{
			Lexer.PopulateFsmTables();
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x001AF4E4 File Offset: 0x001AD6E4
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

		// Token: 0x060030C7 RID: 12487 RVA: 0x001AF54C File Offset: 0x001AD74C
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

		// Token: 0x060030C8 RID: 12488 RVA: 0x001AF5CC File Offset: 0x001AD7CC
		private static void PopulateFsmTables()
		{
			Lexer.StateHandler[] array = new Lexer.StateHandler[28];
			int num = 0;
			Lexer.StateHandler stateHandler;
			if ((stateHandler = Lexer.O_cache._State_0_1) == null)
			{
				stateHandler = (Lexer.O_cache._State_0_1 = new Lexer.StateHandler(Lexer.State1));
			}
			array[num] = stateHandler;
			int num2 = 1;
			Lexer.StateHandler stateHandler2;
			if ((stateHandler2 = Lexer.O_cache._State_1_2) == null)
			{
				stateHandler2 = (Lexer.O_cache._State_1_2 = new Lexer.StateHandler(Lexer.State2));
			}
			array[num2] = stateHandler2;
			int num3 = 2;
			Lexer.StateHandler stateHandler3;
			if ((stateHandler3 = Lexer.O_cache._State_2_3) == null)
			{
				stateHandler3 = (Lexer.O_cache._State_2_3 = new Lexer.StateHandler(Lexer.State3));
			}
			array[num3] = stateHandler3;
			int num4 = 3;
			Lexer.StateHandler stateHandler4;
			if ((stateHandler4 = Lexer.O_cache._State_3_4) == null)
			{
				stateHandler4 = (Lexer.O_cache._State_3_4 = new Lexer.StateHandler(Lexer.State4));
			}
			array[num4] = stateHandler4;
			int num5 = 4;
			Lexer.StateHandler stateHandler5;
			if ((stateHandler5 = Lexer.O_cache._State_4_5) == null)
			{
				stateHandler5 = (Lexer.O_cache._State_4_5 = new Lexer.StateHandler(Lexer.State5));
			}
			array[num5] = stateHandler5;
			int num6 = 5;
			Lexer.StateHandler stateHandler6;
			if ((stateHandler6 = Lexer.O_cache._State_5_6) == null)
			{
				stateHandler6 = (Lexer.O_cache._State_5_6 = new Lexer.StateHandler(Lexer.State6));
			}
			array[num6] = stateHandler6;
			int num7 = 6;
			Lexer.StateHandler stateHandler7;
			if ((stateHandler7 = Lexer.O_cache._State_6_7) == null)
			{
				stateHandler7 = (Lexer.O_cache._State_6_7 = new Lexer.StateHandler(Lexer.State7));
			}
			array[num7] = stateHandler7;
			int num8 = 7;
			Lexer.StateHandler stateHandler8;
			if ((stateHandler8 = Lexer.O_cache._State_7_8) == null)
			{
				stateHandler8 = (Lexer.O_cache._State_7_8 = new Lexer.StateHandler(Lexer.State8));
			}
			array[num8] = stateHandler8;
			int num9 = 8;
			Lexer.StateHandler stateHandler9;
			if ((stateHandler9 = Lexer.O_cache._State_8_9) == null)
			{
				stateHandler9 = (Lexer.O_cache._State_8_9 = new Lexer.StateHandler(Lexer.State9));
			}
			array[num9] = stateHandler9;
			int num10 = 9;
			Lexer.StateHandler stateHandler10;
			if ((stateHandler10 = Lexer.O_cache._State_9_10) == null)
			{
				stateHandler10 = (Lexer.O_cache._State_9_10 = new Lexer.StateHandler(Lexer.State10));
			}
			array[num10] = stateHandler10;
			int num11 = 10;
			Lexer.StateHandler stateHandler11;
			if ((stateHandler11 = Lexer.O_cache._State_10_11) == null)
			{
				stateHandler11 = (Lexer.O_cache._State_10_11 = new Lexer.StateHandler(Lexer.State11));
			}
			array[num11] = stateHandler11;
			int num12 = 11;
			Lexer.StateHandler stateHandler12;
			if ((stateHandler12 = Lexer.O_cache._State_11_12) == null)
			{
				stateHandler12 = (Lexer.O_cache._State_11_12 = new Lexer.StateHandler(Lexer.State12));
			}
			array[num12] = stateHandler12;
			int num13 = 12;
			Lexer.StateHandler stateHandler13;
			if ((stateHandler13 = Lexer.O_cache._State_12_13) == null)
			{
				stateHandler13 = (Lexer.O_cache._State_12_13 = new Lexer.StateHandler(Lexer.State13));
			}
			array[num13] = stateHandler13;
			int num14 = 13;
			Lexer.StateHandler stateHandler14;
			if ((stateHandler14 = Lexer.O_cache._State_13_14) == null)
			{
				stateHandler14 = (Lexer.O_cache._State_13_14 = new Lexer.StateHandler(Lexer.State14));
			}
			array[num14] = stateHandler14;
			int num15 = 14;
			Lexer.StateHandler stateHandler15;
			if ((stateHandler15 = Lexer.O_cache._State_14_15) == null)
			{
				stateHandler15 = (Lexer.O_cache._State_14_15 = new Lexer.StateHandler(Lexer.State15));
			}
			array[num15] = stateHandler15;
			int num16 = 15;
			Lexer.StateHandler stateHandler16;
			if ((stateHandler16 = Lexer.O_cache._State_15_16) == null)
			{
				stateHandler16 = (Lexer.O_cache._State_15_16 = new Lexer.StateHandler(Lexer.State16));
			}
			array[num16] = stateHandler16;
			int num17 = 16;
			Lexer.StateHandler stateHandler17;
			if ((stateHandler17 = Lexer.O_cache._State_16_17) == null)
			{
				stateHandler17 = (Lexer.O_cache._State_16_17 = new Lexer.StateHandler(Lexer.State17));
			}
			array[num17] = stateHandler17;
			int num18 = 17;
			Lexer.StateHandler stateHandler18;
			if ((stateHandler18 = Lexer.O_cache._State_17_18) == null)
			{
				stateHandler18 = (Lexer.O_cache._State_17_18 = new Lexer.StateHandler(Lexer.State18));
			}
			array[num18] = stateHandler18;
			int num19 = 18;
			Lexer.StateHandler stateHandler19;
			if ((stateHandler19 = Lexer.O_cache._State_18_19) == null)
			{
				stateHandler19 = (Lexer.O_cache._State_18_19 = new Lexer.StateHandler(Lexer.State19));
			}
			array[num19] = stateHandler19;
			int num20 = 19;
			Lexer.StateHandler stateHandler20;
			if ((stateHandler20 = Lexer.O_cache._State_19_20) == null)
			{
				stateHandler20 = (Lexer.O_cache._State_19_20 = new Lexer.StateHandler(Lexer.State20));
			}
			array[num20] = stateHandler20;
			int num21 = 20;
			Lexer.StateHandler stateHandler21;
			if ((stateHandler21 = Lexer.O_cache._State_20_21) == null)
			{
				stateHandler21 = (Lexer.O_cache._State_20_21 = new Lexer.StateHandler(Lexer.State21));
			}
			array[num21] = stateHandler21;
			int num22 = 21;
			Lexer.StateHandler stateHandler22;
			if ((stateHandler22 = Lexer.O_cache._State_21_22) == null)
			{
				stateHandler22 = (Lexer.O_cache._State_21_22 = new Lexer.StateHandler(Lexer.State22));
			}
			array[num22] = stateHandler22;
			int num23 = 22;
			Lexer.StateHandler stateHandler23;
			if ((stateHandler23 = Lexer.O_cache._State_22_23) == null)
			{
				stateHandler23 = (Lexer.O_cache._State_22_23 = new Lexer.StateHandler(Lexer.State23));
			}
			array[num23] = stateHandler23;
			int num24 = 23;
			Lexer.StateHandler stateHandler24;
			if ((stateHandler24 = Lexer.O_cache._State_23_24) == null)
			{
				stateHandler24 = (Lexer.O_cache._State_23_24 = new Lexer.StateHandler(Lexer.State24));
			}
			array[num24] = stateHandler24;
			int num25 = 24;
			Lexer.StateHandler stateHandler25;
			if ((stateHandler25 = Lexer.O_cache._State_24_25) == null)
			{
				stateHandler25 = (Lexer.O_cache._State_24_25 = new Lexer.StateHandler(Lexer.State25));
			}
			array[num25] = stateHandler25;
			int num26 = 25;
			Lexer.StateHandler stateHandler26;
			if ((stateHandler26 = Lexer.O_cache._State_25_26) == null)
			{
				stateHandler26 = (Lexer.O_cache._State_25_26 = new Lexer.StateHandler(Lexer.State26));
			}
			array[num26] = stateHandler26;
			int num27 = 26;
			Lexer.StateHandler stateHandler27;
			if ((stateHandler27 = Lexer.O_cache._State_26_27) == null)
			{
				stateHandler27 = (Lexer.O_cache._State_26_27 = new Lexer.StateHandler(Lexer.State27));
			}
			array[num27] = stateHandler27;
			int num28 = 27;
			Lexer.StateHandler stateHandler28;
			if ((stateHandler28 = Lexer.O_cache._State_27_28) == null)
			{
				stateHandler28 = (Lexer.O_cache._State_27_28 = new Lexer.StateHandler(Lexer.State28));
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

		// Token: 0x060030C9 RID: 12489 RVA: 0x001AF958 File Offset: 0x001ADB58
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

		// Token: 0x060030CA RID: 12490 RVA: 0x001AF9E0 File Offset: 0x001ADBE0
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

		// Token: 0x060030CB RID: 12491 RVA: 0x001AFC4C File Offset: 0x001ADE4C
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

		// Token: 0x060030CC RID: 12492 RVA: 0x001AFCFC File Offset: 0x001ADEFC
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

		// Token: 0x060030CD RID: 12493 RVA: 0x001AFE78 File Offset: 0x001AE078
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

		// Token: 0x060030CE RID: 12494 RVA: 0x001AFF98 File Offset: 0x001AE198
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

		// Token: 0x060030CF RID: 12495 RVA: 0x001B0008 File Offset: 0x001AE208
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

		// Token: 0x060030D0 RID: 12496 RVA: 0x001B0154 File Offset: 0x001AE354
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

		// Token: 0x060030D1 RID: 12497 RVA: 0x001B020C File Offset: 0x001AE40C
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

		// Token: 0x060030D2 RID: 12498 RVA: 0x001B0318 File Offset: 0x001AE518
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

		// Token: 0x060030D3 RID: 12499 RVA: 0x001B035C File Offset: 0x001AE55C
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

		// Token: 0x060030D4 RID: 12500 RVA: 0x001B03A0 File Offset: 0x001AE5A0
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

		// Token: 0x060030D5 RID: 12501 RVA: 0x001B03E8 File Offset: 0x001AE5E8
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

		// Token: 0x060030D6 RID: 12502 RVA: 0x001B042C File Offset: 0x001AE62C
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

		// Token: 0x060030D7 RID: 12503 RVA: 0x001B0470 File Offset: 0x001AE670
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

		// Token: 0x060030D8 RID: 12504 RVA: 0x001B04B4 File Offset: 0x001AE6B4
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

		// Token: 0x060030D9 RID: 12505 RVA: 0x001B04FC File Offset: 0x001AE6FC
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

		// Token: 0x060030DA RID: 12506 RVA: 0x001B0540 File Offset: 0x001AE740
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

		// Token: 0x060030DB RID: 12507 RVA: 0x001B0584 File Offset: 0x001AE784
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

		// Token: 0x060030DC RID: 12508 RVA: 0x001B05CC File Offset: 0x001AE7CC
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

		// Token: 0x060030DD RID: 12509 RVA: 0x001B0660 File Offset: 0x001AE860
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

		// Token: 0x060030DE RID: 12510 RVA: 0x001B06A8 File Offset: 0x001AE8A8
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

		// Token: 0x060030DF RID: 12511 RVA: 0x001B0770 File Offset: 0x001AE970
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

		// Token: 0x060030E0 RID: 12512 RVA: 0x001B088C File Offset: 0x001AEA8C
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

		// Token: 0x060030E1 RID: 12513 RVA: 0x001B0920 File Offset: 0x001AEB20
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

		// Token: 0x060030E2 RID: 12514 RVA: 0x001B0974 File Offset: 0x001AEB74
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

		// Token: 0x060030E3 RID: 12515 RVA: 0x001B09C8 File Offset: 0x001AEBC8
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

		// Token: 0x060030E4 RID: 12516 RVA: 0x001B0A10 File Offset: 0x001AEC10
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

		// Token: 0x060030E5 RID: 12517 RVA: 0x001B0A58 File Offset: 0x001AEC58
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

		// Token: 0x060030E6 RID: 12518 RVA: 0x001B0AC0 File Offset: 0x001AECC0
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

		// Token: 0x060030E7 RID: 12519 RVA: 0x001B0AF8 File Offset: 0x001AECF8
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

		// Token: 0x060030E8 RID: 12520 RVA: 0x001B0B38 File Offset: 0x001AED38
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

		// Token: 0x060030E9 RID: 12521 RVA: 0x001B0C35 File Offset: 0x001AEE35
		private void UngetChar()
		{
			this.input_buffer = this.input_char;
		}

		// Token: 0x040029E9 RID: 10729
		private static int[] fsm_return_table;

		// Token: 0x040029EA RID: 10730
		private static Lexer.StateHandler[] fsm_handler_table;

		// Token: 0x040029EB RID: 10731
		private bool allow_comments;

		// Token: 0x040029EC RID: 10732
		private bool allow_single_quoted_strings;

		// Token: 0x040029ED RID: 10733
		private bool end_of_input;

		// Token: 0x040029EE RID: 10734
		private FsmContext fsm_context;

		// Token: 0x040029EF RID: 10735
		private int input_buffer;

		// Token: 0x040029F0 RID: 10736
		private int input_char;

		// Token: 0x040029F1 RID: 10737
		private TextReader reader;

		// Token: 0x040029F2 RID: 10738
		private int state;

		// Token: 0x040029F3 RID: 10739
		private StringBuilder string_buffer;

		// Token: 0x040029F4 RID: 10740
		private string string_value;

		// Token: 0x040029F5 RID: 10741
		private int token;

		// Token: 0x040029F6 RID: 10742
		private int unichar;

		// Token: 0x0200069A RID: 1690
		// (Invoke) Token: 0x0600399C RID: 14748
		private delegate bool StateHandler(FsmContext ctx);

		// Token: 0x0200069B RID: 1691
		[CompilerGenerated]
		private static class O_cache
		{
			// Token: 0x04003138 RID: 12600
			public static Lexer.StateHandler _State_0_1;

			// Token: 0x04003139 RID: 12601
			public static Lexer.StateHandler _State_1_2;

			// Token: 0x0400313A RID: 12602
			public static Lexer.StateHandler _State_2_3;

			// Token: 0x0400313B RID: 12603
			public static Lexer.StateHandler _State_3_4;

			// Token: 0x0400313C RID: 12604
			public static Lexer.StateHandler _State_4_5;

			// Token: 0x0400313D RID: 12605
			public static Lexer.StateHandler _State_5_6;

			// Token: 0x0400313E RID: 12606
			public static Lexer.StateHandler _State_6_7;

			// Token: 0x0400313F RID: 12607
			public static Lexer.StateHandler _State_7_8;

			// Token: 0x04003140 RID: 12608
			public static Lexer.StateHandler _State_8_9;

			// Token: 0x04003141 RID: 12609
			public static Lexer.StateHandler _State_9_10;

			// Token: 0x04003142 RID: 12610
			public static Lexer.StateHandler _State_10_11;

			// Token: 0x04003143 RID: 12611
			public static Lexer.StateHandler _State_11_12;

			// Token: 0x04003144 RID: 12612
			public static Lexer.StateHandler _State_12_13;

			// Token: 0x04003145 RID: 12613
			public static Lexer.StateHandler _State_13_14;

			// Token: 0x04003146 RID: 12614
			public static Lexer.StateHandler _State_14_15;

			// Token: 0x04003147 RID: 12615
			public static Lexer.StateHandler _State_15_16;

			// Token: 0x04003148 RID: 12616
			public static Lexer.StateHandler _State_16_17;

			// Token: 0x04003149 RID: 12617
			public static Lexer.StateHandler _State_17_18;

			// Token: 0x0400314A RID: 12618
			public static Lexer.StateHandler _State_18_19;

			// Token: 0x0400314B RID: 12619
			public static Lexer.StateHandler _State_19_20;

			// Token: 0x0400314C RID: 12620
			public static Lexer.StateHandler _State_20_21;

			// Token: 0x0400314D RID: 12621
			public static Lexer.StateHandler _State_21_22;

			// Token: 0x0400314E RID: 12622
			public static Lexer.StateHandler _State_22_23;

			// Token: 0x0400314F RID: 12623
			public static Lexer.StateHandler _State_23_24;

			// Token: 0x04003150 RID: 12624
			public static Lexer.StateHandler _State_24_25;

			// Token: 0x04003151 RID: 12625
			public static Lexer.StateHandler _State_25_26;

			// Token: 0x04003152 RID: 12626
			public static Lexer.StateHandler _State_26_27;

			// Token: 0x04003153 RID: 12627
			public static Lexer.StateHandler _State_27_28;
		}
	}
}

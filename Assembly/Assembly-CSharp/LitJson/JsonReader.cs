using System;
using System.Collections.Generic;
using System.IO;

namespace LitJson
{
	// Token: 0x02000508 RID: 1288
	public class JsonReader
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x001B40C4 File Offset: 0x001B22C4
		// (set) Token: 0x06003211 RID: 12817 RVA: 0x001B40E1 File Offset: 0x001B22E1
		public bool AllowComments
		{
			get
			{
				return this.lexer.AllowComments;
			}
			set
			{
				this.lexer.AllowComments = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x001B40F4 File Offset: 0x001B22F4
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x001B4111 File Offset: 0x001B2311
		public bool AllowSingleQuotedStrings
		{
			get
			{
				return this.lexer.AllowSingleQuotedStrings;
			}
			set
			{
				this.lexer.AllowSingleQuotedStrings = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x001B4121 File Offset: 0x001B2321
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x001B4129 File Offset: 0x001B2329
		public bool EndOfJson
		{
			get
			{
				return this.end_of_json;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x001B4131 File Offset: 0x001B2331
		public JsonToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x001B4139 File Offset: 0x001B2339
		public object Value
		{
			get
			{
				return this.token_value;
			}
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x001B4141 File Offset: 0x001B2341
		static JsonReader()
		{
			JsonReader.PopulateParseTable();
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x001B414A File Offset: 0x001B234A
		public JsonReader(string json_text) : this(new StringReader(json_text), true)
		{
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x001B415B File Offset: 0x001B235B
		public JsonReader(TextReader reader) : this(reader, false)
		{
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x001B4168 File Offset: 0x001B2368
		private JsonReader(TextReader reader, bool owned)
		{
			bool flag = reader == null;
			if (flag)
			{
				throw new ArgumentNullException("reader");
			}
			this.parser_in_string = false;
			this.parser_return = false;
			this.read_started = false;
			this.automaton_stack = new Stack<int>();
			this.automaton_stack.Push(65553);
			this.automaton_stack.Push(65543);
			this.lexer = new Lexer(reader);
			this.end_of_input = false;
			this.end_of_json = false;
			this.reader = reader;
			this.reader_is_owned = owned;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x001B41FC File Offset: 0x001B23FC
		private static void PopulateParseTable()
		{
			JsonReader.parse_table = new Dictionary<int, IDictionary<int, int[]>>();
			JsonReader.TableAddRow(ParserToken.Array);
			JsonReader.TableAddCol(ParserToken.Array, 91, new int[]
			{
				91,
				65549
			});
			JsonReader.TableAddRow(ParserToken.ArrayPrime);
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 34, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 91, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 93, new int[]
			{
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 123, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65537, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65538, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65539, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65540, new int[]
			{
				65550,
				65551,
				93
			});
			JsonReader.TableAddRow(ParserToken.Object);
			JsonReader.TableAddCol(ParserToken.Object, 123, new int[]
			{
				123,
				65545
			});
			JsonReader.TableAddRow(ParserToken.ObjectPrime);
			JsonReader.TableAddCol(ParserToken.ObjectPrime, 34, new int[]
			{
				65546,
				65547,
				125
			});
			JsonReader.TableAddCol(ParserToken.ObjectPrime, 125, new int[]
			{
				125
			});
			JsonReader.TableAddRow(ParserToken.Pair);
			JsonReader.TableAddCol(ParserToken.Pair, 34, new int[]
			{
				65552,
				58,
				65550
			});
			JsonReader.TableAddRow(ParserToken.PairRest);
			JsonReader.TableAddCol(ParserToken.PairRest, 44, new int[]
			{
				44,
				65546,
				65547
			});
			JsonReader.TableAddCol(ParserToken.PairRest, 125, new int[]
			{
				65554
			});
			JsonReader.TableAddRow(ParserToken.String);
			JsonReader.TableAddCol(ParserToken.String, 34, new int[]
			{
				34,
				65541,
				34
			});
			JsonReader.TableAddRow(ParserToken.Text);
			JsonReader.TableAddCol(ParserToken.Text, 91, new int[]
			{
				65548
			});
			JsonReader.TableAddCol(ParserToken.Text, 123, new int[]
			{
				65544
			});
			JsonReader.TableAddRow(ParserToken.Value);
			JsonReader.TableAddCol(ParserToken.Value, 34, new int[]
			{
				65552
			});
			JsonReader.TableAddCol(ParserToken.Value, 91, new int[]
			{
				65548
			});
			JsonReader.TableAddCol(ParserToken.Value, 123, new int[]
			{
				65544
			});
			JsonReader.TableAddCol(ParserToken.Value, 65537, new int[]
			{
				65537
			});
			JsonReader.TableAddCol(ParserToken.Value, 65538, new int[]
			{
				65538
			});
			JsonReader.TableAddCol(ParserToken.Value, 65539, new int[]
			{
				65539
			});
			JsonReader.TableAddCol(ParserToken.Value, 65540, new int[]
			{
				65540
			});
			JsonReader.TableAddRow(ParserToken.ValueRest);
			JsonReader.TableAddCol(ParserToken.ValueRest, 44, new int[]
			{
				44,
				65550,
				65551
			});
			JsonReader.TableAddCol(ParserToken.ValueRest, 93, new int[]
			{
				65554
			});
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x001B459B File Offset: 0x001B279B
		private static void TableAddCol(ParserToken row, int col, params int[] symbols)
		{
			JsonReader.parse_table[(int)row].Add(col, symbols);
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x001B45B1 File Offset: 0x001B27B1
		private static void TableAddRow(ParserToken rule)
		{
			JsonReader.parse_table.Add((int)rule, new Dictionary<int, int[]>());
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x001B45C8 File Offset: 0x001B27C8
		private void ProcessNumber(string number)
		{
			double num = 0.0;
			bool flag = (number.IndexOf('.') != -1 || number.IndexOf('e') != -1 || number.IndexOf('E') != -1) && double.TryParse(number, out num);
			if (flag)
			{
				this.token = JsonToken.Double;
				this.token_value = num;
			}
			else
			{
				int num2;
				bool flag2 = int.TryParse(number, out num2);
				if (flag2)
				{
					this.token = JsonToken.Int;
					this.token_value = num2;
				}
				else
				{
					long num3;
					bool flag3 = long.TryParse(number, out num3);
					if (flag3)
					{
						this.token = JsonToken.Long;
						this.token_value = num3;
					}
					else
					{
						this.token = JsonToken.Int;
						this.token_value = 0;
					}
				}
			}
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x001B467C File Offset: 0x001B287C
		private void ProcessSymbol()
		{
			bool flag = this.current_symbol == 91;
			if (flag)
			{
				this.token = JsonToken.ArrayStart;
				this.parser_return = true;
			}
			else
			{
				bool flag2 = this.current_symbol == 93;
				if (flag2)
				{
					this.token = JsonToken.ArrayEnd;
					this.parser_return = true;
				}
				else
				{
					bool flag3 = this.current_symbol == 123;
					if (flag3)
					{
						this.token = JsonToken.ObjectStart;
						this.parser_return = true;
					}
					else
					{
						bool flag4 = this.current_symbol == 125;
						if (flag4)
						{
							this.token = JsonToken.ObjectEnd;
							this.parser_return = true;
						}
						else
						{
							bool flag5 = this.current_symbol == 34;
							if (flag5)
							{
								bool flag6 = this.parser_in_string;
								if (flag6)
								{
									this.parser_in_string = false;
									this.parser_return = true;
								}
								else
								{
									bool flag7 = this.token == JsonToken.None;
									if (flag7)
									{
										this.token = JsonToken.String;
									}
									this.parser_in_string = true;
								}
							}
							else
							{
								bool flag8 = this.current_symbol == 65541;
								if (flag8)
								{
									this.token_value = this.lexer.StringValue;
								}
								else
								{
									bool flag9 = this.current_symbol == 65539;
									if (flag9)
									{
										this.token = JsonToken.Boolean;
										this.token_value = false;
										this.parser_return = true;
									}
									else
									{
										bool flag10 = this.current_symbol == 65540;
										if (flag10)
										{
											this.token = JsonToken.Null;
											this.parser_return = true;
										}
										else
										{
											bool flag11 = this.current_symbol == 65537;
											if (flag11)
											{
												this.ProcessNumber(this.lexer.StringValue);
												this.parser_return = true;
											}
											else
											{
												bool flag12 = this.current_symbol == 65546;
												if (flag12)
												{
													this.token = JsonToken.PropertyName;
												}
												else
												{
													bool flag13 = this.current_symbol == 65538;
													if (flag13)
													{
														this.token = JsonToken.Boolean;
														this.token_value = true;
														this.parser_return = true;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x001B4874 File Offset: 0x001B2A74
		private bool ReadToken()
		{
			bool flag = this.end_of_input;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.lexer.NextToken();
				bool endOfInput = this.lexer.EndOfInput;
				if (endOfInput)
				{
					this.Close();
					result = false;
				}
				else
				{
					this.current_input = this.lexer.Token;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x001B48D0 File Offset: 0x001B2AD0
		public void Close()
		{
			bool flag = !this.end_of_input;
			if (flag)
			{
				this.end_of_input = true;
				this.end_of_json = true;
				bool flag2 = this.reader_is_owned;
				if (flag2)
				{
					this.reader.Close();
				}
				this.reader = null;
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x001B491C File Offset: 0x001B2B1C
		public bool Read()
		{
			bool flag = this.end_of_input;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.end_of_json;
				if (flag2)
				{
					this.end_of_json = false;
					this.automaton_stack.Clear();
					this.automaton_stack.Push(65553);
					this.automaton_stack.Push(65543);
				}
				this.parser_in_string = false;
				this.parser_return = false;
				this.token = JsonToken.None;
				this.token_value = null;
				bool flag3 = !this.read_started;
				if (flag3)
				{
					this.read_started = true;
					bool flag4 = !this.ReadToken();
					if (flag4)
					{
						return false;
					}
				}
				for (;;)
				{
					bool flag5 = this.parser_return;
					if (flag5)
					{
						break;
					}
					this.current_symbol = this.automaton_stack.Pop();
					this.ProcessSymbol();
					bool flag6 = this.current_symbol == this.current_input;
					if (flag6)
					{
						bool flag7 = !this.ReadToken();
						if (flag7)
						{
							goto Block_8;
						}
					}
					else
					{
						int[] array;
						try
						{
							array = JsonReader.parse_table[this.current_symbol][this.current_input];
						}
						catch (KeyNotFoundException inner_exception)
						{
							throw new JsonException((ParserToken)this.current_input, inner_exception);
						}
						bool flag8 = array[0] != 65554;
						if (flag8)
						{
							for (int i = array.Length - 1; i >= 0; i--)
							{
								this.automaton_stack.Push(array[i]);
							}
						}
					}
				}
				bool flag9 = this.automaton_stack.Peek() == 65553;
				if (flag9)
				{
					this.end_of_json = true;
				}
				return true;
				Block_8:
				bool flag10 = this.automaton_stack.Peek() != 65553;
				if (flag10)
				{
					throw new JsonException("Input doesn't evaluate to proper JSON text");
				}
				bool flag11 = this.parser_return;
				result = flag11;
			}
			return result;
		}

		// Token: 0x04002AC4 RID: 10948
		private static IDictionary<int, IDictionary<int, int[]>> parse_table;

		// Token: 0x04002AC5 RID: 10949
		private Stack<int> automaton_stack;

		// Token: 0x04002AC6 RID: 10950
		private int current_input;

		// Token: 0x04002AC7 RID: 10951
		private int current_symbol;

		// Token: 0x04002AC8 RID: 10952
		private bool end_of_json;

		// Token: 0x04002AC9 RID: 10953
		private bool end_of_input;

		// Token: 0x04002ACA RID: 10954
		private Lexer lexer;

		// Token: 0x04002ACB RID: 10955
		private bool parser_in_string;

		// Token: 0x04002ACC RID: 10956
		private bool parser_return;

		// Token: 0x04002ACD RID: 10957
		private bool read_started;

		// Token: 0x04002ACE RID: 10958
		private TextReader reader;

		// Token: 0x04002ACF RID: 10959
		private bool reader_is_owned;

		// Token: 0x04002AD0 RID: 10960
		private object token_value;

		// Token: 0x04002AD1 RID: 10961
		private JsonToken token;
	}
}

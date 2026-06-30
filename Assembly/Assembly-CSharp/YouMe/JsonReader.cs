using System;
using System.Collections.Generic;
using System.IO;

namespace YouMe
{
	// Token: 0x020004EC RID: 1260
	public class JsonReader
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x001ADF04 File Offset: 0x001AC104
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x001ADF21 File Offset: 0x001AC121
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

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x001ADF34 File Offset: 0x001AC134
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x001ADF51 File Offset: 0x001AC151
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

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x001ADF64 File Offset: 0x001AC164
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x001ADF7C File Offset: 0x001AC17C
		public bool SkipNonMembers
		{
			get
			{
				return this.skip_non_members;
			}
			set
			{
				this.skip_non_members = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x001ADF86 File Offset: 0x001AC186
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x001ADF8E File Offset: 0x001AC18E
		public bool EndOfJson
		{
			get
			{
				return this.end_of_json;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x001ADF96 File Offset: 0x001AC196
		public JsonToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x001ADF9E File Offset: 0x001AC19E
		public object Value
		{
			get
			{
				return this.token_value;
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x001ADFA6 File Offset: 0x001AC1A6
		static JsonReader()
		{
			JsonReader.PopulateParseTable();
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x001ADFAF File Offset: 0x001AC1AF
		public JsonReader(string json_text) : this(new StringReader(json_text), true)
		{
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x001ADFC0 File Offset: 0x001AC1C0
		public JsonReader(TextReader reader) : this(reader, false)
		{
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x001ADFCC File Offset: 0x001AC1CC
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
			this.skip_non_members = true;
			this.reader = reader;
			this.reader_is_owned = owned;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x001AE068 File Offset: 0x001AC268
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

		// Token: 0x06003095 RID: 12437 RVA: 0x001AE407 File Offset: 0x001AC607
		private static void TableAddCol(ParserToken row, int col, params int[] symbols)
		{
			JsonReader.parse_table[(int)row].Add(col, symbols);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x001AE41D File Offset: 0x001AC61D
		private static void TableAddRow(ParserToken rule)
		{
			JsonReader.parse_table.Add((int)rule, new Dictionary<int, int[]>());
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x001AE434 File Offset: 0x001AC634
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
						ulong num4;
						bool flag4 = ulong.TryParse(number, out num4);
						if (flag4)
						{
							this.token = JsonToken.Long;
							this.token_value = num4;
						}
						else
						{
							this.token = JsonToken.Int;
							this.token_value = 0;
						}
					}
				}
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x001AE514 File Offset: 0x001AC714
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

		// Token: 0x06003099 RID: 12441 RVA: 0x001AE70C File Offset: 0x001AC90C
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

		// Token: 0x0600309A RID: 12442 RVA: 0x001AE768 File Offset: 0x001AC968
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

		// Token: 0x0600309B RID: 12443 RVA: 0x001AE7B4 File Offset: 0x001AC9B4
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

		// Token: 0x040029B9 RID: 10681
		private static IDictionary<int, IDictionary<int, int[]>> parse_table;

		// Token: 0x040029BA RID: 10682
		private Stack<int> automaton_stack;

		// Token: 0x040029BB RID: 10683
		private int current_input;

		// Token: 0x040029BC RID: 10684
		private int current_symbol;

		// Token: 0x040029BD RID: 10685
		private bool end_of_json;

		// Token: 0x040029BE RID: 10686
		private bool end_of_input;

		// Token: 0x040029BF RID: 10687
		private Lexer lexer;

		// Token: 0x040029C0 RID: 10688
		private bool parser_in_string;

		// Token: 0x040029C1 RID: 10689
		private bool parser_return;

		// Token: 0x040029C2 RID: 10690
		private bool read_started;

		// Token: 0x040029C3 RID: 10691
		private TextReader reader;

		// Token: 0x040029C4 RID: 10692
		private bool reader_is_owned;

		// Token: 0x040029C5 RID: 10693
		private bool skip_non_members;

		// Token: 0x040029C6 RID: 10694
		private object token_value;

		// Token: 0x040029C7 RID: 10695
		private JsonToken token;
	}
}

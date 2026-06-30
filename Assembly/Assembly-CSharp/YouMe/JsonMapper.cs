using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace YouMe
{
	// Token: 0x020004EA RID: 1258
	public class JsonMapper
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x001AC4A0 File Offset: 0x001AA6A0
		static JsonMapper()
		{
			JsonMapper.max_nesting_depth = 100;
			JsonMapper.array_metadata = new Dictionary<Type, ArrayMetadata>();
			JsonMapper.conv_ops = new Dictionary<Type, IDictionary<Type, MethodInfo>>();
			JsonMapper.object_metadata = new Dictionary<Type, ObjectMetadata>();
			JsonMapper.type_properties = new Dictionary<Type, IList<PropertyMetadata>>();
			JsonMapper.static_writer = new JsonWriter();
			JsonMapper.datetime_format = DateTimeFormatInfo.InvariantInfo;
			JsonMapper.base_exporters_table = new Dictionary<Type, ExporterFunc>();
			JsonMapper.custom_exporters_table = new Dictionary<Type, ExporterFunc>();
			JsonMapper.base_importers_table = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
			JsonMapper.custom_importers_table = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
			JsonMapper.RegisterBaseExporters();
			JsonMapper.RegisterBaseImporters();
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x001AC558 File Offset: 0x001AA758
		private static void AddArrayMetadata(Type type)
		{
			bool flag = JsonMapper.array_metadata.ContainsKey(type);
			if (!flag)
			{
				ArrayMetadata value = new ArrayMetadata
				{
					IsArray = type.IsArray
				};
				bool flag2 = type.GetInterface("System.Collections.IList") != null;
				if (flag2)
				{
					value.IsList = true;
				}
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					bool flag3 = !(propertyInfo.Name != "Item");
					if (flag3)
					{
						ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
						bool flag4 = indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(int);
						if (flag4)
						{
							value.ElementType = propertyInfo.PropertyType;
						}
					}
				}
				object obj = JsonMapper.array_metadata_lock;
				lock (obj)
				{
					try
					{
						JsonMapper.array_metadata.Add(type, value);
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x001AC684 File Offset: 0x001AA884
		private static void AddObjectMetadata(Type type)
		{
			bool flag = JsonMapper.object_metadata.ContainsKey(type);
			if (!flag)
			{
				ObjectMetadata value = default(ObjectMetadata);
				bool flag2 = type.GetInterface("System.Collections.IDictionary") != null;
				if (flag2)
				{
					value.IsDictionary = true;
				}
				value.Properties = new Dictionary<string, PropertyMetadata>();
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					bool flag3 = propertyInfo.Name == "Item";
					if (flag3)
					{
						ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
						bool flag4 = indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(string);
						if (flag4)
						{
							value.ElementType = propertyInfo.PropertyType;
						}
					}
					else
					{
						PropertyMetadata value2 = new PropertyMetadata
						{
							Info = propertyInfo,
							Type = propertyInfo.PropertyType
						};
						value.Properties.Add(propertyInfo.Name, value2);
					}
				}
				FieldInfo[] fields = type.GetFields();
				foreach (FieldInfo fieldInfo in fields)
				{
					PropertyMetadata value3 = new PropertyMetadata
					{
						Info = fieldInfo,
						IsField = true,
						Type = fieldInfo.FieldType
					};
					value.Properties.Add(fieldInfo.Name, value3);
				}
				object obj = JsonMapper.object_metadata_lock;
				lock (obj)
				{
					try
					{
						JsonMapper.object_metadata.Add(type, value);
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x001AC854 File Offset: 0x001AAA54
		private static void AddTypeProperties(Type type)
		{
			bool flag = JsonMapper.type_properties.ContainsKey(type);
			if (!flag)
			{
				IList<PropertyMetadata> list = new List<PropertyMetadata>();
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					bool flag2 = !(propertyInfo.Name == "Item");
					if (flag2)
					{
						list.Add(new PropertyMetadata
						{
							Info = propertyInfo,
							IsField = false
						});
					}
				}
				FieldInfo[] fields = type.GetFields();
				foreach (FieldInfo info in fields)
				{
					list.Add(new PropertyMetadata
					{
						Info = info,
						IsField = true
					});
				}
				object obj = JsonMapper.type_properties_lock;
				lock (obj)
				{
					try
					{
						JsonMapper.type_properties.Add(type, list);
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x001AC97C File Offset: 0x001AAB7C
		private static MethodInfo GetConvOp(Type t1, Type t2)
		{
			object obj = JsonMapper.conv_ops_lock;
			lock (obj)
			{
				bool flag = !JsonMapper.conv_ops.ContainsKey(t1);
				if (flag)
				{
					JsonMapper.conv_ops.Add(t1, new Dictionary<Type, MethodInfo>());
				}
			}
			bool flag2 = JsonMapper.conv_ops[t1].ContainsKey(t2);
			MethodInfo result;
			if (flag2)
			{
				result = JsonMapper.conv_ops[t1][t2];
			}
			else
			{
				MethodInfo method = t1.GetMethod("op_Implicit", new Type[]
				{
					t2
				});
				object obj2 = JsonMapper.conv_ops_lock;
				lock (obj2)
				{
					try
					{
						JsonMapper.conv_ops[t1].Add(t2, method);
						result = method;
					}
					catch (ArgumentException)
					{
						result = JsonMapper.conv_ops[t1][t2];
					}
				}
			}
			return result;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x001ACA7C File Offset: 0x001AAC7C
		private static object ReadValue(Type inst_type, JsonReader reader)
		{
			reader.Read();
			bool flag = reader.Token == JsonToken.ArrayEnd;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Type underlyingType = Nullable.GetUnderlyingType(inst_type);
				Type type = underlyingType ?? inst_type;
				bool flag2 = reader.Token == JsonToken.Null;
				if (flag2)
				{
					bool flag3 = inst_type.IsClass || underlyingType != null;
					if (!flag3)
					{
						throw new JsonException(string.Format("Can't assign null to an instance of type {0}", inst_type));
					}
					result = null;
				}
				else
				{
					bool flag4 = reader.Token == JsonToken.Double || reader.Token == JsonToken.Int || reader.Token == JsonToken.Long || reader.Token == JsonToken.String || reader.Token == JsonToken.Boolean;
					if (flag4)
					{
						Type type2 = reader.Value.GetType();
						bool flag5 = type.IsAssignableFrom(type2);
						if (flag5)
						{
							result = reader.Value;
						}
						else
						{
							bool flag6 = JsonMapper.custom_importers_table.ContainsKey(type2) && JsonMapper.custom_importers_table[type2].ContainsKey(type);
							if (flag6)
							{
								ImporterFunc importerFunc = JsonMapper.custom_importers_table[type2][type];
								result = importerFunc(reader.Value);
							}
							else
							{
								bool flag7 = JsonMapper.base_importers_table.ContainsKey(type2) && JsonMapper.base_importers_table[type2].ContainsKey(type);
								if (flag7)
								{
									ImporterFunc importerFunc2 = JsonMapper.base_importers_table[type2][type];
									result = importerFunc2(reader.Value);
								}
								else
								{
									bool isEnum = type.IsEnum;
									if (isEnum)
									{
										result = Enum.ToObject(type, reader.Value);
									}
									else
									{
										MethodInfo convOp = JsonMapper.GetConvOp(type, type2);
										bool flag8 = convOp != null;
										if (!flag8)
										{
											throw new JsonException(string.Format("Can't assign value '{0}' (type {1}) to type {2}", reader.Value, type2, inst_type));
										}
										result = convOp.Invoke(null, new object[]
										{
											reader.Value
										});
									}
								}
							}
						}
					}
					else
					{
						object obj = null;
						bool flag9 = reader.Token == JsonToken.ArrayStart;
						if (flag9)
						{
							JsonMapper.AddArrayMetadata(inst_type);
							ArrayMetadata arrayMetadata = JsonMapper.array_metadata[inst_type];
							bool flag10 = !arrayMetadata.IsArray && !arrayMetadata.IsList;
							if (flag10)
							{
								throw new JsonException(string.Format("Type {0} can't act as an array", inst_type));
							}
							bool flag11 = !arrayMetadata.IsArray;
							IList list;
							Type elementType;
							if (flag11)
							{
								list = (IList)Activator.CreateInstance(inst_type);
								elementType = arrayMetadata.ElementType;
							}
							else
							{
								list = new ArrayList();
								elementType = inst_type.GetElementType();
							}
							for (;;)
							{
								object obj2 = JsonMapper.ReadValue(elementType, reader);
								bool flag12 = obj2 == null && reader.Token == JsonToken.ArrayEnd;
								if (flag12)
								{
									break;
								}
								list.Add(obj2);
							}
							bool isArray = arrayMetadata.IsArray;
							if (isArray)
							{
								int count = list.Count;
								obj = Array.CreateInstance(elementType, count);
								for (int i = 0; i < count; i++)
								{
									((Array)obj).SetValue(list[i], i);
								}
							}
							else
							{
								obj = list;
							}
						}
						else
						{
							bool flag13 = reader.Token == JsonToken.ObjectStart;
							if (flag13)
							{
								JsonMapper.AddObjectMetadata(type);
								ObjectMetadata objectMetadata = JsonMapper.object_metadata[type];
								obj = Activator.CreateInstance(type);
								string text;
								for (;;)
								{
									reader.Read();
									bool flag14 = reader.Token == JsonToken.ObjectEnd;
									if (flag14)
									{
										break;
									}
									text = (string)reader.Value;
									bool flag15 = objectMetadata.Properties.ContainsKey(text);
									if (flag15)
									{
										PropertyMetadata propertyMetadata = objectMetadata.Properties[text];
										bool isField = propertyMetadata.IsField;
										if (isField)
										{
											((FieldInfo)propertyMetadata.Info).SetValue(obj, JsonMapper.ReadValue(propertyMetadata.Type, reader));
										}
										else
										{
											PropertyInfo propertyInfo = (PropertyInfo)propertyMetadata.Info;
											bool canWrite = propertyInfo.CanWrite;
											if (canWrite)
											{
												propertyInfo.SetValue(obj, JsonMapper.ReadValue(propertyMetadata.Type, reader), null);
											}
											else
											{
												JsonMapper.ReadValue(propertyMetadata.Type, reader);
											}
										}
									}
									else
									{
										bool flag16 = !objectMetadata.IsDictionary;
										if (flag16)
										{
											bool flag17 = !reader.SkipNonMembers;
											if (flag17)
											{
												goto Block_32;
											}
											JsonMapper.ReadSkip(reader);
										}
										else
										{
											((IDictionary)obj).Add(text, JsonMapper.ReadValue(objectMetadata.ElementType, reader));
										}
									}
								}
								goto IL_46F;
								Block_32:
								throw new JsonException(string.Format("The type {0} doesn't have the property '{1}'", inst_type, text));
							}
						}
						IL_46F:
						result = obj;
					}
				}
			}
			return result;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x001ACF00 File Offset: 0x001AB100
		private static IJsonWrapper ReadValue(WrapperFactory factory, JsonReader reader)
		{
			reader.Read();
			bool flag = reader.Token == JsonToken.ArrayEnd || reader.Token == JsonToken.Null;
			IJsonWrapper result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IJsonWrapper jsonWrapper = factory();
				bool flag2 = reader.Token == JsonToken.String;
				if (flag2)
				{
					jsonWrapper.SetString((string)reader.Value);
					result = jsonWrapper;
				}
				else
				{
					bool flag3 = reader.Token == JsonToken.Double;
					if (flag3)
					{
						jsonWrapper.SetDouble((double)reader.Value);
						result = jsonWrapper;
					}
					else
					{
						bool flag4 = reader.Token == JsonToken.Int;
						if (flag4)
						{
							jsonWrapper.SetInt((int)reader.Value);
							result = jsonWrapper;
						}
						else
						{
							bool flag5 = reader.Token == JsonToken.Long;
							if (flag5)
							{
								jsonWrapper.SetLong((long)reader.Value);
								result = jsonWrapper;
							}
							else
							{
								bool flag6 = reader.Token == JsonToken.Boolean;
								if (flag6)
								{
									jsonWrapper.SetBoolean((bool)reader.Value);
									result = jsonWrapper;
								}
								else
								{
									bool flag7 = reader.Token == JsonToken.ArrayStart;
									if (flag7)
									{
										jsonWrapper.SetJsonType(JsonType.Array);
										for (;;)
										{
											IJsonWrapper jsonWrapper2 = JsonMapper.ReadValue(factory, reader);
											bool flag8 = jsonWrapper2 == null && reader.Token == JsonToken.ArrayEnd;
											if (flag8)
											{
												break;
											}
											jsonWrapper.Add(jsonWrapper2);
										}
									}
									else
									{
										bool flag9 = reader.Token == JsonToken.ObjectStart;
										if (flag9)
										{
											jsonWrapper.SetJsonType(JsonType.Object);
											for (;;)
											{
												reader.Read();
												bool flag10 = reader.Token == JsonToken.ObjectEnd;
												if (flag10)
												{
													break;
												}
												string key = (string)reader.Value;
												jsonWrapper[key] = JsonMapper.ReadValue(factory, reader);
											}
										}
									}
									result = jsonWrapper;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x001AD0B7 File Offset: 0x001AB2B7
		private static void ReadSkip(JsonReader reader)
		{
			JsonMapper.ToWrapper(() => new JsonMockWrapper(), reader);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x001AD0E0 File Offset: 0x001AB2E0
		private static void RegisterBaseExporters()
		{
			JsonMapper.base_exporters_table[typeof(byte)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToInt32((byte)obj));
			};
			JsonMapper.base_exporters_table[typeof(char)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToString((char)obj));
			};
			JsonMapper.base_exporters_table[typeof(DateTime)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToString((DateTime)obj, JsonMapper.datetime_format));
			};
			JsonMapper.base_exporters_table[typeof(decimal)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write((decimal)obj);
			};
			JsonMapper.base_exporters_table[typeof(sbyte)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToInt32((sbyte)obj));
			};
			JsonMapper.base_exporters_table[typeof(short)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToInt32((short)obj));
			};
			JsonMapper.base_exporters_table[typeof(ushort)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToInt32((ushort)obj));
			};
			JsonMapper.base_exporters_table[typeof(uint)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write(Convert.ToUInt64((uint)obj));
			};
			JsonMapper.base_exporters_table[typeof(ulong)] = delegate(object obj, JsonWriter writer)
			{
				writer.Write((ulong)obj);
			};
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x001AD2C4 File Offset: 0x001AB4C4
		private static void RegisterBaseImporters()
		{
			ImporterFunc importer = (object input) => Convert.ToByte((int)input);
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(byte), importer);
			importer = ((object input) => Convert.ToUInt64((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(ulong), importer);
			importer = ((object input) => Convert.ToSByte((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(sbyte), importer);
			importer = ((object input) => Convert.ToInt16((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(short), importer);
			importer = ((object input) => Convert.ToUInt16((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(ushort), importer);
			importer = ((object input) => Convert.ToUInt32((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(uint), importer);
			importer = ((object input) => Convert.ToSingle((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(float), importer);
			importer = ((object input) => Convert.ToDouble((int)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(int), typeof(double), importer);
			importer = ((object input) => Convert.ToDecimal((double)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(double), typeof(decimal), importer);
			importer = ((object input) => Convert.ToUInt32((long)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(long), typeof(uint), importer);
			importer = ((object input) => Convert.ToChar((string)input));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(string), typeof(char), importer);
			importer = ((object input) => Convert.ToDateTime((string)input, JsonMapper.datetime_format));
			JsonMapper.RegisterImporter(JsonMapper.base_importers_table, typeof(string), typeof(DateTime), importer);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x001AD5D4 File Offset: 0x001AB7D4
		private static void RegisterImporter(IDictionary<Type, IDictionary<Type, ImporterFunc>> table, Type json_type, Type value_type, ImporterFunc importer)
		{
			bool flag = !table.ContainsKey(json_type);
			if (flag)
			{
				table.Add(json_type, new Dictionary<Type, ImporterFunc>());
			}
			table[json_type][value_type] = importer;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x001AD610 File Offset: 0x001AB810
		private static void WriteValue(object obj, JsonWriter writer, bool writer_is_private, int depth)
		{
			bool flag = depth > JsonMapper.max_nesting_depth;
			if (flag)
			{
				throw new JsonException(string.Format("Max allowed object depth reached while trying to export from type {0}", obj.GetType()));
			}
			bool flag2 = obj == null;
			if (flag2)
			{
				writer.Write(null);
			}
			else
			{
				bool flag3 = obj is IJsonWrapper;
				if (flag3)
				{
					if (writer_is_private)
					{
						writer.TextWriter.Write(((IJsonWrapper)obj).ToJson());
					}
					else
					{
						((IJsonWrapper)obj).ToJson(writer);
					}
				}
				else
				{
					bool flag4 = obj is string;
					if (flag4)
					{
						writer.Write((string)obj);
					}
					else
					{
						bool flag5 = obj is double;
						if (flag5)
						{
							writer.Write((double)obj);
						}
						else
						{
							bool flag6 = obj is int;
							if (flag6)
							{
								writer.Write((int)obj);
							}
							else
							{
								bool flag7 = obj is bool;
								if (flag7)
								{
									writer.Write((bool)obj);
								}
								else
								{
									bool flag8 = obj is long;
									if (flag8)
									{
										writer.Write((long)obj);
									}
									else
									{
										bool flag9 = obj is Array;
										if (flag9)
										{
											writer.WriteArrayStart();
											foreach (object obj2 in ((Array)obj))
											{
												JsonMapper.WriteValue(obj2, writer, writer_is_private, depth + 1);
											}
											writer.WriteArrayEnd();
										}
										else
										{
											bool flag10 = obj is IList;
											if (flag10)
											{
												writer.WriteArrayStart();
												foreach (object obj3 in ((IList)obj))
												{
													JsonMapper.WriteValue(obj3, writer, writer_is_private, depth + 1);
												}
												writer.WriteArrayEnd();
											}
											else
											{
												bool flag11 = obj is IDictionary;
												if (flag11)
												{
													writer.WriteObjectStart();
													foreach (object obj4 in ((IDictionary)obj))
													{
														DictionaryEntry dictionaryEntry = (DictionaryEntry)obj4;
														writer.WritePropertyName((string)dictionaryEntry.Key);
														JsonMapper.WriteValue(dictionaryEntry.Value, writer, writer_is_private, depth + 1);
													}
													writer.WriteObjectEnd();
												}
												else
												{
													Type type = obj.GetType();
													bool flag12 = JsonMapper.custom_exporters_table.ContainsKey(type);
													if (flag12)
													{
														ExporterFunc exporterFunc = JsonMapper.custom_exporters_table[type];
														exporterFunc(obj, writer);
													}
													else
													{
														bool flag13 = JsonMapper.base_exporters_table.ContainsKey(type);
														if (flag13)
														{
															ExporterFunc exporterFunc2 = JsonMapper.base_exporters_table[type];
															exporterFunc2(obj, writer);
														}
														else
														{
															bool flag14 = obj is Enum;
															if (flag14)
															{
																Type underlyingType = Enum.GetUnderlyingType(type);
																bool flag15 = underlyingType == typeof(long) || underlyingType == typeof(uint) || underlyingType == typeof(ulong);
																if (flag15)
																{
																	writer.Write((ulong)obj);
																}
																else
																{
																	writer.Write((int)obj);
																}
															}
															else
															{
																JsonMapper.AddTypeProperties(type);
																IList<PropertyMetadata> list = JsonMapper.type_properties[type];
																writer.WriteObjectStart();
																foreach (PropertyMetadata propertyMetadata in list)
																{
																	bool isField = propertyMetadata.IsField;
																	if (isField)
																	{
																		writer.WritePropertyName(propertyMetadata.Info.Name);
																		JsonMapper.WriteValue(((FieldInfo)propertyMetadata.Info).GetValue(obj), writer, writer_is_private, depth + 1);
																	}
																	else
																	{
																		PropertyInfo propertyInfo = (PropertyInfo)propertyMetadata.Info;
																		bool canRead = propertyInfo.CanRead;
																		if (canRead)
																		{
																			writer.WritePropertyName(propertyMetadata.Info.Name);
																			JsonMapper.WriteValue(propertyInfo.GetValue(obj, null), writer, writer_is_private, depth + 1);
																		}
																	}
																}
																writer.WriteObjectEnd();
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
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x001ADA88 File Offset: 0x001ABC88
		public static string ToJson(object obj)
		{
			object obj2 = JsonMapper.static_writer_lock;
			string result;
			lock (obj2)
			{
				JsonMapper.static_writer.Reset();
				JsonMapper.WriteValue(obj, JsonMapper.static_writer, true, 0);
				result = JsonMapper.static_writer.ToString();
			}
			return result;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x001ADAE4 File Offset: 0x001ABCE4
		public static void ToJson(object obj, JsonWriter writer)
		{
			JsonMapper.WriteValue(obj, writer, false, 0);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x001ADAF4 File Offset: 0x001ABCF4
		public static JsonData ToObject(JsonReader reader)
		{
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), reader);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x001ADB30 File Offset: 0x001ABD30
		public static JsonData ToObject(TextReader reader)
		{
			JsonReader reader2 = new JsonReader(reader);
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), reader2);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x001ADB74 File Offset: 0x001ABD74
		public static JsonData ToObject(string json)
		{
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), json);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x001ADBB0 File Offset: 0x001ABDB0
		public static T ToObject<T>(JsonReader reader)
		{
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader));
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x001ADBD8 File Offset: 0x001ABDD8
		public static T ToObject<T>(TextReader reader)
		{
			JsonReader reader2 = new JsonReader(reader);
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader2));
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x001ADC08 File Offset: 0x001ABE08
		public static T ToObject<T>(string json)
		{
			JsonReader reader = new JsonReader(json);
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader));
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x001ADC38 File Offset: 0x001ABE38
		public static IJsonWrapper ToWrapper(WrapperFactory factory, JsonReader reader)
		{
			return JsonMapper.ReadValue(factory, reader);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x001ADC54 File Offset: 0x001ABE54
		public static IJsonWrapper ToWrapper(WrapperFactory factory, string json)
		{
			JsonReader reader = new JsonReader(json);
			return JsonMapper.ReadValue(factory, reader);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x001ADC74 File Offset: 0x001ABE74
		public static void RegisterExporter<T>(ExporterFunc<T> exporter)
		{
			ExporterFunc value = delegate(object obj, JsonWriter writer)
			{
				exporter((T)((object)obj), writer);
			};
			JsonMapper.custom_exporters_table[typeof(T)] = value;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x001ADCB4 File Offset: 0x001ABEB4
		public static void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importer)
		{
			ImporterFunc importer2 = (object input) => importer((TJson)((object)input));
			JsonMapper.RegisterImporter(JsonMapper.custom_importers_table, typeof(TJson), typeof(TValue), importer2);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x001ADCFC File Offset: 0x001ABEFC
		public static void UnregisterExporters()
		{
			JsonMapper.custom_exporters_table.Clear();
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x001ADD0A File Offset: 0x001ABF0A
		public static void UnregisterImporters()
		{
			JsonMapper.custom_importers_table.Clear();
		}

		// Token: 0x040029A9 RID: 10665
		private static int max_nesting_depth;

		// Token: 0x040029AA RID: 10666
		private static IFormatProvider datetime_format;

		// Token: 0x040029AB RID: 10667
		private static IDictionary<Type, ExporterFunc> base_exporters_table;

		// Token: 0x040029AC RID: 10668
		private static IDictionary<Type, ExporterFunc> custom_exporters_table;

		// Token: 0x040029AD RID: 10669
		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> base_importers_table;

		// Token: 0x040029AE RID: 10670
		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> custom_importers_table;

		// Token: 0x040029AF RID: 10671
		private static IDictionary<Type, ArrayMetadata> array_metadata;

		// Token: 0x040029B0 RID: 10672
		private static readonly object array_metadata_lock = new object();

		// Token: 0x040029B1 RID: 10673
		private static IDictionary<Type, IDictionary<Type, MethodInfo>> conv_ops;

		// Token: 0x040029B2 RID: 10674
		private static readonly object conv_ops_lock = new object();

		// Token: 0x040029B3 RID: 10675
		private static IDictionary<Type, ObjectMetadata> object_metadata;

		// Token: 0x040029B4 RID: 10676
		private static readonly object object_metadata_lock = new object();

		// Token: 0x040029B5 RID: 10677
		private static IDictionary<Type, IList<PropertyMetadata>> type_properties;

		// Token: 0x040029B6 RID: 10678
		private static readonly object type_properties_lock = new object();

		// Token: 0x040029B7 RID: 10679
		private static JsonWriter static_writer;

		// Token: 0x040029B8 RID: 10680
		private static readonly object static_writer_lock = new object();
	}
}

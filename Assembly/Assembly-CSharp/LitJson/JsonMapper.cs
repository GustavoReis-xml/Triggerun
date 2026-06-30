using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace LitJson
{
	// Token: 0x02000507 RID: 1287
	public class JsonMapper
	{
		// Token: 0x060031F6 RID: 12790 RVA: 0x001B288C File Offset: 0x001B0A8C
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

		// Token: 0x060031F7 RID: 12791 RVA: 0x001B2944 File Offset: 0x001B0B44
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

		// Token: 0x060031F8 RID: 12792 RVA: 0x001B2A70 File Offset: 0x001B0C70
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

		// Token: 0x060031F9 RID: 12793 RVA: 0x001B2C40 File Offset: 0x001B0E40
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

		// Token: 0x060031FA RID: 12794 RVA: 0x001B2D68 File Offset: 0x001B0F68
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

		// Token: 0x060031FB RID: 12795 RVA: 0x001B2E68 File Offset: 0x001B1068
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
				bool flag2 = reader.Token == JsonToken.Null;
				if (flag2)
				{
					bool flag3 = !inst_type.IsClass;
					if (flag3)
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
						Type type = reader.Value.GetType();
						bool flag5 = inst_type.IsAssignableFrom(type);
						if (flag5)
						{
							result = reader.Value;
						}
						else
						{
							bool flag6 = JsonMapper.custom_importers_table.ContainsKey(type) && JsonMapper.custom_importers_table[type].ContainsKey(inst_type);
							if (flag6)
							{
								ImporterFunc importerFunc = JsonMapper.custom_importers_table[type][inst_type];
								result = importerFunc(reader.Value);
							}
							else
							{
								bool flag7 = JsonMapper.base_importers_table.ContainsKey(type) && JsonMapper.base_importers_table[type].ContainsKey(inst_type);
								if (flag7)
								{
									ImporterFunc importerFunc2 = JsonMapper.base_importers_table[type][inst_type];
									result = importerFunc2(reader.Value);
								}
								else
								{
									bool isEnum = inst_type.IsEnum;
									if (isEnum)
									{
										result = Enum.ToObject(inst_type, reader.Value);
									}
									else
									{
										MethodInfo convOp = JsonMapper.GetConvOp(inst_type, type);
										bool flag8 = convOp != null;
										if (!flag8)
										{
											throw new JsonException(string.Format("Can't assign value '{0}' (type {1}) to type {2}", reader.Value, type, inst_type));
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
							object value;
							for (;;)
							{
								value = JsonMapper.ReadValue(elementType, reader);
								bool flag12 = reader.Token == JsonToken.ArrayEnd;
								if (flag12)
								{
									break;
								}
								list.Add(value);
							}
							list.Add(value);
							list.RemoveAt(list.Count - 1);
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
								JsonMapper.AddObjectMetadata(inst_type);
								ObjectMetadata objectMetadata = JsonMapper.object_metadata[inst_type];
								obj = Activator.CreateInstance(inst_type);
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
											goto Block_28;
										}
										((IDictionary)obj).Add(text, JsonMapper.ReadValue(objectMetadata.ElementType, reader));
									}
								}
								goto IL_44C;
								Block_28:
								throw new JsonException(string.Format("The type {0} doesn't have the property '{1}'", inst_type, text));
							}
						}
						IL_44C:
						result = obj;
					}
				}
			}
			return result;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x001B32C8 File Offset: 0x001B14C8
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
											bool flag8 = reader.Token == JsonToken.ArrayEnd && jsonWrapper2 == null;
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

		// Token: 0x060031FD RID: 12797 RVA: 0x001B3480 File Offset: 0x001B1680
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

		// Token: 0x060031FE RID: 12798 RVA: 0x001B3664 File Offset: 0x001B1864
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

		// Token: 0x060031FF RID: 12799 RVA: 0x001B3974 File Offset: 0x001B1B74
		private static void RegisterImporter(IDictionary<Type, IDictionary<Type, ImporterFunc>> table, Type json_type, Type value_type, ImporterFunc importer)
		{
			bool flag = !table.ContainsKey(json_type);
			if (flag)
			{
				table.Add(json_type, new Dictionary<Type, ImporterFunc>());
			}
			table[json_type][value_type] = importer;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x001B39B0 File Offset: 0x001B1BB0
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

		// Token: 0x06003201 RID: 12801 RVA: 0x001B3E28 File Offset: 0x001B2028
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

		// Token: 0x06003202 RID: 12802 RVA: 0x001B3E84 File Offset: 0x001B2084
		public static void ToJson(object obj, JsonWriter writer)
		{
			JsonMapper.WriteValue(obj, writer, false, 0);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x001B3E94 File Offset: 0x001B2094
		public static JsonData ToObject(JsonReader reader)
		{
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), reader);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x001B3ED0 File Offset: 0x001B20D0
		public static JsonData ToObject(TextReader reader)
		{
			JsonReader reader2 = new JsonReader(reader);
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), reader2);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x001B3F14 File Offset: 0x001B2114
		public static JsonData ToObject(string json)
		{
			return (JsonData)JsonMapper.ToWrapper(() => new JsonData(), json);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x001B3F50 File Offset: 0x001B2150
		public static T ToObject<T>(JsonReader reader)
		{
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader));
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x001B3F78 File Offset: 0x001B2178
		public static T ToObject<T>(TextReader reader)
		{
			JsonReader reader2 = new JsonReader(reader);
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader2));
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x001B3FA8 File Offset: 0x001B21A8
		public static T ToObject<T>(string json)
		{
			JsonReader reader = new JsonReader(json);
			return (T)((object)JsonMapper.ReadValue(typeof(T), reader));
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x001B3FD8 File Offset: 0x001B21D8
		public static IJsonWrapper ToWrapper(WrapperFactory factory, JsonReader reader)
		{
			return JsonMapper.ReadValue(factory, reader);
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x001B3FF4 File Offset: 0x001B21F4
		public static IJsonWrapper ToWrapper(WrapperFactory factory, string json)
		{
			JsonReader reader = new JsonReader(json);
			return JsonMapper.ReadValue(factory, reader);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x001B4014 File Offset: 0x001B2214
		public static void RegisterExporter<T>(ExporterFunc<T> exporter)
		{
			ExporterFunc value = delegate(object obj, JsonWriter writer)
			{
				exporter((T)((object)obj), writer);
			};
			JsonMapper.custom_exporters_table[typeof(T)] = value;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x001B4054 File Offset: 0x001B2254
		public static void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importer)
		{
			ImporterFunc importer2 = (object input) => importer((TJson)((object)input));
			JsonMapper.RegisterImporter(JsonMapper.custom_importers_table, typeof(TJson), typeof(TValue), importer2);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x001B409C File Offset: 0x001B229C
		public static void UnregisterExporters()
		{
			JsonMapper.custom_exporters_table.Clear();
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x001B40AA File Offset: 0x001B22AA
		public static void UnregisterImporters()
		{
			JsonMapper.custom_importers_table.Clear();
		}

		// Token: 0x04002AB4 RID: 10932
		private static int max_nesting_depth;

		// Token: 0x04002AB5 RID: 10933
		private static IFormatProvider datetime_format;

		// Token: 0x04002AB6 RID: 10934
		private static IDictionary<Type, ExporterFunc> base_exporters_table;

		// Token: 0x04002AB7 RID: 10935
		private static IDictionary<Type, ExporterFunc> custom_exporters_table;

		// Token: 0x04002AB8 RID: 10936
		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> base_importers_table;

		// Token: 0x04002AB9 RID: 10937
		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> custom_importers_table;

		// Token: 0x04002ABA RID: 10938
		private static IDictionary<Type, ArrayMetadata> array_metadata;

		// Token: 0x04002ABB RID: 10939
		private static readonly object array_metadata_lock = new object();

		// Token: 0x04002ABC RID: 10940
		private static IDictionary<Type, IDictionary<Type, MethodInfo>> conv_ops;

		// Token: 0x04002ABD RID: 10941
		private static readonly object conv_ops_lock = new object();

		// Token: 0x04002ABE RID: 10942
		private static IDictionary<Type, ObjectMetadata> object_metadata;

		// Token: 0x04002ABF RID: 10943
		private static readonly object object_metadata_lock = new object();

		// Token: 0x04002AC0 RID: 10944
		private static IDictionary<Type, IList<PropertyMetadata>> type_properties;

		// Token: 0x04002AC1 RID: 10945
		private static readonly object type_properties_lock = new object();

		// Token: 0x04002AC2 RID: 10946
		private static JsonWriter static_writer;

		// Token: 0x04002AC3 RID: 10947
		private static readonly object static_writer_lock = new object();
	}
}

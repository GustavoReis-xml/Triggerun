using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace LitJson
{
	// Token: 0x02000505 RID: 1285
	public class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x001B1547 File Offset: 0x001AF747
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x001B154F File Offset: 0x001AF74F
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x001B155C File Offset: 0x001AF75C
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x001B1569 File Offset: 0x001AF769
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x001B1576 File Offset: 0x001AF776
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x001B1584 File Offset: 0x001AF784
		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Key);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x001B15F4 File Offset: 0x001AF7F4
		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Value);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x001B1664 File Offset: 0x001AF864
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x001B166C File Offset: 0x001AF86C
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x001B1674 File Offset: 0x001AF874
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x001B167C File Offset: 0x001AF87C
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x001B1684 File Offset: 0x001AF884
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x001B168C File Offset: 0x001AF88C
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x001B1694 File Offset: 0x001AF894
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x001B169C File Offset: 0x001AF89C
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x001B16A9 File Offset: 0x001AF8A9
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x170005EE RID: 1518
		object IDictionary.this[object key]
		{
			get
			{
				return this.EnsureDictionary()[key];
			}
			set
			{
				bool flag = !(key is string);
				if (flag)
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = this.ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		// Token: 0x170005EF RID: 1519
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list[idx].Value;
			}
			set
			{
				this.EnsureDictionary();
				JsonData value2 = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list[idx];
				this.inst_object[keyValuePair.Key] = value2;
				KeyValuePair<string, JsonData> value3 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value2);
				this.object_list[idx] = value3;
			}
		}

		// Token: 0x170005F0 RID: 1520
		object IList.this[int index]
		{
			get
			{
				return this.EnsureList()[index];
			}
			set
			{
				this.EnsureList();
				JsonData value2 = this.ToJsonData(value);
				this[index] = value2;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x001B17EA File Offset: 0x001AF9EA
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x001B17F7 File Offset: 0x001AF9F7
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x001B1802 File Offset: 0x001AFA02
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x001B180D File Offset: 0x001AFA0D
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x001B1818 File Offset: 0x001AFA18
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x001B1823 File Offset: 0x001AFA23
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x001B182E File Offset: 0x001AFA2E
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x001B1839 File Offset: 0x001AFA39
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x001B1844 File Offset: 0x001AFA44
		public IDictionary<string, JsonData> Inst_Object
		{
			get
			{
				bool flag = this.type == JsonType.Object;
				IDictionary<string, JsonData> result;
				if (flag)
				{
					result = this.inst_object;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x170005FA RID: 1530
		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object[prop_name];
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				bool flag = this.inst_object.ContainsKey(prop_name);
				if (flag)
				{
					for (int i = 0; i < this.object_list.Count; i++)
					{
						bool flag2 = this.object_list[i].Key == prop_name;
						if (flag2)
						{
							this.object_list[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object[prop_name] = value;
				this.json = null;
			}
		}

		// Token: 0x170005FB RID: 1531
		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				bool flag = this.type == JsonType.Array;
				JsonData result;
				if (flag)
				{
					result = this.inst_array[index];
				}
				else
				{
					result = this.object_list[index].Value;
				}
				return result;
			}
			set
			{
				this.EnsureCollection();
				bool flag = this.type == JsonType.Array;
				if (flag)
				{
					this.inst_array[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list[index];
					KeyValuePair<string, JsonData> value2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					this.object_list[index] = value2;
					this.inst_object[keyValuePair.Key] = value;
				}
				this.json = null;
			}
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x001B1A03 File Offset: 0x001AFC03
		public JsonData()
		{
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x001B1A0D File Offset: 0x001AFC0D
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x001B1A25 File Offset: 0x001AFC25
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x001B1A3D File Offset: 0x001AFC3D
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x001B1A55 File Offset: 0x001AFC55
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x001B1A70 File Offset: 0x001AFC70
		public JsonData(object obj)
		{
			bool flag = obj is bool;
			if (flag)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
			}
			else
			{
				bool flag2 = obj is double;
				if (flag2)
				{
					this.type = JsonType.Double;
					this.inst_double = (double)obj;
				}
				else
				{
					bool flag3 = obj is int;
					if (flag3)
					{
						this.type = JsonType.Int;
						this.inst_int = (int)obj;
					}
					else
					{
						bool flag4 = obj is long;
						if (flag4)
						{
							this.type = JsonType.Long;
							this.inst_long = (long)obj;
						}
						else
						{
							bool flag5 = obj is string;
							if (!flag5)
							{
								throw new ArgumentException("Unable to wrap the given object with JsonData");
							}
							this.type = JsonType.String;
							this.inst_string = (string)obj;
						}
					}
				}
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x001B1B44 File Offset: 0x001AFD44
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x001B1B5C File Offset: 0x001AFD5C
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x001B1B74 File Offset: 0x001AFD74
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x001B1B8C File Offset: 0x001AFD8C
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x001B1BA4 File Offset: 0x001AFDA4
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x001B1BBC File Offset: 0x001AFDBC
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x001B1BD4 File Offset: 0x001AFDD4
		public static explicit operator bool(JsonData data)
		{
			bool flag = data.type != JsonType.Boolean;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x001B1C08 File Offset: 0x001AFE08
		public static explicit operator double(JsonData data)
		{
			bool flag = data.type != JsonType.Double && data.type != JsonType.Int && data.type != JsonType.Long;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x001B1C54 File Offset: 0x001AFE54
		public static explicit operator int(JsonData data)
		{
			bool flag = data.type != JsonType.Int;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x001B1C88 File Offset: 0x001AFE88
		public static explicit operator long(JsonData data)
		{
			bool flag = data.type != JsonType.Long && data.type != JsonType.Int;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x001B1CC8 File Offset: 0x001AFEC8
		public static explicit operator string(JsonData data)
		{
			bool flag = data.type != JsonType.String;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x001B1CFC File Offset: 0x001AFEFC
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x001B1D10 File Offset: 0x001AFF10
		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			this.object_list.Add(item);
			this.json = null;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x001B1D56 File Offset: 0x001AFF56
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x001B1D78 File Offset: 0x001AFF78
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x001B1D98 File Offset: 0x001AFF98
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x001B1DB0 File Offset: 0x001AFFB0
		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.Count; i++)
			{
				bool flag = this.object_list[i].Key == (string)key;
				if (flag)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x001B1E24 File Offset: 0x001B0024
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x001B1E44 File Offset: 0x001B0044
		bool IJsonWrapper.GetBoolean()
		{
			bool flag = this.type != JsonType.Boolean;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x001B1E78 File Offset: 0x001B0078
		double IJsonWrapper.GetDouble()
		{
			bool flag = this.type != JsonType.Double;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x001B1EAC File Offset: 0x001B00AC
		int IJsonWrapper.GetInt()
		{
			bool flag = this.type != JsonType.Int;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x001B1EE0 File Offset: 0x001B00E0
		long IJsonWrapper.GetLong()
		{
			bool flag = this.type != JsonType.Long;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x001B1F14 File Offset: 0x001B0114
		string IJsonWrapper.GetString()
		{
			bool flag = this.type != JsonType.String;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x001B1F48 File Offset: 0x001B0148
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x001B1F60 File Offset: 0x001B0160
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x001B1F78 File Offset: 0x001B0178
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.inst_double = (double)(this.inst_long = (long)val);
			this.json = null;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x001B1FB0 File Offset: 0x001B01B0
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.inst_double = (double)val;
			this.json = null;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x001B1FDD File Offset: 0x001B01DD
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x001B1FF8 File Offset: 0x001B01F8
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x001B2010 File Offset: 0x001B0210
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x001B201C File Offset: 0x001B021C
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x001B2035 File Offset: 0x001B0235
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x001B204C File Offset: 0x001B024C
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x001B206C File Offset: 0x001B026C
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x001B208A File Offset: 0x001B028A
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x001B20A2 File Offset: 0x001B02A2
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x001B20B9 File Offset: 0x001B02B9
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x001B20D0 File Offset: 0x001B02D0
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x001B20FC File Offset: 0x001B02FC
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = this[text] = this.ToJsonData(value);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			this.object_list.Insert(idx, item);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x001B213C File Offset: 0x001B033C
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x001B2180 File Offset: 0x001B0380
		private ICollection EnsureCollection()
		{
			bool flag = this.type == JsonType.Array;
			ICollection result;
			if (flag)
			{
				result = (ICollection)this.inst_array;
			}
			else
			{
				bool flag2 = this.type == JsonType.Object;
				if (!flag2)
				{
					throw new InvalidOperationException("The JsonData instance has to be initialized first");
				}
				result = (ICollection)this.inst_object;
			}
			return result;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x001B21D4 File Offset: 0x001B03D4
		private IDictionary EnsureDictionary()
		{
			bool flag = this.type == JsonType.Object;
			IDictionary result;
			if (flag)
			{
				result = (IDictionary)this.inst_object;
			}
			else
			{
				bool flag2 = this.type > JsonType.None;
				if (flag2)
				{
					throw new InvalidOperationException("Instance of JsonData is not a dictionary");
				}
				this.type = JsonType.Object;
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				result = (IDictionary)this.inst_object;
			}
			return result;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x001B2244 File Offset: 0x001B0444
		private IList EnsureList()
		{
			bool flag = this.type == JsonType.Array;
			IList result;
			if (flag)
			{
				result = (IList)this.inst_array;
			}
			else
			{
				bool flag2 = this.type > JsonType.None;
				if (flag2)
				{
					throw new InvalidOperationException("Instance of JsonData is not a list");
				}
				this.type = JsonType.Array;
				this.inst_array = new List<JsonData>();
				result = (IList)this.inst_array;
			}
			return result;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x001B22A8 File Offset: 0x001B04A8
		private JsonData ToJsonData(object obj)
		{
			bool flag = obj == null;
			JsonData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = obj is JsonData;
				if (flag2)
				{
					result = (JsonData)obj;
				}
				else
				{
					result = new JsonData(obj);
				}
			}
			return result;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x001B22E4 File Offset: 0x001B04E4
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			bool isString = obj.IsString;
			if (isString)
			{
				writer.Write(obj.GetString());
			}
			else
			{
				bool isBoolean = obj.IsBoolean;
				if (isBoolean)
				{
					writer.Write(obj.GetBoolean());
				}
				else
				{
					bool isDouble = obj.IsDouble;
					if (isDouble)
					{
						writer.Write(obj.GetDouble());
					}
					else
					{
						bool isInt = obj.IsInt;
						if (isInt)
						{
							writer.Write(obj.GetInt());
						}
						else
						{
							bool isLong = obj.IsLong;
							if (isLong)
							{
								writer.Write(obj.GetLong());
							}
							else
							{
								bool isArray = obj.IsArray;
								if (isArray)
								{
									writer.WriteArrayStart();
									foreach (object obj2 in obj)
									{
										JsonData.WriteJson((JsonData)obj2, writer);
									}
									writer.WriteArrayEnd();
								}
								else
								{
									bool flag = !obj.IsObject;
									if (!flag)
									{
										writer.WriteObjectStart();
										foreach (object obj3 in obj)
										{
											DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
											writer.WritePropertyName((string)dictionaryEntry.Key);
											JsonData.WriteJson((JsonData)dictionaryEntry.Value, writer);
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

		// Token: 0x060031E7 RID: 12775 RVA: 0x001B2490 File Offset: 0x001B0690
		public int Add(object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(value2);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x001B24C0 File Offset: 0x001B06C0
		public void Clear()
		{
			bool isObject = this.IsObject;
			if (isObject)
			{
				((IDictionary)this).Clear();
			}
			else
			{
				bool isArray = this.IsArray;
				if (isArray)
				{
					((IList)this).Clear();
				}
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x001B24F8 File Offset: 0x001B06F8
		public bool Equals(JsonData x)
		{
			bool flag = x == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = x.type != this.type;
				if (flag2)
				{
					result = false;
				}
				else
				{
					JsonType jsonType = this.type;
					if (!true)
					{
					}
					bool flag3;
					switch (jsonType)
					{
					case JsonType.None:
						flag3 = true;
						break;
					case JsonType.Object:
						flag3 = this.inst_object.Equals(x.inst_object);
						break;
					case JsonType.Array:
						flag3 = this.inst_array.Equals(x.inst_array);
						break;
					case JsonType.String:
						flag3 = this.inst_string.Equals(x.inst_string);
						break;
					case JsonType.Int:
						flag3 = this.inst_int.Equals(x.inst_int);
						break;
					case JsonType.Long:
						flag3 = this.inst_long.Equals(x.inst_long);
						break;
					case JsonType.Double:
						flag3 = this.inst_double.Equals(x.inst_double);
						break;
					case JsonType.Boolean:
						flag3 = this.inst_boolean.Equals(x.inst_boolean);
						break;
					default:
						flag3 = false;
						break;
					}
					if (!true)
					{
					}
					result = flag3;
				}
			}
			return result;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x001B260C File Offset: 0x001B080C
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x001B2624 File Offset: 0x001B0824
		public void SetJsonType(JsonType type)
		{
			bool flag = this.type != type;
			if (flag)
			{
				switch (type)
				{
				case JsonType.Object:
					this.inst_object = new Dictionary<string, JsonData>();
					this.object_list = new List<KeyValuePair<string, JsonData>>();
					break;
				case JsonType.Array:
					this.inst_array = new List<JsonData>();
					break;
				case JsonType.String:
					this.inst_string = null;
					break;
				case JsonType.Int:
					this.inst_int = 0;
					break;
				case JsonType.Long:
					this.inst_long = 0L;
					break;
				case JsonType.Double:
					this.inst_double = 0.0;
					break;
				case JsonType.Boolean:
					this.inst_boolean = false;
					break;
				}
				this.type = type;
			}
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x001B26D4 File Offset: 0x001B08D4
		public string ToJson()
		{
			bool flag = this.json != null;
			string result;
			if (flag)
			{
				result = this.json;
			}
			else
			{
				StringWriter stringWriter = new StringWriter();
				JsonData.WriteJson(this, new JsonWriter(stringWriter)
				{
					Validate = false
				});
				this.json = stringWriter.ToString();
				result = this.json;
			}
			return result;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x001B272C File Offset: 0x001B092C
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x001B275C File Offset: 0x001B095C
		public override string ToString()
		{
			JsonType jsonType = this.type;
			if (!true)
			{
			}
			string result;
			switch (jsonType)
			{
			case JsonType.Object:
				result = "JsonData object";
				break;
			case JsonType.Array:
				result = "JsonData array";
				break;
			case JsonType.String:
				result = this.inst_string;
				break;
			case JsonType.Int:
				result = this.inst_int.ToString();
				break;
			case JsonType.Long:
				result = this.inst_long.ToString();
				break;
			case JsonType.Double:
				result = this.inst_double.ToString();
				break;
			case JsonType.Boolean:
				result = this.inst_boolean.ToString();
				break;
			default:
				result = "Uninitialized JsonData";
				break;
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x04002AAA RID: 10922
		private IList<JsonData> inst_array;

		// Token: 0x04002AAB RID: 10923
		private bool inst_boolean;

		// Token: 0x04002AAC RID: 10924
		private double inst_double;

		// Token: 0x04002AAD RID: 10925
		private int inst_int;

		// Token: 0x04002AAE RID: 10926
		private long inst_long;

		// Token: 0x04002AAF RID: 10927
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x04002AB0 RID: 10928
		private string inst_string;

		// Token: 0x04002AB1 RID: 10929
		private string json;

		// Token: 0x04002AB2 RID: 10930
		private JsonType type;

		// Token: 0x04002AB3 RID: 10931
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}

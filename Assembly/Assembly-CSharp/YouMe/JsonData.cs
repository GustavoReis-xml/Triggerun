using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace YouMe
{
	// Token: 0x020004E8 RID: 1256
	public class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x001AB1A3 File Offset: 0x001A93A3
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x001AB1AB File Offset: 0x001A93AB
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x001AB1B8 File Offset: 0x001A93B8
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x001AB1C5 File Offset: 0x001A93C5
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x001AB1D2 File Offset: 0x001A93D2
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x001AB1E0 File Offset: 0x001A93E0
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

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x001AB250 File Offset: 0x001A9450
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x001AB2C0 File Offset: 0x001A94C0
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x001AB2C8 File Offset: 0x001A94C8
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x001AB2D0 File Offset: 0x001A94D0
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x001AB2D8 File Offset: 0x001A94D8
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x001AB2E0 File Offset: 0x001A94E0
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x001AB2E8 File Offset: 0x001A94E8
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x001AB2F0 File Offset: 0x001A94F0
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x001AB2F8 File Offset: 0x001A94F8
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x001AB305 File Offset: 0x001A9505
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x1700059C RID: 1436
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

		// Token: 0x1700059D RID: 1437
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

		// Token: 0x1700059E RID: 1438
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x001AB446 File Offset: 0x001A9646
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x001AB453 File Offset: 0x001A9653
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x001AB45E File Offset: 0x001A965E
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x001AB469 File Offset: 0x001A9669
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x001AB474 File Offset: 0x001A9674
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x001AB47F File Offset: 0x001A967F
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x001AB48A File Offset: 0x001A968A
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x001AB495 File Offset: 0x001A9695
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x001AB4A0 File Offset: 0x001A96A0
		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object.Keys;
			}
		}

		// Token: 0x170005A8 RID: 1448
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

		// Token: 0x170005A9 RID: 1449
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

		// Token: 0x06002FF3 RID: 12275 RVA: 0x001AB657 File Offset: 0x001A9857
		public JsonData()
		{
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x001AB661 File Offset: 0x001A9861
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x001AB679 File Offset: 0x001A9879
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x001AB691 File Offset: 0x001A9891
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x001AB6A9 File Offset: 0x001A98A9
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x001AB6C4 File Offset: 0x001A98C4
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

		// Token: 0x06002FF9 RID: 12281 RVA: 0x001AB798 File Offset: 0x001A9998
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x001AB7B0 File Offset: 0x001A99B0
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x001AB7C8 File Offset: 0x001A99C8
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x001AB7E0 File Offset: 0x001A99E0
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x001AB7F8 File Offset: 0x001A99F8
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x001AB810 File Offset: 0x001A9A10
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x001AB828 File Offset: 0x001A9A28
		public static explicit operator bool(JsonData data)
		{
			bool flag = data.type != JsonType.Boolean;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x001AB85C File Offset: 0x001A9A5C
		public static explicit operator double(JsonData data)
		{
			bool flag = data.type != JsonType.Double;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x001AB890 File Offset: 0x001A9A90
		public static explicit operator int(JsonData data)
		{
			bool flag = data.type != JsonType.Int;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x001AB8C4 File Offset: 0x001A9AC4
		public static explicit operator long(JsonData data)
		{
			bool flag = data.type != JsonType.Long;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x001AB8F8 File Offset: 0x001A9AF8
		public static explicit operator string(JsonData data)
		{
			bool flag = data.type != JsonType.String;
			if (flag)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x001AB92C File Offset: 0x001A9B2C
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x001AB940 File Offset: 0x001A9B40
		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			this.object_list.Add(item);
			this.json = null;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x001AB986 File Offset: 0x001A9B86
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x001AB9A8 File Offset: 0x001A9BA8
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x001AB9C8 File Offset: 0x001A9BC8
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x001AB9E0 File Offset: 0x001A9BE0
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

		// Token: 0x0600300A RID: 12298 RVA: 0x001ABA54 File Offset: 0x001A9C54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x001ABA74 File Offset: 0x001A9C74
		bool IJsonWrapper.GetBoolean()
		{
			bool flag = this.type != JsonType.Boolean;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x001ABAA8 File Offset: 0x001A9CA8
		double IJsonWrapper.GetDouble()
		{
			bool flag = this.type != JsonType.Double;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x001ABADC File Offset: 0x001A9CDC
		int IJsonWrapper.GetInt()
		{
			bool flag = this.type != JsonType.Int;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x001ABB10 File Offset: 0x001A9D10
		long IJsonWrapper.GetLong()
		{
			bool flag = this.type != JsonType.Long;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x001ABB44 File Offset: 0x001A9D44
		string IJsonWrapper.GetString()
		{
			bool flag = this.type != JsonType.String;
			if (flag)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x001ABB78 File Offset: 0x001A9D78
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x001ABB90 File Offset: 0x001A9D90
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x001ABBA8 File Offset: 0x001A9DA8
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.json = null;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x001ABBC0 File Offset: 0x001A9DC0
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.json = null;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x001ABBD8 File Offset: 0x001A9DD8
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x001ABBF0 File Offset: 0x001A9DF0
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x001ABC08 File Offset: 0x001A9E08
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x001ABC14 File Offset: 0x001A9E14
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x001ABC2D File Offset: 0x001A9E2D
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x001ABC44 File Offset: 0x001A9E44
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x001ABC64 File Offset: 0x001A9E64
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x001ABC82 File Offset: 0x001A9E82
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x001ABC9A File Offset: 0x001A9E9A
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x001ABCB1 File Offset: 0x001A9EB1
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x001ABCC8 File Offset: 0x001A9EC8
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x001ABCF4 File Offset: 0x001A9EF4
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = this[text] = this.ToJsonData(value);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			this.object_list.Insert(idx, item);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x001ABD34 File Offset: 0x001A9F34
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x001ABD78 File Offset: 0x001A9F78
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

		// Token: 0x06003022 RID: 12322 RVA: 0x001ABDCC File Offset: 0x001A9FCC
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

		// Token: 0x06003023 RID: 12323 RVA: 0x001ABE3C File Offset: 0x001AA03C
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

		// Token: 0x06003024 RID: 12324 RVA: 0x001ABEA0 File Offset: 0x001AA0A0
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

		// Token: 0x06003025 RID: 12325 RVA: 0x001ABEDC File Offset: 0x001AA0DC
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			bool flag = obj == null;
			if (flag)
			{
				writer.Write(null);
			}
			else
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
										bool flag2 = !obj.IsObject;
										if (!flag2)
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
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x001AC0A4 File Offset: 0x001AA2A4
		public int Add(object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(value2);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x001AC0D4 File Offset: 0x001AA2D4
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

		// Token: 0x06003028 RID: 12328 RVA: 0x001AC10C File Offset: 0x001AA30C
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

		// Token: 0x06003029 RID: 12329 RVA: 0x001AC220 File Offset: 0x001AA420
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x001AC238 File Offset: 0x001AA438
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

		// Token: 0x0600302B RID: 12331 RVA: 0x001AC2E8 File Offset: 0x001AA4E8
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

		// Token: 0x0600302C RID: 12332 RVA: 0x001AC340 File Offset: 0x001AA540
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x001AC370 File Offset: 0x001AA570
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

		// Token: 0x0400299F RID: 10655
		private IList<JsonData> inst_array;

		// Token: 0x040029A0 RID: 10656
		private bool inst_boolean;

		// Token: 0x040029A1 RID: 10657
		private double inst_double;

		// Token: 0x040029A2 RID: 10658
		private int inst_int;

		// Token: 0x040029A3 RID: 10659
		private long inst_long;

		// Token: 0x040029A4 RID: 10660
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x040029A5 RID: 10661
		private string inst_string;

		// Token: 0x040029A6 RID: 10662
		private string json;

		// Token: 0x040029A7 RID: 10663
		private JsonType type;

		// Token: 0x040029A8 RID: 10664
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}

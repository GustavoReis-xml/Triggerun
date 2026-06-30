using System;
using System.Collections;
using System.Collections.Specialized;

namespace YouMe
{
	// Token: 0x020004EB RID: 1259
	public class JsonMockWrapper : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x001ADD21 File Offset: 0x001ABF21
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x001ADD24 File Offset: 0x001ABF24
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005AC RID: 1452
		object IList.this[int index]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x001ADD3E File Offset: 0x001ABF3E
		int ICollection.Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x001ADD41 File Offset: 0x001ABF41
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x001ADD44 File Offset: 0x001ABF44
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x001ADD47 File Offset: 0x001ABF47
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x001ADD4A File Offset: 0x001ABF4A
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x001ADD4D File Offset: 0x001ABF4D
		ICollection IDictionary.Keys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x001ADD50 File Offset: 0x001ABF50
		ICollection IDictionary.Values
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B4 RID: 1460
		object IDictionary.this[object key]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170005B5 RID: 1461
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x001ADD82 File Offset: 0x001ABF82
		public bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x001ADD85 File Offset: 0x001ABF85
		public bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x001ADD88 File Offset: 0x001ABF88
		public bool IsDouble
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x001ADD8B File Offset: 0x001ABF8B
		public bool IsInt
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x001ADD8E File Offset: 0x001ABF8E
		public bool IsLong
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x001ADD91 File Offset: 0x001ABF91
		public bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x001ADD94 File Offset: 0x001ABF94
		public bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x001ADD98 File Offset: 0x001ABF98
		public bool GetBoolean()
		{
			return false;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x001ADDAC File Offset: 0x001ABFAC
		public double GetDouble()
		{
			return 0.0;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x001ADDC8 File Offset: 0x001ABFC8
		public int GetInt()
		{
			return 0;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x001ADDDC File Offset: 0x001ABFDC
		public JsonType GetJsonType()
		{
			return JsonType.None;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x001ADDF0 File Offset: 0x001ABFF0
		public long GetLong()
		{
			return 0L;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x001ADE04 File Offset: 0x001AC004
		public string GetString()
		{
			return string.Empty;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x001ADE1B File Offset: 0x001AC01B
		public void SetBoolean(bool val)
		{
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x001ADE1E File Offset: 0x001AC01E
		public void SetDouble(double val)
		{
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x001ADE21 File Offset: 0x001AC021
		public void SetInt(int val)
		{
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x001ADE24 File Offset: 0x001AC024
		public void SetJsonType(JsonType type)
		{
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x001ADE27 File Offset: 0x001AC027
		public void SetLong(long val)
		{
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x001ADE2A File Offset: 0x001AC02A
		public void SetString(string val)
		{
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x001ADE30 File Offset: 0x001AC030
		public string ToJson()
		{
			return string.Empty;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x001ADE47 File Offset: 0x001AC047
		public void ToJson(JsonWriter writer)
		{
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x001ADE4C File Offset: 0x001AC04C
		int IList.Add(object value)
		{
			return 0;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x001ADE5F File Offset: 0x001AC05F
		void IList.Clear()
		{
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x001ADE64 File Offset: 0x001AC064
		bool IList.Contains(object value)
		{
			return false;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x001ADE78 File Offset: 0x001AC078
		int IList.IndexOf(object value)
		{
			return -1;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x001ADE8B File Offset: 0x001AC08B
		void IList.Insert(int i, object v)
		{
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x001ADE8E File Offset: 0x001AC08E
		void IList.Remove(object value)
		{
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x001ADE91 File Offset: 0x001AC091
		void IList.RemoveAt(int index)
		{
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x001ADE94 File Offset: 0x001AC094
		void ICollection.CopyTo(Array array, int index)
		{
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x001ADE98 File Offset: 0x001AC098
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x001ADEAB File Offset: 0x001AC0AB
		void IDictionary.Add(object k, object v)
		{
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x001ADEAE File Offset: 0x001AC0AE
		void IDictionary.Clear()
		{
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x001ADEB4 File Offset: 0x001AC0B4
		bool IDictionary.Contains(object key)
		{
			return false;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x001ADEC7 File Offset: 0x001AC0C7
		void IDictionary.Remove(object key)
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x001ADECC File Offset: 0x001AC0CC
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x001ADEE0 File Offset: 0x001AC0E0
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x001ADEF3 File Offset: 0x001AC0F3
		void IOrderedDictionary.Insert(int i, object k, object v)
		{
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x001ADEF6 File Offset: 0x001AC0F6
		void IOrderedDictionary.RemoveAt(int i)
		{
		}
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
	// Token: 0x02000502 RID: 1282
	public interface IJsonWrapper : IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06003174 RID: 12660
		bool IsArray { get; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06003175 RID: 12661
		bool IsBoolean { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06003176 RID: 12662
		bool IsDouble { get; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06003177 RID: 12663
		bool IsInt { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06003178 RID: 12664
		bool IsLong { get; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06003179 RID: 12665
		bool IsObject { get; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600317A RID: 12666
		bool IsString { get; }

		// Token: 0x0600317B RID: 12667
		bool GetBoolean();

		// Token: 0x0600317C RID: 12668
		double GetDouble();

		// Token: 0x0600317D RID: 12669
		int GetInt();

		// Token: 0x0600317E RID: 12670
		JsonType GetJsonType();

		// Token: 0x0600317F RID: 12671
		long GetLong();

		// Token: 0x06003180 RID: 12672
		string GetString();

		// Token: 0x06003181 RID: 12673
		void SetBoolean(bool val);

		// Token: 0x06003182 RID: 12674
		void SetDouble(double val);

		// Token: 0x06003183 RID: 12675
		void SetInt(int val);

		// Token: 0x06003184 RID: 12676
		void SetJsonType(JsonType type);

		// Token: 0x06003185 RID: 12677
		void SetLong(long val);

		// Token: 0x06003186 RID: 12678
		void SetString(string val);

		// Token: 0x06003187 RID: 12679
		string ToJson();

		// Token: 0x06003188 RID: 12680
		void ToJson(JsonWriter writer);
	}
}

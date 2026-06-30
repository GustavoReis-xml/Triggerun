using System;
using System.Collections;
using System.Collections.Specialized;

namespace YouMe
{
	// Token: 0x020004E5 RID: 1253
	public interface IJsonWrapper : IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002FB3 RID: 12211
		bool IsArray { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002FB4 RID: 12212
		bool IsBoolean { get; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002FB5 RID: 12213
		bool IsDouble { get; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002FB6 RID: 12214
		bool IsInt { get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002FB7 RID: 12215
		bool IsLong { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002FB8 RID: 12216
		bool IsObject { get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002FB9 RID: 12217
		bool IsString { get; }

		// Token: 0x06002FBA RID: 12218
		bool GetBoolean();

		// Token: 0x06002FBB RID: 12219
		double GetDouble();

		// Token: 0x06002FBC RID: 12220
		int GetInt();

		// Token: 0x06002FBD RID: 12221
		JsonType GetJsonType();

		// Token: 0x06002FBE RID: 12222
		long GetLong();

		// Token: 0x06002FBF RID: 12223
		string GetString();

		// Token: 0x06002FC0 RID: 12224
		void SetBoolean(bool val);

		// Token: 0x06002FC1 RID: 12225
		void SetDouble(double val);

		// Token: 0x06002FC2 RID: 12226
		void SetInt(int val);

		// Token: 0x06002FC3 RID: 12227
		void SetJsonType(JsonType type);

		// Token: 0x06002FC4 RID: 12228
		void SetLong(long val);

		// Token: 0x06002FC5 RID: 12229
		void SetString(string val);

		// Token: 0x06002FC6 RID: 12230
		string ToJson();

		// Token: 0x06002FC7 RID: 12231
		void ToJson(JsonWriter writer);
	}
}

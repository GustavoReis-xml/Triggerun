using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x0200050E RID: 1294
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x001B6DC2 File Offset: 0x001B4FC2
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x001B6DD0 File Offset: 0x001B4FD0
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x001B6E04 File Offset: 0x001B5004
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x001B6E24 File Offset: 0x001B5024
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x001B6E44 File Offset: 0x001B5044
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x001B6E58 File Offset: 0x001B5058
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x001B6E75 File Offset: 0x001B5075
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x04002B03 RID: 11011
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}

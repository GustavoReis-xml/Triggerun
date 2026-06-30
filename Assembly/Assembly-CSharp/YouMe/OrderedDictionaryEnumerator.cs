using System;
using System.Collections;
using System.Collections.Generic;

namespace YouMe
{
	// Token: 0x020004F2 RID: 1266
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x001B0CCA File Offset: 0x001AEECA
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x001B0CD8 File Offset: 0x001AEED8
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x001B0D0C File Offset: 0x001AEF0C
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x001B0D2C File Offset: 0x001AEF2C
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x001B0D4C File Offset: 0x001AEF4C
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x001B0D60 File Offset: 0x001AEF60
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x001B0D7D File Offset: 0x001AEF7D
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x040029FA RID: 10746
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}

using System;
using System.Collections.Generic;

namespace YouMe
{
	// Token: 0x020004F1 RID: 1265
	internal struct ObjectMetadata
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x001B0C44 File Offset: 0x001AEE44
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x001B0C77 File Offset: 0x001AEE77
		public Type ElementType
		{
			get
			{
				bool flag = this.element_type == null;
				Type typeFromHandle;
				if (flag)
				{
					typeFromHandle = typeof(JsonData);
				}
				else
				{
					typeFromHandle = this.element_type;
				}
				return typeFromHandle;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x001B0C84 File Offset: 0x001AEE84
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x001B0C9C File Offset: 0x001AEE9C
		public bool IsDictionary
		{
			get
			{
				return this.is_dictionary;
			}
			set
			{
				this.is_dictionary = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x001B0CA8 File Offset: 0x001AEEA8
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x001B0CC0 File Offset: 0x001AEEC0
		public IDictionary<string, PropertyMetadata> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x040029F7 RID: 10743
		private Type element_type;

		// Token: 0x040029F8 RID: 10744
		private bool is_dictionary;

		// Token: 0x040029F9 RID: 10745
		private IDictionary<string, PropertyMetadata> properties;
	}
}

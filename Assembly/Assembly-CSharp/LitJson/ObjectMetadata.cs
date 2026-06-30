using System;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x0200050D RID: 1293
	internal struct ObjectMetadata
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x001B6D3C File Offset: 0x001B4F3C
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x001B6D6F File Offset: 0x001B4F6F
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

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x001B6D7C File Offset: 0x001B4F7C
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x001B6D94 File Offset: 0x001B4F94
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

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x001B6DA0 File Offset: 0x001B4FA0
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x001B6DB8 File Offset: 0x001B4FB8
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

		// Token: 0x04002B00 RID: 11008
		private Type element_type;

		// Token: 0x04002B01 RID: 11009
		private bool is_dictionary;

		// Token: 0x04002B02 RID: 11010
		private IDictionary<string, PropertyMetadata> properties;
	}
}

using System;

namespace LitJson
{
	// Token: 0x020004FD RID: 1277
	internal struct ArrayMetadata
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x001B14B8 File Offset: 0x001AF6B8
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x001B14EB File Offset: 0x001AF6EB
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

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x001B14F8 File Offset: 0x001AF6F8
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x001B1510 File Offset: 0x001AF710
		public bool IsArray
		{
			get
			{
				return this.is_array;
			}
			set
			{
				this.is_array = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x001B151C File Offset: 0x001AF71C
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x001B1534 File Offset: 0x001AF734
		public bool IsList
		{
			get
			{
				return this.is_list;
			}
			set
			{
				this.is_list = value;
			}
		}

		// Token: 0x04002A9D RID: 10909
		private Type element_type;

		// Token: 0x04002A9E RID: 10910
		private bool is_array;

		// Token: 0x04002A9F RID: 10911
		private bool is_list;
	}
}

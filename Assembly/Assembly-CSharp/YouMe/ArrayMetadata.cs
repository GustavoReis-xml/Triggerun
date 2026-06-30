using System;

namespace YouMe
{
	// Token: 0x020004DF RID: 1247
	internal struct ArrayMetadata
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x001AB114 File Offset: 0x001A9314
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x001AB147 File Offset: 0x001A9347
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x001AB154 File Offset: 0x001A9354
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x001AB16C File Offset: 0x001A936C
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x001AB178 File Offset: 0x001A9378
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x001AB190 File Offset: 0x001A9390
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

		// Token: 0x0400298C RID: 10636
		private Type element_type;

		// Token: 0x0400298D RID: 10637
		private bool is_array;

		// Token: 0x0400298E RID: 10638
		private bool is_list;
	}
}

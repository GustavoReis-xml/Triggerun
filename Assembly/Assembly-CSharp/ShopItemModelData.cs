using System;

// Token: 0x0200030D RID: 781
public class ShopItemModelData
{
	// Token: 0x060025D6 RID: 9686 RVA: 0x00157C00 File Offset: 0x00155E00
	public int GetZhuanBeiType()
	{
		bool flag = this.nType == 4;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = this.nType == 5;
			if (flag2)
			{
				result = 3;
			}
			else
			{
				bool flag3 = this.nType == 6;
				if (flag3)
				{
					result = 2;
				}
				else
				{
					bool flag4 = this.nType == 7;
					if (flag4)
					{
						result = 4;
					}
					else
					{
						result = 0;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0400219D RID: 8605
	public int nId;

	// Token: 0x0400219E RID: 8606
	public int nType;

	// Token: 0x0400219F RID: 8607
	public int nHeroId;

	// Token: 0x040021A0 RID: 8608
	public int nTypeId;

	// Token: 0x040021A1 RID: 8609
	public ItemData pItemData;
}

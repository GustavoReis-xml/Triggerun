using System;
using System.Collections.Generic;

// Token: 0x0200028C RID: 652
public class PersonListDataSort : IComparer<NetLiving>
{
	// Token: 0x06001DBF RID: 7615 RVA: 0x00115114 File Offset: 0x00113314
	public int Compare(NetLiving x, NetLiving y)
	{
		bool flag = x == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = y == null;
			if (flag2)
			{
				result = 1;
			}
			else
			{
				int num = (int)x.playerCommon.aryWarData[0];
				int num2 = (int)y.playerCommon.aryWarData[0];
				bool flag3 = num == num2;
				if (flag3)
				{
					int num3 = (int)x.playerCommon.aryWarData[1];
					int num4 = (int)y.playerCommon.aryWarData[1];
					bool flag4 = num3 == num4;
					if (flag4)
					{
						int num5 = (int)x.playerCommon.aryWarData[2];
						int value = (int)y.playerCommon.aryWarData[2];
						result = num5.CompareTo(value);
					}
					else
					{
						result = num3.CompareTo(num4);
					}
				}
				else
				{
					result = -num.CompareTo(num2);
				}
			}
		}
		return result;
	}
}

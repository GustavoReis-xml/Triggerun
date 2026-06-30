using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002AC RID: 684
public class PveDataMgr
{
	// Token: 0x06001F5D RID: 8029 RVA: 0x00120078 File Offset: 0x0011E278
	public void SetData(int nFbType, ArrayList szData)
	{
		bool flag = this.AllPveData.ContainsKey(nFbType);
		if (flag)
		{
			this.AllPveData[nFbType] = szData;
		}
		else
		{
			this.AllPveData.Add(nFbType, szData);
		}
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x001200B8 File Offset: 0x0011E2B8
	public ArrayList GetData(int nType)
	{
		return this.AllPveData[nType];
	}

	// Token: 0x04001C9A RID: 7322
	public Dictionary<int, ArrayList> AllPveData = new Dictionary<int, ArrayList>();
}

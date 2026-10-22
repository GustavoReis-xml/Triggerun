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
		if (this.AllPveData.ContainsKey(nType))
		{
			return this.AllPveData[nType];
		}
		if (this.AllPveData.ContainsKey(9))
		{
			return this.AllPveData[9];
		}
		ArrayList defaultData = new ArrayList();
		defaultData.Add(12);
		ArrayList stars = new ArrayList();
		for (int i = 0; i < 12; i++) stars.Add(3);
		defaultData.Add(stars);
		defaultData.Add(0);
		defaultData.Add(0);
		defaultData.Add(1);
		defaultData.Add(nType);
		defaultData.Add(99999);
		defaultData.Add(0);
		defaultData.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		defaultData.Add(0);
		defaultData.Add(0);
		defaultData.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		defaultData.Add(0);
		ArrayList sweep = new ArrayList();
		for (int i = 0; i < 12; i++) sweep.Add(0);
		defaultData.Add(sweep);
		defaultData.Add(0);
		defaultData.Add(0);
		this.AllPveData[nType] = defaultData;
		return defaultData;
	}

	// Token: 0x04001C9A RID: 7322
	public Dictionary<int, ArrayList> AllPveData = new Dictionary<int, ArrayList>();
}

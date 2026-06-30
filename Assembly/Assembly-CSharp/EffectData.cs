using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class EffectData
{
	// Token: 0x060009FA RID: 2554 RVA: 0x0005B450 File Offset: 0x00059650
	public void Init(string sFpsUrl, string sTpsUrl, DateTime dDate, int nType)
	{
		this.m_sFpsUrl = sFpsUrl;
		this.m_sTpsUrl = sTpsUrl;
		this.m_dDate = dDate;
		this.m_nType = nType;
	}

	// Token: 0x04000817 RID: 2071
	public string m_sFpsUrl = string.Empty;

	// Token: 0x04000818 RID: 2072
	public string m_sTpsUrl = string.Empty;

	// Token: 0x04000819 RID: 2073
	public DateTime m_dDate;

	// Token: 0x0400081A RID: 2074
	public int m_nType;

	// Token: 0x0400081B RID: 2075
	public GameObject oLoadObj;
}

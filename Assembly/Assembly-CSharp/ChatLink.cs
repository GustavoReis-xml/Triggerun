using System;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class ChatLink : MonoBehaviour
{
	// Token: 0x0600046A RID: 1130 RVA: 0x00022963 File Offset: 0x00020B63
	public void Init(string sMsg, int nCid)
	{
		this.sUrl = sMsg;
		this.m_nCid = nCid;
	}

	// Token: 0x04000369 RID: 873
	public string sUrl;

	// Token: 0x0400036A RID: 874
	public int m_nCid;
}

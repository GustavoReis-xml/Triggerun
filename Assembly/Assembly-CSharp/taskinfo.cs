using System;
using System.Collections;

// Token: 0x020003FC RID: 1020
public class taskinfo
{
	// Token: 0x06002ACA RID: 10954 RVA: 0x00178C10 File Offset: 0x00176E10
	public void inittask(ArrayList arrinfo)
	{
		this.m_nTaskId = (int)arrinfo[0];
		this.m_nTaskState = (int)arrinfo[1];
		this.m_nfFinishedCount = (int)arrinfo[2];
		this.m_nTotalCount = (int)arrinfo[3];
		this.m_nBeginNpcId = (int)arrinfo[4];
		this.m_nEndNpcId = (int)arrinfo[5];
		this.makeCount((ArrayList)arrinfo[8]);
		this.m_nTasktype = (int)arrinfo[14];
		this.m_nFlogId = (int)arrinfo[10];
		this.m_nAcceptnum = (int)arrinfo[11];
		this.m_nLvReward = 1;
		this.m_nPinZhi = (int)arrinfo[12];
		bool flag = arrinfo.Count > 15;
		if (flag)
		{
			this.sz_targetArr = (ArrayList)arrinfo[15];
			this.m_nTargetFinishCount = (int)arrinfo[16];
		}
		this.sStartTime = arrinfo[6].ToString();
		this.m_nAutoId = (int)arrinfo[17];
	}

	// Token: 0x06002ACB RID: 10955 RVA: 0x00178D4C File Offset: 0x00176F4C
	private void makeCount(ArrayList arrcount)
	{
		bool flag = arrcount.Count != 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)arrcount[0];
			this.m_nCount = (int)arrayList[1];
			arrayList = (ArrayList)arrcount[1];
			this.m_nCdLong = (int)arrayList[1];
		}
	}

	// Token: 0x0400245A RID: 9306
	public int m_nTaskId;

	// Token: 0x0400245B RID: 9307
	public int m_nTaskState;

	// Token: 0x0400245C RID: 9308
	public int m_nCount;

	// Token: 0x0400245D RID: 9309
	public int m_nAcceptnum;

	// Token: 0x0400245E RID: 9310
	public int m_nfFinishedCount;

	// Token: 0x0400245F RID: 9311
	public int m_nTotalCount;

	// Token: 0x04002460 RID: 9312
	public int m_nBeginNpcId;

	// Token: 0x04002461 RID: 9313
	public int m_nEndNpcId;

	// Token: 0x04002462 RID: 9314
	public int m_nTasktype;

	// Token: 0x04002463 RID: 9315
	public int m_nPinZhi;

	// Token: 0x04002464 RID: 9316
	public int m_nLvReward;

	// Token: 0x04002465 RID: 9317
	public int m_nIshistory;

	// Token: 0x04002466 RID: 9318
	public int m_nFlogId;

	// Token: 0x04002467 RID: 9319
	public int m_nTargetFinishCount;

	// Token: 0x04002468 RID: 9320
	public ArrayList sz_targetArr = new ArrayList();

	// Token: 0x04002469 RID: 9321
	public int m_nCdLong;

	// Token: 0x0400246A RID: 9322
	public string sStartTime;

	// Token: 0x0400246B RID: 9323
	public int m_nAutoId;
}

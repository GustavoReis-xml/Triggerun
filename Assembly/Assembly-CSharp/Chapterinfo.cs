using System;
using System.Collections;

// Token: 0x02000079 RID: 121
public class Chapterinfo
{
	// Token: 0x060003F0 RID: 1008 RVA: 0x00020142 File Offset: 0x0001E342
	public void InitChapter(ArrayList arrinfo)
	{
		this.m_nChapterId = (int)arrinfo[0];
		this.m_nFinishedCount = (int)arrinfo[1];
		this.m_nState = (int)arrinfo[2];
	}

	// Token: 0x04000336 RID: 822
	public int m_nChapterId;

	// Token: 0x04000337 RID: 823
	public int m_nState;

	// Token: 0x04000338 RID: 824
	public int m_nFinishedCount;
}

using System;

// Token: 0x020004B0 RID: 1200
public class WWWRes
{
	// Token: 0x06002E18 RID: 11800 RVA: 0x001983A0 File Offset: 0x001965A0
	public override string ToString()
	{
		return string.Format("<i path = \"{0}\" vesion = \"{1}\" starttick = \"{2}\" endtick = \"{3}\" bytes = \"{4}\" level = \"{5}\"/>", new object[]
		{
			this.m_sPath,
			this.m_nVesion,
			this.m_nStartTick,
			this.m_nEndTick,
			this.m_nBytes,
			this.m_nLevel
		});
	}

	// Token: 0x04002782 RID: 10114
	public string m_sPath = string.Empty;

	// Token: 0x04002783 RID: 10115
	public int m_nVesion;

	// Token: 0x04002784 RID: 10116
	public int m_nStartTick;

	// Token: 0x04002785 RID: 10117
	public int m_nEndTick;

	// Token: 0x04002786 RID: 10118
	public int m_nBytes;

	// Token: 0x04002787 RID: 10119
	public int m_nLevel;
}

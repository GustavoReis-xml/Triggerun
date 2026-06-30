using System;

// Token: 0x020004BE RID: 1214
public class zhunxingJiPuSai : zhunxingBase
{
	// Token: 0x06002ED7 RID: 11991 RVA: 0x001A2A14 File Offset: 0x001A0C14
	protected override void Update()
	{
		base.Update();
		base.CheckAutoAir_Yf(this.m_nSkil_1, this.m_nBuff_1);
		base.CheckAutoAir_Df(this.m_nSkil_2, this.m_nBuff_2);
	}

	// Token: 0x04002881 RID: 10369
	private int m_nSkil_1 = 1062;

	// Token: 0x04002882 RID: 10370
	private int m_nSkil_2 = 1064;

	// Token: 0x04002883 RID: 10371
	private int m_nBuff_1 = 1021;

	// Token: 0x04002884 RID: 10372
	private int m_nBuff_2 = 1010;
}

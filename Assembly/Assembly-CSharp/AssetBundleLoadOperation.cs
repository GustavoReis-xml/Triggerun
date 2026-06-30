using System;
using System.Collections;

// Token: 0x02000027 RID: 39
public abstract class AssetBundleLoadOperation : IEnumerator
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000167 RID: 359 RVA: 0x00007FED File Offset: 0x000061ED
	public object Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00007FF0 File Offset: 0x000061F0
	public bool MoveNext()
	{
		return !this.IsDone();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000800B File Offset: 0x0000620B
	public void Reset()
	{
	}

	// Token: 0x0600016A RID: 362
	public abstract bool Update();

	// Token: 0x0600016B RID: 363
	public abstract bool IsDone();
}

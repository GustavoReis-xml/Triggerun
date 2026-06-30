using System;

// Token: 0x0200014A RID: 330
public class Gem_Tips_UICon : Window
{
	// Token: 0x06000DFE RID: 3582 RVA: 0x000841D9 File Offset: 0x000823D9
	protected override void Awake()
	{
		base.Awake();
		Gem_Tips_UICon.Instance = this;
	}

	// Token: 0x04000B5F RID: 2911
	public static Gem_Tips_UICon Instance;
}

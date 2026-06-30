using System;

// Token: 0x020002FD RID: 765
public class Setting_Option_HallCon : Window
{
	// Token: 0x06002483 RID: 9347 RVA: 0x0014A3F5 File Offset: 0x001485F5
	protected override void Awake()
	{
		base.Awake();
		Setting_Option_HallCon.Instance = this;
	}

	// Token: 0x0400208B RID: 8331
	public static Setting_Option_HallCon Instance;
}

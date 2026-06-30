using System;

// Token: 0x020002FC RID: 764
public class Setting_OptionCon : Window
{
	// Token: 0x06002481 RID: 9345 RVA: 0x0014A3DC File Offset: 0x001485DC
	protected override void Awake()
	{
		base.Awake();
		Setting_OptionCon.Instance = this;
	}

	// Token: 0x0400208A RID: 8330
	public static Setting_OptionCon Instance;
}

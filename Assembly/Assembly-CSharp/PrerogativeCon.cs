using System;

// Token: 0x0200029F RID: 671
public class PrerogativeCon : Window
{
	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0011BF08 File Offset: 0x0011A108
	// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0011BF20 File Offset: 0x0011A120
	public Prerogative_View View_Component
	{
		get
		{
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x0011BF2A File Offset: 0x0011A12A
	protected override void Awake()
	{
		base.Awake();
		PrerogativeCon.Instance = this;
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x0011BF3C File Offset: 0x0011A13C
	private void Start()
	{
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x0011BF78 File Offset: 0x0011A178
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x04001C3C RID: 7228
	private Prerogative_View _View_Component;

	// Token: 0x04001C3D RID: 7229
	public static PrerogativeCon Instance;
}

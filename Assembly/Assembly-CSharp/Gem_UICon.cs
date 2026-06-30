using System;

// Token: 0x0200014D RID: 333
public class Gem_UICon : Window
{
	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000EED RID: 3821 RVA: 0x00086BBC File Offset: 0x00084DBC
	// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00086BD4 File Offset: 0x00084DD4
	public Gem_View View_Component
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

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00086BE0 File Offset: 0x00084DE0
	// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00086BF8 File Offset: 0x00084DF8
	public int Hero_ID
	{
		get
		{
			return this._Hero_ID;
		}
		set
		{
			this._Hero_ID = value;
		}
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x00086C02 File Offset: 0x00084E02
	protected override void Awake()
	{
		base.Awake();
		Gem_UICon.Instance = this;
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x00086C14 File Offset: 0x00084E14
	public override void Close()
	{
		bool flag = Gem_Tips_UICon.Instance != null && Gem_Tips_UICon.Instance.visible;
		if (flag)
		{
			this.View_Component.Batch_Synthetic_Place_Cancel(null);
			Gem_Tips_UICon.Instance.Close();
		}
		base.Close();
	}

	// Token: 0x04000BD4 RID: 3028
	public static Gem_UICon Instance;

	// Token: 0x04000BD5 RID: 3029
	private Gem_View _View_Component;

	// Token: 0x04000BD6 RID: 3030
	private int _Hero_ID;
}

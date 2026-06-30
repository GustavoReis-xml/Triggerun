using System;

// Token: 0x020002BB RID: 699
public class QQ_Games_Hall_PrerogativeCon : Window
{
	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06002032 RID: 8242 RVA: 0x001284C8 File Offset: 0x001266C8
	// (set) Token: 0x06002033 RID: 8243 RVA: 0x001284E0 File Offset: 0x001266E0
	public QQ_Games_Hall_Prerogative_View View_Component
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

	// Token: 0x06002034 RID: 8244 RVA: 0x001284EA File Offset: 0x001266EA
	protected override void Awake()
	{
		base.Awake();
		QQ_Games_Hall_PrerogativeCon.Instance = this;
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x001284FC File Offset: 0x001266FC
	private void Start()
	{
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x00128538 File Offset: 0x00126738
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x04001D72 RID: 7538
	private QQ_Games_Hall_Prerogative_View _View_Component;

	// Token: 0x04001D73 RID: 7539
	public static QQ_Games_Hall_PrerogativeCon Instance;
}

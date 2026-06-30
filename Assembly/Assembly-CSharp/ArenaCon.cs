using System;

// Token: 0x02000010 RID: 16
public class ArenaCon : Window
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00003FAC File Offset: 0x000021AC
	// (set) Token: 0x0600003B RID: 59 RVA: 0x00003FC4 File Offset: 0x000021C4
	public bool Fight_State
	{
		get
		{
			return this._Fight_State;
		}
		set
		{
			this._Fight_State = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600003C RID: 60 RVA: 0x00003FD0 File Offset: 0x000021D0
	// (set) Token: 0x0600003D RID: 61 RVA: 0x00003FE8 File Offset: 0x000021E8
	public int Substitution_State
	{
		get
		{
			return this._Substitution_State;
		}
		set
		{
			this._Substitution_State = value;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00003FF4 File Offset: 0x000021F4
	// (set) Token: 0x0600003F RID: 63 RVA: 0x0000400C File Offset: 0x0000220C
	public int Current_Round
	{
		get
		{
			return this._Current_Round;
		}
		set
		{
			this._Current_Round = value;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000040 RID: 64 RVA: 0x00004018 File Offset: 0x00002218
	// (set) Token: 0x06000041 RID: 65 RVA: 0x00004030 File Offset: 0x00002230
	public string Limit_Time
	{
		get
		{
			return this._Limit_Time;
		}
		set
		{
			this._Limit_Time = value;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000042 RID: 66 RVA: 0x0000403C File Offset: 0x0000223C
	// (set) Token: 0x06000043 RID: 67 RVA: 0x00004054 File Offset: 0x00002254
	public DateTime m_dStartChangeTime
	{
		get
		{
			return this._m_dStartChangeTime;
		}
		set
		{
			this._m_dStartChangeTime = value;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000044 RID: 68 RVA: 0x00004060 File Offset: 0x00002260
	// (set) Token: 0x06000045 RID: 69 RVA: 0x00004078 File Offset: 0x00002278
	public Arena_View View_Component
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

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00004084 File Offset: 0x00002284
	// (set) Token: 0x06000047 RID: 71 RVA: 0x0000409C File Offset: 0x0000229C
	public Arena_Controller Controler_Component
	{
		get
		{
			return this._Controler_Component;
		}
		set
		{
			this._Controler_Component = value;
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000040A6 File Offset: 0x000022A6
	protected override void Awake()
	{
		base.Awake();
		ArenaCon.inst = this;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000040B6 File Offset: 0x000022B6
	public override void Reset()
	{
		base.Reset();
	}

	// Token: 0x0400007C RID: 124
	private bool _Fight_State;

	// Token: 0x0400007D RID: 125
	private int _Substitution_State;

	// Token: 0x0400007E RID: 126
	private int _Current_Round;

	// Token: 0x0400007F RID: 127
	private string _Limit_Time = string.Empty;

	// Token: 0x04000080 RID: 128
	private DateTime _m_dStartChangeTime = DateTime.MinValue;

	// Token: 0x04000081 RID: 129
	private Arena_View _View_Component;

	// Token: 0x04000082 RID: 130
	private Arena_Controller _Controler_Component;

	// Token: 0x04000083 RID: 131
	public static ArenaCon inst;
}

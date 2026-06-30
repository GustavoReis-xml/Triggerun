using System;

// Token: 0x020002E0 RID: 736
public class ScrambleCon : Window
{
	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06002200 RID: 8704 RVA: 0x0013CD00 File Offset: 0x0013AF00
	// (set) Token: 0x06002201 RID: 8705 RVA: 0x0013CD18 File Offset: 0x0013AF18
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

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06002202 RID: 8706 RVA: 0x0013CD24 File Offset: 0x0013AF24
	// (set) Token: 0x06002203 RID: 8707 RVA: 0x0013CD3C File Offset: 0x0013AF3C
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

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06002204 RID: 8708 RVA: 0x0013CD48 File Offset: 0x0013AF48
	// (set) Token: 0x06002205 RID: 8709 RVA: 0x0013CD60 File Offset: 0x0013AF60
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

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x06002206 RID: 8710 RVA: 0x0013CD6C File Offset: 0x0013AF6C
	// (set) Token: 0x06002207 RID: 8711 RVA: 0x0013CD84 File Offset: 0x0013AF84
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

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x06002208 RID: 8712 RVA: 0x0013CD90 File Offset: 0x0013AF90
	// (set) Token: 0x06002209 RID: 8713 RVA: 0x0013CDA8 File Offset: 0x0013AFA8
	public Scramble_View View_Component
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

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x0600220A RID: 8714 RVA: 0x0013CDB4 File Offset: 0x0013AFB4
	// (set) Token: 0x0600220B RID: 8715 RVA: 0x0013CDCC File Offset: 0x0013AFCC
	public Scramble_Controller Controler_Component
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

	// Token: 0x0600220C RID: 8716 RVA: 0x0013CDD6 File Offset: 0x0013AFD6
	protected override void Awake()
	{
		base.Awake();
		ScrambleCon.inst = this;
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x0013CDE8 File Offset: 0x0013AFE8
	public override void Reset()
	{
		base.Reset();
		bool flag = !(this.View_Component == null) && !(this.Controler_Component == null);
		if (flag)
		{
			this.Controler_Component.Fight_Prepare_State = false;
			this.Controler_Component.Fight_State = false;
			this.Controler_Component.Door_State = false;
			this.View_Component.Display_Or_Hidden_Arrows(false);
			Door_Control.Get_Instantiate.Parent_Object = null;
		}
	}

	// Token: 0x04001F67 RID: 8039
	public int m_nGameState = -1;

	// Token: 0x04001F68 RID: 8040
	private bool _Fight_State;

	// Token: 0x04001F69 RID: 8041
	private int _Substitution_State;

	// Token: 0x04001F6A RID: 8042
	private string _Limit_Time = string.Empty;

	// Token: 0x04001F6B RID: 8043
	private DateTime _m_dStartChangeTime = DateTime.MinValue;

	// Token: 0x04001F6C RID: 8044
	private Scramble_View _View_Component;

	// Token: 0x04001F6D RID: 8045
	private Scramble_Controller _Controler_Component;

	// Token: 0x04001F6E RID: 8046
	public static ScrambleCon inst;
}

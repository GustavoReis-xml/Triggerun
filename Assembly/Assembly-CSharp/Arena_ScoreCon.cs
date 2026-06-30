using System;

// Token: 0x02000015 RID: 21
public class Arena_ScoreCon : Window
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000089 RID: 137 RVA: 0x00004F34 File Offset: 0x00003134
	// (set) Token: 0x0600008A RID: 138 RVA: 0x00004F4C File Offset: 0x0000314C
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

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600008B RID: 139 RVA: 0x00004F58 File Offset: 0x00003158
	// (set) Token: 0x0600008C RID: 140 RVA: 0x00004F70 File Offset: 0x00003170
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

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600008D RID: 141 RVA: 0x00004F7C File Offset: 0x0000317C
	// (set) Token: 0x0600008E RID: 142 RVA: 0x00004F94 File Offset: 0x00003194
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

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600008F RID: 143 RVA: 0x00004FA0 File Offset: 0x000031A0
	// (set) Token: 0x06000090 RID: 144 RVA: 0x00004FB8 File Offset: 0x000031B8
	public Arena_Score_View View_Component
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

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000091 RID: 145 RVA: 0x00004FC4 File Offset: 0x000031C4
	// (set) Token: 0x06000092 RID: 146 RVA: 0x00004FDC File Offset: 0x000031DC
	public Arena_Score_Controller Controler_Component
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

	// Token: 0x06000093 RID: 147 RVA: 0x00004FE6 File Offset: 0x000031E6
	protected override void Awake()
	{
		base.Awake();
		Arena_ScoreCon.inst = this;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004FF6 File Offset: 0x000031F6
	public override void Reset()
	{
		base.Reset();
	}

	// Token: 0x04000098 RID: 152
	private bool _Fight_State;

	// Token: 0x04000099 RID: 153
	private int _Substitution_State;

	// Token: 0x0400009A RID: 154
	private string _Limit_Time = string.Empty;

	// Token: 0x0400009B RID: 155
	private Arena_Score_View _View_Component;

	// Token: 0x0400009C RID: 156
	private Arena_Score_Controller _Controler_Component;

	// Token: 0x0400009D RID: 157
	public static Arena_ScoreCon inst;
}

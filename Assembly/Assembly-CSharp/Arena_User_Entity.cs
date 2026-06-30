using System;
using System.Collections;

// Token: 0x0200001E RID: 30
public class Arena_User_Entity
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000124 RID: 292 RVA: 0x00006FF4 File Offset: 0x000051F4
	// (set) Token: 0x06000125 RID: 293 RVA: 0x0000700C File Offset: 0x0000520C
	public GamePlayer GamePlayer_Object
	{
		get
		{
			return this._GamePlayer_Object;
		}
		set
		{
			this._GamePlayer_Object = value;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000126 RID: 294 RVA: 0x00007018 File Offset: 0x00005218
	// (set) Token: 0x06000127 RID: 295 RVA: 0x00007030 File Offset: 0x00005230
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			this._ID = value;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000128 RID: 296 RVA: 0x0000703C File Offset: 0x0000523C
	// (set) Token: 0x06000129 RID: 297 RVA: 0x00007054 File Offset: 0x00005254
	public int Kill_Number
	{
		get
		{
			return this._Kill_Number;
		}
		set
		{
			this._Kill_Number = value;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600012A RID: 298 RVA: 0x00007060 File Offset: 0x00005260
	// (set) Token: 0x0600012B RID: 299 RVA: 0x00007078 File Offset: 0x00005278
	public int Assists_Number
	{
		get
		{
			return this._Assists_Number;
		}
		set
		{
			this._Assists_Number = value;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600012C RID: 300 RVA: 0x00007084 File Offset: 0x00005284
	// (set) Token: 0x0600012D RID: 301 RVA: 0x0000709C File Offset: 0x0000529C
	public int Death_Number
	{
		get
		{
			return this._Death_Number;
		}
		set
		{
			this._Death_Number = value;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600012E RID: 302 RVA: 0x000070A8 File Offset: 0x000052A8
	// (set) Token: 0x0600012F RID: 303 RVA: 0x000070C0 File Offset: 0x000052C0
	public int Model_Index
	{
		get
		{
			return this._Model_Index;
		}
		set
		{
			this._Model_Index = value;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000130 RID: 304 RVA: 0x000070CC File Offset: 0x000052CC
	// (set) Token: 0x06000131 RID: 305 RVA: 0x000070E4 File Offset: 0x000052E4
	public int State
	{
		get
		{
			return this._State;
		}
		set
		{
			this._State = value;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000132 RID: 306 RVA: 0x000070F0 File Offset: 0x000052F0
	// (set) Token: 0x06000133 RID: 307 RVA: 0x00007108 File Offset: 0x00005308
	public int Forces
	{
		get
		{
			return this._Forces;
		}
		set
		{
			this._Forces = value;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00007114 File Offset: 0x00005314
	public void Set_Data(int _ID, ArrayList _Parameters)
	{
		this.GamePlayer_Object = (GamePlayer)Fun.Get_NetLiving(_ID);
		this.ID = _ID;
		this.Kill_Number = int.Parse(_Parameters[1].ToString());
		this.Assists_Number = int.Parse(_Parameters[2].ToString());
		this.Death_Number = int.Parse(_Parameters[3].ToString());
		this.Model_Index = int.Parse(_Parameters[4].ToString());
		this.State = int.Parse(_Parameters[5].ToString());
		this.Forces = int.Parse(_Parameters[6].ToString());
	}

	// Token: 0x040000D8 RID: 216
	private GamePlayer _GamePlayer_Object;

	// Token: 0x040000D9 RID: 217
	private int _ID;

	// Token: 0x040000DA RID: 218
	private int _Kill_Number;

	// Token: 0x040000DB RID: 219
	private int _Assists_Number;

	// Token: 0x040000DC RID: 220
	private int _Death_Number;

	// Token: 0x040000DD RID: 221
	private int _Model_Index;

	// Token: 0x040000DE RID: 222
	private int _State;

	// Token: 0x040000DF RID: 223
	private int _Forces;
}

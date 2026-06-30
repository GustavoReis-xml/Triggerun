using System;
using System.Collections;

// Token: 0x0200001B RID: 27
public class Arena_Score_User_Entity
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000623C File Offset: 0x0000443C
	// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006254 File Offset: 0x00004454
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

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006260 File Offset: 0x00004460
	// (set) Token: 0x060000E6 RID: 230 RVA: 0x00006278 File Offset: 0x00004478
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

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006284 File Offset: 0x00004484
	// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000629C File Offset: 0x0000449C
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

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x000062A8 File Offset: 0x000044A8
	// (set) Token: 0x060000EA RID: 234 RVA: 0x000062C0 File Offset: 0x000044C0
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

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060000EB RID: 235 RVA: 0x000062CC File Offset: 0x000044CC
	// (set) Token: 0x060000EC RID: 236 RVA: 0x000062E4 File Offset: 0x000044E4
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

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060000ED RID: 237 RVA: 0x000062F0 File Offset: 0x000044F0
	// (set) Token: 0x060000EE RID: 238 RVA: 0x00006308 File Offset: 0x00004508
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

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060000EF RID: 239 RVA: 0x00006314 File Offset: 0x00004514
	// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000632C File Offset: 0x0000452C
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

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006338 File Offset: 0x00004538
	// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006350 File Offset: 0x00004550
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

	// Token: 0x060000F3 RID: 243 RVA: 0x0000635C File Offset: 0x0000455C
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

	// Token: 0x040000BC RID: 188
	private GamePlayer _GamePlayer_Object;

	// Token: 0x040000BD RID: 189
	private int _ID;

	// Token: 0x040000BE RID: 190
	private int _Kill_Number;

	// Token: 0x040000BF RID: 191
	private int _Assists_Number;

	// Token: 0x040000C0 RID: 192
	private int _Death_Number;

	// Token: 0x040000C1 RID: 193
	private int _Model_Index;

	// Token: 0x040000C2 RID: 194
	private int _State;

	// Token: 0x040000C3 RID: 195
	private int _Forces;
}

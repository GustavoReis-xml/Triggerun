using System;
using System.Collections;

// Token: 0x020002E7 RID: 743
public class Scramble_User_Entity
{
	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x060022A2 RID: 8866 RVA: 0x0013F360 File Offset: 0x0013D560
	// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0013F378 File Offset: 0x0013D578
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

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0013F384 File Offset: 0x0013D584
	// (set) Token: 0x060022A5 RID: 8869 RVA: 0x0013F39C File Offset: 0x0013D59C
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

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0013F3A8 File Offset: 0x0013D5A8
	// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0013F3C0 File Offset: 0x0013D5C0
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

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0013F3CC File Offset: 0x0013D5CC
	// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0013F3E4 File Offset: 0x0013D5E4
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

	// Token: 0x060022AA RID: 8874 RVA: 0x0013F3F0 File Offset: 0x0013D5F0
	public void Set_Data(int _ID, ArrayList _Parameters)
	{
		this.ID = _ID;
		this.Model_Index = int.Parse(_Parameters[1].ToString());
		this.State = int.Parse(_Parameters[2].ToString());
		this.Forces = int.Parse(_Parameters[3].ToString());
	}

	// Token: 0x04001FB4 RID: 8116
	private int _ID;

	// Token: 0x04001FB5 RID: 8117
	private int _Model_Index;

	// Token: 0x04001FB6 RID: 8118
	private int _State;

	// Token: 0x04001FB7 RID: 8119
	private int _Forces;
}

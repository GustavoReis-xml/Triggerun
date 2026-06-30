using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class Npc_AI_Entity
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00105DA8 File Offset: 0x00103FA8
	// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00105DC0 File Offset: 0x00103FC0
	public int Index
	{
		get
		{
			return this._Index;
		}
		set
		{
			this._Index = value;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00105DCC File Offset: 0x00103FCC
	// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00105DE4 File Offset: 0x00103FE4
	public float Resurgence_Time
	{
		get
		{
			return this._Resurgence_Time;
		}
		set
		{
			this._Resurgence_Time = value;
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00105DF0 File Offset: 0x00103FF0
	// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x00105E08 File Offset: 0x00104008
	public int Move_Type
	{
		get
		{
			return this._Move_Type;
		}
		set
		{
			this._Move_Type = value;
		}
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00105E14 File Offset: 0x00104014
	// (set) Token: 0x06001BCB RID: 7115 RVA: 0x00105E2C File Offset: 0x0010402C
	public float Move_Range
	{
		get
		{
			return this._Move_Range;
		}
		set
		{
			this._Move_Range = value;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00105E38 File Offset: 0x00104038
	// (set) Token: 0x06001BCD RID: 7117 RVA: 0x00105E50 File Offset: 0x00104050
	public float Alert_Range
	{
		get
		{
			return this._Alert_Range;
		}
		set
		{
			this._Alert_Range = value;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00105E5C File Offset: 0x0010405C
	// (set) Token: 0x06001BCF RID: 7119 RVA: 0x00105E8E File Offset: 0x0010408E
	public Dictionary<int, ArrayList> Node_Data
	{
		get
		{
			bool flag = this._Node_Data == null;
			if (flag)
			{
				this._Node_Data = new Dictionary<int, ArrayList>();
			}
			return this._Node_Data;
		}
		set
		{
			this._Node_Data = value;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00105E98 File Offset: 0x00104098
	// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00105EB0 File Offset: 0x001040B0
	public Vector3 Initial_Position
	{
		get
		{
			return this._Initial_Position;
		}
		set
		{
			this._Initial_Position = value;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00105EBC File Offset: 0x001040BC
	// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00105ED4 File Offset: 0x001040D4
	public Vector3 Initial_Rotation
	{
		get
		{
			return this._Initial_Rotation;
		}
		set
		{
			this._Initial_Rotation = value;
		}
	}

	// Token: 0x0400190B RID: 6411
	private int _Index;

	// Token: 0x0400190C RID: 6412
	private float _Resurgence_Time;

	// Token: 0x0400190D RID: 6413
	private int _Move_Type;

	// Token: 0x0400190E RID: 6414
	private float _Move_Range;

	// Token: 0x0400190F RID: 6415
	private float _Alert_Range;

	// Token: 0x04001910 RID: 6416
	private Dictionary<int, ArrayList> _Node_Data;

	// Token: 0x04001911 RID: 6417
	private Vector3 _Initial_Position = Vector3.zero;

	// Token: 0x04001912 RID: 6418
	private Vector3 _Initial_Rotation = Vector3.zero;
}

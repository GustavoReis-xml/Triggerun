using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class CCheckEditDateTime
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001CE44 File Offset: 0x0001B044
	// (set) Token: 0x060003A4 RID: 932 RVA: 0x0001CE6E File Offset: 0x0001B06E
	public DateTime CCParm
	{
		get
		{
			return this._Parm.AddSeconds((double)(0f - this.seed));
		}
		set
		{
			this.seed = UnityEngine.Random.Range(16.2f, 320.6f);
			this._Parm = value.AddSeconds((double)this.seed);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001CE9A File Offset: 0x0001B09A
	public CCheckEditDateTime(DateTime _f)
	{
		this.CCParm = _f;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001CEAC File Offset: 0x0001B0AC
	public bool equla(DateTime _comp)
	{
		float num = Mathf.Abs((float)(this.CCParm - _comp).TotalSeconds);
		return num < 0.1f;
	}

	// Token: 0x040002F1 RID: 753
	private DateTime _Parm;

	// Token: 0x040002F2 RID: 754
	private float seed;
}

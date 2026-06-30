using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class CCheckEditFloat
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001CEEC File Offset: 0x0001B0EC
	// (set) Token: 0x060003A8 RID: 936 RVA: 0x0001CF0B File Offset: 0x0001B10B
	public float CCParm
	{
		get
		{
			return this._Parm - this.seed;
		}
		set
		{
			this.seed = UnityEngine.Random.Range(0.2f, 5.6f);
			this._Parm = this.seed + value;
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001CF31 File Offset: 0x0001B131
	public CCheckEditFloat(float _f)
	{
		this.CCParm = _f;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001CF44 File Offset: 0x0001B144
	public bool equla(float _comp, out float _data)
	{
		_data = Mathf.Abs(this.CCParm - _comp);
		return _data < 0.005f;
	}

	// Token: 0x040002F3 RID: 755
	private float _Parm;

	// Token: 0x040002F4 RID: 756
	private float seed;
}

using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class AIMovePos
{
	// Token: 0x06000020 RID: 32 RVA: 0x000034F5 File Offset: 0x000016F5
	public AIMovePos()
	{
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003500 File Offset: 0x00001700
	public AIMovePos(Vector3 pos, float time, float area, int istrans)
	{
		this._movePos = pos;
		this.StayArea = area;
		this.StayTime = time;
		bool flag = istrans == 0;
		if (flag)
		{
			this.b_isTrans = false;
		}
		else
		{
			this.b_isTrans = true;
		}
	}

	// Token: 0x0400004A RID: 74
	public Vector3 _movePos;

	// Token: 0x0400004B RID: 75
	public float StayArea;

	// Token: 0x0400004C RID: 76
	public float StayTime;

	// Token: 0x0400004D RID: 77
	public bool b_isTrans;
}

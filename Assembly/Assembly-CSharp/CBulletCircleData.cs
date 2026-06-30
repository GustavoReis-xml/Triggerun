using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class CBulletCircleData : CBulletData
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060002F1 RID: 753 RVA: 0x00016323 File Offset: 0x00014523
	public int hitDamage
	{
		get
		{
			return this._hitDamage;
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001632C File Offset: 0x0001452C
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		this._hitDamage = (int)arrayList[0];
	}

	// Token: 0x040002A4 RID: 676
	private int _hitDamage;
}

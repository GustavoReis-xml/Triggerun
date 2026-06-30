using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class CBulletPhysicalData : CBulletData
{
	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000350 RID: 848 RVA: 0x00019589 File Offset: 0x00017789
	public float weaponRadius
	{
		get
		{
			return this._weaponRadius;
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00019594 File Offset: 0x00017794
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		this._weaponRadius = (float)((int)arrayList[0]) * 0.01f;
	}

	// Token: 0x040002C1 RID: 705
	private float _weaponRadius;
}

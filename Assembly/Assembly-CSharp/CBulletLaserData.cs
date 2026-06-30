using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class CBulletLaserData : CBulletData
{
	// Token: 0x0600032D RID: 813 RVA: 0x00017180 File Offset: 0x00015380
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		base.bulletFlySpeed = (float)((int)arrayList[0]);
		base.bulletMaxDistance = (float)((int)arrayList[1]);
	}
}

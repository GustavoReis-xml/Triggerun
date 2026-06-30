using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class CBulletNormalData : CBulletData
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000346 RID: 838 RVA: 0x00018EDC File Offset: 0x000170DC
	// (set) Token: 0x06000347 RID: 839 RVA: 0x00018EF4 File Offset: 0x000170F4
	public int MoreNumber
	{
		get
		{
			return this._moreNumber;
		}
		set
		{
			this._moreNumber = value;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000348 RID: 840 RVA: 0x00018F00 File Offset: 0x00017100
	// (set) Token: 0x06000349 RID: 841 RVA: 0x00018F18 File Offset: 0x00017118
	public int MoreRate
	{
		get
		{
			return this._moreRate;
		}
		set
		{
			this._moreRate = value;
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00018F24 File Offset: 0x00017124
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		base.bulletFlySpeed = (float)((int)arrayList[0]);
		base.bulletMaxDistance = (float)((int)arrayList[1]);
		this._moreNumber = (int)arrayList[2];
		this._moreRate = (int)arrayList[3];
	}

	// Token: 0x040002BF RID: 703
	private int _moreNumber;

	// Token: 0x040002C0 RID: 704
	private int _moreRate;
}

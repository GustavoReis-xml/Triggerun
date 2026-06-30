using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class CBulletTrackData : CBulletData
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000379 RID: 889 RVA: 0x0001AA8B File Offset: 0x00018C8B
	public float initSpeed
	{
		get
		{
			return this._initSpeed;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600037A RID: 890 RVA: 0x0001AA93 File Offset: 0x00018C93
	public float initRotationSpeed
	{
		get
		{
			return this._initRotationSpeed;
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600037B RID: 891 RVA: 0x0001AA9B File Offset: 0x00018C9B
	public float acceleratedSpeed
	{
		get
		{
			return this._acceleratedSpeed;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600037C RID: 892 RVA: 0x0001AAA3 File Offset: 0x00018CA3
	public float acceleratedRotationSpeed
	{
		get
		{
			return this._acceleratedRotationSpeed;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600037D RID: 893 RVA: 0x0001AAAB File Offset: 0x00018CAB
	public float acceleratedSpeedMax
	{
		get
		{
			return this._acceleratedSpeedMax;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x0600037E RID: 894 RVA: 0x0001AAB3 File Offset: 0x00018CB3
	public float acceleratedRotationSpeedMax
	{
		get
		{
			return this._acceleratedRotationSpeedMax;
		}
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0001AABC File Offset: 0x00018CBC
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		this._initSpeed = (float)((int)arrayList[0]);
		this._initRotationSpeed = (float)((int)arrayList[1]);
		this._acceleratedSpeed = (float)((int)arrayList[2]);
		this._acceleratedRotationSpeed = (float)((int)arrayList[3]);
		this._acceleratedSpeedMax = (float)((int)arrayList[4]);
		this._acceleratedRotationSpeedMax = (float)((int)arrayList[5]);
	}

	// Token: 0x040002D1 RID: 721
	private float _initSpeed;

	// Token: 0x040002D2 RID: 722
	private float _initRotationSpeed;

	// Token: 0x040002D3 RID: 723
	private float _acceleratedSpeed;

	// Token: 0x040002D4 RID: 724
	private float _acceleratedRotationSpeed;

	// Token: 0x040002D5 RID: 725
	private float _acceleratedSpeedMax;

	// Token: 0x040002D6 RID: 726
	private float _acceleratedRotationSpeedMax;
}

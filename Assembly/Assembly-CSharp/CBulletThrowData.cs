using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class CBulletThrowData : CBulletData
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000358 RID: 856 RVA: 0x00019764 File Offset: 0x00017964
	// (set) Token: 0x06000359 RID: 857 RVA: 0x0001977C File Offset: 0x0001797C
	public float bulletphysicsForce
	{
		get
		{
			return this._bulletphysicsForce;
		}
		set
		{
			this._bulletphysicsForce = value;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600035A RID: 858 RVA: 0x00019788 File Offset: 0x00017988
	// (set) Token: 0x0600035B RID: 859 RVA: 0x000197A0 File Offset: 0x000179A0
	public float bulletMass
	{
		get
		{
			return this._bulletMass;
		}
		set
		{
			this._bulletMass = value;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600035C RID: 860 RVA: 0x000197AC File Offset: 0x000179AC
	// (set) Token: 0x0600035D RID: 861 RVA: 0x000197C4 File Offset: 0x000179C4
	public float bulletDrag
	{
		get
		{
			return this._bulletDrag;
		}
		set
		{
			this._bulletDrag = value;
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600035E RID: 862 RVA: 0x000197CE File Offset: 0x000179CE
	public bool bulletGravity
	{
		get
		{
			return this._bulletGravity;
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600035F RID: 863 RVA: 0x000197D8 File Offset: 0x000179D8
	// (set) Token: 0x06000360 RID: 864 RVA: 0x000197F0 File Offset: 0x000179F0
	public float checkTime
	{
		get
		{
			return this._checkTime;
		}
		set
		{
			this._checkTime = value;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000361 RID: 865 RVA: 0x000197FC File Offset: 0x000179FC
	// (set) Token: 0x06000362 RID: 866 RVA: 0x00019814 File Offset: 0x00017A14
	public int AngleRate
	{
		get
		{
			return this._angleRate;
		}
		set
		{
			this._angleRate = value;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000363 RID: 867 RVA: 0x00019820 File Offset: 0x00017A20
	// (set) Token: 0x06000364 RID: 868 RVA: 0x00019838 File Offset: 0x00017A38
	public int parmNumber
	{
		get
		{
			return this._parmNumber;
		}
		set
		{
			this._parmNumber = value;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000365 RID: 869 RVA: 0x00019844 File Offset: 0x00017A44
	// (set) Token: 0x06000366 RID: 870 RVA: 0x0001985C File Offset: 0x00017A5C
	public int parmType
	{
		get
		{
			return this._parmType;
		}
		set
		{
			this._parmType = value;
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00019868 File Offset: 0x00017A68
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		this._bulletphysicsForce = (float)arrayList[0];
		this._bulletMass = (float)arrayList[1];
		this._bulletDrag = (float)arrayList[2];
		this._bulletGravity = (bool)arrayList[3];
		base.centerUp = (float)((int)arrayList[4]) * 0.001f;
		this._checkTime = (float)((int)arrayList[5]) * 0.001f;
		ArrayList arrayList2 = (ArrayList)arrayList[6];
		bool flag = arrayList2.Count > 0;
		if (flag)
		{
			this.parmType = (int)arrayList2[0];
			this._angleRate = (int)arrayList2[1];
			this._parmNumber = (int)arrayList2[2];
		}
	}

	// Token: 0x040002C2 RID: 706
	private float _bulletphysicsForce;

	// Token: 0x040002C3 RID: 707
	private float _bulletMass;

	// Token: 0x040002C4 RID: 708
	private float _bulletDrag;

	// Token: 0x040002C5 RID: 709
	private bool _bulletGravity;

	// Token: 0x040002C6 RID: 710
	private float _checkTime;

	// Token: 0x040002C7 RID: 711
	private int _angleRate;

	// Token: 0x040002C8 RID: 712
	private int _parmNumber;

	// Token: 0x040002C9 RID: 713
	private int _parmType;
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class CBulletContraData : CBulletData
{
	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060002FA RID: 762 RVA: 0x0001643C File Offset: 0x0001463C
	// (set) Token: 0x060002FB RID: 763 RVA: 0x00016454 File Offset: 0x00014654
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

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060002FC RID: 764 RVA: 0x00016460 File Offset: 0x00014660
	// (set) Token: 0x060002FD RID: 765 RVA: 0x00016478 File Offset: 0x00014678
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

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060002FE RID: 766 RVA: 0x00016484 File Offset: 0x00014684
	// (set) Token: 0x060002FF RID: 767 RVA: 0x0001649C File Offset: 0x0001469C
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

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000300 RID: 768 RVA: 0x000164A8 File Offset: 0x000146A8
	// (set) Token: 0x06000301 RID: 769 RVA: 0x000164C0 File Offset: 0x000146C0
	public Quaternion[] Qus
	{
		get
		{
			return this._qus;
		}
		set
		{
			this._qus = value;
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x000164CC File Offset: 0x000146CC
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		base.bulletFlySpeed = (float)((int)arrayList[0]);
		base.bulletMaxDistance = (float)((int)arrayList[1]);
		this._moreNumber = (int)arrayList[2];
		this._moreRate = (int)arrayList[3];
		this._angleRate = (int)arrayList[4];
		int num = (int)arrayList[5];
		int angleRate = this._angleRate;
		this._qus = new Quaternion[num];
		for (int i = 0; i < num; i++)
		{
			int num2 = i % 2;
			int num3 = i / 2 + num2;
			int num4 = (num2 == 0) ? 1 : -1;
			this._qus[i] = Quaternion.Euler(0f, (float)(angleRate * num3 * num4), 0f);
		}
	}

	// Token: 0x040002A5 RID: 677
	private int _moreNumber;

	// Token: 0x040002A6 RID: 678
	private int _moreRate;

	// Token: 0x040002A7 RID: 679
	private int _angleRate;

	// Token: 0x040002A8 RID: 680
	private Quaternion[] _qus;
}

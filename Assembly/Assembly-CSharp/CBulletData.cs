using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class CBulletData
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000308 RID: 776 RVA: 0x00016B00 File Offset: 0x00014D00
	// (set) Token: 0x06000309 RID: 777 RVA: 0x00016B18 File Offset: 0x00014D18
	public int bulletType
	{
		get
		{
			return this._bulletType;
		}
		set
		{
			this._bulletType = value;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x0600030A RID: 778 RVA: 0x00016B24 File Offset: 0x00014D24
	// (set) Token: 0x0600030B RID: 779 RVA: 0x00016B3C File Offset: 0x00014D3C
	public string bulletBoomModel
	{
		get
		{
			return this._bulletBoomModel;
		}
		set
		{
			this._bulletBoomModel = value;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x0600030C RID: 780 RVA: 0x00016B48 File Offset: 0x00014D48
	// (set) Token: 0x0600030D RID: 781 RVA: 0x00016B60 File Offset: 0x00014D60
	public string bulletBoomSound
	{
		get
		{
			return this._bulletBoomSound;
		}
		set
		{
			this._bulletBoomSound = value;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600030E RID: 782 RVA: 0x00016B6A File Offset: 0x00014D6A
	public bool bulletBoomBlock
	{
		get
		{
			return this._bulletBoomBlock;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x0600030F RID: 783 RVA: 0x00016B72 File Offset: 0x00014D72
	public float bulletBoomCullBack
	{
		get
		{
			return this._bulletBoomCullBack;
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000310 RID: 784 RVA: 0x00016B7C File Offset: 0x00014D7C
	// (set) Token: 0x06000311 RID: 785 RVA: 0x00016B94 File Offset: 0x00014D94
	public float bulletBoomRange
	{
		get
		{
			return this._bulletBoomRange;
		}
		set
		{
			this._bulletBoomRange = value;
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000312 RID: 786 RVA: 0x00016BA0 File Offset: 0x00014DA0
	// (set) Token: 0x06000313 RID: 787 RVA: 0x00016BB8 File Offset: 0x00014DB8
	public float bulletFlySpeed
	{
		get
		{
			return this._bulletFlySpeed;
		}
		set
		{
			this._bulletFlySpeed = value;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000314 RID: 788 RVA: 0x00016BC4 File Offset: 0x00014DC4
	// (set) Token: 0x06000315 RID: 789 RVA: 0x00016BDC File Offset: 0x00014DDC
	public float bulletMaxDistance
	{
		get
		{
			return this._bulletMaxDistance;
		}
		set
		{
			this._bulletMaxDistance = value;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000316 RID: 790 RVA: 0x00016BE8 File Offset: 0x00014DE8
	// (set) Token: 0x06000317 RID: 791 RVA: 0x00016C00 File Offset: 0x00014E00
	public int bulletChildNumber
	{
		get
		{
			return this._bulletChildNumber;
		}
		set
		{
			this._bulletChildNumber = value;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000318 RID: 792 RVA: 0x00016C0C File Offset: 0x00014E0C
	// (set) Token: 0x06000319 RID: 793 RVA: 0x00016C24 File Offset: 0x00014E24
	public bool bulletTrigger
	{
		get
		{
			return this._bulletTrigger;
		}
		set
		{
			this._bulletTrigger = value;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x0600031A RID: 794 RVA: 0x00016C30 File Offset: 0x00014E30
	// (set) Token: 0x0600031B RID: 795 RVA: 0x00016C48 File Offset: 0x00014E48
	public float bulletBoomDelay
	{
		get
		{
			return this._bulletBoomDelay;
		}
		set
		{
			this._bulletBoomDelay = value;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600031C RID: 796 RVA: 0x00016C54 File Offset: 0x00014E54
	// (set) Token: 0x0600031D RID: 797 RVA: 0x00016C6C File Offset: 0x00014E6C
	public float bulletDestoryTimeOut
	{
		get
		{
			return this._bulletDestoryTimeOut;
		}
		set
		{
			this._bulletDestoryTimeOut = value;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x0600031E RID: 798 RVA: 0x00016C78 File Offset: 0x00014E78
	// (set) Token: 0x0600031F RID: 799 RVA: 0x00016C90 File Offset: 0x00014E90
	public int triggerType
	{
		get
		{
			return this._triggerType;
		}
		set
		{
			this._triggerType = value;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000320 RID: 800 RVA: 0x00016C9C File Offset: 0x00014E9C
	// (set) Token: 0x06000321 RID: 801 RVA: 0x00016CB4 File Offset: 0x00014EB4
	public int bulletThrough
	{
		get
		{
			return this._bulletThrough;
		}
		set
		{
			this._bulletThrough = value;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000322 RID: 802 RVA: 0x00016CC0 File Offset: 0x00014EC0
	// (set) Token: 0x06000323 RID: 803 RVA: 0x00016CD8 File Offset: 0x00014ED8
	public float centerUp
	{
		get
		{
			return this._centerUp;
		}
		set
		{
			this._centerUp = value;
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00016CE4 File Offset: 0x00014EE4
	public virtual void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		ArrayList arrayList = (ArrayList)mainParam[1];
		this._bulletType = bullet_type;
		this._bulletBoomModel = (string)arrayList[0];
		this._bulletBoomSound = (string)arrayList[1];
		this._bulletBoomRange = (float)((int)arrayList[2]) * 0.001f;
		this._bulletBoomDelay = (float)((int)arrayList[3]) * 0.001f;
		this._bulletDestoryTimeOut = (float)((int)arrayList[4]) * 0.001f;
		this._bulletTrigger = (bool)arrayList[5];
		this._triggerType = (int)arrayList[6];
		this._bulletThrough = (int)arrayList[7];
		this._bulletBoomBlock = (bool)arrayList[8];
		this._bulletBoomCullBack = (float)((int)arrayList[9]) * 0.0002f;
	}

	// Token: 0x040002A9 RID: 681
	private int _bulletType;

	// Token: 0x040002AA RID: 682
	private string _bulletBoomModel;

	// Token: 0x040002AB RID: 683
	private string _bulletBoomSound;

	// Token: 0x040002AC RID: 684
	private bool _bulletBoomBlock;

	// Token: 0x040002AD RID: 685
	private float _bulletBoomCullBack;

	// Token: 0x040002AE RID: 686
	private float _bulletBoomRange;

	// Token: 0x040002AF RID: 687
	private float _bulletFlySpeed;

	// Token: 0x040002B0 RID: 688
	private float _bulletMaxDistance;

	// Token: 0x040002B1 RID: 689
	private int _bulletChildNumber;

	// Token: 0x040002B2 RID: 690
	private bool _bulletTrigger;

	// Token: 0x040002B3 RID: 691
	private float _bulletBoomDelay;

	// Token: 0x040002B4 RID: 692
	private float _bulletDestoryTimeOut;

	// Token: 0x040002B5 RID: 693
	private int _triggerType;

	// Token: 0x040002B6 RID: 694
	private int _bulletThrough;

	// Token: 0x040002B7 RID: 695
	private float _centerUp;
}

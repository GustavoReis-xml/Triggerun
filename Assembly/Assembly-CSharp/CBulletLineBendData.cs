using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class CBulletLineBendData : CBulletData
{
	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000333 RID: 819 RVA: 0x000177C1 File Offset: 0x000159C1
	public bool isLine
	{
		get
		{
			return this._isLine;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000177CC File Offset: 0x000159CC
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		base.bulletMaxDistance = (float)((int)arrayList[0]);
		this._isLine = ((int)arrayList[1] != 0);
	}

	// Token: 0x040002B8 RID: 696
	private bool _isLine;
}

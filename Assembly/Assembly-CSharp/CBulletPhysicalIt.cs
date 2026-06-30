using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class CBulletPhysicalIt : CBulletIt
{
	// Token: 0x06000353 RID: 851 RVA: 0x000195DC File Offset: 0x000177DC
	public void SetPhysicalDamage(Vector3 __physicalDir, Vector3 __pointNormal, string _pointTag, Vector3 to)
	{
		base.matVector = __physicalDir;
		this.defaultTag = _pointTag;
		base.hitPhysical = -__pointNormal;
		base.hitVelocity = -base.hitPhysical;
		this._hitPoint = to;
		base.end = true;
		base.readyDestory = true;
		this.boom();
		this.destoryBullet();
	}
}

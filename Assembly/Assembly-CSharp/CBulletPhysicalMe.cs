using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class CBulletPhysicalMe : CBulletMe
{
	// Token: 0x06000355 RID: 853 RVA: 0x00019648 File Offset: 0x00017848
	public void SetPhysicalDamage(RaycastHit hit)
	{
		base.hitPhysical = -hit.normal;
		this._hitPoint = hit.point;
		base.hitVelocity = -base.hitPhysical;
		bool flag = hit.collider != null;
		if (flag)
		{
			base.hitCollider = hit.collider;
			this.defaultTag = hit.collider.tag;
		}
		base.end = true;
		base.readyDestory = true;
		this.boom();
		this.destoryBullet();
	}

	// Token: 0x06000356 RID: 854 RVA: 0x000196DC File Offset: 0x000178DC
	public void SetPhysicalDamage(Collision coll)
	{
		base.hitPhysical = -coll.contacts[0].normal;
		this._hitPoint = coll.contacts[0].point;
		base.hitVelocity = -base.hitPhysical;
		base.hitCollider = coll.collider;
		base.end = true;
		base.readyDestory = true;
		this.boom();
		this.destoryBullet();
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class CMuzzleBezierIt : CMuzzleIt
{
	// Token: 0x060005EA RID: 1514 RVA: 0x0002F4F9 File Offset: 0x0002D6F9
	public void FireParm(ArrayList parm)
	{
		this._rate = (int)parm[0];
		this._rot = (int)parm[1];
		this._no = (int)parm[2];
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0002F534 File Offset: 0x0002D734
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0002F55C File Offset: 0x0002D75C
	private void FireReal(Vector3 from, object target, int __bulletGid)
	{
		this.playFireEffect();
		bool flag = base.muzzleTransform == null || this._fireFrom == 1;
		if (flag)
		{
			base.bulletBornPoint = from;
		}
		else
		{
			base.bulletBornPoint = this.muzzlePoint;
		}
		this.playFireEffect();
		Vector3 targetPoint = (Vector3)target;
		Vector3 vector;
		GameObject gameObject = base.CreateBulletObject(targetPoint, out vector);
		base.bullet = gameObject.AddComponent<CBulletBezierIt>();
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		bool flag2 = base.bulletPrefabDelay != null;
		if (flag2)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		((CBulletBezierIt)base.bullet).CreateBezier(this._no, base.bullet.transform.forward, (float)this._rot, this._rate);
	}

	// Token: 0x0400047A RID: 1146
	private int _rate;

	// Token: 0x0400047B RID: 1147
	private int _rot;

	// Token: 0x0400047C RID: 1148
	private int _no;
}

using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class CMuzzleCircleIt : CMuzzleIt
{
	// Token: 0x060005F2 RID: 1522 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
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
		Vector3 targetPoint = (Vector3)target;
		bool flag2 = target.GetType() == typeof(Vector3);
		if (flag2)
		{
			base.targetObject = null;
			targetPoint = (Vector3)target;
		}
		else
		{
			base.targetObject = (GameObject)target;
			targetPoint = base.targetObject.transform.position;
		}
		Vector3 vector;
		GameObject gameObject = base.CreateBulletObject(targetPoint, out vector);
		base.bullet = gameObject.AddComponent<CBulletCircleIt>();
		base.bullet.bulletGID = __bulletGid;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.bulletTargetObject = base.targetObject;
		base.bullet.bulletTargetNet = base.targetNet;
		bool flag3 = base.bulletPrefabDelay != null;
		if (flag3)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
	}
}

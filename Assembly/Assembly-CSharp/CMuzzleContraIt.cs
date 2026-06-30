using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class CMuzzleContraIt : CMuzzleIt
{
	// Token: 0x060005F9 RID: 1529 RVA: 0x0002FF90 File Offset: 0x0002E190
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
	private void FireReal(Vector3 from, object target, int __bulletGid)
	{
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
		base.bullet = gameObject.AddComponent<CBulletContraIt>();
		base.bullet.bulletGID = __bulletGid;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		bool flag2 = base.bulletPrefabDelay != null;
		if (flag2)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		this.CreateBulletChildObject(targetPoint);
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x000300AC File Offset: 0x0002E2AC
	public void CreateBulletChildObject(Vector3 targetPoint)
	{
		int moreNumber = ((CBulletContraData)base.data).MoreNumber;
		for (int i = 0; i < moreNumber; i++)
		{
			Vector3 b = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * (float)((CBulletContraData)base.data).MoreRate / 10000f;
			Vector3 normalized = ((targetPoint - base.bulletBornPoint).normalized + b).normalized;
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(normalized));
			gameObject.transform.parent = fGameManager.allBulletFather.transform;
			CBulletContraIt cbulletContraIt = gameObject.AddComponent<CBulletContraIt>();
			cbulletContraIt.initBullet(this, base.data, true, 0, i + 1);
			bool flag = base.bulletPrefabDelay != null;
			if (flag)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
				gameObject2.transform.parent = gameObject.transform;
				cbulletContraIt.bulletDelayTransform = gameObject2.transform;
			}
		}
	}
}

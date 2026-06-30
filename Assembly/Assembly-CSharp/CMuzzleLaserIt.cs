using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class CMuzzleLaserIt : CMuzzleIt
{
	// Token: 0x06000607 RID: 1543 RVA: 0x00030964 File Offset: 0x0002EB64
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0003098C File Offset: 0x0002EB8C
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
		base.bullet = gameObject.AddComponent<CBulletNormalIt>();
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

	// Token: 0x06000609 RID: 1545 RVA: 0x00030A80 File Offset: 0x0002EC80
	public void CreateBulletChildObject(Vector3 targetPoint)
	{
		int moreNumber = ((CBulletNormalData)base.data).MoreNumber;
		for (int i = 0; i < moreNumber; i++)
		{
			Vector3 b = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * (float)((CBulletNormalData)base.data).MoreRate / 10000f;
			Vector3 normalized = ((targetPoint - base.bulletBornPoint).normalized + b).normalized;
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(normalized));
			gameObject.transform.parent = fGameManager.allBulletFather.transform;
			CBulletNormalIt cbulletNormalIt = gameObject.AddComponent<CBulletNormalIt>();
			cbulletNormalIt.initBullet(this, base.data, true, 0, i + 1);
			bool flag = base.bulletPrefabDelay != null;
			if (flag)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, Quaternion.LookRotation(normalized));
				gameObject2.transform.parent = gameObject.transform;
				cbulletNormalIt.bulletDelayTransform = gameObject2.transform;
			}
		}
	}
}

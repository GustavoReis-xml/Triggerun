using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class CMuzzleTrackIt : CMuzzleIt
{
	// Token: 0x06000653 RID: 1619 RVA: 0x00034D60 File Offset: 0x00032F60
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00034D88 File Offset: 0x00032F88
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
		bool flag2 = target.GetType() == typeof(Vector3);
		Vector3 targetPoint;
		if (flag2)
		{
			base.targetObject = null;
			targetPoint = (Vector3)target;
		}
		else
		{
			base.targetObject = (GameObject)target;
			bool flag3 = base.targetObject == null;
			if (flag3)
			{
				return;
			}
			targetPoint = base.targetObject.transform.position;
		}
		base.bulletBornPoint = this.muzzlePoint;
		Vector3 vector;
		GameObject gameObject = base.CreateBulletObject(targetPoint, out vector);
		base.bullet = gameObject.AddComponent<CBulletTrackIt>();
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		base.bullet.bulletGID = __bulletGid;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.bulletTargetObject = base.targetObject;
		base.bullet.bulletTargetNet = base.targetNet;
		bool flag4 = base.bulletPrefabDelay != null;
		if (flag4)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		bool flag5 = component == null;
		if (flag5)
		{
			Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有刚体{0}", this.launcher.launcherName));
		}
		else
		{
			component.gameObject.layer = base.bullet.physicalLayer;
			Rigidbody[] allRig = new Rigidbody[]
			{
				component
			};
			base.bullet.allRig = allRig;
			Collider component2 = gameObject.GetComponent<Collider>();
			bool flag6 = component2 == null;
			if (flag6)
			{
				Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有碰撞体{0}", this.launcher.launcherName));
			}
			else
			{
				component2.gameObject.layer = base.bullet.physicalLayer;
				bool bulletTrigger = base.data.bulletTrigger;
				if (bulletTrigger)
				{
					component2.isTrigger = true;
				}
				else
				{
					component2.isTrigger = false;
				}
				component.mass = 1f;
				component.drag = 0f;
				component.useGravity = false;
				component.isKinematic = false;
				component.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
		}
	}
}

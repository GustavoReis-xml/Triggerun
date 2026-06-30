using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class CMuzzleCircleMe : CMuzzleMe
{
	// Token: 0x060005F5 RID: 1525 RVA: 0x0002FB5C File Offset: 0x0002DD5C
	public override int Fire(Vector3 targetPoint, object target, int isContinuous)
	{
		base.Fire(targetPoint, target, isContinuous);
		bool flag = base.bulletPrefab == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool beDelay = this.beDelay;
			if (beDelay)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.launcher.needDelay && !this.beDelay;
				if (flag2)
				{
					this.beDelay = true;
					base.StartCoroutine(this.FireSwitch(targetPoint, target, isContinuous));
					((CWeaponControlMe)this.launcher.cwc).UpdateDelayTime(this.launcher.launcherDelayTime);
					result = 1;
				}
				else
				{
					this.FireReal(targetPoint, target, isContinuous);
					result = 1;
				}
			}
		}
		return result;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0002FC01 File Offset: 0x0002DE01
	private IEnumerator FireSwitch(Vector3 targetPoint, object target, int isContinuous)
	{
		yield return new WaitForSeconds(this.launcher.launcherDelayTime);
		bool flag = this.launcher.delayType == 0;
		if (flag)
		{
			this.launcher.needDelay = false;
		}
		this.beDelay = false;
		bool flag2 = !this.GetBreakShoot;
		if (flag2)
		{
			this.FireReal(targetPoint, target, isContinuous);
		}
		yield break;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0002FC28 File Offset: 0x0002DE28
	private void FireReal(Vector3 targetPoint, object target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(targetPoint, target, isContinuous);
		}
		this.playFireEffect();
		base.bulletBornPoint = this.muzzlePoint;
		base.targetObject = null;
		int num = 0;
		string value = string.Empty;
		Collider hitCollider = null;
		bool flag2 = this.launcher.launcherLockTarget == 0;
		if (flag2)
		{
			object obj = base.bulletBornPoint;
			targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 0.1f) : (this.muzzlePoint + Vector3.up * 0.1f));
		}
		else
		{
			bool flag3 = this.launcher.launcherLockTarget == 1;
			if (flag3)
			{
				bool flag4 = target == null;
				if (flag4)
				{
					return;
				}
				value = ((GameObject)target).name;
				bool flag5 = ((CBulletCircleData)base.data).hitDamage == 1;
				if (flag5)
				{
					PublicInfo component = ((GameObject)target).transform.root.gameObject.GetComponent<PublicInfo>();
					NetLiving netObject = component.NetObject;
					bool flag6 = netObject == null || netObject.isDead;
					if (flag6)
					{
						base.targetObject = null;
						return;
					}
					targetPoint = netObject.defaultMesh.transform.position;
					object obj = netObject.defaultMesh;
					bool flag7 = obj != null;
					if (flag7)
					{
						value = ((GameObject)obj).name;
						num = netObject.getNetid();
						base.targetNet = netObject;
						hitCollider = netObject.bodyCollider;
						base.targetObject = (GameObject)obj;
					}
				}
			}
			else
			{
				bool flag8 = this.launcher.launcherLockTarget != 2;
				if (flag8)
				{
					return;
				}
				targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
				object obj = null;
				bool flag9 = obj != null;
				if (flag9)
				{
					value = ((GameObject)obj).name;
				}
			}
		}
		Vector3 target2;
		GameObject gameObject = base.CreateBulletObject(targetPoint, Quaternion.identity, out target2);
		base.bullet = gameObject.AddComponent<CBulletCircleMe>();
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		base.bullet.hitCollider = hitCollider;
		base.bullet.nContinueBullet = isContinuous;
		bool flag10 = base.bulletPrefabDelay != null;
		if (flag10)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(num);
		arrayList.Add(value);
		base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, target2, arrayList, 0, isContinuous);
	}
}

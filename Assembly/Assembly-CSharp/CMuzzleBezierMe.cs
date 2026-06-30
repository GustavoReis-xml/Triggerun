using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class CMuzzleBezierMe : CMuzzleMe
{
	// Token: 0x060005EE RID: 1518 RVA: 0x0002F6A4 File Offset: 0x0002D8A4
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
					this.FireReal(targetPoint, isContinuous);
					result = 1;
				}
			}
		}
		return result;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0002F748 File Offset: 0x0002D948
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
			this.FireReal(targetPoint, isContinuous);
		}
		yield break;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0002F76C File Offset: 0x0002D96C
	private void FireReal(Vector3 target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(target, null, isContinuous);
		}
		this.playFireEffect();
		base.bulletBornPoint = this.muzzlePoint;
		bool flag2 = this.launcher.launcherLockTarget == 0;
		Vector3 targetPoint;
		if (flag2)
		{
			targetPoint = base.getMubiao();
		}
		else
		{
			bool flag3 = this.launcher.launcherLockTarget == 1;
			if (flag3)
			{
				targetPoint = target;
			}
			else
			{
				bool flag4 = this.launcher.launcherLockTarget != 2;
				if (flag4)
				{
					return;
				}
				targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
			}
		}
		Vector3 target2;
		GameObject gameObject = base.CreateBulletObject(targetPoint, Quaternion.identity, out target2);
		base.bullet = gameObject.AddComponent<CBulletBezierMe>();
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		base.bullet.nContinueBullet = isContinuous;
		bool flag5 = base.bulletPrefabDelay != null;
		if (flag5)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		float num = (float)((int)UnityEngine.Random.Range(0f, 360f));
		int bezierRate = ((CBulletBezierData)base.data).bezierRate;
		int num2 = UnityEngine.Random.Range(0, CBulletBezierData.listLength);
		((CBulletBezierMe)base.bullet).CreateBezier(num2, base.bullet.transform.forward, num, bezierRate);
		ArrayList arrayList = new ArrayList();
		arrayList.Add(bezierRate);
		arrayList.Add((int)num);
		arrayList.Add(num2);
		base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, target2, arrayList, 0, isContinuous);
	}
}

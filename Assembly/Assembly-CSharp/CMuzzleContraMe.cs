using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class CMuzzleContraMe : CMuzzleMe
{
	// Token: 0x060005FD RID: 1533 RVA: 0x0003021C File Offset: 0x0002E41C
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

	// Token: 0x060005FE RID: 1534 RVA: 0x000302C0 File Offset: 0x0002E4C0
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

	// Token: 0x060005FF RID: 1535 RVA: 0x000302E4 File Offset: 0x0002E4E4
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
				bool flag4 = this.launcher.launcherLockTarget == 2;
				if (flag4)
				{
					targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
				}
				else
				{
					bool flag5 = this.launcher.launcherLockTarget != 3;
					if (flag5)
					{
						return;
					}
					UnityEngine.Object target2 = this.launcher.cwc.targetSelect.GetTarget();
					targetPoint = ((target2 != null) ? ((Collider)target2).transform.position : ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f)));
				}
			}
		}
		for (int i = 0; i < ((CBulletContraData)base.data).Qus.Length; i++)
		{
			Vector3 vector;
			GameObject gameObject = base.CreateBulletObject(targetPoint, ((CBulletContraData)base.data).Qus[i], out vector);
			base.bullet = gameObject.AddComponent<CBulletContraMe>();
			base.bullet.initBullet(this, base.data, i != 0, i, 0);
			base.bullet.physicsForceBornPoint = base.bulletBornPoint;
			base.bullet.bornPoint = base.bulletBornPoint;
			base.bullet.nContinueBullet = isContinuous;
			bool flag6 = base.bulletPrefabDelay != null;
			if (flag6)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
				gameObject2.transform.parent = gameObject.transform;
				base.bullet.bulletDelayTransform = gameObject2.transform;
			}
			this.CreateBulletChildObject(vector);
			base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, vector, new ArrayList(), i, isContinuous);
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000305A4 File Offset: 0x0002E7A4
	public void CreateBulletChildObject(Vector3 targetPoint)
	{
		int moreNumber = ((CBulletContraData)base.data).MoreNumber;
		for (int i = 0; i < moreNumber; i++)
		{
			Vector3 b = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * (float)((CBulletContraData)base.data).MoreRate / 10000f;
			Vector3 normalized = ((targetPoint - base.bulletBornPoint).normalized + b).normalized;
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(normalized));
			gameObject.transform.parent = fGameManager.allBulletFather.transform;
			CBulletContraMe cbulletContraMe = gameObject.AddComponent<CBulletContraMe>();
			cbulletContraMe.initBullet(this, base.data, true, i, i + 1);
			cbulletContraMe.physicsForceBornPoint = base.bulletBornPoint;
			cbulletContraMe.bornPoint = base.bulletBornPoint;
			bool flag = base.bulletPrefabDelay != null;
			if (flag)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
				gameObject2.transform.parent = gameObject.transform;
				cbulletContraMe.bulletDelayTransform = gameObject2.transform;
			}
		}
	}
}

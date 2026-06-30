using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class CMuzzleLaserMe : CMuzzleMe
{
	// Token: 0x0600060B RID: 1547 RVA: 0x00030BEC File Offset: 0x0002EDEC
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

	// Token: 0x0600060C RID: 1548 RVA: 0x00030C90 File Offset: 0x0002EE90
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

	// Token: 0x0600060D RID: 1549 RVA: 0x00030CB4 File Offset: 0x0002EEB4
	private void FireReal(Vector3 target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(target, target, isContinuous);
		}
		this.playFireEffect();
		base.bulletBornPoint = this.muzzlePoint;
		this.targetDir = Vector3.zero;
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
					bool flag5 = this.launcher.launcherLockTarget == 3;
					if (flag5)
					{
						NetLiving netTarget = ((HeroPlayer)this.launcher.cwc.NetObject).screentarget.NetTarget;
						bool flag6 = netTarget == null || netTarget.isDead;
						if (flag6)
						{
							base.targetObject = null;
							return;
						}
						targetPoint = netTarget.defaultMesh.transform.position;
					}
					else
					{
						bool flag7 = this.launcher.launcherLockTarget != 5;
						if (flag7)
						{
							return;
						}
						NetLiving netTarget2 = ((HeroPlayer)this.launcher.cwc.NetObject).screentarget.NetTarget;
						targetPoint = ((netTarget2 == null || netTarget2.isDead) ? target : netTarget2.defaultMesh.transform.position);
					}
				}
			}
		}
		Vector3 vector;
		GameObject gameObject = base.CreateBulletObject(targetPoint, Quaternion.identity, out vector);
		base.bullet = gameObject.AddComponent<CBulletNormalMe>();
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		base.bullet.nContinueBullet = isContinuous;
		base.bullet.fullTimeShoot = (int)(((CWeaponMe)this.launcher.w).FullRate * 1000f);
		bool flag8 = base.bulletPrefabDelay != null;
		if (flag8)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		this.CreateBulletChildObject(vector);
		base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, vector, new ArrayList(), 0, isContinuous);
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00030F90 File Offset: 0x0002F190
	public void CreateBulletChildObject(Vector3 targetPoint)
	{
		int moreNumber = ((CBulletNormalData)base.data).MoreNumber;
		for (int i = 0; i < moreNumber; i++)
		{
			Vector3 b = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * (float)((CBulletNormalData)base.data).MoreRate / 10000f;
			Vector3 normalized = ((targetPoint - base.bulletBornPoint).normalized + b).normalized;
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(normalized));
			gameObject.transform.parent = fGameManager.allBulletFather.transform;
			CBulletNormalMe cbulletNormalMe = gameObject.AddComponent<CBulletNormalMe>();
			cbulletNormalMe.initBullet(this, base.data, true, 0, i + 1);
			cbulletNormalMe.physicsForceBornPoint = base.bulletBornPoint;
			cbulletNormalMe.bornPoint = base.bulletBornPoint;
			bool flag = base.bulletPrefabDelay != null;
			if (flag)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
				gameObject2.transform.parent = gameObject.transform;
				cbulletNormalMe.bulletDelayTransform = gameObject2.transform;
			}
		}
	}
}

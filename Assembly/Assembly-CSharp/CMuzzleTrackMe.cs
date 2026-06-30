using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class CMuzzleTrackMe : CMuzzleMe
{
	// Token: 0x06000656 RID: 1622 RVA: 0x00035044 File Offset: 0x00033244
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

	// Token: 0x06000657 RID: 1623 RVA: 0x000350E9 File Offset: 0x000332E9
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

	// Token: 0x06000658 RID: 1624 RVA: 0x00035110 File Offset: 0x00033310
	private void FireReal(Vector3 targetPoint, object target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(targetPoint, target, isContinuous);
		}
		this.playFireEffect();
		base.targetObject = null;
		int num = 0;
		string value = string.Empty;
		bool flag2 = target != null;
		if (flag2)
		{
			bool flag3 = this.launcher.launcherLockTarget == 0;
			object obj;
			if (flag3)
			{
				obj = base.getMubiao();
				bool flag4 = obj != null;
				if (flag4)
				{
					value = ((GameObject)obj).name;
				}
			}
			else
			{
				bool flag5 = this.launcher.launcherLockTarget == 1;
				if (flag5)
				{
					obj = target;
					value = ((GameObject)obj).name;
				}
				else
				{
					bool flag6 = this.launcher.launcherLockTarget == 2;
					if (flag6)
					{
						targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
						obj = null;
						bool flag7 = obj != null;
						if (flag7)
						{
							value = ((GameObject)obj).name;
						}
					}
					else
					{
						bool flag8 = this.launcher.launcherLockTarget != 3;
						if (flag8)
						{
							return;
						}
						NetLiving netLiving = (this.launcher.cwc.NetObject.netobject_type == 6) ? ((RobotAI)this.launcher.cwc.NetObject).screentarget.NetTarget : ((HeroPlayer)this.launcher.cwc.NetObject).screentarget.NetTarget;
						bool flag9 = netLiving == null || netLiving.isDead;
						if (flag9)
						{
							base.targetObject = null;
							return;
						}
						targetPoint = netLiving.defaultMesh.transform.position;
						obj = netLiving.defaultMesh;
						bool flag10 = obj != null;
						if (flag10)
						{
							value = ((GameObject)obj).name;
						}
					}
				}
			}
			bool flag11 = obj != null;
			if (flag11)
			{
				PublicInfo component = ((GameObject)obj).transform.root.gameObject.GetComponent<PublicInfo>();
				bool flag12 = component != null;
				if (flag12)
				{
					num = component.NetObject.getNetid();
					base.targetNet = component.NetObject;
					base.targetObject = (GameObject)obj;
				}
			}
		}
		base.bulletBornPoint = this.muzzlePoint;
		Vector3 target2;
		GameObject gameObject = base.CreateBulletObject(targetPoint, Quaternion.identity, out target2);
		base.bullet = gameObject.AddComponent<CBulletTrackMe>();
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.nContinueBullet = isContinuous;
		base.bullet.bulletTargetObject = base.targetObject;
		base.bullet.bulletTargetNet = base.targetNet;
		bool flag13 = base.bulletPrefabDelay != null;
		if (flag13)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		Rigidbody component2 = gameObject.GetComponent<Rigidbody>();
		bool flag14 = component2 == null;
		if (flag14)
		{
			Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有刚体{0}", this.launcher.launcherName));
		}
		else
		{
			component2.gameObject.layer = base.bullet.physicalLayer;
			Rigidbody[] allRig = new Rigidbody[]
			{
				component2
			};
			base.bullet.allRig = allRig;
			Collider component3 = gameObject.GetComponent<Collider>();
			bool flag15 = component3 == null;
			if (flag15)
			{
				Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有碰撞体{0}", this.launcher.launcherName));
			}
			else
			{
				component3.gameObject.layer = base.bullet.physicalLayer;
				bool bulletTrigger = base.data.bulletTrigger;
				if (bulletTrigger)
				{
					component3.isTrigger = true;
				}
				else
				{
					component3.isTrigger = false;
				}
				component2.mass = 1f;
				component2.drag = 0f;
				component2.useGravity = false;
				component2.isKinematic = false;
				component2.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(num);
				arrayList.Add(value);
				base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, target2, arrayList, 0, isContinuous);
			}
		}
	}
}

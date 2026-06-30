using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class CMuzzleThrowMe : CMuzzleMe
{
	// Token: 0x0600064F RID: 1615 RVA: 0x000344D0 File Offset: 0x000326D0
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

	// Token: 0x06000650 RID: 1616 RVA: 0x00034574 File Offset: 0x00032774
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

	// Token: 0x06000651 RID: 1617 RVA: 0x00034598 File Offset: 0x00032798
	private void FireReal(Vector3 target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(target, target, isContinuous);
		}
		this.playFireEffect();
		base.bulletBornPoint = this.muzzlePoint;
		bool flag2 = this.launcher.launcherLockTarget == 0;
		Vector3 vector;
		if (flag2)
		{
			vector = base.getMubiao();
		}
		else
		{
			bool flag3 = this.launcher.launcherLockTarget == 1;
			if (flag3)
			{
				vector = target;
			}
			else
			{
				bool flag4 = this.launcher.launcherLockTarget != 2;
				if (flag4)
				{
					return;
				}
				vector = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 30f) : (this.muzzlePoint + Vector3.up * 30f));
			}
		}
		Vector3 normalized = (vector - base.bulletBornPoint).normalized;
		bool flag5 = ((CBulletThrowData)base.data).parmNumber == 0;
		Quaternion[] array;
		if (flag5)
		{
			array = new Quaternion[1];
		}
		else
		{
			int parmType = ((CBulletThrowData)base.data).parmType;
			int num = parmType;
			if (num != 1)
			{
				if (num != 2)
				{
					array = new Quaternion[1];
				}
				else
				{
					Vector3 point = (!(base.muzzleTransform == null)) ? base.muzzleTransform.right : Vector3.right;
					int angleRate = ((CBulletThrowData)base.data).AngleRate;
					array = new Quaternion[((CBulletThrowData)base.data).parmNumber + 1];
					for (int i = 0; i < ((CBulletThrowData)base.data).parmNumber; i++)
					{
						Vector3 a = base.bulletBornPoint + normalized;
						Quaternion rotation = Quaternion.AngleAxis((float)i / (float)((CBulletThrowData)base.data).parmNumber * 360f, -normalized);
						Vector3 normalized2 = (rotation * point).normalized;
						Vector3 a2 = a + normalized2 * Mathf.Tan(0.017453292f * (float)angleRate);
						Vector3 vector2 = a2 - base.bulletBornPoint;
						array[i + 1] = Quaternion.FromToRotation(normalized, vector2.normalized);
					}
				}
			}
			else
			{
				array = new Quaternion[((CBulletThrowData)base.data).parmNumber];
				int angleRate2 = ((CBulletThrowData)base.data).AngleRate;
				for (int j = 0; j < ((CBulletThrowData)base.data).parmNumber; j++)
				{
					int num2 = j % 2;
					int num3 = j / 2 + num2;
					int num4 = (num2 == 0) ? 1 : -1;
					array[j] = Quaternion.Euler(0f, (float)(angleRate2 * num3 * num4), 0f);
				}
			}
		}
		for (int k = 0; k < array.Length; k++)
		{
			Vector3 vector3;
			GameObject gameObject = base.CreateBulletObject(vector, array[k], out vector3);
			bool flag6 = ((CBulletThrowData)base.data).checkTime > 0f;
			if (flag6)
			{
				base.bullet = gameObject.AddComponent<CBulletThrowStayTimeMe>();
			}
			else
			{
				base.bullet = gameObject.AddComponent<CBulletThrowMe>();
			}
			base.bullet.physicsForceBornPoint = base.bulletBornPoint;
			base.bullet.bornPoint = base.bulletBornPoint;
			base.bullet.initBullet(this, base.data, k != 0, k, 0);
			base.bullet.fullTimeShoot = (int)(((CWeaponMe)this.launcher.w).FullRate * 1000f);
			base.bullet.nContinueBullet = isContinuous;
			bool flag7 = base.bulletPrefabDelay != null;
			if (flag7)
			{
				GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
				gameObject2.transform.parent = gameObject.transform;
				base.bullet.bulletDelayTransform = gameObject2.transform;
			}
			int num5 = CFilmManager.Instance.filmForce(this.launcher.NetObject);
			bool flag8 = this.launcher.NetObject.playerCommon.m_nShiliServer == num5;
			if (flag8)
			{
				bool flag9 = gameObject.name.Contains("_em_");
				if (flag9)
				{
					gameObject.name = gameObject.name.Replace("_em_", string.Empty);
				}
				bool flag10 = !gameObject.name.Contains("_fr_");
				if (flag10)
				{
					CObjectRender component = gameObject.GetComponent<CObjectRender>();
					base.bullet.objectRender = component;
					bool flag11 = component != null;
					if (flag11)
					{
						component.SetEnemyFriendRenderParticle(false, false);
					}
					gameObject.name += "_fr_";
				}
			}
			else
			{
				bool flag12 = gameObject.name.Contains("_fr_");
				if (flag12)
				{
					gameObject.name = gameObject.name.Replace("_fr_", string.Empty);
				}
				bool flag13 = !gameObject.name.Contains("_em_");
				if (flag13)
				{
					CObjectRender component2 = gameObject.GetComponent<CObjectRender>();
					base.bullet.objectRender = component2;
					bool flag14 = component2 != null;
					if (flag14)
					{
						component2.SetEnemyFriendRenderParticle(true, false);
					}
					gameObject.name += "_em_";
				}
			}
			Rigidbody[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody>(true);
			bool flag15 = componentsInChildren.Length == 0;
			if (flag15)
			{
				Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有刚体{0}", this.launcher.launcherName));
				break;
			}
			base.bullet.allRig = componentsInChildren;
			Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>(true);
			bool flag16 = componentsInChildren2.Length == 0;
			if (flag16)
			{
				Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有碰撞体{0}", this.launcher.launcherName));
				break;
			}
			for (int l = 0; l < componentsInChildren2.Length; l++)
			{
				componentsInChildren2[l].gameObject.layer = base.bullet.physicalLayer;
				bool bulletTrigger = base.data.bulletTrigger;
				if (bulletTrigger)
				{
					componentsInChildren2[l].isTrigger = true;
				}
			}
			Vector3 a3 = (vector3 - base.bulletBornPoint).normalized * ((CBulletThrowData)base.data).bulletphysicsForce;
			for (int m = 0; m < componentsInChildren.Length; m++)
			{
				componentsInChildren[m].mass = ((CBulletThrowData)base.data).bulletMass;
				componentsInChildren[m].drag = ((CBulletThrowData)base.data).bulletDrag;
				componentsInChildren[m].useGravity = ((CBulletThrowData)base.data).bulletGravity;
				componentsInChildren[m].isKinematic = false;
				componentsInChildren[m].AddForce(a3 * this.launcher.gemSpd, ForceMode.Impulse);
				componentsInChildren[m].collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				componentsInChildren[m].gameObject.layer = base.bullet.physicalLayer;
			}
			base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, vector3, new ArrayList(), k, isContinuous);
		}
	}
}

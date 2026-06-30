using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class CMuzzleLineBendMe : CMuzzleMe
{
	// Token: 0x06000619 RID: 1561 RVA: 0x000316A3 File Offset: 0x0002F8A3
	public override void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		base.initMuzzle(cwc, point, animatorNo, animators, mainParam, pointFps, pointTps);
		this.fireEffectLongTime = 0.4f;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x000316C4 File Offset: 0x0002F8C4
	private void InitLinePosition()
	{
		bool flag = !(base.muzzleTargetLine == null) && base.muzzleTargetLine.gameObject.activeInHierarchy;
		if (flag)
		{
			base.lineRender.numPositions = 50;
			int num = 0;
			while ((float)num < 50f)
			{
				Vector3 a = Vector3.Lerp(this.muzzlePoint, this.computV3, (float)num / 50f);
				Vector3 b = Vector3.Lerp(this.computV3, base.targetObject.transform.position, (float)num / 50f);
				Vector3 position = Vector3.Lerp(a, b, (float)num / 50f);
				base.lineRender.SetPosition(num, position);
				num++;
			}
			bool flag2 = base.muzzleTargetLight != null;
			if (flag2)
			{
				base.muzzleTargetLight.position = base.targetObject.transform.position;
			}
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x000317B4 File Offset: 0x0002F9B4
	public override void playFireEffect()
	{
		base.playFireEffect();
		bool flag = base.fireEffect != null;
		if (flag)
		{
			base.fireEffect.transform.eulerAngles += new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
			bool flag2 = base.muzzleTargetLine != null && base.targetObject != null && !base.muzzleTargetLine.gameObject.activeSelf;
			if (flag2)
			{
				base.muzzleTargetLine.gameObject.SetActive(true);
			}
			bool flag3 = base.muzzleTargetLight != null && base.targetObject != null && !base.muzzleTargetLight.gameObject.activeSelf;
			if (flag3)
			{
				base.muzzleTargetLight.gameObject.SetActive(true);
			}
		}
		base.FireSound();
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x000318B4 File Offset: 0x0002FAB4
	private void stopShowLine()
	{
		bool flag = base.muzzleTargetLight != null && base.muzzleTargetLight.gameObject.activeSelf;
		if (flag)
		{
			base.muzzleTargetLight.gameObject.SetActive(false);
		}
		bool flag2 = base.muzzleTargetLine != null && base.muzzleTargetLine.gameObject.activeSelf;
		if (flag2)
		{
			base.muzzleTargetLine.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00031934 File Offset: 0x0002FB34
	public override void stopFireEffect()
	{
		bool flag = base.fireEffect != null;
		if (flag)
		{
			bool activeSelf = base.fireEffect.activeSelf;
			if (activeSelf)
			{
				base.fireEffect.SetActive(false);
			}
			bool flag2 = base.muzzleTargetLight != null && base.muzzleTargetLight.gameObject.activeSelf;
			if (flag2)
			{
				base.muzzleTargetLight.gameObject.SetActive(false);
			}
			bool flag3 = base.muzzleTargetLine != null && base.muzzleTargetLine.gameObject.activeSelf;
			if (flag3)
			{
				base.muzzleTargetLine.gameObject.SetActive(false);
			}
		}
		base.stopFireEffect();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x000319F0 File Offset: 0x0002FBF0
	public override void checkFireEffect()
	{
		bool flag = !base.fireShowStat;
		if (!flag)
		{
			bool flag2 = Time.time > base.fireEffectTimeOut;
			if (flag2)
			{
				base.targetObject = null;
				this.stopFireEffect();
			}
			bool flag3 = this.launcher.launcherLockTarget == 3;
			if (flag3)
			{
				bool flag4 = !((HeroPlayer)this.launcher.cwc.NetObject).screentarget.lockTarget;
				if (flag4)
				{
					base.LineDisable();
				}
			}
			else
			{
				bool flag5 = this.launcher.launcherLockTarget == 1 && base.targetObject == null;
				if (flag5)
				{
					base.LineDisable();
				}
			}
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00031AA4 File Offset: 0x0002FCA4
	protected override void Update()
	{
		base.Update();
		bool flag = !(base.targetObject == null);
		if (flag)
		{
			Vector3 normalized = (base.targetObject.transform.position - this.muzzlePoint).normalized;
			Vector3 vector = normalized;
			bool flag2 = !((CBulletLineBendData)base.data).isLine && base.muzzleTransform != null;
			if (flag2)
			{
				vector = base.muzzleTransform.forward;
			}
			float num = Vector3.Angle(vector, normalized);
			float num2 = Vector3.Distance(this.muzzlePoint, base.targetObject.transform.position);
			float d = num2 * (1f - num / 180f) * 0.3f;
			this.computV3 = this.muzzlePoint + vector * d;
			this.InitLinePosition();
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00031B8C File Offset: 0x0002FD8C
	public override int Fire(Vector3 targetPoint, object target, int isContinuous)
	{
		base.Fire(targetPoint, target, isContinuous);
		bool beDelay = this.beDelay;
		int result;
		if (beDelay)
		{
			result = 0;
		}
		else
		{
			bool flag = this.launcher.needDelay && !this.beDelay;
			if (flag)
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
		return result;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00031C1C File Offset: 0x0002FE1C
	private IEnumerator FireSwitch(Vector3 targetPoint, object target, int isContinuous)
	{
		yield return new WaitForSeconds(this.launcher.launcherDelayTime);
		bool flag = this.launcher.delayType == 0;
		if (flag)
		{
			this.launcher.needDelay = false;
		}
		this.beDelay = false;
		bool flag2 = !this.GetBreakShoot && !this.launcher.NetObject.isDead;
		if (flag2)
		{
			this.FireReal(targetPoint, target, isContinuous);
		}
		yield break;
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00031C40 File Offset: 0x0002FE40
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
		Collider hitCollider = null;
		bool flag2 = this.launcher.launcherLockTarget == 0;
		if (flag2)
		{
			object obj = base.getMubiao();
			bool flag3 = obj != null;
			if (flag3)
			{
				value = ((GameObject)obj).name;
			}
		}
		else
		{
			bool flag4 = this.launcher.launcherLockTarget == 1;
			if (flag4)
			{
				value = ((GameObject)target).name;
				PublicInfo component = ((GameObject)target).transform.root.gameObject.GetComponent<PublicInfo>();
				NetLiving netObject = component.NetObject;
				bool flag5 = netObject == null || netObject.isDead;
				if (flag5)
				{
					base.targetObject = null;
					this.stopShowLine();
					return;
				}
				float num2 = Vector3.Distance(this.launcher.cwc.NetObject.defaultMesh.transform.position, netObject.defaultMesh.transform.position);
				bool flag6 = num2 > base.data.bulletMaxDistance * this.launcher.gemDis;
				if (flag6)
				{
					base.targetObject = null;
					this.stopShowLine();
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
					bool flag8 = base.muzzleTargetLine != null;
					if (flag8)
					{
						targetPoint = base.targetObject.transform.position;
						base.LineShow();
					}
				}
			}
			else
			{
				bool flag9 = this.launcher.launcherLockTarget == 2;
				if (flag9)
				{
					targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
					object obj = null;
					bool flag10 = obj != null;
					if (flag10)
					{
						value = ((GameObject)obj).name;
					}
				}
				else
				{
					bool flag11 = this.launcher.launcherLockTarget == 3;
					if (flag11)
					{
						NetLiving netTarget = ((HeroPlayer)this.launcher.cwc.NetObject).screentarget.NetTarget;
						bool flag12 = netTarget == null || netTarget.isDead || netTarget.haveDestroy;
						if (flag12)
						{
							base.targetObject = null;
							this.stopShowLine();
							return;
						}
						float num3 = Vector3.Distance(this.launcher.cwc.NetObject.defaultMesh.transform.position, netTarget.defaultMesh.transform.position);
						bool flag13 = num3 > base.data.bulletMaxDistance * this.launcher.gemDis;
						if (flag13)
						{
							base.targetObject = null;
							this.stopShowLine();
							return;
						}
						targetPoint = netTarget.defaultMesh.transform.position;
						object obj = netTarget.defaultMesh;
						((HeroPlayer)this.launcher.cwc.NetObject).screentarget.lockTarget = true;
						bool flag14 = obj != null;
						if (flag14)
						{
							value = ((GameObject)obj).name;
							num = netTarget.getNetid();
							base.targetNet = netTarget;
							hitCollider = netTarget.bodyCollider;
							base.targetObject = (GameObject)obj;
							bool flag15 = base.muzzleTargetLine != null;
							if (flag15)
							{
								targetPoint = base.targetObject.transform.position;
								base.LineShow();
							}
						}
					}
					else
					{
						bool flag16 = this.launcher.launcherLockTarget != 4;
						if (flag16)
						{
							return;
						}
						object obj = null;
						targetPoint = ((!(base.muzzleTransform == null)) ? (this.muzzlePoint + base.muzzleTransform.forward * 1000f) : (this.muzzlePoint + Vector3.up * 1000f));
						base.TriggerShow();
						bool flag17 = obj != null;
						if (flag17)
						{
							value = ((GameObject)obj).name;
						}
						bool flag18 = obj != null;
						if (flag18)
						{
							PublicInfo component2 = ((GameObject)obj).transform.root.gameObject.GetComponent<PublicInfo>();
							bool flag19 = component2 != null;
							if (flag19)
							{
								num = component2.NetObject.getNetid();
								base.targetNet = component2.NetObject;
								base.targetObject = (GameObject)obj;
							}
						}
					}
				}
			}
		}
		base.bulletBornPoint = new Vector3(targetPoint.x, targetPoint.y + 0.01f, targetPoint.z);
		Vector3 target2;
		GameObject gameObject = base.CreateBulletObject(targetPoint, Quaternion.identity, out target2);
		base.bullet = gameObject.AddComponent<CBulletLineBendMe>();
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = this.muzzlePoint;
		base.bullet.hitCollider = hitCollider;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.nContinueBullet = isContinuous;
		bool flag20 = base.bulletPrefabDelay != null;
		if (flag20)
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

	// Token: 0x0400047F RID: 1151
	private Vector3 computV3;
}

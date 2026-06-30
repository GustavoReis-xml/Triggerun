using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class CMuzzlePhysicalMe : CMuzzleMe
{
	// Token: 0x0600063C RID: 1596 RVA: 0x00033334 File Offset: 0x00031534
	public override void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		base.initMuzzle(cwc, point, animatorNo, animators, mainParam, pointFps, pointTps);
		this._blockLayer = LayerManager.getBlockLayer(this.launcher.NetObject.nForce);
		Rigidbody rigidbody = point.GetComponent<Rigidbody>();
		bool flag = rigidbody == null;
		if (flag)
		{
			rigidbody = point.gameObject.AddComponent<Rigidbody>();
		}
		rigidbody.isKinematic = true;
		rigidbody.mass = 0.01f;
		bool flag2 = this.launcher.NetObject.netobject_type != 6;
		if (flag2)
		{
			Collider[] componentsInChildren = point.GetComponentsInChildren<Collider>();
			bool flag3 = componentsInChildren.Length == 0;
			if (flag3)
			{
				Debug.LogError(string.Format("策划注意:这个物理武器({0})枪口根节点没有碰撞体", this.launcher.launcherName));
			}
		}
		this.physicalTrigger = point.gameObject.AddComponent<CMuzzlePhysicalTrigger>();
		rigidbody.transform.gameObject.layer = LayerManager.getBulletFightLayer(this.launcher.layerType, this.launcher.NetObject.nForce);
		this.physicalTrigger.InitTrigger(this.launcher.NetObject, this);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00033450 File Offset: 0x00031650
	public override void ReSetphysicalDir()
	{
		base.ReSetphysicalDir();
		RaycastHit physicalDamage;
		bool flag = Physics.Raycast(CameraControl.Instance.cursorRay, out physicalDamage, ((CBulletPhysicalData)base.data).weaponRadius, LayerManager.getMiaoZhunLayer(0));
		if (flag)
		{
			bool flag2 = !physicalDamage.collider.tag.Contains("can_damage");
			if (flag2)
			{
				base.bulletBornPoint = this.launcher.NetObject.userHeadTransform.position;
				Vector3 vector;
				GameObject gameObject = base.CreateBulletObject(physicalDamage.point, Quaternion.identity, out vector);
				base.bullet = gameObject.AddComponent<CBulletPhysicalMe>();
				base.bullet.initBullet(this, base.data, false, 0, 0);
				base.bullet.physicsForceBornPoint = base.bulletBornPoint;
				base.bullet.bornPoint = base.bulletBornPoint;
				base.bullet.matVector = this.physicalDir;
				bool flag3 = base.bulletPrefabDelay != null;
				if (flag3)
				{
					GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
					gameObject2.transform.parent = gameObject.transform;
					base.bullet.bulletDelayTransform = gameObject2.transform;
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add((int)(this.physicalDir.x * 100f));
				arrayList.Add((int)(this.physicalDir.y * 100f));
				arrayList.Add((int)(this.physicalDir.z * 100f));
				arrayList.Add((int)(physicalDamage.normal.x * 100f));
				arrayList.Add((int)(physicalDamage.normal.y * 100f));
				arrayList.Add((int)(physicalDamage.normal.z * 100f));
				arrayList.Add(physicalDamage.transform.tag);
				base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, physicalDamage.point, arrayList, 0, this.beginCount);
				this.beginCount++;
				((CBulletPhysicalMe)base.bullet).SetPhysicalDamage(physicalDamage);
			}
		}
		else
		{
			Vector3 vector;
			GameObject gameObject3 = base.CreateBulletObject(base.bulletBornPoint, Quaternion.identity, out vector);
			base.bullet = gameObject3.AddComponent<CBulletPhysicalMe>();
			base.bullet.initBullet(this, base.data, false, 0, 0);
			base.bullet.physicsForceBornPoint = base.bulletBornPoint;
			base.bullet.bornPoint = base.bulletBornPoint;
			bool flag4 = base.muzzleTransform == null;
			if (flag4)
			{
				base.bullet.matVector = Vector3.forward;
			}
			else
			{
				base.bullet.matVector = base.muzzleTransform.forward;
			}
			bool flag5 = base.bulletPrefabDelay != null;
			if (flag5)
			{
				GameObject gameObject4 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject3.transform.rotation);
				gameObject4.transform.parent = gameObject3.transform;
				base.bullet.bulletDelayTransform = gameObject4.transform;
			}
			base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, Vector3.zero, new ArrayList(), 0, this.beginCount);
			this.beginCount++;
			((CBulletPhysicalMe)base.bullet).SetPhysicalDamage(physicalDamage);
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0003380C File Offset: 0x00031A0C
	protected override void Update()
	{
		base.Update();
		bool flag = this.beDelayDisablePhysical && Time.time > this.disablePhysicalTime;
		if (flag)
		{
			this.beDelayDisablePhysical = false;
			this.SetPhysicalEnd();
		}
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00033850 File Offset: 0x00031A50
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
				base.bulletBornPoint = this.muzzlePoint;
				this.FireReal(targetPoint, isContinuous);
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x000338EC File Offset: 0x00031AEC
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
			this.FireReal(targetPoint, isContinuous);
		}
		yield break;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00033910 File Offset: 0x00031B10
	private void FireReal(Vector3 target, int isContinuous)
	{
		bool flag = this.launcher.delayWork == 0;
		if (flag)
		{
			((CLauncherMe)this.launcher).FireRealDoWork(target, target, isContinuous);
		}
		this.playFireEffect();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00033954 File Offset: 0x00031B54
	public override void OnHitByPhysical(Collision coll)
	{
		Vector3 point = coll.contacts[0].point;
		base.bulletBornPoint = this.muzzlePoint;
		int num = -1;
		int num2 = -1;
		GameObject gameObject = coll.collider.gameObject;
		bool flag = gameObject != null && gameObject.tag.Contains("can_damage");
		if (flag)
		{
			bool bulletBoomBlock = base.data.bulletBoomBlock;
			if (bulletBoomBlock)
			{
				Vector3 userCenter = this.launcher.NetObject.userCenter;
				Vector3 normalized = (coll.collider.bounds.center - userCenter).normalized;
				float maxDistance = Vector3.Distance(userCenter, coll.collider.bounds.center);
				RaycastHit[] array = Physics.RaycastAll(userCenter, normalized, maxDistance, this._blockLayer);
				bool flag2 = false;
				foreach (RaycastHit raycastHit in array)
				{
					bool flag3 = !raycastHit.collider.transform.tag.Contains("can_damage");
					if (flag3)
					{
						flag2 = true;
						break;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
					return;
				}
			}
			PublicInfo component = gameObject.transform.root.gameObject.GetComponent<PublicInfo>();
			bool flag5 = component != null;
			if (flag5)
			{
				num = component.NetObject.getNetid();
				base.targetNet = component.NetObject;
				base.targetObject = gameObject;
				bool flag6 = num == this.launcher.NetObject.getNetid();
				if (flag6)
				{
					return;
				}
			}
		}
		bool flag7 = this.launcher.launcherLockTarget != 0;
		if (!flag7)
		{
			int layerType = this.launcher.layerType;
			int num3 = layerType;
			if (num3 != 1)
			{
				if (num3 != 2)
				{
					bool flag8 = gameObject != null;
					if (flag8)
					{
						num2 = num;
					}
				}
				else
				{
					bool flag9 = num != -1 && base.targetNet.nForce == this.launcher.NetObject.nForce;
					if (flag9)
					{
						num2 = num;
					}
				}
			}
			else
			{
				bool flag10 = num != -1 && base.targetNet.nForce != this.launcher.NetObject.nForce;
				if (flag10)
				{
					num2 = num;
				}
			}
			bool flag11 = num2 != -1 && !this.damageThis.Contains(num2);
			if (flag11)
			{
				this.damageThis.Add(num2);
				Vector3 vector;
				GameObject gameObject2 = base.CreateBulletObject(point, Quaternion.identity, out vector);
				base.bullet = gameObject2.AddComponent<CBulletPhysicalMe>();
				base.bullet.initBullet(this, base.data, false, 0, 0);
				base.bullet.physicsForceBornPoint = base.bulletBornPoint;
				base.bullet.bornPoint = base.bulletBornPoint;
				bool flag12 = base.bulletPrefabDelay != null;
				if (flag12)
				{
					GameObject gameObject3 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject2.transform.rotation);
					gameObject3.transform.parent = gameObject2.transform;
					base.bullet.bulletDelayTransform = gameObject3.transform;
				}
				base.bullet.matVector = this.physicalDir;
				ArrayList arrayList = new ArrayList();
				arrayList.Add((int)(this.physicalDir.x * 100f));
				arrayList.Add((int)(this.physicalDir.y * 100f));
				arrayList.Add((int)(this.physicalDir.z * 100f));
				arrayList.Add((int)(coll.contacts[0].normal.x * 100f));
				arrayList.Add((int)(coll.contacts[0].normal.y * 100f));
				arrayList.Add((int)(coll.contacts[0].normal.z * 100f));
				arrayList.Add(coll.collider.transform.tag);
				base.SendSB(base.bullet.bulletGID, base.bulletBornPoint, coll.contacts[0].point, arrayList, 0, this.beginCount);
				this.beginCount++;
				((CBulletPhysicalMe)base.bullet).SetPhysicalDamage(coll);
			}
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00033DF0 File Offset: 0x00031FF0
	public override void SetPhysicalBegin()
	{
		this.beginCount = 0;
		this.damageThis = new List<int>();
		this.physicalTrigger.triggerEnable = true;
		base.SetPhysicalBegin();
		this.beDelayDisablePhysical = true;
		this.disablePhysicalTime = Time.time + 1f;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00033E3C File Offset: 0x0003203C
	public override void SetPhysicalEnd()
	{
		this.physicalTrigger.triggerEnable = false;
		base.SetPhysicalEnd();
	}

	// Token: 0x04000487 RID: 1159
	private bool beDelayDisablePhysical;

	// Token: 0x04000488 RID: 1160
	private float disablePhysicalTime;

	// Token: 0x04000489 RID: 1161
	private List<int> damageThis;

	// Token: 0x0400048A RID: 1162
	private int _blockLayer;

	// Token: 0x0400048B RID: 1163
	private int beginCount;
}

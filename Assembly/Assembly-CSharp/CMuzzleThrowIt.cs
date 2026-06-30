using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class CMuzzleThrowIt : CMuzzleIt
{
	// Token: 0x0600064C RID: 1612 RVA: 0x00034010 File Offset: 0x00032210
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00034038 File Offset: 0x00032238
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
		Vector3 vector = (Vector3)target;
		Vector3 vector2;
		GameObject gameObject = base.CreateBulletObject(vector, out vector2);
		bool flag2 = ((CBulletThrowData)base.data).checkTime > 0f;
		if (flag2)
		{
			base.bullet = gameObject.AddComponent<CBulletThrowStayTimeIt>();
		}
		else
		{
			base.bullet = gameObject.AddComponent<CBulletThrowIt>();
		}
		base.bullet.bulletGID = __bulletGid;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		bool flag3 = !this.dictBullets.ContainsKey(base.bullet.bulletGID);
		if (flag3)
		{
			this.dictBullets.Add(base.bullet.bulletGID, (CBulletIt)base.bullet);
		}
		bool flag4 = base.bulletPrefabDelay != null;
		if (flag4)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		int num = CFilmManager.Instance.filmForce(this.launcher.NetObject);
		bool flag5 = this.launcher.NetObject.playerCommon.m_nShiliServer == num;
		if (flag5)
		{
			bool flag6 = gameObject.name.Contains("_em_");
			if (flag6)
			{
				gameObject.name = gameObject.name.Replace("_em_", string.Empty);
			}
			bool flag7 = !gameObject.name.Contains("_fr_");
			if (flag7)
			{
				CObjectRender component = gameObject.GetComponent<CObjectRender>();
				base.bullet.objectRender = component;
				bool flag8 = component != null;
				if (flag8)
				{
					component.SetEnemyFriendRenderParticle(false, false);
				}
				gameObject.name += "_fr_";
			}
		}
		else
		{
			bool flag9 = gameObject.name.Contains("_fr_");
			if (flag9)
			{
				gameObject.name = gameObject.name.Replace("_fr_", string.Empty);
			}
			bool flag10 = !gameObject.name.Contains("_em_");
			if (flag10)
			{
				CObjectRender component2 = gameObject.GetComponent<CObjectRender>();
				base.bullet.objectRender = component2;
				bool flag11 = component2 != null;
				if (flag11)
				{
					component2.SetEnemyFriendRenderParticle(true, false);
				}
				gameObject.name += "_em_";
			}
		}
		Rigidbody[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody>();
		bool flag12 = componentsInChildren.Length == 0;
		if (flag12)
		{
			Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有刚体{0}", this.launcher.launcherName));
		}
		else
		{
			base.bullet.allRig = componentsInChildren;
			Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>();
			bool flag13 = componentsInChildren2.Length == 0;
			if (flag13)
			{
				Debug.LogError(string.Format("策划注意:这个发射器发射的子弹根节点没有碰撞体{0}", this.launcher.launcherName));
			}
			else
			{
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].gameObject.layer = base.bullet.physicalLayer;
					bool bulletTrigger = base.data.bulletTrigger;
					if (bulletTrigger)
					{
						componentsInChildren2[i].isTrigger = true;
					}
				}
				Vector3 a = (vector - base.bulletBornPoint).normalized * ((CBulletThrowData)base.data).bulletphysicsForce;
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].mass = ((CBulletThrowData)base.data).bulletMass;
					componentsInChildren[j].drag = ((CBulletThrowData)base.data).bulletDrag;
					componentsInChildren[j].useGravity = ((CBulletThrowData)base.data).bulletGravity;
					componentsInChildren[j].isKinematic = false;
					componentsInChildren[j].AddForce(a * this.launcher.gemSpd, ForceMode.Impulse);
					componentsInChildren[j].collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
					componentsInChildren[j].gameObject.layer = base.bullet.physicalLayer;
				}
			}
		}
	}
}

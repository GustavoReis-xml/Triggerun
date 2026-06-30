using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class CMuzzlePhysicalIt : CMuzzleIt
{
	// Token: 0x06000637 RID: 1591 RVA: 0x000330BC File Offset: 0x000312BC
	public override void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		base.initMuzzle(cwc, point, animatorNo, animators, mainParam, pointFps, pointTps);
		Rigidbody rigidbody = point.GetComponent<Rigidbody>();
		bool flag = rigidbody == null;
		if (flag)
		{
			rigidbody = point.gameObject.AddComponent<Rigidbody>();
		}
		rigidbody.isKinematic = true;
		rigidbody.mass = 0.01f;
		Collider[] componentsInChildren = point.GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00033138 File Offset: 0x00031338
	public void FireParm(ArrayList parm)
	{
		bool flag = parm.Count == 0;
		if (flag)
		{
			this._pointTag = string.Empty;
		}
		else
		{
			this.physicalDir = new Vector3((float)((int)parm[0]) * 0.01f, (float)((int)parm[1]) * 0.01f, (float)((int)parm[2]) * 0.01f);
			this._pointNormal = -new Vector3((float)((int)parm[3]) * 0.01f, (float)((int)parm[4]) * 0.01f, (float)((int)parm[5]) * 0.01f);
			this._pointTag = (string)parm[6];
		}
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00033204 File Offset: 0x00031404
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0003322C File Offset: 0x0003142C
	private void FireReal(Vector3 from, object target, int __bulletGid)
	{
		base.bulletBornPoint = from;
		this.playFireEffect();
		Vector3 vector = (Vector3)target;
		Vector3 vector2;
		GameObject gameObject = base.CreateBulletObject(vector, out vector2);
		base.bullet = gameObject.AddComponent<CBulletPhysicalIt>();
		base.bullet.initBullet(this, base.data, false, 0, 0);
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = base.bulletBornPoint;
		bool flag = base.bulletPrefabDelay != null;
		if (flag)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
		((CBulletPhysicalIt)base.bullet).SetPhysicalDamage(this.physicalDir, this._pointNormal, this._pointTag, vector);
	}

	// Token: 0x04000485 RID: 1157
	private Vector3 _pointNormal;

	// Token: 0x04000486 RID: 1158
	private string _pointTag = string.Empty;
}

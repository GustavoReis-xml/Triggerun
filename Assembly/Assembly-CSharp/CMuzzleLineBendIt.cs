using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class CMuzzleLineBendIt : CMuzzleIt
{
	// Token: 0x06000610 RID: 1552 RVA: 0x0003111B File Offset: 0x0002F31B
	public override void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		base.initMuzzle(cwc, point, animatorNo, animators, mainParam, pointFps, pointTps);
		this.fireEffectLongTime = 0.4f;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0003113C File Offset: 0x0002F33C
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		base.Fire(from, target, __bulletGid);
		this.FireReal(from, target, __bulletGid);
		return 1;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00031164 File Offset: 0x0002F364
	private void FireReal(Vector3 from, object target, int __bulletGid)
	{
		this.playFireEffect();
		bool flag = base.muzzleTransform == null;
		if (flag)
		{
			base.bulletBornPoint = from;
		}
		else
		{
			base.bulletBornPoint = this.muzzlePoint;
		}
		bool flag2 = target.GetType() == typeof(Vector3);
		Vector3 vector;
		if (flag2)
		{
			base.targetObject = null;
			vector = (Vector3)target;
			base.TriggerShow();
			base.LineShow();
		}
		else
		{
			base.targetObject = (GameObject)target;
			vector = base.targetObject.transform.position;
			base.LineShow();
		}
		base.bulletBornPoint = new Vector3(vector.x, vector.y + 0.01f, vector.z);
		Vector3 vector2;
		GameObject gameObject = base.CreateBulletObject(vector, out vector2);
		base.bullet = gameObject.AddComponent<CBulletLineBendIt>();
		base.bullet.physicsForceBornPoint = base.bulletBornPoint;
		base.bullet.bornPoint = this.muzzlePoint;
		base.bullet.bulletGID = __bulletGid;
		base.bullet.initBullet(this, base.data, false, 0, 0);
		bool flag3 = base.bulletPrefabDelay != null;
		if (flag3)
		{
			GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(base.bulletPrefabDelay, base.bulletBornPoint, gameObject.transform.rotation);
			gameObject2.transform.parent = gameObject.transform;
			base.bullet.bulletDelayTransform = gameObject2.transform;
		}
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x000312E4 File Offset: 0x0002F4E4
	public override void checkFireEffect()
	{
		bool flag = base.fireShowStat && Time.time > base.fireEffectTimeOut;
		if (flag)
		{
			base.targetObject = null;
			this.stopFireEffect();
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00031320 File Offset: 0x0002F520
	protected override void Update()
	{
		base.Update();
		bool flag = !(base.targetObject == null) && !(base.muzzleTargetLine == null) && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			Vector3 normalized = (base.targetObject.transform.position - this.muzzlePoint).normalized;
			Vector3 vector = normalized;
			bool flag2 = !((CBulletLineBendData)base.data).isLine;
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

	// Token: 0x06000615 RID: 1557 RVA: 0x0003141C File Offset: 0x0002F61C
	private void InitLinePosition()
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
		bool flag = base.muzzleTargetLight != null;
		if (flag)
		{
			base.muzzleTargetLight.position = base.targetObject.transform.position;
		}
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000314E0 File Offset: 0x0002F6E0
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

	// Token: 0x06000617 RID: 1559 RVA: 0x000315E0 File Offset: 0x0002F7E0
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
			bool flag2 = base.muzzleTargetLine != null && base.muzzleTargetLine.gameObject.activeSelf;
			if (flag2)
			{
				base.muzzleTargetLine.gameObject.SetActive(false);
			}
			bool flag3 = base.muzzleTargetLight != null && base.muzzleTargetLight.gameObject.activeSelf;
			if (flag3)
			{
				base.muzzleTargetLight.gameObject.SetActive(false);
			}
		}
		base.stopFireEffect();
	}

	// Token: 0x0400047E RID: 1150
	private Vector3 computV3;
}

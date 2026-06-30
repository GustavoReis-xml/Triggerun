using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class CMuzzleMe : CMuzzle
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000624 RID: 1572 RVA: 0x0003226C File Offset: 0x0003046C
	protected virtual bool GetBreakShoot
	{
		get
		{
			bool flag = this.isBreakShoot;
			bool result;
			if (flag)
			{
				this.isBreakShoot = false;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00032298 File Offset: 0x00030498
	protected Vector3 getMubiao()
	{
		return ((CLauncherMe)this.launcher).getMubiao();
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x000322BC File Offset: 0x000304BC
	protected Vector3 getMubiaoUp()
	{
		return ((CLauncherMe)this.launcher).getMubiaoUp();
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x000322E0 File Offset: 0x000304E0
	public Vector3 getCameraDir()
	{
		return ((CLauncherMe)this.launcher).getCameraDir();
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00032304 File Offset: 0x00030504
	public override int Fire(Vector3 targetPoint, object target, int isContinuous)
	{
		this.isBreakShoot = false;
		bool flag = this.launcher.delayType == 1;
		if (flag)
		{
			this.launcher.needDelay = true;
		}
		return base.Fire(targetPoint, target, isContinuous);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00032347 File Offset: 0x00030547
	public void SetBreakShoot()
	{
		this.isBreakShoot = true;
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00032351 File Offset: 0x00030551
	public override void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		base.initMuzzle(cwc, point, animatorNo, animators, mainParam, pointFps, pointTps);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00032368 File Offset: 0x00030568
	public GameObject CreateBulletObject(Vector3 targetPoint, Quaternion quate, out Vector3 tempFirePoint)
	{
		Vector3 b = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * this.launcher.cwc.hitRateNow / 10000f;
		bool flag = targetPoint == base.bulletBornPoint;
		if (flag)
		{
			targetPoint += new Vector3(0f, 0.01f, 0f);
		}
		Vector3 vector = (targetPoint - base.bulletBornPoint).normalized;
		bool flag2 = this.launcher.launcherLockTarget == 0 && this.launcher.weaponData.bullet_type != 2;
		if (flag2)
		{
			this.targetDir = this.getCameraDir();
			float num = Vector3.Dot(this.targetDir, vector);
			bool flag3 = num > 0f;
			if (flag3)
			{
				vector = (quate * vector + b).normalized;
			}
			else
			{
				vector = this.targetDir;
				base.bulletBornPoint = targetPoint - this.targetDir * 1f;
			}
		}
		else
		{
			vector = (vector + b).normalized;
		}
		bool flag4 = base.data.centerUp > 0f;
		if (flag4)
		{
			vector = Vector3.Lerp(vector, Vector3.up, base.data.centerUp);
		}
		float d = 400f;
		tempFirePoint = base.bulletBornPoint + vector * d;
		CLauncherMe clauncherMe = (CLauncherMe)this.launcher;
		bool flag5 = clauncherMe.hitrateType == 0;
		if (flag5)
		{
			bool flag6 = clauncherMe.cwc.hitRateNow + (float)clauncherMe.hitrateAdd > (float)clauncherMe.hitrateMax;
			if (flag6)
			{
				clauncherMe.cwc.hitRateNow = (float)clauncherMe.hitrateMax;
			}
			else
			{
				clauncherMe.cwc.hitRateNow += (float)clauncherMe.hitrateAdd;
			}
		}
		else
		{
			clauncherMe.cwc.hitRateNow += Mathf.Pow(clauncherMe.cwc.hitRateNow, 2f) / 100f + (float)clauncherMe.hitrateAdd;
			bool flag7 = clauncherMe.cwc.hitRateNow > (float)clauncherMe.hitrateMax;
			if (flag7)
			{
				clauncherMe.cwc.hitRateNow = (float)clauncherMe.hitrateMax;
			}
		}
		clauncherMe.cwc.hitrateCdNow = Time.time + clauncherMe.cwc.hitrateCd;
		GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(vector));
		gameObject.transform.parent = fGameManager.allBulletFather.transform;
		return gameObject;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00032648 File Offset: 0x00030848
	public void SendSB(int buId, Vector3 born, Vector3 target, ArrayList parm, int zero, int isContinuous)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.launcher.launcherName);
		arrayList.Add(this.launcher.launcherNo);
		arrayList.Add(base.muzzleName);
		arrayList.Add(buId);
		arrayList.Add(this.launcher.bulletTypeNumber);
		arrayList.Add(new ArrayList
		{
			(int)(born.x * 100f),
			(int)(born.y * 100f),
			(int)(born.z * 100f)
		});
		arrayList.Add(new ArrayList
		{
			(int)(target.x * 100f),
			(int)(target.y * 100f),
			(int)(target.z * 100f)
		});
		arrayList.Add(parm);
		arrayList.Add(zero);
		arrayList.Add(this.launcher.skillIdByWeapon);
		arrayList.Add(this.launcher.CartridgePublicID);
		arrayList.Add(this.launcher.nowBulletNumberBefore);
		arrayList.Add(isContinuous);
		float time = Time.time;
		int num = (time - this.lastTick <= 100f) ? ((int)((time - this.lastTick + this.launcher.launcherDelayTime) * 1000f)) : 100000;
		arrayList.Add(num);
		bool flag = isContinuous == 0;
		if (flag)
		{
			this.lastTick = time;
		}
		num = ((time - this.lastTickContinue <= 100f) ? ((int)((time - this.lastTickContinue) * 1000f)) : 100000);
		arrayList.Add(num);
		bool flag2 = isContinuous > 0;
		if (flag2)
		{
			this.lastTickContinue = time;
		}
		arrayList.Add(this.launcher.NetObject.playerCommon.m_nHeroNo);
		arrayList.Add(this.launcher.oneShootTick);
		arrayList.Add(this.launcher.NetObject.loadID);
		this.launcher.NetObject.TosFun("SB", new object[]
		{
			arrayList
		});
	}

	// Token: 0x04000480 RID: 1152
	protected bool beDelay;

	// Token: 0x04000481 RID: 1153
	protected Vector3 targetDir = Vector3.zero;

	// Token: 0x04000482 RID: 1154
	protected bool isBreakShoot;

	// Token: 0x04000483 RID: 1155
	private float lastTick;

	// Token: 0x04000484 RID: 1156
	private float lastTickContinue;
}

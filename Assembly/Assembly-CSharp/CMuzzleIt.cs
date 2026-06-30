using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class CMuzzleIt : CMuzzle
{
	// Token: 0x06000602 RID: 1538 RVA: 0x00030730 File Offset: 0x0002E930
	public override int Fire(Vector3 from, object target, int __bulletGid)
	{
		return base.Fire(from, target, 0);
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0003074C File Offset: 0x0002E94C
	public GameObject CreateBulletObject(Vector3 targetPoint, out Vector3 tempFirePoint)
	{
		CLauncherIt clauncherIt = (CLauncherIt)this.launcher;
		bool isCopy = clauncherIt.NetObject.isCopy;
		if (isCopy)
		{
			bool flag = clauncherIt.hitrateType == 0;
			if (flag)
			{
				bool flag2 = clauncherIt.cwc.hitRateNow + (float)clauncherIt.hitrateAdd > (float)clauncherIt.hitrateMax;
				if (flag2)
				{
					clauncherIt.cwc.hitRateNow = (float)clauncherIt.hitrateMax;
				}
				else
				{
					clauncherIt.cwc.hitRateNow += (float)clauncherIt.hitrateAdd;
				}
			}
			else
			{
				clauncherIt.cwc.hitRateNow += Mathf.Pow(clauncherIt.cwc.hitRateNow, 2f) / 100f + (float)clauncherIt.hitrateAdd;
				bool flag3 = clauncherIt.cwc.hitRateNow > (float)clauncherIt.hitrateMax;
				if (flag3)
				{
					clauncherIt.cwc.hitRateNow = (float)clauncherIt.hitrateMax;
				}
			}
			clauncherIt.cwc.hitrateCdNow = Time.time + clauncherIt.cwc.hitrateCd;
		}
		Vector3 normalized = (targetPoint - base.bulletBornPoint).normalized.normalized;
		tempFirePoint = targetPoint;
		GameObject gameObject = CEffectPools.Instance.CreateGameObject(base.bulletPrefab, base.bulletBornPoint, Quaternion.LookRotation(normalized));
		gameObject.transform.parent = fGameManager.allBulletFather.transform;
		return gameObject;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000308C8 File Offset: 0x0002EAC8
	public void BulletPostionUpdate(int bullId, int x, int y, int z)
	{
		bool flag = this.dictBullets.ContainsKey(bullId);
		if (flag)
		{
			Vector3 position = new Vector3((float)x * 0.01f, (float)y * 0.01f, (float)z * 0.01f);
			base.transform.position = position;
		}
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00030918 File Offset: 0x0002EB18
	public void deleteBulletDict(CBulletIt bul)
	{
		bool flag = this.dictBullets.ContainsKey(bul.bulletGID);
		if (flag)
		{
			this.dictBullets.Remove(bul.bulletGID);
		}
	}

	// Token: 0x0400047D RID: 1149
	protected Dictionary<int, CBulletIt> dictBullets = new Dictionary<int, CBulletIt>();
}

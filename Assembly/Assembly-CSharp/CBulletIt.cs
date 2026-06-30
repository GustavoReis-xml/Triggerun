using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class CBulletIt : CBullet
{
	// Token: 0x06000326 RID: 806 RVA: 0x00016DE0 File Offset: 0x00014FE0
	public static void boomUpdate(ArrayList list)
	{
		int key = (int)list[0];
		bool flag = !CBullet.bulletDict.ContainsKey(key);
		if (!flag)
		{
			CBulletIt cbulletIt = (CBulletIt)CBullet.bulletDict[key];
			CBullet.bulletDict.Remove(key);
			bool flag2 = !(cbulletIt == null) && !cbulletIt.readyDestory;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)list[1];
				Vector3 position = new Vector3((float)((int)arrayList[0]) / 100f, (float)((int)arrayList[1]) / 100f, (float)((int)arrayList[2]) / 100f);
				bool flag3 = !(cbulletIt.transform == null);
				if (flag3)
				{
					cbulletIt.transform.position = position;
					cbulletIt.checkItBoom();
				}
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00016EC8 File Offset: 0x000150C8
	protected override void boom()
	{
		base.boom();
		bool flag = base.muzzle != null && base.muzzle.launcher != null && base.muzzle.launcher.NetObject != null && base.muzzle.launcher.NetObject.heroBase != null;
		if (flag)
		{
			base.muzzle.launcher.NetObject.heroBase.bulletBoom(base.muzzle.launcher.launcherName, base.muzzle.launcher.launcherNo, base.transform.position);
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00016F7C File Offset: 0x0001517C
	private void checkItBoom()
	{
		bool flag = !base.readyDestory;
		if (flag)
		{
			base.end = true;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00016FB8 File Offset: 0x000151B8
	public override void initBullet(CMuzzle muzz, CBulletData __data, bool child, int ismaster = 0, int sandanChildNum = 0)
	{
		base.initBullet(muzz, __data, child, ismaster, sandanChildNum);
		bool flag = base.bulletData.bulletBoomRange <= 0f || CBullet.bulletDict.ContainsKey(base.bulletGID);
		if (!flag)
		{
			float time = Time.time;
			bool flag2 = time > CBullet.bulletDictCheckTime;
			if (flag2)
			{
				CBullet.bulletDictCheckTime = time + 60f;
				ArrayList arrayList = new ArrayList();
				foreach (int num in CBullet.bulletDict.Keys)
				{
					bool flag3 = CBullet.bulletDict[num].gIDTime + 120f < time;
					if (flag3)
					{
						arrayList.Add(num);
					}
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					CBullet.bulletDict.Remove((int)arrayList[i]);
				}
			}
			CBullet.bulletDict.Add(base.bulletGID, this);
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x000170EC File Offset: 0x000152EC
	protected override void checkDelayBoom()
	{
		bool flag = !base.readyDestory && base.bulletData.bulletBoomDelay > 0f && Time.time > base.bulletCreateTime + this.boomTimeDealy && !base.readyDestory;
		if (flag)
		{
			base.end = true;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00017158 File Offset: 0x00015358
	protected override void destoryBullet()
	{
		((CMuzzleIt)base.muzzle).deleteBulletDict(this);
		base.destoryBullet();
	}
}

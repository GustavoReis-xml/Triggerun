using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class CLauncherIt : CLauncher
{
	// Token: 0x0600056E RID: 1390 RVA: 0x0002B114 File Offset: 0x00029314
	public int LauncherFire(string __muzzleName, Vector3 from, object target, int __bulletGid, NetLiving __netTarget, ArrayList parm)
	{
		bool flag = !this._muzzlesDict.ContainsKey(__muzzleName);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = base.bulletPrefab == null;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				CMuzzleIt cmuzzleIt = (CMuzzleIt)this._muzzlesDict[__muzzleName];
				cmuzzleIt.targetNet = __netTarget;
				int bulletTypeNumber = base.bulletTypeNumber;
				int num = bulletTypeNumber;
				if (num != 5)
				{
					if (num == 8)
					{
						((CMuzzlePhysicalIt)cmuzzleIt).FireParm(parm);
					}
				}
				else
				{
					((CMuzzleBezierIt)cmuzzleIt).FireParm(parm);
				}
				cmuzzleIt.Fire(from, target, __bulletGid);
				base.FireTalk();
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0002B1BE File Offset: 0x000293BE
	public void ServerReadyFull()
	{
		base.showAllFullEffect();
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0002B1C8 File Offset: 0x000293C8
	public void readyCancelIt()
	{
		base.disableAllFullEffect();
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0002B1D4 File Offset: 0x000293D4
	public void BulletPostionUpdate(string muzzleName, int bullId, int x, int y, int z)
	{
		CMuzzleIt cmuzzleIt = (CMuzzleIt)this._muzzlesDict[muzzleName];
		cmuzzleIt.BulletPostionUpdate(bullId, x, y, z);
	}
}

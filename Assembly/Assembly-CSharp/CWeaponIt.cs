using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class CWeaponIt : CWeapon
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x0600086B RID: 2155 RVA: 0x00047D14 File Offset: 0x00045F14
	public override float FullRate
	{
		get
		{
			bool flag = this.weaponData.launcherFullTime <= 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				result = (Time.time - this.beginFullTime) / this.weaponData.launcherFullTime;
			}
			return result;
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00047D60 File Offset: 0x00045F60
	public override void InitWeapon(CWeaponControl _weaponcon, NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		base.InitWeapon(_weaponcon, player, weaponinfo, point, pointFps, pointTps);
		for (int i = 0; i < weaponinfo.launcher.Count; i++)
		{
			ArrayList mainParam = (ArrayList)weaponinfo.launcher[i];
			int launcher_type = weaponinfo.launcher_type;
			int num = launcher_type;
			CLauncherIt clauncherIt;
			if (num != 1)
			{
				if (num != 2)
				{
					clauncherIt = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalIt>();
					clauncherIt.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
				}
				else
				{
					clauncherIt = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalIt>();
					clauncherIt.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
				}
			}
			else
			{
				clauncherIt = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalIt>();
				clauncherIt.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
			}
			bool flag = this.launchersDict.ContainsKey(clauncherIt.launcherNo);
			if (flag)
			{
				Debug.LogError("已经存在多个发射器(launcherNo)了,名称:" + weaponinfo.name);
			}
			this.launchersDict.Add(clauncherIt.launcherNo, clauncherIt);
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00047E78 File Offset: 0x00046078
	public int WeaponFire(int __launcherNo, string putKey, string __muzzleName, Vector3 fromPoint, Vector3 targetPoint, object target, int bulletGid, string targetBodyName, ArrayList parm)
	{
		CLauncherNormalIt clauncherNormalIt = (CLauncherNormalIt)this.launchersDict[__launcherNo];
		NetLiving netLiving = null;
		bool flag = (int)target == 0;
		int result;
		if (flag)
		{
			result = clauncherNormalIt.LauncherFire(__muzzleName, fromPoint, targetPoint, bulletGid, netLiving, parm);
		}
		else
		{
			netLiving = (this.NetObject.isCopy ? CFilmManager.Instance.GetFilmNetLiving((int)target) : (GameRoot.SceneSocket.unFsorket.GetNetObj((int)target) as NetLiving));
			bool flag2 = netLiving != null;
			if (flag2)
			{
				GameObject canKillCollider = netLiving.getCanKillCollider(targetBodyName);
				result = clauncherNormalIt.LauncherFire(__muzzleName, fromPoint, canKillCollider, bulletGid, netLiving, parm);
			}
			else
			{
				result = clauncherNormalIt.LauncherFire(__muzzleName, fromPoint, targetPoint, bulletGid, netLiving, parm);
			}
		}
		return result;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00047F40 File Offset: 0x00046140
	public void WeaponFireFullReady(int time1, int needTime)
	{
		this.fullIng = true;
		this.readyFullGoTime = (float)time1 * 0.001f;
		this.beginFullTime = Time.time - this.readyFullGoTime;
		CLauncherNormalIt clauncherNormalIt = (CLauncherNormalIt)this.launchersDict[this.defaultLauncherNo];
		this.cwc.fullWeapon = this;
		clauncherNormalIt.ServerReadyFull();
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00047FA0 File Offset: 0x000461A0
	public void WeaponFireFullCancel()
	{
		this.fullIng = false;
		this.cwc.fullWeapon = null;
		CLauncherNormalIt clauncherNormalIt = (CLauncherNormalIt)this.launchersDict[this.defaultLauncherNo];
		clauncherNormalIt.readyCancelIt();
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00047FE0 File Offset: 0x000461E0
	public void BulletPostionUpdate(int launcherNo, string muzzleName, int bullId, int x, int y, int z)
	{
		CLauncherNormalIt clauncherNormalIt = (CLauncherNormalIt)this.launchersDict[launcherNo];
		clauncherNormalIt.BulletPostionUpdate(muzzleName, bullId, x, y, z);
	}

	// Token: 0x04000626 RID: 1574
	private float beginFullTime;
}

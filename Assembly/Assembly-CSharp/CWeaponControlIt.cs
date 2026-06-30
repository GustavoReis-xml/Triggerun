using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class CWeaponControlIt : CWeaponControl
{
	// Token: 0x06000834 RID: 2100 RVA: 0x00045DA8 File Offset: 0x00043FA8
	public override void CreateWeaponControl(NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		base.CreateWeaponControl(player, weaponinfo, point, pointFps, pointTps);
		CWeaponIt cweaponIt = base.GetMuzzleContainer().AddComponent<CWeaponIt>();
		cweaponIt.InitWeapon(this, player, weaponinfo, point, pointFps, pointTps);
		this.weapons.Add(weaponinfo.shootname, cweaponIt);
		base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00045E08 File Offset: 0x00044008
	public void BulletNumberChange(int __weaponName, int __launcherNo, int number)
	{
		bool flag = number <= 0 || __launcherNo < 0;
		if (!flag)
		{
			bool flag2 = !this.weapons.ContainsKey(__weaponName);
			if (flag2)
			{
				Debug.LogError(string.Format("{0}的CB同步子弹数量,找不到发射器{1},{2},{3}.", new object[]
				{
					this.NetObject.playerCommon.m_nHeroNo,
					__launcherNo,
					__weaponName,
					number
				}));
			}
			else
			{
				CWeaponIt cweaponIt = (CWeaponIt)this.weapons[__weaponName];
				bool flag3 = !cweaponIt.launchersDict.ContainsKey(__launcherNo);
				if (flag3)
				{
					Debug.LogError(string.Format("{0}的CB同步子弹数量,找不到发射器{1},{2},{3},{4}.", new object[]
					{
						cweaponIt.weaponData.name,
						__launcherNo,
						cweaponIt.weaponData.shootname,
						__weaponName,
						number
					}));
				}
				else
				{
					CLauncherIt clauncherIt = (CLauncherIt)cweaponIt.launchersDict[__launcherNo];
					base.cartridgePublicNow[clauncherIt.cartridgeNo] = number;
				}
			}
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00045F2C File Offset: 0x0004412C
	public int WeaponFire(int __weaponName, int __launcherNo, string putKey, string __muzzleName, Vector3 fromPoint, Vector3 targetPoint, object target, int bulletGid, string targetBodyName, ArrayList parm)
	{
		bool flag = !this.weapons.ContainsKey(__weaponName);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			CWeaponIt cweaponIt = (CWeaponIt)this.weapons[__weaponName];
			result = cweaponIt.WeaponFire(__launcherNo, putKey, __muzzleName, fromPoint, targetPoint, target, bulletGid, targetBodyName, parm);
		}
		return result;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00045F80 File Offset: 0x00044180
	public void WeaponFireFullReady(int __weaponName, int time1, int needTime)
	{
		CWeaponIt cweaponIt = (CWeaponIt)this.weapons[__weaponName];
		cweaponIt.WeaponFireFullReady(time1, needTime);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00045FAC File Offset: 0x000441AC
	public void WeaponFireFullCancel(int __weaponName)
	{
		CWeaponIt cweaponIt = (CWeaponIt)this.weapons[__weaponName];
		cweaponIt.WeaponFireFullCancel();
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00045FD4 File Offset: 0x000441D4
	public void BulletPostionUpdate(int __weaponName, int launcherNo, string muzzleName, int bullId, int x, int y, int z)
	{
		CWeaponIt cweaponIt = (CWeaponIt)this.weapons[__weaponName];
		cweaponIt.BulletPostionUpdate(launcherNo, muzzleName, bullId, x, y, z);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00046008 File Offset: 0x00044208
	private void FixedUpdate()
	{
		bool flag = this.NetObject == null || !this.NetObject.isCopy || !this.NetObject.playerHaveCreate || !this.NetObject.haveDestroy || base.hitRateNow == (float)base.hitRateBase[base.iconWeapon];
		if (!flag)
		{
			bool flag2 = Time.time > base.hitrateCdNow;
			if (flag2)
			{
				bool flag3 = base.hitRateNow > (float)base.hitRateBase[base.iconWeapon];
				if (flag3)
				{
					float deltaTime = Time.deltaTime;
					float num = (float)base.hitrateSub * deltaTime;
					base.hitRateNow -= num;
					bool flag4 = base.hitRateNow < (float)base.hitRateBase[base.iconWeapon];
					if (flag4)
					{
						base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
					}
				}
				else
				{
					base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
				}
			}
			else
			{
				bool flag5 = base.hitrateSubFire == 0f;
				if (!flag5)
				{
					bool flag6 = base.hitRateNow > (float)base.hitRateBase[base.iconWeapon];
					if (flag6)
					{
						float deltaTime2 = Time.deltaTime;
						float num2 = (float)base.hitrateSub * deltaTime2 * base.hitrateSubFire;
						base.hitRateNow -= num2;
						bool flag7 = base.hitRateNow < (float)base.hitRateBase[base.iconWeapon];
						if (flag7)
						{
							base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
						}
					}
					else
					{
						base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
					}
				}
			}
		}
	}
}

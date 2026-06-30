using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class CWeapon : MonoBehaviour
{
	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00045190 File Offset: 0x00043390
	public float oneShootTime
	{
		get
		{
			bool flag = !this._checkOneShootTime.equla(this._oneShootTime, out this.outTick) && CCheckDelay.Instance.weaponHacked != 2;
			if (flag)
			{
				CCheckDelay.Instance.weaponHacked = 2;
				CCheckDelay.Instance.weaponStr = string.Format("P:{0:F4},{1:F4},{2:F4}", this._oneShootTime, this._checkOneShootTime.CCParm, this.outTick);
			}
			return this._oneShootTime;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0004521F File Offset: 0x0004341F
	public int oneShootTick
	{
		get
		{
			return this._oneShootTick;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00045227 File Offset: 0x00043427
	public float seed
	{
		get
		{
			return this._seedFire;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00045230 File Offset: 0x00043430
	// (set) Token: 0x060007F9 RID: 2041 RVA: 0x000452C0 File Offset: 0x000434C0
	public float lastShootTick
	{
		get
		{
			bool flag = !this._checkLastShootTick.equla(this._lastShootTick, out this.outTick) && CCheckDelay.Instance.weaponHacked != 1;
			if (flag)
			{
				CCheckDelay.Instance.weaponHacked = 1;
				CCheckDelay.Instance.weaponStr = string.Format("P:{0:F4},{1:F4},{2:F4}", this._lastShootTick, this._checkLastShootTick.CCParm, this.outTick);
			}
			return this._lastShootTick;
		}
		set
		{
			this._lastShootTick = value;
			bool flag = this._checkLastShootTick == null;
			if (flag)
			{
				this._checkLastShootTick = new CCheckEditFloat(this._lastShootTick);
			}
			else
			{
				this._checkLastShootTick.CCParm = this._lastShootTick;
			}
			this._seedFire = this._lastShootTick;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060007FA RID: 2042 RVA: 0x00045317 File Offset: 0x00043517
	public virtual float FullRate
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060007FB RID: 2043 RVA: 0x00045320 File Offset: 0x00043520
	// (set) Token: 0x060007FC RID: 2044 RVA: 0x00045338 File Offset: 0x00043538
	public DateTime lastShootTime
	{
		get
		{
			return this._lastShootTime;
		}
		set
		{
			bool flag = this._lastShootTime > value;
			if (flag)
			{
				string sMsg = string.Concat(new string[]
				{
					"作弊:玩家修改了时间来提升射击速度判断,玩家:",
					this.NetObject.playerCommon.m_sName,
					",武器",
					this.cwc.wpData.name,
					","
				});
				FunManager.Instance.Send(this.NetObject, "140", sMsg);
			}
			this._lastShootTime = value;
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000453C0 File Offset: 0x000435C0
	public virtual void InitWeapon(CWeaponControl _weaponcon, NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		this.cwc = _weaponcon;
		this.NetObject = player;
		this.weaponData = weaponinfo;
		this._oneShootTime = this.maxShootRate / (float)weaponinfo.shootspeed;
		this._checkOneShootTime = new CCheckEditFloat(this._oneShootTime);
		this._oneShootTick = Mathf.CeilToInt(this.maxShootRate * 1000f / (float)weaponinfo.shootspeed);
		this._lastShootTime = Fun.GetNow(this.NetObject).AddHours(-1.0);
		this.lastShootTick = 1f;
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00045458 File Offset: 0x00043658
	public void replaceMuzzle(Transform[] point)
	{
		foreach (int key in this.launchersDict.Keys)
		{
			CLauncher clauncher = this.launchersDict[key];
			clauncher.replaceMuzzle(point);
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000454C4 File Offset: 0x000436C4
	public void StopAllFireEffect()
	{
		foreach (int key in this.launchersDict.Keys)
		{
			CLauncher clauncher = this.launchersDict[key];
			clauncher.StopAllFireEffect();
		}
	}

	// Token: 0x040005EE RID: 1518
	public NetLiving NetObject;

	// Token: 0x040005EF RID: 1519
	public CWeaponControl cwc;

	// Token: 0x040005F0 RID: 1520
	public XMLWeapon weaponData;

	// Token: 0x040005F1 RID: 1521
	public float maxShootRate = 60f;

	// Token: 0x040005F2 RID: 1522
	public Dictionary<int, CLauncher> launchersDict = new Dictionary<int, CLauncher>();

	// Token: 0x040005F3 RID: 1523
	public int defaultLauncherNo;

	// Token: 0x040005F4 RID: 1524
	private float _oneShootTime;

	// Token: 0x040005F5 RID: 1525
	private CCheckEditFloat _checkOneShootTime;

	// Token: 0x040005F6 RID: 1526
	private int _oneShootTick;

	// Token: 0x040005F7 RID: 1527
	private float _seedFire;

	// Token: 0x040005F8 RID: 1528
	public float _lastShootTick;

	// Token: 0x040005F9 RID: 1529
	private float outTick;

	// Token: 0x040005FA RID: 1530
	private CCheckEditFloat _checkLastShootTick;

	// Token: 0x040005FB RID: 1531
	public float readyFullGoTime;

	// Token: 0x040005FC RID: 1532
	public bool fullIng;

	// Token: 0x040005FD RID: 1533
	private DateTime _lastShootTime;
}

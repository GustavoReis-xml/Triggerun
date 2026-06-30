using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class CWeaponControl : MonoBehaviour
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000801 RID: 2049 RVA: 0x00045550 File Offset: 0x00043750
	public string[] groupIcon
	{
		get
		{
			bool flag = this.iconWeapon >= this.weaponIcons.Count;
			string[] result;
			if (flag)
			{
				result = (string[])this.weaponIcons[0];
			}
			else
			{
				result = (string[])this.weaponIcons[this.iconWeapon];
			}
			return result;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000802 RID: 2050 RVA: 0x000455A7 File Offset: 0x000437A7
	public int[] hitRateBase
	{
		get
		{
			return this._hitRateBase;
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x000455B0 File Offset: 0x000437B0
	// (set) Token: 0x06000804 RID: 2052 RVA: 0x000455C8 File Offset: 0x000437C8
	public float hitRateNow
	{
		get
		{
			return this._hitRateNow;
		}
		set
		{
			this._hitRateNow = value;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000805 RID: 2053 RVA: 0x000455D4 File Offset: 0x000437D4
	// (set) Token: 0x06000806 RID: 2054 RVA: 0x000455EC File Offset: 0x000437EC
	public float hitRateMax
	{
		get
		{
			return this._hitRateMax;
		}
		set
		{
			this._hitRateMax = value;
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000807 RID: 2055 RVA: 0x000455F6 File Offset: 0x000437F6
	public int hitrateSub
	{
		get
		{
			return this._hitrateSub;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000808 RID: 2056 RVA: 0x00045600 File Offset: 0x00043800
	// (set) Token: 0x06000809 RID: 2057 RVA: 0x00045618 File Offset: 0x00043818
	public int[] cartridgePublicNow
	{
		get
		{
			return this._cartridgePublicNow;
		}
		set
		{
			this._cartridgePublicNow = value;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x0600080A RID: 2058 RVA: 0x00045622 File Offset: 0x00043822
	public int bulletNow
	{
		get
		{
			return this.cartridgePublicNow[this._cartridgeNow];
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x00045634 File Offset: 0x00043834
	// (set) Token: 0x0600080C RID: 2060 RVA: 0x0004564C File Offset: 0x0004384C
	public int[] cartridgePublicID
	{
		get
		{
			return this._cartridgePublicID;
		}
		set
		{
			this._cartridgePublicID = value;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x0600080D RID: 2061 RVA: 0x00045656 File Offset: 0x00043856
	public float hitrateSubFire
	{
		get
		{
			return this._hitrateSubFire;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x0600080E RID: 2062 RVA: 0x00045660 File Offset: 0x00043860
	// (set) Token: 0x0600080F RID: 2063 RVA: 0x00045678 File Offset: 0x00043878
	public float hitrateCd
	{
		get
		{
			return this._hitrateCd;
		}
		set
		{
			this._hitrateCd = value;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000810 RID: 2064 RVA: 0x00045684 File Offset: 0x00043884
	// (set) Token: 0x06000811 RID: 2065 RVA: 0x0004569C File Offset: 0x0004389C
	public float hitrateCdNow
	{
		get
		{
			return this._hitrateCdNow;
		}
		set
		{
			this._hitrateCdNow = value;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000812 RID: 2066 RVA: 0x000456A6 File Offset: 0x000438A6
	public int[] cartridgePublicMax
	{
		get
		{
			return this._cartridgePublicMax;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06000813 RID: 2067 RVA: 0x000456B0 File Offset: 0x000438B0
	public int bulletMax
	{
		get
		{
			bool flag = this.cartridgePublicMax.Length == 1 && this.cartridgePublicMax[this._cartridgeNow] == 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = this.cartridgePublicMax[this._cartridgeNow];
			}
			return result;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06000814 RID: 2068 RVA: 0x000456F6 File Offset: 0x000438F6
	public ArrayList weaponIcons
	{
		get
		{
			return this._weaponIcons;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06000815 RID: 2069 RVA: 0x00045700 File Offset: 0x00043900
	// (set) Token: 0x06000816 RID: 2070 RVA: 0x00045718 File Offset: 0x00043918
	public float lastFirePublicTime
	{
		get
		{
			return this._lastFirePublicTime;
		}
		set
		{
			this._lastFirePublicTime = value;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000817 RID: 2071 RVA: 0x00045724 File Offset: 0x00043924
	// (set) Token: 0x06000818 RID: 2072 RVA: 0x0004573C File Offset: 0x0004393C
	public CTarget targetSelect
	{
		get
		{
			return this._targetSelect;
		}
		set
		{
			this._targetSelect = value;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000819 RID: 2073 RVA: 0x00045748 File Offset: 0x00043948
	// (set) Token: 0x0600081A RID: 2074 RVA: 0x00045760 File Offset: 0x00043960
	public CLauncher cartridgeLauncher
	{
		get
		{
			return this._cartridgeLauncher;
		}
		set
		{
			this._cartridgeLauncher = value;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x0600081B RID: 2075 RVA: 0x0004576C File Offset: 0x0004396C
	// (set) Token: 0x0600081C RID: 2076 RVA: 0x00045784 File Offset: 0x00043984
	public int iconWeapon
	{
		get
		{
			return this._groupNow;
		}
		set
		{
			this._groupNow = value;
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x0600081D RID: 2077 RVA: 0x00045790 File Offset: 0x00043990
	// (set) Token: 0x0600081E RID: 2078 RVA: 0x000457A8 File Offset: 0x000439A8
	public CWeapon fullWeapon
	{
		get
		{
			return this._fullWeapon;
		}
		set
		{
			this._fullWeapon = value;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x000457B4 File Offset: 0x000439B4
	public bool fullIng
	{
		get
		{
			bool flag = this._fullWeapon == null;
			return !flag && this._fullWeapon.fullIng;
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x000457E6 File Offset: 0x000439E6
	private void Start()
	{
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x000457E9 File Offset: 0x000439E9
	public void InitBulletBeforeSubFor_1()
	{
		this.nowBulletNumberBeforeForInfinite = -1;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x000457F4 File Offset: 0x000439F4
	public void StopAllFireEffect()
	{
		foreach (int key in this.weapons.Keys)
		{
			CWeapon cweapon = this.weapons[key];
			cweapon.StopAllFireEffect();
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00045860 File Offset: 0x00043A60
	public void WeaponDestroy()
	{
		Transform transform = base.transform.root.transform.FindChild("_muzzleContainer");
		bool flag = transform != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000458A4 File Offset: 0x00043AA4
	public virtual void SwitchWeaponGroup(int _group)
	{
		bool flag = this.wpData.cartridgeNoForGroup.Length > _group;
		if (flag)
		{
			this.iconWeapon = _group;
			this._cartridgeNow = this.wpData.cartridgeNoForGroup[_group];
			this.hitRateNow = (float)this.hitRateBase[this.iconWeapon];
		}
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x000458F8 File Offset: 0x00043AF8
	public virtual void UpdateFireTime(float fCD)
	{
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000458FC File Offset: 0x00043AFC
	public virtual bool CanFirePublicCD(float add = 0f)
	{
		return true;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00045910 File Offset: 0x00043B10
	public void SetupWeapon(NetLiving heroNetObj, WeaponData __data, int __hitrateSub, int[] __cartridgepublic)
	{
		this.wpData = __data;
		this._hitRateBase = __data.hitratebase;
		this._hitrateSub = __hitrateSub;
		this._cartridgePublicNow = Fun.DeepCopy<int[]>(__cartridgepublic);
		this._cartridgePublicMax = Fun.DeepCopy<int[]>(__cartridgepublic);
		this._cartridgePublicID = new int[this.cartridgePublicNow.Length];
		this._weaponIcons = __data.weaponicons;
		this._hitrateCd = (float)__data.hitratecd / 1000f;
		this._hitrateSubFire = (float)__data.hitratesubfire / 100f;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00045998 File Offset: 0x00043B98
	public void replaceMuzzlePoint(int __weaponName, Transform[] point)
	{
		CWeapon cweapon = this.weapons[__weaponName];
		cweapon.replaceMuzzle(point);
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x000459BC File Offset: 0x00043BBC
	public GameObject GetMuzzleContainer()
	{
		bool flag = this._muzzleContainer == null;
		if (flag)
		{
			this._muzzleContainer = new GameObject("_muzzleContainer");
			this._muzzleContainer.transform.parent = this.NetObject.userRoot.transform;
		}
		return this._muzzleContainer;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x00045A18 File Offset: 0x00043C18
	public virtual void CreateWeaponControl(NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		this.NetObject = player;
		bool flag = this.weapons.ContainsKey(weaponinfo.shootname);
		if (flag)
		{
			Debug.LogError(string.Format("策划注意:{0}已经存在这个武器了:{1}", player.modelName, weaponinfo.shootname));
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00045A68 File Offset: 0x00043C68
	public virtual void fullBullet(ArrayList __data)
	{
		int num = (int)__data[1];
		this.cartridgePublicNow[num] = (int)__data[2];
		this.cartridgePublicID[num] = (int)__data[3];
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00045AAC File Offset: 0x00043CAC
	public virtual void InitBulletCartridge(ArrayList __dataId, ArrayList __dataCart)
	{
		for (int i = 0; i < __dataId.Count; i++)
		{
			this.cartridgePublicID[i] = (int)__dataId[i];
		}
		for (int j = 0; j < __dataCart.Count; j++)
		{
			this.cartridgePublicNow[j] = (int)__dataCart[j];
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00045B10 File Offset: 0x00043D10
	public void InitBulletGem(ArrayList __data)
	{
		for (int i = 0; i < __data.Count; i++)
		{
			ArrayList arrayList = (ArrayList)__data[i];
			int num = (int)arrayList[0];
			int num2 = (int)arrayList[1];
			CWeapon cweapon = this.weapons[num];
			bool flag = cweapon == null;
			if (flag)
			{
				Debug.LogError(string.Format("符文数据找不到武器__weaponName:{0}", num));
			}
			else
			{
				CLauncher clauncher = cweapon.launchersDict[num2];
				bool flag2 = clauncher == null;
				if (flag2)
				{
					Debug.LogError(string.Format("符文数据找不到武器__launcherNo:{0}", num2));
				}
				else
				{
					clauncher.InitGemSpd((int)arrayList[2]);
					clauncher.InitGemBoomTime((int)arrayList[3]);
					clauncher.InitGemDis((int)arrayList[4]);
				}
			}
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00045C0C File Offset: 0x00043E0C
	public void DeadSound(int __weaponName, int __launcherNo)
	{
		bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		if (!flag)
		{
			CWeapon cweapon = this.weapons[__weaponName];
			bool flag2 = !(cweapon == null) && cweapon.launchersDict.ContainsKey(__launcherNo);
			if (flag2)
			{
				CLauncher clauncher = cweapon.launchersDict[__launcherNo];
				bool flag3 = !(clauncher == null);
				if (flag3)
				{
					clauncher.DeadTalk();
				}
			}
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00045C88 File Offset: 0x00043E88
	public void damageHitTalk(int __weaponName, int __launcherNo)
	{
		bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		if (!flag)
		{
			CWeapon cweapon = this.weapons[__weaponName];
			bool flag2 = !(cweapon == null) && cweapon.launchersDict.ContainsKey(__launcherNo);
			if (flag2)
			{
				CLauncher clauncher = cweapon.launchersDict[__launcherNo];
				bool flag3 = !(clauncher == null);
				if (flag3)
				{
					clauncher.PlayDamageTalk();
				}
			}
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00045D04 File Offset: 0x00043F04
	public DateTime LastShootTime(int __weaponName)
	{
		CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
		bool flag = cweaponMe == null;
		DateTime result;
		if (flag)
		{
			result = DateTime.Now;
		}
		else
		{
			result = cweaponMe.lastShootTime;
		}
		return result;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00045D44 File Offset: 0x00043F44
	public float FullRate(int __weaponName)
	{
		CWeapon cweapon = this.weapons[__weaponName];
		bool flag = cweapon == null;
		float result;
		if (flag)
		{
			result = 0f;
		}
		else
		{
			result = cweapon.FullRate;
		}
		return result;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00045D7D File Offset: 0x00043F7D
	public static void test()
	{
	}

	// Token: 0x040005FE RID: 1534
	public Dictionary<int, CWeapon> weapons = new Dictionary<int, CWeapon>();

	// Token: 0x040005FF RID: 1535
	public string sInputFullKey = string.Empty;

	// Token: 0x04000600 RID: 1536
	public NetLiving NetObject;

	// Token: 0x04000601 RID: 1537
	public WeaponData wpData;

	// Token: 0x04000602 RID: 1538
	public bool notFillCartridge;

	// Token: 0x04000603 RID: 1539
	public int nowBulletNumberBeforeForInfinite = -1;

	// Token: 0x04000604 RID: 1540
	private int[] _hitRateBase;

	// Token: 0x04000605 RID: 1541
	private float _hitRateNow;

	// Token: 0x04000606 RID: 1542
	private float _hitRateMax;

	// Token: 0x04000607 RID: 1543
	private int _hitrateSub;

	// Token: 0x04000608 RID: 1544
	private int[] _cartridgePublicNow;

	// Token: 0x04000609 RID: 1545
	protected int _cartridgeNow;

	// Token: 0x0400060A RID: 1546
	private int[] _cartridgePublicID;

	// Token: 0x0400060B RID: 1547
	private float _hitrateSubFire;

	// Token: 0x0400060C RID: 1548
	private float _hitrateCd;

	// Token: 0x0400060D RID: 1549
	private float _hitrateCdNow;

	// Token: 0x0400060E RID: 1550
	private ArrayList _weaponIcons;

	// Token: 0x0400060F RID: 1551
	private int[] _cartridgePublicMax;

	// Token: 0x04000610 RID: 1552
	private float _lastFirePublicTime;

	// Token: 0x04000611 RID: 1553
	private CTarget _targetSelect;

	// Token: 0x04000612 RID: 1554
	private CLauncher _cartridgeLauncher;

	// Token: 0x04000613 RID: 1555
	private int _groupNow;

	// Token: 0x04000614 RID: 1556
	private GameObject _muzzleContainer;

	// Token: 0x04000615 RID: 1557
	private CWeapon _fullWeapon;
}

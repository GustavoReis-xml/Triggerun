using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class CWeaponControlMe : CWeaponControl
{
	// Token: 0x17000178 RID: 376
	// (get) Token: 0x0600083C RID: 2108 RVA: 0x000461C4 File Offset: 0x000443C4
	public int hitRateBaseDefault
	{
		get
		{
			return base.hitRateBase[base.iconWeapon];
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600083D RID: 2109 RVA: 0x000461D4 File Offset: 0x000443D4
	// (set) Token: 0x0600083E RID: 2110 RVA: 0x000461EC File Offset: 0x000443EC
	public bool fillCartridge
	{
		get
		{
			return this._fillCartridge;
		}
		set
		{
			this._fillCartridge = value;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x0600083F RID: 2111 RVA: 0x000461F8 File Offset: 0x000443F8
	public float fullNow
	{
		get
		{
			bool flag = base.fullWeapon == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				result = base.fullWeapon.readyFullGoTime;
			}
			return result;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000840 RID: 2112 RVA: 0x00046230 File Offset: 0x00044430
	public float fullMax
	{
		get
		{
			bool flag = base.fullWeapon == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				result = base.fullWeapon.weaponData.launcherFullTime;
			}
			return result;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000841 RID: 2113 RVA: 0x0004626C File Offset: 0x0004446C
	// (set) Token: 0x06000842 RID: 2114 RVA: 0x00046284 File Offset: 0x00044484
	public float lastdelayPublicTime
	{
		get
		{
			return this._lastdelayPublicTime;
		}
		set
		{
			this._lastdelayPublicTime = value;
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000843 RID: 2115 RVA: 0x00046290 File Offset: 0x00044490
	public bool canFillCartridge
	{
		get
		{
			bool fillCartridge = this._fillCartridge;
			bool result;
			if (fillCartridge)
			{
				result = false;
			}
			else
			{
				bool flag = base.bulletMax == -1;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = base.bulletNow < base.bulletMax;
					result = flag2;
				}
			}
			return result;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000844 RID: 2116 RVA: 0x000462DC File Offset: 0x000444DC
	public bool mustFillCartridge
	{
		get
		{
			bool fillCartridge = this._fillCartridge;
			bool result;
			if (fillCartridge)
			{
				result = false;
			}
			else
			{
				bool flag = base.bulletMax == -1;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = base.bulletNow == 0;
					result = flag2;
				}
			}
			return result;
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00046324 File Offset: 0x00044524
	public override void UpdateFireTime(float fCD)
	{
		bool flag = fCD > 0f;
		if (flag)
		{
			base.lastFirePublicTime = Time.time + fCD;
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00046350 File Offset: 0x00044550
	public override bool CanFirePublicCD(float add = 0f)
	{
		return Time.time >= base.lastFirePublicTime + add;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00046380 File Offset: 0x00044580
	public void UpdateDelayTime(float delay)
	{
		bool flag = delay <= 0.0001f;
		if (flag)
		{
			this._lastdelayPublicTime = 0f;
		}
		else
		{
			this._lastdelayPublicTime = Time.time + delay + 0.1f;
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000463C4 File Offset: 0x000445C4
	public bool CanFirePublicDelay(float add = 0f)
	{
		return Time.time >= this._lastdelayPublicTime + add;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000463F4 File Offset: 0x000445F4
	public override void CreateWeaponControl(NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		base.CreateWeaponControl(player, weaponinfo, point, pointFps, pointTps);
		base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
		CWeaponMe cweaponMe = base.GetMuzzleContainer().AddComponent<CWeaponMe>();
		cweaponMe.InitWeapon(this, player, weaponinfo, point, pointFps, pointTps);
		this.weapons.Add(weaponinfo.shootname, cweaponMe);
		bool flag = weaponinfo.key != "0" && !this.GroupcartridgePublic.ContainsKey(weaponinfo.keygroup);
		if (flag)
		{
			this.GroupcartridgePublic.Add(weaponinfo.keygroup, cweaponMe);
		}
		bool flag2 = weaponinfo.keyfull != "0";
		if (flag2)
		{
			bool flag3 = this.weaponsFull.ContainsKey(weaponinfo.keyfull);
			if (flag3)
			{
				Debug.LogError("已经存在这个充能快捷键了,一个英雄的不能有一个充能带动2个武器,名称:" + weaponinfo.name);
			}
			this.weaponsFull.Add(weaponinfo.keyfull, cweaponMe);
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000464EC File Offset: 0x000446EC
	public override void SwitchWeaponGroup(int _group)
	{
		this.weaponKeys = new Dictionary<string, CWeaponMe>();
		base.iconWeapon = _group;
		this._cartridgeNow = this.wpData.cartridgeNoForGroup[_group];
		foreach (int key in this.weapons.Keys)
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[key];
			bool flag = cweaponMe.weaponData.keygroup == base.iconWeapon && !(cweaponMe.weaponData.key == "0");
			if (flag)
			{
				this.weaponKeys.Add(cweaponMe.weaponData.key, cweaponMe);
			}
		}
		base.hitRateNow = (float)base.hitRateBase[base.iconWeapon];
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000465DC File Offset: 0x000447DC
	public void fullBreakCancel()
	{
		bool flag = this.sInputFullKey.Length >= 1 && this.weaponsFull.ContainsKey(this.sInputFullKey);
		if (flag)
		{
			CWeaponMe cweaponMe = this.weaponsFull[this.sInputFullKey];
			cweaponMe.fullCancel();
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0004662B File Offset: 0x0004482B
	public void SkillFullWeaponShoot(int __weaponName)
	{
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00046630 File Offset: 0x00044830
	public void readyCancel(string fullkey, bool cancel)
	{
		bool flag = this.weaponsFull.ContainsKey(fullkey);
		if (flag)
		{
			CWeaponMe cweaponMe = this.weaponsFull[fullkey];
			cweaponMe.readyCancel(cancel);
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00046668 File Offset: 0x00044868
	public int WeaponFireBySkill(int __weaponName, string putKey, Vector3 targetPoint, object target)
	{
		bool flag = this.NetObject.isDead || __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
			bool flag2 = cweaponMe.weaponData.launcherFullTime > 0f;
			if (flag2)
			{
				cweaponMe.FullFireBySkill();
				result = 1;
			}
			else
			{
				result = cweaponMe.WeaponFire(putKey, targetPoint, target);
			}
		}
		return result;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x000466E4 File Offset: 0x000448E4
	public int WeaponFire(int __weaponName, string putKey, Vector3 targetPoint, object target)
	{
		bool flag = this.NetObject != null && this.NetObject.isDead;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
			if (flag2)
			{
				result = 0;
			}
			else
			{
				CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
				bool flag3 = cweaponMe.weaponData.launcherFullTime > 0f;
				if (flag3)
				{
					cweaponMe.readyCancel(true);
					result = 1;
				}
				else
				{
					result = cweaponMe.WeaponFire(putKey, targetPoint, target);
				}
			}
		}
		return result;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00046774 File Offset: 0x00044974
	public void WeaponActiveBySkill(int __weaponName, int _skill)
	{
		bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		if (!flag)
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
			cweaponMe.WeaponActiveBySkill(_skill);
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000467B8 File Offset: 0x000449B8
	public void WeaponDownKey(int __weaponName)
	{
		bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		if (!flag)
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
			cweaponMe.WeaponDownKey();
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x000467FC File Offset: 0x000449FC
	public bool LauncherPublicCD(int __weaponName)
	{
		bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
			result = cweaponMe.LauncherPublicCD();
		}
		return result;
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00046844 File Offset: 0x00044A44
	public bool Readyed(int __weaponName)
	{
		bool isDead = this.NetObject.isDead;
		bool result;
		if (isDead)
		{
			result = false;
		}
		else
		{
			bool flag = __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
			if (flag)
			{
				result = true;
			}
			else
			{
				CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
				result = cweaponMe.WeaponReadyed();
			}
		}
		return result;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x000468A0 File Offset: 0x00044AA0
	public int WeaponFire(int __weaponName, string putKey)
	{
		bool flag = this.NetObject.isDead || __weaponName == 0 || !this.weapons.ContainsKey(__weaponName);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			CWeaponMe cweaponMe = (CWeaponMe)this.weapons[__weaponName];
			result = cweaponMe.WeaponFire(putKey);
		}
		return result;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x000468F8 File Offset: 0x00044AF8
	public void StopFulling()
	{
		foreach (string key in this.weaponsFull.Keys)
		{
			CWeaponMe cweaponMe = this.weaponsFull[key];
			cweaponMe.readyCancel(true);
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00046964 File Offset: 0x00044B64
	public void DoCartridge()
	{
		bool canFillCartridge = this.canFillCartridge;
		if (canFillCartridge)
		{
			this.beginFillCartridge();
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00046988 File Offset: 0x00044B88
	public void beginFillCartridge()
	{
		bool flag = !this.notFillCartridge;
		if (flag)
		{
			this.fullBreakCancel();
			this.cartridgeNeed = true;
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000469B4 File Offset: 0x00044BB4
	public void CheckMustFillCartridge()
	{
		bool flag = this.GroupcartridgePublic.ContainsKey(this._cartridgeNow) && this.mustFillCartridge;
		if (flag)
		{
			this.beginFillCartridge();
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x000469EC File Offset: 0x00044BEC
	private void FillCartridgeReal()
	{
		bool isDead = this.NetObject.isDead;
		if (!isDead)
		{
			CWeaponMe cweaponMe = this.GroupcartridgePublic[this._cartridgeNow];
			bool bePlayer = this.NetObject.bePlayer;
			if (bePlayer)
			{
				bool flag = !this.NetObject.heroBase.BulletCanFill();
				if (flag)
				{
					return;
				}
				this.NetObject.playerSound.PlayLoadCartridgeSound(this._cartridgeNow);
				this.NetObject.heroBase.BulletFillBegin();
			}
			this._fillCartridge = true;
			this.NetObject.SetAnimator("Reload", 0, true, 1);
			this.NetObject.SendMsgToServer(31203, new ArrayList
			{
				cweaponMe.weaponData.shootname,
				this.NetObject.getNetid()
			});
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00046ADC File Offset: 0x00044CDC
	public void CheckFillCartridgeForWeaponName(int __weaponName, int cartridgeDelayTime)
	{
		this.cartridgeAddCDTimeByOther((float)cartridgeDelayTime);
		bool mustFillCartridge = this.mustFillCartridge;
		if (mustFillCartridge)
		{
			this.beginFillCartridge();
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00046B08 File Offset: 0x00044D08
	public void cartridgeAddCDTimeByOther(float t)
	{
		bool flag = t > 0f;
		if (flag)
		{
			DateTime t2 = Fun.GetServerDate().AddMilliseconds((double)t);
			bool flag2 = !(t2 < this.cartridgeReadyTime);
			if (flag2)
			{
				this.cartridgeReadyTime = t2;
			}
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00046B50 File Offset: 0x00044D50
	public void cartridgeAddCDTime(int t)
	{
		this.cartridgeReadyTime = Fun.GetServerDate().AddMilliseconds((double)t);
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00046B74 File Offset: 0x00044D74
	public void cancelFillCartridge()
	{
		CWeaponMe cweaponMe = this.GroupcartridgePublic[this._cartridgeNow];
		this.cartridgeNeed = false;
		this._fillCartridge = false;
		bool flag = this.NetObject.netobject_type != 6;
		if (flag)
		{
			this.NetObject.SendMessageToServer(31204, new ArrayList
			{
				cweaponMe.weaponData.shootname,
				0
			});
		}
		bool bePlayer = this.NetObject.bePlayer;
		if (bePlayer)
		{
			this.NetObject.heroBase.BulletFillBreak();
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00046C15 File Offset: 0x00044E15
	public override void fullBullet(ArrayList __data)
	{
		base.fullBullet(__data);
		this._fillCartridge = false;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00046C28 File Offset: 0x00044E28
	private void FixedUpdate()
	{
		bool flag = this.cartridgeNeed && Fun.GetServerDate() > this.cartridgeReadyTime;
		if (flag)
		{
			this.cartridgeNeed = false;
			this.FillCartridgeReal();
		}
		bool flag2 = base.hitRateNow == (float)base.hitRateBase[base.iconWeapon];
		if (!flag2)
		{
			bool flag3 = Time.time > base.hitrateCdNow;
			if (flag3)
			{
				bool flag4 = base.hitRateNow > (float)base.hitRateBase[base.iconWeapon];
				if (flag4)
				{
					float deltaTime = Time.deltaTime;
					float num = (float)base.hitrateSub * deltaTime;
					base.hitRateNow -= num;
					bool flag5 = base.hitRateNow < (float)base.hitRateBase[base.iconWeapon];
					if (flag5)
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
				bool flag6 = base.hitrateSubFire == 0f;
				if (!flag6)
				{
					bool flag7 = base.hitRateNow > (float)base.hitRateBase[base.iconWeapon];
					if (flag7)
					{
						float deltaTime2 = Time.deltaTime;
						float num2 = (float)base.hitrateSub * deltaTime2 * base.hitrateSubFire;
						base.hitRateNow -= num2;
						bool flag8 = base.hitRateNow < (float)base.hitRateBase[base.iconWeapon];
						if (flag8)
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

	// Token: 0x04000616 RID: 1558
	public bool fullIsOpen = true;

	// Token: 0x04000617 RID: 1559
	public Dictionary<string, CWeaponMe> weaponKeys = new Dictionary<string, CWeaponMe>();

	// Token: 0x04000618 RID: 1560
	public Dictionary<int, CWeaponMe> GroupcartridgePublic = new Dictionary<int, CWeaponMe>();

	// Token: 0x04000619 RID: 1561
	public Dictionary<string, CWeaponMe> weaponsFull = new Dictionary<string, CWeaponMe>();

	// Token: 0x0400061A RID: 1562
	private bool cartridgeNeed;

	// Token: 0x0400061B RID: 1563
	private DateTime cartridgeReadyTime = Fun.GetServerDate();

	// Token: 0x0400061C RID: 1564
	private bool _fillCartridge;

	// Token: 0x0400061D RID: 1565
	private float _lastdelayPublicTime;
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class CWeaponMe : CWeapon
{
	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000872 RID: 2162 RVA: 0x00048018 File Offset: 0x00046218
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
				result = (Time.time - this.readyFullGoTime) / this.weaponData.launcherFullTime;
			}
			return result;
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00048064 File Offset: 0x00046264
	public override void InitWeapon(CWeaponControl _weaponcon, NetLiving player, XMLWeapon weaponinfo, Transform[] point, Transform[] pointFps, Transform[] pointTps)
	{
		base.InitWeapon(_weaponcon, player, weaponinfo, point, pointFps, pointTps);
		for (int i = 0; i < weaponinfo.launcher.Count; i++)
		{
			ArrayList mainParam = (ArrayList)weaponinfo.launcher[i];
			int launcher_type = weaponinfo.launcher_type;
			int num = launcher_type;
			CLauncherMe clauncherMe;
			if (num != 1)
			{
				if (num != 2)
				{
					clauncherMe = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalMe>();
					clauncherMe.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
				}
				else
				{
					clauncherMe = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalMe>();
					clauncherMe.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
				}
			}
			else
			{
				clauncherMe = this.cwc.GetMuzzleContainer().AddComponent<CLauncherNormalMe>();
				clauncherMe.initLauncher(this, weaponinfo, point, mainParam, pointFps, pointTps);
			}
			bool flag = this.launchersDict.ContainsKey(clauncherMe.launcherNo);
			if (flag)
			{
				Debug.LogError("已经存在多个发射器(launcherNo)了,名称:" + weaponinfo.name);
			}
			this.launchersDict.Add(clauncherMe.launcherNo, clauncherMe);
			clauncherMe.KeyBorad = weaponinfo.key;
			clauncherMe.KeyGroup = weaponinfo.keygroup;
			clauncherMe.KeyBordFull = weaponinfo.keyfull;
			clauncherMe.recoil = weaponinfo.recoil;
			clauncherMe.recoilMax = weaponinfo.recoilMax;
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x000481BC File Offset: 0x000463BC
	public bool LauncherPublicCD()
	{
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		return clauncherMe.LauncherPublicCD();
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000481EC File Offset: 0x000463EC
	public bool WeaponReadyed()
	{
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		bool flag = clauncherMe == null;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = clauncherMe.bulletPrefab != null;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = clauncherMe.weaponData == null || clauncherMe.weaponData.bullet_type == 0;
				result = flag3;
			}
		}
		return result;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0004825C File Offset: 0x0004645C
	public int WeaponFire(string putKey)
	{
		bool isDead = this.NetObject.isDead;
		int result;
		if (isDead)
		{
			result = 0;
		}
		else
		{
			CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
			Vector3 mubiao = clauncherMe.getMubiao();
			object mubiaoObj = clauncherMe.getMubiaoObj();
			int num = clauncherMe.LauncherFire(putKey, mubiao, mubiaoObj);
			bool bePlayer = this.NetObject.bePlayer;
			if (bePlayer)
			{
				this.NetObject.heroBase.WeaponFired(num == 1);
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x000482E0 File Offset: 0x000464E0
	public int WeaponFire(string putKey, Vector3 targetPoint, object target)
	{
		bool isDead = this.NetObject.isDead;
		int result;
		if (isDead)
		{
			result = 0;
		}
		else
		{
			CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
			result = clauncherMe.LauncherFire(putKey, targetPoint, target);
		}
		return result;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00048328 File Offset: 0x00046528
	public void WeaponActiveBySkill(int _skill)
	{
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		clauncherMe.skillIdByWeapon = _skill;
		clauncherMe.readyCancelDown();
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0004835C File Offset: 0x0004655C
	public int WeaponFull(string putKey)
	{
		bool flag = this.weaponData.launcherFullTime > 0f;
		if (flag)
		{
			bool flag2 = !this.readyFullReadyOk;
			if (flag2)
			{
				Vector3 zero = Vector3.zero;
				object target = null;
				bool flag3 = putKey != this.weaponData.keyfull;
				if (flag3)
				{
					return 0;
				}
				bool flag4 = ((CWeaponControlMe)this.cwc).bulletNow <= 0 && ((CWeaponControlMe)this.cwc).bulletMax != -1;
				if (flag4)
				{
					return 0;
				}
				bool fullIng = this.fullIng;
				if (fullIng)
				{
					return 0;
				}
				bool flag5 = (Fun.GetServerDate() - base.lastShootTime).TotalSeconds < (double)base.oneShootTime;
				if (flag5)
				{
					return 0;
				}
				bool flag6 = !this.cwc.CanFirePublicCD(0f);
				if (flag6)
				{
					return 0;
				}
				bool animatorBool = this.NetObject.GetAnimatorBool("Reload");
				if (animatorBool)
				{
					return 0;
				}
				this.fullIng = true;
				this.cwc.sInputFullKey = putKey;
				((CWeaponControlMe)this.cwc).fullWeapon = this;
				float time = Time.time;
				bool launcherFullZero = this.weaponData.launcherFullZero;
				if (launcherFullZero)
				{
					this.readyFullGoTime = time;
					this.readyFullHaveLongTime = 0f;
				}
				else
				{
					float num = time - this.readyFullStopTime;
					this.readyFullGoTime = time - this.readyFullHaveLongTime + num;
					bool flag7 = this.readyFullGoTime > time;
					if (flag7)
					{
						this.readyFullGoTime = time;
						this.readyFullHaveLongTime = 0f;
					}
				}
				base.StopAllCoroutines();
				base.StartCoroutine(this.readyFire(zero, target));
				return 1;
			}
			else
			{
				bool flag8 = this.weaponData.launcherfullType == 1 || this.weaponData.launcherfullType == 4;
				if (flag8)
				{
					CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
					clauncherMe.showAllFullEffect();
					clauncherMe.FullFire();
					bool flag9 = !this.weaponData.launcherFullZero;
					if (flag9)
					{
						this.readyFullGoTime = Time.time;
						this.readyFullHaveLongTime = 0f;
						clauncherMe.disableAllFullEffect();
					}
					return 1;
				}
			}
		}
		return 0;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000485C4 File Offset: 0x000467C4
	private IEnumerator readyFire(Vector3 targetPoint, object target)
	{
		CLauncherMe lau = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		lau.showAllFullEffect();
		float waittime = this.weaponData.launcherFullTime - this.readyFullHaveLongTime;
		float fulllongtime = this.readyFullHaveLongTime * 1000f;
		ArrayList send = new ArrayList
		{
			this.weaponData.shootname,
			1,
			(int)fulllongtime,
			(int)(this.weaponData.launcherFullTime * 1000f)
		};
		this.NetObject.TosFun("SB_READY", new object[]
		{
			send
		});
		yield return new WaitForSeconds(waittime);
		bool flag = this.weaponData.launcherFullTime > 0f && this.fullIng;
		if (flag)
		{
			this.readyFullReadyOk = true;
		}
		yield break;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000485E4 File Offset: 0x000467E4
	public void WeaponDownKey()
	{
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		clauncherMe.readyCancelDown();
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00048610 File Offset: 0x00046810
	private void FixedUpdate()
	{
		bool flag = this.fullIng && !this.cwc.CanFirePublicCD(0f);
		if (flag)
		{
			this.fullCancel();
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0004864C File Offset: 0x0004684C
	public void fullCancel()
	{
		this.FullEnd();
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		clauncherMe.disableAllFullEffect();
		ArrayList arrayList = new ArrayList
		{
			this.weaponData.shootname,
			0,
			0,
			0
		};
		this.NetObject.TosFun("SB_READY", new object[]
		{
			arrayList
		});
		bool flag = !this.weaponData.launcherFullZero;
		if (flag)
		{
			this.readyFullStopTime = Time.time;
			this.readyFullHaveLongTime = this.readyFullStopTime - this.readyFullGoTime;
		}
		this.readyFullReadyOk = false;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0004871C File Offset: 0x0004691C
	public void FullFireBySkill()
	{
		this.FullEnd();
		((CWeaponControlMe)this.cwc).fullWeapon = null;
		CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
		clauncherMe.disableAllFullEffect();
		ArrayList arrayList = new ArrayList
		{
			this.weaponData.shootname,
			0,
			0,
			0
		};
		this.NetObject.TosFun("SB_READY", new object[]
		{
			arrayList
		});
		bool flag = !this.weaponData.launcherFullZero;
		if (flag)
		{
			this.readyFullStopTime = Time.time;
			this.readyFullHaveLongTime = this.readyFullStopTime - this.readyFullGoTime;
		}
		clauncherMe.FullFireReal();
		this.readyFullReadyOk = false;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00048804 File Offset: 0x00046A04
	public void readyCancel(bool beCancel)
	{
		bool flag = false;
		bool flag2 = this.weaponData.launcherFullTime <= 0f || !this.fullIng;
		if (!flag2)
		{
			if (beCancel)
			{
				this.FullEnd();
				((CWeaponControlMe)this.cwc).fullWeapon = null;
				CLauncherMe clauncherMe = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
				clauncherMe.disableAllFullEffect();
				ArrayList arrayList = new ArrayList
				{
					this.weaponData.shootname,
					0,
					0,
					0
				};
				this.NetObject.TosFun("SB_READY", new object[]
				{
					arrayList
				});
				bool flag3 = !this.weaponData.launcherFullZero;
				if (flag3)
				{
					this.readyFullStopTime = Time.time;
					this.readyFullHaveLongTime = this.readyFullStopTime - this.readyFullGoTime;
				}
				bool flag4 = this.weaponData.launcherfullType != 1;
				if (flag4)
				{
					bool flag5 = this.weaponData.launcherfullType == 2;
					if (flag5)
					{
						bool flag6 = this.readyFullReadyOk;
						if (flag6)
						{
							flag = true;
						}
					}
					else
					{
						bool flag7 = this.weaponData.launcherfullType == 3;
						if (flag7)
						{
							flag = true;
						}
						else
						{
							bool flag8 = this.weaponData.launcherfullType == 4;
							if (flag8)
							{
								flag = true;
							}
						}
					}
				}
				bool flag9 = flag && this.weaponData.key == this.weaponData.keyfull;
				if (flag9)
				{
					clauncherMe.FullFireReal();
				}
				this.readyFullReadyOk = false;
			}
			else
			{
				bool flag10 = this.weaponData.launcherfullType == 2;
				if (flag10)
				{
					bool flag11 = this.readyFullReadyOk;
					if (flag11)
					{
						flag = true;
					}
				}
				else
				{
					bool flag12 = this.weaponData.launcherfullType == 3;
					if (flag12)
					{
						flag = true;
					}
					else
					{
						bool flag13 = this.weaponData.launcherfullType == 4;
						if (flag13)
						{
							flag = true;
						}
					}
				}
				bool flag14 = flag;
				if (flag14)
				{
					this.FullEnd();
					CLauncherMe clauncherMe2 = (CLauncherMe)this.launchersDict[this.defaultLauncherNo];
					clauncherMe2.disableAllFullEffect();
					bool flag15 = !this.weaponData.launcherFullZero;
					if (flag15)
					{
						this.readyFullStopTime = Time.time;
						this.readyFullHaveLongTime = this.readyFullStopTime - this.readyFullGoTime;
					}
					clauncherMe2.FullFireReal();
					this.readyFullReadyOk = false;
					ArrayList arrayList2 = new ArrayList
					{
						this.weaponData.shootname,
						0,
						0,
						0
					};
					this.NetObject.TosFun("SB_READY", new object[]
					{
						arrayList2
					});
				}
			}
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00048B01 File Offset: 0x00046D01
	private void FullEnd()
	{
		this.fullIng = false;
		this.cwc.sInputFullKey = string.Empty;
		((CWeaponControlMe)this.cwc).fullWeapon = null;
	}

	// Token: 0x04000627 RID: 1575
	public float readyFullHaveLongTime;

	// Token: 0x04000628 RID: 1576
	public float readyFullStopTime;

	// Token: 0x04000629 RID: 1577
	public bool readyFullReadyOk;
}

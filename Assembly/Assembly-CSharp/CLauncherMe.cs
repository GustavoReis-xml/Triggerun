using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class CLauncherMe : CLauncher
{
	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000573 RID: 1395 RVA: 0x0002B20C File Offset: 0x0002940C
	// (set) Token: 0x06000574 RID: 1396 RVA: 0x0002B224 File Offset: 0x00029424
	public float[] recoil
	{
		get
		{
			return this._recoil;
		}
		set
		{
			this._recoil = value;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000575 RID: 1397 RVA: 0x0002B230 File Offset: 0x00029430
	// (set) Token: 0x06000576 RID: 1398 RVA: 0x0002B248 File Offset: 0x00029448
	public float[] recoilMax
	{
		get
		{
			return this._recoilMax;
		}
		set
		{
			this._recoilMax = value;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x0002B252 File Offset: 0x00029452
	public bool bulletNumUpdate
	{
		get
		{
			return this._bulletNumUpdate;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000578 RID: 1400 RVA: 0x0002B25C File Offset: 0x0002945C
	// (set) Token: 0x06000579 RID: 1401 RVA: 0x0002B274 File Offset: 0x00029474
	public string KeyBordFull
	{
		get
		{
			return this._KeyBordFull;
		}
		set
		{
			this._KeyBordFull = value;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x0600057A RID: 1402 RVA: 0x0002B280 File Offset: 0x00029480
	// (set) Token: 0x0600057B RID: 1403 RVA: 0x0002B298 File Offset: 0x00029498
	public string KeyBorad
	{
		get
		{
			return this._KeyBorad;
		}
		set
		{
			this._KeyBorad = value;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x0600057C RID: 1404 RVA: 0x0002B2A4 File Offset: 0x000294A4
	// (set) Token: 0x0600057D RID: 1405 RVA: 0x0002B2BC File Offset: 0x000294BC
	public int KeyGroup
	{
		get
		{
			return this._KeyGroup;
		}
		set
		{
			this._KeyGroup = value;
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0002B2C8 File Offset: 0x000294C8
	public override void SendWeaponMsg(int msgType, ArrayList data)
	{
		bool flag = base.NetObject != null && !base.NetObject.haveDestroy;
		if (flag)
		{
			base.NetObject.TosFun("SW", new object[]
			{
				msgType,
				data
			});
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0002B31C File Offset: 0x0002951C
	public void BreakShoot()
	{
		base.launcherContinuousNow = 0;
		foreach (CMuzzle cmuzzle in this._muzzles)
		{
			CMuzzleMe cmuzzleMe = (CMuzzleMe)cmuzzle;
			cmuzzleMe.SetBreakShoot();
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0002B384 File Offset: 0x00029584
	public bool LauncherPublicCD()
	{
		bool flag = !this.weaponData.ignorePubliccd;
		return !flag || base.cwc.CanFirePublicCD(0f);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0002B3C0 File Offset: 0x000295C0
	public int LauncherFire(string putKey, Vector3 targetPoint, object target)
	{
		bool flag = base.bulletNow <= 0 && base.bulletMax != -1;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = base.Seed != base.lastShootTick;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = Time.realtimeSinceStartup - base.lastShootTick < base.oneShootTime;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					bool flag4 = base.launcherContinuousNow > 0;
					if (flag4)
					{
						result = 0;
					}
					else
					{
						bool flag5 = base.bulletPrefab == null;
						if (flag5)
						{
							result = 0;
						}
						else
						{
							bool fullIng = ((CWeaponMe)this.w).fullIng;
							if (fullIng)
							{
								bool flag6 = this.weaponData.launcherfullType != 1 && this.weaponData.launcherfullType != 4;
								if (flag6)
								{
									return 0;
								}
								bool flag7 = !((CWeaponMe)this.w).readyFullReadyOk;
								if (flag7)
								{
									return 0;
								}
							}
							bool flag8 = (Fun.GetServerDate() - base.lastShootTime).TotalSeconds < (double)base.oneShootTime;
							if (flag8)
							{
								result = 0;
							}
							else
							{
								bool launcherHoting = base.launcherHoting;
								if (launcherHoting)
								{
									result = 0;
								}
								else
								{
									bool flag9 = !this.weaponData.ignorePubliccd;
									if (flag9)
									{
										bool flag10 = !base.cwc.CanFirePublicCD(0f);
										if (flag10)
										{
											return 0;
										}
										bool flag11 = !((CWeaponControlMe)base.cwc).CanFirePublicDelay(0f);
										if (flag11)
										{
											return 0;
										}
									}
									bool flag12 = this.weaponData.breakWeaponShootName != 0;
									if (flag12)
									{
										CWeaponMe cweaponMe = (CWeaponMe)base.cwc.weapons[this.weaponData.breakWeaponShootName];
										CLauncherMe clauncherMe = (CLauncherMe)cweaponMe.launchersDict[cweaponMe.defaultLauncherNo];
										clauncherMe.BreakShoot();
									}
									bool breakCartrid = this.weaponData.breakCartrid;
									if (breakCartrid)
									{
										bool fillCartridge = ((CWeaponControlMe)base.cwc).fillCartridge;
										if (fillCartridge)
										{
											base.NetObject.SetAnimator("Reload", 0, false, 1);
											((CWeaponControlMe)base.cwc).cancelFillCartridge();
										}
									}
									else
									{
										bool fillCartridge2 = ((CWeaponControlMe)base.cwc).fillCartridge;
										if (fillCartridge2)
										{
											return 0;
										}
									}
									bool animatorBool = base.NetObject.GetAnimatorBool("Reload");
									if (animatorBool)
									{
										result = 0;
									}
									else
									{
										bool flag13 = base.launcherLockTarget == 1;
										if (flag13)
										{
											this._targetPosition = targetPoint;
											this._targetObject = target;
										}
										bool flag14 = this.weaponData.launcherFullTime > 0f && !((CWeaponMe)this.w).readyFullReadyOk;
										if (flag14)
										{
											result = 0;
										}
										else
										{
											bool flag15 = base.lastShootTick == this.llastshootick;
											if (flag15)
											{
												bool flag16 = CCheckDelay.Instance.weaponHacked != 4;
												if (flag16)
												{
													CCheckDelay.Instance.weaponHacked = 4;
													CCheckDelay.Instance.weaponStr = string.Format("P:{0:F4},{1:F4}", base.lastShootTick, this.llastshootick);
												}
												result = 0;
											}
											else
											{
												result = this.FireReal(targetPoint, target, 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0002B71C File Offset: 0x0002991C
	public void readyCancelDown()
	{
		bool flag = base.launcherHotNow > 0 && !base.launcherHoting && !((CWeaponControlMe)base.cwc).fillCartridge && base.launcherContinuousNow <= 0;
		if (flag)
		{
			this._bCooling = false;
			base.StopAllCoroutines();
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0002B770 File Offset: 0x00029970
	public void FireRealDoWork(Vector3 targetPoint, object target, int isContinuous)
	{
		this.llastshootick = base.lastShootTick;
		base.lastShootTime = Fun.GetServerDate();
		this.bulletSub();
		this.bulletHot();
		((CWeaponControlMe)base.cwc).UpdateDelayTime(0f);
		base.cwc.UpdateFireTime(base.publicCD);
		bool flag = base.launcherContinuousNumber > 0 && !base.NetObject.isDead && base.NetObject.playerCommon.m_nDongjie != 1 && isContinuous <= 0;
		if (flag)
		{
			base.launcherContinuousNow = base.launcherContinuousNumber;
			base.StartCoroutine(this.LauncherFireContinue(targetPoint, target));
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0002B821 File Offset: 0x00029A21
	private IEnumerator LauncherFireContinue(Vector3 targetPoint, object target)
	{
		while (base.launcherContinuousNow > 0)
		{
			yield return 0;
			bool flag = ((CWeaponControlMe)base.cwc).fillCartridge && base.launcherContinuousNow > 5;
			if (flag)
			{
				Debug.Log("fillCartridge:" + base.launcherContinuousNow.ToString());
				base.launcherContinuousNow = 0;
				break;
			}
			bool flag2 = (base.bulletNow <= 0 && base.bulletMax != -1) || base.NetObject.isDead || base.NetObject.playerCommon.m_nDongjie == 1 || base.NetObject.playerCommon.m_nHunMi == 1;
			if (flag2)
			{
				base.launcherContinuousNow = 0;
				break;
			}
			bool flag3 = (Fun.GetServerDate() - base.lastShootTime).TotalSeconds < (double)base.oneContinuousShootTime;
			if (flag3)
			{
				yield return 0;
			}
			else
			{
				bool flag4 = this.FireReal(targetPoint, target, base.launcherContinuousNow) == 1;
				if (flag4)
				{
					int launcherContinuousNow = base.launcherContinuousNow;
					base.launcherContinuousNow = launcherContinuousNow - 1;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0002B840 File Offset: 0x00029A40
	public int FullFire()
	{
		int num = this.LauncherFire(string.Empty, this._targetPosition, this._targetObject);
		bool bePlayer = base.NetObject.bePlayer;
		if (bePlayer)
		{
			((HeroPlayer)base.NetObject).heroBase.WeaponFired(num == 1);
		}
		return num;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0002B898 File Offset: 0x00029A98
	public int FullFireReal()
	{
		int num = this.FireReal(this._targetPosition, this._targetObject, 0);
		bool bePlayer = base.NetObject.bePlayer;
		if (bePlayer)
		{
			((HeroPlayer)base.NetObject).heroBase.WeaponFired(num == 1);
		}
		return num;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0002B8EC File Offset: 0x00029AEC
	private int FireReal(Vector3 targetPoint, object target, int isContinuous)
	{
		CMuzzleMe cmuzzleMe = (CMuzzleMe)base.muzzle;
		bool flag = base.launcherDelayTime > 0f && base.delayType == 0 && Time.realtimeSinceStartup > base.lastShootTick + base.oneShootTime + 0.1f;
		if (flag)
		{
			base.needDelay = true;
		}
		int num = cmuzzleMe.Fire(targetPoint, target, isContinuous);
		bool flag2 = num == 1;
		int result;
		if (flag2)
		{
			base.FireTalk();
			bool flag3 = this.w.weaponData.cartridgeCD > 0;
			if (flag3)
			{
				((CWeaponControlMe)base.cwc).cartridgeAddCDTime(this.w.weaponData.cartridgeCD);
			}
			base.NetObject.SetAnimator(cmuzzleMe.animatorWeapon.animationName, 2, cmuzzleMe.animatorWeapon.animationNo, 1);
			CameraControl.faRecoil = this.recoil;
			CameraControl.faRecoilMax = this.recoilMax;
			bool flag4 = this.weaponData.fillCartridShootName > 0;
			if (flag4)
			{
				((CWeaponControlMe)base.cwc).CheckFillCartridgeForWeaponName(this.weaponData.fillCartridShootName, this.weaponData.cartridgeCD);
			}
			bool flag5 = base.delayWork != 0;
			if (flag5)
			{
				this.FireRealDoWork(targetPoint, target, isContinuous);
			}
			result = 1;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0002BA42 File Offset: 0x00029C42
	public void BulletNumberUpdated()
	{
		this._bulletNumUpdate = false;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0002BA4C File Offset: 0x00029C4C
	protected override void bulletSub()
	{
		bool flag = base.bulletMax != -1;
		if (flag)
		{
			base.nowBulletNumberBefore = base.bulletNow;
			base.bulletNow -= base.useBullet;
			this._bulletNumUpdate = true;
			bool flag2 = base.bulletNow <= 0 && !((CWeaponControlMe)base.cwc).fillCartridge;
			if (flag2)
			{
				base.bulletNow = 0;
				((CWeaponControlMe)base.cwc).DoCartridge();
			}
		}
		else
		{
			base.cwc.nowBulletNumberBeforeForInfinite--;
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0002BAEC File Offset: 0x00029CEC
	public override void bulletHot()
	{
		bool flag = base.launcherHotAdd == 0 || base.launcherHoting;
		if (!flag)
		{
			base.launcherHotNow += base.launcherHotAdd;
			bool flag2 = base.launcherHotNow > base.launcherHotMax;
			if (flag2)
			{
				base.launcherHotNow = base.launcherHotMax;
				base.launcherHoting = true;
				bool flag3 = !this._bCooling;
				if (flag3)
				{
					base.StartCoroutine(this.Cooling());
				}
			}
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002BB6B File Offset: 0x00029D6B
	private IEnumerator Cooling()
	{
		this._bCooling = true;
		base.launcherHotLastTime = Time.time;
		while (base.launcherHotNow > 0)
		{
			yield return new WaitForSeconds(0.1f);
			float t = Time.time - base.launcherHotLastTime;
			int sub = (int)((float)base.launcherHotSubTime * t);
			base.launcherHotNow -= sub;
		}
		this._bCooling = false;
		base.launcherHotNow = 0;
		bool launcherHoting = base.launcherHoting;
		if (launcherHoting)
		{
			base.launcherHoting = false;
		}
		yield break;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0002BB7C File Offset: 0x00029D7C
	public Vector3 getMubiao()
	{
		return base.NetObject.getMubiao(base.layerType);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0002BBA0 File Offset: 0x00029DA0
	public Vector3 getMubiaoUp()
	{
		return base.NetObject.getMubiaoUp;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0002BBC0 File Offset: 0x00029DC0
	public Vector3 getCameraDir()
	{
		return base.NetObject.camTarget.forward;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0002BBE4 File Offset: 0x00029DE4
	public GameObject getMubiaoObj()
	{
		return base.NetObject.getMubiaoObj(base.layerType, false);
	}

	// Token: 0x04000428 RID: 1064
	private Vector3 _targetPosition = Vector3.zero;

	// Token: 0x04000429 RID: 1065
	private object _targetObject;

	// Token: 0x0400042A RID: 1066
	private bool _bCooling;

	// Token: 0x0400042B RID: 1067
	private bool _bulletNumUpdate;

	// Token: 0x0400042C RID: 1068
	private float[] _recoil;

	// Token: 0x0400042D RID: 1069
	private float[] _recoilMax;

	// Token: 0x0400042E RID: 1070
	private string _KeyBordFull;

	// Token: 0x0400042F RID: 1071
	private string _KeyBorad;

	// Token: 0x04000430 RID: 1072
	private int _KeyGroup;

	// Token: 0x04000431 RID: 1073
	private float llastshootick;
}

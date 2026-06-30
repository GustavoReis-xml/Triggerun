using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class CLauncher : MonoBehaviour
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x00029E23 File Offset: 0x00028023
	public int hitrateAdd
	{
		get
		{
			return this._hitrateAdd;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x0600051D RID: 1309 RVA: 0x00029E2B File Offset: 0x0002802B
	public int hitrateType
	{
		get
		{
			return this._hitrateType;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x00029E33 File Offset: 0x00028033
	public float gemSpd
	{
		get
		{
			return this._gemSpd;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x0600051F RID: 1311 RVA: 0x00029E3B File Offset: 0x0002803B
	public float gemBoomTime
	{
		get
		{
			return this._gemBoomTime;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000520 RID: 1312 RVA: 0x00029E43 File Offset: 0x00028043
	public float gemDis
	{
		get
		{
			return this._gemDis;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000521 RID: 1313 RVA: 0x00029E4B File Offset: 0x0002804B
	public int hitrateMax
	{
		get
		{
			return this._hitrateMax;
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x00029E53 File Offset: 0x00028053
	public int forcePlace
	{
		get
		{
			return this._forcePlace;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000523 RID: 1315 RVA: 0x00029E5B File Offset: 0x0002805B
	public int forceSource
	{
		get
		{
			return this._forceSource;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x00029E63 File Offset: 0x00028063
	public int layerType
	{
		get
		{
			return this._layerType;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000525 RID: 1317 RVA: 0x00029E6C File Offset: 0x0002806C
	// (set) Token: 0x06000526 RID: 1318 RVA: 0x00029E84 File Offset: 0x00028084
	public int damageLayerType
	{
		get
		{
			return this._damageLayerType;
		}
		set
		{
			this._damageLayerType = value;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06000527 RID: 1319 RVA: 0x00029E90 File Offset: 0x00028090
	// (set) Token: 0x06000528 RID: 1320 RVA: 0x00029EA8 File Offset: 0x000280A8
	public int bulletTypeNumber
	{
		get
		{
			return this._bulletTypeNumber;
		}
		set
		{
			this._bulletTypeNumber = value;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000529 RID: 1321 RVA: 0x00029EB2 File Offset: 0x000280B2
	public int buffRandom
	{
		get
		{
			return this._buffRandom;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x0600052A RID: 1322 RVA: 0x00029EBA File Offset: 0x000280BA
	public CWeaponControl cwc
	{
		get
		{
			return this.w.cwc;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x0600052B RID: 1323 RVA: 0x00029EC7 File Offset: 0x000280C7
	public float publicCD
	{
		get
		{
			return this._publicCD;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x0600052C RID: 1324 RVA: 0x00029ED0 File Offset: 0x000280D0
	// (set) Token: 0x0600052D RID: 1325 RVA: 0x00029EE8 File Offset: 0x000280E8
	public float launcherHotLastTime
	{
		get
		{
			return this._launcherHotLastTime;
		}
		set
		{
			this._launcherHotLastTime = value;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x0600052E RID: 1326 RVA: 0x00029EF2 File Offset: 0x000280F2
	public int launcherName
	{
		get
		{
			return this.weaponData.shootname;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x0600052F RID: 1327 RVA: 0x00029F00 File Offset: 0x00028100
	public UnityEngine.Object bulletPrefab
	{
		get
		{
			bool flag = this.NetObject.FpsOrTps == 1;
			UnityEngine.Object result;
			if (flag)
			{
				result = this._bulletPrefabFps;
			}
			else
			{
				result = this._bulletPrefabTps;
			}
			return result;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x00029F34 File Offset: 0x00028134
	public UnityEngine.Object bulletPrefabDelay
	{
		get
		{
			bool flag = this.NetObject.FpsOrTps == 1;
			UnityEngine.Object result;
			if (flag)
			{
				result = this._bulletPrefabDelayFps;
			}
			else
			{
				result = this._bulletPrefabDelayTps;
			}
			return result;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000531 RID: 1329 RVA: 0x00029F68 File Offset: 0x00028168
	public NetLiving NetObject
	{
		get
		{
			return this.w.NetObject;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x00029F75 File Offset: 0x00028175
	public int launcherNo
	{
		get
		{
			return this._launcherNo;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x00029F80 File Offset: 0x00028180
	// (set) Token: 0x06000534 RID: 1332 RVA: 0x00029F98 File Offset: 0x00028198
	public int launcherLockTarget
	{
		get
		{
			return this._launcherLockTarget;
		}
		set
		{
			this._launcherLockTarget = value;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x00029FA2 File Offset: 0x000281A2
	public float launcherDelayTime
	{
		get
		{
			return this._launcherDelayTime;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x00029FAA File Offset: 0x000281AA
	public float oneShootTime
	{
		get
		{
			return this.w.oneShootTime;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x00029FB7 File Offset: 0x000281B7
	public int oneShootTick
	{
		get
		{
			return this.w.oneShootTick;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000538 RID: 1336 RVA: 0x00029FC4 File Offset: 0x000281C4
	// (set) Token: 0x06000539 RID: 1337 RVA: 0x00029FDC File Offset: 0x000281DC
	public float oneContinuousShootTime
	{
		get
		{
			return this._oneContinuousShootTime;
		}
		set
		{
			this._oneContinuousShootTime = value;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x0600053A RID: 1338 RVA: 0x00029FE6 File Offset: 0x000281E6
	protected float Seed
	{
		get
		{
			return this.w.seed;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x0600053B RID: 1339 RVA: 0x00029FF4 File Offset: 0x000281F4
	// (set) Token: 0x0600053C RID: 1340 RVA: 0x0002A011 File Offset: 0x00028211
	public DateTime lastShootTime
	{
		get
		{
			return this.w.lastShootTime;
		}
		set
		{
			this.w.lastShootTime = value;
			this.w.lastShootTick = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x0002A032 File Offset: 0x00028232
	public float lastShootTick
	{
		get
		{
			return this.w.lastShootTick;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002A040 File Offset: 0x00028240
	// (set) Token: 0x0600053F RID: 1343 RVA: 0x0002A058 File Offset: 0x00028258
	public bool needDelay
	{
		get
		{
			return this._needDelay;
		}
		set
		{
			this._needDelay = value;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000540 RID: 1344 RVA: 0x0002A062 File Offset: 0x00028262
	public int delayType
	{
		get
		{
			return this._delayType;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000541 RID: 1345 RVA: 0x0002A06A File Offset: 0x0002826A
	public int delayWork
	{
		get
		{
			return this._delayWork;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x0002A072 File Offset: 0x00028272
	public int launcherHotMax
	{
		get
		{
			return this._launcherHotMax;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000543 RID: 1347 RVA: 0x0002A07A File Offset: 0x0002827A
	public int launcherHotAdd
	{
		get
		{
			return this._launcherHotAdd;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x0002A082 File Offset: 0x00028282
	public int launcherHotSubTime
	{
		get
		{
			return this._launcherHotSubTime;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002A08C File Offset: 0x0002828C
	// (set) Token: 0x06000546 RID: 1350 RVA: 0x0002A0A4 File Offset: 0x000282A4
	public int launcherHotNow
	{
		get
		{
			return this._launcherHotNow;
		}
		set
		{
			this._launcherHotNow = value;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002A0B0 File Offset: 0x000282B0
	// (set) Token: 0x06000548 RID: 1352 RVA: 0x0002A0C8 File Offset: 0x000282C8
	public bool launcherHoting
	{
		get
		{
			return this._launcherHoting;
		}
		set
		{
			this._launcherHoting = value;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002A0D2 File Offset: 0x000282D2
	public int[] buff
	{
		get
		{
			return this._buff;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002A0DA File Offset: 0x000282DA
	public int launcherContinuousNumber
	{
		get
		{
			return this._launcherContinuousNumber;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600054B RID: 1355 RVA: 0x0002A0E4 File Offset: 0x000282E4
	// (set) Token: 0x0600054C RID: 1356 RVA: 0x0002A0FC File Offset: 0x000282FC
	public int launcherContinuousNow
	{
		get
		{
			return this._launcherContinuousNow;
		}
		set
		{
			this._launcherContinuousNow = value;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002A108 File Offset: 0x00028308
	public int CartridgePublicID
	{
		get
		{
			bool flag = this.cartridgeNo == -1;
			int result;
			if (flag)
			{
				result = this.cartridgeNo;
			}
			else
			{
				result = this.cwc.cartridgePublicID[this.cartridgeNo];
			}
			return result;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x0600054E RID: 1358 RVA: 0x0002A144 File Offset: 0x00028344
	// (set) Token: 0x0600054F RID: 1359 RVA: 0x0002A178 File Offset: 0x00028378
	public int nowBulletNumberBefore
	{
		get
		{
			bool flag = this.cartridgeNo == -1;
			int result;
			if (flag)
			{
				result = this.cwc.nowBulletNumberBeforeForInfinite;
			}
			else
			{
				result = this._nowBulletNumberBefore;
			}
			return result;
		}
		set
		{
			this._nowBulletNumberBefore = value;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000550 RID: 1360 RVA: 0x0002A184 File Offset: 0x00028384
	// (set) Token: 0x06000551 RID: 1361 RVA: 0x0002A1BF File Offset: 0x000283BF
	public int bulletNow
	{
		get
		{
			bool flag = this.cartridgeNo == -1;
			int result;
			if (flag)
			{
				result = this.cartridgeNo;
			}
			else
			{
				result = this.cwc.cartridgePublicNow[this.cartridgeNo];
			}
			return result;
		}
		set
		{
			this.cwc.cartridgePublicNow[this.cartridgeNo] = value;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000552 RID: 1362 RVA: 0x0002A1D8 File Offset: 0x000283D8
	public int bulletMax
	{
		get
		{
			bool flag = this.cartridgeNo == -1;
			int result;
			if (flag)
			{
				result = this.cartridgeNo;
			}
			else
			{
				result = this.cwc.cartridgePublicMax[this.cartridgeNo];
			}
			return result;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002A214 File Offset: 0x00028414
	public CMuzzle muzzle
	{
		get
		{
			this.muzzleNo++;
			this.muzzleNo %= this._muzzles.Count;
			return this._muzzles[this.muzzleNo];
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000554 RID: 1364 RVA: 0x0002A25D File Offset: 0x0002845D
	public int cartridgeNo
	{
		get
		{
			return this.weaponData.cartridgeNo;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000555 RID: 1365 RVA: 0x0002A26A File Offset: 0x0002846A
	public int useBullet
	{
		get
		{
			return this._useBullet;
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0002A274 File Offset: 0x00028474
	public void replaceMuzzle(Transform[] point)
	{
		for (int i = 0; i < point.Length; i++)
		{
			bool flag = !this._muzzlesDict.ContainsKey(point[i].name);
			if (flag)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"程序注意:",
					this.NetObject.thisModel.name,
					"更换枪口的时候名字不存在",
					point[i].name,
					"."
				}));
			}
			else
			{
				this._muzzlesDict[point[i].name].replaceMuzzle(point[i]);
			}
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0002A320 File Offset: 0x00028520
	public virtual void initLauncher(CWeapon weaponcon, XMLWeapon wInfo, Transform[] point, ArrayList mainParam, Transform[] pointFps, Transform[] pointTps)
	{
		this.weaponData = wInfo;
		this.w = weaponcon;
		this.muzzleNo = 0;
		this._bulletTypeNumber = wInfo.bullet_type;
		this._launcherNo = (int)mainParam[0];
		this._launcherHotMax = (int)mainParam[7];
		this._launcherHotAdd = (int)mainParam[8];
		this._launcherHotSubTime = (int)mainParam[9];
		this._launcherHotNow = 0;
		this.talkSoundName = (string[])mainParam[11];
		this.talkSoundRand = (int)mainParam[12];
		this.deadSoundName = (string[])mainParam[13];
		this._launcherLockTarget = (int)mainParam[15];
		this._launcherDelayTime = (float)((int)mainParam[16]) / 1000f;
		bool flag = this._launcherDelayTime > 0f;
		if (flag)
		{
			this._needDelay = true;
		}
		this._delayType = (int)mainParam[47];
		this._delayWork = (int)mainParam[48];
		this.launcherTags = (ArrayList)mainParam[18];
		this.specialMaterial = new Dictionary<string, int>();
		for (int i = 0; i < this.launcherTags.Count; i++)
		{
			int num = (int)this.launcherTags[i];
			MaterialTag materialTag = fGameManager.mloadConfig.AllMaterialObj[num];
			this.specialMaterial.Add(materialTag.sTag, num);
		}
		this._buff = (int[])mainParam[19];
		this._publicCD = (float)((int)mainParam[20]) / 1000f;
		this._buffRandom = (int)mainParam[23];
		this._launcherContinuousNumber = (int)mainParam[24];
		bool flag2 = (int)mainParam[46] > 0;
		if (flag2)
		{
			this._oneContinuousShootTime = this.w.maxShootRate / (float)((int)mainParam[46]);
		}
		this._launcherContinuousNow = 0;
		this._hitrateAdd = (int)mainParam[25];
		this._hitrateMax = (int)mainParam[26];
		bool flag3 = this.cwc.hitRateMax < (float)this._hitrateMax;
		if (flag3)
		{
			this.cwc.hitRateMax = (float)this._hitrateMax;
		}
		this._forcePlace = (int)mainParam[31];
		this._forceSource = (int)mainParam[38];
		this._layerType = (int)mainParam[33];
		this._damageLayerType = (int)mainParam[34];
		this._useBullet = (int)mainParam[37];
		this._hitrateType = (int)mainParam[39];
		this.damagehittalk_friend = (string)mainParam[53];
		this.damagehittalk_enemy = (string)mainParam[54];
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0002A654 File Offset: 0x00028854
	protected void checkLoadModel(ArrayList mainParam)
	{
		string text = (string)mainParam[10];
		bool flag = text != "0";
		if (flag)
		{
			string url = "EffectRes/" + text + ".ef";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadBulletFpsModel), text);
			}
			else
			{
				this._bulletPrefabFps = resWWW.assetBundle.LoadAsset(resWWW.assetBundle.GetAllAssetNames()[0]);
			}
		}
		text = (string)mainParam[43];
		bool flag3 = text != "0";
		if (flag3)
		{
			string url2 = "EffectRes/" + text + ".ef";
			WWW resWWW2 = GameRoot.GetResWWW(url2);
			bool flag4 = resWWW2 == null;
			if (flag4)
			{
				GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.loadBulletTpsModel), text);
			}
			else
			{
				this._bulletPrefabTps = resWWW2.assetBundle.LoadAsset(resWWW2.assetBundle.GetAllAssetNames()[0]);
			}
		}
		text = (string)mainParam[44];
		bool flag5 = text != "0";
		if (flag5)
		{
			string url3 = "EffectRes/" + text + ".ef";
			WWW resWWW3 = GameRoot.GetResWWW(url3);
			bool flag6 = resWWW3 == null;
			if (flag6)
			{
				GameRoot.LoadAsset(url3, new fResourceManager.DownFinishDelegate(this.loadBulletWarheadFps), text);
			}
			else
			{
				this._bulletPrefabDelayFps = resWWW3.assetBundle.LoadAsset(resWWW3.assetBundle.GetAllAssetNames()[0]);
			}
		}
		text = (string)mainParam[45];
		bool flag7 = text != "0";
		if (flag7)
		{
			string url4 = "EffectRes/" + text + ".ef";
			WWW resWWW4 = GameRoot.GetResWWW(url4);
			bool flag8 = resWWW4 == null;
			if (flag8)
			{
				GameRoot.LoadAsset(url4, new fResourceManager.DownFinishDelegate(this.loadBulletWarheadTps), text);
			}
			else
			{
				this._bulletPrefabDelayTps = resWWW4.assetBundle.LoadAsset(resWWW4.assetBundle.GetAllAssetNames()[0]);
			}
		}
		this.talkSounds = new ArrayList();
		bool flag9 = this.talkSoundName.Length != 0;
		if (flag9)
		{
			for (int i = 0; i < this.talkSoundName.Length; i++)
			{
				string url5 = "SoundRes/" + this.talkSoundName[i] + ".sds";
				WWW resWWW5 = GameRoot.GetResWWW(url5);
				bool flag10 = resWWW5 == null;
				if (flag10)
				{
					GameRoot.LoadAsset(url5, new fResourceManager.DownFinishDelegate(this.loadTalkSoundPrefab), i);
				}
				else
				{
					this.loadTalkSoundPrefab(resWWW5, i);
				}
			}
		}
		this.deadSounds = new ArrayList();
		bool flag11 = this.deadSoundName.Length != 0;
		if (flag11)
		{
			for (int j = 0; j < this.deadSoundName.Length; j++)
			{
				string url6 = "SoundRes/" + this.deadSoundName[j] + ".sds";
				WWW resWWW6 = GameRoot.GetResWWW(url6);
				bool flag12 = resWWW6 == null;
				if (flag12)
				{
					GameRoot.LoadAsset(url6, new fResourceManager.DownFinishDelegate(this.loadDeadSoundPrefab), j);
				}
				else
				{
					this.loadDeadSoundPrefab(resWWW6, j);
				}
			}
		}
		int num = (this.NetObject != null) ? ((CFilmManager.Instance != null) ? CFilmManager.Instance.filmForce(this.NetObject) : ((this.NetObject.playerCommon != null) ? this.NetObject.playerCommon.m_nShiliServer : 0)) : 0;
		bool flag13 = this.NetObject != null && this.NetObject.playerCommon != null && num == this.NetObject.playerCommon.m_nShiliServer;
		bool flag14 = flag13 && this.damagehittalk_friend != "0";
		if (flag14)
		{
			string url7 = "SoundRes/" + this.damagehittalk_friend + ".sds";
			WWW resWWW7 = GameRoot.GetResWWW(url7);
			bool flag15 = resWWW7 == null;
			if (flag15)
			{
				GameRoot.LoadAsset(url7, new fResourceManager.DownFinishDelegate(this.loadDamageTalkPrefab), 0);
			}
			else
			{
				this.loadDamageTalkPrefab(resWWW7, 0);
			}
		}
		bool flag16 = !flag13 && this.damagehittalk_enemy != "0";
		if (flag16)
		{
			string url8 = "SoundRes/" + this.damagehittalk_enemy + ".sds";
			WWW resWWW8 = GameRoot.GetResWWW(url8);
			bool flag17 = resWWW8 == null;
			if (flag17)
			{
				GameRoot.LoadAsset(url8, new fResourceManager.DownFinishDelegate(this.loadDamageTalkPrefab), 0);
			}
			else
			{
				this.loadDamageTalkPrefab(resWWW8, 0);
			}
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0002AB0C File Offset: 0x00028D0C
	private void loadBulletWarheadFps(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this._bulletPrefabDelayFps = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0002AB50 File Offset: 0x00028D50
	private void loadBulletWarheadTps(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this._bulletPrefabDelayTps = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0002AB94 File Offset: 0x00028D94
	private void loadBulletFpsModel(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this._bulletPrefabFps = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0002ABD8 File Offset: 0x00028DD8
	private void loadBulletTpsModel(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this._bulletPrefabTps = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0002AC1C File Offset: 0x00028E1C
	private void loadTalkSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.NetObject != null && !this.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.NetObject._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.talkSounds.Add(component);
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.NetObject);
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0002ACE0 File Offset: 0x00028EE0
	public void FireTalk()
	{
		bool flag = this.talkSounds.Count > 0 && UnityEngine.Random.Range(0f, 10000f) <= (float)this.talkSoundRand;
		if (flag)
		{
			int index = UnityEngine.Random.Range(0, this.talkSounds.Count);
			AudioSource audioSource = (AudioSource)this.talkSounds[index];
			bool flag2 = !audioSource.isPlaying;
			if (flag2)
			{
				audioSource.Play();
				this.NetObject.playerSound.ExeptStopTalkSounds(audioSource);
			}
		}
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0002AD6C File Offset: 0x00028F6C
	private void loadDeadSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.NetObject != null && !this.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.NetObject._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.deadSounds.Add(component);
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.NetObject);
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0002AE30 File Offset: 0x00029030
	public void DeadTalk()
	{
		bool flag = this.deadSounds.Count > 0;
		if (flag)
		{
			int index = UnityEngine.Random.Range(0, this.deadSounds.Count);
			AudioSource audioSource = (AudioSource)this.deadSounds[index];
			bool flag2 = !audioSource.isPlaying;
			if (flag2)
			{
				audioSource.Play();
				this.NetObject.playerSound.ExeptStopTalkSounds(audioSource);
			}
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0002AEA0 File Offset: 0x000290A0
	private void loadDamageTalkPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.NetObject != null && !this.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.NetObject._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.hitTalkAudio = component;
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.NetObject);
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0002AF5C File Offset: 0x0002915C
	public void PlayDamageTalk()
	{
		bool flag = !(this.hitTalkAudio == null) && !this.hitTalkAudio.isPlaying;
		if (flag)
		{
			this.hitTalkAudio.Play();
			this.NetObject.playerSound.ExeptStopTalkSounds(this.hitTalkAudio);
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0002AFB2 File Offset: 0x000291B2
	protected virtual void bulletSub()
	{
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0002AFB5 File Offset: 0x000291B5
	public virtual void bulletHot()
	{
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0002AFB8 File Offset: 0x000291B8
	public void showAllFullEffect()
	{
		for (int i = 0; i < this._muzzles.Count; i++)
		{
			this._muzzles[i].playFullEffect();
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0002AFF4 File Offset: 0x000291F4
	public void disableAllFullEffect()
	{
		for (int i = 0; i < this._muzzles.Count; i++)
		{
			this._muzzles[i].disableFullEffect();
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0002B030 File Offset: 0x00029230
	public void StopAllFireEffect()
	{
		for (int i = 0; i < this._muzzles.Count; i++)
		{
			this._muzzles[i].cancelFireEffect();
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0002B06C File Offset: 0x0002926C
	public virtual void SendWeaponMsg(int msgType, ArrayList data)
	{
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0002B06F File Offset: 0x0002926F
	protected virtual void Update()
	{
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0002B072 File Offset: 0x00029272
	public void InitGemSpd(int v)
	{
		this._gemSpd = 1f + (float)v * 0.0001f;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0002B089 File Offset: 0x00029289
	public void InitGemBoomTime(int v)
	{
		this._gemBoomTime = 1f - (float)v * 0.0001f;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0002B0A0 File Offset: 0x000292A0
	public void InitGemDis(int v)
	{
		this._gemDis = 1f + (float)v * 0.0001f;
	}

	// Token: 0x040003F4 RID: 1012
	public CWeapon w;

	// Token: 0x040003F5 RID: 1013
	public XMLWeapon weaponData;

	// Token: 0x040003F6 RID: 1014
	private int[] _buff;

	// Token: 0x040003F7 RID: 1015
	private int _buffRandom;

	// Token: 0x040003F8 RID: 1016
	private int _launcherContinuousNumber;

	// Token: 0x040003F9 RID: 1017
	private int _launcherContinuousNow;

	// Token: 0x040003FA RID: 1018
	private int _hitrateAdd;

	// Token: 0x040003FB RID: 1019
	private int _hitrateType;

	// Token: 0x040003FC RID: 1020
	private float _gemSpd = 1f;

	// Token: 0x040003FD RID: 1021
	private float _gemBoomTime = 1f;

	// Token: 0x040003FE RID: 1022
	private float _gemDis = 1f;

	// Token: 0x040003FF RID: 1023
	private int _hitrateMax;

	// Token: 0x04000400 RID: 1024
	private int _forcePlace;

	// Token: 0x04000401 RID: 1025
	private int _forceSource;

	// Token: 0x04000402 RID: 1026
	private bool _launcherFullZero;

	// Token: 0x04000403 RID: 1027
	private int _launcherLockTarget;

	// Token: 0x04000404 RID: 1028
	private float _launcherDelayTime;

	// Token: 0x04000405 RID: 1029
	private bool _needDelay;

	// Token: 0x04000406 RID: 1030
	private int _delayType;

	// Token: 0x04000407 RID: 1031
	private int _delayWork;

	// Token: 0x04000408 RID: 1032
	private float _oneContinuousShootTime = 1f;

	// Token: 0x04000409 RID: 1033
	private int _useBullet = 1;

	// Token: 0x0400040A RID: 1034
	private int _launcherHotMax;

	// Token: 0x0400040B RID: 1035
	private int _launcherHotAdd;

	// Token: 0x0400040C RID: 1036
	private int _launcherHotSubTime;

	// Token: 0x0400040D RID: 1037
	private int _launcherHotNow;

	// Token: 0x0400040E RID: 1038
	private bool _launcherHoting;

	// Token: 0x0400040F RID: 1039
	private float _launcherHotLastTime;

	// Token: 0x04000410 RID: 1040
	private string[] talkSoundName;

	// Token: 0x04000411 RID: 1041
	private ArrayList talkSounds;

	// Token: 0x04000412 RID: 1042
	private int talkSoundRand;

	// Token: 0x04000413 RID: 1043
	private string[] deadSoundName;

	// Token: 0x04000414 RID: 1044
	private ArrayList deadSounds;

	// Token: 0x04000415 RID: 1045
	private string damagehittalk_friend;

	// Token: 0x04000416 RID: 1046
	private string damagehittalk_enemy;

	// Token: 0x04000417 RID: 1047
	private AudioSource hitTalkAudio;

	// Token: 0x04000418 RID: 1048
	public ArrayList launcherTags;

	// Token: 0x04000419 RID: 1049
	public Dictionary<string, int> specialMaterial;

	// Token: 0x0400041A RID: 1050
	public List<CMuzzle> _muzzles;

	// Token: 0x0400041B RID: 1051
	public Dictionary<string, CMuzzle> _muzzlesDict = new Dictionary<string, CMuzzle>();

	// Token: 0x0400041C RID: 1052
	private int muzzleNo;

	// Token: 0x0400041D RID: 1053
	private UnityEngine.Object _bulletPrefabFps;

	// Token: 0x0400041E RID: 1054
	private UnityEngine.Object _bulletPrefabTps;

	// Token: 0x0400041F RID: 1055
	private UnityEngine.Object _bulletPrefabDelayFps;

	// Token: 0x04000420 RID: 1056
	private UnityEngine.Object _bulletPrefabDelayTps;

	// Token: 0x04000421 RID: 1057
	private float _publicCD;

	// Token: 0x04000422 RID: 1058
	private int _layerType;

	// Token: 0x04000423 RID: 1059
	private int _damageLayerType;

	// Token: 0x04000424 RID: 1060
	private int _bulletTypeNumber;

	// Token: 0x04000425 RID: 1061
	private int _launcherNo;

	// Token: 0x04000426 RID: 1062
	public int skillIdByWeapon;

	// Token: 0x04000427 RID: 1063
	private int _nowBulletNumberBefore = -1;
}

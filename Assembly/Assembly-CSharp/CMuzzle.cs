using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class CMuzzle : MonoBehaviour
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060005AE RID: 1454 RVA: 0x0002D624 File Offset: 0x0002B824
	// (set) Token: 0x060005AF RID: 1455 RVA: 0x0002D63C File Offset: 0x0002B83C
	public NetLiving targetNet
	{
		get
		{
			return this._targetNet;
		}
		set
		{
			this._targetNet = value;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0002D648 File Offset: 0x0002B848
	// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0002D660 File Offset: 0x0002B860
	public Transform muzzleTransform
	{
		get
		{
			return this._muzzleTransform;
		}
		set
		{
			this._muzzleTransform = value;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0002D66C File Offset: 0x0002B86C
	// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0002D684 File Offset: 0x0002B884
	public bool fireModelRotate
	{
		get
		{
			return this._fireModelRotate;
		}
		set
		{
			this._fireModelRotate = value;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0002D690 File Offset: 0x0002B890
	// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0002D6A8 File Offset: 0x0002B8A8
	public string muzzleName
	{
		get
		{
			return this._muzzleName;
		}
		set
		{
			this._muzzleName = value;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
	// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0002D6CC File Offset: 0x0002B8CC
	public Vector3 bulletBornPoint
	{
		get
		{
			return this._bulletBornPoint;
		}
		set
		{
			this._bulletBornPoint = value;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
	// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0002D6F0 File Offset: 0x0002B8F0
	public GameObject targetObject
	{
		get
		{
			return this._targetObject;
		}
		set
		{
			this._targetObject = value;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x060005BA RID: 1466 RVA: 0x0002D6FA File Offset: 0x0002B8FA
	public UnityEngine.Object bulletPrefab
	{
		get
		{
			return this.launcher.bulletPrefab;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060005BB RID: 1467 RVA: 0x0002D707 File Offset: 0x0002B907
	public UnityEngine.Object bulletPrefabDelay
	{
		get
		{
			return this.launcher.bulletPrefabDelay;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060005BC RID: 1468 RVA: 0x0002D714 File Offset: 0x0002B914
	// (set) Token: 0x060005BD RID: 1469 RVA: 0x0002D72C File Offset: 0x0002B92C
	public CBullet bullet
	{
		get
		{
			return this._bullet;
		}
		set
		{
			this._bullet = value;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060005BE RID: 1470 RVA: 0x0002D738 File Offset: 0x0002B938
	// (set) Token: 0x060005BF RID: 1471 RVA: 0x0002D750 File Offset: 0x0002B950
	public CBulletData data
	{
		get
		{
			return this._data;
		}
		set
		{
			this._data = value;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002D75C File Offset: 0x0002B95C
	// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0002D774 File Offset: 0x0002B974
	public CInfoAnimatorWeapon animatorWeapon
	{
		get
		{
			return this._animatorWeapon;
		}
		set
		{
			this._animatorWeapon = value;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002D77E File Offset: 0x0002B97E
	public Transform muzzleTargetLine
	{
		get
		{
			return this._muzzleTargetLine;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0002D786 File Offset: 0x0002B986
	public Transform muzzleTargetLight
	{
		get
		{
			return this._muzzleTargetLight;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0002D790 File Offset: 0x0002B990
	// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0002D7A8 File Offset: 0x0002B9A8
	public LineRenderer lineRender
	{
		get
		{
			return this._lineRender;
		}
		set
		{
			this._lineRender = value;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0002D7B4 File Offset: 0x0002B9B4
	// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0002D7CC File Offset: 0x0002B9CC
	public bool fireShowStat
	{
		get
		{
			return this._fireShowStat;
		}
		set
		{
			this._fireShowStat = value;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
	// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
	public float fireEffectTimeOut
	{
		get
		{
			return this._fireEffectTimeOut;
		}
		set
		{
			this._fireEffectTimeOut = value;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060005CA RID: 1482 RVA: 0x0002D7FC File Offset: 0x0002B9FC
	public GameObject fireEffect
	{
		get
		{
			bool flag = this.launcher.NetObject.FpsOrTps == 1;
			GameObject result;
			if (flag)
			{
				result = this._fireEffectFps;
			}
			else
			{
				result = this._fireEffectTps;
			}
			return result;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060005CB RID: 1483 RVA: 0x0002D835 File Offset: 0x0002BA35
	public int[] npcIndex
	{
		get
		{
			return this._npcIndex;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x060005CC RID: 1484 RVA: 0x0002D840 File Offset: 0x0002BA40
	// (set) Token: 0x060005CD RID: 1485 RVA: 0x0002D858 File Offset: 0x0002BA58
	public float damageForce
	{
		get
		{
			return this._damageForce;
		}
		set
		{
			this._damageForce = value;
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0002D862 File Offset: 0x0002BA62
	public void replaceMuzzle(Transform point)
	{
		this._muzzleTransform = point;
		this.muzzlePoint = this.muzzleTransform.position;
		this._muzzleName = point.name;
		this.loadRes();
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0002D890 File Offset: 0x0002BA90
	public virtual void initMuzzle(CLauncher cwc, Transform point, int animatorNo, object animators, ArrayList mainParam, Transform pointFps, Transform pointTps)
	{
		this.launcher = cwc;
		bool flag = point == null;
		if (flag)
		{
			point = cwc.transform;
		}
		bool flag2 = pointFps == null;
		if (flag2)
		{
			pointFps = point;
		}
		bool flag3 = pointTps == null;
		if (flag3)
		{
			pointTps = point;
		}
		this._muzzleTransform = point;
		this.muzzlePoint = this.muzzleTransform.position;
		this._muzzleTransformFps = pointFps;
		this._muzzleTransformTps = pointTps;
		this._muzzleName = point.name;
		this._animatorWeapon = new CInfoAnimatorWeapon();
		bool flag4 = this.launcher.weaponData != null && this.launcher.weaponData.muzzleanimationname != null && this.launcher.weaponData.muzzleanimationname.Count > 0;
		if (flag4)
		{
			bool flag5 = animatorNo >= this.launcher.weaponData.muzzleanimationname.Count;
			if (flag5)
			{
				this._animatorWeapon.animationName = Convert.ToString(this.launcher.weaponData.muzzleanimationname[0]);
			}
			else
			{
				this._animatorWeapon.animationName = Convert.ToString(this.launcher.weaponData.muzzleanimationname[animatorNo]);
			}
		}
		else
		{
			this._animatorWeapon.animationName = "0";
		}
		this._animatorWeapon.animationSerial = animatorNo;
		this._animatorWeapon.animationNo = Convert.ToInt32(animators);
		this.fireModelNameFps = ((mainParam[4] != null) ? Convert.ToString(mainParam[4]) : "0");
		this.fullModelName = ((mainParam[5] != null) ? Convert.ToString(mainParam[5]) : "0");
		this.fireSoundName = ((mainParam[6] != null) ? Convert.ToString(mainParam[6]) : "0");
		this.fullSoundName = ((mainParam[17] != null) ? Convert.ToString(mainParam[17]) : "0");
		this.fireModelNameTps = ((mainParam[42] != null) ? Convert.ToString(mainParam[42]) : "0");
		this._lineMuzzleTarget = ((mainParam[29] != null) ? Convert.ToString(mainParam[29]) : "0");
		this._npcIndex = (mainParam[30] as int[]);
		bool flag6 = this._npcIndex == null;
		if (flag6)
		{
			this._npcIndex = new int[0];
		}
		this._damageForce = (float)((mainParam[32] != null) ? Convert.ToInt32(mainParam[32]) : 0);
		this._fireModelRotate = (mainParam[36] != null && Convert.ToBoolean(mainParam[36]));
		this.fireloopsoundname = ((mainParam[41] != null) ? Convert.ToString(mainParam[41]) : "0");
		this.muzzlesoundtimeout = ((mainParam[49] != null) ? ((float)Convert.ToInt32(mainParam[49]) * 0.001f) : 0.15f);
		this.fireloopsounddamage = ((mainParam[51] != null) ? Convert.ToString(mainParam[51]) : "0");
		this.fireloopsoundtype = ((mainParam[52] != null) ? Convert.ToInt32(mainParam[52]) : 0);
		this._fireFrom = ((mainParam[50] != null) ? Convert.ToInt32(mainParam[50]) : 0);
		switch (this.launcher.weaponData.bullet_type)
		{
		case 1:
			this._data = new CBulletNormalData();
			break;
		case 2:
			this._data = new CBulletCircleData();
			break;
		case 3:
			this._data = new CBulletThrowData();
			break;
		case 4:
			this._data = new CBulletTrackData();
			break;
		case 5:
			this._data = new CBulletBezierData();
			break;
		case 6:
			this._data = new CBulletLineBendData();
			break;
		case 7:
			this._data = new CBulletContraData();
			break;
		case 8:
			this._data = new CBulletPhysicalData();
			break;
		default:
			this._data = new CBulletNormalData();
			break;
		}
		this._data.initBulletData(point, this.launcher.weaponData.bullet_type, mainParam);
		this.loadRes();
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
	private void loadRes()
	{
		bool flag = this._fireEffectFps != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this._fireEffectFps);
		}
		bool flag2 = this._fireEffectTps != null;
		if (flag2)
		{
			UnityEngine.Object.Destroy(this._fireEffectTps);
		}
		bool flag3 = this._fullEffect != null;
		if (flag3)
		{
			UnityEngine.Object.Destroy(this._fullEffect);
		}
		bool flag4 = this._fullSound != null;
		if (flag4)
		{
			UnityEngine.Object.Destroy(this._fullSound.gameObject);
		}
		for (int i = 0; i < this._fireSound.Count; i++)
		{
			bool flag5 = (AudioSource)this._fireSound[i] != null;
			if (flag5)
			{
				UnityEngine.Object.Destroy(((AudioSource)this._fireSound[i]).gameObject);
			}
		}
		bool flag6 = this._muzzleLoopSound != null;
		if (flag6)
		{
			UnityEngine.Object.Destroy(this._muzzleLoopSound);
		}
		bool flag7 = this._muzzleLoopSoundDamage != null;
		if (flag7)
		{
			UnityEngine.Object.Destroy(this._muzzleLoopSoundDamage);
		}
		bool flag8 = this.fireModelNameFps != "0" && this._muzzleTransformFps != null;
		if (flag8)
		{
			string url = "EffectRes/" + this.fireModelNameFps + ".ef";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag9 = resWWW == null;
			if (flag9)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadFireModelFps), 0);
			}
			else
			{
				this.loadFireModelFps(resWWW, 0);
			}
		}
		bool flag10 = this.fireModelNameTps != "0";
		if (flag10)
		{
			string url2 = "EffectRes/" + this.fireModelNameTps + ".ef";
			WWW resWWW2 = GameRoot.GetResWWW(url2);
			bool flag11 = resWWW2 == null;
			if (flag11)
			{
				GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.loadFireModelTps), 0);
			}
			else
			{
				this.loadFireModelTps(resWWW2, 0);
			}
		}
		bool flag12 = this.fullModelName != "0";
		if (flag12)
		{
			string url3 = "EffectRes/" + this.fullModelName + ".ef";
			WWW resWWW3 = GameRoot.GetResWWW(url3);
			bool flag13 = resWWW3 == null;
			if (flag13)
			{
				GameRoot.LoadAsset(url3, new fResourceManager.DownFinishDelegate(this.loadFullModel), 0);
			}
			else
			{
				this.loadFullModel(resWWW3, 0);
			}
		}
		bool flag14 = this.fireSoundName != "0";
		if (flag14)
		{
			this._fireSound = new ArrayList();
			this._fireSoundNo = 0;
			string url4 = "SoundRes/" + this.fireSoundName + ".sds";
			WWW resWWW4 = GameRoot.GetResWWW(url4);
			bool flag15 = resWWW4 == null;
			if (flag15)
			{
				GameRoot.LoadAsset(url4, new fResourceManager.DownFinishDelegate(this.loadFireSoundPrefab), 0);
			}
			else
			{
				this.loadFireSoundPrefab(resWWW4, 0);
				this._fireSoundWWW = resWWW4;
			}
		}
		bool flag16 = this.fullSoundName != "0";
		if (flag16)
		{
			string url5 = "SoundRes/" + this.fullSoundName + ".sds";
			WWW resWWW5 = GameRoot.GetResWWW(url5);
			bool flag17 = resWWW5 == null;
			if (flag17)
			{
				GameRoot.LoadAsset(url5, new fResourceManager.DownFinishDelegate(this.loadFullSoundPrefab), 0);
			}
			else
			{
				this.loadFullSoundPrefab(resWWW5, 0);
			}
		}
		bool flag18 = this.fireloopsoundname != "0";
		if (flag18)
		{
			string url6 = "SoundRes/" + this.fireloopsoundname + ".sds";
			WWW resWWW6 = GameRoot.GetResWWW(url6);
			bool flag19 = resWWW6 == null;
			if (flag19)
			{
				GameRoot.LoadAsset(url6, new fResourceManager.DownFinishDelegate(this.loadMuzzleSoundPrefab), 0);
			}
			else
			{
				this.loadMuzzleSoundPrefab(resWWW6, 0);
			}
		}
		bool flag20 = this.fireloopsounddamage != "0";
		if (flag20)
		{
			string url7 = "SoundRes/" + this.fireloopsounddamage + ".sds";
			WWW resWWW7 = GameRoot.GetResWWW(url7);
			bool flag21 = resWWW7 == null;
			if (flag21)
			{
				GameRoot.LoadAsset(url7, new fResourceManager.DownFinishDelegate(this.loadMuzzleSoundloopdamagePrefab), 0);
			}
			else
			{
				this.loadMuzzleSoundloopdamagePrefab(resWWW7, 0);
			}
		}
		string text = this._data.bulletBoomModel;
		bool flag22 = text != "0";
		if (flag22)
		{
			string url8 = "EffectRes/" + text + ".ef";
			WWW resWWW8 = GameRoot.GetResWWW(url8);
			bool flag23 = resWWW8 == null;
			if (flag23)
			{
				GameRoot.LoadAsset(url8, new fResourceManager.DownFinishDelegate(this.loadPrefab), 0);
			}
		}
		text = this._data.bulletBoomSound;
		bool flag24 = text != "0";
		if (flag24)
		{
			string url9 = "SoundRes/" + text + ".sds";
			WWW resWWW9 = GameRoot.GetResWWW(url9);
			bool flag25 = resWWW9 == null;
			if (flag25)
			{
				GameRoot.LoadAsset(url9, new fResourceManager.DownFinishDelegate(this.loadPrefab), 0);
			}
		}
		bool flag26 = this._npcIndex.Length == 0;
		if (!flag26)
		{
			for (int j = 0; j < this._npcIndex.Length; j++)
			{
				bool flag27 = fGameManager.mloadConfig != null && fGameManager.mloadConfig._Npc_Xml_Data != null && fGameManager.mloadConfig._Npc_Xml_Data.ContainsKey(this._npcIndex[j]);
				if (flag27)
				{
					Npc_Entity npc_Entity = fGameManager.mloadConfig._Npc_Xml_Data[this._npcIndex[j]];
					bool flag28 = npc_Entity != null;
					if (flag28)
					{
						string url10 = "HeroAnimator/" + npc_Entity.Resouce_Name + ".mo";
						WWW resWWW10 = GameRoot.GetResWWW(url10);
						bool flag29 = resWWW10 == null;
						if (flag29)
						{
							GameRoot.LoadAsset(url10, new fResourceManager.DownFinishDelegate(this.loadPrefab), 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0002E30D File Offset: 0x0002C50D
	private void loadPrefab(WWW www, object sStyle)
	{
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002E310 File Offset: 0x0002C510
	private void loadFireModelFps(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || this.launcher.NetObject == null || this.launcher.NetObject.haveDestroy;
		if (!flag)
		{
			this._fireEffectFps = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			ParticleLateUpdate component = this._fireEffectFps.GetComponent<ParticleLateUpdate>();
			bool flag2 = component == null;
			if (flag2)
			{
				this._fireEffectFps.transform.parent = this._muzzleTransformFps;
			}
			else
			{
				this._fireEffectFps.transform.parent = this.launcher.cwc.GetMuzzleContainer().transform;
				component.target = this._muzzleTransformFps;
			}
			this._fireEffectFps.transform.localRotation = Quaternion.identity;
			this._fireEffectFps.transform.localPosition = new Vector3(0f, 0f, 0f);
			int layer = LayerMask.NameToLayer("Ignore Raycast");
			Renderer[] componentsInChildren = this._fireEffectFps.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
			bool flag3 = this._lineMuzzleTarget != "0";
			if (flag3)
			{
				this._muzzleTargetLine = this._fireEffectFps.transform.FindChild(this._lineMuzzleTarget);
				this._lineRender = this._muzzleTargetLine.GetComponent<LineRenderer>();
				this._lineRender.useWorldSpace = true;
				this._lineRender.startWidth = 0.05f;
				this._lineRender.endWidth = 0.25f;
				this._lineRender.numPositions = 50;
				for (int j = 0; j < 50; j++)
				{
					this.lineRender.SetPosition(j, Vector3.zero);
				}
				this._muzzleTargetLight = this._fireEffectFps.transform.FindChild("guang");
				bool flag4 = this._muzzleTargetLight != null;
				if (flag4)
				{
					this._muzzleTargetLight.gameObject.SetActive(false);
				}
				this._muzzleTargetLine.gameObject.SetActive(false);
			}
			bool flag5 = this._lineMuzzleTarget == "0";
			if (flag5)
			{
				this._fireEffectFps.SetActive(false);
			}
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0002E588 File Offset: 0x0002C788
	private void loadFireModelTps(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || this.launcher.NetObject == null || this.launcher.NetObject.haveDestroy;
		if (!flag)
		{
			this._fireEffectTps = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			ParticleLateUpdate component = this._fireEffectTps.GetComponent<ParticleLateUpdate>();
			bool flag2 = component == null;
			if (flag2)
			{
				this._fireEffectTps.transform.parent = this._muzzleTransformTps;
			}
			else
			{
				this._fireEffectTps.transform.parent = this.launcher.cwc.GetMuzzleContainer().transform;
				component.target = this._muzzleTransformTps;
			}
			this._fireEffectTps.transform.localRotation = Quaternion.identity;
			this._fireEffectTps.transform.localPosition = new Vector3(0f, 0f, 0f);
			int layer = LayerMask.NameToLayer("Ignore Raycast");
			Renderer[] componentsInChildren = this._fireEffectTps.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
			bool flag3 = this._lineMuzzleTarget != "0";
			if (flag3)
			{
				this._muzzleTargetLine = this._fireEffectTps.transform.FindChild(this._lineMuzzleTarget);
				this._lineRender = this._muzzleTargetLine.GetComponent<LineRenderer>();
				this._lineRender.useWorldSpace = true;
				this._lineRender.startWidth = 0.05f;
				this._lineRender.endWidth = 0.25f;
				this._lineRender.numPositions = 50;
				for (int j = 0; j < 50; j++)
				{
					this.lineRender.SetPosition(j, Vector3.zero);
				}
				this._muzzleTargetLight = this._fireEffectTps.transform.FindChild("guang");
				bool flag4 = this._muzzleTargetLight != null;
				if (flag4)
				{
					this._muzzleTargetLight.gameObject.SetActive(false);
				}
				this._muzzleTargetLine.gameObject.SetActive(false);
			}
			bool flag5 = this._lineMuzzleTarget == "0";
			if (flag5)
			{
				this._fireEffectTps.SetActive(false);
			}
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0002E800 File Offset: 0x0002CA00
	public void LineDisable()
	{
		bool flag = this._muzzleTargetLine != null && this._muzzleTargetLine.gameObject.activeSelf;
		if (flag)
		{
			this._muzzleTargetLine.gameObject.SetActive(false);
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0002E848 File Offset: 0x0002CA48
	public void LineShow()
	{
		bool flag = this._muzzleTargetLine != null && !this._muzzleTargetLine.gameObject.activeSelf;
		if (flag)
		{
			this._muzzleTargetLine.gameObject.SetActive(true);
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0002E894 File Offset: 0x0002CA94
	public void TriggerShow()
	{
		bool flag = this._muzzleTargetLine != null && this._muzzleTargetLine.gameObject.activeSelf;
		if (flag)
		{
			this._muzzleTargetLine.gameObject.SetActive(false);
		}
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0002E8DC File Offset: 0x0002CADC
	private void loadFullModel(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			this._fullEffect = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this._fullEffect.transform.parent = this.muzzleTransform;
			this._fullEffect.transform.localRotation = Quaternion.identity;
			this._fullEffect.transform.localPosition = new Vector3(0f, 0f, 0f);
			int layer = LayerMask.NameToLayer("Ignore Raycast");
			Renderer[] componentsInChildren = this._fullEffect.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
			this._fullEffect.SetActive(false);
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0002E9E4 File Offset: 0x0002CBE4
	private void loadFullSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(this.muzzleTransform == null) && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.muzzleTransform;
			component.transform.localPosition = Vector3.zero;
			this._fullSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(this._fullSound, 0, this.launcher.NetObject, 20);
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0002EABC File Offset: 0x0002CCBC
	private void loadMuzzleSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(this.muzzleTransform == null) && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.loop = true;
			component.transform.parent = this.muzzleTransform;
			component.transform.localPosition = Vector3.zero;
			this._muzzleLoopSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(this._muzzleLoopSound, 0, this.launcher.NetObject, 20);
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002EB9C File Offset: 0x0002CD9C
	private void loadMuzzleSoundloopdamagePrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(this.muzzleTransform == null) && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.loop = true;
			component.transform.parent = this.muzzleTransform;
			component.transform.localPosition = Vector3.zero;
			this._muzzleLoopSoundDamage = component;
			CSoundManager.Instance.SetEffectSoundMixer(this._muzzleLoopSoundDamage, 0, this.launcher.NetObject, 20);
		}
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0002EC7C File Offset: 0x0002CE7C
	private void loadFireSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(this.muzzleTransform == null) && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.muzzleTransform;
			component.transform.localPosition = Vector3.zero;
			this._fireSound.Add(component);
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.launcher.NetObject, 20);
			this._fireSoundWWW = www;
		}
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0002ED5C File Offset: 0x0002CF5C
	public virtual void playFireEffect()
	{
		bool flag = this.fireEffect != null;
		if (flag)
		{
			this._fireShowStat = true;
			this._fireShowSoundStat = true;
			this._fireEffectTimeOut = Time.time + this.fireEffectLongTime;
			this._fireSoundTimeOut = Time.time + this.muzzlesoundtimeout;
			bool activeSelf = this.fireEffect.activeSelf;
			if (activeSelf)
			{
				this.fireEffect.SetActive(false);
			}
			this.fireEffect.SetActive(true);
			bool fireModelRotate = this._fireModelRotate;
			if (fireModelRotate)
			{
				this.fireEffect.transform.eulerAngles += new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
			}
		}
		this.FireSound();
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002EE2C File Offset: 0x0002D02C
	public void FireSound()
	{
		bool flag = this.fireloopsoundtype > 0;
		if (flag)
		{
			bool flag2 = this.fireloopsoundtype == 1;
			if (flag2)
			{
				bool flag3 = this._muzzleLoopSound != null && !this._muzzleLoopSound.isPlaying;
				if (flag3)
				{
					this._muzzleLoopSound.Play();
				}
			}
			else
			{
				bool flag4 = this.fireloopsoundtype == 2;
				if (flag4)
				{
					bool flag5 = this._muzzleTargetLine != null && this._muzzleTargetLine != null && this._muzzleTargetLine.gameObject.activeSelf;
					if (flag5)
					{
						bool flag6 = this._muzzleLoopSoundDamage != null && !this._muzzleLoopSoundDamage.isPlaying;
						if (flag6)
						{
							this._muzzleLoopSoundDamage.Play();
						}
						bool flag7 = this._muzzleLoopSound != null && this._muzzleLoopSound.isPlaying;
						if (flag7)
						{
							this._muzzleLoopSound.Stop();
						}
					}
					else
					{
						bool flag8 = this._muzzleLoopSound != null;
						if (flag8)
						{
							bool flag9 = !this._muzzleLoopSound.isPlaying;
							if (flag9)
							{
								this._muzzleLoopSound.Play();
							}
							bool flag10 = this._muzzleLoopSoundDamage != null && !this._muzzleLoopSoundDamage.isPlaying;
							if (flag10)
							{
								this._muzzleLoopSoundDamage.Stop();
							}
						}
					}
				}
			}
		}
		bool flag11 = this._fireSound.Count == 0 || this.launcher == null;
		if (!flag11)
		{
			bool flag12 = true;
			AudioSource audioSource = null;
			for (int i = 0; i < this._fireSound.Count; i++)
			{
				int num = (this._fireSoundNo + i) % this._fireSound.Count;
				audioSource = (AudioSource)this._fireSound[num];
				bool flag13 = !(audioSource == null) && !audioSource.isPlaying;
				if (flag13)
				{
					flag12 = false;
					this._fireSoundNo = num;
					break;
				}
			}
			bool flag14 = flag12 && this.launcher.NetObject != null && !this.launcher.NetObject.haveDestroy && this.launcher.NetObject.playerHaveCreate;
			if (flag14)
			{
				bool flag15 = this._fireSoundWWW != null;
				if (flag15)
				{
					GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this._fireSoundWWW.assetBundle.LoadAsset(this._fireSoundWWW.assetBundle.GetAllAssetNames()[0]));
					AudioSource component = gameObject.GetComponent<AudioSource>();
					component.playOnAwake = false;
					component.Stop();
					component.transform.parent = this.muzzleTransform;
					component.transform.localPosition = Vector3.zero;
					this._fireSound.Add(component);
					audioSource = component;
					CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.launcher.NetObject, 20);
					this._fireSoundNo = this._fireSound.Count - 1;
				}
				else
				{
					Debug.LogError(string.Format("_fireSoundWWW是空的,武器:武器:{0},武器编号:{1}", this.launcher.cwc.name, this.launcher.weaponData.shootname));
				}
			}
			bool flag16 = audioSource != null;
			if (flag16)
			{
				audioSource.Play();
			}
		}
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0002F19C File Offset: 0x0002D39C
	public virtual void stopFireEffect()
	{
		bool flag = this._fireShowStat && Time.time > this._fireEffectTimeOut;
		if (flag)
		{
			this._fireShowStat = false;
			bool flag2 = this.fireEffect != null && this.fireEffect.activeSelf;
			if (flag2)
			{
				this.fireEffect.SetActive(false);
			}
		}
		bool flag3 = this._fireShowSoundStat && Time.time > this._fireSoundTimeOut;
		if (flag3)
		{
			this._fireShowSoundStat = false;
			bool flag4 = this._muzzleLoopSound != null;
			if (flag4)
			{
				this._muzzleLoopSound.Stop();
			}
			bool flag5 = this._muzzleLoopSoundDamage != null;
			if (flag5)
			{
				this._muzzleLoopSoundDamage.Stop();
			}
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0002F264 File Offset: 0x0002D464
	public void cancelFireEffect()
	{
		this._fireShowStat = false;
		bool flag = this.fireEffect != null && this.fireEffect.activeSelf;
		if (flag)
		{
			this.fireEffect.SetActive(false);
		}
		this._fireShowSoundStat = false;
		bool flag2 = this._muzzleLoopSound != null;
		if (flag2)
		{
			this._muzzleLoopSound.Stop();
		}
		bool flag3 = this._muzzleLoopSoundDamage != null;
		if (flag3)
		{
			this._muzzleLoopSoundDamage.Stop();
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0002F2EC File Offset: 0x0002D4EC
	public void playFullEffect()
	{
		bool flag = this._fullEffect != null;
		if (flag)
		{
			bool flag2 = !this._fullEffect.activeSelf;
			if (flag2)
			{
				this._fullEffect.SetActive(true);
			}
			this._fullEffect.transform.eulerAngles += new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
		}
		bool flag3 = this._fullSound != null;
		if (flag3)
		{
			this._fullSound.Play();
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0002F388 File Offset: 0x0002D588
	public void disableFullEffect()
	{
		bool flag = this._fullEffect != null && this._fullEffect.activeSelf;
		if (flag)
		{
			this._fullEffect.SetActive(false);
		}
		bool flag2 = this._fullSound != null;
		if (flag2)
		{
			this._fullSound.Stop();
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002F3E4 File Offset: 0x0002D5E4
	public virtual int Fire(Vector3 targetPoint, object target, int isContinuous)
	{
		return 1;
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0002F3F7 File Offset: 0x0002D5F7
	public virtual void checkFireEffect()
	{
		this.stopFireEffect();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0002F404 File Offset: 0x0002D604
	protected virtual void Update()
	{
		bool flag = this.muzzleTransform != null;
		if (flag)
		{
			this.muzzlePoint = this.muzzleTransform.position;
		}
		this.checkFireEffect();
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0002F43C File Offset: 0x0002D63C
	public virtual void OnHitByPhysical(Collision coll)
	{
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002F43F File Offset: 0x0002D63F
	public virtual void ReSetphysicalDir()
	{
		this.physicalAt = this.muzzleTransform.position;
		this.physicalDir = this.muzzleTransform.right;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002F464 File Offset: 0x0002D664
	public virtual void SetPhysicalBegin()
	{
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002F467 File Offset: 0x0002D667
	public virtual void SetPhysicalEnd()
	{
	}

	// Token: 0x0400044B RID: 1099
	public CLauncher launcher;

	// Token: 0x0400044C RID: 1100
	public float fireEffectLongTime = 2f;

	// Token: 0x0400044D RID: 1101
	private float _fireSoundTimeOut;

	// Token: 0x0400044E RID: 1102
	private string fireModelNameFps = "0";

	// Token: 0x0400044F RID: 1103
	private string fireModelNameTps = "0";

	// Token: 0x04000450 RID: 1104
	private string fullModelName = "0";

	// Token: 0x04000451 RID: 1105
	private string fireSoundName = "0";

	// Token: 0x04000452 RID: 1106
	private string fullSoundName = "0";

	// Token: 0x04000453 RID: 1107
	private string fireloopsoundname = "0";

	// Token: 0x04000454 RID: 1108
	private string fireloopsounddamage = "0";

	// Token: 0x04000455 RID: 1109
	private int fireloopsoundtype;

	// Token: 0x04000456 RID: 1110
	private float muzzlesoundtimeout = 0.15f;

	// Token: 0x04000457 RID: 1111
	protected int _fireFrom;

	// Token: 0x04000458 RID: 1112
	private string _lineMuzzleTarget = "0";

	// Token: 0x04000459 RID: 1113
	private int[] _npcIndex;

	// Token: 0x0400045A RID: 1114
	protected CMuzzlePhysicalTrigger physicalTrigger;

	// Token: 0x0400045B RID: 1115
	private Transform _muzzleTargetLine;

	// Token: 0x0400045C RID: 1116
	private Transform _muzzleTargetLight;

	// Token: 0x0400045D RID: 1117
	private LineRenderer _lineRender;

	// Token: 0x0400045E RID: 1118
	public const float maxLineLong = 50f;

	// Token: 0x0400045F RID: 1119
	private CBullet _bullet;

	// Token: 0x04000460 RID: 1120
	private CBulletData _data;

	// Token: 0x04000461 RID: 1121
	private Vector3 _bulletBornPoint;

	// Token: 0x04000462 RID: 1122
	private GameObject _targetObject;

	// Token: 0x04000463 RID: 1123
	private string _muzzleName;

	// Token: 0x04000464 RID: 1124
	private GameObject _fullEffect;

	// Token: 0x04000465 RID: 1125
	private GameObject _fireEffectFps;

	// Token: 0x04000466 RID: 1126
	private GameObject _fireEffectTps;

	// Token: 0x04000467 RID: 1127
	private float _fireEffectTimeOut;

	// Token: 0x04000468 RID: 1128
	private bool _fireShowStat;

	// Token: 0x04000469 RID: 1129
	private bool _fireShowSoundStat;

	// Token: 0x0400046A RID: 1130
	private ArrayList _fireSound = new ArrayList();

	// Token: 0x0400046B RID: 1131
	private WWW _fireSoundWWW;

	// Token: 0x0400046C RID: 1132
	private int _fireSoundNo;

	// Token: 0x0400046D RID: 1133
	private AudioSource _fullSound;

	// Token: 0x0400046E RID: 1134
	private AudioSource _muzzleLoopSound;

	// Token: 0x0400046F RID: 1135
	private AudioSource _muzzleLoopSoundDamage;

	// Token: 0x04000470 RID: 1136
	private Transform _muzzleTransform;

	// Token: 0x04000471 RID: 1137
	private Transform _muzzleTransformFps;

	// Token: 0x04000472 RID: 1138
	private Transform _muzzleTransformTps;

	// Token: 0x04000473 RID: 1139
	public Vector3 muzzlePoint;

	// Token: 0x04000474 RID: 1140
	private float _damageForce;

	// Token: 0x04000475 RID: 1141
	protected Vector3 physicalDir;

	// Token: 0x04000476 RID: 1142
	private NetLiving _targetNet;

	// Token: 0x04000477 RID: 1143
	private bool _fireModelRotate;

	// Token: 0x04000478 RID: 1144
	private CInfoAnimatorWeapon _animatorWeapon;

	// Token: 0x04000479 RID: 1145
	private Vector3 physicalAt;
}

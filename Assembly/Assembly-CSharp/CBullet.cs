using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class CBullet : MonoBehaviour
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x0600029C RID: 668 RVA: 0x00014865 File Offset: 0x00012A65
	public string bulletOwnerName
	{
		get
		{
			return this._bulletOwnerName;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600029D RID: 669 RVA: 0x0001486D File Offset: 0x00012A6D
	public float bulletCreateTime
	{
		get
		{
			return this._bulletCreateTime;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x0600029E RID: 670 RVA: 0x00014878 File Offset: 0x00012A78
	// (set) Token: 0x0600029F RID: 671 RVA: 0x00014890 File Offset: 0x00012A90
	public int bulletGID
	{
		get
		{
			return this._bulletGID;
		}
		set
		{
			this._bulletGID = value;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001489A File Offset: 0x00012A9A
	public float gIDTime
	{
		get
		{
			return this._gIDTime;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x000148A4 File Offset: 0x00012AA4
	// (set) Token: 0x060002A2 RID: 674 RVA: 0x000148BC File Offset: 0x00012ABC
	public int fullTimeShoot
	{
		get
		{
			return this._fullTimeShoot;
		}
		set
		{
			this._fullTimeShoot = value;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060002A3 RID: 675 RVA: 0x000148C8 File Offset: 0x00012AC8
	// (set) Token: 0x060002A4 RID: 676 RVA: 0x000148E0 File Offset: 0x00012AE0
	public Vector3 hitPhysical
	{
		get
		{
			return this._hitPhysical;
		}
		set
		{
			this._hitPhysical = value;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060002A5 RID: 677 RVA: 0x000148EC File Offset: 0x00012AEC
	// (set) Token: 0x060002A6 RID: 678 RVA: 0x00014904 File Offset: 0x00012B04
	public RaycastHit hitGameObject
	{
		get
		{
			return this._hitGameObject;
		}
		set
		{
			this._hitGameObject = value;
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060002A7 RID: 679 RVA: 0x00014910 File Offset: 0x00012B10
	// (set) Token: 0x060002A8 RID: 680 RVA: 0x00014928 File Offset: 0x00012B28
	public Vector3 physicsForceBornPoint
	{
		get
		{
			return this._physicsForceBornPoint;
		}
		set
		{
			this._physicsForceBornPoint = value;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060002A9 RID: 681 RVA: 0x00014934 File Offset: 0x00012B34
	// (set) Token: 0x060002AA RID: 682 RVA: 0x0001494C File Offset: 0x00012B4C
	public Vector3 bornPoint
	{
		get
		{
			return this._bornPoint;
		}
		set
		{
			this._bornPoint = value;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060002AB RID: 683 RVA: 0x00014958 File Offset: 0x00012B58
	// (set) Token: 0x060002AC RID: 684 RVA: 0x00014970 File Offset: 0x00012B70
	public Vector3 damagePoint
	{
		get
		{
			return this._damagePoint;
		}
		set
		{
			this._damagePoint = value;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060002AD RID: 685 RVA: 0x0001497C File Offset: 0x00012B7C
	// (set) Token: 0x060002AE RID: 686 RVA: 0x00014994 File Offset: 0x00012B94
	public CMuzzle muzzle
	{
		get
		{
			return this._muzzle;
		}
		set
		{
			this._muzzle = value;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060002AF RID: 687 RVA: 0x000149A0 File Offset: 0x00012BA0
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x000149B8 File Offset: 0x00012BB8
	public CBulletData bulletData
	{
		get
		{
			return this._bulletData;
		}
		set
		{
			this._bulletData = value;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060002B1 RID: 689 RVA: 0x000149C4 File Offset: 0x00012BC4
	// (set) Token: 0x060002B2 RID: 690 RVA: 0x000149DC File Offset: 0x00012BDC
	public bool readyDestory
	{
		get
		{
			return this._readyDestory;
		}
		set
		{
			this._readyDestory = value;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060002B3 RID: 691 RVA: 0x000149E8 File Offset: 0x00012BE8
	// (set) Token: 0x060002B4 RID: 692 RVA: 0x00014A00 File Offset: 0x00012C00
	public bool end
	{
		get
		{
			return this._end;
		}
		set
		{
			this._end = value;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060002B5 RID: 693 RVA: 0x00014A0C File Offset: 0x00012C0C
	// (set) Token: 0x060002B6 RID: 694 RVA: 0x00014A24 File Offset: 0x00012C24
	public Vector3 endPoint
	{
		get
		{
			return this._endPoint;
		}
		set
		{
			this._endPoint = value;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060002B7 RID: 695 RVA: 0x00014A30 File Offset: 0x00012C30
	// (set) Token: 0x060002B8 RID: 696 RVA: 0x00014A48 File Offset: 0x00012C48
	public bool haveCollision
	{
		get
		{
			return this._haveCollision;
		}
		set
		{
			this._haveCollision = value;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060002B9 RID: 697 RVA: 0x00014A54 File Offset: 0x00012C54
	// (set) Token: 0x060002BA RID: 698 RVA: 0x00014A6C File Offset: 0x00012C6C
	public int bulletLayer
	{
		get
		{
			return this._bulletLayer;
		}
		set
		{
			this._bulletLayer = value;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060002BB RID: 699 RVA: 0x00014A78 File Offset: 0x00012C78
	// (set) Token: 0x060002BC RID: 700 RVA: 0x00014A90 File Offset: 0x00012C90
	public int bulletLayerNoneBuild
	{
		get
		{
			return this._bulletLayerNoneBuild;
		}
		set
		{
			this._bulletLayerNoneBuild = value;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060002BD RID: 701 RVA: 0x00014A9C File Offset: 0x00012C9C
	// (set) Token: 0x060002BE RID: 702 RVA: 0x00014AB4 File Offset: 0x00012CB4
	public int physicalLayer
	{
		get
		{
			return this._physicalLayer;
		}
		set
		{
			this._physicalLayer = value;
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060002BF RID: 703 RVA: 0x00014AC0 File Offset: 0x00012CC0
	// (set) Token: 0x060002C0 RID: 704 RVA: 0x00014AD8 File Offset: 0x00012CD8
	public float nowFlyDistance
	{
		get
		{
			return this._nowFlyDistance;
		}
		set
		{
			this._nowFlyDistance = value;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060002C1 RID: 705 RVA: 0x00014AE4 File Offset: 0x00012CE4
	// (set) Token: 0x060002C2 RID: 706 RVA: 0x00014AFC File Offset: 0x00012CFC
	public float trackSpeedNow
	{
		get
		{
			return this._trackSpeedNow;
		}
		set
		{
			this._trackSpeedNow = value;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x00014B08 File Offset: 0x00012D08
	// (set) Token: 0x060002C4 RID: 708 RVA: 0x00014B20 File Offset: 0x00012D20
	public float trackRotationSpeedNow
	{
		get
		{
			return this._trackRotationSpeedNow;
		}
		set
		{
			this._trackRotationSpeedNow = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060002C5 RID: 709 RVA: 0x00014B2C File Offset: 0x00012D2C
	// (set) Token: 0x060002C6 RID: 710 RVA: 0x00014B44 File Offset: 0x00012D44
	public bool isChildBullet
	{
		get
		{
			return this._isChildBullet;
		}
		set
		{
			this._isChildBullet = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x00014B50 File Offset: 0x00012D50
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x00014B68 File Offset: 0x00012D68
	public int bulletOneNo
	{
		get
		{
			return this._bulletOneNo;
		}
		set
		{
			this._bulletOneNo = value;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x00014B74 File Offset: 0x00012D74
	// (set) Token: 0x060002CA RID: 714 RVA: 0x00014B8C File Offset: 0x00012D8C
	public int bulletChildNo
	{
		get
		{
			return this._bulletChildNo;
		}
		set
		{
			this._bulletChildNo = value;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060002CB RID: 715 RVA: 0x00014B98 File Offset: 0x00012D98
	// (set) Token: 0x060002CC RID: 716 RVA: 0x00014BB0 File Offset: 0x00012DB0
	public Collider hitCollider
	{
		get
		{
			return this._hitCollider;
		}
		set
		{
			this._hitCollider = value;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060002CD RID: 717 RVA: 0x00014BBC File Offset: 0x00012DBC
	// (set) Token: 0x060002CE RID: 718 RVA: 0x00014BD4 File Offset: 0x00012DD4
	public Vector3 hitVelocity
	{
		get
		{
			return this._hitVelocity;
		}
		set
		{
			this._hitVelocity = value;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060002CF RID: 719 RVA: 0x00014BE0 File Offset: 0x00012DE0
	// (set) Token: 0x060002D0 RID: 720 RVA: 0x00014BF8 File Offset: 0x00012DF8
	public Vector3 matVector
	{
		get
		{
			return this._matVector;
		}
		set
		{
			this._matVector = value;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x00014C04 File Offset: 0x00012E04
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x00014C1C File Offset: 0x00012E1C
	public int boomLayer
	{
		get
		{
			return this._boomLayer;
		}
		set
		{
			this._boomLayer = value;
		}
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00014C28 File Offset: 0x00012E28
	public virtual void initBullet(CMuzzle muzz, CBulletData __data, bool child, int ismaster = 0, int sandanChildNum = 0)
	{
		this._isChildBullet = child;
		this._bulletOneNo = ismaster;
		this._bulletChildNo = sandanChildNum;
		this._gIDTime = Time.time;
		this._muzzle = muzz;
		bool flag = muzz.launcher == null;
		if (flag)
		{
			Debug.Log("注意:muzz.launcher为Null");
		}
		bool flag2 = muzz.launcher.NetObject == null;
		if (flag2)
		{
			Debug.Log("注意:muzz.launcher.NetObject为Null");
		}
		bool flag3 = muzz.launcher.NetObject.UserControl == null;
		if (flag3)
		{
			Debug.Log("注意:muzz.launcher.NetObject.UserControl为Null");
		}
		bool flag4 = __data == null;
		if (flag4)
		{
			Debug.Log("注意:__data为Null");
		}
		this.netobjectCreateId = muzz.launcher.NetObject.loadID;
		this._bulletData = __data;
		this.boomTimeDealy = muzz.launcher.gemBoomTime * this.bulletData.bulletBoomDelay;
		this._bulletCreateTime = Time.time;
		this.bulletCreateSTime = Fun.Now;
		this._bulletOwnerName = muzz.launcher.NetObject.UserControl.transform.root.name;
		this._blockLayer = LayerManager.getBlockLayer(muzz.launcher.NetObject.nForce);
		this.bulletLayer = LayerManager.getMainPlayerBulletLayer(muzz.launcher.layerType, muzz.launcher.NetObject.nForce);
		this.physicalLayer = LayerManager.getPhysicalBulletLayer(muzz.launcher.layerType, muzz.launcher.NetObject.nForce);
		bool flag5 = this._bulletData.bulletBoomModel != "0";
		if (flag5)
		{
			bool flag6 = this._bulletData.bulletBoomModel.Contains("xingjilieren_baozhao_baozha");
			if (flag6)
			{
				Debug.LogError("xingjilieren_baozhao_baozha");
			}
			string url = "EffectRes/" + this._bulletData.bulletBoomModel + ".ef";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag7 = resWWW == null;
			if (flag7)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadboomModel), 0);
			}
			else
			{
				this.loadboomModel(resWWW, 0);
			}
		}
		bool flag8 = !this._isChildBullet && this._bulletData.bulletBoomSound != "0";
		if (flag8)
		{
			string url2 = "SoundRes/" + this._bulletData.bulletBoomSound + ".sds";
			WWW resWWW2 = GameRoot.GetResWWW(url2);
			bool flag9 = resWWW2 == null;
			if (flag9)
			{
				GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.loadBoomSoundPrefab), 0);
			}
			else
			{
				this.loadBoomSoundPrefab(resWWW2, 0);
			}
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00014EE4 File Offset: 0x000130E4
	private void loadboomModel(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !this.isDestroy;
		if (flag)
		{
			this._boomEffectPrefab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00014F30 File Offset: 0x00013130
	private void loadBoomSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !(fGameManager.allSoundFather == null) && !this.isDestroy && !this._isChildBullet;
		if (flag)
		{
			this._boomSoundPrefab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(this._boomSoundPrefab, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = fGameManager.allSoundFather.transform;
			this._boomSound = gameObject.GetComponent<AudioSource>();
			this._boomSound.Stop();
			this._boomSound.playOnAwake = false;
			this._boomSound.Stop();
			CSoundManager.Instance.SetEffectSoundMixer(this._boomSound, 0, null, 20);
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00015010 File Offset: 0x00013210
	protected virtual void showBoomEffect()
	{
		bool flag = !(fGameManager.allBaozhaFather == null);
		if (flag)
		{
			bool flag2 = this._boomEffectPrefab != null;
			if (flag2)
			{
				GameObject gameObject = CEffectPools.Instance.CreateGameObject(this._boomEffectPrefab, base.transform.position, this._boomForward);
				gameObject.transform.parent = fGameManager.allBaozhaFather.transform;
				EffectAutoDestroy effectAutoDestroy = gameObject.AddComponent<EffectAutoDestroy>();
				effectAutoDestroy.PutInPools = true;
				effectAutoDestroy.ResGID = this._boomEffectPrefab.GetInstanceID();
			}
			bool flag3 = !this._isChildBullet && this._boomSound != null;
			if (flag3)
			{
				this._boomSound.transform.parent = fGameManager.allSoundFather.transform;
				this._boomSound.transform.position = base.transform.position;
				this._boomSound.Play();
				SoundAutoDestroy soundAutoDestroy = this._boomSound.gameObject.AddComponent<SoundAutoDestroy>();
				soundAutoDestroy.PutInPools = true;
				soundAutoDestroy.ResGID = this._boomSoundPrefab.GetInstanceID();
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0001512C File Offset: 0x0001332C
	protected void showTagEffectBoom(Collider coll)
	{
		this.defaultTag = coll.transform.tag;
		bool flag = this.defaultTag.Length > 1 && this.muzzle.launcher.launcherTags.Count != 0;
		if (flag)
		{
			int key = this.muzzle.launcher.specialMaterial.ContainsKey(this.defaultTag) ? this.muzzle.launcher.specialMaterial[this.defaultTag] : 10000;
			MaterialTag materialTag = fGameManager.mloadConfig.AllMaterialObj[key];
			materialTag.checkLoaded();
			Vector3 boomPoint = (!(this.hitGameObject.collider != null)) ? this._hitPoint : this._hitGameObject.point;
			this.showMatBoom(materialTag, boomPoint);
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0001520C File Offset: 0x0001340C
	private void showTagEffect()
	{
		bool flag = this.hitGameObject.collider != null;
		if (flag)
		{
			this.defaultTag = this.hitGameObject.transform.tag;
		}
		else
		{
			bool flag2 = this.hitCollider != null;
			if (flag2)
			{
				this.defaultTag = this.hitCollider.transform.tag;
			}
		}
		bool flag3 = this.defaultTag.Length > 1 && this.muzzle.launcher.launcherTags.Count != 0;
		if (flag3)
		{
			int key = this.muzzle.launcher.specialMaterial.ContainsKey(this.defaultTag) ? this.muzzle.launcher.specialMaterial[this.defaultTag] : 10000;
			MaterialTag materialTag = fGameManager.mloadConfig.AllMaterialObj[key];
			materialTag.checkLoaded();
			Vector3 boomPoint = (!(this.hitGameObject.collider != null)) ? this._hitPoint : this._hitGameObject.point;
			this.showMatBoom(materialTag, boomPoint);
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001533C File Offset: 0x0001353C
	private void showMatBoom(MaterialTag matTag, Vector3 boomPoint)
	{
		UnityEngine.Object @object = (fUIManager.LiZiModel != 2) ? matTag.BaozhaObj : matTag.BaozhaObj_little;
		bool flag = @object == null;
		if (!flag)
		{
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(@object, boomPoint, this._boomForward);
			gameObject.transform.parent = fGameManager.allBaozhaFather.transform;
			EffectAutoDestroy effectAutoDestroy = gameObject.AddComponent<EffectAutoDestroy>();
			effectAutoDestroy.PutInPools = true;
			effectAutoDestroy.ResGID = @object.GetInstanceID();
			int count = matTag.allSoundObj.Count;
			bool flag2 = count > 0;
			if (flag2)
			{
				int index = UnityEngine.Random.Range(0, count);
				UnityEngine.Object object2 = matTag.allSoundObj[index] as UnityEngine.Object;
				bool flag3 = object2 != null;
				if (flag3)
				{
					GameObject gameObject2 = CEffectPools.Instance.CreateGameObject(object2, gameObject.transform.position, Quaternion.identity);
					gameObject2.transform.parent = fGameManager.allSoundFather.transform;
					AudioSource component = gameObject2.GetComponent<AudioSource>();
					SoundAutoDestroy soundAutoDestroy = gameObject2.AddComponent<SoundAutoDestroy>();
					component.playOnAwake = false;
					component.Stop();
					soundAutoDestroy.PutInPools = true;
					soundAutoDestroy.ResGID = object2.GetInstanceID();
					CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
					component.Play();
				}
			}
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00015489 File Offset: 0x00013689
	public virtual void Fire(object target)
	{
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0001548C File Offset: 0x0001368C
	protected virtual void checkDelayBoom()
	{
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00015490 File Offset: 0x00013690
	protected virtual void boom()
	{
		Vector3 vector = Vector3.zero;
		bool flag = this.hitGameObject.collider != null;
		if (flag)
		{
			vector = this.hitGameObject.normal;
		}
		else
		{
			bool flag2 = this.hitVelocity != Vector3.zero;
			if (flag2)
			{
				vector = this.hitVelocity;
			}
		}
		bool flag3 = vector != Vector3.zero;
		if (flag3)
		{
			Quaternion quaternion = Quaternion.LookRotation(vector);
			bool flag4 = this._matVector != Vector3.zero;
			if (flag4)
			{
				Vector3 forward = base.transform.forward;
				Vector3 rhs = Vector3.Cross(forward, this._matVector);
				Vector3 forward2 = Vector3.Cross(vector, rhs);
				quaternion = Quaternion.LookRotation(forward2, vector);
				Quaternion rhs2 = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
				Quaternion rhs3 = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
				quaternion = quaternion * rhs2 * rhs3;
			}
			this._boomForward = quaternion;
		}
		this.showBoomEffect();
		this.showTagEffect();
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0001559C File Offset: 0x0001379C
	protected virtual void destoryBullet()
	{
		bool flag = this.bulletDelayTransform != null;
		if (flag)
		{
			bool flag2 = this._bulletData.bulletDestoryTimeOut > 0f && !this.mustDestoryModel;
			if (flag2)
			{
				this.bulletDelayTransform.parent = base.transform.parent;
				EffectAutoDestroy effectAutoDestroy = this.bulletDelayTransform.gameObject.AddComponent<EffectAutoDestroy>();
				effectAutoDestroy.PutInPools = true;
				effectAutoDestroy.checkPar = false;
				effectAutoDestroy.maxDelayTime = this._bulletData.bulletDestoryTimeOut;
				effectAutoDestroy.ResGID = this.muzzle.bulletPrefabDelay.GetInstanceID();
			}
			else
			{
				this.bulletDelayTransform.parent = base.transform.parent;
				CEffectPools.Instance.PutInPools(this.muzzle.bulletPrefabDelay.GetInstanceID(), this.bulletDelayTransform);
			}
		}
		int instanceID = this.muzzle.bulletPrefab.GetInstanceID();
		CEffectPools.Instance.PutInPools(instanceID, base.transform);
		bool flag3 = CBullet.bulletDict.ContainsKey(this.bulletGID);
		if (flag3)
		{
			CBullet.bulletDict.Remove(this.bulletGID);
		}
		this.DestroyScript();
	}

	// Token: 0x060002DE RID: 734 RVA: 0x000156CF File Offset: 0x000138CF
	protected virtual void playBulletFlySound(GameObject obj)
	{
		Fun.Log("weapon - 子弹呼啸处理----", null);
	}

	// Token: 0x060002DF RID: 735 RVA: 0x000156DE File Offset: 0x000138DE
	protected void DestroyScript()
	{
		this.isDestroy = true;
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04000267 RID: 615
	public static float bulletDictCheckTime = 0f;

	// Token: 0x04000268 RID: 616
	public static Dictionary<int, CBullet> bulletDict = new Dictionary<int, CBullet>();

	// Token: 0x04000269 RID: 617
	public bool mustDestoryModel;

	// Token: 0x0400026A RID: 618
	public CObjectRender objectRender;

	// Token: 0x0400026B RID: 619
	public Rigidbody[] allRig;

	// Token: 0x0400026C RID: 620
	private bool isDestroy;

	// Token: 0x0400026D RID: 621
	private CBulletData _bulletData;

	// Token: 0x0400026E RID: 622
	private CMuzzle _muzzle;

	// Token: 0x0400026F RID: 623
	public Transform bulletDelayTransform;

	// Token: 0x04000270 RID: 624
	private Quaternion _boomForward = Quaternion.identity;

	// Token: 0x04000271 RID: 625
	private float _nowFlyDistance;

	// Token: 0x04000272 RID: 626
	private int _bulletLayer;

	// Token: 0x04000273 RID: 627
	private int _bulletLayerNoneBuild;

	// Token: 0x04000274 RID: 628
	private int _boomLayer;

	// Token: 0x04000275 RID: 629
	protected int _blockLayer;

	// Token: 0x04000276 RID: 630
	private bool _readyDestory;

	// Token: 0x04000277 RID: 631
	private bool _end;

	// Token: 0x04000278 RID: 632
	private bool _haveCollision;

	// Token: 0x04000279 RID: 633
	public int nContinueBullet;

	// Token: 0x0400027A RID: 634
	protected RaycastHit _hitGameObject;

	// Token: 0x0400027B RID: 635
	private Vector3 _hitVelocity;

	// Token: 0x0400027C RID: 636
	private Collider _hitCollider;

	// Token: 0x0400027D RID: 637
	private Vector3 _hitPhysical;

	// Token: 0x0400027E RID: 638
	protected Vector3 _matVector;

	// Token: 0x0400027F RID: 639
	private Vector3 _endPoint;

	// Token: 0x04000280 RID: 640
	private Vector3 _physicsForceBornPoint;

	// Token: 0x04000281 RID: 641
	private Vector3 _bornPoint;

	// Token: 0x04000282 RID: 642
	private Vector3 _damagePoint;

	// Token: 0x04000283 RID: 643
	private UnityEngine.Object _boomEffectPrefab;

	// Token: 0x04000284 RID: 644
	private UnityEngine.Object _boomSoundPrefab;

	// Token: 0x04000285 RID: 645
	private AudioSource _boomSound;

	// Token: 0x04000286 RID: 646
	private float _bulletCreateTime;

	// Token: 0x04000287 RID: 647
	protected DateTime bulletCreateSTime;

	// Token: 0x04000288 RID: 648
	private string _bulletOwnerName;

	// Token: 0x04000289 RID: 649
	private bool _isChildBullet;

	// Token: 0x0400028A RID: 650
	private int _bulletOneNo;

	// Token: 0x0400028B RID: 651
	private int _bulletChildNo;

	// Token: 0x0400028C RID: 652
	public string defaultTag = "0";

	// Token: 0x0400028D RID: 653
	protected Vector3 _hitPoint;

	// Token: 0x0400028E RID: 654
	public int cartID;

	// Token: 0x0400028F RID: 655
	public int numberBefore;

	// Token: 0x04000290 RID: 656
	public GameObject bulletTargetObject;

	// Token: 0x04000291 RID: 657
	public NetLiving bulletTargetNet;

	// Token: 0x04000292 RID: 658
	private int _bulletGID;

	// Token: 0x04000293 RID: 659
	private float _gIDTime;

	// Token: 0x04000294 RID: 660
	private int _fullTimeShoot;

	// Token: 0x04000295 RID: 661
	private int _physicalLayer;

	// Token: 0x04000296 RID: 662
	private float _trackSpeedNow;

	// Token: 0x04000297 RID: 663
	private float _trackRotationSpeedNow;

	// Token: 0x04000298 RID: 664
	protected int netobjectCreateId = -1;

	// Token: 0x04000299 RID: 665
	protected float boomTimeDealy;
}

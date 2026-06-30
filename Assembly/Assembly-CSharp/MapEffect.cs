using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class MapEffect : NetLiving
{
	// Token: 0x170002DE RID: 734
	// (get) Token: 0x060019DD RID: 6621 RVA: 0x000F433F File Offset: 0x000F253F
	public override Vector3 userCore
	{
		get
		{
			return this.vBorn + this._userCoreAdd;
		}
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x000F4354 File Offset: 0x000F2554
	public void InitMapEffect(int nID, int xpos, int ypos, int zpos, int force, int targetId, int rx, int ry, int rz)
	{
		this._rot = new Vector3((float)rx, (float)ry, (float)rz);
		this.parentId = targetId;
		this.netobject_type = 3;
		this.id = nID;
		this.x = xpos;
		this.y = ypos;
		this.z = zpos;
		this.nForce = force;
		this.vBorn = new Vector3((float)xpos / 100f, (float)zpos / 100f, (float)ypos / 100f);
		this._data = fGameManager.mloadConfig.AllMapEffectObj[nID];
		this.nowCheckNum = this._data.checkNum;
		string modelUrl = (fUIManager.LiZiModel == 2) ? this._data.model_little : this._data.model;
		this.modelUrl = modelUrl;
		this.modelName = string.Format("MapEffect_{0}", this.id);
		this.enemyLayer = LayerManager.EnemyLayer(force);
		bool flag = base.PlayerCreated();
		if (flag)
		{
			this.LoadStyle(this.modelUrl);
		}
		else
		{
			InteractionManager.Instance.addDict(this);
		}
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x000F446E File Offset: 0x000F266E
	public override void continueInit()
	{
		base.continueInit();
		this.LoadStyle(this.modelUrl);
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x000F4488 File Offset: 0x000F2688
	private void LoadStyle(string sStyle)
	{
		string url = "EffectRes/" + sStyle + ".ef";
		GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.InitStyle), this.modelName);
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x000F44C0 File Offset: 0x000F26C0
	public void InitStyle(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || this.isNetDead;
		if (!flag)
		{
			bool flag2 = false;
			bool flag3 = this.thisGameObject == null;
			if (flag3)
			{
				this.thisGameObject = new GameObject((string)sStyle);
				this.thisGameObject.tag = "can_interaction";
			}
			bool flag4 = this.thisModel != null;
			if (flag4)
			{
				UnityEngine.Object.Destroy(this.thisModel);
			}
			bool flag5 = this.parentId >= 0;
			if (flag5)
			{
				NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(this.parentId);
				NetLiving netLiving = netObj as NetLiving;
				bool flag6 = netLiving.thisModel != null;
				if (flag6)
				{
					Vector3 position = netLiving.thisModel.transform.position;
					position.y += this._data.model_height;
					this.thisModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), position, Quaternion.identity);
					this.thisModel.transform.parent = this.thisGameObject.transform;
					this.thisGameObject.transform.parent = netLiving.thisModel.transform;
					flag2 = true;
				}
			}
			bool flag7 = !flag2;
			if (flag7)
			{
				Vector3 position2 = this.vBorn;
				position2.y += this._data.model_height;
				this.thisModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), position2, Quaternion.identity);
				this.thisModel.transform.parent = this.thisGameObject.transform;
			}
			this.thisModel.transform.localScale = this._data.model_size;
			this.thisModel.transform.localRotation = Quaternion.Euler(this._rot.x, this._rot.y, this._rot.z);
			this.publicInfo = this.thisGameObject.AddComponent<MapEffectInfo>();
			this.publicInfo.InitInfo(this);
			bool flag8 = this._data.triggerCollide > 0;
			if (flag8)
			{
				Rigidbody rigidbody = this.thisGameObject.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
			bool flag9 = this._data.getStayNetObj == 1;
			if (flag9)
			{
				this.mEBox = this.thisGameObject.AddComponent<MapEffectBox>();
				this.mEBox.Init(this);
			}
			bool flag10 = this._data.musicsound != "0";
			if (flag10)
			{
				string url = "SoundRes/" + this._data.musicsound + ".sds";
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.musicsoundInit), 0);
			}
			bool flag11 = this._data.animationsound != "0";
			if (flag11)
			{
				this.loadTriggerSound(0);
			}
			bool flag12 = !fUIManager.GetInstance().MapEffecyObj.ContainsKey(this.modelName);
			if (flag12)
			{
				fUIManager.GetInstance().MapEffecyObj.Add(this.modelName, this.thisGameObject);
			}
			else
			{
				fUIManager.GetInstance().MapEffecyObj[this.modelName] = this.thisGameObject;
			}
			fUIManager.GetInstance().SetMapEffect();
		}
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x000F4854 File Offset: 0x000F2A54
	private void musicsoundInit(WWW www, object Param)
	{
		bool flag = www != null && www.error == null && !(this.thisModel == null) && !this.isDead && !this.isNetDead;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), this.thisModel.transform.position, Quaternion.identity);
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.transform.parent = this.thisModel.transform;
			component.Stop();
			CSoundManager.Instance.SetSceneSoundMixer(component);
			component.Play();
		}
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x000F4910 File Offset: 0x000F2B10
	public void loadTriggerSound(int type)
	{
		bool flag = this.triggerSound == null;
		if (flag)
		{
			string url = "SoundRes/" + this._data.animationsound + ".sds";
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.triggerInit), type);
		}
		else
		{
			this.playTriggerSound();
		}
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x000F4970 File Offset: 0x000F2B70
	private void triggerInit(WWW www, object Param)
	{
		bool flag = www != null && www.error == null && !(this.thisModel == null) && !this.isDead && !this.isNetDead;
		if (flag)
		{
			int num = (int)Param;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), this.thisModel.transform.position, Quaternion.identity);
			this.triggerSound = gameObject.GetComponent<AudioSource>();
			this.triggerSound.playOnAwake = false;
			this.triggerSound.Stop();
			this.triggerSound.transform.parent = this.thisModel.transform;
			CSoundManager.Instance.SetSceneSoundMixer(this.triggerSound);
			bool flag2 = num != 0;
			if (flag2)
			{
				this.playTriggerSound();
			}
		}
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x000F4A58 File Offset: 0x000F2C58
	private void playTriggerSound()
	{
		bool flag = !(this.triggerSound == null);
		if (flag)
		{
			this.triggerSound.Play();
		}
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x000F4A87 File Offset: 0x000F2C87
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.OnNetDestroy();
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x000F4A94 File Offset: 0x000F2C94
	public override void OnNetDestroy()
	{
		this.isNetDead = true;
		bool flag = this.thisGameObject != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.thisGameObject);
		}
		this.thisGameObject = null;
		this.thisModel = null;
		this._data = null;
		base.OnNetDestroy();
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x000F4AE4 File Offset: 0x000F2CE4
	public void CheckPlayer()
	{
		bool flag = !(this.thisModel == null) && !this.isNetDead;
		if (flag)
		{
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			bool flag2 = player != null && !(player.thisModel == null);
			if (flag2)
			{
				this.heroAIObjectForSearch = null;
				float num = Vector3.Distance(player.thisModel.transform.position, this.thisModel.transform.position);
				player.SendMessageToServer(40002, new ArrayList
				{
					base.getNetid(),
					(int)num
				});
			}
		}
	}

	// Token: 0x04001456 RID: 5206
	public GameObject thisGameObject;

	// Token: 0x04001457 RID: 5207
	public NetLiving heroAIObjectForSearch;

	// Token: 0x04001458 RID: 5208
	public MapEffectData _data;

	// Token: 0x04001459 RID: 5209
	public MapEffectBox mEBox;

	// Token: 0x0400145A RID: 5210
	public int x;

	// Token: 0x0400145B RID: 5211
	public int y;

	// Token: 0x0400145C RID: 5212
	public int z;

	// Token: 0x0400145D RID: 5213
	public int id;

	// Token: 0x0400145E RID: 5214
	public Vector3 vBorn;

	// Token: 0x0400145F RID: 5215
	public int enemyLayer;

	// Token: 0x04001460 RID: 5216
	public int parentId = -1;

	// Token: 0x04001461 RID: 5217
	public AudioSource triggerSound;

	// Token: 0x04001462 RID: 5218
	private Vector3 _rot;

	// Token: 0x04001463 RID: 5219
	public int nowCheckNum;

	// Token: 0x04001464 RID: 5220
	public float nowCd;

	// Token: 0x04001465 RID: 5221
	private bool isNetDead;
}

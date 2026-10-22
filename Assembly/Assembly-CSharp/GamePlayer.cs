using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200013C RID: 316
public class GamePlayer : NetLiving
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000778FC File Offset: 0x00075AFC
	// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00077914 File Offset: 0x00075B14
	public bool Hero_Suicide_State
	{
		get
		{
			return this._Hero_Suicide_State;
		}
		set
		{
			this._Hero_Suicide_State = value;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00077920 File Offset: 0x00075B20
	// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00077938 File Offset: 0x00075B38
	public override GameObject thisModel
	{
		get
		{
			return this.UserControl;
		}
		set
		{
			this.UserControl = value;
		}
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00077942 File Offset: 0x00075B42
	public GamePlayer()
	{
		this.netobject_type = 1;
		this.filmObj = true;
		this.bePlayer = true;
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00077968 File Offset: 0x00075B68
	public override void OnCreate()
	{
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x0007796C File Offset: 0x00075B6C
	public virtual void ChangeHeroDestroy()
	{
		base.Clear_Adhere_Npc();
		CCOpyNameManager.Instance.removeGetName(this);
		this.playerHaveCreate = false;
		bool flag = this.wc != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.wc);
			this.wc = null;
		}
		bool flag2 = this.userRoot != null;
		if (flag2)
		{
			UnityEngine.Object.Destroy(this.userRoot);
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000779D8 File Offset: 0x00075BD8
	public void addPlayer()
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 11;
		if (flag)
		{
			WarPersonManager.Instance.PersonChange();
			bool isMainPlayer = this.isMainPlayer;
			if (isMainPlayer)
			{
				WarPersonManager.Instance.HeroInto();
			}
		}
		this.CreatePlayer();
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00077A28 File Offset: 0x00075C28
	protected override void setHeroForce()
	{
		base.setHeroForce();
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 11 || MatchManager.GetInstance().m_nMatchType == 4;
		if (flag)
		{
			bool flag2 = base.getNetid() == fGameManager.Heroplayer.getNetid();
			if (flag2)
			{
				this.playerCommon.m_nShili = 1;
			}
			else
			{
				this.playerCommon.m_nShili = 2;
			}
		}
		this.nForce = this.playerCommon.m_nShili;
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00077AAC File Offset: 0x00075CAC
	private void LoadAnimatorParm()
	{
		bool isMainPlayer = this.isMainPlayer;
		if (!isMainPlayer)
		{
			foreach (CAnimatorParm canimatorParm in this.animatorParmObject.Values)
			{
				bool flag = canimatorParm.isstate == 0;
				if (flag)
				{
					((TBUserControlIt)this.charMove).SAGet(canimatorParm.ani, canimatorParm.type, canimatorParm.parm);
				}
				else
				{
					((TBUserControlIt)this.charMove).SetAnimatorUpdate(canimatorParm.ani, canimatorParm.type, canimatorParm.parm);
				}
			}
			bool flag2 = this.isCopy && this.cAnimationData != null && this.cAnimationData.Count > 0;
			if (flag2)
			{
				CAData[] array = new CAData[this.cAnimationData.Count];
				this.cAnimationData.Values.CopyTo(array, 0);
				Array.Sort<CAData>(array, new CADataSort());
				for (int i = 0; i < array.Length; i++)
				{
					object[] data = array[i].data;
					this.CA((ArrayList)data[0]);
				}
			}
		}
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00077C00 File Offset: 0x00075E00
	public void CA(ArrayList aryParam)
	{
		bool flag = !this.isMainPlayer && (int)aryParam[2] == this.loadID;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)aryParam[0];
			bool flag2 = (int)aryParam[1] == 0;
			if (flag2)
			{
				((TBUserControlIt)this.charMove).SAGet((string)arrayList[0], (int)arrayList[1], arrayList[2]);
			}
			else
			{
				((TBUserControlIt)this.charMove).SetAnimatorUpdate((string)arrayList[0], (int)arrayList[1], arrayList[2]);
			}
			base.UpdateAnimatorParm(aryParam);
		}
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00077CC4 File Offset: 0x00075EC4
	public override void SetAnimatorUpdate(string ani, int type, object parm)
	{
		bool flag = this.tCharAnimationTps != null;
		if (flag)
		{
			this.tCharAnimationTps.SetAnimator(ani, type, parm);
		}
		bool flag2 = this.isCopy && this.tCharAnimationFps != null;
		if (flag2)
		{
			this.tCharAnimationFps.SetAnimator(ani, type, parm);
		}
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00077D20 File Offset: 0x00075F20
	public void RemoteCreate(ArrayList szCom)
	{
		this.InitPlayerInfo((ArrayList)szCom[0], (ArrayList)szCom[1], false);
		bool flag = this.isCopy && ArenaCon.inst != null && ArenaCon.inst.Fight_State;
		if (flag)
		{
			this.isDead = false;
		}
		bool flag2 = this.playerCommon.m_nGM <= 0 || this.playerCommon.m_szGMTran.Count <= 0;
		if (flag2)
		{
			this.addPlayer();
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00077DB1 File Offset: 0x00075FB1
	public override void PlayerSelect(ArrayList aryNetInfo)
	{
		base.PlayerSelect(aryNetInfo);
		this.RemoteCreate(aryNetInfo);
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00077DC4 File Offset: 0x00075FC4
	public void CreatePlayer()
	{
		this.ChangeHeroDestroy();
		bool haveDestroy = this.haveDestroy;
		if (!haveDestroy)
		{
			this.userRoot = new GameObject(string.Format("User_{0}", base.getNetid()));
			this.userRoot.transform.position = Vector3.zero;
			this.userRoot.transform.eulerAngles = Vector3.zero;
			this.publicInfo = this.userRoot.AddComponent<PlayerInfo>();
			this.publicInfo.InitInfo(this);
			this.UserControl = new GameObject("control_IgnoreDamge");
			this.UserControl.layer = LayerMask.NameToLayer("Default");
			this.UserControl.transform.parent = this.userRoot.transform;
			bool flag = this.playerCommon.posCreate != Vector3.zero;
			if (flag)
			{
				this.playerCommon.pos = this.playerCommon.posCreate;
				this.playerCommon.posCreate = Vector3.zero;
			}
			this.UserControl.transform.position = this.playerCommon.pos;
			this.UserControl.transform.eulerAngles = new Vector3(0f, this.playerCommon.m_nForward, 0f);
			Rigidbody rigidbody = this.UserControl.AddComponent<Rigidbody>();
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			CapsuleCollider capsuleCollider = this.UserControl.AddComponent<CapsuleCollider>();
			capsuleCollider.radius = 0.4f;
			capsuleCollider.height = 1.8f;
			Vector3 center = capsuleCollider.center;
			center.y = 0.9f;
			capsuleCollider.center = center;
			this.defaultRigidbody = rigidbody;
			this.defaultCollider = capsuleCollider;
			this.defaultMesh = new GameObject("defaultMesh_IgnoreDamge");
			this.defaultMesh.transform.parent = this.UserControl.transform;
			this.defaultMesh.transform.localPosition = center + new Vector3(0f, 0.3f, 0f);
			this.defaultMesh.transform.localScale = Vector3.one * 0.3f;
			this.defaultMesh.transform.localEulerAngles = new Vector3(45f, 0f, 45f);
			this.bodyCollider = this.defaultMesh.AddComponent<BoxCollider>();
			this.bodyCollider.isTrigger = true;
			this.bodyRigidbody = this.defaultMesh.AddComponent<Rigidbody>();
			this.bodyRigidbody.isKinematic = true;
			this.defaultMesh.layer = LayerMask.NameToLayer("Default");
			GameObject gameObject = new GameObject("UserHeadTarget");
			this.UserHeadTarget = gameObject.transform;
			this.UserHeadTarget.parent = this.UserControl.transform;
			this.UserHeadTarget.localPosition = Vector3.zero;
			this.UserHeadTarget.localEulerAngles = Vector3.zero;
			this.UserHeadTarget.localScale = Vector3.one;
			GameObject gameObject2 = new GameObject("cameraTarget");
			this.camTarget = gameObject2.transform;
			this.camTarget.parent = this.UserControl.transform;
			this.camTarget.localPosition = this.heroData.TPSCameraPos[1];
			this.camTarget.localEulerAngles = Vector3.zero;
			this.FindMubiaoTarget = new GameObject("FindMubiaoTarget");
			this.FindMubiaoTarget.transform.parent = this.camTarget;
			this.FindMubiaoTarget.transform.localPosition = Vector3.zero;
			this.FindMubiaoTarget.transform.localEulerAngles = Vector3.zero;
			this.playerTarget = new GameObject("PlayerTarget");
			this.playerTarget.transform.parent = this.userRoot.transform;
			this.playerTarget.transform.position = this.camTarget.position + this.camTarget.forward * 200f;
			this._playerSoundContainer = new GameObject("_playerSoundContainer");
			this._playerSoundContainer.transform.parent = this.UserControl.transform;
			this._playerSoundContainer.transform.localPosition = new Vector3(0f, 1f, 1f);
			this._playerSoundContainer.transform.localEulerAngles = Vector3.zero;
			this._playerEffectContainer = new GameObject("_playerEffectContainer");
			this._playerEffectContainer.transform.parent = this.UserControl.transform;
			this._playerEffectContainer.transform.localPosition = Vector3.zero;
			this._playerEffectContainer.transform.localEulerAngles = Vector3.zero;
			this._playerRootEffectContainer = new GameObject("_playerRootEffectContainer");
			this._playerRootEffectContainer.transform.parent = this.userRoot.transform;
			this._playerRootEffectContainer.transform.localPosition = Vector3.zero;
			this._playerRootEffectContainer.transform.localEulerAngles = Vector3.zero;
			bool isMainPlayer = this.isMainPlayer;
			if (isMainPlayer)
			{
				this.playerSound = this._playerSoundContainer.AddComponent<CPlayerSoundMe>();
				this.playerSound.initPlayerSound(this);
			}
			else
			{
				int num = CFilmManager.Instance.filmForce(this);
				bool flag2 = this.playerCommon.m_nShiliServer == num;
				if (flag2)
				{
					this.playerSound = this._playerSoundContainer.AddComponent<CPlayerSoundItFriend>();
				}
				else
				{
					this.playerSound = this._playerSoundContainer.AddComponent<CPlayerSoundItEnemy>();
				}
				this.playerSound.initPlayerSound(this);
			}
			this.CreateDeadBegin();
			this.skillInfo = this.UserControl.AddComponent<SkillInfo>();
			this.skillInfo.InitSkills(this, this.heroData.skillshow.heroAllSkills);
			this.playerDazhao = this.UserControl.AddComponent<CPlayerDazhao>();
			this.OnInitAllUserDaZhao(this.playerCommon.aryheroDazhao);
			bool flag3 = !this.isCopy;
			if (flag3)
			{
				User_Data_Handle.Add_User_Data(this.playerCommon.m_nShili, base.getNetid(), this);
			}
			this.InitUserControlScript();
			this.addMySelfCompent();
			bool flag4 = CameraControl.Instance.NowTargetNetObj != null && CameraControl.Instance.NowTargetNetObj.getNetid() == base.getNetid();
			if (flag4)
			{
				CCameraChange.Instance.changeCameraTarget();
			}
		}
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00078440 File Offset: 0x00076640
	public void LoadModelFps()
	{
		bool flag = CFilmManager.Instance.filmParentNetId(this) == base.getNetid() || (fGameManager.Heroplayer.playerCommon.m_szGMTran.Count > 1 && (int)fGameManager.Heroplayer.playerCommon.m_szGMTran[1] == this.playerCommon.m_nCid);
		if (flag)
		{
			string url = "HeroAnimator/" + this.playerCommon.GetNowModelData()[2] + ".mo";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadModelFpsFinish), this.loadID);
			}
			else
			{
				this.LoadModelFpsFinish(resWWW, this.loadID);
			}
		}
		else
		{
			this.ModeLoadOK();
		}
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0007851C File Offset: 0x0007671C
	private void LoadModelFpsFinish(WWW www, object obj)
	{
		bool flag = www == null || www.error != null || (int)obj != this.loadID || this.haveDestroy;
		if (!flag)
		{
			this.userModelFps = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this.userModelFps.name = "model_fps";
			bool flag2 = this.userAnimatorFps != null;
			if (flag2)
			{
				Animator component = this.userModelFps.GetComponent<Animator>();
				component.runtimeAnimatorController = this.userAnimatorFps;
			}
			else
			{
				Debug.LogError("没找到模型的TPS动画文件" + (string)obj);
			}
			this.userModelFps.transform.parent = this.camTarget.transform;
			this.userModelFps.transform.localPosition = this.heroData.FPSModelPos[1];
			this.userModelFps.transform.rotation = this.camTarget.transform.rotation;
			this.fpsRender = this.userModelFps.GetComponent<CHeroRender>();
			bool flag3 = this.fpsRender != null;
			if (flag3)
			{
				this.fpsRender.GetRenders();
			}
			Setting_Quality_FPS_Model component2 = this.userModelFps.GetComponent<Setting_Quality_FPS_Model>();
			bool flag4 = component2 != null;
			if (flag4)
			{
				component2.Initialize();
				component2.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
			CHeroWeaponDrop component3 = this.userModelFps.GetComponent<CHeroWeaponDrop>();
			bool flag5 = component3 != null;
			if (flag5)
			{
				component3.InitDrop(this);
			}
			Type type = Type.GetType("TBCharAnimation" + this.heroData.heroclass + "Fps");
			this.tCharAnimationFps = (TBCharAnimationPlay)this.userModelFps.AddComponent(type);
			this.tCharAnimationFps.InitFpsPlay();
			Rigidbody rigidbody = this.userModelFps.AddComponent<Rigidbody>();
			rigidbody.isKinematic = true;
			int layer = LayerMask.NameToLayer("FrontCamera");
			Renderer[] componentsInChildren = this.userModelFps.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
				componentsInChildren[i].shadowCastingMode = ShadowCastingMode.Off;
			}
			this.setFpsLayer();
			this.ModeLoadOK();
			this.aniEventFps = this.userModelFps.AddComponent<AniEvent>();
			this.aniEventFps.InitEvent(this);
			this.FPSrenderBianSe = this.userModelFps.AddComponent<CMeshRenderBianSe>();
			this.FPSrenderBianSe.InitBianSe(this);
			this.FPSrenderBianSe.ChangeYinShenFPSMat(2);
			bool flag6 = CFilmManager.Instance.filmNetId(this) != base.getNetid();
			if (flag6)
			{
				this.userModelFps.SetActive(false);
			}
			bool flag7 = fGameManager.IsSingle && PlayerLearnManager.GetInstance().HideHero;
			if (flag7)
			{
				this.userModelFps.SetActive(false);
			}
			CHeroWeapon component4 = this.userModelFps.GetComponent<CHeroWeapon>();
			bool flag8 = component4 != null;
			if (flag8)
			{
				component4.SetHeroData(this.heroData.id, this.playerCommon.m_nCid);
				component4.SetHeroWeapon(1, -1, 0);
			}
			bool gm_State = GameRoot.inst.Setting_FPS.GM_State;
			if (gm_State)
			{
				this.tpsRender.SetRenderMode(false);
				bool flag9 = this.fpsRender != null;
				if (flag9)
				{
					this.fpsRender.SetParticleSystem(false);
				}
				bool flag10 = this.userModelFps != null && this.userModelFps.activeSelf;
				if (flag10)
				{
					this.userModelFps.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x000788DC File Offset: 0x00076ADC
	public void LoadModelTps()
	{
		string url = "HeroAnimator/" + this.playerCommon.GetNowModelData()[0] + ".mo";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadModelTpsFinish), this.loadID);
		}
		else
		{
			this.LoadModelTpsFinish(resWWW, this.loadID);
		}
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00078950 File Offset: 0x00076B50
	private void LoadModelTpsFinish(WWW www, object obj)
	{
		bool flag = (int)obj != this.loadID || www == null || www.error != null || this.haveDestroy;
		if (!flag)
		{
			this.userModelTps = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this.userModelTps.name = "model_tps";
			bool flag2 = this.userAnimatorTps != null;
			if (flag2)
			{
				Animator component = this.userModelTps.GetComponent<Animator>();
				component.runtimeAnimatorController = this.userAnimatorTps;
			}
			else
			{
				Debug.LogError("没找到模型的TPS动画文件" + (string)obj);
			}
			this.userModelTps.transform.parent = this.UserControl.transform;
			this.userModelTps.transform.localPosition = Vector3.zero;
			this.userModelTps.transform.rotation = this.UserControl.transform.rotation;
			base.userSpine = Fun.FindGameObjectChild(this.userModelTps, "Bip001 Spine", false);
			base.userBip = Fun.FindGameObjectChild(this.userModelTps, "Bip001", false);
			base.userBip.transform.localPosition = Vector3.zero;
			this.renderBianSe = this.userModelTps.AddComponent<CMeshRenderBianSe>();
			this.renderBianSe.InitBianSe(this);
			this.renderBianSe.ChangeMateial(true);
			this.renderBianSe.ChangeYinShenMat(2);
			bool flag3 = !this.isCopy;
			if (flag3)
			{
				bool flag4 = fGameManager.Heroplayer.nForce == this.nForce && !this.isMainPlayer;
				if (flag4)
				{
					this.renderBianSe.ChangeHealerSeeThroughMat();
				}
			}
			else
			{
				bool flag5 = CFilmManager.Instance.playerForce == this.nForce && CFilmManager.Instance.filmparentnetid != base.getNetid();
				if (flag5)
				{
					this.renderBianSe.ChangeHealerSeeThroughMat();
				}
			}
			this.hitReaction = this.userModelTps.AddComponent<CHitReaction>();
			bool flag6 = this.hitReaction == null;
			if (flag6)
			{
				Debug.LogError("没加受伤动作!");
			}
			else
			{
				this.hitReaction.HitReactionInit(this);
			}
			CHeroWeaponDrop component2 = this.userModelTps.GetComponent<CHeroWeaponDrop>();
			bool flag7 = component2 != null;
			if (flag7)
			{
				component2.InitDrop(this);
			}
			Type type = Type.GetType("TBCharAnimation" + this.heroData.heroclass + "Tps");
			this.tCharAnimationTps = (TBCharAnimationThirdIK)this.userModelTps.AddComponent(type);
			base.Get_Stroke_Material_Array();
			bool gm_State = GameRoot.inst.Setting_FPS.GM_State;
			if (gm_State)
			{
				base.Open_Or_Close_Stroke_Material(false);
			}
			this.tpsRender = this.userModelTps.GetComponent<CHeroRender>();
			bool flag8 = this.tpsRender != null;
			if (flag8)
			{
				this.tpsRender.GetRenders();
			}
			else
			{
				Debug.LogError("这个角色" + this.userRoot.name + "动画没找到CHeroRender脚本控制.");
			}
			CHeroWeapon component3 = this.userModelTps.GetComponent<CHeroWeapon>();
			bool flag9 = component3 != null;
			if (flag9)
			{
				component3.SetHeroData(this.heroData.id, this.playerCommon.m_nCid);
				component3.SetHeroWeapon(3, -1, 0);
			}
			bool flag10 = CFilmManager.Instance.filmNetId(this) == base.getNetid() && this.tpsRender != null;
			if (flag10)
			{
				this.tpsRender.SetRenderMode(true);
			}
			this.AddHeadLook();
			this.setTpsLayer();
			this.aniEvent = this.userModelTps.AddComponent<AniEvent>();
			this.aniEvent.InitEvent(this);
			this.LoadFpsAnimator();
		}
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00078D24 File Offset: 0x00076F24
	public void AddReviveEff()
	{
		bool flag = this.fuhuoEffObj != null;
		if (flag)
		{
			this.fuhuoEffObj.GetComponentInChildren<ParticleSystem>().Play();
		}
		else
		{
			WWW resWWW = GameRoot.GetResWWW("EffectRes/fuhuo_guanxiao.ef");
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset("EffectRes/fuhuo_guanxiao.ef", new fResourceManager.DownFinishDelegate(this.loadReviveEffPrefab), 0);
			}
			else
			{
				this.loadReviveEffPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00078D9C File Offset: 0x00076F9C
	private void loadReviveEffPrefab(WWW www, object p)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			UnityEngine.Object original = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.fuhuoEffObj = (GameObject)UnityEngine.Object.Instantiate(original);
			this.fuhuoEffObj.transform.SetParent(this.userModelTps.transform);
			this.fuhuoEffObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this.fuhuoEffObj.transform.localScale = Vector3.one;
			this.fuhuoEffObj.transform.localPosition = Vector3.zero;
			this.fuhuoEffObj.SetActive(true);
		}
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00078E5C File Offset: 0x0007705C
	protected override void ModeLoadOK()
	{
		this.playerHaveCreate = true;
		this._Create_Time = Time.time + 10f;
		bool flag = !this.isMainPlayer && this.newplayercon == null;
		if (flag)
		{
			this.initHeroUi();
		}
		base.InitHeroWeapon(this.playerCommon.m_nModelIndex);
		this.tChar.InitCharThird(this, this.tCharAnimationTps, this.charMove, this.tCharAnimationFps);
		this.LoadAnimatorParm();
		this.heroBase.InitForModelLoad();
		this.initActiveSkill();
		bool flag2 = this.playerBuffIt != null;
		if (flag2)
		{
			this.playerBuffIt.AddBuffList(this.playerCommon.m_szBuffEffectData);
		}
		base.ModeLoadOK();
		this.NetLivingCreateFinish();
		bool isDead = this.isDead;
		if (isDead)
		{
			this.CreateDead();
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00078F3C File Offset: 0x0007713C
	private void CreateDeadBegin()
	{
		bool isDead = this.isDead;
		if (isDead)
		{
			this.defaultCollider.enabled = false;
			this.defaultRigidbody.isKinematic = true;
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00078F70 File Offset: 0x00077170
	protected virtual void CreateDead()
	{
		this.tCharAnimationTps.animator.SetInteger("Dead", 1);
		bool flag = ArenaCon.inst != null && ArenaCon.inst.Fight_State;
		if (flag)
		{
			this.isInitDead = true;
		}
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00078FBC File Offset: 0x000771BC
	public void RefreshNewModel(ArrayList aryNetMsg)
	{
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00078FC0 File Offset: 0x000771C0
	protected virtual void addMySelfCompent()
	{
		this.publicInfo.type = "GamePlayer";
		this.tChar = this.UserControl.AddComponent<TBCharacterThirdIt>();
		this.addSkillCompent();
		base.addZhunxingCompent();
		this.charMove = this.UserControl.AddComponent<TBUserControlIt>();
		this.charMove.InitControl(this);
		this.playerBuffIt = this.UserControl.AddComponent<buffIt>();
		this.playerBuffIt.OneBuffInit(this);
		this.playerEffectBase = this.UserControl.AddComponent<effectMe>();
		this.playerEffectBase.gameplayer = this;
		this.LoadTpsAnimator();
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0007905E File Offset: 0x0007725E
	protected void CheckWeaponMat(int ntype)
	{
		this.nLoadWeaponType = ntype;
		this.szHeroWeaponMat = this.playerCommon.GetNowModelWeaponMat(ntype);
		this.nHeroWeaponCount = 0;
		this.LoadWeaponMat();
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00079088 File Offset: 0x00077288
	private void LoadWeaponMat()
	{
		bool flag = this.nHeroWeaponCount >= this.szHeroWeaponMat.Count;
		if (flag)
		{
			bool flag2 = this.nLoadWeaponType == 0;
			if (flag2)
			{
				this.LoadModelTps();
			}
			else
			{
				this.LoadModelFps();
			}
		}
		else
		{
			string url = "HeroAnimator/" + this.szHeroWeaponMat[this.nHeroWeaponCount] + ".mo";
			this.nHeroWeaponCount++;
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag3 = resWWW == null;
			if (flag3)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadWeaponMat), this.loadID);
			}
			else
			{
				this.LoadWeaponMat(resWWW, this.loadID);
			}
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0007914C File Offset: 0x0007734C
	private void LoadWeaponMat(WWW www, object obj)
	{
		bool flag = www != null && www.error == null && (int)obj == this.loadID && !this.haveDestroy;
		if (flag)
		{
			this.LoadWeaponMat();
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0007918C File Offset: 0x0007738C
	protected void LoadTpsAnimator()
	{
		string[] nowModelData = this.playerCommon.GetNowModelData();
		string url = "HeroAnimator/" + nowModelData[1] + ".mo";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadTpsAnimatorFinish), this.loadID);
		}
		else
		{
			this.LoadTpsAnimatorFinish(resWWW, this.loadID);
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00079200 File Offset: 0x00077400
	private void LoadTpsAnimatorFinish(WWW www, object obj)
	{
		bool flag = (int)obj == this.loadID && !this.haveDestroy && www != null && www.error == null;
		if (flag)
		{
			this.userAnimatorTps = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.CheckWeaponMat(0);
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00079264 File Offset: 0x00077464
	protected void LoadFpsAnimator()
	{
		bool flag = CFilmManager.Instance.filmParentNetId(this) == base.getNetid() || (fGameManager.Heroplayer.playerCommon.m_szGMTran.Count > 1 && (int)fGameManager.Heroplayer.playerCommon.m_szGMTran[1] == this.playerCommon.m_nCid);
		if (flag)
		{
			string[] nowModelData = this.playerCommon.GetNowModelData();
			string url = "HeroAnimator/" + nowModelData[3] + ".mo";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFpsAnimatorFinish), this.loadID);
			}
			else
			{
				this.LoadFpsAnimatorFinish(resWWW, this.loadID);
			}
		}
		else
		{
			this.ModeLoadOK();
		}
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00079344 File Offset: 0x00077544
	private void LoadFpsAnimatorFinish(WWW www, object obj)
	{
		bool flag = (int)obj == this.loadID && www != null && www.error == null && !this.haveDestroy;
		if (flag)
		{
			this.userAnimatorFps = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.CheckWeaponMat(1);
		}
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x000793A8 File Offset: 0x000775A8
	public void addSkillCompent()
	{
		this.skillEffectDict = new Dictionary<int, SkillBase>();
		this.addHeroCompent(this.heroData.heroclass);
		for (int i = 0; i < this.heroData.skillshow.heroAllSkills.Count; i++)
		{
			int num = this.heroData.skillshow.heroAllSkills[i];
			bool flag = !fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(num);
			if (flag)
			{
				Debug.LogError(string.Format("英雄初始化技能，发现技能配表没这个技能:{0}", num));
			}
			else
			{
				PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[num];
				this.addOneSkillCompent(num, playerSkillData.skillclass);
			}
		}
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00079464 File Offset: 0x00077664
	public override bool GetAnimatorBool(string ani)
	{
		bool flag = this.charMove == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			result = (!flag2 && this.tCharAnimationTps.GetAnimatorBool(ani));
		}
		return result;
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x000794AC File Offset: 0x000776AC
	public override float GetAnimatorFloat(string ani)
	{
		bool flag = this.charMove == null;
		float result;
		if (flag)
		{
			result = -999f;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			if (flag2)
			{
				result = -999f;
			}
			else
			{
				result = this.tCharAnimationTps.GetAnimatorFloat(ani);
			}
		}
		return result;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x000794FC File Offset: 0x000776FC
	public override int GetAnimatorInteger(string ani)
	{
		bool flag = this.charMove == null;
		int result;
		if (flag)
		{
			result = -999;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			if (flag2)
			{
				result = -999;
			}
			else
			{
				result = this.tCharAnimationTps.GetAnimatorInteger(ani);
			}
		}
		return result;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0007954C File Offset: 0x0007774C
	public virtual void AddHeadLook()
	{
		this.aimSystem = this.userModelTps.GetComponent<CAiming>();
		bool flag = this.aimSystem != null;
		if (flag)
		{
			this.aimSystem.InitAimSystem(this, this.playerTarget.transform);
			this.aimSystem.enabled = true;
		}
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x000795A2 File Offset: 0x000777A2
	public override void setPlayerNewPos(Vector3 v3Pos)
	{
		this.playerCommon.pos = v3Pos;
		this.UserControl.transform.position = v3Pos;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x000795C4 File Offset: 0x000777C4
	public void playerDeadAnim()
	{
		this.RollDead = false;
		bool flag = this.aimSystem != null;
		if (flag)
		{
			this.aimSystem.SetEnable(false);
			this.aimSystem.AimEnable(false);
		}
		bool flag2 = !(this.tCharAnimationTps == null);
		if (flag2)
		{
			this.tCharAnimationTps.animator.enabled = true;
			base.ChangeSpeed();
			this.tCharAnimationTps.animator.speed = 1f;
			this.tCharAnimationTps.animator.SetInteger("Dead", 1);
			bool flag3 = this.tpsRender != null;
			if (flag3)
			{
				this.tpsRender.SetParticleSystem(false);
			}
			this.tChar.DeadInit();
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0007968C File Offset: 0x0007788C
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		base.OnDeath(_attackId, o, forcev3, forcemax, weaponName, launcherName, m_nIsSupperKill, m_szCanYuXiaoMie, nDeadType, _warData);
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
		bool flag = netLiving != null && netLiving.netobject_type == 2;
		if (flag)
		{
			((Npc_Entity)netLiving).NpcKilled(this);
		}
		this.playerCommon.m_nPlayerState = 0;
		this.playerCommon.m_dPlayerDeathDate = Fun.GetServerDate();
		this.playerCommon.m_dPlayerDeathChangeHeroDate = this.playerCommon.m_dPlayerDeathDate.AddSeconds(5.0);
		this.playerCommon.m_nDeathTimeLong = o;
		this.playerCommon.m_nDeathAttackId = _attackId;
		this.playerCommon.m_nDeadType = nDeadType;
		this.bodyCollider.enabled = false;
		int ntype = 0;
		bool hero_Suicide_State = this.Hero_Suicide_State;
		if (hero_Suicide_State)
		{
			ntype = 1;
			this.Hero_Suicide_State = false;
		}
		this.playerSound.PlayDeadSound(ntype);
		bool flag2 = this.playerEffectBase != null;
		if (flag2)
		{
			this.playerEffectBase.OnDeadClearEffect();
		}
		this.heroBase.OnDeath();
		bool flag3 = this.userModelTps != null;
		if (flag3)
		{
			Collider[] componentsInChildren = this.userModelTps.GetComponentsInChildren<Collider>();
			foreach (Collider collider in componentsInChildren)
			{
				if (collider != null)
				{
					collider.enabled = true;
					collider.isTrigger = false;
					collider.gameObject.layer = 0;
				}
			}
		}
		bool flag5 = this.playerHaveCreate || !this.haveDestroy;
		if (flag5)
		{
			base.Open_Or_Close_Stroke_Material(false);
		}
		this.defaultCollider.enabled = false;
		this.defaultRigidbody.isKinematic = true;
		Vector3 fV = new Vector3((float)((int)forcev3[0]) * 0.01f, (float)((int)forcev3[1]) * 0.01f, (float)((int)forcev3[2]) * 0.01f);
		float fMax = (float)forcemax * 0.01f;
		this.publicInfo.playerDeadRagdoll(fV, fMax);
		bool flag7 = GameRoot.inst.IsGmTarget(this);
		if (flag7)
		{
			base.changeCameraState(CameraState.ThirdState, null, string.Empty);
		}
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0007993B File Offset: 0x00077B3B
	public override void OnFightStat(int m_nFightStat, int m_nFightID)
	{
		base.OnFightStat(m_nFightStat, m_nFightID);
		this.playerCommon.m_nFightStat = m_nFightStat;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00079954 File Offset: 0x00077B54
	public void setReviveData(ArrayList szNetMsg)
	{
		this.playerCommon.pos = new Vector3((float)((int)szNetMsg[0]) * 0.01f, (float)((int)szNetMsg[2]) * 0.01f, (float)((int)szNetMsg[1]) * 0.01f);
		this.playerCommon.posCreate = this.playerCommon.pos;
		this.playerCommon.m_nForward = (float)((int)szNetMsg[3]) * 1f;
		this.playerCommon.m_nPlayerState = 1;
		bool flag = !this.isMainPlayer && this.charMove != null;
		if (flag)
		{
			((TBUserControlIt)this.charMove).moveTarget = this.playerCommon.pos;
		}
		this.UserControl.transform.position = this.playerCommon.pos;
		this.UserControl.transform.eulerAngles = new Vector3(0f, this.playerCommon.m_nForward, 0f);
		this.camTarget.localPosition = this.heroData.TPSCameraPos[1];
		this.camTarget.localEulerAngles = Vector3.zero;
		this.playerTarget.transform.position = this.camTarget.position + this.camTarget.forward * 200f;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00079AD0 File Offset: 0x00077CD0
	public override void OnRevive(ArrayList szNetMsg)
	{
		base.OnRevive(szNetMsg);
		bool haveDestroy = this.haveDestroy;
		if (!haveDestroy)
		{
			this.OnReviveTime = Fun.GetServerDate();
			base.GameAttackPlayer = null;
			this.isInitDead = false;
			this.setReviveData(szNetMsg);
			bool flag = this.tCharAnimationTps == null || this.userModelTps == null;
			if (!flag)
			{
				this.CreateDeadHideOrShowTps(true);
				this.tCharAnimationTps.animator.SetInteger("Dead", 0);
				this.tCharAnimationTps.animator.enabled = true;
				this.bodyCollider.enabled = true;
				this.defaultCollider.enabled = true;
				this.defaultRigidbody.isKinematic = false;
				this.setTpsLayer();
				bool flag2 = this.playerHaveCreate || !this.haveDestroy;
				if (flag2)
				{
					base.Open_Or_Close_Stroke_Material(true);
				}
				this.tChar.onGround = true;
				this.tChar.onAir = false;
				Renderer[] componentsInChildren = this.userModelTps.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					bool flag3 = componentsInChildren[i].name.Contains("_toushi");
					if (flag3)
					{
						componentsInChildren[i].gameObject.layer = 0;
					}
				}
				bool flag4 = this.aimSystem != null;
				if (flag4)
				{
					this.aimSystem.SetEnable(true);
					this.aimSystem.AimEnable(true);
				}
				base.userBip.transform.localPosition = Vector3.zero;
				bool flag5 = this.userModelTps != null;
				if (flag5)
				{
					this.userModelTps.transform.localPosition = Vector3.zero;
				}
				this.tCharAnimationTps.SetAnimParm();
				bool flag6 = GameRoot.inst.IsGmTarget(this);
				if (flag6)
				{
					base.changeCameraState(CameraState.FirstState, null, string.Empty);
				}
			}
		}
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00079CC8 File Offset: 0x00077EC8
	public void setFpsLayer()
	{
		int layer = (this.playerCommon.m_nShili != 1) ? LayerMask.NameToLayer("force2_mainplayer") : LayerMask.NameToLayer("force1_mainplayer");
		this.userModelFps.layer = layer;
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x00079D08 File Offset: 0x00077F08
	public void setTpsLayer()
	{
		bool flag = this.playerCommon.m_nShili == 1;
		int layer;
		int layer2;
		if (flag)
		{
			layer = LayerMask.NameToLayer("force1_player");
			layer2 = LayerMask.NameToLayer("force1_mainplayer");
		}
		else
		{
			layer = LayerMask.NameToLayer("force2_player");
			layer2 = LayerMask.NameToLayer("force2_mainplayer");
		}
		bool flag2 = this.isMainPlayer || this.filmMainLayer;
		if (flag2)
		{
			this.UserControl.layer = layer2;
		}
		else
		{
			this.UserControl.layer = layer;
		}
		this.UserControl.name = "control_IgnoreDamge";
		this.UserControl.tag = "can_damage_human";
		this.defaultMesh.name = "defaultMesh_IgnoreDamge";
		this.defaultMesh.tag = "can_damage_human";
		this.defaultMesh.layer = layer;
		if (this.defaultCollider != null)
		{
			this.defaultCollider.enabled = true;
		}
		if (this.bodyCollider != null)
		{
			this.bodyCollider.gameObject.tag = "can_damage_human";
			this.bodyCollider.isTrigger = true;
			this.bodyCollider.enabled = true;
		}
		bool flag3 = this.userModelTps == null;
		if (!flag3)
		{
			Collider[] componentsInChildren = this.userModelTps.GetComponentsInChildren<Collider>();
			bool flag4 = false;
			this.colliderCankill = new Dictionary<string, GameObject>();
			bool flag5 = this.rigidbodyAll.Count == 0;
			if (flag5)
			{
				this.rigidbodyAll = new ArrayList();
				flag4 = true;
			}
			if (this.bodyCollider != null)
			{
				this.bodyCollider.gameObject.tag = "can_damage_human";
				this.bodyCollider.isTrigger = true;
				this.bodyCollider.enabled = true;
			}
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				bool flag7 = this.isMainPlayer || this.filmMainLayer;
				if (flag7)
				{
					componentsInChildren[i].gameObject.layer = layer2;
				}
				else
				{
					componentsInChildren[i].gameObject.layer = layer;
				}
				bool flag8 = !componentsInChildren[i].gameObject.name.Contains("huzhao");
				if (flag8)
				{
					componentsInChildren[i].gameObject.tag = "can_damage_human";
				}
				bool flag9 = componentsInChildren[i].gameObject.name.Contains("body2");
				if (flag9)
				{
					componentsInChildren[i].gameObject.tag = "can_damage_dun";
					componentsInChildren[i].gameObject.layer = layer;
					bool flag10 = componentsInChildren[i].GetComponent<Rigidbody>() != null;
					if (flag10)
					{
						componentsInChildren[i].GetComponent<Rigidbody>().isKinematic = true;
					}
					componentsInChildren[i].isTrigger = false;
				}
				bool flag11 = componentsInChildren[i].name.Contains("Head");
				if (flag11)
				{
					base.userHeadTransform = componentsInChildren[i].transform;
				}
				Rigidbody rigidbody = componentsInChildren[i].gameObject.GetComponent<Rigidbody>();
				bool flag12 = rigidbody == null;
				if (flag12)
				{
					rigidbody = componentsInChildren[i].gameObject.AddComponent<Rigidbody>();
					bool flag13 = componentsInChildren[i].gameObject.name.Contains("body");
					if (flag13)
					{
						rigidbody.isKinematic = true;
					}
				}
				rigidbody.isKinematic = true;
				CharacterJoint component = componentsInChildren[i].gameObject.GetComponent<CharacterJoint>();
				bool flag14 = component != null && flag4;
				if (flag14)
				{
					this.rigidbodyAll.Add(rigidbody);
				}
				bool flag15 = !this.colliderCankill.ContainsKey(componentsInChildren[i].name);
				if (flag15)
				{
					this.colliderCankill.Add(componentsInChildren[i].name, componentsInChildren[i].gameObject);
				}
			}
			Renderer[] componentsInChildren2 = this.userModelTps.GetComponentsInChildren<Renderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				bool flag16 = componentsInChildren2[j].name.Contains("_toushi");
				if (flag16)
				{
					componentsInChildren2[j].gameObject.layer = layer;
				}
			}
		}
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0007A0A8 File Offset: 0x000782A8
	public void CV(ArrayList aryParam)
	{
		bool flag = !this.isMainPlayer && !(this.tChar == null) && !this.tChar.onGround;
		if (flag)
		{
			bool flag2 = (int)aryParam[1] == 1;
			bool flag3 = this.isDead == flag2;
			if (flag3)
			{
				ArrayList arrayList = (ArrayList)aryParam[0];
				this.playerCommon.ve = Fun.ServerToClientPos((int)arrayList[0], (int)arrayList[1], (int)arrayList[2]);
			}
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0007A144 File Offset: 0x00078344
	public void CP(ArrayList aryParam)
	{
		ArrayList arrayList = (ArrayList)aryParam[0];
		bool flag = (int)aryParam[1] == 1;
		bool flag2 = this.isDead != flag;
		if (!flag2)
		{
			this.playerCommon.pos = Fun.ServerToClientPos((int)arrayList[0], (int)arrayList[1], (int)arrayList[2]);
			bool flag3 = !this.isMainPlayer;
			if (flag3)
			{
				bool flag4 = this.charMove != null;
				if (flag4)
				{
					((TBUserControlIt)this.charMove).moveTarget = this.playerCommon.pos;
				}
				else
				{
					this.UserControl.transform.position = this.playerCommon.pos;
				}
			}
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0007A218 File Offset: 0x00078418
	public void CR(ArrayList aryParam)
	{
		bool flag = !this.isMainPlayer;
		if (flag)
		{
			bool flag2 = (int)aryParam[1] == 1;
			bool flag3 = this.isDead == flag2;
			if (flag3)
			{
				this.playerCommon.rot = new Vector3(this.UserControl.transform.eulerAngles.x, (float)((int)aryParam[0]), this.UserControl.transform.eulerAngles.z);
			}
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0007A29C File Offset: 0x0007849C
	public void CT(ArrayList aryParam)
	{
		bool flag = !this.isMainPlayer;
		if (flag)
		{
			bool flag2 = (int)aryParam[1] == 1;
			bool flag3 = this.isDead == flag2 && !(this.playerTarget == null);
			if (flag3)
			{
				ArrayList arrayList = (ArrayList)aryParam[0];
				Vector3 vector = Fun.ServerToClientPos((int)arrayList[0], (int)arrayList[1], (int)arrayList[2]);
				this.playerTarget.transform.position = vector;
				((TBUserControlIt)this.charMove).lookAt = vector;
			}
		}
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0007A34C File Offset: 0x0007854C
	public override void AddBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffIt == null);
		if (flag)
		{
			this.playerBuffIt.AddOneBuff((string)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0007A3A0 File Offset: 0x000785A0
	public override void DeleteBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffIt == null);
		if (flag)
		{
			this.playerBuffIt.DeleteOneBuff((int)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0007A3F4 File Offset: 0x000785F4
	public override void LLS_FlyShieldData(ArrayList arrPam)
	{
		bool flag = this.skillEffectDict.ContainsKey(1163);
		if (flag)
		{
			((Skill_Lilisi_shiledIt)this.skillEffectDict[1163]).UpdateFlyShiled(arrPam);
		}
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0007A434 File Offset: 0x00078634
	public virtual void initActiveSkill()
	{
		foreach (int key in this.skillEffectDict.Keys)
		{
			SkillBase skillBase = this.skillEffectDict[key];
			skillBase.ModelLoaded();
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0007A4A0 File Offset: 0x000786A0
	public virtual void SkillBreakAll()
	{
		foreach (int key in this.skillEffectDict.Keys)
		{
			SkillBase skillBase = this.skillEffectDict[key];
			skillBase.BreakSkill();
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0007A50C File Offset: 0x0007870C
	public virtual void addHeroCompent(string className)
	{
		bool flag = className != "0";
		if (flag)
		{
			Type type = Type.GetType("Hero" + className + "It");
			this.heroBase = (HeroBaseIt)this.UserControl.AddComponent(type);
		}
		else
		{
			this.heroBase = this.UserControl.AddComponent<HeroBaseIt>();
		}
		this.heroBase.HeroInit(this, className);
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0007A57C File Offset: 0x0007877C
	public virtual void addOneSkillCompent(int iSkillIndex, string className)
	{
		bool flag = className != "0";
		string text;
		Type type;
		if (flag)
		{
			text = className + "It";
			type = Type.GetType(text);
		}
		else
		{
			text = string.Format("{0}It", "SkillBase");
			type = Type.GetType(text);
		}
		bool flag2 = type == null;
		if (flag2)
		{
			Debug.LogError("不存在的技能类名称:" + text);
		}
		SkillBaseIt skillBaseIt = (SkillBaseIt)this.UserControl.AddComponent(type);
		skillBaseIt.SkillInit(this, iSkillIndex);
		this.skillEffectDict.Add(iSkillIndex, skillBaseIt);
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0007A614 File Offset: 0x00078814
	public void SkillParmsUpdate(ArrayList list)
	{
		int num = (int)list[0];
		int key = (int)list[1];
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = (num != player.getNetid() || this.isCopy) && this.skillEffectDict.ContainsKey(key);
		if (flag)
		{
			SkillBaseIt skillBaseIt = (SkillBaseIt)this.skillEffectDict[key];
			skillBaseIt.OneSkillActionUpdate(list);
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0007A68C File Offset: 0x0007888C
	public override void OnNetDestroy()
	{
		bool flag = this.UserControl != null;
		if (flag)
		{
			MeBuffModelSign component = this.UserControl.GetComponent<MeBuffModelSign>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.OnDel();
			}
		}
		bool flag3 = this.newplayercon != null;
		if (flag3)
		{
			this.newplayercon.OnDel();
		}
		bool flag4 = this.userRoot != null;
		if (flag4)
		{
			UnityEngine.Object.Destroy(this.userRoot);
		}
		base.OnNetDestroy();
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0007A714 File Offset: 0x00078914
	public override void SkillDo(ArrayList parm, ArrayList parm2)
	{
		int num = (int)parm[0];
		int num2 = CFilmManager.Instance.filmForce(this);
		bool flag = num2 != this.playerCommon.m_nShiliServer;
		if (flag)
		{
			this.skillInfo.SkillTalk(num, false);
		}
		else
		{
			this.skillInfo.SkillTalk(num, true);
		}
		this.skillInfo.SkillAnimation(num);
		bool flag2 = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(num);
		if (flag2)
		{
			this.SkillAction(num, parm, parm2);
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0007A7A0 File Offset: 0x000789A0
	public virtual void SkillAction(int iSkillIndex, object to, ArrayList parm2)
	{
		bool flag = this.skillEffectDict.ContainsKey(iSkillIndex);
		if (flag)
		{
			SkillBaseIt skillBaseIt = (SkillBaseIt)this.skillEffectDict[iSkillIndex];
			skillBaseIt.OneSkillAction(to, parm2);
			bool flag2 = skillBaseIt.GetType() == typeof(SkillBaseIt);
			if (flag2)
			{
				skillBaseIt.OneSkillFinish();
			}
		}
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0007A7FA File Offset: 0x000789FA
	public override void HaveDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.HaveDamage(szDamage, szHp);
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0007A806 File Offset: 0x00078A06
	public override void XingTaiGroupUpdate(ArrayList data)
	{
		this.playerCommon.m_nXingTai = (int)data[0];
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0007A820 File Offset: 0x00078A20
	public override void SuperSkillUpdate(object parm)
	{
		bool flag = this.heroBase != null;
		if (flag)
		{
			this.heroBase.SuperSkillDo(parm);
		}
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0007A850 File Offset: 0x00078A50
	public override void OnRoundEndClear(ArrayList arrPam)
	{
		int nNetID = (int)arrPam[0];
		GamePlayer gamePlayer = (GamePlayer)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
		bool flag = gamePlayer != null && !gamePlayer.haveDestroy;
		if (flag)
		{
			gamePlayer.SkillBreakAll();
		}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0007A8A0 File Offset: 0x00078AA0
	public override void BlackCC(ArrayList szNetMsg)
	{
		base.BlackCC(szNetMsg);
		ArrayList arrayList = (ArrayList)szNetMsg[0];
		ArrayList szPos = (ArrayList)szNetMsg[1];
		int id = (int)szNetMsg[2];
		float lTime = (float)((int)szNetMsg[3]);
		foreach (object obj in arrayList)
		{
			int net_ID = (int)obj;
			NetLiving netLiving = Fun.Get_NetLiving(net_ID);
			bool flag = netLiving == null || netLiving.haveDestroy;
			if (flag)
			{
				break;
			}
			bool flag2 = netLiving.getNetid() == fGameManager.GetInstance().getPlayer().getNetid();
			if (flag2)
			{
				netLiving.heroBase.CloseCar(id, szPos, lTime);
			}
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0007A988 File Offset: 0x00078B88
	public override void boradCastShawangSuper(ArrayList szNetMsg)
	{
		ArrayList arrayList = (ArrayList)szNetMsg[2];
		foreach (object obj in arrayList)
		{
			int nNetID = (int)obj;
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
			if (netLiving != null)
			{
				netLiving.heroBase.HitByShawangSuper((string)szNetMsg[0], (int)szNetMsg[1], (ArrayList)szNetMsg[2]);
			}
		}
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0007AA34 File Offset: 0x00078C34
	public override void boradCastDaJiHunDun(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		NetLiving netLiving = (!this.isCopy) ? ((NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num)) : Fun.Get_NetLiving(num);
		bool flag = netLiving.getNetid() == fGameManager.GetInstance().getPlayer().getNetid() && !netLiving.isCopy;
		if (flag)
		{
			bool flag2 = netLiving.skillEffectDict.ContainsKey(1151);
			if (flag2)
			{
				((SkillDaJiHudunMe)netLiving.skillEffectDict[1151]).Update_Shield_Data(szNetMsg);
			}
		}
		else
		{
			bool flag3 = netLiving.skillEffectDict.ContainsKey(1151);
			if (flag3)
			{
				((SkillDaJiHudunIt)netLiving.skillEffectDict[1151]).Update_Shield_Data(szNetMsg);
			}
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0007AB08 File Offset: 0x00078D08
	public override void DAZHAOToClient(ArrayList arrPam)
	{
		int num = (int)arrPam[0];
		ArrayList arrayList = (ArrayList)arrPam[1];
		bool flag = !(this.playerDazhao == null);
		if (flag)
		{
			switch (num)
			{
			case 50004:
				this.playerDazhao.EnergyUp((int)arrayList[0], (int)arrayList[1], (int)arrayList[2]);
				break;
			case 50005:
			case 50008:
				this.playerDazhao.UpdateFlag(arrayList);
				break;
			}
		}
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0007ABB0 File Offset: 0x00078DB0
	private void OnInitAllUserDaZhao(ArrayList szUserData)
	{
		bool flag = szUserData.Count >= 1 && !this.haveDestroy && this.playerDazhao;
		if (flag)
		{
			this.playerDazhao.initDazhao(this, (int)szUserData[0], (ArrayList)szUserData[1]);
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0007AC08 File Offset: 0x00078E08
	public override void AddCharForce(Vector3 force)
	{
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0007AC0C File Offset: 0x00078E0C
	public override void OnPveUpDateDeadTime(int nTime)
	{
		this.playerCommon.m_dPlayerDeathDate = Fun.GetServerDate();
		this.playerCommon.m_dPlayerDeathChangeHeroDate = this.playerCommon.m_dPlayerDeathDate.AddSeconds(5.0);
		this.playerCommon.m_nDeathTimeLong = nTime;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0007AC5C File Offset: 0x00078E5C
	public void SetPlayerColliderEnable(bool bValue)
	{
		Collider[] componentsInChildren = this.UserControl.GetComponentsInChildren<Collider>();
		bool flag = componentsInChildren[0].enabled != bValue;
		if (flag)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = bValue;
			}
		}
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0007ACA8 File Offset: 0x00078EA8
	private void initHeroUi()
	{
		string url = string.Format("EffectRes/{0}.ef", "newplayerui");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), 0);
		}
		else
		{
			this.loadReayPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0007AD04 File Offset: 0x00078F04
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !this.haveDestroy;
		if (flag)
		{
			this.oPlayerUiperfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.oPlayerUiperfab);
			gameObject.transform.SetParent(this.UserHeadTarget.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
			this.newplayercon = gameObject.AddComponent<NewPlayerTopUICon>();
			this.newplayercon.setUI(this);
		}
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0007ADD4 File Offset: 0x00078FD4
	public void ShowHongwaiMat(bool state, Color col)
	{
		for (int i = 0; i < base.SzHongwaiMat.Count; i++)
		{
			base.SzHongwaiMat[i].enabled = state;
			Material[] materials = base.SzHongwaiMat[i].materials;
			for (int j = 0; j < materials.Length; j++)
			{
				bool flag = materials != null;
				if (flag)
				{
					materials[j].SetColor("_OutlineColor", col);
				}
			}
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0007AE58 File Offset: 0x00079058
	public void Npc_Not_Exists(int _Npc_ID)
	{
		bool flag = !this.isMainPlayer;
		if (!flag)
		{
			NetLiving netLiving = Fun.Get_NetLiving(_Npc_ID);
			bool flag2 = netLiving != null;
			if (flag2)
			{
				Npc_View npc_View = (Npc_View)((Npc_Entity)netLiving).View_Component;
				bool flag3 = !(npc_View == null);
				if (flag3)
				{
					npc_View.Destroy_Wait(1);
				}
			}
		}
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0007AEB8 File Offset: 0x000790B8
	public void CreateDeadHideOrShowTps(bool hide)
	{
		bool flag = this.UserControl.activeSelf != hide;
		if (flag)
		{
			this.UserControl.SetActive(hide);
		}
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0007AEEC File Offset: 0x000790EC
	public override void NVM(ArrayList _Parameters)
	{
		int nVisualModelIndex = Convert.ToInt32(_Parameters[0]);
		this.playerCommon.m_nVisualModelIndex = nVisualModelIndex;
		bool flag = this.userModelTps != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.userModelTps);
			this.userModelTps = null;
		}
		bool flag2 = this.userModelFps != null;
		if (flag2)
		{
			UnityEngine.Object.Destroy(this.userModelFps);
			this.userModelFps = null;
		}
		bool flag3 = this.fuhuoEffObj != null;
		if (flag3)
		{
			UnityEngine.Object.Destroy(this.fuhuoEffObj);
			this.fuhuoEffObj = null;
		}
		this.tCharAnimationTps = null;
		this.tpsRender = null;
		this.renderBianSe = null;
		this.aniEvent = null;
		this.aimSystem = null;
		this.tCharAnimationFps = null;
		this.fpsRender = null;
		this.aniEventFps = null;
		this.FPSrenderBianSe = null;
		this.LoadTpsAnimator();
	}

	// Token: 0x04000A7E RID: 2686
	public GameObject playerZhixin;

	// Token: 0x04000A7F RID: 2687
	public int iModelType = 3;

	// Token: 0x04000A80 RID: 2688
	public AniEvent aniEvent;

	// Token: 0x04000A81 RID: 2689
	public AniEvent aniEventFps;

	// Token: 0x04000A82 RID: 2690
	private GameObject fuhuoEffObj;

	// Token: 0x04000A83 RID: 2691
	private bool _Hero_Suicide_State;

	// Token: 0x04000A84 RID: 2692
	public float _Create_Time;

	// Token: 0x04000A85 RID: 2693
	private int nHeroWeaponCount;

	// Token: 0x04000A86 RID: 2694
	private int nLoadWeaponType;

	// Token: 0x04000A87 RID: 2695
	private List<string> szHeroWeaponMat;

	// Token: 0x04000A88 RID: 2696
	private UnityEngine.Object oPlayerUiperfab;
}

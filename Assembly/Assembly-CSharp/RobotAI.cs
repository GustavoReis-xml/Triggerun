using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class RobotAI : NetLiving
{
	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x0600211F RID: 8479 RVA: 0x001318E8 File Offset: 0x0012FAE8
	// (set) Token: 0x06002120 RID: 8480 RVA: 0x00131900 File Offset: 0x0012FB00
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

	// Token: 0x06002121 RID: 8481 RVA: 0x0013190C File Offset: 0x0012FB0C
	private HeroPlayer GetLocalPlayer()
	{
		return fGameManager.GetInstance().getPlayer() ?? fGameManager.Heroplayer;
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x00131931 File Offset: 0x0012FB31
	private void Start()
	{
		this.AIFollowPaths = new List<AIMovePos>();
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x0013193F File Offset: 0x0012FB3F
	private void Update()
	{
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x00131944 File Offset: 0x0012FB44
	private void InitRobotAICommonInfo(ArrayList msg, ArrayList msg2)
	{
		bool flag = this.playerCommon == null;
		if (flag)
		{
			this.playerCommon = new PlayerCommon();
		}
		bool flag2 = msg == null || msg.Count == 0;
		if (flag2)
		{
			Debug.LogError("msg is null or empty!");
		}
		else
		{
			bool flag3 = msg2 == null || msg2.Count == 0;
			if (flag3)
			{
				Debug.LogError("msg2 is null or empty!");
			}
			else
			{
				bool flag4 = msg.Count > 0 && msg[0] is ArrayList;
				if (flag4)
				{
					this.playerCommon.InitBase((ArrayList)msg[0]);
				}
				bool flag5 = msg.Count > 1 && msg[1] is ArrayList;
				if (flag5)
				{
					this.playerCommon.InitBattleAttr((ArrayList)msg[1]);
				}
				bool flag6 = msg.Count > 2 && msg[2] is ArrayList;
				if (flag6)
				{
					this.playerCommon.InitScn((ArrayList)msg[2]);
				}
				bool flag7 = msg.Count > 3 && msg[3] is ArrayList;
				if (flag7)
				{
					this.playerCommon.InitBuffInfo((ArrayList)msg[3]);
				}
				this.InJiDi = 0;
				this.playerCommon.m_nShili = 2;
				this.nForce = this.playerCommon.m_nShili;
				bool flag8 = msg2.Count > 0 && msg2[0] != null;
				if (flag8)
				{
					this.playerCommon.m_nPlayerState = Convert.ToInt32(msg2[0]);
					this.isDead = (this.playerCommon.m_nPlayerState == 0);
				}
				bool flag9 = msg2.Count > 1 && msg2[1] != null && Convert.ToInt32(msg2[1]) == 0;
				if (flag9)
				{
					this.playerCommon.posCreate = this.playerCommon.pos;
					this.RobotCreatePos = this.playerCommon.pos;
				}
				bool flag10 = msg2.Count > 2 && msg2[2] is ArrayList;
				if (flag10)
				{
					ArrayList arrayList = (ArrayList)msg2[2];
					bool flag11 = arrayList.Count > 0 && arrayList[0] is ArrayList;
					if (flag11)
					{
						this.playerCommon.aryBulletCartridgeID = (ArrayList)arrayList[0];
					}
					bool flag12 = arrayList.Count > 1 && arrayList[1] is ArrayList;
					if (flag12)
					{
						this.playerCommon.aryBulletGemData = (ArrayList)arrayList[1];
					}
				}
				bool flag13 = msg2.Count > 3 && msg2[3] != null;
				if (flag13)
				{
					this.playerCommon.m_nHeroNo = Convert.ToInt32(msg2[3]);
				}
				bool flag14 = msg2.Count > 4 && msg2[4] is ArrayList;
				if (flag14)
				{
					this.playerCommon.aryheroDazhao = (ArrayList)msg2[4];
				}
				bool flag15 = msg2.Count > 5 && msg2[5] is ArrayList;
				if (flag15)
				{
					this.playerCommon.arySkillServerData = (ArrayList)msg2[5];
				}
				bool flag16 = fGameManager.mloadConfig != null && fGameManager.mloadConfig.AllHeroList != null && fGameManager.mloadConfig.AllHeroList.ContainsKey(this.playerCommon.m_nModelIndex);
				if (flag16)
				{
					this.heroData = fGameManager.mloadConfig.AllHeroList[this.playerCommon.m_nModelIndex];
				}
				else
				{
					int key = 1015;
					bool flag17 = fGameManager.mloadConfig != null && fGameManager.mloadConfig.AllHeroList != null && fGameManager.mloadConfig.AllHeroList.ContainsKey(key);
					if (flag17)
					{
						this.heroData = fGameManager.mloadConfig.AllHeroList[key];
					}
					else
					{
						Debug.LogError("AllHeroList doesn't contain Gaya (1015) or config is null!");
					}
				}
				this.RobotAIInit();
			}
		}
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x00131D80 File Offset: 0x0012FF80
	private void RobotAIInit()
	{
		bool flag = fGameManager.mloadConfig == null || fGameManager.mloadConfig.AllRobotCommonData == null || fGameManager.mloadConfig.AllRobotAIData == null;
		if (flag)
		{
			Debug.LogError("fGameManager.mloadConfig or its lists are null!");
		}
		else
		{
			bool flag2 = !fGameManager.mloadConfig.AllRobotCommonData.ContainsKey(this.m_nCreateIndex);
			if (flag2)
			{
				Debug.LogWarning(string.Format("AllRobotCommonData doesn't contain createIndex: {0}. Usando fallback!", this.m_nCreateIndex));
				this.AllAI_Index = new List<int>
				{
					1,
					2,
					3,
					4,
					5
				};
				this.beMonster = 0;
			}
			else
			{
				this.AllAI_Index = fGameManager.mloadConfig.AllRobotCommonData[this.m_nCreateIndex].AI_Index;
				this.beMonster = fGameManager.mloadConfig.AllRobotCommonData[this.m_nCreateIndex].beMonster;
			}
			bool flag3 = this.AllAI_Index == null || this.AllAI_Index.Count == 0;
			if (flag3)
			{
				Debug.LogWarning("AllAI_Index is null or empty! Fallback!");
				this.AllAI_Index = new List<int>
				{
					1,
					2,
					3,
					4,
					5
				};
			}
			this.AI_Index = this.AllAI_Index[UnityEngine.Random.Range(0, this.AllAI_Index.Count)];
			bool flag4 = !fGameManager.mloadConfig.AllRobotAIData.ContainsKey(this.AI_Index);
			if (flag4)
			{
				Debug.LogWarning(string.Format("AllRobotAIData doesn't contain AI_Index: {0}. Usando Fallback Interno!", this.AI_Index));
				this.FollowRange = 40f;
				this.EyeAngle = 180f;
				this.EarRange = 30f;
				this.EyeRange = 50f;
				this.ReactionTime = 0.2f;
				this.turnRotationSpeed = 8f;
				this.DodgeRate = 0.2f;
				this.robotHitRate = 100f;
				this.shootRange = 15f;
				this.safeRange = 5f;
				this.MoveAttack = true;
				this.MoveType = 1;
			}
			else
			{
				this.AIFollowPaths = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].movePos;
				bool flag5 = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].moveAttack == 1;
				if (flag5)
				{
					this.MoveAttack = true;
				}
				else
				{
					this.MoveAttack = false;
				}
				this.MoveType = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].moveType;
				this.FollowRange = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].ChaseEnemyRange;
				this.EyeAngle = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].EyeAngle;
				this.EarRange = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].EarRange;
				this.EyeRange = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].SightRange;
				this.ReactionTime = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].ReactionTime;
				this.turnRotationSpeed = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].turnRotationSpeed;
				this.DodgeRate = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].DodgeRate;
				bool flag6 = fGameManager.mloadConfig.AllRobotCommonData != null && fGameManager.mloadConfig.AllRobotCommonData.ContainsKey(this.m_nCreateIndex);
				if (flag6)
				{
					this.robotHitRate = fGameManager.mloadConfig.AllRobotCommonData[this.m_nCreateIndex].hitRate;
				}
				this.shootRange = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].ShootRange;
				this.safeRange = fGameManager.mloadConfig.AllRobotAIData[this.AI_Index].safeRange;
			}
		}
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x001321A4 File Offset: 0x001303A4
	public void RobotCreate(ArrayList pos, ArrayList rot, int _heroIndex, ArrayList msg, ArrayList msg2, int m_nHavePlayer, int CreateIndex)
	{
		this.m_nCreateIndex = CreateIndex;
		this.InitRobotAICommonInfo(msg, msg2);
		bool flag = pos != null && pos.Count > 2;
		if (flag)
		{
			bool flag2 = this.playerCommon == null;
			if (flag2)
			{
				this.playerCommon = new PlayerCommon();
			}
			this.playerCommon.pos.x = (float)Convert.ToInt32(pos[0]) / 100f;
			this.playerCommon.pos.y = (float)Convert.ToInt32(pos[2]) / 100f;
			this.playerCommon.pos.z = (float)Convert.ToInt32(pos[1]) / 100f;
			this.playerCommon.posCreate = this.playerCommon.pos;
			bool flag3 = rot != null && rot.Count > 1;
			if (flag3)
			{
				this.playerCommon.m_nForward = (float)Convert.ToInt32(rot[1]);
			}
			this.netobject_type = 6;
			this.bePlayer = true;
			HeroPlayer localPlayer = this.GetLocalPlayer();
			bool flag4 = localPlayer != null;
			if (flag4)
			{
				bool flag5 = localPlayer.robotList == null;
				if (flag5)
				{
					localPlayer.robotList = new List<int>();
				}
				localPlayer.robotList.Add(base.getNetid());
			}
			this.addPlayer();
		}
		else
		{
			Debug.LogError("创建机器人有错: " + ((pos != null) ? pos.Count.ToString() : "null") + " create Index " + CreateIndex.ToString());
		}
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x00132332 File Offset: 0x00130532
	public void addPlayer()
	{
		this.setRobotHeroForce();
		this.CreatePlayer();
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x00132343 File Offset: 0x00130543
	private void setRobotHeroForce()
	{
		this.playerCommon.m_nShili = 2;
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x00132354 File Offset: 0x00130554
	private void CreatePlayer()
	{
		this.userRoot = new GameObject(string.Format("User_{0}", base.getNetid()));
		this.userRoot.transform.position = Vector3.zero;
		this.userRoot.transform.eulerAngles = Vector3.zero;
		this.publicInfo = this.userRoot.AddComponent<PlayerInfo>();
		this.publicInfo.InitInfo(this);
		this.UserControl = new GameObject("control");
		this.UserControl.layer = LayerMask.NameToLayer("Ignore Raycast");
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
		rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		CapsuleCollider capsuleCollider = this.UserControl.AddComponent<CapsuleCollider>();
		capsuleCollider.radius = 0.4f;
		capsuleCollider.height = 1.8f;
		Vector3 center = capsuleCollider.center;
		center.y = 0.9f;
		capsuleCollider.center = center;
		this.defaultCollider = capsuleCollider;
		this.defaultRigidbody = rigidbody;
		this.defaultMesh = new GameObject("defaultMesh");
		this.defaultMesh.transform.parent = this.UserControl.transform;
		this.defaultMesh.transform.localPosition = center + new Vector3(0f, 0.3f, 0f);
		this.defaultMesh.transform.localScale = Vector3.one * 0.3f;
		this.bodyCollider = this.defaultMesh.AddComponent<BoxCollider>();
		this.bodyRigidbody = this.defaultMesh.AddComponent<Rigidbody>();
		this.bodyRigidbody.isKinematic = true;
		this.defaultMesh.layer = LayerMask.NameToLayer("Ignore Raycast");
		GameObject gameObject = new GameObject("UserHeadTarget");
		this.UserHeadTarget = gameObject.transform;
		this.UserHeadTarget.parent = this.UserControl.transform;
		this.UserHeadTarget.localPosition = Vector3.zero;
		this.UserHeadTarget.localEulerAngles = Vector3.zero;
		this.UserHeadTarget.localScale = Vector3.one;
		GameObject gameObject2 = new GameObject("cameraTarget");
		this.camTarget = gameObject2.transform;
		this.camTarget.parent = this.UserControl.transform;
		this.camTarget.localEulerAngles = Vector3.zero;
		this.FindMubiaoTarget = new GameObject("FindMubiaoTarget");
		this.FindMubiaoTarget.transform.parent = this.camTarget;
		this.FindMubiaoTarget.transform.localPosition = Vector3.zero;
		this.FindMubiaoTarget.transform.localEulerAngles = Vector3.zero;
		this.playerTarget = new GameObject("PlayerTarget");
		this.playerTarget.transform.parent = this.userRoot.transform;
		this.camTarget.forward = this.UserControl.transform.forward;
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
		HeroPlayer localPlayer = this.GetLocalPlayer();
		bool flag2 = localPlayer != null && this.nForce == localPlayer.playerCommon.m_nShili;
		if (flag2)
		{
			this.playerSound = this._playerSoundContainer.AddComponent<CPlayerSoundItFriend>();
		}
		else
		{
			this.playerSound = this._playerSoundContainer.AddComponent<CPlayerSoundItEnemy>();
		}
		this.playerSound.initPlayerSound(this);
		this.skillInfo = this.UserControl.AddComponent<SkillInfo>();
		this.skillInfo.InitSkills(this, this.heroData.skillshow.heroAllSkills);
		bool flag3 = this.beMonster == 0;
		if (flag3)
		{
			this.playerDazhao = this.UserControl.AddComponent<CPlayerDazhao>();
			this.OnInitAllUserDaZhao(this.playerCommon.aryheroDazhao);
		}
		this.InitUserControlScript();
		this.addMySelfCompent();
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x00132910 File Offset: 0x00130B10
	private void addMySelfCompent()
	{
		this.publicInfo.type = "GamePlayer";
		this.tChar = this.UserControl.AddComponent<TBCharacterThirdIt>();
		this.addSkillCompent();
		Type type = Type.GetType("Hero" + this.heroData.heroclass + "AIController");
		bool flag = type != null;
		if (flag)
		{
			this.robotController = (RobotAIController)this.UserControl.AddComponent(type);
		}
		else
		{
			this.robotController = this.UserControl.AddComponent<RobotAIController>();
		}
		this.robotController.robot = this;
		this.robotController.RobotDeath();
		this.charMove = this.UserControl.AddComponent<TBUserControlIt>();
		this.charMove.InitControl(this);
		this.playerBuffIt = this.UserControl.AddComponent<buffIt>();
		this.playerBuffIt.OneBuffInit(this);
		this.playerEffectBase = this.UserControl.AddComponent<effectMe>();
		this.playerEffectBase.gameplayer = this;
		this.LoadTpsAnimator();
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x00132A18 File Offset: 0x00130C18
	private void addSkillCompent()
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

	// Token: 0x0600212C RID: 8492 RVA: 0x00132AD4 File Offset: 0x00130CD4
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

	// Token: 0x0600212D RID: 8493 RVA: 0x00132B44 File Offset: 0x00130D44
	public virtual void addOneSkillCompent(int iSkillIndex, string className)
	{
		bool flag = className != "0";
		Type type;
		if (flag)
		{
			string typeName = className + "Robot";
			type = Type.GetType(typeName);
		}
		else
		{
			string typeName2 = string.Format("{0}Robot", "SkillBase");
			type = Type.GetType(typeName2);
		}
		bool flag2 = type == null;
		if (flag2)
		{
		}
		bool flag3 = type != null;
		if (flag3)
		{
			SkillBaseRobot skillBaseRobot = (SkillBaseRobot)this.UserControl.AddComponent(type);
			skillBaseRobot.SkillInit(this, iSkillIndex);
			this.skillEffectDict.Add(iSkillIndex, skillBaseRobot);
		}
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x00132BD8 File Offset: 0x00130DD8
	protected void LoadTpsAnimator()
	{
		string[] npcModelData = this.playerCommon.GetNpcModelData(this.m_nCreateIndex);
		string url = "HeroAnimator/" + npcModelData[1] + ".mo";
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

	// Token: 0x0600212F RID: 8495 RVA: 0x00132C54 File Offset: 0x00130E54
	public override void AddBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffIt == null);
		if (flag)
		{
			this.playerBuffIt.AddOneBuff((string)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x00132CA8 File Offset: 0x00130EA8
	public override void DeleteBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffIt == null);
		if (flag)
		{
			this.playerBuffIt.DeleteOneBuff((int)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x00132CFC File Offset: 0x00130EFC
	private void LoadTpsAnimatorFinish(WWW www, object obj)
	{
		bool flag = www != null && www.error == null && (int)obj == this.loadID;
		if (flag)
		{
			this.userAnimatorTps = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.LoadModelTps();
		}
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x00132D58 File Offset: 0x00130F58
	public void LoadModelTps()
	{
		string url = "HeroAnimator/" + this.playerCommon.GetNpcModelData(this.m_nCreateIndex)[0] + ".mo";
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

	// Token: 0x06002133 RID: 8499 RVA: 0x00132DD0 File Offset: 0x00130FD0
	private void LoadModelTpsFinish(WWW www, object obj)
	{
		bool flag = (int)obj == this.loadID && www != null && www.error == null && !this.haveDestroy;
		if (flag)
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
			this.hitReaction = this.userModelTps.AddComponent<CHitReaction>();
			bool flag3 = this.hitReaction == null;
			if (flag3)
			{
				Debug.LogError("没加受伤动作!");
			}
			else
			{
				this.hitReaction.HitReactionInit(this);
			}
			CHeroWeaponDrop component2 = this.userModelTps.GetComponent<CHeroWeaponDrop>();
			bool flag4 = component2 != null;
			if (flag4)
			{
				component2.InitDrop(this);
			}
			Type type = Type.GetType("TBCharAnimation" + this.heroData.heroclass + "Tps");
			this.tCharAnimationTps = (TBCharAnimationThirdIK)this.userModelTps.AddComponent(type);
			base.Get_Stroke_Material_Array();
			this.tpsRender = this.userModelTps.GetComponent<CHeroRender>();
			bool flag5 = this.tpsRender != null;
			if (flag5)
			{
				this.tpsRender.GetRenders();
			}
			else
			{
				Debug.LogError("这个角色" + this.userRoot.name + "动画没找到CHeroRender脚本控制.");
			}
			this.AddHeadLook();
			this.setTpsLayer();
			this.aniEvent = this.userModelTps.AddComponent<AniEvent>();
			this.aniEvent.InitEvent(this);
			CHeroWeapon component3 = this.userModelTps.GetComponent<CHeroWeapon>();
			bool flag6 = component3 != null;
			if (flag6)
			{
				component3.SetHeroData(this.heroData.id, this.playerCommon.m_nCid);
				component3.SetHeroWeapon(3, -1, 1);
			}
			this.ModeLoadOK();
		}
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x001330CC File Offset: 0x001312CC
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

	// Token: 0x06002135 RID: 8501 RVA: 0x00133124 File Offset: 0x00131324
	public void setTpsLayer()
	{
		int layer = LayerMask.NameToLayer("force2_player");
		this.UserControl.tag = "can_damage_human";
		this.defaultMesh.tag = "can_damage_human";
		bool flag = this.userModelTps == null;
		if (!flag)
		{
			Collider[] componentsInChildren = this.userModelTps.GetComponentsInChildren<Collider>();
			bool flag2 = false;
			this.colliderCankill = new Dictionary<string, GameObject>();
			bool flag3 = this.rigidbodyAll.Count == 0;
			if (flag3)
			{
				this.rigidbodyAll = new ArrayList();
				flag2 = true;
			}
			this.bodyCollider.gameObject.tag = "can_damage_human";
			this.bodyCollider.gameObject.layer = layer;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
				bool flag4 = !componentsInChildren[i].gameObject.name.Contains("huzhao");
				if (flag4)
				{
					componentsInChildren[i].gameObject.tag = "can_damage_human";
				}
				bool flag5 = componentsInChildren[i].gameObject.name.Contains("body2");
				if (flag5)
				{
					componentsInChildren[i].gameObject.tag = "can_damage_dun";
					componentsInChildren[i].gameObject.layer = layer;
					bool flag6 = componentsInChildren[i].GetComponent<Rigidbody>() != null;
					if (flag6)
					{
						componentsInChildren[i].GetComponent<Rigidbody>().isKinematic = true;
					}
					componentsInChildren[i].isTrigger = false;
				}
				bool flag7 = componentsInChildren[i].name.Contains("Head");
				if (flag7)
				{
					base.userHeadTransform = componentsInChildren[i].transform;
				}
				Rigidbody rigidbody = componentsInChildren[i].gameObject.GetComponent<Rigidbody>();
				bool flag8 = rigidbody == null;
				if (flag8)
				{
					rigidbody = componentsInChildren[i].gameObject.AddComponent<Rigidbody>();
					bool flag9 = componentsInChildren[i].gameObject.name.Contains("body");
					if (flag9)
					{
						rigidbody.isKinematic = true;
					}
				}
				rigidbody.isKinematic = true;
				CharacterJoint component = componentsInChildren[i].gameObject.GetComponent<CharacterJoint>();
				bool flag10 = component != null && flag2;
				if (flag10)
				{
					this.rigidbodyAll.Add(rigidbody);
				}
				bool flag11 = !this.colliderCankill.ContainsKey(componentsInChildren[i].name);
				if (flag11)
				{
					this.colliderCankill.Add(componentsInChildren[i].name, componentsInChildren[i].gameObject);
				}
			}
			Renderer[] componentsInChildren2 = this.userModelTps.GetComponentsInChildren<Renderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				bool flag12 = componentsInChildren2[j].name.Contains("_toushi");
				if (flag12)
				{
					componentsInChildren2[j].gameObject.layer = layer;
				}
			}
		}
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x00133404 File Offset: 0x00131604
	protected override void ModeLoadOK()
	{
		this.playerHaveCreate = true;
		bool flag = !this.isMainPlayer;
		if (flag)
		{
			this.initHeroUi();
		}
		this.initActiveSkill();
		this.heroBase.InitForModelLoad();
		this.tChar.InitCharThird(this, this.tCharAnimationTps, this.charMove, this.tCharAnimationFps);
		this.LoadAnimatorParm();
		bool isDead = this.isDead;
		if (isDead)
		{
			this.tCharAnimationTps.animator.SetInteger("Dead", 1);
		}
		bool flag2 = this.playerBuffIt != null;
		if (flag2)
		{
			this.playerBuffIt.AddBuffList(this.playerCommon.m_szBuffEffectData);
		}
		base.InitHeroWeapon(this.playerCommon.m_nModelIndex);
		base.ModeLoadOK();
		this.robotController.RobotHaveCreate();
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x001334DC File Offset: 0x001316DC
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

	// Token: 0x06002138 RID: 8504 RVA: 0x00133538 File Offset: 0x00131738
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

	// Token: 0x06002139 RID: 8505 RVA: 0x00133608 File Offset: 0x00131808
	public virtual void initActiveSkill()
	{
		foreach (int key in this.skillEffectDict.Keys)
		{
			SkillBase skillBase = this.skillEffectDict[key];
			skillBase.ModelLoaded();
		}
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x00133674 File Offset: 0x00131874
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
		}
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x00133734 File Offset: 0x00131934
	public override void OnRevive(ArrayList szNetMsg)
	{
		base.OnRevive(szNetMsg);
		this.OnReviveTime = Fun.GetServerDate();
		base.GameAttackPlayer = null;
		this.isInitDead = false;
		this.setReviveData(szNetMsg);
		this.RobotAIInit();
		this.robotController.InitRobotControllerData();
		this.isDead = false;
		bool flag = this.tCharAnimationTps == null || this.userModelTps == null;
		if (!flag)
		{
			this.bodyCollider.enabled = true;
			this.setTpsLayer();
			this.tCharAnimationTps.animator.SetInteger("Dead", 0);
			this.tCharAnimationTps.animator.enabled = true;
			Renderer[] componentsInChildren = this.userModelTps.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				bool flag2 = componentsInChildren[i].name.Contains("_toushi");
				if (flag2)
				{
					componentsInChildren[i].gameObject.layer = 0;
				}
			}
			base.Open_Or_Close_Stroke_Material(true);
			bool flag3 = this.aimSystem != null;
			if (flag3)
			{
				this.aimSystem.SetEnable(true);
				this.aimSystem.AimEnable(true);
			}
			base.userBip.transform.localPosition = Vector3.zero;
			bool flag4 = this.userModelTps != null;
			if (flag4)
			{
				this.userModelTps.transform.localPosition = Vector3.zero;
			}
			this.tCharAnimationTps.SetAnimParm();
		}
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x001338B8 File Offset: 0x00131AB8
	public void setReviveData(ArrayList szNetMsg)
	{
		this.playerCommon.pos = new Vector3((float)((int)szNetMsg[0]) * 0.01f, (float)((int)szNetMsg[2]) * 0.01f, (float)((int)szNetMsg[1]) * 0.01f);
		this.playerCommon.posCreate = this.playerCommon.pos;
		this.playerCommon.m_nForward = (float)((int)szNetMsg[3]) * 1f;
		this.playerCommon.m_nModelIndex = (int)szNetMsg[4];
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

	// Token: 0x0600213D RID: 8509 RVA: 0x00133A4C File Offset: 0x00131C4C
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		bool flag = this.beMonster == 0;
		if (flag)
		{
			base.OnDeath(_attackId, o, forcev3, forcemax, weaponName, launcherName, m_nIsSupperKill, m_szCanYuXiaoMie, nDeadType, _warData);
		}
		this.robotController.RobotDeath();
		this.isDead = true;
		base.Open_Or_Close_Stroke_Material(false);
		this.playerCommon.m_nPlayerState = 0;
		this.playerCommon.m_dPlayerDeathDate = Fun.GetServerDate();
		this.playerCommon.m_dPlayerDeathChangeHeroDate = this.playerCommon.m_dPlayerDeathDate.AddSeconds(5.0);
		this.playerCommon.m_nDeathTimeLong = o;
		this.playerCommon.m_nDeathAttackId = _attackId;
		this.bodyCollider.enabled = false;
		bool flag2 = this.playerEffectBase != null;
		if (flag2)
		{
			this.playerEffectBase.OnDeadClearEffect();
		}
		this.heroBase.OnDeath();
		Vector3 fV = new Vector3((float)((int)forcev3[0]) * 0.01f, (float)((int)forcev3[1]) * 0.01f, (float)((int)forcev3[2]) * 0.01f);
		float fMax = (float)forcemax * 0.01f;
		this.publicInfo.playerDeadRagdoll(fV, fMax);
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x00133B84 File Offset: 0x00131D84
	public void playerDeadAnim()
	{
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
			this.tChar.animSpeedMove = 1f;
			this.tCharAnimationTps.animator.speed = 1f;
			this.tCharAnimationTps.animator.SetInteger("Dead", 1);
			bool flag3 = this.tpsRender != null;
			if (flag3)
			{
				this.tpsRender.SetParticleSystem(false);
			}
			bool flag4 = this.fpsRender != null;
			if (flag4)
			{
				this.fpsRender.SetParticleSystem(false);
			}
			this.tChar.DeadInit();
		}
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x00133C70 File Offset: 0x00131E70
	public override void DamageMsgToServer(NetLiving _HeroPlayer, ArrayList _Parameters)
	{
		bool flag = this.SyncNetObj != null;
		if (flag)
		{
			this.SyncNetObj.DamageMsgToServer(this.SyncNetObj, _Parameters);
		}
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x00133CA0 File Offset: 0x00131EA0
	public void CheckPlayer()
	{
		bool flag = !(this.thisModel == null) && !this.isDead;
		if (flag)
		{
			HeroPlayer localPlayer = this.GetLocalPlayer();
			bool flag2 = localPlayer != null && !(localPlayer.thisModel == null);
			if (flag2)
			{
				this.SyncNetObj = null;
				float num = Vector3.Distance(localPlayer.thisModel.transform.position, this.thisModel.transform.position);
				localPlayer.SendMessageToServer(4010002, new ArrayList
				{
					base.getNetid(),
					(int)num
				});
			}
		}
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x00133D58 File Offset: 0x00131F58
	public void SyncObjSuccess(bool success)
	{
		this.needSendToServer = success;
		this.robotController.InitRobotControllerData();
		bool playerHaveCreate = this.playerHaveCreate;
		if (playerHaveCreate)
		{
			base.InitHeroWeapon(this.playerCommon.m_nModelIndex);
			if (success)
			{
				this.robotController.StopChaseAndCalPath(true);
				this.robotController.ChangeWeaponGround();
				this.wc.InitBulletBeforeSubFor_1();
			}
		}
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x00133DC4 File Offset: 0x00131FC4
	public void SCP(ArrayList aryParam)
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

	// Token: 0x06002143 RID: 8515 RVA: 0x00133E98 File Offset: 0x00132098
	public void RCR(ArrayList aryParam)
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

	// Token: 0x06002144 RID: 8516 RVA: 0x00133F1C File Offset: 0x0013211C
	public void RCT(ArrayList aryParam)
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

	// Token: 0x06002145 RID: 8517 RVA: 0x00133FCC File Offset: 0x001321CC
	public override bool CheckActiveStat()
	{
		return false;
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x00133FE0 File Offset: 0x001321E0
	private void OnInitAllUserDaZhao(ArrayList szUserData)
	{
		bool flag = szUserData.Count >= 1 && !this.haveDestroy && this.playerDazhao;
		if (flag)
		{
			this.playerDazhao.initDazhao(this, (int)szUserData[0], (ArrayList)szUserData[1]);
		}
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x00134038 File Offset: 0x00132238
	public override void SetAnimator(string ani, int type, object parm, int sendtype = 1)
	{
		bool flag = ani == "0" || !this.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = this.tCharAnimationTps != null;
			if (flag2)
			{
				this.tCharAnimationTps.SetAnimator(ani, type, parm);
			}
			bool needSendToServer = this.needSendToServer;
			if (needSendToServer)
			{
				switch (type)
				{
				case 0:
					parm = (((bool)parm) ? 1 : 0);
					break;
				case 1:
					parm = (int)Math.Ceiling((double)((float)parm * 100f));
					break;
				case 2:
					break;
				default:
					return;
				}
				ArrayList arrayList = new ArrayList
				{
					new ArrayList
					{
						ani,
						type,
						parm
					},
					sendtype,
					base.getNetid()
				};
				this.SyncNetObj.TosFun("SA_Robot", new object[]
				{
					arrayList
				});
			}
		}
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x0013415C File Offset: 0x0013235C
	public void CA(ArrayList aryParam)
	{
		bool flag = !this.needSendToServer && !(this.tCharAnimationTps == null) && this.tCharAnimationTps.animator && this.tCharAnimationTps.animator.parameterCount >= 1;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)aryParam[0];
			this.AnimatorUpdate((string)arrayList[0], (int)arrayList[1], arrayList[2]);
		}
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x001341E4 File Offset: 0x001323E4
	private void AnimatorUpdate(string ani, int type, object parm)
	{
		bool flag = !this.needSendToServer && this.tCharAnimationTps.animator && this.tCharAnimationTps.animator.parameterCount >= 1;
		if (flag)
		{
			switch (type)
			{
			case 0:
				parm = ((int)parm == 1);
				this.tCharAnimationTps.animator.SetBool(ani, (bool)parm);
				break;
			case 1:
				parm = (float)((int)parm) * 0.01f;
				this.tCharAnimationTps.animator.SetFloat(ani, (float)parm);
				break;
			case 2:
				this.tCharAnimationTps.animator.SetInteger(ani, (int)parm);
				break;
			}
		}
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x001342B8 File Offset: 0x001324B8
	public override void CB(int launcherName, int launcherNo, string muzzleName, int bullId, int bullettype, ArrayList born, ArrayList target, ArrayList parm, int numberBullet, int _cartridge, int _loadid)
	{
		HeroPlayer localPlayer = this.GetLocalPlayer();
		bool flag = _loadid == this.loadID && !(this.wc == null) && !this.haveDestroy && this.playerHaveCreate && localPlayer != null && (this.SyncNetObj == null || this.SyncNetObj.getNetid() != localPlayer.getNetid()) && !this.needSendToServer && this.wc.GetType() != typeof(CWeaponControlMe);
		if (flag)
		{
			base.CB(launcherName, launcherNo, muzzleName, bullId, bullettype, born, target, parm, numberBullet, _cartridge, _loadid);
		}
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x00134358 File Offset: 0x00132558
	public override void SendMsgToServer(int nNetID, ArrayList szNetMsg)
	{
		bool flag = this.SyncNetObj != null;
		if (flag)
		{
			this.SyncNetObj.SendMessageToServer(nNetID, szNetMsg);
		}
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x00134384 File Offset: 0x00132584
	public override Vector3 getMubiao(int layerType)
	{
		Vector3 result = Vector3.zero;
		base.getMubiao(layerType);
		bool flag = (float)UnityEngine.Random.Range(0, 100) < this.robotHitRate;
		if (flag)
		{
			bool flag2 = this.robotController.chaseTarget != null && this.robotController.chaseTarget.UserControl != null;
			if (flag2)
			{
				result = ((this.robotController.chaseTarget.netobject_type != 2) ? (this.robotController.chaseTarget.UserControl.transform.position + Vector3.up * 1f) : (this.robotController.chaseTarget.UserControl.transform.position + Vector3.up * 0.2f));
			}
		}
		else
		{
			bool flag3 = this.robotController.chaseTarget != null && this.robotController.chaseTarget.UserControl != null;
			if (flag3)
			{
				result = this.robotController.chaseTarget.UserControl.transform.position + new Vector3((float)UnityEngine.Random.Range(1, 3), (float)UnityEngine.Random.Range(1, 3), (float)UnityEngine.Random.Range(1, 3));
			}
		}
		return result;
	}

	// Token: 0x0600214D RID: 8525 RVA: 0x001344CC File Offset: 0x001326CC
	public override void ChangeWeaponGroup(int xingtai)
	{
		bool needSendToServer = this.needSendToServer;
		if (needSendToServer)
		{
			this.SyncNetObj.SendMessageToServer(31207, new ArrayList
			{
				xingtai,
				base.getNetid()
			});
		}
	}

	// Token: 0x0600214E RID: 8526 RVA: 0x0013451C File Offset: 0x0013271C
	public override void ChangeWeaponGroupUpdate(ArrayList data)
	{
		bool needSendToServer = this.needSendToServer;
		if (needSendToServer)
		{
			base.ChangeWeaponGroupUpdate(data);
			this.playerCommon.m_nWeaponGroup = (int)data[0];
			bool flag = this.heroBase != null;
			if (flag)
			{
				this.heroBase.ChangeWeaponGroup(this.playerCommon.m_nWeaponGroup);
			}
			bool flag2 = !(this.wc == null);
			if (flag2)
			{
				((CWeaponControlMe)this.wc).SwitchWeaponGroup(this.playerCommon.m_nWeaponGroup);
			}
		}
	}

	// Token: 0x0600214F RID: 8527 RVA: 0x001345B0 File Offset: 0x001327B0
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

	// Token: 0x06002150 RID: 8528 RVA: 0x00134638 File Offset: 0x00132838
	public override void SkillDo(ArrayList parm, ArrayList parm2)
	{
		int num = (int)parm[0];
		this.skillInfo.SkillTalk(num, true);
		this.skillInfo.SkillAnimation(num);
		bool flag = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(num);
		if (flag)
		{
			ArrayList arrayList = (ArrayList)parm[1];
			Vector3 vector = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			this.SkillAction(num, vector, parm2);
		}
	}

	// Token: 0x06002151 RID: 8529 RVA: 0x001346E0 File Offset: 0x001328E0
	public void SkillAction(int iSkillIndex, object to, ArrayList parm2)
	{
		bool flag = !this.skillEffectDict.ContainsKey(iSkillIndex);
		if (flag)
		{
			Debug.LogError("找不到这个技能的脚本，无法正常释放。:" + iSkillIndex.ToString());
		}
		else
		{
			SkillBaseRobot skillBaseRobot = (SkillBaseRobot)this.skillEffectDict[iSkillIndex];
			skillBaseRobot.OneSkillAction(to, parm2);
			bool flag2 = skillBaseRobot.GetType() == typeof(SkillBaseRobot);
			if (flag2)
			{
				skillBaseRobot.OneSkillFinish();
			}
		}
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x00134758 File Offset: 0x00132958
	public override bool superSkill(ArrayList arr = null)
	{
		bool flag = !this.playerDazhao.canDo;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool isDead = this.isDead;
			if (isDead)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.playerDazhao.weaponReaded(this);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = arr == null;
					if (flag3)
					{
						arr = new ArrayList();
					}
					arr.Add(base.getNetid());
					this.SyncNetObj.SendMessageToServer(31216, arr);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x001347E0 File Offset: 0x001329E0
	public override void DAZHAOToClient(ArrayList arrPam)
	{
		int num = (int)arrPam[0];
		ArrayList arrayList = (ArrayList)arrPam[1];
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

	// Token: 0x06002154 RID: 8532 RVA: 0x0013486F File Offset: 0x00132A6F
	public override void OnRoundEndClear(ArrayList arrPam)
	{
		this.robotController.RobotDeath();
	}

	// Token: 0x06002155 RID: 8533 RVA: 0x00134880 File Offset: 0x00132A80
	public override void SuperSkillUpdate(object parm)
	{
		bool flag = this.heroBase != null;
		if (flag)
		{
			this.heroBase.SuperSkillDo(parm);
		}
		this.playerDazhao.SuperSkillDo(this);
	}

	// Token: 0x06002156 RID: 8534 RVA: 0x001348BC File Offset: 0x00132ABC
	public override void HaveDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.HaveDamage(szDamage, szHp);
		int num = (int)szDamage[3];
		NetLiving chase = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
		this.robotController.ChangeTarget(chase);
		bool flag = szHp.Count > 0 && MatchManager.GetInstance().m_nMatchType == 3 && KingCon.inst != null && this.beMonster == 2;
		if (flag)
		{
			HeroPlayer localPlayer = this.GetLocalPlayer();
			bool flag2 = localPlayer != null && num == localPlayer.getNetid();
			if (flag2)
			{
				KingCon.inst.OnShowBossHp(base.getNetid(), this.playerCommon.GetNowHp(), this.playerCommon.m_sName);
			}
			else
			{
				KingCon.inst.OnOtherUpdateBossHp(base.getNetid(), this.playerCommon.GetNowHp(), this.playerCommon.m_sName);
			}
		}
		bool flag3 = !this.playerCommon.m_nStatForce;
		if (flag3)
		{
			int num2 = (int)szDamage[2];
			ArrayList arrayList = (ArrayList)szDamage[1];
			bool flag4 = arrayList.Count > 1;
			if (flag4)
			{
				this.forceDamage = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			}
			bool flag5 = num2 > 0 && arrayList.Count > 0;
			if (flag5)
			{
				this.AddCharForce(this.forceDamage * (float)num2 * 0.01f);
			}
		}
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x00134A70 File Offset: 0x00132C70
	public override void AddCharForce(Vector3 force)
	{
		bool needSendToServer = this.needSendToServer;
		if (needSendToServer)
		{
			((TBCharacterThirdIt)this.tChar).SpeedPowerForce(force, force.magnitude);
		}
	}

	// Token: 0x04001E15 RID: 7701
	public AniEvent aniEvent;

	// Token: 0x04001E16 RID: 7702
	public NetLiving SyncNetObj;

	// Token: 0x04001E17 RID: 7703
	public RobotAIController robotController;

	// Token: 0x04001E18 RID: 7704
	public List<AIMovePos> AIFollowPaths;

	// Token: 0x04001E19 RID: 7705
	public List<int> AllAI_Index;

	// Token: 0x04001E1A RID: 7706
	public int AI_Index;

	// Token: 0x04001E1B RID: 7707
	public bool MoveAttack;

	// Token: 0x04001E1C RID: 7708
	public int MoveType;

	// Token: 0x04001E1D RID: 7709
	public float FollowRange;

	// Token: 0x04001E1E RID: 7710
	public float EarRange;

	// Token: 0x04001E1F RID: 7711
	public float EyeAngle;

	// Token: 0x04001E20 RID: 7712
	public float EyeRange;

	// Token: 0x04001E21 RID: 7713
	public float robotHitRate;

	// Token: 0x04001E22 RID: 7714
	public float turnRotationSpeed;

	// Token: 0x04001E23 RID: 7715
	public float ReactionTime;

	// Token: 0x04001E24 RID: 7716
	public float DodgeRate;

	// Token: 0x04001E25 RID: 7717
	public float shootRange;

	// Token: 0x04001E26 RID: 7718
	public float safeRange;

	// Token: 0x04001E27 RID: 7719
	public int beMonster;

	// Token: 0x04001E28 RID: 7720
	private int m_nCreateIndex;

	// Token: 0x04001E29 RID: 7721
	private ArrayList createRot;

	// Token: 0x04001E2A RID: 7722
	public Vector3 RobotCreatePos;

	// Token: 0x04001E2B RID: 7723
	private UnityEngine.Object oPlayerUiperfab;
}

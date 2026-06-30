using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class HeroBaseIt : HeroBase
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06001093 RID: 4243 RVA: 0x00099AED File Offset: 0x00097CED
	public NetLiving hero
	{
		get
		{
			return this._hero;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06001094 RID: 4244 RVA: 0x00099AF5 File Offset: 0x00097CF5
	public Npc_Entity npc
	{
		get
		{
			return (Npc_Entity)this._hero;
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x00099B02 File Offset: 0x00097D02
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this._hero = net;
		this.LoadLinePrefeb();
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x00099B1C File Offset: 0x00097D1C
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.CheckIfCHangeSeeThoughMat();
		this.HideLinghunqiuObj();
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00099B34 File Offset: 0x00097D34
	public void LoadLinePrefeb()
	{
		string url = string.Format("EffectRes/{0}.ef", "xian_shaowang");
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

	// Token: 0x06001098 RID: 4248 RVA: 0x00099B90 File Offset: 0x00097D90
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.LinePrefeb = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00099BD4 File Offset: 0x00097DD4
	private void MoveToShaWang(Vector3 selfPos)
	{
		bool isDead = this._hero.isDead;
		if (!isDead)
		{
			bool flag = this.stretchTimer > 0f;
			if (flag)
			{
				this.stretchTimer -= Time.deltaTime;
				this.StretchRope();
			}
			else
			{
				bool flag2 = !this.moveToShawang;
				if (!flag2)
				{
					this.UpdateRopeLength(selfPos);
					bool flag3 = this.MoveTimer <= 0f;
					if (!flag3)
					{
						bool flag4 = Vector3.Distance(this.targetPos, selfPos) > 3f;
						if (flag4)
						{
							bool flag5 = !this._hero.bePlayer && this.NpcRig != null;
							if (flag5)
							{
								((Npc_Entity)this._hero).Add_Velocity(this.MoveDir * Time.deltaTime * 600f);
							}
							else
							{
								bool flag6 = this._hero.netobject_type == 6;
								if (flag6)
								{
									((RobotAI)this._hero).robotController.HitByShawangSuper(this.MoveDir * Time.deltaTime * 15f, true);
								}
							}
							this.MoveTimer -= Time.deltaTime;
						}
						else
						{
							bool flag7 = !this._hero.bePlayer && this.NpcRig != null;
							if (flag7)
							{
								((Npc_Entity)this._hero).Add_Velocity(Vector3.zero);
							}
							this.MoveTimer = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x00099D70 File Offset: 0x00097F70
	private void ShawangSuper()
	{
		bool flag = !this._hero.bePlayer && this._hero.netobject_type != 6;
		if (flag)
		{
			bool flag2 = this.moveToShawang;
			if (flag2)
			{
				this.MoveToShaWang(this.NpcRig.position);
			}
		}
		else
		{
			bool flag3 = !this.hero.isDead;
			if (flag3)
			{
				this.MoveToShaWang(this._hero.tChar.transform.position);
			}
			bool flag4 = this.ropeCreated && !this.moveToShawang;
			if (flag4)
			{
				this.DestroyLineRope();
			}
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x00099E18 File Offset: 0x00098018
	protected override void Update()
	{
		bool flag = this._hero == null;
		if (!flag)
		{
			base.Update();
			bool flag2 = ((this.hero.isCopy && !this.hero.isDead && this.hero.FpsOrTps != 1 && CFilmManager.Instance != null && CFilmManager.Instance.filmParentNetId(this.hero) == this.hero.getNetid()) || (this.hero.FpsOrTps != 1 && GameRoot.inst.IsGmTarget(this.hero) && !this.hero.isDead)) && this.hero.GetAnimatorInteger("ActiveSwith") == 2;
			if (flag2)
			{
				this.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
			this.ShawangSuper();
			bool flag3 = Time.time < this.disForHeroCheckNow;
			if (!flag3)
			{
				this.disForHeroCheckNow = Time.time + 0.1f;
				bool flag4 = this._hero.defaultMesh == null || this._hero.tpsRender == null || !this._hero.heroBase.IsHeroInvi();
				if (!flag4)
				{
					int num = (CFilmManager.Instance != null) ? CFilmManager.Instance.filmForce(this._hero) : ((this._hero.playerCommon != null) ? this._hero.playerCommon.m_nShiliServer : 0);
					bool flag5 = this._hero.disCameraNow < 10f;
					if (flag5)
					{
						bool flag6 = this._hero.playerCommon != null && num == this._hero.playerCommon.m_nShiliServer;
						if (flag6)
						{
							this._hero.renderBianSe.ChangeYinShenMat(0);
						}
						else
						{
							this._hero.renderBianSe.ChangeYinShenMat(3);
						}
					}
					else
					{
						bool flag7 = this._hero.playerCommon != null && num == this._hero.playerCommon.m_nShiliServer;
						if (flag7)
						{
							this._hero.renderBianSe.ChangeYinShenMat(0);
						}
						else
						{
							this._hero.renderBianSe.ChangeYinShenMat(1);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0009A06C File Offset: 0x0009826C
	public override void beDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.beDamage(szDamage, szHp);
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		arrayList.Add(0);
		arrayList.Add(0);
		arrayList.Add(0);
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		bool flag = false;
		int num4 = (int)szDamage[3];
		int num5 = (int)szDamage[4];
		int num6 = (int)szDamage[6];
		bool flag2 = szHp.Count > 0;
		if (flag2)
		{
			ArrayList arrayList2 = (ArrayList)szHp[7];
			num = (int)arrayList2[0] + (int)arrayList2[1] + (int)arrayList2[2] + (int)arrayList2[3];
			num2 = (int)szHp[8];
			num3 = (int)szHp[9];
		}
		bool flag3 = num6 == 0;
		if (flag3)
		{
			NetLiving netLiving = Fun.Get_NetLiving(num4);
			bool flag4 = netLiving != null;
			if (flag4)
			{
				bool flag5 = netLiving.netobject_type == 2;
				if (flag5)
				{
					flag = true;
					int master_ID = ((Npc_Entity)netLiving).Master_ID;
					NetLiving netLiving2 = Fun.Get_NetLiving(master_ID);
					bool flag6 = netLiving2 == null;
					if (flag6)
					{
						return;
					}
					num4 = netLiving2.getNetid();
				}
				else
				{
					bool flag7 = this.hero.isCopy && (gameuiCon.inst == null || gameuiCon.inst.Net == null || !gameuiCon.inst.Net.bePlayer);
					if (flag7)
					{
						return;
					}
				}
			}
			int num7 = (CFilmManager.Instance != null) ? CFilmManager.Instance.filmParentNetId(this.hero) : 0;
			bool flag8 = num4 == num7 && num > 0 && num2 == 2 && num5 != num4;
			if (flag8)
			{
				NetLiving netLiving3 = Fun.Get_NetLiving(num4);
				bool flag9 = !this.hero.isCopy || !flag;
				if (flag9)
				{
					netLiving3.ToDamage((int)szDamage[5], num6, this._hero.isDead, num, this.bOldDead);
				}
			}
			this.bOldDead = this._hero.isDead;
		}
		bool flag10 = this._hero.netobject_type != 2 && num > 0 && num2 == 1 && num3 != 2020001 && this.hero.netobject_type != 6;
		if (flag10)
		{
			this._hero.playerEffectBase.AddOneEffect("ui_jiaxue", "skill_jiexue", Fun.GetServerDate(), 1);
		}
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0009A324 File Offset: 0x00098524
	public override void Revive()
	{
		base.Revive();
		this.HideLinghunqiuObj();
		bool flag = this._hero != null && !(this._hero.userModelTps == null);
		if (flag)
		{
			CHeroWeapon component = this._hero.userModelTps.GetComponent<CHeroWeapon>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.SetWeaponParts(false);
			}
		}
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0009A38B File Offset: 0x0009858B
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		this.HideLinghunqiuObj();
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0009A39C File Offset: 0x0009859C
	public override void OnDeath()
	{
		base.OnDeath();
		this.ShawangSuperFinish();
		bool flag = this._hero.netobject_type == 1;
		if (flag)
		{
			this.AddLinghunqiuObj();
		}
		this.AddBloodObj();
		bool flag2 = this._hero != null && !(this._hero.userModelTps == null);
		if (flag2)
		{
			CHeroWeapon component = this._hero.userModelTps.GetComponent<CHeroWeapon>();
			bool flag3 = component != null;
			if (flag3)
			{
				component.SetWeaponParts(true);
			}
		}
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0009A428 File Offset: 0x00098628
	public override void ChangeYinShenState()
	{
		base.ChangeYinShenState();
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player.nForce == this._hero.nForce;
		if (flag)
		{
			((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(0);
		}
		else
		{
			((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(1);
		}
		bool flag2 = this._hero != null && !(this._hero.userModelTps == null);
		if (flag2)
		{
			CHeroWeapon component = this._hero.userModelTps.GetComponent<CHeroWeapon>();
			bool flag3 = component != null;
			if (flag3)
			{
				component.SetWeaponParts(true);
			}
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0009A4E0 File Offset: 0x000986E0
	public override void changeBackToNormalState()
	{
		base.changeBackToNormalState();
		bool flag = ((GamePlayer)this._hero).renderBianSe != null;
		if (flag)
		{
			((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(2);
		}
		bool flag2 = this._hero != null && !(this._hero.userModelTps == null);
		if (flag2)
		{
			CHeroWeapon component = this._hero.userModelTps.GetComponent<CHeroWeapon>();
			bool flag3 = component != null;
			if (flag3)
			{
				component.SetWeaponParts(false);
			}
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0009A574 File Offset: 0x00098774
	public void CheckIfCHangeSeeThoughMat()
	{
		bool flag = this._hero == null || this._hero.userModelTps == null || this._hero.playerCommon == null;
		if (!flag)
		{
			int num = (CFilmManager.Instance != null) ? CFilmManager.Instance.filmForce(this._hero) : ((this._hero.playerCommon != null) ? this._hero.playerCommon.m_nShiliServer : 0);
			CObjectRender component = this._hero.userModelTps.GetComponent<CObjectRender>();
			bool flag2 = num == this._hero.playerCommon.m_nShiliServer;
			if (flag2)
			{
				bool flag3 = component != null;
				if (flag3)
				{
					component.SetEnemyFriendRenderParticle(false, false);
				}
			}
			else
			{
				bool flag4 = this._hero.renderBianSe != null;
				if (flag4)
				{
					this._hero.renderBianSe.SetLuoLiParticle(false);
				}
				bool flag5 = component != null;
				if (flag5)
				{
					component.SetEnemyFriendRenderParticle(true, false);
				}
			}
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0009A67E File Offset: 0x0009887E
	public override void HitByShawangSuper(string state, int _attackId, ArrayList hitIds)
	{
		base.HitByShawangSuper(state, _attackId, hitIds);
		this.releaseShawangSuper(state, _attackId, hitIds);
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0009A698 File Offset: 0x00098898
	private void releaseShawangSuper(string state, int _attackId, ArrayList hitIds)
	{
		for (int i = 0; i < hitIds.Count; i++)
		{
			int num = (int)hitIds[i];
			bool flag = num != this._hero.getNetid() || this._hero.isDead;
			if (!flag)
			{
				bool flag2 = state == "start" && !this.moveToShawang;
				if (flag2)
				{
					this.MoveTimer = 1f;
					this.stretchTimer = 0.35f;
					NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
					bool flag3 = ((GamePlayer)netLiving).playerDazhao.superNpc == null || ((GamePlayer)netLiving).playerDazhao.superNpc.thisModel == null;
					if (!flag3)
					{
						this.targetPos = ((GamePlayer)netLiving).playerDazhao.superNpc.thisModel.transform.Find("m1").transform.position;
						bool bePlayer = this._hero.bePlayer;
						if (bePlayer)
						{
							this.moveToShawang = true;
							this._hero.tChar.m_MoveByAnimator = false;
							this._hero.tChar.hangInAir = true;
							this.MoveDir = (this.targetPos - this._hero.tChar.transform.position).normalized;
							this.StretchSpeed = Vector3.Distance(netLiving.tChar.transform.position, this.hero.tChar.transform.position) / 0.35f;
							bool flag4 = this.MoveDir.y > 0f;
							if (flag4)
							{
								this.MoveDir.y = 0f;
							}
							this.currentPos = this.targetPos;
							this.CreateLockRope();
						}
						else
						{
							bool flag5 = !this.moveToShawang && ((Npc_Entity)this._hero).Physics == 1;
							if (flag5)
							{
								this.moveToShawang = true;
								this.NpcRig = base.GetComponent<Rigidbody>();
								((Npc_Entity)this._hero).Action_State = false;
								this.MoveDir = (this.targetPos - base.transform.position).normalized;
								this.StretchSpeed = Vector3.Distance(netLiving.tChar.transform.position, base.transform.position) / 0.35f;
								bool flag6 = this.MoveDir.y > 0f;
								if (flag6)
								{
									this.MoveDir.y = 0f;
								}
								this.currentPos = this.targetPos;
								this.CreateLockRope();
							}
						}
					}
				}
				else
				{
					bool bePlayer2 = this._hero.bePlayer;
					if (bePlayer2)
					{
						this.ShawangSuperFinish();
					}
					else
					{
						bool flag7 = ((Npc_Entity)this._hero).Physics == 1;
						if (flag7)
						{
							((Npc_Entity)this._hero).Add_Velocity(Vector3.zero);
							this.ShawangSuperFinish();
						}
					}
				}
			}
		}
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0009A9D8 File Offset: 0x00098BD8
	private void CreateLockRope()
	{
		bool flag = this.LinePrefeb != null;
		if (flag)
		{
			GameObject gameObject = CEffectPools.Instance.CreateGameObject(this.LinePrefeb, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.hero._playerRootEffectContainer.transform;
			gameObject.transform.position = Vector3.zero + Vector3.up;
			this.linerender = gameObject.GetComponentInChildren<LineRenderer>();
			this.rope = gameObject;
			this.ropeCreated = true;
		}
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0009AA6C File Offset: 0x00098C6C
	private void StretchRope()
	{
		bool flag = this.linerender != null;
		if (flag)
		{
			Vector3 a = -this.MoveDir;
			this.linerender.numPositions = 2;
			this.linerender.SetPosition(0, this.targetPos);
			this.currentPos = this.targetPos + a * this.StretchSpeed * Mathf.Pow(0.35f - this.stretchTimer, 2f) / a.magnitude;
			this.linerender.SetPosition(1, this.currentPos);
		}
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0009AB14 File Offset: 0x00098D14
	private void DestroyLineRope()
	{
		bool flag = this.rope != null;
		if (flag)
		{
			this.ropeCreated = false;
			UnityEngine.Object.Destroy(this.rope);
		}
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0009AB48 File Offset: 0x00098D48
	private void UpdateRopeLength(Vector3 selfPos)
	{
		Vector3 vector = Vector3.zero;
		vector = (this.targetPos - selfPos).normalized;
		float num = Vector3.Distance(selfPos, this.targetPos);
		Vector3 vector2 = vector;
		float num2 = Vector3.Angle(vector2, vector);
		float d = num * (1f - num2 / 180f) * 0.3f;
		Vector3 vector3 = selfPos + vector2 * d;
		bool flag = this.linerender != null;
		if (flag)
		{
			this.linerender.numPositions = 50;
			for (int i = 0; i < 50; i++)
			{
				Vector3 a = Vector3.Lerp(selfPos, vector3, (float)i / 50f);
				Vector3 b = Vector3.Lerp(vector3, this.targetPos, (float)i / 50f);
				Vector3 position = Vector3.Lerp(a, b, (float)i / 50f);
				this.linerender.SetPosition(i, position);
			}
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0009AC40 File Offset: 0x00098E40
	private void ShawangSuperFinish()
	{
		bool flag = this.moveToShawang;
		if (flag)
		{
			this.stretchTimer = 0f;
			this.MoveTimer = 0f;
			bool bePlayer = this._hero.bePlayer;
			if (bePlayer)
			{
				this._hero.tChar.hangInAir = false;
				this._hero.tChar.m_MoveByAnimator = true;
				bool flag2 = this.hero.netobject_type == 6;
				if (flag2)
				{
					((RobotAI)this._hero).robotController.HitByShawangSuper(this.MoveDir * Time.deltaTime * 15f, false);
				}
			}
			else
			{
				bool flag3 = ((Npc_Entity)this._hero).Physics == 1;
				if (flag3)
				{
					((Npc_Entity)this._hero).Action_State = true;
				}
			}
			this.moveToShawang = false;
		}
		this.DestroyLineRope();
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0009AD28 File Offset: 0x00098F28
	private void AddLinghunqiuObj()
	{
		bool flag = this._hero == null || this._hero.haveDestroy || !this._hero.playerHaveCreate;
		if (!flag)
		{
			NetLiving netLiving = (this.hero.isCopy && CFilmManager.Instance != null) ? CFilmManager.Instance.GetFilmNetLiving(CFilmManager.Instance.filmparentnetid) : fGameManager.Heroplayer;
			bool flag2 = netLiving == null || netLiving.playerCommon == null || this._hero.playerCommon == null || netLiving.heroBase == null || netLiving.playerCommon.m_nShili != this._hero.playerCommon.m_nShili || (this._hero.netobject_type == 2 && this.npc.Cure != 1) || netLiving.heroData == null || (netLiving.heroData.heroclass != "LuoLi" && netLiving.heroData.heroclass != "Guanyin") || netLiving.getNetid() == this._hero.getNetid() || (this._hero.isDead && this._hero.playerCommon.m_nDeathAttackId == this.hero.getNetid() && this._hero.playerCommon.m_nDeadType == 1);
			if (!flag2)
			{
				bool flag3 = this.linghunqiuObj != null;
				if (flag3)
				{
					this.LinghunqiuShow(true);
					this.ChangLinghunqiuFather();
				}
				else
				{
					WWW resWWW = GameRoot.GetResWWW("EffectRes/linhunqiu.ef");
					bool flag4 = resWWW == null || resWWW.error != null;
					if (flag4)
					{
						GameRoot.LoadAsset("EffectRes/linhunqiu.ef", new fResourceManager.DownFinishDelegate(this.loadLinghunqiuPrefab), 0);
					}
					else
					{
						this.loadLinghunqiuPrefab(resWWW, 0);
					}
				}
				bool flag5 = CFilmManager.Instance == null || !CFilmManager.Instance.playing;
				if (flag5)
				{
					bool flag6 = netLiving.heroData.heroclass == "LuoLi";
					if (flag6)
					{
						((HeroLuoLiMe)netLiving.heroBase).SetDeadMember(this._hero, 1);
					}
					else
					{
						bool flag7 = netLiving.heroData.heroclass == "Guanyin";
						if (flag7)
						{
							((HeroGuanyinMe)netLiving.heroBase).SetDeadMember(this._hero, 1);
						}
					}
				}
				else
				{
					bool isCopy = netLiving.isCopy;
					if (isCopy)
					{
						bool flag8 = netLiving.heroData.heroclass == "LuoLi";
						if (flag8)
						{
							((HeroLuoLiIt)netLiving.heroBase).SetDeadMember(this._hero, 1);
						}
						else
						{
							bool flag9 = netLiving.heroData.heroclass == "Guanyin";
							if (flag9)
							{
								((HeroGuanyinIt)netLiving.heroBase).SetDeadMember(this._hero, 1);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0009B030 File Offset: 0x00099230
	public override void HideLinghunqiuObj()
	{
		base.HideLinghunqiuObj();
		bool flag = this.linghunqiuObj != null && this.linghunqiuObj.activeSelf;
		if (flag)
		{
			this.LinghunqiuShow(false);
		}
		bool flag2 = this._hero == null || this._hero.netobject_type != 1;
		if (!flag2)
		{
			NetLiving netLiving = (this.hero.isCopy && CFilmManager.Instance != null) ? CFilmManager.Instance.GetFilmNetLiving(CFilmManager.Instance.filmparentnetid) : fGameManager.Heroplayer;
			bool flag3 = netLiving != null && netLiving.playerCommon != null && this._hero.playerCommon != null && !(netLiving.heroBase == null) && netLiving.playerCommon.m_nShili == this._hero.playerCommon.m_nShili && netLiving.heroData != null && (netLiving.heroData.heroclass == "LuoLi" || netLiving.heroData.heroclass == "Guanyin") && netLiving.getNetid() != this._hero.getNetid();
			if (flag3)
			{
				bool flag4 = !netLiving.isCopy;
				if (flag4)
				{
					bool flag5 = netLiving.heroData.heroclass == "LuoLi";
					if (flag5)
					{
						((HeroLuoLiMe)netLiving.heroBase).SetDeadMember(this._hero, 0);
					}
					else
					{
						bool flag6 = netLiving.heroData.heroclass == "Guanyin";
						if (flag6)
						{
							((HeroGuanyinMe)netLiving.heroBase).SetDeadMember(this._hero, 0);
						}
					}
				}
				else
				{
					bool flag7 = netLiving.heroData.heroclass == "LuoLi";
					if (flag7)
					{
						((HeroLuoLiIt)netLiving.heroBase).SetDeadMember(this._hero, 0);
					}
					else
					{
						bool flag8 = netLiving.heroData.heroclass == "Guanyin";
						if (flag8)
						{
							((HeroGuanyinIt)netLiving.heroBase).SetDeadMember(this._hero, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0009B25C File Offset: 0x0009945C
	private void loadLinghunqiuPrefab(WWW www, object p)
	{
		bool flag = www != null && string.IsNullOrEmpty(www.error);
		if (flag)
		{
			UnityEngine.Object original = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.linghunqiuObj = (GameObject)UnityEngine.Object.Instantiate(original);
			bool flag2 = !(this.linghunqiuObj == null);
			if (flag2)
			{
				this.ChangLinghunqiuFather();
				this.LinghunqiuShow(true);
			}
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0009B2D0 File Offset: 0x000994D0
	public void LinghunqiuShow(bool hide)
	{
		bool flag = this.linghunqiuObj.activeSelf != hide;
		if (flag)
		{
			this.linghunqiuObj.SetActive(hide);
		}
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0009B304 File Offset: 0x00099504
	public void ChangLinghunqiuFather()
	{
		bool flag = this._hero != null && !(this._hero.tChar == null);
		if (flag)
		{
			bool onGround = this._hero.tChar.onGround;
			if (onGround)
			{
				this.linghunqiuObj.transform.SetParent(this._hero.UserControl.transform);
			}
			else
			{
				this.linghunqiuObj.transform.SetParent(this._hero.userBip);
			}
			this.linghunqiuObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this.linghunqiuObj.transform.localScale = Vector3.one;
			this.linghunqiuObj.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0009B3D8 File Offset: 0x000995D8
	private void AddBloodObj()
	{
		bool flag = MatchManager.GetInstance().m_nMatchType == 3;
		if (!flag)
		{
			bool flag2 = !this.hero.isCopy;
			if (flag2)
			{
				NetLiving heroplayer = fGameManager.Heroplayer;
			}
			else
			{
				NetLiving netLiving = (CFilmManager.Instance != null) ? CFilmManager.Instance.GetFilmNetLiving(CFilmManager.Instance.filmparentnetid) : null;
			}
			bool flag3 = (this._hero.netobject_type != 2 || this.npc.Cure == 1) && !this._hero.haveDestroy && this._hero.playerHaveCreate;
			if (flag3)
			{
				WWW resWWW = GameRoot.GetResWWW("HeroAnimator/npc_lhq.mo");
				bool flag4 = resWWW == null || resWWW.error != null;
				if (flag4)
				{
					GameRoot.LoadAsset("HeroAnimator/npc_lhq.mo", new fResourceManager.DownFinishDelegate(this.loadBloodPrefab), 0);
				}
				else
				{
					this.loadBloodPrefab(resWWW, 0);
				}
			}
		}
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0009B4D4 File Offset: 0x000996D4
	private void loadBloodPrefab(WWW www, object p)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (!flag)
		{
			NetLiving netLiving = (CFilmManager.Instance != null && CFilmManager.Instance.playing) ? CFilmManager.Instance.GetFilmNetLiving(CFilmManager.Instance.filmparentnetid) : fGameManager.Heroplayer;
			bool flag2 = netLiving != null && netLiving.playerCommon != null && this._hero.playerCommon != null && !netLiving.haveDestroy && !netLiving.isDead && netLiving.heroData != null && !(netLiving.heroData.heroclass != "JingKe") && netLiving.getNetid() != this._hero.getNetid() && netLiving.playerCommon.m_nShili != this._hero.playerCommon.m_nShili && this._hero != null && !this._hero.haveDestroy && www != null;
			if (flag2)
			{
				UnityEngine.Object original = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
				bool flag3 = CFilmManager.Instance == null || !CFilmManager.Instance.playing;
				if (flag3)
				{
					gameObject.transform.parent = ((HeroJingKeMe)netLiving.heroBase).BloodFather();
				}
				else
				{
					gameObject.transform.parent = ((HeroJingKeIt)netLiving.heroBase).BloodFilmFather();
				}
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = this._hero.userModelTps.transform.position + 2f * Vector3.up;
				gameObject.AddComponent<BloodFade>();
			}
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0009B6C7 File Offset: 0x000998C7
	public void CloseCar()
	{
		this.daweiCar = true;
		Debug.Log(this.laosiji.getNetid());
		HeroBase.goBalckCar = false;
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0009B6F0 File Offset: 0x000998F0
	public void BlackCar()
	{
		NetLiving netLiving = (this.hero.isCopy && CFilmManager.Instance != null) ? CFilmManager.Instance.GetFilmNetLiving(CFilmManager.Instance.filmparentnetid) : fGameManager.Heroplayer;
		Debug.Log("运行ing111111111111");
		bool flag = netLiving == null || netLiving.playerCommon == null || this.laosiji == null || this.laosiji.playerCommon == null || netLiving.playerCommon.m_nShili != this.laosiji.playerCommon.m_nShili;
		if (!flag)
		{
			this.value -= Time.deltaTime;
			Debug.Log("运行ing22222222222");
			bool flag2 = Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.N) || this.value <= 0f;
			if (flag2)
			{
				this.RollCar();
			}
			bool flag3 = CFilmManager.Instance != null && CFilmManager.Instance.playing;
			if (flag3)
			{
				Debug.Log("显示ing333333333333");
				bool flag4 = ChatUICon.inst != null;
				if (flag4)
				{
					ChatUICon.inst.ClosePushUI(false);
				}
				bool flag5 = gameuiCon.inst != null;
				if (flag5)
				{
					gameuiCon.inst.mainMsgUI.OnCarHint(true, this.laosiji.playerCommon.m_sName, this.value / 5f);
				}
				Debug.Log("222222222222222");
			}
		}
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0009B870 File Offset: 0x00099A70
	private void RollCar()
	{
		this.daweiCar = false;
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.OnCarHint(false, null, 0f);
		}
		this.value = 5f;
		bool flag2 = ChatUICon.inst != null;
		if (flag2)
		{
			ChatUICon.inst.ClosePushUI(true);
		}
	}

	// Token: 0x04000D30 RID: 3376
	private const float disForHeroCheckTime = 0.1f;

	// Token: 0x04000D31 RID: 3377
	private float disForHeroCheckNow;

	// Token: 0x04000D32 RID: 3378
	public bool moveToShawang;

	// Token: 0x04000D33 RID: 3379
	private float MoveTimer = 1f;

	// Token: 0x04000D34 RID: 3380
	private Vector3 MoveDir = Vector3.zero;

	// Token: 0x04000D35 RID: 3381
	private Vector3 targetPos = Vector3.zero;

	// Token: 0x04000D36 RID: 3382
	private UnityEngine.Object LinePrefeb;

	// Token: 0x04000D37 RID: 3383
	private Rigidbody NpcRig;

	// Token: 0x04000D38 RID: 3384
	public PlayerCommon playerCommon;

	// Token: 0x04000D39 RID: 3385
	private GameObject linghunqiuObj;

	// Token: 0x04000D3A RID: 3386
	private bool bOldDead;

	// Token: 0x04000D3B RID: 3387
	private float StretchSpeed;

	// Token: 0x04000D3C RID: 3388
	private float stretchTimer;

	// Token: 0x04000D3D RID: 3389
	private Vector3 MoveTargetPos;

	// Token: 0x04000D3E RID: 3390
	private GameObject rope;

	// Token: 0x04000D3F RID: 3391
	private LineRenderer linerender;

	// Token: 0x04000D40 RID: 3392
	private bool ropeCreated;

	// Token: 0x04000D41 RID: 3393
	private Vector3 currentPos;

	// Token: 0x04000D42 RID: 3394
	public bool daweiCar;

	// Token: 0x04000D43 RID: 3395
	private NetLiving laosiji;

	// Token: 0x04000D44 RID: 3396
	private float value = 5f;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class HeroBaseMe : HeroBase
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0009B90A File Offset: 0x00099B0A
	public HeroPlayer hero
	{
		get
		{
			return (HeroPlayer)this._hero;
		}
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0009B918 File Offset: 0x00099B18
	public HeroBaseMe()
	{
		this.beMeClass = true;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0009B97C File Offset: 0x00099B7C
	public void SetStat(string type, int stat)
	{
		bool flag = !this.skillStats.ContainsKey(type);
		if (flag)
		{
			this.skillStats.Add(type, stat);
		}
		else
		{
			this.skillStats[type] = stat;
		}
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0009B9C0 File Offset: 0x00099BC0
	public int GetStat(string type)
	{
		bool flag = !this.skillStats.ContainsKey(type);
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = this.skillStats[type];
		}
		return result;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0009B9F8 File Offset: 0x00099BF8
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

	// Token: 0x060010BA RID: 4282 RVA: 0x0009BA54 File Offset: 0x00099C54
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.LinePrefeb = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0009BA98 File Offset: 0x00099C98
	public override void HeroInit(NetLiving net, string heroclass)
	{
		this._hero = (HeroPlayer)net;
		base.HeroInit(net, heroclass);
		bool flag = !this._hero.bePlayer;
		if (!flag)
		{
			bool flag2 = heroclass != "LuoLi";
			if (flag2)
			{
				foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
				{
					NetLiving netLiving = (NetLiving)keyValuePair.Value;
					bool flag3 = netLiving.nForce == this._hero.nForce;
					if (flag3)
					{
						netLiving.heroBase.HideLinghunqiuObj();
					}
				}
			}
			this.moveToShawang = false;
			this.MoveTimer = 0f;
			this.MoveDir = Vector3.zero;
			this.LoadLinePrefeb();
			this.playerLevel = PlayerDataManager.getInstance().HUserData.m_nLevel;
			this.hero.playerCommon.negativePlay_Attack = 0f;
			this.hero.playerCommon.negativePlay_Status = 0f;
			this.hero.bePlayActive = true;
			this.hero.beSuperSkill = false;
			bool isSkyLadderMatch = SkyLadderManager.GetInstance().isSkyLadderMatch;
			if (isSkyLadderMatch)
			{
				this.SL_MulTimer1 = 3f;
				this.SL_MulTimer2 = 2f;
				this.SL_MulTimer3 = 1f;
			}
			else
			{
				this.SL_MulTimer1 = (this.SL_MulTimer2 = (this.SL_MulTimer3 = 1f));
			}
		}
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0009BC3C File Offset: 0x00099E3C
	public virtual void OnChangeToFps()
	{
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0009BC3F File Offset: 0x00099E3F
	public virtual void ChanngeLayerCollider(bool revive = false)
	{
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0009BC44 File Offset: 0x00099E44
	protected override void Update()
	{
		base.Update();
		bool flag = this.hero.HuDongChangeCameraIng && this.hero.FpsOrTps != 1 && this.hero.nHuDongState == 0;
		if (flag)
		{
			this.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			this.hero.HuDongChangeCameraIng = false;
			this.OnChangeToFps();
		}
		this.checkTimer -= Time.deltaTime;
		bool flag2 = this.checkTimer < 0f;
		if (flag2)
		{
			this.checkTimer = 1f;
			this.EnemyOnlineCount = this.enemyOnlineCount();
		}
		bool flag3 = false; // [MOD] Inactivity / AFK Kick 100% desativado para testes livres
		if (flag3)
		{
			bool flag4 = !this.hero.isDead && this.EnemyOnlineCount > 0;
			if (flag4)
			{
				bool flag5 = fUIManager.mInst.GetNowGameState() > 0;
				if (flag5)
				{
					bool flag6 = this.playerLevel > 10;
					if (flag6)
					{
						this.hero.playerCommon.negativePlay_Status += Time.deltaTime;
					}
					bool flag7 = this.hero.playerCommon.m_nFightStat == 1;
					if (flag7)
					{
						this.hero.playerCommon.negativePlay_Attack = 0f;
						this.NegativeWarning_Attack = false;
						this.hero.playerCommon.negativePlay_Walk = 0f;
						this.NegativeWarning_Walk = false;
					}
					bool flag8 = this.hero.charMove.state.move.magnitude > 0f;
					if (flag8)
					{
						this.hero.playerCommon.negativePlay_Walk = 0f;
						this.NegativeWarning_Walk = false;
					}
					else
					{
						this.hero.playerCommon.negativePlay_Walk += Time.deltaTime;
						bool flag9 = this.hero.playerCommon.negativePlay_Walk > 30f * this.SL_MulTimer1 && !this.NegativeWarning_Walk;
						if (flag9)
						{
							this.NegativeWarning_Walk = true;
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30053, new object[0]), "ffdd00", "system");
						}
						bool flag10 = this.hero.playerCommon.negativePlay_Walk > 45f * this.SL_MulTimer1;
						if (flag10)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30054, new object[0]), "ffdd00", "system");
							fUIManager.GetInstance().GotoHall(3);
						}
					}
					bool flag11 = this.hero.playerCommon.m_nInJidi == 1;
					if (flag11)
					{
						this.hero.playerCommon.negativePlay_Attack += Time.deltaTime;
						bool flag12 = this.hero.playerCommon.negativePlay_Attack > 90f * this.SL_MulTimer2 && !this.NegativeWarning_Attack;
						if (flag12)
						{
							this.NegativeWarning_Attack = true;
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30055, new object[0]), "ffdd00", "system");
						}
						bool flag13 = this.hero.playerCommon.negativePlay_Attack > 120f * this.SL_MulTimer2;
						if (flag13)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30056, new object[0]), "ffdd00", "system");
							fUIManager.GetInstance().GotoHall(3);
						}
					}
					bool flag14 = this.hero.playerCommon.negativePlay_Status > 180f * this.SL_MulTimer3 && !this.NegativeWarning_NoFireState;
					if (flag14)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30058, new object[0]), "ffdd00", "system");
						this.NegativeWarning_NoFireState = true;
					}
					bool flag15 = this.hero.playerCommon.negativePlay_Status > 210f * this.SL_MulTimer3;
					if (flag15)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(30059, new object[0]), "ffdd00", "system");
						fUIManager.GetInstance().GotoHall(3);
					}
				}
			}
			else
			{
				this.hero.playerCommon.negativePlay_Attack = 0f;
				this.NegativeWarning_Attack = false;
				this.hero.playerCommon.negativePlay_Walk = 0f;
				this.NegativeWarning_Walk = false;
				this.hero.playerCommon.negativePlay_Status = 0f;
			}
		}
		this.ShaWangSuper();
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0009C0F0 File Offset: 0x0009A2F0
	private int enemyOnlineCount()
	{
		int num = 0;
		bool flag = MatchManager.GetInstance().m_nMatchType != 3;
		int result;
		if (flag)
		{
			foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
			{
				NetLiving netLiving = (NetLiving)keyValuePair.Value;
				bool flag2 = netLiving.bePlayer && netLiving.nForce != this._hero.nForce;
				if (flag2)
				{
					num++;
				}
			}
			result = num;
		}
		else
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
	private void ShaWangSuper()
	{
		bool isDead = this.hero.isDead;
		if (!isDead)
		{
			bool flag = this.stretchTimer > 0f;
			if (flag)
			{
				this.shawangSuperTimer -= Time.deltaTime;
				this.stretchTimer -= Time.deltaTime;
				this.StretchRope();
			}
			else
			{
				bool flag2 = !this.moveToShawang;
				if (!flag2)
				{
					bool flag3 = this.shawang == null || this.shawang.haveDestroy || ((GamePlayer)this.shawang).playerDazhao.superNpc == null || ((GamePlayer)this.shawang).playerDazhao.superNpc.thisModel == null;
					if (flag3)
					{
						this.ShawangSuperFinish();
					}
					this.shawangSuperTimer -= Time.deltaTime;
					bool flag4 = this.shawangSuperTimer <= 0f;
					if (flag4)
					{
						this.ShawangSuperFinish();
					}
					this.UpdateRopeLength();
					bool flag5 = this.MoveTimer > 0f;
					if (flag5)
					{
						bool flag6 = Vector3.Distance(this.targetPos, this.hero.tChar.transform.position) > 3f;
						if (flag6)
						{
							((TBCharacterThirdMe)this.hero.tChar).SpeedMoveForce(this.MoveDir * 600f, 0.1f, false, 1000f, 1000f);
						}
						else
						{
							this.MoveTimer = 0f;
						}
						this.MoveTimer -= Time.deltaTime;
					}
				}
			}
		}
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0009C360 File Offset: 0x0009A560
	public virtual bool InputCheck()
	{
		bool flag = this.hero == null || this.hero.wInput == null || this.hero.haveDestroy || !this.hero.playerHaveCreate;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = !this.hero.wInput.focus();
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = !fGameManager.Heroplayer.canControl;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = this.hero.playerCommon.m_nChenMo == 1;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this.hero.playerCommon.m_nDongjie == 1 || this.hero.playerCommon.m_nHunMi == 1;
						result = !flag5;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0009C43C File Offset: 0x0009A63C
	public override void SuperSkillDo(object parm)
	{
		bool flag = this.autoDisableReadyitem && this.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			this.hero.heroSkill.ReadyItemOp(this.hero.heroSkill.defaultReadySkill, false, false);
		}
		bool flag2 = this.bAutoBreakCartridge;
		if (flag2)
		{
			((CWeaponControlMe)this.hero.wc).cancelFillCartridge();
		}
		base.SuperSkillDo(parm);
		this.hero.bePlayActive = false;
		this.hero.beSuperSkill = true;
		this.doSkillTime = 0f;
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0009C4E0 File Offset: 0x0009A6E0
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		this._hero.playerCommon.m_dtToushi = Fun.GetServerDate();
		this._hero.playerCommon.m_dtToushiCheck = new CCheckEditDateTime(this._hero.playerCommon.m_dtToushi);
		bool flag = (CWeaponControlMe)this.hero.wc != null;
		if (flag)
		{
			((CWeaponControlMe)this.hero.wc).CheckMustFillCartridge();
		}
		this.hero.bePlayActive = true;
		this.hero.beSuperSkill = false;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0009C57B File Offset: 0x0009A77B
	public virtual void BreakSth(int type)
	{
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0009C57E File Offset: 0x0009A77E
	public virtual void RoundBegin(int type)
	{
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0009C584 File Offset: 0x0009A784
	public void createSpecialSkill(int id, string _spic, string _kuaijieicon)
	{
		bool flag = !this.playerSpecialSkills.ContainsKey(id);
		if (flag)
		{
			this.playerSpecialSkills[id] = new CSpecialSkillStruct();
		}
		CSpecialSkillStruct cspecialSkillStruct = this.playerSpecialSkills[id];
		cspecialSkillStruct.sid = id;
		cspecialSkillStruct.icon = _spic;
		cspecialSkillStruct.kuaijieicon = _kuaijieicon;
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0009C5DC File Offset: 0x0009A7DC
	public override void beDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.beDamage(szDamage, szHp);
		int num = (int)szDamage[3];
		int num2 = (int)szDamage[6];
		int num3 = (int)szDamage[8];
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		bool flag = szHp.Count > 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szHp[7];
			num4 = (int)arrayList[0];
			num5 = (int)szHp[8];
			num6 = (int)szHp[9];
		}
		bool flag2 = num4 > 0 && num5 == 1 && num6 != 2020001;
		if (flag2)
		{
			this.hero.playerEffectBase.AddOneEffect("ui_jiaxue", "skill_jiexue", Fun.GetServerDate(), 1);
		}
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0009C6B0 File Offset: 0x0009A8B0
	private void OnTriggerEnter(Collider _Collider)
	{
		bool flag = _Collider.CompareTag("Transmission");
		if (flag)
		{
			this.Trigger_Transmission(_Collider.gameObject.transform.root.gameObject);
		}
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0009C6EC File Offset: 0x0009A8EC
	protected override void OnTriggerStay(Collider _Collider)
	{
		base.OnTriggerStay(_Collider);
		bool flag = _Collider.CompareTag("Arena_Effect");
		if (flag)
		{
			this.Trigger_Arena_Effect();
		}
		else
		{
			bool flag2 = _Collider.CompareTag("HeiAnEffHurt");
			if (flag2)
			{
				this.Trigger_HeiAn_Effect();
			}
		}
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0009C734 File Offset: 0x0009A934
	private void Trigger_Arena_Effect()
	{
		bool flag = Time.time >= this._Arena_Effect_Time;
		if (flag)
		{
			this._Arena_Effect_Time = Time.time + 1f;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.hero.getNetid());
			arrayList.Add(this.hero.getNetid());
			arrayList.Add("Area_Hurt");
			this._hero.SendMessageToServer(1000001, arrayList);
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0009C7C0 File Offset: 0x0009A9C0
	private void Trigger_Transmission(GameObject _Value)
	{
		ArrayList arrayList = new ArrayList();
		Sub_Transmission_Controller component = _Value.GetComponent<Sub_Transmission_Controller>();
		bool flag = !component.Entity_Component.Death_State;
		if (flag)
		{
			arrayList.Add(component.Entity_Component.getNetid());
			this._hero.SendMessageToServer(1010100, arrayList);
		}
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0009C81C File Offset: 0x0009AA1C
	private void Trigger_HeiAn_Effect()
	{
		bool flag = Time.time >= this._HeiAn_Hurt_Time;
		if (flag)
		{
			this._HeiAn_Hurt_Time = Time.time + 1f;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.hero.getNetid());
			arrayList.Add(this.hero.getNetid());
			arrayList.Add("HeiAn_Hurt");
			this._hero.SendMessageToServer(1000001, arrayList);
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0009C8A5 File Offset: 0x0009AAA5
	public override void ChangeYinShenState()
	{
		base.ChangeYinShenState();
		((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(0);
		((GamePlayer)this._hero).FPSrenderBianSe.ChangeYinShenFPSMat(0);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0009C8DD File Offset: 0x0009AADD
	public override void changeBackToNormalState()
	{
		base.changeBackToNormalState();
		((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(2);
		((GamePlayer)this._hero).FPSrenderBianSe.ChangeYinShenFPSMat(2);
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0009C915 File Offset: 0x0009AB15
	public override void BulletFillBegin()
	{
		base.BulletFillBegin();
		this.hero.bePlayActive = false;
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0009C92C File Offset: 0x0009AB2C
	public override void BulletFillBreak()
	{
		base.BulletFillBreak();
		this.hero.bePlayActive = true;
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0009C943 File Offset: 0x0009AB43
	public override void BulletFilled()
	{
		base.BulletFilled();
		this.hero.bePlayActive = true;
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0009C95C File Offset: 0x0009AB5C
	public override void OnDeath()
	{
		this.ShawangSuperFinish();
		this.hero.SetAnimator("HuDongState", 2, 0, 2);
		this.hero.SetAnimator("ActiveSwith", 2, 0, 2);
		this.hero.bePlayActive = false;
		bool flag = this.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			this.hero.heroSkill.ReadyItemOp(this.hero.heroSkill.defaultReadySkill, false, false);
		}
		this.hero.HuDongChangeCameraIng = false;
		this.hero.playerCommon.negativePlay_Attack = 0f;
		this.NegativeWarning_Attack = false;
		this.hero.playerCommon.negativePlay_Walk = 0f;
		this.NegativeWarning_Walk = false;
		this.hero.playerCommon.negativePlay_Status = 0f;
		base.OnDeath();
		bool flag2 = gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.mainMsgUI.OnCarHint(false, null, 0f);
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0009CA74 File Offset: 0x0009AC74
	public override void Revive()
	{
		this.hero.bePlayActive = true;
		base.Revive();
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.OnFriendDeadNum(0);
		}
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0009CAB8 File Offset: 0x0009ACB8
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		if (m_bool)
		{
			this.hero.wInput.notInput = true;
		}
		bool flag = !m_bool;
		if (flag)
		{
			this.hero.wInput.notInput = false;
		}
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0009CB04 File Offset: 0x0009AD04
	public override void BreakSupperSkill()
	{
		base.BreakSupperSkill();
		bool flag = !this.hero.isDead;
		if (flag)
		{
			this.hero.SendMessageToServer(31211, new ArrayList());
		}
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0009CB44 File Offset: 0x0009AD44
	public override void HitByShawangSuper(string state, int _attackId, ArrayList hitIds)
	{
		base.HitByShawangSuper(state, _attackId, hitIds);
		foreach (object obj in hitIds)
		{
			int num = (int)obj;
			bool flag = num != this.hero.getNetid();
			if (!flag)
			{
				bool flag2 = state == "start" && !this.moveToShawang;
				if (flag2)
				{
					this.hero.tChar.m_MoveByAnimator = false;
					this.MoveTimer = 1f;
					this.stretchTimer = 0.35f;
					this.moveToShawang = true;
					this.shawang = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
					Npc_Entity superNpc = ((GamePlayer)this.shawang).playerDazhao.superNpc;
					bool flag3 = ((GamePlayer)this.shawang).playerDazhao.superNpc != null && !superNpc.haveDestroy;
					if (flag3)
					{
						this.targetPos = superNpc.UserControl.transform.position + superNpc.UserControl.transform.up * 1.5f;
					}
					this.MoveDir = (this.targetPos - this.hero.tChar.transform.position).normalized;
					bool flag4 = this.MoveDir.y > 0f;
					if (flag4)
					{
						this.MoveDir.y = 0f;
					}
					this.StretchSpeed = Vector3.Distance(this.targetPos, this.hero.tChar.transform.position) / 0.35f;
					this.currentPos = this.targetPos;
					((TBCharacterThirdMe)this.hero.tChar).ForceZeroPhysic = true;
					((TBCharacterThirdMe)this.hero.tChar).airDrag = 300f;
					this.shawangSuperTimer = 7.5f;
					this.CreateLockRope();
				}
				else
				{
					bool flag5 = this.moveToShawang;
					if (flag5)
					{
						this.ShawangSuperFinish();
					}
				}
			}
		}
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x0009CDA0 File Offset: 0x0009AFA0
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
		}
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0009CE2C File Offset: 0x0009B02C
	private void DestroyLineRope()
	{
		bool flag = this.rope != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.rope);
		}
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0009CE58 File Offset: 0x0009B058
	private void StretchRope()
	{
		Vector3 a = -this.MoveDir;
		bool flag = this.linerender != null;
		if (flag)
		{
			this.linerender.numPositions = 2;
			this.linerender.SetPosition(0, this.targetPos);
			this.currentPos = this.targetPos + a * this.StretchSpeed * Mathf.Pow(0.35f - this.stretchTimer, 2f) / a.magnitude;
			this.linerender.SetPosition(1, this.currentPos);
		}
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0009CEFC File Offset: 0x0009B0FC
	private void UpdateRopeLength()
	{
		this.linerender.numPositions = 2;
		this.linerender.SetPosition(0, this.hero.tChar.transform.position);
		this.linerender.SetPosition(1, this.targetPos);
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0009CF4C File Offset: 0x0009B14C
	private void ShawangSuperFinish()
	{
		bool flag = this.moveToShawang;
		if (flag)
		{
			this.stretchTimer = 0f;
			this.moveToShawang = false;
			this.hero.tChar.m_MoveByAnimator = true;
			((TBCharacterThirdMe)this.hero.tChar).ForceZeroPhysic = false;
			((TBCharacterThirdMe)this.hero.tChar).airDrag = 1f;
			this.DestroyLineRope();
		}
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0009CFC1 File Offset: 0x0009B1C1
	public virtual void Break_Reload()
	{
		this.hero.SetAnimator("Reload", 0, false, 1);
		((CWeaponControlMe)this.hero.wc).cancelFillCartridge();
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0009CFF4 File Offset: 0x0009B1F4
	public void Delay(int netID)
	{
		base.StartCoroutine(this.DelayCamera(netID));
		Debug.Log("fuhuozheID: " + this.hero.getNetid().ToString());
		this.hero.renderBianSe.InvincibleMat(true);
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0009D045 File Offset: 0x0009B245
	private IEnumerator DelayCamera(int netID)
	{
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(3031, new object[]
			{
				((NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(netID)).playerCommon.m_sName
			}));
		}
		yield return new WaitForSeconds(1.5f);
		this.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		yield return new WaitForSeconds(0.5f);
		bool flag2 = this.hero != null && !this.hero.haveDestroy;
		if (flag2)
		{
			this.hero.renderBianSe.InvincibleMat(false);
		}
		yield break;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0009D05C File Offset: 0x0009B25C
	public override void OnBlackCar(string name, ArrayList szPos)
	{
		this.longTime -= Time.deltaTime;
		bool flag = !this._hero.isDead && !this._hero.isCopy;
		if (flag)
		{
			bool flag2 = ChatUICon.inst != null;
			if (flag2)
			{
				ChatUICon.inst.ClosePushUI(false);
			}
			bool flag3 = gameuiCon.inst != null;
			if (flag3)
			{
				gameuiCon.inst.mainMsgUI.OnCarHint(true, name, this.longTime / 5f);
			}
			bool keyDown = Input.GetKeyDown(KeyCode.Y);
			if (keyDown)
			{
				this._hero.setPlayerNewPos(this.Split_Position(szPos));
				this.RollCar();
			}
			bool flag4 = Input.GetKeyDown(KeyCode.N) || this.longTime <= 0f;
			if (flag4)
			{
				this.RollCar();
			}
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0009D144 File Offset: 0x0009B344
	private void RollCar()
	{
		HeroBase.goBalckCar = false;
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.OnCarHint(false, null, 0f);
		}
		bool flag2 = ChatUICon.inst != null;
		if (flag2)
		{
			ChatUICon.inst.ClosePushUI(true);
		}
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0009D1A0 File Offset: 0x0009B3A0
	public Vector3 Split_Position(ArrayList _Value)
	{
		return new Vector3((float)int.Parse(_Value[0].ToString()) / 100f, (float)int.Parse(_Value[1].ToString()) / 100f, (float)int.Parse(_Value[2].ToString()) / 100f);
	}

	// Token: 0x04000D45 RID: 3397
	public Dictionary<string, int> skillStats = new Dictionary<string, int>();

	// Token: 0x04000D46 RID: 3398
	public bool bCanDoSkill = true;

	// Token: 0x04000D47 RID: 3399
	protected bool bAutoBreakCartridge = true;

	// Token: 0x04000D48 RID: 3400
	private bool moveToShawang;

	// Token: 0x04000D49 RID: 3401
	private float MoveTimer = 1f;

	// Token: 0x04000D4A RID: 3402
	private Vector3 MoveDir = Vector3.zero;

	// Token: 0x04000D4B RID: 3403
	private Vector3 targetPos = Vector3.zero;

	// Token: 0x04000D4C RID: 3404
	private UnityEngine.Object LinePrefeb;

	// Token: 0x04000D4D RID: 3405
	private bool NegativeWarning_Walk;

	// Token: 0x04000D4E RID: 3406
	private bool NegativeWarning_Attack;

	// Token: 0x04000D4F RID: 3407
	public bool NegativeWarning_NoFireState;

	// Token: 0x04000D50 RID: 3408
	private int playerLevel;

	// Token: 0x04000D51 RID: 3409
	public float doSkillTime;

	// Token: 0x04000D52 RID: 3410
	private float SL_MulTimer1;

	// Token: 0x04000D53 RID: 3411
	private float SL_MulTimer2;

	// Token: 0x04000D54 RID: 3412
	private float SL_MulTimer3;

	// Token: 0x04000D55 RID: 3413
	private float checkTimer = 1f;

	// Token: 0x04000D56 RID: 3414
	private int EnemyOnlineCount;

	// Token: 0x04000D57 RID: 3415
	private Dictionary<int, GamePlayer> _Enemies;

	// Token: 0x04000D58 RID: 3416
	private float _Arena_Effect_Time;

	// Token: 0x04000D59 RID: 3417
	private float _HeiAn_Hurt_Time;

	// Token: 0x04000D5A RID: 3418
	private float StretchSpeed;

	// Token: 0x04000D5B RID: 3419
	private float stretchTimer;

	// Token: 0x04000D5C RID: 3420
	private float shawangSuperTimer;

	// Token: 0x04000D5D RID: 3421
	private NetLiving shawang;

	// Token: 0x04000D5E RID: 3422
	private GameObject rope;

	// Token: 0x04000D5F RID: 3423
	private LineRenderer linerender;

	// Token: 0x04000D60 RID: 3424
	private Vector3 currentPos;
}

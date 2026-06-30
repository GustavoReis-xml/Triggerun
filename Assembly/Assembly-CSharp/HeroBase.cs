using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class HeroBase : MonoBehaviour
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06001064 RID: 4196 RVA: 0x00099174 File Offset: 0x00097374
	// (set) Token: 0x06001065 RID: 4197 RVA: 0x0009918C File Offset: 0x0009738C
	private bool showAlpha
	{
		get
		{
			return this._showAlpha;
		}
		set
		{
			this._showAlpha = value;
		}
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x00099198 File Offset: 0x00097398
	public virtual void HeroInit(NetLiving net, string heroclass)
	{
		this._hero = net;
		this.heroClassName = base.GetType().ToString();
		this.heroInvi = false;
		this._hero.heroInvi = false;
		this.B_Transforing = false;
		Type type = null;
		bool flag = heroclass != null;
		if (flag)
		{
			string typeName = "IHeroData" + heroclass;
			type = Type.GetType(typeName);
		}
		bool flag2 = type == null;
		if (flag2)
		{
			type = Type.GetType("IHeroData");
		}
		this.IHD = (IHeroData)Activator.CreateInstance(type);
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0009921E File Offset: 0x0009741E
	public virtual void InitForModelLoad()
	{
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00099224 File Offset: 0x00097424
	protected virtual void OnTriggerStay(Collider _Collider)
	{
		bool flag = _Collider.name.Contains("_jidi_");
		if (flag)
		{
			this.InJiDi(_Collider);
		}
		this.CheckWaterIn(_Collider);
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00099258 File Offset: 0x00097458
	private void InJiDi(Collider _Collider)
	{
		bool flag = this._hero.netobject_type == 6;
		if (flag)
		{
			bool flag2 = ((RobotAI)this._hero).SyncNetObj == null || ((RobotAI)this._hero).SyncNetObj.getNetid() != fGameManager.Heroplayer.getNetid();
			if (flag2)
			{
				return;
			}
		}
		else
		{
			bool flag3 = !this.beMeClass;
			if (flag3)
			{
				return;
			}
		}
		int num = 0;
		bool flag4 = this._hero.InJiDi == 0;
		if (flag4)
		{
			jidiitemUI component = _Collider.gameObject.GetComponent<jidiitemUI>();
			bool flag5 = component != null;
			if (flag5)
			{
				bool jiDiJieDuanStat = component.GetJiDiJieDuanStat();
				if (jiDiJieDuanStat)
				{
					return;
				}
				num = component.pBuffitem.buffinfo.m_nBuffForce;
			}
			bool flag6 = num != this._hero.nForce;
			if (flag6)
			{
				return;
			}
			this._hero.InJiDi = 1;
			this._hero.SendMsgToServer(51006, new ArrayList
			{
				this._hero.getNetid(),
				1,
				component.pBuffitem.getNetid()
			});
		}
		else
		{
			jidiitemUI component2 = _Collider.gameObject.GetComponent<jidiitemUI>();
			bool flag7 = component2 != null && component2.GetJiDiJieDuanStat();
			if (flag7)
			{
				return;
			}
		}
		this._hero.jiDiTimeOut = 0.3f + Time.realtimeSinceStartup;
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000993F0 File Offset: 0x000975F0
	private void JiDiCheck()
	{
		bool flag = this._hero.InJiDi != 0 && Time.realtimeSinceStartup >= this._hero.jiDiTimeOut;
		if (flag)
		{
			this._hero.InJiDi = 0;
			this._hero.SendMsgToServer(51006, new ArrayList
			{
				this._hero.getNetid(),
				0,
				0
			});
		}
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0009947C File Offset: 0x0009767C
	private void CheckWaterIn(Collider _Collider)
	{
		bool flag = Time.time >= this.waterInTime;
		if (flag)
		{
			this.waterInTime = Time.time + 0.5f;
			this._hero.playerCommon.inWater = false;
			bool flag2 = _Collider.CompareTag("water");
			if (flag2)
			{
				this._hero.playerCommon.inWater = true;
				this.waterTimeOut = Time.time + 1f;
			}
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x000994F8 File Offset: 0x000976F8
	private void CheckWaterOut()
	{
		bool flag = this._hero.playerCommon.inWater && Time.time > this.waterTimeOut;
		if (flag)
		{
			this._hero.playerCommon.inWater = false;
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x0009953F File Offset: 0x0009773F
	public virtual void SetFilmMaterial(bool show)
	{
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x00099542 File Offset: 0x00097742
	public virtual void ChangeWeaponGroup(int xingtai)
	{
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00099548 File Offset: 0x00097748
	public virtual void SuperSkillDo(object parm)
	{
		bool flag = !(this._hero.wc == null);
		if (flag)
		{
			this._hero.wc.lastFirePublicTime = 0f;
		}
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00099586 File Offset: 0x00097786
	public virtual void SuperSkillFinish()
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x00099589 File Offset: 0x00097789
	public virtual void OnDeath()
	{
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0009958C File Offset: 0x0009778C
	public virtual void Revive()
	{
		this._UnbreakableKillCount = 0;
		User_Data_Handle.Watch_Random = -1;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0009959C File Offset: 0x0009779C
	public virtual void Freeze(bool m_bool)
	{
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0009959F File Offset: 0x0009779F
	public virtual void Stun(bool m_bool)
	{
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000995A4 File Offset: 0x000977A4
	public virtual bool BulletCanFill()
	{
		return true;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x000995B7 File Offset: 0x000977B7
	public virtual void BulletFillBegin()
	{
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x000995BA File Offset: 0x000977BA
	public virtual void BulletFillBreak()
	{
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x000995BD File Offset: 0x000977BD
	public virtual void BulletFilled()
	{
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000995C0 File Offset: 0x000977C0
	public virtual void WeaponFired(bool fired)
	{
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000995C3 File Offset: 0x000977C3
	public virtual void bulletBoom(int _weaponName, int _weaponNo, Vector3 pos)
	{
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x000995C8 File Offset: 0x000977C8
	protected virtual void Update()
	{
		this.checkAlpha();
		this.CheckWaterOut();
		this.JiDiCheck();
		bool kill = this.Kill;
		if (kill)
		{
			this.MultiKillTimer -= Time.deltaTime;
			bool flag = this.MultiKillTimer <= 0f;
			if (flag)
			{
				this.MultiKillCount = 0;
				this.Kill = false;
			}
		}
		bool flag2 = HeroBase.goBalckCar;
		if (flag2)
		{
			this.OnBlackCar(this.lsjName, this.HzPos);
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0009964B File Offset: 0x0009784B
	public virtual void beDamage(ArrayList szDamage, ArrayList szHp)
	{
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00099650 File Offset: 0x00097850
	public virtual void ChangeYinShenState()
	{
		this.heroInvi = true;
		this._hero.heroInvi = true;
		bool activeSelf = this._hero._playerEffectContainer.activeSelf;
		if (activeSelf)
		{
			this._hero._playerEffectContainer.SetActive(false);
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0009969C File Offset: 0x0009789C
	public virtual void changeBackToNormalState()
	{
		this.heroInvi = false;
		this._hero.heroInvi = false;
		bool flag = !this._hero._playerEffectContainer.activeSelf;
		if (flag)
		{
			this._hero._playerEffectContainer.SetActive(true);
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000996EC File Offset: 0x000978EC
	public int MultiKill()
	{
		bool flag = this.MultiKillCount == 5;
		if (flag)
		{
			this.MultiKillCount = 0;
		}
		this.MultiKillCount++;
		this.MultiKillTimer = 4f;
		this.Kill = true;
		return this.MultiKillCount;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0009973C File Offset: 0x0009793C
	public int UnBreakableKillCount()
	{
		this._UnbreakableKillCount++;
		return this._UnbreakableKillCount;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00099764 File Offset: 0x00097964
	public bool IsHeroInvi()
	{
		return this.heroInvi;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0009977C File Offset: 0x0009797C
	public virtual void NpcKill(Npc_Entity npc)
	{
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0009977F File Offset: 0x0009797F
	public virtual void BreakSupperSkill()
	{
		this.SuperSkillFinish();
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0009978C File Offset: 0x0009798C
	public virtual bool CanHitReaction()
	{
		bool flag = this._hero.playerCommon.m_nDongjie == 1;
		return !flag;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x000997BC File Offset: 0x000979BC
	public virtual void HitByShawangSuper(string state, int attackId, ArrayList hitIds)
	{
		foreach (object obj in hitIds)
		{
			int num = (int)obj;
			bool flag = num == attackId;
			if (!flag)
			{
				NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
				bool flag2 = netLiving == null;
				if (!flag2)
				{
					bool flag3 = state == "start";
					if (flag3)
					{
						bool bePlayer = netLiving.bePlayer;
						if (bePlayer)
						{
							netLiving.tChar.m_MoveByAnimator = false;
							netLiving.charMove.notMove = true;
						}
					}
					else
					{
						bool bePlayer2 = netLiving.bePlayer;
						if (bePlayer2)
						{
							netLiving.tChar.m_MoveByAnimator = true;
							netLiving.charMove.notMove = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000998AC File Offset: 0x00097AAC
	public virtual void GetHeroNpc(object obj)
	{
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x000998AF File Offset: 0x00097AAF
	public virtual void alphaEnd()
	{
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x000998B4 File Offset: 0x00097AB4
	private void checkAlpha()
	{
		bool flag = this.checkTime > Time.time || this._hero.renderBianSe == null;
		if (!flag)
		{
			this.checkTime = Time.time + 0.5f;
			NetLiving netLiving = (!this._hero.isCopy) ? fGameManager.Heroplayer : Fun.Get_NetLiving(CCameraChange.Instance.filmTargetParent);
			bool flag2 = netLiving == null;
			if (!flag2)
			{
				bool flag3 = !this.showAlpha;
				if (flag3)
				{
					bool flag4 = netLiving.playerCommon.m_dtToushi > Fun.Now;
					if (flag4)
					{
						this.showAlpha = true;
						bool flag5 = gameuiCon.inst != null && this._hero == netLiving;
						if (flag5)
						{
							gameuiCon.inst.mainMsgUI.OnDaZhaoFriendName(this._hero.playerCommon.m_nTouShiAddId);
						}
						bool flag6 = netLiving.nForce == this._hero.nForce;
						if (flag6)
						{
							this._hero.renderBianSe.ChangeMateial(true);
						}
						else
						{
							this._hero.renderBianSe.ChangeMateial(false);
						}
						bool flag7 = !this.showAlpha;
						if (flag7)
						{
							CCheckDelay.Instance._tsBool = true;
						}
					}
				}
				else
				{
					bool flag8 = netLiving.playerCommon.m_dtToushi < Fun.Now;
					if (flag8)
					{
						this.showAlpha = false;
						this._hero.renderBianSe.ChangeMateial(true);
						this.alphaEnd();
					}
				}
			}
		}
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00099A4A File Offset: 0x00097C4A
	public virtual void HideLinghunqiuObj()
	{
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00099A4D File Offset: 0x00097C4D
	public virtual void OnNetDestroy()
	{
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00099A50 File Offset: 0x00097C50
	public virtual GameObject GetShield(string shieldName)
	{
		return null;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00099A63 File Offset: 0x00097C63
	public virtual void RoundBegin()
	{
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00099A66 File Offset: 0x00097C66
	public virtual void SetRobotState(bool attackState)
	{
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x00099A69 File Offset: 0x00097C69
	public virtual void RobotCreateFinish()
	{
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x00099A6C File Offset: 0x00097C6C
	public virtual void SetChenMo(bool m_bool)
	{
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x00099A70 File Offset: 0x00097C70
	public void CloseCar(int id, ArrayList szPos, float lTime)
	{
		this.HzPos = szPos;
		this.longTime = lTime;
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(id);
		this.lsjName = netLiving.playerCommon.m_sName;
		HeroBase.goBalckCar = true;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00099AB9 File Offset: 0x00097CB9
	public virtual void OnBlackCar(string name, ArrayList szPos)
	{
	}

	// Token: 0x04000D1B RID: 3355
	protected bool beMeClass;

	// Token: 0x04000D1C RID: 3356
	protected NetLiving _hero;

	// Token: 0x04000D1D RID: 3357
	private bool heroInvi;

	// Token: 0x04000D1E RID: 3358
	private bool Kill;

	// Token: 0x04000D1F RID: 3359
	private float MultiKillTimer;

	// Token: 0x04000D20 RID: 3360
	private int MultiKillCount;

	// Token: 0x04000D21 RID: 3361
	private int _UnbreakableKillCount;

	// Token: 0x04000D22 RID: 3362
	protected bool autoDisableReadyitem = true;

	// Token: 0x04000D23 RID: 3363
	public Dictionary<int, CSpecialSkillStruct> playerSpecialSkills = new Dictionary<int, CSpecialSkillStruct>();

	// Token: 0x04000D24 RID: 3364
	public string heroClassName = string.Empty;

	// Token: 0x04000D25 RID: 3365
	public IHeroData IHD;

	// Token: 0x04000D26 RID: 3366
	public bool Release_Musth_State;

	// Token: 0x04000D27 RID: 3367
	public bool B_Transforing;

	// Token: 0x04000D28 RID: 3368
	private float waterInTime;

	// Token: 0x04000D29 RID: 3369
	private float waterTimeOut;

	// Token: 0x04000D2A RID: 3370
	private float checkTime;

	// Token: 0x04000D2B RID: 3371
	private bool _showAlpha;

	// Token: 0x04000D2C RID: 3372
	public static bool goBalckCar;

	// Token: 0x04000D2D RID: 3373
	private ArrayList HzPos = new ArrayList();

	// Token: 0x04000D2E RID: 3374
	private string lsjName;

	// Token: 0x04000D2F RID: 3375
	public float longTime;
}

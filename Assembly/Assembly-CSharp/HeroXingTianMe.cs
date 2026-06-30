using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class HeroXingTianMe : HeroBaseMe
{
	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06001505 RID: 5381 RVA: 0x000C7228 File Offset: 0x000C5428
	// (set) Token: 0x06001506 RID: 5382 RVA: 0x000C7240 File Offset: 0x000C5440
	public bool Shield_Call_State
	{
		get
		{
			return this._Shield_Call_State;
		}
		set
		{
			this._Shield_Call_State = value;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06001507 RID: 5383 RVA: 0x000C724C File Offset: 0x000C544C
	// (set) Token: 0x06001508 RID: 5384 RVA: 0x000C7287 File Offset: 0x000C5487
	public Animation Shield_Object_Animation_Component
	{
		get
		{
			bool flag = this._Shield_Object_Animation_Component == null;
			if (flag)
			{
				this._Shield_Object_Animation_Component = this.Shield_Object.GetComponent<Animation>();
			}
			return this._Shield_Object_Animation_Component;
		}
		set
		{
			this._Shield_Object_Animation_Component = value;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06001509 RID: 5385 RVA: 0x000C7294 File Offset: 0x000C5494
	// (set) Token: 0x0600150A RID: 5386 RVA: 0x000C7512 File Offset: 0x000C5712
	public GameObject Shield_Object
	{
		get
		{
			bool flag = this._Shield_Object == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.gameObject, "body2_dun");
				Transform transform2 = Fun.Get_Child_Transform(base.gameObject, "shieldbody");
				this.huzhao = Fun.Get_Child_Transform(base.gameObject, "huzhao");
				this.huzhaoPo = Fun.Get_Child_Transform(base.gameObject, "huzhaopo");
				this.huzhaoPo1 = Fun.Get_Child_Transform(base.gameObject, "huzhaopo1");
				this.dun = Fun.Get_Child_Transform(base.gameObject, "dun");
				this.Aim = Fun.Get_Child_Transform(base.gameObject, "Aim");
				this.tempBodyTrans = new GameObject();
				this.tempBodyTrans.name = "tempBodyTrans";
				bool flag2 = transform != null;
				if (flag2)
				{
					Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
					bool flag3 = rigidbody == null;
					if (flag3)
					{
						rigidbody = transform.gameObject.AddComponent<Rigidbody>();
					}
					rigidbody.isKinematic = true;
					rigidbody.useGravity = true;
					rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
					transform.gameObject.tag = "can_damage_dun";
					transform.gameObject.layer = LayerMask.NameToLayer("shield" + base.hero.nForce.ToString());
					this.tempCol = transform.GetComponent<Collider>();
					this.tempCol.isTrigger = false;
					this.tempCol.enabled = false;
					this.tempBodyTrans.transform.parent = transform2.transform.parent;
					this.tempBodyTrans.transform.localPosition = transform2.transform.localPosition;
					this.tempBodyTrans.transform.localRotation = transform2.transform.localRotation;
					this.tempBodyTrans.transform.parent = this.dun.transform;
					this.tempBodyTrans.transform.rotation = Quaternion.AngleAxis(-22f, this.tempBodyTrans.transform.right) * this.tempBodyTrans.transform.rotation;
					transform.transform.parent = base.hero.UserControl.transform.parent;
					this._Shield_Object = transform.gameObject;
					this.shieldRig = rigidbody;
				}
			}
			return this._Shield_Object;
		}
		set
		{
			this._Shield_Object = value;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x0600150B RID: 5387 RVA: 0x000C751C File Offset: 0x000C571C
	// (set) Token: 0x0600150C RID: 5388 RVA: 0x000C756E File Offset: 0x000C576E
	public GameObject Shield_Break_Object
	{
		get
		{
			bool flag = this._Shield_Break_Object == null;
			if (flag)
			{
				this._Shield_Break_Object = Fun.Get_Child_Transform(base.gameObject, "huzhaosui").gameObject;
				this._Shield_Break_Object.SetActive(false);
			}
			return this._Shield_Break_Object;
		}
		set
		{
			this._Shield_Break_Object = value;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x0600150D RID: 5389 RVA: 0x000C7578 File Offset: 0x000C5778
	// (set) Token: 0x0600150E RID: 5390 RVA: 0x000C7590 File Offset: 0x000C5790
	public HeroXingTianMe.State_Describe Shield_State
	{
		get
		{
			return this._Shield_State;
		}
		set
		{
			this._Shield_State = value;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x0600150F RID: 5391 RVA: 0x000C759C File Offset: 0x000C579C
	// (set) Token: 0x06001510 RID: 5392 RVA: 0x000C75B4 File Offset: 0x000C57B4
	public HeroXingTianMe.Break_State Shield_Break_State
	{
		get
		{
			return this._Shield_Break_State;
		}
		set
		{
			this._Shield_Break_State = value;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06001511 RID: 5393 RVA: 0x000C75C0 File Offset: 0x000C57C0
	// (set) Token: 0x06001512 RID: 5394 RVA: 0x000C75D8 File Offset: 0x000C57D8
	public bool Energy_Recover_State
	{
		get
		{
			return this._Energy_Recover_State;
		}
		set
		{
			this._Energy_Recover_State = value;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06001513 RID: 5395 RVA: 0x000C75E4 File Offset: 0x000C57E4
	// (set) Token: 0x06001514 RID: 5396 RVA: 0x000C75FC File Offset: 0x000C57FC
	public int Shield_Current_Energy
	{
		get
		{
			return this._Shield_Current_Energy;
		}
		set
		{
			this._Shield_Current_Energy = value;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06001515 RID: 5397 RVA: 0x000C7608 File Offset: 0x000C5808
	// (set) Token: 0x06001516 RID: 5398 RVA: 0x000C7620 File Offset: 0x000C5820
	public int Shield_Max_Energy
	{
		get
		{
			return this._Shield_Max_Energy;
		}
		set
		{
			this._Shield_Max_Energy = value;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06001517 RID: 5399 RVA: 0x000C762C File Offset: 0x000C582C
	// (set) Token: 0x06001518 RID: 5400 RVA: 0x000C7644 File Offset: 0x000C5844
	public float Shield_Call_Time
	{
		get
		{
			return this._Shield_Call_Time;
		}
		set
		{
			this._Shield_Call_Time = value;
		}
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000C7650 File Offset: 0x000C5850
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		base.hero.playerSound.LoadShieldContinue();
		base.hero.playerSound.LoadShieldBreak();
		base.hero.playerSound.LoadShieldOpen();
		base.hero.playerSound.LoadShieldClose();
		base.hero.playerSound.LoadShieldAboutCrash();
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000C76BC File Offset: 0x000C58BC
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.BreakSth(1);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.wInput.notInput = true;
		base.hero.wInput.notShoot = true;
		base.StartCoroutine(this.DelayReleaseSuper());
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000C773C File Offset: 0x000C593C
	private IEnumerator DelayReleaseSuper()
	{
		yield return new WaitForSeconds(0.28f);
		while (!base.hero.tChar.onGround && !base.hero.isDead)
		{
			yield return new WaitForSeconds(0.02f);
		}
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		bool flag = !base.hero.isDead;
		if (flag)
		{
			base.hero.SetAnimator("SuperAction", 0, true, 1);
		}
		yield return new WaitForSeconds(0.05f);
		base.hero.playerDazhao.doShoot(base.hero);
		base.hero.bePlayActive = false;
		yield return new WaitForSeconds(0.45f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000C774C File Offset: 0x000C594C
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		base.hero.wInput.notShoot = false;
		base.hero.wInput.notInput = false;
		base.hero.bePlayActive = true;
		bool flag = base.hero.playerCommon.m_nHunMi == 1;
		if (flag)
		{
			base.hero.bePlayActive = false;
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000C77B8 File Offset: 0x000C59B8
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput && !Input_Manager.Get_Button("Skill2");
		if (flag)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000C7818 File Offset: 0x000C5A18
	private void State_Handle()
	{
		bool flag = this._Shield_State == HeroXingTianMe.State_Describe.Wait;
		if (flag)
		{
			this.State_Handle(-1);
		}
		else
		{
			bool flag2 = this.Shield_State == HeroXingTianMe.State_Describe.CallOff;
			if (flag2)
			{
				bool shield_Call_State = this.Shield_Call_State;
				if (shield_Call_State)
				{
					this.State_Handle(1);
				}
			}
			else
			{
				bool flag3 = this.Shield_State == HeroXingTianMe.State_Describe.CallOn && !this.Shield_Call_State;
				if (flag3)
				{
					this.State_Handle(0);
				}
			}
		}
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000C7888 File Offset: 0x000C5A88
	private void State_Handle(int _Shield_State)
	{
		switch (_Shield_State)
		{
		case -1:
			base.hero.wInput.notInput = false;
			base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
			base.hero.SetAnimator("JvDun", 0, false, 2);
			break;
		case 0:
			this.Shield_State = HeroXingTianMe.State_Describe.CallOff;
			base.hero.SetAnimator("JvDun", 0, false, 2);
			base.hero.wInput.notShoot = false;
			base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
			break;
		case 1:
		{
			bool flag = this.Shield_Current_Energy >= 1;
			if (flag)
			{
				bool flag2 = this.Shield_Call_Time == 0f || Time.time >= this.Shield_Call_Time + 30f;
				if (flag2)
				{
					this.Shield_Call_Time = Time.time;
				}
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
				base.hero.wInput.notShoot = true;
				this.Shield_State = HeroXingTianMe.State_Describe.CallOn;
				base.hero.SetAnimator("JvDun", 0, true, 2);
			}
			break;
		}
		}
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000C79E0 File Offset: 0x000C5BE0
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
			base.hero.renderBianSe.makeHongXianDisable();
			base.hero.aimSystem.b_closeDisCheck = true;
		}
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000C7A2C File Offset: 0x000C5C2C
	public override void BreakSth(int type)
	{
		base.BreakSth(type);
		this._Shield_State = HeroXingTianMe.State_Describe.Wait;
		this.State_Handle();
		bool flag = this.skillShield == null && base.hero.skillEffectDict.ContainsKey(1021);
		if (flag)
		{
			this.skillShield = (SkillShieldMe)base.hero.skillEffectDict[1021];
		}
		bool flag2 = this.skillShield != null && base.hero.playerCommon.m_nDongjie == 0 && base.hero.playerCommon.m_nHunMi == 0;
		if (flag2)
		{
			this.skillShield.SkillBreakShield(1.5f);
		}
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000C7AE9 File Offset: 0x000C5CE9
	public IEnumerator Shield_Break_Effect()
	{
		GameObject _Temp = null;
		_Temp = UnityEngine.Object.Instantiate<GameObject>(this.Shield_Break_Object);
		_Temp.transform.parent = base.gameObject.transform;
		_Temp.transform.localPosition = Vector3.zero;
		_Temp.transform.localEulerAngles = Vector3.zero;
		_Temp.transform.localScale = Vector3.one;
		_Temp.SetActive(true);
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(_Temp);
		yield break;
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000C7AF8 File Offset: 0x000C5CF8
	public override void OnDeath()
	{
		base.hero.SetAnimator("SuperAction", 0, false, 1);
		base.OnDeath();
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
		this.SuperSkillFinish();
		base.hero.SetAnimator("JvDun", 0, false, 2);
		base.hero.SetAnimator("Revive", 0, true, 1);
		bool activeSelf = this.huzhao.gameObject.activeSelf;
		if (activeSelf)
		{
			this.huzhao.gameObject.SetActive(false);
		}
		bool activeSelf2 = this.huzhaoPo.gameObject.activeSelf;
		if (activeSelf2)
		{
			this.huzhaoPo.gameObject.SetActive(false);
		}
		bool activeSelf3 = this.huzhaoPo1.gameObject.activeSelf;
		if (activeSelf3)
		{
			this.huzhaoPo1.gameObject.SetActive(false);
		}
		((ISkillDataSkillShield)this.skillShield.ISD).Shield_Call_State = false;
		base.hero.Clear_Adhere_Npc("body2_dun");
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000C7C3C File Offset: 0x000C5E3C
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		base.hero.SetAnimator("Stun", 0, m_bool, 1);
		if (m_bool)
		{
			base.StopCoroutine(this.DelayReleaseSuper());
			this.skillShield.SkillBreakShield(2.5f);
		}
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x000C7C90 File Offset: 0x000C5E90
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
		if (m_bool)
		{
			this.skillShield.SkillBreakShield(2.5f);
		}
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000C7CC0 File Offset: 0x000C5EC0
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("SuperAction", 0, false, 1);
		base.hero.SetAnimator("JvDun", 0, false, 2);
		base.hero.SetAnimator("Revive", 0, false, 1);
		bool flag = !(this.Shield_Object == null) && this.skillShield != null;
		if (flag)
		{
			this.skillShield.SkillBreakShield(0.1f);
			this.skillShield.reviveTimer = 0.5f;
		}
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000C7D64 File Offset: 0x000C5F64
	private void FixedUpdate()
	{
		bool flag = base.hero == null || base.hero.tCharAnimationTps == null || this.Shield_Object == null;
		if (!flag)
		{
			bool flag2 = this.skillShield == null;
			if (flag2)
			{
				bool flag3 = base.hero.skillEffectDict.ContainsKey(1021);
				if (flag3)
				{
					this.skillShield = (SkillShieldMe)base.hero.skillEffectDict[1021];
				}
			}
			else
			{
				bool shield_Call_State = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Call_State;
				if (shield_Call_State)
				{
					CameraControl.Instance.yMaxLimit = 20;
					this.dun.transform.LookAt(this.Aim.position + this.Aim.forward * 20f);
					this.shieldRig.velocity = Vector3.zero;
					this.shieldRig.angularVelocity = Vector3.zero;
					this.shieldRig.MovePosition(this.tempBodyTrans.transform.position);
					this.shieldRig.MoveRotation(this.tempBodyTrans.transform.rotation);
					this.setCameraScopeLimit = true;
				}
				else
				{
					bool flag4 = this.setCameraScopeLimit;
					if (flag4)
					{
						CameraControl.Instance.yMaxLimit = 85;
						this.setCameraScopeLimit = false;
					}
				}
			}
		}
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000C7EDC File Offset: 0x000C60DC
	public override bool CanHitReaction()
	{
		bool shield_Call_State = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Call_State;
		return !shield_Call_State && base.CanHitReaction();
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000C7F14 File Offset: 0x000C6114
	public override GameObject GetShield(string shieldName)
	{
		bool flag = this.tempCol != null;
		GameObject result;
		if (flag)
		{
			result = this.tempCol.gameObject;
		}
		else
		{
			result = base.GetShield(shieldName);
		}
		return result;
	}

	// Token: 0x04001049 RID: 4169
	private bool _Shield_Call_State;

	// Token: 0x0400104A RID: 4170
	private Animation _Shield_Object_Animation_Component;

	// Token: 0x0400104B RID: 4171
	private Rigidbody shieldRig;

	// Token: 0x0400104C RID: 4172
	private GameObject _Shield_Object;

	// Token: 0x0400104D RID: 4173
	private GameObject tempBodyTrans;

	// Token: 0x0400104E RID: 4174
	private Transform huzhao;

	// Token: 0x0400104F RID: 4175
	private Transform huzhaoPo;

	// Token: 0x04001050 RID: 4176
	private Transform huzhaoPo1;

	// Token: 0x04001051 RID: 4177
	private Transform dun;

	// Token: 0x04001052 RID: 4178
	private Transform Aim;

	// Token: 0x04001053 RID: 4179
	private Collider tempCol;

	// Token: 0x04001054 RID: 4180
	private GameObject _Shield_Break_Object;

	// Token: 0x04001055 RID: 4181
	private HeroXingTianMe.State_Describe _Shield_State;

	// Token: 0x04001056 RID: 4182
	private HeroXingTianMe.Break_State _Shield_Break_State = HeroXingTianMe.Break_State.State_1;

	// Token: 0x04001057 RID: 4183
	private bool _Energy_Recover_State = true;

	// Token: 0x04001058 RID: 4184
	private int _Shield_Current_Energy;

	// Token: 0x04001059 RID: 4185
	private int _Shield_Max_Energy = 2000;

	// Token: 0x0400105A RID: 4186
	private float _Shield_Call_Time;

	// Token: 0x0400105B RID: 4187
	private SkillShieldMe skillShield;

	// Token: 0x0400105C RID: 4188
	private bool setCameraScopeLimit;

	// Token: 0x0400105D RID: 4189
	private Quaternion lastRotation;

	// Token: 0x020005CD RID: 1485
	public enum State_Describe
	{
		// Token: 0x04002E3F RID: 11839
		Wait = -1,
		// Token: 0x04002E40 RID: 11840
		CallOff,
		// Token: 0x04002E41 RID: 11841
		CallOn
	}

	// Token: 0x020005CE RID: 1486
	public enum Break_State
	{
		// Token: 0x04002E43 RID: 11843
		State_1 = 1,
		// Token: 0x04002E44 RID: 11844
		State_2,
		// Token: 0x04002E45 RID: 11845
		State_3,
		// Token: 0x04002E46 RID: 11846
		State_4,
		// Token: 0x04002E47 RID: 11847
		State_5,
		// Token: 0x04002E48 RID: 11848
		Break
	}
}

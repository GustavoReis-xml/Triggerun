using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class HeroHouYiMe : HeroBaseMe
{
	// Token: 0x060011EB RID: 4587 RVA: 0x000A808C File Offset: 0x000A628C
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.FireFinishTimer = 0f;
		this.playerState = NetMsg.PlayerState.daiji;
		base.hero.SetAnimator("PlayerState", 2, 3, 1);
		this.BowStayTimer = 0f;
		((TBCharacterThirdMe)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdMe)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		((TBCharacterThirdMe)base.hero.tChar).wallRunWeightSpeed = 5f;
		base.hero.tChar.wallRunCan = true;
		this.FightTimer = 1f;
		this.b_fight = true;
		this.onwall = false;
		this.checkNeedPlayClimAnim = false;
		this.playPaqiang = false;
		this.OnAirFashe = false;
		base.hero.playerSound.LoadSwitchArrowSound();
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x000A8178 File Offset: 0x000A6378
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		((CWeaponControlMe)base.hero.wc).fullBreakCancel();
		((TBCharacterThirdMe)base.hero.tChar).canWall = false;
		base.hero.SetAnimator("fireforce", 1, 0f, 1);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.StartCoroutine(this.delaySuperSkillFinish());
		base.hero.SetAnimator("SkillBreakFight", 0, true, 1);
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000A8228 File Offset: 0x000A6428
	private IEnumerator delaySuperSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		base.hero.playerDazhao.doShoot(base.hero);
		yield return new WaitForSeconds(0.5f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000A8238 File Offset: 0x000A6438
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		((TBCharacterThirdMe)base.hero.tChar).canWall = true;
		base.hero.wInput.notInput = false;
		bool flag = base.hero.playerCommon.m_nHunMi == 1;
		if (flag)
		{
			base.hero.bePlayActive = false;
		}
		base.hero.SetAnimator("SkillBreakFight", 0, false, 1);
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000A82B4 File Offset: 0x000A64B4
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		if (m_bool)
		{
			base.StopCoroutine(this.delaySuperSkillFinish());
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000A82DE File Offset: 0x000A64DE
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
		this.fire = fired;
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000A82F0 File Offset: 0x000A64F0
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.SetAnimator("fireforce", 1, 0f, 1);
		base.hero.tChar.wallState = TBCharacter.WallState.OnGround;
		this.fireforce = 0f;
		this.onwall = false;
		base.hero.SetAnimator("Fire", 0, false, 1);
		this.playerState = NetMsg.PlayerState.daiji;
		base.hero.SetAnimator("PlayerState", 2, 3, 1);
		base.hero.charMove.state.onwall = 0;
		this.StopClimbAnim();
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		base.hero.SetAnimator("PaQiangEnd", 0, false, 1);
		bool flag = base.hero.tCharAnimationFps != null;
		if (flag)
		{
			((TBCharAnimationHouYiFps)base.hero.tCharAnimationFps).houyiSetFireforce(false, 0);
		}
		bool flag2 = base.hero.tCharAnimationTps != null;
		if (flag2)
		{
			((TBCharAnimationHouYiTps)base.hero.tCharAnimationTps).houyiSetFireforce();
		}
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000A842C File Offset: 0x000A662C
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, true, 1);
		base.hero.SetAnimator("Revive", 0, true, 1);
		base.hero.SetAnimator("fireforce", 1, 0f, 1);
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		base.hero.SetAnimator("PaQiangEnd", 0, false, 1);
		this.onwall = false;
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
			((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo = 0;
			base.hero.SetAnimator("SkillJiTui", 0, false, 1);
			base.hero.SetAnimator("SkillBaoLie", 0, false, 1);
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000A8558 File Offset: 0x000A6758
	private void XuliState()
	{
		bool flag = base.hero.playerCommon.m_nHunMi == 1;
		if (!flag)
		{
			bool flag2 = this.houyiFps != null;
			if (flag2)
			{
				this.houyiFps.fulling = this.cw_me.fullIng;
			}
			bool flag3 = !this.cw_me.fullIng;
			if (!flag3)
			{
				bool flag4 = this.playerState == NetMsg.PlayerState.daiji;
				if (flag4)
				{
					this.playerState = NetMsg.PlayerState.xuli;
					bool flag5 = this.onAirTimer > 0f;
					if (flag5)
					{
						this.fireforce = this.onAirTimer + 0.1f;
					}
					this.fireFullTimer = 0f;
				}
				bool flag6 = this.playerState != NetMsg.PlayerState.xuli || this.onWall;
				if (!flag6)
				{
					bool bePlayActive = base.hero.bePlayActive;
					if (bePlayActive)
					{
						base.hero.bePlayActive = false;
					}
					bool flag7 = this.fireforce <= 1f;
					if (flag7)
					{
						this.fireforce += Time.deltaTime;
						bool flag8 = this.fireforce > 1f;
						if (flag8)
						{
							this.fireFullTimer += Time.deltaTime;
							this.fireforce = 1f;
						}
					}
				}
			}
		}
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000A86A4 File Offset: 0x000A68A4
	private void faSheState()
	{
		bool flag = !base.hero.bePlayActive && base.hero.playerCommon.m_nHunMi == 0;
		if (flag)
		{
			base.hero.bePlayActive = true;
		}
		bool flag2 = this.playerState == NetMsg.PlayerState.daiji;
		if (flag2)
		{
			this.playerState = NetMsg.PlayerState.xuli;
		}
		bool flag3 = this.playerState == NetMsg.PlayerState.xuli;
		if (flag3)
		{
			bool flag4 = this.fireforce < 0.9f;
			if (flag4)
			{
				this.BowStayTimer = 0.2f;
			}
			else
			{
				this.BowStayTimer = 0.3f;
			}
			this.fireTimer = 0f;
			this.playerState = NetMsg.PlayerState.fashe;
			base.hero.SetAnimator("PlayerState", 2, 2, 1);
		}
		this.FireFinishTimer = 0f;
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x000A8770 File Offset: 0x000A6970
	private void daiJiState()
	{
		this.BowStayTimer -= Time.deltaTime;
		bool flag = !base.hero.tChar.onGround;
		if (flag)
		{
			this.onAirTimer += Time.deltaTime;
		}
		else
		{
			bool onGround = base.hero.tChar.onGround;
			if (onGround)
			{
				this.onAirTimer = 0f;
			}
		}
		bool flag2 = this.playerState == NetMsg.PlayerState.fashe;
		if (flag2)
		{
			bool flag3 = this.BowStayTimer >= 0f;
			if (flag3)
			{
				this.fireforce += Time.deltaTime * 3f;
				bool flag4 = this.fireforce > 1f;
				if (flag4)
				{
					this.fireforce = 1f;
				}
			}
			else
			{
				bool flag5 = this.fireforce > 0f;
				if (flag5)
				{
					this.fireTimer += Time.deltaTime * 1.2f;
					this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
					bool flag6 = this.fireforce < 0f;
					if (flag6)
					{
						this.fireforce = 0f;
					}
				}
			}
			this.FireFinishTimer += Time.deltaTime;
			bool flag7 = this.FireFinishTimer > 0.4f;
			if (flag7)
			{
				bool flag8 = this.onAirTimer > 0.15f && !this.OnAirFashe;
				if (flag8)
				{
					base.hero.SetAnimator("OnAirFaShe", 0, true, 1);
					this.OnAirFashe = true;
				}
				else
				{
					bool onAirFashe = this.OnAirFashe;
					if (onAirFashe)
					{
						base.hero.SetAnimator("OnAirFaShe", 0, false, 1);
						this.OnAirFashe = false;
					}
				}
				base.hero.SetAnimator("BreakFire", 0, true, 1);
				this.playerState = NetMsg.PlayerState.daiji;
				base.hero.SetAnimator("PlayerState", 2, 3, 1);
				this.FireFinishTimer = 0f;
			}
		}
		else
		{
			bool flag9 = this.playerState != NetMsg.PlayerState.daiji;
			if (!flag9)
			{
				bool flag10 = base.hero.tChar.onGround && this.OnAirFashe;
				if (flag10)
				{
					base.hero.SetAnimator("OnAirFaShe", 0, false, 1);
					this.OnAirFashe = false;
				}
				bool flag11 = this.BowStayTimer >= 0f;
				if (flag11)
				{
					this.fireforce += Time.deltaTime * 5f;
					bool flag12 = this.fireforce > 1f;
					if (flag12)
					{
						this.fireforce = 1f;
					}
				}
				else
				{
					bool flag13 = this.fireforce > 0f;
					if (flag13)
					{
						this.fireTimer += Time.deltaTime * 1.2f;
						this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
						bool flag14 = this.fireforce < 0f;
						if (flag14)
						{
							this.fireforce = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000A8AA8 File Offset: 0x000A6CA8
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero == null || base.hero.wInput == null || base.hero.tCharAnimationTps == null || base.hero.tCharAnimationFps == null;
		if (!flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("WeaponFirst") && Input_Manager.Get_Button_Down("WeaponSec");
			if (flag2)
			{
				base.hero.SetAnimator("fireforce", 1, 0f, 1);
				this.fireforce = 0f;
				base.hero.SetAnimator("Fire", 0, false, 1);
				this.playerState = NetMsg.PlayerState.daiji;
				base.hero.SetAnimator("PlayerState", 2, 3, 1);
				this.keyDownAtSameTime = true;
			}
			else
			{
				bool flag3 = Input_Manager.Get_Button_Up("WeaponFirst") || Input_Manager.Get_Button_Up("WeaponSec");
				if (flag3)
				{
					this.keyDownAtSameTime = false;
				}
				bool flag4 = Input_Manager.Get_Button_Up("WeaponFirst") && !this.weaponFirstUp;
				if (flag4)
				{
					this.weaponFirstUp = true;
					((TBCharAnimationHouYiFps)base.hero.tCharAnimationFps).setWeaponFirstUp();
				}
				bool flag5 = this.cancelRelease;
				if (flag5)
				{
					this.fireTimer += Time.deltaTime * 1.2f;
					this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
					bool flag6 = this.fireforce <= 0.1f;
					if (flag6)
					{
						this.fireforce = 0f;
						this.cancelRelease = false;
						this.bowInNormalState = true;
						this.playerState = NetMsg.PlayerState.daiji;
						base.hero.SetAnimator("PlayerState", 2, 3, 1);
					}
				}
				else
				{
					bool flag7 = !this.canFire && this.bowInNormalState && this.weaponFirstUp;
					if (flag7)
					{
						this.canFire = true;
					}
					bool flag8 = base.hero.nHuDongState == 0 && this.setHuDonStateTrigger;
					if (flag8)
					{
						this.setHuDonStateTrigger = false;
					}
					bool flag9 = base.hero.nHuDongState >= 50 && base.hero.nHuDongState < 300 && !this.setHuDonStateTrigger;
					if (flag9)
					{
						base.hero.SetAnimator("fireforce", 1, 0f, 1);
						this.fireforce = 0f;
						base.hero.SetAnimator("Fire", 0, false, 1);
						this.playerState = NetMsg.PlayerState.daiji;
						base.hero.SetAnimator("PlayerState", 2, 3, 1);
						this.canFire = false;
						this.bowInNormalState = true;
						this.weaponFirstUp = false;
						this.setHuDonStateTrigger = true;
						((TBCharAnimationHouYiFps)base.hero.tCharAnimationFps).houyiSetFireforce(true, base.hero.nHuDongState);
						((TBCharAnimationHouYiTps)base.hero.tCharAnimationTps).houyiSetFireforce();
					}
					else
					{
						bool flag10 = !base.hero.GetAnimatorBool("SkillSuper") && this.canFire && !this.b_fight && !this.keyDownAtSameTime && !base.hero.isDead;
						if (flag10)
						{
							bool flag11 = Input_Manager.Get_Button("WeaponFirst") && !this.onWall && this.weaponFirstUp;
							if (flag11)
							{
								this.XuliState();
							}
							bool flag12 = this.fire;
							if (flag12)
							{
								this.fire = false;
								this.faSheState();
							}
						}
						else
						{
							bool flag13 = base.hero.GetAnimatorBool("SkillSuper") && this.playerState != NetMsg.PlayerState.daiji;
							if (flag13)
							{
								base.hero.SetAnimator("fireforce", 1, 0f, 1);
								this.playerState = NetMsg.PlayerState.daiji;
								base.hero.SetAnimator("PlayerState", 2, 3, 1);
							}
						}
						this.daiJiState();
						bool flag14 = base.hero.GetAnimatorInteger("AttackState") == 3;
						if (flag14)
						{
							this.FightTimer = 1f;
							this.b_fight = true;
							base.hero.SetAnimator("PlayerState", 2, 3, 1);
							bool flag15 = this.playerState != NetMsg.PlayerState.daiji;
							if (flag15)
							{
								this.weaponFirstUp = false;
							}
							else
							{
								this.weaponFirstUp = true;
							}
							this.fireTimer = 0f;
							this.playerState = NetMsg.PlayerState.daiji;
							this.cancelRelease = true;
							this.canFire = false;
							this.bowInNormalState = false;
						}
						bool flag16 = this.FightTimer >= 0f && this.b_fight;
						if (flag16)
						{
							this.FightTimer -= Time.deltaTime;
							bool flag17 = this.fire;
							if (flag17)
							{
								this.fire = false;
								this.faSheState();
							}
						}
						bool flag18 = this.FightTimer <= 0f && this.b_fight;
						if (flag18)
						{
							this.b_fight = false;
							this.onWall = false;
							base.hero.SetAnimator("fireforce", 1, 0f, 1);
							this.playerState = NetMsg.PlayerState.daiji;
						}
						bool flag19 = base.hero.tChar.wallState > TBCharacter.WallState.OnGround;
						if (flag19)
						{
							bool flag20 = base.hero.tChar.wallState == TBCharacter.WallState.OnWall && !this.checkNeedPlayClimAnim;
							if (flag20)
							{
								this.checkNeedPlayClimAnim = true;
								base.StartCoroutine(this.checkNeedPlayClimbAnim());
							}
							else
							{
								bool flag21 = base.hero.tChar.wallState != TBCharacter.WallState.OnWall && this.onwall && !this.playPaqiang;
								if (flag21)
								{
									this.StopClimbAnim();
								}
							}
							bool flag22 = base.hero.tChar.wallState != TBCharacter.WallState.PaQiangEnd;
							if (flag22)
							{
								bool flag23 = base.hero.tChar.wallState == TBCharacter.WallState.OnWall;
								if (flag23)
								{
									bool flag24 = this.fireforce != 0f;
									if (flag24)
									{
										base.hero.SetAnimator("fireforce", 1, 0f, 1);
									}
									this.onWall = true;
									this.fireforce = 0f;
								}
								else
								{
									bool flag25 = ((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo == 1;
									if (flag25)
									{
										base.hero.SetAnimator("SkillJiTui", 0, true, 1);
									}
									else
									{
										bool flag26 = ((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo == 2;
										if (flag26)
										{
											base.hero.SetAnimator("SkillBaoLie", 0, true, 1);
										}
									}
									this.FightTimer = 0.5f;
									this.b_fight = true;
								}
							}
						}
						else
						{
							bool flag27 = this.onwall;
							if (flag27)
							{
								this.StopClimbAnim();
								base.hero.SetAnimator("PaQiang", 0, false, 1);
							}
						}
						bool flag28 = (Input_Manager.Get_Button("WeaponSec") && !this.cancelRelease) || base.hero.tChar.wallState == TBCharacter.WallState.OnWall || !base.hero.wInput.focus();
						if (flag28)
						{
							bool flag29 = this.playerState == NetMsg.PlayerState.xuli;
							if (flag29)
							{
								this.fireTimer = 0f;
								this.playerState = NetMsg.PlayerState.daiji;
								this.cancelRelease = true;
								this.canFire = false;
								this.bowInNormalState = false;
								this.weaponFirstUp = false;
								this.houyiFps.fulling = false;
								base.hero.SetAnimator("fireforce", 1, 0f, 1);
								base.hero.SetAnimator("PlayerState", 2, 3, 1);
							}
						}
						else
						{
							bool flag30 = Input_Manager.Get_Button_Down("superskill") && this.InputCheck() && !base.hero.wInput.notInput;
							if (flag30)
							{
								base.hero.superSkill(null);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000A92E7 File Offset: 0x000A74E7
	private IEnumerator checkNeedPlayClimbAnim()
	{
		yield return new WaitForSeconds(0.05f);
		bool flag = base.hero.charMove.state.onwall == 1;
		if (flag)
		{
			CameraControl.Instance.SetPaQiangCaMera();
			this.playClimbAnim();
		}
		else
		{
			bool flag2 = base.hero.charMove.state.onwall != 1;
			if (flag2)
			{
				this.checkNeedPlayClimAnim = false;
			}
		}
		yield break;
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x000A92F6 File Offset: 0x000A74F6
	private void playClimbAnim()
	{
		base.hero.wInput.notInput = true;
		this.onwall = true;
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000A9312 File Offset: 0x000A7512
	private void StopClimbAnim()
	{
		base.StopCoroutine(this.checkNeedPlayClimbAnim());
		CameraControl.Instance.StopPaQiangCaMera();
		this.onwall = false;
		base.hero.wInput.notInput = false;
		this.checkNeedPlayClimAnim = false;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000A934D File Offset: 0x000A754D
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.cw_me = (CWeaponControlMe)base.hero.wc;
		this.houyiFps = (TBCharAnimationHouYiFps)base.hero.tCharAnimationFps;
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000A9384 File Offset: 0x000A7584
	public override void SetChenMo(bool m_bool)
	{
		base.SetChenMo(m_bool);
		bool flag = m_bool && base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
	}

	// Token: 0x04000E2F RID: 3631
	private float FireFinishTimer;

	// Token: 0x04000E30 RID: 3632
	private float FightTimer = 1f;

	// Token: 0x04000E31 RID: 3633
	private bool b_fight = true;

	// Token: 0x04000E32 RID: 3634
	private bool onWall;

	// Token: 0x04000E33 RID: 3635
	private float fireforce;

	// Token: 0x04000E34 RID: 3636
	private float BowStayTimer;

	// Token: 0x04000E35 RID: 3637
	public NetMsg.PlayerState playerState;

	// Token: 0x04000E36 RID: 3638
	private bool onwall;

	// Token: 0x04000E37 RID: 3639
	private bool checkNeedPlayClimAnim;

	// Token: 0x04000E38 RID: 3640
	private bool playPaqiang;

	// Token: 0x04000E39 RID: 3641
	private CWeaponControlMe cw_me;

	// Token: 0x04000E3A RID: 3642
	private bool fire;

	// Token: 0x04000E3B RID: 3643
	private bool OnAirFashe;

	// Token: 0x04000E3C RID: 3644
	private float fireTimer;

	// Token: 0x04000E3D RID: 3645
	private float fireFullTimer;

	// Token: 0x04000E3E RID: 3646
	private float onAirTimer;

	// Token: 0x04000E3F RID: 3647
	private bool cancelRelease;

	// Token: 0x04000E40 RID: 3648
	private bool canFire = true;

	// Token: 0x04000E41 RID: 3649
	private bool bowInNormalState;

	// Token: 0x04000E42 RID: 3650
	private bool weaponFirstUp;

	// Token: 0x04000E43 RID: 3651
	private bool keyDownAtSameTime = true;

	// Token: 0x04000E44 RID: 3652
	private bool setHuDonStateTrigger;

	// Token: 0x04000E45 RID: 3653
	private TBCharAnimationHouYiFps houyiFps;
}

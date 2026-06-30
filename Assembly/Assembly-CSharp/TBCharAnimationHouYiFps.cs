using System;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class TBCharAnimationHouYiFps : TBCharAnimationPlay
{
	// Token: 0x06002B75 RID: 11125 RVA: 0x0017EFF0 File Offset: 0x0017D1F0
	protected override void Start()
	{
		base.Start();
		this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
		this.BowStayTimer = 0f;
	}

	// Token: 0x06002B76 RID: 11126 RVA: 0x0017F00C File Offset: 0x0017D20C
	private void XuliState()
	{
		bool flag = base.hero.playerCommon.m_nHunMi == 1 || (base.hero.nHuDongState >= 50 && base.hero.nHuDongState < 300);
		if (!flag)
		{
			bool flag2 = this.cw_me == null;
			if (flag2)
			{
				this.cw_me = (CWeaponControlMe)base.hero.wc;
			}
			else
			{
				bool flag3 = !this.cw_me.fullIng;
				if (!flag3)
				{
					bool flag4 = this.playerState == TBCharAnimationHouYiFps.PlayerState.daiji;
					if (flag4)
					{
						this.playerState = TBCharAnimationHouYiFps.PlayerState.xuli;
						bool flag5 = this.onAirTimer > 0f;
						if (flag5)
						{
							this.fireforce = this.onAirTimer + 0.1f;
						}
						base.hero.SetAnimator("Fire", 0, true, 1);
						base.hero.SetAnimator("PlayerState", 2, 1, 2);
						this.fireFullTimer = 0f;
						this.animator.SetBool("InFireMode", false);
						this.setFireFull = false;
					}
					bool flag6 = this.playerState != TBCharAnimationHouYiFps.PlayerState.xuli || this.onWall;
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
							bool flag9 = this.fireFullTimer > 0.4f && !this.setFireFull;
							if (flag9)
							{
								base.hero.SetAnimator("FireFull", 0, true, 1);
								this.setFireFull = true;
							}
							this.animator.SetFloat("fireforce", this.fireforce);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002B77 RID: 11127 RVA: 0x0017F230 File Offset: 0x0017D430
	private void faSheState()
	{
		bool flag = !base.hero.bePlayActive;
		if (flag)
		{
			base.hero.bePlayActive = true;
		}
		bool flag2 = this.playerState == TBCharAnimationHouYiFps.PlayerState.daiji;
		if (flag2)
		{
			this.playerState = TBCharAnimationHouYiFps.PlayerState.xuli;
		}
		bool flag3 = this.playerState == TBCharAnimationHouYiFps.PlayerState.xuli;
		if (flag3)
		{
			this.FireFinishTimer = 0f;
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
			this.playerState = TBCharAnimationHouYiFps.PlayerState.fashe;
			base.hero.SetAnimator("PlayerState", 2, 2, 2);
		}
	}

	// Token: 0x06002B78 RID: 11128 RVA: 0x0017F2E8 File Offset: 0x0017D4E8
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
		bool flag2 = this.playerState == TBCharAnimationHouYiFps.PlayerState.fashe;
		if (flag2)
		{
			bool flag3 = this.fireforce > 0.9f && this.FireFinishTimer == 0f;
			if (flag3)
			{
				this.animator.SetInteger("FireMode", 3);
			}
			else
			{
				bool flag4 = this.fireforce > 0.4f && this.FireFinishTimer == 0f;
				if (flag4)
				{
					this.animator.SetInteger("FireMode", 2);
				}
				else
				{
					bool flag5 = this.FireFinishTimer == 0f;
					if (flag5)
					{
						this.animator.SetInteger("FireMode", 1);
					}
				}
			}
			bool flag6 = this.fireforce > 0f;
			if (flag6)
			{
				this.fireTimer += Time.deltaTime * 0.7f;
				this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
				bool flag7 = this.fireforce < 0f;
				if (flag7)
				{
					this.fireforce = 0f;
				}
				this.animator.SetFloat("fireforce", this.fireforce);
			}
			this.FireFinishTimer += Time.deltaTime;
			bool flag8 = this.FireFinishTimer > 0.4f;
			if (flag8)
			{
				bool flag9 = this.onAirTimer > 0.15f;
				if (flag9)
				{
					this.animator.SetBool("OnAirFaShe", true);
				}
				else
				{
					this.animator.SetBool("OnAirFaShe", false);
				}
				this.animator.SetBool("BreakFire", true);
				this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
				base.hero.SetAnimator("PlayerState", 2, 3, 2);
				this.FireFinishTimer = 0f;
			}
		}
		else
		{
			bool flag10 = this.playerState != TBCharAnimationHouYiFps.PlayerState.daiji;
			if (!flag10)
			{
				bool onGround2 = base.hero.tChar.onGround;
				if (onGround2)
				{
					this.animator.SetBool("OnAirFaShe", false);
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
					this.animator.SetFloat("fireforce", this.fireforce);
				}
				else
				{
					bool flag13 = this.fireforce > 0f;
					if (flag13)
					{
						this.fireTimer += Time.deltaTime * 0.7f;
						this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
						bool flag14 = this.fireforce < 0f;
						if (flag14)
						{
							this.fireforce = 0f;
						}
						this.animator.SetFloat("fireforce", this.fireforce);
					}
				}
			}
		}
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x0017F658 File Offset: 0x0017D858
	public void houyiSetFireforce(bool expression, int hudongState)
	{
		base.hero.SetAnimator("fireforce", 1, 0f, 1);
		this.fireforce = 0f;
		base.hero.SetAnimator("PlayerState", 2, 3, 2);
		if (expression)
		{
			bool flag = this.playerState != TBCharAnimationHouYiFps.PlayerState.daiji;
			if (flag)
			{
				this.canFire = false;
			}
			this.bowInNormalState = true;
			this.weaponFirstUp = false;
		}
		this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		base.hero.SetAnimator("PaQiangEnd", 0, false, 1);
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x0017F70E File Offset: 0x0017D90E
	public void setWeaponFirstUp()
	{
		this.canFire = false;
		this.bowInNormalState = true;
		this.weaponFirstUp = true;
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x0017F728 File Offset: 0x0017D928
	protected override void Update()
	{
		base.Update();
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			bool flag2 = this.playerState != TBCharAnimationHouYiFps.PlayerState.daiji && this.animator.GetInteger("PlayerState") == 3;
			if (flag2)
			{
				this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
			}
			bool flag3 = Input_Manager.Get_Button_Down("WeaponFirst") && Input_Manager.Get_Button_Down("WeaponSec") && !base.hero.isCopy;
			if (flag3)
			{
				base.hero.SetAnimator("fireforce", 1, 0f, 1);
				this.fireforce = 0f;
				base.hero.SetAnimator("Fire", 0, false, 1);
				this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
				base.hero.SetAnimator("PlayerState", 2, 3, 2);
				this.keyDownAtSameTime = true;
			}
			else
			{
				bool flag4 = (Input_Manager.Get_Button_Up("WeaponFirst") || Input_Manager.Get_Button_Up("WeaponSec")) && this.keyDownAtSameTime && !base.hero.isCopy;
				if (flag4)
				{
					this.keyDownAtSameTime = false;
				}
				bool flag5 = Input_Manager.Get_Button_Up("WeaponFirst") && !this.weaponFirstUp;
				if (flag5)
				{
					this.weaponFirstUp = true;
				}
				bool flag6 = this.cancelRelease;
				if (flag6)
				{
					this.fireTimer += Time.deltaTime * 1.2f;
					this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
					bool flag7 = this.fireforce <= 0.1f;
					if (flag7)
					{
						this.fireforce = 0f;
						this.cancelRelease = false;
						this.bowInNormalState = true;
						base.hero.SetAnimator("PlayerState", 2, 3, 2);
						base.hero.SetAnimator("fireforce", 1, this.fireforce, 1);
					}
					this.animator.SetFloat("fireforce", this.fireforce);
				}
				else
				{
					bool flag8 = this.weaponFirstUp && !this.canFire && this.bowInNormalState;
					if (flag8)
					{
						this.canFire = true;
					}
					bool flag9 = !this.animator.GetBool("SkillSuper") && this.canFire && !this.b_fight && !this.keyDownAtSameTime && !base.hero.isDead;
					if (flag9)
					{
						bool flag10 = Input_Manager.Get_Button("WeaponFirst") && !this.onWall && !base.hero.isCopy;
						if (flag10)
						{
							this.XuliState();
						}
						bool flag11 = this.animator.GetInteger("AttackState") == 1 && this.playerState != TBCharAnimationHouYiFps.PlayerState.fashe;
						if (flag11)
						{
							this.faSheState();
						}
					}
					else
					{
						bool flag12 = this.animator.GetBool("SkillSuper") && this.playerState != TBCharAnimationHouYiFps.PlayerState.daiji;
						if (flag12)
						{
							this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
						}
					}
					this.daiJiState();
					bool flag13 = this.animator.GetInteger("AttackState") == 3;
					if (flag13)
					{
						this.FightTimer = 1f;
						this.b_fight = true;
						base.hero.SetAnimator("PlayerState", 2, 3, 2);
						bool flag14 = this.playerState != TBCharAnimationHouYiFps.PlayerState.daiji;
						if (flag14)
						{
							this.weaponFirstUp = false;
						}
						else
						{
							this.weaponFirstUp = true;
						}
						this.fireTimer = 0f;
						this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
						this.cancelRelease = true;
						this.canFire = false;
						this.bowInNormalState = false;
					}
					else
					{
						bool flag15 = this.FightTimer >= 0f && this.b_fight;
						if (flag15)
						{
							this.FightTimer -= Time.deltaTime;
							bool flag16 = this.animator.GetInteger("AttackState") == 1;
							if (flag16)
							{
								this.faSheState();
							}
						}
						bool flag17 = this.FightTimer <= 0f && this.b_fight;
						if (flag17)
						{
							this.b_fight = false;
							this.onWall = false;
							this.canFire = true;
							this.animator.SetFloat("fireforce", 0f);
							this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
						}
						bool flag18 = this.characterController.wallState > TBCharacter.WallState.OnGround;
						if (flag18)
						{
							bool flag19 = this.characterController.wallState == TBCharacter.WallState.PaQiangEnd;
							if (flag19)
							{
								this.animator.SetBool("PaQiangEnd", true);
							}
							else
							{
								bool flag20 = this.characterController.wallState == TBCharacter.WallState.OnWall;
								if (flag20)
								{
									this.animator.SetBool("PaQiang", true);
									this.animator.SetFloat("fireforce", 0f);
									this.onWall = true;
									this.fireforce = 0f;
								}
								else
								{
									this.animator.SetBool("PaQiang", false);
									this.animator.SetBool("PaQiangEnd", false);
									this.FightTimer = 0.5f;
									this.b_fight = true;
								}
							}
						}
						else
						{
							bool @bool = this.animator.GetBool("PaQiang");
							if (@bool)
							{
								this.animator.SetBool("PaQiang", false);
							}
						}
						bool flag21 = !this.cancelRelease && Input_Manager.Get_Button_Down("WeaponSec") && this.playerState == TBCharAnimationHouYiFps.PlayerState.xuli;
						if (flag21)
						{
							this.fireTimer = 0f;
							this.playerState = TBCharAnimationHouYiFps.PlayerState.daiji;
							this.cancelRelease = true;
							this.canFire = false;
							this.bowInNormalState = false;
							this.weaponFirstUp = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x04002546 RID: 9542
	private float FireFinishTimer;

	// Token: 0x04002547 RID: 9543
	public TBCharAnimationHouYiFps.PlayerState playerState;

	// Token: 0x04002548 RID: 9544
	private float fireforce;

	// Token: 0x04002549 RID: 9545
	private float BowStayTimer;

	// Token: 0x0400254A RID: 9546
	public bool fulling = true;

	// Token: 0x0400254B RID: 9547
	private CWeaponControlMe cw_me;

	// Token: 0x0400254C RID: 9548
	private bool setFireFull;

	// Token: 0x0400254D RID: 9549
	private float fireTimer;

	// Token: 0x0400254E RID: 9550
	private float fireFullTimer;

	// Token: 0x0400254F RID: 9551
	private float onAirTimer;

	// Token: 0x04002550 RID: 9552
	private float FightTimer;

	// Token: 0x04002551 RID: 9553
	private bool b_fight;

	// Token: 0x04002552 RID: 9554
	private bool onWall;

	// Token: 0x04002553 RID: 9555
	private bool cancelRelease;

	// Token: 0x04002554 RID: 9556
	private bool canFire = true;

	// Token: 0x04002555 RID: 9557
	private bool bowInNormalState;

	// Token: 0x04002556 RID: 9558
	private bool weaponFirstUp;

	// Token: 0x04002557 RID: 9559
	private bool keyDownAtSameTime;

	// Token: 0x0200067F RID: 1663
	public enum PlayerState
	{
		// Token: 0x040030C3 RID: 12483
		xuli = 1,
		// Token: 0x040030C4 RID: 12484
		fashe,
		// Token: 0x040030C5 RID: 12485
		daiji
	}
}

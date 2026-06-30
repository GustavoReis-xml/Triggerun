using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class HeroJvjishouMe : HeroBaseMe
{
	// Token: 0x060012C9 RID: 4809 RVA: 0x000B4610 File Offset: 0x000B2810
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		((IHeroDataJvjishou)this.IHD).playerState = NetMsg.JvJiShouState.BuKaiJing;
		this.superSkillAnimationFinish = true;
		this.canChangeBack = false;
		this.cantInputTimer = 0f;
		this.hasSetFov = false;
		this.reloadAction = false;
		this.canChangeFov = true;
		((IHeroDataJvjishou)this.IHD).GuanJing = false;
		this.InSuperDoing = false;
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000B4680 File Offset: 0x000B2880
	public void KaiJingState()
	{
		bool flag = base.hero.wInput.notInput || !this.superSkillAnimationFinish || this.cantInputTimer > 0f || ((IHeroDataJvjishou)this.IHD).playerState == NetMsg.JvJiShouState.KaiJing;
		if (!flag)
		{
			bool flag2 = base.hero.heroSkill.defaultReadySkill != 0;
			if (flag2)
			{
				this.cantInputTimer = 0.5f;
			}
			else
			{
				bool flag3 = (base.hero.nHuDongState < 50 || base.hero.nHuDongState >= 300) && !base.hero.isDead;
				if (flag3)
				{
					base.hero.ChangeWeaponGroup(1);
					this.switchSniperFireCD = 0.3f;
					base.hero.wInput.notShoot = true;
					this.b_switchSniper = true;
					((IHeroDataJvjishou)this.IHD).playerState = NetMsg.JvJiShouState.KaiJing;
					base.hero.wc.UpdateFireTime(0.3f);
					base.hero.SetAnimator("SkillTP", 0, false, 2);
					base.hero.SetAnimator("Sniper", 0, true, 2);
					this.changeFOVTimer = 0.35f;
					((CPlayerSoundMe)base.hero.playerSound).PlaySniperSwitchSound(0);
					this.canChangeFOV = true;
					this.hasSetFov = false;
					this.reloadAction = false;
					((IHeroDataJvjishou)this.IHD).GuanJing = false;
					bool flag4 = base.hero.heroBase.IsHeroInvi();
					if (flag4)
					{
						base.hero.DeleteBuffEffect(base.hero, 1012);
					}
				}
			}
		}
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000B4838 File Offset: 0x000B2A38
	public void BuKaiJingState(float delay)
	{
		bool flag = ((IHeroDataJvjishou)this.IHD).playerState != NetMsg.JvJiShouState.BuKaiJing && !this.canChangeBack;
		if (flag)
		{
			this.canChangeFOV = false;
			this.switchSniperFireCD = 0.3f;
			this.b_switchSniper = true;
			base.hero.SetAnimator("Sniper", 0, false, 2);
			base.hero.ChangeWeaponGroup(0);
			base.hero.wInput.notShoot = true;
			CameraControl.Instance.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
			this.delayChangePlayerStateTimer = delay;
			CameraControl.Instance.CameraZoom = false;
			this.canChangeBack = true;
			((IHeroDataJvjishou)this.IHD).GuanJing = true;
			((CPlayerSoundMe)base.hero.playerSound).PlaySniperSwitchSound(1);
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000B4918 File Offset: 0x000B2B18
	private void delayBackState()
	{
		bool flag = ((IHeroDataJvjishou)this.IHD).playerState != NetMsg.JvJiShouState.BuKaiJing && this.canChangeBack;
		if (flag)
		{
			this.delayChangePlayerStateTimer -= Time.deltaTime;
			bool flag2 = this.delayChangePlayerStateTimer <= 0f;
			if (flag2)
			{
				((IHeroDataJvjishou)this.IHD).playerState = NetMsg.JvJiShouState.BuKaiJing;
				this.canChangeBack = false;
			}
		}
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000B4988 File Offset: 0x000B2B88
	private void ChangeBigFOV()
	{
		bool flag = this.canChangeFOV;
		if (flag)
		{
			bool flag2 = this.changeFOVTimer > 0f;
			if (flag2)
			{
				this.changeFOVTimer -= Time.deltaTime;
			}
			bool flag3 = this.breakExpressionAnim && this.changeFOVTimer < 0.35f;
			if (flag3)
			{
				this.breakExpressionAnim = false;
				base.hero.SetAnimator("SkillTP", 0, false, 2);
				base.hero.SetAnimator("Sniper", 0, true, 2);
			}
			bool flag4 = this.changeFOVTimer <= 0f;
			if (flag4)
			{
				CameraControl.Instance.CameraZoom = true;
				CameraControl.Instance.controlCamera.fieldOfView = 25f;
				this.hasSetFov = true;
				this.canChangeFOV = false;
			}
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000B4A66 File Offset: 0x000B2C66
	public override void BreakSth(int type)
	{
		base.BreakSth(type);
		this.BuKaiJingState(0.4f);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000B4A80 File Offset: 0x000B2C80
	public override void RoundBegin(int type)
	{
		base.RoundBegin(type);
		this.BuKaiJingState(0.4f);
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000B4AE0 File Offset: 0x000B2CE0
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
		bool flag = base.hero.heroBase.IsHeroInvi();
		if (flag)
		{
			base.hero.DeleteBuffEffect(base.hero, 1012);
		}
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000B4B24 File Offset: 0x000B2D24
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		bool flag = base.hero.heroBase.IsHeroInvi();
		if (flag)
		{
			base.hero.DeleteBuffEffect(base.hero, 1012);
		}
		base.hero.wInput.notInput = true;
		this.InSuperDoing = true;
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		this.BuKaiJingState(0.4f);
		this.superSkillAnimationFinish = false;
		base.StartCoroutine(this.SuperSkillAnimationFinish());
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000B4BCF File Offset: 0x000B2DCF
	private IEnumerator SuperSkillAnimationFinish()
	{
		yield return new WaitForSeconds(1.2f);
		this.superSkillAnimationFinish = true;
		base.hero.wInput.notInput = false;
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		yield break;
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000B4BDE File Offset: 0x000B2DDE
	public override void alphaEnd()
	{
		this.SuperSkillFinish();
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000B4BE8 File Offset: 0x000B2DE8
	public override void SuperSkillFinish()
	{
		this.InSuperDoing = false;
		base.SuperSkillFinish();
		bool flag = base.hero.playerCommon.m_nHunMi == 1;
		if (flag)
		{
			base.hero.bePlayActive = false;
		}
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000B4C2C File Offset: 0x000B2E2C
	public override void OnDeath()
	{
		base.OnDeath();
		this.BuKaiJingState(0.4f);
		base.hero.SetAnimator("Sniper", 0, false, 2);
		base.hero.SetAnimator("Die", 0, true, 1);
		this.canChangeFov = false;
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000B4C88 File Offset: 0x000B2E88
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("SkillTP", 0, false, 2);
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
		base.hero.tpsRender.SetParticleSystem(true);
		this.BuKaiJingState(0.4f);
		this.canChangeFov = true;
		this.B_Transforing = false;
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000B4D20 File Offset: 0x000B2F20
	public override void Freeze(bool m_bool)
	{
		bool flag = !m_bool;
		if (flag)
		{
			base.hero.tpsRender.SetTransprtMeshVisible();
			base.hero.SetAnimator("FreezeBreak", 0, true, 1);
			bool flag2 = !this.hasSetFov;
			if (flag2)
			{
				this.BuKaiJingState(0.4f);
			}
		}
		if (m_bool)
		{
			this.canChangeFOV = false;
		}
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000B4D8C File Offset: 0x000B2F8C
	protected override void Update()
	{
		base.Update();
		bool flag = Input_Manager.Get_Button("Esc") || Input_Manager.Get_Button("Key_Open_Chat_Window");
		if (flag)
		{
			this.BuKaiJingState(0.1f);
		}
		bool flag2 = this.switchSniperFireCD > 0f;
		if (flag2)
		{
			this.switchSniperFireCD -= Time.deltaTime;
		}
		else
		{
			bool flag3 = this.b_switchSniper;
			if (flag3)
			{
				this.b_switchSniper = false;
				base.hero.wInput.notShoot = false;
			}
		}
		bool inSuperDoing = this.InSuperDoing;
		if (inSuperDoing)
		{
			base.hero.bePlayActive = false;
		}
		bool flag4 = !this.InputCheck() || !this.canChangeFov;
		if (!flag4)
		{
			bool flag5 = this.cantInputTimer >= 0f;
			if (flag5)
			{
				this.cantInputTimer -= Time.deltaTime;
			}
			bool flag6 = Input_Manager.Get_Axis("Jump") > 0f && base.hero.tChar.onGround;
			if (flag6)
			{
				this.BuKaiJingState(0.4f);
			}
			bool animatorBool = base.hero.GetAnimatorBool("Reload");
			if (animatorBool)
			{
				CameraControl.Instance.CameraZoom = false;
				this.BuKaiJingState(1.8f);
				this.reloadAction = true;
			}
			bool flag7 = Input_Manager.Get_Button_Down("Fight");
			if (flag7)
			{
				this.BuKaiJingState(0.4f);
			}
			bool flag8 = Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
			if (flag8)
			{
				base.hero.superSkill(null);
			}
			bool flag9 = Input_Manager.Get_Button_Down("WeaponSec") && !base.hero.GetAnimatorBool("Fight");
			if (flag9)
			{
				bool flag10 = ((IHeroDataJvjishou)this.IHD).playerState == NetMsg.JvJiShouState.BuKaiJing;
				if (flag10)
				{
					this.KaiJingState();
				}
				else
				{
					this.BuKaiJingState(0.4f);
				}
			}
			bool flag11 = Input_Manager.Get_Button("WeaponSec") && this.reloadAction && ((IHeroDataJvjishou)this.IHD).playerState == NetMsg.JvJiShouState.BuKaiJing;
			if (flag11)
			{
				this.KaiJingState();
			}
			bool flag12 = base.hero.GetAnimatorBool("Fight") && base.hero.heroSkill.defaultReadySkill != 0;
			if (flag12)
			{
				base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
			}
			bool flag13 = ((IHeroDataJvjishou)this.IHD).playerState == NetMsg.JvJiShouState.KaiJing && !this.breakExpressionAnim && base.hero.nHuDongState >= 50;
			if (flag13)
			{
				this.BuKaiJingState(0.4f);
			}
			this.delayBackState();
			this.ChangeBigFOV();
		}
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x000B5078 File Offset: 0x000B3278
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		this.BuKaiJingState(0.4f);
	}

	// Token: 0x04000EC2 RID: 3778
	private float changeFOVTimer;

	// Token: 0x04000EC3 RID: 3779
	private bool canChangeFov;

	// Token: 0x04000EC4 RID: 3780
	private bool breakExpressionAnim;

	// Token: 0x04000EC5 RID: 3781
	private bool InSuperDoing;

	// Token: 0x04000EC6 RID: 3782
	private float switchSniperFireCD;

	// Token: 0x04000EC7 RID: 3783
	private bool b_switchSniper;

	// Token: 0x04000EC8 RID: 3784
	private float delayChangePlayerStateTimer;

	// Token: 0x04000EC9 RID: 3785
	private bool canChangeBack;

	// Token: 0x04000ECA RID: 3786
	private bool canChangeFOV;

	// Token: 0x04000ECB RID: 3787
	private float cantInputTimer;

	// Token: 0x04000ECC RID: 3788
	private bool hasSetFov;

	// Token: 0x04000ECD RID: 3789
	private bool superSkillAnimationFinish = true;

	// Token: 0x04000ECE RID: 3790
	private bool reloadAction;

	// Token: 0x020005A5 RID: 1445
	private enum PlayerState
	{
		// Token: 0x04002DC3 RID: 11715
		KaiJing = 1,
		// Token: 0x04002DC4 RID: 11716
		BuKaiJing
	}
}

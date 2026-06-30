using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class HeroJvjishouIt : HeroBaseIt
{
	// Token: 0x060012C1 RID: 4801 RVA: 0x000B4200 File Offset: 0x000B2400
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.bukaijing = true;
		this.kaijing = true;
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000B4218 File Offset: 0x000B2418
	public override void SuperSkillDo(object parm)
	{
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x000B4234 File Offset: 0x000B2434
	public override void Freeze(bool m_bool)
	{
		bool flag = !m_bool;
		if (flag)
		{
			bool flag2 = base.hero.tpsRender != null;
			if (flag2)
			{
				base.hero.tpsRender.SetTransprtMeshVisible();
			}
			base.hero.SetAnimator("FreezeBreak", 0, true, 1);
		}
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000B428D File Offset: 0x000B248D
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000B4298 File Offset: 0x000B2498
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.SetAnimator("Die", 0, true, 1);
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x000B42BB File Offset: 0x000B24BB
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("SkillTP", 0, false, 1);
		base.hero.heroBase.B_Transforing = false;
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000B42F0 File Offset: 0x000B24F0
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero.tCharAnimationTps == null || !base.hero.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = this.m_anim == null;
			if (flag2)
			{
				this.m_anim = base.hero.tCharAnimationTps.animator;
			}
			bool flag3 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
			if (flag3)
			{
				bool flag4 = this.m_anim.GetBool("SkillYinshenIng") && !this.b_yinshen;
				if (flag4)
				{
					this.b_yinshen = true;
					((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(0);
					((GamePlayer)this._hero).FPSrenderBianSe.ChangeYinShenFPSMat(0);
				}
				bool flag5 = !this.m_anim.GetBool("SkillYinshenIng") && this.b_yinshen;
				if (flag5)
				{
					this.b_yinshen = false;
					((GamePlayer)this._hero).renderBianSe.ChangeYinShenMat(2);
					((GamePlayer)this._hero).FPSrenderBianSe.ChangeYinShenFPSMat(2);
				}
				bool flag6 = this.m_anim.GetBool("SkillTPIng") && !this.b_transport;
				if (flag6)
				{
					this.m_anim.SetBool("FilmTransParm", true);
					this.b_transport = true;
					base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
				}
				bool flag7 = !this.m_anim.GetBool("SkillTPIng") && this.b_transport;
				if (flag7)
				{
					this.m_anim.SetBool("FilmTransParm", false);
					this.m_anim.SetBool("FilmTransParmIng", false);
					this.b_transport = false;
					base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
				}
				bool flag8 = this.m_anim.GetBool("Sniper") && this.bukaijing;
				if (flag8)
				{
					((IHeroDataJvjishou)this.IHD).playerState = NetMsg.JvJiShouState.KaiJing;
					((IHeroDataJvjishou)base.hero.heroBase.IHD).GuanJing = false;
					this.kaijing = true;
					this.bukaijing = false;
					CameraControl.Instance.CameraZoom = true;
					CameraControl.Instance.controlCamera.fieldOfView = 25f;
				}
				else
				{
					bool flag9 = !this.m_anim.GetBool("Sniper") && this.kaijing;
					if (flag9)
					{
						((IHeroDataJvjishou)base.hero.heroBase.IHD).GuanJing = true;
						this.kaijing = false;
						this.bukaijing = true;
						CameraControl.Instance.CameraZoom = false;
						CameraControl.Instance.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
					}
				}
			}
		}
	}

	// Token: 0x04000EBD RID: 3773
	private Animator m_anim;

	// Token: 0x04000EBE RID: 3774
	private bool kaijing;

	// Token: 0x04000EBF RID: 3775
	private bool bukaijing;

	// Token: 0x04000EC0 RID: 3776
	private bool b_transport;

	// Token: 0x04000EC1 RID: 3777
	private bool b_yinshen;
}

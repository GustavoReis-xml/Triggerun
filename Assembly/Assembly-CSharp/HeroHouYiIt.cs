using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class HeroHouYiIt : HeroBaseIt
{
	// Token: 0x060011E4 RID: 4580 RVA: 0x000A7CF8 File Offset: 0x000A5EF8
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		((TBCharacterThirdIt)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunWeightSpeed = 4f;
		base.hero.tChar.wallRunCan = true;
		base.hero.playerSound.LoadSwitchArrowSound();
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000A7D7F File Offset: 0x000A5F7F
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillBreakFight", 0, true, 1);
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000A7DA3 File Offset: 0x000A5FA3
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x000A7DB0 File Offset: 0x000A5FB0
	protected override void Update()
	{
		base.Update();
		bool flag = !base.hero.isCopy || !base.hero.playerHaveCreate || CFilmManager.Instance.filmParentNetId(base.hero) != base.hero.getNetid() || base.hero.tCharAnimationTps == null || base.hero.tCharAnimationFps == null;
		if (!flag)
		{
			bool flag2 = this.m_anim == null;
			if (flag2)
			{
				this.m_anim = base.hero.tCharAnimationTps.animator;
			}
			bool flag3 = this.fps_anim == null;
			if (flag3)
			{
				this.fps_anim = base.hero.tCharAnimationFps.animator;
			}
			else
			{
				bool flag4 = this.m_anim.GetInteger("PlayerState") == 3 && !base.hero.isDead && this.fireforce != 0f;
				if (flag4)
				{
					bool flag5 = !this.m_anim.GetBool("CancelXuli");
					if (flag5)
					{
						this.m_anim.SetBool("CancelXuli", true);
					}
					this.fireTimer += Time.deltaTime * 0.5f;
					this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
					bool flag6 = this.fireforce < 0.1f;
					if (flag6)
					{
						this.fireforce = 0f;
					}
					this.fps_anim.SetFloat("fireforce", this.fireforce);
				}
				bool flag7 = this.m_anim.GetInteger("PlayerState") == 1;
				if (flag7)
				{
					this.fps_anim.SetFloat("fireforce", this.fireforce);
					this.fireTimer = 0f;
					this.fireforce += Time.deltaTime * 0.8f;
					bool flag8 = this.fireforce >= 1f;
					if (flag8)
					{
						this.fireforce = 1f;
					}
				}
				bool flag9 = this.m_anim.GetInteger("PlayerState") == 2;
				if (flag9)
				{
					this.fireTimer += Time.deltaTime * 0.5f;
					this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
					bool flag10 = this.fireforce < 0f;
					if (flag10)
					{
						this.fireforce = 0f;
					}
					this.fps_anim.SetFloat("fireforce", this.fireforce);
				}
			}
		}
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000A8053 File Offset: 0x000A6253
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, true, 1);
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x000A8076 File Offset: 0x000A6276
	public override void OnDeath()
	{
		base.OnDeath();
	}

	// Token: 0x04000E2B RID: 3627
	private Animator m_anim;

	// Token: 0x04000E2C RID: 3628
	private Animator fps_anim;

	// Token: 0x04000E2D RID: 3629
	private float fireforce;

	// Token: 0x04000E2E RID: 3630
	private float fireTimer;
}

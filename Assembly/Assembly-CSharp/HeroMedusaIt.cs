using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class HeroMedusaIt : HeroBaseIt
{
	// Token: 0x06001379 RID: 4985 RVA: 0x000B9700 File Offset: 0x000B7900
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		((TBCharacterThirdIt)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		this.ReleaseSuper = false;
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000B9754 File Offset: 0x000B7954
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = base.hero.playerHaveCreate && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.tChar.moveByControlIt = true;
			base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
			base.hero.ChangeSpeed();
			base.hero.tCharAnimationTps.animator.SetBool("skill_Q", true);
			base.hero.tCharAnimationTps.animator.SetBool("Reload", false);
			this.ReleaseSuper = true;
			base.StartCoroutine(this.checkFinish());
			bool flag2 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
			if (flag2)
			{
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
			}
		}
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x000B9877 File Offset: 0x000B7A77
	private IEnumerator checkFinish()
	{
		bool flag = base.hero.netobject_type != 6;
		if (flag)
		{
			yield return new WaitForSeconds(0.1f);
			bool isdo = true;
			while (isdo)
			{
				yield return new WaitForSeconds(0.1f);
				isdo = base.hero.GetAnimatorBool("skill_Q");
			}
			base.hero.tChar.m_MoveByAnimator = true;
			base.hero.tChar.moveByControlIt = false;
			this.SuperSkillFinish();
		}
		else
		{
			yield return new WaitForSeconds(5f);
			base.hero.tChar.m_MoveByAnimator = true;
			base.hero.tChar.moveByControlIt = false;
			this.SuperSkillFinish();
		}
		yield break;
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000B9888 File Offset: 0x000B7A88
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		this.ReleaseSuper = false;
		base.hero.tCharAnimationTps.animator.SetBool("skill_Q", false);
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000B9928 File Offset: 0x000B7B28
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("skill_Q", 0, false, 1);
		base.hero.charMove.notMove = false;
		base.hero.heroBase.B_Transforing = false;
		bool releaseSuper = this.ReleaseSuper;
		if (releaseSuper)
		{
			this.SuperSkillFinish();
		}
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000B998C File Offset: 0x000B7B8C
	protected override void Update()
	{
		base.Update();
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid() && base.hero.playerHaveCreate && !base.hero.haveDestroy) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			bool flag2 = this.m_anim == null;
			if (flag2)
			{
				this.m_anim = base.hero.tCharAnimationTps.animator;
			}
			bool flag3 = this.m_anim.GetBool("skill_Q") && !this.b_super;
			if (flag3)
			{
				this.b_super = true;
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
			}
			bool flag4 = !this.m_anim.GetBool("skill_Q") && this.b_super;
			if (flag4)
			{
				this.b_super = false;
				base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
		}
	}

	// Token: 0x04000F22 RID: 3874
	private bool ReleaseSuper;

	// Token: 0x04000F23 RID: 3875
	private Animator m_anim;

	// Token: 0x04000F24 RID: 3876
	private bool b_super;
}

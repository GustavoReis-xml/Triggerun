using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class HeroXingJiLieRenIt : HeroBaseIt
{
	// Token: 0x060014D3 RID: 5331 RVA: 0x000C5E94 File Offset: 0x000C4094
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		((TBCharacterThirdIt)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		base.hero.tChar.wallRunCan = true;
		this.ReleaseSuper = false;
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000C5EF8 File Offset: 0x000C40F8
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = base.hero.playerHaveCreate && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.tChar.moveByControlIt = true;
			base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
			base.hero.ChangeSpeed();
			base.hero.tCharAnimationTps.animator.SetBool("SkillSuper", true);
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

	// Token: 0x060014D5 RID: 5333 RVA: 0x000C601B File Offset: 0x000C421B
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
				isdo = base.hero.GetAnimatorBool("SkillSuper");
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

	// Token: 0x060014D6 RID: 5334 RVA: 0x000C602C File Offset: 0x000C422C
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		this.ReleaseSuper = false;
		base.hero.tCharAnimationTps.animator.SetBool("SkillSuper", false);
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000C60CC File Offset: 0x000C42CC
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		base.hero.charMove.notMove = false;
		base.hero.heroBase.B_Transforing = false;
		bool releaseSuper = this.ReleaseSuper;
		if (releaseSuper)
		{
			this.SuperSkillFinish();
		}
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000C6130 File Offset: 0x000C4330
	protected override void Update()
	{
		base.Update();
		bool flag = (!base.hero.isCopy || base.hero.isDead || CFilmManager.Instance.filmParentNetId(base.hero) != base.hero.getNetid() || !base.hero.playerHaveCreate || base.hero.haveDestroy) && !GameRoot.inst.IsGmTarget(base.hero);
		if (!flag)
		{
			bool flag2 = this.m_anim == null;
			if (flag2)
			{
				this.m_anim = base.hero.tCharAnimationTps.animator;
			}
			bool flag3 = !(this.m_anim == null);
			if (flag3)
			{
				bool flag4 = this.m_anim.GetBool("SkillSuper") && !this.b_super;
				if (flag4)
				{
					this.b_super = true;
					base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
				}
				bool flag5 = !this.m_anim.GetBool("SkillSuper") && this.b_super;
				if (flag5)
				{
					this.b_super = false;
					base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
				}
			}
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000C6279 File Offset: 0x000C4479
	private IEnumerator checkNeedPlayClimbAnim()
	{
		yield return new WaitForSeconds(0.05f);
		Vector3 f = base.transform.forward;
		f.y = 0f;
		RaycastHit velocityHit = new RaycastHit
		{
			normal = Vector3.up
		};
		bool flag = Physics.Raycast(base.GetComponent<CapsuleCollider>().bounds.center, f, out velocityHit, 1.2f, 1);
		if (flag)
		{
			this.playClimbAnim();
		}
		yield break;
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000C6288 File Offset: 0x000C4488
	private void playClimbAnim()
	{
		base.hero.SetAnimator("PaQiang", 0, true, 1);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000C62A4 File Offset: 0x000C44A4
	private void StopClimbAnim()
	{
		base.hero.SetAnimator("PaQiang", 0, false, 1);
	}

	// Token: 0x04001031 RID: 4145
	private bool ReleaseSuper;

	// Token: 0x04001032 RID: 4146
	private Animator m_anim;

	// Token: 0x04001033 RID: 4147
	private bool b_super;
}

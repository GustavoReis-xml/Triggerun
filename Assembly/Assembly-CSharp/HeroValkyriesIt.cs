using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class HeroValkyriesIt : HeroBaseIt
{
	// Token: 0x06001479 RID: 5241 RVA: 0x000C0E70 File Offset: 0x000BF070
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool animatorBool = base.hero.GetAnimatorBool("SkillSuper");
		if (animatorBool)
		{
			this.SuperSkillDo(null);
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000C0EA4 File Offset: 0x000BF0A4
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
		}
		base.hero.playerCommon.m_fMoveSpeedAnimation = 9.5f;
		base.hero.ChangeSpeed();
		base.hero.tChar.moveByControlIt = true;
		this.isSuperSkill = true;
		base.StartCoroutine(this.checkFinish());
		base.hero.bePlayActive = false;
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000C0F75 File Offset: 0x000BF175
	private IEnumerator checkFinish()
	{
		yield return new WaitForSeconds(1.1f);
		while (this.isSuperSkill)
		{
			yield return new WaitForSeconds(0.1f);
			this.isSuperSkill = base.hero.GetAnimatorBool("SkillSuper");
			bool flag = !this.isSuperSkill;
			if (flag)
			{
				break;
			}
		}
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000C0F84 File Offset: 0x000BF184
	public override void SuperSkillFinish()
	{
		this.isSuperSkill = false;
		base.hero.tChar.moveByControlIt = false;
		base.hero.bePlayActive = true;
		base.SuperSkillFinish();
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x000C1026 File Offset: 0x000BF226
	public override void Freeze(bool mbool)
	{
		base.Freeze(mbool);
		base.hero.SetAnimator("ShopEffect", 0, mbool, 1);
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x000C104A File Offset: 0x000BF24A
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		base.hero.SetAnimator("ShopEffect", 0, m_bool, 1);
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000C106E File Offset: 0x000BF26E
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, false, 1);
	}

	// Token: 0x04000FDF RID: 4063
	private bool isSuperSkill;
}

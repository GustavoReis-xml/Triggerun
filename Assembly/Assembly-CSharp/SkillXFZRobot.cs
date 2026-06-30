using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class SkillXFZRobot : SkillBaseRobot
{
	// Token: 0x060028BE RID: 10430 RVA: 0x0016BF1B File Offset: 0x0016A11B
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x0016BF2C File Offset: 0x0016A12C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.bePlayActive = false;
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 1f;
		base.hero.ChangeSpeed();
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x0016BFB9 File Offset: 0x0016A1B9
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(0.5f);
		while (base.hero.playerCommon.m_nDongjie == 0 && this.skillTimeSpend < 9f)
		{
			this.skillTimeSpend += 0.5f;
			base.RealShoot(__target);
			yield return new WaitForSeconds(0.5f);
		}
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x0016BFD0 File Offset: 0x0016A1D0
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 1f;
		base.hero.ChangeSpeed();
	}

	// Token: 0x04002325 RID: 8997
	private float skillTimeSpend;
}

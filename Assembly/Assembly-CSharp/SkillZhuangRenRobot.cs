using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class SkillZhuangRenRobot : SkillBaseRobot
{
	// Token: 0x06002926 RID: 10534 RVA: 0x0016CBC9 File Offset: 0x0016ADC9
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x0016CBD4 File Offset: 0x0016ADD4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.tChar.hangInAir = true;
		base.hero.SetAnimator("skill_zhuang", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x0016CC45 File Offset: 0x0016AE45
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(1f);
		base.hero.SetAnimator("skill_zhuang", 0, false, 1);
		base.hero.SetAnimator("SuerpIng", 0, true, 1);
		yield return new WaitForSeconds(3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x0016CC5B File Offset: 0x0016AE5B
	public override void BreakSkill()
	{
		base.BreakSkill();
		base.StopAllCoroutines();
		this.OneSkillFinish();
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x0016CC74 File Offset: 0x0016AE74
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.tChar.hangInAir = false;
		base.hero.SetAnimator("SuerpIng", 0, false, 1);
	}
}

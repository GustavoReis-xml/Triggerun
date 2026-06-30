using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class Skill_FaShi_YuanChengRobot : SkillBaseRobot
{
	// Token: 0x0600294D RID: 10573 RVA: 0x0016D5C3 File Offset: 0x0016B7C3
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x0016D5D0 File Offset: 0x0016B7D0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillState", 2, 1, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
		base.RealShoot(__target);
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x0016D626 File Offset: 0x0016B826
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x0016D63C File Offset: 0x0016B83C
	public override void BreakSkill()
	{
		base.BreakSkill();
		base.StopAllCoroutines();
		this.OneSkillFinish();
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x0016D654 File Offset: 0x0016B854
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillState", 2, 0, 1);
	}
}

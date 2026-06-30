using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class Skill_FaShi_ChongJiBoRobot : SkillBaseRobot
{
	// Token: 0x06002941 RID: 10561 RVA: 0x0016D3BC File Offset: 0x0016B5BC
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x0016D3C8 File Offset: 0x0016B5C8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillState", 2, 3, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x0016D416 File Offset: 0x0016B616
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x0016D42C File Offset: 0x0016B62C
	public override void BreakSkill()
	{
		base.BreakSkill();
		base.StopAllCoroutines();
		this.OneSkillFinish();
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x0016D444 File Offset: 0x0016B644
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillState", 2, 0, 1);
	}
}

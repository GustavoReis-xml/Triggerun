using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class SkillZhiLiaoShuiJingRobot : SkillBaseRobot
{
	// Token: 0x06002921 RID: 10529 RVA: 0x0016CB5D File Offset: 0x0016AD5D
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.DelaySkillFinish());
	}

	// Token: 0x06002922 RID: 10530 RVA: 0x0016CB8F File Offset: 0x0016AD8F
	private IEnumerator DelaySkillFinish()
	{
		yield return new WaitForSeconds(0.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x0016CB9E File Offset: 0x0016AD9E
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x0016CBA8 File Offset: 0x0016ADA8
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}
}

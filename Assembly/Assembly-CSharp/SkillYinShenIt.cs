using System;
using System.Collections;

// Token: 0x02000398 RID: 920
public class SkillYinShenIt : SkillBaseIt
{
	// Token: 0x060028E3 RID: 10467 RVA: 0x0016C43C File Offset: 0x0016A63C
	public override void OneSkillInput()
	{
		base.OneSkillInput();
		bool flag = ((HeroPlayer)base.hero).heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.SetAnimator("SkillTP", 0, false, 1);
			((HeroPlayer)base.hero).heroSkill.ReadyItemOp(((HeroPlayer)base.hero).heroSkill.defaultReadySkill, false, false);
		}
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x0016C4B5 File Offset: 0x0016A6B5
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillYinshen", 0, true, 1);
		base.hero.SetAnimator("Reload", 0, false, 1);
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x0016C4F3 File Offset: 0x0016A6F3
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}
}

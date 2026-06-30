using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class SkillGuiLeiMe : SkillBaseMe
{
	// Token: 0x060026F5 RID: 9973 RVA: 0x001611A9 File Offset: 0x0015F3A9
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.wInput.notInput = false;
		((HeroBaseMe)base.hero.heroBase).SetStat("guilei", -1);
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x001611E4 File Offset: 0x0015F3E4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		((HeroBaseMe)base.hero.heroBase).SetStat("guilei", 1);
		base.hero.wInput.notInput = true;
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x0016124D File Offset: 0x0015F44D
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}
}

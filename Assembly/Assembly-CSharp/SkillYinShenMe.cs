using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class SkillYinShenMe : SkillBaseMe
{
	// Token: 0x060028E7 RID: 10471 RVA: 0x0016C508 File Offset: 0x0016A708
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillYinshen", 0, true, 2);
		base.hero.SetAnimator("SkillYinshenIng", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.bePlayActive = false;
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.skillFinish());
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x0016C5B4 File Offset: 0x0016A7B4
	public override void OneSkillInput()
	{
		base.OneSkillInput();
		bool flag = base.hero.heroBase.IsHeroInvi();
		if (flag)
		{
			base.hero.DeleteBuffEffect(base.hero, 1012);
			this.OneSkillFinish();
		}
		bool flag2 = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag2)
		{
			base.hero.SetAnimator("SkillTP", 0, false, 1);
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x0016C654 File Offset: 0x0016A854
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillYinshen", 0, false, 2);
		base.hero.SetAnimator("SkillYinshenIng", 0, false, 2);
		base.hero.bePlayActive = true;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x0016C6A8 File Offset: 0x0016A8A8
	private IEnumerator skillFinish()
	{
		yield return new WaitForSeconds(1f);
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		while (base.hero.heroBase.IsHeroInvi())
		{
			yield return new WaitForSeconds(0.5f);
		}
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x0016C6B7 File Offset: 0x0016A8B7
	public override void readyItemSwitch(bool switchOpen, bool doSkill)
	{
		base.readyItemSwitch(switchOpen, false);
	}
}

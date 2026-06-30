using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class SkillRanShaoDanMe : SkillBaseMe
{
	// Token: 0x0600279D RID: 10141 RVA: 0x001659A4 File Offset: 0x00163BA4
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
		((HeroBaseMe)base.hero.heroBase).SetStat("ranshaodan", -1);
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x001659F4 File Offset: 0x00163BF4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.OneSkillAction(__target, parm2);
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		((HeroBaseMe)base.hero.heroBase).SetStat("ranshaodan", 1);
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x00165A99 File Offset: 0x00163C99
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(0.67f);
		this.OneSkillFinish();
		yield break;
	}
}

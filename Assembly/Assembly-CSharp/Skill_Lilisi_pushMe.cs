using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003B6 RID: 950
public class Skill_Lilisi_pushMe : SkillBaseMe
{
	// Token: 0x0600295E RID: 10590 RVA: 0x0016D7C4 File Offset: 0x0016B9C4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("Skill_push", 0, true, 1);
		base.hero.wInput.notInput = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x0016D842 File Offset: 0x0016BA42
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x0016D851 File Offset: 0x0016BA51
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
	}
}

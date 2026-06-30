using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class Skill_Rambo_2_Me : SkillBaseMe
{
	// Token: 0x060029C1 RID: 10689 RVA: 0x0016FD50 File Offset: 0x0016DF50
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((Hero_Rambo_Me)base.hero.heroBase).Accurately_Aim_Close();
		this.Break_Reload();
		base.hero.wInput.notInput = true;
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x0016FDA3 File Offset: 0x0016DFA3
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x0016FDB2 File Offset: 0x0016DFB2
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}
}

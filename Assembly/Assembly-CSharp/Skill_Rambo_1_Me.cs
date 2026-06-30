using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class Skill_Rambo_1_Me : SkillBaseMe
{
	// Token: 0x060029BC RID: 10684 RVA: 0x0016FCC0 File Offset: 0x0016DEC0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((Hero_Rambo_Me)base.hero.heroBase).Accurately_Aim_Close();
		this.Break_Reload();
		base.hero.wInput.notInput = true;
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x0016FD13 File Offset: 0x0016DF13
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x0016FD22 File Offset: 0x0016DF22
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}
}

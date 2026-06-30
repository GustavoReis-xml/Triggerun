using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C5 RID: 965
public class Skill_NeZhaYouMe : SkillBaseMe
{
	// Token: 0x060029B7 RID: 10679 RVA: 0x0016FC56 File Offset: 0x0016DE56
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x0016FC81 File Offset: 0x0016DE81
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x0016FC90 File Offset: 0x0016DE90
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}
}

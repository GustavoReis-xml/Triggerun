using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class Skill_Roadhog_Jump_It : SkillBaseIt
{
	// Token: 0x060029C7 RID: 10695 RVA: 0x0016FDE9 File Offset: 0x0016DFE9
	public override void OneSkillAction(object _target, ArrayList parm2)
	{
		base.gameplayer.SetAnimator("Super_Jump", 0, true, 1);
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x0016FE12 File Offset: 0x0016E012
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x0016FE21 File Offset: 0x0016E021
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.gameplayer.SetAnimator("SkillJumpEnd", 0, true, 1);
	}
}

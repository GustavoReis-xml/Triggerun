using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class SkillSuckBloodMe : SkillBaseMe
{
	// Token: 0x06002867 RID: 10343 RVA: 0x0016ABD4 File Offset: 0x00168DD4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("XiXue", 0, true, 2);
		base.hero.SetAnimator("SkillXiXueAction", 0, true, 1);
		base.StartCoroutine(this.delayFinish());
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x0016AC2A File Offset: 0x00168E2A
	private IEnumerator delayFinish()
	{
		yield return new WaitForSeconds(5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x0016AC39 File Offset: 0x00168E39
	public override void OneSkillFinish()
	{
		base.hero.SetAnimator("XiXue", 0, false, 2);
		base.OneSkillFinish();
	}
}

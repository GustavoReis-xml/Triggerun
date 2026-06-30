using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class SkillXingJiBiaoJiRobot : SkillBaseRobot
{
	// Token: 0x060028CC RID: 10444 RVA: 0x0016C159 File Offset: 0x0016A359
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.delaySkillFinish());
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x0016C18B File Offset: 0x0016A38B
	private IEnumerator delaySkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028CE RID: 10446 RVA: 0x0016C19A File Offset: 0x0016A39A
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x0016C1A4 File Offset: 0x0016A3A4
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}
}

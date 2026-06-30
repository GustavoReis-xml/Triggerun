using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class SkillTreatmentRobot : SkillBaseRobot
{
	// Token: 0x060028B9 RID: 10425 RVA: 0x0016BE8C File Offset: 0x0016A08C
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.charMove.notMove = true;
		((SkillSprintRobot)base.hero.skillEffectDict[1030]).OneSkillFinish();
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x0016BEF9 File Offset: 0x0016A0F9
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(1.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x0016BF08 File Offset: 0x0016A108
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}
}

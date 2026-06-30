using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class SkillGrenadeRobot : SkillBaseRobot
{
	// Token: 0x060026EE RID: 9966 RVA: 0x0016112E File Offset: 0x0015F32E
	private void Skill_Start()
	{
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x00161134 File Offset: 0x0015F334
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x0016114C File Offset: 0x0015F34C
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x0016115B File Offset: 0x0015F35B
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x0016118D File Offset: 0x0015F38D
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}
}

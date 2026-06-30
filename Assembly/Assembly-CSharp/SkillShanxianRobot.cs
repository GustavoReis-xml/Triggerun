using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class SkillShanxianRobot : SkillBaseRobot
{
	// Token: 0x060027FB RID: 10235 RVA: 0x00168877 File Offset: 0x00166A77
	private void Start()
	{
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x0016887C File Offset: 0x00166A7C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		Vector3 vector = Vector3.zero;
		Vector3 forward = base.hero.UserControl.transform.forward;
		forward.y = 0f;
		vector = forward.normalized;
		base.hero.tChar.moveByControlIt = true;
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillBlinkz", 0, true, 1);
		base.hero.heroBase.B_Transforing = true;
		this.OneSkillFinish();
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x0016891F File Offset: 0x00166B1F
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.heroBase.B_Transforing = false;
		base.hero.tChar.moveByControlIt = false;
	}
}

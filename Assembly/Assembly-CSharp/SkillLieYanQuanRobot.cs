using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class SkillLieYanQuanRobot : SkillBaseRobot
{
	// Token: 0x06002752 RID: 10066 RVA: 0x001632F9 File Offset: 0x001614F9
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x00163304 File Offset: 0x00161504
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillE", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x00163352 File Offset: 0x00161552
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(0.1f);
		base.RealShoot(__target);
		base.hero.bePlayActive = false;
		yield return new WaitForSeconds(1.4f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x00163368 File Offset: 0x00161568
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillE", 0, false, 1);
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x0016338B File Offset: 0x0016158B
	private void Update()
	{
	}
}

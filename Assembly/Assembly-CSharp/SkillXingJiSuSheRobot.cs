using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class SkillXingJiSuSheRobot : SkillBaseRobot
{
	// Token: 0x060028DA RID: 10458 RVA: 0x0016C373 File Offset: 0x0016A573
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x0016C384 File Offset: 0x0016A584
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.bePlayActive = false;
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillLianShe", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x0016C3DF File Offset: 0x0016A5DF
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(0.5f);
		bool flag = base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.RealShoot(__target);
			base.hero.bePlayActive = false;
		}
		yield return new WaitForSeconds(1.7f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x0016C3F5 File Offset: 0x0016A5F5
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillLianShe", 0, false, 1);
	}
}

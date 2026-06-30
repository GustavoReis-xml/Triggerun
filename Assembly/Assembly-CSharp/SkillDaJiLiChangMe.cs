using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class SkillDaJiLiChangMe : SkillBaseMe
{
	// Token: 0x060026AB RID: 9899 RVA: 0x0015FDF0 File Offset: 0x0015DFF0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillE", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x0015FE3F File Offset: 0x0015E03F
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x0015FE50 File Offset: 0x0015E050
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool flag = base.hero.playerCommon.m_nHunMi == 0 && base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.hero.bePlayActive = true;
			base.hero.wInput.notInput = false;
		}
	}
}

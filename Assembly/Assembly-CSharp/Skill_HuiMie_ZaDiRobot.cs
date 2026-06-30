using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class Skill_HuiMie_ZaDiRobot : SkillBaseRobot
{
	// Token: 0x06002953 RID: 10579 RVA: 0x0016D680 File Offset: 0x0016B880
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x0016D68C File Offset: 0x0016B88C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x0016D6DA File Offset: 0x0016B8DA
	private IEnumerator delaySkillFinish(object __target)
	{
		bool flag = base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.RealShoot(__target);
		}
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x0016D6F0 File Offset: 0x0016B8F0
	public override void OneSkillFinish()
	{
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		base.OneSkillFinish();
	}
}

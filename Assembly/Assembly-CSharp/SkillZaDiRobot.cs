using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class SkillZaDiRobot : SkillBaseRobot
{
	// Token: 0x06002911 RID: 10513 RVA: 0x0016C982 File Offset: 0x0016AB82
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x0016C98C File Offset: 0x0016AB8C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002913 RID: 10515 RVA: 0x0016C9DA File Offset: 0x0016ABDA
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(3.5f);
		base.hero.SetAnimator("SuerpIng", 0, true, 1);
		bool flag = base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.RealShoot(__target);
		}
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002914 RID: 10516 RVA: 0x0016C9F0 File Offset: 0x0016ABF0
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}
}

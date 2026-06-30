using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class Skill_FaShi_LeiBaoRobot : SkillBaseRobot
{
	// Token: 0x06002947 RID: 10567 RVA: 0x0016D470 File Offset: 0x0016B670
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x0016D47C File Offset: 0x0016B67C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		this.enemyTargets = new List<NetLiving>();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillState", 2, 2, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
		bool needSendToServer = ((RobotAI)base.hero).needSendToServer;
		if (needSendToServer)
		{
			HeroFaShiAIController heroFaShiAIController = (HeroFaShiAIController)((RobotAI)base.hero).robotController;
			this.enemyTargets = heroFaShiAIController.allNetLivings;
			for (int i = 0; i < this.enemyTargets.Count; i++)
			{
				base.RealShoot(this.enemyTargets[i].defaultMesh.transform.position);
			}
			heroFaShiAIController.allNetLivings.Clear();
		}
	}

	// Token: 0x06002949 RID: 10569 RVA: 0x0016D55C File Offset: 0x0016B75C
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600294A RID: 10570 RVA: 0x0016D572 File Offset: 0x0016B772
	public override void BreakSkill()
	{
		base.BreakSkill();
		base.StopAllCoroutines();
		this.OneSkillFinish();
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x0016D58A File Offset: 0x0016B78A
	public override void OneSkillFinish()
	{
		base.hero.ChangeWeaponGroup(0);
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillState", 2, 0, 1);
	}

	// Token: 0x04002329 RID: 9001
	private List<NetLiving> enemyTargets;
}

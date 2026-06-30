using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class HeroTerminatorIt : HeroBaseIt
{
	// Token: 0x06001446 RID: 5190 RVA: 0x000BF881 File Offset: 0x000BDA81
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		Fun.Load_Sound(base.hero, "skillvoice_zhongjiezhe_run", base.hero._playerSoundContainer, false, "Terminator_Sprint_Sound");
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000BF8B0 File Offset: 0x000BDAB0
	public override void SuperSkillDo(object parm)
	{
		bool flag = base.hero.netobject_type == 6;
		if (flag)
		{
			bool flag2 = base.hero.skillEffectDict.ContainsKey(1030);
			if (flag2)
			{
				((SkillSprintRobot)base.hero.skillEffectDict[1030]).OneSkillFinish();
			}
			base.hero.ChangeWeaponGroup(1);
			base.hero.bePlayActive = false;
			base.SuperSkillDo(parm);
			bool flag3 = base.hero.tCharAnimationTps != null;
			if (flag3)
			{
				base.hero.tCharAnimationTps.animator.SetBool("Skill_Musth", true);
			}
			base.StartCoroutine(this.Wait_Super_Skill_Finish());
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x000BF972 File Offset: 0x000BDB72
	private IEnumerator Wait_Super_Skill_Finish()
	{
		bool flag = base.enabled && base.gameObject.activeSelf;
		if (flag)
		{
			bool flag2 = base.hero != null && base.hero.playerDazhao != null && base.hero.playerDazhao.cdazhaomaininfo != null && base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml != null;
			if (flag2)
			{
				yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
			}
			this.SuperSkillFinish();
		}
		yield break;
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x000BF981 File Offset: 0x000BDB81
	public override void SuperSkillFinish()
	{
		base.hero.ChangeWeaponGroup(0);
		base.StartCoroutine(this.Wait_SuperSkillFinish());
		base.hero.wc.notFillCartridge = false;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000BF9AF File Offset: 0x000BDBAF
	private IEnumerator Wait_SuperSkillFinish()
	{
		yield return new WaitForSeconds(0.5f);
		base.SuperSkillFinish();
		base.hero.tCharAnimationTps.animator.SetBool("Skill_Musth", false);
		bool flag = base.hero.netobject_type == 6;
		if (flag)
		{
			((HeroTerminatorAIController)((RobotAI)base.hero).robotController).SuperFinish();
		}
		yield break;
	}
}

using System;
using System.Collections;

// Token: 0x02000317 RID: 791
public class SkillBaoLieJianMe : SkillBaseMe
{
	// Token: 0x06002649 RID: 9801 RVA: 0x0015D2AA File Offset: 0x0015B4AA
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo = 0;
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x0015D2D8 File Offset: 0x0015B4D8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo = 2;
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillBaoLie", 0, false, 1);
		base.hero.bePlayActive = true;
		this.OneSkillFinish();
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x0015D340 File Offset: 0x0015B540
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		if (switchOpen)
		{
			base.hero.SetAnimator("SkillBaoLie", 0, true, 1);
			base.hero.playerSound.PlaySwitchArrowSound(0);
		}
		else
		{
			base.hero.SetAnimator("SkillBaoLie", 0, false, 1);
			base.hero.playerSound.PlaySwitchArrowSound(0);
		}
	}
}

using System;
using System.Collections;

// Token: 0x0200034E RID: 846
public class SkillJiTuiJianMe : SkillBaseMe
{
	// Token: 0x06002743 RID: 10051 RVA: 0x00163067 File Offset: 0x00161267
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo = 0;
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x00163094 File Offset: 0x00161294
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((CWeaponControlMe)base.hero.wc).weapons[1].defaultLauncherNo = 1;
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillJiTui", 0, false, 1);
		base.hero.bePlayActive = true;
		this.OneSkillFinish();
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x001630FC File Offset: 0x001612FC
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		if (switchOpen)
		{
			base.hero.SetAnimator("SkillJiTui", 0, true, 1);
		}
		else
		{
			base.hero.SetAnimator("SkillJiTui", 0, false, 1);
		}
		base.hero.playerSound.PlaySwitchArrowSound(1);
	}
}

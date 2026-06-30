using System;

// Token: 0x0200034D RID: 845
public class SkillJiTuiJianIt : SkillBaseIt
{
	// Token: 0x06002741 RID: 10049 RVA: 0x00163049 File Offset: 0x00161249
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		base.hero.playerSound.PlaySwitchArrowSound(1);
	}
}

using System;

// Token: 0x02000316 RID: 790
public class SkillBaoLieJianIt : SkillBaseIt
{
	// Token: 0x06002647 RID: 9799 RVA: 0x0015D28C File Offset: 0x0015B48C
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		base.hero.playerSound.PlaySwitchArrowSound(0);
	}
}

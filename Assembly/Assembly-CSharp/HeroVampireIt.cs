using System;

// Token: 0x020001C6 RID: 454
public class HeroVampireIt : HeroBaseIt
{
	// Token: 0x06001490 RID: 5264 RVA: 0x000C14AC File Offset: 0x000BF6AC
	public override void OnDeath()
	{
		base.OnDeath();
		bool isCopy = base.hero.isCopy;
		if (isCopy)
		{
			this.skillit.OneSkillFinish();
			this.SuperSkillFinish();
			base.hero.SetAnimatorUpdate("Skill_Musth", 0, false);
			base.hero.ChangeWeaponGroup(0);
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000C150C File Offset: 0x000BF70C
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		bool isCopy = base.hero.isCopy;
		if (isCopy)
		{
			base.hero.bePlayActive = true;
			base.hero.ChangeWeaponGroup(0);
			this.skillit.OneSkillFinish();
			bool flag = base.hero.GetAnimatorFloat("Forward") != 0f;
			if (flag)
			{
				base.hero.SetAnimatorUpdate("Skill_Musth_State", 0, true);
			}
			else
			{
				base.hero.SetAnimatorUpdate("Skill_Musth_State", 0, false);
			}
			base.hero.SetAnimatorUpdate("Skill_Musth", 0, false);
		}
	}

	// Token: 0x04000FE2 RID: 4066
	public Skill_Vampire_Deformation_It skillit;
}

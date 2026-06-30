using System;

// Token: 0x02000181 RID: 385
public class HeroHeiguafuIt : HeroBaseIt
{
	// Token: 0x060011D9 RID: 4569 RVA: 0x000A7920 File Offset: 0x000A5B20
	public override void ChangeWeaponGroup(int xingtai)
	{
		if (xingtai != 1)
		{
			if (xingtai == 2)
			{
				base.hero.ChangeWeaponGroup(2);
			}
		}
		else
		{
			base.hero.ChangeWeaponGroup(1);
		}
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000A795C File Offset: 0x000A5B5C
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
	}
}

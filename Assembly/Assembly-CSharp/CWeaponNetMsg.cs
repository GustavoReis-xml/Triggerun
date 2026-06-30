using System;
using System.Collections;

// Token: 0x020000EB RID: 235
public class CWeaponNetMsg
{
	// Token: 0x06000882 RID: 2178 RVA: 0x00048B36 File Offset: 0x00046D36
	public void WeaponSkillMsg(HeroPlayer player, ArrayList szNetMsg)
	{
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00048B39 File Offset: 0x00046D39
	private void OnSuperData(NetLiving player, ArrayList data)
	{
		player.OnSuperData(data);
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00048B44 File Offset: 0x00046D44
	private void OnUpdateItemNum(HeroPlayer hero, ArrayList szMsg)
	{
		bool flag = hero != null && !(hero.heroSkill == null);
		if (flag)
		{
			hero.heroSkill.OnUpdateItemNum((int)szMsg[0], (int)szMsg[1]);
		}
	}
}

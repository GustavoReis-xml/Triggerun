using System;

// Token: 0x020001BD RID: 445
public class HeroTouXiZheGjIt : HeroBaseIt
{
	// Token: 0x0600145E RID: 5214 RVA: 0x000BFF6C File Offset: 0x000BE16C
	public override void SetRobotState(bool attackState)
	{
		if (attackState)
		{
			base.hero.SetAnimator("movestate", 1, 0f, 1);
		}
		else
		{
			base.hero.SetAnimator("movestate", 1, 1f, 1);
		}
	}
}

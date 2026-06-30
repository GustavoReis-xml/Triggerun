using System;

// Token: 0x020001BE RID: 446
public class HeroTouXiZheIt : HeroBaseIt
{
	// Token: 0x06001460 RID: 5216 RVA: 0x000BFFC8 File Offset: 0x000BE1C8
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

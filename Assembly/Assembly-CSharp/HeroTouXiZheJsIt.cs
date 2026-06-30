using System;

// Token: 0x020001BF RID: 447
public class HeroTouXiZheJsIt : HeroBaseIt
{
	// Token: 0x06001462 RID: 5218 RVA: 0x000C0024 File Offset: 0x000BE224
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

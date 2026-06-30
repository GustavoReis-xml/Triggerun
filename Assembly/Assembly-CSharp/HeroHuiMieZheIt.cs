using System;

// Token: 0x02000188 RID: 392
public class HeroHuiMieZheIt : HeroBaseIt
{
	// Token: 0x06001226 RID: 4646 RVA: 0x000AA341 File Offset: 0x000A8541
	public override void RobotCreateFinish()
	{
		base.RobotCreateFinish();
		((RobotAI)base.hero).robotController.moveSpeed = 2.4f;
		((RobotAI)base.hero).tChar.limitNowDis = 20f;
	}
}

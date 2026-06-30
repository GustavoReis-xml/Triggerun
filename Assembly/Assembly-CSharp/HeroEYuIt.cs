using System;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class HeroEYuIt : HeroBaseIt
{
	// Token: 0x06001180 RID: 4480 RVA: 0x000A3FC0 File Offset: 0x000A21C0
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

	// Token: 0x06001181 RID: 4481 RVA: 0x000A4013 File Offset: 0x000A2213
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		base.hero.SetAnimator("Forward", 1, 1f, 1);
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x000A403C File Offset: 0x000A223C
	protected override void Update()
	{
		base.Update();
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x000A4048 File Offset: 0x000A2248
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		CapsuleCollider component = base.hero.UserControl.GetComponent<CapsuleCollider>();
		component.height = 1.2f;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x000A407A File Offset: 0x000A227A
	public override void RobotCreateFinish()
	{
		base.RobotCreateFinish();
		((RobotAI)base.hero).robotController.moveSpeed = 2.2f;
	}
}

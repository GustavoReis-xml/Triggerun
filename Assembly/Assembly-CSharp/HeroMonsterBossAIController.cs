using System;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class HeroMonsterBossAIController : RobotAIController
{
	// Token: 0x0600138E RID: 5006 RVA: 0x000B9FF2 File Offset: 0x000B81F2
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
		this.useZaDiTimer = 15f;
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000BA014 File Offset: 0x000B8214
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.UseZaDiSkill();
			this.CleanFireCD();
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000BA048 File Offset: 0x000B8248
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			this.noMoveTimer -= Time.deltaTime;
			bool flag2 = this.noMoveTimer <= 0f;
			if (flag2)
			{
				base.Update();
			}
		}
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000BA09F File Offset: 0x000B829F
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000BA0AC File Offset: 0x000B82AC
	public void UseZaDiSkill()
	{
		this.useZaDiTimer -= Time.deltaTime;
		bool flag = this.useZaDiTimer <= 0f && base.UseSkill() && this.attack && this.CurrentTargetDis < 10f;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("Skill1", 1);
			this.fireCD = 3.5f;
			this.useZaDiTimer = 15f;
			this.noMoveTimer = 3.5f;
		}
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x000BA138 File Offset: 0x000B8338
	private void CleanFireCD()
	{
		bool flag = this.fireCD >= 0f;
		if (flag)
		{
			this.fireCD -= Time.deltaTime;
		}
		else
		{
			this.skillIng = false;
		}
	}

	// Token: 0x04000F28 RID: 3880
	private float fireCD = 1f;

	// Token: 0x04000F29 RID: 3881
	private bool superDoing;

	// Token: 0x04000F2A RID: 3882
	private float useZaDiTimer = 15f;

	// Token: 0x04000F2B RID: 3883
	private float noMoveTimer;
}

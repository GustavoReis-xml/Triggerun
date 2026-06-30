using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class HeroValkyriesAIController : RobotAIController
{
	// Token: 0x06001471 RID: 5233 RVA: 0x000C0C73 File Offset: 0x000BEE73
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
		this.useHuDunTimer = 15f;
		this.useHXuanFengZhanTimer = 20f;
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x000C0CA0 File Offset: 0x000BEEA0
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.CleanFireCD();
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000C0CD0 File Offset: 0x000BEED0
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			base.Update();
		}
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000C0CFF File Offset: 0x000BEEFF
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000C0D0C File Offset: 0x000BEF0C
	public void UseHuDunSkill()
	{
		bool flag = this.useHuDunTimer > 0f;
		if (flag)
		{
			this.useHuDunTimer -= Time.deltaTime;
		}
		else
		{
			bool flag2 = base.UseSkill();
			if (flag2)
			{
				base.robot.heroSkill.DoSomeOne("Skill1", 1);
				this.fireCD = 2f;
				this.useHuDunTimer = 15f;
			}
		}
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000C0D7C File Offset: 0x000BEF7C
	public void UseXuanFengZhanSkill()
	{
		bool flag = this.useHXuanFengZhanTimer > 0f;
		if (flag)
		{
			this.useHXuanFengZhanTimer -= Time.deltaTime;
		}
		else
		{
			bool flag2 = base.UseSkill() && this.attack && this.CurrentTargetDis < 8f;
			if (flag2)
			{
				base.robot.heroSkill.DoSomeOne("Skill2", 1);
				this.fireCD = 10f;
				this.useHXuanFengZhanTimer = 20f;
			}
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000C0E04 File Offset: 0x000BF004
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

	// Token: 0x04000FDA RID: 4058
	private float fireCD = 1f;

	// Token: 0x04000FDB RID: 4059
	private bool superDoing;

	// Token: 0x04000FDC RID: 4060
	private float useHuDunTimer = 8f;

	// Token: 0x04000FDD RID: 4061
	private float useHXuanFengZhanTimer = 20f;

	// Token: 0x04000FDE RID: 4062
	private bool setSpeed;
}

using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class HeroYukiOnnaAIController : RobotAIController
{
	// Token: 0x06001532 RID: 5426 RVA: 0x000C81DD File Offset: 0x000C63DD
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000C81F4 File Offset: 0x000C63F4
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.UseElfSkill();
			this.CleanFireCD();
		}
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000C8228 File Offset: 0x000C6428
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			base.Update();
		}
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000C8257 File Offset: 0x000C6457
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000C8264 File Offset: 0x000C6464
	public void UseElfSkill()
	{
		this.useElfSkillTimer -= Time.deltaTime;
		bool flag = this.useElfSkillTimer <= 0f && base.UseSkill() && this.attack;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("Skill2", 1);
			this.fireCD = 2f;
			this.useElfSkillTimer = 11f;
		}
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000C82D8 File Offset: 0x000C64D8
	public void UseTrapSkill()
	{
		this.useTrapSkillTimer -= Time.deltaTime;
		bool flag = this.useTrapSkillTimer <= 0f && base.UseSkill() && this.attack;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("Skill1", 1);
			this.fireCD = 2f;
			this.useTrapSkillTimer = 11f;
		}
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000C834C File Offset: 0x000C654C
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

	// Token: 0x0400105F RID: 4191
	private Animator animator;

	// Token: 0x04001060 RID: 4192
	private float useElfSkillTimer = 11f;

	// Token: 0x04001061 RID: 4193
	private float fireCD = 1f;

	// Token: 0x04001062 RID: 4194
	private float useTrapSkillTimer = 10f;
}

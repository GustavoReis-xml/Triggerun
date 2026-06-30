using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class HeroXingTianAIController : RobotAIController
{
	// Token: 0x060014EE RID: 5358 RVA: 0x000C68DB File Offset: 0x000C4ADB
	public override void Start()
	{
		base.Start();
		this.lieYanQuanTimer = 4f;
		this.setSpeed = false;
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000C68F8 File Offset: 0x000C4AF8
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.UseLieYanQuanSkill();
			this.CleanFireCD();
		}
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000C692C File Offset: 0x000C4B2C
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			base.Update();
		}
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000C695B File Offset: 0x000C4B5B
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000C6968 File Offset: 0x000C4B68
	private void UseLieYanQuanSkill()
	{
		this.lieYanQuanTimer -= Time.deltaTime;
		bool flag = this.lieYanQuanTimer <= 0f && base.UseSkill();
		if (flag)
		{
			this.lieYanQuanTimer = 4f;
			base.robot.heroSkill.DoSomeOne("Skill2", 1);
		}
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000C69C8 File Offset: 0x000C4BC8
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

	// Token: 0x0400103B RID: 4155
	private float lieYanQuanTimer = 4f;

	// Token: 0x0400103C RID: 4156
	private float cantAttack = 1f;

	// Token: 0x0400103D RID: 4157
	private float fireCD = 1f;

	// Token: 0x0400103E RID: 4158
	private bool setSpeed;
}

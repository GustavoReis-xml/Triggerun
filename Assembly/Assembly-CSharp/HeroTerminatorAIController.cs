using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class HeroTerminatorAIController : RobotAIController
{
	// Token: 0x0600143A RID: 5178 RVA: 0x000BF4BA File Offset: 0x000BD6BA
	public override void Start()
	{
		base.Start();
		this.b_skillRun = false;
		this.HitRecover = 0.2f;
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000BF4D8 File Offset: 0x000BD6D8
	public override void Update()
	{
		bool flag = !base.robot.needSendToServer;
		if (!flag)
		{
			bool flag2 = this.SuperHitRecover > 0f;
			if (flag2)
			{
				this.SuperHitRecover -= Time.deltaTime;
			}
			else
			{
				bool flag3 = this.treatmentTimer > 0f;
				if (flag3)
				{
					this.treatmentTimer -= Time.deltaTime;
				}
				else
				{
					this.Move();
					bool flag4 = !this.superDoing;
					if (flag4)
					{
						this.UseBombSkill();
						this.UseTreatmentSkill();
					}
					this.CleanFireCD();
					this.DoSuper();
				}
			}
		}
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000BF57C File Offset: 0x000BD77C
	private void Move()
	{
		base.Update();
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			this.UseSkillMove();
		}
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
	private void UseSkillMove()
	{
		bool flag = !this.superDoing && base.UseSkill();
		if (flag)
		{
			bool flag2 = !this.attack && this.chaseTarget == null && base.RobotHasPath && this.currentStayTimer <= 0f;
			if (flag2)
			{
				this.SkillRun();
			}
			else
			{
				this.skillIng = false;
				this.b_skillRun = false;
			}
		}
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x000BF620 File Offset: 0x000BD820
	private void SkillRun()
	{
		bool flag = !this.b_skillRun && base.robot.beMonster == 0;
		if (flag)
		{
			this.b_skillRun = true;
			base.robot.heroSkill.DoSomeOne("Skill1", 1);
		}
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x000BF66B File Offset: 0x000BD86B
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x000BF678 File Offset: 0x000BD878
	public void UseBombSkill()
	{
		this.useBombTimer -= Time.deltaTime;
		bool flag = this.useBombTimer <= 0f && base.UseSkill() && this.attack;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("WeaponSec", 1);
			this.fireCD = 1f;
			this.useBombTimer = 10f;
		}
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x000BF6EC File Offset: 0x000BD8EC
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

	// Token: 0x06001442 RID: 5186 RVA: 0x000BF730 File Offset: 0x000BD930
	private void UseTreatmentSkill()
	{
		bool flag = !this.attack && base.robot.playerCommon.GetHpBiLi() <= 0.5f && base.robot.beMonster <= 0 && base.UseSkill();
		if (flag)
		{
			this.treatmentTimer = 1.5f;
			base.robot.heroSkill.DoSomeOne("Skill2", 1);
		}
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x000BF79C File Offset: 0x000BD99C
	private void DoSuper()
	{
		bool flag = base.robot.beMonster <= 0 && base.robot.SyncNetObj != null && !this.superDoing && this.attack && base.robot.superSkill(null);
		if (flag)
		{
			this.weaponFireName = 3;
			this.SuperHitRecover = 1f;
			this.HitRecover = 0f;
			this.superDoing = true;
			base.robot.wc.notFillCartridge = true;
		}
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x000BF81F File Offset: 0x000BDA1F
	public void SuperFinish()
	{
		this.superDoing = false;
		this.HitRecover = 0.2f;
		this.SuperHitRecover = 1f;
		this.weaponFireName = 1;
		base.robot.wc.notFillCartridge = false;
	}

	// Token: 0x04000FA4 RID: 4004
	private Animator animator;

	// Token: 0x04000FA5 RID: 4005
	private bool b_skillRun;

	// Token: 0x04000FA6 RID: 4006
	private float useBombTimer = 10f;

	// Token: 0x04000FA7 RID: 4007
	private float fireCD = 1f;

	// Token: 0x04000FA8 RID: 4008
	private float treatmentTimer;

	// Token: 0x04000FA9 RID: 4009
	private float SuperHitRecover = 0.5f;

	// Token: 0x04000FAA RID: 4010
	private bool superDoing;
}

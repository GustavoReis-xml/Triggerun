using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class HeroXingJiLieRenAIController : RobotAIController
{
	// Token: 0x060014C6 RID: 5318 RVA: 0x000C5922 File Offset: 0x000C3B22
	public override void Start()
	{
		base.Start();
		this.b_skillShanXian = false;
		this.HitRecover = 0.2f;
		this.SyncPosSelf = false;
		this.superDoing = false;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000C594C File Offset: 0x000C3B4C
	public override void Update()
	{
		bool flag = base.robot.SyncNetObj != null && base.robot.needSendToServer;
		if (flag)
		{
			this.Move();
			bool flag2 = this.syncPosTimer >= 0f;
			if (flag2)
			{
				this.syncPosTimer -= Time.deltaTime;
			}
			else
			{
				bool syncPosSelf = this.SyncPosSelf;
				if (syncPosSelf)
				{
					this.SyncPosSelf = false;
				}
			}
			bool flag3 = this.ShanXianCD > 0f;
			if (flag3)
			{
				this.ShanXianCD -= Time.deltaTime;
			}
			bool flag4 = !this.superDoing;
			if (flag4)
			{
				this.UseSuSheSkill();
			}
			this.CleanFireCD();
			this.DoSuper();
			this.ReleaseSuper();
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000C5A14 File Offset: 0x000C3C14
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			bool flag2 = !this.superDoing;
			if (flag2)
			{
				this.UseShanXianMove();
			}
			base.Update();
		}
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x000C5A5C File Offset: 0x000C3C5C
	private void UseShanXianMove()
	{
		bool flag = base.UseSkill();
		if (flag)
		{
			bool flag2 = this.ShanXianCD <= 0f;
			if (flag2)
			{
				this.b_skillShanXian = false;
			}
			bool flag3 = !this.attack;
			if (flag3)
			{
				this.SkillShanXian();
			}
			else
			{
				this.skillIng = false;
			}
		}
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
	private void SkillShanXian()
	{
		Debug.DrawRay(base.robot.UserControl.transform.position + Vector3.up, base.robot.UserControl.transform.forward);
		bool flag = !Physics.Raycast(base.robot.UserControl.transform.position + Vector3.up, base.robot.UserControl.transform.forward, 13f) && !this.b_skillShanXian;
		if (flag)
		{
			this.ShanXianCD = 5f;
			this.b_skillShanXian = true;
			this.SyncPosSelf = true;
			this.syncPosTimer = 0.5f;
			base.robot.heroSkill.DoSomeOne("Skill2", 1);
			Vector3 forward = base.robot.UserControl.transform.forward;
			forward.y = 0f;
			this.SyncPos = base.robot.UserControl.transform.position + forward * 10f;
		}
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x000C5BD9 File Offset: 0x000C3DD9
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000C5BE4 File Offset: 0x000C3DE4
	public void UseSuSheSkill()
	{
		this.useSuSheTimer -= Time.deltaTime;
		bool flag = this.useSuSheTimer <= 0f && base.UseSkill() && this.attack && this.CurrentTargetDis < 12f;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("Skill1", 1);
			this.fireCD = 2f;
			this.useSuSheTimer = 8f;
		}
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000C5C64 File Offset: 0x000C3E64
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

	// Token: 0x060014CE RID: 5326 RVA: 0x000C5CA5 File Offset: 0x000C3EA5
	public override void SyncPosition()
	{
		base.SyncPosition();
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000C5CB0 File Offset: 0x000C3EB0
	public override void Fire()
	{
		bool flag = base.robot.wc != null && !this.skillIng && !this.superDoing;
		if (flag)
		{
			int num = ((CWeaponControlMe)base.robot.wc).WeaponFire(this.weaponFireName, "0", this.chaseTarget.defaultMesh.transform.position, this.chaseTarget.defaultMesh);
			bool flag2 = num == 1;
			if (flag2)
			{
				this.hitRecoverCD = this.HitRecover;
			}
		}
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000C5D44 File Offset: 0x000C3F44
	private void DoSuper()
	{
		bool flag = base.robot.SyncNetObj != null && !this.superDoing && this.attack && this.CurrentTargetDis <= 10f && base.robot.superSkill(null);
		if (flag)
		{
			this.HitRecover = 0f;
			this.superDoing = true;
			base.robot.playerDazhao.doShoot(base.robot);
		}
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000C5DBC File Offset: 0x000C3FBC
	private void ReleaseSuper()
	{
		bool flag = !this.superDoing;
		if (!flag)
		{
			bool flag2 = base.robot.playerDazhao.UseGun(base.robot);
			if (flag2)
			{
				base.robot.playerDazhao.doShoot(base.robot);
			}
			else
			{
				this.superDoing = false;
				base.robot.tChar.m_MoveByAnimator = true;
				bool flag3 = !base.robot.isDead;
				if (flag3)
				{
					base.robot.SetAnimator("SkillSuper", 0, false, 1);
					this.superDoing = false;
				}
			}
		}
	}

	// Token: 0x04001029 RID: 4137
	private Animator animator;

	// Token: 0x0400102A RID: 4138
	private bool b_skillShanXian;

	// Token: 0x0400102B RID: 4139
	private float useSuSheTimer = 8f;

	// Token: 0x0400102C RID: 4140
	private float fireCD = 1f;

	// Token: 0x0400102D RID: 4141
	private bool SyncPosSelf;

	// Token: 0x0400102E RID: 4142
	private float syncPosTimer = 0.5f;

	// Token: 0x0400102F RID: 4143
	private bool superDoing;

	// Token: 0x04001030 RID: 4144
	private float ShanXianCD = 5f;
}

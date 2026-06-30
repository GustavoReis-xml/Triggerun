using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class HeroHuiMieZheAIController : RobotAIController
{
	// Token: 0x0600121A RID: 4634 RVA: 0x000A9EBB File Offset: 0x000A80BB
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
		this.useZaDiTimer = 10f;
		this.useZhuangRenTimer = 10f;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000A9EE6 File Offset: 0x000A80E6
	public override void InitRobotControllerData()
	{
		base.InitRobotControllerData();
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000A9EF0 File Offset: 0x000A80F0
	public override void RobotHaveCreate()
	{
		base.RobotHaveCreate();
		this.boxcol = base.robot.userBip.GetComponent<BoxCollider>();
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x000A9F10 File Offset: 0x000A8110
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.UseZaDiSkill();
			this.UseZhuangRenSkill();
			this.CleanFireCD();
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000A9F4C File Offset: 0x000A814C
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

	// Token: 0x0600121F RID: 4639 RVA: 0x000A9FA3 File Offset: 0x000A81A3
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000A9FB0 File Offset: 0x000A81B0
	public void UseZaDiSkill()
	{
		bool flag = this.canReleaseZaDi && base.UseSkill() && this.chaseTarget != null;
		if (flag)
		{
			float num = Vector3.Distance(this.chaseTarget.UserControl.transform.position, base.transform.position);
			bool flag2 = num < 3f;
			if (flag2)
			{
				this.releaseZadiSkill();
			}
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000AA01C File Offset: 0x000A821C
	private void releaseZadiSkill()
	{
		base.robot.heroSkill.DoSomeOne("Skill1", 1);
		this.fireCD = 3.5f;
		this.useZaDiTimer = 10f;
		this.noMoveTimer = 3.5f;
		this.b_moveAsDash = false;
		base.MoveAsDashFinish();
		this.canReleaseZaDi = false;
		bool flag = base.robot.skillEffectDict.ContainsKey(2006);
		if (flag)
		{
			((SkillZhuangRenRobot)base.robot.skillEffectDict[2006]).BreakSkill();
		}
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000AA0B4 File Offset: 0x000A82B4
	public void UseZhuangRenSkill()
	{
		bool releasedZR = this.releasedZR;
		if (releasedZR)
		{
			this.XuLiTimer -= Time.deltaTime;
			bool flag = this.XuLiTimer <= 0f;
			if (flag)
			{
				this.b_moveAsDash = true;
				this.releasedZR = false;
				this.canReleaseZaDi = true;
			}
		}
		bool b_moveAsDash = this.b_moveAsDash;
		if (b_moveAsDash)
		{
			this.DashTimer -= Time.deltaTime;
			bool flag2 = this.DashTimer <= 0f;
			if (flag2)
			{
				this.b_moveAsDash = false;
				bool flag3 = this.canReleaseZaDi;
				if (flag3)
				{
					this.releaseZadiSkill();
				}
				this.canReleaseZaDi = false;
			}
		}
		this.useZhuangRenTimer -= Time.deltaTime;
		bool flag4 = this.useZhuangRenTimer <= 0f && base.UseSkill();
		if (flag4)
		{
			bool flag5 = this.CurrentTargetDis < 5f;
			if (flag5)
			{
				this.releaseZadiSkill();
				this.useZhuangRenTimer = 19.5f;
			}
			else
			{
				bool flag6 = this.CurrentTargetDis < 50f;
				if (flag6)
				{
					base.robot.heroSkill.DoSomeOne("Skill2", 1);
					this.fireCD = 5f;
					this.useZhuangRenTimer = 19.5f;
					this.XuLiTimer = 1f;
					this.DashTimer = 3f;
					this.releasedZR = true;
					this.b_moveAsDash = false;
				}
			}
		}
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000AA224 File Offset: 0x000A8424
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

	// Token: 0x06001224 RID: 4644 RVA: 0x000AA268 File Offset: 0x000A8468
	private void OnTriggerEnter(Collider _Collider)
	{
		bool flag = this.canReleaseZaDi && _Collider.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet_1")) != 0 && _Collider.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet_2")) != 0 && _Collider.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet")) != 0;
		if (flag)
		{
			this.DashTimer = 0f;
			this.b_moveAsDash = false;
			this.releaseZadiSkill();
			this.canReleaseZaDi = false;
			this.releasedZR = false;
			this.XuLiTimer = 0f;
		}
	}

	// Token: 0x04000E54 RID: 3668
	private float fireCD = 1f;

	// Token: 0x04000E55 RID: 3669
	private bool superDoing;

	// Token: 0x04000E56 RID: 3670
	private float useZaDiTimer = 15f;

	// Token: 0x04000E57 RID: 3671
	private float useZhuangRenTimer = 15f;

	// Token: 0x04000E58 RID: 3672
	private float noMoveTimer;

	// Token: 0x04000E59 RID: 3673
	private BoxCollider boxcol;

	// Token: 0x04000E5A RID: 3674
	private bool canReleaseZaDi;

	// Token: 0x04000E5B RID: 3675
	private float DashTimer;

	// Token: 0x04000E5C RID: 3676
	private float XuLiTimer;
}

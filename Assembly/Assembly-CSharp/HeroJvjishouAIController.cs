using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class HeroJvjishouAIController : RobotAIController
{
	// Token: 0x060012B7 RID: 4791 RVA: 0x000B3DDE File Offset: 0x000B1FDE
	public override void Start()
	{
		this.weaponFireName = 2;
		this.HitRecover = 1f;
		base.Start();
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000B3DFC File Offset: 0x000B1FFC
	public override void Update()
	{
		bool flag = base.robot == null || base.robot.SyncNetObj == null || fUIManager.GetInstance().GetNowGameState() <= 0;
		if (!flag)
		{
			bool flag2 = this.transTimer > 0f;
			if (flag2)
			{
				this.transTimer -= Time.deltaTime;
				this.SyncPosition();
			}
			else
			{
				bool flag3 = !this.setAgentEnable;
				if (flag3)
				{
					bool needSendToServer = base.robot.needSendToServer;
					if (needSendToServer)
					{
						base.GetCTPos();
					}
					this.setAgentEnable = true;
					base.RobotHasPath = true;
				}
				bool needSendToServer2 = base.robot.needSendToServer;
				if (needSendToServer2)
				{
					this.Move();
				}
			}
		}
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x000B3EB8 File Offset: 0x000B20B8
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			this.skillTransTimer -= Time.deltaTime;
			bool flag2 = this.needTrans && !this.skillIng && this.chaseTarget == null && this.skillTransTimer <= 0f && !this.attack;
			if (flag2)
			{
				this.UseTranspostSkill();
			}
			else
			{
				base.Update();
			}
		}
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x000B3F3C File Offset: 0x000B213C
	private void UseTranspostSkill()
	{
		bool flag = base.UseSkill() && Vector3.Distance(this.transprotPos, base.robot.UserControl.transform.position) < 50f;
		if (flag)
		{
			base.robot.heroSkill.skillsDict[1007].ReadyKeyOk = true;
			base.robot.heroSkill.DoSomeOne("Skill2", 1);
			((SkillTransportRobot)base.robot.skillEffectDict[1007]).transPortPos = this.transprotPos;
			this.SyncPos = this.transprotPos;
			this.skillTransTimer = 10f;
		}
		else
		{
			base.RobotHasPath = true;
			this.skillTransTimer = 2f;
		}
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x000B400C File Offset: 0x000B220C
	private IEnumerator gotoPlace()
	{
		yield return new WaitForSeconds(1.5f);
		base.robot.tCharAnimationTps.animator.SetBool("SkillTPIng", false);
		base.robot.tCharAnimationTps.animator.SetBool("FreezeBreak", true);
		this.SyncPosition();
		yield return new WaitForSeconds(1.5f);
		base.robot.tCharAnimationTps.animator.SetBool("SkillTPIng", false);
		this.skillIng = false;
		yield break;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x000B401C File Offset: 0x000B221C
	public override void SyncPosition()
	{
		bool flag = !base.robot.needSendToServer;
		if (!flag)
		{
			bool flag2 = !this.skillIng;
			if (flag2)
			{
				base.SyncPosition();
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				bool flag3 = Vector3.Distance(base.robot.UserControl.transform.position, this.SyncPos) >= 0.1f;
				if (flag3)
				{
					arrayList2.Add((int)(this.SyncPos.x * 100f));
					arrayList2.Add((int)(this.SyncPos.y * 100f));
					arrayList2.Add((int)(this.SyncPos.z * 100f));
					arrayList.Add(arrayList2);
					arrayList.Add(base.robot.isDead ? 1 : 0);
					arrayList.Add(base.robot.getNetid());
					this.skillIng = false;
					base.RobotHasPath = true;
					this.Agent.updatePosition = false;
					this.Agent.nextPosition = this.transprotPos;
					this.Agent.enabled = false;
					this.transTimer = 2f;
					this.setAgentEnable = false;
					bool flag4 = base.robot.SyncNetObj != null;
					if (flag4)
					{
						base.robot.SyncNetObj.TosFun("SSP", new object[]
						{
							arrayList
						});
					}
				}
			}
		}
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x000B41B6 File Offset: 0x000B23B6
	public override void Attack()
	{
		base.Attack();
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x000B41C0 File Offset: 0x000B23C0
	public override void ChangeWeaponGround()
	{
		base.robot.ChangeWeaponGroup(1);
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x000B41D0 File Offset: 0x000B23D0
	public override void InitRobotControllerData()
	{
		base.InitRobotControllerData();
		this.skillTransTimer = 5f;
	}

	// Token: 0x04000EB9 RID: 3769
	private float transTimer;

	// Token: 0x04000EBA RID: 3770
	private Animator animator;

	// Token: 0x04000EBB RID: 3771
	private bool setAgentEnable = true;

	// Token: 0x04000EBC RID: 3772
	private float skillTransTimer = 1f;
}

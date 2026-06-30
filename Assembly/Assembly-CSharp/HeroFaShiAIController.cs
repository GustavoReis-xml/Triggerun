using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class HeroFaShiAIController : RobotAIController
{
	// Token: 0x06001186 RID: 4486 RVA: 0x000A40A7 File Offset: 0x000A22A7
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x000A40BC File Offset: 0x000A22BC
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.CleanFireCD();
			this.UseBiaoJiSkill();
			this.UseStormSkill();
		}
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000A40F8 File Offset: 0x000A22F8
	public void UseYuanChengSkill()
	{
		bool flag = base.UseSkill() && this.attack;
		if (flag)
		{
			base.robot.heroSkill.DoSomeOne("Skill1", 1);
			this.fireCD = 3.5f;
			this.useYuanChengTimer = 10f;
			this.noMoveTimer = 3.5f;
			this.b_moveAsDash = false;
			base.MoveAsDashFinish();
		}
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000A4164 File Offset: 0x000A2364
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

	// Token: 0x0600118A RID: 4490 RVA: 0x000A41BC File Offset: 0x000A23BC
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

	// Token: 0x0600118B RID: 4491 RVA: 0x000A4200 File Offset: 0x000A2400
	public void UseBiaoJiSkill()
	{
		this.useBiaoJiSkillTimer -= Time.deltaTime;
		bool flag = this.useBiaoJiSkillTimer <= 0f && base.UseSkill();
		if (flag)
		{
			this.GetEnemyScreenTarget();
			bool attack = this.attack;
			if (attack)
			{
				base.robot.heroSkill.DoSomeOne("Skill1", 1);
				this.fireCD = 2f;
				this.useBiaoJiSkillTimer = 4f;
			}
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000A427C File Offset: 0x000A247C
	public void UseStormSkill()
	{
		this.useStormSkillTimer -= Time.deltaTime;
		bool flag = this.useStormSkillTimer <= 0f && base.UseSkill();
		if (flag)
		{
			this.FindMuBiao();
			bool flag2 = this.allNetLivings.Count > 0;
			if (flag2)
			{
				base.robot.heroSkill.DoSomeOne("Skill2", 1);
				this.fireCD = 1f;
				this.useStormSkillTimer = 10f;
			}
		}
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000A4300 File Offset: 0x000A2500
	private void GetEnemyScreenTarget()
	{
		bool flag = this.chaseTarget != null && this.TargetInSight;
		if (flag)
		{
			this.enemyTarget = this.chaseTarget;
			base.robot.screentarget.NetTarget = this.enemyTarget;
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000A4348 File Offset: 0x000A2548
	private void FindMuBiao()
	{
		this.allNetLivings.Clear();
		this.GetAttackMemberNum();
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000A4360 File Offset: 0x000A2560
	private void GetAttackMemberNum()
	{
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(position, this.eyeRange, this.layer);
		for (int i = 0; i < array.Length; i++)
		{
			this.tempTarget = null;
			bool flag = (this.layer == LayerMask.NameToLayer("force2_npc") || this.layer == LayerMask.NameToLayer("force1_npc")) && array[i].gameObject.tag.CompareTo("can_damage_robot") != 0;
			if (flag)
			{
				this.tempTarget = null;
			}
			else
			{
				GameObject gameObject = array[i].gameObject.transform.root.gameObject;
				bool flag2 = this.goRoot.IndexOf(gameObject) != -1;
				if (!flag2)
				{
					this.goRoot.Add(gameObject);
					PublicInfo component = gameObject.GetComponent<PublicInfo>();
					bool flag3 = component != null;
					if (flag3)
					{
						NetLiving netObject = component.NetObject;
						bool flag4 = !netObject.isDead;
						if (flag4)
						{
							float d = 0.1f;
							bool flag5 = base.robot.tChar.transform.position.y - array[i].transform.position.y > 5f;
							if (flag5)
							{
								d = 1f;
							}
							bool flag6 = Physics.Raycast(base.robot.tChar.transform.position + Vector3.up * d, array[i].transform.position - base.robot.tChar.transform.position, Vector3.Distance(base.robot.tChar.transform.position, array[i].transform.position), this.pullLayer2);
							if (flag6)
							{
								this.tempTarget = null;
								goto IL_2B4;
							}
							bool flag7 = this.AttackRange < 6f && netObject.netobject_type == 2;
							if (flag7)
							{
								bool flag8 = ((Npc_Entity)netObject).Physics == 0;
								if (flag8)
								{
									this.tempTarget = null;
									goto IL_2B4;
								}
							}
							else
							{
								bool flag9 = this.AttackRange >= 10f && netObject.netobject_type == 2 && ((Npc_Entity)netObject).Type != 1;
								if (flag9)
								{
									this.tempTarget = null;
									goto IL_2B4;
								}
							}
							bool flag10 = !netObject.heroBase.IsHeroInvi();
							if (flag10)
							{
								this.tempTarget = netObject;
							}
						}
					}
					bool flag11 = this.tempTarget != null;
					if (flag11)
					{
						this.allNetLivings.Add(this.tempTarget);
					}
				}
			}
			IL_2B4:;
		}
		this.goRoot.Clear();
	}

	// Token: 0x04000DE2 RID: 3554
	private float noMoveTimer;

	// Token: 0x04000DE3 RID: 3555
	private float fireCD = 1f;

	// Token: 0x04000DE4 RID: 3556
	private float useYuanChengTimer = 15f;

	// Token: 0x04000DE5 RID: 3557
	private Animator animator;

	// Token: 0x04000DE6 RID: 3558
	private float useBiaoJiSkillTimer = 4f;

	// Token: 0x04000DE7 RID: 3559
	private float useHealSkillTimer = 3f;

	// Token: 0x04000DE8 RID: 3560
	private NetLiving enemyTarget;

	// Token: 0x04000DE9 RID: 3561
	private HeroPlayer heroplayer;

	// Token: 0x04000DEA RID: 3562
	private NetLiving healTarget;

	// Token: 0x04000DEB RID: 3563
	private bool superDoing;

	// Token: 0x04000DEC RID: 3564
	private float useStormSkillTimer = 10f;

	// Token: 0x04000DED RID: 3565
	private List<GameObject> goRoot = new List<GameObject>();

	// Token: 0x04000DEE RID: 3566
	public List<NetLiving> allNetLivings = new List<NetLiving>();

	// Token: 0x04000DEF RID: 3567
	private NetLiving tempTarget;
}

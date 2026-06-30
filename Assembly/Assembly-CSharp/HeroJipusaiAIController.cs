using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class HeroJipusaiAIController : RobotAIController
{
	// Token: 0x06001299 RID: 4761 RVA: 0x000B2E39 File Offset: 0x000B1039
	public override void Start()
	{
		base.Start();
		this.HitRecover = 0.2f;
		this.enemyTarget = null;
		CScreenTarget.InitScreenTarget(base.robot, "TargetCenterOne");
		this.heroplayer = fGameManager.GetInstance().getPlayer();
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x000B2E78 File Offset: 0x000B1078
	public override void Update()
	{
		bool needSendToServer = base.robot.needSendToServer;
		if (needSendToServer)
		{
			this.Move();
			this.UseBiaoJiSkill();
			this.UseHealSkill();
			this.CleanFireCD();
			this.DoSuper();
			this.ReleaseSuper();
		}
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000B2EC4 File Offset: 0x000B10C4
	private void Move()
	{
		bool flag = !(base.robot.tCharAnimationTps == null);
		if (flag)
		{
			base.Update();
		}
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x000B2EF4 File Offset: 0x000B10F4
	public override void Attack()
	{
		bool flag = !this.superDoing;
		if (flag)
		{
			base.Attack();
		}
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x000B2F18 File Offset: 0x000B1118
	public void UseBiaoJiSkill()
	{
		this.useBiaoJiSkillTimer -= Time.deltaTime;
		bool flag = this.useBiaoJiSkillTimer <= 0f && base.UseSkill();
		if (flag)
		{
			this.GetEnemyScreenTarget();
			bool flag2 = this.attack && this.canReleaseBiaoJiSkill();
			if (flag2)
			{
				base.robot.heroSkill.DoSomeOne("Skill2", 1);
				this.fireCD = 2f;
				this.useBiaoJiSkillTimer = 4f;
			}
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000B2FA0 File Offset: 0x000B11A0
	public void UseHealSkill()
	{
		this.useHealSkillTimer -= Time.deltaTime;
		bool flag = this.useHealSkillTimer <= 0f && base.UseSkill();
		if (flag)
		{
			this.GetFriendScreenTarget();
			bool flag2 = this.canReleaseHealSkill();
			if (flag2)
			{
				base.robot.heroSkill.DoSomeOne("Skill1", 1);
				this.fireCD = 2f;
				this.useHealSkillTimer = 4f;
			}
		}
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000B301C File Offset: 0x000B121C
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

	// Token: 0x060012A0 RID: 4768 RVA: 0x000B3060 File Offset: 0x000B1260
	private void GetFriendScreenTarget()
	{
		this.GetHealTarget();
		bool flag = this.healTarget != null;
		if (flag)
		{
			base.robot.screentarget.NetTarget = this.healTarget;
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000B309C File Offset: 0x000B129C
	private void GetEnemyScreenTarget()
	{
		bool flag = this.chaseTarget != null && this.TargetInSight;
		if (flag)
		{
			this.enemyTarget = this.chaseTarget;
			base.robot.screentarget.NetTarget = this.enemyTarget;
		}
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000B30E4 File Offset: 0x000B12E4
	private bool canReleaseBiaoJiSkill()
	{
		bool flag = this.enemyTarget != null;
		if (flag)
		{
			bool flag2 = this.enemyTarget.getNetid() == this.heroplayer.getNetid();
			if (flag2)
			{
				bool isBuff = this.heroplayer.playerBuffMe.GetIsBuff(1010);
				return !isBuff;
			}
			bool bePlayer = this.enemyTarget.bePlayer;
			if (bePlayer)
			{
				bool isBuff2 = ((GamePlayer)this.enemyTarget).playerBuffIt.GetIsBuff(1010);
				return !isBuff2;
			}
		}
		return false;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000B3180 File Offset: 0x000B1380
	private bool canReleaseHealSkill()
	{
		bool flag = this.healTarget != null;
		bool result;
		if (flag)
		{
			bool flag2 = this.healTarget.getNetid() == this.heroplayer.getNetid();
			if (flag2)
			{
				bool isBuff = this.heroplayer.playerBuffMe.GetIsBuff(1021);
				result = !isBuff;
			}
			else
			{
				bool flag3 = this.healTarget.netobject_type != 6;
				if (flag3)
				{
					bool isBuff2 = ((GamePlayer)this.healTarget).playerBuffIt.GetIsBuff(1021);
					result = !isBuff2;
				}
				else
				{
					bool isBuff3 = ((RobotAI)this.healTarget).playerBuffIt.GetIsBuff(1021);
					result = !isBuff3;
				}
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x000B3250 File Offset: 0x000B1450
	private void GetHealTarget()
	{
		this.healTarget = null;
		int num = (base.robot.playerCommon.m_nShili != 2) ? (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_mainplayer")) : (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
		int layerMask = 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2");
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(position, 100f, num);
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = (num == LayerMask.NameToLayer("force2_npc") || num == LayerMask.NameToLayer("force1_npc")) && array[i].gameObject.tag.CompareTo("can_damage_robot") != 0;
			if (flag)
			{
				this.healTarget = null;
			}
			else
			{
				GameObject gameObject = array[i].gameObject.transform.root.gameObject;
				bool flag2 = this.goRoot.IndexOf(gameObject) != -1;
				if (!flag2)
				{
					this.goRoot.Add(gameObject);
					PublicInfo component = gameObject.GetComponent<PublicInfo>();
					bool flag3 = !(component != null);
					if (!flag3)
					{
						NetLiving netObject = component.NetObject;
						bool flag4 = netObject.isDead || netObject.getNetid() == base.robot.getNetid() || Physics.Raycast(base.robot.tChar.transform.position + Vector3.up * 0.7f, array[i].transform.position - base.robot.tChar.transform.position, Vector3.Distance(base.robot.tChar.transform.position, array[i].transform.position), layerMask);
						if (!flag4)
						{
							Vector3 from = base.robot.tChar.transform.forward;
							from.y = 0f;
							from = from.normalized;
							Vector3 normalized = (array[i].transform.position - base.robot.UserControl.transform.position).normalized;
							normalized.y = 0f;
							normalized = normalized.normalized;
							bool flag5 = Vector3.Angle(from, normalized) > 70f;
							if (!flag5)
							{
								bool flag6 = this.healTarget != null;
								if (flag6)
								{
									bool flag7 = Physics.Raycast(base.robot.tChar.transform.position + Vector3.up * 0.7f, this.healTarget.UserControl.transform.position - base.robot.tChar.transform.position, Vector3.Distance(base.robot.tChar.transform.position, this.healTarget.UserControl.transform.position), layerMask);
									if (flag7)
									{
										goto IL_3BE;
									}
									bool flag8 = netObject.playerCommon.GetNowHp() < this.healTarget.playerCommon.GetNowHp();
									if (flag8)
									{
										this.healTarget = netObject;
										goto IL_3BE;
									}
								}
								this.healTarget = netObject;
							}
						}
					}
				}
			}
			IL_3BE:;
		}
		this.goRoot.Clear();
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000B3640 File Offset: 0x000B1840
	private void DoSuper()
	{
		bool flag = base.robot.SyncNetObj != null && !this.superDoing && (base.robot.playerCommon.GetHpBiLi() <= 0.8f || (this.healTarget != null && base.robot.UserControl != null && this.healTarget.UserControl != null && Vector3.Distance(this.healTarget.UserControl.transform.position, base.robot.UserControl.transform.position) <= 10f)) && base.robot.tChar != null && base.robot.superSkill(null);
		if (flag)
		{
			this.HitRecover = 0f;
			this.superDoing = true;
			base.robot.tChar.m_MoveByAnimator = false;
			this.superReleaseDelay = 1f;
		}
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x000B373C File Offset: 0x000B193C
	private void ReleaseSuper()
	{
		bool flag = !this.superDoing;
		if (!flag)
		{
			bool flag2 = this.superReleaseDelay > 0f;
			if (flag2)
			{
				this.superReleaseDelay -= Time.deltaTime;
			}
			else
			{
				bool flag3 = base.robot.playerDazhao.UseGun(base.robot);
				if (flag3)
				{
					base.robot.playerDazhao.doShoot(base.robot);
				}
				else
				{
					this.superDoing = false;
					base.robot.tChar.m_MoveByAnimator = true;
					bool flag4 = !base.robot.isDead;
					if (flag4)
					{
						base.robot.tCharAnimationTps.animator.SetBool("SkillSuper", false);
						this.superDoing = false;
					}
				}
			}
		}
	}

	// Token: 0x04000EAD RID: 3757
	private Animator animator;

	// Token: 0x04000EAE RID: 3758
	private float useBiaoJiSkillTimer = 4f;

	// Token: 0x04000EAF RID: 3759
	private float fireCD = 1f;

	// Token: 0x04000EB0 RID: 3760
	private float useHealSkillTimer = 3f;

	// Token: 0x04000EB1 RID: 3761
	private NetLiving enemyTarget;

	// Token: 0x04000EB2 RID: 3762
	private HeroPlayer heroplayer;

	// Token: 0x04000EB3 RID: 3763
	private NetLiving healTarget;

	// Token: 0x04000EB4 RID: 3764
	private bool superDoing;

	// Token: 0x04000EB5 RID: 3765
	private List<GameObject> goRoot = new List<GameObject>();

	// Token: 0x04000EB6 RID: 3766
	private float superReleaseDelay = 0.5f;
}

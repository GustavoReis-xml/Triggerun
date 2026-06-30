using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class SkillBaseRobot : SkillBase
{
	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x06002670 RID: 9840 RVA: 0x0015DD00 File Offset: 0x0015BF00
	protected CPSkillStruct heroSkillData
	{
		get
		{
			bool flag = this._heroSkillData == null;
			if (flag)
			{
				this._heroSkillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
			}
			return this._heroSkillData;
		}
	}

	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x06002671 RID: 9841 RVA: 0x0015DD43 File Offset: 0x0015BF43
	protected NetLiving hero
	{
		get
		{
			return this._hero;
		}
	}

	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x06002672 RID: 9842 RVA: 0x0015DD4C File Offset: 0x0015BF4C
	public bool bePlaying
	{
		get
		{
			CPSkillStruct skillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
			bool flag = skillData == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = skillData.beginTime < Fun.GetNow(this.hero);
				result = !flag2;
			}
			return result;
		}
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x0015DDA2 File Offset: 0x0015BFA2
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this._heroSkillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x0015DDC8 File Offset: 0x0015BFC8
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool flag = this.heroSkillData == null;
		if (!flag)
		{
			RobotAI robotAI = (RobotAI)this.hero;
			bool flag2 = robotAI != null && !(robotAI.wc == null) && robotAI.needSendToServer;
			if (flag2)
			{
				bool flag3 = this.heroSkillData.m_nSKLongTime > 0;
				if (flag3)
				{
					robotAI.SendMsgToServer(31105, new ArrayList
					{
						base.iSkillIndex,
						this.hero.getNetid()
					});
				}
				this.hero.SetAnimator(this.skillData.animation, 0, false, 1);
				bool flag4 = robotAI.wc.GetType() == typeof(CWeaponControlMe);
				if (flag4)
				{
					((CWeaponControlMe)robotAI.wc).CheckMustFillCartridge();
				}
			}
		}
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x0015DEC2 File Offset: 0x0015C0C2
	public virtual void BreakSth(int type)
	{
		((HeroBaseMe)this.hero.heroBase).BreakSth(type);
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x0015DEDC File Offset: 0x0015C0DC
	public override bool OneSkillCanDo()
	{
		bool flag = this.hero.wc == null || !this.hero.wc.CanFirePublicCD(0f);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.heroSkillData == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.hero.CheckActiveStat();
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = !this.hero.CanHuDong;
					if (flag4)
					{
						result = false;
					}
					else
					{
						PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[base.iSkillIndex];
						for (int i = 0; i < playerSkillData.triggerWeapon.Length; i++)
						{
							bool flag5 = !((CWeaponControlMe)this.hero.wc).Readyed(playerSkillData.triggerWeapon[i]);
							if (flag5)
							{
								return false;
							}
						}
						result = base.OneSkillCanDo();
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002677 RID: 9847 RVA: 0x0015DFD8 File Offset: 0x0015C1D8
	protected void SkillShoot(object __target)
	{
		bool flag = this.bCanShoot;
		if (flag)
		{
			this.RealShoot(__target);
		}
	}

	// Token: 0x06002678 RID: 9848 RVA: 0x0015DFFC File Offset: 0x0015C1FC
	protected void RealShoot(object __target)
	{
		bool flag = !(this.hero.wc == null) && this.hero.wc.GetType() == typeof(CWeaponControlMe) && (CWeaponControlMe)this.hero.wc != null;
		if (flag)
		{
			PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[base.iSkillIndex];
			for (int i = 0; i < playerSkillData.triggerWeapon.Length; i++)
			{
				((CWeaponControlMe)this.hero.wc).WeaponActiveBySkill(playerSkillData.triggerWeapon[i], base.iSkillIndex);
				((CWeaponControlMe)this.hero.wc).WeaponFireBySkill(playerSkillData.triggerWeapon[i], "0", (Vector3)__target, this.hero.hitGamaObj);
			}
		}
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x0015E0E2 File Offset: 0x0015C2E2
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.SkillShoot(__target);
		((RobotAI)this._hero).bePlayActive = false;
	}

	// Token: 0x0400221D RID: 8733
	private CPSkillStruct _heroSkillData;

	// Token: 0x0400221E RID: 8734
	protected bool bCanShoot = true;
}

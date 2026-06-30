using System;

// Token: 0x0200019D RID: 413
public class HeroLingLongMe : HeroBaseMe
{
	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06001348 RID: 4936 RVA: 0x000B8328 File Offset: 0x000B6528
	// (set) Token: 0x06001349 RID: 4937 RVA: 0x000B8340 File Offset: 0x000B6540
	public int Transmission_Durability
	{
		get
		{
			return this._Transmission_Durability;
		}
		set
		{
			this._Transmission_Durability = value;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x0600134A RID: 4938 RVA: 0x000B834C File Offset: 0x000B654C
	// (set) Token: 0x0600134B RID: 4939 RVA: 0x000B8364 File Offset: 0x000B6564
	public bool Super_Skill_State
	{
		get
		{
			return this._Super_Skill_State;
		}
		set
		{
			this._Super_Skill_State = value;
		}
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x000B836E File Offset: 0x000B656E
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.autoDisableReadyitem = false;
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000B8384 File Offset: 0x000B6584
	protected override void Update()
	{
		base.Update();
		bool flag = !this.InputCheck();
		if (!flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("superskill");
			if (flag2)
			{
				bool flag3 = !base.hero.tChar.onGround || !base.hero.playerDazhao.canDo || base.hero.isDead || base.hero.heroSkill.CanDo(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.doskill) == 0;
				if (flag3)
				{
					return;
				}
				((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
				base.hero.SetAnimator("Skill_State", 2, 1, 1);
				bool flag4 = base.hero.heroSkill.defaultReadySkill != 0;
				if (flag4)
				{
					this.Super_Skill_State = false;
					base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
				}
				else
				{
					this.Super_Skill_State = true;
					base.hero.heroSkill.DoSkill(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.doskill, 0);
				}
			}
			bool flag5 = this.Super_Skill_State && Input_Manager.Get_Button_Down("WeaponFirst") && base.hero.skillInfo.getReadyItem(1078).beReady && base.hero.heroSkill.defaultReadySkill != 0;
			if (flag5)
			{
				this.Super_Skill_State = false;
				base.hero.superSkill(null);
			}
		}
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x000B8538 File Offset: 0x000B6738
	public override void OnDeath()
	{
		base.hero.SetAnimator("Skill_State", 2, 0, 1);
		base.OnDeath();
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x000B855C File Offset: 0x000B675C
	public override void Stun(bool m_bool)
	{
		if (m_bool)
		{
			base.hero.SetAnimator("Skill_State", 2, 0, 1);
			bool flag = base.hero.heroSkill.defaultReadySkill != 0;
			if (flag)
			{
				base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
			}
			base.hero.bePlayActive = false;
		}
		base.Stun(m_bool);
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x000B85DC File Offset: 0x000B67DC
	public override void Revive()
	{
		base.Revive();
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
	}

	// Token: 0x04000F0B RID: 3851
	private int _Transmission_Durability;

	// Token: 0x04000F0C RID: 3852
	private bool _Super_Skill_State;
}

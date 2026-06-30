using System;

// Token: 0x02000191 RID: 401
public class HeroJipusaiMe : HeroBaseMe
{
	// Token: 0x060012AF RID: 4783 RVA: 0x000B3AF8 File Offset: 0x000B1CF8
	public override void SuperSkillDo(object parm)
	{
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 6f;
		base.hero.ChangeSpeed();
		base.hero.bePlayActive = false;
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x000B3BB8 File Offset: 0x000B1DB8
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "TargetCenterOne");
		base.hero.screentarget.targetAngle = 0.99f;
		base.hero.screentarget.targetDis = 40f;
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000B3C0A File Offset: 0x000B1E0A
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.isSuperSkill = true;
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x000B3C1C File Offset: 0x000B1E1C
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag)
		{
			base.hero.superSkill(null);
		}
		bool flag2 = this.isSuperSkill;
		if (flag2)
		{
			bool flag3 = base.hero.playerDazhao.UseGun(base.hero) && base.hero.playerCommon.m_nDongjie == 0 && base.hero.playerCommon.m_nHunMi == 0;
			if (flag3)
			{
				base.hero.playerDazhao.doShoot(base.hero);
			}
			else
			{
				this.SuperSkillFinish();
			}
		}
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x000B3CE4 File Offset: 0x000B1EE4
	private void SuperSkillEnd()
	{
		this.isSuperSkill = false;
		base.hero.wInput.notInput = false;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.hero.playerCommon.m_fMoveSpeedAnimation = 1f;
		base.hero.ChangeSpeed();
		bool flag = !base.hero.isDead;
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
		base.hero.bePlayActive = true;
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x000B3DA0 File Offset: 0x000B1FA0
	public override void SuperSkillFinish()
	{
		this.SuperSkillEnd();
		base.SuperSkillFinish();
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x000B3DB4 File Offset: 0x000B1FB4
	public override void BreakSupperSkill()
	{
		bool flag = this.isSuperSkill;
		if (flag)
		{
			base.BreakSupperSkill();
		}
	}

	// Token: 0x04000EB8 RID: 3768
	private bool isSuperSkill;
}

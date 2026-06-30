using System;

// Token: 0x020001B4 RID: 436
public class HeroRoadhogMe : HeroBaseMe
{
	// Token: 0x0600140E RID: 5134 RVA: 0x000BD1DC File Offset: 0x000BB3DC
	protected override void Update()
	{
		base.Update();
		bool flag = Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag)
		{
			base.hero.superSkill(null);
		}
		bool flag2 = !this.superDoing;
		if (!flag2)
		{
			bool flag3 = base.hero.playerDazhao.UseGun(base.hero);
			if (flag3)
			{
				base.hero.playerDazhao.doShoot(base.hero);
			}
			else
			{
				this.superDoing = false;
				base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
				base.hero.wInput.notInput = false;
				base.hero.tChar.m_MoveByAnimator = true;
				bool flag4 = !base.hero.isDead;
				if (flag4)
				{
					this.SuperSkillFinish();
				}
			}
		}
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x000BD2C4 File Offset: 0x000BB4C4
	public override void SuperSkillDo(object parm)
	{
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("Skill_Stunt", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
		base.hero.ChangeSpeed();
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x000BD375 File Offset: 0x000BB575
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x000BD388 File Offset: 0x000BB588
	public override void SuperSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.hero.SetAnimator("Skill_Stunt", 0, false, 2);
		base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		base.SuperSkillFinish();
	}

	// Token: 0x04000F67 RID: 3943
	public bool superDoing;
}

using System;

// Token: 0x020001D0 RID: 464
public class HeroYanluoMe : HeroBaseMe
{
	// Token: 0x0600152E RID: 5422 RVA: 0x000C7FE0 File Offset: 0x000C61E0
	public override void SuperSkillDo(object parm)
	{
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
		base.hero.ChangeSpeed();
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x000C8091 File Offset: 0x000C6291
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000C80A4 File Offset: 0x000C62A4
	protected override void Update()
	{
		base.Update();
		bool flag = !this.InputCheck();
		if (!flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
			if (flag2)
			{
				base.hero.superSkill(null);
			}
			bool flag3 = !this.superDoing;
			if (!flag3)
			{
				bool flag4 = base.hero.playerDazhao.UseGun(base.hero);
				if (flag4)
				{
					base.hero.playerDazhao.doShoot(base.hero);
				}
				else
				{
					this.superDoing = false;
					base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
					base.hero.wInput.notInput = false;
					base.hero.tChar.m_MoveByAnimator = true;
					bool flag5 = !base.hero.isDead;
					if (flag5)
					{
						base.hero.SetAnimator("SkillSuper", 0, false, 2);
						base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
						this.SuperSkillFinish();
					}
				}
			}
		}
	}

	// Token: 0x0400105E RID: 4190
	private bool superDoing;
}

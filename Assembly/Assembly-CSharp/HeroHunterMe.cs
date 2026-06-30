using System;

// Token: 0x0200018A RID: 394
public class HeroHunterMe : HeroBaseMe
{
	// Token: 0x0600122B RID: 4651 RVA: 0x000AA3F8 File Offset: 0x000A85F8
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "EnemyCenterOne");
		base.hero.screentarget.targetAngle = 0.99f;
		base.hero.screentarget.targetDis = 100f;
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000AA44C File Offset: 0x000A864C
	public override void SuperSkillDo(object parm)
	{
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
		base.hero.ChangeSpeed();
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000AA4F0 File Offset: 0x000A86F0
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
						this.SuperSkillFinish();
					}
				}
			}
		}
	}

	// Token: 0x04000E5D RID: 3677
	private bool superDoing;
}

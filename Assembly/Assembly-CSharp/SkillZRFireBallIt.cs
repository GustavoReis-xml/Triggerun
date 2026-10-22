using System;
using System.Collections;

// Token: 0x020003AB RID: 939
public class SkillZRFireBallIt : SkillBaseIt
{
	// Token: 0x0600292C RID: 10540 RVA: 0x0016CCCE File Offset: 0x0016AECE
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.gameplayer.charMove.notMove = true;
		base.hero.SetAnimator("SkillE", 0, true, 2);
		base.hero.SetAnimator("SkillShift", 0, true, 2);
		this.SyncAnimator(true);
		base.OneSkillAction(__target, parm2);
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x0016CCF4 File Offset: 0x0016AEF4
	public override void OneSkillActionUpdate(ArrayList _Parameters)
	{
		bool flag = int.Parse(_Parameters[2].ToString()) >= 1;
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x0016CD26 File Offset: 0x0016AF26
	public override void OneSkillFinish()
	{
		base.gameplayer.charMove.notMove = false;
		base.hero.SetAnimator("SkillE", 0, false, 2);
		base.hero.SetAnimator("SkillShift", 0, false, 2);
		this.SyncAnimator(false);
		base.OneSkillFinish();
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x0016CD4C File Offset: 0x0016AF4C
	private void SyncAnimator(bool state)
	{
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			if (state)
			{
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
			}
			else
			{
				base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
		}
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x0016CDE4 File Offset: 0x0016AFE4
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		bool animatorBool = base.hero.GetAnimatorBool("SkillE");
		if (animatorBool)
		{
			this.SyncAnimator(true);
		}
	}
}

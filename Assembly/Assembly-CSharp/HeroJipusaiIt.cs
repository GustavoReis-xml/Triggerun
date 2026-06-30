using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class HeroJipusaiIt : HeroBaseIt
{
	// Token: 0x060012A8 RID: 4776 RVA: 0x000B384C File Offset: 0x000B1A4C
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		bool isCopy = net.isCopy;
		if (isCopy)
		{
			CScreenTarget.InitScreenTarget(base.hero, "TargetCenterOne");
			base.hero.screentarget.targetAngle = 0.99f;
			base.hero.screentarget.targetDis = 40f;
		}
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000B38AC File Offset: 0x000B1AAC
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool animatorBool = base.hero.GetAnimatorBool("SkillSuper");
		if (animatorBool)
		{
			this.SuperSkillDo(null);
		}
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000B38E0 File Offset: 0x000B1AE0
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
		}
		this.isSuperSkill = true;
		base.hero.tChar.moveByControlIt = true;
		bool flag2 = base.hero.netobject_type == 6;
		if (flag2)
		{
			base.hero.tChar.m_MoveByAnimator = false;
			base.hero.playerCommon.m_fMoveSpeedAnimation = 6f;
			base.hero.ChangeSpeed();
			((RobotAI)base.hero).SetAnimator("SkillSuper", 0, true, 1);
			base.StartCoroutine(this.robotSuperFinish());
		}
		else
		{
			base.StartCoroutine(this.checkFinish());
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x000B39F8 File Offset: 0x000B1BF8
	private IEnumerator robotSuperFinish()
	{
		yield return new WaitForSeconds(4f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x000B3A07 File Offset: 0x000B1C07
	private IEnumerator checkFinish()
	{
		yield return new WaitForSeconds(1.1f);
		while (this.isSuperSkill)
		{
			yield return new WaitForSeconds(0.1f);
			this.isSuperSkill = base.hero.GetAnimatorBool("SkillSuper");
			bool flag = !this.isSuperSkill;
			if (flag)
			{
				break;
			}
		}
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x000B3A18 File Offset: 0x000B1C18
	public override void SuperSkillFinish()
	{
		this.isSuperSkill = false;
		base.hero.tChar.moveByControlIt = false;
		base.hero.bePlayActive = true;
		bool flag = base.hero.netobject_type == 6;
		if (flag)
		{
			((RobotAI)base.hero).SetAnimator("SkillSuper", 0, false, 1);
		}
		bool flag2 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag2)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
		base.SuperSkillFinish();
	}

	// Token: 0x04000EB7 RID: 3767
	private bool isSuperSkill;
}

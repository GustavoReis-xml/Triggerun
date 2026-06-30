using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class HeroVampireMe : HeroBaseMe
{
	// Token: 0x06001493 RID: 5267 RVA: 0x000C15D0 File Offset: 0x000BF7D0
	public override bool InputCheck()
	{
		bool flag = base.hero != null && base.hero.skillEffectDict.ContainsKey(1100) && ((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).Deformation_State;
		return !flag && base.InputCheck();
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000C1634 File Offset: 0x000BF834
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "EnemyCenterOne");
		base.hero.screentarget.targetAngle = GlobalDefine.Vampire_Aim_Range;
		base.hero.screentarget.targetDis = (float)GlobalDefine.Vampire_Aim_Distance;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000C1688 File Offset: 0x000BF888
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero.wc != null && base.hero.GetAnimatorInteger("AttackState") == 1 && (Fun.GetServerDate() - base.hero.wc.LastShootTime(1)).TotalSeconds > 0.25;
		if (flag)
		{
			base.hero.SetAnimator("AttackState", 2, 0, 1);
		}
		bool flag2 = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && base.hero.superSkill(null);
		if (flag2)
		{
			this.superDoing = true;
			((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).OneSkillFinish();
		}
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000C1763 File Offset: 0x000BF963
	private IEnumerator Wait_Super_Skill_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x000C1774 File Offset: 0x000BF974
	public override void SuperSkillDo(object parm)
	{
		bool deformation_State = ((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).Deformation_State;
		if (deformation_State)
		{
			((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).OneSkillFinish();
		}
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		base.SuperSkillDo(parm);
		bool flag = base.hero.GetAnimatorFloat("Forward") != 0f;
		if (flag)
		{
			base.hero.SetAnimator("Idle_State", 0, false, 1);
		}
		else
		{
			base.hero.SetAnimator("Idle_State", 0, true, 1);
		}
		base.hero.SetAnimator("Skill_Musth", 0, true, 2);
		base.StartCoroutine(this.Wait_Action());
		base.hero.ChangeWeaponGroup(1);
		base.StartCoroutine(this.Wait_Super_Skill_Finish());
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000C1884 File Offset: 0x000BFA84
	private IEnumerator Wait_Action()
	{
		yield return new WaitForSeconds(0.01f);
		base.hero.wInput.notInput = false;
		yield break;
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000C1894 File Offset: 0x000BFA94
	public override void SuperSkillFinish()
	{
		bool flag = this.superDoing;
		if (flag)
		{
			this.superDoing = false;
			base.hero.bePlayActive = true;
			base.hero.wInput.notInput = true;
			base.hero.ChangeWeaponGroup(0);
			((Skill_Vampire_Transformation_Me)base.hero.skillEffectDict[1101]).OneSkillFinish();
			base.StartCoroutine(this.Wait_SuperSkillFinish());
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000C190E File Offset: 0x000BFB0E
	private IEnumerator Wait_SuperSkillFinish()
	{
		base.SuperSkillFinish();
		bool flag = base.hero.GetAnimatorFloat("Forward") != 0f;
		if (flag)
		{
			base.hero.SetAnimator("Skill_Musth_State", 0, true, 1);
		}
		else
		{
			base.hero.SetAnimator("Skill_Musth_State", 0, false, 1);
		}
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
		yield return new WaitForSeconds(1f);
		base.hero.wInput.notInput = false;
		bool flag2 = (CWeaponControlMe)base.hero.wc != null;
		if (flag2)
		{
			((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		}
		yield break;
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x000C1920 File Offset: 0x000BFB20
	public override void OnDeath()
	{
		((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).OneSkillFinish();
		base.OnDeath();
		this.SuperSkillFinish();
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
		base.hero.ChangeWeaponGroup(0);
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000C1982 File Offset: 0x000BFB82
	public override void BreakSupperSkill()
	{
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000C1985 File Offset: 0x000BFB85
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		((Skill_Vampire_Deformation_Me)base.hero.skillEffectDict[1100]).OneSkillFinish();
	}

	// Token: 0x04000FE3 RID: 4067
	public bool superDoing;
}

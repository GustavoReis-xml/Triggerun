using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class HeroLilisiMe : HeroBaseMe
{
	// Token: 0x06001335 RID: 4917 RVA: 0x000B7CDC File Offset: 0x000B5EDC
	public override void SetFilmMaterial(bool show)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1163);
		if (flag)
		{
			((Skill_Lilisi_shiledMe)base.hero.skillEffectDict[1163]).SetFilmMat(show);
		}
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000B7D28 File Offset: 0x000B5F28
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.wInput.notInput = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.playerDazhao.doShoot(base.hero);
		base.StartCoroutine(this.SuperSkillAnimationFinish());
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000B7DA4 File Offset: 0x000B5FA4
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000B7DF6 File Offset: 0x000B5FF6
	private IEnumerator SuperSkillAnimationFinish()
	{
		yield return new WaitForSeconds(1f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000B7E08 File Offset: 0x000B6008
	public override void SuperSkillFinish()
	{
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.SuperSkillFinish();
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000B7E58 File Offset: 0x000B6058
	public override GameObject GetShield(string shieldName)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1163);
		GameObject result;
		if (flag)
		{
			result = ((Skill_Lilisi_shiledMe)base.hero.skillEffectDict[1163]).getFlyShield(shieldName);
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000B7EA8 File Offset: 0x000B60A8
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
	}
}

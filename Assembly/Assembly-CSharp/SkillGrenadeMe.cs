using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class SkillGrenadeMe : SkillBaseMe
{
	// Token: 0x060026E8 RID: 9960 RVA: 0x00161038 File Offset: 0x0015F238
	private void Skill_Start()
	{
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x0016103C File Offset: 0x0015F23C
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroTerminatorMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x00161072 File Offset: 0x0015F272
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x00161084 File Offset: 0x0015F284
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.OneSkillAction(__target, parm2);
		base.hero.wInput.notInput = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x00161109 File Offset: 0x0015F309
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class SkillTreatmentMe : SkillBaseMe
{
	// Token: 0x060028B4 RID: 10420 RVA: 0x0016BD90 File Offset: 0x00169F90
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroTerminatorMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x0016BDC8 File Offset: 0x00169FC8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x0016BE45 File Offset: 0x0016A045
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(1.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x0016BE54 File Offset: 0x0016A054
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		base.OneSkillFinish();
	}
}

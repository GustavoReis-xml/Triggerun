using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class SkillSpeedwindMe : SkillBaseMe
{
	// Token: 0x0600283F RID: 10303 RVA: 0x0016A494 File Offset: 0x00168694
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroLuoLiMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x0016A4CA File Offset: 0x001686CA
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x0016A4F5 File Offset: 0x001686F5
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x0016A511 File Offset: 0x00168711
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}

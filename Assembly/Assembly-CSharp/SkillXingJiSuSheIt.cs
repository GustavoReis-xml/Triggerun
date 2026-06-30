using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class SkillXingJiSuSheIt : SkillBaseIt
{
	// Token: 0x060028D1 RID: 10449 RVA: 0x0016C1C8 File Offset: 0x0016A3C8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("SkillLianShe", 0, true, 1);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.SkillDelayFinish());
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x0016C215 File Offset: 0x0016A415
	private IEnumerator SkillDelayFinish()
	{
		yield return new WaitForSeconds(2.2f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x0016C224 File Offset: 0x0016A424
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillLianShe", 0, false, 1);
	}
}

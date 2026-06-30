using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class SkillXingJiBiaoJiIt : SkillBaseIt
{
	// Token: 0x060028C3 RID: 10435 RVA: 0x0016C039 File Offset: 0x0016A239
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.SkillDelayFinish());
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x0016C062 File Offset: 0x0016A262
	private IEnumerator SkillDelayFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x0016C071 File Offset: 0x0016A271
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class SkillLieYanQuanIt : SkillBaseIt
{
	// Token: 0x06002747 RID: 10055 RVA: 0x00163162 File Offset: 0x00161362
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x0016316C File Offset: 0x0016136C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillE", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x001631BA File Offset: 0x001613BA
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(1.7f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x001631D0 File Offset: 0x001613D0
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillE", 0, false, 1);
	}
}

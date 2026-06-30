using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class SkillCrazybombMe : SkillBaseMe
{
	// Token: 0x0600267F RID: 9855 RVA: 0x0015E136 File Offset: 0x0015C336
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x0015E14F File Offset: 0x0015C34F
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x0015E159 File Offset: 0x0015C359
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(0.8f);
		this.OneSkillFinish();
		yield break;
	}
}

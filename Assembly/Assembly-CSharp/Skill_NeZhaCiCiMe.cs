using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class Skill_NeZhaCiCiMe : SkillBaseMe
{
	// Token: 0x06002995 RID: 10645 RVA: 0x0016EDAA File Offset: 0x0016CFAA
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x0016EDD5 File Offset: 0x0016CFD5
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x0016EDE4 File Offset: 0x0016CFE4
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}
}

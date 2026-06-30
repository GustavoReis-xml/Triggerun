using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public class Skill_Lilisi_bloodMe : SkillBaseMe
{
	// Token: 0x06002959 RID: 10585 RVA: 0x0016D728 File Offset: 0x0016B928
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillTwo", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x0016D777 File Offset: 0x0016B977
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x0016D786 File Offset: 0x0016B986
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
	}
}

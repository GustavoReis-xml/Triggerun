using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class SkillZhiLiaoShuiJingIt : SkillBaseIt
{
	// Token: 0x06002918 RID: 10520 RVA: 0x0016CA18 File Offset: 0x0016AC18
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillTwo", 0, true, 1);
		base.StartCoroutine(this.DelaySkillFinish());
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x0016CA6E File Offset: 0x0016AC6E
	private IEnumerator DelaySkillFinish()
	{
		yield return new WaitForSeconds(0.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x0016CA7D File Offset: 0x0016AC7D
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillTwo", 0, false, 1);
	}
}

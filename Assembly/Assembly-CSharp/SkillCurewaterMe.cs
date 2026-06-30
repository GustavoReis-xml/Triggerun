using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class SkillCurewaterMe : SkillBaseMe
{
	// Token: 0x06002686 RID: 9862 RVA: 0x0015E180 File Offset: 0x0015C380
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroLuoLiMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x0015E1B6 File Offset: 0x0015C3B6
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x0015E1E4 File Offset: 0x0015C3E4
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.wInput.notInput = false;
			base.OneSkillFinish();
		}
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x0015E22A File Offset: 0x0015C42A
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}

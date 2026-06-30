using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class HeroHunterIt : HeroBaseIt
{
	// Token: 0x06001228 RID: 4648 RVA: 0x000AA388 File Offset: 0x000A8588
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 4f;
		base.hero.ChangeSpeed();
		base.StartCoroutine(this.checkFinish());
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000AA3DD File Offset: 0x000A85DD
	private IEnumerator checkFinish()
	{
		yield return new WaitForSeconds(0.1f);
		bool isdo = true;
		while (isdo)
		{
			yield return new WaitForSeconds(0.1f);
			isdo = base.hero.GetAnimatorBool("SkillSuper");
		}
		base.hero.tChar.m_MoveByAnimator = true;
		yield break;
	}
}

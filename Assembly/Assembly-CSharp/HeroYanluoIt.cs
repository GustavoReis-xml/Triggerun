using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class HeroYanluoIt : HeroBaseIt
{
	// Token: 0x0600152B RID: 5419 RVA: 0x000C7F70 File Offset: 0x000C6170
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 4f;
		base.hero.ChangeSpeed();
		base.StartCoroutine(this.checkFinish());
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000C7FC5 File Offset: 0x000C61C5
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

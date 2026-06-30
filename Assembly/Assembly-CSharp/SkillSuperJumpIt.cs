using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class SkillSuperJumpIt : SkillBaseIt
{
	// Token: 0x0600286B RID: 10347 RVA: 0x0016AC68 File Offset: 0x00168E68
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.gameplayer.SetAnimator("SkillJumpEnd", 0, false, 1);
		base.gameplayer.tChar.moveByControlIt = true;
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x0016ACB7 File Offset: 0x00168EB7
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x0016ACC6 File Offset: 0x00168EC6
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.gameplayer.SetAnimator("SkillJumpEnd", 0, true, 1);
		base.gameplayer.tChar.moveByControlIt = false;
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x0016ACFC File Offset: 0x00168EFC
	public override bool OneSkillCanDo()
	{
		bool flag = base.gameplayer.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.gameplayer.playerCommon.m_nDongjie == 1;
			result = (!flag2 && base.OneSkillCanDo());
		}
		return result;
	}
}

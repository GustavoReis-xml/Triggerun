using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class SkillSuperJumpLzzIt : SkillBaseIt
{
	// Token: 0x06002870 RID: 10352 RVA: 0x0016AD55 File Offset: 0x00168F55
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x0016AD6E File Offset: 0x00168F6E
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x0016AD7D File Offset: 0x00168F7D
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x0016AD88 File Offset: 0x00168F88
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

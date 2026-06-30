using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class SkillZhiLiaoShuiJingMe : SkillBaseMe
{
	// Token: 0x0600291C RID: 10524 RVA: 0x0016CAA9 File Offset: 0x0016ACA9
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.DelaySkillFinish());
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x0016CADB File Offset: 0x0016ACDB
	private IEnumerator DelaySkillFinish()
	{
		yield return new WaitForSeconds(0.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x0016CAEA File Offset: 0x0016ACEA
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x0016CAF4 File Offset: 0x0016ACF4
	public override bool OneSkillCanDo()
	{
		bool flag = base.hero.screentarget == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool isBuff = base.hero.playerZhunxingMe.GetIsBuff(base.hero.screentarget.NetTarget, 1021);
			result = (!isBuff && base.OneSkillCanDo());
		}
		return result;
	}
}

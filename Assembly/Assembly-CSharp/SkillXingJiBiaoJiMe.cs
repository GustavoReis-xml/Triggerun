using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class SkillXingJiBiaoJiMe : SkillBaseMe
{
	// Token: 0x060028C7 RID: 10439 RVA: 0x0016C084 File Offset: 0x0016A284
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.StartCoroutine(this.delaySkillFinish());
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x0016C0D7 File Offset: 0x0016A2D7
	private IEnumerator delaySkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x0016C0E6 File Offset: 0x0016A2E6
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x0016C0F0 File Offset: 0x0016A2F0
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
			bool isBuff = base.hero.playerZhunxingMe.GetIsBuff(base.hero.screentarget.NetTarget, 1010);
			result = (!isBuff && base.OneSkillCanDo());
		}
		return result;
	}
}

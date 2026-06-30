using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class SkillHuanYingTuXiMe : SkillBaseMe
{
	// Token: 0x060026FE RID: 9982 RVA: 0x0016129B File Offset: 0x0015F49B
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x001612A8 File Offset: 0x0015F4A8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x0016131A File Offset: 0x0015F51A
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x00161329 File Offset: 0x0015F529
	private IEnumerator DelayshakeCamera()
	{
		yield return new WaitForSeconds(1.5f);
		SC_shakeCamera.shakeCamera();
		yield break;
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class SkillXingJiSuSheMe : SkillBaseMe
{
	// Token: 0x060028D5 RID: 10453 RVA: 0x0016C250 File Offset: 0x0016A450
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x0016C264 File Offset: 0x0016A464
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		((TBCharacterThirdMe)base.hero.tChar).canWall = false;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.bePlayActive = false;
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillLianShe", 0, true, 2);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x0016C2FD File Offset: 0x0016A4FD
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(0.5f);
		bool flag = base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.RealShoot(__target, null);
			base.hero.bePlayActive = false;
		}
		yield return new WaitForSeconds(1.7f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x0016C314 File Offset: 0x0016A514
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		((TBCharacterThirdMe)base.hero.tChar).canWall = true;
		base.hero.SetAnimator("SkillLianShe", 0, false, 2);
		base.hero.wInput.notInput = false;
	}
}

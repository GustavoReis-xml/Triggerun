using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class SkillLieYanQuanMe : SkillBaseMe
{
	// Token: 0x0600274C RID: 10060 RVA: 0x001631FC File Offset: 0x001613FC
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x00163210 File Offset: 0x00161410
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.wInput.notInput = true;
		base.hero.SetAnimator("SkillE", 0, true, 1);
		base.StartCoroutine(this.delaySkillFinish(__target));
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x00163287 File Offset: 0x00161487
	private IEnumerator delaySkillFinish(object __target)
	{
		yield return new WaitForSeconds(0.1f);
		base.RealShoot(__target, null);
		base.hero.bePlayActive = false;
		yield return new WaitForSeconds(1.4f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x001632A0 File Offset: 0x001614A0
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillE", 0, false, 1);
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x001632ED File Offset: 0x001614ED
	private void Update()
	{
	}
}

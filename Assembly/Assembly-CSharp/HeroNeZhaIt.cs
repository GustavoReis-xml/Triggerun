using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class HeroNeZhaIt : HeroBaseIt
{
	// Token: 0x06001396 RID: 5014 RVA: 0x000BA1A1 File Offset: 0x000B83A1
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000BA1AD File Offset: 0x000B83AD
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000BA1C5 File Offset: 0x000B83C5
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x000BA1D4 File Offset: 0x000B83D4
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x000BA1DE File Offset: 0x000B83DE
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, true, 1);
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000BA201 File Offset: 0x000B8401
	public override void OnDeath()
	{
		base.OnDeath();
	}
}

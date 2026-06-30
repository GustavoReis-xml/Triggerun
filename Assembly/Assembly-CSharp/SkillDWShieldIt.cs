using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class SkillDWShieldIt : SkillBaseIt
{
	// Token: 0x060026C3 RID: 9923 RVA: 0x00160474 File Offset: 0x0015E674
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x0016048C File Offset: 0x0015E68C
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.OneSkillAction(target, parm2);
		((HeroDaWeiIt)base.hero.heroBase).ShieldObjShow(true);
		this.onSkillDoing = true;
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x001604C3 File Offset: 0x0015E6C3
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(((ISkillDataSkillDWShield)this.ISD).Shield_Time);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x001604D4 File Offset: 0x0015E6D4
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			((HeroDaWeiIt)base.hero.heroBase).ShieldObjShow(false);
			this.onSkillDoing = false;
			base.OneSkillFinish();
		}
	}

	// Token: 0x060026C7 RID: 9927 RVA: 0x00160528 File Offset: 0x0015E728
	private void Update()
	{
		bool flag = this.onSkillDoing && (base.hero.playerCommon.m_nHunMi == 1 || base.hero.playerCommon.m_nDongjie == 1);
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x04002240 RID: 8768
	private GameObject Shield;

	// Token: 0x04002241 RID: 8769
	private bool onSkillDoing;
}

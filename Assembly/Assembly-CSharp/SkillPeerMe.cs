using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class SkillPeerMe : SkillBaseMe
{
	// Token: 0x06002770 RID: 10096 RVA: 0x001638C8 File Offset: 0x00161AC8
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroMedusaMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x001638FE File Offset: 0x00161AFE
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x0016392C File Offset: 0x00161B2C
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.wInput.notInput = false;
			base.OneSkillFinish();
		}
	}

	// Token: 0x06002773 RID: 10099 RVA: 0x00163972 File Offset: 0x00161B72
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		bool flag = !((HeroMedusaMe)base.hero.heroBase).superDoing;
		if (flag)
		{
			this.OneSkillFinish();
		}
		yield break;
	}
}

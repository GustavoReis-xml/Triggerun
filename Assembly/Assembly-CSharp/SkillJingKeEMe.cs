using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class SkillJingKeEMe : SkillBaseMe
{
	// Token: 0x06002724 RID: 10020 RVA: 0x00162274 File Offset: 0x00160474
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroJingKeMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x001622AA File Offset: 0x001604AA
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x001622D5 File Offset: 0x001604D5
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds((float)(base.heroSkillData.m_nSKLongTime / 1000) + 0.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x001622E4 File Offset: 0x001604E4
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.wInput.notInput = false;
			base.OneSkillFinish();
		}
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class SkillShanxianIt : SkillBaseIt
{
	// Token: 0x060027F2 RID: 10226 RVA: 0x0016860A File Offset: 0x0016680A
	private void Start()
	{
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x0016860D File Offset: 0x0016680D
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.gameplayer.tChar.moveByControlIt = true;
		base.hero.heroBase.B_Transforing = true;
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x00168649 File Offset: 0x00166849
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.2f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x00168658 File Offset: 0x00166858
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.heroBase.B_Transforing = false;
		base.gameplayer.tChar.moveByControlIt = false;
	}
}

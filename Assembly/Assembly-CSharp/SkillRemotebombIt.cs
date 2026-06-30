using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class SkillRemotebombIt : SkillBaseIt
{
	// Token: 0x060027A1 RID: 10145 RVA: 0x00165AB1 File Offset: 0x00163CB1
	private void Start()
	{
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x00165AB4 File Offset: 0x00163CB4
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		this.bSkill = false;
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x00165AC5 File Offset: 0x00163CC5
	private void Update()
	{
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x00165AC8 File Offset: 0x00163CC8
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		bool animatorBool = base.hero.GetAnimatorBool("LeftHandHadHide");
		if (animatorBool)
		{
			this.bSkill = true;
			this.OneSkillAction(null, null);
		}
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x00165B04 File Offset: 0x00163D04
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		bool playerHaveCreate = base.hero.playerHaveCreate;
		if (playerHaveCreate)
		{
			base.hero.tCharAnimationTps.animator.SetBool("LeftHandHadHide", true);
			base.StartCoroutine(this.checkHandHadHideFinish());
		}
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x00165B55 File Offset: 0x00163D55
	private IEnumerator checkHandHadHideFinish()
	{
		yield return new WaitForSeconds(1.1f);
		while (this.bSkill)
		{
			yield return new WaitForSeconds(0.1f);
			this.bSkill = base.hero.GetAnimatorBool("LeftHandHadHide");
			bool flag = !this.bSkill;
			if (flag)
			{
				break;
			}
		}
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x040022A9 RID: 8873
	private bool bSkill;
}

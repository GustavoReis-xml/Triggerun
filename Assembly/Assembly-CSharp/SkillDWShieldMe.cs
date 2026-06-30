using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class SkillDWShieldMe : SkillBaseMe
{
	// Token: 0x060026C9 RID: 9929 RVA: 0x00160580 File Offset: 0x0015E780
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.onSkillDoing = false;
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x00160594 File Offset: 0x0015E794
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x001605AC File Offset: 0x0015E7AC
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		this.Break_Reload();
		base.hero.SetAnimator("skill_shift", 0, true, 1);
		this.onSkillDoing = true;
		((HeroDaWeiMe)base.hero.heroBase).ShieldObjShow(true);
		base.hero.wInput.notFire = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x0016061F File Offset: 0x0015E81F
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds((float)(base.heroSkillData.m_nSKLongTime / 1000));
		((ISkillDataSkillDWShield)this.ISD).Shield_Time = (float)(base.heroSkillData.m_nSKLongTime / 1000);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x00160630 File Offset: 0x0015E830
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy && this.onSkillDoing;
		if (flag)
		{
			((HeroDaWeiMe)base.hero.heroBase).ShieldObjShow(false);
			base.hero.SetAnimator("skill_shift", 0, false, 1);
			base.OneSkillFinish();
			bool flag2 = !base.hero.GetAnimatorBool("skill_E");
			if (flag2)
			{
				base.hero.wInput.notFire = false;
			}
			else
			{
				base.hero.bePlayActive = false;
			}
			this.onSkillDoing = false;
		}
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x001606DC File Offset: 0x0015E8DC
	private void Update()
	{
		bool flag = this.onSkillDoing && (base.hero.playerCommon.m_nHunMi == 1 || base.hero.playerCommon.m_nDongjie == 1);
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x04002242 RID: 8770
	private bool onSkillDoing;
}

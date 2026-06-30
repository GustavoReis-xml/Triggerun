using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class SkillHZJiaDunMe : SkillBaseMe
{
	// Token: 0x06002704 RID: 9988 RVA: 0x0016134C File Offset: 0x0015F54C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillShift", 0, true, 1);
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x001613B8 File Offset: 0x0015F5B8
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x001613C8 File Offset: 0x0015F5C8
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool flag = base.hero.playerCommon.m_nHunMi == 0 && base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.hero.bePlayActive = true;
			base.hero.wInput.notInput = false;
		}
		base.hero.SetAnimator("SkillShift", 0, false, 1);
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x00161444 File Offset: 0x0015F644
	public override bool OneSkillCanDo()
	{
		bool flag = (float)base.hero.playerCommon.GetNowHp() <= 500f;
		bool result;
		if (flag)
		{
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.SkillMsg(Language.GetLang(2050, new object[0]));
			}
			result = false;
		}
		else
		{
			result = base.OneSkillCanDo();
		}
		return result;
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class SkillHZJiaXueMe : SkillBaseMe
{
	// Token: 0x0600270A RID: 9994 RVA: 0x001614C4 File Offset: 0x0015F6C4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillE", 0, true, 1);
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x00161530 File Offset: 0x0015F730
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x00161540 File Offset: 0x0015F740
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool flag = base.hero.playerCommon.m_nHunMi == 0 && base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.hero.bePlayActive = true;
			base.hero.wInput.notInput = false;
		}
		base.hero.SetAnimator("SkillE", 0, false, 1);
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x001615BC File Offset: 0x0015F7BC
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

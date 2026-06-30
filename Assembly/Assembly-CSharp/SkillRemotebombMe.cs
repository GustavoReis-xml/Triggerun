using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class SkillRemotebombMe : SkillBaseMe
{
	// Token: 0x060027A8 RID: 10152 RVA: 0x00165B70 File Offset: 0x00163D70
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("Reload", 0, false, 1);
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("SkillBoom", 0, true, 2);
		base.hero.SetAnimator("LeftHandHadHide", 0, true, 2);
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng = true;
		this.OnCheckZhaDan();
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x00165C48 File Offset: 0x00163E48
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(0.8f);
		base.hero.wInput.notInput = false;
		base.hero.bePlayActive = true;
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x00165C58 File Offset: 0x00163E58
	public override bool OneSkillCanDo()
	{
		base.hero.wc.UpdateFireTime(0f);
		bool flag = base.hero.playerCommon.m_nHunMi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nJiangzhi == 1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = base.hero.playerCommon.m_nDongjie == 1;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool isCreateItemIng = ((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng;
					result = (!isCreateItemIng && base.OneSkillCanDo());
				}
			}
		}
		return result;
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x00165CFC File Offset: 0x00163EFC
	private void OnCheckZhaDan()
	{
		bool flag = base.IsInvoking("OnLuojiCheckZhaDan");
		if (flag)
		{
			base.CancelInvoke("OnLuojiCheckZhaDan");
		}
		base.Invoke("OnLuojiCheckZhaDan", 5f);
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x00165D38 File Offset: 0x00163F38
	private void OnLuojiCheckZhaDan()
	{
		bool flag = base.hero != null && !base.hero.isDead && ((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng && !((HeroLuoJiMe)base.hero.heroBase).UserYaoKong;
		if (flag)
		{
			((HeroLuoJiMe)base.hero.heroBase).OnNpcDestory();
			((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng = false;
		}
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x00165DC0 File Offset: 0x00163FC0
	private void Update()
	{
		bool flag = ((HeroLuoJiMe)base.hero.heroBase).IsDoSkillYaoKong && Input_Manager.Get_Button_Down("WeaponSec") && !base.hero.isDead && ((HeroLuoJiMe)base.hero.heroBase).NpcObj != null && !(((HeroLuoJiMe)base.hero.heroBase).NpcObj.UserControl == null) && !(((HeroLuoJiMe)base.hero.heroBase).NpcObj.wc == null) && !((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng && base.hero.playerCommon.m_nHunMi != 1 && base.hero.playerCommon.m_nJiangzhi != 1 && base.hero.playerCommon.m_nDongjie != 1;
		if (flag)
		{
			base.hero.SetAnimator("LeftHandHadHide", 0, false, 2);
			base.hero.SetAnimator("IsShiftOut", 0, true, 1);
			base.hero.SetAnimator("ShiftIng", 0, false, 1);
			base.hero.SetAnimator("SkillBoom", 0, false, 2);
			((HeroLuoJiMe)base.hero.heroBase).UserYaoKong = true;
			bool flag2 = ((HeroLuoJiMe)base.hero.heroBase).NpcObj != null;
			if (flag2)
			{
				((HeroLuoJiMe)base.hero.heroBase).NpcObj.Suicide();
			}
			((HeroLuoJiMe)base.hero.heroBase).IsDoSkillYaoKong = false;
			((HeroLuoJiMe)base.hero.heroBase).IsCreateItemIng = false;
			((HeroLuoJiMe)base.hero.heroBase).isZiSha = false;
			Fun.Load_Sound(base.hero, "skill_luoji_bomb_detonate", base.hero.thisModel, true, "LuoJi_YaoKong_Sound");
		}
	}
}

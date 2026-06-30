using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class HeroTerminatorMe : HeroBaseMe
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x0600144D RID: 5197 RVA: 0x000BF9D0 File Offset: 0x000BDBD0
	// (set) Token: 0x0600144E RID: 5198 RVA: 0x000BF9E8 File Offset: 0x000BDBE8
	public bool Skill_One_Key_State
	{
		get
		{
			return this._Skill_One_Key_State;
		}
		set
		{
			this._Skill_One_Key_State = value;
		}
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000BF9F4 File Offset: 0x000BDBF4
	protected override void Update()
	{
		base.Update();
		this.Keyboard_Control();
		bool flag = base.hero.nHuDongState > 0 || base.hero.wInput.notInput;
		if (flag)
		{
			bool flag2 = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
			if (flag2)
			{
				((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
			}
		}
		bool flag3 = this.InputCheck() && Input_Manager.Get_Button_Down("superskill");
		if (flag3)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x000BFAAA File Offset: 0x000BDCAA
	private IEnumerator Wait_Super_Skill_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x000BFAB9 File Offset: 0x000BDCB9
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		Fun.Load_Sound(net, "skillvoice_zhongjiezhe_run", base.hero._playerSoundContainer, false, "Terminator_Sprint_Sound");
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000BFAE4 File Offset: 0x000BDCE4
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
		bool flag = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
		if (flag)
		{
			((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
		}
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x000BFB48 File Offset: 0x000BDD48
	private void Keyboard_Control()
	{
		bool flag = Input_Manager.Get_Button("WeaponSec") && base.hero.heroSkill.CanDoSomeOne("WeaponSec") == 1;
		if (flag)
		{
			bool flag2 = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
			if (flag2)
			{
				((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
			}
		}
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000BFBD0 File Offset: 0x000BDDD0
	public override void SuperSkillDo(object parm)
	{
		bool flag = base.hero.skillEffectDict != null;
		if (flag)
		{
			bool flag2 = base.hero.skillEffectDict.ContainsKey(1032);
			if (flag2)
			{
				((SkillTreatmentMe)base.hero.skillEffectDict[1032]).OneSkillFinish();
			}
			bool flag3 = base.hero.skillEffectDict.ContainsKey(1030);
			if (flag3)
			{
				((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
			}
		}
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		base.SuperSkillDo(parm);
		this.superDoing = true;
		base.hero.SetAnimator("Skill_Musth", 0, true, 2);
		base.StartCoroutine(this.Wait_Action());
		base.hero.ChangeWeaponGroup(1);
		base.hero.wc.notFillCartridge = true;
		base.StartCoroutine(this.Wait_Super_Skill_Finish());
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x000BFCE7 File Offset: 0x000BDEE7
	private IEnumerator Wait_Action()
	{
		yield return new WaitForSeconds(0.5f);
		base.hero.wInput.notInput = false;
		yield break;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000BFCF8 File Offset: 0x000BDEF8
	public override void SuperSkillFinish()
	{
		bool flag = this.superDoing;
		if (flag)
		{
			this.superDoing = false;
			base.hero.wInput.notInput = true;
			base.hero.ChangeWeaponGroup(0);
			base.StartCoroutine(this.Wait_SuperSkillFinish());
			bool flag2 = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1033);
			if (flag2)
			{
				base.hero.skillEffectDict[1033].OneSkillFinish();
			}
			base.hero.wc.notFillCartridge = false;
		}
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000BFD9F File Offset: 0x000BDF9F
	private IEnumerator Wait_SuperSkillFinish()
	{
		yield return new WaitForSeconds(0.5f);
		base.SuperSkillFinish();
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
		base.StartCoroutine(this.Wait_Action());
		yield break;
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000BFDB0 File Offset: 0x000BDFB0
	public override bool BulletCanFill()
	{
		bool flag = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
		bool result;
		if (flag)
		{
			result = ((SkillSprintMe)base.hero.skillEffectDict[1030]).Can_Reload();
		}
		else
		{
			result = base.BulletCanFill();
		}
		return result;
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x000BFE14 File Offset: 0x000BE014
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
		if (flag)
		{
			((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
		}
		this.SuperSkillFinish();
		base.hero.SetAnimator("Aim", 0, true, 1);
		base.hero.SetAnimator("Aim2", 0, false, 1);
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
		base.hero.ChangeWeaponGroup(0);
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000BFED3 File Offset: 0x000BE0D3
	public override void BreakSupperSkill()
	{
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x000BFED8 File Offset: 0x000BE0D8
	public override void Stun(bool m_bool)
	{
		if (m_bool)
		{
			bool flag = base.hero.skillEffectDict != null && base.hero.skillEffectDict.ContainsKey(1030);
			if (flag)
			{
				((SkillSprintMe)base.hero.skillEffectDict[1030]).OneSkillFinish();
			}
		}
		base.Stun(m_bool);
		bool flag2 = this.superDoing;
		if (flag2)
		{
			base.hero.bePlayActive = false;
		}
	}

	// Token: 0x04000FAB RID: 4011
	private bool _Skill_One_Key_State;

	// Token: 0x04000FAC RID: 4012
	public bool superDoing;
}

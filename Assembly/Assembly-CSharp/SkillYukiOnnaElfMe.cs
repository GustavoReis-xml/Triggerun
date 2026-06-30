using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class SkillYukiOnnaElfMe : SkillBaseMe
{
	// Token: 0x060028F4 RID: 10484 RVA: 0x0016C703 File Offset: 0x0016A903
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.bCanShoot = false;
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x0016C718 File Offset: 0x0016A918
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		this._State = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.OneSkillAction(target, parm2);
		base.hero.SetAnimator("Skill_2_Effect", 0, true, 2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x0016C772 File Offset: 0x0016A972
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds((float)(base.heroSkillData.m_nSKLongTime / 1000));
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x0016C784 File Offset: 0x0016A984
	public override void OneSkillFinish()
	{
		bool state = this._State;
		if (state)
		{
			this._State = false;
			base.hero.SetAnimator("Skill_2_Effect", 0, false, 2);
			base.OneSkillFinish();
		}
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x0016C7C8 File Offset: 0x0016A9C8
	private void Update()
	{
		bool state = this._State;
		if (state)
		{
			base.RealShoot(base.hero.thisModel.transform.position, null);
			((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		}
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x0016C81A File Offset: 0x0016AA1A
	public override void BreakSkill()
	{
		base.BreakSkill();
		this.OneSkillFinish();
	}

	// Token: 0x04002326 RID: 8998
	private bool _State;
}

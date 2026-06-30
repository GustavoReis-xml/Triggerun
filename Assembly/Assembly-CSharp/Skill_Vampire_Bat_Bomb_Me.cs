using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class Skill_Vampire_Bat_Bomb_Me : SkillBaseMe
{
	// Token: 0x060029D4 RID: 10708 RVA: 0x0016FFA4 File Offset: 0x0016E1A4
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroVampireMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x0016FFDC File Offset: 0x0016E1DC
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		((CWeaponControlMe)this._hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.OneSkillAction(__target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x00170028 File Offset: 0x0016E228
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(1.3f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029D7 RID: 10711 RVA: 0x00170037 File Offset: 0x0016E237
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
	}
}

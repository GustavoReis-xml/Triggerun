using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class SkillLingLongTransmissionMe : SkillBaseMe
{
	// Token: 0x0600276C RID: 10092 RVA: 0x00163714 File Offset: 0x00161914
	public override void readyItemSwitch(bool _Value, bool isDoSkill = false)
	{
		if (_Value)
		{
			((CWeaponControlMe)this._hero.wc).cancelFillCartridge();
			((HeroLingLongMe)base.hero.heroBase).Super_Skill_State = true;
			base.hero.SetAnimator("Skill_State", 2, 1, 1);
		}
		else
		{
			bool flag = !isDoSkill;
			if (flag)
			{
				base.hero.SetAnimator("Skill_State", 2, 0, 1);
				((HeroLingLongMe)base.hero.heroBase).Super_Skill_State = false;
				((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
			}
		}
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x001637C4 File Offset: 0x001619C4
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		Vector3 position = Vector3.zero;
		Vector3 rotation = Vector3.zero;
		CPlaceItem readyItem = base.hero.skillInfo.getReadyItem(base.iSkillIndex);
		bool flag = readyItem == null;
		if (flag)
		{
			base.hero.SetAnimator("Skill_State", 2, 0, 1);
		}
		else
		{
			base.OneSkillAction(target, parm2);
			this.OneSkillFinish();
			base.hero.heroBase.SuperSkillFinish();
			position = readyItem.ThisGameObjTrans.transform.position;
			rotation = readyItem.ThisGameObjTrans.transform.eulerAngles;
			base.hero.Npc_Handle_Component.Create_Npc(base.hero, -1, 1016, position, rotation, Vector3.one, this.skillData.id, Vector3.zero, string.Empty);
			base.hero.SetAnimator("Skill_State", 2, 3, 1);
		}
	}
}

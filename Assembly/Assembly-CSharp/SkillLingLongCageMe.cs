using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class SkillLingLongCageMe : SkillBaseMe
{
	// Token: 0x0600275B RID: 10075 RVA: 0x001633B8 File Offset: 0x001615B8
	public override void readyItemSwitch(bool _Value, bool isDoSkill = false)
	{
		if (_Value)
		{
			((CWeaponControlMe)this._hero.wc).cancelFillCartridge();
			base.hero.SetAnimator("Skill_State", 2, 1, 1);
		}
		else
		{
			bool flag = !isDoSkill;
			if (flag)
			{
				base.hero.SetAnimator("Skill_State", 2, 0, 1);
				base.StartCoroutine(this.Detection_Reload());
			}
		}
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x0016342E File Offset: 0x0016162E
	private IEnumerator Detection_Reload()
	{
		yield return new WaitForSeconds(1f);
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		yield break;
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x00163440 File Offset: 0x00161640
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
			position = readyItem.ThisGameObjTrans.transform.position;
			rotation = readyItem.ThisGameObjTrans.transform.eulerAngles;
			base.hero.Npc_Handle_Component.Create_Npc(base.hero, -1, 1015, position, rotation, Vector3.one, this.skillData.id, Vector3.zero, string.Empty);
			base.hero.SetAnimator("Skill_State", 2, 3, 1);
			base.StartCoroutine(this.Wait_Finish());
		}
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x00163525 File Offset: 0x00161725
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x0016352F File Offset: 0x0016172F
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}

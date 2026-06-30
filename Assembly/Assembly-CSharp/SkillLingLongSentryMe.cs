using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class SkillLingLongSentryMe : SkillBaseMe
{
	// Token: 0x06002764 RID: 10084 RVA: 0x00163568 File Offset: 0x00161768
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
				((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
			}
		}
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x001635E8 File Offset: 0x001617E8
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		Vector3 position = Vector3.zero;
		Vector3 zero = Vector3.zero;
		base.OneSkillAction(target, parm2);
		CPlaceItem readyItem = base.hero.skillInfo.getReadyItem(base.iSkillIndex);
		bool flag = readyItem == null;
		if (flag)
		{
			base.hero.SetAnimator("Skill_State", 2, 0, 1);
		}
		else
		{
			position = readyItem.ThisGameObjTrans.transform.position;
			zero = new Vector3(0f, 180f, 0f);
			base.hero.Npc_Handle_Component.Create_Npc(base.hero, readyItem.HitNetId, 1014, position, zero, Vector3.one, base.iSkillIndex, Vector3.zero, string.Empty);
			base.hero.SetAnimator("Skill_State", 2, 3, 1);
			base.StartCoroutine(this.Wait_Finish());
		}
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x001636D2 File Offset: 0x001618D2
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x001636DC File Offset: 0x001618DC
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}

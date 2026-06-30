using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class SkillShanxianMe : SkillBaseMe
{
	// Token: 0x060027F7 RID: 10231 RVA: 0x0016868E File Offset: 0x0016688E
	private void Start()
	{
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x00168694 File Offset: 0x00166894
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		float num = Input_Manager.Get_Axis_Raw("Horizontal");
		float num2 = Input_Manager.Get_Axis_Raw("Vertical");
		Vector3 a = Vector3.zero;
		Vector3 b = Vector3.zero;
		bool flag = num != 0f;
		Vector3 a2;
		if (flag)
		{
			a2 = Camera.main.transform.right;
			a2.y = 0f;
			b = (a2 * num).normalized;
		}
		bool flag2 = num2 == 0f && num == 0f;
		if (flag2)
		{
			num2 = 1f;
		}
		a2 = Camera.main.transform.forward;
		a2.y = 0f;
		a = (a2 * num2).normalized;
		a += b;
		float d = ((float)((int)parm2[0]) + 10000f) / 10000f * this.fMaxSpeed;
		((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(true);
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity(a * d, 0.1f, true);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("Reload", 0, false, 1);
		base.hero.SetAnimator("SkillBlinkz", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.hero.heroBase.B_Transforing = true;
		this.OneSkillFinish();
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x00168836 File Offset: 0x00166A36
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.heroBase.B_Transforing = false;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x040022EE RID: 8942
	private float fMaxSpeed = 75f;
}

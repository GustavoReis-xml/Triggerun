using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class SkillShaoweiMe : SkillBaseMe
{
	// Token: 0x06002802 RID: 10242 RVA: 0x00168964 File Offset: 0x00166B64
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		bool flag = base.hero.playerRuntimeData.MoveState == 0;
		if (flag)
		{
			base.StartCoroutine(this.SetMoveState(1));
		}
		else
		{
			base.StartCoroutine(this.SetMoveState(0));
			this.OneSkillFinish();
		}
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x001689B8 File Offset: 0x00166BB8
	public IEnumerator SetMoveState(int iState)
	{
		base.hero.playerRuntimeData.NoInputMove = true;
		base.hero.wInput.notInput = true;
		yield return new WaitForSeconds(1.2f);
		base.hero.playerRuntimeData.MoveState = iState;
		base.hero.ChangeWeaponGroup(iState);
		base.hero.wInput.notInput = false;
		bool flag = iState == 0;
		if (flag)
		{
			base.hero.playerRuntimeData.NoInputMove = false;
		}
		bool flag2 = iState == 1;
		if (flag2)
		{
			base.hero.playerRuntimeData.Deformation_State = true;
		}
		else
		{
			base.hero.playerRuntimeData.Deformation_State = false;
		}
		yield break;
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x001689D0 File Offset: 0x00166BD0
	public void SkillEnd()
	{
		base.hero.playerRuntimeData.MoveState = 0;
		base.hero.ChangeWeaponGroup(0);
		base.hero.wInput.notInput = false;
		base.hero.playerRuntimeData.NoInputMove = false;
		this.OneSkillFinish();
	}
}

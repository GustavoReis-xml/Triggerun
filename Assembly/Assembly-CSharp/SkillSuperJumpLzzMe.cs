using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class SkillSuperJumpLzzMe : SkillBaseMe
{
	// Token: 0x06002875 RID: 10357 RVA: 0x0016ADE4 File Offset: 0x00168FE4
	public override bool OneSkillCanDo()
	{
		bool flag = base.hero.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			result = (!flag2 && base.OneSkillCanDo());
		}
		return result;
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x0016AE34 File Offset: 0x00169034
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x0016AE48 File Offset: 0x00169048
	private void Super_Jump_Start(object _target)
	{
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.charMove.canJump = false;
		base.hero.charMove.state.fly = true;
		Transform transform = CameraControl.Instance.transform;
		transform.localEulerAngles = new Vector3(-75f, transform.localEulerAngles.y, transform.localEulerAngles.z);
		Vector3 speed = transform.forward * 1.2f * 18f;
		((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(true);
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity(speed, 0.8f, false);
		base.StartCoroutine(this.Super_Jump_End());
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x0016AF30 File Offset: 0x00169130
	private IEnumerator Super_Jump_End()
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		while (!base.hero.tChar.onGround)
		{
			yield return 0;
			bool flag = base.hero.tChar.rig.velocity.y < 0f;
			if (flag)
			{
				base.hero.charMove.state.fly = false;
			}
		}
		yield break;
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x0016AF3F File Offset: 0x0016913F
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.charMove.canJump = true;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x0016AF7D File Offset: 0x0016917D
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.Super_Jump_Start(__target);
	}
}

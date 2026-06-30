using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class SkillSuperJumpMe : SkillBaseMe
{
	// Token: 0x0600287C RID: 10364 RVA: 0x0016AF9C File Offset: 0x0016919C
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
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool superDoing = ((HeroValkyriesMe)base.hero.heroBase).superDoing;
				if (superDoing)
				{
					result = false;
				}
				else
				{
					bool isSkillSuperJump = ((HeroValkyriesMe)base.hero.heroBase).IsSkillSuperJump;
					result = (!isSkillSuperJump && base.OneSkillCanDo());
				}
			}
		}
		return result;
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x0016B02A File Offset: 0x0016922A
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x0016B03C File Offset: 0x0016923C
	private void Super_Jump_Start(object _target)
	{
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.SetAnimator("SkillJumpEnd", 0, false, 1);
		base.hero.charMove.canJump = false;
		((HeroValkyriesMe)base.hero.heroBase).IsSkillSuperJump = true;
		this._Firetarget = _target;
		Transform transform = CameraControl.Instance.transform;
		float num = CameraControl.Instance.cy;
		bool flag = num > -25f;
		if (flag)
		{
			num = -25f;
		}
		bool flag2 = num < -65f;
		if (flag2)
		{
			num = -65f;
		}
		transform.localEulerAngles = new Vector3(num, transform.localEulerAngles.y, transform.localEulerAngles.z);
		Vector3 power = (transform.forward + transform.up * 0.1f) * this.Skill_Jump_Height;
		((TBCharacterThirdMe)base.hero.tChar).SpeedPowerForce(power, 2.5f);
		base.StartCoroutine(this.Super_Jump_End());
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x0016B169 File Offset: 0x00169369
	private IEnumerator Super_Jump_End()
	{
		bool onground = false;
		bool bisDown = false;
		yield return new WaitForSeconds(0.1f);
		float fcheck = Time.time;
		while (!onground)
		{
			yield return 0;
			bool flag = base.hero.tChar.IsDown && !bisDown;
			if (flag)
			{
				((HeroValkyriesMe)base.hero.heroBase).IgnoreLayer(true);
				bisDown = true;
			}
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			if (flag2)
			{
				onground = true;
			}
			bool flag3 = (base.hero.tChar.GroundDistance < 0.3f && base.hero.tChar.rig.velocity.y <= 0.01f) || base.hero.isDead;
			if (flag3)
			{
				onground = true;
			}
			bool flag4 = Time.time - fcheck > 5f && !onground;
			if (flag4)
			{
				onground = true;
			}
		}
		((HeroValkyriesMe)base.hero.heroBase).IgnoreLayer(false);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0016B178 File Offset: 0x00169378
	public override void OneSkillFinish()
	{
		base.hero.SetAnimator("SkillJumpEnd", 0, true, 1);
		base.RealShoot(this._Firetarget, null);
		base.StartCoroutine(this.Wait_Action());
		base.OneSkillFinish();
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x0016B1B6 File Offset: 0x001693B6
	private IEnumerator Wait_Action()
	{
		yield return new WaitForSeconds(0.5f);
		((HeroValkyriesMe)base.hero.heroBase).IsSkillSuperJump = false;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.wInput.notInput = false;
		base.hero.charMove.canJump = true;
		yield break;
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x0016B1C8 File Offset: 0x001693C8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		bool flag = !((HeroValkyriesMe)base.hero.heroBase).IsSkillSuperJump;
		if (flag)
		{
			base.OneSkillAction(__target, parm2);
			this.Super_Jump_Start(__target);
		}
	}

	// Token: 0x04002315 RID: 8981
	private float Skill_Jump_Height = 22f;

	// Token: 0x04002316 RID: 8982
	private object _Firetarget;
}

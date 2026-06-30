using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class SkillSandstormDashIt : SkillBaseIt
{
	// Token: 0x17000511 RID: 1297
	// (get) Token: 0x060027DA RID: 10202 RVA: 0x001679B8 File Offset: 0x00165BB8
	// (set) Token: 0x060027DB RID: 10203 RVA: 0x001679D0 File Offset: 0x00165BD0
	private bool DashState
	{
		get
		{
			return this._DashState;
		}
		set
		{
			this._DashState = value;
		}
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x001679DA File Offset: 0x00165BDA
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.DashState = false;
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x001679F0 File Offset: 0x00165BF0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.SkillActive = true;
		bool onGround = base.hero.tChar.onGround;
		if (onGround)
		{
			this.SkillAction();
		}
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x00167A2C File Offset: 0x00165C2C
	private void SkillAction()
	{
		bool flag = this.selfAnim == null && base.hero.tCharAnimationTps != null;
		if (flag)
		{
			this.selfAnim = base.hero.tCharAnimationTps.animator;
			base.hero.SetAnimator("SkillDash", 0, true, 1);
		}
		this.DashTimer = 0f;
		base.hero.bePlayActive = false;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.tChar.hangInAir = true;
		base.hero.tChar.PositionMoveWithGravity = true;
		this.SkillActive = false;
		base.StartCoroutine(this.Dash_Handle());
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x00167AF1 File Offset: 0x00165CF1
	private IEnumerator Dash_Handle()
	{
		yield return new WaitForSeconds(0.2f);
		this.DashState = true;
		base.hero.tChar.m_MoveByAnimator = false;
		yield break;
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x00167B00 File Offset: 0x00165D00
	private void Dash()
	{
		bool dashState = this.DashState;
		if (dashState)
		{
			base.hero.tChar.moveDirectionSandDash = base.gameObject.transform.forward * 0.2f;
		}
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x00167B44 File Offset: 0x00165D44
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool dashState = this.DashState;
		if (dashState)
		{
			this.DashState = false;
			base.hero.tChar.m_MoveByAnimator = true;
			base.hero.tChar.hangInAir = false;
			base.hero.tChar.PositionMoveWithGravity = false;
			bool flag = base.hero.playerCommon.m_nDongjie == 0 && base.hero.playerCommon.m_nHunMi == 0;
			if (flag)
			{
				base.hero.bePlayActive = true;
			}
			base.hero.tChar.moveDirectionSandDash = Vector3.zero;
			base.hero.tChar.m_MoveByAnimator = true;
		}
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x00167C08 File Offset: 0x00165E08
	private void Update()
	{
		bool flag = this.selfAnim == null || base.hero == null;
		if (!flag)
		{
			bool flag2 = this.SkillActive && base.hero.tChar.onGround;
			if (flag2)
			{
				this.SkillAction();
			}
			bool dashState = this.DashState;
			if (dashState)
			{
				bool flag3 = base.hero.playerCommon.m_nHunMi == 1 || base.hero.playerCommon.m_nDongjie == 1;
				if (flag3)
				{
					this.OneSkillFinish();
				}
				this.DashTimer += Time.deltaTime;
				bool flag4 = !this.selfAnim.GetBool("SkillDash");
				if (flag4)
				{
					this.OneSkillFinish();
					this.DashState = false;
				}
				this.Dash();
			}
		}
	}

	// Token: 0x040022DF RID: 8927
	private bool _DashState;

	// Token: 0x040022E0 RID: 8928
	private bool SkillActive;

	// Token: 0x040022E1 RID: 8929
	private float DashTimer;

	// Token: 0x040022E2 RID: 8930
	private Animator selfAnim;
}

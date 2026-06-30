using System;
using UnityEngine;

// Token: 0x0200043D RID: 1085
public class TBCharAnimationPlay : TBCharAnimation
{
	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x001804AD File Offset: 0x0017E6AD
	public NetLiving hero
	{
		get
		{
			return this.characterController.userControl.hero;
		}
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x001804BF File Offset: 0x0017E6BF
	public override bool animationGrounded
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x001804C2 File Offset: 0x0017E6C2
	public void InitFpsPlay()
	{
		base.InitCharAnimation();
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x001804CC File Offset: 0x0017E6CC
	public override Vector3 GetPivotPoint()
	{
		return this.animator.pivotPosition;
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x001804EC File Offset: 0x0017E6EC
	public override bool GetAnimatorBool(string ani)
	{
		bool flag = this.animator == null;
		return !flag && this.animator.GetBool(ani);
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x00180520 File Offset: 0x0017E720
	public override float GetAnimatorFloat(string ani)
	{
		bool flag = this.animator == null;
		float result;
		if (flag)
		{
			result = -999f;
		}
		else
		{
			result = this.animator.GetFloat(ani);
		}
		return result;
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x00180558 File Offset: 0x0017E758
	public override int GetAnimatorInteger(string ani)
	{
		bool flag = this.animator == null;
		int result;
		if (flag)
		{
			result = -999;
		}
		else
		{
			result = this.animator.GetInteger(ani);
		}
		return result;
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x00180590 File Offset: 0x0017E790
	protected override void Start()
	{
		bool flag = this.animator == null;
		if (flag)
		{
			this.InitFpsPlay();
		}
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x001805B8 File Offset: 0x0017E7B8
	public override void SetAnimator(string ani, int type, object parm)
	{
		bool flag = this.animator == null;
		if (!flag)
		{
			base.SetAnimator(ani, type, parm);
			bool isActiveAndEnabled = this.animator.isActiveAndEnabled;
			if (isActiveAndEnabled)
			{
				switch (type)
				{
				case 0:
					this.animator.SetBool(ani, (bool)parm);
					break;
				case 1:
					this.animator.SetFloat(ani, (float)parm);
					break;
				case 2:
					this.animator.SetInteger(ani, (int)parm);
					break;
				default:
					Debug.LogError("AnimationPlay����������ͻ���֧��,����LPJ.");
					break;
				}
			}
		}
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x00180658 File Offset: 0x0017E858
	protected override void LateUpdate()
	{
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x0018065C File Offset: 0x0017E85C
	protected override void Update()
	{
		base.Update();
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			this.animator.SetBool("Fly", this.characterController.animState.fly);
			this.animator.SetFloat("Forward", this.characterController.animState.moveDirection.z);
			this.animator.SetFloat("Right", this.characterController.animState.moveDirection.x);
			this.animator.SetFloat("MouseX", this.characterController.animState.MouseX);
			this.animator.SetFloat("MouseY", this.characterController.animState.MouseY);
			this.animator.SetBool("OnGround", this.characterController.animState.onGround);
			bool flag2 = !this.characterController.animState.onGround && !this.characterController.animState.fly;
			if (flag2)
			{
				this.animator.SetFloat("Jump", this.characterController.animState.yVelocity);
			}
			bool flag3 = !this.characterController.animatorAffectBySpeed;
			if (flag3)
			{
				bool onGround = this.characterController.animState.onGround;
				if (onGround)
				{
					this.animator.speed = this.animSpeedMultiplier;
					this.animator.SetFloat("Speed", this.characterController.animSpeedMove);
				}
				else
				{
					this.animator.speed = 1f;
					this.animator.SetFloat("Speed", 1f);
				}
			}
		}
	}

	// Token: 0x0400255B RID: 9563
	public TBCharacterThird characterController;

	// Token: 0x0400255C RID: 9564
	[Range(0.1f, 3f)]
	[SerializeField]
	private float animSpeedMultiplier = 1f;
}

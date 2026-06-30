using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000449 RID: 1097
[RequireComponent(typeof(Animator))]
public class TBCharAnimationThird : TBCharAnimation
{
	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00180C39 File Offset: 0x0017EE39
	public NetLiving hero
	{
		get
		{
			return this.characterController.userControl.hero;
		}
	}

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x00180C4C File Offset: 0x0017EE4C
	public override bool animationGrounded
	{
		get
		{
			return this.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded Directional") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded Strafe");
		}
	}

	// Token: 0x06002BC7 RID: 11207 RVA: 0x00180C90 File Offset: 0x0017EE90
	protected override void Start()
	{
		base.Start();
		this.initAnimation();
	}

	// Token: 0x06002BC8 RID: 11208 RVA: 0x00180CA1 File Offset: 0x0017EEA1
	public void initAnimation()
	{
		base.InitCharAnimation();
		this.ik = base.GetComponent<FullBodyBipedIK>();
	}

	// Token: 0x06002BC9 RID: 11209 RVA: 0x00180CB8 File Offset: 0x0017EEB8
	public override bool GetAnimatorBool(string ani)
	{
		bool flag = this.animator == null;
		return !flag && this.animator.GetBool(ani);
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x00180CEC File Offset: 0x0017EEEC
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

	// Token: 0x06002BCB RID: 11211 RVA: 0x00180D24 File Offset: 0x0017EF24
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

	// Token: 0x06002BCC RID: 11212 RVA: 0x00180D5C File Offset: 0x0017EF5C
	public override void SetAnimator(string ani, int type, object parm)
	{
		bool flag = !(this.animator == null);
		if (flag)
		{
			base.SetAnimator(ani, type, parm);
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
				Debug.LogError("AnimationThird����������ͻ���֧��,����LPJ.");
				break;
			}
		}
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x00180DF0 File Offset: 0x0017EFF0
	public override Vector3 GetPivotPoint()
	{
		return this.animator.pivotPosition;
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x00180E10 File Offset: 0x0017F010
	protected override void Update()
	{
		base.Update();
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			bool flag2 = this.animator == null;
			if (flag2)
			{
				Fun.LogError("�߼�����animatorΪNull", null);
			}
			else
			{
				this.characterController.ActiveSwith = this.animator.GetInteger("ActiveSwith");
				this.hero.nHuDongState = this.animator.GetInteger("HuDongState");
				bool flag3 = Time.time < this.updateRate;
				if (!flag3)
				{
					this.updateRate = Time.time + 0.016f;
					this.animator.SetBool("Fly", this.characterController.animState.fly);
					this.animator.SetFloat("Forward", this.characterController.animState.moveDirection.z);
					this.animator.SetFloat("Right", this.characterController.animState.moveDirection.x);
					this.animator.SetFloat("MouseX", this.characterController.animState.MouseX);
					this.animator.SetFloat("MouseY", this.characterController.animState.MouseY);
					this.animator.SetBool("OnGround", this.characterController.animState.onGround);
					bool flag4 = !this.characterController.animState.onGround;
					if (flag4)
					{
						bool flag5 = !this.characterController.IsDown;
						if (flag5)
						{
							bool flag6 = this.characterController.GroundDistance > this.jumpHeight;
							if (flag6)
							{
								this.jumpHeight = this.characterController.GroundDistance;
							}
						}
						else
						{
							bool flag7 = this.characterController.GroundDistance < this.jumpHeight;
							if (flag7)
							{
								this.jumpHeight = Mathf.Lerp(this.jumpHeight, this.characterController.GroundDistance, Time.deltaTime * 20f);
							}
							else
							{
								bool flag8 = this.jumpHeight == 0f;
								if (flag8)
								{
									this.jumpHeight = Mathf.Lerp(this.jumpHeight, this.characterController.GroundDistance, Time.deltaTime * 20f);
								}
							}
						}
						this.animator.SetFloat("Jump", this.jumpHeight);
					}
					else
					{
						this.jumpHeight = 0f;
					}
					bool flag9 = !this.characterController.animatorAffectBySpeed;
					if (flag9)
					{
						bool onGround = this.characterController.animState.onGround;
						if (onGround)
						{
							this.animator.speed = this.animSpeedMultiplier;
							this.animator.SetFloat("Speed", this.characterController.animSpeedMove);
							this.characterController.isChangeSpd = this.animator.GetFloat("Speed");
						}
						else
						{
							this.animator.speed = 1f;
							this.animator.SetFloat("Speed", 1f);
							this.characterController.isChangeSpd = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x00181158 File Offset: 0x0017F358
	private void OnAnimatorMove()
	{
		bool flag = !(this.characterController == null) && !(this.animator == null);
		if (flag)
		{
			this.characterController.Move(this.animator.deltaPosition, this.animator.deltaRotation);
		}
	}

	// Token: 0x04002560 RID: 9568
	public TBCharacterThird characterController;

	// Token: 0x04002561 RID: 9569
	[Range(0.1f, 3f)]
	[SerializeField]
	public float animSpeedMultiplier = 1f;

	// Token: 0x04002562 RID: 9570
	protected FullBodyBipedIK ik;

	// Token: 0x04002563 RID: 9571
	private float jumpHeight;

	// Token: 0x04002564 RID: 9572
	private const string groundedDirectional = "Grounded Directional";

	// Token: 0x04002565 RID: 9573
	private const string groundedStrafe = "Grounded Strafe";

	// Token: 0x04002566 RID: 9574
	private float updateRate;
}

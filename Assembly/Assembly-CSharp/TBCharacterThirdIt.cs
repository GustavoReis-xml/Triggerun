using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class TBCharacterThirdIt : TBCharacterThird
{
	// Token: 0x06002B2A RID: 11050 RVA: 0x0017BC77 File Offset: 0x00179E77
	protected override void Start()
	{
		base.Start();
		this.smoothAccelerationTime = 0f;
		this.smoothPhysics = false;
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x0017BC93 File Offset: 0x00179E93
	public override void InitCharThird(NetLiving player, TBCharAnimationThird charAni, TBUserControl control, TBCharAnimationPlay charAniPlay)
	{
		base.InitCharThird(player, charAni, control, charAniPlay);
		this.groundCheckDelayLastTime = UnityEngine.Random.Range(1f, 5f);
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x0017BCB8 File Offset: 0x00179EB8
	protected override void FixedUpdate()
	{
		bool flag = !this.isInited;
		if (!flag)
		{
			bool flag2 = this.hero.isDead || this.hero.haveDestroy;
			if (flag2)
			{
				this.r.velocity = Vector3.zero;
			}
			else
			{
				bool flag3 = !base.hangInAir;
				if (flag3)
				{
					bool flag4 = this.animator != null && this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics;
					if (flag4)
					{
						this.smoothPhysics = false;
						this.characterAnimation.smoothFollow = false;
					}
					this.r.interpolation = (this.smoothPhysics ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
					this.characterAnimation.smoothFollow = this.smoothPhysics;
					this.MoveFixed(this.fixedDeltaPosition);
					this.fixedDeltaPosition = Vector3.zero;
				}
				bool flag5 = this.ActiveSwith == 0 && this.hero.nHuDongState < 50;
				if (flag5)
				{
					base.transform.rotation *= this.fixedDeltaRotation;
					this.fixedDeltaRotation = Quaternion.identity;
					this.Rotate();
				}
				bool flag6 = !base.hangInAir;
				if (flag6)
				{
					this.GroundCheck();
					bool flag7 = this.userControl.state.move == Vector3.zero && this.groundDistance < this.airborneThreshold * 0.5f;
					if (flag7)
					{
						base.HighFriction();
					}
					else
					{
						base.ZeroFriction();
					}
					base.ScaleCapsule((!this.userControl.state.crouch) ? 1f : this.crouchCapsuleScaleMlp);
				}
				this.PositionMove();
			}
		}
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x0017BE7C File Offset: 0x0017A07C
	protected override void PositionMove()
	{
		bool flag = this.groundDistance < 0.2f;
		if (flag)
		{
			this.delayUpdateTime = 0f;
		}
		bool flag2 = false;
		float num = Vector3.Distance(((TBUserControlIt)this.userControl).moveTarget, base.transform.position);
		bool flag3 = !this._moveByControlIt;
		if (flag3)
		{
			float num2 = Mathf.Abs(((TBUserControlIt)this.userControl).moveTarget.y - base.transform.position.y);
			bool flag4 = num > this.limitNowDis;
			if (flag4)
			{
				flag2 = true;
			}
			bool flag5 = num2 > 3f;
			if (flag5)
			{
				flag2 = true;
			}
			bool mayBeStand = ((TBUserControlIt)this.userControl).mayBeStand;
			if (mayBeStand)
			{
				bool flag6 = num > 2f;
				if (flag6)
				{
					flag2 = true;
				}
				bool flag7 = num2 > 0.3f;
				if (flag7)
				{
					flag2 = true;
				}
			}
		}
		bool flag8 = this._moveByControlIt || flag2 || !this.onGround || this.onAir || base.hangInAir || this.animState.fly || Time.time < this.delayUpdateTime;
		if (flag8)
		{
			this.lerpMoveSpeed += Time.deltaTime;
			this.r.velocity = Vector3.zero;
			bool flag9 = flag2;
			if (flag9)
			{
				this.r.MovePosition(((TBUserControlIt)this.userControl).moveTarget);
			}
			else
			{
				float num3 = (!this.animState.fly) ? Mathf.Lerp(10f, 50f, num / 5f) : 10f;
				this.r.MovePosition(Vector3.Lerp(base.transform.position, ((TBUserControlIt)this.userControl).moveTarget, Time.deltaTime * num3));
			}
		}
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x0017C060 File Offset: 0x0017A260
	private Vector3 Vector3SetMaxOne(Vector3 v3)
	{
		bool flag = v3 == Vector3.zero;
		Vector3 result;
		if (flag)
		{
			result = v3;
		}
		else
		{
			float num = (Mathf.Abs(v3.x) <= Mathf.Abs(v3.y)) ? v3.y : v3.x;
			num = ((Mathf.Abs(num) <= Mathf.Abs(v3.z)) ? v3.z : num);
			num = Mathf.Abs(1f / num);
			result = v3 * num;
		}
		return result;
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x0017C0E0 File Offset: 0x0017A2E0
	protected override Vector3 GetMoveDirection()
	{
		bool hangInAir = base.hangInAir;
		Vector3 result;
		if (hangInAir)
		{
			result = Vector3.zero;
		}
		else
		{
			TBCharacterThird.MoveMode moveMode = this.moveMode;
			TBCharacterThird.MoveMode moveMode2 = moveMode;
			if (moveMode2 != TBCharacterThird.MoveMode.Strafe)
			{
				if (moveMode2 != TBCharacterThird.MoveMode.Directional)
				{
					result = Vector3.zero;
				}
				else
				{
					this.moveDirection = Vector3.SmoothDamp(this.moveDirection, new Vector3(0f, 0f, this.userControl.state.move.magnitude), ref this.moveDirectionVelocity, this.smoothAccelerationTime);
					this.moveDirection = Vector3.MoveTowards(this.moveDirection, new Vector3(0f, 0f, this.userControl.state.move.magnitude), Time.deltaTime * this.linearAccelerationSpeed);
					result = this.moveDirection * this.forwardMlp;
				}
			}
			else
			{
				this.moveDirection = Vector3.SmoothDamp(this.moveDirection, this.userControl.state.move, ref this.moveDirectionVelocity, this.smoothAccelerationTime);
				this.moveDirection = Vector3.MoveTowards(this.moveDirection, this.userControl.state.move, Time.deltaTime * this.linearAccelerationSpeed);
				result = this.Vector3SetMaxOne(base.transform.InverseTransformDirection(this.moveDirection));
			}
		}
		return result;
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x0017C234 File Offset: 0x0017A434
	protected override void Update()
	{
		bool flag = !this.isInited;
		if (!flag)
		{
			base.Update();
			this.onGround = this.userControl.state.onground;
			this.onAir = this.userControl.state.onair;
			bool flag2 = this.animState.onGround != this.onGround;
			if (flag2)
			{
				this.animState.onGround = this.onGround;
				bool onGround = this.onGround;
				if (onGround)
				{
					this.delayUpdateTime = Time.time + 1f;
				}
				else
				{
					this.lerpMoveSpeed = 0f;
					this.delayUpdateTime = 0f;
				}
				bool bePlayer = this.hero.bePlayer;
				if (bePlayer)
				{
					this.hero.playerSound.PlayJumpOverSound();
				}
			}
			bool flag3 = !this.isDown;
			if (flag3)
			{
				this.animState.yVelocity = Mathf.Lerp(this.animState.yVelocity, this.jumpVelocity.y, Time.deltaTime * 5f);
			}
			else
			{
				this.animState.yVelocity = Mathf.Lerp(this.animState.yVelocity, -15f, Time.deltaTime * 2f);
			}
		}
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x0017C380 File Offset: 0x0017A580
	private void MoveFixed(Vector3 deltaPosition)
	{
		Vector3 vector = deltaPosition / Time.deltaTime;
		vector += new Vector3(this.platformVelocity.x, 0f, this.platformVelocity.z);
		bool flag = this.onGround && !this.onAir && this.velocityToGroundTangentWeight > 0f;
		if (flag)
		{
			Quaternion b = Quaternion.FromToRotation(base.transform.up, this.normal);
			vector = Quaternion.Lerp(Quaternion.identity, b, this.velocityToGroundTangentWeight) * vector;
		}
		this.Jump();
		bool flag2 = this.wallState != TBCharacter.WallState.OnWall;
		if (flag2)
		{
			vector.y = Mathf.Clamp(this.r.velocity.y, this.r.velocity.y, (!this.onGround) ? this.r.velocity.y : this.maxVerticalVelocityOnGround);
		}
		this.r.velocity = vector;
		this.WallRun();
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x0017C490 File Offset: 0x0017A690
	protected override void WallRun()
	{
		bool flag = !this.wallRunCan;
		if (!flag)
		{
			bool flag2 = this.CanWallRun();
			bool flag3 = this.wallRunWeight > 0f && !flag2;
			if (flag3)
			{
				this.wallRunEndTime = Time.time;
			}
			bool flag4 = Time.time < this.wallRunEndTime + 0.5f;
			if (flag4)
			{
				flag2 = false;
			}
			this.wallRunWeight = Mathf.MoveTowards(this.wallRunWeight, (!flag2) ? 0f : 1f, Time.deltaTime * this.wallRunWeightSpeed);
			bool flag5 = this.wallRunWeight <= 0f && this.lastWallRunWeight > 0f;
			if (flag5)
			{
				base.transform.rotation = Quaternion.LookRotation(new Vector3(base.transform.forward.x, 0f, base.transform.forward.z), Vector3.up);
				this.wallNormal = Vector3.up;
			}
			this.lastWallRunWeight = this.wallRunWeight;
			bool flag6 = this.wallRunWeight > 0f;
			if (flag6)
			{
				bool flag7 = this.onGround && this.r.velocity.y < 0f;
				if (flag7)
				{
					this.r.velocity = new Vector3(this.r.velocity.x, 0f, this.r.velocity.z);
				}
				Vector3 vector = base.transform.forward;
				bool flag8 = vector.y >= 0.8f;
				if (flag8)
				{
					vector = Quaternion.AngleAxis(90f, base.transform.right) * vector;
				}
				RaycastHit raycastHit = new RaycastHit
				{
					normal = Vector3.up
				};
				Physics.Raycast(this.capsule.bounds.center, vector, out raycastHit, 1.5f, this.wallRunLayers);
				this.wallNormal = Vector3.Lerp(this.wallNormal, raycastHit.normal, Time.deltaTime * this.wallRunRotationSpeed);
				this.wallNormal = Vector3.RotateTowards(Vector3.up, this.wallNormal, this.wallRunMaxRotationAngle * 0.017453292f, 0f);
				Vector3 forward = base.transform.forward;
				Vector3 wallNormal = this.wallNormal;
				Vector3.OrthoNormalize(ref wallNormal, ref forward);
				bool flag9 = vector != Vector3.zero;
				if (flag9)
				{
					base.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector, Vector3.up), Quaternion.LookRotation(forward, this.wallNormal), this.wallRunWeight);
				}
			}
		}
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x0017C754 File Offset: 0x0017A954
	protected override bool CanWallRun()
	{
		this.wallState = (TBCharacter.WallState)this.userControl.state.onwall;
		bool flag = this.wallState == TBCharacter.WallState.PaQiangEnd;
		bool result;
		if (flag)
		{
			this.wallState = TBCharacter.WallState.Fall;
			this.userControl.state.onwall = (int)this.wallState;
			this.r.velocity += Vector3.up * this.wallRunJumpAmount;
			result = false;
		}
		else
		{
			bool flag2 = this.wallState == TBCharacter.WallState.OnWall;
			result = flag2;
		}
		return result;
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x0017C7E8 File Offset: 0x0017A9E8
	protected override bool Jump()
	{
		bool flag = !this.userControl.state.onjump;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			this.userControl.state.onjump = false;
			this.userControl.state.onground = false;
			this.jumpEndTime = Time.time + 0.1f;
			this.isDown = false;
			this.lastPos.y = -10000f;
			this.jumpVelocity = base.transform.up * this.jumpPower;
			bool flag2 = this.jumpVelocity.y > 6f;
			if (flag2)
			{
				this.jumpVelocity.y = 6f;
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06002B35 RID: 11061 RVA: 0x0017C8A8 File Offset: 0x0017AAA8
	private void GroundCheck()
	{
		bool groundCheckDelaySwitch = this.groundCheckDelaySwitch;
		if (groundCheckDelaySwitch)
		{
			bool flag = Time.time <= this.groundCheckDelayLastTime;
			if (flag)
			{
				return;
			}
			this.groundCheckDelayLastTime = Time.time + this.groundCheckDelayTime;
		}
		Vector3 b = Vector3.zero;
		this.platformAngularVelocity = Vector3.zero;
		this.hit = this.GetSpherecastHit(false, out this.angClimb);
		this.normal = base.transform.up;
		this.groundDistance = this.r.position.y - this.hit.point.y;
		this.lerpAngleClimb = Mathf.Lerp(this.lerpAngleClimb, this.angClimb, Time.deltaTime * 20f);
		this.isHill = false;
		bool flag2 = this.lerpAngleClimb > this.upHillAngle;
		if (flag2)
		{
			this.groundDistance = Mathf.Lerp(this.groundDistance, 100f, Time.deltaTime * 20f);
			this.isHill = true;
		}
		bool flag3 = this.r.position.y > this.lastPos.y;
		if (flag3)
		{
			this.lastPos.y = this.r.position.y;
		}
		bool flag4 = this.r.position.y - this.lastPos.y < -0.03f;
		if (flag4)
		{
			this.isDown = true;
		}
		else
		{
			this.isDown = false;
		}
		bool flag5 = this.hit.rigidbody != null;
		if (flag5)
		{
			b = this.hit.rigidbody.GetPointVelocity(this.hit.point);
			this.platformAngularVelocity = Vector3.Project(this.hit.rigidbody.angularVelocity, base.transform.up);
		}
		this.platformVelocity = Vector3.Lerp(this.platformVelocity, b, Time.deltaTime * this.platformFriction);
	}

	// Token: 0x04002516 RID: 9494
	private float delayUpdateTime;

	// Token: 0x04002517 RID: 9495
	private float lerpMoveSpeed;

	// Token: 0x04002518 RID: 9496
	private Vector3 jumpVelocity = Vector3.zero;

	// Token: 0x04002519 RID: 9497
	private Vector3 lastPos = Vector3.zero;

	// Token: 0x0400251A RID: 9498
	private const float upJumpVector = 6f;

	// Token: 0x0400251B RID: 9499
	private const float downJumpVector = -15f;

	// Token: 0x0400251C RID: 9500
	private Vector3 lastnormal;

	// Token: 0x0400251D RID: 9501
	private float lerpAngleClimb;

	// Token: 0x0400251E RID: 9502
	private float angClimb;
}

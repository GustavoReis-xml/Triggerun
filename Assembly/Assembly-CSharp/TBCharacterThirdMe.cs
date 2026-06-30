using System;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class TBCharacterThirdMe : TBCharacterThird
{
	// Token: 0x06002B37 RID: 11063 RVA: 0x0017CAC4 File Offset: 0x0017ACC4
	protected override void Start()
	{
		base.Start();
		this.smoothAccelerationTime = 0f;
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x0017CAD9 File Offset: 0x0017ACD9
	public override void InitCharThird(NetLiving player, TBCharAnimationThird charAni, TBUserControl control, TBCharAnimationPlay charAniPlay)
	{
		base.InitCharThird(player, charAni, control, charAniPlay);
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x0017CAE8 File Offset: 0x0017ACE8
	protected override void FixedUpdate()
	{
		bool flag = !this.isInited;
		if (!flag)
		{
			bool isDead = this.hero.isDead;
			if (isDead)
			{
				this.r.velocity = Vector3.zero;
			}
			else
			{
				bool flag2 = !base.hangInAir;
				if (flag2)
				{
					bool flag3 = this.animator != null && this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics;
					if (flag3)
					{
						this.smoothPhysics = false;
						this.characterAnimation.smoothFollow = false;
					}
					this.r.interpolation = (this.smoothPhysics ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
					this.characterAnimation.smoothFollow = this.smoothPhysics;
					this.MoveFixed(this.fixedDeltaPosition);
					this.fixedDeltaPosition = Vector3.zero;
				}
				bool flag4 = this.ActiveSwith == 0;
				if (flag4)
				{
					base.transform.rotation *= this.fixedDeltaRotation;
					this.fixedDeltaRotation = Quaternion.identity;
					this.Rotate();
				}
				bool flag5 = !base.hangInAir;
				if (flag5)
				{
					this.GroundCheck();
					bool isHill = this.isHill;
					if (isHill)
					{
						base.ZeroFriction();
					}
					else
					{
						bool flag6 = this.userControl.state.move == Vector3.zero && !base.ForceZeroPhysic && this.groundDistance < this.airborneThreshold * 0.5f;
						if (flag6)
						{
							base.HighFriction();
						}
						else
						{
							base.ZeroFriction();
						}
					}
					bool flag7 = this.onGround && !this.onAir && !this.onSlope;
					if (flag7)
					{
						this.animState.jump = this.Jump();
					}
					else
					{
						bool flag8 = !this.notVelocity && this.moveAuto == Vector3.zero;
						if (flag8)
						{
							this.r.AddForce(Physics.gravity * (this.gravityMultiplier - 1f) * base.rig.mass);
						}
					}
					this.ForceMove();
					base.ScaleCapsule((!this.userControl.state.crouch) ? 1f : this.crouchCapsuleScaleMlp);
					base.OnNoneAnimatorMove();
					bool notVelocity = this.notVelocity;
					if (notVelocity)
					{
						this.r.AddForce(-Physics.gravity * base.rig.mass);
					}
				}
				this.SandDashMove();
				this.PositionVelocityMove();
				this.PositionMove();
			}
		}
	}

	// Token: 0x06002B3A RID: 11066 RVA: 0x0017CD87 File Offset: 0x0017AF87
	public void SetjumpEndTime(float t)
	{
		this.jumpEndTime = Time.time + t;
	}

	// Token: 0x06002B3B RID: 11067 RVA: 0x0017CD98 File Offset: 0x0017AF98
	public void SpeedMoveVelocity(Vector3 speed, float alltime, bool breakForceEnd)
	{
		this._breakForce = breakForceEnd;
		this.velocityTime = alltime;
		bool flag = this.speedMoveVelocity == Vector3.zero;
		if (flag)
		{
			this.speedMoveVelocity = speed;
			bool flag2 = this.speedMoveVelocity.y > 0f;
			if (flag2)
			{
				this.onGround = false;
			}
			this.oldVelocity = this.r.velocity;
			this.r.velocity += this.speedMoveVelocity;
		}
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x0017CE20 File Offset: 0x0017B020
	public void VelocitySetZero(bool breakDown = true)
	{
		Vector3 velocity = this.r.velocity;
		bool flag = !breakDown || velocity.y <= 0f;
		if (flag)
		{
			this.r.velocity = Vector3.zero;
		}
	}

	// Token: 0x06002B3D RID: 11069 RVA: 0x0017CE68 File Offset: 0x0017B068
	public void VelocityYSetZero(bool breakDown = true)
	{
		Vector3 velocity = this.r.velocity;
		bool flag = !breakDown || velocity.y <= 0f;
		if (flag)
		{
			velocity.y = 0f;
			this.r.velocity = velocity;
		}
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x0017CEB7 File Offset: 0x0017B0B7
	public void SpeedMoveForce(Vector3 speed, float alltime, bool _disableSpeedMoveVelocity = false, float upVector = 1000f, float frontVector = 1000f)
	{
		this.disableBySpeedMoveVelocity = _disableSpeedMoveVelocity;
		this.forceTime = alltime;
		this.speedMoveForce = speed;
		this.speedForceYVectorMax = upVector;
		this.speedFrontVectorMax = frontVector;
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x0017CEDF File Offset: 0x0017B0DF
	public void SetForceTime(float t)
	{
		this.forceTime = t;
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x0017CEEC File Offset: 0x0017B0EC
	private void SandDashMove()
	{
		bool flag = this.moveDirectionSandDash != Vector3.zero;
		if (flag)
		{
			this.r.velocity = new Vector3(0f, this.r.velocity.y, 0f);
			this.r.velocity += this.moveDirectionSandDash;
		}
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x0017CF58 File Offset: 0x0017B158
	protected override void PositionMove()
	{
		bool flag = this.moveDirectionNoForce != Vector3.zero;
		if (flag)
		{
			this.r.velocity = Vector3.zero;
			this.r.angularVelocity = Vector3.zero;
			this.r.MovePosition(this.r.position + this.moveDirectionNoForce);
		}
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x0017CFC0 File Offset: 0x0017B1C0
	public void MoveDirectionByVector(bool switchMove, Vector3 dir)
	{
		this.moveDirectionVector = dir;
		this.moveByDirectionVector = switchMove;
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x0017CFD4 File Offset: 0x0017B1D4
	protected void PositionVelocityMove()
	{
		bool moveByDirectionVector = this.moveByDirectionVector;
		if (moveByDirectionVector)
		{
			this.r.velocity = this.moveDirectionVector;
		}
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x0017D000 File Offset: 0x0017B200
	private void ForceMove()
	{
		bool flag = this.speedMoveVelocity != Vector3.zero;
		if (flag)
		{
			bool flag2 = this.velocityTime <= 0f;
			if (flag2)
			{
				bool flag3 = !this.onGround && this._breakForce;
				if (flag3)
				{
					this._breakForce = false;
					bool flag4 = !this.onGround;
					if (flag4)
					{
						this.r.velocity = this.oldVelocity;
					}
				}
				this.speedMoveVelocity = Vector3.zero;
			}
			this.velocityTime -= Time.deltaTime;
		}
		bool flag5 = this.speedMoveForce != Vector3.zero;
		if (flag5)
		{
			bool flag6 = !this.disableBySpeedMoveVelocity || (this.disableBySpeedMoveVelocity && this.speedMoveVelocity == Vector3.zero);
			if (flag6)
			{
				bool flag7 = this.forceTime <= 0f;
				if (flag7)
				{
					this.speedMoveForce = Vector3.zero;
					this.disableBySpeedMoveVelocity = false;
				}
				else
				{
					bool flag8 = this.r.velocity.y < this.speedForceYVectorMax && Mathf.Abs(this.r.velocity.x * this.r.velocity.z) < this.speedFrontVectorMax;
					if (flag8)
					{
						this.r.AddForce(this.speedMoveForce);
					}
				}
				bool flag9 = this.speedMoveForce.y > 0f;
				if (flag9)
				{
					this.onGround = false;
				}
			}
			this.forceTime -= Time.deltaTime;
		}
		bool flag10 = this.forceLevelTime > Time.time;
		if (flag10)
		{
			this.r.AddForce(this.forceLevel * 10f);
		}
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x0017D1D8 File Offset: 0x0017B3D8
	protected override void Update()
	{
		bool isInited = this.isInited;
		if (isInited)
		{
			base.Update();
			this.userControl.state.onground = this.onGround;
			this.userControl.state.onair = this.onAir;
			this.animState.onGround = this.onGround;
			bool flag = this.wallState == TBCharacter.WallState.OnWall;
			if (flag)
			{
				this.animState.moveDirection.x = this.animState.moveDirection.z;
				this.animState.moveDirection.z = 0f - this.animState.moveDirection.y;
				this.animState.moveDirection.y = this.animState.moveDirection.x;
				this.animState.moveDirection.x = 0f;
			}
			this.animState.yVelocity = Mathf.Lerp(this.animState.yVelocity, this.r.velocity.y, Time.deltaTime * 10f);
		}
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x0017D2FC File Offset: 0x0017B4FC
	private void MoveFixed(Vector3 deltaPosition)
	{
		Vector3 vector = deltaPosition / Time.deltaTime;
		vector += new Vector3(this.platformVelocity.x, 0f, this.platformVelocity.z);
		bool flag = this.onGround && !this.onAir;
		if (flag)
		{
			bool flag2 = this.velocityToGroundTangentWeight > 0f;
			if (flag2)
			{
				Quaternion b = Quaternion.FromToRotation(base.transform.up, this.normal);
				vector = Quaternion.Lerp(Quaternion.identity, b, this.velocityToGroundTangentWeight) * vector;
			}
			vector += this.speedMoveVelocity;
		}
		else
		{
			Vector3 vector2 = this.userControl.state.move.normalized;
			vector2 = new Vector3(vector2.x * this.airSpeedMax, 0f, vector2.z * this.airSpeedMax);
			this.lastAirForce = this.r.velocity - this.speedMoveVelocity;
			this.lastAirForce.y = 0f;
			Vector3 vector3 = Vector3.Lerp(this.lastAirForce, Vector3.zero, Time.deltaTime * this.airDrag);
			bool flag3 = vector3.magnitude > vector2.magnitude;
			if (flag3)
			{
				float num = Vector3.Dot(vector2.normalized, new Vector3(vector3.x, 0f, vector3.z).normalized);
				float d = (0f - (num - 1f)) * 0.5f;
				vector2 *= d;
			}
			vector = Vector3.Lerp(vector3, vector2 + vector3, Time.deltaTime * this.airControl * ((!this.isHill) ? 1f : this.airHillControl));
			vector.y = this.r.velocity.y;
			vector += this.speedMoveVelocity;
		}
		bool flag4 = this.onGround && !this.onAir && Time.time > this.jumpEndTime;
		if (flag4)
		{
			this.r.velocity -= base.transform.up * this.stickyForce * Time.deltaTime;
		}
		bool flag5 = this.wallState != TBCharacter.WallState.OnWall && !this.onAir;
		if (flag5)
		{
			vector.y = Mathf.Clamp(this.r.velocity.y, this.r.velocity.y, (!this.onGround) ? this.r.velocity.y : this.maxVerticalVelocityOnGround);
		}
		this.r.velocity = vector;
		this.WallRun();
	}

	// Token: 0x06002B47 RID: 11079 RVA: 0x0017D5D4 File Offset: 0x0017B7D4
	protected override bool CanWallRun()
	{
		bool flag = !this.canWall;
		if (flag)
		{
			this.wallState = TBCharacter.WallState.Fall;
		}
		bool flag2 = this.wallState == TBCharacter.WallState.Fall;
		bool result;
		if (flag2)
		{
			this.wallState = TBCharacter.WallState.Fall;
			this.userControl.state.onwall = (int)this.wallState;
			bool flag3 = this.onGround && this.groundDistance < 0.5f;
			if (flag3)
			{
				this.wallState = TBCharacter.WallState.OnGround;
				this.userControl.state.onwall = (int)this.wallState;
			}
			result = false;
		}
		else
		{
			bool flag4 = this.wallState == TBCharacter.WallState.PaQiangEnd;
			if (flag4)
			{
				this.wallState = TBCharacter.WallState.Fall;
				this.userControl.state.onwall = (int)this.wallState;
				this.onGround = false;
				this.r.velocity += Vector3.up * this.wallRunJumpAmount;
				result = false;
			}
			else
			{
				bool flag5 = this.wallState == TBCharacter.WallState.OnWall && Time.time > this.wallTime;
				if (flag5)
				{
					this.r.velocity += Vector3.up * this.wallRunJumpAmount;
					this.wallState = TBCharacter.WallState.Fall;
					this.userControl.state.onwall = (int)this.wallState;
					result = false;
				}
				else
				{
					bool flag6 = this.animState.moveDirection.magnitude < 0.8f;
					if (flag6)
					{
						bool flag7 = this.wallState == TBCharacter.WallState.OnWall;
						if (flag7)
						{
							this.wallState = TBCharacter.WallState.Fall;
						}
						this.userControl.state.onwall = (int)this.wallState;
						result = false;
					}
					else
					{
						bool flag8 = this.animState.moveDirection.z < 0f;
						if (flag8)
						{
							bool flag9 = this.wallState == TBCharacter.WallState.OnWall;
							if (flag9)
							{
								this.wallState = TBCharacter.WallState.Fall;
							}
							this.userControl.state.onwall = (int)this.wallState;
							result = false;
						}
						else
						{
							bool flag10 = this.wallState == TBCharacter.WallState.OnGround && this.groundDistance > 0.6f;
							if (flag10)
							{
								Vector3 forward = base.transform.forward;
								forward.y = 0f;
								this.PaqiangDir = forward;
								RaycastHit raycastHit = new RaycastHit
								{
									normal = Vector3.up
								};
								bool flag11 = Physics.Raycast(base.transform.position + base.transform.up * 0.3f + Vector3.up * 0.4f, forward, out raycastHit, 0.8f, this.wallRunLayers);
								if (flag11)
								{
									float num = Vector3.Angle(raycastHit.normal, Vector3.up);
									bool flag12 = num > 80f && num < 100f;
									if (flag12)
									{
										this.wallState = TBCharacter.WallState.OnWall;
										this.wallTime = Time.time + this.wallRunMaxLength;
										this.userControl.state.onwall = (int)this.wallState;
										this.WallPoint = base.transform.position;
										this.wallNormalStart = -raycastHit.normal;
										this.checkStatTimer = 0f;
										this.AngleTimer = 0f;
										this.nullColliderTimer = 0f;
									}
								}
							}
							bool flag13 = this.wallState == TBCharacter.WallState.OnWall;
							result = flag13;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x0017D950 File Offset: 0x0017BB50
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
			bool flag6 = this.wallRunWeight <= 0f && this.wallState == TBCharacter.WallState.OnWall;
			if (flag6)
			{
				this.wallState = TBCharacter.WallState.Fall;
				this.userControl.state.onwall = (int)this.wallState;
			}
			else
			{
				bool flag7 = this.wallRunWeight <= 0f;
				if (!flag7)
				{
					RaycastHit raycastHit = default(RaycastHit);
					Physics.Raycast(base.transform.position - this.PaqiangDir * 0.5f, Vector3.up, out raycastHit, this.capsule.height + 0.1f, this.wallRunLayers);
					bool flag8 = raycastHit.collider != null;
					if (flag8)
					{
						this.nullColliderTimer += Time.deltaTime;
						bool flag9 = this.nullColliderTimer > 0.1f;
						if (flag9)
						{
							this.nullColliderTimer = 0f;
							this.wallState = TBCharacter.WallState.Fall;
							this.userControl.state.onwall = (int)this.wallState;
						}
					}
					else
					{
						this.nullColliderTimer = 0f;
					}
					bool flag10 = this.onGround && this.r.velocity.y < 0f;
					if (flag10)
					{
						this.r.velocity = new Vector3(this.r.velocity.x, 0f, this.r.velocity.z);
					}
					RaycastHit raycastHit2 = new RaycastHit
					{
						normal = Vector3.up
					};
					Vector3 forward = this.hero.camTarget.forward;
					forward.y = 0f;
					bool flag11 = this.wallState == TBCharacter.WallState.OnWall;
					if (flag11)
					{
						Vector3 origin = base.transform.position + base.transform.up * 0.3f + Vector3.up * -0.1f;
						Physics.Raycast(origin, this.PaqiangDir, out raycastHit2, 0.8f, this.wallRunLayers);
						bool flag12 = raycastHit2.collider == null;
						if (flag12)
						{
							this.wallState = TBCharacter.WallState.PaQiangEnd;
							this.userControl.state.onwall = (int)this.wallState;
						}
						bool flag13 = raycastHit2.collider != null;
						if (flag13)
						{
							float num = Vector3.Angle(raycastHit2.normal, Vector3.up);
							bool flag14 = num < 80f || num > 100f;
							if (flag14)
							{
								this.AngleTimer += Time.deltaTime;
								bool flag15 = this.AngleTimer > 0.2f;
								if (flag15)
								{
									this.AngleTimer = 0f;
									this.wallState = TBCharacter.WallState.Fall;
									this.userControl.state.onwall = (int)this.wallState;
								}
							}
							else
							{
								bool flag16 = num > -10f && num < 10f;
								if (flag16)
								{
									this.AngleTimer += Time.deltaTime;
									bool flag17 = this.AngleTimer > 0.2f;
									if (flag17)
									{
										this.AngleTimer = 0f;
										this.wallState = TBCharacter.WallState.Fall;
										this.userControl.state.onwall = (int)this.wallState;
									}
								}
								else
								{
									this.AngleTimer = 0f;
								}
							}
						}
						this.checkStatTimer += Time.deltaTime;
						bool flag18 = this.checkStatTimer > 0.3f;
						if (flag18)
						{
							this.checkStatTimer = 0f;
							float num2 = Vector3.Distance(this.WallPoint, base.transform.position);
							this.WallPoint = base.transform.position;
							bool flag19 = num2 < 0.07f;
							if (flag19)
							{
								this.wallState = TBCharacter.WallState.Fall;
								this.userControl.state.onwall = (int)this.wallState;
							}
						}
					}
					this.wallNormal = Vector3.Lerp(this.wallNormal, raycastHit2.normal, Time.deltaTime * this.wallRunRotationSpeed);
					this.wallNormal = Vector3.RotateTowards(Vector3.up, this.wallNormal, this.wallRunMaxRotationAngle * 0.017453292f, 0f);
					Vector3 forward2 = base.transform.forward;
					Vector3 wallNormal = this.wallNormal;
					Vector3.OrthoNormalize(ref wallNormal, ref forward2);
					base.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(forward, Vector3.up), Quaternion.LookRotation(forward2, this.wallNormal), this.wallRunWeight);
				}
			}
		}
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x0017DEF4 File Offset: 0x0017C0F4
	protected override bool Jump()
	{
		bool flag = this.wallState == TBCharacter.WallState.OnWall;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = !this.nextJump && !this.userControl.state.jump;
			if (flag2)
			{
				this.nextJump = true;
			}
			bool onAir = this.onAir;
			if (onAir)
			{
				result = false;
			}
			else
			{
				bool flag3 = !this.nextJump;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = !this.userControl.state.jump;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool crouch = this.userControl.state.crouch;
						if (crouch)
						{
							result = false;
						}
						else
						{
							bool flag5 = Time.time < this.lastAirTime + this.jumpRepeatDelayTime;
							if (flag5)
							{
								result = false;
							}
							else
							{
								this.onGround = false;
								this.nextJump = false;
								this.userControl.state.onjump = true;
								this.jumpEndTime = Time.time + 0.1f;
								Vector3 velocity = this.userControl.state.move.normalized * this.jumpPowerForward;
								this.r.velocity = velocity;
								this.r.velocity += base.transform.up * this.jumpPower;
								this.isDown = false;
								result = true;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x0017E068 File Offset: 0x0017C268
	private void GroundCheck()
	{
		Vector3 b = Vector3.zero;
		this.platformAngularVelocity = Vector3.zero;
		float stickyForce = 0f;
		this.hit = this.GetSpherecastHit(true, out this.angClimb);
		this.lerpAngleClimb = Mathf.Lerp(this.lerpAngleClimb, this.angClimb, Time.deltaTime * 20f);
		this.normal = this.hit.normal;
		bool flag = this.normal.y > 0.71f;
		if (flag)
		{
			this.acceleraSlope = 0f;
			this.onSlope = false;
		}
		else
		{
			this.onSlope = true;
		}
		Vector3 point = this.hit.point;
		bool flag2 = this.hit.collider == null;
		if (flag2)
		{
			point.y = -1000000f;
		}
		this.groundDistance = this.r.position.y - point.y;
		bool flag3 = this.groundDistance < -1f;
		if (flag3)
		{
			Debug.LogError(string.Format("玩家坐标是负数，这个是不允许的 groundDistance: {0:F5}。", this.groundDistance));
		}
		bool flag4 = Time.time > this.jumpEndTime && this.r.velocity.y < this.jumpPower * 0.5f && !this.onAir;
		bool flag5 = this.r.velocity.y < 0f;
		if (flag5)
		{
			this.isDown = true;
		}
		else
		{
			this.isDown = false;
		}
		bool flag6 = flag4;
		if (flag6)
		{
			bool onGround = this.onGround;
			this.onGround = false;
			float num = onGround ? this.airborneThreshold : (this.airborneThreshold * 0.5f);
			Vector3 velocity = this.r.velocity;
			velocity.y = 0f;
			float magnitude = velocity.magnitude;
			bool flag7 = this.groundDistance < num && this.lerpAngleClimb < this.upHillAngle;
			if (flag7)
			{
				stickyForce = this.groundStickyEffect * magnitude * num;
				bool flag8 = this.hit.rigidbody != null;
				if (flag8)
				{
					b = this.hit.rigidbody.GetPointVelocity(this.hit.point);
					this.platformAngularVelocity = Vector3.Project(this.hit.rigidbody.angularVelocity, base.transform.up);
				}
				bool flag9 = !onGround && this.hero.bePlayer;
				if (flag9)
				{
					((GamePlayer)this.hero).playerSound.PlayJumpOverSound();
				}
				this.onGround = true;
				bool flag10 = !this.userControl.state.jump;
				if (flag10)
				{
					this.nextJump = true;
				}
			}
			bool flag11 = this.lerpAngleClimb > this.upHillAngle;
			if (flag11)
			{
				this.isHill = true;
				bool isDown = this.isDown;
				if (isDown)
				{
					this.groundDistance = Mathf.Lerp(this.groundDistance, 100f, Time.deltaTime * 20f);
				}
			}
			else
			{
				this.isHill = false;
			}
		}
		this.platformVelocity = Vector3.Lerp(this.platformVelocity, b, Time.deltaTime * this.platformFriction);
		this.stickyForce = stickyForce;
		bool flag12 = !this.onGround;
		if (flag12)
		{
			this.lastAirTime = Time.time;
		}
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x0017E3C8 File Offset: 0x0017C5C8
	private bool getVelpcityOnSlope(Vector3 deltaPosition)
	{
		Vector3 a = deltaPosition / Time.deltaTime;
		Vector3 to = Vector3.zero;
		to = TBCharacterThirdMe.SlopeNormal(base.transform.position + a * 0.8f);
		float num = TBCharacterThirdMe.SlopeHeight(base.transform.position + a * 0.8f);
		bool flag = Mathf.Abs(num - base.transform.position.y) > 0.5f;
		bool result;
		if (flag)
		{
			float num2 = Vector3.Angle(Vector3.up, to);
			Vector3 a2 = Quaternion.AngleAxis(0f - num2, base.transform.right) * base.transform.forward;
			bool flag2 = num2 > 45f;
			if (flag2)
			{
				bool flag3 = this.normal.y < 0.9f;
				if (flag3)
				{
					this.r.velocity = -a2 * (10f - this.fixedDeltaPosition.magnitude);
					this.fixedDeltaPosition = Vector3.zero;
					result = true;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				result = false;
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x0017E500 File Offset: 0x0017C700
	public static float SlopeHeight(Vector3 point)
	{
		Vector3 vector = point;
		vector.y += 5f;
		Ray ray = new Ray(vector, Vector3.down);
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(ray, out raycastHit);
		float result;
		if (flag)
		{
			Debug.DrawRay(vector, Vector3.down, Color.red);
			result = raycastHit.point.y;
		}
		else
		{
			Debug.DrawRay(vector, Vector3.down, Color.red);
			result = -1f;
		}
		return result;
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x0017E578 File Offset: 0x0017C778
	public static Vector3 SlopeNormal(Vector3 point)
	{
		Vector3 origin = point;
		origin.y += 1f;
		Ray ray = new Ray(origin, Vector3.down);
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(ray, out raycastHit);
		Vector3 result;
		if (flag)
		{
			bool flag2 = raycastHit.collider.tag == "xuedi" || raycastHit.collider.tag == "can_damage_dun";
			if (flag2)
			{
				result = Vector3.up;
			}
			else
			{
				result = raycastHit.normal;
			}
		}
		else
		{
			result = Vector3.zero;
		}
		return result;
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x0017E60C File Offset: 0x0017C80C
	private float handleSlopeSliding()
	{
		Debug.DrawRay(this.hit.point, this.normal, Color.red);
		bool flag = this.normal.y < 0.7f;
		float result;
		if (flag)
		{
			Vector3 lhs = Vector3.Cross(Vector3.up, this.normal);
			Vector3 a = Vector3.Cross(lhs, this.normal);
			this.slidingDirection = a * 0.4f;
			result = Vector3.Dot(this.slidingDirection, base.transform.forward);
		}
		else
		{
			result = 1f;
		}
		return result;
	}

	// Token: 0x0400251F RID: 9503
	public bool canWall = true;

	// Token: 0x04002520 RID: 9504
	public Vector3 forceLevel;

	// Token: 0x04002521 RID: 9505
	public float forceLevelTime;

	// Token: 0x04002522 RID: 9506
	private float velocityTime;

	// Token: 0x04002523 RID: 9507
	private Vector3 speedMoveVelocity = Vector3.zero;

	// Token: 0x04002524 RID: 9508
	private Vector3 oldVelocity;

	// Token: 0x04002525 RID: 9509
	private bool _breakForce;

	// Token: 0x04002526 RID: 9510
	private float forceTime;

	// Token: 0x04002527 RID: 9511
	private Vector3 speedMoveForce = Vector3.zero;

	// Token: 0x04002528 RID: 9512
	private bool disableBySpeedMoveVelocity;

	// Token: 0x04002529 RID: 9513
	private float speedForceYVectorMax;

	// Token: 0x0400252A RID: 9514
	private float speedFrontVectorMax;

	// Token: 0x0400252B RID: 9515
	private Vector3 lastAirForce = Vector3.zero;

	// Token: 0x0400252C RID: 9516
	private float acceleraSlope;

	// Token: 0x0400252D RID: 9517
	private bool onSlope;

	// Token: 0x0400252E RID: 9518
	private float wallTime;

	// Token: 0x0400252F RID: 9519
	private float checkStatTimer;

	// Token: 0x04002530 RID: 9520
	private Vector3 PaqiangDir;

	// Token: 0x04002531 RID: 9521
	private Vector3 WallPoint;

	// Token: 0x04002532 RID: 9522
	private float AngleTimer;

	// Token: 0x04002533 RID: 9523
	private float nullColliderTimer;

	// Token: 0x04002534 RID: 9524
	private float lerpAngleClimb;

	// Token: 0x04002535 RID: 9525
	private float angClimb;

	// Token: 0x04002536 RID: 9526
	private Vector3 slidingDirection = Vector3.zero;
}

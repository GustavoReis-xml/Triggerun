using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class TBCharacterThird : TBCharacter
{
	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0017AFB8 File Offset: 0x001791B8
	// (set) Token: 0x06002B0B RID: 11019 RVA: 0x0017AFD0 File Offset: 0x001791D0
	public float animSpeedMove
	{
		get
		{
			return this._animSpeedMove;
		}
		set
		{
			this._animSpeedMove = value;
			this.isChangeSpd = 0f;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0017AFE8 File Offset: 0x001791E8
	// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0017B000 File Offset: 0x00179200
	public float MoveSpeedNoneAnimation
	{
		get
		{
			return this.m_MoveSpeedNoneAnimator;
		}
		set
		{
			this.m_MoveSpeedNoneAnimator = value;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0017B00A File Offset: 0x0017920A
	public bool IsDown
	{
		get
		{
			return this.isDown;
		}
	}

	// Token: 0x1700054E RID: 1358
	// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0017B012 File Offset: 0x00179212
	public bool moveByControlIt
	{
		set
		{
			this._moveByControlIt = value;
		}
	}

	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0017B01C File Offset: 0x0017921C
	public float GroundDistance
	{
		get
		{
			return this.groundDistance;
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0017B024 File Offset: 0x00179224
	// (set) Token: 0x06002B12 RID: 11026 RVA: 0x0017B03C File Offset: 0x0017923C
	public bool ForceZeroPhysic
	{
		get
		{
			return this._ForceZeroPhysic;
		}
		set
		{
			this._ForceZeroPhysic = value;
		}
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0017B048 File Offset: 0x00179248
	// (set) Token: 0x06002B14 RID: 11028 RVA: 0x0017B060 File Offset: 0x00179260
	public bool hangInAir
	{
		get
		{
			return this._hangInAir;
		}
		set
		{
			this._hangInAir = value;
			bool flag = this.r != null;
			if (flag)
			{
				this.r.velocity = Vector3.zero;
				this.r.isKinematic = this._hangInAir;
			}
		}
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x06002B15 RID: 11029 RVA: 0x0017B0B0 File Offset: 0x001792B0
	private Vector3 TurnAround
	{
		get
		{
			Vector3 lookPos = this.userControl.state.lookPos;
			lookPos.y = this.r.position.y;
			Vector3 normalized = (lookPos - this.r.position).normalized;
			bool flag = !this.m_MoveByAnimator;
			Vector3 result;
			if (flag)
			{
				result = normalized;
			}
			else
			{
				float num = Vector3.Dot(base.transform.forward, normalized);
				float num2 = Vector3.Dot(base.transform.right, normalized);
				bool flag2 = num < 0f;
				if (flag2)
				{
					num2 = ((num2 >= 0f) ? (num2 - num) : (num2 + num));
				}
				bool flag3 = this.turnAnimatoring;
				if (flag3)
				{
					this.animState.MouseX = num2;
					bool flag4 = Mathf.Abs(this.animState.MouseX) < 0.01f;
					if (flag4)
					{
						this.animState.MouseX = 0f;
						this.turnAnimatoring = false;
					}
				}
				bool flag5 = Mathf.Abs(num2) < this.idleTurnTriggerValue;
				if (flag5)
				{
					result = base.transform.forward;
				}
				else
				{
					this.turnAnimatoring = true;
					result = this.userControl.state.lookPos - this.r.position;
				}
			}
			return result;
		}
	}

	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0017B1FF File Offset: 0x001793FF
	public Rigidbody rig
	{
		get
		{
			return this.r;
		}
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x0017B207 File Offset: 0x00179407
	protected override void Start()
	{
		base.Start();
		this.wallNormal = Vector3.up;
		this.animState.onGround = this.onGround;
		this.wallRunLayers = 67108865;
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x0017B240 File Offset: 0x00179440
	public virtual void InitCharThird(NetLiving player, TBCharAnimationThird charAni, TBUserControl control, TBCharAnimationPlay charAniPlay)
	{
		this.hero = player;
		this.hero.ChangeSpeed();
		this.characterAnimation = charAni;
		this.userControl = control;
		this.characterAnimation.characterController = this;
		bool flag = charAniPlay != null;
		if (flag)
		{
			this.characterAnimationPlay = charAniPlay;
			this.characterAnimationPlay.characterController = this;
		}
		this.animator = base.GetComponent<Animator>();
		this.characterAnimation.initAnimation();
		bool flag2 = this.animator == null;
		if (flag2)
		{
			this.animator = this.characterAnimation.GetComponent<Animator>();
		}
		base.initComm();
		this.isInited = true;
		this.fixedDeltaRotation = Quaternion.identity;
		this.animator.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x0017B308 File Offset: 0x00179508
	public void DeadInit()
	{
		bool flag = this.r != null;
		if (flag)
		{
			this.r.velocity = Vector3.zero;
		}
		this.onAir = false;
	}

	// Token: 0x06002B1A RID: 11034 RVA: 0x0017B340 File Offset: 0x00179540
	public override void Move(Vector3 deltaPosition, Quaternion deltaRotation)
	{
		float num = Vector3.Distance(Vector3.zero, deltaPosition);
		Vector3 zero = Vector3.zero;
		Vector3 vector = new Vector3(Mathf.Abs(this.animator.GetFloat("Right")), 0f, Mathf.Abs(this.animator.GetFloat("Forward")));
		float num2 = vector.x + vector.z;
		bool flag = num2 > 0f;
		if (flag)
		{
			float num3 = vector.x / num2;
			float num4 = vector.z / num2;
			float num5 = 1f - Mathf.Abs(num3 - num4);
			zero.x = 0.207f * num5 * num;
			bool flag2 = deltaPosition.x < 0f;
			if (flag2)
			{
				zero.x = 0f - zero.x;
			}
			float num6 = 1f - Mathf.Abs(num4 - num3);
			zero.z = 0.207f * num6 * num;
			bool flag3 = deltaPosition.z < 0f;
			if (flag3)
			{
				zero.z = 0f - zero.z;
			}
		}
		Vector3 b = new Vector3(deltaPosition.x + zero.x, deltaPosition.y, deltaPosition.z + zero.z);
		this.fixedDeltaPosition += b;
		this.fixedDeltaRotation *= deltaRotation;
	}

	// Token: 0x06002B1B RID: 11035 RVA: 0x0017B4CB File Offset: 0x001796CB
	protected virtual void FixedUpdate()
	{
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x0017B4D0 File Offset: 0x001796D0
	protected void OnNoneAnimatorMove()
	{
		bool flag = Time.deltaTime > 0f && this.onGround && !this.m_MoveByAnimator;
		if (flag)
		{
			Vector3 a = (!(this.moveAuto != Vector3.zero)) ? this.userControl.state.move : this.moveAuto;
			Vector3 velocity = a * this.m_MoveSpeedNoneAnimator;
			velocity.y = this.r.velocity.y;
			this.r.velocity = velocity;
		}
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x0017B560 File Offset: 0x00179760
	public void SpeedPowerForce(Vector3 power, float endtime = 2.5f)
	{
		this.jumpEndTime = Time.time + endtime * 0.04f;
		this.onGround = false;
		this.r.AddForce(power, ForceMode.Impulse);
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x0017B58B File Offset: 0x0017978B
	protected virtual void PositionMove()
	{
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x0017B590 File Offset: 0x00179790
	protected Vector3 NormalizedOneMax(Vector3 o)
	{
		float num = 0f;
		bool flag = Mathf.Abs(o.x) > Mathf.Abs(num);
		if (flag)
		{
			num = Mathf.Abs(o.x);
		}
		bool flag2 = Mathf.Abs(o.y) > Mathf.Abs(num);
		if (flag2)
		{
			num = Mathf.Abs(o.y);
		}
		bool flag3 = Mathf.Abs(o.z) > Mathf.Abs(num);
		if (flag3)
		{
			num = Mathf.Abs(o.z);
		}
		num = ((num <= 0f) ? 1f : (1f / num));
		return o * num;
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x0017B638 File Offset: 0x00179838
	protected virtual void Update()
	{
		bool flag = this.isInited;
		if (flag)
		{
			this.animState.fly = this.userControl.state.fly;
			this.animState.moveDirection = this.GetMoveDirection();
			this.animState.crouch = this.userControl.state.crouch;
			this.animState.isStrafing = (this.moveMode == TBCharacterThird.MoveMode.Strafe);
			this.animState.MouseY = Vector3.Dot((this.userControl.state.lookPos - this.r.position).normalized, Vector3.up);
		}
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x0017B6ED File Offset: 0x001798ED
	private void MoveFixed(Vector3 deltaPosition)
	{
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x0017B6F0 File Offset: 0x001798F0
	protected virtual void WallRun()
	{
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x0017B6F4 File Offset: 0x001798F4
	protected virtual bool CanWallRun()
	{
		return false;
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x0017B708 File Offset: 0x00179908
	protected virtual Vector3 GetMoveDirection()
	{
		TBCharacterThird.MoveMode moveMode = this.moveMode;
		TBCharacterThird.MoveMode moveMode2 = moveMode;
		Vector3 result;
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
			result = base.transform.InverseTransformDirection(this.moveDirection);
		}
		return result;
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x0017B840 File Offset: 0x00179A40
	protected virtual void Rotate()
	{
		bool flag = this.platformAngularVelocity != Vector3.zero;
		if (flag)
		{
			base.transform.rotation = Quaternion.Euler(this.platformAngularVelocity) * base.transform.rotation;
		}
		float angleFromForward = base.GetAngleFromForward(this.GetForwardDirection());
		Vector3 move = this.userControl.state.move;
		move.y = 0f;
		float angle = ((!(move == Vector3.zero) || !this.m_MoveByAnimator || !this.onGround || this.onAir) ? (angleFromForward * ((1.01f - Mathf.Abs(angleFromForward) / 180f) * this.turnSpeed)) : (angleFromForward * ((1.01f - Mathf.Abs(angleFromForward) / 180f) * this.stationaryTurnSpeedMlp))) * Time.deltaTime;
		base.RigidbodyRotateAround(this.characterAnimation.GetPivotPoint(), base.transform.up, angle);
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x0017B938 File Offset: 0x00179B38
	private Vector3 GetForwardDirection()
	{
		Vector3 move = this.userControl.state.move;
		move.y = 0f;
		bool flag = move != Vector3.zero;
		TBCharacterThird.MoveMode moveMode = this.moveMode;
		TBCharacterThird.MoveMode moveMode2 = moveMode;
		Vector3 result;
		if (moveMode2 != TBCharacterThird.MoveMode.Strafe)
		{
			if (moveMode2 != TBCharacterThird.MoveMode.Directional)
			{
				result = Vector3.zero;
			}
			else
			{
				bool flag2 = flag || !this.onGround;
				if (flag2)
				{
					this.animState.MouseX = 0f;
					result = this.userControl.state.move;
				}
				else
				{
					result = ((!this.lookInCameraDirection) ? base.transform.forward : (this.userControl.state.lookPos - this.r.position));
				}
			}
		}
		else
		{
			bool flag3 = flag || !this.onGround || this.onAir;
			if (flag3)
			{
				this.animState.MouseX = 0f;
				bool flag4 = this.wallState == TBCharacter.WallState.OnWall;
				if (flag4)
				{
					result = Vector3.up;
				}
				else
				{
					result = this.userControl.state.lookPos - this.r.position;
				}
			}
			else
			{
				result = ((!this.lookInCameraDirection) ? this.TurnAround : (this.userControl.state.lookPos - this.r.position));
			}
		}
		return result;
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x0017BAAC File Offset: 0x00179CAC
	protected virtual bool Jump()
	{
		return false;
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x0017BABF File Offset: 0x00179CBF
	private void GroundCheck()
	{
	}

	// Token: 0x040024C3 RID: 9411
	[Header("References")]
	public TBCharAnimationThird characterAnimation;

	// Token: 0x040024C4 RID: 9412
	public TBUserControl userControl;

	// Token: 0x040024C5 RID: 9413
	public TBCharAnimationPlay characterAnimationPlay;

	// Token: 0x040024C6 RID: 9414
	[Header("Movement")]
	public bool isHill;

	// Token: 0x040024C7 RID: 9415
	public float upHillAngle = 50f;

	// Token: 0x040024C8 RID: 9416
	private float _animSpeedMove = 1f;

	// Token: 0x040024C9 RID: 9417
	public bool m_MoveByAnimator = true;

	// Token: 0x040024CA RID: 9418
	private float m_MoveSpeedNoneAnimator = 1f;

	// Token: 0x040024CB RID: 9419
	protected bool _moveByControlIt;

	// Token: 0x040024CC RID: 9420
	public bool animatorAffectBySpeed;

	// Token: 0x040024CD RID: 9421
	public Vector3 moveAuto = Vector3.zero;

	// Token: 0x040024CE RID: 9422
	public TBCharacterThird.MoveMode moveMode;

	// Token: 0x040024CF RID: 9423
	public bool smoothPhysics = true;

	// Token: 0x040024D0 RID: 9424
	public float smoothAccelerationTime = 0.1f;

	// Token: 0x040024D1 RID: 9425
	public float linearAccelerationSpeed = 4f;

	// Token: 0x040024D2 RID: 9426
	public float platformFriction = 7f;

	// Token: 0x040024D3 RID: 9427
	public float groundStickyEffect = 4f;

	// Token: 0x040024D4 RID: 9428
	public float maxVerticalVelocityOnGround = 3f;

	// Token: 0x040024D5 RID: 9429
	public float velocityToGroundTangentWeight;

	// Token: 0x040024D6 RID: 9430
	public Vector3 moveDirectionNoForce = Vector3.zero;

	// Token: 0x040024D7 RID: 9431
	protected Vector3 moveDirectionVector = Vector3.zero;

	// Token: 0x040024D8 RID: 9432
	protected bool moveByDirectionVector;

	// Token: 0x040024D9 RID: 9433
	public Vector3 moveDirectionSandDash = Vector3.zero;

	// Token: 0x040024DA RID: 9434
	public bool notVelocity;

	// Token: 0x040024DB RID: 9435
	[Header("Rotation")]
	public bool lookInCameraDirection;

	// Token: 0x040024DC RID: 9436
	public float turnSpeed = 25f;

	// Token: 0x040024DD RID: 9437
	public float stationaryTurnSpeedMlp = 1f;

	// Token: 0x040024DE RID: 9438
	[Range(0.5f, 1f)]
	public float idleTurnTriggerValue = 0.8f;

	// Token: 0x040024DF RID: 9439
	[Header("Jumping and Falling")]
	public bool debugJump;

	// Token: 0x040024E0 RID: 9440
	public float isChangeSpd;

	// Token: 0x040024E1 RID: 9441
	public float airSpeedMax = 5f;

	// Token: 0x040024E2 RID: 9442
	public float airControl = 5f;

	// Token: 0x040024E3 RID: 9443
	public float airHillControl = 0.2f;

	// Token: 0x040024E4 RID: 9444
	public float airDrag = 1f;

	// Token: 0x040024E5 RID: 9445
	public float jumpPower = 7.5f;

	// Token: 0x040024E6 RID: 9446
	public float jumpPowerForward = 5f;

	// Token: 0x040024E7 RID: 9447
	public float jumpRepeatDelayTime;

	// Token: 0x040024E8 RID: 9448
	[Header("Wall Running")]
	[SerializeField]
	public bool wallRunCan;

	// Token: 0x040024E9 RID: 9449
	protected LayerMask wallRunLayers = 1;

	// Token: 0x040024EA RID: 9450
	public float wallRunMaxLength = 2f;

	// Token: 0x040024EB RID: 9451
	public float wallRunMinMoveMag = 0.6f;

	// Token: 0x040024EC RID: 9452
	public float wallRunMinVelocityY = -1f;

	// Token: 0x040024ED RID: 9453
	public float wallRunRotationSpeed = 2f;

	// Token: 0x040024EE RID: 9454
	public float wallRunMaxRotationAngle = 70f;

	// Token: 0x040024EF RID: 9455
	public float wallRunWeightSpeed = 5f;

	// Token: 0x040024F0 RID: 9456
	public float wallRunJumpAmount = 5f;

	// Token: 0x040024F1 RID: 9457
	[Header("Crouching")]
	public float crouchCapsuleScaleMlp = 0.6f;

	// Token: 0x040024F2 RID: 9458
	public bool onGround = true;

	// Token: 0x040024F3 RID: 9459
	public bool onAir;

	// Token: 0x040024F4 RID: 9460
	public int ActiveSwith;

	// Token: 0x040024F5 RID: 9461
	public TBCharacterThird.AnimState animState = default(TBCharacterThird.AnimState);

	// Token: 0x040024F6 RID: 9462
	protected Vector3 moveDirection;

	// Token: 0x040024F7 RID: 9463
	protected Animator animator;

	// Token: 0x040024F8 RID: 9464
	protected Vector3 fixedDeltaPosition;

	// Token: 0x040024F9 RID: 9465
	protected Quaternion fixedDeltaRotation;

	// Token: 0x040024FA RID: 9466
	protected Vector3 wallNormal = Vector3.up;

	// Token: 0x040024FB RID: 9467
	public Vector3 wallNormalStart = Vector3.up;

	// Token: 0x040024FC RID: 9468
	protected float wallRunWeight;

	// Token: 0x040024FD RID: 9469
	protected float lastWallRunWeight;

	// Token: 0x040024FE RID: 9470
	protected float wallRunEndTime;

	// Token: 0x040024FF RID: 9471
	protected Vector3 normal;

	// Token: 0x04002500 RID: 9472
	protected Vector3 platformVelocity;

	// Token: 0x04002501 RID: 9473
	protected Vector3 platformAngularVelocity;

	// Token: 0x04002502 RID: 9474
	protected RaycastHit hit;

	// Token: 0x04002503 RID: 9475
	protected float jumpLeg;

	// Token: 0x04002504 RID: 9476
	protected float jumpEndTime;

	// Token: 0x04002505 RID: 9477
	protected float forwardMlp;

	// Token: 0x04002506 RID: 9478
	protected float groundDistance;

	// Token: 0x04002507 RID: 9479
	protected float lastAirTime;

	// Token: 0x04002508 RID: 9480
	protected float stickyForce;

	// Token: 0x04002509 RID: 9481
	protected Vector3 moveDirectionVelocity;

	// Token: 0x0400250A RID: 9482
	protected NetLiving hero;

	// Token: 0x0400250B RID: 9483
	protected bool isInited;

	// Token: 0x0400250C RID: 9484
	protected bool isDown;

	// Token: 0x0400250D RID: 9485
	public bool PositionMoveWithGravity;

	// Token: 0x0400250E RID: 9486
	private bool _hangInAir;

	// Token: 0x0400250F RID: 9487
	private bool _ForceZeroPhysic;

	// Token: 0x04002510 RID: 9488
	public bool groundCheckDelaySwitch;

	// Token: 0x04002511 RID: 9489
	public float groundCheckDelayTime = 1f;

	// Token: 0x04002512 RID: 9490
	protected float groundCheckDelayLastTime;

	// Token: 0x04002513 RID: 9491
	public float limitNowDis = 2.5f;

	// Token: 0x04002514 RID: 9492
	private bool turnAnimatoring;

	// Token: 0x04002515 RID: 9493
	public bool nextJump = true;

	// Token: 0x0200067D RID: 1661
	[Serializable]
	public enum MoveMode
	{
		// Token: 0x040030B6 RID: 12470
		Strafe,
		// Token: 0x040030B7 RID: 12471
		Directional
	}

	// Token: 0x0200067E RID: 1662
	public struct AnimState
	{
		// Token: 0x040030B8 RID: 12472
		public Vector3 moveDirection;

		// Token: 0x040030B9 RID: 12473
		public bool jump;

		// Token: 0x040030BA RID: 12474
		public bool crouch;

		// Token: 0x040030BB RID: 12475
		public bool onGround;

		// Token: 0x040030BC RID: 12476
		public bool isStrafing;

		// Token: 0x040030BD RID: 12477
		public bool fly;

		// Token: 0x040030BE RID: 12478
		public float yVelocity;

		// Token: 0x040030BF RID: 12479
		public float MouseX;

		// Token: 0x040030C0 RID: 12480
		public float MouseY;

		// Token: 0x040030C1 RID: 12481
		public bool fall;
	}
}

using System;
using UnityEngine;

// Token: 0x02000406 RID: 1030
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class TBCharacter : MonoBehaviour
{
	// Token: 0x06002AFF RID: 11007
	public abstract void Move(Vector3 deltaPosition, Quaternion deltaRotation);

	// Token: 0x06002B00 RID: 11008 RVA: 0x0017AB09 File Offset: 0x00178D09
	protected virtual void Start()
	{
		this.initComm();
	}

	// Token: 0x06002B01 RID: 11009 RVA: 0x0017AB14 File Offset: 0x00178D14
	protected void initComm()
	{
		bool flag = this.r == null;
		if (flag)
		{
			this.r = base.GetComponent<Rigidbody>();
			this.r.constraints = RigidbodyConstraints.FreezeRotation;
		}
		bool flag2 = this.capsule == null;
		if (flag2)
		{
			this.capsule = (base.GetComponent<Collider>() as CapsuleCollider);
			this.originalHeight = this.capsule.height;
			this.originalCenter = this.capsule.center;
		}
		bool flag3 = this.zeroFrictionMaterial == null;
		if (flag3)
		{
			this.zeroFrictionMaterial = new PhysicMaterial();
			this.zeroFrictionMaterial.dynamicFriction = 0f;
			this.zeroFrictionMaterial.staticFriction = 0f;
			this.zeroFrictionMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
			this.zeroFrictionMaterial.bounciness = 0f;
			this.zeroFrictionMaterial.bounceCombine = PhysicMaterialCombine.Minimum;
		}
		bool flag4 = this.highFrictionMaterial == null;
		if (flag4)
		{
			this.highFrictionMaterial = new PhysicMaterial();
		}
		this.groundLayers = 71303171;
	}

	// Token: 0x06002B02 RID: 11010 RVA: 0x0017AC2C File Offset: 0x00178E2C
	protected virtual RaycastHit GetSpherecastHit(bool isSphere, out float angle)
	{
		Vector3 vector = (this.wallState != TBCharacter.WallState.Fall) ? base.transform.up : Vector3.up;
		this.ray.origin = this.r.position + vector * 0.5f;
		this.ray.direction = -vector;
		if (isSphere)
		{
			Physics.SphereCast(this.ray, this.spherecastRadius, out this.h, this.airborneCheck, this.groundLayers);
		}
		else
		{
			Physics.Raycast(this.ray, out this.h, this.airborneCheck, this.groundLayers);
		}
		angle = Vector3.Angle(this.h.normal, vector);
		return this.h;
	}

	// Token: 0x06002B03 RID: 11011 RVA: 0x0017AD04 File Offset: 0x00178F04
	public float GetAngleFromForward(Vector3 worldDirection)
	{
		Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
		return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x0017AD3C File Offset: 0x00178F3C
	protected void RigidbodyRotateAround(Vector3 point, Vector3 axis, float angle)
	{
		Quaternion lhs = Quaternion.AngleAxis(angle, axis);
		this.r.MoveRotation(lhs * base.transform.rotation);
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x0017AD70 File Offset: 0x00178F70
	protected void ScaleCapsule(float mlp)
	{
		bool flag = !this.staticCapsule;
		if (flag)
		{
			bool flag2 = this.capsule.height != this.originalHeight * mlp;
			if (flag2)
			{
				this.capsule.height = Mathf.MoveTowards(this.capsule.height, this.originalHeight * mlp, Time.deltaTime * 4f);
				this.capsule.center = Vector3.MoveTowards(this.capsule.center, this.originalCenter * mlp, Time.deltaTime * 2f);
			}
		}
		else
		{
			bool flag3 = this.capsule.height != this.staticCapsuleHeight * mlp;
			if (flag3)
			{
				this.capsule.height = Mathf.MoveTowards(this.capsule.height, this.staticCapsuleHeight * mlp, Time.deltaTime * 4f);
			}
			bool flag4 = this.capsule.radius != this.staticCapsuleRadius * mlp;
			if (flag4)
			{
				this.capsule.radius = Mathf.MoveTowards(this.capsule.radius, this.staticCapsuleRadius * mlp, Time.deltaTime * 4f);
			}
		}
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x0017AEAE File Offset: 0x001790AE
	protected void HighFriction()
	{
		this.capsule.material = this.highFrictionMaterial;
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x0017AEC3 File Offset: 0x001790C3
	protected void ZeroFriction()
	{
		this.capsule.material = this.zeroFrictionMaterial;
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x0017AED8 File Offset: 0x001790D8
	protected float GetSlopeDamper(Vector3 velocity, Vector3 groundNormal)
	{
		float num = 90f - Vector3.Angle(velocity, groundNormal);
		num -= this.slopeStartAngle;
		float num2 = this.slopeEndAngle - this.slopeStartAngle;
		return 1f - Mathf.Clamp(num / num2, 0f, 1f);
	}

	// Token: 0x040024AF RID: 9391
	[Header("Base Parameters")]
	[Range(-4f, 4f)]
	[SerializeField]
	public float gravityMultiplier = 2f;

	// Token: 0x040024B0 RID: 9392
	[SerializeField]
	protected float airborneThreshold = 0.5f;

	// Token: 0x040024B1 RID: 9393
	[SerializeField]
	protected float airborneCheck = 1f;

	// Token: 0x040024B2 RID: 9394
	[SerializeField]
	private float slopeStartAngle = 50f;

	// Token: 0x040024B3 RID: 9395
	[SerializeField]
	private float slopeEndAngle = 85f;

	// Token: 0x040024B4 RID: 9396
	[SerializeField]
	protected float spherecastRadius = 0.3f;

	// Token: 0x040024B5 RID: 9397
	[SerializeField]
	private LayerMask groundLayers = 1;

	// Token: 0x040024B6 RID: 9398
	public TBCharacter.WallState wallState;

	// Token: 0x040024B7 RID: 9399
	private PhysicMaterial zeroFrictionMaterial;

	// Token: 0x040024B8 RID: 9400
	private PhysicMaterial highFrictionMaterial;

	// Token: 0x040024B9 RID: 9401
	protected Rigidbody r;

	// Token: 0x040024BA RID: 9402
	protected const float half = 0.5f;

	// Token: 0x040024BB RID: 9403
	public bool staticCapsule;

	// Token: 0x040024BC RID: 9404
	public float staticCapsuleHeight = 0.1f;

	// Token: 0x040024BD RID: 9405
	public float staticCapsuleRadius = 0.1f;

	// Token: 0x040024BE RID: 9406
	protected float originalHeight;

	// Token: 0x040024BF RID: 9407
	protected Vector3 originalCenter;

	// Token: 0x040024C0 RID: 9408
	protected CapsuleCollider capsule;

	// Token: 0x040024C1 RID: 9409
	private RaycastHit h = default(RaycastHit);

	// Token: 0x040024C2 RID: 9410
	private Ray ray = default(Ray);

	// Token: 0x0200067C RID: 1660
	[Serializable]
	public enum WallState
	{
		// Token: 0x040030B1 RID: 12465
		OnGround,
		// Token: 0x040030B2 RID: 12466
		OnWall,
		// Token: 0x040030B3 RID: 12467
		Fall,
		// Token: 0x040030B4 RID: 12468
		PaQiangEnd
	}
}

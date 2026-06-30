using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class TBRecoil : OffsetModifier
{
	// Token: 0x17000559 RID: 1369
	// (get) Token: 0x06002C03 RID: 11267 RVA: 0x00181BB8 File Offset: 0x0017FDB8
	private IKEffector primaryHandEffector
	{
		get
		{
			bool flag = this.handedness == TBRecoil.Handedness.Right;
			IKEffector result;
			if (flag)
			{
				result = this.ik.solver.rightHandEffector;
			}
			else
			{
				result = this.ik.solver.leftHandEffector;
			}
			return result;
		}
	}

	// Token: 0x1700055A RID: 1370
	// (get) Token: 0x06002C04 RID: 11268 RVA: 0x00181BFC File Offset: 0x0017FDFC
	private IKEffector secondaryHandEffector
	{
		get
		{
			bool flag = this.handedness == TBRecoil.Handedness.Right;
			IKEffector result;
			if (flag)
			{
				result = this.ik.solver.leftHandEffector;
			}
			else
			{
				result = this.ik.solver.rightHandEffector;
			}
			return result;
		}
	}

	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x06002C05 RID: 11269 RVA: 0x00181C3F File Offset: 0x0017FE3F
	private Transform primaryHand
	{
		get
		{
			return this.primaryHandEffector.bone;
		}
	}

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x06002C06 RID: 11270 RVA: 0x00181C4C File Offset: 0x0017FE4C
	private Transform secondaryHand
	{
		get
		{
			return this.secondaryHandEffector.bone;
		}
	}

	// Token: 0x06002C07 RID: 11271 RVA: 0x00181C5C File Offset: 0x0017FE5C
	public void Fire(float magnitude)
	{
		float num = magnitude * UnityEngine.Random.value * this.magnitudeRandom;
		this.magnitudeMlp = magnitude + num;
		this.randomRotation = Quaternion.Euler(this.rotationRandom * UnityEngine.Random.value);
		TBRecoil.RecoilOffset[] array = this.offsets;
		foreach (TBRecoil.RecoilOffset recoilOffset in array)
		{
			recoilOffset.Start();
		}
		bool flag = Time.time < this.endTime;
		if (flag)
		{
			this.blendWeight = 0f;
		}
		else
		{
			this.blendWeight = 1f;
		}
		Keyframe[] keys = this.recoilWeight.keys;
		this.length = keys[keys.Length - 1].time;
		this.endTime = Time.time + this.length;
	}

	// Token: 0x06002C08 RID: 11272 RVA: 0x00181D2C File Offset: 0x0017FF2C
	protected override void OnModifyOffset()
	{
		bool flag = Time.time < this.endTime;
		if (flag)
		{
			bool flag2 = !this.initiated && this.ik != null;
			if (flag2)
			{
				this.initiated = true;
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
			}
			this.blendTime = Mathf.Max(this.blendTime, 0f);
			bool flag3 = this.blendTime > 0f;
			if (flag3)
			{
				this.blendWeight = Mathf.Min(this.blendWeight + Time.deltaTime * (1f / this.blendTime), 1f);
			}
			else
			{
				this.blendWeight = 1f;
			}
			float b = this.recoilWeight.Evaluate(this.length - (this.endTime - Time.time)) * this.magnitudeMlp;
			this.w = Mathf.Lerp(this.w, b, this.blendWeight);
			Quaternion quaternion = (!(this.aimIK != null)) ? this.ik.references.root.rotation : Quaternion.LookRotation(this.aimIK.solver.IKPosition - this.aimIK.solver.transform.position, this.ik.references.root.up);
			quaternion = this.randomRotation * quaternion;
			TBRecoil.RecoilOffset[] array = this.offsets;
			foreach (TBRecoil.RecoilOffset recoilOffset in array)
			{
				recoilOffset.Apply(this.ik.solver, quaternion, this.w, this.length, this.endTime - Time.time);
			}
			Quaternion lhs = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(this.randomRotation * this.primaryHand.rotation * this.handRotationOffset), this.w);
			this.handRotation = lhs * this.primaryHand.rotation;
			bool flag4 = this.twoHanded;
			if (flag4)
			{
				Vector3 point = this.primaryHand.InverseTransformPoint(this.secondaryHand.position);
				this.secondaryHandRelativeRotation = Quaternion.Inverse(this.primaryHand.rotation) * this.secondaryHand.rotation;
				Vector3 a = this.primaryHand.position + this.primaryHandEffector.positionOffset;
				Vector3 a2 = a + this.handRotation * point;
				this.secondaryHandEffector.positionOffset += a2 - (this.secondaryHand.position + this.secondaryHandEffector.positionOffset);
			}
		}
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x0018201C File Offset: 0x0018021C
	private void AfterFBBIK()
	{
		bool flag = Time.time < this.endTime;
		if (flag)
		{
			this.primaryHand.rotation = this.handRotation;
			bool flag2 = this.twoHanded;
			if (flag2)
			{
				this.secondaryHand.rotation = this.primaryHand.rotation * this.secondaryHandRelativeRotation;
			}
		}
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x00182080 File Offset: 0x00180280
	protected override void OnDestroy()
	{
		base.OnDestroy();
		bool flag = this.ik != null && this.initiated;
		if (flag)
		{
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
		}
	}

	// Token: 0x04002572 RID: 9586
	[Tooltip("Reference to the AimIK component. Optional, only used to getting the aiming direction.")]
	public AimIK aimIK;

	// Token: 0x04002573 RID: 9587
	[Tooltip("Which hand is holding the weapon?")]
	public TBRecoil.Handedness handedness;

	// Token: 0x04002574 RID: 9588
	[Tooltip("Check for 2-handed weapons.")]
	public bool twoHanded = true;

	// Token: 0x04002575 RID: 9589
	[Tooltip("Weight curve for the recoil offsets. Recoil procedure is as long as this curve.")]
	public AnimationCurve recoilWeight;

	// Token: 0x04002576 RID: 9590
	[Tooltip("How much is the magnitude randomized each time Recoil is called?")]
	public float magnitudeRandom = 0.1f;

	// Token: 0x04002577 RID: 9591
	[Tooltip("How much is the rotation randomized each time Recoil is called?")]
	public Vector3 rotationRandom;

	// Token: 0x04002578 RID: 9592
	[Tooltip("Rotating the primary hand bone for the recoil (in local space).")]
	public Vector3 handRotationOffset;

	// Token: 0x04002579 RID: 9593
	[Tooltip("Time of blending in another recoil when doing automatic fire.")]
	public float blendTime;

	// Token: 0x0400257A RID: 9594
	[Space(10f)]
	[Tooltip("FBBIK effector position offsets for the recoil (in aiming direction space).")]
	public TBRecoil.RecoilOffset[] offsets;

	// Token: 0x0400257B RID: 9595
	private float magnitudeMlp = 1f;

	// Token: 0x0400257C RID: 9596
	private float endTime = -1f;

	// Token: 0x0400257D RID: 9597
	private Quaternion handRotation;

	// Token: 0x0400257E RID: 9598
	private Quaternion secondaryHandRelativeRotation;

	// Token: 0x0400257F RID: 9599
	private Quaternion randomRotation;

	// Token: 0x04002580 RID: 9600
	private float length = 1f;

	// Token: 0x04002581 RID: 9601
	private bool initiated;

	// Token: 0x04002582 RID: 9602
	private float blendWeight;

	// Token: 0x04002583 RID: 9603
	private float w;

	// Token: 0x02000680 RID: 1664
	[Serializable]
	public class RecoilOffset
	{
		// Token: 0x06003942 RID: 14658 RVA: 0x001C8340 File Offset: 0x001C6540
		public void Start()
		{
			bool flag = this.additivity > 0f;
			if (flag)
			{
				this.additiveOffset = Vector3.ClampMagnitude(this.lastOffset * this.additivity, this.maxAdditiveOffsetMag);
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x001C8384 File Offset: 0x001C6584
		public void Apply(IKSolverFullBodyBiped solver, Quaternion rotation, float masterWeight, float length, float timeLeft)
		{
			this.additiveOffset = Vector3.Lerp(Vector3.zero, this.additiveOffset, timeLeft / length);
			this.lastOffset = rotation * (this.offset * masterWeight) + rotation * this.additiveOffset;
			TBRecoil.RecoilOffset.EffectorLink[] array = this.effectorLinks;
			foreach (TBRecoil.RecoilOffset.EffectorLink effectorLink in array)
			{
				solver.GetEffector(effectorLink.effector).positionOffset += this.lastOffset * effectorLink.weight;
			}
		}

		// Token: 0x040030C6 RID: 12486
		[Tooltip("Offset vector for the associated effector when doing recoil.")]
		public Vector3 offset;

		// Token: 0x040030C7 RID: 12487
		[Tooltip("When firing before the last recoil has faded, how much of the current recoil offset will be maintained?")]
		[Range(0f, 1f)]
		public float additivity = 1f;

		// Token: 0x040030C8 RID: 12488
		[Tooltip("Max additive recoil for automatic fire.")]
		public float maxAdditiveOffsetMag = 0.2f;

		// Token: 0x040030C9 RID: 12489
		[Tooltip("Linking this recoil offset to FBBIK effectors.")]
		public TBRecoil.RecoilOffset.EffectorLink[] effectorLinks;

		// Token: 0x040030CA RID: 12490
		private Vector3 additiveOffset;

		// Token: 0x040030CB RID: 12491
		private Vector3 lastOffset;

		// Token: 0x020006A9 RID: 1705
		[Serializable]
		public class EffectorLink
		{
			// Token: 0x04003190 RID: 12688
			[Tooltip("Type of the FBBIK effector to use")]
			public FullBodyBipedEffector effector;

			// Token: 0x04003191 RID: 12689
			[Tooltip("Weight of using this effector")]
			public float weight;
		}
	}

	// Token: 0x02000681 RID: 1665
	[Serializable]
	public enum Handedness
	{
		// Token: 0x040030CD RID: 12493
		Right,
		// Token: 0x040030CE RID: 12494
		Left
	}
}

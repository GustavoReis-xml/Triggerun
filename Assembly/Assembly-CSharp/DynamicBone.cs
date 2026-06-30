using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class DynamicBone : MonoBehaviour
{
	// Token: 0x060009A2 RID: 2466 RVA: 0x00055E7F File Offset: 0x0005407F
	private void Start()
	{
		this.MaxDisForWork = this.MaxDisForWorkConst;
		this.SetupParticles();
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00055E98 File Offset: 0x00054098
	private void Update()
	{
		bool flag = this.switchCheck;
		if (flag)
		{
			bool flag2 = QualitySettings.GetQualityLevel() == 0;
			if (flag2)
			{
				this.switchOff = true;
				this.m_Weight = 0f;
			}
			else
			{
				this.switchOff = false;
			}
		}
		bool flag3 = this.switchOff;
		if (!flag3)
		{
			bool flag4 = this.b_CheckDis;
			if (flag4)
			{
				bool flag5 = this.hero == null;
				if (flag5)
				{
					this.hero = fGameManager.GetInstance().getPlayer();
					this.m_Weight = 0f;
					return;
				}
				bool flag6 = this.netobject == null;
				if (flag6)
				{
					this.m_Weight = 0f;
					return;
				}
				bool flag7 = Time.time > this.checkDisTime;
				if (flag7)
				{
					this.checkDisTime = Time.time + 1f;
					bool flag8 = this.netobject.disCameraNow > this.MaxDisForWork || this.netobject.publicInfo.eyeBlock;
					if (flag8)
					{
						this.m_WeightTo = 0f;
					}
					else
					{
						this.m_WeightTo = 1f;
					}
				}
			}
			else
			{
				this.m_WeightTo = 1f;
			}
			this.m_Weight = Mathf.Lerp(this.m_Weight, this.m_WeightTo, Time.deltaTime * 10f);
			bool flag9 = this.m_Weight > 0.1f;
			if (flag9)
			{
				this.InitTransforms();
			}
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00056008 File Offset: 0x00054208
	private void LateUpdate()
	{
		bool flag = this.m_Weight > 0.1f;
		if (flag)
		{
			this.UpdateDynamicBones(Time.deltaTime);
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00056035 File Offset: 0x00054235
	private void OnEnable()
	{
		this.ResetParticlesPosition();
		this.m_ObjectPrevPosition = base.transform.position;
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00056050 File Offset: 0x00054250
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0005605C File Offset: 0x0005425C
	private void OnValidate()
	{
		this.m_UpdateRate = Mathf.Max(this.m_UpdateRate, 0f);
		this.m_Damping = Mathf.Clamp01(this.m_Damping);
		this.m_Elasticity = Mathf.Clamp01(this.m_Elasticity);
		this.m_Stiffness = Mathf.Clamp01(this.m_Stiffness);
		this.m_Inert = Mathf.Clamp01(this.m_Inert);
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		bool flag = Application.isEditor && Application.isPlaying;
		if (flag)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00056100 File Offset: 0x00054300
	private void OnDrawGizmosSelected()
	{
		bool flag = !base.enabled || this.m_Root == null;
		if (!flag)
		{
			bool flag2 = Application.isEditor && !Application.isPlaying && base.transform.hasChanged;
			if (flag2)
			{
				this.InitTransforms();
				this.SetupParticles();
			}
			Gizmos.color = Color.white;
			foreach (DynamicBone.Particle particle in this.m_Particles)
			{
				bool flag3 = particle.m_ParentIndex >= 0;
				if (flag3)
				{
					DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
					Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
				}
				bool flag4 = particle.m_Radius > 0f;
				if (flag4)
				{
					Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
				}
			}
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00056218 File Offset: 0x00054418
	public void SetWeight(float w)
	{
		bool flag = this.m_Weight != w;
		if (flag)
		{
			bool flag2 = w == 0f;
			if (flag2)
			{
				this.InitTransforms();
			}
			else
			{
				bool flag3 = this.m_Weight == 0f;
				if (flag3)
				{
					this.ResetParticlesPosition();
					this.m_ObjectPrevPosition = base.transform.position;
				}
			}
			this.m_Weight = w;
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00056284 File Offset: 0x00054484
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0005629C File Offset: 0x0005449C
	private void UpdateDynamicBones(float t)
	{
		bool flag = this.m_Root == null;
		if (!flag)
		{
			this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
			this.m_ObjectMove = base.transform.position - this.m_ObjectPrevPosition;
			this.m_ObjectPrevPosition = base.transform.position;
			int num = 1;
			bool flag2 = this.m_UpdateRate > 0f;
			if (flag2)
			{
				float num2 = 1f / this.m_UpdateRate;
				this.m_Time += t;
				num = 0;
				while (this.m_Time >= num2)
				{
					this.m_Time -= num2;
					bool flag3 = ++num >= 3;
					if (flag3)
					{
						this.m_Time = 0f;
						break;
					}
				}
			}
			bool flag4 = num > 0;
			if (flag4)
			{
				for (int i = 0; i < num; i++)
				{
					this.UpdateParticles1();
					this.UpdateParticles2();
					this.m_ObjectMove = Vector3.zero;
				}
			}
			else
			{
				this.SkipUpdateParticles();
			}
			this.ApplyParticlesToTransforms();
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000563CC File Offset: 0x000545CC
	private void SetupParticles()
	{
		this.m_Particles.Clear();
		bool flag = this.m_Root == null;
		if (!flag)
		{
			this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
			this.m_ObjectScale = base.transform.lossyScale.x;
			this.m_ObjectPrevPosition = base.transform.position;
			this.m_ObjectMove = Vector3.zero;
			this.m_BoneTotalLength = 0f;
			this.AppendParticles(this.m_Root, -1, 0f);
			foreach (DynamicBone.Particle particle in this.m_Particles)
			{
				particle.m_Damping = this.m_Damping;
				particle.m_Elasticity = this.m_Elasticity;
				particle.m_Stiffness = this.m_Stiffness;
				particle.m_Inert = this.m_Inert;
				particle.m_Radius = this.m_Radius;
				bool flag2 = this.m_BoneTotalLength > 0f;
				if (flag2)
				{
					float time = particle.m_BoneLength / this.m_BoneTotalLength;
					bool flag3 = this.m_DampingDistrib.keys.Length != 0;
					if (flag3)
					{
						particle.m_Damping *= this.m_DampingDistrib.Evaluate(time);
					}
					bool flag4 = this.m_ElasticityDistrib.keys.Length != 0;
					if (flag4)
					{
						particle.m_Elasticity *= this.m_ElasticityDistrib.Evaluate(time);
					}
					bool flag5 = this.m_StiffnessDistrib.keys.Length != 0;
					if (flag5)
					{
						particle.m_Stiffness *= this.m_StiffnessDistrib.Evaluate(time);
					}
					bool flag6 = this.m_InertDistrib.keys.Length != 0;
					if (flag6)
					{
						particle.m_Inert *= this.m_InertDistrib.Evaluate(time);
					}
					bool flag7 = this.m_RadiusDistrib.keys.Length != 0;
					if (flag7)
					{
						particle.m_Radius *= this.m_RadiusDistrib.Evaluate(time);
					}
				}
				particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
				particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
				particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
				particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
				particle.m_Radius = Mathf.Max(particle.m_Radius, 0f);
			}
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00056668 File Offset: 0x00054868
	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		DynamicBone.Particle particle = new DynamicBone.Particle();
		particle.m_Transform = b;
		particle.m_ParentIndex = parentIndex;
		bool flag = b != null;
		if (flag)
		{
			particle.m_Position = (particle.m_PrevPosition = b.position);
			particle.m_InitLocalPosition = b.localPosition;
			particle.m_InitLocalRotation = b.localRotation;
		}
		else
		{
			Transform transform = this.m_Particles[parentIndex].m_Transform;
			bool flag2 = this.m_EndLength > 0f;
			if (flag2)
			{
				Transform parent = transform.parent;
				bool flag3 = parent != null;
				if (flag3)
				{
					particle.m_EndOffset = transform.InverseTransformPoint(transform.position * 2f - parent.position) * this.m_EndLength;
				}
				else
				{
					particle.m_EndOffset = new Vector3(this.m_EndLength, 0f, 0f);
				}
			}
			else
			{
				particle.m_EndOffset = transform.InverseTransformPoint(base.transform.TransformDirection(this.m_EndOffset) + transform.position);
			}
			particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
		}
		bool flag4 = parentIndex >= 0;
		if (flag4)
		{
			boneLength += (this.m_Particles[parentIndex].m_Transform.position - particle.m_Position).magnitude;
			particle.m_BoneLength = boneLength;
			this.m_BoneTotalLength = Mathf.Max(this.m_BoneTotalLength, boneLength);
		}
		int count = this.m_Particles.Count;
		this.m_Particles.Add(particle);
		bool flag5 = !(b != null);
		if (!flag5)
		{
			for (int i = 0; i < b.childCount; i++)
			{
				bool flag6 = false;
				foreach (Transform x in this.m_Exclusions)
				{
					bool flag7 = x == b.GetChild(i);
					if (flag7)
					{
						flag6 = true;
						break;
					}
				}
				bool flag8 = !flag6;
				if (flag8)
				{
					this.AppendParticles(b.GetChild(i), count, boneLength);
				}
			}
			bool flag9 = b.childCount == 0 && (this.m_EndLength > 0f || this.m_EndOffset != Vector3.zero);
			if (flag9)
			{
				this.AppendParticles(null, count, boneLength);
			}
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0005690C File Offset: 0x00054B0C
	private void InitTransforms()
	{
		foreach (DynamicBone.Particle particle in this.m_Particles)
		{
			bool flag = particle.m_Transform != null;
			if (flag)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00056994 File Offset: 0x00054B94
	private void ResetParticlesPosition()
	{
		foreach (DynamicBone.Particle particle in this.m_Particles)
		{
			bool flag = particle.m_Transform != null;
			if (flag)
			{
				particle.m_Position = (particle.m_PrevPosition = particle.m_Transform.position);
			}
			else
			{
				Transform transform = this.m_Particles[particle.m_ParentIndex].m_Transform;
				particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
			}
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00056A48 File Offset: 0x00054C48
	private void UpdateParticles1()
	{
		Vector3 vector = this.m_Gravity;
		Vector3 normalized = this.m_Gravity.normalized;
		Vector3 lhs = this.m_Root.TransformDirection(this.m_LocalGravity);
		Vector3 b = normalized * Mathf.Max(Vector3.Dot(lhs, normalized), 0f);
		vector -= b;
		vector = (vector + this.m_Force) * this.m_ObjectScale;
		foreach (DynamicBone.Particle particle in this.m_Particles)
		{
			bool flag = particle.m_ParentIndex >= 0;
			if (flag)
			{
				Vector3 a = particle.m_Position - particle.m_PrevPosition;
				Vector3 b2 = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + b2;
				particle.m_Position += a * (1f - particle.m_Damping) + vector + b2;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00056BAC File Offset: 0x00054DAC
	private void UpdateParticles2()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			float num = (!(particle.m_Transform != null)) ? (particle.m_EndOffset.magnitude * this.m_ObjectScale) : (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			float num2 = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
			bool flag = num2 > 0f || particle.m_Elasticity > 0f;
			if (flag)
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.m_Position);
				Vector3 a = (!(particle.m_Transform != null)) ? localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
				Vector3 a2 = a - particle.m_Position;
				particle.m_Position += a2 * particle.m_Elasticity;
				bool flag2 = num2 > 0f;
				if (flag2)
				{
					a2 = a - particle.m_Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					bool flag3 = magnitude > num3;
					if (flag3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
			}
			float particleRadius = particle.m_Radius * this.m_ObjectScale;
			foreach (DynamicBoneCollider dynamicBoneCollider in this.m_Colliders)
			{
				bool flag4 = dynamicBoneCollider != null && dynamicBoneCollider.enabled;
				if (flag4)
				{
					dynamicBoneCollider.Collide(ref particle.m_Position, particleRadius);
				}
			}
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude2 = a3.magnitude;
			bool flag5 = magnitude2 > 0f;
			if (flag5)
			{
				particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
			}
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00056E34 File Offset: 0x00055034
	private void SkipUpdateParticles()
	{
		foreach (DynamicBone.Particle particle in this.m_Particles)
		{
			bool flag = particle.m_ParentIndex >= 0;
			if (flag)
			{
				Vector3 b = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition += b;
				particle.m_Position += b;
				DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				float num = (!(particle.m_Transform != null)) ? (particle.m_EndOffset.magnitude * this.m_ObjectScale) : (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
				float num2 = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
				bool flag2 = num2 > 0f;
				if (flag2)
				{
					Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
					localToWorldMatrix.SetColumn(3, particle2.m_Position);
					Vector3 a = (!(particle.m_Transform != null)) ? localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
					Vector3 a2 = a - particle.m_Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					bool flag3 = magnitude > num3;
					if (flag3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
				Vector3 a3 = particle2.m_Position - particle.m_Position;
				float magnitude2 = a3.magnitude;
				bool flag4 = magnitude2 > 0f;
				if (flag4)
				{
					particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
				}
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00057088 File Offset: 0x00055288
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			bool flag = particle2.m_Transform.childCount <= 1;
			if (flag)
			{
				Vector3 direction = (!(particle.m_Transform != null)) ? particle.m_EndOffset : particle.m_Transform.localPosition;
				Quaternion lhs = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), particle.m_Position - particle2.m_Position);
				particle2.m_Transform.rotation = lhs * particle2.m_Transform.rotation;
			}
			bool flag2 = particle.m_Transform;
			if (flag2)
			{
				particle.m_Transform.position = particle.m_Position;
			}
		}
	}

	// Token: 0x0400077D RID: 1917
	public Transform m_Root;

	// Token: 0x0400077E RID: 1918
	public float m_UpdateRate = 60f;

	// Token: 0x0400077F RID: 1919
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x04000780 RID: 1920
	public AnimationCurve m_DampingDistrib;

	// Token: 0x04000781 RID: 1921
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x04000782 RID: 1922
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x04000783 RID: 1923
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x04000784 RID: 1924
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x04000785 RID: 1925
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x04000786 RID: 1926
	public AnimationCurve m_InertDistrib;

	// Token: 0x04000787 RID: 1927
	public float m_Radius;

	// Token: 0x04000788 RID: 1928
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000789 RID: 1929
	public float m_EndLength;

	// Token: 0x0400078A RID: 1930
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x0400078B RID: 1931
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x0400078C RID: 1932
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x0400078D RID: 1933
	public List<DynamicBoneCollider> m_Colliders;

	// Token: 0x0400078E RID: 1934
	public List<Transform> m_Exclusions;

	// Token: 0x0400078F RID: 1935
	private Vector3 m_LocalGravity = Vector3.zero;

	// Token: 0x04000790 RID: 1936
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x04000791 RID: 1937
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x04000792 RID: 1938
	private float m_BoneTotalLength;

	// Token: 0x04000793 RID: 1939
	private float m_ObjectScale = 1f;

	// Token: 0x04000794 RID: 1940
	private float m_Time;

	// Token: 0x04000795 RID: 1941
	private float m_Weight = 1f;

	// Token: 0x04000796 RID: 1942
	public bool b_CheckDis = true;

	// Token: 0x04000797 RID: 1943
	private bool switchOff;

	// Token: 0x04000798 RID: 1944
	public bool switchCheck = true;

	// Token: 0x04000799 RID: 1945
	private List<DynamicBone.Particle> m_Particles = new List<DynamicBone.Particle>();

	// Token: 0x0400079A RID: 1946
	private HeroPlayer hero;

	// Token: 0x0400079B RID: 1947
	private float checkDisTime;

	// Token: 0x0400079C RID: 1948
	public float MaxDisForWorkConst = 10f;

	// Token: 0x0400079D RID: 1949
	private float MaxDisForWork;

	// Token: 0x0400079E RID: 1950
	public float m_WeightTo = 1f;

	// Token: 0x0400079F RID: 1951
	public NetLiving netobject;

	// Token: 0x0200054F RID: 1359
	private class Particle
	{
		// Token: 0x04002C8A RID: 11402
		public Transform m_Transform;

		// Token: 0x04002C8B RID: 11403
		public int m_ParentIndex = -1;

		// Token: 0x04002C8C RID: 11404
		public float m_Damping;

		// Token: 0x04002C8D RID: 11405
		public float m_Elasticity;

		// Token: 0x04002C8E RID: 11406
		public float m_Stiffness;

		// Token: 0x04002C8F RID: 11407
		public float m_Inert;

		// Token: 0x04002C90 RID: 11408
		public float m_Radius;

		// Token: 0x04002C91 RID: 11409
		public float m_BoneLength;

		// Token: 0x04002C92 RID: 11410
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04002C93 RID: 11411
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04002C94 RID: 11412
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04002C95 RID: 11413
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04002C96 RID: 11414
		public Quaternion m_InitLocalRotation = Quaternion.identity;
	}
}

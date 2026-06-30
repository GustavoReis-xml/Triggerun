using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
[AddComponentMenu("Dynamic Bone/Dynamic Bone Collider")]
public class DynamicBoneCollider : MonoBehaviour
{
	// Token: 0x060009B5 RID: 2485 RVA: 0x00057243 File Offset: 0x00055443
	private void OnValidate()
	{
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		this.m_Height = Mathf.Max(this.m_Height, 0f);
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00057274 File Offset: 0x00055474
	public void Collide(ref Vector3 particlePosition, float particleRadius)
	{
		float num = this.m_Radius * Mathf.Abs(base.transform.lossyScale.x);
		float num2 = this.m_Height * 0.5f - num;
		bool flag = num2 <= 0f;
		if (flag)
		{
			bool flag2 = this.m_Bound == DynamicBoneCollider.Bound.Outside;
			if (flag2)
			{
				DynamicBoneCollider.OutsideSphere(ref particlePosition, particleRadius, base.transform.TransformPoint(this.m_Center), num);
			}
			else
			{
				DynamicBoneCollider.InsideSphere(ref particlePosition, particleRadius, base.transform.TransformPoint(this.m_Center), num);
			}
		}
		else
		{
			Vector3 center = this.m_Center;
			Vector3 center2 = this.m_Center;
			switch (this.m_Direction)
			{
			case DynamicBoneCollider.Direction.X:
				center.x -= num2;
				center2.x += num2;
				break;
			case DynamicBoneCollider.Direction.Y:
				center.y -= num2;
				center2.y += num2;
				break;
			case DynamicBoneCollider.Direction.Z:
				center.z -= num2;
				center2.z += num2;
				break;
			}
			bool flag3 = this.m_Bound == DynamicBoneCollider.Bound.Outside;
			if (flag3)
			{
				DynamicBoneCollider.OutsideCapsule(ref particlePosition, particleRadius, base.transform.TransformPoint(center), base.transform.TransformPoint(center2), num);
			}
			else
			{
				DynamicBoneCollider.InsideCapsule(ref particlePosition, particleRadius, base.transform.TransformPoint(center), base.transform.TransformPoint(center2), num);
			}
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x000573E4 File Offset: 0x000555E4
	private static void OutsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
	{
		float num = sphereRadius + particleRadius;
		float num2 = num * num;
		Vector3 a = particlePosition - sphereCenter;
		float sqrMagnitude = a.sqrMagnitude;
		bool flag = sqrMagnitude > 0f && sqrMagnitude < num2;
		if (flag)
		{
			float num3 = Mathf.Sqrt(sqrMagnitude);
			particlePosition = sphereCenter + a * (num / num3);
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00057444 File Offset: 0x00055644
	private static void InsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
	{
		float num = sphereRadius + particleRadius;
		float num2 = num * num;
		Vector3 a = particlePosition - sphereCenter;
		float sqrMagnitude = a.sqrMagnitude;
		bool flag = sqrMagnitude > num2;
		if (flag)
		{
			float num3 = Mathf.Sqrt(sqrMagnitude);
			particlePosition = sphereCenter + a * (num / num3);
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0005749C File Offset: 0x0005569C
	private static void OutsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
	{
		float num = capsuleRadius + particleRadius;
		float num2 = num * num;
		Vector3 vector = capsuleP1 - capsuleP0;
		Vector3 vector2 = particlePosition - capsuleP0;
		float num3 = Vector3.Dot(vector2, vector);
		bool flag = num3 <= 0f;
		if (flag)
		{
			float sqrMagnitude = vector2.sqrMagnitude;
			bool flag2 = sqrMagnitude > 0f && sqrMagnitude < num2;
			if (flag2)
			{
				float num4 = Mathf.Sqrt(sqrMagnitude);
				particlePosition = capsuleP0 + vector2 * (num / num4);
			}
		}
		else
		{
			float sqrMagnitude2 = vector.sqrMagnitude;
			bool flag3 = num3 >= sqrMagnitude2;
			if (flag3)
			{
				vector2 = particlePosition - capsuleP1;
				float sqrMagnitude3 = vector2.sqrMagnitude;
				bool flag4 = sqrMagnitude3 > 0f && sqrMagnitude3 < num2;
				if (flag4)
				{
					float num5 = Mathf.Sqrt(sqrMagnitude3);
					particlePosition = capsuleP1 + vector2 * (num / num5);
				}
			}
			else
			{
				bool flag5 = sqrMagnitude2 > 0f;
				if (flag5)
				{
					num3 /= sqrMagnitude2;
					vector2 -= vector * num3;
					float sqrMagnitude4 = vector2.sqrMagnitude;
					bool flag6 = sqrMagnitude4 > 0f && sqrMagnitude4 < num2;
					if (flag6)
					{
						float num6 = Mathf.Sqrt(sqrMagnitude4);
						particlePosition += vector2 * ((num - num6) / num6);
					}
				}
			}
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00057604 File Offset: 0x00055804
	private static void InsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
	{
		float num = capsuleRadius + particleRadius;
		float num2 = num * num;
		Vector3 vector = capsuleP1 - capsuleP0;
		Vector3 vector2 = particlePosition - capsuleP0;
		float num3 = Vector3.Dot(vector2, vector);
		bool flag = num3 <= 0f;
		if (flag)
		{
			float sqrMagnitude = vector2.sqrMagnitude;
			bool flag2 = sqrMagnitude > num2;
			if (flag2)
			{
				float num4 = Mathf.Sqrt(sqrMagnitude);
				particlePosition = capsuleP0 + vector2 * (num / num4);
			}
		}
		else
		{
			float sqrMagnitude2 = vector.sqrMagnitude;
			bool flag3 = num3 >= sqrMagnitude2;
			if (flag3)
			{
				vector2 = particlePosition - capsuleP1;
				float sqrMagnitude3 = vector2.sqrMagnitude;
				bool flag4 = sqrMagnitude3 > num2;
				if (flag4)
				{
					float num5 = Mathf.Sqrt(sqrMagnitude3);
					particlePosition = capsuleP1 + vector2 * (num / num5);
				}
			}
			else
			{
				bool flag5 = sqrMagnitude2 > 0f;
				if (flag5)
				{
					num3 /= sqrMagnitude2;
					vector2 -= vector * num3;
					float sqrMagnitude4 = vector2.sqrMagnitude;
					bool flag6 = sqrMagnitude4 > num2;
					if (flag6)
					{
						float num6 = Mathf.Sqrt(sqrMagnitude4);
						particlePosition += vector2 * ((num - num6) / num6);
					}
				}
			}
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00057748 File Offset: 0x00055948
	private void OnDrawGizmosSelected()
	{
		bool flag = !base.enabled;
		if (!flag)
		{
			bool flag2 = this.m_Bound == DynamicBoneCollider.Bound.Outside;
			if (flag2)
			{
				Gizmos.color = Color.yellow;
			}
			else
			{
				Gizmos.color = Color.magenta;
			}
			float num = this.m_Radius * Mathf.Abs(base.transform.lossyScale.x);
			float num2 = this.m_Height * 0.5f - num;
			bool flag3 = num2 <= 0f;
			if (flag3)
			{
				Gizmos.DrawWireSphere(base.transform.TransformPoint(this.m_Center), num);
			}
			else
			{
				Vector3 center = this.m_Center;
				Vector3 center2 = this.m_Center;
				switch (this.m_Direction)
				{
				case DynamicBoneCollider.Direction.X:
					center.x -= num2;
					center2.x += num2;
					break;
				case DynamicBoneCollider.Direction.Y:
					center.y -= num2;
					center2.y += num2;
					break;
				case DynamicBoneCollider.Direction.Z:
					center.z -= num2;
					center2.z += num2;
					break;
				}
				Gizmos.DrawWireSphere(base.transform.TransformPoint(center), num);
				Gizmos.DrawWireSphere(base.transform.TransformPoint(center2), num);
			}
		}
	}

	// Token: 0x040007A0 RID: 1952
	public Vector3 m_Center = Vector3.zero;

	// Token: 0x040007A1 RID: 1953
	public float m_Radius = 0.5f;

	// Token: 0x040007A2 RID: 1954
	public float m_Height;

	// Token: 0x040007A3 RID: 1955
	public DynamicBoneCollider.Direction m_Direction;

	// Token: 0x040007A4 RID: 1956
	public DynamicBoneCollider.Bound m_Bound;

	// Token: 0x02000550 RID: 1360
	public enum Direction
	{
		// Token: 0x04002C98 RID: 11416
		X,
		// Token: 0x04002C99 RID: 11417
		Y,
		// Token: 0x04002C9A RID: 11418
		Z
	}

	// Token: 0x02000551 RID: 1361
	public enum Bound
	{
		// Token: 0x04002C9C RID: 11420
		Outside,
		// Token: 0x04002C9D RID: 11421
		Inside
	}
}

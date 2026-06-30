using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class TBCharAnimationThirdIK : TBCharAnimationThird
{
	// Token: 0x06002BD1 RID: 11217 RVA: 0x001811C2 File Offset: 0x0017F3C2
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x001811CC File Offset: 0x0017F3CC
	protected override void LateUpdate()
	{
		bool flag = Time.time >= this.lateUpdateRate;
		if (flag)
		{
			this.lateUpdateRate = Time.time + 0.016f;
			base.LateUpdate();
			bool flag2 = !(this.ik == null) && Vector3.Angle(base.transform.up, Vector3.up) > 0.01f;
			if (flag2)
			{
				Quaternion rotation = Quaternion.FromToRotation(base.transform.up, Vector3.up);
				this.RotateEffector(this.ik.solver.bodyEffector, rotation, 0.1f);
				this.RotateEffector(this.ik.solver.leftShoulderEffector, rotation, 0.2f);
				this.RotateEffector(this.ik.solver.rightShoulderEffector, rotation, 0.2f);
				this.RotateEffector(this.ik.solver.leftHandEffector, rotation, 0.1f);
				this.RotateEffector(this.ik.solver.rightHandEffector, rotation, 0.1f);
			}
		}
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x001812E8 File Offset: 0x0017F4E8
	private void RotateEffector(IKEffector effector, Quaternion rotation, float mlp)
	{
		Vector3 vector = effector.bone.position - base.transform.position;
		Vector3 a = rotation * vector;
		Vector3 a2 = a - vector;
		effector.positionOffset += a2 * mlp;
	}

	// Token: 0x06002BD4 RID: 11220 RVA: 0x0018133A File Offset: 0x0017F53A
	public virtual void SetAnimParm()
	{
		this.animator.SetBool("Aim", true);
	}
}

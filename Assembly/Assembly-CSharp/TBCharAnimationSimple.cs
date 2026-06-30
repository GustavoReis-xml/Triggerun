using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
[RequireComponent(typeof(Animator))]
public class TBCharAnimationSimple : TBCharAnimation
{
	// Token: 0x06002BBD RID: 11197 RVA: 0x00180942 File Offset: 0x0017EB42
	protected override void Start()
	{
		base.Start();
		this.animator = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x00180958 File Offset: 0x0017EB58
	public override Vector3 GetPivotPoint()
	{
		bool flag = this.pivotOffset == 0f;
		Vector3 result;
		if (flag)
		{
			result = base.transform.position;
		}
		else
		{
			result = base.transform.position + base.transform.forward * this.pivotOffset;
		}
		return result;
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x001809B0 File Offset: 0x0017EBB0
	protected override void Update()
	{
		base.Update();
		float num = this.moveSpeed.Evaluate(this.characterController.animState.moveDirection.z);
		this.animator.SetFloat("Speed", num);
		this.characterController.Move(this.characterController.transform.forward * Time.deltaTime * num, Quaternion.identity);
	}

	// Token: 0x0400255D RID: 9565
	[SerializeField]
	private TBCharacterThird characterController;

	// Token: 0x0400255E RID: 9566
	[SerializeField]
	private float pivotOffset;

	// Token: 0x0400255F RID: 9567
	[SerializeField]
	private AnimationCurve moveSpeed;
}

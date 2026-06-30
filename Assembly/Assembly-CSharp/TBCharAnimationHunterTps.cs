using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class TBCharAnimationHunterTps : TBCharAnimationThirdIK
{
	// Token: 0x06002B86 RID: 11142 RVA: 0x001800BF File Offset: 0x0017E2BF
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x001800CC File Offset: 0x0017E2CC
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			int integer = this.animator.GetInteger("AttackState");
			int num = integer;
			if (num != 1)
			{
				if (num == 2)
				{
					this.animator.SetInteger("AttackState", 0);
				}
			}
			else
			{
				this.animator.SetInteger("AttackState", 0);
			}
		}
	}

	// Token: 0x0400255A RID: 9562
	public float magnitude = 1f;
}

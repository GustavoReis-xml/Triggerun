using System;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class TBCharAnimationReinhardtTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BB7 RID: 11191 RVA: 0x00180868 File Offset: 0x0017EA68
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool @bool = this.animator.GetBool("Shield_State");
			if (@bool)
			{
				this.animator.SetFloat("WalkState", 1f);
			}
			else
			{
				bool crouch = this.characterController.animState.crouch;
				if (crouch)
				{
					this.animator.SetFloat("WalkState", -1f);
				}
				else
				{
					this.animator.SetFloat("WalkState", 0f);
				}
			}
		}
	}
}

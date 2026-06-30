using System;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class TBCharAnimationHeiguafuFps : TBCharAnimationPlay
{
	// Token: 0x06002B70 RID: 11120 RVA: 0x0017ED60 File Offset: 0x0017CF60
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool flag2 = !this.animator.GetBool("Sniper");
			if (flag2)
			{
				this.animator.SetFloat("WalkState", (!this.characterController.animState.crouch) ? 0f : -1f);
			}
			else
			{
				this.animator.SetFloat("WalkState", (!this.characterController.animState.crouch) ? 1f : -1f);
			}
		}
	}
}

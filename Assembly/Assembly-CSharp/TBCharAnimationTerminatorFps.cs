using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class TBCharAnimationTerminatorFps : TBCharAnimationPlay
{
	// Token: 0x06002BC1 RID: 11201 RVA: 0x00180A34 File Offset: 0x0017EC34
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool @bool = this.animator.GetBool("Sprint");
			if (@bool)
			{
				float num = this.animator.GetFloat("WalkState");
				num = Mathf.Lerp(this.animator.GetFloat("WalkState"), 1f, Time.deltaTime * 10f);
				this.animator.SetFloat("WalkState", num);
			}
			else
			{
				float num = (!this.characterController.animState.crouch) ? 0f : -1f;
				num = Mathf.Lerp(this.animator.GetFloat("WalkState"), num, Time.deltaTime * 10f);
				this.animator.SetFloat("WalkState", num);
			}
		}
	}
}

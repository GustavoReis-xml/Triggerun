using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class TBCharAnimationTerminatorTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BC3 RID: 11203 RVA: 0x00180B38 File Offset: 0x0017ED38
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

using System;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class TBCharAnimationXingJiLieRenFps : TBCharAnimationPlay
{
	// Token: 0x06002BE8 RID: 11240 RVA: 0x00181484 File Offset: 0x0017F684
	protected override void Update()
	{
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			base.Update();
			bool flag2 = this.characterController.wallState > TBCharacter.WallState.OnGround;
			if (flag2)
			{
				bool flag3 = this.characterController.wallState == TBCharacter.WallState.PaQiangEnd;
				if (flag3)
				{
					this.animator.SetBool("PaQiangEnd", true);
				}
				else
				{
					bool flag4 = this.characterController.wallState == TBCharacter.WallState.OnWall;
					if (flag4)
					{
						this.animator.SetBool("PaQiang", true);
					}
					else
					{
						this.animator.SetBool("PaQiang", false);
						this.animator.SetBool("PaQiangEnd", false);
					}
				}
			}
		}
	}
}

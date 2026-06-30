using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class TBCharAnimationXingJiLieRenTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BEA RID: 11242 RVA: 0x0018154D File Offset: 0x0017F74D
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002BEB RID: 11243 RVA: 0x00181558 File Offset: 0x0017F758
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool flag2 = this.characterController.wallState == TBCharacter.WallState.PaQiangEnd;
			if (flag2)
			{
				this.animator.SetBool("PaQiang", false);
			}
			else
			{
				bool flag3 = this.characterController.wallState == TBCharacter.WallState.Fall;
				if (flag3)
				{
					this.animator.SetBool("PaQiang", false);
				}
				else
				{
					bool flag4 = this.characterController.wallState == TBCharacter.WallState.OnWall;
					if (flag4)
					{
						this.animator.SetBool("PaQiang", true);
					}
				}
			}
			bool flag5 = this.characterController.wallState == TBCharacter.WallState.OnGround && this.animator.GetBool("PaQiang");
			if (flag5)
			{
				this.animator.SetBool("PaQiang", false);
				this.animator.SetBool("PaQiangEnd", false);
			}
		}
	}

	// Token: 0x04002569 RID: 9577
	public float magnitude = 1f;
}

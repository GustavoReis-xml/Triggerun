using System;

// Token: 0x02000423 RID: 1059
public class TBCharAnimationJingKeFps : TBCharAnimationPlay
{
	// Token: 0x06002B89 RID: 11145 RVA: 0x00180160 File Offset: 0x0017E360
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x0018016C File Offset: 0x0017E36C
	protected override void Update()
	{
		base.Update();
		bool flag = this.characterController == null;
		if (!flag)
		{
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
			else
			{
				bool @bool = this.animator.GetBool("PaQiang");
				if (@bool)
				{
					this.animator.SetBool("PaQiang", false);
				}
			}
		}
	}
}

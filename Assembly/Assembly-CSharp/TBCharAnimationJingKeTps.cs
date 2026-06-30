using System;

// Token: 0x02000424 RID: 1060
public class TBCharAnimationJingKeTps : TBCharAnimationThirdIK
{
	// Token: 0x06002B8C RID: 11148 RVA: 0x00180252 File Offset: 0x0017E452
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002B8D RID: 11149 RVA: 0x0018025C File Offset: 0x0017E45C
	protected override void Update()
	{
		bool flag = this.characterController == null;
		if (!flag)
		{
			base.Update();
			bool isDead = base.hero.isDead;
			if (!isDead)
			{
				bool flag2 = this.characterController.wallState != TBCharacter.WallState.OnGround && this.characterController.wallState != TBCharacter.WallState.PaQiangEnd;
				if (flag2)
				{
					bool flag3 = this.characterController.wallState == TBCharacter.WallState.OnWall;
					if (flag3)
					{
						bool flag4 = !this.animator.GetBool("PaQiang");
						if (flag4)
						{
							this.animator.SetBool("PaQiang", true);
						}
					}
					else
					{
						bool @bool = this.animator.GetBool("PaQiang");
						if (@bool)
						{
							this.animator.SetBool("PaQiang", false);
							this.animator.SetBool("PaQiangEnd", false);
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
	}
}

using System;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class TBCharAnimationHouYiTps : TBCharAnimationThirdIK
{
	// Token: 0x06002B7D RID: 11133 RVA: 0x0017FD01 File Offset: 0x0017DF01
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x0017FD0C File Offset: 0x0017DF0C
	public void houyiSetFireforce()
	{
		this.animator.SetInteger("PlayerState", 3);
		this.fireforce = 0f;
		this.animator.SetFloat("fireforce", this.fireforce);
		this.animator.SetBool("CancelXuli", true);
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x0017FD60 File Offset: 0x0017DF60
	protected override void Update()
	{
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			base.Update();
			bool flag2 = this.animator.GetInteger("PlayerState") == 3 && !base.hero.isDead && this.fireforce != 0f;
			if (flag2)
			{
				bool flag3 = !this.animator.GetBool("CancelXuli");
				if (flag3)
				{
					this.animator.SetBool("CancelXuli", true);
				}
				this.fireTimer += Time.deltaTime * 0.5f;
				this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
				bool flag4 = this.fireforce < 0.1f;
				if (flag4)
				{
					this.fireforce = 0f;
				}
				this.animator.SetFloat("fireforce", this.fireforce);
			}
			bool flag5 = this.animator.GetInteger("PlayerState") == 1;
			if (flag5)
			{
				this.animator.SetFloat("fireforce", this.fireforce);
				this.fireTimer = 0f;
				this.fireforce += Time.deltaTime * 0.8f;
				bool flag6 = this.fireforce >= 1f;
				if (flag6)
				{
					this.fireforce = 1f;
				}
			}
			bool flag7 = this.animator.GetInteger("PlayerState") == 2;
			if (flag7)
			{
				this.fireTimer += Time.deltaTime * 0.5f;
				this.fireforce = Mathf.Lerp(this.fireforce, 0f, this.fireTimer);
				bool flag8 = this.fireforce < 0f;
				if (flag8)
				{
					this.fireforce = 0f;
				}
				this.animator.SetFloat("fireforce", this.fireforce);
			}
			bool flag9 = this.characterController.wallState != TBCharacter.WallState.OnGround && this.characterController.wallState != TBCharacter.WallState.PaQiangEnd;
			if (flag9)
			{
				bool flag10 = this.characterController.wallState == TBCharacter.WallState.OnWall;
				if (flag10)
				{
					bool flag11 = !this.animator.GetBool("PaQiang");
					if (flag11)
					{
						this.animator.SetBool("PaQiang", true);
						this.animator.SetBool("Fire", false);
						this.animator.SetFloat("fireforce", 0f);
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
			bool flag12 = this.characterController.wallState == TBCharacter.WallState.OnGround && this.animator.GetBool("PaQiang");
			if (flag12)
			{
				this.animator.SetBool("PaQiang", false);
				this.animator.SetBool("PaQiangEnd", false);
			}
		}
	}

	// Token: 0x04002558 RID: 9560
	private float fireforce;

	// Token: 0x04002559 RID: 9561
	private float fireTimer;
}

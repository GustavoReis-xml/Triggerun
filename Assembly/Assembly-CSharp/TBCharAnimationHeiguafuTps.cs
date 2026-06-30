using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class TBCharAnimationHeiguafuTps : TBCharAnimationThirdIK
{
	// Token: 0x06002B72 RID: 11122 RVA: 0x0017EE20 File Offset: 0x0017D020
	protected override void Start()
	{
		base.Start();
		this.recoilRight = base.GetComponent<TBRecoil>();
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x0017EE38 File Offset: 0x0017D038
	protected override void Update()
	{
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			float num = this.animator.GetFloat("WalkState");
			base.Update();
			bool flag2 = !this.animator.GetBool("Sniper") || this.animator.GetBool("Reload") || this.animator.GetBool("SkillGuilei") || this.animator.GetBool("SkillHongwai");
			if (flag2)
			{
				this.animator.SetFloat("WalkState", (!this.characterController.animState.crouch) ? 0f : -1f);
			}
			else
			{
				bool crouch = this.characterController.animState.crouch;
				if (crouch)
				{
					bool flag3 = num > -1f;
					if (flag3)
					{
						num = -1f;
					}
					num = Mathf.Lerp(num, -2f, Time.deltaTime * 2f);
				}
				else
				{
					bool flag4 = num < 0f || num > 1f;
					if (flag4)
					{
						num = 0f;
					}
					num = Mathf.Lerp(num, 1f, Time.deltaTime * 2f);
				}
				this.animator.SetFloat("WalkState", num);
			}
			int integer = this.animator.GetInteger("AttackState");
			bool flag5 = integer != 0;
			if (flag5)
			{
				this.recoilRight.Fire(this.magnitude);
				this.animator.SetInteger("AttackState", integer);
			}
		}
	}

	// Token: 0x04002544 RID: 9540
	private TBRecoil recoilRight;

	// Token: 0x04002545 RID: 9541
	public float magnitude = 1f;
}

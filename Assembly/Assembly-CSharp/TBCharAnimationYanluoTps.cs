using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class TBCharAnimationYanluoTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BF3 RID: 11251 RVA: 0x001816C4 File Offset: 0x0017F8C4
	protected override void Start()
	{
		base.Start();
		TBRecoil[] components = base.GetComponents<TBRecoil>();
		for (int i = 0; i < components.Length; i++)
		{
			bool flag = components[i].handedness == TBRecoil.Handedness.Left;
			if (flag)
			{
				this.recoilLeft = components[i];
			}
			else
			{
				this.recoilRight = components[i];
			}
		}
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x0018171C File Offset: 0x0017F91C
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			int integer = this.animator.GetInteger("AttackState");
			int num = integer;
			if (num != 1)
			{
				if (num == 2)
				{
					this.recoilRight.Fire(this.magnitude);
					this.animator.SetInteger("AttackState", 0);
				}
			}
			else
			{
				this.recoilLeft.Fire(this.magnitude);
				this.animator.SetInteger("AttackState", 0);
			}
		}
	}

	// Token: 0x0400256A RID: 9578
	private TBRecoil recoilLeft;

	// Token: 0x0400256B RID: 9579
	private TBRecoil recoilRight;

	// Token: 0x0400256C RID: 9580
	public float magnitude = 1f;
}

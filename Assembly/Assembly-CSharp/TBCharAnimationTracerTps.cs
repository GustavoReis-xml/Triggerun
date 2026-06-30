using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class TBCharAnimationTracerTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BDE RID: 11230 RVA: 0x001813A0 File Offset: 0x0017F5A0
	protected override void Start()
	{
		base.Start();
		this.recoilRight = base.GetComponent<TBRecoil>();
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x001813B8 File Offset: 0x0017F5B8
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool flag2 = this.animator.GetInteger("AttackState") != 0;
			if (flag2)
			{
				this.recoilRight.Fire(this.magnitude);
				this.animator.SetInteger("AttackState", 0);
			}
		}
	}

	// Token: 0x04002567 RID: 9575
	private TBRecoil recoilRight;

	// Token: 0x04002568 RID: 9576
	public float magnitude = 1f;
}

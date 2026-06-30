using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class TBCharAnimationXingTianTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BEF RID: 11247 RVA: 0x0018168F File Offset: 0x0017F88F
	protected override void Start()
	{
		base.Start();
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
	}

	// Token: 0x06002BF0 RID: 11248 RVA: 0x001816A6 File Offset: 0x0017F8A6
	protected override void Update()
	{
		base.Update();
	}
}

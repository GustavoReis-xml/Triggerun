using System;

// Token: 0x0200045C RID: 1116
public class TBCharAnimationXingTianFps : TBCharAnimationPlay
{
	// Token: 0x06002BED RID: 11245 RVA: 0x0018166A File Offset: 0x0017F86A
	protected override void Start()
	{
		base.Start();
		this.animator.SetBool("SuperAction", false);
	}
}

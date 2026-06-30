using System;

// Token: 0x0200005D RID: 93
public class CBulletLineBendIt : CBulletIt
{
	// Token: 0x06000336 RID: 822 RVA: 0x00017828 File Offset: 0x00015A28
	private void Start()
	{
		bool flag = !base.readyDestory;
		if (flag)
		{
			this._hitPoint = base.bornPoint;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}
}

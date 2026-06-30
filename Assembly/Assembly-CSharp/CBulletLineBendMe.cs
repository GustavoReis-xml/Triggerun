using System;

// Token: 0x0200005E RID: 94
public class CBulletLineBendMe : CBulletMe
{
	// Token: 0x06000338 RID: 824 RVA: 0x00017870 File Offset: 0x00015A70
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

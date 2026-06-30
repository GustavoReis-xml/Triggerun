using System;

// Token: 0x02000052 RID: 82
public class CBulletCircleIt : CBulletIt
{
	// Token: 0x060002F4 RID: 756 RVA: 0x0001636C File Offset: 0x0001456C
	private void Start()
	{
		bool flag = ((CBulletCircleData)base.bulletData).hitDamage == 1 && !base.readyDestory;
		if (flag)
		{
			this._hitPoint = base.bornPoint;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x000163C1 File Offset: 0x000145C1
	private void FixedUpdate()
	{
		this.checkDelayBoom();
	}
}

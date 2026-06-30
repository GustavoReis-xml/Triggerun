using System;

// Token: 0x02000053 RID: 83
public class CBulletCircleMe : CBulletMe
{
	// Token: 0x060002F7 RID: 759 RVA: 0x000163D4 File Offset: 0x000145D4
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

	// Token: 0x060002F8 RID: 760 RVA: 0x00016429 File Offset: 0x00014629
	private void FixedUpdate()
	{
		this.checkDelayBoom();
	}
}

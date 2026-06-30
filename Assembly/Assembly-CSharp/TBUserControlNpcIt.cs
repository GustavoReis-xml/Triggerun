using System;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class TBUserControlNpcIt : TBUserControlIt
{
	// Token: 0x06002C23 RID: 11299 RVA: 0x00183B0C File Offset: 0x00181D0C
	public override void InitControl(NetLiving h)
	{
		base.InitControl(h);
		this.moveTarget = h.playerCommon.pos;
		bool flag = this.lookAt == Vector3.zero;
		if (flag)
		{
			this.lookAt = this.hero.thisModel.transform.forward * 100f + this.hero.thisModel.transform.position;
		}
		this.inited = true;
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x00183B90 File Offset: 0x00181D90
	protected override void Update()
	{
		float magnitude = (this.lastMoveTarget - this.moveTarget).magnitude;
		bool flag = magnitude < 0.1f;
		if (flag)
		{
			this.lastMoveNumber++;
		}
		else
		{
			this.lastMoveNumber = 0;
		}
		bool flag2 = this.lastMoveNumber > 50;
		if (flag2)
		{
			this.mayBeStand = true;
		}
		else
		{
			this.mayBeStand = false;
		}
		this.lastMoveTarget = this.moveTarget;
		Vector3 vector = this.moveTarget - base.transform.position;
		bool onground = this.state.onground;
		if (onground)
		{
			float num = (!(this.state.move != Vector3.zero)) ? (this.stoppingDistance * this.stoppingThreshold) : this.stoppingDistance;
			Vector3 normalized = vector.normalized;
			bool flag3 = vector.magnitude > num;
			if (flag3)
			{
				this.state.move = normalized;
			}
			else
			{
				this.state.move = Vector3.zero;
			}
		}
		else
		{
			vector.y = 0f;
			this.state.move = vector.normalized;
			bool flag4 = !this.state.fly && vector.magnitude < this.stoppingDistance * 2f;
			if (flag4)
			{
				this.state.move = Vector3.zero;
			}
		}
		bool flag5 = this.state.move.x == 0f || this.state.move.z == 0f;
		if (flag5)
		{
			this.mixInput = 1;
		}
		else
		{
			this.mixInput = 1;
		}
		this.state.lookPos = this.lookAt;
	}

	// Token: 0x040025A0 RID: 9632
	private Vector3 lastMoveTarget = Vector3.zero;

	// Token: 0x040025A1 RID: 9633
	private int lastMoveNumber;
}

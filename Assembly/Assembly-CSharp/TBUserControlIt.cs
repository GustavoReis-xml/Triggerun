using System;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class TBUserControlIt : TBUserControl
{
	// Token: 0x06002C1A RID: 11290 RVA: 0x00182EE8 File Offset: 0x001810E8
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

	// Token: 0x06002C1B RID: 11291 RVA: 0x00182F6C File Offset: 0x0018116C
	public void SetAnimatorUpdate(string ani, int type, object parm)
	{
		switch (type)
		{
		case 0:
			parm = ((int)parm == 1);
			break;
		case 1:
			parm = (float)((int)parm) * 0.01f;
			break;
		case 2:
			break;
		default:
			return;
		}
		bool flag = this.hero.tCharAnimationTps != null;
		if (flag)
		{
			this.hero.tCharAnimationTps.SetAnimator(ani, type, parm);
		}
		bool flag2 = this.hero.isCopy && this.hero.tCharAnimationFps != null;
		if (flag2)
		{
			this.hero.tCharAnimationFps.SetAnimator(ani, type, parm);
		}
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x00183028 File Offset: 0x00181228
	protected override void Update()
	{
		bool flag = this.hero == null;
		if (!flag)
		{
			float magnitude = (this.lastMoveTarget - this.moveTarget).magnitude;
			bool flag2 = magnitude < 0.1f;
			if (flag2)
			{
				this.lastMoveNumber++;
				bool flag3 = this.lastMoveNumber > 30;
				if (flag3)
				{
					this.mayBeStand = true;
				}
			}
			else
			{
				this.lastMoveNumber = 0;
				this.mayBeStand = false;
				this.lastMoveTarget = this.moveTarget;
			}
			Vector3 vector = this.moveTarget - base.transform.position;
			bool isDead = this.hero.isDead;
			if (isDead)
			{
				this.state.move = Vector3.zero;
			}
			else
			{
				bool onground = this.state.onground;
				if (onground)
				{
					float num = (!(this.state.move != Vector3.zero)) ? (this.stoppingDistance * this.stoppingThreshold) : this.stoppingDistance;
					Vector3 normalized = vector.normalized;
					bool flag4 = vector.magnitude > num;
					if (flag4)
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
					bool flag5 = !this.state.fly && vector.magnitude < this.stoppingDistance * 2f;
					if (flag5)
					{
						this.state.move = Vector3.zero;
					}
				}
			}
			bool flag6 = this.state.move.x == 0f || this.state.move.z == 0f;
			if (flag6)
			{
				this.mixInput = 1;
			}
			else
			{
				this.mixInput = 1;
			}
			this.state.lookPos = this.lookAt;
		}
	}

	// Token: 0x04002596 RID: 9622
	public Vector3 moveTarget;

	// Token: 0x04002597 RID: 9623
	public Vector3 lookAt = Vector3.zero;

	// Token: 0x04002598 RID: 9624
	public bool mayBeStand;

	// Token: 0x04002599 RID: 9625
	public bool jump;

	// Token: 0x0400259A RID: 9626
	public float stoppingDistance = 0.2f;

	// Token: 0x0400259B RID: 9627
	public float stoppingThreshold = 2f;

	// Token: 0x0400259C RID: 9628
	private Vector3 lastMoveTarget = Vector3.zero;

	// Token: 0x0400259D RID: 9629
	private int lastMoveNumber;
}

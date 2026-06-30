using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class CMuzzlePhysicalTrigger : MonoBehaviour
{
	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000646 RID: 1606 RVA: 0x00033E5C File Offset: 0x0003205C
	// (set) Token: 0x06000647 RID: 1607 RVA: 0x00033E74 File Offset: 0x00032074
	public bool triggerEnable
	{
		get
		{
			return this._triggerEnable;
		}
		set
		{
			this._triggerEnable = value;
			bool triggerEnable = this._triggerEnable;
			if (triggerEnable)
			{
				this.rig.isKinematic = false;
				this.coll.enabled = true;
			}
			else
			{
				this.rig.isKinematic = true;
				this.coll.enabled = false;
			}
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00033ED0 File Offset: 0x000320D0
	public void InitTrigger(NetLiving net, CMuzzle mu)
	{
		this.muzzle = mu;
		this.myPos = base.transform.localPosition;
		this.myRot = base.transform.localEulerAngles;
		this.rig = base.GetComponent<Rigidbody>();
		this.rig.isKinematic = true;
		this.rig.useGravity = false;
		this.rig.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		this.coll = base.GetComponent<Collider>();
		bool flag = this.coll != null;
		if (flag)
		{
			this.coll.isTrigger = false;
			this.coll.enabled = false;
		}
		this.inited = true;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00033F7C File Offset: 0x0003217C
	private void OnCollisionEnter(Collision collision)
	{
		bool triggerEnable = this._triggerEnable;
		if (triggerEnable)
		{
			this.muzzle.OnHitByPhysical(collision);
		}
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00033FA4 File Offset: 0x000321A4
	private void FixedUpdate()
	{
		bool flag = this.inited;
		if (flag)
		{
			this.rig.velocity = Vector3.zero;
			this.rig.angularVelocity = Vector3.zero;
			base.transform.localEulerAngles = this.myRot;
			base.transform.localPosition = this.myPos;
		}
	}

	// Token: 0x0400048C RID: 1164
	private CMuzzle muzzle;

	// Token: 0x0400048D RID: 1165
	private bool _triggerEnable;

	// Token: 0x0400048E RID: 1166
	private Vector3 myPos;

	// Token: 0x0400048F RID: 1167
	private Vector3 myRot;

	// Token: 0x04000490 RID: 1168
	private Rigidbody rig;

	// Token: 0x04000491 RID: 1169
	private Collider coll;

	// Token: 0x04000492 RID: 1170
	private bool inited;
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class BloodFade : MonoBehaviour
{
	// Token: 0x060001D8 RID: 472 RVA: 0x0000BF24 File Offset: 0x0000A124
	private void Start()
	{
		base.StartCoroutine(this.BloodFades());
		this.collide = base.gameObject.GetComponent<BoxCollider>();
		this.rig = base.gameObject.GetComponent<Rigidbody>();
		this.timer = Time.time;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000BF64 File Offset: 0x0000A164
	private void Update()
	{
		Vector3 velocity = this.rig.velocity;
		bool flag = velocity == Vector3.zero && Time.time - this.timer > 0.005f && !this.collide.isTrigger;
		if (flag)
		{
			this.collide.isTrigger = true;
			this.rig.isKinematic = true;
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000BFCF File Offset: 0x0000A1CF
	private IEnumerator BloodFades()
	{
		yield return new WaitForSeconds(8f);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000170 RID: 368
	private Collider collide;

	// Token: 0x04000171 RID: 369
	private Rigidbody rig;

	// Token: 0x04000172 RID: 370
	private float timer;
}

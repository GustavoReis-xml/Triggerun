using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class CTestDropGround : MonoBehaviour
{
	// Token: 0x060007B0 RID: 1968 RVA: 0x00043610 File Offset: 0x00041810
	private void Update()
	{
		Debug.DrawRay(this.point, this.dir * 20f, Color.red);
		Debug.Log(this.angle.ToString("F3"));
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		if (mouseButtonDown)
		{
			base.transform.position = Vector3.up * 10f;
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0004367C File Offset: 0x0004187C
	private void OnCollisionEnter(Collision collision)
	{
		this.point = collision.contacts[0].point;
		this.dir = collision.contacts[0].normal;
		this.angle = Vector3.Dot(this.dir, Vector3.up);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x000436CE File Offset: 0x000418CE
	private void OnTriggerEnter(Collider collision)
	{
		this.point = collision.transform.position;
		this.dir = -base.GetComponent<Rigidbody>().velocity;
	}

	// Token: 0x040005BA RID: 1466
	private Vector3 point;

	// Token: 0x040005BB RID: 1467
	private Vector3 dir;

	// Token: 0x040005BC RID: 1468
	private float angle;
}

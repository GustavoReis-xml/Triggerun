using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class CTestRigAddForce : MonoBehaviour
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00043DC8 File Offset: 0x00041FC8
	private Transform p
	{
		get
		{
			bool flag = this._p == null;
			if (flag)
			{
				GameObject gameObject = new GameObject("parent");
				this._p = gameObject.transform;
			}
			return this._p;
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00043E09 File Offset: 0x00042009
	private void Start()
	{
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00043E0C File Offset: 0x0004200C
	private void Update()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		if (mouseButtonDown)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject.transform.parent = this.p;
			gameObject.transform.position = base.transform.position;
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			rigidbody.mass = this.mass;
			rigidbody.useGravity = false;
			rigidbody.angularDrag = 0f;
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			rigidbody.AddForce(base.transform.forward * this.max, ForceMode.Impulse);
		}
	}

	// Token: 0x040005C6 RID: 1478
	private Transform _p;

	// Token: 0x040005C7 RID: 1479
	public float max = 0.33f;

	// Token: 0x040005C8 RID: 1480
	public float mass = 0.01f;
}

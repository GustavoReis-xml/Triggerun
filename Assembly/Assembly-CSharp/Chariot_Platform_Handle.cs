using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class Chariot_Platform_Handle : MonoBehaviour
{
	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x0600044D RID: 1101 RVA: 0x00021CCC File Offset: 0x0001FECC
	// (set) Token: 0x0600044E RID: 1102 RVA: 0x00021D37 File Offset: 0x0001FF37
	public Rigidbody Rigidbody_Component
	{
		get
		{
			bool flag = this._Rigidbody_Component == null;
			if (flag)
			{
				this._Rigidbody_Component = base.gameObject.GetComponent<Rigidbody>();
				bool flag2 = this._Rigidbody_Component == null;
				if (flag2)
				{
					this._Rigidbody_Component = base.gameObject.AddComponent<Rigidbody>();
				}
				this._Rigidbody_Component.isKinematic = true;
			}
			return this._Rigidbody_Component;
		}
		set
		{
			this._Rigidbody_Component = value;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x0600044F RID: 1103 RVA: 0x00021D44 File Offset: 0x0001FF44
	// (set) Token: 0x06000450 RID: 1104 RVA: 0x00021D5C File Offset: 0x0001FF5C
	public GameObject Parent_Object
	{
		get
		{
			return this._Parent_Object;
		}
		set
		{
			this._Parent_Object = value;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000451 RID: 1105 RVA: 0x00021D68 File Offset: 0x0001FF68
	// (set) Token: 0x06000452 RID: 1106 RVA: 0x00021DC7 File Offset: 0x0001FFC7
	public GameObject Target_Object
	{
		get
		{
			bool flag = this._Target_Object == null;
			if (flag)
			{
				bool flag2 = this.Parent_Object == null;
				if (flag2)
				{
					return null;
				}
				this._Target_Object = this.Parent_Object.transform.Find("guancai").gameObject;
			}
			return this._Target_Object;
		}
		set
		{
			this._Target_Object = value;
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00021DD4 File Offset: 0x0001FFD4
	public void Initialize()
	{
		BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
		boxCollider.center = new Vector3(0f, 1.4f, 0f);
		boxCollider.size = new Vector3(1f, 0.0001f, 3f);
		base.gameObject.transform.parent = null;
		base.gameObject.transform.localPosition = Vector3.zero;
		base.gameObject.transform.localEulerAngles = Vector3.zero;
		base.gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00021E7C File Offset: 0x0002007C
	private void Update()
	{
		bool flag = !(this.Target_Object == null);
		if (flag)
		{
			this.Rigidbody_Component.MovePosition(this.Target_Object.transform.position);
			this.Rigidbody_Component.MoveRotation(this.Target_Object.transform.rotation);
		}
	}

	// Token: 0x04000356 RID: 854
	private Rigidbody _Rigidbody_Component;

	// Token: 0x04000357 RID: 855
	private GameObject _Parent_Object;

	// Token: 0x04000358 RID: 856
	private GameObject _Target_Object;
}

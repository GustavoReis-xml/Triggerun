using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class CTest2 : MonoBehaviour
{
	// Token: 0x060007A2 RID: 1954 RVA: 0x0004329C File Offset: 0x0004149C
	private void Start()
	{
		this.coll = base.GetComponent<BoxCollider>();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000432AC File Offset: 0x000414AC
	private void Update()
	{
		RaycastHit[] array = new RaycastHit[10];
		this.sizeBox = new Vector3(this.coll.transform.localScale.x * this.coll.size.x * 0.5f, this.coll.transform.localScale.y * this.coll.size.y * 0.5f, this.coll.transform.localScale.z * this.coll.size.z * 0.5f);
		array = Physics.BoxCastAll(this.coll.bounds.center, this.sizeBox, Vector3.up, this.coll.transform.rotation, 0f);
		bool flag = array.Length == 0;
		if (!flag)
		{
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = !(this.coll == array[i].collider);
				if (flag2)
				{
					Debug.Log(array[i].collider.name);
				}
			}
		}
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x000433E5 File Offset: 0x000415E5
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("enter");
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000433F3 File Offset: 0x000415F3
	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Exit");
	}

	// Token: 0x040005B1 RID: 1457
	private BoxCollider coll;

	// Token: 0x040005B2 RID: 1458
	private Vector3 sizeBox;
}

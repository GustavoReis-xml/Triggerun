using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class CmousePut : MonoBehaviour
{
	// Token: 0x060005AB RID: 1451 RVA: 0x0002D576 File Offset: 0x0002B776
	private void Start()
	{
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0002D57C File Offset: 0x0002B77C
	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		Physics.Raycast(ray, out raycastHit, 100f);
		bool flag = null != raycastHit.transform;
		if (flag)
		{
			Debug.Log(raycastHit.normal.normalized);
			bool flag2 = raycastHit.normal.y > 0.8f;
			if (flag2)
			{
				base.transform.position = raycastHit.point;
			}
			else
			{
				base.transform.position = raycastHit.point;
			}
		}
	}
}

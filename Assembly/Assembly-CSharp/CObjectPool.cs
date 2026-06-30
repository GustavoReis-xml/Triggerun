using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class CObjectPool
{
	// Token: 0x06000667 RID: 1639 RVA: 0x000360E4 File Offset: 0x000342E4
	public CObjectPool(int _gid)
	{
		this.GID = _gid;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00036100 File Offset: 0x00034300
	public Transform getOne()
	{
		Transform transform = null;
		bool flag = this.transFormFree.Count == 0;
		Transform result;
		if (flag)
		{
			result = transform;
		}
		else
		{
			transform = this.transFormFree[0];
			this.transFormFree.RemoveAt(0);
			result = transform;
		}
		return result;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00036146 File Offset: 0x00034346
	public void PutOne(Transform tf)
	{
		this.transFormFree.Add(tf);
	}

	// Token: 0x04000497 RID: 1175
	public int GID;

	// Token: 0x04000498 RID: 1176
	public List<Transform> transFormFree = new List<Transform>();
}

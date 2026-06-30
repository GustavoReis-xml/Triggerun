using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class MemGC : MonoBehaviour
{
	// Token: 0x06001A66 RID: 6758 RVA: 0x000FAE7C File Offset: 0x000F907C
	private void Start()
	{
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x000FAE7F File Offset: 0x000F907F
	public void GCMem()
	{
		this.GCResDic();
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000FAE95 File Offset: 0x000F9095
	private void GCAllMem()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x000FAEA4 File Offset: 0x000F90A4
	private void GCResDic()
	{
		bool flag = this.isFirst;
		if (flag)
		{
			this.isFirst = false;
		}
	}

	// Token: 0x04001510 RID: 5392
	public bool isFirst = true;
}

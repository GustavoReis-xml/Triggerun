using System;
using UnityEngine;

// Token: 0x020004DC RID: 1244
public class _LushLODTreesNoDestroy : MonoBehaviour
{
	// Token: 0x06002F8F RID: 12175 RVA: 0x001AA8EA File Offset: 0x001A8AEA
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}

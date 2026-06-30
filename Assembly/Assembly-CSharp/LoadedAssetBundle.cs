using System;
using UnityEngine;

// Token: 0x0200021E RID: 542
public class LoadedAssetBundle
{
	// Token: 0x06001882 RID: 6274 RVA: 0x000E5B4F File Offset: 0x000E3D4F
	public LoadedAssetBundle(WWW wobj, AssetBundle assetBundle)
	{
		this.m_AssetBundle = assetBundle;
		this.m_ReferencedCount = 1;
		this.m_Wwwobj = wobj;
	}

	// Token: 0x04001345 RID: 4933
	public AssetBundle m_AssetBundle;

	// Token: 0x04001346 RID: 4934
	public int m_ReferencedCount;

	// Token: 0x04001347 RID: 4935
	public WWW m_Wwwobj;
}

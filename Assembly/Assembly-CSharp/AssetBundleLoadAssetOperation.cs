using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
{
	// Token: 0x06000155 RID: 341
	public abstract T GetAsset<T>() where T : UnityEngine.Object;
}

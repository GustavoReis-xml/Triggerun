using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
{
	// Token: 0x06000165 RID: 357 RVA: 0x00007F98 File Offset: 0x00006198
	public AssetBundleLoadManifestOperation(string bundleName, string assetName, Type type) : base(bundleName, assetName, type)
	{
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00007FA8 File Offset: 0x000061A8
	public override bool Update()
	{
		base.Update();
		bool flag = this.m_Request != null && this.m_Request.isDone;
		bool result;
		if (flag)
		{
			AssetBundleManager.AssetBundleManifestObject = this.GetAsset<AssetBundleManifest>();
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}
}

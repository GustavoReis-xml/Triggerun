using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class AssetBundleLoadWWWOperation : AssetBundleLoadAssetOperation
{
	// Token: 0x0600016D RID: 365 RVA: 0x00008017 File Offset: 0x00006217
	public AssetBundleLoadWWWOperation(string bundleName)
	{
		this.m_AssetBundleName = bundleName;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00008028 File Offset: 0x00006228
	public override T GetAsset<T>()
	{
		return this.m_Bundle as T;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000804C File Offset: 0x0000624C
	public LoadedAssetBundle GetWWW()
	{
		return this.m_Bundle;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00008064 File Offset: 0x00006264
	public override bool Update()
	{
		LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError);
		bool flag = loadedAssetBundle != null;
		bool result;
		if (flag)
		{
			this.m_Bundle = loadedAssetBundle;
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x000080A0 File Offset: 0x000062A0
	public override bool IsDone()
	{
		bool flag = this.m_Bundle == null && this.m_DownloadingError != null;
		bool result;
		if (flag)
		{
			Debug.LogError(this.m_DownloadingError);
			result = true;
		}
		else
		{
			result = (this.m_Bundle != null);
		}
		return result;
	}

	// Token: 0x040000F3 RID: 243
	protected string m_AssetBundleName;

	// Token: 0x040000F4 RID: 244
	protected string m_DownloadingError;

	// Token: 0x040000F5 RID: 245
	protected LoadedAssetBundle m_Bundle;
}

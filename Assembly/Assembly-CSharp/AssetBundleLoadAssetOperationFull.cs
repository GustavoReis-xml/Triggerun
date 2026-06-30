using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
{
	// Token: 0x06000157 RID: 343 RVA: 0x00007D18 File Offset: 0x00005F18
	public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, Type type)
	{
		this.m_AssetBundleName = bundleName;
		this.m_AssetName = assetName;
		this.m_Type = type;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00007D38 File Offset: 0x00005F38
	public override T GetAsset<T>()
	{
		bool flag = this.m_Request != null && this.m_Request.isDone;
		T result;
		if (flag)
		{
			result = (this.m_Request.asset as T);
		}
		else
		{
			result = default(T);
		}
		return result;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00007D88 File Offset: 0x00005F88
	public override bool Update()
	{
		bool flag = this.m_Request != null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError);
			bool flag2 = loadedAssetBundle != null;
			if (flag2)
			{
				this.m_Request = loadedAssetBundle.m_AssetBundle.LoadAssetAsync(this.m_AssetName, this.m_Type);
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00007DEC File Offset: 0x00005FEC
	public override bool IsDone()
	{
		bool flag = this.m_Request == null && this.m_DownloadingError != null;
		bool result;
		if (flag)
		{
			Debug.LogError(this.m_DownloadingError);
			result = true;
		}
		else
		{
			result = (this.m_Request != null && this.m_Request.isDone);
		}
		return result;
	}

	// Token: 0x040000E8 RID: 232
	protected string m_AssetBundleName;

	// Token: 0x040000E9 RID: 233
	protected string m_AssetName;

	// Token: 0x040000EA RID: 234
	protected string m_DownloadingError;

	// Token: 0x040000EB RID: 235
	protected Type m_Type;

	// Token: 0x040000EC RID: 236
	protected AssetBundleRequest m_Request;
}

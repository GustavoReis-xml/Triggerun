using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000024 RID: 36
public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
{
	// Token: 0x0600015F RID: 351 RVA: 0x00007E9B File Offset: 0x0000609B
	public AssetBundleLoadLevelOperation(string assetbundleName, string levelName, bool isAdditive)
	{
		this.m_AssetBundleName = assetbundleName;
		this.m_LevelName = levelName;
		this.m_IsAdditive = isAdditive;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00007EBC File Offset: 0x000060BC
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
				this.m_Request = SceneManager.LoadSceneAsync(this.m_LevelName);
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00007F14 File Offset: 0x00006114
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

	// Token: 0x040000EE RID: 238
	protected string m_AssetBundleName;

	// Token: 0x040000EF RID: 239
	protected string m_LevelName;

	// Token: 0x040000F0 RID: 240
	protected bool m_IsAdditive;

	// Token: 0x040000F1 RID: 241
	protected string m_DownloadingError;

	// Token: 0x040000F2 RID: 242
	protected AsyncOperation m_Request;
}

using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class BaseLoader : MonoBehaviour
{
	// Token: 0x0600019C RID: 412 RVA: 0x00008E6F File Offset: 0x0000706F
	private IEnumerator Start()
	{
		yield return base.StartCoroutine(this.Initialize());
		yield break;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00008E7E File Offset: 0x0000707E
	protected IEnumerator Initialize()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		string platformFolderForAssetBundles = BaseLoader.GetPlatformFolderForAssetBundles(Application.platform);
		string relativePath = this.GetRelativePath();
		AssetBundleManager.BaseDownloadingURL = string.Concat(new string[]
		{
			relativePath,
			"/",
			SysConfig.streamPath,
			"/",
			platformFolderForAssetBundles,
			"/"
		});
		AssetBundleLoadManifestOperation request = AssetBundleManager.Initialize(platformFolderForAssetBundles);
		bool flag = request != null;
		if (flag)
		{
			yield return base.StartCoroutine(request);
		}
		yield break;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00008E90 File Offset: 0x00007090
	public string GetRelativePath()
	{
		bool isEditor = Application.isEditor;
		string result;
		if (isEditor)
		{
			result = "file://" + Environment.CurrentDirectory.Replace("\\", "/");
		}
		else
		{
			bool isWebPlayer = Application.isWebPlayer;
			if (isWebPlayer)
			{
				result = Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/" + SysConfig.streamPath;
			}
			else
			{
				bool flag = Application.isMobilePlatform || Application.isConsolePlatform;
				if (flag)
				{
					result = Application.streamingAssetsPath;
				}
				else
				{
					result = "file://" + Application.streamingAssetsPath;
				}
			}
		}
		return result;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00008F30 File Offset: 0x00007130
	private static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
	{
		if (!true)
		{
		}
		string result;
		if (platform <= RuntimePlatform.WindowsPlayer)
		{
			if (platform == RuntimePlatform.OSXPlayer)
			{
				result = "OSX";
				goto IL_46;
			}
			if (platform == RuntimePlatform.WindowsPlayer)
			{
				result = "Windows";
				goto IL_46;
			}
		}
		else
		{
			if (platform == RuntimePlatform.IPhonePlayer)
			{
				result = "iOS";
				goto IL_46;
			}
			if (platform == RuntimePlatform.Android)
			{
				result = "Android";
				goto IL_46;
			}
		}
		result = null;
		IL_46:
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00008F8C File Offset: 0x0000718C
	protected IEnumerator Load(string assetBundleName, string assetName)
	{
		Debug.Log("Start to load " + assetName + " at frame " + Time.frameCount.ToString());
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
		bool flag = request != null;
		if (flag)
		{
			yield return base.StartCoroutine(request);
			GameObject prefab = request.GetAsset<GameObject>();
			Debug.Log(assetName + ((!(prefab == null)) ? " is" : " isn't") + " loaded successfully at frame " + Time.frameCount.ToString());
			bool flag2 = prefab != null;
			if (flag2)
			{
				UnityEngine.Object.Instantiate<GameObject>(prefab);
			}
			prefab = null;
		}
		yield break;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00008FA9 File Offset: 0x000071A9
	protected IEnumerator LoadLevel(string assetBundleName, string levelName, bool isAdditive)
	{
		Debug.Log("Start to load scene " + levelName + " at frame " + Time.frameCount.ToString());
		AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(assetBundleName, levelName, isAdditive);
		bool flag = request != null;
		if (flag)
		{
			yield return base.StartCoroutine(request);
			Debug.Log("Finish loading scene " + levelName + " at frame " + Time.frameCount.ToString());
		}
		yield break;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00008FCD File Offset: 0x000071CD
	protected void Update()
	{
	}
}

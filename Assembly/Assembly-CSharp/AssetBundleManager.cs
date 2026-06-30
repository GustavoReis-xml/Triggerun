using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class AssetBundleManager : MonoBehaviour
{
	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000172 RID: 370 RVA: 0x000080E4 File Offset: 0x000062E4
	// (set) Token: 0x06000173 RID: 371 RVA: 0x000080FB File Offset: 0x000062FB
	public static string BaseDownloadingURL
	{
		get
		{
			return AssetBundleManager.m_BaseDownloadingURL;
		}
		set
		{
			AssetBundleManager.m_BaseDownloadingURL = value;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000174 RID: 372 RVA: 0x00008104 File Offset: 0x00006304
	// (set) Token: 0x06000175 RID: 373 RVA: 0x0000811B File Offset: 0x0000631B
	public static string[] Variants
	{
		get
		{
			return AssetBundleManager.m_Variants;
		}
		set
		{
			AssetBundleManager.m_Variants = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (set) Token: 0x06000176 RID: 374 RVA: 0x00008124 File Offset: 0x00006324
	public static AssetBundleManifest AssetBundleManifestObject
	{
		set
		{
			AssetBundleManager.m_AssetBundleManifest = value;
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00008130 File Offset: 0x00006330
	public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
	{
		bool flag = AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out error);
		LoadedAssetBundle result;
		if (flag)
		{
			result = null;
		}
		else
		{
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			bool flag2 = loadedAssetBundle == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string[] array = null;
				bool flag3 = !AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array);
				if (flag3)
				{
					result = loadedAssetBundle;
				}
				else
				{
					string[] array2 = array;
					foreach (string key in array2)
					{
						bool flag4 = AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out error);
						if (flag4)
						{
							return loadedAssetBundle;
						}
						LoadedAssetBundle loadedAssetBundle2;
						AssetBundleManager.m_LoadedAssetBundles.TryGetValue(key, out loadedAssetBundle2);
						bool flag5 = loadedAssetBundle2 == null;
						if (flag5)
						{
							return null;
						}
					}
					result = loadedAssetBundle;
				}
			}
		}
		return result;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x000081F8 File Offset: 0x000063F8
	public static AssetBundleLoadManifestOperation Initialize(string manifestAssetBundleName)
	{
		GameObject target = new GameObject("AssetBundleManager", new Type[]
		{
			typeof(AssetBundleManager)
		});
		UnityEngine.Object.DontDestroyOnLoad(target);
		AssetBundleManager.LoadAssetBundle(manifestAssetBundleName, true);
		AssetBundleLoadManifestOperation assetBundleLoadManifestOperation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
		AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadManifestOperation);
		return assetBundleLoadManifestOperation;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000825C File Offset: 0x0000645C
	protected static void LoadAssetBundle(string assetBundleName, bool isLoadingAssetBundleManifest = false)
	{
		bool flag = !isLoadingAssetBundleManifest;
		if (flag)
		{
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
		}
		bool flag2 = !AssetBundleManager.LoadAssetBundleInternal(assetBundleName, isLoadingAssetBundleManifest) && !isLoadingAssetBundleManifest;
		if (flag2)
		{
			AssetBundleManager.LoadDependencies(assetBundleName);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000829C File Offset: 0x0000649C
	protected static string RemapVariantName(string assetBundleName)
	{
		string[] allAssetBundlesWithVariant = AssetBundleManager.m_AssetBundleManifest.GetAllAssetBundlesWithVariant();
		bool flag = Array.IndexOf<string>(allAssetBundlesWithVariant, assetBundleName) < 0;
		string result;
		if (flag)
		{
			result = assetBundleName;
		}
		else
		{
			string[] array = assetBundleName.Split(new char[]
			{
				'.'
			});
			int num = int.MaxValue;
			int num2 = -1;
			for (int i = 0; i < allAssetBundlesWithVariant.Length; i++)
			{
				string[] array2 = allAssetBundlesWithVariant[i].Split(new char[]
				{
					'.'
				});
				bool flag2 = !(array2[0] != array[0]);
				if (flag2)
				{
					int num3 = Array.IndexOf<string>(AssetBundleManager.m_Variants, array2[1]);
					bool flag3 = num3 != -1 && num3 < num;
					if (flag3)
					{
						num = num3;
						num2 = i;
					}
				}
			}
			bool flag4 = num2 != -1;
			if (flag4)
			{
				result = allAssetBundlesWithVariant[num2];
			}
			else
			{
				result = assetBundleName;
			}
		}
		return result;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00008378 File Offset: 0x00006578
	protected static bool LoadAssetBundleInternal(string assetBundleName, bool isLoadingAssetBundleManifest)
	{
		LoadedAssetBundle loadedAssetBundle = null;
		AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
		bool flag = loadedAssetBundle != null;
		bool result;
		if (flag)
		{
			loadedAssetBundle.m_ReferencedCount++;
			result = true;
		}
		else
		{
			bool flag2 = AssetBundleManager.m_DownloadingWWWs.ContainsKey(assetBundleName);
			if (flag2)
			{
				result = true;
			}
			else
			{
				string url = AssetBundleManager.m_BaseDownloadingURL + assetBundleName;
				WWW value = (!isLoadingAssetBundleManifest) ? WWW.LoadFromCacheOrDownload(url, AssetBundleManager.m_AssetBundleManifest.GetAssetBundleHash(assetBundleName), 0U) : new WWW(url);
				AssetBundleManager.m_DownloadingWWWs.Add(assetBundleName, value);
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000840C File Offset: 0x0000660C
	protected static void LoadDependencies(string assetBundleName)
	{
		bool flag = AssetBundleManager.m_AssetBundleManifest == null;
		if (flag)
		{
			Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
		}
		else
		{
			string[] allDependencies = AssetBundleManager.m_AssetBundleManifest.GetAllDependencies(assetBundleName);
			bool flag2 = allDependencies.Length != 0;
			if (flag2)
			{
				for (int i = 0; i < allDependencies.Length; i++)
				{
					allDependencies[i] = AssetBundleManager.RemapVariantName(allDependencies[i]);
				}
				AssetBundleManager.m_Dependencies.Add(assetBundleName, allDependencies);
				for (int j = 0; j < allDependencies.Length; j++)
				{
					AssetBundleManager.LoadAssetBundleInternal(allDependencies[j], false);
				}
			}
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000084A1 File Offset: 0x000066A1
	public static void UnloadAssetBundle(string assetBundleName)
	{
		AssetBundleManager.UnloadAssetBundleInternal(assetBundleName);
		AssetBundleManager.UnloadDependencies(assetBundleName);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000084B4 File Offset: 0x000066B4
	protected static void UnloadDependencies(string assetBundleName)
	{
		string[] array = null;
		bool flag = AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array);
		if (flag)
		{
			string[] array2 = array;
			foreach (string assetBundleName2 in array2)
			{
				AssetBundleManager.UnloadAssetBundleInternal(assetBundleName2);
			}
			AssetBundleManager.m_Dependencies.Remove(assetBundleName);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000850C File Offset: 0x0000670C
	protected static void UnloadAssetBundleInternal(string assetBundleName)
	{
		string text;
		LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text);
		bool flag;
		if (loadedAssetBundle != null)
		{
			LoadedAssetBundle loadedAssetBundle2 = loadedAssetBundle;
			int num = loadedAssetBundle2.m_ReferencedCount - 1;
			loadedAssetBundle2.m_ReferencedCount = num;
			flag = (num == 0);
		}
		else
		{
			flag = false;
		}
		bool flag2 = flag;
		if (flag2)
		{
			loadedAssetBundle.m_AssetBundle.Unload(false);
			AssetBundleManager.m_LoadedAssetBundles.Remove(assetBundleName);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000855C File Offset: 0x0000675C
	private void Update()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, WWW> keyValuePair in AssetBundleManager.m_DownloadingWWWs)
		{
			WWW value = keyValuePair.Value;
			bool flag = value.error != null;
			if (flag)
			{
				AssetBundleManager.m_DownloadingErrors.Add(keyValuePair.Key, value.error);
				list.Add(keyValuePair.Key);
			}
			else
			{
				bool isDone = value.isDone;
				if (isDone)
				{
					Debug.Log("Downloading " + keyValuePair.Key + " is done at frame " + Time.frameCount.ToString());
					AssetBundleManager.m_LoadedAssetBundles.Add(keyValuePair.Key, new LoadedAssetBundle(value, value.assetBundle));
					list.Add(keyValuePair.Key);
				}
			}
		}
		foreach (string key in list)
		{
			WWW www = AssetBundleManager.m_DownloadingWWWs[key];
			AssetBundleManager.m_DownloadingWWWs.Remove(key);
			www.Dispose();
		}
		int i = 0;
		while (i < AssetBundleManager.m_InProgressOperations.Count)
		{
			bool flag2 = !AssetBundleManager.m_InProgressOperations[i].Update();
			if (flag2)
			{
				AssetBundleManager.m_InProgressOperations.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000870C File Offset: 0x0000690C
	public static AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, Type type)
	{
		AssetBundleManager.LoadAssetBundle(assetBundleName, false);
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);
		AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadAssetOperation);
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00008740 File Offset: 0x00006940
	public static AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive)
	{
		AssetBundleManager.LoadAssetBundle(assetBundleName, false);
		AssetBundleLoadOperation assetBundleLoadOperation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAdditive);
		AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadOperation);
		return assetBundleLoadOperation;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008774 File Offset: 0x00006974
	public static AssetBundleLoadWWWOperation LoadAssetWWW(string assetBundleName)
	{
		AssetBundleManager.LoadAssetBundle(assetBundleName, false);
		AssetBundleLoadWWWOperation assetBundleLoadWWWOperation = new AssetBundleLoadWWWOperation(assetBundleName);
		AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadWWWOperation);
		return assetBundleLoadWWWOperation;
	}

	// Token: 0x040000F6 RID: 246
	private static string m_BaseDownloadingURL = string.Empty;

	// Token: 0x040000F7 RID: 247
	private static string[] m_Variants = new string[0];

	// Token: 0x040000F8 RID: 248
	private static AssetBundleManifest m_AssetBundleManifest = null;

	// Token: 0x040000F9 RID: 249
	private static Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();

	// Token: 0x040000FA RID: 250
	private static Dictionary<string, WWW> m_DownloadingWWWs = new Dictionary<string, WWW>();

	// Token: 0x040000FB RID: 251
	private static Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();

	// Token: 0x040000FC RID: 252
	private static List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();

	// Token: 0x040000FD RID: 253
	private static Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200021F RID: 543
public class LoadingScene : MonoBehaviour
{
	// Token: 0x06001883 RID: 6275 RVA: 0x000E5B70 File Offset: 0x000E3D70
	private void Start()
	{
		string url = string.Empty;
		bool flag = fGameManager.GetInstance().mloadName == string.Empty;
		if (flag)
		{
			Debug.Log("重新刷新客户端---");
		}
		else
		{
			fGameManager.GetInstance();
			fUIManager.GetInstance().ProgressChange(true, "LoadingScene", 0f, 0.66f);
			fUIManager.mUILoading.SetRawImg(fGameManager.GetInstance().nNowScnID.ToString(), fGameManager.GetInstance().mloadName);
			url = "scene/" + fGameManager.GetInstance().mloadName + ".map";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset("scene/" + fGameManager.GetInstance().mloadName + ".map", new fResourceManager.DownFinishDelegate(this.MapLoadOK), fGameManager.GetInstance().mloadName);
			}
			else
			{
				this.MapLoadOK(resWWW, fGameManager.GetInstance().mloadName);
			}
		}
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x000E5C68 File Offset: 0x000E3E68
	private void MapLoadOK(WWW wwwobj, object customParam)
	{
		bool flag = wwwobj != null;
		if (flag)
		{
			AssetBundle assetBundle = wwwobj.assetBundle;
		}
		SceneManager.LoadScene(fGameManager.GetInstance().mloadName);
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x000E5C97 File Offset: 0x000E3E97
	private void OnDestroy()
	{
		fUIManager.GetInstance().ProgressChange(true, "LoadingScene", 1f, 0.66f);
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x000E5CB5 File Offset: 0x000E3EB5
	private void ChangeSceneActive(bool bActive)
	{
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000E5CB8 File Offset: 0x000E3EB8
	private IEnumerator loadScene()
	{
		this.async = SceneManager.LoadSceneAsync(fGameManager.GetInstance().mloadName);
		yield return this.async;
		yield break;
	}

	// Token: 0x04001348 RID: 4936
	private AsyncOperation async;

	// Token: 0x04001349 RID: 4937
	private IEnumerator downienobj;

	// Token: 0x0400134A RID: 4938
	public int m_nDownCount;

	// Token: 0x0400134B RID: 4939
	public long m_nCheckTime;
}

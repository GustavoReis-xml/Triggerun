using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class CReadModel : MonoBehaviour
{
	// Token: 0x1700014E RID: 334
	// (get) Token: 0x0600072D RID: 1837 RVA: 0x0003F578 File Offset: 0x0003D778
	// (set) Token: 0x0600072E RID: 1838 RVA: 0x0003F58F File Offset: 0x0003D78F
	public static CReadModel Instance
	{
		get
		{
			return CReadModel._instance;
		}
		set
		{
			CReadModel._instance = value;
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0003F598 File Offset: 0x0003D798
	private void Start()
	{
		CReadModel._instance = this;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
	public void StartLoadVideoModel()
	{
		this.isLoaded = false;
		this.beginTime = Time.realtimeSinceStartup;
		this.beginRead = true;
		this.lastLoadModelTime = 0f;
		this.progressIng = 0f;
		List<string> list = Fun.DeepCopy<List<string>>(HeroDataManager.GetInstance().GetAllHeroModel(0));
		List<string> collection = Fun.DeepCopy<List<string>>(HeroDataManager.GetInstance().GetAllHeroModel(1));
		this.heros = list;
		this.heros.AddRange(collection);
		this.allMCount = this.heros.Count;
		this.timeOut = Time.realtimeSinceStartup + 20f;
		this.loadAllCount = 0;
		this.loadingCount = 0;
		this.heroObjects = new List<WWW>();
		for (int i = 0; i < this.allMCount; i++)
		{
			string str = this.heros[i];
			string text = "HeroAnimator/" + str + ".mo";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag = resWWW != null && resWWW.error == null;
			if (flag)
			{
				this.loadingCount++;
				this.LoadOtherPlayerLoadUrl(resWWW, text);
			}
		}
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
	private void Update()
	{
		bool flag = !this.beginRead;
		if (!flag)
		{
			bool flag2 = fGameManager.GetInstance().nNowScnID == 0;
			if (flag2)
			{
				Debug.Log("Loading Other Models cancel , hall : " + fGameManager.GetInstance().nNowScnID.ToString());
				this.finishRead();
			}
			else
			{
				bool flag3 = Time.realtimeSinceStartup > this.timeOut;
				if (flag3)
				{
					Debug.Log("Loading Other Models cancel ,timeOut .");
					this.finishRead();
				}
				else
				{
					bool flag4 = this.heroObjects.Count == 0;
					if (flag4)
					{
						Debug.Log("Loading Other Models cancel ,heroObjects.Count is zero .");
						this.finishRead();
					}
					else
					{
						WWW www = this.heroObjects[0];
						this.heroObjects.RemoveAt(0);
						this.loadAllCount++;
						bool flag5 = www != null;
						if (flag5)
						{
							this.progressIng = (float)this.loadAllCount / (float)this.loadingCount;
							fUIManager.mUILoading.setTuiSongValue(this.progressIng);
							GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
							gameObject.name = "model_other_mo";
							gameObject.transform.localPosition = -Vector3.one * 1000f;
							UnityEngine.Object.DestroyImmediate(gameObject);
							bool flag6 = this.loadAllCount >= this.loadingCount;
							if (flag6)
							{
								this.finishRead();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0003F852 File Offset: 0x0003DA52
	private void LoadOtherPlayerLoadUrl(WWW www, object obj)
	{
		this.heroObjects.Add(www);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0003F864 File Offset: 0x0003DA64
	private void finishRead()
	{
		this.beginRead = false;
		this.isLoaded = true;
		bool flag = fGameManager.GetInstance().nNowScnID > 0;
		if (flag)
		{
			MatchManager.GetInstance().m_nJoinState = 6;
			MatchManager.GetInstance().UserJoinFbLog(MatchManager.GetInstance().m_nJoinState, "推显卡完成");
			GameRoot.SceneSocket.unFsorket.LoadMapOKMsg();
		}
		this.lastLoadModelTime = Time.realtimeSinceStartup - this.beginTime;
		string message = string.Format("Loading Other Models Finish, c : {0}/{1}/{2} , t : {3} ...", new object[]
		{
			this.loadAllCount,
			this.loadingCount,
			this.allMCount,
			this.lastLoadModelTime.ToString("F1")
		});
		Debug.Log(message);
	}

	// Token: 0x04000533 RID: 1331
	public bool isLoaded;

	// Token: 0x04000534 RID: 1332
	private int allMCount;

	// Token: 0x04000535 RID: 1333
	private int loadingCount;

	// Token: 0x04000536 RID: 1334
	private int loadAllCount;

	// Token: 0x04000537 RID: 1335
	private float progressIng;

	// Token: 0x04000538 RID: 1336
	private static CReadModel _instance;

	// Token: 0x04000539 RID: 1337
	private bool beginRead;

	// Token: 0x0400053A RID: 1338
	private float timeOut;

	// Token: 0x0400053B RID: 1339
	private const float timeOutMax = 20f;

	// Token: 0x0400053C RID: 1340
	private List<string> heros = new List<string>();

	// Token: 0x0400053D RID: 1341
	private List<WWW> heroObjects = new List<WWW>();

	// Token: 0x0400053E RID: 1342
	private float beginTime;

	// Token: 0x0400053F RID: 1343
	public float lastLoadModelTime;
}

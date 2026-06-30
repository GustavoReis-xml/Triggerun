using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Class;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class fResourceManager : MonoBehaviour
{
	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0006B420 File Offset: 0x00069620
	// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0006B438 File Offset: 0x00069638
	public string WebHead
	{
		get
		{
			return this.m_WebHead;
		}
		set
		{
			this.m_WebHead = value;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0006B444 File Offset: 0x00069644
	// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0006B45C File Offset: 0x0006965C
	public string WebUrl
	{
		get
		{
			return this.m_WebUrl;
		}
		set
		{
			this.m_WebUrl = value;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0006B468 File Offset: 0x00069668
	// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0006B480 File Offset: 0x00069680
	public string BackupWebUrl
	{
		get
		{
			return this.m_BackupWebUrl;
		}
		set
		{
			this.m_BackupWebUrl = value;
		}
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0006B48C File Offset: 0x0006968C
	private void Awake()
	{
		fResourceManager.s_locpath = Application.persistentDataPath + "/GameCache/";
		bool flag = fResourceManager.s_locpath != string.Empty && !Directory.Exists(fResourceManager.s_locpath);
		if (flag)
		{
			Directory.CreateDirectory(fResourceManager.s_locpath);
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0006B4E0 File Offset: 0x000696E0
	public float wwwp(string sName)
	{
		bool flag = this.m_Req.ContainsKey(sName) && this.m_Req[sName].www != null;
		float result;
		if (flag)
		{
			result = this.m_Req[sName].www.progress;
		}
		else
		{
			result = 1f;
		}
		return result;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0006B53C File Offset: 0x0006973C
	public string wwwpurl(string sName)
	{
		bool flag = this.m_Req.ContainsKey(sName);
		string result;
		if (flag)
		{
			result = this.m_Req[sName].sFileName;
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0006B578 File Offset: 0x00069778
	public WWW GetResWWW(string url)
	{
		url = this.m_WebHead + url;
		bool flag = this.m_Dict.ContainsKey(url);
		WWW result;
		if (flag)
		{
			result = this.m_Dict[url];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x0006B5BC File Offset: 0x000697BC
	public bool GetContainsKey(string url)
	{
		url = this.m_WebHead + url;
		bool flag = this.m_Dict.ContainsKey(url);
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = this.m_Req.ContainsKey(url);
			result = flag2;
		}
		return result;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0006B608 File Offset: 0x00069808
	public void AddDic(string sFileName, WWW www)
	{
		sFileName = this.m_WebHead + sFileName;
		bool flag = !this.m_Dict.ContainsKey(sFileName);
		if (flag)
		{
			this.m_Dict.Add(sFileName, www);
		}
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x0006B647 File Offset: 0x00069847
	public void AddDownRequest(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam, AssetBundleManifest AssetBundle)
	{
		this.LoadDependencies(url, callBackFun, customParam, AssetBundle);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0006B658 File Offset: 0x00069858
	public void RequestWithDesVesion(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam, int vers)
	{
		string sVersion = vers.ToString();
		this.LoadDownRequest(url, callBackFun, customParam, sVersion, url);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0006B67C File Offset: 0x0006987C
	public bool ContainsKey(string key)
	{
		bool flag = this.m_Dict.ContainsKey(key);
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = this.m_Req.ContainsKey(key);
			result = flag2;
		}
		return result;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0006B6BC File Offset: 0x000698BC
	public void LoadXmlRequest(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam, string sLocalFile)
	{
		string sVersion = UnityEngine.Random.Range(1000000, 100000000).ToString();
		this.LoadDownRequest(url, callBackFun, customParam, sVersion, sLocalFile);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0006B6F0 File Offset: 0x000698F0
	public void LoadDownRequest(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam, string sLocalFile)
	{
		url = SysConfig.GetFileNameByGrade(url, -3);
		string fileVersion = SysConfig.GetFileVersion(this.m_WebHead + url);
		this.LoadDownRequest(url, callBackFun, customParam, fileVersion, sLocalFile);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0006B728 File Offset: 0x00069928
	public void LoadDownRequest(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam, string sVersion, string sLocalFile)
	{
		bool flag = !(sLocalFile != string.Empty);
		if (!flag)
		{
			sLocalFile = this.m_WebHead + sLocalFile;
			bool flag2 = this.m_Dict.ContainsKey(sLocalFile);
			if (flag2)
			{
				callBackFun(this.m_Dict[sLocalFile], customParam);
			}
			else
			{
				bool flag3 = this.m_Req.ContainsKey(sLocalFile);
				if (flag3)
				{
					fResourceManager.WWWRequest wwwrequest = this.m_Req[sLocalFile];
					wwwrequest.calbackFun = (fResourceManager.DownFinishDelegate)Delegate.Combine(wwwrequest.calbackFun, callBackFun);
					this.m_Req[sLocalFile].customParams.Add(customParam);
					this.m_Req[sLocalFile].CallFunNun++;
				}
				else
				{
					fResourceManager.WWWRequest wwwrequest2 = new fResourceManager.WWWRequest(url, callBackFun, customParam, sVersion, sLocalFile);
					this.m_Req.Add(sLocalFile, wwwrequest2);
					bool flag4 = this.rescLoadNum >= this.m_nMaxDownNum;
					if (flag4)
					{
						this.m_Wait.Add(wwwrequest2);
					}
					else
					{
						base.StartCoroutine(wwwrequest2.curien = this.downloadNew(wwwrequest2));
					}
				}
			}
		}
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0006B858 File Offset: 0x00069A58
	private void CheckOverRes()
	{
		bool flag = (this.m_Req != null && this.m_Req.Count <= 0) || (Environment.TickCount - this.m_nCheckTime) / 1000 <= 10;
		if (!flag)
		{
			this.m_nCheckTime = Environment.TickCount;
			foreach (fResourceManager.WWWRequest wwwrequest in this.m_Req.Values)
			{
				bool flag2 = ((long)Environment.TickCount - wwwrequest.m_nDownTime) / 1000L >= 10L;
				if (flag2)
				{
					bool flag3 = wwwrequest.www != null;
					if (flag3)
					{
						bool flag4 = (wwwrequest.www.progress == wwwrequest.oldprogress || wwwrequest.www.error != null) && !this.m_ReDownBuff.ContainsKey(wwwrequest.sFileName);
						if (flag4)
						{
							this.m_ReDownBuff.Add(wwwrequest.sFileName, wwwrequest);
						}
						wwwrequest.oldprogress = wwwrequest.www.progress;
					}
				}
				else
				{
					bool flag5 = wwwrequest.www != null;
					if (flag5)
					{
						wwwrequest.oldprogress = wwwrequest.www.progress;
					}
				}
			}
			this.RedownFun();
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0006B9BC File Offset: 0x00069BBC
	public void OutputRequest()
	{
		Fun.Log(string.Format("OutputRequest------------------------------start----------------------------{0}", Environment.TickCount), null);
		Fun.Log(string.Format("OutputRequest param:{0},{1},{2}", this.m_nMaxDownNum, this.rescLoadNum, this.m_Wait.Count), null);
		foreach (fResourceManager.WWWRequest wwwrequest in this.m_Req.Values)
		{
			bool flag = wwwrequest.www != null;
			if (flag)
			{
				Fun.Log(string.Format("OutputRequest---{0},{1},{2}", Environment.TickCount, wwwrequest.requestURL, wwwrequest.www.error), null);
			}
			else
			{
				Fun.Log(string.Format("OutputRequest---{0},{1}", Environment.TickCount, wwwrequest.requestURL), null);
			}
		}
		Fun.Log(string.Format("OutputRequest------------------------------ended----------------------------{0}", Environment.TickCount), null);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0006BAE0 File Offset: 0x00069CE0
	private IEnumerator Call(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam)
	{
		yield return 1;
		bool flag = this.m_Dict.ContainsKey(url);
		if (flag)
		{
			callBackFun(this.m_Dict[url], customParam);
		}
		yield break;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0006BB04 File Offset: 0x00069D04
	private void LoadDependencies(string assetBundleName, fResourceManager.DownFinishDelegate callBackFun, object customParam, AssetBundleManifest AssetBundle)
	{
		bool flag = AssetBundle == null;
		if (flag)
		{
			Debug.LogError("Please initialize AssetBundleManifest:" + assetBundleName);
		}
		else
		{
			string[] allDependencies = AssetBundle.GetAllDependencies(assetBundleName);
			bool flag2 = allDependencies.Length == 0;
			if (flag2)
			{
				this.LoadDownRequest(assetBundleName, callBackFun, customParam, assetBundleName);
			}
			else
			{
				DependenciesData dependenciesData = new DependenciesData();
				dependenciesData.iNowDown = 0;
				dependenciesData.iAllDown = allDependencies.Length;
				dependenciesData.assetBundleName = assetBundleName;
				dependenciesData.callBackFun = callBackFun;
				dependenciesData.customParam = customParam;
				fResourceManager.m_Lependencies.Add(dependenciesData);
				for (int i = 0; i < allDependencies.Length; i++)
				{
					this.LoadDownRequest(allDependencies[i], new fResourceManager.DownFinishDelegate(this.DependCallFun), dependenciesData, allDependencies[i]);
				}
			}
		}
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0006BBC4 File Offset: 0x00069DC4
	private void DependCallFun(WWW wobj, object customParam)
	{
		bool flag = wobj.isDone && wobj != null && wobj.assetBundle != null;
		if (flag)
		{
			wobj.assetBundle.LoadAllAssets();
		}
		DependenciesData dependenciesData = (DependenciesData)customParam;
		int num = dependenciesData.iNowDown;
		int iAllDown = dependenciesData.iAllDown;
		num++;
		bool flag2 = num >= iAllDown;
		if (flag2)
		{
			fResourceManager.m_Lependencies.Remove(dependenciesData);
			fResourceManager.DownFinishDelegate callBackFun = dependenciesData.callBackFun;
			object customParam2 = dependenciesData.customParam;
			this.LoadDownRequest(dependenciesData.assetBundleName, callBackFun, customParam2, dependenciesData.assetBundleName);
		}
		else
		{
			dependenciesData.iNowDown = num;
		}
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0006BC64 File Offset: 0x00069E64
	private IEnumerator downloadNew(fResourceManager.WWWRequest wwwReq)
	{
		wwwReq.StartTime = DateTime.Now;
		fGameManager.m_nIsLoad++;
		bool isimg = false;
		bool issave = false;
		string sfilepath = string.Empty;
		string verpath = string.Empty;
		string sVersion = this.WebUrl;
		bool flag = Application.platform == RuntimePlatform.WindowsPlayer && !WebParam.client_resurl_force_local;
		if (flag)
		{
			sVersion = sVersion.Replace(SysConfig.StreamURL, SysConfig.GetFilePath(wwwReq.sFileName));
		}
		string url = sVersion + wwwReq.requestURL;
		string sFileName = wwwReq.sFileName;
		fGameManager.sAssName = url;
		this.rescLoadNum++;
		wwwReq.m_nDownCount++;
		wwwReq.m_nDownTime = (long)Environment.TickCount;
		bool bExitFlag = false;
		int nVersion = 100;
		int.TryParse(wwwReq.sVersion, out nVersion);
		bool flag2 = GameRoot.mPreLoadManager != null;
		if (flag2)
		{
			GameRoot.mPreLoadManager.OnCheckPreLoad(sFileName);
		}
		bool client_resurl_force_local = WebParam.client_resurl_force_local;
		WWW www;
		if (client_resurl_force_local)
		{
			www = new WWW(url + "?v=" + wwwReq.sVersion);
		}
		else
		{
			bool flag3 = Application.platform == RuntimePlatform.WindowsPlayer;
			if (flag3)
			{
				bool flag4 = url.Contains(".txt") || url.Contains(".xml");
				if (flag4)
				{
					bool flag5 = fResourceManager.s_locpath != string.Empty && Application.platform == RuntimePlatform.WindowsPlayer;
					if (flag5)
					{
						isimg = true;
						sfilepath = url;
						verpath = fResourceManager.s_locpath + url.GetHashCode().ToString() + "_" + wwwReq.sVersion;
					}
					bool flag6 = isimg;
					if (flag6)
					{
						bool flag7 = File.Exists(verpath);
						if (flag7)
						{
							url = "file:///" + verpath;
							issave = false;
						}
						else
						{
							issave = true;
						}
					}
					www = new WWW(url + "?v=" + wwwReq.sVersion);
				}
				else
				{
					www = WWW.LoadFromCacheOrDownload(url + "?v=" + nVersion.ToString(), nVersion);
				}
			}
			else
			{
				www = new WWW(url + "?v=" + wwwReq.sVersion);
			}
		}
		wwwReq.www = www;
		wwwReq.oldprogress = www.progress;
		wwwReq.sFileName = sFileName;
		yield return www;
		fGameManager.m_nIsLoad--;
		wwwReq.EndTime = DateTime.Now;
		this.rescLoadNum--;
		bool flag8 = www.error == null;
		if (flag8)
		{
			bool flag9 = !this.m_Dict.ContainsKey(sFileName);
			if (flag9)
			{
				this.m_Dict.Add(sFileName, www);
			}
			bool flag10 = isimg && issave;
			if (flag10)
			{
				string[] files = Directory.GetFiles(fResourceManager.s_locpath, sfilepath.GetHashCode().ToString() + "_*");
				string[] array = files;
				foreach (string text in array)
				{
					Debug.Log("filedir:" + text + " -----");
					File.Delete(text);
}
				string[] array2 = null;
				FileStream fileStream = File.Create(verpath);
				fileStream.Write(www.bytes, 0, www.bytes.Length);
				fileStream.Close();
				files = null;
				array = null;
				fileStream = null;
			}
		}
		else
		{
			Debug.Log(string.Format("redownload:---{0},{1},{2},m_nDownCount={3}", new object[]
			{
				wwwReq.m_nDownTime,
				url,
				wwwReq.www.error,
				wwwReq.m_nDownCount
			}));
			bool flag11 = !wwwReq.requestURL.Contains("files_dir.xml");
			if (flag11)
			{
				Debuger.Out("Res not found.url=" + url);
				bool flag12 = wwwReq.m_nDownCount < 5;
				if (flag12)
				{
					bExitFlag = true;
					bool flag13 = !this.m_ReDownBuff.ContainsKey(wwwReq.sFileName);
					if (flag13)
					{
						this.m_ReDownBuff.Add(wwwReq.sFileName, wwwReq);
					}
					yield break;
				}
				this.SendDownLoadErrorLog(3, "1," + wwwReq.requestURL + "," + www.error.Trim());
				bool flag14 = this.m_Req.ContainsKey(wwwReq.sFileName);
				if (flag14)
				{
					this.m_Req.Remove(wwwReq.sFileName);
				}
			}
			else
			{
				bool flag15 = Application.platform == RuntimePlatform.WindowsPlayer;
				if (flag15)
				{
					this.SendDownLoadErrorLog(13, "2," + wwwReq.requestURL + "," + www.error.Trim());
				}
			}
		}
		bool flag16 = !bExitFlag;
		if (flag16)
		{
			bool flag17 = this.m_Req.ContainsKey(sFileName);
			if (flag17)
			{
				this.m_Req.Remove(sFileName);
				int num;
				for (int i = 0; i < wwwReq.calbackFun.GetInvocationList().GetLength(0); i = num + 1)
				{
					((fResourceManager.DownFinishDelegate)wwwReq.calbackFun.GetInvocationList()[i])(www, wwwReq.customParams[i]);
					num = i;
				}
				wwwReq = null;
				bool flag18 = !GameRoot.IsClose;
				if (flag18)
				{
					this.SubDownNum();
				}
			}
			else
			{
				Debuger.Out("m_Req not containskey:" + sFileName);
			}
		}
		bool flag19 = !WebParam.client_resurl_force_local && GameRoot.mPreLoadManager != null && fGameManager.m_nIsLoad <= 0 && !fGameManager.m_nPreLoadOff;
		if (flag19)
		{
			GameRoot.mPreLoadManager.StartPreLoad();
		}
		bool flag20 = GameRoot.IsClose && fGameManager.m_nIsLoad <= 0;
		if (flag20)
		{
			bool flag21 = fGameManager.LobbyPlayer != null;
			if (flag21)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
				{
					2,
					0
				});
			}
			bool flag22 = fGameManager.Heroplayer != null;
			if (flag22)
			{
				fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
				{
					2,
					0
				});
			}
			Application.Quit();
		}
		yield break;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0006BC7A File Offset: 0x00069E7A
	private void SubDownNum()
	{
		this.StartWaitList();
		this.m_WaitTick = Environment.TickCount;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0006BC90 File Offset: 0x00069E90
	private void StartWaitList()
	{
		while (this.m_Wait.Count > 0 && this.rescLoadNum < this.m_nMaxDownNum)
		{
			fResourceManager.WWWRequest wwwrequest = this.m_Wait[0];
			base.StartCoroutine(wwwrequest.curien = this.downloadNew(wwwrequest));
			this.m_Wait.RemoveAt(0);
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0006BCF6 File Offset: 0x00069EF6
	private void SendDownLoadErrorLog(int nType, string sUrl)
	{
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0006BCFC File Offset: 0x00069EFC
	public void GC()
	{
		foreach (WWW www in this.m_Dict.Values)
		{
			bool flag = www == null || www.error != null;
			if (!flag)
			{
				bool flag2 = www.assetBundle == null;
				if (flag2)
				{
					bool flag3 = www.url.Contains(".assetbundle");
					if (flag3)
					{
						Debug.Log("www.assetBundle is null " + www.url);
					}
				}
				else
				{
					string value = SysConfig.streamPath + "/scene";
					bool flag4 = !www.url.Contains(value);
					if (flag4)
					{
						www.assetBundle.Unload(false);
						www.Dispose();
					}
				}
			}
		}
		this.m_Dict.Clear();
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0006BDF8 File Offset: 0x00069FF8
	private void Start()
	{
		this.m_nCheckTime = Environment.TickCount;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0006BE08 File Offset: 0x0006A008
	private void Update()
	{
		bool keyDown = Input.GetKeyDown(KeyCode.F4);
		if (keyDown)
		{
			this.OutputRequest();
		}
		bool flag = Environment.TickCount - this.m_WaitTick > 1000;
		if (flag)
		{
			this.m_WaitTick = Environment.TickCount;
			this.StartWaitList();
		}
		this.CheckOverRes();
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0006BE60 File Offset: 0x0006A060
	private void RedownFun()
	{
		bool flag = this.m_ReDownBuff.Count > 0;
		if (flag)
		{
			foreach (fResourceManager.WWWRequest wwwrequest in this.m_ReDownBuff.Values)
			{
				this.m_ReDown.Add(wwwrequest.requestURL, wwwrequest);
			}
			this.m_ReDownBuff.Clear();
		}
		bool flag2 = this.m_ReDown.Count <= 0;
		if (!flag2)
		{
			foreach (fResourceManager.WWWRequest wwwrequest2 in this.m_ReDown.Values)
			{
				bool flag3 = wwwrequest2.www != null;
				if (flag3)
				{
					Debug.Log(string.Format("redownload:---{0},{1},{2},progress={3}", new object[]
					{
						wwwrequest2.m_nDownTime,
						wwwrequest2.requestURL,
						wwwrequest2.www.error,
						wwwrequest2.www.progress
					}));
				}
				else
				{
					Debug.Log(string.Format("redownload:---{0},{1}", wwwrequest2.m_nDownTime, wwwrequest2.requestURL));
				}
				this.rescLoadNum--;
				bool flag4 = this.redownnum >= 5;
				if (flag4)
				{
					break;
				}
				base.StopCoroutine(wwwrequest2.curien);
				wwwrequest2.www.Dispose();
				base.StartCoroutine(wwwrequest2.curien = this.downloadNew(wwwrequest2));
				this.redownnum++;
			}
			this.redownnum = 0;
			this.m_ReDown.Clear();
		}
	}

	// Token: 0x040009E1 RID: 2529
	public AssetBundleManifest m_AssetBundleManifest;

	// Token: 0x040009E2 RID: 2530
	public AssetBundleManifest m_AssetBundleManifestEffect;

	// Token: 0x040009E3 RID: 2531
	public AssetBundleManifest m_AssetBundleManifestSound;

	// Token: 0x040009E4 RID: 2532
	private static List<DependenciesData> m_Lependencies = new List<DependenciesData>();

	// Token: 0x040009E5 RID: 2533
	private Dictionary<string, fResourceManager.WWWRequest> m_Req = new Dictionary<string, fResourceManager.WWWRequest>();

	// Token: 0x040009E6 RID: 2534
	private int m_WaitTick = Environment.TickCount;

	// Token: 0x040009E7 RID: 2535
	private List<fResourceManager.WWWRequest> m_Wait = new List<fResourceManager.WWWRequest>();

	// Token: 0x040009E8 RID: 2536
	private Dictionary<string, WWW> m_Dict = new Dictionary<string, WWW>();

	// Token: 0x040009E9 RID: 2537
	public int rescLoadNum;

	// Token: 0x040009EA RID: 2538
	public int redownnum;

	// Token: 0x040009EB RID: 2539
	public int m_nMaxDownNum = 1;

	// Token: 0x040009EC RID: 2540
	public int s_DownCount = 10;

	// Token: 0x040009ED RID: 2541
	private Dictionary<string, fResourceManager.WWWRequest> m_ReDown = new Dictionary<string, fResourceManager.WWWRequest>();

	// Token: 0x040009EE RID: 2542
	private Dictionary<string, fResourceManager.WWWRequest> m_ReDownBuff = new Dictionary<string, fResourceManager.WWWRequest>();

	// Token: 0x040009EF RID: 2543
	private string m_WebUrl = string.Empty;

	// Token: 0x040009F0 RID: 2544
	private string m_BackupWebUrl = string.Empty;

	// Token: 0x040009F1 RID: 2545
	private string m_WebHead = string.Empty;

	// Token: 0x040009F2 RID: 2546
	public static string s_locpath = string.Empty;

	// Token: 0x040009F3 RID: 2547
	private int m_nCheckTime;

	// Token: 0x02000563 RID: 1379
	// (Invoke) Token: 0x060033EB RID: 13291
	public delegate void DownFinishDelegate(WWW wwwObj, object customParam);

	// Token: 0x02000564 RID: 1380
	private class WWWRequest
	{
		// Token: 0x060033EE RID: 13294 RVA: 0x001BB86C File Offset: 0x001B9A6C
		public WWWRequest(string url, fResourceManager.DownFinishDelegate cbFun, object customParam, string swwwVersion, string localfile)
		{
			this.sName = PlayerDataManager.GetSetName();
			this.JoinTime = DateTime.Now;
			this.requestURL = url;
			this.sVersion = swwwVersion;
			this.calbackFun = new fResourceManager.DownFinishDelegate(cbFun.Invoke);
			this.customParams.Add(customParam);
			this.sFileName = localfile;
			this.m_nDownCount = 0;
			this.m_nbytesDownloaded = 0;
			this.isBakUrl = false;
		}

		// Token: 0x04002CB8 RID: 11448
		public string requestURL;

		// Token: 0x04002CB9 RID: 11449
		public string sVersion;

		// Token: 0x04002CBA RID: 11450
		public fResourceManager.DownFinishDelegate calbackFun;

		// Token: 0x04002CBB RID: 11451
		public List<object> customParams = new List<object>();

		// Token: 0x04002CBC RID: 11452
		public string sFileName;

		// Token: 0x04002CBD RID: 11453
		public int m_nDownCount;

		// Token: 0x04002CBE RID: 11454
		public long m_nDownTime;

		// Token: 0x04002CBF RID: 11455
		public int m_nbytesDownloaded;

		// Token: 0x04002CC0 RID: 11456
		public WWW www;

		// Token: 0x04002CC1 RID: 11457
		public bool isBakUrl;

		// Token: 0x04002CC2 RID: 11458
		public float oldprogress;

		// Token: 0x04002CC3 RID: 11459
		public IEnumerator curien;

		// Token: 0x04002CC4 RID: 11460
		public DateTime JoinTime;

		// Token: 0x04002CC5 RID: 11461
		public DateTime StartTime;

		// Token: 0x04002CC6 RID: 11462
		public DateTime EndTime;

		// Token: 0x04002CC7 RID: 11463
		public string sName = "null";

		// Token: 0x04002CC8 RID: 11464
		public int CallFunNun;
	}
}

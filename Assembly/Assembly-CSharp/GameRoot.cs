using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class GameRoot : MonoBehaviour
{
	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0007CB04 File Offset: 0x0007AD04
	public Setting_Entity Setting
	{
		get
		{
			bool flag = this._Setting == null;
			if (flag)
			{
				this._Setting = base.gameObject.GetComponent<Setting_Entity>();
				bool flag2 = this._Setting == null;
				if (flag2)
				{
					this._Setting = base.gameObject.AddComponent<Setting_Entity>();
				}
			}
			return this._Setting;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0007CB64 File Offset: 0x0007AD64
	public Setting_FPS Setting_FPS
	{
		get
		{
			bool flag = this._Setting_FPS == null;
			if (flag)
			{
				this._Setting_FPS = base.gameObject.GetComponent<Setting_FPS>();
				bool flag2 = this._Setting_FPS == null;
				if (flag2)
				{
					this._Setting_FPS = base.gameObject.AddComponent<Setting_FPS>();
				}
			}
			return this._Setting_FPS;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0007CBC4 File Offset: 0x0007ADC4
	// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0007CBDC File Offset: 0x0007ADDC
	public Process_Detector Process_Detector_Component
	{
		get
		{
			return this._Process_Detector_Component;
		}
		set
		{
			this._Process_Detector_Component = value;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0007CBE8 File Offset: 0x0007ADE8
	// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0007CC00 File Offset: 0x0007AE00
	public FPS Fps_Component
	{
		get
		{
			return this._Fps_Component;
		}
		set
		{
			this._Fps_Component = value;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0007CC0C File Offset: 0x0007AE0C
	// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0007CC23 File Offset: 0x0007AE23
	public static UserSocket LoginSocket
	{
		get
		{
			return GameRoot.m_pLogin;
		}
		set
		{
			GameRoot.m_pLogin = value;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0007CC2C File Offset: 0x0007AE2C
	// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0007CC43 File Offset: 0x0007AE43
	public static UserSocket HallSocket
	{
		get
		{
			return GameRoot.m_pHall;
		}
		set
		{
			GameRoot.m_pHall = value;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0007CC4C File Offset: 0x0007AE4C
	// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0007CC63 File Offset: 0x0007AE63
	public static UserSocket SceneSocket
	{
		get
		{
			return GameRoot.m_pScene;
		}
		set
		{
			GameRoot.m_pScene = value;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0007CC6C File Offset: 0x0007AE6C
	// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0007CC83 File Offset: 0x0007AE83
	public static UserSocket ChatSocket
	{
		get
		{
			return GameRoot.m_pChat;
		}
		set
		{
			GameRoot.m_pChat = value;
		}
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0007CC8C File Offset: 0x0007AE8C
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.mGame = fGameManager.GetInstance();
		this.mGame.SetGameRoot(base.gameObject);
		fGameManager.m_nJoinState = 2;
		this.audioObject = new GameObject("_audioListener");
		this.audioEar = this.audioObject.AddComponent<AudioListener>();
		this.audioObject.transform.SetParent(base.transform);
		this.addScripts();
		this.LoadPerRes();
		fUIManager.GetInstance();
		buffitemManager.GetInstance();
		fGameManager.mloadConfig.LoadConfig();
		TimeSpan timeSpan = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000L);
		Debug.Log(string.Format("电脑自从上次启动，系统已经运行了{0}天{1}小时{2}分{3}秒。", new object[]
		{
			(int)timeSpan.TotalDays,
			timeSpan.Hours,
			timeSpan.Minutes,
			timeSpan.Seconds
		}));
		this.Process_Detector_Component = base.gameObject.GetComponent<Process_Detector>();
		bool flag = this.Process_Detector_Component == null;
		if (flag)
		{
			this.Process_Detector_Component = base.gameObject.AddComponent<Process_Detector>();
		}
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0007CDD0 File Offset: 0x0007AFD0
	private void addScripts()
	{
		base.gameObject.AddComponent<CNetMsgManager>();
		base.gameObject.AddComponent<CEffectPools>();
		this.Fps_Component = base.gameObject.AddComponent<FPS>();
		base.gameObject.AddComponent<FunManager>();
		base.gameObject.AddComponent<InteractionManager>();
		base.gameObject.AddComponent<CMapEffectManager>();
		base.gameObject.AddComponent<CFilmManager>();
		base.gameObject.AddComponent<CCameraChange>();
		base.gameObject.AddComponent<RobotAIManager>();
		base.gameObject.AddComponent<CCOpyNameManager>();
		base.gameObject.AddComponent<CReadModel>();
		base.gameObject.AddComponent<CCheckDelay>();
		base.gameObject.AddComponent<CPlayerRexx>();
		base.gameObject.AddComponent<EmotePoseHUD>();
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0007CE80 File Offset: 0x0007B080
	public void LoadPerRes()
	{
		bool flag = GameRoot.mGlobalAssetManager == null;
		if (flag)
		{
			this.fReso = new GameObject("_fResourceManager");
			this.fReso.transform.parent = base.transform;
			GameRoot.mGlobalAssetManager = this.fReso.AddComponent<fResourceManager>();
			GameRoot.mGlobalAssetManager.WebUrl = SysConfig.StreamURL + "/";
		}
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0007CEF0 File Offset: 0x0007B0F0
	public void LoadFirOk()
	{
		this.mPerResNum++;
		GameRoot.mUIAssetManager = this.fReso.AddComponent<fResourceManager>();
		GameRoot.mUIAssetManager.WebUrl = SysConfig.StreamURL + "/UI/";
		GameRoot.mUIAssetManager.WebHead = "UI/";
		string text = "UI";
		GameRoot.mUIAssetManager.m_nMaxDownNum = 2;
		GameRoot.mUIAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "UImanifest", text);
		this.mPerResNum++;
		GameRoot.mMapSceneManager = this.fReso.AddComponent<fResourceManager>();
		GameRoot.mMapSceneManager.WebUrl = SysConfig.StreamURL + "/scene/";
		GameRoot.mMapSceneManager.WebHead = "scene/";
		text = "scene";
		GameRoot.mMapSceneManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "SceneManifest", text);
		this.mPerResNum++;
		GameRoot.mHeroAniManager = this.fReso.AddComponent<fResourceManager>();
		GameRoot.mHeroAniManager.WebUrl = SysConfig.StreamURL + "/HeroAnimator/";
		GameRoot.mHeroAniManager.WebHead = "HeroAnimator/";
		text = "HeroAnimator";
		GameRoot.mHeroAniManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "HeroAnimator", text);
		this.mPerResNum++;
		GameRoot.mEffectResManager = this.fReso.AddComponent<fResourceManager>();
		GameRoot.mEffectResManager.WebUrl = SysConfig.StreamURL + "/EffectRes/";
		GameRoot.mEffectResManager.WebHead = "EffectRes/";
		text = "EffectRes";
		GameRoot.mEffectResManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "EffectResfest", text);
		this.mPerResNum++;
		GameRoot.mSoundResManager = this.fReso.AddComponent<fResourceManager>();
		GameRoot.mSoundResManager.WebUrl = SysConfig.StreamURL + "/SoundRes/";
		GameRoot.mSoundResManager.WebHead = "SoundRes/";
		text = "SoundRes";
		GameRoot.mSoundResManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "SoundResfest", text);
		GameRoot.mPreLoadManager = this.fReso.AddComponent<PreLoadManager>();
		this.mPerResNum++;
		text = "gb2312.txt";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "gb2312", text);
		this.mPerResNum++;
		text = WebParam.client_language + "/config.xml";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "config", text);
		this.mPerResNum++;
		text = "shieldLab_" + WebParam.client_language + ".assetbundle";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "shieldLab", text);
		this.mPerResNum++;
		text = "randomlab_" + WebParam.client_language + ".xml";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "randomlab", text);
		this.mPerResNum++;
		text = "filterchat.assetbundle";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.PerResOK), "filterchat", text);
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0007D258 File Offset: 0x0007B458
	private void PerResOK(WWW www, object obj)
	{
		this.mPerResNumCur++;
		string text = obj.ToString();
		string text2 = text;
		uint num = PrivateImplementationDetails.ComputeStringHash(text2);
		if (num <= 1700845353U)
		{
			if (num <= 474770052U)
			{
				if (num != 422798039U)
				{
					if (num == 474770052U)
					{
						if (text2 == "EffectResfest")
						{
							GameRoot.mEffectResManager.m_AssetBundleManifest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
						}
					}
				}
				else if (text2 == "shieldLab")
				{
					fGameManager.GetInstance().SetUserLoadStep_log("_load_3_shieldLab");
					fGameManager.mloadConfig.doLoadShieldLab(www);
				}
			}
			else if (num != 677104150U)
			{
				if (num != 895309160U)
				{
					if (num == 1700845353U)
					{
						if (text2 == "filterchat")
						{
							fGameManager.mloadConfig.doLoadfilterchat(www);
						}
					}
				}
				else if (text2 == "shader")
				{
					www.assetBundle.LoadAllAssets();
				}
			}
			else if (text2 == "SceneManifest")
			{
				GameRoot.mMapSceneManager.m_AssetBundleManifest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
			}
		}
		else if (num <= 1805303108U)
		{
			if (num != 1758810030U)
			{
				if (num == 1805303108U)
				{
					if (text2 == "SoundResfest")
					{
						GameRoot.mSoundResManager.m_AssetBundleManifest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
					}
				}
			}
			else if (text2 == "UImanifest")
			{
				GameRoot.mUIAssetManager.m_AssetBundleManifest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
			}
		}
		else if (num != 2048532457U)
		{
			if (num != 2408629496U)
			{
				if (num == 3966040212U)
				{
					if (text2 == "gb2312")
					{
						byte[] bytes = www.bytes;
						bool flag = DBCSEncoding.mMemfs == null;
						if (flag)
						{
							DBCSEncoding.mMemfs = new MemoryStream(bytes);
						}
					}
				}
			}
			else if (text2 == "HeroAnimator")
			{
				GameRoot.mHeroAniManager.m_AssetBundleManifest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
			}
		}
		else if (text2 == "randomlab")
		{
			fGameManager.GetInstance().SetUserLoadStep_log("_load_3_randomlab");
			fGameManager.mloadConfig.doLoadRandomStr(www.text);
		}
		fUIManager.mUILoading.SetMainPro((float)this.mPerResNumCur * 1f / (float)this.mPerResNum * 1f);
		bool flag2 = this.mPerResNumCur == this.mPerResNum;
		if (flag2)
		{
			this.InitGame();
		}
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0007D56C File Offset: 0x0007B76C
	private void doLoadVersion(WWW xmlobj)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string text = xmlobj.text;
		text = Regex.Replace(text, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(text);
		XmlNodeList childNodes = xmlDocument.SelectSingleNode("/data").ChildNodes;
		Asset asset = default(Asset);
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = !SysConfig.FileVersion.ContainsKey(xmlElement.GetAttribute("path"));
			if (flag)
			{
				asset.m_sFile = xmlElement.GetAttribute("path");
				asset.m_sCrc = xmlElement.GetAttribute("crc");
				asset.m_sVersion = xmlElement.GetAttribute("vesion");
				SysConfig.FileVersion.Add(asset.m_sFile, asset);
			}
		}
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0007D678 File Offset: 0x0007B878
	public static WWW GetResWWW(string url)
	{
		bool flag = url.StartsWith("/");
		if (flag)
		{
			url = url.Substring(1, url.Length - 2);
		}
		string text = url;
		int num = text.IndexOf("/");
		WWW result = null;
		bool flag2 = num > 0;
		if (flag2)
		{
			string text2 = text.Substring(0, num);
			string a = text2;
			if (a == "file:")
			{
				Debug.LogError("GetResWWW请传入相对路径" + url);
				return null;
			}
			if (!(a == "UI"))
			{
				if (!(a == "scene"))
				{
					if (!(a == "HeroAnimator"))
					{
						if (!(a == "EffectRes"))
						{
							if (!(a == "SoundRes"))
							{
								result = GameRoot.mGlobalAssetManager.GetResWWW(url);
							}
							else
							{
								url = url.Substring(num + 1, url.Length - num - 1);
								result = GameRoot.mSoundResManager.GetResWWW(url);
							}
						}
						else
						{
							url = url.Substring(num + 1, url.Length - num - 1);
							result = GameRoot.mEffectResManager.GetResWWW(url);
						}
					}
					else
					{
						url = url.Substring(num + 1, url.Length - num - 1);
						result = GameRoot.mHeroAniManager.GetResWWW(url);
					}
				}
				else
				{
					url = url.Substring(num + 1, url.Length - num - 1);
					result = GameRoot.mMapSceneManager.GetResWWW(url);
				}
			}
			else
			{
				url = url.Substring(num + 1, url.Length - num - 1);
				result = GameRoot.mUIAssetManager.GetResWWW(url);
			}
		}
		return result;
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0007D818 File Offset: 0x0007BA18
	public static void LoadAsset(string url, fResourceManager.DownFinishDelegate callBackFun, object customParam)
	{
		bool isClose = GameRoot.IsClose;
		if (!isClose)
		{
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW != null;
			if (flag)
			{
				callBackFun(resWWW, customParam);
			}
			else
			{
				bool flag2 = url.Contains("/0.") || url.Contains("/.");
				if (flag2)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"请不要加载名字是0或者空字符的资源,函数是:",
						callBackFun.Target.ToString(),
						"->",
						callBackFun.Method.Name,
						",url:",
						url
					}));
				}
				bool flag3 = url.StartsWith("/");
				if (flag3)
				{
					url = url.Substring(1, url.Length - 2);
				}
				string text = url;
				int num = text.IndexOf("/");
				bool flag4 = num > 0;
				if (flag4)
				{
					string text2 = text.Substring(0, num);
					string a = text2;
					if (!(a == "file:"))
					{
						if (!(a == "UI"))
						{
							if (!(a == "scene"))
							{
								if (!(a == "HeroAnimator"))
								{
									if (!(a == "EffectRes"))
									{
										if (!(a == "SoundRes"))
										{
											GameRoot.mGlobalAssetManager.LoadDownRequest(url, callBackFun, customParam, url);
										}
										else
										{
											url = url.Substring(num + 1, url.Length - num - 1);
											GameRoot.mSoundResManager.AddDownRequest(url, callBackFun, customParam, GameRoot.mSoundResManager.m_AssetBundleManifest);
										}
									}
									else
									{
										url = url.Substring(num + 1, url.Length - num - 1);
										GameRoot.mEffectResManager.AddDownRequest(url, callBackFun, customParam, GameRoot.mEffectResManager.m_AssetBundleManifest);
									}
								}
								else
								{
									url = url.Substring(num + 1, url.Length - num - 1);
									GameRoot.mHeroAniManager.AddDownRequest(url, callBackFun, customParam, GameRoot.mHeroAniManager.m_AssetBundleManifest);
								}
							}
							else
							{
								url = url.Substring(num + 1, url.Length - num - 1);
								GameRoot.mMapSceneManager.AddDownRequest(url, callBackFun, customParam, GameRoot.mMapSceneManager.m_AssetBundleManifest);
							}
						}
						else
						{
							url = url.Substring(num + 1, url.Length - num - 1);
							GameRoot.mUIAssetManager.AddDownRequest(url, callBackFun, customParam, GameRoot.mUIAssetManager.m_AssetBundleManifest);
						}
					}
					else
					{
						Debug.LogError("请传入相对路径" + url);
					}
				}
			}
		}
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0007DA90 File Offset: 0x0007BC90
	public UserSocket GetSocket()
	{
		return GameRoot.mSocket;
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0007DAA8 File Offset: 0x0007BCA8
	public void InitGame()
	{
		fGameManager.m_nJoinState = 7;
		bool flag = GameRoot.mSocket == null;
		if (flag)
		{
			GameObject gameObject = new GameObject("_fGameNet");
			gameObject.transform.parent = base.transform;
			GameRoot.m_pLogin = new GameObject("LoginSocket")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<UserSocket>();
			GameRoot.m_pLogin.InitUserSocket(1);
			GameRoot.m_pHall = new GameObject("HallSocket")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<UserSocket>();
			GameRoot.m_pHall.InitUserSocket(2);
			GameRoot.m_pScene = new GameObject("SceneSocket")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<UserSocket>();
			GameRoot.m_pScene.InitUserSocket(3);
			GameRoot.m_pChat = new GameObject("ChatSocket")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<UserSocket>();
			GameRoot.m_pChat.InitUserSocket(4);
		}
		this.ShowLocalLoginUI();
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0007DBCC File Offset: 0x0007BDCC
	public string serverSaveStr()
	{
		string input = string.Empty;
		input = Application.dataPath.Replace("/", string.Empty).Replace(":", string.Empty) + this.m_sServer;
		return Fun.Md5Sum(input);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0007DC1C File Offset: 0x0007BE1C
	private void testcallfun(WWW www, object obj)
	{
		Debug.Log("testcallfun");
		UnityEngine.Object original = www.assetBundle.LoadAsset("buttontest.prefab");
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
		gameObject.transform.parent = fUIManager.GetInstance().transform;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0007DC68 File Offset: 0x0007BE68
	private void ShowLocalLoginUI()
	{
		bool flag = !WebParam.isExeLogin;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("Login", NetMsg.UITRANSFROM.LoginUI, new fUIManager.UILoadFinishDelegate(this.Load_UI_Complete), null, true);
		}
		else
		{
			fGameManager.GetInstance().LoginUser();
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0007DCB2 File Offset: 0x0007BEB2
	private void Load_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		GameRoot.Login_Control = _UI_Object.GetComponent<LoginCon>();
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
	public void OnSceneCreate(string scenename)
	{
		HeroPlayer.initMainCamera();
		CFilmManager.Instance.InitFilm();
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			MatchManager.GetInstance().m_nJoinState = 6;
			MatchManager.GetInstance().UserJoinFbLog(MatchManager.GetInstance().m_nJoinState, "不用推显卡");
			GameRoot.SceneSocket.unFsorket.LoadMapOKMsg();
			CReadModel.Instance.isLoaded = true;
		}
		else
		{
			bool flag = fGameManager.Heroplayer != null;
			if (flag)
			{
				fGameManager.m_nLoadScnTime = Time.realtimeSinceStartup - fGameManager.nJoinFbTime;
				fGameManager.nJoinFbTime = Time.realtimeSinceStartup;
			}
			CReadModel.Instance.StartLoadVideoModel();
		}
		bool flag2 = CameraControl.Instance != null && CameraControl.Instance.controlCamera != null;
		if (flag2)
		{
			Setting_Quality_Camera component = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Camera>();
			bool flag3 = component != null;
			if (flag3)
			{
				component.Use_Quality(this.Setting.Video_Class.Effect_Level);
			}
			Setting_Quality_Particle component2 = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Particle>();
			bool flag4 = component2 != null;
			if (flag4)
			{
				component2.Use_Quality(this.Setting.Video_Class.Effect_Level);
			}
		}
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0007DDF9 File Offset: 0x0007BFF9
	public void SetJiaoSeLightObj(GameObject tJiaoSe_Light)
	{
		this.tfJiaoSe_Light = tJiaoSe_Light;
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0007DE04 File Offset: 0x0007C004
	private void Awake()
	{
		GameRoot.inst = this;
		Screen.SetResolution(GlobalDefine.Default_Resolution_Width, GlobalDefine.Default_Resolution_Height, false);
		this.Setting.Default_Resolution_Width = Screen.currentResolution.width;
		this.Setting.Default_Resolution_Height = Screen.currentResolution.height;
		Application.logMessageReceived += this.HandleLog;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0007DE70 File Offset: 0x0007C070
	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		bool flag = logString != null && (type == LogType.Error || type == LogType.Exception);
		if (flag)
		{
			this.ReportE(logString, stackTrace);
		}
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0007DE9D File Offset: 0x0007C09D
	public void ReportE(string sTitle, string sError)
	{
		this.ReportE(sTitle, sError, SysConfig.gUserName);
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0007DEB0 File Offset: 0x0007C0B0
	public void ReportE(string sTitle, string sError, string UserName)
	{
		bool flag = SysConfig.g_logType == 0;
		if (!flag)
		{
			int count = this.aReportError.Count;
			bool flag2 = sTitle.Length > 1000;
			if (flag2)
			{
				sTitle = sTitle.Substring(0, 1000);
			}
			bool flag3 = sError.Length > 1000;
			if (flag3)
			{
				sError = sError.Substring(0, 1000);
			}
			string text = sTitle + sError;
			for (int i = count - 1; i >= 0; i--)
			{
				bool flag4 = this.aReportError[i] == text;
				if (flag4)
				{
					return;
				}
			}
			this.aReportError.Add(text);
			base.StartCoroutine(this.DoWWWFrom(sTitle, sError, UserName));
		}
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0007DF7E File Offset: 0x0007C17E
	private IEnumerator DoWWWFrom(string sTitle, string sError, string UserName)
	{
		WWWForm form = new WWWForm();
		form.AddField("t", sTitle);
		sError = Fun.EncodeBase64(sError);
		form.AddField("e", sError);
		form.AddField("u", UserName);
		yield return new WWW(SysConfig.getWebURL("report.aspx?c=1"), form);
		yield break;
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0007DFA4 File Offset: 0x0007C1A4
	private void OnApplicationFocus(bool flag)
	{
		this.m_bIsFocus = flag;
		if (flag)
		{
			bool flag2 = fGameManager.m_nUserType == 1 && !HeroPlayer.isMouseShow;
			if (flag2)
			{
				HeroPlayer.isPlayerControl = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
		else
		{
			bool flag3 = fGameManager.m_nUserType == 1;
			if (flag3)
			{
				bool flag4 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible && MatchManager.GetInstance().m_nIsChangeHero == 1;
				if (flag4)
				{
					return;
				}
				bool flag5 = !HeroPlayer.isMouseShow;
				if (flag5)
				{
					HeroPlayer.isPlayerControl = false;
					HeroPlayer.isMouseShow = true;
					ChatManager.GetInstance().OnCloseChatUI(HeroPlayer.isPlayerControl);
					bool flag6 = Setting_OptionCon.Instance == null || !Setting_OptionCon.Instance.visible;
					if (flag6)
					{
						this.Setting.Open_Setting_Option(null);
					}
				}
				fUIManager.GetInstance().CloseTabWin();
			}
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0007E0AC File Offset: 0x0007C2AC
	private void Update()
	{
		bool flag = this.godModeEnabled && fGameManager.Heroplayer != null && fGameManager.Heroplayer.playerCommon != null;
		if (flag)
		{
			fGameManager.Heroplayer.playerCommon.m_nHp = fGameManager.Heroplayer.playerCommon.m_nMaxHp;
			fGameManager.Heroplayer.playerCommon.m_nShield = fGameManager.Heroplayer.playerCommon.m_nMaxShield;
		}
		bool flag2 = Input_Manager.Get_Button("Interact_Menu");
		if (flag2)
		{
			bool flag3 = MatchManager.GetInstance().m_nMatchType == 4 && fGameManager.GetInstance().getPlayer().playerCommon.m_nModelIndex == 2000;
			if (flag3)
			{
				return;
			}
			ChatManager.GetInstance().OnOpenHuDongWin();
		}
		else
		{
			ChatManager.GetInstance().OnCloseHuDongWin();
		}
		bool flag4 = Input_Manager.Get_Button_Down("Paint") && CPaintManager.Instance != null;
		if (flag4)
		{
			CPaintManager.Instance.OnSetPaint();
		}
		ChatManager.GetInstance().OnHuDongOnKey();
		bool flag5 = !HeroBase.goBalckCar;
		if (flag5)
		{
			ScnMsgManager.GetInstance().OnInputKeyCode();
		}
		bool flag6 = !this.m_bIsFocus;
		if (flag6)
		{
			HeroPlayer.isPlayerControl = this.m_bIsFocus;
		}
		else
		{
			bool flag7 = fGameManager.m_nUserType == 1;
			if (flag7)
			{
				bool keyDown = Input.GetKeyDown(KeyCode.Escape);
				if (keyDown)
				{
					bool flag8 = (DtsUICon.Inst != null && DtsUICon.Inst.visible && DtsUICon.Inst.DtsResultUI.activeSelf) || (SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible && MatchManager.GetInstance().m_nIsChangeHero == 1) || fGameManager.GetInstance().getPlayer() == null || !CReadModel.Instance.isLoaded;
					if (flag8)
					{
						return;
					}
					HeroPlayer.isPlayerControl = !HeroPlayer.isPlayerControl;
					HeroPlayer.isMouseShow = !HeroPlayer.isPlayerControl;
					ChatManager.GetInstance().OnCloseChatUI(HeroPlayer.isPlayerControl);
					bool flag9 = !HeroPlayer.isPlayerControl;
					if (flag9)
					{
						this.Setting.Open_Setting_Option(null);
					}
					else
					{
						this.Escape();
					}
				}
			}
			else
			{
				bool keyDown2 = Input.GetKeyDown(KeyCode.Escape);
				if (keyDown2)
				{
					this.Setting.Open_Setting_Option(null);
				}
			}
			bool keyDown3 = Input.GetKeyDown(KeyCode.Tab);
			if (keyDown3)
			{
				ChatManager.GetInstance().ChangePinDao();
			}
			bool flag10 = Input_Manager.Get_Button_Down("Check_Hero_Information");
			if (flag10)
			{
				fUIManager.GetInstance().ShowHeroSkillInfo();
			}
			bool flag11 = Input_Manager.Get_Button_Up("Reply_Whisper");
			if (flag11)
			{
				ChatManager.GetInstance().OnPenChatMiYu();
			}
		}
		bool flag12 = (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) && TipManager.GetInstance().isPopTip && !MatchManager.GetInstance().isEnterFb;
		if (flag12)
		{
			bool flag13 = easy_tipCon.inst != null;
			if (flag13)
			{
				easy_tipCon.inst.OnKeyPress();
			}
			ChatManager.GetInstance().isChatPopTip = true;
		}
		bool flag14 = Input_Manager.Get_Button_Down("Enter");
		if (flag14)
		{
			ChatManager.GetInstance().OnPenChatUI();
		}
		bool flag15 = Input_Manager.Get_Button_Up("Check_Hero_Information");
		if (flag15)
		{
			fUIManager.GetInstance().CloseHeroSkillInfo();
		}
		bool flag16 = HeroPlayer.isMouseShow || fGameManager.m_nUserType == 0;
		if (flag16)
		{
			bool flag17 = !Cursor.visible && !fUIManager.IsLoading;
			if (flag17)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
		else
		{
			bool flag18 = Cursor.visible | Cursor.lockState != CursorLockMode.Locked;
			if (flag18)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
		bool flag19 = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F11) && PlayerDataManager.getInstance().HUserData.m_nGM == 1 && fGameManager.m_nUserType == 0;
		if (flag19)
		{
			fUIManager.GetInstance().ShowWin("GMUI", NetMsg.UITRANSFROM.PublicUI, null, null, true);
		}
		this.OnCheckFCM();
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0007E4B8 File Offset: 0x0007C6B8
	public static void reConnet()
	{
		GameRoot.mSocket.reConnet();
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0007E4C6 File Offset: 0x0007C6C6
	public void SetChatServerData(int nCid, ArrayList szChat)
	{
		this.UserCid = nCid;
		this.szChatServerData = szChat;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0007E4D7 File Offset: 0x0007C6D7
	public void LinkChatServer()
	{
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0007E4DA File Offset: 0x0007C6DA
	private void OnCheckFCM()
	{
		HeroDataManager.GetInstance().CheckTipTime();
		RoomManage.GetInstance().CheckInvitedTime();
		SkyLadderManager.GetInstance().CheckForceTime();
		DtsBatteManager.GetInstance().CheckForceTime();
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0007E50C File Offset: 0x0007C70C
	public void Escape()
	{
		bool flag = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible && fGameManager.IsJoinGame;
		if (flag)
		{
			bool flag2 = MatchManager.GetInstance().m_nIsChangeHero != 1;
			if (flag2)
			{
				SelectHeroUICon.inst.Close();
			}
		}
		else
		{
			bool flag3 = (Setting_OptionCon.Instance != null && Setting_OptionCon.Instance.visible) || (Setting_Option_HallCon.Instance != null && Setting_Option_HallCon.Instance.visible);
			if (flag3)
			{
				this.Setting.Close_Setting_Option(null);
			}
			else
			{
				bool flag4 = SettingCon.Instance != null && SettingCon.Instance.visible;
				if (flag4)
				{
					this.Setting.View_Component.Close(null);
				}
			}
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0007E5E4 File Offset: 0x0007C7E4
	public void SendHttpRequest(int nType, ArrayList szNetMsg)
	{
		switch (nType)
		{
		case 1600:
		{
			int num = 0;
			bool flag = PlayerDataManager.getInstance().HUserData != null && PlayerDataManager.getInstance().HUserData.m_sOnLoginDate != string.Empty;
			if (flag)
			{
				DateTime d = DateTime.Parse(PlayerDataManager.getInstance().HUserData.m_sOnLoginDate);
				num = (int)(DateTime.Now - d).TotalSeconds;
			}
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/interface/qqreport.aspx?type=userquit&uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&useronlinetime=",
				num.ToString(),
				"&pf=",
				WebParam.client_pf
			}))));
			break;
		}
		case 1601:
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new object[]
			{
				"/interface/qqreport.aspx?type=usercost&uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&usercostnum=",
				szNetMsg[0],
				"&pf=",
				WebParam.client_pf
			}))));
			break;
		case 1602:
		{
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/interface/qqreport.aspx?type=usercreateplayer&uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid.ToString(),
				"&pf=",
				WebParam.client_pf
			}))));
			bool flag2 = WebParam.client_islogover != "1";
			if (flag2)
			{
				base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
				{
					"/qqapi/loadexe_area.ashx?uid=",
					WebParam.client_account,
					"&cid=",
					WebParam.client_cid,
					"&str=create"
				}))));
			}
			break;
		}
		case 1604:
		{
			bool flag3 = WebParam.client_islogover != "1";
			if (flag3)
			{
				base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
				{
					"/qqapi/loadexe_area.ashx?uid=",
					WebParam.client_account,
					"&cid=",
					WebParam.client_cid,
					"&str=inhome"
				}))));
			}
			break;
		}
		case 1605:
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/qqapi/loadexe_area.ashx?uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&str=startbut"
			}))));
			break;
		case 1606:
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/qqapi/loadexe_area.ashx?uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&str=startui"
			}))));
			break;
		case 1607:
		{
			bool flag4 = WebParam.client_islogover != "1";
			if (flag4)
			{
				base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
				{
					"/qqapi/loadexe_area.ashx?uid=",
					WebParam.client_account,
					"&cid=",
					WebParam.client_cid,
					"&str=closeunity&state=",
					fGameManager.m_nJoinState.ToString()
				}))));
			}
			break;
		}
		case 1608:
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/qqapi/client_socketerror.aspx?uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&errormsg=",
				(string)szNetMsg[0]
			}))));
			break;
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0007E9B4 File Offset: 0x0007CBB4
	private void OnApplicationQuit()
	{
		bool flag = fGameManager.m_nIsLoad <= 0;
		if (flag)
		{
			bool flag2 = this.nSend == 0;
			if (flag2)
			{
				base.Invoke("DelClose", 1f);
				bool flag3 = fGameManager.m_nJoinState != 17;
				if (flag3)
				{
					GameRoot.inst.SendHttpRequest(1607, new ArrayList());
				}
				this.nSend = 1;
				Debug.Log("gameroot111//" + fGameManager.m_nIsLoad.ToString() + "//" + this.nSend.ToString());
				bool flag4 = gameuiCon.inst != null && gameuiCon.inst.DtsUiObj != null;
				if (flag4)
				{
					gameuiCon.inst.DtsUiObj.OnAllPosInPut();
				}
			}
		}
		else
		{
			bool flag5 = this.nSend == 0;
			if (flag5)
			{
				bool flag6 = fGameManager.m_nJoinState != 17;
				if (flag6)
				{
					GameRoot.inst.SendHttpRequest(1607, new ArrayList());
				}
				this.nSend = 1;
			}
		}
		bool flag7 = fGameManager.m_nJoinState != 17 && !fGameManager.isClose;
		if (flag7)
		{
			Debug.Log("gameroot222//" + fGameManager.m_nJoinState.ToString() + "//" + fGameManager.isClose.ToString());
		}
		bool flag8 = Time.time < 120f;
		if (flag8)
		{
			Debug.Log("gameroot--onapplicationquit--timelimit--" + Time.time.ToString());
		}
		base.Invoke("DelClose", 5f);
		bool flag9 = fGameManager.m_nIsLoad > 0;
		if (flag9)
		{
			GameRoot.IsClose = true;
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(1401, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, new TipManager.TipOKDelegate(this.CloseWindow), null, null, null);
			Debug.Log("gameroot333//" + fGameManager.m_nIsLoad.ToString());
		}
		VoiceGameCon.Inst.UnInit();
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0007EBE1 File Offset: 0x0007CDE1
	public void CloseWindow(object szparm)
	{
		this.DelClose();
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0007EBEC File Offset: 0x0007CDEC
	private void DelClose()
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		bool flag2 = fGameManager.Heroplayer != null;
		if (flag2)
		{
			fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		fGameManager.m_nIsLoad = 0;
		VoiceGameCon.Inst.UnInit();
		Application.Quit();
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0007EC8C File Offset: 0x0007CE8C
	public void InitGMCamera()
	{
		bool flag = fGameManager.Heroplayer == null || fGameManager.Heroplayer.playerCommon.m_nGM <= 0 || fGameManager.Heroplayer.playerCommon.m_szGMTran.Count <= 0;
		if (!flag)
		{
			int num = (int)fGameManager.Heroplayer.playerCommon.m_szGMTran[0];
			int cid = (int)fGameManager.Heroplayer.playerCommon.m_szGMTran[1];
			GamePlayer playerByCid = User_Data_Handle.GetPlayerByCid(cid);
			bool flag2 = playerByCid == null || playerByCid.camTarget == null || playerByCid.haveDestroy || !playerByCid.playerHaveCreate;
			if (flag2)
			{
				base.Invoke("InitGMCamera", 0.2f);
			}
			else
			{
				bool flag3 = playerByCid.heroData.key != this.gmFollowHero && this.theGmState != 2;
				if (flag3)
				{
					CameraControl.Instance.transform.gameObject.SetActive(true);
					GameRoot.inst.audioEar.transform.SetParent(CameraControl.Instance.transform, false);
					GameRoot.inst.audioEar.transform.localPosition = Vector3.zero;
					this.g_m_camera(playerByCid, 1);
					fUIManager.SetProgreessShow(false);
					this.gmFollowHero = playerByCid.heroData.key;
				}
				base.Invoke("InitGMCamera", 1f);
			}
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0007EE08 File Offset: 0x0007D008
	private void CheckGM()
	{
		bool flag = this.theGmTarget != null && this.gmChangeLast != this.gmChange && !this.theGmTarget.haveDestroy && this.theGmTarget.playerHaveCreate;
		if (flag)
		{
			this.gmChangeLast = this.gmChange;
			this.theGmTarget.FpsOrTps = this.theGmState;
			this.theGmTarget.ChangeFpsTpsState();
			CameraControl.Instance.InitCameraHero(this.theGmTarget);
			int num = this.theGmState;
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					CameraControl.iCameraState = CameraState.ThirdState;
				}
			}
			else
			{
				CameraControl.iCameraState = CameraState.FirstState;
			}
			CameraControl.iCameraCtrl = CameraCtrlState.CameraLuxiang;
		}
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0007EEC0 File Offset: 0x0007D0C0
	public bool IsGmTarget(NetLiving target)
	{
		bool flag = target == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.theGmState == 2;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = target == this.theGmTarget;
				result = flag3;
			}
		}
		return result;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0007EF04 File Offset: 0x0007D104
	public void g_m_camera(NetLiving net, int _State)
	{
		this.theGmTarget = net;
		this.theGmState = _State;
		this.gmChange++;
		this.CheckGM();
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0007EF2A File Offset: 0x0007D12A
	public void g_m_camera_change(int _State)
	{
		this.theGmState = _State;
		this.gmChange++;
		this.CheckGM();
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0007EF4C File Offset: 0x0007D14C
	public void ClearGMCameraData()
	{
		this.theGmState = 0;
		bool flag = !(this.Setting_FPS.GM_Camera == null) && !(this.Setting_FPS.GM_Camera_Component == null);
		if (flag)
		{
			this.Setting_FPS.GM_Camera_Component.enabled = false;
			this.Setting_FPS.GM_Camera.SetActive(false);
		}
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0007EFB8 File Offset: 0x0007D1B8
	public void ClearStoke()
	{
		foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
		{
			bool bePlayer = keyValuePair.Value.bePlayer;
			if (bePlayer)
			{
				GamePlayer gamePlayer = (GamePlayer)keyValuePair.Value;
				gamePlayer.Open_Or_Close_Stroke_Material(false);
			}
		}
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0007F03C File Offset: 0x0007D23C
	public void StartSkyLadderRefresh(int nsec)
	{
		base.CancelInvoke("RefreshSkyLadder");
		nsec += 10;
		base.Invoke("RefreshSkyLadder", (float)nsec * 1f);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0007F065 File Offset: 0x0007D265
	private void RefreshSkyLadder()
	{
		SkyLadderManager.GetInstance().CheckRefreshTime();
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0007F073 File Offset: 0x0007D273
	public void CheckSkyLadderOpenTip(int nsec)
	{
		base.CancelInvoke("CheckSkyLadderOpen");
		base.Invoke("CheckSkyLadderOpen", (float)nsec * 1f);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0007F096 File Offset: 0x0007D296
	private void CheckSkyLadderOpen()
	{
		SkyLadderManager.GetInstance().CheckTipTime();
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0007F0A4 File Offset: 0x0007D2A4
	public void OnDelayCheckTip()
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			base.StartCoroutine("OnCheckCommonTip");
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0007F0CC File Offset: 0x0007D2CC
	private IEnumerator OnCheckCommonTip()
	{
		yield return new WaitForFixedUpdate();
		TipManager.GetInstance().OnCheckTip();
		yield break;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0007F0DB File Offset: 0x0007D2DB
	public void OnDelPveRefresh()
	{
		base.CancelInvoke("RefreshPveLadder");
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0007F0EA File Offset: 0x0007D2EA
	public void StartPveRefresh(int nsec)
	{
		base.CancelInvoke("RefreshPveLadder");
		nsec += 2;
		base.Invoke("RefreshPveLadder", (float)nsec * 1f);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0007F112 File Offset: 0x0007D312
	public void RefreshPveLadder()
	{
		PveManager.GetInstance().OnSetMaoXianBtnState();
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0007F120 File Offset: 0x0007D320
	public void CheckGuildTanhe()
	{
		string sTanheUser = GuildManager.GetInstance().m_sTanheUser;
		string sTanheTime = GuildManager.GetInstance().m_sTanheTime;
		base.CancelInvoke("CheckGuildTanhe");
		bool flag = GuildManager.GetInstance().m_nGuildId != 0 && !string.IsNullOrEmpty(sTanheUser);
		if (flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			DateTime d = DateTime.Parse(sTanheTime);
			int num = (int)(d - serverDate).TotalSeconds;
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.SetGuildTanhe(num);
			}
			bool flag3 = num >= 0;
			if (flag3)
			{
				base.Invoke("CheckGuildTanhe", 1f);
			}
			else
			{
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91035,
					new ArrayList()
				});
			}
		}
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0007F208 File Offset: 0x0007D408
	private void OnGUI()
	{
		bool flag = Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.F1 || Event.current.keyCode == KeyCode.Insert);
		if (flag)
		{
			this.showModMenu = !this.showModMenu;
			HeroPlayer.isMouseShow = this.showModMenu;
		}
		bool flag2 = !this.showModMenu;
		if (!flag2)
		{
			this.windowRect = GUI.Window(999, this.windowRect, new GUI.WindowFunction(this.DrawModMenuWindow), "Trigger Run - Mod/Debug Menu");
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
	private void DrawModMenuWindow(int windowID)
	{
		GUI.DragWindow(new Rect(0f, 0f, 380f, 20f));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Space(10f);
		GUILayout.Label("<b>[ TRAPAÇAS & STATUS ]</b>", new GUILayoutOption[]
		{
			GUILayout.Width(360f)
		});
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		bool flag = GUILayout.Button("Curar Total (Heal)", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag)
		{
			bool flag2 = fGameManager.Heroplayer != null && fGameManager.Heroplayer.playerCommon != null;
			if (flag2)
			{
				fGameManager.Heroplayer.playerCommon.m_nHp = fGameManager.Heroplayer.playerCommon.m_nMaxHp;
				fGameManager.Heroplayer.playerCommon.m_nShield = fGameManager.Heroplayer.playerCommon.m_nMaxShield;
			}
		}
		bool flag3 = GUILayout.Button("Ult 100% (DaZhao)", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag3)
		{
			bool flag4 = fGameManager.Heroplayer != null && fGameManager.Heroplayer.playerDazhao != null && fGameManager.Heroplayer.playerDazhao.cdazhaomaininfo != null;
			if (flag4)
			{
				fGameManager.Heroplayer.playerDazhao.cdazhaomaininfo.c_energy_now = fGameManager.Heroplayer.playerDazhao.cdazhaomaininfo.dazhaoxml.denergymax;
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		this.godModeEnabled = GUILayout.Toggle(this.godModeEnabled, "<b>Modo Deus (God Mode)</b>", new GUILayoutOption[]
		{
			GUILayout.Height(25f)
		});
		bool flag5 = GUILayout.Button("Reset Cooldowns", new GUILayoutOption[]
		{
			GUILayout.Height(25f)
		});
		if (flag5)
		{
			bool flag6 = fGameManager.Heroplayer != null && fGameManager.Heroplayer.heroSkill != null;
			if (flag6)
			{
				foreach (int key in fGameManager.Heroplayer.heroSkill.skillsDict.Keys)
				{
					CPSkillStruct cpskillStruct = fGameManager.Heroplayer.heroSkill.skillsDict[key];
					cpskillStruct.m_nDoNumberNow = cpskillStruct.m_nDoNumberMax;
					cpskillStruct.beginTime = Fun.GetServerDate();
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("<b>[ SELECIONAR PERSONAGEM ]</b>", new GUILayoutOption[]
		{
			GUILayout.Width(360f)
		});
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[]
		{
			GUILayout.Height(180f)
		});
		Dictionary<int, string> dictionary = new Dictionary<int, string>
		{
			{
				1025,
				"Eva (LingLong) - Sentry / Teleporter"
			},
			{
				1061,
				"Brigitte (Zhurong) - Fire Sentry"
			},
			{
				1033,
				"Lily - Soul Chains Ultimate"
			},
			{
				1015,
				"Lars - Claymore Mine / Grenade Launcher"
			},
			{
				1031,
				"Loki - Remote Bomb"
			},
			{
				1026,
				"Scarlett - Frozen Trap / Snowball"
			},
			{
				1006,
				"XingTian - Tank / Shield"
			},
			{
				1003,
				"Lars Classic"
			},
			{
				1005,
				"Brigitte Support"
			},
			{
				1023,
				"Evelynn"
			},
			{
				1024,
				"Grace"
			},
			{
				1027,
				"Jae-ha"
			},
			{
				1035,
				"Valkyrie"
			}
		};
		foreach (KeyValuePair<int, string> keyValuePair in dictionary)
		{
			bool flag7 = GUILayout.Button(keyValuePair.Value, new GUILayoutOption[]
			{
				GUILayout.Height(25f)
			});
			if (flag7)
			{
				this.SendDebugCommand("/hero " + keyValuePair.Key.ToString());
			}
		}
		GUILayout.EndScrollView();
		GUILayout.Space(10f);
		GUILayout.Label("<b>[ GERADOR DE ENTIDADES ]</b>", new GUILayoutOption[]
		{
			GUILayout.Width(360f)
		});
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		bool flag8 = GUILayout.Button("Spawn Dummy (10 HP)", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag8)
		{
			this.SendSpawnCommand(1121);
		}
		bool flag9 = GUILayout.Button("Spawn Dummy (1000 HP)", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag9)
		{
			this.SendSpawnCommand(1122);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		bool flag10 = GUILayout.Button("Spawn Dummy (1500 HP)", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag10)
		{
			this.SendSpawnCommand(1101);
		}
		bool flag11 = GUILayout.Button("Spawn Flame Sentry", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag11)
		{
			this.SendSpawnCommand(1001);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		bool flag12 = GUILayout.Button("Spawn Auto Cannon", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag12)
		{
			this.SendSpawnCommand(1014);
		}
		bool flag13 = GUILayout.Button("Spawn Remote Bomb", new GUILayoutOption[]
		{
			GUILayout.Height(30f)
		});
		if (flag13)
		{
			this.SendSpawnCommand(1019);
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.Label("<color=grey>Aperte [F1] ou [Insert] para fechar o menu.</color>", new GUILayoutOption[]
		{
			GUILayout.Width(360f)
		});
		GUILayout.EndVertical();
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0007F894 File Offset: 0x0007DA94
	public Vector3 GetSpawnPosition()
	{
		bool flag = Camera.main != null;
		if (flag)
		{
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			bool flag2 = Physics.Raycast(ray, out raycastHit, 100f);
			if (flag2)
			{
				Vector3 point = raycastHit.point;
				point.y += 0.5f;
				return point;
			}
		}
		bool flag3 = fGameManager.Heroplayer != null;
		Vector3 result;
		if (flag3)
		{
			result = fGameManager.Heroplayer.tChar.transform.position + fGameManager.Heroplayer.tChar.transform.forward * 3f;
		}
		else
		{
			result = new Vector3(500f, 236f, 241f);
		}
		return result;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0007F968 File Offset: 0x0007DB68
	public void SendSpawnCommand(int npcIndex)
	{
		Vector3 spawnPosition = this.GetSpawnPosition();
		int num = (int)(spawnPosition.x * 100f);
		int num2 = (int)(spawnPosition.y * 100f);
		int num3 = (int)(spawnPosition.z * 100f);
		this.SendDebugCommand(string.Format("/spawn {0} {1} {2} {3}", new object[]
		{
			npcIndex,
			num,
			num2,
			num3
		}));
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0007F9E4 File Offset: 0x0007DBE4
	public void SendDebugCommand(string cmd)
	{
		fGameManager.SendMsgToServer(1006, new ArrayList
		{
			4,
			Fun.EncodingUTF8(cmd),
			string.Empty,
			new ArrayList()
		});
	}

	// Token: 0x04000AC8 RID: 2760
	public static GameRoot inst;

	// Token: 0x04000AC9 RID: 2761
	public fGameManager mGame;

	// Token: 0x04000ACA RID: 2762
	private Setting_Entity _Setting;

	// Token: 0x04000ACB RID: 2763
	private Setting_FPS _Setting_FPS;

	// Token: 0x04000ACC RID: 2764
	private Process_Detector _Process_Detector_Component;

	// Token: 0x04000ACD RID: 2765
	public static UserSocket mSocket;

	// Token: 0x04000ACE RID: 2766
	public static UserSocket mSocket1;

	// Token: 0x04000ACF RID: 2767
	private ArrayList szChatServerData;

	// Token: 0x04000AD0 RID: 2768
	private int UserCid;

	// Token: 0x04000AD1 RID: 2769
	public static LoginCon Login_Control;

	// Token: 0x04000AD2 RID: 2770
	public static fResourceManager mGlobalAssetManager;

	// Token: 0x04000AD3 RID: 2771
	public static fResourceManager mUIAssetManager;

	// Token: 0x04000AD4 RID: 2772
	public static fResourceManager mMapSceneManager;

	// Token: 0x04000AD5 RID: 2773
	public static fResourceManager mHeroAniManager;

	// Token: 0x04000AD6 RID: 2774
	public static fResourceManager mEffectResManager;

	// Token: 0x04000AD7 RID: 2775
	public static fResourceManager mSoundResManager;

	// Token: 0x04000AD8 RID: 2776
	public static PreLoadManager mPreLoadManager;

	// Token: 0x04000AD9 RID: 2777
	private int mPerResNum;

	// Token: 0x04000ADA RID: 2778
	private int mPerResNumCur;

	// Token: 0x04000ADB RID: 2779
	public AudioListener audioEar;

	// Token: 0x04000ADC RID: 2780
	private GameObject audioObject;

	// Token: 0x04000ADD RID: 2781
	public static bool IsClose;

	// Token: 0x04000ADE RID: 2782
	public GameObject tfJiaoSe_Light;

	// Token: 0x04000ADF RID: 2783
	private FPS _Fps_Component;

	// Token: 0x04000AE0 RID: 2784
	private GameObject fReso;

	// Token: 0x04000AE1 RID: 2785
	private static UserSocket m_pLogin;

	// Token: 0x04000AE2 RID: 2786
	private static UserSocket m_pHall;

	// Token: 0x04000AE3 RID: 2787
	private static UserSocket m_pScene;

	// Token: 0x04000AE4 RID: 2788
	private static UserSocket m_pChat;

	// Token: 0x04000AE5 RID: 2789
	public bool netDelay;

	// Token: 0x04000AE6 RID: 2790
	private string m_sServer = "s1";

	// Token: 0x04000AE7 RID: 2791
	private List<string> aReportError = new List<string>();

	// Token: 0x04000AE8 RID: 2792
	public bool m_bIsFocus = true;

	// Token: 0x04000AE9 RID: 2793
	private int nSend;

	// Token: 0x04000AEA RID: 2794
	public int gmFollowHero;

	// Token: 0x04000AEB RID: 2795
	private NetLiving theGmTarget;

	// Token: 0x04000AEC RID: 2796
	private int theGmState = 1;

	// Token: 0x04000AED RID: 2797
	private int gmChange;

	// Token: 0x04000AEE RID: 2798
	private int gmChangeLast;

	// Token: 0x04000AEF RID: 2799
	private bool showModMenu = false;

	// Token: 0x04000AF0 RID: 2800
	private Rect windowRect = new Rect(40f, 40f, 380f, 540f);

	// Token: 0x04000AF1 RID: 2801
	private bool godModeEnabled = false;

	// Token: 0x04000AF2 RID: 2802
	private Vector2 scrollPosition = Vector2.zero;
}

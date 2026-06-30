using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class FPS : MonoBehaviour
{
	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00069A3C File Offset: 0x00067C3C
	// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00069A6E File Offset: 0x00067C6E
	public HallUser lobbyplayer
	{
		get
		{
			bool flag = this._LobbyPlayer == null;
			if (flag)
			{
				this._LobbyPlayer = fGameManager.LobbyPlayer;
			}
			return this._LobbyPlayer;
		}
		set
		{
			this._LobbyPlayer = value;
		}
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00069A78 File Offset: 0x00067C78
	private void Start()
	{
		FPS.instance = this;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00069A84 File Offset: 0x00067C84
	private void SendServerMsg()
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		if (player != null)
		{
			player.SendMessageToServer(41111, new ArrayList
			{
				DateTime.Now.ToString(),
				0
			});
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00069AD4 File Offset: 0x00067CD4
	private void jisuanMoveSpeed()
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player != null && Time.deltaTime != 0f;
		if (flag)
		{
			bool flag2 = player.tChar.animState.moveDirection.magnitude <= 0.1f;
			if (flag2)
			{
				this.allTime = 0f;
			}
			else
			{
				this.allTime += Time.deltaTime;
			}
		}
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00069B50 File Offset: 0x00067D50
	private void jisuanFPS()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		bool flag = (double)this.timeleft > 0.0;
		if (!flag)
		{
			this.fpsNum = this.accum / (float)this.frames;
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
			bool flag2 = CEffectPools.Instance != null;
			if (flag2)
			{
				this.poolnum = CEffectPools.Instance.allNumber;
			}
			bool flag3 = fGameManager.Heroplayer != null && fGameManager.Heroplayer.playerLocal != null;
			if (flag3)
			{
				this.ping = fGameManager.Heroplayer.playerLocal.nowPing;
			}
			else
			{
				this.ping = -1;
			}
			this.MonoUsedM = 0U;
			this.fpsTxt = string.Format("FPS:{0:F0} , Ping:{1} , Pools:{2} Memory:{3}", new object[]
			{
				this.fpsNum,
				this.ping,
				this.poolnum,
				this.MonoUsedM
			});
			bool flag4 = fGameManager.m_nUserType == 0 && fGameManager.LobbyPlayer != null && fGameManager.Heroplayer == null;
			if (flag4)
			{
				bool flag5 = this.lobbyFPS + this.fpsNum < float.MaxValue;
				if (flag5)
				{
					this.lobbyFPS += this.fpsNum;
					this.enterLobbyAvarageTimes++;
				}
				else
				{
					this.lobbyAvarageFPS = this.lobbyFPS / (float)this.enterLobbyAvarageTimes;
					this.LobbyFPSAccurate(this.lobbyAvarageFPS);
					this.enterLobbyAvarageTimes = 0;
					this.lobbyFPS = 0f;
				}
			}
			else
			{
				HeroPlayer player = fGameManager.GetInstance().getPlayer();
				bool flag6 = player == null || player.tCharAnimationFps == null;
				if (!flag6)
				{
					bool flag7 = fUIManager.mInst.GetNowGameState() == 0;
					if (flag7)
					{
						bool flag8 = this.prepare_FPS + this.fpsNum < float.MaxValue;
						if (flag8)
						{
							this.prepare_FPS += this.fpsNum;
							this.enterparepare_AvarageTimes++;
						}
						else
						{
							this.prepare_AvarageFPS = this.prepare_FPS / (float)this.enterparepare_AvarageTimes;
							this.PrePareFPSAccurate(this.prepare_AvarageFPS);
							this.enterparepare_AvarageTimes = 0;
							this.prepare_FPS = 0f;
						}
					}
					bool flag9 = fUIManager.mInst.GetNowGameState() == 1;
					if (flag9)
					{
						bool flag10 = this.fight_FPS + this.fpsNum < 1000f;
						if (flag10)
						{
							this.fight_FPS += this.fpsNum;
							this.enterFight_AvarageTimes++;
						}
						else
						{
							this.fight_AvarageFPS = this.fight_FPS / (float)this.enterFight_AvarageTimes;
							this.FightFPSAccurate(this.fight_AvarageFPS);
							this.enterFight_AvarageTimes = 0;
							this.fight_FPS = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00069E6C File Offset: 0x0006806C
	private float LobbyFPSAccurate(float fps)
	{
		this.enterCount++;
		this.lobbyFPSAccure += fps;
		this.lobbyAvarageFPS = this.lobbyFPSAccure / (float)this.enterCount;
		return this.lobbyAvarageFPS;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00069EB4 File Offset: 0x000680B4
	private float PrePareFPSAccurate(float fps)
	{
		this.prepare_enterCount++;
		this.prepare_FPSAccure += fps;
		this.prepare_AvarageFPS = this.prepare_FPSAccure / (float)this.prepare_enterCount;
		return this.prepare_AvarageFPS;
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00069EFC File Offset: 0x000680FC
	private float FightFPSAccurate(float fps)
	{
		this.fight_enterCount++;
		this.fight_FPSAccure += fps;
		this.fight_AvarageFPS = this.fight_FPSAccure / (float)this.fight_enterCount;
		return this.fight_AvarageFPS;
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00069F44 File Offset: 0x00068144
	public float getLobbyFPSAccurate()
	{
		bool flag = this.enterCount > 0;
		float result;
		if (flag)
		{
			result = this.lobbyAvarageFPS;
		}
		else
		{
			result = this.lobbyFPS / (float)this.enterLobbyAvarageTimes;
		}
		return result;
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00069F7C File Offset: 0x0006817C
	public float getPrePareFPSAccurate()
	{
		bool flag = this.prepare_enterCount > 0;
		float result;
		if (flag)
		{
			result = this.prepare_AvarageFPS;
		}
		else
		{
			result = this.prepare_FPS / (float)this.enterparepare_AvarageTimes;
		}
		return result;
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00069FB4 File Offset: 0x000681B4
	public float getFightFPSAccurate()
	{
		bool flag = this.fight_enterCount > 0;
		float result;
		if (flag)
		{
			result = this.fight_AvarageFPS;
		}
		else
		{
			result = this.fight_FPS / (float)this.enterFight_AvarageTimes;
		}
		return result;
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00069FEC File Offset: 0x000681EC
	private void showplayer()
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.Heroplayer.userModelFps.SetActive(this.splayer);
		}
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x0006A020 File Offset: 0x00068220
	public bool crash()
	{
		return this.crash();
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0006A038 File Offset: 0x00068238
	private void Update()
	{
		bool flag = this.pointshow != null;
		if (flag)
		{
			this.pointshow.transform.position = this.testAt;
		}
		this.jisuanFPS();
		bool keyDown = Input.GetKeyDown(KeyCode.F5);
		if (keyDown)
		{
			bool flag2 = Fun.m_nIsTopUp() == 1;
			if (flag2)
			{
				return;
			}
			this.showFps = !this.showFps;
		}
		bool flag3 = Input.GetKeyDown(KeyCode.F6) && (PlayerDataManager.getInstance().HUserData.m_nGM == 1 || Fun.m_nIsTopUp() != 1);
		if (flag3)
		{
			this.showNet = !this.showNet;
			this.beginGetTime = DateTime.Now;
		}
		else
		{
			bool flag4 = Input.GetKeyDown(KeyCode.F7) && (PlayerDataManager.getInstance().HUserData.m_nGM == 1 || Fun.m_nIsTopUp() != 1);
			if (flag4)
			{
				this.showNetGet = !this.showNetGet;
			}
			BaseSocket.showMsgInfo = this.showNet;
			BaseSocket.showMsgInfoGet = this.showNetGet;
			bool keyDown2 = Input.GetKeyDown(KeyCode.F9);
			if (keyDown2)
			{
				bool flag5 = Fun.m_nIsTopUp() == 1;
				if (flag5)
				{
					return;
				}
				GameRoot.mPreLoadManager.StartPreLoad();
			}
			bool keyDown3 = Input.GetKeyDown(KeyCode.F8);
			if (keyDown3)
			{
				bool flag6 = Fun.m_nIsTopUp() == 1;
				if (flag6)
				{
					return;
				}
				this.debugLogSend = true;
				this.debugLogGet = true;
			}
			bool keyDown4 = Input.GetKeyDown(KeyCode.F4);
			if (keyDown4)
			{
				bool flag7 = Fun.m_nIsTopUp() == 1;
				if (flag7)
				{
					return;
				}
				this.splayer = !this.splayer;
				this.showplayer();
			}
			bool keyDown5 = Input.GetKeyDown(KeyCode.F11);
			if (keyDown5)
			{
				bool flag8 = Fun.m_nIsTopUp() == 1;
				if (flag8)
				{
					return;
				}
				this.NengLiang();
			}
			bool keyDown6 = Input.GetKeyDown(KeyCode.F12);
			if (keyDown6)
			{
				bool flag9 = Fun.m_nIsTopUp() == 1;
				if (flag9)
				{
					return;
				}
				this.ReturnHall();
			}
			bool keyDown7 = Input.GetKeyDown(KeyCode.F3);
			if (keyDown7)
			{
				bool flag10 = Fun.m_nIsTopUp() == 1;
				if (flag10)
				{
					return;
				}
				this.showVidio = !this.showVidio;
			}
			bool keyDown8 = Input.GetKeyDown(KeyCode.F10);
			if (keyDown8)
			{
				this.showCoords = !this.showCoords;
			}
			bool flag11 = Input.GetKeyDown(KeyCode.L) && (PlayerDataManager.getInstance().HUserData.m_nGM == 1 || Fun.m_nIsTopUp() != 1) && fGameManager.Heroplayer != null;
			if (flag11)
			{
				bool flag12 = CameraControl.iCameraState != CameraState.ThirdState;
				if (flag12)
				{
					fGameManager.GetInstance().getPlayer().changeCameraState(CameraState.ThirdState, null, "playeraction");
				}
				else
				{
					fGameManager.GetInstance().getPlayer().changeCameraState(CameraState.FirstState, null, "playeraction");
				}
			}
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x0006A330 File Offset: 0x00068530
	private void NetShow()
	{
		string text = string.Empty;
		GUIStyle guistyle = new GUIStyle();
		guistyle.normal.background = null;
		guistyle.normal.textColor = new Color(1f, 1f, 0f);
		guistyle.fontSize = 14;
		foreach (string text2 in BaseSocket.dictMsg.Keys)
		{
			bool flag = BaseSocket.dictMsg[text2].now_count >= 3;
			if (flag)
			{
				string text3 = text2;
				text += string.Format("type:{0,-40}count:{1,-20}length:{2:N}(kb)          count/s:{3,-20}length:{4:N}(kb/s)\r\n", new object[]
				{
					text3,
					BaseSocket.dictMsg[text2].count,
					(float)BaseSocket.dictMsg[text2].len / 1024f,
					BaseSocket.dictMsg[text2].now_count,
					(float)BaseSocket.dictMsg[text2].now_len / 1024f
				});
			}
		}
		float num = (float)(DateTime.Now - this.beginGetTime).TotalSeconds;
		bool flag2 = num <= 0f;
		if (flag2)
		{
			num = 1f;
		}
		string text4 = string.Format("send all count : {0} , {1}/s    , length :{2:N}kb , {3:N}(kb/s)  , over time:{4:N}s , avg count:{5:N}/s ,avg lenth:{6:N}kb/s", new object[]
		{
			BaseSocket.msg_all_count,
			BaseSocket.msg_now_count,
			(float)BaseSocket.msg_all_len / 1024f,
			(float)BaseSocket.msg_now_len / 1024f,
			num,
			(float)BaseSocket.msg_all_count / num,
			(float)BaseSocket.msg_all_len / 1024f / num
		});
		bool flag3 = this.debugLogSend;
		if (flag3)
		{
			Debug.Log(DateTime.Now.ToString() + "," + text4);
			Debug.Log(DateTime.Now.ToString() + "," + text);
			this.debugLogSend = false;
		}
		GUI.Label(new Rect(100f, 70f, 800f, 30f), text4, guistyle);
		GUI.Label(new Rect(100f, 100f, 800f, 800f), text, guistyle);
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0006A5D0 File Offset: 0x000687D0
	private void NetShowGet()
	{
		string text = string.Empty;
		GUIStyle guistyle = new GUIStyle();
		guistyle.normal.background = null;
		guistyle.normal.textColor = new Color(0f, 0f, 1f);
		guistyle.fontSize = 14;
		foreach (string text2 in BaseSocket.dictMsgGet.Keys)
		{
			bool flag = BaseSocket.dictMsgGet[text2].now_count >= 3;
			if (flag)
			{
				string text3 = text2;
				text += string.Format("type:{0,-40}count:{1,-20}length:{2:N}(kb)          count/s:{3,-20}length:{4:N}(kb/s)\r\n", new object[]
				{
					text3,
					BaseSocket.dictMsgGet[text2].count,
					(float)BaseSocket.dictMsgGet[text2].len / 1024f,
					BaseSocket.dictMsgGet[text2].now_count,
					(float)BaseSocket.dictMsgGet[text2].now_len / 1024f
				});
			}
		}
		float num = (float)(DateTime.Now - this.beginGetTime).TotalSeconds;
		bool flag2 = num <= 0f;
		if (flag2)
		{
			num = 1f;
		}
		string text4 = string.Format("get all count : {0} , {1}/s    , length :{2:N}kb , {3:N}(kb/s)  , over time:{4:N}s , avg count:{5:N}/s ,avg lenth:{6:N}kb/s", new object[]
		{
			BaseSocket.msg_all_countGet,
			BaseSocket.msg_now_countGet,
			(float)BaseSocket.msg_all_lenGet / 1024f,
			(float)BaseSocket.msg_now_lenGet / 1024f,
			num,
			(float)BaseSocket.msg_all_countGet / num,
			(float)BaseSocket.msg_all_lenGet / 1024f / num
		});
		bool flag3 = this.debugLogGet;
		if (flag3)
		{
			Debug.Log(DateTime.Now.ToString() + "," + text4);
			Debug.Log(DateTime.Now.ToString() + "," + text);
			this.debugLogGet = false;
		}
		GUI.Label(new Rect(100f, 370f, 800f, 30f), text4, guistyle);
		GUI.Label(new Rect(100f, 400f, 800f, 800f), text, guistyle);
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0006A870 File Offset: 0x00068A70
	private void ReturnHall()
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			PlayerLearnManager.GetInstance().CloseLearn();
		}
		fUIManager.GetInstance().GotoHall(0);
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0006A8A0 File Offset: 0x00068AA0
	private void NengLiang()
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		if (player != null)
		{
			player.SendMessageToServer(50006, new ArrayList());
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0006A8C4 File Offset: 0x00068AC4
	private void OutputSystemInfo()
	{
		this.systemInfo = string.Concat(new string[]
		{
			SystemInfo.operatingSystem,
			" \r\n processorType(处理器的名称): ",
			SystemInfo.processorType,
			" \r\n processorCount(当前处理器的数量): ",
			SystemInfo.processorCount.ToString(),
			" \r\n processorFrequency(处理器的频率): ",
			SystemInfo.processorFrequency.ToString(),
			" \r\n memorySize(系统内存大小): ",
			SystemInfo.systemMemorySize.ToString(),
			" Mb \r\n Graphics(显卡的名称): ",
			SystemInfo.graphicsDeviceName,
			" vendor: ",
			SystemInfo.graphicsDeviceVendor,
			" \r\n memorySize(显存大小): ",
			SystemInfo.graphicsMemorySize.ToString(),
			" Mb \r\n graphicsMultiThreaded(是否支持多线程渲染): ",
			SystemInfo.graphicsMultiThreaded.ToString(),
			" \r\n supportedRenderTargetCount(支持渲染多少目标纹理): ",
			SystemInfo.supportedRenderTargetCount.ToString(),
			" \r\n graphicsDeviceVersion(显卡的类型和版本): ",
			SystemInfo.graphicsDeviceVersion.ToString(),
			" \r\n graphicsShaderLevel (显卡着色器的级别): ",
			SystemInfo.graphicsShaderLevel.ToString(),
			" \r\n"
		});
		GUI.Label(new Rect(30f, 30f, 800f, 800f), this.systemInfo);
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0006AA18 File Offset: 0x00068C18
	private void OutputVidio()
	{
		this.vidio = string.Concat(new object[]
		{
			"QualitySettings.GetQualityLevel :",
			QualitySettings.GetQualityLevel(),
			"\r\nQualitySettings.pixelLightCount : ",
			QualitySettings.pixelLightCount,
			"\r\nQualitySettings.softParticles : ",
			QualitySettings.softParticles.ToString(),
			"\r\nQualitySettings.softVegetation : ",
			QualitySettings.softVegetation.ToString(),
			"\r\nQualitySettings.masterTextureLimit : ",
			QualitySettings.masterTextureLimit,
			"\r\nQualitySettings.anisotropicFiltering : ",
			QualitySettings.anisotropicFiltering,
			"\r\nQualitySettings.antiAliasing : ",
			QualitySettings.antiAliasing,
			"\r\nQualitySettings.realtimeReflectionProbes : ",
			QualitySettings.realtimeReflectionProbes,
			"\r\nQualitySettings.shadows : ",
			QualitySettings.shadows.ToString(),
			"\r\nQualitySettings.shadowResolution : ",
			QualitySettings.shadowResolution,
			"\r\nQualitySettings.shadowProjection : ",
			QualitySettings.shadowProjection,
			"\r\nQualitySettings.shadowDistance : ",
			QualitySettings.shadowDistance,
			"\r\nQualitySettings.shadowCascades : ",
			QualitySettings.shadowCascades,
			"\r\nQualitySettings.shadowNearPlaneOffset : ",
			QualitySettings.shadowNearPlaneOffset,
			"\r\nQualitySettings.blendWeights : ",
			QualitySettings.blendWeights,
			"\r\nQualitySettings.lodBias : ",
			QualitySettings.lodBias,
			"\r\nQualitySettings.maximumLODLevel : ",
			QualitySettings.maximumLODLevel,
			"\r\nQualitySettings.particleRaycastBudget : ",
			QualitySettings.particleRaycastBudget,
			"\r\nQualitySettings.asyncUploadTimeSlice : ",
			QualitySettings.asyncUploadTimeSlice,
			"\r\nQualitySettings.asyncUploadBufferSize : ",
			QualitySettings.asyncUploadBufferSize,
			"\r\n"
		});
		GUI.Label(new Rect(30f, 30f, 800f, 800f), this.vidio);
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0006AC38 File Offset: 0x00068E38
	public void sendFPSMsgToServer()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.getLobbyFPSAccurate().ToString());
		arrayList.Add(this.getPrePareFPSAccurate().ToString());
		arrayList.Add(this.getFightFPSAccurate().ToString());
		arrayList.Add(SystemInfo.graphicsDeviceName);
		arrayList.Add(SystemInfo.processorType);
		arrayList.Add(WebParam.client_clientip);
		arrayList.Add(QualitySettings.GetQualityLevel());
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player != null;
		if (flag)
		{
			player.SendMessageToServer(1501, arrayList);
		}
		else
		{
			bool flag2 = fGameManager.LobbyPlayer != null;
			if (flag2)
			{
				fGameManager.SendMsgToHallServer(1501, arrayList);
			}
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0006AD00 File Offset: 0x00068F00
	private void OnGUI()
	{
		bool flag = this.showInfo;
		if (flag)
		{
			this.OutputSystemInfo();
		}
		bool flag2 = this.showNet;
		if (flag2)
		{
			this.NetShow();
		}
		bool flag3 = this.showNetGet;
		if (flag3)
		{
			this.NetShowGet();
		}
		bool flag4 = this.showVidio;
		if (flag4)
		{
			this.OutputVidio();
		}
		bool flag5 = this.showFps;
		if (flag5)
		{
			GUI.Label(new Rect(0f, 0f, 680f, 50f), this.fpsTxt);
		}
		bool flag6 = this.showCoords;
		if (flag6)
		{
			this.ShowCoordinates();
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0006ADA4 File Offset: 0x00068FA4
	private void ShowCoordinates()
	{
		HeroPlayer heroPlayer = (fGameManager.GetInstance() != null) ? fGameManager.GetInstance().getPlayer() : null;
		bool flag = heroPlayer == null || heroPlayer.UserControl == null;
		if (!flag)
		{
			Vector3 position = heroPlayer.UserControl.transform.position;
			float y = heroPlayer.UserControl.transform.eulerAngles.y;
			int num = Mathf.RoundToInt(position.x * 100f);
			int num2 = Mathf.RoundToInt(position.y * 100f);
			int num3 = Mathf.RoundToInt(position.z * 100f);
			string text = string.Format("=== COORDENADAS ===", new object[0]);
			string text2 = string.Format("World: X={0:F1}  Y={1:F1}  Z={2:F1}", position.x, position.y, position.z);
			string text3 = string.Format("Server: X={0}  Y={1}  Z={2}", num, num2, num3);
			string text4 = string.Format("Rotation Y: {0:F1}", y);
			bool flag2 = this.coordsBgTex == null;
			if (flag2)
			{
				this.coordsBgTex = new Texture2D(1, 1);
				this.coordsBgTex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
				this.coordsBgTex.Apply();
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.normal.background = this.coordsBgTex;
			GUIStyle guistyle2 = new GUIStyle();
			guistyle2.normal.textColor = new Color(0f, 1f, 0.6f);
			guistyle2.fontSize = 16;
			guistyle2.fontStyle = FontStyle.Bold;
			guistyle2.alignment = TextAnchor.MiddleCenter;
			GUIStyle guistyle3 = new GUIStyle();
			guistyle3.normal.textColor = new Color(1f, 1f, 1f);
			guistyle3.fontSize = 14;
			GUIStyle guistyle4 = new GUIStyle();
			guistyle4.normal.textColor = new Color(1f, 0.85f, 0.2f);
			guistyle4.fontSize = 14;
			guistyle4.fontStyle = FontStyle.Bold;
			GUIStyle guistyle5 = new GUIStyle();
			guistyle5.normal.textColor = new Color(0.5f, 0.8f, 1f);
			guistyle5.fontSize = 13;
			GUIStyle guistyle6 = new GUIStyle();
			guistyle6.normal.textColor = new Color(0.6f, 0.6f, 0.6f);
			guistyle6.fontSize = 11;
			guistyle6.alignment = TextAnchor.MiddleCenter;
			float num4 = 310f;
			float num5 = 110f;
			float num6 = (float)Screen.width - num4 - 10f;
			float num7 = 10f;
			GUI.Box(new Rect(num6, num7, num4, num5), "", guistyle);
			float num8 = 6f;
			GUI.Label(new Rect(num6, num7 + num8, num4, 20f), text, guistyle2);
			GUI.Label(new Rect(num6 + num8, num7 + 28f, num4, 20f), text2, guistyle3);
			GUI.Label(new Rect(num6 + num8, num7 + 48f, num4, 20f), text3, guistyle4);
			GUI.Label(new Rect(num6 + num8, num7 + 68f, num4, 20f), text4, guistyle5);
			GUI.Label(new Rect(num6, num7 + num5 - 18f, num4, 16f), "[F10] Toggle", guistyle6);
		}
	}

	// Token: 0x040009A8 RID: 2472
	private float updateInterval = 0.5f;

	// Token: 0x040009A9 RID: 2473
	private float accum;

	// Token: 0x040009AA RID: 2474
	private int frames;

	// Token: 0x040009AB RID: 2475
	private float timeleft;

	// Token: 0x040009AC RID: 2476
	public float fpsNum;

	// Token: 0x040009AD RID: 2477
	private string fpsTxt = string.Empty;

	// Token: 0x040009AE RID: 2478
	private int poolnum;

	// Token: 0x040009AF RID: 2479
	public static FPS instance;

	// Token: 0x040009B0 RID: 2480
	private float allTime;

	// Token: 0x040009B1 RID: 2481
	public int ping;

	// Token: 0x040009B2 RID: 2482
	private uint MonoUsedM;

	// Token: 0x040009B3 RID: 2483
	private int enterCount;

	// Token: 0x040009B4 RID: 2484
	private float lobbyFPSAccure;

	// Token: 0x040009B5 RID: 2485
	private float lobbyFPS;

	// Token: 0x040009B6 RID: 2486
	private float lobbyAvarageFPS;

	// Token: 0x040009B7 RID: 2487
	private int prepare_enterCount;

	// Token: 0x040009B8 RID: 2488
	private float prepare_FPSAccure;

	// Token: 0x040009B9 RID: 2489
	private float prepare_FPS;

	// Token: 0x040009BA RID: 2490
	private float prepare_AvarageFPS;

	// Token: 0x040009BB RID: 2491
	private int fight_enterCount;

	// Token: 0x040009BC RID: 2492
	private float fight_FPSAccure;

	// Token: 0x040009BD RID: 2493
	private float fight_FPS;

	// Token: 0x040009BE RID: 2494
	private float fight_AvarageFPS;

	// Token: 0x040009BF RID: 2495
	private int enterLobbyAvarageTimes;

	// Token: 0x040009C0 RID: 2496
	private int enterparepare_AvarageTimes;

	// Token: 0x040009C1 RID: 2497
	private int enterFight_AvarageTimes;

	// Token: 0x040009C2 RID: 2498
	public bool showInfo;

	// Token: 0x040009C3 RID: 2499
	public bool showButton;

	// Token: 0x040009C4 RID: 2500
	public bool showFps;

	// Token: 0x040009C5 RID: 2501
	public bool showNet;

	// Token: 0x040009C6 RID: 2502
	public bool showNetGet;

	// Token: 0x040009C7 RID: 2503
	public bool showVidio;

	// Token: 0x040009C8 RID: 2504
	public bool showCoords;

	// Token: 0x040009C9 RID: 2505
	private HallUser _LobbyPlayer;

	// Token: 0x040009CA RID: 2506
	private bool splayer = true;

	// Token: 0x040009CB RID: 2507
	public Vector3 testAt;

	// Token: 0x040009CC RID: 2508
	public Vector3 testDir;

	// Token: 0x040009CD RID: 2509
	public Transform pointshow;

	// Token: 0x040009CE RID: 2510
	private bool debugLogSend;

	// Token: 0x040009CF RID: 2511
	private bool debugLogGet;

	// Token: 0x040009D0 RID: 2512
	private DateTime beginGetTime;

	// Token: 0x040009D1 RID: 2513
	private string systemInfo = string.Empty;

	// Token: 0x040009D2 RID: 2514
	private string vidio = string.Empty;

	// Token: 0x040009D3 RID: 2515
	private Texture2D coordsBgTex;
}

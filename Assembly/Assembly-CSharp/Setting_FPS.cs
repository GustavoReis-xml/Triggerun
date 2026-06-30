using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class Setting_FPS : MonoBehaviour
{
	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06002443 RID: 9283 RVA: 0x00148ABC File Offset: 0x00146CBC
	// (set) Token: 0x06002444 RID: 9284 RVA: 0x00148AD4 File Offset: 0x00146CD4
	public int Frames
	{
		get
		{
			return this._Frames;
		}
		set
		{
			this._Frames = value;
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x06002445 RID: 9285 RVA: 0x00148AE0 File Offset: 0x00146CE0
	// (set) Token: 0x06002446 RID: 9286 RVA: 0x00148AF8 File Offset: 0x00146CF8
	public float Fps
	{
		get
		{
			return this._Fps;
		}
		set
		{
			this._Fps = value;
		}
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x06002447 RID: 9287 RVA: 0x00148B04 File Offset: 0x00146D04
	// (set) Token: 0x06002448 RID: 9288 RVA: 0x00148B1C File Offset: 0x00146D1C
	public float Reduce_Quality_Time_Interval
	{
		get
		{
			return this._Reduce_Quality_Time_Interval;
		}
		set
		{
			this._Reduce_Quality_Time_Interval = value;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x06002449 RID: 9289 RVA: 0x00148B28 File Offset: 0x00146D28
	// (set) Token: 0x0600244A RID: 9290 RVA: 0x00148B40 File Offset: 0x00146D40
	public float Update_Fps_Interval
	{
		get
		{
			return this._Update_Fps_Interval;
		}
		set
		{
			this._Update_Fps_Interval = value;
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x0600244B RID: 9291 RVA: 0x00148B4C File Offset: 0x00146D4C
	// (set) Token: 0x0600244C RID: 9292 RVA: 0x00148B64 File Offset: 0x00146D64
	public float Update_Last_Time
	{
		get
		{
			return this._Update_Last_Time;
		}
		set
		{
			this._Update_Last_Time = value;
		}
	}

	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x0600244D RID: 9293 RVA: 0x00148B70 File Offset: 0x00146D70
	// (set) Token: 0x0600244E RID: 9294 RVA: 0x00148BA2 File Offset: 0x00146DA2
	public ArrayList Fight_Fps_Data
	{
		get
		{
			bool flag = this._Fight_Fps_Data == null;
			if (flag)
			{
				this._Fight_Fps_Data = new ArrayList();
			}
			return this._Fight_Fps_Data;
		}
		set
		{
			this._Fight_Fps_Data = value;
		}
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x0600244F RID: 9295 RVA: 0x00148BAC File Offset: 0x00146DAC
	// (set) Token: 0x06002450 RID: 9296 RVA: 0x00148BC4 File Offset: 0x00146DC4
	public float Current_Time
	{
		get
		{
			return this._Current_Time;
		}
		set
		{
			this._Current_Time = value;
		}
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x06002451 RID: 9297 RVA: 0x00148BD0 File Offset: 0x00146DD0
	// (set) Token: 0x06002452 RID: 9298 RVA: 0x00148BE8 File Offset: 0x00146DE8
	public float Close_Window_Time
	{
		get
		{
			return this._Close_Window_Time;
		}
		set
		{
			this._Close_Window_Time = value;
		}
	}

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x06002453 RID: 9299 RVA: 0x00148BF4 File Offset: 0x00146DF4
	// (set) Token: 0x06002454 RID: 9300 RVA: 0x00148C0C File Offset: 0x00146E0C
	public int Current_Display_Efficiency_FPS
	{
		get
		{
			return this._Current_Display_Efficiency_FPS;
		}
		set
		{
			this._Current_Display_Efficiency_FPS = value;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x06002455 RID: 9301 RVA: 0x00148C18 File Offset: 0x00146E18
	// (set) Token: 0x06002456 RID: 9302 RVA: 0x00148C30 File Offset: 0x00146E30
	public int Current_Display_Efficiency_Ping
	{
		get
		{
			return this._Current_Display_Efficiency_Ping;
		}
		set
		{
			this._Current_Display_Efficiency_Ping = value;
		}
	}

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x06002457 RID: 9303 RVA: 0x00148C3C File Offset: 0x00146E3C
	// (set) Token: 0x06002458 RID: 9304 RVA: 0x00148C7B File Offset: 0x00146E7B
	public Texture Texture
	{
		get
		{
			bool flag = this._Texture == null;
			if (flag)
			{
				this._Texture = (Texture)Resources.Load("hei");
			}
			return this._Texture;
		}
		set
		{
			this._Texture = value;
		}
	}

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x06002459 RID: 9305 RVA: 0x00148C88 File Offset: 0x00146E88
	// (set) Token: 0x0600245A RID: 9306 RVA: 0x00148CA0 File Offset: 0x00146EA0
	public int Game_Count
	{
		get
		{
			return this._Game_Count;
		}
		set
		{
			this._Game_Count = value;
		}
	}

	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x0600245B RID: 9307 RVA: 0x00148CAC File Offset: 0x00146EAC
	// (set) Token: 0x0600245C RID: 9308 RVA: 0x00148CC4 File Offset: 0x00146EC4
	public int Fight_Count
	{
		get
		{
			return this._Fight_Count;
		}
		set
		{
			this._Fight_Count = value;
		}
	}

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x0600245D RID: 9309 RVA: 0x00148CD0 File Offset: 0x00146ED0
	// (set) Token: 0x0600245E RID: 9310 RVA: 0x00148CE8 File Offset: 0x00146EE8
	public bool Load_State
	{
		get
		{
			return this._Load_State;
		}
		set
		{
			this._Load_State = value;
		}
	}

	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x0600245F RID: 9311 RVA: 0x00148CF4 File Offset: 0x00146EF4
	// (set) Token: 0x06002460 RID: 9312 RVA: 0x00148D0C File Offset: 0x00146F0C
	public bool GM_State
	{
		get
		{
			return this._GM_State;
		}
		set
		{
			this._GM_State = value;
		}
	}

	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06002461 RID: 9313 RVA: 0x00148D18 File Offset: 0x00146F18
	// (set) Token: 0x06002462 RID: 9314 RVA: 0x00148D8A File Offset: 0x00146F8A
	public GameObject GM_Camera
	{
		get
		{
			bool flag = this._GM_Camera == null;
			if (flag)
			{
				this._GM_Camera = GameObject.Find("GM_Camera");
				bool flag2 = this._GM_Camera == null;
				if (flag2)
				{
					return null;
				}
				bool flag3 = this._GM_Camera.GetComponent<Smooth_Mouse_Look>() == null;
				if (flag3)
				{
					this._GM_Camera.AddComponent<Smooth_Mouse_Look>();
				}
			}
			return this._GM_Camera;
		}
		set
		{
			this._GM_Camera = value;
		}
	}

	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x06002463 RID: 9315 RVA: 0x00148D94 File Offset: 0x00146F94
	// (set) Token: 0x06002464 RID: 9316 RVA: 0x00148DF2 File Offset: 0x00146FF2
	public Camera GM_Camera_Component
	{
		get
		{
			bool flag = this._GM_Camera_Component == null;
			if (flag)
			{
				bool flag2 = this.GM_Camera == null;
				if (flag2)
				{
					return null;
				}
				this._GM_Camera_Component = this.GM_Camera.GetComponent<Camera>();
				this._GM_Camera_Component.cullingMask = -3;
			}
			return this._GM_Camera_Component;
		}
		set
		{
			this._GM_Camera_Component = value;
		}
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x00148DFC File Offset: 0x00146FFC
	private void Start()
	{
		this.Update_Last_Time = Time.realtimeSinceStartup;
		this.Frames = 0;
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x00148E14 File Offset: 0x00147014
	private void Free_View_Open()
	{
		bool flag = !(this.GM_Camera == null) && !(this.GM_Camera_Component == null);
		if (flag)
		{
			GameRoot.inst.audioEar.transform.SetParent(this.GM_Camera.transform, false);
			GameRoot.inst.audioEar.transform.localPosition = Vector3.zero;
			GameRoot.inst.g_m_camera_change(2);
			this.GM_Camera.transform.position = CameraControl.Instance.gameObject.transform.position;
			this.GM_Camera_Component.enabled = true;
			this.GM_Camera.SetActive(true);
			CameraControl.Instance.controlCamera.gameObject.SetActive(false);
			fUIManager.mInst.Open_Or_Close_Arena_UI(false);
			fUIManager.mInst.Open_Or_Close_Arena_Score_UI(false);
			fUIManager.mInst.Open_Or_Close_Scramble_UI(false);
			this.GM_State = true;
			ChatManager.mInst.OnShowOrHideChatWin(false);
			bool flag2 = ZhanDianCon.inst != null;
			if (flag2)
			{
				ZhanDianCon.inst.SetGmShow(this.GM_State);
			}
			GameRoot.inst.ClearStoke();
		}
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x00148F4C File Offset: 0x0014714C
	public void Free_View_Close()
	{
		bool flag = !(this.GM_Camera == null) && !(this.GM_Camera_Component == null);
		if (flag)
		{
			GameRoot.inst.audioEar.transform.SetParent(CameraControl.Instance.transform, false);
			GameRoot.inst.audioEar.transform.localPosition = Vector3.zero;
			GameRoot.inst.g_m_camera_change(1);
			this.GM_Camera_Component.enabled = false;
			this.GM_Camera.SetActive(false);
			CameraControl.Instance.controlCamera.gameObject.SetActive(true);
			fUIManager.mInst.Open_Or_Close_Arena_UI(true);
			fUIManager.mInst.Open_Or_Close_Arena_Score_UI(true);
			fUIManager.mInst.Open_Or_Close_Scramble_UI(true);
			this.GM_State = false;
			ChatManager.mInst.OnShowOrHideChatWin(true);
			bool flag2 = ZhanDianCon.inst != null;
			if (flag2)
			{
				ZhanDianCon.inst.SetGmShow(this.GM_State);
			}
		}
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x00149054 File Offset: 0x00147254
	private void OnGUI()
	{
		bool flag = !GameRoot.inst.Setting.Load_Setting_Data_State || fGameManager.GetInstance().nNowScnID < 1;
		if (!flag)
		{
			this._Temp_Ping = 0f;
			bool flag2 = fGameManager.Heroplayer != null;
			if (flag2)
			{
				bool flag3 = fGameManager.Heroplayer.playerLocal != null;
				if (flag3)
				{
					this._Temp_Ping = (float)(fGameManager.Heroplayer.playerLocal.nowPing - 50);
				}
				bool flag4 = fGameManager.Heroplayer.playerCommon.m_nGM > 0 && fGameManager.Heroplayer.playerCommon.m_szGMTran.Count > 0;
				if (flag4)
				{
					bool keyDown = Input.GetKeyDown(KeyCode.Home);
					if (keyDown)
					{
						this.Free_View_Open();
					}
					bool keyDown2 = Input.GetKeyDown(KeyCode.End);
					if (keyDown2)
					{
						this.Free_View_Close();
					}
				}
			}
			bool flag5 = this.Current_Display_Efficiency_FPS != 1 && this.Current_Display_Efficiency_Ping != 1;
			if (!flag5)
			{
				bool flag6 = this._Temp_Ping < 35f;
				if (flag6)
				{
					this._Temp_Ping = 35f;
				}
				bool flag7 = this.Current_Display_Efficiency_FPS == 1 && this.Current_Display_Efficiency_Ping == 1;
				if (flag7)
				{
					this._Temp_Texture_Widht = 185;
					GUI.DrawTexture(new Rect(0f, 2f, (float)this._Temp_Texture_Widht, 20f), this.Texture);
					GUI.Label(new Rect(2f, 2f, 100f, 30f), Language.GetLang(102001, new object[]
					{
						GameRoot.inst.Fps_Component.fpsNum
					}));
					GUI.Label(new Rect(110f, 2f, 100f, 30f), Language.GetLang(102002, new object[]
					{
						this._Temp_Ping
					}));
				}
				else
				{
					bool flag8 = this.Current_Display_Efficiency_FPS == 1;
					if (flag8)
					{
						this._Temp_Texture_Widht = 72;
						GUI.DrawTexture(new Rect(0f, 2f, (float)this._Temp_Texture_Widht, 20f), this.Texture);
						GUI.Label(new Rect(2f, 2f, 100f, 30f), Language.GetLang(102001, new object[]
						{
							GameRoot.inst.Fps_Component.fpsNum
						}));
					}
					bool flag9 = this.Current_Display_Efficiency_Ping == 1;
					if (flag9)
					{
						this._Temp_Texture_Widht = 72;
						GUI.DrawTexture(new Rect(0f, 2f, (float)this._Temp_Texture_Widht, 20f), this.Texture);
						GUI.Label(new Rect(2f, 2f, 100f, 30f), Language.GetLang(102002, new object[]
						{
							this._Temp_Ping
						}));
					}
				}
			}
		}
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x0014935C File Offset: 0x0014755C
	private void Load_Gem_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		Gem_View gem_View = _UI_Object.GetComponent<Gem_View>();
		bool flag = gem_View == null;
		if (flag)
		{
			gem_View = _UI_Object.AddComponent<Gem_View>();
		}
		Gem_UICon.Instance.View_Component = gem_View;
		gem_View.Initialize();
	}

	// Token: 0x0600246A RID: 9322 RVA: 0x0014939C File Offset: 0x0014759C
	private void Update()
	{
		bool flag = this.Load_State && Screen.fullScreen != (GameRoot.inst.Setting.Setting_Display_Type == 0);
		if (flag)
		{
			if (!Screen.fullScreen)
			{
				GameRoot.inst.Setting.Setting_Display_Type = 1;
				GameRoot.inst.Setting.Video_Class.Display_Type = 1;
			}
			else
			{
				GameRoot.inst.Setting.Setting_Display_Type = 0;
				GameRoot.inst.Setting.Video_Class.Display_Type = 0;
			}
			bool flag2 = SettingCon.Instance != null && SettingCon.Instance.enabled;
			if (flag2)
			{
				GameRoot.inst.Setting.View_Component.Display_Video_Data();
			}
		}
		bool flag3 = Input_Manager.Get_Button_Up("Improve_Quality");
		if (flag3)
		{
			bool chatInput = ChatManager.ChatInput;
			if (chatInput)
			{
				return;
			}
			bool flag4 = GameRoot.inst.Setting.Video_Class.Effect_Level > 1 && GameRoot.inst.Setting.Video_Class.Effect_Level < 3;
			if (flag4)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(102003, new object[0]), "ffdd00", "system");
				fUISoundManager.instance.PlayUISound("jiemian_unclick", 0, null, 20);
			}
			else
			{
				bool flag5 = GameRoot.inst.Setting.Video_Class.Effect_Level == 3;
				if (flag5)
				{
					GameRoot.inst.Setting.Video_Class.Effect_Level = 1;
				}
				else
				{
					Setting_Entity_Video video_Class = GameRoot.inst.Setting.Video_Class;
					int num = video_Class.Effect_Level;
					video_Class.Effect_Level = num + 1;
				}
				bool flag6 = SettingCon.Instance != null && SettingCon.Instance.visible;
				if (flag6)
				{
					GameRoot.inst.Setting.View_Component.Display_Video_Data();
				}
				GameRoot.inst.Setting.Save_Data();
				this.Use_Setting();
			}
			PlayerPrefs.DeleteKey("Game_FPS_Data");
		}
		else
		{
			bool flag7 = Input_Manager.Get_Button_Up("Reduce_Quality");
			if (flag7)
			{
				bool chatInput2 = ChatManager.ChatInput;
				if (chatInput2)
				{
					return;
				}
				bool flag8 = GameRoot.inst.Setting.Video_Class.Effect_Level < 1;
				if (flag8)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(102004, new object[0]), "ffdd00", "system");
					fUISoundManager.instance.PlayUISound("jiemian_unclick", 0, null, 20);
				}
				else
				{
					bool flag9 = GameRoot.inst.Setting.Video_Class.Effect_Level == 3;
					if (flag9)
					{
						GameRoot.inst.Setting.Video_Class.Effect_Level = 0;
					}
					else
					{
						Setting_Entity_Video video_Class2 = GameRoot.inst.Setting.Video_Class;
						int num = video_Class2.Effect_Level;
						video_Class2.Effect_Level = num - 1;
					}
					bool flag10 = SettingCon.Instance != null && SettingCon.Instance.visible;
					if (flag10)
					{
						GameRoot.inst.Setting.View_Component.Display_Video_Data();
					}
					GameRoot.inst.Setting.Save_Data();
					GameRoot.inst.Setting.Ignore(null);
					this.Use_Setting();
				}
			}
		}
		bool flag11 = fGameManager.GetInstance().nNowScnID < 1;
		if (flag11)
		{
			bool flag12 = Time.time > this._Test_Time;
			if (flag12)
			{
				this._Test_Time = Time.time + 10f;
			}
		}
		else
		{
			bool flag13 = fGameManager.GetInstance().getPlayer() == null;
			if (!flag13)
			{
				bool flag14 = Time.time < fGameManager.GetInstance().getPlayer()._Create_Time;
				if (flag14)
				{
					this.Fight_Fps_Data.Clear();
				}
				else
				{
					this.Current_Time = Time.realtimeSinceStartup;
					bool flag15 = this.Current_Time > this.Reduce_Quality_Time_Interval;
					if (flag15)
					{
						bool flag16 = this.Reduce_Quality_Time_Interval != 0f && fGameManager.GetInstance().getPlayer().playerHaveCreate;
						if (flag16)
						{
							this.Detection_Reduce_Quality();
						}
						this.Reduce_Quality_Time_Interval = this.Current_Time + GlobalDefine.Reduce_Quality_Time_Interval;
					}
					bool flag17 = this.Close_Window_Time != 0f && this.Current_Time > this.Close_Window_Time && Setting_TipsCon.Instance != null;
					if (flag17)
					{
						Setting_TipsCon.Instance.Close();
					}
					bool flag18 = this.Current_Time > this.Update_Last_Time + this.Update_Fps_Interval;
					if (flag18)
					{
						this.Fps = (float)this.Frames / (this.Current_Time - this.Update_Last_Time);
						this.Update_Last_Time = this.Current_Time;
						this.Fight_Fps_Data.Add((int)this.Fps);
						bool flag19 = this.Fight_Fps_Data.Count > 120;
						if (flag19)
						{
							this.Fight_Fps_Data.RemoveAt(0);
						}
						this.Frames = 0;
					}
					int num = this.Frames;
					this.Frames = num + 1;
				}
			}
		}
	}

	// Token: 0x0600246B RID: 9323 RVA: 0x001498C8 File Offset: 0x00147AC8
	public void Load_Prerogative_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		Prerogative_View prerogative_View = _UI_Object.GetComponent<Prerogative_View>();
		bool flag = prerogative_View == null;
		if (flag)
		{
			prerogative_View = _UI_Object.AddComponent<Prerogative_View>();
			prerogative_View.Initialize();
		}
		else
		{
			prerogative_View.Display_Data();
		}
		bool flag2 = _UI_Object.GetComponent<HuangZuanUICon>() == null;
		if (flag2)
		{
			_UI_Object.AddComponent<HuangZuanUICon>();
		}
		PrerogativeCon.Instance.View_Component = prerogative_View;
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x0014992C File Offset: 0x00147B2C
	public void Load_QQ_Games_Hall_Prerogative_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		QQ_Games_Hall_Prerogative_View qq_Games_Hall_Prerogative_View = _UI_Object.GetComponent<QQ_Games_Hall_Prerogative_View>();
		bool flag = qq_Games_Hall_Prerogative_View == null;
		if (flag)
		{
			qq_Games_Hall_Prerogative_View = _UI_Object.AddComponent<QQ_Games_Hall_Prerogative_View>();
			qq_Games_Hall_Prerogative_View.Initialize();
		}
		else
		{
			qq_Games_Hall_Prerogative_View.Display_Data();
		}
		QQ_Games_Hall_PrerogativeCon.Instance.View_Component = qq_Games_Hall_Prerogative_View;
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x00149975 File Offset: 0x00147B75
	private void Load_Setting_Tips_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		Setting_TipsCon.Instance.Initialize(_UI_Object, szParam);
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x00149988 File Offset: 0x00147B88
	private void Use_Setting()
	{
		bool flag = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().userModelFps != null;
		if (flag)
		{
			Setting_Quality_FPS_Model component = fGameManager.GetInstance().getPlayer().userModelFps.GetComponent<Setting_Quality_FPS_Model>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
			CHeroWeapon component2 = fGameManager.GetInstance().getPlayer().userModelFps.GetComponent<CHeroWeapon>();
			bool flag3 = component2 != null;
			if (flag3)
			{
				component2.SetHeroWeapon(1, -1, 0);
			}
		}
		bool flag4 = CameraControl.Instance != null && CameraControl.Instance.controlCamera != null;
		if (flag4)
		{
			Setting_Quality_Camera component3 = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Camera>();
			bool flag5 = component3 != null;
			if (flag5)
			{
				component3.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
			Setting_Quality_Particle component4 = CameraControl.Instance.controlCamera.GetComponent<Setting_Quality_Particle>();
			bool flag6 = component4 != null;
			if (flag6)
			{
				component4.Use_Quality(GameRoot.inst.Setting.Video_Class.Effect_Level);
			}
		}
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x00149AD4 File Offset: 0x00147CD4
	public void Statistic_FPS()
	{
		string text = PlayerPrefs.GetString("Game_FPS_Data");
		ArrayList arrayList = (!(text == string.Empty)) ? new ArrayList(text.Split(new char[]
		{
			'-'
		})) : new ArrayList();
		arrayList.Add(Mathf.RoundToInt(this.Fps).ToString());
		bool flag = arrayList.Count > this.Game_Count;
		if (flag)
		{
			arrayList.RemoveAt(0);
		}
		text = string.Join("-", (string[])arrayList.ToArray(typeof(string)));
		try
		{
			PlayerPrefs.SetString("Game_FPS_Data", text);
		}
		catch (Exception)
		{
			Fun.LogError("没有权限写入注册表，导致Statistic_FPS - SetString报错了", null);
		}
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x00149BA4 File Offset: 0x00147DA4
	public void Detection_Improve_Quality()
	{
		ArrayList arrayList = null;
		int i = 0;
		int num = 0;
		float num2 = 0f;
		string @string = PlayerPrefs.GetString("Game_FPS_Data");
		arrayList = new ArrayList(@string.Split(new char[]
		{
			'-'
		}));
		for (i = 0; i < arrayList.Count; i++)
		{
			bool flag = !(arrayList[i].ToString() == string.Empty);
			if (flag)
			{
				try
				{
					num2 = float.Parse(arrayList[i].ToString());
				}
				catch (Exception ex)
				{
					arrayList.RemoveAt(i);
					i--;
					Fun.Log("游戏FPS取值错误，查看这个报错的值:" + arrayList[i].ToString() + " --- " + ex.Message, null);
				}
				bool flag2 = num2 >= 100f;
				if (flag2)
				{
					num++;
				}
			}
		}
		bool flag3 = num == this.Game_Count && (GameRoot.inst.Setting.Video_Class.Effect_Level < 2 || GameRoot.inst.Setting.Video_Class.Anti_Aliasing < 2 || GameRoot.inst.Setting.Video_Class.Anisotropic_Textures < 2);
		if (flag3)
		{
			this.Close_Window_Time = this.Current_Time + GlobalDefine.Close_Setting_Tips_Window_Time + 80f;
			fUIManager.GetInstance().ShowWin("Setting_Tips", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Load_Setting_Tips_UI_Complete), "Up", true);
		}
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x00149D38 File Offset: 0x00147F38
	private void Detection_Reduce_Quality()
	{
		int num = 0;
		bool flag = this.Fight_Fps_Data.Count < 120;
		if (!flag)
		{
			for (int i = 0; i < this.Fight_Fps_Data.Count; i++)
			{
				int num2 = (int)this.Fight_Fps_Data[i];
				bool flag2 = num2 < 55;
				if (flag2)
				{
					num++;
				}
			}
			bool flag3 = num >= this.Fight_Fps_Data.Count / this.Fight_Count && GameRoot.inst.Setting.Video_Class.Effect_Level != 3 && GameRoot.inst.Setting.Video_Class.Effect_Level > 0;
			if (flag3)
			{
				this.Close_Window_Time = this.Current_Time + GlobalDefine.Close_Setting_Tips_Window_Time;
				fUIManager.GetInstance().ShowWin("Setting_Tips", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.Load_Setting_Tips_UI_Complete), "Down", true);
			}
		}
	}

	// Token: 0x04002073 RID: 8307
	private int _Frames;

	// Token: 0x04002074 RID: 8308
	private float _Fps;

	// Token: 0x04002075 RID: 8309
	private float _Reduce_Quality_Time_Interval;

	// Token: 0x04002076 RID: 8310
	private float _Update_Fps_Interval = 0.5f;

	// Token: 0x04002077 RID: 8311
	private float _Update_Last_Time;

	// Token: 0x04002078 RID: 8312
	private ArrayList _Fight_Fps_Data;

	// Token: 0x04002079 RID: 8313
	private float _Current_Time;

	// Token: 0x0400207A RID: 8314
	private float _Close_Window_Time;

	// Token: 0x0400207B RID: 8315
	private int _Current_Display_Efficiency_FPS;

	// Token: 0x0400207C RID: 8316
	private int _Current_Display_Efficiency_Ping;

	// Token: 0x0400207D RID: 8317
	private Texture _Texture;

	// Token: 0x0400207E RID: 8318
	private int _Game_Count = 5;

	// Token: 0x0400207F RID: 8319
	private int _Fight_Count = 6;

	// Token: 0x04002080 RID: 8320
	private bool _Load_State;

	// Token: 0x04002081 RID: 8321
	private bool _GM_State;

	// Token: 0x04002082 RID: 8322
	private GameObject _GM_Camera;

	// Token: 0x04002083 RID: 8323
	private Camera _GM_Camera_Component;

	// Token: 0x04002084 RID: 8324
	private float _Temp_Ping;

	// Token: 0x04002085 RID: 8325
	private string _Temp_Fps_Value = string.Empty;

	// Token: 0x04002086 RID: 8326
	private string _Temp_Ping_Value = string.Empty;

	// Token: 0x04002087 RID: 8327
	private int _Temp_Texture_Widht;

	// Token: 0x04002088 RID: 8328
	private float _Test_Time;
}

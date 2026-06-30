using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class Setting_Entity_Video
{
	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x060023F1 RID: 9201 RVA: 0x00147964 File Offset: 0x00145B64
	// (set) Token: 0x060023F2 RID: 9202 RVA: 0x0014798C File Offset: 0x00145B8C
	public string Graphics_Equipment_Describe
	{
		get
		{
			this._Graphics_Equipment_Describe = PlayerPrefs.GetString("Graphics_Equipment_Describe");
			return this._Graphics_Equipment_Describe;
		}
		set
		{
			this._Graphics_Equipment_Describe = value;
			PlayerPrefs.SetString("Graphics_Equipment_Describe", this._Graphics_Equipment_Describe);
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x060023F3 RID: 9203 RVA: 0x001479A8 File Offset: 0x00145BA8
	// (set) Token: 0x060023F4 RID: 9204 RVA: 0x001479D0 File Offset: 0x00145BD0
	public int Display_Type
	{
		get
		{
			this._Display_Type = PlayerPrefs.GetInt("Video_Display_Type");
			return this._Display_Type;
		}
		set
		{
			this._Display_Type = value;
			PlayerPrefs.SetInt("Video_Display_Type", this._Display_Type);
		}
	}

	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x060023F5 RID: 9205 RVA: 0x001479EC File Offset: 0x00145BEC
	// (set) Token: 0x060023F6 RID: 9206 RVA: 0x00147A40 File Offset: 0x00145C40
	public int Resolution_Ratio
	{
		get
		{
			this._Resolution_Ratio = PlayerPrefs.GetInt("Video_Resolution_Ratio");
			bool flag = this._Resolution_Ratio < 0;
			if (flag)
			{
				this._Resolution_Ratio = this.Get_Default_Resolution_Ratio();
			}
			PlayerPrefs.SetInt("Video_Resolution_Ratio", this._Resolution_Ratio);
			return this._Resolution_Ratio;
		}
		set
		{
			this._Resolution_Ratio = value;
			PlayerPrefs.SetInt("Video_Resolution_Ratio", this._Resolution_Ratio);
		}
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x060023F7 RID: 9207 RVA: 0x00147A5C File Offset: 0x00145C5C
	// (set) Token: 0x060023F8 RID: 9208 RVA: 0x00147A84 File Offset: 0x00145C84
	public int Smooth
	{
		get
		{
			this._Smooth = PlayerPrefs.GetInt("Video_Smooth");
			return this._Smooth;
		}
		set
		{
			this._Smooth = value;
			bool flag = CameraControl.Instance != null;
			if (flag)
			{
				CameraControl.Instance.cameraSpeed = Mathf.Lerp(CameraControl.SmoothMax, CameraControl.SmoothMin, (float)this._Smooth * 0.1f);
			}
			PlayerPrefs.SetInt("Video_Smooth", this._Smooth);
		}
	}

	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x060023F9 RID: 9209 RVA: 0x00147AE4 File Offset: 0x00145CE4
	// (set) Token: 0x060023FA RID: 9210 RVA: 0x00147B18 File Offset: 0x00145D18
	public int VSync
	{
		get
		{
			this._VSync = PlayerPrefs.GetInt("Video_VSync");
			QualitySettings.vSyncCount = this._VSync;
			return this._VSync;
		}
		set
		{
			this._VSync = value;
			PlayerPrefs.SetInt("Video_VSync", this._VSync);
			QualitySettings.vSyncCount = this._VSync;
		}
	}

	// Token: 0x1700049B RID: 1179
	// (get) Token: 0x060023FB RID: 9211 RVA: 0x00147B40 File Offset: 0x00145D40
	// (set) Token: 0x060023FC RID: 9212 RVA: 0x00147B80 File Offset: 0x00145D80
	public int Frame_Limit
	{
		get
		{
			this._Frame_Limit = PlayerPrefs.GetInt("Video_Frame_Limit");
			bool flag = this._Frame_Limit == 0;
			if (flag)
			{
				this.Frame_Limit = 120;
			}
			return this._Frame_Limit;
		}
		set
		{
			this._Frame_Limit = value;
			Application.targetFrameRate = this._Frame_Limit;
			PlayerPrefs.SetInt("Video_Frame_Limit", this._Frame_Limit);
		}
	}

	// Token: 0x1700049C RID: 1180
	// (get) Token: 0x060023FD RID: 9213 RVA: 0x00147BA8 File Offset: 0x00145DA8
	// (set) Token: 0x060023FE RID: 9214 RVA: 0x00147BD0 File Offset: 0x00145DD0
	public int Display_Efficiency_FPS
	{
		get
		{
			this._Display_Efficiency_FPS = PlayerPrefs.GetInt("Video_Display_Efficiency_FPS");
			return this._Display_Efficiency_FPS;
		}
		set
		{
			this._Display_Efficiency_FPS = value;
			GameRoot.inst.Setting_FPS.Current_Display_Efficiency_FPS = this._Display_Efficiency_FPS;
			PlayerPrefs.SetInt("Video_Display_Efficiency_FPS", this._Display_Efficiency_FPS);
		}
	}

	// Token: 0x1700049D RID: 1181
	// (get) Token: 0x060023FF RID: 9215 RVA: 0x00147C04 File Offset: 0x00145E04
	// (set) Token: 0x06002400 RID: 9216 RVA: 0x00147C2C File Offset: 0x00145E2C
	public int Display_Efficiency_Ping
	{
		get
		{
			this._Display_Efficiency_Ping = PlayerPrefs.GetInt("Video_Display_Efficiency_Ping");
			return this._Display_Efficiency_Ping;
		}
		set
		{
			this._Display_Efficiency_Ping = value;
			GameRoot.inst.Setting_FPS.Current_Display_Efficiency_Ping = this._Display_Efficiency_Ping;
			PlayerPrefs.SetInt("Video_Display_Efficiency_Ping", this._Display_Efficiency_Ping);
		}
	}

	// Token: 0x1700049E RID: 1182
	// (get) Token: 0x06002401 RID: 9217 RVA: 0x00147C60 File Offset: 0x00145E60
	// (set) Token: 0x06002402 RID: 9218 RVA: 0x00147C88 File Offset: 0x00145E88
	public int Effect_Level
	{
		get
		{
			this._Effect_Level = PlayerPrefs.GetInt("Video_Effect_Level");
			return this._Effect_Level;
		}
		set
		{
			this.Change_Effect_Level_State = true;
			this._Effect_Level = value;
			PlayerPrefs.SetInt("Video_Effect_Level", this._Effect_Level);
			bool flag = Setting_TipsCon.Instance != null;
			if (flag)
			{
				Setting_TipsCon.Instance.Close();
			}
			this.Use_Data();
			fUIManager.SetGameQuality();
		}
	}

	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x06002403 RID: 9219 RVA: 0x00147CE0 File Offset: 0x00145EE0
	// (set) Token: 0x06002404 RID: 9220 RVA: 0x00147D08 File Offset: 0x00145F08
	public int Pixel_Light_Count
	{
		get
		{
			this._Pixel_Light_Count = PlayerPrefs.GetInt("Video_Pixel_Light_Count");
			return this._Pixel_Light_Count;
		}
		set
		{
			this._Pixel_Light_Count = value;
			PlayerPrefs.SetInt("Video_Pixel_Light_Count", this._Pixel_Light_Count);
		}
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x06002405 RID: 9221 RVA: 0x00147D24 File Offset: 0x00145F24
	// (set) Token: 0x06002406 RID: 9222 RVA: 0x00147D4C File Offset: 0x00145F4C
	public int Texture_Quality
	{
		get
		{
			this._Texture_Quality = PlayerPrefs.GetInt("Video_Texture_Quality");
			return this._Texture_Quality;
		}
		set
		{
			bool flag = false;
			bool flag2 = this._Texture_Quality != value;
			if (flag2)
			{
				flag = true;
			}
			this._Texture_Quality = value;
			PlayerPrefs.SetInt("Video_Texture_Quality", this._Texture_Quality);
			bool flag3 = flag && !this.Change_Effect_Level_State;
			if (flag3)
			{
				this.Effect_Level = 3;
				this.Use_Data();
			}
		}
	}

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x06002407 RID: 9223 RVA: 0x00147DB0 File Offset: 0x00145FB0
	// (set) Token: 0x06002408 RID: 9224 RVA: 0x00147DD8 File Offset: 0x00145FD8
	public int Anisotropic_Textures
	{
		get
		{
			this._Anisotropic_Textures = PlayerPrefs.GetInt("Video_Anisotropic_Textures");
			return this._Anisotropic_Textures;
		}
		set
		{
			bool flag = false;
			bool flag2 = this._Anisotropic_Textures != value;
			if (flag2)
			{
				flag = true;
			}
			this._Anisotropic_Textures = value;
			PlayerPrefs.SetInt("Video_Anisotropic_Textures", this._Anisotropic_Textures);
			bool flag3 = flag && !this.Change_Effect_Level_State;
			if (flag3)
			{
				this.Effect_Level = 3;
				this.Use_Data();
			}
		}
	}

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x06002409 RID: 9225 RVA: 0x00147E3C File Offset: 0x0014603C
	// (set) Token: 0x0600240A RID: 9226 RVA: 0x00147E64 File Offset: 0x00146064
	public int Anti_Aliasing
	{
		get
		{
			this._Anti_Aliasing = PlayerPrefs.GetInt("Video_Anti_Aliasing");
			return this._Anti_Aliasing;
		}
		set
		{
			bool flag = false;
			bool flag2 = this._Anti_Aliasing != value;
			if (flag2)
			{
				flag = true;
			}
			this._Anti_Aliasing = value;
			PlayerPrefs.SetInt("Video_Anti_Aliasing", this._Anti_Aliasing);
			bool flag3 = flag && !this.Change_Effect_Level_State;
			if (flag3)
			{
				this.Effect_Level = 3;
				this.Use_Data();
			}
		}
	}

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x0600240B RID: 9227 RVA: 0x00147EC8 File Offset: 0x001460C8
	// (set) Token: 0x0600240C RID: 9228 RVA: 0x00147EF0 File Offset: 0x001460F0
	public int Realtime_Reflection_Probes
	{
		get
		{
			this._Realtime_Reflection_Probes = PlayerPrefs.GetInt("Video_Realtime_Reflection_Probes");
			return this._Realtime_Reflection_Probes;
		}
		set
		{
			this._Realtime_Reflection_Probes = value;
			PlayerPrefs.SetInt("Video_Realtime_Reflection_Probes", this._Realtime_Reflection_Probes);
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x0600240D RID: 9229 RVA: 0x00147F0C File Offset: 0x0014610C
	// (set) Token: 0x0600240E RID: 9230 RVA: 0x00147F34 File Offset: 0x00146134
	public int Shadow
	{
		get
		{
			this._Shadow = PlayerPrefs.GetInt("Video_Shadow");
			return this._Shadow;
		}
		set
		{
			this._Shadow = value;
			PlayerPrefs.SetInt("Video_Shadow", this._Shadow);
		}
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x0600240F RID: 9231 RVA: 0x00147F50 File Offset: 0x00146150
	// (set) Token: 0x06002410 RID: 9232 RVA: 0x00147F78 File Offset: 0x00146178
	public int Shadow_Resolution
	{
		get
		{
			this._Shadow_Resolution = PlayerPrefs.GetInt("Video_Shadow_Resolution");
			return this._Shadow_Resolution;
		}
		set
		{
			bool flag = false;
			bool flag2 = this._Shadow_Resolution != value;
			if (flag2)
			{
				flag = true;
			}
			this._Shadow_Resolution = value;
			PlayerPrefs.SetInt("Video_Shadow_Resolution", this._Shadow_Resolution);
			bool flag3 = flag && !this.Change_Effect_Level_State;
			if (flag3)
			{
				this.Effect_Level = 3;
				this.Use_Data();
			}
		}
	}

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x06002411 RID: 9233 RVA: 0x00147FDC File Offset: 0x001461DC
	// (set) Token: 0x06002412 RID: 9234 RVA: 0x00148004 File Offset: 0x00146204
	public int Shadow_Projection
	{
		get
		{
			this._Shadow_Projection = PlayerPrefs.GetInt("Video_Shadow_Projection");
			return this._Shadow_Projection;
		}
		set
		{
			this._Shadow_Projection = value;
			PlayerPrefs.SetInt("Video_Shadow_Projection", this._Shadow_Projection);
		}
	}

	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x06002413 RID: 9235 RVA: 0x00148020 File Offset: 0x00146220
	// (set) Token: 0x06002414 RID: 9236 RVA: 0x00148048 File Offset: 0x00146248
	public float Shadow_Distance
	{
		get
		{
			this._Shadow_Distance = PlayerPrefs.GetFloat("Video_Shadow_Distance");
			return this._Shadow_Distance;
		}
		set
		{
			this._Shadow_Distance = value;
			PlayerPrefs.SetFloat("Video_Shadow_Distance", this._Shadow_Distance);
		}
	}

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x06002415 RID: 9237 RVA: 0x00148064 File Offset: 0x00146264
	// (set) Token: 0x06002416 RID: 9238 RVA: 0x0014808C File Offset: 0x0014628C
	public float Shadow_Near_Plane_Offset
	{
		get
		{
			this._Shadow_Near_Plane_Offset = PlayerPrefs.GetFloat("Video_Shadow_Near_Plane_Offset");
			return this._Shadow_Near_Plane_Offset;
		}
		set
		{
			this._Shadow_Near_Plane_Offset = value;
			PlayerPrefs.SetFloat("Video_Shadow_Near_Plane_Offset", this._Shadow_Near_Plane_Offset);
		}
	}

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x06002417 RID: 9239 RVA: 0x001480A8 File Offset: 0x001462A8
	// (set) Token: 0x06002418 RID: 9240 RVA: 0x001480D0 File Offset: 0x001462D0
	public int Shadow_Cascades
	{
		get
		{
			this._Shadow_Cascades = PlayerPrefs.GetInt("Video_Shadow_Cascades");
			return this._Shadow_Cascades;
		}
		set
		{
			this._Shadow_Cascades = value;
			PlayerPrefs.SetInt("Video_Shadow_Cascades", this._Shadow_Cascades);
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x06002419 RID: 9241 RVA: 0x001480EC File Offset: 0x001462EC
	// (set) Token: 0x0600241A RID: 9242 RVA: 0x00148114 File Offset: 0x00146314
	public int Blend_Weights
	{
		get
		{
			this._Blend_Weights = PlayerPrefs.GetInt("Video_Blend_Weights");
			return this._Blend_Weights;
		}
		set
		{
			this._Blend_Weights = value;
			PlayerPrefs.SetInt("Video_Blend_Weights", this._Blend_Weights);
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x0600241B RID: 9243 RVA: 0x00148130 File Offset: 0x00146330
	// (set) Token: 0x0600241C RID: 9244 RVA: 0x00148158 File Offset: 0x00146358
	public float Lod_Bias
	{
		get
		{
			this._Lod_Bias = PlayerPrefs.GetFloat("Video_Lod_Bias");
			return this._Lod_Bias;
		}
		set
		{
			this._Lod_Bias = value;
			PlayerPrefs.SetFloat("Video_Lod_Bias", this._Lod_Bias);
		}
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x0600241D RID: 9245 RVA: 0x00148174 File Offset: 0x00146374
	// (set) Token: 0x0600241E RID: 9246 RVA: 0x0014819C File Offset: 0x0014639C
	public int Maximum_LOD_Level
	{
		get
		{
			this._Maximum_LOD_Level = PlayerPrefs.GetInt("Video_Maximum_LOD_Level");
			return this._Maximum_LOD_Level;
		}
		set
		{
			this._Maximum_LOD_Level = value;
			PlayerPrefs.SetInt("Video_Maximum_LOD_Level", this._Maximum_LOD_Level);
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x0600241F RID: 9247 RVA: 0x001481B8 File Offset: 0x001463B8
	// (set) Token: 0x06002420 RID: 9248 RVA: 0x001481E0 File Offset: 0x001463E0
	public int Particle_Raycast_Budget
	{
		get
		{
			this._Particle_Raycast_Budget = PlayerPrefs.GetInt("Video_Particle_Raycast_Budget");
			return this._Particle_Raycast_Budget;
		}
		set
		{
			this._Particle_Raycast_Budget = value;
			PlayerPrefs.SetInt("Video_Particle_Raycast_Budget", this._Particle_Raycast_Budget);
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x06002421 RID: 9249 RVA: 0x001481FC File Offset: 0x001463FC
	// (set) Token: 0x06002422 RID: 9250 RVA: 0x00148224 File Offset: 0x00146424
	public int Async_Upload_Time_Slice
	{
		get
		{
			this._Async_Upload_Time_Slice = PlayerPrefs.GetInt("Video_Async_Upload_Time_Slice");
			return this._Async_Upload_Time_Slice;
		}
		set
		{
			this._Async_Upload_Time_Slice = value;
			PlayerPrefs.SetInt("Video_Async_Upload_Time_Slice", this._Async_Upload_Time_Slice);
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x06002423 RID: 9251 RVA: 0x00148240 File Offset: 0x00146440
	// (set) Token: 0x06002424 RID: 9252 RVA: 0x00148268 File Offset: 0x00146468
	public int Async_Upload_Buffer_Size
	{
		get
		{
			this._Async_Upload_Buffer_Size = PlayerPrefs.GetInt("Video_Async_Upload_Buffer_Size");
			return this._Async_Upload_Buffer_Size;
		}
		set
		{
			this._Async_Upload_Buffer_Size = value;
			PlayerPrefs.SetInt("Video_Async_Upload_Buffer_Size", this._Async_Upload_Buffer_Size);
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x06002425 RID: 9253 RVA: 0x00148284 File Offset: 0x00146484
	// (set) Token: 0x06002426 RID: 9254 RVA: 0x0014829C File Offset: 0x0014649C
	public bool Change_Effect_Level_State
	{
		get
		{
			return this._Change_Effect_Level_State;
		}
		set
		{
			this._Change_Effect_Level_State = value;
		}
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x001482A8 File Offset: 0x001464A8
	public void Use_Data()
	{
		int vsync = this.VSync;
		bool flag = this.Effect_Level < 3;
		if (flag)
		{
			QualitySettings.SetQualityLevel(this.Effect_Level);
			this.Get_Data();
		}
		else
		{
			QualitySettings.SetQualityLevel(3);
			this.Set_Data();
		}
		this.Change_Effect_Level_State = false;
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x001482FC File Offset: 0x001464FC
	public void Use_Auto_Data()
	{
		string text = this.Graphics_Equipment_Describe.TrimEnd(new char[0]);
		Debug.Log("查看显卡型号1:" + this.Graphics_Equipment_Describe.TrimStart(new char[0]));
		Debug.Log("查看显卡型号2:" + this.Graphics_Equipment_Describe.Trim());
		Debug.Log("查看显卡型号3:" + this.Graphics_Equipment_Describe.TrimEnd(new char[0]));
		bool flag = fGameManager.GetInstance() == null;
		if (!flag)
		{
			bool flag2 = !fGameManager.mloadConfig._Graphic_Card_Ladder_Xml_Data.ContainsKey(text);
			if (flag2)
			{
				this.Effect_Level = 0;
				ArrayList value = new ArrayList
				{
					text,
					0
				};
				fGameManager.SendMsgToHallServer(3010000, new ArrayList
				{
					3010017,
					value
				});
			}
			else
			{
				Graphic_Card_Ladder_Entity graphic_Card_Ladder_Entity = fGameManager.mloadConfig._Graphic_Card_Ladder_Xml_Data[text];
				bool flag3 = graphic_Card_Ladder_Entity.Score < 758f;
				if (flag3)
				{
					this.Effect_Level = 0;
					this.Resolution_Ratio = 0;
				}
				else
				{
					bool flag4 = graphic_Card_Ladder_Entity.Score >= 758f && graphic_Card_Ladder_Entity.Score < 3266f;
					if (flag4)
					{
						this.Effect_Level = 1;
						this.Resolution_Ratio = 1;
					}
					else
					{
						bool flag5 = graphic_Card_Ladder_Entity.Score >= 3266f;
						if (flag5)
						{
							this.Effect_Level = 2;
							this.Resolution_Ratio = -1;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x001484A4 File Offset: 0x001466A4
	private void Get_Data()
	{
		this.Pixel_Light_Count = QualitySettings.pixelLightCount;
		this.Texture_Quality = this._Effect_Level;
		this.Anisotropic_Textures = (int)QualitySettings.anisotropicFiltering;
		this.Anti_Aliasing = QualitySettings.antiAliasing;
		this.Realtime_Reflection_Probes = (QualitySettings.realtimeReflectionProbes ? 1 : 0);
		this.Shadow_Resolution = (int)QualitySettings.shadowResolution;
		this.Shadow_Projection = (int)QualitySettings.shadowProjection;
		this.Shadow_Distance = QualitySettings.shadowDistance;
		this.Shadow_Near_Plane_Offset = QualitySettings.shadowNearPlaneOffset;
		this.Shadow_Cascades = QualitySettings.shadowCascades;
		this.Blend_Weights = (int)QualitySettings.blendWeights;
		this.Lod_Bias = QualitySettings.lodBias;
		this.Maximum_LOD_Level = QualitySettings.maximumLODLevel;
		this.Particle_Raycast_Budget = QualitySettings.particleRaycastBudget;
		this.Async_Upload_Time_Slice = QualitySettings.asyncUploadTimeSlice;
		this.Async_Upload_Buffer_Size = QualitySettings.asyncUploadBufferSize;
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x0014857C File Offset: 0x0014677C
	public void Set_Data()
	{
		QualitySettings.pixelLightCount = this.Pixel_Light_Count;
		QualitySettings.anisotropicFiltering = (AnisotropicFiltering)this.Anisotropic_Textures;
		QualitySettings.antiAliasing = this.Anti_Aliasing;
		QualitySettings.realtimeReflectionProbes = (this.Realtime_Reflection_Probes == 1);
		QualitySettings.shadowResolution = (ShadowResolution)this.Shadow_Resolution;
		QualitySettings.shadowProjection = (ShadowProjection)this.Shadow_Projection;
		QualitySettings.shadowDistance = this.Shadow_Distance;
		QualitySettings.shadowNearPlaneOffset = this.Shadow_Near_Plane_Offset;
		QualitySettings.shadowCascades = this.Shadow_Cascades;
		QualitySettings.blendWeights = (BlendWeights)this.Blend_Weights;
		QualitySettings.lodBias = this.Lod_Bias;
		QualitySettings.maximumLODLevel = this.Maximum_LOD_Level;
		QualitySettings.particleRaycastBudget = this.Particle_Raycast_Budget;
		QualitySettings.asyncUploadTimeSlice = this.Async_Upload_Time_Slice;
		QualitySettings.asyncUploadBufferSize = this.Async_Upload_Buffer_Size;
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x00148644 File Offset: 0x00146844
	public int Get_Default_Resolution_Ratio()
	{
		string text = GameRoot.inst.Setting.Default_Resolution_Width.ToString() + "*" + GameRoot.inst.Setting.Default_Resolution_Width.ToString();
		string a = text;
		int result;
		if (!(a == "1280*720"))
		{
			if (!(a == "1366*768"))
			{
				if (!(a == "1600*900"))
				{
					if (!(a == "1920*1080"))
					{
						bool flag = Screen.currentResolution.width >= 1920;
						if (flag)
						{
							result = 3;
						}
						else
						{
							bool flag2 = Screen.currentResolution.width >= 1600;
							if (flag2)
							{
								result = 2;
							}
							else
							{
								bool flag3 = Screen.currentResolution.width >= 1366;
								if (flag3)
								{
									result = 1;
								}
								else
								{
									result = 0;
								}
							}
						}
					}
					else
					{
						result = 3;
					}
				}
				else
				{
					result = 2;
				}
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x0400204F RID: 8271
	private string _Graphics_Equipment_Describe;

	// Token: 0x04002050 RID: 8272
	private int _Display_Type;

	// Token: 0x04002051 RID: 8273
	private int _Resolution_Ratio = -1;

	// Token: 0x04002052 RID: 8274
	private int _Smooth;

	// Token: 0x04002053 RID: 8275
	private int _VSync;

	// Token: 0x04002054 RID: 8276
	private int _Frame_Limit = 120;

	// Token: 0x04002055 RID: 8277
	private int _Display_Efficiency_FPS;

	// Token: 0x04002056 RID: 8278
	private int _Display_Efficiency_Ping;

	// Token: 0x04002057 RID: 8279
	private int _Effect_Level;

	// Token: 0x04002058 RID: 8280
	private int _Pixel_Light_Count;

	// Token: 0x04002059 RID: 8281
	private int _Texture_Quality;

	// Token: 0x0400205A RID: 8282
	private int _Anisotropic_Textures;

	// Token: 0x0400205B RID: 8283
	private int _Anti_Aliasing;

	// Token: 0x0400205C RID: 8284
	private int _Realtime_Reflection_Probes;

	// Token: 0x0400205D RID: 8285
	private int _Shadow;

	// Token: 0x0400205E RID: 8286
	private int _Shadow_Resolution;

	// Token: 0x0400205F RID: 8287
	private int _Shadow_Projection;

	// Token: 0x04002060 RID: 8288
	private float _Shadow_Distance;

	// Token: 0x04002061 RID: 8289
	private float _Shadow_Near_Plane_Offset;

	// Token: 0x04002062 RID: 8290
	private int _Shadow_Cascades;

	// Token: 0x04002063 RID: 8291
	private int _Blend_Weights;

	// Token: 0x04002064 RID: 8292
	private float _Lod_Bias;

	// Token: 0x04002065 RID: 8293
	private int _Maximum_LOD_Level;

	// Token: 0x04002066 RID: 8294
	private int _Particle_Raycast_Budget;

	// Token: 0x04002067 RID: 8295
	private int _Async_Upload_Time_Slice;

	// Token: 0x04002068 RID: 8296
	private int _Async_Upload_Buffer_Size;

	// Token: 0x04002069 RID: 8297
	private bool _Change_Effect_Level_State = true;
}

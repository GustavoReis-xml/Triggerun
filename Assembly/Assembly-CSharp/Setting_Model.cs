using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class Setting_Model : MonoBehaviour
{
	// Token: 0x170004CB RID: 1227
	// (get) Token: 0x06002473 RID: 9331 RVA: 0x00149E64 File Offset: 0x00148064
	// (set) Token: 0x06002474 RID: 9332 RVA: 0x00149E7C File Offset: 0x0014807C
	public int Selected_Hero_Control_Index
	{
		get
		{
			return this._Selected_Hero_Control_Index;
		}
		set
		{
			this._Selected_Hero_Control_Index = value;
		}
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x00149E88 File Offset: 0x00148088
	private object Get_Setting_Class(string _Selected_Option_Name)
	{
		object result = null;
		if (!(_Selected_Option_Name == "tab_Video"))
		{
			if (!(_Selected_Option_Name == "tab_Voice"))
			{
				if (!(_Selected_Option_Name == "tab_Game"))
				{
					if (!(_Selected_Option_Name == "tab_Control"))
					{
						if (_Selected_Option_Name == "tab_Social")
						{
							result = GameRoot.inst.Setting.Social_Class;
						}
					}
					else
					{
						result = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.Selected_Hero_Control_Index);
					}
				}
				else
				{
					result = GameRoot.inst.Setting.Game_Class;
				}
			}
			else
			{
				result = GameRoot.inst.Setting.Voice_Class;
			}
		}
		else
		{
			result = GameRoot.inst.Setting.Video_Class;
		}
		return result;
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x00149F4C File Offset: 0x0014814C
	private object Get_Temp_Setting_Class(string _Selected_Option_Name)
	{
		object result = null;
		if (!(_Selected_Option_Name == "tab_Video"))
		{
			if (!(_Selected_Option_Name == "tab_Voice"))
			{
				if (!(_Selected_Option_Name == "tab_Game"))
				{
					if (!(_Selected_Option_Name == "tab_Control"))
					{
						if (_Selected_Option_Name == "tab_Social")
						{
							result = GameRoot.inst.Setting.Temp_Social_Class;
						}
					}
					else
					{
						result = GameRoot.inst.Setting.Temp_Hero_Control_Class.Get_Hero_Control_Data(this.Selected_Hero_Control_Index);
					}
				}
				else
				{
					result = GameRoot.inst.Setting.Temp_Game_Class;
				}
			}
			else
			{
				result = GameRoot.inst.Setting.Temp_Voice_Class;
			}
		}
		else
		{
			result = GameRoot.inst.Setting.Temp_Video_Class;
		}
		return result;
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x0014A010 File Offset: 0x00148210
	public object Get_Property_Value(string _Selected_Option_Name, string _Property_Name)
	{
		object @class = GlobalDefine.Setting_Save_Confirm ? this.Get_Temp_Setting_Class(_Selected_Option_Name) : this.Get_Setting_Class(_Selected_Option_Name);
		return GameRoot.inst.Setting.Get_Property_Value(@class, _Property_Name);
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x0014A050 File Offset: 0x00148250
	public bool Set_Property_Value(string _Selected_Option_Name, string _Property_Name, object _Property_Value)
	{
		bool flag = _Selected_Option_Name == "tab_Control";
		if (flag)
		{
			GameRoot.inst.Setting.Hero_Control_Class.Initialize_Hero_Control_Data(this.Selected_Hero_Control_Index);
		}
		object @class = GlobalDefine.Setting_Save_Confirm ? this.Get_Temp_Setting_Class(_Selected_Option_Name) : this.Get_Setting_Class(_Selected_Option_Name);
		return GameRoot.inst.Setting.Set_Property_Value(@class, _Property_Name, _Property_Value);
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x0014A0BC File Offset: 0x001482BC
	public void Initialize_Hero_Control_Data()
	{
		Setting_Entity_Hero_Control setting_Entity_Hero_Control = GlobalDefine.Setting_Save_Confirm ? GameRoot.inst.Setting.Temp_Hero_Control_Class : GameRoot.inst.Setting.Hero_Control_Class;
		setting_Entity_Hero_Control.Initialize_Hero_Control_Data(this.Selected_Hero_Control_Index);
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x0014A104 File Offset: 0x00148304
	public void Restore_Hero_Control_Data()
	{
		Setting_Entity_Hero_Control setting_Entity_Hero_Control = GlobalDefine.Setting_Save_Confirm ? GameRoot.inst.Setting.Temp_Hero_Control_Class : GameRoot.inst.Setting.Hero_Control_Class;
		setting_Entity_Hero_Control.Restore_Hero_Control_Data(this.Selected_Hero_Control_Index);
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x0014A14C File Offset: 0x0014834C
	public void Restore_Video_Setting()
	{
		Setting_Entity_Video setting_Entity_Video = GameRoot.inst.Setting.Video_Class;
		setting_Entity_Video.Display_Type = 0;
		setting_Entity_Video.Resolution_Ratio = setting_Entity_Video.Get_Default_Resolution_Ratio();
		setting_Entity_Video.Smooth = 0;
		setting_Entity_Video.VSync = 0;
		setting_Entity_Video.Frame_Limit = 120;
		setting_Entity_Video.Display_Efficiency_FPS = 0;
		setting_Entity_Video.Display_Efficiency_Ping = 0;
		setting_Entity_Video.Effect_Level = 0;
		setting_Entity_Video.Pixel_Light_Count = 0;
		setting_Entity_Video.Texture_Quality = 0;
		setting_Entity_Video.Anisotropic_Textures = 0;
		setting_Entity_Video.Anti_Aliasing = 0;
		setting_Entity_Video.Realtime_Reflection_Probes = 0;
		setting_Entity_Video.Shadow = 0;
		setting_Entity_Video.Shadow_Resolution = 0;
		setting_Entity_Video.Shadow_Projection = 0;
		setting_Entity_Video.Shadow_Distance = 0f;
		setting_Entity_Video.Shadow_Near_Plane_Offset = 0f;
		setting_Entity_Video.Shadow_Cascades = 0;
		setting_Entity_Video.Blend_Weights = 0;
		setting_Entity_Video.Lod_Bias = 0f;
		setting_Entity_Video.Maximum_LOD_Level = 0;
		setting_Entity_Video.Particle_Raycast_Budget = 0;
		setting_Entity_Video.Async_Upload_Time_Slice = 0;
		setting_Entity_Video.Async_Upload_Buffer_Size = 0;
		setting_Entity_Video = GameRoot.inst.Setting.Temp_Video_Class;
		setting_Entity_Video.Display_Type = 0;
		setting_Entity_Video.Resolution_Ratio = setting_Entity_Video.Get_Default_Resolution_Ratio();
		setting_Entity_Video.VSync = 0;
		setting_Entity_Video.Display_Efficiency_FPS = 0;
		setting_Entity_Video.Display_Efficiency_Ping = 0;
		setting_Entity_Video.Effect_Level = 0;
		setting_Entity_Video.Pixel_Light_Count = 0;
		setting_Entity_Video.Texture_Quality = 0;
		setting_Entity_Video.Anisotropic_Textures = 0;
		setting_Entity_Video.Anti_Aliasing = 0;
		setting_Entity_Video.Realtime_Reflection_Probes = 0;
		setting_Entity_Video.Shadow = 0;
		setting_Entity_Video.Shadow_Resolution = 0;
		setting_Entity_Video.Shadow_Projection = 0;
		setting_Entity_Video.Shadow_Distance = 0f;
		setting_Entity_Video.Shadow_Near_Plane_Offset = 0f;
		setting_Entity_Video.Shadow_Cascades = 0;
		setting_Entity_Video.Blend_Weights = 0;
		setting_Entity_Video.Lod_Bias = 0f;
		setting_Entity_Video.Maximum_LOD_Level = 0;
		setting_Entity_Video.Particle_Raycast_Budget = 0;
		setting_Entity_Video.Async_Upload_Time_Slice = 0;
		setting_Entity_Video.Async_Upload_Buffer_Size = 0;
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x0014A31F File Offset: 0x0014851F
	public void Restore_Voice_Setting()
	{
		GameRoot.inst.Setting.Voice_Class = new Setting_Entity_Voice();
		GameRoot.inst.Setting.Temp_Voice_Class = new Setting_Entity_Voice();
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x0014A34C File Offset: 0x0014854C
	public void Restore_Game_Setting()
	{
		GameRoot.inst.Setting.Game_Class = new Setting_Entity_Game();
		GameRoot.inst.Setting.Temp_Game_Class = new Setting_Entity_Game();
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x0014A379 File Offset: 0x00148579
	public void Restore_Control_Setting()
	{
		GameRoot.inst.Setting.Hero_Control_Class = new Setting_Entity_Hero_Control();
		GameRoot.inst.Setting.Temp_Hero_Control_Class = new Setting_Entity_Hero_Control();
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x0014A3A6 File Offset: 0x001485A6
	public void Restore_Social_Setting()
	{
		GameRoot.inst.Setting.Social_Class = new Setting_Entity_Social();
		GameRoot.inst.Setting.Temp_Social_Class = new Setting_Entity_Social();
	}

	// Token: 0x04002089 RID: 8329
	private int _Selected_Hero_Control_Index;
}

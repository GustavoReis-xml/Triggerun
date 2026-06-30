using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class GlobalDefine
{
	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0008E898 File Offset: 0x0008CA98
	// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0008E8FA File Offset: 0x0008CAFA
	public static Dictionary<int, float> Chariot_Forward_Move_Speed
	{
		get
		{
			bool flag = GlobalDefine._Chariot_Forward_Move_Speed == null;
			if (flag)
			{
				GlobalDefine._Chariot_Forward_Move_Speed = new Dictionary<int, float>();
				GlobalDefine._Chariot_Forward_Move_Speed.Add(1, 1.35f);
				GlobalDefine._Chariot_Forward_Move_Speed.Add(2, 1.8f);
				GlobalDefine._Chariot_Forward_Move_Speed.Add(3, 2.25f);
			}
			return GlobalDefine._Chariot_Forward_Move_Speed;
		}
		set
		{
			GlobalDefine._Chariot_Forward_Move_Speed = value;
		}
	}

	// Token: 0x04000C18 RID: 3096
	public static int Login_Type = 0;

	// Token: 0x04000C19 RID: 3097
	public static string Login_IP = "192.168.0.23";

	// Token: 0x04000C1A RID: 3098
	public static bool Setting_Save_Confirm = false;

	// Token: 0x04000C1B RID: 3099
	public static float Reduce_Quality_Time_Interval = 30f;

	// Token: 0x04000C1C RID: 3100
	public static float Close_Setting_Tips_Window_Time = 10f;

	// Token: 0x04000C1D RID: 3101
	public static int Default_Resolution_Width = 1280;

	// Token: 0x04000C1E RID: 3102
	public static int Default_Resolution_Height = 720;

	// Token: 0x04000C1F RID: 3103
	public const float Default_Jump_Base_Height = 1.2f;

	// Token: 0x04000C20 RID: 3104
	public const float Default_Jump_Extra_Height = 0f;

	// Token: 0x04000C21 RID: 3105
	public const float Multistage_Jump_Speed = 2f;

	// Token: 0x04000C22 RID: 3106
	public const float Super_Jump_Height = 18f;

	// Token: 0x04000C23 RID: 3107
	public const float Energy_Recover_Cycle = 0.1f;

	// Token: 0x04000C24 RID: 3108
	public const float Energy_Recover_Value = 0.09f;

	// Token: 0x04000C25 RID: 3109
	public const float Energy_Deduct_Value = 0.1f;

	// Token: 0x04000C26 RID: 3110
	public const float Energy_Recover_MaxValue = 0.4f;

	// Token: 0x04000C27 RID: 3111
	public const float Dash_Addition_Force = 18f;

	// Token: 0x04000C28 RID: 3112
	public const float Dash_Wait_Time = 0.5f;

	// Token: 0x04000C29 RID: 3113
	public const float Dash_Detection_Range = 300f;

	// Token: 0x04000C2A RID: 3114
	public const float Dash_Detection_Distance = 5f;

	// Token: 0x04000C2B RID: 3115
	public const float Dash_Detection_Angle = 10f;

	// Token: 0x04000C2C RID: 3116
	public const float Dash_Torque = 0.08f;

	// Token: 0x04000C2D RID: 3117
	public const float Dash_Camera_Rotation_Angle_X = 10f;

	// Token: 0x04000C2E RID: 3118
	public const float Stunt_Detection_Range = 2000f;

	// Token: 0x04000C2F RID: 3119
	public const float Shield_Sound_Time_Interval = 30f;

	// Token: 0x04000C30 RID: 3120
	public static float Chariot_Detection_Time = 0.1f;

	// Token: 0x04000C31 RID: 3121
	public static float Chariot_State_Change_Wait_Time = 9f;

	// Token: 0x04000C32 RID: 3122
	private static Dictionary<int, float> _Chariot_Forward_Move_Speed = null;

	// Token: 0x04000C33 RID: 3123
	public static float Chariot_Back_Move_Speed = 0.5f;

	// Token: 0x04000C34 RID: 3124
	public const float Skill_Jump_Height = 14f;

	// Token: 0x04000C35 RID: 3125
	public static float Vampire_Aim_Range = 0.85f;

	// Token: 0x04000C36 RID: 3126
	public static int Vampire_Aim_Distance = 100;

	// Token: 0x04000C37 RID: 3127
	public static float Vampire_Gravity_Offset = 15f;

	// Token: 0x04000C38 RID: 3128
	public static float Vampire_Forward_Speed = 11.2f;

	// Token: 0x04000C39 RID: 3129
	public static float Vampire_Bat_Camera_Distance = 0f;

	// Token: 0x04000C3A RID: 3130
	public static Vector3 Vampire_Bat_Camera_Position = new Vector3(0f, 0.4f, 0f);

	// Token: 0x04000C3B RID: 3131
	public static float Rambo_Aim_Range = 0.93f;

	// Token: 0x04000C3C RID: 3132
	public static int Rambo_Aim_Distance = 100;

	// Token: 0x04000C3D RID: 3133
	public static int Rambo_Aim_FOV = 29;

	// Token: 0x02000586 RID: 1414
	public enum Shield_Break_State_1
	{
		// Token: 0x04002D6B RID: 11627
		Min = 8005,
		// Token: 0x04002D6C RID: 11628
		Max = 10000
	}

	// Token: 0x02000587 RID: 1415
	public enum Shield_Break_State_2
	{
		// Token: 0x04002D6E RID: 11630
		Min = 6005,
		// Token: 0x04002D6F RID: 11631
		Max = 8004
	}

	// Token: 0x02000588 RID: 1416
	public enum Shield_Break_State_3
	{
		// Token: 0x04002D71 RID: 11633
		Min = 4005,
		// Token: 0x04002D72 RID: 11634
		Max = 6004
	}

	// Token: 0x02000589 RID: 1417
	public enum Shield_Break_State_4
	{
		// Token: 0x04002D74 RID: 11636
		Min = 2005,
		// Token: 0x04002D75 RID: 11637
		Max = 4004
	}

	// Token: 0x0200058A RID: 1418
	public enum Shield_Break_State_5
	{
		// Token: 0x04002D77 RID: 11639
		Min = 1,
		// Token: 0x04002D78 RID: 11640
		Max = 2005
	}

	// Token: 0x0200058B RID: 1419
	public enum DAJI_Shield_Break_State_1
	{
		// Token: 0x04002D7A RID: 11642
		Min = 2705,
		// Token: 0x04002D7B RID: 11643
		Max = 4000
	}

	// Token: 0x0200058C RID: 1420
	public enum DAJI_Shield_Break_State_2
	{
		// Token: 0x04002D7D RID: 11645
		Min = 1305,
		// Token: 0x04002D7E RID: 11646
		Max = 2704
	}

	// Token: 0x0200058D RID: 1421
	public enum DAJI_Shield_Break_State_3
	{
		// Token: 0x04002D80 RID: 11648
		Min = 1,
		// Token: 0x04002D81 RID: 11649
		Max = 1304
	}
}

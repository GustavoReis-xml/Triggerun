using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F3 RID: 755
public class Setting_Entity_Control
{
	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06002371 RID: 9073 RVA: 0x001467FC File Offset: 0x001449FC
	// (set) Token: 0x06002372 RID: 9074 RVA: 0x00146814 File Offset: 0x00144A14
	[Description("英雄ID")]
	public int Hero_Index
	{
		get
		{
			return this._Hero_Index;
		}
		set
		{
			this._Hero_Index = value;
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06002373 RID: 9075 RVA: 0x00146820 File Offset: 0x00144A20
	// (set) Token: 0x06002374 RID: 9076 RVA: 0x00146838 File Offset: 0x00144A38
	[Description("鼠标灵敏度")]
	public float Mouse_Sensitivity
	{
		get
		{
			return this._Mouse_Sensitivity;
		}
		set
		{
			this._Mouse_Sensitivity = value;
			bool flag = this._Mouse_Sensitivity < 1f;
			if (flag)
			{
				this._Mouse_Sensitivity = 0f;
			}
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x06002375 RID: 9077 RVA: 0x0014686C File Offset: 0x00144A6C
	// (set) Token: 0x06002376 RID: 9078 RVA: 0x00146884 File Offset: 0x00144A84
	[Description("瞄准鼠标灵敏度")]
	public float Aim_Mouse_Sensitivity
	{
		get
		{
			return this._Aim_Mouse_Sensitivity;
		}
		set
		{
			this._Aim_Mouse_Sensitivity = value;
			bool flag = this._Aim_Mouse_Sensitivity < 1f;
			if (flag)
			{
				this._Aim_Mouse_Sensitivity = 0f;
			}
		}
	}

	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x06002377 RID: 9079 RVA: 0x001468B8 File Offset: 0x00144AB8
	// (set) Token: 0x06002378 RID: 9080 RVA: 0x001468D0 File Offset: 0x00144AD0
	[Description("鼠标是否反转")]
	public int Mouse_Inversion
	{
		get
		{
			return this._Mouse_Inversion;
		}
		set
		{
			this._Mouse_Inversion = value;
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x06002379 RID: 9081 RVA: 0x001468DC File Offset: 0x00144ADC
	// (set) Token: 0x0600237A RID: 9082 RVA: 0x001468F4 File Offset: 0x00144AF4
	[Description("队友生命条")]
	public int Teammate_Health_Bars
	{
		get
		{
			return this._Teammate_Health_Bars;
		}
		set
		{
			this._Teammate_Health_Bars = value;
		}
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x0600237B RID: 9083 RVA: 0x00146900 File Offset: 0x00144B00
	// (set) Token: 0x0600237C RID: 9084 RVA: 0x00146918 File Offset: 0x00144B18
	[Description("向前")]
	public string Key_Forward
	{
		get
		{
			return this._Key_Forward;
		}
		set
		{
			this._Key_Forward = value;
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x0600237D RID: 9085 RVA: 0x00146924 File Offset: 0x00144B24
	// (set) Token: 0x0600237E RID: 9086 RVA: 0x0014693C File Offset: 0x00144B3C
	[Description("向左")]
	public string Key_Left
	{
		get
		{
			return this._Key_Left;
		}
		set
		{
			this._Key_Left = value;
		}
	}

	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x0600237F RID: 9087 RVA: 0x00146948 File Offset: 0x00144B48
	// (set) Token: 0x06002380 RID: 9088 RVA: 0x00146960 File Offset: 0x00144B60
	[Description("向后")]
	public string Key_Backward
	{
		get
		{
			return this._Key_Backward;
		}
		set
		{
			this._Key_Backward = value;
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x06002381 RID: 9089 RVA: 0x0014696C File Offset: 0x00144B6C
	// (set) Token: 0x06002382 RID: 9090 RVA: 0x00146984 File Offset: 0x00144B84
	[Description("向右")]
	public string Key_Right
	{
		get
		{
			return this._Key_Right;
		}
		set
		{
			this._Key_Right = value;
		}
	}

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x06002383 RID: 9091 RVA: 0x00146990 File Offset: 0x00144B90
	// (set) Token: 0x06002384 RID: 9092 RVA: 0x001469A8 File Offset: 0x00144BA8
	[Description("跳跃")]
	public string Key_Jump
	{
		get
		{
			return this._Key_Jump;
		}
		set
		{
			this._Key_Jump = value;
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06002385 RID: 9093 RVA: 0x001469B4 File Offset: 0x00144BB4
	// (set) Token: 0x06002386 RID: 9094 RVA: 0x001469CC File Offset: 0x00144BCC
	[Description("技能1")]
	public string Key_Skill_1
	{
		get
		{
			return this._Key_Skill_1;
		}
		set
		{
			this._Key_Skill_1 = value;
		}
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06002387 RID: 9095 RVA: 0x001469D8 File Offset: 0x00144BD8
	// (set) Token: 0x06002388 RID: 9096 RVA: 0x001469F0 File Offset: 0x00144BF0
	[Description("技能2")]
	public string Key_Skill_2
	{
		get
		{
			return this._Key_Skill_2;
		}
		set
		{
			this._Key_Skill_2 = value;
		}
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06002389 RID: 9097 RVA: 0x001469FC File Offset: 0x00144BFC
	// (set) Token: 0x0600238A RID: 9098 RVA: 0x00146A14 File Offset: 0x00144C14
	[Description("技能3")]
	public string Key_Skill_3
	{
		get
		{
			return this._Key_Skill_3;
		}
		set
		{
			this._Key_Skill_3 = value;
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x0600238B RID: 9099 RVA: 0x00146A20 File Offset: 0x00144C20
	// (set) Token: 0x0600238C RID: 9100 RVA: 0x00146A38 File Offset: 0x00144C38
	[Description("呼叫武器1")]
	public string Key_Call_Weapon_1
	{
		get
		{
			return this._Key_Call_Weapon_1;
		}
		set
		{
			this._Key_Call_Weapon_1 = value;
		}
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x0600238D RID: 9101 RVA: 0x00146A44 File Offset: 0x00144C44
	// (set) Token: 0x0600238E RID: 9102 RVA: 0x00146A5C File Offset: 0x00144C5C
	[Description("呼叫武器2")]
	public string Key_Call_Weapon_2
	{
		get
		{
			return this._Key_Call_Weapon_2;
		}
		set
		{
			this._Key_Call_Weapon_2 = value;
		}
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x0600238F RID: 9103 RVA: 0x00146A68 File Offset: 0x00144C68
	// (set) Token: 0x06002390 RID: 9104 RVA: 0x00146A80 File Offset: 0x00144C80
	[Description("换弹")]
	public string Key_Reload
	{
		get
		{
			return this._Key_Reload;
		}
		set
		{
			this._Key_Reload = value;
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06002391 RID: 9105 RVA: 0x00146A8C File Offset: 0x00144C8C
	// (set) Token: 0x06002392 RID: 9106 RVA: 0x00146AA4 File Offset: 0x00144CA4
	[Description("使用武器1")]
	public string Key_Use_Weapon_1
	{
		get
		{
			return this._Key_Use_Weapon_1;
		}
		set
		{
			this._Key_Use_Weapon_1 = value;
		}
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06002393 RID: 9107 RVA: 0x00146AB0 File Offset: 0x00144CB0
	// (set) Token: 0x06002394 RID: 9108 RVA: 0x00146AC8 File Offset: 0x00144CC8
	[Description("使用武器2")]
	public string Key_Use_Weapon_2
	{
		get
		{
			return this._Key_Use_Weapon_2;
		}
		set
		{
			this._Key_Use_Weapon_2 = value;
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06002395 RID: 9109 RVA: 0x00146AD4 File Offset: 0x00144CD4
	// (set) Token: 0x06002396 RID: 9110 RVA: 0x00146AEC File Offset: 0x00144CEC
	[Description("近战攻击")]
	public string Key_Melee_Attack
	{
		get
		{
			return this._Key_Melee_Attack;
		}
		set
		{
			this._Key_Melee_Attack = value;
		}
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06002397 RID: 9111 RVA: 0x00146AF8 File Offset: 0x00144CF8
	// (set) Token: 0x06002398 RID: 9112 RVA: 0x00146B10 File Offset: 0x00144D10
	[Description("呼叫上一个武器")]
	public string Key_Call_Prev_Weapon
	{
		get
		{
			return this._Key_Call_Prev_Weapon;
		}
		set
		{
			this._Key_Call_Prev_Weapon = value;
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06002399 RID: 9113 RVA: 0x00146B1C File Offset: 0x00144D1C
	// (set) Token: 0x0600239A RID: 9114 RVA: 0x00146B34 File Offset: 0x00144D34
	[Description("呼叫下一个武器")]
	public string Key_Call_Next_Weapon
	{
		get
		{
			return this._Key_Call_Next_Weapon;
		}
		set
		{
			this._Key_Call_Next_Weapon = value;
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x0600239B RID: 9115 RVA: 0x00146B40 File Offset: 0x00144D40
	// (set) Token: 0x0600239C RID: 9116 RVA: 0x00146B58 File Offset: 0x00144D58
	[Description("更换英雄")]
	public string Key_Replace_Hero
	{
		get
		{
			return this._Key_Replace_Hero;
		}
		set
		{
			this._Key_Replace_Hero = value;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x0600239D RID: 9117 RVA: 0x00146B64 File Offset: 0x00144D64
	// (set) Token: 0x0600239E RID: 9118 RVA: 0x00146B7C File Offset: 0x00144D7C
	[Description("得分界面")]
	public string Key_Score_Window
	{
		get
		{
			return this._Key_Score_Window;
		}
		set
		{
			this._Key_Score_Window = value;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x0600239F RID: 9119 RVA: 0x00146B88 File Offset: 0x00144D88
	// (set) Token: 0x060023A0 RID: 9120 RVA: 0x00146BA0 File Offset: 0x00144DA0
	[Description("互动菜单")]
	public string Key_Interact_Menu
	{
		get
		{
			return this._Key_Interact_Menu;
		}
		set
		{
			this._Key_Interact_Menu = value;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00146BAC File Offset: 0x00144DAC
	// (set) Token: 0x060023A2 RID: 9122 RVA: 0x00146BC4 File Offset: 0x00144DC4
	[Description("互动-感谢")]
	public string Key_Interact_Thank
	{
		get
		{
			return this._Key_Interact_Thank;
		}
		set
		{
			this._Key_Interact_Thank = value;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00146BD0 File Offset: 0x00144DD0
	// (set) Token: 0x060023A4 RID: 9124 RVA: 0x00146BE8 File Offset: 0x00144DE8
	[Description("互动-表情")]
	public string Key_Interact_Expression
	{
		get
		{
			return this._Key_Interact_Expression;
		}
		set
		{
			this._Key_Interact_Expression = value;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00146BF4 File Offset: 0x00144DF4
	// (set) Token: 0x060023A6 RID: 9126 RVA: 0x00146C0C File Offset: 0x00144E0C
	[Description("互动-收到")]
	public string Key_Interact_Roger
	{
		get
		{
			return this._Key_Interact_Roger;
		}
		set
		{
			this._Key_Interact_Roger = value;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00146C18 File Offset: 0x00144E18
	// (set) Token: 0x060023A8 RID: 9128 RVA: 0x00146C30 File Offset: 0x00144E30
	[Description("互动-你好")]
	public string Key_Interact_Hello
	{
		get
		{
			return this._Key_Interact_Hello;
		}
		set
		{
			this._Key_Interact_Hello = value;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00146C3C File Offset: 0x00144E3C
	// (set) Token: 0x060023AA RID: 9130 RVA: 0x00146C54 File Offset: 0x00144E54
	[Description("互动-定制语音")]
	public string Key_Interact_Custom_Voice_Sound
	{
		get
		{
			return this._Key_Interact_Custom_Voice_Sound;
		}
		set
		{
			this._Key_Interact_Custom_Voice_Sound = value;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x060023AB RID: 9131 RVA: 0x00146C60 File Offset: 0x00144E60
	// (set) Token: 0x060023AC RID: 9132 RVA: 0x00146C78 File Offset: 0x00144E78
	[Description("互动-需要帮助")]
	public string Key_Interact_Need_Help
	{
		get
		{
			return this._Key_Interact_Need_Help;
		}
		set
		{
			this._Key_Interact_Need_Help = value;
		}
	}

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x060023AD RID: 9133 RVA: 0x00146C84 File Offset: 0x00144E84
	// (set) Token: 0x060023AE RID: 9134 RVA: 0x00146C9C File Offset: 0x00144E9C
	[Description("互动-终极技能状态")]
	public string Key_Interact_Ultimate_Skill_State
	{
		get
		{
			return this._Key_Interact_Ultimate_Skill_State;
		}
		set
		{
			this._Key_Interact_Ultimate_Skill_State = value;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x060023AF RID: 9135 RVA: 0x00146CA8 File Offset: 0x00144EA8
	// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00146CC0 File Offset: 0x00144EC0
	[Description("互动-集合")]
	public string Key_Interact_Collection
	{
		get
		{
			return this._Key_Interact_Collection;
		}
		set
		{
			this._Key_Interact_Collection = value;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00146CCC File Offset: 0x00144ECC
	// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00146CE4 File Offset: 0x00144EE4
	[Description("互动-回复密语")]
	public string Key_Reply_Whisper
	{
		get
		{
			return this._Key_Reply_Whisper;
		}
		set
		{
			this._Key_Reply_Whisper = value;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00146CF0 File Offset: 0x00144EF0
	// (set) Token: 0x060023B4 RID: 9140 RVA: 0x00146D08 File Offset: 0x00144F08
	[Description("显示聊天窗口")]
	public string Key_Display_Chat_Window
	{
		get
		{
			return this._Key_Display_Chat_Window;
		}
		set
		{
			this._Key_Display_Chat_Window = value;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00146D14 File Offset: 0x00144F14
	// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00146D2C File Offset: 0x00144F2C
	[Description("隐藏聊天窗口")]
	public string Key_Hidden_Chat_Window
	{
		get
		{
			return this._Key_Hidden_Chat_Window;
		}
		set
		{
			this._Key_Hidden_Chat_Window = value;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00146D38 File Offset: 0x00144F38
	// (set) Token: 0x060023B8 RID: 9144 RVA: 0x00146D50 File Offset: 0x00144F50
	[Description("社交菜单")]
	public string Key_Social_Menu
	{
		get
		{
			return this._Key_Social_Menu;
		}
		set
		{
			this._Key_Social_Menu = value;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x060023B9 RID: 9145 RVA: 0x00146D5C File Offset: 0x00144F5C
	// (set) Token: 0x060023BA RID: 9146 RVA: 0x00146D74 File Offset: 0x00144F74
	[Description("社交菜单频道")]
	public string Key_Social_Menu_Channel
	{
		get
		{
			return this._Key_Social_Menu_Channel;
		}
		set
		{
			this._Key_Social_Menu_Channel = value;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x060023BB RID: 9147 RVA: 0x00146D80 File Offset: 0x00144F80
	// (set) Token: 0x060023BC RID: 9148 RVA: 0x00146D98 File Offset: 0x00144F98
	[Description("按键语音聊天")]
	public string Key_Voice_Chat
	{
		get
		{
			return this._Key_Voice_Chat;
		}
		set
		{
			this._Key_Voice_Chat = value;
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x060023BD RID: 9149 RVA: 0x00146DA4 File Offset: 0x00144FA4
	// (set) Token: 0x060023BE RID: 9150 RVA: 0x00146DBC File Offset: 0x00144FBC
	[Description("麦克风静音开关")]
	public string Key_Microphone_Mute_On_Off
	{
		get
		{
			return this._Key_Microphone_Mute_On_Off;
		}
		set
		{
			this._Key_Microphone_Mute_On_Off = value;
		}
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x060023BF RID: 9151 RVA: 0x00146DC8 File Offset: 0x00144FC8
	// (set) Token: 0x060023C0 RID: 9152 RVA: 0x00146DE0 File Offset: 0x00144FE0
	[Description("队伍聊天")]
	public string Key_Country_Chat
	{
		get
		{
			return this._Key_Country_Chat;
		}
		set
		{
			this._Key_Country_Chat = value;
		}
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x060023C1 RID: 9153 RVA: 0x00146DEC File Offset: 0x00144FEC
	// (set) Token: 0x060023C2 RID: 9154 RVA: 0x00146E04 File Offset: 0x00145004
	[Description("喷漆")]
	public string Key_Spray_Paint
	{
		get
		{
			return this._Key_Spray_Paint;
		}
		set
		{
			this._Key_Spray_Paint = value;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x060023C3 RID: 9155 RVA: 0x00146E10 File Offset: 0x00145010
	// (set) Token: 0x060023C4 RID: 9156 RVA: 0x00146E28 File Offset: 0x00145028
	[Description("增加品质")]
	public string Key_Improve_Quality
	{
		get
		{
			return this._Key_Improve_Quality;
		}
		set
		{
			this._Key_Improve_Quality = value;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00146E34 File Offset: 0x00145034
	// (set) Token: 0x060023C6 RID: 9158 RVA: 0x00146E4C File Offset: 0x0014504C
	[Description("降低品质")]
	public string Key_Reduce_Quality
	{
		get
		{
			return this._Key_Reduce_Quality;
		}
		set
		{
			this._Key_Reduce_Quality = value;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00146E58 File Offset: 0x00145058
	// (set) Token: 0x060023C8 RID: 9160 RVA: 0x00146E70 File Offset: 0x00145070
	[Description("查看英雄信息")]
	public string Key_Check_Hero_Information
	{
		get
		{
			return this._Key_Check_Hero_Information;
		}
		set
		{
			this._Key_Check_Hero_Information = value;
		}
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x060023C9 RID: 9161 RVA: 0x00146E7C File Offset: 0x0014507C
	// (set) Token: 0x060023CA RID: 9162 RVA: 0x00146E94 File Offset: 0x00145094
	[Description("打开菜单")]
	public string Key_Open_Menu
	{
		get
		{
			return this._Key_Open_Menu;
		}
		set
		{
			this._Key_Open_Menu = value;
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x060023CB RID: 9163 RVA: 0x00146EA0 File Offset: 0x001450A0
	// (set) Token: 0x060023CC RID: 9164 RVA: 0x00146EB8 File Offset: 0x001450B8
	[Description("语音频道")]
	public string Key_Open_Chat_Window
	{
		get
		{
			return this._Key_Open_Chat_Window;
		}
		set
		{
			this._Key_Open_Chat_Window = value;
		}
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x060023CD RID: 9165 RVA: 0x00146EC4 File Offset: 0x001450C4
	// (set) Token: 0x060023CE RID: 9166 RVA: 0x00146EDC File Offset: 0x001450DC
	[Description("小队语音频道开关")]
	public string Key_Team_Chat_On_Off
	{
		get
		{
			return this._Key_Team_Chat_On_Off;
		}
		set
		{
			this._Key_Team_Chat_On_Off = value;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x060023CF RID: 9167 RVA: 0x00146EE8 File Offset: 0x001450E8
	// (set) Token: 0x060023D0 RID: 9168 RVA: 0x00146F00 File Offset: 0x00145100
	[Description("比赛语音频道开关")]
	public string Key_Game_Chat_On_Off
	{
		get
		{
			return this._Key_Game_Chat_On_Off;
		}
		set
		{
			this._Key_Game_Chat_On_Off = value;
		}
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x00146F0C File Offset: 0x0014510C
	public ArrayList Update_Value(string _Key, string _Value, Dictionary<string, GameObject> _Control_Content_Structure)
	{
		ArrayList result = null;
		string[] array = _Key.Split(new char[]
		{
			'_'
		});
		bool flag = array[0].ToString() == "Key";
		if (flag)
		{
			try
			{
				result = this.Update_Key_Value(_Key, _Value, _Control_Content_Structure);
			}
			catch (Exception ex)
			{
				Fun.LogError(string.Concat(new string[]
				{
					"设置系统报错了，为了方便调试，查看报错原因。\n + ",
					ex.Message,
					"\n 参数[_Key:",
					_Key,
					" - _Value:",
					_Value,
					" - _Control_Content_Structure:",
					_Control_Content_Structure.Keys.ToString(),
					"]"
				}), null);
			}
		}
		else
		{
			result = this.Update_Other_Value(_Key, _Value);
		}
		return result;
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x00146FDC File Offset: 0x001451DC
	private ArrayList Update_Key_Value(string _Key, string _Value, Dictionary<string, GameObject> _Control_Content_Structure)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = false;
		PropertyInfo propertyInfo = null;
		string text = _Control_Content_Structure[_Key].transform.Find("lbl_Describe").GetComponent<Text>().text;
		string text2 = GameRoot.inst.Setting.Get_Key_Describe(_Value);
		string value = text2 + " 已经绑定至 " + text;
		ArrayList control_Key_Field_Name_Array = GameRoot.inst.Setting.Control_Key_Field_Name_Array;
		for (int i = 0; i < control_Key_Field_Name_Array.Count; i++)
		{
			propertyInfo = (PropertyInfo)control_Key_Field_Name_Array[i];
			string a = propertyInfo.GetValue(this, null).ToString();
			bool flag2 = a == _Value && _Key != propertyInfo.Name;
			if (flag2)
			{
				propertyInfo.SetValue(this, string.Empty, null);
				fUIManager.GetInstance().Setting_Modify_Key_Complete(propertyInfo.Name, string.Empty);
				value = string.Concat(new string[]
				{
					text2,
					" 已经与 “",
					_Control_Content_Structure[propertyInfo.Name].transform.Find("lbl_Describe").GetComponent<Text>().text,
					"” 解除绑定，现在与 “",
					text,
					"” 绑定"
				});
				flag = true;
				break;
			}
		}
		arrayList.Add(flag);
		arrayList.Add(value);
		arrayList.Add(propertyInfo.Name);
		return arrayList;
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x0014716C File Offset: 0x0014536C
	private ArrayList Update_Other_Value(string _Key, string _Value)
	{
		return null;
	}

	// Token: 0x04002016 RID: 8214
	private int _Hero_Index;

	// Token: 0x04002017 RID: 8215
	private float _Mouse_Sensitivity = 40f;

	// Token: 0x04002018 RID: 8216
	private float _Aim_Mouse_Sensitivity = 25f;

	// Token: 0x04002019 RID: 8217
	private int _Mouse_Inversion;

	// Token: 0x0400201A RID: 8218
	private int _Teammate_Health_Bars = 1;

	// Token: 0x0400201B RID: 8219
	private string _Key_Forward = "W";

	// Token: 0x0400201C RID: 8220
	private string _Key_Left = "A";

	// Token: 0x0400201D RID: 8221
	private string _Key_Backward = "S";

	// Token: 0x0400201E RID: 8222
	private string _Key_Right = "D";

	// Token: 0x0400201F RID: 8223
	private string _Key_Jump = "Space";

	// Token: 0x04002020 RID: 8224
	private string _Key_Skill_1 = "LeftShift";

	// Token: 0x04002021 RID: 8225
	private string _Key_Skill_2 = "E";

	// Token: 0x04002022 RID: 8226
	private string _Key_Skill_3 = "Q";

	// Token: 0x04002023 RID: 8227
	private string _Key_Call_Weapon_1 = "Alpha1";

	// Token: 0x04002024 RID: 8228
	private string _Key_Call_Weapon_2 = "Alpha2";

	// Token: 0x04002025 RID: 8229
	private string _Key_Reload = "R";

	// Token: 0x04002026 RID: 8230
	private string _Key_Use_Weapon_1 = "Mouse0";

	// Token: 0x04002027 RID: 8231
	private string _Key_Use_Weapon_2 = "Mouse1";

	// Token: 0x04002028 RID: 8232
	private string _Key_Melee_Attack = "F";

	// Token: 0x04002029 RID: 8233
	private string _Key_Call_Prev_Weapon = "Mouse2";

	// Token: 0x0400202A RID: 8234
	private string _Key_Call_Next_Weapon = "Mouse3";

	// Token: 0x0400202B RID: 8235
	private string _Key_Replace_Hero = "H";

	// Token: 0x0400202C RID: 8236
	private string _Key_Score_Window = "Tab";

	// Token: 0x0400202D RID: 8237
	private string _Key_Interact_Menu = "C";

	// Token: 0x0400202E RID: 8238
	private string _Key_Interact_Thank = "Keypad1";

	// Token: 0x0400202F RID: 8239
	private string _Key_Interact_Expression = "Keypad2";

	// Token: 0x04002030 RID: 8240
	private string _Key_Interact_Roger = "Keypad3";

	// Token: 0x04002031 RID: 8241
	private string _Key_Interact_Hello = "Keypad4";

	// Token: 0x04002032 RID: 8242
	private string _Key_Interact_Custom_Voice_Sound = "Keypad5";

	// Token: 0x04002033 RID: 8243
	private string _Key_Interact_Need_Help = "Keypad6";

	// Token: 0x04002034 RID: 8244
	private string _Key_Interact_Ultimate_Skill_State = "Keypad7";

	// Token: 0x04002035 RID: 8245
	private string _Key_Interact_Collection = "Keypad8";

	// Token: 0x04002036 RID: 8246
	private string _Key_Reply_Whisper = "Backspace";

	// Token: 0x04002037 RID: 8247
	private string _Key_Display_Chat_Window = "Return";

	// Token: 0x04002038 RID: 8248
	private string _Key_Hidden_Chat_Window = "LeftShift+LeftCtrl+C";

	// Token: 0x04002039 RID: 8249
	private string _Key_Social_Menu = "O";

	// Token: 0x0400203A RID: 8250
	private string _Key_Social_Menu_Channel = "P";

	// Token: 0x0400203B RID: 8251
	private string _Key_Voice_Chat = "BackQuote";

	// Token: 0x0400203C RID: 8252
	private string _Key_Microphone_Mute_On_Off = "O";

	// Token: 0x0400203D RID: 8253
	private string _Key_Country_Chat = "Y";

	// Token: 0x0400203E RID: 8254
	private string _Key_Spray_Paint = "T";

	// Token: 0x0400203F RID: 8255
	private string _Key_Improve_Quality = "KeypadPlus";

	// Token: 0x04002040 RID: 8256
	private string _Key_Reduce_Quality = "KeypadMinus";

	// Token: 0x04002041 RID: 8257
	private string _Key_Check_Hero_Information = "F1";

	// Token: 0x04002042 RID: 8258
	private string _Key_Open_Menu = "Escape";

	// Token: 0x04002043 RID: 8259
	private string _Key_Open_Chat_Window = "P";

	// Token: 0x04002044 RID: 8260
	private string _Key_Team_Chat_On_Off = "N";

	// Token: 0x04002045 RID: 8261
	private string _Key_Game_Chat_On_Off = "V";
}

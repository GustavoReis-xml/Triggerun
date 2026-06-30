using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class Arena_Score_Message_Handle
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000B0 RID: 176 RVA: 0x000057D4 File Offset: 0x000039D4
	public static Arena_Score_Message_Handle Instantiation
	{
		get
		{
			bool flag = Arena_Score_Message_Handle._Instantiation == null;
			if (flag)
			{
				Arena_Score_Message_Handle._Instantiation = new Arena_Score_Message_Handle();
			}
			return Arena_Score_Message_Handle._Instantiation;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005804 File Offset: 0x00003A04
	// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000581C File Offset: 0x00003A1C
	public GameObject UI_Object
	{
		get
		{
			return this._UI_Object;
		}
		set
		{
			this._UI_Object = value;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005828 File Offset: 0x00003A28
	// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005840 File Offset: 0x00003A40
	public Arena_Score_Controller Controller_Component
	{
		get
		{
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000584C File Offset: 0x00003A4C
	// (set) Token: 0x060000B6 RID: 182 RVA: 0x00005864 File Offset: 0x00003A64
	public bool Load_User_Data_State
	{
		get
		{
			return this._Load_User_Data_State;
		}
		set
		{
			this._Load_User_Data_State = value;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005870 File Offset: 0x00003A70
	// (set) Token: 0x060000B8 RID: 184 RVA: 0x00005888 File Offset: 0x00003A88
	public ArrayList Load_User_Data
	{
		get
		{
			return this._Load_User_Data;
		}
		set
		{
			this._Load_User_Data = value;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005894 File Offset: 0x00003A94
	// (set) Token: 0x060000BA RID: 186 RVA: 0x000058AC File Offset: 0x00003AAC
	public bool Load_Fight_Prepare_Data_State
	{
		get
		{
			return this._Load_Fight_Prepare_Data_State;
		}
		set
		{
			this._Load_Fight_Prepare_Data_State = value;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000BB RID: 187 RVA: 0x000058B8 File Offset: 0x00003AB8
	// (set) Token: 0x060000BC RID: 188 RVA: 0x000058D0 File Offset: 0x00003AD0
	public ArrayList Load_Fight_Prepare_Data
	{
		get
		{
			return this._Load_Fight_Prepare_Data;
		}
		set
		{
			this._Load_Fight_Prepare_Data = value;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000BD RID: 189 RVA: 0x000058DC File Offset: 0x00003ADC
	// (set) Token: 0x060000BE RID: 190 RVA: 0x000058F4 File Offset: 0x00003AF4
	public bool Load_Fight_Start_Data_State
	{
		get
		{
			return this._Load_Fight_Start_Data_State;
		}
		set
		{
			this._Load_Fight_Start_Data_State = value;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000BF RID: 191 RVA: 0x00005900 File Offset: 0x00003B00
	// (set) Token: 0x060000C0 RID: 192 RVA: 0x00005918 File Offset: 0x00003B18
	public bool Load_Round_End_Data_State
	{
		get
		{
			return this._Load_Round_End_Data_State;
		}
		set
		{
			this._Load_Round_End_Data_State = value;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005924 File Offset: 0x00003B24
	// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000593C File Offset: 0x00003B3C
	public ArrayList Load_Round_End_Data
	{
		get
		{
			return this._Load_Round_End_Data;
		}
		set
		{
			this._Load_Round_End_Data = value;
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00005948 File Offset: 0x00003B48
	public void Message_Handle(int _Net_ID, ArrayList _Parameters)
	{
		switch (_Net_ID)
		{
		case 110301:
		{
			bool flag = this.UI_Object == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("Arena_Score", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.Load_UI_Complete), null, true);
			}
			else
			{
				this.Load_UI_Complete(string.Empty, this.UI_Object, null);
				this.UI_Object.SetActive(true);
			}
			break;
		}
		case 110302:
		{
			bool flag2 = this.Controller_Component != null;
			if (flag2)
			{
				this.Controller_Component.Display_Fight_Data(_Parameters);
			}
			else
			{
				this.Load_User_Data = _Parameters;
				this.Load_User_Data_State = true;
			}
			break;
		}
		case 110303:
		{
			bool flag3 = this.Controller_Component != null;
			if (flag3)
			{
				this.Controller_Component.Fight_Start();
			}
			else
			{
				this.Load_Fight_Start_Data_State = true;
			}
			break;
		}
		case 110304:
			fUIManager.GetInstance().GotoHall(0);
			break;
		case 110305:
		{
			bool flag4 = !(this.Controller_Component == null);
			if (flag4)
			{
				this.Controller_Component.Countdown_Data(_Parameters);
			}
			break;
		}
		case 110306:
		{
			bool flag5 = !(this.Controller_Component == null);
			if (flag5)
			{
				this.Controller_Component.Fight_Result(_Parameters);
			}
			break;
		}
		case 110307:
		{
			bool flag6 = this.Controller_Component != null;
			if (flag6)
			{
				this.Controller_Component.Fight_Prepare(_Parameters);
			}
			else
			{
				this.Load_Fight_Prepare_Data = _Parameters;
				this.Load_Fight_Prepare_Data_State = true;
			}
			break;
		}
		case 110308:
		{
			bool flag7 = this.Controller_Component != null;
			if (flag7)
			{
				this.Controller_Component.Round_End(_Parameters);
			}
			else
			{
				this.Load_Round_End_Data = _Parameters;
				this.Load_Round_End_Data_State = true;
			}
			break;
		}
		case 110309:
		{
			bool flag8 = this.Controller_Component != null;
			if (flag8)
			{
				this.Controller_Component.Round_Start();
			}
			break;
		}
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00005B40 File Offset: 0x00003D40
	private void Load_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		this.UI_Object = _UI_Object;
		this.Controller_Component = this.UI_Object.GetComponent<Arena_Score_Controller>();
		bool flag = this.Controller_Component == null;
		if (flag)
		{
			this.Controller_Component = this.UI_Object.AddComponent<Arena_Score_Controller>();
		}
		bool flag2 = fGameManager.GetInstance().nNowScnID < 1 || fGameManager.Heroplayer == null || MatchManager.GetInstance().m_pFbData.nType != 7;
		if (flag2)
		{
			Arena_ScoreCon.inst.Close();
		}
		else
		{
			bool load_User_Data_State = this.Load_User_Data_State;
			if (load_User_Data_State)
			{
				this.Load_User_Data_State = false;
				this.Controller_Component.Display_Fight_Data(this.Load_User_Data);
			}
			bool load_Fight_Prepare_Data_State = this.Load_Fight_Prepare_Data_State;
			if (load_Fight_Prepare_Data_State)
			{
				this.Load_Fight_Prepare_Data_State = false;
				this.Controller_Component.Fight_Prepare(this.Load_Fight_Prepare_Data);
			}
			bool load_Fight_Start_Data_State = this.Load_Fight_Start_Data_State;
			if (load_Fight_Start_Data_State)
			{
				this.Load_Fight_Start_Data_State = false;
				this.Controller_Component.Fight_Start();
			}
			bool load_Round_End_Data_State = this.Load_Round_End_Data_State;
			if (load_Round_End_Data_State)
			{
				this.Load_Round_End_Data_State = true;
				this.Controller_Component.Round_End(this.Load_Round_End_Data);
			}
			this.Controller_Component.Initialize();
		}
	}

	// Token: 0x040000A4 RID: 164
	private static Arena_Score_Message_Handle _Instantiation;

	// Token: 0x040000A5 RID: 165
	private GameObject _UI_Object;

	// Token: 0x040000A6 RID: 166
	private Arena_Score_Controller _Controller_Component;

	// Token: 0x040000A7 RID: 167
	private bool _Load_User_Data_State;

	// Token: 0x040000A8 RID: 168
	private ArrayList _Load_User_Data;

	// Token: 0x040000A9 RID: 169
	private bool _Load_Fight_Prepare_Data_State;

	// Token: 0x040000AA RID: 170
	private ArrayList _Load_Fight_Prepare_Data;

	// Token: 0x040000AB RID: 171
	private bool _Load_Fight_Start_Data_State;

	// Token: 0x040000AC RID: 172
	private bool _Load_Round_End_Data_State;

	// Token: 0x040000AD RID: 173
	private ArrayList _Load_Round_End_Data;
}

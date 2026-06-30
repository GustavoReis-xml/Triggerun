using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class Arena_Message_Handle
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000065 RID: 101 RVA: 0x000048A8 File Offset: 0x00002AA8
	public static Arena_Message_Handle Instantiation
	{
		get
		{
			bool flag = Arena_Message_Handle._Instantiation == null;
			if (flag)
			{
				Arena_Message_Handle._Instantiation = new Arena_Message_Handle();
			}
			return Arena_Message_Handle._Instantiation;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000066 RID: 102 RVA: 0x000048D8 File Offset: 0x00002AD8
	// (set) Token: 0x06000067 RID: 103 RVA: 0x000048F0 File Offset: 0x00002AF0
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

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000068 RID: 104 RVA: 0x000048FC File Offset: 0x00002AFC
	// (set) Token: 0x06000069 RID: 105 RVA: 0x00004914 File Offset: 0x00002B14
	public Arena_Controller Controller_Component
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

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600006A RID: 106 RVA: 0x00004920 File Offset: 0x00002B20
	// (set) Token: 0x0600006B RID: 107 RVA: 0x00004938 File Offset: 0x00002B38
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

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600006C RID: 108 RVA: 0x00004944 File Offset: 0x00002B44
	// (set) Token: 0x0600006D RID: 109 RVA: 0x0000495C File Offset: 0x00002B5C
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

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600006E RID: 110 RVA: 0x00004968 File Offset: 0x00002B68
	// (set) Token: 0x0600006F RID: 111 RVA: 0x00004980 File Offset: 0x00002B80
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

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000070 RID: 112 RVA: 0x0000498C File Offset: 0x00002B8C
	// (set) Token: 0x06000071 RID: 113 RVA: 0x000049A4 File Offset: 0x00002BA4
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

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000072 RID: 114 RVA: 0x000049B0 File Offset: 0x00002BB0
	// (set) Token: 0x06000073 RID: 115 RVA: 0x000049C8 File Offset: 0x00002BC8
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

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000074 RID: 116 RVA: 0x000049D4 File Offset: 0x00002BD4
	// (set) Token: 0x06000075 RID: 117 RVA: 0x000049EC File Offset: 0x00002BEC
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

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000076 RID: 118 RVA: 0x000049F8 File Offset: 0x00002BF8
	// (set) Token: 0x06000077 RID: 119 RVA: 0x00004A10 File Offset: 0x00002C10
	public int Load_Narrow_Area_Data
	{
		get
		{
			return this._Load_Narrow_Area_Data;
		}
		set
		{
			this._Load_Narrow_Area_Data = value;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000078 RID: 120 RVA: 0x00004A1C File Offset: 0x00002C1C
	// (set) Token: 0x06000079 RID: 121 RVA: 0x00004A34 File Offset: 0x00002C34
	public bool Load_Narrow_Area_Data_State
	{
		get
		{
			return this._Load_Narrow_Area_Data_State;
		}
		set
		{
			this._Load_Narrow_Area_Data_State = value;
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004A40 File Offset: 0x00002C40
	public void Message_Handle(int _Net_ID, ArrayList _Parameters)
	{
		switch (_Net_ID)
		{
		case 110001:
		{
			bool flag = this.UI_Object == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("Arena", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.Load_UI_Complete), null, true);
			}
			else
			{
				this.Load_UI_Complete(string.Empty, this.UI_Object, null);
				this.UI_Object.SetActive(true);
			}
			break;
		}
		case 110002:
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
		case 110003:
		{
			bool flag3 = this.Controller_Component != null;
			if (flag3)
			{
				this.Controller_Component.Narrow_Area(int.Parse(_Parameters[0].ToString()));
			}
			else
			{
				this.Load_Narrow_Area_Data = int.Parse(_Parameters[0].ToString());
				this.Load_Narrow_Area_Data_State = true;
			}
			break;
		}
		case 110004:
		{
			bool flag4 = this.Controller_Component != null;
			if (flag4)
			{
				this.Controller_Component.Fight_Start();
			}
			else
			{
				this.Load_Fight_Start_Data_State = true;
			}
			break;
		}
		case 110005:
			fUIManager.GetInstance().GotoHall(0);
			break;
		case 110006:
		{
			bool flag5 = !(this.Controller_Component == null);
			if (flag5)
			{
				this.Controller_Component.Countdown_Data(_Parameters);
			}
			break;
		}
		case 110009:
		{
			bool flag6 = this.Controller_Component != null;
			if (flag6)
			{
				this.Controller_Component.Fight_Prepare();
			}
			else
			{
				this.Load_Fight_Prepare_Data_State = true;
			}
			break;
		}
		case 110010:
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
		case 110011:
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

	// Token: 0x0600007B RID: 123 RVA: 0x00004C6C File Offset: 0x00002E6C
	private void Load_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		this.UI_Object = _UI_Object;
		this.Controller_Component = this.UI_Object.GetComponent<Arena_Controller>();
		bool flag = this.Controller_Component == null;
		if (flag)
		{
			this.Controller_Component = this.UI_Object.AddComponent<Arena_Controller>();
		}
		bool flag2 = fGameManager.GetInstance().nNowScnID < 1 || fGameManager.Heroplayer == null || MatchManager.GetInstance().m_pFbData.nType != 4;
		if (flag2)
		{
			ArenaCon.inst.Close();
		}
		else
		{
			this.Controller_Component.Initialize();
			bool load_User_Data_State = this.Load_User_Data_State;
			if (load_User_Data_State)
			{
				this.Load_User_Data_State = false;
				this.Controller_Component.Display_Fight_Data(this.Load_User_Data);
			}
			bool load_Narrow_Area_Data_State = this.Load_Narrow_Area_Data_State;
			if (load_Narrow_Area_Data_State)
			{
				this.Controller_Component.Narrow_Area(this.Load_Narrow_Area_Data);
				this.Load_Narrow_Area_Data_State = false;
			}
		}
	}

	// Token: 0x04000089 RID: 137
	private static Arena_Message_Handle _Instantiation;

	// Token: 0x0400008A RID: 138
	private GameObject _UI_Object;

	// Token: 0x0400008B RID: 139
	private Arena_Controller _Controller_Component;

	// Token: 0x0400008C RID: 140
	private bool _Load_User_Data_State;

	// Token: 0x0400008D RID: 141
	private ArrayList _Load_User_Data;

	// Token: 0x0400008E RID: 142
	private bool _Load_Fight_Prepare_Data_State;

	// Token: 0x0400008F RID: 143
	private bool _Load_Fight_Start_Data_State;

	// Token: 0x04000090 RID: 144
	private bool _Load_Round_End_Data_State;

	// Token: 0x04000091 RID: 145
	private ArrayList _Load_Round_End_Data;

	// Token: 0x04000092 RID: 146
	private int _Load_Narrow_Area_Data;

	// Token: 0x04000093 RID: 147
	private bool _Load_Narrow_Area_Data_State;
}

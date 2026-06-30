using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class Scramble_Message_Handle
{
	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06002248 RID: 8776 RVA: 0x0013DF00 File Offset: 0x0013C100
	public static Scramble_Message_Handle Instantiation
	{
		get
		{
			bool flag = Scramble_Message_Handle._Instantiation == null;
			if (flag)
			{
				Scramble_Message_Handle._Instantiation = new Scramble_Message_Handle();
			}
			return Scramble_Message_Handle._Instantiation;
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06002249 RID: 8777 RVA: 0x0013DF30 File Offset: 0x0013C130
	// (set) Token: 0x0600224A RID: 8778 RVA: 0x0013DF48 File Offset: 0x0013C148
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

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x0600224B RID: 8779 RVA: 0x0013DF54 File Offset: 0x0013C154
	// (set) Token: 0x0600224C RID: 8780 RVA: 0x0013DF6C File Offset: 0x0013C16C
	public Scramble_Controller Controller_Component
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

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x0600224D RID: 8781 RVA: 0x0013DF78 File Offset: 0x0013C178
	// (set) Token: 0x0600224E RID: 8782 RVA: 0x0013DF90 File Offset: 0x0013C190
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

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x0600224F RID: 8783 RVA: 0x0013DF9C File Offset: 0x0013C19C
	// (set) Token: 0x06002250 RID: 8784 RVA: 0x0013DFB4 File Offset: 0x0013C1B4
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

	// Token: 0x06002251 RID: 8785 RVA: 0x0013DFC0 File Offset: 0x0013C1C0
	public void Message_Handle(int _Net_ID, ArrayList _Parameters)
	{
		switch (_Net_ID)
		{
		case 110101:
		{
			bool flag = this.UI_Object == null;
			if (flag)
			{
				fUIManager.GetInstance().ShowWin("Scramble", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.Load_UI_Complete), null, true);
			}
			else
			{
				this.Load_UI_Complete(string.Empty, this.UI_Object, null);
				this.UI_Object.SetActive(true);
			}
			break;
		}
		case 110102:
		{
			bool flag2 = !(this.Controller_Component == null);
			if (flag2)
			{
				this.Controller_Component.Countdown_Data(_Parameters);
			}
			break;
		}
		case 110103:
		{
			bool flag3 = this.Controller_Component != null;
			if (flag3)
			{
				this.Controller_Component.User_Data(_Parameters);
			}
			else
			{
				this.Load_User_Data = _Parameters;
				this.Load_User_Data_State = true;
			}
			break;
		}
		case 110104:
		{
			bool flag4 = !(this.Controller_Component == null);
			if (flag4)
			{
				this.Controller_Component.Score_Data(_Parameters);
			}
			break;
		}
		case 110105:
		{
			bool flag5 = !(this.Controller_Component == null);
			if (flag5)
			{
				this.Controller_Component.Fight_Data(_Parameters);
			}
			break;
		}
		case 110106:
		{
			bool flag6 = !(this.Controller_Component == null);
			if (flag6)
			{
				this.Controller_Component.Fight_Prepare();
			}
			break;
		}
		case 110107:
		{
			bool flag7 = !(this.Controller_Component == null);
			if (flag7)
			{
				this.Controller_Component.Fight_Start();
			}
			break;
		}
		case 110108:
		{
			bool flag8 = !(this.Controller_Component == null);
			if (flag8)
			{
				this.Controller_Component.Fight_Result(_Parameters);
			}
			break;
		}
		case 110109:
		{
			bool flag9 = !(this.Controller_Component == null);
			if (flag9)
			{
				this.Controller_Component.Fight_End();
			}
			break;
		}
		}
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x0013E1AC File Offset: 0x0013C3AC
	private void Load_UI_Complete(string _UI_Name, GameObject _UI_Object, object szParam)
	{
		this.UI_Object = _UI_Object;
		this.Controller_Component = this.UI_Object.GetComponent<Scramble_Controller>();
		bool flag = this.Controller_Component == null;
		if (flag)
		{
			this.Controller_Component = this.UI_Object.AddComponent<Scramble_Controller>();
		}
		bool flag2 = fGameManager.GetInstance().nNowScnID < 1 || fGameManager.Heroplayer == null || MatchManager.GetInstance().m_pFbData.nType != 5;
		if (flag2)
		{
			ScrambleCon.inst.Close();
		}
		else
		{
			this.Controller_Component.Initialize();
			bool load_User_Data_State = this.Load_User_Data_State;
			if (load_User_Data_State)
			{
				this.Load_User_Data_State = false;
				this.Controller_Component.User_Data(this.Load_User_Data);
			}
		}
	}

	// Token: 0x04001F82 RID: 8066
	private static Scramble_Message_Handle _Instantiation;

	// Token: 0x04001F83 RID: 8067
	private GameObject _UI_Object;

	// Token: 0x04001F84 RID: 8068
	private Scramble_Controller _Controller_Component;

	// Token: 0x04001F85 RID: 8069
	private bool _Load_User_Data_State;

	// Token: 0x04001F86 RID: 8070
	private ArrayList _Load_User_Data;

	// Token: 0x04001F87 RID: 8071
	public const int SYSTEM_SCRAMBLE = 110100;

	// Token: 0x04001F88 RID: 8072
	public const int SEND_SCRAMBLE_CREATE = 110101;

	// Token: 0x04001F89 RID: 8073
	public const int SEND_SCRAMBLE_COUNTDOWN = 110102;

	// Token: 0x04001F8A RID: 8074
	public const int SEND_SCRAMBLE_USER_DATA = 110103;

	// Token: 0x04001F8B RID: 8075
	public const int SEND_SCRAMBLE_FIGHT_SCORE = 110104;

	// Token: 0x04001F8C RID: 8076
	public const int SEND_SCRAMBLE_FIGHT_DATA = 110105;

	// Token: 0x04001F8D RID: 8077
	public const int SEND_SCRAMBLE_FIGHT_PREPARE = 110106;

	// Token: 0x04001F8E RID: 8078
	public const int SEND_SCRAMBLE_FIGHT_START = 110107;

	// Token: 0x04001F8F RID: 8079
	public const int SEND_SCRAMBLE_FIGHT_RESULT = 110108;

	// Token: 0x04001F90 RID: 8080
	public const int SEND_SCRAMBLE_FIGHT_END = 110109;
}

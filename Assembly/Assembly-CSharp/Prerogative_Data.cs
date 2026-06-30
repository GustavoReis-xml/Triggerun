using System;
using System.Collections;

// Token: 0x020002A1 RID: 673
public class Prerogative_Data
{
	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0011BFD0 File Offset: 0x0011A1D0
	// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0011BFE8 File Offset: 0x0011A1E8
	public int Platform_ID
	{
		get
		{
			return this._Platform_ID;
		}
		set
		{
			this._Platform_ID = value;
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0011BFF4 File Offset: 0x0011A1F4
	// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0011C00C File Offset: 0x0011A20C
	public string Platform_Name
	{
		get
		{
			return this._Platform_Name;
		}
		set
		{
			this._Platform_Name = value;
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0011C018 File Offset: 0x0011A218
	// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0011C030 File Offset: 0x0011A230
	public bool Prerogative_State
	{
		get
		{
			return this._Prerogative_State;
		}
		set
		{
			this._Prerogative_State = value;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0011C03C File Offset: 0x0011A23C
	// (set) Token: 0x06001E87 RID: 7815 RVA: 0x0011C066 File Offset: 0x0011A266
	public int Prerogative_Level
	{
		get
		{
			bool flag = this._Prerogative_Level < 1;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				result = this._Prerogative_Level;
			}
			return result;
		}
		set
		{
			this._Prerogative_Level = value;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0011C070 File Offset: 0x0011A270
	// (set) Token: 0x06001E89 RID: 7817 RVA: 0x0011C088 File Offset: 0x0011A288
	public bool Prerogative_Year
	{
		get
		{
			return this._Prerogative_Year;
		}
		set
		{
			this._Prerogative_Year = value;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0011C094 File Offset: 0x0011A294
	// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0011C0AC File Offset: 0x0011A2AC
	public int Prerogative_Growth_Level
	{
		get
		{
			return this._Prerogative_Growth_Level;
		}
		set
		{
			this._Prerogative_Growth_Level = value;
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0011C0B8 File Offset: 0x0011A2B8
	// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0011C0D0 File Offset: 0x0011A2D0
	public bool Prerogative_Super
	{
		get
		{
			return this._Prerogative_Super;
		}
		set
		{
			this._Prerogative_Super = value;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0011C0DC File Offset: 0x0011A2DC
	// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0011C0F4 File Offset: 0x0011A2F4
	public string Platform_Data
	{
		get
		{
			return this._Platform_Data;
		}
		set
		{
			this._Platform_Data = value;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0011C100 File Offset: 0x0011A300
	// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0011C118 File Offset: 0x0011A318
	public bool Get_Day_Reward_State
	{
		get
		{
			return this._Get_Day_Reward_State;
		}
		set
		{
			this._Get_Day_Reward_State = value;
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0011C124 File Offset: 0x0011A324
	// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0011C13C File Offset: 0x0011A33C
	public bool Get_Super_Reward_State
	{
		get
		{
			return this._Get_Super_Reward_State;
		}
		set
		{
			this._Get_Super_Reward_State = value;
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0011C148 File Offset: 0x0011A348
	// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0011C160 File Offset: 0x0011A360
	public bool Get_Year_Reward_State
	{
		get
		{
			return this._Get_Year_Reward_State;
		}
		set
		{
			this._Get_Year_Reward_State = value;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0011C16C File Offset: 0x0011A36C
	// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0011C184 File Offset: 0x0011A384
	public bool Get_Novice_Reward_State
	{
		get
		{
			return this._Get_Novice_Reward_State;
		}
		set
		{
			this._Get_Novice_Reward_State = value;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0011C190 File Offset: 0x0011A390
	// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0011C1A8 File Offset: 0x0011A3A8
	public bool Get_Growth_Reward_State
	{
		get
		{
			return this._Get_Growth_Reward_State;
		}
		set
		{
			this._Get_Growth_Reward_State = value;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0011C1B4 File Offset: 0x0011A3B4
	// (set) Token: 0x06001E9B RID: 7835 RVA: 0x0011C1CC File Offset: 0x0011A3CC
	public ArrayList Growth_Data
	{
		get
		{
			return this._Growth_Data;
		}
		set
		{
			this._Growth_Data = value;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0011C1D8 File Offset: 0x0011A3D8
	// (set) Token: 0x06001E9D RID: 7837 RVA: 0x0011C1F0 File Offset: 0x0011A3F0
	public int QQ_Games_Hall_Reward_State_New
	{
		get
		{
			return this._QQ_Games_Hall_Reward_State_New;
		}
		set
		{
			this._QQ_Games_Hall_Reward_State_New = value;
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0011C1FC File Offset: 0x0011A3FC
	// (set) Token: 0x06001E9F RID: 7839 RVA: 0x0011C214 File Offset: 0x0011A414
	public int QQ_Games_Hall_Reward_State_Day
	{
		get
		{
			return this._QQ_Games_Hall_Reward_State_Day;
		}
		set
		{
			this._QQ_Games_Hall_Reward_State_Day = value;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x0011C220 File Offset: 0x0011A420
	// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x0011C238 File Offset: 0x0011A438
	public ArrayList QQ_Games_Hall_Reward_State_Level_Up
	{
		get
		{
			return this._QQ_Games_Hall_Reward_State_Level_Up;
		}
		set
		{
			this._QQ_Games_Hall_Reward_State_Level_Up = value;
		}
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x0011C244 File Offset: 0x0011A444
	public void Receive_Message(int _Message_ID, ArrayList _Parameters)
	{
		bool flag = _Message_ID == 302002;
		if (flag)
		{
			this.Initialize_Data(_Parameters);
		}
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x0011C268 File Offset: 0x0011A468
	public void Initialize_Data(ArrayList _Value)
	{
		this.Platform_ID = int.Parse(_Value[5].ToString());
		this.Platform_Name = _Value[0].ToString();
		this.Platform_Data = _Value[6].ToString();
		this.Prerogative_State = (int.Parse(_Value[7].ToString()) == 1);
		this.Prerogative_Level = int.Parse(_Value[1].ToString());
		this.Prerogative_Year = (int.Parse(_Value[2].ToString()) == 1);
		this.Prerogative_Growth_Level = int.Parse(_Value[3].ToString());
		this.Prerogative_Super = (int.Parse(_Value[4].ToString()) == 1);
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetLzAwardState();
			MainUICon.inst.SetQQAwardState();
		}
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x0011C35C File Offset: 0x0011A55C
	public void Initialize_Reward_Data(ArrayList _Value)
	{
		this.Get_Day_Reward_State = ((int)_Value[0] == 1);
		this.Get_Year_Reward_State = ((int)_Value[1] == 1);
		this.Get_Novice_Reward_State = ((int)_Value[2] == 1);
		this.Growth_Data = (ArrayList)_Value[3];
		this.Get_Growth_Reward_State = ((int)_Value[4] == 1);
		this.Get_Super_Reward_State = ((int)_Value[5] == 1);
		bool flag = PrerogativeCon.Instance != null && PrerogativeCon.Instance.visible;
		if (flag)
		{
			PrerogativeCon.Instance.View_Component.Display_Button();
			PrerogativeCon.Instance.View_Component.Display_Data();
		}
		this.QQ_Games_Hall_Reward_State_New = (int)_Value[11];
		this.QQ_Games_Hall_Reward_State_Day = (int)_Value[12];
		this.QQ_Games_Hall_Reward_State_Level_Up = (ArrayList)_Value[13];
		bool flag2 = QQ_Games_Hall_PrerogativeCon.Instance != null && QQ_Games_Hall_PrerogativeCon.Instance.visible;
		if (flag2)
		{
			QQ_Games_Hall_PrerogativeCon.Instance.View_Component.Display_Button();
			QQ_Games_Hall_PrerogativeCon.Instance.View_Component.Display_Data();
		}
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x0011C4A8 File Offset: 0x0011A6A8
	public bool Get_Reward_State()
	{
		bool get_Day_Reward_State = this.Get_Day_Reward_State;
		bool result;
		if (get_Day_Reward_State)
		{
			result = true;
		}
		else
		{
			bool get_Year_Reward_State = this.Get_Year_Reward_State;
			if (get_Year_Reward_State)
			{
				result = true;
			}
			else
			{
				bool get_Novice_Reward_State = this.Get_Novice_Reward_State;
				if (get_Novice_Reward_State)
				{
					result = true;
				}
				else
				{
					bool get_Growth_Reward_State = this.Get_Growth_Reward_State;
					if (get_Growth_Reward_State)
					{
						result = true;
					}
					else
					{
						bool get_Super_Reward_State = this.Get_Super_Reward_State;
						if (get_Super_Reward_State)
						{
							result = true;
						}
						else
						{
							for (int i = 0; i < this.Growth_Data.Count; i++)
							{
								bool flag = int.Parse(this.Growth_Data[i].ToString()) == 1;
								if (flag)
								{
									return true;
								}
							}
							result = false;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x0011C554 File Offset: 0x0011A754
	public bool Get_QQ_Games_Hall_Reward_State()
	{
		bool flag = this.QQ_Games_Hall_Reward_State_New == 1;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = this.QQ_Games_Hall_Reward_State_Day == 1;
			if (flag2)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < this.QQ_Games_Hall_Reward_State_Level_Up.Count; i++)
				{
					bool flag3 = int.Parse(this.QQ_Games_Hall_Reward_State_Level_Up[i].ToString()) == 1;
					if (flag3)
					{
						return true;
					}
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x04001C3E RID: 7230
	public static Prerogative_Data Instance;

	// Token: 0x04001C3F RID: 7231
	private int _Platform_ID;

	// Token: 0x04001C40 RID: 7232
	private string _Platform_Name;

	// Token: 0x04001C41 RID: 7233
	private bool _Prerogative_State;

	// Token: 0x04001C42 RID: 7234
	private int _Prerogative_Level;

	// Token: 0x04001C43 RID: 7235
	private bool _Prerogative_Year;

	// Token: 0x04001C44 RID: 7236
	private int _Prerogative_Growth_Level;

	// Token: 0x04001C45 RID: 7237
	private bool _Prerogative_Super;

	// Token: 0x04001C46 RID: 7238
	private string _Platform_Data;

	// Token: 0x04001C47 RID: 7239
	private bool _Get_Day_Reward_State;

	// Token: 0x04001C48 RID: 7240
	private bool _Get_Super_Reward_State;

	// Token: 0x04001C49 RID: 7241
	private bool _Get_Year_Reward_State;

	// Token: 0x04001C4A RID: 7242
	private bool _Get_Novice_Reward_State;

	// Token: 0x04001C4B RID: 7243
	private bool _Get_Growth_Reward_State;

	// Token: 0x04001C4C RID: 7244
	private ArrayList _Growth_Data = new ArrayList();

	// Token: 0x04001C4D RID: 7245
	private int _QQ_Games_Hall_Reward_State_New;

	// Token: 0x04001C4E RID: 7246
	private int _QQ_Games_Hall_Reward_State_Day;

	// Token: 0x04001C4F RID: 7247
	private ArrayList _QQ_Games_Hall_Reward_State_Level_Up = new ArrayList();
}

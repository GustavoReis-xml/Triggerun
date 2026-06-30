using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002E1 RID: 737
public class Scramble_Area_Entity
{
	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x0600220F RID: 8719 RVA: 0x0013CE8C File Offset: 0x0013B08C
	// (set) Token: 0x06002210 RID: 8720 RVA: 0x0013CEA4 File Offset: 0x0013B0A4
	public int Index
	{
		get
		{
			return this._Index;
		}
		set
		{
			this._Index = value;
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06002211 RID: 8721 RVA: 0x0013CEB0 File Offset: 0x0013B0B0
	// (set) Token: 0x06002212 RID: 8722 RVA: 0x0013CEC8 File Offset: 0x0013B0C8
	public int Number
	{
		get
		{
			return this._Number;
		}
		set
		{
			this._Number = value;
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06002213 RID: 8723 RVA: 0x0013CED4 File Offset: 0x0013B0D4
	// (set) Token: 0x06002214 RID: 8724 RVA: 0x0013CEEC File Offset: 0x0013B0EC
	public int Forces
	{
		get
		{
			return this._Forces;
		}
		set
		{
			this._Forces = value;
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06002215 RID: 8725 RVA: 0x0013CEF8 File Offset: 0x0013B0F8
	// (set) Token: 0x06002216 RID: 8726 RVA: 0x0013CF10 File Offset: 0x0013B110
	public int A_Amount
	{
		get
		{
			return this._A_Amount;
		}
		set
		{
			this._A_Amount = value;
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06002217 RID: 8727 RVA: 0x0013CF1C File Offset: 0x0013B11C
	// (set) Token: 0x06002218 RID: 8728 RVA: 0x0013CF34 File Offset: 0x0013B134
	public int B_Amount
	{
		get
		{
			return this._B_Amount;
		}
		set
		{
			this._B_Amount = value;
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06002219 RID: 8729 RVA: 0x0013CF40 File Offset: 0x0013B140
	// (set) Token: 0x0600221A RID: 8730 RVA: 0x0013CF58 File Offset: 0x0013B158
	public int Occupy_Max_Progress
	{
		get
		{
			return this._Occupy_Max_Progress;
		}
		set
		{
			this._Occupy_Max_Progress = value;
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x0600221B RID: 8731 RVA: 0x0013CF64 File Offset: 0x0013B164
	// (set) Token: 0x0600221C RID: 8732 RVA: 0x0013CF7C File Offset: 0x0013B17C
	public int Occupy_A_Current_Progress
	{
		get
		{
			return this._Occupy_A_Current_Progress;
		}
		set
		{
			this._Occupy_A_Current_Progress = value;
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x0600221D RID: 8733 RVA: 0x0013CF88 File Offset: 0x0013B188
	// (set) Token: 0x0600221E RID: 8734 RVA: 0x0013CFA0 File Offset: 0x0013B1A0
	public int Occupy_B_Current_Progress
	{
		get
		{
			return this._Occupy_B_Current_Progress;
		}
		set
		{
			this._Occupy_B_Current_Progress = value;
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x0600221F RID: 8735 RVA: 0x0013CFAC File Offset: 0x0013B1AC
	// (set) Token: 0x06002220 RID: 8736 RVA: 0x0013CFC4 File Offset: 0x0013B1C4
	public int Occupy_State
	{
		get
		{
			return this._Occupy_State;
		}
		set
		{
			bool flag = this._Occupy_State != value;
			if (flag)
			{
				this.Play_Occupy_State(value);
				this.Describe_State = false;
			}
			this._Occupy_State = value;
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06002221 RID: 8737 RVA: 0x0013CFFC File Offset: 0x0013B1FC
	// (set) Token: 0x06002222 RID: 8738 RVA: 0x0013D014 File Offset: 0x0013B214
	public int State
	{
		get
		{
			return this._State;
		}
		set
		{
			bool flag = value == 3;
			if (flag)
			{
				this._State = value;
			}
			else
			{
				bool flag2 = this._State != value;
				if (flag2)
				{
					bool flag3 = !this.Describe_State;
					if (flag3)
					{
						this.Describe_State = true;
					}
					this.Play_State(value);
				}
				this._State = value;
			}
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06002223 RID: 8739 RVA: 0x0013D06C File Offset: 0x0013B26C
	// (set) Token: 0x06002224 RID: 8740 RVA: 0x0013D084 File Offset: 0x0013B284
	public bool Describe_State
	{
		get
		{
			return this._Describe_State;
		}
		set
		{
			this._Describe_State = value;
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06002225 RID: 8741 RVA: 0x0013D090 File Offset: 0x0013B290
	// (set) Token: 0x06002226 RID: 8742 RVA: 0x0013D0A8 File Offset: 0x0013B2A8
	public string Describe
	{
		get
		{
			return this._Describe;
		}
		set
		{
			this._Describe = value;
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06002227 RID: 8743 RVA: 0x0013D0B4 File Offset: 0x0013B2B4
	// (set) Token: 0x06002228 RID: 8744 RVA: 0x0013D0E6 File Offset: 0x0013B2E6
	public List<int> Scramble_User_ID_List
	{
		get
		{
			bool flag = this._Scramble_User_ID_List == null;
			if (flag)
			{
				this._Scramble_User_ID_List = new List<int>();
			}
			return this._Scramble_User_ID_List;
		}
		set
		{
			this._Scramble_User_ID_List = value;
		}
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x0013D0F0 File Offset: 0x0013B2F0
	public void Set_Data(int _Index, ArrayList _Parameters)
	{
		this.Index = _Index;
		this.Number = int.Parse(_Parameters[1].ToString());
		this.Forces = int.Parse(_Parameters[2].ToString());
		this.A_Amount = int.Parse(_Parameters[3].ToString());
		this.B_Amount = int.Parse(_Parameters[4].ToString());
		this.State = int.Parse(_Parameters[5].ToString());
		this.Occupy_State = int.Parse(_Parameters[6].ToString());
		this.Occupy_Max_Progress = int.Parse(_Parameters[7].ToString());
		this.Occupy_A_Current_Progress = int.Parse(_Parameters[8].ToString());
		this.Occupy_B_Current_Progress = int.Parse(_Parameters[9].ToString());
		this.Scramble_User_ID_List.Clear();
		string text = _Parameters[10].ToString();
		string[] array = text.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			text = array[i].ToString();
			bool flag = !(text == string.Empty);
			if (flag)
			{
				this.Scramble_User_ID_List.Add(int.Parse(text));
			}
		}
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x0013D258 File Offset: 0x0013B458
	public void Play_State(int _State)
	{
		string text = null;
		bool flag = _State == 0;
		if (flag)
		{
			this.Describe = string.Empty;
		}
		else
		{
			int nShili = PlayerDataManager.getInstance().playercom.m_nShili;
			switch (this.Number)
			{
			case 1:
				text = "A";
				break;
			case 2:
				text = "B";
				break;
			case 3:
				text = "C";
				break;
			case 4:
				text = "D";
				break;
			case 5:
				text = "E";
				break;
			}
			string text2 = (nShili != _State) ? Language.GetLang(150008, new object[0]) : Language.GetLang(150007, new object[0]);
			this.Describe = Language.GetLang(150009, new object[]
			{
				text,
				text2
			});
			PreloadData preloadData = null;
			bool flag2 = _State == nShili;
			if (flag2)
			{
				bool flag3 = text.ToLower() == "a";
				if (flag3)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[726];
				}
				bool flag4 = text.ToLower() == "b";
				if (flag4)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[727];
				}
				bool flag5 = text.ToLower() == "c";
				if (flag5)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[728];
				}
			}
			else
			{
				bool flag6 = text.ToLower() == "a";
				if (flag6)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[729];
				}
				bool flag7 = text.ToLower() == "b";
				if (flag7)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[730];
				}
				bool flag8 = text.ToLower() == "c";
				if (flag8)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[731];
				}
			}
			bool flag9 = preloadData != null;
			if (flag9)
			{
				Sound_Manager.Instantiation.Play_Sound("Scramble", "Prepare_Occupy", preloadData.sUrl);
			}
		}
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x0013D480 File Offset: 0x0013B680
	public void Play_Occupy_State(int _Occupy_State)
	{
		string text = null;
		bool flag = _Occupy_State == 0;
		if (flag)
		{
			this.Describe = string.Empty;
		}
		else
		{
			int nShili = PlayerDataManager.getInstance().playercom.m_nShili;
			switch (this.Number)
			{
			case 1:
				text = "A";
				break;
			case 2:
				text = "B";
				break;
			case 3:
				text = "C";
				break;
			case 4:
				text = "D";
				break;
			case 5:
				text = "E";
				break;
			}
			string text2 = (nShili != _Occupy_State) ? Language.GetLang(150008, new object[0]) : Language.GetLang(150007, new object[0]);
			this.Describe = Language.GetLang(150010, new object[]
			{
				text,
				text2
			});
			PreloadData preloadData = null;
			bool flag2 = _Occupy_State == nShili;
			if (flag2)
			{
				bool flag3 = text.ToLower() == "a";
				if (flag3)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[735];
				}
				bool flag4 = text.ToLower() == "b";
				if (flag4)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[736];
				}
				bool flag5 = text.ToLower() == "c";
				if (flag5)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[737];
				}
			}
			else
			{
				bool flag6 = text.ToLower() == "a";
				if (flag6)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[723];
				}
				bool flag7 = text.ToLower() == "b";
				if (flag7)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[724];
				}
				bool flag8 = text.ToLower() == "c";
				if (flag8)
				{
					preloadData = fGameManager.mloadConfig.AllProLodaList[725];
				}
			}
			bool flag9 = preloadData != null;
			if (flag9)
			{
				Sound_Manager.Instantiation.Play_Sound("Scramble", "Had_Occupy", preloadData.sUrl);
			}
		}
	}

	// Token: 0x04001F6F RID: 8047
	private int _Index;

	// Token: 0x04001F70 RID: 8048
	private int _Number;

	// Token: 0x04001F71 RID: 8049
	private int _Forces;

	// Token: 0x04001F72 RID: 8050
	private int _A_Amount;

	// Token: 0x04001F73 RID: 8051
	private int _B_Amount;

	// Token: 0x04001F74 RID: 8052
	private int _Occupy_Max_Progress = 210;

	// Token: 0x04001F75 RID: 8053
	private int _Occupy_A_Current_Progress;

	// Token: 0x04001F76 RID: 8054
	private int _Occupy_B_Current_Progress;

	// Token: 0x04001F77 RID: 8055
	private int _Occupy_State;

	// Token: 0x04001F78 RID: 8056
	private int _State;

	// Token: 0x04001F79 RID: 8057
	private bool _Describe_State;

	// Token: 0x04001F7A RID: 8058
	private string _Describe = string.Empty;

	// Token: 0x04001F7B RID: 8059
	private List<int> _Scramble_User_ID_List;
}

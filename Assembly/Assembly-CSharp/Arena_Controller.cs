using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class Arena_Controller : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600004B RID: 75 RVA: 0x000040E0 File Offset: 0x000022E0
	// (set) Token: 0x0600004C RID: 76 RVA: 0x0000413E File Offset: 0x0000233E
	public Arena_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Arena_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Arena_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00004148 File Offset: 0x00002348
	// (set) Token: 0x0600004E RID: 78 RVA: 0x000041A6 File Offset: 0x000023A6
	public Arena_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Arena_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Arena_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600004F RID: 79 RVA: 0x000041B0 File Offset: 0x000023B0
	// (set) Token: 0x06000050 RID: 80 RVA: 0x000041C8 File Offset: 0x000023C8
	public bool Fight_State
	{
		get
		{
			return this._Fight_State;
		}
		set
		{
			this._Fight_State = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000051 RID: 81 RVA: 0x000041D4 File Offset: 0x000023D4
	// (set) Token: 0x06000052 RID: 82 RVA: 0x000041EC File Offset: 0x000023EC
	public int Substitution_State
	{
		get
		{
			return this._Substitution_State;
		}
		set
		{
			this._Substitution_State = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000053 RID: 83 RVA: 0x000041F8 File Offset: 0x000023F8
	// (set) Token: 0x06000054 RID: 84 RVA: 0x00004210 File Offset: 0x00002410
	public bool Door_State
	{
		get
		{
			return this._Door_State;
		}
		set
		{
			this._Door_State = value;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000421A File Offset: 0x0000241A
	public void Initialize()
	{
		this.View_Component.Initialize();
		this.Model_Component.Clear();
		ArenaCon.inst.View_Component = this.View_Component;
		ArenaCon.inst.Controler_Component = this;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004254 File Offset: 0x00002454
	public void Display_Fight_Data(ArrayList _Parameters)
	{
		ArrayList user_Data_Format = (ArrayList)_Parameters[0];
		int current_Round_Number = (int)_Parameters[1];
		int a_Victory_Round_Number = (int)_Parameters[2];
		int b_Victory_Round_Number = (int)_Parameters[3];
		this.Set_User_Data_Format(user_Data_Format);
		this.Model_Component.Current_Round_Number = current_Round_Number;
		this.Model_Component.A_Victory_Round_Number = a_Victory_Round_Number;
		this.Model_Component.B_Victory_Round_Number = b_Victory_Round_Number;
		this.View_Component.Display_Fight_Data(this.Fight_State, this.Model_Component.Get_User_Data(1), this.Model_Component.Get_User_Data(2), current_Round_Number, a_Victory_Round_Number, b_Victory_Round_Number);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000042FC File Offset: 0x000024FC
	private void OnSetChangeTime(string sTime)
	{
		bool flag = sTime == string.Empty;
		if (flag)
		{
			ArenaCon.inst.m_dStartChangeTime = DateTime.MinValue;
		}
		else
		{
			ArenaCon.inst.m_dStartChangeTime = DateTime.Parse(sTime);
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004340 File Offset: 0x00002540
	public void Countdown_Data(ArrayList _Parameters)
	{
		int num = (int)_Parameters[0];
		this.Fight_State = (int.Parse(_Parameters[1].ToString()) == 1);
		this.Substitution_State = int.Parse(_Parameters[2].ToString());
		ArenaCon.inst.Fight_State = this.Fight_State;
		ArenaCon.inst.Substitution_State = this.Substitution_State;
		ArenaCon.inst.Limit_Time = _Parameters[3].ToString();
		this.OnSetChangeTime(ArenaCon.inst.Limit_Time);
		this.View_Component.Display_Countdown(num);
		bool fight_State = this.Fight_State;
		if (fight_State)
		{
			this.View_Component.Revert_Area_Close();
			bool flag = num == 20;
			if (flag)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[717];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			}
			bool flag2 = !this.Door_State;
			if (flag2)
			{
				this.Door_State = true;
				Door_Control.Get_Instantiate.General_Door_Handle(1, "close_open", true);
				Door_Control.Get_Instantiate.Effect_Door_Handle(1, true);
				Door_Control.Get_Instantiate.Effect_Door_Handle(2, true);
			}
			this.View_Component.Display_Prepare_State_Describe(false);
		}
		else
		{
			bool door_State = this.Door_State;
			if (door_State)
			{
				this.Door_State = false;
				Door_Control.Get_Instantiate.General_Door_Handle(1, "open_close", true);
			}
			this.View_Component.Revert_Area_Open();
			bool flag3 = num == 5;
			if (flag3)
			{
				PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[878];
				fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
			}
			bool flag4 = num >= 0 && num <= 5;
			if (flag4)
			{
				this.View_Component.Display_Countdown_Describe(num);
			}
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004520 File Offset: 0x00002720
	public void Set_User_Data_Format(ArrayList _Parameters)
	{
		this.Model_Component.Clear();
		for (int i = 0; i < _Parameters.Count; i++)
		{
			string text = _Parameters[i].ToString();
			bool flag = !(text == string.Empty) && !(text == string.Empty);
			if (flag)
			{
				string[] array = text.Split(new char[]
				{
					'-'
				});
				int forces = int.Parse(array[6].ToString());
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < array.Length; j++)
				{
					arrayList.Add(array[j].ToString());
				}
				this.Set_User_Data(forces, arrayList);
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000045F5 File Offset: 0x000027F5
	private void Set_User_Data(int _Forces, ArrayList _Parameters)
	{
		this.Model_Component.Set_User_Data(_Forces, _Parameters);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004608 File Offset: 0x00002808
	public void Fight_Prepare()
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player.heroBase != null;
		if (flag)
		{
			((HeroBaseMe)player.heroBase).RoundBegin(0);
		}
		fGameManager.Heroplayer.canControl = true;
		player.MultiRoundBegin(0);
		this.View_Component.Display_Prepare_State_Describe(true);
		bool flag2 = gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004684 File Offset: 0x00002884
	public void Fight_Start()
	{
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[718];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
		this.View_Component.Display_Prepare_State_Describe(false);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004700 File Offset: 0x00002900
	public void Fight_End(ArrayList _Parameters)
	{
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004703 File Offset: 0x00002903
	public void Narrow_Area(int _State)
	{
		this.View_Component.Narrow_Area(_State);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004713 File Offset: 0x00002913
	public void Round_Start()
	{
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004718 File Offset: 0x00002918
	public void Round_End(ArrayList _Parameters)
	{
		int num = int.Parse(_Parameters[0].ToString());
		int victory_Ground = int.Parse(_Parameters[1].ToString());
		int a_Victory_Round_Number = int.Parse(_Parameters[2].ToString());
		int b_Victory_Round_Number = int.Parse(_Parameters[3].ToString());
		int final_Victory_Ground = int.Parse(_Parameters[4].ToString());
		ArrayList round_Result_Data = (ArrayList)_Parameters[5];
		int nShili = PlayerDataManager.getInstance().playerCommon.m_nShili;
		this.Door_State = false;
		this.Model_Component.Current_Round_Number = num;
		Door_Control.Get_Instantiate.General_Door_Handle(1, "open_close", true);
		fGameManager.GetInstance().getPlayer().MultiRoundBegin(1);
		PlayerDataManager.getInstance().playercom.m_nIsFristFight = 0;
		this.View_Component.Round_End();
		this.View_Component.Display_Fight_State_Describe(Language.GetLang(150003, new object[0]));
		fGameManager.Heroplayer.canControl = false;
		base.StartCoroutine(this.Wait_Handle_Round_End(num, victory_Ground, nShili, a_Victory_Round_Number, b_Victory_Round_Number, final_Victory_Ground, round_Result_Data));
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004844 File Offset: 0x00002A44
	private IEnumerator Wait_Handle_Round_End(int _Round_Number, int _Victory_Ground, int _Country, int _A_Victory_Round_Number, int _B_Victory_Round_Number, int _Final_Victory_Ground, ArrayList _Round_Result_Data)
	{
		yield return new WaitForSeconds(4f);
		int _Result = 0;
		int i = 0;
		bool flag = !(gameuiCon.inst != null);
		if (flag)
		{
			yield break;
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(_Round_Number);
		_Result = ((_Country == _Victory_Ground) ? 1 : 2);
		arrayList.Add(_Result);
		bool flag2 = _Country == 1;
		if (flag2)
		{
			arrayList.Add(_A_Victory_Round_Number);
			arrayList.Add(_B_Victory_Round_Number);
			bool flag3 = _Final_Victory_Ground == 2;
			if (flag3)
			{
				_Final_Victory_Ground = 3;
			}
		}
		else
		{
			arrayList.Add(_B_Victory_Round_Number);
			arrayList.Add(_A_Victory_Round_Number);
			bool flag4 = _Final_Victory_Ground == 1;
			if (flag4)
			{
				_Final_Victory_Ground = 3;
			}
			bool flag5 = _Final_Victory_Ground == 2;
			if (flag5)
			{
				_Final_Victory_Ground = 1;
			}
		}
		int num;
		for (i = 0; i < _Round_Result_Data.Count; i = num + 1)
		{
			bool flag6 = _Country == 2;
			if (flag6)
			{
				bool flag7 = int.Parse(_Round_Result_Data[i].ToString()) == 2;
				if (flag7)
				{
					_Round_Result_Data[i] = 1;
				}
				else
				{
					bool flag8 = int.Parse(_Round_Result_Data[i].ToString()) == 1;
					if (flag8)
					{
						_Round_Result_Data[i] = 2;
					}
				}
			}
			num = i;
		}
		arrayList.Add(_Round_Result_Data);
		arrayList.Add(_Final_Victory_Ground);
		gameuiCon.inst.OpenRoundEndUI(arrayList);
		yield break;
	}

	// Token: 0x04000084 RID: 132
	private Arena_View _View_Component;

	// Token: 0x04000085 RID: 133
	private Arena_Model _Model_Component;

	// Token: 0x04000086 RID: 134
	private bool _Fight_State;

	// Token: 0x04000087 RID: 135
	private int _Substitution_State;

	// Token: 0x04000088 RID: 136
	private bool _Door_State;
}

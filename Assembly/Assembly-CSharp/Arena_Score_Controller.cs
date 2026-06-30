using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class Arena_Score_Controller : MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000096 RID: 150 RVA: 0x00005014 File Offset: 0x00003214
	// (set) Token: 0x06000097 RID: 151 RVA: 0x00005072 File Offset: 0x00003272
	public Arena_Score_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Arena_Score_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Arena_Score_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000098 RID: 152 RVA: 0x0000507C File Offset: 0x0000327C
	// (set) Token: 0x06000099 RID: 153 RVA: 0x000050DA File Offset: 0x000032DA
	public Arena_Score_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Arena_Score_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Arena_Score_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600009A RID: 154 RVA: 0x000050E4 File Offset: 0x000032E4
	// (set) Token: 0x0600009B RID: 155 RVA: 0x000050FC File Offset: 0x000032FC
	public bool Fight_Prepare_State
	{
		get
		{
			return this._Fight_Prepare_State;
		}
		set
		{
			this._Fight_Prepare_State = value;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00005108 File Offset: 0x00003308
	// (set) Token: 0x0600009D RID: 157 RVA: 0x00005120 File Offset: 0x00003320
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

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600009E RID: 158 RVA: 0x0000512C File Offset: 0x0000332C
	// (set) Token: 0x0600009F RID: 159 RVA: 0x00005144 File Offset: 0x00003344
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

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005150 File Offset: 0x00003350
	// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005168 File Offset: 0x00003368
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

	// Token: 0x060000A2 RID: 162 RVA: 0x00005174 File Offset: 0x00003374
	public void Initialize()
	{
		bool fight_State = this.Fight_State;
		if (fight_State)
		{
			this.View_Component.Display_Prepare_State_Describe(false, 0f);
		}
		else
		{
			this.View_Component.Display_Prepare_State_Describe(true, 0f);
		}
		Arena_ScoreCon.inst.View_Component = this.View_Component;
		Arena_ScoreCon.inst.Controler_Component = this;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000051D4 File Offset: 0x000033D4
	public void Display_Fight_Data(ArrayList _Parameters)
	{
		this.Fight_State = (int.Parse(_Parameters[0].ToString()) == 1);
		int max_Score = (int)_Parameters[1];
		int a_Score_Number = (int)_Parameters[2];
		int b_Score_Number = (int)_Parameters[3];
		this.View_Component.Display_Fight_Data(this.Fight_State, max_Score, a_Score_Number, b_Score_Number);
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00005240 File Offset: 0x00003440
	public void Countdown_Data(ArrayList _Parameters)
	{
		int num = (int)_Parameters[0];
		this.Fight_State = (int.Parse(_Parameters[1].ToString()) == 1);
		this.Substitution_State = int.Parse(_Parameters[2].ToString());
		Arena_ScoreCon.inst.Fight_State = this.Fight_State;
		Arena_ScoreCon.inst.Substitution_State = this.Substitution_State;
		Arena_ScoreCon.inst.Limit_Time = _Parameters[3].ToString();
		this.View_Component.Display_Countdown(num);
		bool fight_State = this.Fight_State;
		if (fight_State)
		{
			bool flag = num == 20;
			if (flag)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[722];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			}
			bool flag2 = !this.Door_State;
			if (flag2)
			{
				this.Door_State = true;
				Door_Control.Get_Instantiate.General_Door_Handle(1, "close_open", true);
				Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
				this.View_Component.UI_Component.Effect_Object.SetActive(false);
			}
		}
		else
		{
			bool door_State = this.Door_State;
			if (door_State)
			{
				this.Door_State = false;
				Door_Control.Get_Instantiate.General_Door_Handle(1, "open_close", true);
			}
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

	// Token: 0x060000A5 RID: 165 RVA: 0x00005400 File Offset: 0x00003600
	public void Fight_Prepare(ArrayList _Parameters)
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player.heroBase != null;
		if (flag)
		{
			((HeroBaseMe)player.heroBase).RoundBegin(0);
		}
		this.Fight_Prepare_State = true;
		player.MultiRoundBegin(0);
		this.View_Component.Display_Max_Score(int.Parse(_Parameters[0].ToString()));
		this.View_Component.Display_Socre(int.Parse(_Parameters[1].ToString()), int.Parse(_Parameters[2].ToString()));
		this.View_Component.Display_Prepare_State_Describe(true, 1f);
		fGameManager.Heroplayer.canControl = true;
		bool flag2 = gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000054D4 File Offset: 0x000036D4
	public void Fight_Start()
	{
		this.Fight_Prepare_State = false;
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[718];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
		this.View_Component.Display_Prepare_State_Describe(false, 1f);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00005541 File Offset: 0x00003741
	public void Fight_End(ArrayList _Parameters)
	{
		this.Fight_Result(_Parameters);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000554C File Offset: 0x0000374C
	public void Fight_Result(ArrayList _Parameters)
	{
		this.Fight_State = false;
		Arena_ScoreCon.inst.Fight_State = this.Fight_State;
		fGameManager.Heroplayer.canControl = false;
		int num = (int)_Parameters[0];
		bool flag = num == 0;
		if (flag)
		{
			this.View_Component.Display_Fight_Result();
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[862];
			Sound_Manager.Instantiation.Play_Sound("Scramble", string.Empty, preloadData.sUrl);
		}
		else
		{
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == num;
			if (flag2)
			{
				this.View_Component.Display_Fight_Result(true);
				PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[881];
				Sound_Manager.Instantiation.Play_Sound("Scramble", string.Empty, preloadData2.sUrl);
			}
			else
			{
				this.View_Component.Display_Fight_Result(false);
				PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[885];
				Sound_Manager.Instantiation.Play_Sound("Scramble", string.Empty, preloadData3.sUrl);
			}
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00005672 File Offset: 0x00003872
	public void Round_Start()
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00005678 File Offset: 0x00003878
	public void Round_End(ArrayList _Parameters)
	{
		int round_Number = int.Parse(_Parameters[0].ToString());
		int a_Score_Number = int.Parse(_Parameters[2].ToString());
		int b_Score_Number = int.Parse(_Parameters[3].ToString());
		int final_Victory_Ground = int.Parse(_Parameters[4].ToString());
		ArrayList round_Result_Data = (ArrayList)_Parameters[5];
		int nShili = PlayerDataManager.getInstance().playerCommon.m_nShili;
		this.Door_State = false;
		Door_Control.Get_Instantiate.General_Door_Handle(1, "open_close", true);
		fGameManager.GetInstance().getPlayer().MultiRoundBegin(1);
		PlayerDataManager.getInstance().playercom.m_nIsFristFight = 0;
		this.View_Component.Round_End(round_Number);
		this.View_Component.Display_Fight_State_Describe(Language.GetLang(150004, new object[0]));
		fGameManager.Heroplayer.canControl = false;
		base.StartCoroutine(this.Wait_Handle_Round_End(round_Number, nShili, a_Score_Number, b_Score_Number, final_Victory_Ground, round_Result_Data));
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000577F File Offset: 0x0000397F
	private IEnumerator Wait_Handle_Round_End(int _Round_Number, int _Country, int _A_Score_Number, int _B_Score_Number, int _Final_Victory_Ground, ArrayList _Round_Result_Data)
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
		bool flag2 = _Final_Victory_Ground == 3;
		if (flag2)
		{
			_Result = 0;
			_Final_Victory_Ground = 2;
		}
		else
		{
			bool flag3 = _Country == _Final_Victory_Ground;
			if (flag3)
			{
				_Result = 1;
				_Final_Victory_Ground = 1;
			}
			else
			{
				_Result = 2;
				_Final_Victory_Ground = 3;
			}
		}
		arrayList.Add(_Result);
		bool flag4 = _Country == 1;
		if (flag4)
		{
			arrayList.Add(_A_Score_Number);
			arrayList.Add(_B_Score_Number);
		}
		else
		{
			arrayList.Add(_B_Score_Number);
			arrayList.Add(_A_Score_Number);
		}
		int num;
		for (i = 0; i < _Round_Result_Data.Count; i = num + 1)
		{
			bool flag5 = _Country == 2;
			if (flag5)
			{
				bool flag6 = int.Parse(_Round_Result_Data[i].ToString()) == 2;
				if (flag6)
				{
					_Round_Result_Data[i] = 1;
				}
				else
				{
					bool flag7 = int.Parse(_Round_Result_Data[i].ToString()) == 1;
					if (flag7)
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

	// Token: 0x0400009E RID: 158
	private Arena_Score_View _View_Component;

	// Token: 0x0400009F RID: 159
	private Arena_Score_Model _Model_Component;

	// Token: 0x040000A0 RID: 160
	private bool _Fight_Prepare_State;

	// Token: 0x040000A1 RID: 161
	private bool _Fight_State;

	// Token: 0x040000A2 RID: 162
	private int _Substitution_State;

	// Token: 0x040000A3 RID: 163
	private bool _Door_State;
}

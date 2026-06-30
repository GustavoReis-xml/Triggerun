using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class Scramble_Controller : MonoBehaviour
{
	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x0600222D RID: 8749 RVA: 0x0013D6C8 File Offset: 0x0013B8C8
	// (set) Token: 0x0600222E RID: 8750 RVA: 0x0013D726 File Offset: 0x0013B926
	public Scramble_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Scramble_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Scramble_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x0600222F RID: 8751 RVA: 0x0013D730 File Offset: 0x0013B930
	// (set) Token: 0x06002230 RID: 8752 RVA: 0x0013D78E File Offset: 0x0013B98E
	public Scramble_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Scramble_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Scramble_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06002231 RID: 8753 RVA: 0x0013D798 File Offset: 0x0013B998
	// (set) Token: 0x06002232 RID: 8754 RVA: 0x0013D7B0 File Offset: 0x0013B9B0
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

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06002233 RID: 8755 RVA: 0x0013D7BC File Offset: 0x0013B9BC
	// (set) Token: 0x06002234 RID: 8756 RVA: 0x0013D7D4 File Offset: 0x0013B9D4
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

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06002235 RID: 8757 RVA: 0x0013D7E0 File Offset: 0x0013B9E0
	// (set) Token: 0x06002236 RID: 8758 RVA: 0x0013D7F8 File Offset: 0x0013B9F8
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

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06002237 RID: 8759 RVA: 0x0013D804 File Offset: 0x0013BA04
	// (set) Token: 0x06002238 RID: 8760 RVA: 0x0013D81C File Offset: 0x0013BA1C
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

	// Token: 0x06002239 RID: 8761 RVA: 0x0013D826 File Offset: 0x0013BA26
	public void Initialize()
	{
		this.View_Component.Initialize(this.Fight_State);
		ScrambleCon.inst.View_Component = this.View_Component;
		ScrambleCon.inst.Controler_Component = this;
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x0013D858 File Offset: 0x0013BA58
	public void User_Data(ArrayList _Parameters)
	{
		this.Set_User_Data(_Parameters);
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x0013D864 File Offset: 0x0013BA64
	public void Set_User_Data(ArrayList _Parameters)
	{
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
				int forces = int.Parse(array[3].ToString());
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < array.Length; j++)
				{
					arrayList.Add(array[j].ToString());
				}
				this.Model_Component.Set_User_Data(forces, arrayList);
			}
		}
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x0013D934 File Offset: 0x0013BB34
	public void Score_Data(ArrayList _Parameters)
	{
		this.Model_Component.A_Score = (int)_Parameters[0];
		this.Model_Component.B_Score = (int)_Parameters[1];
		this.View_Component.Display_Socre(this.Model_Component.A_Score, this.Model_Component.B_Score);
	}

	// Token: 0x0600223D RID: 8765 RVA: 0x0013D994 File Offset: 0x0013BB94
	public void Fight_Data(ArrayList _Parameters)
	{
		this.Set_Fight_Data(_Parameters);
		this.View_Component.Display_Area(this.Model_Component.Get_Area_Data());
		bool fight_State = this.Fight_State;
		if (fight_State)
		{
			this.Area_State_Data();
		}
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x0013D9D4 File Offset: 0x0013BBD4
	public void Set_Fight_Data(ArrayList _Parameters)
	{
		for (int i = 0; i < _Parameters.Count; i++)
		{
			string text = _Parameters[i].ToString();
			bool flag = text == string.Empty || text == string.Empty;
			if (flag)
			{
				break;
			}
			string[] array = text.Split(new char[]
			{
				'-'
			});
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < array.Length; j++)
			{
				arrayList.Add(array[j].ToString());
			}
			this.Model_Component.Set_Area_Data(arrayList);
		}
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x0013DA8C File Offset: 0x0013BC8C
	private void OnSetChangeTime(string sTime)
	{
		bool flag = sTime == string.Empty;
		if (flag)
		{
			ScrambleCon.inst.m_dStartChangeTime = DateTime.MinValue;
		}
		else
		{
			ScrambleCon.inst.m_dStartChangeTime = DateTime.Parse(sTime);
		}
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x0013DAD0 File Offset: 0x0013BCD0
	public void Countdown_Data(ArrayList _Parameters)
	{
		int num = (int)_Parameters[0];
		bool flag = !this.Fight_State;
		if (flag)
		{
			this.Fight_State = (int.Parse(_Parameters[1].ToString()) == 1);
		}
		this.Substitution_State = int.Parse(_Parameters[2].ToString());
		ScrambleCon.inst.m_nGameState = int.Parse(_Parameters[1].ToString());
		ScrambleCon.inst.Fight_State = this.Fight_State;
		ScrambleCon.inst.Substitution_State = this.Substitution_State;
		ScrambleCon.inst.Limit_Time = _Parameters[3].ToString();
		this.OnSetChangeTime(ScrambleCon.inst.Limit_Time);
		this.View_Component.Display_Countdown(num);
		this.View_Component.Display_Or_Hidden_Arrows(this.Fight_State);
		bool fight_State = this.Fight_State;
		if (fight_State)
		{
			bool flag2 = !this.Door_State;
			if (flag2)
			{
				this.View_Component.Hidden_Prepare_Describe_Text();
				this.Door_State = true;
				Door_Control.Get_Instantiate.General_Door_Handle(1, "close_open", true);
				Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
			}
		}
		else
		{
			bool flag3 = num == 5 && this.Fight_Prepare_State;
			if (flag3)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[878];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			}
			bool flag4 = num >= 0 && num <= 5 && this.Fight_Prepare_State;
			if (flag4)
			{
				this.View_Component.Display_Describe(num);
			}
		}
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x0013DC78 File Offset: 0x0013BE78
	public void Area_State_Data()
	{
		string text = string.Empty;
		List<Scramble_Area_Entity> area_Data = this.Model_Component.Get_Area_Data();
		for (int i = 0; i < area_Data.Count; i++)
		{
			bool flag = !(area_Data[i].Describe == string.Empty);
			if (flag)
			{
				text = area_Data[i].Describe;
				area_Data[i].Describe = string.Empty;
			}
		}
		bool flag2 = !(text == string.Empty);
		if (flag2)
		{
			this.View_Component.Display_Describe(text);
		}
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x0013DD16 File Offset: 0x0013BF16
	public void Fight_Prepare()
	{
		this.Fight_Prepare_State = true;
		fGameManager.Heroplayer.canControl = true;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x0013DD2C File Offset: 0x0013BF2C
	public void Fight_Start()
	{
		this.Fight_Prepare_State = false;
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[732];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x0013DD88 File Offset: 0x0013BF88
	public void Fight_Result(ArrayList _Parameters)
	{
		this.Fight_State = false;
		ScrambleCon.inst.Fight_State = this.Fight_State;
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

	// Token: 0x06002245 RID: 8773 RVA: 0x0013DEAE File Offset: 0x0013C0AE
	public void Fight_End()
	{
		this.Fight_Prepare_State = false;
		this.Fight_State = false;
		this.Door_State = false;
		this.View_Component.Display_Or_Hidden_Arrows(false);
		Door_Control.Get_Instantiate.Parent_Object = null;
		fUIManager.GetInstance().GotoHall(0);
	}

	// Token: 0x04001F7C RID: 8060
	private Scramble_View _View_Component;

	// Token: 0x04001F7D RID: 8061
	private Scramble_Model _Model_Component;

	// Token: 0x04001F7E RID: 8062
	private bool _Fight_Prepare_State;

	// Token: 0x04001F7F RID: 8063
	private bool _Fight_State;

	// Token: 0x04001F80 RID: 8064
	private int _Substitution_State;

	// Token: 0x04001F81 RID: 8065
	private bool _Door_State;
}

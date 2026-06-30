using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E8 RID: 744
public class Scramble_View : MonoBehaviour
{
	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x060022AC RID: 8876 RVA: 0x0013F458 File Offset: 0x0013D658
	// (set) Token: 0x060022AD RID: 8877 RVA: 0x0013F4B6 File Offset: 0x0013D6B6
	public Scramble_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Scramble_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Scramble_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x060022AE RID: 8878 RVA: 0x0013F4C0 File Offset: 0x0013D6C0
	// (set) Token: 0x060022AF RID: 8879 RVA: 0x0013F4D8 File Offset: 0x0013D6D8
	public bool Hold_Progress_Display_State
	{
		get
		{
			return this._Hold_Progress_Display_State;
		}
		set
		{
			this._Hold_Progress_Display_State = value;
		}
	}

	// Token: 0x060022B0 RID: 8880 RVA: 0x0013F4E4 File Offset: 0x0013D6E4
	public void Initialize(bool _Fight_State)
	{
		this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.one, 0f);
		this.UI_Component.Describe_Text.text = string.Empty;
		this.Display_Socre(0, 0);
		this.Display_Single_Area(null, this.UI_Component.A_Group_Object, this.UI_Component.A_Image_Progress_Effect_Component, fUIManager.MapEffectObj1, this.UI_Component.Area_UI_Position_Rotation_1);
		this.Display_Single_Area(null, this.UI_Component.B_Group_Object, this.UI_Component.B_Image_Progress_Effect_Component, fUIManager.MapEffectObj2, this.UI_Component.Area_UI_Position_Rotation_2);
		this.Display_Single_Area(null, this.UI_Component.C_Group_Object, this.UI_Component.C_Image_Progress_Effect_Component, fUIManager.MapEffectObj3, this.UI_Component.Area_UI_Position_Rotation_3);
		this.UI_Component.Victory_Effect.SetActive(false);
		this.UI_Component.Draw_Effect.SetActive(false);
		this.UI_Component.Failure_Effect.SetActive(false);
	}

	// Token: 0x060022B1 RID: 8881 RVA: 0x0013F5F1 File Offset: 0x0013D7F1
	public void Hidden_Prepare_Describe_Text()
	{
		this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.zero, 0f);
	}

	// Token: 0x060022B2 RID: 8882 RVA: 0x0013F614 File Offset: 0x0013D814
	public void Display_Countdown(int _Value)
	{
		bool flag = _Value < 0;
		if (flag)
		{
			this.UI_Component.Remaining_Time_Text.text = Language.GetLang(150002, new object[0]);
		}
		else
		{
			this.UI_Component.Remaining_Time_Text.text = Fun.sencondsToHMS_CXP(_Value);
		}
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x0013F668 File Offset: 0x0013D868
	public void Display_Describe(int _Value)
	{
		bool flag = _Value > 0 && _Value <= 5;
		if (flag)
		{
			this.UI_Component.Countdown_Effect.transform.Find(_Value.ToString()).gameObject.SetActive(true);
		}
		else
		{
			bool flag2 = _Value == 0;
			if (flag2)
			{
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.one, 0f);
				this.UI_Component.Describe_Text.gameObject.SetActive(true);
				this.UI_Component.Describe_Text.text = Language.GetLang(103004, new object[0]);
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.zero, 1f);
				this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.zero, 1f);
				this.UI_Component.Countdown_Effect.transform.Find("1").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("2").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("3").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("4").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("5").gameObject.SetActive(false);
			}
			else
			{
				this.UI_Component.Describe_Text.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x0013F838 File Offset: 0x0013DA38
	public void Display_Describe(string _Value)
	{
		base.StopCoroutine(this.Wait_Display_Describe());
		this.UI_Component.Describe_Text.transform.DOScale(Vector3.one, 0f);
		this.UI_Component.Describe_Text.text = _Value;
		base.StartCoroutine(this.Wait_Display_Describe());
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x0013F892 File Offset: 0x0013DA92
	private IEnumerator Wait_Display_Describe()
	{
		yield return new WaitForSeconds(2f);
		this.UI_Component.Describe_Text.transform.DOScale(Vector3.zero, 1f);
		yield break;
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x0013F8A4 File Offset: 0x0013DAA4
	public void Display_Socre(int _A_Score, int _B_Score)
	{
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			this.UI_Component.A_Score_Text.text = _A_Score.ToString();
			this.UI_Component.A_Progress_Slider.value = (float)_A_Score / 5000f;
			this.UI_Component.B_Score_Text.text = _B_Score.ToString();
			this.UI_Component.B_Progress_Slider.value = (float)_B_Score / 5000f;
			this.SetScore(_A_Score);
		}
		else
		{
			this.UI_Component.A_Score_Text.text = _B_Score.ToString();
			this.UI_Component.A_Progress_Slider.value = (float)_B_Score / 5000f;
			this.UI_Component.B_Score_Text.text = _A_Score.ToString();
			this.UI_Component.B_Progress_Slider.value = (float)_A_Score / 5000f;
			this.SetScore(_B_Score);
		}
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x0013F9A4 File Offset: 0x0013DBA4
	public void Display_Area(List<Scramble_Area_Entity> _Parameters)
	{
		GameObject parent_Object = null;
		GameObject map_Effect_Object = null;
		Progress_Effect_Handle_Image progress_Effect_Handle_Component = null;
		UI_Position_Rotation ui_Position_Rotation_Component = null;
		this.Hold_Progress_Display_State = false;
		for (int i = 0; i < _Parameters.Count; i++)
		{
			Scramble_Area_Entity scramble_Area_Entity = _Parameters[i];
			bool flag = scramble_Area_Entity.Number == 1;
			if (flag)
			{
				parent_Object = this.UI_Component.A_Group_Object;
				progress_Effect_Handle_Component = this.UI_Component.A_Image_Progress_Effect_Component;
				ui_Position_Rotation_Component = this.UI_Component.Area_UI_Position_Rotation_1;
				map_Effect_Object = fUIManager.MapEffectObj1;
			}
			else
			{
				bool flag2 = scramble_Area_Entity.Number == 2;
				if (flag2)
				{
					parent_Object = this.UI_Component.B_Group_Object;
					progress_Effect_Handle_Component = this.UI_Component.B_Image_Progress_Effect_Component;
					ui_Position_Rotation_Component = this.UI_Component.Area_UI_Position_Rotation_2;
					map_Effect_Object = fUIManager.MapEffectObj2;
				}
				else
				{
					bool flag3 = scramble_Area_Entity.Number == 3;
					if (flag3)
					{
						parent_Object = this.UI_Component.C_Group_Object;
						progress_Effect_Handle_Component = this.UI_Component.C_Image_Progress_Effect_Component;
						ui_Position_Rotation_Component = this.UI_Component.Area_UI_Position_Rotation_3;
						map_Effect_Object = fUIManager.MapEffectObj3;
					}
				}
			}
			this.Display_Single_Area(scramble_Area_Entity, parent_Object, progress_Effect_Handle_Component, map_Effect_Object, ui_Position_Rotation_Component);
		}
		bool flag4 = !this.Hold_Progress_Display_State;
		if (flag4)
		{
			this.UI_Component.Hold_Progress_Effect_Component.Progress_Effect_Handle_Image_Component = null;
		}
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x0013FADC File Offset: 0x0013DCDC
	public void Display_Single_Area(Scramble_Area_Entity _Scramble_Area_Entity, GameObject _Parent_Object, Progress_Effect_Handle_Image _Progress_Effect_Handle_Component, GameObject _Map_Effect_Object, UI_Position_Rotation _UI_Position_Rotation_Component)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int max_Value = 0;
		int num4 = 0;
		int number = 0;
		int number2 = 0;
		List<int> list = new List<int>();
		bool flag = _Scramble_Area_Entity != null;
		if (flag)
		{
			num = _Scramble_Area_Entity.Occupy_State;
			num2 = _Scramble_Area_Entity.Occupy_A_Current_Progress;
			num3 = _Scramble_Area_Entity.Occupy_B_Current_Progress;
			max_Value = _Scramble_Area_Entity.Occupy_Max_Progress;
			num4 = _Scramble_Area_Entity.State;
			number = _Scramble_Area_Entity.A_Amount;
			number2 = _Scramble_Area_Entity.B_Amount;
			list = _Scramble_Area_Entity.Scramble_User_ID_List;
		}
		Image component = _Parent_Object.transform.Find("Background").GetComponent<Image>();
		bool flag2 = _Map_Effect_Object != null;
		if (flag2)
		{
			bool flag3 = PlayerDataManager.getInstance().playercom.m_nShili == num;
			if (flag3)
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_mubiaodise_youfang");
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(true);
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
			}
			else
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_mubiaodise_difang");
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(false);
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(true);
			}
		}
		bool flag4 = num == 0;
		if (flag4)
		{
			component.sprite = Window.spriteName("scramble_atlas", "judiankong");
			bool flag5 = _Map_Effect_Object != null;
			if (flag5)
			{
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(true);
				_Map_Effect_Object.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(true);
			}
		}
		int current_Value = (num3 <= 0) ? num2 : num3;
		_Progress_Effect_Handle_Component.Display_State = true;
		Text component2 = _Parent_Object.transform.Find("State").gameObject.GetComponent<Text>();
		bool flag6 = num4 == 0;
		if (flag6)
		{
			_Parent_Object.transform.Find("State").gameObject.SetActive(false);
			component2.text = string.Empty;
		}
		else
		{
			bool flag7 = num4 == 1 || num4 == 2;
			if (flag7)
			{
				component2.text = Language.GetLang(150005, new object[0]);
			}
			else
			{
				component2.text = Language.GetLang(150006, new object[0]);
				_Progress_Effect_Handle_Component.Display_State = false;
			}
			_Parent_Object.transform.Find("State").gameObject.SetActive(true);
		}
		_Progress_Effect_Handle_Component.Current_Value = current_Value;
		_Progress_Effect_Handle_Component.Max_Value = max_Value;
		component = _Parent_Object.transform.Find("Image_Progress").gameObject.GetComponent<Image>();
		bool flag8 = PlayerDataManager.getInstance().playercom.m_nShili == num4;
		if (flag8)
		{
			component.color = new Color(0f, 200f, 255f, 255f);
		}
		else
		{
			bool flag9 = num4 != 0;
			if (flag9)
			{
				component.color = new Color(255f, 0f, 0f, 255f);
			}
		}
		bool flag10 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag10)
		{
			this.Display_Area_User_Number(number, _Parent_Object.transform.Find("A_Number").gameObject);
			this.Display_Area_User_Number(number2, _Parent_Object.transform.Find("B_Number").gameObject);
		}
		else
		{
			this.Display_Area_User_Number(number2, _Parent_Object.transform.Find("A_Number").gameObject);
			this.Display_Area_User_Number(number, _Parent_Object.transform.Find("B_Number").gameObject);
		}
		bool flag11 = _UI_Position_Rotation_Component != null;
		if (flag11)
		{
			_UI_Position_Rotation_Component.Update_Color(num);
		}
		for (int i = 0; i < list.Count; i++)
		{
			bool flag12 = fGameManager.GetInstance().getPlayer().getNetid() == list[i] && num != PlayerDataManager.getInstance().playercom.m_nShili;
			if (flag12)
			{
				this.Hold_Progress_Display_State = true;
				this.UI_Component.Hold_Progress_Effect_Component.Progress_Effect_Handle_Image_Component = _Progress_Effect_Handle_Component;
			}
		}
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x0013FF38 File Offset: 0x0013E138
	public void Display_Area_User_Number(int _Number, GameObject _Parent_Object)
	{
		bool flag = _Number > 0;
		if (flag)
		{
			_Parent_Object.SetActive(true);
			_Parent_Object.transform.Find("Text").GetComponent<Text>().text = _Number.ToString();
		}
		else
		{
			_Parent_Object.SetActive(false);
		}
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x0013FF88 File Offset: 0x0013E188
	public void Display_Fight_Result()
	{
		GameObject draw_Effect = this.UI_Component.Draw_Effect;
		draw_Effect.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_Result(draw_Effect));
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x0013FFBC File Offset: 0x0013E1BC
	public void Display_Fight_Result(bool _State)
	{
		GameObject gameObject = (!_State) ? this.UI_Component.Failure_Effect : this.UI_Component.Victory_Effect;
		gameObject.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_Result(gameObject));
	}

	// Token: 0x060022BC RID: 8892 RVA: 0x0013FFFE File Offset: 0x0013E1FE
	private IEnumerator Wait_Hidden_Fight_Result(GameObject _Parent_Object)
	{
		yield return new WaitForSeconds(8f);
		_Parent_Object.SetActive(false);
		yield break;
	}

	// Token: 0x060022BD RID: 8893 RVA: 0x00140014 File Offset: 0x0013E214
	public void Display_Or_Hidden_Arrows(bool _Value)
	{
		bool flag = this.UI_Component.Area_UI_Position_Rotation_1 != null;
		if (flag)
		{
			this.UI_Component.Area_UI_Position_Rotation_1.Fight_State = _Value;
		}
		bool flag2 = this.UI_Component.Area_UI_Position_Rotation_2 != null;
		if (flag2)
		{
			this.UI_Component.Area_UI_Position_Rotation_2.Fight_State = _Value;
		}
		bool flag3 = this.UI_Component.Area_UI_Position_Rotation_3 != null;
		if (flag3)
		{
			this.UI_Component.Area_UI_Position_Rotation_3.Fight_State = _Value;
		}
	}

	// Token: 0x060022BE RID: 8894 RVA: 0x001400A0 File Offset: 0x0013E2A0
	private void SetScore(int nScore)
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel > 10;
		if (!flag)
		{
			bool flag2 = this.nOldScore != nScore;
			if (flag2)
			{
				this.m_sStartAddTime = Fun.GetServerDate().ToString();
				this.nOldScore = nScore;
			}
			else
			{
				int num = 0;
				bool flag3 = this.m_sStartAddTime != string.Empty;
				if (flag3)
				{
					num = (int)(Fun.GetServerDate() - DateTime.Parse(this.m_sStartAddTime)).TotalSeconds;
				}
				bool flag4 = num % 30 == 0 && num >= 30 && gameuiCon.inst != null;
				if (flag4)
				{
					gameuiCon.inst.mainMsgUI.OnShowNewPlayerMsg(Language.GetLang(2046, new object[0]));
				}
			}
		}
	}

	// Token: 0x04001FB8 RID: 8120
	private Scramble_UI _UI_Component;

	// Token: 0x04001FB9 RID: 8121
	private bool _Hold_Progress_Display_State;

	// Token: 0x04001FBA RID: 8122
	private string m_sStartAddTime = string.Empty;

	// Token: 0x04001FBB RID: 8123
	private int nOldScore = -1;
}

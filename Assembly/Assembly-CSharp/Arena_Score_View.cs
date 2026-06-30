using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class Arena_Score_View : MonoBehaviour
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006420 File Offset: 0x00004620
	// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000647E File Offset: 0x0000467E
	public Arena_Score_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Arena_Score_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Arena_Score_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006488 File Offset: 0x00004688
	// (set) Token: 0x060000F8 RID: 248 RVA: 0x000064A0 File Offset: 0x000046A0
	public float Countdown
	{
		get
		{
			return this._Countdown;
		}
		set
		{
			this._Countdown = value;
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000064AC File Offset: 0x000046AC
	public void Display_Prepare_State_Describe(bool _Value, float _Time)
	{
		this.UI_Component.Effect_Object.SetActive(_Value);
		this.UI_Component.Prepare_Describe_Text.gameObject.SetActive(_Value);
		bool flag = !_Value;
		if (flag)
		{
			this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.zero, _Time);
		}
		else
		{
			this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.one, 0f);
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00006530 File Offset: 0x00004730
	public void Display_Fight_State_Describe(string _Value)
	{
		this.UI_Component.Fight_Describe_Text.text = _Value;
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.zero, 0f);
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.one, 1f);
		this.UI_Component.Effect_Object.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_State_Describe());
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000065AF File Offset: 0x000047AF
	public IEnumerator Wait_Hidden_Fight_State_Describe()
	{
		yield return new WaitForSeconds(4f);
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.zero, 1f);
		this.UI_Component.Effect_Object.SetActive(false);
		yield break;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000065BE File Offset: 0x000047BE
	public void Display_Fight_Data(bool _Fight_State, int _Max_Score, int _A_Score_Number, int _B_Score_Number)
	{
		this.Display_Max_Score(_Max_Score);
		this.Display_Socre(_A_Score_Number, _B_Score_Number);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000065D3 File Offset: 0x000047D3
	public void Display_Max_Score(int _Max_Score)
	{
		this.UI_Component.Max_Score_Text.text = _Max_Score.ToString();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000065F0 File Offset: 0x000047F0
	public void Display_Socre(int _A_Score_Number, int _B_Score_Number)
	{
		bool flag = PlayerDataManager.getInstance().playerCommon.m_nShili == 1;
		if (flag)
		{
			this.UI_Component.A_Score_Text.text = _A_Score_Number.ToString();
			this.UI_Component.B_Score_Text.text = _B_Score_Number.ToString();
		}
		else
		{
			this.UI_Component.A_Score_Text.text = _B_Score_Number.ToString();
			this.UI_Component.B_Score_Text.text = _A_Score_Number.ToString();
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000667C File Offset: 0x0000487C
	public void Display_Countdown(int _Number)
	{
		bool flag = _Number < 0;
		if (flag)
		{
			this.UI_Component.Remaining_Time_Text.text = Language.GetLang(150002, new object[0]);
		}
		else
		{
			this.UI_Component.Remaining_Time_Text.text = Fun.sencondsToHMS_CXP(_Number);
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000066D0 File Offset: 0x000048D0
	public void Display_Countdown_Describe(int _Number)
	{
		bool flag = _Number > 0 && _Number <= 5;
		if (flag)
		{
			this.UI_Component.Countdown_Effect.transform.Find(_Number.ToString()).gameObject.SetActive(true);
		}
		else
		{
			bool flag2 = _Number == 0;
			if (flag2)
			{
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.one, 0f);
				this.UI_Component.Describe_Text.gameObject.SetActive(true);
				this.UI_Component.Describe_Text.text = Language.GetLang(150001, new object[0]);
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.zero, 1f);
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

	// Token: 0x06000101 RID: 257 RVA: 0x0000687D File Offset: 0x00004A7D
	public void Round_End(int _Round_Number)
	{
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006880 File Offset: 0x00004A80
	public void Display_Fight_Result()
	{
		GameObject draw_Effect = this.UI_Component.Draw_Effect;
		draw_Effect.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_Result(draw_Effect));
	}

	// Token: 0x06000103 RID: 259 RVA: 0x000068B4 File Offset: 0x00004AB4
	public void Display_Fight_Result(bool _State)
	{
		GameObject gameObject = (!_State) ? this.UI_Component.Failure_Effect : this.UI_Component.Victory_Effect;
		gameObject.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_Result(gameObject));
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000068F6 File Offset: 0x00004AF6
	private IEnumerator Wait_Hidden_Fight_Result(GameObject _Parent_Object)
	{
		yield return new WaitForSeconds(8f);
		_Parent_Object.SetActive(false);
		yield break;
	}

	// Token: 0x040000C4 RID: 196
	private Arena_Score_UI _UI_Component;

	// Token: 0x040000C5 RID: 197
	private float _Countdown;
}

using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class Setting_Controller : MonoBehaviour
{
	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x0600230F RID: 8975 RVA: 0x0014343C File Offset: 0x0014163C
	// (set) Token: 0x06002310 RID: 8976 RVA: 0x0014349A File Offset: 0x0014169A
	public Setting_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Setting_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Setting_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x06002311 RID: 8977 RVA: 0x001434A4 File Offset: 0x001416A4
	// (set) Token: 0x06002312 RID: 8978 RVA: 0x001434BC File Offset: 0x001416BC
	public int Selected_Hero_Control_Index
	{
		get
		{
			return this._Selected_Hero_Control_Index;
		}
		set
		{
			this._Selected_Hero_Control_Index = value;
		}
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x001434C8 File Offset: 0x001416C8
	public object Get_Property_Value(string _Tag_Name, string _Property_Name)
	{
		return this.Model_Component.Get_Property_Value(_Tag_Name, _Property_Name);
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x001434EC File Offset: 0x001416EC
	public bool Set_Property_Value(string _Tag_Name, string _Property_Name, object _Property_Value)
	{
		return this.Model_Component.Set_Property_Value(_Tag_Name, _Property_Name, _Property_Value);
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x00143510 File Offset: 0x00141710
	public int Detection_Modify_Value(int _Value, int _Min, int _Max)
	{
		bool flag = _Value < _Min;
		int result;
		if (flag)
		{
			result = _Min;
		}
		else
		{
			bool flag2 = _Value > _Max;
			if (flag2)
			{
				result = _Max;
			}
			else
			{
				result = _Value;
			}
		}
		return result;
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x0014353F File Offset: 0x0014173F
	public void Initialize_Hero_Control_Data()
	{
		this.Model_Component.Initialize_Hero_Control_Data();
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x0014354E File Offset: 0x0014174E
	public void Restore_Hero_Control_Data()
	{
		this.Model_Component.Restore_Hero_Control_Data();
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x0014355D File Offset: 0x0014175D
	public void Restore_Video_Setting()
	{
		this.Model_Component.Restore_Video_Setting();
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x0014356C File Offset: 0x0014176C
	public void Restore_Voice_Setting()
	{
		this.Model_Component.Restore_Voice_Setting();
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x0014357B File Offset: 0x0014177B
	public void Restore_Game_Setting()
	{
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x0014357E File Offset: 0x0014177E
	public void Restore_Control_Setting()
	{
		this.Model_Component.Restore_Control_Setting();
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x0014358D File Offset: 0x0014178D
	public void Restore_Social_Setting()
	{
		this.Model_Component.Restore_Social_Setting();
	}

	// Token: 0x04001FFD RID: 8189
	private Setting_Model _Model_Component;

	// Token: 0x04001FFE RID: 8190
	private int _Selected_Hero_Control_Index;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class Npc_AI_Model : MonoBehaviour
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00105F00 File Offset: 0x00104100
	// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x00105F18 File Offset: 0x00104118
	public Npc_AI_Entity Entity_Component
	{
		get
		{
			return this._Entity_Component;
		}
		set
		{
			this._Entity_Component = value;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00105F24 File Offset: 0x00104124
	// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00105F5C File Offset: 0x0010415C
	public Dictionary<int, ArrayList> Node_Data
	{
		get
		{
			bool flag = this._Node_Data == null;
			if (flag)
			{
				this._Node_Data = this.Entity_Component.Node_Data;
			}
			return this._Node_Data;
		}
		set
		{
			this._Node_Data = value;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00105F68 File Offset: 0x00104168
	// (set) Token: 0x06001BDA RID: 7130 RVA: 0x00105F80 File Offset: 0x00104180
	public int Current_Node_Number
	{
		get
		{
			return this._Current_Node_Number;
		}
		set
		{
			this._Current_Node_Number = value;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00105F8C File Offset: 0x0010418C
	// (set) Token: 0x06001BDC RID: 7132 RVA: 0x00105FA4 File Offset: 0x001041A4
	public Vector3 Next_Node_Position
	{
		get
		{
			return this._Next_Node_Position;
		}
		set
		{
			this._Next_Node_Position = value;
		}
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x00105FAE File Offset: 0x001041AE
	public void Initialize(int _Index)
	{
		this.Entity_Component = fGameManager.mloadConfig._Npc_AI_Xml_Data[_Index];
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x00105FC8 File Offset: 0x001041C8
	public ArrayList Get_Previous_Node_Data()
	{
		bool flag = !this.Node_Data.ContainsKey(this.Current_Node_Number - 1);
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.Node_Data[this.Current_Node_Number - 1];
		}
		return result;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x0010600C File Offset: 0x0010420C
	public Vector3 Get_Previous_Node_Position()
	{
		ArrayList previous_Node_Data = this.Get_Previous_Node_Data();
		bool flag = previous_Node_Data == null;
		Vector3 result;
		if (flag)
		{
			result = this.Get_Next_Node_Position();
		}
		else
		{
			result = (Vector3)previous_Node_Data[0];
		}
		return result;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00106048 File Offset: 0x00104248
	public ArrayList Get_Current_Node_Data()
	{
		bool flag = !this.Node_Data.ContainsKey(this.Current_Node_Number);
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.Node_Data[this.Current_Node_Number];
		}
		return result;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x00106088 File Offset: 0x00104288
	public Vector3 Get_Current_Node_Position()
	{
		ArrayList current_Node_Data = this.Get_Current_Node_Data();
		bool flag = current_Node_Data == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = (Vector3)current_Node_Data[0];
		}
		return result;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x001060C0 File Offset: 0x001042C0
	public ArrayList Get_Next_Node_Data()
	{
		bool flag = !this.Node_Data.ContainsKey(this.Current_Node_Number + 1);
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.Node_Data[this.Current_Node_Number + 1];
		}
		return result;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00106104 File Offset: 0x00104304
	public Vector3 Get_Next_Node_Position()
	{
		ArrayList next_Node_Data = this.Get_Next_Node_Data();
		bool flag = next_Node_Data == null;
		Vector3 result;
		if (flag)
		{
			result = this.Get_Previous_Node_Position();
		}
		else
		{
			result = (Vector3)next_Node_Data[0];
		}
		return result;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x00106140 File Offset: 0x00104340
	public float Get_Current_Node_Wait_Time()
	{
		ArrayList current_Node_Data = this.Get_Current_Node_Data();
		return (float)int.Parse(current_Node_Data[1].ToString());
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x00106173 File Offset: 0x00104373
	public void Modify_Current_Node_Number(int _Value)
	{
		this.Current_Node_Number = _Value;
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x00106180 File Offset: 0x00104380
	public int Get_Current_Node_Number()
	{
		return this.Current_Node_Number;
	}

	// Token: 0x04001913 RID: 6419
	private Npc_AI_Entity _Entity_Component;

	// Token: 0x04001914 RID: 6420
	private Dictionary<int, ArrayList> _Node_Data;

	// Token: 0x04001915 RID: 6421
	private int _Current_Node_Number = 1;

	// Token: 0x04001916 RID: 6422
	private Vector3 _Next_Node_Position = Vector3.zero;
}

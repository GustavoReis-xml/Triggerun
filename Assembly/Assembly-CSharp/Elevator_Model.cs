using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class Elevator_Model : Npc_Model
{
	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0005C088 File Offset: 0x0005A288
	// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0005C0C5 File Offset: 0x0005A2C5
	public Dictionary<int, ArrayList> Node_Data
	{
		get
		{
			bool flag = this._Node_Data == null;
			if (flag)
			{
				this._Node_Data = ((Elevator_Entity)base.Entity_Component).Node_Data;
			}
			return this._Node_Data;
		}
		set
		{
			this._Node_Data = value;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0005C0D0 File Offset: 0x0005A2D0
	// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0005C0E8 File Offset: 0x0005A2E8
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

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0005C0F4 File Offset: 0x0005A2F4
	// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0005C10C File Offset: 0x0005A30C
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

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0005C118 File Offset: 0x0005A318
	// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0005C150 File Offset: 0x0005A350
	public Elevator_Entity Elevator_Entity_Component
	{
		get
		{
			bool flag = this._Elevator_Entity_Component == null;
			if (flag)
			{
				this._Elevator_Entity_Component = (Elevator_Entity)base.Entity_Component;
			}
			return this._Elevator_Entity_Component;
		}
		set
		{
			this._Elevator_Entity_Component = value;
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0005C15C File Offset: 0x0005A35C
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

	// Token: 0x06000A2B RID: 2603 RVA: 0x0005C1A0 File Offset: 0x0005A3A0
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

	// Token: 0x06000A2C RID: 2604 RVA: 0x0005C1DC File Offset: 0x0005A3DC
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

	// Token: 0x06000A2D RID: 2605 RVA: 0x0005C21C File Offset: 0x0005A41C
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

	// Token: 0x06000A2E RID: 2606 RVA: 0x0005C254 File Offset: 0x0005A454
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

	// Token: 0x06000A2F RID: 2607 RVA: 0x0005C298 File Offset: 0x0005A498
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

	// Token: 0x06000A30 RID: 2608 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
	public float Get_Current_Node_Wait_Time()
	{
		ArrayList current_Node_Data = this.Get_Current_Node_Data();
		return (float)int.Parse(current_Node_Data[1].ToString());
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0005C307 File Offset: 0x0005A507
	public void Modify_Current_Node_Number(int _Value)
	{
		this.Current_Node_Number = _Value;
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0005C314 File Offset: 0x0005A514
	public int Get_Current_Node_Number()
	{
		return this.Current_Node_Number;
	}

	// Token: 0x0400082C RID: 2092
	private Dictionary<int, ArrayList> _Node_Data;

	// Token: 0x0400082D RID: 2093
	private int _Current_Node_Number = 1;

	// Token: 0x0400082E RID: 2094
	private Vector3 _Next_Node_Position = Vector3.zero;

	// Token: 0x0400082F RID: 2095
	private Elevator_Entity _Elevator_Entity_Component;
}

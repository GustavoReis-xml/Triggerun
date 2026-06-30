using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.Track;

// Token: 0x0200007C RID: 124
public class Chariot_Model : Npc_Model
{
	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000436 RID: 1078 RVA: 0x000218A0 File Offset: 0x0001FAA0
	// (set) Token: 0x06000437 RID: 1079 RVA: 0x000218B8 File Offset: 0x0001FAB8
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

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x000218C4 File Offset: 0x0001FAC4
	// (set) Token: 0x06000439 RID: 1081 RVA: 0x00021901 File Offset: 0x0001FB01
	public Dictionary<int, ArrayList> Node_Data
	{
		get
		{
			bool flag = this._Node_Data == null;
			if (flag)
			{
				this._Node_Data = ((Chariot_Entity)base.Entity_Component).Node_Data;
			}
			return this._Node_Data;
		}
		set
		{
			this._Node_Data = value;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x0002190C File Offset: 0x0001FB0C
	// (set) Token: 0x0600043B RID: 1083 RVA: 0x00021949 File Offset: 0x0001FB49
	public Dictionary<int, ArrayList> Pass_Node_Data
	{
		get
		{
			bool flag = this._Pass_Node_Data == null;
			if (flag)
			{
				this._Pass_Node_Data = ((Chariot_Entity)base.Entity_Component).Pass_Node_Data;
			}
			return this._Pass_Node_Data;
		}
		set
		{
			this._Pass_Node_Data = value;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x00021954 File Offset: 0x0001FB54
	// (set) Token: 0x0600043D RID: 1085 RVA: 0x0002196C File Offset: 0x0001FB6C
	public int Pass_Node_Number
	{
		get
		{
			return this._Pass_Node_Number;
		}
		set
		{
			this._Pass_Node_Number = value;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x0600043E RID: 1086 RVA: 0x00021978 File Offset: 0x0001FB78
	// (set) Token: 0x0600043F RID: 1087 RVA: 0x000219B1 File Offset: 0x0001FBB1
	public List<Track> Track_Object_List
	{
		get
		{
			bool flag = this._Track_Object_List == null;
			if (flag)
			{
				this._Track_Object_List = new List<Track>(UnityEngine.Object.FindObjectsOfType<Track>());
			}
			return this._Track_Object_List;
		}
		set
		{
			this._Track_Object_List = value;
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x000219BC File Offset: 0x0001FBBC
	public float Get_Move_Speed(int _Player_Amount)
	{
		float num = 0f;
		Dictionary<int, float> move_Speed = (base.Get_Entity() as Chariot_Entity).Move_Speed;
		bool flag = !move_Speed.ContainsKey(_Player_Amount);
		float result;
		if (flag)
		{
			result = num;
		}
		else
		{
			result = move_Speed[_Player_Amount];
		}
		return result;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00021A04 File Offset: 0x0001FC04
	public ArrayList Get_Previous_Node_Data()
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

	// Token: 0x06000442 RID: 1090 RVA: 0x00021A44 File Offset: 0x0001FC44
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

	// Token: 0x06000443 RID: 1091 RVA: 0x00021A84 File Offset: 0x0001FC84
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

	// Token: 0x06000444 RID: 1092 RVA: 0x00021AC8 File Offset: 0x0001FCC8
	public float Get_Current_Node_Wait_Time()
	{
		ArrayList current_Node_Data = this.Get_Current_Node_Data();
		return (float)int.Parse(current_Node_Data[1].ToString());
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00021AFC File Offset: 0x0001FCFC
	public bool Modify_Current_Node_Number(int _Value)
	{
		bool flag = _Value <= this.Current_Node_Number;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			this.Current_Node_Number = _Value;
			result = true;
		}
		return result;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00021B2C File Offset: 0x0001FD2C
	public int Get_Current_Node_Number()
	{
		return this.Current_Node_Number;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00021B44 File Offset: 0x0001FD44
	public bool Modify_Current_Pass_Node_Number(int _Value)
	{
		bool flag = _Value <= this.Pass_Node_Number;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			this.Pass_Node_Number = _Value;
			result = true;
		}
		return result;
	}

	// Token: 0x0400034E RID: 846
	private int _Current_Node_Number;

	// Token: 0x0400034F RID: 847
	private Dictionary<int, ArrayList> _Node_Data;

	// Token: 0x04000350 RID: 848
	private Dictionary<int, ArrayList> _Pass_Node_Data;

	// Token: 0x04000351 RID: 849
	private int _Pass_Node_Number;

	// Token: 0x04000352 RID: 850
	public List<Track> _Track_Object_List;
}

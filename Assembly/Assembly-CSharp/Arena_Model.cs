using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class Arena_Model : MonoBehaviour
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00004D58 File Offset: 0x00002F58
	// (set) Token: 0x0600007E RID: 126 RVA: 0x00004D8A File Offset: 0x00002F8A
	public Dictionary<int, Dictionary<int, Arena_User_Entity>> User_Data
	{
		get
		{
			bool flag = this._User_Data == null;
			if (flag)
			{
				this._User_Data = new Dictionary<int, Dictionary<int, Arena_User_Entity>>();
			}
			return this._User_Data;
		}
		set
		{
			this._User_Data = value;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600007F RID: 127 RVA: 0x00004D94 File Offset: 0x00002F94
	// (set) Token: 0x06000080 RID: 128 RVA: 0x00004DAC File Offset: 0x00002FAC
	public int Current_Round_Number
	{
		get
		{
			return this._Current_Round_Number;
		}
		set
		{
			this._Current_Round_Number = value;
			ArenaCon.inst.Current_Round = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000081 RID: 129 RVA: 0x00004DC4 File Offset: 0x00002FC4
	// (set) Token: 0x06000082 RID: 130 RVA: 0x00004DDC File Offset: 0x00002FDC
	public int A_Victory_Round_Number
	{
		get
		{
			return this._A_Victory_Round_Number;
		}
		set
		{
			this._A_Victory_Round_Number = value;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000083 RID: 131 RVA: 0x00004DE8 File Offset: 0x00002FE8
	// (set) Token: 0x06000084 RID: 132 RVA: 0x00004E00 File Offset: 0x00003000
	public int B_Victory_Round_Number
	{
		get
		{
			return this._B_Victory_Round_Number;
		}
		set
		{
			this._B_Victory_Round_Number = value;
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004E0A File Offset: 0x0000300A
	public void Clear()
	{
		this.User_Data.Clear();
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004E1C File Offset: 0x0000301C
	public void Set_User_Data(int _Forces, ArrayList _Parameters)
	{
		int num = int.Parse(_Parameters[0].ToString());
		bool flag = !this.User_Data.ContainsKey(_Forces);
		if (flag)
		{
			this.User_Data.Add(_Forces, new Dictionary<int, Arena_User_Entity>());
		}
		Dictionary<int, Arena_User_Entity> dictionary = this.User_Data[_Forces];
		bool flag2 = !dictionary.ContainsKey(num);
		if (flag2)
		{
			dictionary.Add(num, new Arena_User_Entity());
		}
		Arena_User_Entity arena_User_Entity = dictionary[num];
		arena_User_Entity.Set_Data(num, _Parameters);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004EA8 File Offset: 0x000030A8
	public ArrayList Get_User_Data(int _Forces)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.User_Data.ContainsKey(_Forces);
		if (flag)
		{
			Dictionary<int, Arena_User_Entity> dictionary = this.User_Data[_Forces];
			List<int> list = new List<int>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int key = list[i];
				arrayList.Add(dictionary[key]);
			}
		}
		return arrayList;
	}

	// Token: 0x04000094 RID: 148
	private Dictionary<int, Dictionary<int, Arena_User_Entity>> _User_Data;

	// Token: 0x04000095 RID: 149
	private int _Current_Round_Number;

	// Token: 0x04000096 RID: 150
	private int _A_Victory_Round_Number;

	// Token: 0x04000097 RID: 151
	private int _B_Victory_Round_Number;
}

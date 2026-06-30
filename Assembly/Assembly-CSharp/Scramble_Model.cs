using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class Scramble_Model : MonoBehaviour
{
	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x06002254 RID: 8788 RVA: 0x0013E274 File Offset: 0x0013C474
	// (set) Token: 0x06002255 RID: 8789 RVA: 0x0013E2A6 File Offset: 0x0013C4A6
	public Dictionary<int, Dictionary<int, Scramble_User_Entity>> User_Data
	{
		get
		{
			bool flag = this._User_Data == null;
			if (flag)
			{
				this._User_Data = new Dictionary<int, Dictionary<int, Scramble_User_Entity>>();
			}
			return this._User_Data;
		}
		set
		{
			this._User_Data = value;
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06002256 RID: 8790 RVA: 0x0013E2B0 File Offset: 0x0013C4B0
	// (set) Token: 0x06002257 RID: 8791 RVA: 0x0013E2E2 File Offset: 0x0013C4E2
	public Dictionary<int, Scramble_Area_Entity> Area_Data
	{
		get
		{
			bool flag = this._Area_Data == null;
			if (flag)
			{
				this._Area_Data = new Dictionary<int, Scramble_Area_Entity>();
			}
			return this._Area_Data;
		}
		set
		{
			this._Area_Data = value;
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06002258 RID: 8792 RVA: 0x0013E2EC File Offset: 0x0013C4EC
	// (set) Token: 0x06002259 RID: 8793 RVA: 0x0013E304 File Offset: 0x0013C504
	public int A_Score
	{
		get
		{
			return this._A_Score;
		}
		set
		{
			this._A_Score = value;
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x0600225A RID: 8794 RVA: 0x0013E310 File Offset: 0x0013C510
	// (set) Token: 0x0600225B RID: 8795 RVA: 0x0013E328 File Offset: 0x0013C528
	public int B_Score
	{
		get
		{
			return this._B_Score;
		}
		set
		{
			this._B_Score = value;
		}
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x0013E332 File Offset: 0x0013C532
	public void Clear()
	{
		this.User_Data.Clear();
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x0013E344 File Offset: 0x0013C544
	public void Set_User_Data(int _Forces, ArrayList _Parameters)
	{
		int num = int.Parse(_Parameters[0].ToString());
		bool flag = !this.User_Data.ContainsKey(_Forces);
		if (flag)
		{
			this.User_Data.Add(_Forces, new Dictionary<int, Scramble_User_Entity>());
		}
		Dictionary<int, Scramble_User_Entity> dictionary = this.User_Data[_Forces];
		bool flag2 = !dictionary.ContainsKey(num);
		if (flag2)
		{
			dictionary.Add(num, new Scramble_User_Entity());
		}
		Scramble_User_Entity scramble_User_Entity = dictionary[num];
		scramble_User_Entity.Set_Data(num, _Parameters);
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x0013E3D0 File Offset: 0x0013C5D0
	public ArrayList Get_User_Data(int _Value)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.User_Data.ContainsKey(_Value);
		if (flag)
		{
			Dictionary<int, Scramble_User_Entity> dictionary = this.User_Data[_Value];
			List<int> list = new List<int>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int key = list[i];
				arrayList.Add(dictionary[key]);
			}
		}
		return arrayList;
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x0013E458 File Offset: 0x0013C658
	public void Set_Area_Data(ArrayList _Parameters)
	{
		int num = int.Parse(_Parameters[0].ToString());
		bool flag = !this.Area_Data.ContainsKey(num);
		if (flag)
		{
			this.Area_Data.Add(num, new Scramble_Area_Entity());
		}
		Scramble_Area_Entity scramble_Area_Entity = this.Area_Data[num];
		scramble_Area_Entity.Set_Data(num, _Parameters);
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x0013E4BC File Offset: 0x0013C6BC
	public List<Scramble_Area_Entity> Get_Area_Data()
	{
		List<Scramble_Area_Entity> list = new List<Scramble_Area_Entity>();
		List<int> list2 = new List<int>(this.Area_Data.Keys);
		for (int i = 0; i < list2.Count; i++)
		{
			int key = list2[i];
			list.Add(this.Area_Data[key]);
		}
		return list;
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x0013E524 File Offset: 0x0013C724
	public Scramble_Area_Entity Get_Area_Data(int _Index)
	{
		bool flag = !this.Area_Data.ContainsKey(_Index);
		Scramble_Area_Entity result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.Area_Data[_Index];
		}
		return result;
	}

	// Token: 0x04001F91 RID: 8081
	private Dictionary<int, Dictionary<int, Scramble_User_Entity>> _User_Data;

	// Token: 0x04001F92 RID: 8082
	private Dictionary<int, Scramble_Area_Entity> _Area_Data;

	// Token: 0x04001F93 RID: 8083
	private int _A_Score;

	// Token: 0x04001F94 RID: 8084
	private int _B_Score;
}

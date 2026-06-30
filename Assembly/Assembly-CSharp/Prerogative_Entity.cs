using System;
using System.Collections.Generic;

// Token: 0x020002A2 RID: 674
public class Prerogative_Entity
{
	// Token: 0x17000398 RID: 920
	// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0011C5EC File Offset: 0x0011A7EC
	// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0011C604 File Offset: 0x0011A804
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			this._ID = value;
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0011C610 File Offset: 0x0011A810
	// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0011C628 File Offset: 0x0011A828
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0011C634 File Offset: 0x0011A834
	// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0011C64C File Offset: 0x0011A84C
	public string Reward_Data
	{
		get
		{
			return this._Reward_Data;
		}
		set
		{
			this._Reward_Data = value;
			string[] array = this.Reward_Data.Split(new char[]
			{
				'|'
			});
			this.Item_Data.Clear();
			bool flag = this.ID == 4 || this.ID == 5 || this.ID == 8;
			if (flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Dictionary<int, int> dictionary = new Dictionary<int, int>();
					string[] array2 = array[i].ToString().Split(new char[]
					{
						'#'
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array3 = array2[j].ToString().Split(new char[]
						{
							','
						});
						dictionary.Add(int.Parse(array3[0].ToString()), int.Parse(array3[1].ToString()));
					}
					this.Item_Data.Add(i + 1, dictionary);
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].ToString().Split(new char[]
					{
						'#'
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array3 = array2[j].ToString().Split(new char[]
						{
							','
						});
						bool flag2 = !this.Item_Data.ContainsKey(int.Parse(array3[0].ToString()));
						if (flag2)
						{
							this.Item_Data.Add(int.Parse(array3[0].ToString()), int.Parse(array3[1].ToString()));
						}
					}
				}
			}
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0011C81C File Offset: 0x0011AA1C
	// (set) Token: 0x06001EAF RID: 7855 RVA: 0x0011C84E File Offset: 0x0011AA4E
	public Dictionary<int, object> Item_Data
	{
		get
		{
			bool flag = this._Item_Data == null;
			if (flag)
			{
				this._Item_Data = new Dictionary<int, object>();
			}
			return this._Item_Data;
		}
		set
		{
			this._Item_Data = value;
		}
	}

	// Token: 0x04001C50 RID: 7248
	private int _ID;

	// Token: 0x04001C51 RID: 7249
	private string _Name;

	// Token: 0x04001C52 RID: 7250
	private string _Reward_Data;

	// Token: 0x04001C53 RID: 7251
	private Dictionary<int, object> _Item_Data;
}

using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000143 RID: 323
public class Gem_Data
{
	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0008293C File Offset: 0x00080B3C
	// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00082954 File Offset: 0x00080B54
	public Dictionary<int, List<int>> Gem_Package_Data
	{
		get
		{
			return this._Gem_Package_Data;
		}
		set
		{
			this._Gem_Package_Data = value;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00082960 File Offset: 0x00080B60
	// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00082978 File Offset: 0x00080B78
	public Dictionary<int, ArrayList> Gem_Equip_Data
	{
		get
		{
			return this._Gem_Equip_Data;
		}
		set
		{
			this._Gem_Equip_Data = value;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00082984 File Offset: 0x00080B84
	// (set) Token: 0x06000D91 RID: 3473 RVA: 0x000829B8 File Offset: 0x00080BB8
	public Dictionary<int, int> Gem_Have_New
	{
		get
		{
			bool flag = this._Gem_Have_New == null;
			if (flag)
			{
				this._Gem_Have_New = new Dictionary<int, int>();
			}
			return this._Gem_Have_New;
		}
		set
		{
			this._Gem_Have_New = value;
			bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
			if (flag)
			{
				foreach (int num in this._Gem_Have_New.Keys)
				{
					HeroInfoUICon.inst.SetOneHeroGrid(num);
					HeroInfoUICon.inst.SetUnlockTgNewState(num);
				}
				HeroInfoUICon.inst.OnClickUnlock(1);
			}
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.SetHeroNewState();
			}
		}
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00082A78 File Offset: 0x00080C78
	public int GetGemNewState(int nheroid)
	{
		int result = 0;
		bool flag = this.Gem_Have_New.ContainsKey(nheroid);
		if (flag)
		{
			result = this.Gem_Have_New[nheroid];
		}
		return result;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x00082AAC File Offset: 0x00080CAC
	public void Initialize_Data(ArrayList _Value)
	{
		this.Gem_Package_Data = this.Create_Package_Data(_Value[0].ToString());
		this.Gem_Equip_Data = this.Create_Equip_Data(_Value[1].ToString());
		this.Gem_Have_New = this.Create_Have_New_Data(_Value[2].ToString());
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x00082B08 File Offset: 0x00080D08
	public void Update_Package_Data(ArrayList _Value)
	{
		int item = Fun.Convert_Int(_Value[0].ToString());
		int num = Fun.Convert_Int(_Value[1].ToString());
		int num2 = Fun.Convert_Int(_Value[2].ToString());
		List<int> value = new List<int>
		{
			item,
			num,
			num2
		};
		bool flag = !this.Gem_Package_Data.ContainsKey(num);
		if (flag)
		{
			this.Gem_Package_Data.Add(num, value);
		}
		else
		{
			bool flag2 = num2 < 1;
			if (flag2)
			{
				this.Gem_Package_Data.Remove(num);
			}
			else
			{
				this.Gem_Package_Data[num] = value;
			}
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00082BCC File Offset: 0x00080DCC
	public void Update_Equip_Data(string _Value)
	{
		bool flag = !(_Value == string.Empty);
		if (flag)
		{
			this.Gem_Equip_Data = this.Create_Equip_Data(_Value);
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00082BFC File Offset: 0x00080DFC
	public void Update_Have_New_Data(string _Value)
	{
		this.Gem_Have_New = this.Create_Have_New_Data(_Value);
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00082C10 File Offset: 0x00080E10
	private Dictionary<int, List<int>> Create_Package_Data(string _Value)
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		bool flag = _Value != "()";
		ArrayList arrayList;
		if (flag)
		{
			_Value = Fun.CheckChatSpecialStr_Gem_Data(_Value);
			arrayList = Fun.Convert_ArrayList(_Value, '|');
		}
		else
		{
			arrayList = new ArrayList();
		}
		for (int i = 0; i < arrayList.Count; i++)
		{
			string[] array = arrayList[i].ToString().Split(new char[]
			{
				','
			});
			bool flag2 = fGameManager.mloadConfig.Gem_Xml_Data.ContainsKey(int.Parse(array[0].ToString())) && !dictionary.ContainsKey(int.Parse(array[0].ToString()));
			if (flag2)
			{
				int item = Fun.Convert_Int(array[0].ToString());
				int num = Fun.Convert_Int(array[1].ToString());
				int item2 = Fun.Convert_Int(array[2].ToString());
				dictionary.Add(num, new List<int>
				{
					item,
					num,
					item2
				});
			}
		}
		return dictionary;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x00082D3C File Offset: 0x00080F3C
	private Dictionary<int, ArrayList> Create_Equip_Data(string _Value)
	{
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		ArrayList arrayList = Fun.Convert_ArrayList(_Value, '&');
		for (int i = 0; i < arrayList.Count; i++)
		{
			ArrayList arrayList2 = new ArrayList(arrayList[i].ToString().Split(new char[]
			{
				'|'
			}));
			bool flag = !dictionary.ContainsKey(int.Parse(arrayList2[0].ToString()));
			if (flag)
			{
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(new ArrayList(arrayList2[1].ToString().Split(new char[]
				{
					','
				})));
				arrayList3.Add(new ArrayList(arrayList2[2].ToString().Split(new char[]
				{
					','
				})));
				arrayList3.Add(new ArrayList(arrayList2[3].ToString().Split(new char[]
				{
					','
				})));
				arrayList3.Add(new ArrayList(arrayList2[4].ToString().Split(new char[]
				{
					','
				})));
				dictionary.Add(int.Parse(arrayList2[0].ToString()), arrayList3);
			}
		}
		return dictionary;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00082E98 File Offset: 0x00081098
	private Dictionary<int, int> Create_Have_New_Data(string _Value)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		bool flag = _Value != "()";
		ArrayList arrayList;
		if (flag)
		{
			_Value = Fun.CheckChatSpecialStr_Gem_Data(_Value);
			arrayList = Fun.Convert_ArrayList(_Value, '|');
		}
		else
		{
			arrayList = new ArrayList();
		}
		for (int i = 0; i < arrayList.Count; i++)
		{
			string[] array = arrayList[i].ToString().Split(new char[]
			{
				','
			});
			bool flag2 = !dictionary.ContainsKey(int.Parse(array[0].ToString()));
			if (flag2)
			{
				dictionary.Add(Fun.Convert_Int(array[0].ToString()), Fun.Convert_Int(array[1].ToString()));
			}
		}
		return dictionary;
	}

	// Token: 0x04000B30 RID: 2864
	public static Gem_Data Instance;

	// Token: 0x04000B31 RID: 2865
	private Dictionary<int, List<int>> _Gem_Package_Data;

	// Token: 0x04000B32 RID: 2866
	private Dictionary<int, ArrayList> _Gem_Equip_Data;

	// Token: 0x04000B33 RID: 2867
	private Dictionary<int, int> _Gem_Have_New;
}

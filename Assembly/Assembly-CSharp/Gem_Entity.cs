using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000144 RID: 324
public class Gem_Entity
{
	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00082F68 File Offset: 0x00081168
	// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00082F80 File Offset: 0x00081180
	public int Index
	{
		get
		{
			return this._Index;
		}
		set
		{
			this._Index = value;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00082F8C File Offset: 0x0008118C
	// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00082FA4 File Offset: 0x000811A4
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

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00082FB0 File Offset: 0x000811B0
	// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00082FC8 File Offset: 0x000811C8
	public int Amount
	{
		get
		{
			return this._Amount;
		}
		set
		{
			this._Amount = value;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00082FD4 File Offset: 0x000811D4
	// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00082FEC File Offset: 0x000811EC
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

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00082FF8 File Offset: 0x000811F8
	// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00083010 File Offset: 0x00081210
	public int Quality
	{
		get
		{
			return this._Quality;
		}
		set
		{
			this._Quality = value;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0008301C File Offset: 0x0008121C
	// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00083034 File Offset: 0x00081234
	public int Type
	{
		get
		{
			return this._Type;
		}
		set
		{
			this._Type = value;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00083040 File Offset: 0x00081240
	// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00083058 File Offset: 0x00081258
	public int Sub_Type
	{
		get
		{
			return this._Sub_Type;
		}
		set
		{
			this._Sub_Type = value;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00083064 File Offset: 0x00081264
	// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0008307C File Offset: 0x0008127C
	public int Sell_Price
	{
		get
		{
			return this._Sell_Price;
		}
		set
		{
			this._Sell_Price = value;
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00083088 File Offset: 0x00081288
	// (set) Token: 0x06000DAC RID: 3500 RVA: 0x000830A0 File Offset: 0x000812A0
	public int Limit_Equip_Amount
	{
		get
		{
			return this._Limit_Equip_Amount;
		}
		set
		{
			this._Limit_Equip_Amount = value;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000830AC File Offset: 0x000812AC
	// (set) Token: 0x06000DAE RID: 3502 RVA: 0x000830C4 File Offset: 0x000812C4
	public ArrayList Limit_Equip_Data
	{
		get
		{
			return this._Limit_Equip_Data;
		}
		set
		{
			this._Limit_Equip_Data = value;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000830D0 File Offset: 0x000812D0
	// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000830E8 File Offset: 0x000812E8
	public int Limit_Hero_Index
	{
		get
		{
			return this._Limit_Hero_Index;
		}
		set
		{
			this._Limit_Hero_Index = value;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000830F4 File Offset: 0x000812F4
	// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0008310C File Offset: 0x0008130C
	public Dictionary<int, List<int>> General_Attribute
	{
		get
		{
			return this._General_Attribute;
		}
		set
		{
			this._General_Attribute = value;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00083118 File Offset: 0x00081318
	// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00083130 File Offset: 0x00081330
	public Dictionary<int, Dictionary<int, List<int>>> Skill_Attribute
	{
		get
		{
			return this._Skill_Attribute;
		}
		set
		{
			this._Skill_Attribute = value;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0008313C File Offset: 0x0008133C
	// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00083154 File Offset: 0x00081354
	public Dictionary<int, Dictionary<int, List<int>>> Stunt_Attribute
	{
		get
		{
			return this._Stunt_Attribute;
		}
		set
		{
			this._Stunt_Attribute = value;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00083160 File Offset: 0x00081360
	// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00083178 File Offset: 0x00081378
	public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> Weapon_Attribute
	{
		get
		{
			return this._Weapon_Attribute;
		}
		set
		{
			this._Weapon_Attribute = value;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00083184 File Offset: 0x00081384
	// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0008319C File Offset: 0x0008139C
	public Dictionary<int, Dictionary<int, List<int>>> Buff_Attribute
	{
		get
		{
			return this._Buff_Attribute;
		}
		set
		{
			this._Buff_Attribute = value;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000DBB RID: 3515 RVA: 0x000831A8 File Offset: 0x000813A8
	// (set) Token: 0x06000DBC RID: 3516 RVA: 0x000831C0 File Offset: 0x000813C0
	public Dictionary<int, Dictionary<int, List<int>>> Npc_Attribute
	{
		get
		{
			return this._Npc_Attribute;
		}
		set
		{
			this._Npc_Attribute = value;
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000831CC File Offset: 0x000813CC
	// (set) Token: 0x06000DBE RID: 3518 RVA: 0x000831E4 File Offset: 0x000813E4
	public Dictionary<int, List<int>> Special_Attribute
	{
		get
		{
			return this._Special_Attribute;
		}
		set
		{
			this._Special_Attribute = value;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000831F0 File Offset: 0x000813F0
	// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x00083208 File Offset: 0x00081408
	public Dictionary<int, Dictionary<int, List<int>>> Addition_Buff_Attribute
	{
		get
		{
			return this._Addition_Buff_Attribute;
		}
		set
		{
			this._Addition_Buff_Attribute = value;
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00083214 File Offset: 0x00081414
	// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x0008322C File Offset: 0x0008142C
	public string Describe
	{
		get
		{
			return this._Describe;
		}
		set
		{
			this._Describe = value;
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00083238 File Offset: 0x00081438
	// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00083250 File Offset: 0x00081450
	public string Icon
	{
		get
		{
			return this._Icon;
		}
		set
		{
			this._Icon = value;
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0008325C File Offset: 0x0008145C
	// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00083274 File Offset: 0x00081474
	public int Next_1_Level_Index
	{
		get
		{
			return this._Next_1_Level_Index;
		}
		set
		{
			this._Next_1_Level_Index = value;
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00083280 File Offset: 0x00081480
	// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00083298 File Offset: 0x00081498
	public int Next_2_Level_Index
	{
		get
		{
			return this._Next_2_Level_Index;
		}
		set
		{
			this._Next_2_Level_Index = value;
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000832A4 File Offset: 0x000814A4
	// (set) Token: 0x06000DCA RID: 3530 RVA: 0x000832BC File Offset: 0x000814BC
	public List<int> Language_ID
	{
		get
		{
			return this._Language_ID;
		}
		set
		{
			this._Language_ID = value;
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000832C8 File Offset: 0x000814C8
	// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000832E0 File Offset: 0x000814E0
	public bool Equip_State
	{
		get
		{
			return this._Equip_State;
		}
		set
		{
			this._Equip_State = value;
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x000832EC File Offset: 0x000814EC
	public void Initialize_Data()
	{
		Gem_Entity gem_Entity = fGameManager.mloadConfig.Gem_Xml_Data[this.Index];
		this.Index = gem_Entity.Index;
		this.Name = gem_Entity.Name;
		this.Quality = gem_Entity.Quality;
		this.Type = gem_Entity.Type;
		this.Sell_Price = gem_Entity.Sell_Price;
		this.Limit_Equip_Amount = gem_Entity.Limit_Equip_Amount;
		this.Limit_Equip_Data = gem_Entity.Limit_Equip_Data;
		this.Limit_Hero_Index = gem_Entity.Limit_Hero_Index;
		this.General_Attribute = gem_Entity.General_Attribute;
		this.Skill_Attribute = gem_Entity.Skill_Attribute;
		this.Stunt_Attribute = gem_Entity.Stunt_Attribute;
		this.Describe = gem_Entity.Describe;
		this.Icon = gem_Entity.Icon;
		this.Next_1_Level_Index = gem_Entity.Next_1_Level_Index;
		this.Next_2_Level_Index = gem_Entity.Next_2_Level_Index;
	}

	// Token: 0x04000B34 RID: 2868
	private int _Index;

	// Token: 0x04000B35 RID: 2869
	private int _ID;

	// Token: 0x04000B36 RID: 2870
	private int _Amount;

	// Token: 0x04000B37 RID: 2871
	private string _Name;

	// Token: 0x04000B38 RID: 2872
	private int _Quality;

	// Token: 0x04000B39 RID: 2873
	private int _Type;

	// Token: 0x04000B3A RID: 2874
	private int _Sub_Type;

	// Token: 0x04000B3B RID: 2875
	private int _Sell_Price;

	// Token: 0x04000B3C RID: 2876
	private int _Limit_Equip_Amount;

	// Token: 0x04000B3D RID: 2877
	private ArrayList _Limit_Equip_Data;

	// Token: 0x04000B3E RID: 2878
	private int _Limit_Hero_Index;

	// Token: 0x04000B3F RID: 2879
	private Dictionary<int, List<int>> _General_Attribute;

	// Token: 0x04000B40 RID: 2880
	private Dictionary<int, Dictionary<int, List<int>>> _Skill_Attribute;

	// Token: 0x04000B41 RID: 2881
	private Dictionary<int, Dictionary<int, List<int>>> _Stunt_Attribute;

	// Token: 0x04000B42 RID: 2882
	private Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> _Weapon_Attribute;

	// Token: 0x04000B43 RID: 2883
	private Dictionary<int, Dictionary<int, List<int>>> _Buff_Attribute;

	// Token: 0x04000B44 RID: 2884
	private Dictionary<int, Dictionary<int, List<int>>> _Npc_Attribute;

	// Token: 0x04000B45 RID: 2885
	private Dictionary<int, Dictionary<int, List<int>>> _Addition_Buff_Attribute;

	// Token: 0x04000B46 RID: 2886
	private Dictionary<int, List<int>> _Special_Attribute;

	// Token: 0x04000B47 RID: 2887
	private string _Describe;

	// Token: 0x04000B48 RID: 2888
	private string _Icon;

	// Token: 0x04000B49 RID: 2889
	private int _Next_1_Level_Index;

	// Token: 0x04000B4A RID: 2890
	private int _Next_2_Level_Index;

	// Token: 0x04000B4B RID: 2891
	private List<int> _Language_ID;

	// Token: 0x04000B4C RID: 2892
	private bool _Equip_State;

	// Token: 0x04000B4D RID: 2893
	private int _Item_ID;
}

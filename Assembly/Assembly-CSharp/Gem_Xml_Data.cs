using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200014F RID: 335
public class Gem_Xml_Data
{
	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0008CA70 File Offset: 0x0008AC70
	// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0008CA88 File Offset: 0x0008AC88
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

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0008CA94 File Offset: 0x0008AC94
	// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0008CAC6 File Offset: 0x0008ACC6
	public Gem_Entity EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Gem_Entity();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0008CAD0 File Offset: 0x0008ACD0
	public void Initialize(XmlElement _XmlElement)
	{
		this.Index = Fun.Convert_Int(_XmlElement.GetAttribute("Index"));
		this.EntityComponent.Index = this.Index;
		this.EntityComponent.Name = _XmlElement.GetAttribute("Name");
		this.EntityComponent.Quality = Fun.Convert_Int(_XmlElement.GetAttribute("Quality"));
		this.EntityComponent.Type = Fun.Convert_Int(_XmlElement.GetAttribute("Type"));
		this.EntityComponent.Sub_Type = Fun.Convert_Int(_XmlElement.GetAttribute("Sub_Type"));
		this.EntityComponent.Sell_Price = Fun.Convert_Int(_XmlElement.GetAttribute("Sell_Price"));
		this.EntityComponent.Limit_Equip_Amount = Fun.Convert_Int(_XmlElement.GetAttribute("Limit_Equip_Amount"));
		this.EntityComponent.Limit_Equip_Data = Fun.Convert_ArrayList(_XmlElement.GetAttribute("Limit_Equip_Data"), ',');
		this.EntityComponent.Limit_Hero_Index = Fun.Convert_Int(_XmlElement.GetAttribute("Limit_Hero_Index"));
		this.EntityComponent.General_Attribute = Fun.Convert_Dictionary(_XmlElement.GetAttribute("General_Attribute"), '|', ',');
		this.EntityComponent.Skill_Attribute = this.Convert_Dictionary_2(_XmlElement.GetAttribute("Skill_Attribute"), '|', ',');
		this.EntityComponent.Stunt_Attribute = this.Convert_Dictionary_2(_XmlElement.GetAttribute("Stunt_Attribute"), '|', ',');
		this.EntityComponent.Weapon_Attribute = this.Convert_Dictionary_4(_XmlElement.GetAttribute("Weapon_Attribute"), '|', ',');
		this.EntityComponent.Buff_Attribute = this.Convert_Dictionary_2(_XmlElement.GetAttribute("Buff_Attribute"), '|', ',');
		this.EntityComponent.Npc_Attribute = this.Convert_Dictionary_2(_XmlElement.GetAttribute("Npc_Attribute"), '|', ',');
		this.EntityComponent.Special_Attribute = Fun.Convert_Dictionary(_XmlElement.GetAttribute("Special_Attribute"), '|', ',');
		this.EntityComponent.Describe = _XmlElement.GetAttribute("Describe");
		this.EntityComponent.Icon = _XmlElement.GetAttribute("Icon");
		this.EntityComponent.Next_1_Level_Index = Fun.Convert_Int(_XmlElement.GetAttribute("Next_1_Level_Index"));
		this.EntityComponent.Next_2_Level_Index = Fun.Convert_Int(_XmlElement.GetAttribute("Next_2_Level_Index"));
		this.EntityComponent.Addition_Buff_Attribute = this.Convert_Dictionary_2(_XmlElement.GetAttribute("Skill_Buff_Attribute"), '|', ',');
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0008CD50 File Offset: 0x0008AF50
	public Dictionary<int, Dictionary<int, List<int>>> Convert_Dictionary_2(string _Value, char _Separator_1, char _Separator_2)
	{
		Dictionary<int, Dictionary<int, List<int>>> dictionary = new Dictionary<int, Dictionary<int, List<int>>>();
		ArrayList arrayList = Fun.Convert_ArrayList(_Value, _Separator_1);
		for (int i = 0; i < arrayList.Count; i++)
		{
			string[] array = arrayList[i].ToString().Split(new char[]
			{
				_Separator_2
			});
			bool flag = array.Length >= 2 && !dictionary.ContainsKey(int.Parse(array[0].ToString()));
			if (flag)
			{
				Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
				dictionary2.Add(int.Parse(array[1].ToString()), new List<int>
				{
					int.Parse(array[2].ToString()),
					int.Parse(array[3].ToString())
				});
				dictionary.Add(int.Parse(array[0].ToString()), dictionary2);
			}
		}
		return dictionary;
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0008CE44 File Offset: 0x0008B044
	public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> Convert_Dictionary_4(string _Value, char _Separator_1, char _Separator_2)
	{
		Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>> dictionary = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>>();
		ArrayList arrayList = Fun.Convert_ArrayList(_Value, _Separator_1);
		for (int i = 0; i < arrayList.Count; i++)
		{
			string[] array = arrayList[i].ToString().Split(new char[]
			{
				_Separator_2
			});
			bool flag = array.Length >= 2 && !dictionary.ContainsKey(int.Parse(array[0].ToString()));
			if (flag)
			{
				Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> dictionary2 = new Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>();
				Dictionary<int, Dictionary<int, List<int>>> dictionary3 = new Dictionary<int, Dictionary<int, List<int>>>();
				Dictionary<int, List<int>> dictionary4 = new Dictionary<int, List<int>>();
				dictionary2.Add(int.Parse(array[1].ToString()), dictionary3);
				dictionary3.Add(int.Parse(array[2].ToString()), dictionary4);
				dictionary4.Add(int.Parse(array[3].ToString()), new List<int>
				{
					int.Parse(array[4].ToString()),
					int.Parse(array[5].ToString())
				});
				dictionary.Add(int.Parse(array[0].ToString()), dictionary2);
			}
		}
		return dictionary;
	}

	// Token: 0x04000BEA RID: 3050
	private int _Index;

	// Token: 0x04000BEB RID: 3051
	private Gem_Entity _EntityComponent;
}

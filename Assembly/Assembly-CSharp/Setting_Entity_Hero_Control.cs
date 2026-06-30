using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002F5 RID: 757
public class Setting_Entity_Hero_Control
{
	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00147394 File Offset: 0x00145594
	// (set) Token: 0x060023D7 RID: 9175 RVA: 0x001473D8 File Offset: 0x001455D8
	public Dictionary<int, Setting_Entity_Control> Hero_Control_Data
	{
		get
		{
			bool flag = this._Hero_Control_Data == null;
			if (flag)
			{
				this._Hero_Control_Data = new Dictionary<int, Setting_Entity_Control>();
				this._Hero_Control_Data.Add(0, new Setting_Entity_Control());
			}
			return this._Hero_Control_Data;
		}
		set
		{
			this._Hero_Control_Data = value;
		}
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x001473E4 File Offset: 0x001455E4
	public Setting_Entity_Control Get_Hero_Control_Data(int _Index)
	{
		bool flag = !this.Hero_Control_Data.ContainsKey(_Index);
		if (flag)
		{
			_Index = 0;
		}
		return this.Hero_Control_Data[_Index];
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x0014741C File Offset: 0x0014561C
	public void Initialize_Hero_Control_Data(int _Index)
	{
		bool flag = _Index != 0 && !this.Hero_Control_Data.ContainsKey(_Index);
		if (flag)
		{
			Setting_Entity_Control setting_Entity_Control = new Setting_Entity_Control();
			this.Copy_Default_Data(setting_Entity_Control);
			setting_Entity_Control.Hero_Index = _Index;
			this.Hero_Control_Data.Add(_Index, setting_Entity_Control);
		}
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x0014746C File Offset: 0x0014566C
	public void Restore_Hero_Control_Data(int _Index)
	{
		bool flag = _Index != 0 && this.Hero_Control_Data.ContainsKey(_Index);
		if (flag)
		{
			this.Hero_Control_Data.Remove(_Index);
		}
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x001474A0 File Offset: 0x001456A0
	private void Copy_Default_Data(Setting_Entity_Control _New_Entity)
	{
		Setting_Entity_Control @class = this.Hero_Control_Data[0];
		string parameter = GameRoot.inst.Setting.Get_Data(@class, GameRoot.inst.Setting.Control_Field_Name_Array);
		GameRoot.inst.Setting.Set_Data(_New_Entity, GameRoot.inst.Setting.Control_Field_Name_Array, parameter);
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x00147500 File Offset: 0x00145700
	public void Check_Data_Conformity_Degree()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		List<int> list = new List<int>(this.Hero_Control_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			Setting_Entity_Control @class = this.Hero_Control_Data[list[i]];
			text2 = GameRoot.inst.Setting.Get_Data(@class, GameRoot.inst.Setting.Control_Field_Name_Array);
			bool flag = i == 0;
			if (flag)
			{
				text = text2;
			}
			else
			{
				bool flag2 = text.Substring(text.IndexOf(',', 0), text.Length - text.IndexOf(',', 0)).Equals(text2.Substring(text2.IndexOf(',', 0), text2.Length - text2.IndexOf(',', 0)));
				if (flag2)
				{
					this.Hero_Control_Data.Remove(list[i]);
				}
			}
		}
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x001475F8 File Offset: 0x001457F8
	public void Set_Data(ArrayList _Field_Name_Array, string _Parameter)
	{
		bool flag = _Parameter == string.Empty;
		if (!flag)
		{
			string[] array = _Parameter.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = !(array[i].ToString() == string.Empty);
				if (flag2)
				{
					string[] array2 = array[i].Split(new char[]
					{
						','
					});
					int key = int.Parse(array2[0].ToString());
					bool flag3 = !this.Hero_Control_Data.ContainsKey(key);
					if (flag3)
					{
						this.Hero_Control_Data.Add(key, new Setting_Entity_Control());
					}
					Setting_Entity_Control @class = this.Hero_Control_Data[key];
					GameRoot.inst.Setting.Set_Data(@class, _Field_Name_Array, array[i]);
				}
			}
		}
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x001476E4 File Offset: 0x001458E4
	public string Get_Data(ArrayList _Field_Name_Array)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		text3 += text;
		List<int> list = new List<int>(this.Hero_Control_Data.Keys);
		int i = 0;
		while (i < list.Count)
		{
			Setting_Entity_Control @class = this.Hero_Control_Data[list[i]];
			text2 = GameRoot.inst.Setting.Get_Data(@class, _Field_Name_Array);
			bool flag = i == 0;
			if (flag)
			{
				text = text2;
				goto IL_D8;
			}
			bool flag2 = text.Substring(text.IndexOf(',', 0), text.Length - text.IndexOf(',', 0)).Equals(text2.Substring(text2.IndexOf(',', 0), text2.Length - text2.IndexOf(',', 0)));
			if (!flag2)
			{
				goto IL_D8;
			}
			this.Hero_Control_Data.Remove(list[i]);
			IL_E9:
			i++;
			continue;
			IL_D8:
			text3 = text3 + text2 + "#";
			goto IL_E9;
		}
		bool flag3 = text3 != string.Empty;
		if (flag3)
		{
			text3 = text3.Substring(0, text3.Length - 1);
		}
		return text3;
	}

	// Token: 0x04002046 RID: 8262
	private Dictionary<int, Setting_Entity_Control> _Hero_Control_Data;
}

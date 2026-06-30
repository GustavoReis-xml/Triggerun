using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class Gem_Tips_View : MonoBehaviour
{
	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000841F4 File Offset: 0x000823F4
	// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00084252 File Offset: 0x00082452
	public Gem_Tips_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Gem_Tips_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Gem_Tips_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0008425C File Offset: 0x0008245C
	// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00084274 File Offset: 0x00082474
	public Gem_View Parent_View_Component
	{
		get
		{
			return this._Parent_View_Component;
		}
		set
		{
			this._Parent_View_Component = value;
		}
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00084280 File Offset: 0x00082480
	public void Display_Data(int _Gem_Index)
	{
		bool flag = _Gem_Index == 0;
		if (flag)
		{
			Gem_Tips_UICon.Instance.Close();
		}
		else
		{
			Gem_Entity gem_Entity = this.Parent_View_Component.Get_Gem_Xml_Data(_Gem_Index);
			bool flag2 = gem_Entity == null;
			if (flag2)
			{
				Gem_Tips_UICon.Instance.Close();
			}
			else
			{
				this.UI_Component.Name_Text.text = this.Get_Gem_Quality_Color_Value(gem_Entity.Quality, gem_Entity.Name);
				bool flag3 = !fGameManager.mloadConfig.AllHeroList.ContainsKey(gem_Entity.Limit_Hero_Index);
				if (flag3)
				{
					this.UI_Component.Limit_Hero_Text.text = Language.GetLang(101003, new object[0]) + Language.GetLang(101004, new object[0]);
				}
				else
				{
					this.UI_Component.Limit_Hero_Text.text = Language.GetLang(101003, new object[0]) + fGameManager.mloadConfig.AllHeroList[gem_Entity.Limit_Hero_Index].hname;
				}
				this.UI_Component.Type_Text.text = Language.GetLang(101005, new object[0]) + Language.GetLang(Fun.Convert_Int("10102" + gem_Entity.Type.ToString()), new object[0]);
				this.UI_Component.Describe_Text.text = gem_Entity.Describe;
				this.UI_Component.Background_RectTransform.sizeDelta = new Vector2(this.UI_Component.Background_RectTransform.sizeDelta.x, 100f + this.UI_Component.Describe_Text.preferredHeight - 20f);
			}
		}
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x00084438 File Offset: 0x00082638
	private int Get_Gem_Use_Amount(ArrayList _Parameters)
	{
		int num = 0;
		ArrayList arrayList = null;
		Dictionary<int, ArrayList> gem_Equip_Data = Gem_Data.Instance.Gem_Equip_Data;
		bool flag = gem_Equip_Data.ContainsKey(this.Parent_View_Component.Hero_ID);
		if (flag)
		{
			arrayList = gem_Equip_Data[this.Parent_View_Component.Hero_ID];
		}
		bool flag2 = arrayList == null;
		int result;
		if (flag2)
		{
			Gem_Tips_UICon.Instance.Close();
			result = 0;
		}
		else
		{
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < arrayList.Count; i++)
			{
				ArrayList arrayList3 = (ArrayList)arrayList[i];
				for (int j = 0; j < arrayList3.Count; j++)
				{
					int num2 = int.Parse(arrayList3[j].ToString());
					arrayList2.Add(num2);
				}
			}
			for (int i = 0; i < arrayList2.Count; i++)
			{
				bool flag3 = _Parameters.IndexOf(arrayList2[i].ToString()) >= 0;
				if (flag3)
				{
					num++;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0008456C File Offset: 0x0008276C
	private string Get_Gem_Quality_Color_Value(int _Quality, string _Value)
	{
		if (!true)
		{
		}
		string result;
		switch (_Quality)
		{
		case 1:
			result = "<color=#BBBBBB>" + _Value + "</color>";
			break;
		case 2:
			result = "<color=#00FF00>" + _Value + "</color>";
			break;
		case 3:
			result = "<color=#00B4FF>" + _Value + "</color>";
			break;
		case 4:
			result = "<color=#FF00FF>" + _Value + "</color>";
			break;
		case 5:
			result = "<color=#F0C800>" + _Value + "</color>";
			break;
		case 6:
			result = "<color=#FF3333>" + _Value + "</color>";
			break;
		default:
			result = "<color=#BBBBBB>" + _Value + "</color>";
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x04000B60 RID: 2912
	private Gem_Tips_UI _UI_Component;

	// Token: 0x04000B61 RID: 2913
	private Gem_View _Parent_View_Component;
}

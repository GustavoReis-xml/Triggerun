using System;
using System.Collections;
using System.Xml;

// Token: 0x020001E4 RID: 484
[Serializable]
public class hudongData
{
	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x060015CB RID: 5579 RVA: 0x000CBF50 File Offset: 0x000CA150
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000CBF58 File Offset: 0x000CA158
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.m_sName = xml.GetAttribute("name");
		this.m_nType = int.Parse(xml.GetAttribute("type"));
		this.m_nType_1 = int.Parse(xml.GetAttribute("type1"));
		this.m_nIsDo = int.Parse(xml.GetAttribute("isdo"));
		this.m_szSound = hudongData.GetArrlist(xml.GetAttribute("sound"), '#');
		this.m_szMsg = hudongData.GetArrlist(xml.GetAttribute("msgtxt"), '#');
		this.m_szDongZuo = hudongData.GetArrlist(xml.GetAttribute("dongzuo"), '#');
		this.m_sIco = xml.GetAttribute("ico");
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000CC028 File Offset: 0x000CA228
	public static ArrayList GetArrlist(string items, char sc)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = items == string.Empty || items == "0";
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			string[] array = items.Split(new char[]
			{
				sc
			});
			for (int i = 0; i < array.Length; i++)
			{
				arrayList.Add(array[i]);
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x040010DD RID: 4317
	public int id;

	// Token: 0x040010DE RID: 4318
	public string m_sName;

	// Token: 0x040010DF RID: 4319
	public int m_nType;

	// Token: 0x040010E0 RID: 4320
	public int m_nType_1;

	// Token: 0x040010E1 RID: 4321
	public int m_nIsDo;

	// Token: 0x040010E2 RID: 4322
	public ArrayList m_szSound;

	// Token: 0x040010E3 RID: 4323
	public ArrayList m_szMsg;

	// Token: 0x040010E4 RID: 4324
	public ArrayList m_szDongZuo;

	// Token: 0x040010E5 RID: 4325
	public string m_sIco;
}

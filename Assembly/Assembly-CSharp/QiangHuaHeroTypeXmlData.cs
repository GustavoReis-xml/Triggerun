using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020002B7 RID: 695
public class QiangHuaHeroTypeXmlData
{
	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x001263E4 File Offset: 0x001245E4
	public int key
	{
		get
		{
			return this.nType;
		}
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x001263EC File Offset: 0x001245EC
	public void Init(XmlElement xml)
	{
		this.nType = int.Parse(xml.GetAttribute("herotype"));
		this.sName = xml.GetAttribute("name");
		this.nMaxJie = int.Parse(xml.GetAttribute("maxjie"));
		this.szJinJieRand = ItemData.GetArrlist(xml.GetAttribute("jinjie"), '|', false, ',');
		this.szHeroId = ItemData.GetArrlist(xml.GetAttribute("heroid"), ',', true, ',');
		this.m_sTypeName = xml.GetAttribute("typename");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			QiangHuaAttributeData qiangHuaAttributeData = new QiangHuaAttributeData();
			qiangHuaAttributeData.Init(xml2);
			this.AllAttributeData.Add(qiangHuaAttributeData.nType, qiangHuaAttributeData);
		}
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x001264E8 File Offset: 0x001246E8
	public int GetJinJieItem()
	{
		ArrayList arrayList = (ArrayList)this.szJinJieRand[0];
		return (int)arrayList[2];
	}

	// Token: 0x04001D2F RID: 7471
	public int nType;

	// Token: 0x04001D30 RID: 7472
	public string sName;

	// Token: 0x04001D31 RID: 7473
	public int nMaxJie;

	// Token: 0x04001D32 RID: 7474
	public ArrayList szJinJieRand;

	// Token: 0x04001D33 RID: 7475
	public ArrayList szHeroId;

	// Token: 0x04001D34 RID: 7476
	public string m_sTypeName;

	// Token: 0x04001D35 RID: 7477
	public Dictionary<int, QiangHuaAttributeData> AllAttributeData = new Dictionary<int, QiangHuaAttributeData>();
}

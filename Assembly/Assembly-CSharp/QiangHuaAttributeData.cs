using System;
using System.Collections;
using System.Xml;

// Token: 0x020002B6 RID: 694
public class QiangHuaAttributeData
{
	// Token: 0x06001FF5 RID: 8181 RVA: 0x001262D4 File Offset: 0x001244D4
	public void Init(XmlElement xml)
	{
		this.nType = int.Parse(xml.GetAttribute("type"));
		this.sName = xml.GetAttribute("name");
		this.nMaxLv = int.Parse(xml.GetAttribute("maxlv"));
		this.nBaseplus = int.Parse(xml.GetAttribute("baseplus"));
		this.nJJBaseplus = int.Parse(xml.GetAttribute("jjbaseplus"));
		this.m_nItem = int.Parse(xml.GetAttribute("item"));
		this.nXiShu = int.Parse(xml.GetAttribute("xhxishu"));
		this.szJinXieXhXiShu = ItemData.GetArrlist(xml.GetAttribute("jjxhxishu"), ',', true, ',');
		this.m_sIco = xml.GetAttribute("icon");
		this.m_sDesc = xml.GetAttribute("desc");
		this.m_sTypeName = xml.GetAttribute("typename");
	}

	// Token: 0x04001D24 RID: 7460
	public int nType;

	// Token: 0x04001D25 RID: 7461
	public string sName;

	// Token: 0x04001D26 RID: 7462
	public int nMaxLv;

	// Token: 0x04001D27 RID: 7463
	public int nBaseplus;

	// Token: 0x04001D28 RID: 7464
	public int nJJBaseplus;

	// Token: 0x04001D29 RID: 7465
	public int nXiShu;

	// Token: 0x04001D2A RID: 7466
	public ArrayList szJinXieXhXiShu;

	// Token: 0x04001D2B RID: 7467
	public int m_nItem;

	// Token: 0x04001D2C RID: 7468
	public string m_sIco;

	// Token: 0x04001D2D RID: 7469
	public string m_sDesc = string.Empty;

	// Token: 0x04001D2E RID: 7470
	public string m_sTypeName = string.Empty;
}

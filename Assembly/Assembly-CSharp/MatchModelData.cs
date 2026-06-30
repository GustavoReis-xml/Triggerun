using System;
using System.Xml;

// Token: 0x0200023C RID: 572
[Serializable]
public class MatchModelData
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000F9081 File Offset: 0x000F7281
	public int key
	{
		get
		{
			return this.m_nId;
		}
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x000F908C File Offset: 0x000F728C
	public void Init(XmlElement xml)
	{
		this.m_nId = int.Parse(xml.GetAttribute("id"));
		this.m_nfbtype = int.Parse(xml.GetAttribute("fbtype"));
		this.m_sName = xml.GetAttribute("name");
		this.m_sDesc = xml.GetAttribute("desc");
		this.m_nIsOpen = int.Parse(xml.GetAttribute("isopen"));
		this.m_sIcon = xml.GetAttribute("modelicon");
		this.m_nIsTuiJian = int.Parse(xml.GetAttribute("istuijian"));
	}

	// Token: 0x040014C2 RID: 5314
	public int m_nId;

	// Token: 0x040014C3 RID: 5315
	public int m_nfbtype;

	// Token: 0x040014C4 RID: 5316
	public string m_sName;

	// Token: 0x040014C5 RID: 5317
	public string m_sDesc;

	// Token: 0x040014C6 RID: 5318
	public int m_nIsOpen;

	// Token: 0x040014C7 RID: 5319
	public string m_sIcon;

	// Token: 0x040014C8 RID: 5320
	public int m_nIsTuiJian;
}

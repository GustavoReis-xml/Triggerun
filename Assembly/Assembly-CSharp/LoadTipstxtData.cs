using System;
using System.Xml;

// Token: 0x02000220 RID: 544
public class LoadTipstxtData
{
	// Token: 0x06001889 RID: 6281 RVA: 0x000E5CD0 File Offset: 0x000E3ED0
	public void Init(XmlElement xml)
	{
		this.m_nId = int.Parse(xml.GetAttribute("id"));
		this.m_nMaptype = int.Parse(xml.GetAttribute("maptype"));
		this.m_sTipstxt = xml.GetAttribute("tipstxt");
	}

	// Token: 0x0400134C RID: 4940
	public int m_nId;

	// Token: 0x0400134D RID: 4941
	public int m_nMaptype;

	// Token: 0x0400134E RID: 4942
	public string m_sTipstxt;
}

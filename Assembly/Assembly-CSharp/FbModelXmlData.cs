using System;
using System.Collections;
using System.Xml;

// Token: 0x02000123 RID: 291
[Serializable]
public class FbModelXmlData
{
	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00061355 File Offset: 0x0005F555
	public int key
	{
		get
		{
			return this.nId;
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00061360 File Offset: 0x0005F560
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.objname1 = int.Parse(xml.GetAttribute("pointobj1"));
		this.objname2 = int.Parse(xml.GetAttribute("pointobj2"));
		this.objname3 = int.Parse(xml.GetAttribute("pointobj3"));
		this.nreadytime = int.Parse(xml.GetAttribute("readytime"));
		this.overtime = int.Parse(xml.GetAttribute("overtime"));
		this.m_nNanDu = int.Parse(xml.GetAttribute("nandu"));
		this.m_nDropId = int.Parse(xml.GetAttribute("dropid"));
		this.m_szShowItem = ItemData.GetArrlist(xml.GetAttribute("showitem"), ',', true, ',');
		this.m_nZdl = int.Parse(xml.GetAttribute("tjzl"));
		this.m_nMinZdl = int.Parse(xml.GetAttribute("zdzl"));
	}

	// Token: 0x040008B0 RID: 2224
	public int nId;

	// Token: 0x040008B1 RID: 2225
	public string sName;

	// Token: 0x040008B2 RID: 2226
	public ArrayList scnpoint = new ArrayList();

	// Token: 0x040008B3 RID: 2227
	public int objname1;

	// Token: 0x040008B4 RID: 2228
	public int objname2;

	// Token: 0x040008B5 RID: 2229
	public int objname3;

	// Token: 0x040008B6 RID: 2230
	public ArrayList correct1 = new ArrayList();

	// Token: 0x040008B7 RID: 2231
	public ArrayList correct2 = new ArrayList();

	// Token: 0x040008B8 RID: 2232
	public int nreadytime;

	// Token: 0x040008B9 RID: 2233
	public int stagetime1;

	// Token: 0x040008BA RID: 2234
	public int stagetime2;

	// Token: 0x040008BB RID: 2235
	public int overtime;

	// Token: 0x040008BC RID: 2236
	public int m_nNanDu;

	// Token: 0x040008BD RID: 2237
	public int m_nDropId;

	// Token: 0x040008BE RID: 2238
	public ArrayList m_szShowItem = new ArrayList();

	// Token: 0x040008BF RID: 2239
	public int m_nZdl;

	// Token: 0x040008C0 RID: 2240
	public int m_nMinZdl;
}

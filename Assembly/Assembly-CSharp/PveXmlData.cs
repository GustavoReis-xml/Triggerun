using System;
using System.Collections;
using System.Xml;

// Token: 0x020002B0 RID: 688
[Serializable]
public class PveXmlData
{
	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00122FAD File Offset: 0x001211AD
	public int key
	{
		get
		{
			return this.nId;
		}
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x00122FB8 File Offset: 0x001211B8
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.sIcon = xml.GetAttribute("ico");
		this.szScnid = ItemData.GetArrlist(xml.GetAttribute("mapidlist"), ',', true, ',');
		this.szColor = xml.GetAttribute("bgcolor").Split(new char[]
		{
			','
		});
		this.m_sStartTime = xml.GetAttribute("starttime");
		this.m_EndTime = xml.GetAttribute("endtime");
		this.m_nFbType = int.Parse(xml.GetAttribute("fbtype"));
	}

	// Token: 0x04001CD1 RID: 7377
	public int nId;

	// Token: 0x04001CD2 RID: 7378
	public string sName;

	// Token: 0x04001CD3 RID: 7379
	public string sIcon;

	// Token: 0x04001CD4 RID: 7380
	public ArrayList szScnid;

	// Token: 0x04001CD5 RID: 7381
	public string m_sIco = "1";

	// Token: 0x04001CD6 RID: 7382
	public string[] szColor;

	// Token: 0x04001CD7 RID: 7383
	public string m_sStartTime;

	// Token: 0x04001CD8 RID: 7384
	public string m_EndTime;

	// Token: 0x04001CD9 RID: 7385
	public int m_nFbType;
}

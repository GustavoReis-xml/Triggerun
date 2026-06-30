using System;
using System.Xml;

// Token: 0x0200029D RID: 669
[Serializable]
public class PreloadData
{
	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0011AE1E File Offset: 0x0011901E
	public int key
	{
		get
		{
			return this.nId;
		}
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0011AE28 File Offset: 0x00119028
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nPriority = int.Parse(xml.GetAttribute("priority"));
		this.nType = int.Parse(xml.GetAttribute("ntype"));
		this.sName = xml.GetAttribute("name");
		bool flag = this.nType == 5;
		if (flag)
		{
			this.sUrl = this.sName.Replace("SoundRes/", string.Empty).Replace(".sds", string.Empty);
		}
		this.nPre = int.Parse(xml.GetAttribute("asssize"));
		this.nAssindex = int.Parse(xml.GetAttribute("assindex"));
		this.nAssNum = int.Parse(xml.GetAttribute("assnum"));
	}

	// Token: 0x04001C1F RID: 7199
	public int nId;

	// Token: 0x04001C20 RID: 7200
	public int nPriority;

	// Token: 0x04001C21 RID: 7201
	public int nType;

	// Token: 0x04001C22 RID: 7202
	public string sName;

	// Token: 0x04001C23 RID: 7203
	public int nPre;

	// Token: 0x04001C24 RID: 7204
	public int nAssindex;

	// Token: 0x04001C25 RID: 7205
	public int nAssNum;

	// Token: 0x04001C26 RID: 7206
	public string sUrl;
}

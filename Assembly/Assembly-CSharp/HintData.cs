using System;
using System.Xml;

// Token: 0x020001DB RID: 475
[Serializable]
public class HintData
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x0600159B RID: 5531 RVA: 0x000CA2C3 File Offset: 0x000C84C3
	public int key
	{
		get
		{
			return this.nID;
		}
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000CA2CB File Offset: 0x000C84CB
	public void Init(XmlElement xml)
	{
		this.nID = int.Parse(xml.GetAttribute("id"));
		this.sMsg = xml.GetAttribute("msg").Replace("\\n", "\n");
	}

	// Token: 0x0400107D RID: 4221
	public int nID;

	// Token: 0x0400107E RID: 4222
	public string sMsg;
}

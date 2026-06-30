using System;
using System.Xml;

// Token: 0x020001E7 RID: 487
public class HuiliuActData
{
	// Token: 0x060015E2 RID: 5602 RVA: 0x000CCD50 File Offset: 0x000CAF50
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.szReward = xml.GetAttribute("itemdata").Split(new char[]
		{
			','
		});
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
	}

	// Token: 0x04001103 RID: 4355
	public int nId;

	// Token: 0x04001104 RID: 4356
	public string sActName;

	// Token: 0x04001105 RID: 4357
	public string[] szReward;

	// Token: 0x04001106 RID: 4358
	public string sDesc;
}

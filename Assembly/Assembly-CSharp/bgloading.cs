using System;
using System.Xml;

// Token: 0x0200002F RID: 47
[Serializable]
public class bgloading
{
	// Token: 0x060001D6 RID: 470 RVA: 0x0000BF00 File Offset: 0x0000A100
	public void Init(XmlElement xml)
	{
		this.url = xml.GetAttribute("path");
		this.bload = false;
	}

	// Token: 0x0400016E RID: 366
	public string url;

	// Token: 0x0400016F RID: 367
	public bool bload;
}

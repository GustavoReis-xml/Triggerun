using System;
using System.Xml;

// Token: 0x02000207 RID: 519
public class KaiXueGiftData
{
	// Token: 0x06001696 RID: 5782 RVA: 0x000D0F6C File Offset: 0x000CF16C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sActDesc1 = xml.GetAttribute("desc1").Replace("\\n", "\n");
		this.sActDesc2 = xml.GetAttribute("desc2").Replace("\\n", "\n");
		this.szReward = xml.GetAttribute("reward").Split(new char[]
		{
			','
		});
	}

	// Token: 0x0400119C RID: 4508
	public int nId;

	// Token: 0x0400119D RID: 4509
	public string sActName;

	// Token: 0x0400119E RID: 4510
	public string sIconName;

	// Token: 0x0400119F RID: 4511
	public string sActDesc1;

	// Token: 0x040011A0 RID: 4512
	public string sActDesc2;

	// Token: 0x040011A1 RID: 4513
	public string[] szReward;
}

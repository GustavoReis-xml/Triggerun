using System;
using System.Xml;

// Token: 0x02000156 RID: 342
public class GoddessGiftData
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x00090134 File Offset: 0x0008E334
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

	// Token: 0x04000C60 RID: 3168
	public int nId;

	// Token: 0x04000C61 RID: 3169
	public string sActName;

	// Token: 0x04000C62 RID: 3170
	public string sIconName;

	// Token: 0x04000C63 RID: 3171
	public string sActDesc1;

	// Token: 0x04000C64 RID: 3172
	public string sActDesc2;

	// Token: 0x04000C65 RID: 3173
	public string[] szReward;
}

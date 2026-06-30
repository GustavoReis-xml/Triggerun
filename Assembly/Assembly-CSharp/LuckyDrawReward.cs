using System;
using System.Xml;

// Token: 0x02000227 RID: 551
public class LuckyDrawReward
{
	// Token: 0x060018DC RID: 6364 RVA: 0x000E841C File Offset: 0x000E661C
	public void Init(XmlElement xml)
	{
		this.nIndex = int.Parse(xml.GetAttribute("id"));
		this.baseRate = int.Parse(xml.GetAttribute("baserate"));
		this.szReward = xml.GetAttribute("reward").Split(new char[]
		{
			'#'
		});
		bool flag = this.szReward.Length != 0;
		if (flag)
		{
			this.szShowItem = this.szReward[0].Split(new char[]
			{
				','
			});
		}
		this.sShowName = xml.GetAttribute("name").Replace("\\n", "\n");
		this.sDesc1 = xml.GetAttribute("desc1").Replace("\\n", "\n");
		this.sDesc2 = xml.GetAttribute("desc2").Replace("\\n", "\n");
	}

	// Token: 0x04001385 RID: 4997
	public int nIndex;

	// Token: 0x04001386 RID: 4998
	public string sShowName;

	// Token: 0x04001387 RID: 4999
	public int baseRate;

	// Token: 0x04001388 RID: 5000
	public string sDesc1;

	// Token: 0x04001389 RID: 5001
	public string sDesc2;

	// Token: 0x0400138A RID: 5002
	public string[] szReward;

	// Token: 0x0400138B RID: 5003
	public string[] szShowItem;
}

using System;
using System.Collections;
using System.Xml;

// Token: 0x020003FA RID: 1018
public class TaskChapterData
{
	// Token: 0x06002ABB RID: 10939 RVA: 0x00178824 File Offset: 0x00176A24
	public void Init(XmlElement xml)
	{
		this.m_nChapterId = int.Parse(xml.GetAttribute("id"));
		this.m_sChapterName = xml.GetAttribute("name");
		this.m_szTaskList = ItemData.GetArrlist(xml.GetAttribute("taskdata"), '|', false, ',');
		this.m_szRewardList = ItemData.GetArrlist(xml.GetAttribute("reward"), '|', false, ',');
		this.m_sChapterbg = xml.GetAttribute("chapterbg");
		this.m_sRewardbg = xml.GetAttribute("rewardbg");
	}

	// Token: 0x0400244D RID: 9293
	public int m_nChapterId;

	// Token: 0x0400244E RID: 9294
	public string m_sChapterName;

	// Token: 0x0400244F RID: 9295
	public ArrayList m_szTaskList;

	// Token: 0x04002450 RID: 9296
	public int m_nNextChapterId;

	// Token: 0x04002451 RID: 9297
	public string m_sChapterbg;

	// Token: 0x04002452 RID: 9298
	public string m_sRewardbg;

	// Token: 0x04002453 RID: 9299
	public ArrayList m_szRewardList;
}

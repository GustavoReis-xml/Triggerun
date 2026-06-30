using System;
using System.Collections;

// Token: 0x0200022E RID: 558
public class MailStateInfo
{
	// Token: 0x06001911 RID: 6417 RVA: 0x000EB15A File Offset: 0x000E935A
	public void OnInit(ArrayList szInfo)
	{
		this.m_nMailId = (int)szInfo[0];
		this.m_nIsRead = (int)szInfo[1];
		this.m_nIsGet = (int)szInfo[2];
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x000EB193 File Offset: 0x000E9393
	public void OnSetIsItem(int nState)
	{
		this.m_nIsItem = nState;
	}

	// Token: 0x040013CC RID: 5068
	public int m_nMailId;

	// Token: 0x040013CD RID: 5069
	public int m_nIsRead;

	// Token: 0x040013CE RID: 5070
	public int m_nIsGet;

	// Token: 0x040013CF RID: 5071
	public int m_nIsItem;
}

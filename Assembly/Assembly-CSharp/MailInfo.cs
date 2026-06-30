using System;
using System.Collections;

// Token: 0x0200022C RID: 556
public class MailInfo
{
	// Token: 0x060018FD RID: 6397 RVA: 0x000EA4F4 File Offset: 0x000E86F4
	public void OnInit(ArrayList szInfo)
	{
		this.m_nMailId = (int)szInfo[0];
		this.m_sMailtitle = Language.GetUTFCodeStr(szInfo[1].ToString());
		this.m_sMailtext = Language.GetUTFCodeStr(szInfo[2].ToString());
		this.m_sSendNick = Language.GetUTFCodeStr(szInfo[3].ToString());
		this.m_sCreateTime = szInfo[4].ToString().Replace("-", ".");
		this.m_szItem = (ArrayList)szInfo[5];
		this.m_nIsItem = (int)szInfo[6];
	}

	// Token: 0x040013BD RID: 5053
	public int m_nMailId;

	// Token: 0x040013BE RID: 5054
	public string m_sMailtitle;

	// Token: 0x040013BF RID: 5055
	public string m_sMailtext;

	// Token: 0x040013C0 RID: 5056
	public string m_sSendNick;

	// Token: 0x040013C1 RID: 5057
	public string m_sCreateTime;

	// Token: 0x040013C2 RID: 5058
	public ArrayList m_szItem;

	// Token: 0x040013C3 RID: 5059
	public int m_nIsItem;
}

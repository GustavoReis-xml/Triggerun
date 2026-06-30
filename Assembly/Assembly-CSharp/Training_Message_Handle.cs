using System;
using System.Collections;

// Token: 0x0200047E RID: 1150
public class Training_Message_Handle
{
	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x0018942C File Offset: 0x0018762C
	public static Training_Message_Handle Instantiation
	{
		get
		{
			bool flag = Training_Message_Handle._Instantiation == null;
			if (flag)
			{
				Training_Message_Handle._Instantiation = new Training_Message_Handle();
			}
			return Training_Message_Handle._Instantiation;
		}
	}

	// Token: 0x06002CAA RID: 11434 RVA: 0x0018945C File Offset: 0x0018765C
	public void Message_Handle(int _Net_ID, ArrayList _Parameters)
	{
		bool flag = _Net_ID == 110201;
		if (flag)
		{
		}
	}

	// Token: 0x040025E2 RID: 9698
	private static Training_Message_Handle _Instantiation;
}

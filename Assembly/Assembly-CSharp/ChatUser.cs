using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class ChatUser : NetObj
{
	// Token: 0x060004CE RID: 1230 RVA: 0x00027794 File Offset: 0x00025994
	public ChatUser()
	{
		fGameManager.ChatPlayer = this;
		bool flag = ChatUICon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("ChatUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), null, true);
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00027804 File Offset: 0x00025A04
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
		ChatManager.GetInstance().OnInitUserChat();
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00027812 File Offset: 0x00025A12
	public override void OnCreate()
	{
		base.OnCreate();
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0002781C File Offset: 0x00025A1C
	public void SendMsgToChatServer(int nNetID, ArrayList szMsg)
	{
		this.TosFun("SendMsgToChatServer", new object[]
		{
			nNetID,
			szMsg
		});
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0002783E File Offset: 0x00025A3E
	private void InitData(ArrayList szData)
	{
		this.m_szBanTalkData = (ArrayList)szData[0];
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00027854 File Offset: 0x00025A54
	public void SendMsgToChatClient(int nNetID, ArrayList szMsg)
	{
		int num = nNetID;
		int num2 = num;
		if (num2 != 1001)
		{
			if (num2 != 1006)
			{
				if (num2 != 3000)
				{
					Debug.LogError("SendMsgToChatClient 消息ID没有找到." + nNetID.ToString());
				}
				else
				{
					this.OnRadioNetMsg((int)szMsg[0], (ArrayList)szMsg[1]);
				}
			}
			else
			{
				ChatManager.GetInstance().Chat_Server_To_ClientNetMsg((int)szMsg[0], szMsg);
			}
		}
		else
		{
			this.InitData(szMsg);
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x000278E2 File Offset: 0x00025AE2
	private void OnRadioNetMsg(int nNetID, ArrayList szData)
	{
		Debug.LogError("OnRadioNetMsg 消息ID没有找到." + nNetID.ToString());
	}

	// Token: 0x040003A3 RID: 931
	public ArrayList m_szBanTalkData = new ArrayList
	{
		string.Empty,
		0
	};
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class ScnMsgManager
{
	// Token: 0x060021F8 RID: 8696 RVA: 0x0013C7B4 File Offset: 0x0013A9B4
	public static ScnMsgManager GetInstance()
	{
		bool flag = ScnMsgManager.mInst == null;
		if (flag)
		{
			ScnMsgManager.mInst = new ScnMsgManager();
		}
		return ScnMsgManager.mInst;
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x0013C7E4 File Offset: 0x0013A9E4
	public void OnAddMsg(ArrayList szData, int nTimlong = 20)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			OnePushMsgInfo onePushMsgInfo = new OnePushMsgInfo
			{
				m_nType = (int)szData[0],
				m_sMsg = szData[1].ToString(),
				szData = (ArrayList)szData[2],
				dCreateTime = Fun.GetServerDate().ToString()
			};
			bool flag2 = ChatUICon.inst == null || (this.onShowPushMsgInfo != null && this.onShowPushMsgInfo.m_nState == 1);
			if (flag2)
			{
				this.m_szAllMsgList.Add(onePushMsgInfo);
			}
			else
			{
				bool flag3 = (onePushMsgInfo.m_nType != 2 || TeamManager.GetInstance().IsShowMsg(onePushMsgInfo) != 0) && ChatUICon.inst != null;
				if (flag3)
				{
					ChatUICon.inst.AddPushMsg(onePushMsgInfo, nTimlong);
				}
			}
		}
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x0013C8D0 File Offset: 0x0013AAD0
	public void OnInputKeyCode()
	{
		bool flag = fGameManager.Heroplayer != null && !MatchManager.GetInstance().isEnterFb && this.onShowPushMsgInfo != null && !(ChatUICon.inst == null);
		if (flag)
		{
			bool keyDown = Input.GetKeyDown(KeyCode.Y);
			if (keyDown)
			{
				this.OnKeyCodeYes();
			}
			else
			{
				bool keyDown2 = Input.GetKeyDown(KeyCode.N);
				if (keyDown2)
				{
					this.OnKeyCodeNo(true);
				}
			}
		}
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x0013C93C File Offset: 0x0013AB3C
	public void OnKeyCodeYes()
	{
		bool flag = this.onShowPushMsgInfo == null || ChatUICon.inst == null;
		if (!flag)
		{
			switch (this.onShowPushMsgInfo.m_nType)
			{
			case 1:
				GuildManager.GetInstance().OnInviteOk((int)this.onShowPushMsgInfo.szData[0], this.onShowPushMsgInfo.szData[1].ToString(), 1);
				break;
			case 2:
			{
				bool flag2 = MatchManager.GetInstance().m_nMatchType == 2;
				if (flag2)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40056, new object[0]), "ffdd00", "system");
					return;
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = (ArrayList)this.onShowPushMsgInfo.szData.Clone();
				arrayList2[0] = Fun.EncodingUTF8(arrayList2[0].ToString());
				arrayList.Add(60002);
				arrayList.Add(arrayList2);
				arrayList.Add(0);
				fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
				fUIManager.GetInstance().GotoHall(0);
				TeamManager.GetInstance().szAddTeamInfo = this.onShowPushMsgInfo.szData;
				break;
			}
			case 3:
				FriendManager.GetInstance().UnRequstMsg(1, this.onShowPushMsgInfo.szData[0].ToString(), (int)this.onShowPushMsgInfo.szData[1]);
				break;
			case 4:
				RoomManage.GetInstance().OnInviteBack(1, this.onShowPushMsgInfo.szData);
				break;
			case 5:
				fUIManager.GetInstance().GotoHall(0);
				break;
			}
			ChatUICon.inst.OnChangeNextMsg();
		}
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x0013CB14 File Offset: 0x0013AD14
	public void OnKeyCodeNo(bool iss = true)
	{
		bool flag = this.onShowPushMsgInfo == null || ChatUICon.inst == null;
		if (!flag)
		{
			switch (this.onShowPushMsgInfo.m_nType)
			{
			case 1:
				GuildManager.GetInstance().OnInviteOk((int)this.onShowPushMsgInfo.szData[0], this.onShowPushMsgInfo.szData[1].ToString(), 0);
				break;
			case 2:
				TeamManager.GetInstance().onMsgClick(true, 0, this.onShowPushMsgInfo.szData);
				break;
			case 3:
				if (iss)
				{
					FriendManager.GetInstance().UnRequstMsg(0, this.onShowPushMsgInfo.szData[0].ToString(), (int)this.onShowPushMsgInfo.szData[1]);
				}
				break;
			case 4:
				RoomManage.GetInstance().OnInviteBack(0, this.onShowPushMsgInfo.szData);
				break;
			case 5:
			{
				TeamManager.GetInstance().IsInitTeam = 1;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(0);
				arrayList.Add(0);
				bool flag2 = fGameManager.Heroplayer != null;
				if (flag2)
				{
					fGameManager.SendMsgToServer(60003, arrayList);
				}
				break;
			}
			}
			if (iss)
			{
				ChatUICon.inst.OnChangeNextMsg();
			}
		}
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x0013CC88 File Offset: 0x0013AE88
	public void OnClearPushMsg()
	{
		bool flag = ChatUICon.inst != null;
		if (flag)
		{
			ChatUICon.inst.ClosePushMsg();
		}
		this.m_szAllMsgList.Clear();
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x0013CCC0 File Offset: 0x0013AEC0
	public int GetPushMsgTimeLong(string sCreateTime, int m_nTimeLong = 20)
	{
		int endCDTime = Fun.GetEndCDTime(sCreateTime, m_nTimeLong);
		bool flag = endCDTime < 1;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = endCDTime;
		}
		return result;
	}

	// Token: 0x04001F64 RID: 8036
	public static ScnMsgManager mInst;

	// Token: 0x04001F65 RID: 8037
	public List<OnePushMsgInfo> m_szAllMsgList = new List<OnePushMsgInfo>();

	// Token: 0x04001F66 RID: 8038
	public OnePushMsgInfo onShowPushMsgInfo;
}

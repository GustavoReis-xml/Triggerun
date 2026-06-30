using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class LoginUser : NetObj
{
	// Token: 0x0600189E RID: 6302 RVA: 0x000E6828 File Offset: 0x000E4A28
	public LoginUser()
	{
		fGameManager.GetInstance().CloseLink(1);
		bool flag = GameRoot.Login_Control != null;
		if (flag)
		{
			GameRoot.Login_Control.m_nLogin = 0;
		}
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x000E6868 File Offset: 0x000E4A68
	public void ShowGamePlayer(ArrayList szgameplayer, string sServerTime, int nOldUser, int nIsFf)
	{
		bool flag = szgameplayer.Count == 0;
		if (flag)
		{
			this.OnCreatPlayer();
		}
		else
		{
			this.playname = szgameplayer[0].ToString();
			bool flag2 = (int)szgameplayer[1] == 0;
			if (flag2)
			{
				this.OnCreatPlayer();
			}
		}
		this.SaveUserSystemInfo();
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000E68C8 File Offset: 0x000E4AC8
	public void OnCreatPlayer()
	{
		fGameManager.m_nJoinState = 15;
		GameRoot.LoginSocket.unFsorket.ToServer("OnCreatePlayer", new object[]
		{
			Fun.EncodingUTF8(RandomName.GetRandName()),
			0,
			0,
			1,
			1,
			SystemInfo.graphicsDeviceName
		});
		GameRoot.inst.SendHttpRequest(1602, new ArrayList());
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000E6948 File Offset: 0x000E4B48
	public void SendUserLoginInfo(int nNetID, ArrayList szMsg)
	{
		Fun.Log(string.Format("SendUserLoginInfo---------------{0}", nNetID), null);
		if (nNetID == 1119)
		{
			this.OnLoginFail(szMsg);
		}
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000E6985 File Offset: 0x000E4B85
	private void OnLoginFail(ArrayList szMsg)
	{
		Fun.Log(string.Format("OnLoginFail:{0}", szMsg[1]), null);
		this.SendLoginState(1003, "OnLoginFail");
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000E69B4 File Offset: 0x000E4BB4
	public void OnLoginOK(int nCid, int nAutoID, string sNick, ArrayList szHall)
	{
		Fun.Log(string.Format("OnLoginOK-{0},{1}", sNick, nAutoID), null);
		this.SendMessageToLoginClient(1, new ArrayList
		{
			1,
			new ArrayList()
		});
		bool flag = szHall.Count < 3;
		if (flag)
		{
			Fun.Log(string.Format("OnLoginOK szHall is null.{0}{1}", nCid, sNick), null);
		}
		else
		{
			string sIp = (string)szHall[0];
			int nPort = (int)szHall[1];
			string sGroup = (string)szHall[2];
			fGameManager.m_nJoinState = 9;
			fGameManager.GetInstance().SetHallParam(nCid, sGroup, sNick, nAutoID, sIp, nPort);
			fGameManager.GetInstance().OnLoginOK();
		}
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000E6A7C File Offset: 0x000E4C7C
	public void onFault(int nState, ArrayList szUserLoginInfo)
	{
		Fun.Log(string.Format("onFault-{0}", nState), null);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000E6A96 File Offset: 0x000E4C96
	public void IsMultiNick(string sNick, int nRet)
	{
		Fun.Log(string.Format("IsMultiNick-{0},{1}", sNick, nRet), null);
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000E6AB1 File Offset: 0x000E4CB1
	public void SendLoginState(int nstate, string info)
	{
		Fun.Log(string.Format("SendLoginState-{0},{1}", nstate, info), null);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000E6ACC File Offset: 0x000E4CCC
	public void SaveUserSystemInfo()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		arrayList2.Add(0);
		arrayList2.Add(SystemInfo.graphicsDeviceName);
		arrayList2.Add(SystemInfo.graphicsMemorySize);
		arrayList2.Add(SystemInfo.systemMemorySize);
		arrayList2.Add(SystemInfo.operatingSystem);
		arrayList2.Add(SystemInfo.processorCount);
		arrayList2.Add(SystemInfo.processorType);
		arrayList2.Add("explore");
		arrayList2.Add(SystemInfo.graphicsDeviceVendor);
		arrayList2.Add(SystemInfo.graphicsDeviceID);
		arrayList2.Add(SystemInfo.graphicsDeviceVendorID);
		arrayList2.Add(SystemInfo.graphicsDeviceVersion);
		arrayList2.Add(SystemInfo.graphicsShaderLevel);
		bool supportsShadows = SystemInfo.supportsShadows;
		if (supportsShadows)
		{
			arrayList2.Add(1);
		}
		else
		{
			arrayList2.Add(0);
		}
		arrayList2.Add(1);
		bool supportsImageEffects = SystemInfo.supportsImageEffects;
		if (supportsImageEffects)
		{
			arrayList2.Add(1);
		}
		else
		{
			arrayList2.Add(0);
		}
		arrayList2.Add(string.Empty);
		arrayList2.Add(SystemInfo.deviceName);
		arrayList2.Add(SystemInfo.deviceModel);
		arrayList.Add(arrayList2);
		this.TosFun("SaveUserSystemInfo", new object[]
		{
			arrayList
		});
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000E6C44 File Offset: 0x000E4E44
	public void SendMessageToLoginClient(int _Net_ID, ArrayList _Parameters)
	{
		fGameManager.m_nLostReason = int.Parse(_Parameters[0].ToString());
		bool flag = _Net_ID == 1002;
		if (flag)
		{
			bool flag2 = (int)_Parameters[0] == 1016;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)_Parameters[1];
				RandomName.AddUsedName(Language.GetUTFCodeStr(arrayList[3].ToString()));
				this.OnCreatPlayer();
			}
			bool flag3 = NewRoleUICon.inst != null;
			if (flag3)
			{
				NewRoleUICon.inst.ShowTipMsg(_Parameters);
			}
		}
		else
		{
			bool flag4 = !WebParam.isExeLogin;
			if (flag4)
			{
				GameRoot.Login_Control.Message_Handle(int.Parse(_Parameters[0].ToString()), (ArrayList)_Parameters[1]);
			}
			else
			{
				fGameManager.GetInstance().Message_Handle(int.Parse(_Parameters[0].ToString()), (ArrayList)_Parameters[1]);
			}
		}
	}

	// Token: 0x0400135C RID: 4956
	public string playname;

	// Token: 0x0400135D RID: 4957
	public int playercid;
}

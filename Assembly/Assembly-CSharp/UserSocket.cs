using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200048F RID: 1167
public class UserSocket : MonoBehaviour
{
	// Token: 0x06002D0B RID: 11531 RVA: 0x0018C474 File Offset: 0x0018A674
	private void Start()
	{
	}

	// Token: 0x06002D0C RID: 11532 RVA: 0x0018C478 File Offset: 0x0018A678
	public void InitUserSocket(int socketType)
	{
		this.msocketType = socketType;
		bool flag = this.unFsorket == null;
		if (flag)
		{
			this.unFsorket = new BaseSocket();
		}
		this.unFsorket.setDefobj(this);
		Application.runInBackground = true;
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x0018C4BC File Offset: 0x0018A6BC
	private void Update()
	{
		bool flag = this.unFsorket != null;
		if (flag)
		{
			this.unFsorket.OnFrame();
		}
		this.OnFrameError();
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x0018C4EC File Offset: 0x0018A6EC
	private void OnFrameError()
	{
		for (int i = 0; i < this.m_szError.Count; i++)
		{
			bool bLosted = this.m_bLosted;
			if (bLosted)
			{
				break;
			}
			this.OnError(this.m_szError[i].m_nCode, this.m_szError[i].m_sMsg);
		}
		this.m_szError.Clear();
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x0018C558 File Offset: 0x0018A758
	public void breakIt()
	{
		this.m_bClosed = true;
		bool flag = this.unFsorket != null;
		if (flag)
		{
			this.unFsorket.Closed();
			this.unFsorket.clear();
		}
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x0018C594 File Offset: 0x0018A794
	private void OnApplicationQuit()
	{
		bool flag = fGameManager.m_nJoinState != 17 && !fGameManager.isClose;
		if (flag)
		{
			Debug.Log("usersocket//" + fGameManager.m_nJoinState.ToString() + "//" + fGameManager.isClose.ToString());
		}
		bool flag2 = Time.time < 120f;
		if (flag2)
		{
			Debug.Log("userscoket--onapplicationquit--timelimit--" + Time.time.ToString());
		}
		bool flag3 = fGameManager.LobbyPlayer != null;
		if (flag3)
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		bool flag4 = fGameManager.Heroplayer != null;
		if (flag4)
		{
			fGameManager.Heroplayer.SendMessageToServer(20002, new ArrayList
			{
				2,
				0
			});
		}
		this.m_bClosed = true;
		FPS.instance.sendFPSMsgToServer();
		VoiceGameCon.Inst.UnInit();
		bool flag5 = this.unFsorket != null;
		if (flag5)
		{
			this.unFsorket.Closed();
		}
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x0018C6CA File Offset: 0x0018A8CA
	public void SetProxy(string sProxyIp, string sProxyPort)
	{
		this.m_sip_proxy = sProxyIp;
		this.m_nprot_proxy = int.Parse(sProxyPort);
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x0018C6E0 File Offset: 0x0018A8E0
	public void SetIpAndPort()
	{
		string text = string.Empty;
		text = WebParam.client_ips;
		bool flag = string.IsNullOrEmpty(text);
		if (!flag)
		{
			string[] array = text.Split(new char[]
			{
				','
			});
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				int num = UnityEngine.Random.Range(0, array.Length);
				string[] array2 = array[num].Split(new char[]
				{
					':'
				});
				bool flag3 = array2.Length != 0;
				if (flag3)
				{
					this.m_sIP = array2[0];
					this.m_nPort = int.Parse(array2[1]);
				}
				else
				{
					Debug.LogError("SetIpAndPort is Error." + text);
				}
			}
			else
			{
				Debug.LogError("SetIpAndPort is Error." + text);
			}
		}
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x0018C79B File Offset: 0x0018A99B
	public void ReconnectServer()
	{
		this.m_bClosed = false;
		this.m_bLosted = false;
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x0018C7AC File Offset: 0x0018A9AC
	public void reConnet()
	{
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x0018C7B0 File Offset: 0x0018A9B0
	public void linkOk()
	{
		Debug.Log("linkOk");
		this.reLinked = true;
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x0018C7ED File Offset: 0x0018A9ED
	public void cancelRelink()
	{
		Debug.Log("cancelRelink");
		this.beBreak = true;
		base.StopAllCoroutines();
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x0018C80C File Offset: 0x0018AA0C
	public void relink()
	{
		this.reLinked = false;
		Debug.Log("重新连线");
		fUISoundManager.instance.RelinkStopSound();
		fGameManager.mInst.OnReturnHallOK(0, string.Empty, 0);
		this.beBreak = false;
		base.StopAllCoroutines();
		base.StartCoroutine(this.reConnetCallOut());
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x0018C864 File Offset: 0x0018AA64
	private IEnumerator reConnetCallOut()
	{
		Debug.Log("reConnetCallOut");
		fUIManager.GetInstance().CloseAll();
		fUIManager.GetInstance().ShowMainUIBackGround();
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
		TipManager.GetInstance().ShowComTipMsg(string.Empty, Language.GetLang(1507, new object[0]), 0f, null, null);
		yield return new WaitForSeconds(3f);
		bool flag2 = this.beBreak;
		if (flag2)
		{
			Debug.Log("reConnetCallOut beBreak");
			yield break;
		}
		fGameManager.GetInstance().mloadName = string.Empty;
		SceneManager.LoadScene(1);
		string savestr = GameRoot.inst.serverSaveStr();
		while (!this.reLinked)
		{
			bool flag3 = easy_tipCon.inst != null;
			if (flag3)
			{
				easy_tipCon.inst.ClearPopTip(true);
			}
			TipManager.GetInstance().ShowComTipMsg(string.Empty, Language.GetLang(1508, new object[0]), 0f, null, null);
			this.ReconnectServer();
			this.unFsorket.InitClientScoketInfo("LoginServer", "LoginUser", WebParam.client_account, WebParam.client_password, string.Concat(new string[]
			{
				"s1#HS#",
				SysConfig.g_client,
				"#",
				Fun.GetUdId(),
				"#",
				Fun.GetMAC0()
			}));
			this.connectServer();
			Debug.Log("等待重连");
			yield return new WaitForSeconds(35f);
			Debug.Log("超时");
			bool flag4 = this.beBreak;
			if (flag4)
			{
				Debug.Log("InitClientScoketInfo beBreak");
				break;
			}
		}
		Debug.Log("重连结束");
		yield break;
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x0018C874 File Offset: 0x0018AA74
	public void ConnectServer(string sIp, int nPort, string sGroup, string sUser, string sAccount, string sPwd, string sExtMsg)
	{
		bool flag = !this.m_bClosed && sUser == "PlayerUser";
		if (flag)
		{
			fGameManager.mInst.OnReturnHallOK(0, string.Empty, 0);
			this.ConnectServer(sIp, nPort, sGroup, sUser, sAccount, sPwd, sExtMsg);
		}
		else
		{
			this.m_bClosed = false;
			this.m_bLosted = false;
			bool flag2 = this.unFsorket != null;
			if (flag2)
			{
				this.m_sIP = sIp;
				this.m_nPort = nPort;
				this.unFsorket.InitClientScoketInfo(sGroup, sUser, sAccount, sPwd, sExtMsg);
				this.unFsorket.connectServer(sIp, nPort);
			}
		}
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x0018C914 File Offset: 0x0018AB14
	private void ConnectLog(string stype, string sIp, int nPort, string sGroup, string sUser, string sAccount, string sPwd, string sExtMsg)
	{
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(stype);
			arrayList.Add(string.Concat(new string[]
			{
				"连接状态:",
				sIp,
				",",
				sGroup,
				",",
				sUser,
				",",
				sAccount,
				",",
				sPwd,
				",",
				sExtMsg
			}));
			arrayList.Add(DateTime.Now.ToString());
			arrayList.Add(SysConfig.g_client);
			arrayList.Add(string.Empty);
			fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
		}
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x0018C9E1 File Offset: 0x0018ABE1
	public void connectServer()
	{
		this.m_bClosed = false;
		this.m_bLosted = false;
		this.unFsorket.connectServer(this.m_sIP, this.m_nPort);
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x0018CA0A File Offset: 0x0018AC0A
	public void connectOK()
	{
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x0018CA0D File Offset: 0x0018AC0D
	public void connectErr(string msg)
	{
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x0018CA10 File Offset: 0x0018AC10
	public void SockErr(string msg, string emsg)
	{
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x0018CA13 File Offset: 0x0018AC13
	public void DebugMsg(string msg)
	{
		Debug.Log("DebugMsg: " + msg);
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x0018CA27 File Offset: 0x0018AC27
	public void DestroyPlayer(NetObj play, Transform obj)
	{
		UnityEngine.Object.Destroy(obj.gameObject);
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x0018CA38 File Offset: 0x0018AC38
	internal void OnLost(string sMsg)
	{
		bool flag = !this.m_bClosed;
		if (flag)
		{
			fGameManager.GetInstance().OnUserLost(sMsg);
		}
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x0018CA64 File Offset: 0x0018AC64
	internal void OnSocketError(int nErrorCode, string sMsg)
	{
		SocketError item = new SocketError
		{
			m_nCode = nErrorCode,
			m_sMsg = sMsg
		};
		this.m_szError.Add(item);
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x0018CA9C File Offset: 0x0018AC9C
	public string GetSocketInfo()
	{
		return string.Format("socketType = {0}, m_bClosed = {1}, m_szError.count={2}", this.msocketType, this.m_bClosed, this.m_szError.Count);
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x0018CAE0 File Offset: 0x0018ACE0
	private void OnLogTimerOverError()
	{
		ArrayList arrayList = new ArrayList();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("host = {0}:{1}", this.m_sIP, this.m_nPort);
		stringBuilder.AppendFormat(",time = {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
		stringBuilder.AppendFormat(",socketType = {0}", this.msocketType);
		stringBuilder.AppendFormat(",loginsocket = {0}", GameRoot.LoginSocket.GetSocketInfo());
		stringBuilder.AppendFormat(",hallsocket = {0}", GameRoot.HallSocket.GetSocketInfo());
		stringBuilder.AppendFormat(",scenesocket = {0}", GameRoot.SceneSocket.GetSocketInfo());
		arrayList.Add(stringBuilder.ToString());
		GameRoot.inst.SendHttpRequest(1608, arrayList);
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x0018CBAC File Offset: 0x0018ADAC
	internal void OnError(int nErrorCode, string sMsg)
	{
		Debug.Log(string.Format("OnSocketError: errorcode = {0}.msg = {1}.closed = {2}.sockettype = {3}." + DateTime.Now.ToString(), new object[]
		{
			nErrorCode,
			sMsg,
			this.m_bClosed,
			this.msocketType
		}));
		bool flag = this.msocketType == 1 && GameRoot.Login_Control != null;
		if (flag)
		{
			GameRoot.Login_Control.m_nLogin = 0;
		}
		bool flag2 = nErrorCode == 10053 && !this.m_bClosed;
		if (flag2)
		{
			Debug.Log("Socket 10053." + DateTime.Now.ToString());
		}
		if (nErrorCode <= 10051)
		{
			if (nErrorCode != 1 && nErrorCode != 10051)
			{
				return;
			}
		}
		else if (nErrorCode - 10053 > 1)
		{
			if (nErrorCode != 10060)
			{
				return;
			}
			bool flag3 = !this.m_bClosed;
			if (flag3)
			{
				this.OnLogTimerOverError();
				Debug.Log("Socket Time out." + DateTime.Now.ToString());
				fGameManager.GetInstance().OnTimerOver(this.m_sIP, this.m_nPort);
			}
			return;
		}
		this.m_bLosted = true;
		this.OnLost(sMsg);
	}

	// Token: 0x04002641 RID: 9793
	public BaseSocket unFsorket;

	// Token: 0x04002642 RID: 9794
	public string m_sip_proxy = string.Empty;

	// Token: 0x04002643 RID: 9795
	public int m_nprot_proxy;

	// Token: 0x04002644 RID: 9796
	public string m_sIP = "10.207.153.107";

	// Token: 0x04002645 RID: 9797
	public int m_nPort = 2500;

	// Token: 0x04002646 RID: 9798
	private bool reLinked;

	// Token: 0x04002647 RID: 9799
	private bool beBreak;

	// Token: 0x04002648 RID: 9800
	public Transform playerModel;

	// Token: 0x04002649 RID: 9801
	public Transform Player;

	// Token: 0x0400264A RID: 9802
	private List<SocketError> m_szError = new List<SocketError>();

	// Token: 0x0400264B RID: 9803
	public int msocketType;

	// Token: 0x0400264C RID: 9804
	private bool m_bClosed;

	// Token: 0x0400264D RID: 9805
	private bool m_bLosted;

	// Token: 0x0400264E RID: 9806
	public int m_nLost;
}

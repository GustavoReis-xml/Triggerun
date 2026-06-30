using System;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class testlogin : MonoBehaviour
{
	// Token: 0x06002C62 RID: 11362 RVA: 0x00186594 File Offset: 0x00184794
	private void Start()
	{
		this.savestr = GameRoot.inst.serverSaveStr();
		this.uss = fGameManager.GetInstance().GetSocket();
		this.ipstr = PlayerPrefs.GetString(this.savestr + "ip");
		this.maccc = PlayerPrefs.GetString(this.savestr + "uName");
		this.mpassd = PlayerPrefs.GetString(this.savestr + "uPwd");
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x00186614 File Offset: 0x00184814
	private void SetProxyInfo(int nCid, string sProxyInfo)
	{
		bool flag = !string.IsNullOrEmpty(sProxyInfo);
		if (flag)
		{
			string[] array = sProxyInfo.Split(new char[]
			{
				','
			});
			string[] array2 = array[nCid % array.Length].Split(new char[]
			{
				':'
			});
			this.client_proxyip = array2[0];
			this.client_proxyport = array2[1];
		}
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x00186670 File Offset: 0x00184870
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width / 2 - 60), (float)(Screen.height / 2 - 80), 100f, 20f), "IP");
		this.ipstr = GUI.TextField(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2 - 80), 100f, 20f), this.ipstr);
		GUI.Label(new Rect((float)(Screen.width / 2 - 60), (float)(Screen.height / 2 - 40), 100f, 20f), "账号");
		this.maccc = GUI.TextField(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2 - 40), 100f, 20f), this.maccc);
		GUI.Label(new Rect((float)(Screen.width / 2 - 60), (float)(Screen.height / 2), 100f, 20f), "密码");
		this.mpassd = GUI.TextField(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 100f, 20f), this.mpassd);
		bool flag = GUI.Button(new Rect((float)(Screen.width / 2 - 70), (float)(Screen.height / 2 + 40), 80f, 20f), "Connect");
		if (flag)
		{
			bool flag2 = this.m_sProxy.Length > 0;
			if (flag2)
			{
				this.SetProxyInfo(this.maccc.GetHashCode(), this.m_sProxy);
			}
			bool flag3 = !string.IsNullOrEmpty(this.client_proxyip) && !string.IsNullOrEmpty(this.client_proxyport);
			if (flag3)
			{
				this.uss.SetProxy(this.client_proxyip, this.client_proxyport);
			}
			this.uss.m_sIP = this.ipstr;
			this.uss.m_nPort = 11041;
			PlayerPrefs.SetString(this.savestr + "ip", this.ipstr);
			PlayerPrefs.SetString(this.savestr + "uName", this.maccc);
			PlayerPrefs.SetString(this.savestr + "uPwd", this.mpassd);
			SysConfig.gUserName = this.maccc;
			this.uss.SetIpAndPort();
			this.uss.unFsorket.InitClientScoketInfo("LoginServer", "LoginUser", this.maccc, this.mpassd, string.Concat(new string[]
			{
				this.m_sServer,
				"#HS#",
				SysConfig.g_client,
				"#",
				Fun.GetUdId(),
				"#",
				Fun.GetMAC0()
			}));
			this.uss.connectServer();
			Fun.Log("Connect:" + this.uss.m_sIP + ":" + this.uss.m_nPort.ToString(), null);
			base.gameObject.SetActive(false);
		}
		bool flag4 = GUI.Button(new Rect((float)(Screen.width / 2 + 20), (float)(Screen.height / 2 + 40), 80f, 20f), "Reg");
		if (flag4)
		{
			Debuger.OpenURL("http://192.168.0.8/reg/reg.aspx");
			Fun.Log("Url", null);
		}
	}

	// Token: 0x040025BE RID: 9662
	public BaseSocket unFsorket;

	// Token: 0x040025BF RID: 9663
	public string ipstr = "127.0.0.1";

	// Token: 0x040025C0 RID: 9664
	private string maccc = "feer";

	// Token: 0x040025C1 RID: 9665
	private string mpassd = "111111";

	// Token: 0x040025C2 RID: 9666
	public string m_sProxy = string.Empty;

	// Token: 0x040025C3 RID: 9667
	public string m_sServer = "s1";

	// Token: 0x040025C4 RID: 9668
	public UserSocket uss;

	// Token: 0x040025C5 RID: 9669
	private string savestr = string.Empty;

	// Token: 0x040025C6 RID: 9670
	private string client_proxyip = string.Empty;

	// Token: 0x040025C7 RID: 9671
	private string client_proxyport = string.Empty;
}

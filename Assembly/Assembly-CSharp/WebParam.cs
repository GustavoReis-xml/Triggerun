using System;
using UnityEngine;

// Token: 0x0200049A RID: 1178
public class WebParam : MonoBehaviour
{
	// Token: 0x06002DA8 RID: 11688 RVA: 0x00192D04 File Offset: 0x00190F04
	private void StartIn()
	{
		WebParam.client_version = int.Parse(WebParam.client_gameversion.Replace(".", string.Empty)).ToString();
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x00192D38 File Offset: 0x00190F38
	public void getWebParam()
	{
		bool flag = Application.platform == RuntimePlatform.WindowsPlayer;
		if (flag)
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			bool flag2 = commandLineArgs.Length >= 2;
			if (flag2)
			{
				string str = commandLineArgs[1];
				this.GetExeStr(str);
			}
		}
		else
		{
			this.StartIn();
		}
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x00192D80 File Offset: 0x00190F80
	public void traceWeb(string sMsg)
	{
		Application.ExternalCall("Clogin", new object[]
		{
			sMsg
		});
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x00192D98 File Offset: 0x00190F98
	public void GetStr(string str)
	{
		WebParam.LoginUI_web_login = "1";
		Application.ExternalCall("Clogin", new object[]
		{
			"11111"
		});
		bool flag = !(str == string.Empty);
		if (flag)
		{
			this.SetData(str);
		}
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x00192DE5 File Offset: 0x00190FE5
	public void GetExeStr(string str)
	{
		this.SetData(str);
		this.OpenPlugins();
		this.ReportStartgame();
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x00192E00 File Offset: 0x00191000
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
			WebParam.client_proxyip = array2[0];
			bool flag2 = array2.Length > 1;
			if (flag2)
			{
				WebParam.client_proxyport = array2[1];
			}
			else
			{
				WebParam.client_proxyport = "80";
			}
		}
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x00192E74 File Offset: 0x00191074
	private void SetData(string str)
	{
		string[] array = str.Split(new char[]
		{
			'|'
		});
		bool flag = array.Length > 20;
		if (flag)
		{
			WebParam.client_resurl_force_local = false;
			WebParam.isExeLogin = true;
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				WebParam.client_cid = array[0];
			}
			bool flag3 = array.Length > 1;
			if (flag3)
			{
				WebParam.client_account = array[1];
			}
			bool flag4 = array.Length > 2;
			if (flag4)
			{
				WebParam.client_areaID = array[2];
			}
			bool flag5 = array.Length > 3;
			if (flag5)
			{
				WebParam.client_password = array[3];
			}
			bool flag6 = array.Length > 4;
			if (flag6)
			{
				WebParam.client_serverweburl = array[4];
			}
			bool flag7 = array.Length > 5;
			if (flag7)
			{
				WebParam.client_pf = array[5];
			}
			bool flag8 = array.Length > 6;
			if (flag8)
			{
				WebParam.client_ips = array[6];
			}
			bool flag9 = array.Length > 7;
			if (flag9)
			{
				int num = 10002;
				int.TryParse(array[7], out num);
				WebParam.client_port = num;
			}
			bool flag10 = array.Length > 8;
			if (flag10)
			{
				WebParam.client_serverid = array[8];
			}
			bool flag11 = array.Length > 9;
			if (flag11)
			{
				WebParam.client_gameversion = array[9];
			}
			bool flag12 = array.Length > 10;
			if (flag12)
			{
				WebParam.client_company = array[10];
			}
			bool flag13 = array.Length > 11;
			if (flag13)
			{
				WebParam.client_resurl = array[11];
			}
			bool flag14 = array.Length > 12;
			if (flag14)
			{
				WebParam.client_resurl2 = array[12];
			}
			bool flag15 = array.Length > 13;
			if (flag15)
			{
				WebParam.client_language = array[13];
			}
			bool flag16 = array.Length > 14;
			if (flag16)
			{
				WebParam.client_appid = array[14];
			}
			bool flag17 = array.Length > 16;
			if (flag17)
			{
				WebParam.client_islogover = array[16];
			}
			bool flag18 = array.Length > 20;
			if (flag18)
			{
				WebParam.client_loadtype = array[20];
			}
			bool flag19 = array.Length > 21;
			if (flag19)
			{
				WebParam.client_cache = array[21];
			}
			bool flag20 = array.Length > 22;
			if (flag20)
			{
				WebParam.client_opentime = array[22];
			}
			bool flag21 = array.Length > 23;
			if (flag21)
			{
				WebParam.client_chongzhiurl = array[23];
			}
			bool flag22 = array.Length > 24;
			if (flag22)
			{
				WebParam.client_bbsurl = array[24];
			}
			bool flag23 = array.Length > 25;
			if (flag23)
			{
				WebParam.client_shoucangurl = array[25];
			}
			bool flag24 = array.Length > 26;
			if (flag24)
			{
				WebParam.client_newcardsurl = array[26];
			}
			bool flag25 = array.Length > 27;
			if (flag25)
			{
				WebParam.client_fcmurl = array[27];
			}
			bool flag26 = array.Length > 28;
			if (flag26)
			{
				WebParam.client_kefuurl = array[28];
			}
			bool flag27 = array.Length > 29;
			if (flag27)
			{
				WebParam.client_proxyip = array[29];
			}
			bool flag28 = array.Length > 30;
			if (flag28)
			{
				WebParam.client_proxyport = array[30];
			}
			int nCid = 0;
			int.TryParse(WebParam.client_cid, out nCid);
			this.SetProxyInfo(nCid, WebParam.client_proxyip);
			bool flag29 = array.Length > 31;
			if (flag29)
			{
				WebParam.client_clientip = array[31];
			}
			try
			{
				bool flag30 = array.Length > 32;
				if (flag30)
				{
					WebParam.client_jointeststate = int.Parse(array[32]);
				}
			}
			catch
			{
				WebParam.client_jointeststate = 0;
			}
			bool flag31 = string.IsNullOrEmpty(WebParam.client_resurl2);
			if (flag31)
			{
				WebParam.client_resurl2 = WebParam.client_resurl;
			}
			SysConfig.gUserName = WebParam.client_cid;
			WebParam.client_version = int.Parse(WebParam.client_gameversion.Replace(".", string.Empty)).ToString();
		}
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x001931BC File Offset: 0x001913BC
	public void SetEditorSocketInfo(string sProxy, string sIp, int nPort, string sServer, string sAcc, string sPwd)
	{
		bool flag = !string.IsNullOrEmpty(sProxy);
		if (flag)
		{
			string[] array = sProxy.Split(new char[]
			{
				':'
			});
			WebParam.client_proxyip = array[0];
			WebParam.client_proxyport = array[1];
		}
		WebParam.client_ips = sIp + ":" + nPort.ToString();
		WebParam.client_port = nPort;
		WebParam.client_serverid = sServer;
		WebParam.client_account = sAcc;
		WebParam.client_password = sPwd;
		WebParam.client_clientip = "127.0.0.1";
		Debug.Log(string.Concat(new string[]
		{
			"Editor:",
			WebParam.client_ips,
			":",
			WebParam.client_port.ToString(),
			",",
			WebParam.client_serverid,
			",",
			WebParam.client_account,
			",",
			WebParam.client_password
		}));
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x001932A0 File Offset: 0x001914A0
	public void ReportStartgame()
	{
		bool flag = WebParam.client_islogover != "1";
		if (flag)
		{
			base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
			{
				"/qqapi/loadexe_area.ashx?uid=",
				WebParam.client_account,
				"&cid=",
				WebParam.client_cid,
				"&str=start"
			}))));
		}
		bool flag2 = WebParam.client_company == "qq" && !base.IsInvoking("UpdateOpenKey");
		if (flag2)
		{
			base.InvokeRepeating("UpdateOpenKey", 600f, 600f);
		}
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x00193348 File Offset: 0x00191548
	private void UpdateOpenKey()
	{
		base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL("/interface/qq_renew_openkey.aspx?uid=" + WebParam.client_account + "&d=" + UnityEngine.Random.Range(0, 100).ToString())));
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x0019338C File Offset: 0x0019158C
	private void OpenPlugins()
	{
		bool flag = !(WebParam.client_company == "qq");
		if (flag)
		{
		}
	}

	// Token: 0x040026D0 RID: 9936
	public static bool isExeLogin = false;

	// Token: 0x040026D1 RID: 9937
	public static string client_cache = "0";

	// Token: 0x040026D2 RID: 9938
	public static string client_loadtype = "1";

	// Token: 0x040026D3 RID: 9939
	public static string client_cid = string.Empty;

	// Token: 0x040026D4 RID: 9940
	public static string client_account = string.Empty;

	// Token: 0x040026D5 RID: 9941
	public static string client_password;

	// Token: 0x040026D6 RID: 9942
	public static string client_proxyip;

	// Token: 0x040026D7 RID: 9943
	public static string client_proxyport;

	// Token: 0x040026D8 RID: 9944
	public static string client_ips;

	// Token: 0x040026D9 RID: 9945
	public static int client_port;

	// Token: 0x040026DA RID: 9946
	public static string client_chongzhiurl;

	// Token: 0x040026DB RID: 9947
	public static string client_opentime;

	// Token: 0x040026DC RID: 9948
	public static string client_serverid;

	// Token: 0x040026DD RID: 9949
	public static string client_areaID;

	// Token: 0x040026DE RID: 9950
	public static string client_gameversion = "1.0.0";

	// Token: 0x040026DF RID: 9951
	public static string client_bbsurl;

	// Token: 0x040026E0 RID: 9952
	public static string client_shoucangurl;

	// Token: 0x040026E1 RID: 9953
	public static string client_newcardsurl;

	// Token: 0x040026E2 RID: 9954
	public static string client_fcmurl;

	// Token: 0x040026E3 RID: 9955
	public static string client_kefuurl;

	// Token: 0x040026E4 RID: 9956
	public static string client_pf;

	// Token: 0x040026E5 RID: 9957
	public static string client_appid;

	// Token: 0x040026E6 RID: 9958
	public static bool client_resurl_force_local = true;

	// Token: 0x040026E7 RID: 9959
	public static string client_resurl = "http://qscs.resources.3737.com";

	// Token: 0x040026E8 RID: 9960
	public static string client_resurl2 = "http://qscs.resources.3737.com";

	// Token: 0x040026E9 RID: 9961
	public static string client_serverweburl = "192.168.0.8";

	// Token: 0x040026EA RID: 9962
	public static string client_clientip = "127.0.0.1";

	// Token: 0x040026EB RID: 9963
	public static string client_language = "baxi";

	// Token: 0x040026EC RID: 9964
	public static int version_language = 14;

	// Token: 0x040026ED RID: 9965
	public static string client_company;

	// Token: 0x040026EE RID: 9966
	public static string client_islogover;

	// Token: 0x040026EF RID: 9967
	public static string LoginUI_web_login = "0";

	// Token: 0x040026F0 RID: 9968
	public static string clientVersion = "1.0.20170405.01";

	// Token: 0x040026F1 RID: 9969
	public static int startgametimes = Environment.TickCount;

	// Token: 0x040026F2 RID: 9970
	public static string client_version = "211";

	// Token: 0x040026F3 RID: 9971
	public static int client_jointeststate = 0;
}

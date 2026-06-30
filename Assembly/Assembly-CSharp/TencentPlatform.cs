using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using LitJson;
using UnityEngine;

// Token: 0x02000471 RID: 1137
public class TencentPlatform : MonoBehaviour
{
	// Token: 0x06002C4E RID: 11342
	[DllImport("Client")]
	public static extern int RegPCallbacks(TencentPlatform.OnConnectSucc succCall, TencentPlatform.OnConnectFailed failedCall, TencentPlatform.OnConnectionDestroyed desTroyDes, TencentPlatform.OnReceiveMsg resMsg);

	// Token: 0x06002C4F RID: 11343
	[DllImport("Client")]
	public static extern int Init(StringBuilder sb);

	// Token: 0x06002C50 RID: 11344
	[DllImport("Client")]
	public static extern int SendMsg(byte[] pSendBuf, short lBufLen, short CommandID);

	// Token: 0x06002C51 RID: 11345 RVA: 0x00185C68 File Offset: 0x00183E68
	public static void InitComponent()
	{
		TencentPlatform.OnConnectSucc succCall;
		if ((succCall = TencentPlatform.O_cache._State_0_OnConnectSuccFunc) == null)
		{
			succCall = (TencentPlatform.O_cache._State_0_OnConnectSuccFunc = new TencentPlatform.OnConnectSucc(TencentPlatform.OnConnectSuccFunc));
		}
		TencentPlatform.OnConnectFailed failedCall;
		if ((failedCall = TencentPlatform.O_cache._State_1_OnConnectFailedFunc) == null)
		{
			failedCall = (TencentPlatform.O_cache._State_1_OnConnectFailedFunc = new TencentPlatform.OnConnectFailed(TencentPlatform.OnConnectFailedFunc));
		}
		TencentPlatform.OnConnectionDestroyed desTroyDes;
		if ((desTroyDes = TencentPlatform.O_cache._State_2_ConnectionDestroyedFunc) == null)
		{
			desTroyDes = (TencentPlatform.O_cache._State_2_ConnectionDestroyedFunc = new TencentPlatform.OnConnectionDestroyed(TencentPlatform.ConnectionDestroyedFunc));
		}
		TencentPlatform.OnReceiveMsg resMsg;
		if ((resMsg = TencentPlatform.O_cache._State_3_OnReceiveMsgFunc) == null)
		{
			resMsg = (TencentPlatform.O_cache._State_3_OnReceiveMsgFunc = new TencentPlatform.OnReceiveMsg(TencentPlatform.OnReceiveMsgFunc));
		}
		TencentPlatform.RegPCallbacks(succCall, failedCall, desTroyDes, resMsg);
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x00185CE8 File Offset: 0x00183EE8
	private static void OnReceiveMsgFunc(short nCommandID, short nDataLen, byte[] abyData)
	{
		if (nCommandID == 3)
		{
			int num = BitConverter.ToInt32(abyData, 0);
		}
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x00185D0E File Offset: 0x00183F0E
	private static void OnConnectSuccFunc()
	{
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x00185D11 File Offset: 0x00183F11
	private static void OnConnectFailedFunc(uint dwErrorCode)
	{
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x00185D14 File Offset: 0x00183F14
	private static void ConnectionDestroyedFunc()
	{
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x00185D17 File Offset: 0x00183F17
	public static void CallQQGameExit()
	{
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x00185D1C File Offset: 0x00183F1C
	public static byte[] CallQQGameOpenUrl(int width, int height, string title, string url)
	{
		int num = 0;
		int num2 = Encoding.UTF8.GetBytes(title).Length;
		int num3 = Encoding.UTF8.GetBytes(url).Length;
		int num4 = 10 + num2 + 2 + num3;
		byte[] array = new byte[8 + num4];
		BitConverter.GetBytes(3).CopyTo(array, num);
		num += 4;
		BitConverter.GetBytes(num4).CopyTo(array, num);
		num += 4;
		BitConverter.GetBytes(width).CopyTo(array, num);
		num += 4;
		BitConverter.GetBytes(height).CopyTo(array, num);
		num += 4;
		BitConverter.GetBytes((short)num2).CopyTo(array, num);
		num += 2;
		Encoding.UTF8.GetBytes(title).CopyTo(array, num);
		num += num2;
		BitConverter.GetBytes((short)num3).CopyTo(array, num);
		num += 2;
		Encoding.UTF8.GetBytes(url).CopyTo(array, num);
		num += num3;
		return array;
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x00185E07 File Offset: 0x00184007
	public void OpenQQUrl(string operate, string param)
	{
		this.OpenQQUrl(operate, param, string.Empty);
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x00185E18 File Offset: 0x00184018
	public void OpenQQUrl(string operate, string param, string Reason)
	{
		string text = string.Empty;
		bool flag = WebParam.client_pf == "qzone";
		if (flag)
		{
			string text2 = Fun.Md5Sum(WebParam.client_cid + "@PAY@" + WebParam.client_account);
			bool flag2 = operate == "buygold";
			if (flag2)
			{
				text = SysConfig.getWebURL(string.Concat(new string[]
				{
					"/interface/qq_buy_goods.aspx?isqzone=",
					this.isqzone.ToString(),
					"&pf=",
					WebParam.client_pf,
					"&cid=",
					WebParam.client_cid,
					"&uid=",
					WebParam.client_account,
					"&sign=",
					text2,
					"&payvalue=",
					param,
					"&operate=",
					operate,
					"&Reason=",
					Reason,
					"&t=",
					DateTime.Now.ToString("hhmmss")
				}));
				this.Openbuy(text);
				return;
			}
			bool flag3 = operate == "hzvip";
			if (flag3)
			{
				text = SysConfig.getWebURL(string.Concat(new string[]
				{
					"/interface/qq_buy_goods.aspx?isqzone=",
					this.isqzone.ToString(),
					"&pf=",
					WebParam.client_pf,
					"&cid=",
					WebParam.client_cid,
					"&uid=",
					WebParam.client_account,
					"&sign=",
					text2,
					"&payvalue=",
					param,
					"&operate=",
					operate,
					"&t=",
					DateTime.Now.ToString("hhmmss")
				}));
			}
		}
		else
		{
			string text3 = Fun.Md5Sum(WebParam.client_cid + "@PAY@" + WebParam.client_account);
			bool flag4 = operate == "buygold";
			if (flag4)
			{
				text = SysConfig.getWebURL(string.Concat(new string[]
				{
					"/interface/qq_buy_goods.aspx?isqzone=",
					this.isqzone.ToString(),
					"&pf=",
					WebParam.client_pf,
					"&cid=",
					WebParam.client_cid,
					"&uid=",
					WebParam.client_account,
					"&sign=",
					text3,
					"&payvalue=",
					param,
					"&operate=",
					operate,
					"&Reason=",
					Reason,
					"&t=",
					DateTime.Now.ToString("hhmmss")
				}));
			}
			else
			{
				bool flag5 = operate == "bluevip";
				if (flag5)
				{
					text = SysConfig.getWebURL(string.Concat(new string[]
					{
						"/interface/qq_buy_goods.aspx?isqzone=",
						this.isqzone.ToString(),
						"&pf=",
						WebParam.client_pf,
						"&cid=",
						WebParam.client_cid,
						"&uid=",
						WebParam.client_account,
						"&sign=",
						text3,
						"&payvalue=0&operate=",
						operate,
						"&t=",
						DateTime.Now.ToString("hhmmss")
					}));
				}
			}
		}
		bool flag6 = operate == "kfopen";
		if (flag6)
		{
			text = this.GetOpenKeFuPageUrl();
			this.Openbuy(text);
		}
		else
		{
			bool flag7 = operate == "fcmopen";
			if (flag7)
			{
				text = SysConfig.getWebURL("/reg/fcm.aspx?uid=" + WebParam.client_cid + "&t=" + DateTime.Now.ToString("hhmmss"));
				this.Openbuy(text);
			}
			else
			{
				bool flag8 = text.Length > 0;
				if (flag8)
				{
					UnityEngine.Debug.Log(text);
					base.StartCoroutine(this.GET(operate, text));
				}
				else
				{
					this.msg(Language.GetLang(110001, new object[0]));
				}
			}
		}
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x0018622E File Offset: 0x0018442E
	private IEnumerator GET(string types, string url)
	{
		WWW www = new WWW(url);
		yield return www;
		bool flag = www.error != null;
		if (flag)
		{
			this.msg(Language.GetLang(110002, new object[0]));
			yield break;
		}
		string sFlag = www.text;
		bool flag2 = !sFlag.Contains("ret");
		if (flag2)
		{
			yield break;
		}
		JsonData jsonData = JsonMapper.ToObject(sFlag);
		bool flag3 = (string)jsonData["ret"] == "0";
		if (flag3)
		{
			sFlag = Convert.ToString(jsonData["msg"]);
			bool flag4 = sFlag.Contains(WebParam.client_appid);
			if (flag4)
			{
				bool flag5 = types == "hzvip";
				if (flag5)
				{
					Debuger.OpenURL(sFlag);
				}
				else
				{
					this.Openbuy(sFlag);
				}
			}
			else
			{
				this.msg(Language.GetLang(110003, new object[0]));
			}
		}
		else
		{
			this.msg(Convert.ToString(jsonData["msg"]));
		}
		yield break;
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x0018624C File Offset: 0x0018444C
	private void msg(string str)
	{
		UnityEngine.Debug.Log(str);
		TipManager.GetInstance().ShowComTipMsg("提示", str, "确定", string.Empty, string.Empty, null, null, null, null);
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x00186288 File Offset: 0x00184488
	private void Openbuy(string webdata)
	{
		this.buyWindow = ImportFromDLL.FindWindow(null, "CSJJBuyBox");
		bool flag = this.buyWindow == IntPtr.Zero;
		if (flag)
		{
			this.openpayexe(webdata, 600, 800, 0, -30);
			this.buyWindow = ImportFromDLL.FindWindow(null, "CSJJBuyBox");
		}
		else
		{
			base.CancelInvoke("Show");
			ImportFromDLL.ShowWindow(this.buyWindow, 0);
			ImportFromDLL.COPYDATASTRUCT copydatastruct = new ImportFromDLL.COPYDATASTRUCT
			{
				dwData = 0,
				cbData = Encoding.Default.GetBytes(webdata).Length + 1,
				lpData = webdata
			};
			ImportFromDLL.SendMessage(this.buyWindow, 74, IntPtr.Zero, ref copydatastruct);
			bool fullScreen = Screen.fullScreen;
			if (fullScreen)
			{
			}
			base.Invoke("Show", 0.5f);
		}
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x0018635E File Offset: 0x0018455E
	private void Show()
	{
		this.buyWindow = ImportFromDLL.FindWindow(null, "CSJJBuyBox");
		ImportFromDLL.ShowWindow(this.buyWindow, 5);
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x00186380 File Offset: 0x00184580
	private void openpayexe(string cmdstr, int height = 600, int width = 800, int h2 = 0, int w2 = -30)
	{
		bool flag = WebParam.client_company == "qq";
		if (flag)
		{
			TencentPlatform.currentProcess = Process.Start(Application.dataPath + "/../CSJJBuyBox.exe", cmdstr.ToString());
		}
		else
		{
			TencentPlatform.currentProcess = Process.Start(Application.dataPath + "/CSJJBuyBox.exe", cmdstr.ToString());
		}
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x001863E8 File Offset: 0x001845E8
	public string GetOpenKeFuPageUrl()
	{
		string stringToEscape = string.Empty;
		string text = string.Empty;
		string text2 = string.Empty;
		bool flag = PlayerDataManager.getInstance().HUserData != null;
		if (flag)
		{
			text = PlayerDataManager.getInstance().HUserData.m_sName;
			text2 = PlayerDataManager.getInstance().HUserData.m_nCid.ToString();
		}
		bool flag2 = string.IsNullOrEmpty(text);
		if (flag2)
		{
			text = PlayerDataManager.getInstance().playerCommon.m_sName;
			text2 = PlayerDataManager.getInstance().playerCommon.m_nCid.ToString();
		}
		string text3 = WebParam.client_serverid;
		text3 = text3.ToLower().Replace("s", string.Empty);
		string text4 = Convert.ToString((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000L);
		string client_account = WebParam.client_account;
		stringToEscape = SysConfig.getWebURL(string.Concat(new string[]
		{
			"/qqapi/kefu.ashx?uname=",
			text,
			"&sid=",
			text3,
			"&time=",
			text4,
			"&uid=",
			text2,
			"&openid=",
			client_account,
			string.Empty
		}));
		return Uri.EscapeUriString(stringToEscape);
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x00186530 File Offset: 0x00184730
	private void OnDestroy()
	{
		try
		{
			ImportFromDLL.KillProcess("CSJJBuyBox");
			bool flag = TencentPlatform.currentProcess != null;
			if (flag)
			{
				TencentPlatform.currentProcess.CloseMainWindow();
				TencentPlatform.currentProcess.Dispose();
			}
		}
		catch
		{
		}
	}

	// Token: 0x040025B6 RID: 9654
	private const string exeTitle = "CSJJBuyBox";

	// Token: 0x040025B7 RID: 9655
	public const int CS_GAME_EXIT = 1;

	// Token: 0x040025B8 RID: 9656
	public const int CS_NAVIGATE_URL = 3;

	// Token: 0x040025B9 RID: 9657
	public const int SC_BOSSKEY = 3;

	// Token: 0x040025BA RID: 9658
	public const int SC_WND_BRINGTOP = 4;

	// Token: 0x040025BB RID: 9659
	private IntPtr buyWindow;

	// Token: 0x040025BC RID: 9660
	private static Process currentProcess;

	// Token: 0x040025BD RID: 9661
	private int isqzone;

	// Token: 0x02000683 RID: 1667
	// (Invoke) Token: 0x06003946 RID: 14662
	public delegate void OnReceiveMsg(short nCommandID, short nDataLen, byte[] abyData);

	// Token: 0x02000684 RID: 1668
	// (Invoke) Token: 0x0600394A RID: 14666
	public delegate void OnConnectSucc();

	// Token: 0x02000685 RID: 1669
	// (Invoke) Token: 0x0600394E RID: 14670
	public delegate void OnConnectFailed(uint dwErrorCode);

	// Token: 0x02000686 RID: 1670
	// (Invoke) Token: 0x06003952 RID: 14674
	public delegate void OnConnectionDestroyed();

	// Token: 0x02000687 RID: 1671
	[CompilerGenerated]
	private static class O_cache
	{
		// Token: 0x040030DA RID: 12506
		public static TencentPlatform.OnConnectSucc _State_0_OnConnectSuccFunc;

		// Token: 0x040030DB RID: 12507
		public static TencentPlatform.OnConnectFailed _State_1_OnConnectFailedFunc;

		// Token: 0x040030DC RID: 12508
		public static TencentPlatform.OnConnectionDestroyed _State_2_ConnectionDestroyedFunc;

		// Token: 0x040030DD RID: 12509
		public static TencentPlatform.OnReceiveMsg _State_3_OnReceiveMsgFunc;
	}
}

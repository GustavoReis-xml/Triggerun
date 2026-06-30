using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class QQDllMsg : MonoBehaviour
{
	// Token: 0x06002024 RID: 8228
	[DllImport("ProcDll")]
	private static extern int MyADD(int x, int y);

	// Token: 0x06002025 RID: 8229
	[DllImport("ProcDll")]
	public static extern void InitDllReCall(QQDllMsg.OnConnectSucc succCall, QQDllMsg.OnConnectFailed failedCall, QQDllMsg.OnConnectionDestroyed desTroyDes, QQDllMsg.OnReceiveMsg resMsg);

	// Token: 0x06002026 RID: 8230
	[DllImport("ProcDll")]
	public static extern int InitDll(string name);

	// Token: 0x06002027 RID: 8231
	[DllImport("ProcDll")]
	public static extern void SendMsg(int nCommandID, byte[] pSendBuf, long lBufLen);

	// Token: 0x06002028 RID: 8232 RVA: 0x00128390 File Offset: 0x00126590
	public static void InitCom()
	{
		bool flag = !string.IsNullOrEmpty(QQDllMsg.DTPROCPARA);
		if (flag)
		{
			QQDllMsg.OnConnectSucc succCall;
			if ((succCall = QQDllMsg.O_cache._State_0_OnConnectSuccFunc) == null)
			{
				succCall = (QQDllMsg.O_cache._State_0_OnConnectSuccFunc = new QQDllMsg.OnConnectSucc(QQDllMsg.OnConnectSuccFunc));
			}
			QQDllMsg.OnConnectFailed failedCall;
			if ((failedCall = QQDllMsg.O_cache._State_1_OnConnectFailedFunc) == null)
			{
				failedCall = (QQDllMsg.O_cache._State_1_OnConnectFailedFunc = new QQDllMsg.OnConnectFailed(QQDllMsg.OnConnectFailedFunc));
			}
			QQDllMsg.OnConnectionDestroyed desTroyDes;
			if ((desTroyDes = QQDllMsg.O_cache._State_2_ConnectionDestroyedFunc) == null)
			{
				desTroyDes = (QQDllMsg.O_cache._State_2_ConnectionDestroyedFunc = new QQDllMsg.OnConnectionDestroyed(QQDllMsg.ConnectionDestroyedFunc));
			}
			QQDllMsg.OnReceiveMsg resMsg;
			if ((resMsg = QQDllMsg.O_cache._State_3_OnReceiveMsgFunc) == null)
			{
				resMsg = (QQDllMsg.O_cache._State_3_OnReceiveMsgFunc = new QQDllMsg.OnReceiveMsg(QQDllMsg.OnReceiveMsgFunc));
			}
			QQDllMsg.InitDllReCall(succCall, failedCall, desTroyDes, resMsg);
			QQDllMsg.InitDll(QQDllMsg.DTPROCPARA);
		}
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x0012842E File Offset: 0x0012662E
	private static void ConnectionDestroyedFunc()
	{
		Debug.Log("ConnectionDestroyedFunc");
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0012843C File Offset: 0x0012663C
	private static void OnConnectFailedFunc(int dwErrorCode)
	{
		Debug.Log("OnConnectFailedFunc");
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x0012844A File Offset: 0x0012664A
	private static void OnConnectSuccFunc()
	{
		Debug.Log("OnConnectSuccFunc");
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x00128458 File Offset: 0x00126658
	private static void OnReceiveMsgFunc(long nCommandID, long nDataLen, byte[] abyData)
	{
		Debug.Log("OnReceiveMsgFunc");
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x00128466 File Offset: 0x00126666
	private void Start()
	{
		QQDllMsg.InitCom();
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x0012846F File Offset: 0x0012666F
	private void Update()
	{
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x00128474 File Offset: 0x00126674
	public static void SendMsgQQ()
	{
		byte[] bytes = Encoding.UTF8.GetBytes("aaaaaaa");
		bool flag = !string.IsNullOrEmpty(QQDllMsg.DTPROCPARA);
		if (flag)
		{
			QQDllMsg.SendMsg(1, bytes, (long)bytes.Length);
		}
	}

	// Token: 0x04001D71 RID: 7537
	public static string DTPROCPARA = string.Empty;

	// Token: 0x020005FE RID: 1534
	// (Invoke) Token: 0x060036B3 RID: 14003
	public delegate void OnConnectFailed(int dwErrorCode);

	// Token: 0x020005FF RID: 1535
	// (Invoke) Token: 0x060036B7 RID: 14007
	public delegate void OnConnectionDestroyed();

	// Token: 0x02000600 RID: 1536
	// (Invoke) Token: 0x060036BB RID: 14011
	public delegate void OnConnectSucc();

	// Token: 0x02000601 RID: 1537
	// (Invoke) Token: 0x060036BF RID: 14015
	public delegate void OnReceiveMsg(long nCommandID, long nDataLen, byte[] abyData);

	// Token: 0x02000602 RID: 1538
	[CompilerGenerated]
	private static class O_cache
	{
		// Token: 0x04002F04 RID: 12036
		public static QQDllMsg.OnConnectSucc _State_0_OnConnectSuccFunc;

		// Token: 0x04002F05 RID: 12037
		public static QQDllMsg.OnConnectFailed _State_1_OnConnectFailedFunc;

		// Token: 0x04002F06 RID: 12038
		public static QQDllMsg.OnConnectionDestroyed _State_2_ConnectionDestroyedFunc;

		// Token: 0x04002F07 RID: 12039
		public static QQDllMsg.OnReceiveMsg _State_3_OnReceiveMsgFunc;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class BaseSocket : NetObj
{
	// Token: 0x060001A4 RID: 420 RVA: 0x00008FDC File Offset: 0x000071DC
	private void LogCallback(string condition, string stackTrace, LogType type)
	{
		bool flag = string.IsNullOrEmpty(condition);
		if (!flag)
		{
			bool flag2 = condition.StartsWith("LOG:") || condition.StartsWith("tgw_l7_forward") || condition.StartsWith("NGOL") || condition.Contains("[RECEBEU DO SERVIDOR]") || condition.Contains("[ENVIOU PARA SERVIDOR]");
			if (!flag2)
			{
				bool flag3 = this.clientSocket != null && this.clientSocket.Connected && this.m_LoginSt == BaseSocket.LOGIN_ST.LS_LINK;
				if (flag3)
				{
					try
					{
						string text = string.Format("LOG:[{0}] {1}", type.ToString(), condition);
						bool flag4 = type == LogType.Exception || type == LogType.Error;
						if (flag4)
						{
							text = text + "\nStack: " + stackTrace;
						}
						this.SendMessageStr(text);
					}
					catch (Exception)
					{
					}
				}
			}
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x000090C4 File Offset: 0x000072C4
	public BaseSocket()
	{
		this.InitDBCS();
		this.m_LoginSt = BaseSocket.LOGIN_ST.LS_UNKOWN;
		this.send_mas_num = 0;
		this.InitInt64();
		bool flag = BaseSocket.s_activeSocket != null;
		if (flag)
		{
			Application.logMessageReceived -= BaseSocket.s_activeSocket.LogCallback;
		}
		BaseSocket.s_activeSocket = this;
		Application.logMessageReceived += this.LogCallback;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00009208 File Offset: 0x00007408
	public void InitDBCS()
	{
		bool flag = this.encoding == null;
		if (flag)
		{
			this.encoding = new DBCSEncoding();
		}
		this.encoding.GetDBCSEncoding("gb2312");
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00009244 File Offset: 0x00007444
	public string GetGBString(string sMsg)
	{
		bool flag = this.encoding != null;
		string result;
		if (flag)
		{
			byte[] bytes = this.encoding.GetBytes(sMsg);
			result = this.encoding.GetString(bytes);
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00009288 File Offset: 0x00007488
	public NetObj GetNetObj(int nNetID)
	{
		string key = nNetID.ToString();
		bool flag = !this.NetObjTable.ContainsKey(key);
		NetObj result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.NetObjTable[key];
		}
		return result;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000092C6 File Offset: 0x000074C6
	public void InitClientScoketInfo(string gn, string ut, string un, string up, string em)
	{
		this.m_bIsChgScn = true;
		this.groupname = gn;
		this.usertype = ut;
		this.username = un;
		this.userpass = up;
		this.ExtMsg = em;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000092F8 File Offset: 0x000074F8
	public void clear()
	{
		bool flag = this.clientSocket != null;
		if (flag)
		{
			this.clientSocket.Close();
		}
		this.m_LoginSt = BaseSocket.LOGIN_ST.LS_UNKOWN;
		this.send_mas_num = 0;
		foreach (KeyValuePair<string, NetObj> keyValuePair in this.NetObjTable)
		{
			try
			{
				keyValuePair.Value.setCs(null);
				keyValuePair.Value.OnNetDestroy();
			}
			catch (Exception ex)
			{
				Fun.LogError("socket clear error.e=" + ex.ToString(), null);
			}
		}
		this.NetObjTable.Clear();
		this.m_nAllIndex = 0;
		this.m_bNew = false;
		this.m_nCmdLen = 0;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000093DC File Offset: 0x000075DC
	public void setDefobj(UserSocket obj)
	{
		this.defcmdobj = obj;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000093E8 File Offset: 0x000075E8
	public object getDefobj()
	{
		return this.defcmdobj;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00009400 File Offset: 0x00007600
	public static bool IsValidAddress(IPAddress address)
	{
		return address.AddressFamily == AddressFamily.InterNetwork && !address.Equals(IPAddress.Loopback) && !address.Equals(IPAddress.None) && !address.Equals(IPAddress.Any);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00009446 File Offset: 0x00007646
	public void reConnet(string shost, int nport)
	{
		this.Closed();
		this.connectServer(shost, nport);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000945C File Offset: 0x0000765C
	private void DelayConnect()
	{
		this.defcmdobj.OnSocketError(6, "没有连接到服务器,准备重连.");
		this.m_nReConnectCount++;
		bool flag = this.m_nReConnectCount >= this.m_nMaxReConnectCount;
		if (flag)
		{
			this.m_nReConnectCount = 0;
			this.defcmdobj.OnSocketError(10060, "连接超时.");
		}
		else
		{
			System.Timers.Timer timer = new System.Timers.Timer(1000.0);
			timer.Elapsed += this.ReconnectServer;
			timer.AutoReset = false;
			timer.Enabled = true;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000094F1 File Offset: 0x000076F1
	public void ReconnectServer(object source, ElapsedEventArgs e)
	{
		this.defcmdobj.ReconnectServer();
		this.connectServer(this.HOST, this.PORT);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00009514 File Offset: 0x00007714
	public void connectServer(string shost, int nport)
	{
		this.HOST = shost;
		this.PORT = nport;
		this.send_mas_num = 0;
		this.clear();
		bool flag = !string.IsNullOrEmpty(this.defcmdobj.m_sip_proxy);
		if (flag)
		{
			shost = this.defcmdobj.m_sip_proxy;
			nport = this.defcmdobj.m_nprot_proxy;
		}
		this.mainThreadId = 0;
		IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(shost), nport);
		this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		this.clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
		this.clientSocket.Blocking = true;
		this.clientSocket.NoDelay = true;
		try
		{
			this.clientSocket.Connect(remoteEP);
		}
		catch (SocketException ex)
		{
			bool flag2 = ex.ErrorCode == 10060;
			if (flag2)
			{
				this.DelayConnect();
				return;
			}
			this.defcmdobj.OnSocketError(ex.ErrorCode, "connectServer:" + ex.Message + "#" + ex.StackTrace);
			this.DelayConnect();
			return;
		}
		this.connectCallback(null);
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000963C File Offset: 0x0000783C
	public void InitInt64()
	{
		BaseSocket.s_chr64toint = new int[256];
		BaseSocket.s_int2chr64 = new char[64];
		for (int i = 0; i < 64; i++)
		{
			bool flag = i < 10;
			char c;
			if (flag)
			{
				c = (char)(i + 48);
			}
			else
			{
				bool flag2 = i < 36;
				if (flag2)
				{
					c = (char)(i + 65 - 10);
				}
				else
				{
					c = (char)(i + 95 - 36);
				}
			}
			BaseSocket.s_chr64toint[(int)c] = i;
			BaseSocket.s_int2chr64[i] = c;
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x000096C0 File Offset: 0x000078C0
	private void connectCallback(IAsyncResult asyncConnect)
	{
		this.m_nReConnectCount = 0;
		this.m_bIsChgScn = false;
		this.m_LoginSt = BaseSocket.LOGIN_ST.LS_CONNECT;
		bool flag = this.thread != null;
		if (flag)
		{
		}
		this.thread = new Thread(new ThreadStart(this.ReceiveSorket));
		this.mainThreadId = this.thread.ManagedThreadId;
		this.thread.Priority = System.Threading.ThreadPriority.Highest;
		this.thread.IsBackground = true;
		bool flag2 = !string.IsNullOrEmpty(this.defcmdobj.m_sip_proxy);
		if (flag2)
		{
			this.SendMessageStr(string.Format("tgw_l7_forward\r\nHost:{0}:{1}\r\n:{2}:{3}\r\n\r\n", new object[]
			{
				this.defcmdobj.m_sip_proxy,
				this.defcmdobj.m_nprot_proxy,
				this.HOST,
				this.PORT
			}));
		}
		this.thread.Start();
		this.defcmdobj.linkOk();
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000097B4 File Offset: 0x000079B4
	private void ReceiveSorket()
	{
		int num = this.mainThreadId;
		while (this.clientSocket != null)
		{
			bool flag = num != this.mainThreadId || this.mainThreadId == 0;
			if (flag)
			{
				break;
			}
			try
			{
				int num2 = this.clientSocket.Receive(this.m_szPool);
				bool flag2 = num2 <= 0;
				if (flag2)
				{
					this.defcmdobj.OnSocketError(1, "Receive bytes 0.");
					break;
				}
				this.AddPool(this.m_szPool, num2);
			}
			catch (SocketException ex)
			{
				this.defcmdobj.OnSocketError(ex.ErrorCode, "ReceiveSorket:" + ex.Message + "#" + ex.StackTrace);
				break;
			}
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000988C File Offset: 0x00007A8C
	private void AddPool(byte[] bytes, int index)
	{
		object szPackagPool = this.m_szPackagPool;
		object obj = szPackagPool;
		lock (obj)
		{
			Array.Copy(bytes, 0, this.m_szPackagPool, this.m_nAllIndex, index);
			this.m_nAllIndex += index;
			this.m_bNew = true;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000098F0 File Offset: 0x00007AF0
	private void SocketErr(string sMsg)
	{
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000098F3 File Offset: 0x00007AF3
	public void Socketlog(string sMsg)
	{
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000098F8 File Offset: 0x00007AF8
	public void OnFrame()
	{
		bool flag = !this.m_bNew;
		if (!flag)
		{
			object szPackagPool = this.m_szPackagPool;
			object obj = szPackagPool;
			string @string;
			lock (obj)
			{
				bool flag2 = this.m_nCmdLen > 0;
				if (flag2)
				{
					Array.Copy(this.m_szCmd, 0, this.m_szPackag, 0, this.m_nCmdLen);
				}
				Array.Copy(this.m_szPackagPool, 0, this.m_szPackag, this.m_nCmdLen, this.m_nAllIndex);
				this.m_nPackSize = this.m_nAllIndex + this.m_nCmdLen;
				@string = this.encoding.GetString(this.m_szPackag, 0, this.m_nPackSize);
				this.m_nAllIndex = 0;
				this.m_nCmdLen = 0;
				this.m_bNew = false;
			}
			this.parserController(@string);
		}
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000099D8 File Offset: 0x00007BD8
	public bool parserController(string scmd)
	{
		int num = scmd.Length;
		switch (this.m_LoginSt)
		{
		case BaseSocket.LOGIN_ST.LS_UNKOWN:
			return false;
		case BaseSocket.LOGIN_ST.LS_CONNECT:
		{
			bool flag = scmd.Substring(0, 1).Equals("<");
			if (flag)
			{
				return true;
			}
			bool flag2 = num > 128 || num < 9;
			if (flag2)
			{
				Fun.LogError(string.Format("socket Len err :{0}", scmd), null);
				return false;
			}
			bool flag3 = scmd.Substring(0).Equals("@");
			if (flag3)
			{
				string[] wordS = this.GetWordS(scmd, ":", false);
				this.AgvPing = this.GetFromInt64(wordS[1]);
			}
			else
			{
				bool flag4 = !scmd.Substring(0, 3).Equals(BaseSocket.GETSOCKETVER1);
				if (flag4)
				{
					Fun.LogError(string.Format("socket ver1 err:", scmd), null);
					return false;
				}
				bool flag5 = !scmd.Substring(4, 4).Equals(BaseSocket.GETSOCKETVER2);
				if (flag5)
				{
					Fun.LogError(string.Format("socket ver2 err:", scmd), null);
					return false;
				}
				bool bzip = this.m_bzip;
				string str;
				if (bzip)
				{
					str = string.Format("{0}B{1}", BaseSocket.SRESSOCKETVERZIP, scmd.Substring(9, scmd.Length - 9));
				}
				else
				{
					str = string.Format("{0}B{1}", BaseSocket.SRESSOCKETVER, scmd.Substring(9, scmd.Length - 9));
				}
				this.SendMessageStr(str);
				str = string.Format("NGOL'{0}','{1}','{2}','{3}',{4}", new object[]
				{
					this.groupname,
					this.usertype,
					this.username,
					this.userpass,
					this.ExtMsg
				});
				this.SendMessageStr(str);
				this.m_LoginSt = BaseSocket.LOGIN_ST.LS_LINK;
			}
			break;
		}
		case BaseSocket.LOGIN_ST.LS_LINK:
		{
			string[] array = scmd.Split(new char[]
			{
				'\n'
			});
			num = array.Length;
			scmd = scmd.Trim(new char[1]);
			char[] array2 = scmd.Substring(scmd.Length - 1, 1).ToCharArray();
			bool flag6 = array2[0] != '\n';
			if (flag6)
			{
				num--;
				int byteCount = this.encoding.GetByteCount(array[num]);
				bool flag7 = this.m_szCmd.Length < byteCount;
				if (flag7)
				{
					this.m_szCmd = new byte[byteCount];
				}
				bool flag8 = this.m_nPackSize >= byteCount;
				if (flag8)
				{
					Array.Copy(this.m_szPackag, this.m_nPackSize - byteCount, this.m_szCmd, 0, byteCount);
					this.m_nCmdLen = byteCount;
				}
				else
				{
					Fun.LogError(string.Format("cmd error:varcmd = {0},m_nPackSize = {1},nCmdLen = {2},sCmd = {3}", new object[]
					{
						array[num],
						this.m_nPackSize,
						byteCount,
						scmd
					}), null);
				}
			}
			for (int i = 0; i < num; i++)
			{
				bool flag9 = !(array[i] == string.Empty);
				if (flag9)
				{
					bool flag10 = this.clientSocket == null;
					if (flag10)
					{
						break;
					}
					bool flag11 = this.OnGameLine(array[i]);
					if (flag11)
					{
						Fun.LogError(string.Format("net err cmd={0}\n i={1},len={2}", array[i], i, num), null);
					}
				}
			}
			return true;
		}
		}
		return true;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00009D48 File Offset: 0x00007F48
	private bool OnGameLine(string scmd)
	{
		char[] array = scmd.ToCharArray(0, 1);
		Debug.Log("\ud83d\udce5 [RECEBEU DO SERVIDOR]: " + scmd);
		NetObj netObj = null;
		char c = array[0];
		switch (c)
		{
		case 'c':
		{
			bool flag = this.NetObjTable == null;
			if (flag)
			{
				Fun.LogError("Net player Create no def group Obj", null);
				return false;
			}
			this.classstr = string.Empty;
			string text = scmd.Substring(1, scmd.Length - 1);
			string[] wordS = this.GetWordS(text, ":", false);
			this.classstr += wordS[0];
			text = wordS[1];
			wordS = this.GetWordS(text, ",", false);
			int fromInt = this.GetFromInt64(wordS[0]);
			text = wordS[1];
			Type type = Type.GetType(this.classstr);
			bool flag2 = type == null;
			if (flag2)
			{
				Fun.LogError(string.Format("Create player err :{0}", scmd), null);
			}
			else
			{
				bool flag3 = this.NetObjTable.ContainsKey(Convert.ToString(fromInt));
				if (flag3)
				{
					netObj = this.NetObjTable[Convert.ToString(fromInt)];
				}
				else
				{
					netObj = null;
				}
				bool flag4 = netObj != null;
				if (flag4)
				{
					netObj.OnDestroy();
					this.NetObjTable.Remove(Convert.ToString(fromInt));
				}
				netObj = (NetObj)Activator.CreateInstance(type);
				bool flag5 = netObj == null;
				if (flag5)
				{
					Fun.LogError(string.Format("create player err:{0}", scmd), null);
					return false;
				}
				netObj.setNetid(fromInt);
				netObj.setCs(this);
				netObj.setUobj(this.defcmdobj);
				netObj.OnCreate();
				this.NetObjTable.Add(Convert.ToString(fromInt), netObj);
				object[] array2 = this.CreateWithShortStr(text);
				bool flag6 = array2 == null;
				if (flag6)
				{
					return true;
				}
				bool flag7 = array2.Length < 1;
				if (flag7)
				{
					return true;
				}
				string funname = (string)array2[0];
				array2 = BaseSocket.DeleteArr(0, array2);
				this.getLog(array[0], netObj, funname, array2, text);
				this.CallObjFun(array[0], netObj, funname, array2);
				return false;
			}
			break;
		}
		case 'd':
		{
			string str = scmd.Substring(1, scmd.Length - 1);
			int fromInt2 = this.GetFromInt64(str);
			bool flag8 = this.NetObjTable.ContainsKey(Convert.ToString(fromInt2));
			if (flag8)
			{
				netObj = this.NetObjTable[Convert.ToString(fromInt2)];
			}
			else
			{
				netObj = null;
			}
			bool flag9 = netObj != null;
			if (flag9)
			{
				netObj.OnNetDestroy();
				this.NetObjTable.Remove(Convert.ToString(fromInt2));
			}
			break;
		}
		case 'e':
		{
			string text2 = scmd.Substring(1, scmd.Length - 1);
			string[] wordS2 = this.GetWordS(text2, ":", false);
			string text3 = wordS2[0];
			text2 = wordS2[1];
			int fromInt3 = this.GetFromInt64(text3);
			bool flag10 = text3.Length < 1;
			if (flag10)
			{
				netObj = this;
			}
			else
			{
				bool flag11 = this.NetObjTable.ContainsKey(Convert.ToString(fromInt3));
				if (flag11)
				{
					netObj = this.NetObjTable[Convert.ToString(fromInt3)];
				}
			}
			bool flag12 = netObj != null;
			if (flag12)
			{
				string[] wordS3 = this.GetWordS(text2, ",", false);
				text3 = wordS3[0];
				text2 = wordS3[1];
				object[] funAgv = this.CreateWithShortStr(text2);
				this.getLog(array[0], netObj, text3, funAgv, text2);
				this.CallObjFun(array[0], netObj, text3, funAgv);
				return false;
			}
			break;
		}
		default:
		{
			bool flag13 = c != '!';
			if (flag13)
			{
				bool flag14 = c != '.';
				if (flag14)
				{
					bool flag15 = c != '@';
					if (flag15)
					{
						bool flag16 = c == 'm';
						if (flag16)
						{
							bool flag17 = this.NetObjTable == null;
							if (flag17)
							{
								Fun.LogError("Net player Create no def group Obj", null);
								return false;
							}
							string str2 = scmd.Substring(1, scmd.Length - 1);
							string[] wordS4 = this.GetWordS(str2, ":", false);
							this.classstr = string.Empty;
							this.classstr += wordS4[0];
							str2 = wordS4[1];
							int fromInt4 = this.GetFromInt64(wordS4[1]);
							Type type2 = Type.GetType(this.classstr);
							bool flag18 = type2 == null;
							if (flag18)
							{
								Fun.LogError(string.Format("Create player err :{0}", scmd), null);
							}
							else
							{
								bool flag19 = this.NetObjTable.ContainsKey(Convert.ToString(fromInt4));
								if (flag19)
								{
									netObj = this.NetObjTable[Convert.ToString(fromInt4)];
								}
								else
								{
									netObj = null;
								}
								bool flag20 = netObj != null;
								if (flag20)
								{
									netObj.OnDestroy();
									this.NetObjTable.Remove(Convert.ToString(fromInt4));
								}
								netObj = (NetObj)Activator.CreateInstance(type2);
								bool flag21 = netObj != null;
								if (flag21)
								{
									this.mainObj = netObj;
									netObj.setNetid(fromInt4);
									netObj.setCs(this);
									netObj.setUobj(this.defcmdobj);
									netObj.OnCreate();
									this.NetObjTable.Add(Convert.ToString(fromInt4), netObj);
								}
								else
								{
									Fun.LogError(string.Format("create player err:{0}", scmd), null);
								}
							}
						}
					}
					else
					{
						int length = scmd.Length;
						string[] wordS5 = this.GetWordS(scmd, ":", false);
						this.SendMessageStr(wordS5[0] + "\n");
						this.AgvPing = this.GetFromInt64(wordS5[1]);
					}
				}
				else
				{
					bool flag22 = scmd == ".Server close client";
					if (flag22)
					{
						Fun.LogError(scmd + "cmd", null);
						this.clear();
					}
				}
			}
			break;
		}
		}
		return false;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000A318 File Offset: 0x00008518
	private static object[] DeleteArr(int num, object[] arr)
	{
		List<object> list = new List<object>();
		foreach (object item in arr)
		{
			list.Add(item);
		}
		list.RemoveAt(num);
		arr = new object[list.Count];
		for (int j = 0; j < list.Count; j++)
		{
			arr[j] = list[j];
		}
		return arr;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000A390 File Offset: 0x00008590
	public object[] CreateWithShortStr(string sAgv)
	{
		object[] result;
		try
		{
			bool flag = sAgv == string.Empty;
			if (flag)
			{
				result = null;
			}
			else
			{
				Stack stack = new Stack();
				ArrayList arrayList = new ArrayList();
				char[] array = sAgv.ToCharArray();
				int i = 0;
				int num = array.Length;
				string text = string.Empty;
				bool flag2 = array[i] == '(' && array[num - 1] == ')';
				ArrayList arrayList2;
				if (flag2)
				{
					stack.Push(arrayList);
					arrayList2 = arrayList;
					i = 1;
					num--;
				}
				else
				{
					stack.Push(arrayList);
					arrayList2 = arrayList;
				}
				while (i < num)
				{
					char c = array[i];
					text = string.Empty;
					switch (c)
					{
					case '"':
					{
						int num2 = 0;
						int j;
						for (j = i + 1; j < num; j++)
						{
							num2++;
							bool flag3 = array[j] == '"';
							if (flag3)
							{
								char[] array2 = new char[num2 - 1];
								Array.ConstrainedCopy(array, i + 1, array2, 0, num2 - 1);
								text = new string(array2);
								arrayList2.Add(text);
								i = j;
								int num3 = i + 1;
								break;
							}
						}
						bool flag4 = j == num;
						if (flag4)
						{
							return null;
						}
						break;
					}
					case '#':
					case '%':
					case '&':
					case '*':
					case '+':
						goto IL_178;
					case '$':
						break;
					case '\'':
					{
						int num4 = 0;
						int k;
						for (k = i + 1; k < num; k++)
						{
							num4++;
							bool flag5 = array[k] == '\'';
							if (flag5)
							{
								char[] array3 = new char[num4 - 1];
								Array.ConstrainedCopy(array, i + 1, array3, 0, num4 - 1);
								text = new string(array3);
								arrayList2.Add(text);
								i = k;
								int num3 = i + 1;
								break;
							}
						}
						bool flag6 = k == num;
						if (flag6)
						{
							return null;
						}
						break;
					}
					case '(':
					{
						ArrayList arrayList3 = new ArrayList();
						arrayList2.Add(arrayList3);
						arrayList2 = arrayList3;
						stack.Push(arrayList3);
						int num3 = i + 1;
						break;
					}
					case ')':
					{
						stack.Pop();
						arrayList2 = (ArrayList)stack.Peek();
						bool flag7 = arrayList2 == null;
						if (flag7)
						{
							return null;
						}
						int num3 = i + 1;
						break;
					}
					case ',':
					{
						bool flag8 = array[i + 1] != '\'' && array[i + 1] != '"' && array[i + 1] != '(';
						if (flag8)
						{
							bool flag9 = array[i + 1] == '-';
							int num5;
							if (flag9)
							{
								num5 = -1;
							}
							else
							{
								num5 = 1;
							}
							int l;
							for (l = i + 1; l < num + 1; l++)
							{
								text += array[l].ToString();
								bool flag10 = array[l] == ',' || array[l] == ')' || array[l] == '(';
								if (flag10)
								{
									text = text.Substring(0, text.Length - 1);
									int num6 = num5 * this.GetFromInt64(text);
									arrayList2.Add(num6);
									i = l - 1;
									break;
								}
							}
							bool flag11 = l == num + 1;
							if (flag11)
							{
								return null;
							}
						}
						break;
					}
					case '-':
					{
						int num5 = -1;
						int m;
						for (m = i + 1; m < num + 1; m++)
						{
							text += array[m].ToString();
							bool flag12 = array[m] == ',' || array[m] == ')' || array[m] == '(';
							if (flag12)
							{
								text = text.Substring(0, text.Length - 1);
								int num7 = num5 * this.GetFromInt64(text);
								arrayList2.Add(num7);
								i = m - 1;
								break;
							}
						}
						bool flag13 = m == num + 1;
						if (flag13)
						{
							return null;
						}
						break;
					}
					default:
						goto IL_178;
					}
					IL_550:
					i++;
					continue;
					IL_178:
					bool flag14 = c != '\\';
					if (flag14)
					{
						int num5 = 1;
						bool flag15 = false;
						int n;
						for (n = i; n < num; n++)
						{
							text += array[n].ToString();
							bool flag16 = array[n] == ',' || array[n] == ')' || array[n] == '(';
							if (flag16)
							{
								text = text.Substring(0, text.Length - 1);
								int num8 = num5 * this.GetFromInt64(text);
								arrayList2.Add(num8);
								i = n - 1;
								flag15 = true;
								break;
							}
						}
						bool flag17 = !flag15;
						if (flag17)
						{
							text = text.Substring(0, text.Length);
							int num9 = num5 * this.GetFromInt64(text);
							arrayList2.Add(num9);
							i = n;
						}
						bool flag18 = n == num + 1;
						if (flag18)
						{
							return null;
						}
					}
					else
					{
						bool flag19 = array[i + 1] == '"' || array[i + 1] == '\'';
						if (flag19)
						{
							i++;
						}
					}
					goto IL_550;
				}
				bool flag20 = arrayList != null;
				if (flag20)
				{
					object[] array4 = new object[arrayList.Count];
					for (int num10 = 0; num10 < arrayList.Count; num10++)
					{
						array4[num10] = arrayList[num10];
					}
					result = array4;
				}
				else
				{
					result = null;
				}
			}
		}
		catch
		{
			this.SocketErr("lllllllllll" + sAgv);
			result = null;
		}
		return result;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A994 File Offset: 0x00008B94
	private static Type[] conParaType(object[] ali)
	{
		bool flag = ali == null;
		Type[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Type[] array = new Type[ali.Length];
			for (int i = 0; i < ali.Length; i++)
			{
				array[i] = ((ali[i] != null) ? ali[i].GetType() : typeof(object));
			}
			result = array;
		}
		return result;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A9F0 File Offset: 0x00008BF0
	private void getLog(char funtype, object netobj, string funname, object[] funAgv, string str)
	{
		bool flag = BaseSocket.showMsgInfoGet;
		if (flag)
		{
			bool flag2 = funname == "SendMsgToClient";
			string key;
			if (flag2)
			{
				key = string.Format("{0}({1})", funname, funAgv[0].ToString());
			}
			else
			{
				bool flag3 = funname == "SendFbMsgToClient";
				if (flag3)
				{
					key = string.Format("{0}({1})", funname, funAgv[0].ToString());
				}
				else
				{
					key = funname;
				}
			}
			BaseSocket.sec_lenGet += str.Length;
			BaseSocket.sec_countGet++;
			BaseSocket.msg_all_countGet++;
			BaseSocket.msg_all_lenGet += str.Length;
			bool flag4 = Time.realtimeSinceStartup - BaseSocket.lastTimeGet > 1f;
			if (flag4)
			{
				BaseSocket.lastTimeGet = Time.realtimeSinceStartup;
				BaseSocket.msg_now_countGet = BaseSocket.sec_countGet;
				BaseSocket.msg_now_lenGet = BaseSocket.sec_lenGet;
				BaseSocket.sec_lenGet = str.Length;
				BaseSocket.sec_countGet = 1;
			}
			bool flag5 = BaseSocket.dictMsgGet.ContainsKey(key);
			if (flag5)
			{
				BaseSocket.CMsg cmsg = BaseSocket.dictMsgGet[key];
				cmsg.count++;
				cmsg.len += str.Length;
				cmsg.sec_len += str.Length;
				cmsg.sec_count++;
				bool flag6 = Time.realtimeSinceStartup - cmsg.lastTime > 1f;
				if (flag6)
				{
					cmsg.now_len = cmsg.sec_len;
					cmsg.now_count = cmsg.sec_count;
					cmsg.lastTime = Time.realtimeSinceStartup;
					cmsg.sec_len = str.Length;
					cmsg.sec_count = 1;
				}
			}
			else
			{
				BaseSocket.CMsg cmsg2 = new BaseSocket.CMsg();
				cmsg2.len = str.Length;
				cmsg2.count = 1;
				cmsg2.lastTime = Time.realtimeSinceStartup;
				cmsg2.sec_len = cmsg2.len;
				cmsg2.sec_count = 1;
				cmsg2.now_len = 0;
				cmsg2.now_count = 0;
				BaseSocket.dictMsgGet.Add(key, cmsg2);
			}
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000AC0C File Offset: 0x00008E0C
	private bool CallObjFun(char funtype, object netobj, string funname, object[] funAgv)
	{
		bool result;
		try
		{
			Type type = netobj.GetType();
			Type[] array = BaseSocket.conParaType(funAgv);
			bool flag = array == null;
			MethodInfo methodInfo;
			if (flag)
			{
				methodInfo = type.GetMethod(funname);
			}
			else
			{
				methodInfo = type.GetMethod(funname, array);
				bool flag2 = methodInfo == null;
				if (flag2)
				{
					foreach (MethodInfo methodInfo2 in type.GetMethods())
					{
						bool flag3 = methodInfo2.Name == funname;
						if (flag3)
						{
							methodInfo = methodInfo2;
							break;
						}
					}
				}
			}
			bool flag4 = methodInfo != null;
			if (flag4)
			{
				CFilmManager.Instance.InsertGamePlayerFilm(type, netobj, funtype, funname, funAgv);
				methodInfo.Invoke(netobj, funAgv);
				result = true;
			}
			else
			{
				string text = string.Empty;
				text = funname;
				bool flag5 = funAgv.Length != 0;
				if (flag5)
				{
					text = string.Format("{0},{1}", text, BaseSocket.GetShortStr(funAgv));
				}
				Fun.LogError("CallObjFun Error method:" + text, null);
				result = false;
			}
		}
		catch (Exception ex)
		{
			string shortStr = BaseSocket.GetShortStr(funAgv);
			string text2 = (ex.InnerException != null) ? ex.InnerException.Message : "Nenhuma pista interna";
			string text3 = (ex.InnerException != null) ? ex.InnerException.StackTrace : "Sem rastro";
			Debug.LogError(string.Format(" [CRASH NA INTERFACE]\nFuncao Alvo: {0}\nDados lidos: {1}\nErro Casca: {2}\nERRO REAL (Inner): {3}\nRastro Real: {4}", new object[]
			{
				funname,
				shortStr,
				ex.Message,
				text2,
				text3
			}));
			return false;
		}
		return result;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000ADA4 File Offset: 0x00008FA4
	public int GetFromInt64(string str)
	{
		int i = 0;
		char[] array = str.ToCharArray(0, str.Length);
		bool flag = array.Length == 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int num = BaseSocket.s_chr64toint[Convert.ToInt32(array[0])];
			i++;
			int length = str.Length;
			while (i < length)
			{
				num *= 64;
				num += BaseSocket.s_chr64toint[Convert.ToInt32(array[i])];
				i++;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000AE1C File Offset: 0x0000901C
	public static string FormatInt64(int number, int shift)
	{
		char[] array = new char[64];
		int num = 64;
		int num2 = 1 << shift;
		bool flag = number == int.MinValue;
		if (flag)
		{
			number = int.MaxValue;
		}
		long num3 = (long)(num2 - 1);
		do
		{
			array[--num] = BaseSocket.s_int2chr64[(int)((long)number & num3)];
			number >>= shift;
		}
		while (number != 0);
		return new string(array, num, 64 - num);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000AE90 File Offset: 0x00009090
	public string[] GetWordS(string str, string ch, bool bmusthavech)
	{
		string[] array = new string[2];
		int num = str.IndexOf(ch);
		bool flag = num != -1;
		if (flag)
		{
			array[0] = str.Substring(0, num);
			array[1] = str.Substring(num + 1, str.Length - (num + 1));
		}
		else
		{
			if (bmusthavech)
			{
				return array;
			}
			array[0] = str;
			array[1] = string.Empty;
		}
		return array;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000AF00 File Offset: 0x00009100
	public int SendMessageStr(string str)
	{
		int result = 0;
		str = string.Format("{0}\r\n", str);
		byte[] bytes = this.encoding.GetBytes(str);
		bool flag = this.m_bzip && this.m_LoginSt == BaseSocket.LOGIN_ST.LS_LINK;
		if (flag)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)((bytes[i] ^ this.CRYPT_SEED1) + 37 ^ 173);
			}
		}
		try
		{
			bool flag2 = this.clientSocket != null;
			if (flag2)
			{
				result = this.clientSocket.Send(bytes, 0, bytes.Length, SocketFlags.None);
			}
		}
		catch (SocketException ex)
		{
			this.defcmdobj.OnSocketError(ex.ErrorCode, "SendMessageStr:" + ex.Message + "#" + ex.StackTrace);
		}
		return result;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000AFE8 File Offset: 0x000091E8
	public void SendMessage(string str, bool mustSend)
	{
		this.SendMessageStr(str);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000AFF4 File Offset: 0x000091F4
	public void SendMessage(object obj)
	{
		try
		{
			Fun.Log("这个貌似不用了", null);
			short value = (short)Marshal.SizeOf(obj);
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] array = this.StructToBytes(obj);
			byte[] array2 = new byte[bytes.Length + array.Length];
			Array.Copy(bytes, 0, array2, 0, bytes.Length);
			Array.Copy(array, 0, array2, bytes.Length, array.Length);
			bool flag = this.clientSocket != null;
			if (flag)
			{
				this.clientSocket.Send(array2, 0, array2.Length, SocketFlags.None);
			}
		}
		catch (Exception ex)
		{
			Fun.LogError(string.Format("Send message error.{0}", ex.Message), null);
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000B0A4 File Offset: 0x000092A4
	public byte[] StructToBytes(object structObj)
	{
		int num = Marshal.SizeOf(structObj);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		byte[] result;
		try
		{
			Marshal.StructureToPtr(structObj, intPtr, false);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			result = array;
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		return result;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000B104 File Offset: 0x00009304
	public object BytesToStruct(byte[] bytes, Type strcutType)
	{
		int num = Marshal.SizeOf(strcutType);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		object result;
		try
		{
			Marshal.Copy(bytes, 0, intPtr, num);
			result = Marshal.PtrToStructure(intPtr, strcutType);
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		return result;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000B158 File Offset: 0x00009358
	private void sendCallback(IAsyncResult asyncSend)
	{
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000B15C File Offset: 0x0000935C
	public void Closed()
	{
		this.mainThreadId = 0;
		bool flag = this.thread != null;
		if (flag)
		{
			this.thread = null;
		}
		this.m_LoginSt = BaseSocket.LOGIN_ST.LS_UNKOWN;
		this.send_mas_num = 0;
		bool flag2 = this.clientSocket != null && this.clientSocket.Connected;
		if (flag2)
		{
			this.clientSocket.Close();
		}
		this.m_nAllIndex = 0;
		this.m_bNew = false;
		this.clientSocket = null;
		bool flag3 = BaseSocket.s_activeSocket == this;
		if (flag3)
		{
			Application.logMessageReceived -= this.LogCallback;
			BaseSocket.s_activeSocket = null;
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000B1F8 File Offset: 0x000093F8
	public void ToServer(string funname, params object[] args)
	{
		this.ToServerFunReady(null, false, funname, args);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000B208 File Offset: 0x00009408
	private int getnetkey(string kstr)
	{
		int num = 0;
		byte[] bytes = this.encoding.GetBytes(kstr);
		for (int i = 0; i < bytes.Length; i++)
		{
			num += (int)bytes[i];
		}
		return num;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000B248 File Offset: 0x00009448
	public void ToServerFunReady(NetObj pnetobj, bool mustSend, string funname, params object[] args)
	{
		if (mustSend)
		{
			this.ToServerFun(pnetobj, mustSend, funname, args);
		}
		else
		{
			BaseSocket.SendPackage sendPackage = new BaseSocket.SendPackage();
			sendPackage.pnetobj = pnetobj;
			sendPackage.funname = funname;
			sendPackage.args = args;
			this.sendmsgList.Add(sendPackage);
			bool delayNet = this.mainObj.delayNet;
			if (!delayNet)
			{
				while (this.sendmsgList.Count > 0)
				{
					sendPackage = this.sendmsgList[0];
					this.ToServerFun(sendPackage.pnetobj, mustSend, sendPackage.funname, sendPackage.args);
					this.sendmsgList.RemoveAt(0);
				}
			}
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000B2F4 File Offset: 0x000094F4
	private void ToServerFun(NetObj pnetobj, bool mustSend, string funname, params object[] args)
	{
		bool flag = false;
		string shortStr = BaseSocket.GetShortStr(args);
		Debug.Log(" [ENVIOU]: " + funname + " | DADOS: " + shortStr);
		string text = string.Empty;
		string text2 = string.Empty;
		int num = 0;
		bool flag2 = this.clientSocket == null || !this.clientSocket.Connected;
		if (!flag2)
		{
			bool flag3 = pnetobj != null;
			int num2;
			if (flag3)
			{
				num2 = pnetobj.getNetid();
			}
			else
			{
				num2 = 0;
			}
			text2 = "e";
			bool flag4 = num2 != 0;
			if (flag4)
			{
				text2 = string.Format("{0}{1}", text2, BaseSocket.FormatInt64(num2, 6));
				bool flag5 = this.m_bzip && this.m_LoginSt == BaseSocket.LOGIN_ST.LS_LINK;
				if (flag5)
				{
					flag = true;
					text2 = string.Format("{0}:{1}", text2, BaseSocket.FormatInt64(this.send_mas_num + 1, 6));
				}
			}
			bool flag6 = this.m_bzip && this.m_LoginSt == BaseSocket.LOGIN_ST.LS_LINK && num2 != 0;
			if (flag6)
			{
				text2 = string.Format("{0}:", text2);
				num = this.getnetkey(text2);
			}
			text = string.Format("{0}:{1}", text, funname);
			bool flag7 = args.Length != 0;
			if (flag7)
			{
				text = string.Format("{0},{1}", text, BaseSocket.GetShortStr(args));
			}
			bool flag8 = this.m_bzip && this.m_LoginSt == BaseSocket.LOGIN_ST.LS_LINK && num2 != 0;
			string text3;
			if (flag8)
			{
				int num3 = this.random.Next(4000);
				num += this.getnetkey(text);
				num = num / 45 + (this.send_mas_num + 1) + num3;
				text3 = string.Format("{0}{1}:{2}{3}\n", new object[]
				{
					text2,
					BaseSocket.FormatInt64(num3, 6),
					BaseSocket.FormatInt64(num, 6),
					text
				});
			}
			else
			{
				text3 = string.Format("{0}{1}\n", text2, text);
			}
			bool flag9 = flag;
			if (flag9)
			{
				this.send_mas_num++;
			}
			this.SendMessage(text3, mustSend);
			bool flag10 = BaseSocket.showMsgInfo;
			if (flag10)
			{
				bool flag11 = funname == "SendMsgToServer";
				string key;
				if (flag11)
				{
					key = string.Format("{0}({1})", funname, args[0].ToString());
				}
				else
				{
					bool flag12 = funname == "SendMsgToChatServer";
					if (flag12)
					{
						key = string.Format("{0}({1})", funname, args[0].ToString());
					}
					else
					{
						bool flag13 = funname == "SendMsgToHallServer";
						if (flag13)
						{
							key = string.Format("{0}({1})", funname, args[0].ToString());
						}
						else
						{
							key = funname;
						}
					}
				}
				BaseSocket.sec_len += text3.Length;
				BaseSocket.sec_count++;
				BaseSocket.msg_all_count++;
				BaseSocket.msg_all_len += text3.Length;
				bool flag14 = Time.realtimeSinceStartup - BaseSocket.lastTime > 1f;
				if (flag14)
				{
					BaseSocket.lastTime = Time.realtimeSinceStartup;
					BaseSocket.msg_now_count = BaseSocket.sec_count;
					BaseSocket.msg_now_len = BaseSocket.sec_len;
					BaseSocket.sec_len = text3.Length;
					BaseSocket.sec_count = 1;
				}
				bool flag15 = BaseSocket.dictMsg.ContainsKey(key);
				if (flag15)
				{
					BaseSocket.CMsg cmsg = BaseSocket.dictMsg[key];
					cmsg.count++;
					cmsg.len += text3.Length;
					cmsg.sec_len += text3.Length;
					cmsg.sec_count++;
					bool flag16 = Time.realtimeSinceStartup - cmsg.lastTime > 1f;
					if (flag16)
					{
						cmsg.now_len = cmsg.sec_len;
						cmsg.now_count = cmsg.sec_count;
						cmsg.lastTime = Time.realtimeSinceStartup;
						cmsg.sec_len = text3.Length;
						cmsg.sec_count = 1;
					}
				}
				else
				{
					BaseSocket.CMsg cmsg2 = new BaseSocket.CMsg();
					cmsg2.len = text3.Length;
					cmsg2.count = 1;
					cmsg2.lastTime = Time.realtimeSinceStartup;
					cmsg2.sec_len = cmsg2.len;
					cmsg2.sec_count = 1;
					cmsg2.now_len = 0;
					cmsg2.now_count = 0;
					BaseSocket.dictMsg.Add(key, cmsg2);
				}
			}
		}
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000B730 File Offset: 0x00009930
	public static string GetShortStrArr(ArrayList args)
	{
		bool flag = args == null;
		string result;
		if (flag)
		{
			result = "null";
		}
		else
		{
			string str = "(";
			for (int i = 0; i < args.Count; i++)
			{
				object obj = args[i];
				bool flag2 = obj == null;
				if (flag2)
				{
					str += "null";
					bool flag3 = i < args.Count - 1;
					if (flag3)
					{
						str += ",";
					}
				}
				else
				{
					string a = obj.GetType().ToString();
					bool flag4 = !(a == "System.Int32");
					if (flag4)
					{
						bool flag5 = !(a == "System.String");
						if (flag5)
						{
							bool flag6 = !(a == "System.Collections.ArrayList");
							if (flag6)
							{
								str += obj.ToString();
								bool flag7 = i < args.Count - 1;
								if (flag7)
								{
									str += ",";
								}
							}
							else
							{
								str += BaseSocket.GetShortStrArr((ArrayList)obj);
								bool flag8 = i < args.Count - 1;
								if (flag8)
								{
									str += ",";
								}
							}
						}
						else
						{
							str = str + "'" + (string)obj + "'";
							bool flag9 = i < args.Count - 1;
							if (flag9)
							{
								str += ",";
							}
						}
					}
					else
					{
						str = (((int)obj < 0) ? (str + "-" + BaseSocket.FormatInt64(-(int)obj, 6)) : (str + BaseSocket.FormatInt64((int)obj, 6)));
						bool flag10 = i < args.Count - 1;
						if (flag10)
						{
							str += ",";
						}
					}
				}
			}
			result = str + ")";
		}
		return result;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000B920 File Offset: 0x00009B20
	public static string GetShortStr(object[] args)
	{
		bool flag = args == null;
		string result;
		if (flag)
		{
			result = "null";
		}
		else
		{
			string text = string.Empty;
			bool flag2 = args.Length > 1;
			if (flag2)
			{
				text = "(";
			}
			for (int i = 0; i < args.Length; i++)
			{
				object obj = args[i];
				bool flag3 = obj == null;
				if (flag3)
				{
					text += "null";
					bool flag4 = i < args.Length - 1;
					if (flag4)
					{
						text += ",";
					}
				}
				else
				{
					string a = obj.GetType().ToString();
					bool flag5 = !(a == "System.Int32");
					if (flag5)
					{
						bool flag6 = !(a == "System.String");
						if (flag6)
						{
							bool flag7 = !(a == "System.Collections.ArrayList");
							if (flag7)
							{
								text += obj.ToString();
								bool flag8 = i < args.Length - 1;
								if (flag8)
								{
									text += ",";
								}
							}
							else
							{
								text += BaseSocket.GetShortStrArr((ArrayList)obj);
								bool flag9 = i < args.Length - 1;
								if (flag9)
								{
									text += ",";
								}
							}
						}
						else
						{
							text = text + "'" + (string)obj + "'";
							bool flag10 = i < args.Length - 1;
							if (flag10)
							{
								text += ",";
							}
						}
					}
					else
					{
						text = (((int)obj < 0) ? (text + "-" + BaseSocket.FormatInt64(-(int)obj, 6)) : (text + BaseSocket.FormatInt64((int)obj, 6)));
						bool flag11 = i < args.Length - 1;
						if (flag11)
						{
							text += ",";
						}
					}
				}
			}
			bool flag12 = args.Length > 1;
			if (flag12)
			{
				text += ")";
			}
			result = text;
		}
		return result;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000BB24 File Offset: 0x00009D24
	public void SendLoginState(int nstate, string info)
	{
		Debuger.Out(string.Concat(new object[]
		{
			"SendLoginState: ",
			nstate,
			",",
			info
		}));
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000BB58 File Offset: 0x00009D58
	public void PreLoadScene(string sMapName, int nMapId)
	{
		bool flag = GameRoot.mPreLoadManager != null && nMapId != 5205 && nMapId != 5202;
		if (flag)
		{
			GameRoot.mPreLoadManager.StopPreLoad();
		}
		fUIManager.IsMapOK = false;
		fGameManager.GetInstance().nNowScnID = nMapId;
		fGameManager.GetInstance().mloadName = sMapName;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000BBB8 File Offset: 0x00009DB8
	public void CLoadScene(ArrayList szMap)
	{
		string str = "MATCH_DEBUG: CLoadScene called for map: ";
		object obj = szMap[1];
		Debug.Log(str + ((obj != null) ? obj.ToString() : null));
		Fun.Log("加载地图-----", null);
		fUIManager.IsMapOK = false;
		fGameManager.GetInstance().mloadName = szMap[1].ToString();
		fGameManager.GetInstance().loadScene(fGameManager.GetInstance().mloadName, 11, (int)szMap[3]);
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData != null;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound(proomData.m_pRoomFbData.loadsound, 0, new fUISoundManager.UISoundStopFun(this.SoundStop), 20);
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000BC70 File Offset: 0x00009E70
	public void SoundStop()
	{
		bool flag = !fUIManager.IsMapOK;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound("music_commen", 1, null, 20);
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000BCA0 File Offset: 0x00009EA0
	public void LoadMapOKMsg()
	{
		MatchManager.GetInstance().m_nJoinState = 7;
		MatchManager.GetInstance().UserJoinFbLog(MatchManager.GetInstance().m_nJoinState, string.Concat(new object[]
		{
			"地图下载成功 发送loadmapok:",
			fGameManager.bGoToHall,
			":",
			this.clientSocket
		}));
		bool bGoToHall = fGameManager.bGoToHall;
		if (!bGoToHall)
		{
			fUIManager.IsMapOK = true;
			bool flag = fGameManager.Heroplayer != null;
			if (flag)
			{
				fGameManager.m_nLoadCarTime = Time.realtimeSinceStartup - fGameManager.nJoinFbTime;
				fGameManager.nJoinFbTime = Time.realtimeSinceStartup;
			}
			Fun.Log(" zlg  场景 加载完成 进入游戏  " + fGameManager.IsJoinGame.ToString(), null);
			bool flag2 = this.clientSocket == null || !this.clientSocket.Connected;
			if (flag2)
			{
				Debuger.Out(string.Concat(new object[]
				{
					"LoadMapOKMsg send err -----:clientSocket.Connected",
					this.clientSocket.Connected.ToString(),
					"connectServer:",
					this.groupname,
					",",
					this.HOST,
					":",
					this.PORT,
					"\r\n"
				}));
			}
			else
			{
				bool flag3 = MatchManager.GetInstance().m_nMatchType == 3;
				if (flag3)
				{
					KingTrainManager.GetInstance().OnLoadMapOk();
				}
				this.ToServer("SLoadMapOKMsg", new object[]
				{
					SysConfig.g_client,
					Mathf.FloorToInt(fGameManager.m_nLoadScnTime),
					Mathf.FloorToInt(fGameManager.m_nLoadCarTime)
				});
			}
		}
	}

	// Token: 0x04000130 RID: 304
	private static BaseSocket s_activeSocket = null;

	// Token: 0x04000131 RID: 305
	public static int[] s_chr64toint;

	// Token: 0x04000132 RID: 306
	public static char[] s_int2chr64;

	// Token: 0x04000133 RID: 307
	public static string GETSOCKETVER1 = "B1A";

	// Token: 0x04000134 RID: 308
	public static string GETSOCKETVER2 = "6CDA";

	// Token: 0x04000135 RID: 309
	public static string SRESSOCKETVER = "EULG";

	// Token: 0x04000136 RID: 310
	public static string SRESSOCKETVERZIP = "EULB";

	// Token: 0x04000137 RID: 311
	private int m_nCmdLen;

	// Token: 0x04000138 RID: 312
	private byte[] m_szCmd = new byte[0];

	// Token: 0x04000139 RID: 313
	public bool m_bzip = true;

	// Token: 0x0400013A RID: 314
	public byte CRYPT_SEED1 = 227;

	// Token: 0x0400013B RID: 315
	public int send_mas_num;

	// Token: 0x0400013C RID: 316
	protected string groupname = "Group";

	// Token: 0x0400013D RID: 317
	protected string usertype = "SvrUser";

	// Token: 0x0400013E RID: 318
	protected string username = "UserName";

	// Token: 0x0400013F RID: 319
	protected string userpass = "Password";

	// Token: 0x04000140 RID: 320
	protected string ExtMsg = "extmsg";

	// Token: 0x04000141 RID: 321
	protected bool btrace = true;

	// Token: 0x04000142 RID: 322
	protected int CodeID;

	// Token: 0x04000143 RID: 323
	protected int AgvPing;

	// Token: 0x04000144 RID: 324
	public BaseSocket.LOGIN_ST m_LoginSt;

	// Token: 0x04000145 RID: 325
	public string HOST = "192.168.0.68";

	// Token: 0x04000146 RID: 326
	public int PORT = 10002;

	// Token: 0x04000147 RID: 327
	private DBCSEncoding encoding;

	// Token: 0x04000148 RID: 328
	private Socket clientSocket;

	// Token: 0x04000149 RID: 329
	private string classstr;

	// Token: 0x0400014A RID: 330
	private UserSocket defcmdobj;

	// Token: 0x0400014B RID: 331
	private byte[] m_szPool = new byte[4096];

	// Token: 0x0400014C RID: 332
	private byte[] m_szPackagPool = new byte[1048576];

	// Token: 0x0400014D RID: 333
	private byte[] m_szPackag = new byte[1048576];

	// Token: 0x0400014E RID: 334
	private int m_nPackSize;

	// Token: 0x0400014F RID: 335
	private int m_nAllIndex;

	// Token: 0x04000150 RID: 336
	private bool m_bNew;

	// Token: 0x04000151 RID: 337
	public List<string> netErrMsg = new List<string>();

	// Token: 0x04000152 RID: 338
	public Dictionary<string, NetObj> NetObjTable = new Dictionary<string, NetObj>();

	// Token: 0x04000153 RID: 339
	protected int NetObjNum;

	// Token: 0x04000154 RID: 340
	public NetObj mainObj;

	// Token: 0x04000155 RID: 341
	private bool m_bIsChgScn;

	// Token: 0x04000156 RID: 342
	private int m_nReConnectCount;

	// Token: 0x04000157 RID: 343
	private int m_nMaxReConnectCount = 10;

	// Token: 0x04000158 RID: 344
	public int mainThreadId;

	// Token: 0x04000159 RID: 345
	private Thread thread;

	// Token: 0x0400015A RID: 346
	private System.Random random = new System.Random();

	// Token: 0x0400015B RID: 347
	private List<BaseSocket.SendPackage> sendmsgList = new List<BaseSocket.SendPackage>();

	// Token: 0x0400015C RID: 348
	public static int msg_now_count = 0;

	// Token: 0x0400015D RID: 349
	public static int msg_all_count = 0;

	// Token: 0x0400015E RID: 350
	public static int msg_now_len = 0;

	// Token: 0x0400015F RID: 351
	public static int msg_all_len = 0;

	// Token: 0x04000160 RID: 352
	public static int sec_len = 0;

	// Token: 0x04000161 RID: 353
	public static int sec_count = 0;

	// Token: 0x04000162 RID: 354
	public static float lastTime = 0f;

	// Token: 0x04000163 RID: 355
	public static bool showMsgInfo = false;

	// Token: 0x04000164 RID: 356
	public static Dictionary<string, BaseSocket.CMsg> dictMsg = new Dictionary<string, BaseSocket.CMsg>();

	// Token: 0x04000165 RID: 357
	public static int msg_now_countGet = 0;

	// Token: 0x04000166 RID: 358
	public static int msg_all_countGet = 0;

	// Token: 0x04000167 RID: 359
	public static int msg_now_lenGet = 0;

	// Token: 0x04000168 RID: 360
	public static int msg_all_lenGet = 0;

	// Token: 0x04000169 RID: 361
	public static int sec_lenGet = 0;

	// Token: 0x0400016A RID: 362
	public static int sec_countGet = 0;

	// Token: 0x0400016B RID: 363
	public static float lastTimeGet = 0f;

	// Token: 0x0400016C RID: 364
	public static bool showMsgInfoGet = false;

	// Token: 0x0400016D RID: 365
	public static Dictionary<string, BaseSocket.CMsg> dictMsgGet = new Dictionary<string, BaseSocket.CMsg>();

	// Token: 0x02000521 RID: 1313
	public enum LOGIN_ST
	{
		// Token: 0x04002B7B RID: 11131
		LS_UNKOWN,
		// Token: 0x04002B7C RID: 11132
		LS_CONNECT,
		// Token: 0x04002B7D RID: 11133
		LS_LINK,
		// Token: 0x04002B7E RID: 11134
		LS_ERR
	}

	// Token: 0x02000522 RID: 1314
	[Serializable]
	private class SendPackage
	{
		// Token: 0x04002B7F RID: 11135
		public NetObj pnetobj;

		// Token: 0x04002B80 RID: 11136
		public string funname;

		// Token: 0x04002B81 RID: 11137
		public object[] args;
	}

	// Token: 0x02000523 RID: 1315
	public class CMsg
	{
		// Token: 0x04002B82 RID: 11138
		public int len;

		// Token: 0x04002B83 RID: 11139
		public int count;

		// Token: 0x04002B84 RID: 11140
		public float lastTime;

		// Token: 0x04002B85 RID: 11141
		public int sec_len;

		// Token: 0x04002B86 RID: 11142
		public int sec_count;

		// Token: 0x04002B87 RID: 11143
		public int now_len;

		// Token: 0x04002B88 RID: 11144
		public int now_count;
	}
}

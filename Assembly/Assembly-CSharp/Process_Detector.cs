using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class Process_Detector : MonoBehaviour
{
	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0011EBE0 File Offset: 0x0011CDE0
	// (set) Token: 0x06001F14 RID: 7956 RVA: 0x0011EBF8 File Offset: 0x0011CDF8
	public bool Activation_State
	{
		get
		{
			return this._Activation_State;
		}
		set
		{
			this._Activation_State = value;
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0011EC04 File Offset: 0x0011CE04
	public HallUser User
	{
		get
		{
			bool flag = this._User == null;
			if (flag)
			{
				this._User = fGameManager.LobbyPlayer;
			}
			return this._User;
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0011EC38 File Offset: 0x0011CE38
	// (set) Token: 0x06001F17 RID: 7959 RVA: 0x0011EC50 File Offset: 0x0011CE50
	public float Check_User_ID_Time
	{
		get
		{
			return this._Check_User_ID_Time;
		}
		set
		{
			this._Check_User_ID_Time = value;
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0011EC5C File Offset: 0x0011CE5C
	// (set) Token: 0x06001F19 RID: 7961 RVA: 0x0011EC74 File Offset: 0x0011CE74
	public float Check_User_ID_Time_Interval
	{
		get
		{
			return this._Check_User_ID_Time_Interval;
		}
		set
		{
			this._Check_User_ID_Time_Interval = value;
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0011EC80 File Offset: 0x0011CE80
	// (set) Token: 0x06001F1B RID: 7963 RVA: 0x0011EC98 File Offset: 0x0011CE98
	public float Check_Process_Time
	{
		get
		{
			return this._Check_Process_Time;
		}
		set
		{
			this._Check_Process_Time = value;
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0011ECA4 File Offset: 0x0011CEA4
	// (set) Token: 0x06001F1D RID: 7965 RVA: 0x0011ECBC File Offset: 0x0011CEBC
	public float Check_Process_Time_Interval
	{
		get
		{
			return this._Check_Process_Time_Interval;
		}
		set
		{
			this._Check_Process_Time_Interval = value;
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0011ECC8 File Offset: 0x0011CEC8
	// (set) Token: 0x06001F1F RID: 7967 RVA: 0x0011ECE0 File Offset: 0x0011CEE0
	public float Check_MD5_Time
	{
		get
		{
			return this._Check_MD5_Time;
		}
		set
		{
			this._Check_MD5_Time = value;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0011ECEC File Offset: 0x0011CEEC
	// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0011ED04 File Offset: 0x0011CF04
	public float Check_MD5_Time_Interval
	{
		get
		{
			return this._Check_MD5_Time_Interval;
		}
		set
		{
			this._Check_MD5_Time_Interval = value;
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0011ED10 File Offset: 0x0011CF10
	// (set) Token: 0x06001F23 RID: 7971 RVA: 0x0011ED42 File Offset: 0x0011CF42
	public List<int> Detector_User_ID_Data
	{
		get
		{
			bool flag = this._Detector_User_ID_Data == null;
			if (flag)
			{
				this._Detector_User_ID_Data = new List<int>();
			}
			return this._Detector_User_ID_Data;
		}
		set
		{
			this._Detector_User_ID_Data = value;
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0011ED4C File Offset: 0x0011CF4C
	// (set) Token: 0x06001F25 RID: 7973 RVA: 0x0011ED7E File Offset: 0x0011CF7E
	public List<string> Detector_Progress_Data
	{
		get
		{
			bool flag = this._Detector_Progress_Data == null;
			if (flag)
			{
				this._Detector_Progress_Data = new List<string>();
			}
			return this._Detector_Progress_Data;
		}
		set
		{
			this._Detector_Progress_Data = value;
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0011ED88 File Offset: 0x0011CF88
	// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0011EDB7 File Offset: 0x0011CFB7
	public static List<string> Detector_MD5_Data
	{
		get
		{
			bool flag = Process_Detector._Detector_MD5_Data == null;
			if (flag)
			{
				Process_Detector._Detector_MD5_Data = new List<string>();
			}
			return Process_Detector._Detector_MD5_Data;
		}
		set
		{
			Process_Detector._Detector_MD5_Data = value;
		}
	}

	// Token: 0x06001F28 RID: 7976
	[DllImport("code")]
	private static extern IntPtr GetProgressStr(byte[] InBuf);

	// Token: 0x06001F29 RID: 7977 RVA: 0x0011EDC0 File Offset: 0x0011CFC0
	private void Start()
	{
		base.StartCoroutine(this.Get_Detector_Data(SysConfig.getWebURL("/qqapi/GameGetMsgFromadmin.aspx?type=str")));
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x0011EDDA File Offset: 0x0011CFDA
	private IEnumerator Get_Detector_Data(string _Parameters)
	{
		WWW _WWW = new WWW(_Parameters);
		yield return _WWW;
		bool flag = _WWW.error == null;
		if (flag)
		{
			this.Set_Detector_Data(_WWW.text);
		}
		yield break;
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x0011EDF0 File Offset: 0x0011CFF0
	private void Set_Detector_Data(string _Parameters)
	{
		this.Activation_State = true;
		_Parameters = _Parameters.ToLower();
		string[] array = _Parameters.Split(new char[]
		{
			'|'
		});
		bool flag = !string.IsNullOrEmpty(array[0].ToString());
		if (flag)
		{
			string[] array2 = array[0].ToString().Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				bool flag2 = !string.IsNullOrEmpty(array2[i].ToString()) && !this.Detector_User_ID_Data.Contains(int.Parse(array2[i].ToString()));
				if (flag2)
				{
					this.Detector_User_ID_Data.Add(int.Parse(array2[i].ToString()));
				}
			}
		}
		bool flag3 = !string.IsNullOrEmpty(array[1].ToString());
		if (flag3)
		{
			string[] array3 = array[1].ToString().Split(new char[]
			{
				';'
			});
			for (int j = 0; j < array3.Length; j++)
			{
				bool flag4 = !string.IsNullOrEmpty(array3[j].ToString()) && !this.Detector_Progress_Data.Contains(array3[j].ToString());
				if (flag4)
				{
					this.Detector_Progress_Data.Add(array3[j].ToString());
				}
			}
		}
		bool flag5 = string.IsNullOrEmpty(array[2].ToString());
		if (!flag5)
		{
			string[] array4 = array[2].ToString().Split(new char[]
			{
				';'
			});
			for (int k = 0; k < array4.Length; k++)
			{
				bool flag6 = !string.IsNullOrEmpty(array4[k].ToString()) && !Process_Detector.Detector_MD5_Data.Contains(array4[k].ToString());
				if (flag6)
				{
					Process_Detector.Detector_MD5_Data.Add(array4[k].ToString());
				}
			}
		}
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x0011EFE5 File Offset: 0x0011D1E5
	private void Update()
	{
		this.Check_User_ID();
		this.Check_Process();
		this.Check_MD5();
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x0011F000 File Offset: 0x0011D200
	private void Check_User_ID()
	{
		bool flag = this.Activation_State && Time.time >= this.Check_User_ID_Time && this.User != null && this.User.Load_Data_State;
		if (flag)
		{
			this.Check_User_ID_Time = Time.time + this.Check_User_ID_Time_Interval;
			int nCid = PlayerDataManager.getInstance().HUserData.m_nCid;
			bool flag2 = this.Detector_User_ID_Data.Contains(nCid);
			if (flag2)
			{
				this.Check_User_Process();
			}
		}
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0011F080 File Offset: 0x0011D280
	private void Check_User_Process()
	{
		try
		{
			byte[] array = new byte[8192];
			Process_Detector.GetProgressStr(array);
			string @string = Encoding.GetEncoding("gb2312").GetString(array);
			string[] array2 = @string.Split(new char[]
			{
				';'
			});
			bool flag = false;
			for (int i = 0; i < array2.Length - 1; i++)
			{
				string[] array3 = array2[i].ToString().Split(new char[]
				{
					'\\'
				});
				string text = array3[array3.Length - 1];
				text = text.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
				text = text.ToLower();
				bool flag2 = !this._User_Process_Data.Contains(text);
				if (flag2)
				{
					this._User_Process_Data.Add(text);
					flag = true;
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				string text = string.Empty;
				for (int j = 0; j < this._User_Process_Data.Count; j++)
				{
					text = text + this._User_Process_Data[j] + ";";
				}
				this.Send_Suspicious_Process_Data(1, "可疑进程:" + text);
			}
		}
		catch
		{
		}
		finally
		{
		}
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x0011F228 File Offset: 0x0011D428
	private void Check_Process()
	{
		bool flag = !this.Activation_State || Time.time < this.Check_Process_Time || this.User == null || !this.User.Load_Data_State;
		if (!flag)
		{
			try
			{
				this.Check_Process_Time = Time.time + this.Check_Process_Time_Interval;
				byte[] array = new byte[8192];
				Process_Detector.GetProgressStr(array);
				string @string = Encoding.GetEncoding("gb2312").GetString(array);
				string[] array2 = @string.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array2.Length - 1; i++)
				{
					string[] array3 = array2[i].ToString().Split(new char[]
					{
						'\\'
					});
					string text = array3[array3.Length - 1];
					text = text.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
					text = text.ToLower();
					for (int j = 0; j < this.Detector_Progress_Data.Count; j++)
					{
						bool flag2 = text.Contains(this.Detector_Progress_Data[j]);
						if (flag2)
						{
							this.Send_Suspicious_Process_Data(2, text);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
			}
		}
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x0011F3D8 File Offset: 0x0011D5D8
	private void Check_MD5()
	{
		bool flag = !this.Activation_State || Time.time < this.Check_MD5_Time || this.User == null || !this.User.Load_Data_State;
		if (!flag)
		{
			try
			{
				this.Check_MD5_Time = Time.time + this.Check_MD5_Time_Interval;
				byte[] array = new byte[8192];
				Process_Detector.GetProgressStr(array);
				string @string = Encoding.GetEncoding("gb2312").GetString(array);
				string[] array2 = @string.Split(new char[]
				{
					';'
				});
				string text = string.Empty;
				for (int i = 0; i < array2.Length; i++)
				{
					text = Process_Detector.Get_MD5_Hash(array2[i]);
					for (int j = 0; j < Process_Detector.Detector_MD5_Data.Count; j++)
					{
						bool flag2 = Process_Detector.Detector_MD5_Data[j].ToUpper().Equals(text);
						if (flag2)
						{
							this.Send_Suspicious_Process_Data(3, string.Concat(new string[]
							{
								"可疑MD5，进程名:",
								array2[i],
								" - MD5:",
								Process_Detector.Detector_MD5_Data[j],
								"- Hash:",
								text
							}));
							this.Activation_State = false;
							fGameManager.GetInstance().OnFengHaoToServer();
							base.Invoke("Quit", 5f);
							return;
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
			}
		}
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x0011F590 File Offset: 0x0011D790
	private void Quit()
	{
		Application.Quit();
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x0011F59C File Offset: 0x0011D79C
	public static string Get_MD5_Hash(string _Process_Name)
	{
		string result = string.Empty;
		string text = string.Empty;
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		try
		{
			FileStream fileStream = new FileStream(_Process_Name.Replace("\"", string.Empty), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			byte[] value = md5CryptoServiceProvider.ComputeHash(fileStream);
			fileStream.Close();
			text = BitConverter.ToString(value);
			text = text.Replace("-", string.Empty);
			result = text;
		}
		catch (Exception ex)
		{
			Fun.outlog(3, "GetMd5Hash error: " + ex.Message, null);
		}
		return result;
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x0011F63C File Offset: 0x0011D83C
	private void Send_Suspicious_Process_Data(int _Type, string _Process_Name)
	{
		bool flag = this.User != null && this.User.Load_Data_State;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			arrayList2.Add(_Type);
			arrayList2.Add(_Process_Name);
			arrayList.Add(3030001);
			arrayList.Add(arrayList2);
			this.User.SendMsgToHallServer(3030000, arrayList);
		}
	}

	// Token: 0x04001C7B RID: 7291
	private bool _Activation_State;

	// Token: 0x04001C7C RID: 7292
	private HallUser _User;

	// Token: 0x04001C7D RID: 7293
	private float _Check_User_ID_Time;

	// Token: 0x04001C7E RID: 7294
	private float _Check_User_ID_Time_Interval = 5f;

	// Token: 0x04001C7F RID: 7295
	private float _Check_Process_Time;

	// Token: 0x04001C80 RID: 7296
	private float _Check_Process_Time_Interval = 300f;

	// Token: 0x04001C81 RID: 7297
	private float _Check_MD5_Time;

	// Token: 0x04001C82 RID: 7298
	private float _Check_MD5_Time_Interval = 300f;

	// Token: 0x04001C83 RID: 7299
	private List<int> _Detector_User_ID_Data = new List<int>();

	// Token: 0x04001C84 RID: 7300
	private List<string> _Detector_Progress_Data = new List<string>();

	// Token: 0x04001C85 RID: 7301
	private static List<string> _Detector_MD5_Data = new List<string>();

	// Token: 0x04001C86 RID: 7302
	private List<string> _User_Process_Data = new List<string>();
}

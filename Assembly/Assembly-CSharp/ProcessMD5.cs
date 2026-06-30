using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class ProcessMD5 : MonoBehaviour
{
	// Token: 0x06001F0B RID: 7947
	[DllImport("code")]
	private static extern IntPtr GetProgressStr(byte[] InBuf);

	// Token: 0x06001F0C RID: 7948 RVA: 0x0011E8DC File Offset: 0x0011CADC
	private void Start()
	{
		this.bgetstr = false;
		this.sArrMd5 = new List<string>();
		base.Invoke("InSetMD5", 300f);
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x0011E902 File Offset: 0x0011CB02
	private void InSetMD5()
	{
		this.SetMd5("5790C71CE245C9498F0BF1B03870DEC2", null);
		base.Invoke("InSetMD5", 300f);
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x0011E924 File Offset: 0x0011CB24
	private void Update()
	{
		bool flag = this.sArrMd5.Count <= 0 || (int)(Time.realtimeSinceStartup % 3f) != 0;
		if (!flag)
		{
			bool flag2 = !this.bgetstr;
			if (flag2)
			{
				this.bgetstr = true;
				byte[] array = new byte[8192];
				IntPtr progressStr = ProcessMD5.GetProgressStr(array);
				string @string = Encoding.GetEncoding("gb2312").GetString(array);
				bool flag3 = @string == null;
				if (flag3)
				{
					Debug.Log("process str null;");
					this.bgetstr = false;
					return;
				}
				this.pathstr = @string.Split(new char[]
				{
					';'
				});
				bool flag4 = this.Pcallback != null;
				if (flag4)
				{
					this.Pcallback(1, string.Empty);
				}
			}
			string md5Hash = ProcessMD5.GetMd5Hash(this.pathstr[this.crridx]);
			int i = 0;
			while (i < this.sArrMd5.Count)
			{
				bool flag5 = this.sArrMd5[i].Equals(md5Hash);
				if (flag5)
				{
					bool flag6 = this.Pcallback != null;
					if (flag6)
					{
						this.Pcallback(2, md5Hash + ";" + this.pathstr[this.crridx]);
					}
					else
					{
						Debug.Log(md5Hash + ";" + this.pathstr[this.crridx]);
						this.abc();
					}
				}
				IL_158:
				i++;
				continue;
				goto IL_158;
			}
			this.crridx++;
			bool flag7 = this.crridx >= this.pathstr.Length;
			if (flag7)
			{
				this.crridx = 0;
				this.bgetstr = false;
				this.sArrMd5 = new List<string>();
				bool flag8 = this.Pcallback != null;
				if (flag8)
				{
					this.Pcallback(3, string.Empty);
				}
			}
		}
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x0011EB0C File Offset: 0x0011CD0C
	private void abc()
	{
		this.abc();
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x0011EB16 File Offset: 0x0011CD16
	public void SetMd5(string md5str, ProcessMD5.CheckMd5CallBackFun callfun)
	{
		this.sArrMd5 = Process_Detector.Detector_MD5_Data;
		this.crridx = 0;
		this.bgetstr = false;
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x0011EB34 File Offset: 0x0011CD34
	public static string GetMd5Hash(string pathName)
	{
		string result = string.Empty;
		string text = string.Empty;
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		try
		{
			FileStream fileStream = new FileStream(pathName.Replace("\"", string.Empty), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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

	// Token: 0x04001C76 RID: 7286
	private string[] pathstr;

	// Token: 0x04001C77 RID: 7287
	private List<string> sArrMd5;

	// Token: 0x04001C78 RID: 7288
	private int crridx;

	// Token: 0x04001C79 RID: 7289
	private bool bgetstr;

	// Token: 0x04001C7A RID: 7290
	public ProcessMD5.CheckMd5CallBackFun Pcallback;

	// Token: 0x020005F9 RID: 1529
	// (Invoke) Token: 0x06003697 RID: 13975
	public delegate void CheckMd5CallBackFun(int type, string has);
}

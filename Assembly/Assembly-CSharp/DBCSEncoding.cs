using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public sealed class DBCSEncoding : Encoding
{
	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000495BF File Offset: 0x000477BF
	public override string WebName
	{
		get
		{
			return this._webName;
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x000495C8 File Offset: 0x000477C8
	static DBCSEncoding()
	{
		bool flag = !BitConverter.IsLittleEndian;
		if (flag)
		{
			throw new PlatformNotSupportedException("Not supported big endian platform.");
		}
		DBCSEncoding._cache = new Dictionary<string, DBCSEncoding.Tuple<char[], ushort[]>>();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x000495F8 File Offset: 0x000477F8
	public void GetDBCSEncoding(string name)
	{
		UnityEngine.Object @object = Resources.Load("gb2312");
		TextAsset textAsset = (TextAsset)@object;
		MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
		this._webName = name;
		BinaryReader binaryReader = new BinaryReader(memoryStream);
		this._dbcsToUnicode = new char[65536];
		this._unicodeToDbcs = new ushort[65536];
		for (int i = 0; i < 65535; i++)
		{
			ushort num = binaryReader.ReadUInt16();
			this._unicodeToDbcs[i] = num;
		}
		for (int j = 0; j < 65535; j++)
		{
			ushort num2 = binaryReader.ReadUInt16();
			this._dbcsToUnicode[j] = (char)num2;
		}
		memoryStream.Close();
		binaryReader.Close();
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x000496C0 File Offset: 0x000478C0
	private void DownloadOK(WWW www, object sParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			string text = (string)sParam;
			bool flag2 = !DBCSEncoding._cache.ContainsKey(text);
			if (flag2)
			{
				MemoryStream memoryStream = new MemoryStream(www.bytes);
				this._webName = text;
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				this._dbcsToUnicode = new char[65536];
				this._unicodeToDbcs = new ushort[65536];
				for (int i = 0; i < 65535; i++)
				{
					ushort num = binaryReader.ReadUInt16();
					this._unicodeToDbcs[i] = num;
				}
				for (int j = 0; j < 65535; j++)
				{
					ushort num2 = binaryReader.ReadUInt16();
					this._dbcsToUnicode[j] = (char)num2;
				}
				memoryStream.Close();
				binaryReader.Close();
			}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x000497B0 File Offset: 0x000479B0
	public override int GetByteCount(char[] chars, int index, int count)
	{
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			char c = chars[index];
			ushort num2 = this._unicodeToDbcs[(int)c];
			bool flag = num2 > 255;
			if (flag)
			{
				num++;
			}
			index++;
			num++;
		}
		return num;
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00049808 File Offset: 0x00047A08
	public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
	{
		int num = 0;
		for (int i = 0; i < charCount; i++)
		{
			char c = chars[charIndex];
			ushort num2 = this._unicodeToDbcs[(int)c];
			bool flag = num2 == 0 && c > '\0';
			if (flag)
			{
				bytes[byteIndex] = 63;
			}
			else
			{
				bool flag2 = num2 < 256;
				if (flag2)
				{
					bytes[byteIndex] = (byte)num2;
				}
				else
				{
					bytes[byteIndex] = (byte)(num2 >> 8 & 255);
					byteIndex++;
					num++;
					bytes[byteIndex] = (byte)(num2 & 255);
				}
			}
			charIndex++;
			byteIndex++;
			num++;
		}
		return num;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000498B0 File Offset: 0x00047AB0
	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return this.GetCharCount(bytes, index, count, null);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000498CC File Offset: 0x00047ACC
	private int GetCharCount(byte[] bytes, int index, int count, DBCSEncoding.DBCSDecoder decoder)
	{
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			ushort num2 = 0;
			bool flag = decoder != null && decoder.pendingByte > 0;
			if (flag)
			{
				num2 = (ushort)decoder.pendingByte;
				decoder.pendingByte = 0;
			}
			num2 = (ushort)((int)num2 << 8 | (int)bytes[index]);
			char c = this._dbcsToUnicode[(int)num2];
			bool flag2 = c == '￾';
			if (flag2)
			{
				bool flag3 = i < count - 1;
				if (flag3)
				{
					index++;
					i++;
				}
				else
				{
					bool flag4 = decoder != null;
					if (flag4)
					{
						decoder.pendingByte = bytes[index];
						return num;
					}
				}
			}
			index++;
			num++;
		}
		return num;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00049988 File Offset: 0x00047B88
	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, null);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x000499A8 File Offset: 0x00047BA8
	private int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, DBCSEncoding.DBCSDecoder decoder)
	{
		int num = 0;
		for (int i = 0; i < byteCount; i++)
		{
			ushort num2 = 0;
			bool flag = decoder != null && decoder.pendingByte > 0;
			if (flag)
			{
				num2 = (ushort)decoder.pendingByte;
				decoder.pendingByte = 0;
			}
			num2 = (ushort)((int)num2 << 8 | (int)bytes[byteIndex]);
			char c = this._dbcsToUnicode[(int)num2];
			bool flag2 = c == '￾';
			if (flag2)
			{
				bool flag3 = i < byteCount - 1;
				if (flag3)
				{
					byteIndex++;
					i++;
					num2 = (ushort)((int)num2 << 8 | (int)bytes[byteIndex]);
					c = this._dbcsToUnicode[(int)num2];
				}
				else
				{
					bool flag4 = decoder != null;
					if (flag4)
					{
						decoder.pendingByte = bytes[byteIndex];
						return num;
					}
					c = '\0';
				}
			}
			bool flag5 = c == '\0' && num2 > 0;
			if (flag5)
			{
				chars[charIndex] = '?';
			}
			else
			{
				chars[charIndex] = c;
			}
			byteIndex++;
			charIndex++;
			num++;
		}
		return num;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00049AA4 File Offset: 0x00047CA4
	public override int GetMaxByteCount(int charCount)
	{
		bool flag = charCount < 0;
		if (flag)
		{
			throw new ArgumentOutOfRangeException("charCount");
		}
		long num = (long)(charCount + 1);
		num *= 2L;
		bool flag2 = num > 2147483647L;
		if (flag2)
		{
			throw new ArgumentOutOfRangeException("charCount");
		}
		return (int)num;
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00049AF0 File Offset: 0x00047CF0
	public override int GetMaxCharCount(int byteCount)
	{
		bool flag = byteCount < 0;
		if (flag)
		{
			throw new ArgumentOutOfRangeException("byteCount");
		}
		long num = (long)(byteCount + 3);
		bool flag2 = num > 2147483647L;
		if (flag2)
		{
			throw new ArgumentOutOfRangeException("byteCount");
		}
		return (int)num;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00049B38 File Offset: 0x00047D38
	public override Decoder GetDecoder()
	{
		return new DBCSEncoding.DBCSDecoder(this);
	}

	// Token: 0x04000649 RID: 1609
	private const char LEAD_BYTE_CHAR = '￾';

	// Token: 0x0400064A RID: 1610
	private char[] _dbcsToUnicode;

	// Token: 0x0400064B RID: 1611
	private ushort[] _unicodeToDbcs;

	// Token: 0x0400064C RID: 1612
	private string _webName;

	// Token: 0x0400064D RID: 1613
	private static Dictionary<string, DBCSEncoding.Tuple<char[], ushort[]>> _cache;

	// Token: 0x0400064E RID: 1614
	public static FileStream mfs;

	// Token: 0x0400064F RID: 1615
	public static MemoryStream mMemfs;

	// Token: 0x0200054B RID: 1355
	private struct Tuple<T0>
	{
		// Token: 0x04002C82 RID: 11394
		public T0 Field0;
	}

	// Token: 0x0200054C RID: 1356
	private struct Tuple<T0, T1>
	{
		// Token: 0x060033A1 RID: 13217 RVA: 0x001BB0F1 File Offset: 0x001B92F1
		public Tuple(T0 tt, T1 t2)
		{
			this.Item1 = tt;
			this.Item2 = t2;
		}

		// Token: 0x04002C83 RID: 11395
		public T0 Item1;

		// Token: 0x04002C84 RID: 11396
		public T1 Item2;
	}

	// Token: 0x0200054D RID: 1357
	private struct Tuple<T0, T1, T2>
	{
		// Token: 0x04002C85 RID: 11397
		public T0 Field0;

		// Token: 0x04002C86 RID: 11398
		public T1 Field1;

		// Token: 0x04002C87 RID: 11399
		public T2 Field2;
	}

	// Token: 0x0200054E RID: 1358
	private sealed class DBCSDecoder : Decoder
	{
		// Token: 0x060033A2 RID: 13218 RVA: 0x001BB101 File Offset: 0x001B9301
		public DBCSDecoder(DBCSEncoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x001BB114 File Offset: 0x001B9314
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this._encoding.GetCharCount(bytes, index, count, this);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x001BB138 File Offset: 0x001B9338
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this._encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex, this);
		}

		// Token: 0x04002C88 RID: 11400
		private DBCSEncoding _encoding;

		// Token: 0x04002C89 RID: 11401
		public byte pendingByte;
	}
}

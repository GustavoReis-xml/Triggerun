using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x020001F6 RID: 502
public class ImportFromDLL
{
	// Token: 0x06001611 RID: 5649
	[DllImport("User32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref ImportFromDLL.COPYDATASTRUCT pcd);

	// Token: 0x06001612 RID: 5650
	[DllImport("User32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

	// Token: 0x06001613 RID: 5651
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

	// Token: 0x06001614 RID: 5652
	[DllImport("User32.dll")]
	public static extern int MoveWindow(int hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

	// Token: 0x06001615 RID: 5653
	[DllImport("User32.dll")]
	public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	// Token: 0x06001616 RID: 5654
	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

	// Token: 0x06001617 RID: 5655
	[DllImport("kernel32.dll")]
	public static extern int OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

	// Token: 0x06001618 RID: 5656
	[DllImport("Kernel32.dll")]
	public static extern IntPtr GetConsoleWindow();

	// Token: 0x06001619 RID: 5657
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

	// Token: 0x0600161A RID: 5658
	[DllImport("Shell32.dll")]
	internal static extern void GetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);

	// Token: 0x0600161B RID: 5659 RVA: 0x000CDC38 File Offset: 0x000CBE38
	public static bool KillProcess(string WindowName)
	{
		IntPtr hwnd = ImportFromDLL.FindWindow(null, WindowName);
		bool flag = hwnd.ToInt32() == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num;
			ImportFromDLL.GetWindowThreadProcessId(hwnd, out num);
			bool flag2 = num == 0;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num2 = ImportFromDLL.OpenProcess(2035711, false, num);
				result = (num2 != 0 && ImportFromDLL.TerminateProcess(new IntPtr(num2), 0U));
			}
		}
		return result;
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000CDCA0 File Offset: 0x000CBEA0
	public static byte[] FromBinStrToUTF8Bytes(string s)
	{
		char[] array = s.ToCharArray();
		int num = s.Length / 2;
		List<byte> list = new List<byte>();
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 2;
			string s2 = new string(new char[]
			{
				array[num2],
				array[num2 + 1]
			});
			list.Add(byte.Parse(s2, NumberStyles.HexNumber));
		}
		return list.ToArray();
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000CDD1C File Offset: 0x000CBF1C
	public static string StrToBin(string s)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++)
		{
			stringBuilder.Append(bytes[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04001122 RID: 4386
	public const int WM_COPYDATA = 74;

	// Token: 0x04001123 RID: 4387
	public const int SW_SHOW = 5;

	// Token: 0x04001124 RID: 4388
	public const int SW_HIDE = 0;

	// Token: 0x04001125 RID: 4389
	private const int PROCESS_ALL_ACCESS = 2035711;

	// Token: 0x04001126 RID: 4390
	private const int PROCESS_VM_READ = 16;

	// Token: 0x04001127 RID: 4391
	private const int PROCESS_VM_WRITE = 32;

	// Token: 0x04001128 RID: 4392
	public const int SER_MESSAAGE = 1124;

	// Token: 0x020005D8 RID: 1496
	public struct COPYDATASTRUCT
	{
		// Token: 0x04002E66 RID: 11878
		public int dwData;

		// Token: 0x04002E67 RID: 11879
		public int cbData;

		// Token: 0x04002E68 RID: 11880
		[MarshalAs(UnmanagedType.LPStr)]
		public string lpData;
	}
}

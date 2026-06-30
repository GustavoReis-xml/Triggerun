using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Token: 0x020000EC RID: 236
public class CWin32Help
{
	// Token: 0x06000886 RID: 2182
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool EnumWindows(CWin32Help.Wndenumproc lpEnumFunc, uint lParam);

	// Token: 0x06000887 RID: 2183
	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr GetParent(IntPtr hWnd);

	// Token: 0x06000888 RID: 2184
	[DllImport("user32.dll")]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

	// Token: 0x06000889 RID: 2185
	[DllImport("kernel32.dll")]
	private static extern void SetLastError(uint dwErrCode);

	// Token: 0x0600088A RID: 2186 RVA: 0x00048B9C File Offset: 0x00046D9C
	public static IntPtr GetProcessWnd()
	{
		IntPtr ptrWnd = IntPtr.Zero;
		uint id = (uint)Process.GetCurrentProcess().Id;
		return (CWin32Help.EnumWindows(delegate(IntPtr hwnd, uint lParam)
		{
			uint num = 0U;
			bool flag = CWin32Help.GetParent(hwnd) != IntPtr.Zero;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				CWin32Help.GetWindowThreadProcessId(hwnd, ref num);
				bool flag2 = num != lParam;
				if (flag2)
				{
					result = true;
				}
				else
				{
					ptrWnd = hwnd;
					CWin32Help.SetLastError(0U);
					result = false;
				}
			}
			return result;
		}, id) || Marshal.GetLastWin32Error() != 0) ? IntPtr.Zero : ptrWnd;
	}

	// Token: 0x0600088B RID: 2187
	[DllImport("imm32.dll")]
	private static extern IntPtr ImmGetContext(IntPtr hwnd);

	// Token: 0x0600088C RID: 2188
	[DllImport("imm32.dll")]
	private static extern bool ImmGetOpenStatus(IntPtr himc);

	// Token: 0x0600088D RID: 2189
	[DllImport("imm32.dll")]
	private static extern bool ImmSetOpenStatus(IntPtr himc, bool b);

	// Token: 0x0600088E RID: 2190 RVA: 0x00048BF4 File Offset: 0x00046DF4
	public static IntPtr GetIme(IntPtr tf)
	{
		return CWin32Help.ImmGetContext(tf);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00048C0C File Offset: 0x00046E0C
	public static bool GetImeStatus(IntPtr tf)
	{
		return CWin32Help.ImmGetOpenStatus(tf);
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00048C24 File Offset: 0x00046E24
	public static bool SetImeStatus(IntPtr tf, bool open)
	{
		return CWin32Help.ImmSetOpenStatus(tf, open);
	}

	// Token: 0x02000549 RID: 1353
	// (Invoke) Token: 0x0600339C RID: 13212
	private delegate bool Wndenumproc(IntPtr hwnd, uint lParam);
}

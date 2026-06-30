using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class CInputMethodManager : MonoBehaviour
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x00029D6C File Offset: 0x00027F6C
	// (set) Token: 0x06000517 RID: 1303 RVA: 0x00029D83 File Offset: 0x00027F83
	public static CInputMethodManager Instance
	{
		get
		{
			return CInputMethodManager._instance;
		}
		set
		{
			CInputMethodManager._instance = value;
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00029D8C File Offset: 0x00027F8C
	private void Start()
	{
		CInputMethodManager.Instance = this;
		this.handle = CWin32Help.GetProcessWnd();
		this.imm = CWin32Help.GetIme(this.handle);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00029DB4 File Offset: 0x00027FB4
	private void Update()
	{
		bool flag = !ChatManager.ChatInput;
		if (flag)
		{
			bool imeStatus = CWin32Help.GetImeStatus(this.imm);
			if (imeStatus)
			{
				CWin32Help.SetImeStatus(this.imm, false);
			}
		}
		else
		{
			bool flag2 = !CWin32Help.GetImeStatus(this.imm);
			if (flag2)
			{
				CWin32Help.SetImeStatus(this.imm, true);
			}
		}
	}

	// Token: 0x040003EF RID: 1007
	private IntPtr handle;

	// Token: 0x040003F0 RID: 1008
	private IntPtr imm;

	// Token: 0x040003F1 RID: 1009
	private static CInputMethodManager _instance;
}

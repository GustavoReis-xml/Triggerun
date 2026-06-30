using System;
using UnityEngine;

// Token: 0x020004D5 RID: 1237
public class _LushLODPostProcessor_DisableHQ : MonoBehaviour
{
	// Token: 0x06002F5A RID: 12122 RVA: 0x001A690A File Offset: 0x001A4B0A
	private void OnEnable()
	{
		_LushLODPostProcessor_DisableHQ.DisablingHQ = true;
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x001A6913 File Offset: 0x001A4B13
	private void OnDisable()
	{
		_LushLODPostProcessor_DisableHQ.DisablingHQ = false;
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x001A691C File Offset: 0x001A4B1C
	private void Start()
	{
		_LushLODPostProcessor_DisableHQ.DisablingHQ = true;
	}

	// Token: 0x06002F5D RID: 12125 RVA: 0x001A6925 File Offset: 0x001A4B25
	private void OnPreRender()
	{
		Shader.SetGlobalInt("_LushLODTreeDisableFarDithering", 0);
		Shader.SetGlobalInt("_LushLODTreeDisableHQ", 0);
	}

	// Token: 0x06002F5E RID: 12126 RVA: 0x001A6940 File Offset: 0x001A4B40
	private void OnPostRender()
	{
		Shader.SetGlobalInt("_LushLODTreeDisableFarDithering", 1);
		Shader.SetGlobalInt("_LushLODTreeDisableHQ", 1);
	}

	// Token: 0x04002907 RID: 10503
	public static bool DisablingHQ;
}

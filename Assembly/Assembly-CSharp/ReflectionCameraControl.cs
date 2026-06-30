using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class ReflectionCameraControl : MonoBehaviour
{
	// Token: 0x060020FE RID: 8446 RVA: 0x00130680 File Offset: 0x0012E880
	private void OnPreRender()
	{
		GL.invertCulling = true;
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x0013068A File Offset: 0x0012E88A
	private void OnPostRender()
	{
		base.GetComponent<Camera>().targetTexture = MirrorReflection.m_ReflectionTexture;
		GL.invertCulling = false;
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x001306A5 File Offset: 0x0012E8A5
	private void OnDestroy()
	{
		GL.invertCulling = false;
	}
}

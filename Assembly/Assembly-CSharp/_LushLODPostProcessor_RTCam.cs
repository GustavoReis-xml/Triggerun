using System;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class _LushLODPostProcessor_RTCam : MonoBehaviour
{
	// Token: 0x06002F60 RID: 12128 RVA: 0x001A6964 File Offset: 0x001A4B64
	private void DontHogSoMuchMemory()
	{
		bool flag = this.OriginalPixels != null || this.StencilPixels != null;
		if (flag)
		{
			bool flag2 = this.OriginalPixels != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.OriginalPixels);
			}
			bool flag3 = this.StencilPixels != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.StencilPixels);
			}
		}
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x001A69D4 File Offset: 0x001A4BD4
	private void Update()
	{
		bool flag = this.OriginalPixels.width != Screen.width || this.OriginalPixels.height != Screen.height;
		if (flag)
		{
			this.DontHogSoMuchMemory();
			this.OriginalPixels = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
			this.StencilPixels = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		}
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x001A6A49 File Offset: 0x001A4C49
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x001A6A53 File Offset: 0x001A4C53
	private void OnPreRender()
	{
		Shader.SetGlobalInt("_LushLODTreeDisableFarDithering", 1);
		Shader.SetGlobalInt("_LushLODTreeDisableHQ", (_LushLODTree.TreesManager.BillBoardSetting_PREVIOUS != _LushLODTreesManager.BillBoardSettings.BillboardsOnly) ? 1 : 0);
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x001A6A80 File Offset: 0x001A4C80
	private void Start()
	{
		this.DontHogSoMuchMemory();
		this.MakeNonStencilPixelsOpaque = new Material(Shader.Find("Hidden/LushLODTree/MakeNonStencilPixelsOpaque"));
		this.SmoothDitheringPostProcess = new Material(Shader.Find("Hidden/LushLODTree/SmoothTheDithering"));
		this.BlitCopy = new Material(Shader.Find("Hidden/BlitCopy"));
		this.OriginalPixels = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		this.StencilPixels = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x001A6B04 File Offset: 0x001A4D04
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		bool flag = _LushLODTree.TreesManager != null && _LushLODTree.TreesManager.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra;
		if (flag)
		{
			Graphics.Blit(src, this.OriginalPixels, this.BlitCopy);
			Shader.SetGlobalTexture("_LushLODOriginalPixels", this.OriginalPixels);
			Graphics.Blit(src, this.StencilPixels, this.MakeNonStencilPixelsOpaque);
			Graphics.Blit(this.StencilPixels, dest, this.SmoothDitheringPostProcess);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}

	// Token: 0x04002908 RID: 10504
	private Material SmoothDitheringPostProcess;

	// Token: 0x04002909 RID: 10505
	private Material MakeNonStencilPixelsOpaque;

	// Token: 0x0400290A RID: 10506
	private Material BlitCopy;

	// Token: 0x0400290B RID: 10507
	private RenderTexture OriginalPixels;

	// Token: 0x0400290C RID: 10508
	private RenderTexture StencilPixels;
}

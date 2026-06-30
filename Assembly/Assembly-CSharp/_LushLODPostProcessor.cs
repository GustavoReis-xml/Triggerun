using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020004D4 RID: 1236
public class _LushLODPostProcessor : MonoBehaviour
{
	// Token: 0x06002F55 RID: 12117 RVA: 0x001A66B4 File Offset: 0x001A48B4
	private void OnPreRender()
	{
		Shader.SetGlobalInt("_LushLODTreeDisableFarDithering", 1);
		Shader.SetGlobalInt("_LushLODTreeDisableHQ", (_LushLODTree.TreesManager.BillBoardSetting_PREVIOUS != _LushLODTreesManager.BillBoardSettings.BillboardsOnly) ? 1 : 0);
		bool flag = this.cmdBuffer == null && _LushLODTree.TreesManager != null && _LushLODTree.TreesManager.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra;
		if (flag)
		{
			this.OnEnable2();
		}
		else
		{
			bool flag2 = this.cmdBuffer != null && _LushLODTree.TreesManager != null && _LushLODTree.TreesManager.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra;
			if (flag2)
			{
				this.OnDisable();
			}
		}
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x001A6754 File Offset: 0x001A4954
	private void OnEnable2()
	{
		base.CancelInvoke("OnEnable2");
		bool flag = this.cmdBuffer == null;
		if (flag)
		{
			this.ThisCam = base.GetComponent<Camera>();
			this.MakeNonStencilPixelsOpaque = new Material(Shader.Find("Hidden/LushLODTree/MakeNonStencilPixelsOpaque"));
			this.SmoothDitheringPostProcess = new Material(Shader.Find("Hidden/LushLODTree/SmoothTheDithering"));
			this.cmdBuffer = new CommandBuffer();
			this.cmdBuffer.name = "cmdBuffer";
			int nameID = Shader.PropertyToID("_LushLODOriginalPixels");
			this.cmdBuffer.GetTemporaryRT(nameID, -1, -1, 24);
			int nameID2 = Shader.PropertyToID("_LushLODStencilPixels");
			this.cmdBuffer.GetTemporaryRT(nameID2, -1, -1, 24);
			this.cmdBuffer.Blit(BuiltinRenderTextureType.CameraTarget, nameID);
			this.cmdBuffer.Blit(nameID, BuiltinRenderTextureType.CameraTarget, this.MakeNonStencilPixelsOpaque);
			this.cmdBuffer.Blit(BuiltinRenderTextureType.CameraTarget, nameID2);
			this.cmdBuffer.Blit(nameID2, BuiltinRenderTextureType.CameraTarget, this.SmoothDitheringPostProcess);
			this.ThisCam.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.cmdBuffer);
		}
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x001A688A File Offset: 0x001A4A8A
	private void OnEnable()
	{
		base.InvokeRepeating("OnEnable2", 1f, 99f);
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x001A68A4 File Offset: 0x001A4AA4
	private void OnDisable()
	{
		base.CancelInvoke("OnEnable2");
		bool flag = this.ThisCam != null && this.ThisCam != null && this.cmdBuffer != null;
		if (flag)
		{
			this.ThisCam.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.cmdBuffer);
		}
		this.cmdBuffer = null;
	}

	// Token: 0x04002903 RID: 10499
	private CommandBuffer cmdBuffer;

	// Token: 0x04002904 RID: 10500
	private Material MakeNonStencilPixelsOpaque;

	// Token: 0x04002905 RID: 10501
	private Material SmoothDitheringPostProcess;

	// Token: 0x04002906 RID: 10502
	private Camera ThisCam;
}

using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000034 RID: 52
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GL/Brightness Effect")]
public class BrightnessEffect : ImageEffectBase
{
	// Token: 0x060001E4 RID: 484 RVA: 0x0000C534 File Offset: 0x0000A734
	protected override void Start()
	{
		this.shader = Shader.Find("Hidden/GL/Brightness Effect");
		base.Start();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000C54E File Offset: 0x0000A74E
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Brightness", this._Brightness);
		base.material.SetFloat("_Contrast", this._Contrast);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000C58D File Offset: 0x0000A78D
	public void SetBrightness(float value)
	{
		this._Brightness = value;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000C597 File Offset: 0x0000A797
	public void SetContrast(float value)
	{
		this._Contrast = value;
	}

	// Token: 0x0400018B RID: 395
	[Range(0.5f, 2f)]
	public float _Brightness = 1f;

	// Token: 0x0400018C RID: 396
	[Range(0.5f, 2f)]
	public float _Contrast = 1f;

	// Token: 0x0400018D RID: 397
	private Material curMaterial;
}

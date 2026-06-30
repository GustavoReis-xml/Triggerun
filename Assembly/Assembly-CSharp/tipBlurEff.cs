using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047A RID: 1146
public class tipBlurEff : MonoBehaviour
{
	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x06002C76 RID: 11382 RVA: 0x00186FDC File Offset: 0x001851DC
	protected Material material
	{
		get
		{
			bool flag = tipBlurEff.m_Material == null;
			if (flag)
			{
				tipBlurEff.m_Material = new Material(this.blurShader);
				tipBlurEff.m_Material.hideFlags = HideFlags.DontSave;
			}
			return tipBlurEff.m_Material;
		}
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x00187024 File Offset: 0x00185224
	protected void OnDisable()
	{
		bool flag = tipBlurEff.m_Material;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(tipBlurEff.m_Material);
		}
		this.rawImage = null;
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x00187054 File Offset: 0x00185254
	protected void Start()
	{
		bool flag = !SystemInfo.supportsImageEffects;
		if (flag)
		{
			base.enabled = false;
		}
		else
		{
			this.blurShader = Shader.Find("Hidden/BlurEffectConeTap");
			bool flag2 = !this.blurShader || !this.material.shader.isSupported;
			if (flag2)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x001870BC File Offset: 0x001852BC
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(0f - num, 0f - num),
			new Vector2(0f - num, num),
			new Vector2(num, num),
			new Vector2(num, 0f - num)
		});
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x00187140 File Offset: 0x00185340
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(0f - num, 0f - num),
			new Vector2(0f - num, num),
			new Vector2(num, num),
			new Vector2(num, 0f - num)
		});
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x001871B8 File Offset: 0x001853B8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		bool flag = this.rawImage != null;
		if (flag)
		{
			int width = source.width / 4;
			int height = source.height / 4;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0);
			this.DownSample4x(source, renderTexture);
			for (int i = 0; i < this.iterations; i++)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
				this.FourTapCone(renderTexture, temporary, i);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destination);
			this.rawImage.texture = renderTexture;
			base.enabled = false;
		}
	}

	// Token: 0x040025CD RID: 9677
	[Range(0f, 10f)]
	public int iterations = 3;

	// Token: 0x040025CE RID: 9678
	[Range(0f, 1f)]
	public float blurSpread;

	// Token: 0x040025CF RID: 9679
	public Shader blurShader;

	// Token: 0x040025D0 RID: 9680
	private static Material m_Material;

	// Token: 0x040025D1 RID: 9681
	public RawImage rawImage;
}

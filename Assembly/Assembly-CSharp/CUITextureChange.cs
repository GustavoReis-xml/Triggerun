using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E3 RID: 227
public class CUITextureChange : MonoBehaviour
{
	// Token: 0x060007EE RID: 2030 RVA: 0x00044DEC File Offset: 0x00042FEC
	private void Update()
	{
		bool flag = this.reset;
		if (flag)
		{
			this.reset = false;
			this.Apply();
		}
		bool flag2 = this.rateLast != this.rate;
		if (flag2)
		{
			this.rateLast = this.rate;
			this.MaskRate(this.rate);
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00044E44 File Offset: 0x00043044
	public bool MaskRate(float rate)
	{
		bool flag = this.mat == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			float x = Mathf.Lerp(0f, this.maxOffset, rate);
			Vector2 offset = new Vector2(x, 0f);
			this.mat.SetTextureOffset("_MainTex", offset);
			result = true;
		}
		return result;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00044E9D File Offset: 0x0004309D
	public void Apply()
	{
		this.Changed(this.Grid2, this.OffsetSub);
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00044EB4 File Offset: 0x000430B4
	public void InitTexture(Image _image)
	{
		this.img = _image;
		this.mat = this.img.material;
		this.textureMask = (Texture2D)this.mat.GetTexture("_Mask");
		this.textureMain = (Texture2D)this.mat.GetTexture("_MainTex");
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00044F10 File Offset: 0x00043110
	public void Changed(Vector2 grid, float _OffsetSub)
	{
		this.Grid2 = grid;
		this.OffsetSub = _OffsetSub;
		RectTransform component = base.GetComponent<RectTransform>();
		this.imageSize = component.sizeDelta;
		this.bili = this.imageSize.x / this.imageSize.y;
		Texture2D texture2D = (Texture2D)this.img.material.GetTexture("_Mask");
		Texture2D texture2D2 = this.createNew(this.textureMask);
		this.img.material.SetTexture("_Mask", texture2D2);
		bool flag = texture2D != this.textureMask;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(texture2D, true);
		}
		this.maxOffset = 1f - this.OffsetSub / (float)texture2D2.width;
		Texture2D texture2D3 = (Texture2D)this.img.material.GetTexture("_MainTex");
		Texture2D texture = this.createNew(this.textureMain);
		this.img.material.SetTexture("_MainTex", texture);
		bool flag2 = texture2D3 != this.textureMain;
		if (flag2)
		{
			UnityEngine.Object.DestroyImmediate(texture2D3, true);
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00045030 File Offset: 0x00043230
	private Texture2D createNew(Texture2D maskText)
	{
		int width = (int)(this.bili * (float)maskText.height);
		Texture2D texture2D = new Texture2D(width, maskText.height);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		Color pixel = maskText.GetPixel(maskText.width / 2, maskText.height / 2);
		int num = texture2D.width - (int)this.Grid2.y;
		int num2 = maskText.width - (int)this.Grid2.y;
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				texture2D.SetPixel(i, j, pixel);
				bool flag = (float)i < this.Grid2.x;
				if (flag)
				{
					texture2D.SetPixel(i, j, maskText.GetPixel(i, j));
				}
				else
				{
					bool flag2 = i >= num;
					if (flag2)
					{
						texture2D.SetPixel(i, j, maskText.GetPixel(num2 + (i - num), j));
					}
				}
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x040005E2 RID: 1506
	private Vector2 imageSize;

	// Token: 0x040005E3 RID: 1507
	private Material mat;

	// Token: 0x040005E4 RID: 1508
	private Vector2 Grid2 = new Vector4(64f, 65f);

	// Token: 0x040005E5 RID: 1509
	private float bili;

	// Token: 0x040005E6 RID: 1510
	private Image img;

	// Token: 0x040005E7 RID: 1511
	private float maxOffset = 0.84375f;

	// Token: 0x040005E8 RID: 1512
	private float OffsetSub = 20f;

	// Token: 0x040005E9 RID: 1513
	public bool reset;

	// Token: 0x040005EA RID: 1514
	public float rate;

	// Token: 0x040005EB RID: 1515
	private float rateLast;

	// Token: 0x040005EC RID: 1516
	private Texture2D textureMask;

	// Token: 0x040005ED RID: 1517
	private Texture2D textureMain;
}

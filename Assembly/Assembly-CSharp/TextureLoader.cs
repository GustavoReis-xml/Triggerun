using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000474 RID: 1140
public class TextureLoader
{
	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x06002C69 RID: 11369 RVA: 0x00186B99 File Offset: 0x00184D99
	public Texture loadingImage
	{
		get
		{
			return this._loadingTexture;
		}
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x06002C6A RID: 11370 RVA: 0x00186BA4 File Offset: 0x00184DA4
	public static TextureLoader instance
	{
		get
		{
			bool flag = TextureLoader._instance == null;
			if (flag)
			{
				TextureLoader._instance = new TextureLoader();
			}
			return TextureLoader._instance;
		}
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x00186BD3 File Offset: 0x00184DD3
	private TextureLoader()
	{
		this._loadingTexture = (Resources.Load("Texture/loading") as Texture);
		this._imageDatas = new Dictionary<string, Texture>();
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x00186C00 File Offset: 0x00184E00
	public void load(string url, GameObject gameobject, bool useSize = false)
	{
		RawImage component = gameobject.GetComponent<RawImage>();
		RectTransform component2 = gameobject.GetComponent<RectTransform>();
		Vector2 sizeDelta = component2.sizeDelta;
		bool flag = this._imageDatas.ContainsKey(url);
		if (flag)
		{
			Texture texture = this._imageDatas[url];
			component.texture = texture;
		}
		else
		{
			ImageData imageData = new ImageData();
			imageData.size = component2.sizeDelta;
			imageData.rawImage = component;
			imageData.rtf = component2;
			imageData.url = url;
			component.texture = this._loadingTexture;
			int num = (int)(Mathf.Min(sizeDelta.x, sizeDelta.y) * 0.7f);
			component2.sizeDelta = new Vector2((float)num, (float)num);
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this._loadComplete), imageData);
		}
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x00186CC4 File Offset: 0x00184EC4
	private void _loadComplete(WWW www, object customParam)
	{
		bool flag = www.error == null;
		if (flag)
		{
			ImageData imageData = (ImageData)customParam;
			Debug.LogError("注意:这里内存泄露");
			Texture texture = www.texture;
			bool flag2 = !this._imageDatas.ContainsKey(imageData.url);
			if (flag2)
			{
				this._imageDatas.Add(imageData.url, texture);
			}
			imageData.rawImage.texture = texture;
			imageData.rtf.sizeDelta = imageData.size;
		}
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x00186D46 File Offset: 0x00184F46
	private void show(RawImage image, Texture texture)
	{
		image.texture = texture;
		Debug.LogError("内存泄露");
	}

	// Token: 0x040025CA RID: 9674
	private static TextureLoader _instance;

	// Token: 0x040025CB RID: 9675
	private Texture _loadingTexture;

	// Token: 0x040025CC RID: 9676
	private Dictionary<string, Texture> _imageDatas;
}

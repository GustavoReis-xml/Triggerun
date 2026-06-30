using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002B RID: 43
public class BackgroundUICon : Window
{
	// Token: 0x06000188 RID: 392 RVA: 0x0000898C File Offset: 0x00006B8C
	protected override void Awake()
	{
		base.Awake();
		this.mainBgSp = this.UIGameObj.Get_GameObj(0).GetComponent<Image>();
		this.particleObjs.Add(this.UIGameObj.Get_GameObj(1));
		this.mainIconBg = this.UIGameObj.Get_GameObj(2).GetComponent<Image>();
		BackgroundUICon.inst = this;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000089ED File Offset: 0x00006BED
	private void Start()
	{
		this.SetMainBackground(BackgroundUICon.isShowBack, string.Empty);
		this.SetGameQuality();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00008A08 File Offset: 0x00006C08
	private void SetGameQuality()
	{
		bool flag = GameRoot.inst.Setting.Video_Class.Effect_Level == 0;
		if (flag)
		{
			for (int i = 0; i < this.particleObjs.Count; i++)
			{
				this.particleObjs[i].SetActive(false);
			}
		}
		else
		{
			for (int j = 0; j < this.particleObjs.Count; j++)
			{
				this.particleObjs[j].SetActive(true);
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00008A97 File Offset: 0x00006C97
	public void Update()
	{
		this.mainBgSp.material.mainTextureOffset += new Vector2(Time.deltaTime * this.moveSpeed * 0.02f, 0f);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00008AD2 File Offset: 0x00006CD2
	public override void Reset()
	{
		base.Reset();
		this.SetGameQuality();
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00008AE4 File Offset: 0x00006CE4
	public void ShowIconImg(Sprite sp = null)
	{
		bool flag = sp == null;
		if (flag)
		{
			this.mainIconBg.enabled = false;
			this.mainBgSp.gameObject.SetActive(true);
		}
		else
		{
			this.mainIconBg.sprite = sp;
			this.mainIconBg.enabled = true;
			this.mainBgSp.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00008B50 File Offset: 0x00006D50
	public void SetMainBackground(bool isShow, string iconname = "")
	{
		if (isShow)
		{
			bool flag = !string.IsNullOrEmpty(iconname);
			if (flag)
			{
				GameRoot.LoadAsset("Icon/" + iconname, new fResourceManager.DownFinishDelegate(this.LoadBgIconOk), iconname);
			}
			else
			{
				this.mainBgSp.gameObject.SetActive(true);
			}
		}
		else
		{
			this.mainBgSp.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00008BC0 File Offset: 0x00006DC0
	private void LoadBgIconOk(WWW www, object customParam)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.mainBgSp.material.mainTexture = null;
			UnityEngine.Object.DestroyImmediate(this.mainBgSp.material.mainTexture);
			bool flag2 = this.mainBgSp.material.mainTexture == null;
			if (flag2)
			{
				this.mainBgSp.material.mainTexture = new Texture2D(4, 4);
				www.LoadImageIntoTexture((Texture2D)this.mainBgSp.material.mainTexture);
			}
			this.mainBgSp.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000122 RID: 290
	public static BackgroundUICon inst;

	// Token: 0x04000123 RID: 291
	public static bool isShowBack = true;

	// Token: 0x04000124 RID: 292
	private Image mainBgSp;

	// Token: 0x04000125 RID: 293
	private Image mainIconBg;

	// Token: 0x04000126 RID: 294
	public float moveSpeed = -1f;

	// Token: 0x04000127 RID: 295
	private List<GameObject> particleObjs = new List<GameObject>();
}

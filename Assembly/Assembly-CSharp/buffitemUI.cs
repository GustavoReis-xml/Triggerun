using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003D RID: 61
public class buffitemUI : MonoBehaviour
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000223 RID: 547 RVA: 0x0000EA67 File Offset: 0x0000CC67
	public GameObject Gui
	{
		get
		{
			return this._GUi;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000224 RID: 548 RVA: 0x0000EA70 File Offset: 0x0000CC70
	// (set) Token: 0x06000225 RID: 549 RVA: 0x0000EA88 File Offset: 0x0000CC88
	public Canvas GuiCanvas
	{
		get
		{
			return this._GuiCanvas;
		}
		set
		{
			this._GuiCanvas = value;
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000EA92 File Offset: 0x0000CC92
	public virtual void SetUI(Transform tf)
	{
		this.initBuffUi(tf);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
	private void initBuffUi(Transform tf)
	{
		bool flag = this.Gui != null;
		if (flag)
		{
			this.Gui.transform.SetParent(tf);
			this.Gui.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			this.Gui.transform.localPosition = new Vector3(0f, 0.4f, 0f);
		}
		else
		{
			string url = string.Format("EffectRes/{0}.ef", "buffitemui");
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), tf);
			}
			else
			{
				this.loadReayPrefab(resWWW, tf);
			}
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000EB60 File Offset: 0x0000CD60
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.obuffitemuiperfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this._GUi = (GameObject)UnityEngine.Object.Instantiate(this.obuffitemuiperfab);
			this.imageFill = this._GUi.transform.FindChild("fill").GetComponent<Image>();
			this._GUi.SetActive(false);
			Transform parent = (Transform)sStyle;
			this._GUi.transform.SetParent(parent);
			this._GUi.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			this._GUi.transform.localPosition = new Vector3(0f, 0.4f, 0f);
			this.GuiCanvas = this._GUi.GetComponent<Canvas>();
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000EC5C File Offset: 0x0000CE5C
	public void setImageFill(float fill)
	{
		this.imageFill.fillAmount = fill;
	}

	// Token: 0x040001C0 RID: 448
	private GameObject _GUi;

	// Token: 0x040001C1 RID: 449
	private Canvas _GuiCanvas;

	// Token: 0x040001C2 RID: 450
	private Image imageFill;

	// Token: 0x040001C3 RID: 451
	private UnityEngine.Object obuffitemuiperfab;
}

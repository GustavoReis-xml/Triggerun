using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public class Window : MonoBehaviour
{
	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x06002DED RID: 11757 RVA: 0x001962F8 File Offset: 0x001944F8
	// (set) Token: 0x06002DEE RID: 11758 RVA: 0x00196310 File Offset: 0x00194510
	public virtual bool visible
	{
		get
		{
			return this._visible;
		}
		set
		{
			this._visible = value;
		}
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x0019631A File Offset: 0x0019451A
	protected virtual void Awake()
	{
		this.UIGameObj = base.GetComponent<UIGameObjList>();
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06002DF0 RID: 11760 RVA: 0x00196334 File Offset: 0x00194534
	public void init(string _sUI)
	{
		this.sUI = _sUI;
		this.loadResoure();
		this.OnOpen();
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x0019634C File Offset: 0x0019454C
	public virtual void Reset()
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x0019635C File Offset: 0x0019455C
	public void MoveToTop()
	{
		int num = 0;
		foreach (object obj in base.transform.parent.transform)
		{
			Transform transform = (Transform)obj;
			num = Mathf.Max(num, transform.GetSiblingIndex());
		}
		base.transform.SetSiblingIndex(num);
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x001963DC File Offset: 0x001945DC
	public virtual bool CanOpen()
	{
		return true;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x001963F0 File Offset: 0x001945F0
	public void Open()
	{
		bool flag = this.CanOpen();
		if (flag)
		{
			this.OnOpen();
		}
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x00196414 File Offset: 0x00194614
	protected virtual void OnOpen()
	{
		bool flag = !this.visible;
		if (flag)
		{
			this.OnMauiOpen();
			this.SetVisible(true);
			this.MoveToTop();
		}
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x00196447 File Offset: 0x00194647
	public virtual void OnMauiOpen()
	{
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x0019644C File Offset: 0x0019464C
	public static Sprite spriteName(string atlasName, string imageName)
	{
		atlasName += ".ab";
		atlasName = atlasName.ToLower();
		bool flag = Window.windowAtlas.ContainsKey(atlasName);
		Sprite result;
		if (flag)
		{
			AssetBundle assetBundle = Window.windowAtlas[atlasName];
			UnityEngine.Object @object = assetBundle.LoadAsset(imageName, typeof(Sprite));
			result = (@object as Sprite);
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x001964AC File Offset: 0x001946AC
	public virtual void Close()
	{
		bool flag = this && this.visible;
		if (flag)
		{
			this.SetVisible(false);
		}
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x001964DC File Offset: 0x001946DC
	protected void SetVisible(bool visible)
	{
		bool flag = this.visible != visible;
		if (flag)
		{
			this.visible = visible;
			base.gameObject.SetActive(visible);
		}
	}

	// Token: 0x06002DFA RID: 11770 RVA: 0x00196514 File Offset: 0x00194714
	public virtual void DestroyThis()
	{
		bool flag = base.gameObject != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x06002DFB RID: 11771 RVA: 0x00196540 File Offset: 0x00194740
	private void loadResoure()
	{
		AssetBundleManifest assetBundleManifest = GameRoot.mUIAssetManager.m_AssetBundleManifest;
		bool flag = !(assetBundleManifest != null);
		if (!flag)
		{
			string str = (!(this.sUI == "MainUI") || !(WebParam.client_language == "baxi")) ? this.sUI : "MainUI_baxi";
			string[] allDependencies = assetBundleManifest.GetAllDependencies(str + ".ab");
			string[] array = allDependencies;
			foreach (string text in array)
			{
				bool flag2 = text.Contains(".prefab");
				if (!flag2)
				{
					WWW resWWW = GameRoot.mUIAssetManager.GetResWWW(text);
					bool flag3 = resWWW != null;
					if (flag3)
					{
						AssetBundle assetBundle = resWWW.assetBundle;
						bool flag4 = !Window.windowAtlas.ContainsKey(text);
						if (flag4)
						{
							Window.windowAtlas.Add(text, assetBundle);
						}
					}
				}
			}
		}
	}

	// Token: 0x0400274F RID: 10063
	public string sUI = string.Empty;

	// Token: 0x04002750 RID: 10064
	public UIGameObjList UIGameObj;

	// Token: 0x04002751 RID: 10065
	private static Dictionary<string, AssetBundle> windowAtlas = new Dictionary<string, AssetBundle>();

	// Token: 0x04002752 RID: 10066
	private bool _visible;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000074 RID: 116
public class CCOpyNameManager : MonoBehaviour
{
	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001DB9C File Offset: 0x0001BD9C
	// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001DBB3 File Offset: 0x0001BDB3
	public static CCOpyNameManager Instance
	{
		get
		{
			return CCOpyNameManager._instance;
		}
		set
		{
			CCOpyNameManager._instance = value;
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001DBBC File Offset: 0x0001BDBC
	private Camera cloneCamera
	{
		get
		{
			return this._cloneCamera;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
	private void Start()
	{
		CCOpyNameManager.Instance = this;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001DBCE File Offset: 0x0001BDCE
	public void InitCopyName()
	{
		this.loadPrefab();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
	public void GetName(NetLiving net)
	{
		bool flag = !this.netList.Contains(net);
		if (flag)
		{
			this.netList.Add(net);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0001DC08 File Offset: 0x0001BE08
	public void removeGetName(NetLiving net)
	{
		bool flag = this.netList.Contains(net);
		if (flag)
		{
			this.netList.Remove(net);
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001DC38 File Offset: 0x0001BE38
	private void loadPrefab()
	{
		string url = string.Format("EffectRes/{0}.ef", "clonename");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadNameObjPrefab), 0);
		}
		else
		{
			this.LoadNameObjPrefab(resWWW, 0);
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001DC94 File Offset: 0x0001BE94
	private void LoadNameObjPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.nameObj = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this.nameObj.name = "copyname_root";
			this._cloneCamera = this.nameObj.GetComponentInChildren<Camera>(true);
			this.nameObj.transform.position = new Vector3(0f, -10000f, -10000f);
			GameObject gameObject = this.nameObj.transform.FindChild("body/Canvas").gameObject;
			this._TextName = gameObject.transform.FindChild("top/clonename").GetComponent<Text>();
			this._lzList = gameObject.transform.Find("top/lz").gameObject;
			this._lzlzimg = gameObject.transform.Find("lzimg").gameObject;
			this._cloneCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			this._cloneCamera.clearFlags = CameraClearFlags.Color;
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
	private void Update()
	{
		bool flag = !(CameraControl.Instance == null) && fGameManager.Heroplayer != null && CameraControl.Instance.camerHero != null && this.netList.Count != 0 && !(this._cloneCamera == null) && !this.doing;
		if (flag)
		{
			NetLiving net = this.netList[0];
			this.netList.RemoveAt(0);
			this.doing = true;
			base.StopAllCoroutines();
			base.StartCoroutine(this.DoIt(net));
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0001DE59 File Offset: 0x0001C059
	private IEnumerator DoIt(NetLiving net)
	{
		yield return 0;
		bool flag = net == null || net.haveDestroy || !net.playerHaveCreate;
		if (flag)
		{
			this.doing = false;
			yield break;
		}
		yield return new WaitForEndOfFrame();
		this.copyNameToTexture(net);
		yield return 0;
		this.doing = false;
		yield break;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001DE70 File Offset: 0x0001C070
	private void copyNameToTexture(NetLiving net)
	{
		bool flag = false;
		bool isCopy = net.isCopy;
		if (isCopy)
		{
			bool flag2 = CFilmManager.Instance.filmForce(net) != net.playerCommon.m_nShiliServer;
			if (flag2)
			{
				flag = true;
			}
		}
		else
		{
			bool flag3 = fGameManager.Heroplayer.playerCommon.m_nShiliServer != net.playerCommon.m_nShiliServer;
			if (flag3)
			{
				flag = true;
			}
		}
		bool flag4 = !flag;
		if (flag4)
		{
			this._TextName.text = "<color='#00aedd'>" + net.playerCommon.m_sName + "</color>";
		}
		else
		{
			this._TextName.text = "<color='#ff0000'>" + net.playerCommon.m_sName + "</color>";
		}
		this._TextName.fontStyle = FontStyle.Normal;
		this._TextName.lineSpacing *= 0.7f;
		this.OnLzIcoData(net.playerCommon);
		Texture2D name = this.copytext();
		net.newplayercon.setName(name);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001DF7C File Offset: 0x0001C17C
	private Texture2D copytext()
	{
		int num = (int)((float)Screen.width * 0.5f);
		int num2 = (int)((float)Screen.height * 0.5f);
		int num3 = this.picSize;
		RenderTexture renderTexture = new RenderTexture(num, num2, 24);
		this.cloneCamera.targetTexture = renderTexture;
		bool flag = num < this.picSize || num2 < this.picSize;
		if (flag)
		{
			num3 = ((num >= num2) ? num2 : num);
		}
		Texture2D texture2D = new Texture2D(num3, num3, TextureFormat.ARGB32, false);
		this.cloneCamera.Render();
		RenderTexture.active = renderTexture;
		int num4 = num / 2 - num3 / 2;
		int num5 = num2 / 2 - num3 / 2;
		Rect source = new Rect((float)num4, (float)num5, (float)num3, (float)num3);
		texture2D.ReadPixels(source, 0, 0, false);
		texture2D.Apply();
		UnityEngine.Object.Destroy(renderTexture);
		this.cloneCamera.targetTexture = null;
		RenderTexture.active = null;
		return texture2D;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0001E064 File Offset: 0x0001C264
	private void OnLzIcoData(PlayerCommon playercom)
	{
		string text = string.Empty;
		Fun.ClearChildObejct(this._lzList);
		text = Fun.GetOtherLzImgName(playercom);
		bool flag = !string.IsNullOrEmpty(text);
		if (flag)
		{
			this.AddLzIco(text);
		}
		text = Fun.GetOtherLzNianImgName(playercom);
		bool flag2 = !string.IsNullOrEmpty(text);
		if (flag2)
		{
			this.AddLzIco(text);
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
	private void AddLzIco(string imgname)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._lzlzimg);
		gameObject.transform.SetParent(this._lzList.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, imgname);
		gameObject.SetActive(true);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001E144 File Offset: 0x0001C344
	public Texture2D GetNameText2D(string sName)
	{
		bool flag = this._TextName == null;
		Texture2D result;
		if (flag)
		{
			result = null;
		}
		else
		{
			this._TextName.text = sName;
			this._TextName.fontStyle = FontStyle.Normal;
			this._TextName.lineSpacing *= 0.7f;
			Fun.ClearChildObejct(this._lzList);
			result = this.copytext();
		}
		return result;
	}

	// Token: 0x04000307 RID: 775
	private static CCOpyNameManager _instance;

	// Token: 0x04000308 RID: 776
	private List<NetLiving> netList = new List<NetLiving>();

	// Token: 0x04000309 RID: 777
	private bool doing;

	// Token: 0x0400030A RID: 778
	private Camera _cloneCamera;

	// Token: 0x0400030B RID: 779
	private GameObject nameObj;

	// Token: 0x0400030C RID: 780
	private Text _TextName;

	// Token: 0x0400030D RID: 781
	private GameObject _lzList;

	// Token: 0x0400030E RID: 782
	private GameObject _lzlzimg;

	// Token: 0x0400030F RID: 783
	private int picSize = 256;
}

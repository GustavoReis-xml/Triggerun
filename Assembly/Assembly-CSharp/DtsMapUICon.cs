using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000106 RID: 262
public class DtsMapUICon : Window
{
	// Token: 0x06000943 RID: 2371 RVA: 0x0004F1C4 File Offset: 0x0004D3C4
	protected override void Awake()
	{
		base.Awake();
		this.MaxMap = base.transform.Find("MaxMap").gameObject;
		this.MaxMask = this.MaxMap.transform.Find("Mask").gameObject;
		this.MapRaw = this.MaxMap.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
		this.MaxMapDuQuan = this.MaxMask.transform.Find("DuQuan").gameObject;
		this.MaxMapDuQuan.SetActive(false);
		this.MaxMapDuQuanEff = this.MaxMap.transform.Find("DuQuan_n").gameObject;
		this.MaxMapDuQuanEff.SetActive(false);
		this.MaxMapSafeQuan = this.MaxMask.transform.Find("SafeQuan").gameObject;
		this.MaxMapSafeQuan.SetActive(false);
		this.MaxMapSafeQuanEff = this.MaxMap.transform.Find("SafeQuan_n").gameObject;
		this.MaxMapSafeQuanEff.SetActive(false);
		this.MaxMapsign = this.MaxMask.transform.Find("sign").gameObject;
		this.xcText = this.MaxMap.transform.Find("xctext").GetComponent<Text>();
		this.jsText = this.MaxMap.transform.Find("jstext").GetComponent<Text>();
		this.MaxMap.SetActive(false);
		this.MixMap = base.transform.Find("MixMap").gameObject;
		this.minimapTexture = this.MixMap.transform.Find("Quad").GetComponent<MeshRenderer>();
		this.minimapTexture.enabled = false;
		this.MixMovepanel = this.MixMap.transform.Find("Mask/movepanel").gameObject;
		this.MixMovequan = this.MixMap.transform.Find("quan").gameObject;
		this.MixMapRaw = this.MixMovepanel.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
		this.MixMapsign = this.MixMovepanel.transform.Find("sign").gameObject;
		this.MixMapsign.SetActive(false);
		this.MixMapDuQuan = this.MixMovepanel.transform.Find("MiniDuQuan").gameObject;
		this.MixMapDuQuan.SetActive(false);
		this.MixMapDuQuanEff = this.MixMovequan.transform.Find("DuQuan_n").gameObject;
		this.MixMapDuQuanEff.SetActive(false);
		this.MixMapSafeQuan = this.MixMovepanel.transform.Find("MiniSafe").gameObject;
		this.MixMapSafeQuan.SetActive(false);
		this.MixMapSafeQuanEff = this.MixMovequan.transform.Find("SafeQuan_n").gameObject;
		this.MixMapSafeQuanEff.SetActive(false);
		this.MixMap.SetActive(false);
		this.bMapLoadOk = false;
		this.GetPlayerModel();
		DtsMapUICon.inst = this;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0004F50C File Offset: 0x0004D70C
	private void Update()
	{
		bool flag = this._NetLiving != null && this.pDtsUser != null;
		if (flag)
		{
			this.CheckTabUI();
			this.CheckMaxMap();
			this.CheckMixMap();
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0004F54C File Offset: 0x0004D74C
	private void CheckTabUI()
	{
		bool flag = Input_Manager.Get_Button_Down("Score_Window") || (MatchManager.GetInstance().m_nMatchType == 4 && Input.GetKeyDown(KeyCode.M));
		if (flag)
		{
			bool flag2 = !this.bMapLoadOk;
			if (flag2)
			{
				bool activeSelf = this.MixMap.activeSelf;
				if (activeSelf)
				{
					this.MixMap.SetActive(false);
				}
				bool activeSelf2 = this.MaxMap.activeSelf;
				if (activeSelf2)
				{
					this.MaxMap.SetActive(false);
				}
				this.GetPlayerModel();
				Fun.Log("大逃杀tab键地图还没有下载完成--" + this.bMapLoadOk.ToString(), null);
				return;
			}
			bool flag3 = DtsUICon.Inst != null && DtsUICon.Inst.visible && DtsUICon.Inst.DtsResultUI.activeSelf;
			if (flag3)
			{
				return;
			}
			bool activeSelf3 = this.MixMap.activeSelf;
			if (activeSelf3)
			{
				this.MixMap.SetActive(false);
			}
			bool flag4 = !this.MaxMap.activeSelf;
			if (flag4)
			{
				this.MaxMap.SetActive(true);
			}
			this.jsText.text = Language.GetLang(29038, new object[]
			{
				this.pDtsUser.m_nKill
			});
			this.xcText.text = Language.GetLang(29037, new object[]
			{
				DtsBatteManager.GetInstance().m_nLivePlayerNum
			});
		}
		bool flag5 = (!Input_Manager.Get_Button_Up("Score_Window") && (MatchManager.GetInstance().m_nMatchType != 4 || !Input.GetKeyUp(KeyCode.M))) || !this.bMapLoadOk;
		if (!flag5)
		{
			bool activeSelf4 = this.MaxMap.activeSelf;
			if (activeSelf4)
			{
				this.MaxMap.SetActive(false);
			}
			bool flag6 = this.pDtsUser.m_nSelectScnID != 0;
			if (flag6)
			{
				bool flag7 = !this.MixMap.activeSelf;
				if (flag7)
				{
					this.MixMap.SetActive(true);
				}
			}
			else
			{
				bool activeSelf5 = this.MixMap.activeSelf;
				if (activeSelf5)
				{
					this.MixMap.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0004F784 File Offset: 0x0004D984
	public void SetData(NetLiving _Net, DtsUser dUser)
	{
		this._NetLiving = _Net;
		this.pDtsUser = dUser;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0004F798 File Offset: 0x0004D998
	public void GetPlayerModel()
	{
		bool flag = !this.bMapLoadOk;
		if (flag)
		{
			this.bMapLoadOk = false;
			string str = "dtsmap";
			string url = "scenemap/" + str + ".txt";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.miniMapLoadOk), 0);
			}
			else
			{
				this.miniMapLoadOk(resWWW, 0);
			}
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0004F814 File Offset: 0x0004DA14
	private void miniMapLoadOk(WWW www, object mapname)
	{
		this.MapRaw.material.mainTexture = null;
		UnityEngine.Object.DestroyImmediate(this.MapRaw.material.mainTexture);
		this.MapRaw.material.mainTexture = new Texture2D(4, 4);
		www.LoadImageIntoTexture((Texture2D)this.MapRaw.material.mainTexture);
		int width = this.MapRaw.material.mainTexture.width;
		int height = this.MapRaw.material.mainTexture.height;
		this.MapRaw.enabled = true;
		this.mapScaleRatioX = this.fMapMaskWidth / this.fMapScnWidth;
		this.mapScaleRatioY = this.fMapMaskHeight / this.fMapScnHeight;
		this.MixMapRaw.material = this.MapRaw.material;
		this.MixMapRaw.enabled = true;
		this.MixMap.SetActive(true);
		this.bMapLoadOk = true;
		Fun.Log("大逃杀tab键地图下载完成--" + this.bMapLoadOk.ToString(), null);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0004F934 File Offset: 0x0004DB34
	private void CheckMaxMap()
	{
		bool flag = this._NetLiving == null || this._NetLiving.haveDestroy || !this._NetLiving.playerHaveCreate || this._NetLiving.playerTarget == null || this._NetLiving.UserControl == null || !this.MaxMap.activeSelf || this.MaxMapsign == null;
		if (!flag)
		{
			this.MaxMapsign.transform.localPosition = this.GetNowPlayerPos();
			this.MaxMapsign.transform.localRotation = this.GetPlayerRotation();
			bool flag2 = QuanManager.GetInstance().GetDuQuan() != null && QuanManager.GetInstance().GetDuQuan().m_bShowState;
			if (flag2)
			{
				bool flag3 = !this.MaxMapDuQuan.activeSelf;
				if (flag3)
				{
					this.MaxMapDuQuan.SetActive(true);
				}
				bool flag4 = !this.MaxMapDuQuanEff.activeSelf;
				if (flag4)
				{
					this.MaxMapDuQuanEff.SetActive(true);
				}
				this.MaxMapDuQuan.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetEffPoint().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetEffPoint().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
				this.MaxMapDuQuanEff.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetEffPoint().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetEffPoint().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
				this.MaxMapDuQuan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(QuanManager.GetInstance().GetDuQuan().GetEffScale().x * this.mapScaleRatioX, QuanManager.GetInstance().GetDuQuan().GetEffScale().z * this.mapScaleRatioY);
				float num = QuanManager.GetInstance().GetDuQuan().GetEffScale().x * this.mapScaleRatioX * 0.024f;
				bool flag5 = num < 0.53f;
				if (flag5)
				{
					num = 0.53f;
				}
				float num2 = QuanManager.GetInstance().GetDuQuan().GetEffScale().z * this.mapScaleRatioY * 0.024f;
				bool flag6 = num2 < 0.53f;
				if (flag6)
				{
					num2 = 0.53f;
				}
				this.MaxMapDuQuanEff.transform.GetComponent<RectTransform>().localScale = new Vector3(num, num2, 0f);
				bool flag7 = !this.MaxMapSafeQuan.activeSelf;
				if (flag7)
				{
					this.MaxMapSafeQuan.SetActive(true);
				}
				bool flag8 = !this.MaxMapSafeQuanEff.activeSelf;
				if (flag8)
				{
					this.MaxMapSafeQuanEff.SetActive(true);
				}
				this.MaxMapSafeQuan.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeEff().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetSafeEff().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
				this.MaxMapSafeQuanEff.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeEff().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetSafeEff().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
				this.MaxMapSafeQuan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(QuanManager.GetInstance().GetDuQuan().GetSafeScale().x * this.mapScaleRatioX, QuanManager.GetInstance().GetDuQuan().GetSafeScale().z * this.mapScaleRatioY);
				this.MaxMapSafeQuanEff.transform.GetComponent<RectTransform>().localScale = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeScale().x * this.mapScaleRatioX * 0.0025f, QuanManager.GetInstance().GetDuQuan().GetSafeScale().z * this.mapScaleRatioY * 0.0025f, 0f);
			}
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
	private void CheckMixMap()
	{
		bool flag = this._NetLiving == null || this._NetLiving.haveDestroy || !this._NetLiving.playerHaveCreate || this._NetLiving.playerTarget == null || this._NetLiving.UserControl == null || this.MixMap == null || !this.bMapLoadOk;
		if (!flag)
		{
			bool flag2 = this.pDtsUser.m_nSelectScnID == 0;
			if (flag2)
			{
				bool activeSelf = this.MixMap.activeSelf;
				if (activeSelf)
				{
					this.MixMap.SetActive(false);
				}
			}
			else
			{
				Vector3 nowPlayerPos = this.GetNowPlayerPos();
				float num = nowPlayerPos.x;
				float num2 = nowPlayerPos.y;
				bool flag3 = num > this.fMapMaskWidth / 2f - this.fMinMaskWidth / 2f;
				if (flag3)
				{
					num = this.fMapMaskWidth / 2f - this.fMinMaskWidth / 2f;
				}
				bool flag4 = num < 0f - (this.fMapMaskWidth / 2f - this.fMinMaskWidth / 2f);
				if (flag4)
				{
					num = 0f - (this.fMapMaskWidth / 2f - this.fMinMaskWidth / 2f);
				}
				bool flag5 = num2 > this.fMapMaskHeight / 2f - this.fMinMaskHeight / 2f;
				if (flag5)
				{
					num2 = this.fMapMaskHeight / 2f - this.fMinMaskHeight / 2f;
				}
				bool flag6 = num2 < 0f - (this.fMapMaskHeight / 2f - this.fMinMaskHeight / 2f);
				if (flag6)
				{
					num2 = 0f - (this.fMapMaskHeight / 2f - this.fMinMaskHeight / 2f);
				}
				this.MixMovepanel.transform.localPosition = new Vector3(0f - num, 0f - num2, 0f);
				this.MixMovequan.transform.localPosition = new Vector3(0f - num, 0f - num2, 0f);
				this.MixMapsign.transform.localPosition = nowPlayerPos;
				this.MixMapsign.transform.localRotation = this.GetPlayerRotation();
				bool flag7 = !this.MixMapsign.activeSelf;
				if (flag7)
				{
					this.MixMapsign.SetActive(true);
				}
				bool flag8 = QuanManager.GetInstance().GetDuQuan() != null && QuanManager.GetInstance().GetDuQuan().m_bShowState;
				if (flag8)
				{
					bool flag9 = !this.MixMapDuQuan.activeSelf;
					if (flag9)
					{
						this.MixMapDuQuan.SetActive(true);
					}
					bool flag10 = !this.MixMapDuQuanEff.activeSelf;
					if (flag10)
					{
						this.MixMapDuQuanEff.SetActive(true);
					}
					this.MixMapDuQuan.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetEffPoint().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetEffPoint().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
					this.MixMapDuQuanEff.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetEffPoint().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetEffPoint().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
					this.MixMapDuQuan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(QuanManager.GetInstance().GetDuQuan().GetEffScale().x * this.mapScaleRatioX, QuanManager.GetInstance().GetDuQuan().GetEffScale().z * this.mapScaleRatioY);
					float num3 = QuanManager.GetInstance().GetDuQuan().GetEffScale().x * this.mapScaleRatioX * 0.024f;
					bool flag11 = num3 < 0.15f;
					if (flag11)
					{
						num3 = 0.15f;
					}
					float num4 = QuanManager.GetInstance().GetDuQuan().GetEffScale().z * this.mapScaleRatioY * 0.024f;
					bool flag12 = num4 < 0.12f;
					if (flag12)
					{
						num4 = 0.12f;
					}
					this.MixMapDuQuanEff.transform.GetComponent<RectTransform>().localScale = new Vector3(num3, num4, 0f);
					bool flag13 = !this.MixMapSafeQuan.activeSelf;
					if (flag13)
					{
						this.MixMapSafeQuan.SetActive(true);
					}
					bool flag14 = !this.MixMapSafeQuanEff.activeSelf;
					if (flag14)
					{
						this.MixMapSafeQuanEff.SetActive(true);
					}
					this.MixMapSafeQuan.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeEff().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetSafeEff().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
					this.MixMapSafeQuanEff.transform.localPosition = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeEff().x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, QuanManager.GetInstance().GetDuQuan().GetSafeEff().z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
					this.MixMapSafeQuan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(QuanManager.GetInstance().GetDuQuan().GetSafeScale().x * this.mapScaleRatioX, QuanManager.GetInstance().GetDuQuan().GetSafeScale().z * this.mapScaleRatioY);
					this.MixMapSafeQuanEff.transform.GetComponent<RectTransform>().localScale = new Vector3(QuanManager.GetInstance().GetDuQuan().GetSafeScale().x * this.mapScaleRatioX * 0.0025f, QuanManager.GetInstance().GetDuQuan().GetSafeScale().z * this.mapScaleRatioY * 0.0025f, 0f);
				}
				bool flag15 = !this.MixMap.activeSelf;
				if (flag15)
				{
					this.MixMap.SetActive(true);
				}
			}
		}
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00050470 File Offset: 0x0004E670
	private Vector3 GetNowPlayerPos()
	{
		Vector3 zero = Vector3.zero;
		bool flag = this._NetLiving == null;
		Vector3 result;
		if (flag)
		{
			result = zero;
		}
		else
		{
			result = new Vector3(this._NetLiving.playerCommon.pos.x * this.mapScaleRatioX - this.fMapMaskWidth / 2f, this._NetLiving.playerCommon.pos.z * this.mapScaleRatioY - this.fMapMaskHeight / 2f, 0f);
		}
		return result;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000504F8 File Offset: 0x0004E6F8
	private Quaternion GetPlayerRotation()
	{
		Quaternion identity = Quaternion.identity;
		Vector3 normalized = (this._NetLiving.playerTarget.transform.position - this._NetLiving.UserControl.transform.position).normalized;
		normalized.y = 0f;
		float num = Vector3.Angle(normalized, Vector3.forward);
		bool flag = Vector3.Cross(normalized, Vector3.forward).y > 0f;
		if (flag)
		{
			num = 360f - num;
		}
		return Quaternion.Euler(0f, 0f, 0f - num);
	}

	// Token: 0x040006CA RID: 1738
	public static DtsMapUICon inst;

	// Token: 0x040006CB RID: 1739
	private NetLiving _NetLiving;

	// Token: 0x040006CC RID: 1740
	private DtsUser pDtsUser;

	// Token: 0x040006CD RID: 1741
	private GameObject MaxMap;

	// Token: 0x040006CE RID: 1742
	private GameObject MaxMask;

	// Token: 0x040006CF RID: 1743
	private RawImage MapRaw;

	// Token: 0x040006D0 RID: 1744
	private GameObject MaxMapDuQuan;

	// Token: 0x040006D1 RID: 1745
	private GameObject MaxMapDuQuanEff;

	// Token: 0x040006D2 RID: 1746
	private GameObject MaxMapSafeQuan;

	// Token: 0x040006D3 RID: 1747
	private GameObject MaxMapSafeQuanEff;

	// Token: 0x040006D4 RID: 1748
	private GameObject MaxMapsign;

	// Token: 0x040006D5 RID: 1749
	private Text xcText;

	// Token: 0x040006D6 RID: 1750
	private Text jsText;

	// Token: 0x040006D7 RID: 1751
	private GameObject MixMap;

	// Token: 0x040006D8 RID: 1752
	private MeshRenderer minimapTexture;

	// Token: 0x040006D9 RID: 1753
	private GameObject MixMapsign;

	// Token: 0x040006DA RID: 1754
	private GameObject MixMapDuQuan;

	// Token: 0x040006DB RID: 1755
	private GameObject MixMapDuQuanEff;

	// Token: 0x040006DC RID: 1756
	private GameObject MixMapSafeQuan;

	// Token: 0x040006DD RID: 1757
	private GameObject MixMapSafeQuanEff;

	// Token: 0x040006DE RID: 1758
	private RawImage MixMapRaw;

	// Token: 0x040006DF RID: 1759
	private GameObject MixMovepanel;

	// Token: 0x040006E0 RID: 1760
	private GameObject MixMovequan;

	// Token: 0x040006E1 RID: 1761
	private float mapScaleRatioX;

	// Token: 0x040006E2 RID: 1762
	private float mapScaleRatioY;

	// Token: 0x040006E3 RID: 1763
	private float fMapScnWidth = 800f;

	// Token: 0x040006E4 RID: 1764
	private float fMapScnHeight = 800f;

	// Token: 0x040006E5 RID: 1765
	private float fMapMaskWidth = 1080f;

	// Token: 0x040006E6 RID: 1766
	private float fMapMaskHeight = 1080f;

	// Token: 0x040006E7 RID: 1767
	private float fMinMaskWidth = 295f;

	// Token: 0x040006E8 RID: 1768
	private float fMinMaskHeight = 210f;

	// Token: 0x040006E9 RID: 1769
	private bool bMapLoadOk;
}

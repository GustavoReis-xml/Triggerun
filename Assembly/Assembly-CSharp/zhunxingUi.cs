using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CD RID: 1229
public class zhunxingUi : MonoBehaviour
{
	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x06002F13 RID: 12051 RVA: 0x001A48D8 File Offset: 0x001A2AD8
	// (set) Token: 0x06002F14 RID: 12052 RVA: 0x001A48F0 File Offset: 0x001A2AF0
	public Transform tCommonUi
	{
		get
		{
			return this._tCommonUi;
		}
		set
		{
			this._tCommonUi = value;
		}
	}

	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x06002F15 RID: 12053 RVA: 0x001A48FC File Offset: 0x001A2AFC
	// (set) Token: 0x06002F16 RID: 12054 RVA: 0x001A4914 File Offset: 0x001A2B14
	public Transform tOtherUi
	{
		get
		{
			return this._tOtherUi;
		}
		set
		{
			this._tOtherUi = value;
		}
	}

	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x06002F17 RID: 12055 RVA: 0x001A4920 File Offset: 0x001A2B20
	// (set) Token: 0x06002F18 RID: 12056 RVA: 0x001A4938 File Offset: 0x001A2B38
	public HeroData oneHeroData
	{
		get
		{
			return this._oneHeroData;
		}
		set
		{
			this._oneHeroData = value;
		}
	}

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x06002F19 RID: 12057 RVA: 0x001A4944 File Offset: 0x001A2B44
	// (set) Token: 0x06002F1A RID: 12058 RVA: 0x001A495C File Offset: 0x001A2B5C
	public Image imageRate
	{
		get
		{
			return this._imageRate;
		}
		set
		{
			this._imageRate = value;
		}
	}

	// Token: 0x1700057A RID: 1402
	// (get) Token: 0x06002F1B RID: 12059 RVA: 0x001A4968 File Offset: 0x001A2B68
	// (set) Token: 0x06002F1C RID: 12060 RVA: 0x001A4980 File Offset: 0x001A2B80
	public HitEffect hiteffect
	{
		get
		{
			return this._hiteffect;
		}
		set
		{
			this._hiteffect = value;
		}
	}

	// Token: 0x1700057B RID: 1403
	// (get) Token: 0x06002F1D RID: 12061 RVA: 0x001A498C File Offset: 0x001A2B8C
	// (set) Token: 0x06002F1E RID: 12062 RVA: 0x001A49A4 File Offset: 0x001A2BA4
	public RawImage ImageZhunxing
	{
		get
		{
			return this._ImageZhunxing;
		}
		set
		{
			this._ImageZhunxing = value;
		}
	}

	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x06002F1F RID: 12063 RVA: 0x001A49B0 File Offset: 0x001A2BB0
	// (set) Token: 0x06002F20 RID: 12064 RVA: 0x001A49C8 File Offset: 0x001A2BC8
	public Transform ItAutoAirUI
	{
		get
		{
			return this._ItAutoAirUI;
		}
		set
		{
			this._ItAutoAirUI = value;
		}
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06002F21 RID: 12065 RVA: 0x001A49D4 File Offset: 0x001A2BD4
	// (set) Token: 0x06002F22 RID: 12066 RVA: 0x001A49EC File Offset: 0x001A2BEC
	public Transform OwnAutoAirUI
	{
		get
		{
			return this._OwnAutoAirUI;
		}
		set
		{
			this._OwnAutoAirUI = value;
		}
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06002F23 RID: 12067 RVA: 0x001A49F8 File Offset: 0x001A2BF8
	// (set) Token: 0x06002F24 RID: 12068 RVA: 0x001A4A10 File Offset: 0x001A2C10
	public Transform OtherAutoAirUI
	{
		get
		{
			return this._OtherAutoAirUI;
		}
		set
		{
			this._OtherAutoAirUI = value;
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x06002F25 RID: 12069 RVA: 0x001A4A1C File Offset: 0x001A2C1C
	// (set) Token: 0x06002F26 RID: 12070 RVA: 0x001A4A34 File Offset: 0x001A2C34
	public Transform zhunxingshizi
	{
		get
		{
			return this._zhunxingshizi;
		}
		set
		{
			this._zhunxingshizi = value;
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06002F27 RID: 12071 RVA: 0x001A4A40 File Offset: 0x001A2C40
	// (set) Token: 0x06002F28 RID: 12072 RVA: 0x001A4A58 File Offset: 0x001A2C58
	public Vector2 zhunxingshiziVector
	{
		get
		{
			return this._zhunxingshiziVector;
		}
		set
		{
			this._zhunxingshiziVector = value;
		}
	}

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x06002F29 RID: 12073 RVA: 0x001A4A64 File Offset: 0x001A2C64
	// (set) Token: 0x06002F2A RID: 12074 RVA: 0x001A4A7C File Offset: 0x001A2C7C
	public Cross_Hair_Handle Cross_Hair_Handle_Component
	{
		get
		{
			return this._Cross_Hair_Handle_Component;
		}
		set
		{
			this._Cross_Hair_Handle_Component = value;
		}
	}

	// Token: 0x06002F2B RID: 12075 RVA: 0x001A4A86 File Offset: 0x001A2C86
	public virtual void setUi(Transform gcommonui, Transform gotherui)
	{
		this.tCommonUi = gcommonui;
		this.tOtherUi = gotherui;
		this.initCommon();
	}

	// Token: 0x06002F2C RID: 12076 RVA: 0x001A4AA0 File Offset: 0x001A2CA0
	private void initCommon()
	{
		this.imageRate = this.tCommonUi.FindChild("jingzhun").GetComponent<Image>();
		this.ImageZhunxing = this.tCommonUi.FindChild("zhunxing+").GetComponent<RawImage>();
		this.ImageZhunxing.enabled = false;
		this.hiteffect = new HitEffect();
		this.hiteffect.gLitterHit = this.tCommonUi.FindChild("Hit_Effect/litterhit").gameObject;
		this.hiteffect.gLitterHit.SetActive(false);
		this.hiteffect.gBigHit = this.tCommonUi.FindChild("Hit_Effect/bighit").gameObject;
		this.hiteffect.gBigHit.SetActive(false);
		this.hiteffect.gBaotouHit = this.tCommonUi.FindChild("Hit_Effect/baotouhit").gameObject;
		this.hiteffect.gBaotouHit.SetActive(false);
		this.hiteffect.gcanyuhitHit = this.tCommonUi.FindChild("Hit_Effect/canyuhit").gameObject;
		this.hiteffect.gcanyuhitHit.SetActive(false);
		this.hiteffect.gLastHit = this.tCommonUi.FindChild("Hit_Effect/lasthit").gameObject;
		this.hiteffect.gLastHit.SetActive(false);
		this.hiteffect.gNewLastHit = this.tCommonUi.FindChild("ui_jisha").gameObject;
		this.hiteffect.gNewLastHit.SetActive(false);
		this.hiteffect.gShieldHit = this.tCommonUi.FindChild("Hit_Effect/hudunhit").gameObject;
		this.hiteffect.gShieldHit.SetActive(false);
		this.hiteffect.gPinSiHit = this.tCommonUi.FindChild("ui_pinsi").gameObject;
		this.hiteffect.gPinSiHit.SetActive(false);
		this.hiteffect.gJiaXueHit = this.tCommonUi.FindChild("ui_jiaxue").gameObject;
		this.hiteffect.gJiaXueHit.SetActive(false);
		this.hiteffect.gZhongDuHit = this.tCommonUi.FindChild("ui_zhongdu").gameObject;
		this.hiteffect.gZhongDuHit.SetActive(false);
		this.hiteffect.gGongJiBuff = this.tCommonUi.FindChild("gongji_buff").gameObject;
		this.hiteffect.gGongJiBuff.SetActive(false);
		this.hiteffect.gHuDunEffect = this.tCommonUi.FindChild("ui_hudun").gameObject;
		this.hiteffect.gHuDunEffect.SetActive(false);
		this.hiteffect.gJiaSuEffect = this.tCommonUi.FindChild("ui_jiasu").gameObject;
		this.hiteffect.gJiaSuEffect.SetActive(false);
		this.hiteffect.gBingDongEffect = this.tCommonUi.FindChild("ui_bingdong").gameObject;
		this.hiteffect.gBingDongEffect.SetActive(false);
		this.hiteffect.gRanshaoEffect = this.tCommonUi.FindChild("ui_ranshao").gameObject;
		this.hiteffect.gRanshaoEffect.SetActive(false);
		this.hiteffect.gDaoBiaoEffect = this.tCommonUi.FindChild("ui_luan").gameObject;
		this.hiteffect.gDaoBiaoEffect.SetActive(false);
		this.hiteffect.gXuanYunEffect = this.tCommonUi.FindChild("ui_xuanyun").gameObject;
		this.hiteffect.gXuanYunEffect.SetActive(false);
		this.hiteffect.gJianGongEffect = this.tCommonUi.FindChild("ui_jiangong").gameObject;
		this.hiteffect.gJianGongEffect.SetActive(false);
		this.hiteffect.gDianJiEffect = this.tCommonUi.FindChild("dianji_buff").gameObject;
		this.hiteffect.gDianJiEffect.SetActive(false);
		this.hiteffect.gLls_fy_buff = this.tCommonUi.FindChild("lls_fy_buff").gameObject;
		this.hiteffect.gLls_fy_buff.SetActive(false);
		this.hiteffect.ghz_buff_dun = this.tCommonUi.FindChild("hz_buff_dun").gameObject;
		this.hiteffect.ghz_buff_dun.SetActive(false);
		this.hiteffect.gReviveIng = this.tCommonUi.FindChild("ui_zzfh").gameObject;
		this.hiteffect.gReviveIng.SetActive(false);
		this.hiteffect.gHitTextEffect = this.tCommonUi.FindChild("lianshaeffect/hiteffect").gameObject.GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.hiteffect.gHitTextEffect.font = fUIManager.mNewComic;
		}
		this.hiteffect.gHitTextEffect.enabled = false;
		this.ItAutoAirUI = this.tCommonUi.FindChild("autoair_it");
		this.ItAutoAirUI.gameObject.SetActive(false);
		this.OwnAutoAirUI = this.tCommonUi.FindChild("autoair_own");
		this.OwnAutoAirUI.gameObject.SetActive(false);
		this.OtherAutoAirUI = this.tCommonUi.FindChild("autoair_hz");
		this.OtherAutoAirUI.gameObject.SetActive(false);
		this.zhunxingshizi = this.tCommonUi.FindChild("zhunxing");
		this.Cross_Hair_Handle_Component = this.zhunxingshizi.gameObject.GetComponent<Cross_Hair_Handle>();
		bool flag2 = this.Cross_Hair_Handle_Component == null;
		if (flag2)
		{
			this.Cross_Hair_Handle_Component = this.zhunxingshizi.gameObject.AddComponent<Cross_Hair_Handle>();
		}
		this.Cross_Hair_Handle_Component.Setting_Size(this.oneHeroData.Cross_Hair_Width, this.oneHeroData.Cross_Hair_Height);
		this.zhunxingshiziVector = this.zhunxingshizi.GetComponent<RectTransform>().sizeDelta;
		this.zhunxingshizi.gameObject.SetActive(false);
		this._imageRate.enabled = false;
	}

	// Token: 0x06002F2D RID: 12077 RVA: 0x001A50D8 File Offset: 0x001A32D8
	public void iconShow()
	{
		this.ImageZhunxing.enabled = false;
		bool flag = !(this.oneHeroData.hzxicon == string.Empty);
		if (flag)
		{
			string spath = "herozhunxing/" + this.oneHeroData.hzxicon + ".txt";
			sysload.loadImage(this._ImageZhunxing, spath, null);
		}
	}

	// Token: 0x06002F2E RID: 12078 RVA: 0x001A513C File Offset: 0x001A333C
	public bool doZhunxingChange(float fSize)
	{
		bool flag = this.imageRate == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			this.imageRate.rectTransform.sizeDelta = new Vector2(fSize, fSize);
			result = true;
		}
		return result;
	}

	// Token: 0x06002F2F RID: 12079 RVA: 0x001A517C File Offset: 0x001A337C
	public bool doShiZiZhunxingChange(float fSize)
	{
		bool flag = this.zhunxingshizi == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			this.zhunxingshizi.GetComponent<RectTransform>().sizeDelta = this.zhunxingshiziVector + new Vector2(fSize, fSize);
			result = true;
		}
		return result;
	}

	// Token: 0x06002F30 RID: 12080 RVA: 0x001A51C8 File Offset: 0x001A33C8
	public void OnAutoAirEffect(NetLiving NetTarget, GameObject oObj)
	{
		bool flag = NetTarget == null;
		if (!flag)
		{
			Vector3 userCenter = NetTarget.userCenter;
			Vector3 vector = CameraControl.Instance.controlCamera.WorldToScreenPoint(userCenter);
			float num = (float)Screen.width;
			float num2 = (float)Screen.height;
			bool flag2 = vector.x < 0f || vector.x > num || vector.y < 0f || vector.y > num2 || vector.z <= 0f;
			if (flag2)
			{
				bool activeSelf = oObj.activeSelf;
				if (activeSelf)
				{
					oObj.SetActive(false);
				}
			}
			else
			{
				bool flag3 = !oObj.activeSelf;
				if (flag3)
				{
					oObj.SetActive(true);
				}
				Vector2 sizeDelta = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
				float num3 = vector.x / num;
				float num4 = vector.y / num2;
				float x = sizeDelta.x * num3 - sizeDelta.x / 2f;
				float y = sizeDelta.y * num4 - sizeDelta.y / 2f;
				oObj.transform.localPosition = new Vector3(x, y, 0f);
			}
		}
	}

	// Token: 0x06002F31 RID: 12081 RVA: 0x001A5300 File Offset: 0x001A3500
	public GameObject GetPlayerFpsEffect(string sEffName)
	{
		GameObject gameObject = null;
		bool flag = this.hiteffect == null;
		GameObject result;
		if (flag)
		{
			result = gameObject;
		}
		else
		{
			uint num = PrivateImplementationDetails.ComputeStringHash(sEffName);
			if (num <= 1692759408U)
			{
				if (num <= 900695366U)
				{
					if (num != 63613925U)
					{
						if (num != 303252787U)
						{
							if (num == 900695366U)
							{
								if (sEffName == "ui_jiaxue")
								{
									gameObject = this.hiteffect.gJiaXueHit;
								}
							}
						}
						else if (sEffName == "gongji_buff")
						{
							gameObject = this.hiteffect.gGongJiBuff;
						}
					}
					else if (sEffName == "jiasu_buff")
					{
						gameObject = this.hiteffect.gJiaSuEffect;
					}
				}
				else if (num != 1118654500U)
				{
					if (num != 1492915855U)
					{
						if (num == 1692759408U)
						{
							if (sEffName == "ghz_buff_dun")
							{
								gameObject = this.hiteffect.ghz_buff_dun;
							}
						}
					}
					else if (sEffName == "ui_jiangong")
					{
						gameObject = this.hiteffect.gJianGongEffect;
					}
				}
				else if (sEffName == "dianji_buff")
				{
					gameObject = this.hiteffect.gDianJiEffect;
				}
			}
			else if (num <= 3020417035U)
			{
				if (num != 1882542136U)
				{
					if (num != 2701315836U)
					{
						if (num == 3020417035U)
						{
							if (sEffName == "ui_zhongdu")
							{
								gameObject = this.hiteffect.gZhongDuHit;
							}
						}
					}
					else if (sEffName == "ui_xuanyun")
					{
						gameObject = this.hiteffect.gXuanYunEffect;
					}
				}
				else if (sEffName == "ui_bingdong")
				{
					gameObject = this.hiteffect.gBingDongEffect;
				}
			}
			else if (num <= 3491351346U)
			{
				if (num != 3365796158U)
				{
					if (num == 3491351346U)
					{
						if (sEffName == "ui_hudun")
						{
							gameObject = this.hiteffect.gHuDunEffect;
						}
					}
				}
				else if (sEffName == "lls_fy_buff")
				{
					gameObject = this.hiteffect.gLls_fy_buff;
				}
			}
			else if (num != 3814954110U)
			{
				if (num == 4197356140U)
				{
					if (sEffName == "ui_luan")
					{
						gameObject = this.hiteffect.gDaoBiaoEffect;
					}
				}
			}
			else if (sEffName == "ui_ranshao")
			{
				gameObject = this.hiteffect.gRanshaoEffect;
			}
			result = gameObject;
		}
		return result;
	}

	// Token: 0x040028D1 RID: 10449
	private Transform _tCommonUi;

	// Token: 0x040028D2 RID: 10450
	private Transform _tOtherUi;

	// Token: 0x040028D3 RID: 10451
	private HeroData _oneHeroData;

	// Token: 0x040028D4 RID: 10452
	private Image _imageRate;

	// Token: 0x040028D5 RID: 10453
	private HitEffect _hiteffect;

	// Token: 0x040028D6 RID: 10454
	private RawImage _ImageZhunxing;

	// Token: 0x040028D7 RID: 10455
	private Transform _ItAutoAirUI;

	// Token: 0x040028D8 RID: 10456
	private Transform _OwnAutoAirUI;

	// Token: 0x040028D9 RID: 10457
	private Transform _OtherAutoAirUI;

	// Token: 0x040028DA RID: 10458
	private Transform _zhunxingshizi;

	// Token: 0x040028DB RID: 10459
	private Vector2 _zhunxingshiziVector;

	// Token: 0x040028DC RID: 10460
	private Cross_Hair_Handle _Cross_Hair_Handle_Component;
}

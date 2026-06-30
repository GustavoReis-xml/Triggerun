using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000258 RID: 600
public class NewHeroHpUi : Window
{
	// Token: 0x06001B4F RID: 6991 RVA: 0x00101D64 File Offset: 0x000FFF64
	protected override void Awake()
	{
		base.Awake();
		this.oUser = base.transform.FindChild("user").gameObject;
		this.imageHero = base.transform.FindChild("user/img").GetComponent<RawImage>();
		this.numshow = base.transform.FindChild("numshow").gameObject;
		this.NowBulletNum = base.transform.FindChild("numshow/leftnum").GetComponent<Text>();
		this.MaxBulletNum = base.transform.FindChild("numshow/rightnum").GetComponent<Text>();
		this.Ghpshow = base.transform.FindChild("hpshow").gameObject;
		this.Ghpshow.SetActive(false);
		this.Glshowclone = base.transform.FindChild("lshowclone").gameObject;
		this.Glshowclone.SetActive(false);
		this.hpObj = base.transform.FindChild("hp").gameObject;
		this.hpObj.SetActive(false);
		this.oNewhp = base.transform.FindChild("newhp").gameObject;
		this.hpNewcon = this.oNewhp.AddComponent<HpNewControl>();
		this.oUser.SetActive(false);
		this.numshow.SetActive(false);
		this.oNewhp.SetActive(false);
		NewHeroHpUi.Inst = this;
		this.LoadSingleEff();
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x00101ED8 File Offset: 0x001000D8
	public void Refresh(NetLiving _net)
	{
		bool flag = _net != null;
		if (flag)
		{
			this._NetLiving = _net;
			string sHead = string.Empty;
			int num = 0;
			int cid = 0;
			bool flag2 = this._NetLiving != null;
			if (flag2)
			{
				num = this._NetLiving.playerCommon.m_nModelIndex;
				if (this._NetLiving.playerCommon != null)
				{
					cid = this._NetLiving.playerCommon.m_nCid;
				}
			}
			bool flag3 = MatchManager.GetInstance().m_nMatchType == 4 && num == 2000;
			if (flag3)
			{
				sHead = "male_1";
			}
			else
			{
				SkinUnlock useSkinData = HeroDataManager.GetInstance().GetUseSkinData(num, cid);
				if (useSkinData != null)
				{
					sHead = useSkinData.sIconName;
				}
				else
				{
					bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(num);
					if (flag4)
					{
						sHead = fGameManager.mloadConfig.AllHeroUnlock[num].szSkinUnlock[1].sIconName;
					}
				}
			}
			this.UpdateHpObj(this._NetLiving, sHead);
		}
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x00101F91 File Offset: 0x00100191
	public void UpdateHpObj(NetLiving netobj, string sHead)
	{
		this._NetLiving = netobj;
		this.nNewHp = 0;
		this.nOldNewHp = 0;
		this.setHead(sHead);
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00101FB4 File Offset: 0x001001B4
	private void setHead(string sHead)
	{
		bool flag = !(NewHeroHpUi.Inst == null);
		if (flag)
		{
			this.imageHero.enabled = false;
			bool flag2 = sHead != string.Empty;
			if (flag2)
			{
				sysload.loadImage(this.imageHero, "herounlock/skin/" + sHead + ".txt", null);
				RectTransform rt = this.imageHero.GetComponent<RectTransform>();
				if (rt != null)
				{
					rt.sizeDelta = new Vector2(109f, 158f);
				}
			}
		}
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00102011 File Offset: 0x00100211
	private void Update()
	{
		this.CheckUiActSelf();
		this.CheckNewHp();
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x00102024 File Offset: 0x00100224
	private void CheckNewHp()
	{
		bool flag = this._NetLiving != null && !(this.hpNewcon == null);
		if (flag)
		{
			this.nNewHp = this._NetLiving.playerCommon.GetNowHp();
			bool flag2 = this.nNewHp != this.nOldNewHp;
			if (flag2)
			{
				int maxHp = this._NetLiving.playerCommon.GetMaxHp();
				this.NowBulletNum.text = this.nNewHp.ToString();
				this.MaxBulletNum.text = maxHp.ToString();
				this.hpNewcon.SetHp(maxHp, this._NetLiving.playerCommon.m_nHp, this._NetLiving.playerCommon.m_nArmor, this._NetLiving.playerCommon.m_nShield, 0);
				this.nOldNewHp = this.nNewHp;
			}
		}
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x0010210C File Offset: 0x0010030C
	public void CheckUiActSelf()
	{
		bool flag = this._NetLiving == null || (this._NetLiving != null && this._NetLiving.isDead) || roleuiManager.GetInstance().bOtherUiVisable || (fGameManager.IsSingle && PlayerLearnManager.GetInstance().HideHeroHpUI) || (gameuiCon.inst.Net != null && gameuiCon.inst.Net.netobject_type == 2);
		if (flag)
		{
			bool activeSelf = this.oUser.activeSelf;
			if (activeSelf)
			{
				this.oUser.SetActive(false);
			}
			bool activeSelf2 = this.oNewhp.activeSelf;
			if (activeSelf2)
			{
				this.oNewhp.SetActive(false);
			}
			bool activeSelf3 = this.numshow.activeSelf;
			if (activeSelf3)
			{
				this.numshow.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.oUser.activeSelf;
			if (flag2)
			{
				this.oUser.SetActive(true);
			}
			bool flag3 = !this.oNewhp.activeSelf;
			if (flag3)
			{
				this.oNewhp.SetActive(true);
			}
			bool flag4 = !this.numshow.activeSelf;
			if (flag4)
			{
				this.numshow.SetActive(true);
			}
		}
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x00102248 File Offset: 0x00100448
	private void LoadSingleEff()
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			string url = "UI/learn/ui_tishi.ab";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadMapEffOk), null);
			}
			else
			{
				this.LoadMapEffOk(resWWW, null);
			}
		}
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x0010229C File Offset: 0x0010049C
	private void LoadMapEffOk(WWW www, object Param)
	{
		bool flag = www != null && www.error == null && !(base.transform == null);
		if (flag)
		{
			bool flag2 = this.szSingleEffObj.Count > 0;
			if (flag2)
			{
				Debug.Log("=============不需要重复加载...");
			}
			else
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = base.transform;
				gameObject.SetActive(false);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = new Vector3(525f, 80f, 0f);
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.szSingleEffObj.Add(gameObject);
			}
		}
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x00102380 File Offset: 0x00100580
	public void SetUiEffActive(bool active)
	{
		bool flag = this.szSingleEffObj.Count > 0;
		if (flag)
		{
			this.szSingleEffObj[0].transform.gameObject.SetActive(active);
		}
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x001023C0 File Offset: 0x001005C0
	public void CleanSingleEff()
	{
		foreach (GameObject obj in this.szSingleEffObj)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.szSingleEffObj.Clear();
	}

	// Token: 0x040018A3 RID: 6307
	public static NewHeroHpUi Inst;

	// Token: 0x040018A4 RID: 6308
	private RawImage imageHero;

	// Token: 0x040018A5 RID: 6309
	private Text NowBulletNum;

	// Token: 0x040018A6 RID: 6310
	private Text MaxBulletNum;

	// Token: 0x040018A7 RID: 6311
	private GameObject Glshowclone;

	// Token: 0x040018A8 RID: 6312
	private GameObject Ghpshow;

	// Token: 0x040018A9 RID: 6313
	private NetLiving _NetLiving;

	// Token: 0x040018AA RID: 6314
	private GameObject hpObj;

	// Token: 0x040018AB RID: 6315
	private HpControl hpcon;

	// Token: 0x040018AC RID: 6316
	private GameObject oUser;

	// Token: 0x040018AD RID: 6317
	private GameObject numshow;

	// Token: 0x040018AE RID: 6318
	private GameObject oNewhp;

	// Token: 0x040018AF RID: 6319
	private HpNewControl hpNewcon;

	// Token: 0x040018B0 RID: 6320
	private List<GameObject> szSingleEffObj = new List<GameObject>();

	// Token: 0x040018B1 RID: 6321
	private int nNewHp;

	// Token: 0x040018B2 RID: 6322
	private int nOldNewHp;
}

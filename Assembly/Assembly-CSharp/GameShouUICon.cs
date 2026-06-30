using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013F RID: 319
public class GameShouUICon : Window
{
	// Token: 0x06000D57 RID: 3415 RVA: 0x0007FAA8 File Offset: 0x0007DCA8
	protected override void Awake()
	{
		base.Awake();
		this.qczj = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.qczj.font = fUIManager.mNewComic;
		}
		this.username = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.heroname = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.xm_obj = this.UIGameObj.Get_GameObj(3);
		this.xm_Text = this.xm_obj.transform.Find("value").GetComponent<Text>();
		this.xm_Eff = this.xm_obj.transform.Find("ui_shuzi").gameObject;
		this.xm_Eff.SetActive(false);
		this.mbgfxm_obj = this.UIGameObj.Get_GameObj(4);
		this.mbgfxm_Text = this.mbgfxm_obj.transform.Find("value").GetComponent<Text>();
		this.mbgfxm_Eff = this.mbgfxm_obj.transform.Find("ui_shuzi").gameObject;
		this.mbgfxm_Eff.SetActive(false);
		this.mbgfsj_obj = this.UIGameObj.Get_GameObj(5);
		this.mbgfsj_Text = this.mbgfsj_obj.transform.Find("value").GetComponent<Text>();
		this.mbgfsj_Eff = this.mbgfsj_obj.transform.Find("ui_shuzi").gameObject;
		this.mbgfsj_Eff.SetActive(false);
		this.shl_obj = this.UIGameObj.Get_GameObj(6);
		this.shl_Text = this.shl_obj.transform.Find("value").GetComponent<Text>();
		this.shl_Eff = this.shl_obj.transform.Find("ui_shuzi").gameObject;
		this.shl_Eff.SetActive(false);
		this.zll_obj = this.UIGameObj.Get_GameObj(7);
		this.zll_Text = this.zll_obj.transform.Find("value").GetComponent<Text>();
		this.zll_Eff = this.zll_obj.transform.Find("ui_shuzi").gameObject;
		this.zll_Eff.SetActive(false);
		this.renwu_effect = this.UIGameObj.Get_GameObj(8);
		this.renwu_effect.SetActive(false);
		GameShouUICon.Inst = this;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0007FD28 File Offset: 0x0007DF28
	private IEnumerator OnStartModelEffect()
	{
		yield return new WaitForSeconds(1f);
		this.renwu_effect.SetActive(true);
		yield break;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0007FD38 File Offset: 0x0007DF38
	public void OnInitShow()
	{
		this.username.text = string.Empty;
		this.heroname.text = string.Empty;
		this.xm_Text.enabled = false;
		this.mbgfxm_Text.enabled = false;
		this.mbgfsj_Text.enabled = false;
		this.shl_Text.enabled = false;
		this.zll_Text.enabled = false;
		this.xm_Eff.SetActive(false);
		this.mbgfxm_Eff.SetActive(false);
		this.mbgfsj_Eff.SetActive(false);
		this.shl_Eff.SetActive(false);
		this.zll_Eff.SetActive(false);
		this.renwu_effect.SetActive(false);
		this.modelAni = null;
		this.emotionState = 0;
		this.qczj.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-600f, 420f, 0f);
		this.username.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-600f, 272f, 0f);
		this.heroname.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-600f, 160f, 0f);
		this.xm_obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(730f, 0f, 0f);
		this.mbgfxm_obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(730f, -110f, 0f);
		this.mbgfsj_obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(730f, -220f, 0f);
		this.shl_obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(730f, -330f, 0f);
		this.zll_obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(730f, -440f, 0f);
		this.m_szMvpUserdata = new ArrayList();
		this.sUISenceName = string.Empty;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0007FF54 File Offset: 0x0007E154
	public void OnLoadMvpModeData(ArrayList szData)
	{
		this.OnInitShow();
		this.m_szMvpUserdata = (ArrayList)(szData[0] as ArrayList).Clone();
		this.sUISenceName = szData[1].ToString();
		base.StartCoroutine(this.OnDelayTime());
		string url = "UI/" + this.sUISenceName + ".ab";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadUIOK), null);
		}
		else
		{
			this.LoadUIOK(resWWW, null);
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0007FFEC File Offset: 0x0007E1EC
	private void LoadUIOK(WWW www, object customParam)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (!flag)
		{
			bool flag2 = this.m_szMvpUserdata.Count < 1;
			if (flag2)
			{
				this.Close();
				Debug.Log("结算时展示界面下载回来时结算界面已经关闭:" + roleuiManager.instance.GetMeName() + "--时间:" + Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"));
			}
			else
			{
				bool flag3 = gameuiCon.inst != null && gameuiCon.inst.visible;
				if (flag3)
				{
					gameuiCon.inst.CloseRoundEndUI();
				}
				fUIManager.GetInstance().OnLoadResultUI(0);
				bool flag4 = fUIManager.mGameUI != null && fUIManager.mGameUI.activeSelf;
				if (flag4)
				{
					fUIManager.mGameUI.SetActive(false);
				}
				RenderSettings.ambientLight = Color.clear;
				bool flag5 = GameRoot.inst != null && GameRoot.inst.tfJiaoSe_Light != null;
				if (flag5)
				{
					GameRoot.inst.tfJiaoSe_Light.SetActive(false);
				}
				bool flag6 = this.modelScn != null;
				if (flag6)
				{
					UnityEngine.Object.DestroyImmediate(this.modelScn);
					this.modelScn = null;
				}
				bool flag7 = this.modelScn == null;
				if (flag7)
				{
					this.modelScn = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
					this.modelScn.transform.localPosition = new Vector3(-1000f, -1000f, -800f);
					this.modelScn.transform.localScale = Vector3.one;
					this.modelScn.transform.localRotation = Quaternion.Euler(Vector3.zero);
					this.modelScn.name = this.sUISenceName;
				}
				Transform x = this.modelScn.transform.Find("HeroList");
				bool flag8 = x == null || this.oHeroList == null;
				if (flag8)
				{
					this.oHeroList = new GameObject("HeroList");
					this.oHeroList.transform.SetParent(this.modelScn.transform);
					this.oHeroList.transform.localPosition = Vector3.zero;
					this.oHeroList.transform.localScale = Vector3.one;
					this.oHeroList.transform.localRotation = Quaternion.Euler(Vector3.zero);
				}
				this.OnLoadHeroModel();
			}
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00080294 File Offset: 0x0007E494
	private void OnLoadHeroModel()
	{
		HeroUnlockData heroUnlockData = null;
		SkinUnlock skinUnlock = null;
		int num = 0;
		int num2 = 0;
		EmotionUnlock emotionUnlock = null;
		string sName = string.Empty;
		sName = Fun.DecodingUTF8(this.m_szMvpUserdata[1].ToString());
		bool flag = this.oHeroList.transform.childCount > 0;
		if (flag)
		{
			Fun.ClearChildObejct(this.oHeroList);
		}
		UserData userData = roleuiManager.GetInstance().GetUserData(sName);
		bool flag2 = userData == null;
		if (!flag2)
		{
			int nHeroID = userData.m_nHeroID;
			int nCid = userData.m_nCid;
			Transform transform = this.oHeroList.transform.Find(nHeroID.ToString());
			bool flag3 = transform != null;
			if (flag3)
			{
				UnityEngine.Object.DestroyImmediate(transform.gameObject);
			}
			int useEquipId = userData.GetUseEquipId(1, 1);
			bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nHeroID);
			if (flag4)
			{
				heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[nHeroID];
				bool flag5 = heroUnlockData != null && heroUnlockData.szSkinUnlock.ContainsKey(useEquipId);
				if (flag5)
				{
					skinUnlock = heroUnlockData.szSkinUnlock[useEquipId];
				}
			}
			bool flag6 = skinUnlock == null;
			if (flag6)
			{
				Debug.Log(string.Concat(new string[]
				{
					"游戏结算展示时没有该玩家皮肤 --选中的英雄nSlcHeroId:",
					nHeroID.ToString(),
					"--sRloeName:",
					userData.m_sNick,
					"--m_nNetid:",
					userData.m_nNetid.ToString(),
					"--nSkiId:",
					useEquipId.ToString(),
					"--Cid:",
					nCid.ToString()
				}));
			}
			else
			{
				string text = skinUnlock.tpsModel;
				int useEquipId2 = userData.GetUseEquipId(2, 1);
				bool flag7 = heroUnlockData != null && heroUnlockData.szEmotionUnlock.ContainsKey(useEquipId2);
				if (flag7)
				{
					emotionUnlock = heroUnlockData.szEmotionUnlock[useEquipId2];
				}
				bool flag8 = emotionUnlock != null && !string.IsNullOrEmpty(emotionUnlock.animParm);
				if (flag8)
				{
					num = int.Parse(emotionUnlock.animParm);
				}
				text = "HeroAnimator/" + text + ".mo";
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadModelTpsFinish), new ArrayList
				{
					nHeroID,
					skinUnlock.tpsControl,
					string.Empty,
					num,
					0,
					num2,
					heroUnlockData
				});
			}
		}
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00080528 File Offset: 0x0007E728
	private void LoadModelTpsFinish(WWW www, object obj)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList = (ArrayList)obj;
			int num = (int)arrayList[0];
			string text = arrayList[1].ToString();
			int num2 = (int)arrayList[3];
			int num3 = (int)arrayList[4];
			int num4 = (int)arrayList[5];
			HeroUnlockData heroUnlockData = (HeroUnlockData)arrayList[6];
			bool flag2 = heroUnlockData == null || this.oHeroList == null;
			if (!flag2)
			{
				Transform transform = this.oHeroList.transform.Find(num3.ToString());
				bool flag3 = transform != null;
				if (flag3)
				{
					UnityEngine.Object.DestroyImmediate(transform.gameObject);
				}
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.name = num3.ToString();
				gameObject.transform.SetParent(this.oHeroList.transform);
				gameObject.transform.localPosition = new Vector3(-0.3f, -1f, -4f);
				gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 170f, 0f));
				gameObject.transform.localScale = Vector3.one * heroUnlockData.modelScale;
				Transform[] componentsInChildren = gameObject.transform.GetComponentsInChildren<Transform>(true);
				foreach (Transform transform2 in componentsInChildren)
				{
					bool flag4 = transform2.name.Contains("yinshen") || transform2.name.Contains("hongxian") || transform2.name.Contains("honwai");
					if (flag4)
					{
						Renderer component = transform2.GetComponent<Renderer>();
						bool flag5 = component != null;
						if (flag5)
						{
							component.enabled = false;
						}
					}
				}
				Animator component2 = gameObject.transform.GetComponent<Animator>();
				Transform transform3 = gameObject.transform.Find("GrounderTrs");
				bool flag6 = transform3 != null;
				if (flag6)
				{
					UnityEngine.Object.DestroyImmediate(transform3.gameObject);
				}
				transform3 = gameObject.transform.Find("HitReactionTrs");
				bool flag7 = transform3 != null;
				if (flag7)
				{
					UnityEngine.Object.DestroyImmediate(transform3.gameObject);
				}
				bool flag8 = text != "0";
				if (flag8)
				{
					gameObject.SetActive(false);
					GameRoot.LoadAsset("HeroAnimator/" + text + ".mo", new fResourceManager.DownFinishDelegate(this.LoadControlTpsFinish), new ArrayList
					{
						component2,
						heroUnlockData,
						gameObject,
						num2,
						num4
					});
				}
			}
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00080838 File Offset: 0x0007EA38
	private void LoadControlTpsFinish(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList = (ArrayList)customParam;
			Animator animator = (Animator)arrayList[0];
			GameObject gameObject = (GameObject)arrayList[2];
			int emotion = (int)arrayList[3];
			string sName = Fun.DecodingUTF8(this.m_szMvpUserdata[1].ToString());
			UserData userData = roleuiManager.GetInstance().GetUserData(sName);
			bool flag2 = userData != null;
			if (flag2)
			{
				animator.runtimeAnimatorController = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
				gameObject.SetActive(true);
				this.OnSetModelAni(animator, emotion);
				this.OnShowWin(this.m_szMvpUserdata);
				CHeroWeapon component = gameObject.GetComponent<CHeroWeapon>();
				bool flag3 = component != null;
				if (flag3)
				{
					component.SetHeroData(userData.m_nHeroID, userData.m_nCid);
					component.SetHeroWeapon(3, userData.GetUseEquipId(4, 1), 2);
				}
			}
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00080950 File Offset: 0x0007EB50
	public void OnShowWin(ArrayList szMvpUserdata)
	{
		string text = string.Empty;
		string sName = string.Empty;
		sName = Fun.DecodingUTF8(szMvpUserdata[1].ToString());
		UserData userData = roleuiManager.GetInstance().GetUserData(sName);
		bool flag = userData != null;
		if (flag)
		{
			this.username.text = userData.m_sNick;
			bool flag2 = fGameManager.mloadConfig.AllHeroList.ContainsKey(userData.m_nHeroID);
			if (flag2)
			{
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[userData.m_nHeroID];
				text = heroData.hname;
			}
			this.heroname.text = Language.GetLang(4009, new object[]
			{
				text
			});
			this.xm_Text.text = szMvpUserdata[10].ToString();
			this.mbgfxm_Text.text = szMvpUserdata[11].ToString();
			this.mbgfsj_Text.text = szMvpUserdata[12].ToString();
			this.shl_Text.text = szMvpUserdata[13].ToString();
			this.zll_Text.text = szMvpUserdata[14].ToString();
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[2104];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			bool activeSelf = base.gameObject.activeSelf;
			if (activeSelf)
			{
				base.StartCoroutine(this.OnStartLeftTopEffect());
			}
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00080AD0 File Offset: 0x0007ECD0
	private IEnumerator OnStartLeftTopEffect()
	{
		yield return new WaitForSeconds(1.5f);
		base.StopCoroutine("OnStartTextEffect");
		bool flag = !base.gameObject.activeSelf;
		if (flag)
		{
			yield return null;
		}
		base.StartCoroutine(this.OnStartTextEffect(this.qczj.transform.gameObject, 0f, 0, "ui_mvp_title"));
		base.StartCoroutine(this.OnStartTextEffect(this.username.transform.gameObject, 0.3f, 0, "ui_mvp_data"));
		base.StartCoroutine(this.OnStartTextEffect(this.heroname.transform.gameObject, 0.5f, 1, "ui_mvp_data"));
		yield break;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x00080AE0 File Offset: 0x0007ECE0
	private void OnStartEffect()
	{
		base.StopCoroutine("OnStartEffect");
		bool activeSelf = base.gameObject.activeSelf;
		if (activeSelf)
		{
			base.StartCoroutine(this.OnStartEffect(this.xm_obj, this.xm_Text, 0f));
			base.StartCoroutine(this.OnStartEffect(this.mbgfxm_obj, this.mbgfxm_Text, 0.2f));
			base.StartCoroutine(this.OnStartEffect(this.mbgfsj_obj, this.mbgfsj_Text, 0.4f));
			base.StartCoroutine(this.OnStartEffect(this.shl_obj, this.shl_Text, 0.6f));
			base.StartCoroutine(this.OnStartEffect(this.zll_obj, this.zll_Text, 0.8f));
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x00080BA4 File Offset: 0x0007EDA4
	private IEnumerator OnStartEffect(GameObject obj, Text gText, float ftime)
	{
		yield return new WaitForSeconds(ftime);
		fUISoundManager.instance.PlayUISound("ui_mvp_numbwr", 0, null, 20);
		Tweener tweener = obj.transform.transform.DOLocalMoveX(-30f, 0.15f, false);
		tweener.OnComplete(delegate
		{
			tweener = obj.transform.transform.DOLocalMoveX(0f, 0.1f, false);
			tweener.OnComplete(delegate
			{
				gText.enabled = true;
				obj.transform.Find("ui_shuzi").gameObject.SetActive(true);
			});
		});
		yield break;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x00080BC8 File Offset: 0x0007EDC8
	private IEnumerator OnStartTextEffect(GameObject obj, float ftime, int nType, string sSound)
	{
		yield return new WaitForSeconds(ftime);
		fUISoundManager.instance.PlayUISound(sSound, 0, null, 20);
		Tweener tweener = obj.transform.transform.DOLocalMoveX(0f, 0.15f, false);
		bool flag = nType == 1;
		if (flag)
		{
			tweener.OnComplete(delegate
			{
				this.OnStartEffect();
				this.OnAni();
			});
		}
		yield break;
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x00080BF4 File Offset: 0x0007EDF4
	public override void Close()
	{
		this.OnInitShow();
		bool flag = this.modelScn != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.modelScn);
			this.modelScn = null;
		}
		bool flag2 = this.oHeroList != null;
		if (flag2)
		{
			bool flag3 = this.oHeroList.transform.childCount > 0;
			if (flag3)
			{
				Fun.ClearChildObejct(this.oHeroList);
			}
			this.oHeroList = null;
		}
		base.StopCoroutine("OnDelayTime");
		base.Close();
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00080C80 File Offset: 0x0007EE80
	private void OnSetModelAni(Animator Ani, int emotion)
	{
		this.modelAni = Ani;
		this.emotionState = emotion;
		bool activeSelf = base.gameObject.activeSelf;
		if (activeSelf)
		{
			base.StartCoroutine(this.OnStartModelEffect());
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00080CBC File Offset: 0x0007EEBC
	private void OnAni()
	{
		bool flag = !(this.modelAni == null) && this.modelAni.GetInteger("HuDongState") != this.emotionState;
		if (flag)
		{
			this.modelAni.SetInteger("HuDongState", this.emotionState);
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x00080D13 File Offset: 0x0007EF13
	private IEnumerator OnDelayTime()
	{
		yield return new WaitForSeconds(7f);
		fUIManager.GetInstance().ShowGameResultUI();
		yield break;
	}

	// Token: 0x04000AF3 RID: 2803
	public static GameShouUICon Inst;

	// Token: 0x04000AF4 RID: 2804
	private Text qczj;

	// Token: 0x04000AF5 RID: 2805
	private Text username;

	// Token: 0x04000AF6 RID: 2806
	private Text heroname;

	// Token: 0x04000AF7 RID: 2807
	private GameObject xm_obj;

	// Token: 0x04000AF8 RID: 2808
	private GameObject mbgfxm_obj;

	// Token: 0x04000AF9 RID: 2809
	private GameObject mbgfsj_obj;

	// Token: 0x04000AFA RID: 2810
	private GameObject shl_obj;

	// Token: 0x04000AFB RID: 2811
	private GameObject zll_obj;

	// Token: 0x04000AFC RID: 2812
	private Text xm_Text;

	// Token: 0x04000AFD RID: 2813
	private Text mbgfxm_Text;

	// Token: 0x04000AFE RID: 2814
	private Text mbgfsj_Text;

	// Token: 0x04000AFF RID: 2815
	private Text shl_Text;

	// Token: 0x04000B00 RID: 2816
	private Text zll_Text;

	// Token: 0x04000B01 RID: 2817
	private GameObject xm_Eff;

	// Token: 0x04000B02 RID: 2818
	private GameObject mbgfxm_Eff;

	// Token: 0x04000B03 RID: 2819
	private GameObject mbgfsj_Eff;

	// Token: 0x04000B04 RID: 2820
	private GameObject shl_Eff;

	// Token: 0x04000B05 RID: 2821
	private GameObject zll_Eff;

	// Token: 0x04000B06 RID: 2822
	private GameObject renwu_effect;

	// Token: 0x04000B07 RID: 2823
	public GameObject modelScn;

	// Token: 0x04000B08 RID: 2824
	private GameObject oHeroList;

	// Token: 0x04000B09 RID: 2825
	private ArrayList m_szMvpUserdata = new ArrayList();

	// Token: 0x04000B0A RID: 2826
	private string sUISenceName = string.Empty;

	// Token: 0x04000B0B RID: 2827
	private Animator modelAni;

	// Token: 0x04000B0C RID: 2828
	private int emotionState;
}

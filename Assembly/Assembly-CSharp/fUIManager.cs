using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000134 RID: 308
public class fUIManager : MonoBehaviour
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x0006E68A File Offset: 0x0006C88A
	private void Start()
	{
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0006E68D File Offset: 0x0006C88D
	public void Handle_Field_Caret(RectTransform _RectTransform)
	{
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0006E690 File Offset: 0x0006C890
	public bool Arena_Score_Death_Change_Hero()
	{
		bool result = false;
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 7;
		if (flag)
		{
			result = (this.GetGameState() == 0 || fGameManager.GetInstance().getPlayer().isDead);
		}
		return result;
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
	public void createLobbyUI()
	{
		bool flag = fUIManager.mLobbyUI != null;
		if (flag)
		{
			this.DestoryLobbyUI();
		}
		fUIManager.mLobbyUI = new GameObject("LobbyUI");
		fUIManager.mLobbyUI.transform.SetParent(fUIManager.mUIObj.transform);
		fUIManager.mLobbyUI.layer = 5;
		RectTransform rectTransform = fUIManager.mLobbyUI.AddComponent<RectTransform>();
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
		rectTransform.offsetMin = new Vector2(0f, 0f);
		rectTransform.offsetMax = new Vector2(0f, 0f);
		fUIManager.mLobbyUI.transform.localScale = Vector3.one;
		fUIManager.mLobbyUI.transform.localPosition = new Vector3(fUIManager.mLobbyUI.transform.localPosition.x, fUIManager.mLobbyUI.transform.localPosition.y, 0f);
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0006E804 File Offset: 0x0006CA04
	public void CreateGameUI()
	{
		bool flag = fUIManager.mGameUI != null;
		if (flag)
		{
			this.DestoryGameUI();
		}
		fUIManager.mGameUI = new GameObject("GameUI");
		fUIManager.mGameUI.transform.SetParent(fUIManager.mUIObj.transform);
		fUIManager.mGameUI.layer = 5;
		RectTransform rectTransform = fUIManager.mGameUI.AddComponent<RectTransform>();
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
		rectTransform.offsetMin = new Vector2(0f, 0f);
		rectTransform.offsetMax = new Vector2(0f, 0f);
		fUIManager.mGameUI.transform.localScale = Vector3.one;
		fUIManager.mGameUI.transform.localPosition = new Vector3(fUIManager.mGameUI.transform.localPosition.x, fUIManager.mGameUI.transform.localPosition.y, 0f);
		fUIManager.mGameUI.transform.SetSiblingIndex(2);
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x0006E940 File Offset: 0x0006CB40
	public void DestoryLobbyUI()
	{
		UnityEngine.Object.DestroyImmediate(fUIManager.mLobbyUI);
		this.winLobbyList = new Dictionary<string, GameObject>();
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x0006E959 File Offset: 0x0006CB59
	public void DestoryGameUI()
	{
		UnityEngine.Object.DestroyImmediate(fUIManager.mGameUI);
		this.winGameList = new Dictionary<string, GameObject>();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0006E974 File Offset: 0x0006CB74
	public static fUIManager GetInstance()
	{
		bool flag = fUIManager.mInst == null;
		if (flag)
		{
			fUIManager.mUIObj = new GameObject("_fUIManager");
			fUIManager.mUIObj.layer = LayerMask.NameToLayer("UI");
			UnityEngine.Object.DontDestroyOnLoad(fUIManager.mUIObj);
			GameObject gameObject = new GameObject("EventSystem");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.AddComponent<EventSystem>();
			gameObject.AddComponent<StandaloneInputModule>();
			fRenderTextureManager.GetInstance();
			bool flag2 = fUIManager.mUIObj;
			if (flag2)
			{
				fUIManager.initUiCamera();
				fUIManager.mInst = fUIManager.mUIObj.AddComponent<fUIManager>();
				UnityEngine.Object.DontDestroyOnLoad(fUIManager.mInst);
				fUIManager.mCanObj = fUIManager.mUIObj.AddComponent<Canvas>();
				UnityEngine.Object.DontDestroyOnLoad(fUIManager.mCanObj);
				fUIManager.mCanObj.renderMode = RenderMode.ScreenSpaceCamera;
				fUIManager.mCanObj.worldCamera = fUIManager.uiCamera;
				fUIManager.mCanScalObj = fUIManager.mUIObj.AddComponent<CanvasScaler>();
				fUIManager.mCanScalObj.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				fUIManager.mCanScalObj.referenceResolution = new Vector2(SysConfig.ScreenWidth, SysConfig.ScreenHeight);
				fUIManager.mCanScalObj.matchWidthOrHeight = 0.5f;
				UnityEngine.Object.DontDestroyOnLoad(fUIManager.mCanScalObj);
				fUIManager.mGrapRayObj = fUIManager.mUIObj.AddComponent<GraphicRaycaster>();
				UnityEngine.Object.DontDestroyOnLoad(fUIManager.mGrapRayObj);
				fUIManager.mInst.createLobbyUI();
				fUIManager.mInst.CreateGameUI();
				fUIManager.mPublicUI = new GameObject("PublicUI");
				fUIManager.mPublicUI.transform.SetParent(fUIManager.mUIObj.transform);
				fUIManager.mPublicUI.layer = 5;
				RectTransform rectTransform = fUIManager.mPublicUI.AddComponent<RectTransform>();
				rectTransform.anchorMax = new Vector2(1f, 1f);
				rectTransform.anchorMin = new Vector2(0f, 0f);
				rectTransform.pivot = new Vector2(0.5f, 0.5f);
				rectTransform.offsetMin = new Vector2(0f, 0f);
				rectTransform.offsetMax = new Vector2(0f, 0f);
				fUIManager.mPublicUI.transform.localScale = Vector3.one;
				fUIManager.mPublicUI.transform.localPosition = new Vector3(fUIManager.mPublicUI.transform.localPosition.x, fUIManager.mPublicUI.transform.localPosition.y, 0f);
				fUIManager.mGameLoadUI = new GameObject("GameLoadUI");
				fUIManager.mGameLoadUI.transform.SetParent(fUIManager.mUIObj.transform);
				fUIManager.mGameLoadUI.layer = 5;
				RectTransform rectTransform2 = fUIManager.mGameLoadUI.AddComponent<RectTransform>();
				rectTransform2.anchorMax = new Vector2(1f, 1f);
				rectTransform2.anchorMin = new Vector2(0f, 0f);
				rectTransform2.pivot = new Vector2(0.5f, 0.5f);
				rectTransform2.offsetMin = new Vector2(0f, 0f);
				rectTransform2.offsetMax = new Vector2(0f, 0f);
				fUIManager.mGameLoadUI.transform.localScale = Vector3.one;
				fUIManager.mGameLoadUI.transform.localPosition = new Vector3(fUIManager.mGameLoadUI.transform.localPosition.x, fUIManager.mGameLoadUI.transform.localPosition.y, 0f);
				fUIManager.mGameUISound = new GameObject("_fUISoundManager");
				fUIManager.mGameUISound.transform.SetParent(fUIManager.mUIObj.transform);
				fUIManager.mGameUISound.layer = 5;
				fUIManager.mGameUISound.AddComponent<fUISoundManager>();
				RectTransform rectTransform3 = fUIManager.mGameUISound.AddComponent<RectTransform>();
				rectTransform3.anchorMax = new Vector2(1f, 1f);
				rectTransform3.anchorMin = new Vector2(0f, 0f);
				rectTransform3.pivot = new Vector2(0.5f, 0.5f);
				rectTransform3.offsetMin = new Vector2(0f, 0f);
				rectTransform3.offsetMax = new Vector2(0f, 0f);
				bool flag3 = WebParam.client_language == "baxi";
				if (flag3)
				{
					fUIManager.mFont = Resources.Load<Font>("DIN Light");
					fUIManager.mNewComic = Resources.Load<Font>("newcomictitle");
					fUIManager.mDINMedium = Resources.Load<Font>("DIN Medium");
				}
				else
				{
					fUIManager.mFont = Resources.Load<Font>("Adobe-Heiti-Std-hs");
				}
				bool flag4 = fUIManager.mFont == null;
				if (flag4)
				{
					Debug.LogError(" Font not found ! ");
				}
				bool flag5 = fUIManager.mProgress == null;
				if (flag5)
				{
					GameObject gameObject2 = (GameObject)Resources.Load("LoadingProgress");
					fUIManager.mProgress = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
					fUIManager.mProgress.transform.SetParent(fUIManager.mGameLoadUI.transform);
					fUIManager.mProgress.transform.localScale = Vector3.one;
					fUIManager.mProgress.transform.localPosition = Vector3.zero;
					RectTransform component = gameObject2.GetComponent<RectTransform>();
					component.sizeDelta = new Vector2(0f, 0f);
					component.anchoredPosition = new Vector2(0f, 0f);
					fUIManager.mProgress.name = "GameProgress";
					Text[] componentsInChildren = fUIManager.mProgress.GetComponentsInChildren<Text>(true);
					foreach (Text text in componentsInChildren)
					{
						bool flag6 = text.name == "ArialText";
						if (flag6)
						{
							fUIManager.mArialFont = text.font;
						}
						else
						{
							text.font = fUIManager.mFont;
						}
					}
					bool flag7 = fUIManager.mArialFont == null;
					if (flag7)
					{
						Debug.LogError(" Font Arial not found ! ");
					}
					fUIManager.mUILoading = fUIManager.mProgress.AddComponent<UILoading>();
					fUIManager.mProgress.SetActive(true);
				}
				fUIManager.VoiceGameObj = new GameObject("VoiceGameObj");
				fUIManager.VoiceGameObj.transform.SetParent(fUIManager.mUIObj.transform);
				fUIManager.VoiceGameObj.AddComponent<VoiceGameCon>();
			}
			else
			{
				Debuger.OutError("_fUIManager obj null");
			}
		}
		return fUIManager.mInst;
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0006EFC4 File Offset: 0x0006D1C4
	private static void initUiCamera()
	{
		bool flag = fUIManager.uiCamera == null;
		if (flag)
		{
			GameObject gameObject = new GameObject("uiCamera");
			fUIManager.uiCamera = gameObject.AddComponent<Camera>();
			fUIManager.uiCamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
			fUIManager.uiCamera.orthographic = true;
			fUIManager.uiCamera.orthographicSize = 5f;
			fUIManager.uiCamera.clearFlags = CameraClearFlags.Depth;
			fUIManager.uiCamera.depth = 100f;
			gameObject.transform.position = new Vector3(-1000f, -1000f, -1000f);
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			fUIManager.tipBlur = gameObject.AddComponent<tipBlurEff>();
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0006F080 File Offset: 0x0006D280
	public void ShowWin(string sUI, NetMsg.UITRANSFROM sLayer, fUIManager.UILoadFinishDelegate func = null, object szParam = null, bool DontDestroy = true)
	{
		GameObject gameObject = null;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(sUI);
		arrayList.Add(sLayer);
		arrayList.Add(func);
		arrayList.Add(szParam);
		arrayList.Add(DontDestroy);
		bool flag = this.winList.ContainsKey(sUI);
		if (flag)
		{
			gameObject = this.winList[sUI];
		}
		else
		{
			bool flag2 = this.winLobbyList.ContainsKey(sUI);
			if (flag2)
			{
				gameObject = this.winLobbyList[sUI];
			}
			else
			{
				bool flag3 = this.winGameList.ContainsKey(sUI);
				if (flag3)
				{
					gameObject = this.winGameList[sUI];
				}
			}
		}
		bool flag4 = gameObject != null;
		if (flag4)
		{
			Window window = (Window)gameObject.GetComponent(sUI + this.CodeCon);
			window.Open();
			window.Reset();
			if (func != null)
			{
				func(sUI, gameObject, szParam);
			}
		}
		else
		{
			sUI = ((!(sUI == "MainUI") || !(WebParam.client_language == "baxi")) ? ("UI/" + sUI.ToLower() + ".ab") : "UI/mainui_baxi.ab");
			GameRoot.LoadAsset(sUI, new fResourceManager.DownFinishDelegate(this.LoadUIOK), arrayList);
		}
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x0006F1CC File Offset: 0x0006D3CC
	public void LoadUIOK(WWW www, object customParam)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (flag)
		{
			Debug.LogError("LoadUIOK资源有问题");
		}
		else
		{
			GameObject gameObject = null;
			Transform transform = null;
			ArrayList arrayList = (ArrayList)customParam;
			string text = arrayList[0].ToString();
			bool flag2 = (bool)arrayList[4];
			NetMsg.UITRANSFROM uitransfrom = (NetMsg.UITRANSFROM)arrayList[1];
			bool flag3 = this.winList.ContainsKey(text) || this.winLobbyList.ContainsKey(text) || this.winGameList.ContainsKey(text);
			if (!flag3)
			{
				bool isDone = www.isDone;
				if (isDone)
				{
					UnityEngine.Object[] array = www.assetBundle.LoadAllAssets();
					UnityEngine.Object[] array2 = array;
					foreach (UnityEngine.Object original in array2)
					{
						gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
						Text[] componentsInChildren = gameObject.GetComponentsInChildren<Text>(true);
						bool flag4 = componentsInChildren != null;
						if (flag4)
						{
							foreach (Text text2 in componentsInChildren)
							{
								text2.font = fUIManager.mFont;
								text2.fontStyle = FontStyle.Normal;
								bool flag5 = WebParam.client_language == "zh";
								if (flag5)
								{
									text2.lineSpacing *= 0.7f;
								}
								int nMsgID;
								bool flag6 = int.TryParse(text2.text, out nMsgID);
								if (flag6)
								{
									text2.text = Language.GetLang(nMsgID, new object[0]);
								}
							}
						}
						InputField[] componentsInChildren2 = gameObject.GetComponentsInChildren<InputField>(true);
						bool flag7 = componentsInChildren2 != null;
						if (flag7)
						{
							foreach (InputField inputField in componentsInChildren2)
							{
								Text text2 = inputField.textComponent;
								bool flag8 = text2 != null;
								if (flag8)
								{
									text2.font = fUIManager.mArialFont;
								}
							}
						}
						foreach (object obj in gameObject.transform)
						{
							Transform transform2 = (Transform)obj;
							transform = transform2;
							Vector2 offsetMin = transform2.GetComponent<RectTransform>().offsetMin;
							Vector2 offsetMax = transform2.GetComponent<RectTransform>().offsetMax;
							switch (uitransfrom)
							{
							case NetMsg.UITRANSFROM.LobbyUI:
								transform2.SetParent(fUIManager.mLobbyUI.transform);
								break;
							case NetMsg.UITRANSFROM.GameUI:
								transform2.SetParent(fUIManager.mGameUI.transform);
								break;
							case NetMsg.UITRANSFROM.GameLoadUI:
								transform2.SetParent(fUIManager.mGameLoadUI.transform);
								break;
							case NetMsg.UITRANSFROM.PublicUI:
								transform2.SetParent(fUIManager.mPublicUI.transform);
								break;
							default:
								transform2.SetParent(base.transform);
								break;
							}
							transform2.GetComponent<RectTransform>().offsetMin = offsetMin;
							transform2.GetComponent<RectTransform>().offsetMax = offsetMax;
							Vector3 anchoredPosition3D = transform2.GetComponent<RectTransform>().anchoredPosition3D;
							transform2.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(anchoredPosition3D.x, anchoredPosition3D.y, 0f);
						}
						UnityEngine.Object.Destroy(gameObject);
					}
				}
				else
				{
					Debug.Log("LoadUIOK Err:" + customParam.ToString());
				}
				bool flag9 = transform == null;
				if (!flag9)
				{
					transform.transform.localScale = Vector3.one;
					Type type = Type.GetType(text + this.CodeCon);
					bool flag10 = type != null;
					if (flag10)
					{
						bool flag11 = type.BaseType.Name != "Window";
						if (flag11)
						{
							Debug.LogError(string.Empty + text + "Con.cs Not expand Window =============================================");
							return;
						}
						Window window = (Window)transform.gameObject.AddComponent(type);
						window.init(text);
						NetMsg.UITRANSFROM uitransfrom2 = uitransfrom;
						NetMsg.UITRANSFROM uitransfrom3 = uitransfrom2;
						if (uitransfrom3 != NetMsg.UITRANSFROM.LobbyUI)
						{
							if (uitransfrom3 != NetMsg.UITRANSFROM.GameUI)
							{
								bool flag12 = !this.winList.ContainsKey(text);
								if (flag12)
								{
									this.winList.Add(text, transform.gameObject);
								}
							}
							else
							{
								bool flag13 = !this.winGameList.ContainsKey(text);
								if (flag13)
								{
									this.winGameList.Add(text, transform.gameObject);
								}
							}
						}
						else
						{
							bool flag14 = !this.winLobbyList.ContainsKey(text);
							if (flag14)
							{
								this.winLobbyList.Add(text, transform.gameObject);
							}
						}
						bool flag15 = arrayList[2] != null;
						if (flag15)
						{
							fUIManager.UILoadFinishDelegate uiloadFinishDelegate = (fUIManager.UILoadFinishDelegate)arrayList[2];
							if (uiloadFinishDelegate != null)
							{
								uiloadFinishDelegate(text, transform.gameObject, arrayList[3]);
							}
						}
					}
					else
					{
						Debug.LogError(string.Empty + text + "Con.cs not find=============================================");
					}
					bool flag16 = flag2;
					if (flag16)
					{
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
			}
		}
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0006F6EC File Offset: 0x0006D8EC
	public void SetWinActive(string sUI, bool state)
	{
		bool flag = this.winList.ContainsKey(sUI);
		if (flag)
		{
			GameObject gameObject = this.winList[sUI];
			Window component = gameObject.GetComponent<Window>();
			if (state)
			{
				component.Open();
				component.Reset();
			}
			else
			{
				component.Close();
			}
		}
		bool flag2 = this.winLobbyList.ContainsKey(sUI);
		if (flag2)
		{
			GameObject gameObject = this.winLobbyList[sUI];
			Window component2 = gameObject.GetComponent<Window>();
			if (state)
			{
				component2.Open();
				component2.Reset();
			}
			else
			{
				component2.Close();
			}
		}
		bool flag3 = this.winGameList.ContainsKey(sUI);
		if (flag3)
		{
			GameObject gameObject = this.winGameList[sUI];
			Window component3 = gameObject.GetComponent<Window>();
			if (state)
			{
				component3.Open();
				component3.Reset();
			}
			else
			{
				component3.Close();
			}
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0006F7E0 File Offset: 0x0006D9E0
	public void ProgressChange(bool boo, string proName, float v, float vBig = 1f)
	{
		this._LoadFlag = true;
		this.booldata = boo;
		bool flag = this.booldata;
		if (flag)
		{
			this._LoadFlag = true;
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0006F810 File Offset: 0x0006DA10
	public static void SetProgreessShow(bool bl)
	{
		fUIManager.IsLoading = bl;
		bool isLoading = fUIManager.IsLoading;
		if (isLoading)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (bl)
		{
			roleuiManager.GetInstance().OnCloseSelectHeroUI();
		}
		fUIManager.mProgress.SetActive(bl);
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0006F86D File Offset: 0x0006DA6D
	private IEnumerator DoTimeEnd()
	{
		yield return new WaitForSeconds(1f);
		this._LoadFlag = false;
		bool flag = !this.booldata;
		if (flag)
		{
			fUIManager.SetProgreessShow(this.booldata);
		}
		yield break;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0006F87C File Offset: 0x0006DA7C
	public void SetMapEffect()
	{
		bool flag = PlayerDataManager.getInstance().playercom == null;
		if (!flag)
		{
			FbXmlData fbXmlData = null;
			FbModelXmlData fbModelXmlData = null;
			bool flag2 = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
			if (flag2)
			{
				fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
			}
			bool flag3 = fbXmlData == null;
			if (!flag3)
			{
				bool flag4 = fGameManager.mloadConfig.FbModeData.ContainsKey(fbXmlData.nstartplotid);
				if (flag4)
				{
					fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
				}
				bool flag5 = fbModelXmlData == null;
				if (!flag5)
				{
					bool flag6 = fbModelXmlData.objname1 != 0 && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname1.ToString());
					if (flag6)
					{
						fUIManager.MapEffectObj1 = fUIManager.GetInstance().MapEffecyObj["MapEffect_" + fbModelXmlData.objname1.ToString()];
					}
					bool flag7 = fbModelXmlData.objname2 != 0 && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname2.ToString());
					if (flag7)
					{
						fUIManager.MapEffectObj2 = fUIManager.GetInstance().MapEffecyObj["MapEffect_" + fbModelXmlData.objname2.ToString()];
					}
					bool flag8 = fbModelXmlData.objname3 != 0 && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname3.ToString());
					if (flag8)
					{
						fUIManager.MapEffectObj3 = fUIManager.GetInstance().MapEffecyObj["MapEffect_" + fbModelXmlData.objname3.ToString()];
					}
					bool flag9 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
					if (flag9)
					{
						bool flag10 = fUIManager.MapEffectObj1 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname1.ToString());
						if (flag10)
						{
							fUIManager.MapEffectObj1.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(false);
						}
						bool flag11 = fUIManager.MapEffectObj2 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname2.ToString());
						if (flag11)
						{
							fUIManager.MapEffectObj2.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(false);
						}
						bool flag12 = fUIManager.MapEffectObj3 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname2.ToString());
						if (flag12)
						{
							fUIManager.MapEffectObj3.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(false);
						}
					}
					else
					{
						bool flag13 = fUIManager.MapEffectObj1 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname1.ToString());
						if (flag13)
						{
							fUIManager.MapEffectObj1.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
						}
						bool flag14 = fUIManager.MapEffectObj2 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname2.ToString());
						if (flag14)
						{
							fUIManager.MapEffectObj2.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
						}
						bool flag15 = fUIManager.MapEffectObj3 != null && fUIManager.GetInstance().MapEffecyObj.ContainsKey("MapEffect_" + fbModelXmlData.objname2.ToString());
						if (flag15)
						{
							fUIManager.MapEffectObj3.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
						}
					}
					bool flag16 = ZhanDianCon.inst != null;
					if (flag16)
					{
						ZhanDianCon.inst.LoadMapEffect();
					}
				}
			}
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0006FCBC File Offset: 0x0006DEBC
	public GameObject GetUI(string sUI)
	{
		bool flag = this.winList.ContainsKey(sUI);
		GameObject result;
		if (flag)
		{
			result = this.winList[sUI];
		}
		else
		{
			bool flag2 = this.winLobbyList.ContainsKey(sUI);
			if (flag2)
			{
				result = this.winLobbyList[sUI];
			}
			else
			{
				bool flag3 = this.winGameList.ContainsKey(sUI);
				if (flag3)
				{
					result = this.winGameList[sUI];
				}
				else
				{
					result = null;
				}
			}
		}
		return result;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x0006FD30 File Offset: 0x0006DF30
	public void Open_Or_Close_Arena_UI(bool _State)
	{
		bool flag = !(ArenaCon.inst == null);
		if (flag)
		{
			ArenaCon.inst.gameObject.SetActive(_State);
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0006FD64 File Offset: 0x0006DF64
	public void Open_Or_Close_Arena_Score_UI(bool _State)
	{
		bool flag = !(Arena_ScoreCon.inst == null);
		if (flag)
		{
			Arena_ScoreCon.inst.gameObject.SetActive(_State);
		}
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0006FD98 File Offset: 0x0006DF98
	public void Open_Or_Close_Scramble_UI(bool _State)
	{
		bool flag = !(ScrambleCon.inst == null);
		if (flag)
		{
			ScrambleCon.inst.gameObject.SetActive(_State);
		}
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x0006FDCC File Offset: 0x0006DFCC
	public void CloseAll()
	{
		Debug.Log("MATCH_DEBUG: CloseAll called");
		bool flag = Gem_UICon.Instance != null && Gem_UICon.Instance.visible;
		if (flag)
		{
			Gem_UICon.Instance.Close();
		}
		GameRoot.inst.Setting.Close_Setting_Option(null);
		bool flag2 = SettingCon.Instance != null && SettingCon.Instance.visible;
		if (flag2)
		{
			GameRoot.inst.Setting.View_Component.Close(null);
		}
		bool flag3 = HeroSkillInfoUICon.inst != null;
		if (flag3)
		{
			HeroSkillInfoUICon.inst.isShowClose = false;
			this.SetWinActive("HeroSkillInfoUI", false);
		}
		bool flag4 = easy_tipCon.inst != null;
		if (flag4)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
		this.SetWinActive("CreateRoleUI", false);
		this.SetWinActive("FaceUI", false);
		this.SetWinActive("GMUI", false);
		this.SetWinActive("VoiceUI", false);
		TipManager.GetInstance().HideTextTip();
		this.createLobbyUI();
		fUIManager.mGameUI.SetActive(true);
		bool flag5 = ShopUICon.inst != null;
		if (flag5)
		{
			ShopUICon.inst.Close();
		}
		bool flag6 = PrerogativeCon.Instance != null && PrerogativeCon.Instance.visible;
		if (flag6)
		{
			PrerogativeCon.Instance.Close();
		}
		bool flag7 = QQ_Games_Hall_PrerogativeCon.Instance != null && QQ_Games_Hall_PrerogativeCon.Instance.visible;
		if (flag7)
		{
			QQ_Games_Hall_PrerogativeCon.Instance.Close();
		}
		bool flag8 = ShopBuyItemUICon.inst != null;
		if (flag8)
		{
			ShopBuyItemUICon.inst.OnCloseClick(null);
		}
		bool flag9 = QingHuaJinJieUICon.inst != null;
		if (flag9)
		{
			QingHuaJinJieUICon.inst.Close();
		}
		bool flag10 = LiBaoUICon.inst != null;
		if (flag10)
		{
			LiBaoUICon.inst.Close();
		}
		bool flag11 = ChongZhiUICon.inst != null;
		if (flag11)
		{
			ChongZhiUICon.inst.Close();
		}
		bool flag12 = WenJuanUICon.inst != null;
		if (flag12)
		{
			WenJuanUICon.inst.Close();
		}
		bool flag13 = MailUICon.inst != null;
		if (flag13)
		{
			MailUICon.inst.Close();
		}
		bool flag14 = jointestserverCon.inst != null;
		if (flag14)
		{
			jointestserverCon.inst.Close();
		}
		bool flag15 = KingJieSuanUICon.inst != null && KingJieSuanUICon.inst.visible;
		if (flag15)
		{
			KingJieSuanUICon.inst.OnReBackMain(true);
		}
		bool flag16 = QiangHuaUICon.inst != null && QiangHuaUICon.inst.visible;
		if (flag16)
		{
			QiangHuaUICon.inst.Close();
		}
		PveManager.GetInstance().OnDelPveRefresh();
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000700A0 File Offset: 0x0006E2A0
	public void ShowMainUIBackGround()
	{
		bool flag = BackgroundUICon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("BackgroundUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.loadbackuiok), null, true);
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000700DE File Offset: 0x0006E2DE
	private void loadbackuiok(string sUI, GameObject ob, object szParam)
	{
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000700E4 File Offset: 0x0006E2E4
	public void CloseGameUI()
	{
		this.DestoryGameUI();
		this.CreateGameUI();
		TipManager.GetInstance().HideTextTip();
		this.createLobbyUI();
		this.ShowMainUIBackGround();
		bool flag = fGameManager.LobbyPlayer != null;
		if (flag)
		{
			fGameManager.LobbyPlayer.ShowMainUI();
		}
		this.SetWinActive("HeroSkillInfoUI", false);
		this.SetWinActive("VoiceUI", false);
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x0007014C File Offset: 0x0006E34C
	public void GotoHall(int nreason = 0)
	{
		CPlayerRexx.Instance.exit();
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			GameRoot.inst.ClearGMCameraData();
			fGameManager.bGoToHall = true;
			this.Close_UI();
			this.CloseSheJiaoWin();
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.Close();
				gameuiCon.inst.CloseResultSence();
			}
			bool flag3 = RankUICon.inst != null;
			if (flag3)
			{
				RankUICon.inst.Close();
			}
			fGameManager.Heroplayer.SendMessageToServer(20001, new ArrayList
			{
				nreason,
				0
			});
			fGameManager.GetInstance().nNowScnID = 0;
			CSoundManager.Instance.maxDisParm = 1f;
			bool flag4 = nreason != 0;
			if (flag4)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(20002, new ArrayList
				{
					nreason,
					0
				});
			}
		}
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0007025C File Offset: 0x0006E45C
	public void InitRoomData()
	{
		PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		bool flag = RoomManage.GetInstance().PRoomData != null;
		if (flag)
		{
			RoomManage.GetInstance().PRoomData.ClearRoomUserData();
		}
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0007029D File Offset: 0x0006E49D
	public void Close_UI()
	{
		this.Close_UI(Arena_Message_Handle.Instantiation.UI_Object);
		this.Close_UI(Scramble_Message_Handle.Instantiation.UI_Object);
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x000702C4 File Offset: 0x0006E4C4
	private void Close_UI(GameObject _UI_Object)
	{
		bool flag = !(_UI_Object == null);
		if (flag)
		{
			_UI_Object.SetActive(false);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000702EC File Offset: 0x0006E4EC
	public void GetIconAsset(string sUrl, RawImage rawimg)
	{
		bool flag = !(rawimg != null);
		if (!flag)
		{
			GetIconComponent getIconComponent = rawimg.GetComponent<GetIconComponent>();
			bool flag2 = getIconComponent == null;
			if (flag2)
			{
				getIconComponent = rawimg.gameObject.AddComponent<GetIconComponent>();
				getIconComponent._Url = sUrl;
				bool flag3 = rawimg != null && rawimg.texture != null;
				if (flag3)
				{
					rawimg.texture = null;
				}
			}
			else
			{
				bool flag4 = getIconComponent._Url != sUrl;
				if (flag4)
				{
					getIconComponent._Url = sUrl;
				}
				else
				{
					bool flag5 = rawimg.texture != null;
					if (flag5)
					{
						rawimg.enabled = true;
						return;
					}
				}
				getIconComponent.OnDisable();
			}
			rawimg.enabled = false;
			getIconComponent.IsShow = true;
			GameRoot.LoadAsset("Icon/" + sUrl, new fResourceManager.DownFinishDelegate(this.LoadIconOk), rawimg);
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x000703D0 File Offset: 0x0006E5D0
	private void LoadIconOk(WWW www, object customParam)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (flag)
		{
			Debug.LogError("资源问题1");
		}
		else
		{
			RawImage rawImage = (RawImage)customParam;
			bool flag2 = rawImage == null || !(rawImage.texture == null);
			if (!flag2)
			{
				GetIconComponent component = rawImage.GetComponent<GetIconComponent>();
				bool flag3 = component != null;
				if (flag3)
				{
					bool isShow = component.IsShow;
					if (isShow)
					{
						bool flag4 = rawImage.texture == null;
						if (flag4)
						{
							rawImage.texture = new Texture2D(4, 4);
						}
						www.LoadImageIntoTexture((Texture2D)rawImage.texture);
						rawImage.enabled = true;
					}
				}
				else
				{
					Debug.Log("LoadIconOk GetPhotoUrl null");
				}
			}
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000704A0 File Offset: 0x0006E6A0
	public int GetNowGameState()
	{
		int num = -1;
		FbXmlData fbXmlData = null;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		if (flag)
		{
			fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
		}
		bool flag2 = fbXmlData == null;
		int result;
		if (flag2)
		{
			result = num;
		}
		else
		{
			switch (fbXmlData.nType)
			{
			case 1:
			case 2:
			case 3:
			{
				bool flag3 = ZhanDianCon.inst != null;
				if (flag3)
				{
					num = ZhanDianCon.inst.m_nGameState;
				}
				break;
			}
			case 4:
			{
				bool flag4 = ArenaCon.inst != null;
				if (flag4)
				{
					num = (ArenaCon.inst.Fight_State ? 1 : 0);
				}
				break;
			}
			case 5:
			{
				bool flag5 = ScrambleCon.inst != null;
				if (flag5)
				{
					num = (ScrambleCon.inst.Fight_State ? 1 : 0);
				}
				break;
			}
			case 6:
				num = 1;
				break;
			case 7:
			{
				bool flag6 = Arena_ScoreCon.inst != null;
				if (flag6)
				{
					num = (Arena_ScoreCon.inst.Fight_State ? 1 : 0);
				}
				break;
			}
			case 9:
			case 12:
				num = KingTrainManager.GetInstance().GetGameState();
				break;
			case 10:
			case 11:
				num = 1;
				break;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000705F8 File Offset: 0x0006E7F8
	public int GetGameState()
	{
		int num = 0;
		FbXmlData fbXmlData = null;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		if (flag)
		{
			fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
		}
		bool flag2 = fbXmlData == null;
		int result;
		if (flag2)
		{
			result = num;
		}
		else
		{
			int nType = fbXmlData.nType;
			int num2 = nType;
			if (num2 <= 7)
			{
				if (num2 != 4)
				{
					if (num2 == 7)
					{
						num = 1;
					}
				}
				else
				{
					bool flag3 = ArenaCon.inst != null;
					if (flag3)
					{
						num = (ArenaCon.inst.Fight_State ? 1 : 0);
					}
				}
			}
			else if (num2 == 9 || num2 == 12)
			{
				num = KingTrainManager.GetInstance().GetFuhuoState();
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x000706C0 File Offset: 0x0006E8C0
	public int GetGameTime()
	{
		FbXmlData fbXmlData = null;
		int num = -1;
		int num2 = -1;
		DateTime d = DateTime.MinValue;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		if (flag)
		{
			fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
		}
		bool flag2 = fbXmlData == null;
		int result;
		if (flag2)
		{
			result = num2;
		}
		else
		{
			switch (fbXmlData.nType)
			{
			case 1:
			case 2:
			case 3:
			{
				bool flag3 = ZhanDianCon.inst != null;
				if (flag3)
				{
					d = ZhanDianCon.inst.m_dStartChangeTime;
				}
				break;
			}
			case 4:
			{
				bool flag4 = ArenaCon.inst != null;
				if (flag4)
				{
					d = ArenaCon.inst.m_dStartChangeTime;
				}
				break;
			}
			case 5:
			{
				bool flag5 = ScrambleCon.inst != null;
				if (flag5)
				{
					d = ScrambleCon.inst.m_dStartChangeTime;
				}
				break;
			}
			}
			bool flag6 = d != DateTime.MinValue;
			if (flag6)
			{
				num = (int)(d - Fun.GetServerDate()).TotalSeconds;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x000707E4 File Offset: 0x0006E9E4
	public int GetGameRound()
	{
		FbXmlData fbXmlData = null;
		int num = -1;
		bool flag = Application.platform != RuntimePlatform.WindowsPlayer;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
			if (flag2)
			{
				fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
			}
			bool flag3 = fbXmlData == null;
			if (flag3)
			{
				result = -1;
			}
			else
			{
				int nType = fbXmlData.nType;
				int num2 = nType;
				if (num2 != 3)
				{
					if (num2 == 4)
					{
						bool flag4 = ArenaCon.inst != null;
						if (flag4)
						{
							num = ArenaCon.inst.Current_Round;
						}
					}
				}
				else
				{
					bool flag5 = ZhanDianCon.inst != null;
					if (flag5)
					{
						num = ZhanDianCon.inst.m_nRound;
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x000708B8 File Offset: 0x0006EAB8
	public int GetGameFbType()
	{
		bool flag = !fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
			result = fbXmlData.nType;
		}
		return result;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00070914 File Offset: 0x0006EB14
	public void GetTabData()
	{
		bool flag = fGameManager.Heroplayer != null && MatchManager.GetInstance().m_pFbData.nType != 6 && MatchManager.GetInstance().m_nMatchType != 3 && !fGameManager.IsSingle && (!(gameuiCon.inst != null) || !gameuiCon.inst.mainRoundUI.mboolstat);
		if (flag)
		{
			this.bTabUIOpen = true;
			fGameManager.SendMsgToServer(5025, new ArrayList
			{
				0,
				0
			});
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x000709AC File Offset: 0x0006EBAC
	public void OpenGameTabWin()
	{
		bool flag = fGameManager.Heroplayer == null || ChatManager.ChatInput || !this.bTabUIOpen || (GameResultUICon.Inst != null && GameResultUICon.Inst.visible);
		if (!flag)
		{
			bool flag2 = GameTabUICon.Inst == null;
			if (flag2)
			{
				this.ShowWin("GameTabUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.ShowGameTabUI), null, true);
			}
			else
			{
				bool flag3 = !GameTabUICon.Inst.visible;
				if (flag3)
				{
					GameTabUICon.Inst.Open();
				}
				GameTabUICon.Inst.OnShowWin();
			}
		}
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00070A4C File Offset: 0x0006EC4C
	public void ShowGameTabUI(string sUI, GameObject ob, object szParam)
	{
		bool flag = GameTabUICon.Inst != null;
		if (flag)
		{
			bool flag2 = this.bTabUIOpen;
			if (flag2)
			{
				GameTabUICon.Inst.OnShowWin();
			}
			else
			{
				GameTabUICon.Inst.Close();
			}
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00070A94 File Offset: 0x0006EC94
	public void CloseTabWin()
	{
		this.bTabUIOpen = false;
		bool flag = GameTabUICon.Inst != null && GameTabUICon.Inst.visible;
		if (flag)
		{
			GameTabUICon.Inst.Close();
		}
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00070AD4 File Offset: 0x0006ECD4
	public void ShowGameResultUI()
	{
		bool flag = !HeroPlayer.isMouseShow;
		if (flag)
		{
			HeroPlayer.isPlayerControl = false;
			HeroPlayer.isMouseShow = true;
		}
		bool flag2 = gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
		fUIManager.GetInstance().CloseTabWin();
		ChatManager.mInst.OnShowOrHideChatWin(false);
		this.OnLoadResultUI(1);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00070B37 File Offset: 0x0006ED37
	public void OnLoadResultUI(int nType)
	{
		this.ShowWin("GameResultUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.GameResultUIEnd), nType, true);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00070B5C File Offset: 0x0006ED5C
	public void GameResultUIEnd(string sUI, GameObject ob, object szParam)
	{
		bool flag = (int)szParam != 0;
		if (flag)
		{
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.CloseResultSence();
			}
			bool flag3 = GameResultUICon.Inst != null;
			if (flag3)
			{
				GameResultUICon.Inst.OnShowWin();
			}
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00070BB4 File Offset: 0x0006EDB4
	public void CloseResultWin()
	{
		bool flag = GameResultUICon.Inst != null;
		if (flag)
		{
			GameResultUICon.Inst.Close();
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00070BE0 File Offset: 0x0006EDE0
	public void ShowHeroSkillInfo()
	{
		int num = 0;
		int num2 = 0;
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			num = ((!(SelectHeroUICon.inst != null) || !SelectHeroUICon.inst.visible) ? fGameManager.Heroplayer.playerCommon.m_nModelIndex : SelectHeroUICon.inst.m_nSeletHero);
			num2 = 1;
			bool flag2 = fGameManager.Heroplayer.playerCommon.m_nGM > 0 && fGameManager.Heroplayer.playerCommon.m_szGMTran.Count > 0;
			if (flag2)
			{
				num = 0;
			}
		}
		else
		{
			bool flag3 = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
			if (flag3)
			{
				num = HeroInfoUICon.inst.nSlcHeroId;
				num2 = 0;
			}
		}
		bool flag4 = num > 0;
		if (flag4)
		{
			bool flag5 = HeroSkillInfoUICon.inst == null;
			if (flag5)
			{
				this.ShowWin("HeroSkillInfoUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnLoadHeroSkill), new ArrayList
				{
					num2,
					num
				}, true);
			}
			else
			{
				this.SetWinActive("HeroSkillInfoUI", true);
				HeroSkillInfoUICon.inst.ShowHeroSkill(num2, num);
			}
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00070D12 File Offset: 0x0006EF12
	public void CloseHeroSkillInfo()
	{
		HeroSkillInfoUICon.isClosed = true;
		this.SetWinActive("HeroSkillInfoUI", false);
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00070D28 File Offset: 0x0006EF28
	private void OnLoadHeroSkill(string sUI, GameObject UIObj, object szParam)
	{
		ArrayList arrayList = (ArrayList)szParam;
		bool flag = HeroSkillInfoUICon.inst != null;
		if (flag)
		{
			HeroSkillInfoUICon.inst.ShowHeroSkill((int)arrayList[0], (int)arrayList[1]);
		}
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00070D74 File Offset: 0x0006EF74
	public static void OnClearObj(List<GameObject> szList)
	{
		for (int i = 0; i < szList.Count; i++)
		{
			bool flag = szList[i] == null;
			if (!flag)
			{
				RawImage[] componentsInChildren = szList[i].GetComponentsInChildren<RawImage>(true);
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					bool flag2 = componentsInChildren[j] != null && componentsInChildren[j].texture != null;
					if (flag2)
					{
						UnityEngine.Object.DestroyImmediate(componentsInChildren[j].texture);
					}
				}
				UnityEngine.Object.DestroyImmediate(szList[i]);
			}
		}
		szList.Clear();
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00070E1C File Offset: 0x0006F01C
	public static void OnClearObj(Dictionary<int, GameObject> objList)
	{
		foreach (GameObject gameObject in objList.Values)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
		objList.Clear();
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00070E88 File Offset: 0x0006F088
	public static void OnAddRawImage(RawImage rimg)
	{
		fUIManager.m_szAllRawImgList.Add(rimg);
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00070E98 File Offset: 0x0006F098
	public static void OnDelRawImage()
	{
		for (int i = 0; i < fUIManager.m_szAllRawImgList.Count; i++)
		{
			RawImage rawImage = fUIManager.m_szAllRawImgList[i];
			bool flag = !(rawImage == null) && !(rawImage.texture == null);
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(rawImage.texture);
			}
		}
		fUIManager.m_szAllRawImgList.Clear();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00070F08 File Offset: 0x0006F108
	public static void SetGameQuality()
	{
		bool flag = fUIManager.mUIObj != null;
		if (flag)
		{
			fUIManager.mUIObj.BroadcastMessage("SetGameQuality", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x00070F38 File Offset: 0x0006F138
	public void ChangeHallResetUI()
	{
		bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.Reset();
		}
		bool flag2 = ShopUICon.inst != null && ShopUICon.inst.visible;
		if (flag2)
		{
			ShopUICon.inst.ReBack();
		}
		bool flag3 = fGameManager.bFirstLogin && !fGameManager.m_nPreLoadOff;
		if (flag3)
		{
			fUISoundManager.instance.PlayUISound("music_commen", 1, null, 10);
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00070FC8 File Offset: 0x0006F1C8
	public void Setting_Modify_Key_Complete(string _Key, string _Value)
	{
		if (_Key == "All" || _Key == "Key_Skill_1" || _Key == "Key_Skill_2" || _Key == "Key_Use_Weapon_2")
		{
			bool flag = gameuiCon.inst != null;
			if (flag)
			{
				gameuiCon.inst.NewMainHeroSkill.OnSkillKeyShow();
			}
		}
		bool flag2 = (_Key == "All" || _Key == "Key_Skill_3") && gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.NewMainDaZhao.OnSuperSkilKey();
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00071071 File Offset: 0x0006F271
	public void ShowSheJiaoUIWin()
	{
		fUIManager.GetInstance().SetWinActive("TaskUI", false);
		this.ShowWin("SheJiaoUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.GameSheJiaoUIEnd), null, true);
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x000710A0 File Offset: 0x0006F2A0
	public void GameSheJiaoUIEnd(string sUI, GameObject ob, object szParam)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.Close();
		}
		bool flag2 = SheJiaoUICon.Inst != null;
		if (flag2)
		{
			SheJiaoUICon.Inst.OnChangeTab();
		}
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000710E8 File Offset: 0x0006F2E8
	public void CloseSheJiaoWin()
	{
		bool flag = SheJiaoUICon.Inst != null;
		if (flag)
		{
			SheJiaoUICon.Inst.OnCloseUI(null);
		}
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00071114 File Offset: 0x0006F314
	public bool GetResultModelScn()
	{
		bool result = false;
		bool flag = GameShouUICon.Inst != null && GameShouUICon.Inst.modelScn != null;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00071151 File Offset: 0x0006F351
	public void OpenPlayerReportUI(ArrayList arr)
	{
		this.ShowWin("playerreport", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.OpenPlayerReportUIEnd), arr, true);
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00071170 File Offset: 0x0006F370
	public void OpenPlayerReportUIEnd(string sUI, GameObject ob, object szParam)
	{
		bool flag = playerreportCon.inst != null;
		if (flag)
		{
			ArrayList data = (ArrayList)szParam;
			playerreportCon.inst.SetData(data);
		}
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x000711A4 File Offset: 0x0006F3A4
	public void OnPushMsg(ArrayList szMsg, int nTimelong = 20)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			ScnMsgManager.GetInstance().OnAddMsg(szMsg, nTimelong);
		}
		else
		{
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.OpenSystemMsgTip(szMsg);
			}
		}
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000711EC File Offset: 0x0006F3EC
	public void OnFindOtherUserOk(int nNetId, ArrayList szMsg)
	{
		if (nNetId <= 60017)
		{
			if (nNetId <= 1312)
			{
				if (nNetId != 1311)
				{
					if (nNetId == 1312)
					{
						RoomManage.GetInstance().OnInviteRoomBack(szMsg);
					}
				}
				else
				{
					RoomManage.GetInstance().OnInviteRoom(szMsg);
				}
			}
			else
			{
				switch (nNetId)
				{
				case 60005:
					TeamManager.GetInstance().OnInviteTeam(szMsg);
					break;
				case 60006:
					TeamManager.GetInstance().DelImgCD(Fun.DecodingUTF8(szMsg[1].ToString()));
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40003, new object[]
					{
						Fun.DecodingUTF8(szMsg[1].ToString())
					}), "ffdd00", "system");
					break;
				case 60007:
					TeamManager.GetInstance().DelImgCD(Fun.DecodingUTF8(szMsg[1].ToString()));
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40012, new object[0]), "ffdd00", "system");
					break;
				default:
					if (nNetId == 60017)
					{
						TeamManager.GetInstance().DelImgCD(Fun.DecodingUTF8(szMsg[1].ToString()));
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40033, new object[0]), "ffdd00", "system");
					}
					break;
				}
			}
		}
		else if (nNetId <= 91023)
		{
			if (nNetId != 60021)
			{
				if (nNetId == 91023)
				{
					GuildManager.GetInstance().GuildServerMsgToClient(nNetId, szMsg);
				}
			}
			else
			{
				TeamManager.GetInstance().ApplyJoinTeam(szMsg);
			}
		}
		else if (nNetId != 91026)
		{
			if (nNetId == 91028)
			{
				int num = (int)szMsg[1];
				ChatManager instance = ChatManager.GetInstance();
				if (!true)
				{
				}
				int nMsgID;
				if (num != 1)
				{
					if (num != 2)
					{
						nMsgID = 38038;
					}
					else
					{
						nMsgID = 38065;
					}
				}
				else
				{
					nMsgID = 38059;
				}
				if (!true)
				{
				}
				instance.OnSendSystemMsg(Language.GetLang(nMsgID, new object[]
				{
					Fun.DecodingUTF8(szMsg[0].ToString())
				}), "ffdd00", "system");
			}
		}
		else
		{
			int num2 = (int)szMsg[0];
			bool flag = num2 == 1;
			if (flag)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(46071, new object[]
				{
					Fun.DecodingUTF8(szMsg[1].ToString())
				}), "ffdd00", "system");
			}
			else
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(46072, new object[]
				{
					Fun.DecodingUTF8(szMsg[1].ToString())
				}), "ffdd00", "system");
			}
			GuildManager.GetInstance().OnDeleteSenInvite(Fun.DecodingUTF8(szMsg[1].ToString()));
		}
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00071500 File Offset: 0x0006F700
	public void OnFindOtherUserNo(int nNetId, ArrayList szMsg, int nIsFinder, string sName, int nLevel)
	{
		if (nNetId != 60005)
		{
			if (nNetId != 91023)
			{
				if (nNetId != 91026)
				{
					bool flag = nIsFinder == 0;
					if (flag)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(46070, new object[0]), "ffdd00", "system");
					}
				}
			}
			else
			{
				bool flag2 = nIsFinder == 0;
				if (flag2)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(46070, new object[0]), "ffdd00", "system");
				}
			}
		}
		else
		{
			bool flag3 = nIsFinder == 0;
			if (flag3)
			{
				TeamManager.GetInstance().DelImgCD(sName);
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(46070, new object[0]), "ffdd00", "system");
			}
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x000715D4 File Offset: 0x0006F7D4
	public void CallJoinTeam()
	{
		base.Invoke("CallOK", 3f);
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000715E8 File Offset: 0x0006F7E8
	public void CallOK()
	{
		TeamManager.GetInstance().onMsgClick(true, 1, TeamManager.GetInstance().szAddTeamInfo);
		TeamManager.GetInstance().szAddTeamInfo.Clear();
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00071614 File Offset: 0x0006F814
	public void ShowInviteUI(int ntype, int nteam = 0, object param = null)
	{
		this.ShowWin("InviteUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.LoadInviteUIOk), new ArrayList
		{
			ntype,
			nteam,
			param
		}, true);
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00071664 File Offset: 0x0006F864
	private void LoadInviteUIOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = InviteUICon.inst != null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szParam;
			InviteUICon.inst.ShowListByInfo((int)arrayList[0], (int)arrayList[1], arrayList[2]);
		}
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000716B4 File Offset: 0x0006F8B4
	public void ShowJiaoXueWin(int nType)
	{
		fUIManager.GetInstance().ShowWin("EnterJiaoXue", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), nType, true);
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x000716DC File Offset: 0x0006F8DC
	private void loaduiok(string sUI, GameObject obj, object szParam)
	{
		int nType = (int)szParam;
		bool flag = EnterJiaoXueCon.inst != null;
		if (flag)
		{
			EnterJiaoXueCon.inst.ShowInfo(nType);
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00071710 File Offset: 0x0006F910
	public void OpenChongZhiUI(string sysname = "chongzhi")
	{
		bool flag = Fun.GetIsCharge() == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(94004, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.BuyGoldName = sysname;
			fUIManager.GetInstance().ShowWin("ChongZhiUI", NetMsg.UITRANSFROM.PublicUI, null, null, true);
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00071794 File Offset: 0x0006F994
	public void openGameUi(NetLiving _Net, int nOpenType = 0)
	{
		bool flag = gameuiCon.inst == null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("gameui", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.loadgameuiok), new ArrayList
			{
				_Net,
				nOpenType
			}, true);
		}
		else
		{
			fGameManager.m_nJoinGame = "进入游戏";
			fUIManager.SetProgreessShow(false);
			gameuiCon.inst.Open();
			gameuiCon.inst.showHeroUi(_Net, nOpenType);
		}
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00071818 File Offset: 0x0006FA18
	private void loadgameuiok(string sUI, GameObject ob, object szParam)
	{
		bool flag = fGameManager.GetInstance().nNowScnID == 0;
		if (flag)
		{
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.Close();
			}
		}
		else
		{
			ArrayList arrayList = (ArrayList)szParam;
			NetLiving netObj = (NetLiving)arrayList[0];
			int nOpenType = (int)arrayList[1];
			fUIManager.SetProgreessShow(false);
			fGameManager.m_nJoinGame = "进入游戏";
			fUIManager.GetInstance().ProgressChange(false, "LoadingGamePlayer", 1f, 1f);
			gameuiCon.inst.showHeroUi(netObj, nOpenType);
			bool isSingle = fGameManager.IsSingle;
			if (isSingle)
			{
				PlayerLearnManager.GetInstance().PlayerLearnStart();
			}
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x000718CC File Offset: 0x0006FACC
	public void openMvpShowUi(ArrayList szMvpUserdata, string sUIName)
	{
		ArrayList arrayList = new ArrayList
		{
			szMvpUserdata,
			sUIName
		};
		bool flag = GameShouUICon.Inst == null;
		if (flag)
		{
			this.ShowWin("GameShouUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnGameShouUIEnd), arrayList, true);
		}
		else
		{
			bool flag2 = !GameShouUICon.Inst.visible;
			if (flag2)
			{
				GameShouUICon.Inst.Open();
			}
			GameShouUICon.Inst.OnLoadMvpModeData(arrayList);
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00071949 File Offset: 0x0006FB49
	private void OnGameShouUIEnd(string sUI, GameObject UIObj, object szParam)
	{
		GameShouUICon.Inst.OnLoadMvpModeData((ArrayList)szParam);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00071960 File Offset: 0x0006FB60
	public void ShowDamageMsg(int nHurt, NetLiving pPlayer)
	{
		bool flag = pPlayer != null && pPlayer.playerHaveCreate && !pPlayer.haveDestroy;
		if (flag)
		{
			string text = string.Empty;
			text += nHurt.ToString();
			Vector3 position = pPlayer.defaultRigidbody.transform.position;
			float num = ((RobotAI)pPlayer).heroData.vHeadNamePos[1];
			position.y += num;
			fUIManager.GetInstance().showDamage(position, text, 1);
		}
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x000719E8 File Offset: 0x0006FBE8
	public void showDamage(Vector3 at, string sTxt, int type)
	{
		this.damageAt = at;
		string url = "UI/pve/kinghp.ab";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.InitHpFloatRoot), sTxt);
		}
		else
		{
			this.InitHpFloatRoot(resWWW, sTxt);
		}
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00071A38 File Offset: 0x0006FC38
	public void InitHpFloatRoot(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("资源问题2");
		}
		else
		{
			string text = (string)sStyle;
			AssetBundle assetBundle = www.assetBundle;
			Camera controlCamera = CameraControl.Instance.controlCamera;
			Vector3 vector = controlCamera.WorldToScreenPoint(this.damageAt);
			Vector2 vector2 = new Vector2(vector.x - (float)(Screen.width / 2), vector.y - (float)(Screen.height / 2));
			GameObject gameObject = new GameObject("damage_txt");
			gameObject.transform.parent = base.transform;
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				GameObject gameObject2 = new GameObject("damage_txt");
				Image image = gameObject2.AddComponent<Image>();
				image.sprite = (assetBundle.LoadAsset("bj_" + text[i].ToString(), typeof(Sprite)) as Sprite);
				gameObject2.transform.parent = gameObject.transform;
				gameObject2.transform.localPosition = new Vector3((float)num, 0f, 0f);
				gameObject2.transform.localScale = Vector3.one;
				num += (int)image.rectTransform.sizeDelta.x;
			}
			float num2 = 0.4f;
			gameObject.transform.localPosition = new Vector3(vector2.x - (float)(num / 2) * num2, vector2.y, 0f);
			gameObject.transform.localScale = new Vector3(num2, num2, num2);
			gameObject.AddComponent<DamgeTextParabola>();
		}
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00071C00 File Offset: 0x0006FE00
	public void OpenUIByUrl(string[] szUrl)
	{
		bool flag = szUrl.Length == 0;
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			string text = szUrl[0];
			bool flag2 = text != null && text == "HeroInfoUI";
			if (flag2)
			{
				arrayList.Add(int.Parse(szUrl[1]));
				arrayList.Add(int.Parse(szUrl[2]));
				arrayList.Add(int.Parse(szUrl[3]));
				bool flag3 = MainUICon.inst != null;
				if (flag3)
				{
					MainUICon.inst.ChangeTitleToIdx(1, arrayList);
				}
			}
		}
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00071C98 File Offset: 0x0006FE98
	public void TecentCheckShieldTxt(fUIManager.ShieldCheckDelegate func, string content, int ntype = 0, int nstart = 0)
	{
		bool flag = base.IsInvoking("LocalCheckShieldTxt");
		if (flag)
		{
			base.CancelInvoke("LocalCheckShieldTxt");
		}
		this.shieldBackFun = func;
		this.szShield.Clear();
		this.szShield.Add(content);
		this.szShield.Add(ntype);
		this.szShield.Add(nstart);
		base.Invoke("LocalCheckShieldTxt", 1f);
		base.StartCoroutine(this.CheckShieldTxtBack(SysConfig.getWebURL(string.Concat(new string[]
		{
			"/interface/qq_uic.aspx?action=check&text=",
			Fun.EncodeBase64(content),
			"&type=1&account=",
			WebParam.client_account,
			string.Empty
		})), content));
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00071D60 File Offset: 0x0006FF60
	private IEnumerator CheckShieldTxtBack(string url, string content)
	{
		this.shieldWWW = new WWW(url);
		yield return this.shieldWWW;
		bool flag = this.szShield.Count > 0 && this.szShield[0].ToString() == content;
		if (flag)
		{
			this.LocalCheckShieldTxt();
		}
		yield break;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00071D80 File Offset: 0x0006FF80
	private void LocalCheckShieldTxt()
	{
		bool flag = base.IsInvoking("LocalCheckShieldTxt");
		if (flag)
		{
			base.CancelInvoke("LocalCheckShieldTxt");
		}
		bool flag2 = this.szShield.Count == 0;
		if (!flag2)
		{
			bool flag3 = this.shieldWWW == null;
			if (flag3)
			{
				this.shieldBackFun(Fun.HaveShieldTxt(this.szShield[0].ToString(), (int)this.szShield[2]), Fun.GetShieldTxt(this.szShield[0].ToString()));
			}
			else
			{
				bool flag4 = !this.shieldWWW.isDone || this.shieldWWW.error != null;
				if (flag4)
				{
					this.shieldBackFun(Fun.HaveShieldTxt(this.szShield[0].ToString(), (int)this.szShield[2]), Fun.GetShieldTxt(this.szShield[0].ToString()));
				}
				else
				{
					bool flag5 = string.IsNullOrEmpty(this.shieldWWW.text);
					if (flag5)
					{
						this.shieldBackFun(Fun.HaveShieldTxt(this.szShield[0].ToString(), (int)this.szShield[2]), Fun.GetShieldTxt(this.szShield[0].ToString()));
					}
					else
					{
						string[] array = this.shieldWWW.text.Split(new char[]
						{
							'|'
						});
						bool flag6 = array[0] == "0";
						if (flag6)
						{
							this.shieldBackFun(1, array[1]);
						}
						else
						{
							this.shieldBackFun(-1, array[1]);
						}
					}
				}
			}
			this.szShield.Clear();
			this.shieldBackFun = null;
			this.shieldWWW = null;
		}
	}

	// Token: 0x04000A10 RID: 2576
	private fUIManager.ShieldCheckDelegate shieldBackFun;

	// Token: 0x04000A11 RID: 2577
	public static GameObject mUIObj;

	// Token: 0x04000A12 RID: 2578
	public static fUIManager mInst;

	// Token: 0x04000A13 RID: 2579
	public static bool UILock = false;

	// Token: 0x04000A14 RID: 2580
	public static GameObject mLobbyUI;

	// Token: 0x04000A15 RID: 2581
	public static GameObject mGameUI;

	// Token: 0x04000A16 RID: 2582
	public static GameObject mGameLoadUI;

	// Token: 0x04000A17 RID: 2583
	public static GameObject mGameUISound;

	// Token: 0x04000A18 RID: 2584
	public static GameObject mPublicUI;

	// Token: 0x04000A19 RID: 2585
	public static int LiZiModel = 1;

	// Token: 0x04000A1A RID: 2586
	public static GameObject mProgress;

	// Token: 0x04000A1B RID: 2587
	public static UILoading mUILoading;

	// Token: 0x04000A1C RID: 2588
	public bool lanzuanshow;

	// Token: 0x04000A1D RID: 2589
	private Dictionary<string, GameObject> winList = new Dictionary<string, GameObject>();

	// Token: 0x04000A1E RID: 2590
	private Dictionary<string, GameObject> winLobbyList = new Dictionary<string, GameObject>();

	// Token: 0x04000A1F RID: 2591
	private Dictionary<string, GameObject> winGameList = new Dictionary<string, GameObject>();

	// Token: 0x04000A20 RID: 2592
	public Dictionary<string, GameObject> MapEffecyObj = new Dictionary<string, GameObject>();

	// Token: 0x04000A21 RID: 2593
	public static Camera uiCamera;

	// Token: 0x04000A22 RID: 2594
	public static Canvas mCanObj;

	// Token: 0x04000A23 RID: 2595
	public static CanvasScaler mCanScalObj;

	// Token: 0x04000A24 RID: 2596
	public static GraphicRaycaster mGrapRayObj;

	// Token: 0x04000A25 RID: 2597
	public static tipBlurEff tipBlur;

	// Token: 0x04000A26 RID: 2598
	public static GameObject MapEffectObj1;

	// Token: 0x04000A27 RID: 2599
	public static GameObject MapEffectObj2;

	// Token: 0x04000A28 RID: 2600
	public static GameObject MapEffectObj3;

	// Token: 0x04000A29 RID: 2601
	public string sChatName = string.Empty;

	// Token: 0x04000A2A RID: 2602
	private string CodeCon = "Con";

	// Token: 0x04000A2B RID: 2603
	public static Font mFont;

	// Token: 0x04000A2C RID: 2604
	public static Font mArialFont;

	// Token: 0x04000A2D RID: 2605
	public static Font mNewComic;

	// Token: 0x04000A2E RID: 2606
	public static Font mDINMedium;

	// Token: 0x04000A2F RID: 2607
	public static bool IsMapOK = false;

	// Token: 0x04000A30 RID: 2608
	public static bool IsLoading = false;

	// Token: 0x04000A31 RID: 2609
	public static GameObject VoiceGameObj;

	// Token: 0x04000A32 RID: 2610
	public bool booldata = true;

	// Token: 0x04000A33 RID: 2611
	public bool _LoadFlag;

	// Token: 0x04000A34 RID: 2612
	private bool bTabUIOpen;

	// Token: 0x04000A35 RID: 2613
	public static List<RawImage> m_szAllRawImgList = new List<RawImage>();

	// Token: 0x04000A36 RID: 2614
	public string BuyGoldName = string.Empty;

	// Token: 0x04000A37 RID: 2615
	private Vector3 damageAt;

	// Token: 0x04000A38 RID: 2616
	private WWW shieldWWW;

	// Token: 0x04000A39 RID: 2617
	private ArrayList szShield = new ArrayList();

	// Token: 0x0200056B RID: 1387
	// (Invoke) Token: 0x06003405 RID: 13317
	public delegate void UILoadFinishDelegate(string sUI, GameObject UIObj, object szParam);

	// Token: 0x0200056C RID: 1388
	// (Invoke) Token: 0x06003409 RID: 13321
	public delegate void ShieldCheckDelegate(int ishave, string sback);
}

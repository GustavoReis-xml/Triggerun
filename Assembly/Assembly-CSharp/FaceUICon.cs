using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000120 RID: 288
public class FaceUICon : Window
{
	// Token: 0x06000A6E RID: 2670 RVA: 0x0005E1BC File Offset: 0x0005C3BC
	protected override void Awake()
	{
		base.Awake();
		this.titleTx = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.titleTx.font = fUIManager.mNewComic;
		}
		GameObject gameObject = this.UIGameObj.Get_GameObj(34);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverMoney);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitMoney);
		this.gameGoldTx = this.UIGameObj.Get_GameObj(35).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(36);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverMoney);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitMoney);
		this.chargeTx = this.UIGameObj.Get_GameObj(37).GetComponent<Text>();
		this.myFaceImg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.myLzImg = this.UIGameObj.Get_GameObj(24).GetComponent<Image>();
		this.myLzNianImg = this.UIGameObj.Get_GameObj(25).GetComponent<Image>();
		this.myNickTx = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.myLevelTx = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.myExpBar = this.UIGameObj.Get_GameObj(3).GetComponent<Slider>();
		this.myExpTx = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTab);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTabBtn.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTab);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTabBtn.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(10);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTab);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTabBtn.Add(gameObject.GetComponent<Toggle>());
		this.gridList = this.UIGameObj.Get_GameObj(6).GetComponent<ScrollRect>();
		this.faceGrid = this.UIGameObj.Get_GameObj(7);
		this.faceGrid.SetActive(false);
		gameObject = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		this.faceUnlockTxt = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.faceUnlockObj = this.UIGameObj.Get_GameObj(27);
		this.faceBtn1 = this.UIGameObj.Get_GameObj(28).GetComponent<Button>();
		EventTriggerListener.Get(this.faceBtn1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFaceBtn);
		EventTriggerListener.Get(this.faceBtn1.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.faceBtnSp1 = this.faceBtn1.transform.Find("Image").GetComponent<Image>();
		this.faceBtnTxt1 = this.faceBtn1.transform.Find("Text").GetComponent<Text>();
		this.faceBtn2 = this.UIGameObj.Get_GameObj(29).GetComponent<Button>();
		EventTriggerListener.Get(this.faceBtn2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFaceBtn);
		EventTriggerListener.Get(this.faceBtn2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.faceBtnSp2 = this.faceBtn2.transform.Find("Image").GetComponent<Image>();
		this.faceBtnTxt2 = this.faceBtn2.transform.Find("Text").GetComponent<Text>();
		this.faceBtn3 = this.UIGameObj.Get_GameObj(22).GetComponent<Button>();
		EventTriggerListener.Get(this.faceBtn3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGiftFaceBtn);
		EventTriggerListener.Get(this.faceBtn3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.dropDownBtn = this.UIGameObj.Get_GameObj(12);
		EventTriggerListener.Get(this.dropDownBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDropDown);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.dropDownBtn.SetActive(false);
		this.dropDownList = this.UIGameObj.Get_GameObj(15);
		this.itemObj = this.UIGameObj.Get_GameObj(16);
		this.itemObj.SetActive(false);
		this.dropDownList.SetActive(false);
		this.dropDownBtn.SetActive(false);
		this.paintObj = this.UIGameObj.Get_GameObj(17);
		this.paintGrid = this.UIGameObj.Get_GameObj(18);
		this.paintGrid.SetActive(false);
		this.paintName = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		this.paintIcon = this.UIGameObj.Get_GameObj(20).GetComponent<RawImage>();
		this.paintUnlockTxt = this.UIGameObj.Get_GameObj(30).GetComponent<Text>();
		this.paintUnlockObj = this.UIGameObj.Get_GameObj(31);
		this.paintBtn1 = this.UIGameObj.Get_GameObj(32).GetComponent<Button>();
		EventTriggerListener.Get(this.paintBtn1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPaintBtn);
		EventTriggerListener.Get(this.paintBtn1.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.paintBtnSp1 = this.paintBtn1.transform.Find("Image").GetComponent<Image>();
		this.paintBtnTxt1 = this.paintBtn1.transform.Find("Text").GetComponent<Text>();
		this.paintBtn2 = this.UIGameObj.Get_GameObj(33).GetComponent<Button>();
		EventTriggerListener.Get(this.paintBtn2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPaintBtn);
		EventTriggerListener.Get(this.paintBtn2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.paintBtn3 = this.UIGameObj.Get_GameObj(23).GetComponent<Button>();
		EventTriggerListener.Get(this.paintBtn3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGiftPaintBtn);
		EventTriggerListener.Get(this.paintBtn3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.paintBtnSp2 = this.paintBtn2.transform.Find("Image").GetComponent<Image>();
		this.paintBtnTxt2 = this.paintBtn2.transform.Find("Text").GetComponent<Text>();
		this.paintObj.SetActive(false);
		FaceUICon.inst = this;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0005E91B File Offset: 0x0005CB1B
	private void OnExitMoney(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0005E92C File Offset: 0x0005CB2C
	private void OnOverMoney(GameObject go)
	{
		string sText = string.Empty;
		sText = ((!(go.name == "Charge")) ? Language.GetLang(5026, new object[]
		{
			PlayerDataManager.getInstance().playerdet.gameCurrency
		}) : Language.GetLang(5025, new object[]
		{
			PlayerDataManager.getInstance().playerdet.chargeCurrency
		}));
		TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0005E9BF File Offset: 0x0005CBBF
	private void Start()
	{
		this.SetPlayerInfo();
		this.ChangeToIdx(0);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0005E9D1 File Offset: 0x0005CBD1
	private void Update()
	{
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0005E9D4 File Offset: 0x0005CBD4
	public override void Reset()
	{
		base.Reset();
		this.SetPlayerInfo();
		this.ChangeToIdx(0);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0005E9F0 File Offset: 0x0005CBF0
	public void ChangeToIdx(int idx)
	{
		bool flag = idx >= 0 && idx < this.szTabBtn.Count;
		if (flag)
		{
			this.OnClickTab(this.szTabBtn[idx].gameObject);
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0005EA34 File Offset: 0x0005CC34
	public void SetPlayerInfo()
	{
		int nFace = PlayerDataManager.getInstance().HUserData.m_nFace;
		bool flag = fGameManager.mloadConfig.AllFaceData.ContainsKey(nFace);
		if (flag)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nFace];
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.myFaceImg);
		}
		string text = Fun.GetLzImgName();
		bool flag2 = string.IsNullOrEmpty(text);
		if (flag2)
		{
			this.myLzImg.enabled = false;
		}
		else
		{
			this.myLzImg.enabled = true;
			this.myLzImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
		}
		text = Fun.GetLzNianImgName();
		bool flag3 = string.IsNullOrEmpty(text);
		if (flag3)
		{
			this.myLzNianImg.enabled = false;
		}
		else
		{
			this.myLzNianImg.enabled = true;
			this.myLzNianImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
		}
		this.myNickTx.text = PlayerDataManager.getInstance().HUserData.m_sName;
		this.myLevelTx.text = PlayerDataManager.getInstance().HUserData.m_nLevel.ToString();
		this.myExpBar.value = (float)PlayerDataManager.getInstance().HUserData.m_nExp * 1f / (float)Fun.GetUserLvlupExp(PlayerDataManager.getInstance().HUserData.m_nLevel);
		this.myExpTx.text = string.Format("{0}<color=#bf772a>/{1}</color>", PlayerDataManager.getInstance().HUserData.m_nExp, Fun.GetUserLvlupExp(PlayerDataManager.getInstance().HUserData.m_nLevel));
		this.gameGoldTx.text = Fun.MoneyToShow(PlayerDataManager.getInstance().playerdet.gameCurrency);
		this.chargeTx.text = Fun.MoneyToShow(PlayerDataManager.getInstance().playerdet.chargeCurrency);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0005EC28 File Offset: 0x0005CE28
	private void OnClickTab(GameObject go)
	{
		for (int i = 0; i < this.szTabBtn.Count; i++)
		{
			bool flag = this.szTabBtn[i].name == go.name;
			if (flag)
			{
				this.szTabBtn[i].isOn = true;
				this.szTabBtn[i].GetComponentInChildren<Text>().color = Color.white;
			}
			else
			{
				this.szTabBtn[i].isOn = false;
				this.szTabBtn[i].GetComponentInChildren<Text>().color = new Color(0.7647059f, 0.7647059f, 0.7647059f);
			}
		}
		this.dropDownList.SetActive(false);
		string name = go.name;
		string a = name;
		if (!(a == "Toggle0"))
		{
			if (!(a == "Toggle1"))
			{
				if (a == "Toggle2")
				{
					this.titleTx.text = Language.GetLang(12005, new object[0]);
					this.paintObj.SetActive(true);
					this.gridList.gameObject.SetActive(false);
					this.faceUnlockObj.SetActive(false);
					this.faceUnlockTxt.text = string.Empty;
					this.paintUnlockObj.SetActive(true);
					this.InitPaintData();
				}
			}
			else
			{
				this.titleTx.text = Language.GetLang(12002, new object[0]);
				this.paintObj.SetActive(false);
				this.gridList.gameObject.SetActive(false);
			}
		}
		else
		{
			this.titleTx.text = Language.GetLang(12001, new object[0]);
			this.paintObj.SetActive(false);
			this.gridList.gameObject.SetActive(true);
			this.faceUnlockObj.SetActive(true);
			this.paintUnlockObj.SetActive(false);
			this.paintUnlockTxt.text = string.Empty;
			this.InitFaceData();
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0005EE54 File Offset: 0x0005D054
	public void InitFaceData()
	{
		List<PlayerFaceData> list = new List<PlayerFaceData>(fGameManager.mloadConfig.AllFaceData.Values);
		list.Sort(delegate(PlayerFaceData x, PlayerFaceData y)
		{
			bool flag8 = x.nIsNew != y.nIsNew;
			int result;
			if (flag8)
			{
				result = y.nIsNew.CompareTo(x.nIsNew);
			}
			else
			{
				result = ((x.nIsOpen != y.nIsOpen) ? y.nIsOpen.CompareTo(x.nIsOpen) : x.nSortId.CompareTo(y.nSortId));
			}
			return result;
		});
		GameObject gameObject = null;
		int i = 0;
		int num = Mathf.Max(this.szFaceGrid.Count, list.Count);
		while (i < num)
		{
			bool flag = i < list.Count;
			if (flag)
			{
				PlayerFaceData playerFaceData = list[i];
				bool flag2 = i < this.szFaceGrid.Count;
				GameObject gameObject2;
				if (flag2)
				{
					gameObject2 = this.szFaceGrid[i];
				}
				else
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.faceGrid);
					gameObject2.transform.SetParent(this.faceGrid.transform.parent);
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject2.SetActive(true);
					this.szFaceGrid.Add(gameObject2);
					EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFace);
					EventTriggerListener.Get(gameObject2).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverFace);
					EventTriggerListener.Get(gameObject2).onExit = new EventTriggerListener.VoidDelegate(this.OnExitFace);
					EventTriggerListener.Get(gameObject2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
				gameObject2.name = string.Format("faceGrid{0}", playerFaceData.key);
				RawImage component = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component);
				Image component2 = gameObject2.transform.Find("Image").GetComponent<Image>();
				bool flag3 = playerFaceData.nIsOpen == 1;
				if (flag3)
				{
					component2.enabled = false;
				}
				else
				{
					component2.enabled = true;
				}
				component2 = gameObject2.transform.Find("UseSp").GetComponent<Image>();
				bool flag4 = playerFaceData.key == PlayerDataManager.getInstance().HUserData.m_nFace;
				if (flag4)
				{
					component2.enabled = true;
					gameObject = gameObject2;
				}
				else
				{
					component2.enabled = false;
				}
				Text component3 = gameObject2.transform.Find("Text").GetComponent<Text>();
				bool flag5 = playerFaceData.nIsNew == 1;
				if (flag5)
				{
					component3.enabled = true;
				}
				else
				{
					component3.enabled = false;
				}
				i++;
			}
			else
			{
				bool flag6 = i < this.szFaceGrid.Count;
				if (flag6)
				{
					UnityEngine.Object.DestroyImmediate(this.szFaceGrid[i]);
					this.szFaceGrid.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
		bool flag7 = gameObject != null;
		if (flag7)
		{
			this.nSlcFace = 0;
			this.OnClickFace(gameObject);
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0005F17A File Offset: 0x0005D37A
	private void OnExitFace(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0005F188 File Offset: 0x0005D388
	private void OnOverFace(GameObject go)
	{
		string sText = string.Empty;
		bool flag = go.name.StartsWith("faceGrid");
		if (flag)
		{
			int key = int.Parse(go.name.Replace("faceGrid", string.Empty));
			bool flag2 = !fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
			if (flag2)
			{
				TipManager.GetInstance().HideTextTip();
				return;
			}
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
			sText = ((playerFaceData.nIsOpen != 1) ? (playerFaceData.sFaceName + "\n" + playerFaceData.sOpenDesc) : playerFaceData.sFaceName);
		}
		bool flag3 = go.name.StartsWith("paintGrid");
		if (flag3)
		{
			int key2 = int.Parse(go.name.Replace("paintGrid", string.Empty));
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[key2];
			sText = ((playerPaintData.nLockState <= 0) ? (playerPaintData.sName + "\n" + playerPaintData.sActDesc) : playerPaintData.sName);
		}
		TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0005F2C4 File Offset: 0x0005D4C4
	private void OnClickFace(GameObject go)
	{
		bool flag = go.name.StartsWith("faceGrid");
		if (flag)
		{
			int num = int.Parse(go.name.Replace("faceGrid", string.Empty));
			bool flag2 = !fGameManager.mloadConfig.AllFaceData.ContainsKey(num) || num == this.nSlcFace;
			if (flag2)
			{
				return;
			}
			this.nSlcFace = num;
			this.RefreshFaceInfo();
			for (int i = 0; i < this.szFaceGrid.Count; i++)
			{
				Image component = this.szFaceGrid[i].transform.Find("SelectSp").GetComponent<Image>();
				bool flag3 = this.szFaceGrid[i].name == go.name;
				if (flag3)
				{
					component.enabled = true;
				}
				else
				{
					component.enabled = false;
				}
			}
		}
		bool flag4 = !go.name.StartsWith("paintGrid");
		if (!flag4)
		{
			int num2 = int.Parse(go.name.Replace("paintGrid", string.Empty));
			bool flag5 = num2 == this.nSlcPaint;
			if (!flag5)
			{
				this.nSlcPaint = num2;
				this.RefreshPaintInfo();
				for (int j = 0; j < this.szPaintGrid.Count; j++)
				{
					Image component2 = this.szPaintGrid[j].transform.Find("SelectSp").GetComponent<Image>();
					bool flag6 = this.szPaintGrid[j].name == go.name;
					if (flag6)
					{
						component2.enabled = true;
					}
					else
					{
						component2.enabled = false;
					}
				}
			}
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0005F49C File Offset: 0x0005D69C
	private void RefreshFaceInfo()
	{
		PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this.nSlcFace];
		bool flag = playerFaceData.nIsOpen == 0;
		if (flag)
		{
			this.faceUnlockTxt.text = playerFaceData.sOpenDesc;
			List<string> list = playerFaceData.BtnUnlockData();
			bool flag2 = list.Count >= 3;
			if (flag2)
			{
				bool flag3 = string.IsNullOrEmpty(list[0]);
				if (flag3)
				{
					this.faceBtn1.gameObject.SetActive(false);
				}
				else
				{
					this.faceBtn1.gameObject.SetActive(true);
					this.faceBtnTxt1.text = list[0];
					bool flag4 = int.Parse(list[1]) == 5;
					if (flag4)
					{
						this.faceBtnSp1.gameObject.SetActive(true);
						this.faceBtnSp1.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
					}
					else
					{
						bool flag5 = int.Parse(list[1]) == 2;
						if (flag5)
						{
							this.faceBtnSp1.gameObject.SetActive(true);
							this.faceBtnSp1.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
						}
						else
						{
							this.faceBtnSp1.gameObject.SetActive(false);
						}
					}
				}
				this.faceBtn1.name = "FaceBtn" + list[1] + "_" + list[2];
			}
			else
			{
				this.faceBtn1.gameObject.SetActive(false);
			}
			bool flag6 = list.Count >= 6;
			if (flag6)
			{
				bool flag7 = string.IsNullOrEmpty(list[3]);
				if (flag7)
				{
					this.faceBtn2.gameObject.SetActive(false);
				}
				else
				{
					this.faceBtn2.gameObject.SetActive(true);
					this.faceBtnTxt2.text = list[3];
					this.faceBtn2.interactable = true;
					bool flag8 = int.Parse(list[4]) == 5;
					if (flag8)
					{
						this.faceBtnSp2.gameObject.SetActive(true);
						this.faceBtnSp2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
					}
					else
					{
						bool flag9 = int.Parse(list[4]) == 2;
						if (flag9)
						{
							this.faceBtnSp2.gameObject.SetActive(true);
							this.faceBtnSp2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
						}
						else
						{
							this.faceBtnSp2.gameObject.SetActive(false);
						}
					}
				}
				this.faceBtn2.name = "FaceBtn" + list[4] + "_" + list[5];
			}
			else
			{
				this.faceBtn2.gameObject.SetActive(false);
			}
			this.faceBtn3.gameObject.SetActive(false);
		}
		else
		{
			this.faceUnlockTxt.text = string.Empty;
			this.faceBtn1.gameObject.SetActive(false);
			this.faceBtn2.gameObject.SetActive(true);
			this.faceBtnSp2.gameObject.SetActive(false);
			this.faceBtn2.name = "FaceUse";
			bool flag10 = PlayerDataManager.getInstance().HUserData.m_nFace == playerFaceData.nFaceId;
			if (flag10)
			{
				this.faceBtnTxt2.text = Language.GetLang(7011, new object[0]);
				this.faceBtn2.interactable = false;
			}
			else
			{
				this.faceBtnTxt2.text = Language.GetLang(7002, new object[0]);
				this.faceBtn2.interactable = true;
			}
			bool flag11 = playerFaceData.szGiftData.Length > 1 && int.Parse(playerFaceData.szGiftData[0]) == 1;
			if (flag11)
			{
				this.faceBtn3.gameObject.SetActive(true);
			}
			else
			{
				this.faceBtn3.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0005F8BC File Offset: 0x0005DABC
	private void OnClickFaceBtn(GameObject go)
	{
		PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this.nSlcFace];
		bool flag = go.name == "FaceUse";
		if (flag)
		{
			bool flag2 = Time.time - this.clickCd < 1f || playerFaceData.nIsOpen == 0 || PlayerDataManager.getInstance().HUserData.m_nFace == playerFaceData.nFaceId;
			if (flag2)
			{
				return;
			}
			this.clickCd = Time.time;
			HeroDataManager.GetInstance().SendHeroDataMsgToServer(11003, new ArrayList
			{
				this.nSlcFace,
				0
			});
		}
		bool flag3 = !go.name.StartsWith("FaceBtn");
		if (!flag3)
		{
			this.isUseZhekou = 0;
			bool flag4 = playerFaceData.nIsOpen != 0;
			if (!flag4)
			{
				string[] array = go.name.Replace("FaceBtn", string.Empty).Split(new char[]
				{
					'_'
				});
				int num;
				bool flag5 = !int.TryParse(array[0], out num);
				if (!flag5)
				{
					int num2 = num;
					int num3 = num2;
					if (num3 != 2)
					{
						if (num3 == 5)
						{
							bool flag6 = PlayerDataManager.getInstance().playerdet.gameCurrency < int.Parse(array[1]);
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7023, new object[]
								{
									playerFaceData.sFaceName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.FaceUnlock), num, null, null);
							}
						}
					}
					else
					{
						bool flag7 = PlayerDataManager.getInstance().playerdet.chargeCurrency < int.Parse(array[1]);
						if (flag7)
						{
							fUIManager.GetInstance().OpenChongZhiUI("FaceUICon");
						}
						else
						{
							bool flag8 = playerFaceData.nIsZhekou == 1;
							if (flag8)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
								{
									playerFaceData.sFaceName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), new TipManager.TipOKDelegate(this.FaceUnlock), num, null, null, Language.GetLang(39543, new object[]
								{
									ShopManager.GetInstance().GetItemNumById(1027)
								}), false, new TipManager.TipToggleDelegate(this.OnFaceZhekouCheck), 11, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
								{
									playerFaceData.sFaceName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.FaceUnlock), num, null, null);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0005FC90 File Offset: 0x0005DE90
	private void OnFaceZhekouCheck(bool bstate)
	{
		int num = ShopManager.GetInstance().GetHaveItemNum(1027);
		bool flag = num < 1;
		if (!flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this.nSlcFace];
			bool flag2 = playerFaceData.nIsZhekou == 1;
			if (flag2)
			{
				num = int.Parse(playerFaceData.GetUnlockValue("2"));
				this.isUseZhekou = (bstate ? 1 : 0);
				if (bstate)
				{
					num = Mathf.CeilToInt((float)(num * itemData.nZhekou) / 100f);
				}
				bool flag3 = easy_tipCon.inst != null;
				if (flag3)
				{
					easy_tipCon.inst.ContentText.text = Language.GetLang(7038, new object[]
					{
						playerFaceData.sFaceName,
						num
					});
				}
			}
		}
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0005FD7C File Offset: 0x0005DF7C
	private void FaceUnlock(object szParam)
	{
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(11002, new ArrayList
		{
			this.nSlcFace,
			(int)szParam,
			this.isUseZhekou
		});
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0005FDD8 File Offset: 0x0005DFD8
	private void RefreshPaintInfo()
	{
		PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[this.nSlcPaint];
		this.paintName.text = playerPaintData.sName;
		fUIManager.GetInstance().GetIconAsset("painticon/" + playerPaintData.sIconName + ".txt", this.paintIcon);
		bool flag = playerPaintData.nLockState == 0;
		if (flag)
		{
			this.paintUnlockTxt.text = playerPaintData.sActDesc;
			List<string> list = playerPaintData.BtnUnlockData();
			bool flag2 = list.Count >= 3;
			if (flag2)
			{
				bool flag3 = string.IsNullOrEmpty(list[0]);
				if (flag3)
				{
					this.paintBtn1.gameObject.SetActive(false);
				}
				else
				{
					this.paintBtn1.gameObject.SetActive(true);
					this.paintBtnTxt1.text = list[0];
					bool flag4 = int.Parse(list[1]) == 5;
					if (flag4)
					{
						this.paintBtnSp1.gameObject.SetActive(true);
						this.paintBtnSp1.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
					}
					else
					{
						bool flag5 = int.Parse(list[1]) == 2;
						if (flag5)
						{
							this.paintBtnSp1.gameObject.SetActive(true);
							this.paintBtnSp1.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
						}
						else
						{
							this.paintBtnSp1.gameObject.SetActive(false);
						}
					}
				}
				this.paintBtn1.name = "PaintBtn" + list[1] + "_" + list[2];
			}
			else
			{
				this.paintBtn1.gameObject.SetActive(false);
			}
			bool flag6 = list.Count >= 6;
			if (flag6)
			{
				bool flag7 = string.IsNullOrEmpty(list[3]);
				if (flag7)
				{
					this.paintBtn2.gameObject.SetActive(false);
				}
				else
				{
					this.paintBtn2.gameObject.SetActive(true);
					this.paintBtnTxt2.text = list[3];
					this.paintBtn2.interactable = true;
					bool flag8 = int.Parse(list[4]) == 5;
					if (flag8)
					{
						this.paintBtnSp2.gameObject.SetActive(true);
						this.paintBtnSp2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
					}
					else
					{
						bool flag9 = int.Parse(list[4]) == 2;
						if (flag9)
						{
							this.paintBtnSp2.gameObject.SetActive(true);
							this.paintBtnSp2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
						}
						else
						{
							this.paintBtnSp2.gameObject.SetActive(false);
						}
					}
				}
				this.paintBtn2.name = "PaintBtn" + list[4] + "_" + list[5];
			}
			else
			{
				this.paintBtn2.gameObject.SetActive(false);
			}
			this.paintBtn3.gameObject.SetActive(false);
		}
		else
		{
			this.paintUnlockTxt.text = string.Empty;
			this.paintBtn1.gameObject.SetActive(false);
			this.paintBtn2.gameObject.SetActive(true);
			this.paintBtnSp2.gameObject.SetActive(false);
			this.paintBtn2.name = "PaintUse";
			bool flag10 = PlayerDataManager.getInstance().HUserData.m_nUsePaint == playerPaintData.nPaintId;
			if (flag10)
			{
				this.paintBtnTxt2.text = Language.GetLang(7011, new object[0]);
				this.paintBtn2.interactable = false;
			}
			else
			{
				this.paintBtnTxt2.text = Language.GetLang(7002, new object[0]);
				this.paintBtn2.interactable = true;
			}
			bool flag11 = playerPaintData.szGiftData.Length > 1 && int.Parse(playerPaintData.szGiftData[0]) == 1;
			if (flag11)
			{
				this.paintBtn3.gameObject.SetActive(true);
			}
			else
			{
				this.paintBtn3.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00060230 File Offset: 0x0005E430
	private void OnClickPaintBtn(GameObject go)
	{
		PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[this.nSlcPaint];
		bool flag = go.name == "PaintUse";
		if (flag)
		{
			bool flag2 = Time.time - this.clickCd < 1f || playerPaintData.nLockState == 0 || PlayerDataManager.getInstance().HUserData.m_nUsePaint == playerPaintData.nPaintId;
			if (flag2)
			{
				return;
			}
			this.clickCd = Time.time;
			HeroDataManager.GetInstance().SendHeroDataMsgToServer(11010, new ArrayList
			{
				this.nSlcPaint,
				0
			});
		}
		bool flag3 = !go.name.StartsWith("PaintBtn");
		if (!flag3)
		{
			this.isUseZhekou = 0;
			bool flag4 = playerPaintData.nLockState != 0;
			if (!flag4)
			{
				string[] array = go.name.Replace("PaintBtn", string.Empty).Split(new char[]
				{
					'_'
				});
				int num;
				bool flag5 = !int.TryParse(array[0], out num);
				if (!flag5)
				{
					int num2 = num;
					int num3 = num2;
					if (num3 != 2)
					{
						if (num3 == 5)
						{
							bool flag6 = PlayerDataManager.getInstance().playerdet.gameCurrency < int.Parse(array[1]);
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7023, new object[]
								{
									playerPaintData.sName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.ConfirmUnlock), num, null, null);
							}
						}
					}
					else
					{
						bool flag7 = PlayerDataManager.getInstance().playerdet.chargeCurrency < int.Parse(array[1]);
						if (flag7)
						{
							fUIManager.GetInstance().OpenChongZhiUI("FaceUICon");
						}
						else
						{
							bool flag8 = playerPaintData.nIsZhekou == 1;
							if (flag8)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
								{
									playerPaintData.sName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), new TipManager.TipOKDelegate(this.ConfirmUnlock), num, null, null, Language.GetLang(39543, new object[]
								{
									ShopManager.GetInstance().GetItemNumById(1027)
								}), false, new TipManager.TipToggleDelegate(this.OnPaintZhekouCheck), 11, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
								{
									playerPaintData.sName,
									array[1]
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.ConfirmUnlock), num, null, null);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00060604 File Offset: 0x0005E804
	private void OnPaintZhekouCheck(bool bstate)
	{
		int num = ShopManager.GetInstance().GetHaveItemNum(1027);
		bool flag = num < 1;
		if (!flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[this.nSlcPaint];
			bool flag2 = playerPaintData.nIsZhekou == 1;
			if (flag2)
			{
				num = int.Parse(playerPaintData.GetUnlockValue("2"));
				this.isUseZhekou = (bstate ? 1 : 0);
				if (bstate)
				{
					num = Mathf.CeilToInt((float)(num * itemData.nZhekou) / 100f);
				}
				bool flag3 = easy_tipCon.inst != null;
				if (flag3)
				{
					easy_tipCon.inst.ContentText.text = Language.GetLang(7038, new object[]
					{
						playerPaintData.sName,
						num
					});
				}
			}
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x000606F0 File Offset: 0x0005E8F0
	private void ConfirmUnlock(object szParam)
	{
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(11009, new ArrayList
		{
			this.nSlcPaint,
			(int)szParam,
			this.isUseZhekou
		});
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00060749 File Offset: 0x0005E949
	private void InitPaintData()
	{
		this.ResetPaintGrid();
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00060754 File Offset: 0x0005E954
	public void ResetPaintGrid()
	{
		GameObject gameObject = null;
		int nUsePaint = PlayerDataManager.getInstance().HUserData.m_nUsePaint;
		List<PlayerPaintData> list = new List<PlayerPaintData>(fGameManager.mloadConfig.AllPaintData.Values);
		list.Sort(delegate(PlayerPaintData x, PlayerPaintData y)
		{
			bool flag9 = x.nNewState != y.nNewState;
			int result;
			if (flag9)
			{
				result = y.nNewState.CompareTo(x.nNewState);
			}
			else
			{
				result = ((x.nLockState != y.nLockState) ? y.nLockState.CompareTo(x.nLockState) : x.nSortId.CompareTo(y.nSortId));
			}
			return result;
		});
		int i = 0;
		int num = Mathf.Max(this.szPaintGrid.Count, list.Count);
		while (i < num)
		{
			bool flag = i < list.Count;
			if (flag)
			{
				PlayerPaintData playerPaintData = list[i];
				bool flag2 = i < this.szPaintGrid.Count;
				GameObject gameObject2;
				if (flag2)
				{
					gameObject2 = this.szPaintGrid[i];
				}
				else
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.paintGrid);
					gameObject2.transform.SetParent(this.paintGrid.transform.parent);
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject2.SetActive(true);
					this.szPaintGrid.Add(gameObject2);
					EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFace);
					EventTriggerListener.Get(gameObject2).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverFace);
					EventTriggerListener.Get(gameObject2).onExit = new EventTriggerListener.VoidDelegate(this.OnExitFace);
					EventTriggerListener.Get(gameObject2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
				gameObject2.name = string.Format("paintGrid{0}", playerPaintData.nPaintId);
				RawImage component = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset("painticon/" + playerPaintData.sIconName + ".txt", component);
				Image component2 = gameObject2.transform.Find("Image").GetComponent<Image>();
				bool flag3 = playerPaintData.nLockState > 0;
				if (flag3)
				{
					component2.enabled = false;
				}
				else
				{
					component2.enabled = true;
				}
				component2 = gameObject2.transform.Find("UseSp").GetComponent<Image>();
				bool flag4 = nUsePaint == playerPaintData.nPaintId && playerPaintData.nLockState > 0;
				if (flag4)
				{
					component2.enabled = true;
				}
				else
				{
					component2.enabled = false;
				}
				Text component3 = gameObject2.transform.Find("Text").GetComponent<Text>();
				bool flag5 = playerPaintData.nNewState == 1;
				if (flag5)
				{
					component3.enabled = true;
				}
				else
				{
					component3.enabled = false;
				}
				bool flag6 = nUsePaint == playerPaintData.nPaintId;
				if (flag6)
				{
					gameObject = gameObject2;
				}
				i++;
			}
			else
			{
				bool flag7 = i < this.szPaintGrid.Count;
				if (flag7)
				{
					UnityEngine.Object.DestroyImmediate(this.szPaintGrid[i]);
					this.szPaintGrid.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
		bool flag8 = gameObject != null;
		if (flag8)
		{
			this.nSlcPaint = 0;
			this.OnClickFace(gameObject);
		}
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00060A9D File Offset: 0x0005EC9D
	private void OnClickDropDown(GameObject go)
	{
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00060AA0 File Offset: 0x0005ECA0
	private void OnClickItem(GameObject go)
	{
		int num = int.Parse(go.name.Replace("Item", string.Empty));
		bool flag = this.nSlcHeroId != num;
		if (flag)
		{
			this.nSlcHeroId = num;
			go.GetComponent<Toggle>().isOn = true;
			this.InitPaintData();
		}
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00060AF6 File Offset: 0x0005ECF6
	private void OnGiftFaceBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowInviteUI(5, 0, this.nSlcFace);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00060B11 File Offset: 0x0005ED11
	private void OnGiftPaintBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowInviteUI(6, 0, this.nSlcPaint);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00060B2C File Offset: 0x0005ED2C
	public override void Close()
	{
		base.Close();
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(11004, new ArrayList
		{
			-1,
			0
		});
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00060B65 File Offset: 0x0005ED65
	private void OnClickClose(GameObject go)
	{
		fUIManager.GetInstance().SetWinActive("MainUI", true);
		this.Close();
	}

	// Token: 0x0400087A RID: 2170
	public static FaceUICon inst;

	// Token: 0x0400087B RID: 2171
	private Text titleTx;

	// Token: 0x0400087C RID: 2172
	private Text gameGoldTx;

	// Token: 0x0400087D RID: 2173
	private Text chargeTx;

	// Token: 0x0400087E RID: 2174
	private RawImage myFaceImg;

	// Token: 0x0400087F RID: 2175
	private Image myLzImg;

	// Token: 0x04000880 RID: 2176
	private Image myLzNianImg;

	// Token: 0x04000881 RID: 2177
	private Text myNickTx;

	// Token: 0x04000882 RID: 2178
	private Text myLevelTx;

	// Token: 0x04000883 RID: 2179
	private Slider myExpBar;

	// Token: 0x04000884 RID: 2180
	private Text myExpTx;

	// Token: 0x04000885 RID: 2181
	private List<Toggle> szTabBtn = new List<Toggle>();

	// Token: 0x04000886 RID: 2182
	private ScrollRect gridList;

	// Token: 0x04000887 RID: 2183
	private GameObject faceGrid;

	// Token: 0x04000888 RID: 2184
	private List<GameObject> szFaceGrid = new List<GameObject>();

	// Token: 0x04000889 RID: 2185
	private Text faceUnlockTxt;

	// Token: 0x0400088A RID: 2186
	private GameObject faceUnlockObj;

	// Token: 0x0400088B RID: 2187
	private Button faceBtn1;

	// Token: 0x0400088C RID: 2188
	private Image faceBtnSp1;

	// Token: 0x0400088D RID: 2189
	private Text faceBtnTxt1;

	// Token: 0x0400088E RID: 2190
	private Button faceBtn2;

	// Token: 0x0400088F RID: 2191
	private Image faceBtnSp2;

	// Token: 0x04000890 RID: 2192
	private Text faceBtnTxt2;

	// Token: 0x04000891 RID: 2193
	private Button faceBtn3;

	// Token: 0x04000892 RID: 2194
	private GameObject dropDownBtn;

	// Token: 0x04000893 RID: 2195
	private GameObject dropDownList;

	// Token: 0x04000894 RID: 2196
	private GameObject itemObj;

	// Token: 0x04000895 RID: 2197
	private GameObject paintObj;

	// Token: 0x04000896 RID: 2198
	private GameObject paintGrid;

	// Token: 0x04000897 RID: 2199
	private Text paintName;

	// Token: 0x04000898 RID: 2200
	private RawImage paintIcon;

	// Token: 0x04000899 RID: 2201
	private List<GameObject> szPaintGrid = new List<GameObject>();

	// Token: 0x0400089A RID: 2202
	private Text paintUnlockTxt;

	// Token: 0x0400089B RID: 2203
	private GameObject paintUnlockObj;

	// Token: 0x0400089C RID: 2204
	private Button paintBtn1;

	// Token: 0x0400089D RID: 2205
	private Image paintBtnSp1;

	// Token: 0x0400089E RID: 2206
	private Text paintBtnTxt1;

	// Token: 0x0400089F RID: 2207
	private Button paintBtn2;

	// Token: 0x040008A0 RID: 2208
	private Image paintBtnSp2;

	// Token: 0x040008A1 RID: 2209
	private Text paintBtnTxt2;

	// Token: 0x040008A2 RID: 2210
	private Button paintBtn3;

	// Token: 0x040008A3 RID: 2211
	private int nSlcFace;

	// Token: 0x040008A4 RID: 2212
	private int nSlcHeroId;

	// Token: 0x040008A5 RID: 2213
	private int nSlcPaint;

	// Token: 0x040008A6 RID: 2214
	private int isUseZhekou;

	// Token: 0x040008A7 RID: 2215
	private float clickCd;
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C8 RID: 456
public class HeroWeaponCon : Window
{
	// Token: 0x060014A0 RID: 5280 RVA: 0x000C19C0 File Offset: 0x000BFBC0
	protected override void Awake()
	{
		base.Awake();
		GameObject gameObject = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTabClick);
		this.szTabTg.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(1);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTabClick);
		this.szTabTg.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTabClick);
		this.szTabTg.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(3);
		gameObject.SetActive(false);
		this.szTabUI.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(4);
		gameObject.SetActive(false);
		this.szTabUI.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(5);
		gameObject.SetActive(false);
		this.szTabUI.Add(gameObject);
		this.unlockRender = this.UIGameObj.Get_GameObj(6);
		this.unlockRender.SetActive(false);
		this.attrPanel = this.UIGameObj.Get_GameObj(36);
		this.attrRender = this.UIGameObj.Get_GameObj(7);
		this.attrRender.SetActive(false);
		this.partsPanel = this.UIGameObj.Get_GameObj(37);
		this.partsRender = this.UIGameObj.Get_GameObj(8);
		this.partsRender.SetActive(false);
		this.partsGrid1 = this.UIGameObj.Get_GameObj(9);
		this.partsGrid1.SetActive(false);
		this.descText = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		this.btnPanel = this.UIGameObj.Get_GameObj(11);
		this.unlockBtn = this.UIGameObj.Get_GameObj(12).GetComponent<Button>();
		this.jinbiSp = this.unlockBtn.transform.Find("Render/JinbiSp").GetComponent<Image>();
		this.unlockLb = this.unlockBtn.transform.Find("Render/Text").GetComponent<Text>();
		this.heroReturnSp = this.unlockBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.unlockBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlockBtn);
		EventTriggerListener.Get(this.unlockBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.useStateBtn = this.UIGameObj.Get_GameObj(13).GetComponent<Button>();
		this.zuanshiSp = this.useStateBtn.transform.Find("ZuanshiSp").GetComponent<Image>();
		this.useStateLb = this.useStateBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.useStateBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlockBtn);
		EventTriggerListener.Get(this.useStateBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.giftBtn = this.UIGameObj.Get_GameObj(14).GetComponent<Button>();
		EventTriggerListener.Get(this.giftBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGiftBtn);
		EventTriggerListener.Get(this.giftBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.partsGrid2 = this.UIGameObj.Get_GameObj(15);
		this.partsGrid2.SetActive(false);
		this.nowAttrIcon = this.UIGameObj.Get_GameObj(16).GetComponent<Image>();
		this.nowLvlTxt = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.nowAttrTxt = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		this.nextAttrIcon = this.UIGameObj.Get_GameObj(19).GetComponent<Image>();
		this.nextLvlTxt = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.nextAttrTxt = this.UIGameObj.Get_GameObj(20).GetComponent<Text>();
		this.moveTipTxt = this.UIGameObj.Get_GameObj(22);
		this.moveTipTxt.AddComponent<HeroPartsAnim>();
		this.partsExpBar = this.UIGameObj.Get_GameObj(23).GetComponent<Slider>();
		this.partsExpTxt = this.UIGameObj.Get_GameObj(24).GetComponent<Text>();
		this.lvlUpCostTxt = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.lvlUpBtn = this.UIGameObj.Get_GameObj(26).GetComponent<Button>();
		EventTriggerListener.Get(this.lvlUpBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLvlUp);
		EventTriggerListener.Get(this.lvlUpBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.oneKeyBtn = this.UIGameObj.Get_GameObj(27).GetComponent<Button>();
		EventTriggerListener.Get(this.oneKeyBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLvlUp);
		EventTriggerListener.Get(this.oneKeyBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lvlUpTip = this.UIGameObj.Get_GameObj(38).GetComponent<LinkButton>();
		EventTriggerListener.Get(this.lvlUpTip.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverLvlUpTip);
		EventTriggerListener.Get(this.lvlUpTip.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitLvlUpTip);
		this.partsGrid3 = this.UIGameObj.Get_GameObj(28);
		this.partsGrid3.SetActive(false);
		this.removeAttrIcon = this.UIGameObj.Get_GameObj(29).GetComponent<Image>();
		this.removeLvlTxt = this.UIGameObj.Get_GameObj(30).GetComponent<Text>();
		this.removeAttrTxt = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.removeCostTxt = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.removeBtn = this.UIGameObj.Get_GameObj(33).GetComponent<Button>();
		EventTriggerListener.Get(this.removeBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRemove);
		EventTriggerListener.Get(this.removeBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.removeTip = this.UIGameObj.Get_GameObj(39).GetComponent<LinkButton>();
		EventTriggerListener.Get(this.removeTip.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRemoveTip);
		EventTriggerListener.Get(this.removeTip.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRemoveTip);
		this.removeNoteTxt = this.UIGameObj.Get_GameObj(47).GetComponent<Text>();
		this.useTipPanel = this.UIGameObj.Get_GameObj(34);
		this.useTipPanel.SetActive(false);
		this.useTipGrid = this.UIGameObj.Get_GameObj(35);
		this.useTipGrid.SetActive(false);
		this.comPanel = this.UIGameObj.Get_GameObj(40);
		this.specPanel = this.UIGameObj.Get_GameObj(41);
		this.specNameTxt = this.UIGameObj.Get_GameObj(42).GetComponent<Text>();
		this.specAttrIcon = this.UIGameObj.Get_GameObj(43).GetComponent<Image>();
		this.specLvlTxt = this.UIGameObj.Get_GameObj(44).GetComponent<Text>();
		this.specAttrTxt = this.UIGameObj.Get_GameObj(45).GetComponent<Text>();
		this.specDescTxt = this.UIGameObj.Get_GameObj(46).GetComponent<Text>();
		HeroWeaponCon.inst = this;
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000C21A8 File Offset: 0x000C03A8
	private void ExitRemoveTip(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000C21B6 File Offset: 0x000C03B6
	private void OverRemoveTip(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(7081, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000C21E4 File Offset: 0x000C03E4
	private void ExitLvlUpTip(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x000C21F2 File Offset: 0x000C03F2
	private void OverLvlUpTip(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(7074, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000C2220 File Offset: 0x000C0420
	private void Start()
	{
		this.clickCD = Fun.GetServerDate();
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x000C2230 File Offset: 0x000C0430
	private void Update()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		if (mouseButtonDown)
		{
			this.GetTheClick();
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000C2254 File Offset: 0x000C0454
	private void GetTheClick()
	{
		bool flag = this.useTipPanel.activeSelf && !(fUIManager.uiCamera == null);
		if (flag)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = fUIManager.uiCamera.WorldToScreenPoint(this.useTipPanel.transform.position);
			RectTransform component = this.useTipPanel.GetComponent<RectTransform>();
			this.nWidth = component.sizeDelta.x;
			this.nHeight = component.sizeDelta.y;
			bool flag2 = mousePosition.x < vector.x || mousePosition.x > vector.x + this.nWidth || mousePosition.y < vector.y || mousePosition.y > vector.y + this.nHeight;
			if (flag2)
			{
				this.useTipPanel.SetActive(false);
			}
		}
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000C2336 File Offset: 0x000C0536
	public override void Reset()
	{
		base.Reset();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000C2340 File Offset: 0x000C0540
	private void OnClickGiftBtn(GameObject go)
	{
		bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.OnClickGiftBtn(go);
		}
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x000C237C File Offset: 0x000C057C
	private void OnClickUnlockBtn(GameObject go)
	{
		bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.OnClickUnlockBtn(go);
		}
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000C23B8 File Offset: 0x000C05B8
	private void OnClickRemove(GameObject go)
	{
		HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[this.nSlcParts];
		bool flag = heroPartsData.nNowLvl != 0;
		if (flag)
		{
			int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(heroPartsData.nRemoveId);
			ItemData itemData = fGameManager.mloadConfig.AllItemData[heroPartsData.nRemoveId];
			bool flag2 = this.nRemoveCost > haveItemNum;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7075, new object[]
				{
					itemData.m_sName
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				HeroDataManager.GetInstance().SendHeroDataMsgToServer(12023, new ArrayList
				{
					this.nSlcHeroId,
					this.nSlcId,
					this.nSlcGrid,
					this.nSlcParts
				});
			}
		}
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000C24D8 File Offset: 0x000C06D8
	private void OnClickLvlUp(GameObject go)
	{
		DateTime serverDate = Fun.GetServerDate();
		bool flag = (serverDate - this.clickCD).TotalSeconds < 0.5 || (this.sliderAnim != null && this.sliderAnim.IsPlaying());
		if (!flag)
		{
			this.clickCD = serverDate;
			int num = 0;
			bool flag2 = go.name == "OneKeyBtn";
			if (flag2)
			{
				num = 1;
			}
			HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[this.nSlcParts];
			bool flag3 = heroPartsData.nNowLvl != 0 && heroPartsData.nNowLvl != heroPartsData.szExpData.Length;
			if (flag3)
			{
				int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(heroPartsData.nLvlUpId);
				ItemData itemData = fGameManager.mloadConfig.AllItemData[heroPartsData.nLvlUpId];
				bool flag4 = int.Parse(heroPartsData.szCostData[heroPartsData.nNowLvl - 1]) > haveItemNum;
				if (flag4)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7075, new object[]
					{
						itemData.m_sName
					}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					HeroDataManager.GetInstance().SendHeroDataMsgToServer(12021, new ArrayList
					{
						this.nSlcParts,
						num
					});
				}
			}
		}
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000C2668 File Offset: 0x000C0868
	private void OnTabClick(GameObject go)
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		this.moveTipTxt.GetComponent<HeroPartsAnim>().StopAnim();
		bool flag = this.sliderAnim != null;
		if (flag)
		{
			this.sliderAnim.Kill(true);
		}
		string name = go.name;
		string a = name;
		if (!(a == "Toggle1"))
		{
			if (!(a == "Toggle2"))
			{
				if (a == "Toggle3")
				{
					bool flag2 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState < 2;
					if (flag2)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7069, new object[]
						{
							oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sName
						}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						this.szTabTg[this.nSlcTab].isOn = true;
					}
					else
					{
						bool flag3 = oneHeroData.GetUseParts(this.nSlcId).Count == 0;
						if (flag3)
						{
							bool flag4 = this.nSlcTab == 2;
							if (flag4)
							{
								this.nSlcTab = 0;
								this.OnTabClick(this.szTabTg[0].gameObject);
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7079, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							this.szTabTg[this.nSlcTab].isOn = true;
						}
						else
						{
							this.nSlcTab = 2;
							this.szTabTg[2].isOn = true;
							this.szTabUI[0].SetActive(false);
							this.szTabUI[1].SetActive(false);
							this.szTabUI[2].SetActive(true);
							this.ShowPartsRemove();
						}
					}
				}
			}
			else
			{
				bool flag5 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState < 2;
				if (flag5)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7069, new object[]
					{
						oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sName
					}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					this.szTabTg[this.nSlcTab].isOn = true;
				}
				else
				{
					this.nSlcTab = 1;
					this.szTabTg[1].isOn = true;
					this.szTabUI[0].SetActive(false);
					this.szTabUI[1].SetActive(true);
					this.szTabUI[2].SetActive(false);
					this.ShowPartsLevelUp(0);
				}
			}
		}
		else
		{
			this.nSlcTab = 0;
			this.szTabTg[0].isOn = true;
			this.szTabUI[0].SetActive(true);
			this.szTabUI[1].SetActive(false);
			this.szTabUI[2].SetActive(false);
			this.ShowOpenWeapon();
		}
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000C2A18 File Offset: 0x000C0C18
	public void ShowWeaponData(int heroid, int ntype)
	{
		this.nSlcHeroId = heroid;
		bool flag = ntype == 0;
		if (flag)
		{
			this.OnTabClick(this.szTabTg[0].gameObject);
		}
		else
		{
			this.OnTabClick(this.szTabTg[this.nSlcTab].gameObject);
		}
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x000C2A70 File Offset: 0x000C0C70
	private void ShowOpenWeapon()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		GameObject gameObject = null;
		int num = 0;
		int index = 0;
		fUIManager.OnClearObj(this.szUnlockRend);
		foreach (WeaponUnlock weaponUnlock in heroUnlockData.szWeaponUnlock.Values)
		{
			int num2 = 0;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.unlockRender);
			gameObject2.transform.SetParent(this.unlockRender.transform.parent);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.name = "Weapon_" + weaponUnlock.nId.ToString();
			gameObject2.SetActive(true);
			this.szUnlockRend.Add(gameObject2);
			Image component = gameObject2.transform.Find("Background").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetImageName(weaponUnlock.nQual));
			RawImage component2 = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
			component2.enabled = false;
			GetIconComponent component3 = component2.GetComponent<GetIconComponent>();
			bool flag = component3 != null;
			if (flag)
			{
				component3._Url = string.Empty;
			}
			bool flag2 = !string.IsNullOrEmpty(weaponUnlock.sIconName);
			if (flag2)
			{
				fUIManager.GetInstance().GetIconAsset("herounlock/weapon/" + weaponUnlock.sIconName + ".txt", component2);
			}
			component = gameObject2.transform.Find("CenterSp").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetCenterImage(weaponUnlock.nQual));
			bool flag3 = weaponUnlock.nLockState > 0;
			if (flag3)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
			component = gameObject2.transform.Find("LockImg").GetComponent<Image>();
			bool flag4 = weaponUnlock.nLockState > 0;
			if (flag4)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
			}
			component = gameObject2.transform.Find("StateSp").GetComponent<Image>();
			bool flag5 = weaponUnlock.nLockState == 1;
			if (flag5)
			{
				num2 = 1;
			}
			bool flag6 = weaponUnlock.nNewState == 1;
			if (flag6)
			{
				num2 = 2;
			}
			bool flag7 = num2 == 0;
			if (flag7)
			{
				component.enabled = false;
			}
			else
			{
				bool flag8 = WebParam.client_language == "zh";
				if (flag8)
				{
					component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, (num2 != 1) ? "mainui_xin" : "mainui_xian");
				}
				bool flag9 = WebParam.client_language == "baxi";
				if (flag9)
				{
					component.sprite = Window.spriteName(AtlasType.ATLAS_RESC_BAXI, (num2 != 1) ? "mainui_baxi_xin" : "mainui_baxi_xian");
				}
				component.enabled = true;
				component.SetNativeSize();
			}
			EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlockTg);
			EventTriggerListener.Get(gameObject2).onEnter = new EventTriggerListener.VoidDelegate(this.OverUnlockTg);
			EventTriggerListener.Get(gameObject2).onExit = new EventTriggerListener.VoidDelegate(this.ExitUnlockTg);
			EventTriggerListener.Get(gameObject2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			bool flag10 = weaponUnlock.nId == this.nSlcId;
			if (flag10)
			{
				gameObject = gameObject2;
			}
			bool flag11 = weaponUnlock.nId == oneHeroData.nUseWeapon;
			if (flag11)
			{
				index = num;
			}
			num++;
		}
		bool flag12 = gameObject == null && this.szUnlockRend.Count > 0;
		if (flag12)
		{
			gameObject = this.szUnlockRend[index];
		}
		bool flag13 = gameObject != null;
		if (flag13)
		{
			this.OnClickUnlockTg(gameObject);
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000C2ECC File Offset: 0x000C10CC
	private void ExitUnlockTg(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000C2EDC File Offset: 0x000C10DC
	private void OverUnlockTg(GameObject go)
	{
		int key = int.Parse(go.name.Split(new char[]
		{
			'_'
		})[1]);
		string text = string.Empty;
		string text2 = string.Empty;
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			WeaponUnlock weaponUnlock = oneHeroData.unlockData.szWeaponUnlock[key];
			text = "<size=22>" + weaponUnlock.sName + "</size>";
			string[] szAddAttr = weaponUnlock.szAddAttr;
			bool flag2 = weaponUnlock.nLockState < 2;
			if (flag2)
			{
				text2 = weaponUnlock.sActDesc;
			}
			bool flag3 = szAddAttr != null;
			if (flag3)
			{
				int num = 0;
				while (num < szAddAttr.Length && !string.IsNullOrEmpty(szAddAttr[num]))
				{
					bool flag4 = int.Parse(szAddAttr[num]) != 10;
					if (flag4)
					{
						text = text + "\n" + this.GetAttrDesc(int.Parse(szAddAttr[num]), szAddAttr[num + 2], 1);
					}
					num += 3;
				}
			}
			ArrayList arrayList = (ArrayList)weaponUnlock.szPartsGrid.Clone();
			bool flag5 = weaponUnlock.nLockState < 2;
			if (flag5)
			{
				text = text + "\n" + Language.GetLang(7095, new object[]
				{
					arrayList.Count
				});
			}
			else
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					ArrayList arrayList2 = (ArrayList)arrayList[i];
					bool flag6 = (int)arrayList2[0] == 0;
					if (flag6)
					{
						text = text + "\n" + Language.GetLang(7097, new object[]
						{
							i + 1
						});
					}
					else
					{
						bool flag7 = (int)arrayList2[1] == 0;
						if (flag7)
						{
							text = text + "\n" + Language.GetLang(7098, new object[]
							{
								i + 1
							});
						}
						else
						{
							HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[(int)arrayList2[1]];
							text = text + "\n" + Language.GetLang(7096, new object[]
							{
								this.GetAttrDesc(heroPartsData.GetAttrType(), Fun.DecimalCutZero((float)(heroPartsData.GetAttrValue() * heroPartsData.nNowLvl) / 100f, "0.00") + "%", 1),
								i + 1
							});
						}
					}
				}
			}
			bool flag8 = !string.IsNullOrEmpty(text2);
			if (flag8)
			{
				text = text + "\n" + text2;
			}
			bool flag9 = !string.IsNullOrEmpty(text);
			if (flag9)
			{
				TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
			}
		}
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000C31D4 File Offset: 0x000C13D4
	private void OnClickUnlockTg(GameObject go)
	{
		bool flag = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible;
		if (flag)
		{
			HeroInfoUICon.inst.OnClickUnlockTg(go);
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000C3210 File Offset: 0x000C1410
	private string GetImageName(int nqual)
	{
		string[] array = new string[]
		{
			string.Empty,
			string.Empty,
			"heroui_green",
			"heroui_blue",
			"heroui_violet",
			"heroui_yellow"
		};
		return array[nqual];
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000C325C File Offset: 0x000C145C
	private string GetCenterImage(int nqual)
	{
		string[] array = new string[]
		{
			string.Empty,
			string.Empty,
			"heroui_green1",
			"heroui_blue1",
			"heroui_violet1",
			"heroui_yellow1"
		};
		return array[nqual];
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000C32A8 File Offset: 0x000C14A8
	public void ShowAttrPanel(string[] szAttr)
	{
		fUIManager.OnClearObj(this.szAttrRender);
		int num = 0;
		bool flag = szAttr != null;
		if (flag)
		{
			int num2 = 0;
			while (num2 < szAttr.Length && !string.IsNullOrEmpty(szAttr[num2]))
			{
				int num3 = int.Parse(szAttr[num2]);
				bool flag2 = num3 != 10;
				if (flag2)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.attrRender);
					gameObject.transform.SetParent(this.attrRender.transform.parent);
					gameObject.name = string.Format("Render{0}", num3);
					gameObject.SetActive(true);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					this.szAttrRender.Add(gameObject);
					Image component = gameObject.transform.Find("Image").GetComponent<Image>();
					component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(num3));
					Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
					component2.text = this.GetAttrDesc(num3, szAttr[num2 + 2], 1);
					num = 1;
				}
				num2 += 3;
			}
		}
		bool flag3 = string.IsNullOrEmpty(this.descText.text);
		if (flag3)
		{
			this.btnPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -18f, 0f);
			base.GetComponent<RectTransform>().sizeDelta = new Vector2(438f, 643f);
		}
		else
		{
			this.btnPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -47f, 0f);
			base.GetComponent<RectTransform>().sizeDelta = new Vector2(438f, 668f);
		}
		bool flag4 = num == 1;
		if (flag4)
		{
			this.attrPanel.SetActive(true);
		}
		else
		{
			this.attrPanel.SetActive(false);
		}
		this.ShowPartsPanel();
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000C34D8 File Offset: 0x000C16D8
	private void ShowPartsPanel()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			fUIManager.OnClearObj(this.szPartsRender);
			fUIManager.OnClearObj(this.szPartsGrid1);
			bool activeSelf = this.useTipPanel.activeSelf;
			if (activeSelf)
			{
				this.useTipPanel.SetActive(false);
			}
			bool flag2 = !oneHeroData.unlockData.szWeaponUnlock.ContainsKey(this.nSlcId);
			if (!flag2)
			{
				WeaponUnlock weaponUnlock = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId];
				bool flag3 = weaponUnlock.nLockState < 2;
				if (flag3)
				{
					this.partsPanel.SetActive(false);
				}
				else
				{
					ArrayList arrayList = (ArrayList)weaponUnlock.szPartsGrid.Clone();
					for (int i = 0; i < arrayList.Count; i++)
					{
						ArrayList arrayList2 = (ArrayList)arrayList[i];
						int num = (int)arrayList2[0];
						int num2 = (int)arrayList2[1];
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.partsGrid1);
						gameObject.transform.SetParent(this.partsGrid1.transform.parent);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.name = string.Format("PartsGrid_{0}", i);
						gameObject.SetActive(true);
						this.szPartsGrid1.Add(gameObject);
						EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickAddParts);
						bool flag4 = num == 0;
						if (flag4)
						{
							Image component = gameObject.transform.Find("Image").GetComponent<Image>();
							component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan2");
							component = gameObject.transform.Find("Image/AttrIcon").GetComponent<Image>();
							component.enabled = true;
							component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_suo1");
							component.SetNativeSize();
							Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
							string[] szGridOpen = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].szGridOpen;
							component2.text = this.GetOpenDesc(int.Parse(szGridOpen[i * 2]), int.Parse(szGridOpen[i * 2 + 1]));
							gameObject.GetComponent<Button>().interactable = true;
						}
						else
						{
							bool flag5 = num2 == 0;
							if (flag5)
							{
								Image component = gameObject.transform.Find("Image").GetComponent<Image>();
								component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan1");
								component = gameObject.transform.Find("Image/AttrIcon").GetComponent<Image>();
								component.enabled = false;
								Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
								component2.text = Language.GetLang(7063, new object[0]);
								gameObject.GetComponent<Button>().interactable = true;
							}
							else
							{
								HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[num2];
								Image component = gameObject.transform.Find("Image").GetComponent<Image>();
								component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan1");
								component = gameObject.transform.Find("Image/AttrIcon").GetComponent<Image>();
								component.enabled = true;
								component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData.GetAttrType()) + "_big");
								component.SetNativeSize();
								Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
								component2.text = Language.GetLang(7064, new object[]
								{
									heroPartsData.nNowLvl,
									heroPartsData.sName
								});
								gameObject.GetComponent<Button>().interactable = false;
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.partsRender);
								gameObject.transform.SetParent(this.partsRender.transform.parent);
								gameObject.name = string.Format("Render{0}", heroPartsData.nId);
								gameObject.SetActive(true);
								gameObject.transform.localPosition = Vector3.zero;
								gameObject.transform.localScale = Vector3.one;
								gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
								this.szPartsRender.Add(gameObject);
								component = gameObject.transform.Find("Image").GetComponent<Image>();
								component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData.GetAttrType()));
								component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
								int attrValue = heroPartsData.GetAttrValue();
								component2.text = this.GetAttrDesc(heroPartsData.GetAttrType(), Fun.DecimalCutZero((float)(attrValue * heroPartsData.nNowLvl) / 100f, "0.00") + "%", 1);
							}
						}
					}
					this.partsPanel.SetActive(true);
				}
			}
		}
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000C3A60 File Offset: 0x000C1C60
	private void OnClickAddParts(GameObject go)
	{
		this.nSlcGrid = int.Parse(go.name.Replace("PartsGrid_", string.Empty));
		fUIManager.OnClearObj(this.szUseTipGrid);
		bool activeSelf = this.useTipPanel.activeSelf;
		if (activeSelf)
		{
			this.useTipPanel.SetActive(false);
		}
		int num = 0;
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			WeaponUnlock weaponUnlock = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId];
			string[] szAddAttr = weaponUnlock.szAddAttr;
			List<int> list = new List<int>();
			ArrayList arrayList = (ArrayList)weaponUnlock.szPartsGrid.Clone();
			ArrayList arrayList2 = (ArrayList)arrayList[this.nSlcGrid];
			bool flag2 = (int)arrayList2[0] == 0;
			if (flag2)
			{
				string[] szGridOpen = weaponUnlock.szGridOpen;
				int num2 = int.Parse(szGridOpen[this.nSlcGrid * 2]);
				int num3 = int.Parse(szGridOpen[this.nSlcGrid * 2 + 1]);
				bool flag3 = num2 == 1 && PlayerDataManager.getInstance().HUserData.m_nLevel < num3;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7065, new object[]
					{
						num3
					}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = num2 == 8 && oneHeroData.nHeroLvl < num3;
					if (flag4)
					{
						HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.nSlcHeroId];
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7066, new object[]
						{
							heroData.hname,
							num3
						}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						int num4 = num2;
						int num5 = num4;
						if (num5 != 2)
						{
							if (num5 != 5)
							{
								if (num5 != 7)
								{
									this.OnConfirmOpen(null);
								}
								else
								{
									bool flag5 = PlayerDataManager.getInstance().playerdet.matchMoney < num3;
									if (flag5)
									{
										TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7068, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
									}
									else
									{
										TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7092, new object[]
										{
											num3
										}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnConfirmOpen), null, null, null);
									}
								}
							}
							else
							{
								bool flag6 = PlayerDataManager.getInstance().playerdet.gameCurrency < num3;
								if (flag6)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7067, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
								}
								else
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7091, new object[]
									{
										num3
									}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnConfirmOpen), null, null, null);
								}
							}
						}
						else
						{
							bool flag7 = PlayerDataManager.getInstance().playerdet.chargeCurrency < num3;
							if (flag7)
							{
								fUIManager.GetInstance().OpenChongZhiUI("HeroWeaponCon");
							}
							else
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7090, new object[]
								{
									num3
								}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnConfirmOpen), null, null, null);
							}
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					arrayList2 = (ArrayList)arrayList[i];
					int num6 = (int)arrayList2[0];
					int num7 = (int)arrayList2[1];
					bool flag8 = num6 != 0 && num7 != 0;
					if (flag8)
					{
						HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[num7];
						bool flag9 = !list.Contains(heroPartsData.GetAttrType());
						if (flag9)
						{
							list.Add(heroPartsData.GetAttrType());
						}
					}
				}
				for (int j = 0; j < szAddAttr.Length; j += 3)
				{
					bool flag10 = !string.IsNullOrEmpty(szAddAttr[j]) && !list.Contains(int.Parse(szAddAttr[j]));
					if (flag10)
					{
						list.Add(int.Parse(szAddAttr[j]));
					}
				}
				foreach (HeroPartsData heroPartsData2 in fGameManager.mloadConfig.AllHeroPartsData.Values)
				{
					bool flag11 = heroPartsData2.nNowLvl != 0 && !list.Contains(heroPartsData2.GetAttrType());
					if (flag11)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.useTipGrid);
						gameObject.transform.SetParent(this.useTipGrid.transform.parent);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.name = string.Format("UseGrid_{0}", heroPartsData2.nId);
						gameObject.SetActive(true);
						Image component = gameObject.transform.Find("Image/AttrIcon").GetComponent<Image>();
						component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData2.GetAttrType()) + "_big");
						component.SetNativeSize();
						Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
						component2.text = Language.GetLang(7064, new object[]
						{
							heroPartsData2.nNowLvl,
							heroPartsData2.sName
						});
						this.szUseTipGrid.Add(gameObject);
						EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUseParts);
						num = 1;
					}
				}
				bool flag12 = num == 1;
				if (flag12)
				{
					this.useTipPanel.SetActive(true);
					RectTransform component3 = this.useTipPanel.GetComponent<RectTransform>();
					bool flag13 = this.szPartsGrid1.Count > 3 && this.nSlcGrid < 3;
					if (flag13)
					{
						component3.anchoredPosition3D = new Vector3(11.5f, 203f, 0f);
					}
					else
					{
						this.useTipPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(11.5f, 138f, 0f);
					}
				}
				else
				{
					this.useTipPanel.SetActive(false);
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7093, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
			}
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000C42C8 File Offset: 0x000C24C8
	private void OnConfirmOpen(object szParam)
	{
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12019, new ArrayList
		{
			this.nSlcHeroId,
			2,
			this.nSlcId,
			this.nSlcGrid
		});
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000C4330 File Offset: 0x000C2530
	private void OnClickUseParts(GameObject go)
	{
		int num = int.Parse(go.name.Replace("UseGrid_", string.Empty));
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12022, new ArrayList
		{
			this.nSlcHeroId,
			this.nSlcId,
			this.nSlcGrid,
			num
		});
		this.useTipPanel.SetActive(false);
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000C43C0 File Offset: 0x000C25C0
	public void ShowPartsLevelUp(int nindex)
	{
		GameObject gameObject = null;
		bool flag = this.szPartsGrid2.Count == 0;
		if (flag)
		{
			foreach (HeroPartsData heroPartsData in fGameManager.mloadConfig.AllHeroPartsData.Values)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.partsGrid2);
				gameObject2.transform.SetParent(this.partsGrid2.transform.parent);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject2.name = string.Format("LvlUpGrid_{0}", heroPartsData.nId);
				gameObject2.SetActive(true);
				this.szPartsGrid2.Add(gameObject2);
				EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLvlUpParts);
			}
		}
		for (int i = 0; i < this.szPartsGrid2.Count; i++)
		{
			GameObject gameObject3 = this.szPartsGrid2[i];
			int num = int.Parse(gameObject3.name.Replace("LvlUpGrid_", string.Empty));
			bool flag2 = num == nindex;
			if (flag2)
			{
				gameObject = gameObject3;
			}
			HeroPartsData heroPartsData2 = fGameManager.mloadConfig.AllHeroPartsData[num];
			bool flag3 = heroPartsData2.nNowLvl == 0;
			if (flag3)
			{
				Image component = gameObject3.transform.Find("Image").GetComponent<Image>();
				component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan2");
				component = gameObject3.transform.Find("Image/AttrIcon").GetComponent<Image>();
				component.enabled = true;
				component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_suo1");
				component.SetNativeSize();
				Text component2 = gameObject3.transform.Find("AttrDesc").GetComponent<Text>();
				string[] szOpenData = heroPartsData2.szOpenData;
				HeroPartsData heroPartsData3 = fGameManager.mloadConfig.AllHeroPartsData[int.Parse(szOpenData[0])];
				component2.text = Language.GetLang(7070, new object[]
				{
					heroPartsData3.sName,
					int.Parse(szOpenData[1])
				});
			}
			else
			{
				bool flag4 = heroPartsData2.nNowLvl == heroPartsData2.szExpData.Length;
				if (flag4)
				{
					Image component3 = gameObject3.transform.Find("Image").GetComponent<Image>();
					component3.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan1");
					component3 = gameObject3.transform.Find("Image/AttrIcon").GetComponent<Image>();
					component3.enabled = true;
					component3.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData2.GetAttrType()) + "_big");
					Text component4 = gameObject3.transform.Find("AttrDesc").GetComponent<Text>();
					component4.text = Language.GetLang(7071, new object[]
					{
						heroPartsData2.sName
					});
				}
				else
				{
					Image component5 = gameObject3.transform.Find("Image").GetComponent<Image>();
					component5.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_quan1");
					component5 = gameObject3.transform.Find("Image/AttrIcon").GetComponent<Image>();
					component5.enabled = true;
					component5.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData2.GetAttrType()) + "_big");
					component5.SetNativeSize();
					Text component6 = gameObject3.transform.Find("AttrDesc").GetComponent<Text>();
					component6.text = Language.GetLang(7064, new object[]
					{
						heroPartsData2.nNowLvl,
						heroPartsData2.sName
					});
				}
			}
		}
		bool flag5 = gameObject == null;
		if (flag5)
		{
			gameObject = this.szPartsGrid2[0];
		}
		bool flag6 = nindex == 0 || nindex != this.nSlcParts;
		if (flag6)
		{
			this.OnClickLvlUpParts(gameObject);
		}
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000C4838 File Offset: 0x000C2A38
	private void OnClickLvlUpParts(GameObject go)
	{
		int num = int.Parse(go.name.Replace("LvlUpGrid_", string.Empty));
		go.GetComponent<Toggle>().isOn = true;
		this.nSlcParts = num;
		this.ResetLvlUpTab(0);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000C4880 File Offset: 0x000C2A80
	public void ResetLvlUpTab(int ntype = 0)
	{
		bool flag = this.nSlcTab != 1 || !fGameManager.mloadConfig.AllHeroPartsData.ContainsKey(this.nSlcParts);
		if (!flag)
		{
			HeroPartsData parts = fGameManager.mloadConfig.AllHeroPartsData[this.nSlcParts];
			bool flag2 = !fGameManager.mloadConfig.AllItemData.ContainsKey(parts.nLvlUpId);
			if (!flag2)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[parts.nLvlUpId];
				bool flag3 = parts.nNowLvl > 0 && parts.nNowLvl < parts.szExpData.Length;
				if (flag3)
				{
					this.comPanel.SetActive(true);
					this.specPanel.SetActive(false);
					float fval = (float)parts.nNowExp / float.Parse(parts.szExpData[parts.nNowLvl - 1]);
					if (ntype != 0)
					{
						if (ntype != 2)
						{
							this.sliderAnim = this.partsExpBar.DOValue(fval, fval - this.partsExpBar.value, false);
							this.sliderAnim.OnComplete(delegate
							{
								this.partsExpTxt.text = Fun.DecimalCutZero(fval * 100f, "0.00") + "%";
							});
							this.nowAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
							this.nowAttrIcon.SetNativeSize();
							this.nowLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl);
							int nnum = parts.GetAttrValue();
							this.nowAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * parts.nNowLvl) / 100f, "0.00") + "%", 1);
							this.nextAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
							this.nextAttrIcon.SetNativeSize();
							this.nextLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl + 1);
							this.nextAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * (parts.nNowLvl + 1)) / 100f, "0.00") + "%", 1);
						}
						else
						{
							this.sliderAnim = this.partsExpBar.DOValue(1f, 1f - this.partsExpBar.value, false);
							int nnum;
							this.sliderAnim.OnComplete(delegate
							{
								this.sliderAnim = this.partsExpBar.DOValue(fval, fval, false);
								this.sliderAnim.OnComplete(delegate
								{
									this.partsExpTxt.text = Fun.DecimalCutZero(fval * 100f, "0.00") + "%";
								});
								this.nowAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
								this.nowAttrIcon.SetNativeSize();
								this.nowLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl);
								nnum = parts.GetAttrValue();
								this.nowAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * parts.nNowLvl) / 100f, "0.00") + "%", 1);
								this.nextAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
								this.nextAttrIcon.SetNativeSize();
								this.nextLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl + 1);
								this.nextAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * (parts.nNowLvl + 1)) / 100f, "0.00") + "%", 1);
							});
						}
					}
					else
					{
						this.partsExpBar.value = fval;
						this.partsExpTxt.text = Fun.DecimalCutZero(fval * 100f, "0.00") + "%";
						this.nowAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
						this.nowAttrIcon.SetNativeSize();
						this.nowLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl);
						int nnum = parts.GetAttrValue();
						this.nowAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * parts.nNowLvl) / 100f, "0.00") + "%", 1);
						this.nextAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
						this.nextAttrIcon.SetNativeSize();
						this.nextLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", parts.nNowLvl + 1);
						this.nextAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * (parts.nNowLvl + 1)) / 100f, "0.00") + "%", 1);
					}
					this.lvlUpCostTxt.text = Language.GetLang(7072, new object[]
					{
						itemData.m_sName,
						parts.szCostData[parts.nNowLvl - 1],
						ShopManager.GetInstance().GetHaveItemNum(parts.nLvlUpId)
					});
					this.lvlUpTip.targetGraphic.GetComponent<Text>().text = Language.GetLang(7073, new object[]
					{
						itemData.m_sName
					});
				}
				else
				{
					this.comPanel.SetActive(false);
					this.specPanel.SetActive(true);
					this.specNameTxt.text = parts.sName;
					this.specAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(parts.GetAttrType()) + "_big");
					this.specAttrIcon.SetNativeSize();
					this.specLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", Mathf.Max(1, parts.nNowLvl));
					int nnum = parts.GetAttrValue();
					this.specAttrTxt.text = this.GetAttrDesc(parts.GetAttrType(), Fun.DecimalCutZero((float)(nnum * Mathf.Max(1, parts.nNowLvl)) / 100f, "0.00") + "%", 1);
					bool flag4 = parts.nNowLvl == 0;
					if (flag4)
					{
						string[] szOpenData = parts.szOpenData;
						parts = fGameManager.mloadConfig.AllHeroPartsData[int.Parse(szOpenData[0])];
						this.specDescTxt.text = Language.GetLang(7078, new object[]
						{
							parts.sName,
							int.Parse(szOpenData[1])
						});
					}
					else
					{
						this.specDescTxt.text = Language.GetLang(7077, new object[0]);
					}
				}
			}
		}
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000C5068 File Offset: 0x000C3268
	private void ShowPartsRemove()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			fUIManager.OnClearObj(this.szPartsGrid3);
			List<int> useParts = oneHeroData.GetUseParts(this.nSlcId);
			bool flag2 = useParts.Count == 0;
			if (flag2)
			{
				this.OnTabClick(this.szTabTg[0].gameObject);
			}
			else
			{
				for (int i = 0; i < useParts.Count; i += 2)
				{
					bool flag3 = fGameManager.mloadConfig.AllHeroPartsData.ContainsKey(useParts[i + 1]);
					if (flag3)
					{
						HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[useParts[i + 1]];
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.partsGrid3);
						gameObject.transform.SetParent(this.partsGrid3.transform.parent);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.name = string.Format("RemoveGrid_{0}_{1}", useParts[i], useParts[i + 1]);
						gameObject.SetActive(true);
						this.szPartsGrid3.Add(gameObject);
						Image component = gameObject.transform.Find("Image/AttrIcon").GetComponent<Image>();
						component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData.GetAttrType()) + "_big");
						component.SetNativeSize();
						Text component2 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
						component2.text = Language.GetLang(7064, new object[]
						{
							heroPartsData.nNowLvl,
							heroPartsData.sName
						});
						EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRemoveParts);
					}
				}
				this.OnClickRemoveParts(this.szPartsGrid3[0]);
			}
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000C52A4 File Offset: 0x000C34A4
	private void OnClickRemoveParts(GameObject go)
	{
		go.GetComponent<Toggle>().isOn = true;
		string[] array = go.name.Split(new char[]
		{
			'_'
		});
		this.nSlcGrid = int.Parse(array[1]);
		this.nSlcParts = int.Parse(array[2]);
		this.ResetRemoveTab();
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000C52FC File Offset: 0x000C34FC
	public void ResetRemoveTab()
	{
		bool flag = this.nSlcTab == 2 && fGameManager.mloadConfig.AllHeroPartsData.ContainsKey(this.nSlcParts);
		if (flag)
		{
			HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[this.nSlcParts];
			bool flag2 = fGameManager.mloadConfig.AllItemData.ContainsKey(heroPartsData.nRemoveId);
			if (flag2)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[heroPartsData.nRemoveId];
				this.removeAttrIcon.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(heroPartsData.GetAttrType()) + "_big");
				this.removeAttrIcon.SetNativeSize();
				this.removeLvlTxt.text = string.Format("<color=#ee9905>Lv.{0}</color>", heroPartsData.nNowLvl);
				int attrValue = heroPartsData.GetAttrValue();
				this.removeAttrTxt.text = this.GetAttrDesc(heroPartsData.GetAttrType(), Fun.DecimalCutZero((float)(attrValue * heroPartsData.nNowLvl) / 100f, "0.00") + "%", 0);
				this.removeCostTxt.text = Language.GetLang(7072, new object[]
				{
					itemData.m_sName,
					this.nRemoveCost,
					ShopManager.GetInstance().GetHaveItemNum(heroPartsData.nRemoveId)
				});
				this.removeTip.targetGraphic.GetComponent<Text>().text = Language.GetLang(7073, new object[]
				{
					itemData.m_sName
				});
				this.removeNoteTxt.text = string.Empty;
			}
		}
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000C54A8 File Offset: 0x000C36A8
	private string GetOpenDesc(int ntype, int nvalue)
	{
		string result = string.Empty;
		if (ntype != 2)
		{
			if (ntype != 7)
			{
				if (ntype == 8)
				{
					result = Language.GetLang(7060, new object[]
					{
						nvalue
					});
				}
			}
			else
			{
				result = Language.GetLang(7061, new object[]
				{
					nvalue
				});
			}
		}
		else
		{
			result = Language.GetLang(7062, new object[]
			{
				nvalue
			});
		}
		return result;
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000C552B File Offset: 0x000C372B
	private void ShowPartsGrid()
	{
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000C5530 File Offset: 0x000C3730
	private string GetAttrImg(int nidx)
	{
		string result = string.Empty;
		if (nidx != 2)
		{
			switch (nidx)
			{
			case 7:
				result = "heroui_fanshang";
				break;
			case 8:
			case 10:
				break;
			case 9:
				result = "heroui_sudu";
				break;
			case 11:
				result = "heroui_huifu";
				break;
			default:
				switch (nidx)
				{
				case 16:
					result = "heroui_chongneng";
					break;
				case 17:
					result = "heroui_xixue";
					break;
				case 21:
					result = "heroui_jianshang";
					break;
				case 22:
					result = "heroui_gongji";
					break;
				}
				break;
			}
		}
		else
		{
			result = "heroui_shengming";
		}
		return result;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000C55D8 File Offset: 0x000C37D8
	private string GetAttrDesc(int nidx, string snum, int ntype = 1)
	{
		string result = string.Empty;
		if (nidx != 2)
		{
			switch (nidx)
			{
			case 7:
				result = ((ntype != 1) ? Language.GetLang(7086, new object[]
				{
					snum
				}) : Language.GetLang(7055, new object[]
				{
					snum
				}));
				break;
			case 8:
			case 10:
				break;
			case 9:
				result = ((ntype != 1) ? Language.GetLang(7083, new object[]
				{
					snum
				}) : Language.GetLang(7048, new object[]
				{
					snum
				}));
				break;
			case 11:
				result = ((ntype != 1) ? Language.GetLang(7089, new object[]
				{
					snum
				}) : Language.GetLang(7059, new object[]
				{
					snum
				}));
				break;
			default:
				switch (nidx)
				{
				case 16:
					result = ((ntype != 1) ? Language.GetLang(7087, new object[]
					{
						snum
					}) : Language.GetLang(7056, new object[]
					{
						snum
					}));
					break;
				case 17:
					result = ((ntype != 1) ? Language.GetLang(7088, new object[]
					{
						snum
					}) : Language.GetLang(7057, new object[]
					{
						snum
					}));
					break;
				case 21:
					result = ((ntype != 1) ? Language.GetLang(7084, new object[]
					{
						snum
					}) : Language.GetLang(7049, new object[]
					{
						snum
					}));
					break;
				case 22:
					result = ((ntype != 1) ? Language.GetLang(7085, new object[]
					{
						snum
					}) : Language.GetLang(7050, new object[]
					{
						snum
					}));
					break;
				}
				break;
			}
		}
		else
		{
			result = ((ntype != 1) ? Language.GetLang(7082, new object[]
			{
				snum
			}) : Language.GetLang(7047, new object[]
			{
				snum
			}));
		}
		return result;
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x000C57DC File Offset: 0x000C39DC
	public void PlayLvlUpEff(int nparts, int addvalue, int addlvl)
	{
		bool flag = this.nSlcTab == 1 && fGameManager.mloadConfig.AllHeroPartsData.ContainsKey(nparts);
		if (flag)
		{
			HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[nparts];
			HeroPartsAnim component = this.moveTipTxt.GetComponent<HeroPartsAnim>();
			bool flag2 = addvalue > 0;
			if (flag2)
			{
				component.AddShowText(Language.GetLang(7076, new object[]
				{
					addvalue
				}));
			}
			bool flag3 = addlvl > 0;
			if (flag3)
			{
				component.AddShowText(this.GetAttrDesc(heroPartsData.GetAttrType(), Fun.DecimalCutZero((float)(heroPartsData.GetAttrValue() * addlvl) / 100f, "0.00") + "%", 1));
				this.ShowPartsLevelUp(nparts);
			}
		}
	}

	// Token: 0x04000FE4 RID: 4068
	public static HeroWeaponCon inst;

	// Token: 0x04000FE5 RID: 4069
	private List<Toggle> szTabTg = new List<Toggle>();

	// Token: 0x04000FE6 RID: 4070
	private List<GameObject> szTabUI = new List<GameObject>();

	// Token: 0x04000FE7 RID: 4071
	private GameObject unlockRender;

	// Token: 0x04000FE8 RID: 4072
	private List<GameObject> szUnlockRend = new List<GameObject>();

	// Token: 0x04000FE9 RID: 4073
	public GameObject attrPanel;

	// Token: 0x04000FEA RID: 4074
	private GameObject attrRender;

	// Token: 0x04000FEB RID: 4075
	private List<GameObject> szAttrRender = new List<GameObject>();

	// Token: 0x04000FEC RID: 4076
	private GameObject partsPanel;

	// Token: 0x04000FED RID: 4077
	private GameObject partsRender;

	// Token: 0x04000FEE RID: 4078
	private List<GameObject> szPartsRender = new List<GameObject>();

	// Token: 0x04000FEF RID: 4079
	private GameObject partsGrid1;

	// Token: 0x04000FF0 RID: 4080
	private List<GameObject> szPartsGrid1 = new List<GameObject>();

	// Token: 0x04000FF1 RID: 4081
	public Text descText;

	// Token: 0x04000FF2 RID: 4082
	private GameObject btnPanel;

	// Token: 0x04000FF3 RID: 4083
	public Button unlockBtn;

	// Token: 0x04000FF4 RID: 4084
	public Image jinbiSp;

	// Token: 0x04000FF5 RID: 4085
	public Text unlockLb;

	// Token: 0x04000FF6 RID: 4086
	public Image heroReturnSp;

	// Token: 0x04000FF7 RID: 4087
	public Button useStateBtn;

	// Token: 0x04000FF8 RID: 4088
	public Image zuanshiSp;

	// Token: 0x04000FF9 RID: 4089
	public Text useStateLb;

	// Token: 0x04000FFA RID: 4090
	public Button giftBtn;

	// Token: 0x04000FFB RID: 4091
	private GameObject partsGrid2;

	// Token: 0x04000FFC RID: 4092
	private List<GameObject> szPartsGrid2 = new List<GameObject>();

	// Token: 0x04000FFD RID: 4093
	private Image nowAttrIcon;

	// Token: 0x04000FFE RID: 4094
	private Text nowLvlTxt;

	// Token: 0x04000FFF RID: 4095
	private Text nowAttrTxt;

	// Token: 0x04001000 RID: 4096
	private Image nextAttrIcon;

	// Token: 0x04001001 RID: 4097
	private Text nextLvlTxt;

	// Token: 0x04001002 RID: 4098
	private Text nextAttrTxt;

	// Token: 0x04001003 RID: 4099
	private GameObject moveTipTxt;

	// Token: 0x04001004 RID: 4100
	private Slider partsExpBar;

	// Token: 0x04001005 RID: 4101
	private Text partsExpTxt;

	// Token: 0x04001006 RID: 4102
	private Text lvlUpCostTxt;

	// Token: 0x04001007 RID: 4103
	private Button lvlUpBtn;

	// Token: 0x04001008 RID: 4104
	private Button oneKeyBtn;

	// Token: 0x04001009 RID: 4105
	private LinkButton lvlUpTip;

	// Token: 0x0400100A RID: 4106
	private GameObject comPanel;

	// Token: 0x0400100B RID: 4107
	private GameObject specPanel;

	// Token: 0x0400100C RID: 4108
	private Text specNameTxt;

	// Token: 0x0400100D RID: 4109
	private Image specAttrIcon;

	// Token: 0x0400100E RID: 4110
	private Text specLvlTxt;

	// Token: 0x0400100F RID: 4111
	private Text specAttrTxt;

	// Token: 0x04001010 RID: 4112
	private Text specDescTxt;

	// Token: 0x04001011 RID: 4113
	private GameObject partsGrid3;

	// Token: 0x04001012 RID: 4114
	private List<GameObject> szPartsGrid3 = new List<GameObject>();

	// Token: 0x04001013 RID: 4115
	private Image removeAttrIcon;

	// Token: 0x04001014 RID: 4116
	private Text removeLvlTxt;

	// Token: 0x04001015 RID: 4117
	private Text removeAttrTxt;

	// Token: 0x04001016 RID: 4118
	private Text removeCostTxt;

	// Token: 0x04001017 RID: 4119
	private Button removeBtn;

	// Token: 0x04001018 RID: 4120
	private LinkButton removeTip;

	// Token: 0x04001019 RID: 4121
	private Text removeNoteTxt;

	// Token: 0x0400101A RID: 4122
	private GameObject useTipPanel;

	// Token: 0x0400101B RID: 4123
	private GameObject useTipGrid;

	// Token: 0x0400101C RID: 4124
	private List<GameObject> szUseTipGrid = new List<GameObject>();

	// Token: 0x0400101D RID: 4125
	private GameObject modelScn;

	// Token: 0x0400101E RID: 4126
	private GameObject modelWeapon;

	// Token: 0x0400101F RID: 4127
	private Tweener sliderAnim;

	// Token: 0x04001020 RID: 4128
	private int nSlcHeroId;

	// Token: 0x04001021 RID: 4129
	private int nSlcGrid;

	// Token: 0x04001022 RID: 4130
	public int nSlcId;

	// Token: 0x04001023 RID: 4131
	private int nSlcTab;

	// Token: 0x04001024 RID: 4132
	public int nSlcParts;

	// Token: 0x04001025 RID: 4133
	private int nRemoveCost = 1;

	// Token: 0x04001026 RID: 4134
	private float nWidth;

	// Token: 0x04001027 RID: 4135
	private float nHeight;

	// Token: 0x04001028 RID: 4136
	private DateTime clickCD;
}

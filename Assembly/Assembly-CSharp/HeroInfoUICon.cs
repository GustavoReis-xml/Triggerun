using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class HeroInfoUICon : Window
{
	// Token: 0x06001231 RID: 4657 RVA: 0x000AA654 File Offset: 0x000A8854
	protected override void Awake()
	{
		base.Awake();
		this.heroGrid = this.UIGameObj.Get_GameObj(1);
		this.heroGrid.SetActive(false);
		this.heroName = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.heroName.font = fUIManager.mNewComic;
		}
		GameObject gameObject = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeHero);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		gameObject = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeHero);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "jiemian_page");
		Toggle component = this.UIGameObj.Get_GameObj(5).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component = this.UIGameObj.Get_GameObj(6).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component = this.UIGameObj.Get_GameObj(7).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component = this.UIGameObj.Get_GameObj(8).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component = this.UIGameObj.Get_GameObj(9).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component.gameObject.SetActive(false);
		component = this.UIGameObj.Get_GameObj(10).GetComponent<Toggle>();
		EventTriggerListener.Get(component.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlock);
		EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUnlock);
		EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUnlock);
		EventTriggerListener.Get(component.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szUnlockTg.Add(component);
		component.gameObject.SetActive(Fun.IsFunctionOpen("HeroGemTabBtn", null));
		this.infoPanelObj = this.UIGameObj.Get_GameObj(11);
		this.unlockRender = this.UIGameObj.Get_GameObj(13);
		this.unlockRender.SetActive(false);
		this.unlockDataObj = this.UIGameObj.Get_GameObj(14);
		this.heroLevel = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(16);
		this.heroExpBar = gameObject.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverExpBar);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitExpBar);
		this.heroFightTime = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		gameObject = this.heroFightTime.transform.parent.gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverFightTime);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitFightTime);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", string.Empty);
		this.heroWinNum = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		gameObject = this.heroWinNum.transform.parent.gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWinNum);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWinNum);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", string.Empty);
		this.awardRender = this.UIGameObj.Get_GameObj(20);
		this.awardRender.SetActive(false);
		this.heroUnlockUI = this.UIGameObj.Get_GameObj(30);
		this.heroUnlockUI.SetActive(false);
		this.unlockName = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.nowChipNum = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.nowChipSp = this.UIGameObj.Get_GameObj(33).GetComponent<Image>();
		this.titlePanelObj = this.UIGameObj.Get_GameObj(39);
		this.chipInfoObj = this.UIGameObj.Get_GameObj(22);
		this.descText = this.UIGameObj.Get_GameObj(23).GetComponent<Text>();
		this.overTime = this.UIGameObj.Get_GameObj(24).GetComponent<Text>();
		this.needChipNum = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.needChipSp = this.UIGameObj.Get_GameObj(35).GetComponent<Image>();
		this.unlockBtn = this.UIGameObj.Get_GameObj(26).GetComponent<Button>();
		this.jinbiSp = this.UIGameObj.Get_GameObj(37);
		this.unlockLb = this.unlockBtn.transform.Find("Render/Text").GetComponent<Text>();
		this.heroReturnSp = this.unlockBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.unlockBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlockBtn);
		EventTriggerListener.Get(this.unlockBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.useStateBtn = this.UIGameObj.Get_GameObj(28).GetComponent<Button>();
		this.zuanshiSp = this.UIGameObj.Get_GameObj(38);
		this.useStateLb = this.useStateBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.useStateBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUnlockBtn);
		EventTriggerListener.Get(this.useStateBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.giftBtn = this.UIGameObj.Get_GameObj(40).GetComponent<Button>();
		EventTriggerListener.Get(this.giftBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGiftBtn);
		EventTriggerListener.Get(this.giftBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(27);
		EventTriggerListener.Get(gameObject).onDrag = new EventTriggerListener.DataDelegate(this.OnDragModel);
		gameObject = this.UIGameObj.Get_GameObj(29);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHeroSkill);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lRotBtn = this.UIGameObj.Get_GameObj(31);
		EventTriggerListener.Get(this.lRotBtn).onDown = new EventTriggerListener.VoidDelegate(this.OnDownRotModel);
		EventTriggerListener.Get(this.lRotBtn).onUp = new EventTriggerListener.VoidDelegate(this.OnUpRotModel);
		EventTriggerListener.Get(this.lRotBtn).SetSoundStr("sound_main_mouse", string.Empty);
		this.rRotBtn = this.UIGameObj.Get_GameObj(32);
		EventTriggerListener.Get(this.rRotBtn).onDown = new EventTriggerListener.VoidDelegate(this.OnDownRotModel);
		EventTriggerListener.Get(this.rRotBtn).onUp = new EventTriggerListener.VoidDelegate(this.OnUpRotModel);
		EventTriggerListener.Get(this.rRotBtn).SetSoundStr("sound_main_mouse", string.Empty);
		gameObject = this.UIGameObj.Get_GameObj(36);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverChipPanel);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitChipPanel);
		this.attrPanel = this.UIGameObj.Get_GameObj(41);
		this.attrName = this.UIGameObj.Get_GameObj(42).GetComponent<Text>();
		this.attrRender = this.UIGameObj.Get_GameObj(43);
		this.attrRender.SetActive(false);
		this.attrPanel.SetActive(false);
		this.modelTable = this.UIGameObj.Get_GameObj(44);
		this.modelTable.SetActive(false);
		this.heroDiscountSp = this.UIGameObj.Get_GameObj(45).GetComponent<Image>();
		this.heroDiscountTxt = this.UIGameObj.Get_GameObj(46).GetComponent<Text>();
		this.heroDiscountTxt.text = string.Empty;
		this.heroDiscountSp.gameObject.SetActive(false);
		this.weaponPanel = this.UIGameObj.Get_GameObj(47);
		this.weaponCon = this.weaponPanel.AddComponent<HeroWeaponCon>();
		this.weaponPanel.SetActive(false);
		HeroInfoUICon.inst = this;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000AB158 File Offset: 0x000A9358
	private void Start()
	{
		this.InitHeroData();
		this.SetGameQuality();
		this.showBgId = GameRoot.inst.Setting.Social_Class.Loading_Background_Image;
		this.SetBackgroundUI();
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12015, new ArrayList
		{
			1,
			0,
			0,
			0
		});
		bool flag = this.openUIParam.Count > 0 && this.szHeroGrid.ContainsKey((int)this.openUIParam[0]);
		if (flag)
		{
			this.OnClickHero(this.szHeroGrid[(int)this.openUIParam[0]].gameObject);
		}
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x000AB23C File Offset: 0x000A943C
	private void Update()
	{
		bool flag = this.modelAni != null && this.modelAni.isInitialized && this.modelAni.GetInteger("HuDongState") != this.emotionState;
		if (flag)
		{
			this.modelAni.SetInteger("HuDongState", this.emotionState);
		}
		bool flag2 = this.rotAngel != 0f && this.modelObj != null && this.modelObj.activeSelf;
		if (flag2)
		{
			this.modelObj.transform.localEulerAngles = new Vector3(0f, this.modelObj.transform.localEulerAngles.y + this.rotAngel, 0f);
		}
		bool flag3 = this.rotAngel != 0f && this.modelWeapon != null && this.modelWeapon.activeSelf;
		if (flag3)
		{
			this.modelWeapon.transform.localEulerAngles = new Vector3(0f, this.modelWeapon.transform.localEulerAngles.y + this.rotAngel, 0f);
		}
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x000AB378 File Offset: 0x000A9578
	public override void Reset()
	{
		base.Reset();
		this.InitHeroData();
		bool flag = this.nDefHeroId != 0;
		if (flag)
		{
			this.nSlcType = 0;
			this.OnClickHero(this.szHeroGrid[this.nDefHeroId].gameObject);
		}
		this.SetGameQuality();
		this.showBgId = GameRoot.inst.Setting.Social_Class.Loading_Background_Image;
		this.SetBackgroundUI();
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12015, new ArrayList
		{
			1,
			0,
			0,
			0
		});
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x000AB438 File Offset: 0x000A9638
	private void SetGameQuality()
	{
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x000AB43C File Offset: 0x000A963C
	private void SetBackgroundUI()
	{
		bool flag = this.showBgId != this.oldBgId;
		if (flag)
		{
			this.oldBgId = this.showBgId;
			bool flag2 = this.showBgId == 0;
			if (flag2)
			{
				List<int> list = new List<int>(fGameManager.mloadConfig.AllHeroInBg.Keys);
				list.Remove(0);
				int index = UnityEngine.Random.Range(0, list.Count);
				this.showBgId = list[index];
			}
		}
		else
		{
			this.showBgId = this.nowBgId;
		}
		HeroInfoBg heroInfoBg = fGameManager.mloadConfig.AllHeroInBg[this.showBgId];
		bool flag3 = !string.IsNullOrEmpty(heroInfoBg.sUIName);
		if (flag3)
		{
			this.nowBgId = this.showBgId;
			string url = "UI/" + heroInfoBg.sUIName.ToLower() + ".ab";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag4 = resWWW == null;
			if (flag4)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadUIOK), heroInfoBg);
			}
			else
			{
				this.LoadUIOK(resWWW, heroInfoBg);
			}
		}
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000AB554 File Offset: 0x000A9754
	public void InitHeroData()
	{
		int num = 0;
		List<HeroUnlockData> list = new List<HeroUnlockData>(fGameManager.mloadConfig.AllHeroUnlock.Values);
		foreach (HeroUnlockData heroUnlockData in list)
		{
			bool flag = heroUnlockData.nHeroid != 0;
			if (flag)
			{
				num++;
				bool flag2 = this.szHeroGrid.ContainsKey(heroUnlockData.key);
				GameObject gameObject;
				if (flag2)
				{
					gameObject = this.szHeroGrid[heroUnlockData.key].gameObject;
				}
				else
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.heroGrid);
					gameObject.transform.SetParent(this.heroGrid.transform.parent);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.name = string.Format("HeroGrid{0}", heroUnlockData.key);
					gameObject.SetActive(true);
					this.szHeroGrid.Add(heroUnlockData.nHeroid, gameObject.GetComponent<Toggle>());
				}
				gameObject.transform.SetSiblingIndex(num);
				this.SetOneHeroGrid(gameObject, heroUnlockData.key);
			}
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x000AB6DC File Offset: 0x000A98DC
	public void OnChangeSkinOk(int nHeroId)
	{
		bool flag = this.szHeroGrid.ContainsKey(nHeroId);
		if (flag)
		{
			this.SetOneHeroGrid(this.szHeroGrid[nHeroId].gameObject, nHeroId);
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x000AB718 File Offset: 0x000A9918
	public void SetOneHeroGrid(int nHeroId)
	{
		bool flag = !this.szHeroGrid.ContainsKey(nHeroId);
		if (!flag)
		{
			GameObject gameObject = this.szHeroGrid[nHeroId].gameObject;
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nHeroId);
			bool flag2 = oneHeroData == null || oneHeroData.unlockData == null;
			if (flag2)
			{
				Image component = gameObject.transform.Find("expvalue").GetComponent<Image>();
				component.fillAmount = 0f;
				Text component2 = gameObject.transform.Find("lvl").GetComponent<Text>();
				component2.text = Language.GetLang(7035, new object[0]);
				component2 = gameObject.transform.Find("ActText").GetComponent<Text>();
				component2.enabled = false;
				GameObject gameObject2 = gameObject.transform.Find("mash").gameObject;
				gameObject2.SetActive(true);
				component = gameObject.transform.Find("StateSp").GetComponent<Image>();
				component.enabled = false;
				Toggle component3 = gameObject.GetComponent<Toggle>();
				component3.targetGraphic.raycastTarget = false;
				EventTriggerListener.Get(gameObject).onClick = null;
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHero);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHero);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", string.Empty);
			}
			else
			{
				Image component = gameObject.transform.Find("expvalue").GetComponent<Image>();
				Text component2 = gameObject.transform.Find("lvl").GetComponent<Text>();
				GameObject gameObject2 = gameObject.transform.Find("mash").gameObject;
				bool flag3 = oneHeroData.nOpenState > 0;
				if (flag3)
				{
					component.fillAmount = (float)oneHeroData.nHeroExp * 1f / (float)Fun.GetHeroLvlupExp(oneHeroData.nHeroLvl) * 1f;
					component2.text = oneHeroData.nHeroLvl.ToString();
					gameObject2.SetActive(false);
				}
				else
				{
					component.fillAmount = 0f;
					component2.text = Language.GetLang(7035, new object[0]);
					gameObject2.SetActive(true);
				}
				component = gameObject.transform.Find("XianSp").GetComponent<Image>();
				bool flag4 = oneHeroData.nHeroNewType == 1;
				if (flag4)
				{
					bool flag5 = WebParam.client_language == "zh";
					if (flag5)
					{
						component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_xian");
					}
					bool flag6 = WebParam.client_language == "baxi";
					if (flag6)
					{
						component.sprite = Window.spriteName(AtlasType.ATLAS_RESC_BAXI, "mainui_baxi_xian");
					}
					component.enabled = true;
					component.SetNativeSize();
				}
				else
				{
					component.enabled = false;
				}
				component = gameObject.transform.Find("XinSp").GetComponent<Image>();
				bool flag7 = oneHeroData.nHeroNewType == 2 || Gem_Data.Instance.GetGemNewState(nHeroId) == 1;
				if (flag7)
				{
					bool flag8 = WebParam.client_language == "zh";
					if (flag8)
					{
						component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_xin");
					}
					bool flag9 = WebParam.client_language == "baxi";
					if (flag9)
					{
						component.sprite = Window.spriteName(AtlasType.ATLAS_RESC_BAXI, "mainui_baxi_xin");
					}
					component.enabled = true;
					component.SetNativeSize();
				}
				else
				{
					component.enabled = false;
				}
				Toggle component3 = gameObject.GetComponent<Toggle>();
				component3.targetGraphic.raycastTarget = true;
				EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHero);
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHero);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHero);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				bool flag10 = this.nSlcHeroId == -1 && this.nDefHeroId == 0;
				if (flag10)
				{
					this.nDefHeroId = oneHeroData.nHeroId;
					this.OnClickHero(gameObject);
				}
			}
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000ABB50 File Offset: 0x000A9D50
	public void RefreshSlcHeroInfo()
	{
		bool flag = this.nSlcHeroId != -1;
		if (flag)
		{
			this.SetOneHeroGrid(this.nSlcHeroId);
			this.SetUnlockTgNewState(this.nSlcHeroId);
			this.OnClickUnlock(1);
		}
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000ABB94 File Offset: 0x000A9D94
	private void SetOneHeroGrid(GameObject obj, int nheroid)
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nheroid);
		bool flag = oneHeroData != null;
		if (flag)
		{
			RawImage component = obj.transform.Find("heropic").GetComponent<RawImage>();
			fUIManager.GetInstance().GetIconAsset("herounlock/skin/" + oneHeroData.unlockData.szSkinUnlock[1].sIconName + ".txt", component);
			this.SetOneHeroGrid(nheroid);
		}
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000ABC08 File Offset: 0x000A9E08
	private void ResetAni()
	{
		EmotionUnlock useEmotionData = HeroDataManager.GetInstance().GetUseEmotionData(this.nSlcHeroId, PlayerDataManager.getInstance().HUserData.m_nCid);
		bool flag = useEmotionData != null && !string.IsNullOrEmpty(useEmotionData.animParm);
		if (flag)
		{
			this.emotionState = int.Parse(useEmotionData.animParm);
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x000ABC64 File Offset: 0x000A9E64
	private void SetClickHero(int heroid)
	{
		bool flag = !HeroDataManager.GetInstance().allOpenHero.ContainsKey(heroid);
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(0, null);
		}
		else
		{
			this.ResetLastNew();
			this.nSlcHeroId = heroid;
			bool flag2 = this.openUIParam.Count > 0;
			if (flag2)
			{
				this.nSlcType = (int)this.openUIParam[1];
			}
			this.SetUnlockTgNewState(this.nSlcHeroId);
			this.OnClickUnlock(0);
		}
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000ABCE8 File Offset: 0x000A9EE8
	public void SetUnlockTgNewState(int heroid)
	{
		bool flag = this.nSlcHeroId != heroid;
		if (!flag)
		{
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
			bool flag2 = oneHeroData != null && oneHeroData.unlockData != null;
			if (flag2)
			{
				for (int i = 0; i < this.szUnlockTg.Count; i++)
				{
					int num = 0;
					GameObject gameObject = this.szUnlockTg[i].gameObject;
					Image component = gameObject.transform.Find("StateSp").GetComponent<Image>();
					bool flag3 = gameObject.name == "SkinTg";
					if (flag3)
					{
						num = oneHeroData.unlockData.skinNewType;
					}
					bool flag4 = gameObject.name == "WeaponTg";
					if (flag4)
					{
						num = oneHeroData.unlockData.weaponNewType;
					}
					bool flag5 = gameObject.name == "EmotionTg";
					if (flag5)
					{
						num = oneHeroData.unlockData.emotionNewType;
					}
					bool flag6 = gameObject.name == "PoseTg";
					if (flag6)
					{
						num = oneHeroData.unlockData.poseNewType;
					}
					bool flag7 = gameObject.name == "SkillTg";
					if (flag7)
					{
						num = ((Gem_Data.Instance.GetGemNewState(heroid) == 1) ? 2 : 0);
					}
					bool flag8 = num == 0;
					if (flag8)
					{
						component.enabled = false;
					}
					else
					{
						bool flag9 = WebParam.client_language == "zh";
						if (flag9)
						{
							component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, (num != 1) ? "mainui_xin" : "mainui_xian");
						}
						bool flag10 = WebParam.client_language == "baxi";
						if (flag10)
						{
							component.sprite = Window.spriteName(AtlasType.ATLAS_RESC_BAXI, (num != 1) ? "mainui_baxi_xin" : "mainui_baxi_xian");
						}
						component.enabled = true;
						component.SetNativeSize();
					}
				}
			}
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.nSlcHeroId];
			this.heroName.text = heroData.hname;
		}
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000ABF18 File Offset: 0x000AA118
	public void SetHeroBaseData()
	{
		bool flag = this.nSlcType != 0;
		if (!flag)
		{
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
			bool flag2 = oneHeroData != null && oneHeroData.unlockData != null;
			if (flag2)
			{
				bool flag3 = oneHeroData.nOpenState < 1;
				if (flag3)
				{
					this.heroUnlockUI.SetActive(true);
					this.unlockDataObj.SetActive(false);
				}
				else
				{
					this.heroUnlockUI.SetActive(false);
					this.unlockDataObj.SetActive(true);
					this.heroLevel.text = Language.GetLang(7024, new object[]
					{
						oneHeroData.nHeroLvl
					});
					this.heroExpBar.fillAmount = (float)oneHeroData.nHeroExp * 1f / (float)Fun.GetHeroLvlupExp(oneHeroData.nHeroLvl) * 1f;
					this.SetHeroMatchData(this.nSlcHeroId);
				}
			}
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000AC00C File Offset: 0x000AA20C
	public void SetHeroMatchData(int heroid)
	{
		bool flag = this.nSlcHeroId == heroid;
		if (flag)
		{
			ArrayList heroMatchData = HeroDataManager.GetInstance().GetHeroMatchData(this.nSlcHeroId);
			bool flag2 = heroMatchData.Count == 0;
			if (flag2)
			{
				this.heroFightTime.text = Fun.SecToMaxTime(0, "h", "s");
				this.heroWinNum.text = "0";
			}
			else
			{
				this.heroFightTime.text = Fun.SecToMaxTime((int)heroMatchData[4], "h", "s");
				this.heroWinNum.text = heroMatchData[2].ToString();
			}
		}
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000AC0BC File Offset: 0x000AA2BC
	private void SetHeroAwardData()
	{
		fUIManager.OnClearObj(this.szAwardRend);
		HeroAwardData heroAwardData = fGameManager.mloadConfig.AllHeroAward[this.nSlcHeroId];
		for (int i = 0; i < heroAwardData.szAward.Count; i++)
		{
			ArrayList arrayList = (ArrayList)heroAwardData.szAward[i];
			int num = (int)arrayList[0];
			int ntype = (int)arrayList[1];
			int nvalue = (int)arrayList[2];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.awardRender);
			gameObject.transform.SetParent(this.awardRender.transform.parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.name = "HeroAward" + num.ToString();
			gameObject.SetActive(true);
			bool flag = HeroDataManager.GetInstance().IsAwarded(heroAwardData.nHeroid, num);
			string text = flag ? "#00ff00" : "#ffffff";
			Text component = gameObject.transform.Find("Level").GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				"<color=",
				text,
				">",
				Language.GetLang(7037, new object[]
				{
					num
				}),
				"</color>"
			});
			component = gameObject.transform.Find("DescPanel/Desc").GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				"<color=",
				text,
				">",
				heroAwardData.GetDesc(ntype, nvalue),
				"</color>"
			});
			Image component2 = gameObject.transform.Find("DescPanel/Wancheng").GetComponent<Image>();
			component2.enabled = flag;
			this.szAwardRend.Add(gameObject);
		}
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000AC2E0 File Offset: 0x000AA4E0
	private void LoadUIOK(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null || HeroInfoUICon.inst == null || !this.visible;
		if (!flag)
		{
			HeroInfoBg heroInfoBg = (HeroInfoBg)customParam;
			bool flag2 = heroInfoBg == null || heroInfoBg.nID != this.showBgId;
			if (!flag2)
			{
				bool flag3 = BackgroundUICon.inst == null;
				if (flag3)
				{
					BackgroundUICon.isShowBack = false;
				}
				else
				{
					BackgroundUICon.inst.SetMainBackground(false, string.Empty);
				}
				bool flag4 = this.modelScn != null;
				if (flag4)
				{
					bool flag5 = this.modelScn.name == heroInfoBg.sUIName;
					if (flag5)
					{
						return;
					}
					UnityEngine.Object.DestroyImmediate(this.modelScn);
					this.modelScn = null;
				}
				this.modelScn = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				this.modelScn.transform.localPosition = new Vector3(-1000f, -1000f, -800f);
				this.modelScn.transform.localScale = Vector3.one;
				this.modelScn.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.modelScn.name = heroInfoBg.sUIName;
				Transform transform = this.modelScn.transform.Find("Camera");
				bool flag6 = transform != null && GameRoot.inst != null && GameRoot.inst.audioEar != null;
				if (flag6)
				{
					GameRoot.inst.audioEar.transform.position = transform.position;
				}
				this.tableCloneObj = UnityEngine.Object.Instantiate<GameObject>(this.modelTable);
				this.tableCloneObj.transform.SetParent(this.modelScn.transform);
				this.tableCloneObj.transform.localPosition = new Vector3(0f, -1.16f, -4f);
				this.tableCloneObj.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
				this.tableCloneObj.transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
				this.tableCloneObj.SetActive(false);
				bool flag7 = this.modelObj == null;
				if (flag7)
				{
					this.LoadHeroModel(null, -1, -1);
				}
			}
		}
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000AC57C File Offset: 0x000AA77C
	private void LoadHeroModel(HeroUnlockData data = null, int nId = -1, int weapon = -1)
	{
		bool flag = this.modelScn == null;
		if (!flag)
		{
			bool flag2 = this.openUIParam.Count > 0 && (int)this.openUIParam[1] == 1;
			if (flag2)
			{
				nId = (int)this.openUIParam[2];
			}
			bool flag3 = this.openUIParam.Count > 0 && (int)this.openUIParam[1] == 2;
			if (flag3)
			{
				weapon = (int)this.openUIParam[2];
			}
			bool flag4 = data != null && this.nSlcHeroId == data.nHeroid && this.nSlcId == nId && this.nWeaponId == weapon;
			if (!flag4)
			{
				bool flag5 = this.modelObj != null;
				if (flag5)
				{
					this.modelObj.SetActive(false);
				}
				bool flag6 = this.modelWeapon != null;
				if (flag6)
				{
					this.modelWeapon.SetActive(false);
				}
				bool flag7 = data == null;
				if (flag7)
				{
					bool flag8 = !fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(this.nSlcHeroId);
					if (flag8)
					{
						return;
					}
					data = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
				}
				bool flag9 = this.openUIParam.Count > 0 && data.nHeroid == (int)this.openUIParam[0];
				if (flag9)
				{
					this.openUIParam.Clear();
				}
				bool flag10 = this.nSlcType == 2;
				if (flag10)
				{
					bool flag11 = weapon != -1;
					if (flag11)
					{
						WeaponUnlock weaponUnlock = data.szWeaponUnlock[weapon];
						string text = data.sHeroWeapon;
						ParticleSystem componentInChildren = this.tableCloneObj.GetComponentInChildren<ParticleSystem>();
						Color[] array = new Color[]
						{
							Color.white,
							Color.white,
							new Color(0f, 1f, 0.13f),
							new Color(0.04f, 0.2f, 1f),
							new Color(0.83f, 0.04f, 1f),
							new Color(1f, 0.48f, 0f)
						};
						componentInChildren.startColor = array[weaponUnlock.nQual];
						this.tableCloneObj.SetActive(true);
						bool flag12 = !string.IsNullOrEmpty(text);
						if (flag12)
						{
							text = "HeroAnimator/" + text + ".mo";
							GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadWeaponFinish), new ArrayList
							{
								data,
								nId,
								weapon
							});
						}
					}
				}
				else
				{
					this.tableCloneObj.SetActive(false);
					string text2 = (!data.szSkinUnlock.ContainsKey(nId)) ? HeroDataManager.GetInstance().GetUseSkinData(this.nSlcHeroId, PlayerDataManager.getInstance().HUserData.m_nCid).tpsModel : data.szSkinUnlock[nId].tpsModel;
					text2 = "HeroAnimator/" + text2 + ".mo";
					GameRoot.LoadAsset(text2, new fResourceManager.DownFinishDelegate(this.LoadModelTpsFinish), new ArrayList
					{
						data,
						nId,
						weapon
					});
				}
			}
		}
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000AC914 File Offset: 0x000AAB14
	private void LoadWeaponFinish(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null || this.modelScn == null || !this.visible;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)customParam;
			HeroUnlockData heroUnlockData = (HeroUnlockData)arrayList[0];
			bool flag2 = heroUnlockData == null;
			if (!flag2)
			{
				int num = (int)arrayList[1];
				int num2 = (int)arrayList[2];
				WeaponUnlock weaponUnlock = heroUnlockData.szWeaponUnlock[num2];
				bool flag3 = heroUnlockData.key == this.nSlcHeroId;
				if (flag3)
				{
					bool flag4 = this.modelWeapon != null && int.Parse(this.modelWeapon.name.Replace("heroweapon", string.Empty)) != this.nSlcHeroId;
					if (flag4)
					{
						UnityEngine.Object.DestroyImmediate(this.modelWeapon);
						this.modelWeapon = null;
					}
					bool flag5 = this.modelWeapon == null;
					if (flag5)
					{
						this.modelWeapon = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
						this.modelWeapon.name = string.Format("heroweapon{0}", heroUnlockData.key);
						this.modelWeapon.transform.SetParent(this.modelScn.transform);
						this.modelWeapon.transform.localPosition = new Vector3(0f, heroUnlockData.weaponYPos, -4f);
						this.modelWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, (float)heroUnlockData.weaponAngle, 0f));
						this.modelWeapon.transform.localScale = Vector3.one * heroUnlockData.weaponScale;
					}
					CHeroWeapon component = this.modelWeapon.GetComponent<CHeroWeapon>();
					bool flag6 = component != null;
					if (flag6)
					{
						component.SetHeroData(this.nSlcHeroId, PlayerDataManager.getInstance().HUserData.m_nCid);
						component.SetHeroWeapon(3, num2, 2);
					}
					this.modelWeapon.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x000ACB50 File Offset: 0x000AAD50
	private void LoadModelTpsFinish(WWW www, object obj)
	{
		bool flag = www == null || www.error != null || this.modelScn == null || !this.visible;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)obj;
			HeroUnlockData heroUnlockData = (HeroUnlockData)arrayList[0];
			bool flag2 = heroUnlockData == null;
			if (!flag2)
			{
				int key = (int)arrayList[1];
				int num = (int)arrayList[2];
				bool flag3 = heroUnlockData.key != this.nSlcHeroId || !this.visible;
				if (!flag3)
				{
					bool flag4 = this.modelObj != null;
					if (flag4)
					{
						UnityEngine.Object.DestroyImmediate(this.modelObj);
						this.modelAni = null;
					}
					this.modelObj = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
					this.modelObj.name = "heroinfo_" + heroUnlockData.key.ToString();
					this.modelObj.transform.SetParent(this.modelScn.transform);
					this.modelObj.transform.localPosition = heroUnlockData.modelPos;
					this.modelObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
					this.modelObj.transform.localScale = Vector3.one * heroUnlockData.modelScale;
					Transform[] componentsInChildren = this.modelObj.transform.GetComponentsInChildren<Transform>();
					foreach (Transform transform in componentsInChildren)
					{
						bool flag5 = transform.name.Contains("yinshen") || transform.name.Contains("hongxian") || transform.name.Contains("honwai");
						if (flag5)
						{
							Renderer component = transform.GetComponent<Renderer>();
							bool flag6 = component != null;
							if (flag6)
							{
								component.enabled = false;
							}
						}
					}
					this.modelAni = this.modelObj.transform.GetComponent<Animator>();
					ModelEvent modelEvent = this.modelObj.AddComponent<ModelEvent>();
					modelEvent.InitEvent(this.nSlcHeroId, this.modelObj);
					Transform transform2 = this.modelObj.transform.Find("GrounderTrs");
					bool flag7 = transform2 != null;
					if (flag7)
					{
						UnityEngine.Object.DestroyImmediate(transform2.gameObject);
					}
					transform2 = this.modelObj.transform.Find("HitReactionTrs");
					bool flag8 = transform2 != null;
					if (flag8)
					{
						UnityEngine.Object.DestroyImmediate(transform2.gameObject);
					}
					string text = (!heroUnlockData.szSkinUnlock.ContainsKey(key)) ? HeroDataManager.GetInstance().GetUseSkinData(this.nSlcHeroId, PlayerDataManager.getInstance().HUserData.m_nCid).tpsControl : heroUnlockData.szSkinUnlock[key].tpsControl;
					bool flag9 = text != "0";
					if (flag9)
					{
						this.modelObj.SetActive(false);
						GameRoot.LoadAsset("HeroAnimator/" + text + ".mo", new fResourceManager.DownFinishDelegate(this.LoadControlTpsFinish), new ArrayList
						{
							heroUnlockData,
							num
						});
					}
				}
			}
		}
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x000ACEC0 File Offset: 0x000AB0C0
	private void LoadControlTpsFinish(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null || !this.visible;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)customParam;
			HeroUnlockData heroUnlockData = (HeroUnlockData)arrayList[0];
			bool flag2 = heroUnlockData == null || this.modelAni == null || this.modelObj == null;
			if (!flag2)
			{
				int nid = (int)arrayList[1];
				bool flag3 = heroUnlockData.key == this.nSlcHeroId;
				if (flag3)
				{
					this.modelAni.runtimeAnimatorController = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
					this.modelObj.SetActive(true);
					DynamicBone[] components = this.modelObj.transform.GetComponents<DynamicBone>();
					foreach (DynamicBone dynamicBone in components)
					{
						dynamicBone.b_CheckDis = false;
						dynamicBone.switchCheck = false;
					}
					CHeroWeapon component = this.modelObj.GetComponent<CHeroWeapon>();
					bool flag4 = component != null;
					if (flag4)
					{
						component.SetHeroData(this.nSlcHeroId, PlayerDataManager.getInstance().HUserData.m_nCid);
						component.SetHeroWeapon(3, nid, 2);
					}
				}
			}
		}
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000AD014 File Offset: 0x000AB214
	public void OnClickUnlock(int ntype = 0)
	{
		this.szUnlockTg[this.nSlcType].isOn = true;
		this.unlockName.text = "<color=#ffae00>" + this.GetSlcTypeStr(this.nSlcType) + "</color>";
		fUIManager.OnClearObj(this.szUnlockRend);
		bool flag = ntype == 0;
		if (flag)
		{
			bool flag2 = this.openUIParam.Count > 0;
			if (flag2)
			{
				this.nSlcId = (int)this.openUIParam[2];
				this.weaponCon.nSlcId = this.nSlcId;
			}
			else
			{
				this.nSlcId = 0;
				this.weaponCon.nSlcId = 0;
			}
			this.LoadHeroModel(null, -1, -1);
		}
		switch (this.nSlcType)
		{
		case 0:
			this.infoPanelObj.SetActive(false);
			this.chipInfoObj.SetActive(true);
			this.titlePanelObj.SetActive(true);
			this.weaponPanel.SetActive(false);
			fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			this.SetHeroBaseData();
			this.SetHeroAwardData();
			this.SetUnlockInfo();
			this.ResetAni();
			break;
		case 1:
			this.heroUnlockUI.SetActive(false);
			this.unlockDataObj.SetActive(false);
			this.infoPanelObj.SetActive(true);
			this.chipInfoObj.SetActive(true);
			this.titlePanelObj.SetActive(true);
			this.weaponPanel.SetActive(false);
			fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			this.RefreshSkinData();
			break;
		case 2:
		{
			this.heroUnlockUI.SetActive(false);
			this.unlockDataObj.SetActive(false);
			this.infoPanelObj.SetActive(false);
			this.chipInfoObj.SetActive(false);
			this.titlePanelObj.SetActive(true);
			this.weaponPanel.SetActive(true);
			fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			this.unlockName.text = string.Empty;
			bool flag3 = this.modelWeapon != null;
			if (flag3)
			{
				CHeroWeapon component = this.modelWeapon.GetComponent<CHeroWeapon>();
				bool flag4 = component != null;
				if (flag4)
				{
					component.ShowWeapParts();
				}
			}
			this.weaponCon.ShowWeaponData(this.nSlcHeroId, ntype);
			break;
		}
		case 3:
			this.heroUnlockUI.SetActive(false);
			this.unlockDataObj.SetActive(false);
			this.infoPanelObj.SetActive(true);
			this.chipInfoObj.SetActive(true);
			this.titlePanelObj.SetActive(true);
			this.weaponPanel.SetActive(false);
			fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			this.RefreshEmotionData();
			break;
		case 4:
			this.heroUnlockUI.SetActive(false);
			this.unlockDataObj.SetActive(false);
			this.infoPanelObj.SetActive(true);
			this.chipInfoObj.SetActive(true);
			this.titlePanelObj.SetActive(true);
			this.weaponPanel.SetActive(false);
			fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			this.RefreshPoseData();
			break;
		case 5:
			this.heroUnlockUI.SetActive(false);
			this.unlockDataObj.SetActive(false);
			this.infoPanelObj.SetActive(false);
			this.chipInfoObj.SetActive(false);
			this.titlePanelObj.SetActive(false);
			this.weaponPanel.SetActive(false);
			this.RefreshSkillData();
			break;
		}
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000AD3BC File Offset: 0x000AB5BC
	public void SetUnlockInfo()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			List<string> list = new List<string>();
			int num = 0;
			int num2 = -1;
			string[] array = null;
			string[] array2 = null;
			this.needChipNum.text = string.Empty;
			this.needChipSp.enabled = false;
			this.nowChipNum.text = string.Empty;
			this.nowChipSp.enabled = false;
			switch (this.nSlcType)
			{
			case 0:
				num = oneHeroData.nOpenState;
				text2 = oneHeroData.unlockData.sBtnData;
				list = oneHeroData.unlockData.BtnUnlockData();
				text = oneHeroData.unlockData.sUnlockDesc;
				array2 = oneHeroData.unlockData.szGiftData;
				this.nSlcId = 0;
				this.weaponCon.nSlcId = 0;
				break;
			case 1:
			{
				num = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].nLockState;
				text = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].sActDesc;
				text2 = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].sBtnData;
				list = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].BtnUnlockData();
				bool flag2 = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].nLockState > 0;
				if (flag2)
				{
					num2 = ((this.nSlcId == oneHeroData.nUseSkin) ? 1 : 0);
				}
				text3 = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].sName;
				array = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].szAddAttr;
				array2 = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].szGiftData;
				break;
			}
			case 2:
			{
				num = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState;
				text = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sActDesc;
				list = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].BtnUnlockData();
				int[] tipInfo = this.GetTipInfo(this.nSlcType, list[1]);
				bool flag3 = list[1] == "2";
				if (flag3)
				{
					this.nowChipNum.text = tipInfo[1].ToString();
					this.nowChipSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
					this.nowChipSp.enabled = true;
				}
				else
				{
					bool flag4 = list[1] == "7";
					if (flag4)
					{
						this.nowChipNum.text = tipInfo[1].ToString();
						this.nowChipSp.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
						this.nowChipSp.enabled = true;
					}
					else
					{
						bool flag5 = list[1] == "5";
						if (flag5)
						{
							this.nowChipNum.text = tipInfo[1].ToString();
							this.nowChipSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
							this.nowChipSp.enabled = true;
						}
						else
						{
							this.nowChipNum.text = string.Empty;
							this.nowChipSp.enabled = false;
						}
					}
				}
				bool flag6 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState > 0;
				if (flag6)
				{
					num2 = ((this.nSlcId == oneHeroData.nUseWeapon) ? 1 : 0);
				}
				text3 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sName;
				array = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].szAddAttr;
				array2 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].szGiftData;
				break;
			}
			case 3:
			{
				num = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].nLockState;
				text = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].sActDesc;
				text2 = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].sBtnData;
				list = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].BtnUnlockData();
				bool flag7 = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].nLockState > 0;
				if (flag7)
				{
					num2 = ((this.nSlcId == oneHeroData.nUseEmotion) ? 1 : 0);
				}
				array2 = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].szGiftData;
				break;
			}
			case 4:
			{
				num = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].nLockState;
				text = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].sActDesc;
				list = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].BtnUnlockData();
				bool flag8 = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].nLockState > 0;
				if (flag8)
				{
					num2 = ((this.nSlcId == oneHeroData.nUsePose) ? 1 : 0);
				}
				array2 = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].szGiftData;
				break;
			}
			}
			this.overTime.text = string.Empty;
			HeroLimitBuyInfo heroLimitInfo = HeroDataManager.GetInstance().GetHeroLimitInfo(this.nSlcHeroId, this.nSlcType, this.nSlcId);
			bool flag9 = heroLimitInfo != null;
			if (flag9)
			{
				list = heroLimitInfo.BtnUnlockData();
				array2 = heroLimitInfo.szGiftData;
				text = heroLimitInfo.sUnlockDesc;
			}
			HeroDiscountInfo heroDiscountInfo = HeroDataManager.GetInstance().GetHeroDiscountInfo(this.nSlcHeroId, this.nSlcType, this.nSlcId, false);
			this.heroDiscountSp.gameObject.SetActive(false);
			this.heroDiscountTxt.text = string.Empty;
			bool flag10 = array2 != null && array2.Length > 1 && int.Parse(array2[0]) == 1;
			if (flag10)
			{
				this.giftBtn.gameObject.SetActive(true);
				this.weaponCon.giftBtn.gameObject.SetActive(true);
			}
			else
			{
				this.giftBtn.gameObject.SetActive(false);
				this.weaponCon.giftBtn.gameObject.SetActive(false);
			}
			bool flag11 = num < 2;
			if (flag11)
			{
				this.descText.text = text;
				this.weaponCon.descText.text = text;
				bool flag12 = list.Count >= 3;
				if (flag12)
				{
					bool flag13 = string.IsNullOrEmpty(list[0]);
					if (flag13)
					{
						this.unlockBtn.gameObject.SetActive(false);
						this.weaponCon.unlockBtn.gameObject.SetActive(false);
					}
					else
					{
						bool flag14 = array2.Length > 1;
						if (flag14)
						{
							this.unlockBtn.gameObject.SetActive(true);
							this.weaponCon.unlockBtn.gameObject.SetActive(true);
							this.unlockLb.text = Language.GetLang(7004, new object[0]);
							this.weaponCon.unlockLb.text = Language.GetLang(7004, new object[0]);
							this.unlockBtn.name = "UnlockBtn_tip";
							this.weaponCon.unlockBtn.name = "UnlockBtn_tip";
						}
						else
						{
							this.unlockBtn.gameObject.SetActive(true);
							this.weaponCon.unlockBtn.gameObject.SetActive(true);
							this.unlockLb.text = list[0];
							this.weaponCon.unlockLb.text = list[0];
							Image component = this.jinbiSp.GetComponent<Image>();
							bool flag15 = int.Parse(list[1]) == 5;
							if (flag15)
							{
								this.jinbiSp.SetActive(true);
								component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
								component.rectTransform.sizeDelta = new Vector2(26f, 26f);
								this.weaponCon.jinbiSp.enabled = true;
								this.weaponCon.jinbiSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
								this.weaponCon.jinbiSp.rectTransform.sizeDelta = new Vector2(26f, 26f);
							}
							else
							{
								bool flag16 = int.Parse(list[1]) == 2;
								if (flag16)
								{
									this.jinbiSp.SetActive(true);
									component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
									component.rectTransform.sizeDelta = new Vector2(26f, 22f);
									this.weaponCon.jinbiSp.enabled = true;
									this.weaponCon.jinbiSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
									this.weaponCon.jinbiSp.rectTransform.sizeDelta = new Vector2(26f, 22f);
								}
								else
								{
									bool flag17 = int.Parse(list[1]) == 7;
									if (flag17)
									{
										this.jinbiSp.SetActive(true);
										component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
										component.rectTransform.sizeDelta = new Vector2(26f, 28f);
										this.weaponCon.jinbiSp.enabled = true;
										this.weaponCon.jinbiSp.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
										this.weaponCon.jinbiSp.rectTransform.sizeDelta = new Vector2(26f, 28f);
									}
									else
									{
										this.jinbiSp.SetActive(false);
										this.weaponCon.jinbiSp.enabled = false;
									}
								}
							}
							this.unlockBtn.name = "UnlockBtn" + list[1] + "_" + list[2];
							this.weaponCon.unlockBtn.name = "UnlockBtn" + list[1] + "_" + list[2];
						}
					}
				}
				else
				{
					this.unlockBtn.gameObject.SetActive(false);
					this.weaponCon.unlockBtn.gameObject.SetActive(false);
				}
				bool flag18 = list.Count >= 6;
				if (flag18)
				{
					bool flag19 = string.IsNullOrEmpty(list[3]) || array2.Length > 1;
					if (flag19)
					{
						this.useStateBtn.gameObject.SetActive(false);
						this.weaponCon.useStateBtn.gameObject.SetActive(false);
					}
					else
					{
						this.useStateBtn.gameObject.SetActive(true);
						this.weaponCon.useStateBtn.gameObject.SetActive(true);
						this.useStateLb.text = list[3];
						this.weaponCon.useStateLb.text = list[3];
						this.useStateBtn.interactable = true;
						this.weaponCon.useStateBtn.interactable = true;
						Image component2 = this.zuanshiSp.GetComponent<Image>();
						bool flag20 = int.Parse(list[4]) == 5;
						if (flag20)
						{
							this.zuanshiSp.SetActive(true);
							component2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
							component2.rectTransform.sizeDelta = new Vector2(26f, 26f);
							this.weaponCon.zuanshiSp.enabled = true;
							this.weaponCon.zuanshiSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
							this.weaponCon.zuanshiSp.rectTransform.sizeDelta = new Vector2(26f, 26f);
						}
						else
						{
							bool flag21 = int.Parse(list[4]) == 2;
							if (flag21)
							{
								this.zuanshiSp.SetActive(true);
								component2.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
								component2.rectTransform.sizeDelta = new Vector2(26f, 22f);
								this.weaponCon.zuanshiSp.enabled = true;
								this.weaponCon.zuanshiSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
								this.weaponCon.zuanshiSp.rectTransform.sizeDelta = new Vector2(26f, 22f);
							}
							else
							{
								bool flag22 = int.Parse(list[4]) == 7;
								if (flag22)
								{
									this.zuanshiSp.SetActive(true);
									component2.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
									component2.rectTransform.sizeDelta = new Vector2(26f, 28f);
									this.weaponCon.zuanshiSp.enabled = true;
									this.weaponCon.zuanshiSp.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
									this.weaponCon.zuanshiSp.rectTransform.sizeDelta = new Vector2(26f, 28f);
								}
								else
								{
									this.zuanshiSp.SetActive(false);
									this.weaponCon.zuanshiSp.enabled = false;
								}
							}
						}
					}
					this.useStateBtn.name = "UnlockBtn" + list[4] + "_" + list[5];
					this.weaponCon.useStateBtn.name = "UnlockBtn" + list[4] + "_" + list[5];
				}
				else
				{
					this.useStateBtn.gameObject.SetActive(false);
					this.weaponCon.useStateBtn.gameObject.SetActive(false);
				}
				bool flag23 = heroDiscountInfo != null;
				if (flag23)
				{
					this.heroDiscountSp.gameObject.SetActive(true);
					this.descText.text = heroDiscountInfo.sDesc;
					this.weaponCon.descText.text = heroDiscountInfo.sDesc;
					this.heroDiscountTxt.text = Language.GetLang(7052, new object[]
					{
						heroDiscountInfo.nDiscount
					});
				}
				else
				{
					this.descText.text = text;
					this.weaponCon.descText.text = text;
				}
			}
			else
			{
				this.descText.text = string.Empty;
				this.weaponCon.descText.text = string.Empty;
				this.unlockBtn.gameObject.SetActive(false);
				this.weaponCon.unlockBtn.gameObject.SetActive(false);
				this.jinbiSp.SetActive(false);
				this.weaponCon.jinbiSp.enabled = false;
				this.zuanshiSp.SetActive(false);
				this.weaponCon.zuanshiSp.enabled = false;
				this.unlockBtn.name = "UnlockBtn";
				this.weaponCon.unlockBtn.name = "UnlockBtn";
				this.useStateBtn.name = "UseBtn";
				this.weaponCon.useStateBtn.name = "UseBtn";
				bool flag24 = num2 == -1;
				if (flag24)
				{
					this.useStateBtn.gameObject.SetActive(false);
					this.weaponCon.useStateBtn.gameObject.SetActive(false);
				}
				else
				{
					this.useStateBtn.gameObject.SetActive(true);
					this.weaponCon.useStateBtn.gameObject.SetActive(true);
					bool flag25 = num2 == 0;
					if (flag25)
					{
						this.useStateLb.text = Language.GetLang(7002, new object[0]);
						this.useStateBtn.interactable = true;
						this.weaponCon.useStateLb.text = Language.GetLang(7002, new object[0]);
						this.weaponCon.useStateBtn.interactable = true;
					}
					else
					{
						this.useStateLb.text = Language.GetLang(7011, new object[0]);
						this.useStateBtn.interactable = false;
						this.weaponCon.useStateLb.text = Language.GetLang(7011, new object[0]);
						this.weaponCon.useStateBtn.interactable = false;
					}
				}
			}
			bool activeSelf = this.unlockBtn.gameObject.activeSelf;
			if (activeSelf)
			{
				this.heroReturnSp.gameObject.SetActive(LiBaoManager.GetInstance().CheckHeroReturn(this.nSlcHeroId, this.nSlcType, this.nSlcId));
			}
			else
			{
				this.heroReturnSp.gameObject.SetActive(false);
			}
			bool activeSelf2 = this.weaponCon.unlockBtn.gameObject.activeSelf;
			if (activeSelf2)
			{
				this.weaponCon.heroReturnSp.gameObject.SetActive(LiBaoManager.GetInstance().CheckHeroReturn(this.nSlcHeroId, this.nSlcType, this.nSlcId));
			}
			else
			{
				this.weaponCon.heroReturnSp.gameObject.SetActive(false);
			}
			this.weaponCon.ShowAttrPanel(array);
			bool flag26 = array == null;
			if (flag26)
			{
				this.attrPanel.SetActive(false);
			}
			else
			{
				fUIManager.OnClearObj(this.szAttrRender);
				int num3 = 0;
				int num4 = 0;
				while (num4 < array.Length && !string.IsNullOrEmpty(array[num4]))
				{
					int num5 = int.Parse(array[num4]);
					bool flag27 = num5 != 10;
					if (flag27)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.attrRender);
						gameObject.transform.SetParent(this.attrRender.transform.parent);
						gameObject.name = string.Format("Render{0}", num5);
						gameObject.SetActive(true);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						this.szAttrRender.Add(gameObject);
						Image component3 = gameObject.transform.Find("Image").GetComponent<Image>();
						component3.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetAttrImg(num5));
						Text component4 = gameObject.transform.Find("AttrDesc").GetComponent<Text>();
						component4.text = this.GetAttrDesc(num5, array[num4 + 2]);
						num3 = 1;
					}
					num4 += 3;
				}
				bool flag28 = num3 == 1;
				if (flag28)
				{
					this.attrName.text = text3;
					this.attrPanel.SetActive(true);
				}
				else
				{
					this.attrPanel.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x000AE7DC File Offset: 0x000AC9DC
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

	// Token: 0x0600124A RID: 4682 RVA: 0x000AE884 File Offset: 0x000ACA84
	private string GetAttrDesc(int nidx, string snum)
	{
		string result = string.Empty;
		if (nidx != 2)
		{
			switch (nidx)
			{
			case 7:
				result = Language.GetLang(7055, new object[]
				{
					snum
				});
				break;
			case 8:
			case 10:
				break;
			case 9:
				result = Language.GetLang(7048, new object[]
				{
					snum
				});
				break;
			case 11:
				result = Language.GetLang(7059, new object[]
				{
					snum
				});
				break;
			default:
				switch (nidx)
				{
				case 16:
					result = Language.GetLang(7056, new object[]
					{
						snum
					});
					break;
				case 17:
					result = Language.GetLang(7057, new object[]
					{
						snum
					});
					break;
				case 21:
					result = Language.GetLang(7049, new object[]
					{
						snum
					});
					break;
				case 22:
					result = Language.GetLang(7050, new object[]
					{
						snum
					});
					break;
				}
				break;
			}
		}
		else
		{
			result = Language.GetLang(7047, new object[]
			{
				snum
			});
		}
		return result;
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
	public void SetChipInfoData()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = oneHeroData == null;
		if (!flag)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			int num = -1;
			int num2 = -1;
			this.needChipNum.text = string.Empty;
			this.needChipSp.enabled = false;
			this.nowChipNum.text = string.Empty;
			this.nowChipSp.enabled = false;
			int num3 = this.nSlcType;
			bool flag2 = num3 == 2;
			if (flag2)
			{
				List<string> list = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].BtnUnlockData();
				int[] tipInfo = this.GetTipInfo(this.nSlcType, list[1]);
				bool flag3 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState < 2;
				if (flag3)
				{
					num = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState;
					text = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sActDesc;
					text2 = Language.GetLang(tipInfo[0], new object[]
					{
						tipInfo[1],
						list[2]
					});
					text4 = list[0];
					this.needChipSp.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
					this.needChipSp.enabled = true;
				}
				this.nowChipNum.text = tipInfo[1].ToString();
				this.nowChipSp.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, "heroui_jifen");
				this.nowChipSp.enabled = true;
				bool flag4 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState > 0;
				if (flag4)
				{
					num2 = ((this.nSlcId == oneHeroData.nUseWeapon) ? 1 : 0);
				}
				bool flag5 = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nLockState == 1;
				if (flag5)
				{
					DateTime d = DateTime.Parse(oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].overTime);
					int num4 = (int)(d - Fun.GetServerDate()).TotalSeconds;
					bool flag6 = num4 < 60;
					if (flag6)
					{
						num4 = 60;
					}
					text3 = Language.GetLang(7013, new object[]
					{
						Fun.SecToMaxTime(num4, "d", "m")
					});
				}
			}
			this.descText.text = text;
			this.needChipNum.text = text2;
			this.overTime.text = text3;
			bool flag7 = num == -1 || string.IsNullOrEmpty(text4);
			if (flag7)
			{
				this.unlockBtn.gameObject.SetActive(false);
			}
			else
			{
				this.unlockBtn.gameObject.SetActive(true);
				this.unlockLb.text = text4;
			}
			bool flag8 = num2 == -1;
			if (flag8)
			{
				this.useStateBtn.gameObject.SetActive(false);
			}
			else
			{
				this.useStateBtn.gameObject.SetActive(true);
				bool flag9 = num2 == 0;
				if (flag9)
				{
					this.useStateLb.text = Language.GetLang(7002, new object[0]);
					this.useStateBtn.interactable = true;
				}
				else
				{
					this.useStateLb.text = Language.GetLang(7011, new object[0]);
					this.useStateBtn.interactable = false;
				}
			}
		}
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x000AED5C File Offset: 0x000ACF5C
	private int[] GetTipInfo(int slctype, string stype)
	{
		int[] array = new int[2];
		if (!(stype == "2"))
		{
			if (!(stype == "4"))
			{
				if (!(stype == "5"))
				{
					if (stype == "7")
					{
						array[0] = 7040;
						array[1] = PlayerDataManager.getInstance().playerdet.matchMoney;
					}
				}
				else
				{
					array[0] = 7025;
					array[1] = PlayerDataManager.getInstance().playerdet.gameCurrency;
				}
			}
			else
			{
				bool flag = slctype == 1;
				if (flag)
				{
					array[0] = 7015;
					array[1] = PlayerDataManager.getInstance().playerdet.heroSkinChip;
				}
				bool flag2 = slctype == 3;
				if (flag2)
				{
					array[0] = 7015;
					array[1] = PlayerDataManager.getInstance().playerdet.heroEmotChip;
				}
			}
		}
		else
		{
			array[0] = 7026;
			array[1] = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		}
		return array;
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000AEE60 File Offset: 0x000AD060
	private void OverChipPanel(GameObject go)
	{
		string text = string.Empty;
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		bool flag = go.name == "NowChip";
		if (flag)
		{
			bool flag2 = this.nSlcType == 1;
			if (flag2)
			{
				text = Language.GetLang(7036, new object[]
				{
					PlayerDataManager.getInstance().playerdet.heroSkinChip
				});
			}
			bool flag3 = this.nSlcType == 2;
			if (flag3)
			{
				List<string> list = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].BtnUnlockData();
				int[] tipInfo = this.GetTipInfo(list[1]);
				text = Language.GetLang(tipInfo[0], new object[]
				{
					tipInfo[1]
				});
			}
			bool flag4 = this.nSlcType == 3;
			if (flag4)
			{
				text = Language.GetLang(7016, new object[]
				{
					PlayerDataManager.getInstance().playerdet.heroEmotChip
				});
			}
		}
		bool flag5 = !string.IsNullOrEmpty(text);
		if (flag5)
		{
			TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
		else
		{
			TipManager.GetInstance().HideTextTip();
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x000AEFA4 File Offset: 0x000AD1A4
	private int[] GetTipInfo(string stype)
	{
		int[] array = new int[2];
		if (!(stype == "2"))
		{
			if (stype == "7")
			{
				array[0] = 7040;
				array[1] = PlayerDataManager.getInstance().playerdet.matchMoney;
			}
		}
		else
		{
			array[0] = 5025;
			array[1] = PlayerDataManager.getInstance().playerdet.chargeCurrency;
		}
		return array;
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000AF016 File Offset: 0x000AD216
	private void ExitChipPanel(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000AF024 File Offset: 0x000AD224
	private void OnDragModel(GameObject go, PointerEventData eventData)
	{
		bool flag = this.modelObj != null && this.modelObj.activeSelf;
		if (flag)
		{
			this.modelObj.transform.localEulerAngles = new Vector3(0f, this.modelObj.transform.localEulerAngles.y - eventData.delta.x / 5f, 0f);
		}
		bool flag2 = this.modelWeapon != null && this.modelWeapon.activeSelf;
		if (flag2)
		{
			this.modelWeapon.transform.localEulerAngles = new Vector3(0f, this.modelWeapon.transform.localEulerAngles.y - eventData.delta.x / 5f, 0f);
		}
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000AF104 File Offset: 0x000AD304
	private void OnDownRotModel(GameObject go)
	{
		bool flag = go.name == "LRotBtn";
		if (flag)
		{
			this.rotAngel = -3f;
		}
		bool flag2 = go.name == "RRotBtn";
		if (flag2)
		{
			this.rotAngel = 3f;
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000AF154 File Offset: 0x000AD354
	private void OnUpRotModel(GameObject go)
	{
		this.rotAngel = 0f;
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000AF164 File Offset: 0x000AD364
	private void RefreshSkinData()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		GameObject gameObject = null;
		int num = 0;
		int index = 0;
		foreach (SkinUnlock skinUnlock in heroUnlockData.szSkinUnlock.Values)
		{
			int num2 = 0;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.unlockRender);
			gameObject2.transform.SetParent(this.unlockRender.transform.parent);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.name = "Skin_" + skinUnlock.nId.ToString();
			gameObject2.SetActive(true);
			this.szUnlockRend.Add(gameObject2);
			Image component = gameObject2.transform.Find("Background").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetImageName(skinUnlock.nQual));
			RawImage component2 = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
			component2.enabled = false;
			GetIconComponent component3 = component2.GetComponent<GetIconComponent>();
			bool flag = component3 != null;
			if (flag)
			{
				component3._Url = string.Empty;
			}
			bool flag2 = !string.IsNullOrEmpty(skinUnlock.sIconName);
			if (flag2)
			{
				fUIManager.GetInstance().GetIconAsset("herounlock/skin/" + skinUnlock.sIconName + ".txt", component2);
			}
			component = gameObject2.transform.Find("CenterSp").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetCenterImage(skinUnlock.nQual));
			bool flag3 = skinUnlock.nLockState > 0;
			if (flag3)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
			component = gameObject2.transform.Find("LockImg").GetComponent<Image>();
			bool flag4 = skinUnlock.nLockState > 0;
			if (flag4)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
			}
			component = gameObject2.transform.Find("StateSp").GetComponent<Image>();
			bool flag5 = skinUnlock.nLockState == 1;
			if (flag5)
			{
				num2 = 1;
			}
			bool flag6 = skinUnlock.nNewState == 1;
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
			bool flag10 = skinUnlock.nId == this.nSlcId;
			if (flag10)
			{
				gameObject = gameObject2;
			}
			bool flag11 = skinUnlock.nId == oneHeroData.nUseSkin;
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

	// Token: 0x06001254 RID: 4692 RVA: 0x000AF5B4 File Offset: 0x000AD7B4
	private void RefreshWeaponData()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		GameObject gameObject = null;
		int num = 0;
		int index = 0;
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

	// Token: 0x06001255 RID: 4693 RVA: 0x000AFA04 File Offset: 0x000ADC04
	private void RefreshEmotionData()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		GameObject gameObject = null;
		int num = 0;
		int index = 0;
		foreach (EmotionUnlock emotionUnlock in heroUnlockData.szEmotionUnlock.Values)
		{
			int num2 = 0;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.unlockRender);
			gameObject2.transform.SetParent(this.unlockRender.transform.parent);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.name = "Emotion_" + emotionUnlock.nId.ToString();
			gameObject2.SetActive(true);
			this.szUnlockRend.Add(gameObject2);
			Image component = gameObject2.transform.Find("Background").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetImageName(emotionUnlock.nQual));
			RawImage component2 = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
			component2.enabled = false;
			GetIconComponent component3 = component2.GetComponent<GetIconComponent>();
			bool flag = component3 != null;
			if (flag)
			{
				component3._Url = string.Empty;
			}
			bool flag2 = !string.IsNullOrEmpty(emotionUnlock.sIconName);
			if (flag2)
			{
				fUIManager.GetInstance().GetIconAsset("herounlock/emotion/" + emotionUnlock.sIconName + ".txt", component2);
			}
			component = gameObject2.transform.Find("CenterSp").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetCenterImage(emotionUnlock.nQual));
			bool flag3 = emotionUnlock.nLockState > 0;
			if (flag3)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
			component = gameObject2.transform.Find("LockImg").GetComponent<Image>();
			bool flag4 = emotionUnlock.nLockState > 0;
			if (flag4)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
			}
			component = gameObject2.transform.Find("StateSp").GetComponent<Image>();
			bool flag5 = emotionUnlock.nLockState == 1;
			if (flag5)
			{
				num2 = 1;
			}
			bool flag6 = emotionUnlock.nNewState == 1;
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
			bool flag10 = emotionUnlock.nId == this.nSlcId;
			if (flag10)
			{
				gameObject = gameObject2;
			}
			bool flag11 = emotionUnlock.nId == oneHeroData.nUseEmotion;
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

	// Token: 0x06001256 RID: 4694 RVA: 0x000AFE54 File Offset: 0x000AE054
	private void RefreshPoseData()
	{
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		GameObject gameObject = null;
		int num = 0;
		int index = 0;
		foreach (PoseUnlock poseUnlock in heroUnlockData.szPoseUnlock.Values)
		{
			int num2 = 0;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.unlockRender);
			gameObject2.transform.SetParent(this.unlockRender.transform.parent);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.name = "Pose_" + poseUnlock.nId.ToString();
			gameObject2.SetActive(true);
			this.szUnlockRend.Add(gameObject2);
			Image component = gameObject2.transform.Find("Background").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetImageName(poseUnlock.nQual));
			RawImage component2 = gameObject2.transform.Find("RawImage").GetComponent<RawImage>();
			component2.enabled = false;
			GetIconComponent component3 = component2.GetComponent<GetIconComponent>();
			bool flag = component3 != null;
			if (flag)
			{
				component3._Url = string.Empty;
			}
			bool flag2 = !string.IsNullOrEmpty(poseUnlock.sIconName);
			if (flag2)
			{
				fUIManager.GetInstance().GetIconAsset("herounlock/pose/" + poseUnlock.sIconName + ".txt", component2);
			}
			component = gameObject2.transform.Find("CenterSp").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, this.GetCenterImage(poseUnlock.nQual));
			bool flag3 = poseUnlock.nLockState > 0;
			if (flag3)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
			component = gameObject2.transform.Find("LockImg").GetComponent<Image>();
			bool flag4 = poseUnlock.nLockState > 0;
			if (flag4)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
			}
			component = gameObject2.transform.Find("StateSp").GetComponent<Image>();
			bool flag5 = poseUnlock.nLockState == 1;
			if (flag5)
			{
				num2 = 1;
			}
			bool flag6 = poseUnlock.nNewState == 1;
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
			bool flag10 = poseUnlock.nId == this.nSlcId;
			if (flag10)
			{
				gameObject = gameObject2;
			}
			bool flag11 = poseUnlock.nId == oneHeroData.nUsePose;
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

	// Token: 0x06001257 RID: 4695 RVA: 0x000B02A4 File Offset: 0x000AE4A4
	private void RefreshSkillData()
	{
		this.ResetAni();
		fUIManager.GetInstance().ShowWin("Gem_UI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.LoadGemUIOk), null, true);
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000B02D0 File Offset: 0x000AE4D0
	private void LoadGemUIOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = !(Gem_UICon.Instance == null);
		if (flag)
		{
			Gem_UICon.Instance.Hero_ID = this.nSlcHeroId;
			Gem_View gem_View = UIObj.GetComponent<Gem_View>();
			bool flag2 = gem_View == null;
			if (flag2)
			{
				gem_View = UIObj.AddComponent<Gem_View>();
				Gem_UICon.Instance.View_Component = gem_View;
				gem_View.Initialize();
			}
			else
			{
				gem_View.Display_Package();
				gem_View.Display_Equip();
			}
			gem_View.Clear_Synthetic_Data();
			gem_View.Batch_Synthetic_Place_Cancel(null);
			UIObj.transform.SetParent(base.gameObject.transform);
			bool flag3 = this.nSlcType != 5;
			if (flag3)
			{
				fUIManager.GetInstance().SetWinActive("Gem_UI", false);
			}
		}
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x000B0394 File Offset: 0x000AE594
	public void OnClickUnlockTg(GameObject go)
	{
		go.GetComponent<Toggle>().isOn = true;
		int num = int.Parse(go.name.Split(new char[]
		{
			'_'
		})[1]);
		int num2 = 0;
		int num3 = 0;
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		bool flag = this.nSlcType == 1;
		if (flag)
		{
			SkinUnlock skinUnlock = heroUnlockData.szSkinUnlock[num];
			bool flag2 = skinUnlock.nNewState == 1;
			if (flag2)
			{
				num3 = 1;
			}
			this.LoadHeroModel(heroUnlockData, skinUnlock.nId, -1);
		}
		bool flag3 = this.nSlcType == 2;
		if (flag3)
		{
			WeaponUnlock weaponUnlock = heroUnlockData.szWeaponUnlock[num];
			bool flag4 = weaponUnlock.nNewState == 1;
			if (flag4)
			{
				num3 = 1;
			}
			this.LoadHeroModel(heroUnlockData, -1, num);
		}
		bool flag5 = this.nSlcType == 3;
		if (flag5)
		{
			EmotionUnlock emotionUnlock = heroUnlockData.szEmotionUnlock[num];
			bool flag6 = emotionUnlock.nNewState == 1;
			if (flag6)
			{
				num3 = 1;
			}
			bool flag7 = !string.IsNullOrEmpty(emotionUnlock.animParm);
			if (flag7)
			{
				num2 = int.Parse(emotionUnlock.animParm);
			}
		}
		bool flag8 = this.nSlcType == 4;
		if (flag8)
		{
			PoseUnlock poseUnlock = heroUnlockData.szPoseUnlock[num];
			bool flag9 = poseUnlock.nNewState == 1;
			if (flag9)
			{
				num3 = 1;
			}
			bool flag10 = !string.IsNullOrEmpty(poseUnlock.animParm);
			if (flag10)
			{
				num2 = int.Parse(poseUnlock.animParm);
			}
		}
		bool flag11 = this.nSlcType == 5;
		if (flag11)
		{
			num3 = Gem_Data.Instance.GetGemNewState(this.nSlcHeroId);
		}
		this.nSlcId = num;
		this.weaponCon.nSlcId = num;
		bool flag12 = num2 == 0;
		if (flag12)
		{
			this.ResetAni();
		}
		else
		{
			this.emotionState = num2;
			bool flag13 = this.modelAni != null && this.modelAni.isInitialized;
			if (flag13)
			{
				this.modelAni.SetInteger("HuDongState", this.emotionState);
			}
		}
		bool flag14 = num3 == 1;
		if (flag14)
		{
			bool flag15 = this.nSlcType == 5;
			if (flag15)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(3020117, new ArrayList
				{
					this.nSlcHeroId,
					0
				});
			}
			else
			{
				HeroDataManager.GetInstance().SendHeroDataMsgToServer(12013, new ArrayList
				{
					this.nSlcHeroId,
					this.nSlcType,
					this.nSlcId
				});
			}
		}
		this.SetUnlockInfo();
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x000B0650 File Offset: 0x000AE850
	public void OverUnlockTg(GameObject go)
	{
		int key = int.Parse(go.name.Split(new char[]
		{
			'_'
		})[1]);
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string[] array = null;
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nSlcHeroId];
		bool flag = this.nSlcType == 1;
		if (flag)
		{
			SkinUnlock skinUnlock = heroUnlockData.szSkinUnlock[key];
			text2 = "<size=22>" + skinUnlock.sName + "</size>";
			array = skinUnlock.szAddAttr;
			bool flag2 = skinUnlock.nLockState == 1;
			if (flag2)
			{
				int num = (int)(DateTime.Parse(skinUnlock.overTime) - Fun.GetServerDate()).TotalSeconds;
				bool flag3 = num < 60;
				if (flag3)
				{
					num = 60;
				}
				text = Language.GetLang(7029, new object[]
				{
					Fun.SecToMaxTime(num, "d", "m")
				});
			}
			bool flag4 = skinUnlock.nLockState < 1;
			if (flag4)
			{
				text3 = skinUnlock.sActDesc;
			}
		}
		bool flag5 = this.nSlcType == 2;
		if (flag5)
		{
			WeaponUnlock weaponUnlock = heroUnlockData.szWeaponUnlock[key];
			text2 = "<size=22>" + weaponUnlock.sName + "</size>";
			array = weaponUnlock.szAddAttr;
			bool flag6 = weaponUnlock.nLockState == 1;
			if (flag6)
			{
				int num2 = (int)(DateTime.Parse(weaponUnlock.overTime) - Fun.GetServerDate()).TotalSeconds;
				bool flag7 = num2 < 60;
				if (flag7)
				{
					num2 = 60;
				}
				text = Language.GetLang(7029, new object[]
				{
					Fun.SecToMaxTime(num2, "d", "m")
				});
			}
			bool flag8 = weaponUnlock.nLockState < 1;
			if (flag8)
			{
				text3 = weaponUnlock.sActDesc;
			}
		}
		bool flag9 = this.nSlcType == 3;
		if (flag9)
		{
			EmotionUnlock emotionUnlock = heroUnlockData.szEmotionUnlock[key];
			text2 = "<size=22>" + emotionUnlock.sName + "</size>";
			bool flag10 = emotionUnlock.nLockState == 1;
			if (flag10)
			{
				int num3 = (int)(DateTime.Parse(emotionUnlock.overTime) - Fun.GetServerDate()).TotalSeconds;
				bool flag11 = num3 < 60;
				if (flag11)
				{
					num3 = 60;
				}
				text = Language.GetLang(7029, new object[]
				{
					Fun.SecToMaxTime(num3, "d", "m")
				});
			}
			bool flag12 = emotionUnlock.nLockState < 1;
			if (flag12)
			{
				text3 = emotionUnlock.sActDesc;
			}
		}
		bool flag13 = this.nSlcType == 4;
		if (flag13)
		{
			PoseUnlock poseUnlock = heroUnlockData.szPoseUnlock[key];
			text2 = "<size=22>" + poseUnlock.sName + "</size>";
			bool flag14 = poseUnlock.nLockState == 1;
			if (flag14)
			{
				int num4 = (int)(DateTime.Parse(poseUnlock.overTime) - Fun.GetServerDate()).TotalSeconds;
				bool flag15 = num4 < 60;
				if (flag15)
				{
					num4 = 60;
				}
				text = Language.GetLang(7029, new object[]
				{
					Fun.SecToMaxTime(num4, "d", "m")
				});
			}
			bool flag16 = poseUnlock.nLockState < 1;
			if (flag16)
			{
				text3 = poseUnlock.sActDesc;
			}
		}
		bool flag17 = array != null;
		if (flag17)
		{
			int num5 = 0;
			while (num5 < array.Length && !string.IsNullOrEmpty(array[num5]))
			{
				bool flag18 = int.Parse(array[num5]) != 10;
				if (flag18)
				{
					text2 = text2 + "\n" + this.GetAttrDesc(int.Parse(array[num5]), array[num5 + 2]);
				}
				num5 += 3;
			}
		}
		bool flag19 = !string.IsNullOrEmpty(text3);
		if (flag19)
		{
			text2 = text2 + "\n" + text3;
		}
		bool flag20 = !string.IsNullOrEmpty(text);
		if (flag20)
		{
			bool flag21 = go.GetComponent<Toggle>().isOn && !string.IsNullOrEmpty(this.overTime.text);
			if (flag21)
			{
				text = Language.GetLang(7029, new object[]
				{
					this.overTime.text.Substring(7)
				});
			}
			text2 = text2 + "\n" + text;
		}
		bool flag22 = !string.IsNullOrEmpty(text2);
		if (flag22)
		{
			TipManager.GetInstance().ShowTextTip(text2, TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000B0AE3 File Offset: 0x000AECE3
	public void ExitUnlockTg(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000B0AF4 File Offset: 0x000AECF4
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

	// Token: 0x0600125D RID: 4701 RVA: 0x000B0B40 File Offset: 0x000AED40
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

	// Token: 0x0600125E RID: 4702 RVA: 0x000B0B8C File Offset: 0x000AED8C
	private void OnExitUnlock(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000B0B9C File Offset: 0x000AED9C
	private void OnOverUnlock(GameObject go)
	{
		string str = string.Empty;
		int ntype = 0;
		string name = go.name;
		string a = name;
		if (!(a == "DataTg"))
		{
			if (!(a == "SkinTg"))
			{
				if (!(a == "WeaponTg"))
				{
					if (!(a == "EmotionTg"))
					{
						if (!(a == "PoseTg"))
						{
							if (a == "SkillTg")
							{
								ntype = 5;
							}
						}
						else
						{
							ntype = 4;
						}
					}
					else
					{
						ntype = 3;
					}
				}
				else
				{
					ntype = 2;
				}
			}
			else
			{
				ntype = 1;
			}
		}
		else
		{
			ntype = 0;
		}
		str = this.GetSlcTypeStr(ntype);
		TipManager.GetInstance().ShowTextTip("<size=20>" + str + "</size>", TextAnchor.MiddleCenter, 1f, 20f, -20f);
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000B0C58 File Offset: 0x000AEE58
	private void OnClickUnlock(GameObject go)
	{
		this.ResetLastNew();
		string name = go.name;
		string a = name;
		if (!(a == "DataTg"))
		{
			if (!(a == "SkinTg"))
			{
				if (!(a == "WeaponTg"))
				{
					if (!(a == "EmotionTg"))
					{
						if (!(a == "PoseTg"))
						{
							if (a == "SkillTg")
							{
								this.nSlcType = 5;
								this.OnClickUnlock(0);
							}
						}
						else
						{
							this.nSlcType = 4;
							this.OnClickUnlock(0);
						}
					}
					else
					{
						this.nSlcType = 3;
						this.OnClickUnlock(0);
						HeroDataManager.GetInstance().SendHeroDataMsgToServer(12015, new ArrayList
						{
							0,
							0,
							0,
							1
						});
					}
				}
				else
				{
					this.nSlcType = 2;
					this.OnClickUnlock(0);
					HeroDataManager.GetInstance().SendHeroDataMsgToServer(12015, new ArrayList
					{
						0,
						0,
						1,
						0
					});
				}
			}
			else
			{
				this.nSlcType = 1;
				this.OnClickUnlock(0);
				HeroDataManager.GetInstance().SendHeroDataMsgToServer(12015, new ArrayList
				{
					0,
					1,
					0,
					0
				});
			}
		}
		else
		{
			this.nSlcType = 0;
			this.OnClickUnlock(0);
		}
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000B0E1C File Offset: 0x000AF01C
	private void ResetLastNew()
	{
		bool flag = this.nSlcHeroId > 0 && this.nSlcType > 0;
		if (flag)
		{
			bool flag2 = this.nSlcType == 5;
			if (flag2)
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(3020117, new ArrayList
				{
					this.nSlcHeroId,
					0
				});
			}
			else
			{
				HeroDataManager.GetInstance().SendHeroDataMsgToServer(12013, new ArrayList
				{
					this.nSlcHeroId,
					this.nSlcType,
					-1
				});
			}
		}
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x000B0ED8 File Offset: 0x000AF0D8
	private string GetSlcTypeStr(int ntype)
	{
		string result = string.Empty;
		switch (ntype)
		{
		case 0:
			result = Language.GetLang(7005, new object[0]);
			break;
		case 1:
			result = Language.GetLang(7006, new object[0]);
			break;
		case 2:
			result = Language.GetLang(7007, new object[0]);
			break;
		case 3:
			result = Language.GetLang(7008, new object[0]);
			break;
		case 4:
			result = Language.GetLang(7009, new object[0]);
			break;
		case 5:
			result = Language.GetLang(7010, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x000B0F88 File Offset: 0x000AF188
	public void OnClickUnlockBtn(GameObject go)
	{
		bool flag = go.name == "UseBtn";
		if (flag)
		{
			this.OnClickUse();
		}
		else
		{
			bool flag2 = go.name == "UnlockBtn_tip";
			if (flag2)
			{
				HeroDataManager.GetInstance().OpenUnlockTip(this.nSlcHeroId, this.nSlcType, this.nSlcId, string.Empty, 0);
			}
			else
			{
				this.isUseZhekou = 0;
				string[] array = go.name.Replace("UnlockBtn", string.Empty).Split(new char[]
				{
					'_'
				});
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				int nowNumByType = this.GetNowNumByType(num);
				bool flag3 = nowNumByType < num2;
				if (flag3)
				{
					bool flag4 = num == 5;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					bool flag5 = num == 2;
					if (flag5)
					{
						fUIManager.GetInstance().OpenChongZhiUI("HeroInfoUICon");
					}
					bool flag6 = num == 7;
					if (flag6)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7053, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
				}
				else
				{
					bool flag7 = num == 5;
					if (flag7)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7023, new object[]
						{
							this.GetEquipName(),
							num2
						}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnClickYesBtn), num, null, null);
					}
					bool flag8 = num == 2;
					if (flag8)
					{
						this.nChargeCost = num2;
						bool flag9 = HeroDataManager.GetInstance().GetEquipZhekou(this.nSlcHeroId, this.nSlcType, this.nSlcId) == 1;
						if (flag9)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
							{
								this.GetEquipName(),
								this.nChargeCost
							}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), new TipManager.TipOKDelegate(this.OnClickYesBtn), num, null, null, Language.GetLang(39543, new object[]
							{
								ShopManager.GetInstance().GetItemNumById(1027)
							}), false, new TipManager.TipToggleDelegate(this.OnEquipZhekouCheck), 11, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
						}
						else
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7038, new object[]
							{
								this.GetEquipName(),
								num2
							}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnClickYesBtn), num, null, null);
						}
					}
					bool flag10 = num == 7;
					if (flag10)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7054, new object[]
						{
							this.GetEquipName(),
							num2
						}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.OnClickYesBtn), num, null, null);
					}
				}
			}
		}
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x000B13C8 File Offset: 0x000AF5C8
	private void OnEquipZhekouCheck(bool bstate)
	{
		int num = ShopManager.GetInstance().GetHaveItemNum(1027);
		bool flag = num >= 1 && HeroDataManager.GetInstance().GetEquipZhekou(this.nSlcHeroId, this.nSlcType, this.nSlcId) == 1;
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			num = this.nChargeCost;
			this.isUseZhekou = (bstate ? 1 : 0);
			if (bstate)
			{
				num = Mathf.CeilToInt((float)(num * itemData.nZhekou) / 100f);
			}
			bool flag2 = easy_tipCon.inst != null;
			if (flag2)
			{
				easy_tipCon.inst.ContentText.text = Language.GetLang(7038, new object[]
				{
					this.GetEquipName(),
					num
				});
			}
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000B14A0 File Offset: 0x000AF6A0
	private string GetEquipName()
	{
		string result = string.Empty;
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		switch (this.nSlcType)
		{
		case 0:
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[oneHeroData.nHeroId];
			result = heroData.hname;
			break;
		}
		case 1:
			result = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].sName;
			break;
		case 2:
			result = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sName;
			break;
		case 3:
			result = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].sName;
			break;
		case 4:
			result = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].sName;
			break;
		}
		return result;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000B1590 File Offset: 0x000AF790
	private int GetNowNumByType(int ntype)
	{
		int result = 0;
		switch (ntype)
		{
		case 2:
			result = PlayerDataManager.getInstance().playerdet.chargeCurrency;
			break;
		case 4:
		{
			bool flag = this.nSlcType == 1;
			if (flag)
			{
				result = PlayerDataManager.getInstance().playerdet.heroSkinChip;
			}
			bool flag2 = this.nSlcType == 3;
			if (flag2)
			{
				result = PlayerDataManager.getInstance().playerdet.heroEmotChip;
			}
			break;
		}
		case 5:
			result = PlayerDataManager.getInstance().playerdet.gameCurrency;
			break;
		case 7:
			result = PlayerDataManager.getInstance().playerdet.matchMoney;
			break;
		}
		return result;
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000B1648 File Offset: 0x000AF848
	private void OnClickUnlockBtn()
	{
		this.nNowChip = 0;
		this.nMaxChip = 0;
		this.sOpenName = string.Empty;
		this.nChipId = 0;
		OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.nSlcHeroId);
		switch (this.nSlcType)
		{
		case 1:
			this.nNowChip = PlayerDataManager.getInstance().playerdet.heroSkinChip;
			this.nMaxChip = int.Parse(oneHeroData.unlockData.szSkinUnlock[this.nSlcId].BtnUnlockData()[2]);
			this.sOpenName = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].sName;
			this.nChipId = oneHeroData.unlockData.szSkinUnlock[this.nSlcId].nChipId;
			break;
		case 2:
			this.nNowChip = 0;
			this.nMaxChip = int.Parse(oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].BtnUnlockData()[2]);
			this.sOpenName = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].sName;
			this.nChipId = oneHeroData.unlockData.szWeaponUnlock[this.nSlcId].nChipId;
			break;
		case 3:
			this.nNowChip = PlayerDataManager.getInstance().playerdet.heroEmotChip;
			this.nMaxChip = int.Parse(oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].BtnUnlockData()[2]);
			this.sOpenName = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].sName;
			this.nChipId = oneHeroData.unlockData.szEmotionUnlock[this.nSlcId].nChipId;
			break;
		case 4:
			this.nNowChip = PlayerDataManager.getInstance().playerdet.gameCurrency;
			this.nMaxChip = int.Parse(oneHeroData.unlockData.szPoseUnlock[this.nSlcId].BtnUnlockData()[2]);
			this.sOpenName = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].sName;
			this.nChipId = oneHeroData.unlockData.szPoseUnlock[this.nSlcId].nChipId;
			break;
		}
		bool flag = this.nSlcType == 1 || this.nSlcType == 3;
		if (flag)
		{
			bool flag2 = this.nNowChip < this.nMaxChip;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7034, new object[]
				{
					fGameManager.mloadConfig.AllItemData[this.nChipId].m_sName
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7017, new object[]
				{
					this.nMaxChip,
					fGameManager.mloadConfig.AllItemData[this.nChipId].m_sName,
					this.sOpenName
				}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickYesBtn), null, null, null);
			}
		}
		bool flag3 = this.nSlcType == 4;
		if (flag3)
		{
			bool flag4 = this.nNowChip < this.nMaxChip;
			if (flag4)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7023, new object[]
				{
					this.sOpenName,
					this.nMaxChip
				}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnClickYesBtn), null, null, null);
			}
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000B1ADA File Offset: 0x000AFCDA
	private void OnTipToggle(bool bstate)
	{
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
	private void OnClickYesBtn(object szParam)
	{
		bool flag = this.nSlcType == 0;
		if (flag)
		{
			HeroDataManager.GetInstance().SendHeroDataMsgToServer(12001, new ArrayList
			{
				this.nSlcHeroId,
				(int)szParam,
				0,
				this.isUseZhekou
			});
		}
		else
		{
			HeroDataManager.GetInstance().SendHeroDataMsgToServer(12007, new ArrayList
			{
				this.nSlcHeroId,
				this.nSlcType,
				this.nSlcId,
				(int)szParam,
				this.isUseZhekou
			});
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
	private void OnClickUse()
	{
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12009, new ArrayList
		{
			this.nSlcHeroId,
			this.nSlcType,
			this.nSlcId
		});
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000B1C24 File Offset: 0x000AFE24
	private void OnClickHero(GameObject go)
	{
		int clickHero = int.Parse(go.name.Replace("HeroGrid", string.Empty));
		foreach (Toggle toggle in this.szHeroGrid.Values)
		{
			GameObject gameObject = toggle.transform.Find("ui_xuanren").gameObject;
			bool flag = toggle.name == go.name;
			if (flag)
			{
				toggle.isOn = true;
				toggle.targetGraphic.color = new Color(1f, 1f, 1f, 0f);
				gameObject.SetActive(true);
			}
			else
			{
				toggle.isOn = false;
				toggle.targetGraphic.color = Color.white;
				gameObject.SetActive(false);
			}
		}
		this.SetClickHero(clickHero);
		HeroDataManager.GetInstance().SendHeroDataMsgToServer(12014, new ArrayList
		{
			this.nSlcHeroId
		});
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000B1D54 File Offset: 0x000AFF54
	private void OnExitHero(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000B1D64 File Offset: 0x000AFF64
	private void OnOverHero(GameObject go)
	{
		int num = int.Parse(go.name.Replace("HeroGrid", string.Empty));
		string sText = string.Empty;
		bool flag = fGameManager.mloadConfig.AllHeroList.ContainsKey(num);
		if (flag)
		{
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[num];
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(num);
			sText = "<size=20>" + heroData.hname + "</size>";
			bool flag2 = oneHeroData != null && oneHeroData.nOpenState == 1;
			if (flag2)
			{
				DateTime d = DateTime.Parse(oneHeroData.overTime);
				int num2 = (int)(d - Fun.GetServerDate()).TotalSeconds;
				sText = Language.GetLang(7039, new object[]
				{
					heroData.hname,
					Language.GetLang(9008, new object[]
					{
						num2 / 86400 + 1
					})
				});
			}
			TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleCenter, 1f, 20f, -20f);
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x000B1E7C File Offset: 0x000B007C
	private void OnClickChangeHero(GameObject go)
	{
		List<int> list = new List<int>(this.szHeroGrid.Keys);
		int nHeroId = this.nSlcHeroId;
		int i = list.IndexOf(this.nSlcHeroId);
		bool flag = i == -1;
		if (!flag)
		{
			bool flag2 = go.name == "PreBtn";
			if (flag2)
			{
				while (i >= 0)
				{
					i--;
					bool flag3 = i < 0;
					if (flag3)
					{
						i = list.Count - 1;
					}
					OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(list[i]);
					bool flag4 = oneHeroData != null && oneHeroData.unlockData != null;
					if (flag4)
					{
						nHeroId = oneHeroData.nHeroId;
						break;
					}
				}
			}
			bool flag5 = go.name == "NextBtn";
			if (flag5)
			{
				while (i < list.Count)
				{
					i++;
					bool flag6 = i >= list.Count;
					if (flag6)
					{
						i = 0;
					}
					OpenHeroData oneHeroData2 = HeroDataManager.GetInstance().GetOneHeroData(list[i]);
					bool flag7 = oneHeroData2 != null && oneHeroData2.unlockData != null;
					if (flag7)
					{
						nHeroId = oneHeroData2.nHeroId;
						break;
					}
				}
			}
			this.OnClickHero(this.szHeroGrid[nHeroId].gameObject);
		}
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000B1FC6 File Offset: 0x000B01C6
	private void ExitExpBar(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000B1FD4 File Offset: 0x000B01D4
	private void OverExpBar(GameObject go)
	{
		OpenHeroData openHeroData = HeroDataManager.GetInstance().GetOpenHeroData(this.nSlcHeroId);
		bool flag = openHeroData != null && openHeroData.nHeroLvl > 0;
		if (flag)
		{
			TipManager.GetInstance().ShowTextTip(Language.GetLang(7028, new object[]
			{
				openHeroData.nHeroExp,
				Fun.GetHeroLvlupExp(openHeroData.nHeroLvl)
			}), TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
		else
		{
			TipManager.GetInstance().HideTextTip();
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000B2063 File Offset: 0x000B0263
	private void OnClickHeroSkill(GameObject go)
	{
		fUIManager.GetInstance().ShowHeroSkillInfo();
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000B2071 File Offset: 0x000B0271
	private void OverFightTime(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(7030, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000B209F File Offset: 0x000B029F
	private void ExitFightTime(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000B20AD File Offset: 0x000B02AD
	private void OverWinNum(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(7031, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000B20DB File Offset: 0x000B02DB
	private void ExitWinNum(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000B20EC File Offset: 0x000B02EC
	public void OnClickGiftBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowInviteUI(4, 0, new ArrayList
		{
			this.nSlcHeroId,
			this.nSlcType,
			this.nSlcId
		});
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x000B2144 File Offset: 0x000B0344
	public override void Close()
	{
		base.Close();
		this.openUIParam.Clear();
		fUIManager.GetInstance().SetWinActive("Gem_UI", false);
		this.ResetLastNew();
		bool flag = this.modelScn != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.modelScn);
		}
		bool flag2 = BackgroundUICon.inst == null;
		if (flag2)
		{
			BackgroundUICon.isShowBack = true;
		}
		else
		{
			BackgroundUICon.inst.SetMainBackground(true, string.Empty);
		}
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x000B21C8 File Offset: 0x000B03C8
	public void OnChangeTab(object szParam)
	{
		bool flag = szParam is ArrayList;
		if (flag)
		{
			this.openUIParam = (ArrayList)szParam;
		}
		else
		{
			this.openUIParam.Clear();
		}
		bool flag2 = this.openUIParam.Count > 0 && this.szHeroGrid.ContainsKey((int)this.openUIParam[0]);
		if (flag2)
		{
			this.OnClickHero(this.szHeroGrid[(int)this.openUIParam[0]].gameObject);
		}
	}

	// Token: 0x04000E61 RID: 3681
	public static HeroInfoUICon inst;

	// Token: 0x04000E62 RID: 3682
	private GameObject modelScn;

	// Token: 0x04000E63 RID: 3683
	private GameObject modelObj;

	// Token: 0x04000E64 RID: 3684
	private Animator modelAni;

	// Token: 0x04000E65 RID: 3685
	private GameObject modelTable;

	// Token: 0x04000E66 RID: 3686
	private GameObject modelWeapon;

	// Token: 0x04000E67 RID: 3687
	private GameObject tableCloneObj;

	// Token: 0x04000E68 RID: 3688
	private GameObject heroGrid;

	// Token: 0x04000E69 RID: 3689
	private Dictionary<int, Toggle> szHeroGrid = new Dictionary<int, Toggle>();

	// Token: 0x04000E6A RID: 3690
	private Text heroName;

	// Token: 0x04000E6B RID: 3691
	private Button unlockBtn;

	// Token: 0x04000E6C RID: 3692
	private GameObject jinbiSp;

	// Token: 0x04000E6D RID: 3693
	private Text unlockLb;

	// Token: 0x04000E6E RID: 3694
	private Image heroReturnSp;

	// Token: 0x04000E6F RID: 3695
	private Button useStateBtn;

	// Token: 0x04000E70 RID: 3696
	private GameObject zuanshiSp;

	// Token: 0x04000E71 RID: 3697
	private Text useStateLb;

	// Token: 0x04000E72 RID: 3698
	private Button giftBtn;

	// Token: 0x04000E73 RID: 3699
	private Text heroLevel;

	// Token: 0x04000E74 RID: 3700
	private Image heroExpBar;

	// Token: 0x04000E75 RID: 3701
	private Text heroFightTime;

	// Token: 0x04000E76 RID: 3702
	private Text heroWinNum;

	// Token: 0x04000E77 RID: 3703
	private GameObject heroUnlockUI;

	// Token: 0x04000E78 RID: 3704
	private GameObject awardRender;

	// Token: 0x04000E79 RID: 3705
	private List<GameObject> szAwardRend = new List<GameObject>();

	// Token: 0x04000E7A RID: 3706
	private List<Toggle> szUnlockTg = new List<Toggle>();

	// Token: 0x04000E7B RID: 3707
	private Text unlockName;

	// Token: 0x04000E7C RID: 3708
	private Text nowChipNum;

	// Token: 0x04000E7D RID: 3709
	private Image nowChipSp;

	// Token: 0x04000E7E RID: 3710
	private GameObject unlockDataObj;

	// Token: 0x04000E7F RID: 3711
	private GameObject infoPanelObj;

	// Token: 0x04000E80 RID: 3712
	private GameObject unlockRender;

	// Token: 0x04000E81 RID: 3713
	private List<GameObject> szUnlockRend = new List<GameObject>();

	// Token: 0x04000E82 RID: 3714
	private GameObject titlePanelObj;

	// Token: 0x04000E83 RID: 3715
	private GameObject chipInfoObj;

	// Token: 0x04000E84 RID: 3716
	private Text descText;

	// Token: 0x04000E85 RID: 3717
	private Text needChipNum;

	// Token: 0x04000E86 RID: 3718
	private Image needChipSp;

	// Token: 0x04000E87 RID: 3719
	private Text overTime;

	// Token: 0x04000E88 RID: 3720
	private GameObject attrPanel;

	// Token: 0x04000E89 RID: 3721
	private Text attrName;

	// Token: 0x04000E8A RID: 3722
	private GameObject attrRender;

	// Token: 0x04000E8B RID: 3723
	private List<GameObject> szAttrRender = new List<GameObject>();

	// Token: 0x04000E8C RID: 3724
	private GameObject weaponPanel;

	// Token: 0x04000E8D RID: 3725
	private HeroWeaponCon weaponCon;

	// Token: 0x04000E8E RID: 3726
	private string sOpenName;

	// Token: 0x04000E8F RID: 3727
	private int nNowChip;

	// Token: 0x04000E90 RID: 3728
	private int nMaxChip;

	// Token: 0x04000E91 RID: 3729
	private int nChipId;

	// Token: 0x04000E92 RID: 3730
	public int nSlcHeroId = -1;

	// Token: 0x04000E93 RID: 3731
	public int nSlcType;

	// Token: 0x04000E94 RID: 3732
	private int nSlcId;

	// Token: 0x04000E95 RID: 3733
	private int nDefHeroId;

	// Token: 0x04000E96 RID: 3734
	private int nWeaponId;

	// Token: 0x04000E97 RID: 3735
	private int emotionState = 301;

	// Token: 0x04000E98 RID: 3736
	private int oldBgId = -1;

	// Token: 0x04000E99 RID: 3737
	private int showBgId = -1;

	// Token: 0x04000E9A RID: 3738
	private int nowBgId = -1;

	// Token: 0x04000E9B RID: 3739
	private GameObject lRotBtn;

	// Token: 0x04000E9C RID: 3740
	private GameObject rRotBtn;

	// Token: 0x04000E9D RID: 3741
	private float rotAngel;

	// Token: 0x04000E9E RID: 3742
	private Image heroDiscountSp;

	// Token: 0x04000E9F RID: 3743
	private Text heroDiscountTxt;

	// Token: 0x04000EA0 RID: 3744
	private int nChargeCost;

	// Token: 0x04000EA1 RID: 3745
	private int isUseZhekou;

	// Token: 0x04000EA2 RID: 3746
	private ArrayList openUIParam = new ArrayList();
}
